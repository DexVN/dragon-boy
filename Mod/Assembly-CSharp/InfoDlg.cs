using System;

// Token: 0x020000AD RID: 173
public class InfoDlg
{
	// Token: 0x060007C2 RID: 1986 RVA: 0x00070652 File Offset: 0x0006EA52
	public static void show(string title, string subtitle, int delay)
	{
		if (title == null)
		{
			return;
		}
		InfoDlg.isShow = true;
		InfoDlg.title = title;
		InfoDlg.subtitke = subtitle;
		InfoDlg.delay = delay;
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00070673 File Offset: 0x0006EA73
	public static void showWait()
	{
		InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
		InfoDlg.isLock = true;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0007068B File Offset: 0x0006EA8B
	public static void showWait(string str)
	{
		InfoDlg.show(str, null, 700);
		InfoDlg.isLock = true;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x000706A0 File Offset: 0x0006EAA0
	public static void paint(mGraphics g)
	{
		if (!InfoDlg.isShow)
		{
			return;
		}
		if (InfoDlg.isLock && InfoDlg.delay > 4990)
		{
			return;
		}
		if (GameScr.isPaintAlert)
		{
			return;
		}
		int num = 10;
		GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
		if (InfoDlg.isLock)
		{
			GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
		}
		else if (InfoDlg.subtitke != null)
		{
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
			mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
		}
		else
		{
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
		}
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x000707A7 File Offset: 0x0006EBA7
	public static void update()
	{
		if (InfoDlg.delay > 0)
		{
			InfoDlg.delay--;
			if (InfoDlg.delay == 0)
			{
				InfoDlg.hide();
			}
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x000707CF File Offset: 0x0006EBCF
	public static void hide()
	{
		InfoDlg.title = string.Empty;
		InfoDlg.subtitke = null;
		InfoDlg.isLock = false;
		InfoDlg.delay = 0;
		InfoDlg.isShow = false;
	}

	// Token: 0x04000EB2 RID: 3762
	public static bool isShow;

	// Token: 0x04000EB3 RID: 3763
	private static string title;

	// Token: 0x04000EB4 RID: 3764
	private static string subtitke;

	// Token: 0x04000EB5 RID: 3765
	public static int delay;

	// Token: 0x04000EB6 RID: 3766
	public static bool isLock;
}
