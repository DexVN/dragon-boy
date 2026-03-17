using System;

// Token: 0x0200000E RID: 14
public class BigBoss2 : Mob, IMapObject
{
	// Token: 0x0600006E RID: 110 RVA: 0x00006D70 File Offset: 0x00004F70
	public BigBoss2(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		bool flag = BigBoss2.shadowBig == null;
		if (flag)
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

	// Token: 0x0600006F RID: 111 RVA: 0x00006EFC File Offset: 0x000050FC
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
			BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109.ToString() + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BigBoss2.data.width;
		this.h = BigBoss2.data.height;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00002ED4 File Offset: 0x000010D4
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00002EE5 File Offset: 0x000010E5
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00006FD4 File Offset: 0x000051D4
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

	// Token: 0x06000073 RID: 115 RVA: 0x00007028 File Offset: 0x00005228
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

	// Token: 0x06000074 RID: 116 RVA: 0x0000706C File Offset: 0x0000526C
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

	// Token: 0x06000075 RID: 117 RVA: 0x000071C4 File Offset: 0x000053C4
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003136 File Offset: 0x00001336
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000721C File Offset: 0x0000541C
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
			case 4:
				this.timeStatus = 0;
				this.updateMobFly();
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

	// Token: 0x06000078 RID: 120 RVA: 0x00007320 File Offset: 0x00005520
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

	// Token: 0x06000079 RID: 121 RVA: 0x000073FC File Offset: 0x000055FC
	private void updateMobFly()
	{
		bool flag = this.flyUp;
		if (flag)
		{
			this.dy++;
			this.y -= this.dy;
			this.checkFrameTick(this.fly);
			bool flag2 = this.y <= -500;
			if (flag2)
			{
				this.flyUp = false;
				this.flyDown = true;
				this.dy = 0;
			}
		}
		bool flag3 = this.flyDown;
		if (flag3)
		{
			this.x = this.xTo;
			this.dy += 2;
			this.y += this.dy;
			this.checkFrameTick(this.hitground);
			bool flag4 = this.y > this.yFirst;
			if (flag4)
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

	// Token: 0x0600007A RID: 122 RVA: 0x00003136 File Offset: 0x00001336
	public new void setInjure()
	{
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000074F8 File Offset: 0x000056F8
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

	// Token: 0x0600007C RID: 124 RVA: 0x000075D8 File Offset: 0x000057D8
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003136 File Offset: 0x00001336
	private void updateInjure()
	{
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00007618 File Offset: 0x00005818
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

	// Token: 0x0600007F RID: 127 RVA: 0x00007693 File Offset: 0x00005893
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000076A4 File Offset: 0x000058A4
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.status = 3;
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.tick = 0;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000076CC File Offset: 0x000058CC
	public new void updateMobAttack()
	{
		bool flag = this.type == 0;
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
		bool flag4 = this.type == 1;
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
					MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 25, true, this.dameHP[j], 0, this.charAttack[j], 24);
				}
			}
		}
		bool flag7 = this.type == 2;
		if (flag7)
		{
			bool flag8 = this.tick == this.fly.Length - 1;
			if (flag8)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.fly);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.xTo = this.x;
			this.yTo = this.y;
			bool flag9 = this.tick == 12;
			if (flag9)
			{
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003136 File Offset: 0x00001336
	public new void updateMobWalk()
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000079C8 File Offset: 0x00005BC8
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00007A2C File Offset: 0x00005C2C
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00007A48 File Offset: 0x00005C48
	public new bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00007A70 File Offset: 0x00005C70
	public override void paint(mGraphics g)
	{
		bool flag = BigBoss2.data == null;
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
						BigBoss2.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
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

	// Token: 0x06000087 RID: 135 RVA: 0x00007D8C File Offset: 0x00005F8C
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00007DA3 File Offset: 0x00005FA3
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

	// Token: 0x06000089 RID: 137 RVA: 0x00007DE0 File Offset: 0x00005FE0
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

	// Token: 0x0600008A RID: 138 RVA: 0x00007EC0 File Offset: 0x000060C0
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00007ED8 File Offset: 0x000060D8
	public new int getY()
	{
		return this.y - 50;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00007EF4 File Offset: 0x000060F4
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00007F08 File Offset: 0x00006108
	public new int getW()
	{
		return 50;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00007F1C File Offset: 0x0000611C
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

	// Token: 0x0600008F RID: 143 RVA: 0x00007F64 File Offset: 0x00006164
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00007F8C File Offset: 0x0000618C
	public new void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00007FB0 File Offset: 0x000061B0
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00007FBA File Offset: 0x000061BA
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x040000B7 RID: 183
	public static Image shadowBig;

	// Token: 0x040000B8 RID: 184
	public static EffectData data;

	// Token: 0x040000B9 RID: 185
	public int xTo;

	// Token: 0x040000BA RID: 186
	public int yTo;

	// Token: 0x040000BB RID: 187
	public bool haftBody;

	// Token: 0x040000BC RID: 188
	public bool change;

	// Token: 0x040000BD RID: 189
	private Mob mob1;

	// Token: 0x040000BE RID: 190
	public new int xSd;

	// Token: 0x040000BF RID: 191
	public new int ySd;

	// Token: 0x040000C0 RID: 192
	private bool isOutMap;

	// Token: 0x040000C1 RID: 193
	private int wCount;

	// Token: 0x040000C2 RID: 194
	public new bool isShadown = true;

	// Token: 0x040000C3 RID: 195
	private int tick;

	// Token: 0x040000C4 RID: 196
	private int frame;

	// Token: 0x040000C5 RID: 197
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x040000C6 RID: 198
	private bool wy;

	// Token: 0x040000C7 RID: 199
	private int wt;

	// Token: 0x040000C8 RID: 200
	private int fy;

	// Token: 0x040000C9 RID: 201
	private int ty;

	// Token: 0x040000CA RID: 202
	public new int typeSuperEff;

	// Token: 0x040000CB RID: 203
	private global::Char focus;

	// Token: 0x040000CC RID: 204
	private int timeDead;

	// Token: 0x040000CD RID: 205
	private bool flyUp;

	// Token: 0x040000CE RID: 206
	private bool flyDown;

	// Token: 0x040000CF RID: 207
	private int dy;

	// Token: 0x040000D0 RID: 208
	public bool changePos;

	// Token: 0x040000D1 RID: 209
	private int tShock;

	// Token: 0x040000D2 RID: 210
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x040000D3 RID: 211
	private int tA;

	// Token: 0x040000D4 RID: 212
	private global::Char[] charAttack;

	// Token: 0x040000D5 RID: 213
	private int[] dameHP;

	// Token: 0x040000D6 RID: 214
	private sbyte type;

	// Token: 0x040000D7 RID: 215
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

	// Token: 0x040000D8 RID: 216
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

	// Token: 0x040000D9 RID: 217
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

	// Token: 0x040000DA RID: 218
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

	// Token: 0x040000DB RID: 219
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

	// Token: 0x040000DC RID: 220
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

	// Token: 0x040000DD RID: 221
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

	// Token: 0x040000DE RID: 222
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

	// Token: 0x040000DF RID: 223
	private bool shock;

	// Token: 0x040000E0 RID: 224
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x040000E1 RID: 225
	public new global::Char injureBy;

	// Token: 0x040000E2 RID: 226
	public new bool injureThenDie;

	// Token: 0x040000E3 RID: 227
	public new Mob mobToAttack;

	// Token: 0x040000E4 RID: 228
	public new int forceWait;

	// Token: 0x040000E5 RID: 229
	public new bool blindEff;

	// Token: 0x040000E6 RID: 230
	public new bool sleepEff;
}
