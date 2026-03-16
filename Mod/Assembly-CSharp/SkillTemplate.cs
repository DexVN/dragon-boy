using System;

// Token: 0x02000084 RID: 132
public class SkillTemplate
{
	// Token: 0x0600044D RID: 1101 RVA: 0x00034CFE File Offset: 0x000330FE
	public bool isBuffToPlayer()
	{
		return this.type == 2;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00034D0F File Offset: 0x0003310F
	public bool isUseAlone()
	{
		return this.type == 3;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00034D20 File Offset: 0x00033120
	public bool isAttackSkill()
	{
		return this.type == 1;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00034D31 File Offset: 0x00033131
	public bool isSkillSpec()
	{
		return this.type == 4;
	}

	// Token: 0x040007A9 RID: 1961
	public sbyte id;

	// Token: 0x040007AA RID: 1962
	public int classId;

	// Token: 0x040007AB RID: 1963
	public string name;

	// Token: 0x040007AC RID: 1964
	public int maxPoint;

	// Token: 0x040007AD RID: 1965
	public int manaUseType;

	// Token: 0x040007AE RID: 1966
	public int type;

	// Token: 0x040007AF RID: 1967
	public int iconId;

	// Token: 0x040007B0 RID: 1968
	public string[] description;

	// Token: 0x040007B1 RID: 1969
	public Skill[] skills;

	// Token: 0x040007B2 RID: 1970
	public string damInfo;
}
