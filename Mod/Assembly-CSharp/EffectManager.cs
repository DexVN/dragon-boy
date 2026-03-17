using System;

// Token: 0x0200002B RID: 43
public class EffectManager : MyVector
{
	// Token: 0x06000229 RID: 553 RVA: 0x0003719C File Offset: 0x0003539C
	public void updateAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			bool flag = effect_End != null;
			if (flag)
			{
				effect_End.update();
				bool isRemove = effect_End.isRemove;
				if (isRemove)
				{
					base.removeElementAt(i);
				}
			}
		}
	}

	// Token: 0x0600022A RID: 554 RVA: 0x000371FA File Offset: 0x000353FA
	public static void update()
	{
		EffectManager.hiEffects.updateAll();
		EffectManager.mid_2Effects.updateAll();
		EffectManager.midEffects.updateAll();
		EffectManager.lowEffects.updateAll();
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0003722C File Offset: 0x0003542C
	public void paintAll(mGraphics g)
	{
		for (int i = 0; i < base.size(); i++)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			bool flag = effect_End != null && !effect_End.isRemove;
			if (flag)
			{
				((Effect_End)base.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00037288 File Offset: 0x00035488
	public void removeAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			bool flag = effect_End != null;
			if (flag)
			{
				effect_End.isRemove = true;
				base.removeElementAt(i);
			}
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x000372D8 File Offset: 0x000354D8
	public static void remove()
	{
		EffectManager.hiEffects.removeAll();
		EffectManager.lowEffects.removeAll();
		EffectManager.midEffects.removeAll();
		EffectManager.mid_2Effects.removeAll();
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00037307 File Offset: 0x00035507
	public static void addHiEffect(Effect_End eff)
	{
		EffectManager.hiEffects.addElement(eff);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00037316 File Offset: 0x00035516
	public static void addMidEffects(Effect_End eff)
	{
		EffectManager.midEffects.addElement(eff);
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00037325 File Offset: 0x00035525
	public static void addMid_2Effects(Effect_End eff)
	{
		EffectManager.mid_2Effects.addElement(eff);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00037334 File Offset: 0x00035534
	public static void addLowEffect(Effect_End eff)
	{
		EffectManager.lowEffects.addElement(eff);
	}

	// Token: 0x0400051A RID: 1306
	public static EffectManager lowEffects = new EffectManager();

	// Token: 0x0400051B RID: 1307
	public static EffectManager mid_2Effects = new EffectManager();

	// Token: 0x0400051C RID: 1308
	public static EffectManager midEffects = new EffectManager();

	// Token: 0x0400051D RID: 1309
	public static EffectManager hiEffects = new EffectManager();
}
