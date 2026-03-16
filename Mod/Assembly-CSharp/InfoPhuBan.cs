using System;

// Token: 0x02000060 RID: 96
public class InfoPhuBan
{
	// Token: 0x06000375 RID: 885 RVA: 0x0001CF4C File Offset: 0x0001B34C
	public InfoPhuBan(int type_PB, short idmapPaint, string nameTeam1, string nameTeam2, int maxPoint, short timeSecond)
	{
		this.type_PB = type_PB;
		this.idmapPaint = idmapPaint;
		this.nameTeam1 = nameTeam1;
		this.nameTeam2 = nameTeam2;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
		this.maxPoint = maxPoint;
		if (this.maxPoint <= 0)
		{
			this.maxPoint = 1;
		}
		this.pointTeam1 = 0;
		this.pointTeam2 = 0;
		this.owner = 0;
		this.color_1 = 4;
		this.color_2 = 6;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001CFEB File Offset: 0x0001B3EB
	public void updateTime(int type_PB, short timeSecond)
	{
		this.type_PB = type_PB;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001D006 File Offset: 0x0001B406
	public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
	{
		this.type_PB = type_PB;
		this.pointTeam1 = pointTeam1;
		this.pointTeam2 = pointTeam2;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0001D01D File Offset: 0x0001B41D
	public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
	{
		this.type_PB = type_PB;
		this.lifeTeam1 = lifeTeam1;
		this.lifeTeam2 = lifeTeam2;
	}

	// Token: 0x04000579 RID: 1401
	public int type_PB;

	// Token: 0x0400057A RID: 1402
	public int maxPoint;

	// Token: 0x0400057B RID: 1403
	public int pointTeam1;

	// Token: 0x0400057C RID: 1404
	public int pointTeam2;

	// Token: 0x0400057D RID: 1405
	public int color_1;

	// Token: 0x0400057E RID: 1406
	public int color_2;

	// Token: 0x0400057F RID: 1407
	public int maxLife = 1;

	// Token: 0x04000580 RID: 1408
	public int lifeTeam1;

	// Token: 0x04000581 RID: 1409
	public int lifeTeam2;

	// Token: 0x04000582 RID: 1410
	public string nameTeam1;

	// Token: 0x04000583 RID: 1411
	public string nameTeam2;

	// Token: 0x04000584 RID: 1412
	public short idmapPaint;

	// Token: 0x04000585 RID: 1413
	public short timeSecond;

	// Token: 0x04000586 RID: 1414
	public short timepaintSecond;

	// Token: 0x04000587 RID: 1415
	public short maxtimeSecond = 1;

	// Token: 0x04000588 RID: 1416
	public byte owner;

	// Token: 0x04000589 RID: 1417
	public long timeStart;

	// Token: 0x0400058A RID: 1418
	public MyVector vecInfo = new MyVector("vecInfo chientruong");
}
