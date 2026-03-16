using System;

// Token: 0x02000040 RID: 64
public class FireWorkMn
{
	// Token: 0x06000292 RID: 658 RVA: 0x000144B0 File Offset: 0x000128B0
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

	// Token: 0x06000293 RID: 659 RVA: 0x0001457C File Offset: 0x0001297C
	public void paint(mGraphics g)
	{
		for (int i = 0; i < this.fw.size(); i++)
		{
			Firework firework = (Firework)this.fw.elementAt(i);
			if (firework.y < -200)
			{
				this.fw.removeElementAt(i);
			}
			firework.paint(g);
		}
	}

	// Token: 0x0400030B RID: 779
	private int x;

	// Token: 0x0400030C RID: 780
	private int y;

	// Token: 0x0400030D RID: 781
	private int goc = 1;

	// Token: 0x0400030E RID: 782
	private int n = 360;

	// Token: 0x0400030F RID: 783
	private MyRandom rd = new MyRandom();

	// Token: 0x04000310 RID: 784
	private MyVector fw = new MyVector();

	// Token: 0x04000311 RID: 785
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
