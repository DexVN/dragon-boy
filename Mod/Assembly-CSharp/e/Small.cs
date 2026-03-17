using System;

namespace Assets.src.e
{
	// Token: 0x020000C6 RID: 198
	public class Small
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x000AF8F8 File Offset: 0x000ADAF8
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000AF920 File Offset: 0x000ADB20
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			bool flag = GameCanvas.gameTick % 1000 == 0;
			if (flag)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000AF988 File Offset: 0x000ADB88
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000AF9AC File Offset: 0x000ADBAC
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			bool flag = mGraphics.getImageWidth(this.img) == 1;
			if (!flag)
			{
				g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
				bool flag2 = GameCanvas.gameTick % 1000 == 0;
				if (flag2)
				{
					this.timePaint++;
					this.timeUpdate = this.timePaint;
				}
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000AFA1C File Offset: 0x000ADC1C
		public void update()
		{
			this.timeUpdate++;
			bool flag = this.timeUpdate - this.timePaint > 1 && !global::Char.myCharz().isCharBodyImageID(this.id);
			if (flag)
			{
				SmallImage.imgNew[this.id] = null;
			}
		}

		// Token: 0x040013FB RID: 5115
		public Image img;

		// Token: 0x040013FC RID: 5116
		public int id;

		// Token: 0x040013FD RID: 5117
		public int timePaint;

		// Token: 0x040013FE RID: 5118
		public int timeUpdate;
	}
}
