using System;

// Token: 0x02000049 RID: 73
public class InfoPhuBan
{
	// Token: 0x06000401 RID: 1025 RVA: 0x00057924 File Offset: 0x00055B24
	public InfoPhuBan(int type_PB, short idmapPaint, string nameTeam1, string nameTeam2, int maxPoint, short timeSecond)
	{
		this.type_PB = type_PB;
		this.idmapPaint = idmapPaint;
		this.nameTeam1 = nameTeam1;
		this.nameTeam2 = nameTeam2;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
		this.maxPoint = maxPoint;
		bool flag = this.maxPoint <= 0;
		if (flag)
		{
			this.maxPoint = 1;
		}
		this.pointTeam1 = 0;
		this.pointTeam2 = 0;
		this.owner = 0;
		this.color_1 = 4;
		this.color_2 = 6;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x000579CB File Offset: 0x00055BCB
	public void updateTime(int type_PB, short timeSecond)
	{
		this.type_PB = type_PB;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000579E7 File Offset: 0x00055BE7
	public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
	{
		this.type_PB = type_PB;
		this.pointTeam1 = pointTeam1;
		this.pointTeam2 = pointTeam2;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000579FF File Offset: 0x00055BFF
	public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
	{
		this.type_PB = type_PB;
		this.lifeTeam1 = lifeTeam1;
		this.lifeTeam2 = lifeTeam2;
	}

	// Token: 0x040008E1 RID: 2273
	public int type_PB;

	// Token: 0x040008E2 RID: 2274
	public int maxPoint;

	// Token: 0x040008E3 RID: 2275
	public int pointTeam1;

	// Token: 0x040008E4 RID: 2276
	public int pointTeam2;

	// Token: 0x040008E5 RID: 2277
	public int color_1;

	// Token: 0x040008E6 RID: 2278
	public int color_2;

	// Token: 0x040008E7 RID: 2279
	public int maxLife = 1;

	// Token: 0x040008E8 RID: 2280
	public int lifeTeam1;

	// Token: 0x040008E9 RID: 2281
	public int lifeTeam2;

	// Token: 0x040008EA RID: 2282
	public string nameTeam1;

	// Token: 0x040008EB RID: 2283
	public string nameTeam2;

	// Token: 0x040008EC RID: 2284
	public short idmapPaint;

	// Token: 0x040008ED RID: 2285
	public short timeSecond;

	// Token: 0x040008EE RID: 2286
	public short timepaintSecond;

	// Token: 0x040008EF RID: 2287
	public short maxtimeSecond = 1;

	// Token: 0x040008F0 RID: 2288
	public byte owner;

	// Token: 0x040008F1 RID: 2289
	public long timeStart;

	// Token: 0x040008F2 RID: 2290
	public MyVector vecInfo = new MyVector("vecInfo chientruong");
}
