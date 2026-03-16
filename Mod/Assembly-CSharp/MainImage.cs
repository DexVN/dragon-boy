using System;

// Token: 0x02000045 RID: 69
public class MainImage
{
	// Token: 0x060002A2 RID: 674 RVA: 0x00014B3E File Offset: 0x00012F3E
	public MainImage()
	{
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x00014B60 File Offset: 0x00012F60
	public MainImage(Image im, sbyte nFrame)
	{
		this.img = im;
		this.count = 0L;
		this.nFrame = nFrame;
	}

	// Token: 0x0400032E RID: 814
	public Image img;

	// Token: 0x0400032F RID: 815
	public long count = -1L;

	// Token: 0x04000330 RID: 816
	public int timeImageNull;

	// Token: 0x04000331 RID: 817
	public int idImage;

	// Token: 0x04000332 RID: 818
	public long timerequest;

	// Token: 0x04000333 RID: 819
	public sbyte nFrame = 1;

	// Token: 0x04000334 RID: 820
	public long timeUse = mSystem.currentTimeMillis();
}
