using System;

// Token: 0x02000032 RID: 50
public class FireWorkEff
{
	// Token: 0x0600027C RID: 636 RVA: 0x0003B81C File Offset: 0x00039A1C
	public static void preDraw()
	{
		bool flag = FireWorkEff.st;
		if (flag)
		{
			FireWorkEff.animate();
		}
		bool flag2 = FireWorkEff.t > 32 && FireWorkEff.st;
		if (flag2)
		{
			FireWorkEff.st = false;
			FireWorkEff.mg.removeAllElements();
			FireWorkEff.mg.addElement(new FireWorkMn(Res.random(50, GameCanvas.w - 50), Res.random(GameCanvas.h - 100, GameCanvas.h), 5, 72));
		}
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0003B898 File Offset: 0x00039A98
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
		bool flag = !FireWorkEff.st;
		if (flag)
		{
			FireWorkEff.keyPressed(-(global::Math.abs(FireWorkEff.r.nextInt() % 3) + 5));
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0003B930 File Offset: 0x00039B30
	public static void keyPressed(int k)
	{
		bool flag = k == -5 && !FireWorkEff.st;
		if (flag)
		{
			FireWorkEff.x0 = FireWorkEff.w / 2;
			FireWorkEff.ag = 80;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else
		{
			bool flag2 = k == -7 && !FireWorkEff.st;
			if (flag2)
			{
				FireWorkEff.ag = 60;
				FireWorkEff.x0 = 0;
				FireWorkEff.st = true;
				FireWorkEff.add();
			}
			else
			{
				bool flag3 = k == -6 && !FireWorkEff.st;
				if (flag3)
				{
					FireWorkEff.ag = 120;
					FireWorkEff.x0 = FireWorkEff.w;
					FireWorkEff.st = true;
					FireWorkEff.add();
				}
			}
		}
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0003B9DC File Offset: 0x00039BDC
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

	// Token: 0x06000280 RID: 640 RVA: 0x0003BA34 File Offset: 0x00039C34
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
		bool flag = FireWorkEff.time() - FireWorkEff.last >= FireWorkEff.delay;
		if (flag)
		{
			FireWorkEff.t++;
			FireWorkEff.last = FireWorkEff.time();
		}
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0003BB50 File Offset: 0x00039D50
	public static long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x040005A1 RID: 1441
	private static int w;

	// Token: 0x040005A2 RID: 1442
	private static int h;

	// Token: 0x040005A3 RID: 1443
	private static MyRandom r = new MyRandom();

	// Token: 0x040005A4 RID: 1444
	private static MyVector mg = new MyVector();

	// Token: 0x040005A5 RID: 1445
	private static int f = 17;

	// Token: 0x040005A6 RID: 1446
	private static int x;

	// Token: 0x040005A7 RID: 1447
	private static int y;

	// Token: 0x040005A8 RID: 1448
	private static int ag;

	// Token: 0x040005A9 RID: 1449
	private static int x0;

	// Token: 0x040005AA RID: 1450
	private static int y0;

	// Token: 0x040005AB RID: 1451
	private static int t;

	// Token: 0x040005AC RID: 1452
	private static int v;

	// Token: 0x040005AD RID: 1453
	private static int ymax = 269;

	// Token: 0x040005AE RID: 1454
	private static float a;

	// Token: 0x040005AF RID: 1455
	private static int[] mang_x = new int[3];

	// Token: 0x040005B0 RID: 1456
	private static int[] mang_y = new int[3];

	// Token: 0x040005B1 RID: 1457
	private static bool st = false;

	// Token: 0x040005B2 RID: 1458
	private static long last = 0L;

	// Token: 0x040005B3 RID: 1459
	private static long delay = 150L;
}
