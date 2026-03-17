using System;

// Token: 0x020000B9 RID: 185
public class TouchScreenKeyboard
{
	// Token: 0x06000A18 RID: 2584 RVA: 0x000A6FB0 File Offset: 0x000A51B0
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType t, bool b1, bool b2, bool type, bool b3, string caption)
	{
		return null;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00003136 File Offset: 0x00001336
	public static void Clear()
	{
	}

	// Token: 0x04001329 RID: 4905
	public static bool hideInput;

	// Token: 0x0400132A RID: 4906
	public static bool visible;

	// Token: 0x0400132B RID: 4907
	public bool done;

	// Token: 0x0400132C RID: 4908
	public bool active;

	// Token: 0x0400132D RID: 4909
	public string text;
}
