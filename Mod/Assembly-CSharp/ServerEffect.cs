using System;

// Token: 0x02000097 RID: 151
public class ServerEffect : Effect2
{
	// Token: 0x06000832 RID: 2098 RVA: 0x00091784 File Offset: 0x0008F984
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x000917CC File Offset: 0x0008F9CC
	public static void addServerEffect(int id, int cx, int cy, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0009181C File Offset: 0x0008FA1C
	public static void addServerEffect(int id, Mob m, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.m = m;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0009185C File Offset: 0x0008FA5C
	public static void addServerEffect(int id, global::Char c, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0009189C File Offset: 0x0008FA9C
	public static void addServerEffect(int id, global::Char c, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x000918E4 File Offset: 0x0008FAE4
	public static void addServerEffectWithTime(int id, int cx, int cy, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00091938 File Offset: 0x0008FB38
	public static void addServerEffectWithTime(int id, global::Char c, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00091984 File Offset: 0x0008FB84
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
			bool flag5 = GameCanvas.isPaint(num, num2);
			if (flag5)
			{
				SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00091ABC File Offset: 0x0008FCBC
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
				Effect2.vEffect2.removeElement(this);
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
					Effect2.vEffect2.removeElement(this);
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
			Effect2.vEffect2.removeElement(this);
		}
	}

	// Token: 0x040010B0 RID: 4272
	public EffectCharPaint eff;

	// Token: 0x040010B1 RID: 4273
	private int i0;

	// Token: 0x040010B2 RID: 4274
	private int dx0;

	// Token: 0x040010B3 RID: 4275
	private int dy0;

	// Token: 0x040010B4 RID: 4276
	private int x;

	// Token: 0x040010B5 RID: 4277
	private int y;

	// Token: 0x040010B6 RID: 4278
	private global::Char c;

	// Token: 0x040010B7 RID: 4279
	private Mob m;

	// Token: 0x040010B8 RID: 4280
	private short loopCount;

	// Token: 0x040010B9 RID: 4281
	private long endTime;

	// Token: 0x040010BA RID: 4282
	private int trans;
}
