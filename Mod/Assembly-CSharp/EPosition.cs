using System;

// Token: 0x02000038 RID: 56
public class EPosition
{
	// Token: 0x06000255 RID: 597 RVA: 0x00011D00 File Offset: 0x00010100
	public EPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00011D24 File Offset: 0x00010124
	public EPosition(int x, int y, int fol)
	{
		this.x = x;
		this.y = y;
		this.follow = (sbyte)fol;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00011D50 File Offset: 0x00010150
	public EPosition()
	{
	}

	// Token: 0x0400028E RID: 654
	public int x;

	// Token: 0x0400028F RID: 655
	public int y;

	// Token: 0x04000290 RID: 656
	public int anchor;

	// Token: 0x04000291 RID: 657
	public sbyte follow;

	// Token: 0x04000292 RID: 658
	public sbyte count;

	// Token: 0x04000293 RID: 659
	public sbyte dir = 1;

	// Token: 0x04000294 RID: 660
	public short index = -1;
}
