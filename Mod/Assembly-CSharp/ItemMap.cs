using System;

// Token: 0x02000062 RID: 98
public class ItemMap : IMapObject
{
	// Token: 0x0600038E RID: 910 RVA: 0x0001DBC0 File Offset: 0x0001BFC0
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

	// Token: 0x0600038F RID: 911 RVA: 0x0001DC5C File Offset: 0x0001C05C
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
		if (this.isAuraItem())
		{
			this.rO = (int)r;
			this.setAuraItem();
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0001DD5C File Offset: 0x0001C15C
	public void setPoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - this.x >> 2;
		this.vy = yEnd - this.y >> 2;
		this.status = 2;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001DD94 File Offset: 0x0001C194
	public void update()
	{
		if ((int)this.status == 2 && this.x == this.xEnd && this.y == this.yEnd)
		{
			GameScr.vItemMap.removeElement(this);
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this))
			{
				global::Char.myCharz().itemFocus = null;
			}
			return;
		}
		if ((int)this.status > 0)
		{
			if (this.vx == 0)
			{
				this.x = this.xEnd;
			}
			if (this.vy == 0)
			{
				this.y = this.yEnd;
			}
			if (this.x != this.xEnd)
			{
				this.x += this.vx;
				if ((this.vx > 0 && this.x > this.xEnd) || (this.vx < 0 && this.x < this.xEnd))
				{
					this.x = this.xEnd;
				}
			}
			if (this.y != this.yEnd)
			{
				this.y += this.vy;
				if ((this.vy > 0 && this.y > this.yEnd) || (this.vy < 0 && this.y < this.yEnd))
				{
					this.y = this.yEnd;
				}
			}
		}
		else
		{
			this.status = (sbyte)((int)this.status - 4);
			if ((int)this.status < -12)
			{
				this.y -= 12;
				this.status = 1;
			}
		}
		if (this.isAuraItem())
		{
			this.updateAuraItemEff();
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0001DF64 File Offset: 0x0001C364
	public void paint(mGraphics g)
	{
		if (this.isAuraItem())
		{
			g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
			if ((int)this.status <= 0)
			{
				if (this.countAura < 10)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			else if (this.countAura < 10)
			{
				g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		else if (!this.isAuraItem())
		{
			if (GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if ((int)this.status <= 0)
			{
				SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this) && (int)this.status != 2)
			{
				g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
			}
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0001E184 File Offset: 0x0001C584
	private bool isAuraItem()
	{
		return (int)this.template.type == 22;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0001E1B0 File Offset: 0x0001C5B0
	private void setAuraItem()
	{
		this.xO = this.x;
		this.yO = this.y;
		this.iDot = 120;
		this.angle = 0;
		if (!GameCanvas.lowGraphic)
		{
			this.iAngle = 360 / this.iDot;
			this.xArg = new int[this.iDot];
			this.yArg = new int[this.iDot];
			this.xDot = new int[this.iDot];
			this.yDot = new int[this.iDot];
			this.setDotPosition();
		}
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0001E24C File Offset: 0x0001C64C
	private void updateAuraItemEff()
	{
		this.count++;
		this.countAura++;
		if (this.countAura >= 40)
		{
			this.countAura = 0;
		}
		if (this.count >= this.iDot)
		{
			this.count = 0;
		}
		if (this.count % 10 == 0 && !GameCanvas.lowGraphic)
		{
			ServerEffect.addServerEffect(114, this.x - 5, this.y - 30, 1);
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001E2D4 File Offset: 0x0001C6D4
	public void paintAuraItemEff(mGraphics g)
	{
		if (!GameCanvas.lowGraphic && this.isAuraItem())
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				if (this.count == i)
				{
					if (this.countAura <= 20)
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

	// Token: 0x06000397 RID: 919 RVA: 0x0001E384 File Offset: 0x0001C784
	private void setDotPosition()
	{
		if (!GameCanvas.lowGraphic)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				if (this.angle < 90)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 90 && this.angle < 180)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 180 && this.angle < 270)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				else
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				this.angle += this.iAngle;
			}
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001E522 File Offset: 0x0001C922
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001E52A File Offset: 0x0001C92A
	public int getY()
	{
		return this.y;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x0001E532 File Offset: 0x0001C932
	public int getH()
	{
		return 20;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0001E536 File Offset: 0x0001C936
	public int getW()
	{
		return 20;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001E53A File Offset: 0x0001C93A
	public void stopMoving()
	{
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001E53C File Offset: 0x0001C93C
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x040005F1 RID: 1521
	public int x;

	// Token: 0x040005F2 RID: 1522
	public int y;

	// Token: 0x040005F3 RID: 1523
	public int xEnd;

	// Token: 0x040005F4 RID: 1524
	public int yEnd;

	// Token: 0x040005F5 RID: 1525
	public int f;

	// Token: 0x040005F6 RID: 1526
	public int vx;

	// Token: 0x040005F7 RID: 1527
	public int vy;

	// Token: 0x040005F8 RID: 1528
	public int playerId;

	// Token: 0x040005F9 RID: 1529
	public int itemMapID;

	// Token: 0x040005FA RID: 1530
	public int IdCharMove;

	// Token: 0x040005FB RID: 1531
	public ItemTemplate template;

	// Token: 0x040005FC RID: 1532
	public sbyte status;

	// Token: 0x040005FD RID: 1533
	public bool isHintFocus;

	// Token: 0x040005FE RID: 1534
	public int rO;

	// Token: 0x040005FF RID: 1535
	public int xO;

	// Token: 0x04000600 RID: 1536
	public int yO;

	// Token: 0x04000601 RID: 1537
	public int angle;

	// Token: 0x04000602 RID: 1538
	public int iAngle;

	// Token: 0x04000603 RID: 1539
	public int iDot;

	// Token: 0x04000604 RID: 1540
	public int[] xArg;

	// Token: 0x04000605 RID: 1541
	public int[] yArg;

	// Token: 0x04000606 RID: 1542
	public int[] xDot;

	// Token: 0x04000607 RID: 1543
	public int[] yDot;

	// Token: 0x04000608 RID: 1544
	public int count;

	// Token: 0x04000609 RID: 1545
	public int countAura;

	// Token: 0x0400060A RID: 1546
	public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

	// Token: 0x0400060B RID: 1547
	public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

	// Token: 0x0400060C RID: 1548
	public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

	// Token: 0x0400060D RID: 1549
	public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
}
