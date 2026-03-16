using System;
using System.Text;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class myReader
{
	// Token: 0x060001E8 RID: 488 RVA: 0x00002F43 File Offset: 0x00001343
	public myReader()
	{
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00002F4B File Offset: 0x0000134B
	public myReader(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00002F5C File Offset: 0x0000135C
	public myReader(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.buffer = mSystem.convertToSbyte(textAsset.bytes);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00002F98 File Offset: 0x00001398
	public sbyte readSByte()
	{
		if (this.posRead < this.buffer.Length)
		{
			return this.buffer[this.posRead++];
		}
		this.posRead = this.buffer.Length;
		throw new Exception(" loi doc sbyte eof ");
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00002FE9 File Offset: 0x000013E9
	public sbyte readsbyte()
	{
		return this.readSByte();
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00002FF1 File Offset: 0x000013F1
	public sbyte readByte()
	{
		return this.readSByte();
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00002FF9 File Offset: 0x000013F9
	public void mark(int readlimit)
	{
		this.posMark = this.posRead;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00003007 File Offset: 0x00001407
	public void reset()
	{
		this.posRead = this.posMark;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00003015 File Offset: 0x00001415
	public byte readUnsignedByte()
	{
		return myReader.convertSbyteToByte(this.readSByte());
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00003024 File Offset: 0x00001424
	public short readShort()
	{
		short num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (short)(num << 8);
			num |= (short)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00003070 File Offset: 0x00001470
	public ushort readUnsignedShort()
	{
		ushort num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (ushort)(num << 8);
			num |= (ushort)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x000030BC File Offset: 0x000014BC
	public int readInt()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			num <<= 8;
			num |= (255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00003104 File Offset: 0x00001504
	public long readLong()
	{
		long num = 0L;
		for (int i = 0; i < 8; i++)
		{
			num <<= 8;
			num |= (long)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000314E File Offset: 0x0000154E
	public bool readBool()
	{
		return (int)this.readSByte() > 0;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x00003164 File Offset: 0x00001564
	public bool readBoolean()
	{
		return (int)this.readSByte() > 0;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000317C File Offset: 0x0000157C
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

	// Token: 0x060001F8 RID: 504 RVA: 0x000031C4 File Offset: 0x000015C4
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

	// Token: 0x060001F9 RID: 505 RVA: 0x0000320C File Offset: 0x0000160C
	public string readUTF()
	{
		return this.readStringUTF();
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00003214 File Offset: 0x00001614
	public int read()
	{
		if (this.posRead < this.buffer.Length)
		{
			return (int)this.readSByte();
		}
		return -1;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00003234 File Offset: 0x00001634
	public int read(ref sbyte[] data)
	{
		if (data == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return -1;
			}
			num++;
		}
		return num;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00003288 File Offset: 0x00001688
	public void readFully(ref sbyte[] data)
	{
		if (data == null || data.Length + this.posRead > this.buffer.Length)
		{
			return;
		}
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x000032D4 File Offset: 0x000016D4
	public int available()
	{
		return this.buffer.Length - this.posRead;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x000032E5 File Offset: 0x000016E5
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000032FC File Offset: 0x000016FC
	public static byte[] convertSbyteToByte(sbyte[] var)
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

	// Token: 0x06000200 RID: 512 RVA: 0x0000334B File Offset: 0x0000174B
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00003354 File Offset: 0x00001754
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00003360 File Offset: 0x00001760
	public void read(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			data[i + arg1] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return;
			}
		}
	}

	// Token: 0x040001E1 RID: 481
	public sbyte[] buffer;

	// Token: 0x040001E2 RID: 482
	private int posRead;

	// Token: 0x040001E3 RID: 483
	private int posMark;

	// Token: 0x040001E4 RID: 484
	private static string fileName;

	// Token: 0x040001E5 RID: 485
	private static int status;
}
