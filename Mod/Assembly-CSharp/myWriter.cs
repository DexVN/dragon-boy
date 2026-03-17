using System;
using System.Text;

// Token: 0x0200007D RID: 125
public class myWriter
{
	// Token: 0x06000625 RID: 1573 RVA: 0x0006B3F6 File Offset: 0x000695F6
	public myWriter()
	{
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0006B41B File Offset: 0x0006961B
	public myWriter(int len)
	{
		this.buffer = new sbyte[len];
		this.lenght = len;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0006B454 File Offset: 0x00069654
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		sbyte[] array = this.buffer;
		int num = this.posWrite;
		this.posWrite = num + 1;
		array[num] = value;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0006B484 File Offset: 0x00069684
	public void writeSByteUncheck(sbyte value)
	{
		sbyte[] array = this.buffer;
		int num = this.posWrite;
		this.posWrite = num + 1;
		array[num] = value;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0006B4AB File Offset: 0x000696AB
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0006B4B6 File Offset: 0x000696B6
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0006B4C2 File Offset: 0x000696C2
	public void writeChar(char value)
	{
		this.writeSByte(0);
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0006B4B6 File Offset: 0x000696B6
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0006B4D8 File Offset: 0x000696D8
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0006B510 File Offset: 0x00069710
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0006B548 File Offset: 0x00069748
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0006B584 File Offset: 0x00069784
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0006B5C4 File Offset: 0x000697C4
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0006B600 File Offset: 0x00069800
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0006B63C File Offset: 0x0006983C
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0006B677 File Offset: 0x00069877
	public void writeBoolean(bool value)
	{
		this.writeSByte((sbyte)((!value) ? 0 : 1));
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0006B677 File Offset: 0x00069877
	public void writeBool(bool value)
	{
        this.writeSByte((sbyte)((!value) ? 0 : 1));
    }

	// Token: 0x06000636 RID: 1590 RVA: 0x0006B68C File Offset: 0x0006988C
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

	// Token: 0x06000637 RID: 1591 RVA: 0x0006B6D8 File Offset: 0x000698D8
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

	// Token: 0x06000638 RID: 1592 RVA: 0x0006B748 File Offset: 0x00069948
	public void write(ref sbyte[] data, int arg1, int arg2)
	{
		bool flag = data == null;
		if (!flag)
		{
			for (int i = 0; i < arg2; i++)
			{
				this.writeSByte(data[i + arg1]);
				bool flag2 = this.posWrite > this.buffer.Length;
				if (flag2)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0006B798 File Offset: 0x00069998
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0006B7A4 File Offset: 0x000699A4
	public sbyte[] getData()
	{
		bool flag = this.posWrite <= 0;
		sbyte[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			sbyte[] array = new sbyte[this.posWrite];
			for (int i = 0; i < this.posWrite; i++)
			{
				array[i] = this.buffer[i];
			}
			result = array;
		}
		return result;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0006B7FC File Offset: 0x000699FC
	public void checkLenght(int ltemp)
	{
		bool flag = this.posWrite + ltemp > this.lenght;
		if (flag)
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

	// Token: 0x0600063C RID: 1596 RVA: 0x0006B878 File Offset: 0x00069A78
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0006B878 File Offset: 0x00069A78
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x04000E03 RID: 3587
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x04000E04 RID: 3588
	private int posWrite;

	// Token: 0x04000E05 RID: 3589
	private int lenght = 2048;
}
