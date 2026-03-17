using System;

// Token: 0x02000035 RID: 53
public class FrameImage
{
	// Token: 0x06000287 RID: 647 RVA: 0x0003BCFC File Offset: 0x00039EFC
	public FrameImage(int ID)
	{
		this.Id = ID;
		Image image = Effect_End.getImage(ID);
		bool flag = image != null;
		if (flag)
		{
			this.imgFrame = image;
			this.frameWidth = (int)Effect_End.arrInfoEff[ID][0];
			this.frameHeight = (int)(Effect_End.arrInfoEff[ID][1] / Effect_End.arrInfoEff[ID][2]);
			this.nFrame = (int)Effect_End.arrInfoEff[ID][2];
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0003BD70 File Offset: 0x00039F70
	public FrameImage(Image img, int width, int height)
	{
		bool flag = img != null;
		if (flag)
		{
			this.imgFrame = img;
			this.frameWidth = width;
			this.frameHeight = height;
			this.nFrame = img.getHeight() / height;
			bool flag2 = this.nFrame < 1;
			if (flag2)
			{
				this.nFrame = 1;
			}
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0003BDD0 File Offset: 0x00039FD0
	public FrameImage(Image img, int numW, int numH, int numNull)
	{
		bool flag = img != null;
		if (flag)
		{
			this.imgFrame = img;
			this.numWidth = numW;
			this.numHeight = numH;
			this.frameWidth = this.imgFrame.getWidth() / numW;
			this.frameHeight = this.imgFrame.getHeight() / numH;
			this.nFrame = numW * numH - numNull;
		}
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0003BE40 File Offset: 0x0003A040
	public void drawFrame(int idx, int x, int y, int trans, int anchor, mGraphics g)
	{
		try
		{
			bool flag = this.imgFrame != null;
			if (flag)
			{
				bool flag2 = idx > this.nFrame;
				if (flag2)
				{
					idx = this.nFrame;
				}
				int num = idx * this.frameHeight;
				bool flag3 = num > this.frameHeight * (this.nFrame - 1) || num < 0;
				if (flag3)
				{
					num = this.frameHeight * (this.nFrame - 1);
				}
				g.drawRegion(this.imgFrame, 0, num, this.frameWidth, this.frameHeight, trans, x, y, anchor);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x040005BF RID: 1471
	public int frameWidth;

	// Token: 0x040005C0 RID: 1472
	public int frameHeight;

	// Token: 0x040005C1 RID: 1473
	public int nFrame;

	// Token: 0x040005C2 RID: 1474
	public Image imgFrame;

	// Token: 0x040005C3 RID: 1475
	public int Id = -1;

	// Token: 0x040005C4 RID: 1476
	public int numWidth;

	// Token: 0x040005C5 RID: 1477
	public int numHeight;
}
