using System;

// Token: 0x02000053 RID: 83
public class Item
{
	// Token: 0x06000458 RID: 1112 RVA: 0x0005867A File Offset: 0x0005687A
	public void getCompare()
	{
		this.compare = GameCanvas.panel.getCompare(this);
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00058690 File Offset: 0x00056890
	public string getPrice()
	{
		string result = string.Empty;
		bool flag = this.buyCoin <= 0 && this.buyGold <= 0;
		string result2;
		if (flag)
		{
			result2 = null;
		}
		else
		{
			bool flag2 = this.buyCoin > 0 && this.buyGold <= 0;
			if (flag2)
			{
				result = this.buyCoin.ToString() + mResources.XU;
			}
			else
			{
				bool flag3 = this.buyGold > 0 && this.buyCoin <= 0;
				if (flag3)
				{
					result = this.buyGold.ToString() + mResources.LUONG;
				}
				else
				{
					bool flag4 = this.buyCoin > 0 && this.buyGold > 0;
					if (flag4)
					{
						result = string.Concat(new object[]
						{
							this.buyCoin,
							mResources.XU,
							"/",
							this.buyGold,
							mResources.LUONG
						});
					}
				}
			}
			result2 = result;
		}
		return result2;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0005879C File Offset: 0x0005699C
	public void paintUpgradeEffect(int x, int y, int upgrade, mGraphics g)
	{
		int num = GameScr.indexSize - 2;
		int num2 = 0;
		int num3 = (upgrade >= 4) ? ((upgrade >= 8) ? ((upgrade >= 12) ? ((upgrade > 14) ? 4 : 3) : 2) : 1) : 0;
		for (int i = num2; i < this.size.Length; i++)
		{
			int num4 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - i * 4);
			int num5 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - i * 4);
			g.setColor(this.colorBorder[num3][i]);
			g.fillRect(num4 - this.size[i] / 2, num5 - this.size[i] / 2, this.size[i], this.size[i]);
		}
		bool flag = upgrade == 4 || upgrade == 8;
		if (flag)
		{
			for (int j = num2; j < this.size.Length; j++)
			{
				int num6 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 2 - j * 4);
				int num7 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 2 - j * 4);
				g.setColor(this.colorBorder[num3 - 1][j]);
				g.fillRect(num6 - this.size[j] / 2, num7 - this.size[j] / 2, this.size[j], this.size[j]);
			}
		}
		bool flag2 = upgrade != 1 && upgrade != 4 && upgrade != 8;
		if (flag2)
		{
			for (int k = num2; k < this.size.Length; k++)
			{
				int num8 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 2 - k * 4);
				int num9 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 2 - k * 4);
				g.setColor(this.colorBorder[num3][k]);
				g.fillRect(num8 - this.size[k] / 2, num9 - this.size[k] / 2, this.size[k], this.size[k]);
			}
		}
		bool flag3 = upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9;
		if (flag3)
		{
			for (int l = num2; l < this.size.Length; l++)
			{
				int num10 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num - l * 4);
				int num11 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num - l * 4);
				g.setColor(this.colorBorder[num3][l]);
				g.fillRect(num10 - this.size[l] / 2, num11 - this.size[l] / 2, this.size[l], this.size[l]);
			}
		}
		bool flag4 = upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9 && upgrade != 13 && upgrade != 3 && upgrade != 6 && upgrade != 10 && upgrade != 15;
		if (flag4)
		{
			for (int m = num2; m < this.size.Length; m++)
			{
				int num12 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 3 - m * 4);
				int num13 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 3 - m * 4);
				g.setColor(this.colorBorder[num3][m]);
				g.fillRect(num12 - this.size[m] / 2, num13 - this.size[m] / 2, this.size[m], this.size[m]);
			}
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00058BA4 File Offset: 0x00056DA4
	private int upgradeEffectY(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		bool flag = 0 <= num2 && num2 < num;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = num <= num2 && num2 < num * 2;
			if (flag2)
			{
				result = num2 % num;
			}
			else
			{
				bool flag3 = num * 2 <= num2 && num2 < num * 3;
				if (flag3)
				{
					result = num;
				}
				else
				{
					result = num - num2 % num;
				}
			}
		}
		return result;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00058C14 File Offset: 0x00056E14
	private int upgradeEffectX(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		bool flag = 0 <= num2 && num2 < num;
		int result;
		if (flag)
		{
			result = num2 % num;
		}
		else
		{
			bool flag2 = num <= num2 && num2 < num * 2;
			if (flag2)
			{
				result = num;
			}
			else
			{
				bool flag3 = num * 2 <= num2 && num2 < num * 3;
				if (flag3)
				{
					result = num - num2 % num;
				}
				else
				{
					result = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00058C84 File Offset: 0x00056E84
	public bool isHaveOption(int id)
	{
		for (int i = 0; i < this.itemOption.Length; i++)
		{
			ItemOption itemOption = this.itemOption[i];
			bool flag = itemOption != null && itemOption.optionTemplate.id == id;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00058CD8 File Offset: 0x00056ED8
	public Item clone()
	{
		Item item = new Item();
		item.template = this.template;
		bool flag = this.options != null;
		if (flag)
		{
			item.options = new MyVector();
			for (int i = 0; i < this.options.size(); i++)
			{
				ItemOption itemOption = new ItemOption();
				itemOption.optionTemplate = ((ItemOption)this.options.elementAt(i)).optionTemplate;
				itemOption.param = ((ItemOption)this.options.elementAt(i)).param;
				item.options.addElement(itemOption);
			}
		}
		item.itemId = this.itemId;
		item.playerId = this.playerId;
		item.indexUI = this.indexUI;
		item.quantity = this.quantity;
		item.isLock = this.isLock;
		item.sys = this.sys;
		item.upgrade = this.upgrade;
		item.buyCoin = this.buyCoin;
		item.buyCoinLock = this.buyCoinLock;
		item.buyGold = this.buyGold;
		item.buyGoldLock = this.buyGoldLock;
		item.saleCoinLock = this.saleCoinLock;
		item.typeUI = this.typeUI;
		item.isExpires = this.isExpires;
		return item;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00058E2C File Offset: 0x0005702C
	public bool isTypeBody()
	{
		return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00058E9C File Offset: 0x0005709C
	public string getLockstring()
	{
		return (!this.isLock) ? mResources.NOLOCK : mResources.LOCKED;
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00058EC4 File Offset: 0x000570C4
	public string getUpgradestring()
	{
		bool flag = this.template.level < 10 || this.template.type >= 10;
		string result;
		if (flag)
		{
			result = mResources.NOTUPGRADE;
		}
		else
		{
			bool flag2 = this.upgrade == 0;
			if (flag2)
			{
				result = mResources.NOUPGRADE;
			}
			else
			{
				result = null;
			}
		}
		return result;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00058F20 File Offset: 0x00057120
	public bool isTypeUIMe()
	{
		return this.typeUI == 5 || this.typeUI == 3 || this.typeUI == 4;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00058F50 File Offset: 0x00057150
	public bool isTypeUIShopView()
	{
		return this.isTypeUIShop() || this.isTypeUIStore() || this.isTypeUIBook() || this.isTypeUIFashion();
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00058F88 File Offset: 0x00057188
	public bool isTypeUIShop()
	{
		return this.typeUI == 20 || this.typeUI == 21 || this.typeUI == 22 || this.typeUI == 23 || this.typeUI == 24 || this.typeUI == 25 || this.typeUI == 26 || this.typeUI == 27 || this.typeUI == 28 || this.typeUI == 29 || this.typeUI == 16 || this.typeUI == 17 || this.typeUI == 18 || this.typeUI == 19 || this.typeUI == 2 || this.typeUI == 6 || this.typeUI == 8;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00059050 File Offset: 0x00057250
	public bool isTypeUIShopLock()
	{
		return this.typeUI == 7 || this.typeUI == 9;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00059078 File Offset: 0x00057278
	public bool isTypeUIStore()
	{
		return this.typeUI == 14;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00059094 File Offset: 0x00057294
	public bool isTypeUIBook()
	{
		return this.typeUI == 15;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x000590B0 File Offset: 0x000572B0
	public bool isTypeUIFashion()
	{
		return this.typeUI == 32;
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x000590CC File Offset: 0x000572CC
	public bool isUpMax()
	{
		return this.getUpMax() == this.upgrade;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x000590EC File Offset: 0x000572EC
	public int getUpMax()
	{
		bool flag = this.template.level >= 1 && this.template.level < 20;
		int result;
		if (flag)
		{
			result = 4;
		}
		else
		{
			bool flag2 = this.template.level >= 20 && this.template.level < 40;
			if (flag2)
			{
				result = 8;
			}
			else
			{
				bool flag3 = this.template.level >= 40 && this.template.level < 50;
				if (flag3)
				{
					result = 12;
				}
				else
				{
					bool flag4 = this.template.level >= 50 && this.template.level < 60;
					if (flag4)
					{
						result = 14;
					}
					else
					{
						result = 16;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x000591AE File Offset: 0x000573AE
	public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
	{
		this.headTemp = headTemp;
		this.bodyTemp = bodyTemp;
		this.legTemp = legTemp;
		this.bagTemp = bagTemp;
	}

	// Token: 0x0400092C RID: 2348
	public const int OPT_STAR = 34;

	// Token: 0x0400092D RID: 2349
	public const int OPT_MOON = 35;

	// Token: 0x0400092E RID: 2350
	public const int OPT_SUN = 36;

	// Token: 0x0400092F RID: 2351
	public const int OPT_COLORNAME = 41;

	// Token: 0x04000930 RID: 2352
	public const int OPT_LVITEM = 72;

	// Token: 0x04000931 RID: 2353
	public const int OPT_STARSLOT = 102;

	// Token: 0x04000932 RID: 2354
	public const int OPT_MAXSTARSLOT = 107;

	// Token: 0x04000933 RID: 2355
	public const int TYPE_BODY_MIN = 0;

	// Token: 0x04000934 RID: 2356
	public const int TYPE_BODY_MAX = 6;

	// Token: 0x04000935 RID: 2357
	public const int TYPE_AO = 0;

	// Token: 0x04000936 RID: 2358
	public const int TYPE_QUAN = 1;

	// Token: 0x04000937 RID: 2359
	public const int TYPE_GANGTAY = 2;

	// Token: 0x04000938 RID: 2360
	public const int TYPE_GIAY = 3;

	// Token: 0x04000939 RID: 2361
	public const int TYPE_RADA = 4;

	// Token: 0x0400093A RID: 2362
	public const int TYPE_HAIR = 5;

	// Token: 0x0400093B RID: 2363
	public const int TYPE_DAUTHAN = 6;

	// Token: 0x0400093C RID: 2364
	public const int TYPE_NGOCRONG = 12;

	// Token: 0x0400093D RID: 2365
	public const int TYPE_SACH = 7;

	// Token: 0x0400093E RID: 2366
	public const int TYPE_NHIEMVU = 8;

	// Token: 0x0400093F RID: 2367
	public const int TYPE_GOLD = 9;

	// Token: 0x04000940 RID: 2368
	public const int TYPE_DIAMOND = 10;

	// Token: 0x04000941 RID: 2369
	public const int TYPE_BALO = 11;

	// Token: 0x04000942 RID: 2370
	public const int TYPE_MOUNT = 23;

	// Token: 0x04000943 RID: 2371
	public const int TYPE_MOUNT_VIP = 24;

	// Token: 0x04000944 RID: 2372
	public const int TYPE_DIAMOND_LOCK = 34;

	// Token: 0x04000945 RID: 2373
	public const int TYPE_TRAINSUIT = 32;

	// Token: 0x04000946 RID: 2374
	public const int TYPE_HAT = 35;

	// Token: 0x04000947 RID: 2375
	public const sbyte UI_WEAPON = 2;

	// Token: 0x04000948 RID: 2376
	public const sbyte UI_BAG = 3;

	// Token: 0x04000949 RID: 2377
	public const sbyte UI_BOX = 4;

	// Token: 0x0400094A RID: 2378
	public const sbyte UI_BODY = 5;

	// Token: 0x0400094B RID: 2379
	public const sbyte UI_STACK = 6;

	// Token: 0x0400094C RID: 2380
	public const sbyte UI_STACK_LOCK = 7;

	// Token: 0x0400094D RID: 2381
	public const sbyte UI_GROCERY = 8;

	// Token: 0x0400094E RID: 2382
	public const sbyte UI_GROCERY_LOCK = 9;

	// Token: 0x0400094F RID: 2383
	public const sbyte UI_UPGRADE = 10;

	// Token: 0x04000950 RID: 2384
	public const sbyte UI_UPPEARL = 11;

	// Token: 0x04000951 RID: 2385
	public const sbyte UI_UPPEARL_LOCK = 12;

	// Token: 0x04000952 RID: 2386
	public const sbyte UI_SPLIT = 13;

	// Token: 0x04000953 RID: 2387
	public const sbyte UI_STORE = 14;

	// Token: 0x04000954 RID: 2388
	public const sbyte UI_BOOK = 15;

	// Token: 0x04000955 RID: 2389
	public const sbyte UI_LIEN = 16;

	// Token: 0x04000956 RID: 2390
	public const sbyte UI_NHAN = 17;

	// Token: 0x04000957 RID: 2391
	public const sbyte UI_NGOCBOI = 18;

	// Token: 0x04000958 RID: 2392
	public const sbyte UI_PHU = 19;

	// Token: 0x04000959 RID: 2393
	public const sbyte UI_NONNAM = 20;

	// Token: 0x0400095A RID: 2394
	public const sbyte UI_NONNU = 21;

	// Token: 0x0400095B RID: 2395
	public const sbyte UI_AONAM = 22;

	// Token: 0x0400095C RID: 2396
	public const sbyte UI_AONU = 23;

	// Token: 0x0400095D RID: 2397
	public const sbyte UI_GANGTAYNAM = 24;

	// Token: 0x0400095E RID: 2398
	public const sbyte UI_GANGTAYNU = 25;

	// Token: 0x0400095F RID: 2399
	public const sbyte UI_QUANNAM = 26;

	// Token: 0x04000960 RID: 2400
	public const sbyte UI_QUANNU = 27;

	// Token: 0x04000961 RID: 2401
	public const sbyte UI_GIAYNAM = 28;

	// Token: 0x04000962 RID: 2402
	public const sbyte UI_GIAYNU = 29;

	// Token: 0x04000963 RID: 2403
	public const sbyte UI_TRADE = 30;

	// Token: 0x04000964 RID: 2404
	public const sbyte UI_UPGRADE_GOLD = 31;

	// Token: 0x04000965 RID: 2405
	public const sbyte UI_FASHION = 32;

	// Token: 0x04000966 RID: 2406
	public const sbyte UI_CONVERT = 33;

	// Token: 0x04000967 RID: 2407
	public ItemOption[] itemOption;

	// Token: 0x04000968 RID: 2408
	public ItemTemplate template;

	// Token: 0x04000969 RID: 2409
	public MyVector options;

	// Token: 0x0400096A RID: 2410
	public int itemId;

	// Token: 0x0400096B RID: 2411
	public int playerId;

	// Token: 0x0400096C RID: 2412
	public bool isSelect;

	// Token: 0x0400096D RID: 2413
	public int indexUI;

	// Token: 0x0400096E RID: 2414
	public int quantity;

	// Token: 0x0400096F RID: 2415
	public int quantilyToBuy;

	// Token: 0x04000970 RID: 2416
	public long powerRequire;

	// Token: 0x04000971 RID: 2417
	public bool isLock;

	// Token: 0x04000972 RID: 2418
	public int sys;

	// Token: 0x04000973 RID: 2419
	public int upgrade;

	// Token: 0x04000974 RID: 2420
	public int buyCoin;

	// Token: 0x04000975 RID: 2421
	public int buyCoinLock;

	// Token: 0x04000976 RID: 2422
	public int buyGold;

	// Token: 0x04000977 RID: 2423
	public int buyGoldLock;

	// Token: 0x04000978 RID: 2424
	public int saleCoinLock;

	// Token: 0x04000979 RID: 2425
	public int buySpec;

	// Token: 0x0400097A RID: 2426
	public int buyRuby;

	// Token: 0x0400097B RID: 2427
	public short iconSpec = -1;

	// Token: 0x0400097C RID: 2428
	public sbyte buyType = -1;

	// Token: 0x0400097D RID: 2429
	public int typeUI;

	// Token: 0x0400097E RID: 2430
	public bool isExpires;

	// Token: 0x0400097F RID: 2431
	public bool isBuySpec;

	// Token: 0x04000980 RID: 2432
	public EffectCharPaint eff;

	// Token: 0x04000981 RID: 2433
	public int indexEff;

	// Token: 0x04000982 RID: 2434
	public Image img;

	// Token: 0x04000983 RID: 2435
	public string info;

	// Token: 0x04000984 RID: 2436
	public string content;

	// Token: 0x04000985 RID: 2437
	public string reason = string.Empty;

	// Token: 0x04000986 RID: 2438
	public int compare;

	// Token: 0x04000987 RID: 2439
	public sbyte isMe;

	// Token: 0x04000988 RID: 2440
	public bool newItem;

	// Token: 0x04000989 RID: 2441
	public int headTemp = -1;

	// Token: 0x0400098A RID: 2442
	public int bodyTemp = -1;

	// Token: 0x0400098B RID: 2443
	public int legTemp = -1;

	// Token: 0x0400098C RID: 2444
	public int bagTemp = -1;

	// Token: 0x0400098D RID: 2445
	public int wpTemp = -1;

	// Token: 0x0400098E RID: 2446
	public string nameNguoiKyGui = string.Empty;

	// Token: 0x0400098F RID: 2447
	private int[] color = new int[]
	{
		0,
		0,
		0,
		0,
		600841,
		600841,
		667658,
		667658,
		3346944,
		3346688,
		4199680,
		5052928,
		3276851,
		3932211,
		4587571,
		5046280,
		6684682,
		3359744
	};

	// Token: 0x04000990 RID: 2448
	private int[][] colorBorder = new int[][]
	{
		new int[]
		{
			18687,
			16869,
			15052,
			13235,
			11161,
			9344
		},
		new int[]
		{
			45824,
			39168,
			32768,
			26112,
			19712,
			13056
		},
		new int[]
		{
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			13500671,
			12058853,
			10682572,
			9371827,
			7995545,
			6684800
		},
		new int[]
		{
			16711705,
			15007767,
			13369364,
			11730962,
			10027023,
			8388621
		}
	};

	// Token: 0x04000991 RID: 2449
	private int[] size = new int[]
	{
		2,
		1,
		1,
		1,
		1,
		1
	};
}
