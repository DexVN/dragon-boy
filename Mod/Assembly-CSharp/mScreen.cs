using System;

// Token: 0x020000C3 RID: 195
public class mScreen
{
	// Token: 0x060009D1 RID: 2513 RVA: 0x0005C8D2 File Offset: 0x0005ACD2
	public virtual void switchToMe()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
		if (GameCanvas.currentScreen != null)
		{
			GameCanvas.currentScreen.unLoad();
		}
		GameCanvas.currentScreen = this;
		Cout.LogError3("cur Screen: " + GameCanvas.currentScreen);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0005C90C File Offset: 0x0005AD0C
	public virtual void unLoad()
	{
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0005C90E File Offset: 0x0005AD0E
	public static void initPos()
	{
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0005C910 File Offset: 0x0005AD10
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0005C912 File Offset: 0x0005AD12
	public virtual void update()
	{
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0005C914 File Offset: 0x0005AD14
	public virtual void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.center != null)
			{
				this.center.performAction();
			}
		}
		if (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left))
		{
			GameCanvas.keyPressed[12] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().left != null)
				{
					ChatTextField.gI().left.performAction();
				}
			}
			else if (this.left != null)
			{
				this.left.performAction();
			}
		}
		if (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right))
		{
			GameCanvas.keyPressed[13] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().right != null)
				{
					ChatTextField.gI().right.performAction();
				}
			}
			else if (this.right != null)
			{
				this.right.performAction();
			}
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0005CA8C File Offset: 0x0005AE8C
	public static bool getCmdPointerLast(Command cmd)
	{
		if (cmd == null)
		{
			return false;
		}
		if (cmd.x >= 0 && cmd.y != 0)
		{
			return cmd.isPointerPressInside();
		}
		if (GameCanvas.currentDialog != null)
		{
			if (GameCanvas.currentDialog.center != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 1;
				if (cmd == GameCanvas.currentDialog.center && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (GameCanvas.currentDialog.left != null && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 0;
				if (cmd == GameCanvas.currentDialog.left && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (GameCanvas.currentDialog.right != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 2;
				if ((cmd == GameCanvas.currentDialog.right || cmd == ChatTextField.gI().right) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
		}
		else
		{
			if (cmd == GameCanvas.currentScreen.left && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 0;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (cmd == GameCanvas.currentScreen.right && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 2;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if ((cmd == GameCanvas.currentScreen.center || ChatPopup.currChatPopup != null) && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 1;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0005CD28 File Offset: 0x0005B128
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
		if (!ChatTextField.gI().isShow || !Main.isPC)
		{
			if (GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}
	}

	// Token: 0x0400121A RID: 4634
	public Command left;

	// Token: 0x0400121B RID: 4635
	public Command center;

	// Token: 0x0400121C RID: 4636
	public Command right;

	// Token: 0x0400121D RID: 4637
	public Command cmdClose;

	// Token: 0x0400121E RID: 4638
	public static int ITEM_HEIGHT;

	// Token: 0x0400121F RID: 4639
	public static int yOpenKeyBoard = 100;

	// Token: 0x04001220 RID: 4640
	public static int cmdW = 68;

	// Token: 0x04001221 RID: 4641
	public static int cmdH = 26;

	// Token: 0x04001222 RID: 4642
	public static int keyTouch = -1;

	// Token: 0x04001223 RID: 4643
	public static int keyMouse = -1;
}
