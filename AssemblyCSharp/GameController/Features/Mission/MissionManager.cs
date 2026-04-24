using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public class MissionManager
    {
        private static MissionManager _instance;
        public static MissionManager gI() => _instance ?? (_instance = new MissionManager());

        private IMission _currentMission;
        private bool _isLoadedFromRMS = false;

        public IMission CurrentMission
        {
            get => _currentMission;
            set
            {
                _currentMission = value;
                if(_currentMission != null)
                {
                    Debug.Log($"[MissionManager] Started mission: {_currentMission.Name}");
                }
                else
                {
                    Debug.Log("[MissionManager] Stopped all missions.");
                }
            }
        }

        public void OnReceiveMessage(sbyte cmd, string message = "")
        {
            if (CurrentMission != null)
            {
                CurrentMission.OnReceiveMessage(cmd, message);
            }
        }

        public void Update()
        {
            if (CurrentMission != null)
            {
                CurrentMission.CheckMission();
                if (!CurrentMission.IsStart) return;
                CurrentMission.Execute();
            }
            else
            {
                Debug.Log("[MissionManager] Mission Completed!");
                CurrentMission = null;
            }
        }

        public void LoadSavedConfig()
        {
            if (!_isLoadedFromRMS)
            {
                if (Rms.loadRMSString("auto_hotong") == "1")
                {
                    CurrentMission = new HoTongDuongTangMission();
                }
                _isLoadedFromRMS = true;
            }
        }
    }
}
