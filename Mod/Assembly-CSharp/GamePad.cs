using System;

// Token: 0x02000039 RID: 57
public class GamePad
{
	// Token: 0x060002EA RID: 746 RVA: 0x000429DC File Offset: 0x00040BDC
	public GamePad()
	{
		this.R = 28;
		bool flag = GameCanvas.w < 300;
		if (flag)
		{
			this.isSmallGamePad = true;
			this.isMediumGamePad = false;
			this.isLargeGamePad = false;
		}
		bool flag2 = GameCanvas.w >= 300 && GameCanvas.w <= 380;
		if (flag2)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = true;
			this.isLargeGamePad = false;
		}
		bool flag3 = GameCanvas.w > 380;
		if (flag3)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = false;
			this.isLargeGamePad = true;
		}
		bool flag4 = !this.isLargeGamePad;
		if (flag4)
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
			bool flag5 = mSystem.clientType == 2;
			if (flag5)
			{
				this.xZone = 0;
				this.yZone = (GameCanvas.h >> 1) + 40;
				this.wZone = GameCanvas.hw / 4 * 3 - 40;
				this.hZone = GameCanvas.h;
			}
		}
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00042B38 File Offset: 0x00040D38
	public void update()
	{
		try
		{
			bool flag = GameScr.isAnalog != 0;
			if (flag)
			{
				bool flag2 = GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease;
				if (flag2)
				{
					this.xTemp = GameCanvas.pxFirst;
					this.yTemp = GameCanvas.pyFirst;
					bool flag3 = this.xTemp >= this.xZone && this.xTemp <= this.wZone && this.yTemp >= this.yZone && this.yTemp <= this.hZone;
					if (flag3)
					{
						bool flag4 = !this.isGamePad;
						if (flag4)
						{
							this.xC = (this.xM = this.xTemp);
							this.yC = (this.yM = this.yTemp);
						}
						this.isGamePad = true;
						this.deltaX = GameCanvas.px - this.xC;
						this.deltaY = GameCanvas.py - this.yC;
						this.delta = global::Math.pow(this.deltaX, 2) + global::Math.pow(this.deltaY, 2);
						this.d = Res.sqrt(this.delta);
						bool flag5 = global::Math.abs(this.deltaX) > 4 || global::Math.abs(this.deltaY) > 4;
						if (flag5)
						{
							this.angle = Res.angle(this.deltaX, this.deltaY);
							bool flag6 = !GameCanvas.isPointerHoldIn(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R);
							if (flag6)
							{
								bool flag7 = this.d != 0;
								if (flag7)
								{
									this.yM = this.deltaY * this.R / this.d;
									this.xM = this.deltaX * this.R / this.d;
									this.xM += this.xC;
									this.yM += this.yC;
									bool flag8 = !Res.inRect(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R, this.xM, this.yM);
									if (flag8)
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
							bool flag9 = this.checkPointerMove(2);
							if (flag9)
							{
								bool flag10 = (this.angle <= 360 && this.angle >= 340) || (this.angle >= 0 && this.angle <= 20);
								if (flag10)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
								else
								{
									bool flag11 = this.angle > 40 && this.angle < 70;
									if (flag11)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
									}
									else
									{
										bool flag12 = this.angle >= 70 && this.angle <= 110;
										if (flag12)
										{
											GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
											GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
										}
										else
										{
											bool flag13 = this.angle > 110 && this.angle < 120;
											if (flag13)
											{
												GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
												GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
											}
											else
											{
												bool flag14 = this.angle >= 120 && this.angle <= 200;
												if (flag14)
												{
													GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
													GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
												}
												else
												{
													bool flag15 = this.angle > 200 && this.angle < 250;
													if (flag15)
													{
														GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
														GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
														GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
														GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
													}
													else
													{
														bool flag16 = this.angle >= 250 && this.angle <= 290;
														if (flag16)
														{
															GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
															GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
														}
														else
														{
															bool flag17 = this.angle > 290 && this.angle < 340;
															if (flag17)
															{
																GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
																GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
																GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
																GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
															}
														}
													}
												}
											}
										}
									}
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
					bool flag18 = !this.isLargeGamePad;
					if (flag18)
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

	// Token: 0x060002EC RID: 748 RVA: 0x0004318C File Offset: 0x0004138C
	private bool checkPointerMove(int distance)
	{
		bool flag = GameScr.isAnalog == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = global::Char.myCharz().statusMe == 3;
			if (flag2)
			{
				result = true;
			}
			else
			{
				try
				{
					for (int i = 2; i > 0; i--)
					{
						int i2 = GameCanvas.arrPos[i].x - GameCanvas.arrPos[i - 1].x;
						int i3 = GameCanvas.arrPos[i].y - GameCanvas.arrPos[i - 1].y;
						bool flag3 = Res.abs(i2) > distance && Res.abs(i3) > distance;
						if (flag3)
						{
							return false;
						}
					}
				}
				catch (Exception ex)
				{
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00043254 File Offset: 0x00041454
	private void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	// Token: 0x060002EE RID: 750 RVA: 0x00043260 File Offset: 0x00041460
	public void paint(mGraphics g)
	{
		bool flag = GameScr.isAnalog == 0;
		if (!flag)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x000432C4 File Offset: 0x000414C4
	public bool disableCheckDrag()
	{
		return GameScr.isAnalog != 0 && this.isGamePad;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x000432E8 File Offset: 0x000414E8
	public bool disableClickMove()
	{
		bool result;
		try
		{
			bool flag2 = GameScr.isAnalog == 0;
			if (flag2)
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

	// Token: 0x0400068E RID: 1678
	private int xC;

	// Token: 0x0400068F RID: 1679
	private int yC;

	// Token: 0x04000690 RID: 1680
	private int xM;

	// Token: 0x04000691 RID: 1681
	private int yM;

	// Token: 0x04000692 RID: 1682
	private int xMLast;

	// Token: 0x04000693 RID: 1683
	private int yMLast;

	// Token: 0x04000694 RID: 1684
	private int R;

	// Token: 0x04000695 RID: 1685
	private int r;

	// Token: 0x04000696 RID: 1686
	private int d;

	// Token: 0x04000697 RID: 1687
	private int xTemp;

	// Token: 0x04000698 RID: 1688
	private int yTemp;

	// Token: 0x04000699 RID: 1689
	private int deltaX;

	// Token: 0x0400069A RID: 1690
	private int deltaY;

	// Token: 0x0400069B RID: 1691
	private int delta;

	// Token: 0x0400069C RID: 1692
	private int angle;

	// Token: 0x0400069D RID: 1693
	public int xZone;

	// Token: 0x0400069E RID: 1694
	public int yZone;

	// Token: 0x0400069F RID: 1695
	public int wZone;

	// Token: 0x040006A0 RID: 1696
	public int hZone;

	// Token: 0x040006A1 RID: 1697
	private bool isGamePad;

	// Token: 0x040006A2 RID: 1698
	public bool isSmallGamePad;

	// Token: 0x040006A3 RID: 1699
	public bool isMediumGamePad;

	// Token: 0x040006A4 RID: 1700
	public bool isLargeGamePad;
}
