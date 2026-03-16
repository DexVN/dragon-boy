using System;

// Token: 0x0200006A RID: 106
public class Line
{
	// Token: 0x060003BD RID: 957 RVA: 0x0001ECFD File Offset: 0x0001D0FD
	public void setLine(int x0, int y0, int x1, int y1, int vx, int vy, bool is2Line)
	{
		this.x0 = x0;
		this.y0 = y0;
		this.x1 = x1;
		this.y1 = y1;
		this.vx = vx;
		this.vy = vy;
		this.is2Line = is2Line;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001ED34 File Offset: 0x0001D134
	public void update()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
		this.f++;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0001ED9C File Offset: 0x0001D19C
	public void update_not_F()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
	}

	// Token: 0x0400064A RID: 1610
	public int x0;

	// Token: 0x0400064B RID: 1611
	public int y0;

	// Token: 0x0400064C RID: 1612
	public int x1;

	// Token: 0x0400064D RID: 1613
	public int y1;

	// Token: 0x0400064E RID: 1614
	public int vx;

	// Token: 0x0400064F RID: 1615
	public int vy;

	// Token: 0x04000650 RID: 1616
	public int f;

	// Token: 0x04000651 RID: 1617
	public int fRe;

	// Token: 0x04000652 RID: 1618
	public int idColor;

	// Token: 0x04000653 RID: 1619
	public int type;

	// Token: 0x04000654 RID: 1620
	public bool is2Line;

	// Token: 0x04000655 RID: 1621
	public FrameImage fraImgEff;

	// Token: 0x04000656 RID: 1622
	public int[] frame;
}
