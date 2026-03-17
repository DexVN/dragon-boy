using System;

// Token: 0x020000A2 RID: 162
public class Skills
{
	// Token: 0x06000926 RID: 2342 RVA: 0x0009AF13 File Offset: 0x00099113
	public static void add(Skill skill)
	{
		Skills.skills.put(skill.skillId, skill);
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0009AF30 File Offset: 0x00099130
	public static Skill get(short skillId)
	{
		return (Skill)Skills.skills.get(skillId);
	}

	// Token: 0x04001163 RID: 4451
	public static MyHashTable skills = new MyHashTable();
}
