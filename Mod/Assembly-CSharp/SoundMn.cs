using System;

// Token: 0x020000A8 RID: 168
public class SoundMn
{
	// Token: 0x06000965 RID: 2405 RVA: 0x0009C48A File Offset: 0x0009A68A
	public static void init(SoundMn.AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0009C494 File Offset: 0x0009A694
	public static SoundMn gI()
	{
		bool flag = SoundMn.gIz == null;
		if (flag)
		{
			SoundMn.gIz = new SoundMn();
		}
		return SoundMn.gIz;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0009C4C4 File Offset: 0x0009A6C4
	public void loadSound(int mapID)
	{
		Sound.init(new int[]
		{
			SoundMn.AIR_SHIP,
			SoundMn.RAIN,
			SoundMn.TAITAONANGLUONG
		}, new int[]
		{
			SoundMn.GET_ITEM,
			SoundMn.MOVE,
			SoundMn.LOW_PUNCH,
			SoundMn.LOW_KICK,
			SoundMn.FLY,
			SoundMn.JUMP,
			SoundMn.PANEL_OPEN,
			SoundMn.BUTTON_CLOSE,
			SoundMn.BUTTON_CLICK,
			SoundMn.MEDIUM_PUNCH,
			SoundMn.MEDIUM_KICK,
			SoundMn.PANEL_OPEN,
			SoundMn.EAT_PEAN,
			SoundMn.OPEN_DIALOG,
			SoundMn.NORMAL_KAME,
			SoundMn.NAMEK_KAME,
			SoundMn.XAYDA_KAME,
			SoundMn.EXPLODE_1,
			SoundMn.EXPLODE_2,
			SoundMn.TRAIDAT_KAME,
			SoundMn.HP_UP,
			SoundMn.THAIDUONGHASAN,
			SoundMn.HOISINH,
			SoundMn.GONG,
			SoundMn.KHICHAY,
			SoundMn.BIG_EXPLODE,
			SoundMn.NAMEK_LAZER,
			SoundMn.NAMEK_CHARGE,
			SoundMn.RADAR_CLICK,
			SoundMn.RADAR_ITEM,
			SoundMn.FIREWORK,
			SoundMn.KAMEX10_0,
			SoundMn.KAMEX10_1,
			SoundMn.DESTROY_0,
			SoundMn.DESTROY_1,
			SoundMn.MAFUBA_0,
			SoundMn.MAFUBA_1,
			SoundMn.MAFUBA_2,
			SoundMn.DESTROY_2
		});
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0009C654 File Offset: 0x0009A854
	public void getSoundOption()
	{
		bool flag = GameCanvas.loginScr.isLogin2 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 2;
		if (flag)
		{
			Panel.strTool = new string[]
			{
				mResources.radaCard,
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account,
				mResources.REGISTOPROTECT
			};
			bool havePet = global::Char.myCharz().havePet;
			if (havePet)
			{
				Panel.strTool = new string[]
				{
					mResources.radaCard,
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account,
					mResources.REGISTOPROTECT
				};
			}
		}
		else
		{
			Panel.strTool = new string[]
			{
				mResources.radaCard,
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account
			};
			bool havePet2 = global::Char.myCharz().havePet;
			if (havePet2)
			{
				Panel.strTool = new string[]
				{
					mResources.radaCard,
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
			}
		}
		bool isDelAcc = SoundMn.IsDelAcc;
		if (isDelAcc)
		{
			string[] array = new string[Panel.strTool.Length + 1];
			for (int i = 0; i < Panel.strTool.Length; i++)
			{
				array[i] = Panel.strTool[i];
			}
			array[Panel.strTool.Length] = mResources.delacc;
			Panel.strTool = array;
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0009C894 File Offset: 0x0009AA94
	public void getStrOption()
	{
		string str = "[x]   ";
		string str2 = "[  ]   ";
		bool isPC = Main.isPC;
		if (isPC)
		{
			Panel.strCauhinh = new string[]
			{
				(!global::Char.isPaintAura) ? (str + mResources.aura_off) : (str2 + mResources.aura_off),
				(!global::Char.isPaintAura2) ? (str + mResources.aura_off_2) : (str2 + mResources.aura_off_2),
				(!GameCanvas.isPlaySound) ? (str2 + mResources.turnOffSound) : (str + mResources.turnOffSound),
				(mGraphics.zoomLevel <= 1) ? (str2 + mResources.x2Screen) : (str + mResources.x1Screen)
			};
		}
		else
		{
			string text = (GameScr.isAnalog != 0) ? (str + mResources.turnOffAnalog) : (str2 + mResources.turnOnAnalog);
			bool flag = !GameCanvas.isTouch;
			if (flag)
			{
				text = (GameScr.isPaintChatVip ? (str + mResources.serverchat_off) : (str2 + mResources.serverchat_off));
			}
			Panel.strCauhinh = new string[]
			{
				(!global::Char.isPaintAura) ? (str + mResources.aura_off) : (str2 + mResources.aura_off),
				(!global::Char.isPaintAura2) ? (str + mResources.aura_off_2) : (str2 + mResources.aura_off_2),
				(!GameCanvas.isPlaySound) ? (str2 + mResources.turnOffSound) : (str + mResources.turnOffSound),
				(!GameCanvas.lowGraphic) ? (str2 + mResources.cauhinhthap) : (str + mResources.cauhinhthap),
				text
			};
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0009CA3E File Offset: 0x0009AC3E
	public void HP_MPup()
	{
		Sound.playSound(SoundMn.HP_UP, 0.5f);
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0009CA54 File Offset: 0x0009AC54
	public void charPunch(bool isKick, float volumn)
	{
		bool flag = !global::Char.myCharz().me;
		if (flag)
		{
			SoundMn.volume /= 2f;
		}
		bool flag2 = volumn <= 0f;
		if (flag2)
		{
			volumn = 0.01f;
		}
		int num = Res.random(0, 3);
		if (isKick)
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_KICK : SoundMn.LOW_KICK, 0.1f);
		}
		else
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_PUNCH : SoundMn.LOW_PUNCH, 0.1f);
		}
		this.poolCount++;
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0009CAF2 File Offset: 0x0009ACF2
	public void thaiduonghasan()
	{
		Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0009CB13 File Offset: 0x0009AD13
	public void rain()
	{
		Sound.playMus(SoundMn.RAIN, 0.3f, true);
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0009CB27 File Offset: 0x0009AD27
	public void gongName()
	{
		Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
		this.poolCount++;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0009CB48 File Offset: 0x0009AD48
	public void gong()
	{
		Sound.playSound(SoundMn.GONG, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0009CB69 File Offset: 0x0009AD69
	public void getItem()
	{
		Sound.playSound(SoundMn.GET_ITEM, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0009CB8C File Offset: 0x0009AD8C
	public void soundToolOption()
	{
		GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			SoundMn.gI().loadSound(TileMap.mapID);
			Rms.saveRMSInt("isPlaySound", 1);
		}
		else
		{
			SoundMn.gI().closeSound();
			Rms.saveRMSInt("isPlaySound", 0);
		}
		this.getStrOption();
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0009CBF0 File Offset: 0x0009ADF0
	public void chatVipToolOption()
	{
		GameScr.isPaintChatVip = !GameScr.isPaintChatVip;
		bool isPaintChatVip = GameScr.isPaintChatVip;
		if (isPaintChatVip)
		{
			Rms.saveRMSInt("serverchat", 0);
		}
		else
		{
			Rms.saveRMSInt("serverchat", 1);
		}
		this.getStrOption();
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0009CC3C File Offset: 0x0009AE3C
	public void analogToolOption()
	{
		bool flag = GameScr.isAnalog == 0;
		if (flag)
		{
			GameScr.isAnalog = 1;
			Rms.saveRMSInt("analog", GameScr.isAnalog);
			GameScr.setSkillBarPosition();
		}
		else
		{
			GameScr.isAnalog = 0;
			Rms.saveRMSInt("analog", GameScr.isAnalog);
			GameScr.setSkillBarPosition();
		}
		this.getStrOption();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0009CC9C File Offset: 0x0009AE9C
	public void CaseAnalog()
	{
		bool flag = !Main.isPC;
		if (flag)
		{
			bool flag2 = !GameCanvas.isTouch;
			if (flag2)
			{
				this.chatVipToolOption();
			}
			else
			{
				this.analogToolOption();
			}
		}
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0009CCD8 File Offset: 0x0009AED8
	public void CaseSizeScr()
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (lowGraphic)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		else
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		this.getStrOption();
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0009CD38 File Offset: 0x0009AF38
	public void AuraToolOption()
	{
		bool isPaintAura = global::Char.isPaintAura;
		if (isPaintAura)
		{
			Rms.saveRMSInt("isPaintAura", 0);
			global::Char.isPaintAura = false;
		}
		else
		{
			Rms.saveRMSInt("isPaintAura", 1);
			global::Char.isPaintAura = true;
		}
		this.getStrOption();
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0009CD80 File Offset: 0x0009AF80
	public void AuraToolOption2()
	{
		bool isPaintAura = global::Char.isPaintAura2;
		if (isPaintAura)
		{
			Rms.saveRMSInt("isPaintAura2", 0);
			global::Char.isPaintAura2 = false;
		}
		else
		{
			Rms.saveRMSInt("isPaintAura2", 1);
			global::Char.isPaintAura2 = true;
		}
		this.getStrOption();
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0009CDC8 File Offset: 0x0009AFC8
	public void HatToolOption()
	{
		Service.gI().sendOptHat(0);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00003136 File Offset: 0x00001336
	public void update()
	{
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0009CDD7 File Offset: 0x0009AFD7
	public void closeSound()
	{
		Sound.stopAll = true;
		this.stopAll();
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0009CDE8 File Offset: 0x0009AFE8
	public void openSound()
	{
		bool flag = Sound.music == null;
		if (flag)
		{
			this.loadSound(0);
		}
		Sound.stopAll = false;
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0009CE12 File Offset: 0x0009B012
	public void bigeExlode()
	{
		Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0009CE33 File Offset: 0x0009B033
	public void explode_1()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0009CE33 File Offset: 0x0009B033
	public void explode_2()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0009CE54 File Offset: 0x0009B054
	public void traidatKame()
	{
		Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
		this.poolCount++;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0009CE75 File Offset: 0x0009B075
	public void namekKame()
	{
		Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0009CE96 File Offset: 0x0009B096
	public void nameLazer()
	{
		Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0009CEB7 File Offset: 0x0009B0B7
	public void xaydaKame()
	{
		Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0009CED8 File Offset: 0x0009B0D8
	public void mobKame(int type)
	{
		int id = SoundMn.XAYDA_KAME;
		bool flag = type == 13;
		if (flag)
		{
			id = SoundMn.NORMAL_KAME;
		}
		Sound.playSound(id, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0009CF18 File Offset: 0x0009B118
	public void charRun(float volumn)
	{
		bool flag = !global::Char.myCharz().me;
		if (flag)
		{
			SoundMn.volume /= 2f;
			bool flag2 = volumn <= 0f;
			if (flag2)
			{
				volumn = 0.01f;
			}
		}
		bool flag3 = GameCanvas.gameTick % 8 == 0;
		if (flag3)
		{
			Sound.playSound(SoundMn.MOVE, volumn);
			this.poolCount++;
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0009CF8C File Offset: 0x0009B18C
	public void monkeyRun(float volumn)
	{
		bool flag = GameCanvas.gameTick % 8 == 0;
		if (flag)
		{
			Sound.playSound(SoundMn.KHICHAY, 0.2f);
			this.poolCount++;
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0009CFC8 File Offset: 0x0009B1C8
	public void charFall()
	{
		Sound.playSound(SoundMn.MOVE, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0009CFE9 File Offset: 0x0009B1E9
	public void charJump()
	{
		Sound.playSound(SoundMn.MOVE, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0009D00A File Offset: 0x0009B20A
	public void panelOpen()
	{
		Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0009D02B File Offset: 0x0009B22B
	public void buttonClose()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0009D04C File Offset: 0x0009B24C
	public void buttonClick()
	{
		Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00003136 File Offset: 0x00001336
	public void stopMove()
	{
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0009D06D File Offset: 0x0009B26D
	public void charFly()
	{
		Sound.playSound(SoundMn.FLY, 0.2f);
		this.poolCount++;
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00003136 File Offset: 0x00001336
	public void stopFly()
	{
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0009D02B File Offset: 0x0009B22B
	public void openMenu()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0009D08E File Offset: 0x0009B28E
	public void panelClick()
	{
		Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0009D0AF File Offset: 0x0009B2AF
	public void eatPeans()
	{
		Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0009D0D0 File Offset: 0x0009B2D0
	public void openDialog()
	{
		Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0009D0E3 File Offset: 0x0009B2E3
	public void hoisinh()
	{
		Sound.playSound(SoundMn.HOISINH, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0009D104 File Offset: 0x0009B304
	public void taitao()
	{
		Sound.playMus(SoundMn.TAITAONANGLUONG, 0.5f, true);
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00003136 File Offset: 0x00001336
	public void taitaoPause()
	{
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0009D118 File Offset: 0x0009B318
	public bool isPlayRain()
	{
		bool result;
		try
		{
			result = Sound.isPlayingSound();
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0009D14C File Offset: 0x0009B34C
	public bool isPlayAirShip()
	{
		return false;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0009D160 File Offset: 0x0009B360
	public void airShip()
	{
		SoundMn.cout++;
		bool flag = SoundMn.cout % 2 == 0;
		if (flag)
		{
			Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
		}
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00003136 File Offset: 0x00001336
	public void pauseAirShip()
	{
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x00003136 File Offset: 0x00001336
	public void resumeAirShip()
	{
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0009D19B File Offset: 0x0009B39B
	public void stopAll()
	{
		Sound.stopAllz();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0009D1A4 File Offset: 0x0009B3A4
	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0009D1D4 File Offset: 0x0009B3D4
	public void newKame()
	{
		this.poolCount++;
		bool flag = this.poolCount % 15 == 0;
		if (flag)
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
		}
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0009D212 File Offset: 0x0009B412
	public void radarClick()
	{
		Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0009D225 File Offset: 0x0009B425
	public void radarItem()
	{
		Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0009D238 File Offset: 0x0009B438
	public static void playSound(int x, int y, int id, float volume)
	{
		Sound.playSound(id, volume);
	}

	// Token: 0x040011B6 RID: 4534
	public static bool IsDelAcc;

	// Token: 0x040011B7 RID: 4535
	public static SoundMn gIz;

	// Token: 0x040011B8 RID: 4536
	public static bool isSound = true;

	// Token: 0x040011B9 RID: 4537
	public static float volume = 0.5f;

	// Token: 0x040011BA RID: 4538
	private static int MAX_VOLUME = 10;

	// Token: 0x040011BB RID: 4539
	public static SoundMn.MediaPlayer[] music;

	// Token: 0x040011BC RID: 4540
	public static SoundMn.SoundPool[] sound;

	// Token: 0x040011BD RID: 4541
	public static int[] soundID;

	// Token: 0x040011BE RID: 4542
	public static int AIR_SHIP;

	// Token: 0x040011BF RID: 4543
	public static int RAIN = 1;

	// Token: 0x040011C0 RID: 4544
	public static int TAITAONANGLUONG = 2;

	// Token: 0x040011C1 RID: 4545
	public static int GET_ITEM;

	// Token: 0x040011C2 RID: 4546
	public static int MOVE = 1;

	// Token: 0x040011C3 RID: 4547
	public static int LOW_PUNCH = 2;

	// Token: 0x040011C4 RID: 4548
	public static int LOW_KICK = 3;

	// Token: 0x040011C5 RID: 4549
	public static int FLY = 4;

	// Token: 0x040011C6 RID: 4550
	public static int JUMP = 5;

	// Token: 0x040011C7 RID: 4551
	public static int PANEL_OPEN = 6;

	// Token: 0x040011C8 RID: 4552
	public static int BUTTON_CLOSE = 7;

	// Token: 0x040011C9 RID: 4553
	public static int BUTTON_CLICK = 8;

	// Token: 0x040011CA RID: 4554
	public static int MEDIUM_PUNCH = 9;

	// Token: 0x040011CB RID: 4555
	public static int MEDIUM_KICK = 10;

	// Token: 0x040011CC RID: 4556
	public static int PANEL_CLICK = 11;

	// Token: 0x040011CD RID: 4557
	public static int EAT_PEAN = 12;

	// Token: 0x040011CE RID: 4558
	public static int OPEN_DIALOG = 13;

	// Token: 0x040011CF RID: 4559
	public static int NORMAL_KAME = 14;

	// Token: 0x040011D0 RID: 4560
	public static int NAMEK_KAME = 15;

	// Token: 0x040011D1 RID: 4561
	public static int XAYDA_KAME = 16;

	// Token: 0x040011D2 RID: 4562
	public static int EXPLODE_1 = 17;

	// Token: 0x040011D3 RID: 4563
	public static int EXPLODE_2 = 18;

	// Token: 0x040011D4 RID: 4564
	public static int TRAIDAT_KAME = 19;

	// Token: 0x040011D5 RID: 4565
	public static int HP_UP = 20;

	// Token: 0x040011D6 RID: 4566
	public static int THAIDUONGHASAN = 21;

	// Token: 0x040011D7 RID: 4567
	public static int HOISINH = 22;

	// Token: 0x040011D8 RID: 4568
	public static int GONG = 23;

	// Token: 0x040011D9 RID: 4569
	public static int KHICHAY = 24;

	// Token: 0x040011DA RID: 4570
	public static int BIG_EXPLODE = 25;

	// Token: 0x040011DB RID: 4571
	public static int NAMEK_LAZER = 26;

	// Token: 0x040011DC RID: 4572
	public static int NAMEK_CHARGE = 27;

	// Token: 0x040011DD RID: 4573
	public static int RADAR_CLICK = 28;

	// Token: 0x040011DE RID: 4574
	public static int RADAR_ITEM = 29;

	// Token: 0x040011DF RID: 4575
	public static int FIREWORK = 30;

	// Token: 0x040011E0 RID: 4576
	public static int KAMEX10_0 = 31;

	// Token: 0x040011E1 RID: 4577
	public static int KAMEX10_1 = 32;

	// Token: 0x040011E2 RID: 4578
	public static int DESTROY_0 = 33;

	// Token: 0x040011E3 RID: 4579
	public static int DESTROY_1 = 34;

	// Token: 0x040011E4 RID: 4580
	public static int MAFUBA_0 = 35;

	// Token: 0x040011E5 RID: 4581
	public static int MAFUBA_1 = 36;

	// Token: 0x040011E6 RID: 4582
	public static int MAFUBA_2 = 37;

	// Token: 0x040011E7 RID: 4583
	public static int DESTROY_2 = 38;

	// Token: 0x040011E8 RID: 4584
	public bool freePool;

	// Token: 0x040011E9 RID: 4585
	public int poolCount;

	// Token: 0x040011EA RID: 4586
	public static int cout = 1;

	// Token: 0x020000D0 RID: 208
	public class MediaPlayer
	{
	}

	// Token: 0x020000D1 RID: 209
	public class SoundPool
	{
	}

	// Token: 0x020000D2 RID: 210
	public class AssetManager
	{
	}
}
