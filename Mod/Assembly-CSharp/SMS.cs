using System;
using System.Threading;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class SMS
{
	// Token: 0x0600093D RID: 2365 RVA: 0x0009B78C File Offset: 0x0009998C
	public static int send(string content, string to)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		int result;
		if (flag)
		{
			result = SMS.__send(content, to);
		}
		else
		{
			result = SMS._send(content, to);
		}
		return result;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0009B7C8 File Offset: 0x000999C8
	private static int _send(string content, string to)
	{
		bool flag = SMS.status != 0;
		if (flag)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = SMS.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = SMS.status != 0;
			if (flag3)
			{
				Cout.LogError("CANNOT SEND SMS " + content + " WHEN SENDING " + SMS._content);
				return -1;
			}
		}
		SMS._content = content;
		SMS._to = to;
		SMS._result = -1;
		SMS.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			bool flag4 = SMS.status == 0;
			if (flag4)
			{
				break;
			}
		}
		bool flag5 = j == 500;
		if (flag5)
		{
			Debug.LogError("TOO LONG FOR SEND SMS " + content);
			SMS.status = 0;
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"Send SMS ",
				content,
				" done in ",
				j * 5,
				"ms"
			}));
		}
		return SMS._result;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0009B8FC File Offset: 0x00099AFC
	private static int __send(string content, string to)
	{
		int num = iOSPlugins.Check();
		Cout.println("vao sms ko " + num.ToString());
		bool flag = num >= 0;
		if (flag)
		{
			SMS.f = true;
			SMS.sendEnable = true;
			iOSPlugins.SMSsend(to, content, num);
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
		return num;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0009B958 File Offset: 0x00099B58
	public static void update()
	{
		float num = Time.time;
		bool flag = num - (float)SMS.time > 1f;
		if (flag)
		{
			SMS.time++;
		}
		bool flag2 = SMS.f;
		if (flag2)
		{
			SMS.OnSMS();
		}
		bool flag3 = SMS.status == 2;
		if (flag3)
		{
			SMS.status = 1;
			try
			{
				SMS._result = SMS.__send(SMS._content, SMS._to);
			}
			catch (Exception ex)
			{
				Debug.Log("CANNOT SEND SMS");
			}
			SMS.status = 0;
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0009B9F4 File Offset: 0x00099BF4
	private static void OnSMS()
	{
		bool flag = SMS.sendEnable;
		if (flag)
		{
			bool flag2 = iOSPlugins.checkRotation() == 1;
			if (flag2)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			else
			{
				bool flag3 = iOSPlugins.checkRotation() == -1;
				if (flag3)
				{
					Screen.orientation = ScreenOrientation.Portrait;
				}
				else
				{
					bool flag4 = iOSPlugins.checkRotation() == 0;
					if (flag4)
					{
						Screen.orientation = ScreenOrientation.AutoRotation;
					}
					else
					{
						bool flag5 = iOSPlugins.checkRotation() == 2;
						if (flag5)
						{
							Screen.orientation = ScreenOrientation.LandscapeRight;
						}
						else
						{
							bool flag6 = iOSPlugins.checkRotation() == 3;
							if (flag6)
							{
								Screen.orientation = ScreenOrientation.PortraitUpsideDown;
							}
						}
					}
				}
			}
			bool flag7 = SMS.time0 < 5;
			if (flag7)
			{
				SMS.time0++;
			}
			else
			{
				iOSPlugins.Send();
				SMS.sendEnable = false;
				SMS.time0 = 0;
			}
		}
		bool flag8 = iOSPlugins.unpause() == 1;
		if (flag8)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			bool flag9 = SMS.time0 < 5;
			if (flag9)
			{
				SMS.time0++;
			}
			else
			{
				SMS.f = false;
				iOSPlugins.back();
				SMS.time0 = 0;
			}
		}
	}

	// Token: 0x0400117A RID: 4474
	private const int INTERVAL = 5;

	// Token: 0x0400117B RID: 4475
	private const int MAXTIME = 500;

	// Token: 0x0400117C RID: 4476
	private static int status;

	// Token: 0x0400117D RID: 4477
	private static int _result;

	// Token: 0x0400117E RID: 4478
	private static string _to;

	// Token: 0x0400117F RID: 4479
	private static string _content;

	// Token: 0x04001180 RID: 4480
	private static bool f;

	// Token: 0x04001181 RID: 4481
	private static int time;

	// Token: 0x04001182 RID: 4482
	public static bool sendEnable;

	// Token: 0x04001183 RID: 4483
	private static int time0;
}
