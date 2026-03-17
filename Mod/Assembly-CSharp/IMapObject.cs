using System;

// Token: 0x02000042 RID: 66
public interface IMapObject
{
	// Token: 0x060003D2 RID: 978
	int getX();

	// Token: 0x060003D3 RID: 979
	int getY();

	// Token: 0x060003D4 RID: 980
	int getW();

	// Token: 0x060003D5 RID: 981
	int getH();

	// Token: 0x060003D6 RID: 982
	void stopMoving();

	// Token: 0x060003D7 RID: 983
	bool isInvisible();
}
