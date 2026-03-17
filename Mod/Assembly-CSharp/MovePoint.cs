using System;

// Token: 0x02000071 RID: 113
public class MovePoint
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x0006777C File Offset: 0x0006597C
	public MovePoint(int xEnd, int yEnd, int act, int dir)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.dir = dir;
		this.status = act;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x000677A3 File Offset: 0x000659A3
	public MovePoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
	}

	// Token: 0x04000C0B RID: 3083
	public int xEnd;

	// Token: 0x04000C0C RID: 3084
	public int yEnd;

	// Token: 0x04000C0D RID: 3085
	public int dir;

	// Token: 0x04000C0E RID: 3086
	public int cvx;

	// Token: 0x04000C0F RID: 3087
	public int cvy;

	// Token: 0x04000C10 RID: 3088
	public int status;
}
