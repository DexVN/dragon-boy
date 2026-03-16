using System;

namespace Assets.src.g
{
	// Token: 0x020000BE RID: 190
	public class PetFollow
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x000918C2 File Offset: 0x0008FCC2
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00091900 File Offset: 0x0008FD00
		public void SetImg(int fimg, int[] frameNew, int wimg, int himg)
		{
			if (fimg < 1)
			{
				return;
			}
			this.fimg = fimg;
			this.frame = frameNew;
			this.wimg = wimg;
			this.himg = himg;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00091928 File Offset: 0x0008FD28
		public void paint(mGraphics g)
		{
			int w = 32;
			int h = 32;
			int num = (GameCanvas.gameTick % 10 <= 5) ? 0 : 1;
			if (this.fimg > 0)
			{
				w = this.wimg;
				h = this.himg;
				num = 0;
			}
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + num, w, h, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000919AC File Offset: 0x0008FDAC
		public void update()
		{
			this.moveCamera();
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.f = this.frame[this.count];
				this.count++;
			}
			if (this.count >= this.frame.Length)
			{
				this.count = 0;
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00091A06 File Offset: 0x0008FE06
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 1);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00091A34 File Offset: 0x0008FE34
		public void moveCamera()
		{
			if (this.cmy != this.cmtoY)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			if (this.cmx != this.cmtoX)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
		}

		// Token: 0x0400117B RID: 4475
		public short smallID;

		// Token: 0x0400117C RID: 4476
		public Info info = new Info();

		// Token: 0x0400117D RID: 4477
		public int dir;

		// Token: 0x0400117E RID: 4478
		public int f;

		// Token: 0x0400117F RID: 4479
		public int tF;

		// Token: 0x04001180 RID: 4480
		public int cmtoY;

		// Token: 0x04001181 RID: 4481
		public int cmy;

		// Token: 0x04001182 RID: 4482
		public int cmdy;

		// Token: 0x04001183 RID: 4483
		public int cmvy;

		// Token: 0x04001184 RID: 4484
		public int cmyLim;

		// Token: 0x04001185 RID: 4485
		public int cmtoX;

		// Token: 0x04001186 RID: 4486
		public int cmx;

		// Token: 0x04001187 RID: 4487
		public int cmdx;

		// Token: 0x04001188 RID: 4488
		public int cmvx;

		// Token: 0x04001189 RID: 4489
		public int cmxLim;

		// Token: 0x0400118A RID: 4490
		public int fimg = -1;

		// Token: 0x0400118B RID: 4491
		public int wimg;

		// Token: 0x0400118C RID: 4492
		public int himg;

		// Token: 0x0400118D RID: 4493
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x0400118E RID: 4494
		private int count;
	}
}
