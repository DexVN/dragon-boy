using System;

// Token: 0x0200005B RID: 91
public class Friend
{
	// Token: 0x06000338 RID: 824 RVA: 0x0001B379 File Offset: 0x00019779
	public Friend(string friendName, sbyte type)
	{
		this.friendName = friendName;
		this.type = type;
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0001B38F File Offset: 0x0001978F
	public Friend(string friendName)
	{
		this.friendName = friendName;
		this.type = 2;
	}

	// Token: 0x04000549 RID: 1353
	public string friendName;

	// Token: 0x0400054A RID: 1354
	public sbyte type;
}
