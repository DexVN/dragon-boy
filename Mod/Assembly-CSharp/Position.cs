using System;

// Token: 0x0200007C RID: 124
public class Position
{
	// Token: 0x06000431 RID: 1073 RVA: 0x00033C5F File Offset: 0x0003205F
	public Position()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00033C75 File Offset: 0x00032075
	public Position(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00033C92 File Offset: 0x00032092
	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00033CA8 File Offset: 0x000320A8
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00033CE0 File Offset: 0x000320E0
	public int translate()
	{
		if (this.x == (int)this.xTo && this.y == (int)this.yTo)
		{
			return -1;
		}
		if (global::Math.abs(((int)this.xTo - this.x) / 2) <= 1 && global::Math.abs(((int)this.yTo - this.y) / 2) <= 1)
		{
			this.x = (int)this.xTo;
			this.y = (int)this.yTo;
			return 0;
		}
		if (this.x != (int)this.xTo)
		{
			this.x += ((int)this.xTo - this.x) / 2;
		}
		if (this.y != (int)this.yTo)
		{
			this.y += ((int)this.yTo - this.y) / 2;
		}
		if (Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo) <= (int)(this.distant / 5))
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00033DE6 File Offset: 0x000321E6
	public void update()
	{
		this.layer.update();
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00033DF3 File Offset: 0x000321F3
	public void paint(mGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x04000751 RID: 1873
	public int x;

	// Token: 0x04000752 RID: 1874
	public int y;

	// Token: 0x04000753 RID: 1875
	public int anchor;

	// Token: 0x04000754 RID: 1876
	public int g;

	// Token: 0x04000755 RID: 1877
	public int v;

	// Token: 0x04000756 RID: 1878
	public int w;

	// Token: 0x04000757 RID: 1879
	public int h;

	// Token: 0x04000758 RID: 1880
	public int color;

	// Token: 0x04000759 RID: 1881
	public int limitY;

	// Token: 0x0400075A RID: 1882
	public Layer layer;

	// Token: 0x0400075B RID: 1883
	public short yTo;

	// Token: 0x0400075C RID: 1884
	public short xTo;

	// Token: 0x0400075D RID: 1885
	public short distant;
}
