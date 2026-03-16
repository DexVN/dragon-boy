using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class Res
{
	// Token: 0x060009AE RID: 2478 RVA: 0x000952C8 File Offset: 0x000936C8
	public static void init()
	{
		Res.cosz = new short[91];
		Res.tanz = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			Res.cosz[i] = Res.sinz[90 - i];
			if (Res.cosz[i] == 0)
			{
				Res.tanz[i] = int.MaxValue;
			}
			else
			{
				Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
			}
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00095348 File Offset: 0x00093748
	public static int sin(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.sinz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)Res.sinz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.sinz[a - 180]);
		}
		return (int)(-(int)Res.sinz[360 - a]);
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x000953C8 File Offset: 0x000937C8
	public static int cos(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.cosz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)(-(int)Res.cosz[180 - a]);
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.cosz[a - 180]);
		}
		return (int)Res.cosz[360 - a];
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00095448 File Offset: 0x00093848
	public static int tan(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return Res.tanz[a];
		}
		if (a >= 90 && a < 180)
		{
			return -Res.tanz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return Res.tanz[a - 180];
		}
		return -Res.tanz[360 - a];
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x000954C8 File Offset: 0x000938C8
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			if (Res.tanz[i] >= a)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x000954F8 File Offset: 0x000938F8
	public static int angle(int dx, int dy)
	{
		int num;
		if (dx != 0)
		{
			int a = global::Math.abs((dy << 10) / dx);
			num = Res.atan(a);
			if (dy >= 0 && dx < 0)
			{
				num = 180 - num;
			}
			if (dy < 0 && dx < 0)
			{
				num = 180 + num;
			}
			if (dy < 0 && dx >= 0)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0009557A File Offset: 0x0009397A
	public static int fixangle(int angle)
	{
		if (angle >= 360)
		{
			angle -= 360;
		}
		if (angle < 0)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x000955A1 File Offset: 0x000939A1
	public static sbyte[] TakeSnapShot()
	{
		return null;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x000955A4 File Offset: 0x000939A4
	public static void outz(string s)
	{
		if (mSystem.isTest)
		{
			Debug.Log(s);
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x000955B6 File Offset: 0x000939B6
	public static void outz(string s, int logIndex)
	{
		if (mSystem.isTest)
		{
			Debug.Log(Res.LOG_CAT[logIndex] + s);
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x000955D4 File Offset: 0x000939D4
	public static void err(string s)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(s);
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x000955E6 File Offset: 0x000939E6
	public static void outz2(string s)
	{
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x000955E8 File Offset: 0x000939E8
	public static void onScreenDebug(string s)
	{
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x000955EA File Offset: 0x000939EA
	public static void paintOnScreenDebug(mGraphics g)
	{
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x000955EC File Offset: 0x000939EC
	public static void updateOnScreenDebug()
	{
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x000955EE File Offset: 0x000939EE
	public static string changeString(string str)
	{
		return str;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x000955F1 File Offset: 0x000939F1
	public static string replace(string _text, string _searchStr, string _replacementStr)
	{
		return _text.Replace(_searchStr, _replacementStr);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x000955FB File Offset: 0x000939FB
	public static int xetVX(int goc, int d)
	{
		return Res.cos(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0009560D File Offset: 0x00093A0D
	public static int xetVY(int goc, int d)
	{
		return Res.sin(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0009561F File Offset: 0x00093A1F
	public static int random(int a, int b)
	{
		if (a == b)
		{
			return a;
		}
		return a + Res.r.nextInt(b - a);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00095639 File Offset: 0x00093A39
	public static int random(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x00095648 File Offset: 0x00093A48
	public static int random_Am(int a, int b)
	{
		int num = a + Res.r.nextInt(b - a);
		if (Res.random(2) == 0)
		{
			num = -num;
		}
		return num;
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00095674 File Offset: 0x00093A74
	public static int random_Am_0(int a)
	{
		int num;
		for (num = 0; num == 0; num = Res.r.nextInt() % a)
		{
		}
		return num;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0009569C File Offset: 0x00093A9C
	public static int s2tick(int currentTimeMillis)
	{
		int num = currentTimeMillis * 16 / 1000;
		if (currentTimeMillis * 16 % 1000 >= 5)
		{
			num++;
		}
		return num;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x000956CB File Offset: 0x00093ACB
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x000956E1 File Offset: 0x00093AE1
	public static int getDistance(int x, int y)
	{
		return Res.sqrt(x * x + y * y);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000956F0 File Offset: 0x00093AF0
	public static int sqrt(int a)
	{
		if (a <= 0)
		{
			return 0;
		}
		int num = (a + 1) / 2;
		int num2;
		do
		{
			num2 = num;
			num = num / 2 + a / (2 * num);
		}
		while (global::Math.abs(num2 - num) > 1);
		return num;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00095727 File Offset: 0x00093B27
	public static int rnd(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00095734 File Offset: 0x00093B34
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00095745 File Offset: 0x00093B45
	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00095774 File Offset: 0x00093B74
	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		string[] array;
		if (num >= 0)
		{
			array = Res.split(original.Substring(num + separator.Length), separator, count + 1);
		}
		else
		{
			array = new string[count + 1];
			num = original.Length;
		}
		array[count] = original.Substring(0, num);
		return array;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x000957CC File Offset: 0x00093BCC
	public static string formatNumber(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 100000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			if (num > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 100000L;
			number /= 1000000L;
			text = number + string.Empty;
			if (num2 > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num2,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else
		{
			text = number + string.Empty;
		}
		return text;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000958FC File Offset: 0x00093CFC
	public static string formatNumber2(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 10000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			if (num >= 10L)
			{
				if (num % 10L == 0L)
				{
					num /= 10L;
				}
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else if (num > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",0",
					num,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 10000L;
			number /= 1000000L;
			text = number + string.Empty;
			if (num2 >= 10L)
			{
				if (num2 % 10L == 0L)
				{
					num2 /= 10L;
				}
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num2,
					text2
				});
			}
			else if (num2 > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",0",
					num2,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 10000L)
		{
			text2 = "k";
			long num3 = number % 1000L / 10L;
			number /= 1000L;
			text = number + string.Empty;
			if (num3 >= 10L)
			{
				if (num3 % 10L == 0L)
				{
					num3 /= 10L;
				}
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num3,
					text2
				});
			}
			else if (num3 > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",0",
					num3,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else
		{
			text = number + string.Empty;
		}
		return text;
	}

	// Token: 0x04001211 RID: 4625
	private static short[] sinz = new short[]
	{
		0,
		18,
		36,
		54,
		71,
		89,
		107,
		125,
		143,
		160,
		178,
		195,
		213,
		230,
		248,
		265,
		282,
		299,
		316,
		333,
		350,
		367,
		384,
		400,
		416,
		433,
		449,
		465,
		481,
		496,
		512,
		527,
		543,
		558,
		573,
		587,
		602,
		616,
		630,
		644,
		658,
		672,
		685,
		698,
		711,
		724,
		737,
		749,
		761,
		773,
		784,
		796,
		807,
		818,
		828,
		839,
		849,
		859,
		868,
		878,
		887,
		896,
		904,
		912,
		920,
		928,
		935,
		943,
		949,
		956,
		962,
		968,
		974,
		979,
		984,
		989,
		994,
		998,
		1002,
		1005,
		1008,
		1011,
		1014,
		1016,
		1018,
		1020,
		1022,
		1023,
		1023,
		1024,
		1024
	};

	// Token: 0x04001212 RID: 4626
	private static short[] cosz;

	// Token: 0x04001213 RID: 4627
	private static int[] tanz;

	// Token: 0x04001214 RID: 4628
	public static string[] LOG_CAT = new string[]
	{
		"<color=#ff0000ff>[  LOG_CAT  ]</color>",
		"<color=#ff0000ff>[LOG_SESSION]</color>",
		"<color=#ffff00ff>[LOG_SESSION]</color>",
		"<color=#ff0000ff>[LOG_MOBILE ]</color>",
		string.Empty
	};

	// Token: 0x04001215 RID: 4629
	public static int count;

	// Token: 0x04001216 RID: 4630
	public static bool isIcon;

	// Token: 0x04001217 RID: 4631
	public static bool isBig;

	// Token: 0x04001218 RID: 4632
	public static MyVector debug = new MyVector();

	// Token: 0x04001219 RID: 4633
	public static MyRandom r = new MyRandom();
}
