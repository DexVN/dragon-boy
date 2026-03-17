using System;

// Token: 0x02000085 RID: 133
public class Paint
{
	// Token: 0x06000687 RID: 1671 RVA: 0x0006E9EC File Offset: 0x0006CBEC
	public static void loadbg()
	{
		for (int i = 0; i < Paint.goc.Length; i++)
		{
			Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1).ToString() + ".png");
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0006EA38 File Offset: 0x0006CC38
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

	// Token: 0x06000689 RID: 1673 RVA: 0x0006EB6A File Offset: 0x0006CD6A
	public void paintfillDefaultBg(mGraphics g)
	{
		g.setColor(205314);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00003136 File Offset: 0x00001336
	public void repaintCircleBg()
	{
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00003136 File Offset: 0x00001336
	public void paintSolidBg(mGraphics g)
	{
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0006EB8C File Offset: 0x0006CD8C
	public void paintDefaultPopup(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(8411138);
		g.fillRect(x, y, w, h);
		g.setColor(13606712);
		g.drawRect(x, y, w, h);
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0006EBC1 File Offset: 0x0006CDC1
	public void paintWhitePopup(mGraphics g, int y, int x, int width, int height)
	{
		g.setColor(16776363);
		g.fillRect(x, y, width, height);
		g.setColor(0);
		g.drawRect(x - 1, y - 1, width + 1, height + 1);
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0006EBFC File Offset: 0x0006CDFC
	public void paintDefaultPopupH(mGraphics g, int h)
	{
		g.setColor(14279153);
		g.fillRect(8, GameCanvas.h - (h + 37), GameCanvas.w - 16, h + 4);
		g.setColor(4682453);
		g.fillRect(10, GameCanvas.h - (h + 35), GameCanvas.w - 20, h);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0006EC5C File Offset: 0x0006CE5C
	public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
	{
		mFont mFont = (!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark;
		int num = 3;
		bool flag = left != null;
		if (flag)
		{
			Paint.lenCaption = mFont.getWidth(left.caption);
			bool flag2 = Paint.lenCaption > 0;
			if (flag2)
			{
				bool flag3 = left.x >= 0 && left.y > 0;
				if (flag3)
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
		bool flag4 = center != null;
		if (flag4)
		{
			Paint.lenCaption = mFont.getWidth(center.caption);
			bool flag5 = Paint.lenCaption > 0;
			if (flag5)
			{
				bool flag6 = center.x > 0 && center.y > 0;
				if (flag6)
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
		bool flag7 = right != null;
		if (flag7)
		{
			Paint.lenCaption = mFont.getWidth(right.caption);
			bool flag8 = Paint.lenCaption > 0;
			if (flag8)
			{
				bool flag9 = right.x > 0 && right.y > 0;
				if (flag9)
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

	// Token: 0x06000690 RID: 1680 RVA: 0x00003136 File Offset: 0x00001336
	public void paintTabSoft(mGraphics g)
	{
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0006EE84 File Offset: 0x0006D084
	public void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774843);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0006EEA0 File Offset: 0x0006D0A0
	public void paintLogo(mGraphics g, int x, int y)
	{
		g.drawImage(Paint.imgLogo, x, y, 3);
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00003136 File Offset: 0x00001336
	public void paintHotline(mGraphics g, string number)
	{
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0006EEB4 File Offset: 0x0006D0B4
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

	// Token: 0x06000695 RID: 1685 RVA: 0x00003136 File Offset: 0x00001336
	public void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check)
	{
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00003136 File Offset: 0x00001336
	public void paintDefaultScrList(mGraphics g, string title, string subTitle, string check)
	{
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0006EF3C File Offset: 0x0006D13C
	public void paintCheck(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgTick[1], x, y, 3);
		bool flag = index == 1;
		if (flag)
		{
			g.drawImage(Paint.imgTick[0], x + 1, y - 3, 3);
		}
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0006EF7B File Offset: 0x0006D17B
	public void paintImgMsg(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgMsg[index], x, y, 0);
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0006EF90 File Offset: 0x0006D190
	public void paintTitleBoard(mGraphics g, int roomId)
	{
		this.paintDefaultBg(g);
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0006EF9C File Offset: 0x0006D19C
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

	// Token: 0x0600069B RID: 1691 RVA: 0x0006EFF8 File Offset: 0x0006D1F8
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

	// Token: 0x0600069C RID: 1692 RVA: 0x00003136 File Offset: 0x00001336
	public void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool isSe, int i, int wStr)
	{
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0006F060 File Offset: 0x0006D260
	public void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo)
	{
		g.setColor(16774843);
		g.drawLine(x, y, xTo, yTo);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0006F07C File Offset: 0x0006D27C
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

	// Token: 0x0600069F RID: 1695 RVA: 0x0006F0CC File Offset: 0x0006D2CC
	public void paintScroll(mGraphics g, int x, int y, int h)
	{
		g.setColor(3847752);
		g.fillRect(x, y, 4, h);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0006F0E8 File Offset: 0x0006D2E8
	public int[] getColorMsg()
	{
		return this.color;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0006F100 File Offset: 0x0006D300
	public void paintLogo(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgLogo, GameCanvas.h >> 1, GameCanvas.w >> 1, 3);
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0006F140 File Offset: 0x0006D340
	public void paintTextLogin(mGraphics g, bool isRes)
	{
		int num = 0;
		bool flag = !isRes && GameCanvas.h <= 240;
		if (flag)
		{
			num = 15;
		}
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[0], GameCanvas.hw, GameCanvas.hh + 60 - num, 2);
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[1], GameCanvas.hw, GameCanvas.hh + 73 - num, 2);
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0006F1B4 File Offset: 0x0006D3B4
	public void paintSellectBoard(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(Paint.imgSelectBoard, x - 7, y, 0);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0006F1C8 File Offset: 0x0006D3C8
	public int isRegisterUsingWAP()
	{
		return 0;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0006F1DC File Offset: 0x0006D3DC
	public string getCard()
	{
		return "/vmg/card.on";
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0006F1F3 File Offset: 0x0006D3F3
	public void paintSellectedShop(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16777215);
		g.drawRect(x, y, 40, 40);
		g.drawRect(x + 1, y + 1, 38, 38);
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0006F220 File Offset: 0x0006D420
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

	// Token: 0x060006A8 RID: 1704 RVA: 0x00003136 File Offset: 0x00001336
	public void doSelect(int focus)
	{
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0006F264 File Offset: 0x0006D464
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

	// Token: 0x060006AA RID: 1706 RVA: 0x0006F498 File Offset: 0x0006D698
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

	// Token: 0x060006AB RID: 1707 RVA: 0x0006F5C1 File Offset: 0x0006D7C1
	public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(6702080);
		g.fillRect(x, y, w, h);
		g.setColor(14338484);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0006F600 File Offset: 0x0006D800
	public void paintFrameBorder(int x, int y, int w, int h, mGraphics g)
	{
		this.paintFrame(x, y, w, h, g);
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0006F611 File Offset: 0x0006D811
	public void paintFrameInside(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0006F62E File Offset: 0x0006D82E
	public void paintFrameInsideSelected(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORLIGHT);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x04000E64 RID: 3684
	public static int COLORBACKGROUND = 15787715;

	// Token: 0x04000E65 RID: 3685
	public static int COLORLIGHT = 16383818;

	// Token: 0x04000E66 RID: 3686
	public static int COLORDARK = 3937280;

	// Token: 0x04000E67 RID: 3687
	public static int COLORBORDER = 15224576;

	// Token: 0x04000E68 RID: 3688
	public static int COLORFOCUS = 16777215;

	// Token: 0x04000E69 RID: 3689
	public static Image imgBg;

	// Token: 0x04000E6A RID: 3690
	public static Image imgLogo;

	// Token: 0x04000E6B RID: 3691
	public static Image imgLB;

	// Token: 0x04000E6C RID: 3692
	public static Image imgLT;

	// Token: 0x04000E6D RID: 3693
	public static Image imgRB;

	// Token: 0x04000E6E RID: 3694
	public static Image imgRT;

	// Token: 0x04000E6F RID: 3695
	public static Image imgChuong;

	// Token: 0x04000E70 RID: 3696
	public static Image imgSelectBoard;

	// Token: 0x04000E71 RID: 3697
	public static Image imgtoiSmall;

	// Token: 0x04000E72 RID: 3698
	public static Image imgTayTren;

	// Token: 0x04000E73 RID: 3699
	public static Image imgTayDuoi;

	// Token: 0x04000E74 RID: 3700
	public static Image[] imgTick = new Image[2];

	// Token: 0x04000E75 RID: 3701
	public static Image[] imgMsg = new Image[2];

	// Token: 0x04000E76 RID: 3702
	public static Image[] goc = new Image[6];

	// Token: 0x04000E77 RID: 3703
	public static int hTab = 24;

	// Token: 0x04000E78 RID: 3704
	public static int lenCaption = 0;

	// Token: 0x04000E79 RID: 3705
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

	// Token: 0x04000E7A RID: 3706
	public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
}
