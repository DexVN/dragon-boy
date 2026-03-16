using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class Image
{
	// Token: 0x06000032 RID: 50 RVA: 0x00002668 File Offset: 0x00000A68
	public static Image createEmptyImage()
	{
		return Image.__createEmptyImage();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0000266F File Offset: 0x00000A6F
	public static Image createImage(string filename)
	{
		return Image.__createImage(filename);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002677 File Offset: 0x00000A77
	public static Image createImage(byte[] imageData)
	{
		return Image.__createImage(imageData);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000267F File Offset: 0x00000A7F
	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return Image.__createImage(src, x, y, w, h, transform);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000268E File Offset: 0x00000A8E
	public static Image createImage(int w, int h)
	{
		return Image.__createImage(w, h);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002698 File Offset: 0x00000A98
	public static Image createImage(Image img)
	{
		Image image = Image.createImage(img.w, img.h);
		image.texture = img.texture;
		image.texture.Apply();
		return image;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000026D0 File Offset: 0x00000AD0
	public static Image createImage(sbyte[] imageData, int offset, int lenght)
	{
		if (offset + lenght > imageData.Length)
		{
			return null;
		}
		byte[] array = new byte[lenght];
		for (int i = 0; i < lenght; i++)
		{
			array[i] = Image.convertSbyteToByte(imageData[i + offset]);
		}
		return Image.createImage(array);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002716 File Offset: 0x00000B16
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000272C File Offset: 0x00000B2C
	public static byte[] convertArrSbyteToArrByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000277C File Offset: 0x00000B7C
	public static Image createRGBImage(int[] rbg, int w, int h, bool bl)
	{
		Image image = Image.createImage(w, h);
		Color[] array = new Color[rbg.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = Image.setColorFromRBG(rbg[i]);
		}
		image.texture.SetPixels(0, 0, w, h, array);
		image.texture.Apply();
		return image;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000027E0 File Offset: 0x00000BE0
	public static Color setColorFromRBG(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float b = (float)num / 256f;
		float g = (float)num2 / 256f;
		float r = (float)num3 / 256f;
		Color result = new Color(r, g, b);
		return result;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002838 File Offset: 0x00000C38
	public static void update()
	{
		if (Image.status == 2)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createEmptyImage();
			Image.status = 0;
		}
		else if (Image.status == 3)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.filenametemp);
			Image.status = 0;
		}
		else if (Image.status == 4)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.datatemp);
			Image.status = 0;
		}
		else if (Image.status == 5)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
			Image.status = 0;
		}
		else if (Image.status == 6)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
			Image.status = 0;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002930 File Offset: 0x00000D30
	private static Image _createEmptyImage()
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000029AC File Offset: 0x00000DAC
	private static Image _createImage(string filename)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.filenametemp = filename;
		Image.status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002A40 File Offset: 0x00000E40
	private static Image _createImage(byte[] imageData)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.datatemp = imageData;
		Image.status = 4;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002AC0 File Offset: 0x00000EC0
	private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.imgSrcTemp = src;
		Image.xtemp = x;
		Image.ytemp = y;
		Image.wtemp = w;
		Image.htemp = h;
		Image.transformtemp = transform;
		Image.status = 5;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002B60 File Offset: 0x00000F60
	private static Image _createImage(int w, int h)
	{
		if (Image.status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
			return null;
		}
		Image.imgTemp = null;
		Image.wtemp = w;
		Image.htemp = h;
		Image.status = 6;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Image.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
			Image.status = 0;
		}
		return Image.imgTemp;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002BE8 File Offset: 0x00000FE8
	public static byte[] loadData(string filename)
	{
		Image image = new Image();
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		sbyte[] array = ArrayCast.cast(textAsset.bytes);
		Debug.LogError("CHIEU DAI MANG BYTE IMAGE CREAT = " + array.Length);
		return textAsset.bytes;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002C70 File Offset: 0x00001070
	private static Image __createImage(string filename)
	{
		Image image = new Image();
		Texture2D x = Resources.Load(filename) as Texture2D;
		if (x == null)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		image.texture = x;
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002CDC File Offset: 0x000010DC
	private static Image __createImage(byte[] imageData)
	{
		if (imageData == null || imageData.Length == 0)
		{
			Cout.LogError("Create Image from byte array fail");
			return null;
		}
		Image image = new Image();
		try
		{
			image.texture.LoadImage(imageData);
			image.w = image.texture.width;
			image.h = image.texture.height;
			Image.setTextureQuality(image);
		}
		catch (Exception ex)
		{
			Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
		}
		return image;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002D70 File Offset: 0x00001170
	private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h);
		y = src.texture.height - y - h;
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				int num = i;
				if (transform == 2)
				{
					num = w - i;
				}
				int num2 = j;
				image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
			}
		}
		image.texture.Apply();
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002E2C File Offset: 0x0000122C
	private static Image __createEmptyImage()
	{
		return new Image();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002E34 File Offset: 0x00001234
	public static Image __createImage(int w, int h)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h, TextureFormat.RGBA32, false);
		Image.setTextureQuality(image);
		image.w = w;
		image.h = h;
		image.texture.Apply();
		return image;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002E76 File Offset: 0x00001276
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002E7E File Offset: 0x0000127E
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002E86 File Offset: 0x00001286
	public int getWidth()
	{
		return this.w / mGraphics.zoomLevel;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002E94 File Offset: 0x00001294
	public int getHeight()
	{
		return this.h / mGraphics.zoomLevel;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002EA2 File Offset: 0x000012A2
	private static void setTextureQuality(Image img)
	{
		Image.setTextureQuality(img.texture);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00002EAF File Offset: 0x000012AF
	public static void setTextureQuality(Texture2D texture)
	{
		texture.anisoLevel = 0;
		texture.filterMode = FilterMode.Point;
		texture.mipMapBias = 0f;
		texture.wrapMode = TextureWrapMode.Clamp;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002ED1 File Offset: 0x000012D1
	public Color[] getColor()
	{
		return this.texture.GetPixels();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00002EDE File Offset: 0x000012DE
	public int getRealImageWidth()
	{
		return this.w;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00002EE6 File Offset: 0x000012E6
	public int getRealImageHeight()
	{
		return this.h;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002EF0 File Offset: 0x000012F0
	public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
	{
		Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
		for (int i = 0; i < pixels.Length; i++)
		{
			data[i] = mGraphics.getIntByColor(pixels[i]);
		}
	}

	// Token: 0x04000010 RID: 16
	private const int INTERVAL = 5;

	// Token: 0x04000011 RID: 17
	private const int MAXTIME = 500;

	// Token: 0x04000012 RID: 18
	public Texture2D texture = new Texture2D(1, 1);

	// Token: 0x04000013 RID: 19
	public static Image imgTemp;

	// Token: 0x04000014 RID: 20
	public static string filenametemp;

	// Token: 0x04000015 RID: 21
	public static byte[] datatemp;

	// Token: 0x04000016 RID: 22
	public static Image imgSrcTemp;

	// Token: 0x04000017 RID: 23
	public static int xtemp;

	// Token: 0x04000018 RID: 24
	public static int ytemp;

	// Token: 0x04000019 RID: 25
	public static int wtemp;

	// Token: 0x0400001A RID: 26
	public static int htemp;

	// Token: 0x0400001B RID: 27
	public static int transformtemp;

	// Token: 0x0400001C RID: 28
	public int w;

	// Token: 0x0400001D RID: 29
	public int h;

	// Token: 0x0400001E RID: 30
	public static int status;

	// Token: 0x0400001F RID: 31
	public Color colorBlend = Color.black;
}
