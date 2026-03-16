using System;

// Token: 0x02000057 RID: 87
public class EffectManager : MyVector
{
	// Token: 0x060002F2 RID: 754 RVA: 0x00017740 File Offset: 0x00015B40
	public void updateAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			if (effect_End != null)
			{
				effect_End.update();
				if (effect_End.isRemove)
				{
					base.removeElementAt(i);
				}
			}
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00017791 File Offset: 0x00015B91
	public static void update()
	{
		EffectManager.hiEffects.updateAll();
		EffectManager.mid_2Effects.updateAll();
		EffectManager.midEffects.updateAll();
		EffectManager.lowEffects.updateAll();
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x000177BC File Offset: 0x00015BBC
	public void paintAll(mGraphics g)
	{
		for (int i = 0; i < base.size(); i++)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			if (effect_End != null && !effect_End.isRemove)
			{
				((Effect_End)base.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00017810 File Offset: 0x00015C10
	public void removeAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			if (effect_End != null)
			{
				effect_End.isRemove = true;
				base.removeElementAt(i);
			}
		}
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00017857 File Offset: 0x00015C57
	public static void remove()
	{
		EffectManager.hiEffects.removeAll();
		EffectManager.lowEffects.removeAll();
		EffectManager.midEffects.removeAll();
		EffectManager.mid_2Effects.removeAll();
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00017881 File Offset: 0x00015C81
	public static void addHiEffect(Effect_End eff)
	{
		EffectManager.hiEffects.addElement(eff);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0001788E File Offset: 0x00015C8E
	public static void addMidEffects(Effect_End eff)
	{
		EffectManager.midEffects.addElement(eff);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0001789B File Offset: 0x00015C9B
	public static void addMid_2Effects(Effect_End eff)
	{
		EffectManager.mid_2Effects.addElement(eff);
	}

	// Token: 0x060002FA RID: 762 RVA: 0x000178A8 File Offset: 0x00015CA8
	public static void addLowEffect(Effect_End eff)
	{
		EffectManager.lowEffects.addElement(eff);
	}

	// Token: 0x040004E4 RID: 1252
	public static EffectManager lowEffects = new EffectManager();

	// Token: 0x040004E5 RID: 1253
	public static EffectManager mid_2Effects = new EffectManager();

	// Token: 0x040004E6 RID: 1254
	public static EffectManager midEffects = new EffectManager();

	// Token: 0x040004E7 RID: 1255
	public static EffectManager hiEffects = new EffectManager();
}
