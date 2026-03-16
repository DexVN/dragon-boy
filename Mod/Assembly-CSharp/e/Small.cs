using System;

namespace Assets.src.e
{
	// Token: 0x02000086 RID: 134
	public class Small
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00034D85 File Offset: 0x00033185
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00034DAC File Offset: 0x000331AC
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00034E10 File Offset: 0x00033210
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00034E34 File Offset: 0x00033234
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			if (mGraphics.getImageWidth(this.img) == 1)
			{
				return;
			}
			g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00034E9C File Offset: 0x0003329C
		public void update()
		{
			this.timeUpdate++;
			if (this.timeUpdate - this.timePaint > 1 && !global::Char.myCharz().isCharBodyImageID(this.id))
			{
				SmallImage.imgNew[this.id] = null;
			}
		}

		// Token: 0x040007B4 RID: 1972
		public Image img;

		// Token: 0x040007B5 RID: 1973
		public int id;

		// Token: 0x040007B6 RID: 1974
		public int timePaint;

		// Token: 0x040007B7 RID: 1975
		public int timeUpdate;
	}
}
