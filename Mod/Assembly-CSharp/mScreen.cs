using System;

// Token: 0x02000073 RID: 115
public class mScreen
{
	// Token: 0x060005AE RID: 1454 RVA: 0x00068CA0 File Offset: 0x00066EA0
	public virtual void switchToMe()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
		bool flag = GameCanvas.currentScreen != null;
		if (flag)
		{
			GameCanvas.currentScreen.unLoad();
		}
		GameCanvas.currentScreen = this;
		string str = "cur Screen: ";
		mScreen currentScreen = GameCanvas.currentScreen;
		Cout.LogError3(str + ((currentScreen != null) ? currentScreen.ToString() : null));
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00003136 File Offset: 0x00001336
	public virtual void unLoad()
	{
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00003136 File Offset: 0x00001336
	public static void initPos()
	{
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00003136 File Offset: 0x00001336
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00003136 File Offset: 0x00001336
	public virtual void update()
	{
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00068CFC File Offset: 0x00066EFC
	public virtual void updateKey()
	{
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center);
		if (flag)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool flag2 = this.center != null;
			if (flag2)
			{
				this.center.performAction();
			}
		}
		bool flag3 = GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left);
		if (flag3)
		{
			GameCanvas.keyPressed[12] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool isShow = ChatTextField.gI().isShow;
			if (isShow)
			{
				bool flag4 = ChatTextField.gI().left != null;
				if (flag4)
				{
					ChatTextField.gI().left.performAction();
				}
			}
			else
			{
				bool flag5 = this.left != null;
				if (flag5)
				{
					this.left.performAction();
				}
			}
		}
		bool flag6 = GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right);
		if (flag6)
		{
			GameCanvas.keyPressed[13] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool isShow2 = ChatTextField.gI().isShow;
			if (isShow2)
			{
				bool flag7 = ChatTextField.gI().right != null;
				if (flag7)
				{
					ChatTextField.gI().right.performAction();
				}
			}
			else
			{
				bool flag8 = this.right != null;
				if (flag8)
				{
					this.right.performAction();
				}
			}
		}
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00068E8C File Offset: 0x0006708C
	public static bool getCmdPointerLast(Command cmd)
	{
		bool flag = cmd == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = cmd.x >= 0 && cmd.y != 0;
			if (flag2)
			{
				result = cmd.isPointerPressInside();
			}
			else
			{
				bool flag3 = GameCanvas.currentDialog != null;
				if (flag3)
				{
					bool flag4 = GameCanvas.currentDialog.center != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag4)
					{
						mScreen.keyTouch = 1;
						bool flag5 = cmd == GameCanvas.currentDialog.center && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag5)
						{
							return true;
						}
					}
					bool flag6 = GameCanvas.currentDialog.left != null && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag6)
					{
						mScreen.keyTouch = 0;
						bool flag7 = cmd == GameCanvas.currentDialog.left && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag7)
						{
							return true;
						}
					}
					bool flag8 = GameCanvas.currentDialog.right != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag8)
					{
						mScreen.keyTouch = 2;
						bool flag9 = (cmd == GameCanvas.currentDialog.right || cmd == ChatTextField.gI().right) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag9)
						{
							return true;
						}
					}
				}
				else
				{
					bool flag10 = cmd == GameCanvas.currentScreen.left && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag10)
					{
						mScreen.keyTouch = 0;
						bool flag11 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag11)
						{
							return true;
						}
					}
					bool flag12 = cmd == GameCanvas.currentScreen.right && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag12)
					{
						mScreen.keyTouch = 2;
						bool flag13 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag13)
						{
							return true;
						}
					}
					bool flag14 = (cmd == GameCanvas.currentScreen.center || ChatPopup.currChatPopup != null) && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag14)
					{
						mScreen.keyTouch = 1;
						bool flag15 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag15)
						{
							return true;
						}
					}
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00069170 File Offset: 0x00067370
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
		bool flag = !ChatTextField.gI().isShow || !Main.isPC;
		if (flag)
		{
			bool flag2 = GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu;
			if (flag2)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}
	}

	// Token: 0x04000DD6 RID: 3542
	public Command left;

	// Token: 0x04000DD7 RID: 3543
	public Command center;

	// Token: 0x04000DD8 RID: 3544
	public Command right;

	// Token: 0x04000DD9 RID: 3545
	public Command cmdClose;

	// Token: 0x04000DDA RID: 3546
	public static int ITEM_HEIGHT;

	// Token: 0x04000DDB RID: 3547
	public static int yOpenKeyBoard = 100;

	// Token: 0x04000DDC RID: 3548
	public static int cmdW = 68;

	// Token: 0x04000DDD RID: 3549
	public static int cmdH = 26;

	// Token: 0x04000DDE RID: 3550
	public static int keyTouch = -1;

	// Token: 0x04000DDF RID: 3551
	public static int keyMouse = -1;
}
