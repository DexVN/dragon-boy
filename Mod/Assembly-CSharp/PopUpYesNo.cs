using System;

// Token: 0x0200007B RID: 123
public class PopUpYesNo : IActionListener
{
	// Token: 0x0600042D RID: 1069 RVA: 0x000338B4 File Offset: 0x00031CB4
	public void setPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.info = new string[]
		{
			info
		};
		this.H = 29;
		this.cmdYes = cmdYes;
		this.cmdNo = cmdNo;
		this.cmdYes.img = (this.cmdNo.img = GameScr.imgNut);
		this.cmdYes.imgFocus = (this.cmdNo.imgFocus = GameScr.imgNutF);
		this.cmdYes.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdNo.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdYes.h = mGraphics.getImageHeight(cmdYes.img);
		this.cmdNo.h = mGraphics.getImageHeight(cmdYes.img);
		this.last = mSystem.currentTimeMillis();
		this.dem = this.info[0].Length / 3;
		if (this.dem < 15)
		{
			this.dem = 15;
		}
		TextInfo.reset();
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000339B8 File Offset: 0x00031DB8
	public void paint(mGraphics g)
	{
		PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H + (GameCanvas.isTouch ? 0 : 10), 16777215, false);
		if (this.info != null)
		{
			TextInfo.paint(g, this.info[0], this.X + 5, this.Y + this.H / 2 - ((!GameCanvas.isTouch) ? 6 : 4), this.W - 10, this.H, mFont.tahoma_7);
			if (GameCanvas.isTouch)
			{
				this.cmdYes.paint(g);
				mFont.tahoma_7_yellow.drawString(g, this.dem + string.Empty, this.cmdYes.x + this.cmdYes.w / 2, this.cmdYes.y + this.cmdYes.h + 5, 2, mFont.tahoma_7_grey);
			}
			else if (TField.isQwerty)
			{
				mFont.tahoma_7b_blue.drawString(g, mResources.do_accept_qwerty + this.dem + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
			}
			else
			{
				mFont.tahoma_7b_blue.drawString(g, mResources.do_accept + this.dem + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
			}
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00033B5C File Offset: 0x00031F5C
	public void update()
	{
		if (this.info != null)
		{
			this.X = GameCanvas.w - 5 - this.W;
			this.Y = 45;
			if (GameCanvas.w - 50 > 155 + this.W)
			{
				this.X = GameCanvas.w - 55 - this.W;
				this.Y = 5;
			}
			this.cmdYes.x = this.X - 35;
			this.cmdYes.y = this.Y;
			this.curr = mSystem.currentTimeMillis();
			Res.outz("curr - last= " + (this.curr - this.last));
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.dem--;
			}
			if (this.dem == 0)
			{
				GameScr.gI().popUpYesNo = null;
			}
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00033C5D File Offset: 0x0003205D
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000747 RID: 1863
	public Command cmdYes;

	// Token: 0x04000748 RID: 1864
	public Command cmdNo;

	// Token: 0x04000749 RID: 1865
	public string[] info;

	// Token: 0x0400074A RID: 1866
	private int X;

	// Token: 0x0400074B RID: 1867
	private int Y;

	// Token: 0x0400074C RID: 1868
	private int W = 120;

	// Token: 0x0400074D RID: 1869
	private int H;

	// Token: 0x0400074E RID: 1870
	private int dem;

	// Token: 0x0400074F RID: 1871
	private long last;

	// Token: 0x04000750 RID: 1872
	private long curr;
}
