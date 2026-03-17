using System;

// Token: 0x0200004A RID: 74
public class Info_RadaScr
{
	// Token: 0x06000405 RID: 1029 RVA: 0x00057A18 File Offset: 0x00055C18
	public void SetInfo(int id, int no, int idIcon, sbyte rank, sbyte typeMonster, short templateId, string name, string info, global::Char charInfo, ItemOption[] itemOption)
	{
		this.id = id;
		this.no = no;
		this.idIcon = idIcon;
		this.rank = rank;
		this.typeMonster = typeMonster;
		bool flag = templateId != -1;
		if (flag)
		{
			this.mobInfo = new Mob();
			this.mobInfo.templateId = (int)templateId;
		}
		this.name = name;
		this.info = info;
		this.charInfo = charInfo;
		this.itemOption = itemOption;
		this.addItemDetail();
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00057A98 File Offset: 0x00055C98
	public void SetAmount(sbyte amount, sbyte max_amount)
	{
		this.amount = amount;
		this.max_amount = max_amount;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00057AA9 File Offset: 0x00055CA9
	public void SetLevel(sbyte level)
	{
		this.level = level;
		this.addItemDetail();
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00057ABA File Offset: 0x00055CBA
	public void SetUse(sbyte isUse)
	{
		this.isUse = isUse;
		this.addItemDetail();
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00057ACC File Offset: 0x00055CCC
	public static global::Char SetCharInfo(int head, int body, int leg, int bag)
	{
		return new global::Char
		{
			head = head,
			body = body,
			leg = leg,
			bag = bag
		};
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00057B00 File Offset: 0x00055D00
	public static Info_RadaScr GetInfo(MyVector vec, int id)
	{
		bool flag = vec != null;
		if (flag)
		{
			for (int i = 0; i < vec.size(); i++)
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)vec.elementAt(i);
				bool flag2 = info_RadaScr != null && info_RadaScr.id == id;
				if (flag2)
				{
					return info_RadaScr;
				}
			}
		}
		return null;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00057B60 File Offset: 0x00055D60
	public void paintInfo(mGraphics g, int x, int y)
	{
		this.count++;
		bool flag = this.count > this.f.Length - 1;
		if (flag)
		{
			this.count = 0;
		}
		bool flag2 = this.typeMonster == 0;
		if (flag2)
		{
			bool flag3 = Mob.arrMobTemplate[this.mobInfo.templateId] != null;
			if (flag3)
			{
				bool flag4 = Mob.arrMobTemplate[this.mobInfo.templateId].data != null;
				if (flag4)
				{
					Mob.arrMobTemplate[this.mobInfo.templateId].data.paintFrame(g, this.f[this.count], x, y, 0, 0);
				}
				else
				{
					bool flag5 = this.timeRequest - GameCanvas.timeNow < 0L;
					if (flag5)
					{
						this.timeRequest = GameCanvas.timeNow + 1500L;
						this.mobInfo.getData();
					}
				}
			}
		}
		else
		{
			bool flag6 = this.charInfo != null;
			if (flag6)
			{
				this.charInfo.paintCharBody(g, x, y, 1, this.f[this.count], true);
			}
		}
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00057C80 File Offset: 0x00055E80
	public void addItemDetail()
	{
		this.cp = new ChatPopup();
		string text = string.Empty;
		string text2 = string.Empty;
		text2 = text2 + "\n|6|" + this.info;
		text2 += "\n--";
		bool flag2 = this.itemOption != null;
		if (flag2)
		{
			int num = 0;
			bool flag = true;
			while (flag)
			{
				int num2 = 0;
				for (int i = 0; i < this.itemOption.Length; i++)
				{
					text = this.itemOption[i].getOptionString();
					bool flag3 = !text.Equals(string.Empty) && num == (int)this.itemOption[i].activeCard;
					if (flag3)
					{
						num2++;
						break;
					}
				}
				bool flag4 = num2 == 0;
				if (flag4)
				{
					break;
				}
				bool flag5 = num == 0;
				if (flag5)
				{
					text2 = text2 + "\n|6|2|--" + mResources.unlock + "--";
				}
				else
				{
					string text3 = text2;
					text2 = string.Concat(new object[]
					{
						text3,
						"\n|6|2|--",
						mResources.equip,
						" Lv.",
						num,
						"--"
					});
				}
				for (int j = 0; j < this.itemOption.Length; j++)
				{
					text = this.itemOption[j].getOptionString();
					bool flag6 = !text.Equals(string.Empty) && num == (int)this.itemOption[j].activeCard;
					if (flag6)
					{
						string text4 = "1";
						bool flag7 = this.level == 0;
						if (flag7)
						{
							text4 = "2";
						}
						else
						{
							bool flag8 = this.itemOption[j].activeCard != 0;
							if (flag8)
							{
								bool flag9 = this.isUse == 0;
								if (flag9)
								{
									text4 = "2";
								}
								else
								{
									bool flag10 = this.level < this.itemOption[j].activeCard;
									if (flag10)
									{
										text4 = "2";
									}
								}
							}
						}
						string text5 = text2;
						text2 = string.Concat(new string[]
						{
							text5,
							"\n|",
							text4,
							"|1|",
							text
						});
					}
				}
				bool flag11 = num2 != 0;
				if (flag11)
				{
					num++;
				}
			}
		}
		this.popUpDetailInit(this.cp, text2);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00057EE8 File Offset: 0x000560E8
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.sayWidth = RadarScr.wText;
		cp.cx = RadarScr.xText;
		cp.says = mFont.tahoma_7.splitFontArray(chat, cp.sayWidth - 8);
		cp.delay = 10000000;
		cp.c = null;
		cp.ch = cp.says.Length * 12;
		cp.cy = RadarScr.yText;
		cp.strY = 10;
		cp.lim = cp.ch - RadarScr.hText;
		bool flag = cp.lim < 0;
		if (flag)
		{
			cp.lim = 0;
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00057F84 File Offset: 0x00056184
	public void SetEff()
	{
		bool flag = this.amount == this.max_amount && this.eff.size() == 0;
		if (flag)
		{
			int num = Res.random(1, 5);
			for (int i = 0; i < num; i++)
			{
				Position position = new Position();
				position.x = Res.random(5, 25);
				position.y = Res.random(5, 25);
				position.v = i * Res.random(0, 8);
				position.w = 0;
				position.anchor = -1;
				this.eff.addElement(position);
			}
		}
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00058020 File Offset: 0x00056220
	public void paintEff(mGraphics g, int x, int y)
	{
		this.SetEff();
		for (int i = 0; i < this.eff.size(); i++)
		{
			Position position = (Position)this.eff.elementAt(i);
			bool flag = position != null;
			if (flag)
			{
				bool flag2 = position.w < position.v;
				if (flag2)
				{
					position.w++;
				}
				bool flag3 = position.w >= position.v;
				if (flag3)
				{
					position.anchor = GameCanvas.gameTick / 3 % (RadarScr.fraEff.nFrame + 1);
					bool flag4 = position.anchor >= RadarScr.fraEff.nFrame;
					if (flag4)
					{
						this.eff.removeElementAt(i);
						i--;
					}
					else
					{
						RadarScr.fraEff.drawFrame(position.anchor, x + position.x, y + position.y, 0, 3, g);
					}
				}
			}
		}
	}

	// Token: 0x040008F3 RID: 2291
	public const sbyte TYPE_MONSTER = 0;

	// Token: 0x040008F4 RID: 2292
	public const sbyte TYPE_CHARPART = 1;

	// Token: 0x040008F5 RID: 2293
	public sbyte rank;

	// Token: 0x040008F6 RID: 2294
	public sbyte amount;

	// Token: 0x040008F7 RID: 2295
	public sbyte max_amount;

	// Token: 0x040008F8 RID: 2296
	public sbyte typeMonster;

	// Token: 0x040008F9 RID: 2297
	public int id;

	// Token: 0x040008FA RID: 2298
	public int no;

	// Token: 0x040008FB RID: 2299
	public int idIcon;

	// Token: 0x040008FC RID: 2300
	public string name;

	// Token: 0x040008FD RID: 2301
	public string info;

	// Token: 0x040008FE RID: 2302
	public sbyte level;

	// Token: 0x040008FF RID: 2303
	public sbyte isUse;

	// Token: 0x04000900 RID: 2304
	public global::Char charInfo;

	// Token: 0x04000901 RID: 2305
	public Mob mobInfo;

	// Token: 0x04000902 RID: 2306
	public ItemOption[] itemOption;

	// Token: 0x04000903 RID: 2307
	private int[] f = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000904 RID: 2308
	private int count;

	// Token: 0x04000905 RID: 2309
	private long timeRequest;

	// Token: 0x04000906 RID: 2310
	public ChatPopup cp;

	// Token: 0x04000907 RID: 2311
	public MyVector eff = new MyVector(string.Empty);
}
