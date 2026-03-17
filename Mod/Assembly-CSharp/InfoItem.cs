using System;

// Token: 0x02000047 RID: 71
public class InfoItem
{
	// Token: 0x060003F5 RID: 1013 RVA: 0x00056B5D File Offset: 0x00054D5D
	public InfoItem(string s)
	{
		this.f = mFont.tahoma_7_green2;
		this.s = s;
		this.speed = 20;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00056B89 File Offset: 0x00054D89
	public InfoItem(string s, mFont f, int speed)
	{
		this.f = f;
		this.s = s;
		this.speed = speed;
	}

	// Token: 0x040008C0 RID: 2240
	public string s;

	// Token: 0x040008C1 RID: 2241
	private mFont f;

	// Token: 0x040008C2 RID: 2242
	public int speed = 70;

	// Token: 0x040008C3 RID: 2243
	public global::Char charInfo;

	// Token: 0x040008C4 RID: 2244
	public bool isChatServer;

	// Token: 0x040008C5 RID: 2245
	public bool isOnline;

	// Token: 0x040008C6 RID: 2246
	public int timeCount;

	// Token: 0x040008C7 RID: 2247
	public int maxTime;

	// Token: 0x040008C8 RID: 2248
	public long last;

	// Token: 0x040008C9 RID: 2249
	public long curr;
}
