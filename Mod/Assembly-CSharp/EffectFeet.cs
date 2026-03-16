using System;

// Token: 0x0200003D RID: 61
public class EffectFeet : Effect2
{
	// Token: 0x06000282 RID: 642 RVA: 0x00013C98 File Offset: 0x00012098
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

	// Token: 0x06000283 RID: 643 RVA: 0x00013CE7 File Offset: 0x000120E7
	public override void update()
	{
		if (mSystem.currentTimeMillis() - this.endTime > 0L)
		{
			Effect2.vEffectFeet.removeElement(this);
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00013D08 File Offset: 0x00012108
	public override void paint(mGraphics g)
	{
		int num = (int)TileMap.size;
		if (TileMap.tileTypeAt(this.x + num / 2, this.y + 1, 4))
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt((this.x - num / 2) / num, (this.y + 1) / num) == 0)
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, 100, 100);
		}
		else if (TileMap.tileTypeAt((this.x + num / 2) / num, (this.y + 1) / num) == 0)
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt(this.x - num / 2, this.y + 1, 8))
		{
			g.setClip(this.x / 24 * num, (this.y - 30) / num * num, num, 100);
		}
		g.drawRegion((!this.isF) ? EffectFeet.imgFeet3 : EffectFeet.imgFeet1, 0, 0, EffectFeet.imgFeet1.getWidth(), EffectFeet.imgFeet1.getHeight(), this.trans, this.x, this.y, mGraphics.BOTTOM | mGraphics.HCENTER);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x040002E6 RID: 742
	private int x;

	// Token: 0x040002E7 RID: 743
	private int y;

	// Token: 0x040002E8 RID: 744
	private int trans;

	// Token: 0x040002E9 RID: 745
	private long endTime;

	// Token: 0x040002EA RID: 746
	private bool isF;

	// Token: 0x040002EB RID: 747
	public static Image imgFeet1 = GameCanvas.loadImage("/mainImage/myTexture2dmove-1.png");

	// Token: 0x040002EC RID: 748
	public static Image imgFeet3 = GameCanvas.loadImage("/mainImage/myTexture2dmove-3.png");
}
