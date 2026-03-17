using System;

// Token: 0x02000065 RID: 101
public class Member
{
	// Token: 0x060004E0 RID: 1248 RVA: 0x0005DA64 File Offset: 0x0005BC64
	public static string getRole(int r)
	{
		bool flag = r == 0;
		string result;
		if (flag)
		{
			result = mResources.clan_leader;
		}
		else
		{
			bool flag2 = r == 1;
			if (flag2)
			{
				result = mResources.clan_coleader;
			}
			else
			{
				bool flag3 = r == 2;
				if (flag3)
				{
					result = mResources.member;
				}
				else
				{
					result = string.Empty;
				}
			}
		}
		return result;
	}

	// Token: 0x04000A9D RID: 2717
	public int ID;

	// Token: 0x04000A9E RID: 2718
	public short head;

	// Token: 0x04000A9F RID: 2719
	public short headICON = -1;

	// Token: 0x04000AA0 RID: 2720
	public short leg;

	// Token: 0x04000AA1 RID: 2721
	public short body;

	// Token: 0x04000AA2 RID: 2722
	public string name;

	// Token: 0x04000AA3 RID: 2723
	public sbyte role;

	// Token: 0x04000AA4 RID: 2724
	public string powerPoint;

	// Token: 0x04000AA5 RID: 2725
	public int donate;

	// Token: 0x04000AA6 RID: 2726
	public int receive_donate;

	// Token: 0x04000AA7 RID: 2727
	public int curClanPoint;

	// Token: 0x04000AA8 RID: 2728
	public int clanPoint;

	// Token: 0x04000AA9 RID: 2729
	public int lastRequest;

	// Token: 0x04000AAA RID: 2730
	public string joinTime;
}
