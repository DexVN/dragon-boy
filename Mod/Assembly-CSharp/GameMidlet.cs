using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class GameMidlet
{
	// Token: 0x060002E2 RID: 738 RVA: 0x000428A0 File Offset: 0x00040AA0
	public GameMidlet()
	{
		this.initGame();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x000428B4 File Offset: 0x00040AB4
	public void initGame()
	{
		GameMidlet.instance = this;
		MotherCanvas.instance = new MotherCanvas();
		Session_ME.gI().setHandler(Controller.gI());
		Session_ME2.gI().setHandler(Controller.gI());
		Session_ME2.isMainSession = false;
		GameMidlet.instance = this;
		GameMidlet.gameCanvas = new GameCanvas();
		GameMidlet.gameCanvas.start();
		SplashScr.loadImg();
		SplashScr.loadSplashScr();
		GameCanvas.currentScreen = new SplashScr();
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0004292C File Offset: 0x00040B2C
	public void exit()
	{
		bool flag = Main.typeClient == 6;
		if (flag)
		{
			mSystem.exitWP();
		}
		else
		{
			GameCanvas.bRun = false;
			mSystem.gcc();
			this.notifyDestroyed();
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00042965 File Offset: 0x00040B65
	public static void sendSMS(string data, string to, Command successAction, Command failAction)
	{
		Cout.println("SEND SMS");
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00042973 File Offset: 0x00040B73
	public static void flatForm(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0004298E File Offset: 0x00040B8E
	public void notifyDestroyed()
	{
		Main.exit();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00042997 File Offset: 0x00040B97
	public void platformRequest(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x04000682 RID: 1666
	public static string IP = "112.213.94.23";

	// Token: 0x04000683 RID: 1667
	public static int PORT = 14445;

	// Token: 0x04000684 RID: 1668
	public static string IP2;

	// Token: 0x04000685 RID: 1669
	public static int PORT2;

	// Token: 0x04000686 RID: 1670
	public static sbyte PROVIDER;

	// Token: 0x04000687 RID: 1671
	public static int LANGUAGE;

	// Token: 0x04000688 RID: 1672
	public static string VERSION = "2.3.7";

	// Token: 0x04000689 RID: 1673
	public static int intVERSION = 237;

	// Token: 0x0400068A RID: 1674
	public static GameCanvas gameCanvas;

	// Token: 0x0400068B RID: 1675
	public static GameMidlet instance;

	// Token: 0x0400068C RID: 1676
	public static bool isConnect2;

	// Token: 0x0400068D RID: 1677
	public static bool isBackWindowsPhone;
}
