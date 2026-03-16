using System;

// Token: 0x0200002D RID: 45
public class mLine
{
	// Token: 0x060001E6 RID: 486 RVA: 0x0000DB39 File Offset: 0x0000BF39
	public mLine(int x1, int y1, int x2, int y2, int cl)
	{
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.setColor(cl);
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000DB68 File Offset: 0x0000BF68
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

	// Token: 0x040001D9 RID: 473
	public int x1;

	// Token: 0x040001DA RID: 474
	public int x2;

	// Token: 0x040001DB RID: 475
	public int y1;

	// Token: 0x040001DC RID: 476
	public int y2;

	// Token: 0x040001DD RID: 477
	public float r;

	// Token: 0x040001DE RID: 478
	public float b;

	// Token: 0x040001DF RID: 479
	public float g;

	// Token: 0x040001E0 RID: 480
	public float a;
}
