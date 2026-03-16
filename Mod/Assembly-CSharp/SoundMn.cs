using System;

// Token: 0x02000089 RID: 137
public class SoundMn
{
	// Token: 0x06000469 RID: 1129 RVA: 0x000355E5 File Offset: 0x000339E5
	public static void init(SoundMn.AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x000355ED File Offset: 0x000339ED
	public static SoundMn gI()
	{
		if (SoundMn.gIz == null)
		{
			SoundMn.gIz = new SoundMn();
		}
		return SoundMn.gIz;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00035608 File Offset: 0x00033A08
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

	// Token: 0x0600046C RID: 1132 RVA: 0x00035798 File Offset: 0x00033B98
	public void getSoundOption()
	{
		if (GameCanvas.loginScr.isLogin2 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 2)
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
			if (global::Char.myCharz().havePet)
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
			if (global::Char.myCharz().havePet)
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
		if (SoundMn.IsDelAcc)
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

	// Token: 0x0600046D RID: 1133 RVA: 0x000359C0 File Offset: 0x00033DC0
	public void getStrOption()
	{
		string str = "[x]   ";
		string str2 = "[  ]   ";
		if (Main.isPC)
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
			if (!GameCanvas.isTouch)
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

	// Token: 0x0600046E RID: 1134 RVA: 0x00035B99 File Offset: 0x00033F99
	public void HP_MPup()
	{
		Sound.playSound(SoundMn.HP_UP, 0.5f);
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00035BAC File Offset: 0x00033FAC
	public void charPunch(bool isKick, float volumn)
	{
		if (!global::Char.myCharz().me)
		{
			SoundMn.volume /= 2f;
		}
		if (volumn <= 0f)
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

	// Token: 0x06000470 RID: 1136 RVA: 0x00035C49 File Offset: 0x00034049
	public void thaiduonghasan()
	{
		Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00035C68 File Offset: 0x00034068
	public void rain()
	{
		Sound.playMus(SoundMn.RAIN, 0.3f, true);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00035C7A File Offset: 0x0003407A
	public void gongName()
	{
		Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00035C99 File Offset: 0x00034099
	public void gong()
	{
		Sound.playSound(SoundMn.GONG, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00035CB8 File Offset: 0x000340B8
	public void getItem()
	{
		Sound.playSound(SoundMn.GET_ITEM, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00035CD8 File Offset: 0x000340D8
	public void soundToolOption()
	{
		GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
		if (GameCanvas.isPlaySound)
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

	// Token: 0x06000476 RID: 1142 RVA: 0x00035D36 File Offset: 0x00034136
	public void chatVipToolOption()
	{
		GameScr.isPaintChatVip = !GameScr.isPaintChatVip;
		if (GameScr.isPaintChatVip)
		{
			Rms.saveRMSInt("serverchat", 0);
		}
		else
		{
			Rms.saveRMSInt("serverchat", 1);
		}
		this.getStrOption();
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00035D70 File Offset: 0x00034170
	public void analogToolOption()
	{
		if (GameScr.isAnalog == 0)
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

	// Token: 0x06000478 RID: 1144 RVA: 0x00035DC6 File Offset: 0x000341C6
	public void CaseAnalog()
	{
		if (!Main.isPC)
		{
			if (!GameCanvas.isTouch)
			{
				this.chatVipToolOption();
			}
			else
			{
				this.analogToolOption();
			}
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00035DF4 File Offset: 0x000341F4
	public void CaseSizeScr()
	{
		if (GameCanvas.lowGraphic)
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

	// Token: 0x0600047A RID: 1146 RVA: 0x00035E4C File Offset: 0x0003424C
	public void AuraToolOption()
	{
		if (global::Char.isPaintAura)
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

	// Token: 0x0600047B RID: 1147 RVA: 0x00035E85 File Offset: 0x00034285
	public void AuraToolOption2()
	{
		if (global::Char.isPaintAura2)
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

	// Token: 0x0600047C RID: 1148 RVA: 0x00035EBE File Offset: 0x000342BE
	public void HatToolOption()
	{
		Service.gI().sendOptHat(0);
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00035ECB File Offset: 0x000342CB
	public void update()
	{
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00035ECD File Offset: 0x000342CD
	public void closeSound()
	{
		Sound.stopAll = true;
		this.stopAll();
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00035EDB File Offset: 0x000342DB
	public void openSound()
	{
		if (Sound.music == null)
		{
			this.loadSound(0);
		}
		Sound.stopAll = false;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00035EF4 File Offset: 0x000342F4
	public void bigeExlode()
	{
		Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00035F13 File Offset: 0x00034313
	public void explode_1()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00035F32 File Offset: 0x00034332
	public void explode_2()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00035F51 File Offset: 0x00034351
	public void traidatKame()
	{
		Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
		this.poolCount++;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00035F70 File Offset: 0x00034370
	public void namekKame()
	{
		Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00035F8F File Offset: 0x0003438F
	public void nameLazer()
	{
		Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00035FAE File Offset: 0x000343AE
	public void xaydaKame()
	{
		Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00035FD0 File Offset: 0x000343D0
	public void mobKame(int type)
	{
		int id = SoundMn.XAYDA_KAME;
		if (type == 13)
		{
			id = SoundMn.NORMAL_KAME;
		}
		Sound.playSound(id, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0003600C File Offset: 0x0003440C
	public void charRun(float volumn)
	{
		if (!global::Char.myCharz().me)
		{
			SoundMn.volume /= 2f;
			if (volumn <= 0f)
			{
				volumn = 0.01f;
			}
		}
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.MOVE, volumn);
			this.poolCount++;
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0003606F File Offset: 0x0003446F
	public void monkeyRun(float volumn)
	{
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.KHICHAY, 0.2f);
			this.poolCount++;
		}
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0003609A File Offset: 0x0003449A
	public void charFall()
	{
		Sound.playSound(SoundMn.MOVE, 0.1f);
		this.poolCount++;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x000360B9 File Offset: 0x000344B9
	public void charJump()
	{
		Sound.playSound(SoundMn.MOVE, 0.2f);
		this.poolCount++;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x000360D8 File Offset: 0x000344D8
	public void panelOpen()
	{
		Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x000360F7 File Offset: 0x000344F7
	public void buttonClose()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00036116 File Offset: 0x00034516
	public void buttonClick()
	{
		Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00036135 File Offset: 0x00034535
	public void stopMove()
	{
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00036137 File Offset: 0x00034537
	public void charFly()
	{
		Sound.playSound(SoundMn.FLY, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00036156 File Offset: 0x00034556
	public void stopFly()
	{
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00036158 File Offset: 0x00034558
	public void openMenu()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00036177 File Offset: 0x00034577
	public void panelClick()
	{
		Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00036196 File Offset: 0x00034596
	public void eatPeans()
	{
		Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x000361B5 File Offset: 0x000345B5
	public void openDialog()
	{
		Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x000361C6 File Offset: 0x000345C6
	public void hoisinh()
	{
		Sound.playSound(SoundMn.HOISINH, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x000361E5 File Offset: 0x000345E5
	public void taitao()
	{
		Sound.playMus(SoundMn.TAITAONANGLUONG, 0.5f, true);
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x000361F7 File Offset: 0x000345F7
	public void taitaoPause()
	{
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x000361FC File Offset: 0x000345FC
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

	// Token: 0x0600049A RID: 1178 RVA: 0x00036230 File Offset: 0x00034630
	public bool isPlayAirShip()
	{
		return false;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00036233 File Offset: 0x00034633
	public void airShip()
	{
		SoundMn.cout++;
		if (SoundMn.cout % 2 == 0)
		{
			Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0003625D File Offset: 0x0003465D
	public void pauseAirShip()
	{
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0003625F File Offset: 0x0003465F
	public void resumeAirShip()
	{
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00036261 File Offset: 0x00034661
	public void stopAll()
	{
		Sound.stopAllz();
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00036268 File Offset: 0x00034668
	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00036292 File Offset: 0x00034692
	public void newKame()
	{
		this.poolCount++;
		if (this.poolCount % 15 == 0)
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x000362BF File Offset: 0x000346BF
	public void radarClick()
	{
		Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000362D0 File Offset: 0x000346D0
	public void radarItem()
	{
		Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x000362E1 File Offset: 0x000346E1
	public static void playSound(int x, int y, int id, float volume)
	{
		Sound.playSound(id, volume);
	}

	// Token: 0x040007C3 RID: 1987
	public static bool IsDelAcc;

	// Token: 0x040007C4 RID: 1988
	public static SoundMn gIz;

	// Token: 0x040007C5 RID: 1989
	public static bool isSound = true;

	// Token: 0x040007C6 RID: 1990
	public static float volume = 0.5f;

	// Token: 0x040007C7 RID: 1991
	private static int MAX_VOLUME = 10;

	// Token: 0x040007C8 RID: 1992
	public static SoundMn.MediaPlayer[] music;

	// Token: 0x040007C9 RID: 1993
	public static SoundMn.SoundPool[] sound;

	// Token: 0x040007CA RID: 1994
	public static int[] soundID;

	// Token: 0x040007CB RID: 1995
	public static int AIR_SHIP;

	// Token: 0x040007CC RID: 1996
	public static int RAIN = 1;

	// Token: 0x040007CD RID: 1997
	public static int TAITAONANGLUONG = 2;

	// Token: 0x040007CE RID: 1998
	public static int GET_ITEM;

	// Token: 0x040007CF RID: 1999
	public static int MOVE = 1;

	// Token: 0x040007D0 RID: 2000
	public static int LOW_PUNCH = 2;

	// Token: 0x040007D1 RID: 2001
	public static int LOW_KICK = 3;

	// Token: 0x040007D2 RID: 2002
	public static int FLY = 4;

	// Token: 0x040007D3 RID: 2003
	public static int JUMP = 5;

	// Token: 0x040007D4 RID: 2004
	public static int PANEL_OPEN = 6;

	// Token: 0x040007D5 RID: 2005
	public static int BUTTON_CLOSE = 7;

	// Token: 0x040007D6 RID: 2006
	public static int BUTTON_CLICK = 8;

	// Token: 0x040007D7 RID: 2007
	public static int MEDIUM_PUNCH = 9;

	// Token: 0x040007D8 RID: 2008
	public static int MEDIUM_KICK = 10;

	// Token: 0x040007D9 RID: 2009
	public static int PANEL_CLICK = 11;

	// Token: 0x040007DA RID: 2010
	public static int EAT_PEAN = 12;

	// Token: 0x040007DB RID: 2011
	public static int OPEN_DIALOG = 13;

	// Token: 0x040007DC RID: 2012
	public static int NORMAL_KAME = 14;

	// Token: 0x040007DD RID: 2013
	public static int NAMEK_KAME = 15;

	// Token: 0x040007DE RID: 2014
	public static int XAYDA_KAME = 16;

	// Token: 0x040007DF RID: 2015
	public static int EXPLODE_1 = 17;

	// Token: 0x040007E0 RID: 2016
	public static int EXPLODE_2 = 18;

	// Token: 0x040007E1 RID: 2017
	public static int TRAIDAT_KAME = 19;

	// Token: 0x040007E2 RID: 2018
	public static int HP_UP = 20;

	// Token: 0x040007E3 RID: 2019
	public static int THAIDUONGHASAN = 21;

	// Token: 0x040007E4 RID: 2020
	public static int HOISINH = 22;

	// Token: 0x040007E5 RID: 2021
	public static int GONG = 23;

	// Token: 0x040007E6 RID: 2022
	public static int KHICHAY = 24;

	// Token: 0x040007E7 RID: 2023
	public static int BIG_EXPLODE = 25;

	// Token: 0x040007E8 RID: 2024
	public static int NAMEK_LAZER = 26;

	// Token: 0x040007E9 RID: 2025
	public static int NAMEK_CHARGE = 27;

	// Token: 0x040007EA RID: 2026
	public static int RADAR_CLICK = 28;

	// Token: 0x040007EB RID: 2027
	public static int RADAR_ITEM = 29;

	// Token: 0x040007EC RID: 2028
	public static int FIREWORK = 30;

	// Token: 0x040007ED RID: 2029
	public static int KAMEX10_0 = 31;

	// Token: 0x040007EE RID: 2030
	public static int KAMEX10_1 = 32;

	// Token: 0x040007EF RID: 2031
	public static int DESTROY_0 = 33;

	// Token: 0x040007F0 RID: 2032
	public static int DESTROY_1 = 34;

	// Token: 0x040007F1 RID: 2033
	public static int MAFUBA_0 = 35;

	// Token: 0x040007F2 RID: 2034
	public static int MAFUBA_1 = 36;

	// Token: 0x040007F3 RID: 2035
	public static int MAFUBA_2 = 37;

	// Token: 0x040007F4 RID: 2036
	public static int DESTROY_2 = 38;

	// Token: 0x040007F5 RID: 2037
	public bool freePool;

	// Token: 0x040007F6 RID: 2038
	public int poolCount;

	// Token: 0x040007F7 RID: 2039
	public static int cout = 1;

	// Token: 0x0200008A RID: 138
	public class MediaPlayer
	{
	}

	// Token: 0x0200008B RID: 139
	public class SoundPool
	{
	}

	// Token: 0x0200008C RID: 140
	public class AssetManager
	{
	}
}
