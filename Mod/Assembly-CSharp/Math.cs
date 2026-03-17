using System;

// Token: 0x02000064 RID: 100
public class Math
{
	// Token: 0x060004DB RID: 1243 RVA: 0x0005D9E4 File Offset: 0x0005BBE4
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0005DA00 File Offset: 0x0005BC00
	public static int min(int x, int y)
	{
		return (x >= y) ? y : x;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0005DA1C File Offset: 0x0005BC1C
	public static int max(int x, int y)
	{
		return (x <= y) ? y : x;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0005DA38 File Offset: 0x0005BC38
	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}

	// Token: 0x04000A9C RID: 2716
	public const double PI = 3.141592653589793;
}
