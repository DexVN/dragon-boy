using System;

// Token: 0x0200008F RID: 143
public class PopUpYesNo : IActionListener
{
	// Token: 0x060007C9 RID: 1993 RVA: 0x0008D954 File Offset: 0x0008BB54
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
		bool flag = this.dem < 15;
		if (flag)
		{
			this.dem = 15;
		}
		TextInfo.reset();
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0008DA5C File Offset: 0x0008BC5C
	public void paint(mGraphics g)
	{
		PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H + (GameCanvas.isTouch ? 0 : 10), 16777215, false);
		bool flag = this.info != null;
		if (flag)
		{
			TextInfo.paint(g, this.info[0], this.X + 5, this.Y + this.H / 2 - ((!GameCanvas.isTouch) ? 6 : 4), this.W - 10, this.H, mFont.tahoma_7);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.cmdYes.paint(g);
				mFont.tahoma_7_yellow.drawString(g, this.dem.ToString() + string.Empty, this.cmdYes.x + this.cmdYes.w / 2, this.cmdYes.y + this.cmdYes.h + 5, 2, mFont.tahoma_7_grey);
			}
			else
			{
				bool isQwerty = TField.isQwerty;
				if (isQwerty)
				{
					mFont.tahoma_7b_blue.drawString(g, mResources.do_accept_qwerty + this.dem.ToString() + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
				}
				else
				{
					mFont.tahoma_7b_blue.drawString(g, mResources.do_accept + this.dem.ToString() + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
				}
			}
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0008DC04 File Offset: 0x0008BE04
	public void update()
	{
		bool flag = this.info != null;
		if (flag)
		{
			this.X = GameCanvas.w - 5 - this.W;
			this.Y = 45;
			bool flag2 = GameCanvas.w - 50 > 155 + this.W;
			if (flag2)
			{
				this.X = GameCanvas.w - 55 - this.W;
				this.Y = 5;
			}
			this.cmdYes.x = this.X - 35;
			this.cmdYes.y = this.Y;
			this.curr = mSystem.currentTimeMillis();
			Res.outz("curr - last= " + (this.curr - this.last).ToString());
			bool flag3 = this.curr - this.last >= 1000L;
			if (flag3)
			{
				this.last = mSystem.currentTimeMillis();
				this.dem--;
			}
			bool flag4 = this.dem == 0;
			if (flag4)
			{
				GameScr.gI().popUpYesNo = null;
			}
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00003136 File Offset: 0x00001336
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04001030 RID: 4144
	public Command cmdYes;

	// Token: 0x04001031 RID: 4145
	public Command cmdNo;

	// Token: 0x04001032 RID: 4146
	public string[] info;

	// Token: 0x04001033 RID: 4147
	private int X;

	// Token: 0x04001034 RID: 4148
	private int Y;

	// Token: 0x04001035 RID: 4149
	private int W = 120;

	// Token: 0x04001036 RID: 4150
	private int H;

	// Token: 0x04001037 RID: 4151
	private int dem;

	// Token: 0x04001038 RID: 4152
	private long last;

	// Token: 0x04001039 RID: 4153
	private long curr;
}
