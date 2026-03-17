using System;
using Assets.src.e;

// Token: 0x020000A5 RID: 165
public class SmallImage
{
	// Token: 0x06000930 RID: 2352 RVA: 0x0009AFEB File Offset: 0x000991EB
	public SmallImage()
	{
		this.readImage();
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0009AFFC File Offset: 0x000991FC
	public static void loadBigRMS()
	{
		bool flag = SmallImage.imgbig == null;
		if (flag)
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

	// Token: 0x06000932 RID: 2354 RVA: 0x0009B064 File Offset: 0x00099264
	public static void freeBig()
	{
		SmallImage.imgbig = null;
		mSystem.gcc();
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0009B073 File Offset: 0x00099273
	public static void loadBigImage()
	{
		SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0009B089 File Offset: 0x00099289
	public static void init()
	{
		SmallImage.instance = null;
		SmallImage.instance = new SmallImage();
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00003136 File Offset: 0x00001336
	public void readData(byte[] data)
	{
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0009B09C File Offset: 0x0009929C
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

	// Token: 0x06000937 RID: 2359 RVA: 0x00003136 File Offset: 0x00001336
	public static void clearHastable()
	{
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0009B1B0 File Offset: 0x000993B0
	public static void createImage(int id)
	{
		Res.outz(string.Concat(new object[]
		{
			"is request =",
			id,
			" zoom=",
			mGraphics.zoomLevel
		}));
		bool flag2 = mGraphics.zoomLevel == 1;
		if (flag2)
		{
			Image image = GameCanvas.loadImage("/SmallImage/Small" + id.ToString() + ".png");
			bool flag3 = image != null;
			if (flag3)
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
			Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id.ToString() + ".png");
			bool flag4 = image2 != null;
			if (flag4)
			{
				SmallImage.imgNew[id] = new Small(image2, id);
			}
			else
			{
				bool flag = false;
				sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel.ToString() + "Small" + id.ToString());
				bool flag5 = array != null;
				if (flag5)
				{
					bool flag6 = SmallImage.newSmallVersion != null && array.Length % 127 != (int)SmallImage.newSmallVersion[id];
					if (flag6)
					{
						flag = true;
					}
					bool flag7 = !flag;
					if (flag7)
					{
						Image image3 = Image.createImage(array, 0, array.Length);
						bool flag8 = image3 != null;
						if (flag8)
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
				bool flag9 = flag;
				if (flag9)
				{
					SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
					Service.gI().requestIcon(id);
				}
			}
		}
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0009B364 File Offset: 0x00099564
	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
	{
		bool flag = SmallImage.imgbig == null;
		if (flag)
		{
			Small small = SmallImage.imgNew[id];
			bool flag2 = small == null;
			if (flag2)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small, 0, 0, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img), transform, x, y, anchor);
			}
		}
		else
		{
			bool flag3 = SmallImage.smallImg != null;
			if (flag3)
			{
				bool flag4 = id >= SmallImage.smallImg.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256;
				if (flag4)
				{
					Small small2 = SmallImage.imgNew[id];
					bool flag5 = small2 == null;
					if (flag5)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small2.paint(g, transform, x, y, anchor);
					}
				}
				else
				{
					bool flag6 = SmallImage.imgbig[SmallImage.smallImg[id][0]] != null;
					if (flag6)
					{
						g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], SmallImage.smallImg[id][1], SmallImage.smallImg[id][2], SmallImage.smallImg[id][3], SmallImage.smallImg[id][4], transform, x, y, anchor);
					}
				}
			}
			else
			{
				bool flag7 = GameCanvas.currentScreen != GameScr.gI();
				if (flag7)
				{
					Small small3 = SmallImage.imgNew[id];
					bool flag8 = small3 == null;
					if (flag8)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small3.paint(g, transform, x, y, anchor);
					}
				}
			}
		}
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0009B508 File Offset: 0x00099708
	public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
	{
		bool flag = SmallImage.imgbig == null;
		if (flag)
		{
			Small small = SmallImage.imgNew[id];
			bool flag2 = small == null;
			if (flag2)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small.img, 0, f * w, w, h, transform, x, y, anchor);
			}
		}
		else
		{
			bool flag3 = SmallImage.smallImg != null;
			if (flag3)
			{
				bool flag4 = id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256;
				if (flag4)
				{
					Small small2 = SmallImage.imgNew[id];
					bool flag5 = small2 == null;
					if (flag5)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small2.paint(g, transform, f, x, y, w, h, anchor);
					}
				}
				else
				{
					bool flag6 = SmallImage.smallImg[id][0] != 4 && SmallImage.imgbig[SmallImage.smallImg[id][0]] != null;
					if (flag6)
					{
						g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], 0, f * w, w, h, transform, x, y, anchor);
					}
					else
					{
						Small small3 = SmallImage.imgNew[id];
						bool flag7 = small3 == null;
						if (flag7)
						{
							SmallImage.createImage(id);
						}
						else
						{
							small3.paint(g, transform, f, x, y, w, h, anchor);
						}
					}
				}
			}
			else
			{
				bool flag8 = GameCanvas.currentScreen != GameScr.gI();
				if (flag8)
				{
					Small small4 = SmallImage.imgNew[id];
					bool flag9 = small4 == null;
					if (flag9)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small4.paint(g, transform, f, x, y, w, h, anchor);
					}
				}
			}
		}
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0009B6E8 File Offset: 0x000998E8
	public static void update()
	{
		int num = 0;
		bool flag = GameCanvas.gameTick % 1000 == 0;
		if (flag)
		{
			for (int i = 0; i < SmallImage.imgNew.Length; i++)
			{
				bool flag2 = SmallImage.imgNew[i] != null;
				if (flag2)
				{
					num++;
					SmallImage.imgNew[i].update();
					SmallImage.smallCount++;
				}
			}
			bool flag3 = num > 200 && GameCanvas.lowGraphic;
			if (flag3)
			{
				SmallImage.imgNew = new Small[(int)SmallImage.maxSmall];
			}
		}
	}

	// Token: 0x04001171 RID: 4465
	public static int[][] smallImg;

	// Token: 0x04001172 RID: 4466
	public static SmallImage instance;

	// Token: 0x04001173 RID: 4467
	public static Image[] imgbig;

	// Token: 0x04001174 RID: 4468
	public static Small[] imgNew;

	// Token: 0x04001175 RID: 4469
	public static MyVector vKeys = new MyVector();

	// Token: 0x04001176 RID: 4470
	public static Image imgEmpty = null;

	// Token: 0x04001177 RID: 4471
	public static sbyte[] newSmallVersion;

	// Token: 0x04001178 RID: 4472
	public static int smallCount;

	// Token: 0x04001179 RID: 4473
	public static short maxSmall;
}
