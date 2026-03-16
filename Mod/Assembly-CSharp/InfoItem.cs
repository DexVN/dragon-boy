using System;

// Token: 0x020000AE RID: 174
public class InfoItem
{
	// Token: 0x060007C8 RID: 1992 RVA: 0x000707F3 File Offset: 0x0006EBF3
	public InfoItem(string s)
	{
		this.f = mFont.tahoma_7_green2;
		this.s = s;
		this.speed = 20;
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0007081D File Offset: 0x0006EC1D
	public InfoItem(string s, mFont f, int speed)
	{
		this.f = f;
		this.s = s;
		this.speed = speed;
	}

	// Token: 0x04000EB7 RID: 3767
	public string s;

	// Token: 0x04000EB8 RID: 3768
	private mFont f;

	// Token: 0x04000EB9 RID: 3769
	public int speed = 70;

	// Token: 0x04000EBA RID: 3770
	public global::Char charInfo;

	// Token: 0x04000EBB RID: 3771
	public bool isChatServer;

	// Token: 0x04000EBC RID: 3772
	public bool isOnline;

	// Token: 0x04000EBD RID: 3773
	public int timeCount;

	// Token: 0x04000EBE RID: 3774
	public int maxTime;

	// Token: 0x04000EBF RID: 3775
	public long last;

	// Token: 0x04000EC0 RID: 3776
	public long curr;
}
