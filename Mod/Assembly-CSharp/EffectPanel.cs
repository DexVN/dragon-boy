using System;

// Token: 0x0200003E RID: 62
public class EffectPanel : Effect2
{
	// Token: 0x06000287 RID: 647 RVA: 0x00013ECC File Offset: 0x000122CC
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		EffectPanel effectPanel = new EffectPanel();
		effectPanel.eff = GameScr.efs[id - 1];
		effectPanel.x = cx;
		effectPanel.y = cy;
		effectPanel.loopCount = (short)loopCount;
		Effect2.vEffect3.addElement(effectPanel);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00013F10 File Offset: 0x00012310
	public override void paint(mGraphics g)
	{
		if (mGraphics.zoomLevel == 1)
		{
			GameScr.countEff++;
		}
		if (GameScr.countEff < 8)
		{
			if (this.c != null)
			{
				this.x = this.c.cx;
				this.y = this.c.cy + GameCanvas.transY;
			}
			if (this.m != null)
			{
				this.x = this.m.x;
				this.y = this.m.y + GameCanvas.transY;
			}
			int num = this.x + this.dx0 + this.eff.arrEfInfo[this.i0].dx;
			int num2 = this.y + this.dy0 + this.eff.arrEfInfo[this.i0].dy;
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00014024 File Offset: 0x00012424
	public override void update()
	{
		if (this.endTime != 0L)
		{
			this.i0++;
			if (this.i0 >= this.eff.arrEfInfo.Length)
			{
				this.i0 = 0;
			}
			if (mSystem.currentTimeMillis() - this.endTime > 0L)
			{
				Effect2.vEffect3.removeElement(this);
			}
		}
		else
		{
			this.i0++;
			if (this.i0 >= this.eff.arrEfInfo.Length)
			{
				this.loopCount -= 1;
				if (this.loopCount <= 0)
				{
					Effect2.vEffect3.removeElement(this);
				}
				else
				{
					this.i0 = 0;
				}
			}
		}
		if (GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != global::Char.myCharz() && !GameScr.vCharInMap.contains(this.c))
		{
			Effect2.vEffect3.removeElement(this);
		}
	}

	// Token: 0x040002ED RID: 749
	public EffectCharPaint eff;

	// Token: 0x040002EE RID: 750
	private int i0;

	// Token: 0x040002EF RID: 751
	private int dx0;

	// Token: 0x040002F0 RID: 752
	private int dy0;

	// Token: 0x040002F1 RID: 753
	private int x;

	// Token: 0x040002F2 RID: 754
	private int y;

	// Token: 0x040002F3 RID: 755
	private global::Char c;

	// Token: 0x040002F4 RID: 756
	private Mob m;

	// Token: 0x040002F5 RID: 757
	private short loopCount;

	// Token: 0x040002F6 RID: 758
	private long endTime;

	// Token: 0x040002F7 RID: 759
	private int trans;
}
