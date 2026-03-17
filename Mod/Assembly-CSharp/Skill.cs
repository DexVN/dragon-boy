using System;

// Token: 0x0200009D RID: 157
public class Skill
{
	// Token: 0x0600091D RID: 2333 RVA: 0x0009ACD8 File Offset: 0x00098ED8
	public string strCurExp()
	{
		bool flag = this.curExp / 10 >= 100;
		string result;
		if (flag)
		{
			result = "MAX";
		}
		else
		{
			bool flag2 = this.curExp % 10 == 0;
			if (flag2)
			{
				result = ((int)(this.curExp / 10)).ToString() + "%";
			}
			else
			{
				int num = (int)(this.curExp % 10);
				result = string.Concat(new object[]
				{
					(int)(this.curExp / 10),
					".",
					num % 10,
					"%"
				});
			}
		}
		return result;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0009AD7C File Offset: 0x00098F7C
	public string strTimeReplay()
	{
		bool flag = this.coolDown % 1000 == 0;
		string result;
		if (flag)
		{
			result = (this.coolDown / 1000).ToString() + string.Empty;
		}
		else
		{
			int num = this.coolDown % 1000;
			result = (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}
		return result;
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0009AE08 File Offset: 0x00099008
	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis();
		long num2 = num - this.lastTimeUseThisSkill;
		bool flag = num2 < (long)this.coolDown;
		if (flag)
		{
			g.setColor(2721889, 0.7f);
			bool flag2 = this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2;
			if (flag2)
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

	// Token: 0x04001135 RID: 4405
	public const sbyte ATT_STAND = 0;

	// Token: 0x04001136 RID: 4406
	public const sbyte ATT_FLY = 1;

	// Token: 0x04001137 RID: 4407
	public const sbyte SKILL_AUTO_USE = 0;

	// Token: 0x04001138 RID: 4408
	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	// Token: 0x04001139 RID: 4409
	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	// Token: 0x0400113A RID: 4410
	public const sbyte SKILL_CLICK_NPC = 3;

	// Token: 0x0400113B RID: 4411
	public const sbyte SKILL_CLICK_LIVE = 4;

	// Token: 0x0400113C RID: 4412
	public SkillTemplate template;

	// Token: 0x0400113D RID: 4413
	public short skillId;

	// Token: 0x0400113E RID: 4414
	public int point;

	// Token: 0x0400113F RID: 4415
	public long powRequire;

	// Token: 0x04001140 RID: 4416
	public int coolDown;

	// Token: 0x04001141 RID: 4417
	public long lastTimeUseThisSkill;

	// Token: 0x04001142 RID: 4418
	public int dx;

	// Token: 0x04001143 RID: 4419
	public int dy;

	// Token: 0x04001144 RID: 4420
	public int maxFight;

	// Token: 0x04001145 RID: 4421
	public int manaUse;

	// Token: 0x04001146 RID: 4422
	public SkillOption[] options;

	// Token: 0x04001147 RID: 4423
	public bool paintCanNotUseSkill;

	// Token: 0x04001148 RID: 4424
	public short damage;

	// Token: 0x04001149 RID: 4425
	public string moreInfo;

	// Token: 0x0400114A RID: 4426
	public short price;

	// Token: 0x0400114B RID: 4427
	public short curExp;
}
