using System;

namespace Assets.src.g
{
	// Token: 0x020000C2 RID: 194
	public class PetFollow
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x000AA17B File Offset: 0x000A837B
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000AA1BC File Offset: 0x000A83BC
		public void SetImg(int fimg, int[] frameNew, int wimg, int himg)
		{
			bool flag = fimg < 1;
			if (!flag)
			{
				this.fimg = fimg;
				this.frame = frameNew;
				this.wimg = wimg;
				this.himg = himg;
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000AA1F4 File Offset: 0x000A83F4
		public void paint(mGraphics g)
		{
			int w = 32;
			int h = 32;
			int num = (GameCanvas.gameTick % 10 <= 5) ? 0 : 1;
			bool flag = this.fimg > 0;
			if (flag)
			{
				w = this.wimg;
				h = this.himg;
				num = 0;
			}
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + num, w, h, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000AA270 File Offset: 0x000A8470
		public void update()
		{
			this.moveCamera();
			bool flag = GameCanvas.gameTick % 3 == 0;
			if (flag)
			{
				this.f = this.frame[this.count];
				this.count++;
			}
			bool flag2 = this.count >= this.frame.Length;
			if (flag2)
			{
				this.count = 0;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000AA2D6 File Offset: 0x000A84D6
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 1);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000AA300 File Offset: 0x000A8500
		public void moveCamera()
		{
			bool flag = this.cmy != this.cmtoY;
			if (flag)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			bool flag2 = this.cmx != this.cmtoX;
			if (flag2)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
		}

		// Token: 0x040013A2 RID: 5026
		public short smallID;

		// Token: 0x040013A3 RID: 5027
		public Info info = new Info();

		// Token: 0x040013A4 RID: 5028
		public int dir;

		// Token: 0x040013A5 RID: 5029
		public int f;

		// Token: 0x040013A6 RID: 5030
		public int tF;

		// Token: 0x040013A7 RID: 5031
		public int cmtoY;

		// Token: 0x040013A8 RID: 5032
		public int cmy;

		// Token: 0x040013A9 RID: 5033
		public int cmdy;

		// Token: 0x040013AA RID: 5034
		public int cmvy;

		// Token: 0x040013AB RID: 5035
		public int cmyLim;

		// Token: 0x040013AC RID: 5036
		public int cmtoX;

		// Token: 0x040013AD RID: 5037
		public int cmx;

		// Token: 0x040013AE RID: 5038
		public int cmdx;

		// Token: 0x040013AF RID: 5039
		public int cmvx;

		// Token: 0x040013B0 RID: 5040
		public int cmxLim;

		// Token: 0x040013B1 RID: 5041
		public int fimg = -1;

		// Token: 0x040013B2 RID: 5042
		public int wimg;

		// Token: 0x040013B3 RID: 5043
		public int himg;

		// Token: 0x040013B4 RID: 5044
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x040013B5 RID: 5045
		private int count;
	}
}
