using System;

// Token: 0x020000CA RID: 202
public class Timer
{
	// Token: 0x06000A33 RID: 2611 RVA: 0x0009A5AD File Offset: 0x000989AD
	public static void setTimer(IActionListener actionListener, int action, long timeEllapse)
	{
		Timer.timeListener = actionListener;
		Timer.idAction = action;
		Timer.timeExecute = mSystem.currentTimeMillis() + timeEllapse;
		Timer.isON = true;
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0009A5D0 File Offset: 0x000989D0
	public static void update()
	{
		long num = mSystem.currentTimeMillis();
		if (Timer.isON && num > Timer.timeExecute)
		{
			Timer.isON = false;
			try
			{
				if (Timer.idAction > 0)
				{
					GameScr.gI().actionPerform(Timer.idAction, null);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x0400130D RID: 4877
	public static IActionListener timeListener;

	// Token: 0x0400130E RID: 4878
	public static int idAction;

	// Token: 0x0400130F RID: 4879
	public static long timeExecute;

	// Token: 0x04001310 RID: 4880
	public static bool isON;
}
