using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class Cout
{
	// Token: 0x060001AC RID: 428 RVA: 0x000310E0 File Offset: 0x0002F2E0
	public static void println(string s)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
			Cout.count++;
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00031128 File Offset: 0x0002F328
	public static void Log(string str)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.Log(str);
		}
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00031148 File Offset: 0x0002F348
	public static void LogError(string str)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00031168 File Offset: 0x0002F368
	public static void LogError2(string str)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00031184 File Offset: 0x0002F384
	public static void LogError3(string str)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.LogError(str);
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000311A4 File Offset: 0x0002F3A4
	public static void LogWarning(string str)
	{
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			Debug.LogWarning(str);
		}
	}

	// Token: 0x0400045F RID: 1119
	public static int count;
}
