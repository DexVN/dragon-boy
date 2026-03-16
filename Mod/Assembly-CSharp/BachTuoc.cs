using System;

// Token: 0x0200009C RID: 156
public class BachTuoc : Mob, IMapObject
{
	// Token: 0x06000593 RID: 1427 RVA: 0x0005896C File Offset: 0x00056D6C
	public BachTuoc(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		this.mobId = id;
		this.xFirst = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yFirst = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		this.w_hp_bar = 100;
		this.h_hp_bar = 6;
		this.len = this.w_hp_bar;
		base.updateHp_bar();
		this.getDataB();
		this.status = 2;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00058AA0 File Offset: 0x00056EA0
	public void getDataB()
	{
		BachTuoc.data = null;
		BachTuoc.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			108,
			"/data"
		});
		try
		{
			BachTuoc.data.readData2(patch);
			BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108 + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BachTuoc.data.width;
		this.h = BachTuoc.data.height;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00058B74 File Offset: 0x00056F74
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00058B84 File Offset: 0x00056F84
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00058B90 File Offset: 0x00056F90
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

	// Token: 0x06000598 RID: 1432 RVA: 0x00058BD8 File Offset: 0x00056FD8
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00058C10 File Offset: 0x00057010
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

	// Token: 0x0600059A RID: 1434 RVA: 0x00058D48 File Offset: 0x00057148
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00058D9C File Offset: 0x0005719C
	public new void updateSuperEff()
	{
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00058DA0 File Offset: 0x000571A0
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

	// Token: 0x0600059D RID: 1437 RVA: 0x00058E88 File Offset: 0x00057288
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

	// Token: 0x0600059E RID: 1438 RVA: 0x00058F56 File Offset: 0x00057356
	public new void setInjure()
	{
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00058F58 File Offset: 0x00057358
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

	// Token: 0x060005A0 RID: 1440 RVA: 0x00059036 File Offset: 0x00057436
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0005906F File Offset: 0x0005746F
	private void updateInjure()
	{
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00059074 File Offset: 0x00057474
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x000590E7 File Offset: 0x000574E7
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x000590F7 File Offset: 0x000574F7
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.status = 3;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00059118 File Offset: 0x00057518
	public new void updateMobAttack()
	{
		if ((int)this.type == 3)
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
		if ((int)this.type == 4)
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
					this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
				}
			}
		}
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x000592EC File Offset: 0x000576EC
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.movee);
		this.x += ((this.x >= this.xTo) ? -2 : 2);
		this.y = this.yTo;
		this.dir = ((this.x >= this.xTo) ? -1 : 1);
		if (Res.abs(this.x - this.xTo) <= 1)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00059380 File Offset: 0x00057780
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x000593F2 File Offset: 0x000577F2
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00059402 File Offset: 0x00057802
	public new bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00059420 File Offset: 0x00057820
	public override void paint(mGraphics g)
	{
		if (BachTuoc.data == null)
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
				BachTuoc.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
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
			BachTuoc.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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

	// Token: 0x060005AB RID: 1451 RVA: 0x00059717 File Offset: 0x00057B17
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0005971E File Offset: 0x00057B1E
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

	// Token: 0x060005AD RID: 1453 RVA: 0x00059758 File Offset: 0x00057B58
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

	// Token: 0x060005AE RID: 1454 RVA: 0x00059836 File Offset: 0x00057C36
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0005983E File Offset: 0x00057C3E
	public new int getY()
	{
		return this.y - 40;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00059849 File Offset: 0x00057C49
	public new int getH()
	{
		return 40;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0005984D File Offset: 0x00057C4D
	public new int getW()
	{
		return 40;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00059854 File Offset: 0x00057C54
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00059895 File Offset: 0x00057C95
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000598AE File Offset: 0x00057CAE
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x000598C2 File Offset: 0x00057CC2
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x000598CB File Offset: 0x00057CCB
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x000598D4 File Offset: 0x00057CD4
	public new void move(short xMoveTo)
	{
		this.xTo = (int)xMoveTo;
		this.status = 5;
	}

	// Token: 0x04000A19 RID: 2585
	public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000A1A RID: 2586
	public static EffectData data;

	// Token: 0x04000A1B RID: 2587
	public int xTo;

	// Token: 0x04000A1C RID: 2588
	public int yTo;

	// Token: 0x04000A1D RID: 2589
	public bool haftBody;

	// Token: 0x04000A1E RID: 2590
	public bool change;

	// Token: 0x04000A1F RID: 2591
	private Mob mob1;

	// Token: 0x04000A20 RID: 2592
	public new int xSd;

	// Token: 0x04000A21 RID: 2593
	public new int ySd;

	// Token: 0x04000A22 RID: 2594
	private bool isOutMap;

	// Token: 0x04000A23 RID: 2595
	private int wCount;

	// Token: 0x04000A24 RID: 2596
	public new bool isShadown = true;

	// Token: 0x04000A25 RID: 2597
	private int tick;

	// Token: 0x04000A26 RID: 2598
	private int frame;

	// Token: 0x04000A27 RID: 2599
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000A28 RID: 2600
	private bool wy;

	// Token: 0x04000A29 RID: 2601
	private int wt;

	// Token: 0x04000A2A RID: 2602
	private int fy;

	// Token: 0x04000A2B RID: 2603
	private int ty;

	// Token: 0x04000A2C RID: 2604
	public new int typeSuperEff;

	// Token: 0x04000A2D RID: 2605
	private global::Char focus;

	// Token: 0x04000A2E RID: 2606
	private bool flyUp;

	// Token: 0x04000A2F RID: 2607
	private bool flyDown;

	// Token: 0x04000A30 RID: 2608
	private int dy;

	// Token: 0x04000A31 RID: 2609
	public bool changePos;

	// Token: 0x04000A32 RID: 2610
	private int tShock;

	// Token: 0x04000A33 RID: 2611
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000A34 RID: 2612
	private int tA;

	// Token: 0x04000A35 RID: 2613
	private global::Char[] charAttack;

	// Token: 0x04000A36 RID: 2614
	private int[] dameHP;

	// Token: 0x04000A37 RID: 2615
	private sbyte type;

	// Token: 0x04000A38 RID: 2616
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

	// Token: 0x04000A39 RID: 2617
	public int[] movee = new int[]
	{
		0,
		0,
		0,
		2,
		2,
		2,
		3,
		3,
		3,
		4,
		4,
		4
	};

	// Token: 0x04000A3A RID: 2618
	public new int[] attack1 = new int[]
	{
		0,
		0,
		0,
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6
	};

	// Token: 0x04000A3B RID: 2619
	public new int[] attack2 = new int[]
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
		9,
		10,
		10,
		10,
		11,
		11
	};

	// Token: 0x04000A3C RID: 2620
	public new int[] hurt = new int[]
	{
		1,
		1,
		7,
		7
	};

	// Token: 0x04000A3D RID: 2621
	private bool shock;

	// Token: 0x04000A3E RID: 2622
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000A3F RID: 2623
	public new global::Char injureBy;

	// Token: 0x04000A40 RID: 2624
	public new bool injureThenDie;

	// Token: 0x04000A41 RID: 2625
	public new Mob mobToAttack;

	// Token: 0x04000A42 RID: 2626
	public new int forceWait;

	// Token: 0x04000A43 RID: 2627
	public new bool blindEff;

	// Token: 0x04000A44 RID: 2628
	public new bool sleepEff;
}
