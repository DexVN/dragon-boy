using System;

// Token: 0x020000BA RID: 186
public class NewBoss : Mob, IMapObject
{
	// Token: 0x0600085F RID: 2143 RVA: 0x00076178 File Offset: 0x00074578
	public NewBoss(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		this.mobId = id;
		this.x = (this.xFirst = (int)(px + 20));
		this.yFirst = (int)py;
		this.y = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		this.h_hp_bar = 6;
		this.w_hp_bar = 100;
		this.len = this.w_hp_bar;
		base.updateHp_bar();
		if (Mob.arrMobTemplate[this.templateId].data == null)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.status = 2;
		this.frameArr = null;
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x000763DA File Offset: 0x000747DA
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x000763EA File Offset: 0x000747EA
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x000763F4 File Offset: 0x000747F4
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

	// Token: 0x06000863 RID: 2147 RVA: 0x0007643C File Offset: 0x0007483C
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00076474 File Offset: 0x00074874
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.x;
		if (TileMap.tileTypeAt(this.x, this.y, 2))
		{
			this.ySd = this.y;
			return;
		}
		this.ySd = this.y;
		while (i < 30)
		{
			i++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
				}
				break;
			}
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00076520 File Offset: 0x00074920
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128)
		{
			if (TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4))
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0)
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0)
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
			}
			else if (TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8))
			{
				g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
			}
		}
		g.drawImage(NewBoss.shadowBig, this.xSd, this.ySd - 5, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x000766B4 File Offset: 0x00074AB4
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x000766B8 File Offset: 0x00074AB8
	public override void update()
	{
		if (this.frameArr == null && Mob.arrMobTemplate[this.templateId].data != null)
		{
			this.GetFrame();
		}
		if (this.frameArr == null)
		{
			return;
		}
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
			base.update();
			break;
		}
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000767E4 File Offset: 0x00074BE4
	private void updateDead()
	{
		this.tick++;
		if (this.tick > this.frameArr[13].Length - 1)
		{
			this.tick = this.frameArr[13].Length - 1;
		}
		this.frame = this.frameArr[13][this.tick];
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0007689A File Offset: 0x00074C9A
	private void updateMobFly()
	{
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0007689C File Offset: 0x00074C9C
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
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

	// Token: 0x0600086B RID: 2155 RVA: 0x0007695C File Offset: 0x00074D5C
	private void updateInjure()
	{
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00076960 File Offset: 0x00074D60
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.frameArr[0]);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x000769D5 File Offset: 0x00074DD5
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x000769E8 File Offset: 0x00074DE8
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type, sbyte dir)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.dir = (int)dir;
		this.status = 3;
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00076A74 File Offset: 0x00074E74
	public new void updateMobAttack()
	{
		if (this.tick == this.frameArr[(int)this.type + 1].Length - 1)
		{
			this.status = 2;
		}
		this.checkFrameTick(this.frameArr[(int)this.type + 1]);
		if (this.tick == this.frameArr[15][(int)this.type - 1])
		{
			for (int i = 0; i < this.charAttack.Length; i++)
			{
				this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				ServerEffect.addServerEffect(this.frameArr[16][(int)this.type - 1], this.charAttack[i].cx, this.charAttack[i].cy, 1);
			}
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00076B40 File Offset: 0x00074F40
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.frameArr[1]);
		sbyte speed = Mob.arrMobTemplate[this.templateId].speed;
		int num = (int)speed;
		if (Res.abs(this.x - this.xTo) < (int)speed)
		{
			num = Res.abs(this.x - this.xTo);
		}
		this.x += ((this.x >= this.xTo) ? (-num) : num);
		this.y = this.yTo;
		if (this.x < this.xTo)
		{
			this.dir = 1;
		}
		else if (this.x > this.xTo)
		{
			this.dir = -1;
		}
		if (Res.abs(this.x - this.xTo) <= 1)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00076C30 File Offset: 0x00075030
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00076CA2 File Offset: 0x000750A2
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00076CB4 File Offset: 0x000750B4
	public override void paint(mGraphics g)
	{
		if (Mob.arrMobTemplate[this.templateId].data == null)
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
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			return;
		}
		if (this.isShadown)
		{
			this.paintShadow(g);
		}
		g.translate(0, GameCanvas.transY);
		if (!this.changBody)
		{
			int num = 33;
			if (this.yTemp == -1)
			{
				this.yTemp = this.y;
			}
			if (TileMap.tileTypeAt(this.x + num, this.y + this.fy, 4))
			{
				this.xTempLeft = TileMap.tileXofPixel(this.x + num) - num;
				this.xTempRight = TileMap.tileXofPixel(this.x + num);
				if (this.x > this.xTempLeft && this.x < this.xTempRight && this.xTempRight != -1)
				{
					this.x = this.xTempLeft;
				}
			}
			if (this.y < this.yTemp && this.yTemp != -1)
			{
				this.yTemp = this.y;
				this.x += num;
			}
			if (this.y > this.yTemp)
			{
				this.yTemp = this.y;
				this.x -= num;
			}
			Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		g.translate(0, -GameCanvas.transY);
		if (this.hp > 0)
		{
			int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
			int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
			int num2 = imageWidth;
			int num3 = this.x - imageWidth;
			int y = this.y - this.h - 5;
			int num4 = imageWidth * 2 * this.per / 100;
			int w = num4;
			if (this.per_tem >= this.per)
			{
				w = imageWidth * (this.per_tem -= ((GameCanvas.gameTick % 6 <= 3) ? this.offset : this.offset++)) / 100;
				if (this.per_tem <= 0)
				{
					this.per_tem = 0;
				}
				if (this.per_tem < this.per)
				{
					this.per_tem = this.per;
				}
				if (this.offset >= 3)
				{
					this.offset = 3;
				}
			}
			int num5;
			if (num4 > num2)
			{
				num5 = num4 - num2;
				if (num5 <= 0)
				{
					num5 = 0;
				}
			}
			else
			{
				num2 = num4;
				num5 = 0;
			}
			g.drawImage(GameScr.imgHP_tm_xam, num3, y, mGraphics.TOP | mGraphics.LEFT);
			g.drawImage(GameScr.imgHP_tm_xam, num3 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
			g.setColor(16777215);
			g.fillRect(num3, y, w, 2);
			g.drawRegion(this.imgHPtem, 0, 0, num2, imageHeight, 0, num3, y, mGraphics.TOP | mGraphics.LEFT);
			g.drawRegion(this.imgHPtem, 0, 0, num5, imageHeight, 0, num3 + imageWidth, y, mGraphics.TOP | mGraphics.LEFT);
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x000770DA File Offset: 0x000754DA
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000770E1 File Offset: 0x000754E1
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

	// Token: 0x06000876 RID: 2166 RVA: 0x0007711C File Offset: 0x0007551C
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
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

	// Token: 0x06000877 RID: 2167 RVA: 0x000771DC File Offset: 0x000755DC
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x000771E4 File Offset: 0x000755E4
	public new int getY()
	{
		return this.y;
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000771EC File Offset: 0x000755EC
	public new int getH()
	{
		return this.h;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000771F4 File Offset: 0x000755F4
	public new int getW()
	{
		return this.w;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000771FC File Offset: 0x000755FC
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0007723D File Offset: 0x0007563D
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00077256 File Offset: 0x00075656
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0007726A File Offset: 0x0007566A
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00077273 File Offset: 0x00075673
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0007727C File Offset: 0x0007567C
	public new void move(short xMoveTo, short yMoveTo)
	{
		if (yMoveTo != -1)
		{
			if (Res.distance(this.x, this.y, this.xTo, this.yTo) > 100)
			{
				this.x = (int)xMoveTo;
				this.y = (int)yMoveTo;
				this.status = 2;
			}
			else
			{
				this.xTo = (int)xMoveTo;
				this.yTo = (int)yMoveTo;
				this.status = 5;
			}
		}
		else
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x000772F8 File Offset: 0x000756F8
	public new void GetFrame()
	{
		try
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId + string.Empty);
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00077384 File Offset: 0x00075784
	public void setDie()
	{
		this.status = 0;
	}

	// Token: 0x04001003 RID: 4099
	public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04001004 RID: 4100
	public int xTo;

	// Token: 0x04001005 RID: 4101
	public int yTo;

	// Token: 0x04001006 RID: 4102
	public bool haftBody;

	// Token: 0x04001007 RID: 4103
	public bool change;

	// Token: 0x04001008 RID: 4104
	public new int xSd;

	// Token: 0x04001009 RID: 4105
	public new int ySd;

	// Token: 0x0400100A RID: 4106
	private int wCount;

	// Token: 0x0400100B RID: 4107
	public new bool isShadown = true;

	// Token: 0x0400100C RID: 4108
	private int tick;

	// Token: 0x0400100D RID: 4109
	private int frame;

	// Token: 0x0400100E RID: 4110
	public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x0400100F RID: 4111
	private bool wy;

	// Token: 0x04001010 RID: 4112
	private int wt;

	// Token: 0x04001011 RID: 4113
	private int fy;

	// Token: 0x04001012 RID: 4114
	private int ty;

	// Token: 0x04001013 RID: 4115
	public new int typeSuperEff;

	// Token: 0x04001014 RID: 4116
	private global::Char focus;

	// Token: 0x04001015 RID: 4117
	private bool flyUp;

	// Token: 0x04001016 RID: 4118
	private bool flyDown;

	// Token: 0x04001017 RID: 4119
	private int dy;

	// Token: 0x04001018 RID: 4120
	public bool changePos;

	// Token: 0x04001019 RID: 4121
	private int tShock;

	// Token: 0x0400101A RID: 4122
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x0400101B RID: 4123
	private int tA;

	// Token: 0x0400101C RID: 4124
	private global::Char[] charAttack;

	// Token: 0x0400101D RID: 4125
	private int[] dameHP;

	// Token: 0x0400101E RID: 4126
	private sbyte type;

	// Token: 0x0400101F RID: 4127
	private int ff;

	// Token: 0x04001020 RID: 4128
	private int offset;

	// Token: 0x04001021 RID: 4129
	private int xTempRight = -1;

	// Token: 0x04001022 RID: 4130
	private int xTempLeft = -1;

	// Token: 0x04001023 RID: 4131
	private int yTemp = -1;

	// Token: 0x04001024 RID: 4132
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04001025 RID: 4133
	public new global::Char injureBy;

	// Token: 0x04001026 RID: 4134
	public new bool injureThenDie;

	// Token: 0x04001027 RID: 4135
	public new Mob mobToAttack;

	// Token: 0x04001028 RID: 4136
	public new int forceWait;

	// Token: 0x04001029 RID: 4137
	public new bool blindEff;

	// Token: 0x0400102A RID: 4138
	public new bool sleepEff;

	// Token: 0x0400102B RID: 4139
	private int[][] frameArr = new int[][]
	{
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		}
	};

	// Token: 0x0400102C RID: 4140
	public new const sbyte stand = 0;

	// Token: 0x0400102D RID: 4141
	public const sbyte moveFra = 1;

	// Token: 0x0400102E RID: 4142
	public new const sbyte attack1 = 2;

	// Token: 0x0400102F RID: 4143
	public new const sbyte attack2 = 3;

	// Token: 0x04001030 RID: 4144
	public const sbyte attack3 = 4;

	// Token: 0x04001031 RID: 4145
	public const sbyte attack4 = 5;

	// Token: 0x04001032 RID: 4146
	public const sbyte attack5 = 6;

	// Token: 0x04001033 RID: 4147
	public const sbyte attack6 = 7;

	// Token: 0x04001034 RID: 4148
	public const sbyte attack7 = 8;

	// Token: 0x04001035 RID: 4149
	public const sbyte attack8 = 9;

	// Token: 0x04001036 RID: 4150
	public const sbyte attack9 = 10;

	// Token: 0x04001037 RID: 4151
	public const sbyte attack10 = 11;

	// Token: 0x04001038 RID: 4152
	public new const sbyte hurt = 12;

	// Token: 0x04001039 RID: 4153
	public const sbyte die = 13;

	// Token: 0x0400103A RID: 4154
	public const sbyte fly = 14;

	// Token: 0x0400103B RID: 4155
	public const sbyte adddame = 15;

	// Token: 0x0400103C RID: 4156
	public const sbyte typeEff = 16;
}
