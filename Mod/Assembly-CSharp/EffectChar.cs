using System;

// Token: 0x02000054 RID: 84
public class EffectChar
{
	// Token: 0x060002ED RID: 749 RVA: 0x000176EB File Offset: 0x00015AEB
	public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
	{
		this.template = EffectChar.effTemplates[(int)templateId];
		this.timeStart = timeStart;
		this.timeLenght = timeLenght / 1000;
		this.param = param;
	}

	// Token: 0x040004D8 RID: 1240
	public static EffectTemplate[] effTemplates;

	// Token: 0x040004D9 RID: 1241
	public static sbyte EFF_ME;

	// Token: 0x040004DA RID: 1242
	public static sbyte EFF_FRIEND = 1;

	// Token: 0x040004DB RID: 1243
	public int timeStart;

	// Token: 0x040004DC RID: 1244
	public int timeLenght;

	// Token: 0x040004DD RID: 1245
	public short param;

	// Token: 0x040004DE RID: 1246
	public EffectTemplate template;
}
