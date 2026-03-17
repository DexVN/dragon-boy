using System;

// Token: 0x02000087 RID: 135
public class Part
{
	// Token: 0x0600079F RID: 1951 RVA: 0x0008BDA8 File Offset: 0x00089FA8
	public Part(int type)
	{
		this.type = type;
		bool flag = type == 0;
		if (flag)
		{
			this.pi = new PartImage[3];
		}
		bool flag2 = type == 1;
		if (flag2)
		{
			this.pi = new PartImage[17];
		}
		bool flag3 = type == 2;
		if (flag3)
		{
			this.pi = new PartImage[14];
		}
		bool flag4 = type == 3;
		if (flag4)
		{
			this.pi = new PartImage[2];
		}
	}

	// Token: 0x04000FB5 RID: 4021
	public int type;

	// Token: 0x04000FB6 RID: 4022
	public PartImage[] pi;
}
