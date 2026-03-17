using System;

// Token: 0x0200005E RID: 94
public class Line
{
	// Token: 0x060004A0 RID: 1184 RVA: 0x0005A5D1 File Offset: 0x000587D1
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

	// Token: 0x060004A1 RID: 1185 RVA: 0x0005A60C File Offset: 0x0005880C
	public void update()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
		this.f++;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0005A674 File Offset: 0x00058874
	public void update_not_F()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
	}

	// Token: 0x04000A00 RID: 2560
	public int x0;

	// Token: 0x04000A01 RID: 2561
	public int y0;

	// Token: 0x04000A02 RID: 2562
	public int x1;

	// Token: 0x04000A03 RID: 2563
	public int y1;

	// Token: 0x04000A04 RID: 2564
	public int vx;

	// Token: 0x04000A05 RID: 2565
	public int vy;

	// Token: 0x04000A06 RID: 2566
	public int f;

	// Token: 0x04000A07 RID: 2567
	public int fRe;

	// Token: 0x04000A08 RID: 2568
	public int idColor;

	// Token: 0x04000A09 RID: 2569
	public int type;

	// Token: 0x04000A0A RID: 2570
	public bool is2Line;

	// Token: 0x04000A0B RID: 2571
	public FrameImage fraImgEff;

	// Token: 0x04000A0C RID: 2572
	public int[] frame;
}
