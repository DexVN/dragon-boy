using System;

// Token: 0x0200000A RID: 10
public class BackgroudEffect
{
	// Token: 0x06000044 RID: 68 RVA: 0x00003D88 File Offset: 0x00001F88
	public BackgroudEffect(int typeS)
	{
		BackgroudEffect.isFog = true;
		BackgroudEffect.initCloud();
		this.typeEff = typeS;
		switch (this.typeEff)
		{
		case 0:
		case 12:
		{
			bool flag = BackgroudEffect.imgHatMua == null;
			if (flag)
			{
				BackgroudEffect.imgHatMua = GameCanvas.loadImageRMS("/bg/mua.png");
			}
			bool flag2 = BackgroudEffect.imgMua1 == null;
			if (flag2)
			{
				BackgroudEffect.imgMua1 = GameCanvas.loadImageRMS("/bg/mua1.png");
			}
			bool flag3 = BackgroudEffect.imgMua2 == null;
			if (flag3)
			{
				BackgroudEffect.imgMua2 = GameCanvas.loadImageRMS("/bg/mua2.png");
			}
			this.sum = Res.random(GameCanvas.w / 3, GameCanvas.w / 2);
			this.x = new int[this.sum];
			this.y = new int[this.sum];
			this.vx = new int[this.sum];
			this.vy = new int[this.sum];
			this.type = new int[this.sum];
			this.t = new int[this.sum];
			this.frame = new int[this.sum];
			this.isRainEffect = new bool[this.sum];
			this.activeEff = new bool[this.sum];
			for (int i = 0; i < this.sum; i++)
			{
				this.y[i] = Res.random(-10, GameCanvas.h + 100) + GameScr.cmy;
				this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
				this.t[i] = Res.random(0, 1);
				this.vx[i] = -12;
				this.vy[i] = 12;
				this.type[i] = Res.random(1, 3);
				this.isRainEffect[i] = false;
				bool flag4 = this.type[i] == 2 && i % 2 == 0;
				if (flag4)
				{
					this.isRainEffect[i] = true;
				}
				this.activeEff[i] = false;
				this.frame[i] = Res.random(1, 2);
			}
			break;
		}
		case 1:
		case 2:
		case 5:
		case 6:
		case 7:
		case 11:
		case 15:
		{
			bool flag5 = this.typeEff == 1;
			if (flag5)
			{
				BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay.png");
				BackgroudEffect.PIXEL = 10;
			}
			bool flag6 = this.typeEff == 2;
			if (flag6)
			{
				BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay2.png");
				BackgroudEffect.PIXEL = 18;
			}
			bool flag7 = this.typeEff == 5;
			if (flag7)
			{
				BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay3.png");
				BackgroudEffect.PIXEL = 14;
			}
			bool flag8 = this.typeEff == 6;
			if (flag8)
			{
				BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay4.png");
				BackgroudEffect.PIXEL = 14;
			}
			bool flag9 = this.typeEff == 7;
			if (flag9)
			{
				BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay5.png");
				BackgroudEffect.PIXEL = 12;
			}
			bool flag10 = this.typeEff == 11;
			if (flag10)
			{
				BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/tuyet.png");
			}
			bool flag11 = this.typeEff == 15;
			if (flag11)
			{
				bool flag12 = SmallImage.imgNew[11120] == null;
				if (flag12)
				{
					SmallImage.createImage(11120);
				}
				BackgroudEffect.PIXEL = 16;
			}
			this.sum = Res.random(15, 25);
			bool flag13 = this.typeEff == 11;
			if (flag13)
			{
				this.sum = 100;
			}
			this.x = new int[this.sum];
			this.y = new int[this.sum];
			this.vx = new int[this.sum];
			this.vy = new int[this.sum];
			this.t = new int[this.sum];
			this.frame = new int[this.sum];
			this.activeEff = new bool[this.sum];
			for (int j = 0; j < this.sum; j++)
			{
				this.x[j] = Res.random(-10, TileMap.pxw + 10);
				this.y[j] = Res.random(0, TileMap.pxh);
				this.frame[j] = Res.random(0, 1);
				this.t[j] = Res.random(0, 1);
				this.vx[j] = Res.random(-3, 3);
				this.vy[j] = Res.random(1, 4);
				bool flag14 = this.typeEff == 11;
				if (flag14)
				{
					this.frame[j] = Res.random(0, 2);
					this.vx[j] = Res.abs(Res.random(1, 3));
					this.vy[j] = Res.abs(Res.random(1, 3));
				}
				bool flag15 = this.typeEff == 15;
				if (flag15)
				{
					this.frame[j] = Res.random(0, 2);
					this.vx[j] = Res.abs(Res.random(1, 3));
					this.vy[j] = Res.abs(Res.random(1, 3));
				}
			}
			break;
		}
		case 3:
			GameCanvas.isBoltEff = true;
			break;
		case 4:
		{
			this.sum = Res.random(5, 10);
			bool flag16 = BackgroudEffect.imgSao == null;
			if (flag16)
			{
				BackgroudEffect.imgSao = GameCanvas.loadImageRMS("/bg/sao.png");
			}
			this.x = new int[this.sum];
			this.y = new int[this.sum];
			this.frame = new int[this.sum];
			this.t = new int[this.sum];
			this.tick = new int[this.sum];
			for (int k = 0; k < this.sum; k++)
			{
				this.x[k] = Res.random(0, GameCanvas.w);
				this.y[k] = Res.random(0, 50);
				bool flag17 = k % 2 == 0;
				if (flag17)
				{
					this.tick[k] = 0;
				}
				else
				{
					bool flag18 = k % 3 == 0;
					if (flag18)
					{
						this.tick[k] = 1;
					}
					else
					{
						bool flag19 = k % 4 == 0;
						if (flag19)
						{
							this.tick[k] = 2;
						}
						else
						{
							this.tick[k] = 3;
						}
					}
				}
				this.t[k] = Res.random(0, 10);
			}
			break;
		}
		case 8:
		{
			this.tStart = Res.random(100, 300);
			bool flag20 = BackgroudEffect.imgShip == null;
			if (flag20)
			{
				BackgroudEffect.imgShip = GameCanvas.loadImageRMS("/bg/ship.png");
			}
			bool flag21 = BackgroudEffect.imgFire1 == null;
			if (flag21)
			{
				BackgroudEffect.imgFire1 = GameCanvas.loadImageRMS("/bg/fire1.png");
			}
			bool flag22 = BackgroudEffect.imgFire2 == null;
			if (flag22)
			{
				BackgroudEffect.imgFire2 = GameCanvas.loadImageRMS("/bg/fire2.png");
			}
			this.isFly = false;
			this.reloadShip();
			break;
		}
		case 9:
		{
			bool flag23 = BackgroudEffect.imgChamTron1 == null;
			if (flag23)
			{
				BackgroudEffect.imgChamTron1 = GameCanvas.loadImageRMS("/bg/cham-tron1.png");
			}
			bool flag24 = BackgroudEffect.imgChamTron2 == null;
			if (flag24)
			{
				BackgroudEffect.imgChamTron2 = GameCanvas.loadImageRMS("/bg/cham-tron2.png");
			}
			this.num = 20;
			this.x = new int[this.num];
			this.y = new int[this.num];
			BackgroudEffect.wP = new int[this.num];
			this.vx = new int[this.num];
			for (int l = 0; l < this.num; l++)
			{
				this.x[l] = Res.abs(Res.random(0, GameCanvas.w));
				this.y[l] = Res.abs(Res.random(10, 80));
				BackgroudEffect.wP[l] = Res.abs(Res.random(1, 3));
				this.vx[l] = BackgroudEffect.wP[l];
			}
			break;
		}
		case 10:
		{
			this.num = 30;
			this.x = new int[this.num];
			this.y = new int[this.num];
			BackgroudEffect.wP = new int[this.num];
			this.vx = new int[this.num];
			int num = 0;
			for (int m = 0; m < this.num; m++)
			{
				this.x[m] = Res.abs(Res.random(0, GameCanvas.w)) + GameScr.cmx;
				num++;
				bool flag25 = num > this.num / 2;
				if (flag25)
				{
					this.y[m] = Res.abs(Res.random(20, 60));
					BackgroudEffect.wP[m] = 10;
				}
				else
				{
					this.y[m] = Res.abs(Res.random(0, 20));
					BackgroudEffect.wP[m] = 7;
				}
				this.vx[m] = BackgroudEffect.wP[m] / 2 - 2;
			}
			break;
		}
		case 13:
		{
			bool flag26 = Res.abs(Res.random(0, 2)) == 0;
			if (flag26)
			{
				bool flag27 = Res.abs(Res.random(0, 2)) == 0;
				if (flag27)
				{
					BackgroudEffect.isPaintFar = true;
				}
				else
				{
					BackgroudEffect.isPaintFar = false;
				}
				BackgroudEffect.nCloud = Res.abs(Res.random(2, 5));
				BackgroudEffect.initCloud();
			}
			break;
		}
		case 14:
		{
			bool flag28 = Res.abs(Res.random(0, 2)) == 0;
			if (flag28)
			{
				BackgroudEffect.isFog = true;
				BackgroudEffect.initCloud();
			}
			break;
		}
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00004746 File Offset: 0x00002946
	public static void clearImage()
	{
		TileMap.yWater = 0;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00004750 File Offset: 0x00002950
	public static bool isHaveRain()
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			BackgroudEffect backgroudEffect = (BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i);
			bool flag = backgroudEffect.typeEff == 0 || backgroudEffect.typeEff == 12;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000047B0 File Offset: 0x000029B0
	public static void initCloud()
	{
		bool flag = mSystem.clientType == 1;
		if (flag)
		{
			BackgroudEffect.imgCloud1 = null;
			BackgroudEffect.imgFog = null;
		}
		else
		{
			bool lowGraphic = GameCanvas.lowGraphic;
			if (lowGraphic)
			{
				BackgroudEffect.imgCloud1 = null;
				BackgroudEffect.imgFog = null;
			}
			else
			{
				bool flag2 = BackgroudEffect.nCloud > 0;
				if (flag2)
				{
					bool flag3 = BackgroudEffect.imgCloud1 == null;
					if (flag3)
					{
						BackgroudEffect.imgCloud1 = GameCanvas.loadImage("/bg/fog1.png");
						BackgroudEffect.cloudw = BackgroudEffect.imgCloud1.getWidth();
					}
				}
				else
				{
					BackgroudEffect.imgCloud1 = null;
				}
				bool flag4 = !BackgroudEffect.isFog;
				if (flag4)
				{
					BackgroudEffect.imgFog = null;
				}
				else
				{
					bool flag5 = BackgroudEffect.imgFog == null;
					if (flag5)
					{
						BackgroudEffect.imgFog = GameCanvas.loadImage("/bg/fog0.png");
					}
					BackgroudEffect.fogw = 287;
				}
			}
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00004880 File Offset: 0x00002A80
	public static void updateCloud2()
	{
		bool flag = mSystem.clientType == 1;
		if (!flag)
		{
			bool lowGraphic = GameCanvas.lowGraphic;
			if (!lowGraphic)
			{
				bool flag2 = BackgroudEffect.nCloud > 0;
				if (flag2)
				{
					int num = (GameCanvas.currentScreen != GameScr.gI()) ? (GameScr.cmx + GameCanvas.w) : TileMap.pxw;
					for (int i = 0; i < BackgroudEffect.nCloud; i++)
					{
						int num2 = i + 1;
						GameCanvas.cloudX[i] -= num2;
						bool flag3 = GameCanvas.cloudX[i] < -BackgroudEffect.cloudw;
						if (flag3)
						{
							GameCanvas.cloudX[i] = num + 100;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00004934 File Offset: 0x00002B34
	public static void updateFog()
	{
		bool flag = mSystem.clientType == 1;
		if (!flag)
		{
			bool lowGraphic = GameCanvas.lowGraphic;
			if (!lowGraphic)
			{
				bool flag2 = BackgroudEffect.isFog;
				if (flag2)
				{
					BackgroudEffect.xfog--;
					bool flag3 = BackgroudEffect.xfog < -BackgroudEffect.fogw;
					if (flag3)
					{
						BackgroudEffect.xfog = 0;
					}
				}
			}
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00004990 File Offset: 0x00002B90
	public static void paintCloud2(mGraphics g)
	{
		bool flag = mSystem.clientType == 1;
		if (!flag)
		{
			bool lowGraphic = GameCanvas.lowGraphic;
			if (!lowGraphic)
			{
				bool flag2 = BackgroudEffect.nCloud == 0;
				if (!flag2)
				{
					bool flag3 = BackgroudEffect.imgCloud1 != null;
					if (flag3)
					{
						for (int i = 0; i < BackgroudEffect.nCloud; i++)
						{
							int num = i;
							bool flag4 = num > 3;
							if (flag4)
							{
								num = 3;
							}
							bool flag5 = num == 0;
							if (flag5)
							{
							}
							g.drawImage(BackgroudEffect.imgCloud1, GameCanvas.cloudX[i], GameCanvas.cloudY[i], 3);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004A34 File Offset: 0x00002C34
	public static void paintFog(mGraphics g)
	{
		bool flag = mSystem.clientType == 1;
		if (!flag)
		{
			bool lowGraphic = GameCanvas.lowGraphic;
			if (!lowGraphic)
			{
				bool flag2 = !BackgroudEffect.isFog;
				if (!flag2)
				{
					bool flag3 = BackgroudEffect.imgFog != null;
					if (flag3)
					{
						for (int i = BackgroudEffect.xfog; i < TileMap.pxw; i += BackgroudEffect.fogw)
						{
							bool flag4 = i >= GameScr.cmx - BackgroudEffect.fogw;
							if (flag4)
							{
								g.drawImageFog(BackgroudEffect.imgFog, i, BackgroudEffect.yfog, 0);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00004ACC File Offset: 0x00002CCC
	private void reloadShip()
	{
		int cmx = GameScr.cmx;
		int cmy = GameScr.cmy;
		this.way = Res.random(1, 3);
		this.isFly = false;
		this.speed = Res.random(3, 5);
		bool flag = this.way == 1;
		if (flag)
		{
			this.xShip = -50;
			this.yShip = Res.random(cmy, GameCanvas.h - 100 + cmy);
			this.trans = 0;
		}
		else
		{
			bool flag2 = this.way == 2;
			if (flag2)
			{
				this.xShip = TileMap.pxw + 50;
				this.yShip = Res.random(cmy, GameCanvas.h - 100 + cmy);
				this.trans = 2;
			}
			else
			{
				bool flag3 = this.way == 3;
				if (flag3)
				{
					this.xShip = Res.random(50 + cmx, GameCanvas.w - 50 + cmx);
					this.yShip = -50;
					this.trans = ((Res.random(0, 2) != 0) ? 2 : 0);
				}
				else
				{
					bool flag4 = this.way == 4;
					if (flag4)
					{
						this.xShip = Res.random(50 + cmx, GameCanvas.w - 50 + cmx);
						this.yShip = TileMap.pxh + 50;
						this.trans = ((Res.random(0, 2) != 0) ? 2 : 0);
					}
				}
			}
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00004C14 File Offset: 0x00002E14
	public void paintWater(mGraphics g)
	{
		bool flag = this.typeEff == 10;
		if (flag)
		{
			g.setColor(this.colorWater);
			for (int i = 0; i < this.num; i++)
			{
				g.drawImage((i >= this.num / 2) ? BackgroudEffect.water1 : BackgroudEffect.water2, this.x[i], this.y[i] + this.yWater, 0);
			}
			bool flag2 = BackgroudEffect.id_water1 != 0 && BackgroudEffect.water3 == null;
			if (flag2)
			{
				BackgroudEffect.water3 = SmallImage.imgNew[(int)BackgroudEffect.id_water1].img;
			}
			bool flag3 = BackgroudEffect.water3 != null;
			if (flag3)
			{
				for (int j = 0; j < this.num / 2; j++)
				{
					g.drawImage(BackgroudEffect.water3, this.x[j], this.y[j] + this.yWater, 0);
				}
			}
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00004D14 File Offset: 0x00002F14
	public void paintFar(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool flag = this.typeEff == 4;
		if (flag)
		{
			for (int i = 0; i < this.sum; i++)
			{
				g.drawRegion(BackgroudEffect.imgSao, 0, 16 * this.frame[i], 16, 16, 0, this.x[i], this.y[i], 0);
			}
		}
		bool flag2 = this.typeEff == 9;
		if (flag2)
		{
			g.setColor(16777215);
			for (int j = 0; j < this.num; j++)
			{
				g.drawImage((BackgroudEffect.wP[j] != 1) ? BackgroudEffect.imgChamTron2 : BackgroudEffect.imgChamTron1, this.x[j], this.y[j], 3);
			}
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00004DF8 File Offset: 0x00002FF8
	public void update()
	{
		try
		{
			switch (this.typeEff)
			{
			case 0:
			case 12:
				for (int i = 0; i < this.sum; i++)
				{
					bool flag = i % 3 != 0 && this.typeEff != 12 && TileMap.tileTypeAt(this.x[i], this.y[i] - GameCanvas.transY, 2);
					if (flag)
					{
						this.activeEff[i] = true;
					}
					bool flag2 = i % 3 == 0 && this.y[i] > GameCanvas.h + GameScr.cmy;
					if (flag2)
					{
						this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
						this.y[i] = Res.random(-100, 0) + GameScr.cmy;
					}
					bool flag3 = !this.activeEff[i];
					if (flag3)
					{
						this.y[i] += this.vy[i];
						this.x[i] += this.vx[i];
					}
					bool flag4 = this.activeEff[i];
					if (flag4)
					{
						this.t[i]++;
						bool flag5 = this.t[i] > 2;
						if (flag5)
						{
							this.frame[i]++;
							this.t[i] = 0;
							bool flag6 = this.frame[i] > 1;
							if (flag6)
							{
								this.frame[i] = 0;
								this.activeEff[i] = false;
								this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
								this.y[i] = Res.random(-100, 0) + GameScr.cmy;
							}
						}
					}
				}
				break;
			case 1:
			case 2:
			case 5:
			case 6:
			case 7:
			case 11:
			case 15:
				for (int j = 0; j < this.sum; j++)
				{
					bool flag7 = j % 3 != 0 && TileMap.tileTypeAt(this.x[j], this.y[j] + ((TileMap.tileID != 15) ? 0 : 10), 2);
					if (flag7)
					{
						this.activeEff[j] = true;
					}
					bool flag8 = j % 3 == 0 && this.y[j] > TileMap.pxh;
					if (flag8)
					{
						this.x[j] = Res.random(-10, TileMap.pxw + 50);
						this.y[j] = Res.random(-50, 0);
					}
					bool flag9 = !this.activeEff[j];
					if (flag9)
					{
						for (int k = 0; k < Teleport.vTeleport.size(); k++)
						{
							Teleport teleport = (Teleport)Teleport.vTeleport.elementAt(k);
							bool flag10 = teleport != null && teleport.paintFire && this.x[j] < teleport.x + 80 && this.x[j] > teleport.x - 80 && this.y[j] < teleport.y + 80 && this.y[j] > teleport.y - 80;
							if (flag10)
							{
								this.x[j] += ((this.x[j] >= teleport.x) ? 10 : -10);
							}
						}
						this.y[j] += this.vy[j];
						this.x[j] += this.vx[j];
						this.t[j]++;
						int num3 = (this.typeEff != 11) ? 4 : 3;
						int num2 = (this.typeEff != 15) ? 4 : 4;
						bool flag11 = this.t[j] > ((this.typeEff == 2) ? 4 : 2);
						if (flag11)
						{
							bool flag12 = this.typeEff != 11 && this.typeEff != 15;
							if (flag12)
							{
								this.frame[j]++;
							}
							this.t[j] = 0;
							bool flag13 = this.frame[j] > num2 - 1;
							if (flag13)
							{
								this.frame[j] = 0;
							}
						}
					}
					else
					{
						this.t[j]++;
						bool flag14 = this.t[j] == 100;
						if (flag14)
						{
							this.t[j] = 0;
							this.x[j] = Res.random(-10, TileMap.pxw + 50);
							this.y[j] = Res.random(-50, 0);
							this.activeEff[j] = false;
						}
					}
				}
				break;
			case 4:
				for (int l = 0; l < this.sum; l++)
				{
					this.t[l]++;
					bool flag15 = this.t[l] > 10;
					if (flag15)
					{
						this.tick[l]++;
						this.t[l] = 0;
						bool flag16 = this.tick[l] > 5;
						if (flag16)
						{
							this.tick[l] = 0;
						}
						this.frame[l] = this.dem[this.tick[l]];
					}
				}
				break;
			case 8:
			{
				this.tFire++;
				bool flag17 = this.tFire == 3;
				if (flag17)
				{
					this.tFire = 0;
					this.frameFire++;
					bool flag18 = this.frameFire > 1;
					if (flag18)
					{
						this.frameFire = 0;
					}
				}
				bool flag19 = GameCanvas.gameTick % this.tStart == 0;
				if (flag19)
				{
					this.isFly = true;
				}
				bool flag20 = this.isFly;
				if (flag20)
				{
					bool flag21 = this.way == 1;
					if (flag21)
					{
						this.xShip += this.speed;
						bool flag22 = this.xShip > TileMap.pxw + 50;
						if (flag22)
						{
							this.reloadShip();
						}
					}
					else
					{
						bool flag23 = this.way == 2;
						if (flag23)
						{
							this.xShip -= this.speed;
							bool flag24 = this.xShip < -50;
							if (flag24)
							{
								this.reloadShip();
							}
						}
						else
						{
							bool flag25 = this.way == 3;
							if (flag25)
							{
								this.yShip += this.speed;
								bool flag26 = this.yShip > TileMap.pxh + 50;
								if (flag26)
								{
									this.reloadShip();
								}
							}
							else
							{
								bool flag27 = this.way == 4;
								if (flag27)
								{
									this.yShip -= this.speed;
									bool flag28 = this.yShip < -50;
									if (flag28)
									{
										this.reloadShip();
									}
								}
							}
						}
					}
				}
				break;
			}
			case 9:
				for (int m = 0; m < this.num; m++)
				{
					this.x[m] -= this.vx[m];
					bool flag29 = this.x[m] < -this.vx[m];
					if (flag29)
					{
						BackgroudEffect.wP[m] = Res.abs(Res.random(1, 3));
						this.vx[m] = BackgroudEffect.wP[m];
						this.x[m] = GameCanvas.w + this.vx[m];
					}
				}
				break;
			case 10:
				for (int n = 0; n < this.num; n++)
				{
					this.x[n] -= this.vx[n];
					bool flag30 = this.x[n] < -this.vx[n] + GameScr.cmx;
					if (flag30)
					{
						this.x[n] = GameCanvas.w + this.vx[n] + GameScr.cmx;
					}
				}
				break;
			case 13:
				BackgroudEffect.updateCloud2();
				break;
			case 14:
				BackgroudEffect.updateFog();
				break;
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00005664 File Offset: 0x00003864
	public void paintFront(mGraphics g)
	{
		try
		{
			switch (this.typeEff)
			{
			case 0:
			case 12:
			{
				int cmx = GameScr.cmx;
				int cmy = GameScr.cmy;
				for (int i = 0; i < this.sum; i++)
				{
					bool flag = this.type[i] == 2;
					if (flag)
					{
						bool flag2 = this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy;
						if (flag2)
						{
							bool flag3 = this.activeEff[i];
							if (flag3)
							{
								g.drawRegion(BackgroudEffect.imgHatMua, 0, 10 * this.frame[i], 13, 10, 0, this.x[i], this.y[i] - 10, 0);
							}
							else
							{
								g.drawImage(BackgroudEffect.imgMua1, this.x[i], this.y[i], 0);
							}
						}
					}
				}
				break;
			}
			case 1:
			case 2:
			case 5:
			case 6:
			case 7:
			case 11:
			case 15:
			{
				bool flag4 = this.typeEff == 15;
				if (flag4)
				{
					bool flag5 = SmallImage.imgNew[11120] != null && SmallImage.imgNew[11120].img != null;
					if (flag5)
					{
						BackgroudEffect.imgLacay = SmallImage.imgNew[11120].img;
					}
					bool flag6 = BackgroudEffect.imgLacay == null;
					if (flag6)
					{
						break;
					}
				}
				this.paintLacay1(g, BackgroudEffect.imgLacay);
				break;
			}
			case 13:
			{
				bool flag7 = !BackgroudEffect.isPaintFar;
				if (flag7)
				{
					BackgroudEffect.paintCloud2(g);
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00005888 File Offset: 0x00003A88
	public void paintLacay1(mGraphics g, Image img)
	{
		int num = (this.typeEff != 11) ? 4 : 3;
		int num2 = (this.typeEff != 15) ? 4 : 4;
		for (int i = 0; i < this.sum; i++)
		{
			bool flag = i % 3 == 0;
			if (flag)
			{
				bool flag2 = this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy;
				if (flag2)
				{
					bool flag3 = img != null;
					if (flag3)
					{
						g.drawRegion(img, 0, BackgroudEffect.PIXEL * this.frame[i], img.getWidth(), BackgroudEffect.PIXEL, 0, this.x[i], this.y[i], 0);
					}
				}
			}
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00005978 File Offset: 0x00003B78
	public void paintLacay2(mGraphics g, Image img)
	{
		int num = (this.typeEff != 11) ? 4 : 3;
		int num2 = (this.typeEff != 15) ? 4 : 4;
		for (int i = 0; i < this.sum; i++)
		{
			bool flag = i % 3 != 0;
			if (flag)
			{
				bool flag2 = this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy;
				if (flag2)
				{
					bool flag3 = img != null;
					if (flag3)
					{
						g.drawRegion(img, 0, BackgroudEffect.PIXEL * this.frame[i], img.getWidth(), BackgroudEffect.PIXEL, 0, this.x[i], this.y[i], 0);
					}
				}
			}
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00005A68 File Offset: 0x00003C68
	public void paintBehindTile(mGraphics g)
	{
		int num = this.typeEff;
		bool flag = num != 8;
		if (flag)
		{
			bool flag2 = num == 13;
			if (flag2)
			{
				bool flag3 = BackgroudEffect.isPaintFar;
				if (flag3)
				{
					BackgroudEffect.paintCloud2(g);
				}
			}
		}
		else
		{
			g.drawRegion(BackgroudEffect.imgShip, 0, 0, BackgroudEffect.imgShip.getWidth(), BackgroudEffect.imgShip.getHeight(), this.trans, this.xShip, this.yShip, 3);
			bool flag4 = this.way == 1 || this.way == 2;
			if (flag4)
			{
				int num2 = (this.trans != 0) ? 25 : -25;
				g.drawRegion(BackgroudEffect.imgFire1, 0, this.frameFire * 8, 20, 8, this.trans, this.xShip + num2, this.yShip + 5, 3);
			}
			else
			{
				int num3 = (this.trans != 0) ? -11 : 11;
				g.drawRegion(BackgroudEffect.imgFire2, 0, this.frameFire * 18, 8, 18, this.trans, this.xShip + num3, this.yShip + 22, 3);
			}
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00005B88 File Offset: 0x00003D88
	public void paintBack(mGraphics g)
	{
		switch (this.typeEff)
		{
		case 0:
		{
			int cmx = GameScr.cmx;
			int cmy = GameScr.cmy;
			g.setColor(10742731);
			for (int i = 0; i < this.sum; i++)
			{
				bool flag = this.type[i] != 2;
				if (flag)
				{
					bool flag2 = this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy;
					if (flag2)
					{
						g.drawImage(BackgroudEffect.imgMua2, this.x[i], this.y[i], 0);
					}
				}
			}
			break;
		}
		case 1:
		case 2:
		case 5:
		case 6:
		case 7:
		case 11:
		case 15:
		{
			bool flag3 = this.typeEff == 15;
			if (flag3)
			{
				bool flag4 = SmallImage.imgNew[11120] != null && SmallImage.imgNew[11120].img != null;
				if (flag4)
				{
					BackgroudEffect.imgLacay = SmallImage.imgNew[11120].img;
				}
				bool flag5 = BackgroudEffect.imgLacay == null;
				if (flag5)
				{
					break;
				}
			}
			this.paintLacay2(g, BackgroudEffect.imgLacay);
			break;
		}
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00005D2C File Offset: 0x00003F2C
	public static void addEffect(int id)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			BackgroudEffect o = new BackgroudEffect(id);
			BackgroudEffect.vBgEffect.addElement(o);
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00005D5C File Offset: 0x00003F5C
	public static void addWater(int color, int yWater)
	{
		BackgroudEffect backgroudEffect = new BackgroudEffect(10);
		backgroudEffect.colorWater = color;
		backgroudEffect.yWater = yWater;
		BackgroudEffect.vBgEffect.addElement(backgroudEffect);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005D8C File Offset: 0x00003F8C
	public static void paintWaterAll(mGraphics g)
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintWater(g);
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00005DCC File Offset: 0x00003FCC
	public static void paintBehindTileAll(mGraphics g)
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintBehindTile(g);
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00005E0C File Offset: 0x0000400C
	public static void paintFrontAll(mGraphics g)
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintFront(g);
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00005E4C File Offset: 0x0000404C
	public static void paintFarAll(mGraphics g)
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintFar(g);
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00005E8C File Offset: 0x0000408C
	public static void paintBackAll(mGraphics g)
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintBack(g);
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00005ECC File Offset: 0x000040CC
	public static void updateEff()
	{
		for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
		{
			((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).update();
		}
	}

	// Token: 0x0400004F RID: 79
	public static MyVector vBgEffect = new MyVector();

	// Token: 0x04000050 RID: 80
	private int[] x;

	// Token: 0x04000051 RID: 81
	private int[] y;

	// Token: 0x04000052 RID: 82
	private int[] vx;

	// Token: 0x04000053 RID: 83
	private int[] vy;

	// Token: 0x04000054 RID: 84
	public static int[] wP;

	// Token: 0x04000055 RID: 85
	private int num;

	// Token: 0x04000056 RID: 86
	private int xShip;

	// Token: 0x04000057 RID: 87
	private int yShip;

	// Token: 0x04000058 RID: 88
	private int way;

	// Token: 0x04000059 RID: 89
	private int trans;

	// Token: 0x0400005A RID: 90
	private int frameFire;

	// Token: 0x0400005B RID: 91
	private int tFire;

	// Token: 0x0400005C RID: 92
	private int tStart;

	// Token: 0x0400005D RID: 93
	private int speed;

	// Token: 0x0400005E RID: 94
	private bool isFly;

	// Token: 0x0400005F RID: 95
	public static Image imgSnow;

	// Token: 0x04000060 RID: 96
	public static Image imgHatMua;

	// Token: 0x04000061 RID: 97
	public static Image imgMua1;

	// Token: 0x04000062 RID: 98
	public static Image imgMua2;

	// Token: 0x04000063 RID: 99
	public static Image imgSao;

	// Token: 0x04000064 RID: 100
	private static Image imgLacay;

	// Token: 0x04000065 RID: 101
	private static Image imgShip;

	// Token: 0x04000066 RID: 102
	private static Image imgFire1;

	// Token: 0x04000067 RID: 103
	private static Image imgFire2;

	// Token: 0x04000068 RID: 104
	private int[] type;

	// Token: 0x04000069 RID: 105
	private int sum;

	// Token: 0x0400006A RID: 106
	public int typeEff;

	// Token: 0x0400006B RID: 107
	public int xx;

	// Token: 0x0400006C RID: 108
	public int waterY;

	// Token: 0x0400006D RID: 109
	private bool[] isRainEffect;

	// Token: 0x0400006E RID: 110
	private int[] frame;

	// Token: 0x0400006F RID: 111
	private int[] t;

	// Token: 0x04000070 RID: 112
	private bool[] activeEff;

	// Token: 0x04000071 RID: 113
	private int yWater;

	// Token: 0x04000072 RID: 114
	private int colorWater;

	// Token: 0x04000073 RID: 115
	public const int TYPE_MUA = 0;

	// Token: 0x04000074 RID: 116
	public const int TYPE_LATRAIDAT_1 = 1;

	// Token: 0x04000075 RID: 117
	public const int TYPE_LATRAIDAT_2 = 2;

	// Token: 0x04000076 RID: 118
	public const int TYPE_SAMSET = 3;

	// Token: 0x04000077 RID: 119
	public const int TYPE_SAO = 4;

	// Token: 0x04000078 RID: 120
	public const int TYPE_LANAMEK_1 = 5;

	// Token: 0x04000079 RID: 121
	public const int TYPE_LASAYAI_1 = 6;

	// Token: 0x0400007A RID: 122
	public const int TYPE_LANAMEK_2 = 7;

	// Token: 0x0400007B RID: 123
	public const int TYPE_SHIP_TRAIDAT = 8;

	// Token: 0x0400007C RID: 124
	public const int TYPE_HANHTINH = 9;

	// Token: 0x0400007D RID: 125
	public const int TYPE_WATER = 10;

	// Token: 0x0400007E RID: 126
	public const int TYPE_SNOW = 11;

	// Token: 0x0400007F RID: 127
	public const int TYPE_MUA_FRONT = 12;

	// Token: 0x04000080 RID: 128
	public const int TYPE_CLOUD = 13;

	// Token: 0x04000081 RID: 129
	public const int TYPE_FOG = 14;

	// Token: 0x04000082 RID: 130
	public const int TYPE_LUNAR_YEAR = 15;

	// Token: 0x04000083 RID: 131
	public static int PIXEL = 16;

	// Token: 0x04000084 RID: 132
	public static Image water1 = GameCanvas.loadImage("/mainImage/myTexture2dwater1.png");

	// Token: 0x04000085 RID: 133
	public static Image water2 = GameCanvas.loadImage("/mainImage/myTexture2dwater2.png");

	// Token: 0x04000086 RID: 134
	public static Image imgChamTron1;

	// Token: 0x04000087 RID: 135
	public static Image imgChamTron2;

	// Token: 0x04000088 RID: 136
	public static short id_water1;

	// Token: 0x04000089 RID: 137
	public static short id_water2;

	// Token: 0x0400008A RID: 138
	public static Image water3 = null;

	// Token: 0x0400008B RID: 139
	public static bool isFog;

	// Token: 0x0400008C RID: 140
	public static bool isPaintFar;

	// Token: 0x0400008D RID: 141
	public static int nCloud;

	// Token: 0x0400008E RID: 142
	public static Image imgCloud1;

	// Token: 0x0400008F RID: 143
	public static Image imgFog;

	// Token: 0x04000090 RID: 144
	public static int cloudw;

	// Token: 0x04000091 RID: 145
	public static int xfog;

	// Token: 0x04000092 RID: 146
	public static int yfog;

	// Token: 0x04000093 RID: 147
	public static int fogw;

	// Token: 0x04000094 RID: 148
	private int[] dem = new int[]
	{
		0,
		1,
		2,
		1,
		0,
		0
	};

	// Token: 0x04000095 RID: 149
	private int[] tick;
}
