using System;

// Token: 0x020000B0 RID: 176
public class Task
{
	// Token: 0x060009BD RID: 2493 RVA: 0x000A297C File Offset: 0x000A0B7C
	public Task(short taskId, sbyte index, string name, string detail, string[] subNames, short[] counts, short count, string[] contentInfo)
	{
		this.taskId = taskId;
		this.index = (int)index;
		this.names = mFont.tahoma_7b_green2.splitFontArray(name, Panel.WIDTH_PANEL - 20);
		this.details = mFont.tahoma_7.splitFontArray(detail, Panel.WIDTH_PANEL - 20);
		this.subNames = subNames;
		this.counts = counts;
		this.count = count;
		this.contentInfo = contentInfo;
	}

	// Token: 0x0400123A RID: 4666
	public int index;

	// Token: 0x0400123B RID: 4667
	public int max;

	// Token: 0x0400123C RID: 4668
	public short[] counts;

	// Token: 0x0400123D RID: 4669
	public short taskId;

	// Token: 0x0400123E RID: 4670
	public string[] names;

	// Token: 0x0400123F RID: 4671
	public string[] details;

	// Token: 0x04001240 RID: 4672
	public string[] subNames;

	// Token: 0x04001241 RID: 4673
	public string[] contentInfo;

	// Token: 0x04001242 RID: 4674
	public short count;
}
