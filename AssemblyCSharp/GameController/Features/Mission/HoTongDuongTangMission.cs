using AssemblyCSharp.GameController.Features.Navigation;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public enum HoTongDuongTangState
    {
        GET_MISSION_TO_NPC,
        WAIT_CHAR_DISPLAY,
        ENSCORT,
        ENSCORTING,
        RETURNING,
        COMPLETED
    }

    public enum GetMissionState
    {
        GO_TO_MISSION_MAP,
        GO_TO_MISSION_NPC,
        WAITING,
        COMPLETED
    }

    public class HoTongDuongTangMission : IMission
    {
        public string Name => "Hộ tống đường tăng";

        public bool IsStart { get; private set; } = true;
        private long _lastTimeAction;
        private long _delayNextStep;
        private int targetMapID;

        private int DUONG_TANG_ID = -100000063;
        private int NPC_DUONG_TANG_ID = 49;

        public HoTongDuongTangState _currentState = HoTongDuongTangState.GET_MISSION_TO_NPC;
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
                Debug.Log("[HoTongDuongTangMission] Nhân vật đã chết, dừng nhiệm vụ bò mộng!");
                IsStart = false;
            }
            if (TileMap.mapID != Map.LANG_ARU)
            {
                _currentGetMissionState = GetMissionState.GO_TO_MISSION_MAP;
            }
            if (TileMap.mapID == targetMapID && _currentState == HoTongDuongTangState.ENSCORTING)
            {
                _currentState = HoTongDuongTangState.GET_MISSION_TO_NPC;
            }
        }

        public void Execute()
        {
            switch (_currentState)
            {
                case HoTongDuongTangState.GET_MISSION_TO_NPC:
                    GetMission();
                    break;
                case HoTongDuongTangState.ENSCORT:
                    //if (CheckIfEscortStarted()) return;
                    OnEscortLogic();
                    break;
                case HoTongDuongTangState.RETURNING:
                    OnReturnToStart();
                    break;
                case HoTongDuongTangState.COMPLETED:
                    Logger.Info("Not start yet!");
                    break;
            }
        }

        public void OnReceiveMessage(sbyte cmd, string message)
        {
            Debug.Log($"[HoTongDuongTangMission] onReceiveMessage: {message}");
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
                    if (_currentState == HoTongDuongTangState.WAIT_CHAR_DISPLAY)
                    {
                        targetMapID = Map.GetMapIdByName(message);
                        Logger.Info("TARGET MAP ID" + targetMapID);
                        _currentState = HoTongDuongTangState.ENSCORT;
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
                    if (TileMap.mapID == Map.LANG_ARU)
                    {
                        _currentGetMissionState = GetMissionState.GO_TO_MISSION_NPC;
                        CapsuleController.gI().Reset();
                        SetDelay(500, 1000);
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
                            SetDelay(500, 1000);
                            NpcMenuController.gI().Start(NPC_DUONG_TANG_ID, new int[] { 2, 0 });
                            _currentState = HoTongDuongTangState.WAIT_CHAR_DISPLAY;
                            break;
                        }
                    }
                    break;
                case GetMissionState.WAITING:
                    break;
            }
        }

        private void OnEscortLogic()
        {
            MapNavigation.gI().StartPath(targetMapID);
            Logger.Info("OnEscortLogic");
            _currentState = HoTongDuongTangState.ENSCORTING;
        }

        private void OnReturnToStart()
        {
            Logger.Info("Returning...");
        }

        private bool CheckIfEscortStarted()
        {
            return false;
        }

    }
}
