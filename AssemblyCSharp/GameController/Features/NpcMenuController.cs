using System;
using System.Collections.Generic;

public enum MenuState
{
    IDLE,
    OPENING_NPC,
    WAIT_MENU_SHOW,
    SELECTING,
    DONE
}

public class NpcMenuController
{
    private static NpcMenuController _instance;
    public static NpcMenuController gI() => _instance ?? (_instance = new NpcMenuController());

    public MenuState CurrentState = MenuState.IDLE;

    private Queue<int> _menuQueue = new Queue<int>();

    private int _npcId;
    private long _lastTimeAction;
    private long _delayNextStep;

    public void Start(int npcId, int[] selectIndices)
    {
        this._npcId = npcId;
        this._menuQueue.Clear();
        foreach (int index in selectIndices)
        {
            _menuQueue.Enqueue(index);
        }
        this.CurrentState = MenuState.OPENING_NPC;
        SetDelay(1000, 1000);
    }

    private void SetDelay(int min, int max)
    {
        _lastTimeAction = mSystem.currentTimeMillis();
        _delayNextStep = Res.random(min, max);
    }

    public void Update()
    {
        if (CurrentState == MenuState.IDLE) return;
        if (mSystem.currentTimeMillis() - _lastTimeAction < _delayNextStep)
        {
            return;
        }
        Logger.Info("State: " + CurrentState);
        switch (CurrentState)
        {
            case MenuState.OPENING_NPC:
                Service.gI().openMenu(_npcId);
                SetDelay(200, 300);
                CurrentState = MenuState.WAIT_MENU_SHOW;
                break;

            case MenuState.WAIT_MENU_SHOW:
                if (GameCanvas.menu.showMenu)
                {
                    SetDelay(200, 300);
                    CurrentState = MenuState.SELECTING;
                }
                break;
            case MenuState.SELECTING:
                if (_menuQueue.Count == 0)
                {
                    CurrentState = MenuState.DONE;
                    GameCanvas.menu.showMenu = false;
                    break;
                }
                int nextIndex = _menuQueue.Dequeue();
                if (GameCanvas.menu.showMenu && GameCanvas.menu.menuItems != null)
                {
                    GameCanvas.menu.menuSelectedItem = nextIndex;
                    Logger.Info("NextIndex: " + nextIndex);
                    Command cmd = (Command)GameCanvas.menu.menuItems.elementAt(nextIndex);
                    cmd.performAction();
                    SetDelay(500, 1000);
                    if (_menuQueue.Count > 0)
                    {
                        CurrentState = MenuState.WAIT_MENU_SHOW;
                        GameCanvas.menu.showMenu = false;
                    }
                }
                break;
            case MenuState.DONE:
                Logger.Info("DONE");
                if (!GameCanvas.menu.showMenu) CurrentState = MenuState.IDLE;
                break;
        }
    }
}