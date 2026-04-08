using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Navigation
{
    public class MapNavigation
    {
        private const int WAIT_DURATION = 5000;
        private const int MOVE_DURATION = 200;
        private const int MOVE_LEFT_KEY = 23;  
        private const int MOVE_RIGHT_KEY = 24; 
        private const int SENSOR_OFFSET = 20;
        private const int WAYPOINT_THRESHOLD = 50;
        private const int ACTION_DELAY_MS = 2000;


        private static MapNavigation _instance;
        public static MapNavigation gI() => _instance ?? (_instance = new MapNavigation());
        private MapDatabase _db = new MapDatabase();
        private PathFinder _finder;
        private List<MapExit> _currentPath;

        public bool IsRunning { get; private set; }
        private int _stepIndex;
        private long _lastTimeWait;
        private long _startMoveTime;
        private bool _isWaiting;
        private long _lastActionTime;

        public MapNavigation()
        {
            _finder = new PathFinder(_db);
            LoadData();
        }

        private void LoadData()
        {
            // ==========================================================
            // --- TRÁI ĐẤT ---
            // ==========================================================
            // Vách núi Aru <-> Làng Aru <-> Đồi hoa cúc <-> Thung lũng tre <-> Rừng nấm <-> Rừng xương
            _db.AddEdge(Map.LANG_ARU, Map.VACH_NUI_ARU, MoveType.Left);
            _db.AddEdge(Map.VACH_NUI_ARU, Map.LANG_ARU, MoveType.Right);

            _db.AddEdge(Map.LANG_ARU, Map.DOI_HOA_CUC, MoveType.Right);
            _db.AddEdge(Map.DOI_HOA_CUC, Map.LANG_ARU, MoveType.Left);

            _db.AddEdge(Map.DOI_HOA_CUC, Map.THUNG_LUNG_TRE, MoveType.Right);
            _db.AddEdge(Map.THUNG_LUNG_TRE, Map.DOI_HOA_CUC, MoveType.Left);

            _db.AddEdge(Map.THUNG_LUNG_TRE, Map.RUNG_NAM, MoveType.Right);
            _db.AddEdge(Map.RUNG_NAM, Map.THUNG_LUNG_TRE, MoveType.Left);

            _db.AddEdge(Map.RUNG_NAM, Map.RUNG_XUONG, MoveType.Right);
            _db.AddEdge(Map.RUNG_XUONG, Map.RUNG_NAM, MoveType.Left);

            _db.AddEdge(Map.RUNG_XUONG, Map.DAO_KAME, MoveType.Right);
            _db.AddEdge(Map.DAO_KAME, Map.RUNG_XUONG, MoveType.Left);

            _db.AddEdge(Map.DAO_KAME, Map.DONG_KARIN, MoveType.Right);
            _db.AddEdge(Map.DONG_KARIN, Map.DAO_KAME, MoveType.Left);

            //Rừng nấm <-> Rừng Bamboo <-> Rừng dương xỉ <-> Nam Kamê <-> Đảo Bulông
            _db.AddEdge(Map.RUNG_NAM, Map.RUNG_BAMBOO, MoveType.Waypoint, 1380, 324);
            _db.AddEdge(Map.RUNG_BAMBOO, Map.RUNG_NAM, MoveType.Left);

            _db.AddEdge(Map.RUNG_BAMBOO, Map.RUNG_DUONG_XI, MoveType.Right);
            _db.AddEdge(Map.RUNG_DUONG_XI, Map.RUNG_BAMBOO, MoveType.Left);

            _db.AddEdge(Map.RUNG_DUONG_XI, Map.NAM_KAME, MoveType.Right);
            _db.AddEdge(Map.NAM_KAME, Map.RUNG_DUONG_XI, MoveType.Left);

            _db.AddEdge(Map.NAM_KAME, Map.DAO_BULONG, MoveType.Right);
            _db.AddEdge(Map.DAO_BULONG, Map.NAM_KAME, MoveType.Left);

            // Đảo kamê <-> Nam Kamê
            _db.AddEdge(Map.DAO_KAME, Map.NAM_KAME, MoveType.Waypoint, 828, 348);
            _db.AddEdge(Map.NAM_KAME, Map.DAO_KAME, MoveType.Waypoint, 732, 348);


            // ==========================================================
            // --- NAMẾC ---
            // ==========================================================
            // Vách núi Moori <-> Làng Mori <-> Đồi nấm tím <-> Thị trấn Moori <-> Thung lũng Maima <-> Vực Maima <-> Đảo Guru <-> Thung lũng Namếc
            _db.AddEdge(Map.LANG_MORI, Map.VACH_NUI_MOORI, MoveType.Left);
            _db.AddEdge(Map.VACH_NUI_MOORI, Map.LANG_MORI, MoveType.Right);

            _db.AddEdge(Map.LANG_MORI, Map.DOI_NAM_TIM, MoveType.Right);
            _db.AddEdge(Map.DOI_NAM_TIM, Map.LANG_MORI, MoveType.Left);

            _db.AddEdge(Map.DOI_NAM_TIM, Map.THI_TRAN_MOORI, MoveType.Right);
            _db.AddEdge(Map.THI_TRAN_MOORI, Map.DOI_NAM_TIM, MoveType.Left);

            _db.AddEdge(Map.THI_TRAN_MOORI, Map.THUNG_LUNG_MAIMA, MoveType.Right);
            _db.AddEdge(Map.THUNG_LUNG_MAIMA, Map.THI_TRAN_MOORI, MoveType.Left);

            _db.AddEdge(Map.THUNG_LUNG_MAIMA, Map.VUC_MAIMA, MoveType.Right);
            _db.AddEdge(Map.VUC_MAIMA, Map.THUNG_LUNG_MAIMA, MoveType.Left);

            _db.AddEdge(Map.VUC_MAIMA, Map.DAO_GURU, MoveType.Right);
            _db.AddEdge(Map.DAO_GURU, Map.VUC_MAIMA, MoveType.Left);

            _db.AddEdge(Map.DAO_GURU, Map.THUNG_LUNG_NAMEC, MoveType.Right);
            _db.AddEdge(Map.THUNG_LUNG_NAMEC, Map.DAO_GURU, MoveType.Left);

            // Thung lũng Maima <-> Núi hoa vàng <-> Núi hoa tím <-> Nam Guru <-> Đông Nam Guru
            _db.AddEdge(Map.THUNG_LUNG_MAIMA, Map.NUI_HOA_VANG, MoveType.Waypoint, 1284, 372);
            _db.AddEdge(Map.NUI_HOA_VANG, Map.THUNG_LUNG_MAIMA, MoveType.Left);

            _db.AddEdge(Map.NUI_HOA_VANG, Map.NUI_HOA_TIM, MoveType.Right);
            _db.AddEdge(Map.NUI_HOA_TIM, Map.NUI_HOA_VANG, MoveType.Left);

            _db.AddEdge(Map.NUI_HOA_TIM, Map.NAM_GURU, MoveType.Right);
            _db.AddEdge(Map.NAM_GURU, Map.NUI_HOA_TIM, MoveType.Left);

            _db.AddEdge(Map.NAM_GURU, Map.DONG_NAM_GURU, MoveType.Right);
            _db.AddEdge(Map.DONG_NAM_GURU, Map.NAM_GURU, MoveType.Left);

            // Đảo Guru <-> Nam Guru
            _db.AddEdge(Map.DAO_GURU, Map.NAM_GURU, MoveType.Waypoint, 492, 372);
            _db.AddEdge(Map.NAM_GURU, Map.DAO_GURU, MoveType.Waypoint, 660, 348);


            // ==========================================================
            // --- XAYDA ---
            // ==========================================================
            // Vách núi Kakarot <-> Làng Kakarot <-> Đồi hoang <-> Làng Plant <-> Rừng nguyên sinh <-> Rừng thông Xayda <-> Vách núi đen <-> Thành phố Vegeta
            _db.AddEdge(Map.LANG_KAKAROT, Map.VACH_NUI_KAKAROT, MoveType.Left);
            _db.AddEdge(Map.VACH_NUI_KAKAROT, Map.LANG_KAKAROT, MoveType.Right);

            _db.AddEdge(Map.LANG_KAKAROT, Map.DOI_HOANG, MoveType.Right);
            _db.AddEdge(Map.DOI_HOANG, Map.LANG_KAKAROT, MoveType.Left);

            _db.AddEdge(Map.DOI_HOANG, Map.LANG_PLANT, MoveType.Right);
            _db.AddEdge(Map.LANG_PLANT, Map.DOI_HOANG, MoveType.Left);

            _db.AddEdge(Map.LANG_PLANT, Map.RUNG_NGUYEN_SINH, MoveType.Right);
            _db.AddEdge(Map.RUNG_NGUYEN_SINH, Map.LANG_PLANT, MoveType.Left);

            _db.AddEdge(Map.RUNG_NGUYEN_SINH, Map.RUNG_THONG_XAYDA, MoveType.Right);
            _db.AddEdge(Map.RUNG_THONG_XAYDA, Map.RUNG_NGUYEN_SINH, MoveType.Left);

            _db.AddEdge(Map.RUNG_THONG_XAYDA, Map.VACH_NUI_DEN, MoveType.Right);
            _db.AddEdge(Map.VACH_NUI_DEN, Map.RUNG_THONG_XAYDA, MoveType.Left);
            
            _db.AddEdge(Map.VACH_NUI_DEN, Map.THANH_PHO_VEGETA, MoveType.Right);
            _db.AddEdge(Map.THANH_PHO_VEGETA, Map.VACH_NUI_DEN, MoveType.Left);

            // Rừng nguyên sinh <-> Rừng cọ <-> Rừng đá <-> Thung lũng đen <-> Bờ vực đen
            _db.AddEdge(Map.RUNG_NGUYEN_SINH, Map.RUNG_CO, MoveType.Waypoint, 1356, 300);
            _db.AddEdge(Map.RUNG_CO, Map.RUNG_NGUYEN_SINH, MoveType.Left);

            _db.AddEdge(Map.RUNG_CO, Map.RUNG_DA, MoveType.Right);
            _db.AddEdge(Map.RUNG_DA, Map.RUNG_CO, MoveType.Left);

            _db.AddEdge(Map.RUNG_DA, Map.THUNG_LUNG_DEN, MoveType.Right);
            _db.AddEdge(Map.THUNG_LUNG_DEN, Map.RUNG_DA, MoveType.Left);

            _db.AddEdge(Map.THUNG_LUNG_DEN, Map.BO_VUC_DEN, MoveType.Right);
            _db.AddEdge(Map.BO_VUC_DEN, Map.THUNG_LUNG_DEN, MoveType.Left);

            // Vách núi đen <-> Thung lũng đen
            _db.AddEdge(Map.VACH_NUI_DEN, Map.THUNG_LUNG_DEN, MoveType.Waypoint, 1236, 348);
            _db.AddEdge(Map.THUNG_LUNG_DEN, Map.VACH_NUI_DEN, MoveType.Waypoint, 732, 372);

            // ==========================================================
            // --- TRẠM TÀU VŨ TRỤ ---
            // ==========================================================
            // Trạm tàu vũ trụ trái đất <-> Thung lũng tre
            _db.AddEdge(Map.THUNG_LUNG_TRE, Map.TRAM_TAU_VU_TRU_TRAI_DAT, MoveType.Waypoint, 588, 372);
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_TRAI_DAT, Map.THUNG_LUNG_TRE, MoveType.Waypoint, 228, 324);

            // Trạm tàu vũ trụ Namec <-> Thị trấn Moori
            _db.AddEdge(Map.THI_TRAN_MOORI, Map.TRAM_TAU_VU_TRU_NAMEC, MoveType.Waypoint, 732, 444);
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_NAMEC, Map.THI_TRAN_MOORI, MoveType.Waypoint, 228, 324);

            // Trạm tàu vũ trụ Xayda <-> Làng Plant
            _db.AddEdge(Map.LANG_PLANT, Map.TRAM_TAU_VU_TRU_XAYDA, MoveType.Waypoint, 228, 276);
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_XAYDA, Map.LANG_PLANT, MoveType.Waypoint, 396, 324);

            // ==========================================================
            // --- LIÊN KẾT GIỮA CÁC TRẠM TÀU
            // ==========================================================
            // Trạm tàu vũ trụ trái đất <-> Trạm tàu vũ trụ Namec <-> Trạm tàu vũ trụ Xayda
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_TRAI_DAT, Map.TRAM_TAU_VU_TRU_NAMEC, MoveType.SpaceShip, npcTemplateId: 10, menuSelectedItem: 0);
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_TRAI_DAT, Map.TRAM_TAU_VU_TRU_XAYDA, MoveType.SpaceShip, npcTemplateId: 10, menuSelectedItem: 1);

            // Trạm tàu vũ trụ Namec <-> Trạm tàu vũ trụ trái đất <-> Trạm tàu vũ trụ Xayda
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_NAMEC, Map.TRAM_TAU_VU_TRU_TRAI_DAT, MoveType.SpaceShip, npcTemplateId: 11, menuSelectedItem: 0);
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_NAMEC, Map.TRAM_TAU_VU_TRU_XAYDA, MoveType.SpaceShip, npcTemplateId: 11, menuSelectedItem: 1);

            // Trạm tàu vũ trụ Xayda <-> Trạm tàu vũ trụ trái đất <-> Trạm tàu vũ trụ Namec
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_XAYDA, Map.TRAM_TAU_VU_TRU_TRAI_DAT, MoveType.SpaceShip, npcTemplateId: 12, menuSelectedItem: 0);
            _db.AddEdge(Map.TRAM_TAU_VU_TRU_XAYDA, Map.TRAM_TAU_VU_TRU_NAMEC, MoveType.SpaceShip, npcTemplateId: 12, menuSelectedItem: 1);
        }

        public void StartPath(int targetMapId)
        {
            Debug.Log($"[AutoPath] Bắt đầu tìm đường từ Map {TileMap.mapID} đến Map {targetMapId}...");
            _currentPath = _finder.GetPath(TileMap.mapID, targetMapId);
            if (_currentPath != null)
            {
                string steps = "";
                foreach (var s in _currentPath) steps += s.DestMapID + " (" + s.Type + ") -> ";
                Debug.Log("[AutoPath] Lộ trình tối ưu: " + TileMap.mapID + " -> " + steps + "DONE");
            }
            _stepIndex = 0;
            _isWaiting = false;
            _startMoveTime = mSystem.currentTimeMillis();
            IsRunning = (_currentPath != null);
        }

        public void Update()
        {
            if (!IsRunning || _currentPath == null)
            {
                Debug.Log($"[AutoPath] Không có lộ trình | IsRunning : {IsRunning} | _currentPath : {_currentPath != null}");
                return;
            }

            if (_stepIndex >= _currentPath.Count)
            {
                GameCanvas.clearKeyHold();
                StopNavigation();
                return;
            }

            if (Char.isLoadingMap || GameCanvas.isLoading)
            { 
                Debug.Log("[AutoPath] Đang tải map, tạm dừng di chuyển...");
                GameCanvas.clearKeyHold();
                return;
            }

            long now = mSystem.currentTimeMillis();
            float tScale = Time.timeScale < 0.1f ? 1f : Time.timeScale;
            MapExit currentStep = _currentPath[_stepIndex];

            if (TileMap.mapID == currentStep.DestMapID)
            {
                _stepIndex++;

                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();

                Char.myCharz().cdir = (currentStep.Type == MoveType.Left) ? 1 : -1;

                if (_stepIndex >= _currentPath.Count)
                {
                    StopNavigation();
                    return;
                }
                _isWaiting = true;
                _lastTimeWait = now;
                GameCanvas.clearKeyHold();
                return;
            }

            if (_isWaiting)
            {
                if (now - _lastTimeWait >= WAIT_DURATION / tScale)
                {
                    _isWaiting = false;
                    _startMoveTime = now;
                }
                else
                {
                    GameCanvas.clearKeyHold();
                    return;
                }
            }

            float adjustedMoveDuration = MOVE_DURATION / tScale;
            if (now - _startMoveTime < adjustedMoveDuration)
            {
                ExecuteMovement(currentStep);
            }
            else
            {
                GameCanvas.clearKeyHold();
                _isWaiting = true;
                _lastTimeWait = now;
            }
        }

        public void StopNavigation()
        {
            IsRunning = false;
            _currentPath = null;
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
        }

        private void ExecuteMovement(MapExit step)
        {
            Char me = Char.myCharz();
            if (me == null) return;

            if (step.Type == MoveType.Waypoint && step.TargetX != -1)
            {
                int diffX = me.cx - step.TargetX;

                if (Math.abs(diffX) > WAYPOINT_THRESHOLD)
                {
                    if (diffX > 0) LeftMovement(me);
                    else RightMovement(me);
                }
                else
                {
                    Debug.Log($"[AutoPath] Arrived at waypoint target X: {step.TargetX}, Current X: {me.cx}. Executing map change.");
                    GameCanvas.clearKeyHold();
                    InteractWithWaypoints(me);
                }
            }
            else
            {
                switch (step.Type)
                {
                    case MoveType.Right:
                        RightMovement(me);
                        break;
                    case MoveType.Left:
                        LeftMovement(me);
                        break;
                    case MoveType.SpaceShip:
                        Debug.Log($"[AutoPath] Initiating SpaceShip movement: NPC ID {step.NpcTemplateId}, Menu Item {step.MenuSelectedItem}");
                        long now = mSystem.currentTimeMillis();
                        if (now - _lastActionTime > ACTION_DELAY_MS)
                        {
                            Debug.Log($"[AutoPath] Initiating SpaceShip movement: NPC ID {step.NpcTemplateId}, Menu Item {step.MenuSelectedItem}");
                            for (int i = 0; i < GameScr.vNpc.size(); i++)
                            {
                                if (GameScr.vNpc.elementAt(i) is Npc npc && npc.template.npcTemplateId == step.NpcTemplateId)
                                {
                                    me.npcFocus = npc;
                                    me.cx = npc.cx;
                                    me.cy = npc.cy;
                                    Service.gI().charMove();
                                    break;
                                }
                            }
                            Service.gI().openMenu(step.NpcTemplateId);
                            Service.gI().confirmMenu(step.NpcTemplateId, step.MenuSelectedItem);
                            _lastActionTime = now;
                            _isWaiting = true;
                            _lastTimeWait = now;
                            GameCanvas.clearKeyHold();
                            GameCanvas.menu.showMenu = false;
                        }
                        break;
                    case MoveType.Capsule:
                        Debug.LogWarning("[AutoPath] Capsule movement not implemented yet.");
                        break;
                }
            }
        }
        private void LeftMovement(Char me)
        {
            me.cdir = -1;
            HandleTeleportMovement(me, -1);
        }

        private void RightMovement(Char me)
        {
            me.cdir = 1;
            HandleTeleportMovement(me, 1);
        }

        private void HandleTeleportMovement(Char me, int direction)
        {
            int sensorX = me.cx + 10;
            int targetY = -1;

            for (int yOff = 0; yOff < 500; yOff += 24)
            {
                int tileType = TileMap.tileTypeAtPixel(sensorX, me.cy - yOff);
                if (tileType == TileMap.T_EMPTY)
                {
                    int tileBelow = TileMap.tileTypeAtPixel(sensorX, me.cy - yOff + 24);
                    if (tileBelow != TileMap.T_EMPTY)
                    {
                        targetY = me.cy - yOff;
                        break;
                    }
                }
            }

            if (targetY != -1 && targetY < me.cy - 10 && !IsNearAnyWaypoint(Char.myCharz()))
            {
                me.cx = sensorX;
                me.cy = targetY;
                Service.gI().charMove();
                GameCanvas.clearKeyHold();
            }
            else
            {
                if (direction == -1) GameCanvas.keyHold[MOVE_LEFT_KEY] = true;
                else GameCanvas.keyHold[MOVE_RIGHT_KEY] = true;
            }
        }

        private void InteractWithWaypoints(Char me)
        {
            for (int i = 0; i < TileMap.vGo.size(); i++)
            {
                if (!(TileMap.vGo.elementAt(i) is Waypoint wp)) continue;
                if (me.cx + WAYPOINT_THRESHOLD >= wp.minX && me.cx <= wp.maxX)
                {
                    ExecuteMapChange(me, wp);
                    _isWaiting = true;
                    _lastTimeWait = mSystem.currentTimeMillis();
                    return;
                }
                else if (me.cx >= wp.minX && me.cx <= wp.maxX + WAYPOINT_THRESHOLD)
                {
                    ExecuteMapChange(me, wp);
                    _isWaiting = true;
                    _lastTimeWait = mSystem.currentTimeMillis();
                    return;
                }
            }
        }

        private void ExecuteMapChange(Char me, Waypoint wp)
        {
            me.cx = (wp.minX + wp.maxX) / 2;
            me.cy = wp.maxY;
            Service.gI().charMove();
            Service.gI().requestChangeMap();
            GameCanvas.clearKeyHold();
        }

        private bool IsNearAnyWaypoint(Char me)
        {
            int direction = me.cdir;
            int sensorX = me.cx;
            bool isWallTooHigh = true;
            for (int yOff = 0; yOff < 550; yOff += 24)
            {
                if (TileMap.tileTypeAtPixel(sensorX, me.cy - yOff) == TileMap.T_EMPTY)
                {
                    isWallTooHigh = false;
                    break;
                }
            }
            if (isWallTooHigh) return true;
            return false;
        }
    }
}