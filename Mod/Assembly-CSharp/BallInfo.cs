using System;

// Token: 0x0200000B RID: 11
public class BallInfo
{
	// Token: 0x0600005E RID: 94 RVA: 0x00005F44 File Offset: 0x00004144
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

	// Token: 0x0600005F RID: 95 RVA: 0x00005FD2 File Offset: 0x000041D2
	public void UpdChar()
	{
		this.cFocus.cx = this.x;
		this.cFocus.cy = this.y;
	}

	// Token: 0x04000096 RID: 150
	public int x;

	// Token: 0x04000097 RID: 151
	public int y;

	// Token: 0x04000098 RID: 152
	public int xTo = -999;

	// Token: 0x04000099 RID: 153
	public int yTo = -999;

	// Token: 0x0400009A RID: 154
	public int count;

	// Token: 0x0400009B RID: 155
	public int vy;

	// Token: 0x0400009C RID: 156
	public int vx;

	// Token: 0x0400009D RID: 157
	public int dir;

	// Token: 0x0400009E RID: 158
	public int idImg;

	// Token: 0x0400009F RID: 159
	public bool isPaint = true;

	// Token: 0x040000A0 RID: 160
	public bool isDone;

	// Token: 0x040000A1 RID: 161
	public bool isSetImg;

	// Token: 0x040000A2 RID: 162
	public global::Char cFocus;
}
