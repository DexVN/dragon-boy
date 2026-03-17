using System;

// Token: 0x0200005B RID: 91
public class Key
{
	// Token: 0x06000498 RID: 1176 RVA: 0x0005A504 File Offset: 0x00058704
	public static void mapKeyPC()
	{
		bool flag = !Main.isPC;
		if (!flag)
		{
			Key.UP = 15;
			Key.DOWN = 16;
			Key.LEFT = 17;
			Key.RIGHT = 18;
		}
	}

	// Token: 0x040009D6 RID: 2518
	public static int NUM0;

	// Token: 0x040009D7 RID: 2519
	public static int NUM1 = 1;

	// Token: 0x040009D8 RID: 2520
	public static int NUM2 = 2;

	// Token: 0x040009D9 RID: 2521
	public static int NUM3 = 3;

	// Token: 0x040009DA RID: 2522
	public static int NUM4 = 4;

	// Token: 0x040009DB RID: 2523
	public static int NUM5 = 5;

	// Token: 0x040009DC RID: 2524
	public static int NUM6 = 6;

	// Token: 0x040009DD RID: 2525
	public static int NUM7 = 7;

	// Token: 0x040009DE RID: 2526
	public static int NUM8 = 8;

	// Token: 0x040009DF RID: 2527
	public static int NUM9 = 9;

	// Token: 0x040009E0 RID: 2528
	public static int STAR = 10;

	// Token: 0x040009E1 RID: 2529
	public static int BOUND = 11;

	// Token: 0x040009E2 RID: 2530
	public static int UP = 12;

	// Token: 0x040009E3 RID: 2531
	public static int DOWN = 13;

	// Token: 0x040009E4 RID: 2532
	public static int LEFT = 14;

	// Token: 0x040009E5 RID: 2533
	public static int RIGHT = 15;

	// Token: 0x040009E6 RID: 2534
	public static int FIRE = 16;

	// Token: 0x040009E7 RID: 2535
	public static int LEFT_SOFTKEY = 17;

	// Token: 0x040009E8 RID: 2536
	public static int RIGHT_SOFTKEY = 18;

	// Token: 0x040009E9 RID: 2537
	public static int CLEAR = 19;

	// Token: 0x040009EA RID: 2538
	public static int BACK = 20;
}
