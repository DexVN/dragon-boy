using System;

// Token: 0x02000079 RID: 121
public class Point
{
	// Token: 0x06000412 RID: 1042 RVA: 0x00032828 File Offset: 0x00030C28
	public Point()
	{
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0003288C File Offset: 0x00030C8C
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x000328FC File Offset: 0x00030CFC
	public Point(int x, int y, int goc)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00032973 File Offset: 0x00030D73
	public void update()
	{
		this.f++;
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x000329A9 File Offset: 0x00030DA9
	public void update_not_f()
	{
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x000329D4 File Offset: 0x00030DD4
	public void paint(mGraphics g)
	{
		if (!this.isRemove)
		{
			int num = 0;
			if (this.isSmall && this.f >= this.fSmall)
			{
				num = 1;
			}
			Point.FraEffInMap[this.color].drawFrame(this.frame / 2 + num, this.x, this.y, this.dis, 3, g);
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00032A3C File Offset: 0x00030E3C
	public void updateInMap()
	{
		this.f++;
		if (this.maxframe > 1)
		{
			this.frame++;
			if (this.frame / 2 >= this.maxframe)
			{
				this.frame = 0;
			}
		}
		if (this.f >= this.fRe)
		{
			this.isRemove = true;
		}
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00032AA4 File Offset: 0x00030EA4
	public int setFrameAngle(int goc)
	{
		int result;
		if (goc <= 15 || goc > 345)
		{
			result = 12;
		}
		else
		{
			int num = (goc - 15) / 15 + 1;
			if (num > 24)
			{
				num = 24;
			}
			result = (int)this.mpaintone_Arrow[num];
		}
		return result;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00032AEC File Offset: 0x00030EEC
	public void create_Arrow(int vMax)
	{
		this.vMax = vMax;
		int dx = this.toX - this.x;
		int dy = this.toY - this.y;
		if (this.x > this.toX)
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

	// Token: 0x0600041B RID: 1051 RVA: 0x00032B5C File Offset: 0x00030F5C
	public void create_Speed(int dx, int dy)
	{
		int frameAngle = Res.angle(dx, dy);
		this.frame = this.setFrameAngle(frameAngle);
		int num = Res.getDistance(dx, dy) / this.vMax;
		if (num == 0)
		{
			num = 1;
		}
		int num2 = dx / num;
		int num3 = dy / num;
		if (num2 == 0 && dx < num)
		{
			num2 = ((dx >= 0) ? 1 : -1);
		}
		if (num3 == 0 && dy < num)
		{
			num3 = ((dy >= 0) ? 1 : -1);
		}
		if (Res.abs(num2) > Res.abs(dx))
		{
			num2 = dx;
		}
		if (Res.abs(num3) > Res.abs(dy))
		{
			num3 = dy;
		}
		this.vx = num2;
		this.vy = num3;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00032C10 File Offset: 0x00031010
	public void moveTo_xy(int toX, int toY)
	{
		int num = toX - this.x;
		int dy = toY - this.y;
		if (num > 1)
		{
			int frameAngle = Res.angle(num, dy);
			this.frame = this.setFrameAngle(frameAngle);
		}
		if (Res.abs(this.vx) > 0)
		{
			if (Res.abs(this.x - toX) < Res.abs(this.vx))
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
		if (Res.abs(this.vy) > 0)
		{
			if (Res.abs(this.y - toY) < Res.abs(this.vy))
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

	// Token: 0x0600041D RID: 1053 RVA: 0x00032D1C File Offset: 0x0003111C
	public void paint_Arrow(mGraphics g, FrameImage frm, int anchor, bool isCountFr)
	{
		if (frm == null)
		{
			return;
		}
		int num = frm.nFrame / 3;
		if (num < 1)
		{
			num = 1;
		}
		int num2 = 3;
		int num3;
		if (frm.nFrame > 3)
		{
			num3 = ((this.f / num2 % 2 != 0) ? 3 : 0);
		}
		else
		{
			num3 = this.f % num;
		}
		int idx = num * (int)this.mImageArrow[this.frame] + num3;
		if (frm.nFrame < 3)
		{
			idx = this.f / num2 % frm.nFrame;
		}
		if (isCountFr)
		{
			idx = this.f / num2 % frm.nFrame;
		}
		frm.drawFrame(idx, this.x, this.y, (int)this.mXoayArrow[this.frame], anchor, g);
	}

	// Token: 0x040006F7 RID: 1783
	public sbyte type;

	// Token: 0x040006F8 RID: 1784
	public int x;

	// Token: 0x040006F9 RID: 1785
	public int y;

	// Token: 0x040006FA RID: 1786
	public int g;

	// Token: 0x040006FB RID: 1787
	public int v;

	// Token: 0x040006FC RID: 1788
	public int vMax;

	// Token: 0x040006FD RID: 1789
	public int w;

	// Token: 0x040006FE RID: 1790
	public int h;

	// Token: 0x040006FF RID: 1791
	public int color;

	// Token: 0x04000700 RID: 1792
	public int limitY;

	// Token: 0x04000701 RID: 1793
	public int vx;

	// Token: 0x04000702 RID: 1794
	public int vy;

	// Token: 0x04000703 RID: 1795
	public int x2;

	// Token: 0x04000704 RID: 1796
	public int y2;

	// Token: 0x04000705 RID: 1797
	public int toX;

	// Token: 0x04000706 RID: 1798
	public int toY;

	// Token: 0x04000707 RID: 1799
	public int dis;

	// Token: 0x04000708 RID: 1800
	public int f;

	// Token: 0x04000709 RID: 1801
	public int ftam;

	// Token: 0x0400070A RID: 1802
	public int fRe;

	// Token: 0x0400070B RID: 1803
	public int frame;

	// Token: 0x0400070C RID: 1804
	public int maxframe;

	// Token: 0x0400070D RID: 1805
	public int fSmall;

	// Token: 0x0400070E RID: 1806
	public int goc;

	// Token: 0x0400070F RID: 1807
	public int gocT_Arc;

	// Token: 0x04000710 RID: 1808
	public int idir;

	// Token: 0x04000711 RID: 1809
	public int dirThrow;

	// Token: 0x04000712 RID: 1810
	public int dir;

	// Token: 0x04000713 RID: 1811
	public int dir_nguoc;

	// Token: 0x04000714 RID: 1812
	public int idSkill;

	// Token: 0x04000715 RID: 1813
	public int id;

	// Token: 0x04000716 RID: 1814
	public int levelPaint;

	// Token: 0x04000717 RID: 1815
	public int num_per_frame = 1;

	// Token: 0x04000718 RID: 1816
	public int life;

	// Token: 0x04000719 RID: 1817
	public int goc_Arc;

	// Token: 0x0400071A RID: 1818
	public int vx1000;

	// Token: 0x0400071B RID: 1819
	public int vy1000;

	// Token: 0x0400071C RID: 1820
	public int va;

	// Token: 0x0400071D RID: 1821
	public int x1000;

	// Token: 0x0400071E RID: 1822
	public int y1000;

	// Token: 0x0400071F RID: 1823
	public int vX1000;

	// Token: 0x04000720 RID: 1824
	public int vY1000;

	// Token: 0x04000721 RID: 1825
	public long time;

	// Token: 0x04000722 RID: 1826
	public long timecount;

	// Token: 0x04000723 RID: 1827
	public MyVector vecEffPoint;

	// Token: 0x04000724 RID: 1828
	public string name;

	// Token: 0x04000725 RID: 1829
	public string info;

	// Token: 0x04000726 RID: 1830
	public bool isRemove;

	// Token: 0x04000727 RID: 1831
	public bool isSmall;

	// Token: 0x04000728 RID: 1832
	public bool isPaint;

	// Token: 0x04000729 RID: 1833
	public bool isChange;

	// Token: 0x0400072A RID: 1834
	public static FrameImage[] FraEffInMap;

	// Token: 0x0400072B RID: 1835
	public FrameImage fraImgEff;

	// Token: 0x0400072C RID: 1836
	public FrameImage fraImgEff_2;

	// Token: 0x0400072D RID: 1837
	public short index;

	// Token: 0x0400072E RID: 1838
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

	// Token: 0x0400072F RID: 1839
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

	// Token: 0x04000730 RID: 1840
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
