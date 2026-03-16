using System;

// Token: 0x020000B0 RID: 176
public class Info_RadaScr
{
	// Token: 0x060007D5 RID: 2005 RVA: 0x00071478 File Offset: 0x0006F878
	public void SetInfo(int id, int no, int idIcon, sbyte rank, sbyte typeMonster, short templateId, string name, string info, global::Char charInfo, ItemOption[] itemOption)
	{
		this.id = id;
		this.no = no;
		this.idIcon = idIcon;
		this.rank = rank;
		this.typeMonster = typeMonster;
		if (templateId != -1)
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

	// Token: 0x060007D6 RID: 2006 RVA: 0x000714F0 File Offset: 0x0006F8F0
	public void SetAmount(sbyte amount, sbyte max_amount)
	{
		this.amount = amount;
		this.max_amount = max_amount;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00071500 File Offset: 0x0006F900
	public void SetLevel(sbyte level)
	{
		this.level = level;
		this.addItemDetail();
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0007150F File Offset: 0x0006F90F
	public void SetUse(sbyte isUse)
	{
		this.isUse = isUse;
		this.addItemDetail();
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00071520 File Offset: 0x0006F920
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

	// Token: 0x060007DA RID: 2010 RVA: 0x00071550 File Offset: 0x0006F950
	public static Info_RadaScr GetInfo(MyVector vec, int id)
	{
		if (vec != null)
		{
			for (int i = 0; i < vec.size(); i++)
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)vec.elementAt(i);
				if (info_RadaScr != null && info_RadaScr.id == id)
				{
					return info_RadaScr;
				}
			}
		}
		return null;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0007159C File Offset: 0x0006F99C
	public void paintInfo(mGraphics g, int x, int y)
	{
		this.count++;
		if (this.count > this.f.Length - 1)
		{
			this.count = 0;
		}
		if ((int)this.typeMonster == 0)
		{
			if (Mob.arrMobTemplate[this.mobInfo.templateId] != null)
			{
				if (Mob.arrMobTemplate[this.mobInfo.templateId].data != null)
				{
					Mob.arrMobTemplate[this.mobInfo.templateId].data.paintFrame(g, this.f[this.count], x, y, 0, 0);
				}
				else if (this.timeRequest - GameCanvas.timeNow < 0L)
				{
					this.timeRequest = GameCanvas.timeNow + 1500L;
					this.mobInfo.getData();
				}
			}
		}
		else if (this.charInfo != null)
		{
			this.charInfo.paintCharBody(g, x, y, 1, this.f[this.count], true);
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x000716A0 File Offset: 0x0006FAA0
	public void addItemDetail()
	{
		this.cp = new ChatPopup();
		string text = string.Empty;
		string text2 = string.Empty;
		text2 = text2 + "\n|6|" + this.info;
		text2 += "\n--";
		if (this.itemOption != null)
		{
			int num = 0;
			bool flag = true;
			while (flag)
			{
				int num2 = 0;
				for (int i = 0; i < this.itemOption.Length; i++)
				{
					text = this.itemOption[i].getOptionString();
					if (!text.Equals(string.Empty) && num == (int)this.itemOption[i].activeCard)
					{
						num2++;
						break;
					}
				}
				if (num2 == 0)
				{
					break;
				}
				if (num == 0)
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
					if (!text.Equals(string.Empty) && num == (int)this.itemOption[j].activeCard)
					{
						string text4 = "1";
						if ((int)this.level == 0)
						{
							text4 = "2";
						}
						else if ((int)this.itemOption[j].activeCard != 0)
						{
							if ((int)this.isUse == 0)
							{
								text4 = "2";
							}
							else if ((int)this.level < (int)this.itemOption[j].activeCard)
							{
								text4 = "2";
							}
						}
						string text3 = text2;
						text2 = string.Concat(new string[]
						{
							text3,
							"\n|",
							text4,
							"|1|",
							text
						});
					}
				}
				if (num2 != 0)
				{
					num++;
				}
			}
		}
		this.popUpDetailInit(this.cp, text2);
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x000718CC File Offset: 0x0006FCCC
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
		if (cp.lim < 0)
		{
			cp.lim = 0;
		}
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00071964 File Offset: 0x0006FD64
	public void SetEff()
	{
		if ((int)this.amount == (int)this.max_amount && this.eff.size() == 0)
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

	// Token: 0x060007DF RID: 2015 RVA: 0x000719FC File Offset: 0x0006FDFC
	public void paintEff(mGraphics g, int x, int y)
	{
		this.SetEff();
		for (int i = 0; i < this.eff.size(); i++)
		{
			Position position = (Position)this.eff.elementAt(i);
			if (position != null)
			{
				if (position.w < position.v)
				{
					position.w++;
				}
				if (position.w >= position.v)
				{
					position.anchor = GameCanvas.gameTick / 3 % (RadarScr.fraEff.nFrame + 1);
					if (position.anchor >= RadarScr.fraEff.nFrame)
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

	// Token: 0x04000ED8 RID: 3800
	public const sbyte TYPE_MONSTER = 0;

	// Token: 0x04000ED9 RID: 3801
	public const sbyte TYPE_CHARPART = 1;

	// Token: 0x04000EDA RID: 3802
	public sbyte rank;

	// Token: 0x04000EDB RID: 3803
	public sbyte amount;

	// Token: 0x04000EDC RID: 3804
	public sbyte max_amount;

	// Token: 0x04000EDD RID: 3805
	public sbyte typeMonster;

	// Token: 0x04000EDE RID: 3806
	public int id;

	// Token: 0x04000EDF RID: 3807
	public int no;

	// Token: 0x04000EE0 RID: 3808
	public int idIcon;

	// Token: 0x04000EE1 RID: 3809
	public string name;

	// Token: 0x04000EE2 RID: 3810
	public string info;

	// Token: 0x04000EE3 RID: 3811
	public sbyte level;

	// Token: 0x04000EE4 RID: 3812
	public sbyte isUse;

	// Token: 0x04000EE5 RID: 3813
	public global::Char charInfo;

	// Token: 0x04000EE6 RID: 3814
	public Mob mobInfo;

	// Token: 0x04000EE7 RID: 3815
	public ItemOption[] itemOption;

	// Token: 0x04000EE8 RID: 3816
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

	// Token: 0x04000EE9 RID: 3817
	private int count;

	// Token: 0x04000EEA RID: 3818
	private long timeRequest;

	// Token: 0x04000EEB RID: 3819
	public ChatPopup cp;

	// Token: 0x04000EEC RID: 3820
	public MyVector eff = new MyVector(string.Empty);
}
