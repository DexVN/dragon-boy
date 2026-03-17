using System;

// Token: 0x0200000D RID: 13
public class BgItemMn
{
	// Token: 0x0600006A RID: 106 RVA: 0x0000686C File Offset: 0x00004A6C
	public static Image blendImage(Image img, int layer, int idImage)
	{
		int num = TileMap.tileID - 1;
		Image image = img;
		bool flag = num == 0 && layer == 1;
		if (flag)
		{
			image = mGraphics.blend(img, 0.3f, 807956);
		}
		bool flag2 = num == 1 && layer == 1;
		if (flag2)
		{
			image = mGraphics.blend(img, 0.35f, 739339);
		}
		bool flag3 = num == 2 && layer == 1;
		if (flag3)
		{
			image = mGraphics.blend(img, 0.1f, 3977975);
		}
		bool flag4 = num == 3;
		if (flag4)
		{
			bool flag5 = layer == 1;
			if (flag5)
			{
				image = mGraphics.blend(img, 0.2f, 15265992);
			}
			bool flag6 = layer == 3;
			if (flag6)
			{
				image = mGraphics.blend(img, 0.1f, 15265992);
			}
		}
		bool flag7 = num == 4;
		if (flag7)
		{
			bool flag8 = layer == 1;
			if (flag8)
			{
				image = mGraphics.blend(img, 0.3f, 1330178);
			}
			bool flag9 = layer == 3;
			if (flag9)
			{
				image = mGraphics.blend(img, 0.1f, 1330178);
			}
		}
		bool flag10 = num == 6;
		if (flag10)
		{
			bool flag11 = layer == 1;
			if (flag11)
			{
				image = mGraphics.blend(img, 0.3f, 420382);
			}
			bool flag12 = layer == 3;
			if (flag12)
			{
				image = mGraphics.blend(img, 0.15f, 420382);
			}
		}
		bool flag13 = num == 5;
		if (flag13)
		{
			bool flag14 = layer == 1;
			if (flag14)
			{
				image = mGraphics.blend(img, 0.35f, 3270903);
			}
			bool flag15 = layer == 3;
			if (flag15)
			{
				image = mGraphics.blend(img, 0.15f, 3270903);
			}
		}
		bool flag16 = num == 8;
		if (flag16)
		{
			bool flag17 = layer == 1;
			if (flag17)
			{
				image = mGraphics.blend(img, 0.3f, 7094528);
			}
			bool flag18 = layer == 3;
			if (flag18)
			{
				image = mGraphics.blend(img, 0.15f, 7094528);
			}
		}
		bool flag19 = num == 9;
		if (flag19)
		{
			bool flag20 = layer == 1;
			if (flag20)
			{
				image = mGraphics.blend(img, 0.3f, 12113627);
			}
			bool flag21 = layer == 3;
			if (flag21)
			{
				image = mGraphics.blend(img, 0.15f, 12113627);
			}
		}
		bool flag22 = num == 10 && layer == 1;
		if (flag22)
		{
			image = mGraphics.blend(img, 0.3f, 14938312);
		}
		bool flag23 = num == 10 && layer == 1;
		if (flag23)
		{
			image = mGraphics.blend(img, 0.2f, 14938312);
		}
		bool flag24 = num == 11;
		if (flag24)
		{
			bool flag25 = layer == 1;
			if (flag25)
			{
				image = mGraphics.blend(img, 0.3f, 0);
			}
			bool flag26 = layer == 3;
			if (flag26)
			{
				image = mGraphics.blend(img, 0.15f, 0);
			}
		}
		bool flag27 = num > 11;
		if (flag27)
		{
			bool flag28 = layer == 1 || layer == 2;
			if (flag28)
			{
				image = mGraphics.blend(img, 0.3f, 0);
			}
			bool flag29 = layer == 3;
			if (flag29)
			{
				image = mGraphics.blend(img, 0.15f, 0);
			}
		}
		byte[] byteArray = BgItemMn.getByteArray(image);
		Rms.saveRMS(string.Concat(new object[]
		{
			"x",
			mGraphics.zoomLevel,
			"blend",
			idImage,
			"layer",
			layer
		}), ArrayCast.cast(byteArray));
		return image;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00006BC0 File Offset: 0x00004DC0
	public static byte[] getByteArray(Image img)
	{
		byte[] result;
		try
		{
			byte[] array = img.texture.EncodeToPNG();
			result = array;
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00006BFC File Offset: 0x00004DFC
	public static void blendcurrBg(short id, Image img)
	{
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			bool flag = bgItem.idImage == id && !bgItem.isNotBlend() && bgItem.layer != 2 && bgItem.layer != 4 && !BgItem.imgNew.containsKey(bgItem.idImage.ToString() + "blend" + bgItem.layer.ToString());
			if (flag)
			{
				sbyte[] array = Rms.loadRMS(string.Concat(new object[]
				{
					"x",
					mGraphics.zoomLevel,
					"blend",
					id,
					"layer",
					bgItem.layer
				}));
				bool flag2 = array == null;
				if (flag2)
				{
					BgItem.imgNew.put(bgItem.idImage.ToString() + "blend" + bgItem.layer.ToString(), BgItemMn.blendImage(img, (int)bgItem.layer, (int)bgItem.idImage));
				}
				else
				{
					Image v = Image.createImage(array, 0, array.Length);
					BgItem.imgNew.put(bgItem.idImage.ToString() + "blend" + bgItem.layer.ToString(), v);
				}
			}
		}
	}
}
