using System;
using Assets.src.g;

// Token: 0x020000B7 RID: 183
public class Mob : IMapObject
{
	// Token: 0x0600081E RID: 2078 RVA: 0x00055B08 File Offset: 0x00053F08
	public Mob()
	{
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00055C94 File Offset: 0x00054094
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
		if (templateId != 70)
		{
			this.checkData();
			this.getData();
		}
		if (!Mob.isExistNewMob(templateId + string.Empty))
		{
			Mob.newMob.addElement(templateId + string.Empty);
		}
		this.maxHp = maxp;
		this.levelBoss = levelBoss;
		this.updateHp_bar();
		this.per_tem = (int)((long)hp * 100L / (long)this.maxHp);
		this.isDie = false;
		this.xSd = (int)pointx;
		this.ySd = (int)pointy;
		if (this.isNewModStand())
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
		else if (this.isNewMod())
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
		else if (this.isSpecial())
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

	// Token: 0x06000820 RID: 2080 RVA: 0x00056141 File Offset: 0x00054541
	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00056170 File Offset: 0x00054570
	public void getData()
	{
		if (Mob.arrMobTemplate[this.templateId].data == null)
		{
			Mob.arrMobTemplate[this.templateId].data = new EffectData();
			string text = "/Mob/" + this.templateId;
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
				Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			if (Mob.lastMob.size() > 15)
			{
				Mob.arrMobTemplate[int.Parse((string)Mob.lastMob.elementAt(0))].data = null;
				Mob.lastMob.removeElementAt(0);
			}
			Mob.lastMob.addElement(this.templateId + string.Empty);
		}
		else
		{
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x000562C1 File Offset: 0x000546C1
	public virtual void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x000562D1 File Offset: 0x000546D1
	public virtual void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x000562DC File Offset: 0x000546DC
	public static bool isExistNewMob(string id)
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

	// Token: 0x06000825 RID: 2085 RVA: 0x00056324 File Offset: 0x00054724
	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
		{
			if (Mob.arrMobTemplate[i].data != null)
			{
				num++;
			}
		}
		if (num >= 10)
		{
			for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
			{
				if (Mob.arrMobTemplate[j].data != null && num > 5)
				{
					Mob.arrMobTemplate[j].data = null;
				}
			}
		}
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x000563A5 File Offset: 0x000547A5
	public void checkFrameTick(int[] array)
	{
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
		this.tick++;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x000563DC File Offset: 0x000547DC
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

	// Token: 0x06000828 RID: 2088 RVA: 0x00056514 File Offset: 0x00054914
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
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
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00056674 File Offset: 0x00054A74
	public void updateSuperEff()
	{
		if (this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		if (this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000566EC File Offset: 0x00054AEC
	public virtual void update()
	{
		if (this.isMafuba)
		{
			return;
		}
		this.GetFrame();
		if (this.blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, this.x, this.y, 1);
		}
		if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
		}
		if (!GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
				{
					global::Char char2 = new global::Char();
					char2.cx = @char.cx;
					char2.cy = @char.cy - @char.ch;
					if (@char.cgender == 0)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char2, 25);
					}
				}
			}
			if (global::Char.myCharz().isFlyAndCharge && global::Char.myCharz().cf == 32)
			{
				global::Char char3 = new global::Char();
				char3.cx = global::Char.myCharz().cx;
				char3.cy = global::Char.myCharz().cy - global::Char.myCharz().ch;
				if (global::Char.myCharz().cgender == 0)
				{
					MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char3, 25);
				}
			}
		}
		if (this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
		{
			EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
		}
		if (this.isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			long num = mSystem.currentTimeMillis();
			if (num - this.last >= 1000L)
			{
				this.seconds--;
				this.last = num;
				if (this.seconds < 0)
				{
					this.isFreez = false;
					this.seconds = 0;
				}
			}
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 11;
				}
				else
				{
					this.frame = 10;
				}
			}
			else if (this.isSpecial())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (GameCanvas.gameTick % 20 > 5)
			{
				this.frame = 11;
			}
			else
			{
				this.frame = 10;
			}
		}
		if (!this.isUpdate())
		{
			return;
		}
		if (this.isShadown)
		{
			this.updateShadown();
		}
		if (this.vMobMove == null && (int)Mob.arrMobTemplate[this.templateId].rangeMove != 0)
		{
			return;
		}
		if (this.status != 3 && this.isBusyAttackSomeOne)
		{
			if (this.cFocus != null)
			{
				this.cFocus.doInjure(this.dame, this.dameMp, false, true);
			}
			else if (this.mobToAttack != null)
			{
				this.mobToAttack.setInjure();
			}
			this.isBusyAttackSomeOne = false;
		}
		if ((int)this.levelBoss > 0)
		{
			this.updateSuperEff();
		}
		switch (this.status)
		{
		case 1:
			this.isDisable = false;
			this.isDontMove = false;
			this.isFire = false;
			this.isIce = false;
			this.isWind = false;
			this.y += this.p1;
			if (GameCanvas.gameTick % 2 == 0)
			{
				if (this.p2 > 1)
				{
					this.p2--;
				}
				else if (this.p2 < -1)
				{
					this.p2++;
				}
			}
			this.x += this.p2;
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				this.frame = 11;
			}
			else if (this.isSpecial())
			{
				this.frame = 15;
			}
			else
			{
				this.frame = 11;
			}
			if (this.isDie)
			{
				this.isDie = false;
				if (this.isMobMe)
				{
					for (int j = 0; j < GameScr.vMob.size(); j++)
					{
						if (((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId)
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
				return;
			}
			if ((TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2)
			{
				this.p1 = ((this.p1 <= 4) ? (-this.p1) : -4);
				if (this.p3 == 0)
				{
					this.p3 = 16;
				}
			}
			else
			{
				this.p1++;
			}
			if (this.p3 > 0)
			{
				this.p3--;
				if (this.p3 == 0)
				{
					this.isDie = true;
				}
			}
			break;
		case 2:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.isFreez)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			this.timeStatus = 0;
			this.updateMobStandWait();
			break;
		case 3:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (this.isFreez)
			{
				return;
			}
			this.updateMobAttack();
			break;
		case 4:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (this.isFreez)
			{
				return;
			}
			this.timeStatus = 0;
			this.p1++;
			if (this.p1 > 40 + this.mobId % 5)
			{
				this.y -= 2;
				this.status = 5;
				this.p1 = 0;
			}
			break;
		case 5:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (this.isFreez)
			{
				if ((int)Mob.arrMobTemplate[this.templateId].type == 4)
				{
					this.ty++;
					this.wt++;
					this.fy += (this.wy ? -1 : 1);
					if (this.wt == 10)
					{
						this.wt = 0;
						this.wy = !this.wy;
					}
				}
				return;
			}
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

	// Token: 0x0600082B RID: 2091 RVA: 0x00056FB4 File Offset: 0x000553B4
	public void setInjure()
	{
		if (this.hp > 0 && this.status != 3 && this.status != 7)
		{
			this.timeStatus = 4;
			this.status = 7;
			if ((int)this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30)
			{
				this.x -= 10 * this.dir;
			}
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00057034 File Offset: 0x00055434
	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00057080 File Offset: 0x00055480
	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss2)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000570CC File Offset: 0x000554CC
	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BachTuoc)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00057118 File Offset: 0x00055518
	public static NewBoss getNewBoss(sbyte idBoss)
	{
		Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
		if (mob is NewBoss)
		{
			return (NewBoss)mob;
		}
		return null;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0005714C File Offset: 0x0005554C
	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				GameScr.vMob.removeElement(mob);
				return;
			}
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0005719C File Offset: 0x0005559C
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
		if (Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2)
		{
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0005724D File Offset: 0x0005564D
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00057286 File Offset: 0x00055686
	private bool isNewModStand()
	{
		return this.templateId == 76;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00057292 File Offset: 0x00055692
	private bool isNewMod()
	{
		return this.templateId >= 73 && !this.isNewModStand();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x000572B0 File Offset: 0x000556B0
	private void updateInjure()
	{
		if (!this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
		{
			if (this.isTypeNewMod())
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else if (this.isNewModStand())
			{
				this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
			}
			else if (this.isNewMod())
			{
				if (this.frame != 10)
				{
					this.frame = 10;
				}
				else
				{
					this.frame = 11;
				}
			}
			else if (this.isSpecial())
			{
				if (this.frame != 1)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (this.frame != 10)
			{
				this.frame = 10;
			}
			else
			{
				this.frame = 11;
			}
		}
		this.timeStatus--;
		if (this.timeStatus <= 0 && (this.isTypeNewMod() || this.isNewModStand() || (this.isNewMod() && this.frame == 11) || (this.isSpecial() && this.frame == 15) || (this.templateId < 58 && this.frame == 11)))
		{
			if ((this.injureBy != null && this.injureThenDie) || this.hp == 0)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 1;
				this.p1 = -3;
				this.p3 = 0;
			}
			else
			{
				this.status = 5;
				if (this.injureBy != null)
				{
					this.dir = -this.injureBy.cdir;
					if (Res.abs(this.x - this.injureBy.cx) < 24)
					{
						this.status = 2;
					}
				}
				this.p1 = (this.p2 = (this.p3 = 0));
				this.timeStatus = 0;
			}
			this.injureBy = null;
			return;
		}
		if ((int)Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null)
		{
			int num = -this.injureBy.cdir << 1;
			if (this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
			{
				this.x -= num;
			}
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00057570 File Offset: 0x00055970
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		switch (Mob.arrMobTemplate[this.templateId].type)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			this.p1++;
			if (this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
			{
				this.status = 5;
			}
			break;
		case 4:
		case 5:
			this.p1++;
			if (this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
			{
				this.status = 5;
			}
			break;
		}
		if (this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.cFocus.cx > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		else if (this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.mobToAttack.x > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		if (this.forceWait > 0)
		{
			this.forceWait--;
			this.status = 2;
		}
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0005777C File Offset: 0x00055B7C
	public void updateMobAttack()
	{
		int[] array = (this.p3 != 0) ? this.attack2 : this.attack1;
		if (this.tick < array.Length)
		{
			this.checkFrameTick(array);
			if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().charPunch(false, 0.05f);
			}
		}
		if (this.p1 == 0)
		{
			int num = (this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx;
			int num2 = (this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy;
			if (!this.isNewMod())
			{
				if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.p1 = 1;
				}
				if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.p1 = 1;
				}
			}
			if (((int)Mob.arrMobTemplate[this.templateId].type == 4 || (int)Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove)
			{
				this.y += (num2 - this.y) / 20;
			}
			this.p2++;
			if (this.p2 > array.Length - 1 || this.p1 == 1)
			{
				this.p1 = 1;
				if (this.p3 == 0)
				{
					if (this.cFocus != null)
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
					if (this.cFocus != null)
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
		else if (this.p1 == 1)
		{
			if ((int)Mob.arrMobTemplate[this.templateId].type == 0 || this.isDontMove || this.isIce || !this.isWind)
			{
			}
			if (this.tick == array.Length)
			{
				this.status = 2;
				this.p1 = 0;
				this.p2 = 0;
				this.tick = 0;
			}
		}
		if (this.tick == 5 && this.cFocus != null && this.cFocus.charID == global::Char.myCharz().charID)
		{
			if (this.templateId == 88 && this.p3 != 0)
			{
				GameScr.shock_scr = 2;
			}
			if (this.templateId == 89)
			{
				GameScr.shock_scr = 2;
			}
		}
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00057B4C File Offset: 0x00055F4C
	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			if (this.injureThenDie)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 3;
				this.p1 = -5;
				this.p3 = 0;
			}
			num = 1;
			if (!this.isIce)
			{
				if (this.isDontMove || this.isWind)
				{
					this.checkFrameTick(this.stand);
				}
				else
				{
					switch (Mob.arrMobTemplate[this.templateId].type)
					{
					case 0:
						if (this.isNewModStand())
						{
							this.frame = this.stand[GameCanvas.gameTick % this.stand.Length];
						}
						else
						{
							this.frame = 0;
						}
						num = 2;
						break;
					case 1:
					case 2:
					case 3:
					{
						num = 3;
						sbyte b = Mob.arrMobTemplate[this.templateId].speed;
						if ((int)b == 1)
						{
							if (GameCanvas.gameTick % 2 == 1)
							{
								break;
							}
						}
						else if ((int)b > 2)
						{
							b = (sbyte)((int)b + (int)((sbyte)(this.mobId % 2)));
						}
						else if (GameCanvas.gameTick % 2 == 1)
						{
							b = (sbyte)((int)b - 1);
						}
						this.x += (int)b * this.dir;
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
						}
						if (Res.abs(this.x - global::Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = ((this.x <= global::Char.myCharz().cx) ? 1 : -1);
							if (Res.abs(this.x - global::Char.myCharz().cx) < 20)
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
						b2 = (sbyte)((int)b2 + (int)((sbyte)(this.mobId % 2)));
						this.x += (int)b2 * this.dir;
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b2 * this.dirV;
						}
						b2 = (sbyte)((int)b2 + (int)((sbyte)((GameCanvas.gameTick + this.mobId) % 2)));
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						this.checkFrameTick(this.move);
						break;
					}
					case 5:
					{
						num = 5;
						sbyte b3 = Mob.arrMobTemplate[this.templateId].speed;
						b3 = (sbyte)((int)b3 + (int)((sbyte)(this.mobId % 2)));
						this.x += (int)b3 * this.dir;
						b3 = (sbyte)((int)b3 + (int)((sbyte)((GameCanvas.gameTick + this.mobId) % 2)));
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b3 * this.dirV;
						}
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						if (TileMap.tileTypeAt(this.x, this.y, 2))
						{
							if (GameCanvas.gameTick % 10 > 5)
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
			Cout.println("lineee: " + num);
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0005812C File Offset: 0x0005652C
	public MobTemplate getTemplate()
	{
		return Mob.arrMobTemplate[this.templateId];
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0005813C File Offset: 0x0005653C
	public bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x000581F6 File Offset: 0x000565F6
	public bool isUpdate()
	{
		return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00058231 File Offset: 0x00056631
	public bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00058250 File Offset: 0x00056650
	public void updateHp_bar()
	{
		this.len = (int)((long)this.hp * 100L / (long)this.maxHp * (long)this.w_hp_bar) / 100;
		this.per = (int)((long)this.hp * 100L / (long)this.maxHp);
		if (this.per == 100)
		{
			this.per_tem = this.per;
		}
		if (this.per >= 100)
		{
			this.per_tem = this.per;
		}
		this.offset = 0;
		if (this.per < 30)
		{
			this.color = 15473700;
			this.imgHPtem = GameScr.imgHP_tm_do;
		}
		else if (this.per < 60)
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

	// Token: 0x0600083E RID: 2110 RVA: 0x0005833C File Offset: 0x0005673C
	public virtual void paint(mGraphics g)
	{
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
		if (this.isShadown && this.status != 0)
		{
			this.paintShadow(g);
		}
		if (!this.isPaint())
		{
			return;
		}
		if (this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0)
		{
			return;
		}
		g.translate(0, GameCanvas.transY);
		if (!this.changBody)
		{
			Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		g.translate(0, -GameCanvas.transY);
		if (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.Equals(this) && this.status != 1)
		{
			if (this.hp > 0)
			{
				if (this.imgHPtem != null)
				{
					int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
					int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
					int num = imageWidth * this.per / 100;
					int num2 = num;
					if (this.per_tem >= this.per)
					{
						num2 = imageWidth * (this.per_tem -= ((GameCanvas.gameTick % 6 <= 3) ? this.offset : this.offset++)) / 100;
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
					g.drawImage(GameScr.imgHP_tm_xam, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
					g.setColor(16777215);
					g.fillRect(this.x - (imageWidth >> 1), this.y - this.h - 5, num2, 2);
					g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
				}
			}
		}
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00058678 File Offset: 0x00056A78
	public int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00058680 File Offset: 0x00056A80
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

	// Token: 0x06000841 RID: 2113 RVA: 0x000586D0 File Offset: 0x00056AD0
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
		if (Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2)
		{
			if (this.x < num)
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

	// Token: 0x06000842 RID: 2114 RVA: 0x000587AE File Offset: 0x00056BAE
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x000587B6 File Offset: 0x00056BB6
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x000587BE File Offset: 0x00056BBE
	public int getH()
	{
		return this.h;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x000587C6 File Offset: 0x00056BC6
	public int getW()
	{
		return this.w;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x000587D0 File Offset: 0x00056BD0
	public void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00058811 File Offset: 0x00056C11
	public bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0005882A File Offset: 0x00056C2A
	public void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0005883E File Offset: 0x00056C3E
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00058847 File Offset: 0x00056C47
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00058850 File Offset: 0x00056C50
	public void GetFrame()
	{
		if (this.isGetFr && this.isTypeNewMod() && Mob.arrMobTemplate[this.templateId].data != null)
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId + string.Empty);
			this.stand = this.frameArr[0];
			this.move = this.frameArr[1];
			this.moveFast = this.frameArr[2];
			this.attack1 = this.frameArr[3];
			this.attack2 = this.frameArr[4];
			this.hurt = this.frameArr[5];
			this.isGetFr = false;
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0005890E File Offset: 0x00056D0E
	private bool isTypeNewMod()
	{
		return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
	}

	// Token: 0x04000F81 RID: 3969
	public const sbyte TYPE_DUNG = 0;

	// Token: 0x04000F82 RID: 3970
	public const sbyte TYPE_DI = 1;

	// Token: 0x04000F83 RID: 3971
	public const sbyte TYPE_NHAY = 2;

	// Token: 0x04000F84 RID: 3972
	public const sbyte TYPE_LET = 3;

	// Token: 0x04000F85 RID: 3973
	public const sbyte TYPE_BAY = 4;

	// Token: 0x04000F86 RID: 3974
	public const sbyte TYPE_BAY_DAU = 5;

	// Token: 0x04000F87 RID: 3975
	public static MobTemplate[] arrMobTemplate;

	// Token: 0x04000F88 RID: 3976
	public const sbyte MA_INHELL = 0;

	// Token: 0x04000F89 RID: 3977
	public const sbyte MA_DEADFLY = 1;

	// Token: 0x04000F8A RID: 3978
	public const sbyte MA_STANDWAIT = 2;

	// Token: 0x04000F8B RID: 3979
	public const sbyte MA_ATTACK = 3;

	// Token: 0x04000F8C RID: 3980
	public const sbyte MA_STANDFLY = 4;

	// Token: 0x04000F8D RID: 3981
	public const sbyte MA_WALK = 5;

	// Token: 0x04000F8E RID: 3982
	public const sbyte MA_FALL = 6;

	// Token: 0x04000F8F RID: 3983
	public const sbyte MA_INJURE = 7;

	// Token: 0x04000F90 RID: 3984
	public bool changBody;

	// Token: 0x04000F91 RID: 3985
	public short smallBody;

	// Token: 0x04000F92 RID: 3986
	public bool isHintFocus;

	// Token: 0x04000F93 RID: 3987
	public string flystring;

	// Token: 0x04000F94 RID: 3988
	public int flyx;

	// Token: 0x04000F95 RID: 3989
	public int flyy;

	// Token: 0x04000F96 RID: 3990
	public int flyIndex;

	// Token: 0x04000F97 RID: 3991
	public bool isFreez;

	// Token: 0x04000F98 RID: 3992
	public int seconds;

	// Token: 0x04000F99 RID: 3993
	public long last;

	// Token: 0x04000F9A RID: 3994
	public long cur;

	// Token: 0x04000F9B RID: 3995
	public int holdEffID;

	// Token: 0x04000F9C RID: 3996
	public int hp;

	// Token: 0x04000F9D RID: 3997
	public int maxHp;

	// Token: 0x04000F9E RID: 3998
	public int x;

	// Token: 0x04000F9F RID: 3999
	public int y;

	// Token: 0x04000FA0 RID: 4000
	public int dir = 1;

	// Token: 0x04000FA1 RID: 4001
	public int dirV = 1;

	// Token: 0x04000FA2 RID: 4002
	public int status;

	// Token: 0x04000FA3 RID: 4003
	public int p1;

	// Token: 0x04000FA4 RID: 4004
	public int p2;

	// Token: 0x04000FA5 RID: 4005
	public int p3;

	// Token: 0x04000FA6 RID: 4006
	public int xFirst;

	// Token: 0x04000FA7 RID: 4007
	public int yFirst;

	// Token: 0x04000FA8 RID: 4008
	public int vy;

	// Token: 0x04000FA9 RID: 4009
	public int exp;

	// Token: 0x04000FAA RID: 4010
	public int w;

	// Token: 0x04000FAB RID: 4011
	public int h;

	// Token: 0x04000FAC RID: 4012
	public int hpInjure;

	// Token: 0x04000FAD RID: 4013
	public int charIndex;

	// Token: 0x04000FAE RID: 4014
	public int timeStatus;

	// Token: 0x04000FAF RID: 4015
	public int mobId;

	// Token: 0x04000FB0 RID: 4016
	public bool isx;

	// Token: 0x04000FB1 RID: 4017
	public bool isy;

	// Token: 0x04000FB2 RID: 4018
	public bool isDisable;

	// Token: 0x04000FB3 RID: 4019
	public bool isDontMove;

	// Token: 0x04000FB4 RID: 4020
	public bool isFire;

	// Token: 0x04000FB5 RID: 4021
	public bool isIce;

	// Token: 0x04000FB6 RID: 4022
	public bool isWind;

	// Token: 0x04000FB7 RID: 4023
	public bool isDie;

	// Token: 0x04000FB8 RID: 4024
	public MyVector vMobMove = new MyVector();

	// Token: 0x04000FB9 RID: 4025
	public bool isGo;

	// Token: 0x04000FBA RID: 4026
	public string mobName;

	// Token: 0x04000FBB RID: 4027
	public int templateId;

	// Token: 0x04000FBC RID: 4028
	public short pointx;

	// Token: 0x04000FBD RID: 4029
	public short pointy;

	// Token: 0x04000FBE RID: 4030
	public global::Char cFocus;

	// Token: 0x04000FBF RID: 4031
	public int dame;

	// Token: 0x04000FC0 RID: 4032
	public int dameMp;

	// Token: 0x04000FC1 RID: 4033
	public int sys;

	// Token: 0x04000FC2 RID: 4034
	public sbyte levelBoss;

	// Token: 0x04000FC3 RID: 4035
	public sbyte level;

	// Token: 0x04000FC4 RID: 4036
	public bool isBoss;

	// Token: 0x04000FC5 RID: 4037
	public bool isMobMe;

	// Token: 0x04000FC6 RID: 4038
	public static MyVector lastMob = new MyVector();

	// Token: 0x04000FC7 RID: 4039
	public static MyVector newMob = new MyVector();

	// Token: 0x04000FC8 RID: 4040
	public bool isMafuba;

	// Token: 0x04000FC9 RID: 4041
	public int xMFB;

	// Token: 0x04000FCA RID: 4042
	public int yMFB;

	// Token: 0x04000FCB RID: 4043
	public int xSd;

	// Token: 0x04000FCC RID: 4044
	public int ySd;

	// Token: 0x04000FCD RID: 4045
	private bool isOutMap;

	// Token: 0x04000FCE RID: 4046
	private int wCount;

	// Token: 0x04000FCF RID: 4047
	public bool isShadown = true;

	// Token: 0x04000FD0 RID: 4048
	private int tick;

	// Token: 0x04000FD1 RID: 4049
	private int frame;

	// Token: 0x04000FD2 RID: 4050
	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000FD3 RID: 4051
	private bool wy;

	// Token: 0x04000FD4 RID: 4052
	private int wt;

	// Token: 0x04000FD5 RID: 4053
	private int fy;

	// Token: 0x04000FD6 RID: 4054
	private int ty;

	// Token: 0x04000FD7 RID: 4055
	public int typeSuperEff;

	// Token: 0x04000FD8 RID: 4056
	public bool isBusyAttackSomeOne = true;

	// Token: 0x04000FD9 RID: 4057
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

	// Token: 0x04000FDA RID: 4058
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

	// Token: 0x04000FDB RID: 4059
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

	// Token: 0x04000FDC RID: 4060
	public int[] attack1 = new int[]
	{
		4,
		5,
		6
	};

	// Token: 0x04000FDD RID: 4061
	public int[] attack2 = new int[]
	{
		7,
		8,
		9
	};

	// Token: 0x04000FDE RID: 4062
	public int[] hurt = new int[1];

	// Token: 0x04000FDF RID: 4063
	private int color = 8421504;

	// Token: 0x04000FE0 RID: 4064
	public int len = 24;

	// Token: 0x04000FE1 RID: 4065
	public int w_hp_bar = 24;

	// Token: 0x04000FE2 RID: 4066
	public int per = 100;

	// Token: 0x04000FE3 RID: 4067
	public int per_tem = 100;

	// Token: 0x04000FE4 RID: 4068
	public byte h_hp_bar = 4;

	// Token: 0x04000FE5 RID: 4069
	public Image imgHPtem;

	// Token: 0x04000FE6 RID: 4070
	private int offset;

	// Token: 0x04000FE7 RID: 4071
	public bool isHide;

	// Token: 0x04000FE8 RID: 4072
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000FE9 RID: 4073
	public global::Char injureBy;

	// Token: 0x04000FEA RID: 4074
	public bool injureThenDie;

	// Token: 0x04000FEB RID: 4075
	public Mob mobToAttack;

	// Token: 0x04000FEC RID: 4076
	public int forceWait;

	// Token: 0x04000FED RID: 4077
	public bool blindEff;

	// Token: 0x04000FEE RID: 4078
	public bool sleepEff;

	// Token: 0x04000FEF RID: 4079
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

	// Token: 0x04000FF0 RID: 4080
	private bool isGetFr = true;
}
