using System;

// Token: 0x02000005 RID: 5
public class DataOutputStream
{
	// Token: 0x06000021 RID: 33 RVA: 0x000023D6 File Offset: 0x000007D6
	public DataOutputStream()
	{
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000023E9 File Offset: 0x000007E9
	public DataOutputStream(int len)
	{
		this.w = new myWriter(len);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002408 File Offset: 0x00000808
	public void writeShort(short i)
	{
		this.w.writeShort(i);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002416 File Offset: 0x00000816
	public void writeInt(int i)
	{
		this.w.writeInt(i);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002424 File Offset: 0x00000824
	public void write(sbyte[] data)
	{
		this.w.writeSByte(data);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002432 File Offset: 0x00000832
	public sbyte[] toByteArray()
	{
		return this.w.getData();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000243F File Offset: 0x0000083F
	public void close()
	{
		this.w.Close();
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000244C File Offset: 0x0000084C
	public void writeByte(sbyte b)
	{
		this.w.writeByte(b);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000245A File Offset: 0x0000085A
	public void writeUTF(string name)
	{
		this.w.writeUTF(name);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002468 File Offset: 0x00000868
	public void writeBoolean(bool b)
	{
		this.w.writeBoolean(b);
	}

	// Token: 0x04000008 RID: 8
	private myWriter w = new myWriter();
}
