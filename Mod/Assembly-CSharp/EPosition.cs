using System;

// Token: 0x02000030 RID: 48
public class EPosition
{
	// Token: 0x06000274 RID: 628 RVA: 0x0003B515 File Offset: 0x00039715
	public EPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0003B53B File Offset: 0x0003973B
	public EPosition(int x, int y, int fol)
	{
		this.x = x;
		this.y = y;
		this.follow = (sbyte)fol;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0003B569 File Offset: 0x00039769
	public EPosition()
	{
	}

	// Token: 0x0400058A RID: 1418
	public int x;

	// Token: 0x0400058B RID: 1419
	public int y;

	// Token: 0x0400058C RID: 1420
	public int anchor;

	// Token: 0x0400058D RID: 1421
	public sbyte follow;

	// Token: 0x0400058E RID: 1422
	public sbyte count;

	// Token: 0x0400058F RID: 1423
	public sbyte dir = 1;

	// Token: 0x04000590 RID: 1424
	public short index = -1;
}
