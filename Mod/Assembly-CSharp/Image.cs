using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class Image
{
	// Token: 0x060003AF RID: 943 RVA: 0x0005501C File Offset: 0x0005321C
	public static Image createEmptyImage()
	{
		return Image.__createEmptyImage();
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00055034 File Offset: 0x00053234
	public static Image createImage(string filename)
	{
		return Image.__createImage(filename);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0005504C File Offset: 0x0005324C
	public static Image createImage(byte[] imageData)
	{
		return Image.__createImage(imageData);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00055064 File Offset: 0x00053264
	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return Image.__createImage(src, x, y, w, h, transform);
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00055084 File Offset: 0x00053284
	public static Image createImage(int w, int h)
	{
		return Image.__createImage(w, h);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x000550A0 File Offset: 0x000532A0
	public static Image createImage(Image img)
	{
		Image image = Image.createImage(img.w, img.h);
		image.texture = img.texture;
		image.texture.Apply();
		return image;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x000550E0 File Offset: 0x000532E0
	public static Image createImage(sbyte[] imageData, int offset, int lenght)
	{
		bool flag = offset + lenght > imageData.Length;
		Image result;
		if (flag)
		{
			result = null;
		}
		else
		{
			byte[] array = new byte[lenght];
			for (int i = 0; i < lenght; i++)
			{
				array[i] = Image.convertSbyteToByte(imageData[i + offset]);
			}
			result = Image.createImage(array);
		}
		return result;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00055134 File Offset: 0x00053334
	public static byte convertSbyteToByte(sbyte var)
	{
		bool flag = var > 0;
		byte result;
		if (flag)
		{
			result = (byte)var;
		}
		else
		{
			result = (byte)((int)var + 256);
		}
		return result;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0005515C File Offset: 0x0005335C
	public static byte[] convertArrSbyteToArrByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
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

	// Token: 0x060003B8 RID: 952 RVA: 0x000551B4 File Offset: 0x000533B4
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

	// Token: 0x060003B9 RID: 953 RVA: 0x0005521C File Offset: 0x0005341C
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

	// Token: 0x060003BA RID: 954 RVA: 0x00055278 File Offset: 0x00053478
	public static void update()
	{
		bool flag = Image.status == 2;
		if (flag)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createEmptyImage();
			Image.status = 0;
		}
		else
		{
			bool flag2 = Image.status == 3;
			if (flag2)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createImage(Image.filenametemp);
				Image.status = 0;
			}
			else
			{
				bool flag3 = Image.status == 4;
				if (flag3)
				{
					Image.status = 1;
					Image.imgTemp = Image.__createImage(Image.datatemp);
					Image.status = 0;
				}
				else
				{
					bool flag4 = Image.status == 5;
					if (flag4)
					{
						Image.status = 1;
						Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
						Image.status = 0;
					}
					else
					{
						bool flag5 = Image.status == 6;
						if (flag5)
						{
							Image.status = 1;
							Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
							Image.status = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0005537C File Offset: 0x0005357C
	private static Image _createEmptyImage()
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.status = 2;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0005540C File Offset: 0x0005360C
	private static Image _createImage(string filename)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.filenametemp = filename;
			Image.status = 3;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x000554B4 File Offset: 0x000536B4
	private static Image _createImage(byte[] imageData)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.datatemp = imageData;
			Image.status = 4;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00055548 File Offset: 0x00053748
	private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
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
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00055600 File Offset: 0x00053800
	private static Image _createImage(int w, int h)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.wtemp = w;
			Image.htemp = h;
			Image.status = 6;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0005569C File Offset: 0x0005389C
	public static byte[] loadData(string filename)
	{
		Image image = new Image();
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		bool flag = textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0;
		if (flag)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		sbyte[] array = ArrayCast.cast(textAsset.bytes);
		Debug.LogError("CHIEU DAI MANG BYTE IMAGE CREAT = " + array.Length.ToString());
		return textAsset.bytes;
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00055730 File Offset: 0x00053930
	private static Image __createImage(string filename)
	{
		Image image = new Image();
		Texture2D x = Resources.Load(filename) as Texture2D;
		bool flag = x == null;
		if (flag)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		image.texture = x;
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x000557A4 File Offset: 0x000539A4
	private static Image __createImage(byte[] imageData)
	{
		bool flag = imageData == null || imageData.Length == 0;
		Image result;
		if (flag)
		{
			Cout.LogError("Create Image from byte array fail");
			result = null;
		}
		else
		{
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
			result = image;
		}
		return result;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00055840 File Offset: 0x00053A40
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
				bool flag = transform == 2;
				if (flag)
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

	// Token: 0x060003C4 RID: 964 RVA: 0x00055910 File Offset: 0x00053B10
	private static Image __createEmptyImage()
	{
		return new Image();
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00055928 File Offset: 0x00053B28
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

	// Token: 0x060003C6 RID: 966 RVA: 0x00055974 File Offset: 0x00053B74
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0005598C File Offset: 0x00053B8C
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000559A4 File Offset: 0x00053BA4
	public int getWidth()
	{
		return this.w / mGraphics.zoomLevel;
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x000559C4 File Offset: 0x00053BC4
	public int getHeight()
	{
		return this.h / mGraphics.zoomLevel;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x000559E2 File Offset: 0x00053BE2
	private static void setTextureQuality(Image img)
	{
		Image.setTextureQuality(img.texture);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000559F1 File Offset: 0x00053BF1
	public static void setTextureQuality(Texture2D texture)
	{
		texture.anisoLevel = 0;
		texture.filterMode = FilterMode.Point;
		texture.mipMapBias = 0f;
		texture.wrapMode = TextureWrapMode.Clamp;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00055A18 File Offset: 0x00053C18
	public Color[] getColor()
	{
		return this.texture.GetPixels();
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00055A38 File Offset: 0x00053C38
	public int getRealImageWidth()
	{
		return this.w;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00055A50 File Offset: 0x00053C50
	public int getRealImageHeight()
	{
		return this.h;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00055A68 File Offset: 0x00053C68
	public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
	{
		Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
		for (int i = 0; i < pixels.Length; i++)
		{
			data[i] = mGraphics.getIntByColor(pixels[i]);
		}
	}

	// Token: 0x04000886 RID: 2182
	private const int INTERVAL = 5;

	// Token: 0x04000887 RID: 2183
	private const int MAXTIME = 500;

	// Token: 0x04000888 RID: 2184
	public Texture2D texture = new Texture2D(1, 1);

	// Token: 0x04000889 RID: 2185
	public static Image imgTemp;

	// Token: 0x0400088A RID: 2186
	public static string filenametemp;

	// Token: 0x0400088B RID: 2187
	public static byte[] datatemp;

	// Token: 0x0400088C RID: 2188
	public static Image imgSrcTemp;

	// Token: 0x0400088D RID: 2189
	public static int xtemp;

	// Token: 0x0400088E RID: 2190
	public static int ytemp;

	// Token: 0x0400088F RID: 2191
	public static int wtemp;

	// Token: 0x04000890 RID: 2192
	public static int htemp;

	// Token: 0x04000891 RID: 2193
	public static int transformtemp;

	// Token: 0x04000892 RID: 2194
	public int w;

	// Token: 0x04000893 RID: 2195
	public int h;

	// Token: 0x04000894 RID: 2196
	public static int status;

	// Token: 0x04000895 RID: 2197
	public Color colorBlend = Color.black;
}
