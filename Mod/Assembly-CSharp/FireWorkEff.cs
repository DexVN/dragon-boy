using System;

// Token: 0x0200003F RID: 63
public class FireWorkEff
{
	// Token: 0x0600028B RID: 651 RVA: 0x00014138 File Offset: 0x00012538
	public static void preDraw()
	{
		if (FireWorkEff.st)
		{
			FireWorkEff.animate();
		}
		if (FireWorkEff.t > 32 && FireWorkEff.st)
		{
			FireWorkEff.st = false;
			FireWorkEff.mg.removeAllElements();
			FireWorkEff.mg.addElement(new FireWorkMn(Res.random(50, GameCanvas.w - 50), Res.random(GameCanvas.h - 100, GameCanvas.h), 5, 72));
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x000141B0 File Offset: 0x000125B0
	public static void paint(mGraphics g)
	{
		FireWorkEff.preDraw();
		g.setColor(0);
		g.fillRect(0, 0, FireWorkEff.w, FireWorkEff.h);
		g.setColor(16711680);
		for (int i = 0; i < FireWorkEff.mg.size(); i++)
		{
			((FireWorkMn)FireWorkEff.mg.elementAt(i)).paint(g);
		}
		if (!FireWorkEff.st)
		{
			FireWorkEff.keyPressed(-(global::Math.abs(FireWorkEff.r.nextInt() % 3) + 5));
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0001423C File Offset: 0x0001263C
	public static void keyPressed(int k)
	{
		if (k == -5 && !FireWorkEff.st)
		{
			FireWorkEff.x0 = FireWorkEff.w / 2;
			FireWorkEff.ag = 80;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else if (k == -7 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 60;
			FireWorkEff.x0 = 0;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else if (k == -6 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 120;
			FireWorkEff.x0 = FireWorkEff.w;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x000142DC File Offset: 0x000126DC
	public static void add()
	{
		FireWorkEff.y0 = 0;
		FireWorkEff.v = 16;
		FireWorkEff.t = 0;
		FireWorkEff.a = 0f;
		for (int i = 0; i < 3; i++)
		{
			FireWorkEff.mang_y[i] = 0;
			FireWorkEff.mang_x[i] = FireWorkEff.x0;
		}
		FireWorkEff.st = true;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00014334 File Offset: 0x00012734
	public static void animate()
	{
		FireWorkEff.mang_y[2] = FireWorkEff.mang_y[1];
		FireWorkEff.mang_x[2] = FireWorkEff.mang_x[1];
		FireWorkEff.mang_y[1] = FireWorkEff.mang_y[0];
		FireWorkEff.mang_x[1] = FireWorkEff.mang_x[0];
		FireWorkEff.mang_y[0] = FireWorkEff.y;
		FireWorkEff.mang_x[0] = FireWorkEff.x;
		FireWorkEff.x = Res.cos((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.v * FireWorkEff.t + FireWorkEff.x0;
		FireWorkEff.y = (int)((float)(FireWorkEff.v * Res.sin((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.t) - FireWorkEff.a * (float)FireWorkEff.t * (float)FireWorkEff.t / 2f) + FireWorkEff.y0;
		if (FireWorkEff.time() - FireWorkEff.last >= FireWorkEff.delay)
		{
			FireWorkEff.t++;
			FireWorkEff.last = FireWorkEff.time();
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00014446 File Offset: 0x00012846
	public static long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x040002F8 RID: 760
	private static int w;

	// Token: 0x040002F9 RID: 761
	private static int h;

	// Token: 0x040002FA RID: 762
	private static MyRandom r = new MyRandom();

	// Token: 0x040002FB RID: 763
	private static MyVector mg = new MyVector();

	// Token: 0x040002FC RID: 764
	private static int f = 17;

	// Token: 0x040002FD RID: 765
	private static int x;

	// Token: 0x040002FE RID: 766
	private static int y;

	// Token: 0x040002FF RID: 767
	private static int ag;

	// Token: 0x04000300 RID: 768
	private static int x0;

	// Token: 0x04000301 RID: 769
	private static int y0;

	// Token: 0x04000302 RID: 770
	private static int t;

	// Token: 0x04000303 RID: 771
	private static int v;

	// Token: 0x04000304 RID: 772
	private static int ymax = 269;

	// Token: 0x04000305 RID: 773
	private static float a;

	// Token: 0x04000306 RID: 774
	private static int[] mang_x = new int[3];

	// Token: 0x04000307 RID: 775
	private static int[] mang_y = new int[3];

	// Token: 0x04000308 RID: 776
	private static bool st = false;

	// Token: 0x04000309 RID: 777
	private static long last = 0L;

	// Token: 0x0400030A RID: 778
	private static long delay = 150L;
}
