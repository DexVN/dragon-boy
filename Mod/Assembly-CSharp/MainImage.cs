using System;

// Token: 0x02000062 RID: 98
public class MainImage
{
	// Token: 0x060004D7 RID: 1239 RVA: 0x0005D91D File Offset: 0x0005BB1D
	public MainImage()
	{
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0005D941 File Offset: 0x0005BB41
	public MainImage(Image im, sbyte nFrame)
	{
		this.img = im;
		this.count = 0L;
		this.nFrame = nFrame;
	}

	// Token: 0x04000A8D RID: 2701
	public Image img;

	// Token: 0x04000A8E RID: 2702
	public long count = -1L;

	// Token: 0x04000A8F RID: 2703
	public int timeImageNull;

	// Token: 0x04000A90 RID: 2704
	public int idImage;

	// Token: 0x04000A91 RID: 2705
	public long timerequest;

	// Token: 0x04000A92 RID: 2706
	public sbyte nFrame = 1;

	// Token: 0x04000A93 RID: 2707
	public long timeUse = mSystem.currentTimeMillis();
}
