using System;

// Token: 0x02000085 RID: 133
public class Skills
{
	// Token: 0x06000452 RID: 1106 RVA: 0x00034D4A File Offset: 0x0003314A
	public static void add(Skill skill)
	{
		Skills.skills.put(skill.skillId, skill);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00034D62 File Offset: 0x00033162
	public static Skill get(short skillId)
	{
		return (Skill)Skills.skills.get(skillId);
	}

	// Token: 0x040007B3 RID: 1971
	public static MyHashTable skills = new MyHashTable();
}
