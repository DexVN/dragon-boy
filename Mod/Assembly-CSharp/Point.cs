using System;

// Token: 0x0200008D RID: 141
public class Point
{
	// Token: 0x060007AF RID: 1967 RVA: 0x0008C78C File Offset: 0x0008A98C
	public Point()
	{
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0008C7F0 File Offset: 0x0008A9F0
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0008C864 File Offset: 0x0008AA64
	public Point(int x, int y, int goc)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0008C8DD File Offset: 0x0008AADD
	public void update()
	{
		this.f++;
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0008C914 File Offset: 0x0008AB14
	public void update_not_f()
	{
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0008C940 File Offset: 0x0008AB40
	public void paint(mGraphics g)
	{
		bool flag = !this.isRemove;
		if (flag)
		{
			int num = 0;
			bool flag2 = this.isSmall && this.f >= this.fSmall;
			if (flag2)
			{
				num = 1;
			}
			Point.FraEffInMap[this.color].drawFrame(this.frame / 2 + num, this.x, this.y, this.dis, 3, g);
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0008C9B4 File Offset: 0x0008ABB4
	public void updateInMap()
	{
		this.f++;
		bool flag = this.maxframe > 1;
		if (flag)
		{
			this.frame++;
			bool flag2 = this.frame / 2 >= this.maxframe;
			if (flag2)
			{
				this.frame = 0;
			}
		}
		bool flag3 = this.f >= this.fRe;
		if (flag3)
		{
			this.isRemove = true;
		}
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0008CA2C File Offset: 0x0008AC2C
	public int setFrameAngle(int goc)
	{
		bool flag = goc <= 15 || goc > 345;
		int result;
		if (flag)
		{
			result = 12;
		}
		else
		{
			int num = (goc - 15) / 15 + 1;
			bool flag2 = num > 24;
			if (flag2)
			{
				num = 24;
			}
			result = (int)this.mpaintone_Arrow[num];
		}
		return result;
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0008CA80 File Offset: 0x0008AC80
	public void create_Arrow(int vMax)
	{
		this.vMax = vMax;
		int dx = this.toX - this.x;
		int dy = this.toY - this.y;
		bool flag = this.x > this.toX;
		if (flag)
		{
			this.dir = 2;
			this.dir_nguoc = 0;
		}
		else
		{
			this.dir = 0;
			this.dir_nguoc = 2;
		}
		this.create_Speed(dx, dy);
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0008CAF0 File Offset: 0x0008ACF0
	public void create_Speed(int dx, int dy)
	{
		int frameAngle = Res.angle(dx, dy);
		this.frame = this.setFrameAngle(frameAngle);
		int num = Res.getDistance(dx, dy) / this.vMax;
		bool flag = num == 0;
		if (flag)
		{
			num = 1;
		}
		int num2 = dx / num;
		int num3 = dy / num;
		bool flag2 = num2 == 0 && dx < num;
		if (flag2)
		{
			num2 = ((dx >= 0) ? 1 : -1);
		}
		bool flag3 = num3 == 0 && dy < num;
		if (flag3)
		{
			num3 = ((dy >= 0) ? 1 : -1);
		}
		bool flag4 = Res.abs(num2) > Res.abs(dx);
		if (flag4)
		{
			num2 = dx;
		}
		bool flag5 = Res.abs(num3) > Res.abs(dy);
		if (flag5)
		{
			num3 = dy;
		}
		this.vx = num2;
		this.vy = num3;
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0008CBAC File Offset: 0x0008ADAC
	public void moveTo_xy(int toX, int toY)
	{
		int num = toX - this.x;
		int dy = toY - this.y;
		bool flag = num > 1;
		if (flag)
		{
			int frameAngle = Res.angle(num, dy);
			this.frame = this.setFrameAngle(frameAngle);
		}
		bool flag2 = Res.abs(this.vx) > 0;
		if (flag2)
		{
			bool flag3 = Res.abs(this.x - toX) < Res.abs(this.vx);
			if (flag3)
			{
				this.x = toX;
				this.vx = 0;
			}
			else
			{
				this.x += this.vx;
			}
		}
		else
		{
			this.x = toX;
			this.vx = 0;
		}
		bool flag4 = Res.abs(this.vy) > 0;
		if (flag4)
		{
			bool flag5 = Res.abs(this.y - toY) < Res.abs(this.vy);
			if (flag5)
			{
				this.y = toY;
				this.vy = 0;
			}
			else
			{
				this.y += this.vy;
			}
		}
		else
		{
			this.y = toY;
			this.vy = 0;
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0008CCCC File Offset: 0x0008AECC
	public void paint_Arrow(mGraphics g, FrameImage frm, int anchor, bool isCountFr)
	{
		bool flag = frm == null;
		if (!flag)
		{
			int num = frm.nFrame / 3;
			bool flag2 = num < 1;
			if (flag2)
			{
				num = 1;
			}
			int num2 = 3;
			bool flag3 = frm.nFrame > 3;
			int num3;
			if (flag3)
			{
				num3 = ((this.f / num2 % 2 != 0) ? 3 : 0);
			}
			else
			{
				num3 = this.f % num;
			}
			int idx = num * (int)this.mImageArrow[this.frame] + num3;
			bool flag4 = frm.nFrame < 3;
			if (flag4)
			{
				idx = this.f / num2 % frm.nFrame;
			}
			if (isCountFr)
			{
				idx = this.f / num2 % frm.nFrame;
			}
			frm.drawFrame(idx, this.x, this.y, (int)this.mXoayArrow[this.frame], anchor, g);
		}
	}

	// Token: 0x04000FE0 RID: 4064
	public sbyte type;

	// Token: 0x04000FE1 RID: 4065
	public int x;

	// Token: 0x04000FE2 RID: 4066
	public int y;

	// Token: 0x04000FE3 RID: 4067
	public int g;

	// Token: 0x04000FE4 RID: 4068
	public int v;

	// Token: 0x04000FE5 RID: 4069
	public int vMax;

	// Token: 0x04000FE6 RID: 4070
	public int w;

	// Token: 0x04000FE7 RID: 4071
	public int h;

	// Token: 0x04000FE8 RID: 4072
	public int color;

	// Token: 0x04000FE9 RID: 4073
	public int limitY;

	// Token: 0x04000FEA RID: 4074
	public int vx;

	// Token: 0x04000FEB RID: 4075
	public int vy;

	// Token: 0x04000FEC RID: 4076
	public int x2;

	// Token: 0x04000FED RID: 4077
	public int y2;

	// Token: 0x04000FEE RID: 4078
	public int toX;

	// Token: 0x04000FEF RID: 4079
	public int toY;

	// Token: 0x04000FF0 RID: 4080
	public int dis;

	// Token: 0x04000FF1 RID: 4081
	public int f;

	// Token: 0x04000FF2 RID: 4082
	public int ftam;

	// Token: 0x04000FF3 RID: 4083
	public int fRe;

	// Token: 0x04000FF4 RID: 4084
	public int frame;

	// Token: 0x04000FF5 RID: 4085
	public int maxframe;

	// Token: 0x04000FF6 RID: 4086
	public int fSmall;

	// Token: 0x04000FF7 RID: 4087
	public int goc;

	// Token: 0x04000FF8 RID: 4088
	public int gocT_Arc;

	// Token: 0x04000FF9 RID: 4089
	public int idir;

	// Token: 0x04000FFA RID: 4090
	public int dirThrow;

	// Token: 0x04000FFB RID: 4091
	public int dir;

	// Token: 0x04000FFC RID: 4092
	public int dir_nguoc;

	// Token: 0x04000FFD RID: 4093
	public int idSkill;

	// Token: 0x04000FFE RID: 4094
	public int id;

	// Token: 0x04000FFF RID: 4095
	public int levelPaint;

	// Token: 0x04001000 RID: 4096
	public int num_per_frame = 1;

	// Token: 0x04001001 RID: 4097
	public int life;

	// Token: 0x04001002 RID: 4098
	public int goc_Arc;

	// Token: 0x04001003 RID: 4099
	public int vx1000;

	// Token: 0x04001004 RID: 4100
	public int vy1000;

	// Token: 0x04001005 RID: 4101
	public int va;

	// Token: 0x04001006 RID: 4102
	public int x1000;

	// Token: 0x04001007 RID: 4103
	public int y1000;

	// Token: 0x04001008 RID: 4104
	public int vX1000;

	// Token: 0x04001009 RID: 4105
	public int vY1000;

	// Token: 0x0400100A RID: 4106
	public long time;

	// Token: 0x0400100B RID: 4107
	public long timecount;

	// Token: 0x0400100C RID: 4108
	public MyVector vecEffPoint;

	// Token: 0x0400100D RID: 4109
	public string name;

	// Token: 0x0400100E RID: 4110
	public string info;

	// Token: 0x0400100F RID: 4111
	public bool isRemove;

	// Token: 0x04001010 RID: 4112
	public bool isSmall;

	// Token: 0x04001011 RID: 4113
	public bool isPaint;

	// Token: 0x04001012 RID: 4114
	public bool isChange;

	// Token: 0x04001013 RID: 4115
	public static FrameImage[] FraEffInMap;

	// Token: 0x04001014 RID: 4116
	public FrameImage fraImgEff;

	// Token: 0x04001015 RID: 4117
	public FrameImage fraImgEff_2;

	// Token: 0x04001016 RID: 4118
	public short index;

	// Token: 0x04001017 RID: 4119
	public byte[] mpaintone_Arrow = new byte[]
	{
		12,
		11,
		10,
		9,
		8,
		7,
		6,
		5,
		4,
		3,
		2,
		1,
		0,
		23,
		22,
		21,
		20,
		19,
		18,
		17,
		16,
		15,
		14,
		13
	};

	// Token: 0x04001018 RID: 4120
	public byte[] mImageArrow = new byte[]
	{
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2
	};

	// Token: 0x04001019 RID: 4121
	public byte[] mXoayArrow = new byte[]
	{
		2,
		2,
		3,
		3,
		3,
		4,
		5,
		5,
		5,
		5,
		5,
		1,
		0,
		0,
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		6,
		6,
		2
	};
}
