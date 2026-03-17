using System;

// Token: 0x0200001A RID: 26
public class Command
{
	// Token: 0x0600017D RID: 381 RVA: 0x0001D068 File Offset: 0x0001B268
	public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0001D0D2 File Offset: 0x0001B2D2
	public Command()
	{
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0001D104 File Offset: 0x0001B304
	public Command(string caption, IActionListener actionListener, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0001D160 File Offset: 0x0001B360
	public Command(string caption, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.p = p;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0001D1B2 File Offset: 0x0001B3B2
	public Command(string caption, int action)
	{
		this.caption = caption;
		this.idAction = action;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0001D1F4 File Offset: 0x0001B3F4
	public Command(string caption, int action, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0001D250 File Offset: 0x0001B450
	public void perform(string str)
	{
		bool flag = this.actionChat != null;
		if (flag)
		{
			this.actionChat(str);
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0001D27C File Offset: 0x0001B47C
	public void performAction()
	{
		GameCanvas.clearAllPointerEvent();
		bool flag = this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null);
		if (flag)
		{
			SoundMn.gI().buttonClick();
		}
		bool flag2 = this.idAction > 0;
		if (flag2)
		{
			bool flag3 = this.actionListener != null;
			if (flag3)
			{
				this.actionListener.perform(this.idAction, this.p);
			}
			else
			{
				GameScr.gI().actionPerform(this.idAction, this.p);
			}
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0001D335 File Offset: 0x0001B535
	public void setType()
	{
		this.type = 1;
		this.w = 160;
		this.hw = 80;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0001D354 File Offset: 0x0001B554
	public void paint(mGraphics g)
	{
		bool flag = this.img != null;
		if (flag)
		{
			g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
			bool flag2 = this.isFocus;
			if (flag2)
			{
				bool flag3 = this.imgFocus == null;
				if (flag3)
				{
					bool flag4 = this.cmdClosePanel;
					if (flag4)
					{
						g.drawImage(ItemMap.imageFlare, this.x + 8, this.y + mGraphics.addYWhenOpenKeyBoard + 8, 3);
					}
					else
					{
						g.drawImage(ItemMap.imageFlare, this.x - ((!this.img.Equals(GameScr.imgMenu)) ? 0 : 10), this.y + mGraphics.addYWhenOpenKeyBoard, 0);
					}
				}
				else
				{
					g.drawImage(this.imgFocus, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
				}
			}
			bool flag5 = this.caption != "menu" && this.caption != null;
			if (flag5)
			{
				bool flag6 = !this.isFocus;
				if (flag6)
				{
					mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
				else
				{
					mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
			}
		}
		else
		{
			bool flag7 = this.caption != string.Empty;
			if (flag7)
			{
				bool flag8 = this.type == 1;
				if (flag8)
				{
					bool flag9 = !this.isFocus;
					if (flag9)
					{
						Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 160, g);
					}
					else
					{
						Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 160, g);
					}
				}
				else
				{
					bool flag10 = !this.isFocus;
					if (flag10)
					{
						Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 76, g);
					}
					else
					{
						Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 76, g);
					}
				}
			}
			bool flag11 = this.type == 1;
			int num;
			if (flag11)
			{
				num = this.x + this.hw;
			}
			else
			{
				num = this.x + 38;
			}
			bool flag12 = !this.isFocus;
			if (flag12)
			{
				mFont.tahoma_7b_dark.drawString(g, this.caption, num, this.y + 7, 2);
			}
			else
			{
				mFont.tahoma_7b_green2.drawString(g, this.caption, num, this.y + 7, 2);
			}
		}
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0001D668 File Offset: 0x0001B868
	public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
	{
		for (int i = 10; i <= size - 20; i += 10)
		{
			g.drawImage(img1, x + i, y, 0);
		}
		int num = size % 10;
		bool flag = num > 0;
		if (flag)
		{
			g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img2, x + size - 10, y, 0);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0001D6EC File Offset: 0x0001B8EC
	public bool isPointerPressInside()
	{
		this.isFocus = false;
		bool flag = GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h);
		if (flag)
		{
			bool isPointerDown = GameCanvas.isPointerDown;
			if (isPointerDown)
			{
				this.isFocus = true;
			}
			bool flag2 = GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0001D754 File Offset: 0x0001B954
	public bool isPointerPressInsideCamera(int cmx, int cmy)
	{
		this.isFocus = false;
		bool flag = GameCanvas.isPointerHoldIn(this.x - cmx, this.y - cmy, this.w, this.h);
		if (flag)
		{
			Res.outz("w= " + this.w.ToString());
			bool isPointerDown = GameCanvas.isPointerDown;
			if (isPointerDown)
			{
				this.isFocus = true;
			}
			bool flag2 = GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000432 RID: 1074
	public bool isDisplay;

	// Token: 0x04000433 RID: 1075
	public ActionChat actionChat;

	// Token: 0x04000434 RID: 1076
	public string caption;

	// Token: 0x04000435 RID: 1077
	public string[] subCaption;

	// Token: 0x04000436 RID: 1078
	public IActionListener actionListener;

	// Token: 0x04000437 RID: 1079
	public int idAction;

	// Token: 0x04000438 RID: 1080
	public bool isPlaySoundButton = true;

	// Token: 0x04000439 RID: 1081
	public Image img;

	// Token: 0x0400043A RID: 1082
	public Image imgFocus;

	// Token: 0x0400043B RID: 1083
	public int x;

	// Token: 0x0400043C RID: 1084
	public int y;

	// Token: 0x0400043D RID: 1085
	public int w = mScreen.cmdW;

	// Token: 0x0400043E RID: 1086
	public int h = mScreen.cmdH;

	// Token: 0x0400043F RID: 1087
	public int hw;

	// Token: 0x04000440 RID: 1088
	private int lenCaption;

	// Token: 0x04000441 RID: 1089
	public bool isFocus;

	// Token: 0x04000442 RID: 1090
	public object p;

	// Token: 0x04000443 RID: 1091
	public int type;

	// Token: 0x04000444 RID: 1092
	public string caption2 = string.Empty;

	// Token: 0x04000445 RID: 1093
	public static Image btn0left;

	// Token: 0x04000446 RID: 1094
	public static Image btn0mid;

	// Token: 0x04000447 RID: 1095
	public static Image btn0right;

	// Token: 0x04000448 RID: 1096
	public static Image btn1left;

	// Token: 0x04000449 RID: 1097
	public static Image btn1mid;

	// Token: 0x0400044A RID: 1098
	public static Image btn1right;

	// Token: 0x0400044B RID: 1099
	public bool cmdClosePanel;
}
