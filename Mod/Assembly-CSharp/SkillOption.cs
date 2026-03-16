using System;

// Token: 0x02000081 RID: 129
public class SkillOption
{
	// Token: 0x06000449 RID: 1097 RVA: 0x00034C98 File Offset: 0x00033098
	public string getOptionString()
	{
		if (this.optionString == null)
		{
			this.optionString = NinjaUtil.replace(this.optionTemplate.name, "#", string.Empty + this.param);
		}
		return this.optionString;
	}

	// Token: 0x0400079F RID: 1951
	public int param;

	// Token: 0x040007A0 RID: 1952
	public SkillOptionTemplate optionTemplate;

	// Token: 0x040007A1 RID: 1953
	public string optionString;
}
