using System;

// Token: 0x02000074 RID: 116
public class Paint
{
	// Token: 0x060003DC RID: 988 RVA: 0x000313A0 File Offset: 0x0002F7A0
	public static void loadbg()
	{
		for (int i = 0; i < Paint.goc.Length; i++)
		{
			Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1) + ".png");
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x000313E8 File Offset: 0x0002F7E8
	public void paintDefaultBg(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgBg, GameCanvas.w / 2, GameCanvas.h / 2 - Paint.hTab / 2 - 1, 3);
		g.drawImage(Paint.imgLT, 0, 0, 0);
		g.drawImage(Paint.imgRT, GameCanvas.w, 0, mGraphics.TOP | mGraphics.RIGHT);
		g.drawImage(Paint.imgLB, 0, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.LEFT);
		g.drawImage(Paint.imgRB, GameCanvas.w, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.RIGHT);
		g.setColor(16774843);
		g.drawRect(0, 0, GameCanvas.w, 0);
		g.drawRect(0, GameCanvas.h - Paint.hTab - 2, GameCanvas.w, 0);
		g.drawRect(0, 0, 0, GameCanvas.h - Paint.hTab);
		g.drawRect(GameCanvas.w - 1, 0, 0, GameCanvas.h - Paint.hTab);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0003150D File Offset: 0x0002F90D
	public void paintfillDefaultBg(mGraphics g)
	{
		g.setColor(205314);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0003152C File Offset: 0x0002F92C
	public void repaintCircleBg()
	{
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0003152E File Offset: 0x0002F92E
	public void paintSolidBg(mGraphics g)
	{
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00031530 File Offset: 0x0002F930
	public void paintDefaultPopup(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(8411138);
		g.fillRect(x, y, w, h);
		g.setColor(13606712);
		g.drawRect(x, y, w, h);
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00031560 File Offset: 0x0002F960
	public void paintWhitePopup(mGraphics g, int y, int x, int width, int height)
	{
		g.setColor(16776363);
		g.fillRect(x, y, width, height);
		g.setColor(0);
		g.drawRect(x - 1, y - 1, width + 1, height + 1);
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00031594 File Offset: 0x0002F994
	public void paintDefaultPopupH(mGraphics g, int h)
	{
		g.setColor(14279153);
		g.fillRect(8, GameCanvas.h - (h + 37), GameCanvas.w - 16, h + 4);
		g.setColor(4682453);
		g.fillRect(10, GameCanvas.h - (h + 35), GameCanvas.w - 20, h);
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000315F0 File Offset: 0x0002F9F0
	public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
	{
		mFont mFont = (!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark;
		int num = 3;
		if (left != null)
		{
			Paint.lenCaption = mFont.getWidth(left.caption);
			if (Paint.lenCaption > 0)
			{
				if (left.x >= 0 && left.y > 0)
				{
					left.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 0) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, 1, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, left.caption, 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		if (center != null)
		{
			Paint.lenCaption = mFont.getWidth(center.caption);
			if (Paint.lenCaption > 0)
			{
				if (center.x > 0 && center.y > 0)
				{
					center.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.hw - 35, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, center.caption, GameCanvas.hw, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		if (right != null)
		{
			Paint.lenCaption = mFont.getWidth(right.caption);
			if (Paint.lenCaption > 0)
			{
				if (right.x > 0 && right.y > 0)
				{
					right.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 2) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, right.caption, GameCanvas.w - 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000317F1 File Offset: 0x0002FBF1
	public void paintTabSoft(mGraphics g)
	{
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x000317F3 File Offset: 0x0002FBF3
	public void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774843);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0003180C File Offset: 0x0002FC0C
	public void paintLogo(mGraphics g, int x, int y)
	{
		g.drawImage(Paint.imgLogo, x, y, 3);
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0003181C File Offset: 0x0002FC1C
	public void paintHotline(mGraphics g, string number)
	{
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00031820 File Offset: 0x0002FC20
	public void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(16646144);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16770612);
		}
		else
		{
			g.setColor(16775097);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16775097);
		}
		g.fillRoundRect(x + 3, y + 3, w - 6, h - 6, 10, 10);
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0003189D File Offset: 0x0002FC9D
	public void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check)
	{
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0003189F File Offset: 0x0002FC9F
	public void paintDefaultScrList(mGraphics g, string title, string subTitle, string check)
	{
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000318A1 File Offset: 0x0002FCA1
	public void paintCheck(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgTick[1], x, y, 3);
		if (index == 1)
		{
			g.drawImage(Paint.imgTick[0], x + 1, y - 3, 3);
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000318CF File Offset: 0x0002FCCF
	public void paintImgMsg(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgMsg[index], x, y, 0);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000318E2 File Offset: 0x0002FCE2
	public void paintTitleBoard(mGraphics g, int roomId)
	{
		this.paintDefaultBg(g);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000318EC File Offset: 0x0002FCEC
	public void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus)
	{
		if (focus)
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 1 : 3) * 18, 20, 18, 0, x, y, 0);
		}
		else
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 0 : 2) * 18, 20, 18, 0, x, y, 0);
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00031950 File Offset: 0x0002FD50
	public void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str)
	{
		this.paintFrame(x, y, w, h, g);
		int num = y + 20 - mFont.tahoma_8b.getHeight();
		int i = 0;
		int num2 = num;
		while (i < str.Length)
		{
			mFont.tahoma_8b.drawString(g, str[i], x + w / 2, num2, 2);
			i++;
			num2 += mFont.tahoma_8b.getHeight();
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000319B5 File Offset: 0x0002FDB5
	public void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool isSe, int i, int wStr)
	{
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x000319B7 File Offset: 0x0002FDB7
	public void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo)
	{
		g.setColor(16774843);
		g.drawLine(x, y, xTo, yTo);
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000319D0 File Offset: 0x0002FDD0
	public void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(13132288);
			g.fillRect(x + 2, y + 2, w - 3, w - 3);
		}
		g.setColor(3502080);
		g.drawRect(x, y, w, w);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00031A0F File Offset: 0x0002FE0F
	public void paintScroll(mGraphics g, int x, int y, int h)
	{
		g.setColor(3847752);
		g.fillRect(x, y, 4, h);
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00031A27 File Offset: 0x0002FE27
	public int[] getColorMsg()
	{
		return this.color;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00031A2F File Offset: 0x0002FE2F
	public void paintLogo(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgLogo, GameCanvas.h >> 1, GameCanvas.w >> 1, 3);
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00031A68 File Offset: 0x0002FE68
	public void paintTextLogin(mGraphics g, bool isRes)
	{
		int num = 0;
		if (!isRes && GameCanvas.h <= 240)
		{
			num = 15;
		}
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[0], GameCanvas.hw, GameCanvas.hh + 60 - num, 2);
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[1], GameCanvas.hw, GameCanvas.hh + 73 - num, 2);
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00031AD3 File Offset: 0x0002FED3
	public void paintSellectBoard(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(Paint.imgSelectBoard, x - 7, y, 0);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00031AE5 File Offset: 0x0002FEE5
	public int isRegisterUsingWAP()
	{
		return 0;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00031AE8 File Offset: 0x0002FEE8
	public string getCard()
	{
		return "/vmg/card.on";
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00031AEF File Offset: 0x0002FEEF
	public void paintSellectedShop(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16777215);
		g.drawRect(x, y, 40, 40);
		g.drawRect(x + 1, y + 1, 38, 38);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00031B18 File Offset: 0x0002FF18
	public string getUrlUpdateGame()
	{
		return string.Concat(new object[]
		{
			"http://wap.teamobi.com?info=checkupdate&game=3&version=",
			GameMidlet.VERSION,
			"&provider=",
			GameMidlet.PROVIDER
		});
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00031B4A File Offset: 0x0002FF4A
	public void doSelect(int focus)
	{
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00031B4C File Offset: 0x0002FF4C
	public void paintPopUp(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(9340251);
		g.drawRect(x + 18, y, (w - 36) / 2 - 32, h);
		g.drawRect(x + 18 + (w - 36) / 2 + 32, y, (w - 36) / 2 - 22, h);
		g.drawRect(x, y + 8, w, h - 17);
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x + 18, y + 3, (w - 36) / 2 - 32, h - 4);
		g.fillRect(x + 18 + (w - 36) / 2 + 31, y + 3, (w - 38) / 2 - 22, h - 4);
		g.fillRect(x + 1, y + 6, w - 1, h - 11);
		g.setColor(14667919);
		g.fillRect(x + 18, y + 1, (w - 36) / 2 - 32, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + 1, (w - 36) / 2 - 12, 2);
		g.fillRect(x + 18, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 1, y + 11, 2, h - 18);
		g.fillRect(x + w - 2, y + 11, 2, h - 18);
		g.drawImage(Paint.goc[0], x - 3, y - 2, mGraphics.TOP | mGraphics.LEFT);
		g.drawImage(Paint.goc[2], x + w + 3, y - 2, StaticObj.TOP_RIGHT);
		g.drawImage(Paint.goc[1], x - 3, y + h + 3, StaticObj.BOTTOM_LEFT);
		g.drawImage(Paint.goc[3], x + w + 4, y + h + 2, StaticObj.BOTTOM_RIGHT);
		g.drawImage(Paint.goc[4], x + w / 2, y, StaticObj.TOP_CENTER);
		g.drawImage(Paint.goc[5], x + w / 2, y + h + 1, StaticObj.BOTTOM_HCENTER);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00031D6C File Offset: 0x0003016C
	public void paintFrame(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(13524492);
		g.drawRect(x + 6, y, w - 12, h);
		g.drawRect(x, y + 6, w, h - 12);
		g.drawRect(x + 7, y + 1, w - 14, h - 2);
		g.drawRect(x + 1, y + 7, w - 2, h - 14);
		g.setColor(14338484);
		g.fillRect(x + 8, y + 2, w - 16, h - 3);
		g.fillRect(x + 2, y + 8, w - 3, h - 14);
		g.drawImage(GameCanvas.imgBorder[2], x, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 2, x + w + 1, y, StaticObj.TOP_RIGHT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 1, x, y + h + 1, StaticObj.BOTTOM_LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 3, x + w + 1, y + h + 1, StaticObj.BOTTOM_RIGHT);
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00031E88 File Offset: 0x00030288
	public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(6702080);
		g.fillRect(x, y, w, h);
		g.setColor(14338484);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00031EC2 File Offset: 0x000302C2
	public void paintFrameBorder(int x, int y, int w, int h, mGraphics g)
	{
		this.paintFrame(x, y, w, h, g);
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00031ED1 File Offset: 0x000302D1
	public void paintFrameInside(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00031EEB File Offset: 0x000302EB
	public void paintFrameInsideSelected(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORLIGHT);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x040006BD RID: 1725
	public static int COLORBACKGROUND = 15787715;

	// Token: 0x040006BE RID: 1726
	public static int COLORLIGHT = 16383818;

	// Token: 0x040006BF RID: 1727
	public static int COLORDARK = 3937280;

	// Token: 0x040006C0 RID: 1728
	public static int COLORBORDER = 15224576;

	// Token: 0x040006C1 RID: 1729
	public static int COLORFOCUS = 16777215;

	// Token: 0x040006C2 RID: 1730
	public static Image imgBg;

	// Token: 0x040006C3 RID: 1731
	public static Image imgLogo;

	// Token: 0x040006C4 RID: 1732
	public static Image imgLB;

	// Token: 0x040006C5 RID: 1733
	public static Image imgLT;

	// Token: 0x040006C6 RID: 1734
	public static Image imgRB;

	// Token: 0x040006C7 RID: 1735
	public static Image imgRT;

	// Token: 0x040006C8 RID: 1736
	public static Image imgChuong;

	// Token: 0x040006C9 RID: 1737
	public static Image imgSelectBoard;

	// Token: 0x040006CA RID: 1738
	public static Image imgtoiSmall;

	// Token: 0x040006CB RID: 1739
	public static Image imgTayTren;

	// Token: 0x040006CC RID: 1740
	public static Image imgTayDuoi;

	// Token: 0x040006CD RID: 1741
	public static Image[] imgTick = new Image[2];

	// Token: 0x040006CE RID: 1742
	public static Image[] imgMsg = new Image[2];

	// Token: 0x040006CF RID: 1743
	public static Image[] goc = new Image[6];

	// Token: 0x040006D0 RID: 1744
	public static int hTab = 24;

	// Token: 0x040006D1 RID: 1745
	public static int lenCaption = 0;

	// Token: 0x040006D2 RID: 1746
	public int[] color = new int[]
	{
		15970400,
		13479911,
		2250052,
		16374659,
		15906669,
		12931125,
		3108954
	};

	// Token: 0x040006D3 RID: 1747
	public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
}
