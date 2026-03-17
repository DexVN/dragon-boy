using System;

namespace Assets.src.g
{
	// Token: 0x020000BD RID: 189
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x000A79D0 File Offset: 0x000A5BD0
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
			bool flag = s == 0;
			if (flag)
			{
				this.getDataB();
			}
			bool flag2 = s == 1;
			if (flag2)
			{
				this.getDataB2();
			}
			bool flag3 = s == 2;
			if (flag3)
			{
				this.getDataB2();
				this.haftBody = true;
			}
			this.status = 2;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000A7BB4 File Offset: 0x000A5DB4
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
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100.ToString() + "/img.png");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000A7C94 File Offset: 0x000A5E94
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
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101.ToString() + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00002ED4 File Offset: 0x000010D4
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00002EE5 File Offset: 0x000010E5
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000A7D78 File Offset: 0x000A5F78
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

		// Token: 0x06000A2A RID: 2602 RVA: 0x000A7DCC File Offset: 0x000A5FCC
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

		// Token: 0x06000A2B RID: 2603 RVA: 0x000A7E10 File Offset: 0x000A6010
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

		// Token: 0x06000A2C RID: 2604 RVA: 0x000A7F68 File Offset: 0x000A6168
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00003136 File Offset: 0x00001336
		public new void updateSuperEff()
		{
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000A7FB8 File Offset: 0x000A61B8
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

		// Token: 0x06000A2F RID: 2607 RVA: 0x000A80BC File Offset: 0x000A62BC
		private void updateDead()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			bool flag = GameCanvas.gameTick % 5 == 0;
			if (flag)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			bool flag2 = this.x != this.xFirst || this.y != this.yFirst;
			if (flag2)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000A81A8 File Offset: 0x000A63A8
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

		// Token: 0x06000A31 RID: 2609 RVA: 0x00003136 File Offset: 0x00001336
		public new void setInjure()
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000A82A4 File Offset: 0x000A64A4
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

		// Token: 0x06000A33 RID: 2611 RVA: 0x000A8384 File Offset: 0x000A6584
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00003136 File Offset: 0x00001336
		private void updateInjure()
		{
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000A83C4 File Offset: 0x000A65C4
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			bool flag = this.x != this.xFirst || this.y != this.yFirst;
			if (flag)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000A844F File Offset: 0x000A664F
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000A8460 File Offset: 0x000A6660
		public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			bool flag = type < 3;
			if (flag)
			{
				this.status = 3;
			}
			bool flag2 = type == 3;
			if (flag2)
			{
				this.flyUp = true;
				this.status = 4;
			}
			bool flag3 = type == 4;
			if (flag3)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				}
			}
			bool flag4 = type == 7;
			if (flag4)
			{
				this.status = 3;
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000A8504 File Offset: 0x000A6704
		public new void updateMobAttack()
		{
			bool flag = this.type == 7;
			if (flag)
			{
				bool flag2 = this.tick > 8;
				if (flag2)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				bool flag3 = GameCanvas.gameTick % 4 == 0;
				if (flag3)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? -15 : 15), this.y - 40, 1);
				}
			}
			bool flag4 = this.type == 0;
			if (flag4)
			{
				bool flag5 = this.tick == this.attack1.Length - 1;
				if (flag5)
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick(this.attack1);
				bool flag6 = this.tick == 8;
				if (flag6)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 30, true, this.dameHP[i], 0, this.charAttack[i], 24);
					}
				}
			}
			bool flag7 = this.type == 1;
			if (flag7)
			{
				bool flag8 = this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1));
				if (flag8)
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				bool flag9 = this.tick == 18;
				if (flag9)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			bool flag10 = this.type == 8;
			if (flag10)
			{
			}
			bool flag11 = this.type == 2;
			if (flag11)
			{
				bool flag12 = this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1));
				if (flag12)
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
				bool flag13 = this.tick == 13;
				if (flag13)
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

		// Token: 0x06000A39 RID: 2617 RVA: 0x00003136 File Offset: 0x00001336
		public new void updateMobWalk()
		{
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000A8874 File Offset: 0x000A6A74
		public new bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000A88D8 File Offset: 0x000A6AD8
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000A88F4 File Offset: 0x000A6AF4
		public new bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000A891C File Offset: 0x000A6B1C
		public override void paint(mGraphics g)
		{
			bool flag = BigBoss.data == null;
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
							BigBoss.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
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
							Res.outz("type= " + this.type.ToString());
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

		// Token: 0x06000A3E RID: 2622 RVA: 0x000A8C50 File Offset: 0x000A6E50
		public new int getHPColor()
		{
			return 16711680;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000A8C67 File Offset: 0x000A6E67
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

		// Token: 0x06000A40 RID: 2624 RVA: 0x000A8CA4 File Offset: 0x000A6EA4
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

		// Token: 0x06000A41 RID: 2625 RVA: 0x000A8D84 File Offset: 0x000A6F84
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000A8D9C File Offset: 0x000A6F9C
		public new int getY()
		{
			return (!this.haftBody) ? (this.y - 60) : (this.y - 20);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000A8DCC File Offset: 0x000A6FCC
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000A8DE0 File Offset: 0x000A6FE0
		public new int getW()
		{
			return 60;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000A8DF4 File Offset: 0x000A6FF4
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

		// Token: 0x06000A46 RID: 2630 RVA: 0x000A8E3C File Offset: 0x000A703C
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000A8E64 File Offset: 0x000A7064
		public new void removeHoldEff()
		{
			bool flag = this.holdEffID != 0;
			if (flag)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000A8E88 File Offset: 0x000A7088
		public new void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000A8E92 File Offset: 0x000A7092
		public new void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x0400134B RID: 4939
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x0400134C RID: 4940
		public static EffectData data;

		// Token: 0x0400134D RID: 4941
		public int xTo;

		// Token: 0x0400134E RID: 4942
		public int yTo;

		// Token: 0x0400134F RID: 4943
		public bool haftBody;

		// Token: 0x04001350 RID: 4944
		public bool change;

		// Token: 0x04001351 RID: 4945
		public new int xSd;

		// Token: 0x04001352 RID: 4946
		public new int ySd;

		// Token: 0x04001353 RID: 4947
		private bool isOutMap;

		// Token: 0x04001354 RID: 4948
		private int wCount;

		// Token: 0x04001355 RID: 4949
		public new bool isShadown = true;

		// Token: 0x04001356 RID: 4950
		private int tick;

		// Token: 0x04001357 RID: 4951
		private int frame;

		// Token: 0x04001358 RID: 4952
		private bool wy;

		// Token: 0x04001359 RID: 4953
		private int wt;

		// Token: 0x0400135A RID: 4954
		private int fy;

		// Token: 0x0400135B RID: 4955
		private int ty;

		// Token: 0x0400135C RID: 4956
		public new int typeSuperEff;

		// Token: 0x0400135D RID: 4957
		private global::Char focus;

		// Token: 0x0400135E RID: 4958
		private bool flyUp;

		// Token: 0x0400135F RID: 4959
		private bool flyDown;

		// Token: 0x04001360 RID: 4960
		private int dy;

		// Token: 0x04001361 RID: 4961
		public bool changePos;

		// Token: 0x04001362 RID: 4962
		private int tShock;

		// Token: 0x04001363 RID: 4963
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04001364 RID: 4964
		private int tA;

		// Token: 0x04001365 RID: 4965
		private global::Char[] charAttack;

		// Token: 0x04001366 RID: 4966
		private int[] dameHP;

		// Token: 0x04001367 RID: 4967
		private sbyte type;

		// Token: 0x04001368 RID: 4968
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

		// Token: 0x04001369 RID: 4969
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

		// Token: 0x0400136A RID: 4970
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

		// Token: 0x0400136B RID: 4971
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

		// Token: 0x0400136C RID: 4972
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

		// Token: 0x0400136D RID: 4973
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

		// Token: 0x0400136E RID: 4974
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

		// Token: 0x0400136F RID: 4975
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

		// Token: 0x04001370 RID: 4976
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

		// Token: 0x04001371 RID: 4977
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

		// Token: 0x04001372 RID: 4978
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

		// Token: 0x04001373 RID: 4979
		private bool shock;

		// Token: 0x04001374 RID: 4980
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04001375 RID: 4981
		public new global::Char injureBy;

		// Token: 0x04001376 RID: 4982
		public new bool injureThenDie;

		// Token: 0x04001377 RID: 4983
		public new Mob mobToAttack;

		// Token: 0x04001378 RID: 4984
		public new int forceWait;

		// Token: 0x04001379 RID: 4985
		public new bool blindEff;

		// Token: 0x0400137A RID: 4986
		public new bool sleepEff;
	}
}
