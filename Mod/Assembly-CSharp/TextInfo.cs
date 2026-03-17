using System;

// Token: 0x020000B4 RID: 180
public class TextInfo
{
	// Token: 0x060009C6 RID: 2502 RVA: 0x000A37D1 File Offset: 0x000A19D1
	public static void reset()
	{
		TextInfo.dx = 0;
		TextInfo.tx = 0;
		TextInfo.isBack = false;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x000A37E8 File Offset: 0x000A19E8
	public static void paint(mGraphics g, string str, int x, int y, int w, int h, mFont f)
	{
		bool flag = TextInfo.wStr != f.getWidth(str) || !TextInfo.laststring.Equals(str);
		if (flag)
		{
			TextInfo.laststring = str;
			TextInfo.dx = 0;
			TextInfo.wStr = f.getWidth(str);
			TextInfo.isBack = false;
			TextInfo.tx = 0;
		}
		g.setClip(x, y, w, h);
		bool flag2 = TextInfo.wStr > w;
		if (flag2)
		{
			f.drawString(g, str, x - TextInfo.dx, y, 0);
		}
		else
		{
			f.drawString(g, str, x + w / 2, y, 2);
		}
		GameCanvas.resetTrans(g);
		bool flag3 = TextInfo.wStr > w;
		if (flag3)
		{
			bool flag4 = !TextInfo.isBack;
			if (flag4)
			{
				TextInfo.tx++;
				bool flag5 = TextInfo.tx > 50;
				if (flag5)
				{
					TextInfo.dx++;
					bool flag6 = TextInfo.dx >= TextInfo.wStr;
					if (flag6)
					{
						TextInfo.tx = 0;
						TextInfo.dx = -w + 30;
						TextInfo.isBack = true;
					}
				}
			}
			else
			{
				bool flag7 = TextInfo.dx < 0;
				if (flag7)
				{
					int num = w + TextInfo.dx >> 1;
					TextInfo.dx += num;
				}
				bool flag8 = TextInfo.dx > 0;
				if (flag8)
				{
					TextInfo.dx = 0;
				}
				bool flag9 = TextInfo.dx == 0;
				if (flag9)
				{
					TextInfo.tx++;
					bool flag10 = TextInfo.tx == 50;
					if (flag10)
					{
						TextInfo.tx = 0;
						TextInfo.isBack = false;
					}
				}
			}
		}
	}

	// Token: 0x04001266 RID: 4710
	public static int dx;

	// Token: 0x04001267 RID: 4711
	public static int tx;

	// Token: 0x04001268 RID: 4712
	public static int wStr;

	// Token: 0x04001269 RID: 4713
	public static bool isBack;

	// Token: 0x0400126A RID: 4714
	public static string laststring = string.Empty;
}
