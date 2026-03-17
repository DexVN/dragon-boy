using System;

// Token: 0x02000021 RID: 33
public class DataOutputStream
{
	// Token: 0x060001E8 RID: 488 RVA: 0x00034939 File Offset: 0x00032B39
	public DataOutputStream()
	{
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0003494E File Offset: 0x00032B4E
	public DataOutputStream(int len)
	{
		this.w = new myWriter(len);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0003496F File Offset: 0x00032B6F
	public void writeShort(short i)
	{
		this.w.writeShort(i);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0003497F File Offset: 0x00032B7F
	public void writeInt(int i)
	{
		this.w.writeInt(i);
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0003498F File Offset: 0x00032B8F
	public void write(sbyte[] data)
	{
		this.w.writeSByte(data);
	}

	// Token: 0x060001ED RID: 493 RVA: 0x000349A0 File Offset: 0x00032BA0
	public sbyte[] toByteArray()
	{
		return this.w.getData();
	}

	// Token: 0x060001EE RID: 494 RVA: 0x000349BD File Offset: 0x00032BBD
	public void close()
	{
		this.w.Close();
	}

	// Token: 0x060001EF RID: 495 RVA: 0x000349CC File Offset: 0x00032BCC
	public void writeByte(sbyte b)
	{
		this.w.writeByte(b);
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000349DC File Offset: 0x00032BDC
	public void writeUTF(string name)
	{
		this.w.writeUTF(name);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x000349EC File Offset: 0x00032BEC
	public void writeBoolean(bool b)
	{
		this.w.writeBoolean(b);
	}

	// Token: 0x040004B1 RID: 1201
	private myWriter w = new myWriter();
}
