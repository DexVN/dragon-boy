using System;

// Token: 0x020000A9 RID: 169
public class SplashScr : mScreen
{
	// Token: 0x060009A2 RID: 2466 RVA: 0x0009D37C File Offset: 0x0009B57C
	public SplashScr()
	{
		SplashScr.instance = this;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0009D394 File Offset: 0x0009B594
	public static void loadSplashScr()
	{
		SplashScr.splashScrStat = 0;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0009D3A0 File Offset: 0x0009B5A0
	public override void update()
	{
		bool flag = SplashScr.splashScrStat == 30 && !this.isCheckConnect;
		if (flag)
		{
			this.isCheckConnect = true;
			bool flag2 = Rms.loadRMSInt("serverchat") != -1;
			if (flag2)
			{
				GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") == 0);
			}
			bool flag3 = Rms.loadRMSInt("isPlaySound") != -1;
			if (flag3)
			{
				GameCanvas.isPlaySound = (Rms.loadRMSInt("isPlaySound") == 1);
			}
			bool isPlaySound = GameCanvas.isPlaySound;
			if (isPlaySound)
			{
				SoundMn.gI().loadSound(TileMap.mapID);
			}
			SoundMn.gI().getStrOption();
			bool flag4 = Rms.loadRMSInt("svselect") == -1;
			if (flag4)
			{
				string linkDefault = ServerListScreen.linkDefault;
				string[] array = Res.split(linkDefault.Trim(), ",", 0);
				mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
				ServerListScreen.nameServer = new string[array.Length - 2];
				ServerListScreen.address = new string[array.Length - 2];
				ServerListScreen.port = new short[array.Length - 2];
				ServerListScreen.language = new sbyte[array.Length - 2];
				ServerListScreen.hasConnected = new bool[2];
				for (int i = 0; i < array.Length - 2; i++)
				{
					string[] array2 = Res.split(array[i].Trim(), ":", 0);
					ServerListScreen.nameServer[i] = array2[0];
					ServerListScreen.address[i] = array2[1];
					ServerListScreen.port[i] = short.Parse(array2[2]);
					ServerListScreen.language[i] = sbyte.Parse(array2[3].Trim());
				}
				GameCanvas.serverScr.switchToMe();
			}
			else
			{
				ServerListScreen.loadIP();
			}
		}
		SplashScr.splashScrStat++;
		ServerListScreen.updateDeleteData();
		bool flag5 = SplashScr.splashScrStat >= 150;
		if (flag5)
		{
			Res.outz("cho man hinh nay qa lau");
			bool flag6 = Session_ME.gI().isConnected();
			if (flag6)
			{
				ServerListScreen.loadScreen = true;
				GameCanvas.serverScreen.switchToMe();
			}
			else
			{
				mSystem.onDisconnected();
			}
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0009D5C4 File Offset: 0x0009B7C4
	public static void loadIP()
	{
		bool flag = Rms.loadRMSInt("svselect") == -1;
		if (flag)
		{
			int num = 0;
			bool flag2 = mResources.language > 0;
			if (flag2)
			{
				for (int i = 0; i < (int)mResources.language; i++)
				{
					num += ServerListScreen.lengthServer[i];
				}
			}
			bool flag3 = ServerListScreen.serverPriority == -1;
			if (flag3)
			{
				ServerListScreen.ipSelect = num + Res.random(0, ServerListScreen.lengthServer[(int)mResources.language]);
			}
			else
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
			}
			Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}
		else
		{
			ServerListScreen.ipSelect = Rms.loadRMSInt("svselect");
			bool flag4 = ServerListScreen.ipSelect > ServerListScreen.nameServer.Length - 1;
			if (flag4)
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			}
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0009D738 File Offset: 0x0009B938
	public override void paint(mGraphics g)
	{
		bool flag = SplashScr.imgLogo != null && SplashScr.splashScrStat < 30;
		if (flag)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(SplashScr.imgLogo, GameCanvas.w / 2, GameCanvas.h / 2, 3);
		}
		bool flag2 = SplashScr.nData != -1;
		if (flag2)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + (SplashScr.nData * 100 / SplashScr.maxData).ToString() + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
		}
		else
		{
			bool flag3 = SplashScr.splashScrStat >= 30;
			if (flag3)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
				bool flag4 = ServerListScreen.cmdDeleteRMS != null;
				if (flag4)
				{
					mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
				}
			}
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0009D8B6 File Offset: 0x0009BAB6
	public static void loadImg()
	{
		SplashScr.imgLogo = GameCanvas.loadImage("/gamelogo.png");
	}

	// Token: 0x040011EB RID: 4587
	public static int splashScrStat;

	// Token: 0x040011EC RID: 4588
	private bool isCheckConnect;

	// Token: 0x040011ED RID: 4589
	private bool isSwitchToLogin;

	// Token: 0x040011EE RID: 4590
	public static int nData = -1;

	// Token: 0x040011EF RID: 4591
	public static int maxData = -1;

	// Token: 0x040011F0 RID: 4592
	public static SplashScr instance;

	// Token: 0x040011F1 RID: 4593
	public static Image imgLogo;

	// Token: 0x040011F2 RID: 4594
	private int timeLoading = 10;

	// Token: 0x040011F3 RID: 4595
	public long TIMEOUT;
}
