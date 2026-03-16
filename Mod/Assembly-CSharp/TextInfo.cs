using System;

// Token: 0x02000094 RID: 148
public class TextInfo
{
	// Token: 0x060004B8 RID: 1208 RVA: 0x0003CC28 File Offset: 0x0003B028
	public static void reset()
	{
		TextInfo.dx = 0;
		TextInfo.tx = 0;
		TextInfo.isBack = false;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0003CC3C File Offset: 0x0003B03C
	public static void paint(mGraphics g, string str, int x, int y, int w, int h, mFont f)
	{
		if (TextInfo.wStr != f.getWidth(str) || !TextInfo.laststring.Equals(str))
		{
			TextInfo.laststring = str;
			TextInfo.dx = 0;
			TextInfo.wStr = f.getWidth(str);
			TextInfo.isBack = false;
			TextInfo.tx = 0;
		}
		g.setClip(x, y, w, h);
		if (TextInfo.wStr > w)
		{
			f.drawString(g, str, x - TextInfo.dx, y, 0);
		}
		else
		{
			f.drawString(g, str, x + w / 2, y, 2);
		}
		GameCanvas.resetTrans(g);
		if (TextInfo.wStr > w)
		{
			if (!TextInfo.isBack)
			{
				TextInfo.tx++;
				if (TextInfo.tx > 50)
				{
					TextInfo.dx++;
					if (TextInfo.dx >= TextInfo.wStr)
					{
						TextInfo.tx = 0;
						TextInfo.dx = -w + 30;
						TextInfo.isBack = true;
					}
				}
			}
			else
			{
				if (TextInfo.dx < 0)
				{
					int num = w + TextInfo.dx >> 1;
					TextInfo.dx += num;
				}
				if (TextInfo.dx > 0)
				{
					TextInfo.dx = 0;
				}
				if (TextInfo.dx == 0)
				{
					TextInfo.tx++;
					if (TextInfo.tx == 50)
					{
						TextInfo.tx = 0;
						TextInfo.isBack = false;
					}
				}
			}
		}
	}

	// Token: 0x04000824 RID: 2084
	public static int dx;

	// Token: 0x04000825 RID: 2085
	public static int tx;

	// Token: 0x04000826 RID: 2086
	public static int wStr;

	// Token: 0x04000827 RID: 2087
	public static bool isBack;

	// Token: 0x04000828 RID: 2088
	public static string laststring = string.Empty;
}
