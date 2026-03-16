using System;

// Token: 0x020000A7 RID: 167
public abstract class Dialog
{
	// Token: 0x060006FF RID: 1791 RVA: 0x00060AE8 File Offset: 0x0005EEE8
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00060B44 File Offset: 0x0005EF44
	public virtual void keyPress(int keyCode)
	{
		switch (keyCode + 7)
		{
		case 0:
			goto IL_D2;
		case 1:
			goto IL_BF;
		case 2:
			goto IL_E5;
		default:
			if (keyCode == -39)
			{
				goto IL_8C;
			}
			if (keyCode != -38)
			{
				if (keyCode == -22)
				{
					goto IL_D2;
				}
				if (keyCode == -21)
				{
					goto IL_BF;
				}
				if (keyCode == -27)
				{
					return;
				}
				if (keyCode != 10)
				{
					return;
				}
				goto IL_E5;
			}
			break;
		case 5:
			goto IL_8C;
		case 6:
			break;
		}
		GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
		return;
		IL_8C:
		GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
		return;
		IL_BF:
		GameCanvas.keyHold[12] = true;
		GameCanvas.keyPressed[12] = true;
		return;
		IL_D2:
		GameCanvas.keyHold[13] = true;
		GameCanvas.keyPressed[13] = true;
		return;
		IL_E5:
		GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00060C6C File Offset: 0x0005F06C
	public virtual void update()
	{
		if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.center != null)
			{
				this.center.performAction();
			}
			mScreen.keyTouch = -1;
		}
		if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.left != null)
			{
				this.left.performAction();
			}
			mScreen.keyTouch = -1;
		}
		if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			mScreen.keyTouch = -1;
			if (this.right != null)
			{
				this.right.performAction();
			}
			mScreen.keyTouch = -1;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00060DC0 File Offset: 0x0005F1C0
	public virtual void show()
	{
	}

	// Token: 0x04000CD6 RID: 3286
	public Command left;

	// Token: 0x04000CD7 RID: 3287
	public Command center;

	// Token: 0x04000CD8 RID: 3288
	public Command right;

	// Token: 0x04000CD9 RID: 3289
	private int lenCaption;
}
