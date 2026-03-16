using System;

// Token: 0x0200000F RID: 15
public class MyRandom
{
	// Token: 0x0600006A RID: 106 RVA: 0x00003A38 File Offset: 0x00001E38
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003A4B File Offset: 0x00001E4B
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003A58 File Offset: 0x00001E58
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00003A66 File Offset: 0x00001E66
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x04000026 RID: 38
	public Random r;
}
