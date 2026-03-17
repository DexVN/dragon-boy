using System;

// Token: 0x020000A3 RID: 163
public class SkillTemplate
{
	// Token: 0x0600092A RID: 2346 RVA: 0x0009AF64 File Offset: 0x00099164
	public bool isBuffToPlayer()
	{
		return this.type == 2;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0009AF80 File Offset: 0x00099180
	public bool isUseAlone()
	{
		return this.type == 3;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0009AF9C File Offset: 0x0009919C
	public bool isAttackSkill()
	{
		return this.type == 1;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0009AFB8 File Offset: 0x000991B8
	public bool isSkillSpec()
	{
		return this.type == 4;
	}

	// Token: 0x04001164 RID: 4452
	public sbyte id;

	// Token: 0x04001165 RID: 4453
	public int classId;

	// Token: 0x04001166 RID: 4454
	public string name;

	// Token: 0x04001167 RID: 4455
	public int maxPoint;

	// Token: 0x04001168 RID: 4456
	public int manaUseType;

	// Token: 0x04001169 RID: 4457
	public int type;

	// Token: 0x0400116A RID: 4458
	public int iconId;

	// Token: 0x0400116B RID: 4459
	public string[] description;

	// Token: 0x0400116C RID: 4460
	public Skill[] skills;

	// Token: 0x0400116D RID: 4461
	public string damInfo;
}
