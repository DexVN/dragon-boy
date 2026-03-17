using System;
using System.Text;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class myReader
{
	// Token: 0x060005F8 RID: 1528 RVA: 0x00059D71 File Offset: 0x00057F71
	public myReader()
	{
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0006AC73 File Offset: 0x00068E73
	public myReader(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0006AC84 File Offset: 0x00068E84
	public myReader(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.buffer = mSystem.convertToSbyte(textAsset.bytes);
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0006ACC0 File Offset: 0x00068EC0
	public sbyte readSByte()
	{
		bool flag = this.posRead < this.buffer.Length;
		if (flag)
		{
			sbyte[] array = this.buffer;
			int num = this.posRead;
			this.posRead = num + 1;
			return array[num];
		}
		this.posRead = this.buffer.Length;
		throw new Exception(" loi doc sbyte eof ");
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0006AD18 File Offset: 0x00068F18
	public sbyte readsbyte()
	{
		return this.readSByte();
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0006AD30 File Offset: 0x00068F30
	public sbyte readByte()
	{
		return this.readSByte();
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0006AD48 File Offset: 0x00068F48
	public void mark(int readlimit)
	{
		this.posMark = this.posRead;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0006AD57 File Offset: 0x00068F57
	public void reset()
	{
		this.posRead = this.posMark;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0006AD68 File Offset: 0x00068F68
	public byte readUnsignedByte()
	{
		return myReader.convertSbyteToByte(this.readSByte());
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0006AD88 File Offset: 0x00068F88
	public short readShort()
	{
		short num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (short)(num << 8);
			short num2 = num;
			short num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (short)((num2 | (num3 & array[num4])));
		}
		return num;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0006ADDC File Offset: 0x00068FDC
	public ushort readUnsignedShort()
	{
		ushort num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (ushort)(num << 8);
			ushort num2 = num;
			ushort num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (ushort)((num2 | (num3 & array[num4])));
		}
		return num;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0006AE30 File Offset: 0x00069030
	public int readInt()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			num <<= 8;
			int num2 = num;
			int num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (num2 | (num3 & array[num4]));
		}
		return num;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0006AE80 File Offset: 0x00069080
	public long readLong()
	{
		long num = 0L;
		for (int i = 0; i < 8; i++)
		{
			num <<= 8;
			long num2 = num;
			long num3 = 255L;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (num2 | (num3 & array[num4]));
		}
		return num;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0006AED0 File Offset: 0x000690D0
	public bool readBool()
	{
		return this.readSByte() > 0;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0006AEEC File Offset: 0x000690EC
	public bool readBoolean()
	{
		return this.readSByte() > 0;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0006AF08 File Offset: 0x00069108
	public string readString()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0006AF5C File Offset: 0x0006915C
	public string readStringUTF()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0006AFB0 File Offset: 0x000691B0
	public string readUTF()
	{
		return this.readStringUTF();
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0006AFC8 File Offset: 0x000691C8
	public int read()
	{
		bool flag = this.posRead < this.buffer.Length;
		int result;
		if (flag)
		{
			result = (int)this.readSByte();
		}
		else
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0006AFFC File Offset: 0x000691FC
	public int read(ref sbyte[] data)
	{
		bool flag = data == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.readSByte();
				bool flag2 = this.posRead > this.buffer.Length;
				if (flag2)
				{
					return -1;
				}
				num++;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0006B060 File Offset: 0x00069260
	public void readFully(ref sbyte[] data)
	{
		bool flag = data == null || data.Length + this.posRead > this.buffer.Length;
		if (!flag)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.readSByte();
			}
		}
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0006B0B0 File Offset: 0x000692B0
	public int available()
	{
		return this.buffer.Length - this.posRead;
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0006B0D4 File Offset: 0x000692D4
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

	// Token: 0x0600060F RID: 1551 RVA: 0x0006B0FC File Offset: 0x000692FC
	public static byte[] convertSbyteToByte(sbyte[] var)
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

	// Token: 0x06000610 RID: 1552 RVA: 0x0006B152 File Offset: 0x00069352
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0006B152 File Offset: 0x00069352
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0006B15C File Offset: 0x0006935C
	public void read(ref sbyte[] data, int arg1, int arg2)
	{
		bool flag = data == null;
		if (!flag)
		{
			for (int i = 0; i < arg2; i++)
			{
				data[i + arg1] = this.readSByte();
				bool flag2 = this.posRead > this.buffer.Length;
				if (flag2)
				{
					break;
				}
			}
		}
	}

	// Token: 0x04000DFD RID: 3581
	public sbyte[] buffer;

	// Token: 0x04000DFE RID: 3582
	private int posRead;

	// Token: 0x04000DFF RID: 3583
	private int posMark;

	// Token: 0x04000E00 RID: 3584
	private static string fileName;

	// Token: 0x04000E01 RID: 3585
	private static int status;
}
