using System;
using System.Text;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class mSystem
{
	// Token: 0x060005BF RID: 1471 RVA: 0x00003136 File Offset: 0x00001336
	public static void AddIpTest()
	{
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00069597 File Offset: 0x00067797
	public static void resetCurInapp()
	{
		mSystem.curINAPP = 0;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x000695A0 File Offset: 0x000677A0
	public static int getWidth(Image img)
	{
		bool flag = mSystem.clientType == 5;
		int width;
		if (flag)
		{
			width = img.getWidth();
		}
		else
		{
			width = img.getWidth();
		}
		return width;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000695D0 File Offset: 0x000677D0
	public static int getHeight(Image img)
	{
		bool flag = mSystem.clientType == 5;
		int result;
		if (flag)
		{
			result = img.getHeight();
		}
		else
		{
			result = img.getWidth();
		}
		return result;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00069600 File Offset: 0x00067800
	public static string getTimeCountDown(long timeStart, int secondCount, bool isOnlySecond, bool isShortText)
	{
		string result = string.Empty;
		long num = (timeStart + (long)(secondCount * 1000) - mSystem.currentTimeMillis()) / 1000L;
		bool flag = num <= 0L;
		string result2;
		if (flag)
		{
			result2 = string.Empty;
		}
		else
		{
			long num2 = 0L;
			long num3 = 0L;
			long num4 = num / 60L;
			long num5 = num;
			if (isOnlySecond)
			{
				result2 = num5.ToString() + string.Empty;
			}
			else
			{
				bool flag2 = num >= 86400L;
				if (flag2)
				{
					num2 = num / 86400L;
					num3 = num % 86400L / 3600L;
				}
				else
				{
					bool flag3 = num >= 3600L;
					if (flag3)
					{
						num3 = num / 3600L;
						num4 = num % 3600L / 60L;
					}
					else
					{
						bool flag4 = num >= 60L;
						if (flag4)
						{
							num4 = num / 60L;
							num5 = num % 60L;
						}
						else
						{
							num5 = num;
						}
					}
				}
				if (isShortText)
				{
					bool flag5 = num2 > 0L;
					if (flag5)
					{
						return num2.ToString() + "d";
					}
					bool flag6 = num3 > 0L;
					if (flag6)
					{
						return num3.ToString() + "h";
					}
					bool flag7 = num4 > 0L;
					if (flag7)
					{
						return num4.ToString() + "m";
					}
					bool flag8 = num5 > 0L;
					if (flag8)
					{
						return num5.ToString() + "s";
					}
				}
				bool flag9 = num2 > 0L;
				if (flag9)
				{
					bool flag10 = num2 >= 10L;
					if (flag10)
					{
						bool flag11 = num3 < 1L;
						if (flag11)
						{
							result = num2.ToString() + "d";
						}
						else
						{
							bool flag12 = num3 < 10L;
							if (flag12)
							{
								result = string.Concat(new object[]
								{
									num2,
									"d0",
									num3,
									"h"
								});
							}
							else
							{
								result = string.Concat(new object[]
								{
									num2,
									"d",
									num3,
									"h"
								});
							}
						}
					}
					else
					{
						bool flag13 = num2 < 10L;
						if (flag13)
						{
							bool flag14 = num3 < 1L;
							if (flag14)
							{
								result = num2.ToString() + "d";
							}
							else
							{
								bool flag15 = num3 < 10L;
								if (flag15)
								{
									result = string.Concat(new object[]
									{
										num2,
										"d0",
										num3,
										"h"
									});
								}
								else
								{
									result = string.Concat(new object[]
									{
										num2,
										"d",
										num3,
										"h"
									});
								}
							}
						}
					}
				}
				else
				{
					bool flag16 = num3 > 0L;
					if (flag16)
					{
						bool flag17 = num3 >= 10L;
						if (flag17)
						{
							bool flag18 = num4 < 1L;
							if (flag18)
							{
								result = num3.ToString() + "h";
							}
							else
							{
								bool flag19 = num4 < 10L;
								if (flag19)
								{
									result = string.Concat(new object[]
									{
										num3,
										"h0",
										num4,
										"m"
									});
								}
								else
								{
									result = string.Concat(new object[]
									{
										num3,
										"h",
										num4,
										"m"
									});
								}
							}
						}
						else
						{
							bool flag20 = num3 < 10L;
							if (flag20)
							{
								bool flag21 = num4 < 1L;
								if (flag21)
								{
									result = num3.ToString() + "h";
								}
								else
								{
									bool flag22 = num4 < 10L;
									if (flag22)
									{
										result = string.Concat(new object[]
										{
											num3,
											"h0",
											num4,
											"m"
										});
									}
									else
									{
										result = string.Concat(new object[]
										{
											num3,
											"h",
											num4,
											"m"
										});
									}
								}
							}
						}
					}
					else
					{
						bool flag23 = num4 > 0L;
						if (flag23)
						{
							bool flag24 = num4 >= 10L;
							if (flag24)
							{
								bool flag25 = num5 >= 10L;
								if (flag25)
								{
									result = string.Concat(new object[]
									{
										num4,
										"m",
										num5,
										string.Empty
									});
								}
								else
								{
									bool flag26 = num5 < 10L;
									if (flag26)
									{
										result = string.Concat(new object[]
										{
											num4,
											"m0",
											num5,
											string.Empty
										});
									}
								}
							}
							else
							{
								bool flag27 = num4 < 10L;
								if (flag27)
								{
									bool flag28 = num5 >= 10L;
									if (flag28)
									{
										result = string.Concat(new object[]
										{
											num4,
											"m",
											num5,
											string.Empty
										});
									}
									else
									{
										bool flag29 = num5 < 10L;
										if (flag29)
										{
											result = string.Concat(new object[]
											{
												num4,
												"m0",
												num5,
												string.Empty
											});
										}
									}
								}
							}
						}
						else
						{
							result = ((num5 >= 10L) ? (num5.ToString() + string.Empty) : ("0" + num5.ToString() + string.Empty));
						}
					}
				}
				result2 = result;
			}
		}
		return result2;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00069BD8 File Offset: 0x00067DD8
	public static string numberTostring2(int aa)
	{
		string result;
		try
		{
			string text = string.Empty;
			string str = string.Empty;
			string text2 = aa.ToString() + string.Empty;
			bool flag = text2.Equals(string.Empty);
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = text2[0] == '-';
				if (flag2)
				{
					str = "-";
					text2 = text2.Substring(1);
				}
				for (int i = text2.Length - 1; i >= 0; i--)
				{
					bool flag3 = (text2.Length - 1 - i) % 3 == 0 && text2.Length - 1 - i > 0;
					if (flag3)
					{
						text = text2[i].ToString() + "." + text;
					}
					else
					{
						text = text2[i].ToString() + text;
					}
				}
				result = str + text;
			}
		}
		catch (Exception ex)
		{
			result = aa.ToString() + string.Empty;
		}
		return result;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00069D00 File Offset: 0x00067F00
	public static string numberTostring(long number)
	{
		string text = string.Empty + number.ToString();
		bool flag = false;
		try
		{
			string text2 = string.Empty;
			bool flag2 = number < 0L;
			if (flag2)
			{
				flag = true;
				number = -number;
				text = string.Empty + number.ToString();
			}
			bool flag3 = number >= 1000000000L;
			int length;
			if (flag3)
			{
				text2 = "b";
				number /= 1000000000L;
				length = (string.Empty + number.ToString()).Length;
			}
			else
			{
				bool flag4 = number >= 1000000L;
				if (flag4)
				{
					text2 = "m";
					number /= 1000000L;
					length = (string.Empty + number.ToString()).Length;
				}
				else
				{
					bool flag5 = number >= 1000L;
					if (flag5)
					{
						text2 = "k";
						number /= 1000L;
						length = (string.Empty + number.ToString()).Length;
					}
					else
					{
						bool flag6 = flag;
						if (flag6)
						{
							return "-" + text;
						}
						return text;
					}
				}
			}
			int num = int.Parse(text.Substring(length, 2));
			bool flag7 = num == 0;
			if (flag7)
			{
				text = text.Substring(0, length) + text2;
			}
			else
			{
				bool flag8 = num % 10 == 0;
				if (flag8)
				{
					text = text.Substring(0, length) + "," + text.Substring(length, 1) + text2;
				}
				else
				{
					text = text.Substring(0, length) + "," + text.Substring(length, 2) + text2;
				}
			}
		}
		catch (Exception ex)
		{
		}
		bool flag9 = flag;
		string result;
		if (flag9)
		{
			result = "-" + text;
		}
		else
		{
			result = text;
		}
		return result;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00069EE8 File Offset: 0x000680E8
	public static void callHotlinePC()
	{
		Application.OpenURL("http://ngocrongonline.com/");
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00003136 File Offset: 0x00001336
	public static void callHotlineJava()
	{
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00003136 File Offset: 0x00001336
	public static void callHotlineIphone()
	{
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00003136 File Offset: 0x00001336
	public static void callHotlineWindowsPhone()
	{
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00003136 File Offset: 0x00001336
	public static void closeBanner()
	{
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00003136 File Offset: 0x00001336
	public static void showBanner()
	{
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00003136 File Offset: 0x00001336
	public static void createAdmob()
	{
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00003136 File Offset: 0x00001336
	public static void checkAdComlete()
	{
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00069EF6 File Offset: 0x000680F6
	public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
	{
		g.fillRect(x, y, w + 10, h, 0, 90);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00069F0B File Offset: 0x0006810B
	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00069F1C File Offset: 0x0006811C
	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		bool flag = scr == null || dest == null || scrPos + lenght > scr.Length;
		if (!flag)
		{
			sbyte[] array = new sbyte[dest.Length + lenght];
			for (int i = 0; i < destPos; i++)
			{
				array[i] = dest[i];
			}
			for (int j = destPos; j < destPos + lenght; j++)
			{
				array[j] = scr[scrPos + j - destPos];
			}
			for (int k = destPos + lenght; k < array.Length; k++)
			{
				array[k] = dest[destPos + k - lenght];
			}
		}
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00069FBC File Offset: 0x000681BC
	public static long currentTimeMillis()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00069FFC File Offset: 0x000681FC
	public static void freeData()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0006A00C File Offset: 0x0006820C
	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0006A044 File Offset: 0x00068244
	public static sbyte[] convertToSbyte(string scr)
	{
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(scr);
		return mSystem.convertToSbyte(bytes);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0006A06C File Offset: 0x0006826C
	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			bool flag = scr[i] > 0;
			if (flag)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)((int)scr[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0006A0C4 File Offset: 0x000682C4
	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0006A0FC File Offset: 0x000682FC
	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0006A11B File Offset: 0x0006831B
	public static void println(object str)
	{
		Debug.Log(str);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00069FFC File Offset: 0x000681FC
	public static void gcc()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0006A128 File Offset: 0x00068328
	public static mSystem gI()
	{
		bool flag = mSystem.instance == null;
		if (flag)
		{
			mSystem.instance = new mSystem();
		}
		return mSystem.instance;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0006A157 File Offset: 0x00068357
	public static void onConnectOK()
	{
		Controller.isConnectOK = true;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0006A160 File Offset: 0x00068360
	public static void onConnectionFail()
	{
		Controller.isConnectionFail = true;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0006A169 File Offset: 0x00068369
	public static void onDisconnected()
	{
		Controller.isDisconnected = true;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00003136 File Offset: 0x00001336
	public static void exitWP()
	{
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0006A174 File Offset: 0x00068374
	public static void paintFlyText(mGraphics g)
	{
		for (int i = 0; i < 5; i++)
		{
			bool flag = GameScr.flyTextState[i] != -1;
			if (flag)
			{
				bool flag2 = GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]);
				if (flag2)
				{
					bool flag3 = GameScr.flyTextColor[i] == mFont.RED;
					if (flag3)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else
					{
						bool flag4 = GameScr.flyTextColor[i] == mFont.YELLOW;
						if (flag4)
						{
							mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
						}
						else
						{
							bool flag5 = GameScr.flyTextColor[i] == mFont.GREEN;
							if (flag5)
							{
								mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
							}
							else
							{
								bool flag6 = GameScr.flyTextColor[i] == mFont.FATAL;
								if (flag6)
								{
									mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
								}
								else
								{
									bool flag7 = GameScr.flyTextColor[i] == mFont.FATAL_ME;
									if (flag7)
									{
										mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
									}
									else
									{
										bool flag8 = GameScr.flyTextColor[i] == mFont.MISS;
										if (flag8)
										{
											mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
										}
										else
										{
											bool flag9 = GameScr.flyTextColor[i] == mFont.ORANGE;
											if (flag9)
											{
												mFont.bigNumber_orange.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
											}
											else
											{
												bool flag10 = GameScr.flyTextColor[i] == mFont.ADDMONEY;
												if (flag10)
												{
													mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
												}
												else
												{
													bool flag11 = GameScr.flyTextColor[i] == mFont.MISS_ME;
													if (flag11)
													{
														mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
													}
													else
													{
														bool flag12 = GameScr.flyTextColor[i] == mFont.HP;
														if (flag12)
														{
															mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
														}
														else
														{
															bool flag13 = GameScr.flyTextColor[i] == mFont.MP;
															if (flag13)
															{
																mFont.bigNumber_blue.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00003136 File Offset: 0x00001336
	public static void endKey()
	{
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0006A4B4 File Offset: 0x000686B4
	public static FrameImage getFraImage(string nameImg)
	{
		FrameImage result = null;
		MainImage mainImage = null;
		bool flag = mainImage == null;
		if (flag)
		{
			mainImage = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
		}
		bool flag2 = mainImage.img != null;
		if (flag2)
		{
			int num = mainImage.img.getHeight() / (int)mainImage.nFrame;
			bool flag3 = num < 1;
			if (flag3)
			{
				num = 1;
			}
			result = new FrameImage(mainImage.img, mainImage.img.getWidth(), num);
		}
		return result;
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0006A530 File Offset: 0x00068730
	public static Image loadImage(string path)
	{
		return GameCanvas.loadImage(path);
	}

	// Token: 0x04000DE5 RID: 3557
	public static bool isTest;

	// Token: 0x04000DE6 RID: 3558
	public static string strAdmob;

	// Token: 0x04000DE7 RID: 3559
	public static bool loadAdOk;

	// Token: 0x04000DE8 RID: 3560
	public static string publicID;

	// Token: 0x04000DE9 RID: 3561
	public static string android_pack;

	// Token: 0x04000DEA RID: 3562
	public static int clientType = 4;

	// Token: 0x04000DEB RID: 3563
	public static sbyte LANGUAGE;

	// Token: 0x04000DEC RID: 3564
	public static sbyte curINAPP;

	// Token: 0x04000DED RID: 3565
	public static sbyte maxINAPP = 5;

	// Token: 0x04000DEE RID: 3566
	public const int JAVA = 1;

	// Token: 0x04000DEF RID: 3567
	public const int ANDROID = 2;

	// Token: 0x04000DF0 RID: 3568
	public const int IP_JB = 3;

	// Token: 0x04000DF1 RID: 3569
	public const int PC = 4;

	// Token: 0x04000DF2 RID: 3570
	public const int IP_APPSTORE = 5;

	// Token: 0x04000DF3 RID: 3571
	public const int WINDOWS_PHONE = 6;

	// Token: 0x04000DF4 RID: 3572
	public const int GOOGLE_PLAY = 7;

	// Token: 0x04000DF5 RID: 3573
	public static mSystem instance;

	// Token: 0x04000DF6 RID: 3574
	internal static bool isANDROID;
}
