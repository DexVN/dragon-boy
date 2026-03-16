using System;

// Token: 0x020000BF RID: 191
public class PlayerData
{
	// Token: 0x0600097F RID: 2431 RVA: 0x00091AFB File Offset: 0x0008FEFB
	public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
	{
		this.playerID = playerID;
		this.name = name;
		this.head = head;
		this.body = body;
		this.leg = leg;
		this.powpoint = ppoint;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00091B30 File Offset: 0x0008FF30
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

	// Token: 0x0400118F RID: 4495
	public int playerID;

	// Token: 0x04001190 RID: 4496
	public string name;

	// Token: 0x04001191 RID: 4497
	public short head;

	// Token: 0x04001192 RID: 4498
	public short body;

	// Token: 0x04001193 RID: 4499
	public short leg;

	// Token: 0x04001194 RID: 4500
	public long powpoint;
}
