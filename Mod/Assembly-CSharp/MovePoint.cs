using System;

// Token: 0x0200006F RID: 111
public class MovePoint
{
	// Token: 0x060003CD RID: 973 RVA: 0x0003132D File Offset: 0x0002F72D
	public MovePoint(int xEnd, int yEnd, int act, int dir)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.dir = dir;
		this.status = act;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00031352 File Offset: 0x0002F752
	public MovePoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
	}

	// Token: 0x04000693 RID: 1683
	public int xEnd;

	// Token: 0x04000694 RID: 1684
	public int yEnd;

	// Token: 0x04000695 RID: 1685
	public int dir;

	// Token: 0x04000696 RID: 1686
	public int cvx;

	// Token: 0x04000697 RID: 1687
	public int cvy;

	// Token: 0x04000698 RID: 1688
	public int status;
}
