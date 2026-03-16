using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class RadarScr : mScreen
{
	// Token: 0x06000981 RID: 2433 RVA: 0x00091B6C File Offset: 0x0008FF6C
	public RadarScr()
	{
		RadarScr.TYPE_UI = true;
		Image img = mSystem.loadImage("/radar/17.png");
		Image img2 = mSystem.loadImage("/radar/3.png");
		Image img3 = mSystem.loadImage("/radar/23.png");
		RadarScr.fraImgFocus = new FrameImage(img, 28, 28);
		RadarScr.fraImgFocusNone = new FrameImage(img2, 30, 30);
		RadarScr.fraEff = new FrameImage(img3, 11, 11);
		RadarScr.imgUI = mSystem.loadImage("/radar/0.png");
		RadarScr.imgArrow_Left = mSystem.loadImage("/radar/1.png");
		RadarScr.imgArrow_Right = mSystem.loadImage("/radar/2.png");
		RadarScr.imgUIText = mSystem.loadImage("/radar/17.png");
		RadarScr.imgArrow_Down = mSystem.loadImage("/radar/4.png");
		RadarScr.imgLock = mSystem.loadImage("/radar/5.png");
		RadarScr.imgUse_0 = mSystem.loadImage("/radar/6.png");
		RadarScr.imgRank = new Image[7];
		for (int i = 0; i < 7; i++)
		{
			RadarScr.imgRank[i] = mSystem.loadImage("/radar/" + (i + 7) + ".png");
		}
		RadarScr.imgUse = mSystem.loadImage("/radar/14.png");
		RadarScr.imgBack = mSystem.loadImage("/radar/15.png");
		RadarScr.imgChange = mSystem.loadImage("/radar/16.png");
		RadarScr.imgUIText = mSystem.loadImage("/radar/18.png");
		RadarScr.imgBar_1 = mSystem.loadImage("/radar/19.png");
		RadarScr.imgPro_0 = mSystem.loadImage("/radar/20.png");
		RadarScr.imgPro_1 = mSystem.loadImage("/radar/21.png");
		RadarScr.imgBar_0 = mSystem.loadImage("/radar/22.png");
		RadarScr.wUi = 200;
		RadarScr.hUi = 219;
		RadarScr.xUi = GameCanvas.hw - (RadarScr.wUi + 40) / 2;
		RadarScr.yUi = GameCanvas.hh - RadarScr.hUi / 2;
		RadarScr.xText = RadarScr.xUi + RadarScr.wUi - 81;
		RadarScr.yText = RadarScr.yUi + 29;
		RadarScr.wText = 120;
		RadarScr.hText = 80;
		RadarScr.xyArrow = new int[][]
		{
			new int[]
			{
				RadarScr.xUi + 34,
				RadarScr.yUi + RadarScr.hUi - 42
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgArrow_Down.getWidth() / 2,
				RadarScr.yUi + RadarScr.hUi / 2 + 33
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 41,
				RadarScr.yUi + RadarScr.hUi - 42
			}
		};
		RadarScr.xyItem = new int[][]
		{
			new int[]
			{
				RadarScr.xUi + 25,
				RadarScr.yUi + RadarScr.hUi - 82
			},
			new int[]
			{
				RadarScr.xUi + 57,
				RadarScr.yUi + RadarScr.hUi - 62
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - 14,
				RadarScr.yUi + RadarScr.hUi - 102
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 57 - 28,
				RadarScr.yUi + RadarScr.hUi - 62
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 25 - 28,
				RadarScr.yUi + RadarScr.hUi - 82
			}
		};
		this.dxArrow = new int[2];
		this.dyArrow = 0;
		RadarScr.xMon = RadarScr.xUi + 73;
		RadarScr.yMon = RadarScr.yUi + RadarScr.hUi / 2 + 5;
		RadarScr.yCmd = RadarScr.yUi + RadarScr.hUi - 22;
		RadarScr.xCmd = new int[]
		{
			RadarScr.xUi + RadarScr.wUi / 2 - 8 - 80,
			RadarScr.xUi + RadarScr.wUi / 2 - 8,
			RadarScr.xUi + RadarScr.wUi / 2 - 8 + 80
		};
		RadarScr.dxCmd = new int[3];
		this.yClip = RadarScr.yText + 10 + 70;
		this.hClip = 0;
		RadarScr.list = new MyVector();
		RadarScr.listUse = new MyVector();
		this.page = 1;
		this.maxpage = 2;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00091FA2 File Offset: 0x000903A2
	public static RadarScr gI()
	{
		if (RadarScr.instance == null)
		{
			RadarScr.instance = new RadarScr();
		}
		return RadarScr.instance;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00091FC0 File Offset: 0x000903C0
	public void SetRadarScr(MyVector list, int num, int numMax)
	{
		RadarScr.list = list;
		RadarScr.SetNum(num, numMax);
		this.page = 1;
		this.indexFocus = 2;
		this.listIndex();
		RadarScr.TYPE_UI = true;
		RadarScr.SetListUse();
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = list.size() / 5 + ((list.size() % 5 <= 0) ? 0 : 1);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 <= 0) ? 0 : 1);
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00092058 File Offset: 0x00090458
	public static void SetNum(int num, int numMax)
	{
		RadarScr.num = num;
		RadarScr.numMax = numMax;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00092068 File Offset: 0x00090468
	public static void SetListUse()
	{
		RadarScr.listUse = new MyVector(string.Empty);
		for (int i = 0; i < RadarScr.list.size(); i++)
		{
			Info_RadaScr info_RadaScr = (Info_RadaScr)RadarScr.list.elementAt(i);
			if (info_RadaScr != null && (int)info_RadaScr.isUse == 1)
			{
				RadarScr.listUse.addElement(info_RadaScr);
			}
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x000920D0 File Offset: 0x000904D0
	public void listIndex()
	{
		MyVector myVector = RadarScr.listUse;
		if (RadarScr.TYPE_UI)
		{
			myVector = RadarScr.list;
		}
		int num = (this.page - 1) * 5;
		int num2 = num + 5;
		for (int i = num; i < num2; i++)
		{
			if (i >= myVector.size())
			{
				RadarScr.index[i - num] = -1;
			}
			else
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)myVector.elementAt(i);
				if (info_RadaScr != null)
				{
					RadarScr.index[i - num] = info_RadaScr.id;
				}
			}
		}
		RadarScr.cmyText = 0;
		RadarScr.hText = 0;
		SoundMn.gI().radarItem();
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0009216C File Offset: 0x0009056C
	public override void update()
	{
		try
		{
			if (RadarScr.hText < 80)
			{
				RadarScr.hText += 4;
				if (RadarScr.hText > 80)
				{
					RadarScr.hText = 80;
				}
			}
			this.focus_card = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[this.indexFocus]);
			if (RadarScr.TYPE_UI)
			{
				this.focus_card = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[this.indexFocus]);
			}
			GameScr.gI().update();
			if (GameCanvas.gameTick % 10 < 6)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.dyArrow--;
				}
			}
			else
			{
				this.dyArrow = 0;
			}
			if (this.focus_card != null)
			{
				int num = (int)this.focus_card.amount * 100 / (int)this.focus_card.max_amount;
				this.hClip = num * RadarScr.imgBar_1.getHeight() / 100;
				int num2 = RadarScr.num * 100 / RadarScr.list.size();
				this.wClip = num2 * RadarScr.imgPro_1.getWidth() / 100;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-upd-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x000922C8 File Offset: 0x000906C8
	public override void updateKey()
	{
		if (InfoDlg.isLock)
		{
			return;
		}
		if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
		{
			this.updateKeyTouchControl();
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
			this.doKeyText(1);
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
			this.doKeyText(-1);
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = false;
			this.doKeyItem(1);
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false;
			this.doKeyItem(0);
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			this.doClickUse(1);
		}
		if (GameCanvas.keyPressed[13])
		{
			this.doClickUse(2);
		}
		if (GameCanvas.keyPressed[12])
		{
			GameCanvas.keyPressed[12] = false;
			this.doClickUse(0);
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00092478 File Offset: 0x00090878
	private void doChangeUI()
	{
		RadarScr.TYPE_UI = !RadarScr.TYPE_UI;
		this.page = 1;
		this.indexFocus = 0;
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 <= 0) ? 0 : 1);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 <= 0) ? 0 : 1);
		}
		this.listIndex();
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00092510 File Offset: 0x00090910
	private void updateKeyTouchControl()
	{
		if (GameCanvas.isPointerClick)
		{
			for (int i = 0; i < 5; i++)
			{
				if (GameCanvas.isPointerHoldIn(RadarScr.xyItem[i][0], RadarScr.xyItem[i][1], 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i != this.indexFocus)
				{
					this.doClickItem(i);
				}
			}
			if (GameCanvas.isPointerHoldIn(RadarScr.xyArrow[0][0] - 5, RadarScr.xyArrow[0][1] - 5, 20, 20))
			{
				if (GameCanvas.isPointerDown)
				{
					this.dxArrow[0] = 1;
				}
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.doClickArrow(0);
					this.dxArrow[0] = 0;
				}
			}
			if (GameCanvas.isPointerHoldIn(RadarScr.xyArrow[2][0] - 5, RadarScr.xyArrow[2][1] - 5, 20, 20))
			{
				if (GameCanvas.isPointerDown)
				{
					this.dxArrow[1] = 1;
				}
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.doClickArrow(1);
					this.dxArrow[1] = 0;
				}
			}
			for (int j = 0; j < RadarScr.xCmd.Length; j++)
			{
				if (GameCanvas.isPointerHoldIn(RadarScr.xCmd[j] - 5, RadarScr.yCmd - 5, 20, 20))
				{
					if (GameCanvas.isPointerDown)
					{
						RadarScr.dxCmd[j] = 1;
					}
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						this.doClickUse(j);
						RadarScr.dxCmd[j] = 0;
					}
				}
			}
		}
		else
		{
			RadarScr.dxCmd[0] = 0;
			RadarScr.dxCmd[1] = 0;
			RadarScr.dxCmd[2] = 0;
			this.dxArrow[0] = 0;
			this.dxArrow[1] = 0;
		}
		if (GameCanvas.isPointerHoldIn(RadarScr.xText, 0, RadarScr.wText, RadarScr.yText + RadarScr.hText))
		{
			if (GameCanvas.isPointerMove)
			{
				if (this.pyy == 0)
				{
					this.pyy = GameCanvas.py;
				}
				this.pxx = this.pyy - GameCanvas.py;
				if (this.pxx != 0)
				{
					RadarScr.cmyText += this.pxx;
					this.pyy = GameCanvas.py;
				}
				if (RadarScr.cmyText < 0)
				{
					RadarScr.cmyText = 0;
				}
				if (RadarScr.cmyText > this.focus_card.cp.lim)
				{
					RadarScr.cmyText = this.focus_card.cp.lim;
				}
			}
			else
			{
				this.pyy = 0;
				this.pyy = 0;
			}
		}
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0009279C File Offset: 0x00090B9C
	private void doClickUse(int i)
	{
		if (i == 0)
		{
			this.doChangeUI();
		}
		else if (i == 1)
		{
			if (this.focus_card != null)
			{
				Service.gI().SendRada(1, this.focus_card.id);
			}
		}
		else if (i == 2)
		{
			GameScr.gI().switchToMe();
		}
		SoundMn.gI().radarClick();
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00092804 File Offset: 0x00090C04
	private void doClickArrow(int dir)
	{
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 <= 0) ? 0 : 1);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 <= 0) ? 0 : 1);
		}
		int num = this.page;
		if (dir == 0)
		{
			if (this.page == 1)
			{
				return;
			}
			num--;
			if (num < 1)
			{
				num = 1;
			}
		}
		else
		{
			if (this.page == this.maxpage)
			{
				return;
			}
			num++;
			if (num > this.maxpage)
			{
				num = this.maxpage;
			}
		}
		if (num != this.page)
		{
			this.page = num;
			this.listIndex();
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x000928E6 File Offset: 0x00090CE6
	private void doClickItem(int focus)
	{
		this.indexFocus = focus;
		this.listIndex();
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x000928F8 File Offset: 0x00090CF8
	private void doKeyText(int type)
	{
		RadarScr.cmyText += 12 * type;
		if (RadarScr.cmyText < 0)
		{
			RadarScr.cmyText = 0;
		}
		if (RadarScr.cmyText > this.focus_card.cp.lim)
		{
			RadarScr.cmyText = this.focus_card.cp.lim;
		}
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00092954 File Offset: 0x00090D54
	private void doKeyItem(int type)
	{
		int num = this.indexFocus;
		int num2 = this.page;
		if (type == 0)
		{
			num++;
		}
		else
		{
			num--;
		}
		if (num >= RadarScr.index.Length)
		{
			if (this.page < this.maxpage)
			{
				num = 0;
				num2++;
			}
			else
			{
				num = RadarScr.index.Length - 1;
			}
		}
		if (num < 0)
		{
			if (this.page > 1)
			{
				num = RadarScr.index.Length - 1;
				num2--;
			}
			else
			{
				num = 0;
			}
		}
		if (num != this.indexFocus)
		{
			this.indexFocus = num;
			RadarScr.cmyText = 0;
			RadarScr.hText = 0;
		}
		if (num2 != this.page)
		{
			this.page = num2;
			this.listIndex();
		}
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00092A18 File Offset: 0x00090E18
	public override void paint(mGraphics g)
	{
		try
		{
			GameScr.gI().paint(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			g.translate(0, GameCanvas.transY);
			GameScr.resetTranslate(g);
			g.drawImage(RadarScr.imgUI, RadarScr.xUi, RadarScr.yUi, 0);
			g.drawImage(RadarScr.imgPro_0, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 - 2, 0);
			g.setClip(RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, this.wClip, RadarScr.imgPro_0.getHeight());
			g.drawImage(RadarScr.imgPro_1, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, 0);
			GameScr.resetTranslate(g);
			g.drawImage(RadarScr.imgChange, RadarScr.xCmd[0], RadarScr.yCmd + RadarScr.dxCmd[0], 0);
			g.drawImage(RadarScr.imgUse_0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			g.drawImage(RadarScr.imgBack, RadarScr.xCmd[2], RadarScr.yCmd + RadarScr.dxCmd[2], 0);
			if (RadarScr.TYPE_UI)
			{
				g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			}
			else
			{
				g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 1, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			}
			if (this.focus_card != null)
			{
				g.setClip(RadarScr.xUi + 30, RadarScr.yUi + 13, RadarScr.wUi - 60, RadarScr.hUi / 2);
				this.focus_card.paintInfo(g, RadarScr.xMon, RadarScr.yMon);
				GameScr.resetTranslate(g);
				mFont.tahoma_7b_yellow.drawString(g, (((int)this.focus_card.level <= 0) ? " " : ("Lv." + this.focus_card.level + " ")) + this.focus_card.name, RadarScr.xUi + RadarScr.wUi / 2, RadarScr.yUi + 15, 2);
				mFont.tahoma_7_white.drawString(g, "no." + this.focus_card.no, RadarScr.xUi + 30, RadarScr.yText - 2, 0);
				g.drawImage(RadarScr.imgBar_0, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
				g.setClip(RadarScr.xUi + 36, this.yClip - this.hClip, 7, this.hClip);
				g.drawImage(RadarScr.imgBar_1, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
				GameScr.resetTranslate(g);
				g.drawImage(RadarScr.imgRank[(int)this.focus_card.rank], RadarScr.xUi + 39 - 5 + 14, RadarScr.yText + 12, 0);
			}
			g.setClip(RadarScr.xText, RadarScr.yText, RadarScr.wText + 5, RadarScr.hText + 8);
			if (this.focus_card != null)
			{
				g.drawImage(RadarScr.imgUIText, RadarScr.xText, RadarScr.yText, 0);
			}
			GameScr.resetTranslate(g);
			g.setClip(RadarScr.xText, RadarScr.yText + 1, RadarScr.wText, RadarScr.hText + 5);
			if (this.focus_card != null && this.focus_card.cp != null)
			{
				if (this.focus_card.cp.says == null)
				{
					return;
				}
				this.focus_card.cp.paintRada(g, RadarScr.cmyText);
			}
			GameScr.resetTranslate(g);
			if ((!RadarScr.TYPE_UI && RadarScr.listUse.size() > 5) || RadarScr.TYPE_UI)
			{
				if (this.page > 1)
				{
					g.drawImage(RadarScr.imgArrow_Left, RadarScr.xyArrow[0][0], RadarScr.xyArrow[0][1] + this.dxArrow[0], 0);
				}
				if (this.page < this.maxpage)
				{
					g.drawImage(RadarScr.imgArrow_Right, RadarScr.xyArrow[2][0], RadarScr.xyArrow[2][1] + this.dxArrow[1], 0);
				}
			}
			for (int i = 0; i < RadarScr.index.Length; i++)
			{
				int num = 0;
				int num2 = 0;
				int idx = 0;
				if (i == this.indexFocus)
				{
					num = this.dyArrow;
					num2 = -10;
					idx = 1;
					g.drawImage(RadarScr.imgArrow_Down, RadarScr.xyItem[i][0] + 10, RadarScr.xyItem[i][1] + this.dyArrow + 29 + num2, 0);
				}
				Info_RadaScr info = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[i]);
				if (RadarScr.TYPE_UI)
				{
					info = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[i]);
				}
				if (info != null)
				{
					RadarScr.fraImgFocus.drawFrame((int)info.rank, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					SmallImage.drawSmallImage(g, info.idIcon, RadarScr.xyItem[i][0] + 14, RadarScr.xyItem[i][1] + 14 + num + num2, 0, StaticObj.VCENTER_HCENTER);
					info.paintEff(g, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2);
					if ((int)info.level == 0)
					{
						g.drawImage(RadarScr.imgLock, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0);
					}
					if (i == this.indexFocus)
					{
						RadarScr.fraImgFocus.drawFrame(7, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
					if ((int)info.isUse == 1)
					{
						RadarScr.fraImgFocus.drawFrame(8, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
				}
				else
				{
					RadarScr.fraImgFocusNone.drawFrame(idx, RadarScr.xyItem[i][0] - 1, RadarScr.xyItem[i][1] - 1 + num + num2, 0, 0, g);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-pnt-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x000930B8 File Offset: 0x000914B8
	public override void switchToMe()
	{
		GameScr.isPaintOther = true;
		base.switchToMe();
	}

	// Token: 0x04001195 RID: 4501
	public const sbyte SUBCMD_ALL = 0;

	// Token: 0x04001196 RID: 4502
	public const sbyte SUBCMD_USE = 1;

	// Token: 0x04001197 RID: 4503
	public const sbyte SUBCMD_LEVEL = 2;

	// Token: 0x04001198 RID: 4504
	public const sbyte SUBCMD_AMOUNT = 3;

	// Token: 0x04001199 RID: 4505
	public const sbyte SUBCMD_AURA = 4;

	// Token: 0x0400119A RID: 4506
	public static RadarScr instance;

	// Token: 0x0400119B RID: 4507
	public static bool TYPE_UI;

	// Token: 0x0400119C RID: 4508
	public static FrameImage fraImgFocus;

	// Token: 0x0400119D RID: 4509
	public static FrameImage fraImgFocusNone;

	// Token: 0x0400119E RID: 4510
	public static FrameImage fraEff;

	// Token: 0x0400119F RID: 4511
	private static Image imgUI;

	// Token: 0x040011A0 RID: 4512
	private static Image imgUIText;

	// Token: 0x040011A1 RID: 4513
	private static Image imgArrow_Left;

	// Token: 0x040011A2 RID: 4514
	private static Image imgArrow_Right;

	// Token: 0x040011A3 RID: 4515
	private static Image imgArrow_Down;

	// Token: 0x040011A4 RID: 4516
	private static Image imgLock;

	// Token: 0x040011A5 RID: 4517
	private static Image imgUse_0;

	// Token: 0x040011A6 RID: 4518
	private static Image imgUse;

	// Token: 0x040011A7 RID: 4519
	private static Image imgBack;

	// Token: 0x040011A8 RID: 4520
	private static Image imgChange;

	// Token: 0x040011A9 RID: 4521
	private static Image imgBar_0;

	// Token: 0x040011AA RID: 4522
	private static Image imgBar_1;

	// Token: 0x040011AB RID: 4523
	private static Image imgPro_0;

	// Token: 0x040011AC RID: 4524
	private static Image imgPro_1;

	// Token: 0x040011AD RID: 4525
	private static Image[] imgRank;

	// Token: 0x040011AE RID: 4526
	public static int xUi;

	// Token: 0x040011AF RID: 4527
	public static int yUi;

	// Token: 0x040011B0 RID: 4528
	public static int wUi;

	// Token: 0x040011B1 RID: 4529
	public static int hUi;

	// Token: 0x040011B2 RID: 4530
	public static int xMon;

	// Token: 0x040011B3 RID: 4531
	public static int yMon;

	// Token: 0x040011B4 RID: 4532
	public static int xText;

	// Token: 0x040011B5 RID: 4533
	public static int yText;

	// Token: 0x040011B6 RID: 4534
	public static int wText;

	// Token: 0x040011B7 RID: 4535
	public static int cmyText;

	// Token: 0x040011B8 RID: 4536
	public static int hText;

	// Token: 0x040011B9 RID: 4537
	public static int yCmd;

	// Token: 0x040011BA RID: 4538
	public static int[] xCmd = new int[0];

	// Token: 0x040011BB RID: 4539
	public static int[] dxCmd = new int[0];

	// Token: 0x040011BC RID: 4540
	private static int[][] xyArrow;

	// Token: 0x040011BD RID: 4541
	private static int[][] xyItem;

	// Token: 0x040011BE RID: 4542
	private static int[] index = new int[]
	{
		-2,
		-1,
		0,
		1,
		2
	};

	// Token: 0x040011BF RID: 4543
	private int dyArrow;

	// Token: 0x040011C0 RID: 4544
	private int[] dxArrow;

	// Token: 0x040011C1 RID: 4545
	private int page;

	// Token: 0x040011C2 RID: 4546
	private int maxpage;

	// Token: 0x040011C3 RID: 4547
	private int indexFocus;

	// Token: 0x040011C4 RID: 4548
	public static MyVector list;

	// Token: 0x040011C5 RID: 4549
	public static MyVector listUse;

	// Token: 0x040011C6 RID: 4550
	private static int num;

	// Token: 0x040011C7 RID: 4551
	private static int numMax;

	// Token: 0x040011C8 RID: 4552
	private Info_RadaScr focus_card;

	// Token: 0x040011C9 RID: 4553
	private int pxx;

	// Token: 0x040011CA RID: 4554
	private int pyy;

	// Token: 0x040011CB RID: 4555
	private int xClip;

	// Token: 0x040011CC RID: 4556
	private int wClip;

	// Token: 0x040011CD RID: 4557
	private int yClip;

	// Token: 0x040011CE RID: 4558
	private int hClip;
}
