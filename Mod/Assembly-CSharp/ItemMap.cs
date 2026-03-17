using System;

// Token: 0x02000054 RID: 84
public class ItemMap : IMapObject
{
	// Token: 0x0600046D RID: 1133 RVA: 0x000592CC File Offset: 0x000574CC
	public ItemMap(short itemMapID, short itemTemplateID, int x, int y, int xEnd, int yEnd)
	{
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		this.x = xEnd;
		this.y = y;
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - x >> 2;
		this.vy = 5;
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			this.playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00059368 File Offset: 0x00057568
	public ItemMap(int playerId, short itemMapID, short itemTemplateID, int x, int y, short r)
	{
		Res.outz(string.Concat(new object[]
		{
			"item map item= ",
			itemMapID,
			" template= ",
			itemTemplateID,
			" x= ",
			x,
			" y= ",
			y
		}));
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
		this.xEnd = x;
		this.x = x;
		this.yEnd = y;
		this.y = y;
		this.status = 1;
		this.playerId = playerId;
		bool flag = this.isAuraItem();
		if (flag)
		{
			this.rO = (int)r;
			this.setAuraItem();
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0005946C File Offset: 0x0005766C
	public void setPoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - this.x >> 2;
		this.vy = yEnd - this.y >> 2;
		this.status = 2;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x000594A4 File Offset: 0x000576A4
	public void update()
	{
		bool flag = this.status == 2 && this.x == this.xEnd && this.y == this.yEnd;
		if (flag)
		{
			GameScr.vItemMap.removeElement(this);
			bool flag2 = global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this);
			if (flag2)
			{
				global::Char.myCharz().itemFocus = null;
			}
		}
		else
		{
			bool flag3 = this.status > 0;
			if (flag3)
			{
				bool flag4 = this.vx == 0;
				if (flag4)
				{
					this.x = this.xEnd;
				}
				bool flag5 = this.vy == 0;
				if (flag5)
				{
					this.y = this.yEnd;
				}
				bool flag6 = this.x != this.xEnd;
				if (flag6)
				{
					this.x += this.vx;
					bool flag7 = (this.vx > 0 && this.x > this.xEnd) || (this.vx < 0 && this.x < this.xEnd);
					if (flag7)
					{
						this.x = this.xEnd;
					}
				}
				bool flag8 = this.y != this.yEnd;
				if (flag8)
				{
					this.y += this.vy;
					bool flag9 = (this.vy > 0 && this.y > this.yEnd) || (this.vy < 0 && this.y < this.yEnd);
					if (flag9)
					{
						this.y = this.yEnd;
					}
				}
			}
			else
			{
				this.status -= 4;
				bool flag10 = this.status < -12;
				if (flag10)
				{
					this.y -= 12;
					this.status = 1;
				}
			}
			bool flag11 = this.isAuraItem();
			if (flag11)
			{
				this.updateAuraItemEff();
			}
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x000596A4 File Offset: 0x000578A4
	public void paint(mGraphics g)
	{
		bool flag = this.isAuraItem();
		if (flag)
		{
			g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
			bool flag2 = this.status <= 0;
			if (flag2)
			{
				bool flag3 = this.countAura < 10;
				if (flag3)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			else
			{
				bool flag4 = this.countAura < 10;
				if (flag4)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
		else
		{
			bool flag5 = !this.isAuraItem();
			if (flag5)
			{
				bool flag6 = GameCanvas.gameTick % 4 == 0;
				if (flag6)
				{
					g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				bool flag7 = this.status <= 0;
				if (flag7)
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				bool flag8 = global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this) && this.status != 2;
				if (flag8)
				{
					g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
				}
			}
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x000598EC File Offset: 0x00057AEC
	private bool isAuraItem()
	{
		return this.template.type == 22;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00059910 File Offset: 0x00057B10
	private void setAuraItem()
	{
		this.xO = this.x;
		this.yO = this.y;
		this.iDot = 120;
		this.angle = 0;
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			this.iAngle = 360 / this.iDot;
			this.xArg = new int[this.iDot];
			this.yArg = new int[this.iDot];
			this.xDot = new int[this.iDot];
			this.yDot = new int[this.iDot];
			this.setDotPosition();
		}
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x000599B0 File Offset: 0x00057BB0
	private void updateAuraItemEff()
	{
		this.count++;
		this.countAura++;
		bool flag = this.countAura >= 40;
		if (flag)
		{
			this.countAura = 0;
		}
		bool flag2 = this.count >= this.iDot;
		if (flag2)
		{
			this.count = 0;
		}
		bool flag3 = this.count % 10 == 0 && !GameCanvas.lowGraphic;
		if (flag3)
		{
			ServerEffect.addServerEffect(114, this.x - 5, this.y - 30, 1);
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00059A48 File Offset: 0x00057C48
	public void paintAuraItemEff(mGraphics g)
	{
		bool flag = !GameCanvas.lowGraphic && this.isAuraItem();
		if (flag)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				bool flag2 = this.count == i;
				if (flag2)
				{
					bool flag3 = this.countAura <= 20;
					if (flag3)
					{
						g.drawImage(ItemMap.imageAuraItem3, this.xDot[i], this.yDot[i] + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.template.iconID, this.xDot[i], this.yDot[i] + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00059B10 File Offset: 0x00057D10
	private void setDotPosition()
	{
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				bool flag2 = this.angle < 90;
				if (flag2)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else
				{
					bool flag3 = this.angle >= 90 && this.angle < 180;
					if (flag3)
					{
						this.xDot[i] = this.xO - this.xArg[i];
						this.yDot[i] = this.yO - this.yArg[i];
					}
					else
					{
						bool flag4 = this.angle >= 180 && this.angle < 270;
						if (flag4)
						{
							this.xDot[i] = this.xO - this.xArg[i];
							this.yDot[i] = this.yO + this.yArg[i];
						}
						else
						{
							this.xDot[i] = this.xO + this.xArg[i];
							this.yDot[i] = this.yO + this.yArg[i];
						}
					}
				}
				this.angle += this.iAngle;
			}
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00059CC8 File Offset: 0x00057EC8
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00059CE0 File Offset: 0x00057EE0
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00059CF8 File Offset: 0x00057EF8
	public int getH()
	{
		return 20;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00059D0C File Offset: 0x00057F0C
	public int getW()
	{
		return 20;
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00003136 File Offset: 0x00001336
	public void stopMoving()
	{
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00059D20 File Offset: 0x00057F20
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x04000992 RID: 2450
	public int x;

	// Token: 0x04000993 RID: 2451
	public int y;

	// Token: 0x04000994 RID: 2452
	public int xEnd;

	// Token: 0x04000995 RID: 2453
	public int yEnd;

	// Token: 0x04000996 RID: 2454
	public int f;

	// Token: 0x04000997 RID: 2455
	public int vx;

	// Token: 0x04000998 RID: 2456
	public int vy;

	// Token: 0x04000999 RID: 2457
	public int playerId;

	// Token: 0x0400099A RID: 2458
	public int itemMapID;

	// Token: 0x0400099B RID: 2459
	public int IdCharMove;

	// Token: 0x0400099C RID: 2460
	public ItemTemplate template;

	// Token: 0x0400099D RID: 2461
	public sbyte status;

	// Token: 0x0400099E RID: 2462
	public bool isHintFocus;

	// Token: 0x0400099F RID: 2463
	public int rO;

	// Token: 0x040009A0 RID: 2464
	public int xO;

	// Token: 0x040009A1 RID: 2465
	public int yO;

	// Token: 0x040009A2 RID: 2466
	public int angle;

	// Token: 0x040009A3 RID: 2467
	public int iAngle;

	// Token: 0x040009A4 RID: 2468
	public int iDot;

	// Token: 0x040009A5 RID: 2469
	public int[] xArg;

	// Token: 0x040009A6 RID: 2470
	public int[] yArg;

	// Token: 0x040009A7 RID: 2471
	public int[] xDot;

	// Token: 0x040009A8 RID: 2472
	public int[] yDot;

	// Token: 0x040009A9 RID: 2473
	public int count;

	// Token: 0x040009AA RID: 2474
	public int countAura;

	// Token: 0x040009AB RID: 2475
	public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

	// Token: 0x040009AC RID: 2476
	public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

	// Token: 0x040009AD RID: 2477
	public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

	// Token: 0x040009AE RID: 2478
	public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
}
