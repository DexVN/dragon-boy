using System;

// Token: 0x0200006F RID: 111
public class MonsterDart : Effect2
{
	// Token: 0x06000590 RID: 1424 RVA: 0x00066BA4 File Offset: 0x00064DA4
	public MonsterDart(int x, int y, bool isBoss, int dame, int dameMp, global::Char c, int dartType)
	{
		this.info = GameScr.darts[dartType];
		this.x = x;
		this.y = y;
		this.isBoss = isBoss;
		this.dame = dame;
		this.dameMp = dameMp;
		this.c = c;
		this.va = this.info.va;
		this.setAngle(Res.angle(c.cx - x, c.cy - y));
		bool flag = x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			SoundMn.gI().mobKame(dartType);
		}
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00066C60 File Offset: 0x00064E60
	public MonsterDart(int x, int y, bool isBoss, int dame, int dameMp, int xTo, int yTo, int dartType)
	{
		this.info = GameScr.darts[dartType];
		this.x = x;
		this.y = y;
		this.isBoss = isBoss;
		this.dame = dame;
		this.dameMp = dameMp;
		this.xTo = xTo;
		this.yTo = yTo;
		this.va = this.info.va;
		this.setAngle(Res.angle(xTo - x, yTo - y));
		bool flag = x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			SoundMn.gI().mobKame(dartType);
		}
		this.c = null;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00066D1E File Offset: 0x00064F1E
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00066D54 File Offset: 0x00064F54
	public static void addMonsterDart(int x, int y, bool isBoss, int dame, int dameMp, global::Char c, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00066D74 File Offset: 0x00064F74
	public static void addMonsterDart(int x, int y, bool isBoss, int dame, int dameMp, int xTo, int yTo, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, xTo, yTo, dartType));
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00066DA0 File Offset: 0x00064FA0
	public override void update()
	{
		for (int i = 0; i < (int)this.info.nUpdate; i++)
		{
			bool flag = this.info.tail.Length != 0;
			if (flag)
			{
				this.darts.addElement(new SmallDart(this.x, this.y));
			}
			this.dx = ((this.c == null) ? this.xTo : this.c.cx) - this.x;
			this.dy = ((this.c == null) ? this.yTo : this.c.cy) - 10 - this.y;
			int num = 60;
			bool flag2 = TileMap.mapID == 0;
			if (flag2)
			{
				num = 600;
			}
			this.life++;
			bool flag3 = (this.c != null && (this.c.statusMe == 5 || this.c.statusMe == 14)) || this.c == null;
			if (flag3)
			{
				this.x += (((this.c == null) ? this.xTo : this.c.cx) - this.x) / 2;
				this.y += (((this.c == null) ? this.yTo : this.c.cy) - this.y) / 2;
			}
			bool flag4 = (Res.abs(this.dx) < 16 && Res.abs(this.dy) < 16) || this.life > num;
			if (flag4)
			{
				bool flag5 = this.c != null && this.c.charID >= 0 && this.dameMp != -1;
				if (flag5)
				{
					bool flag6 = this.dameMp != -100;
					if (flag6)
					{
						this.c.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						ServerEffect.addServerEffect(80, this.c, 1);
					}
				}
				Effect2.vEffect2.removeElement(this);
				bool flag7 = this.dameMp != -100;
				if (flag7)
				{
					ServerEffect.addServerEffect(81, this.c, 1);
					bool flag8 = this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w;
					if (flag8)
					{
						SoundMn.gI().explode_2();
					}
				}
			}
			int num2 = Res.angle(this.dx, this.dy);
			bool flag9 = global::Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096;
			if (flag9)
			{
				bool flag10 = global::Math.abs(num2 - this.angle) < 15;
				if (flag10)
				{
					this.angle = num2;
				}
				else
				{
					bool flag11 = (num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180;
					if (flag11)
					{
						this.angle = Res.fixangle(this.angle + 15);
					}
					else
					{
						this.angle = Res.fixangle(this.angle - 15);
					}
				}
			}
			bool flag12 = !this.isSpeedUp && this.va < 8192;
			if (flag12)
			{
				this.va += 1024;
			}
			this.vx = this.va * Res.cos(this.angle) >> 10;
			this.vy = this.va * Res.sin(this.angle) >> 10;
			this.dx += this.vx;
			int num3 = this.dx >> 10;
			this.x += num3;
			this.dx &= 1023;
			this.dy += this.vy;
			int num4 = this.dy >> 10;
			this.y += num4;
			this.dy &= 1023;
		}
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
			smallDart.index++;
			bool flag13 = smallDart.index >= this.info.tail.Length;
			if (flag13)
			{
				this.darts.removeElementAt(j);
			}
		}
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00067258 File Offset: 0x00065458
	public static int findDirIndexFromAngle(int angle)
	{
		for (int i = 0; i < MonsterDart.ARROWINDEX.Length - 1; i++)
		{
			bool flag = angle >= MonsterDart.ARROWINDEX[i] && angle <= MonsterDart.ARROWINDEX[i + 1];
			if (flag)
			{
				bool flag2 = i >= 16;
				int result;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = i;
				}
				return result;
			}
		}
		return 0;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x000672C0 File Offset: 0x000654C0
	public override void paint(mGraphics g)
	{
		int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
		int num2 = (int)MonsterDart.FRAME[num];
		int transform = MonsterDart.TRANSFORM[num];
		for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
			SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
		}
		int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
		SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
			SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
		}
		SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
		for (int k = 0; k < this.darts.size(); k++)
		{
			SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
			bool flag = Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent;
			if (flag)
			{
				SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
			}
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x000674C2 File Offset: 0x000656C2
	public static void addMonsterDart(int x2, int y2, bool checkIsBoss, int dame2, int dameMp2, Mob mobToAttack, sbyte dartType)
	{
		MonsterDart.addMonsterDart(x2, y2, checkIsBoss, dame2, dameMp2, mobToAttack.x, mobToAttack.y, (int)dartType);
	}

	// Token: 0x04000BEB RID: 3051
	public int va;

	// Token: 0x04000BEC RID: 3052
	private DartInfo info;

	// Token: 0x04000BED RID: 3053
	public static MyRandom r = new MyRandom();

	// Token: 0x04000BEE RID: 3054
	public int angle;

	// Token: 0x04000BEF RID: 3055
	public int vx;

	// Token: 0x04000BF0 RID: 3056
	public int vy;

	// Token: 0x04000BF1 RID: 3057
	public int x;

	// Token: 0x04000BF2 RID: 3058
	public int y;

	// Token: 0x04000BF3 RID: 3059
	public int z;

	// Token: 0x04000BF4 RID: 3060
	public int xTo;

	// Token: 0x04000BF5 RID: 3061
	public int yTo;

	// Token: 0x04000BF6 RID: 3062
	private int life;

	// Token: 0x04000BF7 RID: 3063
	public bool isSpeedUp;

	// Token: 0x04000BF8 RID: 3064
	public int dame;

	// Token: 0x04000BF9 RID: 3065
	public int dameMp;

	// Token: 0x04000BFA RID: 3066
	public global::Char c;

	// Token: 0x04000BFB RID: 3067
	public bool isBoss;

	// Token: 0x04000BFC RID: 3068
	public MyVector darts = new MyVector();

	// Token: 0x04000BFD RID: 3069
	private int dx;

	// Token: 0x04000BFE RID: 3070
	private int dy;

	// Token: 0x04000BFF RID: 3071
	public static int[] ARROWINDEX = new int[]
	{
		0,
		15,
		37,
		52,
		75,
		105,
		127,
		142,
		165,
		195,
		217,
		232,
		255,
		285,
		307,
		322,
		345,
		370
	};

	// Token: 0x04000C00 RID: 3072
	public static int[] TRANSFORM = new int[]
	{
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		2,
		2,
		3,
		3,
		4,
		5,
		5,
		5,
		1
	};

	// Token: 0x04000C01 RID: 3073
	public static sbyte[] FRAME = new sbyte[]
	{
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0
	};
}
