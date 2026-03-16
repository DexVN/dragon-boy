using System;

// Token: 0x020000B1 RID: 177
public class InputDlg : Dialog
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x00071ADC File Offset: 0x0006FEDC
	public InputDlg()
	{
		this.padLeft = 40;
		if (GameCanvas.w <= 176)
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

	// Token: 0x060007E1 RID: 2017 RVA: 0x00071B94 File Offset: 0x0006FF94
	public void show(string info, Command ok, int type)
	{
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(type);
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
		this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
		this.center = ok;
		this.show();
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00071C04 File Offset: 0x00070004
	public override void paint(mGraphics g)
	{
		GameCanvas.paintz.paintInputDlg(g, this.padLeft, GameCanvas.h - 77 - mScreen.cmdH, GameCanvas.w - this.padLeft * 2, 69, this.info);
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00071C59 File Offset: 0x00070059
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		base.keyPress(keyCode);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00071C6F File Offset: 0x0007006F
	public override void update()
	{
		this.tfInput.update();
		base.update();
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00071C82 File Offset: 0x00070082
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00071C8A File Offset: 0x0007008A
	public void hide()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x04000EED RID: 3821
	protected string[] info;

	// Token: 0x04000EEE RID: 3822
	public TField tfInput;

	// Token: 0x04000EEF RID: 3823
	private int padLeft;
}
