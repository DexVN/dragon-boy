using System;

// Token: 0x02000033 RID: 51
public class FireWorkMn
{
	// Token: 0x06000284 RID: 644 RVA: 0x0003BBC8 File Offset: 0x00039DC8
	public FireWorkMn(int x, int y, int goc, int n)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
		this.n = n;
		for (int i = 0; i < n; i++)
		{
			this.fw.addElement(new Firework(x, y, global::Math.abs(this.rd.nextInt() % 8) + 3, i * goc, this.color[global::Math.abs(this.rd.nextInt() % this.color.Length)]));
		}
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0003BC98 File Offset: 0x00039E98
	public void paint(mGraphics g)
	{
		for (int i = 0; i < this.fw.size(); i++)
		{
			Firework firework = (Firework)this.fw.elementAt(i);
			bool flag = firework.y < -200;
			if (flag)
			{
				this.fw.removeElementAt(i);
			}
			firework.paint(g);
		}
	}

	// Token: 0x040005B4 RID: 1460
	private int x;

	// Token: 0x040005B5 RID: 1461
	private int y;

	// Token: 0x040005B6 RID: 1462
	private int goc = 1;

	// Token: 0x040005B7 RID: 1463
	private int n = 360;

	// Token: 0x040005B8 RID: 1464
	private MyRandom rd = new MyRandom();

	// Token: 0x040005B9 RID: 1465
	private MyVector fw = new MyVector();

	// Token: 0x040005BA RID: 1466
	private int[] color = new int[]
	{
		16711680,
		16776960,
		65280,
		16777215,
		255,
		65535,
		15790320,
		12632256
	};
}
