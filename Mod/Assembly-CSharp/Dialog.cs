using System;

// Token: 0x02000022 RID: 34
public abstract class Dialog
{
	// Token: 0x060001F2 RID: 498 RVA: 0x000349FC File Offset: 0x00032BFC
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00034A5C File Offset: 0x00032C5C
	public virtual void keyPress(int keyCode)
	{
		switch (keyCode + 7)
		{
		case 0:
			goto IL_10E;
		case 1:
			goto IL_F9;
		case 2:
			goto IL_123;
		case 5:
			goto IL_D0;
		case 6:
			goto IL_A8;
		}
		bool flag = keyCode == -39;
		if (flag)
		{
			goto IL_D0;
		}
		bool flag2 = keyCode != -38;
		if (flag2)
		{
			bool flag3 = keyCode == -22;
			if (flag3)
			{
				goto IL_10E;
			}
			bool flag4 = keyCode == -21;
			if (flag4)
			{
				goto IL_F9;
			}
			bool flag5 = keyCode == -27;
			if (flag5)
			{
				return;
			}
			bool flag6 = keyCode != 10;
			if (flag6)
			{
				return;
			}
			goto IL_123;
		}
		IL_A8:
		GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
		return;
		IL_D0:
		GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
		return;
		IL_F9:
		GameCanvas.keyHold[12] = true;
		GameCanvas.keyPressed[12] = true;
		return;
		IL_10E:
		GameCanvas.keyHold[13] = true;
		GameCanvas.keyPressed[13] = true;
		return;
		IL_123:
		GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00034BB4 File Offset: 0x00032DB4
	public virtual void update()
	{
		bool flag = this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center));
		if (flag)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool flag2 = this.center != null;
			if (flag2)
			{
				this.center.performAction();
			}
			mScreen.keyTouch = -1;
		}
		bool flag3 = this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left));
		if (flag3)
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool flag4 = this.left != null;
			if (flag4)
			{
				this.left.performAction();
			}
			mScreen.keyTouch = -1;
		}
		bool flag5 = this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right));
		if (flag5)
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			mScreen.keyTouch = -1;
			bool flag6 = this.right != null;
			if (flag6)
			{
				this.right.performAction();
			}
			mScreen.keyTouch = -1;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00003136 File Offset: 0x00001336
	public virtual void show()
	{
	}

	// Token: 0x040004B2 RID: 1202
	public Command left;

	// Token: 0x040004B3 RID: 1203
	public Command center;

	// Token: 0x040004B4 RID: 1204
	public Command right;

	// Token: 0x040004B5 RID: 1205
	private int lenCaption;
}
