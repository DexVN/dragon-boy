using System;

// Token: 0x02000075 RID: 117
public class Part
{
	// Token: 0x06000405 RID: 1029 RVA: 0x00031F84 File Offset: 0x00030384
	public Part(int type)
	{
		this.type = type;
		if (type == 0)
		{
			this.pi = new PartImage[3];
		}
		if (type == 1)
		{
			this.pi = new PartImage[17];
		}
		if (type == 2)
		{
			this.pi = new PartImage[14];
		}
		if (type == 3)
		{
			this.pi = new PartImage[2];
		}
	}

	// Token: 0x040006D4 RID: 1748
	public int type;

	// Token: 0x040006D5 RID: 1749
	public PartImage[] pi;
}
