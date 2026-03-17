using System;

// Token: 0x0200009F RID: 159
public class SkillOption
{
	// Token: 0x06000922 RID: 2338 RVA: 0x0009AEBC File Offset: 0x000990BC
	public string getOptionString()
	{
		bool flag = this.optionString == null;
		if (flag)
		{
			this.optionString = NinjaUtil.replace(this.optionTemplate.name, "#", string.Empty + this.param.ToString());
		}
		return this.optionString;
	}

	// Token: 0x04001159 RID: 4441
	public int param;

	// Token: 0x0400115A RID: 4442
	public SkillOptionTemplate optionTemplate;

	// Token: 0x0400115B RID: 4443
	public string optionString;
}
