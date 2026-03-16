using System;

// Token: 0x0200005F RID: 95
public class Info : IActionListener
{
	// Token: 0x0600036B RID: 875 RVA: 0x0001C4A9 File Offset: 0x0001A8A9
	public void hide()
	{
		this.says = null;
		this.infoWaitToShow.removeAllElements();
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0001C4C0 File Offset: 0x0001A8C0
	public void paint(mGraphics g, int x, int y, int dir)
	{
		if (this.infoWaitToShow.size() != 0)
		{
			g.translate(x, y);
			if (this.says != null && this.says.Length != 0 && this.type != 1)
			{
				if (this.outSide)
				{
					this.cx -= GameScr.cmx;
					this.cy -= GameScr.cmy;
					this.cy += 35;
				}
				int num = (mGraphics.zoomLevel != 1) ? 10 : 0;
				if (this.info.charInfo == null)
				{
					PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H, 16777215, false);
				}
				else
				{
					mSystem.paintPopUp2(g, this.X - 23, this.Y - num / 2, this.W + 15, this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num);
				}
				if (this.info.charInfo == null)
				{
					g.drawRegion(Info.gocnhon, 0, 0, 9, 8, (dir != 1) ? 2 : 0, this.cx - 3 + ((dir != 1) ? 20 : -15), this.cy - this.ch - 20 + this.sayRun + 2, mGraphics.TOP | mGraphics.HCENTER);
				}
				int num2 = -1;
				int i = 0;
				while (i < this.says.Length)
				{
					mFont mFont = mFont.tahoma_7;
					string text = this.says[i];
					int num4;
					if (this.says[i].StartsWith("|"))
					{
						string[] array = Res.split(this.says[i], "|", 0);
						if (array.Length == 3)
						{
							text = array[2];
						}
						if (array.Length == 4)
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
					IL_28C:
					if (this.info.charInfo == null)
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
						if (num9 < 0)
						{
							num9 = 0;
						}
						g.setColor(43758);
						g.fillRect(num5, num6 + num8, num9, 2);
						if (this.info.timeCount == 0)
						{
							return;
						}
						this.info.charInfo.paintHead(g, this.X + 5, this.Y + this.H / 2, 0);
						if (mGraphics.zoomLevel == 1)
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y + 3, 0);
						}
						else
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y - 3, 0);
						}
						if (!GameCanvas.isTouch)
						{
							if (!TField.isQwerty)
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn # để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn Y để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
						}
						if (mGraphics.zoomLevel == 1)
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
					i++;
					continue;
					goto IL_28C;
				}
				if (this.info.charInfo != null)
				{
				}
			}
			g.translate(-x, -y);
		}
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0001CA84 File Offset: 0x0001AE84
	public void update()
	{
		if (this.infoWaitToShow.size() != 0 && this.info.timeCount == 0)
		{
			this.time++;
			if (this.time >= this.info.speed)
			{
				this.time = 0;
				this.infoWaitToShow.removeElementAt(0);
				if (this.infoWaitToShow.size() == 0)
				{
					return;
				}
				InfoItem infoItem = (InfoItem)this.infoWaitToShow.firstElement();
				this.info = infoItem;
				this.getInfo();
			}
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0001CB1C File Offset: 0x0001AF1C
	public void getInfo()
	{
		this.sayWidth = 100;
		if (GameCanvas.w == 128)
		{
			this.sayWidth = 128;
		}
		int num;
		if (this.info.charInfo != null)
		{
			this.says = new string[]
			{
				this.info.s
			};
			if (mGraphics.zoomLevel == 1)
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

	// Token: 0x0600036F RID: 879 RVA: 0x0001CC6C File Offset: 0x0001B06C
	public void addInfo(string s, int Type, global::Char cInfo, bool isChatServer)
	{
		this.type = Type;
		if (GameCanvas.w == 128)
		{
			this.limLeft = 1;
		}
		if (this.infoWaitToShow.size() > 10)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		if (this.infoWaitToShow.size() <= 0 || s.Equals(((InfoItem)this.infoWaitToShow.lastElement()).s))
		{
		}
		InfoItem infoItem = new InfoItem(s);
		if (this.type == 0)
		{
			infoItem.speed = s.Length;
		}
		if (infoItem.speed < 70)
		{
			infoItem.speed = 70;
		}
		if (this.type == 1)
		{
			infoItem.speed = 10000000;
		}
		if (this.type == 3)
		{
			infoItem.speed = 300;
			infoItem.last = mSystem.currentTimeMillis();
			infoItem.timeCount = s.Length;
			if (infoItem.timeCount < 15)
			{
				infoItem.timeCount = 15;
			}
			if (infoItem.timeCount > 100)
			{
				infoItem.timeCount = 100;
			}
			infoItem.maxTime = infoItem.timeCount;
		}
		if (cInfo != null)
		{
			infoItem.charInfo = cInfo;
			infoItem.isChatServer = isChatServer;
			GameCanvas.panel.addChatMessage(infoItem);
			if (GameCanvas.isTouch && GameCanvas.panel.isViewChatServer)
			{
				GameScr.info2.cmdChat = new Command(mResources.CHAT, this, 1000, infoItem);
			}
		}
		if ((cInfo != null && GameCanvas.panel.isViewChatServer) || cInfo == null)
		{
			this.infoWaitToShow.addElement(infoItem);
		}
		if (this.infoWaitToShow.size() == 1)
		{
			this.info = (InfoItem)this.infoWaitToShow.firstElement();
			this.getInfo();
		}
		if (GameCanvas.isTouch && cInfo != null && GameCanvas.panel.isViewChatServer && GameCanvas.w - 50 > 155 + this.W)
		{
			GameScr.info2.cmdChat.x = GameCanvas.w - this.W - 50;
			GameScr.info2.cmdChat.y = 35;
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001CEA4 File Offset: 0x0001B2A4
	public void addInfo(string s, int speed, mFont f)
	{
		if (GameCanvas.w == 128)
		{
			this.limLeft = 1;
		}
		if (this.infoWaitToShow.size() > 10)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		this.infoWaitToShow.addElement(new InfoItem(s, f, speed));
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001CEF8 File Offset: 0x0001B2F8
	public bool isEmpty()
	{
		return this.p1 == 5 && this.infoWaitToShow.size() == 0;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001CF17 File Offset: 0x0001B317
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0001CF38 File Offset: 0x0001B338
	public void onCancelChat()
	{
	}

	// Token: 0x0400055C RID: 1372
	public MyVector infoWaitToShow = new MyVector();

	// Token: 0x0400055D RID: 1373
	public InfoItem info;

	// Token: 0x0400055E RID: 1374
	public int p1 = 5;

	// Token: 0x0400055F RID: 1375
	public int p2;

	// Token: 0x04000560 RID: 1376
	public int p3;

	// Token: 0x04000561 RID: 1377
	public int x;

	// Token: 0x04000562 RID: 1378
	public int strWidth;

	// Token: 0x04000563 RID: 1379
	public int limLeft = 2;

	// Token: 0x04000564 RID: 1380
	public int hI = 20;

	// Token: 0x04000565 RID: 1381
	public int xChar;

	// Token: 0x04000566 RID: 1382
	public int yChar;

	// Token: 0x04000567 RID: 1383
	public int sayWidth = 100;

	// Token: 0x04000568 RID: 1384
	public int sayRun;

	// Token: 0x04000569 RID: 1385
	public string[] says;

	// Token: 0x0400056A RID: 1386
	public int cx;

	// Token: 0x0400056B RID: 1387
	public int cy;

	// Token: 0x0400056C RID: 1388
	public int ch;

	// Token: 0x0400056D RID: 1389
	public bool outSide;

	// Token: 0x0400056E RID: 1390
	public int f;

	// Token: 0x0400056F RID: 1391
	public int tF;

	// Token: 0x04000570 RID: 1392
	public Image img;

	// Token: 0x04000571 RID: 1393
	public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

	// Token: 0x04000572 RID: 1394
	public int time;

	// Token: 0x04000573 RID: 1395
	public int timeW;

	// Token: 0x04000574 RID: 1396
	public int type;

	// Token: 0x04000575 RID: 1397
	public int X;

	// Token: 0x04000576 RID: 1398
	public int Y;

	// Token: 0x04000577 RID: 1399
	public int W;

	// Token: 0x04000578 RID: 1400
	public int H;
}
