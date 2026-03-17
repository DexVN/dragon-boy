using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class iOSPlugins
{
	// Token: 0x0600041B RID: 1051
	[DllImport("__Internal")]
	private static extern void _SMSsend(string tophone, string withtext, int n);

	// Token: 0x0600041C RID: 1052
	[DllImport("__Internal")]
	private static extern int _unpause();

	// Token: 0x0600041D RID: 1053
	[DllImport("__Internal")]
	private static extern int _checkRotation();

	// Token: 0x0600041E RID: 1054
	[DllImport("__Internal")]
	private static extern int _back();

	// Token: 0x0600041F RID: 1055
	[DllImport("__Internal")]
	private static extern int _Send();

	// Token: 0x06000420 RID: 1056
	[DllImport("__Internal")]
	private static extern void _purchaseItem(string itemID, string userName, string gameID);

	// Token: 0x06000421 RID: 1057 RVA: 0x00058348 File Offset: 0x00056548
	public static int Check()
	{
		bool flag = Application.platform == RuntimePlatform.IPhonePlayer;
		int result;
		if (flag)
		{
			result = iOSPlugins.checkCanSendSMS();
		}
		else
		{
			iOSPlugins.devide = iPhoneSettings.generation.ToString();
			string a = string.Empty + iOSPlugins.devide[2].ToString();
			bool flag2 = a == "h" && iOSPlugins.devide.Length > 6;
			if (flag2)
			{
				iOSPlugins.Myname = SystemInfo.operatingSystem.ToString();
				string a2 = string.Empty + iOSPlugins.Myname[10].ToString();
				bool flag3 = a2 != "2" && a2 != "3";
				if (flag3)
				{
					result = 0;
				}
				else
				{
					result = 1;
				}
			}
			else
			{
				Cout.println(iOSPlugins.devide + "  loai");
				bool flag4 = iOSPlugins.devide == "Unknown" && ScaleGUI.WIDTH * ScaleGUI.HEIGHT < 786432f;
				if (flag4)
				{
					result = 0;
				}
				else
				{
					result = -1;
				}
			}
		}
		return result;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0005846C File Offset: 0x0005666C
	public static int checkCanSendSMS()
	{
		bool flag = iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation == iPhoneGeneration.iPhone4S || iPhoneSettings.generation == iPhoneGeneration.iPhone5;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x000584B0 File Offset: 0x000566B0
	public static void SMSsend(string phonenumber, string bodytext, int n)
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._SMSsend(phonenumber, bodytext, n);
		}
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x000584D8 File Offset: 0x000566D8
	public static void back()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._back();
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x000584FC File Offset: 0x000566FC
	public static void Send()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._Send();
		}
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00058520 File Offset: 0x00056720
	public static int unpause()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		int result;
		if (flag)
		{
			result = iOSPlugins._unpause();
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00058548 File Offset: 0x00056748
	public static int checkRotation()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		int result;
		if (flag)
		{
			result = iOSPlugins._checkRotation();
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00058570 File Offset: 0x00056770
	public static void purchaseItem(string itemID, string userName, string gameID)
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._purchaseItem(itemID, userName, gameID);
		}
	}

	// Token: 0x0400090B RID: 2315
	public static string devide;

	// Token: 0x0400090C RID: 2316
	public static string Myname;
}
