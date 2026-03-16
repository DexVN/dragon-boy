using System;

// Token: 0x0200009F RID: 159
public class BigBoss2 : Mob, IMapObject
{
	// Token: 0x060005E3 RID: 1507 RVA: 0x0005AD98 File Offset: 0x00059198
	public BigBoss2(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		if (BigBoss2.shadowBig == null)
		{
			BigBoss2.shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");
		}
		this.mobId = id;
		this.xTo = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yTo = (int)py;
		this.yFirst = (int)py;
		this.hp = hp;
		this.maxHp = maxHp;
		this.templateId = templateID;
		this.w_hp_bar = 100;
		this.h_hp_bar = 6;
		this.len = this.w_hp_bar;
		base.updateHp_bar();
		this.getDataB();
		this.status = 2;
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0005AF1C File Offset: 0x0005931C
	public void getDataB()
	{
		BigBoss2.data = null;
		BigBoss2.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			109,
			"/data"
		});
		try
		{
			BigBoss2.data.readData2(patch);
			BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109 + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BigBoss2.data.width;
		this.h = BigBoss2.data.height;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0005AFF0 File Offset: 0x000593F0
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0005B000 File Offset: 0x00059400
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0005B00C File Offset: 0x0005940C
	public new static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			string text = (string)Mob.newMob.elementAt(i);
			if (text.Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0005B054 File Offset: 0x00059454
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0005B08C File Offset: 0x0005948C
	private void updateShadown()
	{
		int num = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		if (this.ySd <= 0)
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) == 0)
		{
			this.isOutMap = true;
		}
		else if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			this.xSd = this.x;
			this.ySd = this.y;
			this.isOutMap = false;
		}
		while (this.isOutMap && this.wCount < 10)
		{
			this.wCount++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
				}
				return;
			}
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0005B1C4 File Offset: 0x000595C4
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0005B218 File Offset: 0x00059618
	public new void updateSuperEff()
	{
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0005B21C File Offset: 0x0005961C
	public override void update()
	{
		if (!this.isUpdate())
		{
			return;
		}
		this.updateShadown();
		switch (this.status)
		{
		case 0:
		case 1:
			this.updateDead();
			break;
		case 2:
			this.updateMobStandWait();
			break;
		case 3:
			this.updateMobAttack();
			break;
		case 4:
			this.timeStatus = 0;
			this.updateMobFly();
			break;
		case 5:
			this.timeStatus = 0;
			this.updateMobWalk();
			break;
		case 6:
			this.timeStatus = 0;
			this.p1++;
			this.y += this.p1;
			if (this.y >= this.yFirst)
			{
				this.y = this.yFirst;
				this.p1 = 0;
				this.status = 5;
			}
			break;
		case 7:
			this.updateInjure();
			break;
		}
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0005B318 File Offset: 0x00059718
	private void updateDead()
	{
		this.checkFrameTick(this.stand);
		if (GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
		}
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0005B3E8 File Offset: 0x000597E8
	private void updateMobFly()
	{
		if (this.flyUp)
		{
			this.dy++;
			this.y -= this.dy;
			this.checkFrameTick(this.fly);
			if (this.y <= -500)
			{
				this.flyUp = false;
				this.flyDown = true;
				this.dy = 0;
			}
		}
		if (this.flyDown)
		{
			this.x = this.xTo;
			this.dy += 2;
			this.y += this.dy;
			this.checkFrameTick(this.hitground);
			if (this.y > this.yFirst)
			{
				this.y = this.yFirst;
				this.flyDown = false;
				this.dy = 0;
				this.status = 2;
				GameScr.shock_scr = 10;
				this.shock = true;
			}
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0005B4D6 File Offset: 0x000598D6
	public new void setInjure()
	{
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0005B4D8 File Offset: 0x000598D8
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((cFocus.cx <= this.x) ? -1 : 1);
		int cx = cFocus.cx;
		int cy = cFocus.cy;
		if (Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2)
		{
			if (this.x < cx)
			{
				this.x = cx - this.w;
			}
			else
			{
				this.x = cx + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0005B5B6 File Offset: 0x000599B6
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0005B5EF File Offset: 0x000599EF
	private void updateInjure()
	{
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0005B5F4 File Offset: 0x000599F4
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0005B667 File Offset: 0x00059A67
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0005B677 File Offset: 0x00059A77
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.status = 3;
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.tick = 0;
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0005B69C File Offset: 0x00059A9C
	public new void updateMobAttack()
	{
		if ((int)this.type == 0)
		{
			if (this.tick == this.attack1.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack1);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.y += (this.charAttack[0].cy - this.y) / 4;
			this.xTo = this.x;
			if (this.tick == 8)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}
		if ((int)this.type == 1)
		{
			if (this.tick == this.attack2.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack2);
			if (this.tick == 8)
			{
				for (int j = 0; j < this.charAttack.Length; j++)
				{
					MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 25, true, this.dameHP[j], 0, this.charAttack[j], 24);
				}
			}
		}
		if ((int)this.type == 2)
		{
			if (this.tick == this.fly.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.fly);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.xTo = this.x;
			this.yTo = this.y;
			if (this.tick == 12)
			{
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
				}
			}
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0005B965 File Offset: 0x00059D65
	public new void updateMobWalk()
	{
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0005B968 File Offset: 0x00059D68
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0005B9DA File Offset: 0x00059DDA
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0005B9EA File Offset: 0x00059DEA
	public new bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0005BA08 File Offset: 0x00059E08
	public override void paint(mGraphics g)
	{
		if (BigBoss2.data == null)
		{
			return;
		}
		if (this.isHide)
		{
			return;
		}
		if (this.isMafuba)
		{
			if (!this.changBody)
			{
				BigBoss2.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			return;
		}
		if (this.isShadown && this.status != 0)
		{
			this.paintShadow(g);
		}
		g.translate(0, GameCanvas.transY);
		if (!this.changBody)
		{
			BigBoss2.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		g.translate(0, -GameCanvas.transY);
		int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
		int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
		int num = imageWidth;
		int num2 = this.x - imageWidth;
		int y = this.y - this.h - 5;
		int num3 = imageWidth * 2 * this.per / 100;
		int num4;
		if (num3 > num)
		{
			num4 = num3 - num;
			if (num4 <= 0)
			{
				num4 = 0;
			}
		}
		else
		{
			num = num3;
			num4 = 0;
		}
		g.drawImage(GameScr.imgHP_tm_xam, num2, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawImage(GameScr.imgHP_tm_xam, num2 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, num2, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(this.imgHPtem, 0, 0, num4, imageHeight, 0, num2 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
		if (this.shock)
		{
			this.tShock++;
			Effect me = new Effect(((int)this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1);
			EffecMn.addEff(me);
			Effect me2 = new Effect(((int)this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1);
			EffecMn.addEff(me2);
			if (this.tShock == 50)
			{
				this.tShock = 0;
				this.shock = false;
			}
		}
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0005BCFF File Offset: 0x0005A0FF
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0005BD06 File Offset: 0x0005A106
	public new void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0005BD40 File Offset: 0x0005A140
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x <= this.x) ? -1 : 1);
		int x = mobToAttack.x;
		int y = mobToAttack.y;
		if (Res.abs(x - this.x) < this.w * 2 && Res.abs(y - this.y) < this.h * 2)
		{
			if (this.x < x)
			{
				this.x = x - this.w;
			}
			else
			{
				this.x = x + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0005BE1E File Offset: 0x0005A21E
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0005BE26 File Offset: 0x0005A226
	public new int getY()
	{
		return this.y - 50;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0005BE31 File Offset: 0x0005A231
	public new int getH()
	{
		return 40;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0005BE35 File Offset: 0x0005A235
	public new int getW()
	{
		return 50;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0005BE3C File Offset: 0x0005A23C
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0005BE7D File Offset: 0x0005A27D
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0005BE96 File Offset: 0x0005A296
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0005BEAA File Offset: 0x0005A2AA
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0005BEB3 File Offset: 0x0005A2B3
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x04000A82 RID: 2690
	public static Image shadowBig;

	// Token: 0x04000A83 RID: 2691
	public static EffectData data;

	// Token: 0x04000A84 RID: 2692
	public int xTo;

	// Token: 0x04000A85 RID: 2693
	public int yTo;

	// Token: 0x04000A86 RID: 2694
	public bool haftBody;

	// Token: 0x04000A87 RID: 2695
	public bool change;

	// Token: 0x04000A88 RID: 2696
	private Mob mob1;

	// Token: 0x04000A89 RID: 2697
	public new int xSd;

	// Token: 0x04000A8A RID: 2698
	public new int ySd;

	// Token: 0x04000A8B RID: 2699
	private bool isOutMap;

	// Token: 0x04000A8C RID: 2700
	private int wCount;

	// Token: 0x04000A8D RID: 2701
	public new bool isShadown = true;

	// Token: 0x04000A8E RID: 2702
	private int tick;

	// Token: 0x04000A8F RID: 2703
	private int frame;

	// Token: 0x04000A90 RID: 2704
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000A91 RID: 2705
	private bool wy;

	// Token: 0x04000A92 RID: 2706
	private int wt;

	// Token: 0x04000A93 RID: 2707
	private int fy;

	// Token: 0x04000A94 RID: 2708
	private int ty;

	// Token: 0x04000A95 RID: 2709
	public new int typeSuperEff;

	// Token: 0x04000A96 RID: 2710
	private global::Char focus;

	// Token: 0x04000A97 RID: 2711
	private int timeDead;

	// Token: 0x04000A98 RID: 2712
	private bool flyUp;

	// Token: 0x04000A99 RID: 2713
	private bool flyDown;

	// Token: 0x04000A9A RID: 2714
	private int dy;

	// Token: 0x04000A9B RID: 2715
	public bool changePos;

	// Token: 0x04000A9C RID: 2716
	private int tShock;

	// Token: 0x04000A9D RID: 2717
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000A9E RID: 2718
	private int tA;

	// Token: 0x04000A9F RID: 2719
	private global::Char[] charAttack;

	// Token: 0x04000AA0 RID: 2720
	private int[] dameHP;

	// Token: 0x04000AA1 RID: 2721
	private sbyte type;

	// Token: 0x04000AA2 RID: 2722
	public new int[] stand = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000AA3 RID: 2723
	public new int[] move = new int[]
	{
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		2,
		2,
		2
	};

	// Token: 0x04000AA4 RID: 2724
	public new int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04000AA5 RID: 2725
	public new int[] attack1 = new int[]
	{
		0,
		0,
		0,
		7,
		7,
		7,
		8,
		8,
		8,
		9,
		9,
		9
	};

	// Token: 0x04000AA6 RID: 2726
	public new int[] attack2 = new int[]
	{
		0,
		0,
		0,
		10,
		10,
		10,
		11,
		11,
		11,
		12,
		12,
		12
	};

	// Token: 0x04000AA7 RID: 2727
	public int[] attack3 = new int[]
	{
		0,
		0,
		1,
		1,
		4,
		4,
		6,
		6,
		8,
		8,
		25,
		25,
		26,
		26,
		28,
		28,
		30,
		30,
		32,
		32,
		2,
		2,
		1,
		1
	};

	// Token: 0x04000AA8 RID: 2728
	public int[] fly = new int[]
	{
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6,
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x04000AA9 RID: 2729
	public int[] hitground = new int[]
	{
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x04000AAA RID: 2730
	private bool shock;

	// Token: 0x04000AAB RID: 2731
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000AAC RID: 2732
	public new global::Char injureBy;

	// Token: 0x04000AAD RID: 2733
	public new bool injureThenDie;

	// Token: 0x04000AAE RID: 2734
	public new Mob mobToAttack;

	// Token: 0x04000AAF RID: 2735
	public new int forceWait;

	// Token: 0x04000AB0 RID: 2736
	public new bool blindEff;

	// Token: 0x04000AB1 RID: 2737
	public new bool sleepEff;
}
