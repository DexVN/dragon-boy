using System;

// Token: 0x02000066 RID: 102
public class ItemTemplate
{
	// Token: 0x060003A6 RID: 934 RVA: 0x0001E650 File Offset: 0x0001CA50
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

	// Token: 0x0400061B RID: 1563
	public short id;

	// Token: 0x0400061C RID: 1564
	public sbyte type;

	// Token: 0x0400061D RID: 1565
	public sbyte gender;

	// Token: 0x0400061E RID: 1566
	public string name;

	// Token: 0x0400061F RID: 1567
	public string[] subName;

	// Token: 0x04000620 RID: 1568
	public string description;

	// Token: 0x04000621 RID: 1569
	public sbyte level;

	// Token: 0x04000622 RID: 1570
	public short iconID;

	// Token: 0x04000623 RID: 1571
	public short part;

	// Token: 0x04000624 RID: 1572
	public bool isUpToUp;

	// Token: 0x04000625 RID: 1573
	public int w;

	// Token: 0x04000626 RID: 1574
	public int h;

	// Token: 0x04000627 RID: 1575
	public int strRequire;
}
