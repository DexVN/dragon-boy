using System;

// Token: 0x0200006A RID: 106
public class mLine
{
	// Token: 0x0600054F RID: 1359 RVA: 0x00062DE1 File Offset: 0x00060FE1
	public mLine(int x1, int y1, int x2, int y2, int cl)
	{
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.setColor(cl);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00062E14 File Offset: 0x00061014
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x04000B4C RID: 2892
	public int x1;

	// Token: 0x04000B4D RID: 2893
	public int x2;

	// Token: 0x04000B4E RID: 2894
	public int y1;

	// Token: 0x04000B4F RID: 2895
	public int y2;

	// Token: 0x04000B50 RID: 2896
	public float r;

	// Token: 0x04000B51 RID: 2897
	public float b;

	// Token: 0x04000B52 RID: 2898
	public float g;

	// Token: 0x04000B53 RID: 2899
	public float a;
}
