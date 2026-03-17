using System;
using Assets.src.e;
using Assets.src.g;

// Token: 0x0200000F RID: 15
public class Char : IMapObject
{
	// Token: 0x06000094 RID: 148 RVA: 0x00007FD8 File Offset: 0x000061D8
	public Char()
	{
		this.statusMe = 6;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000823C File Offset: 0x0000643C
	public void applyCharLevelPercent()
	{
		try
		{
			long num = 1L;
			long num2 = 0L;
			int num3 = 0;
			for (int i = GameScr.exps.Length - 1; i >= 0; i--)
			{
				bool flag = this.cPower >= GameScr.exps[i];
				if (flag)
				{
					bool flag2 = i == GameScr.exps.Length - 1;
					if (flag2)
					{
						num = 1L;
					}
					else
					{
						num = GameScr.exps[i + 1] - GameScr.exps[i];
					}
					num2 = this.cPower - GameScr.exps[i];
					num3 = i;
					break;
				}
			}
			this.clevel = num3;
			this.cLevelPercent = (long)((int)(num2 * 10000L / num));
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi char level percent: " + ex.ToString());
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00008314 File Offset: 0x00006514
	public int getdxSkill()
	{
		bool flag = this.myskill != null;
		int result;
		if (flag)
		{
			result = this.myskill.dx;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00008344 File Offset: 0x00006544
	public int getdySkill()
	{
		bool flag = this.myskill != null;
		int result;
		if (flag)
		{
			result = this.myskill.dy;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00008374 File Offset: 0x00006574
	public static void taskAction(bool isNextStep)
	{
		Task task = global::Char.myCharz().taskMaint;
		bool flag = task.index > task.contentInfo.Length - 1;
		if (flag)
		{
			task.index = task.contentInfo.Length - 1;
		}
		string text = task.contentInfo[task.index];
		bool flag2 = text != null && !text.Equals(string.Empty);
		if (flag2)
		{
			bool flag3 = text.StartsWith("#");
			if (flag3)
			{
				text = NinjaUtil.replace(text, "#", string.Empty);
				Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				npc.cx = (npc.cy = -100);
				npc.avatar = GameScr.info1.charId[global::Char.myCharz().cgender][2];
				npc.charID = 5;
				bool flag4 = GameCanvas.currentScreen == GameScr.instance;
				if (flag4)
				{
					ChatPopup.addNextPopUpMultiLine(text, npc);
				}
			}
			else if (isNextStep)
			{
				GameScr.info1.addInfo(text, 0);
			}
		}
		GameScr.isHaveSelectSkill = true;
		Cout.println("TASKx " + global::Char.myCharz().taskMaint.taskId.ToString());
		bool flag5 = global::Char.myCharz().taskMaint.taskId <= 2;
		if (flag5)
		{
			global::Char.myCharz().canFly = false;
		}
		else
		{
			global::Char.myCharz().canFly = true;
		}
		GameScr.gI().left = null;
		bool flag6 = task.taskId == 0;
		if (flag6)
		{
			Hint.isViewMap = false;
			Hint.isViewPotential = false;
			GameScr.gI().right = null;
			GameScr.isHaveSelectSkill = false;
			GameScr.gI().left = null;
			bool flag7 = task.index < 4;
			if (flag7)
			{
				MagicTree.isPaint = false;
				GameScr.isPaintRada = -1;
			}
			bool flag8 = task.index == 4;
			if (flag8)
			{
				GameScr.isPaintRada = 1;
				MagicTree.isPaint = true;
			}
			bool flag9 = task.index >= 5;
			if (flag9)
			{
				GameScr.gI().right = GameScr.gI().cmdFocus;
			}
		}
		bool flag10 = task.taskId == 1;
		if (flag10)
		{
			GameScr.isHaveSelectSkill = true;
		}
		bool flag11 = task.taskId >= 1;
		if (flag11)
		{
			GameScr.gI().right = GameScr.gI().cmdFocus;
			GameScr.gI().left = GameScr.gI().cmdMenu;
		}
		bool flag12 = task.taskId >= 0;
		if (flag12)
		{
			Panel.isPaintMap = true;
		}
		else
		{
			Panel.isPaintMap = false;
		}
		bool flag13 = task.taskId < 12;
		if (flag13)
		{
			GameCanvas.panel.mainTabName = mResources.mainTab1;
		}
		else
		{
			GameCanvas.panel.mainTabName = mResources.mainTab2;
		}
		GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
		bool flag14 = global::Char.myChar.taskMaint.taskId > 10;
		if (flag14)
		{
			Rms.saveRMSString("fake", "aa");
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00008694 File Offset: 0x00006894
	public string getStrLevel()
	{
		string text = string.Concat(new object[]
		{
			this.strLevel[this.clevel],
			"+",
			this.cLevelPercent / 100L,
			".",
			this.cLevelPercent % 100L,
			"%"
		});
		bool flag = text.Length > 23 && text.IndexOf("cấp ") >= 0;
		if (flag)
		{
			text = Res.replace(text, "cấp ", "c");
		}
		return text;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00008734 File Offset: 0x00006934
	public int avatarz()
	{
		return this.getAvatar(this.head);
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00008754 File Offset: 0x00006954
	public int getAvatar(int headId)
	{
		for (int i = 0; i < global::Char.idHead.Length; i++)
		{
			bool flag = headId == (int)global::Char.idHead[i];
			if (flag)
			{
				return (int)global::Char.idAvatar[i];
			}
		}
		return -1;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00008798 File Offset: 0x00006998
	public void setPowerInfo(string info, short p, short maxP, short sc)
	{
		this.powerPoint = p;
		this.strInfo = info;
		this.maxPowerPoint = maxP;
		this.secondPower = sc;
		this.lastS = (this.currS = mSystem.currentTimeMillis());
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000087D8 File Offset: 0x000069D8
	public void addInfo(string info)
	{
		bool flag = this.chatInfo == null;
		if (flag)
		{
			this.chatInfo = new Info();
		}
		global::Char cInfo = null;
		this.chatInfo.addInfo(info, 0, cInfo, false);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00008814 File Offset: 0x00006A14
	public int getSys()
	{
		bool flag = this.nClass.classId == 1 || this.nClass.classId == 2;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = this.nClass.classId == 3 || this.nClass.classId == 4;
			if (flag2)
			{
				result = 2;
			}
			else
			{
				bool flag3 = this.nClass.classId == 5 || this.nClass.classId == 6;
				if (flag3)
				{
					result = 3;
				}
				else
				{
					result = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000088A0 File Offset: 0x00006AA0
	public static global::Char myCharz()
	{
		bool flag = global::Char.myChar == null;
		if (flag)
		{
			global::Char.myChar = new global::Char();
			global::Char.myChar.me = true;
			global::Char.myChar.cmtoChar = true;
		}
		return global::Char.myChar;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000088E8 File Offset: 0x00006AE8
	public static global::Char myPetz()
	{
		bool flag = global::Char.myPet == null;
		if (flag)
		{
			global::Char.myPet = new global::Char();
			global::Char.myPet.me = false;
		}
		return global::Char.myPet;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00008922 File Offset: 0x00006B22
	public static void clearMyChar()
	{
		global::Char.myChar = null;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000892C File Offset: 0x00006B2C
	public void bagSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBag.Length; i++)
			{
				Item item = this.arrItemBag[i];
				bool flag = item != null && item.template.isUpToUp && !item.isExpires;
				if (flag)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				bool flag2 = item2 != null;
				if (flag2)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						bool flag3 = item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock;
						if (flag3)
						{
							item2.quantity += item3.quantity;
							this.arrItemBag[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBag.Length; l++)
			{
				bool flag4 = this.arrItemBag[l] != null;
				if (flag4)
				{
					for (int m = 0; m <= l; m++)
					{
						bool flag5 = this.arrItemBag[m] == null;
						if (flag5)
						{
							this.arrItemBag[m] = this.arrItemBag[l];
							this.arrItemBag[m].indexUI = m;
							this.arrItemBag[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Char.bagSort()");
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00008B28 File Offset: 0x00006D28
	public void boxSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBox.Length; i++)
			{
				Item item = this.arrItemBox[i];
				bool flag = item != null && item.template.isUpToUp && !item.isExpires;
				if (flag)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				bool flag2 = item2 != null;
				if (flag2)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						bool flag3 = item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock;
						if (flag3)
						{
							item2.quantity += item3.quantity;
							this.arrItemBox[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBox.Length; l++)
			{
				bool flag4 = this.arrItemBox[l] != null;
				if (flag4)
				{
					for (int m = 0; m <= l; m++)
					{
						bool flag5 = this.arrItemBox[m] == null;
						if (flag5)
						{
							this.arrItemBox[m] = this.arrItemBox[l];
							this.arrItemBox[m].indexUI = m;
							this.arrItemBox[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Char.boxSort()");
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00008D24 File Offset: 0x00006F24
	public void useItem(int indexUI)
	{
		Item item = this.arrItemBag[indexUI];
		bool flag = item.isTypeBody();
		if (flag)
		{
			item.isLock = true;
			item.typeUI = 5;
			Item item2 = this.arrItemBody[(int)item.template.type];
			this.arrItemBag[indexUI] = null;
			bool flag2 = item2 != null;
			if (flag2)
			{
				item2.typeUI = 3;
				this.arrItemBody[(int)item.template.type] = null;
				item2.indexUI = indexUI;
				this.arrItemBag[indexUI] = item2;
			}
			item.indexUI = (int)item.template.type;
			this.arrItemBody[item.indexUI] = item;
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				Item item3 = this.arrItemBody[i];
				bool flag3 = item3 != null;
				if (flag3)
				{
					bool flag4 = item3.template.type == 0;
					if (flag4)
					{
						this.body = (int)item3.template.part;
					}
					else
					{
						bool flag5 = item3.template.type == 1;
						if (flag5)
						{
							this.leg = (int)item3.template.part;
						}
					}
				}
			}
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00008E58 File Offset: 0x00007058
	public Skill getSkill(SkillTemplate skillTemplate)
	{
		for (int i = 0; i < this.vSkill.size(); i++)
		{
			bool flag = ((Skill)this.vSkill.elementAt(i)).template.id == skillTemplate.id;
			if (flag)
			{
				return (Skill)this.vSkill.elementAt(i);
			}
		}
		return null;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00008EC4 File Offset: 0x000070C4
	public Waypoint isInEnterOfflinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		bool flag = task != null && task.taskId == 0 && task.index < 6;
		Waypoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				bool flag2 = PopUp.vPopups.size() >= num;
				if (flag2)
				{
					PopUp popUp = (PopUp)PopUp.vPopups.elementAt((int)b);
					bool flag3 = !popUp.isPaint;
					if (flag3)
					{
						return null;
					}
				}
				bool flag4 = this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && waypoint.isOffline;
				if (flag4)
				{
					return waypoint;
				}
				b += 1;
			}
			result = null;
		}
		return result;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00008FDC File Offset: 0x000071DC
	public Waypoint isInEnterOnlinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		bool flag = task != null && task.taskId == 0 && task.index < 6;
		Waypoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				bool flag2 = PopUp.vPopups.size() >= num;
				if (flag2)
				{
					PopUp popUp = (PopUp)PopUp.vPopups.elementAt((int)b);
					bool flag3 = !popUp.isPaint;
					if (flag3)
					{
						return null;
					}
				}
				bool flag4 = this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && !waypoint.isOffline;
				if (flag4)
				{
					return waypoint;
				}
				b += 1;
			}
			result = null;
		}
		return result;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000090F8 File Offset: 0x000072F8
	public bool isInWaypoint()
	{
		bool flag = TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = this.isTeleport || this.isUsePlane;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num = TileMap.vGo.size();
				sbyte b = 0;
				while ((int)b < num)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
					bool flag3 = (TileMap.mapID == 47 || TileMap.isInAirMap()) && this.cy <= (int)(waypoint.minY + waypoint.maxY) && this.cx > (int)waypoint.minX && this.cx < (int)waypoint.maxX;
					if (flag3)
					{
						return !TileMap.isInAirMap() || this.cTypePk == 0;
					}
					bool flag4 = this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && !waypoint.isEnter;
					if (flag4)
					{
						return true;
					}
					b += 1;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00009244 File Offset: 0x00007444
	public bool isPunchKickSkill()
	{
		return this.skillPaint != null && ((this.skillPaint.id >= 0 && this.skillPaint.id <= 6) || (this.skillPaint.id >= 14 && this.skillPaint.id <= 20) || (this.skillPaint.id >= 28 && this.skillPaint.id <= 34) || (this.skillPaint.id >= 63 && this.skillPaint.id <= 69));
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000092E4 File Offset: 0x000074E4
	public void soundUpdate()
	{
		bool flag = this.me && this.statusMe == 10 && this.cf == 8 && this.ty > 20 && GameCanvas.gameTick % 20 == 0;
		if (flag)
		{
			SoundMn.gI().charFly();
		}
		bool flag2 = this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length && this.isPunchKickSkill() && (this.me || (!this.me && this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0;
		if (flag2)
		{
			bool flag3 = this.cf == 9 || this.cf == 10 || this.cf == 11;
			if (flag3)
			{
				SoundMn.gI().charPunch(true, (!this.me) ? 0.05f : 0.1f);
			}
			else
			{
				SoundMn.gI().charPunch(false, (!this.me) ? 0.05f : 0.1f);
			}
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003136 File Offset: 0x00001336
	public void updateChargeSkill()
	{
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000940C File Offset: 0x0000760C
	public virtual void update()
	{
		bool flag5 = this.isMafuba;
		if (flag5)
		{
			this.cf = 23;
			this.countMafuba += 1;
			bool flag6 = this.countMafuba > 150;
			if (flag6)
			{
				this.isMafuba = false;
			}
		}
		else
		{
			this.countMafuba = 0;
			bool flag7 = this.isHide;
			if (!flag7)
			{
				bool flag8 = this.isMabuHold;
				if (!flag8)
				{
					bool flag9 = (!this.isCopy && this.clevel < 14) || this.statusMe == 1 || this.statusMe == 6;
					if (flag9)
					{
					}
					bool flag10 = this.petFollow != null;
					if (flag10)
					{
						bool flag11 = GameCanvas.gameTick % 3 == 0;
						if (flag11)
						{
							bool flag12 = global::Char.myCharz().cdir == 1;
							if (flag12)
							{
								this.petFollow.cmtoX = this.cx - 20;
							}
							bool flag13 = global::Char.myCharz().cdir == -1;
							if (flag13)
							{
								this.petFollow.cmtoX = this.cx + 20;
							}
							this.petFollow.cmtoY = this.cy - 40;
							bool flag14 = this.petFollow.cmx > this.cx;
							if (flag14)
							{
								this.petFollow.dir = -1;
							}
							else
							{
								this.petFollow.dir = 1;
							}
							bool flag15 = this.petFollow.cmtoX < 100;
							if (flag15)
							{
								this.petFollow.cmtoX = 100;
							}
							bool flag16 = this.petFollow.cmtoX > TileMap.pxw - 100;
							if (flag16)
							{
								this.petFollow.cmtoX = TileMap.pxw - 100;
							}
						}
						this.petFollow.update();
					}
					bool flag17 = !this.me && this.cHP <= 0 && this.clanID != -100 && this.statusMe != 14 && this.statusMe != 5;
					if (flag17)
					{
						this.startDie((short)this.cx, (short)this.cy);
					}
					bool flag18 = this.isInjureHp;
					if (flag18)
					{
						this.twHp++;
						bool flag19 = this.twHp == 20;
						if (flag19)
						{
							this.twHp = 0;
							this.isInjureHp = false;
						}
					}
					else
					{
						bool flag20 = this.dHP > this.cHP;
						if (flag20)
						{
							int num = this.dHP - this.cHP >> 1;
							bool flag21 = num < 1;
							if (flag21)
							{
								num = 1;
							}
							this.dHP -= num;
						}
						else
						{
							this.dHP = this.cHP;
						}
					}
					bool flag22 = this.secondPower != 0;
					if (flag22)
					{
						this.currS = mSystem.currentTimeMillis();
						bool flag23 = this.currS - this.lastS >= 1000L;
						if (flag23)
						{
							this.lastS = mSystem.currentTimeMillis();
							this.secondPower -= 1;
						}
					}
					bool flag24 = this.isPaintNewSkill;
					if (flag24)
					{
						bool flag25 = GameCanvas.timeNow > this.timeReset_newSkill || this.statusMe == 14 || this.statusMe == 5;
						if (flag25)
						{
							this.timeReset_newSkill = 0L;
							this.isPaintNewSkill = false;
						}
						this.UpdSkillPaint_NEW();
						bool flag26 = this.isShadown;
						if (flag26)
						{
							this.updateShadown();
						}
					}
					else
					{
						bool flag27 = !this.me && GameScr.notPaint;
						if (!flag27)
						{
							bool flag28 = this.sleepEff && GameCanvas.gameTick % 10 == 0;
							if (flag28)
							{
								EffecMn.addEff(new Effect(41, this.cx, this.cy, 3, 1, 1));
							}
							bool flag29 = this.huytSao;
							if (flag29)
							{
								this.huytSao = false;
								EffecMn.addEff(new Effect(39, this.cx, this.cy, 3, 3, 1));
							}
							bool flag30 = this.blindEff && GameCanvas.gameTick % 5 == 0;
							if (flag30)
							{
								ServerEffect.addServerEffect(113, this, 1);
							}
							bool flag31 = this.protectEff;
							if (flag31)
							{
								int y = this.cH_new + 73;
								bool flag32 = GameCanvas.gameTick % 5 == 0;
								if (flag32)
								{
									this.eProtect = new Effect(33, this.cx, y, 3, 3, 1);
								}
								bool flag33 = this.eProtect != null;
								if (flag33)
								{
									this.eProtect.update();
									this.eProtect.x = this.cx;
									this.eProtect.y = y;
								}
							}
							bool flag34 = this.danhHieuEff;
							if (flag34)
							{
								bool flag35 = this.eDanhHieu == null;
								if (flag35)
								{
									string text = (string)GameCanvas.danhHieu.get(this.charID.ToString() + string.Empty);
									bool flag36 = text != null;
									if (flag36)
									{
										string[] array = Res.split(text.Trim(), ",", 0);
										short id = short.Parse(array[0]);
										short num2 = short.Parse(array[1]);
										this.eDanhHieu = new Effect((int)id, this.cx, this.cH_new + 73, 1, -1, -1);
										this.eDanhHieu.timeExist = (long)(num2 * 1000) + mSystem.currentTimeMillis();
									}
								}
								bool flag37 = this.eDanhHieu != null;
								if (flag37)
								{
									this.eDanhHieu.update();
									this.eDanhHieu.x = this.cx;
									this.eDanhHieu.y = this.cH_new;
									bool flag38 = this.eDanhHieu.timeExist <= mSystem.currentTimeMillis();
									if (flag38)
									{
										this.eDanhHieu = null;
										GameCanvas.danhHieu.remove(this.charID.ToString() + string.Empty);
									}
								}
							}
							bool flag39 = this.charFocus != null && this.charFocus.cy < 0;
							if (flag39)
							{
								this.charFocus = null;
							}
							bool flag40 = this.isFusion;
							if (flag40)
							{
								this.tFusion++;
							}
							bool flag41 = this.isNhapThe && GameCanvas.gameTick % 25 == 0;
							if (flag41)
							{
								int id2 = 114;
								ServerEffect.addServerEffect(id2, this, 1);
							}
							bool flag42 = this.isSetPos;
							if (flag42)
							{
								this.tpos++;
								bool flag43 = this.tpos == 1;
								if (flag43)
								{
									this.tpos = 0;
									this.isSetPos = false;
									this.cx = (int)this.xPos;
									this.cy = (int)this.yPos;
									this.cp1 = (this.cp2 = (this.cp3 = 0));
									bool flag44 = this.typePos == 1;
									if (flag44)
									{
										bool flag45 = this.me;
										if (flag45)
										{
											this.cxSend = this.cx;
											this.cySend = this.cy;
										}
										this.currentMovePoint = null;
										this.telePortSkill = false;
										ServerEffect.addServerEffect(173, this.cx, this.cy, 1);
									}
									else
									{
										ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
									}
									bool flag46 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
									if (flag46)
									{
										this.statusMe = 1;
									}
									else
									{
										this.statusMe = 4;
									}
								}
							}
							else
							{
								this.soundUpdate();
								bool flag47 = this.stone;
								if (!flag47)
								{
									bool flag48 = this.isFreez;
									if (flag48)
									{
										bool flag49 = GameCanvas.gameTick % 5 == 0;
										if (flag49)
										{
											ServerEffect.addServerEffect(113, this.cx, this.cy, 1);
										}
										this.cf = 23;
										long num3 = mSystem.currentTimeMillis();
										bool flag50 = num3 - this.lastFreez >= 1000L;
										if (flag50)
										{
											this.freezSeconds--;
											this.lastFreez = num3;
											bool flag51 = this.freezSeconds < 0;
											if (flag51)
											{
												this.isFreez = false;
												this.seconds = 0;
												bool flag52 = this.me;
												if (flag52)
												{
													global::Char.myCharz().isLockMove = false;
													GameScr.gI().dem = 0;
													GameScr.gI().isFreez = false;
												}
											}
										}
										bool flag53 = TileMap.tileTypeAt(this.cx / (int)TileMap.size, this.cy / (int)TileMap.size) == 0;
										if (flag53)
										{
											this.ty++;
											this.wt++;
											this.fy += (this.wy ? -1 : 1);
											bool flag54 = this.wt == 10;
											if (flag54)
											{
												this.wt = 0;
												this.wy = !this.wy;
											}
										}
									}
									else
									{
										bool flag55 = this.isWaitMonkey;
										if (flag55)
										{
											this.isLockMove = true;
											this.cf = 17;
											bool flag56 = GameCanvas.gameTick % 5 == 0;
											if (flag56)
											{
												ServerEffect.addServerEffect(154, this.cx, this.cy - 10, 2);
											}
											bool flag57 = GameCanvas.gameTick % 5 == 0;
											if (flag57)
											{
												ServerEffect.addServerEffect(1, this.cx, this.cy + 10, 1);
											}
											this.chargeCount++;
											bool flag58 = this.chargeCount == 500;
											if (flag58)
											{
												this.isWaitMonkey = false;
												this.isLockMove = false;
											}
										}
										else
										{
											bool flag59 = this.isStandAndCharge;
											if (flag59)
											{
												this.chargeCount++;
												bool flag = !TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
												this.updateEffect();
												this.updateSkillPaint();
												this.moveFast = null;
												this.currentMovePoint = null;
												this.cf = 17;
												bool flag60 = flag && this.cgender != 2;
												if (flag60)
												{
													this.cf = 12;
												}
												bool flag61 = this.cgender == 2;
												if (flag61)
												{
													bool flag62 = GameCanvas.gameTick % 3 == 0;
													if (flag62)
													{
														ServerEffect.addServerEffect(154, this.cx, this.cy - this.ch / 2 + 10, 1);
													}
													bool flag63 = GameCanvas.gameTick % 5 == 0;
													if (flag63)
													{
														ServerEffect.addServerEffect(114, this.cx + Res.random(-20, 20), this.cy + Res.random(-20, 20), 1);
													}
												}
												bool flag64 = this.cgender == 1;
												if (flag64)
												{
													bool flag65 = GameCanvas.gameTick % 4 == 0;
													if (flag65)
													{
													}
													bool flag66 = GameCanvas.gameTick % 2 == 0;
													if (flag66)
													{
														bool flag67 = this.cdir == 1;
														if (flag67)
														{
															ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
															ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
														}
														else
														{
															ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
															ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
														}
													}
												}
												this.cur = mSystem.currentTimeMillis();
												bool flag68 = this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L;
												if (flag68)
												{
													this.stopUseChargeSkill();
													bool flag69 = this.me;
													if (flag69)
													{
														GameScr.gI().auto = 0;
														bool flag70 = this.cgender == 2;
														if (flag70)
														{
															global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
															Service.gI().skill_not_focus(8);
														}
														bool flag71 = this.cgender == 1;
														if (flag71)
														{
															Res.outz("set skipp paint");
															this.isCreateDark = true;
															global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
														}
													}
													else
													{
														bool flag72 = this.cgender == 2;
														if (flag72)
														{
															this.setAutoSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
														}
													}
													bool flag73 = this.cgender == 2 && this.statusMe != 14 && this.statusMe != 5;
													if (flag73)
													{
														GameScr.gI().activeSuperPower(this.cx, this.cy);
													}
												}
												this.chargeCount++;
												bool flag74 = this.chargeCount == 500;
												if (flag74)
												{
													this.stopUseChargeSkill();
												}
											}
											else
											{
												bool flag75 = this.isFlyAndCharge;
												if (flag75)
												{
													this.updateEffect();
													this.updateSkillPaint();
													this.moveFast = null;
													this.currentMovePoint = null;
													this.posDisY++;
													bool flag76 = TileMap.tileTypeAt(this.cx, this.cy - this.ch, 8192);
													if (flag76)
													{
														this.stopUseChargeSkill();
													}
													else
													{
														bool flag77 = this.posDisY == 20;
														if (flag77)
														{
															this.last = mSystem.currentTimeMillis();
														}
														bool flag78 = this.posDisY <= 20;
														if (flag78)
														{
															bool flag79 = this.statusMe != 14;
															if (flag79)
															{
																this.statusMe = 3;
															}
															this.cvy = -3;
															this.cy += this.cvy;
															this.cf = 7;
														}
														else
														{
															this.cur = mSystem.currentTimeMillis();
															bool flag80 = this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L;
															if (flag80)
															{
																this.isFlyAndCharge = false;
																bool flag81 = this.me;
																if (flag81)
																{
																	this.isCreateDark = true;
																	bool flag2 = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
																	this.isUseSkillAfterCharge = true;
																	global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag2) ? 1 : 0);
																}
															}
															else
															{
																this.cf = 32;
																bool flag82 = this.cgender == 0 && GameCanvas.gameTick % 3 == 0;
																if (flag82)
																{
																	ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
																}
																this.chargeCount++;
																bool flag83 = this.chargeCount == 500;
																if (flag83)
																{
																	this.stopUseChargeSkill();
																}
															}
														}
													}
												}
												else
												{
													bool flag84 = this.me && GameCanvas.isTouch;
													if (flag84)
													{
														bool flag85 = this.charFocus != null && this.charFocus.charID >= 0 && this.charFocus.cx > 100 && this.charFocus.cx < TileMap.pxw - 100 && this.isInEnterOnlinePoint() == null && this.isInEnterOfflinePoint() == null && !this.isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null;
														if (flag85)
														{
															int num4 = global::Math.abs(this.cx - this.charFocus.cx);
															int num5 = global::Math.abs(this.cy - this.charFocus.cy);
															bool flag86 = num4 < 60 && num5 < 40;
															if (flag86)
															{
																bool flag87 = this.cmdMenu == null;
																if (flag87)
																{
																	this.cmdMenu = new Command(mResources.MENU, 11111);
																	this.cmdMenu.isPlaySoundButton = false;
																}
																this.cmdMenu.x = this.charFocus.cx - GameScr.cmx;
																this.cmdMenu.y = this.charFocus.cy - this.charFocus.ch - 30 - GameScr.cmy;
															}
															else
															{
																this.cmdMenu = null;
															}
														}
														else
														{
															this.cmdMenu = null;
														}
													}
													bool flag88 = this.isShadown;
													if (flag88)
													{
														this.updateShadown();
													}
													bool flag89 = this.isTeleport;
													if (!flag89)
													{
														bool flag90 = this.chatInfo != null;
														if (flag90)
														{
															this.chatInfo.update();
														}
														bool flag91 = this.shadowLife > 0;
														if (flag91)
														{
															this.shadowLife--;
														}
														bool flag92 = this.resultTest > 0 && GameCanvas.gameTick % 2 == 0;
														if (flag92)
														{
															this.resultTest -= 1;
															bool flag93 = this.resultTest == 30 || this.resultTest == 60;
															if (flag93)
															{
																this.resultTest = 0;
															}
														}
														this.updateSkillPaint();
														bool flag94 = this.mobMe != null;
														if (flag94)
														{
															this.updateMobMe();
														}
														bool flag95 = this.arr != null;
														if (flag95)
														{
															this.arr.update();
														}
														bool flag96 = this.dart != null;
														if (flag96)
														{
															this.dart.update();
														}
														this.updateEffect();
														bool flag97 = this.holdEffID != 0;
														if (flag97)
														{
															bool flag98 = GameCanvas.gameTick % 5 == 0;
															if (flag98)
															{
																EffecMn.addEff(new Effect(32, this.cx, this.cy + 24, 3, 5, 1));
															}
														}
														else
														{
															bool flag99 = this.blindEff;
															if (!flag99)
															{
																bool flag100 = this.sleepEff;
																if (!flag100)
																{
																	bool flag101 = this.holder;
																	if (flag101)
																	{
																		bool flag102 = this.charHold != null && (this.charHold.statusMe == 14 || this.charHold.statusMe == 5);
																		if (flag102)
																		{
																			this.removeHoleEff();
																		}
																		bool flag103 = this.mobHold != null && this.mobHold.status == 1;
																		if (flag103)
																		{
																			this.removeHoleEff();
																		}
																		bool flag104 = this.me && this.statusMe == 2 && this.currentMovePoint != null;
																		if (flag104)
																		{
																			this.holder = false;
																			this.charHold = null;
																			this.mobHold = null;
																		}
																		bool flag105 = TileMap.tileTypeAt(this.cx, this.cy, 2);
																		if (flag105)
																		{
																			this.cf = 16;
																		}
																		else
																		{
																			this.cf = 31;
																		}
																	}
																	else
																	{
																		bool flag106 = this.cHP > 0;
																		if (flag106)
																		{
																			for (int i = 0; i < this.vEff.size(); i++)
																			{
																				EffectChar effectChar = (EffectChar)this.vEff.elementAt(i);
																				bool flag107 = effectChar.template.type == 0 || effectChar.template.type == 12;
																				if (flag107)
																				{
																					bool isEff = GameCanvas.isEff1;
																					if (isEff)
																					{
																						this.cHP += (int)effectChar.param;
																						this.cMP += (int)effectChar.param;
																					}
																				}
																				else
																				{
																					bool flag108 = effectChar.template.type == 4 || effectChar.template.type == 17;
																					if (flag108)
																					{
																						bool isEff2 = GameCanvas.isEff1;
																						if (isEff2)
																						{
																							this.cHP += (int)effectChar.param;
																						}
																					}
																					else
																					{
																						bool flag109 = effectChar.template.type == 13 && GameCanvas.isEff1;
																						if (flag109)
																						{
																							this.cHP -= this.cHPFull * 3 / 100;
																							bool flag110 = this.cHP < 1;
																							if (flag110)
																							{
																								this.cHP = 1;
																							}
																						}
																					}
																				}
																			}
																			bool flag111 = this.eff5BuffHp > 0 && GameCanvas.isEff2;
																			if (flag111)
																			{
																				this.cHP += this.eff5BuffHp;
																			}
																			bool flag112 = this.eff5BuffMp > 0 && GameCanvas.isEff2;
																			if (flag112)
																			{
																				this.cMP += this.eff5BuffMp;
																			}
																			bool flag113 = this.cHP > this.cHPFull;
																			if (flag113)
																			{
																				this.cHP = this.cHPFull;
																			}
																			bool flag114 = this.cMP > this.cMPFull;
																			if (flag114)
																			{
																				this.cMP = this.cMPFull;
																			}
																		}
																		bool flag115 = this.cmtoChar;
																		if (flag115)
																		{
																			GameScr.cmtoX = this.cx - GameScr.gW2;
																			GameScr.cmtoY = this.cy - GameScr.gH23;
																			bool flag116 = !GameCanvas.isTouchControl;
																			if (flag116)
																			{
																				GameScr.cmtoX += GameScr.gW6 * this.cdir;
																			}
																		}
																		this.tick = (this.tick + 1) % 100;
																		bool flag117 = this.me;
																		if (flag117)
																		{
																			bool flag118 = this.charFocus != null && !GameScr.vCharInMap.contains(this.charFocus);
																			if (flag118)
																			{
																				this.charFocus = null;
																			}
																			bool flag119 = this.cx < 10;
																			if (flag119)
																			{
																				this.cvx = 0;
																				this.cx = 10;
																			}
																			else
																			{
																				bool flag120 = this.cx > TileMap.pxw - 10;
																				if (flag120)
																				{
																					this.cx = TileMap.pxw - 10;
																					this.cvx = 0;
																				}
																			}
																			bool flag121 = this.me && !global::Char.ischangingMap && this.isInWaypoint();
																			if (flag121)
																			{
																				Service.gI().charMove();
																				bool flag122 = TileMap.isTrainingMap();
																				if (flag122)
																				{
																					Service.gI().getMapOffline();
																					global::Char.ischangingMap = true;
																				}
																				else
																				{
																					Service.gI().requestChangeMap();
																				}
																				global::Char.isLockKey = true;
																				global::Char.ischangingMap = true;
																				GameCanvas.clearKeyHold();
																				GameCanvas.clearKeyPressed();
																				InfoDlg.showWait();
																				return;
																			}
																			bool flag123 = this.statusMe != 4 && Res.abs(this.cx - this.cxSend) + Res.abs(this.cy - this.cySend) >= 70 && this.cy - this.cySend <= 0 && this.me;
																			if (flag123)
																			{
																				Service.gI().charMove();
																			}
																			bool flag124 = this.isLockMove;
																			if (flag124)
																			{
																				this.currentMovePoint = null;
																			}
																			bool flag125 = this.currentMovePoint != null;
																			if (flag125)
																			{
																				bool flag126 = global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 16 && global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 16;
																				if (flag126)
																				{
																					this.cx = (this.currentMovePoint.xEnd + this.cx) / 2;
																					this.cy = this.currentMovePoint.yEnd;
																					this.currentMovePoint = null;
																					GameScr.instance.clickMoving = false;
																					this.checkPerformEndMovePointAction();
																					this.cvx = (this.cvy = 0);
																					bool flag127 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
																					if (flag127)
																					{
																						this.statusMe = 1;
																					}
																					else
																					{
																						this.setCharFallFromJump();
																					}
																					Service.gI().charMove();
																				}
																				else
																				{
																					this.cdir = ((this.currentMovePoint.xEnd <= this.cx) ? -1 : 1);
																					bool flag128 = TileMap.tileTypeAt(this.cx, this.cy, 2);
																					if (flag128)
																					{
																						this.statusMe = 2;
																						bool flag129 = this.currentMovePoint != null;
																						if (flag129)
																						{
																							this.cvx = this.cspeed * this.cdir;
																							this.cvy = 0;
																						}
																						bool flag130 = global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10;
																						if (flag130)
																						{
																							bool flag131 = this.currentMovePoint.yEnd > this.cy;
																							if (flag131)
																							{
																								bool flag3 = false;
																								bool flag132 = this.cdir == 1;
																								sbyte b;
																								if (flag132)
																								{
																									b = 1;
																								}
																								else
																								{
																									b = -1;
																								}
																								for (int j = 0; j < 2; j++)
																								{
																									bool flag133 = TileMap.tileTypeAt(this.currentMovePoint.xEnd + this.chw * (int)b, this.cy + this.chh * j, 2);
																									if (flag133)
																									{
																										flag3 = true;
																										break;
																									}
																								}
																								bool flag134 = flag3;
																								if (flag134)
																								{
																									this.currentMovePoint = null;
																									GameScr.instance.clickMoving = false;
																									this.statusMe = 1;
																									this.cvx = (this.cvy = 0);
																									this.checkPerformEndMovePointAction();
																								}
																								else
																								{
																									SoundMn.gI().charJump();
																									this.cx = this.currentMovePoint.xEnd;
																									this.statusMe = 10;
																									this.cvy = -5;
																									this.cvx = 0;
																									Res.outz("Jum lun");
																								}
																							}
																							else
																							{
																								SoundMn.gI().charJump();
																								this.cx = this.currentMovePoint.xEnd;
																								this.statusMe = 10;
																								this.cvy = -5;
																								this.cvx = 0;
																							}
																						}
																						bool flag135 = this.cdir == 1;
																						if (flag135)
																						{
																							bool flag136 = TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4);
																							if (flag136)
																							{
																								this.cvx = this.cspeed * this.cdir;
																								this.statusMe = 10;
																								this.cvy = -5;
																							}
																						}
																						else
																						{
																							bool flag137 = TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8);
																							if (flag137)
																							{
																								this.cvx = this.cspeed * this.cdir;
																								this.statusMe = 10;
																								this.cvy = -5;
																							}
																						}
																					}
																					else
																					{
																						bool flag138 = this.currentMovePoint.yEnd < this.cy + 10;
																						if (flag138)
																						{
																							this.statusMe = 10;
																							this.cvy = -5;
																							bool flag139 = global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 10;
																							if (flag139)
																							{
																								this.cy = this.currentMovePoint.yEnd;
																								this.cvy = 0;
																							}
																							bool flag140 = global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10;
																							if (flag140)
																							{
																								this.cvx = 0;
																							}
																							else
																							{
																								this.cvx = this.cspeed * this.cdir;
																							}
																						}
																						else
																						{
																							bool flag141 = TileMap.tileTypeAt(this.cx, this.cy, 2);
																							if (flag141)
																							{
																								this.currentMovePoint = null;
																								GameScr.instance.clickMoving = false;
																								this.statusMe = 1;
																								this.cvx = (this.cvy = 0);
																								this.checkPerformEndMovePointAction();
																							}
																							else
																							{
																								bool flag142 = this.statusMe == 10 || this.statusMe == 2;
																								if (flag142)
																								{
																									this.cvy = 0;
																								}
																								this.statusMe = 4;
																							}
																						}
																						bool flag143 = this.currentMovePoint.yEnd > this.cy;
																						if (flag143)
																						{
																							bool flag144 = this.cdir == 1;
																							if (flag144)
																							{
																								bool flag145 = TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4);
																								if (flag145)
																								{
																									this.cvx = (this.cvy = 0);
																									this.statusMe = 4;
																									this.currentMovePoint = null;
																									GameScr.instance.clickMoving = false;
																									this.checkPerformEndMovePointAction();
																								}
																							}
																							else
																							{
																								bool flag146 = TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8);
																								if (flag146)
																								{
																									this.cvx = (this.cvy = 0);
																									this.statusMe = 4;
																									this.currentMovePoint = null;
																									GameScr.instance.clickMoving = false;
																									this.checkPerformEndMovePointAction();
																								}
																							}
																						}
																					}
																				}
																			}
																			this.searchFocus();
																		}
																		else
																		{
																			this.checkHideCharName();
																			bool flag147 = this.statusMe == 1 || this.statusMe == 6;
																			if (flag147)
																			{
																				bool flag4 = false;
																				bool flag148 = this.currentMovePoint != null;
																				if (flag148)
																				{
																					bool flag149 = global::Char.abs(this.currentMovePoint.xEnd - this.cx) < 17 && global::Char.abs(this.currentMovePoint.yEnd - this.cy) < 25;
																					if (flag149)
																					{
																						this.cx = this.currentMovePoint.xEnd;
																						this.cy = this.currentMovePoint.yEnd;
																						this.currentMovePoint = null;
																						bool flag150 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
																						if (flag150)
																						{
																							this.statusMe = 1;
																							this.cp3 = 0;
																							GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
																							GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
																						}
																						else
																						{
																							this.statusMe = 4;
																							this.cvy = 0;
																							this.cp1 = 0;
																						}
																						flag4 = true;
																					}
																					else
																					{
																						bool flag151 = (this.statusBeforeNothing == 10 || this.cf == 8) && this.vMovePoints.size() > 0;
																						if (flag151)
																						{
																							flag4 = true;
																						}
																						else
																						{
																							bool flag152 = this.cy == this.currentMovePoint.yEnd;
																							if (flag152)
																							{
																								bool flag153 = this.cx != this.currentMovePoint.xEnd;
																								if (flag153)
																								{
																									this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
																									this.cf = GameCanvas.gameTick % 5 + 2;
																								}
																							}
																							else
																							{
																								bool flag154 = this.cy < this.currentMovePoint.yEnd;
																								if (flag154)
																								{
																									this.cf = 12;
																									this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
																									bool flag155 = this.cvy < 0;
																									if (flag155)
																									{
																										this.cvy = 0;
																									}
																									this.cy += this.cvy;
																									bool flag156 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
																									if (flag156)
																									{
																										GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
																										GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
																									}
																									this.cvy++;
																									bool flag157 = this.cvy > 16;
																									if (flag157)
																									{
																										this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
																									}
																								}
																								else
																								{
																									this.cf = 7;
																									this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
																									this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
																								}
																							}
																						}
																					}
																				}
																				else
																				{
																					flag4 = true;
																				}
																				bool flag158 = flag4 && this.vMovePoints.size() > 0;
																				if (flag158)
																				{
																					this.currentMovePoint = (MovePoint)this.vMovePoints.firstElement();
																					this.vMovePoints.removeElementAt(0);
																					bool flag159 = this.currentMovePoint.status == 2;
																					if (flag159)
																					{
																						bool flag160 = (TileMap.tileTypeAtPixel(this.cx, this.cy + 12) & 2) != 2;
																						if (flag160)
																						{
																							this.statusMe = 10;
																							this.cp1 = 0;
																							this.cp2 = 0;
																							this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
																							this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
																							bool flag161 = this.cx - this.currentMovePoint.xEnd > 0;
																							if (flag161)
																							{
																								this.cdir = -1;
																							}
																							else
																							{
																								bool flag162 = this.cx - this.currentMovePoint.xEnd < 0;
																								if (flag162)
																								{
																									this.cdir = 1;
																								}
																							}
																						}
																						else
																						{
																							this.statusMe = 2;
																							bool flag163 = this.cx - this.currentMovePoint.xEnd > 0;
																							if (flag163)
																							{
																								this.cdir = -1;
																							}
																							else
																							{
																								bool flag164 = this.cx - this.currentMovePoint.xEnd < 0;
																								if (flag164)
																								{
																									this.cdir = 1;
																								}
																							}
																							this.cvx = this.cspeed * this.cdir;
																							this.cvy = 0;
																						}
																					}
																					else
																					{
																						bool flag165 = this.currentMovePoint.status == 3;
																						if (flag165)
																						{
																							bool flag166 = (TileMap.tileTypeAtPixel(this.cx, this.cy + 23) & 2) != 2;
																							if (flag166)
																							{
																								this.statusMe = 10;
																								this.cp1 = 0;
																								this.cp2 = 0;
																								this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
																								this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
																								bool flag167 = this.cx - this.currentMovePoint.xEnd > 0;
																								if (flag167)
																								{
																									this.cdir = -1;
																								}
																								else
																								{
																									bool flag168 = this.cx - this.currentMovePoint.xEnd < 0;
																									if (flag168)
																									{
																										this.cdir = 1;
																									}
																								}
																							}
																							else
																							{
																								this.statusMe = 3;
																								GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
																								GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
																								bool flag169 = this.cx - this.currentMovePoint.xEnd > 0;
																								if (flag169)
																								{
																									this.cdir = -1;
																								}
																								else
																								{
																									bool flag170 = this.cx - this.currentMovePoint.xEnd < 0;
																									if (flag170)
																									{
																										this.cdir = 1;
																									}
																								}
																								this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 10 * this.cdir;
																								this.cvy = -10;
																							}
																						}
																						else
																						{
																							bool flag171 = this.currentMovePoint.status == 4;
																							if (flag171)
																							{
																								this.statusMe = 4;
																								bool flag172 = this.cx - this.currentMovePoint.xEnd > 0;
																								if (flag172)
																								{
																									this.cdir = -1;
																								}
																								else
																								{
																									bool flag173 = this.cx - this.currentMovePoint.xEnd < 0;
																									if (flag173)
																									{
																										this.cdir = 1;
																									}
																								}
																								this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 9 * this.cdir;
																								this.cvy = 0;
																							}
																							else
																							{
																								this.cx = this.currentMovePoint.xEnd;
																								this.cy = this.currentMovePoint.yEnd;
																								this.currentMovePoint = null;
																							}
																						}
																					}
																				}
																			}
																		}
																		switch (this.statusMe)
																		{
																		case 1:
																			this.updateCharStand();
																			break;
																		case 2:
																			this.updateCharRun();
																			break;
																		case 3:
																			this.updateCharJump();
																			break;
																		case 4:
																			this.updateCharFall();
																			break;
																		case 5:
																			this.updateCharDeadFly();
																			break;
																		case 6:
																		{
																			bool flag174 = this.isInjure <= 0;
																			if (flag174)
																			{
																				this.cf = 0;
																			}
																			else
																			{
																				bool flag175 = this.statusBeforeNothing == 10;
																				if (flag175)
																				{
																					this.cx += this.cvx;
																				}
																				else
																				{
																					bool flag176 = this.cf <= 1;
																					if (flag176)
																					{
																						this.cp1++;
																						bool flag177 = this.cp1 > 6;
																						if (flag177)
																						{
																							this.cf = 0;
																						}
																						else
																						{
																							this.cf = 1;
																						}
																						bool flag178 = this.cp1 > 10;
																						if (flag178)
																						{
																							this.cp1 = 0;
																						}
																					}
																				}
																			}
																			bool flag179 = this.cf != 7 && this.cf != 12 && (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2;
																			if (flag179)
																			{
																				this.cvx = 0;
																				this.cvy = 0;
																				this.statusMe = 4;
																				this.cf = 7;
																			}
																			bool flag180 = !this.me;
																			if (flag180)
																			{
																				this.cp3++;
																				bool flag181 = this.cp3 > 10;
																				if (flag181)
																				{
																					bool flag182 = (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2;
																					if (flag182)
																					{
																						this.cy += 5;
																					}
																					else
																					{
																						this.cf = 0;
																					}
																				}
																				bool flag183 = this.cp3 > 50;
																				if (flag183)
																				{
																					this.cp3 = 0;
																					this.currentMovePoint = null;
																				}
																			}
																			break;
																		}
																		case 9:
																			this.updateCharAutoJump();
																			break;
																		case 10:
																			this.updateCharFly();
																			break;
																		case 12:
																			this.updateSkillStand();
																			break;
																		case 13:
																			this.updateSkillFall();
																			break;
																		case 14:
																		{
																			this.cp1++;
																			bool flag184 = this.cp1 > 30;
																			if (flag184)
																			{
																				this.cp1 = 0;
																			}
																			bool flag185 = this.cp1 % 15 < 5;
																			if (flag185)
																			{
																				this.cf = 0;
																			}
																			else
																			{
																				this.cf = 1;
																			}
																			break;
																		}
																		case 16:
																			this.updateResetPoint();
																			break;
																		}
																		bool flag186 = this.isInjure > 0;
																		if (flag186)
																		{
																			this.cf = 23;
																			this.isInjure -= 1;
																		}
																		bool flag187 = this.wdx != 0 || this.wdy != 0;
																		if (flag187)
																		{
																			this.startDie(this.wdx, this.wdy);
																			this.wdx = 0;
																			this.wdy = 0;
																		}
																		bool flag188 = this.moveFast != null;
																		if (flag188)
																		{
																			bool flag189 = this.moveFast[0] == 0;
																			if (flag189)
																			{
																				short[] array2 = this.moveFast;
																				int num6 = 0;
																				short[] array4 = array2;
																				int num9 = num6;
																				array4[num9] += 1;
																				ServerEffect.addServerEffect(60, this, 1);
																			}
																			else
																			{
																				bool flag190 = this.moveFast[0] < 10;
																				if (flag190)
																				{
																					short[] array3 = this.moveFast;
																					int num7 = 0;
																					short[] array5 = array3;
																					int num10 = num7;
																					array5[num10] += 1;
																				}
																				else
																				{
																					this.cx = (int)this.moveFast[1];
																					this.cy = (int)this.moveFast[2];
																					this.moveFast = null;
																					ServerEffect.addServerEffect(60, this, 1);
																					bool flag191 = this.me;
																					if (flag191)
																					{
																						bool flag192 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2;
																						if (flag192)
																						{
																							this.statusMe = 4;
																							global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
																						}
																						else
																						{
																							Service.gI().charMove();
																							global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
																						}
																					}
																				}
																			}
																		}
																		bool flag193 = this.statusMe != 10;
																		if (flag193)
																		{
																			this.fy = 0;
																		}
																		bool flag194 = this.isCharge;
																		if (flag194)
																		{
																			this.cf = 17;
																			bool flag195 = GameCanvas.gameTick % 4 == 0;
																			if (flag195)
																			{
																				ServerEffect.addServerEffect(1, this.cx, this.cy + GameCanvas.transY, 1);
																			}
																			bool flag196 = this.me;
																			if (flag196)
																			{
																				long num8 = mSystem.currentTimeMillis();
																				bool flag197 = num8 - this.last >= 1000L;
																				if (flag197)
																				{
																					Res.outz("%= " + this.myskill.damage.ToString());
																					this.last = num8;
																					this.cHP += this.cHPFull * (int)this.myskill.damage / 100;
																					this.cMP += this.cMPFull * (int)this.myskill.damage / 100;
																					bool flag198 = this.cHP < this.cHPFull;
																					if (flag198)
																					{
																						GameScr.startFlyText(string.Concat(new object[]
																						{
																							"+",
																							this.cHPFull * (int)this.myskill.damage / 100,
																							" ",
																							mResources.HP
																						}), this.cx, this.cy - this.ch - 20, 0, -1, mFont.HP);
																					}
																					bool flag199 = this.cMP < this.cMPFull;
																					if (flag199)
																					{
																						GameScr.startFlyText(string.Concat(new object[]
																						{
																							"+",
																							this.cMPFull * (int)this.myskill.damage / 100,
																							" ",
																							mResources.KI
																						}), this.cx, this.cy - this.ch - 20, 0, -2, mFont.MP);
																					}
																					Service.gI().skill_not_focus(2);
																				}
																			}
																		}
																		bool flag200 = this.isFlyUp;
																		if (flag200)
																		{
																			bool flag201 = this.me;
																			if (flag201)
																			{
																				global::Char.isLockKey = true;
																				this.statusMe = 3;
																				this.cvy = -8;
																				bool flag202 = this.cy <= TileMap.pxh - 240;
																				if (flag202)
																				{
																					this.isFlyUp = false;
																					global::Char.isLockKey = false;
																					this.statusMe = 4;
																				}
																			}
																			else
																			{
																				this.statusMe = 3;
																				this.cvy = -8;
																				bool flag203 = this.cy <= TileMap.pxh - 240;
																				if (flag203)
																				{
																					this.cvy = 0;
																					this.isFlyUp = false;
																					this.cvy = 0;
																					this.statusMe = 1;
																				}
																			}
																		}
																		this.updateMount();
																		this.updEffChar();
																		this.updateEye();
																		this.updateFHead();
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000BE38 File Offset: 0x0000A038
	private void updateEffect()
	{
		bool flag = this.effPaints != null;
		if (flag)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				bool flag2 = this.effPaints[i] != null;
				if (flag2)
				{
					bool flag3 = this.effPaints[i].eMob != null;
					if (flag3)
					{
						bool flag4 = !this.effPaints[i].isFly;
						if (flag4)
						{
							this.effPaints[i].eMob.setInjure();
							this.effPaints[i].eMob.injureBy = this;
							bool flag5 = this.me;
							if (flag5)
							{
								this.effPaints[i].eMob.hpInjure = global::Char.myCharz().cDamFull / 2 - global::Char.myCharz().cDamFull * NinjaUtil.randomNumber(11) / 100;
							}
							int num = this.effPaints[i].eMob.h >> 1;
							bool flag6 = this.effPaints[i].eMob.isBigBoss();
							if (flag6)
							{
								num = this.effPaints[i].eMob.getY() + 20;
							}
							GameScr.startSplash(this.effPaints[i].eMob.x, this.effPaints[i].eMob.y - num, this.cdir);
							this.effPaints[i].isFly = true;
						}
					}
					else
					{
						bool flag7 = this.effPaints[i].eChar != null && !this.effPaints[i].isFly;
						if (flag7)
						{
							bool flag8 = this.effPaints[i].eChar.charID >= 0;
							if (flag8)
							{
								this.effPaints[i].eChar.doInjure();
							}
							GameScr.startSplash(this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy - (this.effPaints[i].eChar.ch >> 1), this.cdir);
							this.effPaints[i].isFly = true;
						}
					}
					this.effPaints[i].index++;
					bool flag9 = this.effPaints[i].index >= this.effPaints[i].effCharPaint.arrEfInfo.Length;
					if (flag9)
					{
						this.effPaints[i] = null;
					}
				}
			}
		}
		bool flag10 = this.indexEff >= 0 && this.eff != null && GameCanvas.gameTick % 2 == 0;
		if (flag10)
		{
			this.indexEff++;
			bool flag11 = this.indexEff >= this.eff.arrEfInfo.Length;
			if (flag11)
			{
				this.indexEff = -1;
				this.eff = null;
			}
		}
		bool flag12 = this.indexEffTask >= 0 && this.effTask != null && GameCanvas.gameTick % 2 == 0;
		if (flag12)
		{
			this.indexEffTask++;
			bool flag13 = this.indexEffTask >= this.effTask.arrEfInfo.Length;
			if (flag13)
			{
				this.indexEffTask = -1;
				this.effTask = null;
			}
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000C17C File Offset: 0x0000A37C
	private void checkPerformEndMovePointAction()
	{
		bool flag = this.endMovePointCommand != null;
		if (flag)
		{
			Command command = this.endMovePointCommand;
			this.endMovePointCommand = null;
			command.performAction();
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
	private void checkHideCharName()
	{
		bool flag = GameCanvas.gameTick % 20 == 0 && this.charID >= 0;
		if (flag)
		{
			this.paintName = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = null;
				try
				{
					@char = (global::Char)GameScr.vCharInMap.elementAt(i);
				}
				catch (Exception ex)
				{
				}
				bool flag2 = @char != null && !@char.Equals(this);
				if (flag2)
				{
					bool flag3 = (@char.cy == this.cy && Res.abs(@char.cx - this.cx) < 35) || (this.cy - @char.cy < 32 && this.cy - @char.cy > 0 && Res.abs(@char.cx - this.cx) < 24);
					if (flag3)
					{
						this.paintName = false;
					}
				}
			}
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc = null;
				try
				{
					npc = (Npc)GameScr.vNpc.elementAt(j);
				}
				catch (Exception ex2)
				{
				}
				bool flag4 = npc != null;
				if (flag4)
				{
					bool flag5 = npc.cy == this.cy && Res.abs(npc.cx - this.cx) < 24;
					if (flag5)
					{
						this.paintName = false;
					}
				}
			}
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000C354 File Offset: 0x0000A554
	private void updateMobMe()
	{
		bool flag = this.tMobMeBorn != 0;
		if (flag)
		{
			this.tMobMeBorn--;
		}
		bool flag2 = this.tMobMeBorn == 0;
		if (flag2)
		{
			this.mobMe.xFirst = ((this.cdir != 1) ? (this.cx + 30) : (this.cx - 30));
			this.mobMe.yFirst = this.cy - 60;
			int num = this.mobMe.xFirst - this.mobMe.x;
			int num2 = this.mobMe.yFirst - this.mobMe.y;
			this.mobMe.x += num / 4;
			this.mobMe.y += num2 / 4;
			this.mobMe.dir = this.cdir;
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000C438 File Offset: 0x0000A638
	private void updateSkillPaint()
	{
		bool flag = this.statusMe == 14 || this.statusMe == 5;
		if (!flag)
		{
			bool flag2 = this.skillPaint != null && ((this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus) && this.charFocus.statusMe == 14) || (this.mobFocus != null && this.mobFocus.status == 0));
			if (flag2)
			{
				bool flag3 = !this.me;
				if (flag3)
				{
					bool flag4 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
					if (flag4)
					{
						this.statusMe = 1;
					}
					else
					{
						this.statusMe = 6;
					}
					this.cp3 = 0;
				}
				this.indexSkill = 0;
				this.skillPaint = null;
				this.skillPaintRandomPaint = null;
				this.eff0 = (this.eff1 = (this.eff2 = null));
				this.i0 = (this.i1 = (this.i2 = 0));
				this.mobFocus = null;
				this.charFocus = null;
				this.effPaints = null;
				this.currentMovePoint = null;
				this.arr = null;
				bool flag5 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2;
				if (flag5)
				{
					this.delayFall = 5;
				}
			}
			bool flag6 = this.skillPaint != null && this.arr == null && this.skillInfoPaint() != null && this.indexSkill >= this.skillInfoPaint().Length;
			if (flag6)
			{
				bool flag7 = !this.me;
				if (flag7)
				{
					bool flag8 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
					if (flag8)
					{
						this.statusMe = 1;
					}
					else
					{
						this.statusMe = 6;
					}
					this.cp3 = 0;
				}
				this.indexSkill = 0;
				Res.outz("remove 2");
				this.skillPaint = null;
				this.skillPaintRandomPaint = null;
				this.eff0 = (this.eff1 = (this.eff2 = null));
				this.i0 = (this.i1 = (this.i2 = 0));
				this.arr = null;
				bool flag9 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2;
				if (flag9)
				{
					this.delayFall = 5;
				}
			}
			SkillInfoPaint[] array = this.skillInfoPaint();
			bool flag10 = array != null && this.indexSkill >= 0 && this.indexSkill <= array.Length - 1;
			if (flag10)
			{
				bool flag11 = array[this.indexSkill].effS0Id != 0;
				if (flag11)
				{
					this.eff0 = GameScr.efs[array[this.indexSkill].effS0Id - 1];
					this.i0 = (this.dx0 = (this.dy0 = 0));
				}
				bool flag12 = array[this.indexSkill].effS1Id != 0;
				if (flag12)
				{
					this.eff1 = GameScr.efs[array[this.indexSkill].effS1Id - 1];
					this.i1 = (this.dx1 = (this.dy1 = 0));
				}
				bool flag13 = array[this.indexSkill].effS2Id != 0;
				if (flag13)
				{
					this.eff2 = GameScr.efs[array[this.indexSkill].effS2Id - 1];
					this.i2 = (this.dx2 = (this.dy2 = 0));
				}
				SkillInfoPaint[] array2 = array;
				int num = this.indexSkill;
				bool flag14 = array2 != null && array2[num] != null && num >= 0 && num <= array2.Length - 1 && array2[num].arrowId != 0;
				if (flag14)
				{
					int arrowId = array2[num].arrowId;
					bool flag15 = arrowId >= 100;
					if (flag15)
					{
						bool flag16 = this.mobFocus == null;
						IMapObject mapObject2;
						if (flag16)
						{
							IMapObject mapObject = this.charFocus;
							mapObject2 = mapObject;
						}
						else
						{
							mapObject2 = this.mobFocus;
						}
						IMapObject mapObject3 = mapObject2;
						bool flag17 = mapObject3 != null;
						if (flag17)
						{
							int num2 = Res.abs(mapObject3.getX() - this.cx);
							int num3 = Res.abs(mapObject3.getY() - this.cy);
							bool flag18 = num2 > 4 * num3;
							int num4;
							if (flag18)
							{
								num4 = 0;
							}
							else
							{
								bool flag19 = mapObject3.getY() < this.cy;
								if (flag19)
								{
									num4 = -3;
								}
								else
								{
									num4 = 3;
								}
								bool flag20 = mapObject3 is BigBoss;
								if (flag20)
								{
									BigBoss bigBoss = (BigBoss)mapObject3;
									bool haftBody = bigBoss.haftBody;
									if (haftBody)
									{
										num4 = -20;
									}
								}
							}
							this.dart = new PlayerDart(this, arrowId - 100, this.skillPaintRandomPaint, this.cx + (array2[num].adx - 10) * this.cdir, this.cy + array2[num].ady + num4);
							bool flag21 = this.myskill != null;
							if (flag21)
							{
								bool flag22 = this.myskill.template.id == 1;
								if (flag22)
								{
									SoundMn.gI().traidatKame();
								}
								else
								{
									bool flag23 = this.myskill.template.id == 3;
									if (flag23)
									{
										SoundMn.gI().namekKame();
									}
									else
									{
										bool flag24 = this.myskill.template.id == 5;
										if (flag24)
										{
											SoundMn.gI().xaydaKame();
										}
										else
										{
											bool flag25 = this.myskill.template.id == 11;
											if (flag25)
											{
												SoundMn.gI().nameLazer();
											}
										}
									}
								}
							}
						}
						else
						{
							bool flag26 = this.isFlyAndCharge || this.isUseSkillAfterCharge;
							if (flag26)
							{
								this.stopUseChargeSkill();
							}
						}
					}
					else
					{
						Res.outz("g");
						this.arr = new Arrow(this, GameScr.arrs[arrowId - 1]);
						this.arr.life = 10;
						this.arr.ax = this.cx + array2[num].adx;
						this.arr.ay = this.cy + array2[num].ady;
					}
				}
				bool flag27 = (this.mobFocus != null || (!this.me && this.charFocus != null) || (this.me && this.charFocus != null && (this.isMeCanAttackOtherPlayer(this.charFocus) || this.isSelectingSkillBuffToPlayer()) && this.arr == null && this.dart == null)) && this.indexSkill == array.Length - 1;
				if (flag27)
				{
					this.setAttack();
					bool flag28 = this.me && this.myskill.template.isAttackSkill();
					if (flag28)
					{
						this.saveLoadPreviousSkill();
					}
				}
				bool flag29 = !this.me;
				if (flag29)
				{
					IMapObject mapObject4 = null;
					bool flag30 = this.mobFocus != null;
					if (flag30)
					{
						mapObject4 = this.mobFocus;
					}
					else
					{
						bool flag31 = this.charFocus != null;
						if (flag31)
						{
							mapObject4 = this.charFocus;
						}
					}
					bool flag32 = mapObject4 != null;
					if (flag32)
					{
						bool flag33 = Res.abs(mapObject4.getX() - this.cx) < 10;
						if (flag33)
						{
							bool flag34 = mapObject4.getX() > this.cx;
							if (flag34)
							{
								this.cx -= 10;
							}
							else
							{
								this.cx += 10;
							}
						}
						bool flag35 = mapObject4.getX() > this.cx;
						if (flag35)
						{
							this.cdir = 1;
						}
						else
						{
							this.cdir = -1;
						}
					}
				}
			}
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00003136 File Offset: 0x00001336
	public void saveLoadPreviousSkill()
	{
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000CBEC File Offset: 0x0000ADEC
	public void setResetPoint(int x, int y)
	{
		InfoDlg.hide();
		this.currentMovePoint = null;
		int num = this.cx - x;
		bool flag = this.cy - y == 0;
		if (flag)
		{
			this.cx = x;
			global::Char.ischangingMap = false;
			global::Char.isLockKey = false;
		}
		else
		{
			this.statusMe = 16;
			this.cp2 = x;
			this.cp3 = y;
			this.cp1 = 0;
			global::Char.myCharz().cxSend = x;
			global::Char.myCharz().cySend = y;
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000CC68 File Offset: 0x0000AE68
	private void updateCharDeadFly()
	{
		this.isFreez = false;
		bool flag = this.isCharge;
		if (flag)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		bool flag2 = this.cp1 > 7;
		if (flag2)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		bool flag3 = Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10;
		if (flag3)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 14;
			bool flag4 = this.me;
			if (flag4)
			{
				GameScr.gI().resetButton();
				Service.gI().charMove();
			}
		}
		this.cf = 23;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000CD94 File Offset: 0x0000AF94
	private void updateResetPoint()
	{
		InfoDlg.hide();
		GameCanvas.clearAllPointerEvent();
		this.currentMovePoint = null;
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		bool flag = this.cp1 > 7;
		if (flag)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		bool flag2 = Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10;
		if (flag2)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 1;
			this.cp3 = 0;
			global::Char.ischangingMap = false;
			Service.gI().charMove();
		}
		this.cf = 23;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00003136 File Offset: 0x00001336
	public void updateSkillFall()
	{
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000CE98 File Offset: 0x0000B098
	public void updateSkillStand()
	{
		this.ty = 0;
		this.cp1++;
		bool flag = this.cdir == 1;
		if (flag)
		{
			bool flag2 = (TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - this.chh) & 4) == 4;
			if (flag2)
			{
				this.cvx = 0;
			}
		}
		else
		{
			bool flag3 = (TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - this.chh) & 8) == 8;
			if (flag3)
			{
				this.cvx = 0;
			}
		}
		bool flag4 = this.cy > this.ch && TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192);
		if (flag4)
		{
			bool flag5 = !TileMap.tileTypeAt(this.cx, this.cy, 2);
			if (flag5)
			{
				this.statusMe = 4;
				this.cp1 = 0;
				this.cp2 = 0;
				this.cvy = 1;
			}
			else
			{
				this.cy = TileMap.tileYofPixel(this.cy);
			}
		}
		this.cx += this.cvx;
		this.cy += this.cvy;
		bool flag6 = this.cy < 0;
		if (flag6)
		{
			this.cy = (this.cvy = 0);
		}
		bool flag7 = this.cvy == 0;
		if (flag7)
		{
			bool flag8 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2;
			if (flag8)
			{
				this.statusMe = 4;
				this.cvx = (this.cspeed >> 1) * this.cdir;
				this.cp1 = (this.cp2 = 0);
			}
		}
		else
		{
			bool flag9 = this.cvy < 0;
			if (flag9)
			{
				this.cvy++;
				bool flag10 = this.cvy == 0;
				if (flag10)
				{
					this.cvy = 1;
				}
			}
			else
			{
				bool flag11 = this.cvy < 20 && this.cp1 % 5 == 0;
				if (flag11)
				{
					this.cvy++;
				}
				bool flag12 = this.cvy > 3;
				if (flag12)
				{
					this.cvy = 3;
				}
				bool flag13 = (TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2 && this.cy <= TileMap.tileXofPixel(this.cy + 3);
				if (flag13)
				{
					this.cvx = (this.cvy = 0);
					this.cy = TileMap.tileXofPixel(this.cy + 3);
				}
			}
		}
		bool flag14 = this.cvx > 0;
		if (flag14)
		{
			this.cvx--;
		}
		else
		{
			bool flag15 = this.cvx < 0;
			if (flag15)
			{
				this.cvx++;
			}
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000D188 File Offset: 0x0000B388
	public void updateCharAutoJump()
	{
		this.isFreez = false;
		bool flag = this.isCharge;
		if (flag)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cx += this.cvx * this.cdir;
		this.cy += this.cvyJump;
		this.cvyJump++;
		bool flag2 = this.cp1 == 0;
		if (flag2)
		{
			this.cf = 7;
		}
		else
		{
			this.cf = 23;
		}
		bool flag3 = this.cvyJump == -3;
		if (flag3)
		{
			this.cf = 8;
		}
		else
		{
			bool flag4 = this.cvyJump == -2;
			if (flag4)
			{
				this.cf = 9;
			}
			else
			{
				bool flag5 = this.cvyJump == -1;
				if (flag5)
				{
					this.cf = 10;
				}
				else
				{
					bool flag6 = this.cvyJump == 0;
					if (flag6)
					{
						this.cf = 11;
					}
				}
			}
		}
		bool flag7 = this.cvyJump == 0;
		if (flag7)
		{
			this.statusMe = 6;
			this.cp3 = 0;
			((MovePoint)this.vMovePoints.firstElement()).status = 4;
			this.isJump = true;
			this.cp1 = 0;
			this.cvy = 1;
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
	public int getVx(int size, int dx, int dy)
	{
		bool flag = dy > 0 && !TileMap.tileTypeAt(this.cx, this.cy, 2);
		if (flag)
		{
			bool flag2 = dx - dy <= 10;
			if (flag2)
			{
				return 5;
			}
			bool flag3 = dx - dy <= 30;
			if (flag3)
			{
				return 6;
			}
			bool flag4 = dx - dy <= 50;
			if (flag4)
			{
				return 7;
			}
			bool flag5 = dx - dy <= 70;
			if (flag5)
			{
				return 8;
			}
		}
		bool flag6 = dx <= 30;
		int result;
		if (flag6)
		{
			result = 4;
		}
		else
		{
			bool flag7 = dx <= 160;
			if (flag7)
			{
				result = 5;
			}
			else
			{
				bool flag8 = dx <= 270;
				if (flag8)
				{
					result = 6;
				}
				else
				{
					bool flag9 = dx <= 320;
					if (flag9)
					{
						result = 7;
					}
					else
					{
						result = 8;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
	public void hide()
	{
		this.isHide = true;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 15, 1));
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
	public void show()
	{
		this.isHide = false;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 10, 1));
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000D40C File Offset: 0x0000B60C
	public int getVy(int size, int dx, int dy)
	{
		bool flag = dy <= 10;
		int result;
		if (flag)
		{
			result = 5;
		}
		else
		{
			bool flag2 = dy <= 20;
			if (flag2)
			{
				result = 6;
			}
			else
			{
				bool flag3 = dy <= 30;
				if (flag3)
				{
					result = 7;
				}
				else
				{
					bool flag4 = dy <= 40;
					if (flag4)
					{
						result = 8;
					}
					else
					{
						bool flag5 = dy <= 50;
						if (flag5)
						{
							result = 9;
						}
						else
						{
							result = 10;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000D47C File Offset: 0x0000B67C
	public int returnAct(int xFirst, int yFirst, int xEnd, int yEnd)
	{
		int num = xEnd - xFirst;
		int num2 = yEnd - yFirst;
		bool flag = num == 0 && num2 == 0;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = num2 == 0 && yFirst % 24 == 0 && TileMap.tileTypeAt(xFirst, yFirst, 2);
			if (flag2)
			{
				result = 2;
			}
			else
			{
				bool flag3 = num2 > 0 && (yFirst % 24 != 0 || !TileMap.tileTypeAt(xFirst, yFirst, 2));
				if (flag3)
				{
					result = 4;
				}
				else
				{
					this.cvy = -10;
					this.cp1 = 0;
					this.cdir = ((num <= 0) ? -1 : 1);
					bool flag4 = num <= 5;
					if (flag4)
					{
						this.cvx = 0;
					}
					else
					{
						bool flag5 = num <= 10;
						if (flag5)
						{
							this.cvx = 3;
						}
						else
						{
							this.cvx = 5;
						}
					}
					result = 9;
				}
			}
		}
		return result;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000D550 File Offset: 0x0000B750
	public void setAutoJump()
	{
		int num = ((MovePoint)this.vMovePoints.firstElement()).xEnd - this.cx;
		this.cvyJump = -10;
		this.cp1 = 0;
		this.cdir = ((num <= 0) ? -1 : 1);
		bool flag = num <= 6;
		if (flag)
		{
			this.cvx = 0;
		}
		else
		{
			bool flag2 = num <= 20;
			if (flag2)
			{
				this.cvx = 3;
			}
			else
			{
				this.cvx = 5;
			}
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
	public void updateCharStand()
	{
		this.isSoundJump = false;
		this.isAttack = false;
		this.isAttFly = false;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1++;
		bool flag = this.cp1 > 30;
		if (flag)
		{
			this.cp1 = 0;
		}
		bool flag2 = this.cp1 % 15 < 5;
		if (flag2)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		this.updateCharInBridge();
		bool flag3 = !this.me;
		if (flag3)
		{
			this.cp3++;
			bool flag4 = this.cp3 > 50;
			if (flag4)
			{
				this.cp3 = 0;
				this.currentMovePoint = null;
			}
		}
		this.updateSuperEff();
		bool flag5 = this.me && GameScr.vCharInMap.size() != 0 && TileMap.mapID == 50;
		if (flag5)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(0);
			bool flag6 = !@char.changePos;
			if (flag6)
			{
				bool flag7 = @char.statusMe != 2;
				if (flag7)
				{
					@char.moveTo(this.cx - 45, this.cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				bool flag8 = Res.abs(this.cx - 45 - @char.cx) <= 10;
				if (flag8)
				{
					@char.changePos = true;
				}
			}
			else
			{
				bool flag9 = @char.statusMe != 2;
				if (flag9)
				{
					@char.moveTo(this.cx + 45, this.cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				bool flag10 = Res.abs(this.cx + 45 - @char.cx) <= 10;
				if (flag10)
				{
					@char.changePos = false;
				}
			}
			bool flag11 = GameCanvas.gameTick % 100 == 0;
			if (flag11)
			{
				@char.addInfo("Cắc cùm cum");
			}
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
	public void updateSuperEff()
	{
		bool isShow = GameCanvas.panel.isShow;
		if (!isShow)
		{
			bool flag = this.isCopy;
			if (!flag)
			{
				bool flag2 = this.isFusion;
				if (!flag2)
				{
					bool flag3 = this.isSetPos;
					if (!flag3)
					{
						bool flag4 = this.isPet || this.isMiniPet;
						if (!flag4)
						{
							bool flag5 = this.isMonkey == 1;
							if (!flag5)
							{
								bool flag6 = this.me;
								if (flag6)
								{
									bool flag7 = !global::Char.isPaintAura && this.idAuraEff > -1;
									if (flag7)
									{
										return;
									}
								}
								else
								{
									bool flag8 = this.idAuraEff > -1;
									if (flag8)
									{
										return;
									}
								}
								this.ty++;
								bool flag9 = this.clevel < 14;
								if (flag9)
								{
									bool flag10 = this.clevel >= 9 && !GameCanvas.lowGraphic && (this.ty == 40 || this.ty == 50);
									if (flag10)
									{
										GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
										GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
										this.addDustEff(1);
									}
									bool flag11 = this.ty > 50 && this.clevel >= 9;
									if (flag11)
									{
										bool flag12 = this.cgender == 0;
										if (flag12)
										{
											bool flag13 = GameCanvas.gameTick % 25 == 0;
											if (flag13)
											{
												int id = 114;
												ServerEffect.addServerEffect(id, this, 1);
											}
											bool flag14 = this.clevel >= 13 && GameCanvas.gameTick % 4 == 0;
											if (flag14)
											{
												int id2 = 132;
												ServerEffect.addServerEffect(id2, this, 1);
											}
										}
										bool flag15 = this.cgender == 1;
										if (flag15)
										{
											bool flag16 = GameCanvas.gameTick % 4 == 0;
											if (flag16)
											{
												int id3 = 132;
												ServerEffect.addServerEffect(id3, this, 1);
											}
											bool flag17 = this.clevel >= 13 && GameCanvas.gameTick % 7 == 0;
											if (flag17)
											{
												int id4 = 131;
												ServerEffect.addServerEffect(id4, this, 1);
											}
										}
										bool flag18 = this.cgender == 2;
										if (flag18)
										{
											bool flag19 = GameCanvas.gameTick % 7 == 0;
											if (flag19)
											{
												int id5 = 131;
												ServerEffect.addServerEffect(id5, this, 1);
											}
											bool flag20 = this.clevel >= 13 && GameCanvas.gameTick % 25 == 0;
											if (flag20)
											{
												int id6 = 114;
												ServerEffect.addServerEffect(id6, this, 1);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000DA7C File Offset: 0x0000BC7C
	public float getSoundVolumn()
	{
		bool flag = this.me;
		float result;
		if (flag)
		{
			result = 0.1f;
		}
		else
		{
			int num = Res.abs(global::Char.myChar.cx - this.cx);
			bool flag2 = num >= 0 && num <= 50;
			if (flag2)
			{
				result = 0.1f;
			}
			else
			{
				result = 0.05f;
			}
		}
		return result;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000DADC File Offset: 0x0000BCDC
	public void updateCharRun()
	{
		int num = (this.isMonkey != 1 || this.me) ? 1 : 2;
		bool flag = this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			bool flag2 = this.isMonkey == 0;
			if (flag2)
			{
				SoundMn.gI().charRun(this.getSoundVolumn());
			}
			else
			{
				SoundMn.gI().monkeyRun(this.getSoundVolumn());
			}
		}
		this.ty = 0;
		this.isFreez = false;
		bool flag3 = this.isCharge;
		if (flag3)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		int num2 = 0;
		bool flag4 = !this.me && this.currentMovePoint != null;
		if (flag4)
		{
			num2 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
		}
		this.cp1++;
		bool flag5 = this.cp1 >= 10;
		if (flag5)
		{
			this.cp1 = 0;
			this.cBonusSpeed = 0;
		}
		this.cf = (this.cp1 >> 1) + 2;
		bool flag6 = (TileMap.tileTypeAtPixel(this.cx, this.cy - 1) & 64) == 64;
		if (flag6)
		{
			this.cx += this.cvx * num >> 1;
		}
		else
		{
			this.cx += this.cvx * num;
		}
		bool flag7 = this.cdir == 1;
		if (flag7)
		{
			bool flag8 = TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4);
			if (flag8)
			{
				bool flag9 = this.me;
				if (flag9)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				}
				else
				{
					this.stop();
				}
			}
		}
		else
		{
			bool flag10 = TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8);
			if (flag10)
			{
				bool flag11 = this.me;
				if (flag11)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
				}
				else
				{
					this.stop();
				}
			}
		}
		bool flag12 = this.me;
		if (flag12)
		{
			bool flag13 = this.cvx > 0;
			if (flag13)
			{
				this.cvx--;
			}
			else
			{
				bool flag14 = this.cvx < 0;
				if (flag14)
				{
					this.cvx++;
				}
				else
				{
					bool flag15 = this.cx - this.cxSend != 0 && this.me;
					if (flag15)
					{
						Service.gI().charMove();
					}
					this.statusMe = 1;
					this.cBonusSpeed = 0;
				}
			}
		}
		bool flag16 = (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2;
		if (flag16)
		{
			bool flag17 = this.me;
			if (flag17)
			{
				bool flag18 = this.cx - this.cxSend != 0 || this.cy - this.cySend != 0;
				if (flag18)
				{
					Service.gI().charMove();
				}
				this.cf = 7;
				this.statusMe = 4;
				this.delayFall = 0;
				this.cvx = 3 * this.cdir;
				this.cp2 = 0;
			}
			else
			{
				this.stop();
			}
		}
		bool flag19 = !this.me;
		if (flag19)
		{
			bool flag20 = this.currentMovePoint != null;
			if (flag20)
			{
				int num3 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
				bool flag21 = num3 > num2;
				if (flag21)
				{
					this.stop();
				}
			}
		}
		GameCanvas.gI().startDust(this.cdir, this.cx - (this.cdir << 3), this.cy);
		this.updateCharInBridge();
		this.addDustEff(2);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
	private void stop()
	{
		this.statusMe = 6;
		this.cp3 = 0;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1 = (this.cp2 = 0);
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000DF34 File Offset: 0x0000C134
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000DF50 File Offset: 0x0000C150
	public void updateCharJump()
	{
		this.setMountIsStart();
		this.ty = 0;
		this.isFreez = false;
		bool flag = this.isCharge;
		if (flag)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.addDustEff(3);
		this.cx += this.cvx;
		this.cy += this.cvy;
		bool flag2 = this.cy < 0;
		if (flag2)
		{
			this.cy = 0;
			this.cvy = -1;
		}
		this.cvy++;
		bool flag3 = this.cvy > 0;
		if (flag3)
		{
			this.cvy = 0;
		}
		bool flag4 = !this.me && this.currentMovePoint != null;
		if (flag4)
		{
			int num = this.currentMovePoint.xEnd - this.cx;
			bool flag5 = num > 0;
			if (flag5)
			{
				bool flag6 = this.cvx > num;
				if (flag6)
				{
					this.cvx = num;
				}
				bool flag7 = this.cvx < 0;
				if (flag7)
				{
					this.cvx = num;
				}
			}
			else
			{
				bool flag8 = num < 0;
				if (flag8)
				{
					bool flag9 = this.cvx < num;
					if (flag9)
					{
						this.cvx = num;
					}
					bool flag10 = this.cvx > 0;
					if (flag10)
					{
						this.cvx = num;
					}
				}
				else
				{
					this.cvx = num;
				}
			}
		}
		bool flag11 = this.cdir == 1;
		if (flag11)
		{
			bool flag12 = (TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12;
			if (flag12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				this.cvx = 0;
			}
		}
		else
		{
			bool flag13 = (TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12;
			if (flag13)
			{
				this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
				this.cvx = 0;
			}
		}
		bool flag14 = this.cvy == 0;
		if (flag14)
		{
			bool flag15 = !this.isAttFly;
			if (flag15)
			{
				bool flag16 = this.me;
				if (flag16)
				{
					this.setCharFallFromJump();
				}
				else
				{
					this.stop();
				}
			}
			else
			{
				this.setCharFallFromJump();
			}
		}
		bool flag17 = this.me && !global::Char.ischangingMap && this.isInWaypoint();
		if (flag17)
		{
			Service.gI().charMove();
			bool flag18 = TileMap.isTrainingMap();
			if (flag18)
			{
				global::Char.ischangingMap = true;
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().requestChangeMap();
			}
			global::Char.isLockKey = true;
			global::Char.ischangingMap = true;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			InfoDlg.showWait();
		}
		else
		{
			bool flag19 = this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0);
			if (flag19)
			{
				this.statusMe = 4;
				this.cp1 = 0;
				this.cp2 = 0;
				this.cvy = 1;
				this.delayFall = 0;
				bool flag20 = this.cy < 0;
				if (flag20)
				{
					this.cy = 0;
				}
				this.cy = TileMap.tileYofPixel(this.cy + 25);
				GameCanvas.clearKeyHold();
			}
			bool flag21 = this.cp3 < 0;
			if (flag21)
			{
				this.cp3++;
			}
			this.cf = 7;
			bool flag22 = !this.me;
			if (flag22)
			{
				bool flag23 = this.currentMovePoint != null && this.cy < this.currentMovePoint.yEnd;
				if (flag23)
				{
					this.stop();
				}
			}
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000E384 File Offset: 0x0000C584
	public bool checkInRangeJump(int x1, int xw1, int xmob, int y1, int yh1, int ymob)
	{
		return xmob <= xw1 && xmob >= x1 && ymob <= y1 && ymob >= yh1;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
	public void setCharFallFromJump()
	{
		this.cyStartFall = this.cy;
		this.cp1 = 0;
		this.cp2 = 0;
		this.statusMe = 10;
		this.cvx = this.cdir << 2;
		this.cvy = 0;
		this.cy = TileMap.tileYofPixel(this.cy) + 12;
		bool flag = this.me && (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24);
		if (flag)
		{
			Service.gI().charMove();
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000E484 File Offset: 0x0000C684
	public void updateCharFall()
	{
		bool flag = this.holder;
		if (!flag)
		{
			this.ty = 0;
			bool flag2 = this.cy + 4 >= TileMap.pxh;
			if (flag2)
			{
				this.statusMe = 1;
				bool flag3 = this.me;
				if (flag3)
				{
					SoundMn.gI().charFall();
				}
				this.cvx = (this.cvy = 0);
				this.cp3 = 0;
			}
			else
			{
				bool flag4 = this.cy % 24 == 0 && (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2;
				if (flag4)
				{
					this.delayFall = 0;
					bool flag5 = this.me;
					if (flag5)
					{
						bool flag6 = this.cy - this.cySend > 0;
						if (flag6)
						{
							Service.gI().charMove();
						}
						else
						{
							bool flag7 = this.cx - this.cxSend != 0 || this.cy - this.cySend < 0;
							if (flag7)
							{
								Service.gI().charMove();
							}
						}
						this.cvx = (this.cvy = 0);
						this.cp1 = (this.cp2 = 0);
						this.statusMe = 1;
						this.cp3 = 0;
						return;
					}
					this.stop();
					this.cf = 0;
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
				}
				bool flag8 = this.delayFall > 0;
				if (flag8)
				{
					this.delayFall--;
					bool flag9 = this.delayFall % 10 > 5;
					if (flag9)
					{
						this.cy++;
					}
					else
					{
						this.cy--;
					}
				}
				else
				{
					bool flag10 = this.cvy < -4;
					if (flag10)
					{
						this.cf = 7;
					}
					else
					{
						this.cf = 12;
					}
					this.cx += this.cvx;
					bool flag11 = !this.me && this.currentMovePoint != null;
					if (flag11)
					{
						int num = this.currentMovePoint.xEnd - this.cx;
						bool flag12 = num > 0;
						if (flag12)
						{
							bool flag13 = this.cvx > num;
							if (flag13)
							{
								this.cvx = num;
							}
							bool flag14 = this.cvx < 0;
							if (flag14)
							{
								this.cvx = num;
							}
						}
						else
						{
							bool flag15 = num < 0;
							if (flag15)
							{
								bool flag16 = this.cvx < num;
								if (flag16)
								{
									this.cvx = num;
								}
								bool flag17 = this.cvx > 0;
								if (flag17)
								{
									this.cvx = num;
								}
							}
							else
							{
								this.cvx = num;
							}
						}
					}
					this.cvy++;
					bool flag18 = this.cvy > 8;
					if (flag18)
					{
						this.cvy = 8;
					}
					bool flag19 = this.skillPaintRandomPaint == null;
					if (flag19)
					{
						this.cy += this.cvy;
					}
					bool flag20 = this.cdir == 1;
					if (flag20)
					{
						bool flag21 = (TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12;
						if (flag21)
						{
							this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
							this.cvx = 0;
						}
					}
					else
					{
						bool flag22 = (TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12;
						if (flag22)
						{
							this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
							this.cvx = 0;
						}
					}
					bool flag23 = this.cvy > 3 && (this.cyStartFall == 0 || this.cyStartFall <= TileMap.tileYofPixel(this.cy + 3)) && (TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2;
					if (flag23)
					{
						bool flag24 = this.me;
						if (flag24)
						{
							this.cyStartFall = 0;
							this.cvx = (this.cvy = 0);
							this.cp1 = (this.cp2 = 0);
							this.cy = TileMap.tileXofPixel(this.cy + 3);
							this.statusMe = 1;
							bool flag25 = this.me;
							if (flag25)
							{
								SoundMn.gI().charFall();
							}
							this.cp3 = 0;
							GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
							GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
							this.addDustEff(1);
							bool flag26 = this.cy - this.cySend > 0;
							if (flag26)
							{
								bool flag27 = this.me;
								if (flag27)
								{
									Service.gI().charMove();
								}
							}
							else
							{
								bool flag28 = (this.cx - this.cxSend != 0 || this.cy - this.cySend < 0) && this.me;
								if (flag28)
								{
									Service.gI().charMove();
								}
							}
						}
						else
						{
							this.stop();
							this.cy = TileMap.tileXofPixel(this.cy + 3);
							this.cf = 0;
							GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
							GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
							this.addDustEff(1);
						}
					}
					else
					{
						this.cf = 12;
						bool flag29 = this.me;
						if (flag29)
						{
							bool flag30 = this.isAttack;
							if (flag30)
							{
							}
						}
						else
						{
							bool flag31 = (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) == 2;
							if (flag31)
							{
								this.cf = 0;
							}
							bool flag32 = this.currentMovePoint != null && this.cy > this.currentMovePoint.yEnd;
							if (flag32)
							{
								this.stop();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
	public void updateCharFly()
	{
		int num = (this.isMonkey != 1 || this.me) ? 1 : 2;
		this.setMountIsStart();
		bool flag = this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0);
		if (flag)
		{
			bool flag2 = this.cy - this.ch < 0;
			if (flag2)
			{
				this.cy = this.ch;
			}
			this.cf = 7;
			this.statusMe = 4;
			this.cvx = 0;
			this.cp2 = 0;
			this.currentMovePoint = null;
		}
		else
		{
			int num2 = this.cy;
			this.cp1++;
			bool flag3 = this.cp1 >= 9;
			if (flag3)
			{
				this.cp1 = 0;
				bool flag4 = !this.me;
				if (flag4)
				{
					this.cvx = (this.cvy = 0);
				}
				this.cBonusSpeed = 0;
			}
			this.cf = 8;
			bool flag5 = Res.abs(this.cvx) <= 4 && this.me;
			if (flag5)
			{
				bool flag6 = this.currentMovePoint != null;
				if (flag6)
				{
					int num3 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
					int num4 = global::Char.abs(this.cy - this.currentMovePoint.yEnd);
					bool flag7 = num3 > num4 * 10;
					if (flag7)
					{
						this.cf = 8;
					}
					else
					{
						bool flag8 = num3 > num4 && num3 > 48 && num4 > 32;
						if (flag8)
						{
							this.cf = 8;
						}
						else
						{
							this.cf = 7;
						}
					}
				}
				else
				{
					bool flag9 = this.cvy < 0;
					if (flag9)
					{
						this.cvy = 0;
					}
					bool flag10 = this.cvy > 16;
					if (flag10)
					{
						this.cvy = 16;
					}
					this.cf = 7;
				}
			}
			bool flag11 = !this.me;
			if (flag11)
			{
				bool flag12 = global::Char.abs(this.cvx) < 2;
				if (flag12)
				{
					this.cvx = (this.cdir << 1) * num;
				}
				bool flag13 = this.cvy != 0;
				if (flag13)
				{
					this.cf = 7;
				}
				bool flag14 = global::Char.abs(this.cvx) <= 2;
				if (flag14)
				{
					this.cp2++;
					bool flag15 = this.cp2 > 32;
					if (flag15)
					{
						this.statusMe = 4;
						this.cvx = 0;
						this.cvy = 0;
					}
				}
			}
			bool flag16 = this.cdir == 1;
			if (flag16)
			{
				bool flag17 = TileMap.tileTypeAt(this.cx + this.chw, this.cy - 1, 4);
				if (flag17)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
					bool flag18 = this.cvy == 0;
					if (flag18)
					{
						this.currentMovePoint = null;
					}
				}
			}
			else
			{
				bool flag19 = TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - 1, 8);
				if (flag19)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
					bool flag20 = this.cvy == 0;
					if (flag20)
					{
						this.currentMovePoint = null;
					}
				}
			}
			this.cx += this.cvx * num;
			this.cy += this.cvy * num;
			bool flag21 = !this.isMount && num2 - this.cy == 0;
			if (flag21)
			{
				this.ty++;
				this.wt++;
				this.fy += (this.wy ? -1 : 1);
				bool flag22 = this.wt == 10;
				if (flag22)
				{
					this.wt = 0;
					this.wy = !this.wy;
				}
				bool flag23 = this.ty > 20;
				if (flag23)
				{
					this.delayFall = 10;
					bool flag24 = GameCanvas.gameTick % 3 == 0;
					if (flag24)
					{
						ServerEffect.addServerEffect(111, this.cx + ((this.cdir != 1) ? 27 : -17), this.cy + this.fy + 13, 1, (this.cdir == 1) ? 0 : 2);
					}
				}
			}
			bool flag25 = this.me;
			if (flag25)
			{
				bool flag26 = this.cvx > 0;
				if (flag26)
				{
					this.cvx--;
				}
				else
				{
					bool flag27 = this.cvx < 0;
					if (flag27)
					{
						this.cvx++;
					}
					else
					{
						bool flag28 = this.cvy == 0;
						if (flag28)
						{
							this.statusMe = 4;
							this.checkDelayFallIfTooHigh();
							Service.gI().charMove();
						}
					}
				}
				bool flag29 = (TileMap.tileTypeAtPixel(this.cx, this.cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(this.cx, this.cy + 40) & 2) == 2;
				if (flag29)
				{
					bool flag30 = this.cvy == 0;
					if (flag30)
					{
						this.delayFall = 0;
					}
					this.cyStartFall = 0;
					this.cvx = (this.cvy = 0);
					this.cp1 = (this.cp2 = 0);
					this.statusMe = 4;
					this.addDustEff(3);
				}
				bool flag31 = global::Char.abs(this.cx - this.cxSend) > 96 || global::Char.abs(this.cy - this.cySend) > 24;
				if (flag31)
				{
					Service.gI().charMove();
				}
			}
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
	public void setMount(int cid, int ctrans, int cgender)
	{
		this.idcharMount = cid;
		this.transMount = ctrans;
		this.genderMount = cgender;
		this.speedMount = 30;
		bool flag = this.transMount < 0;
		if (flag)
		{
			this.transMount = 0;
			this.xMount = GameScr.cmx + GameCanvas.w + 50;
			this.dxMount = -19;
		}
		else
		{
			bool flag2 = this.transMount == 1;
			if (flag2)
			{
				this.transMount = 2;
				this.xMount = GameScr.cmx - 100;
				this.dxMount = -33;
			}
		}
		this.dyMount = -17;
		this.yMount = this.cy;
		this.frameMount = 0;
		this.frameNewMount = 0;
		this.isMount = false;
		this.isEndMount = false;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000F190 File Offset: 0x0000D390
	public void updateMount()
	{
		this.frameMount++;
		bool flag = this.frameMount > this.FrameMount.Length - 1;
		if (flag)
		{
			this.frameMount = 0;
		}
		this.frameNewMount++;
		bool flag2 = this.frameNewMount > 1000;
		if (flag2)
		{
			this.frameNewMount = 0;
		}
		bool flag3 = this.isStartMount && !this.isMount;
		if (flag3)
		{
			this.yMount = this.cy;
			bool flag4 = this.transMount == 0;
			if (flag4)
			{
				bool flag5 = this.xMount - this.cx >= this.speedMount;
				if (flag5)
				{
					this.xMount -= this.speedMount;
				}
				else
				{
					this.xMount = this.cx;
					this.isMount = true;
					this.isEndMount = false;
				}
			}
			else
			{
				bool flag6 = this.transMount == 2;
				if (flag6)
				{
					bool flag7 = this.cx - this.xMount >= this.speedMount;
					if (flag7)
					{
						this.xMount += this.speedMount;
					}
					else
					{
						this.xMount = this.cx;
						this.isMount = true;
						this.isEndMount = false;
					}
				}
			}
		}
		else
		{
			bool flag8 = this.isMount;
			if (flag8)
			{
				bool flag9 = this.statusMe == 14 || this.ySd - this.cy < 24;
				if (flag9)
				{
					this.setMountIsEnd();
				}
				bool flag10 = this.cp1 % 15 < 5;
				if (flag10)
				{
					this.cf = 0;
				}
				else
				{
					this.cf = 1;
				}
				this.transMount = this.cdir;
				this.updateSuperEff();
				bool flag11 = this.transMount < 0;
				if (flag11)
				{
					this.transMount = 0;
					this.dxMount = -19;
				}
				else
				{
					bool flag12 = this.transMount == 1;
					if (flag12)
					{
						this.transMount = 2;
						this.dxMount = -31;
						bool flag13 = this.isEventMount;
						if (flag13)
						{
							this.dxMount = -38;
						}
					}
				}
				bool flag14 = this.skillInfoPaint() != null;
				if (flag14)
				{
					this.dyMount = -15;
				}
				else
				{
					this.dyMount = -17;
				}
				this.yMount = this.cy;
				this.xMount = this.cx;
			}
			else
			{
				bool flag15 = this.isEndMount;
				if (flag15)
				{
					bool flag16 = this.transMount == 0;
					if (flag16)
					{
						bool flag17 = this.xMount > GameScr.cmx - 100;
						if (flag17)
						{
							this.xMount -= 20;
						}
						else
						{
							this.isStartMount = false;
							this.isMount = false;
							this.isEndMount = false;
						}
					}
					else
					{
						bool flag18 = this.transMount == 2;
						if (flag18)
						{
							bool flag19 = this.xMount < GameScr.cmx + GameCanvas.w + 50;
							if (flag19)
							{
								this.xMount += 20;
							}
							else
							{
								this.isStartMount = false;
								this.isMount = false;
								this.isEndMount = false;
							}
						}
					}
				}
				else
				{
					bool flag20 = !this.isStartMount || !this.isMount || !this.isEndMount;
					if (flag20)
					{
						this.xMount = GameScr.cmx - 100;
						this.yMount = GameScr.cmy - 100;
					}
				}
			}
		}
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
	public void getMountData()
	{
		bool flag = Mob.arrMobTemplate[50].data == null;
		if (flag)
		{
			Mob.arrMobTemplate[50].data = new EffectData();
			string text = "/Mob/" + 50.ToString();
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag2 = dataInputStream != null;
			if (flag2)
			{
				Mob.arrMobTemplate[50].data.readData(text + "/data");
				Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(50);
			}
			Mob.lastMob.addElement(50.ToString() + string.Empty);
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
	public void checkFrameTick(int[] array)
	{
		this.t++;
		bool flag = this.t > array.Length - 1;
		if (flag)
		{
			this.t = 0;
		}
		this.fM = array[this.t];
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000F60C File Offset: 0x0000D80C
	public void paintMount1(mGraphics g)
	{
		bool flag = this.xMount > GameScr.cmx && this.xMount < GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			bool flag2 = this.me;
			if (flag2)
			{
				bool flag3 = this.isEndMount || this.isStartMount || this.isMount;
				if (flag3)
				{
					bool flag4 = this.idMount >= global::Char.ID_NEW_MOUNT;
					if (flag4)
					{
						string nameImg = this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_0";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						bool flag5 = fraImage != null;
						if (flag5)
						{
							fraImage.drawFrame(this.frameNewMount / 2 % fraImage.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						}
					}
					else
					{
						bool flag6 = this.isSpeacialMount;
						if (!flag6)
						{
							bool flag7 = this.isEventMount;
							if (flag7)
							{
								g.drawRegion(global::Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							}
							else
							{
								bool flag8 = this.genderMount == 2;
								if (flag8)
								{
									bool flag9 = !this.isMountVip;
									if (flag9)
									{
										g.drawRegion(global::Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
									}
									else
									{
										g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
									}
								}
								else
								{
									bool flag10 = this.genderMount == 1;
									if (flag10)
									{
										bool flag11 = !this.isMountVip;
										if (flag11)
										{
											g.drawRegion(global::Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag12 = !this.me;
				if (flag12)
				{
					bool flag13 = this.idMount >= global::Char.ID_NEW_MOUNT;
					if (flag13)
					{
						string nameImg2 = this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_0";
						FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
						bool flag14 = fraImage2 != null;
						if (flag14)
						{
							fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						}
					}
					else
					{
						bool flag15 = this.isSpeacialMount;
						if (!flag15)
						{
							bool flag16 = this.isEventMount;
							if (flag16)
							{
								g.drawRegion(global::Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							}
							else
							{
								bool flag17 = this.isMount;
								if (flag17)
								{
									bool flag18 = this.genderMount == 2;
									if (flag18)
									{
										bool flag19 = !this.isMountVip;
										if (flag19)
										{
											g.drawRegion(global::Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
									}
									else
									{
										bool flag20 = this.genderMount == 1;
										if (flag20)
										{
											bool flag21 = !this.isMountVip;
											if (flag21)
											{
												g.drawRegion(global::Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
											}
											else
											{
												g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000FB88 File Offset: 0x0000DD88
	public void paintMount2(mGraphics g)
	{
		bool flag = this.xMount > GameScr.cmx && this.xMount < GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			bool flag2 = this.me;
			if (flag2)
			{
				bool flag3 = this.isEndMount || this.isStartMount || this.isMount;
				if (flag3)
				{
					bool flag4 = this.idMount >= global::Char.ID_NEW_MOUNT;
					if (flag4)
					{
						string nameImg = this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_1";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						bool flag5 = fraImage != null;
						if (flag5)
						{
							fraImage.drawFrame(this.frameNewMount / 2 % fraImage.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						}
					}
					else
					{
						bool flag6 = this.isSpeacialMount;
						if (flag6)
						{
							this.checkFrameTick(this.move);
							bool flag7 = Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null;
							if (flag7)
							{
								Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
							}
							else
							{
								this.getMountData();
							}
						}
						else
						{
							bool flag8 = this.isEventMount;
							if (flag8)
							{
								g.drawRegion(global::Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							}
							else
							{
								bool flag9 = this.genderMount == 0;
								if (flag9)
								{
									bool flag10 = !this.isMountVip;
									if (flag10)
									{
										g.drawRegion(global::Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
									}
									else
									{
										g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
									}
								}
								else
								{
									bool flag11 = this.genderMount == 1;
									if (flag11)
									{
										bool flag12 = !this.isMountVip;
										if (flag12)
										{
											g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag13 = !this.me;
				if (flag13)
				{
					bool flag14 = this.idMount >= global::Char.ID_NEW_MOUNT;
					if (flag14)
					{
						string nameImg2 = this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_1";
						FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
						bool flag15 = fraImage2 != null;
						if (flag15)
						{
							fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						}
					}
					else
					{
						bool flag16 = this.isSpeacialMount;
						if (flag16)
						{
							this.checkFrameTick(this.move);
							bool flag17 = Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null;
							if (flag17)
							{
								Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
							}
							else
							{
								this.getMountData();
							}
						}
						else
						{
							bool flag18 = this.isEventMount;
							if (flag18)
							{
								g.drawRegion(global::Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							}
							bool flag19 = this.isMount;
							if (flag19)
							{
								bool flag20 = this.genderMount == 0;
								if (flag20)
								{
									bool flag21 = !this.isMountVip;
									if (flag21)
									{
										g.drawRegion(global::Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
									}
									else
									{
										g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
									}
								}
								else
								{
									bool flag22 = this.genderMount == 1;
									if (flag22)
									{
										bool flag23 = !this.isMountVip;
										if (flag23)
										{
											g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0001020C File Offset: 0x0000E40C
	public void setMountIsStart()
	{
		bool flag = this.me;
		if (flag)
		{
			this.isHaveMount = this.checkHaveMount();
			bool flag2 = TileMap.isVoDaiMap();
			if (flag2)
			{
				this.isHaveMount = false;
			}
		}
		bool flag3 = this.isHaveMount;
		if (flag3)
		{
			bool flag4 = this.ySd - this.cy <= 20;
			if (flag4)
			{
				this.xChar = this.cx;
			}
			bool flag5 = this.xdis < 100;
			if (flag5)
			{
				this.xdis = Res.abs(this.xChar - this.cx);
			}
			bool flag6 = this.xdis >= 70 && this.ySd - this.cy > 30 && !this.isStartMount && !this.isEndMount;
			if (flag6)
			{
				this.setMount(this.charID, this.cdir, this.cgender);
				this.isStartMount = true;
			}
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000102FC File Offset: 0x0000E4FC
	public void setMountIsEnd()
	{
		bool flag = this.ySd - this.cy < 24 && !this.isEndMount;
		if (flag)
		{
			this.isStartMount = false;
			this.isMount = false;
			this.isEndMount = true;
			this.xdis = 0;
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0001034C File Offset: 0x0000E54C
	public bool checkHaveMount()
	{
		bool result = false;
		short num = -1;
		Item[] array = this.arrItemBody;
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = array[i] != null && (array[i].template.type == 24 || array[i].template.type == 23);
			if (flag)
			{
				bool flag2 = array[i].template.part >= 0;
				if (flag2)
				{
					num = (short)(global::Char.ID_NEW_MOUNT + array[i].template.part);
				}
				else
				{
					num = array[i].template.id;
				}
				result = true;
				break;
			}
		}
		this.isMountVip = false;
		this.isSpeacialMount = false;
		this.isEventMount = false;
		this.idMount = -1;
		bool flag3 = num == 349 || num == 350 || num == 351;
		if (flag3)
		{
			this.isMountVip = true;
		}
		else
		{
			bool flag4 = num == 396;
			if (flag4)
			{
				this.isEventMount = true;
			}
			else
			{
				bool flag5 = num == 532;
				if (flag5)
				{
					this.isSpeacialMount = true;
				}
				else
				{
					bool flag6 = num >= global::Char.ID_NEW_MOUNT;
					if (flag6)
					{
						this.idMount = num;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00010498 File Offset: 0x0000E698
	private void checkDelayFallIfTooHigh()
	{
		bool flag = true;
		for (int i = 0; i < 150; i += 24)
		{
			bool flag2 = (TileMap.tileTypeAtPixel(this.cx, this.cy + i) & 2) == 2 || this.cy + i > TileMap.tmh * (int)TileMap.size - 24;
			if (flag2)
			{
				flag = false;
				break;
			}
		}
		bool flag3 = flag;
		if (flag3)
		{
			this.delayFall = 40;
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0001050C File Offset: 0x0000E70C
	public void setDefaultPart()
	{
		this.setDefaultWeapon();
		this.setDefaultBody();
		this.setDefaultLeg();
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00010524 File Offset: 0x0000E724
	public void setDefaultWeapon()
	{
		bool flag = this.cgender == 0;
		if (flag)
		{
			this.wp = 0;
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00010548 File Offset: 0x0000E748
	public void setDefaultBody()
	{
		bool flag = this.cgender == 0;
		if (flag)
		{
			this.body = 57;
		}
		else
		{
			bool flag2 = this.cgender == 1;
			if (flag2)
			{
				this.body = 59;
			}
			else
			{
				bool flag3 = this.cgender == 2;
				if (flag3)
				{
					this.body = 57;
				}
			}
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000105A0 File Offset: 0x0000E7A0
	public void setDefaultLeg()
	{
		bool flag = this.cgender == 0;
		if (flag)
		{
			this.leg = 58;
		}
		else
		{
			bool flag2 = this.cgender == 1;
			if (flag2)
			{
				this.leg = 60;
			}
			else
			{
				bool flag3 = this.cgender == 2;
				if (flag3)
				{
					this.leg = 58;
				}
			}
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x000105F8 File Offset: 0x0000E7F8
	public bool isSelectingSkillUseAlone()
	{
		return this.myskill != null && this.myskill.template.isUseAlone();
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00010628 File Offset: 0x0000E828
	public bool isUseSkillSpec()
	{
		return this.myskill != null && this.myskill.template.isSkillSpec();
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00010658 File Offset: 0x0000E858
	public bool isSelectingSkillBuffToPlayer()
	{
		return this.myskill != null && this.myskill.template.isBuffToPlayer();
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00010688 File Offset: 0x0000E888
	public bool isUseChargeSkill()
	{
		return !this.isUseSkillAfterCharge && this.myskill != null && (this.myskill.template.id == 10 || this.myskill.template.id == 11);
	}

	// Token: 0x060000DC RID: 220 RVA: 0x000106D8 File Offset: 0x0000E8D8
	public void setSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.hasSendAttack = false;
		bool flag = this.stone;
		if (!flag)
		{
			bool flag2 = this.me && this.myskill.template.id == 9 && this.cHP <= this.cHPFull / 10;
			if (!flag2)
			{
				bool flag3 = this.me;
				if (flag3)
				{
					bool flag4 = this.mobFocus == null && this.charFocus == null;
					if (flag4)
					{
						this.stopUseChargeSkill();
					}
					bool flag5 = this.mobFocus != null && (this.mobFocus.status == 1 || this.mobFocus.status == 0);
					if (flag5)
					{
						this.stopUseChargeSkill();
					}
					bool flag6 = this.charFocus != null && (this.charFocus.statusMe == 14 || this.charFocus.statusMe == 5);
					if (flag6)
					{
						this.stopUseChargeSkill();
					}
					bool flag7 = this.myskill.template.id == 23;
					if (flag7)
					{
						bool flag8 = this.charFocus != null && this.charFocus.holdEffID != 0;
						if (flag8)
						{
							return;
						}
						bool flag9 = this.mobFocus != null && this.mobFocus.holdEffID != 0;
						if (flag9)
						{
							return;
						}
						bool flag10 = this.holdEffID != 0;
						if (flag10)
						{
							return;
						}
					}
					bool flag11 = this.sleepEff || this.blindEff;
					if (flag11)
					{
						return;
					}
				}
				Res.outz("skill id= " + skillPaint.id.ToString());
				bool flag12 = this.me && this.dart != null;
				if (!flag12)
				{
					bool flag13 = TileMap.isOfflineMap();
					if (!flag13)
					{
						long num = mSystem.currentTimeMillis();
						bool flag14 = this.me;
						if (flag14)
						{
							bool flag15 = this.isSelectingSkillBuffToPlayer() && this.charFocus == null;
							if (flag15)
							{
								return;
							}
							bool flag16 = num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown;
							if (flag16)
							{
								this.myskill.paintCanNotUseSkill = true;
								return;
							}
							this.myskill.lastTimeUseThisSkill = num;
							bool flag17 = this.myskill.template.manaUseType == 2;
							if (flag17)
							{
								this.cMP = 1;
							}
							else
							{
								bool flag18 = this.myskill.template.manaUseType != 1;
								if (flag18)
								{
									this.cMP -= this.myskill.manaUse;
								}
								else
								{
									this.cMP -= this.myskill.manaUse * this.cMPFull / 100;
								}
							}
							global::Char.myCharz().cStamina--;
							GameScr.gI().isInjureMp = true;
							GameScr.gI().twMp = 0;
							bool flag19 = this.cMP < 0;
							if (flag19)
							{
								this.cMP = 0;
							}
						}
						bool flag20 = this.me;
						if (flag20)
						{
							bool flag21 = this.myskill.template.id == 7;
							if (flag21)
							{
								SoundMn.gI().hoisinh();
							}
							bool flag22 = this.myskill.template.id == 6;
							if (flag22)
							{
								Service.gI().skill_not_focus(0);
								GameScr.gI().isUseFreez = true;
								SoundMn.gI().thaiduonghasan();
							}
							bool flag23 = this.myskill.template.id == 8;
							if (flag23)
							{
								bool flag24 = !this.isCharge;
								if (flag24)
								{
									SoundMn.gI().taitaoPause();
									Service.gI().skill_not_focus(1);
									this.isCharge = true;
									this.last = (this.cur = mSystem.currentTimeMillis());
								}
								else
								{
									Service.gI().skill_not_focus(3);
									this.isCharge = false;
									SoundMn.gI().taitaoPause();
								}
							}
							bool flag25 = this.myskill.template.id == 13;
							if (flag25)
							{
								bool flag26 = this.isMonkey != 0;
								if (flag26)
								{
									GameScr.gI().auto = 0;
									return;
								}
								bool flag27 = this.isCreateDark;
								if (flag27)
								{
									return;
								}
								SoundMn.gI().gong();
								Service.gI().skill_not_focus(6);
								this.chargeCount = 0;
								this.isWaitMonkey = true;
								return;
							}
							else
							{
								bool flag28 = this.myskill.template.id == 14;
								if (flag28)
								{
									SoundMn.gI().gong();
									Service.gI().skill_not_focus(7);
									this.useChargeSkill(true);
								}
								bool flag29 = this.myskill.template.id == 21;
								if (flag29)
								{
									Service.gI().skill_not_focus(10);
									return;
								}
								bool flag30 = this.myskill.template.id == 12;
								if (flag30)
								{
									Service.gI().skill_not_focus(8);
								}
								bool flag31 = this.myskill.template.id == 19;
								if (flag31)
								{
									Service.gI().skill_not_focus(9);
									return;
								}
							}
						}
						bool flag32 = this.isMonkey == 1 && skillPaint.id >= 35 && skillPaint.id <= 41;
						if (flag32)
						{
							skillPaint = GameScr.sks[106];
						}
						bool flag33 = skillPaint.id >= 128 && skillPaint.id <= 134;
						if (flag33)
						{
							skillPaint = GameScr.sks[skillPaint.id - 65];
							bool flag34 = this.charFocus != null;
							if (flag34)
							{
								this.cx = this.charFocus.cx;
								this.cy = this.charFocus.cy;
								this.currentMovePoint = null;
							}
							bool flag35 = this.mobFocus != null;
							if (flag35)
							{
								this.cx = this.mobFocus.x;
								this.cy = this.mobFocus.y;
								this.currentMovePoint = null;
							}
							ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
							this.telePortSkill = true;
						}
						bool flag36 = skillPaint.id >= 107 && skillPaint.id <= 113;
						if (flag36)
						{
							skillPaint = GameScr.sks[skillPaint.id - 44];
							EffecMn.addEff(new Effect(23, this.cx, this.cy + this.ch / 2, 3, 2, 1));
						}
						this.setAutoSkillPaint(skillPaint, sType);
					}
				}
			}
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00010D88 File Offset: 0x0000EF88
	public void useSkillNotFocus()
	{
		GameScr.gI().auto = 0;
		global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2)) ? 1 : 0);
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00010DE4 File Offset: 0x0000EFE4
	public void sendUseChargeSkill()
	{
		bool flag = this.me && (this.isFreez || this.isUsePlane);
		if (flag)
		{
			GameScr.gI().auto = 0;
		}
		else
		{
			long num = mSystem.currentTimeMillis();
			bool flag2 = this.me && num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown;
			if (flag2)
			{
				this.myskill.paintCanNotUseSkill = true;
			}
			else
			{
				bool flag3 = this.myskill.template.id == 10;
				if (flag3)
				{
					this.useChargeSkill(false);
				}
				bool flag4 = this.myskill.template.id == 11;
				if (flag4)
				{
					this.useChargeSkill(true);
				}
			}
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00010EA8 File Offset: 0x0000F0A8
	public void stopUseChargeSkill()
	{
		this.isFlyAndCharge = false;
		this.isStandAndCharge = false;
		this.isUseSkillAfterCharge = false;
		this.isCreateDark = false;
		bool flag = this.me && this.statusMe != 14 && this.statusMe != 5;
		if (flag)
		{
			this.isLockMove = false;
		}
		GameScr.gI().auto = 0;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00010F0C File Offset: 0x0000F10C
	public void useChargeSkill(bool isGround)
	{
		bool flag = this.isCreateDark;
		if (!flag)
		{
			GameScr.gI().auto = 0;
			if (isGround)
			{
				bool flag2 = !this.isStandAndCharge;
				if (flag2)
				{
					this.chargeCount = 0;
					this.seconds = 50000;
					this.posDisY = 0;
					this.last = mSystem.currentTimeMillis();
					bool flag3 = this.me;
					if (flag3)
					{
						this.isLockMove = true;
						bool flag4 = this.cgender == 1;
						if (flag4)
						{
							Service.gI().skill_not_focus(4);
						}
					}
					bool flag5 = this.cgender == 1;
					if (flag5)
					{
						SoundMn.gI().gongName();
					}
					this.isStandAndCharge = true;
				}
			}
			else
			{
				bool flag6 = !this.isFlyAndCharge;
				if (flag6)
				{
					bool flag7 = this.me;
					if (flag7)
					{
						GameScr.gI().auto = 0;
						this.isLockMove = true;
						Service.gI().skill_not_focus(4);
					}
					this.isUseSkillAfterCharge = false;
					this.chargeCount = 0;
					this.isFlyAndCharge = true;
					this.posDisY = 0;
					this.seconds = 50000;
					this.isFlying = TileMap.tileTypeAt(this.cx, this.cy, 2);
				}
			}
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00011044 File Offset: 0x0000F244
	public void setAutoSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.skillPaint = skillPaint;
		Res.outz("set auto skill " + ((skillPaint == null) ? "null" : "ko null"));
		bool flag = skillPaint.id >= 0 && skillPaint.id <= 6;
		if (flag)
		{
			int num = Res.random(0, skillPaint.id + 4) - 1;
			bool flag2 = num < 0;
			if (flag2)
			{
				num = 0;
			}
			bool flag3 = num > 6;
			if (flag3)
			{
				num = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num];
		}
		else
		{
			bool flag4 = skillPaint.id >= 14 && skillPaint.id <= 20;
			if (flag4)
			{
				int num2 = Res.random(0, skillPaint.id - 14 + 4) - 1;
				bool flag5 = num2 < 0;
				if (flag5)
				{
					num2 = 0;
				}
				bool flag6 = num2 > 6;
				if (flag6)
				{
					num2 = 6;
				}
				this.skillPaintRandomPaint = GameScr.sks[num2 + 14];
			}
			else
			{
				bool flag7 = skillPaint.id >= 28 && skillPaint.id <= 34;
				if (flag7)
				{
					int num3 = Res.random(0, ((this.isMonkey != 1) ? skillPaint.id : 105) - ((this.isMonkey != 1) ? 28 : 105) + 4) - 1;
					bool flag8 = num3 < 0;
					if (flag8)
					{
						num3 = 0;
					}
					bool flag9 = num3 > 6;
					if (flag9)
					{
						num3 = 6;
					}
					bool flag10 = this.isMonkey == 1;
					if (flag10)
					{
						num3 = 0;
					}
					this.skillPaintRandomPaint = GameScr.sks[num3 + ((this.isMonkey != 1) ? 28 : 105)];
				}
				else
				{
					bool flag11 = skillPaint.id >= 63 && skillPaint.id <= 69;
					if (flag11)
					{
						int num4 = Res.random(0, skillPaint.id - 63 + 4) - 1;
						bool flag12 = num4 < 0;
						if (flag12)
						{
							num4 = 0;
						}
						bool flag13 = num4 > 6;
						if (flag13)
						{
							num4 = 6;
						}
						this.skillPaintRandomPaint = GameScr.sks[num4 + 63];
					}
					else
					{
						bool flag14 = skillPaint.id >= 107 && skillPaint.id <= 109;
						if (flag14)
						{
							int num5 = Res.random(0, skillPaint.id - 107 + 4) - 1;
							bool flag15 = num5 < 0;
							if (flag15)
							{
								num5 = 0;
							}
							bool flag16 = num5 > 6;
							if (flag16)
							{
								num5 = 6;
							}
							this.skillPaintRandomPaint = GameScr.sks[num5 + 107];
						}
						else
						{
							this.skillPaintRandomPaint = skillPaint;
						}
					}
				}
			}
		}
		this.sType = sType;
		this.indexSkill = 0;
		this.i0 = (this.i1 = (this.i2 = (this.dx0 = (this.dx1 = (this.dx2 = (this.dy0 = (this.dy1 = (this.dy2 = 0))))))));
		this.eff0 = null;
		this.eff1 = null;
		this.eff2 = null;
		this.cvy = 0;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00011350 File Offset: 0x0000F550
	public SkillInfoPaint[] skillInfoPaint()
	{
		bool flag = this.skillPaint == null;
		SkillInfoPaint[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.skillPaintRandomPaint == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				bool flag3 = this.sType == 0;
				if (flag3)
				{
					result = this.skillPaintRandomPaint.skillStand;
				}
				else
				{
					result = this.skillPaintRandomPaint.skillfly;
				}
			}
		}
		return result;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x000113B0 File Offset: 0x0000F5B0
	public void setAttack()
	{
		bool flag = this.me;
		if (flag)
		{
			SkillPaint skillPaint = this.skillPaintRandomPaint;
			bool flag2 = this.dart != null;
			if (flag2)
			{
				skillPaint = this.dart.skillPaint;
			}
			bool flag3 = skillPaint != null;
			if (flag3)
			{
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				bool flag4 = this.charFocus != null;
				if (flag4)
				{
					myVector2.addElement(this.charFocus);
				}
				else
				{
					bool flag5 = this.mobFocus != null;
					if (flag5)
					{
						myVector.addElement(this.mobFocus);
					}
				}
				this.effPaints = new EffectPaint[myVector.size() + myVector2.size()];
				for (int i = 0; i < myVector.size(); i++)
				{
					this.effPaints[i] = new EffectPaint();
					this.effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					bool flag6 = !this.isSelectingSkillUseAlone();
					if (flag6)
					{
						this.effPaints[i].eMob = (Mob)myVector.elementAt(i);
					}
				}
				for (int j = 0; j < myVector2.size(); j++)
				{
					this.effPaints[j + myVector.size()] = new EffectPaint();
					this.effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					this.effPaints[j + myVector.size()].eChar = (global::Char)myVector2.elementAt(j);
				}
				int type = 0;
				bool flag7 = this.mobFocus != null;
				if (flag7)
				{
					type = 1;
				}
				else
				{
					bool flag8 = this.charFocus != null;
					if (flag8)
					{
						type = 2;
					}
				}
				bool flag9 = myVector.size() == 0 && myVector2.size() == 0;
				if (flag9)
				{
					this.stopUseChargeSkill();
				}
				bool flag10 = this.me && !this.isSelectingSkillUseAlone() && !this.hasSendAttack;
				if (flag10)
				{
					Service.gI().sendPlayerAttack(myVector, myVector2, type);
					this.hasSendAttack = true;
				}
			}
		}
		else
		{
			SkillPaint skillPaint2 = this.skillPaintRandomPaint;
			bool flag11 = this.dart != null;
			if (flag11)
			{
				skillPaint2 = this.dart.skillPaint;
			}
			bool flag12 = skillPaint2 != null;
			if (flag12)
			{
				bool flag13 = this.attMobs != null;
				if (flag13)
				{
					this.effPaints = new EffectPaint[this.attMobs.Length];
					for (int k = 0; k < this.attMobs.Length; k++)
					{
						this.effPaints[k] = new EffectPaint();
						this.effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[k].eMob = this.attMobs[k];
					}
					this.attMobs = null;
				}
				else
				{
					bool flag14 = this.attChars != null;
					if (flag14)
					{
						this.effPaints = new EffectPaint[this.attChars.Length];
						for (int l = 0; l < this.attChars.Length; l++)
						{
							this.effPaints[l] = new EffectPaint();
							this.effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
							this.effPaints[l].eChar = this.attChars[l];
						}
						this.attChars = null;
					}
				}
			}
		}
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00011740 File Offset: 0x0000F940
	public bool isOutX()
	{
		return this.cx < GameScr.cmx || this.cx > GameScr.cmx + GameScr.gW;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00011778 File Offset: 0x0000F978
	public bool isPaint()
	{
		return this.cy >= GameScr.cmy && this.cy <= GameScr.cmy + GameScr.gH + 30 && !this.isOutX() && !this.isSetPos && !this.isFusion;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x000117C9 File Offset: 0x0000F9C9
	public void createShadow(int x, int y, int life)
	{
		this.shadowX = x;
		this.shadowY = y;
		this.shadowLife = life;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x000117E1 File Offset: 0x0000F9E1
	public void setMabuHold(bool m)
	{
		this.isMabuHold = m;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x000117EC File Offset: 0x0000F9EC
	public virtual void paint(mGraphics g)
	{
		bool flag = this.isHide;
		if (!flag)
		{
			bool flag2 = this.isMafuba;
			if (flag2)
			{
				this.paintCharWithoutSkill(g);
			}
			else
			{
				bool flag3 = this.isMabuHold;
				if (flag3)
				{
					bool flag4 = this.cmtoChar;
					if (flag4)
					{
						GameScr.cmtoX = this.cx - GameScr.gW2;
						GameScr.cmtoY = this.cy - GameScr.gH23;
						bool flag5 = !GameCanvas.isTouchControl;
						if (flag5)
						{
							GameScr.cmtoX += GameScr.gW6 * this.cdir;
						}
					}
				}
				else
				{
					bool flag6 = !this.isPaint();
					if (!flag6)
					{
						bool flag7 = !this.me && GameScr.notPaint;
						if (!flag7)
						{
							bool flag8 = this.petFollow != null;
							if (flag8)
							{
								this.petFollow.paint(g);
							}
							this.paintMount1(g);
							bool flag9 = TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48;
							if (!flag9)
							{
								bool flag10 = this.isTeleport;
								if (!flag10)
								{
									bool flag11 = this.holder && GameCanvas.gameTick % 2 == 0;
									if (flag11)
									{
										g.setColor(16185600);
										bool flag12 = this.charHold != null;
										if (flag12)
										{
											g.drawLine(this.cx, this.cy - this.ch / 2, this.charHold.cx, this.charHold.cy - this.charHold.ch / 2);
										}
										bool flag13 = this.mobHold != null;
										if (flag13)
										{
											g.drawLine(this.cx, this.cy - this.ch / 2, this.mobHold.x, this.mobHold.y - this.mobHold.h / 2);
										}
									}
									this.paintSuperEffBehind(g);
									this.paintAuraBehind(g);
									this.paintEffBehind(g);
									this.paintEff_Lvup_behind(g);
									this.paintEff_Pet(g);
									bool flag14 = this.shadowLife > 0;
									if (flag14)
									{
										bool flag15 = GameCanvas.gameTick % 2 == 0;
										if (flag15)
										{
											this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 25, true);
										}
										else
										{
											bool flag16 = this.shadowLife > 5;
											if (flag16)
											{
												this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 7, true);
											}
										}
									}
									bool flag17 = !this.isPaint() && this.skillPaint != null && (this.skillPaint.id < 70 || this.skillPaint.id > 76) && (this.skillPaint.id < 77 || this.skillPaint.id > 83);
									if (flag17)
									{
										bool flag18 = this.skillPaint != null;
										if (flag18)
										{
											this.indexSkill = this.skillInfoPaint().Length;
											this.skillPaint = null;
										}
										this.effPaints = null;
										this.eff = null;
										this.effTask = null;
										this.indexEff = -1;
										this.indexEffTask = -1;
									}
									else
									{
										bool flag19 = this.statusMe == 15 || (this.moveFast != null && this.moveFast[0] > 0);
										if (!flag19)
										{
											this.paintCharName_HP_MP_Overhead(g);
											bool flag20 = this.skillPaint == null || this.skillInfoPaint() == null || this.indexSkill >= this.skillInfoPaint().Length;
											if (flag20)
											{
												this.paintCharWithoutSkill(g);
											}
											bool flag21 = this.arr != null;
											if (flag21)
											{
												this.arr.paint(g);
											}
											bool flag22 = this.dart != null;
											if (flag22)
											{
												this.dart.paint(g);
											}
											this.paintEffect(g);
											bool flag23 = this.mobMe != null;
											if (flag23)
											{
											}
											this.paintMount2(g);
											this.paintEff_Lvup_front(g);
											this.paintSuperEffFront(g);
											this.paintAuraFront(g);
											this.paintEffFront(g);
											this.paint_map_line(g);
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00011C18 File Offset: 0x0000FE18
	private void paint_map_line(mGraphics g)
	{
		bool flag = this.isPaintNewSkill;
		if (!flag)
		{
			bool flag2 = this.x_hint != 0 && this.y_hint != 0 && this.statusMe != 14;
			if (flag2)
			{
				int arg = 0;
				int x = this.cx - 30;
				int y = this.cy - 15;
				int num = -30;
				int num2 = 5;
				bool flag3 = Res.abs(this.cy - (int)this.y_hint) > 150;
				if (flag3)
				{
					bool flag4 = this.cy > (int)this.y_hint;
					if (flag4)
					{
						arg = 7;
						x = this.cx;
						y = this.cy - 15 - 60;
					}
					else
					{
						arg = 5;
						x = this.cx;
						y = this.cy - 15 + 60;
					}
				}
				else
				{
					bool flag5 = this.cx > (int)this.x_hint;
					if (flag5)
					{
						arg = 2;
					}
					else
					{
						bool flag6 = this.cx <= (int)this.x_hint;
						if (flag6)
						{
							x = this.cx + 30;
						}
					}
				}
				bool flag7 = GameCanvas.gameTick % 10 < 5;
				if (!flag7)
				{
					bool flag8 = Res.abs(this.cx - (int)this.x_hint) > 100;
					if (flag8)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
					}
					else
					{
						g.drawImage(Panel.imgBantay, (int)this.x_hint + num, (int)(this.y_hint - 60) + num2, 0);
					}
				}
			}
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00011D98 File Offset: 0x0000FF98
	private void paintEff_Pet(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			bool flag = effect.effId >= 201;
			if (flag)
			{
				effect.paint(g);
			}
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00011DF4 File Offset: 0x0000FFF4
	private void paintSuperEffBehind(mGraphics g)
	{
		bool flag = this.me;
		if (flag)
		{
			bool flag2 = !global::Char.isPaintAura && this.idAuraEff > -1;
			if (flag2)
			{
				return;
			}
		}
		else
		{
			bool flag3 = this.idAuraEff > -1;
			if (flag3)
			{
				return;
			}
		}
		bool flag4 = !global::Char.isPaintAura2;
		if (!flag4)
		{
			bool flag5 = (this.statusMe == 1 || this.statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - this.timeBlue > 0L && !this.isCopy && this.clevel >= 16;
			if (flag5)
			{
				int num = 7598;
				int num2 = 4;
				bool flag6 = this.clevel >= 19;
				if (flag6)
				{
					num = 7676;
				}
				bool flag7 = this.clevel >= 22;
				if (flag7)
				{
					num = 7677;
				}
				bool flag8 = this.clevel >= 25;
				if (flag8)
				{
					num = 7678;
				}
				bool flag9 = num != -1;
				if (flag9)
				{
					Small small = SmallImage.imgNew[num];
					bool flag10 = small == null;
					if (flag10)
					{
						SmallImage.createImage(num);
					}
					else
					{
						int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
						g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00011F9C File Offset: 0x0001019C
	private void paintSuperEffFront(mGraphics g)
	{
		bool flag2 = this.me;
		if (flag2)
		{
			bool flag3 = !global::Char.isPaintAura && this.idAuraEff > -1;
			if (flag3)
			{
				return;
			}
		}
		else
		{
			bool flag4 = this.idAuraEff > -1;
			if (flag4)
			{
				return;
			}
		}
		bool flag5 = !global::Char.isPaintAura2;
		if (!flag5)
		{
			bool flag6 = this.statusMe == 1 || this.statusMe == 6;
			if (flag6)
			{
				bool flag7 = !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - this.timeBlue > 0L;
				if (flag7)
				{
					bool flag8 = this.isCopy;
					if (flag8)
					{
						bool flag9 = GameCanvas.gameTick % 2 == 0;
						if (flag9)
						{
							this.tBlue++;
						}
						bool flag10 = this.tBlue > 6;
						if (flag10)
						{
							this.tBlue = 0;
						}
						g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						bool flag11 = this.clevel >= 14 && !GameCanvas.lowGraphic;
						if (flag11)
						{
							bool flag = false;
							bool flag12 = mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1;
							if (flag12)
							{
								flag = true;
								this.IsAddDust1 = false;
							}
							bool flag13 = mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2;
							if (flag13)
							{
								flag = true;
								this.IsAddDust2 = false;
							}
							bool flag14 = flag;
							if (flag14)
							{
								GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
								GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
								this.addDustEff(1);
							}
						}
						bool flag15 = this.clevel == 14;
						if (flag15)
						{
							bool flag16 = GameCanvas.gameTick % 2 == 0;
							if (flag16)
							{
								this.tBlue++;
							}
							bool flag17 = this.tBlue > 6;
							if (flag17)
							{
								this.tBlue = 0;
							}
							g.drawImage(GameCanvas.imgBlue[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						else
						{
							bool flag18 = this.clevel == 15;
							if (flag18)
							{
								bool flag19 = GameCanvas.gameTick % 2 == 0;
								if (flag19)
								{
									this.tBlue++;
								}
								bool flag20 = this.tBlue > 6;
								if (flag20)
								{
									this.tBlue = 0;
								}
								g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
							}
							else
							{
								bool flag21 = this.clevel >= 16;
								if (flag21)
								{
									int num = -1;
									int num2 = 4;
									bool flag22 = this.clevel >= 16 && this.clevel < 22;
									if (flag22)
									{
										num = 7599;
										num2 = 4;
									}
									bool flag23 = num != -1;
									if (flag23)
									{
										Small small = SmallImage.imgNew[num];
										bool flag24 = small == null;
										if (flag24)
										{
											SmallImage.createImage(num);
										}
										else
										{
											int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
											g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				this.timeBlue = mSystem.currentTimeMillis() + 1500L;
				this.IsAddDust1 = true;
				this.IsAddDust2 = true;
			}
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00012384 File Offset: 0x00010584
	private void paintEffect(mGraphics g)
	{
		bool flag = this.effPaints != null;
		if (flag)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				bool flag2 = this.effPaints[i] != null;
				if (flag2)
				{
					bool flag3 = this.effPaints[i].eMob != null;
					if (flag3)
					{
						int y = this.effPaints[i].eMob.y;
						bool flag4 = this.effPaints[i].eMob is BigBoss;
						if (flag4)
						{
							y = this.effPaints[i].eMob.y - 60;
						}
						bool flag5 = this.effPaints[i].eMob is BigBoss2;
						if (flag5)
						{
							y = this.effPaints[i].eMob.y - 50;
						}
						bool flag6 = this.effPaints[i].eMob is BachTuoc;
						if (flag6)
						{
							y = this.effPaints[i].eMob.y - 40;
						}
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						bool flag7 = this.effPaints[i].eChar != null;
						if (flag7)
						{
							SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
					}
				}
			}
		}
		bool flag8 = this.indexEff >= 0 && this.eff != null;
		if (flag8)
		{
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.indexEff].idImg, this.cx + this.eff.arrEfInfo[this.indexEff].dx, this.cy + this.eff.arrEfInfo[this.indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		bool flag9 = this.indexEffTask >= 0 && this.effTask != null;
		if (flag9)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00003136 File Offset: 0x00001336
	private void paintArrowAttack(mGraphics g)
	{
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0001263C File Offset: 0x0001083C
	public void paintHp(mGraphics g, int x, int y)
	{
		int num = this.cHP * 100 / this.cHPFull / 10 - 1;
		bool flag = num < 0;
		if (flag)
		{
			num = 0;
		}
		bool flag2 = num > 9;
		if (flag2)
		{
			num = 9;
		}
		bool flag3 = !this.me;
		if (flag3)
		{
			g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y, 3);
		}
		bool flag4 = this.cTypePk != 0 || (global::Char.myCharz().cFlag != 0 && this.cFlag != 0 && (this.cFlag == 8 || global::Char.myCharz().cFlag == 8 || this.cFlag != global::Char.myCharz().cFlag));
		if (flag4)
		{
			this.len = (int)((long)this.cHP * 100L / (long)this.cHPFull * (long)this.w_hp_bar) / 100;
			num = (int)((long)this.cHP * 100L / (long)this.cHPFull);
			bool flag5 = num < 30;
			if (flag5)
			{
				this.imgHPtem = GameScr.imgHP_tm_do;
			}
			else
			{
				bool flag6 = num < 60;
				if (flag6)
				{
					this.imgHPtem = GameScr.imgHP_tm_vang;
				}
				else
				{
					this.imgHPtem = GameScr.imgHP_tm_xanh;
				}
			}
			int imageWidth = mGraphics.getImageWidth(GameScr.imgHP_tm_do);
			int imageHeight = mGraphics.getImageHeight(GameScr.imgHP_tm_do);
			int w = imageWidth * num / 100;
			g.drawImage(GameScr.imgHP_tm_xam, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
			bool flag7 = this.len < 5;
			if (flag7)
			{
				bool flag8 = GameCanvas.gameTick % 6 < 3;
				if (flag8)
				{
					g.drawRegion(this.imgHPtem, 0, 0, w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
				}
			}
			else
			{
				g.drawRegion(this.imgHPtem, 0, 0, w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
			}
		}
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00012834 File Offset: 0x00010A34
	public int getClassColor()
	{
		int result = 9145227;
		bool flag = this.nClass.classId == 1 || this.nClass.classId == 2;
		if (flag)
		{
			result = 16711680;
		}
		else
		{
			bool flag2 = this.nClass.classId == 3 || this.nClass.classId == 4;
			if (flag2)
			{
				result = 33023;
			}
			else
			{
				bool flag3 = this.nClass.classId == 5 || this.nClass.classId == 6;
				if (flag3)
				{
					result = 7443811;
				}
			}
		}
		return result;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x000128D4 File Offset: 0x00010AD4
	public void paintNameInSameParty(mGraphics g)
	{
		bool flag = this.cTypePk == 3 || this.cTypePk == 5;
		if (!flag)
		{
			bool flag2 = this.isPaint();
			if (flag2)
			{
				bool flag3 = global::Char.myCharz().charFocus == null || !global::Char.myCharz().charFocus.Equals(this);
				if (flag3)
				{
					mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
				}
				else
				{
					bool flag4 = global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this);
					if (flag4)
					{
						mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 10, mFont.CENTER, mFont.tahoma_7_grey);
					}
				}
			}
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x000129D8 File Offset: 0x00010BD8
	private void paintCharName_HP_MP_Overhead(mGraphics g)
	{
		Part part = GameScr.parts[this.getFHead(this.head)];
		int num = global::Char.CharInfo[this.cf][0][2] - (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + 5;
		bool flag4 = this.isInvisiblez && !this.me;
		if (!flag4)
		{
			bool flag5 = !this.me && TileMap.mapID == 113 && this.cy >= 360;
			if (!flag5)
			{
				bool flag6 = this.me;
				if (flag6)
				{
					num += 5;
					this.paintHp(g, this.cx, this.cy - num + 3);
					bool flag7 = this.fraDanhHieu != null;
					if (flag7)
					{
						int x = this.cx - this.fraDanhHieu.frameWidth / 2;
						int y = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
						bool flag8 = GameCanvas.gameTick % 5 == 0;
						if (flag8)
						{
							this.danhHieuFramme++;
						}
						bool flag9 = this.danhHieuFramme >= this.fraDanhHieu.nFrame;
						if (flag9)
						{
							this.danhHieuFramme = 0;
						}
						this.fraDanhHieu.drawFrame(this.danhHieuFramme, x, y, 0, mGraphics.TOP | mGraphics.LEFT, g);
					}
				}
				else
				{
					bool flag = global::Char.myChar.clan != null && this.clanID == global::Char.myChar.clan.ID;
					bool flag2 = this.cTypePk == 3 || this.cTypePk == 5;
					bool flag3 = this.cTypePk == 4;
					bool flag10 = this.cName.StartsWith("$");
					if (flag10)
					{
						this.cName = this.cName.Substring(1);
						this.isPet = true;
					}
					bool flag11 = this.cName.StartsWith("#");
					if (flag11)
					{
						this.cName = this.cName.Substring(1);
						this.isMiniPet = true;
					}
					mFont mFont = mFont.tahoma_7_whiteSmall;
					bool flag12 = global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this);
					if (flag12)
					{
						num += 5;
						this.paintHp(g, this.cx, this.cy - num + 3);
						bool flag13 = this.fraDanhHieu != null;
						if (flag13)
						{
							int x2 = this.cx - this.fraDanhHieu.frameWidth / 2;
							int y2 = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
							bool flag14 = GameCanvas.gameTick % 5 == 0;
							if (flag14)
							{
								this.danhHieuFramme++;
							}
							bool flag15 = this.danhHieuFramme >= this.fraDanhHieu.nFrame;
							if (flag15)
							{
								this.danhHieuFramme = 0;
							}
							this.fraDanhHieu.drawFrame(this.danhHieuFramme, x2, y2, 0, mGraphics.TOP | mGraphics.LEFT, g);
						}
					}
					num += mFont.tahoma_7_white.getHeight();
					bool flag16 = this.isPet || this.isMiniPet;
					if (flag16)
					{
						mFont = mFont.tahoma_7_blue1Small;
					}
					else
					{
						bool flag17 = flag2;
						if (flag17)
						{
							mFont = mFont.nameFontRed;
						}
						else
						{
							bool flag18 = flag3;
							if (flag18)
							{
								mFont = mFont.nameFontYellow;
							}
							else
							{
								bool flag19 = flag;
								if (flag19)
								{
									mFont = mFont.nameFontGreen;
								}
							}
						}
					}
					bool flag20 = (this.paintName || flag2 || flag3) && !flag;
					if (flag20)
					{
						bool flag21 = mSystem.clientType == 1;
						if (flag21)
						{
							mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
						}
						else
						{
							bool flag22 = this.charID == -83;
							if (flag22)
							{
								mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
							}
							else
							{
								mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER);
							}
						}
						num += mFont.tahoma_7.getHeight();
					}
					bool flag23 = flag;
					if (flag23)
					{
						bool flag24 = global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this);
						if (flag24)
						{
							mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
						}
						else
						{
							bool flag25 = this.charFocus == null;
							if (flag25)
							{
								mFont.drawString(g, this.cName, this.cx - 10, this.cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
								this.paintHp(g, this.cx - 16, this.cy - num + 10);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00012EF4 File Offset: 0x000110F4
	public void paintShadow(mGraphics g)
	{
		bool flag = this.isMabuHold;
		if (!flag)
		{
			bool flag2 = this.head == 377;
			if (!flag2)
			{
				bool flag3 = this.leg == 471;
				if (!flag3)
				{
					bool flag4 = this.isTeleport;
					if (!flag4)
					{
						bool flag5 = this.isFlyUp;
						if (!flag5)
						{
							int num = (int)TileMap.size;
							bool flag6 = (TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128;
							if (flag6)
							{
								bool flag7 = !TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4);
								if (flag7)
								{
									bool flag8 = TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0;
									if (flag8)
									{
										g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
									}
									else
									{
										bool flag9 = TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0;
										if (flag9)
										{
											g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
										}
										else
										{
											bool flag10 = TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8);
											if (flag10)
											{
												g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
											}
										}
									}
								}
							}
							g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
							g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
						}
					}
				}
			}
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000130E4 File Offset: 0x000112E4
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.cx;
		bool flag = TileMap.tileTypeAt(this.cx, this.cy, 2);
		if (flag)
		{
			this.ySd = this.cy;
		}
		else
		{
			this.ySd = this.cy;
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

	// Token: 0x060000F5 RID: 245 RVA: 0x00013194 File Offset: 0x00011394
	private void paintCharWithoutSkill(mGraphics g)
	{
		try
		{
			bool flag = this.isMafuba;
			if (flag)
			{
				this.paintCharBody(g, this.xMFB, this.yMFB, this.cdir, this.cf, false);
			}
			else
			{
				bool flag2 = this.isInvisiblez;
				if (flag2)
				{
					bool flag3 = this.me;
					if (flag3)
					{
						bool flag4 = GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90;
						if (flag4)
						{
							SmallImage.drawSmallImage(g, 1196, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
						else
						{
							SmallImage.drawSmallImage(g, 1195, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
				}
				else
				{
					this.paintCharBody(g, this.cx, this.cy + this.fy, this.cdir, this.cf, true);
				}
				bool flag5 = this.isLockAttack;
				if (flag5)
				{
					SmallImage.drawSmallImage(g, 290, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi paint char without skill: " + ex.ToString());
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00013300 File Offset: 0x00011500
	public void paintBag(mGraphics g, short[] id, int x, int y, int dir, bool isPaintChar)
	{
		int num = 0;
		int num2 = 0;
		bool flag = this.statusMe == 6;
		if (flag)
		{
			num = 8;
			num2 = 17;
		}
		bool flag2 = this.statusMe == 1;
		if (flag2)
		{
			bool flag3 = this.cp1 % 15 < 5;
			if (flag3)
			{
				num = 8;
				num2 = 17;
			}
			else
			{
				num = 8;
				num2 = 18;
			}
		}
		bool flag4 = this.statusMe == 2;
		if (flag4)
		{
			bool flag5 = this.cf <= 3;
			if (flag5)
			{
				num = 7;
				num2 = 17;
			}
			else
			{
				num = 7;
				num2 = 18;
			}
		}
		bool flag6 = this.statusMe == 3 || this.statusMe == 9;
		if (flag6)
		{
			num = 5;
			num2 = 20;
		}
		bool flag7 = this.statusMe == 4;
		if (flag7)
		{
			bool flag8 = this.cf == 8;
			if (flag8)
			{
				num = 5;
				num2 = 16;
			}
			else
			{
				num = 5;
				num2 = 20;
			}
		}
		bool flag9 = this.statusMe == 10;
		if (flag9)
		{
			bool flag10 = this.cf == 8;
			if (flag10)
			{
				num = 0;
				num2 = 23;
			}
			else
			{
				num = 5;
				num2 = 22;
			}
		}
		bool flag11 = this.isInjure > 0;
		if (flag11)
		{
			num = 5;
			num2 = 18;
		}
		bool flag12 = this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length;
		if (flag12)
		{
			num = -1;
			num2 = 17;
		}
		this.fBag++;
		bool flag13 = this.fBag > 10000;
		if (flag13)
		{
			this.fBag = 0;
		}
		sbyte b = (sbyte)(this.fBag / 4 % id.Length);
		bool flag14 = !isPaintChar;
		if (flag14)
		{
			bool flag15 = id.Length == 2;
			if (flag15)
			{
				b = 1;
			}
			bool flag16 = id.Length == 3;
			if (flag16)
			{
				bool flag17 = id[2] >= 0;
				if (flag17)
				{
					b = 2;
					bool flag18 = GameCanvas.gameTick % 10 > 5;
					if (flag18)
					{
						b = 1;
					}
				}
				else
				{
					b = 1;
				}
			}
		}
		else
		{
			bool flag19 = id.Length > 1 && (b == 0 || b == 1) && this.statusMe != 1 && this.statusMe != 6;
			if (flag19)
			{
				this.fBag = 0;
				b = 0;
				bool flag20 = GameCanvas.gameTick % 10 > 5;
				if (flag20)
				{
					b = 1;
				}
			}
		}
		SmallImage.drawSmallImage(g, (int)id[(int)b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0001355C File Offset: 0x0001175C
	public bool isCharBodyImageID(int id)
	{
		Part part = GameScr.parts[this.head];
		Part part2 = GameScr.parts[this.leg];
		Part part3 = GameScr.parts[this.body];
		int i = 0;
		while (i < global::Char.CharInfo.Length)
		{
			bool flag = id == (int)part.pi[global::Char.CharInfo[i][0][0]].id;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = id == (int)part2.pi[global::Char.CharInfo[i][1][0]].id;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = id == (int)part3.pi[global::Char.CharInfo[i][2][0]].id;
					if (!flag3)
					{
						i++;
						continue;
					}
					result = true;
				}
			}
			return result;
		}
		return false;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00013628 File Offset: 0x00011828
	public void paintHead(mGraphics g, int cx, int cy, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00013678 File Offset: 0x00011878
	public void paintHeadWithXY(mGraphics g, int x, int y, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, x + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000136E8 File Offset: 0x000118E8
	public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
	{
		this.ph = GameScr.parts[this.head];
		this.pl = GameScr.parts[this.leg];
		this.pb = GameScr.parts[this.body];
		bool flag = this.bag >= 0 && this.statusMe != 14;
		if (flag)
		{
			bool flag2 = !ClanImage.idImages.containsKey(this.bag.ToString() + string.Empty);
			if (flag2)
			{
				ClanImage.idImages.put(this.bag.ToString() + string.Empty, new ClanImage());
				Service.gI().requestBagImage((sbyte)this.bag);
			}
			else
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(this.bag.ToString() + string.Empty);
				bool flag3 = clanImage.idImage != null && isPaintBag;
				if (flag3)
				{
					this.paintBag(g, clanImage.idImage, cx, cy, cdir, true);
				}
			}
		}
		int num = 2;
		int anchor = 24;
		int anchor2 = StaticObj.TOP_RIGHT;
		int num2 = -1;
		bool flag4 = cdir == 1;
		if (flag4)
		{
			num = 0;
			anchor = 0;
			anchor2 = 0;
			num2 = 1;
		}
		bool flag5 = this.statusMe == 14;
		if (flag5)
		{
			bool flag6 = GameCanvas.gameTick % 4 > 0;
			if (flag6)
			{
				g.drawImage(ItemMap.imageFlare, cx, cy - this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			int num3 = 0;
			bool flag7 = this.head == 89 || this.head == 457 || this.head == 460 || this.head == 461 || this.head == 462 || this.head == 463 || this.head == 464 || this.head == 465 || this.head == 466;
			if (flag7)
			{
				num3 = 15;
			}
			SmallImage.drawSmallImage(g, 834, cx, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy - 2 + num3, num, StaticObj.TOP_CENTER);
			SmallImage.drawSmallImage(g, 79, cx, cy - this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
			SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			this.paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			bool flag8 = this.isHead_2Fr(this.head);
			if (flag8)
			{
				Part part = GameScr.parts[this.getFHead(this.head)];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			this.paintHat_front(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			this.paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
		}
		else
		{
			this.paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			bool flag9 = this.isHead_2Fr(this.head);
			if (flag9)
			{
				Part part2 = GameScr.parts[this.getFHead(this.head)];
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part2.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part2.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			SmallImage.drawSmallImage(g, (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].id, cx + (global::Char.CharInfo[cf][1][1] + (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].dx) * num2, cy - global::Char.CharInfo[cf][1][2] + (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].dy, num, anchor);
			SmallImage.drawSmallImage(g, (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].id, cx + (global::Char.CharInfo[cf][2][1] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dx) * num2, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy, num, anchor);
			this.paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
		}
		this.ch = ((this.isMonkey != 1 && !this.isFusion) ? (global::Char.CharInfo[0][0][2] + (int)this.ph.pi[global::Char.CharInfo[0][0][0]].dy + 10) : 60);
		int num4 = (int)((Res.abs((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy) < 22) ? this.ph.pi[global::Char.CharInfo[cf][0][0]].dy : ((this.ph.pi[global::Char.CharInfo[cf][0][0]].dy >= 0) ? (this.ph.pi[global::Char.CharInfo[cf][0][0]].dy - 5) : (this.ph.pi[global::Char.CharInfo[cf][0][0]].dy + 5)));
		this.cH_new = cy - global::Char.CharInfo[cf][0][2] + num4;
		bool flag10 = this.statusMe == 1 && this.charID > 0 && !this.isMask && !this.isUseChargeSkill() && !this.isWaitMonkey && this.skillPaint == null && cf != 23 && this.bag < 0 && ((GameCanvas.gameTick + this.charID) % 30 == 0 || this.isFreez);
		if (flag10)
		{
			g.drawImage((this.cgender != 1) ? global::Char.eyeTraiDat : global::Char.eyeNamek, cx + -((this.cgender != 1) ? 2 : 2) * num2, cy - 32 + ((this.cgender != 1) ? 11 : 10) - cf, anchor2);
		}
		bool flag11 = this.eProtect != null;
		if (flag11)
		{
			this.eProtect.paint(g);
		}
		bool flag12 = this.eDanhHieu != null;
		if (flag12)
		{
			this.eDanhHieu.paint(g);
		}
		this.paintPKFlag(g);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0001408C File Offset: 0x0001228C
	public void paintCharWithSkill(mGraphics g)
	{
		this.ty = 0;
		SkillInfoPaint[] array = this.skillInfoPaint();
		this.cf = array[this.indexSkill].status;
		this.paintCharWithoutSkill(g);
		bool flag = this.cdir == 1;
		if (flag)
		{
			bool flag2 = this.eff0 != null;
			if (flag2)
			{
				bool flag3 = this.dx0 == 0;
				if (flag3)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				bool flag4 = this.dy0 == 0;
				if (flag4)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx + this.dx0 + this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				bool flag5 = this.i0 >= this.eff0.arrEfInfo.Length;
				if (flag5)
				{
					this.eff0 = null;
					this.i0 = (this.dx0 = (this.dy0 = 0));
				}
			}
			bool flag6 = this.eff1 != null;
			if (flag6)
			{
				bool flag7 = this.dx1 == 0;
				if (flag7)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				bool flag8 = this.dy1 == 0;
				if (flag8)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx + this.dx1 + this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				bool flag9 = this.i1 >= this.eff1.arrEfInfo.Length;
				if (flag9)
				{
					this.eff1 = null;
					this.i1 = (this.dx1 = (this.dy1 = 0));
				}
			}
			bool flag10 = this.eff2 != null;
			if (flag10)
			{
				bool flag11 = this.dx2 == 0;
				if (flag11)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				bool flag12 = this.dy2 == 0;
				if (flag12)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx + this.dx2 + this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				bool flag13 = this.i2 >= this.eff2.arrEfInfo.Length;
				if (flag13)
				{
					this.eff2 = null;
					this.i2 = (this.dx2 = (this.dy2 = 0));
				}
			}
		}
		else
		{
			bool flag14 = this.eff0 != null;
			if (flag14)
			{
				bool flag15 = this.dx0 == 0;
				if (flag15)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				bool flag16 = this.dy0 == 0;
				if (flag16)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx - this.dx0 - this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				bool flag17 = this.i0 >= this.eff0.arrEfInfo.Length;
				if (flag17)
				{
					this.eff0 = null;
					this.i0 = 0;
					this.dx0 = 0;
					this.dy0 = 0;
				}
			}
			bool flag18 = this.eff1 != null;
			if (flag18)
			{
				bool flag19 = this.dx1 == 0;
				if (flag19)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				bool flag20 = this.dy1 == 0;
				if (flag20)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx - this.dx1 - this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				bool flag21 = this.i1 >= this.eff1.arrEfInfo.Length;
				if (flag21)
				{
					this.eff1 = null;
					this.i1 = 0;
					this.dx1 = 0;
					this.dy1 = 0;
				}
			}
			bool flag22 = this.eff2 != null;
			if (flag22)
			{
				bool flag23 = this.dx2 == 0;
				if (flag23)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				bool flag24 = this.dy2 == 0;
				if (flag24)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx - this.dx2 - this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				bool flag25 = this.i2 >= this.eff2.arrEfInfo.Length;
				if (flag25)
				{
					this.eff2 = null;
					this.i2 = 0;
					this.dx2 = 0;
					this.dy2 = 0;
				}
			}
		}
		this.indexSkill++;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0001479C File Offset: 0x0001299C
	public static int getIndexChar(int ID)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			bool flag = @char.charID == ID;
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000147F0 File Offset: 0x000129F0
	public void moveTo(int toX, int toY, int type)
	{
		bool flag = type == 1 || Res.abs(toX - this.cx) > 100 || Res.abs(toY - this.cy) > 300;
		if (flag)
		{
			this.createShadow(this.cx, this.cy, 10);
			this.cx = toX;
			this.cy = toY;
			this.vMovePoints.removeAllElements();
			this.statusMe = 6;
			this.cp3 = 0;
			this.currentMovePoint = null;
			this.cf = 25;
		}
		else
		{
			int dir = 0;
			int act = 0;
			int num = toX - this.cx;
			int num2 = toY - this.cy;
			bool flag2 = num == 0 && num2 == 0;
			if (flag2)
			{
				act = 1;
				this.cp3 = 0;
			}
			else
			{
				bool flag3 = num2 == 0;
				if (flag3)
				{
					act = 2;
					bool flag4 = num > 0;
					if (flag4)
					{
						dir = 1;
					}
					bool flag5 = num < 0;
					if (flag5)
					{
						dir = -1;
					}
				}
				else
				{
					bool flag6 = num2 != 0;
					if (flag6)
					{
						bool flag7 = num2 < 0;
						if (flag7)
						{
							act = 3;
						}
						bool flag8 = num2 > 0;
						if (flag8)
						{
							act = 4;
						}
						bool flag9 = num < 0;
						if (flag9)
						{
							dir = -1;
						}
						bool flag10 = num > 0;
						if (flag10)
						{
							dir = 1;
						}
					}
				}
			}
			this.vMovePoints.addElement(new MovePoint(toX, toY, act, dir));
			bool flag11 = this.statusMe != 6;
			if (flag11)
			{
				this.statusBeforeNothing = this.statusMe;
			}
			this.statusMe = 6;
			this.cp3 = 0;
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00014968 File Offset: 0x00012B68
	public static void getcharInjure(int cID, int dx, int dy, int HP)
	{
		global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(cID);
		bool flag = @char.vMovePoints.size() == 0;
		if (!flag)
		{
			MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
			int xEnd = movePoint.xEnd + dx;
			int yEnd = movePoint.yEnd + dy;
			global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(cID);
			char2.cHP -= HP;
			bool flag2 = char2.cHP < 0;
			if (flag2)
			{
				char2.cHP = 0;
			}
			char2.cHPShow = ((global::Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
			char2.statusMe = 6;
			char2.cp3 = 0;
			char2.vMovePoints.addElement(new MovePoint(xEnd, yEnd, 8, char2.cdir));
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00014A4C File Offset: 0x00012C4C
	public bool isMagicTree()
	{
		bool flag = GameScr.gI().magicTree != null;
		bool result;
		if (flag)
		{
			int x = GameScr.gI().magicTree.x;
			int y = GameScr.gI().magicTree.y;
			result = (this.cx > x - 30 && this.cx < x + 30 && this.cy > y - 30 && this.cy < y + 30);
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00014AC8 File Offset: 0x00012CC8
	public void searchItem()
	{
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		bool flag = this.itemFocus == null;
		if (flag)
		{
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
				int num2 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
				int num3 = (num <= num2) ? num2 : num;
				bool flag2 = num <= 48 && num2 <= 48 && (this.itemFocus == null || num3 < array[3]);
				if (flag2)
				{
					bool flag3 = GameScr.gI().auto != 0 && GameScr.gI().isBagFull();
					if (flag3)
					{
						bool flag4 = itemMap.template.type == 9;
						if (flag4)
						{
							this.itemFocus = itemMap;
							array[3] = num3;
						}
					}
					else
					{
						this.itemFocus = itemMap;
						array[3] = num3;
					}
				}
			}
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00014BEC File Offset: 0x00012DEC
	public void searchFocus()
	{
		bool flag = global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null;
		if (flag)
		{
			this.timeFocusToMob = 200;
		}
		else
		{
			bool flag2 = this.timeFocusToMob > 0;
			if (flag2)
			{
				this.timeFocusToMob--;
			}
			else
			{
				bool flag3 = global::Char.isManualFocus && this.charFocus != null && (this.charFocus.statusMe == 15 || this.charFocus.isInvisiblez);
				if (flag3)
				{
					this.charFocus = null;
				}
				bool flag4 = GameCanvas.gameTick % 2 == 0;
				if (!flag4)
				{
					bool flag5 = this.isMeCanAttackOtherPlayer(this.charFocus);
					if (!flag5)
					{
						int num = 0;
						bool flag6 = this.nClass != null && (this.nClass.classId == 0 || this.nClass.classId == 1 || this.nClass.classId == 3 || this.nClass.classId == 5);
						if (flag6)
						{
							num = 40;
						}
						int[] array = new int[]
						{
							-1,
							-1,
							-1,
							-1
						};
						int num2 = GameScr.cmx - 10;
						int num3 = GameScr.cmx + GameCanvas.w + 10;
						int num4 = GameScr.cmy;
						int num5 = GameScr.cmy + GameCanvas.h - GameScr.cmdBarH + 10;
						bool flag7 = global::Char.isManualFocus;
						if (flag7)
						{
							bool flag8 = (this.mobFocus != null && this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && num4 <= this.mobFocus.y && this.mobFocus.y <= num5) || (this.npcFocus != null && num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && num4 <= this.npcFocus.cy && this.npcFocus.cy <= num5) || (this.charFocus != null && num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && num4 <= this.charFocus.cy && this.charFocus.cy <= num5) || (this.itemFocus != null && num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && num4 <= this.itemFocus.y && this.itemFocus.y <= num5);
							if (flag8)
							{
								return;
							}
							global::Char.isManualFocus = false;
						}
						num2 = global::Char.myCharz().cx - 80;
						num3 = global::Char.myCharz().cx + 80;
						num4 = global::Char.myCharz().cy - 30;
						num5 = global::Char.myCharz().cy + 30;
						bool flag9 = this.npcFocus != null && this.npcFocus.template.npcTemplateId == 6;
						if (flag9)
						{
							num2 = global::Char.myCharz().cx - 20;
							num3 = global::Char.myCharz().cx + 20;
							num4 = global::Char.myCharz().cy - 10;
							num5 = global::Char.myCharz().cy + 10;
						}
						bool flag10 = this.npcFocus == null;
						if (flag10)
						{
							for (int i = 0; i < GameScr.vNpc.size(); i++)
							{
								Npc npc = (Npc)GameScr.vNpc.elementAt(i);
								bool flag11 = npc.statusMe != 15;
								if (flag11)
								{
									int num6 = global::Math.abs(global::Char.myCharz().cx - npc.cx);
									int num7 = global::Math.abs(global::Char.myCharz().cy - npc.cy);
									int num8 = (num6 <= num7) ? num7 : num6;
									num2 = global::Char.myCharz().cx - 80;
									num3 = global::Char.myCharz().cx + 80;
									num4 = global::Char.myCharz().cy - 30;
									num5 = global::Char.myCharz().cy + 30;
									bool flag12 = npc.template.npcTemplateId == 6;
									if (flag12)
									{
										num2 = global::Char.myCharz().cx - 20;
										num3 = global::Char.myCharz().cx + 20;
										num4 = global::Char.myCharz().cy - 10;
										num5 = global::Char.myCharz().cy + 10;
									}
									bool flag13 = num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5 && (this.npcFocus == null || num8 < array[1]);
									if (flag13)
									{
										this.npcFocus = npc;
										array[1] = num8;
									}
								}
							}
						}
						else
						{
							bool flag14 = num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && num4 <= this.npcFocus.cy && this.npcFocus.cy <= num5;
							if (flag14)
							{
								this.clearFocus(1);
								return;
							}
							this.deFocusNPC();
							for (int j = 0; j < GameScr.vNpc.size(); j++)
							{
								Npc npc2 = (Npc)GameScr.vNpc.elementAt(j);
								bool flag15 = npc2.statusMe != 15;
								if (flag15)
								{
									int num9 = global::Math.abs(global::Char.myCharz().cx - npc2.cx);
									int num10 = global::Math.abs(global::Char.myCharz().cy - npc2.cy);
									int num11 = (num9 <= num10) ? num10 : num9;
									num2 = global::Char.myCharz().cx - 80;
									num3 = global::Char.myCharz().cx + 80;
									num4 = global::Char.myCharz().cy - 30;
									num5 = global::Char.myCharz().cy + 30;
									bool flag16 = npc2.template.npcTemplateId == 6;
									if (flag16)
									{
										num2 = global::Char.myCharz().cx - 20;
										num3 = global::Char.myCharz().cx + 20;
										num4 = global::Char.myCharz().cy - 10;
										num5 = global::Char.myCharz().cy + 10;
									}
									bool flag17 = num2 <= npc2.cx && npc2.cx <= num3 && num4 <= npc2.cy && npc2.cy <= num5 && (this.npcFocus == null || num11 < array[1]);
									if (flag17)
									{
										this.npcFocus = npc2;
										array[1] = num11;
									}
								}
							}
						}
						bool flag18 = this.itemFocus == null;
						if (flag18)
						{
							for (int k = 0; k < GameScr.vItemMap.size(); k++)
							{
								ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
								int num12 = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
								int num13 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
								int num14 = (num12 <= num13) ? num13 : num12;
								bool flag19 = num12 <= 48 && num13 <= 48 && (this.itemFocus == null || num14 < array[3]);
								if (flag19)
								{
									bool flag20 = GameScr.gI().auto != 0 && GameScr.gI().isBagFull();
									if (flag20)
									{
										bool flag21 = itemMap.template.type == 9;
										if (flag21)
										{
											this.itemFocus = itemMap;
											array[3] = num14;
										}
									}
									else
									{
										this.itemFocus = itemMap;
										array[3] = num14;
									}
								}
							}
						}
						else
						{
							bool flag22 = num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && num4 <= this.itemFocus.y && this.itemFocus.y <= num5;
							if (flag22)
							{
								this.clearFocus(3);
								return;
							}
							this.itemFocus = null;
							for (int l = 0; l < GameScr.vItemMap.size(); l++)
							{
								ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
								int num15 = global::Math.abs(global::Char.myCharz().cx - itemMap2.x);
								int num16 = global::Math.abs(global::Char.myCharz().cy - itemMap2.y);
								int num17 = (num15 <= num16) ? num16 : num15;
								bool flag23 = num2 <= itemMap2.x && itemMap2.x <= num3 && num4 <= itemMap2.y && itemMap2.y <= num5 && (this.itemFocus == null || num17 < array[3]);
								if (flag23)
								{
									bool flag24 = GameScr.gI().auto != 0 && GameScr.gI().isBagFull();
									if (flag24)
									{
										bool flag25 = itemMap2.template.type == 9;
										if (flag25)
										{
											this.itemFocus = itemMap2;
											array[3] = num17;
										}
									}
									else
									{
										this.itemFocus = itemMap2;
										array[3] = num17;
									}
								}
							}
						}
						num2 = global::Char.myCharz().cx - global::Char.myCharz().getdxSkill() - 10;
						num3 = global::Char.myCharz().cx + global::Char.myCharz().getdxSkill() + 10;
						num4 = global::Char.myCharz().cy - global::Char.myCharz().getdySkill() - num - 20;
						num5 = global::Char.myCharz().cy + global::Char.myCharz().getdySkill() + 20;
						bool flag26 = num5 > global::Char.myCharz().cy + 30;
						if (flag26)
						{
							num5 = global::Char.myCharz().cy + 30;
						}
						bool flag27 = this.mobFocus == null;
						if (flag27)
						{
							for (int m = 0; m < GameScr.vMob.size(); m++)
							{
								Mob mob = (Mob)GameScr.vMob.elementAt(m);
								int num18 = global::Math.abs(global::Char.myCharz().cx - mob.x);
								int num19 = global::Math.abs(global::Char.myCharz().cy - mob.y);
								int num20 = (num18 <= num19) ? num19 : num18;
								bool flag28 = num2 <= mob.x && mob.x <= num3 && num4 <= mob.y && mob.y <= num5 && (this.mobFocus == null || num20 < array[0]);
								if (flag28)
								{
									this.mobFocus = mob;
									array[0] = num20;
								}
							}
						}
						else
						{
							bool flag29 = this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && num4 <= this.mobFocus.y && this.mobFocus.y <= num5;
							if (flag29)
							{
								this.clearFocus(0);
								return;
							}
							this.mobFocus = null;
							for (int n = 0; n < GameScr.vMob.size(); n++)
							{
								Mob mob2 = (Mob)GameScr.vMob.elementAt(n);
								int num21 = global::Math.abs(global::Char.myCharz().cx - mob2.x);
								int num22 = global::Math.abs(global::Char.myCharz().cy - mob2.y);
								int num23 = (num21 <= num22) ? num22 : num21;
								bool flag30 = num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5 && (this.mobFocus == null || num23 < array[0]);
								if (flag30)
								{
									this.mobFocus = mob2;
									array[0] = num23;
								}
							}
						}
						bool flag31 = this.charFocus == null;
						if (flag31)
						{
							for (int num24 = 0; num24 < GameScr.vCharInMap.size(); num24++)
							{
								global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num24);
								bool flag32 = @char.statusMe != 15 && !@char.isInvisiblez;
								if (flag32)
								{
									bool flag33 = this.wdx == 0 && this.wdy == 0;
									if (flag33)
									{
										int num25 = global::Math.abs(global::Char.myCharz().cx - @char.cx);
										int num26 = global::Math.abs(global::Char.myCharz().cy - @char.cy);
										int num27 = (num25 <= num26) ? num26 : num25;
										bool flag34 = num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && (this.charFocus == null || num27 < array[2]);
										if (flag34)
										{
											this.charFocus = @char;
											array[2] = num27;
										}
									}
								}
							}
						}
						else
						{
							bool flag35 = num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && num4 <= this.charFocus.cy && this.charFocus.cy <= num5 && this.charFocus.statusMe != 15 && !this.charFocus.isInvisiblez;
							if (flag35)
							{
								this.clearFocus(2);
								return;
							}
							this.charFocus = null;
							for (int num28 = 0; num28 < GameScr.vCharInMap.size(); num28++)
							{
								global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(num28);
								bool flag36 = char2.statusMe != 15 && !char2.isInvisiblez;
								if (flag36)
								{
									bool flag37 = this.wdx == 0 && this.wdy == 0;
									if (flag37)
									{
										int num29 = global::Math.abs(global::Char.myCharz().cx - char2.cx);
										int num30 = global::Math.abs(global::Char.myCharz().cy - char2.cy);
										int num31 = (num29 <= num30) ? num30 : num29;
										bool flag38 = num2 <= char2.cx && char2.cx <= num3 && num4 <= char2.cy && char2.cy <= num5 && (this.charFocus == null || num31 < array[2]);
										if (flag38)
										{
											this.charFocus = char2;
											array[2] = num31;
										}
									}
								}
							}
						}
						int num32 = -1;
						for (int num33 = 0; num33 < array.Length; num33++)
						{
							bool flag39 = num32 == -1;
							if (flag39)
							{
								bool flag40 = array[num33] != -1;
								if (flag40)
								{
									num32 = num33;
								}
							}
							else
							{
								bool flag41 = array[num33] < array[num32] && array[num33] != -1;
								if (flag41)
								{
									num32 = num33;
								}
							}
						}
						this.clearFocus(num32);
						bool flag42 = this.me && this.isAttacPlayerStatus();
						if (flag42)
						{
							bool flag43 = this.mobFocus != null && !this.mobFocus.isMobMe;
							if (flag43)
							{
								this.mobFocus = null;
							}
							this.npcFocus = null;
							this.itemFocus = null;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00015B5C File Offset: 0x00013D5C
	public void clearFocus(int index)
	{
		bool flag = index == 0;
		if (flag)
		{
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
		}
		else
		{
			bool flag2 = index == 1;
			if (flag2)
			{
				this.mobFocus = null;
				this.charFocus = null;
				this.itemFocus = null;
			}
			else
			{
				bool flag3 = index == 2;
				if (flag3)
				{
					this.mobFocus = null;
					this.deFocusNPC();
					this.itemFocus = null;
				}
				else
				{
					bool flag4 = index == 3;
					if (flag4)
					{
						this.mobFocus = null;
						this.deFocusNPC();
						this.charFocus = null;
					}
				}
			}
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00015BEC File Offset: 0x00013DEC
	public static bool isCharInScreen(global::Char c)
	{
		int cmx = GameScr.cmx;
		int num = GameScr.cmx + GameCanvas.w;
		int num2 = GameScr.cmy + 10;
		int num3 = GameScr.cmy + GameScr.gH;
		return c.statusMe != 15 && !c.isInvisiblez && cmx <= c.cx && c.cx <= num && num2 <= c.cy && c.cy <= num3;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00015C64 File Offset: 0x00013E64
	public bool isAttacPlayerStatus()
	{
		return this.cTypePk == 4 || this.cTypePk == 3;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00015C8C File Offset: 0x00013E8C
	public void setHoldChar(global::Char r)
	{
		bool flag = this.cx < r.cx;
		if (flag)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.charHold = r;
		this.holder = true;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00015CD0 File Offset: 0x00013ED0
	public void setHoldMob(Mob r)
	{
		bool flag = this.cx < r.x;
		if (flag)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.mobHold = r;
		this.holder = true;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00015D14 File Offset: 0x00013F14
	public void findNextFocusByKey()
	{
		Res.outz("focus size= " + this.focus.size().ToString());
		bool flag = (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null) && this.focus.size() == 0;
		if (!flag)
		{
			this.focus.removeAllElements();
			int num = 0;
			int num2 = GameScr.cmx + 10;
			int num3 = GameScr.cmx + GameCanvas.w - 10;
			int num4 = GameScr.cmy + 10;
			int num5 = GameScr.cmy + GameScr.gH;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				bool flag2 = @char.statusMe != 15 && !@char.isInvisiblez && num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && @char.charID != -114 && (TileMap.mapID != 129 || (TileMap.mapID == 129 && global::Char.myCharz().cy > 264));
				if (flag2)
				{
					this.focus.addElement(@char);
					bool flag3 = this.charFocus != null && @char.Equals(this.charFocus);
					if (flag3)
					{
						num = this.focus.size();
					}
				}
			}
			bool flag4 = this.me && this.isAttacPlayerStatus();
			if (flag4)
			{
				Res.outz("co the tan cong nguoi");
				for (int j = 0; j < GameScr.vMob.size(); j++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(j);
					bool flag5 = !GameScr.gI().isMeCanAttackMob(mob);
					if (flag5)
					{
						Res.outz("khong the tan cong quai");
						this.mobFocus = null;
					}
					else
					{
						Res.outz("co the tan ong quai");
						this.focus.addElement(mob);
						bool flag6 = this.mobFocus != null;
						if (flag6)
						{
							num = this.focus.size();
						}
					}
				}
				this.npcFocus = null;
				this.itemFocus = null;
				bool flag7 = this.focus.size() > 0;
				if (flag7)
				{
					bool flag8 = num >= this.focus.size();
					if (flag8)
					{
						num = 0;
					}
					this.focusManualTo(this.focus.elementAt(num));
				}
				else
				{
					this.mobFocus = null;
					this.deFocusNPC();
					this.charFocus = null;
					this.itemFocus = null;
					global::Char.isManualFocus = false;
				}
			}
			else
			{
				for (int k = 0; k < GameScr.vItemMap.size(); k++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
					bool flag9 = num2 <= itemMap.x && itemMap.x <= num3 && num4 <= itemMap.y && itemMap.y <= num5;
					if (flag9)
					{
						this.focus.addElement(itemMap);
						bool flag10 = this.itemFocus != null && itemMap.Equals(this.itemFocus);
						if (flag10)
						{
							num = this.focus.size();
						}
					}
				}
				for (int l = 0; l < GameScr.vMob.size(); l++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
					bool flag11 = mob2.status != 1 && mob2.status != 0 && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5;
					if (flag11)
					{
						this.focus.addElement(mob2);
						bool flag12 = this.mobFocus != null && mob2.Equals(this.mobFocus);
						if (flag12)
						{
							num = this.focus.size();
						}
					}
				}
				for (int m = 0; m < GameScr.vNpc.size(); m++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(m);
					bool flag13 = npc.statusMe != 15 && num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5;
					if (flag13)
					{
						this.focus.addElement(npc);
						bool flag14 = this.npcFocus != null && npc.Equals(this.npcFocus);
						if (flag14)
						{
							num = this.focus.size();
						}
					}
				}
				bool flag15 = this.focus.size() > 0;
				if (flag15)
				{
					bool flag16 = num >= this.focus.size();
					if (flag16)
					{
						num = 0;
					}
					this.focusManualTo(this.focus.elementAt(num));
				}
				else
				{
					this.mobFocus = null;
					this.deFocusNPC();
					this.charFocus = null;
					this.itemFocus = null;
					global::Char.isManualFocus = false;
				}
			}
		}
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0001628C File Offset: 0x0001448C
	public void deFocusNPC()
	{
		bool flag = this.me && this.npcFocus != null;
		if (flag)
		{
			bool flag2 = !GameCanvas.menu.showMenu;
			if (flag2)
			{
				global::Char.chatPopup = null;
			}
			this.npcFocus = null;
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x000162D4 File Offset: 0x000144D4
	public void updateCharInBridge()
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			bool flag = TileMap.tileTypeAt(this.cx, this.cy + 1, 1024);
			if (flag)
			{
				TileMap.setTileTypeAtPixel(this.cx, this.cy + 1, 512);
				TileMap.setTileTypeAtPixel(this.cx, this.cy - 2, 512);
			}
			bool flag2 = TileMap.tileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512);
			if (flag2)
			{
				TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512);
				TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy - 2, 512);
			}
			bool flag3 = TileMap.tileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512);
			if (flag3)
			{
				TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512);
				TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy - 2, 512);
			}
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00016408 File Offset: 0x00014608
	public static void sort(int[] data)
	{
		int num = 5;
		for (int i = 0; i < num - 1; i++)
		{
			for (int j = i + 1; j < num; j++)
			{
				bool flag = data[i] < data[j];
				if (flag)
				{
					int num2 = data[j];
					data[j] = data[i];
					data[i] = num2;
				}
			}
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00016464 File Offset: 0x00014664
	public static bool setInsc(int cmX, int cmWx, int x, int cmy, int cmyH, int y)
	{
		return x <= cmWx && x >= cmX && y <= cmyH && y >= cmy;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00016490 File Offset: 0x00014690
	public void kickOption(Item item, int maxKick)
	{
		int num = 0;
		bool flag = item != null && item.options != null;
		if (flag)
		{
			for (int i = 0; i < item.options.size(); i++)
			{
				ItemOption itemOption = (ItemOption)item.options.elementAt(i);
				itemOption.active = 0;
				bool flag2 = itemOption.optionTemplate.type == 2;
				if (flag2)
				{
					bool flag3 = num < maxKick;
					if (flag3)
					{
						itemOption.active = 1;
						num++;
					}
				}
				else
				{
					bool flag4 = itemOption.optionTemplate.type == 3 && item.upgrade >= 4;
					if (flag4)
					{
						itemOption.active = 1;
					}
					else
					{
						bool flag5 = itemOption.optionTemplate.type == 4 && item.upgrade >= 8;
						if (flag5)
						{
							itemOption.active = 1;
						}
						else
						{
							bool flag6 = itemOption.optionTemplate.type == 5 && item.upgrade >= 12;
							if (flag6)
							{
								itemOption.active = 1;
							}
							else
							{
								bool flag7 = itemOption.optionTemplate.type == 6 && item.upgrade >= 14;
								if (flag7)
								{
									itemOption.active = 1;
								}
								else
								{
									bool flag8 = itemOption.optionTemplate.type == 7 && item.upgrade >= 16;
									if (flag8)
									{
										itemOption.active = 1;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00016614 File Offset: 0x00014814
	public void doInjure(int HPShow, int MPShow, bool isCrit, bool isMob)
	{
		this.isCrit = isCrit;
		this.isMob = isMob;
		Res.outz(string.Concat(new object[]
		{
			"CHP= ",
			this.cHP,
			" dame -= ",
			HPShow,
			" HP FULL= ",
			this.cHPFull
		}));
		this.cHP -= HPShow;
		this.cMP -= MPShow;
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		bool flag = this.cHP < 0;
		if (flag)
		{
			this.cHP = 0;
		}
		bool flag2 = this.cMP < 0;
		if (flag2)
		{
			this.cMP = 0;
		}
		bool flag3 = isMob || (!isMob && this.cTypePk != 4 && this.damMP != -100);
		if (flag3)
		{
			bool flag4 = HPShow <= 0;
			if (flag4)
			{
				bool flag5 = this.me;
				if (flag5)
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS_ME);
				}
				else
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS);
				}
			}
			else
			{
				GameScr.startFlyText("-" + HPShow.ToString(), this.cx, this.cy - this.ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
			}
		}
		bool flag6 = HPShow > 0;
		if (flag6)
		{
			this.isInjure = 6;
		}
		ServerEffect.addServerEffect(80, this, 1);
		bool flag7 = this.isDie;
		if (flag7)
		{
			this.isDie = false;
			global::Char.isLockKey = false;
			this.startDie((short)this.xSd, (short)this.ySd);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00016818 File Offset: 0x00014A18
	public void doInjure()
	{
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		this.isInjure = 6;
		ServerEffect.addServerEffect(8, this, 1);
		this.isInjureHp = true;
		this.twHp = 0;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00016870 File Offset: 0x00014A70
	public void startDie(short toX, short toY)
	{
		this.isMonkey = 0;
		this.isWaitMonkey = false;
		bool flag = this.me && this.isDie;
		if (!flag)
		{
			bool flag2 = this.me;
			if (flag2)
			{
				this.isLockMove = true;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					@char.killCharId = -9999;
				}
				bool flag3 = GameCanvas.panel != null && GameCanvas.panel.cp != null;
				if (flag3)
				{
					GameCanvas.panel.cp = null;
				}
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.cp != null;
				if (flag4)
				{
					GameCanvas.panel2.cp = null;
				}
			}
			this.statusMe = 5;
			this.cp2 = (int)toX;
			this.cp3 = (int)toY;
			this.cp1 = 0;
			this.cHP = 0;
			this.testCharId = -9999;
			this.killCharId = -9999;
			bool flag5 = this.me && this.myskill != null && this.myskill.template.id != 14;
			if (flag5)
			{
				this.stopUseChargeSkill();
			}
			this.cTypePk = 0;
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x000169C3 File Offset: 0x00014BC3
	public void waitToDie(short toX, short toY)
	{
		this.wdx = toX;
		this.wdy = toY;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x000169D4 File Offset: 0x00014BD4
	public void liveFromDead()
	{
		this.cHP = this.cHPFull;
		this.cMP = this.cMPFull;
		this.statusMe = 1;
		this.cp1 = (this.cp2 = (this.cp3 = 0));
		ServerEffect.addServerEffect(109, this, 2);
		GameScr.gI().center = null;
		GameScr.isHaveSelectSkill = true;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00016A38 File Offset: 0x00014C38
	public bool doUsePotion()
	{
		bool flag = this.arrItemBag == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			for (int i = 0; i < this.arrItemBag.Length; i++)
			{
				bool flag2 = this.arrItemBag[i] != null;
				if (flag2)
				{
					bool flag3 = this.arrItemBag[i].template.type == 6;
					if (flag3)
					{
						Service.gI().useItem(0, 1, -1, this.arrItemBag[i].template.id);
						return true;
					}
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00016ACC File Offset: 0x00014CCC
	public bool isLang()
	{
		return TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00016B3C File Offset: 0x00014D3C
	public bool isMeCanAttackOtherPlayer(global::Char cAtt)
	{
		return cAtt != null && global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type != 2 && (global::Char.myCharz().myskill.template.type != 4 || cAtt.statusMe == 14 || cAtt.statusMe == 5) && ((cAtt.cTypePk == 3 && global::Char.myCharz().cTypePk == 3) || (global::Char.myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (global::Char.myCharz().cTypePk == 1 && cAtt.cTypePk == 1)) || (global::Char.myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (global::Char.myCharz().testCharId >= 0 && global::Char.myCharz().testCharId == cAtt.charID) || (global::Char.myCharz().killCharId >= 0 && global::Char.myCharz().killCharId == cAtt.charID && !this.isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == global::Char.myCharz().charID && !this.isLang()) || (global::Char.myCharz().cFlag == 8 && cAtt.cFlag != 0) || (global::Char.myCharz().cFlag != 0 && cAtt.cFlag == 8) || (global::Char.myCharz().cFlag != cAtt.cFlag && global::Char.myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14 && cAtt.statusMe != 5;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00016CE8 File Offset: 0x00014EE8
	public void clearTask()
	{
		global::Char.myCharz().taskMaint = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			bool flag = global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.type == 8;
			if (flag)
			{
				global::Char.myCharz().arrItemBag[i] = null;
			}
		}
		Npc.clearEffTask();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00016D64 File Offset: 0x00014F64
	public int getX()
	{
		return this.cx;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00016D7C File Offset: 0x00014F7C
	public int getY()
	{
		return this.cy;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00016D94 File Offset: 0x00014F94
	public int getH()
	{
		return 32;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00016DA8 File Offset: 0x00014FA8
	public int getW()
	{
		return 24;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00016DBC File Offset: 0x00014FBC
	public void focusManualTo(object objectz)
	{
		bool flag = objectz is Mob;
		if (flag)
		{
			this.mobFocus = (Mob)objectz;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
		}
		else
		{
			bool flag2 = objectz is Npc;
			if (flag2)
			{
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().deFocusNPC();
				global::Char.myCharz().npcFocus = (Npc)objectz;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
			}
			else
			{
				bool flag3 = objectz is global::Char;
				if (flag3)
				{
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().deFocusNPC();
					global::Char.myCharz().charFocus = (global::Char)objectz;
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					bool flag4 = objectz is ItemMap;
					if (flag4)
					{
						global::Char.myCharz().mobFocus = null;
						global::Char.myCharz().deFocusNPC();
						global::Char.myCharz().charFocus = null;
						global::Char.myCharz().itemFocus = (ItemMap)objectz;
					}
				}
			}
		}
		global::Char.isManualFocus = true;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00003136 File Offset: 0x00001336
	public void stopMoving()
	{
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00003136 File Offset: 0x00001336
	public void cancelAttack()
	{
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00016ED8 File Offset: 0x000150D8
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00016EEC File Offset: 0x000150EC
	public bool focusToAttack()
	{
		return this.mobFocus != null || (this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus));
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00016F20 File Offset: 0x00015120
	public void addDustEff(int type)
	{
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			bool flag2 = type == 1;
			if (flag2)
			{
				bool flag3 = this.clevel >= 9;
				if (flag3)
				{
					Effect effect = new Effect(19, this.cx - 5, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(effect);
				}
			}
			else
			{
				bool flag4 = type == 2;
				if (flag4)
				{
					bool flag5 = this.me && this.isMonkey == 1;
					if (!flag5)
					{
						bool flag6 = this.isNhapThe && GameCanvas.gameTick % 5 == 0;
						if (flag6)
						{
							Effect effect2 = new Effect(22, this.cx - 5, this.cy + 35, 2, 1, -1);
							EffecMn.addEff(effect2);
						}
					}
				}
				else
				{
					bool flag7 = type == 3 && this.clevel >= 9 && this.ySd - this.cy <= 5;
					if (flag7)
					{
						Effect effect3 = new Effect(19, this.cx - 5, this.ySd + 20, 2, 1, -1);
						EffecMn.addEff(effect3);
					}
				}
			}
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00017048 File Offset: 0x00015248
	public bool isGetFlagImage(sbyte getFlag)
	{
		bool result = true;
		for (int i = 0; i < GameScr.vFlag.size(); i++)
		{
			PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
			bool flag = pkflag != null;
			if (flag)
			{
				bool flag2 = pkflag.cflag == getFlag;
				if (flag2)
				{
					return true;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x000170B0 File Offset: 0x000152B0
	private void paintPKFlag(mGraphics g)
	{
		bool flag = this.cdir == 1;
		if (flag)
		{
			bool flag2 = this.cFlag != 0 && this.cFlag != -1;
			if (flag2)
			{
				SmallImage.drawSmallImage(g, this.flagImage, this.cx - 10, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), 2, 0);
			}
		}
		else
		{
			bool flag3 = this.cFlag != 0 && this.cFlag != -1;
			if (flag3)
			{
				SmallImage.drawSmallImage(g, this.flagImage, this.cx, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), 0, 0);
			}
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x000171A0 File Offset: 0x000153A0
	public void removeHoleEff()
	{
		bool flag = this.holder;
		if (flag)
		{
			this.holder = false;
			this.charHold = null;
			this.mobHold = null;
		}
		else
		{
			this.holdEffID = 0;
			this.charHold = null;
			this.mobHold = null;
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000171E8 File Offset: 0x000153E8
	public void removeProtectEff()
	{
		this.protectEff = false;
		this.eProtect = null;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000171F9 File Offset: 0x000153F9
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00017204 File Offset: 0x00015404
	public void removeEffect()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
		bool flag2 = this.holder;
		if (flag2)
		{
			this.holder = false;
		}
		bool flag3 = this.protectEff;
		if (flag3)
		{
			this.protectEff = false;
		}
		this.eProtect = null;
		this.charHold = null;
		this.mobHold = null;
		this.blindEff = false;
		this.sleepEff = false;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00017274 File Offset: 0x00015474
	public void setPos(short xPos, short yPos, sbyte typePos)
	{
		this.isSetPos = true;
		this.xPos = xPos;
		this.yPos = yPos;
		this.typePos = typePos;
		this.tpos = 0;
		bool flag = this.me;
		if (flag)
		{
			bool flag2 = GameCanvas.panel != null;
			if (flag2)
			{
				GameCanvas.panel.hide();
			}
			bool flag3 = GameCanvas.panel2 != null;
			if (flag3)
			{
				GameCanvas.panel2.hide();
			}
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x000172E3 File Offset: 0x000154E3
	public void removeHuytSao()
	{
		this.huytSao = false;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x000172ED File Offset: 0x000154ED
	public void fusionComplete()
	{
		this.isFusion = false;
		global::Char.isLockKey = false;
		this.tFusion = 0;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00017304 File Offset: 0x00015504
	public void setFusion(sbyte fusion)
	{
		this.tFusion = 0;
		bool flag = fusion == 4 || fusion == 5;
		if (flag)
		{
			bool flag2 = this.me;
			if (flag2)
			{
				Service.gI().funsion(fusion);
			}
			EffecMn.addEff(new Effect(34, this.cx, this.cy + 12, 2, 1, -1));
		}
		bool flag3 = fusion == 6;
		if (flag3)
		{
			EffecMn.addEff(new Effect(38, this.cx, this.cy + 12, 2, 1, -1));
		}
		bool flag4 = this.me;
		if (flag4)
		{
			GameCanvas.panel.hideNow();
			global::Char.isLockKey = true;
		}
		this.isFusion = true;
		bool flag5 = fusion == 1;
		if (flag5)
		{
			this.isNhapThe = false;
		}
		else
		{
			this.isNhapThe = true;
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000173CC File Offset: 0x000155CC
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000173D6 File Offset: 0x000155D6
	public void setPartOld()
	{
		this.headTemp = this.head;
		this.bodyTemp = this.body;
		this.legTemp = this.leg;
		this.bagTemp = this.bag;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0001740C File Offset: 0x0001560C
	public void setPartTemp(int head, int body, int leg, int bag)
	{
		bool flag = head != -1;
		if (flag)
		{
			this.head = head;
		}
		bool flag2 = body != -1;
		if (flag2)
		{
			this.body = body;
		}
		bool flag3 = leg != -1;
		if (flag3)
		{
			this.leg = leg;
		}
		bool flag4 = bag != -1;
		if (flag4)
		{
			this.bag = bag;
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0001746C File Offset: 0x0001566C
	public void resetPartTemp()
	{
		bool flag = this.headTemp != -1;
		if (flag)
		{
			this.head = this.headTemp;
			this.headTemp = -1;
		}
		bool flag2 = this.bodyTemp != -1;
		if (flag2)
		{
			this.body = this.bodyTemp;
			this.bodyTemp = -1;
		}
		bool flag3 = this.legTemp != -1;
		if (flag3)
		{
			this.leg = this.legTemp;
			this.legTemp = -1;
		}
		bool flag4 = this.bagTemp != -1;
		if (flag4)
		{
			this.bag = this.bagTemp;
			this.bagTemp = -1;
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00017510 File Offset: 0x00015710
	public Effect getEffById(int id)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			bool flag = effect.effId == id;
			if (flag)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00017565 File Offset: 0x00015765
	public void addEffChar(Effect e)
	{
		this.removeEffChar(0, e.effId);
		this.vEffChar.addElement(e);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00017584 File Offset: 0x00015784
	public void removeEffChar(int type, int id)
	{
		bool flag = type == -1;
		if (flag)
		{
			this.vEffChar.removeAllElements();
		}
		else
		{
			bool flag2 = this.getEffById(id) != null;
			if (flag2)
			{
				this.vEffChar.removeElement(this.getEffById(id));
			}
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x000175D0 File Offset: 0x000157D0
	public void paintEffBehind(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			bool flag2 = effect.layer == 0;
			if (flag2)
			{
				bool flag = true;
				bool flag3 = effect.isStand == 0;
				if (flag3)
				{
					flag = (this.statusMe == 1 || this.statusMe == 6);
				}
				bool flag4 = flag;
				if (flag4)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00017658 File Offset: 0x00015858
	public void paintEffFront(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			bool flag2 = effect.layer == 1;
			if (flag2)
			{
				bool flag = true;
				bool flag3 = effect.isStand == 0;
				if (flag3)
				{
					flag = (this.statusMe == 1 || this.statusMe == 6);
				}
				bool flag4 = flag;
				if (flag4)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x000176E0 File Offset: 0x000158E0
	public void updEffChar()
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			((Effect)this.vEffChar.elementAt(i)).update();
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00017724 File Offset: 0x00015924
	public int checkLuong()
	{
		return this.luong + this.luongKhoa;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00017744 File Offset: 0x00015944
	public void updateEye()
	{
		bool flag = this.head == 934;
		if (flag)
		{
			bool flag2 = GameCanvas.timeNow - this.timeAddChopmat > 0L;
			if (flag2)
			{
				this.fChopmat++;
				bool flag3 = this.fChopmat > this.frEye.Length - 1;
				if (flag3)
				{
					this.fChopmat = 0;
					this.timeAddChopmat = GameCanvas.timeNow + (long)Res.random(2000, 3500);
					this.frEye = this.frChopCham;
					bool flag4 = Res.random(2) == 0;
					if (flag4)
					{
						this.frEye = this.frChopNhanh;
					}
				}
			}
			else
			{
				this.fChopmat = 0;
			}
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000177FC File Offset: 0x000159FC
	private void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
	{
		bool flag = this.head == 934 && (this.statusMe == 1 || this.statusMe == 6);
		if (flag)
		{
			bool flag2 = global::Char.fraRedEye == null || global::Char.fraRedEye.imgFrame == null;
			if (flag2)
			{
				Image img = mSystem.loadImage("/redeye.png");
				global::Char.fraRedEye = new FrameImage(img, 14, 10);
			}
			else
			{
				bool flag3 = this.frEye[this.fChopmat] != -1;
				if (flag3)
				{
					int num = 8;
					int num2 = 15;
					bool flag4 = trans == 2;
					if (flag4)
					{
						num = -8;
					}
					global::Char.fraRedEye.drawFrame(this.frEye[this.fChopmat], xx + num, yy + num2, trans, anchor, g);
				}
			}
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000178C8 File Offset: 0x00015AC8
	public bool isHead_2Fr(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			bool flag = global::Char.Arr_Head_2Fr[i][0] == idHead;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00017908 File Offset: 0x00015B08
	private void updateFHead()
	{
		bool flag = this.isHead_2Fr(this.head);
		if (flag)
		{
			this.fHead++;
			bool flag2 = this.fHead > 10000;
			if (flag2)
			{
				this.fHead = 0;
			}
		}
		else
		{
			this.fHead = 0;
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0001795C File Offset: 0x00015B5C
	private int getFHead(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			bool flag = global::Char.Arr_Head_2Fr[i][0] == idHead;
			if (flag)
			{
				return global::Char.Arr_Head_2Fr[i][this.fHead / 4 % global::Char.Arr_Head_2Fr[i].Length];
			}
		}
		return idHead;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000179B8 File Offset: 0x00015BB8
	public void paintAuraBehind(mGraphics g)
	{
		bool flag = this.me && global::Char.isPaintAura;
		if (!flag)
		{
			bool flag2 = this.idAuraEff <= -1;
			if (!flag2)
			{
				bool flag3 = (this.statusMe == 1 || this.statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - this.timeBlue > 0L;
				if (flag3)
				{
					string nameImg = this.strEffAura + this.idAuraEff.ToString() + "_0";
					FrameImage fraImage = mSystem.getFraImage(nameImg);
					bool flag4 = fraImage != null;
					if (flag4)
					{
						fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
				}
			}
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00017A9C File Offset: 0x00015C9C
	public void paintAuraFront(mGraphics g)
	{
		bool flag2 = this.me && !global::Char.isPaintAura;
		if (!flag2)
		{
			bool flag3 = this.idAuraEff <= -1;
			if (!flag3)
			{
				bool flag4 = this.statusMe == 1 || this.statusMe == 6;
				if (flag4)
				{
					bool flag5 = !GameCanvas.panel.isShow && !GameCanvas.lowGraphic;
					if (flag5)
					{
						bool flag = false;
						bool flag6 = mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1;
						if (flag6)
						{
							flag = true;
							this.IsAddDust1 = false;
						}
						bool flag7 = mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2;
						if (flag7)
						{
							flag = true;
							this.IsAddDust2 = false;
						}
						bool flag8 = flag;
						if (flag8)
						{
							GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
							GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
							this.addDustEff(1);
						}
						bool flag9 = mSystem.currentTimeMillis() - this.timeBlue > 0L;
						if (flag9)
						{
							string nameImg = this.strEffAura + this.idAuraEff.ToString() + "_1";
							FrameImage fraImage = mSystem.getFraImage(nameImg);
							bool flag10 = fraImage != null;
							if (flag10)
							{
								fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy + 2, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
							}
						}
					}
				}
				else
				{
					this.timeBlue = mSystem.currentTimeMillis() + 1500L;
					this.IsAddDust1 = true;
					this.IsAddDust2 = true;
				}
			}
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00017C70 File Offset: 0x00015E70
	public void paintEff_Lvup_behind(mGraphics g)
	{
		bool flag = this.idEff_Set_Item == -1;
		if (!flag)
		{
			bool flag2 = this.fraEff != null;
			if (flag2)
			{
				this.fraEff.drawFrame(GameCanvas.gameTick / 4 % this.fraEff.nFrame, this.cx, this.cy + 3, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			else
			{
				this.fraEff = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item.ToString() + "_0");
			}
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00017D10 File Offset: 0x00015F10
	public void paintEff_Lvup_front(mGraphics g)
	{
		bool flag = this.idEff_Set_Item == -1;
		if (!flag)
		{
			bool flag2 = this.fraEffSub != null;
			if (flag2)
			{
				this.fraEffSub.drawFrame(GameCanvas.gameTick / 4 % this.fraEffSub.nFrame, this.cx, this.cy + 8, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			else
			{
				this.fraEffSub = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item.ToString() + "_1");
			}
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00017DB0 File Offset: 0x00015FB0
	public void paintHat_behind(mGraphics g, int cf, int yh)
	{
		try
		{
			bool flag = this.idHat != -1;
			if (flag)
			{
				bool flag2 = this.isFrNgang(cf);
				if (flag2)
				{
					bool flag3 = this.fraHat_behind_2 != null;
					if (flag3)
					{
						this.fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind_2.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_behind_2 = mSystem.getFraImage(this.strHat_behind + this.strNgang + this.idHat.ToString());
					}
				}
				else
				{
					bool flag4 = this.fraHat_behind != null;
					if (flag4)
					{
						this.fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_behind = mSystem.getFraImage(this.strHat_behind + this.idHat.ToString());
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00017F40 File Offset: 0x00016140
	public void paintHat_front(mGraphics g, int cf, int yh)
	{
		try
		{
			bool flag = this.idHat != -1;
			if (flag)
			{
				bool flag2 = this.isFrNgang(cf);
				if (flag2)
				{
					bool flag3 = this.fraHat_font_2 != null;
					if (flag3)
					{
						this.fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font_2.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_font_2 = mSystem.getFraImage(this.strHat_font + this.strNgang + this.idHat.ToString());
					}
				}
				else
				{
					bool flag4 = this.fraHat_font != null;
					if (flag4)
					{
						this.fraHat_font.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir != 1) ? -1 : 1), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_font = mSystem.getFraImage(this.strHat_font + this.idHat.ToString());
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000180D0 File Offset: 0x000162D0
	public bool isFrNgang(int fr)
	{
		return fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0001812C File Offset: 0x0001632C
	public void sendNewAttack(short idTemplateSkill)
	{
		short x = -1;
		short y = -1;
		bool flag = this.mobFocus != null;
		if (flag)
		{
			x = (short)this.mobFocus.x;
			y = (short)this.mobFocus.y;
		}
		bool flag2 = this.charFocus != null && !this.charFocus.isPet && !this.charFocus.isMiniPet;
		if (flag2)
		{
			x = (short)this.charFocus.cx;
			y = (short)this.charFocus.cy;
		}
		Service.gI().new_skill_not_focus((sbyte)idTemplateSkill, (sbyte)this.cdir, x, y);
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000181C4 File Offset: 0x000163C4
	public void SetSkillPaint_NEW(short idskillPaint, bool isFly, sbyte typeFrame, sbyte typePaint, sbyte dir, short timeGong, sbyte typeItem)
	{
		this.isPaintNewSkill = true;
		this.timeReset_newSkill = GameCanvas.timeNow + 10000L;
		this.idskillPaint = idskillPaint;
		this.isFly = isFly;
		this.typeFrame = typeFrame;
		this.typePaint = typePaint;
		this.typeItem = typeItem;
		this.cdir = (int)dir;
		this.count_NEW = 0;
		this.stt = 0;
		long lastTimeUseThisSkill = mSystem.currentTimeMillis();
		bool flag = this.me;
		if (flag)
		{
			this.saveLoadPreviousSkill();
			this.myskill.lastTimeUseThisSkill = lastTimeUseThisSkill;
			bool flag2 = this.myskill.template.manaUseType == 2;
			if (flag2)
			{
				this.cMP = 1;
			}
			else
			{
				bool flag3 = this.myskill.template.manaUseType != 1;
				if (flag3)
				{
					this.cMP -= this.myskill.manaUse;
				}
				else
				{
					this.cMP -= this.myskill.manaUse * this.cMPFull / 100;
				}
			}
			global::Char.myCharz().cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0;
			bool flag4 = this.cMP < 0;
			if (flag4)
			{
				this.cMP = 0;
			}
		}
		bool flag5 = idskillPaint == 24;
		if (flag5)
		{
			GameScr.addEffectEnd_Target(18, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(21, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		else
		{
			bool flag6 = idskillPaint == 25;
			if (flag6)
			{
				GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
				GameScr.addEffectEnd_Target(22, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
			}
			else
			{
				bool flag7 = idskillPaint == 26;
				if (flag7)
				{
					GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
					GameScr.addEffectEnd_Target(23, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
				}
			}
		}
		bool flag8 = this.typeFrame == 1;
		if (flag8)
		{
			bool flag9 = !this.isFly;
			if (flag9)
			{
				this.fr_start = new byte[]
				{
					20,
					20,
					20,
					20,
					20,
					20,
					19
				};
				this.fr_atk = new byte[]
				{
					20
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					31,
					31,
					31,
					31,
					31,
					31,
					30
				};
				this.fr_atk = new byte[]
				{
					31
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
		bool flag10 = this.typeFrame == 2;
		if (flag10)
		{
			bool flag11 = !this.isFly;
			if (flag11)
			{
				this.fr_start = new byte[]
				{
					20
				};
				this.fr_atk = new byte[]
				{
					13,
					13,
					13,
					14,
					14,
					14
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					31
				};
				this.fr_atk = new byte[]
				{
					26,
					26,
					26,
					27,
					27,
					27
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
		bool flag12 = this.typeFrame == 4;
		if (flag12)
		{
			bool flag13 = !this.isFly;
			if (flag13)
			{
				this.fr_start = new byte[]
				{
					17,
					17,
					17,
					18,
					18,
					18
				};
				this.fr_atk = new byte[]
				{
					18
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					7,
					7,
					7,
					12,
					12,
					12,
					12
				};
				this.fr_atk = new byte[]
				{
					12
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
		bool flag14 = this.typeFrame == 3;
		if (flag14)
		{
			bool flag15 = !this.isFly;
			if (flag15)
			{
				this.fr_start = new byte[]
				{
					24,
					24,
					24,
					17,
					17,
					17,
					18,
					18,
					18
				};
				this.fr_atk = new byte[]
				{
					20
				};
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[]
				{
					23,
					23,
					23,
					7,
					7,
					7,
					12,
					12,
					12,
					12
				};
				this.fr_atk = new byte[]
				{
					31
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00018614 File Offset: 0x00016814
	public void SetSkillPaint_STT(int stt, short idskillPaint, Point targetDame, short timeDame, short rangeDame, sbyte typePaint, Point[] listObj, sbyte typeItem)
	{
		this.stt = stt;
		this.idskillPaint = idskillPaint;
		this.count_NEW = 0;
		this.targetDame = targetDame;
		this.typePaint = typePaint;
		this.timeDame = mSystem.currentTimeMillis() + (long)timeDame;
		this.rangeDame = rangeDame;
		this.typeItem = typeItem;
		bool flag = this.stt != 1;
		if (!flag)
		{
			bool flag2 = this.idskillPaint == 24;
			if (flag2)
			{
				GameScr.addEffectEnd_Target(18, 1, (int)typePaint, this, null, 3, timeDame, 0);
				GameScr.addEffectEnd_Target(24, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame);
			}
			bool flag3 = this.idskillPaint == 25;
			if (flag3)
			{
				GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this, null, 3, timeDame, 0);
				GameScr.addEffectEnd_Target(25, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame);
			}
			bool flag4 = this.idskillPaint == 26;
			if (flag4)
			{
				GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this, null, 3, timeDame, 0);
				GameScr.addEffectEnd(26, (int)typeItem, (int)typePaint, targetDame.x, targetDame.y, 1, 0, timeDame, listObj);
			}
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0001872C File Offset: 0x0001692C
	public void UpdSkillPaint_NEW()
	{
		bool flag = this.stt == 0;
		if (flag)
		{
			bool flag2 = this.isFly && this.count_NEW < 20;
			if (flag2)
			{
				this.cvy = -3;
				this.cy += this.cvy;
			}
			bool flag3 = this.fr_start.Length == 1;
			if (flag3)
			{
				this.cf = (int)this.fr_start[0];
			}
			else
			{
				bool flag4 = this.count_NEW > this.fr_start.Length - 1;
				if (flag4)
				{
					this.cf = (int)this.fr_start[this.fr_start.Length - 1];
				}
				else
				{
					this.cf = (int)this.fr_start[this.count_NEW];
				}
			}
		}
		else
		{
			bool flag5 = this.stt == 1;
			if (flag5)
			{
				this.cf = (int)this.fr_atk[this.count_NEW % this.fr_atk.Length];
				bool flag6 = mSystem.currentTimeMillis() - this.timeDame > 0L;
				if (flag6)
				{
					this.SetSkillPaint_STT(2, 0, null, 0, 0, 0, null, 0);
				}
				bool flag7 = this.count_NEW % 5 == 0;
				if (flag7)
				{
					GameScr.shock_scr = 5;
				}
				bool flag8 = this.typeFrame == 1 && this.count_NEW < 10 && !TileMap.tileTypeAt(this.cx - (this.chw + 1) * this.cdir, this.cy, (this.cdir != 1) ? 4 : 8);
				if (flag8)
				{
					this.cx -= this.cdir;
				}
				bool flag9 = this.typeFrame == 2;
				if (flag9)
				{
				}
			}
			else
			{
				bool flag10 = this.stt == 2;
				if (flag10)
				{
					bool flag11 = this.fr_end.Length == 1;
					if (flag11)
					{
						this.cf = (int)this.fr_end[0];
					}
					else
					{
						bool flag12 = this.count_NEW > this.fr_end.Length - 1;
						if (flag12)
						{
							this.cf = (int)this.fr_end[this.fr_end.Length - 1];
						}
						else
						{
							this.cf = (int)this.fr_end[this.count_NEW];
						}
					}
					bool flag13 = this.isFly;
					if (flag13)
					{
						this.cvx = (this.cvy = 0);
						this.statusMe = 4;
					}
					this.isPaintNewSkill = false;
				}
			}
		}
		this.count_NEW++;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00018994 File Offset: 0x00016B94
	public global::Char clone()
	{
		global::Char @char = new global::Char();
		@char.charID = this.charID;
		@char.cx = this.cx;
		@char.cy = this.cy;
		@char.cdir = this.cdir;
		bool flag = this.arrItemBody != null;
		if (flag)
		{
			@char.arrItemBody = new Item[this.arrItemBody.Length];
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				bool flag2 = this.arrItemBody[i] == null;
				if (flag2)
				{
					@char.arrItemBody[i] = null;
				}
				else
				{
					@char.arrItemBody[i] = this.arrItemBody[i].clone();
				}
			}
		}
		return @char;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00018A50 File Offset: 0x00016C50
	public bool containsCaiTrang(int v)
	{
		bool flag = this.arrItemBody != null;
		if (flag)
		{
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				bool flag2 = this.arrItemBody[i] != null && this.arrItemBody[i].template != null;
				if (flag2)
				{
					bool flag3 = (int)this.arrItemBody[i].template.id == v;
					if (flag3)
					{
						return true;
					}
				}
			}
		}
		Res.err("tim kiem id cai trang " + v.ToString() + " ko tim thay");
		return false;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00018AF0 File Offset: 0x00016CF0
	public void printlog()
	{
		string text = string.Empty;
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isInjure,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isMonkey,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isAddChopMat,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isAttack,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isAttFly,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			global::Char.ischangingMap,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCharge,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCopy,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCreateDark,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isCrit,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDirtyPostion,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isEndMount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isEventMount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isMafuba,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isFusion,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isFeetEff,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isFlying,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isWaitMonkey,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isUseSkillSpec(),
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"isInjure ",
			this.isDie,
			"\n"
		});
		Res.outz(text);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00018F5C File Offset: 0x0001715C
	public void setDanhHieu(int smallDanhHieu, int frame)
	{
		bool flag = this.mainImg == null;
		if (flag)
		{
			this.mainImg = ImgByName.getImagePath("banner_" + 0.ToString(), ImgByName.hashImagePath);
		}
		bool flag2 = this.mainImg.img != null;
		if (flag2)
		{
			int num = this.mainImg.img.getHeight() / (int)this.mainImg.nFrame;
			bool flag3 = num < 1;
			if (flag3)
			{
				num = 1;
			}
			this.fraDanhHieu = new FrameImage(this.mainImg.img, this.mainImg.img.getWidth(), num);
		}
		Res.err("===== tim thay DanhHieu ve danh hieu ra");
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00019010 File Offset: 0x00017210
	static Char()
	{
		int[][] array = new int[32][];
		array[0] = new int[]
		{
			5,
			-7
		};
		array[1] = new int[]
		{
			5,
			-7
		};
		array[2] = new int[]
		{
			5,
			-8
		};
		array[3] = new int[]
		{
			5,
			-7
		};
		array[4] = new int[]
		{
			5,
			-6
		};
		array[5] = new int[]
		{
			5,
			-8
		};
		array[6] = new int[]
		{
			5,
			-7
		};
		int num = 7;
		int[] array2 = new int[2];
		array2[0] = 9;
		array[num] = array2;
		array[8] = new int[]
		{
			11,
			1
		};
		int num2 = 9;
		int[] array3 = new int[2];
		array3[0] = 4;
		array[num2] = array3;
		array[10] = new int[]
		{
			4,
			-1
		};
		array[11] = new int[]
		{
			4,
			8
		};
		array[12] = new int[]
		{
			6,
			5
		};
		array[13] = new int[]
		{
			6,
			-6
		};
		array[14] = new int[]
		{
			2,
			-5
		};
		array[15] = new int[]
		{
			7,
			-8
		};
		array[16] = new int[]
		{
			7,
			-6
		};
		int num3 = 17;
		int[] array4 = new int[2];
		array4[0] = 8;
		array[num3] = array4;
		array[18] = new int[]
		{
			7,
			5
		};
		array[19] = new int[]
		{
			9,
			-7
		};
		array[20] = new int[]
		{
			7,
			-3
		};
		array[21] = new int[]
		{
			2,
			8
		};
		array[22] = new int[]
		{
			4,
			5
		};
		array[23] = new int[]
		{
			10,
			-5
		};
		array[24] = new int[]
		{
			9,
			-5
		};
		array[25] = new int[]
		{
			9,
			-5
		};
		array[26] = new int[]
		{
			6,
			-6
		};
		array[27] = new int[]
		{
			2,
			-5
		};
		array[28] = new int[]
		{
			7,
			-8
		};
		array[29] = new int[]
		{
			7,
			-6
		};
		array[30] = new int[]
		{
			9,
			-7
		};
		array[31] = new int[]
		{
			7,
			-3
		};
		global::Char.hatInfo = array;
	}

	// Token: 0x040000E7 RID: 231
	public string xuStr;

	// Token: 0x040000E8 RID: 232
	public string luongStr;

	// Token: 0x040000E9 RID: 233
	public string luongKhoaStr;

	// Token: 0x040000EA RID: 234
	public long lastUpdateTime;

	// Token: 0x040000EB RID: 235
	public bool meLive;

	// Token: 0x040000EC RID: 236
	public bool isMask;

	// Token: 0x040000ED RID: 237
	public bool isTeleport;

	// Token: 0x040000EE RID: 238
	public bool isUsePlane;

	// Token: 0x040000EF RID: 239
	public int shadowX;

	// Token: 0x040000F0 RID: 240
	public int shadowY;

	// Token: 0x040000F1 RID: 241
	public int shadowLife;

	// Token: 0x040000F2 RID: 242
	public bool isNhapThe;

	// Token: 0x040000F3 RID: 243
	public PetFollow petFollow;

	// Token: 0x040000F4 RID: 244
	public int rank;

	// Token: 0x040000F5 RID: 245
	public const sbyte A_STAND = 1;

	// Token: 0x040000F6 RID: 246
	public const sbyte A_RUN = 2;

	// Token: 0x040000F7 RID: 247
	public const sbyte A_JUMP = 3;

	// Token: 0x040000F8 RID: 248
	public const sbyte A_FALL = 4;

	// Token: 0x040000F9 RID: 249
	public const sbyte A_DEADFLY = 5;

	// Token: 0x040000FA RID: 250
	public const sbyte A_NOTHING = 6;

	// Token: 0x040000FB RID: 251
	public const sbyte A_ATTK = 7;

	// Token: 0x040000FC RID: 252
	public const sbyte A_INJURE = 8;

	// Token: 0x040000FD RID: 253
	public const sbyte A_AUTOJUMP = 9;

	// Token: 0x040000FE RID: 254
	public const sbyte A_FLY = 10;

	// Token: 0x040000FF RID: 255
	public const sbyte SKILL_STAND = 12;

	// Token: 0x04000100 RID: 256
	public const sbyte SKILL_FALL = 13;

	// Token: 0x04000101 RID: 257
	public const sbyte A_DEAD = 14;

	// Token: 0x04000102 RID: 258
	public const sbyte A_HIDE = 15;

	// Token: 0x04000103 RID: 259
	public const sbyte A_RESETPOINT = 16;

	// Token: 0x04000104 RID: 260
	public static ChatPopup chatPopup;

	// Token: 0x04000105 RID: 261
	public long cPower;

	// Token: 0x04000106 RID: 262
	public Info chatInfo;

	// Token: 0x04000107 RID: 263
	public sbyte petStatus;

	// Token: 0x04000108 RID: 264
	public int cx = 24;

	// Token: 0x04000109 RID: 265
	public int cy = 24;

	// Token: 0x0400010A RID: 266
	public int cvx;

	// Token: 0x0400010B RID: 267
	public int cvy;

	// Token: 0x0400010C RID: 268
	public int cp1;

	// Token: 0x0400010D RID: 269
	public int cp2;

	// Token: 0x0400010E RID: 270
	public int cp3;

	// Token: 0x0400010F RID: 271
	public int statusMe = 5;

	// Token: 0x04000110 RID: 272
	public int cdir = 1;

	// Token: 0x04000111 RID: 273
	public int charID;

	// Token: 0x04000112 RID: 274
	public int cgender;

	// Token: 0x04000113 RID: 275
	public int ctaskId;

	// Token: 0x04000114 RID: 276
	public int menuSelect;

	// Token: 0x04000115 RID: 277
	public int cBonusSpeed;

	// Token: 0x04000116 RID: 278
	public int cspeed = 4;

	// Token: 0x04000117 RID: 279
	public int ccurrentAttack;

	// Token: 0x04000118 RID: 280
	public int cDamFull;

	// Token: 0x04000119 RID: 281
	public int cDefull;

	// Token: 0x0400011A RID: 282
	public int cCriticalFull;

	// Token: 0x0400011B RID: 283
	public int clevel;

	// Token: 0x0400011C RID: 284
	public int cMP;

	// Token: 0x0400011D RID: 285
	public int cHP;

	// Token: 0x0400011E RID: 286
	public int cHPNew;

	// Token: 0x0400011F RID: 287
	public int cMaxEXP;

	// Token: 0x04000120 RID: 288
	public int cHPShow;

	// Token: 0x04000121 RID: 289
	public int xReload;

	// Token: 0x04000122 RID: 290
	public int yReload;

	// Token: 0x04000123 RID: 291
	public int cyStartFall;

	// Token: 0x04000124 RID: 292
	public int saveStatus;

	// Token: 0x04000125 RID: 293
	public int eff5BuffHp;

	// Token: 0x04000126 RID: 294
	public int eff5BuffMp;

	// Token: 0x04000127 RID: 295
	public int cHPFull;

	// Token: 0x04000128 RID: 296
	public int cMPFull;

	// Token: 0x04000129 RID: 297
	public int cdameDown;

	// Token: 0x0400012A RID: 298
	public int cStr;

	// Token: 0x0400012B RID: 299
	public long cLevelPercent;

	// Token: 0x0400012C RID: 300
	public long cTiemNang;

	// Token: 0x0400012D RID: 301
	public long cNangdong;

	// Token: 0x0400012E RID: 302
	public int damHP;

	// Token: 0x0400012F RID: 303
	public int damMP;

	// Token: 0x04000130 RID: 304
	public bool isMob;

	// Token: 0x04000131 RID: 305
	public bool isCrit;

	// Token: 0x04000132 RID: 306
	public bool isDie;

	// Token: 0x04000133 RID: 307
	public int pointUydanh;

	// Token: 0x04000134 RID: 308
	public int pointNon;

	// Token: 0x04000135 RID: 309
	public int pointVukhi;

	// Token: 0x04000136 RID: 310
	public int pointAo;

	// Token: 0x04000137 RID: 311
	public int pointLien;

	// Token: 0x04000138 RID: 312
	public int pointGangtay;

	// Token: 0x04000139 RID: 313
	public int pointNhan;

	// Token: 0x0400013A RID: 314
	public int pointQuan;

	// Token: 0x0400013B RID: 315
	public int pointNgocboi;

	// Token: 0x0400013C RID: 316
	public int pointGiay;

	// Token: 0x0400013D RID: 317
	public int pointPhu;

	// Token: 0x0400013E RID: 318
	public int countFinishDay;

	// Token: 0x0400013F RID: 319
	public int countLoopBoos;

	// Token: 0x04000140 RID: 320
	public int limitTiemnangso;

	// Token: 0x04000141 RID: 321
	public int limitKynangso;

	// Token: 0x04000142 RID: 322
	public short[] potential = new short[4];

	// Token: 0x04000143 RID: 323
	public string cName = string.Empty;

	// Token: 0x04000144 RID: 324
	public int clanID;

	// Token: 0x04000145 RID: 325
	public sbyte ctypeClan;

	// Token: 0x04000146 RID: 326
	public Clan clan;

	// Token: 0x04000147 RID: 327
	public sbyte role;

	// Token: 0x04000148 RID: 328
	public int cw = 22;

	// Token: 0x04000149 RID: 329
	public int ch = 32;

	// Token: 0x0400014A RID: 330
	public int chw = 11;

	// Token: 0x0400014B RID: 331
	public int chh = 16;

	// Token: 0x0400014C RID: 332
	public Command cmdMenu;

	// Token: 0x0400014D RID: 333
	public bool canFly = true;

	// Token: 0x0400014E RID: 334
	public bool cmtoChar;

	// Token: 0x0400014F RID: 335
	public bool me;

	// Token: 0x04000150 RID: 336
	public bool cFinishedAttack;

	// Token: 0x04000151 RID: 337
	public bool cchistlast;

	// Token: 0x04000152 RID: 338
	public bool isAttack;

	// Token: 0x04000153 RID: 339
	public bool isAttFly;

	// Token: 0x04000154 RID: 340
	public int cwpt;

	// Token: 0x04000155 RID: 341
	public int cwplv;

	// Token: 0x04000156 RID: 342
	public int cf;

	// Token: 0x04000157 RID: 343
	public int tick;

	// Token: 0x04000158 RID: 344
	public static bool fallAttack;

	// Token: 0x04000159 RID: 345
	public bool isJump;

	// Token: 0x0400015A RID: 346
	public bool autoFall;

	// Token: 0x0400015B RID: 347
	public bool attack = true;

	// Token: 0x0400015C RID: 348
	public long xu;

	// Token: 0x0400015D RID: 349
	public int xuInBox;

	// Token: 0x0400015E RID: 350
	public int yen;

	// Token: 0x0400015F RID: 351
	public int gold_lock;

	// Token: 0x04000160 RID: 352
	public int luong;

	// Token: 0x04000161 RID: 353
	public int luongKhoa;

	// Token: 0x04000162 RID: 354
	public NClass nClass;

	// Token: 0x04000163 RID: 355
	public Command endMovePointCommand;

	// Token: 0x04000164 RID: 356
	public MyVector vSkill = new MyVector();

	// Token: 0x04000165 RID: 357
	public MyVector vSkillFight = new MyVector();

	// Token: 0x04000166 RID: 358
	public MyVector vEff = new MyVector();

	// Token: 0x04000167 RID: 359
	public Skill myskill;

	// Token: 0x04000168 RID: 360
	public Task taskMaint;

	// Token: 0x04000169 RID: 361
	public bool paintName = true;

	// Token: 0x0400016A RID: 362
	public Archivement[] arrArchive;

	// Token: 0x0400016B RID: 363
	public Item[] arrItemBag;

	// Token: 0x0400016C RID: 364
	public Item[] arrItemBox;

	// Token: 0x0400016D RID: 365
	public Item[] arrItemBody;

	// Token: 0x0400016E RID: 366
	public Skill[] arrPetSkill;

	// Token: 0x0400016F RID: 367
	public Item[][] arrItemShop;

	// Token: 0x04000170 RID: 368
	public string[][] infoSpeacialSkill;

	// Token: 0x04000171 RID: 369
	public short[][] imgSpeacialSkill;

	// Token: 0x04000172 RID: 370
	public short cResFire;

	// Token: 0x04000173 RID: 371
	public short cResIce;

	// Token: 0x04000174 RID: 372
	public short cResWind;

	// Token: 0x04000175 RID: 373
	public short cMiss;

	// Token: 0x04000176 RID: 374
	public short cExactly;

	// Token: 0x04000177 RID: 375
	public short cFatal;

	// Token: 0x04000178 RID: 376
	public sbyte cPk;

	// Token: 0x04000179 RID: 377
	public sbyte cTypePk;

	// Token: 0x0400017A RID: 378
	public short cReactDame;

	// Token: 0x0400017B RID: 379
	public short sysUp;

	// Token: 0x0400017C RID: 380
	public short sysDown;

	// Token: 0x0400017D RID: 381
	public int avatar;

	// Token: 0x0400017E RID: 382
	public int skillTemplateId;

	// Token: 0x0400017F RID: 383
	public Mob mobFocus;

	// Token: 0x04000180 RID: 384
	public Mob mobMe;

	// Token: 0x04000181 RID: 385
	public int tMobMeBorn;

	// Token: 0x04000182 RID: 386
	public Npc npcFocus;

	// Token: 0x04000183 RID: 387
	public global::Char charFocus;

	// Token: 0x04000184 RID: 388
	public ItemMap itemFocus;

	// Token: 0x04000185 RID: 389
	public MyVector focus = new MyVector();

	// Token: 0x04000186 RID: 390
	public Mob[] attMobs;

	// Token: 0x04000187 RID: 391
	public global::Char[] attChars;

	// Token: 0x04000188 RID: 392
	public short[] moveFast;

	// Token: 0x04000189 RID: 393
	public int testCharId = -9999;

	// Token: 0x0400018A RID: 394
	public int killCharId = -9999;

	// Token: 0x0400018B RID: 395
	public sbyte resultTest;

	// Token: 0x0400018C RID: 396
	public int countKill;

	// Token: 0x0400018D RID: 397
	public int countKillMax;

	// Token: 0x0400018E RID: 398
	public bool isInvisiblez;

	// Token: 0x0400018F RID: 399
	public bool isShadown = true;

	// Token: 0x04000190 RID: 400
	public const sbyte PK_NORMAL = 0;

	// Token: 0x04000191 RID: 401
	public const sbyte PK_PHE = 1;

	// Token: 0x04000192 RID: 402
	public const sbyte PK_BANG = 2;

	// Token: 0x04000193 RID: 403
	public const sbyte PK_THIDAU = 3;

	// Token: 0x04000194 RID: 404
	public const sbyte PK_LUYENTAP = 4;

	// Token: 0x04000195 RID: 405
	public const sbyte PK_TUDO = 5;

	// Token: 0x04000196 RID: 406
	public MyVector taskOrders = new MyVector();

	// Token: 0x04000197 RID: 407
	public int cStamina;

	// Token: 0x04000198 RID: 408
	public static short[] idHead;

	// Token: 0x04000199 RID: 409
	public static short[] idAvatar;

	// Token: 0x0400019A RID: 410
	public int exp;

	// Token: 0x0400019B RID: 411
	public string[] strLevel;

	// Token: 0x0400019C RID: 412
	public string currStrLevel;

	// Token: 0x0400019D RID: 413
	public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

	// Token: 0x0400019E RID: 414
	public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

	// Token: 0x0400019F RID: 415
	public bool isFreez;

	// Token: 0x040001A0 RID: 416
	public bool isCharge;

	// Token: 0x040001A1 RID: 417
	public int seconds;

	// Token: 0x040001A2 RID: 418
	public int freezSeconds;

	// Token: 0x040001A3 RID: 419
	public long last;

	// Token: 0x040001A4 RID: 420
	public long cur;

	// Token: 0x040001A5 RID: 421
	public long lastFreez;

	// Token: 0x040001A6 RID: 422
	public long currFreez;

	// Token: 0x040001A7 RID: 423
	public bool isFlyUp;

	// Token: 0x040001A8 RID: 424
	public static MyVector vItemTime = new MyVector();

	// Token: 0x040001A9 RID: 425
	public static short ID_NEW_MOUNT = 30000;

	// Token: 0x040001AA RID: 426
	public short idMount;

	// Token: 0x040001AB RID: 427
	public bool isHaveMount;

	// Token: 0x040001AC RID: 428
	public bool isMountVip;

	// Token: 0x040001AD RID: 429
	public bool isEventMount;

	// Token: 0x040001AE RID: 430
	public bool isSpeacialMount;

	// Token: 0x040001AF RID: 431
	public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

	// Token: 0x040001B0 RID: 432
	public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

	// Token: 0x040001B1 RID: 433
	public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

	// Token: 0x040001B2 RID: 434
	public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

	// Token: 0x040001B3 RID: 435
	public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

	// Token: 0x040001B4 RID: 436
	public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

	// Token: 0x040001B5 RID: 437
	public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

	// Token: 0x040001B6 RID: 438
	public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

	// Token: 0x040001B7 RID: 439
	public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

	// Token: 0x040001B8 RID: 440
	public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

	// Token: 0x040001B9 RID: 441
	public sbyte[] FrameMount = new sbyte[]
	{
		0,
		0,
		1,
		1,
		2,
		2,
		1,
		1
	};

	// Token: 0x040001BA RID: 442
	public int frameMount;

	// Token: 0x040001BB RID: 443
	public int frameNewMount;

	// Token: 0x040001BC RID: 444
	public int transMount;

	// Token: 0x040001BD RID: 445
	public int genderMount;

	// Token: 0x040001BE RID: 446
	public int idcharMount;

	// Token: 0x040001BF RID: 447
	public int xMount;

	// Token: 0x040001C0 RID: 448
	public int yMount;

	// Token: 0x040001C1 RID: 449
	public int dxMount;

	// Token: 0x040001C2 RID: 450
	public int dyMount;

	// Token: 0x040001C3 RID: 451
	public int xChar;

	// Token: 0x040001C4 RID: 452
	public int xdis;

	// Token: 0x040001C5 RID: 453
	public int speedMount;

	// Token: 0x040001C6 RID: 454
	public bool isStartMount;

	// Token: 0x040001C7 RID: 455
	public bool isMount;

	// Token: 0x040001C8 RID: 456
	public bool isEndMount;

	// Token: 0x040001C9 RID: 457
	public sbyte cFlag;

	// Token: 0x040001CA RID: 458
	public int flagImage;

	// Token: 0x040001CB RID: 459
	public short x_hint;

	// Token: 0x040001CC RID: 460
	public short y_hint;

	// Token: 0x040001CD RID: 461
	public short s_danhHieu1;

	// Token: 0x040001CE RID: 462
	public static int[][][] CharInfo = new int[][][]
	{
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				16
			},
			new int[]
			{
				1,
				-9,
				45
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				35
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				17
			},
			new int[]
			{
				1,
				-9,
				46
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				2,
				-10,
				11
			},
			new int[]
			{
				2,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				32
			},
			new int[]
			{
				3,
				-12,
				10
			},
			new int[]
			{
				3,
				-11,
				15
			},
			new int[]
			{
				1,
				-13,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				4,
				-8,
				11
			},
			new int[]
			{
				4,
				-7,
				17
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				5,
				-12,
				11
			},
			new int[]
			{
				5,
				-9,
				17
			},
			new int[]
			{
				1,
				-13,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				6,
				-10,
				10
			},
			new int[]
			{
				6,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				7,
				-11,
				25
			},
			new int[]
			{
				1,
				-8,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-7,
				35
			},
			new int[]
			{
				0,
				-18,
				22
			},
			new int[]
			{
				7,
				-10,
				25
			},
			new int[]
			{
				1,
				-7,
				48
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				35
			},
			new int[]
			{
				10,
				-3,
				25
			},
			new int[]
			{
				12,
				-10,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				37
			},
			new int[]
			{
				11,
				-3,
				25
			},
			new int[]
			{
				12,
				-11,
				27
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				12,
				-8,
				21
			},
			new int[]
			{
				9,
				-7,
				31
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-12,
				35
			},
			new int[]
			{
				8,
				-5,
				14
			},
			new int[]
			{
				8,
				-15,
				29
			},
			new int[]
			{
				1,
				-9,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-10,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				7,
				-13,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				8,
				-15,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				9,
				-8,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				8,
				-16,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-8,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				0,
				-5,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				0,
				-6,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				2,
				-9,
				36
			},
			new int[]
			{
				13,
				-5,
				17
			},
			new int[]
			{
				16,
				-11,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				9,
				-8,
				29
			},
			new int[3]
		}
	};

	// Token: 0x040001CF RID: 463
	public static int[] CHAR_WEAPONX = new int[]
	{
		-2,
		-6,
		22,
		21,
		19,
		22,
		10,
		-2,
		-2,
		5,
		19
	};

	// Token: 0x040001D0 RID: 464
	public static int[] CHAR_WEAPONY = new int[]
	{
		9,
		22,
		25,
		17,
		26,
		37,
		36,
		49,
		50,
		52,
		36
	};

	// Token: 0x040001D1 RID: 465
	private static global::Char myChar;

	// Token: 0x040001D2 RID: 466
	private static global::Char myPet;

	// Token: 0x040001D3 RID: 467
	public static int[] listAttack;

	// Token: 0x040001D4 RID: 468
	public static int[][] listIonC;

	// Token: 0x040001D5 RID: 469
	public int cvyJump;

	// Token: 0x040001D6 RID: 470
	private int indexUseSkill = -1;

	// Token: 0x040001D7 RID: 471
	public int cxSend;

	// Token: 0x040001D8 RID: 472
	public int cySend;

	// Token: 0x040001D9 RID: 473
	public int cdirSend = 1;

	// Token: 0x040001DA RID: 474
	public int cxFocus;

	// Token: 0x040001DB RID: 475
	public int cyFocus;

	// Token: 0x040001DC RID: 476
	public int cactFirst = 5;

	// Token: 0x040001DD RID: 477
	public MyVector vMovePoints = new MyVector();

	// Token: 0x040001DE RID: 478
	public static string[][] inforClass = new string[][]
	{
		new string[]
		{
			"1",
			"1",
			"chiêu 1",
			"0"
		},
		new string[]
		{
			"2",
			"2",
			"chiêu 2",
			"5"
		}
	};

	// Token: 0x040001DF RID: 479
	public static int[][] inforSkill = new int[][]
	{
		new int[]
		{
			1,
			0,
			1,
			1000,
			40,
			1,
			0,
			20,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			1,
			10,
			1000,
			100,
			1,
			0,
			40,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			2,
			11,
			800,
			100,
			1,
			0,
			45,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			3,
			12,
			600,
			100,
			1,
			0,
			50,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			4,
			13,
			500,
			100,
			1,
			0,
			55,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			1,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			2,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			3,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			4,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			5,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		}
	};

	// Token: 0x040001E0 RID: 480
	public static bool flag;

	// Token: 0x040001E1 RID: 481
	public static bool ischangingMap;

	// Token: 0x040001E2 RID: 482
	public static bool isLockKey;

	// Token: 0x040001E3 RID: 483
	public static bool isLoadingMap;

	// Token: 0x040001E4 RID: 484
	public bool isLockMove;

	// Token: 0x040001E5 RID: 485
	public bool isLockAttack;

	// Token: 0x040001E6 RID: 486
	public string strInfo;

	// Token: 0x040001E7 RID: 487
	public short powerPoint;

	// Token: 0x040001E8 RID: 488
	public short maxPowerPoint;

	// Token: 0x040001E9 RID: 489
	public short secondPower;

	// Token: 0x040001EA RID: 490
	public long lastS;

	// Token: 0x040001EB RID: 491
	public long currS;

	// Token: 0x040001EC RID: 492
	public bool havePet = true;

	// Token: 0x040001ED RID: 493
	public MovePoint currentMovePoint;

	// Token: 0x040001EE RID: 494
	public int bom;

	// Token: 0x040001EF RID: 495
	public int delayFall;

	// Token: 0x040001F0 RID: 496
	private bool isSoundJump;

	// Token: 0x040001F1 RID: 497
	public int lastFrame;

	// Token: 0x040001F2 RID: 498
	private Effect eProtect;

	// Token: 0x040001F3 RID: 499
	private Effect eDanhHieu;

	// Token: 0x040001F4 RID: 500
	private int twHp;

	// Token: 0x040001F5 RID: 501
	public bool isInjureHp;

	// Token: 0x040001F6 RID: 502
	public bool changePos;

	// Token: 0x040001F7 RID: 503
	public bool isHide;

	// Token: 0x040001F8 RID: 504
	private bool wy;

	// Token: 0x040001F9 RID: 505
	public int wt;

	// Token: 0x040001FA RID: 506
	public int fy;

	// Token: 0x040001FB RID: 507
	public int ty;

	// Token: 0x040001FC RID: 508
	private int t;

	// Token: 0x040001FD RID: 509
	private int fM;

	// Token: 0x040001FE RID: 510
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

	// Token: 0x040001FF RID: 511
	private string strMount = "mount_";

	// Token: 0x04000200 RID: 512
	public int headICON = -1;

	// Token: 0x04000201 RID: 513
	public int head;

	// Token: 0x04000202 RID: 514
	public int leg;

	// Token: 0x04000203 RID: 515
	public int body;

	// Token: 0x04000204 RID: 516
	public int bag;

	// Token: 0x04000205 RID: 517
	public int wp;

	// Token: 0x04000206 RID: 518
	public int indexEff = -1;

	// Token: 0x04000207 RID: 519
	public int indexEffTask = -1;

	// Token: 0x04000208 RID: 520
	public EffectCharPaint eff;

	// Token: 0x04000209 RID: 521
	public EffectCharPaint effTask;

	// Token: 0x0400020A RID: 522
	public int indexSkill;

	// Token: 0x0400020B RID: 523
	public int i0;

	// Token: 0x0400020C RID: 524
	public int i1;

	// Token: 0x0400020D RID: 525
	public int i2;

	// Token: 0x0400020E RID: 526
	public int dx0;

	// Token: 0x0400020F RID: 527
	public int dx1;

	// Token: 0x04000210 RID: 528
	public int dx2;

	// Token: 0x04000211 RID: 529
	public int dy0;

	// Token: 0x04000212 RID: 530
	public int dy1;

	// Token: 0x04000213 RID: 531
	public int dy2;

	// Token: 0x04000214 RID: 532
	public EffectCharPaint eff0;

	// Token: 0x04000215 RID: 533
	public EffectCharPaint eff1;

	// Token: 0x04000216 RID: 534
	public EffectCharPaint eff2;

	// Token: 0x04000217 RID: 535
	public Arrow arr;

	// Token: 0x04000218 RID: 536
	public PlayerDart dart;

	// Token: 0x04000219 RID: 537
	public bool isCreateDark;

	// Token: 0x0400021A RID: 538
	public SkillPaint skillPaint;

	// Token: 0x0400021B RID: 539
	public SkillPaint skillPaintRandomPaint;

	// Token: 0x0400021C RID: 540
	public EffectPaint[] effPaints;

	// Token: 0x0400021D RID: 541
	public int sType;

	// Token: 0x0400021E RID: 542
	public sbyte isInjure;

	// Token: 0x0400021F RID: 543
	public bool isUseSkillAfterCharge;

	// Token: 0x04000220 RID: 544
	public bool isFlyAndCharge;

	// Token: 0x04000221 RID: 545
	public bool isStandAndCharge;

	// Token: 0x04000222 RID: 546
	private bool isFlying;

	// Token: 0x04000223 RID: 547
	public int posDisY;

	// Token: 0x04000224 RID: 548
	private int chargeCount;

	// Token: 0x04000225 RID: 549
	private bool hasSendAttack;

	// Token: 0x04000226 RID: 550
	public bool isMabuHold;

	// Token: 0x04000227 RID: 551
	private long timeBlue;

	// Token: 0x04000228 RID: 552
	private int tBlue;

	// Token: 0x04000229 RID: 553
	private bool IsAddDust1;

	// Token: 0x0400022A RID: 554
	private bool IsAddDust2;

	// Token: 0x0400022B RID: 555
	public int len = 24;

	// Token: 0x0400022C RID: 556
	public int w_hp_bar = 24;

	// Token: 0x0400022D RID: 557
	private int per = 100;

	// Token: 0x0400022E RID: 558
	private int per_tem = 100;

	// Token: 0x0400022F RID: 559
	private Image imgHPtem;

	// Token: 0x04000230 RID: 560
	private bool isPet;

	// Token: 0x04000231 RID: 561
	private bool isMiniPet;

	// Token: 0x04000232 RID: 562
	private int iiii;

	// Token: 0x04000233 RID: 563
	private int danhHieuFramme;

	// Token: 0x04000234 RID: 564
	public int xSd;

	// Token: 0x04000235 RID: 565
	public int ySd;

	// Token: 0x04000236 RID: 566
	private bool isOutMap;

	// Token: 0x04000237 RID: 567
	private int fBag;

	// Token: 0x04000238 RID: 568
	private Part ph;

	// Token: 0x04000239 RID: 569
	private Part pl;

	// Token: 0x0400023A RID: 570
	private Part pb;

	// Token: 0x0400023B RID: 571
	public int cH_new = 32;

	// Token: 0x0400023C RID: 572
	private int statusBeforeNothing;

	// Token: 0x0400023D RID: 573
	private int timeFocusToMob;

	// Token: 0x0400023E RID: 574
	public static bool isManualFocus = false;

	// Token: 0x0400023F RID: 575
	private global::Char charHold;

	// Token: 0x04000240 RID: 576
	private Mob mobHold;

	// Token: 0x04000241 RID: 577
	private int nInjure;

	// Token: 0x04000242 RID: 578
	public short wdx;

	// Token: 0x04000243 RID: 579
	public short wdy;

	// Token: 0x04000244 RID: 580
	public bool isDirtyPostion;

	// Token: 0x04000245 RID: 581
	public Skill lastNormalSkill;

	// Token: 0x04000246 RID: 582
	public bool currentFireByShortcut;

	// Token: 0x04000247 RID: 583
	public int cDamGoc;

	// Token: 0x04000248 RID: 584
	public int cHPGoc;

	// Token: 0x04000249 RID: 585
	public int cMPGoc;

	// Token: 0x0400024A RID: 586
	public int cDefGoc;

	// Token: 0x0400024B RID: 587
	public int cCriticalGoc;

	// Token: 0x0400024C RID: 588
	public sbyte hpFrom1000TiemNang;

	// Token: 0x0400024D RID: 589
	public sbyte mpFrom1000TiemNang;

	// Token: 0x0400024E RID: 590
	public sbyte damFrom1000TiemNang;

	// Token: 0x0400024F RID: 591
	public sbyte defFrom1000TiemNang = 1;

	// Token: 0x04000250 RID: 592
	public sbyte criticalFrom1000Tiemnang = 1;

	// Token: 0x04000251 RID: 593
	public short cMaxStamina;

	// Token: 0x04000252 RID: 594
	public short expForOneAdd;

	// Token: 0x04000253 RID: 595
	public sbyte isMonkey;

	// Token: 0x04000254 RID: 596
	public bool isCopy;

	// Token: 0x04000255 RID: 597
	public bool isWaitMonkey;

	// Token: 0x04000256 RID: 598
	private bool isFeetEff;

	// Token: 0x04000257 RID: 599
	public bool meDead;

	// Token: 0x04000258 RID: 600
	public int holdEffID;

	// Token: 0x04000259 RID: 601
	public bool holder;

	// Token: 0x0400025A RID: 602
	public bool protectEff;

	// Token: 0x0400025B RID: 603
	public bool danhHieuEff = true;

	// Token: 0x0400025C RID: 604
	private bool isSetPos;

	// Token: 0x0400025D RID: 605
	private int tpos;

	// Token: 0x0400025E RID: 606
	private short xPos;

	// Token: 0x0400025F RID: 607
	private short yPos;

	// Token: 0x04000260 RID: 608
	private sbyte typePos;

	// Token: 0x04000261 RID: 609
	private bool isMyFusion;

	// Token: 0x04000262 RID: 610
	public bool isFusion;

	// Token: 0x04000263 RID: 611
	public int tFusion;

	// Token: 0x04000264 RID: 612
	public bool huytSao;

	// Token: 0x04000265 RID: 613
	public bool blindEff;

	// Token: 0x04000266 RID: 614
	public bool telePortSkill;

	// Token: 0x04000267 RID: 615
	public bool sleepEff;

	// Token: 0x04000268 RID: 616
	public bool stone;

	// Token: 0x04000269 RID: 617
	public int perCentMp = 100;

	// Token: 0x0400026A RID: 618
	public int dHP;

	// Token: 0x0400026B RID: 619
	public int headTemp = -1;

	// Token: 0x0400026C RID: 620
	public int bodyTemp = -1;

	// Token: 0x0400026D RID: 621
	public int legTemp = -1;

	// Token: 0x0400026E RID: 622
	public int bagTemp = -1;

	// Token: 0x0400026F RID: 623
	public int wpTemp = -1;

	// Token: 0x04000270 RID: 624
	public MyVector vEffChar = new MyVector("vEff");

	// Token: 0x04000271 RID: 625
	public static FrameImage fraRedEye;

	// Token: 0x04000272 RID: 626
	private int fChopmat;

	// Token: 0x04000273 RID: 627
	private bool isAddChopMat;

	// Token: 0x04000274 RID: 628
	private long timeAddChopmat;

	// Token: 0x04000275 RID: 629
	private int[] frChopNhanh = new int[]
	{
		-1,
		-1,
		-1,
		-1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		-1,
		-1,
		-1,
		-1
	};

	// Token: 0x04000276 RID: 630
	private int[] frChopCham = new int[]
	{
		-1,
		-1,
		-1,
		-1,
		0,
		0,
		1,
		1,
		1,
		0,
		0,
		1,
		1,
		1,
		0,
		0,
		1,
		1,
		1,
		-1,
		-1,
		-1,
		-1
	};

	// Token: 0x04000277 RID: 631
	private int[] frEye = new int[]
	{
		-1,
		-1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		-1,
		-1
	};

	// Token: 0x04000278 RID: 632
	public static int[][] Arr_Head_2Fr = new int[][]
	{
		new int[]
		{
			542,
			543
		}
	};

	// Token: 0x04000279 RID: 633
	private int fHead;

	// Token: 0x0400027A RID: 634
	private string strEffAura = "aura_";

	// Token: 0x0400027B RID: 635
	public short idAuraEff = -1;

	// Token: 0x0400027C RID: 636
	public static bool isPaintAura = true;

	// Token: 0x0400027D RID: 637
	public static bool isPaintAura2 = true;

	// Token: 0x0400027E RID: 638
	private FrameImage fraEff;

	// Token: 0x0400027F RID: 639
	private FrameImage fraEffSub;

	// Token: 0x04000280 RID: 640
	private string strEff_Set_Item = "set_eff_";

	// Token: 0x04000281 RID: 641
	public short idEff_Set_Item = -1;

	// Token: 0x04000282 RID: 642
	private FrameImage fraHat_behind;

	// Token: 0x04000283 RID: 643
	private FrameImage fraHat_font;

	// Token: 0x04000284 RID: 644
	private FrameImage fraHat_behind_2;

	// Token: 0x04000285 RID: 645
	private FrameImage fraHat_font_2;

	// Token: 0x04000286 RID: 646
	private string strHat_behind = "hat_sau_";

	// Token: 0x04000287 RID: 647
	private string strHat_font = "hat_truoc_";

	// Token: 0x04000288 RID: 648
	private string strNgang = "ngang_";

	// Token: 0x04000289 RID: 649
	public short idHat = -1;

	// Token: 0x0400028A RID: 650
	public static int[][] hatInfo;

	// Token: 0x0400028B RID: 651
	public const byte TYPE_SKILL_KAMEX10 = 1;

	// Token: 0x0400028C RID: 652
	public const byte TYPE_SKILL_FINAL = 2;

	// Token: 0x0400028D RID: 653
	public const byte TYPE_SKILL_MAFUBA = 3;

	// Token: 0x0400028E RID: 654
	public const byte TYPE_SKILL_GENKI = 4;

	// Token: 0x0400028F RID: 655
	public bool isPaintNewSkill;

	// Token: 0x04000290 RID: 656
	private bool isFly;

	// Token: 0x04000291 RID: 657
	private long timeReset_newSkill;

	// Token: 0x04000292 RID: 658
	private sbyte typeFrame;

	// Token: 0x04000293 RID: 659
	private short idskillPaint;

	// Token: 0x04000294 RID: 660
	private byte[] fr_start;

	// Token: 0x04000295 RID: 661
	private byte[] fr_atk;

	// Token: 0x04000296 RID: 662
	private byte[] fr_end;

	// Token: 0x04000297 RID: 663
	private int count_NEW;

	// Token: 0x04000298 RID: 664
	private int stt;

	// Token: 0x04000299 RID: 665
	private short rangeDame;

	// Token: 0x0400029A RID: 666
	private sbyte typePaint;

	// Token: 0x0400029B RID: 667
	private sbyte typeItem;

	// Token: 0x0400029C RID: 668
	private Point targetDame;

	// Token: 0x0400029D RID: 669
	private long timeDame;

	// Token: 0x0400029E RID: 670
	public bool isMafuba;

	// Token: 0x0400029F RID: 671
	private short countMafuba;

	// Token: 0x040002A0 RID: 672
	public int xMFB;

	// Token: 0x040002A1 RID: 673
	public int yMFB;

	// Token: 0x040002A2 RID: 674
	public int timeGongSkill;

	// Token: 0x040002A3 RID: 675
	private FrameImage fraDanhHieu;

	// Token: 0x040002A4 RID: 676
	private MainImage mainImg;
}
