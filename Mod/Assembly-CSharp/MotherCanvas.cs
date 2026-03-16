using System;

// Token: 0x020000CF RID: 207
public class MotherCanvas
{
	// Token: 0x06000AA0 RID: 2720 RVA: 0x000A1780 File Offset: 0x0009FB80
	public MotherCanvas()
	{
		this.checkZoomLevel(this.getWidth(), this.getHeight());
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x000A17AC File Offset: 0x0009FBAC
	public void checkZoomLevel(int w, int h)
	{
		if (Main.isWindowsPhone)
		{
			mGraphics.zoomLevel = 2;
			if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
			}
			else if (w * h > 384000)
			{
				mGraphics.zoomLevel = 3;
			}
		}
		else if (!Main.isPC)
		{
			if (Main.isIpod)
			{
				mGraphics.zoomLevel = 2;
			}
			else if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
			}
			else if (w * h >= 691200)
			{
				mGraphics.zoomLevel = 3;
			}
			else if (w * h > 153600)
			{
				mGraphics.zoomLevel = 2;
			}
		}
		else
		{
			mGraphics.zoomLevel = 2;
			if (w * h < 480000)
			{
				mGraphics.zoomLevel = 1;
			}
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x000A1879 File Offset: 0x0009FC79
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x000A1881 File Offset: 0x0009FC81
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x000A1889 File Offset: 0x0009FC89
	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x000A1892 File Offset: 0x0009FC92
	protected void paint(mGraphics g)
	{
		this.tCanvas.paint(g);
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x000A18A0 File Offset: 0x0009FCA0
	protected void keyPressed(int keyCode)
	{
		this.tCanvas.keyPressedz(keyCode);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x000A18AE File Offset: 0x0009FCAE
	protected void keyReleased(int keyCode)
	{
		this.tCanvas.keyReleasedz(keyCode);
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x000A18BC File Offset: 0x0009FCBC
	protected void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerDragged(x, y);
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x000A18DD File Offset: 0x0009FCDD
	protected void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerPressed(x, y);
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x000A18FE File Offset: 0x0009FCFE
	protected void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerReleased(x, y);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x000A1920 File Offset: 0x0009FD20
	public int getWidthz()
	{
		int width = this.getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x000A1944 File Offset: 0x0009FD44
	public int getHeightz()
	{
		int height = this.getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	// Token: 0x04001400 RID: 5120
	public static MotherCanvas instance;

	// Token: 0x04001401 RID: 5121
	public GameCanvas tCanvas;

	// Token: 0x04001402 RID: 5122
	public int zoomLevel = 1;

	// Token: 0x04001403 RID: 5123
	public Image imgCache;

	// Token: 0x04001404 RID: 5124
	private int[] imgRGBCache;

	// Token: 0x04001405 RID: 5125
	private int newWidth;

	// Token: 0x04001406 RID: 5126
	private int newHeight;

	// Token: 0x04001407 RID: 5127
	private int[] output;

	// Token: 0x04001408 RID: 5128
	private int OUTPUTSIZE = 20;
}
