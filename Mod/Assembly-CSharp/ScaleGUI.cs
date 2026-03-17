using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class ScaleGUI
{
	// Token: 0x06000820 RID: 2080 RVA: 0x000907A0 File Offset: 0x0008E9A0
	public static void initScaleGUI()
	{
		Cout.println(string.Concat(new object[]
		{
			"Init Scale GUI: Screen.w=",
			Screen.width,
			" Screen.h=",
			Screen.height
		}));
		ScaleGUI.WIDTH = (float)Screen.width;
		ScaleGUI.HEIGHT = (float)Screen.height;
		ScaleGUI.scaleScreen = false;
		bool flag = Screen.width > 1200;
		if (flag)
		{
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00090818 File Offset: 0x0008EA18
	public static void BeginGUI()
	{
		bool flag = !ScaleGUI.scaleScreen;
		if (!flag)
		{
			ScaleGUI.stack.Add(GUI.matrix);
			Matrix4x4 rhs = default(Matrix4x4);
			float num = (float)Screen.width;
			float num2 = (float)Screen.height;
			float num3 = num / num2;
			Vector3 zero = Vector3.zero;
			bool flag2 = num3 < ScaleGUI.WIDTH / ScaleGUI.HEIGHT;
			float d;
			if (flag2)
			{
				d = (float)Screen.width / ScaleGUI.WIDTH;
			}
			else
			{
				d = (float)Screen.height / ScaleGUI.HEIGHT;
			}
			rhs.SetTRS(zero, Quaternion.identity, Vector3.one * d);
			GUI.matrix *= rhs;
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x000908D0 File Offset: 0x0008EAD0
	public static void EndGUI()
	{
		bool flag = !ScaleGUI.scaleScreen;
		if (!flag)
		{
			GUI.matrix = ScaleGUI.stack[ScaleGUI.stack.Count - 1];
			ScaleGUI.stack.RemoveAt(ScaleGUI.stack.Count - 1);
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00090920 File Offset: 0x0008EB20
	public static float scaleX(float x)
	{
		bool flag = !ScaleGUI.scaleScreen;
		float result;
		if (flag)
		{
			result = x;
		}
		else
		{
			x = x * ScaleGUI.WIDTH / (float)Screen.width;
			result = x;
		}
		return result;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x00090954 File Offset: 0x0008EB54
	public static float scaleY(float y)
	{
		bool flag = !ScaleGUI.scaleScreen;
		float result;
		if (flag)
		{
			result = y;
		}
		else
		{
			y = y * ScaleGUI.HEIGHT / (float)Screen.height;
			result = y;
		}
		return result;
	}

	// Token: 0x0400108F RID: 4239
	public static bool scaleScreen;

	// Token: 0x04001090 RID: 4240
	public static float WIDTH;

	// Token: 0x04001091 RID: 4241
	public static float HEIGHT;

	// Token: 0x04001092 RID: 4242
	private static List<Matrix4x4> stack = new List<Matrix4x4>();
}
