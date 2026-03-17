using System;

// Token: 0x02000056 RID: 86
public class ItemOption
{
	// Token: 0x0600047F RID: 1151 RVA: 0x00059D71 File Offset: 0x00057F71
	public ItemOption()
	{
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00059D7C File Offset: 0x00057F7C
	public ItemOption(int optionTemplateId, int param)
	{
		bool flag = optionTemplateId == 22;
		if (flag)
		{
			optionTemplateId = 6;
			param *= 1000;
		}
		bool flag2 = optionTemplateId == 23;
		if (flag2)
		{
			optionTemplateId = 7;
			param *= 1000;
		}
		this.param = param;
		this.optionTemplate = GameScr.gI().iOptionTemplates[optionTemplateId];
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00059DD8 File Offset: 0x00057FD8
	public string getOptionString()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00059E14 File Offset: 0x00058014
	public string getOptionName()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "+#", string.Empty);
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00059E40 File Offset: 0x00058040
	public string getOptiongColor()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "$", string.Empty);
	}

	// Token: 0x040009B5 RID: 2485
	public int param;

	// Token: 0x040009B6 RID: 2486
	public sbyte active;

	// Token: 0x040009B7 RID: 2487
	public sbyte activeCard;

	// Token: 0x040009B8 RID: 2488
	public ItemOptionTemplate optionTemplate;
}
