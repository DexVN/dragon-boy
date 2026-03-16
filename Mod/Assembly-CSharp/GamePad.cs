using System;

// Token: 0x020000CE RID: 206
public class GamePad
{
	// Token: 0x06000A99 RID: 2713 RVA: 0x000A0E18 File Offset: 0x0009F218
	public GamePad()
	{
		this.R = 28;
		if (GameCanvas.w < 300)
		{
			this.isSmallGamePad = true;
			this.isMediumGamePad = false;
			this.isLargeGamePad = false;
		}
		if (GameCanvas.w >= 300 && GameCanvas.w <= 380)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = true;
			this.isLargeGamePad = false;
		}
		if (GameCanvas.w > 380)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = false;
			this.isLargeGamePad = true;
		}
		if (!this.isLargeGamePad)
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h - 80;
		}
		else
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw / 4 * 3 - 20;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h;
			if (mSystem.clientType == 2)
			{
				this.xZone = 0;
				this.yZone = (GameCanvas.h >> 1) + 40;
				this.wZone = GameCanvas.hw / 4 * 3 - 40;
				this.hZone = GameCanvas.h;
			}
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x000A0F5C File Offset: 0x0009F35C
	public void update()
	{
		try
		{
			if (GameScr.isAnalog != 0)
			{
				if (GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease)
				{
					this.xTemp = GameCanvas.pxFirst;
					this.yTemp = GameCanvas.pyFirst;
					if (this.xTemp >= this.xZone && this.xTemp <= this.wZone && this.yTemp >= this.yZone && this.yTemp <= this.hZone)
					{
						if (!this.isGamePad)
						{
							this.xC = (this.xM = this.xTemp);
							this.yC = (this.yM = this.yTemp);
						}
						this.isGamePad = true;
						this.deltaX = GameCanvas.px - this.xC;
						this.deltaY = GameCanvas.py - this.yC;
						this.delta = global::Math.pow(this.deltaX, 2) + global::Math.pow(this.deltaY, 2);
						this.d = Res.sqrt(this.delta);
						if (global::Math.abs(this.deltaX) > 4 || global::Math.abs(this.deltaY) > 4)
						{
							this.angle = Res.angle(this.deltaX, this.deltaY);
							if (!GameCanvas.isPointerHoldIn(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R))
							{
								if (this.d != 0)
								{
									this.yM = this.deltaY * this.R / this.d;
									this.xM = this.deltaX * this.R / this.d;
									this.xM += this.xC;
									this.yM += this.yC;
									if (!Res.inRect(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R, this.xM, this.yM))
									{
										this.xM = this.xMLast;
										this.yM = this.yMLast;
									}
									else
									{
										this.xMLast = this.xM;
										this.yMLast = this.yM;
									}
								}
								else
								{
									this.xM = this.xMLast;
									this.yM = this.yMLast;
								}
							}
							else
							{
								this.xM = GameCanvas.px;
								this.yM = GameCanvas.py;
							}
							this.resetHold();
							if (this.checkPointerMove(2))
							{
								if ((this.angle <= 360 && this.angle >= 340) || (this.angle >= 0 && this.angle <= 20))
								{
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
								else if (this.angle > 40 && this.angle < 70)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
								else if (this.angle >= 70 && this.angle <= 110)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
								}
								else if (this.angle > 110 && this.angle < 120)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
								}
								else if (this.angle >= 120 && this.angle <= 200)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
								}
								else if (this.angle > 200 && this.angle < 250)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
								}
								else if (this.angle >= 250 && this.angle <= 290)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
								}
								else if (this.angle > 290 && this.angle < 340)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
							}
							else
							{
								this.resetHold();
							}
						}
					}
				}
				else
				{
					this.xM = (this.xC = 45);
					if (!this.isLargeGamePad)
					{
						this.yM = (this.yC = GameCanvas.h - 90);
					}
					else
					{
						this.yM = (this.yC = GameCanvas.h - 45);
					}
					this.isGamePad = false;
					this.resetHold();
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x000A15B8 File Offset: 0x0009F9B8
	private bool checkPointerMove(int distance)
	{
		if (GameScr.isAnalog == 0)
		{
			return false;
		}
		if (global::Char.myCharz().statusMe == 3)
		{
			return true;
		}
		try
		{
			for (int i = 2; i > 0; i--)
			{
				int i2 = GameCanvas.arrPos[i].x - GameCanvas.arrPos[i - 1].x;
				int i3 = GameCanvas.arrPos[i].y - GameCanvas.arrPos[i - 1].y;
				if (Res.abs(i2) > distance && Res.abs(i3) > distance)
				{
					return false;
				}
			}
		}
		catch (Exception ex)
		{
		}
		return true;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x000A166C File Offset: 0x0009FA6C
	private void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x000A1674 File Offset: 0x0009FA74
	public void paint(mGraphics g)
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
		g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000A16D0 File Offset: 0x0009FAD0
	public bool disableCheckDrag()
	{
		return GameScr.isAnalog != 0 && this.isGamePad;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x000A16E4 File Offset: 0x0009FAE4
	public bool disableClickMove()
	{
		bool result;
		try
		{
			if (GameScr.isAnalog == 0)
			{
				result = false;
			}
			else
			{
				bool flag = (GameCanvas.px >= this.xZone && GameCanvas.px <= this.wZone && GameCanvas.py >= this.yZone && GameCanvas.py <= this.hZone) || GameCanvas.px >= GameCanvas.w - 50;
				result = flag;
			}
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x040013E9 RID: 5097
	private int xC;

	// Token: 0x040013EA RID: 5098
	private int yC;

	// Token: 0x040013EB RID: 5099
	private int xM;

	// Token: 0x040013EC RID: 5100
	private int yM;

	// Token: 0x040013ED RID: 5101
	private int xMLast;

	// Token: 0x040013EE RID: 5102
	private int yMLast;

	// Token: 0x040013EF RID: 5103
	private int R;

	// Token: 0x040013F0 RID: 5104
	private int r;

	// Token: 0x040013F1 RID: 5105
	private int d;

	// Token: 0x040013F2 RID: 5106
	private int xTemp;

	// Token: 0x040013F3 RID: 5107
	private int yTemp;

	// Token: 0x040013F4 RID: 5108
	private int deltaX;

	// Token: 0x040013F5 RID: 5109
	private int deltaY;

	// Token: 0x040013F6 RID: 5110
	private int delta;

	// Token: 0x040013F7 RID: 5111
	private int angle;

	// Token: 0x040013F8 RID: 5112
	public int xZone;

	// Token: 0x040013F9 RID: 5113
	public int yZone;

	// Token: 0x040013FA RID: 5114
	public int wZone;

	// Token: 0x040013FB RID: 5115
	public int hZone;

	// Token: 0x040013FC RID: 5116
	private bool isGamePad;

	// Token: 0x040013FD RID: 5117
	public bool isSmallGamePad;

	// Token: 0x040013FE RID: 5118
	public bool isMediumGamePad;

	// Token: 0x040013FF RID: 5119
	public bool isLargeGamePad;
}
