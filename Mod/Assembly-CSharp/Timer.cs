using System;

// Token: 0x020000B7 RID: 183
public class Timer
{
	// Token: 0x06000A14 RID: 2580 RVA: 0x000A6F11 File Offset: 0x000A5111
	public static void setTimer(IActionListener actionListener, int action, long timeEllapse)
	{
		Timer.timeListener = actionListener;
		Timer.idAction = action;
		Timer.timeExecute = mSystem.currentTimeMillis() + timeEllapse;
		Timer.isON = true;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x000A6F34 File Offset: 0x000A5134
	public static void update()
	{
		long num = mSystem.currentTimeMillis();
		bool flag = Timer.isON && num > Timer.timeExecute;
		if (flag)
		{
			Timer.isON = false;
			try
			{
				bool flag2 = Timer.idAction > 0;
				if (flag2)
				{
					GameScr.gI().actionPerform(Timer.idAction, null);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x0400131C RID: 4892
	public static IActionListener timeListener;

	// Token: 0x0400131D RID: 4893
	public static int idAction;

	// Token: 0x0400131E RID: 4894
	public static long timeExecute;

	// Token: 0x0400131F RID: 4895
	public static bool isON;
}
