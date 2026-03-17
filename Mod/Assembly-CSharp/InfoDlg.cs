using System;

// Token: 0x02000046 RID: 70
public class InfoDlg
{
	// Token: 0x060003EE RID: 1006 RVA: 0x00056974 File Offset: 0x00054B74
	public static void show(string title, string subtitle, int delay)
	{
		bool flag = title == null;
		if (!flag)
		{
			InfoDlg.isShow = true;
			InfoDlg.title = title;
			InfoDlg.subtitke = subtitle;
			InfoDlg.delay = delay;
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000569A5 File Offset: 0x00054BA5
	public static void showWait()
	{
		InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
		InfoDlg.isLock = true;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x000569BF File Offset: 0x00054BBF
	public static void showWait(string str)
	{
		InfoDlg.show(str, null, 700);
		InfoDlg.isLock = true;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000569D8 File Offset: 0x00054BD8
	public static void paint(mGraphics g)
	{
		bool flag = !InfoDlg.isShow;
		if (!flag)
		{
			bool flag2 = InfoDlg.isLock && InfoDlg.delay > 4990;
			if (!flag2)
			{
				bool isPaintAlert = GameScr.isPaintAlert;
				if (!isPaintAlert)
				{
					int num = 10;
					GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
					bool flag3 = InfoDlg.isLock;
					if (flag3)
					{
						GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
						mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
					}
					else
					{
						bool flag4 = InfoDlg.subtitke != null;
						if (flag4)
						{
							mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
							mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
						}
						else
						{
							mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
						}
					}
				}
			}
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00056AFC File Offset: 0x00054CFC
	public static void update()
	{
		bool flag = InfoDlg.delay > 0;
		if (flag)
		{
			InfoDlg.delay--;
			bool flag2 = InfoDlg.delay == 0;
			if (flag2)
			{
				InfoDlg.hide();
			}
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00056B38 File Offset: 0x00054D38
	public static void hide()
	{
		InfoDlg.title = string.Empty;
		InfoDlg.subtitke = null;
		InfoDlg.isLock = false;
		InfoDlg.delay = 0;
		InfoDlg.isShow = false;
	}

	// Token: 0x040008BB RID: 2235
	public static bool isShow;

	// Token: 0x040008BC RID: 2236
	private static string title;

	// Token: 0x040008BD RID: 2237
	private static string subtitke;

	// Token: 0x040008BE RID: 2238
	public static int delay;

	// Token: 0x040008BF RID: 2239
	public static bool isLock;
}
