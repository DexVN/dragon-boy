using System;

// Token: 0x02000079 RID: 121
public class MyRandom
{
	// Token: 0x060005F4 RID: 1524 RVA: 0x0006ABFE File Offset: 0x00068DFE
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0006AC14 File Offset: 0x00068E14
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0006AC34 File Offset: 0x00068E34
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0006AC54 File Offset: 0x00068E54
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x04000DFC RID: 3580
	public Random r;
}
