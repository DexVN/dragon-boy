using System;

// Token: 0x02000064 RID: 100
public class ItemOption
{
	// Token: 0x060003A0 RID: 928 RVA: 0x0001E585 File Offset: 0x0001C985
	public ItemOption()
	{
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001E590 File Offset: 0x0001C990
	public ItemOption(int optionTemplateId, int param)
	{
		if (optionTemplateId == 22)
		{
			optionTemplateId = 6;
			param *= 1000;
		}
		if (optionTemplateId == 23)
		{
			optionTemplateId = 7;
			param *= 1000;
		}
		this.param = param;
		this.optionTemplate = GameScr.gI().iOptionTemplates[optionTemplateId];
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0001E5E4 File Offset: 0x0001C9E4
	public string getOptionString()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0001E610 File Offset: 0x0001CA10
	public string getOptionName()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "+#", string.Empty);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0001E62C File Offset: 0x0001CA2C
	public string getOptiongColor()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "$", string.Empty);
	}

	// Token: 0x04000614 RID: 1556
	public int param;

	// Token: 0x04000615 RID: 1557
	public sbyte active;

	// Token: 0x04000616 RID: 1558
	public sbyte activeCard;

	// Token: 0x04000617 RID: 1559
	public ItemOptionTemplate optionTemplate;
}
