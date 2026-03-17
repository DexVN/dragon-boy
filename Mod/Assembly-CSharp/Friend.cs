using System;

// Token: 0x02000036 RID: 54
public class Friend
{
	// Token: 0x0600028B RID: 651 RVA: 0x0003BEE8 File Offset: 0x0003A0E8
	public Friend(string friendName, sbyte type)
	{
		this.friendName = friendName;
		this.type = type;
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0003BF00 File Offset: 0x0003A100
	public Friend(string friendName)
	{
		this.friendName = friendName;
		this.type = 2;
	}

	// Token: 0x040005C6 RID: 1478
	public string friendName;

	// Token: 0x040005C7 RID: 1479
	public sbyte type;
}
