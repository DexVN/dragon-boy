using System;

// Token: 0x02000058 RID: 88
public class EffectPaint
{
	// Token: 0x060002FD RID: 765 RVA: 0x000178E7 File Offset: 0x00015CE7
	public int getImgId()
	{
		return this.effCharPaint.arrEfInfo[this.index].idImg;
	}

	// Token: 0x040004E8 RID: 1256
	public int index;

	// Token: 0x040004E9 RID: 1257
	public Mob eMob;

	// Token: 0x040004EA RID: 1258
	public global::Char eChar;

	// Token: 0x040004EB RID: 1259
	public EffectCharPaint effCharPaint;

	// Token: 0x040004EC RID: 1260
	public bool isFly;
}
