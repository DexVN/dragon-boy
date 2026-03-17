using System;

// Token: 0x02000070 RID: 112
public class MotherCanvas
{
	// Token: 0x0600059A RID: 1434 RVA: 0x00067540 File Offset: 0x00065740
	public MotherCanvas()
	{
		this.checkZoomLevel(this.getWidth(), this.getHeight());
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0006756C File Offset: 0x0006576C
	public void checkZoomLevel(int w, int h)
	{
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			mGraphics.zoomLevel = 2;
			bool flag = w * h >= 2073600;
			if (flag)
			{
				mGraphics.zoomLevel = 4;
			}
			else
			{
				bool flag2 = w * h > 384000;
				if (flag2)
				{
					mGraphics.zoomLevel = 3;
				}
			}
		}
		else
		{
			bool flag3 = !Main.isPC;
			if (flag3)
			{
				bool isIpod = Main.isIpod;
				if (isIpod)
				{
					mGraphics.zoomLevel = 2;
				}
				else
				{
					bool flag4 = w * h >= 2073600;
					if (flag4)
					{
						mGraphics.zoomLevel = 4;
					}
					else
					{
						bool flag5 = w * h >= 691200;
						if (flag5)
						{
							mGraphics.zoomLevel = 3;
						}
						else
						{
							bool flag6 = w * h > 153600;
							if (flag6)
							{
								mGraphics.zoomLevel = 2;
							}
						}
					}
				}
			}
			else
			{
				mGraphics.zoomLevel = 2;
				bool flag7 = w * h < 480000;
				if (flag7)
				{
					mGraphics.zoomLevel = 1;
				}
			}
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00067658 File Offset: 0x00065858
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00067670 File Offset: 0x00065870
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00067688 File Offset: 0x00065888
	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00067692 File Offset: 0x00065892
	protected void paint(mGraphics g)
	{
		this.tCanvas.paint(g);
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x000676A2 File Offset: 0x000658A2
	protected void keyPressed(int keyCode)
	{
		this.tCanvas.keyPressedz(keyCode);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x000676B2 File Offset: 0x000658B2
	protected void keyReleased(int keyCode)
	{
		this.tCanvas.keyReleasedz(keyCode);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000676C2 File Offset: 0x000658C2
	protected void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerDragged(x, y);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x000676E5 File Offset: 0x000658E5
	protected void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerPressed(x, y);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00067708 File Offset: 0x00065908
	protected void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerReleased(x, y);
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0006772C File Offset: 0x0006592C
	public int getWidthz()
	{
		int width = this.getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00067754 File Offset: 0x00065954
	public int getHeightz()
	{
		int height = this.getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	// Token: 0x04000C02 RID: 3074
	public static MotherCanvas instance;

	// Token: 0x04000C03 RID: 3075
	public GameCanvas tCanvas;

	// Token: 0x04000C04 RID: 3076
	public int zoomLevel = 1;

	// Token: 0x04000C05 RID: 3077
	public Image imgCache;

	// Token: 0x04000C06 RID: 3078
	private int[] imgRGBCache;

	// Token: 0x04000C07 RID: 3079
	private int newWidth;

	// Token: 0x04000C08 RID: 3080
	private int newHeight;

	// Token: 0x04000C09 RID: 3081
	private int[] output;

	// Token: 0x04000C0A RID: 3082
	private int OUTPUTSIZE = 20;
}
