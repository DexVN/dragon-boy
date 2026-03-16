using System;

// Token: 0x0200003C RID: 60
public class EffectData
{
	// Token: 0x06000276 RID: 630 RVA: 0x00012E14 File Offset: 0x00011214
	public ImageInfo getImageInfo(sbyte id)
	{
		for (int i = 0; i < this.imgInfo.Length; i++)
		{
			if (this.imgInfo[i].ID == (int)id)
			{
				return this.imgInfo[i];
			}
		}
		return null;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00012E58 File Offset: 0x00011258
	public short[] get()
	{
		return this.arrFrame;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00012E60 File Offset: 0x00011260
	public short[] get(int index)
	{
		if (index >= this.anim_data.Length)
		{
			index = 0;
		}
		if (this.anim_data[index] == null)
		{
			return new short[1];
		}
		return this.anim_data[index];
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00012E90 File Offset: 0x00011290
	public void readData(string patch)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = MyStream.readFile(patch);
		}
		catch (Exception ex)
		{
			return;
		}
		this.readData(dataInputStream.r);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00012ED0 File Offset: 0x000112D0
	public void readData2(string patch)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = MyStream.readFile(patch);
		}
		catch (Exception ex)
		{
			return;
		}
		this.readEffect(dataInputStream.r);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00012F10 File Offset: 0x00011310
	public void readEffect(myReader msg)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = msg.readByte();
			Res.outz("size IMG==========" + b);
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)msg.readByte();
				this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
				this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
				this.imgInfo[i].w = (int)msg.readUnsignedByte();
				this.imgInfo[i].h = (int)msg.readUnsignedByte();
			}
			short num5 = msg.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < this.frame.Length; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = msg.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = msg.readShort();
					this.frame[j].dy[k] = msg.readShort();
					this.frame[j].idImg[k] = msg.readByte();
					if (j == 0)
					{
						if (num > (int)this.frame[j].dx[k])
						{
							num = (int)this.frame[j].dx[k];
						}
						if (num2 > (int)this.frame[j].dy[k])
						{
							num2 = (int)this.frame[j].dy[k];
						}
						if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			this.arrFrame = new short[(int)msg.readShort()];
			for (int l = 0; l < this.arrFrame.Length; l++)
			{
				this.arrFrame[l] = msg.readShort();
			}
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
			Res.outz("1");
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00013278 File Offset: 0x00011678
	public void readData(myReader iss)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = iss.readByte();
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)iss.readByte();
				this.imgInfo[i].x0 = (int)iss.readUnsignedByte();
				this.imgInfo[i].y0 = (int)iss.readUnsignedByte();
				this.imgInfo[i].w = (int)iss.readUnsignedByte();
				this.imgInfo[i].h = (int)iss.readUnsignedByte();
			}
			short num5 = iss.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < (int)num5; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = iss.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = iss.readShort();
					this.frame[j].dy[k] = iss.readShort();
					this.frame[j].idImg[k] = iss.readByte();
					if (j == 0)
					{
						if (num > (int)this.frame[j].dx[k])
						{
							num = (int)this.frame[j].dx[k];
						}
						if (num2 > (int)this.frame[j].dy[k])
						{
							num2 = (int)this.frame[j].dy[k];
						}
						if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			short num6 = iss.readShort();
			this.arrFrame = new short[(int)num6];
			if (this.ID >= 201)
			{
				short num7 = 0;
				short[] array = new short[(int)num6];
				int num8 = 0;
				string arg = string.Empty;
				bool flag = false;
				for (int l = 0; l < (int)num6; l++)
				{
					short num9 = iss.readShort();
					arg = arg + num9 + ",";
					this.arrFrame[l] = num9;
					if (num9 + 500 >= 500)
					{
						array[num8++] = num9;
						flag = true;
					}
					else
					{
						num7 = (short)Res.abs((int)(num9 + 500));
						this.anim_data[(int)num7] = new short[num8];
						Array.Copy(array, 0, this.anim_data[(int)num7], 0, num8);
						num8 = 0;
					}
				}
				if (!flag)
				{
					this.anim_data[0] = new short[num8];
					Array.Copy(array, 0, this.anim_data[(int)num7], 0, num8);
				}
				else
				{
					for (int m = 0; m < 16; m++)
					{
						if (this.anim_data[m] == null)
						{
							this.anim_data[m] = this.anim_data[2];
						}
					}
				}
			}
			else
			{
				for (int n = 0; n < (int)num6; n++)
				{
					this.arrFrame[n] = iss.readShort();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readData cua EffectDAta" + ex.ToString());
		}
	}

	// Token: 0x0600027D RID: 637 RVA: 0x000136F4 File Offset: 0x00011AF4
	public void readData(sbyte[] data)
	{
		myReader iss = new myReader(data);
		this.readData(iss);
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00013710 File Offset: 0x00011B10
	public void readDataNewBoss(sbyte[] data, sbyte typeread)
	{
		myReader msg = new myReader(data);
		this.readMobNew(msg, typeread);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0001372C File Offset: 0x00011B2C
	public void paintFrame(mGraphics g, int f, int x, int y, int trans, int layer)
	{
		if (this.frame != null && this.frame.Length != 0)
		{
			Frame frame = this.frame[f];
			for (int i = 0; i < frame.dx.Length; i++)
			{
				ImageInfo imageInfo = this.getImageInfo(frame.idImg[i]);
				try
				{
					if (trans == -1)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i], 0);
					}
					else if (trans == 0)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), 0);
					}
					else if (trans == 1)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 2, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), StaticObj.TOP_RIGHT);
					}
					else if (trans == 2)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 7, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), StaticObj.VCENTER_HCENTER);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00013920 File Offset: 0x00011D20
	public void readMobNew(myReader msg, sbyte typeread)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = msg.readByte();
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)msg.readByte();
				if ((int)typeread == 1)
				{
					this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
					this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
				}
				else
				{
					this.imgInfo[i].x0 = (int)msg.readShort();
					this.imgInfo[i].y0 = (int)msg.readShort();
				}
				this.imgInfo[i].w = (int)msg.readUnsignedByte();
				this.imgInfo[i].h = (int)msg.readUnsignedByte();
			}
			short num5 = msg.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < this.frame.Length; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = msg.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = msg.readShort();
					this.frame[j].dy[k] = msg.readShort();
					this.frame[j].idImg[k] = msg.readByte();
					if (j == 0)
					{
						if (num > (int)this.frame[j].dx[k])
						{
							num = (int)this.frame[j].dx[k];
						}
						if (num2 > (int)this.frame[j].dy[k])
						{
							num2 = (int)this.frame[j].dy[k];
						}
						if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			this.arrFrame = new short[(int)msg.readShort()];
			for (int l = 0; l < this.arrFrame.Length; l++)
			{
				this.arrFrame[l] = msg.readShort();
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x040002DD RID: 733
	public Image img;

	// Token: 0x040002DE RID: 734
	public ImageInfo[] imgInfo;

	// Token: 0x040002DF RID: 735
	public Frame[] frame;

	// Token: 0x040002E0 RID: 736
	public short[] arrFrame;

	// Token: 0x040002E1 RID: 737
	public short[][] anim_data = new short[16][];

	// Token: 0x040002E2 RID: 738
	public int ID;

	// Token: 0x040002E3 RID: 739
	public int typeData;

	// Token: 0x040002E4 RID: 740
	public int width;

	// Token: 0x040002E5 RID: 741
	public int height;
}
