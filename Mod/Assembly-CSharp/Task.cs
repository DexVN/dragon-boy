using System;

// Token: 0x02000090 RID: 144
public class Task
{
	// Token: 0x060004AE RID: 1198 RVA: 0x0003BEC8 File Offset: 0x0003A2C8
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

	// Token: 0x040007F8 RID: 2040
	public int index;

	// Token: 0x040007F9 RID: 2041
	public int max;

	// Token: 0x040007FA RID: 2042
	public short[] counts;

	// Token: 0x040007FB RID: 2043
	public short taskId;

	// Token: 0x040007FC RID: 2044
	public string[] names;

	// Token: 0x040007FD RID: 2045
	public string[] details;

	// Token: 0x040007FE RID: 2046
	public string[] subNames;

	// Token: 0x040007FF RID: 2047
	public string[] contentInfo;

	// Token: 0x04000800 RID: 2048
	public short count;
}
