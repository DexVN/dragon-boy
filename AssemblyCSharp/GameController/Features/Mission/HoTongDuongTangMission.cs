using AssemblyCSharp.GameController.Features.Navigation;
using System;
using System.Text.RegularExpressions;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public enum HoTongDuongTangState
    {
        GET_MISSION,
        WAIT_CHAR_DISPLAY,
        ESCORT,
        ESCORTING,
        COMPLETED
    }

    public enum GetMissionState
    {
        GO_TO_MISSION_MAP,
        GO_TO_MISSION_NPC,
        GET_MISSION_FROM_NPC,
        COMPLETED
    }

    public class HoTongDuongTangMission : IMission
    {
        public string Name => "Hộ tống đường tăng";

        public bool IsStart { get; private set; } = true;
        private long _lastTimeAction;
        private long _delayNextStep;
        private int targetMapID = -1;

        private int DUONG_TANG_ID = -100000063;
        private int NPC_DUONG_TANG_ID = 49;

        public HoTongDuongTangState _currentState = HoTongDuongTangState.GET_MISSION;
        public GetMissionState _currentGetMissionState = GetMissionState.GO_TO_MISSION_MAP;

        private void SetDelay(int min, int max)
        {
            _lastTimeAction = mSystem.currentTimeMillis();
            _delayNextStep = Res.random(min, max);
        }

        public void CheckMission()
        {
            Char me = Char.myCharz();
            if (me.statusMe == 14)
            {
                IsStart = false;
            }
            if (TileMap.mapID == targetMapID && _currentState == HoTongDuongTangState.COMPLETED)
            {
                _currentState = HoTongDuongTangState.GET_MISSION;
                _currentGetMissionState = GetMissionState.GO_TO_MISSION_MAP;
                targetMapID = -1;
            }
        }

        public void Execute()
        {
            if (mSystem.currentTimeMillis() - _lastTimeAction < _delayNextStep) return;
            switch (_currentState)
            {
                case HoTongDuongTangState.GET_MISSION:
                    GetMission();
                    break;
                case HoTongDuongTangState.ESCORTING:
                    OnEscortLogic();
                    break;
                case HoTongDuongTangState.COMPLETED:
                    break;
            }
        }

        public void OnReceiveMessage(sbyte cmd, string message)
        {
            Logger.Info($"onReceiveMessage: {message}; targetMapID: {targetMapID}; _currentState: {_currentState}");
            switch (cmd)
            {
                case 32:
                    string pattern = @"Số lượt còn lại\s*:\s*(\d+)/(\d+)";
                    Match match = Regex.Match(message, pattern);
                    if (match.Success)
                    {
                        int remainingTurns = int.Parse(match.Groups[1].Value);
                        int totalTurns = int.Parse(match.Groups[2].Value);
                        if (remainingTurns == 0) IsStart = false;
                    }
                    break;
                case -7:
                    if (targetMapID == -1 && _currentState == HoTongDuongTangState.ESCORT)
                    {
                        Char me = Char.myCharz();
                        if (!me.cName.Contains(message)) break;
                        targetMapID = Map.GetMapIdByName(message);
                        Logger.Info($"message: {message}");
                        Logger.Info($"TargetMapID: {targetMapID}");
                        _currentState = HoTongDuongTangState.ESCORTING;
                    }
                    break;
            }     
        }

        private void GetMission()
        {
            if (mSystem.currentTimeMillis() - _lastTimeAction < _delayNextStep) return;
            switch (_currentGetMissionState)
            {
                case GetMissionState.GO_TO_MISSION_MAP:
                    SetDelay(500, 1000);
                    if (TileMap.mapID == Map.LANG_ARU)
                    {
                        _currentGetMissionState = GetMissionState.GO_TO_MISSION_NPC;
                        CapsuleController.gI().Reset();
                        break;
                    }
                    if (CapsuleController.gI().CurrentState == CapsuleState.IDLE)
                    {
                        CapsuleController.gI().Start(Map.LANG_ARU, "Làng Aru");
                    }
                    if (CapsuleController.gI().IsError)
                    {
                        CapsuleController.gI().Reset(); IsStart = false;
                    }
                    break;
                case GetMissionState.GO_TO_MISSION_NPC:
                    if (NpcMenuController.gI().CurrentState != MenuState.IDLE) break;
                    Char me = Char.myCharz();
                    for (int i = 0; i < GameScr.vNpc.size(); i++)
                    {
                        if (GameScr.vNpc.elementAt(i) is Npc npc && npc.template.npcTemplateId == 49)
                        {
                            me.npcFocus = npc;
                            me.cx = npc.cx;
                            me.cy = npc.cy;
                            Service.gI().charMove();
                            _currentGetMissionState = GetMissionState.GET_MISSION_FROM_NPC;
                            break;
                        }
                    }
                    SetDelay(500, 1000);
                    break;
                case GetMissionState.GET_MISSION_FROM_NPC:
                    NpcMenuController.gI().Start(NPC_DUONG_TANG_ID, new int[] { 2, 0 });
                    _currentGetMissionState = GetMissionState.COMPLETED;
                    _currentState = HoTongDuongTangState.ESCORT;
                    SetDelay(1500, 2000);
                    break;
                case GetMissionState.COMPLETED:
                    break;
            }
        }

        private void OnEscortLogic()
        {
            MapNavigation.gI().StartPath(targetMapID);
            _currentState = HoTongDuongTangState.COMPLETED;
        }
    }
}
