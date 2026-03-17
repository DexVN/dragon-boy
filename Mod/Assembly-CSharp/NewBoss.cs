using System;

// Token: 0x02000080 RID: 128
public class NewBoss : Mob, IMapObject
{
	// Token: 0x06000643 RID: 1603 RVA: 0x0006B968 File Offset: 0x00069B68
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
		bool flag = Mob.arrMobTemplate[this.templateId].data == null;
		if (flag)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.status = 2;
		this.frameArr = null;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00002ED4 File Offset: 0x000010D4
	public override void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00002EE5 File Offset: 0x000010E5
	public override void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0006BBD0 File Offset: 0x00069DD0
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

	// Token: 0x06000647 RID: 1607 RVA: 0x0006BC24 File Offset: 0x00069E24
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

	// Token: 0x06000648 RID: 1608 RVA: 0x0006BC68 File Offset: 0x00069E68
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.x;
		bool flag = TileMap.tileTypeAt(this.x, this.y, 2);
		if (flag)
		{
			this.ySd = this.y;
		}
		else
		{
			this.ySd = this.y;
			while (i < 30)
			{
				i++;
				this.ySd += 24;
				bool flag2 = TileMap.tileTypeAt(this.xSd, this.ySd, 2);
				if (flag2)
				{
					bool flag3 = this.ySd % 24 != 0;
					if (flag3)
					{
						this.ySd -= this.ySd % 24;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0006BD18 File Offset: 0x00069F18
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		bool flag = (TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128;
		if (flag)
		{
			bool flag2 = TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4);
			if (flag2)
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0;
				if (flag3)
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0;
					if (flag4)
					{
						g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
					}
					else
					{
						bool flag5 = TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8);
						if (flag5)
						{
							g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
						}
					}
				}
			}
		}
		g.drawImage(NewBoss.shadowBig, this.xSd, this.ySd - 5, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00003136 File Offset: 0x00001336
	public new void updateSuperEff()
	{
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0006BEC0 File Offset: 0x0006A0C0
	public override void update()
	{
		bool flag = this.frameArr == null && Mob.arrMobTemplate[this.templateId].data != null;
		if (flag)
		{
			this.GetFrame();
		}
		bool flag2 = this.frameArr == null;
		if (!flag2)
		{
			bool flag3 = !this.isUpdate();
			if (!flag3)
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
					bool flag4 = this.y >= this.yFirst;
					if (flag4)
					{
						this.y = this.yFirst;
						this.p1 = 0;
						this.status = 5;
					}
					break;
				}
				case 7:
					this.updateInjure();
					base.update();
					break;
				}
			}
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0006C008 File Offset: 0x0006A208
	private void updateDead()
	{
		this.tick++;
		bool flag = this.tick > this.frameArr[13].Length - 1;
		if (flag)
		{
			this.tick = this.frameArr[13].Length - 1;
		}
		this.frame = this.frameArr[13][this.tick];
		bool flag2 = this.x != this.xTo || this.y != this.yTo;
		if (flag2)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00003136 File Offset: 0x00001336
	private void updateMobFly()
	{
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0006C0C8 File Offset: 0x0006A2C8
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

	// Token: 0x0600064F RID: 1615 RVA: 0x00003136 File Offset: 0x00001336
	private void updateInjure()
	{
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0006C190 File Offset: 0x0006A390
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.frameArr[0]);
		bool flag = this.x != this.xTo || this.y != this.yTo;
		if (flag)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0006C20D File Offset: 0x0006A40D
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0006C220 File Offset: 0x0006A420
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type, sbyte dir)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.dir = (int)dir;
		this.status = 3;
		bool flag = this.x != this.xTo || this.y != this.yTo;
		if (flag)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0006C2B4 File Offset: 0x0006A4B4
	public new void updateMobAttack()
	{
		bool flag = this.tick == this.frameArr[(int)(this.type + 1)].Length - 1;
		if (flag)
		{
			this.status = 2;
		}
		this.checkFrameTick(this.frameArr[(int)(this.type + 1)]);
		bool flag2 = this.tick == this.frameArr[15][(int)(this.type - 1)];
		if (flag2)
		{
			for (int i = 0; i < this.charAttack.Length; i++)
			{
				this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				ServerEffect.addServerEffect(this.frameArr[16][(int)(this.type - 1)], this.charAttack[i].cx, this.charAttack[i].cy, 1);
			}
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0006C388 File Offset: 0x0006A588
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.frameArr[1]);
		sbyte speed = Mob.arrMobTemplate[this.templateId].speed;
		int num = (int)speed;
		bool flag = Res.abs(this.x - this.xTo) < (int)speed;
		if (flag)
		{
			num = Res.abs(this.x - this.xTo);
		}
		this.x += ((this.x >= this.xTo) ? (-num) : num);
		this.y = this.yTo;
		bool flag2 = this.x < this.xTo;
		if (flag2)
		{
			this.dir = 1;
		}
		else
		{
			bool flag3 = this.x > this.xTo;
			if (flag3)
			{
				this.dir = -1;
			}
		}
		bool flag4 = Res.abs(this.x - this.xTo) <= 1;
		if (flag4)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0006C480 File Offset: 0x0006A680
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0006C4E4 File Offset: 0x0006A6E4
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0006C500 File Offset: 0x0006A700
	public override void paint(mGraphics g)
	{
		bool flag = Mob.arrMobTemplate[this.templateId].data == null;
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
						Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
				else
				{
					bool flag3 = this.isShadown;
					if (flag3)
					{
						this.paintShadow(g);
					}
					g.translate(0, GameCanvas.transY);
					bool flag4 = !this.changBody;
					if (flag4)
					{
						int num = 33;
						bool flag5 = this.yTemp == -1;
						if (flag5)
						{
							this.yTemp = this.y;
						}
						bool flag6 = TileMap.tileTypeAt(this.x + num, this.y + this.fy, 4);
						if (flag6)
						{
							this.xTempLeft = TileMap.tileXofPixel(this.x + num) - num;
							this.xTempRight = TileMap.tileXofPixel(this.x + num);
							bool flag7 = this.x > this.xTempLeft && this.x < this.xTempRight && this.xTempRight != -1;
							if (flag7)
							{
								this.x = this.xTempLeft;
							}
						}
						bool flag8 = this.y < this.yTemp && this.yTemp != -1;
						if (flag8)
						{
							this.yTemp = this.y;
							this.x += num;
						}
						bool flag9 = this.y > this.yTemp;
						if (flag9)
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
					bool flag10 = this.hp > 0;
					if (flag10)
					{
						int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
						int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
						int num2 = imageWidth;
						int num3 = this.x - imageWidth;
						int y = this.y - this.h - 5;
						int num4 = imageWidth * 2 * this.per / 100;
						int w = num4;
						bool flag11 = this.per_tem >= this.per;
						if (flag11)
						{
							int num6 = imageWidth;
							int per_tem = this.per_tem;
							int num8;
							if (GameCanvas.gameTick % 6 > 3)
							{
								int num7 = this.offset;
								this.offset = num7 + 1;
								num8 = num7;
							}
							else
							{
								num8 = this.offset;
							}
							w = num6 * (this.per_tem = per_tem - num8) / 100;
							bool flag12 = this.per_tem <= 0;
							if (flag12)
							{
								this.per_tem = 0;
							}
							bool flag13 = this.per_tem < this.per;
							if (flag13)
							{
								this.per_tem = this.per;
							}
							bool flag14 = this.offset >= 3;
							if (flag14)
							{
								this.offset = 3;
							}
						}
						bool flag15 = num4 > num2;
						int num5;
						if (flag15)
						{
							num5 = num4 - num2;
							bool flag16 = num5 <= 0;
							if (flag16)
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
			}
		}
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0006C994 File Offset: 0x0006AB94
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0006C9AB File Offset: 0x0006ABAB
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

	// Token: 0x0600065A RID: 1626 RVA: 0x0006C9E8 File Offset: 0x0006ABE8
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

	// Token: 0x0600065B RID: 1627 RVA: 0x0006CAB0 File Offset: 0x0006ACB0
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0006CAC8 File Offset: 0x0006ACC8
	public new int getY()
	{
		return this.y;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0006CAE0 File Offset: 0x0006ACE0
	public new int getH()
	{
		return this.h;
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0006CAF8 File Offset: 0x0006ACF8
	public new int getW()
	{
		return this.w;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0006CB10 File Offset: 0x0006AD10
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

	// Token: 0x06000660 RID: 1632 RVA: 0x0006CB58 File Offset: 0x0006AD58
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0006CB80 File Offset: 0x0006AD80
	public new void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0006CBA4 File Offset: 0x0006ADA4
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0006CBAE File Offset: 0x0006ADAE
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
	public new void move(short xMoveTo, short yMoveTo)
	{
		bool flag = yMoveTo != -1;
		if (flag)
		{
			bool flag2 = Res.distance(this.x, this.y, this.xTo, this.yTo) > 100;
			if (flag2)
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

	// Token: 0x06000665 RID: 1637 RVA: 0x0006CC3C File Offset: 0x0006AE3C
	public new void GetFrame()
	{
		try
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId.ToString() + string.Empty);
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0006CCC8 File Offset: 0x0006AEC8
	public void setDie()
	{
		this.status = 0;
	}

	// Token: 0x04000E0B RID: 3595
	public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000E0C RID: 3596
	public int xTo;

	// Token: 0x04000E0D RID: 3597
	public int yTo;

	// Token: 0x04000E0E RID: 3598
	public bool haftBody;

	// Token: 0x04000E0F RID: 3599
	public bool change;

	// Token: 0x04000E10 RID: 3600
	public new int xSd;

	// Token: 0x04000E11 RID: 3601
	public new int ySd;

	// Token: 0x04000E12 RID: 3602
	private int wCount;

	// Token: 0x04000E13 RID: 3603
	public new bool isShadown = true;

	// Token: 0x04000E14 RID: 3604
	private int tick;

	// Token: 0x04000E15 RID: 3605
	private int frame;

	// Token: 0x04000E16 RID: 3606
	public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000E17 RID: 3607
	private bool wy;

	// Token: 0x04000E18 RID: 3608
	private int wt;

	// Token: 0x04000E19 RID: 3609
	private int fy;

	// Token: 0x04000E1A RID: 3610
	private int ty;

	// Token: 0x04000E1B RID: 3611
	public new int typeSuperEff;

	// Token: 0x04000E1C RID: 3612
	private global::Char focus;

	// Token: 0x04000E1D RID: 3613
	private bool flyUp;

	// Token: 0x04000E1E RID: 3614
	private bool flyDown;

	// Token: 0x04000E1F RID: 3615
	private int dy;

	// Token: 0x04000E20 RID: 3616
	public bool changePos;

	// Token: 0x04000E21 RID: 3617
	private int tShock;

	// Token: 0x04000E22 RID: 3618
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000E23 RID: 3619
	private int tA;

	// Token: 0x04000E24 RID: 3620
	private global::Char[] charAttack;

	// Token: 0x04000E25 RID: 3621
	private int[] dameHP;

	// Token: 0x04000E26 RID: 3622
	private sbyte type;

	// Token: 0x04000E27 RID: 3623
	private int ff;

	// Token: 0x04000E28 RID: 3624
	private int offset;

	// Token: 0x04000E29 RID: 3625
	private int xTempRight = -1;

	// Token: 0x04000E2A RID: 3626
	private int xTempLeft = -1;

	// Token: 0x04000E2B RID: 3627
	private int yTemp = -1;

	// Token: 0x04000E2C RID: 3628
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000E2D RID: 3629
	public new global::Char injureBy;

	// Token: 0x04000E2E RID: 3630
	public new bool injureThenDie;

	// Token: 0x04000E2F RID: 3631
	public new Mob mobToAttack;

	// Token: 0x04000E30 RID: 3632
	public new int forceWait;

	// Token: 0x04000E31 RID: 3633
	public new bool blindEff;

	// Token: 0x04000E32 RID: 3634
	public new bool sleepEff;

	// Token: 0x04000E33 RID: 3635
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

	// Token: 0x04000E34 RID: 3636
	public new const sbyte stand = 0;

	// Token: 0x04000E35 RID: 3637
	public const sbyte moveFra = 1;

	// Token: 0x04000E36 RID: 3638
	public new const sbyte attack1 = 2;

	// Token: 0x04000E37 RID: 3639
	public new const sbyte attack2 = 3;

	// Token: 0x04000E38 RID: 3640
	public const sbyte attack3 = 4;

	// Token: 0x04000E39 RID: 3641
	public const sbyte attack4 = 5;

	// Token: 0x04000E3A RID: 3642
	public const sbyte attack5 = 6;

	// Token: 0x04000E3B RID: 3643
	public const sbyte attack6 = 7;

	// Token: 0x04000E3C RID: 3644
	public const sbyte attack7 = 8;

	// Token: 0x04000E3D RID: 3645
	public const sbyte attack8 = 9;

	// Token: 0x04000E3E RID: 3646
	public const sbyte attack9 = 10;

	// Token: 0x04000E3F RID: 3647
	public const sbyte attack10 = 11;

	// Token: 0x04000E40 RID: 3648
	public new const sbyte hurt = 12;

	// Token: 0x04000E41 RID: 3649
	public const sbyte die = 13;

	// Token: 0x04000E42 RID: 3650
	public const sbyte fly = 14;

	// Token: 0x04000E43 RID: 3651
	public const sbyte adddame = 15;

	// Token: 0x04000E44 RID: 3652
	public const sbyte typeEff = 16;
}
