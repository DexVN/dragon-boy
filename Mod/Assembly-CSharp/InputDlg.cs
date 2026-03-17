using System;

// Token: 0x0200004B RID: 75
public class InputDlg : Dialog
{
	// Token: 0x06000411 RID: 1041 RVA: 0x00058154 File Offset: 0x00056354
	public InputDlg()
	{
		this.padLeft = 40;
		bool flag = GameCanvas.w <= 176;
		if (flag)
		{
			this.padLeft = 10;
		}
		this.tfInput = new TField();
		this.tfInput.x = this.padLeft + 10;
		this.tfInput.y = GameCanvas.h - mScreen.ITEM_HEIGHT - 43;
		this.tfInput.width = GameCanvas.w - 2 * (this.padLeft + 10);
		this.tfInput.height = mScreen.ITEM_HEIGHT + 2;
		this.tfInput.isFocus = true;
		this.right = this.tfInput.cmdClear;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00058214 File Offset: 0x00056414
	public void show(string info, Command ok, int type)
	{
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(type);
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
		this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
		this.center = ok;
		this.show();
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00058288 File Offset: 0x00056488
	public override void paint(mGraphics g)
	{
		GameCanvas.paintz.paintInputDlg(g, this.padLeft, GameCanvas.h - 77 - mScreen.cmdH, GameCanvas.w - this.padLeft * 2, 69, this.info);
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x000582E1 File Offset: 0x000564E1
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		base.keyPress(keyCode);
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x000582F9 File Offset: 0x000564F9
	public override void update()
	{
		this.tfInput.update();
		base.update();
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0005830F File Offset: 0x0005650F
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00058318 File Offset: 0x00056518
	public void hide()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x04000908 RID: 2312
	protected string[] info;

	// Token: 0x04000909 RID: 2313
	public TField tfInput;

	// Token: 0x0400090A RID: 2314
	private int padLeft;
}
