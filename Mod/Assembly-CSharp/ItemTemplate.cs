using System;

// Token: 0x02000058 RID: 88
public class ItemTemplate
{
	// Token: 0x06000485 RID: 1157 RVA: 0x00059E6C File Offset: 0x0005806C
	public ItemTemplate(short templateID, sbyte type, sbyte gender, string name, string description, sbyte level, int strRequire, short iconID, short part, bool isUpToUp)
	{
		this.id = templateID;
		this.type = type;
		this.gender = gender;
		this.name = name;
		this.name = Res.changeString(this.name);
		this.description = description;
		this.description = Res.changeString(this.description);
		this.level = level;
		this.strRequire = strRequire;
		this.iconID = iconID;
		this.part = part;
		this.isUpToUp = isUpToUp;
	}

	// Token: 0x040009BC RID: 2492
	public short id;

	// Token: 0x040009BD RID: 2493
	public sbyte type;

	// Token: 0x040009BE RID: 2494
	public sbyte gender;

	// Token: 0x040009BF RID: 2495
	public string name;

	// Token: 0x040009C0 RID: 2496
	public string[] subName;

	// Token: 0x040009C1 RID: 2497
	public string description;

	// Token: 0x040009C2 RID: 2498
	public sbyte level;

	// Token: 0x040009C3 RID: 2499
	public short iconID;

	// Token: 0x040009C4 RID: 2500
	public short part;

	// Token: 0x040009C5 RID: 2501
	public bool isUpToUp;

	// Token: 0x040009C6 RID: 2502
	public int w;

	// Token: 0x040009C7 RID: 2503
	public int h;

	// Token: 0x040009C8 RID: 2504
	public int strRequire;
}
