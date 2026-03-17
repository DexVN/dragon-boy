using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class ImgByName
{
	// Token: 0x060003DC RID: 988 RVA: 0x00055AD8 File Offset: 0x00053CD8
	public static void SetImage(string name, Image img, sbyte nFrame)
	{
		ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00055AF8 File Offset: 0x00053CF8
	public static MainImage getImagePath(string nameImg, MyHashTable hash)
	{
		MainImage mainImage = (MainImage)hash.get(string.Empty + nameImg);
		bool flag = mainImage == null;
		if (flag)
		{
			mainImage = new MainImage();
			MainImage fromRms = ImgByName.getFromRms(nameImg);
			bool flag2 = fromRms != null;
			if (flag2)
			{
				mainImage.img = fromRms.img;
				mainImage.nFrame = fromRms.nFrame;
			}
			hash.put(string.Empty + nameImg, mainImage);
		}
		mainImage.count = GameCanvas.timeNow / 1000L;
		bool flag3 = mainImage.img == null;
		if (flag3)
		{
			mainImage.timeImageNull--;
			bool flag4 = mainImage.timeImageNull <= 0;
			if (flag4)
			{
				Service.gI().getImgByName(nameImg);
				mainImage.timeImageNull = 200;
			}
		}
		return mainImage;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00055BCC File Offset: 0x00053DCC
	public static MainImage getFromRms(string nameImg)
	{
		string text = mGraphics.zoomLevel.ToString() + "ImgByName_" + nameImg;
		MainImage mainImage = null;
		sbyte[] array = Rms.loadRMS(text);
		bool flag = array == null;
		MainImage result;
		if (flag)
		{
			result = mainImage;
		}
		else
		{
			try
			{
				mainImage = new MainImage();
				mainImage.nFrame = array[0];
				mainImage.img = Image.createImage(array, 1, array.Length - 1);
				bool flag2 = mainImage.img == null;
				if (flag2)
				{
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(text + ">>>>>getFromRms: nulllllllllll 2222");
				return null;
			}
			result = mainImage;
		}
		return result;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00055C70 File Offset: 0x00053E70
	public static void saveRMS(string nameImg, sbyte nFrame, sbyte[] data)
	{
		string text = mGraphics.zoomLevel.ToString() + "ImgByName_" + nameImg;
		DataOutputStream dataOutputStream = new DataOutputStream(data.Length + 1);
		int i = 0;
		try
		{
			dataOutputStream.writeByte(nFrame);
			for (i = 0; i < data.Length; i++)
			{
				dataOutputStream.writeByte(data[i]);
			}
			Rms.saveRMS(text, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new object[]
			{
				i,
				">>Errr save rms: ",
				text,
				"  ",
				ex.ToString()
			}));
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00055D30 File Offset: 0x00053F30
	public static void checkDelHash(MyHashTable hash, int minute, bool isTrue)
	{
		MyVector myVector = new MyVector("checkDelHash");
		if (isTrue)
		{
			hash.clear();
		}
		else
		{
			IDictionaryEnumerator enumerator = hash.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MainImage mainImage = (MainImage)enumerator.Value;
				bool flag = GameCanvas.timeNow / 1000L - mainImage.count > (long)(minute * 60);
				if (flag)
				{
					string o = (string)enumerator.Key;
					myVector.addElement(o);
				}
			}
			for (int i = 0; i < myVector.size(); i++)
			{
				hash.remove((string)myVector.elementAt(i));
			}
		}
	}

	// Token: 0x0400089D RID: 2205
	public static MyHashTable hashImagePath = new MyHashTable();
}
