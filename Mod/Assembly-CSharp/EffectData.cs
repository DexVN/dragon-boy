using System;

// Token: 0x02000028 RID: 40
public class EffectData
{
	// Token: 0x06000217 RID: 535 RVA: 0x00035FD0 File Offset: 0x000341D0
	public ImageInfo getImageInfo(sbyte id)
	{
		for (int i = 0; i < this.imgInfo.Length; i++)
		{
			bool flag = this.imgInfo[i].ID == (int)id;
			if (flag)
			{
				return this.imgInfo[i];
			}
		}
		return null;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0003601C File Offset: 0x0003421C
	public short[] get()
	{
		return this.arrFrame;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00036034 File Offset: 0x00034234
	public short[] get(int index)
	{
		bool flag = index >= this.anim_data.Length;
		if (flag)
		{
			index = 0;
		}
		bool flag2 = this.anim_data[index] == null;
		short[] result;
		if (flag2)
		{
			result = new short[1];
		}
		else
		{
			result = this.anim_data[index];
		}
		return result;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00036080 File Offset: 0x00034280
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

	// Token: 0x0600021B RID: 539 RVA: 0x000360BC File Offset: 0x000342BC
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

	// Token: 0x0600021C RID: 540 RVA: 0x000360F8 File Offset: 0x000342F8
	public void readEffect(myReader msg)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = msg.readByte();
			Res.outz("size IMG==========" + b.ToString());
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
					bool flag = j == 0;
					if (flag)
					{
						bool flag2 = num > (int)this.frame[j].dx[k];
						if (flag2)
						{
							num = (int)this.frame[j].dx[k];
						}
						bool flag3 = num2 > (int)this.frame[j].dy[k];
						if (flag3)
						{
							num2 = (int)this.frame[j].dy[k];
						}
						bool flag4 = num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						if (flag4)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						bool flag5 = num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						if (flag5)
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

	// Token: 0x0600021D RID: 541 RVA: 0x0003648C File Offset: 0x0003468C
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
					bool flag2 = j == 0;
					if (flag2)
					{
						bool flag3 = num > (int)this.frame[j].dx[k];
						if (flag3)
						{
							num = (int)this.frame[j].dx[k];
						}
						bool flag4 = num2 > (int)this.frame[j].dy[k];
						if (flag4)
						{
							num2 = (int)this.frame[j].dy[k];
						}
						bool flag5 = num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						if (flag5)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						bool flag6 = num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						if (flag6)
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
			bool flag7 = this.ID >= 201;
			if (flag7)
			{
				short num7 = 0;
				short[] array = new short[(int)num6];
				int num8 = 0;
				string arg = string.Empty;
				bool flag = false;
				for (int l = 0; l < (int)num6; l++)
				{
					short num9 = iss.readShort();
					arg = arg + num9.ToString() + ",";
					this.arrFrame[l] = num9;
					bool flag8 = num9 + 500 >= 500;
					if (flag8)
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
				bool flag9 = !flag;
				if (flag9)
				{
					this.anim_data[0] = new short[num8];
					Array.Copy(array, 0, this.anim_data[(int)num7], 0, num8);
				}
				else
				{
					for (int m = 0; m < 16; m++)
					{
						bool flag10 = this.anim_data[m] == null;
						if (flag10)
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

	// Token: 0x0600021E RID: 542 RVA: 0x00036958 File Offset: 0x00034B58
	public void readData(sbyte[] data)
	{
		myReader iss = new myReader(data);
		this.readData(iss);
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00036978 File Offset: 0x00034B78
	public void readDataNewBoss(sbyte[] data, sbyte typeread)
	{
		myReader msg = new myReader(data);
		this.readMobNew(msg, typeread);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00036998 File Offset: 0x00034B98
	public void paintFrame(mGraphics g, int f, int x, int y, int trans, int layer)
	{
		bool flag = this.frame != null && this.frame.Length != 0;
		if (flag)
		{
			Frame frame = this.frame[f];
			for (int i = 0; i < frame.dx.Length; i++)
			{
				ImageInfo imageInfo = this.getImageInfo(frame.idImg[i]);
				try
				{
					bool flag2 = trans == -1;
					if (flag2)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i], 0);
					}
					else
					{
						bool flag3 = trans == 0;
						if (flag3)
						{
							g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), 0);
						}
						else
						{
							bool flag4 = trans == 1;
							if (flag4)
							{
								g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 2, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), StaticObj.TOP_RIGHT);
							}
							else
							{
								bool flag5 = trans == 2;
								if (flag5)
								{
									g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 7, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), StaticObj.VCENTER_HCENTER);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00036B94 File Offset: 0x00034D94
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
				bool flag = typeread == 1;
				if (flag)
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
					bool flag2 = j == 0;
					if (flag2)
					{
						bool flag3 = num > (int)this.frame[j].dx[k];
						if (flag3)
						{
							num = (int)this.frame[j].dx[k];
						}
						bool flag4 = num2 > (int)this.frame[j].dy[k];
						if (flag4)
						{
							num2 = (int)this.frame[j].dy[k];
						}
						bool flag5 = num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						if (flag5)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						bool flag6 = num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						if (flag6)
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

	// Token: 0x04000507 RID: 1287
	public Image img;

	// Token: 0x04000508 RID: 1288
	public ImageInfo[] imgInfo;

	// Token: 0x04000509 RID: 1289
	public Frame[] frame;

	// Token: 0x0400050A RID: 1290
	public short[] arrFrame;

	// Token: 0x0400050B RID: 1291
	public short[][] anim_data = new short[16][];

	// Token: 0x0400050C RID: 1292
	public int ID;

	// Token: 0x0400050D RID: 1293
	public int typeData;

	// Token: 0x0400050E RID: 1294
	public int width;

	// Token: 0x0400050F RID: 1295
	public int height;
}
