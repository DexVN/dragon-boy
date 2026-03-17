using System;

// Token: 0x02000045 RID: 69
public class Info : IActionListener
{
	// Token: 0x060003E3 RID: 995 RVA: 0x00055DF6 File Offset: 0x00053FF6
	public void hide()
	{
		this.says = null;
		this.infoWaitToShow.removeAllElements();
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00055E0C File Offset: 0x0005400C
	public void paint(mGraphics g, int x, int y, int dir)
	{
		bool flag = this.infoWaitToShow.size() != 0;
		if (flag)
		{
			g.translate(x, y);
			bool flag2 = this.says != null && this.says.Length != 0 && this.type != 1;
			if (flag2)
			{
				bool flag3 = this.outSide;
				if (flag3)
				{
					this.cx -= GameScr.cmx;
					this.cy -= GameScr.cmy;
					this.cy += 35;
				}
				int num = (mGraphics.zoomLevel != 1) ? 10 : 0;
				bool flag4 = this.info.charInfo == null;
				if (flag4)
				{
					PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H, 16777215, false);
				}
				else
				{
					mSystem.paintPopUp2(g, this.X - 23, this.Y - num / 2, this.W + 15, this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num);
				}
				bool flag5 = this.info.charInfo == null;
				if (flag5)
				{
					g.drawRegion(Info.gocnhon, 0, 0, 9, 8, (dir != 1) ? 2 : 0, this.cx - 3 + ((dir != 1) ? 20 : -15), this.cy - this.ch - 20 + this.sayRun + 2, mGraphics.TOP | mGraphics.HCENTER);
				}
				int num2 = -1;
				for (int i = 0; i < this.says.Length; i++)
				{
					mFont mFont = mFont.tahoma_7;
					string text = this.says[i];
					bool flag6 = this.says[i].StartsWith("|");
					int num4;
					if (flag6)
					{
						string[] array = Res.split(this.says[i], "|", 0);
						bool flag7 = array.Length == 3;
						if (flag7)
						{
							text = array[2];
						}
						bool flag8 = array.Length == 4;
						if (flag8)
						{
							text = array[3];
							int num3 = int.Parse(array[2]);
						}
						num4 = int.Parse(array[1]);
						num2 = num4;
					}
					else
					{
						num4 = num2;
					}
					switch (num4 + 1)
					{
					case 0:
						mFont = mFont.tahoma_7;
						break;
					case 1:
						mFont = mFont.tahoma_7b_dark;
						break;
					case 2:
						mFont = mFont.tahoma_7b_green;
						break;
					case 3:
						mFont = mFont.tahoma_7b_blue;
						break;
					case 4:
						mFont = mFont.tahoma_7_red;
						break;
					case 5:
						mFont = mFont.tahoma_7_green;
						break;
					case 6:
						mFont = mFont.tahoma_7_blue;
						break;
					case 8:
						mFont = mFont.tahoma_7b_red;
						break;
					}
					bool flag9 = this.info.charInfo == null;
					if (flag9)
					{
						mFont.drawString(g, text, this.cx, this.cy - this.ch - 15 + this.sayRun + i * 12 - this.says.Length * 12 - 9, 2);
					}
					else
					{
						int num5 = this.X - 23;
						int num6 = this.Y - num / 2;
						int num7 = (mSystem.clientType != 1) ? (this.W + 25) : (this.W + 28);
						int num8 = this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num;
						g.setColor(4465169);
						g.fillRect(num5, num6 + num8, num7, 2);
						int num9 = this.info.timeCount * num7 / this.info.maxTime;
						bool flag10 = num9 < 0;
						if (flag10)
						{
							num9 = 0;
						}
						g.setColor(43758);
						g.fillRect(num5, num6 + num8, num9, 2);
						bool flag11 = this.info.timeCount == 0;
						if (flag11)
						{
							return;
						}
						this.info.charInfo.paintHead(g, this.X + 5, this.Y + this.H / 2, 0);
						bool flag12 = mGraphics.zoomLevel == 1;
						if (flag12)
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y + 3, 0);
						}
						else
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y - 3, 0);
						}
						bool flag13 = !GameCanvas.isTouch;
						if (flag13)
						{
							bool flag14 = !TField.isQwerty;
							if (flag14)
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn # để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn Y để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
						}
						bool flag15 = mGraphics.zoomLevel == 1;
						if (flag15)
						{
							TextInfo.paint(g, text, this.X + 14, this.Y + this.H / 2 + 2, this.W - 16, this.H, mFont.tahoma_7_whiteSmall);
						}
						else
						{
							string[] array2 = mFont.tahoma_7_whiteSmall.splitFontArray(text, 120);
							for (int j = 0; j < array2.Length; j++)
							{
								mFont.tahoma_7_whiteSmall.drawString(g, array2[j], this.X + 12, this.Y + 12 + j * 12 - 3, 0);
							}
							GameCanvas.resetTrans(g);
						}
					}
				}
				bool flag16 = this.info.charInfo != null;
				if (flag16)
				{
				}
			}
			g.translate(-x, -y);
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0005640C File Offset: 0x0005460C
	public void update()
	{
		bool flag = this.infoWaitToShow.size() != 0 && this.info.timeCount == 0;
		if (flag)
		{
			this.time++;
			bool flag2 = this.time >= this.info.speed;
			if (flag2)
			{
				this.time = 0;
				this.infoWaitToShow.removeElementAt(0);
				bool flag3 = this.infoWaitToShow.size() == 0;
				if (!flag3)
				{
					InfoItem infoItem = (InfoItem)this.infoWaitToShow.firstElement();
					this.info = infoItem;
					this.getInfo();
				}
			}
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x000564B0 File Offset: 0x000546B0
	public void getInfo()
	{
		this.sayWidth = 100;
		bool flag = GameCanvas.w == 128;
		if (flag)
		{
			this.sayWidth = 128;
		}
		bool flag2 = this.info.charInfo != null;
		int num;
		if (flag2)
		{
			this.says = new string[]
			{
				this.info.s
			};
			bool flag3 = mGraphics.zoomLevel == 1;
			if (flag3)
			{
				num = this.says.Length;
			}
			else
			{
				string[] array = mFont.tahoma_7_whiteSmall.splitFontArray(this.info.s, 120);
				num = array.Length;
			}
		}
		else
		{
			this.says = mFont.tahoma_7.splitFontArray(this.info.s, this.sayWidth - 10);
			num = this.says.Length;
		}
		this.sayRun = 7;
		this.X = this.cx - this.sayWidth / 2 - 1;
		this.Y = this.cy - this.ch - 15 + this.sayRun - num * 12 - 15;
		this.W = this.sayWidth + 2 + ((this.info.charInfo == null) ? 0 : 30);
		this.H = (num + 1) * 12 + 1 + ((this.info.charInfo == null) ? 0 : 5);
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00056600 File Offset: 0x00054800
	public void addInfo(string s, int Type, global::Char cInfo, bool isChatServer)
	{
		this.type = Type;
		bool flag = GameCanvas.w == 128;
		if (flag)
		{
			this.limLeft = 1;
		}
		bool flag2 = this.infoWaitToShow.size() > 10;
		if (flag2)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		bool flag3 = this.infoWaitToShow.size() <= 0 || s.Equals(((InfoItem)this.infoWaitToShow.lastElement()).s);
		if (flag3)
		{
		}
		InfoItem infoItem = new InfoItem(s);
		bool flag4 = this.type == 0;
		if (flag4)
		{
			infoItem.speed = s.Length;
		}
		bool flag5 = infoItem.speed < 70;
		if (flag5)
		{
			infoItem.speed = 70;
		}
		bool flag6 = this.type == 1;
		if (flag6)
		{
			infoItem.speed = 10000000;
		}
		bool flag7 = this.type == 3;
		if (flag7)
		{
			infoItem.speed = 300;
			infoItem.last = mSystem.currentTimeMillis();
			infoItem.timeCount = s.Length;
			bool flag8 = infoItem.timeCount < 15;
			if (flag8)
			{
				infoItem.timeCount = 15;
			}
			bool flag9 = infoItem.timeCount > 100;
			if (flag9)
			{
				infoItem.timeCount = 100;
			}
			infoItem.maxTime = infoItem.timeCount;
		}
		bool flag10 = cInfo != null;
		if (flag10)
		{
			infoItem.charInfo = cInfo;
			infoItem.isChatServer = isChatServer;
			GameCanvas.panel.addChatMessage(infoItem);
			bool flag11 = GameCanvas.isTouch && GameCanvas.panel.isViewChatServer;
			if (flag11)
			{
				GameScr.info2.cmdChat = new Command(mResources.CHAT, this, 1000, infoItem);
			}
		}
		bool flag12 = (cInfo != null && GameCanvas.panel.isViewChatServer) || cInfo == null;
		if (flag12)
		{
			this.infoWaitToShow.addElement(infoItem);
		}
		bool flag13 = this.infoWaitToShow.size() == 1;
		if (flag13)
		{
			this.info = (InfoItem)this.infoWaitToShow.firstElement();
			this.getInfo();
		}
		bool flag14 = GameCanvas.isTouch && cInfo != null && GameCanvas.panel.isViewChatServer && GameCanvas.w - 50 > 155 + this.W;
		if (flag14)
		{
			GameScr.info2.cmdChat.x = GameCanvas.w - this.W - 50;
			GameScr.info2.cmdChat.y = 35;
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00056874 File Offset: 0x00054A74
	public void addInfo(string s, int speed, mFont f)
	{
		bool flag = GameCanvas.w == 128;
		if (flag)
		{
			this.limLeft = 1;
		}
		bool flag2 = this.infoWaitToShow.size() > 10;
		if (flag2)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		this.infoWaitToShow.addElement(new InfoItem(s, f, speed));
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x000568D4 File Offset: 0x00054AD4
	public bool isEmpty()
	{
		return this.p1 == 5 && this.infoWaitToShow.size() == 0;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00056900 File Offset: 0x00054B00
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1000;
		if (flag)
		{
			ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00003136 File Offset: 0x00001336
	public void onCancelChat()
	{
	}

	// Token: 0x0400089E RID: 2206
	public MyVector infoWaitToShow = new MyVector();

	// Token: 0x0400089F RID: 2207
	public InfoItem info;

	// Token: 0x040008A0 RID: 2208
	public int p1 = 5;

	// Token: 0x040008A1 RID: 2209
	public int p2;

	// Token: 0x040008A2 RID: 2210
	public int p3;

	// Token: 0x040008A3 RID: 2211
	public int x;

	// Token: 0x040008A4 RID: 2212
	public int strWidth;

	// Token: 0x040008A5 RID: 2213
	public int limLeft = 2;

	// Token: 0x040008A6 RID: 2214
	public int hI = 20;

	// Token: 0x040008A7 RID: 2215
	public int xChar;

	// Token: 0x040008A8 RID: 2216
	public int yChar;

	// Token: 0x040008A9 RID: 2217
	public int sayWidth = 100;

	// Token: 0x040008AA RID: 2218
	public int sayRun;

	// Token: 0x040008AB RID: 2219
	public string[] says;

	// Token: 0x040008AC RID: 2220
	public int cx;

	// Token: 0x040008AD RID: 2221
	public int cy;

	// Token: 0x040008AE RID: 2222
	public int ch;

	// Token: 0x040008AF RID: 2223
	public bool outSide;

	// Token: 0x040008B0 RID: 2224
	public int f;

	// Token: 0x040008B1 RID: 2225
	public int tF;

	// Token: 0x040008B2 RID: 2226
	public Image img;

	// Token: 0x040008B3 RID: 2227
	public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

	// Token: 0x040008B4 RID: 2228
	public int time;

	// Token: 0x040008B5 RID: 2229
	public int timeW;

	// Token: 0x040008B6 RID: 2230
	public int type;

	// Token: 0x040008B7 RID: 2231
	public int X;

	// Token: 0x040008B8 RID: 2232
	public int Y;

	// Token: 0x040008B9 RID: 2233
	public int W;

	// Token: 0x040008BA RID: 2234
	public int H;
}
