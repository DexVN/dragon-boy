using System;

// Token: 0x0200006E RID: 110
public class MoneyCharge : mScreen, IActionListener
{
	// Token: 0x06000586 RID: 1414 RVA: 0x000663E4 File Offset: 0x000645E4
	public MoneyCharge()
	{
		this.w = GameCanvas.w - 20;
		bool flag = this.w > 320;
		if (flag)
		{
			this.w = 320;
		}
		this.strPaint = mFont.tahoma_7b_green2.splitFontArray(mResources.pay_card, this.w - 20);
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = GameCanvas.h - 150 - (this.strPaint.Length - 1) * 20;
		this.h = 110 + (this.strPaint.Length - 1) * 20;
		this.yP = this.y;
		this.tfSerial = new TField();
		this.tfSerial.name = mResources.SERI_NUM;
		this.tfSerial.x = this.x + 10;
		this.tfSerial.y = this.y + 35 + (this.strPaint.Length - 1) * 20;
		this.yt = this.tfSerial.y;
		this.tfSerial.width = this.w - 20;
		this.tfSerial.height = mScreen.ITEM_HEIGHT + 2;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.tfSerial.isFocus = false;
		}
		else
		{
			this.tfSerial.isFocus = true;
		}
		this.tfSerial.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfSerial.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfSerial.isPaintMouse = false;
		}
		bool flag2 = !GameCanvas.isTouch;
		if (flag2)
		{
			this.right = this.tfSerial.cmdClear;
		}
		this.tfCode = new TField();
		this.tfCode.name = mResources.CARD_CODE;
		this.tfCode.x = this.x + 10;
		this.tfCode.y = this.tfSerial.y + 35;
		this.tfCode.width = this.w - 20;
		this.tfCode.height = mScreen.ITEM_HEIGHT + 2;
		this.tfCode.isFocus = false;
		this.tfCode.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone2 = Main.isWindowsPhone;
		if (isWindowsPhone2)
		{
			this.tfCode.showSubTextField = false;
		}
		bool isIPhone2 = Main.isIPhone;
		if (isIPhone2)
		{
			this.tfCode.isPaintMouse = false;
		}
		this.left = new Command(mResources.CLOSE, this, 1, null);
		this.center = new Command(mResources.pay_card2, this, 2, null);
		bool isTouch2 = GameCanvas.isTouch;
		if (isTouch2)
		{
			this.center.x = GameCanvas.w / 2 + 18;
			this.left.x = GameCanvas.w / 2 - 85;
			this.center.y = (this.left.y = this.y + this.h + 5);
		}
		this.freeAreaHeight = this.tfSerial.y - (4 * this.tfSerial.height - 10);
		this.yP = this.tfSerial.y;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00066734 File Offset: 0x00064934
	public static MoneyCharge gI()
	{
		bool flag = MoneyCharge.instance == null;
		if (flag)
		{
			MoneyCharge.instance = new MoneyCharge();
		}
		return MoneyCharge.instance;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00066763 File Offset: 0x00064963
	public override void switchToMe()
	{
		this.focus = 0;
		base.switchToMe();
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00003136 File Offset: 0x00001336
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00066774 File Offset: 0x00064974
	public override void paint(mGraphics g)
	{
		GameScr.gI().paint(g);
		PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
		for (int i = 0; i < this.strPaint.Length; i++)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
		}
		this.tfSerial.paint(g);
		this.tfCode.paint(g);
		base.paint(g);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00066818 File Offset: 0x00064A18
	public override void update()
	{
		GameScr.gI().update();
		this.tfSerial.update();
		this.tfCode.update();
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.updateTfWhenOpenKb();
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0006685C File Offset: 0x00064A5C
	public override void keyPress(int keyCode)
	{
		bool isFocus = this.tfSerial.isFocus;
		if (isFocus)
		{
			this.tfSerial.keyPressed(keyCode);
		}
		else
		{
			bool isFocus2 = this.tfCode.isFocus;
			if (isFocus2)
			{
				this.tfCode.keyPressed(keyCode);
			}
		}
		base.keyPress(keyCode);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x000668B0 File Offset: 0x00064AB0
	public override void updateKey()
	{
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			this.focus--;
			bool flag2 = this.focus < 0;
			if (flag2)
			{
				this.focus = 1;
			}
		}
		else
		{
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			if (flag3)
			{
				this.focus++;
				bool flag4 = this.focus > 1;
				if (flag4)
				{
					this.focus = 1;
				}
			}
		}
		bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
		if (flag5)
		{
			GameCanvas.clearKeyPressed();
			bool flag6 = this.focus == 1;
			if (flag6)
			{
				this.tfSerial.isFocus = false;
				this.tfCode.isFocus = true;
				bool flag7 = !GameCanvas.isTouch;
				if (flag7)
				{
					this.right = this.tfCode.cmdClear;
				}
			}
			else
			{
				bool flag8 = this.focus == 0;
				if (flag8)
				{
					this.tfSerial.isFocus = true;
					this.tfCode.isFocus = false;
					bool flag9 = !GameCanvas.isTouch;
					if (flag9)
					{
						this.right = this.tfSerial.cmdClear;
					}
				}
				else
				{
					this.tfSerial.isFocus = false;
					this.tfCode.isFocus = false;
				}
			}
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			bool flag10 = GameCanvas.isPointerHoldIn(this.tfSerial.x, this.tfSerial.y, this.tfSerial.width, this.tfSerial.height);
			if (flag10)
			{
				this.focus = 0;
			}
			else
			{
				bool flag11 = GameCanvas.isPointerHoldIn(this.tfCode.x, this.tfCode.y, this.tfCode.width, this.tfCode.height);
				if (flag11)
				{
					this.focus = 1;
				}
			}
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00066AC3 File Offset: 0x00064CC3
	public void clearScreen()
	{
		MoneyCharge.instance = null;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00066ACC File Offset: 0x00064CCC
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1;
		if (flag)
		{
			GameScr.instance.switchToMe();
			this.clearScreen();
		}
		bool flag2 = idAction == 2;
		if (flag2)
		{
			bool flag3 = this.tfSerial.getText() == null || this.tfSerial.getText().Equals(string.Empty);
			if (flag3)
			{
				GameCanvas.startOKDlg(mResources.serial_blank);
			}
			else
			{
				bool flag4 = this.tfCode.getText() == null || this.tfCode.getText().Equals(string.Empty);
				if (flag4)
				{
					GameCanvas.startOKDlg(mResources.card_code_blank);
				}
				else
				{
					Service.gI().sendCardInfo(this.tfSerial.getText(), this.tfCode.getText());
					GameScr.instance.switchToMe();
					this.clearScreen();
				}
			}
		}
	}

	// Token: 0x04000BDE RID: 3038
	public static MoneyCharge instance;

	// Token: 0x04000BDF RID: 3039
	public TField tfSerial;

	// Token: 0x04000BE0 RID: 3040
	public TField tfCode;

	// Token: 0x04000BE1 RID: 3041
	private int x;

	// Token: 0x04000BE2 RID: 3042
	private int y;

	// Token: 0x04000BE3 RID: 3043
	private int w;

	// Token: 0x04000BE4 RID: 3044
	private int h;

	// Token: 0x04000BE5 RID: 3045
	private string[] strPaint;

	// Token: 0x04000BE6 RID: 3046
	private int focus;

	// Token: 0x04000BE7 RID: 3047
	private int yt;

	// Token: 0x04000BE8 RID: 3048
	private int freeAreaHeight;

	// Token: 0x04000BE9 RID: 3049
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000BEA RID: 3050
	private int yP;
}
