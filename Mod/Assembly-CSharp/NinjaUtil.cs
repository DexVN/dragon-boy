using System;

// Token: 0x0200009A RID: 154
public class NinjaUtil
{
	// Token: 0x060004EC RID: 1260 RVA: 0x000502D0 File Offset: 0x0004E6D0
	public static void onLoadMapComplete()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000502D7 File Offset: 0x0004E6D7
	public void onLoading()
	{
		GameCanvas.startWaitDlg(mResources.downloading_data);
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000502E4 File Offset: 0x0004E6E4
	public static int randomNumber(int max)
	{
		MyRandom myRandom = new MyRandom();
		return myRandom.nextInt(max);
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00050300 File Offset: 0x0004E700
	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			if (num > 1)
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

	// Token: 0x060004F0 RID: 1264 RVA: 0x0005035C File Offset: 0x0004E75C
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

	// Token: 0x060004F1 RID: 1265 RVA: 0x000503AC File Offset: 0x0004E7AC
	public static string replace(string text, string regex, string replacement)
	{
		return text.Replace(regex, replacement);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x000503B8 File Offset: 0x0004E7B8
	public static string numberTostring(string number)
	{
		string text = string.Empty;
		string str = string.Empty;
		if (number.Equals(string.Empty))
		{
			return text;
		}
		if (number[0] == '-')
		{
			str = "-";
			number = number.Substring(1);
		}
		for (int i = number.Length - 1; i >= 0; i--)
		{
			if ((number.Length - 1 - i) % 3 == 0 && number.Length - 1 - i > 0)
			{
				text = number[i] + "." + text;
			}
			else
			{
				text = number[i] + text;
			}
		}
		return str + text;
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00050474 File Offset: 0x0004E874
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

	// Token: 0x060004F4 RID: 1268 RVA: 0x0005053C File Offset: 0x0004E93C
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

	// Token: 0x060004F5 RID: 1269 RVA: 0x000505C4 File Offset: 0x0004E9C4
	public static string getTime(int timeRemainS)
	{
		int num = 0;
		if (timeRemainS > 60)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		if (num > 60)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		if (num2 > 24)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		if (num3 > 0)
		{
			text += num3;
			text += "d";
			text = text + num2 + "h";
		}
		else if (num2 > 0)
		{
			text += num2;
			text += "h";
			text = text + num + "'";
		}
		else
		{
			if (num > 9)
			{
				text += num;
			}
			else
			{
				text = text + "0" + num;
			}
			text += ":";
			if (timeRemainS > 9)
			{
				text += timeRemainS;
			}
			else
			{
				text = text + "0" + timeRemainS;
			}
		}
		return text;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x000506E8 File Offset: 0x0004EAE8
	public static string getMoneys(long m)
	{
		string text = string.Empty;
		long num = m / 1000L + 1L;
		int num2 = 0;
		while ((long)num2 < num)
		{
			if (m < 1000L)
			{
				text = m + text;
				break;
			}
			long num3 = m % 1000L;
			if (num3 == 0L)
			{
				text = ".000" + text;
			}
			else if (num3 < 10L)
			{
				text = ".00" + num3 + text;
			}
			else if (num3 < 100L)
			{
				text = ".0" + num3 + text;
			}
			else
			{
				text = "." + num3 + text;
			}
			m /= 1000L;
			num2++;
		}
		return text;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x000507BC File Offset: 0x0004EBBC
	public static string getTimeAgo(int timeRemainS)
	{
		int num = 0;
		if (timeRemainS > 60)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		if (num > 60)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		if (num2 > 24)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		if (num3 > 0)
		{
			text += num3;
			text += "d";
			text = text + num2 + "h";
		}
		else if (num2 > 0)
		{
			text += num2;
			text += "h";
			text = text + num + "'";
		}
		else
		{
			if (num == 0)
			{
				num = 1;
			}
			text += num;
			text += "ph";
		}
		return text;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0005089C File Offset: 0x0004EC9C
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
		if (myVector.size() > 0)
		{
			for (int j = 0; j < myVector.size(); j++)
			{
				array[j] = (string)myVector.elementAt(j);
			}
		}
		return array;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00050930 File Offset: 0x0004ED30
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
