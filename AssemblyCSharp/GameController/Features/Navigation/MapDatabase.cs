using System;
using System.Collections.Generic;

namespace AssemblyCSharp.GameController.Features.Navigation
{
    public class MapDatabase
    {
        private readonly Dictionary<int, MapNode> _maps = new Dictionary<int, MapNode>();

        private static readonly List<MapExit> _emptyExits = new List<MapExit>();

        private MapNode GetOrCreateNode(int mapId)
        {
            MapNode node;
            if (!_maps.TryGetValue(mapId, out node))
            {
                node = new MapNode(mapId);
                _maps[mapId] = node;
            }
            return node;
        }

        public void AddEdge(int startId, int destId, MoveType type, int targetX = -1, int targetY = -1)
        {
            MapNode startNode = GetOrCreateNode(startId);
            GetOrCreateNode(destId);

            startNode.Exits.Add(new MapExit(destId, type, targetX, targetY));
        }

        public void AddEdge(int startId, int destId, MoveType type, short npcTemplateId, sbyte menuSelectedItem)
        {
            MapNode startNode = GetOrCreateNode(startId);
            GetOrCreateNode(destId);

            startNode.Exits.Add(new MapExit(destId, type, npcTemplateId, menuSelectedItem));
        }

        public List<MapExit> GetExits(int mapId)
        {
            MapNode node;
            if (_maps.TryGetValue(mapId, out node))
            {
                return node.Exits;
            }

            return _emptyExits;
        }
    }
}