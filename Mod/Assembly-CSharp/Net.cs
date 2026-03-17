using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
internal class Net
{
	// Token: 0x0600063F RID: 1599 RVA: 0x0006B884 File Offset: 0x00069A84
	public static void update()
	{
		bool flag = Net.www != null && Net.www.isDone;
		if (flag)
		{
			string str = string.Empty;
			bool flag2 = Net.www.error == null || Net.www.error.Equals(string.Empty);
			if (flag2)
			{
				str = Net.www.text;
			}
			Net.www = null;
			bool flag3 = Net.h != null;
			if (flag3)
			{
				Net.h.perform(str);
			}
		}
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0006B908 File Offset: 0x00069B08
	public static void connectHTTP(string link, Command h)
	{
		bool flag = Net.www != null;
		if (flag)
		{
			Cout.LogError("GET HTTP BUSY");
		}
		Net.www = new WWW(link);
		Net.h = h;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0006B940 File Offset: 0x00069B40
	public static void connectHTTP2(string link, Command h)
	{
		Net.h = h;
		bool flag = link != null;
		if (flag)
		{
			h.perform(link);
		}
	}

	// Token: 0x04000E09 RID: 3593
	public static WWW www;

	// Token: 0x04000E0A RID: 3594
	public static Command h;
}
