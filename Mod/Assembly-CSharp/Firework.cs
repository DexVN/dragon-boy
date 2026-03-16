using System;

// Token: 0x02000041 RID: 65
public class Firework
{
	// Token: 0x06000294 RID: 660 RVA: 0x000145DC File Offset: 0x000129DC
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

	// Token: 0x06000295 RID: 661 RVA: 0x0001469C File Offset: 0x00012A9C
	public void preDraw()
	{
		if (this.time() - this.last >= this.delay)
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

	// Token: 0x06000296 RID: 662 RVA: 0x000147B0 File Offset: 0x00012BB0
	public void paint(mGraphics g)
	{
		this.Drawline(g, this.w - this.x, this.h - this.y, this.cl);
		for (int i = 0; i < 2; i++)
		{
			this.Drawline(g, this.w - this.arr_x[i], this.h - this.arr_y[i], this.cl);
		}
		if (this.act)
		{
			this.preDraw();
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00014832 File Offset: 0x00012C32
	public long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00014839 File Offset: 0x00012C39
	public void Drawline(mGraphics g, int x, int y, int color)
	{
		g.setColor(color);
		g.fillRect(x, y, 1, 2);
	}

	// Token: 0x04000312 RID: 786
	public int w;

	// Token: 0x04000313 RID: 787
	public int h;

	// Token: 0x04000314 RID: 788
	public int v;

	// Token: 0x04000315 RID: 789
	public int x0;

	// Token: 0x04000316 RID: 790
	public int x;

	// Token: 0x04000317 RID: 791
	public int y;

	// Token: 0x04000318 RID: 792
	public int y0;

	// Token: 0x04000319 RID: 793
	public int angle;

	// Token: 0x0400031A RID: 794
	public int t;

	// Token: 0x0400031B RID: 795
	public int cl = 16711680;

	// Token: 0x0400031C RID: 796
	private float a;

	// Token: 0x0400031D RID: 797
	private long last;

	// Token: 0x0400031E RID: 798
	private long delay = 150L;

	// Token: 0x0400031F RID: 799
	private bool act = true;

	// Token: 0x04000320 RID: 800
	private int[] arr_x = new int[2];

	// Token: 0x04000321 RID: 801
	private int[] arr_y = new int[2];
}
