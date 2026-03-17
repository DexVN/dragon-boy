using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class Res
{
	// Token: 0x060007E7 RID: 2023 RVA: 0x0008F5F4 File Offset: 0x0008D7F4
	public static void init()
	{
		Res.cosz = new short[91];
		Res.tanz = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			Res.cosz[i] = Res.sinz[90 - i];
			bool flag = Res.cosz[i] == 0;
			if (flag)
			{
				Res.tanz[i] = int.MaxValue;
			}
			else
			{
				Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
			}
		}
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0008F67C File Offset: 0x0008D87C
	public static int sin(int a)
	{
		a = Res.fixangle(a);
		bool flag = a >= 0 && a < 90;
		int result;
		if (flag)
		{
			result = (int)Res.sinz[a];
		}
		else
		{
			bool flag2 = a >= 90 && a < 180;
			if (flag2)
			{
				result = (int)Res.sinz[180 - a];
			}
			else
			{
				bool flag3 = a >= 180 && a < 270;
				if (flag3)
				{
					result = (int)(-(int)Res.sinz[a - 180]);
				}
				else
				{
					result = (int)(-(int)Res.sinz[360 - a]);
				}
			}
		}
		return result;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0008F710 File Offset: 0x0008D910
	public static int cos(int a)
	{
		a = Res.fixangle(a);
		bool flag = a >= 0 && a < 90;
		int result;
		if (flag)
		{
			result = (int)Res.cosz[a];
		}
		else
		{
			bool flag2 = a >= 90 && a < 180;
			if (flag2)
			{
				result = (int)(-(int)Res.cosz[180 - a]);
			}
			else
			{
				bool flag3 = a >= 180 && a < 270;
				if (flag3)
				{
					result = (int)(-(int)Res.cosz[a - 180]);
				}
				else
				{
					result = (int)Res.cosz[360 - a];
				}
			}
		}
		return result;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0008F7A4 File Offset: 0x0008D9A4
	public static int tan(int a)
	{
		a = Res.fixangle(a);
		bool flag = a >= 0 && a < 90;
		int result;
		if (flag)
		{
			result = Res.tanz[a];
		}
		else
		{
			bool flag2 = a >= 90 && a < 180;
			if (flag2)
			{
				result = -Res.tanz[180 - a];
			}
			else
			{
				bool flag3 = a >= 180 && a < 270;
				if (flag3)
				{
					result = Res.tanz[a - 180];
				}
				else
				{
					result = -Res.tanz[360 - a];
				}
			}
		}
		return result;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0008F838 File Offset: 0x0008DA38
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			bool flag = Res.tanz[i] >= a;
			if (flag)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0008F878 File Offset: 0x0008DA78
	public static int angle(int dx, int dy)
	{
		bool flag = dx != 0;
		int num;
		if (flag)
		{
			int a = global::Math.abs((dy << 10) / dx);
			num = Res.atan(a);
			bool flag2 = dy >= 0 && dx < 0;
			if (flag2)
			{
				num = 180 - num;
			}
			bool flag3 = dy < 0 && dx < 0;
			if (flag3)
			{
				num = 180 + num;
			}
			bool flag4 = dy < 0 && dx >= 0;
			if (flag4)
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

	// Token: 0x060007ED RID: 2029 RVA: 0x0008F910 File Offset: 0x0008DB10
	public static int fixangle(int angle)
	{
		bool flag = angle >= 360;
		if (flag)
		{
			angle -= 360;
		}
		bool flag2 = angle < 0;
		if (flag2)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0008F950 File Offset: 0x0008DB50
	public static sbyte[] TakeSnapShot()
	{
		return null;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0008F964 File Offset: 0x0008DB64
	public static void outz(string s)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.Log(s);
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0008F984 File Offset: 0x0008DB84
	public static void outz(string s, int logIndex)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.Log(Res.LOG_CAT[logIndex] + s);
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0008F9B0 File Offset: 0x0008DBB0
	public static void err(string s)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.LogError(s);
		}
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00003136 File Offset: 0x00001336
	public static void outz2(string s)
	{
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00003136 File Offset: 0x00001336
	public static void onScreenDebug(string s)
	{
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00003136 File Offset: 0x00001336
	public static void paintOnScreenDebug(mGraphics g)
	{
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00003136 File Offset: 0x00001336
	public static void updateOnScreenDebug()
	{
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0008F9D0 File Offset: 0x0008DBD0
	public static string changeString(string str)
	{
		return str;
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0008F9E4 File Offset: 0x0008DBE4
	public static string replace(string _text, string _searchStr, string _replacementStr)
	{
		return _text.Replace(_searchStr, _replacementStr);
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0008FA00 File Offset: 0x0008DC00
	public static int xetVX(int goc, int d)
	{
		return Res.cos(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0008FA24 File Offset: 0x0008DC24
	public static int xetVY(int goc, int d)
	{
		return Res.sin(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0008FA48 File Offset: 0x0008DC48
	public static int random(int a, int b)
	{
		bool flag = a == b;
		int result;
		if (flag)
		{
			result = a;
		}
		else
		{
			result = a + Res.r.nextInt(b - a);
		}
		return result;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0008FA78 File Offset: 0x0008DC78
	public static int random(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0008FA98 File Offset: 0x0008DC98
	public static int random_Am(int a, int b)
	{
		int num = a + Res.r.nextInt(b - a);
		bool flag = Res.random(2) == 0;
		if (flag)
		{
			num = -num;
		}
		return num;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0008FAD0 File Offset: 0x0008DCD0
	public static int random_Am_0(int a)
	{
		int num;
		for (num = 0; num == 0; num = Res.r.nextInt() % a)
		{
		}
		return num;
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0008FB00 File Offset: 0x0008DD00
	public static int s2tick(int currentTimeMillis)
	{
		int num = currentTimeMillis * 16 / 1000;
		bool flag = currentTimeMillis * 16 % 1000 >= 5;
		if (flag)
		{
			num++;
		}
		return num;
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0008FB38 File Offset: 0x0008DD38
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0008FB60 File Offset: 0x0008DD60
	public static int getDistance(int x, int y)
	{
		return Res.sqrt(x * x + y * y);
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0008FB80 File Offset: 0x0008DD80
	public static int sqrt(int a)
	{
		bool flag = a <= 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int num = (a + 1) / 2;
			int num2;
			do
			{
				num2 = num;
				num = num / 2 + a / (2 * num);
			}
			while (global::Math.abs(num2 - num) > 1);
			result = num;
		}
		return result;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0008FBC8 File Offset: 0x0008DDC8
	public static int rnd(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0008FBE8 File Offset: 0x0008DDE8
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0008FC04 File Offset: 0x0008DE04
	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0008FC34 File Offset: 0x0008DE34
	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		bool flag = num >= 0;
		string[] array;
		if (flag)
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

	// Token: 0x06000806 RID: 2054 RVA: 0x0008FC94 File Offset: 0x0008DE94
	public static string formatNumber(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		bool flag = number >= 1000000000L;
		if (flag)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 100000000L;
			number /= 1000000000L;
			text = number.ToString() + string.Empty;
			bool flag2 = num > 0L;
			if (flag2)
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
		else
		{
			bool flag3 = number >= 1000000L;
			if (flag3)
			{
				text2 = mResources.million;
				long num2 = number % 1000000L / 100000L;
				number /= 1000000L;
				text = number.ToString() + string.Empty;
				bool flag4 = num2 > 0L;
				if (flag4)
				{
					string text4 = text;
					text = string.Concat(new object[]
					{
						text4,
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
				text = number.ToString() + string.Empty;
			}
		}
		return text;
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0008FDE8 File Offset: 0x0008DFE8
	public static string formatNumber2(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		bool flag = number >= 1000000000L;
		if (flag)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 10000000L;
			number /= 1000000000L;
			text = number.ToString() + string.Empty;
			bool flag2 = num >= 10L;
			if (flag2)
			{
				bool flag3 = num % 10L == 0L;
				if (flag3)
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
			else
			{
				bool flag4 = num > 0L;
				if (flag4)
				{
					string text4 = text;
					text = string.Concat(new object[]
					{
						text4,
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
		}
		else
		{
			bool flag5 = number >= 1000000L;
			if (flag5)
			{
				text2 = mResources.million;
				long num2 = number % 1000000L / 10000L;
				number /= 1000000L;
				text = number.ToString() + string.Empty;
				bool flag6 = num2 >= 10L;
				if (flag6)
				{
					bool flag7 = num2 % 10L == 0L;
					if (flag7)
					{
						num2 /= 10L;
					}
					string text5 = text;
					text = string.Concat(new object[]
					{
						text5,
						",",
						num2,
						text2
					});
				}
				else
				{
					bool flag8 = num2 > 0L;
					if (flag8)
					{
						string text6 = text;
						text = string.Concat(new object[]
						{
							text6,
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
			}
			else
			{
				bool flag9 = number >= 10000L;
				if (flag9)
				{
					text2 = "k";
					long num3 = number % 1000L / 10L;
					number /= 1000L;
					text = number.ToString() + string.Empty;
					bool flag10 = num3 >= 10L;
					if (flag10)
					{
						bool flag11 = num3 % 10L == 0L;
						if (flag11)
						{
							num3 /= 10L;
						}
						string text7 = text;
						text = string.Concat(new object[]
						{
							text7,
							",",
							num3,
							text2
						});
					}
					else
					{
						bool flag12 = num3 > 0L;
						if (flag12)
						{
							string text8 = text;
							text = string.Concat(new object[]
							{
								text8,
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
				}
				else
				{
					text = number.ToString() + string.Empty;
				}
			}
		}
		return text;
	}

	// Token: 0x04001081 RID: 4225
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

	// Token: 0x04001082 RID: 4226
	private static short[] cosz;

	// Token: 0x04001083 RID: 4227
	private static int[] tanz;

	// Token: 0x04001084 RID: 4228
	public static string[] LOG_CAT = new string[]
	{
		"<color=#ff0000ff>[  LOG_CAT  ]</color>",
		"<color=#ff0000ff>[LOG_SESSION]</color>",
		"<color=#ffff00ff>[LOG_SESSION]</color>",
		"<color=#ff0000ff>[LOG_MOBILE ]</color>",
		string.Empty
	};

	// Token: 0x04001085 RID: 4229
	public static int count;

	// Token: 0x04001086 RID: 4230
	public static bool isIcon;

	// Token: 0x04001087 RID: 4231
	public static bool isBig;

	// Token: 0x04001088 RID: 4232
	public static MyVector debug = new MyVector();

	// Token: 0x04001089 RID: 4233
	public static MyRandom r = new MyRandom();
}
