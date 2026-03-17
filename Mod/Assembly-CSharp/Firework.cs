using System;

// Token: 0x02000031 RID: 49
public class Firework
{
	// Token: 0x06000277 RID: 631 RVA: 0x0003B584 File Offset: 0x00039784
	public Firework(int x0, int y0, int v, int angle, int cl)
	{
		this.y0 = y0;
		this.x0 = x0;
		this.a = 1f;
		this.v = v;
		this.angle = angle;
		this.w = GameCanvas.w;
		this.h = GameCanvas.h;
		this.last = this.time();
		for (int i = 0; i < 2; i++)
		{
			this.arr_x[i] = x0;
			this.arr_y[i] = y0;
		}
		this.cl = cl;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0003B648 File Offset: 0x00039848
	public void preDraw()
	{
		bool flag = this.time() - this.last >= this.delay;
		if (flag)
		{
			this.t++;
			this.last = this.time();
			this.arr_x[1] = this.arr_x[0];
			this.arr_y[1] = this.arr_y[0];
			this.arr_x[0] = this.x;
			this.arr_y[0] = this.y;
			this.x = Res.cos((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.v * this.t + this.x0;
			this.y = (int)((float)(this.v * Res.sin((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.t) - this.a * (float)this.t * (float)this.t / 2f) + this.y0;
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0003B764 File Offset: 0x00039964
	public void paint(mGraphics g)
	{
		this.Drawline(g, this.w - this.x, this.h - this.y, this.cl);
		for (int i = 0; i < 2; i++)
		{
			this.Drawline(g, this.w - this.arr_x[i], this.h - this.arr_y[i], this.cl);
		}
		bool flag = this.act;
		if (flag)
		{
			this.preDraw();
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0003B7EC File Offset: 0x000399EC
	public long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0003B803 File Offset: 0x00039A03
	public void Drawline(mGraphics g, int x, int y, int color)
	{
		g.setColor(color);
		g.fillRect(x, y, 1, 2);
	}

	// Token: 0x04000591 RID: 1425
	public int w;

	// Token: 0x04000592 RID: 1426
	public int h;

	// Token: 0x04000593 RID: 1427
	public int v;

	// Token: 0x04000594 RID: 1428
	public int x0;

	// Token: 0x04000595 RID: 1429
	public int x;

	// Token: 0x04000596 RID: 1430
	public int y;

	// Token: 0x04000597 RID: 1431
	public int y0;

	// Token: 0x04000598 RID: 1432
	public int angle;

	// Token: 0x04000599 RID: 1433
	public int t;

	// Token: 0x0400059A RID: 1434
	public int cl = 16711680;

	// Token: 0x0400059B RID: 1435
	private float a;

	// Token: 0x0400059C RID: 1436
	private long last;

	// Token: 0x0400059D RID: 1437
	private long delay = 150L;

	// Token: 0x0400059E RID: 1438
	private bool act = true;

	// Token: 0x0400059F RID: 1439
	private int[] arr_x = new int[2];

	// Token: 0x040005A0 RID: 1440
	private int[] arr_y = new int[2];
}
