using System;

// Token: 0x02000090 RID: 144
public class Position
{
	// Token: 0x060007CE RID: 1998 RVA: 0x0008DD31 File Offset: 0x0008BF31
	public Position()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0008DD49 File Offset: 0x0008BF49
	public Position(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0008DD68 File Offset: 0x0008BF68
	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0008DD80 File Offset: 0x0008BF80
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0008DDB8 File Offset: 0x0008BFB8
	public int translate()
	{
		bool flag = this.x == (int)this.xTo && this.y == (int)this.yTo;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = global::Math.abs(((int)this.xTo - this.x) / 2) <= 1 && global::Math.abs(((int)this.yTo - this.y) / 2) <= 1;
			if (flag2)
			{
				this.x = (int)this.xTo;
				this.y = (int)this.yTo;
				result = 0;
			}
			else
			{
				bool flag3 = this.x != (int)this.xTo;
				if (flag3)
				{
					this.x += ((int)this.xTo - this.x) / 2;
				}
				bool flag4 = this.y != (int)this.yTo;
				if (flag4)
				{
					this.y += ((int)this.yTo - this.y) / 2;
				}
				bool flag5 = Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo) <= (int)(this.distant / 5);
				if (flag5)
				{
					result = 2;
				}
				else
				{
					result = 1;
				}
			}
		}
		return result;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0008DEEB File Offset: 0x0008C0EB
	public void update()
	{
		this.layer.update();
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0008DEFA File Offset: 0x0008C0FA
	public void paint(mGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x0400103A RID: 4154
	public int x;

	// Token: 0x0400103B RID: 4155
	public int y;

	// Token: 0x0400103C RID: 4156
	public int anchor;

	// Token: 0x0400103D RID: 4157
	public int g;

	// Token: 0x0400103E RID: 4158
	public int v;

	// Token: 0x0400103F RID: 4159
	public int w;

	// Token: 0x04001040 RID: 4160
	public int h;

	// Token: 0x04001041 RID: 4161
	public int color;

	// Token: 0x04001042 RID: 4162
	public int limitY;

	// Token: 0x04001043 RID: 4163
	public Layer layer;

	// Token: 0x04001044 RID: 4164
	public short yTo;

	// Token: 0x04001045 RID: 4165
	public short xTo;

	// Token: 0x04001046 RID: 4166
	public short distant;
}
