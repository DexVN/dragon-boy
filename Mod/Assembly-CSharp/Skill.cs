using System;

// Token: 0x0200007F RID: 127
public class Skill
{
	// Token: 0x06000444 RID: 1092 RVA: 0x00034AD4 File Offset: 0x00032ED4
	public string strCurExp()
	{
		if (this.curExp / 10 >= 100)
		{
			return "MAX";
		}
		if (this.curExp % 10 == 0)
		{
			return (int)(this.curExp / 10) + "%";
		}
		int num = (int)(this.curExp % 10);
		return string.Concat(new object[]
		{
			(int)(this.curExp / 10),
			".",
			num % 10,
			"%"
		});
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00034B60 File Offset: 0x00032F60
	public string strTimeReplay()
	{
		if (this.coolDown % 1000 == 0)
		{
			return this.coolDown / 1000 + string.Empty;
		}
		int num = this.coolDown % 1000;
		return this.coolDown / 1000 + "." + ((num % 100 != 0) ? (num / 10) : (num / 100));
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00034BE0 File Offset: 0x00032FE0
	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis();
		long num2 = num - this.lastTimeUseThisSkill;
		if (num2 < (long)this.coolDown)
		{
			g.setColor(2721889, 0.7f);
			if (this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2)
			{
				g.setColor(876862);
			}
			int num3 = (int)(num2 * 20L / (long)this.coolDown);
			g.fillRect(x - 10, y - 10 + num3, 20, 20 - num3);
		}
		else
		{
			this.paintCanNotUseSkill = false;
		}
	}

	// Token: 0x0400077B RID: 1915
	public const sbyte ATT_STAND = 0;

	// Token: 0x0400077C RID: 1916
	public const sbyte ATT_FLY = 1;

	// Token: 0x0400077D RID: 1917
	public const sbyte SKILL_AUTO_USE = 0;

	// Token: 0x0400077E RID: 1918
	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	// Token: 0x0400077F RID: 1919
	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	// Token: 0x04000780 RID: 1920
	public const sbyte SKILL_CLICK_NPC = 3;

	// Token: 0x04000781 RID: 1921
	public const sbyte SKILL_CLICK_LIVE = 4;

	// Token: 0x04000782 RID: 1922
	public SkillTemplate template;

	// Token: 0x04000783 RID: 1923
	public short skillId;

	// Token: 0x04000784 RID: 1924
	public int point;

	// Token: 0x04000785 RID: 1925
	public long powRequire;

	// Token: 0x04000786 RID: 1926
	public int coolDown;

	// Token: 0x04000787 RID: 1927
	public long lastTimeUseThisSkill;

	// Token: 0x04000788 RID: 1928
	public int dx;

	// Token: 0x04000789 RID: 1929
	public int dy;

	// Token: 0x0400078A RID: 1930
	public int maxFight;

	// Token: 0x0400078B RID: 1931
	public int manaUse;

	// Token: 0x0400078C RID: 1932
	public SkillOption[] options;

	// Token: 0x0400078D RID: 1933
	public bool paintCanNotUseSkill;

	// Token: 0x0400078E RID: 1934
	public short damage;

	// Token: 0x0400078F RID: 1935
	public string moreInfo;

	// Token: 0x04000790 RID: 1936
	public short price;

	// Token: 0x04000791 RID: 1937
	public short curExp;
}
