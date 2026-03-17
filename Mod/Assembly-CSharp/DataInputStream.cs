using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class DataInputStream
{
	// Token: 0x060001D3 RID: 467 RVA: 0x00034604 File Offset: 0x00032804
	public DataInputStream(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.r = new myReader(ArrayCast.cast(textAsset.bytes));
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00034645 File Offset: 0x00032845
	public DataInputStream(sbyte[] data)
	{
		this.r = new myReader(data);
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0003465C File Offset: 0x0003285C
	public static void update()
	{
		bool flag = DataInputStream.status == 2;
		if (flag)
		{
			DataInputStream.status = 1;
			DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
			DataInputStream.status = 0;
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00034694 File Offset: 0x00032894
	public static DataInputStream getResourceAsStream(string filename)
	{
		return DataInputStream.__getResourceAsStream(filename);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000346AC File Offset: 0x000328AC
	private static DataInputStream _getResourceAsStream(string filename)
	{
		bool flag = DataInputStream.status != 0;
		if (flag)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = DataInputStream.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = DataInputStream.status != 0;
			if (flag3)
			{
				Debug.LogError("CANNOT GET INPUTSTREAM " + filename + " WHEN GETTING " + DataInputStream.filenametemp);
				return null;
			}
		}
		DataInputStream.istemp = null;
		DataInputStream.filenametemp = filename;
		DataInputStream.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			bool flag4 = DataInputStream.status == 0;
			if (flag4)
			{
				break;
			}
		}
		bool flag5 = j == 500;
		DataInputStream result;
		if (flag5)
		{
			Debug.LogError("TOO LONG FOR CREATE INPUTSTREAM " + filename);
			DataInputStream.status = 0;
			result = null;
		}
		else
		{
			result = DataInputStream.istemp;
		}
		return result;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000347A0 File Offset: 0x000329A0
	private static DataInputStream __getResourceAsStream(string filename)
	{
		DataInputStream result;
		try
		{
			result = new DataInputStream(filename);
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x000347D8 File Offset: 0x000329D8
	public short readShort()
	{
		return this.r.readShort();
	}

	// Token: 0x060001DA RID: 474 RVA: 0x000347F8 File Offset: 0x000329F8
	public int readInt()
	{
		return this.r.readInt();
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00034818 File Offset: 0x00032A18
	public int read()
	{
		return (int)this.r.readUnsignedByte();
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00034835 File Offset: 0x00032A35
	public void read(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00034845 File Offset: 0x00032A45
	public void close()
	{
		this.r.Close();
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00034845 File Offset: 0x00032A45
	public void Close()
	{
		this.r.Close();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00034854 File Offset: 0x00032A54
	public string readUTF()
	{
		return this.r.readUTF();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00034874 File Offset: 0x00032A74
	public sbyte readByte()
	{
		return this.r.readByte();
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00034894 File Offset: 0x00032A94
	public long readLong()
	{
		return this.r.readLong();
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x000348B4 File Offset: 0x00032AB4
	public bool readBoolean()
	{
		return this.r.readBoolean();
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000348D4 File Offset: 0x00032AD4
	public int readUnsignedByte()
	{
		return (int)((byte)this.r.readByte());
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x000348F4 File Offset: 0x00032AF4
	public int readUnsignedShort()
	{
		return (int)this.r.readUnsignedShort();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00034835 File Offset: 0x00032A35
	public void readFully(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00034914 File Offset: 0x00032B14
	public int available()
	{
		return this.r.available();
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00034931 File Offset: 0x00032B31
	internal void read(ref sbyte[] byteData, int p, int size)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040004AB RID: 1195
	public myReader r;

	// Token: 0x040004AC RID: 1196
	private const int INTERVAL = 5;

	// Token: 0x040004AD RID: 1197
	private const int MAXTIME = 500;

	// Token: 0x040004AE RID: 1198
	public static DataInputStream istemp;

	// Token: 0x040004AF RID: 1199
	private static int status;

	// Token: 0x040004B0 RID: 1200
	private static string filenametemp;
}
