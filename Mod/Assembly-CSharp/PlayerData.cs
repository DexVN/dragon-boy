using System;

// Token: 0x0200008B RID: 139
public class PlayerData
{
	// Token: 0x060007A7 RID: 1959 RVA: 0x0008C667 File Offset: 0x0008A867
	public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
	{
		this.playerID = playerID;
		this.name = name;
		this.head = head;
		this.body = body;
		this.leg = leg;
		this.powpoint = ppoint;
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0008C6A0 File Offset: 0x0008A8A0
	public string getInfo()
	{
		return string.Concat(new object[]
		{
			this.name,
			"\n",
			mResources.power_point,
			" ",
			this.powpoint
		});
	}

	// Token: 0x04000FCC RID: 4044
	public int playerID;

	// Token: 0x04000FCD RID: 4045
	public string name;

	// Token: 0x04000FCE RID: 4046
	public short head;

	// Token: 0x04000FCF RID: 4047
	public short body;

	// Token: 0x04000FD0 RID: 4048
	public short leg;

	// Token: 0x04000FD1 RID: 4049
	public long powpoint;
}
