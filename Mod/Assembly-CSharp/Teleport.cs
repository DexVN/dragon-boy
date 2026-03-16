using System;

// Token: 0x02000093 RID: 147
public class Teleport
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x0003BF88 File Offset: 0x0003A388
	public Teleport(int x, int y, int headId, int dir, int type, bool isMe, int planet)
	{
		this.x = x;
		this.y = 5;
		this.y2 = y;
		this.headId = headId;
		this.type = type;
		this.isMe = isMe;
		this.dir = dir;
		this.planet = planet;
		this.tPrepare = 0;
		int i = 0;
		while (i < 100)
		{
			i++;
			this.y2 += 12;
			if (TileMap.tileTypeAt(x, this.y2, 2))
			{
				if (this.y2 % 24 != 0)
				{
					this.y2 -= this.y2 % 24;
				}
				break;
			}
		}
		this.isDown = true;
		this.isUp = false;
		if (this.planet > 2)
		{
			this.y2 += 4;
			if (Teleport.maybay[3] == null)
			{
				Teleport.maybay[3] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4a.png");
			}
			if (Teleport.maybay[4] == null)
			{
				Teleport.maybay[4] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4b.png");
			}
			if (Teleport.hole == null)
			{
				Teleport.hole = GameCanvas.loadImage("/mainImage/hole.png");
			}
		}
		else if (Teleport.maybay[planet] == null)
		{
			Teleport.maybay[planet] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay" + (planet + 1) + ".png");
		}
		if (x > GameScr.cmx && x < GameScr.cmx + GameCanvas.w && this.y2 > 100 && !SoundMn.gI().isPlayAirShip() && !SoundMn.gI().isPlayRain())
		{
			this.createShip = true;
			SoundMn.gI().airShip();
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0003C14C File Offset: 0x0003A54C
	public static void addTeleport(Teleport p)
	{
		Teleport.vTeleport.addElement(p);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0003C159 File Offset: 0x0003A559
	public void paintHole(mGraphics g)
	{
		if (this.planet > 2 && this.tHole)
		{
			g.drawImage(Teleport.hole, this.x, this.y2 + 20, StaticObj.BOTTOM_HCENTER);
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0003C194 File Offset: 0x0003A594
	public void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (this.x < GameScr.cmx || this.x > GameScr.cmx + GameCanvas.w)
		{
			return;
		}
		Part part = GameScr.parts[this.headId];
		int num = 0;
		int num2 = 0;
		if (this.planet == 0)
		{
			num = 15;
			num2 = 40;
		}
		if (this.planet == 1)
		{
			num = 7;
			num2 = 55;
		}
		if (this.planet == 2)
		{
			num = 18;
			num2 = 52;
		}
		if (this.painHead && this.planet < 3)
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, this.x + ((this.dir != 1) ? (-num) : num), this.y - num2, (this.dir != 1) ? 2 : 0, StaticObj.TOP_CENTER);
		}
		if (this.planet < 3)
		{
			g.drawRegion(Teleport.maybay[this.planet], 0, 0, mGraphics.getImageWidth(Teleport.maybay[this.planet]), mGraphics.getImageHeight(Teleport.maybay[this.planet]), (this.dir != 1) ? 0 : 2, this.x, this.y, StaticObj.BOTTOM_HCENTER);
		}
		else if (this.isDown)
		{
			if (this.tPrepare > 10)
			{
				g.drawRegion(Teleport.maybay[4], 0, 0, mGraphics.getImageWidth(Teleport.maybay[4]), mGraphics.getImageHeight(Teleport.maybay[4]), (this.dir != 1) ? 0 : 2, (this.dir != 1) ? (this.x + 11) : (this.x - 11), this.y + 2, StaticObj.BOTTOM_HCENTER);
			}
			else
			{
				g.drawRegion(Teleport.maybay[3], 0, 0, mGraphics.getImageWidth(Teleport.maybay[3]), mGraphics.getImageHeight(Teleport.maybay[3]), (this.dir != 1) ? 0 : 2, this.x, this.y, StaticObj.BOTTOM_HCENTER);
			}
		}
		else if (this.tPrepare < 20)
		{
			g.drawRegion(Teleport.maybay[4], 0, 0, mGraphics.getImageWidth(Teleport.maybay[4]), mGraphics.getImageHeight(Teleport.maybay[4]), (this.dir != 1) ? 0 : 2, (this.dir != 1) ? (this.x + 11) : (this.x - 11), this.y + 2, StaticObj.BOTTOM_HCENTER);
		}
		else
		{
			g.drawRegion(Teleport.maybay[3], 0, 0, mGraphics.getImageWidth(Teleport.maybay[3]), mGraphics.getImageHeight(Teleport.maybay[3]), (this.dir != 1) ? 0 : 2, this.x, this.y, StaticObj.BOTTOM_HCENTER);
		}
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0003C49C File Offset: 0x0003A89C
	public void update()
	{
		if (this.planet > 2 && this.paintFire && this.y != -80)
		{
			if (this.isDown && this.tPrepare == 0)
			{
				if (GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(1, this.x, this.y, 1, 0);
				}
			}
			else if (this.isUp && GameCanvas.gameTick % 3 == 0)
			{
				ServerEffect.addServerEffect(1, this.x, this.y + 16, 1, 1);
			}
		}
		this.tFire++;
		if (this.tFire > 3)
		{
			this.tFire = 0;
		}
		if (this.isDown)
		{
			this.paintFire = true;
			this.painHead = (this.type != 0);
			if (this.planet < 3)
			{
				int num = this.y2 - this.y >> 3;
				if (num < 1)
				{
					num = 1;
					this.paintFire = false;
				}
				this.y += num;
			}
			else
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.vy++;
				}
				if (this.y2 - this.y < this.vy)
				{
					this.y = this.y2;
					this.paintFire = false;
				}
				else
				{
					this.y += this.vy;
				}
			}
			if (this.isMe && this.type == 1 && global::Char.myCharz().isTeleport)
			{
				global::Char.myCharz().cx = this.x;
				global::Char.myCharz().cy = this.y - 30;
				global::Char.myCharz().statusMe = 4;
				GameScr.cmtoX = this.x - GameScr.gW2;
				GameScr.cmtoY = this.y - GameScr.gH23;
				GameScr.info1.isUpdate = false;
			}
			if (GameScr.findCharInMap(this.id) != null && !this.isMe && this.type == 1 && GameScr.findCharInMap(this.id).isTeleport)
			{
				GameScr.findCharInMap(this.id).cx = this.x;
				GameScr.findCharInMap(this.id).cy = this.y - 30;
				GameScr.findCharInMap(this.id).statusMe = 4;
			}
			if (Res.abs(this.y - this.y2) < 50 && TileMap.tileTypeAt(this.x, this.y, 2))
			{
				this.tHole = true;
				if (this.planet < 3)
				{
					SoundMn.gI().pauseAirShip();
					if (this.y % 24 != 0)
					{
						this.y -= this.y % 24;
					}
					this.tPrepare++;
					if (this.tPrepare > 10)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					if (this.type == 1)
					{
						if (this.isMe)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else if (GameScr.findCharInMap(this.id) != null)
						{
							GameScr.findCharInMap(this.id).isTeleport = false;
						}
						this.painHead = false;
					}
				}
				else
				{
					this.y = this.y2;
					if (!this.isShock)
					{
						ServerEffect.addServerEffect(92, this.x + 4, this.y + 14, 1, 0);
						GameScr.shock_scr = 10;
						this.isShock = true;
					}
					this.tPrepare++;
					if (this.tPrepare > 30)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					if (this.type == 1)
					{
						if (this.isMe)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else if (GameScr.findCharInMap(this.id) != null)
						{
							GameScr.findCharInMap(this.id).isTeleport = false;
						}
						this.painHead = false;
					}
				}
			}
		}
		else if (this.isUp)
		{
			this.tPrepare++;
			if (this.tPrepare > 30)
			{
				int num2 = this.y2 + 24 - this.y >> 3;
				if (num2 > 30)
				{
					num2 = 30;
				}
				this.y -= num2;
				this.paintFire = true;
			}
			else
			{
				if (this.tPrepare == 14 && this.createShip)
				{
					SoundMn.gI().resumeAirShip();
				}
				if (this.tPrepare > 0 && this.type == 0)
				{
					if (this.isMe)
					{
						global::Char.myCharz().isTeleport = false;
						if (global::Char.myCharz().statusMe != 14)
						{
							global::Char.myCharz().statusMe = 3;
						}
						global::Char.myCharz().cvy = -3;
					}
					else if (GameScr.findCharInMap(this.id) != null)
					{
						GameScr.findCharInMap(this.id).isTeleport = false;
						if (GameScr.findCharInMap(this.id).statusMe != 14)
						{
							GameScr.findCharInMap(this.id).statusMe = 3;
						}
						GameScr.findCharInMap(this.id).cvy = -3;
					}
					this.painHead = false;
				}
				if (this.tPrepare > 12 && this.type == 0)
				{
					if (this.isMe)
					{
						global::Char.myCharz().isTeleport = true;
					}
					else if (GameScr.findCharInMap(this.id) != null)
					{
						GameScr.findCharInMap(this.id).cx = this.x;
						GameScr.findCharInMap(this.id).cy = this.y;
						GameScr.findCharInMap(this.id).isTeleport = true;
					}
					this.painHead = true;
				}
			}
			if (this.isMe)
			{
				if (this.type == 0)
				{
					GameScr.cmtoX = this.x - GameScr.gW2;
					GameScr.cmtoY = this.y - GameScr.gH23;
				}
				if (this.type == 1)
				{
					GameScr.info1.isUpdate = true;
				}
			}
			if (this.y <= -80)
			{
				if (this.isMe && this.type == 0)
				{
					Controller.isStopReadMessage = false;
					global::Char.ischangingMap = true;
				}
				if (!this.isMe && GameScr.findCharInMap(this.id) != null && this.type == 0)
				{
					GameScr.vCharInMap.removeElement(GameScr.findCharInMap(this.id));
				}
				if (this.planet < 3)
				{
					Teleport.vTeleport.removeElement(this);
				}
				else
				{
					this.y = -80;
					this.tDelayHole++;
					if (this.tDelayHole > 80)
					{
						this.tDelayHole = 0;
						Teleport.vTeleport.removeElement(this);
					}
				}
			}
		}
		if (this.paintFire && this.planet < 3 && Res.abs(this.y - this.y2) <= 50 && GameCanvas.gameTick % 5 == 0)
		{
			Effect me = new Effect(19, this.x, this.y2 + 20, 2, 1, -1);
			EffecMn.addEff(me);
		}
	}

	// Token: 0x0400080D RID: 2061
	public static MyVector vTeleport = new MyVector();

	// Token: 0x0400080E RID: 2062
	public int x;

	// Token: 0x0400080F RID: 2063
	public int y;

	// Token: 0x04000810 RID: 2064
	public int headId;

	// Token: 0x04000811 RID: 2065
	public int type;

	// Token: 0x04000812 RID: 2066
	public bool isMe;

	// Token: 0x04000813 RID: 2067
	public int y2;

	// Token: 0x04000814 RID: 2068
	public int id;

	// Token: 0x04000815 RID: 2069
	public int dir;

	// Token: 0x04000816 RID: 2070
	public int planet;

	// Token: 0x04000817 RID: 2071
	public static Image[] maybay = new Image[5];

	// Token: 0x04000818 RID: 2072
	public static Image hole;

	// Token: 0x04000819 RID: 2073
	public bool isUp;

	// Token: 0x0400081A RID: 2074
	public bool isDown;

	// Token: 0x0400081B RID: 2075
	private bool createShip;

	// Token: 0x0400081C RID: 2076
	public bool paintFire;

	// Token: 0x0400081D RID: 2077
	private bool painHead;

	// Token: 0x0400081E RID: 2078
	private int tPrepare;

	// Token: 0x0400081F RID: 2079
	private int vy = 1;

	// Token: 0x04000820 RID: 2080
	private int tFire;

	// Token: 0x04000821 RID: 2081
	private int tDelayHole;

	// Token: 0x04000822 RID: 2082
	private bool tHole;

	// Token: 0x04000823 RID: 2083
	private bool isShock;
}
