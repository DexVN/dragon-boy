using System;

// Token: 0x0200005A RID: 90
public class ItemTime
{
	// Token: 0x0600048C RID: 1164 RVA: 0x00059F7D File Offset: 0x0005817D
	public ItemTime()
	{
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00059F90 File Offset: 0x00058190
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

	// Token: 0x0600048E RID: 1166 RVA: 0x00059FF8 File Offset: 0x000581F8
	public void initTimeText(sbyte id, string text, int time)
	{
		bool flag = time == -1;
		if (flag)
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

	// Token: 0x0600048F RID: 1167 RVA: 0x0005A080 File Offset: 0x00058280
	public void initTime(int time, bool isText)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.time = time;
		this.coutTime = time;
		this.isText = isText;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0005A0CC File Offset: 0x000582CC
	public static bool isExistItem(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0005A120 File Offset: 0x00058320
	public static ItemTime getMessageById(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0005A174 File Offset: 0x00058374
	public static bool isExistMessage(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0005A1C8 File Offset: 0x000583C8
	public static ItemTime getItemById(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0005A21C File Offset: 0x0005841C
	public void initTime(int time)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.coutTime = time;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0005A25C File Offset: 0x0005845C
	public void paint(mGraphics g, int x, int y)
	{
		SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
		string st = string.Empty;
		st = this.minute.ToString() + "'";
		bool flag = this.minute == 0;
		if (flag)
		{
			st = this.second.ToString() + "s";
		}
		mFont.tahoma_7b_white.drawString(g, st, x, y + 15, 2, mFont.tahoma_7b_dark);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0005A2D4 File Offset: 0x000584D4
	public void paintText(mGraphics g, int x, int y)
	{
		bool flag = this.isPaint_coolDownBar;
		if (flag)
		{
			bool flag2 = global::Char.myCharz() != null;
			if (flag2)
			{
				int num = 80;
				int x2 = GameCanvas.w / 2 - num / 2;
				int y2 = GameCanvas.h - 80;
				g.setColor(8421504);
				g.fillRect(x2, y2, num, 2);
				g.setColor(16777215);
				bool flag3 = this.per > 0;
				if (flag3)
				{
					g.fillRect(x2, y2, num * this.per / 100, 2);
				}
			}
		}
		else
		{
			string str = string.Empty;
			str = this.minute.ToString() + "'";
			bool flag4 = this.minute < 1;
			if (flag4)
			{
				str = this.second.ToString() + "s";
			}
			bool flag5 = this.minute < 0;
			if (flag5)
			{
				str = string.Empty;
			}
			bool flag6 = this.dontClear;
			if (flag6)
			{
				str = string.Empty;
			}
			mFont.tahoma_7b_white.drawString(g, this.text + " " + str, x, y, 0, mFont.tahoma_7b_dark);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0005A400 File Offset: 0x00058600
	public void update()
	{
		this.curr = mSystem.currentTimeMillis();
		bool flag = this.curr - this.last >= 1000L;
		if (flag)
		{
			this.last = mSystem.currentTimeMillis();
			this.second--;
			this.coutTime--;
			bool flag2 = this.second <= 0;
			if (flag2)
			{
				this.second = 60;
				this.minute--;
			}
			bool flag3 = this.time > 0;
			if (flag3)
			{
				this.per = this.coutTime * 100 / this.time;
			}
		}
		bool flag4 = this.minute < 0 && !this.isText;
		if (flag4)
		{
			global::Char.vItemTime.removeElement(this);
		}
		bool flag5 = this.minute < 0 && this.isText && !this.dontClear;
		if (flag5)
		{
			GameScr.textTime.removeElement(this);
		}
	}

	// Token: 0x040009CA RID: 2506
	public short idIcon;

	// Token: 0x040009CB RID: 2507
	public int second;

	// Token: 0x040009CC RID: 2508
	public int minute;

	// Token: 0x040009CD RID: 2509
	private long curr;

	// Token: 0x040009CE RID: 2510
	private long last;

	// Token: 0x040009CF RID: 2511
	private bool isText;

	// Token: 0x040009D0 RID: 2512
	private bool dontClear;

	// Token: 0x040009D1 RID: 2513
	private string text;

	// Token: 0x040009D2 RID: 2514
	private bool isPaint_coolDownBar;

	// Token: 0x040009D3 RID: 2515
	public int time;

	// Token: 0x040009D4 RID: 2516
	public int coutTime;

	// Token: 0x040009D5 RID: 2517
	private int per = 100;
}
