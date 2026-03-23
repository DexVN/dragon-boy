using AssemblyCSharp.GameController.Command;
using System;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoPilgrimage
{
    internal class AutoPilgrimage : ICommand
    {
        private static AutoPilgrimage _instance;
        public static bool IsEnabled { get; private set; }

        // CONSTANTS - VIẾT HOA TOÀN BỘ
        private const int WAIT_DURATION = 2000;      // Thời gian nghỉ (ms)
        private const int MOVE_DURATION = 100;       // Thời gian giữ phím (ms)
        private const int JUMP_KEY = 21;             // Phím nhảy
        //private const int MOVE_LEFT_KEY = 22;        // Phím sang trái
        private const int MOVE_RIGHT_KEY = 24;       // Phím sang phải
        private const int SENSOR_OFFSET = 15;        // Khoảng cách check vật cản
        private const int WAYPOINT_THRESHOLD = 50;   // Khoảng cách nhận diện Waypoint

        // Trạng thái nội bộ
        private static int _lastMapID = -1;
        private static long _lastTimeWait;
        private static long _startMoveTime;
        private static bool _isWaiting;

        public static AutoPilgrimage gI() => _instance ?? (_instance = new AutoPilgrimage());

        public void Execute(GameControllerCommand cmdObj)
        {
            IsEnabled = cmdObj.value != 0f;
            if (IsEnabled) ResetState();
        }

        private static void ResetState()
        {
            _lastMapID = -1;
            _isWaiting = false;
        }

        public static void Update()
        {
            if (!IsEnabled) return;

            // Đưa vào MainThread để đảm bảo an toàn khi tương tác với Unity API
            MainThreadDispatcher.Enqueue(ProcessAutoPilgrimage);
        }

        private static void ProcessAutoPilgrimage()
        {
            if (Char.isLoadingMap || GameCanvas.isLoading) return;

            Char me = Char.myCharz();
            if (me == null) return;

            long now = mSystem.currentTimeMillis();

            // 1. Kiểm tra nếu đã sang bản đồ mới
            if (HasChangedMap()) return;

            // 2. Xử lý trạng thái đang nghỉ
            if (IsCurrentlyWaiting(now)) return;

            // 3. Kiểm tra các điểm chuyển map (Waypoint)
            if (TryInteractWithWaypoints(me, now)) return;

            // 4. Thực hiện giữ phím di chuyển
            PerformMovement(me, now);
        }

        private static bool HasChangedMap()
        {
            if (TileMap.mapID == _lastMapID) return false;

            _lastMapID = TileMap.mapID;
            _isWaiting = false;
            GameCanvas.clearKeyHold();
            return true;
        }

        private static bool IsCurrentlyWaiting(long now)
        {
            if (!_isWaiting) return false;

            if (now - _lastTimeWait >= WAIT_DURATION)
            {
                _isWaiting = false;
                _startMoveTime = now;
                return false;
            }

            GameCanvas.clearKeyHold();
            return true;
        }

        private static bool TryInteractWithWaypoints(Char me, long now)
        {
            for (int i = 0; i < TileMap.vGo.size(); i++)
            {
                if (!(TileMap.vGo.elementAt(i) is Waypoint wp)) continue;

                // Kiểm tra phạm vi Waypoint "Trạm tàu vũ trụ"
                if (wp.name == "Trạm tàu vũ trụ" && (me.cx + WAYPOINT_THRESHOLD >= wp.minX && me.cx <= wp.maxX))
                {
                    ExecuteMapChange(me, wp);

                    _isWaiting = true;
                    _lastTimeWait = now;
                    return true;
                }
            }
            return false;
        }

        private static void ExecuteMapChange(Char me, Waypoint wp)
        {
            me.cx = (wp.minX + wp.maxX) / 2;
            me.cy = wp.maxY;

            Service.gI().charMove();
            Service.gI().requestChangeMap();
            GameCanvas.clearKeyHold();
        }

        private static void PerformMovement(Char me, long now)
        {
            float currentTimeScale = Time.timeScale;
            if (currentTimeScale < 0.1f) currentTimeScale = 1.0f;

            // Điều chỉnh thời gian giữ phím dựa trên Time.timeScale
            float adjustedMoveDuration = MOVE_DURATION / currentTimeScale; 

            // Nếu vẫn đang trong thời gian giữ phím
            if (now - _startMoveTime < adjustedMoveDuration)
            {
                // Mặc định đi PHẢI (Có thể đổi sang MOVE_LEFT_KEY và cdir = -1 nếu cần)
                GameCanvas.keyHold[MOVE_RIGHT_KEY] = true;
                me.cdir = 1;

                // Tự động nhảy khi vướng tường bên trái của tile (T_LEFT)
                int sensorX = me.cx + SENSOR_OFFSET;
                bool shouldJump = (TileMap.tileTypeAtPixel(sensorX, me.cy - 12) & TileMap.T_LEFT) != 0;
                GameCanvas.keyHold[JUMP_KEY] = shouldJump;
            }
            else
            {
                // Hết thời gian giữ phím -> Bắt đầu nghỉ
                GameCanvas.clearKeyHold();
                _isWaiting = true;
                _lastTimeWait = now;
            }
        }
    }
}