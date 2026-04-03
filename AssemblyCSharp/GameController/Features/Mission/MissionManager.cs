using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public class MissionManager
    {
        private static MissionManager _instance;
        public static MissionManager gI() => _instance ?? (_instance = new MissionManager());

        public IMission CurrentMission { get; set; }

        public void OnReceiveMessage(string message)
        {
            if (CurrentMission != null && !CurrentMission.IsCompleted)
            {
                CurrentMission.OnReceiveMessage(message);
            }
        }

        public void Update()
        {
            if (CurrentMission != null && CurrentMission.IsCompleted)
            {
                CurrentMission.Execute();
            }
            else
            {
                Debug.Log("[MissionManager] Mission Completed!");
                CurrentMission = null;
            }
        }
        
    }
}
