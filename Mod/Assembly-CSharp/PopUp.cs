using System;

// Token: 0x0200008E RID: 142
public class PopUp
{
	// Token: 0x060007BB RID: 1979 RVA: 0x0008CDA4 File Offset: 0x0008AFA4
	public PopUp(string info, int x, int y)
	{
		this.sayWidth = 100;
		bool flag = info.Length < 10;
		if (flag)
		{
			this.sayWidth = 60;
		}
		bool flag2 = GameCanvas.w == 128;
		if (flag2)
		{
			this.sayWidth = 128;
		}
		this.says = mFont.tahoma_7b_dark.splitFontArray(info, this.sayWidth - 10);
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		bool flag3 = x >= 0 && x <= 24;
		if (flag3)
		{
			this.cx += this.cw / 2 + 30;
		}
		bool flag4 = x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24;
		if (flag4)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0008CF80 File Offset: 0x0008B180
	public static void loadBg()
	{
		bool flag = PopUp.goc == null;
		if (flag)
		{
			PopUp.goc = GameCanvas.loadImage("/mainImage/myTexture2dbd3.png");
		}
		bool flag2 = PopUp.imgPopUp == null;
		if (flag2)
		{
			PopUp.imgPopUp = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup.png");
		}
		bool flag3 = PopUp.imgPopUp2 == null;
		if (flag3)
		{
			PopUp.imgPopUp2 = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup2.png");
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0008CFE8 File Offset: 0x0008B1E8
	public void updateXYWH(string[] info, int x, int y)
	{
		this.sayWidth = 0;
		for (int i = 0; i < info.Length; i++)
		{
			bool flag = this.sayWidth < mFont.tahoma_7b_dark.getWidth(info[i]);
			if (flag)
			{
				this.sayWidth = mFont.tahoma_7b_dark.getWidth(info[i]);
			}
		}
		this.sayWidth += 20;
		this.says = info;
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		bool flag2 = x >= 0 && x <= 24;
		if (flag2)
		{
			this.cx += this.cw / 2 + 30;
		}
		bool flag3 = x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24;
		if (flag3)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0008D1BE File Offset: 0x0008B3BE
	public static void addPopUp(int x, int y, string info)
	{
		PopUp.vPopups.addElement(new PopUp(info, x, y));
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0008D1D4 File Offset: 0x0008B3D4
	public static void addPopUp(PopUp p)
	{
		PopUp.vPopups.addElement(p);
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0008D1E3 File Offset: 0x0008B3E3
	public static void removePopUp(PopUp p)
	{
		PopUp.vPopups.removeElement(p);
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0008D1F4 File Offset: 0x0008B3F4
	public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
	{
		bool flag = color == 1;
		if (flag)
		{
			g.fillRect(x, y, w, h, 16777215, 90);
		}
		else
		{
			g.fillRect(x, y, w, h, 0, 77);
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0008D238 File Offset: 0x0008B438
	public static void paintPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isButton)
	{
		bool flag = !isButton;
		if (flag)
		{
			g.setColor(0);
			g.fillRect(x + 6, y, w - 14 + 1, h);
			g.fillRect(x, y + 6, w, h - 12 + 1);
			g.setColor(color);
			g.fillRect(x + 6, y + 1, w - 12, h - 2);
			g.fillRect(x + 1, y + 6, w - 2, h - 12);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 0, x, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 2, x + w - 7, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 1, x, y + h - 6, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 3, x + w - 7, y + h - 6, 0);
		}
		else
		{
			Image arg = (color != 1) ? PopUp.imgPopUp : PopUp.imgPopUp2;
			g.drawRegion(arg, 0, 0, 10, 10, 0, x, y, 0);
			g.drawRegion(arg, 0, 20, 10, 10, 0, x + w - 10, y, 0);
			g.drawRegion(arg, 0, 50, 10, 10, 0, x, y + h - 10, 0);
			g.drawRegion(arg, 0, 70, 10, 10, 0, x + w - 10, y + h - 10, 0);
			int num = ((w - 20) % 10 != 0) ? ((w - 20) / 10 + 1) : ((w - 20) / 10);
			int num2 = ((h - 20) % 10 != 0) ? ((h - 20) / 10 + 1) : ((h - 20) / 10);
			for (int i = 0; i < num; i++)
			{
				g.drawRegion(arg, 0, 10, 10, 10, 0, x + 10 + i * 10, y, 0);
			}
			for (int j = 0; j < num2; j++)
			{
				g.drawRegion(arg, 0, 30, 10, 10, 0, x, y + 10 + j * 10, 0);
			}
			for (int k = 0; k < num; k++)
			{
				g.drawRegion(arg, 0, 60, 10, 10, 0, x + 10 + k * 10, y + h - 10, 0);
			}
			for (int l = 0; l < num2; l++)
			{
				g.drawRegion(arg, 0, 40, 10, 10, 0, x + w - 10, y + 10 + l * 10, 0);
			}
			g.setColor((color != 1) ? 16770503 : 12052656);
			g.fillRect(x + 10, y + 10, w - 20, h - 20);
		}
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0008D4D8 File Offset: 0x0008B6D8
	public void paint(mGraphics g)
	{
		bool flag = !this.isPaint;
		if (!flag)
		{
			bool flag2 = this.says == null;
			if (!flag2)
			{
				bool flag3 = ChatPopup.currChatPopup != null;
				if (!flag3)
				{
					bool flag4 = !this.isHide;
					if (flag4)
					{
						this.paintClipPopUp(g, this.cx, this.cy - GameCanvas.transY, this.cw, this.ch, (this.timeDelay != 0) ? 1 : 0, true);
						for (int i = 0; i < this.says.Length; i++)
						{
							((this.timeDelay != 0) ? mFont.tahoma_7b_green2 : mFont.tahoma_7b_white).drawString(g, this.says[i], this.cx + this.cw / 2, this.cy + (this.ch / 2 - this.says.Length * 12 / 2) + i * 12 - GameCanvas.transY, 2);
						}
					}
				}
			}
		}
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0008D5E0 File Offset: 0x0008B7E0
	private void update()
	{
		bool flag = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0;
		if (flag)
		{
			bool flag2 = this.cx + this.cw >= GameScr.cmx && this.cx <= GameCanvas.w + GameScr.cmx && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy;
			if (flag2)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		bool flag3 = global::Char.myCharz().taskMaint == null || (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId != 0);
		if (flag3)
		{
			bool flag4 = this.cx + this.cw / 2 >= global::Char.myCharz().cx - 100 && this.cx + this.cw / 2 <= global::Char.myCharz().cx + 100 && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy;
			if (flag4)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		bool flag5 = this.timeDelay > 0;
		if (flag5)
		{
			this.timeDelay--;
			bool flag6 = this.timeDelay == 0 && this.command != null;
			if (flag6)
			{
				this.command.performAction();
			}
		}
		bool flag7 = this.isWayPoint;
		if (flag7)
		{
			bool flag8 = global::Char.myCharz().taskMaint != null;
			if (flag8)
			{
				bool flag9 = global::Char.myCharz().taskMaint.taskId == 0;
				if (flag9)
				{
					bool flag10 = global::Char.myCharz().taskMaint.index == 0;
					if (flag10)
					{
						this.isPaint = false;
					}
					bool flag11 = global::Char.myCharz().taskMaint.index == 1;
					if (flag11)
					{
						this.isPaint = true;
					}
					bool flag12 = global::Char.myCharz().taskMaint.index > 1 && global::Char.myCharz().taskMaint.index < 6;
					if (flag12)
					{
						this.isPaint = false;
					}
				}
				else
				{
					bool flag13 = !this.isPaint;
					if (flag13)
					{
						this.tDelay++;
						bool flag14 = this.tDelay == 50;
						if (flag14)
						{
							this.isPaint = true;
						}
					}
				}
			}
			else
			{
				bool flag15 = !this.isPaint;
				if (flag15)
				{
					Hint.isPaint = false;
					this.tDelay++;
					bool flag16 = this.tDelay == 50;
					if (flag16)
					{
						this.isPaint = true;
						Hint.isPaint = true;
					}
				}
			}
		}
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0008D8BD File Offset: 0x0008BABD
	public void doClick(int timeDelay)
	{
		this.timeDelay = timeDelay;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0008D8C8 File Offset: 0x0008BAC8
	public static void paintAll(mGraphics g)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0008D908 File Offset: 0x0008BB08
	public static void updateAll()
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).update();
		}
	}

	// Token: 0x0400101A RID: 4122
	public static MyVector vPopups = new MyVector();

	// Token: 0x0400101B RID: 4123
	public int sayWidth;

	// Token: 0x0400101C RID: 4124
	public int sayRun;

	// Token: 0x0400101D RID: 4125
	public string[] says;

	// Token: 0x0400101E RID: 4126
	public int cx;

	// Token: 0x0400101F RID: 4127
	public int cy;

	// Token: 0x04001020 RID: 4128
	public int cw;

	// Token: 0x04001021 RID: 4129
	public int ch;

	// Token: 0x04001022 RID: 4130
	public static int f;

	// Token: 0x04001023 RID: 4131
	public static int tF;

	// Token: 0x04001024 RID: 4132
	public static int dir;

	// Token: 0x04001025 RID: 4133
	public bool isWayPoint;

	// Token: 0x04001026 RID: 4134
	public int tDelay;

	// Token: 0x04001027 RID: 4135
	private int timeDelay;

	// Token: 0x04001028 RID: 4136
	public Command command;

	// Token: 0x04001029 RID: 4137
	public bool isPaint = true;

	// Token: 0x0400102A RID: 4138
	public bool isHide;

	// Token: 0x0400102B RID: 4139
	public static Image goc;

	// Token: 0x0400102C RID: 4140
	public static Image imgPopUp;

	// Token: 0x0400102D RID: 4141
	public static Image imgPopUp2;

	// Token: 0x0400102E RID: 4142
	public Image imgFocus;

	// Token: 0x0400102F RID: 4143
	public Image imgUnFocus;
}
