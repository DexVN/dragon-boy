using System;

// Token: 0x02000035 RID: 53
public class Member
{
	// Token: 0x0600022C RID: 556 RVA: 0x0000E486 File Offset: 0x0000C886
	public static string getRole(int r)
	{
		if (r == 0)
		{
			return mResources.clan_leader;
		}
		if (r == 1)
		{
			return mResources.clan_coleader;
		}
		if (r == 2)
		{
			return mResources.member;
		}
		return string.Empty;
	}

	// Token: 0x0400020D RID: 525
	public int ID;

	// Token: 0x0400020E RID: 526
	public short head;

	// Token: 0x0400020F RID: 527
	public short headICON = -1;

	// Token: 0x04000210 RID: 528
	public short leg;

	// Token: 0x04000211 RID: 529
	public short body;

	// Token: 0x04000212 RID: 530
	public string name;

	// Token: 0x04000213 RID: 531
	public sbyte role;

	// Token: 0x04000214 RID: 532
	public string powerPoint;

	// Token: 0x04000215 RID: 533
	public int donate;

	// Token: 0x04000216 RID: 534
	public int receive_donate;

	// Token: 0x04000217 RID: 535
	public int curClanPoint;

	// Token: 0x04000218 RID: 536
	public int clanPoint;

	// Token: 0x04000219 RID: 537
	public int lastRequest;

	// Token: 0x0400021A RID: 538
	public string joinTime;
}
