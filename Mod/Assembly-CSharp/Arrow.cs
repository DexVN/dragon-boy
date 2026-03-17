using System;

// Token: 0x02000007 RID: 7
public class Arrow
{
	// Token: 0x06000012 RID: 18 RVA: 0x00002103 File Offset: 0x00000303
	public Arrow(global::Char charBelong, Arrowpaint arrp)
	{
		this.charBelong = charBelong;
		this.arrp = arrp;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000211C File Offset: 0x0000031C
	public void update()
	{
		bool flag = this.charBelong.mobFocus == null && this.charBelong.charFocus == null;
		if (flag)
		{
			this.endMe();
		}
		else
		{
			bool flag2 = this.charBelong.mobFocus != null;
			if (flag2)
			{
				this.axTo = this.charBelong.mobFocus.x;
				this.ayTo = this.charBelong.mobFocus.y - this.charBelong.mobFocus.h / 4;
			}
			else
			{
				bool flag3 = this.charBelong.charFocus != null;
				if (flag3)
				{
					this.axTo = this.charBelong.charFocus.cx;
					this.ayTo = this.charBelong.charFocus.cy - this.charBelong.charFocus.ch / 4;
				}
			}
			int num = this.axTo - this.ax;
			int num2 = this.ayTo - this.ay;
			int num3 = 5;
			int num4 = 4;
			bool flag4 = num + num2 < 60;
			if (flag4)
			{
				num4 = 3;
			}
			else
			{
				bool flag5 = num + num2 < 30;
				if (flag5)
				{
					num4 = 2;
				}
			}
			bool flag6 = this.ax != this.axTo;
			if (flag6)
			{
				bool flag7 = num > 0 && num < num3;
				if (flag7)
				{
					this.ax = this.axTo;
				}
				else
				{
					bool flag8 = num < 0 && num > -num3;
					if (flag8)
					{
						this.ax = this.axTo;
					}
					else
					{
						this.avx = this.axTo - this.ax << 2;
						this.adx += this.avx;
						this.ax += this.adx >> num4;
						this.adx &= 15;
					}
				}
			}
			bool flag9 = this.ay != this.ayTo;
			if (flag9)
			{
				bool flag10 = num2 > 0 && num2 < num3;
				if (flag10)
				{
					this.ay = this.ayTo;
				}
				else
				{
					bool flag11 = num2 < 0 && num2 > -num3;
					if (flag11)
					{
						this.ay = this.ayTo;
					}
					else
					{
						this.avy = this.ayTo - this.ay << 2;
						this.ady += this.avy;
						this.ay += this.ady >> num4;
						this.ady &= 15;
					}
				}
			}
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			bool flag12 = this.charBelong.mobFocus != null;
			if (flag12)
			{
				num5 = this.axTo - this.charBelong.mobFocus.w / 4;
				num7 = this.axTo + this.charBelong.mobFocus.w / 4;
				num6 = this.ayTo - this.charBelong.mobFocus.h / 4;
				num8 = this.ayTo + this.charBelong.mobFocus.h / 4;
			}
			else
			{
				bool flag13 = this.charBelong.charFocus != null;
				if (flag13)
				{
					num5 = this.axTo - this.charBelong.charFocus.cw / 4;
					num7 = this.axTo + this.charBelong.charFocus.cw / 4;
					num6 = this.ayTo - this.charBelong.charFocus.ch / 4;
					num8 = this.ayTo + this.charBelong.charFocus.ch / 4;
				}
			}
			bool flag14 = this.life > 0;
			if (flag14)
			{
				this.life--;
			}
			bool flag15 = this.life == 0 || (this.ax >= num5 && this.ax <= num7 && this.ay >= num6 && this.ay <= num8);
			if (flag15)
			{
				this.endMe();
			}
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002530 File Offset: 0x00000730
	private void endMe()
	{
		this.charBelong.arr = null;
		this.ax = (this.ay = (this.axTo = (this.ayTo = (this.avx = (this.avy = (this.adx = (this.ady = 0)))))));
		this.charBelong.setAttack();
		bool me = this.charBelong.me;
		if (me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000025BC File Offset: 0x000007BC
	public void paint(mGraphics g)
	{
		int dx = this.axTo - this.ax;
		int num = this.ayTo - this.ay;
		int num2 = Arrow.findDirIndexFromAngle(Res.angle(dx, -num));
		SmallImage.drawSmallImage(g, this.arrp.imgId[(int)Arrow.FRAME[num2]], this.ax, this.ay, Arrow.TRANSFORM[num2], mGraphics.VCENTER | mGraphics.HCENTER);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000262C File Offset: 0x0000082C
	public static int findDirIndexFromAngle(int angle)
	{
		for (int i = 0; i < Arrow.ARROWINDEX.Length - 1; i++)
		{
			bool flag = angle >= Arrow.ARROWINDEX[i] && angle <= Arrow.ARROWINDEX[i + 1];
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

	// Token: 0x04000006 RID: 6
	public int life;

	// Token: 0x04000007 RID: 7
	public int ax;

	// Token: 0x04000008 RID: 8
	public int ay;

	// Token: 0x04000009 RID: 9
	public int axTo;

	// Token: 0x0400000A RID: 10
	public int ayTo;

	// Token: 0x0400000B RID: 11
	public int avx;

	// Token: 0x0400000C RID: 12
	public int avy;

	// Token: 0x0400000D RID: 13
	public int adx;

	// Token: 0x0400000E RID: 14
	public int ady;

	// Token: 0x0400000F RID: 15
	public global::Char charBelong;

	// Token: 0x04000010 RID: 16
	public Arrowpaint arrp;

	// Token: 0x04000011 RID: 17
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

	// Token: 0x04000012 RID: 18
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

	// Token: 0x04000013 RID: 19
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
}
