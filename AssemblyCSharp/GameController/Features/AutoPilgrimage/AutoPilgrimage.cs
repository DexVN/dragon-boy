using AssemblyCSharp.GameController.Command;
using AssemblyCSharp.GameController.Features.Navigation;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoPilgrimage
{
    internal class AutoPilgrimage : ICommand
    {
        private static AutoPilgrimage _instance;
        public static AutoPilgrimage gI() => _instance ?? (_instance = new AutoPilgrimage());

        public bool IsEnabled { get; private set; }

        private const int TARGET_MAP_ID = Map.VACH_NUI_DEN;

        private const int NPC_DUONG_TANG_ID = 49;

        private const sbyte CONFIRM_MENU_ID = 2;

        private bool isMissionCompleted = false;

        private bool isSubMissionCompleted = false;

        private bool isUsingCapsule = false;

        private bool isRequestMapSelect = false;

        public void Execute(GameControllerCommand cmdObj)
        {
            gI().CheckMissionTurns("dd");
            gI().IsEnabled = cmdObj.value != 0f;
            Service.gI().charMove();
            if (gI().IsEnabled)
            {
                isMissionCompleted = false;
                isUsingCapsule = false;
                GameCanvas.panel.mapNames = null;
                MapNavigation.gI().StartPath(TARGET_MAP_ID);
                Debug.Log($"[AutoPilgrimage] Bật - Mục tiêu: {TARGET_MAP_ID}");
            }
            else
            {
                gI().isMissionCompleted = false;
                gI().isSubMissionCompleted = false;
                gI().isUsingCapsule = false;
                gI().isRequestMapSelect = false;

                MapNavigation.gI().StopNavigation();
                Debug.Log("[AutoPilgrimage] Tắt");
            }
        }

        public static void Update()
        {
            if (!gI().IsEnabled) return;

            if (gI().isMissionCompleted) return;

            if (gI().isSubMissionCompleted)
            {
                if (!gI().isUsingCapsule && TileMap.mapID != Map.LANG_ARU)
                {
                    Debug.Log($"[AutoPilgrimage] Request Map select");
                    GameCanvas.panel.mapNames = null;
                    Service.gI().useItem(0, 1, 39, -1);
                    gI().isUsingCapsule = true;
                }
                else
                {
                    if (gI().isRequestMapSelect || GameCanvas.panel.mapNames == null) return;

                    for (int i = 0; i < GameCanvas.panel.mapNames.Length; i++)
                    {
                        if (GameCanvas.panel.mapNames[i].Contains("Làng Aru"))
                        {
                            gI().isRequestMapSelect = true;
                            Debug.Log($"[AutoPilgrimage] Request Map select ARU at index {i}");
                            Service.gI().requestMapSelect(i);
                            GameCanvas.panel.vPlayerMenu.removeAllElements();
                            return;
                        }
                    }
                }
            }
            
            Debug.Log("[AutoPilgrimage] Đang di chuyển...");

            try
            {
                MapNavigation.gI().Update();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AutoPilgrimage] Lỗi trong Update: {ex.Message}");
            }
        }

        public void CheckMissionTurns(string message)
        {
            // Mẫu Regex: Tìm chuỗi "Số lượt còn lại : " theo sau là các chữ số, dấu "/", và các chữ số
            string pattern = @"Số lượt còn lại\s*:\s*(\d+)/(\d+)";

            Match match = Regex.Match(message, pattern);

            if (match.Success)
            {
                // Groups[1] là số đầu tiên (11), Groups[2] là số thứ hai (15)
                int luotConLai = int.Parse(match.Groups[1].Value);
                int tongSoLuot = int.Parse(match.Groups[2].Value);

                Debug.Log($"[AutoPilgrimage] Số lượt còn lại: {luotConLai} / {tongSoLuot}");

                // Code logic của bạn ở đây. Ví dụ:
                if (luotConLai > 0)
                {
                    gI().isMissionCompleted = false;
                }
                else
                {
                    // Đã hết lượt, dừng auto
                    gI().IsEnabled = false;
                }
            }
            else
            {
                Debug.Log("[AutoPilgrimage] Không tìm thấy thông tin số lượt trong tin nhắn.");
            }
        }
    }
}