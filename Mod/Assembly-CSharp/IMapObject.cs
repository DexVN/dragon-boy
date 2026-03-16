using System;

// Token: 0x020000AB RID: 171
public interface IMapObject
{
	// Token: 0x060007B4 RID: 1972
	int getX();

	// Token: 0x060007B5 RID: 1973
	int getY();

	// Token: 0x060007B6 RID: 1974
	int getW();

	// Token: 0x060007B7 RID: 1975
	int getH();

	// Token: 0x060007B8 RID: 1976
	void stopMoving();

	// Token: 0x060007B9 RID: 1977
	bool isInvisible();
}
