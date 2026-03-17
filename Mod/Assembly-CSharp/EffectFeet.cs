using System;

// Token: 0x02000029 RID: 41
public class EffectFeet : Effect2
{
	// Token: 0x06000223 RID: 547 RVA: 0x00036F4C File Offset: 0x0003514C
	public static void addFeet(int cx, int cy, int ctrans, int timeLengthInSecond, bool isCF)
	{
		EffectFeet effectFeet = new EffectFeet();
		effectFeet.x = cx;
		effectFeet.y = cy;
		effectFeet.trans = ctrans;
		effectFeet.isF = isCF;
		effectFeet.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffectFeet.addElement(effectFeet);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00036FA0 File Offset: 0x000351A0
	public override void update()
	{
		bool flag = mSystem.currentTimeMillis() - this.endTime > 0L;
		if (flag)
		{
			Effect2.vEffectFeet.removeElement(this);
		}
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00036FD0 File Offset: 0x000351D0
	public override void paint(mGraphics g)
	{
		int num = (int)TileMap.size;
		bool flag = TileMap.tileTypeAt(this.x + num / 2, this.y + 1, 4);
		if (flag)
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, num, 100);
		}
		else
		{
			bool flag2 = TileMap.tileTypeAt((this.x - num / 2) / num, (this.y + 1) / num) == 0;
			if (flag2)
			{
				g.setClip(this.x / num * num, (this.y - 30) / num * num, 100, 100);
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt((this.x + num / 2) / num, (this.y + 1) / num) == 0;
				if (flag3)
				{
					g.setClip(this.x / num * num, (this.y - 30) / num * num, num, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt(this.x - num / 2, this.y + 1, 8);
					if (flag4)
					{
						g.setClip(this.x / 24 * num, (this.y - 30) / num * num, num, 100);
					}
				}
			}
		}
		g.drawRegion((!this.isF) ? EffectFeet.imgFeet3 : EffectFeet.imgFeet1, 0, 0, EffectFeet.imgFeet1.getWidth(), EffectFeet.imgFeet1.getHeight(), this.trans, this.x, this.y, mGraphics.BOTTOM | mGraphics.HCENTER);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x04000510 RID: 1296
	private int x;

	// Token: 0x04000511 RID: 1297
	private int y;

	// Token: 0x04000512 RID: 1298
	private int trans;

	// Token: 0x04000513 RID: 1299
	private long endTime;

	// Token: 0x04000514 RID: 1300
	private bool isF;

	// Token: 0x04000515 RID: 1301
	public static Image imgFeet1 = GameCanvas.loadImage("/mainImage/myTexture2dmove-1.png");

	// Token: 0x04000516 RID: 1302
	public static Image imgFeet3 = GameCanvas.loadImage("/mainImage/myTexture2dmove-3.png");
}
