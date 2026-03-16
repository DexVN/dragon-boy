using System;

// Token: 0x02000052 RID: 82
public class Command
{
	// Token: 0x060002DE RID: 734 RVA: 0x00016FFC File Offset: 0x000153FC
	public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00017064 File Offset: 0x00015464
	public Command()
	{
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00017094 File Offset: 0x00015494
	public Command(string caption, IActionListener actionListener, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000170EC File Offset: 0x000154EC
	public Command(string caption, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.p = p;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0001713C File Offset: 0x0001553C
	public Command(string caption, int action)
	{
		this.caption = caption;
		this.idAction = action;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0001717C File Offset: 0x0001557C
	public Command(string caption, int action, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x000171D4 File Offset: 0x000155D4
	public void perform(string str)
	{
		if (this.actionChat != null)
		{
			this.actionChat(str);
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x000171F0 File Offset: 0x000155F0
	public void performAction()
	{
		GameCanvas.clearAllPointerEvent();
		if (this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null))
		{
			SoundMn.gI().buttonClick();
		}
		if (this.idAction > 0)
		{
			if (this.actionListener != null)
			{
				this.actionListener.perform(this.idAction, this.p);
			}
			else
			{
				GameScr.gI().actionPerform(this.idAction, this.p);
			}
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x000172A0 File Offset: 0x000156A0
	public void setType()
	{
		this.type = 1;
		this.w = 160;
		this.hw = 80;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x000172BC File Offset: 0x000156BC
	public void paint(mGraphics g)
	{
		if (this.img != null)
		{
			g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
			if (this.isFocus)
			{
				if (this.imgFocus == null)
				{
					if (this.cmdClosePanel)
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
			if (this.caption != "menu" && this.caption != null)
			{
				if (!this.isFocus)
				{
					mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
				else
				{
					mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
			}
			return;
		}
		if (this.caption != string.Empty)
		{
			if (this.type == 1)
			{
				if (!this.isFocus)
				{
					Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 160, g);
				}
				else
				{
					Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 160, g);
				}
			}
			else if (!this.isFocus)
			{
				Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 76, g);
			}
			else
			{
				Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 76, g);
			}
		}
		int num;
		if (this.type == 1)
		{
			num = this.x + this.hw;
		}
		else
		{
			num = this.x + 38;
		}
		if (!this.isFocus)
		{
			mFont.tahoma_7b_dark.drawString(g, this.caption, num, this.y + 7, 2);
		}
		else
		{
			mFont.tahoma_7b_green2.drawString(g, this.caption, num, this.y + 7, 2);
		}
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0001758C File Offset: 0x0001598C
	public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
	{
		for (int i = 10; i <= size - 20; i += 10)
		{
			g.drawImage(img1, x + i, y, 0);
		}
		int num = size % 10;
		if (num > 0)
		{
			g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img2, x + size - 10, y, 0);
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00017604 File Offset: 0x00015A04
	public bool isPointerPressInside()
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
		{
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00017664 File Offset: 0x00015A64
	public bool isPointerPressInsideCamera(int cmx, int cmy)
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x - cmx, this.y - cmy, this.w, this.h))
		{
			Res.outz("w= " + this.w);
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040004B3 RID: 1203
	public bool isDisplay;

	// Token: 0x040004B4 RID: 1204
	public ActionChat actionChat;

	// Token: 0x040004B5 RID: 1205
	public string caption;

	// Token: 0x040004B6 RID: 1206
	public string[] subCaption;

	// Token: 0x040004B7 RID: 1207
	public IActionListener actionListener;

	// Token: 0x040004B8 RID: 1208
	public int idAction;

	// Token: 0x040004B9 RID: 1209
	public bool isPlaySoundButton = true;

	// Token: 0x040004BA RID: 1210
	public Image img;

	// Token: 0x040004BB RID: 1211
	public Image imgFocus;

	// Token: 0x040004BC RID: 1212
	public int x;

	// Token: 0x040004BD RID: 1213
	public int y;

	// Token: 0x040004BE RID: 1214
	public int w = mScreen.cmdW;

	// Token: 0x040004BF RID: 1215
	public int h = mScreen.cmdH;

	// Token: 0x040004C0 RID: 1216
	public int hw;

	// Token: 0x040004C1 RID: 1217
	private int lenCaption;

	// Token: 0x040004C2 RID: 1218
	public bool isFocus;

	// Token: 0x040004C3 RID: 1219
	public object p;

	// Token: 0x040004C4 RID: 1220
	public int type;

	// Token: 0x040004C5 RID: 1221
	public string caption2 = string.Empty;

	// Token: 0x040004C6 RID: 1222
	public static Image btn0left;

	// Token: 0x040004C7 RID: 1223
	public static Image btn0mid;

	// Token: 0x040004C8 RID: 1224
	public static Image btn0right;

	// Token: 0x040004C9 RID: 1225
	public static Image btn1left;

	// Token: 0x040004CA RID: 1226
	public static Image btn1mid;

	// Token: 0x040004CB RID: 1227
	public static Image btn1right;

	// Token: 0x040004CC RID: 1228
	public bool cmdClosePanel;
}
