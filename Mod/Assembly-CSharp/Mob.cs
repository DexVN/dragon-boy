using System;
using Assets.src.g;

// Token: 0x0200006B RID: 107
public class Mob : IMapObject
{
	// Token: 0x06000551 RID: 1361 RVA: 0x00062E74 File Offset: 0x00061074
	public Mob()
	{
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00063004 File Offset: 0x00061204
	public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, int hp, sbyte level, int maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
	{
		this.isDisable = isDisable;
		this.isDontMove = isDontMove;
		this.isFire = isFire;
		this.isIce = isIce;
		this.isWind = isWind;
		this.sys = sys;
		this.mobId = mobId;
		this.templateId = templateId;
		this.hp = hp;
		this.level = level;
		this.pointx = pointx;
		this.x = (int)pointx;
		this.xFirst = (int)pointx;
		this.pointy = pointy;
		this.y = (int)pointy;
		this.yFirst = (int)pointy;
		this.status = (int)status;
		bool flag = templateId != 70;
		if (flag)
		{
			this.checkData();
			this.getData();
		}
		bool flag2 = !Mob.isExistNewMob(templateId.ToString() + string.Empty);
		if (flag2)
		{
			Mob.newMob.addElement(templateId.ToString() + string.Empty);
		}
		this.maxHp = maxp;
		this.levelBoss = levelBoss;
		this.updateHp_bar();
		this.per_tem = (int)((long)hp * 100L / (long)this.maxHp);
		this.isDie = false;
		this.xSd = (int)pointx;
		this.ySd = (int)pointy;
		bool flag3 = this.isNewModStand();
		if (flag3)
		{
			this.stand = new int[]
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
				1,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.move = new int[]
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
				1,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.moveFast = new int[]
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
				1,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.attack1 = new int[]
			{
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5
			};
			this.attack2 = new int[]
			{
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5
			};
		}
		else
		{
			bool flag4 = this.isNewMod();
			if (flag4)
			{
				this.stand = new int[]
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
				this.move = new int[]
				{
					1,
					1,
					1,
					1,
					2,
					2,
					2,
					2,
					1,
					1,
					1,
					1,
					3,
					3,
					3,
					3
				};
				this.moveFast = new int[]
				{
					1,
					1,
					2,
					2,
					1,
					1,
					3,
					3
				};
				this.attack1 = new int[]
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
					6
				};
				this.attack2 = new int[]
				{
					7,
					7,
					7,
					8,
					8,
					8,
					9,
					9,
					9,
					9,
					9
				};
			}
			else
			{
				bool flag5 = this.isSpecial();
				if (flag5)
				{
					this.stand = new int[]
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
					this.move = new int[]
					{
						2,
						2,
						3,
						3,
						2,
						2,
						4,
						4,
						2,
						2,
						3,
						3,
						2,
						2,
						4,
						4
					};
					this.moveFast = new int[]
					{
						2,
						2,
						3,
						3,
						2,
						2,
						4,
						4
					};
					this.attack1 = new int[]
					{
						5,
						6,
						7,
						8,
						9,
						10,
						11,
						12
					};
					this.attack2 = new int[]
					{
						5,
						12,
						13,
						14
					};
				}
				else
				{
					this.stand = new int[]
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
					this.move = new int[]
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
					this.moveFast = new int[]
					{
						1,
						1,
						2,
						2,
						3,
						3,
						2
					};
					this.attack1 = new int[]
					{
						4,
						5,
						6
					};
					this.attack2 = new int[]
					{
						7,
						8,
						9
					};
				}
			}
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x000634C0 File Offset: 0x000616C0
	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000634F8 File Offset: 0x000616F8
	public void getData()
	{
		bool flag = Mob.arrMobTemplate[this.templateId].data == null;
		if (flag)
		{
			Mob.arrMobTemplate[this.templateId].data = new EffectData();
			string text = "/Mob/" + this.templateId.ToString();
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag2 = dataInputStream != null;
			if (flag2)
			{
				Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
				Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			bool flag3 = Mob.lastMob.size() > 15;
			if (flag3)
			{
				Mob.arrMobTemplate[int.Parse((string)Mob.lastMob.elementAt(0))].data = null;
				Mob.lastMob.removeElementAt(0);
			}
			Mob.lastMob.addElement(this.templateId.ToString() + string.Empty);
		}
		else
		{
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00002ED4 File Offset: 0x000010D4
	public virtual void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00002EE5 File Offset: 0x000010E5
	public virtual void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0006365C File Offset: 0x0006185C
	public static bool isExistNewMob(string id)
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

	// Token: 0x06000558 RID: 1368 RVA: 0x000636B0 File Offset: 0x000618B0
	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
		{
			bool flag = Mob.arrMobTemplate[i].data != null;
			if (flag)
			{
				num++;
			}
		}
		bool flag2 = num >= 10;
		if (flag2)
		{
			for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
			{
				bool flag3 = Mob.arrMobTemplate[j].data != null && num > 5;
				if (flag3)
				{
					Mob.arrMobTemplate[j].data = null;
				}
			}
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0006374C File Offset: 0x0006194C
	public void checkFrameTick(int[] array)
	{
		bool flag = this.tick > array.Length - 1;
		if (flag)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
		this.tick++;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00063790 File Offset: 0x00061990
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

	// Token: 0x0600055B RID: 1371 RVA: 0x000638E8 File Offset: 0x00061AE8
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		bool flag = TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4);
		if (flag)
		{
			g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
		}
		else
		{
			bool flag2 = TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0;
			if (flag2)
			{
				g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0;
				if (flag3)
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8);
					if (flag4)
					{
						g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
					}
				}
			}
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00063A58 File Offset: 0x00061C58
	public void updateSuperEff()
	{
		bool flag = this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0;
		if (flag)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		bool flag2 = this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0;
		if (flag2)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		bool flag3 = this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0;
		if (flag3)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00063AE0 File Offset: 0x00061CE0
	public virtual void update()
	{
		bool flag = this.isMafuba;
		if (!flag)
		{
			this.GetFrame();
			bool flag2 = this.blindEff && GameCanvas.gameTick % 5 == 0;
			if (flag2)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			bool flag3 = this.sleepEff && GameCanvas.gameTick % 10 == 0;
			if (flag3)
			{
				EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
			}
			bool flag4 = !GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0;
			if (flag4)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					bool flag5 = @char != null && @char.isFlyAndCharge && @char.cf == 32;
					if (flag5)
					{
						global::Char char2 = new global::Char();
						char2.cx = @char.cx;
						char2.cy = @char.cy - @char.ch;
						bool flag6 = @char.cgender == 0;
						if (flag6)
						{
							MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char2, 25);
						}
					}
				}
				bool flag7 = global::Char.myCharz().isFlyAndCharge && global::Char.myCharz().cf == 32;
				if (flag7)
				{
					global::Char char3 = new global::Char();
					char3.cx = global::Char.myCharz().cx;
					char3.cy = global::Char.myCharz().cy - global::Char.myCharz().ch;
					bool flag8 = global::Char.myCharz().cgender == 0;
					if (flag8)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char3, 25);
					}
				}
			}
			bool flag9 = this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0;
			if (flag9)
			{
				EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
			}
			bool flag10 = this.isFreez;
			if (flag10)
			{
				bool flag11 = GameCanvas.gameTick % 5 == 0;
				if (flag11)
				{
					ServerEffect.addServerEffect(113, this.x, this.y, 1);
				}
				long num = mSystem.currentTimeMillis();
				bool flag12 = num - this.last >= 1000L;
				if (flag12)
				{
					this.seconds--;
					this.last = num;
					bool flag13 = this.seconds < 0;
					if (flag13)
					{
						this.isFreez = false;
						this.seconds = 0;
					}
				}
				bool flag14 = this.isTypeNewMod();
				if (flag14)
				{
					this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
				}
				else
				{
					bool flag15 = this.isNewModStand();
					if (flag15)
					{
						this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
					}
					else
					{
						bool flag16 = this.isNewMod();
						if (flag16)
						{
							bool flag17 = GameCanvas.gameTick % 20 > 5;
							if (flag17)
							{
								this.frame = 11;
							}
							else
							{
								this.frame = 10;
							}
						}
						else
						{
							bool flag18 = this.isSpecial();
							if (flag18)
							{
								bool flag19 = GameCanvas.gameTick % 20 > 5;
								if (flag19)
								{
									this.frame = 1;
								}
								else
								{
									this.frame = 15;
								}
							}
							else
							{
								bool flag20 = GameCanvas.gameTick % 20 > 5;
								if (flag20)
								{
									this.frame = 11;
								}
								else
								{
									this.frame = 10;
								}
							}
						}
					}
				}
			}
			bool flag21 = !this.isUpdate();
			if (!flag21)
			{
				bool flag22 = this.isShadown;
				if (flag22)
				{
					this.updateShadown();
				}
				bool flag23 = this.vMobMove == null && Mob.arrMobTemplate[this.templateId].rangeMove != 0;
				if (!flag23)
				{
					bool flag24 = this.status != 3 && this.isBusyAttackSomeOne;
					if (flag24)
					{
						bool flag25 = this.cFocus != null;
						if (flag25)
						{
							this.cFocus.doInjure(this.dame, this.dameMp, false, true);
						}
						else
						{
							bool flag26 = this.mobToAttack != null;
							if (flag26)
							{
								this.mobToAttack.setInjure();
							}
						}
						this.isBusyAttackSomeOne = false;
					}
					bool flag27 = this.levelBoss > 0;
					if (flag27)
					{
						this.updateSuperEff();
					}
					switch (this.status)
					{
					case 1:
					{
						this.isDisable = false;
						this.isDontMove = false;
						this.isFire = false;
						this.isIce = false;
						this.isWind = false;
						this.y += this.p1;
						bool flag28 = GameCanvas.gameTick % 2 == 0;
						if (flag28)
						{
							bool flag29 = this.p2 > 1;
							if (flag29)
							{
								this.p2--;
							}
							else
							{
								bool flag30 = this.p2 < -1;
								if (flag30)
								{
									this.p2++;
								}
							}
						}
						this.x += this.p2;
						bool flag31 = this.isTypeNewMod();
						if (flag31)
						{
							this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
						}
						else
						{
							bool flag32 = this.isNewModStand();
							if (flag32)
							{
								this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
							}
							else
							{
								bool flag33 = this.isNewMod();
								if (flag33)
								{
									this.frame = 11;
								}
								else
								{
									bool flag34 = this.isSpecial();
									if (flag34)
									{
										this.frame = 15;
									}
									else
									{
										this.frame = 11;
									}
								}
							}
						}
						bool flag35 = this.isDie;
						if (flag35)
						{
							this.isDie = false;
							bool flag36 = this.isMobMe;
							if (flag36)
							{
								for (int j = 0; j < GameScr.vMob.size(); j++)
								{
									bool flag37 = ((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId;
									if (flag37)
									{
										GameScr.vMob.removeElementAt(j);
									}
								}
							}
							this.p1 = 0;
							this.p2 = 0;
							this.x = (this.y = 0);
							this.hp = this.getTemplate().hp;
							this.status = 0;
							this.timeStatus = 0;
						}
						else
						{
							bool flag38 = (TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2;
							if (flag38)
							{
								this.p1 = ((this.p1 <= 4) ? (-this.p1) : -4);
								bool flag39 = this.p3 == 0;
								if (flag39)
								{
									this.p3 = 16;
								}
							}
							else
							{
								this.p1++;
							}
							bool flag40 = this.p3 > 0;
							if (flag40)
							{
								this.p3--;
								bool flag41 = this.p3 == 0;
								if (flag41)
								{
									this.isDie = true;
								}
							}
						}
						break;
					}
					case 2:
					{
						bool flag42 = this.holdEffID != 0;
						if (!flag42)
						{
							bool flag43 = this.isFreez;
							if (!flag43)
							{
								bool flag44 = this.blindEff;
								if (!flag44)
								{
									bool flag45 = this.sleepEff;
									if (!flag45)
									{
										this.timeStatus = 0;
										this.updateMobStandWait();
									}
								}
							}
						}
						break;
					}
					case 3:
					{
						bool flag46 = this.holdEffID != 0;
						if (!flag46)
						{
							bool flag47 = this.blindEff;
							if (!flag47)
							{
								bool flag48 = this.sleepEff;
								if (!flag48)
								{
									bool flag49 = this.isFreez;
									if (!flag49)
									{
										this.updateMobAttack();
									}
								}
							}
						}
						break;
					}
					case 4:
					{
						bool flag50 = this.holdEffID != 0;
						if (!flag50)
						{
							bool flag51 = this.blindEff;
							if (!flag51)
							{
								bool flag52 = this.sleepEff;
								if (!flag52)
								{
									bool flag53 = this.isFreez;
									if (!flag53)
									{
										this.timeStatus = 0;
										this.p1++;
										bool flag54 = this.p1 > 40 + this.mobId % 5;
										if (flag54)
										{
											this.y -= 2;
											this.status = 5;
											this.p1 = 0;
										}
									}
								}
							}
						}
						break;
					}
					case 5:
					{
						bool flag55 = this.holdEffID != 0;
						if (!flag55)
						{
							bool flag56 = this.blindEff;
							if (!flag56)
							{
								bool flag57 = this.sleepEff;
								if (!flag57)
								{
									bool flag58 = this.isFreez;
									if (flag58)
									{
										bool flag59 = Mob.arrMobTemplate[this.templateId].type == 4;
										if (flag59)
										{
											this.ty++;
											this.wt++;
											this.fy += (this.wy ? -1 : 1);
											bool flag60 = this.wt == 10;
											if (flag60)
											{
												this.wt = 0;
												this.wy = !this.wy;
											}
										}
									}
									else
									{
										this.timeStatus = 0;
										this.updateMobWalk();
									}
								}
							}
						}
						break;
					}
					case 6:
					{
						this.timeStatus = 0;
						this.p1++;
						this.y += this.p1;
						bool flag61 = this.y >= this.yFirst;
						if (flag61)
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
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000644F0 File Offset: 0x000626F0
	public void setInjure()
	{
		bool flag = this.hp > 0 && this.status != 3 && this.status != 7;
		if (flag)
		{
			this.timeStatus = 4;
			this.status = 7;
			bool flag2 = this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30;
			if (flag2)
			{
				this.x -= 10 * this.dir;
			}
		}
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00064578 File Offset: 0x00062778
	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BigBoss;
			if (flag)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x000645D0 File Offset: 0x000627D0
	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BigBoss2;
			if (flag)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00064628 File Offset: 0x00062828
	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BachTuoc;
			if (flag)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00064680 File Offset: 0x00062880
	public static NewBoss getNewBoss(sbyte idBoss)
	{
		Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
		bool flag = mob is NewBoss;
		NewBoss result;
		if (flag)
		{
			result = (NewBoss)mob;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x000646BC File Offset: 0x000628BC
	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BigBoss;
			if (flag)
			{
				GameScr.vMob.removeElement(mob);
				break;
			}
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00064714 File Offset: 0x00062914
	public void setAttack(global::Char cFocus)
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
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x000647C4 File Offset: 0x000629C4
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00064804 File Offset: 0x00062A04
	private bool isNewModStand()
	{
		return this.templateId == 76;
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00064820 File Offset: 0x00062A20
	private bool isNewMod()
	{
		return this.templateId >= 73 && !this.isNewModStand();
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00064848 File Offset: 0x00062A48
	private void updateInjure()
	{
		bool flag = !this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0;
		if (flag)
		{
			bool flag2 = this.isTypeNewMod();
			if (flag2)
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else
			{
				bool flag3 = this.isNewModStand();
				if (flag3)
				{
					this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
				}
				else
				{
					bool flag4 = this.isNewMod();
					if (flag4)
					{
						bool flag5 = this.frame != 10;
						if (flag5)
						{
							this.frame = 10;
						}
						else
						{
							this.frame = 11;
						}
					}
					else
					{
						bool flag6 = this.isSpecial();
						if (flag6)
						{
							bool flag7 = this.frame != 1;
							if (flag7)
							{
								this.frame = 1;
							}
							else
							{
								this.frame = 15;
							}
						}
						else
						{
							bool flag8 = this.frame != 10;
							if (flag8)
							{
								this.frame = 10;
							}
							else
							{
								this.frame = 11;
							}
						}
					}
				}
			}
		}
		this.timeStatus--;
		bool flag9 = this.timeStatus <= 0 && (this.isTypeNewMod() || this.isNewModStand() || (this.isNewMod() && this.frame == 11) || (this.isSpecial() && this.frame == 15) || (this.templateId < 58 && this.frame == 11));
		if (flag9)
		{
			bool flag10 = (this.injureBy != null && this.injureThenDie) || this.hp == 0;
			if (flag10)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 1;
				this.p1 = -3;
				this.p3 = 0;
			}
			else
			{
				this.status = 5;
				bool flag11 = this.injureBy != null;
				if (flag11)
				{
					this.dir = -this.injureBy.cdir;
					bool flag12 = Res.abs(this.x - this.injureBy.cx) < 24;
					if (flag12)
					{
						this.status = 2;
					}
				}
				this.p1 = (this.p2 = (this.p3 = 0));
				this.timeStatus = 0;
			}
			this.injureBy = null;
		}
		else
		{
			bool flag13 = Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null;
			if (flag13)
			{
				int num = -this.injureBy.cdir << 1;
				bool flag14 = this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
				if (flag14)
				{
					this.x -= num;
				}
			}
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00064B3C File Offset: 0x00062D3C
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		sbyte type = Mob.arrMobTemplate[this.templateId].type;
		sbyte b = type;
		if (b > 3)
		{
			if (b - 4 <= 1)
			{
				this.p1++;
				bool flag = this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80);
				if (flag)
				{
					this.status = 5;
				}
			}
		}
		else
		{
			this.p1++;
			bool flag2 = this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80);
			if (flag2)
			{
				this.status = 5;
			}
		}
		bool flag3 = this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0;
		if (flag3)
		{
			bool flag4 = this.cFocus.cx > this.x;
			if (flag4)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		else
		{
			bool flag5 = this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0;
			if (flag5)
			{
				bool flag6 = this.mobToAttack.x > this.x;
				if (flag6)
				{
					this.dir = 1;
				}
				else
				{
					this.dir = -1;
				}
			}
		}
		bool flag7 = this.forceWait > 0;
		if (flag7)
		{
			this.forceWait--;
			this.status = 2;
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00064D48 File Offset: 0x00062F48
	public void updateMobAttack()
	{
		int[] array = (this.p3 != 0) ? this.attack2 : this.attack1;
		bool flag = this.tick < array.Length;
		if (flag)
		{
			this.checkFrameTick(array);
			bool flag2 = this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0;
			if (flag2)
			{
				SoundMn.gI().charPunch(false, 0.05f);
			}
		}
		bool flag3 = this.p1 == 0;
		if (flag3)
		{
			int num = (this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx;
			int num2 = (this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy;
			bool flag4 = !this.isNewMod();
			if (flag4)
			{
				bool flag5 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
				if (flag5)
				{
					this.p1 = 1;
				}
				bool flag6 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
				if (flag6)
				{
					this.p1 = 1;
				}
			}
			bool flag7 = (Mob.arrMobTemplate[this.templateId].type == 4 || Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove;
			if (flag7)
			{
				this.y += (num2 - this.y) / 20;
			}
			this.p2++;
			bool flag8 = this.p2 > array.Length - 1 || this.p1 == 1;
			if (flag8)
			{
				this.p1 = 1;
				bool flag9 = this.p3 == 0;
				if (flag9)
				{
					bool flag10 = this.cFocus != null;
					if (flag10)
					{
						this.cFocus.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						this.mobToAttack.setInjure();
					}
					this.isBusyAttackSomeOne = false;
				}
				else
				{
					bool flag11 = this.cFocus != null;
					if (flag11)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, this.cFocus, (int)this.getTemplate().dartType);
					}
					else
					{
						global::Char @char = new global::Char();
						@char.cx = this.mobToAttack.x;
						@char.cy = this.mobToAttack.y;
						@char.charID = -100;
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, @char, (int)this.getTemplate().dartType);
					}
					this.isBusyAttackSomeOne = false;
				}
			}
			this.dir = ((this.x >= num) ? -1 : 1);
		}
		else
		{
			bool flag12 = this.p1 == 1;
			if (flag12)
			{
				bool flag13 = Mob.arrMobTemplate[this.templateId].type == 0 || this.isDontMove || this.isIce || !this.isWind;
				if (flag13)
				{
				}
				bool flag14 = this.tick == array.Length;
				if (flag14)
				{
					this.status = 2;
					this.p1 = 0;
					this.p2 = 0;
					this.tick = 0;
				}
			}
		}
		bool flag15 = this.tick == 5 && this.cFocus != null && this.cFocus.charID == global::Char.myCharz().charID;
		if (flag15)
		{
			bool flag16 = this.templateId == 88 && this.p3 != 0;
			if (flag16)
			{
				GameScr.shock_scr = 2;
			}
			bool flag17 = this.templateId == 89;
			if (flag17)
			{
				GameScr.shock_scr = 2;
			}
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00065150 File Offset: 0x00063350
	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			bool flag = this.injureThenDie;
			if (flag)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 3;
				this.p1 = -5;
				this.p3 = 0;
			}
			num = 1;
			bool flag2 = !this.isIce;
			if (flag2)
			{
				bool flag3 = this.isDontMove || this.isWind;
				if (flag3)
				{
					this.checkFrameTick(this.stand);
				}
				else
				{
					switch (Mob.arrMobTemplate[this.templateId].type)
					{
					case 0:
					{
						bool flag4 = this.isNewModStand();
						if (flag4)
						{
							this.frame = this.stand[GameCanvas.gameTick % this.stand.Length];
						}
						else
						{
							this.frame = 0;
						}
						num = 2;
						break;
					}
					case 1:
					case 2:
					case 3:
					{
						num = 3;
						sbyte b = Mob.arrMobTemplate[this.templateId].speed;
						bool flag5 = b == 1;
						if (flag5)
						{
							bool flag6 = GameCanvas.gameTick % 2 == 1;
							if (flag6)
							{
								break;
							}
						}
						else
						{
							bool flag7 = b > 2;
							if (flag7)
							{
								b += (sbyte)(this.mobId % 2);
							}
							else
							{
								bool flag8 = GameCanvas.gameTick % 2 == 1;
								if (flag8)
								{
									b -= 1;
								}
							}
						}
						this.x += (int)b * this.dir;
						bool flag9 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag9)
						{
							this.dir = -1;
						}
						else
						{
							bool flag10 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
							if (flag10)
							{
								this.dir = 1;
							}
						}
						bool flag11 = Res.abs(this.x - global::Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag11)
						{
							this.dir = ((this.x <= global::Char.myCharz().cx) ? 1 : -1);
							bool flag12 = Res.abs(this.x - global::Char.myCharz().cx) < 20;
							if (flag12)
							{
								this.x -= this.dir * 10;
							}
							this.status = 2;
							this.forceWait = 20;
						}
						this.checkFrameTick((this.w <= 30) ? this.moveFast : this.move);
						break;
					}
					case 4:
					{
						num = 4;
						sbyte b2 = Mob.arrMobTemplate[this.templateId].speed;
						b2 += (sbyte)(this.mobId % 2);
						this.x += (int)b2 * this.dir;
						bool flag13 = GameCanvas.gameTick % 10 > 2;
						if (flag13)
						{
							this.y += (int)b2 * this.dirV;
						}
						b2 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						bool flag14 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag14)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else
						{
							bool flag15 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
							if (flag15)
							{
								this.dir = 1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
						}
						bool flag16 = this.y > this.yFirst + 24;
						if (flag16)
						{
							this.dirV = -1;
						}
						else
						{
							bool flag17 = this.y < this.yFirst - (20 + GameCanvas.gameTick % 10);
							if (flag17)
							{
								this.dirV = 1;
							}
						}
						this.checkFrameTick(this.move);
						break;
					}
					case 5:
					{
						num = 5;
						sbyte b3 = Mob.arrMobTemplate[this.templateId].speed;
						b3 += (sbyte)(this.mobId % 2);
						this.x += (int)b3 * this.dir;
						b3 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						bool flag18 = GameCanvas.gameTick % 10 > 2;
						if (flag18)
						{
							this.y += (int)b3 * this.dirV;
						}
						bool flag19 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag19)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else
						{
							bool flag20 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
							if (flag20)
							{
								this.dir = 1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
						}
						bool flag21 = this.y > this.yFirst + 24;
						if (flag21)
						{
							this.dirV = -1;
						}
						else
						{
							bool flag22 = this.y < this.yFirst - (20 + GameCanvas.gameTick % 10);
							if (flag22)
							{
								this.dirV = 1;
							}
						}
						bool flag23 = TileMap.tileTypeAt(this.x, this.y, 2);
						if (flag23)
						{
							bool flag24 = GameCanvas.gameTick % 10 > 5;
							if (flag24)
							{
								this.y = TileMap.tileYofPixel(this.y);
								this.status = 4;
								this.p1 = 0;
								this.dirV = -1;
							}
							else
							{
								this.dirV = -1;
							}
						}
						break;
					}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("lineee: " + num.ToString());
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00065778 File Offset: 0x00063978
	public MobTemplate getTemplate()
	{
		return Mob.arrMobTemplate[this.templateId];
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00065798 File Offset: 0x00063998
	public bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00065834 File Offset: 0x00063A34
	public bool isUpdate()
	{
		return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00065874 File Offset: 0x00063A74
	public bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0006589C File Offset: 0x00063A9C
	public void updateHp_bar()
	{
		this.len = (int)((long)this.hp * 100L / (long)this.maxHp * (long)this.w_hp_bar) / 100;
		this.per = (int)((long)this.hp * 100L / (long)this.maxHp);
		bool flag = this.per == 100;
		if (flag)
		{
			this.per_tem = this.per;
		}
		bool flag2 = this.per >= 100;
		if (flag2)
		{
			this.per_tem = this.per;
		}
		this.offset = 0;
		bool flag3 = this.per < 30;
		if (flag3)
		{
			this.color = 15473700;
			this.imgHPtem = GameScr.imgHP_tm_do;
		}
		else
		{
			bool flag4 = this.per < 60;
			if (flag4)
			{
				this.color = 16744448;
				this.imgHPtem = GameScr.imgHP_tm_vang;
			}
			else
			{
				this.color = 11992374;
				this.imgHPtem = GameScr.imgHP_tm_xanh;
			}
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00065994 File Offset: 0x00063B94
	public virtual void paint(mGraphics g)
	{
		bool flag = this.isHide;
		if (!flag)
		{
			bool flag2 = this.isMafuba;
			if (flag2)
			{
				bool flag3 = !this.changBody;
				if (flag3)
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
				bool flag4 = this.isShadown && this.status != 0;
				if (flag4)
				{
					this.paintShadow(g);
				}
				bool flag5 = !this.isPaint();
				if (!flag5)
				{
					bool flag6 = this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0;
					if (!flag6)
					{
						g.translate(0, GameCanvas.transY);
						bool flag7 = !this.changBody;
						if (flag7)
						{
							Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
						}
						else
						{
							SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						g.translate(0, -GameCanvas.transY);
						bool flag8 = global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.Equals(this) && this.status != 1;
						if (flag8)
						{
							bool flag9 = this.hp > 0;
							if (flag9)
							{
								bool flag10 = this.imgHPtem != null;
								if (flag10)
								{
									int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
									int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
									int num = imageWidth * this.per / 100;
									int num2 = num;
									bool flag11 = this.per_tem >= this.per;
									if (flag11)
									{
										int num3 = imageWidth;
										int num4 = this.per_tem;
										int num6;
										if (GameCanvas.gameTick % 6 > 3)
										{
											int num5 = this.offset;
											this.offset = num5 + 1;
											num6 = num5;
										}
										else
										{
											num6 = this.offset;
										}
										num2 = num3 * (this.per_tem = num4 - num6) / 100;
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
									g.drawImage(GameScr.imgHP_tm_xam, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
									g.setColor(16777215);
									g.fillRect(this.x - (imageWidth >> 1), this.y - this.h - 5, num2, 2);
									g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00065D28 File Offset: 0x00063F28
	public int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00065D40 File Offset: 0x00063F40
	public void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00065D94 File Offset: 0x00063F94
	public void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x <= this.x) ? -1 : 1);
		int num = mobToAttack.x;
		int num2 = mobToAttack.y;
		bool flag = Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2;
		if (flag)
		{
			bool flag2 = this.x < num;
			if (flag2)
			{
				this.x = num - this.w;
			}
			else
			{
				this.x = num + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00065E74 File Offset: 0x00064074
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00065E8C File Offset: 0x0006408C
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00065EA4 File Offset: 0x000640A4
	public int getH()
	{
		return this.h;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00065EBC File Offset: 0x000640BC
	public int getW()
	{
		return this.w;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00065ED4 File Offset: 0x000640D4
	public void stopMoving()
	{
		bool flag = this.status == 5;
		if (flag)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00065F1C File Offset: 0x0006411C
	public bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00065F44 File Offset: 0x00064144
	public void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00065F68 File Offset: 0x00064168
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00065F72 File Offset: 0x00064172
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00065F7C File Offset: 0x0006417C
	public void GetFrame()
	{
		bool flag = this.isGetFr && this.isTypeNewMod() && Mob.arrMobTemplate[this.templateId].data != null;
		if (flag)
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId.ToString() + string.Empty);
			this.stand = this.frameArr[0];
			this.move = this.frameArr[1];
			this.moveFast = this.frameArr[2];
			this.attack1 = this.frameArr[3];
			this.attack2 = this.frameArr[4];
			this.hurt = this.frameArr[5];
			this.isGetFr = false;
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00066040 File Offset: 0x00064240
	private bool isTypeNewMod()
	{
		return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
	}

	// Token: 0x04000B54 RID: 2900
	public const sbyte TYPE_DUNG = 0;

	// Token: 0x04000B55 RID: 2901
	public const sbyte TYPE_DI = 1;

	// Token: 0x04000B56 RID: 2902
	public const sbyte TYPE_NHAY = 2;

	// Token: 0x04000B57 RID: 2903
	public const sbyte TYPE_LET = 3;

	// Token: 0x04000B58 RID: 2904
	public const sbyte TYPE_BAY = 4;

	// Token: 0x04000B59 RID: 2905
	public const sbyte TYPE_BAY_DAU = 5;

	// Token: 0x04000B5A RID: 2906
	public static MobTemplate[] arrMobTemplate;

	// Token: 0x04000B5B RID: 2907
	public const sbyte MA_INHELL = 0;

	// Token: 0x04000B5C RID: 2908
	public const sbyte MA_DEADFLY = 1;

	// Token: 0x04000B5D RID: 2909
	public const sbyte MA_STANDWAIT = 2;

	// Token: 0x04000B5E RID: 2910
	public const sbyte MA_ATTACK = 3;

	// Token: 0x04000B5F RID: 2911
	public const sbyte MA_STANDFLY = 4;

	// Token: 0x04000B60 RID: 2912
	public const sbyte MA_WALK = 5;

	// Token: 0x04000B61 RID: 2913
	public const sbyte MA_FALL = 6;

	// Token: 0x04000B62 RID: 2914
	public const sbyte MA_INJURE = 7;

	// Token: 0x04000B63 RID: 2915
	public bool changBody;

	// Token: 0x04000B64 RID: 2916
	public short smallBody;

	// Token: 0x04000B65 RID: 2917
	public bool isHintFocus;

	// Token: 0x04000B66 RID: 2918
	public string flystring;

	// Token: 0x04000B67 RID: 2919
	public int flyx;

	// Token: 0x04000B68 RID: 2920
	public int flyy;

	// Token: 0x04000B69 RID: 2921
	public int flyIndex;

	// Token: 0x04000B6A RID: 2922
	public bool isFreez;

	// Token: 0x04000B6B RID: 2923
	public int seconds;

	// Token: 0x04000B6C RID: 2924
	public long last;

	// Token: 0x04000B6D RID: 2925
	public long cur;

	// Token: 0x04000B6E RID: 2926
	public int holdEffID;

	// Token: 0x04000B6F RID: 2927
	public int hp;

	// Token: 0x04000B70 RID: 2928
	public int maxHp;

	// Token: 0x04000B71 RID: 2929
	public int x;

	// Token: 0x04000B72 RID: 2930
	public int y;

	// Token: 0x04000B73 RID: 2931
	public int dir = 1;

	// Token: 0x04000B74 RID: 2932
	public int dirV = 1;

	// Token: 0x04000B75 RID: 2933
	public int status;

	// Token: 0x04000B76 RID: 2934
	public int p1;

	// Token: 0x04000B77 RID: 2935
	public int p2;

	// Token: 0x04000B78 RID: 2936
	public int p3;

	// Token: 0x04000B79 RID: 2937
	public int xFirst;

	// Token: 0x04000B7A RID: 2938
	public int yFirst;

	// Token: 0x04000B7B RID: 2939
	public int vy;

	// Token: 0x04000B7C RID: 2940
	public int exp;

	// Token: 0x04000B7D RID: 2941
	public int w;

	// Token: 0x04000B7E RID: 2942
	public int h;

	// Token: 0x04000B7F RID: 2943
	public int hpInjure;

	// Token: 0x04000B80 RID: 2944
	public int charIndex;

	// Token: 0x04000B81 RID: 2945
	public int timeStatus;

	// Token: 0x04000B82 RID: 2946
	public int mobId;

	// Token: 0x04000B83 RID: 2947
	public bool isx;

	// Token: 0x04000B84 RID: 2948
	public bool isy;

	// Token: 0x04000B85 RID: 2949
	public bool isDisable;

	// Token: 0x04000B86 RID: 2950
	public bool isDontMove;

	// Token: 0x04000B87 RID: 2951
	public bool isFire;

	// Token: 0x04000B88 RID: 2952
	public bool isIce;

	// Token: 0x04000B89 RID: 2953
	public bool isWind;

	// Token: 0x04000B8A RID: 2954
	public bool isDie;

	// Token: 0x04000B8B RID: 2955
	public MyVector vMobMove = new MyVector();

	// Token: 0x04000B8C RID: 2956
	public bool isGo;

	// Token: 0x04000B8D RID: 2957
	public string mobName;

	// Token: 0x04000B8E RID: 2958
	public int templateId;

	// Token: 0x04000B8F RID: 2959
	public short pointx;

	// Token: 0x04000B90 RID: 2960
	public short pointy;

	// Token: 0x04000B91 RID: 2961
	public global::Char cFocus;

	// Token: 0x04000B92 RID: 2962
	public int dame;

	// Token: 0x04000B93 RID: 2963
	public int dameMp;

	// Token: 0x04000B94 RID: 2964
	public int sys;

	// Token: 0x04000B95 RID: 2965
	public sbyte levelBoss;

	// Token: 0x04000B96 RID: 2966
	public sbyte level;

	// Token: 0x04000B97 RID: 2967
	public bool isBoss;

	// Token: 0x04000B98 RID: 2968
	public bool isMobMe;

	// Token: 0x04000B99 RID: 2969
	public static MyVector lastMob = new MyVector();

	// Token: 0x04000B9A RID: 2970
	public static MyVector newMob = new MyVector();

	// Token: 0x04000B9B RID: 2971
	public bool isMafuba;

	// Token: 0x04000B9C RID: 2972
	public int xMFB;

	// Token: 0x04000B9D RID: 2973
	public int yMFB;

	// Token: 0x04000B9E RID: 2974
	public int xSd;

	// Token: 0x04000B9F RID: 2975
	public int ySd;

	// Token: 0x04000BA0 RID: 2976
	private bool isOutMap;

	// Token: 0x04000BA1 RID: 2977
	private int wCount;

	// Token: 0x04000BA2 RID: 2978
	public bool isShadown = true;

	// Token: 0x04000BA3 RID: 2979
	private int tick;

	// Token: 0x04000BA4 RID: 2980
	private int frame;

	// Token: 0x04000BA5 RID: 2981
	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000BA6 RID: 2982
	private bool wy;

	// Token: 0x04000BA7 RID: 2983
	private int wt;

	// Token: 0x04000BA8 RID: 2984
	private int fy;

	// Token: 0x04000BA9 RID: 2985
	private int ty;

	// Token: 0x04000BAA RID: 2986
	public int typeSuperEff;

	// Token: 0x04000BAB RID: 2987
	public bool isBusyAttackSomeOne = true;

	// Token: 0x04000BAC RID: 2988
	public int[] stand = new int[]
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

	// Token: 0x04000BAD RID: 2989
	public int[] move = new int[]
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

	// Token: 0x04000BAE RID: 2990
	public int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04000BAF RID: 2991
	public int[] attack1 = new int[]
	{
		4,
		5,
		6
	};

	// Token: 0x04000BB0 RID: 2992
	public int[] attack2 = new int[]
	{
		7,
		8,
		9
	};

	// Token: 0x04000BB1 RID: 2993
	public int[] hurt = new int[1];

	// Token: 0x04000BB2 RID: 2994
	private int color = 8421504;

	// Token: 0x04000BB3 RID: 2995
	public int len = 24;

	// Token: 0x04000BB4 RID: 2996
	public int w_hp_bar = 24;

	// Token: 0x04000BB5 RID: 2997
	public int per = 100;

	// Token: 0x04000BB6 RID: 2998
	public int per_tem = 100;

	// Token: 0x04000BB7 RID: 2999
	public byte h_hp_bar = 4;

	// Token: 0x04000BB8 RID: 3000
	public Image imgHPtem;

	// Token: 0x04000BB9 RID: 3001
	private int offset;

	// Token: 0x04000BBA RID: 3002
	public bool isHide;

	// Token: 0x04000BBB RID: 3003
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000BBC RID: 3004
	public global::Char injureBy;

	// Token: 0x04000BBD RID: 3005
	public bool injureThenDie;

	// Token: 0x04000BBE RID: 3006
	public Mob mobToAttack;

	// Token: 0x04000BBF RID: 3007
	public int forceWait;

	// Token: 0x04000BC0 RID: 3008
	public bool blindEff;

	// Token: 0x04000BC1 RID: 3009
	public bool sleepEff;

	// Token: 0x04000BC2 RID: 3010
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
		}
	};

	// Token: 0x04000BC3 RID: 3011
	private bool isGetFr = true;
}
