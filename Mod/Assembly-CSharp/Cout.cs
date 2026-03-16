using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class Cout
{
	// Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000004FA
	public static void println(string s)
	{
		if (mSystem.isTest)
		{
			Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
			Cout.count++;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000538
	public static void Log(string str)
	{
		if (mSystem.isTest)
		{
			Debug.Log(str);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000214A File Offset: 0x0000054A
	public static void LogError(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000055C
	public static void LogError2(string str)
	{
		if (mSystem.isTest)
		{
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002168 File Offset: 0x00000568
	public static void LogError3(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000217A File Offset: 0x0000057A
	public static void LogWarning(string str)
	{
		if (mSystem.isTest)
		{
			Debug.LogWarning(str);
		}
	}

	// Token: 0x04000001 RID: 1
	public static int count;
}
