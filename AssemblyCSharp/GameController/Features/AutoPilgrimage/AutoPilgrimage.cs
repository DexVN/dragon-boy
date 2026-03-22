
using AssemblyCSharp.GameController.Command;
using System;
using UnityEngine;

/*
 * Kiểm tra feature auto pilgrimage có bật hay không
 * Nếu không -> return
 * Nếu có -> 
 *  - Kiểm tra nhân vật có đang ở làng Aru hay không
 *      -> Nếu không -> dùng capsule về chỗ NPC hành hương
 *      -> Nếu có -> Kiểm tra xem có đang ở gần NPC hành hương hay không
 *          -> Nếu không -> di chuyển đến gần NPC hành hương
 
 *              - Click vào NPC hành hương để lấy thông tin hành hương
 *              - Kiểm tra số lần hành hương còn lại
 *                  -> Nếu không còn -> kết thúc hành hương -> thông báo đã hoàn thành hành hương
 *                  -> Nếu còn -> Nhận nhiệm vụ hành hương
 *          -> Nếu có -> tương tác với NPC để bắt đầu hành hương
 * 
 * Hành hương
 * - Kiểm tra vị trí của nhân vật có đang ở gần Char hành hương hay không
 *      -> Nếu không -> di chuyển đến gần Char hành hương
 *      -> Nếu có -> di chuyển đến vị trí tiếp theo cách Char hành hương một khoảng ngắn
 *          -> Nếu đã đến vị trí cuối cùng -> kết thúc hành hương, capsule về lại vị trí NPC hành hương
 *          
 * 
 */
namespace AssemblyCSharp.GameController.Features.AutoPilgrimage
{
    internal class AutoPilgrimage : ICommand
    {
        private static AutoPilgrimage instance;

        public static bool IsAutoPilgrimage { get; private set; } = false;

        public static int lastMapID = -1;


        private static long lastAttemptMs = 0;

        public static long lastTimeMove; // Thời gian lần cuối di chuyển

        public static int targetX = 0;   // Vị trí X cần đến

        public static int targetY = 0; // Thêm targetY để Teleport chính xác độ cao của cổng

        public static bool isWaiting = false; // Trạng thái đang chờ 2 giây

        public static bool isGoingToVGO = false;

        public static AutoPilgrimage gI()
        {
            if (instance == null)
                instance = new AutoPilgrimage();
            return instance;
        }

        public static void Update()
        {
            if (!IsAutoPilgrimage) return;

            MainThreadDispatcher.Enqueue(() =>
            {
                MoveToPilgrimageMap();
            });
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    IsAutoPilgrimage = cmdObj.value != 0f;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to toggle auto login: {ex.Message}");
                }
            });
        }

        public static void MoveToPilgrimageMap()
        {
            if (Char.isLoadingMap || GameCanvas.isLoading) return;

            Char me = Char.myCharz();
            if (me == null) return;

            // 1. RESET KHI ĐỔI MAP
            if (TileMap.mapID != lastMapID)
            {
                lastMapID = TileMap.mapID;
                targetX = 0;
                isWaiting = false;
                isGoingToVGO = false;
                GameCanvas.clearKeyHold();
                return;
            }

            long currentTime = mSystem.currentTimeMillis();

            // 2. NGHỈ 2 GIÂY
            if (isWaiting && !isGoingToVGO)
            {
                if (currentTime - lastTimeMove >= 2000) { isWaiting = false; targetX = 0; }
                else { GameCanvas.clearKeyHold(); return; }
            }

            // 3. TÌM CỔNG (WayPoint)
            Waypoint foundWP = null;
            if (targetX == 0)
            {
                if (TileMap.vGo != null)
                {
                    for (int i = 0; i < TileMap.vGo.size(); i++)
                    {
                        Waypoint wp = (Waypoint)TileMap.vGo.elementAt(i);
                        if (wp.minX > me.cx && wp.minX < me.cx + 100)
                        {
                            foundWP = wp;
                            break;
                        }
                    }
                }

                if (foundWP != null)
                {
                    targetX = foundWP.minX + (foundWP.maxX - foundWP.minX) / 2;
                    targetY = foundWP.maxY;
                    isGoingToVGO = true;
                }
                else
                {
                    targetX = me.cx + 25;
                    isGoingToVGO = false;
                }
            }

            // 4. DI CHUYỂN VÀ XỬ LÝ VÀO CỔNG
            int distance = Math.abs(me.cx - targetX);

            if (me.cx < targetX)
            {
                // --- KHI LẠI GẦN CỔNG (Dưới 30px) ---
                if (isGoingToVGO && distance < 30)
                {
                    // A. Dịch chuyển tức thời vào giữa cổng
                    me.cx = targetX;
                    me.cy = targetY;

                    // B. Gửi vị trí mới lên Server để "xác nhận" đang đứng ở cổng
                    Service.gI().charMove();

                    // C. MÔ PHỎNG NHẤN PHÍM CHỌN (Index 12/25)
                    GameCanvas.clearKeyHold();
                    GameCanvas.keyPressed[12] = true;
                    GameCanvas.keyPressed[25] = true;

                    // D. ÉP GỬI LỆNH LÊN SERVER (Quan trọng nhất)
                    // Trong nhiều bản mod, lệnh này sẽ bắt Server cho qua map ngay
                    

                    targetX = 0;
                }
                else
                {
                    // Chạy bình thường
                    GameCanvas.keyHold[24] = true;
                    me.cdir = 1;

                    // Nhảy khi vướng
                    int sensorX = me.cx + 15;
                    if ((TileMap.tileTypeAtPixel(sensorX, me.cy - 12) & TileMap.T_LEFT) != 0) GameCanvas.keyHold[21] = true;
                    else GameCanvas.keyHold[21] = false;

                    if (GameCanvas.gameTick % 5 == 0) Service.gI().charMove();
                }
            }
            else
            {
                // Đi đủ 100px thì nghỉ
                GameCanvas.clearKeyHold();
                isWaiting = true;
                lastTimeMove = currentTime;
                targetX = 0;
            }
        }
    }
}
