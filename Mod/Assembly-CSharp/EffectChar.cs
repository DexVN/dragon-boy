using System;

// Token: 0x02000026 RID: 38
public class EffectChar
{
	// Token: 0x06000214 RID: 532 RVA: 0x00035F94 File Offset: 0x00034194
	public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
	{
		this.template = EffectChar.effTemplates[(int)templateId];
		this.timeStart = timeStart;
		this.timeLenght = timeLenght / 1000;
		this.param = param;
	}

	// Token: 0x040004FE RID: 1278
	public static EffectTemplate[] effTemplates;

	// Token: 0x040004FF RID: 1279
	public static sbyte EFF_ME;

	// Token: 0x04000500 RID: 1280
	public static sbyte EFF_FRIEND = 1;

	// Token: 0x04000501 RID: 1281
	public int timeStart;

	// Token: 0x04000502 RID: 1282
	public int timeLenght;

	// Token: 0x04000503 RID: 1283
	public short param;

	// Token: 0x04000504 RID: 1284
	public EffectTemplate template;
}
