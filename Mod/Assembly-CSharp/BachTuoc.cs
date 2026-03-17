using System;

// Token: 0x02000009 RID: 9
public class BachTuoc : Mob, IMapObject
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002CC4 File Offset: 0x00000EC4
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

	// Token: 0x0600001F RID: 31 RVA: 0x00002DFC File Offset: 0x00000FFC
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
			BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108.ToString() + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BachTuoc.data.width;
		this.h = BachTuoc.data.height;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002ED4 File Offset: 0x000010D4
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002EE5 File Offset: 0x000010E5
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002EF0 File Offset: 0x000010F0
	public new static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			string text = (string)Mob.newMob.elementAt(i);
			bool flag = text.Equals(id);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002F44 File Offset: 0x00001144
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		bool flag = this.tick > array.Length - 1;
		if (flag)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002F88 File Offset: 0x00001188
	private void updateShadown()
	{
		int num = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		bool flag = this.ySd <= 0;
		if (!flag)
		{
			bool flag2 = TileMap.tileTypeAt(this.xSd, this.ySd, 2);
			if (!flag2)
			{
				bool flag3 = TileMap.tileTypeAt(this.xSd / num, this.ySd / num) == 0;
				if (flag3)
				{
					this.isOutMap = true;
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt(this.xSd / num, this.ySd / num) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2);
					if (flag4)
					{
						this.xSd = this.x;
						this.ySd = this.y;
						this.isOutMap = false;
					}
				}
				while (this.isOutMap && this.wCount < 10)
				{
					this.wCount++;
					this.ySd += 24;
					bool flag5 = TileMap.tileTypeAt(this.xSd, this.ySd, 2);
					if (flag5)
					{
						bool flag6 = this.ySd % 24 != 0;
						if (flag6)
						{
							this.ySd -= this.ySd % 24;
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000030E0 File Offset: 0x000012E0
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00003136 File Offset: 0x00001336
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000313C File Offset: 0x0000133C
	public override void update()
	{
		bool flag = !this.isUpdate();
		if (!flag)
		{
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
			{
				this.timeStatus = 0;
				this.p1++;
				this.y += this.p1;
				bool flag2 = this.y >= this.yFirst;
				if (flag2)
				{
					this.y = this.yFirst;
					this.p1 = 0;
					this.status = 5;
				}
				break;
			}
			case 7:
				this.updateInjure();
				break;
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000322C File Offset: 0x0000142C
	private void updateDead()
	{
		this.checkFrameTick(this.stand);
		bool flag = GameCanvas.gameTick % 5 == 0;
		if (flag)
		{
			ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
		}
		bool flag2 = this.x != this.xTo || this.y != this.yTo;
		if (flag2)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003136 File Offset: 0x00001336
	public new void setInjure()
	{
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003308 File Offset: 0x00001508
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
		bool flag = Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2;
		if (flag)
		{
			bool flag2 = this.x < cx;
			if (flag2)
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

	// Token: 0x0600002B RID: 43 RVA: 0x000033E8 File Offset: 0x000015E8
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003136 File Offset: 0x00001336
	private void updateInjure()
	{
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003428 File Offset: 0x00001628
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		bool flag = this.x != this.xTo || this.y != this.yTo;
		if (flag)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000034A3 File Offset: 0x000016A3
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000034B4 File Offset: 0x000016B4
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.status = 3;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000034D4 File Offset: 0x000016D4
	public new void updateMobAttack()
	{
		bool flag = this.type == 3;
		if (flag)
		{
			bool flag2 = this.tick == this.attack1.Length - 1;
			if (flag2)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack1);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.y += (this.charAttack[0].cy - this.y) / 4;
			this.xTo = this.x;
			bool flag3 = this.tick == 8;
			if (flag3)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}
		bool flag4 = this.type == 4;
		if (flag4)
		{
			bool flag5 = this.tick == this.attack2.Length - 1;
			if (flag5)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack2);
			bool flag6 = this.tick == 8;
			if (flag6)
			{
				for (int j = 0; j < this.charAttack.Length; j++)
				{
					this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000036C8 File Offset: 0x000018C8
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.movee);
		this.x += ((this.x >= this.xTo) ? -2 : 2);
		this.y = this.yTo;
		this.dir = ((this.x >= this.xTo) ? -1 : 1);
		bool flag = Res.abs(this.x - this.xTo) <= 1;
		if (flag)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00003758 File Offset: 0x00001958
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000037BC File Offset: 0x000019BC
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000037D8 File Offset: 0x000019D8
	public new bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003800 File Offset: 0x00001A00
	public override void paint(mGraphics g)
	{
		bool flag = BachTuoc.data == null;
		if (!flag)
		{
			bool isHide = this.isHide;
			if (!isHide)
			{
				bool isMafuba = this.isMafuba;
				if (isMafuba)
				{
					bool flag2 = !this.changBody;
					if (flag2)
					{
						BachTuoc.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
				else
				{
					bool flag3 = this.isShadown && this.status != 0;
					if (flag3)
					{
						this.paintShadow(g);
					}
					g.translate(0, GameCanvas.transY);
					bool flag4 = !this.changBody;
					if (flag4)
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
					bool flag5 = num3 > num;
					int num4;
					if (flag5)
					{
						num4 = num3 - num;
						bool flag6 = num4 <= 0;
						if (flag6)
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
					bool flag7 = this.shock;
					if (flag7)
					{
						this.tShock++;
						Effect me = new Effect((this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1);
						EffecMn.addEff(me);
						Effect me2 = new Effect((this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1);
						EffecMn.addEff(me2);
						bool flag8 = this.tShock == 50;
						if (flag8)
						{
							this.tShock = 0;
							this.shock = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003B1C File Offset: 0x00001D1C
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003B33 File Offset: 0x00001D33
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

	// Token: 0x06000038 RID: 56 RVA: 0x00003B70 File Offset: 0x00001D70
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
		bool flag = Res.abs(x - this.x) < this.w * 2 && Res.abs(y - this.y) < this.h * 2;
		if (flag)
		{
			bool flag2 = this.x < x;
			if (flag2)
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

	// Token: 0x06000039 RID: 57 RVA: 0x00003C50 File Offset: 0x00001E50
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003C68 File Offset: 0x00001E68
	public new int getY()
	{
		return this.y - 40;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003C84 File Offset: 0x00001E84
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003C98 File Offset: 0x00001E98
	public new int getW()
	{
		return 40;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003CAC File Offset: 0x00001EAC
	public new void stopMoving()
	{
		bool flag = this.status == 5;
		if (flag)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003CF4 File Offset: 0x00001EF4
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003D1C File Offset: 0x00001F1C
	public new void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003D40 File Offset: 0x00001F40
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003D4A File Offset: 0x00001F4A
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003D54 File Offset: 0x00001F54
	public new void move(short xMoveTo)
	{
		this.xTo = (int)xMoveTo;
		this.status = 5;
	}

	// Token: 0x04000023 RID: 35
	public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000024 RID: 36
	public static EffectData data;

	// Token: 0x04000025 RID: 37
	public int xTo;

	// Token: 0x04000026 RID: 38
	public int yTo;

	// Token: 0x04000027 RID: 39
	public bool haftBody;

	// Token: 0x04000028 RID: 40
	public bool change;

	// Token: 0x04000029 RID: 41
	private Mob mob1;

	// Token: 0x0400002A RID: 42
	public new int xSd;

	// Token: 0x0400002B RID: 43
	public new int ySd;

	// Token: 0x0400002C RID: 44
	private bool isOutMap;

	// Token: 0x0400002D RID: 45
	private int wCount;

	// Token: 0x0400002E RID: 46
	public new bool isShadown = true;

	// Token: 0x0400002F RID: 47
	private int tick;

	// Token: 0x04000030 RID: 48
	private int frame;

	// Token: 0x04000031 RID: 49
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000032 RID: 50
	private bool wy;

	// Token: 0x04000033 RID: 51
	private int wt;

	// Token: 0x04000034 RID: 52
	private int fy;

	// Token: 0x04000035 RID: 53
	private int ty;

	// Token: 0x04000036 RID: 54
	public new int typeSuperEff;

	// Token: 0x04000037 RID: 55
	private global::Char focus;

	// Token: 0x04000038 RID: 56
	private bool flyUp;

	// Token: 0x04000039 RID: 57
	private bool flyDown;

	// Token: 0x0400003A RID: 58
	private int dy;

	// Token: 0x0400003B RID: 59
	public bool changePos;

	// Token: 0x0400003C RID: 60
	private int tShock;

	// Token: 0x0400003D RID: 61
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x0400003E RID: 62
	private int tA;

	// Token: 0x0400003F RID: 63
	private global::Char[] charAttack;

	// Token: 0x04000040 RID: 64
	private int[] dameHP;

	// Token: 0x04000041 RID: 65
	private sbyte type;

	// Token: 0x04000042 RID: 66
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

	// Token: 0x04000043 RID: 67
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

	// Token: 0x04000044 RID: 68
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

	// Token: 0x04000045 RID: 69
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

	// Token: 0x04000046 RID: 70
	public new int[] hurt = new int[]
	{
		1,
		1,
		7,
		7
	};

	// Token: 0x04000047 RID: 71
	private bool shock;

	// Token: 0x04000048 RID: 72
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000049 RID: 73
	public new global::Char injureBy;

	// Token: 0x0400004A RID: 74
	public new bool injureThenDie;

	// Token: 0x0400004B RID: 75
	public new Mob mobToAttack;

	// Token: 0x0400004C RID: 76
	public new int forceWait;

	// Token: 0x0400004D RID: 77
	public new bool blindEff;

	// Token: 0x0400004E RID: 78
	public new bool sleepEff;
}
