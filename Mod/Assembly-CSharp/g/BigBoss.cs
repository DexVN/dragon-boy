using System;

namespace Assets.src.g
{
	// Token: 0x0200009E RID: 158
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x000599E0 File Offset: 0x00057DE0
		public BigBoss(int id, short px, short py, int templateID, int hp, int maxhp, int s)
		{
			this.xFirst = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yFirst = (int)py;
			this.mobId = id;
			this.hp = hp;
			this.maxHp = maxhp;
			this.templateId = templateID;
			this.w_hp_bar = 100;
			this.h_hp_bar = 6;
			this.len = this.w_hp_bar;
			base.updateHp_bar();
			if (s == 0)
			{
				this.getDataB();
			}
			if (s == 1)
			{
				this.getDataB2();
			}
			if (s == 2)
			{
				this.getDataB2();
				this.haftBody = true;
			}
			this.status = 2;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00059BB8 File Offset: 0x00057FB8
		public void getDataB2()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				100,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100 + "/img.png");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00059C94 File Offset: 0x00058094
		public void getDataB()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				101,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00059D74 File Offset: 0x00058174
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00059D84 File Offset: 0x00058184
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00059D90 File Offset: 0x00058190
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

		// Token: 0x060005C2 RID: 1474 RVA: 0x00059DD8 File Offset: 0x000581D8
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00059E10 File Offset: 0x00058210
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

		// Token: 0x060005C4 RID: 1476 RVA: 0x00059F48 File Offset: 0x00058348
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00059F95 File Offset: 0x00058395
		public new void updateSuperEff()
		{
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00059F98 File Offset: 0x00058398
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

		// Token: 0x060005C7 RID: 1479 RVA: 0x0005A094 File Offset: 0x00058494
		private void updateDead()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0005A178 File Offset: 0x00058578
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

		// Token: 0x060005C9 RID: 1481 RVA: 0x0005A266 File Offset: 0x00058666
		public new void setInjure()
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0005A268 File Offset: 0x00058668
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

		// Token: 0x060005CB RID: 1483 RVA: 0x0005A346 File Offset: 0x00058746
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0005A37F File Offset: 0x0005877F
		private void updateInjure()
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0005A384 File Offset: 0x00058784
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0005A40D File Offset: 0x0005880D
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0005A420 File Offset: 0x00058820
		public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			if ((int)type < 3)
			{
				this.status = 3;
			}
			if ((int)type == 3)
			{
				this.flyUp = true;
				this.status = 4;
			}
			if ((int)type == 4)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				}
			}
			if ((int)type == 7)
			{
				this.status = 3;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0005A4B8 File Offset: 0x000588B8
		public new void updateMobAttack()
		{
			if ((int)this.type == 7)
			{
				if (this.tick > 8)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? -15 : 15), this.y - 40, 1);
				}
			}
			if ((int)this.type == 0)
			{
				if (this.tick == this.attack1.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick(this.attack1);
				if (this.tick == 8)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 30, true, this.dameHP[i], 0, this.charAttack[i], 24);
					}
				}
			}
			if ((int)this.type == 1)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				if (this.tick == 18)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			if ((int)this.type == 8)
			{
			}
			if ((int)this.type == 2)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
				if (this.tick == 13)
				{
					GameScr.shock_scr = 10;
					this.shock = true;
					for (int k = 0; k < this.charAttack.Length; k++)
					{
						this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					}
				}
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0005A7F9 File Offset: 0x00058BF9
		public new void updateMobWalk()
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0005A7FC File Offset: 0x00058BFC
		public new bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0005A86E File Offset: 0x00058C6E
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0005A87E File Offset: 0x00058C7E
		public new bool checkIsBoss()
		{
			return this.isBoss || (int)this.levelBoss > 0;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0005A89C File Offset: 0x00058C9C
		public override void paint(mGraphics g)
		{
			if (BigBoss.data == null)
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
					BigBoss.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
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
				BigBoss.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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
				Res.outz("type= " + this.type);
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

		// Token: 0x060005D6 RID: 1494 RVA: 0x0005ABAD File Offset: 0x00058FAD
		public new int getHPColor()
		{
			return 16711680;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0005ABB4 File Offset: 0x00058FB4
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

		// Token: 0x060005D8 RID: 1496 RVA: 0x0005ABF0 File Offset: 0x00058FF0
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

		// Token: 0x060005D9 RID: 1497 RVA: 0x0005ACCE File Offset: 0x000590CE
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0005ACD6 File Offset: 0x000590D6
		public new int getY()
		{
			return (!this.haftBody) ? (this.y - 60) : (this.y - 20);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0005ACFA File Offset: 0x000590FA
		public new int getH()
		{
			return 40;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0005ACFE File Offset: 0x000590FE
		public new int getW()
		{
			return 60;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0005AD04 File Offset: 0x00059104
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0005AD45 File Offset: 0x00059145
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0005AD5E File Offset: 0x0005915E
		public new void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0005AD72 File Offset: 0x00059172
		public new void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0005AD7B File Offset: 0x0005917B
		public new void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x04000A52 RID: 2642
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04000A53 RID: 2643
		public static EffectData data;

		// Token: 0x04000A54 RID: 2644
		public int xTo;

		// Token: 0x04000A55 RID: 2645
		public int yTo;

		// Token: 0x04000A56 RID: 2646
		public bool haftBody;

		// Token: 0x04000A57 RID: 2647
		public bool change;

		// Token: 0x04000A58 RID: 2648
		public new int xSd;

		// Token: 0x04000A59 RID: 2649
		public new int ySd;

		// Token: 0x04000A5A RID: 2650
		private bool isOutMap;

		// Token: 0x04000A5B RID: 2651
		private int wCount;

		// Token: 0x04000A5C RID: 2652
		public new bool isShadown = true;

		// Token: 0x04000A5D RID: 2653
		private int tick;

		// Token: 0x04000A5E RID: 2654
		private int frame;

		// Token: 0x04000A5F RID: 2655
		private bool wy;

		// Token: 0x04000A60 RID: 2656
		private int wt;

		// Token: 0x04000A61 RID: 2657
		private int fy;

		// Token: 0x04000A62 RID: 2658
		private int ty;

		// Token: 0x04000A63 RID: 2659
		public new int typeSuperEff;

		// Token: 0x04000A64 RID: 2660
		private global::Char focus;

		// Token: 0x04000A65 RID: 2661
		private bool flyUp;

		// Token: 0x04000A66 RID: 2662
		private bool flyDown;

		// Token: 0x04000A67 RID: 2663
		private int dy;

		// Token: 0x04000A68 RID: 2664
		public bool changePos;

		// Token: 0x04000A69 RID: 2665
		private int tShock;

		// Token: 0x04000A6A RID: 2666
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04000A6B RID: 2667
		private int tA;

		// Token: 0x04000A6C RID: 2668
		private global::Char[] charAttack;

		// Token: 0x04000A6D RID: 2669
		private int[] dameHP;

		// Token: 0x04000A6E RID: 2670
		private sbyte type;

		// Token: 0x04000A6F RID: 2671
		public new int[] stand = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		};

		// Token: 0x04000A70 RID: 2672
		public int[] stand_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			38,
			39,
			39,
			40,
			40,
			40,
			39,
			39,
			39,
			38,
			38,
			38
		};

		// Token: 0x04000A71 RID: 2673
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

		// Token: 0x04000A72 RID: 2674
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

		// Token: 0x04000A73 RID: 2675
		public new int[] attack1 = new int[]
		{
			0,
			0,
			34,
			34,
			35,
			35,
			36,
			36,
			2,
			2,
			1,
			1
		};

		// Token: 0x04000A74 RID: 2676
		public new int[] attack2 = new int[]
		{
			0,
			0,
			0,
			4,
			4,
			6,
			6,
			9,
			9,
			10,
			10,
			13,
			13,
			15,
			15,
			17,
			17,
			19,
			19,
			21,
			21,
			23,
			23
		};

		// Token: 0x04000A75 RID: 2677
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

		// Token: 0x04000A76 RID: 2678
		public int[] attack2_1 = new int[]
		{
			37,
			37,
			5,
			5,
			7,
			7,
			11,
			11,
			14,
			14,
			16,
			16,
			18,
			18,
			20,
			20,
			22,
			22,
			24,
			24
		};

		// Token: 0x04000A77 RID: 2679
		public int[] attack3_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			5,
			5,
			7,
			7,
			11,
			11,
			27,
			27,
			29,
			29,
			31,
			31,
			33,
			33,
			38,
			38
		};

		// Token: 0x04000A78 RID: 2680
		public int[] fly = new int[]
		{
			8,
			8,
			9,
			9,
			10,
			10,
			12,
			12
		};

		// Token: 0x04000A79 RID: 2681
		public int[] hitground = new int[]
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

		// Token: 0x04000A7A RID: 2682
		private bool shock;

		// Token: 0x04000A7B RID: 2683
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04000A7C RID: 2684
		public new global::Char injureBy;

		// Token: 0x04000A7D RID: 2685
		public new bool injureThenDie;

		// Token: 0x04000A7E RID: 2686
		public new Mob mobToAttack;

		// Token: 0x04000A7F RID: 2687
		public new int forceWait;

		// Token: 0x04000A80 RID: 2688
		public new bool blindEff;

		// Token: 0x04000A81 RID: 2689
		public new bool sleepEff;
	}
}
