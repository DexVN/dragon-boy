using System;

// Token: 0x02000068 RID: 104
public class ItemTime
{
	// Token: 0x060003AD RID: 941 RVA: 0x0001E72F File Offset: 0x0001CB2F
	public ItemTime()
	{
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001E740 File Offset: 0x0001CB40
	public ItemTime(short idIcon, int s)
	{
		this.idIcon = idIcon;
		this.minute = s / 60;
		this.second = s % 60;
		this.time = s;
		this.coutTime = s;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isPaint_coolDownBar = (idIcon == 14);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001E7A4 File Offset: 0x0001CBA4
	public void initTimeText(sbyte id, string text, int time)
	{
		if (time == -1)
		{
			this.dontClear = true;
		}
		else
		{
			this.dontClear = false;
		}
		this.isText = true;
		this.minute = time / 60;
		this.second = time % 60;
		this.idIcon = (short)id;
		this.time = time;
		this.coutTime = time;
		this.text = text;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isPaint_coolDownBar = (this.idIcon == 14);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0001E828 File Offset: 0x0001CC28
	public void initTime(int time, bool isText)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.time = time;
		this.coutTime = time;
		this.isText = isText;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001E874 File Offset: 0x0001CC74
	public static bool isExistItem(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0001E8BC File Offset: 0x0001CCBC
	public static ItemTime getMessageById(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001E904 File Offset: 0x0001CD04
	public static bool isExistMessage(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001E94C File Offset: 0x0001CD4C
	public static ItemTime getItemById(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0001E994 File Offset: 0x0001CD94
	public void initTime(int time)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.coutTime = time;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001E9D0 File Offset: 0x0001CDD0
	public void paint(mGraphics g, int x, int y)
	{
		SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
		string st = string.Empty;
		st = this.minute + "'";
		if (this.minute == 0)
		{
			st = this.second + "s";
		}
		mFont.tahoma_7b_white.drawString(g, st, x, y + 15, 2, mFont.tahoma_7b_dark);
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001EA44 File Offset: 0x0001CE44
	public void paintText(mGraphics g, int x, int y)
	{
		if (this.isPaint_coolDownBar)
		{
			if (global::Char.myCharz() != null)
			{
				int num = 80;
				int x2 = GameCanvas.w / 2 - num / 2;
				int y2 = GameCanvas.h - 80;
				g.setColor(8421504);
				g.fillRect(x2, y2, num, 2);
				g.setColor(16777215);
				if (this.per > 0)
				{
					g.fillRect(x2, y2, num * this.per / 100, 2);
				}
			}
		}
		else
		{
			string str = string.Empty;
			str = this.minute + "'";
			if (this.minute < 1)
			{
				str = this.second + "s";
			}
			if (this.minute < 0)
			{
				str = string.Empty;
			}
			if (this.dontClear)
			{
				str = string.Empty;
			}
			mFont.tahoma_7b_white.drawString(g, this.text + " " + str, x, y, 0, mFont.tahoma_7b_dark);
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001EB48 File Offset: 0x0001CF48
	public void update()
	{
		this.curr = mSystem.currentTimeMillis();
		if (this.curr - this.last >= 1000L)
		{
			this.last = mSystem.currentTimeMillis();
			this.second--;
			this.coutTime--;
			if (this.second <= 0)
			{
				this.second = 60;
				this.minute--;
			}
			if (this.time > 0)
			{
				this.per = this.coutTime * 100 / this.time;
			}
		}
		if (this.minute < 0 && !this.isText)
		{
			global::Char.vItemTime.removeElement(this);
		}
		if (this.minute < 0 && this.isText && !this.dontClear)
		{
			GameScr.textTime.removeElement(this);
		}
	}

	// Token: 0x04000629 RID: 1577
	public short idIcon;

	// Token: 0x0400062A RID: 1578
	public int second;

	// Token: 0x0400062B RID: 1579
	public int minute;

	// Token: 0x0400062C RID: 1580
	private long curr;

	// Token: 0x0400062D RID: 1581
	private long last;

	// Token: 0x0400062E RID: 1582
	private bool isText;

	// Token: 0x0400062F RID: 1583
	private bool dontClear;

	// Token: 0x04000630 RID: 1584
	private string text;

	// Token: 0x04000631 RID: 1585
	private bool isPaint_coolDownBar;

	// Token: 0x04000632 RID: 1586
	public int time;

	// Token: 0x04000633 RID: 1587
	public int coutTime;

	// Token: 0x04000634 RID: 1588
	private int per = 100;
}
