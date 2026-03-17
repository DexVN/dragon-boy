using System;

// Token: 0x0200004C RID: 76
public class InputStream : myReader
{
	// Token: 0x06000418 RID: 1048 RVA: 0x00058321 File Offset: 0x00056521
	public InputStream()
	{
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0005832B File Offset: 0x0005652B
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0005833C File Offset: 0x0005653C
	public InputStream(string filename) : base(filename)
	{
	}
}
