using AssemblyCSharp.GameController.Features.Navigation;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public enum HoTongDuongTangState
    {
        MOVING_TO_NPC,
        ENSCORTING,
        RETURNING,
        COMPLETED_ALL
    }

    public class HoTongDuongTangMission : IMission
    {

        public string Name => "Hộ tống đường tăng";

        public bool IsChangeMap { get; set; } = false;

        public bool IsCompleted { get; private set; } = false;

        public void CheckEnviroment()
        {
            Char me = Char.myCharz();
            if (me.statusMe == 14)
            {
                Debug.Log("[HoTongDuongTangMission] Nhân vật đã chết, dừng nhiệm vụ bò mộng!");
                IsCompleted = true;
            }
            if (TileMap.mapID != Map.LANG_ARU)
            {
                IsChangeMap = true;
            }
        }

        public void Execute()
        {
            Debug.Log("[HoTongDuongTangMission] Đang hộ tống....");
        }

        public void OnReceiveMessage(string message)
        {
            Debug.Log($"[HoTongDuongTangMission] onReceiveMessage: {message}");
            string pattern = @"Số lượt còn lại\s*:\s*(\d+)/(\d+)";

            Match match = Regex.Match(message, pattern);
            
            if (match.Success)
            {
                int remainingTurns = int.Parse(match.Groups[1].Value);
                int totalTurns = int.Parse(match.Groups[2].Value);
                if (remainingTurns > 0) IsCompleted = false;
                else IsCompleted = true;
            }
        }


    }
}
