using System;

// Token: 0x02000025 RID: 37
public abstract class Effect2
{
	// Token: 0x06000210 RID: 528 RVA: 0x00003136 File Offset: 0x00001336
	public virtual void update()
	{
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00003136 File Offset: 0x00001336
	public virtual void paint(mGraphics g)
	{
	}

	// Token: 0x040004F8 RID: 1272
	public static MyVector vEffect3 = new MyVector();

	// Token: 0x040004F9 RID: 1273
	public static MyVector vEffect2 = new MyVector();

	// Token: 0x040004FA RID: 1274
	public static MyVector vRemoveEffect2 = new MyVector();

	// Token: 0x040004FB RID: 1275
	public static MyVector vEffect2Outside = new MyVector();

	// Token: 0x040004FC RID: 1276
	public static MyVector vAnimateEffect = new MyVector();

	// Token: 0x040004FD RID: 1277
	public static MyVector vEffectFeet = new MyVector();
}
