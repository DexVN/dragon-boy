using System;

// Token: 0x02000095 RID: 149
public class Scroll
{
	// Token: 0x06000827 RID: 2087 RVA: 0x00090994 File Offset: 0x0008EB94
	public void clear()
	{
		this.cmtoX = 0;
		this.cmtoY = 0;
		this.cmx = 0;
		this.cmy = 0;
		this.cmvx = 0;
		this.cmvy = 0;
		this.cmdx = 0;
		this.cmdy = 0;
		this.cmxLim = 0;
		this.cmyLim = 0;
		this.width = 0;
		this.height = 0;
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x000909F8 File Offset: 0x0008EBF8
	public ScrollResult updateKey()
	{
		bool flag = this.styleUPDOWN;
		ScrollResult result;
		if (flag)
		{
			result = this.updateKeyScrollUpDown(false);
		}
		else
		{
			result = this.updateKeyScrollLeftRight();
		}
		return result;
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00090A28 File Offset: 0x0008EC28
	public ScrollResult updateKey(bool isGetSelectNow)
	{
		bool flag = this.styleUPDOWN;
		ScrollResult result;
		if (flag)
		{
			result = this.updateKeyScrollUpDown(isGetSelectNow);
		}
		else
		{
			result = this.updateKeyScrollLeftRight();
		}
		return result;
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x00090A58 File Offset: 0x0008EC58
	private ScrollResult updateKeyScrollUpDown(bool isGetNow)
	{
		int num = this.xPos;
		int num2 = this.yPos;
		int w = this.width;
		int h = this.height;
		bool isPointerDown = GameCanvas.isPointerDown;
		if (isPointerDown)
		{
			bool flag = !this.pointerIsDowning && GameCanvas.isPointer(num, num2, w, h);
			if (flag)
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.py;
				}
				this.pointerDownFirstX = GameCanvas.py;
				this.pointerIsDowning = true;
				bool flag2 = !isGetNow;
				if (flag2)
				{
					this.selectedItem = -1;
				}
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else
			{
				bool flag3 = this.pointerIsDowning;
				if (flag3)
				{
					this.pointerDownTime++;
					bool flag4 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning;
					if (flag4)
					{
						this.pointerDownFirstX = -1000;
						bool flag5 = this.ITEM_PER_LINE > 1;
						if (flag5)
						{
							int num3 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
							int num4 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
							this.selectedItem = num3 * this.ITEM_PER_LINE + num4;
						}
						else
						{
							this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
						}
					}
					int num5 = GameCanvas.py - this.pointerDownLastX[0];
					bool flag6 = !isGetNow;
					if (flag6)
					{
						bool flag7 = num5 != 0 && this.selectedItem != -1;
						if (flag7)
						{
							this.selectedItem = -1;
						}
					}
					else
					{
						this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					}
					for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
					{
						this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.py;
					this.cmtoY -= num5;
					bool flag8 = this.cmtoY < 0;
					if (flag8)
					{
						this.cmtoY = 0;
					}
					bool flag9 = this.cmtoY > this.cmyLim;
					if (flag9)
					{
						this.cmtoY = this.cmyLim;
					}
					bool flag10 = this.cmy < 0 || this.cmy > this.cmyLim;
					if (flag10)
					{
						num5 /= 2;
					}
					this.cmy -= num5;
				}
			}
		}
		bool isFinish = false;
		bool flag11 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
		if (flag11)
		{
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			bool flag12 = Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
			if (flag12)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				bool flag13 = this.ITEM_PER_LINE > 1;
				if (flag13)
				{
					int num6 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					int num7 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					this.selectedItem = num6 * this.ITEM_PER_LINE + num7;
				}
				else
				{
					this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
				}
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else
			{
				bool flag14 = this.selectedItem != -1 && this.pointerDownTime > 5;
				if (flag14)
				{
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else
				{
					bool flag15 = (this.selectedItem == -1 && !this.isDownWhenRunning) || (isGetNow && this.selectedItem != -1 && !this.isDownWhenRunning);
					if (flag15)
					{
						bool flag16 = this.cmy < 0;
						if (flag16)
						{
							this.cmtoY = 0;
						}
						else
						{
							bool flag17 = this.cmy > this.cmyLim;
							if (flag17)
							{
								this.cmtoY = this.cmyLim;
							}
							else
							{
								int num8 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
								bool flag18 = num8 > 10;
								if (flag18)
								{
									num8 = 10;
								}
								else
								{
									bool flag19 = num8 < -10;
									if (flag19)
									{
										num8 = -10;
									}
									else
									{
										num8 = 0;
									}
								}
								this.cmRun = -num8 * 100;
							}
						}
					}
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = isFinish,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00090F58 File Offset: 0x0008F158
	private ScrollResult updateKeyScrollLeftRight()
	{
		int num = this.xPos;
		int y = this.yPos;
		int w = this.width;
		int h = this.height;
		bool isPointerDown = GameCanvas.isPointerDown;
		if (isPointerDown)
		{
			bool flag = !this.pointerIsDowning && GameCanvas.isPointer(num, y, w, h);
			if (flag)
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.px;
				}
				this.pointerDownFirstX = GameCanvas.px;
				this.pointerIsDowning = true;
				this.selectedItem = -1;
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else
			{
				bool flag2 = this.pointerIsDowning;
				if (flag2)
				{
					this.pointerDownTime++;
					bool flag3 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning;
					if (flag3)
					{
						this.pointerDownFirstX = -1000;
						this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					}
					int num2 = GameCanvas.px - this.pointerDownLastX[0];
					bool flag4 = num2 != 0 && this.selectedItem != -1;
					if (flag4)
					{
						this.selectedItem = -1;
					}
					for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
					{
						this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.px;
					this.cmtoX -= num2;
					bool flag5 = this.cmtoX < 0;
					if (flag5)
					{
						this.cmtoX = 0;
					}
					bool flag6 = this.cmtoX > this.cmxLim;
					if (flag6)
					{
						this.cmtoX = this.cmxLim;
					}
					bool flag7 = this.cmx < 0 || this.cmx > this.cmxLim;
					if (flag7)
					{
						num2 /= 2;
					}
					this.cmx -= num2;
				}
			}
		}
		bool isFinish = false;
		bool flag8 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
		if (flag8)
		{
			int i2 = GameCanvas.px - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			bool flag9 = Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
			if (flag9)
			{
				this.cmRun = 0;
				this.cmtoX = this.cmx;
				this.pointerDownFirstX = -1000;
				this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else
			{
				bool flag10 = this.selectedItem != -1 && this.pointerDownTime > 5;
				if (flag10)
				{
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else
				{
					bool flag11 = this.selectedItem == -1 && !this.isDownWhenRunning;
					if (flag11)
					{
						bool flag12 = this.cmx < 0;
						if (flag12)
						{
							this.cmtoX = 0;
						}
						else
						{
							bool flag13 = this.cmx > this.cmxLim;
							if (flag13)
							{
								this.cmtoX = this.cmxLim;
							}
							else
							{
								int num3 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
								bool flag14 = num3 > 10;
								if (flag14)
								{
									num3 = 10;
								}
								else
								{
									bool flag15 = num3 < -10;
									if (flag15)
									{
										num3 = -10;
									}
									else
									{
										num3 = 0;
									}
								}
								this.cmRun = -num3 * 100;
							}
						}
					}
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = isFinish,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0009135C File Offset: 0x0008F55C
	public void updatecm()
	{
		bool flag = this.cmRun != 0 && !this.pointerIsDowning;
		if (flag)
		{
			bool flag2 = this.styleUPDOWN;
			if (flag2)
			{
				this.cmtoY += this.cmRun / 100;
				bool flag3 = this.cmtoY < 0;
				if (flag3)
				{
					this.cmtoY = 0;
				}
				else
				{
					bool flag4 = this.cmtoY > this.cmyLim;
					if (flag4)
					{
						this.cmtoY = this.cmyLim;
					}
					else
					{
						this.cmy = this.cmtoY;
					}
				}
			}
			else
			{
				this.cmtoX += this.cmRun / 100;
				bool flag5 = this.cmtoX < 0;
				if (flag5)
				{
					this.cmtoX = 0;
				}
				else
				{
					bool flag6 = this.cmtoX > this.cmxLim;
					if (flag6)
					{
						this.cmtoX = this.cmxLim;
					}
					else
					{
						this.cmx = this.cmtoX;
					}
				}
			}
			this.cmRun = this.cmRun * 9 / 10;
			bool flag7 = this.cmRun < 100 && this.cmRun > -100;
			if (flag7)
			{
				this.cmRun = 0;
			}
		}
		bool flag8 = this.cmx != this.cmtoX && !this.pointerIsDowning;
		if (flag8)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		bool flag9 = this.cmy != this.cmtoY && !this.pointerIsDowning;
		if (flag9)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0009156C File Offset: 0x0008F76C
	public void setStyle(int nItem, int ITEM_SIZE, int xPos, int yPos, int width, int height, bool styleUPDOWN, int ITEM_PER_LINE)
	{
		this.xPos = xPos;
		this.yPos = yPos;
		this.ITEM_SIZE = ITEM_SIZE;
		this.nITEM = nItem;
		this.width = width;
		this.height = height;
		this.styleUPDOWN = styleUPDOWN;
		this.ITEM_PER_LINE = ITEM_PER_LINE;
		Res.outz(string.Concat(new object[]
		{
			"nItem= ",
			nItem,
			" ITEMSIZE= ",
			ITEM_SIZE,
			" heghit= ",
			height
		}));
		if (styleUPDOWN)
		{
			int num = nItem / ITEM_PER_LINE;
			bool flag = nItem % ITEM_PER_LINE != 0;
			if (flag)
			{
				num++;
			}
			this.cmyLim = num * ITEM_SIZE - height;
		}
		else
		{
			this.cmxLim = ITEM_PER_LINE * ITEM_SIZE - width;
		}
		bool flag2 = this.cmyLim < 0;
		if (flag2)
		{
			this.cmyLim = 0;
		}
		bool flag3 = this.cmxLim < 0;
		if (flag3)
		{
			this.cmxLim = 0;
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00091668 File Offset: 0x0008F868
	public void moveTo(int to)
	{
		bool flag = this.styleUPDOWN;
		if (flag)
		{
			to -= (this.height - this.ITEM_SIZE) / 2;
			this.cmtoY = to;
			bool flag2 = this.cmtoY < 0;
			if (flag2)
			{
				this.cmtoY = 0;
			}
			bool flag3 = this.cmtoY > this.cmyLim;
			if (flag3)
			{
				this.cmtoY = this.cmyLim;
			}
		}
		else
		{
			to -= (this.width - this.ITEM_SIZE) / 2;
			this.cmtoX = to;
			bool flag4 = this.cmtoX < 0;
			if (flag4)
			{
				this.cmtoX = 0;
			}
			bool flag5 = this.cmtoX > this.cmxLim;
			if (flag5)
			{
				this.cmtoX = this.cmxLim;
			}
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00091728 File Offset: 0x0008F928
	public static Scroll gIz()
	{
		bool flag = Scroll.gI == null;
		if (flag)
		{
			Scroll.gI = new Scroll();
		}
		return Scroll.gI;
	}

	// Token: 0x04001093 RID: 4243
	public int cmtoX;

	// Token: 0x04001094 RID: 4244
	public int cmtoY;

	// Token: 0x04001095 RID: 4245
	public int cmx;

	// Token: 0x04001096 RID: 4246
	public int cmy;

	// Token: 0x04001097 RID: 4247
	public int cmvx;

	// Token: 0x04001098 RID: 4248
	public int cmvy;

	// Token: 0x04001099 RID: 4249
	public int cmdx;

	// Token: 0x0400109A RID: 4250
	public int cmdy;

	// Token: 0x0400109B RID: 4251
	public int xPos;

	// Token: 0x0400109C RID: 4252
	public int yPos;

	// Token: 0x0400109D RID: 4253
	public int width;

	// Token: 0x0400109E RID: 4254
	public int height;

	// Token: 0x0400109F RID: 4255
	public int cmxLim;

	// Token: 0x040010A0 RID: 4256
	public int cmyLim;

	// Token: 0x040010A1 RID: 4257
	public static Scroll gI;

	// Token: 0x040010A2 RID: 4258
	private int pointerDownTime;

	// Token: 0x040010A3 RID: 4259
	private int pointerDownFirstX;

	// Token: 0x040010A4 RID: 4260
	private int[] pointerDownLastX = new int[3];

	// Token: 0x040010A5 RID: 4261
	public bool pointerIsDowning;

	// Token: 0x040010A6 RID: 4262
	public bool isDownWhenRunning;

	// Token: 0x040010A7 RID: 4263
	private int cmRun;

	// Token: 0x040010A8 RID: 4264
	public int selectedItem;

	// Token: 0x040010A9 RID: 4265
	public int ITEM_SIZE;

	// Token: 0x040010AA RID: 4266
	public int nITEM;

	// Token: 0x040010AB RID: 4267
	public int ITEM_PER_LINE;

	// Token: 0x040010AC RID: 4268
	public bool styleUPDOWN = true;
}
