using System;

// Token: 0x02000051 RID: 81
public class ipKeyboard
{
	// Token: 0x0600044F RID: 1103 RVA: 0x000585B0 File Offset: 0x000567B0
	public static void openKeyBoard(string caption, int type, string text, Command action)
	{
		ipKeyboard.act = action;
		TouchScreenKeyboardType t = (type != 0 && type != 2) ? TouchScreenKeyboardType.NumberPad : TouchScreenKeyboardType.ASCIICapable;
		TouchScreenKeyboard.hideInput = false;
		ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x000585EC File Offset: 0x000567EC
	public static void update()
	{
		try
		{
			bool flag = ipKeyboard.tk != null;
			if (flag)
			{
				bool done = ipKeyboard.tk.done;
				if (done)
				{
					bool flag2 = ipKeyboard.act != null;
					if (flag2)
					{
						ipKeyboard.act.perform(ipKeyboard.tk.text);
					}
					ipKeyboard.tk.text = string.Empty;
					ipKeyboard.tk = null;
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x04000927 RID: 2343
	private static TouchScreenKeyboard tk;

	// Token: 0x04000928 RID: 2344
	public static int TEXT;

	// Token: 0x04000929 RID: 2345
	public static int NUMBERIC = 1;

	// Token: 0x0400092A RID: 2346
	public static int PASS = 2;

	// Token: 0x0400092B RID: 2347
	private static Command act;
}
