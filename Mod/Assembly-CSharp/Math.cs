using System;

// Token: 0x0200000B RID: 11
public class Math
{
	// Token: 0x06000057 RID: 87 RVA: 0x000033D1 File Offset: 0x000017D1
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x000033E2 File Offset: 0x000017E2
	public static int min(int x, int y)
	{
		return (x >= y) ? y : x;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000033F2 File Offset: 0x000017F2
	public static int max(int x, int y)
	{
		return (x <= y) ? y : x;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003404 File Offset: 0x00001804
	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}

	// Token: 0x04000020 RID: 32
	public const double PI = 3.141592653589793;
}
