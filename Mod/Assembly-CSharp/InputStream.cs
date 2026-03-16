using System;

// Token: 0x0200000A RID: 10
public class InputStream : myReader
{
	// Token: 0x06000053 RID: 83 RVA: 0x000033A9 File Offset: 0x000017A9
	public InputStream()
	{
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000033B1 File Offset: 0x000017B1
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000033C0 File Offset: 0x000017C0
	public InputStream(string filename) : base(filename)
	{
	}
}
