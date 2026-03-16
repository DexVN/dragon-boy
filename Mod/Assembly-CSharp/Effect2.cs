using System;

// Token: 0x0200003B RID: 59
public abstract class Effect2
{
	// Token: 0x06000272 RID: 626 RVA: 0x00010418 File Offset: 0x0000E818
	public virtual void update()
	{
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0001041A File Offset: 0x0000E81A
	public virtual void paint(mGraphics g)
	{
	}

	// Token: 0x040002D7 RID: 727
	public static MyVector vEffect3 = new MyVector();

	// Token: 0x040002D8 RID: 728
	public static MyVector vEffect2 = new MyVector();

	// Token: 0x040002D9 RID: 729
	public static MyVector vRemoveEffect2 = new MyVector();

	// Token: 0x040002DA RID: 730
	public static MyVector vEffect2Outside = new MyVector();

	// Token: 0x040002DB RID: 731
	public static MyVector vAnimateEffect = new MyVector();

	// Token: 0x040002DC RID: 732
	public static MyVector vEffectFeet = new MyVector();
}
