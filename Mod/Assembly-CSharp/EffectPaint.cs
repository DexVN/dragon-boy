using System;

// Token: 0x0200002C RID: 44
public class EffectPaint
{
	// Token: 0x06000234 RID: 564 RVA: 0x00037378 File Offset: 0x00035578
	public int getImgId()
	{
		return this.effCharPaint.arrEfInfo[this.index].idImg;
	}

	// Token: 0x0400051E RID: 1310
	public int index;

	// Token: 0x0400051F RID: 1311
	public Mob eMob;

	// Token: 0x04000520 RID: 1312
	public global::Char eChar;

	// Token: 0x04000521 RID: 1313
	public EffectCharPaint effCharPaint;

	// Token: 0x04000522 RID: 1314
	public bool isFly;
}
