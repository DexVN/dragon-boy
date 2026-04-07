using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.GameController.Features
{
    public enum CapsuleState
    {
        IDLE,
        USE_ITEM,
        WAIT_API,
        SELECT_MAP,
        WAIT_MAP_LOAD,
        DONE,
        ERROR_NO_ITEM
    }

    public class CapsuleController
    {
        private static CapsuleController _instance;
        public static CapsuleController gI() => _instance ?? (_instance = new CapsuleController());

        public CapsuleState CurrentState { get; private set; } = CapsuleState.IDLE;

        public bool IsError => CurrentState == CapsuleState.ERROR_NO_ITEM;

        private int _targetMapId;
        private string _targetMapName;

        public void Start(int mapId, string mapName)
        {
            Logger.Info($"Map ID: {mapId}; MapName: {mapName}");
            _targetMapId = mapId;
            _targetMapName = mapName;
            CurrentState = CapsuleState.USE_ITEM;
        }

        public void Reset()
        {
            CurrentState = CapsuleState.IDLE;
        }

        public void Update()
        {
            switch (CurrentState)
            {
                case CapsuleState.USE_ITEM:
                    Char me = Char.myCharz();
                    bool foundItem = false;

                    for (int i = 0; i < me.arrItemBag.Length; i++)
                    {
                        if (me.arrItemBag[i] != null && me.arrItemBag[i].template.id == ItemID.CAPSULE_VIP)
                        {
                            GameCanvas.panel.mapNames = null;
                            Service.gI().useItem(0, 1, (sbyte)i, -1);

                            CurrentState = CapsuleState.WAIT_API;
                            foundItem = true;
                            break;
                        }
                    }

                    if (!foundItem)
                    {
                        CurrentState = CapsuleState.ERROR_NO_ITEM;
                        Logger.Error("Không tìm thấy Capsule VIP trong hành trang!");
                    }
                    break;

                case CapsuleState.WAIT_API:
                    if (GameCanvas.panel.mapNames != null && GameCanvas.panel.mapNames.Length > 0)
                    {
                        CurrentState = CapsuleState.SELECT_MAP;
                    }
                    break;

                case CapsuleState.SELECT_MAP:
                    for (int i = 0; i < GameCanvas.panel.mapNames.Length; i++)
                    {
                        if (GameCanvas.panel.mapNames[i] != null && GameCanvas.panel.mapNames[i].Contains(_targetMapName))
                        {
                            Service.gI().requestMapSelect(i);
                            GameCanvas.panel.vPlayerMenu.removeAllElements();
                            GameCanvas.panel.mapNames = null;
                            CurrentState = CapsuleState.WAIT_MAP_LOAD;
                            break;
                        }
                    }
                    break;
                case CapsuleState.WAIT_MAP_LOAD:
                    break;
                case CapsuleState.DONE:
                    break;
            }
        }
    }
}
