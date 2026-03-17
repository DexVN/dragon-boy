using System;

// Token: 0x02000008 RID: 8
public class Arrowpaint
{
	// Token: 0x06000018 RID: 24 RVA: 0x000026E8 File Offset: 0x000008E8
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

	// Token: 0x06000019 RID: 25 RVA: 0x00002AFC File Offset: 0x00000CFC
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

	// Token: 0x0600001A RID: 26 RVA: 0x00002B88 File Offset: 0x00000D88
	public void paint(mGraphics g)
	{
		int dx = this.axTo - this.ax;
		int num = this.ayTo - this.ay;
		int num2 = Arrowpaint.findDirIndexFromAngle(Res.angle(dx, -num));
		SmallImage.drawSmallImage(g, this.imgId[(int)Arrowpaint.FRAME[num2]], this.ax, this.ay, Arrowpaint.TRANSFORM[num2], mGraphics.VCENTER | mGraphics.HCENTER);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002BF4 File Offset: 0x00000DF4
	public static int findDirIndexFromAngle(int angle)
	{
		for (int i = 0; i < Arrowpaint.ARROWINDEX.Length - 1; i++)
		{
			bool flag = angle >= Arrowpaint.ARROWINDEX[i] && angle <= Arrowpaint.ARROWINDEX[i + 1];
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

	// Token: 0x04000014 RID: 20
	public int id;

	// Token: 0x04000015 RID: 21
	public int life;

	// Token: 0x04000016 RID: 22
	public int ax;

	// Token: 0x04000017 RID: 23
	public int ay;

	// Token: 0x04000018 RID: 24
	public int axTo;

	// Token: 0x04000019 RID: 25
	public int ayTo;

	// Token: 0x0400001A RID: 26
	public int avx;

	// Token: 0x0400001B RID: 27
	public int avy;

	// Token: 0x0400001C RID: 28
	public int adx;

	// Token: 0x0400001D RID: 29
	public int ady;

	// Token: 0x0400001E RID: 30
	public global::Char charBelong;

	// Token: 0x0400001F RID: 31
	public int[] imgId = new int[3];

	// Token: 0x04000020 RID: 32
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

	// Token: 0x04000021 RID: 33
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

	// Token: 0x04000022 RID: 34
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
