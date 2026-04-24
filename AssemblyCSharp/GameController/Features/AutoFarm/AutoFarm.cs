using AssemblyCSharp.GameController.Command;
using System;
using System.IO;

namespace AssemblyCSharp.GameController.Features.AutoFarm
{
    internal class AutoFarm : ICommand
    {
        private static AutoFarm _instance;
        public static AutoFarm gI() => _instance ?? (_instance = new AutoFarm());

        public static bool IsAutoFarming { get; private set; } = false;

        private long _lastTimeChangeZone;
        private long _lastTimeRequestChange;
        private long _timeDetectedBoss = -1;
        private bool _isLoaded = false;
        private int _lastWaypointIndex = -1;
        private int _lastMapID = -1;

        public AutoFarm() { }

        private string GetDynamicKey(string baseKey)
        {
            string exeName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            return exeName + "_" + baseKey;
        }

        public static void Update()
        {
            if (!gI()._isLoaded)
            {
                gI().LoadSavedState();
                gI()._isLoaded = true;
            }

            if (IsAutoFarming)
            {
                if (Char.myCharz() == null || Char.myCharz().statusMe == 14) return;

                GameScr.isAutoPlay = true;
                GameScr.canAutoPlay = true;
                GameScr.gI().autoPlay();

                gI().HandleSmartFarmLogic();

            }
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            try
            {
                IsAutoFarming = cmdObj.value != 0f;
                Rms.saveRMSString(GetDynamicKey("auto_farm_state"), IsAutoFarming ? "1" : "0");

                if (!IsAutoFarming)
                {
                    GameScr.isAutoPlay = false;
                    GameScr.canAutoPlay = false;
                    if (Char.myCharz() != null) Char.myCharz().currentMovePoint = null;
                    _timeDetectedBoss = -1;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"[AutoFarm] Lỗi bật/tắt: {ex.Message}");
            }
        }

        private void HandleSmartFarmLogic()
        {
            if (!IsAutoFarming) return;

            long now = mSystem.currentTimeMillis();

            // Reset trạng thái khi sang map mới
            if (_lastMapID != TileMap.mapID)
            {
                _lastMapID = TileMap.mapID;
                _lastWaypointIndex = -1;
                _lastTimeChangeZone = now;
                _timeDetectedBoss = -1;
                return;
            }

            // Chống spam lệnh
            if (now - _lastTimeRequestChange < 500) return;

            bool noMobs = IsNoMobsLeft();
            bool hasBoss = IsBossPresent();
            bool hasStranger = IsStrangerPresent();
            bool shouldChangeZone = false;

            // 1. Nếu hết quái -> Dịch chuyển tìm quái
            if (noMobs)
            {
                MoveToNextWaypoint(now);
                return;
            }

            // 2. Nếu có Boss -> Chờ 15s
            if (hasBoss)
            {
                if (_timeDetectedBoss == -1)
                {
                    _timeDetectedBoss = now;
                    Logger.Info("[AutoFarm] Phát hiện Boss! Sẽ đổi khu sau 15s...");
                }
                if (now - _timeDetectedBoss >= 15000) shouldChangeZone = true;
            }
            else
            {
                _timeDetectedBoss = -1;
            }

            // 3. Nếu có người lạ -> Chờ 15s
            if (!shouldChangeZone && hasStranger)
            {
                if (now - _lastTimeChangeZone >= 15000) shouldChangeZone = true;
            }

            // 4. Định kỳ đổi khu sau 5 phút
            if (!shouldChangeZone && now - _lastTimeChangeZone >= 300000)
            {
                shouldChangeZone = true;
            }

            // Thực thi đổi khu
            if (shouldChangeZone)
            {
                PerformChangeZone(now);
            }
        }

        private void MoveToNextWaypoint(long currentTime)
        {
            try
            {

                if (TileMap.vGo == null || TileMap.vGo.size() == 0) return;

                // 2. Chọn Waypoint ngẫu nhiên
                int index = Res.random(0, TileMap.vGo.size());
                if (TileMap.vGo.size() > 1 && index == _lastWaypointIndex)
                {
                    index = (index + 1) % TileMap.vGo.size();
                }

                Waypoint wp = (Waypoint)TileMap.vGo.elementAt(index);
                if (wp != null && wp.name != "Nhà Bunma" && wp.name != "Võ đài Xên bọ hung")
                {
                    _lastWaypointIndex = index;

                    // 3. Lấy tọa độ chính giữa Waypoint (Không lùi 60px nữa)
                    int targetX = (wp.minX + wp.maxX) / 2;
                    int targetY = wp.maxY;

                    Logger.Info($"[AutoFarm] Hết quái! Nhảy sang map khác qua Waypoint ({targetX}, {targetY}).");

                    // -- LOGIC DỊCH CHUYỂN VÀ QUA MAP --

                    // Quay mặt
                    Char.myCharz().cdir = (Char.myCharz().cx - targetX <= 0) ? 1 : -1;

                    // Set tọa độ bay thẳng vào giữa Waypoint
                    Char.myCharz().cx = targetX;
                    Char.myCharz().cy = targetY;
                    Char.myCharz().currentMovePoint = null;

                    // Gửi tọa độ lên Server để server xác nhận mình đang đứng ở Waypoint
                    Service.gI().charMove();

                    // GỌI LỆNH CHUYỂN MAP
                    Service.gI().requestChangeMap();

                    // Khóa phím và bật cờ đang chuyển map (giống hệt logic gốc của game)
                    Char.isLockKey = true;
                    Char.ischangingMap = true;
                    GameCanvas.clearKeyHold();
                    GameCanvas.clearKeyPressed();

                    // Reset bộ đếm
                    _lastTimeChangeZone = currentTime;
                    _lastTimeRequestChange = currentTime;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"[AutoFarm] Lỗi Waypoint: {ex.Message}");
            }
        }

        private void PerformChangeZone(long currentTime)
        {
            try
            {
                string exeName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
                Random rand = new Random(exeName.GetHashCode() + (int)currentTime);

                // Giới hạn khu từ 0 đến 9
                int maxZone = 4;
                int nextZone = rand.Next(0, maxZone);

                if (nextZone == TileMap.zoneID) nextZone = (nextZone + 1) % maxZone;

                Service.gI().requestChangeZone(nextZone, -1);

                _lastTimeChangeZone = currentTime;
                _lastTimeRequestChange = currentTime;
                _timeDetectedBoss = -1;
            }
            catch { }
        }

        private bool IsBossPresent()
        {
            if (GameScr.vCharInMap == null) return false;
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char c = (Char)GameScr.vCharInMap.elementAt(i);
                if (c != null && !c.me && c.charID > 0 && c.cTypePk == 5) return true;
            }
            return false;
        }

        private bool IsStrangerPresent()
        {
            if (GameScr.vCharInMap == null) return false;
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char c = (Char)GameScr.vCharInMap.elementAt(i);
                if (c != null && !c.me && c.charID > 0 && !IsMyPet(c)) return true;
            }
            return false;
        }

        private bool IsNoMobsLeft()
        {
            // Đã xóa dòng Logger.Info ở đây để tránh giật lag game
            if (GameScr.vMob == null || GameScr.vMob.size() == 0) return true;
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob m = (Mob)GameScr.vMob.elementAt(i);
                if (m != null && m.hp > 0 && m.status != 0 && m.status != 1) return false;
            }
            return true;
        }

        private bool IsMyPet(Char c)
        {
            if (Char.myCharz() == null) return false;
            return c.charID < 0 || (c.cName != null && (c.cName.Contains("đệ tử") || c.cName.Contains(Char.myCharz().cName)));
        }

        public void LoadSavedState()
        {
            try
            {
                string farmState = Rms.loadRMSString(GetDynamicKey("auto_farm_state"));
                IsAutoFarming = (farmState == "1");
            }
            catch { }
        }
    }
}