using System;
using System.Text;

// Token: 0x0200002F RID: 47
public class myWriter
{
	// Token: 0x06000204 RID: 516 RVA: 0x0000DBC7 File Offset: 0x0000BFC7
	public myWriter()
	{
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000DBEA File Offset: 0x0000BFEA
	public myWriter(int len)
	{
		this.buffer = new sbyte[len];
		this.lenght = len;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000DC20 File Offset: 0x0000C020
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000DC50 File Offset: 0x0000C050
	public void writeSByteUncheck(sbyte value)
	{
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000DC76 File Offset: 0x0000C076
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000DC7F File Offset: 0x0000C07F
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000DC89 File Offset: 0x0000C089
	public void writeChar(char value)
	{
		this.writeSByte(0);
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000DC9A File Offset: 0x0000C09A
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000DCA4 File Offset: 0x0000C0A4
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000DCD8 File Offset: 0x0000C0D8
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000DD0C File Offset: 0x0000C10C
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000DD44 File Offset: 0x0000C144
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000DD7C File Offset: 0x0000C17C
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000DDB4 File Offset: 0x0000C1B4
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000DDEC File Offset: 0x0000C1EC
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000DE21 File Offset: 0x0000C221
	public void writeBoolean(bool value)
	{
        this.writeSByte((sbyte)((!value) ? 0 : 1));
    }

	// Token: 0x06000214 RID: 532 RVA: 0x0000DE36 File Offset: 0x0000C236
	public void writeBool(bool value)
	{
		this.writeSByte((sbyte)((!value) ? 0 : 1));
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000DE4C File Offset: 0x0000C24C
	public void writeString(string value)
	{
		char[] array = value.ToCharArray();
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000DE94 File Offset: 0x0000C294
	public void writeUTF(string value)
	{
		Encoding unicode = Encoding.Unicode;
		Encoding encoding = Encoding.GetEncoding(65001);
		byte[] bytes = unicode.GetBytes(value);
		byte[] array = Encoding.Convert(unicode, encoding, bytes);
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		foreach (sbyte value2 in array)
		{
			this.writeSByteUncheck(value2);
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000DF00 File Offset: 0x0000C300
	public void write(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			this.writeSByte(data[i + arg1]);
			if (this.posWrite > this.buffer.Length)
			{
				return;
			}
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000DF47 File Offset: 0x0000C347
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000DF50 File Offset: 0x0000C350
	public sbyte[] getData()
	{
		if (this.posWrite <= 0)
		{
			return null;
		}
		sbyte[] array = new sbyte[this.posWrite];
		for (int i = 0; i < this.posWrite; i++)
		{
			array[i] = this.buffer[i];
		}
		return array;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000DF9C File Offset: 0x0000C39C
	public void checkLenght(int ltemp)
	{
		if (this.posWrite + ltemp > this.lenght)
		{
			sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
			for (int i = 0; i < this.lenght; i++)
			{
				array[i] = this.buffer[i];
			}
			this.buffer = null;
			this.buffer = array;
			this.lenght += 1024 + ltemp;
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000E014 File Offset: 0x0000C414
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000E01D File Offset: 0x0000C41D
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x040001E6 RID: 486
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x040001E7 RID: 487
	private int posWrite;

	// Token: 0x040001E8 RID: 488
	private int lenght = 2048;
}
