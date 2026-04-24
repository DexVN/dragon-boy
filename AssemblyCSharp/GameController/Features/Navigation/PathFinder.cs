using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Navigation
{
    public class PathFinder
    {
        private MapDatabase _db;
        public PathFinder(MapDatabase db) { _db = db; }

        public List<MapExit> GetPath(int startId, int endId)
        {
            if (startId == endId) return new List<MapExit>();

            Queue<int> queue = new Queue<int>();
            // Lưu vết: Map con -> Map cha (Để quay ngược lại tìm đường)
            Dictionary<int, int> parentMap = new Dictionary<int, int>();
            // Lưu vết: Map đích -> Cách thức di chuyển đến đó (Exit)
            Dictionary<int, MapExit> pathToExit = new Dictionary<int, MapExit>();

            queue.Enqueue(startId);
            parentMap[startId] = -1; // Root node

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                // Nếu đã chạm tới đích, dừng ngay lập tức (Đảm bảo ngắn nhất)
                if (current == endId)
                {
                    return ReconstructPath(parentMap, pathToExit, endId);
                }

                // Lấy tất cả các hướng đi từ Map hiện tại (Trái, Phải, Waypoint, Tàu...)
                foreach (var exit in _db.GetExits(current))
                {
                    if (!parentMap.ContainsKey(exit.DestMapID))
                    {
                        parentMap[exit.DestMapID] = current;
                        pathToExit[exit.DestMapID] = exit;
                        queue.Enqueue(exit.DestMapID);
                    }
                }
            }

            return null; // Không tìm thấy đường
        }

        private List<MapExit> ReconstructPath(Dictionary<int, int> parent, Dictionary<int, MapExit> edgeTo, int endId)
        {
            List<MapExit> path = new List<MapExit>();
            int curr = endId;
            while (parent.ContainsKey(curr) && parent[curr] != -1)
            {
                path.Add(edgeTo[curr]);
                curr = parent[curr];
            }
            path.Reverse(); // Đảo ngược lại để đi từ Start -> End
            return path;
        }
    }
}
