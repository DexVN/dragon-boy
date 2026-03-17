using System;

// Token: 0x02000023 RID: 35
public class EffecMn
{
	// Token: 0x060001F7 RID: 503 RVA: 0x00034D15 File Offset: 0x00032F15
	public static void addEff(Effect me)
	{
		EffecMn.vEff.addElement(me);
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00034D24 File Offset: 0x00032F24
	public static void removeEff(int id)
	{
		bool flag = EffecMn.getEffById(id) != null;
		if (flag)
		{
			EffecMn.vEff.removeElement(EffecMn.getEffById(id));
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00034D54 File Offset: 0x00032F54
	public static Effect getEffById(int id)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			Effect effect = (Effect)EffecMn.vEff.elementAt(i);
			bool flag = effect.effId == id;
			if (flag)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00034DA8 File Offset: 0x00032FA8
	public static void paintBackGroundUnderLayer(mGraphics g, int x, int y, int layer)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == -layer;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paintUnderBackground(g, x, y);
			}
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00034E0C File Offset: 0x0003300C
	public static void paintLayer1(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 1;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00034E6C File Offset: 0x0003306C
	public static void paintLayer2(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 2;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00034ECC File Offset: 0x000330CC
	public static void paintLayer3(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 3;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00034F2C File Offset: 0x0003312C
	public static void paintLayer4(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 4;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00034F8C File Offset: 0x0003318C
	public static void update()
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			((Effect)EffecMn.vEff.elementAt(i)).update();
		}
	}

	// Token: 0x040004B6 RID: 1206
	public static MyVector vEff = new MyVector();
}
