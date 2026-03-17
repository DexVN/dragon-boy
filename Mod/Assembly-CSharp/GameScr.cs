using System;
using Assets.src.g;

// Token: 0x0200003A RID: 58
public class GameScr : mScreen, IChatable
{
	// Token: 0x060002F1 RID: 753 RVA: 0x00043378 File Offset: 0x00041578
	public GameScr()
	{
		bool flag = GameCanvas.w == 128 || GameCanvas.h <= 208;
		if (flag)
		{
			GameScr.indexSize = 20;
		}
		this.cmdback = new Command(string.Empty, 11021);
		this.cmdMenu = new Command("menu", 11000);
		this.cmdFocus = new Command(string.Empty, 11001);
		this.cmdMenu.img = GameScr.imgMenu;
		this.cmdMenu.w = mGraphics.getImageWidth(this.cmdMenu.img) + 20;
		this.cmdMenu.isPlaySoundButton = false;
		this.cmdFocus.img = GameScr.imgFocus;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = 50;
			this.cmdFocus = null;
		}
		else
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = GameScr.gH - 30;
			this.cmdFocus.x = GameScr.gW - 32;
			this.cmdFocus.y = GameScr.gH - 32;
		}
		this.right = this.cmdFocus;
		GameScr.isPaintRada = 1;
		bool isTouch2 = GameCanvas.isTouch;
		if (isTouch2)
		{
			GameScr.isHaveSelectSkill = true;
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00043558 File Offset: 0x00041758
	public static void loadBg()
	{
		GameScr.fra_PVE_Bar_0 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_0.png"), 6, 15);
		GameScr.fra_PVE_Bar_1 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_1.png"), 38, 21);
		GameScr.imgVS = mSystem.loadImage("/mainImage/i_vs.png");
		GameScr.imgBall = mSystem.loadImage("/mainImage/i_charlife.png");
		GameScr.imgHP_NEW = mSystem.loadImage("/mainImage/i_hp.png");
		GameScr.imgKhung = mSystem.loadImage("/mainImage/i_khung.png");
		GameScr.imgLbtn = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
		GameScr.imgLbtnFocus = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
		GameScr.imgLbtn2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnl2.png");
		GameScr.imgLbtnFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf2.png");
		GameScr.imgPanel = GameCanvas.loadImage("/mainImage/myTexture2dpanel.png");
		GameScr.imgPanel2 = GameCanvas.loadImage("/mainImage/panel2.png");
		GameScr.imgHP = GameCanvas.loadImage("/mainImage/myTexture2dHP.png");
		GameScr.imgSP = GameCanvas.loadImage("/mainImage/SP.png");
		GameScr.imgHPLost = GameCanvas.loadImage("/mainImage/myTexture2dhpLost.png");
		GameScr.imgMPLost = GameCanvas.loadImage("/mainImage/myTexture2dmpLost.png");
		GameScr.imgMP = GameCanvas.loadImage("/mainImage/myTexture2dMP.png");
		GameScr.imgSkill = GameCanvas.loadImage("/mainImage/myTexture2dskill.png");
		GameScr.imgSkill2 = GameCanvas.loadImage("/mainImage/myTexture2dskill2.png");
		GameScr.imgMenu = GameCanvas.loadImage("/mainImage/myTexture2dmenu.png");
		GameScr.imgFocus = GameCanvas.loadImage("/mainImage/myTexture2dfocus.png");
		GameScr.imgHP_tm_do = GameCanvas.loadImage("/mainImage/tm-do.png");
		GameScr.imgHP_tm_vang = GameCanvas.loadImage("/mainImage/tm-vang.png");
		GameScr.imgHP_tm_xam = GameCanvas.loadImage("/mainImage/tm-xam.png");
		GameScr.imgHP_tm_xanh = GameCanvas.loadImage("/mainImage/tm-xanh.png");
		GameScr.imgChatPC = GameCanvas.loadImage("/pc/chat.png");
		GameScr.imgChatsPC2 = GameCanvas.loadImage("/pc/chat2.png");
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			GameScr.imgArrow = GameCanvas.loadImage("/mainImage/myTexture2darrow.png");
			GameScr.imgArrow2 = GameCanvas.loadImage("/mainImage/myTexture2darrow2.png");
			GameScr.imgChat = GameCanvas.loadImage("/mainImage/myTexture2dchat.png");
			GameScr.imgChat2 = GameCanvas.loadImage("/mainImage/myTexture2dchat2.png");
			GameScr.imgFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dfocus2.png");
			GameScr.imgHP1 = GameCanvas.loadImage("/mainImage/myTexture2dPea0.png");
			GameScr.imgHP2 = GameCanvas.loadImage("/mainImage/myTexture2dPea1.png");
			GameScr.imgAnalog1 = GameCanvas.loadImage("/mainImage/myTexture2danalog1.png");
			GameScr.imgAnalog2 = GameCanvas.loadImage("/mainImage/myTexture2danalog2.png");
			GameScr.imgHP3 = GameCanvas.loadImage("/mainImage/myTexture2dPea2.png");
			GameScr.imgHP4 = GameCanvas.loadImage("/mainImage/myTexture2dPea3.png");
			GameScr.imgFire0 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn0.png");
			GameScr.imgFire1 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn1.png");
		}
		GameScr.flyTextX = new int[5];
		GameScr.flyTextY = new int[5];
		GameScr.flyTextDx = new int[5];
		GameScr.flyTextDy = new int[5];
		GameScr.flyTextState = new int[5];
		GameScr.flyTextString = new string[5];
		GameScr.flyTextYTo = new int[5];
		GameScr.flyTime = new int[5];
		GameScr.flyTextColor = new int[8];
		for (int i = 0; i < 5; i++)
		{
			GameScr.flyTextState[i] = -1;
		}
		sbyte[] array = Rms.loadRMS("NRdataVersion");
		sbyte[] array2 = Rms.loadRMS("NRmapVersion");
		sbyte[] array3 = Rms.loadRMS("NRskillVersion");
		sbyte[] array4 = Rms.loadRMS("NRitemVersion");
		bool flag = array != null;
		if (flag)
		{
			GameScr.vcData = array[0];
		}
		bool flag2 = array2 != null;
		if (flag2)
		{
			GameScr.vcMap = array2[0];
		}
		bool flag3 = array3 != null;
		if (flag3)
		{
			GameScr.vcSkill = array3[0];
		}
		bool flag4 = array4 != null;
		if (flag4)
		{
			GameScr.vcItem = array4[0];
		}
		GameScr.imgNut = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
		GameScr.imgNutF = GameCanvas.loadImage("/mainImage/myTexture2dnutF.png");
		MobCapcha.init();
		GameScr.isAnalog = ((Rms.loadRMSInt("analog") != 1) ? 0 : 1);
		GameScr.gamePad = new GamePad();
		GameScr.arrow = GameCanvas.loadImage("/mainImage/myTexture2darrow3.png");
		GameScr.imgTrans = GameCanvas.loadImage("/bg/trans.png");
		GameScr.imgRoomStat = GameCanvas.loadImage("/mainImage/myTexture2dstat.png");
		GameScr.frBarPow0 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor20.png");
		GameScr.frBarPow1 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor21.png");
		GameScr.frBarPow2 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor22.png");
		GameScr.frBarPow20 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor00.png");
		GameScr.frBarPow21 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor01.png");
		GameScr.frBarPow22 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor02.png");
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x000439AA File Offset: 0x00041BAA
	public void initSelectChar()
	{
		this.readPart();
		SmallImage.init();
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x000439BC File Offset: 0x00041BBC
	public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
	{
		int clipX = g.getClipX();
		int clipY = g.getClipY();
		int clipWidth = g.getClipWidth();
		int clipHeight = g.getClipHeight();
		g.setClip((int)x, (int)y, (int)pixelPercent, 13);
		int num = size / 15 - 2;
		for (int i = 0; i < num; i++)
		{
			g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img1, x + (float)size - 30f, y, 0);
		g.drawImage(img2, x + (float)size - 15f, y, 0);
		g.setClip(clipX, clipY, clipWidth, clipHeight);
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00043A7C File Offset: 0x00041C7C
	public void initTraining()
	{
		bool isCreateChar = CreateCharScr.isCreateChar;
		if (isCreateChar)
		{
			CreateCharScr.isCreateChar = false;
			this.right = null;
		}
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00043AA4 File Offset: 0x00041CA4
	public bool isMapDocNhan()
	{
		return TileMap.mapID >= 53 && TileMap.mapID <= 62;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00043AD0 File Offset: 0x00041CD0
	public bool isMapFize()
	{
		return TileMap.mapID >= 63;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00043AF0 File Offset: 0x00041CF0
	public override void switchToMe()
	{
		GameScr.vChatVip.removeAllElements();
		ServerListScreen.isWait = false;
		bool flag = BackgroudEffect.isHaveRain();
		if (flag)
		{
			SoundMn.gI().rain();
		}
		LoginScr.isContinueToLogin = false;
		global::Char.isLoadingMap = false;
		bool flag2 = !GameScr.isPaintOther;
		if (flag2)
		{
			Service.gI().finishLoadMap();
		}
		bool flag3 = TileMap.isTrainingMap();
		if (flag3)
		{
			this.initTraining();
		}
		GameScr.info1.isUpdate = true;
		GameScr.info2.isUpdate = true;
		this.resetButton();
		GameScr.isLoadAllData = true;
		GameScr.isPaintOther = false;
		base.switchToMe();
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00043B8C File Offset: 0x00041D8C
	public static int getMaxExp(int level)
	{
		int num = 0;
		for (int i = 0; i <= level; i++)
		{
			num += (int)GameScr.exps[i];
		}
		return num;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00043BC4 File Offset: 0x00041DC4
	public static void resetAllvector()
	{
		GameScr.vCharInMap.removeAllElements();
		Teleport.vTeleport.removeAllElements();
		GameScr.vItemMap.removeAllElements();
		Effect2.vEffect2.removeAllElements();
		Effect2.vAnimateEffect.removeAllElements();
		Effect2.vEffect2Outside.removeAllElements();
		Effect2.vEffectFeet.removeAllElements();
		Effect2.vEffect3.removeAllElements();
		GameScr.vMobAttack.removeAllElements();
		GameScr.vMob.removeAllElements();
		GameScr.vNpc.removeAllElements();
		global::Char.myCharz().vMovePoints.removeAllElements();
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00003136 File Offset: 0x00001336
	public void loadSkillShortcut()
	{
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00043C5C File Offset: 0x00041E5C
	public void onOSkill(sbyte[] oSkillID)
	{
		Cout.println("GET onScreenSkill!");
		GameScr.onScreenSkill = new Skill[10];
		bool flag = oSkillID == null;
		if (flag)
		{
			this.loadDefaultonScreenSkill();
		}
		else
		{
			for (int i = 0; i < oSkillID.Length; i++)
			{
				for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
					bool flag2 = skill.template.id == oSkillID[i];
					if (flag2)
					{
						GameScr.onScreenSkill[i] = skill;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00043D08 File Offset: 0x00041F08
	public void onKSkill(sbyte[] kSkillID)
	{
		Cout.println("GET KEYSKILL!");
		GameScr.keySkill = new Skill[10];
		bool flag = kSkillID == null;
		if (flag)
		{
			this.loadDefaultKeySkill();
		}
		else
		{
			for (int i = 0; i < kSkillID.Length; i++)
			{
				for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
					bool flag2 = skill.template.id == kSkillID[i];
					if (flag2)
					{
						GameScr.keySkill[i] = skill;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00043DB4 File Offset: 0x00041FB4
	public void onCSkill(sbyte[] cSkillID)
	{
		Cout.println("GET CURRENTSKILL!");
		bool flag = cSkillID == null || cSkillID.Length == 0;
		if (flag)
		{
			bool flag2 = global::Char.myCharz().vSkillFight.size() > 0;
			if (flag2)
			{
				global::Char.myCharz().myskill = (Skill)global::Char.myCharz().vSkillFight.elementAt(0);
			}
		}
		else
		{
			for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
				bool flag3 = skill.template.id == cSkillID[0];
				if (flag3)
				{
					global::Char.myCharz().myskill = skill;
					break;
				}
			}
		}
		bool flag4 = global::Char.myCharz().myskill != null;
		if (flag4)
		{
			Service.gI().selectSkill((int)global::Char.myCharz().myskill.template.id);
			this.saveRMSCurrentSkill(global::Char.myCharz().myskill.template.id);
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00043EC4 File Offset: 0x000420C4
	private void loadDefaultonScreenSkill()
	{
		Cout.println("LOAD DEFAULT ONmScreen SKILL");
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			bool flag = i >= global::Char.myCharz().vSkillFight.size();
			if (flag)
			{
				break;
			}
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			GameScr.onScreenSkill[i] = skill;
		}
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00043F38 File Offset: 0x00042138
	private void loadDefaultKeySkill()
	{
		Cout.println("LOAD DEFAULT KEY SKILL");
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			bool flag = i >= global::Char.myCharz().vSkillFight.size();
			if (flag)
			{
				break;
			}
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			GameScr.keySkill[i] = skill;
		}
		this.saveKeySkillToRMS();
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00043FAC File Offset: 0x000421AC
	public void doSetOnScreenSkill(SkillTemplate skillTemplate)
	{
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			object[] p = new object[]
			{
				skill,
				i.ToString() + string.Empty
			};
			Command command = new Command(mResources.into_place + (i + 1).ToString(), 11120, p);
			Skill skill2 = GameScr.onScreenSkill[i];
			bool flag = skill2 != null;
			if (flag)
			{
				command.isDisplay = true;
			}
			myVector.addElement(command);
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00044058 File Offset: 0x00042258
	public void doSetKeySkill(SkillTemplate skillTemplate)
	{
		Cout.println("DO SET KEY SKILL");
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		string[] array = (!TField.isQwerty) ? mResources.key_skill : mResources.key_skill_qwerty;
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			object[] p = new object[]
			{
				skill,
				i.ToString() + string.Empty
			};
			myVector.addElement(new Command(array[i], 11121, p));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x06000303 RID: 771 RVA: 0x000440F0 File Offset: 0x000422F0
	public void saveonScreenSkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.onScreenSkill.Length];
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			bool flag = GameScr.onScreenSkill[i] == null;
			if (flag)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.onScreenSkill[i].template.id;
			}
		}
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0004415C File Offset: 0x0004235C
	public void saveKeySkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.keySkill.Length];
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			bool flag = GameScr.keySkill[i] == null;
			if (flag)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.keySkill[i].template.id;
			}
		}
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00003136 File Offset: 0x00001336
	public void saveRMSCurrentSkill(sbyte id)
	{
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000441C8 File Offset: 0x000423C8
	public void addSkillShortcut(Skill skill)
	{
		Cout.println("ADD SKILL SHORTCUT TO SKILL " + skill.template.id.ToString());
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			bool flag = GameScr.onScreenSkill[i] == null;
			if (flag)
			{
				GameScr.onScreenSkill[i] = skill;
				break;
			}
		}
		for (int j = 0; j < GameScr.keySkill.Length; j++)
		{
			bool flag2 = GameScr.keySkill[j] == null;
			if (flag2)
			{
				GameScr.keySkill[j] = skill;
				break;
			}
		}
		bool flag3 = global::Char.myCharz().myskill == null;
		if (flag3)
		{
			global::Char.myCharz().myskill = skill;
		}
		this.saveKeySkillToRMS();
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0004428C File Offset: 0x0004248C
	public bool isBagFull()
	{
		for (int i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
		{
			bool flag = global::Char.myCharz().arrItemBag[i] == null;
			if (flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x000442D9 File Offset: 0x000424D9
	public void createConfirm(string[] menu, Npc npc)
	{
		this.resetButton();
		this.isLockKey = true;
		this.left = new Command(menu[0], 130011, npc);
		this.right = new Command(menu[1], 130012, npc);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00044314 File Offset: 0x00042514
	public void createMenu(string[] menu, Npc npc)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < menu.Length; i++)
		{
			myVector.addElement(new Command(menu[i], 11057, npc));
		}
		GameCanvas.menu.startAt(myVector, 2);
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00044360 File Offset: 0x00042560
	public void readPart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_part"));
			int num = (int)dataInputStream.readShort();
			GameScr.parts = new Part[num];
			for (int i = 0; i < num; i++)
			{
				int type = (int)dataInputStream.readByte();
				GameScr.parts[i] = new Part(type);
				for (int j = 0; j < GameScr.parts[i].pi.Length; j++)
				{
					GameScr.parts[i].pi[j] = new PartImage();
					GameScr.parts[i].pi[j].id = dataInputStream.readShort();
					GameScr.parts[i].pi[j].dx = dataInputStream.readByte();
					GameScr.parts[i].pi[j].dy = dataInputStream.readByte();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readPart " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Res.outz2("LOI TAI readPart 2" + ex2.StackTrace);
			}
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x000444C0 File Offset: 0x000426C0
	public void readEfect()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_effect"));
			int num = (int)dataInputStream.readShort();
			GameScr.efs = new EffectCharPaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.efs[i] = new EffectCharPaint();
				GameScr.efs[i].idEf = (int)dataInputStream.readShort();
				GameScr.efs[i].arrEfInfo = new EffectInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.efs[i].arrEfInfo.Length; j++)
				{
					GameScr.efs[i].arrEfInfo[j] = new EffectInfoPaint();
					GameScr.efs[i].arrEfInfo[j].idImg = (int)dataInputStream.readShort();
					GameScr.efs[i].arrEfInfo[j].dx = (int)dataInputStream.readByte();
					GameScr.efs[i].arrEfInfo[j].dy = (int)dataInputStream.readByte();
				}
			}
		}
		catch (Exception ex)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham Eff: " + ex2.ToString());
			}
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00044620 File Offset: 0x00042820
	public void readArrow()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_arrow"));
			int num = (int)dataInputStream.readShort();
			GameScr.arrs = new Arrowpaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.arrs[i] = new Arrowpaint();
				GameScr.arrs[i].id = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[0] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[1] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[2] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception ex)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham readArrow: " + ex2.ToString());
			}
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00044720 File Offset: 0x00042920
	public void readDart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_dart"));
			int num = (int)dataInputStream.readShort();
			GameScr.darts = new DartInfo[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.darts[i] = new DartInfo();
				GameScr.darts[i].id = dataInputStream.readShort();
				GameScr.darts[i].nUpdate = dataInputStream.readShort();
				GameScr.darts[i].va = (int)(dataInputStream.readShort() * 256);
				GameScr.darts[i].xdPercent = dataInputStream.readShort();
				int num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tail = new short[num2];
				for (int j = 0; j < num2; j++)
				{
					GameScr.darts[i].tail[j] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tailBorder = new short[num2];
				for (int k = 0; k < num2; k++)
				{
					GameScr.darts[i].tailBorder[k] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd1 = new short[num2];
				for (int l = 0; l < num2; l++)
				{
					GameScr.darts[i].xd1[l] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd2 = new short[num2];
				for (int m = 0; m < num2; m++)
				{
					GameScr.darts[i].xd2[m] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].head = new short[num2][];
				for (int n = 0; n < num2; n++)
				{
					short num3 = dataInputStream.readShort();
					GameScr.darts[i].head[n] = new short[(int)num3];
					for (int num4 = 0; num4 < (int)num3; num4++)
					{
						GameScr.darts[i].head[n][num4] = dataInputStream.readShort();
					}
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].headBorder = new short[num2][];
				for (int num5 = 0; num5 < num2; num5++)
				{
					short num6 = dataInputStream.readShort();
					GameScr.darts[i].headBorder[num5] = new short[(int)num6];
					for (int num7 = 0; num7 < (int)num6; num7++)
					{
						GameScr.darts[i].headBorder[num5][num7] = dataInputStream.readShort();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham ReadDart: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham reaaDart: " + ex2.ToString());
			}
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00044A78 File Offset: 0x00042C78
	public void readSkill()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_skill"));
			int num = (int)dataInputStream.readShort();
			int num2 = Skills.skills.size();
			GameScr.sks = new SkillPaint[num2];
			for (int i = 0; i < num; i++)
			{
				short num3 = dataInputStream.readShort();
				bool flag = num3 == 1111;
				if (flag)
				{
					num3 = (short)(num - 1);
				}
				GameScr.sks[(int)num3] = new SkillPaint();
				GameScr.sks[(int)num3].id = (int)num3;
				GameScr.sks[(int)num3].effectHappenOnMob = (int)dataInputStream.readShort();
				bool flag2 = GameScr.sks[(int)num3].effectHappenOnMob <= 0;
				if (flag2)
				{
					GameScr.sks[(int)num3].effectHappenOnMob = 80;
				}
				GameScr.sks[(int)num3].numEff = (int)dataInputStream.readByte();
				GameScr.sks[(int)num3].skillStand = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.sks[(int)num3].skillStand.Length; j++)
				{
					GameScr.sks[(int)num3].skillStand[j] = new SkillInfoPaint();
					GameScr.sks[(int)num3].skillStand[j].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num3].skillStand[j].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillStand[j].ady = (int)dataInputStream.readShort();
				}
				GameScr.sks[(int)num3].skillfly = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int k = 0; k < GameScr.sks[(int)num3].skillfly.Length; k++)
				{
					GameScr.sks[(int)num3].skillfly[k] = new SkillInfoPaint();
					GameScr.sks[(int)num3].skillfly[k].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num3].skillfly[k].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num3].skillfly[k].ady = (int)dataInputStream.readShort();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham readSkill: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham readskill: " + ex2.ToString());
			}
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00044F64 File Offset: 0x00043164
	public static GameScr gI()
	{
		bool flag = GameScr.instance == null;
		if (flag)
		{
			GameScr.instance = new GameScr();
		}
		return GameScr.instance;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00044F93 File Offset: 0x00043193
	public static void clearGameScr()
	{
		GameScr.instance = null;
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00044F9C File Offset: 0x0004319C
	public void loadGameScr()
	{
		GameScr.loadSplash();
		Res.init();
		this.loadInforBar();
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00044FB4 File Offset: 0x000431B4
	public void doMenuInforMe()
	{
		GameScr.scrMain.clear();
		GameScr.scrInfo.clear();
		GameScr.isViewNext = false;
		this.cmdBag = new Command(mResources.MENUME[0], 1100011);
		this.cmdSkill = new Command(mResources.MENUME[1], 1100012);
		this.cmdTiemnang = new Command(mResources.MENUME[2], 1100013);
		this.cmdInfo = new Command(mResources.MENUME[3], 1100014);
		this.cmdtrangbi = new Command(mResources.MENUME[4], 1100015);
		MyVector myVector = new MyVector();
		myVector.addElement(this.cmdBag);
		myVector.addElement(this.cmdSkill);
		myVector.addElement(this.cmdTiemnang);
		myVector.addElement(this.cmdInfo);
		myVector.addElement(this.cmdtrangbi);
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000313 RID: 787 RVA: 0x000450A8 File Offset: 0x000432A8
	public void doMenusynthesis()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.SYNTHESIS[0], 110002));
		myVector.addElement(new Command(mResources.SYNTHESIS[1], 1100032));
		myVector.addElement(new Command(mResources.SYNTHESIS[2], 1100033));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00045114 File Offset: 0x00043314
	public static void loadCamera(bool fullmScreen, int cx, int cy)
	{
		GameScr.gW = GameCanvas.w;
		GameScr.cmdBarH = 39;
		GameScr.gH = GameCanvas.h;
		GameScr.cmdBarW = GameScr.gW;
		GameScr.cmdBarX = 0;
		GameScr.cmdBarY = GameCanvas.h - Paint.hTab - GameScr.cmdBarH;
		GameScr.girlHPBarY = 0;
		GameScr.csPadMaxH = GameCanvas.h / 6;
		bool flag = GameScr.csPadMaxH < 48;
		if (flag)
		{
			GameScr.csPadMaxH = 48;
		}
		GameScr.gW2 = GameScr.gW >> 1;
		GameScr.gH2 = GameScr.gH >> 1;
		GameScr.gW3 = GameScr.gW / 3;
		GameScr.gH3 = GameScr.gH / 3;
		GameScr.gW23 = GameScr.gH - 120;
		GameScr.gH23 = GameScr.gH * 2 / 3;
		GameScr.gW34 = 3 * GameScr.gW / 4;
		GameScr.gH34 = 3 * GameScr.gH / 4;
		GameScr.gW6 = GameScr.gW / 6;
		GameScr.gH6 = GameScr.gH / 6;
		GameScr.gssw = GameScr.gW / (int)TileMap.size + 2;
		GameScr.gssh = GameScr.gH / (int)TileMap.size + 2;
		bool flag2 = GameScr.gW % 24 != 0;
		if (flag2)
		{
			GameScr.gssw++;
		}
		GameScr.cmxLim = (TileMap.tmw - 1) * (int)TileMap.size - GameScr.gW;
		GameScr.cmyLim = (TileMap.tmh - 1) * (int)TileMap.size - GameScr.gH;
		bool flag3 = cx == -1 && cy == -1;
		if (flag3)
		{
			GameScr.cmx = (GameScr.cmtoX = global::Char.myCharz().cx - GameScr.gW2 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = global::Char.myCharz().cy - GameScr.gH23);
		}
		else
		{
			GameScr.cmx = (GameScr.cmtoX = cx - GameScr.gW23 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = cy - GameScr.gH23);
		}
		GameScr.firstY = GameScr.cmy;
		bool flag4 = GameScr.cmx < 24;
		if (flag4)
		{
			GameScr.cmx = (GameScr.cmtoX = 24);
		}
		bool flag5 = GameScr.cmx > GameScr.cmxLim;
		if (flag5)
		{
			GameScr.cmx = (GameScr.cmtoX = GameScr.cmxLim);
		}
		bool flag6 = GameScr.cmy < 0;
		if (flag6)
		{
			GameScr.cmy = (GameScr.cmtoY = 0);
		}
		bool flag7 = GameScr.cmy > GameScr.cmyLim;
		if (flag7)
		{
			GameScr.cmy = (GameScr.cmtoY = GameScr.cmyLim);
		}
		GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
		bool flag8 = GameScr.gssx < 0;
		if (flag8)
		{
			GameScr.gssx = 0;
		}
		GameScr.gssy = GameScr.cmy / (int)TileMap.size;
		GameScr.gssxe = GameScr.gssx + GameScr.gssw;
		GameScr.gssye = GameScr.gssy + GameScr.gssh;
		bool flag9 = GameScr.gssy < 0;
		if (flag9)
		{
			GameScr.gssy = 0;
		}
		bool flag10 = GameScr.gssye > TileMap.tmh - 1;
		if (flag10)
		{
			GameScr.gssye = TileMap.tmh - 1;
		}
		TileMap.countx = (GameScr.gssxe - GameScr.gssx) * 4;
		bool flag11 = TileMap.countx > TileMap.tmw;
		if (flag11)
		{
			TileMap.countx = TileMap.tmw;
		}
		TileMap.county = (GameScr.gssye - GameScr.gssy) * 4;
		bool flag12 = TileMap.county > TileMap.tmh;
		if (flag12)
		{
			TileMap.county = TileMap.tmh;
		}
		TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
		bool flag13 = TileMap.gssx < 0;
		if (flag13)
		{
			TileMap.gssx = 0;
		}
		TileMap.gssxe = TileMap.gssx + TileMap.countx;
		bool flag14 = TileMap.gssxe > TileMap.tmw;
		if (flag14)
		{
			TileMap.gssxe = TileMap.tmw;
		}
		TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
		bool flag15 = TileMap.gssy < 0;
		if (flag15)
		{
			TileMap.gssy = 0;
		}
		TileMap.gssye = TileMap.gssy + TileMap.county;
		bool flag16 = TileMap.gssye > TileMap.tmh;
		if (flag16)
		{
			TileMap.gssye = TileMap.tmh;
		}
		ChatTextField.gI().parentScreen = GameScr.instance;
		ChatTextField.gI().tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
		ChatTextField.gI().initChatTextField();
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			GameScr.yTouchBar = GameScr.gH - 88;
			GameScr.xC = GameScr.gW - 40;
			GameScr.yC = 2;
			bool flag17 = GameCanvas.w <= 240;
			if (flag17)
			{
				GameScr.xC = GameScr.gW - 35;
				GameScr.yC = 5;
			}
			GameScr.xF = GameScr.gW - 55;
			GameScr.yF = GameScr.yTouchBar + 35;
			GameScr.xTG = GameScr.gW - 37;
			GameScr.yTG = GameScr.yTouchBar - 1;
			bool flag18 = GameCanvas.w >= 450;
			if (flag18)
			{
				GameScr.yTG -= 12;
				GameScr.yHP -= 7;
				GameScr.xF -= 10;
				GameScr.yF -= 5;
				GameScr.xTG -= 10;
			}
		}
		GameScr.setSkillBarPosition();
		GameScr.disXC = ((GameCanvas.w <= 200) ? 30 : 40);
		bool flag19 = Rms.loadRMSInt("viewchat") == -1;
		if (flag19)
		{
			GameCanvas.panel.isViewChatServer = true;
		}
		else
		{
			GameCanvas.panel.isViewChatServer = (Rms.loadRMSInt("viewchat") == 1);
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x000456C4 File Offset: 0x000438C4
	public static void setSkillBarPosition()
	{
		Skill[] array = (!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill;
		GameScr.xS = new int[array.Length];
		GameScr.yS = new int[array.Length];
		bool flag = GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch;
		if (flag)
		{
			GameScr.xSkill = 23;
			GameScr.ySkill = 52;
			GameScr.padSkill = 5;
			for (int i = 0; i < GameScr.xS.Length; i++)
			{
				GameScr.xS[i] = i * (25 + GameScr.padSkill);
				GameScr.yS[i] = GameScr.ySkill;
				bool flag2 = GameScr.xS.Length > 5 && i >= GameScr.xS.Length / 2;
				if (flag2)
				{
					GameScr.xS[i] = (i - GameScr.xS.Length / 2) * (25 + GameScr.padSkill);
					GameScr.yS[i] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * (25 + GameScr.padSkill);
			GameScr.yHP = GameScr.ySkill;
		}
		else
		{
			GameScr.wSkill = 30;
			bool flag3 = GameCanvas.w <= 320;
			if (flag3)
			{
				GameScr.ySkill = GameScr.gH - GameScr.wSkill - 6;
				GameScr.xSkill = GameScr.gW2 - array.Length * GameScr.wSkill / 2 - 25;
			}
			else
			{
				GameScr.wSkill = 40;
				GameScr.xSkill = 10;
				GameScr.ySkill = GameCanvas.h - GameScr.wSkill + 7;
			}
			for (int j = 0; j < GameScr.xS.Length; j++)
			{
				GameScr.xS[j] = j * GameScr.wSkill;
				GameScr.yS[j] = GameScr.ySkill;
				bool flag4 = GameScr.xS.Length > 5 && j >= GameScr.xS.Length / 2;
				if (flag4)
				{
					GameScr.xS[j] = (j - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[j] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * GameScr.wSkill;
			GameScr.yHP = GameScr.ySkill;
		}
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			GameScr.xSkill = 17;
			GameScr.ySkill = GameCanvas.h - 40;
			bool flag5 = GameScr.gamePad.isSmallGamePad && GameScr.isAnalog == 1;
			if (flag5)
			{
				GameScr.xHP = array.Length * GameScr.wSkill;
				GameScr.yHP = GameScr.ySkill;
			}
			else
			{
				GameScr.xHP = GameCanvas.w - 45;
				GameScr.yHP = GameCanvas.h - 45;
			}
			GameScr.setTouchBtn();
			for (int k = 0; k < GameScr.xS.Length; k++)
			{
				GameScr.xS[k] = k * GameScr.wSkill;
				GameScr.yS[k] = GameScr.ySkill;
				bool flag6 = GameScr.xS.Length > 5 && k >= GameScr.xS.Length / 2;
				if (flag6)
				{
					GameScr.xS[k] = (k - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[k] = GameScr.ySkill - 32;
				}
			}
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x000459F0 File Offset: 0x00043BF0
	private static void updateCamera()
	{
		bool flag = GameScr.isPaintOther;
		if (!flag)
		{
			bool flag2 = GameScr.cmx != GameScr.cmtoX || GameScr.cmy != GameScr.cmtoY;
			if (flag2)
			{
				GameScr.cmvx = GameScr.cmtoX - GameScr.cmx << 2;
				GameScr.cmvy = GameScr.cmtoY - GameScr.cmy << 2;
				GameScr.cmdx += GameScr.cmvx;
				GameScr.cmx += GameScr.cmdx >> 4;
				GameScr.cmdx &= 15;
				GameScr.cmdy += GameScr.cmvy;
				GameScr.cmy += GameScr.cmdy >> 4;
				GameScr.cmdy &= 15;
				bool flag3 = GameScr.cmx < 24;
				if (flag3)
				{
					GameScr.cmx = 24;
				}
				bool flag4 = GameScr.cmx > GameScr.cmxLim;
				if (flag4)
				{
					GameScr.cmx = GameScr.cmxLim;
				}
				bool flag5 = GameScr.cmy < 0;
				if (flag5)
				{
					GameScr.cmy = 0;
				}
				bool flag6 = GameScr.cmy > GameScr.cmyLim;
				if (flag6)
				{
					GameScr.cmy = GameScr.cmyLim;
				}
			}
			GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
			bool flag7 = GameScr.gssx < 0;
			if (flag7)
			{
				GameScr.gssx = 0;
			}
			GameScr.gssy = GameScr.cmy / (int)TileMap.size;
			GameScr.gssxe = GameScr.gssx + GameScr.gssw;
			GameScr.gssye = GameScr.gssy + GameScr.gssh;
			bool flag8 = GameScr.gssy < 0;
			if (flag8)
			{
				GameScr.gssy = 0;
			}
			bool flag9 = GameScr.gssye > TileMap.tmh - 1;
			if (flag9)
			{
				GameScr.gssye = TileMap.tmh - 1;
			}
			TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
			bool flag10 = TileMap.gssx < 0;
			if (flag10)
			{
				TileMap.gssx = 0;
			}
			TileMap.gssxe = TileMap.gssx + TileMap.countx;
			bool flag11 = TileMap.gssxe > TileMap.tmw;
			if (flag11)
			{
				TileMap.gssxe = TileMap.tmw;
				TileMap.gssx = TileMap.gssxe - TileMap.countx;
			}
			TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
			bool flag12 = TileMap.gssy < 0;
			if (flag12)
			{
				TileMap.gssy = 0;
			}
			TileMap.gssye = TileMap.gssy + TileMap.county;
			bool flag13 = TileMap.gssye > TileMap.tmh;
			if (flag13)
			{
				TileMap.gssye = TileMap.tmh;
				TileMap.gssy = TileMap.gssye - TileMap.county;
			}
			GameScr.scrMain.updatecm();
			GameScr.scrInfo.updatecm();
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00045CA4 File Offset: 0x00043EA4
	public bool testAct()
	{
		for (sbyte b = 2; b < 9; b += 2)
		{
			bool flag = GameCanvas.keyHold[(int)b];
			if (flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00045CDC File Offset: 0x00043EDC
	public void clanInvite(string strInvite, int clanID, int code)
	{
		ClanObject clanObject = new ClanObject();
		clanObject.code = code;
		clanObject.clanID = clanID;
		this.startYesNoPopUp(strInvite, new Command(mResources.YES, 12002, clanObject), new Command(mResources.NO, 12003, clanObject));
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00045D28 File Offset: 0x00043F28
	public void playerMenu(global::Char c)
	{
		this.auto = 0;
		GameCanvas.clearKeyHold();
		bool flag = global::Char.myCharz().charFocus.charID < 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().charID < 0;
			if (!flag2)
			{
				MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
				bool flag3 = vPlayerMenu.size() > 0;
				if (!flag3)
				{
					bool flag4 = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId > 1;
					if (flag4)
					{
						vPlayerMenu.addElement(new Command(mResources.make_friend, 11112, global::Char.myCharz().charFocus));
						vPlayerMenu.addElement(new Command(mResources.trade, 11113, global::Char.myCharz().charFocus));
					}
					bool flag5 = global::Char.myCharz().clan != null && global::Char.myCharz().role < 2 && global::Char.myCharz().charFocus.clanID == -1;
					if (flag5)
					{
						vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[4], 110391));
					}
					bool flag6 = global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().charFocus.statusMe != 5;
					if (flag6)
					{
						bool flag7 = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14;
						if (flag7)
						{
							vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[0], 2003));
						}
					}
					else
					{
						bool flag8 = global::Char.myCharz().myskill.template.type == 4;
						if (flag8)
						{
						}
					}
					bool flag9 = global::Char.myCharz().clan != null && global::Char.myCharz().clan.ID == global::Char.myCharz().charFocus.clanID && global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14;
					if (flag9)
					{
						vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[1], 2004));
					}
					int num = global::Char.myCharz().nClass.skillTemplates.Length;
					for (int i = 0; i < num; i++)
					{
						SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
						Skill skill = global::Char.myCharz().getSkill(skillTemplate);
						bool flag10 = skill != null && skillTemplate.isBuffToPlayer() && skill.point >= 1;
						if (flag10)
						{
							vPlayerMenu.addElement(new Command(skillTemplate.name, 12004, skill));
						}
					}
				}
			}
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00045FF8 File Offset: 0x000441F8
	public bool isAttack()
	{
		bool flag5 = this.checkClickToBotton(global::Char.myCharz().charFocus);
		bool result;
		if (flag5)
		{
			result = false;
		}
		else
		{
			bool flag6 = this.checkClickToBotton(global::Char.myCharz().mobFocus);
			if (flag6)
			{
				result = false;
			}
			else
			{
				bool flag7 = this.checkClickToBotton(global::Char.myCharz().npcFocus);
				if (flag7)
				{
					result = false;
				}
				else
				{
					bool isShow = ChatTextField.gI().isShow;
					if (isShow)
					{
						result = false;
					}
					else
					{
						bool flag8 = InfoDlg.isLock || global::Char.myCharz().isLockAttack || global::Char.isLockKey;
						if (flag8)
						{
							result = false;
						}
						else
						{
							bool flag9 = global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.id == 6 && global::Char.myCharz().itemFocus != null;
							if (flag9)
							{
								this.pickItem();
								result = false;
							}
							else
							{
								bool flag10 = global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type == 2 && global::Char.myCharz().npcFocus == null && global::Char.myCharz().myskill.template.id != 6;
								if (flag10)
								{
									result = this.checkSkillValid();
								}
								else
								{
									bool flag11 = global::Char.myCharz().skillPaint != null || (global::Char.myCharz().mobFocus == null && global::Char.myCharz().npcFocus == null && global::Char.myCharz().charFocus == null && global::Char.myCharz().itemFocus == null);
									if (flag11)
									{
										result = false;
									}
									else
									{
										bool flag12 = global::Char.myCharz().mobFocus != null;
										if (flag12)
										{
											bool flag13 = global::Char.myCharz().mobFocus.isBigBoss() && global::Char.myCharz().mobFocus.status == 4;
											if (flag13)
											{
												global::Char.myCharz().mobFocus = null;
												global::Char.myCharz().currentMovePoint = null;
											}
											GameScr.isAutoPlay = true;
											bool flag14 = !this.isMeCanAttackMob(global::Char.myCharz().mobFocus);
											if (flag14)
											{
												Res.outz("can not attack");
												result = false;
											}
											else
											{
												bool flag15 = this.mobCapcha != null;
												if (flag15)
												{
													result = false;
												}
												else
												{
													bool flag16 = global::Char.myCharz().myskill == null;
													if (flag16)
													{
														result = false;
													}
													else
													{
														bool flag17 = global::Char.myCharz().isSelectingSkillUseAlone();
														if (flag17)
														{
															result = false;
														}
														else
														{
															int num = -1;
															int num2 = Res.abs(global::Char.myCharz().cx - GameScr.cmx) * mGraphics.zoomLevel;
															bool flag18 = global::Char.myCharz().charFocus != null;
															if (flag18)
															{
																num = Res.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx) * mGraphics.zoomLevel;
															}
															else
															{
																bool flag19 = global::Char.myCharz().mobFocus != null;
																if (flag19)
																{
																	num = Res.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.x) * mGraphics.zoomLevel;
																}
															}
															bool flag20 = global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0 || global::Char.myCharz().myskill.template.type == 4 || num == -1 || num > num2;
															if (flag20)
															{
																bool flag21 = global::Char.myCharz().myskill.template.type == 4;
																if (flag21)
																{
																	bool flag22 = global::Char.myCharz().mobFocus.x < global::Char.myCharz().cx;
																	if (flag22)
																	{
																		global::Char.myCharz().cdir = -1;
																	}
																	else
																	{
																		global::Char.myCharz().cdir = 1;
																	}
																	this.doSelectSkill(global::Char.myCharz().myskill, true);
																}
																result = false;
															}
															else
															{
																bool flag23 = !this.checkSkillValid();
																if (flag23)
																{
																	result = false;
																}
																else
																{
																	bool flag24 = global::Char.myCharz().cx < global::Char.myCharz().mobFocus.getX();
																	if (flag24)
																	{
																		global::Char.myCharz().cdir = 1;
																	}
																	else
																	{
																		global::Char.myCharz().cdir = -1;
																	}
																	int num3 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.getX());
																	int num4 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY());
																	global::Char.myCharz().cvx = 0;
																	bool flag25 = num3 > global::Char.myCharz().myskill.dx || num4 > global::Char.myCharz().myskill.dy;
																	if (flag25)
																	{
																		bool flag = false;
																		bool flag26 = global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2;
																		if (flag26)
																		{
																			flag = true;
																		}
																		int num5 = (global::Char.myCharz().myskill.dx - ((!flag) ? 20 : 50)) * ((global::Char.myCharz().cx <= global::Char.myCharz().mobFocus.getX()) ? -1 : 1);
																		bool flag27 = num3 <= global::Char.myCharz().myskill.dx;
																		if (flag27)
																		{
																			num5 = 0;
																		}
																		global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().mobFocus.getX() + num5, global::Char.myCharz().mobFocus.getY());
																		global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
																		GameCanvas.clearKeyHold();
																		GameCanvas.clearKeyPressed();
																		result = false;
																	}
																	else
																	{
																		bool flag28 = global::Char.myCharz().myskill.template.id == 20;
																		if (flag28)
																		{
																			result = true;
																		}
																		else
																		{
																			bool flag29 = num4 > num3 && Res.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY()) > 30 && global::Char.myCharz().mobFocus.getTemplate().type == 4;
																			if (flag29)
																			{
																				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().cx + global::Char.myCharz().cdir, global::Char.myCharz().mobFocus.getY());
																				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
																				GameCanvas.clearKeyHold();
																				GameCanvas.clearKeyPressed();
																				result = false;
																			}
																			else
																			{
																				int num6 = 20;
																				bool flag2 = false;
																				bool flag30 = global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2;
																				if (flag30)
																				{
																					flag2 = true;
																				}
																				bool flag31 = global::Char.myCharz().myskill.dx > 100;
																				if (flag31)
																				{
																					num6 = 60;
																					bool flag32 = num3 < 20;
																					if (flag32)
																					{
																						global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
																					}
																				}
																				bool flag3 = false;
																				bool flag33 = (TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2;
																				if (flag33)
																				{
																					int num7 = (global::Char.myCharz().cx <= global::Char.myCharz().mobFocus.getX()) ? -1 : 1;
																					bool flag34 = (TileMap.tileTypeAtPixel(global::Char.myCharz().mobFocus.getX() + num6 * num7, global::Char.myCharz().cy + 3) & 2) != 2;
																					if (flag34)
																					{
																						flag3 = true;
																					}
																				}
																				bool flag35 = num3 <= num6 && !flag3;
																				if (flag35)
																				{
																					bool flag36 = global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX();
																					if (flag36)
																					{
																						global::Char.myCharz().cx = global::Char.myCharz().mobFocus.getX() + num6 + ((!flag2) ? 0 : 30);
																						global::Char.myCharz().cdir = -1;
																					}
																					else
																					{
																						global::Char.myCharz().cx = global::Char.myCharz().mobFocus.getX() - num6 - ((!flag2) ? 0 : 30);
																						global::Char.myCharz().cdir = 1;
																					}
																					Service.gI().charMove();
																				}
																				GameCanvas.clearKeyHold();
																				GameCanvas.clearKeyPressed();
																				result = true;
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
										else
										{
											bool flag37 = global::Char.myCharz().npcFocus != null;
											if (flag37)
											{
												bool isHide = global::Char.myCharz().npcFocus.isHide;
												if (isHide)
												{
													result = false;
												}
												else
												{
													bool flag38 = global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx;
													if (flag38)
													{
														global::Char.myCharz().cdir = 1;
													}
													else
													{
														global::Char.myCharz().cdir = -1;
													}
													bool flag39 = global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx;
													if (flag39)
													{
														global::Char.myCharz().npcFocus.cdir = -1;
													}
													else
													{
														global::Char.myCharz().npcFocus.cdir = 1;
													}
													int num8 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().npcFocus.cx);
													int num9 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().npcFocus.cy);
													bool flag40 = num9 > 40;
													if (flag40)
													{
														global::Char.myCharz().cy = global::Char.myCharz().npcFocus.cy - 40;
													}
													bool flag41 = num8 < 60;
													if (flag41)
													{
														GameCanvas.clearKeyHold();
														GameCanvas.clearKeyPressed();
														bool flag42 = this.tMenuDelay == 0;
														if (flag42)
														{
															bool flag43 = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0;
															if (flag43)
															{
																bool flag44 = global::Char.myCharz().taskMaint.index < 4 && global::Char.myCharz().npcFocus.template.npcTemplateId == 4;
																if (flag44)
																{
																	return false;
																}
																bool flag45 = global::Char.myCharz().taskMaint.index < 3 && global::Char.myCharz().npcFocus.template.npcTemplateId == 3;
																if (flag45)
																{
																	return false;
																}
															}
															this.tMenuDelay = 50;
															InfoDlg.showWait();
															Service.gI().charMove();
															Service.gI().openMenu(global::Char.myCharz().npcFocus.template.npcTemplateId);
														}
													}
													else
													{
														int num10 = (20 + Res.r.nextInt(20)) * ((global::Char.myCharz().cx <= global::Char.myCharz().npcFocus.cx) ? -1 : 1);
														global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().npcFocus.cx + num10, global::Char.myCharz().cy);
														global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
														GameCanvas.clearKeyHold();
														GameCanvas.clearKeyPressed();
													}
													result = false;
												}
											}
											else
											{
												bool flag46 = global::Char.myCharz().charFocus != null;
												if (flag46)
												{
													bool flag47 = this.mobCapcha != null;
													if (flag47)
													{
														result = false;
													}
													else
													{
														bool flag48 = global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx;
														if (flag48)
														{
															global::Char.myCharz().cdir = 1;
														}
														else
														{
															global::Char.myCharz().cdir = -1;
														}
														int num11 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx);
														int num12 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().charFocus.cy);
														bool flag49 = !global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && !global::Char.myCharz().isSelectingSkillBuffToPlayer();
														if (flag49)
														{
															bool flag50 = num11 < 60 && num12 < 40;
															if (flag50)
															{
																this.playerMenu(global::Char.myCharz().charFocus);
																bool flag51 = !GameCanvas.isTouch && global::Char.myCharz().charFocus.charID >= 0 && TileMap.mapID != 51 && TileMap.mapID != 52 && this.popUpYesNo == null;
																if (flag51)
																{
																	GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
																	GameCanvas.panel.show();
																	Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
																	Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
																}
															}
															else
															{
																int num13 = (20 + Res.r.nextInt(20)) * ((global::Char.myCharz().cx <= global::Char.myCharz().charFocus.cx) ? -1 : 1);
																global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num13, global::Char.myCharz().charFocus.cy);
																global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
																GameCanvas.clearKeyHold();
																GameCanvas.clearKeyPressed();
															}
															result = false;
														}
														else
														{
															bool flag52 = global::Char.myCharz().myskill == null;
															if (flag52)
															{
																result = false;
															}
															else
															{
																bool flag53 = !this.checkSkillValid();
																if (flag53)
																{
																	result = false;
																}
																else
																{
																	bool flag54 = global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx;
																	if (flag54)
																	{
																		global::Char.myCharz().cdir = 1;
																	}
																	else
																	{
																		global::Char.myCharz().cdir = -1;
																	}
																	global::Char.myCharz().cvx = 0;
																	bool flag55 = num11 > global::Char.myCharz().myskill.dx || num12 > global::Char.myCharz().myskill.dy;
																	if (flag55)
																	{
																		int num14 = (global::Char.myCharz().myskill.dx - 20) * ((global::Char.myCharz().cx <= global::Char.myCharz().charFocus.cx) ? -1 : 1);
																		bool flag56 = num11 <= global::Char.myCharz().myskill.dx;
																		if (flag56)
																		{
																			num14 = 0;
																		}
																		global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num14, global::Char.myCharz().charFocus.cy);
																		global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
																		GameCanvas.clearKeyHold();
																		GameCanvas.clearKeyPressed();
																		result = false;
																	}
																	else
																	{
																		bool flag57 = global::Char.myCharz().myskill.template.id == 20;
																		if (flag57)
																		{
																			result = true;
																		}
																		else
																		{
																			int num15 = 20;
																			bool flag58 = global::Char.myCharz().myskill.dx > 60;
																			if (flag58)
																			{
																				num15 = 60;
																				bool flag59 = num11 < 20;
																				if (flag59)
																				{
																					global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
																				}
																			}
																			bool flag4 = false;
																			bool flag60 = (TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2;
																			if (flag60)
																			{
																				int num16 = (global::Char.myCharz().cx <= global::Char.myCharz().charFocus.cx) ? -1 : 1;
																				bool flag61 = (TileMap.tileTypeAtPixel(global::Char.myCharz().charFocus.cx + num15 * num16, global::Char.myCharz().cy + 3) & 2) != 2;
																				if (flag61)
																				{
																					flag4 = true;
																				}
																			}
																			bool flag62 = num11 <= num15 && !flag4;
																			if (flag62)
																			{
																				bool flag63 = global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx;
																				if (flag63)
																				{
																					global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx + num15;
																					global::Char.myCharz().cdir = -1;
																				}
																				else
																				{
																					global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx - num15;
																					global::Char.myCharz().cdir = 1;
																				}
																				Service.gI().charMove();
																			}
																			GameCanvas.clearKeyHold();
																			GameCanvas.clearKeyPressed();
																			result = true;
																		}
																	}
																}
															}
														}
													}
												}
												else
												{
													bool flag64 = global::Char.myCharz().itemFocus != null;
													if (flag64)
													{
														this.pickItem();
														result = false;
													}
													else
													{
														result = true;
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
		return result;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0004702C File Offset: 0x0004522C
	public bool isMeCanAttackMob(Mob m)
	{
		bool flag = m == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = global::Char.myCharz().cTypePk == 5;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = global::Char.myCharz().isAttacPlayerStatus() && !m.isMobMe;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = global::Char.myCharz().mobMe != null && m.Equals(global::Char.myCharz().mobMe);
					if (flag4)
					{
						result = false;
					}
					else
					{
						global::Char @char = GameScr.findCharInMap(m.mobId);
						result = (@char == null || @char.cTypePk == 5 || global::Char.myCharz().isMeCanAttackOtherPlayer(@char));
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x000470D8 File Offset: 0x000452D8
	private bool checkSkillValid()
	{
		bool flag = global::Char.myCharz().myskill != null && ((global::Char.myCharz().myskill.template.manaUseType != 1 && global::Char.myCharz().cMP < global::Char.myCharz().myskill.manaUse) || (global::Char.myCharz().myskill.template.manaUseType == 1 && global::Char.myCharz().cMP < global::Char.myCharz().cMPFull * global::Char.myCharz().myskill.manaUse / 100));
		bool result;
		if (flag)
		{
			GameScr.info1.addInfo(mResources.NOT_ENOUGH_MP, 0);
			this.auto = 0;
			result = false;
		}
		else
		{
			bool flag2 = global::Char.myCharz().myskill == null || (global::Char.myCharz().myskill.template.maxPoint > 0 && global::Char.myCharz().myskill.point == 0);
			if (flag2)
			{
				GameCanvas.startOKDlg(mResources.SKILL_FAIL);
				result = false;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600031D RID: 797 RVA: 0x000471E4 File Offset: 0x000453E4
	private bool checkSkillValid2()
	{
		return (global::Char.myCharz().myskill == null || ((global::Char.myCharz().myskill.template.manaUseType == 1 || global::Char.myCharz().cMP >= global::Char.myCharz().myskill.manaUse) && (global::Char.myCharz().myskill.template.manaUseType != 1 || global::Char.myCharz().cMP >= global::Char.myCharz().cMPFull * global::Char.myCharz().myskill.manaUse / 100))) && global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.maxPoint <= 0 || global::Char.myCharz().myskill.point != 0);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x000472B0 File Offset: 0x000454B0
	public void resetButton()
	{
		GameCanvas.menu.showMenu = false;
		ChatTextField.gI().close();
		ChatTextField.gI().center = null;
		this.isLockKey = false;
		this.typeTrade = 0;
		GameScr.indexMenu = 0;
		GameScr.indexSelect = 0;
		this.indexItemUse = -1;
		GameScr.indexRow = -1;
		GameScr.indexRowMax = 0;
		GameScr.indexTitle = 0;
		this.typeTrade = (this.typeTradeOrder = 0);
		mSystem.endKey();
		bool flag = global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5;
		if (flag)
		{
			bool meDead = global::Char.myCharz().meDead;
			if (meDead)
			{
				this.cmdDead = new Command(mResources.DIES[0], 11038);
				this.center = this.cmdDead;
				global::Char.myCharz().cHP = 0;
			}
			GameScr.isHaveSelectSkill = false;
		}
		else
		{
			GameScr.isHaveSelectSkill = true;
		}
		GameScr.scrMain.clear();
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000473B2 File Offset: 0x000455B2
	public override void keyPress(int keyCode)
	{
		base.keyPress(keyCode);
	}

	// Token: 0x06000320 RID: 800 RVA: 0x000473C0 File Offset: 0x000455C0
	public override void updateKey()
	{
		bool flag = Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().isPaintNewSkill;
		if (!flag)
		{
			bool isLock = InfoDlg.isLock;
			if (!isLock)
			{
				bool flag2 = GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu;
				if (flag2)
				{
					this.updateKeyTouchControl();
				}
				this.checkAuto();
				GameCanvas.debug("F2", 0);
				bool flag3 = ChatPopup.currChatPopup != null;
				if (flag3)
				{
					Command cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
					bool flag4 = (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(cmdNextLine)) && cmdNextLine != null;
					if (flag4)
					{
						GameCanvas.isPointerJustRelease = false;
						GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
						mScreen.keyTouch = -1;
						bool flag5 = cmdNextLine != null;
						if (flag5)
						{
							cmdNextLine.performAction();
						}
					}
				}
				else
				{
					bool flag6 = !ChatTextField.gI().isShow;
					if (flag6)
					{
						bool flag7 = (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left)) && this.left != null;
						if (flag7)
						{
							GameCanvas.isPointerJustRelease = false;
							GameCanvas.isPointerClick = false;
							GameCanvas.keyPressed[12] = false;
							mScreen.keyTouch = -1;
							bool flag8 = this.left != null;
							if (flag8)
							{
								this.left.performAction();
							}
						}
						bool flag9 = (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right)) && this.right != null;
						if (flag9)
						{
							GameCanvas.isPointerJustRelease = false;
							GameCanvas.isPointerClick = false;
							GameCanvas.keyPressed[13] = false;
							mScreen.keyTouch = -1;
							bool flag10 = this.right != null;
							if (flag10)
							{
								this.right.performAction();
							}
						}
						bool flag11 = (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center)) && this.center != null;
						if (flag11)
						{
							GameCanvas.isPointerJustRelease = false;
							GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
							mScreen.keyTouch = -1;
							bool flag12 = this.center != null;
							if (flag12)
							{
								this.center.performAction();
							}
						}
					}
					else
					{
						bool flag13 = ChatTextField.gI().left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(ChatTextField.gI().left)) && ChatTextField.gI().left != null;
						if (flag13)
						{
							ChatTextField.gI().left.performAction();
						}
						bool flag14 = ChatTextField.gI().right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(ChatTextField.gI().right)) && ChatTextField.gI().right != null;
						if (flag14)
						{
							ChatTextField.gI().right.performAction();
						}
						bool flag15 = ChatTextField.gI().center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(ChatTextField.gI().center)) && ChatTextField.gI().center != null;
						if (flag15)
						{
							ChatTextField.gI().center.performAction();
						}
					}
				}
				GameCanvas.debug("F6", 0);
				this.updateKeyAlert();
				GameCanvas.debug("F7", 0);
				bool flag16 = global::Char.myCharz().currentMovePoint != null;
				if (flag16)
				{
					for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
					{
						bool flag17 = GameCanvas.keyPressed[i];
						if (flag17)
						{
							global::Char.myCharz().currentMovePoint = null;
							break;
						}
					}
				}
				GameCanvas.debug("F8", 0);
				bool flag18 = ChatTextField.gI().isShow && GameCanvas.keyAsciiPress != 0;
				if (flag18)
				{
					ChatTextField.gI().keyPressed(GameCanvas.keyAsciiPress);
					GameCanvas.keyAsciiPress = 0;
				}
				else
				{
					bool flag19 = this.isLockKey;
					if (flag19)
					{
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
					}
					else
					{
						bool flag20 = GameCanvas.menu.showMenu || this.isOpenUI() || global::Char.isLockKey;
						if (!flag20)
						{
							bool flag21 = GameCanvas.keyPressed[10];
							if (flag21)
							{
								GameCanvas.keyPressed[10] = false;
								this.doUseHP();
								GameCanvas.clearKeyPressed();
							}
							bool flag22 = GameCanvas.keyPressed[11] && this.mobCapcha == null;
							if (flag22)
							{
								bool flag23 = this.popUpYesNo != null;
								if (flag23)
								{
									this.popUpYesNo.cmdYes.performAction();
								}
								else
								{
									bool flag24 = GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null;
									if (flag24)
									{
										GameCanvas.panel.setTypeMessage();
										GameCanvas.panel.show();
									}
								}
								GameCanvas.keyPressed[11] = false;
								GameCanvas.clearKeyPressed();
							}
							bool flag25 = GameCanvas.keyAsciiPress != 0 && TField.isQwerty && GameCanvas.keyAsciiPress == 32;
							if (flag25)
							{
								this.doUseHP();
								GameCanvas.keyAsciiPress = 0;
								GameCanvas.clearKeyPressed();
							}
							bool flag26 = GameCanvas.keyAsciiPress != 0 && this.mobCapcha == null && TField.isQwerty && GameCanvas.keyAsciiPress == 121;
							if (flag26)
							{
								bool flag27 = this.popUpYesNo != null;
								if (flag27)
								{
									this.popUpYesNo.cmdYes.performAction();
									GameCanvas.keyAsciiPress = 0;
									GameCanvas.clearKeyPressed();
								}
								else
								{
									bool flag28 = GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null;
									if (flag28)
									{
										GameCanvas.panel.setTypeMessage();
										GameCanvas.panel.show();
										GameCanvas.keyAsciiPress = 0;
										GameCanvas.clearKeyPressed();
									}
								}
							}
							bool flag29 = GameCanvas.keyPressed[10] && this.mobCapcha == null;
							if (flag29)
							{
								GameCanvas.keyPressed[10] = false;
								GameScr.info2.doClick(10);
								GameCanvas.clearKeyPressed();
							}
							this.checkDrag();
							bool flag30 = !global::Char.myCharz().isFlyAndCharge;
							if (flag30)
							{
								this.checkClick();
							}
							bool flag31 = global::Char.myCharz().cmdMenu != null && global::Char.myCharz().cmdMenu.isPointerPressInside();
							if (flag31)
							{
								global::Char.myCharz().cmdMenu.performAction();
							}
							bool flag32 = global::Char.myCharz().skillPaint != null;
							if (!flag32)
							{
								bool flag33 = GameCanvas.keyAsciiPress != 0;
								if (flag33)
								{
									bool flag34 = this.mobCapcha == null;
									if (flag34)
									{
										bool isQwerty = TField.isQwerty;
										if (isQwerty)
										{
											bool flag35 = GameCanvas.keyPressed[1];
											if (flag35)
											{
												bool flag36 = GameScr.keySkill[0] != null;
												if (flag36)
												{
													this.doSelectSkill(GameScr.keySkill[0], true);
												}
											}
											else
											{
												bool flag37 = GameCanvas.keyPressed[2];
												if (flag37)
												{
													bool flag38 = GameScr.keySkill[1] != null;
													if (flag38)
													{
														this.doSelectSkill(GameScr.keySkill[1], true);
													}
												}
												else
												{
													bool flag39 = GameCanvas.keyPressed[3];
													if (flag39)
													{
														bool flag40 = GameScr.keySkill[2] != null;
														if (flag40)
														{
															this.doSelectSkill(GameScr.keySkill[2], true);
														}
													}
													else
													{
														bool flag41 = GameCanvas.keyPressed[4];
														if (flag41)
														{
															bool flag42 = GameScr.keySkill[3] != null;
															if (flag42)
															{
																this.doSelectSkill(GameScr.keySkill[3], true);
															}
														}
														else
														{
															bool flag43 = GameCanvas.keyPressed[5];
															if (flag43)
															{
																bool flag44 = GameScr.keySkill[4] != null;
																if (flag44)
																{
																	this.doSelectSkill(GameScr.keySkill[4], true);
																}
															}
															else
															{
																bool flag45 = GameCanvas.keyPressed[6];
																if (flag45)
																{
																	bool flag46 = GameScr.keySkill[5] != null;
																	if (flag46)
																	{
																		this.doSelectSkill(GameScr.keySkill[5], true);
																	}
																}
																else
																{
																	bool flag47 = GameCanvas.keyPressed[7];
																	if (flag47)
																	{
																		bool flag48 = GameScr.keySkill[6] != null;
																		if (flag48)
																		{
																			this.doSelectSkill(GameScr.keySkill[6], true);
																		}
																	}
																	else
																	{
																		bool flag49 = GameCanvas.keyPressed[8];
																		if (flag49)
																		{
																			bool flag50 = GameScr.keySkill[7] != null;
																			if (flag50)
																			{
																				this.doSelectSkill(GameScr.keySkill[7], true);
																			}
																		}
																		else
																		{
																			bool flag51 = GameCanvas.keyPressed[9];
																			if (flag51)
																			{
																				bool flag52 = GameScr.keySkill[8] != null;
																				if (flag52)
																				{
																					this.doSelectSkill(GameScr.keySkill[8], true);
																				}
																			}
																			else
																			{
																				bool flag53 = GameCanvas.keyPressed[0];
																				if (flag53)
																				{
																					bool flag54 = GameScr.keySkill[9] != null;
																					if (flag54)
																					{
																						this.doSelectSkill(GameScr.keySkill[9], true);
																					}
																				}
																				else
																				{
																					bool flag55 = GameCanvas.keyAsciiPress == 114;
																					if (flag55)
																					{
																						ChatTextField.gI().startChat(this, string.Empty);
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
										else
										{
											bool flag56 = !GameCanvas.isMoveNumberPad;
											if (flag56)
											{
												ChatTextField.gI().startChat(GameCanvas.keyAsciiPress, this, string.Empty);
											}
											else
											{
												bool flag57 = GameCanvas.keyAsciiPress == 55;
												if (flag57)
												{
													bool flag58 = GameScr.keySkill[0] != null;
													if (flag58)
													{
														this.doSelectSkill(GameScr.keySkill[0], true);
													}
												}
												else
												{
													bool flag59 = GameCanvas.keyAsciiPress == 56;
													if (flag59)
													{
														bool flag60 = GameScr.keySkill[1] != null;
														if (flag60)
														{
															this.doSelectSkill(GameScr.keySkill[1], true);
														}
													}
													else
													{
														bool flag61 = GameCanvas.keyAsciiPress == 57;
														if (flag61)
														{
															bool flag62 = GameScr.keySkill[(!Main.isPC) ? 2 : 21] != null;
															if (flag62)
															{
																this.doSelectSkill(GameScr.keySkill[2], true);
															}
														}
														else
														{
															bool flag63 = GameCanvas.keyAsciiPress == 48;
															if (flag63)
															{
																ChatTextField.gI().startChat(this, string.Empty);
															}
														}
													}
												}
											}
										}
									}
									else
									{
										char[] array = this.keyInput.ToCharArray();
										MyVector myVector = new MyVector();
										for (int j = 0; j < array.Length; j++)
										{
											myVector.addElement(array[j].ToString() + string.Empty);
										}
										myVector.removeElementAt(0);
										string text = ((char)GameCanvas.keyAsciiPress).ToString() + string.Empty;
										bool flag64 = text.Equals(string.Empty) || text == null || text.Equals("\n");
										if (flag64)
										{
											text = "-";
										}
										myVector.insertElementAt(text, myVector.size());
										this.keyInput = string.Empty;
										for (int k = 0; k < myVector.size(); k++)
										{
											this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
										}
										Service.gI().mobCapcha((char)GameCanvas.keyAsciiPress);
									}
									GameCanvas.keyAsciiPress = 0;
								}
								bool flag65 = global::Char.myCharz().statusMe == 1;
								if (flag65)
								{
									GameCanvas.debug("F10", 0);
									bool flag66 = !this.doSeleckSkillFlag;
									if (flag66)
									{
										bool flag67 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
										if (flag67)
										{
											GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
											this.doFire(false, false);
										}
										else
										{
											bool flag68 = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
											if (flag68)
											{
												bool flag69 = !global::Char.myCharz().isLockMove;
												if (flag69)
												{
													this.setCharJump(0);
												}
											}
											else
											{
												bool flag70 = GameCanvas.keyHold[1] && this.mobCapcha == null;
												if (flag70)
												{
													bool flag71 = !Main.isPC;
													if (flag71)
													{
														global::Char.myCharz().cdir = -1;
														bool flag72 = !global::Char.myCharz().isLockMove;
														if (flag72)
														{
															this.setCharJump(-4);
														}
													}
												}
												else
												{
													bool flag73 = GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] && this.mobCapcha == null;
													if (flag73)
													{
														bool flag74 = !Main.isPC;
														if (flag74)
														{
															global::Char.myCharz().cdir = 1;
															bool flag75 = !global::Char.myCharz().isLockMove;
															if (flag75)
															{
																this.setCharJump(4);
															}
														}
													}
													else
													{
														bool flag76 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
														if (flag76)
														{
															GameScr.isAutoPlay = false;
															global::Char.myCharz().isAttack = false;
															bool flag77 = global::Char.myCharz().cdir == 1;
															if (flag77)
															{
																global::Char.myCharz().cdir = -1;
															}
															else
															{
																bool flag78 = !global::Char.myCharz().isLockMove;
																if (flag78)
																{
																	bool flag79 = global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0;
																	if (flag79)
																	{
																		Service.gI().charMove();
																	}
																	global::Char.myCharz().statusMe = 2;
																	global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
																}
															}
															global::Char.myCharz().holder = false;
														}
														else
														{
															bool flag80 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
															if (flag80)
															{
																GameScr.isAutoPlay = false;
																global::Char.myCharz().isAttack = false;
																bool flag81 = global::Char.myCharz().cdir == -1;
																if (flag81)
																{
																	global::Char.myCharz().cdir = 1;
																}
																else
																{
																	bool flag82 = !global::Char.myCharz().isLockMove;
																	if (flag82)
																	{
																		bool flag83 = global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0;
																		if (flag83)
																		{
																			Service.gI().charMove();
																		}
																		global::Char.myCharz().statusMe = 2;
																		global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
																	}
																}
																global::Char.myCharz().holder = false;
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
									bool flag84 = global::Char.myCharz().statusMe == 2;
									if (flag84)
									{
										GameCanvas.debug("F11", 0);
										bool flag85 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
										if (flag85)
										{
											GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
											this.doFire(false, true);
										}
										else
										{
											bool flag86 = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
											if (flag86)
											{
												bool flag87 = global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0;
												if (flag87)
												{
													Service.gI().charMove();
												}
												global::Char.myCharz().cvy = -10;
												global::Char.myCharz().statusMe = 3;
												global::Char.myCharz().cp1 = 0;
											}
											else
											{
												bool flag88 = GameCanvas.keyHold[1] && this.mobCapcha == null;
												if (flag88)
												{
													bool isPC = Main.isPC;
													if (isPC)
													{
														bool flag89 = global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0;
														if (flag89)
														{
															Service.gI().charMove();
														}
														global::Char.myCharz().cdir = -1;
														global::Char.myCharz().cvy = -10;
														global::Char.myCharz().cvx = -4;
														global::Char.myCharz().statusMe = 3;
														global::Char.myCharz().cp1 = 0;
													}
												}
												else
												{
													bool flag90 = GameCanvas.keyHold[3] && this.mobCapcha == null;
													if (flag90)
													{
														bool flag91 = !Main.isPC;
														if (flag91)
														{
															bool flag92 = global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0;
															if (flag92)
															{
																Service.gI().charMove();
															}
															global::Char.myCharz().cdir = 1;
															global::Char.myCharz().cvy = -10;
															global::Char.myCharz().cvx = 4;
															global::Char.myCharz().statusMe = 3;
															global::Char.myCharz().cp1 = 0;
														}
													}
													else
													{
														bool flag93 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
														if (flag93)
														{
															GameScr.isAutoPlay = false;
															bool flag94 = global::Char.myCharz().cdir == 1;
															if (flag94)
															{
																global::Char.myCharz().cdir = -1;
															}
															else
															{
																global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
															}
														}
														else
														{
															bool flag95 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
															if (flag95)
															{
																GameScr.isAutoPlay = false;
																bool flag96 = global::Char.myCharz().cdir == -1;
																if (flag96)
																{
																	global::Char.myCharz().cdir = 1;
																}
																else
																{
																	global::Char.myCharz().cvx = global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
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
										bool flag97 = global::Char.myCharz().statusMe == 3;
										if (flag97)
										{
											GameScr.isAutoPlay = false;
											GameCanvas.debug("F12", 0);
											bool flag98 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
											if (flag98)
											{
												GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
												this.doFire(false, true);
											}
											bool flag99 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || (GameCanvas.keyHold[1] && this.mobCapcha == null);
											if (flag99)
											{
												bool flag100 = global::Char.myCharz().cdir == 1;
												if (flag100)
												{
													global::Char.myCharz().cdir = -1;
												}
												else
												{
													global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
												}
											}
											else
											{
												bool flag101 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] || (GameCanvas.keyHold[3] && this.mobCapcha == null);
												if (flag101)
												{
													bool flag102 = global::Char.myCharz().cdir == -1;
													if (flag102)
													{
														global::Char.myCharz().cdir = 1;
													}
													else
													{
														global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
													}
												}
											}
											bool flag103 = (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || ((GameCanvas.keyHold[1] || GameCanvas.keyHold[3]) && this.mobCapcha == null)) && global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0 && global::Char.myCharz().cp1 < 8 && global::Char.myCharz().cvy > -4;
											if (flag103)
											{
												global::Char.myCharz().cp1++;
												global::Char.myCharz().cvy = -7;
											}
										}
										else
										{
											bool flag104 = global::Char.myCharz().statusMe == 4;
											if (flag104)
											{
												GameCanvas.debug("F13", 0);
												bool flag105 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
												if (flag105)
												{
													GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
													this.doFire(false, true);
												}
												bool flag106 = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] && global::Char.myCharz().cMP > 0 && global::Char.myCharz().canFly;
												if (flag106)
												{
													GameScr.isAutoPlay = false;
													bool flag107 = (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24);
													if (flag107)
													{
														Service.gI().charMove();
													}
													global::Char.myCharz().cvy = -10;
													global::Char.myCharz().statusMe = 3;
													global::Char.myCharz().cp1 = 0;
												}
												bool flag108 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
												if (flag108)
												{
													GameScr.isAutoPlay = false;
													bool flag109 = global::Char.myCharz().cdir == 1;
													if (flag109)
													{
														global::Char.myCharz().cdir = -1;
													}
													else
													{
														global::Char.myCharz().cp1++;
														global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
														bool flag110 = global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6;
														if (flag110)
														{
															global::Char.myCharz().statusMe = 10;
															global::Char.myCharz().cp1 = 0;
															global::Char.myCharz().cvy = 0;
														}
													}
												}
												else
												{
													bool flag111 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
													if (flag111)
													{
														GameScr.isAutoPlay = false;
														bool flag112 = global::Char.myCharz().cdir == -1;
														if (flag112)
														{
															global::Char.myCharz().cdir = 1;
														}
														else
														{
															global::Char.myCharz().cp1++;
															global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
															bool flag113 = global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6;
															if (flag113)
															{
																global::Char.myCharz().statusMe = 10;
																global::Char.myCharz().cp1 = 0;
																global::Char.myCharz().cvy = 0;
															}
														}
													}
												}
											}
											else
											{
												bool flag114 = global::Char.myCharz().statusMe == 10;
												if (flag114)
												{
													GameCanvas.debug("F14", 0);
													bool flag115 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
													if (flag115)
													{
														GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
														this.doFire(false, true);
													}
													bool flag116 = global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0;
													if (flag116)
													{
														bool flag117 = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
														if (flag117)
														{
															GameScr.isAutoPlay = false;
															bool flag118 = (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24);
															if (flag118)
															{
																Service.gI().charMove();
															}
															global::Char.myCharz().cvy = -10;
															global::Char.myCharz().statusMe = 3;
															global::Char.myCharz().cp1 = 0;
														}
														else
														{
															bool flag119 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
															if (flag119)
															{
																GameScr.isAutoPlay = false;
																bool flag120 = global::Char.myCharz().cdir == 1;
																if (flag120)
																{
																	global::Char.myCharz().cdir = -1;
																}
																else
																{
																	global::Char.myCharz().cvx = -(global::Char.myCharz().cspeed + 1);
																}
															}
															else
															{
																bool flag121 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
																if (flag121)
																{
																	bool flag122 = global::Char.myCharz().cdir == -1;
																	if (flag122)
																	{
																		global::Char.myCharz().cdir = 1;
																	}
																	else
																	{
																		global::Char.myCharz().cvx = global::Char.myCharz().cspeed + 1;
																	}
																}
															}
														}
													}
												}
												else
												{
													bool flag123 = global::Char.myCharz().statusMe == 7;
													if (flag123)
													{
														GameCanvas.debug("F15", 0);
														bool flag124 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
														if (flag124)
														{
															GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
														}
														bool flag125 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
														if (flag125)
														{
															GameScr.isAutoPlay = false;
															bool flag126 = global::Char.myCharz().cdir == 1;
															if (flag126)
															{
																global::Char.myCharz().cdir = -1;
															}
															else
															{
																global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + 2;
															}
														}
														else
														{
															bool flag127 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
															if (flag127)
															{
																GameScr.isAutoPlay = false;
																bool flag128 = global::Char.myCharz().cdir == -1;
																if (flag128)
																{
																	global::Char.myCharz().cdir = 1;
																}
																else
																{
																	global::Char.myCharz().cvx = global::Char.myCharz().cspeed - 2;
																}
															}
														}
													}
												}
											}
										}
									}
								}
								GameCanvas.debug("F17", 0);
								bool flag129 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] && GameCanvas.keyAsciiPress != 56;
								if (flag129)
								{
									GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
									global::Char.myCharz().delayFall = 0;
								}
								bool flag130 = GameCanvas.keyPressed[10];
								if (flag130)
								{
									GameCanvas.keyPressed[10] = false;
									this.doUseHP();
								}
								GameCanvas.debug("F20", 0);
								GameCanvas.clearKeyPressed();
								GameCanvas.debug("F23", 0);
								this.doSeleckSkillFlag = false;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00048D1C File Offset: 0x00046F1C
	public bool isVsMap()
	{
		return true;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00048D30 File Offset: 0x00046F30
	private void checkDrag()
	{
		bool flag = GameScr.isAnalog == 1;
		if (!flag)
		{
			bool flag2 = GameScr.gamePad.disableCheckDrag();
			if (!flag2)
			{
				global::Char.myCharz().cmtoChar = true;
				bool flag3 = GameScr.isUseTouch;
				if (!flag3)
				{
					bool isPointerJustDown = GameCanvas.isPointerJustDown;
					if (isPointerJustDown)
					{
						GameCanvas.isPointerJustDown = false;
						this.isPointerDowning = true;
						this.ptDownTime = 0;
						this.ptLastDownX = (this.ptFirstDownX = GameCanvas.px);
						this.ptLastDownY = (this.ptFirstDownY = GameCanvas.py);
					}
					bool flag4 = this.isPointerDowning;
					if (flag4)
					{
						int num = GameCanvas.px - this.ptLastDownX;
						int num2 = GameCanvas.py - this.ptLastDownY;
						bool flag5 = !this.isChangingCameraMode && (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15);
						if (flag5)
						{
							this.isChangingCameraMode = true;
						}
						this.ptLastDownX = GameCanvas.px;
						this.ptLastDownY = GameCanvas.py;
						this.ptDownTime++;
						bool flag6 = this.isChangingCameraMode;
						if (flag6)
						{
							global::Char.myCharz().cmtoChar = false;
							GameScr.cmx -= num;
							GameScr.cmy -= num2;
							bool flag7 = GameScr.cmx < 24;
							if (flag7)
							{
								int num3 = (24 - GameScr.cmx) / 3;
								bool flag8 = num3 != 0;
								if (flag8)
								{
									GameScr.cmx += num - num / num3;
								}
							}
							bool flag9 = GameScr.cmx < ((!this.isVsMap()) ? 0 : 24);
							if (flag9)
							{
								GameScr.cmx = ((!this.isVsMap()) ? 0 : 24);
							}
							bool flag10 = GameScr.cmx > GameScr.cmxLim;
							if (flag10)
							{
								int num4 = (GameScr.cmx - GameScr.cmxLim) / 3;
								bool flag11 = num4 != 0;
								if (flag11)
								{
									GameScr.cmx += num - num / num4;
								}
							}
							bool flag12 = GameScr.cmx > GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0);
							if (flag12)
							{
								GameScr.cmx = GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0);
							}
							bool flag13 = GameScr.cmy < 0;
							if (flag13)
							{
								int num5 = -GameScr.cmy / 3;
								bool flag14 = num5 != 0;
								if (flag14)
								{
									GameScr.cmy += num2 - num2 / num5;
								}
							}
							bool flag15 = GameScr.cmy < -((!this.isVsMap()) ? 24 : 0);
							if (flag15)
							{
								GameScr.cmy = -((!this.isVsMap()) ? 24 : 0);
							}
							bool flag16 = GameScr.cmy > GameScr.cmyLim;
							if (flag16)
							{
								GameScr.cmy = GameScr.cmyLim;
							}
							GameScr.cmtoX = GameScr.cmx;
							GameScr.cmtoY = GameScr.cmy;
						}
					}
					bool flag17 = this.isPointerDowning && GameCanvas.isPointerJustRelease;
					if (flag17)
					{
						this.isPointerDowning = false;
						this.isChangingCameraMode = false;
						bool flag18 = Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15;
						if (flag18)
						{
							GameCanvas.isPointerJustRelease = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0004907C File Offset: 0x0004727C
	private void checkClick()
	{
		bool flag = this.isCharging();
		if (!flag)
		{
			bool flag2 = this.popUpYesNo != null && this.popUpYesNo.cmdYes != null && this.popUpYesNo.cmdYes.isPointerPressInside();
			if (flag2)
			{
				this.popUpYesNo.cmdYes.performAction();
			}
			else
			{
				bool flag3 = this.checkClickToCapcha();
				if (!flag3)
				{
					long num = mSystem.currentTimeMillis();
					bool flag4 = this.lastSingleClick != 0L;
					if (flag4)
					{
						this.lastSingleClick = 0L;
						GameCanvas.isPointerJustDown = false;
						bool flag5 = !this.disableSingleClick;
						if (flag5)
						{
							this.checkSingleClick();
							GameCanvas.isPointerJustRelease = false;
							this.isWaitingDoubleClick = true;
							this.timeStartDblClick = mSystem.currentTimeMillis();
						}
					}
					bool flag6 = this.isWaitingDoubleClick;
					if (flag6)
					{
						this.timeEndDblClick = mSystem.currentTimeMillis();
						bool flag7 = this.timeEndDblClick - this.timeStartDblClick < 300L && GameCanvas.isPointerJustRelease;
						if (flag7)
						{
							this.isWaitingDoubleClick = false;
							this.checkDoubleClick();
						}
					}
					bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
					if (isPointerJustRelease)
					{
						this.disableSingleClick = this.checkSingleClickEarly();
						this.lastSingleClick = num;
						this.lastClickCMX = GameScr.cmx;
						this.lastClickCMY = GameScr.cmy;
						GameCanvas.isPointerJustRelease = false;
					}
				}
			}
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x000491CC File Offset: 0x000473CC
	private IMapObject findClickToItem(int px, int py)
	{
		IMapObject mapObject = null;
		int num = 0;
		int num2 = 30;
		MyVector[] array = new MyVector[]
		{
			GameScr.vMob,
			GameScr.vNpc,
			GameScr.vItemMap,
			GameScr.vCharInMap
		};
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array[i].size(); j++)
			{
				IMapObject mapObject2 = (IMapObject)array[i].elementAt(j);
				bool flag = !mapObject2.isInvisible();
				if (flag)
				{
					bool flag2 = mapObject2 is Mob;
					if (flag2)
					{
						Mob mob = (Mob)mapObject2;
						bool flag3 = mob.isMobMe && mob.Equals(global::Char.myCharz().mobMe);
						if (flag3)
						{
							goto IL_15C;
						}
					}
					int x = mapObject2.getX();
					int y = mapObject2.getY();
					int w = mapObject2.getW();
					int h = mapObject2.getH();
					bool flag4 = this.inRectangle(px, py, x - w / 2 - num2, y - h - num2, w + num2 * 2, h + num2 * 2);
					if (flag4)
					{
						bool flag5 = mapObject == null;
						if (flag5)
						{
							mapObject = mapObject2;
							num = Res.abs(px - x) + Res.abs(py - y);
							bool flag6 = i == 1;
							if (flag6)
							{
								return mapObject;
							}
						}
						else
						{
							int num3 = Res.abs(px - x) + Res.abs(py - y);
							bool flag7 = num3 < num;
							if (flag7)
							{
								mapObject = mapObject2;
								num = num3;
							}
						}
					}
				}
				IL_15C:;
			}
		}
		return mapObject;
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00049374 File Offset: 0x00047574
	private Mob findClickToMOB(int px, int py)
	{
		int num = 30;
		Mob mob = null;
		int num2 = 0;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
			bool flag = !mob2.isInvisible();
			if (flag)
			{
				bool flag2 = mob2 != null;
				if (flag2)
				{
					Mob mob3 = mob2;
					bool flag3 = mob3.isMobMe && mob3.Equals(global::Char.myCharz().mobMe);
					if (flag3)
					{
						goto IL_110;
					}
				}
				int x = mob2.getX();
				int y = mob2.getY();
				int w = mob2.getW();
				int h = mob2.getH();
				bool flag4 = this.inRectangle(px, py, x - w / 2 - num, y - h - num, w + num * 2, h + num * 2);
				if (flag4)
				{
					bool flag5 = mob == null;
					if (flag5)
					{
						mob = mob2;
						num2 = Res.abs(px - x) + Res.abs(py - y);
					}
					else
					{
						int num3 = Res.abs(px - x) + Res.abs(py - y);
						bool flag6 = num3 < num2;
						if (flag6)
						{
							mob = mob2;
							num2 = num3;
						}
					}
				}
			}
			IL_110:;
		}
		return mob;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x000494B8 File Offset: 0x000476B8
	private bool inRectangle(int xClick, int yClick, int x, int y, int w, int h)
	{
		return xClick >= x && xClick <= x + w && yClick >= y && yClick <= y + h;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x000494E8 File Offset: 0x000476E8
	private bool checkSingleClickEarly()
	{
		int num = GameCanvas.px + GameScr.cmx;
		int num2 = GameCanvas.py + GameScr.cmy;
		global::Char.myCharz().cancelAttack();
		IMapObject mapObject = this.findClickToItem(num, num2);
		bool flag = mapObject == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus);
			if (flag2)
			{
				bool flag3 = !mapObject.Equals(global::Char.myCharz().charFocus.mobMe);
				if (flag3)
				{
					bool flag4 = mapObject is global::Char;
					if (flag4)
					{
						global::Char @char = (global::Char)mapObject;
						bool flag5 = @char.cTypePk != 5 && !@char.isAttacPlayerStatus();
						if (flag5)
						{
							this.checkClickMoveTo(num, num2, 2);
							return false;
						}
					}
				}
			}
			bool flag6 = global::Char.myCharz().mobFocus == mapObject || global::Char.myCharz().itemFocus == mapObject;
			if (flag6)
			{
				this.doDoubleClickToObj(mapObject);
				result = true;
			}
			else
			{
				bool flag7 = TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus);
				if (flag7)
				{
					this.checkClickMoveTo(num, num2, 3);
					result = false;
				}
				else
				{
					bool flag8 = global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null;
					if (flag8)
					{
						result = false;
					}
					else
					{
						global::Char.myCharz().focusManualTo(mapObject);
						mapObject.stopMoving();
						result = false;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00049694 File Offset: 0x00047894
	private void checkDoubleClick()
	{
		int num = GameCanvas.px + this.lastClickCMX;
		int num2 = GameCanvas.py + this.lastClickCMY;
		int cy = global::Char.myCharz().cy;
		bool flag = this.isLockKey;
		if (!flag)
		{
			IMapObject mapObject = this.findClickToItem(num, num2);
			bool flag2 = mapObject != null;
			if (flag2)
			{
				bool flag3 = mapObject is Mob && !this.isMeCanAttackMob((Mob)mapObject);
				if (flag3)
				{
					this.checkClickMoveTo(num, num2, 4);
				}
				else
				{
					bool flag4 = this.checkClickToBotton(mapObject);
					if (!flag4)
					{
						bool flag5 = !mapObject.Equals(global::Char.myCharz().npcFocus) && this.mobCapcha != null;
						if (!flag5)
						{
							bool flag6 = global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus);
							if (flag6)
							{
								bool flag7 = !mapObject.Equals(global::Char.myCharz().charFocus.mobMe);
								if (flag7)
								{
									bool flag8 = mapObject is global::Char;
									if (flag8)
									{
										global::Char @char = (global::Char)mapObject;
										bool flag9 = @char.cTypePk != 5 && !@char.isAttacPlayerStatus();
										if (flag9)
										{
											this.checkClickMoveTo(num, num2, 5);
											return;
										}
									}
								}
							}
							bool flag10 = TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus);
							if (flag10)
							{
								this.checkClickMoveTo(num, num2, 6);
							}
							else
							{
								this.doDoubleClickToObj(mapObject);
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = this.checkClickToPopup(num, num2);
				if (!flag11)
				{
					bool flag12 = this.checkClipTopChatPopUp(num, num2);
					if (!flag12)
					{
						bool isPC = Main.isPC;
						if (!isPC)
						{
							this.checkClickMoveTo(num, num2, 7);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0004986C File Offset: 0x00047A6C
	private bool checkClickToBotton(IMapObject Object)
	{
		bool flag = Object == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int i = Object.getY();
			int num = global::Char.myCharz().cy;
			bool flag2 = i < num;
			if (flag2)
			{
				while (i < num)
				{
					num -= 5;
					bool flag3 = TileMap.tileTypeAt(global::Char.myCharz().cx, num, 8192);
					if (flag3)
					{
						this.auto = 0;
						global::Char.myCharz().cancelAttack();
						global::Char.myCharz().currentMovePoint = null;
						return true;
					}
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x0600032A RID: 810 RVA: 0x000498FC File Offset: 0x00047AFC
	private void doDoubleClickToObj(IMapObject obj)
	{
		bool flag = !obj.Equals(global::Char.myCharz().npcFocus) && this.mobCapcha != null;
		if (!flag)
		{
			bool flag2 = this.checkClickToBotton(obj);
			if (!flag2)
			{
				this.checkEffToObj(obj, false);
				global::Char.myCharz().cancelAttack();
				global::Char.myCharz().currentMovePoint = null;
				global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
				obj.stopMoving();
				this.auto = 10;
				this.doFire(false, true);
				this.clickToX = obj.getX();
				this.clickToY = obj.getY();
				this.clickOnTileTop = false;
				this.clickMoving = true;
				this.clickMovingRed = true;
				this.clickMovingTimeOut = 20;
				this.clickMovingP1 = 30;
			}
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x000499D0 File Offset: 0x00047BD0
	private void checkSingleClick()
	{
		int xClick = GameCanvas.px + this.lastClickCMX;
		int yClick = GameCanvas.py + this.lastClickCMY;
		bool flag = this.isLockKey;
		if (!flag)
		{
			bool flag2 = this.checkClickToPopup(xClick, yClick);
			if (!flag2)
			{
				bool flag3 = this.checkClipTopChatPopUp(xClick, yClick);
				if (!flag3)
				{
					this.checkClickMoveTo(xClick, yClick, 0);
				}
			}
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00049A30 File Offset: 0x00047C30
	private bool checkClipTopChatPopUp(int xClick, int yClick)
	{
		bool flag = this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null;
			if (flag2)
			{
				int x = Res.abs(GameScr.info2.cmx) + GameScr.info2.info.X - 40;
				int y = Res.abs(GameScr.info2.cmy) + GameScr.info2.info.Y;
				bool flag3 = this.inRectangle(xClick - GameScr.cmx, yClick - GameScr.cmy, x, y, 200, GameScr.info2.info.H);
				if (flag3)
				{
					GameScr.info2.doClick(10);
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00049B28 File Offset: 0x00047D28
	private bool checkClickToPopup(int xClick, int yClick)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
			bool flag = this.inRectangle(xClick, yClick, popUp.cx, popUp.cy, popUp.cw, popUp.ch);
			if (flag)
			{
				bool flag2 = popUp.cy <= 24 && TileMap.isInAirMap() && global::Char.myCharz().cTypePk != 0;
				bool result;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = popUp.isPaint;
					if (!flag3)
					{
						goto IL_86;
					}
					popUp.doClick(10);
					result = true;
				}
				return result;
			}
			IL_86:;
		}
		return false;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00049BE0 File Offset: 0x00047DE0
	private void checkClickMoveTo(int xClick, int yClick, int index)
	{
		bool flag = GameScr.gamePad.disableClickMove();
		if (!flag)
		{
			global::Char.myCharz().cancelAttack();
			bool flag2 = xClick < TileMap.pxw && xClick > TileMap.pxw - 32;
			if (flag2)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
			}
			else
			{
				bool flag3 = xClick < 32 && xClick > 0;
				if (flag3)
				{
					global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
				}
				else
				{
					bool flag4 = xClick < TileMap.pxw && xClick > TileMap.pxw - 48;
					if (flag4)
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
					}
					else
					{
						bool flag5 = xClick < 48 && xClick > 0;
						if (flag5)
						{
							global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
						}
						else
						{
							this.clickToX = xClick;
							this.clickToY = yClick;
							this.clickOnTileTop = false;
							global::Char.myCharz().delayFall = 0;
							int num = (!global::Char.myCharz().canFly || global::Char.myCharz().cMP <= 0) ? 1000 : 0;
							bool flag6 = this.clickToY > global::Char.myCharz().cy && Res.abs(this.clickToX - global::Char.myCharz().cx) < 12;
							if (!flag6)
							{
								for (int i = 0; i < 60 + num; i += 24)
								{
									bool flag7 = this.clickToY + i >= TileMap.pxh - 24;
									if (flag7)
									{
										break;
									}
									bool flag8 = TileMap.tileTypeAt(this.clickToX, this.clickToY + i, 2);
									if (flag8)
									{
										this.clickToY = TileMap.tileYofPixel(this.clickToY + i);
										this.clickOnTileTop = true;
										break;
									}
								}
								for (int j = 0; j < 40 + num; j += 24)
								{
									bool flag9 = TileMap.tileTypeAt(this.clickToX, this.clickToY - j, 2);
									if (flag9)
									{
										this.clickToY = TileMap.tileYofPixel(this.clickToY - j);
										this.clickOnTileTop = true;
										break;
									}
								}
								this.clickMoving = true;
								this.clickMovingRed = false;
								this.clickMovingP1 = ((!this.clickOnTileTop) ? 30 : ((yClick >= this.clickToY) ? this.clickToY : yClick));
								global::Char.myCharz().delayFall = 0;
								bool flag10 = !this.clickOnTileTop && this.clickToY < global::Char.myCharz().cy - 50;
								if (flag10)
								{
									global::Char.myCharz().delayFall = 20;
								}
								this.clickMovingTimeOut = 30;
								this.auto = 0;
								bool holder = global::Char.myCharz().holder;
								if (holder)
								{
									global::Char.myCharz().removeHoleEff();
								}
								global::Char.myCharz().currentMovePoint = new MovePoint(this.clickToX, this.clickToY);
								global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
								global::Char.myCharz().endMovePointCommand = null;
								GameScr.isAutoPlay = false;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00049F04 File Offset: 0x00048104
	private void checkAuto()
	{
		long num = mSystem.currentTimeMillis();
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] || GameCanvas.keyPressed[1] || GameCanvas.keyPressed[3];
		if (flag)
		{
			this.auto = 0;
			GameScr.isAutoPlay = false;
		}
		bool flag2 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && !this.isPaintPopup();
		if (flag2)
		{
			bool flag3 = this.auto == 0;
			if (flag3)
			{
				bool flag4 = num - this.lastFire < 800L && this.checkSkillValid2() && (global::Char.myCharz().mobFocus != null || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)));
				if (flag4)
				{
					Res.outz("toi day");
					this.auto = 10;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
			}
			else
			{
				this.auto = 0;
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false);
			}
			this.lastFire = num;
		}
		bool flag5 = GameCanvas.gameTick % 5 == 0 && this.auto > 0 && global::Char.myCharz().currentMovePoint == null;
		if (flag5)
		{
			bool flag6 = global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.isUseAlone() || global::Char.myCharz().myskill.paintCanNotUseSkill);
			if (flag6)
			{
				return;
			}
			bool flag7 = (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.status != 1 && global::Char.myCharz().mobFocus.status != 0 && global::Char.myCharz().charFocus == null) || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus));
			if (flag7)
			{
				bool paintCanNotUseSkill = global::Char.myCharz().myskill.paintCanNotUseSkill;
				if (paintCanNotUseSkill)
				{
					return;
				}
				this.doFire(false, true);
			}
		}
		bool flag8 = this.auto > 1;
		if (flag8)
		{
			this.auto--;
		}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0004A184 File Offset: 0x00048384
	public void doUseHP()
	{
		bool stone = global::Char.myCharz().stone;
		if (!stone)
		{
			bool blindEff = global::Char.myCharz().blindEff;
			if (!blindEff)
			{
				bool flag = global::Char.myCharz().holdEffID > 0;
				if (!flag)
				{
					long num = mSystem.currentTimeMillis();
					bool flag2 = num - this.lastUsePotion < 10000L;
					if (!flag2)
					{
						bool flag3 = !global::Char.myCharz().doUsePotion();
						if (flag3)
						{
							GameScr.info1.addInfo(mResources.HP_EMPTY, 0);
						}
						else
						{
							ServerEffect.addServerEffect(11, global::Char.myCharz(), 5);
							ServerEffect.addServerEffect(104, global::Char.myCharz(), 4);
							this.lastUsePotion = num;
							SoundMn.gI().eatPeans();
						}
					}
				}
			}
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0004A248 File Offset: 0x00048448
	public void activeSuperPower(int x, int y)
	{
		bool flag = !this.isSuperPower;
		if (flag)
		{
			SoundMn.gI().bigeExlode();
			this.isSuperPower = true;
			this.tPower = 0;
			this.dxPower = 0;
			this.xPower = x - GameScr.cmx;
			this.yPower = y - GameScr.cmy;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0004A2A0 File Offset: 0x000484A0
	public void activeRongThanEff(bool isMe)
	{
		this.activeRongThan = true;
		this.isUseFreez = true;
		this.isMeCallRongThan = true;
		if (isMe)
		{
			Effect me = new Effect(20, global::Char.myCharz().cx, global::Char.myCharz().cy - 77, 2, 8, 1);
			EffecMn.addEff(me);
		}
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0004A2F3 File Offset: 0x000484F3
	public void hideRongThanEff()
	{
		this.activeRongThan = false;
		this.isUseFreez = true;
		this.isMeCallRongThan = false;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0004A30B File Offset: 0x0004850B
	public void doiMauTroi()
	{
		this.isRongThanXuatHien = true;
		this.mautroi = mGraphics.blendColor(0.4f, 0, GameCanvas.colorTop[GameCanvas.colorTop.Length - 1]);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0004A338 File Offset: 0x00048538
	public void callRongThan(int x, int y)
	{
		Res.outz(string.Concat(new object[]
		{
			"VE RONG THAN O VI TRI x= ",
			x,
			" y=",
			y
		}));
		this.doiMauTroi();
		Effect me = new Effect((!this.isRongNamek) ? 17 : 25, x, y - 77, 2, -1, 1);
		EffecMn.addEff(me);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0004A3A4 File Offset: 0x000485A4
	public void hideRongThan()
	{
		this.isRongThanXuatHien = false;
		EffecMn.removeEff(17);
		bool flag = this.isRongNamek;
		if (flag)
		{
			this.isRongNamek = false;
			EffecMn.removeEff(25);
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0004A3DC File Offset: 0x000485DC
	private void autoPlay()
	{
		bool flag3 = this.timeSkill > 0;
		if (flag3)
		{
			this.timeSkill--;
		}
		bool flag4 = !GameScr.canAutoPlay;
		if (!flag4)
		{
			bool flag5 = GameScr.isChangeZone;
			if (!flag5)
			{
				bool flag6 = global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5;
				if (!flag6)
				{
					bool flag7 = global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseChargeSkill();
					if (!flag7)
					{
						bool flag = false;
						for (int i = 0; i < GameScr.vMob.size(); i++)
						{
							Mob mob = (Mob)GameScr.vMob.elementAt(i);
							bool flag8 = mob.status != 0 && mob.status != 1;
							if (flag8)
							{
								flag = true;
							}
						}
						bool flag9 = !flag;
						if (!flag9)
						{
							bool flag2 = false;
							for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
							{
								Item item = global::Char.myCharz().arrItemBag[j];
								bool flag10 = item != null && item.template.type == 6;
								if (flag10)
								{
									flag2 = true;
									break;
								}
							}
							bool flag11 = !flag2 && GameCanvas.gameTick % 150 == 0;
							if (flag11)
							{
								Service.gI().requestPean();
							}
							bool flag12 = global::Char.myCharz().cHP <= global::Char.myCharz().cHPFull * 20 / 100 || global::Char.myCharz().cMP <= global::Char.myCharz().cMPFull * 20 / 100;
							if (flag12)
							{
								this.doUseHP();
							}
							bool flag13 = global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.isMobMe);
							if (flag13)
							{
								for (int k = 0; k < GameScr.vMob.size(); k++)
								{
									Mob mob2 = (Mob)GameScr.vMob.elementAt(k);
									bool flag14 = mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe;
									if (flag14)
									{
										global::Char.myCharz().cx = mob2.x;
										global::Char.myCharz().cy = mob2.y;
										global::Char.myCharz().mobFocus = mob2;
										Service.gI().charMove();
										break;
									}
								}
							}
							else
							{
								bool flag15 = global::Char.myCharz().mobFocus.hp <= 0 || global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0;
								if (flag15)
								{
									global::Char.myCharz().mobFocus = null;
								}
							}
							bool flag16 = global::Char.myCharz().mobFocus != null && this.timeSkill == 0;
							if (flag16)
							{
								bool flag17 = global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length && global::Char.myCharz().dart != null && global::Char.myCharz().arr != null;
								if (!flag17)
								{
									Skill skill = null;
									bool isTouch = GameCanvas.isTouch;
									if (isTouch)
									{
										for (int l = 0; l < GameScr.onScreenSkill.Length; l++)
										{
											bool flag18 = GameScr.onScreenSkill[l] != null;
											if (flag18)
											{
												bool flag19 = !GameScr.onScreenSkill[l].paintCanNotUseSkill;
												if (flag19)
												{
													bool flag20 = GameScr.onScreenSkill[l].template.id != 10;
													if (flag20)
													{
														bool flag21 = GameScr.onScreenSkill[l].template.id != 11;
														if (flag21)
														{
															bool flag22 = GameScr.onScreenSkill[l].template.id != 14;
															if (flag22)
															{
																bool flag23 = GameScr.onScreenSkill[l].template.id != 23;
																if (flag23)
																{
																	bool flag24 = GameScr.onScreenSkill[l].template.id != 7;
																	if (flag24)
																	{
																		bool flag25 = global::Char.myCharz().skillInfoPaint() == null;
																		if (flag25)
																		{
																			bool flag26 = !GameScr.onScreenSkill[l].template.isSkillSpec();
																			if (flag26)
																			{
																				bool flag27 = GameScr.onScreenSkill[l].template.manaUseType == 2;
																				int num;
																				if (flag27)
																				{
																					num = 1;
																				}
																				else
																				{
																					bool flag28 = GameScr.onScreenSkill[l].template.manaUseType != 1;
																					if (flag28)
																					{
																						num = GameScr.onScreenSkill[l].manaUse;
																					}
																					else
																					{
																						num = GameScr.onScreenSkill[l].manaUse * global::Char.myCharz().cMPFull / 100;
																					}
																				}
																				bool flag29 = global::Char.myCharz().cMP >= num;
																				if (flag29)
																				{
																					bool flag30 = skill == null;
																					if (flag30)
																					{
																						skill = GameScr.onScreenSkill[l];
																					}
																					else
																					{
																						bool flag31 = skill.coolDown < GameScr.onScreenSkill[l].coolDown;
																						if (flag31)
																						{
																							skill = GameScr.onScreenSkill[l];
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
										bool flag32 = skill != null;
										if (flag32)
										{
											this.doSelectSkill(skill, true);
											this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
										}
									}
									else
									{
										for (int m = 0; m < GameScr.keySkill.Length; m++)
										{
											bool flag33 = GameScr.keySkill[m] != null;
											if (flag33)
											{
												bool flag34 = !GameScr.keySkill[m].paintCanNotUseSkill;
												if (flag34)
												{
													bool flag35 = GameScr.keySkill[m].template.id != 10;
													if (flag35)
													{
														bool flag36 = GameScr.keySkill[m].template.id != 11;
														if (flag36)
														{
															bool flag37 = GameScr.keySkill[m].template.id != 14;
															if (flag37)
															{
																bool flag38 = GameScr.keySkill[m].template.id != 23;
																if (flag38)
																{
																	bool flag39 = GameScr.keySkill[m].template.id != 7;
																	if (flag39)
																	{
																		bool flag40 = global::Char.myCharz().skillInfoPaint() == null;
																		if (flag40)
																		{
																			bool flag41 = GameScr.keySkill[m].template.manaUseType == 2;
																			int num2;
																			if (flag41)
																			{
																				num2 = 1;
																			}
																			else
																			{
																				bool flag42 = GameScr.keySkill[m].template.manaUseType != 1;
																				if (flag42)
																				{
																					num2 = GameScr.keySkill[m].manaUse;
																				}
																				else
																				{
																					num2 = GameScr.keySkill[m].manaUse * global::Char.myCharz().cMPFull / 100;
																				}
																			}
																			bool flag43 = global::Char.myCharz().cMP >= num2;
																			if (flag43)
																			{
																				bool flag44 = skill == null;
																				if (flag44)
																				{
																					skill = GameScr.keySkill[m];
																				}
																				else
																				{
																					bool flag45 = skill.coolDown < GameScr.keySkill[m].coolDown;
																					if (flag45)
																					{
																						skill = GameScr.keySkill[m];
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
										bool flag46 = skill != null;
										if (flag46)
										{
											this.doSelectSkill(skill, true);
											this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
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

	// Token: 0x06000338 RID: 824 RVA: 0x0004ABB4 File Offset: 0x00048DB4
	private void doFire(bool isFireByShortCut, bool skipWaypoint)
	{
		GameScr.tam++;
		Waypoint waypoint = global::Char.myCharz().isInEnterOfflinePoint();
		Waypoint waypoint2 = global::Char.myCharz().isInEnterOnlinePoint();
		bool flag2 = !skipWaypoint && waypoint != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0));
		if (flag2)
		{
			waypoint.popup.command.performAction();
		}
		else
		{
			bool flag3 = !skipWaypoint && waypoint2 != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0));
			if (flag3)
			{
				waypoint2.popup.command.performAction();
			}
			else
			{
				bool flag4 = TileMap.mapID == 51 && global::Char.myCharz().npcFocus != null;
				if (!flag4)
				{
					bool flag5 = global::Char.myCharz().statusMe != 14;
					if (flag5)
					{
						global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
						bool flag6 = global::Char.myCharz().isSelectingSkillUseAlone() && global::Char.myCharz().focusToAttack();
						if (flag6)
						{
							bool flag7 = this.checkSkillValid();
							if (flag7)
							{
								global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
								global::Char.myCharz().useSkillNotFocus();
							}
						}
						else
						{
							bool flag8 = this.isAttack();
							if (flag8)
							{
								bool flag9 = global::Char.myCharz().isUseChargeSkill() && global::Char.myCharz().focusToAttack();
								if (flag9)
								{
									bool flag10 = this.checkSkillValid();
									if (flag10)
									{
										global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
										global::Char.myCharz().sendUseChargeSkill();
									}
									else
									{
										global::Char.myCharz().stopUseChargeSkill();
									}
								}
								else
								{
									bool flag = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
									global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag) ? 1 : 0);
									bool flag11 = flag;
									if (flag11)
									{
										global::Char.myCharz().delayFall = 20;
									}
									global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
								}
							}
						}
						bool flag12 = global::Char.myCharz().isSelectingSkillBuffToPlayer();
						if (flag12)
						{
							this.auto = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0004AE1C File Offset: 0x0004901C
	private void askToPick()
	{
		Npc npc = new Npc(5, 0, -100, 100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
		string nhatvatpham = mResources.nhatvatpham;
		string[] menu = new string[]
		{
			mResources.YES,
			mResources.NO
		};
		npc.idItem = 673;
		GameScr.gI().createMenu(menu, npc);
		ChatPopup.addChatPopupWithIcon(nhatvatpham, 100000, npc, 5820);
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0004AE98 File Offset: 0x00049098
	private void pickItem()
	{
		bool flag = global::Char.myCharz().itemFocus != null;
		if (flag)
		{
			bool flag2 = global::Char.myCharz().cx < global::Char.myCharz().itemFocus.x;
			if (flag2)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			int num = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().itemFocus.x);
			int num2 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().itemFocus.y);
			bool flag3 = num <= 40 && num2 < 40;
			if (flag3)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				bool flag4 = global::Char.myCharz().itemFocus.template.id != 673;
				if (flag4)
				{
					Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
				}
				else
				{
					this.askToPick();
				}
			}
			else
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0004AFF8 File Offset: 0x000491F8
	public bool isCharging()
	{
		return global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseSkillAfterCharge || global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isWaitMonkey || this.isSuperPower || global::Char.myCharz().isFreez;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0004B050 File Offset: 0x00049250
	public void doSelectSkill(Skill skill, bool isShortcut)
	{
		bool isCreateDark = global::Char.myCharz().isCreateDark;
		if (!isCreateDark)
		{
			bool flag = this.isCharging();
			if (!flag)
			{
				bool flag2 = global::Char.myCharz().taskMaint.taskId <= 1;
				if (!flag2)
				{
					global::Char.myCharz().myskill = skill;
					bool flag3 = this.lastSkill != skill && this.lastSkill != null;
					if (flag3)
					{
						Service.gI().selectSkill((int)skill.template.id);
						this.saveRMSCurrentSkill(skill.template.id);
						this.resetButton();
						this.lastSkill = skill;
						this.selectedIndexSkill = -1;
						GameScr.gI().auto = 0;
					}
					else
					{
						bool flag4 = global::Char.myCharz().isUseSkillSpec();
						if (flag4)
						{
							Res.outz(">>>use skill spec: " + skill.template.id.ToString());
							global::Char.myCharz().sendNewAttack((short)skill.template.id);
							this.saveRMSCurrentSkill(skill.template.id);
							this.resetButton();
							this.lastSkill = skill;
							this.selectedIndexSkill = -1;
							GameScr.gI().auto = 0;
						}
						else
						{
							bool flag5 = global::Char.myCharz().isSelectingSkillUseAlone();
							if (flag5)
							{
								Res.outz("use skill not focus");
								this.doUseSkillNotFocus(skill);
								this.lastSkill = skill;
							}
							else
							{
								this.selectedIndexSkill = -1;
								bool flag6 = skill != null;
								if (flag6)
								{
									Res.outz("only select skill");
									bool flag7 = this.lastSkill != skill;
									if (flag7)
									{
										Service.gI().selectSkill((int)skill.template.id);
										this.saveRMSCurrentSkill(skill.template.id);
										this.resetButton();
									}
									bool flag8 = global::Char.myCharz().charFocus == null && global::Char.myCharz().isSelectingSkillBuffToPlayer();
									if (!flag8)
									{
										bool flag9 = global::Char.myCharz().focusToAttack();
										if (flag9)
										{
											this.doFire(isShortcut, true);
											this.doSeleckSkillFlag = true;
										}
										this.lastSkill = skill;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0004B270 File Offset: 0x00049470
	public void doUseSkill(Skill skill, bool isShortcut)
	{
		bool flag = (TileMap.mapID == 112 || TileMap.mapID == 113) && global::Char.myCharz().cTypePk == 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().isSelectingSkillUseAlone();
			if (flag2)
			{
				Res.outz("HERE");
				this.doUseSkillNotFocus(skill);
			}
			else
			{
				this.selectedIndexSkill = -1;
				bool flag3 = skill != null;
				if (flag3)
				{
					Service.gI().selectSkill((int)skill.template.id);
					this.saveRMSCurrentSkill(skill.template.id);
					this.resetButton();
					global::Char.myCharz().myskill = skill;
					this.doFire(isShortcut, true);
				}
			}
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0004B320 File Offset: 0x00049520
	public void doUseSkillNotFocus(Skill skill)
	{
		bool flag = (TileMap.mapID == 112 || TileMap.mapID == 113) && global::Char.myCharz().cTypePk == 0;
		if (!flag)
		{
			bool flag2 = this.checkSkillValid();
			if (flag2)
			{
				this.selectedIndexSkill = -1;
				bool flag3 = skill != null;
				if (flag3)
				{
					Service.gI().selectSkill((int)skill.template.id);
					this.saveRMSCurrentSkill(skill.template.id);
					this.resetButton();
					global::Char.myCharz().myskill = skill;
					global::Char.myCharz().useSkillNotFocus();
					global::Char.myCharz().currentFireByShortcut = true;
					this.auto = 0;
				}
			}
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0004B3CC File Offset: 0x000495CC
	public void sortSkill()
	{
		for (int i = 0; i < global::Char.myCharz().vSkillFight.size() - 1; i++)
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			for (int j = i + 1; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill2 = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				bool flag = skill2.template.id < skill.template.id;
				if (flag)
				{
					Skill skill3 = skill2;
					skill2 = skill;
					skill = skill3;
					global::Char.myCharz().vSkillFight.setElementAt(skill, i);
					global::Char.myCharz().vSkillFight.setElementAt(skill2, j);
				}
			}
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0004B4A0 File Offset: 0x000496A0
	public void updateKeyTouchCapcha()
	{
		bool flag = this.isNotPaintTouchControl();
		if (!flag)
		{
			for (int i = 0; i < this.strCapcha.Length; i++)
			{
				this.keyCapcha[i] = -1;
				bool isTouchControl = GameCanvas.isTouchControl;
				if (isTouchControl)
				{
					int num = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2;
					int w = this.strCapcha.Length * GameScr.disXC;
					int y = GameCanvas.h - 40;
					int h = GameScr.disXC;
					bool flag2 = GameCanvas.isPointerHoldIn(num, y, w, h);
					if (flag2)
					{
						int num2 = (GameCanvas.px - num) / GameScr.disXC;
						bool flag3 = i == num2;
						if (flag3)
						{
							this.keyCapcha[i] = 1;
						}
						bool flag4 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i == num2;
						if (flag4)
						{
							char[] array = this.keyInput.ToCharArray();
							MyVector myVector = new MyVector();
							for (int j = 0; j < array.Length; j++)
							{
								myVector.addElement(array[j].ToString() + string.Empty);
							}
							myVector.removeElementAt(0);
							myVector.insertElementAt(this.strCapcha[i].ToString() + string.Empty, myVector.size());
							this.keyInput = string.Empty;
							for (int k = 0; k < myVector.size(); k++)
							{
								this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
							}
							Service.gI().mobCapcha(this.strCapcha[i]);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0004B680 File Offset: 0x00049880
	public bool checkClickToCapcha()
	{
		bool flag = this.mobCapcha == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int x = (GameCanvas.w - 5 * GameScr.disXC) / 2;
			int w = 5 * GameScr.disXC;
			int y = GameCanvas.h - 40;
			int h = GameScr.disXC;
			result = GameCanvas.isPointerHoldIn(x, y, w, h);
		}
		return result;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0004B6DC File Offset: 0x000498DC
	public void checkMouseChat()
	{
		bool flag = GameCanvas.isMouseFocus(GameScr.xC, GameScr.yC, 34, 34);
		if (flag)
		{
			bool flag2 = !TileMap.isOfflineMap();
			if (flag2)
			{
				mScreen.keyMouse = 15;
			}
		}
		else
		{
			bool flag3 = GameCanvas.isMouseFocus(GameScr.xHP, GameScr.yHP, 40, 40);
			if (flag3)
			{
				bool flag4 = global::Char.myCharz().statusMe != 14;
				if (flag4)
				{
					mScreen.keyMouse = 10;
				}
			}
			else
			{
				bool flag5 = GameCanvas.isMouseFocus(GameScr.xF, GameScr.yF, 40, 40);
				if (flag5)
				{
					bool flag6 = global::Char.myCharz().statusMe != 14;
					if (flag6)
					{
						mScreen.keyMouse = 5;
					}
				}
				else
				{
					bool flag7 = this.cmdMenu != null && GameCanvas.isMouseFocus(this.cmdMenu.x, this.cmdMenu.y, this.cmdMenu.w / 2, this.cmdMenu.h);
					if (flag7)
					{
						mScreen.keyMouse = 1;
					}
					else
					{
						mScreen.keyMouse = -1;
					}
				}
			}
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0004B7EC File Offset: 0x000499EC
	private void updateKeyTouchControl()
	{
		bool flag2 = this.isNotPaintTouchControl();
		if (!flag2)
		{
			mScreen.keyTouch = -1;
			bool isTouchControl = GameCanvas.isTouchControl;
			if (isTouchControl)
			{
				bool flag3 = GameCanvas.isPointerHoldIn(0, 0, 60, 50) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
				if (flag3)
				{
					bool flag4 = global::Char.myCharz().cmdMenu != null;
					if (flag4)
					{
						global::Char.myCharz().cmdMenu.performAction();
					}
					global::Char.myCharz().currentMovePoint = null;
					GameCanvas.clearAllPointerEvent();
					this.flareFindFocus = true;
					this.flareTime = 5;
					return;
				}
				bool isPC = Main.isPC;
				if (isPC)
				{
					this.checkMouseChat();
				}
				bool flag5 = !TileMap.isOfflineMap() && GameCanvas.isPointerHoldIn(GameScr.xC, GameScr.yC, 34, 34);
				if (flag5)
				{
					mScreen.keyTouch = 15;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					bool flag6 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
					if (flag6)
					{
						ChatTextField.gI().startChat(this, string.Empty);
						SoundMn.gI().buttonClick();
						global::Char.myCharz().currentMovePoint = null;
						GameCanvas.clearAllPointerEvent();
						return;
					}
				}
				bool flag7 = global::Char.myCharz().cmdMenu != null && GameCanvas.isPointerHoldIn(global::Char.myCharz().cmdMenu.x - 17, global::Char.myCharz().cmdMenu.y - 17, 34, 34);
				if (flag7)
				{
					mScreen.keyTouch = 20;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					bool flag8 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
					if (flag8)
					{
						GameCanvas.clearAllPointerEvent();
						global::Char.myCharz().cmdMenu.performAction();
						return;
					}
				}
				this.updateGamePad();
				bool flag9 = ((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP, 40, 40)) && global::Char.myCharz().statusMe != 14 && this.mobCapcha == null;
				if (flag9)
				{
					mScreen.keyTouch = 10;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					bool flag10 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
					if (flag10)
					{
						GameCanvas.keyPressed[10] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					}
				}
			}
			bool flag11 = this.mobCapcha != null;
			if (flag11)
			{
				this.updateKeyTouchCapcha();
			}
			else
			{
				bool flag12 = GameScr.isHaveSelectSkill;
				if (flag12)
				{
					bool flag13 = this.isCharging();
					if (flag13)
					{
						return;
					}
					this.keyTouchSkill = -1;
					bool flag = false;
					bool flag14 = GameScr.onScreenSkill.Length > 5 && (GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[5] - GameScr.wSkill / 2 + 12, GameScr.yS[5] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill));
					if (flag14)
					{
						flag = true;
					}
					bool flag15 = flag || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || (!GameCanvas.isTouchControl && GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, GameScr.wSkill, GameScr.onScreenSkill.Length * GameScr.wSkill));
					if (flag15)
					{
						GameCanvas.isPointerJustDown = false;
						this.isPointerDowning = false;
						int num = (GameCanvas.pxLast - (GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12)) / GameScr.wSkill;
						bool flag16 = flag && GameCanvas.pyLast < GameScr.yS[0];
						if (flag16)
						{
							num += 5;
						}
						this.keyTouchSkill = num;
						bool flag17 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag17)
						{
							GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
							this.selectedIndexSkill = num;
							bool flag18 = GameScr.indexSelect < 0;
							if (flag18)
							{
								GameScr.indexSelect = 0;
							}
							bool flag19 = !Main.isPC;
							if (flag19)
							{
								bool flag20 = this.selectedIndexSkill > GameScr.onScreenSkill.Length - 1;
								if (flag20)
								{
									this.selectedIndexSkill = GameScr.onScreenSkill.Length - 1;
								}
							}
							else
							{
								bool flag21 = this.selectedIndexSkill > GameScr.keySkill.Length - 1;
								if (flag21)
								{
									this.selectedIndexSkill = GameScr.keySkill.Length - 1;
								}
							}
							bool flag22 = !Main.isPC;
							Skill skill;
							if (flag22)
							{
								skill = GameScr.onScreenSkill[this.selectedIndexSkill];
							}
							else
							{
								skill = GameScr.keySkill[this.selectedIndexSkill];
							}
							bool flag23 = skill != null;
							if (flag23)
							{
								this.doSelectSkill(skill, true);
							}
						}
					}
				}
			}
			bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
			if (isPointerJustRelease)
			{
				bool flag24 = GameCanvas.keyHold[1] || GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || GameCanvas.keyHold[3] || GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
				if (flag24)
				{
					GameCanvas.isPointerJustRelease = false;
				}
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = false;
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = false;
			}
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0004BDC4 File Offset: 0x00049FC4
	public void setCharJumpAtt()
	{
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0004BDEC File Offset: 0x00049FEC
	public void setCharJump(int cvx)
	{
		bool flag = global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0;
		if (flag)
		{
			Service.gI().charMove();
		}
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().cvx = cvx;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0004BE6C File Offset: 0x0004A06C
	public void updateOpen()
	{
		bool flag = !this.isstarOpen;
		if (!flag)
		{
			bool flag2 = this.moveUp > -3;
			if (flag2)
			{
				this.moveUp -= 4;
			}
			else
			{
				this.moveUp = -2;
			}
			bool flag3 = this.moveDow < GameCanvas.h + 3;
			if (flag3)
			{
				this.moveDow += 4;
			}
			else
			{
				this.moveDow = GameCanvas.h + 2;
			}
			bool flag4 = this.moveUp <= -2 && this.moveDow >= GameCanvas.h + 2;
			if (flag4)
			{
				this.isstarOpen = false;
			}
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00003136 File Offset: 0x00001336
	public void initCreateCommand()
	{
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00003136 File Offset: 0x00001336
	public void checkCharFocus()
	{
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0004BF18 File Offset: 0x0004A118
	public void updateXoSo()
	{
		bool flag = this.tShow != 0;
		if (flag)
		{
			GameScr.currXS = mSystem.currentTimeMillis();
			bool flag2 = GameScr.currXS - GameScr.lastXS > 1000L;
			if (flag2)
			{
				GameScr.lastXS = mSystem.currentTimeMillis();
				GameScr.secondXS++;
			}
			bool flag3 = GameScr.secondXS > 20;
			if (flag3)
			{
				for (int i = 0; i < this.winnumber.Length; i++)
				{
					this.randomNumber[i] = this.winnumber[i];
				}
				this.tShow--;
				bool flag4 = this.tShow == 0;
				if (flag4)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
					GameScr.secondXS = 0;
				}
			}
			else
			{
				bool flag5 = this.moveIndex > this.winnumber.Length - 1;
				if (flag5)
				{
					this.tShow--;
					bool flag6 = this.tShow == 0;
					if (flag6)
					{
						this.yourNumber = string.Empty;
						GameScr.info1.addInfo(this.strFinish, 0);
					}
				}
				else
				{
					bool flag7 = this.moveIndex < this.randomNumber.Length;
					if (flag7)
					{
						bool flag8 = this.tMove[this.moveIndex] == 15;
						if (flag8)
						{
							bool flag9 = this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex] - 1;
							if (flag9)
							{
								this.delayMove[this.moveIndex] = 10;
							}
							bool flag10 = this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex];
							if (flag10)
							{
								this.tMove[this.moveIndex] = -1;
								this.moveIndex++;
							}
						}
						else
						{
							bool flag11 = GameCanvas.gameTick % 5 == 0;
							if (flag11)
							{
								this.tMove[this.moveIndex]++;
							}
						}
					}
					for (int j = 0; j < this.winnumber.Length; j++)
					{
						bool flag12 = this.tMove[j] != -1;
						if (flag12)
						{
							this.moveCount[j]++;
							bool flag13 = this.moveCount[j] > this.tMove[j] + this.delayMove[j];
							if (flag13)
							{
								this.moveCount[j] = 0;
								this.randomNumber[j]++;
								bool flag14 = this.randomNumber[j] >= 10;
								if (flag14)
								{
									this.randomNumber[j] = 0;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0004C1D0 File Offset: 0x0004A3D0
	public override void update()
	{
		bool flag = GameCanvas.keyPressed[16];
		if (flag)
		{
			GameCanvas.keyPressed[16] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		bool flag2 = GameCanvas.keyPressed[13] && !GameCanvas.panel.isShow;
		if (flag2)
		{
			GameCanvas.keyPressed[13] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		bool flag3 = GameCanvas.keyPressed[17];
		if (flag3)
		{
			GameCanvas.keyPressed[17] = false;
			global::Char.myCharz().searchItem();
			bool flag4 = global::Char.myCharz().itemFocus != null;
			if (flag4)
			{
				this.pickItem();
			}
		}
		bool flag5 = GameCanvas.gameTick % 100 == 0 && TileMap.mapID == 137;
		if (flag5)
		{
			GameScr.shock_scr = 30;
		}
		bool flag6 = GameScr.isAutoPlay && GameCanvas.gameTick % 20 == 0;
		if (flag6)
		{
			this.autoPlay();
		}
		this.updateXoSo();
		mSystem.checkAdComlete();
		SmallImage.update();
		try
		{
			bool isContinueToLogin = LoginScr.isContinueToLogin;
			if (isContinueToLogin)
			{
				LoginScr.isContinueToLogin = false;
			}
			bool flag7 = GameScr.tickMove == 1;
			if (flag7)
			{
				GameScr.lastTick = mSystem.currentTimeMillis();
			}
			bool flag8 = GameScr.tickMove == 100;
			if (flag8)
			{
				GameScr.tickMove = 0;
				GameScr.currTick = mSystem.currentTimeMillis();
				int second = (int)(GameScr.currTick - GameScr.lastTick) / 1000;
				Service.gI().checkMMove(second);
			}
			bool flag9 = GameScr.lockTick > 0;
			if (flag9)
			{
				GameScr.lockTick--;
				bool flag10 = GameScr.lockTick == 0;
				if (flag10)
				{
					Controller.isStopReadMessage = false;
				}
			}
			this.checkCharFocus();
			GameCanvas.debug("E1", 0);
			GameScr.updateCamera();
			GameCanvas.debug("E2", 0);
			ChatTextField.gI().update();
			GameCanvas.debug("E3", 0);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				((global::Char)GameScr.vCharInMap.elementAt(i)).update();
			}
			for (int j = 0; j < Teleport.vTeleport.size(); j++)
			{
				((Teleport)Teleport.vTeleport.elementAt(j)).update();
			}
			global::Char.myCharz().update();
			bool flag11 = global::Char.myCharz().statusMe == 1;
			if (flag11)
			{
			}
			bool flag12 = this.popUpYesNo != null;
			if (flag12)
			{
				this.popUpYesNo.update();
			}
			EffecMn.update();
			GameCanvas.debug("E5x", 0);
			for (int k = 0; k < GameScr.vMob.size(); k++)
			{
				((Mob)GameScr.vMob.elementAt(k)).update();
			}
			GameCanvas.debug("E6", 0);
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				((Npc)GameScr.vNpc.elementAt(l)).update();
			}
			this.nSkill = GameScr.onScreenSkill.Length;
			for (int m = GameScr.onScreenSkill.Length - 1; m >= 0; m--)
			{
				Skill skill = GameScr.onScreenSkill[m];
				bool flag13 = skill != null;
				if (flag13)
				{
					this.nSkill = m + 1;
					break;
				}
				this.nSkill--;
			}
			bool flag14 = this.nSkill == 1 && GameCanvas.isTouch;
			if (flag14)
			{
				GameScr.xSkill = -200;
			}
			else
			{
				bool flag15 = GameScr.xSkill < 0;
				if (flag15)
				{
					GameScr.setSkillBarPosition();
				}
			}
			GameCanvas.debug("E7", 0);
			GameCanvas.gI().updateDust();
			GameCanvas.debug("E8", 0);
			GameScr.updateFlyText();
			PopUp.updateAll();
			GameScr.updateSplash();
			this.updateSS();
			GameCanvas.updateBG();
			GameCanvas.debug("E9", 0);
			this.updateClickToArrow();
			GameCanvas.debug("E10", 0);
			for (int n = 0; n < GameScr.vItemMap.size(); n++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(n)).update();
			}
			GameCanvas.debug("E11", 0);
			GameCanvas.debug("E13", 0);
			for (int i2 = Effect2.vRemoveEffect2.size() - 1; i2 >= 0; i2--)
			{
				Effect2.vEffect2.removeElement(Effect2.vRemoveEffect2.elementAt(i2));
				Effect2.vRemoveEffect2.removeElementAt(i2);
			}
			for (int i3 = 0; i3 < Effect2.vEffect2.size(); i3++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i3);
				effect.update();
			}
			for (int i4 = 0; i4 < Effect2.vEffect2Outside.size(); i4++)
			{
				Effect2 effect2 = (Effect2)Effect2.vEffect2Outside.elementAt(i4);
				effect2.update();
			}
			for (int i5 = 0; i5 < Effect2.vAnimateEffect.size(); i5++)
			{
				Effect2 effect3 = (Effect2)Effect2.vAnimateEffect.elementAt(i5);
				effect3.update();
			}
			for (int i6 = 0; i6 < Effect2.vEffectFeet.size(); i6++)
			{
				Effect2 effect4 = (Effect2)Effect2.vEffectFeet.elementAt(i6);
				effect4.update();
			}
			for (int i7 = 0; i7 < Effect2.vEffect3.size(); i7++)
			{
				Effect2 effect5 = (Effect2)Effect2.vEffect3.elementAt(i7);
				effect5.update();
			}
			BackgroudEffect.updateEff();
			GameScr.info1.update();
			GameScr.info2.update();
			GameCanvas.debug("E15", 0);
			bool flag16 = GameScr.currentCharViewInfo != null && !GameScr.currentCharViewInfo.Equals(global::Char.myCharz());
			if (flag16)
			{
				GameScr.currentCharViewInfo.update();
			}
			this.runArrow++;
			bool flag17 = this.runArrow > 3;
			if (flag17)
			{
				this.runArrow = 0;
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
				bool flag20 = this.dHP > global::Char.myCharz().cHP;
				if (flag20)
				{
					int num = this.dHP - global::Char.myCharz().cHP >> 1;
					bool flag21 = num < 1;
					if (flag21)
					{
						num = 1;
					}
					this.dHP -= num;
				}
				else
				{
					this.dHP = global::Char.myCharz().cHP;
				}
			}
			bool flag22 = this.isInjureMp;
			if (flag22)
			{
				this.twMp++;
				bool flag23 = this.twMp == 20;
				if (flag23)
				{
					this.twMp = 0;
					this.isInjureMp = false;
				}
			}
			else
			{
				bool flag24 = this.dMP > global::Char.myCharz().cMP;
				if (flag24)
				{
					int num2 = this.dMP - global::Char.myCharz().cMP >> 1;
					bool flag25 = num2 < 1;
					if (flag25)
					{
						num2 = 1;
					}
					this.dMP -= num2;
				}
				else
				{
					this.dMP = global::Char.myCharz().cMP;
				}
			}
			bool flag26 = this.tMenuDelay > 0;
			if (flag26)
			{
				this.tMenuDelay--;
			}
			bool flag27 = this.isRongThanMenu();
			if (flag27)
			{
				int num3 = 100;
				while (this.yR - num3 < GameScr.cmy)
				{
					GameScr.cmy--;
				}
			}
			for (int i8 = 0; i8 < global::Char.vItemTime.size(); i8++)
			{
				((ItemTime)global::Char.vItemTime.elementAt(i8)).update();
			}
			for (int i9 = 0; i9 < GameScr.textTime.size(); i9++)
			{
				((ItemTime)GameScr.textTime.elementAt(i9)).update();
			}
			this.updateChatVip();
		}
		catch (Exception ex)
		{
		}
		int num4 = GameCanvas.gameTick % 4000;
		bool flag28 = num4 == 1000;
		if (flag28)
		{
			GameScr.checkRemoveImage();
		}
		EffectManager.update();
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00003136 File Offset: 0x00001336
	public void updateKeyChatPopUp()
	{
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0004CA84 File Offset: 0x0004AC84
	public bool isRongThanMenu()
	{
		return this.isMeCallRongThan;
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0004CA9C File Offset: 0x0004AC9C
	public void paintEffect(mGraphics g)
	{
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			bool flag = effect != null && !(effect is ChatPopup);
			if (flag)
			{
				effect.paint(g);
			}
		}
		bool flag2 = !GameCanvas.lowGraphic;
		if (flag2)
		{
			for (int j = 0; j < Effect2.vAnimateEffect.size(); j++)
			{
				Effect2 effect2 = (Effect2)Effect2.vAnimateEffect.elementAt(j);
				effect2.paint(g);
			}
		}
		for (int k = 0; k < Effect2.vEffect2Outside.size(); k++)
		{
			Effect2 effect3 = (Effect2)Effect2.vEffect2Outside.elementAt(k);
			effect3.paint(g);
		}
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0004CB80 File Offset: 0x0004AD80
	public void paintBgItem(mGraphics g, int layer)
	{
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			bool flag = bgItem.idImage != -1 && (int)bgItem.layer == layer;
			if (flag)
			{
				bgItem.paint(g);
			}
		}
		bool flag2 = TileMap.mapID == 48 && layer == 3 && GameCanvas.bgW != null && GameCanvas.bgW[0] != 0;
		if (flag2)
		{
			for (int j = 0; j < TileMap.pxw / GameCanvas.bgW[0] + 1; j++)
			{
				g.drawImage(GameCanvas.imgBG[0], j * GameCanvas.bgW[0], TileMap.pxh - GameCanvas.bgH[0] - 70, 0);
			}
		}
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0004CC58 File Offset: 0x0004AE58
	public void paintBlackSky(mGraphics g)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			g.fillTrans(GameScr.imgTrans, 0, 0, GameCanvas.w, GameCanvas.h);
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0004CC8C File Offset: 0x0004AE8C
	public void paintCapcha(mGraphics g)
	{
		MobCapcha.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy);
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool showMenu = GameCanvas.menu.showMenu;
		if (!showMenu)
		{
			bool isShow = GameCanvas.panel.isShow;
			if (!isShow)
			{
				bool flag = ChatPopup.currChatPopup != null;
				if (!flag)
				{
					bool isTouch = GameCanvas.isTouch;
					if (isTouch)
					{
						for (int i = 0; i < this.strCapcha.Length; i++)
						{
							int x = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2 + i * GameScr.disXC + GameScr.disXC / 2;
							bool flag2 = this.keyCapcha[i] == -1;
							if (flag2)
							{
								g.drawImage(GameScr.imgNut, x, GameCanvas.h - 25, 3);
								mFont.tahoma_7b_dark.drawString(g, this.strCapcha[i].ToString() + string.Empty, x, GameCanvas.h - 30, 2);
							}
							else
							{
								g.drawImage(GameScr.imgNutF, x, GameCanvas.h - 25, 3);
								mFont.tahoma_7b_green2.drawString(g, this.strCapcha[i].ToString() + string.Empty, x, GameCanvas.h - 30, 2);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0004CE1C File Offset: 0x0004B01C
	public override void paint(mGraphics g)
	{
		GameScr.countEff = 0;
		bool flag2 = !GameScr.isPaint;
		if (!flag2)
		{
			GameCanvas.debug("PA1", 1);
			bool flag3 = this.isFreez || (this.isUseFreez && ChatPopup.currChatPopup == null);
			if (flag3)
			{
				this.dem++;
				bool flag4 = (this.dem < 30 && this.dem >= 0 && GameCanvas.gameTick % 4 == 0) || (this.dem >= 30 && this.dem <= 50 && GameCanvas.gameTick % 3 == 0) || this.dem > 50;
				if (flag4)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					bool flag5 = this.dem > 50;
					if (flag5)
					{
						bool flag6 = this.isUseFreez;
						if (flag6)
						{
							this.isUseFreez = false;
							this.dem = 0;
							bool flag7 = this.activeRongThan;
							if (flag7)
							{
								this.callRongThan(this.xR, this.yR);
							}
							else
							{
								this.hideRongThan();
							}
						}
						this.paintInfoBar(g);
						g.translate(-GameScr.cmx, -GameScr.cmy);
						g.translate(0, GameCanvas.transY);
						global::Char.myCharz().paint(g);
						mSystem.paintFlyText(g);
						GameScr.resetTranslate(g);
						this.paintSelectedSkill(g);
						return;
					}
					return;
				}
			}
			GameCanvas.debug("PA2", 1);
			GameCanvas.paintBGGameScr(g);
			this.paint_ios_bg(g);
			bool flag8 = (this.isRongThanXuatHien || this.isFireWorks) && TileMap.bgID != 3;
			if (flag8)
			{
				this.paintBlackSky(g);
			}
			GameCanvas.debug("PA3", 1);
			bool flag9 = GameScr.shock_scr > 0;
			if (flag9)
			{
				g.translate(-GameScr.cmx + GameScr.shock_x[GameScr.shock_scr % GameScr.shock_x.Length], -GameScr.cmy + GameScr.shock_y[GameScr.shock_scr % GameScr.shock_y.Length]);
				GameScr.shock_scr--;
			}
			else
			{
				g.translate(-GameScr.cmx, -GameScr.cmy);
			}
			bool flag10 = this.isSuperPower;
			if (flag10)
			{
				int tx = (GameCanvas.gameTick % 3 != 0) ? -3 : 3;
				g.translate(tx, 0);
			}
			BackgroudEffect.paintBehindTileAll(g);
			EffecMn.paintLayer1(g);
			TileMap.paintTilemap(g);
			TileMap.paintOutTilemap(g);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				bool flag11 = @char.isMabuHold && TileMap.mapID == 128;
				if (flag11)
				{
					@char.paintHeadWithXY(g, @char.cx, @char.cy, 0);
				}
			}
			bool flag12 = global::Char.myCharz().isMabuHold && TileMap.mapID == 128;
			if (flag12)
			{
				global::Char.myCharz().paintHeadWithXY(g, global::Char.myCharz().cx, global::Char.myCharz().cy, 0);
			}
			this.paintBgItem(g, 2);
			bool flag13 = global::Char.myCharz().cmdMenu != null && GameCanvas.isTouch;
			if (flag13)
			{
				bool flag14 = mScreen.keyTouch == 20;
				if (flag14)
				{
					g.drawImage(GameScr.imgChat2, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else
				{
					g.drawImage(GameScr.imgChat, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
			GameCanvas.debug("PA4", 1);
			GameCanvas.debug("PA5", 1);
			BackgroudEffect.paintBackAll(g);
			EffectManager.lowEffects.paintAll(g);
			for (int j = 0; j < Effect2.vEffectFeet.size(); j++)
			{
				Effect2 effect = (Effect2)Effect2.vEffectFeet.elementAt(j);
				effect.paint(g);
			}
			for (int k = 0; k < Teleport.vTeleport.size(); k++)
			{
				((Teleport)Teleport.vTeleport.elementAt(k)).paintHole(g);
			}
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(l);
				bool flag15 = npc.cHP > 0;
				if (flag15)
				{
					npc.paintShadow(g);
				}
			}
			for (int m = 0; m < GameScr.vNpc.size(); m++)
			{
				((Npc)GameScr.vNpc.elementAt(m)).paint(g);
			}
			g.translate(0, GameCanvas.transY);
			GameCanvas.debug("PA7", 1);
			GameCanvas.debug("PA8", 1);
			for (int n = 0; n < GameScr.vCharInMap.size(); n++)
			{
				global::Char char2 = null;
				try
				{
					char2 = (global::Char)GameScr.vCharInMap.elementAt(n);
				}
				catch (Exception ex)
				{
					Cout.LogError("Loi ham paint char gamesc: " + ex.ToString());
				}
				bool flag16 = char2 != null;
				if (flag16)
				{
					bool flag17 = !GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop();
					if (flag17)
					{
						bool isShadown = char2.isShadown;
						if (isShadown)
						{
							char2.paintShadow(g);
						}
					}
				}
			}
			global::Char.myCharz().paintShadow(g);
			EffecMn.paintLayer2(g);
			for (int i2 = 0; i2 < GameScr.vMob.size(); i2++)
			{
				((Mob)GameScr.vMob.elementAt(i2)).paint(g);
			}
			for (int i3 = 0; i3 < Teleport.vTeleport.size(); i3++)
			{
				((Teleport)Teleport.vTeleport.elementAt(i3)).paint(g);
			}
			for (int i4 = 0; i4 < GameScr.vCharInMap.size(); i4++)
			{
				global::Char char3 = null;
				try
				{
					char3 = (global::Char)GameScr.vCharInMap.elementAt(i4);
				}
				catch (Exception ex2)
				{
				}
				bool flag18 = char3 != null;
				if (flag18)
				{
					bool flag19 = !GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop();
					if (flag19)
					{
						char3.paint(g);
					}
				}
			}
			global::Char.myCharz().paint(g);
			bool flag20 = global::Char.myCharz().skillPaint != null && global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length;
			if (flag20)
			{
				global::Char.myCharz().paintCharWithSkill(g);
				global::Char.myCharz().paintMount2(g);
			}
			for (int i5 = 0; i5 < GameScr.vCharInMap.size(); i5++)
			{
				global::Char char4 = null;
				try
				{
					char4 = (global::Char)GameScr.vCharInMap.elementAt(i5);
				}
				catch (Exception ex3)
				{
					Cout.LogError("Loi ham paint char gamescr: " + ex3.ToString());
				}
				bool flag21 = char4 != null;
				if (flag21)
				{
					bool flag22 = !GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop();
					if (flag22)
					{
						bool flag23 = char4.skillPaint != null && char4.skillInfoPaint() != null && char4.indexSkill < char4.skillInfoPaint().Length;
						if (flag23)
						{
							char4.paintCharWithSkill(g);
							char4.paintMount2(g);
						}
					}
				}
			}
			for (int i6 = 0; i6 < GameScr.vItemMap.size(); i6++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(i6)).paint(g);
			}
			g.translate(0, -GameCanvas.transY);
			GameCanvas.debug("PA9", 1);
			GameScr.paintSplash(g);
			GameCanvas.debug("PA10", 1);
			GameCanvas.debug("PA11", 1);
			GameCanvas.debug("PA13", 1);
			this.paintEffect(g);
			this.paintBgItem(g, 3);
			for (int i7 = 0; i7 < GameScr.vNpc.size(); i7++)
			{
				Npc npc2 = (Npc)GameScr.vNpc.elementAt(i7);
				npc2.paintName(g);
			}
			EffecMn.paintLayer3(g);
			for (int i8 = 0; i8 < GameScr.vNpc.size(); i8++)
			{
				Npc npc3 = (Npc)GameScr.vNpc.elementAt(i8);
				bool flag24 = npc3.chatInfo != null;
				if (flag24)
				{
					bool flag25 = npc3 != null;
					if (flag25)
					{
						npc3.chatInfo.paint(g, npc3.cx, npc3.cy - npc3.ch - GameCanvas.transY, npc3.cdir);
					}
				}
			}
			for (int i9 = 0; i9 < GameScr.vCharInMap.size(); i9++)
			{
				global::Char char5 = null;
				try
				{
					char5 = (global::Char)GameScr.vCharInMap.elementAt(i9);
				}
				catch (Exception ex4)
				{
				}
				bool flag26 = char5 != null;
				if (flag26)
				{
					bool flag27 = char5.chatInfo != null;
					if (flag27)
					{
						char5.chatInfo.paint(g, char5.cx, char5.cy - char5.ch, char5.cdir);
					}
				}
			}
			bool flag28 = global::Char.myCharz().chatInfo != null;
			if (flag28)
			{
				global::Char.myCharz().chatInfo.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, global::Char.myCharz().cdir);
			}
			EffectManager.mid_2Effects.paintAll(g);
			EffectManager.midEffects.paintAll(g);
			BackgroudEffect.paintFrontAll(g);
			for (int j2 = 0; j2 < TileMap.vCurrItem.size(); j2++)
			{
				BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(j2);
				bool flag29 = bgItem.idImage != -1 && bgItem.layer > 3;
				if (flag29)
				{
					bgItem.paint(g);
				}
			}
			PopUp.paintAll(g);
			bool flag30 = TileMap.mapID == 120;
			if (flag30)
			{
				bool flag31 = this.percentMabu != 100;
				if (flag31)
				{
					int w = (int)this.percentMabu * mGraphics.getImageWidth(GameScr.imgHPLost) / 100;
					int num = (int)this.percentMabu;
					g.drawImage(GameScr.imgHPLost, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
					g.setClip(TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, w, 10);
					g.drawImage(GameScr.imgHP, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
					g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				}
				bool flag32 = this.mabuEff;
				if (flag32)
				{
					this.tMabuEff++;
					bool flag33 = GameCanvas.gameTick % 3 == 0;
					if (flag33)
					{
						Effect me = new Effect(19, Res.random(TileMap.pxw / 2 - 50, TileMap.pxw / 2 + 50), 340, 2, 1, -1);
						EffecMn.addEff(me);
					}
					bool flag34 = GameCanvas.gameTick % 15 == 0;
					if (flag34)
					{
						Effect me2 = new Effect(18, Res.random(TileMap.pxw / 2 - 5, TileMap.pxw / 2 + 5), Res.random(300, 320), 2, 1, -1);
						EffecMn.addEff(me2);
					}
					bool flag35 = this.tMabuEff == 100;
					if (flag35)
					{
						this.activeSuperPower(TileMap.pxw / 2, 300);
					}
					bool flag36 = this.tMabuEff == 110;
					if (flag36)
					{
						this.tMabuEff = 0;
						this.mabuEff = false;
					}
				}
			}
			BackgroudEffect.paintFog(g);
			bool flag = true;
			for (int i10 = 0; i10 < BackgroudEffect.vBgEffect.size(); i10++)
			{
				BackgroudEffect backgroudEffect = (BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i10);
				bool flag37 = backgroudEffect.typeEff == 0;
				if (flag37)
				{
					flag = false;
					break;
				}
			}
			bool flag38 = mGraphics.zoomLevel <= 1 || Main.isIpod || Main.isIphone4;
			if (flag38)
			{
				flag = false;
			}
			bool flag39 = flag && !this.isRongThanXuatHien;
			if (flag39)
			{
				int num2 = TileMap.pxw / (mGraphics.getImageWidth(TileMap.imgLight) + 50);
				bool flag40 = num2 <= 0;
				if (flag40)
				{
					num2 = 1;
				}
				bool flag41 = TileMap.tileID != 28;
				if (flag41)
				{
					for (int i11 = 0; i11 < num2; i11++)
					{
						int num3 = 100 + i11 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2;
						int num4 = -20;
						int imageWidth = mGraphics.getImageWidth(TileMap.imgLight);
						bool flag42 = num3 + imageWidth >= GameScr.cmx && num3 <= GameScr.cmx + GameCanvas.w && num4 + mGraphics.getImageHeight(TileMap.imgLight) >= GameScr.cmy && num4 <= GameScr.cmy + GameCanvas.h;
						if (flag42)
						{
							g.drawImage(TileMap.imgLight, 100 + i11 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2, num4, 0);
						}
					}
				}
			}
			mSystem.paintFlyText(g);
			GameCanvas.debug("PA14", 1);
			GameCanvas.debug("PA15", 1);
			GameCanvas.debug("PA16", 1);
			this.paintArrowPointToNPC(g);
			GameCanvas.debug("PA17", 1);
			bool flag43 = !GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow;
			if (flag43)
			{
				this.paintInfoBar(g);
			}
			GameScr.resetTranslate(g);
			this.paint_xp_bar(g);
			bool flag44 = !GameScr.isPaintOther;
			if (flag44)
			{
				bool open3Hour = GameCanvas.open3Hour;
				if (open3Hour)
				{
					bool flag45 = GameCanvas.w > 250;
					if (flag45)
					{
						g.drawImage(GameCanvas.img12, 160, 6, 0);
						mFont.tahoma_7_white.drawString(g, "Dành cho người chơi trên 12 tuổi.", 180, 2, 0);
						mFont.tahoma_7_white.drawString(g, "Chơi quá 180 phút mỗi ngày ", 180, 12, 0);
						mFont.tahoma_7_white.drawString(g, "sẽ hại sức khỏe.", 180, 22, 0);
					}
					else
					{
						g.drawImage(GameCanvas.img12, 5, GameCanvas.h - 67, 0);
						mFont.tahoma_7_white.drawString(g, "Dành cho người chơi trên 12 tuổi.", 25, GameCanvas.h - 70, 0);
						mFont.tahoma_7_white.drawString(g, "Chơi quá 180 phút mỗi ngày sẽ hại sức khỏe.", 25, GameCanvas.h - 60, 0);
					}
				}
				GameCanvas.debug("PA21", 1);
				GameCanvas.debug("PA18", 1);
				g.translate(-g.getTranslateX(), -g.getTranslateY());
				bool flag46 = (TileMap.mapID == 128 || TileMap.mapID == 127) && GameScr.mabuPercent != 0;
				if (flag46)
				{
					int num5 = 30;
					int num6 = 200;
					g.setColor(0);
					g.fillRect(num5 - 27, num6 - 112, 54, 8);
					g.setColor(16711680);
					g.setClip(num5 - 25, num6 - 110, (int)GameScr.mabuPercent, 4);
					g.fillRect(num5 - 25, num6 - 110, 50, 4);
					g.setClip(0, 0, 3000, 3000);
					mFont.tahoma_7b_white.drawString(g, "Mabu", num5, num6 - 112 + 10, 2, mFont.tahoma_7b_dark);
				}
				bool isFusion = global::Char.myCharz().isFusion;
				if (isFusion)
				{
					global::Char.myCharz().tFusion++;
					bool flag47 = GameCanvas.gameTick % 3 == 0;
					if (flag47)
					{
						g.setColor(16777215);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					}
					bool flag48 = global::Char.myCharz().tFusion >= 100;
					if (flag48)
					{
						global::Char.myCharz().fusionComplete();
					}
				}
				for (int i12 = 0; i12 < GameScr.vCharInMap.size(); i12++)
				{
					global::Char char6 = null;
					try
					{
						char6 = (global::Char)GameScr.vCharInMap.elementAt(i12);
					}
					catch (Exception ex5)
					{
					}
					bool flag49 = char6 != null;
					if (flag49)
					{
						bool flag50 = char6.isFusion && global::Char.isCharInScreen(char6);
						if (flag50)
						{
							char6.tFusion++;
							bool flag51 = GameCanvas.gameTick % 3 == 0;
							if (flag51)
							{
								g.setColor(16777215);
								g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							}
							bool flag52 = char6.tFusion >= 100;
							if (flag52)
							{
								char6.fusionComplete();
							}
						}
					}
				}
				GameCanvas.paintz.paintTabSoft(g);
				GameCanvas.debug("PA19", 1);
				GameCanvas.debug("PA20", 1);
				GameScr.resetTranslate(g);
				this.paintSelectedSkill(g);
				GameCanvas.debug("PA22", 1);
				GameScr.resetTranslate(g);
				bool flag53 = GameCanvas.isTouch && GameCanvas.isTouchControl;
				if (flag53)
				{
					this.paintTouchControl(g);
				}
				GameScr.resetTranslate(g);
				this.paintChatVip(g);
				bool flag54 = !GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && GameCanvas.currentScreen.Equals(GameScr.instance);
				if (flag54)
				{
					base.paint(g);
					bool flag55 = mScreen.keyMouse == 1 && this.cmdMenu != null;
					if (flag55)
					{
						g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 7, this.cmdMenu.y + 15, 3);
					}
				}
				GameScr.resetTranslate(g);
				int num7 = 100 + ((global::Char.vItemTime.size() == 0) ? 0 : (GameScr.textTime.size() * 12));
				bool flag56 = global::Char.myCharz().clan != null;
				if (flag56)
				{
					int num8 = 0;
					int num9 = 0;
					int num10 = (GameCanvas.h - 100 - 60) / 12;
					for (int i13 = 0; i13 < GameScr.vCharInMap.size(); i13++)
					{
						global::Char char7 = (global::Char)GameScr.vCharInMap.elementAt(i13);
						bool flag57 = char7.clanID != -1 && char7.clanID == global::Char.myCharz().clan.ID;
						if (flag57)
						{
							bool flag58 = char7.isOutX() && char7.cx < global::Char.myCharz().cx;
							if (flag58)
							{
								int num11 = num10;
								bool flag59 = global::Char.vItemTime.size() != 0;
								if (flag59)
								{
									num11 -= GameScr.textTime.size();
								}
								bool flag60 = num8 <= num11;
								if (flag60)
								{
									mFont.tahoma_7_green.drawString(g, char7.cName, 20, num7 - 12 + num8 * 12, mFont.LEFT, mFont.tahoma_7_grey);
									char7.paintHp(g, 10, num7 + num8 * 12 - 5);
									num8++;
								}
							}
							else
							{
								bool flag61 = char7.isOutX() && char7.cx > global::Char.myCharz().cx;
								if (flag61)
								{
									bool flag62 = num9 <= num10;
									if (flag62)
									{
										mFont.tahoma_7_green.drawString(g, char7.cName, GameCanvas.w - 25, num7 - 12 + num9 * 12, mFont.RIGHT, mFont.tahoma_7_grey);
										char7.paintHp(g, GameCanvas.w - 15, num7 + num9 * 12 - 5);
										num9++;
									}
								}
							}
						}
					}
				}
				ChatTextField.gI().paint(g);
				bool flag63 = GameScr.isNewClanMessage && !GameCanvas.panel.isShow && GameCanvas.gameTick % 4 == 0;
				if (flag63)
				{
					g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 15, this.cmdMenu.y + 30, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				bool flag64 = this.isSuperPower;
				if (flag64)
				{
					this.dxPower += 5;
					bool flag65 = this.tPower >= 0;
					if (flag65)
					{
						this.tPower += this.dxPower;
					}
					Res.outz("x power= " + this.xPower.ToString());
					bool flag66 = this.tPower < 0;
					if (flag66)
					{
						this.tPower--;
						bool flag67 = this.tPower == -20;
						if (flag67)
						{
							this.isSuperPower = false;
							this.tPower = 0;
							this.dxPower = 0;
						}
					}
					else
					{
						bool flag68 = (this.xPower - this.tPower > 0 || this.tPower < TileMap.pxw) && this.tPower > 0;
						if (flag68)
						{
							g.setColor(16777215);
							bool flag69 = !GameCanvas.lowGraphic;
							if (flag69)
							{
								g.fillArg(0, 0, GameCanvas.w, GameCanvas.h, 0, 0);
							}
							else
							{
								g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							}
						}
						else
						{
							this.tPower = -1;
						}
					}
				}
				for (int i14 = 0; i14 < global::Char.vItemTime.size(); i14++)
				{
					((ItemTime)global::Char.vItemTime.elementAt(i14)).paint(g, this.cmdMenu.x + 32 + i14 * 24, 55);
				}
				for (int i15 = 0; i15 < GameScr.textTime.size(); i15++)
				{
					((ItemTime)GameScr.textTime.elementAt(i15)).paintText(g, this.cmdMenu.x + ((global::Char.vItemTime.size() == 0) ? 25 : 5), ((global::Char.vItemTime.size() == 0) ? 45 : 90) + i15 * 12);
				}
				this.paintXoSo(g);
				bool flag70 = mResources.language == 1;
				if (flag70)
				{
					long second = mSystem.currentTimeMillis() - GameScr.deltaTime;
					mFont.tahoma_7b_white.drawString(g, NinjaUtil.getDate2(second), 10, GameCanvas.h - 65, 0, mFont.tahoma_7b_dark);
				}
				bool flag71 = !this.yourNumber.Equals(string.Empty);
				if (flag71)
				{
					for (int i16 = 0; i16 < this.strPaint.Length; i16++)
					{
						mFont.tahoma_7b_white.drawString(g, this.strPaint[i16], 5, 85 + i16 * 18, 0, mFont.tahoma_7b_dark);
					}
				}
			}
			int num12 = 0;
			int num13 = GameCanvas.hw;
			bool flag72 = num13 > 200;
			if (flag72)
			{
				num13 = 200;
			}
			this.paintPhuBanBar(g, num12 + GameCanvas.w / 2, 0, num13);
			EffectManager.hiEffects.paintAll(g);
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0004E640 File Offset: 0x0004C840
	private void paintXoSo(mGraphics g)
	{
		bool flag = this.tShow != 0;
		if (flag)
		{
			string text = string.Empty;
			for (int i = 0; i < this.winnumber.Length; i++)
			{
				text = text + this.randomNumber[i].ToString() + " ";
			}
			PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, false);
			mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
			mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0004E6E4 File Offset: 0x0004C8E4
	private void checkEffToObj(IMapObject obj, bool isnew)
	{
		bool flag = obj == null;
		if (!flag)
		{
			bool flag2 = this.tDoubleDelay > 0;
			if (!flag2)
			{
				this.tDoubleDelay = 10;
				int x = obj.getX();
				int num = Res.abs(global::Char.myCharz().cx - x);
				bool flag3 = num <= 80;
				int loopCount;
				if (flag3)
				{
					loopCount = 1;
				}
				else
				{
					bool flag4 = num > 80 && num <= 200;
					if (flag4)
					{
						loopCount = 2;
					}
					else
					{
						bool flag5 = num > 200 && num <= 400;
						if (flag5)
						{
							loopCount = 3;
						}
						else
						{
							loopCount = 4;
						}
					}
				}
				bool flag6 = !isnew;
				if (flag6)
				{
					bool flag7 = obj.Equals(global::Char.myCharz().mobFocus) || (obj.Equals(global::Char.myCharz().charFocus) && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus));
					if (flag7)
					{
						ServerEffect.addServerEffect(135, obj.getX(), obj.getY(), loopCount);
					}
					else
					{
						bool flag8 = obj.Equals(global::Char.myCharz().npcFocus) || obj.Equals(global::Char.myCharz().itemFocus) || obj.Equals(global::Char.myCharz().charFocus);
						if (flag8)
						{
							ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), loopCount);
						}
					}
				}
				else
				{
					ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), loopCount);
				}
			}
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0004E874 File Offset: 0x0004CA74
	private void updateClickToArrow()
	{
		bool flag = this.tDoubleDelay > 0;
		if (flag)
		{
			this.tDoubleDelay--;
		}
		bool flag2 = this.clickMoving;
		if (flag2)
		{
			this.clickMoving = false;
			IMapObject mapObject = this.findClickToItem(this.clickToX, this.clickToY);
			bool flag3 = mapObject == null || (mapObject != null && mapObject.Equals(global::Char.myCharz().npcFocus) && TileMap.mapID == 51);
			if (flag3)
			{
				ServerEffect.addServerEffect(134, this.clickToX, this.clickToY + GameCanvas.transY / 2, 3);
			}
		}
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0004E914 File Offset: 0x0004CB14
	private void paintWaypointArrow(mGraphics g)
	{
		int num = 10;
		Task taskMaint = global::Char.myCharz().taskMaint;
		bool flag = taskMaint != null && taskMaint.taskId == 0 && ((taskMaint.index != 1 && taskMaint.index < 6) || taskMaint.index == 0);
		if (!flag)
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				bool flag2 = waypoint.minY == 0 || (int)waypoint.maxY >= TileMap.pxh - 24;
				if (flag2)
				{
					bool flag3 = (int)waypoint.maxY <= TileMap.pxh / 2;
					if (flag3)
					{
						int x = (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2);
						int y = (int)(waypoint.minY + (waypoint.maxY - waypoint.minY) / 2) + this.runArrow;
						bool isTouch = GameCanvas.isTouch;
						if (isTouch)
						{
							y = (int)(waypoint.maxY + (waypoint.maxY - waypoint.minY)) + this.runArrow + num;
						}
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 6, x, y, StaticObj.VCENTER_HCENTER);
					}
					else
					{
						bool flag4 = (int)waypoint.minY >= TileMap.pxh / 2;
						if (flag4)
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.minY - 12) - this.runArrow, StaticObj.VCENTER_HCENTER);
						}
					}
				}
				else
				{
					bool flag5 = waypoint.minX >= 0 && waypoint.minX < 24;
					if (flag5)
					{
						bool flag6 = !GameCanvas.isTouch;
						if (flag6)
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
						}
						else
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
						}
					}
					else
					{
						bool flag7 = (int)waypoint.minX <= TileMap.tmw * 24 && (int)waypoint.minX >= TileMap.tmw * 24 - 48;
						if (flag7)
						{
							bool flag8 = !GameCanvas.isTouch;
							if (flag8)
							{
								g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
							}
							else
							{
								g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
							}
						}
						else
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.maxY - 48) - this.runArrow, StaticObj.VCENTER_HCENTER);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0004EC68 File Offset: 0x0004CE68
	public static Npc findNPCInMap(short id)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			bool flag = npc.template.npcTemplateId == (int)id;
			if (flag)
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0004ECC0 File Offset: 0x0004CEC0
	public static global::Char findCharInMap(int charId)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			bool flag = @char.charID == charId;
			if (flag)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0004ED14 File Offset: 0x0004CF14
	public static Mob findMobInMap(sbyte mobIndex)
	{
		return (Mob)GameScr.vMob.elementAt((int)mobIndex);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0004ED38 File Offset: 0x0004CF38
	public static Mob findMobInMap(int mobId)
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob.mobId == mobId;
			if (flag)
			{
				return mob;
			}
		}
		return null;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0004ED8C File Offset: 0x0004CF8C
	public static Npc getNpcTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			bool flag = npc.template.npcTemplateId == (int)GameScr.getTaskNpcId();
			if (flag)
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0004EDE8 File Offset: 0x0004CFE8
	private void paintArrowPointToNPC(mGraphics g)
	{
		try
		{
			bool flag = ChatPopup.currChatPopup == null;
			if (flag)
			{
				int num = (int)GameScr.getTaskNpcId();
				bool flag2 = num != -1;
				if (flag2)
				{
					Npc npc = null;
					for (int i = 0; i < GameScr.vNpc.size(); i++)
					{
						Npc npc2 = (Npc)GameScr.vNpc.elementAt(i);
						bool flag3 = npc2.template.npcTemplateId == num;
						if (flag3)
						{
							bool flag4 = npc == null;
							if (flag4)
							{
								npc = npc2;
							}
							else
							{
								bool flag5 = Res.abs(npc2.cx - global::Char.myCharz().cx) < Res.abs(npc.cx - global::Char.myCharz().cx);
								if (flag5)
								{
									npc = npc2;
								}
							}
						}
					}
					bool flag6 = npc != null && npc.statusMe != 15;
					if (flag6)
					{
						bool flag7 = npc.cx <= GameScr.cmx || npc.cx >= GameScr.cmx + GameScr.gW || npc.cy <= GameScr.cmy || npc.cy >= GameScr.cmy + GameScr.gH;
						if (flag7)
						{
							bool flag8 = GameCanvas.gameTick % 10 >= 5;
							if (flag8)
							{
								int num2 = npc.cx - global::Char.myCharz().cx;
								int num3 = npc.cy - global::Char.myCharz().cy;
								int x = 0;
								int y = 0;
								int arg = 0;
								bool flag9 = num2 > 0 && num3 >= 0;
								if (flag9)
								{
									bool flag10 = Res.abs(num2) >= Res.abs(num3);
									if (flag10)
									{
										x = GameScr.gW - 10;
										y = GameScr.gH / 2 + 30;
										bool isTouch = GameCanvas.isTouch;
										if (isTouch)
										{
											y = GameScr.gH / 2 + 10;
										}
										arg = 0;
									}
									else
									{
										x = GameScr.gW / 2;
										y = GameScr.gH - 10;
										arg = 5;
									}
								}
								else
								{
									bool flag11 = num2 >= 0 && num3 < 0;
									if (flag11)
									{
										bool flag12 = Res.abs(num2) >= Res.abs(num3);
										if (flag12)
										{
											x = GameScr.gW - 10;
											y = GameScr.gH / 2 + 30;
											bool isTouch2 = GameCanvas.isTouch;
											if (isTouch2)
											{
												y = GameScr.gH / 2 + 10;
											}
											arg = 0;
										}
										else
										{
											x = GameScr.gW / 2;
											y = 10;
											arg = 6;
										}
									}
								}
								bool flag13 = num2 < 0 && num3 >= 0;
								if (flag13)
								{
									bool flag14 = Res.abs(num2) >= Res.abs(num3);
									if (flag14)
									{
										x = 10;
										y = GameScr.gH / 2 + 30;
										bool isTouch3 = GameCanvas.isTouch;
										if (isTouch3)
										{
											y = GameScr.gH / 2 + 10;
										}
										arg = 3;
									}
									else
									{
										x = GameScr.gW / 2;
										y = GameScr.gH - 10;
										arg = 5;
									}
								}
								else
								{
									bool flag15 = num2 <= 0 && num3 < 0;
									if (flag15)
									{
										bool flag16 = Res.abs(num2) >= Res.abs(num3);
										if (flag16)
										{
											x = 10;
											y = GameScr.gH / 2 + 30;
											bool isTouch4 = GameCanvas.isTouch;
											if (isTouch4)
											{
												y = GameScr.gH / 2 + 10;
											}
											arg = 3;
										}
										else
										{
											x = GameScr.gW / 2;
											y = 10;
											arg = 6;
										}
									}
								}
								GameScr.resetTranslate(g);
								g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham arrow to npc: " + ex.ToString());
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
	public static void resetTranslate(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0004F1EC File Offset: 0x0004D3EC
	private void paintTouchControl(mGraphics g)
	{
		bool flag = this.isNotPaintTouchControl();
		if (!flag)
		{
			GameScr.resetTranslate(g);
			bool flag2 = !TileMap.isOfflineMap() && !this.isVS();
			if (flag2)
			{
				bool flag3 = mScreen.keyTouch == 15 || mScreen.keyMouse == 15;
				if (flag3)
				{
					g.drawImage((!Main.isPC) ? GameScr.imgChat2 : GameScr.imgChatsPC2, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else
				{
					g.drawImage((!Main.isPC) ? GameScr.imgChat : GameScr.imgChatPC, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
			bool flag4 = !GameScr.isUseTouch;
			if (flag4)
			{
			}
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0004F2D4 File Offset: 0x0004D4D4
	public void paintImageBarRight(mGraphics g, global::Char c)
	{
		int num = (int)((long)c.cHP * GameScr.hpBarW / (long)c.cHPFull);
		int num2 = c.cMP * GameScr.mpBarW;
		int num3 = (int)((long)this.dHP * GameScr.hpBarW / (long)c.cHPFull);
		int num4 = this.dMP * GameScr.mpBarW;
		g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(GameScr.imgPanel), 0, 95, 100);
		g.drawRegion(GameScr.imgPanel, 0, 0, mGraphics.getImageWidth(GameScr.imgPanel), mGraphics.getImageHeight(GameScr.imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num3), 5, num3, 10);
		g.drawImage(GameScr.imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num), 5, num, 10);
		g.drawImage(GameScr.imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num4), 20, num4, 6);
		g.drawImage(GameScr.imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num2), 20, num2, 6);
		g.drawImage(GameScr.imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0004F50C File Offset: 0x0004D70C
	private void paintImageBar(mGraphics g, bool isLeft, global::Char c)
	{
		bool flag = c == null;
		if (!flag)
		{
			bool flag2 = c.charID == global::Char.myCharz().charID;
			int num;
			int num2;
			int num3;
			int num4;
			if (flag2)
			{
				num = (int)((long)this.dHP * GameScr.hpBarW / (long)c.cHPFull);
				num2 = this.dMP * GameScr.mpBarW / c.cMPFull;
				num3 = (int)((long)c.cHP * GameScr.hpBarW / (long)c.cHPFull);
				num4 = c.cMP * GameScr.mpBarW / c.cMPFull;
			}
			else
			{
				num = (int)((long)c.dHP * GameScr.hpBarW / (long)c.cHPFull);
				num2 = c.perCentMp * GameScr.mpBarW / 100;
				num3 = (int)((long)c.cHP * GameScr.hpBarW / (long)c.cHPFull);
				num4 = c.perCentMp * GameScr.mpBarW / 100;
			}
			bool flag3 = global::Char.myCharz().secondPower > 0;
			if (flag3)
			{
				int w = (int)global::Char.myCharz().powerPoint * GameScr.spBarW / (int)global::Char.myCharz().maxPowerPoint;
				g.drawImage(GameScr.imgPanel2, 58, 29, 0);
				g.setClip(83, 31, w, 10);
				g.drawImage(GameScr.imgSP, 83, 31, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
				{
					global::Char.myCharz().strInfo,
					":",
					global::Char.myCharz().powerPoint,
					"/",
					global::Char.myCharz().maxPowerPoint
				}), 115, 29, 2);
			}
			bool flag4 = c.charID != global::Char.myCharz().charID;
			if (flag4)
			{
				g.setClip(mGraphics.getImageWidth(GameScr.imgPanel) - 95, 0, 95, 100);
			}
			g.drawImage(GameScr.imgPanel, 0, 0, 0);
			if (isLeft)
			{
				g.setClip(83, 5, num, 10);
			}
			else
			{
				g.setClip((int)(83L + GameScr.hpBarW - (long)num), 5, num, 10);
			}
			g.drawImage(GameScr.imgHPLost, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 5, num3, 10);
			}
			else
			{
				g.setClip((int)(83L + GameScr.hpBarW - (long)num3), 5, num3, 10);
			}
			g.drawImage(GameScr.imgHP, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num2, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num2, 20, num2, 6);
			}
			g.drawImage(GameScr.imgMPLost, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num2, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num4, 20, num4, 6);
			}
			g.drawImage(GameScr.imgMP, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			bool flag5 = global::Char.myCharz().cMP == 0 && GameCanvas.gameTick % 10 > 5;
			if (flag5)
			{
				g.setClip(83, 20, 2, 6);
				g.drawImage(GameScr.imgMPLost, 83, 20, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00003136 File Offset: 0x00001336
	public void getInjure()
	{
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0004F8AC File Offset: 0x0004DAAC
	public void starVS()
	{
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.secondVS = 180;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0004F8DC File Offset: 0x0004DADC
	private global::Char findCharVS1()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			bool flag = @char.cTypePk != 0;
			if (flag)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0004F930 File Offset: 0x0004DB30
	private global::Char findCharVS2()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			bool flag = @char.cTypePk != 0 && @char != this.findCharVS1();
			if (flag)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0004F994 File Offset: 0x0004DB94
	private void paintInfoBar(mGraphics g)
	{
		GameScr.resetTranslate(g);
		bool flag = TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null;
		if (flag)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, this.findCharVS1());
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, this.findCharVS2());
			this.findCharVS1().paintHeadWithXY(g, 137, 25, 0);
			this.findCharVS2().paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
		}
		else
		{
			bool flag2 = this.isVS() && global::Char.myCharz().charFocus != null;
			if (flag2)
			{
				g.translate(GameCanvas.w / 2 - 62, 0);
				this.paintImageBar(g, true, global::Char.myCharz().charFocus);
				g.translate(-(GameCanvas.w / 2 - 65), 0);
				this.paintImageBarRight(g, global::Char.myCharz());
				global::Char.myCharz().paintHeadWithXY(g, 137, 25, 0);
				global::Char.myCharz().charFocus.paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
			}
			else
			{
				bool flag3 = GameScr.ispaintPhubangBar() && GameScr.isSmallScr();
				if (flag3)
				{
					GameScr.paintHPBar_NEW(g, 1, 1, global::Char.myCharz());
				}
				else
				{
					this.paintImageBar(g, true, global::Char.myCharz());
					bool flag4 = global::Char.myCharz().isInEnterOfflinePoint() != null || global::Char.myCharz().isInEnterOnlinePoint() != null;
					if (flag4)
					{
						mFont.tahoma_7_green2.drawString(g, mResources.enter, this.imgScrW / 2, 8 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					}
					else
					{
						bool flag5 = global::Char.myCharz().mobFocus != null;
						if (flag5)
						{
							bool flag6 = global::Char.myCharz().mobFocus.getTemplate() != null;
							if (flag6)
							{
								mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().mobFocus.getTemplate().name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
							}
							bool flag7 = global::Char.myCharz().mobFocus.templateId != 0;
							if (flag7)
							{
								mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().mobFocus.hp) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
							}
						}
						else
						{
							bool flag8 = global::Char.myCharz().npcFocus != null;
							if (flag8)
							{
								mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().npcFocus.template.name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
								bool flag9 = global::Char.myCharz().npcFocus.template.npcTemplateId == 4;
								if (flag9)
								{
									mFont.tahoma_7b_green2.drawString(g, GameScr.gI().magicTree.currPeas.ToString() + "/" + GameScr.gI().magicTree.maxPeas.ToString(), this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
								}
							}
							else
							{
								bool flag10 = global::Char.myCharz().charFocus != null;
								if (flag10)
								{
									mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().charFocus.cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
									mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().charFocus.cHP) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
								}
								else
								{
									mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
									mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(global::Char.myCharz().cPower) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
								}
							}
						}
					}
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool flag11 = this.isVS() && this.secondVS > 0;
		if (flag11)
		{
			this.curr = mSystem.currentTimeMillis();
			bool flag12 = this.curr - this.last >= 1000L;
			if (flag12)
			{
				this.last = mSystem.currentTimeMillis();
				this.secondVS--;
			}
			mFont.tahoma_7b_white.drawString(g, this.secondVS.ToString() + string.Empty, GameCanvas.w / 2, 13, 2, mFont.tahoma_7b_dark);
		}
		bool flag13 = this.flareFindFocus;
		if (flag13)
		{
			g.drawImage(ItemMap.imageFlare, 40, 35, mGraphics.BOTTOM | mGraphics.HCENTER);
			this.flareTime--;
			bool flag14 = this.flareTime < 0;
			if (flag14)
			{
				this.flareTime = 0;
				this.flareFindFocus = false;
			}
		}
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0004FED8 File Offset: 0x0004E0D8
	public bool isVS()
	{
		return TileMap.isVoDaiMap() && (global::Char.myCharz().cTypePk != 0 || (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null));
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0004FF24 File Offset: 0x0004E124
	private void paintSelectedSkill(mGraphics g)
	{
		bool flag = this.mobCapcha != null;
		if (flag)
		{
			this.paintCapcha(g);
		}
		else
		{
			bool flag2 = GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || this.isPaintPopup() || GameCanvas.panel.isShow || global::Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance;
			if (!flag2)
			{
				long num = mSystem.currentTimeMillis();
				long num2 = num - this.lastUsePotion;
				int num3 = 0;
				bool flag3 = num2 < 10000L;
				if (flag3)
				{
					num3 = (int)(num2 * 20L / 10000L);
				}
				bool flag4 = !GameCanvas.isTouch;
				if (flag4)
				{
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
					SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
					mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 15, 1);
					bool flag5 = num2 < 10000L;
					if (flag5)
					{
						g.setColor(2721889);
						num3 = (int)(num2 * 20L / 10000L);
						g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num3, 20, 20 - num3);
					}
				}
				else
				{
					bool flag6 = global::Char.myCharz().statusMe != 14;
					if (flag6)
					{
						bool isSmallGamePad = GameScr.gamePad.isSmallGamePad;
						if (isSmallGamePad)
						{
							bool flag7 = GameScr.isAnalog != 1;
							if (flag7)
							{
								g.setColor(9670800);
								g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10, 22, 20);
								g.setColor(16777215);
								g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num3 == 0) ? 0 : (20 - num3)), 22, (num3 == 0) ? 20 : num3);
								g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP, 0);
								mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15, 2);
							}
							else
							{
								bool flag8 = GameScr.isAnalog == 1;
								if (flag8)
								{
									g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
									SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
									mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 13, 1);
									bool flag9 = num2 < 10000L;
									if (flag9)
									{
										g.setColor(2721889);
										num3 = (int)(num2 * 20L / 10000L);
										g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num3, 20, 20 - num3);
									}
								}
							}
						}
						else
						{
							bool flag10 = GameScr.isAnalog != 1;
							if (flag10)
							{
								g.setColor(9670800);
								g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 - 6, 22, 20);
								g.setColor(16777215);
								g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num3 == 0) ? 0 : (20 - num3)) - 6, 22, (num3 == 0) ? 20 : num3);
								g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP - 6, 0);
								mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15 - 6, 2);
							}
							else
							{
								g.setColor(9670800);
								g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 - 6, 20, 18);
								g.setColor(16777215);
								g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 + ((num3 == 0) ? 0 : (20 - num3)) - 6, 20, (num3 == 0) ? 18 : num3);
								g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP3 : GameScr.imgHP4, GameScr.xHP + 20, GameScr.yHP + 20 - 6, mGraphics.HCENTER | mGraphics.VCENTER);
								mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15 - 6, 2);
							}
						}
					}
				}
				bool flag11 = GameScr.isHaveSelectSkill;
				if (flag11)
				{
					Skill[] array = Main.isPC ? GameScr.keySkill : ((!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill);
					bool flag12 = mScreen.keyTouch == 10;
					if (flag12)
					{
					}
					bool flag13 = !GameCanvas.isTouch;
					if (flag13)
					{
						g.setColor(11152401);
						g.fillRect(GameScr.xSkill + GameScr.xHP + 2, GameScr.yHP - 10 + 6, 20, 10);
						mFont.tahoma_7_white.drawString(g, "*", GameScr.xSkill + GameScr.xHP + 12, GameScr.yHP - 8 + 6, mFont.CENTER);
					}
					int num4 = (!Main.isPC) ? ((!GameCanvas.isTouch) ? array.Length : this.nSkill) : array.Length;
					for (int i = 0; i < num4; i++)
					{
						bool isPC = Main.isPC;
						if (isPC)
						{
							bool isQwerty = TField.isQwerty;
							string[] array3;
							if (isQwerty)
							{
								string[] array2 = new string[10];
								array2[0] = "1";
								array2[1] = "2";
								array2[2] = "3";
								array2[3] = "4";
								array2[4] = "5";
								array2[5] = "6";
								array2[6] = "7";
								array2[7] = "8";
								array2[8] = "9";
								array3 = array2;
								array2[9] = "0";
							}
							else
							{
								string[] array4 = new string[5];
								array4[0] = "7";
								array4[1] = "8";
								array4[2] = "9";
								array4[3] = "10";
								array3 = array4;
								array4[4] = "11";
							}
							string[] array5 = array3;
							int num5 = -13;
							bool flag14 = num4 > 5 && i < 5;
							if (flag14)
							{
								num5 = 27;
							}
							mFont.tahoma_7b_dark.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num5, mFont.CENTER);
							mFont.tahoma_7b_white.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num5 + 1, mFont.CENTER);
						}
						else
						{
							bool flag15 = !GameCanvas.isTouch;
							if (flag15)
							{
								bool isQwerty2 = TField.isQwerty;
								string[] array7;
								if (isQwerty2)
								{
									string[] array6 = new string[5];
									array6[0] = "Q";
									array6[1] = "W";
									array6[2] = "E";
									array6[3] = "R";
									array7 = array6;
									array6[4] = "T";
								}
								else
								{
									string[] array8 = new string[5];
									array8[0] = "7";
									array8[1] = "8";
									array8[2] = "9";
									array8[3] = "1";
									array7 = array8;
									array8[4] = "3";
								}
								string[] array9 = array7;
								g.setColor(11152401);
								g.fillRect(GameScr.xSkill + GameScr.xS[i] + 2, GameScr.yS[i] - 10 + 8, 20, 10);
								mFont.tahoma_7_white.drawString(g, array9[i], GameScr.xSkill + GameScr.xS[i] + 12, GameScr.yS[i] - 10 + 6, mFont.CENTER);
							}
						}
						Skill skill = array[i];
						bool flag16 = skill != global::Char.myCharz().myskill;
						if (flag16)
						{
							g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
						}
						bool flag17 = skill != null;
						if (flag17)
						{
							bool flag18 = skill == global::Char.myCharz().myskill;
							if (flag18)
							{
								g.drawImage(GameScr.imgSkill2, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
								bool flag19 = GameCanvas.isTouch && !Main.isPC;
								if (flag19)
								{
									g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, GameScr.xSkill + GameScr.xS[i] + 8, GameScr.yS[i] - 7, 0);
								}
							}
							skill.paint(GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 13, g);
							bool flag20 = (i == this.selectedIndexSkill && !this.isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == this.keyTouchSkill;
							if (flag20)
							{
								g.drawImage(ItemMap.imageFlare, GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 14, 3);
							}
						}
					}
				}
				this.paintGamePad(g);
			}
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00050918 File Offset: 0x0004EB18
	public void paintOpen(mGraphics g)
	{
		bool flag = this.isstarOpen;
		if (flag)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.fillRect(0, 0, GameCanvas.w, this.moveUp);
			g.setColor(10275899);
			g.fillRect(0, this.moveUp - 1, GameCanvas.w, 1);
			g.fillRect(0, this.moveDow + 1, GameCanvas.w, 1);
		}
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00050994 File Offset: 0x0004EB94
	public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
	{
		int num = -1;
		for (int i = 0; i < 5; i++)
		{
			bool flag = GameScr.flyTextState[i] == -1;
			if (flag)
			{
				num = i;
				break;
			}
		}
		bool flag2 = num == -1;
		if (!flag2)
		{
			GameScr.flyTextColor[num] = color;
			GameScr.flyTextString[num] = flyString;
			GameScr.flyTextX[num] = x;
			GameScr.flyTextY[num] = y;
			GameScr.flyTextDx[num] = dx;
			GameScr.flyTextDy[num] = ((dy >= 0) ? 5 : -5);
			GameScr.flyTextState[num] = 0;
			GameScr.flyTime[num] = 0;
			GameScr.flyTextYTo[num] = 10;
			for (int j = 0; j < 5; j++)
			{
				bool flag3 = GameScr.flyTextState[j] != -1 && num != j && GameScr.flyTextDy[num] < 0 && Res.abs(GameScr.flyTextX[num] - GameScr.flyTextX[j]) <= 20 && GameScr.flyTextYTo[num] == GameScr.flyTextYTo[j];
				if (flag3)
				{
					GameScr.flyTextYTo[num] += 10;
				}
			}
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00050AA4 File Offset: 0x0004ECA4
	public static void updateFlyText()
	{
		for (int i = 0; i < 5; i++)
		{
			bool flag = GameScr.flyTextState[i] != -1;
			if (flag)
			{
				bool flag2 = GameScr.flyTextState[i] > GameScr.flyTextYTo[i];
				if (flag2)
				{
					GameScr.flyTime[i]++;
					bool flag3 = GameScr.flyTime[i] == 25;
					if (flag3)
					{
						GameScr.flyTime[i] = 0;
						GameScr.flyTextState[i] = -1;
						GameScr.flyTextYTo[i] = 0;
						GameScr.flyTextDx[i] = 0;
						GameScr.flyTextX[i] = 0;
					}
				}
				else
				{
					GameScr.flyTextState[i] += Res.abs(GameScr.flyTextDy[i]);
					GameScr.flyTextX[i] += GameScr.flyTextDx[i];
					GameScr.flyTextY[i] += GameScr.flyTextDy[i];
				}
			}
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00050B8C File Offset: 0x0004ED8C
	public static void loadSplash()
	{
		bool flag = GameScr.imgSplash == null;
		if (flag)
		{
			GameScr.imgSplash = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameScr.imgSplash[i] = GameCanvas.loadImage("/e/sp" + i.ToString() + ".png");
			}
		}
		GameScr.splashX = new int[2];
		GameScr.splashY = new int[2];
		GameScr.splashState = new int[2];
		GameScr.splashF = new int[2];
		GameScr.splashDir = new int[2];
		GameScr.splashState[0] = (GameScr.splashState[1] = -1);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00050C30 File Offset: 0x0004EE30
	public static bool startSplash(int x, int y, int dir)
	{
		int num = (GameScr.splashState[0] != -1) ? 1 : 0;
		bool flag = GameScr.splashState[num] != -1;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			GameScr.splashState[num] = 0;
			GameScr.splashDir[num] = dir;
			GameScr.splashX[num] = x;
			GameScr.splashY[num] = y;
			result = true;
		}
		return result;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00050C88 File Offset: 0x0004EE88
	public static void updateSplash()
	{
		for (int i = 0; i < 2; i++)
		{
			bool flag = GameScr.splashState[i] != -1;
			if (flag)
			{
				GameScr.splashState[i]++;
				GameScr.splashX[i] += GameScr.splashDir[i] << 2;
				GameScr.splashY[i]--;
				bool flag2 = GameScr.splashState[i] >= 6;
				if (flag2)
				{
					GameScr.splashState[i] = -1;
				}
				else
				{
					GameScr.splashF[i] = (GameScr.splashState[i] >> 1) % 3;
				}
			}
		}
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00050D2C File Offset: 0x0004EF2C
	public static void paintSplash(mGraphics g)
	{
		for (int i = 0; i < 2; i++)
		{
			bool flag = GameScr.splashState[i] != -1;
			if (flag)
			{
				bool flag2 = GameScr.splashDir[i] == 1;
				if (flag2)
				{
					g.drawImage(GameScr.imgSplash[GameScr.splashF[i]], GameScr.splashX[i], GameScr.splashY[i], 3);
				}
				else
				{
					g.drawRegion(GameScr.imgSplash[GameScr.splashF[i]], 0, 0, mGraphics.getImageWidth(GameScr.imgSplash[GameScr.splashF[i]]), mGraphics.getImageHeight(GameScr.imgSplash[GameScr.splashF[i]]), 2, GameScr.splashX[i], GameScr.splashY[i], 3);
				}
			}
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00050DE9 File Offset: 0x0004EFE9
	private void loadInforBar()
	{
		this.imgScrW = 84;
		GameScr.hpBarW = 66L;
		GameScr.mpBarW = 59;
		GameScr.hpBarX = 52;
		GameScr.hpBarY = 10;
		GameScr.spBarW = 61;
		GameScr.expBarW = GameScr.gW - 61;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00050E28 File Offset: 0x0004F028
	public void updateSS()
	{
		bool flag = GameScr.indexMenu == -1;
		if (!flag)
		{
			bool flag2 = GameScr.cmySK != GameScr.cmtoYSK;
			if (flag2)
			{
				GameScr.cmvySK = GameScr.cmtoYSK - GameScr.cmySK << 2;
				GameScr.cmdySK += GameScr.cmvySK;
				GameScr.cmySK += GameScr.cmdySK >> 4;
				GameScr.cmdySK &= 15;
			}
			bool flag3 = global::Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK < 0;
			if (flag3)
			{
				GameScr.cmtoYSK = 0;
			}
			bool flag4 = global::Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK > GameScr.cmyLimSK;
			if (flag4)
			{
				GameScr.cmtoYSK = GameScr.cmyLimSK;
			}
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00050EFC File Offset: 0x0004F0FC
	public void updateKeyAlert()
	{
		bool flag2 = !GameScr.isPaintAlert || GameCanvas.currentDialog != null;
		if (!flag2)
		{
			bool flag = false;
			bool flag3 = GameCanvas.keyPressed[Key.NUM8];
			if (flag3)
			{
				GameScr.indexRow++;
				bool flag4 = GameScr.indexRow >= this.texts.size();
				if (flag4)
				{
					GameScr.indexRow = 0;
				}
				flag = true;
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[Key.NUM2];
				if (flag5)
				{
					GameScr.indexRow--;
					bool flag6 = GameScr.indexRow < 0;
					if (flag6)
					{
						GameScr.indexRow = this.texts.size() - 1;
					}
					flag = true;
				}
			}
			bool flag7 = flag;
			if (flag7)
			{
				GameScr.scrMain.moveTo(GameScr.indexRow * GameScr.scrMain.ITEM_SIZE);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				ScrollResult scrollResult = GameScr.scrMain.updateKey();
				bool flag8 = scrollResult.isDowning || scrollResult.isFinish;
				if (flag8)
				{
					GameScr.indexRow = scrollResult.selected;
					flag = true;
				}
			}
			bool flag9 = flag && GameScr.indexRow >= 0 && GameScr.indexRow < this.texts.size();
			if (flag9)
			{
				string text = (string)this.texts.elementAt(GameScr.indexRow);
				this.fnick = null;
				this.alertURL = null;
				this.center = null;
				ChatTextField.gI().center = null;
				int num;
				bool flag10 = (num = text.IndexOf("http://")) >= 0;
				if (flag10)
				{
					Cout.println("currentLine: " + text);
					this.alertURL = text.Substring(num);
					this.center = new Command(mResources.open_link, 12000);
					bool flag11 = !GameCanvas.isTouch;
					if (flag11)
					{
						ChatTextField.gI().center = new Command(mResources.open_link, null, 12000, null);
					}
				}
				else
				{
					bool flag12 = text.IndexOf("@") >= 0;
					if (flag12)
					{
						string text2 = text.Substring(2);
						text2 = text2.Trim();
						num = text2.IndexOf("@");
						string text3 = text2.Substring(num);
						int num2 = text3.IndexOf(" ");
						bool flag13 = num2 <= 0;
						if (flag13)
						{
							num2 = num + text3.Length;
						}
						else
						{
							num2 += num;
						}
						this.fnick = text2.Substring(num + 1, num2);
						bool flag14 = !this.fnick.Equals(string.Empty) && !this.fnick.Equals(global::Char.myCharz().cName);
						if (flag14)
						{
							this.center = new Command(mResources.SELECT, 12009, this.fnick);
							bool flag15 = !GameCanvas.isTouch;
							if (flag15)
							{
								ChatTextField.gI().center = new Command(mResources.SELECT, null, 12009, this.fnick);
							}
						}
						else
						{
							this.fnick = null;
							this.center = null;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00051228 File Offset: 0x0004F428
	public bool isPaintPopup()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade || GameScr.isPaintAlert || GameScr.isPaintZone || GameScr.isPaintTeam || GameScr.isPaintClan || GameScr.isPaintFindTeam || GameScr.isPaintTask || GameScr.isPaintFriend || GameScr.isPaintEnemies || GameScr.isPaintCharInMap || GameScr.isPaintMessage;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00051380 File Offset: 0x0004F580
	public bool isNotPaintTouchControl()
	{
		return (!GameCanvas.isTouchControl && GameCanvas.currentScreen == GameScr.gI()) || !GameCanvas.isTouch || ChatTextField.gI().isShow || InfoDlg.isShow || GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || this.isPaintPopup();
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000513F4 File Offset: 0x0004F5F4
	public bool isPaintUI()
	{
		return GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade;
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000514D4 File Offset: 0x0004F6D4
	public bool isOpenUI()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintWeapon || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintSplit || GameScr.isPaintTrade;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x000515C8 File Offset: 0x0004F7C8
	public static void setPopupSize(int w, int h)
	{
		bool flag = GameCanvas.w == 128 || GameCanvas.h <= 208;
		if (flag)
		{
			w = 126;
			h = 160;
		}
		GameScr.indexTitle = 0;
		GameScr.popupW = w;
		GameScr.popupH = h;
		GameScr.popupX = GameScr.gW2 - w / 2;
		GameScr.popupY = GameScr.gH2 - h / 2;
		bool flag2 = GameCanvas.isTouch && !GameScr.isPaintZone && !GameScr.isPaintTeam && !GameScr.isPaintClan && !GameScr.isPaintCharInMap && !GameScr.isPaintFindTeam && !GameScr.isPaintFriend && !GameScr.isPaintEnemies && !GameScr.isPaintTask && !GameScr.isPaintMessage;
		if (flag2)
		{
			bool flag3 = GameCanvas.h <= 240;
			if (flag3)
			{
				GameScr.popupY -= 10;
			}
			bool flag4 = GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr;
			if (flag4)
			{
				GameScr.popupW = 310;
				GameScr.popupX = GameScr.gW / 2 - GameScr.popupW / 2;
				bool flag5 = GameScr.isPaintInfoMe && GameScr.indexMenu > 0;
				if (flag5)
				{
					GameScr.popupW = w;
					GameScr.popupX = GameScr.gW2 - w / 2;
				}
			}
		}
		bool flag6 = GameScr.popupY < -10;
		if (flag6)
		{
			GameScr.popupY = -10;
		}
		bool flag7 = GameCanvas.h > 208 && GameScr.popupY < 0;
		if (flag7)
		{
			GameScr.popupY = 0;
		}
		bool flag8 = GameCanvas.h == 208 && GameScr.popupY < 10;
		if (flag8)
		{
			GameScr.popupY = 10;
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0005177C File Offset: 0x0004F97C
	public static void loadImg()
	{
		TileMap.loadTileImage();
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00051788 File Offset: 0x0004F988
	public void paintTitle(mGraphics g, string title, bool arrow)
	{
		int num = GameScr.gW / 2;
		g.setColor(Paint.COLORDARK);
		g.fillRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		bool flag = (GameScr.indexTitle == 0 || GameCanvas.isTouch) && arrow;
		if (flag)
		{
			SmallImage.drawSmallImage(g, 989, num - mFont.tahoma_8b.getWidth(title) / 2 - 15 - 7 - ((GameCanvas.gameTick % 8 > 3) ? 0 : 2), GameScr.popupY + 16, 2, StaticObj.VCENTER_HCENTER);
			SmallImage.drawSmallImage(g, 989, num + mFont.tahoma_8b.getWidth(title) / 2 + 15 + 5 + ((GameCanvas.gameTick % 8 > 3) ? 0 : 2), GameScr.popupY + 16, 0, StaticObj.VCENTER_HCENTER);
		}
		bool flag2 = GameScr.indexTitle == 0;
		if (flag2)
		{
			g.setColor(Paint.COLORFOCUS);
		}
		else
		{
			g.setColor(Paint.COLORBORDER);
		}
		g.drawRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		mFont.tahoma_8b.drawString(g, title, num, GameScr.popupY + 9, 2);
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000518E0 File Offset: 0x0004FAE0
	public static int getTaskMapId()
	{
		bool flag = global::Char.myCharz().taskMaint == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			result = GameScr.mapTasks[global::Char.myCharz().taskMaint.index];
		}
		return result;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00051924 File Offset: 0x0004FB24
	public static sbyte getTaskNpcId()
	{
		sbyte result = 0;
		bool flag = global::Char.myCharz().taskMaint == null;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = global::Char.myCharz().taskMaint.index <= GameScr.tasks.Length - 1;
			if (flag2)
			{
				result = (sbyte)GameScr.tasks[global::Char.myCharz().taskMaint.index];
			}
		}
		return result;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00003136 File Offset: 0x00001336
	public void refreshTeam()
	{
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0005198C File Offset: 0x0004FB8C
	public void onChatFromMe(string text, string to)
	{
		Res.outz("CHAT");
		bool flag = !GameScr.isPaintMessage || GameCanvas.isTouch;
		if (flag)
		{
			ChatTextField.gI().isShow = false;
		}
		bool flag2 = to.Equals(mResources.chat_player);
		if (flag2)
		{
			bool flag3 = GameScr.info2.playerID == global::Char.myCharz().charID;
			if (!flag3)
			{
				Service.gI().chatPlayer(text, GameScr.info2.playerID);
			}
		}
		else
		{
			bool flag4 = text.Equals(string.Empty);
			if (!flag4)
			{
				Service.gI().chat(text);
			}
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00051A2C File Offset: 0x0004FC2C
	public void onCancelChat()
	{
		bool flag = GameScr.isPaintMessage;
		if (flag)
		{
			GameScr.isPaintMessage = false;
			ChatTextField.gI().center = null;
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00051A58 File Offset: 0x0004FC58
	public void openWeb(string strLeft, string strRight, string url, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		this.left = new Command(strLeft, 11068, url);
		this.right = new Command(strRight, 11069);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00051AD8 File Offset: 0x0004FCD8
	public void sendSms(string strLeft, string strRight, short port, string syntax, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		MyVector myVector = new MyVector();
		myVector.addElement(string.Empty + port.ToString());
		myVector.addElement(syntax);
		this.left = new Command(strLeft, 11074);
		this.right = new Command(strRight, 11075);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00051B7B File Offset: 0x0004FD7B
	public void actMenu()
	{
		GameCanvas.panel.setTypeMain();
		GameCanvas.panel.show();
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00051B94 File Offset: 0x0004FD94
	public void openUIZone(Message message)
	{
		InfoDlg.hide();
		try
		{
			this.zones = new int[(int)message.reader().readByte()];
			this.pts = new int[this.zones.Length];
			this.numPlayer = new int[this.zones.Length];
			this.maxPlayer = new int[this.zones.Length];
			this.rank1 = new int[this.zones.Length];
			this.rankName1 = new string[this.zones.Length];
			this.rank2 = new int[this.zones.Length];
			this.rankName2 = new string[this.zones.Length];
			for (int i = 0; i < this.zones.Length; i++)
			{
				this.zones[i] = (int)message.reader().readByte();
				this.pts[i] = (int)message.reader().readByte();
				this.numPlayer[i] = (int)message.reader().readByte();
				this.maxPlayer[i] = (int)message.reader().readByte();
				sbyte b = message.reader().readByte();
				bool flag = b == 1;
				if (flag)
				{
					this.rankName1[i] = message.reader().readUTF();
					this.rank1[i] = message.reader().readInt();
					this.rankName2[i] = message.reader().readUTF();
					this.rank2[i] = message.reader().readInt();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham OPEN UIZONE " + ex.ToString());
		}
		GameCanvas.panel.setTypeZone();
		GameCanvas.panel.show();
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00051D64 File Offset: 0x0004FF64
	public void showViewInfo()
	{
		GameScr.indexMenu = 3;
		GameScr.isPaintInfoMe = true;
		GameScr.setPopupSize(175, 200);
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00051D84 File Offset: 0x0004FF84
	private void actDead()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.DIES[1], 110381));
		myVector.addElement(new Command(mResources.DIES[2], 110382));
		myVector.addElement(new Command(mResources.DIES[3], 110383));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00051DED File Offset: 0x0004FFED
	public void startYesNoPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.popUpYesNo = new PopUpYesNo();
		this.popUpYesNo.setPopUp(info, cmdYes, cmdNo);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00051E0C File Offset: 0x0005000C
	public void player_vs_player(int playerId, int xu, string info, sbyte typePK)
	{
		global::Char @char = GameScr.findCharInMap(playerId);
		bool flag = @char != null;
		if (flag)
		{
			bool flag2 = typePK == 3;
			if (flag2)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2000, @char), new Command(mResources.CLOSE, 2009, @char));
			}
			bool flag3 = typePK == 4;
			if (flag3)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2005, @char), new Command(mResources.CLOSE, 2009, @char));
			}
		}
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00051E94 File Offset: 0x00050094
	public void giaodich(int playerID)
	{
		global::Char @char = GameScr.findCharInMap(playerID);
		bool flag = @char != null;
		if (flag)
		{
			this.startYesNoPopUp(@char.cName + mResources.want_to_trade, new Command(mResources.YES, 11114, @char), new Command(mResources.NO, 2009, @char));
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00051EEC File Offset: 0x000500EC
	public void getFlagImage(int charID, sbyte cflag)
	{
		bool flag = GameScr.vFlag.size() == 0;
		if (flag)
		{
			Service.gI().getFlag(2, cflag);
			Res.outz("getFlag1");
		}
		else
		{
			bool flag2 = charID == global::Char.myCharz().charID;
			if (flag2)
			{
				Res.outz("my cflag: isme");
				bool flag3 = global::Char.myCharz().isGetFlagImage(cflag);
				if (flag3)
				{
					Res.outz("my cflag: true");
					for (int i = 0; i < GameScr.vFlag.size(); i++)
					{
						PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
						bool flag4 = pkflag != null && pkflag.cflag == cflag;
						if (flag4)
						{
							Res.outz("my cflag: cflag==");
							global::Char.myCharz().flagImage = pkflag.IDimageFlag;
						}
					}
				}
				else
				{
					bool flag5 = !global::Char.myCharz().isGetFlagImage(cflag);
					if (flag5)
					{
						Res.outz("my cflag: false");
						Service.gI().getFlag(2, cflag);
					}
				}
			}
			else
			{
				Res.outz("my cflag: not me");
				bool flag6 = GameScr.findCharInMap(charID) != null;
				if (flag6)
				{
					bool flag7 = GameScr.findCharInMap(charID).isGetFlagImage(cflag);
					if (flag7)
					{
						Res.outz("my cflag: true");
						for (int j = 0; j < GameScr.vFlag.size(); j++)
						{
							PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(j);
							bool flag8 = pkflag2 != null && pkflag2.cflag == cflag;
							if (flag8)
							{
								Res.outz("my cflag: cflag==");
								GameScr.findCharInMap(charID).flagImage = pkflag2.IDimageFlag;
							}
						}
					}
					else
					{
						bool flag9 = !GameScr.findCharInMap(charID).isGetFlagImage(cflag);
						if (flag9)
						{
							Res.outz("my cflag: false");
							Service.gI().getFlag(2, cflag);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000387 RID: 903 RVA: 0x000520DC File Offset: 0x000502DC
	public void actionPerform(int idAction, object p)
	{
		Cout.println("PERFORM WITH ID = " + idAction.ToString());
		switch (idAction)
		{
		case 2000:
		{
			this.popUpYesNo = null;
			GameCanvas.endDlg();
			bool flag = (global::Char)p == null;
			if (flag)
			{
				Service.gI().player_vs_player(1, 3, -1);
				return;
			}
			Service.gI().player_vs_player(1, 3, ((global::Char)p).charID);
			Service.gI().charMove();
			return;
		}
		case 2001:
			GameCanvas.endDlg();
			return;
		case 2003:
			GameCanvas.endDlg();
			InfoDlg.showWait();
			Service.gI().player_vs_player(0, 3, global::Char.myCharz().charFocus.charID);
			return;
		case 2004:
			GameCanvas.endDlg();
			Service.gI().player_vs_player(0, 4, global::Char.myCharz().charFocus.charID);
			return;
		case 2005:
		{
			GameCanvas.endDlg();
			this.popUpYesNo = null;
			bool flag2 = (global::Char)p == null;
			if (flag2)
			{
				Service.gI().player_vs_player(1, 4, -1);
				return;
			}
			Service.gI().player_vs_player(1, 4, ((global::Char)p).charID);
			return;
		}
		case 2006:
			GameCanvas.endDlg();
			Service.gI().player_vs_player(2, 4, global::Char.myCharz().charFocus.charID);
			return;
		case 2007:
			GameCanvas.endDlg();
			GameMidlet.instance.exit();
			return;
		case 2009:
			this.popUpYesNo = null;
			return;
		}
		switch (idAction)
		{
		case 11111:
		{
			bool flag3 = global::Char.myCharz().charFocus == null;
			if (flag3)
			{
				return;
			}
			InfoDlg.showWait();
			bool flag4 = GameCanvas.panel.vPlayerMenu.size() <= 0;
			if (flag4)
			{
				this.playerMenu(global::Char.myCharz().charFocus);
			}
			GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
			GameCanvas.panel.show();
			Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
			Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
			return;
		}
		case 11112:
		{
			global::Char @char = (global::Char)p;
			Service.gI().friend(1, @char.charID);
			return;
		}
		case 11113:
		{
			global::Char char2 = (global::Char)p;
			bool flag5 = char2 != null;
			if (flag5)
			{
				Service.gI().giaodich(0, char2.charID, -1, -1);
			}
			return;
		}
		case 11114:
		{
			this.popUpYesNo = null;
			GameCanvas.endDlg();
			global::Char char3 = (global::Char)p;
			bool flag6 = char3 == null;
			if (flag6)
			{
				return;
			}
			Service.gI().giaodich(1, char3.charID, -1, -1);
			return;
		}
		case 11115:
		{
			bool flag7 = global::Char.myCharz().charFocus == null;
			if (flag7)
			{
				return;
			}
			InfoDlg.showWait();
			Service.gI().playerMenuAction(global::Char.myCharz().charFocus.charID, (short)global::Char.myCharz().charFocus.menuSelect);
			return;
		}
		case 11120:
		{
			object[] array = (object[])p;
			Skill skill3 = (Skill)array[0];
			int num = int.Parse((string)array[1]);
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				bool flag8 = GameScr.onScreenSkill[i] == skill3;
				if (flag8)
				{
					GameScr.onScreenSkill[i] = null;
				}
			}
			GameScr.onScreenSkill[num] = skill3;
			this.saveonScreenSkillToRMS();
			return;
		}
		case 11121:
		{
			object[] array2 = (object[])p;
			Skill skill4 = (Skill)array2[0];
			int num2 = int.Parse((string)array2[1]);
			for (int j = 0; j < GameScr.keySkill.Length; j++)
			{
				bool flag9 = GameScr.keySkill[j] == skill4;
				if (flag9)
				{
					GameScr.keySkill[j] = null;
				}
			}
			GameScr.keySkill[num2] = skill4;
			this.saveKeySkillToRMS();
			return;
		}
		}
		switch (idAction)
		{
		case 12000:
			Service.gI().getClan(1, -1, null);
			break;
		case 12001:
			GameCanvas.endDlg();
			break;
		case 12002:
		{
			GameCanvas.endDlg();
			ClanObject clanObject = (ClanObject)p;
			Service.gI().clanInvite(1, -1, clanObject.clanID, clanObject.code);
			this.popUpYesNo = null;
			break;
		}
		case 12003:
		{
			ClanObject clanObject2 = (ClanObject)p;
			GameCanvas.endDlg();
			Service.gI().clanInvite(2, -1, clanObject2.clanID, clanObject2.code);
			this.popUpYesNo = null;
			break;
		}
		case 12004:
		{
			Skill skill5 = (Skill)p;
			this.doUseSkill(skill5, true);
			global::Char.myCharz().saveLoadPreviousSkill();
			break;
		}
		case 12005:
		{
			bool flag10 = GameCanvas.serverScr == null;
			if (flag10)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
			GameCanvas.endDlg();
			break;
		}
		case 12006:
			GameMidlet.instance.exit();
			break;
		default:
			switch (idAction)
			{
			case 11000:
				this.actMenu();
				break;
			case 11001:
				global::Char.myCharz().findNextFocusByKey();
				break;
			case 11002:
				GameCanvas.panel.hide();
				break;
			default:
			{
				bool flag11 = idAction != 1;
				if (flag11)
				{
					bool flag12 = idAction != 2;
					if (flag12)
					{
						int num3 = idAction;
						int num4 = num3;
						if (num4 != 11057)
						{
							if (num4 != 11059)
							{
								int num5 = idAction;
								int num6 = num5;
								if (num6 != 110001)
								{
									if (num6 != 110004)
									{
										bool flag13 = idAction != 110382;
										if (flag13)
										{
											bool flag14 = idAction != 110383;
											if (flag14)
											{
												bool flag15 = idAction != 8002;
												if (flag15)
												{
													bool flag16 = idAction != 11038;
													if (flag16)
													{
														bool flag17 = idAction != 11067;
														if (flag17)
														{
															bool flag18 = idAction != 110391;
															if (flag18)
															{
																bool flag19 = idAction == 888351;
																if (flag19)
																{
																	Service.gI().petStatus(5);
																	GameCanvas.endDlg();
																}
															}
															else
															{
																Service.gI().clanInvite(0, global::Char.myCharz().charFocus.charID, -1, -1);
															}
														}
														else
														{
															bool flag20 = TileMap.zoneID != GameScr.indexSelect;
															if (flag20)
															{
																Service.gI().requestChangeZone(GameScr.indexSelect, this.indexItemUse);
																InfoDlg.showWait();
															}
															else
															{
																GameScr.info1.addInfo(mResources.ZONE_HERE, 0);
															}
														}
													}
													else
													{
														this.actDead();
													}
												}
												else
												{
													this.doFire(false, true);
													GameCanvas.clearKeyHold();
													GameCanvas.clearKeyPressed();
												}
											}
											else
											{
												Service.gI().wakeUpFromDead();
											}
										}
										else
										{
											Service.gI().returnTownFromDead();
										}
									}
									else
									{
										GameCanvas.menu.showMenu = false;
									}
								}
								else
								{
									GameCanvas.panel.setTypeMain();
									GameCanvas.panel.show();
								}
							}
							else
							{
								Skill skill6 = GameScr.onScreenSkill[this.selectedIndexSkill];
								this.doUseSkill(skill6, false);
								this.center = null;
							}
						}
						else
						{
							Effect2.vEffect2Outside.removeAllElements();
							Effect2.vEffect2.removeAllElements();
							Npc npc = (Npc)p;
							bool flag21 = npc.idItem == 0;
							if (flag21)
							{
								Service.gI().confirmMenu((short)npc.template.npcTemplateId, (sbyte)GameCanvas.menu.menuSelectedItem);
							}
							else
							{
								bool flag22 = GameCanvas.menu.menuSelectedItem == 0;
								if (flag22)
								{
									Service.gI().pickItem(npc.idItem);
								}
							}
						}
					}
					else
					{
						GameCanvas.menu.showMenu = false;
					}
				}
				else
				{
					GameCanvas.endDlg();
				}
				break;
			}
			}
			break;
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00052940 File Offset: 0x00050B40
	private static void setTouchBtn()
	{
		bool flag = GameScr.isAnalog == 0;
		if (!flag)
		{
			GameScr.xTG = (GameScr.xF = GameCanvas.w - 45);
			bool isLargeGamePad = GameScr.gamePad.isLargeGamePad;
			if (isLargeGamePad)
			{
				GameScr.xSkill = GameScr.gamePad.wZone + 20;
				GameScr.wSkill = 35;
				GameScr.xHP = GameScr.xF - 45;
			}
			else
			{
				bool isMediumGamePad = GameScr.gamePad.isMediumGamePad;
				if (isMediumGamePad)
				{
					GameScr.xHP = GameScr.xF - 45;
				}
			}
			GameScr.yF = GameCanvas.h - 45;
			GameScr.yTG = GameScr.yF - 45;
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x000529E4 File Offset: 0x00050BE4
	private void updateGamePad()
	{
		bool flag = GameScr.isAnalog == 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().statusMe == 14;
			if (!flag2)
			{
				bool flag3 = GameCanvas.isPointerHoldIn(GameScr.xF, GameScr.yF, 40, 40);
				if (flag3)
				{
					mScreen.keyTouch = 5;
					bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
					if (isPointerJustRelease)
					{
						GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					}
				}
				GameScr.gamePad.update();
				bool flag4 = GameCanvas.isPointerHoldIn(GameScr.xTG, GameScr.yTG, 34, 34);
				if (flag4)
				{
					mScreen.keyTouch = 13;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					bool flag5 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
					if (flag5)
					{
						global::Char.myCharz().findNextFocusByKey();
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					}
				}
			}
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00052ADC File Offset: 0x00050CDC
	private void paintGamePad(mGraphics g)
	{
		bool flag = GameScr.isAnalog == 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().statusMe == 14;
			if (!flag2)
			{
				g.drawImage((mScreen.keyTouch != 5 && mScreen.keyMouse != 5) ? GameScr.imgFire0 : GameScr.imgFire1, GameScr.xF + 20, GameScr.yF + 20, mGraphics.HCENTER | mGraphics.VCENTER);
				GameScr.gamePad.paint(g);
				g.drawImage((mScreen.keyTouch != 13) ? GameScr.imgFocus : GameScr.imgFocus2, GameScr.xTG + 20, GameScr.yTG + 20, mGraphics.HCENTER | mGraphics.VCENTER);
			}
		}
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00052B98 File Offset: 0x00050D98
	public void showWinNumber(string num, string finish)
	{
		this.winnumber = new int[num.Length];
		this.randomNumber = new int[num.Length];
		this.tMove = new int[num.Length];
		this.moveCount = new int[num.Length];
		this.delayMove = new int[num.Length];
		try
		{
			for (int i = 0; i < num.Length; i++)
			{
				this.winnumber[i] = (int)short.Parse(num[i].ToString());
				this.randomNumber[i] = Res.random(0, 11);
				this.tMove[i] = 1;
				this.delayMove[i] = 0;
			}
		}
		catch (Exception ex)
		{
		}
		this.tShow = 100;
		this.moveIndex = 0;
		this.strFinish = finish;
		GameScr.lastXS = (GameScr.currXS = mSystem.currentTimeMillis());
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00052C90 File Offset: 0x00050E90
	public void chatVip(string chatVip)
	{
		bool flag = !this.startChat;
		if (flag)
		{
			this.currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
			this.xChatVip = GameCanvas.w;
			this.startChat = true;
		}
		bool flag2 = chatVip.StartsWith("!");
		if (flag2)
		{
			chatVip = chatVip.Substring(1, chatVip.Length);
			this.isFireWorks = true;
		}
		GameScr.vChatVip.addElement(chatVip);
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00052D03 File Offset: 0x00050F03
	public void clearChatVip()
	{
		GameScr.vChatVip.removeAllElements();
		this.xChatVip = GameCanvas.w;
		this.startChat = false;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00052D24 File Offset: 0x00050F24
	public void paintChatVip(mGraphics g)
	{
		bool flag = GameScr.vChatVip.size() == 0;
		if (!flag)
		{
			bool flag2 = !GameScr.isPaintChatVip;
			if (!flag2)
			{
				g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
				g.fillRect(0, GameCanvas.h - 13, GameCanvas.w, 15, 0, 90);
				string st = (string)GameScr.vChatVip.elementAt(0);
				mFont.tahoma_7b_yellow.drawString(g, st, this.xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
			}
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00052DB8 File Offset: 0x00050FB8
	public void updateChatVip()
	{
		bool flag = this.startChat;
		if (flag)
		{
			this.xChatVip -= 2;
			bool flag2 = this.xChatVip < -this.currChatWidth;
			if (flag2)
			{
				this.xChatVip = GameCanvas.w;
				GameScr.vChatVip.removeElementAt(0);
				bool flag3 = GameScr.vChatVip.size() == 0;
				if (flag3)
				{
					this.isFireWorks = false;
					this.startChat = false;
				}
				else
				{
					this.currChatWidth = mFont.tahoma_7b_white.getWidth((string)GameScr.vChatVip.elementAt(0));
				}
			}
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00052E4E File Offset: 0x0005104E
	public void showYourNumber(string strNum)
	{
		this.yourNumber = strNum;
		this.strPaint = mFont.tahoma_7.splitFontArray(this.yourNumber, 500);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00052E73 File Offset: 0x00051073
	public static void checkRemoveImage()
	{
		ImgByName.checkDelHash(ImgByName.hashImagePath, 10, false);
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00052E84 File Offset: 0x00051084
	public static void StartServerPopUp(string strMsg)
	{
		GameCanvas.endDlg();
		int avatar = 1139;
		ChatPopup.addBigMessage(strMsg, 100000, new Npc(-1, 0, 0, 0, 0, 0)
		{
			avatar = avatar
		});
		ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
		ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
		ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00052F0C File Offset: 0x0005110C
	public static bool ispaintPhubangBar()
	{
		return TileMap.mapPhuBang() && GameScr.phuban_Info.type_PB == 0;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00052F38 File Offset: 0x00051138
	public void paintPhuBanBar(mGraphics g, int x, int y, int w)
	{
		bool flag = GameScr.phuban_Info == null;
		if (!flag)
		{
			bool flag2 = !GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow && GameScr.ispaintPhubangBar();
			if (flag2)
			{
				bool flag3 = w < GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4;
				if (flag3)
				{
					w = GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4;
				}
				bool flag4 = x > GameCanvas.w - w / 2;
				if (flag4)
				{
					x = GameCanvas.w - w / 2;
				}
				bool flag5 = x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
				if (flag5)
				{
					x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
				}
				int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
				int num = y + frameHeight + mGraphics.getImageHeight(GameScr.imgBall) / 2 + 2;
				int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
				int num2 = w / 2 - frameWidth / 2;
				int num3 = x - w / 2;
				int num4 = x + frameWidth / 2;
				int y2 = y + 3;
				int num5 = num2 - GameScr.fra_PVE_Bar_0.frameWidth;
				int num6 = num5 / GameScr.fra_PVE_Bar_0.frameWidth;
				bool flag6 = num5 % GameScr.fra_PVE_Bar_0.frameWidth > 0;
				if (flag6)
				{
					num6++;
				}
				for (int i = 0; i < num6; i++)
				{
					bool flag7 = i < num6 - 1;
					if (flag7)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + num5, y2, 0, 0, g);
					}
					bool flag8 = i < num6 - 1;
					if (flag8)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(0, num3, y2, 2, 0, g);
				GameScr.fra_PVE_Bar_0.drawFrame(0, num4 + num5, y2, 0, 0, g);
				bool flag9 = GameScr.phuban_Info.pointTeam1 > 0;
				if (flag9)
				{
					int idx = 2;
					int idx2 = 3;
					bool flag10 = GameScr.phuban_Info.color_1 == 4;
					if (flag10)
					{
						idx = 4;
						idx2 = 5;
					}
					int num7 = GameScr.phuban_Info.pointTeam1 * num2 / GameScr.phuban_Info.maxPoint;
					bool flag11 = num7 < 0;
					if (flag11)
					{
						num7 = 0;
					}
					bool flag12 = num7 > num2;
					if (flag12)
					{
						num7 = num2;
					}
					g.setClip(num3 + num2 - num7, y2, num7, frameHeight);
					for (int j = 0; j < num6; j++)
					{
						bool flag13 = j < num6 - 1;
						if (flag13)
						{
							GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + GameScr.fra_PVE_Bar_0.frameWidth + j * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
						}
						else
						{
							GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + num5, y2, 0, 0, g);
						}
					}
					GameScr.fra_PVE_Bar_0.drawFrame(idx, num3, y2, 2, 0, g);
					GameCanvas.resetTrans(g);
				}
				bool flag14 = GameScr.phuban_Info.pointTeam2 > 0;
				if (flag14)
				{
					int idx3 = 2;
					int idx4 = 3;
					bool flag15 = GameScr.phuban_Info.color_2 == 4;
					if (flag15)
					{
						idx3 = 4;
						idx4 = 5;
					}
					int num8 = GameScr.phuban_Info.pointTeam2 * num2 / GameScr.phuban_Info.maxPoint;
					bool flag16 = num8 < 0;
					if (flag16)
					{
						num8 = 0;
					}
					bool flag17 = num8 > num2;
					if (flag17)
					{
						num8 = num2;
					}
					g.setClip(num4, y2, num8, frameHeight);
					for (int k = 0; k < num6; k++)
					{
						bool flag18 = k < num6 - 1;
						if (flag18)
						{
							GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + k * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
						}
						else
						{
							GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
						}
					}
					GameScr.fra_PVE_Bar_0.drawFrame(idx3, num4 + num5, y2, 0, 0, g);
					GameCanvas.resetTrans(g);
				}
				GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
				string timeCountDown = mSystem.getTimeCountDown(GameScr.phuban_Info.timeStart, (int)GameScr.phuban_Info.timeSecond, true, false);
				mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + GameScr.fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
				Panel.setTextColor(GameScr.phuban_Info.color_1, 1).drawString(g, GameScr.phuban_Info.nameTeam1, x - 5, num + 5, 1);
				Panel.setTextColor(GameScr.phuban_Info.color_2, 1).drawString(g, GameScr.phuban_Info.nameTeam2, x + 5, num + 5, 0);
				bool flag19 = GameScr.phuban_Info.type_PB != 0;
				if (flag19)
				{
					int y3 = y + frameHeight / 2 - 2;
					mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam1.ToString(), num3 + num2 / 2, y3, 2);
					mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam2.ToString(), num4 + num2 / 2, y3, 2);
				}
				g.drawImage(GameScr.imgVS, x, y + GameScr.fra_PVE_Bar_1.frameHeight + 2, 3);
				bool flag20 = GameScr.phuban_Info.type_PB == 0;
				if (flag20)
				{
					GameScr.paintChienTruong_Life(g, GameScr.phuban_Info.maxLife, GameScr.phuban_Info.color_1, GameScr.phuban_Info.lifeTeam1, x - 13, GameScr.phuban_Info.color_2, GameScr.phuban_Info.lifeTeam2, x + 13, num);
				}
			}
		}
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00053554 File Offset: 0x00051754
	public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
	{
		bool flag = GameScr.imgBall != null;
		if (flag)
		{
			int num = mGraphics.getImageHeight(GameScr.imgBall) / 2;
			for (int i = 0; i < maxLife; i++)
			{
				int num2 = 0;
				bool flag2 = i < lifeTeam1;
				if (flag2)
				{
					num2 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num2 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			for (int j = 0; j < maxLife; j++)
			{
				int num3 = 0;
				bool flag3 = j < lifeTeam2;
				if (flag3)
				{
					num3 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num3 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00053634 File Offset: 0x00051834
	public static void paintHPBar_NEW(mGraphics g, int x, int y, global::Char c)
	{
		g.drawImage(GameScr.imgKhung, x, y, 0);
		int x2 = x + 3;
		int num = y + 19;
		int width = GameScr.imgHP_NEW.getWidth();
		int num2 = GameScr.imgHP_NEW.getHeight() / 2;
		int num3 = c.cHP * width / c.cHPFull;
		bool flag = num3 <= 0;
		if (flag)
		{
			num3 = 1;
		}
		else
		{
			bool flag2 = num3 > width;
			if (flag2)
			{
				num3 = width;
			}
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, num2, num3, num2, 0, x2, num, 0);
		int num4 = c.cMP * width / c.cMPFull;
		bool flag3 = num4 <= 0;
		if (flag3)
		{
			num4 = 1;
		}
		else
		{
			bool flag4 = num4 > width;
			if (flag4)
			{
				num4 = width;
			}
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, 0, num4, num2, 0, x2, num + 6, 0);
		int x3 = x + GameScr.imgKhung.getWidth() / 2 + 1;
		int y2 = num + 13;
		mFont.tahoma_7_green2.drawString(g, c.cName, x3, y + 4, 2);
		bool flag5 = c.mobFocus != null;
		if (flag5)
		{
			bool flag6 = c.mobFocus.getTemplate() != null;
			if (flag6)
			{
				mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, x3, y2, 2);
			}
		}
		else
		{
			bool flag7 = c.npcFocus != null;
			if (flag7)
			{
				mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, x3, y2, 2);
			}
			else
			{
				bool flag8 = c.charFocus != null;
				if (flag8)
				{
					mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, x3, y2, 2);
				}
			}
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000537E8 File Offset: 0x000519E8
	public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		Effect_End eff = new Effect_End(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj);
		GameScr.addEffect2Vector(eff);
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00053814 File Offset: 0x00051A14
	public static void addEffectEnd_Target(int type, int subtype, int typePaint, global::Char charUse, Point target, int levelPaint, short timeRemove, short range)
	{
		Effect_End eff = new Effect_End(type, subtype, typePaint, charUse.clone(), target, levelPaint, timeRemove, range);
		GameScr.addEffect2Vector(eff);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00053840 File Offset: 0x00051A40
	public static void addEffect2Vector(Effect_End eff)
	{
		bool flag = eff.levelPaint == 0;
		if (flag)
		{
			EffectManager.addHiEffect(eff);
		}
		else
		{
			bool flag2 = eff.levelPaint == 1;
			if (flag2)
			{
				EffectManager.addMidEffects(eff);
			}
			else
			{
				bool flag3 = eff.levelPaint == 2;
				if (flag3)
				{
					EffectManager.addMid_2Effects(eff);
				}
				else
				{
					EffectManager.addLowEffect(eff);
				}
			}
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000538A0 File Offset: 0x00051AA0
	public static bool setIsInScreen(int x, int y, int wOne, int hOne)
	{
		return x >= GameScr.cmx - wOne && x <= GameScr.cmx + GameCanvas.w + wOne && y >= GameScr.cmy - hOne && y <= GameScr.cmy + GameCanvas.h + hOne * 3 / 2;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x000538F0 File Offset: 0x00051AF0
	public static bool isSmallScr()
	{
		return GameCanvas.w <= 320;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00053914 File Offset: 0x00051B14
	private void paint_xp_bar(mGraphics g)
	{
		g.setColor(8421504);
		g.fillRect(0, GameCanvas.h - 2, GameCanvas.w, 2);
		int num = (int)(global::Char.myCharz().cLevelPercent * (long)GameCanvas.w / 10000L);
		g.setColor(16777215);
		g.fillRect(0, GameCanvas.h - 2, num, 2);
		g.setColor(0);
		num = GameCanvas.w / 10;
		for (int i = 1; i < 10; i++)
		{
			g.fillRect(i * num, GameCanvas.h - 2, 1, 2);
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000539B4 File Offset: 0x00051BB4
	private void paint_ios_bg(mGraphics g)
	{
		bool flag = mSystem.clientType != 5;
		if (!flag)
		{
			bool flag2 = GameScr.imgBgIOS != null;
			if (flag2)
			{
				g.setColor(16777215);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				g.drawImage(GameScr.imgBgIOS, GameCanvas.w / 2, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			else
			{
				GameScr.imgBgIOS = GameCanvas.loadImage("/bg/bg_ios_" + ((TileMap.bgID % 2 != 0) ? 1 : 2).ToString() + ".png");
			}
		}
	}

	// Token: 0x040006A5 RID: 1701
	public bool isWaitingDoubleClick;

	// Token: 0x040006A6 RID: 1702
	public long timeStartDblClick;

	// Token: 0x040006A7 RID: 1703
	public long timeEndDblClick;

	// Token: 0x040006A8 RID: 1704
	public static bool isPaintOther = false;

	// Token: 0x040006A9 RID: 1705
	public static MyVector textTime = new MyVector(string.Empty);

	// Token: 0x040006AA RID: 1706
	public static bool isLoadAllData = false;

	// Token: 0x040006AB RID: 1707
	public static GameScr instance;

	// Token: 0x040006AC RID: 1708
	public static int gW;

	// Token: 0x040006AD RID: 1709
	public static int gH;

	// Token: 0x040006AE RID: 1710
	public static int gW2;

	// Token: 0x040006AF RID: 1711
	public static int gssw;

	// Token: 0x040006B0 RID: 1712
	public static int gssh;

	// Token: 0x040006B1 RID: 1713
	public static int gH34;

	// Token: 0x040006B2 RID: 1714
	public static int gW3;

	// Token: 0x040006B3 RID: 1715
	public static int gH3;

	// Token: 0x040006B4 RID: 1716
	public static int gH23;

	// Token: 0x040006B5 RID: 1717
	public static int gW23;

	// Token: 0x040006B6 RID: 1718
	public static int gH2;

	// Token: 0x040006B7 RID: 1719
	public static int csPadMaxH;

	// Token: 0x040006B8 RID: 1720
	public static int cmdBarH;

	// Token: 0x040006B9 RID: 1721
	public static int gW34;

	// Token: 0x040006BA RID: 1722
	public static int gW6;

	// Token: 0x040006BB RID: 1723
	public static int gH6;

	// Token: 0x040006BC RID: 1724
	public static int cmx;

	// Token: 0x040006BD RID: 1725
	public static int cmy;

	// Token: 0x040006BE RID: 1726
	public static int cmdx;

	// Token: 0x040006BF RID: 1727
	public static int cmdy;

	// Token: 0x040006C0 RID: 1728
	public static int cmvx;

	// Token: 0x040006C1 RID: 1729
	public static int cmvy;

	// Token: 0x040006C2 RID: 1730
	public static int cmtoX;

	// Token: 0x040006C3 RID: 1731
	public static int cmtoY;

	// Token: 0x040006C4 RID: 1732
	public static int cmxLim;

	// Token: 0x040006C5 RID: 1733
	public static int cmyLim;

	// Token: 0x040006C6 RID: 1734
	public static int gssx;

	// Token: 0x040006C7 RID: 1735
	public static int gssy;

	// Token: 0x040006C8 RID: 1736
	public static int gssxe;

	// Token: 0x040006C9 RID: 1737
	public static int gssye;

	// Token: 0x040006CA RID: 1738
	public Command cmdback;

	// Token: 0x040006CB RID: 1739
	public Command cmdBag;

	// Token: 0x040006CC RID: 1740
	public Command cmdSkill;

	// Token: 0x040006CD RID: 1741
	public Command cmdTiemnang;

	// Token: 0x040006CE RID: 1742
	public Command cmdtrangbi;

	// Token: 0x040006CF RID: 1743
	public Command cmdInfo;

	// Token: 0x040006D0 RID: 1744
	public Command cmdFocus;

	// Token: 0x040006D1 RID: 1745
	public Command cmdFire;

	// Token: 0x040006D2 RID: 1746
	public static int d;

	// Token: 0x040006D3 RID: 1747
	public static int hpPotion;

	// Token: 0x040006D4 RID: 1748
	public static SkillPaint[] sks;

	// Token: 0x040006D5 RID: 1749
	public static Arrowpaint[] arrs;

	// Token: 0x040006D6 RID: 1750
	public static DartInfo[] darts;

	// Token: 0x040006D7 RID: 1751
	public static Part[] parts;

	// Token: 0x040006D8 RID: 1752
	public static EffectCharPaint[] efs;

	// Token: 0x040006D9 RID: 1753
	public static int lockTick;

	// Token: 0x040006DA RID: 1754
	private int moveUp;

	// Token: 0x040006DB RID: 1755
	private int moveDow;

	// Token: 0x040006DC RID: 1756
	private int idTypeTask;

	// Token: 0x040006DD RID: 1757
	private bool isstarOpen;

	// Token: 0x040006DE RID: 1758
	private bool isChangeSkill;

	// Token: 0x040006DF RID: 1759
	public static MyVector vClan = new MyVector();

	// Token: 0x040006E0 RID: 1760
	public static MyVector vPtMap = new MyVector();

	// Token: 0x040006E1 RID: 1761
	public static MyVector vFriend = new MyVector();

	// Token: 0x040006E2 RID: 1762
	public static MyVector vEnemies = new MyVector();

	// Token: 0x040006E3 RID: 1763
	public static MyVector vCharInMap = new MyVector();

	// Token: 0x040006E4 RID: 1764
	public static MyVector vItemMap = new MyVector();

	// Token: 0x040006E5 RID: 1765
	public static MyVector vMobAttack = new MyVector();

	// Token: 0x040006E6 RID: 1766
	public static MyVector vSet = new MyVector();

	// Token: 0x040006E7 RID: 1767
	public static MyVector vMob = new MyVector();

	// Token: 0x040006E8 RID: 1768
	public static MyVector vNpc = new MyVector();

	// Token: 0x040006E9 RID: 1769
	public static MyVector vFlag = new MyVector();

	// Token: 0x040006EA RID: 1770
	public static NClass[] nClasss;

	// Token: 0x040006EB RID: 1771
	public static int indexSize = 28;

	// Token: 0x040006EC RID: 1772
	public static int indexTitle = 0;

	// Token: 0x040006ED RID: 1773
	public static int indexSelect = 0;

	// Token: 0x040006EE RID: 1774
	public static int indexRow = -1;

	// Token: 0x040006EF RID: 1775
	public static int indexRowMax;

	// Token: 0x040006F0 RID: 1776
	public static int indexMenu = 0;

	// Token: 0x040006F1 RID: 1777
	public Item itemFocus;

	// Token: 0x040006F2 RID: 1778
	public ItemOptionTemplate[] iOptionTemplates;

	// Token: 0x040006F3 RID: 1779
	public SkillOptionTemplate[] sOptionTemplates;

	// Token: 0x040006F4 RID: 1780
	private static Scroll scrInfo = new Scroll();

	// Token: 0x040006F5 RID: 1781
	public static Scroll scrMain = new Scroll();

	// Token: 0x040006F6 RID: 1782
	public static MyVector vItemUpGrade = new MyVector();

	// Token: 0x040006F7 RID: 1783
	public static bool isTypeXu;

	// Token: 0x040006F8 RID: 1784
	public static bool isViewNext;

	// Token: 0x040006F9 RID: 1785
	public static bool isViewClanMemOnline = false;

	// Token: 0x040006FA RID: 1786
	public static bool isViewClanInvite = true;

	// Token: 0x040006FB RID: 1787
	public static bool isChop;

	// Token: 0x040006FC RID: 1788
	public static string titleInputText = string.Empty;

	// Token: 0x040006FD RID: 1789
	public static int tickMove;

	// Token: 0x040006FE RID: 1790
	public static bool isPaintAlert = false;

	// Token: 0x040006FF RID: 1791
	public static bool isPaintTask = false;

	// Token: 0x04000700 RID: 1792
	public static bool isPaintTeam = false;

	// Token: 0x04000701 RID: 1793
	public static bool isPaintFindTeam = false;

	// Token: 0x04000702 RID: 1794
	public static bool isPaintFriend = false;

	// Token: 0x04000703 RID: 1795
	public static bool isPaintEnemies = false;

	// Token: 0x04000704 RID: 1796
	public static bool isPaintItemInfo = false;

	// Token: 0x04000705 RID: 1797
	public static bool isHaveSelectSkill = false;

	// Token: 0x04000706 RID: 1798
	public static bool isPaintSkill = false;

	// Token: 0x04000707 RID: 1799
	public static bool isPaintInfoMe = false;

	// Token: 0x04000708 RID: 1800
	public static bool isPaintStore = false;

	// Token: 0x04000709 RID: 1801
	public static bool isPaintNonNam = false;

	// Token: 0x0400070A RID: 1802
	public static bool isPaintNonNu = false;

	// Token: 0x0400070B RID: 1803
	public static bool isPaintAoNam = false;

	// Token: 0x0400070C RID: 1804
	public static bool isPaintAoNu = false;

	// Token: 0x0400070D RID: 1805
	public static bool isPaintGangTayNam = false;

	// Token: 0x0400070E RID: 1806
	public static bool isPaintGangTayNu = false;

	// Token: 0x0400070F RID: 1807
	public static bool isPaintQuanNam = false;

	// Token: 0x04000710 RID: 1808
	public static bool isPaintQuanNu = false;

	// Token: 0x04000711 RID: 1809
	public static bool isPaintGiayNam = false;

	// Token: 0x04000712 RID: 1810
	public static bool isPaintGiayNu = false;

	// Token: 0x04000713 RID: 1811
	public static bool isPaintLien = false;

	// Token: 0x04000714 RID: 1812
	public static bool isPaintNhan = false;

	// Token: 0x04000715 RID: 1813
	public static bool isPaintNgocBoi = false;

	// Token: 0x04000716 RID: 1814
	public static bool isPaintPhu = false;

	// Token: 0x04000717 RID: 1815
	public static bool isPaintWeapon = false;

	// Token: 0x04000718 RID: 1816
	public static bool isPaintStack = false;

	// Token: 0x04000719 RID: 1817
	public static bool isPaintStackLock = false;

	// Token: 0x0400071A RID: 1818
	public static bool isPaintGrocery = false;

	// Token: 0x0400071B RID: 1819
	public static bool isPaintGroceryLock = false;

	// Token: 0x0400071C RID: 1820
	public static bool isPaintUpGrade = false;

	// Token: 0x0400071D RID: 1821
	public static bool isPaintConvert = false;

	// Token: 0x0400071E RID: 1822
	public static bool isPaintUpGradeGold = false;

	// Token: 0x0400071F RID: 1823
	public static bool isPaintUpPearl = false;

	// Token: 0x04000720 RID: 1824
	public static bool isPaintBox = false;

	// Token: 0x04000721 RID: 1825
	public static bool isPaintSplit = false;

	// Token: 0x04000722 RID: 1826
	public static bool isPaintCharInMap = false;

	// Token: 0x04000723 RID: 1827
	public static bool isPaintTrade = false;

	// Token: 0x04000724 RID: 1828
	public static bool isPaintZone = false;

	// Token: 0x04000725 RID: 1829
	public static bool isPaintMessage = false;

	// Token: 0x04000726 RID: 1830
	public static bool isPaintClan = false;

	// Token: 0x04000727 RID: 1831
	public static bool isRequestMember = false;

	// Token: 0x04000728 RID: 1832
	public static global::Char currentCharViewInfo;

	// Token: 0x04000729 RID: 1833
	public static long[] exps;

	// Token: 0x0400072A RID: 1834
	public static int[] crystals;

	// Token: 0x0400072B RID: 1835
	public static int[] upClothe;

	// Token: 0x0400072C RID: 1836
	public static int[] upAdorn;

	// Token: 0x0400072D RID: 1837
	public static int[] upWeapon;

	// Token: 0x0400072E RID: 1838
	public static int[] coinUpCrystals;

	// Token: 0x0400072F RID: 1839
	public static int[] coinUpClothes;

	// Token: 0x04000730 RID: 1840
	public static int[] coinUpAdorns;

	// Token: 0x04000731 RID: 1841
	public static int[] coinUpWeapons;

	// Token: 0x04000732 RID: 1842
	public static int[] maxPercents;

	// Token: 0x04000733 RID: 1843
	public static int[] goldUps;

	// Token: 0x04000734 RID: 1844
	public int tMenuDelay;

	// Token: 0x04000735 RID: 1845
	public int zoneCol = 6;

	// Token: 0x04000736 RID: 1846
	public int[] zones;

	// Token: 0x04000737 RID: 1847
	public int[] pts;

	// Token: 0x04000738 RID: 1848
	public int[] numPlayer;

	// Token: 0x04000739 RID: 1849
	public int[] maxPlayer;

	// Token: 0x0400073A RID: 1850
	public int[] rank1;

	// Token: 0x0400073B RID: 1851
	public int[] rank2;

	// Token: 0x0400073C RID: 1852
	public string[] rankName1;

	// Token: 0x0400073D RID: 1853
	public string[] rankName2;

	// Token: 0x0400073E RID: 1854
	public int typeTrade;

	// Token: 0x0400073F RID: 1855
	public int typeTradeOrder;

	// Token: 0x04000740 RID: 1856
	public int coinTrade;

	// Token: 0x04000741 RID: 1857
	public int coinTradeOrder;

	// Token: 0x04000742 RID: 1858
	public int timeTrade;

	// Token: 0x04000743 RID: 1859
	public int indexItemUse = -1;

	// Token: 0x04000744 RID: 1860
	public int cLastFocusID = -1;

	// Token: 0x04000745 RID: 1861
	public int cPreFocusID = -1;

	// Token: 0x04000746 RID: 1862
	public bool isLockKey;

	// Token: 0x04000747 RID: 1863
	public static int[] tasks;

	// Token: 0x04000748 RID: 1864
	public static int[] mapTasks;

	// Token: 0x04000749 RID: 1865
	public static Image imgRoomStat;

	// Token: 0x0400074A RID: 1866
	public static Image frBarPow0;

	// Token: 0x0400074B RID: 1867
	public static Image frBarPow1;

	// Token: 0x0400074C RID: 1868
	public static Image frBarPow2;

	// Token: 0x0400074D RID: 1869
	public static Image frBarPow20;

	// Token: 0x0400074E RID: 1870
	public static Image frBarPow21;

	// Token: 0x0400074F RID: 1871
	public static Image frBarPow22;

	// Token: 0x04000750 RID: 1872
	public MyVector texts;

	// Token: 0x04000751 RID: 1873
	public string textsTitle;

	// Token: 0x04000752 RID: 1874
	public static sbyte vcData;

	// Token: 0x04000753 RID: 1875
	public static sbyte vcMap;

	// Token: 0x04000754 RID: 1876
	public static sbyte vcSkill;

	// Token: 0x04000755 RID: 1877
	public static sbyte vcItem;

	// Token: 0x04000756 RID: 1878
	public static sbyte vsData;

	// Token: 0x04000757 RID: 1879
	public static sbyte vsMap;

	// Token: 0x04000758 RID: 1880
	public static sbyte vsSkill;

	// Token: 0x04000759 RID: 1881
	public static sbyte vsItem;

	// Token: 0x0400075A RID: 1882
	public static sbyte vcTask;

	// Token: 0x0400075B RID: 1883
	public static Image imgArrow;

	// Token: 0x0400075C RID: 1884
	public static Image imgArrow2;

	// Token: 0x0400075D RID: 1885
	public static Image imgChat;

	// Token: 0x0400075E RID: 1886
	public static Image imgChat2;

	// Token: 0x0400075F RID: 1887
	public static Image imgMenu;

	// Token: 0x04000760 RID: 1888
	public static Image imgFocus;

	// Token: 0x04000761 RID: 1889
	public static Image imgFocus2;

	// Token: 0x04000762 RID: 1890
	public static Image imgSkill;

	// Token: 0x04000763 RID: 1891
	public static Image imgSkill2;

	// Token: 0x04000764 RID: 1892
	public static Image imgHP1;

	// Token: 0x04000765 RID: 1893
	public static Image imgHP2;

	// Token: 0x04000766 RID: 1894
	public static Image imgHP3;

	// Token: 0x04000767 RID: 1895
	public static Image imgHP4;

	// Token: 0x04000768 RID: 1896
	public static Image imgFire0;

	// Token: 0x04000769 RID: 1897
	public static Image imgFire1;

	// Token: 0x0400076A RID: 1898
	public static Image imgLbtn;

	// Token: 0x0400076B RID: 1899
	public static Image imgLbtnFocus;

	// Token: 0x0400076C RID: 1900
	public static Image imgLbtn2;

	// Token: 0x0400076D RID: 1901
	public static Image imgLbtnFocus2;

	// Token: 0x0400076E RID: 1902
	public static Image imgAnalog1;

	// Token: 0x0400076F RID: 1903
	public static Image imgAnalog2;

	// Token: 0x04000770 RID: 1904
	public string tradeName = string.Empty;

	// Token: 0x04000771 RID: 1905
	public string tradeItemName = string.Empty;

	// Token: 0x04000772 RID: 1906
	public int timeLengthMap;

	// Token: 0x04000773 RID: 1907
	public int timeStartMap;

	// Token: 0x04000774 RID: 1908
	public static sbyte typeViewInfo = 0;

	// Token: 0x04000775 RID: 1909
	public static sbyte typeActive = 0;

	// Token: 0x04000776 RID: 1910
	public static InfoMe info1 = new InfoMe();

	// Token: 0x04000777 RID: 1911
	public static InfoMe info2 = new InfoMe();

	// Token: 0x04000778 RID: 1912
	public static Image imgPanel;

	// Token: 0x04000779 RID: 1913
	public static Image imgPanel2;

	// Token: 0x0400077A RID: 1914
	public static Image imgHP;

	// Token: 0x0400077B RID: 1915
	public static Image imgMP;

	// Token: 0x0400077C RID: 1916
	public static Image imgSP;

	// Token: 0x0400077D RID: 1917
	public static Image imgHPLost;

	// Token: 0x0400077E RID: 1918
	public static Image imgMPLost;

	// Token: 0x0400077F RID: 1919
	public static Image imgHP_tm_do;

	// Token: 0x04000780 RID: 1920
	public static Image imgHP_tm_vang;

	// Token: 0x04000781 RID: 1921
	public static Image imgHP_tm_xam;

	// Token: 0x04000782 RID: 1922
	public static Image imgHP_tm_xanh;

	// Token: 0x04000783 RID: 1923
	public Mob mobCapcha;

	// Token: 0x04000784 RID: 1924
	public MagicTree magicTree;

	// Token: 0x04000785 RID: 1925
	private short l;

	// Token: 0x04000786 RID: 1926
	public static int countEff;

	// Token: 0x04000787 RID: 1927
	public static GamePad gamePad = new GamePad();

	// Token: 0x04000788 RID: 1928
	public static Image imgChatPC;

	// Token: 0x04000789 RID: 1929
	public static Image imgChatsPC2;

	// Token: 0x0400078A RID: 1930
	public static int isAnalog = 0;

	// Token: 0x0400078B RID: 1931
	public static bool isUseTouch;

	// Token: 0x0400078C RID: 1932
	public const int numSkill = 10;

	// Token: 0x0400078D RID: 1933
	public const int numSkill_2 = 5;

	// Token: 0x0400078E RID: 1934
	public static Skill[] keySkill = new Skill[10];

	// Token: 0x0400078F RID: 1935
	public static Skill[] onScreenSkill = new Skill[10];

	// Token: 0x04000790 RID: 1936
	public Command cmdMenu;

	// Token: 0x04000791 RID: 1937
	public static int firstY;

	// Token: 0x04000792 RID: 1938
	public static int wSkill;

	// Token: 0x04000793 RID: 1939
	public static long deltaTime;

	// Token: 0x04000794 RID: 1940
	public bool isPointerDowning;

	// Token: 0x04000795 RID: 1941
	public bool isChangingCameraMode;

	// Token: 0x04000796 RID: 1942
	private int ptLastDownX;

	// Token: 0x04000797 RID: 1943
	private int ptLastDownY;

	// Token: 0x04000798 RID: 1944
	private int ptFirstDownX;

	// Token: 0x04000799 RID: 1945
	private int ptFirstDownY;

	// Token: 0x0400079A RID: 1946
	private int ptDownTime;

	// Token: 0x0400079B RID: 1947
	private bool disableSingleClick;

	// Token: 0x0400079C RID: 1948
	public long lastSingleClick;

	// Token: 0x0400079D RID: 1949
	public bool clickMoving;

	// Token: 0x0400079E RID: 1950
	public bool clickOnTileTop;

	// Token: 0x0400079F RID: 1951
	public bool clickMovingRed;

	// Token: 0x040007A0 RID: 1952
	private int clickToX;

	// Token: 0x040007A1 RID: 1953
	private int clickToY;

	// Token: 0x040007A2 RID: 1954
	private int lastClickCMX;

	// Token: 0x040007A3 RID: 1955
	private int lastClickCMY;

	// Token: 0x040007A4 RID: 1956
	private int clickMovingP1;

	// Token: 0x040007A5 RID: 1957
	private int clickMovingTimeOut;

	// Token: 0x040007A6 RID: 1958
	private long lastMove;

	// Token: 0x040007A7 RID: 1959
	public static bool isNewClanMessage;

	// Token: 0x040007A8 RID: 1960
	private long lastFire;

	// Token: 0x040007A9 RID: 1961
	private long lastUsePotion;

	// Token: 0x040007AA RID: 1962
	public int auto;

	// Token: 0x040007AB RID: 1963
	public int dem;

	// Token: 0x040007AC RID: 1964
	private string strTam = string.Empty;

	// Token: 0x040007AD RID: 1965
	private int a;

	// Token: 0x040007AE RID: 1966
	public bool isFreez;

	// Token: 0x040007AF RID: 1967
	public bool isUseFreez;

	// Token: 0x040007B0 RID: 1968
	public static Image imgTrans;

	// Token: 0x040007B1 RID: 1969
	public bool isRongThanXuatHien;

	// Token: 0x040007B2 RID: 1970
	public bool isRongNamek;

	// Token: 0x040007B3 RID: 1971
	public bool isSuperPower;

	// Token: 0x040007B4 RID: 1972
	public int tPower;

	// Token: 0x040007B5 RID: 1973
	public int xPower;

	// Token: 0x040007B6 RID: 1974
	public int yPower;

	// Token: 0x040007B7 RID: 1975
	public int dxPower;

	// Token: 0x040007B8 RID: 1976
	public bool activeRongThan;

	// Token: 0x040007B9 RID: 1977
	public bool isMeCallRongThan;

	// Token: 0x040007BA RID: 1978
	public int mautroi;

	// Token: 0x040007BB RID: 1979
	public int mapRID;

	// Token: 0x040007BC RID: 1980
	public int zoneRID;

	// Token: 0x040007BD RID: 1981
	public int bgRID = -1;

	// Token: 0x040007BE RID: 1982
	public static int tam = 0;

	// Token: 0x040007BF RID: 1983
	public static bool isAutoPlay;

	// Token: 0x040007C0 RID: 1984
	public static bool canAutoPlay;

	// Token: 0x040007C1 RID: 1985
	public static bool isChangeZone;

	// Token: 0x040007C2 RID: 1986
	private int timeSkill;

	// Token: 0x040007C3 RID: 1987
	private int nSkill;

	// Token: 0x040007C4 RID: 1988
	private int selectedIndexSkill = -1;

	// Token: 0x040007C5 RID: 1989
	private Skill lastSkill;

	// Token: 0x040007C6 RID: 1990
	private bool doSeleckSkillFlag;

	// Token: 0x040007C7 RID: 1991
	public string strCapcha;

	// Token: 0x040007C8 RID: 1992
	private long longPress;

	// Token: 0x040007C9 RID: 1993
	private int move;

	// Token: 0x040007CA RID: 1994
	public bool flareFindFocus;

	// Token: 0x040007CB RID: 1995
	private int flareTime;

	// Token: 0x040007CC RID: 1996
	public int keyTouchSkill = -1;

	// Token: 0x040007CD RID: 1997
	private long lastSendUpdatePostion;

	// Token: 0x040007CE RID: 1998
	public static long lastTick;

	// Token: 0x040007CF RID: 1999
	public static long currTick;

	// Token: 0x040007D0 RID: 2000
	private int timeAuto;

	// Token: 0x040007D1 RID: 2001
	public static long lastXS;

	// Token: 0x040007D2 RID: 2002
	public static long currXS;

	// Token: 0x040007D3 RID: 2003
	public static int secondXS;

	// Token: 0x040007D4 RID: 2004
	public int runArrow;

	// Token: 0x040007D5 RID: 2005
	public static int isPaintRada;

	// Token: 0x040007D6 RID: 2006
	public static Image imgNut;

	// Token: 0x040007D7 RID: 2007
	public static Image imgNutF;

	// Token: 0x040007D8 RID: 2008
	public int[] keyCapcha;

	// Token: 0x040007D9 RID: 2009
	public static Image imgCapcha;

	// Token: 0x040007DA RID: 2010
	public string keyInput;

	// Token: 0x040007DB RID: 2011
	public static int disXC;

	// Token: 0x040007DC RID: 2012
	public static bool isPaint = true;

	// Token: 0x040007DD RID: 2013
	public static int shock_scr;

	// Token: 0x040007DE RID: 2014
	private static int[] shock_x = new int[]
	{
		1,
		-1,
		1,
		-1
	};

	// Token: 0x040007DF RID: 2015
	private static int[] shock_y = new int[]
	{
		1,
		-1,
		-1,
		1
	};

	// Token: 0x040007E0 RID: 2016
	private int tDoubleDelay;

	// Token: 0x040007E1 RID: 2017
	public static Image arrow;

	// Token: 0x040007E2 RID: 2018
	private static int yTouchBar;

	// Token: 0x040007E3 RID: 2019
	private static int xC;

	// Token: 0x040007E4 RID: 2020
	private static int yC;

	// Token: 0x040007E5 RID: 2021
	private static int xL;

	// Token: 0x040007E6 RID: 2022
	private static int yL;

	// Token: 0x040007E7 RID: 2023
	public int xR;

	// Token: 0x040007E8 RID: 2024
	public int yR;

	// Token: 0x040007E9 RID: 2025
	private static int xU;

	// Token: 0x040007EA RID: 2026
	private static int yU;

	// Token: 0x040007EB RID: 2027
	private static int xF;

	// Token: 0x040007EC RID: 2028
	private static int yF;

	// Token: 0x040007ED RID: 2029
	public static int xHP;

	// Token: 0x040007EE RID: 2030
	public static int yHP;

	// Token: 0x040007EF RID: 2031
	private static int xTG;

	// Token: 0x040007F0 RID: 2032
	private static int yTG;

	// Token: 0x040007F1 RID: 2033
	public static int[] xS;

	// Token: 0x040007F2 RID: 2034
	public static int[] yS;

	// Token: 0x040007F3 RID: 2035
	public static int xSkill;

	// Token: 0x040007F4 RID: 2036
	public static int ySkill;

	// Token: 0x040007F5 RID: 2037
	public static int padSkill;

	// Token: 0x040007F6 RID: 2038
	public int dMP;

	// Token: 0x040007F7 RID: 2039
	public int twMp;

	// Token: 0x040007F8 RID: 2040
	public bool isInjureMp;

	// Token: 0x040007F9 RID: 2041
	public int dHP;

	// Token: 0x040007FA RID: 2042
	public int twHp;

	// Token: 0x040007FB RID: 2043
	public bool isInjureHp;

	// Token: 0x040007FC RID: 2044
	private long curr;

	// Token: 0x040007FD RID: 2045
	private long last;

	// Token: 0x040007FE RID: 2046
	private int secondVS;

	// Token: 0x040007FF RID: 2047
	private int[] idVS = new int[]
	{
		-1,
		-1
	};

	// Token: 0x04000800 RID: 2048
	public static string[] flyTextString;

	// Token: 0x04000801 RID: 2049
	public static int[] flyTextX;

	// Token: 0x04000802 RID: 2050
	public static int[] flyTextY;

	// Token: 0x04000803 RID: 2051
	public static int[] flyTextYTo;

	// Token: 0x04000804 RID: 2052
	public static int[] flyTextDx;

	// Token: 0x04000805 RID: 2053
	public static int[] flyTextDy;

	// Token: 0x04000806 RID: 2054
	public static int[] flyTextState;

	// Token: 0x04000807 RID: 2055
	public static int[] flyTextColor;

	// Token: 0x04000808 RID: 2056
	public static int[] flyTime;

	// Token: 0x04000809 RID: 2057
	public static int[] splashX;

	// Token: 0x0400080A RID: 2058
	public static int[] splashY;

	// Token: 0x0400080B RID: 2059
	public static int[] splashState;

	// Token: 0x0400080C RID: 2060
	public static int[] splashF;

	// Token: 0x0400080D RID: 2061
	public static int[] splashDir;

	// Token: 0x0400080E RID: 2062
	public static Image[] imgSplash;

	// Token: 0x0400080F RID: 2063
	public static int cmdBarX;

	// Token: 0x04000810 RID: 2064
	public static int cmdBarY;

	// Token: 0x04000811 RID: 2065
	public static int cmdBarW;

	// Token: 0x04000812 RID: 2066
	public static int cmdBarLeftW;

	// Token: 0x04000813 RID: 2067
	public static int cmdBarRightW;

	// Token: 0x04000814 RID: 2068
	public static int cmdBarCenterW;

	// Token: 0x04000815 RID: 2069
	public static int hpBarX;

	// Token: 0x04000816 RID: 2070
	public static int hpBarY;

	// Token: 0x04000817 RID: 2071
	public static int spBarW;

	// Token: 0x04000818 RID: 2072
	public static int mpBarW;

	// Token: 0x04000819 RID: 2073
	public static int expBarW;

	// Token: 0x0400081A RID: 2074
	public static int lvPosX;

	// Token: 0x0400081B RID: 2075
	public static int moneyPosX;

	// Token: 0x0400081C RID: 2076
	public static int hpBarH;

	// Token: 0x0400081D RID: 2077
	public static int girlHPBarY;

	// Token: 0x0400081E RID: 2078
	public static long hpBarW;

	// Token: 0x0400081F RID: 2079
	public static Image[] imgCmdBar;

	// Token: 0x04000820 RID: 2080
	private int imgScrW;

	// Token: 0x04000821 RID: 2081
	public static int popupY;

	// Token: 0x04000822 RID: 2082
	public static int popupX;

	// Token: 0x04000823 RID: 2083
	public static int isborderIndex;

	// Token: 0x04000824 RID: 2084
	public static int isselectedRow;

	// Token: 0x04000825 RID: 2085
	private static Image imgNolearn;

	// Token: 0x04000826 RID: 2086
	public int cmxp;

	// Token: 0x04000827 RID: 2087
	public int cmvxp;

	// Token: 0x04000828 RID: 2088
	public int cmdxp;

	// Token: 0x04000829 RID: 2089
	public int cmxLimp;

	// Token: 0x0400082A RID: 2090
	public int cmyLimp;

	// Token: 0x0400082B RID: 2091
	public int cmyp;

	// Token: 0x0400082C RID: 2092
	public int cmvyp;

	// Token: 0x0400082D RID: 2093
	public int cmdyp;

	// Token: 0x0400082E RID: 2094
	private int indexTiemNang;

	// Token: 0x0400082F RID: 2095
	private string alertURL;

	// Token: 0x04000830 RID: 2096
	private string fnick;

	// Token: 0x04000831 RID: 2097
	public static int xstart;

	// Token: 0x04000832 RID: 2098
	public static int ystart;

	// Token: 0x04000833 RID: 2099
	public static int popupW = 140;

	// Token: 0x04000834 RID: 2100
	public static int popupH = 160;

	// Token: 0x04000835 RID: 2101
	public static int cmySK;

	// Token: 0x04000836 RID: 2102
	public static int cmtoYSK;

	// Token: 0x04000837 RID: 2103
	public static int cmdySK;

	// Token: 0x04000838 RID: 2104
	public static int cmvySK;

	// Token: 0x04000839 RID: 2105
	public static int cmyLimSK;

	// Token: 0x0400083A RID: 2106
	public static int columns = 6;

	// Token: 0x0400083B RID: 2107
	public static int rows;

	// Token: 0x0400083C RID: 2108
	private int totalRowInfo;

	// Token: 0x0400083D RID: 2109
	private int ypaintKill;

	// Token: 0x0400083E RID: 2110
	private int ylimUp;

	// Token: 0x0400083F RID: 2111
	private int ylimDow;

	// Token: 0x04000840 RID: 2112
	private int yPaint;

	// Token: 0x04000841 RID: 2113
	public static int indexEff = 0;

	// Token: 0x04000842 RID: 2114
	public static EffectCharPaint effUpok;

	// Token: 0x04000843 RID: 2115
	public static int inforX;

	// Token: 0x04000844 RID: 2116
	public static int inforY;

	// Token: 0x04000845 RID: 2117
	public static int inforW;

	// Token: 0x04000846 RID: 2118
	public static int inforH;

	// Token: 0x04000847 RID: 2119
	public Command cmdDead;

	// Token: 0x04000848 RID: 2120
	public static bool notPaint = false;

	// Token: 0x04000849 RID: 2121
	public static bool isPing = false;

	// Token: 0x0400084A RID: 2122
	public static int INFO = 0;

	// Token: 0x0400084B RID: 2123
	public static int STORE = 1;

	// Token: 0x0400084C RID: 2124
	public static int ZONE = 2;

	// Token: 0x0400084D RID: 2125
	public static int UPGRADE = 3;

	// Token: 0x0400084E RID: 2126
	private int Hitem = 30;

	// Token: 0x0400084F RID: 2127
	private int maxSizeRow = 5;

	// Token: 0x04000850 RID: 2128
	private int isTranKyNang;

	// Token: 0x04000851 RID: 2129
	private bool isTran;

	// Token: 0x04000852 RID: 2130
	private int cmY_Old;

	// Token: 0x04000853 RID: 2131
	private int cmX_Old;

	// Token: 0x04000854 RID: 2132
	public PopUpYesNo popUpYesNo;

	// Token: 0x04000855 RID: 2133
	public static MyVector vChatVip = new MyVector();

	// Token: 0x04000856 RID: 2134
	public static int vBig;

	// Token: 0x04000857 RID: 2135
	public bool isFireWorks;

	// Token: 0x04000858 RID: 2136
	public int[] winnumber;

	// Token: 0x04000859 RID: 2137
	public int[] randomNumber;

	// Token: 0x0400085A RID: 2138
	public int[] tMove;

	// Token: 0x0400085B RID: 2139
	public int[] moveCount;

	// Token: 0x0400085C RID: 2140
	public int[] delayMove;

	// Token: 0x0400085D RID: 2141
	public int moveIndex;

	// Token: 0x0400085E RID: 2142
	private bool isWin;

	// Token: 0x0400085F RID: 2143
	private string strFinish;

	// Token: 0x04000860 RID: 2144
	private int tShow;

	// Token: 0x04000861 RID: 2145
	private int xChatVip;

	// Token: 0x04000862 RID: 2146
	private int currChatWidth;

	// Token: 0x04000863 RID: 2147
	private bool startChat;

	// Token: 0x04000864 RID: 2148
	public sbyte percentMabu;

	// Token: 0x04000865 RID: 2149
	public bool mabuEff;

	// Token: 0x04000866 RID: 2150
	public int tMabuEff;

	// Token: 0x04000867 RID: 2151
	public static bool isPaintChatVip;

	// Token: 0x04000868 RID: 2152
	public static sbyte mabuPercent;

	// Token: 0x04000869 RID: 2153
	public static sbyte isNewMember;

	// Token: 0x0400086A RID: 2154
	private string yourNumber = string.Empty;

	// Token: 0x0400086B RID: 2155
	private string[] strPaint;

	// Token: 0x0400086C RID: 2156
	public static Image imgHP_NEW;

	// Token: 0x0400086D RID: 2157
	public static InfoPhuBan phuban_Info;

	// Token: 0x0400086E RID: 2158
	public static FrameImage fra_PVE_Bar_0;

	// Token: 0x0400086F RID: 2159
	public static FrameImage fra_PVE_Bar_1;

	// Token: 0x04000870 RID: 2160
	public static Image imgVS;

	// Token: 0x04000871 RID: 2161
	public static Image imgBall;

	// Token: 0x04000872 RID: 2162
	public static Image imgKhung;

	// Token: 0x04000873 RID: 2163
	public int countFrameSkill;

	// Token: 0x04000874 RID: 2164
	public static Image imgBgIOS;
}
