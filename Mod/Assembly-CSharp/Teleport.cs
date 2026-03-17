using System;

// Token: 0x020000B3 RID: 179
public class Teleport
{
	// Token: 0x060009C0 RID: 2496 RVA: 0x000A2A34 File Offset: 0x000A0C34
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
			bool flag = TileMap.tileTypeAt(x, this.y2, 2);
			if (flag)
			{
				bool flag2 = this.y2 % 24 != 0;
				if (flag2)
				{
					this.y2 -= this.y2 % 24;
				}
				break;
			}
		}
		this.isDown = true;
		this.isUp = false;
		bool flag3 = this.planet > 2;
		if (flag3)
		{
			this.y2 += 4;
			bool flag4 = Teleport.maybay[3] == null;
			if (flag4)
			{
				Teleport.maybay[3] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4a.png");
			}
			bool flag5 = Teleport.maybay[4] == null;
			if (flag5)
			{
				Teleport.maybay[4] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4b.png");
			}
			bool flag6 = Teleport.hole == null;
			if (flag6)
			{
				Teleport.hole = GameCanvas.loadImage("/mainImage/hole.png");
			}
		}
		else
		{
			bool flag7 = Teleport.maybay[planet] == null;
			if (flag7)
			{
				Teleport.maybay[planet] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay" + (planet + 1).ToString() + ".png");
			}
		}
		bool flag8 = x > GameScr.cmx && x < GameScr.cmx + GameCanvas.w && this.y2 > 100 && !SoundMn.gI().isPlayAirShip() && !SoundMn.gI().isPlayRain();
		if (flag8)
		{
			this.createShip = true;
			SoundMn.gI().airShip();
		}
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x000A2C17 File Offset: 0x000A0E17
	public static void addTeleport(Teleport p)
	{
		Teleport.vTeleport.addElement(p);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x000A2C28 File Offset: 0x000A0E28
	public void paintHole(mGraphics g)
	{
		bool flag = this.planet > 2 && this.tHole;
		if (flag)
		{
			g.drawImage(Teleport.hole, this.x, this.y2 + 20, StaticObj.BOTTOM_HCENTER);
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000A2C70 File Offset: 0x000A0E70
	public void paint(mGraphics g)
	{
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			bool flag = this.x < GameScr.cmx || this.x > GameScr.cmx + GameCanvas.w;
			if (!flag)
			{
				Part part = GameScr.parts[this.headId];
				int num = 0;
				int num2 = 0;
				bool flag2 = this.planet == 0;
				if (flag2)
				{
					num = 15;
					num2 = 40;
				}
				bool flag3 = this.planet == 1;
				if (flag3)
				{
					num = 7;
					num2 = 55;
				}
				bool flag4 = this.planet == 2;
				if (flag4)
				{
					num = 18;
					num2 = 52;
				}
				bool flag5 = this.painHead && this.planet < 3;
				if (flag5)
				{
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, this.x + ((this.dir != 1) ? (-num) : num), this.y - num2, (this.dir != 1) ? 2 : 0, StaticObj.TOP_CENTER);
				}
				bool flag6 = this.planet < 3;
				if (flag6)
				{
					g.drawRegion(Teleport.maybay[this.planet], 0, 0, mGraphics.getImageWidth(Teleport.maybay[this.planet]), mGraphics.getImageHeight(Teleport.maybay[this.planet]), (this.dir != 1) ? 0 : 2, this.x, this.y, StaticObj.BOTTOM_HCENTER);
				}
				else
				{
					bool flag7 = this.isDown;
					if (flag7)
					{
						bool flag8 = this.tPrepare > 10;
						if (flag8)
						{
							g.drawRegion(Teleport.maybay[4], 0, 0, mGraphics.getImageWidth(Teleport.maybay[4]), mGraphics.getImageHeight(Teleport.maybay[4]), (this.dir != 1) ? 0 : 2, (this.dir != 1) ? (this.x + 11) : (this.x - 11), this.y + 2, StaticObj.BOTTOM_HCENTER);
						}
						else
						{
							g.drawRegion(Teleport.maybay[3], 0, 0, mGraphics.getImageWidth(Teleport.maybay[3]), mGraphics.getImageHeight(Teleport.maybay[3]), (this.dir != 1) ? 0 : 2, this.x, this.y, StaticObj.BOTTOM_HCENTER);
						}
					}
					else
					{
						bool flag9 = this.tPrepare < 20;
						if (flag9)
						{
							g.drawRegion(Teleport.maybay[4], 0, 0, mGraphics.getImageWidth(Teleport.maybay[4]), mGraphics.getImageHeight(Teleport.maybay[4]), (this.dir != 1) ? 0 : 2, (this.dir != 1) ? (this.x + 11) : (this.x - 11), this.y + 2, StaticObj.BOTTOM_HCENTER);
						}
						else
						{
							g.drawRegion(Teleport.maybay[3], 0, 0, mGraphics.getImageWidth(Teleport.maybay[3]), mGraphics.getImageHeight(Teleport.maybay[3]), (this.dir != 1) ? 0 : 2, this.x, this.y, StaticObj.BOTTOM_HCENTER);
						}
					}
				}
			}
		}
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x000A2F74 File Offset: 0x000A1174
	public void update()
	{
		bool flag = this.planet > 2 && this.paintFire && this.y != -80;
		if (flag)
		{
			bool flag2 = this.isDown && this.tPrepare == 0;
			if (flag2)
			{
				bool flag3 = GameCanvas.gameTick % 3 == 0;
				if (flag3)
				{
					ServerEffect.addServerEffect(1, this.x, this.y, 1, 0);
				}
			}
			else
			{
				bool flag4 = this.isUp && GameCanvas.gameTick % 3 == 0;
				if (flag4)
				{
					ServerEffect.addServerEffect(1, this.x, this.y + 16, 1, 1);
				}
			}
		}
		this.tFire++;
		bool flag5 = this.tFire > 3;
		if (flag5)
		{
			this.tFire = 0;
		}
		bool flag6 = this.isDown;
		if (flag6)
		{
			this.paintFire = true;
			this.painHead = (this.type != 0);
			bool flag7 = this.planet < 3;
			if (flag7)
			{
				int num = this.y2 - this.y >> 3;
				bool flag8 = num < 1;
				if (flag8)
				{
					num = 1;
					this.paintFire = false;
				}
				this.y += num;
			}
			else
			{
				bool flag9 = GameCanvas.gameTick % 2 == 0;
				if (flag9)
				{
					this.vy++;
				}
				bool flag10 = this.y2 - this.y < this.vy;
				if (flag10)
				{
					this.y = this.y2;
					this.paintFire = false;
				}
				else
				{
					this.y += this.vy;
				}
			}
			bool flag11 = this.isMe && this.type == 1 && global::Char.myCharz().isTeleport;
			if (flag11)
			{
				global::Char.myCharz().cx = this.x;
				global::Char.myCharz().cy = this.y - 30;
				global::Char.myCharz().statusMe = 4;
				GameScr.cmtoX = this.x - GameScr.gW2;
				GameScr.cmtoY = this.y - GameScr.gH23;
				GameScr.info1.isUpdate = false;
			}
			bool flag12 = GameScr.findCharInMap(this.id) != null && !this.isMe && this.type == 1 && GameScr.findCharInMap(this.id).isTeleport;
			if (flag12)
			{
				GameScr.findCharInMap(this.id).cx = this.x;
				GameScr.findCharInMap(this.id).cy = this.y - 30;
				GameScr.findCharInMap(this.id).statusMe = 4;
			}
			bool flag13 = Res.abs(this.y - this.y2) < 50 && TileMap.tileTypeAt(this.x, this.y, 2);
			if (flag13)
			{
				this.tHole = true;
				bool flag14 = this.planet < 3;
				if (flag14)
				{
					SoundMn.gI().pauseAirShip();
					bool flag15 = this.y % 24 != 0;
					if (flag15)
					{
						this.y -= this.y % 24;
					}
					this.tPrepare++;
					bool flag16 = this.tPrepare > 10;
					if (flag16)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					bool flag17 = this.type == 1;
					if (flag17)
					{
						bool flag18 = this.isMe;
						if (flag18)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else
						{
							bool flag19 = GameScr.findCharInMap(this.id) != null;
							if (flag19)
							{
								GameScr.findCharInMap(this.id).isTeleport = false;
							}
						}
						this.painHead = false;
					}
				}
				else
				{
					this.y = this.y2;
					bool flag20 = !this.isShock;
					if (flag20)
					{
						ServerEffect.addServerEffect(92, this.x + 4, this.y + 14, 1, 0);
						GameScr.shock_scr = 10;
						this.isShock = true;
					}
					this.tPrepare++;
					bool flag21 = this.tPrepare > 30;
					if (flag21)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					bool flag22 = this.type == 1;
					if (flag22)
					{
						bool flag23 = this.isMe;
						if (flag23)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else
						{
							bool flag24 = GameScr.findCharInMap(this.id) != null;
							if (flag24)
							{
								GameScr.findCharInMap(this.id).isTeleport = false;
							}
						}
						this.painHead = false;
					}
				}
			}
		}
		else
		{
			bool flag25 = this.isUp;
			if (flag25)
			{
				this.tPrepare++;
				bool flag26 = this.tPrepare > 30;
				if (flag26)
				{
					int num2 = this.y2 + 24 - this.y >> 3;
					bool flag27 = num2 > 30;
					if (flag27)
					{
						num2 = 30;
					}
					this.y -= num2;
					this.paintFire = true;
				}
				else
				{
					bool flag28 = this.tPrepare == 14 && this.createShip;
					if (flag28)
					{
						SoundMn.gI().resumeAirShip();
					}
					bool flag29 = this.tPrepare > 0 && this.type == 0;
					if (flag29)
					{
						bool flag30 = this.isMe;
						if (flag30)
						{
							global::Char.myCharz().isTeleport = false;
							bool flag31 = global::Char.myCharz().statusMe != 14;
							if (flag31)
							{
								global::Char.myCharz().statusMe = 3;
							}
							global::Char.myCharz().cvy = -3;
						}
						else
						{
							bool flag32 = GameScr.findCharInMap(this.id) != null;
							if (flag32)
							{
								GameScr.findCharInMap(this.id).isTeleport = false;
								bool flag33 = GameScr.findCharInMap(this.id).statusMe != 14;
								if (flag33)
								{
									GameScr.findCharInMap(this.id).statusMe = 3;
								}
								GameScr.findCharInMap(this.id).cvy = -3;
							}
						}
						this.painHead = false;
					}
					bool flag34 = this.tPrepare > 12 && this.type == 0;
					if (flag34)
					{
						bool flag35 = this.isMe;
						if (flag35)
						{
							global::Char.myCharz().isTeleport = true;
						}
						else
						{
							bool flag36 = GameScr.findCharInMap(this.id) != null;
							if (flag36)
							{
								GameScr.findCharInMap(this.id).cx = this.x;
								GameScr.findCharInMap(this.id).cy = this.y;
								GameScr.findCharInMap(this.id).isTeleport = true;
							}
						}
						this.painHead = true;
					}
				}
				bool flag37 = this.isMe;
				if (flag37)
				{
					bool flag38 = this.type == 0;
					if (flag38)
					{
						GameScr.cmtoX = this.x - GameScr.gW2;
						GameScr.cmtoY = this.y - GameScr.gH23;
					}
					bool flag39 = this.type == 1;
					if (flag39)
					{
						GameScr.info1.isUpdate = true;
					}
				}
				bool flag40 = this.y <= -80;
				if (flag40)
				{
					bool flag41 = this.isMe && this.type == 0;
					if (flag41)
					{
						Controller.isStopReadMessage = false;
						global::Char.ischangingMap = true;
					}
					bool flag42 = !this.isMe && GameScr.findCharInMap(this.id) != null && this.type == 0;
					if (flag42)
					{
						GameScr.vCharInMap.removeElement(GameScr.findCharInMap(this.id));
					}
					bool flag43 = this.planet < 3;
					if (flag43)
					{
						Teleport.vTeleport.removeElement(this);
					}
					else
					{
						this.y = -80;
						this.tDelayHole++;
						bool flag44 = this.tDelayHole > 80;
						if (flag44)
						{
							this.tDelayHole = 0;
							Teleport.vTeleport.removeElement(this);
						}
					}
				}
			}
		}
		bool flag45 = this.paintFire && this.planet < 3 && Res.abs(this.y - this.y2) <= 50 && GameCanvas.gameTick % 5 == 0;
		if (flag45)
		{
			Effect me = new Effect(19, this.x, this.y2 + 20, 2, 1, -1);
			EffecMn.addEff(me);
		}
	}

	// Token: 0x0400124F RID: 4687
	public static MyVector vTeleport = new MyVector();

	// Token: 0x04001250 RID: 4688
	public int x;

	// Token: 0x04001251 RID: 4689
	public int y;

	// Token: 0x04001252 RID: 4690
	public int headId;

	// Token: 0x04001253 RID: 4691
	public int type;

	// Token: 0x04001254 RID: 4692
	public bool isMe;

	// Token: 0x04001255 RID: 4693
	public int y2;

	// Token: 0x04001256 RID: 4694
	public int id;

	// Token: 0x04001257 RID: 4695
	public int dir;

	// Token: 0x04001258 RID: 4696
	public int planet;

	// Token: 0x04001259 RID: 4697
	public static Image[] maybay = new Image[5];

	// Token: 0x0400125A RID: 4698
	public static Image hole;

	// Token: 0x0400125B RID: 4699
	public bool isUp;

	// Token: 0x0400125C RID: 4700
	public bool isDown;

	// Token: 0x0400125D RID: 4701
	private bool createShip;

	// Token: 0x0400125E RID: 4702
	public bool paintFire;

	// Token: 0x0400125F RID: 4703
	private bool painHead;

	// Token: 0x04001260 RID: 4704
	private int tPrepare;

	// Token: 0x04001261 RID: 4705
	private int vy = 1;

	// Token: 0x04001262 RID: 4706
	private int tFire;

	// Token: 0x04001263 RID: 4707
	private int tDelayHole;

	// Token: 0x04001264 RID: 4708
	private bool tHole;

	// Token: 0x04001265 RID: 4709
	private bool isShock;
}
