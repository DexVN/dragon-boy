using System;

// Token: 0x02000039 RID: 57
public class EffecMn
{
	// Token: 0x06000259 RID: 601 RVA: 0x00011D6E File Offset: 0x0001016E
	public static void addEff(Effect me)
	{
		EffecMn.vEff.addElement(me);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00011D7B File Offset: 0x0001017B
	public static void removeEff(int id)
	{
		if (EffecMn.getEffById(id) != null)
		{
			EffecMn.vEff.removeElement(EffecMn.getEffById(id));
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00011D98 File Offset: 0x00010198
	public static Effect getEffById(int id)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			Effect effect = (Effect)EffecMn.vEff.elementAt(i);
			if (effect.effId == id)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x00011DE0 File Offset: 0x000101E0
	public static void paintBackGroundUnderLayer(mGraphics g, int x, int y, int layer)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == -layer)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paintUnderBackground(g, x, y);
			}
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00011E3C File Offset: 0x0001023C
	public static void paintLayer1(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 1)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00011E98 File Offset: 0x00010298
	public static void paintLayer2(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 2)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00011EF4 File Offset: 0x000102F4
	public static void paintLayer3(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 3)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00011F50 File Offset: 0x00010350
	public static void paintLayer4(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			if (((Effect)EffecMn.vEff.elementAt(i)).layer == 4)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00011FAC File Offset: 0x000103AC
	public static void update()
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			((Effect)EffecMn.vEff.elementAt(i)).update();
		}
	}

	// Token: 0x04000295 RID: 661
	public static MyVector vEff = new MyVector();
}
