using System;

// Token: 0x02000024 RID: 36
public class Message
{
	// Token: 0x0600014D RID: 333 RVA: 0x00008635 File Offset: 0x00006A35
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00008650 File Offset: 0x00006A50
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00008663 File Offset: 0x00006A63
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000867D File Offset: 0x00006A7D
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00008698 File Offset: 0x00006A98
	public sbyte[] getData()
	{
		return this.dos.getData();
	}

	// Token: 0x06000152 RID: 338 RVA: 0x000086A5 File Offset: 0x00006AA5
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000086AD File Offset: 0x00006AAD
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000086B5 File Offset: 0x00006AB5
	public int readInt3Byte()
	{
		return this.dis.readInt();
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000086C2 File Offset: 0x00006AC2
	public void cleanup()
	{
	}

	// Token: 0x04000125 RID: 293
	public sbyte command;

	// Token: 0x04000126 RID: 294
	private myReader dis;

	// Token: 0x04000127 RID: 295
	private myWriter dos;
}
