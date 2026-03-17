using System;

// Token: 0x02000067 RID: 103
public class Message
{
	// Token: 0x060004EF RID: 1263 RVA: 0x0005EE67 File Offset: 0x0005D067
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0005EE84 File Offset: 0x0005D084
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0005EE99 File Offset: 0x0005D099
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0005EEB5 File Offset: 0x0005D0B5
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0005EED4 File Offset: 0x0005D0D4
	public sbyte[] getData()
	{
		return this.dos.getData();
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0005EEF4 File Offset: 0x0005D0F4
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0005EF0C File Offset: 0x0005D10C
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0005EF24 File Offset: 0x0005D124
	public int readInt3Byte()
	{
		return this.dis.readInt();
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00003136 File Offset: 0x00001336
	public void cleanup()
	{
	}

	// Token: 0x04000AD1 RID: 2769
	public sbyte command;

	// Token: 0x04000AD2 RID: 2770
	private myReader dis;

	// Token: 0x04000AD3 RID: 2771
	private myWriter dos;
}
