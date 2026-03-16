using System;

// Token: 0x0200009D RID: 157
public class BallInfo
{
	// Token: 0x060005BA RID: 1466 RVA: 0x0005992C File Offset: 0x00057D2C
	public void SetChar()
	{
		this.cFocus = new global::Char();
		this.cFocus.charID = Res.random(-999, -800);
		this.cFocus.head = -1;
		this.cFocus.body = -1;
		this.cFocus.leg = -1;
		this.cFocus.bag = -1;
		this.cFocus.cName = string.Empty;
		this.cFocus.cHP = (this.cFocus.cHPFull = 20);
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x000599B9 File Offset: 0x00057DB9
	public void UpdChar()
	{
		this.cFocus.cx = this.x;
		this.cFocus.cy = this.y;
	}

	// Token: 0x04000A45 RID: 2629
	public int x;

	// Token: 0x04000A46 RID: 2630
	public int y;

	// Token: 0x04000A47 RID: 2631
	public int xTo = -999;

	// Token: 0x04000A48 RID: 2632
	public int yTo = -999;

	// Token: 0x04000A49 RID: 2633
	public int count;

	// Token: 0x04000A4A RID: 2634
	public int vy;

	// Token: 0x04000A4B RID: 2635
	public int vx;

	// Token: 0x04000A4C RID: 2636
	public int dir;

	// Token: 0x04000A4D RID: 2637
	public int idImg;

	// Token: 0x04000A4E RID: 2638
	public bool isPaint = true;

	// Token: 0x04000A4F RID: 2639
	public bool isDone;

	// Token: 0x04000A50 RID: 2640
	public bool isSetImg;

	// Token: 0x04000A51 RID: 2641
	public global::Char cFocus;
}
