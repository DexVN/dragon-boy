using System;

// Token: 0x02000082 RID: 130
public class NinjaUtil
{
	// Token: 0x0600066E RID: 1646 RVA: 0x00058318 File Offset: 0x00056518
	public static void onLoadMapComplete()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0006CCF2 File Offset: 0x0006AEF2
	public void onLoading()
	{
		GameCanvas.startWaitDlg(mResources.downloading_data);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0006CD00 File Offset: 0x0006AF00
	public static int randomNumber(int max)
	{
		MyRandom myRandom = new MyRandom();
		return myRandom.nextInt(max);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0006CD20 File Offset: 0x0006AF20
	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			bool flag = num > 1;
			if (flag)
			{
				sbyte[] result = new sbyte[num];
				msg.reader().read(ref result);
				return result;
			}
		}
		catch (Exception ex)
		{
		}
		return null;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0006CD7C File Offset: 0x0006AF7C
	public static sbyte[] readByteArray(myReader dos)
	{
		try
		{
			int num = dos.readInt();
			sbyte[] result = new sbyte[num];
			dos.read(ref result);
			return result;
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI DOC readByteArray dos  NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0006CDCC File Offset: 0x0006AFCC
	public static string replace(string text, string regex, string replacement)
	{
		return text.Replace(regex, replacement);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0006CDE8 File Offset: 0x0006AFE8
	public static string numberTostring(string number)
	{
		string text = string.Empty;
		string str = string.Empty;
		bool flag = number.Equals(string.Empty);
		string result;
		if (flag)
		{
			result = text;
		}
		else
		{
			bool flag2 = number[0] == '-';
			if (flag2)
			{
				str = "-";
				number = number.Substring(1);
			}
			for (int i = number.Length - 1; i >= 0; i--)
			{
				bool flag3 = (number.Length - 1 - i) % 3 == 0 && number.Length - 1 - i > 0;
				if (flag3)
				{
					text = number[i].ToString() + "." + text;
				}
				else
				{
					text = number[i].ToString() + text;
				}
			}
			result = str + text;
		}
		return result;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0006CECC File Offset: 0x0006B0CC
	public static string getDate(int second)
	{
		long num = (long)second * 1000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime dateTime2 = dateTime.Add(new TimeSpan(num * 10000L)).ToUniversalTime();
		int hour = dateTime2.Hour;
		int minute = dateTime2.Minute;
		int day = dateTime2.Day;
		int month = dateTime2.Month;
		int year = dateTime2.Year;
		return string.Concat(new object[]
		{
			day,
			"/",
			month,
			"/",
			year,
			" ",
			hour,
			"h"
		});
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0006CF98 File Offset: 0x0006B198
	public static string getDate2(long second)
	{
		long num = second + 25200000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime dateTime2 = dateTime.Add(new TimeSpan(num * 10000L)).ToUniversalTime();
		int hour = dateTime2.Hour;
		int minute = dateTime2.Minute;
		return string.Concat(new object[]
		{
			hour,
			"h",
			minute,
			"m"
		});
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0006D024 File Offset: 0x0006B224
	public static string getTime(int timeRemainS)
	{
		int num = 0;
		bool flag = timeRemainS > 60;
		if (flag)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		bool flag2 = num > 60;
		if (flag2)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		bool flag3 = num2 > 24;
		if (flag3)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		bool flag4 = num3 > 0;
		if (flag4)
		{
			text += num3.ToString();
			text += "d";
			text = text + num2.ToString() + "h";
		}
		else
		{
			bool flag5 = num2 > 0;
			if (flag5)
			{
				text += num2.ToString();
				text += "h";
				text = text + num.ToString() + "'";
			}
			else
			{
				bool flag6 = num > 9;
				if (flag6)
				{
					text += num.ToString();
				}
				else
				{
					text = text + "0" + num.ToString();
				}
				text += ":";
				bool flag7 = timeRemainS > 9;
				if (flag7)
				{
					text += timeRemainS.ToString();
				}
				else
				{
					text = text + "0" + timeRemainS.ToString();
				}
			}
		}
		return text;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0006D178 File Offset: 0x0006B378
	public static string getMoneys(long m)
	{
		string text = string.Empty;
		long num = m / 1000L + 1L;
		int num2 = 0;
		while ((long)num2 < num)
		{
			bool flag = m < 1000L;
			if (flag)
			{
				text = m.ToString() + text;
				break;
			}
			long num3 = m % 1000L;
			bool flag2 = num3 == 0L;
			if (flag2)
			{
				text = ".000" + text;
			}
			else
			{
				bool flag3 = num3 < 10L;
				if (flag3)
				{
					text = ".00" + num3.ToString() + text;
				}
				else
				{
					bool flag4 = num3 < 100L;
					if (flag4)
					{
						text = ".0" + num3.ToString() + text;
					}
					else
					{
						text = "." + num3.ToString() + text;
					}
				}
			}
			m /= 1000L;
			num2++;
		}
		return text;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0006D268 File Offset: 0x0006B468
	public static string getTimeAgo(int timeRemainS)
	{
		int num = 0;
		bool flag = timeRemainS > 60;
		if (flag)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		bool flag2 = num > 60;
		if (flag2)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		bool flag3 = num2 > 24;
		if (flag3)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		bool flag4 = num3 > 0;
		if (flag4)
		{
			text += num3.ToString();
			text += "d";
			text = text + num2.ToString() + "h";
		}
		else
		{
			bool flag5 = num2 > 0;
			if (flag5)
			{
				text += num2.ToString();
				text += "h";
				text = text + num.ToString() + "'";
			}
			else
			{
				bool flag6 = num == 0;
				if (flag6)
				{
					num = 1;
				}
				text += num.ToString();
				text += "ph";
			}
		}
		return text;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0006D370 File Offset: 0x0006B570
	public static string[] split(string original, string separator)
	{
		MyVector myVector = new MyVector();
		for (int i = original.IndexOf(separator); i >= 0; i = original.IndexOf(separator))
		{
			myVector.addElement(original.Substring(0, i));
			original = original.Substring(i + separator.Length);
		}
		myVector.addElement(original);
		string[] array = new string[myVector.size()];
		bool flag = myVector.size() > 0;
		if (flag)
		{
			for (int j = 0; j < myVector.size(); j++)
			{
				array[j] = (string)myVector.elementAt(j);
			}
		}
		return array;
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0006D41C File Offset: 0x0006B61C
	public static bool checkNumber(string numberStr)
	{
		bool result;
		try
		{
			int.Parse(numberStr);
			result = true;
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}
}
