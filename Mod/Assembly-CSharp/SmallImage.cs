using System;
using Assets.src.e;

// Token: 0x02000087 RID: 135
public class SmallImage
{
	// Token: 0x0600045A RID: 1114 RVA: 0x00034EEC File Offset: 0x000332EC
	public SmallImage()
	{
		this.readImage();
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00034EFC File Offset: 0x000332FC
	public static void loadBigRMS()
	{
		if (SmallImage.imgbig == null)
		{
			SmallImage.imgbig = new Image[]
			{
				GameCanvas.loadImageRMS("/img/Big0.png"),
				GameCanvas.loadImageRMS("/img/Big1.png"),
				GameCanvas.loadImageRMS("/img/Big2.png"),
				GameCanvas.loadImageRMS("/img/Big3.png"),
				GameCanvas.loadImageRMS("/img/Big4.png")
			};
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00034F5F File Offset: 0x0003335F
	public static void freeBig()
	{
		SmallImage.imgbig = null;
		mSystem.gcc();
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00034F6C File Offset: 0x0003336C
	public static void loadBigImage()
	{
		SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00034F81 File Offset: 0x00033381
	public static void init()
	{
		SmallImage.instance = null;
		SmallImage.instance = new SmallImage();
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00034F93 File Offset: 0x00033393
	public void readData(byte[] data)
	{
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00034F98 File Offset: 0x00033398
	public void readImage()
	{
		int num = 0;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NR_image"));
			short num2 = dataInputStream.readShort();
			SmallImage.smallImg = new int[(int)num2][];
			for (int i = 0; i < SmallImage.smallImg.Length; i++)
			{
				SmallImage.smallImg[i] = new int[5];
			}
			for (int j = 0; j < (int)num2; j++)
			{
				num++;
				SmallImage.smallImg[j][0] = dataInputStream.readUnsignedByte();
				SmallImage.smallImg[j][1] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][2] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][3] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][4] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError3(string.Concat(new object[]
			{
				"Loi readImage: ",
				ex.ToString(),
				"i= ",
				num
			}));
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x000350A8 File Offset: 0x000334A8
	public static void clearHastable()
	{
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x000350AC File Offset: 0x000334AC
	public static void createImage(int id)
	{
		Res.outz(string.Concat(new object[]
		{
			"is request =",
			id,
			" zoom=",
			mGraphics.zoomLevel
		}));
		if (mGraphics.zoomLevel == 1)
		{
			Image image = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			if (image != null)
			{
				SmallImage.imgNew[id] = new Small(image, id);
			}
			else
			{
				SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
				Service.gI().requestIcon(id);
			}
		}
		else
		{
			Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			if (image2 != null)
			{
				SmallImage.imgNew[id] = new Small(image2, id);
			}
			else
			{
				bool flag = false;
				sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "Small" + id);
				if (array != null)
				{
					if (SmallImage.newSmallVersion != null && array.Length % 127 != (int)SmallImage.newSmallVersion[id])
					{
						flag = true;
					}
					if (!flag)
					{
						Image image3 = Image.createImage(array, 0, array.Length);
						if (image3 != null)
						{
							SmallImage.imgNew[id] = new Small(image3, id);
						}
						else
						{
							flag = true;
						}
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
					Service.gI().requestIcon(id);
				}
			}
		}
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00035224 File Offset: 0x00033624
	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
	{
		if (SmallImage.imgbig == null)
		{
			Small small = SmallImage.imgNew[id];
			if (small == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small, 0, 0, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img), transform, x, y, anchor);
			}
			return;
		}
		if (SmallImage.smallImg != null)
		{
			if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
			{
				Small small2 = SmallImage.imgNew[id];
				if (small2 == null)
				{
					SmallImage.createImage(id);
				}
				else
				{
					small2.paint(g, transform, x, y, anchor);
				}
			}
			else if (SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
			{
				g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], SmallImage.smallImg[id][1], SmallImage.smallImg[id][2], SmallImage.smallImg[id][3], SmallImage.smallImg[id][4], transform, x, y, anchor);
			}
		}
		else if (GameCanvas.currentScreen != GameScr.gI())
		{
			Small small3 = SmallImage.imgNew[id];
			if (small3 == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				small3.paint(g, transform, x, y, anchor);
			}
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00035398 File Offset: 0x00033798
	public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
	{
		if (SmallImage.imgbig == null)
		{
			Small small = SmallImage.imgNew[id];
			if (small == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small.img, 0, f * w, w, h, transform, x, y, anchor);
			}
			return;
		}
		if (SmallImage.smallImg != null)
		{
			if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
			{
				Small small2 = SmallImage.imgNew[id];
				if (small2 == null)
				{
					SmallImage.createImage(id);
				}
				else
				{
					small2.paint(g, transform, f, x, y, w, h, anchor);
				}
			}
			else if (SmallImage.smallImg[id][0] != 4 && SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
			{
				g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], 0, f * w, w, h, transform, x, y, anchor);
			}
			else
			{
				Small small3 = SmallImage.imgNew[id];
				if (small3 == null)
				{
					SmallImage.createImage(id);
				}
				else
				{
					small3.paint(g, transform, f, x, y, w, h, anchor);
				}
			}
		}
		else if (GameCanvas.currentScreen != GameScr.gI())
		{
			Small small4 = SmallImage.imgNew[id];
			if (small4 == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				small4.paint(g, transform, f, x, y, w, h, anchor);
			}
		}
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00035540 File Offset: 0x00033940
	public static void update()
	{
		int num = 0;
		if (GameCanvas.gameTick % 1000 == 0)
		{
			for (int i = 0; i < SmallImage.imgNew.Length; i++)
			{
				if (SmallImage.imgNew[i] != null)
				{
					num++;
					SmallImage.imgNew[i].update();
					SmallImage.smallCount++;
				}
			}
			if (num > 200 && GameCanvas.lowGraphic)
			{
				SmallImage.imgNew = new Small[(int)SmallImage.maxSmall];
			}
		}
	}

	// Token: 0x040007B8 RID: 1976
	public static int[][] smallImg;

	// Token: 0x040007B9 RID: 1977
	public static SmallImage instance;

	// Token: 0x040007BA RID: 1978
	public static Image[] imgbig;

	// Token: 0x040007BB RID: 1979
	public static Small[] imgNew;

	// Token: 0x040007BC RID: 1980
	public static MyVector vKeys = new MyVector();

	// Token: 0x040007BD RID: 1981
	public static Image imgEmpty = null;

	// Token: 0x040007BE RID: 1982
	public static sbyte[] newSmallVersion;

	// Token: 0x040007BF RID: 1983
	public static int smallCount;

	// Token: 0x040007C0 RID: 1984
	public static short maxSmall;
}
