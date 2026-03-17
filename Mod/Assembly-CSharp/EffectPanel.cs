using System;

// Token: 0x0200002D RID: 45
public class EffectPanel : Effect2
{
	// Token: 0x06000236 RID: 566 RVA: 0x000373A4 File Offset: 0x000355A4
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		EffectPanel effectPanel = new EffectPanel();
		effectPanel.eff = GameScr.efs[id - 1];
		effectPanel.x = cx;
		effectPanel.y = cy;
		effectPanel.loopCount = (short)loopCount;
		Effect2.vEffect3.addElement(effectPanel);
	}

	// Token: 0x06000237 RID: 567 RVA: 0x000373EC File Offset: 0x000355EC
	public override void paint(mGraphics g)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			GameScr.countEff++;
		}
		bool flag2 = GameScr.countEff < 8;
		if (flag2)
		{
			bool flag3 = this.c != null;
			if (flag3)
			{
				this.x = this.c.cx;
				this.y = this.c.cy + GameCanvas.transY;
			}
			bool flag4 = this.m != null;
			if (flag4)
			{
				this.x = this.m.x;
				this.y = this.m.y + GameCanvas.transY;
			}
			int num = this.x + this.dx0 + this.eff.arrEfInfo[this.i0].dx;
			int num2 = this.y + this.dy0 + this.eff.arrEfInfo[this.i0].dy;
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00037514 File Offset: 0x00035714
	public override void update()
	{
		bool flag = this.endTime != 0L;
		if (flag)
		{
			this.i0++;
			bool flag2 = this.i0 >= this.eff.arrEfInfo.Length;
			if (flag2)
			{
				this.i0 = 0;
			}
			bool flag3 = mSystem.currentTimeMillis() - this.endTime > 0L;
			if (flag3)
			{
				Effect2.vEffect3.removeElement(this);
			}
		}
		else
		{
			this.i0++;
			bool flag4 = this.i0 >= this.eff.arrEfInfo.Length;
			if (flag4)
			{
				this.loopCount -= 1;
				bool flag5 = this.loopCount <= 0;
				if (flag5)
				{
					Effect2.vEffect3.removeElement(this);
				}
				else
				{
					this.i0 = 0;
				}
			}
		}
		bool flag6 = GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != global::Char.myCharz() && !GameScr.vCharInMap.contains(this.c);
		if (flag6)
		{
			Effect2.vEffect3.removeElement(this);
		}
	}

	// Token: 0x04000523 RID: 1315
	public EffectCharPaint eff;

	// Token: 0x04000524 RID: 1316
	private int i0;

	// Token: 0x04000525 RID: 1317
	private int dx0;

	// Token: 0x04000526 RID: 1318
	private int dy0;

	// Token: 0x04000527 RID: 1319
	private int x;

	// Token: 0x04000528 RID: 1320
	private int y;

	// Token: 0x04000529 RID: 1321
	private global::Char c;

	// Token: 0x0400052A RID: 1322
	private Mob m;

	// Token: 0x0400052B RID: 1323
	private short loopCount;

	// Token: 0x0400052C RID: 1324
	private long endTime;

	// Token: 0x0400052D RID: 1325
	private int trans;
}
