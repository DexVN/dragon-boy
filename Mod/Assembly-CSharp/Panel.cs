using System;
using System.Collections.Generic;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class Panel : IActionListener, IChatable
{
	// Token: 0x060006B1 RID: 1713 RVA: 0x0006F6E8 File Offset: 0x0006D8E8
	public Panel()
	{
		this.init();
		this.cmdClose = new Command(string.Empty, this, 1003, null);
		this.cmdClose.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		this.cmdClose.cmdClosePanel = true;
		this.currItem = null;
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
	public static void loadBg()
	{
		Panel.imgMap = GameCanvas.loadImage("/img/map" + TileMap.planetID.ToString() + ".png");
		Panel.imgBantay = GameCanvas.loadImage("/mainImage/myTexture2dbantay.png");
		Panel.imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		Panel.imgXu = GameCanvas.loadImage("/mainImage/myTexture2dimgMoney.png");
		Panel.imgLuong = GameCanvas.loadImage("/mainImage/myTexture2dimgDiamond.png");
		Panel.imgLuongKhoa = GameCanvas.loadImage("/mainImage/luongkhoa.png");
		Panel.imgUp = GameCanvas.loadImage("/mainImage/myTexture2dup.png");
		Panel.imgDown = GameCanvas.loadImage("/mainImage/myTexture2ddown.png");
		Panel.imgStar = GameCanvas.loadImage("/mainImage/star.png");
		Panel.imgMaxStar = GameCanvas.loadImage("/mainImage/starE.png");
		Panel.imgStar8 = GameCanvas.loadImage("/mainImage/star8.png");
		Panel.imgNew = GameCanvas.loadImage("/mainImage/new.png");
		Panel.imgTicket = GameCanvas.loadImage("/mainImage/ticket12.png");
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0006FBD8 File Offset: 0x0006DDD8
	public void init()
	{
		this.pX = GameCanvas.pxLast + this.cmxMap;
		this.pY = GameCanvas.pyLast + this.cmyMap;
		this.lastTabIndex = new int[this.tabName.Length];
		for (int i = 0; i < this.lastTabIndex.Length; i++)
		{
			this.lastTabIndex[i] = -1;
		}
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0006FC40 File Offset: 0x0006DE40
	public int getXMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			bool flag = TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i];
			if (flag)
			{
				return Panel.mapX[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0006FC9C File Offset: 0x0006DE9C
	public int getYMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			bool flag = TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i];
			if (flag)
			{
				return Panel.mapY[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0006FCF8 File Offset: 0x0006DEF8
	public int getXMapTask()
	{
		bool flag = global::Char.myCharz().taskMaint == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag2 = GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i];
				if (flag2)
				{
					return Panel.mapX[(int)TileMap.planetID][i];
				}
			}
			result = -1;
		}
		return result;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0006FD7C File Offset: 0x0006DF7C
	public int getYMapTask()
	{
		bool flag = global::Char.myCharz().taskMaint == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag2 = GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i];
				if (flag2)
				{
					return Panel.mapY[(int)TileMap.planetID][i];
				}
			}
			result = -1;
		}
		return result;
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0006FE00 File Offset: 0x0006E000
	private void setType(int position)
	{
		this.typeShop = -1;
		this.W = Panel.WIDTH_PANEL;
		this.H = GameCanvas.h;
		this.X = 0;
		this.Y = 0;
		this.ITEM_HEIGHT = 24;
		this.position = position;
		bool flag = position == 0;
		if (flag)
		{
			this.xScroll = 2;
			this.yScroll = 80;
			this.wScroll = this.W - 4;
			this.hScroll = this.H - 96;
			this.cmx = this.wScroll;
			this.cmtoX = 0;
			this.X = 0;
		}
		else
		{
			bool flag2 = position == 1;
			if (flag2)
			{
				this.wScroll = this.W - 4;
				this.xScroll = GameCanvas.w - this.wScroll;
				this.yScroll = 80;
				this.hScroll = this.H - 96;
				this.X = this.xScroll - 2;
				this.cmx = -(GameCanvas.w + this.W);
				this.cmtoX = GameCanvas.w - this.W;
			}
		}
		this.TAB_W = this.W / 5 - 1;
		this.currentTabIndex = 0;
		this.currentTabName = this.tabName[this.type];
		bool flag3 = this.currentTabName.Length < 5;
		if (flag3)
		{
			this.TAB_W += 5;
		}
		this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
		this.lastSelect = new int[this.currentTabName.Length];
		this.cmyLast = new int[this.currentTabName.Length];
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			this.lastSelect[i] = ((!GameCanvas.isTouch) ? 0 : -1);
		}
		bool flag4 = this.lastTabIndex[this.type] != -1;
		if (flag4)
		{
			this.currentTabIndex = this.lastTabIndex[this.type];
		}
		bool flag5 = this.currentTabIndex < 0;
		if (flag5)
		{
			this.currentTabIndex = 0;
		}
		bool flag6 = this.currentTabIndex > this.currentTabName.Length - 1;
		if (flag6)
		{
			this.currentTabIndex = this.currentTabName.Length - 1;
		}
		this.scroll = null;
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0007004C File Offset: 0x0006E24C
	public void setTypeMapTrans()
	{
		this.type = 14;
		this.setType(0);
		this.setTabMapTrans();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00070081 File Offset: 0x0006E281
	public void setTypeInfomatioin()
	{
		this.type = 6;
		this.cmx = this.wScroll;
		this.cmtoX = 0;
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x000700A0 File Offset: 0x0006E2A0
	public void setTypeMap()
	{
		bool flag = GameScr.gI().isMapFize();
		if (!flag)
		{
			bool flag2 = !Panel.isPaintMap;
			if (!flag2)
			{
				bool flag3 = Hint.isOnTask(2, 0);
				if (flag3)
				{
					Hint.isViewMap = true;
					GameScr.info1.addInfo(mResources.go_to_quest, 0);
				}
				bool flag4 = Hint.isOnTask(3, 0);
				if (flag4)
				{
					Hint.isViewPotential = true;
				}
				this.type = 4;
				this.currentTabName = this.tabName[this.type];
				this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
				this.cmx = (this.cmtoX = 0);
				this.setTabMap();
			}
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00070168 File Offset: 0x0006E368
	public void setTypeArchivement()
	{
		this.currentListLength = global::Char.myCharz().arrArchive.Length;
		this.setType(0);
		this.type = 9;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00070238 File Offset: 0x0006E438
	public void setTypeKiGuiOnly()
	{
		this.type = 17;
		this.setType(1);
		this.setTabKiGui();
		this.typeShop = 2;
		this.currentTabIndex = 0;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00070260 File Offset: 0x0006E460
	public void setTabChatManager()
	{
		this.currentListLength = this.chats.Count;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00003136 File Offset: 0x00001336
	public void setTabChatPlayer()
	{
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00003136 File Offset: 0x00001336
	public void setTypeChatPlayer()
	{
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00070310 File Offset: 0x0006E510
	public void setTabKiGui()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().arrItemShop[4].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x000703DF File Offset: 0x0006E5DF
	public void setTypeBodyOnly()
	{
		this.type = 7;
		this.setType(1);
		this.setTabInventory(true);
		this.currentTabIndex = 0;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00070400 File Offset: 0x0006E600
	public void addChatMessage(InfoItem info)
	{
		this.logChat.insertElementAt(info, 0);
		bool flag = this.logChat.size() > 20;
		if (flag)
		{
			this.logChat.removeElementAt(this.logChat.size() - 1);
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0007044C File Offset: 0x0006E64C
	private bool IsNewMessage(string name)
	{
		return false;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00070460 File Offset: 0x0006E660
	public bool IsHaveNewMessage()
	{
		return false;
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00003136 File Offset: 0x00001336
	private void ClearNewMessage(string name)
	{
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00070473 File Offset: 0x0006E673
	public void addPlayerMenu(Command pm)
	{
		this.vPlayerMenu.addElement(pm);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00070484 File Offset: 0x0006E684
	public void setTabPlayerMenu()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.vPlayerMenu.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00070550 File Offset: 0x0006E750
	public void setTypeFlag()
	{
		this.type = 18;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabFlag();
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00070584 File Offset: 0x0006E784
	public void setTabFlag()
	{
		this.currentListLength = this.vFlag.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0007066D File Offset: 0x0006E86D
	public void setTypePlayerMenu(global::Char c)
	{
		this.type = 10;
		this.setType(0);
		this.setTabPlayerMenu();
		this.charMenu = c;
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0007068E File Offset: 0x0006E88E
	public void setTypeFriend()
	{
		this.type = 11;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabFriend();
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x000706C1 File Offset: 0x0006E8C1
	public void setTypeEnemy()
	{
		this.type = 16;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabEnemy();
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x000706F4 File Offset: 0x0006E8F4
	public void setTypeTop(sbyte t)
	{
		this.type = 15;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabTop();
		this.isThachDau = (t != 0);
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00070734 File Offset: 0x0006E934
	public void setTabTop()
	{
		this.currentListLength = this.vTop.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00070820 File Offset: 0x0006EA20
	public void setTabFriend()
	{
		this.currentListLength = this.vFriend.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0007090C File Offset: 0x0006EB0C
	public void setTabEnemy()
	{
		this.currentListLength = this.vEnemy.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x000709F5 File Offset: 0x0006EBF5
	public void setTypeMessage()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x000709F5 File Offset: 0x0006EBF5
	public void setTypeLockInventory()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00070A15 File Offset: 0x0006EC15
	public void setTypeShop(int typeShop)
	{
		this.type = 1;
		this.setType(0);
		this.setTabShop();
		this.currentTabIndex = 0;
		this.typeShop = typeShop;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00070A3C File Offset: 0x0006EC3C
	public void setTypeBox()
	{
		this.type = 2;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt
			};
		}
		else
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt,
				mResources.inventory
			};
		}
		this.tabName[2] = Panel.boxTabName;
		this.setType(0);
		bool flag2 = this.currentTabIndex == 0;
		if (flag2)
		{
			this.setTabBox();
		}
		bool flag3 = this.currentTabIndex == 1;
		if (flag3)
		{
			this.setTabInventory(true);
		}
		bool flag4 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag4)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.tabName[7] = new string[][]
			{
				new string[]
				{
					string.Empty
				}
			};
			GameCanvas.panel2.setTypeBodyOnly();
			GameCanvas.panel2.show();
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00070B30 File Offset: 0x0006ED30
	public void setTypeCombine()
	{
		this.type = 12;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine
			};
		}
		else
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine,
				mResources.inventory
			};
		}
		this.tabName[this.type] = Panel.boxCombine;
		this.setType(0);
		bool flag2 = this.currentTabIndex == 0;
		if (flag2)
		{
			this.setTabCombine();
		}
		bool flag3 = this.currentTabIndex == 1;
		if (flag3)
		{
			this.setTabInventory(true);
		}
		bool flag4 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag4)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.tabName[7] = new string[][]
			{
				new string[]
				{
					string.Empty
				}
			};
			GameCanvas.panel2.setTypeBodyOnly();
			GameCanvas.panel2.show();
		}
		this.combineSuccess = -1;
		this.isDoneCombine = true;
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00070C38 File Offset: 0x0006EE38
	public void setTabCombine()
	{
		this.currentListLength = this.vItemCombine.size() + 1;
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00070D08 File Offset: 0x0006EF08
	public void setTypeAuto()
	{
		this.type = 22;
		this.setType(0);
		this.setTabAuto();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00070D40 File Offset: 0x0006EF40
	private void setTabAuto()
	{
		this.currentListLength = Panel.strAuto.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00070E08 File Offset: 0x0006F008
	public void setTypePetMain()
	{
		this.type = 21;
		bool flag = GameCanvas.panel2 != null;
		if (flag)
		{
			Panel.boxPet = mResources.petMainTab2;
		}
		else
		{
			Panel.boxPet = mResources.petMainTab;
		}
		this.tabName[21] = Panel.boxPet;
		bool flag2 = global::Char.myCharz().cgender == 1;
		if (flag2)
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion,
				mResources.fusionForever
			};
		}
		else
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion
			};
		}
		this.setType(2);
		bool flag3 = this.currentTabIndex == 0;
		if (flag3)
		{
			this.setTabPetInventory();
		}
		bool flag4 = this.currentTabIndex == 1;
		if (flag4)
		{
			this.setTabPetStatus();
		}
		bool flag5 = this.currentTabIndex == 2;
		if (flag5)
		{
			this.setTabInventory(true);
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00070F28 File Offset: 0x0006F128
	public void setTypeMain()
	{
		this.type = 0;
		this.setType(0);
		bool flag = this.currentTabIndex == 1;
		if (flag)
		{
			this.setTabInventory(true);
		}
		bool flag2 = this.currentTabIndex == 2;
		if (flag2)
		{
			this.setTabSkill();
		}
		bool flag3 = this.currentTabIndex == 3;
		if (flag3)
		{
			bool flag4 = this.mainTabName.Length == 4;
			if (flag4)
			{
				this.setTabTool();
			}
			else
			{
				this.setTabClans();
			}
		}
		bool flag5 = this.currentTabIndex == 4;
		if (flag5)
		{
			this.setTabTool();
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00070FBC File Offset: 0x0006F1BC
	public void setTypeZone()
	{
		this.type = 3;
		this.setType(0);
		this.setTabZone();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00070FF0 File Offset: 0x0006F1F0
	public void addItemDetail(Item item)
	{
		try
		{
			this.cp = new ChatPopup();
			string text = string.Empty;
			string text2 = string.Empty;
			bool flag3 = (int)item.template.gender != global::Char.myCharz().cgender;
			if (flag3)
			{
				bool flag4 = item.template.gender == 0;
				if (flag4)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_earth;
				}
				else
				{
					bool flag5 = item.template.gender == 1;
					if (flag5)
					{
						text2 = text2 + "\n|7|1|" + mResources.from_namec;
					}
					else
					{
						bool flag6 = item.template.gender == 2;
						if (flag6)
						{
							text2 = text2 + "\n|7|1|" + mResources.from_sayda;
						}
					}
				}
			}
			string str = string.Empty;
			bool flag7 = item.itemOption != null;
			if (flag7)
			{
				for (int i = 0; i < item.itemOption.Length; i++)
				{
					bool flag8 = item.itemOption[i].optionTemplate.id == 72;
					if (flag8)
					{
						str = " [+" + item.itemOption[i].param.ToString() + "]";
					}
				}
			}
			bool flag = false;
			bool flag9 = item.itemOption != null;
			if (flag9)
			{
				for (int j = 0; j < item.itemOption.Length; j++)
				{
					bool flag10 = item.itemOption[j].optionTemplate.id == 41;
					if (flag10)
					{
						flag = true;
						bool flag11 = item.itemOption[j].param == 1;
						if (flag11)
						{
							text2 = text2 + "|0|1|" + item.template.name + str;
						}
						bool flag12 = item.itemOption[j].param == 2;
						if (flag12)
						{
							text2 = text2 + "|2|1|" + item.template.name + str;
						}
						bool flag13 = item.itemOption[j].param == 3;
						if (flag13)
						{
							text2 = text2 + "|8|1|" + item.template.name + str;
						}
						bool flag14 = item.itemOption[j].param == 4;
						if (flag14)
						{
							text2 = text2 + "|7|1|" + item.template.name + str;
						}
					}
				}
			}
			bool flag15 = !flag;
			if (flag15)
			{
				text2 = text2 + "|0|1|" + item.template.name + str;
			}
			bool flag16 = item.itemOption != null;
			if (flag16)
			{
				for (int k = 0; k < item.itemOption.Length; k++)
				{
					bool flag2 = item.itemOption[k].optionTemplate.name.StartsWith("$");
					bool flag17 = flag2;
					if (flag17)
					{
						text = item.itemOption[k].getOptiongColor();
						bool flag18 = item.itemOption[k].param == 1;
						if (flag18)
						{
							text2 = text2 + "\n|1|1|" + text;
						}
						bool flag19 = item.itemOption[k].param == 0;
						if (flag19)
						{
							text2 = text2 + "\n|0|1|" + text;
						}
					}
					else
					{
						text = item.itemOption[k].getOptionString();
						bool flag20 = !text.Equals(string.Empty);
						if (flag20)
						{
							bool flag21 = item.itemOption[k].optionTemplate.id != 72;
							if (flag21)
							{
								bool flag22 = item.itemOption[k].optionTemplate.id == 102;
								if (flag22)
								{
									this.cp.starSlot = (sbyte)item.itemOption[k].param;
									Res.outz("STAR SLOT= " + this.cp.starSlot.ToString());
								}
								else
								{
									bool flag23 = item.itemOption[k].optionTemplate.id == 107;
									if (flag23)
									{
										this.cp.maxStarSlot = (sbyte)item.itemOption[k].param;
										Res.outz("STAR SLOT= " + this.cp.maxStarSlot.ToString());
									}
									else
									{
										text2 = text2 + "\n|1|1|" + text;
									}
								}
							}
						}
					}
				}
			}
			bool flag24 = this.currItem.template.strRequire > 1;
			if (flag24)
			{
				string str2 = mResources.pow_request + ": " + this.currItem.template.strRequire.ToString();
				bool flag25 = (long)this.currItem.template.strRequire > global::Char.myCharz().cPower;
				if (flag25)
				{
					text2 = text2 + "\n|3|1|" + str2;
					string text3 = text2;
					text2 = string.Concat(new object[]
					{
						text3,
						"\n|3|1|",
						mResources.your_pow,
						": ",
						global::Char.myCharz().cPower
					});
				}
				else
				{
					text2 = text2 + "\n|6|1|" + str2;
				}
			}
			else
			{
				text2 += "\n|6|1|";
			}
			this.currItem.compare = this.getCompare(this.currItem);
			text2 += "\n--";
			text2 = text2 + "\n|6|" + item.template.description;
			bool flag26 = !item.reason.Equals(string.Empty);
			if (flag26)
			{
				bool flag27 = !item.template.description.Equals(string.Empty);
				if (flag27)
				{
					text2 += "\n--";
				}
				text2 = text2 + "\n|2|" + item.reason;
			}
			bool flag28 = this.cp.maxStarSlot > 0;
			if (flag28)
			{
				text2 += "\n\n";
			}
			this.popUpDetailInit(this.cp, text2);
			this.idIcon = (int)item.template.iconID;
			this.partID = null;
			this.charInfo = null;
		}
		catch (Exception ex)
		{
			Res.outz("ex " + ex.StackTrace);
		}
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0007163C File Offset: 0x0006F83C
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.isClip = false;
		cp.sayWidth = 180;
		cp.cx = 3 + this.X - ((this.X != 0) ? (Res.abs(cp.sayWidth - this.W) + 8) : 0);
		cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		bool flag = cp.ch > GameCanvas.h - 80;
		if (flag)
		{
			cp.ch = GameCanvas.h - 80;
			cp.lim = cp.says.Length * 12 - cp.ch + 17;
			bool flag2 = cp.lim < 0;
			if (flag2)
			{
				cp.lim = 0;
			}
			ChatPopup.cmyText = 0;
			cp.isClip = true;
		}
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		while (cp.cy < 10)
		{
			cp.cy++;
			GameCanvas.menu.menuY++;
		}
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00071798 File Offset: 0x0006F998
	public void popUpDetailInitArray(ChatPopup cp, string[] chat)
	{
		cp.sayWidth = 160;
		cp.cx = 3 + this.X;
		cp.says = chat;
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00071824 File Offset: 0x0006FA24
	public void addMessageDetail(ClanMessage cm)
	{
		this.cp = new ChatPopup();
		string text = "|0|" + cm.playerName;
		text = text + "\n|1|" + Member.getRole((int)cm.role);
		for (int i = 0; i < this.myMember.size(); i++)
		{
			Member member = (Member)this.myMember.elementAt(i);
			bool flag = cm.playerId == member.ID;
			if (flag)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuledonate,
					": ",
					member.clanPoint
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuleself,
					": ",
					member.curClanPoint
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|4|",
					mResources.give_pea,
					": ",
					member.donate,
					mResources.time
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|4|",
					mResources.receive_pea,
					": ",
					member.receive_donate,
					mResources.time
				});
				this.partID = new int[]
				{
					(int)member.head,
					(int)member.leg,
					(int)member.body
				};
				break;
			}
		}
		text += "\n--";
		for (int j = 0; j < cm.chat.Length; j++)
		{
			text = text + "\n" + cm.chat[j];
		}
		bool flag2 = cm.type == 1;
		if (flag2)
		{
			string text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				"\n|6|",
				mResources.received,
				" ",
				cm.recieve,
				"/",
				cm.maxCap
			});
		}
		this.popUpDetailInit(this.cp, text);
		this.charInfo = null;
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00071A88 File Offset: 0x0006FC88
	public void addThachDauDetail(TopInfo t)
	{
		string text = "|0|1|" + t.name;
		text = text + "\n|1|Top " + t.rank.ToString();
		text = text + "\n|1|" + t.info;
		text = text + "\n|2|" + t.info2;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			t.headID,
			(int)t.leg,
			(int)t.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00071B30 File Offset: 0x0006FD30
	public void addClanMemberDetail(Member m)
	{
		string text = "|0|1|" + m.name;
		string str = "\n|2|1|";
		bool flag = m.role == 0;
		if (flag)
		{
			str = "\n|7|1|";
		}
		bool flag2 = m.role == 1;
		if (flag2)
		{
			str = "\n|1|1|";
		}
		bool flag3 = m.role == 2;
		if (flag3)
		{
			str = "\n|0|1|";
		}
		text = text + str + Member.getRole((int)m.role);
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|2|1|",
			mResources.power,
			": ",
			m.powerPoint
		});
		text += "\n--";
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|5|",
			mResources.clan_capsuledonate,
			": ",
			m.clanPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|5|",
			mResources.clan_capsuleself,
			": ",
			m.curClanPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.give_pea,
			": ",
			m.donate,
			mResources.time
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.receive_pea,
			": ",
			m.receive_donate,
			mResources.time
		});
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|6|",
			mResources.join_date,
			": ",
			m.joinTime
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			(int)m.head,
			(int)m.leg,
			(int)m.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00071D5C File Offset: 0x0006FF5C
	public void addClanDetail(Clan cl)
	{
		try
		{
			string text = "|0|" + cl.name;
			string[] array = mFont.tahoma_7_green.splitFontArray(cl.slogan, this.wScroll - 60);
			for (int i = 0; i < array.Length; i++)
			{
				text = text + "\n|2|" + array[i];
			}
			text += "\n--";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|7|",
				mResources.clan_leader,
				": ",
				cl.leaderName
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|1|",
				mResources.power_point,
				": ",
				cl.powerPoint
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|4|",
				mResources.member,
				": ",
				cl.currMember,
				"/",
				cl.maxMember
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|4|",
				mResources.level,
				": ",
				cl.level
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|4|",
				mResources.clan_birthday,
				": ",
				NinjaUtil.getDate(cl.date)
			});
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.idIcon = (int)ClanImage.getClanImage((short)cl.imgID).idImage[0];
			this.currItem = null;
		}
		catch (Exception ex)
		{
			Res.outz("Throw  exception " + ex.StackTrace);
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00071F60 File Offset: 0x00070160
	public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
	{
		string text = "|0|" + tp.name;
		for (int i = 0; i < tp.description.Length; i++)
		{
			text = text + "\n|4|" + tp.description[i];
		}
		text += "\n--";
		bool flag = skill != null;
		if (flag)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|2|",
				mResources.cap_do,
				": ",
				skill.point
			});
			text = text + "\n|5|" + NinjaUtil.replace(tp.damInfo, "#", skill.damage.ToString() + string.Empty);
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|5|",
				mResources.KI_consume,
				skill.manaUse,
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|5|",
				mResources.cooldown,
				": ",
				skill.strTimeReplay(),
				"s"
			});
			text += "\n--";
			bool flag2 = skill.point == tp.maxPoint;
			if (flag2)
			{
				text = text + "\n|0|" + mResources.max_level_reach;
			}
			else
			{
				bool flag3 = !skill.template.isSkillSpec();
				if (flag3)
				{
					text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|1|",
						mResources.next_level_require,
						Res.formatNumber(nextSkill.powRequire),
						" ",
						mResources.potential
					});
				}
				text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage.ToString() + string.Empty);
			}
		}
		else
		{
			text = text + "\n|2|" + mResources.not_learn;
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|1|",
				mResources.learn_require,
				Res.formatNumber(nextSkill.powRequire),
				" ",
				mResources.potential
			});
			text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage.ToString() + string.Empty);
			text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				"\n|4|",
				mResources.KI_consume,
				nextSkill.manaUse,
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|4|",
				mResources.cooldown,
				": ",
				nextSkill.strTimeReplay(),
				"s"
			});
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.idIcon = 0;
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x000722C4 File Offset: 0x000704C4
	public void show()
	{
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.cmdClose.x = 156;
			this.cmdClose.y = 3;
		}
		else
		{
			this.cmdClose.x = GameCanvas.w - 19;
			this.cmdClose.y = GameCanvas.h - 19;
		}
		this.cmdClose.isPlaySoundButton = false;
		ChatPopup.currChatPopup = null;
		InfoDlg.hide();
		this.timeShow = 20;
		this.isShow = true;
		this.isClose = false;
		SoundMn.gI().panelOpen();
		bool flag = this.isTypeShop();
		if (flag)
		{
			global::Char.myCharz().setPartOld();
		}
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00072374 File Offset: 0x00070574
	public void chatTFUpdateKey()
	{
		bool flag = this.chatTField != null && this.chatTField.isShow;
		if (flag)
		{
			bool flag2 = this.chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.chatTField.left)) && this.chatTField.left != null;
			if (flag2)
			{
				this.chatTField.left.performAction();
			}
			bool flag3 = this.chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.chatTField.right)) && this.chatTField.right != null;
			if (flag3)
			{
				this.chatTField.right.performAction();
			}
			bool flag4 = this.chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.chatTField.center)) && this.chatTField.center != null;
			if (flag4)
			{
				this.chatTField.center.performAction();
			}
			bool flag5 = this.chatTField.isShow && GameCanvas.keyAsciiPress != 0;
			if (flag5)
			{
				this.chatTField.keyPressed(GameCanvas.keyAsciiPress);
				GameCanvas.keyAsciiPress = 0;
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x000724E4 File Offset: 0x000706E4
	public void updateKey()
	{
		bool flag = this.chatTField != null && this.chatTField.isShow;
		if (!flag)
		{
			bool flag2 = !GameCanvas.panel.isDoneCombine;
			if (!flag2)
			{
				bool flag3 = InfoDlg.isShow;
				if (!flag3)
				{
					bool flag4 = this.tabIcon != null && this.tabIcon.isShow;
					if (flag4)
					{
						this.tabIcon.updateKey();
					}
					else
					{
						bool flag5 = this.isClose;
						if (!flag5)
						{
							bool flag6 = !this.isShow;
							if (!flag6)
							{
								bool flag7 = this.cmdClose.isPointerPressInside();
								if (flag7)
								{
									this.cmdClose.performAction();
								}
								else
								{
									bool flag8 = GameCanvas.keyPressed[13];
									if (flag8)
									{
										bool flag9 = this.type != 4;
										if (flag9)
										{
											this.hide();
											return;
										}
										this.setTypeMain();
										this.cmx = (this.cmtoX = 0);
									}
									bool flag10 = GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
									if (flag10)
									{
										bool flag11 = this.left.idAction > 0;
										if (flag11)
										{
											this.perform(this.left.idAction, this.left.p);
										}
										else
										{
											this.waitToPerform = 2;
										}
									}
									bool flag12 = this.Equals(GameCanvas.panel) && GameCanvas.panel2 == null && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.X, this.Y, this.W, this.H) && !this.pointerIsDowning;
									if (flag12)
									{
										this.hide();
									}
									else
									{
										bool flag13 = !this.isClanOption;
										if (flag13)
										{
											this.updateKeyInTabBar();
										}
										switch (this.type)
										{
										case 0:
										{
											bool flag14 = this.currentTabIndex == 0;
											if (flag14)
											{
												this.updateKeyQuest();
												GameCanvas.clearKeyPressed();
												return;
											}
											bool flag15 = this.currentTabIndex == 1;
											if (flag15)
											{
												this.updateKeyInventory();
											}
											bool flag16 = this.currentTabIndex == 2;
											if (flag16)
											{
												this.updateKeySkill();
											}
											bool flag17 = this.currentTabIndex == 3;
											if (flag17)
											{
												bool flag18 = this.mainTabName.Length == 4;
												if (flag18)
												{
													this.updateKeyTool();
												}
												else
												{
													this.updateKeyClans();
												}
											}
											bool flag19 = this.currentTabIndex == 4;
											if (flag19)
											{
												this.updateKeyTool();
											}
											break;
										}
										case 1:
										case 17:
										case 25:
										{
											bool flag20 = this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17;
											if (flag20)
											{
												this.updateKeyScrollView();
											}
											else
											{
												bool flag21 = this.typeShop == 0;
												if (flag21)
												{
													this.updateKeyInventory();
												}
												else
												{
													this.updateKeyScrollView();
												}
											}
											break;
										}
										case 2:
											this.updateKeyInventory();
											break;
										case 3:
											this.updateKeyScrollView();
											break;
										case 4:
											this.updateKeyMap();
											GameCanvas.clearKeyPressed();
											return;
										case 7:
											this.updateKeyInventory();
											break;
										case 8:
											this.updateKeyScrollView();
											break;
										case 9:
											this.updateKeyScrollView();
											break;
										case 10:
											this.updateKeyScrollView();
											break;
										case 11:
										case 16:
											this.updateKeyScrollView();
											break;
										case 12:
											this.updateKeyCombine();
											break;
										case 13:
											this.updateKeyGiaoDich();
											break;
										case 14:
											this.updateKeyScrollView();
											break;
										case 15:
											this.updateKeyScrollView();
											break;
										case 18:
											this.updateKeyScrollView();
											break;
										case 19:
											this.updateKeyOption();
											break;
										case 20:
											this.updateKeyOption();
											break;
										case 21:
										{
											bool flag22 = this.currentTabIndex == 0;
											if (flag22)
											{
												this.updateKeyScrollView();
											}
											bool flag23 = this.currentTabIndex == 1;
											if (flag23)
											{
												this.updateKeyPetStatus();
											}
											bool flag24 = this.currentTabIndex == 2;
											if (flag24)
											{
												this.updateKeyScrollView();
											}
											break;
										}
										case 22:
											this.updateKeyAuto();
											break;
										case 23:
										case 24:
											this.updateKeyScrollView();
											break;
										}
										GameCanvas.clearKeyHold();
										for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
										{
											GameCanvas.keyPressed[i] = false;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00003136 File Offset: 0x00001336
	private void updateKeyAuto()
	{
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0007298D File Offset: 0x00070B8D
	private void updateKeyPetStatus()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00003136 File Offset: 0x00001336
	private void updateKeyPetSkill()
	{
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0007298D File Offset: 0x00070B8D
	private void keyGiaodich()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00072998 File Offset: 0x00070B98
	private void updateKeyGiaoDich()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			bool flag2 = this.Equals(GameCanvas.panel);
			if (flag2)
			{
				this.updateKeyInventory();
			}
			bool flag3 = this.Equals(GameCanvas.panel2);
			if (flag3)
			{
				this.keyGiaodich();
			}
		}
		bool flag4 = this.currentTabIndex == 1 || this.currentTabIndex == 2;
		if (flag4)
		{
			this.keyGiaodich();
		}
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0007298D File Offset: 0x00070B8D
	private void updateKeyTool()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0007298D File Offset: 0x00070B8D
	private void updateKeySkill()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0007298D File Offset: 0x00070B8D
	private void updateKeyClanIcon()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00072A08 File Offset: 0x00070C08
	public void setTabGiaoDich(bool isMe)
	{
		this.currentListLength = ((!isMe) ? (this.vFriendGD.size() + 3) : (this.vMyGD.size() + 3));
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00072AE8 File Offset: 0x00070CE8
	public void setTypeGiaoDich(global::Char cGD)
	{
		this.type = 13;
		this.tabName[this.type] = Panel.boxGD;
		this.isAccept = false;
		this.isLock = false;
		this.isFriendLock = false;
		this.vMyGD.removeAllElements();
		this.vFriendGD.removeAllElements();
		this.moneyGD = 0;
		this.friendMoneyGD = 0;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.type = 13;
			GameCanvas.panel2.tabName[this.type] = new string[][]
			{
				mResources.item_receive
			};
			GameCanvas.panel2.setType(1);
			GameCanvas.panel2.setTabGiaoDich(false);
			GameCanvas.panel.tabName[this.type] = new string[][]
			{
				mResources.inventory,
				mResources.item_give
			};
			GameCanvas.panel2.show();
			GameCanvas.panel2.charMenu = cGD;
		}
		bool flag2 = this.Equals(GameCanvas.panel);
		if (flag2)
		{
			this.setType(0);
		}
		bool flag3 = this.currentTabIndex == 0;
		if (flag3)
		{
			this.setTabInventory(true);
		}
		bool flag4 = this.currentTabIndex == 1;
		if (flag4)
		{
			this.setTabGiaoDich(true);
		}
		bool flag5 = this.currentTabIndex == 2;
		if (flag5)
		{
			this.setTabGiaoDich(false);
		}
		this.charMenu = cGD;
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00072C54 File Offset: 0x00070E54
	private void paintGiaoDich(mGraphics g, bool isMe)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		MyVector myVector = (!isMe) ? this.vFriendGD : this.vMyGD;
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					bool flag3 = i == this.currentListLength - 1;
					if (flag3)
					{
						if (isMe)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4);
							bool flag4 = !this.isLock;
							if (flag4)
							{
								bool flag5 = !this.isFriendLock;
								if (flag5)
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
								else
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
							}
							else
							{
								bool flag6 = this.isFriendLock;
								if (flag6)
								{
									g.setColor(15196114);
									g.fillRect(num5, num2, this.wScroll, num4);
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.done, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
								else
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
							}
						}
					}
					else
					{
						bool flag7 = i == this.currentListLength - 2;
						if (flag7)
						{
							if (isMe)
							{
								g.setColor(15196114);
								g.fillRect(num5, num2, this.wScroll, num4);
								bool flag8 = !this.isAccept;
								if (flag8)
								{
									bool flag9 = !this.isLock;
									if (flag9)
									{
										g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
										((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.mlock, this.xScroll + this.wScroll - 22, num2 + 7, 2);
										mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.not_lock_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
									}
									else
									{
										g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
										((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.CANCEL, this.xScroll + this.wScroll - 22, num2 + 7, 2);
										mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
									}
								}
							}
							else
							{
								bool flag10 = !this.isFriendLock;
								if (flag10)
								{
									mFont.tahoma_7b_dark.drawString(g, mResources.not_lock_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
								else
								{
									mFont.tahoma_7b_dark.drawString(g, mResources.locked_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
							}
						}
						else
						{
							bool flag11 = i == this.currentListLength - 3;
							if (flag11)
							{
								bool flag12 = this.isLock;
								if (flag12)
								{
									g.setColor(13748667);
								}
								else
								{
									g.setColor((i != this.selected) ? 15196114 : 16383818);
								}
								g.fillRect(num, num2, num3, num4);
								bool flag13 = this.isLock;
								if (flag13)
								{
									g.setColor(13748667);
								}
								else
								{
									g.setColor((i != this.selected) ? 9993045 : 7300181);
								}
								g.fillRect(num5, num6, num7, num8);
								g.drawImage(Panel.imgXu, num5 + num7 / 2, num6 + num8 / 2, 3);
								mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys((long)((!isMe) ? this.friendMoneyGD : this.moneyGD)) + " " + mResources.XU, num + 5, num2 + 11, 0);
								mFont.tahoma_7_green.drawString(g, mResources.money_trade, num + 5, num2, 0);
							}
							else
							{
								bool flag14 = myVector.size() == 0;
								if (flag14)
								{
									return;
								}
								bool flag15 = this.isLock;
								if (flag15)
								{
									g.setColor(13748667);
								}
								else
								{
									g.setColor((i != this.selected) ? 15196114 : 16383818);
								}
								g.fillRect(num, num2, num3, num4);
								bool flag16 = this.isLock;
								if (flag16)
								{
									g.setColor(13748667);
								}
								else
								{
									g.setColor((i != this.selected) ? 9993045 : 9541120);
								}
								Item item = (Item)myVector.elementAt(i);
								bool flag17 = item != null;
								if (flag17)
								{
									for (int j = 0; j < item.itemOption.Length; j++)
									{
										bool flag18 = item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0;
										if (flag18)
										{
											sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
											int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
											bool flag19 = color_ItemBg != -1;
											if (flag19)
											{
												bool flag20 = this.isLock;
												if (flag20)
												{
													g.setColor(13748667);
												}
												else
												{
													g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
												}
											}
										}
									}
								}
								g.fillRect(num5, num6, num7, num8);
								bool flag21 = item != null;
								if (flag21)
								{
									string str = string.Empty;
									mFont mFont = mFont.tahoma_7_green2;
									bool flag22 = item.itemOption != null;
									if (flag22)
									{
										for (int k = 0; k < item.itemOption.Length; k++)
										{
											bool flag23 = item.itemOption[k].optionTemplate.id == 72;
											if (flag23)
											{
												str = " [+" + item.itemOption[k].param.ToString() + "]";
											}
											bool flag24 = item.itemOption[k].optionTemplate.id == 41;
											if (flag24)
											{
												bool flag25 = item.itemOption[k].param == 1;
												if (flag25)
												{
													mFont = Panel.GetFont(0);
												}
												else
												{
													bool flag26 = item.itemOption[k].param == 2;
													if (flag26)
													{
														mFont = Panel.GetFont(2);
													}
													else
													{
														bool flag27 = item.itemOption[k].param == 3;
														if (flag27)
														{
															mFont = Panel.GetFont(8);
														}
														else
														{
															bool flag28 = item.itemOption[k].param == 4;
															if (flag28)
															{
																mFont = Panel.GetFont(7);
															}
														}
													}
												}
											}
										}
									}
									mFont.drawString(g, item.template.name + str, num + 5, num2 + 1, 0);
									string text = string.Empty;
									bool flag29 = item.itemOption != null;
									if (flag29)
									{
										bool flag30 = item.itemOption.Length != 0 && item.itemOption[0] != null;
										if (flag30)
										{
											text += item.itemOption[0].getOptionString();
										}
										mFont mFont2 = mFont.tahoma_7_blue;
										bool flag31 = item.compare < 0 && item.template.type != 5;
										if (flag31)
										{
											mFont2 = mFont.tahoma_7_red;
										}
										bool flag32 = item.itemOption.Length > 1;
										if (flag32)
										{
											for (int l = 1; l < item.itemOption.Length; l++)
											{
												bool flag33 = item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107;
												if (flag33)
												{
													text = text + "," + item.itemOption[l].getOptionString();
												}
											}
										}
										mFont2.drawString(g, text, num + 5, num2 + 11, mFont.LEFT);
									}
									SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
									bool flag34 = item.itemOption != null;
									if (flag34)
									{
										for (int m = 0; m < item.itemOption.Length; m++)
										{
											this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8);
										}
										for (int n = 0; n < item.itemOption.Length; n++)
										{
											this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
										}
									}
									bool flag35 = item.quantity > 1;
									if (flag35)
									{
										mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
									}
								}
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x000737EC File Offset: 0x000719EC
	private void updateKeyMap()
	{
		bool flag = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			this.yMove -= 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			bool flag2 = this.yMove < this.yScroll;
			if (flag2)
			{
				this.yMove = this.yScroll;
			}
		}
		bool flag3 = GameCanvas.keyHold[(!Main.isPC) ? 8 : 22];
		if (flag3)
		{
			this.yMove += 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			bool flag4 = this.yMove > this.yScroll + 200;
			if (flag4)
			{
				this.yMove = this.yScroll + 200;
			}
		}
		bool flag5 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
		if (flag5)
		{
			this.xMove -= 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			bool flag6 = this.xMove < 16;
			if (flag6)
			{
				this.xMove = 16;
			}
		}
		bool flag7 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
		if (flag7)
		{
			this.xMove += 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			bool flag8 = this.xMove > 250;
			if (flag8)
			{
				this.xMove = 250;
			}
		}
		bool isPointerDown = GameCanvas.isPointerDown;
		if (isPointerDown)
		{
			this.pointerIsDowning = true;
			bool flag9 = !this.trans;
			if (flag9)
			{
				this.pa1 = this.cmxMap;
				this.pa2 = this.cmyMap;
				this.trans = true;
			}
			this.cmxMap = this.pa1 + (GameCanvas.pxLast - GameCanvas.px);
			this.cmyMap = this.pa2 + (GameCanvas.pyLast - GameCanvas.py);
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			this.trans = false;
			GameCanvas.pxLast = GameCanvas.px;
			GameCanvas.pyLast = GameCanvas.py;
			this.pX = GameCanvas.pxLast + this.cmxMap;
			this.pY = GameCanvas.pyLast + this.cmyMap;
		}
		bool isPointerClick = GameCanvas.isPointerClick;
		if (isPointerClick)
		{
			this.pointerIsDowning = false;
		}
		bool flag10 = this.cmxMap < 0;
		if (flag10)
		{
			this.cmxMap = 0;
		}
		bool flag11 = this.cmxMap > this.cmxMapLim;
		if (flag11)
		{
			this.cmxMap = this.cmxMapLim;
		}
		bool flag12 = this.cmyMap < 0;
		if (flag12)
		{
			this.cmyMap = 0;
		}
		bool flag13 = this.cmyMap > this.cmyMapLim;
		if (flag13)
		{
			this.cmyMap = this.cmyMapLim;
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00073AC8 File Offset: 0x00071CC8
	private void updateKeyCombine()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			this.updateKeyScrollView();
			this.keyTouchCombine = -1;
			bool flag2 = this.selected == this.vItemCombine.size() && GameCanvas.isPointerClick;
			if (flag2)
			{
				GameCanvas.isPointerClick = false;
				this.keyTouchCombine = 1;
			}
		}
		bool flag3 = this.currentTabIndex == 1;
		if (flag3)
		{
			this.updateKeyScrollView();
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00073B38 File Offset: 0x00071D38
	private void updateKeyQuest()
	{
		bool flag = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			this.cmyQuest -= 5;
		}
		bool flag2 = GameCanvas.keyHold[(!Main.isPC) ? 8 : 22];
		if (flag2)
		{
			this.cmyQuest += 5;
		}
		bool flag3 = this.cmyQuest < 0;
		if (flag3)
		{
			this.cmyQuest = 0;
		}
		int num = this.indexRowMax * 12 - (this.hScroll - 60);
		bool flag4 = num < 0;
		if (flag4)
		{
			num = 0;
		}
		bool flag5 = this.cmyQuest > num;
		if (flag5)
		{
			this.cmyQuest = num;
		}
		bool flag6 = this.scroll != null;
		if (flag6)
		{
			bool flag7 = !GameCanvas.isTouch;
			if (flag7)
			{
				this.scroll.cmy = this.cmyQuest;
			}
			this.scroll.updateKey();
		}
		int num2 = this.xScroll + this.wScroll / 2 - 35;
		int num3 = (GameCanvas.h <= 300) ? 15 : 20;
		int num4 = this.yScroll + this.hScroll - num3 - 15;
		int px = GameCanvas.px;
		int py = GameCanvas.py;
		this.keyTouchMapButton = -1;
		bool flag8 = Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && px >= num2 && px <= num2 + 70 && py >= num4 && py <= num4 + 30;
		if (flag8)
		{
			bool flag9 = this.scroll != null && this.scroll.pointerIsDowning;
			if (!flag9)
			{
				this.keyTouchMapButton = 1;
				bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
				if (isPointerJustRelease)
				{
					SoundMn.gI().buttonClick();
					this.waitToPerform = 2;
					GameCanvas.clearAllPointerEvent();
				}
			}
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00073CFC File Offset: 0x00071EFC
	private void getCurrClanOtion()
	{
		this.isClanOption = false;
		bool flag = this.type == 0 && this.mainTabName.Length == 5 && this.currentTabIndex == 3;
		if (flag)
		{
			this.isClanOption = false;
			bool flag2 = this.selected == 0;
			if (flag2)
			{
				this.currClanOption = new int[this.clansOption.Length];
				for (int i = 0; i < this.currClanOption.Length; i++)
				{
					this.currClanOption[i] = i;
				}
				bool flag3 = !this.isViewMember;
				if (flag3)
				{
					this.isClanOption = true;
				}
			}
			else
			{
				bool flag4 = this.selected != 1;
				if (flag4)
				{
					bool flag5 = this.isSearchClan;
					if (!flag5)
					{
						bool flag6 = this.selected > 0;
						if (flag6)
						{
							this.currClanOption = new int[1];
							for (int j = 0; j < this.currClanOption.Length; j++)
							{
								this.currClanOption[j] = j;
							}
							this.isClanOption = true;
						}
					}
				}
			}
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00073E10 File Offset: 0x00072010
	private void updateKeyClansOption()
	{
		bool flag = this.currClanOption == null;
		if (!flag)
		{
			bool flag2 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag2)
			{
				this.currMess = this.getCurrMessage();
				this.cSelected--;
				bool flag3 = this.selected == 0 && this.cSelected < 0;
				if (flag3)
				{
					this.cSelected = this.currClanOption.Length - 1;
				}
				bool flag4 = this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected < 0;
				if (flag4)
				{
					this.cSelected = this.currMess.option.Length - 1;
				}
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
				if (flag5)
				{
					this.currMess = this.getCurrMessage();
					this.cSelected++;
					bool flag6 = this.selected == 0 && this.cSelected > this.currClanOption.Length - 1;
					if (flag6)
					{
						this.cSelected = 0;
					}
					bool flag7 = this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected > this.currMess.option.Length - 1;
					if (flag7)
					{
						this.cSelected = 0;
					}
				}
			}
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00073F84 File Offset: 0x00072184
	private void updateKeyClans()
	{
		this.updateKeyScrollView();
		this.updateKeyClansOption();
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00073F98 File Offset: 0x00072198
	private void checkOptionSelect()
	{
		try
		{
			bool flag = this.type == 0 && this.currentTabIndex == 3 && this.mainTabName.Length == 5;
			if (flag)
			{
				bool flag2 = this.selected != -1;
				if (flag2)
				{
					int num = 0;
					bool flag3 = this.selected == 0;
					if (flag3)
					{
						num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
						this.cSelected = (GameCanvas.px - num) / this.TAB_W;
					}
					else
					{
						this.currMess = this.getCurrMessage();
						bool flag4 = this.currMess != null && this.currMess.option != null;
						if (flag4)
						{
							num = this.xScroll + this.wScroll - 2 - this.currMess.option.Length * 40;
							this.cSelected = (GameCanvas.px - num) / 40;
						}
					}
					bool flag5 = GameCanvas.px < num;
					if (flag5)
					{
						this.cSelected = -1;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Res.outz("Throw err " + ex.StackTrace);
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x000740E0 File Offset: 0x000722E0
	public void updateScroolMouse(int a)
	{
		bool flag = false;
		bool flag2 = GameCanvas.pxMouse > this.wScroll;
		if (!flag2)
		{
			bool flag3 = this.indexMouse == -1;
			if (flag3)
			{
				this.indexMouse = this.selected;
			}
			bool flag4 = a > 0;
			if (flag4)
			{
				this.indexMouse -= a;
				flag = true;
			}
			else
			{
				bool flag5 = a < 0;
				if (flag5)
				{
					this.indexMouse += -a;
					flag = true;
				}
			}
			bool flag6 = this.indexMouse < 0;
			if (flag6)
			{
				this.indexMouse = 0;
			}
			bool flag7 = !flag;
			if (!flag7)
			{
				this.cmtoY = this.indexMouse * 12;
				bool flag8 = this.cmtoY > this.cmyLim;
				if (flag8)
				{
					this.cmtoY = this.cmyLim;
				}
				bool flag9 = this.cmtoY < 0;
				if (flag9)
				{
					this.cmtoY = 0;
				}
			}
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x000741CC File Offset: 0x000723CC
	private void updateKeyScrollView()
	{
		bool flag2 = this.currentListLength <= 0;
		if (!flag2)
		{
			bool flag = false;
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag3)
			{
				flag = true;
				bool flag4 = this.isTabInven() && this.isnewInventory;
				if (flag4)
				{
					bool flag5 = this.selected > 0 && this.sellectInventory == 0;
					if (flag5)
					{
						this.selected--;
					}
				}
				else
				{
					this.selected--;
					bool flag6 = this.type == 24;
					if (flag6)
					{
						this.selected -= 2;
						bool flag7 = this.selected < 0;
						if (flag7)
						{
							this.selected = 0;
						}
					}
					else
					{
						bool flag8 = this.selected < 0;
						if (flag8)
						{
							bool flag9 = this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1;
							if (flag9)
							{
								InfoDlg.showWait();
								bool flag10 = this.currPageShop[this.currentTabIndex] <= 0;
								if (flag10)
								{
									Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
								}
								else
								{
									Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
								}
								return;
							}
							this.selected = this.currentListLength - 1;
							bool flag11 = this.isClanOption;
							if (flag11)
							{
								this.selected = -1;
							}
							bool flag12 = this.size_tab > 0;
							if (flag12)
							{
								this.selected = -1;
							}
						}
					}
					this.lastSelect[this.currentTabIndex] = this.selected;
					this.cSelected = 0;
					this.getCurrClanOtion();
				}
			}
			else
			{
				bool flag13 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
				if (flag13)
				{
					flag = true;
					bool flag14 = this.isTabInven() && this.isnewInventory;
					if (flag14)
					{
						bool flag15 = this.selected < 1 && this.sellectInventory == 0;
						if (flag15)
						{
							this.selected++;
						}
					}
					else
					{
						this.selected++;
						bool flag16 = this.type == 24;
						if (flag16)
						{
							this.selected += 2;
							bool flag17 = this.selected > this.currentListLength - 1;
							if (flag17)
							{
								this.selected = this.currentListLength - 1;
							}
						}
						else
						{
							bool flag18 = this.selected > this.currentListLength - 1;
							if (flag18)
							{
								bool flag19 = this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1;
								if (flag19)
								{
									InfoDlg.showWait();
									bool flag20 = this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1;
									if (flag20)
									{
										Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
									}
									else
									{
										Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
									}
									return;
								}
								this.selected = 0;
							}
						}
						this.lastSelect[this.currentTabIndex] = this.selected;
						this.cSelected = 0;
						this.getCurrClanOtion();
					}
				}
			}
			bool flag21 = this.isnewInventory && GameCanvas.keyPressed[5] && this.itemInvenNew != null;
			if (flag21)
			{
				this.pointerDownTime = 0;
				this.waitToPerform = 2;
			}
			bool flag22 = flag;
			if (flag22)
			{
				this.cmtoY = this.selected * this.ITEM_HEIGHT - this.hScroll / 2;
				bool flag23 = this.cmtoY > this.cmyLim;
				if (flag23)
				{
					this.cmtoY = this.cmyLim;
				}
				bool flag24 = this.cmtoY < 0;
				if (flag24)
				{
					this.cmtoY = 0;
				}
				this.cmy = this.cmtoY;
			}
			bool isPointerDown = GameCanvas.isPointerDown;
			if (isPointerDown)
			{
				this.justRelease = false;
				bool flag25 = !this.pointerIsDowning && GameCanvas.isPointer(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
				if (flag25)
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.py;
					}
					this.pointerDownFirstX = GameCanvas.py;
					this.pointerIsDowning = true;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else
				{
					bool flag26 = this.pointerIsDowning;
					if (flag26)
					{
						this.pointerDownTime++;
						bool flag27 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning;
						if (flag27)
						{
							this.pointerDownFirstX = -1000;
							this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
							bool flag28 = this.selected >= this.currentListLength;
							if (flag28)
							{
								this.selected = -1;
							}
							this.checkOptionSelect();
						}
						else
						{
							this.indexMouse = -1;
						}
						int num = GameCanvas.py - this.pointerDownLastX[0];
						bool flag29 = num != 0 && this.selected != -1;
						if (flag29)
						{
							this.selected = -1;
							this.cSelected = -1;
						}
						for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
						{
							this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
						}
						this.pointerDownLastX[0] = GameCanvas.py;
						this.cmtoY -= num;
						bool flag30 = this.cmtoY < 0;
						if (flag30)
						{
							this.cmtoY = 0;
						}
						bool flag31 = this.cmtoY > this.cmyLim;
						if (flag31)
						{
							this.cmtoY = this.cmyLim;
						}
						bool flag32 = this.cmy < 0 || this.cmy > this.cmyLim;
						if (flag32)
						{
							num /= 2;
						}
						this.cmy -= num;
						bool flag33 = this.cmy < -(GameCanvas.h / 3);
						if (flag33)
						{
							this.wantUpdateList = true;
						}
						else
						{
							this.wantUpdateList = false;
						}
						bool flag34 = this.isnewInventory;
						if (flag34)
						{
							int num2 = GameCanvas.px - this.xScroll;
							int num3 = GameCanvas.py - this.yScroll;
							this.sellectInventory = num3 / 34 * 5 + num2 / 34;
						}
					}
				}
			}
			bool flag35 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
			if (flag35)
			{
				this.justRelease = true;
				int i2 = GameCanvas.py - this.pointerDownLastX[0];
				GameCanvas.isPointerJustRelease = false;
				bool flag36 = Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
				if (flag36)
				{
					this.cmRun = 0;
					this.cmtoY = this.cmy;
					this.pointerDownFirstX = -1000;
					this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
					bool flag37 = this.selected >= this.currentListLength;
					if (flag37)
					{
						this.selected = -1;
					}
					this.checkOptionSelect();
					this.pointerDownTime = 0;
					this.waitToPerform = 10;
					bool flag38 = this.isnewInventory;
					if (flag38)
					{
						this.waitToPerform = -1;
					}
					SoundMn.gI().panelClick();
				}
				else
				{
					bool flag39 = this.selected != -1 && this.pointerDownTime > 5;
					if (flag39)
					{
						this.pointerDownTime = 0;
						this.waitToPerform = 1;
					}
					else
					{
						bool flag40 = this.selected == -1 && !this.isDownWhenRunning;
						if (flag40)
						{
							bool flag41 = this.cmy < 0;
							if (flag41)
							{
								this.cmtoY = 0;
							}
							else
							{
								bool flag42 = this.cmy > this.cmyLim;
								if (flag42)
								{
									this.cmtoY = this.cmyLim;
								}
								else
								{
									int num4 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
									bool flag43 = num4 > 10;
									if (flag43)
									{
										num4 = 10;
									}
									else
									{
										bool flag44 = num4 < -10;
										if (flag44)
										{
											num4 = -10;
										}
										else
										{
											num4 = 0;
										}
									}
									this.cmRun = -num4 * 100;
								}
							}
						}
					}
				}
				bool flag45 = (this.isTabInven() || this.type == 13) && GameCanvas.py < this.yScroll + 21;
				if (flag45)
				{
					this.selected = 0;
					this.updateKeyInvenTab();
				}
				this.pointerIsDowning = false;
				this.pointerDownTime = 0;
				GameCanvas.isPointerJustRelease = false;
			}
		}
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00074B14 File Offset: 0x00072D14
	public string subArray(string[] str)
	{
		return null;
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00074B28 File Offset: 0x00072D28
	private void updateKeyInTabBar()
	{
		bool flag = this.scroll != null && this.scroll.pointerIsDowning;
		if (!flag)
		{
			bool flag2 = this.pointerIsDowning;
			if (!flag2)
			{
				int num = this.currentTabIndex;
				bool flag3 = this.isTabInven() && this.isnewInventory;
				if (flag3)
				{
					bool flag4 = this.selected == -1;
					if (flag4)
					{
						bool flag5 = GameCanvas.keyPressed[6];
						if (flag5)
						{
							this.currentTabIndex++;
							bool flag6 = this.currentTabIndex >= this.currentTabName.Length;
							if (flag6)
							{
								bool flag7 = GameCanvas.panel2 != null;
								if (flag7)
								{
									this.currentTabIndex = this.currentTabName.Length - 1;
									GameCanvas.isFocusPanel2 = true;
								}
								else
								{
									this.currentTabIndex = 0;
								}
							}
							this.selected = this.lastSelect[this.currentTabIndex];
							this.lastTabIndex[this.type] = this.currentTabIndex;
						}
						bool flag8 = GameCanvas.keyPressed[4];
						if (flag8)
						{
							this.currentTabIndex--;
							bool flag9 = this.currentTabIndex < 0;
							if (flag9)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
							}
							bool isFocusPanel = GameCanvas.isFocusPanel2;
							if (isFocusPanel)
							{
								GameCanvas.isFocusPanel2 = false;
							}
							this.selected = this.lastSelect[this.currentTabIndex];
							this.lastTabIndex[this.type] = this.currentTabIndex;
						}
					}
					else
					{
						bool flag10 = this.selected > 0;
						if (flag10)
						{
							bool flag11 = GameCanvas.keyPressed[8];
							if (flag11)
							{
								bool flag12 = this.newSelected == 0;
								if (flag12)
								{
									this.sellectInventory++;
								}
								else
								{
									this.sellectInventory += 5;
								}
							}
							else
							{
								bool flag13 = GameCanvas.keyPressed[2];
								if (flag13)
								{
									bool flag14 = this.newSelected == 0;
									if (flag14)
									{
										this.sellectInventory--;
									}
									else
									{
										this.sellectInventory -= 5;
									}
								}
								else
								{
									bool flag15 = GameCanvas.keyPressed[4];
									if (flag15)
									{
										bool flag16 = this.newSelected == 0;
										if (flag16)
										{
											this.sellectInventory -= 5;
										}
										else
										{
											this.sellectInventory--;
										}
									}
									else
									{
										bool flag17 = GameCanvas.keyPressed[6];
										if (flag17)
										{
											bool flag18 = this.newSelected == 0;
											if (flag18)
											{
												this.sellectInventory += 5;
											}
											else
											{
												this.sellectInventory++;
											}
										}
									}
								}
							}
						}
					}
					bool flag19 = this.sellectInventory < 0;
					if (flag19)
					{
					}
					bool flag20 = this.sellectInventory == this.nTableItem;
					if (flag20)
					{
						this.sellectInventory = 0;
					}
				}
				else
				{
					bool flag21 = !this.IsTabOption();
					if (flag21)
					{
						bool flag22 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
						if (flag22)
						{
							bool flag23 = this.isTabInven();
							if (flag23)
							{
								bool flag24 = this.selected >= 0;
								if (flag24)
								{
									this.updateKeyInvenTab();
								}
								else
								{
									this.currentTabIndex++;
									bool flag25 = this.currentTabIndex >= this.currentTabName.Length;
									if (flag25)
									{
										bool flag26 = GameCanvas.panel2 != null;
										if (flag26)
										{
											this.currentTabIndex = this.currentTabName.Length - 1;
											GameCanvas.isFocusPanel2 = true;
										}
										else
										{
											this.currentTabIndex = 0;
										}
									}
									this.selected = this.lastSelect[this.currentTabIndex];
									this.lastTabIndex[this.type] = this.currentTabIndex;
								}
							}
							else
							{
								this.currentTabIndex++;
								bool flag27 = this.currentTabIndex >= this.currentTabName.Length;
								if (flag27)
								{
									bool flag28 = GameCanvas.panel2 != null;
									if (flag28)
									{
										this.currentTabIndex = this.currentTabName.Length - 1;
										GameCanvas.isFocusPanel2 = true;
									}
									else
									{
										this.currentTabIndex = 0;
									}
								}
								this.selected = this.lastSelect[this.currentTabIndex];
								this.lastTabIndex[this.type] = this.currentTabIndex;
							}
						}
						bool flag29 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
						if (flag29)
						{
							this.currentTabIndex--;
							bool flag30 = this.currentTabIndex < 0;
							if (flag30)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
							}
							bool isFocusPanel2 = GameCanvas.isFocusPanel2;
							if (isFocusPanel2)
							{
								GameCanvas.isFocusPanel2 = false;
							}
							this.selected = this.lastSelect[this.currentTabIndex];
							this.lastTabIndex[this.type] = this.currentTabIndex;
						}
					}
				}
				this.keyTouchTab = -1;
				for (int i = 0; i < this.currentTabName.Length; i++)
				{
					bool flag31 = GameCanvas.isPointer(this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25);
					if (flag31)
					{
						this.keyTouchTab = i;
						bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
						if (isPointerJustRelease)
						{
							this.currentTabIndex = i;
							this.lastTabIndex[this.type] = i;
							GameCanvas.isPointerJustRelease = false;
							this.selected = this.lastSelect[this.currentTabIndex];
							bool flag32 = num == this.currentTabIndex && this.cmRun == 0;
							if (flag32)
							{
								this.cmtoY = 0;
								this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
							}
							break;
						}
					}
				}
				bool flag33 = num != this.currentTabIndex;
				if (flag33)
				{
					this.size_tab = 0;
					SoundMn.gI().panelClick();
					int num2 = this.type;
					switch (num2)
					{
					case 0:
					{
						bool flag34 = this.currentTabIndex == 0;
						if (flag34)
						{
							this.setTabTask();
						}
						bool flag35 = this.currentTabIndex == 1;
						if (flag35)
						{
							this.setTabInventory(true);
						}
						bool flag36 = this.currentTabIndex == 2;
						if (flag36)
						{
							this.setTabSkill();
						}
						bool flag37 = this.currentTabIndex == 3;
						if (flag37)
						{
							bool flag38 = this.mainTabName.Length > 4;
							if (flag38)
							{
								this.setTabClans();
							}
							else
							{
								this.setTabTool();
							}
						}
						bool flag39 = this.currentTabIndex == 4;
						if (flag39)
						{
							this.setTabTool();
						}
						break;
					}
					case 1:
						this.setTabShop();
						break;
					case 2:
					{
						bool flag40 = this.currentTabIndex == 0;
						if (flag40)
						{
							this.setTabBox();
						}
						bool flag41 = this.currentTabIndex == 1;
						if (flag41)
						{
							this.setTabInventory(true);
						}
						break;
					}
					case 3:
						this.setTabZone();
						break;
					default:
					{
						bool flag42 = num2 != 12;
						if (flag42)
						{
							bool flag43 = num2 != 13;
							if (flag43)
							{
								bool flag44 = num2 != 21;
								if (flag44)
								{
									bool flag45 = num2 == 25;
									if (flag45)
									{
										this.setTabSpeacialSkill();
									}
								}
								else
								{
									bool flag46 = this.currentTabIndex == 0;
									if (flag46)
									{
										this.setTabPetInventory();
									}
									bool flag47 = this.currentTabIndex == 1;
									if (flag47)
									{
										this.setTabPetStatus();
									}
									bool flag48 = this.currentTabIndex == 2;
									if (flag48)
									{
										this.setTabInventory(true);
									}
								}
							}
							else
							{
								bool flag49 = this.currentTabIndex == 0;
								if (flag49)
								{
									bool flag50 = this.Equals(GameCanvas.panel);
									if (flag50)
									{
										this.setTabInventory(true);
									}
									else
									{
										bool flag51 = this.Equals(GameCanvas.panel2);
										if (flag51)
										{
											this.setTabGiaoDich(false);
										}
									}
								}
								bool flag52 = this.currentTabIndex == 1;
								if (flag52)
								{
									this.setTabGiaoDich(true);
								}
								bool flag53 = this.currentTabIndex == 2;
								if (flag53)
								{
									this.setTabGiaoDich(false);
								}
							}
						}
						else
						{
							bool flag54 = this.currentTabIndex == 0;
							if (flag54)
							{
								this.setTabCombine();
							}
							bool flag55 = this.currentTabIndex == 1;
							if (flag55)
							{
								this.setTabInventory(true);
							}
						}
						break;
					}
					}
					this.selected = this.lastSelect[this.currentTabIndex];
				}
			}
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0007535C File Offset: 0x0007355C
	private void setTabPetStatus()
	{
		this.currentListLength = this.strStatus.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00003136 File Offset: 0x00001336
	private void setTabPetSkill()
	{
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00075428 File Offset: 0x00073628
	private void setTabTool()
	{
		SoundMn.gI().getSoundOption();
		this.currentListLength = Panel.strTool.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x000754FC File Offset: 0x000736FC
	public void initTabClans()
	{
		bool flag = this.isSearchClan;
		if (flag)
		{
			this.currentListLength = ((this.clans != null) ? (this.clans.Length + 2) : 2);
			this.clanInfo = mResources.clan_list;
		}
		else
		{
			bool flag2 = this.isViewMember;
			if (flag2)
			{
				this.clanReport = string.Empty;
				this.currentListLength = ((this.member != null) ? this.member.size() : this.myMember.size()) + 2;
				this.clanInfo = mResources.member + " " + ((this.currClan == null) ? global::Char.myCharz().clan.name : this.currClan.name);
			}
			else
			{
				bool flag3 = this.isMessage;
				if (flag3)
				{
					this.currentListLength = ClanMessage.vMessage.size() + 2;
					this.clanInfo = mResources.msg;
					this.clanReport = string.Empty;
				}
			}
		}
		bool flag4 = global::Char.myCharz().clan == null;
		if (flag4)
		{
			this.clansOption = new string[][]
			{
				mResources.findClan,
				mResources.createClan
			};
		}
		else
		{
			bool flag5 = !this.isViewMember;
			if (flag5)
			{
				bool flag6 = this.myMember.size() > 1;
				if (flag6)
				{
					this.clansOption = new string[][]
					{
						mResources.chatClan,
						mResources.request_pea2,
						mResources.memberr
					};
				}
				else
				{
					this.clansOption = new string[][]
					{
						mResources.memberr
					};
				}
			}
			else
			{
				bool flag7 = global::Char.myCharz().role > 0;
				if (flag7)
				{
					this.clansOption = new string[][]
					{
						mResources.msgg,
						mResources.leaveClan
					};
				}
				else
				{
					bool flag8 = this.myMember.size() > 1;
					if (flag8)
					{
						this.clansOption = new string[][]
						{
							mResources.msgg,
							mResources.leaveClan,
							mResources.khau_hieuu,
							mResources.bieu_tuongg
						};
					}
					else
					{
						this.clansOption = new string[][]
						{
							mResources.msgg,
							mResources.khau_hieuu,
							mResources.bieu_tuongg
						};
					}
				}
			}
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag9 = this.cmyLim < 0;
		if (flag9)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag10 = this.cmy < 0;
		if (flag10)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag11 = this.cmy > this.cmyLim;
		if (flag11)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x000757C8 File Offset: 0x000739C8
	public void setTabClans()
	{
		GameScr.isNewClanMessage = false;
		this.ITEM_HEIGHT = 24;
		bool flag = this.lastSelect != null && this.lastSelect[3] == 0;
		if (flag)
		{
			this.lastSelect[3] = -1;
		}
		this.currentListLength = 2;
		bool flag2 = global::Char.myCharz().clan != null;
		if (flag2)
		{
			this.isMessage = true;
			this.isViewMember = false;
			this.isSearchClan = false;
		}
		else
		{
			this.isMessage = false;
			this.isViewMember = false;
			this.isSearchClan = true;
		}
		bool flag3 = global::Char.myCharz().clan != null;
		if (flag3)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		this.initTabClans();
		this.cSelected = -1;
		bool flag4 = this.chatTField == null;
		if (flag4)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		bool flag5 = global::Char.myCharz().clan == null;
		if (flag5)
		{
			this.clanReport = mResources.findingClan;
			Service.gI().searchClan(string.Empty);
		}
		this.selected = this.lastSelect[this.currentTabIndex];
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.selected = -1;
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0007593C File Offset: 0x00073B3C
	public void initLogMessage()
	{
		this.currentListLength = this.logChat.size() + 1;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00075A01 File Offset: 0x00073C01
	private void setTabMessage()
	{
		this.ITEM_HEIGHT = 24;
		this.initLogMessage();
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00075A24 File Offset: 0x00073C24
	public void setTabShop()
	{
		this.ITEM_HEIGHT = 24;
		bool flag = this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2;
		if (flag)
		{
			this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		}
		else
		{
			this.currentListLength = global::Char.myCharz().arrItemShop[this.currentTabIndex].Length;
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag2 = this.cmyLim < 0;
		if (flag2)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag3 = this.cmy < 0;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag4 = this.cmy > this.cmyLim;
		if (flag4)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00075B54 File Offset: 0x00073D54
	private void setTabSkill()
	{
		this.ITEM_HEIGHT = 30;
		this.currentListLength = global::Char.myCharz().nClass.skillTemplates.Length + 6;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = this.cmyLim;
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00075C20 File Offset: 0x00073E20
	private void setTabMapTrans()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.mapNames.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00075C80 File Offset: 0x00073E80
	private void setTabZone()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = GameScr.gI().zones.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00075CE4 File Offset: 0x00073EE4
	private void setTabBox()
	{
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBox.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00075DB8 File Offset: 0x00073FB8
	private void setTabPetInventory()
	{
		this.ITEM_HEIGHT = 30;
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		this.currentListLength = arrItemBody.Length + arrPetSkill.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00075E98 File Offset: 0x00074098
	private void setTabInventory(bool resetSelect)
	{
		bool flag = this.isnewInventory;
		if (flag)
		{
			int num = global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length;
			this.currentListLength = this.checkCurrentListLength(num);
			this.currentListLength = 3;
			this.newSelected = 0;
			this.size_tab = (sbyte)(num / 20 + ((num % 20 <= 0) ? 0 : 1));
			Res.outz("sizeTab = " + this.size_tab.ToString());
		}
		else
		{
			this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
			this.ITEM_HEIGHT = 24;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			bool flag2 = this.cmyLim < 0;
			if (flag2)
			{
				this.cmyLim = 0;
			}
			bool flag3 = this.cmy < 0;
			if (flag3)
			{
				this.cmy = (this.cmtoY = 0);
			}
			bool flag4 = this.cmy > this.cmyLim;
			if (flag4)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (resetSelect)
			{
				this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
			}
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00075FFC File Offset: 0x000741FC
	private void setTabMap()
	{
		bool flag = !Panel.isPaintMap;
		if (!flag)
		{
			bool flag2 = TileMap.lastPlanetId != TileMap.planetID;
			if (flag2)
			{
				Res.outz("LOAD TAM HINH");
				Panel.imgMap = GameCanvas.loadImageRMS("/img/map" + TileMap.planetID.ToString() + ".png");
				TileMap.lastPlanetId = TileMap.planetID;
			}
			this.cmxMap = this.getXMap() - this.wScroll / 2;
			this.cmyMap = this.getYMap() + this.yScroll - (this.yScroll + this.hScroll / 2);
			this.pa1 = this.cmxMap;
			this.pa2 = this.cmyMap;
			this.cmxMapLim = 250 - this.wScroll;
			this.cmyMapLim = 220 - this.hScroll;
			bool flag3 = this.cmxMapLim < 0;
			if (flag3)
			{
				this.cmxMapLim = 0;
			}
			bool flag4 = this.cmyMapLim < 0;
			if (flag4)
			{
				this.cmyMapLim = 0;
			}
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag5 = TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i];
				if (flag5)
				{
					this.xMove = Panel.mapX[(int)TileMap.planetID][i] + this.xScroll;
					this.yMove = Panel.mapY[(int)TileMap.planetID][i] + this.yScroll + 5;
					break;
				}
			}
			this.xMap = this.getXMap() + this.xScroll;
			this.yMap = this.getYMap() + this.yScroll;
			this.xMapTask = this.getXMapTask() + this.xScroll;
			this.yMapTask = this.getYMapTask() + this.yScroll;
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x000761DB File Offset: 0x000743DB
	private void setTabTask()
	{
		this.cmyQuest = 0;
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x000761E8 File Offset: 0x000743E8
	public void moveCamera()
	{
		bool flag = this.timeShow > 0;
		if (flag)
		{
			this.timeShow--;
		}
		bool flag2 = this.justRelease && this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1;
		if (flag2)
		{
			bool flag3 = this.cmy < -50;
			if (flag3)
			{
				InfoDlg.showWait();
				this.justRelease = false;
				bool flag4 = this.currPageShop[this.currentTabIndex] <= 0;
				if (flag4)
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
				}
				else
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
				}
			}
			else
			{
				bool flag5 = this.cmy > this.cmyLim + 50;
				if (flag5)
				{
					this.justRelease = false;
					InfoDlg.showWait();
					bool flag6 = this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1;
					if (flag6)
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
					}
					else
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
					}
				}
			}
		}
		bool flag7 = this.cmx != this.cmtoX && !this.pointerIsDowning;
		if (flag7)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		bool flag8 = global::Math.abs(this.cmtoX - this.cmx) < 10;
		if (flag8)
		{
			this.cmx = this.cmtoX;
		}
		bool flag9 = this.isClose;
		if (flag9)
		{
			this.isClose = false;
			this.cmtoX = this.wScroll;
		}
		bool flag10 = this.cmtoX >= this.wScroll - 10 && this.cmx >= this.wScroll - 10 && this.position == 0;
		if (flag10)
		{
			this.isShow = false;
			this.cleanCombine();
			bool flag11 = this.isChangeZone;
			if (flag11)
			{
				this.isChangeZone = false;
				bool flag12 = global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14;
				if (flag12)
				{
					InfoDlg.showWait();
					bool flag13 = this.type == 3;
					if (flag13)
					{
						Service.gI().requestChangeZone(this.selected, -1);
					}
					else
					{
						bool flag14 = this.type == 14;
						if (flag14)
						{
							Service.gI().requestMapSelect(this.selected);
						}
					}
				}
			}
			bool flag15 = this.isSelectPlayerMenu;
			if (flag15)
			{
				this.isSelectPlayerMenu = false;
				int num = this.vPlayerMenu.size() - this.vPlayerMenu_id.size();
				bool flag16 = global::Char.myCharz().charFocus != null;
				if (flag16)
				{
					bool flag17 = this.selected - num < 0;
					if (flag17)
					{
						global::Char.myCharz().charFocus.menuSelect = this.selected;
					}
					else
					{
						global::Char.myCharz().charFocus.menuSelect = (int)short.Parse((string)this.vPlayerMenu_id.elementAt(this.selected - num));
					}
				}
				Command command = (Command)this.vPlayerMenu.elementAt(this.selected);
				command.performAction();
			}
			this.vPlayerMenu.removeAllElements();
			this.charMenu = null;
		}
		bool flag18 = this.cmRun != 0 && !this.pointerIsDowning;
		if (flag18)
		{
			this.cmtoY += this.cmRun / 100;
			bool flag19 = this.cmtoY < 0;
			if (flag19)
			{
				this.cmtoY = 0;
			}
			else
			{
				bool flag20 = this.cmtoY > this.cmyLim;
				if (flag20)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					this.cmy = this.cmtoY;
				}
			}
			this.cmRun = this.cmRun * 9 / 10;
			bool flag21 = this.cmRun < 100 && this.cmRun > -100;
			if (flag21)
			{
				this.cmRun = 0;
			}
		}
		bool flag22 = this.cmy != this.cmtoY && !this.pointerIsDowning;
		if (flag22)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		this.cmyLast[this.currentTabIndex] = this.cmy;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00076700 File Offset: 0x00074900
	public void paintDetail(mGraphics g)
	{
		bool flag = this.cp != null;
		if (flag)
		{
			bool flag2 = this.cp.says == null;
			if (!flag2)
			{
				this.cp.paint(g);
				int num = this.cp.cx + 13;
				int num2 = this.cp.cy + 11;
				bool flag3 = this.type == 15;
				if (flag3)
				{
					num += 5;
					num2 += 26;
				}
				bool flag4 = this.type == 0 && this.currentTabIndex == 3;
				if (flag4)
				{
					bool flag5 = this.isSearchClan;
					if (flag5)
					{
						num -= 5;
					}
					else
					{
						bool flag6 = this.partID != null || this.charInfo != null;
						if (flag6)
						{
							num = this.cp.cx + 21;
							num2 = this.cp.cy + 40;
						}
					}
				}
				bool flag7 = this.partID != null;
				if (flag7)
				{
					Part part = GameScr.parts[this.partID[0]];
					Part part2 = GameScr.parts[this.partID[1]];
					Part part3 = GameScr.parts[this.partID[2]];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, num + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, num2 - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, num + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, num2 - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy, 0, 0);
				}
				else
				{
					bool flag8 = this.charInfo != null;
					if (flag8)
					{
						this.charInfo.paintCharBody(g, num + 5, num2 + 25, 1, 0, true);
					}
					else
					{
						bool flag9 = this.idIcon != -1;
						if (flag9)
						{
							SmallImage.drawSmallImage(g, this.idIcon, num, num2, 0, 3);
						}
					}
				}
				bool flag10 = this.currItem != null && this.currItem.template.type != 5;
				if (flag10)
				{
					bool flag11 = this.currItem.compare > 0;
					if (flag11)
					{
						g.drawImage(Panel.imgUp, num - 7, num2 + 13, 3);
						mFont.tahoma_7b_green.drawString(g, Res.abs(this.currItem.compare).ToString() + string.Empty, num + 1, num2 + 8, 0);
					}
					else
					{
						bool flag12 = this.currItem.compare < 0 && this.currItem.compare != -1;
						if (flag12)
						{
							g.drawImage(Panel.imgDown, num - 7, num2 + 13, 3);
							mFont.tahoma_7b_red.drawString(g, Res.abs(this.currItem.compare).ToString() + string.Empty, num + 1, num2 + 8, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00076AC8 File Offset: 0x00074CC8
	public void paintTop(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (!flag)
		{
			int num = (this.cmy + this.hScroll) / 24 + 1;
			bool flag2 = num < this.hScroll / 24 + 1;
			if (flag2)
			{
				num = this.hScroll / 24 + 1;
			}
			bool flag3 = num > this.currentListLength;
			if (flag3)
			{
				num = this.currentListLength;
			}
			int num2 = this.cmy / 24;
			bool flag4 = num2 >= num;
			if (flag4)
			{
				num2 = num - 1;
			}
			bool flag5 = num2 < 0;
			if (flag5)
			{
				num2 = 0;
			}
			for (int i = num2; i < num; i++)
			{
				int num3 = this.xScroll;
				int num4 = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num6 = this.xScroll + num5;
				int num7 = this.yScroll + i * this.ITEM_HEIGHT;
				int num8 = this.wScroll - num5;
				int num9 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num6, num7, num8, num9);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num3, num4, num5, h);
				TopInfo topInfo = (TopInfo)this.vTop.elementAt(i);
				bool flag6 = topInfo.headICON != -1;
				if (flag6)
				{
					SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[topInfo.headID];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num4 + num9 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				bool flag7 = topInfo.pId != global::Char.myCharz().charID;
				if (flag7)
				{
					mFont.tahoma_7b_green.drawString(g, topInfo.name, num6 + 5, num7, 0);
				}
				else
				{
					mFont.tahoma_7b_red.drawString(g, topInfo.name, num6 + 5, num7, 0);
				}
				mFont.tahoma_7_blue.drawString(g, topInfo.info, num6 + num8 - 5, num7 + 11, 1);
				mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
				{
					mResources.rank,
					": ",
					topInfo.rank,
					string.Empty
				}), num6 + 5, num7 + 11, 0);
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00076DF0 File Offset: 0x00074FF0
	public void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
		g.translate(-this.cmx, 0);
		g.translate(this.X, this.Y);
		bool flag = GameCanvas.panel.combineSuccess != -1;
		if (flag)
		{
			bool flag2 = this.Equals(GameCanvas.panel);
			if (flag2)
			{
				this.paintCombineEff(g);
			}
		}
		else
		{
			GameCanvas.paintz.paintFrameSimple(this.X, this.Y, this.W, this.H, g);
			this.paintTopInfo(g);
			this.paintBottomMoneyInfo(g);
			this.paintTab(g);
			switch (this.type)
			{
			case 0:
			{
				bool flag3 = this.currentTabIndex == 0;
				if (flag3)
				{
					this.paintTask(g);
				}
				bool flag4 = this.currentTabIndex == 1;
				if (flag4)
				{
					this.paintInventory(g);
				}
				bool flag5 = this.currentTabIndex == 2;
				if (flag5)
				{
					this.paintSkill(g);
				}
				bool flag6 = this.currentTabIndex == 3;
				if (flag6)
				{
					bool flag7 = this.mainTabName.Length == 4;
					if (flag7)
					{
						this.paintTools(g);
					}
					else
					{
						this.paintClans(g);
					}
				}
				bool flag8 = this.currentTabIndex == 4;
				if (flag8)
				{
					this.paintTools(g);
				}
				break;
			}
			case 1:
				this.paintShop(g);
				break;
			case 2:
			{
				bool flag9 = this.currentTabIndex == 0;
				if (flag9)
				{
					this.paintBox(g);
				}
				bool flag10 = this.currentTabIndex == 1;
				if (flag10)
				{
					this.paintInventory(g);
				}
				break;
			}
			case 3:
				this.paintZone(g);
				break;
			case 4:
				this.paintMap(g);
				break;
			case 7:
				this.paintInventory(g);
				break;
			case 8:
				this.paintLogChat(g);
				break;
			case 9:
				this.paintArchivement(g);
				break;
			case 10:
				this.paintPlayerMenu(g);
				break;
			case 11:
				this.paintFriend(g);
				break;
			case 12:
			{
				bool flag11 = this.currentTabIndex == 0;
				if (flag11)
				{
					this.paintCombine(g);
				}
				bool flag12 = this.currentTabIndex == 1;
				if (flag12)
				{
					this.paintInventory(g);
				}
				break;
			}
			case 13:
			{
				bool flag13 = this.currentTabIndex == 0;
				if (flag13)
				{
					bool flag14 = this.Equals(GameCanvas.panel);
					if (flag14)
					{
						this.paintInventory(g);
					}
					else
					{
						this.paintGiaoDich(g, false);
					}
				}
				bool flag15 = this.currentTabIndex == 1;
				if (flag15)
				{
					this.paintGiaoDich(g, true);
				}
				bool flag16 = this.currentTabIndex == 2;
				if (flag16)
				{
					this.paintGiaoDich(g, false);
				}
				break;
			}
			case 14:
				this.paintMapTrans(g);
				break;
			case 15:
				this.paintTop(g);
				break;
			case 16:
				this.paintEnemy(g);
				break;
			case 17:
				this.paintShop(g);
				break;
			case 18:
				this.paintFlagChange(g);
				break;
			case 19:
				this.paintOption(g);
				break;
			case 20:
				this.paintAccount(g);
				break;
			case 21:
			{
				bool flag17 = this.currentTabIndex == 0;
				if (flag17)
				{
					this.paintPetInventory(g);
				}
				bool flag18 = this.currentTabIndex == 1;
				if (flag18)
				{
					this.paintPetStatus(g);
				}
				bool flag19 = this.currentTabIndex == 2;
				if (flag19)
				{
					this.paintInventory(g);
				}
				break;
			}
			case 22:
				this.paintAuto(g);
				break;
			case 23:
				this.paintGameInfo(g);
				break;
			case 24:
				this.paintGameSubInfo(g);
				break;
			case 25:
				this.paintSpeacialSkill(g);
				break;
			}
			GameScr.resetTranslate(g);
			this.paintDetail(g);
			bool flag20 = this.cmx == this.cmtoX;
			if (flag20)
			{
				this.cmdClose.paint(g);
			}
			bool flag21 = this.tabIcon != null && this.tabIcon.isShow;
			if (flag21)
			{
				this.tabIcon.paint(g);
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.translate(this.X, this.Y);
			g.translate(-this.cmx, 0);
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00077270 File Offset: 0x00075470
	private void paintShop(mGraphics g)
	{
		try
		{
			bool flag = this.type == 1 && this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2;
			if (flag)
			{
				this.paintInventory(g);
			}
			else
			{
				g.setColor(16711680);
				g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
				bool flag2 = this.typeShop == 2 && this.Equals(GameCanvas.panel);
				if (flag2)
				{
					bool flag3 = this.currentTabIndex <= 3 && GameCanvas.isTouch;
					if (flag3)
					{
						bool flag4 = this.cmy < -50;
						if (flag4)
						{
							GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + 30, g);
						}
						else
						{
							bool flag5 = this.cmy < 0;
							if (flag5)
							{
								mFont.tahoma_7_grey.drawString(g, mResources.getDown, this.xScroll + this.wScroll / 2, this.yScroll + 15, 2);
							}
							else
							{
								bool flag6 = this.cmyLim >= 0;
								if (flag6)
								{
									bool flag7 = this.cmy > this.cmyLim + 50;
									if (flag7)
									{
										GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 30, g);
									}
									else
									{
										bool flag8 = this.cmy > this.cmyLim;
										if (flag8)
										{
											mFont.tahoma_7_grey.drawString(g, mResources.getUp, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 25, 2);
										}
									}
								}
							}
						}
					}
					bool flag9 = global::Char.myCharz().arrItemShop[this.currentTabIndex].Length == 0 && this.type != 17;
					if (flag9)
					{
						mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
						return;
					}
				}
				g.translate(0, -this.cmy);
				Item[] array = global::Char.myCharz().arrItemShop[this.currentTabIndex];
				bool flag10 = this.typeShop == 2 && (this.currentTabIndex == 4 || this.type == 17);
				if (flag10)
				{
					array = global::Char.myCharz().arrItemShop[4];
					bool flag11 = array.Length == 0;
					if (flag11)
					{
						mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
						return;
					}
				}
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					int num2 = this.xScroll + 26;
					int num3 = this.yScroll + i * this.ITEM_HEIGHT;
					int num4 = this.wScroll - 26;
					int h = this.ITEM_HEIGHT - 1;
					int num5 = this.xScroll;
					int num6 = this.yScroll + i * this.ITEM_HEIGHT;
					int num7 = 24;
					int num8 = this.ITEM_HEIGHT - 1;
					bool flag12 = num3 - this.cmy <= this.yScroll + this.hScroll;
					if (flag12)
					{
						bool flag13 = num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
						if (flag13)
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
							g.fillRect(num2, num3, num4, h);
							g.setColor((i != this.selected) ? 9993045 : 9541120);
							g.fillRect(num5, num6, num7, num8);
							Item item = array[i];
							bool flag14 = item != null;
							if (flag14)
							{
								string str = string.Empty;
								mFont mFont = mFont.tahoma_7_green2;
								bool flag15 = item.isMe != 0 && this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2);
								if (flag15)
								{
									mFont = mFont.tahoma_7b_green;
								}
								bool flag16 = item.itemOption != null;
								if (flag16)
								{
									for (int j = 0; j < item.itemOption.Length; j++)
									{
										bool flag17 = item.itemOption[j].optionTemplate.id == 72;
										if (flag17)
										{
											str = " [+" + item.itemOption[j].param.ToString() + "]";
										}
										bool flag18 = item.itemOption[j].optionTemplate.id == 41;
										if (flag18)
										{
											bool flag19 = item.itemOption[j].param == 1;
											if (flag19)
											{
												mFont = Panel.GetFont(0);
											}
											else
											{
												bool flag20 = item.itemOption[j].param == 2;
												if (flag20)
												{
													mFont = Panel.GetFont(2);
												}
												else
												{
													bool flag21 = item.itemOption[j].param == 3;
													if (flag21)
													{
														mFont = Panel.GetFont(8);
													}
													else
													{
														bool flag22 = item.itemOption[j].param == 4;
														if (flag22)
														{
															mFont = Panel.GetFont(7);
														}
													}
												}
											}
										}
									}
								}
								mFont.drawString(g, item.template.name + str, num2 + 5, num3 + 1, 0);
								string text = string.Empty;
								bool flag23 = item.itemOption != null && item.itemOption.Length >= 1;
								if (flag23)
								{
									bool flag24 = item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
									if (flag24)
									{
										text += item.itemOption[0].getOptionString();
									}
									mFont mFont2 = mFont.tahoma_7_blue;
									bool flag25 = item.compare < 0 && item.template.type != 5;
									if (flag25)
									{
										mFont2 = mFont.tahoma_7_red;
									}
									bool flag26 = this.typeShop == 2 && item.itemOption.Length > 1 && item.buyType != -1;
									if (flag26)
									{
										text += string.Empty;
									}
									bool flag27 = this.typeShop != 2 || (this.typeShop == 2 && item.buyType <= 1);
									if (flag27)
									{
										mFont2.drawString(g, text, num2 + 5, num3 + 11, 0);
									}
								}
								bool flag28 = item.buySpec > 0;
								if (flag28)
								{
									SmallImage.drawSmallImage(g, (int)item.iconSpec, num2 + num4 - 7, num3 + 9, 0, 3);
									mFont.tahoma_7b_blue.drawString(g, Res.formatNumber((long)item.buySpec), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
								}
								bool flag29 = item.buyCoin != 0 || item.buyGold != 0;
								if (flag29)
								{
									bool flag30 = this.typeShop != 2 && item.powerRequire == 0L;
									if (flag30)
									{
										bool flag31 = item.buyCoin > 0 && item.buyGold > 0;
										if (flag31)
										{
											bool flag32 = item.buyCoin > 0;
											if (flag32)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
											}
											bool flag33 = item.buyGold > 0;
											if (flag33)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
												mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
											}
										}
										else
										{
											bool flag34 = item.buyCoin > 0;
											if (flag34)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
											}
											bool flag35 = item.buyGold > 0;
											if (flag35)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
												mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
											}
										}
									}
									bool flag36 = this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2);
									if (flag36)
									{
										bool flag37 = item.buyCoin > 0 && item.buyGold > 0;
										if (flag37)
										{
											bool flag38 = item.buyCoin > 0;
											if (flag38)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
												bool flag39 = global::Char.myCharz().xu < (long)item.buyCoin;
												if (flag39)
												{
													mFont = mFont.tahoma_7b_red;
												}
												else
												{
													mFont = mFont.tahoma_7b_yellow;
												}
												mFont.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
											}
											bool flag40 = item.buyGold > 0;
											if (flag40)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
												bool flag41 = global::Char.myCharz().luong < item.buyGold;
												if (flag41)
												{
													mFont = mFont.tahoma_7b_red;
												}
												else
												{
													mFont = mFont.tahoma_7b_green;
												}
												mFont.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
											}
										}
										else
										{
											bool flag42 = item.buyCoin > 0;
											if (flag42)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
												bool flag43 = global::Char.myCharz().xu < (long)item.buyCoin;
												if (flag43)
												{
													mFont = mFont.tahoma_7b_red;
												}
												else
												{
													mFont = mFont.tahoma_7b_yellow;
												}
												mFont.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
											}
											bool flag44 = item.buyGold > 0;
											if (flag44)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
												bool flag45 = global::Char.myCharz().luong < item.buyGold;
												if (flag45)
												{
													mFont = mFont.tahoma_7b_red;
												}
												else
												{
													mFont = mFont.tahoma_7b_green;
												}
												mFont.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
											}
											try
											{
												mFont = mFont.tahoma_7b_green;
												bool flag46 = !global::Char.myCharz().cName.Equals(item.nameNguoiKyGui);
												if (flag46)
												{
													mFont = mFont.tahoma_7b_green;
												}
												mFont.drawString(g, item.nameNguoiKyGui, num2 + num4, num3 + 1 + mFont.tahoma_7b_red.getHeight(), mFont.RIGHT);
											}
											catch (Exception ex)
											{
											}
										}
									}
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								bool flag47 = item.quantity > 1;
								if (flag47)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
								bool flag48 = item.newItem && GameCanvas.gameTick % 10 > 5;
								if (flag48)
								{
									g.drawImage(Panel.imgNew, num5 + num7 / 2, num3 + 19, 3);
								}
							}
							bool flag49 = this.typeShop == 2 && (this.Equals(GameCanvas.panel2) || this.currentTabIndex == 4);
							if (flag49)
							{
								bool flag50 = item.buyType != 0;
								if (flag50)
								{
									bool flag51 = item.buyType == 1;
									if (flag51)
									{
										mFont.tahoma_7_green.drawString(g, mResources.dangban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
										bool flag52 = item.buyCoin != -1;
										if (flag52)
										{
											g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 19, 3);
											mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 13, mFont.RIGHT);
										}
										else
										{
											bool flag53 = item.buyGold != -1;
											if (flag53)
											{
												g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
												mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
											}
										}
									}
									else
									{
										bool flag54 = item.buyType == 2;
										if (flag54)
										{
											mFont.tahoma_7b_blue.drawString(g, mResources.daban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
											bool flag55 = item.buyCoin != -1;
											if (flag55)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 17, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
											}
											else
											{
												bool flag56 = item.buyGold != -1;
												if (flag56)
												{
													g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
													mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
												}
											}
										}
									}
								}
							}
						}
					}
				}
				this.paintScrollArrow(g);
			}
		}
		catch (Exception ex2)
		{
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00003136 File Offset: 0x00001336
	private void paintAuto(mGraphics g)
	{
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0007812C File Offset: 0x0007632C
	private void paintPetStatus(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.strStatus.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, this.strStatus[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x00003136 File Offset: 0x00001336
	private void paintPetSkill()
	{
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00078254 File Offset: 0x00076454
	private void paintPetInventory(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		for (int i = 0; i < arrItemBody.Length + arrPetSkill.Length; i++)
		{
			bool flag = i < arrItemBody.Length;
			int num = i;
			int num2 = i - arrItemBody.Length;
			int num3 = this.xScroll + 36;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll;
			int num7 = this.yScroll + i * this.ITEM_HEIGHT;
			int num8 = 34;
			int num9 = this.ITEM_HEIGHT - 1;
			bool flag2 = num4 - this.cmy <= this.yScroll + this.hScroll;
			if (flag2)
			{
				bool flag3 = num4 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag3)
				{
					Item item = (!flag) ? null : arrItemBody[num];
					g.setColor((i != this.selected) ? ((!flag) ? 15723751 : 15196114) : 16383818);
					g.fillRect(num3, num4, num5, h);
					g.setColor((i != this.selected) ? ((!flag) ? 11837316 : 9993045) : 9541120);
					bool flag4 = item != null;
					if (flag4)
					{
						for (int j = 0; j < item.itemOption.Length; j++)
						{
							bool flag5 = item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0;
							if (flag5)
							{
								sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
								int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
								bool flag6 = color_ItemBg != -1;
								if (flag6)
								{
									g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
								}
							}
						}
					}
					g.fillRect(num6, num7, num8, num9);
					bool flag7 = item != null && item.isSelect && GameCanvas.panel.type == 12;
					if (flag7)
					{
						g.setColor((i != this.selected) ? 6047789 : 7040779);
						g.fillRect(num6, num7, num8, num9);
					}
					bool flag8 = item != null;
					if (flag8)
					{
						string str = string.Empty;
						mFont mFont = mFont.tahoma_7_green2;
						bool flag9 = item.itemOption != null;
						if (flag9)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								bool flag10 = item.itemOption[k].optionTemplate.id == 72;
								if (flag10)
								{
									str = " [+" + item.itemOption[k].param.ToString() + "]";
								}
								bool flag11 = item.itemOption[k].optionTemplate.id == 41;
								if (flag11)
								{
									bool flag12 = item.itemOption[k].param == 1;
									if (flag12)
									{
										mFont = Panel.GetFont(0);
									}
									else
									{
										bool flag13 = item.itemOption[k].param == 2;
										if (flag13)
										{
											mFont = Panel.GetFont(2);
										}
										else
										{
											bool flag14 = item.itemOption[k].param == 3;
											if (flag14)
											{
												mFont = Panel.GetFont(8);
											}
											else
											{
												bool flag15 = item.itemOption[k].param == 4;
												if (flag15)
												{
													mFont = Panel.GetFont(7);
												}
											}
										}
									}
								}
							}
						}
						mFont.drawString(g, item.template.name + str, num3 + 5, num4 + 1, 0);
						string text = string.Empty;
						bool flag16 = item.itemOption != null;
						if (flag16)
						{
							bool flag17 = item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
							if (flag17)
							{
								text += item.itemOption[0].getOptionString();
							}
							mFont mFont2 = mFont.tahoma_7_blue;
							bool flag18 = item.compare < 0 && item.template.type != 5;
							if (flag18)
							{
								mFont2 = mFont.tahoma_7_red;
							}
							bool flag19 = item.itemOption.Length > 1;
							if (flag19)
							{
								for (int l = 1; l < 2; l++)
								{
									bool flag20 = item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107;
									if (flag20)
									{
										text = text + "," + item.itemOption[l].getOptionString();
									}
								}
							}
							mFont2.drawString(g, text, num3 + 5, num4 + 11, mFont.LEFT);
						}
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						bool flag21 = item.itemOption != null;
						if (flag21)
						{
							for (int m = 0; m < item.itemOption.Length; m++)
							{
								this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num6, num7, num8, num9);
							}
							for (int n = 0; n < item.itemOption.Length; n++)
							{
								this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num6, num7, num8, num9);
							}
						}
						bool flag22 = item.quantity > 1;
						if (flag22)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num6 + num8, num7 + num9 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
					else
					{
						bool flag23 = !flag;
						if (flag23)
						{
							Skill skill = arrPetSkill[num2];
							g.drawImage(GameScr.imgSkill, num6 + num8 / 2, num7 + num9 / 2, 3);
							bool flag24 = skill.template != null;
							if (flag24)
							{
								mFont.tahoma_7_blue.drawString(g, skill.template.name, num3 + 5, num4 + 1, 0);
								mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
								{
									mResources.level,
									": ",
									skill.point,
									string.Empty
								}), num3 + 5, num4 + 11, 0);
								SmallImage.drawSmallImage(g, skill.template.iconId, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
							}
							else
							{
								mFont.tahoma_7_green2.drawString(g, skill.moreInfo, num3 + 5, num4 + 5, 0);
								SmallImage.drawSmallImage(g, GameScr.efs[98].arrEfInfo[0].idImg, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00078A38 File Offset: 0x00076C38
	private void paintScrollArrow(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool flag = (this.cmy > 24 && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1);
		if (flag)
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
		}
		bool flag2 = (this.cmy < this.cmyLim && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1);
		if (flag2)
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 8, 0);
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00078B48 File Offset: 0x00076D48
	private void paintTools(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strTool.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strTool[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
					bool flag3 = Panel.strTool[i].Equals(mResources.gameInfo);
					if (flag3)
					{
						for (int j = 0; j < Panel.vGameInfo.size(); j++)
						{
							GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(j);
							bool flag4 = !gameInfo.hasRead;
							if (flag4)
							{
								bool flag5 = GameCanvas.gameTick % 20 > 10;
								if (flag5)
								{
									g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
								}
								break;
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x00078CFC File Offset: 0x00076EFC
	private void paintGameSubInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.contenInfo.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * 15;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					mFont.tahoma_7b_dark.drawString(g, Panel.contenInfo[i], this.xScroll + 5, num2 + 6, mFont.LEFT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00078DF0 File Offset: 0x00076FF0
	private void paintGameInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.vGameInfo.size(); i++)
		{
			GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(i);
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					mFont.tahoma_7b_dark.drawString(g, gameInfo.main, this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
					bool flag3 = !gameInfo.hasRead && GameCanvas.gameTick % 20 > 10;
					if (flag3)
					{
						g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00078F64 File Offset: 0x00077164
	private void paintSkill(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int num = global::Char.myCharz().nClass.skillTemplates.Length;
		for (int i = 0; i < num + 6; i++)
		{
			int num2 = this.xScroll + 30;
			int num3 = this.yScroll + i * this.ITEM_HEIGHT;
			int num4 = this.wScroll - 30;
			int h = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = this.ITEM_HEIGHT - 1;
			bool flag = num3 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					bool flag3 = i == 5;
					if (flag3)
					{
						g.setColor((i != this.selected) ? 16765060 : 16776068);
					}
					g.fillRect(num2, num3, num4, h);
					g.drawImage(GameScr.imgSkill, num5, num6, 0);
					bool flag4 = i == 0;
					if (flag4)
					{
						SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
						string st = string.Concat(new string[]
						{
							mResources.HP,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cHPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(global::Char.myCharz().cHPGoc + 1000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().hpFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					bool flag5 = i == 1;
					if (flag5)
					{
						SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
						string st2 = string.Concat(new string[]
						{
							mResources.KI,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cMPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(global::Char.myCharz().cMPGoc + 1000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().mpFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					bool flag6 = i == 2;
					if (flag6)
					{
						SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
						string st3 = string.Concat(new string[]
						{
							mResources.hit_point,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cDamGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(global::Char.myCharz().cDamGoc * 100)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().damFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					bool flag7 = i == 3;
					if (flag7)
					{
						SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
						string st4 = string.Concat(new string[]
						{
							mResources.armor,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cDefGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(500000 + global::Char.myCharz().cDefGoc * 100000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().defFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					bool flag8 = i == 4;
					if (flag8)
					{
						SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
						string st5 = string.Concat(new object[]
						{
							mResources.critical,
							" ",
							mResources.root,
							": ",
							global::Char.myCharz().cCriticalGoc,
							"%"
						});
						int num8 = global::Char.myCharz().cCriticalGoc;
						bool flag9 = num8 > Panel.t_tiemnang.Length - 1;
						if (flag9)
						{
							num8 = Panel.t_tiemnang.Length - 1;
						}
						long num9 = Panel.t_tiemnang[num8];
						mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
						long number = num9;
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							Res.formatNumber2(number),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().criticalFrom1000Tiemnang
						}), num2 + 5, num3 + 15, 0);
					}
					bool flag10 = i == 5;
					if (flag10)
					{
						bool flag11 = Panel.specialInfo != null;
						if (flag11)
						{
							SmallImage.drawSmallImage(g, (int)Panel.spearcialImage, num5 + 4, num6 + 4, 0, 0);
							string[] array = mFont.tahoma_7.splitFontArray(Panel.specialInfo, 120);
							for (int j = 0; j < array.Length; j++)
							{
								mFont.tahoma_7_green2.drawString(g, array[j], num2 + 5, num3 + 3 + j * 12, 0);
							}
						}
						else
						{
							mFont.tahoma_7_green2.drawString(g, string.Empty, num2 + 5, num3 + 9, 0);
						}
					}
					bool flag12 = i >= 6;
					if (flag12)
					{
						int num10 = i - 6;
						SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num10];
						SmallImage.drawSmallImage(g, skillTemplate.iconId, num5 + 4, num6 + 4, 0, 0);
						Skill skill = global::Char.myCharz().getSkill(skillTemplate);
						bool flag13 = skill != null;
						if (flag13)
						{
							mFont.tahoma_7b_blue.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_blue.drawString(g, mResources.level + ": " + skill.point.ToString(), num2 + num4 - 5, num3 + 3, mFont.RIGHT);
							bool flag14 = skill.point == skillTemplate.maxPoint;
							if (flag14)
							{
								mFont.tahoma_7_green2.drawString(g, mResources.max_level_reach, num2 + 5, num3 + 15, 0);
							}
							else
							{
								bool flag15 = skill.template.isSkillSpec();
								if (flag15)
								{
									string text = mResources.proficiency + ": ";
									int x = mFont.tahoma_7_green2.getWidthExactOf(text) + num2 + 5;
									int num11 = num3 + 15;
									mFont.tahoma_7_green2.drawString(g, text, num2 + 5, num11, 0);
									mFont.tahoma_7_green2.drawString(g, "(" + skill.strCurExp() + ")", num2 + num4 - 5, num11, mFont.RIGHT);
									num11 += 4;
									g.setColor(7169134);
									g.fillRect(x, num11, 50, 5);
									int num12 = (int)(skill.curExp * 50 / 1000);
									g.setColor(11992374);
									g.fillRect(x, num11, num12, 5);
									bool flag16 = skill.curExp >= 1000;
									if (flag16)
									{
									}
								}
								else
								{
									Skill skill2 = skillTemplate.skills[skill.point];
									mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
									{
										mResources.level,
										" ",
										skill.point + 1,
										" ",
										mResources.need,
										" ",
										Res.formatNumber2(skill2.powRequire),
										" ",
										mResources.potential
									}), num2 + 5, num3 + 15, 0);
								}
							}
						}
						else
						{
							Skill skill3 = skillTemplate.skills[0];
							mFont.tahoma_7b_green.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
							{
								mResources.need_upper,
								" ",
								Res.formatNumber2(skill3.powRequire),
								" ",
								mResources.potential_to_learn
							}), num2 + 5, num3 + 15, 0);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00079924 File Offset: 0x00077B24
	private void paintMapTrans(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.mapNames.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(this.xScroll, num2, this.wScroll, h);
					mFont.tahoma_7b_blue.drawString(g, this.mapNames[i], 5, num2 + 1, 0);
					mFont.tahoma_7_grey.drawString(g, this.planetNames[i], 5, num2 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00079A94 File Offset: 0x00077C94
	private void paintZone(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int[] zones = GameScr.gI().zones;
		int[] pts = GameScr.gI().pts;
		for (int i = 0; i < pts.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int y = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 34;
			int h2 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					g.setColor(this.zoneColor[pts[i]]);
					g.fillRect(num4, y, num5, h2);
					bool flag3 = zones[i] != -1;
					if (flag3)
					{
						bool flag4 = pts[i] != 1;
						if (flag4)
						{
							mFont.tahoma_7_yellow.drawString(g, zones[i].ToString() + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
						}
						else
						{
							mFont.tahoma_7_grey.drawString(g, zones[i].ToString() + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
						}
						mFont.tahoma_7_green2.drawString(g, GameScr.gI().numPlayer[i].ToString() + "/" + GameScr.gI().maxPlayer[i].ToString(), num + 5, num2 + 6, 0);
					}
					bool flag5 = GameScr.gI().rankName1[i] != null;
					if (flag5)
					{
						mFont.tahoma_7_grey.drawString(g, string.Concat(new object[]
						{
							GameScr.gI().rankName1[i],
							"(Top ",
							GameScr.gI().rank1[i],
							")"
						}), num + num3 - 2, num2 + 1, mFont.RIGHT);
						mFont.tahoma_7_grey.drawString(g, string.Concat(new object[]
						{
							GameScr.gI().rankName2[i],
							"(Top ",
							GameScr.gI().rank2[i],
							")"
						}), num + num3 - 2, num2 + 11, mFont.RIGHT);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00079DA8 File Offset: 0x00077FA8
	private void paintSpeacialSkill(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (!flag)
		{
			int num = (this.cmy + this.hScroll) / 24 + 1;
			bool flag2 = num < this.hScroll / 24 + 1;
			if (flag2)
			{
				num = this.hScroll / 24 + 1;
			}
			bool flag3 = num > this.currentListLength;
			if (flag3)
			{
				num = this.currentListLength;
			}
			int num2 = this.cmy / 24;
			bool flag4 = num2 >= num;
			if (flag4)
			{
				num2 = num - 1;
			}
			bool flag5 = num2 < 0;
			if (flag5)
			{
				num2 = 0;
			}
			for (int i = num2; i < num; i++)
			{
				int num3 = this.xScroll;
				int num4 = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 24;
				int num6 = this.ITEM_HEIGHT - 1;
				int num7 = this.xScroll + num5;
				int num8 = this.yScroll + i * this.ITEM_HEIGHT;
				int num9 = this.wScroll - num5;
				int h = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num7, num8, num9, h);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num3, num4, num5, num6);
				SmallImage.drawSmallImage(g, (int)global::Char.myCharz().imgSpeacialSkill[this.currentTabIndex][i], num3 + num5 / 2, num4 + num6 / 2, 0, 3);
				string[] array = mFont.tahoma_7_grey.splitFontArray(global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex][i], 140);
				for (int j = 0; j < array.Length; j++)
				{
					mFont.tahoma_7_grey.drawString(g, array[j], num7 + 5, num8 + 1 + j * 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00079FD8 File Offset: 0x000781D8
	private void paintBox(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		try
		{
			Item[] arrItemBox = global::Char.myCharz().arrItemBox;
			this.currentListLength = this.checkCurrentListLength(arrItemBox.Length);
			int num = arrItemBox.Length / 20 + ((arrItemBox.Length % 20 <= 0) ? 0 : 1);
			this.TAB_W_NEW = this.wScroll / num;
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num2 = this.xScroll + 36;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 36;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				bool flag = num3 - this.cmy <= this.yScroll + this.hScroll;
				if (flag)
				{
					bool flag2 = num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
					if (flag2)
					{
						bool flag3 = i == 0;
						if (flag3)
						{
							for (int j = 0; j < num; j++)
							{
								int num9 = (j != this.newSelected || this.selected != 0) ? 0 : ((GameCanvas.gameTick % 10 >= 7) ? 0 : -1);
								g.setColor((j != this.newSelected) ? 15723751 : 16383818);
								g.fillRect(this.xScroll + j * this.TAB_W_NEW, num3 + 9 + num9, this.TAB_W_NEW - 1, 14);
								mFont.tahoma_7_grey.drawString(g, string.Empty + j.ToString(), this.xScroll + j * this.TAB_W_NEW + this.TAB_W_NEW / 2, this.yScroll + 11 + num9, mFont.CENTER);
							}
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
							g.fillRect(num2, num3, num4, h);
							g.setColor((i != this.selected) ? 9993045 : 9541120);
							int inventorySelect_body = this.GetInventorySelect_body(i, this.newSelected);
							Item item = arrItemBox[inventorySelect_body];
							bool flag4 = item != null;
							if (flag4)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									bool flag5 = item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0;
									if (flag5)
									{
										sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
										int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
										bool flag6 = color_ItemBg != -1;
										if (flag6)
										{
											g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
								}
							}
							g.fillRect(num5, num6, num7, num8);
							bool flag7 = item != null;
							if (flag7)
							{
								string str = string.Empty;
								mFont mFont = mFont.tahoma_7_green2;
								bool flag8 = item.itemOption != null;
								if (flag8)
								{
									for (int l = 0; l < item.itemOption.Length; l++)
									{
										bool flag9 = item.itemOption[l].optionTemplate.id == 72;
										if (flag9)
										{
											str = " [+" + item.itemOption[l].getOptionString() + "]";
										}
										bool flag10 = item.itemOption[l].optionTemplate.id == 41;
										if (flag10)
										{
											bool flag11 = item.itemOption[l].param == 1;
											if (flag11)
											{
												mFont = Panel.GetFont(0);
											}
											else
											{
												bool flag12 = item.itemOption[l].param == 2;
												if (flag12)
												{
													mFont = Panel.GetFont(2);
												}
												else
												{
													bool flag13 = item.itemOption[l].param == 3;
													if (flag13)
													{
														mFont = Panel.GetFont(8);
													}
													else
													{
														bool flag14 = item.itemOption[l].param == 4;
														if (flag14)
														{
															mFont = Panel.GetFont(7);
														}
													}
												}
											}
										}
									}
								}
								mFont.drawString(g, item.template.name + str, num2 + 5, num3 + 1, 0);
								string text = string.Empty;
								bool flag15 = item.itemOption != null;
								if (flag15)
								{
									bool flag16 = item.itemOption.Length != 0 && item.itemOption[0] != null;
									if (flag16)
									{
										text += item.itemOption[0].getOptionString();
									}
									mFont mFont2 = mFont.tahoma_7_blue;
									bool flag17 = item.compare < 0 && item.template.type != 5;
									if (flag17)
									{
										mFont2 = mFont.tahoma_7_red;
									}
									bool flag18 = item.itemOption.Length > 1;
									if (flag18)
									{
										for (int m = 1; m < item.itemOption.Length; m++)
										{
											bool flag19 = item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107;
											if (flag19)
											{
												text = text + "," + item.itemOption[m].getOptionString();
											}
										}
									}
									mFont2.drawString(g, text, num2 + 5, num3 + 11, mFont.LEFT);
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								bool flag20 = item.itemOption != null;
								if (flag20)
								{
									for (int n = 0; n < item.itemOption.Length; n++)
									{
										this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
									}
									for (int num10 = 0; num10 < item.itemOption.Length; num10++)
									{
										this.paintOptSlotItem(g, item.itemOption[num10].optionTemplate.id, item.itemOption[num10].param, num5, num6, num7, num8);
									}
								}
								bool flag21 = item.quantity > 1;
								if (flag21)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0007A730 File Offset: 0x00078930
	public Member getCurrMember()
	{
		bool flag = this.selected < 2;
		Member result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.selected > ((this.member == null) ? this.myMember.size() : this.member.size()) + 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = ((this.member == null) ? ((Member)this.myMember.elementAt(this.selected - 2)) : ((Member)this.member.elementAt(this.selected - 2)));
			}
		}
		return result;
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0007A7C4 File Offset: 0x000789C4
	public ClanMessage getCurrMessage()
	{
		bool flag = this.selected < 2;
		ClanMessage result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.selected > ClanMessage.vMessage.size() + 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = (ClanMessage)ClanMessage.vMessage.elementAt(this.selected - 2);
			}
		}
		return result;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0007A81C File Offset: 0x00078A1C
	public Clan getCurrClan()
	{
		bool flag = this.selected < 2;
		Clan result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.selected > this.clans.Length + 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = this.clans[this.selected - 2];
			}
		}
		return result;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0007A86C File Offset: 0x00078A6C
	private void paintLogChat(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.logChat.size() == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_msg, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2 + 24, 2);
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll + num3;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.wScroll - num3;
			int num7 = this.ITEM_HEIGHT - 1;
			bool flag2 = i == 0;
			if (flag2)
			{
				g.setColor(15196114);
				g.fillRect(num, num5, this.wScroll, num7);
				g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num5 + 2, StaticObj.TOP_RIGHT);
				((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, (!this.isViewChatServer) ? mResources.on : mResources.off, this.xScroll + this.wScroll - 22, num5 + 7, 2);
				mFont.tahoma_7_grey.drawString(g, (!this.isViewChatServer) ? mResources.onPlease : mResources.offPlease, this.xScroll + 5, num5 + num7 / 2 - 4, mFont.LEFT);
			}
			else
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, num7);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.logChat.elementAt(i - 1);
				bool flag3 = infoItem.charInfo.headICON != -1;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				mFont mFont = mFont.tahoma_7b_dark;
				mFont = mFont.tahoma_7b_green2;
				mFont.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				bool flag4 = !infoItem.isChatServer;
				if (flag4)
				{
					mFont.tahoma_7_blue.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_red.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0007AC28 File Offset: 0x00078E28
	private void paintFlagChange(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 26;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 26;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = 24;
			int num7 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					g.setColor((i != this.selected) ? 9993045 : 9541120);
					g.fillRect(num4, num5, num6, num7);
					Item item = (Item)this.vFlag.elementAt(i);
					bool flag3 = item != null;
					if (flag3)
					{
						mFont.tahoma_7_green2.drawString(g, item.template.name, num + 5, num2 + 1, 0);
						string text = string.Empty;
						bool flag4 = item.itemOption != null && item.itemOption.Length >= 1;
						if (flag4)
						{
							bool flag5 = item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
							if (flag5)
							{
								text += item.itemOption[0].getOptionString();
							}
							mFont tahoma_7_blue = mFont.tahoma_7_blue;
							tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0007AE90 File Offset: 0x00079090
	private void paintEnemy(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_enemy, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
		}
		else
		{
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int h2 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, h2);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.vEnemy.elementAt(i);
				bool flag2 = infoItem.charInfo.headICON != -1;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				bool isOnline = infoItem.isOnline;
				if (isOnline)
				{
					mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0007B15C File Offset: 0x0007935C
	private void paintFriend(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_friend, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
		}
		else
		{
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int h2 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, h2);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.vFriend.elementAt(i);
				bool flag2 = infoItem.charInfo.headICON != -1;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				bool isOnline = infoItem.isOnline;
				if (isOnline)
				{
					mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0007B428 File Offset: 0x00079628
	public void paintPlayerMenu(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.vPlayerMenu.size(); i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					Command command = (Command)this.vPlayerMenu.elementAt(i);
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					bool flag3 = command.caption2.Equals(string.Empty);
					if (flag3)
					{
						mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 1, mFont.CENTER);
						mFont.tahoma_7b_dark.drawString(g, command.caption2, this.xScroll + this.wScroll / 2, num + 11, mFont.CENTER);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0007B5DC File Offset: 0x000797DC
	private void paintClans(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmx, -this.cmy);
		g.setColor(0);
		int num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
		bool flag = this.currentListLength == 2;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, this.clanReport, this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			bool flag2 = this.isMessage && this.myMember.size() == 1;
			if (flag2)
			{
				for (int i = 0; i < mResources.clanEmpty.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, mResources.clanEmpty[i], this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mResources.clanEmpty.Length * 12 / 2 + i * 12, mFont.CENTER);
				}
			}
		}
		bool flag3 = this.isMessage;
		if (flag3)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		for (int j = 0; j < this.currentListLength; j++)
		{
			int num2 = this.xScroll;
			int num3 = this.yScroll + j * this.ITEM_HEIGHT;
			int num4 = 24;
			int num5 = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll + num4;
			int num7 = this.yScroll + j * this.ITEM_HEIGHT;
			int num8 = this.wScroll - num4;
			int num9 = this.ITEM_HEIGHT - 1;
			bool flag4 = num7 - this.cmy <= this.yScroll + this.hScroll;
			if (flag4)
			{
				bool flag5 = num7 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag5)
				{
					bool flag6 = j == 0;
					if (flag6)
					{
						for (int k = 0; k < this.clansOption.Length; k++)
						{
							g.setColor((k != this.cSelected || j != this.selected) ? 15723751 : 16383818);
							g.fillRect(num + k * this.TAB_W, num7, this.TAB_W - 1, 23);
							for (int l = 0; l < this.clansOption[k].Length; l++)
							{
								mFont.tahoma_7_grey.drawString(g, this.clansOption[k][l], num + k * this.TAB_W + this.TAB_W / 2, this.yScroll + l * 11, mFont.CENTER);
							}
						}
					}
					else
					{
						bool flag7 = j == 1;
						if (flag7)
						{
							g.setColor((j != this.selected) ? 15196114 : 16383818);
							g.fillRect(this.xScroll, num7, this.wScroll, num9);
							bool flag8 = this.clanInfo != null;
							if (flag8)
							{
								mFont.tahoma_7b_dark.drawString(g, this.clanInfo, this.xScroll + this.wScroll / 2, num7 + 6, mFont.CENTER);
							}
						}
						else
						{
							bool flag9 = this.isSearchClan;
							if (flag9)
							{
								bool flag10 = this.clans != null;
								if (flag10)
								{
									bool flag11 = this.clans.Length != 0;
									if (flag11)
									{
										g.setColor((j != this.selected) ? 15196114 : 16383818);
										g.fillRect(num6, num7, num8, num9);
										g.setColor((j != this.selected) ? 9993045 : 9541120);
										g.fillRect(num2, num3, num4, num5);
										bool flag12 = ClanImage.isExistClanImage(this.clans[j - 2].imgID);
										if (flag12)
										{
											bool flag13 = ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage != null;
											if (flag13)
											{
												SmallImage.drawSmallImage(g, (int)ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage[0], num2 + num4 / 2, num3 + num5 / 2, 0, StaticObj.VCENTER_HCENTER);
											}
										}
										else
										{
											ClanImage clanImage = new ClanImage();
											clanImage.ID = this.clans[j - 2].imgID;
											bool flag14 = !ClanImage.isExistClanImage(clanImage.ID);
											if (flag14)
											{
												ClanImage.addClanImage(clanImage);
											}
										}
										string st = (this.clans[j - 2].name.Length <= 23) ? this.clans[j - 2].name : (this.clans[j - 2].name.Substring(0, 23) + "...");
										mFont.tahoma_7b_green2.drawString(g, st, num6 + 5, num7, 0);
										g.setClip(num6, num7, num8 - 10, num9);
										mFont.tahoma_7_blue.drawString(g, this.clans[j - 2].slogan, num6 + 5, num7 + 11, 0);
										g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
										mFont.tahoma_7_green2.drawString(g, this.clans[j - 2].currMember.ToString() + "/" + this.clans[j - 2].maxMember.ToString(), num6 + num8 - 5, num7, mFont.RIGHT);
									}
								}
							}
							else
							{
								bool flag15 = this.isViewMember;
								if (flag15)
								{
									g.setColor((j != this.selected) ? 15196114 : 16383818);
									g.fillRect(num6, num7, num8, num9);
									g.setColor((j != this.selected) ? 9993045 : 9541120);
									g.fillRect(num2, num3, num4, num5);
									Member member = (this.member == null) ? ((Member)this.myMember.elementAt(j - 2)) : ((Member)this.member.elementAt(j - 2));
									bool flag16 = member.headICON != -1;
									if (flag16)
									{
										SmallImage.drawSmallImage(g, (int)member.headICON, num2, num3, 0, 0);
									}
									else
									{
										Part part = GameScr.parts[(int)member.head];
										SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num3 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
									}
									g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
									mFont mFont = mFont.tahoma_7b_dark;
									bool flag17 = member.role == 0;
									if (flag17)
									{
										mFont = mFont.tahoma_7b_red;
									}
									else
									{
										bool flag18 = member.role == 1;
										if (flag18)
										{
											mFont = mFont.tahoma_7b_green;
										}
										else
										{
											bool flag19 = member.role == 2;
											if (flag19)
											{
												mFont = mFont.tahoma_7b_green2;
											}
										}
									}
									mFont.drawString(g, member.name, num6 + 5, num7, 0);
									mFont.tahoma_7_blue.drawString(g, mResources.power + ": " + member.powerPoint, num6 + 5, num7 + 11, 0);
									SmallImage.drawSmallImage(g, 7223, num6 + num8 - 7, num7 + 12, 0, 3);
									mFont.tahoma_7_blue.drawString(g, string.Empty + member.clanPoint.ToString(), num6 + num8 - 15, num7 + 6, mFont.RIGHT);
								}
								else
								{
									bool flag20 = this.isMessage && ClanMessage.vMessage.size() != 0;
									if (flag20)
									{
										ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(j - 2);
										g.setColor((j != this.selected || clanMessage.option != null) ? 15196114 : 16383818);
										g.fillRect(num2, num3, num8 + num4, num9);
										clanMessage.paint(g, num2, num3);
										bool flag21 = clanMessage.option != null;
										if (flag21)
										{
											int num10 = this.xScroll + this.wScroll - 2 - clanMessage.option.Length * 40;
											for (int m = 0; m < clanMessage.option.Length; m++)
											{
												bool flag22 = m == this.cSelected && j == this.selected;
												if (flag22)
												{
													g.drawImage(GameScr.imgLbtnFocus2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
													mFont.tahoma_7b_green2.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
												}
												else
												{
													g.drawImage(GameScr.imgLbtn2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
													mFont.tahoma_7b_dark.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0007BFA8 File Offset: 0x0007A1A8
	private void paintArchivement(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_mission, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
		}
		else
		{
			bool flag2 = global::Char.myCharz().arrArchive == null;
			if (!flag2)
			{
				bool flag3 = global::Char.myCharz().arrArchive.Length != this.currentListLength;
				if (!flag3)
				{
					for (int i = 0; i < this.currentListLength; i++)
					{
						int num = this.xScroll;
						int num2 = this.yScroll + i * this.ITEM_HEIGHT;
						int num3 = this.wScroll;
						int num4 = this.ITEM_HEIGHT - 1;
						Archivement archivement = global::Char.myCharz().arrArchive[i];
						g.setColor((i != this.selected || ((archivement.isRecieve || archivement.isFinish) && (!archivement.isRecieve || !archivement.isFinish))) ? 15196114 : 16383818);
						g.fillRect(num, num2, num3, num4);
						bool flag4 = archivement != null;
						if (flag4)
						{
							bool flag5 = !archivement.isFinish;
							if (flag5)
							{
								mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
								mFont.tahoma_7_green.drawString(g, archivement.money.ToString() + " " + mResources.RUBY, num + num3 - 5, num2, mFont.RIGHT);
								mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
							}
							else
							{
								bool flag6 = archivement.isFinish && !archivement.isRecieve;
								if (flag6)
								{
									mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
									mFont.tahoma_7_blue.drawString(g, string.Concat(new object[]
									{
										mResources.reward_mission,
										archivement.money,
										" ",
										mResources.RUBY
									}), num + 5, num2 + 11, 0);
									bool flag7 = i == this.selected;
									if (flag7)
									{
										mFont.tahoma_7b_green2.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
										mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
									}
									else
									{
										g.drawImage(GameScr.imgLbtn2, num + num3 - 20, num2 + num4 / 2, StaticObj.VCENTER_HCENTER);
										mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
									}
								}
								else
								{
									bool flag8 = archivement.isFinish && archivement.isRecieve;
									if (flag8)
									{
										mFont.tahoma_7_green.drawString(g, archivement.info1, num + 5, num2, 0);
										mFont.tahoma_7_green.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
									}
								}
							}
						}
					}
					this.paintScrollArrow(g);
				}
			}
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0007C328 File Offset: 0x0007A528
	private void paintCombine(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		bool flag = this.vItemCombine.size() == 0;
		if (flag)
		{
			bool flag2 = this.combineInfo != null;
			if (flag2)
			{
				for (int i = 0; i < this.combineInfo.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, this.combineInfo[i], this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - this.combineInfo.Length * 14 / 2 + i * 14 + 5, 2);
				}
			}
		}
		else
		{
			for (int j = 0; j < this.vItemCombine.size() + 1; j++)
			{
				int num = this.xScroll + 36;
				int num2 = this.yScroll + j * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 36;
				int num4 = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + j * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				bool flag3 = num2 - this.cmy <= this.yScroll + this.hScroll;
				if (flag3)
				{
					bool flag4 = num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
					if (flag4)
					{
						bool flag5 = j == this.vItemCombine.size();
						if (flag5)
						{
							bool flag6 = this.vItemCombine.size() > 0;
							if (flag6)
							{
								bool flag7 = !GameCanvas.isTouch && j == this.selected;
								if (flag7)
								{
									g.setColor(16383818);
									g.fillRect(num5, num2, this.wScroll, num4 + 2);
								}
								bool flag8 = (j == this.selected && this.keyTouchCombine == 1) || (!GameCanvas.isTouch && j == this.selected);
								if (flag8)
								{
									g.drawImage(GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
									mFont.tahoma_7b_green2.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
								else
								{
									g.drawImage(GameScr.imgLbtn, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
									mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
							}
						}
						else
						{
							g.setColor((j != this.selected) ? 15196114 : 16383818);
							g.fillRect(num, num2, num3, num4);
							g.setColor((j != this.selected) ? 9993045 : 9541120);
							Item item = (Item)this.vItemCombine.elementAt(j);
							bool flag9 = item != null;
							if (flag9)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									bool flag10 = item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0;
									if (flag10)
									{
										sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
										int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
										bool flag11 = color_ItemBg != -1;
										if (flag11)
										{
											g.setColor((j != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
								}
							}
							g.fillRect(num5, num6, num7, num8);
							bool flag12 = item != null;
							if (flag12)
							{
								string str = string.Empty;
								mFont mFont = mFont.tahoma_7_green2;
								bool flag13 = item.itemOption != null;
								if (flag13)
								{
									for (int l = 0; l < item.itemOption.Length; l++)
									{
										bool flag14 = item.itemOption[l].optionTemplate.id == 72;
										if (flag14)
										{
											str = " [+" + item.itemOption[l].param.ToString() + "]";
										}
										bool flag15 = item.itemOption[l].optionTemplate.id == 41;
										if (flag15)
										{
											bool flag16 = item.itemOption[l].param == 1;
											if (flag16)
											{
												mFont = Panel.GetFont(0);
											}
											else
											{
												bool flag17 = item.itemOption[l].param == 2;
												if (flag17)
												{
													mFont = Panel.GetFont(2);
												}
												else
												{
													bool flag18 = item.itemOption[l].param == 3;
													if (flag18)
													{
														mFont = Panel.GetFont(8);
													}
													else
													{
														bool flag19 = item.itemOption[l].param == 4;
														if (flag19)
														{
															mFont = Panel.GetFont(7);
														}
													}
												}
											}
										}
									}
								}
								mFont.drawString(g, item.template.name + str, num + 5, num2 + 1, 0);
								string text = string.Empty;
								bool flag20 = item.itemOption != null;
								if (flag20)
								{
									bool flag21 = item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
									if (flag21)
									{
										text += item.itemOption[0].getOptionString();
									}
									mFont mFont2 = mFont.tahoma_7_blue;
									bool flag22 = item.compare < 0 && item.template.type != 5;
									if (flag22)
									{
										mFont2 = mFont.tahoma_7_red;
									}
									bool flag23 = item.itemOption.Length > 1;
									if (flag23)
									{
										for (int m = 1; m < item.itemOption.Length; m++)
										{
											bool flag24 = item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107;
											if (flag24)
											{
												text = text + "," + item.itemOption[m].getOptionString();
											}
										}
									}
									mFont2.drawString(g, text, num + 5, num2 + 11, mFont.LEFT);
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								bool flag25 = item.itemOption != null;
								if (flag25)
								{
									for (int n = 0; n < item.itemOption.Length; n++)
									{
										this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
									}
									for (int num9 = 0; num9 < item.itemOption.Length; num9++)
									{
										this.paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
									}
								}
								bool flag26 = item.quantity > 1;
								if (flag26)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0007CB64 File Offset: 0x0007AD64
	private void paintInventory(mGraphics g)
	{
		bool flag = true;
		bool flag3 = flag && this.isnewInventory;
		if (flag3)
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			g.setColor(16711680);
			int num = arrItemBody.Length + arrItemBag.Length;
			int num2 = num / 20 + ((num % 20 <= 0) ? 0 : 1) + 1;
			int num3 = 0;
			this.TAB_W_NEW = this.wScroll / num2;
			for (int i = num3; i < num2; i++)
			{
				int num4 = (i != this.newSelected || this.selected != 0) ? 0 : ((GameCanvas.gameTick % 10 >= 7) ? 0 : -1);
				g.setColor((i != this.newSelected) ? 15723751 : 16383818);
				g.fillRect(this.xScroll + i * this.TAB_W_NEW, 89 + num4 - 10, this.TAB_W_NEW - 1, 21);
				bool flag4 = i == this.newSelected;
				if (flag4)
				{
					g.setColor(13524492);
					int x = this.xScroll + i * this.TAB_W_NEW;
					int num5 = 89 + num4 - 10 + 21;
					g.fillRect(x, num5 - 3, this.TAB_W_NEW - 1, 3);
				}
				mFont.tahoma_7_grey.drawString(g, string.Empty + (i + 1).ToString(), this.xScroll + i * this.TAB_W_NEW + this.TAB_W_NEW / 2, 91 + num4 - 10, mFont.CENTER);
			}
			num3 = 1;
			int num6 = this.xScroll;
			int num7 = this.yScroll + num3 * this.ITEM_HEIGHT;
			int num8 = 34;
			int num9 = this.ITEM_HEIGHT - 1;
			for (int j = 0; j < 4; j++)
			{
				num6 = this.xScroll;
				num7 = this.yScroll + (j + num3) * this.ITEM_HEIGHT;
				bool flag2 = true;
				int k = 0;
				while (k < 5)
				{
					bool flag5 = this.newSelected > 0;
					if (flag5)
					{
						int num10 = (this.newSelected - 1) * 20;
						bool flag6 = j * 5 + k + num10 >= arrItemBag.Length;
						if (flag6)
						{
							break;
						}
						Item item = arrItemBag[j * 5 + k + num10];
						num6 = this.xScroll + num8 * k;
						int num11 = this.sellectInventory % 5;
						int num12 = this.sellectInventory / 5;
						bool flag7 = this.newSelected > 0;
						if (flag7)
						{
							g.setColor(15196114);
						}
						else
						{
							g.setColor(9993045);
						}
						g.drawRect(num6, num7, num8, num9);
						bool flag8 = j == num12 && k == num11 && this.selected > 0;
						if (flag8)
						{
							g.setColor(16383818);
							this.itemInvenNew = item;
						}
						g.fillRect(num6 + 2, num7 + 2, num8 - 3, num9 - 3);
						bool flag9 = item != null;
						if (flag9)
						{
							int x2 = num6 + Panel.imgNew.getWidth() / 2;
							int y = num7;
							int num13 = 34;
							int h = this.ITEM_HEIGHT - 1;
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
							bool flag10 = item.quantity > 1;
							if (flag10)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num6, num7 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
							bool flag11 = item.newItem && GameCanvas.gameTick % 10 > 5;
							if (flag11)
							{
								g.drawImage(Panel.imgNew, x2, y, 3);
							}
							for (int l = 0; l < item.itemOption.Length; l++)
							{
								this.paintOptSlotItem(g, item.itemOption[l].optionTemplate.id, item.itemOption[l].param, x2, y, num13, h);
							}
						}
						bool flag12 = !flag2;
						if (flag12)
						{
							break;
						}
						k++;
					}
					else
					{
						bool flag13 = j * 5 + k < arrItemBody.Length;
						if (flag13)
						{
							Item item2 = arrItemBody[j * 5 + k];
							break;
						}
						break;
					}
				}
			}
			num3 = ((this.newSelected != 0) ? 5 : 3);
			int num14 = this.yScroll + num3 * this.ITEM_HEIGHT + 5;
			bool flag14 = this.newSelected == 0;
			if (flag14)
			{
			}
			num6 = this.xScroll;
			num7 = this.yScroll + num3 * this.ITEM_HEIGHT;
			num8 = 34;
			num9 = this.ITEM_HEIGHT - 1;
			bool flag15 = this.newSelected == 0;
			if (flag15)
			{
				g.setColor(15196114);
				num3 = 1;
				this.nTableItem = 10;
				bool flag16 = this.eBanner != null;
				if (flag16)
				{
					this.eBanner.paint(g);
					this.eBanner.x = num6 + 34 + 34;
					this.eBanner.y = num7 + num9 - 25;
				}
				for (int m = 0; m < 10; m++)
				{
					Item item3 = arrItemBody[m];
					bool flag17 = m < 5;
					if (flag17)
					{
						num6 = this.xScroll;
						num7 = this.yScroll + (m + num3) * this.ITEM_HEIGHT;
					}
					else
					{
						int num15 = 5;
						num6 = this.xScroll + 4 * num8;
						num7 = this.yScroll + (m - num15 + num3) * this.ITEM_HEIGHT;
					}
					g.setColor(15196114);
					g.drawRect(num6, num7, num8, num9);
					bool flag18 = this.sellectInventory == m;
					if (flag18)
					{
						this.itemInvenNew = item3;
						g.setColor(16383818);
					}
					else
					{
						g.setColor(9993045);
					}
					g.fillRect(num6 + 2, num7 + 2, num8 - 3, num9 - 3);
					bool flag19 = item3 == null;
					if (flag19)
					{
						Panel.screenTab6.drawFrame(m, num6 + num8 / 2 - 8, num7 + num9 / 2 - 8, 0, mGraphics.TOP | mGraphics.LEFT, g);
					}
					bool flag20 = item3 != null;
					if (flag20)
					{
						SmallImage.drawSmallImage(g, (int)item3.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						bool flag21 = item3.quantity > 1;
						if (flag21)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item3.quantity.ToString(), num6 + 4 * num8, num7 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
				}
				num3 = 1;
				num6 = this.xScroll + 34;
				num7 = this.yScroll + num3 * this.ITEM_HEIGHT;
				num9 = 4 * (this.ITEM_HEIGHT - 1);
				global::Char.myCharz().paintCharBody(g, num6 + 34 + 17, num7 + num9 - 25, 1, 0, true);
				num3 = 3;
				int num16 = 2;
				num6 = this.xScroll + 34;
				num7 = this.yScroll + (1 + num3) * this.ITEM_HEIGHT - 1;
				num8 = 102;
				num9 = this.ITEM_HEIGHT * num16;
				g.setColor(15196114);
				g.drawRect(num6, num7, num8, num9);
				g.setColor(9993045);
				g.fillRect(num6 + 1, num7 + 1, num8 - 2, num9 - 2);
				this.paintItemBodyBagInfo(g, num6 + 3, num7 - 2);
				num3 = ((this.newSelected != 0) ? 5 : 6);
				num14 = this.yScroll + num3 * this.ITEM_HEIGHT;
				g.setColor(15196114);
				bool flag22 = this.newSelected == 0;
				if (flag22)
				{
					num16 = 1;
				}
				g.drawRect(this.xScroll, num14, this.wScroll, this.ITEM_HEIGHT * num16);
				g.setColor(16777215);
				g.fillRect(this.xScroll + 1, num14 + 1, this.wScroll - 2, this.ITEM_HEIGHT * num16 - 2);
			}
			bool flag23 = this.itemInvenNew != null && this.itemInvenNew.itemOption != null;
			if (flag23)
			{
				string str = string.Empty;
				mFont mFont = mFont.tahoma_7_green2;
				bool flag24 = this.itemInvenNew.itemOption != null;
				if (flag24)
				{
					for (int n = 0; n < this.itemInvenNew.itemOption.Length; n++)
					{
						bool flag25 = this.itemInvenNew.itemOption[n].optionTemplate.id == 72;
						if (flag25)
						{
							str = " [+" + this.itemInvenNew.itemOption[n].param.ToString() + "]";
						}
						bool flag26 = this.itemInvenNew.itemOption[n].optionTemplate.id == 41;
						if (flag26)
						{
							bool flag27 = this.itemInvenNew.itemOption[n].param == 1;
							if (flag27)
							{
								mFont = Panel.GetFont(0);
							}
							else
							{
								bool flag28 = this.itemInvenNew.itemOption[n].param == 2;
								if (flag28)
								{
									mFont = Panel.GetFont(2);
								}
								else
								{
									bool flag29 = this.itemInvenNew.itemOption[n].param == 3;
									if (flag29)
									{
										mFont = Panel.GetFont(8);
									}
									else
									{
										bool flag30 = this.itemInvenNew.itemOption[n].param == 4;
										if (flag30)
										{
											mFont = Panel.GetFont(7);
										}
									}
								}
							}
						}
					}
				}
				mFont.drawString(g, this.itemInvenNew.template.name + str, this.xScroll + 5, num14 + 1, 0);
				string text = string.Empty;
				bool flag31 = this.itemInvenNew.itemOption != null;
				if (flag31)
				{
					bool flag32 = this.itemInvenNew.itemOption.Length != 0 && this.itemInvenNew.itemOption[0] != null && this.itemInvenNew.itemOption[0].optionTemplate.id != 102 && this.itemInvenNew.itemOption[0].optionTemplate.id != 107;
					if (flag32)
					{
						text += this.itemInvenNew.itemOption[0].getOptionString();
					}
					mFont mFont2 = mFont.tahoma_7_blue;
					bool flag33 = this.itemInvenNew.compare < 0 && this.itemInvenNew.template.type != 5;
					if (flag33)
					{
						mFont2 = mFont.tahoma_7_red;
					}
					bool flag34 = this.itemInvenNew.itemOption.Length > 1;
					if (flag34)
					{
						for (int num17 = 1; num17 < 2; num17++)
						{
							bool flag35 = this.itemInvenNew.itemOption[num17] != null && this.itemInvenNew.itemOption[num17].optionTemplate.id != 102 && this.itemInvenNew.itemOption[num17].optionTemplate.id != 107;
							if (flag35)
							{
								text = text + "," + this.itemInvenNew.itemOption[num17].getOptionString();
							}
						}
					}
					try
					{
						bool flag36 = mFont2.getWidth(text) > this.wScroll;
						if (flag36)
						{
							text = mFont2.splitFontArray(text, this.wScroll)[0];
						}
					}
					catch (Exception ex)
					{
					}
					mFont2.drawString(g, text, this.xScroll + 5, num14 + 11, mFont.LEFT);
				}
			}
		}
		bool flag37 = flag && this.isnewInventory;
		if (!flag37)
		{
			g.setColor(16711680);
			Item[] arrItemBody2 = global::Char.myCharz().arrItemBody;
			Item[] arrItemBag2 = global::Char.myCharz().arrItemBag;
			this.currentListLength = this.checkCurrentListLength(arrItemBody2.Length + arrItemBag2.Length);
			int num18 = (arrItemBody2.Length + arrItemBag2.Length) / 20 + (((arrItemBody2.Length + arrItemBag2.Length) % 20 <= 0) ? 0 : 1);
			this.TAB_W_NEW = this.wScroll / num18;
			for (int num19 = 0; num19 < num18; num19++)
			{
				int num20 = (num19 != this.newSelected || this.selected != 0) ? 0 : ((GameCanvas.gameTick % 10 >= 7) ? 0 : -1);
				g.setColor((num19 != this.newSelected) ? 15723751 : 16383818);
				g.fillRect(this.xScroll + num19 * this.TAB_W_NEW, 89 + num20 - 10, this.TAB_W_NEW - 1, 21);
				bool flag38 = num19 == this.newSelected;
				if (flag38)
				{
					g.setColor(13524492);
					int x3 = this.xScroll + num19 * this.TAB_W_NEW;
					int num21 = 89 + num20 - 10 + 21;
					g.fillRect(x3, num21 - 3, this.TAB_W_NEW - 1, 3);
				}
				mFont.tahoma_7_grey.drawString(g, string.Empty + (num19 + 1).ToString(), this.xScroll + num19 * this.TAB_W_NEW + this.TAB_W_NEW / 2, 91 + num20 - 10, mFont.CENTER);
			}
			g.setClip(this.xScroll, this.yScroll + 21, this.wScroll, this.hScroll - 21);
			g.translate(0, -this.cmy);
			try
			{
				for (int num22 = 1; num22 < this.currentListLength; num22++)
				{
					int num23 = this.xScroll + 36;
					int num24 = this.yScroll + num22 * this.ITEM_HEIGHT;
					int num25 = this.wScroll - 36;
					int h2 = this.ITEM_HEIGHT - 1;
					int num26 = this.xScroll;
					int num27 = this.yScroll + num22 * this.ITEM_HEIGHT;
					int num28 = 34;
					int num29 = this.ITEM_HEIGHT - 1;
					bool flag39 = num24 - this.cmy <= this.yScroll + this.hScroll;
					if (flag39)
					{
						bool flag40 = num24 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
						if (flag40)
						{
							bool inventorySelect_isbody = this.GetInventorySelect_isbody(num22, this.newSelected, global::Char.myCharz().arrItemBody);
							int inventorySelect_body = this.GetInventorySelect_body(num22, this.newSelected);
							int inventorySelect_bag = this.GetInventorySelect_bag(num22, this.newSelected, global::Char.myCharz().arrItemBody);
							g.setColor((num22 != this.selected) ? ((!inventorySelect_isbody) ? 15723751 : 15196114) : 16383818);
							g.fillRect(num23, num24, num25, h2);
							g.setColor((num22 != this.selected) ? ((!inventorySelect_isbody) ? 11837316 : 9993045) : 9541120);
							Item item4 = (!inventorySelect_isbody) ? arrItemBag2[inventorySelect_bag] : arrItemBody2[inventorySelect_body];
							bool flag41 = item4 != null;
							if (flag41)
							{
								for (int num30 = 0; num30 < item4.itemOption.Length; num30++)
								{
									bool flag42 = item4.itemOption[num30].optionTemplate.id == 72 && item4.itemOption[num30].param > 0;
									if (flag42)
									{
										byte id = (byte)Panel.GetColor_Item_Upgrade(item4.itemOption[num30].param);
										int color_ItemBg = Panel.GetColor_ItemBg((int)id);
										bool flag43 = color_ItemBg != -1;
										if (flag43)
										{
											g.setColor((num22 != this.selected) ? Panel.GetColor_ItemBg((int)id) : Panel.GetColor_ItemBg((int)id));
										}
									}
								}
							}
							g.fillRect(num26, num27, num28, num29);
							bool flag44 = item4 != null && item4.isSelect && GameCanvas.panel.type == 12;
							if (flag44)
							{
								g.setColor((num22 != this.selected) ? 6047789 : 7040779);
								g.fillRect(num26, num27, num28, num29);
							}
							bool flag45 = item4 != null;
							if (flag45)
							{
								string str2 = string.Empty;
								mFont mFont3 = mFont.tahoma_7_green2;
								bool flag46 = item4.itemOption != null;
								if (flag46)
								{
									for (int num31 = 0; num31 < item4.itemOption.Length; num31++)
									{
										bool flag47 = item4.itemOption[num31].optionTemplate.id == 72;
										if (flag47)
										{
											str2 = " [+" + item4.itemOption[num31].param.ToString() + "]";
										}
										bool flag48 = item4.itemOption[num31].optionTemplate.id == 41;
										if (flag48)
										{
											bool flag49 = item4.itemOption[num31].param == 1;
											if (flag49)
											{
												mFont3 = Panel.GetFont(0);
											}
											else
											{
												bool flag50 = item4.itemOption[num31].param == 2;
												if (flag50)
												{
													mFont3 = Panel.GetFont(2);
												}
												else
												{
													bool flag51 = item4.itemOption[num31].param == 3;
													if (flag51)
													{
														mFont3 = Panel.GetFont(8);
													}
													else
													{
														bool flag52 = item4.itemOption[num31].param == 4;
														if (flag52)
														{
															mFont3 = Panel.GetFont(7);
														}
													}
												}
											}
										}
									}
								}
								mFont3.drawString(g, item4.template.name + str2, num23 + 5, num24 + 1, 0);
								string text2 = string.Empty;
								bool flag53 = item4.itemOption != null;
								if (flag53)
								{
									bool flag54 = item4.itemOption.Length != 0 && item4.itemOption[0] != null && item4.itemOption[0].optionTemplate.id != 102 && item4.itemOption[0].optionTemplate.id != 107;
									if (flag54)
									{
										text2 += item4.itemOption[0].getOptionString();
									}
									mFont mFont4 = mFont.tahoma_7_blue;
									bool flag55 = item4.compare < 0 && item4.template.type != 5;
									if (flag55)
									{
										mFont4 = mFont.tahoma_7_red;
									}
									bool flag56 = item4.itemOption.Length > 1;
									if (flag56)
									{
										for (int num32 = 1; num32 < 2; num32++)
										{
											bool flag57 = item4.itemOption[num32] != null && item4.itemOption[num32].optionTemplate.id != 102 && item4.itemOption[num32].optionTemplate.id != 107;
											if (flag57)
											{
												text2 = text2 + "," + item4.itemOption[num32].getOptionString();
											}
										}
									}
									mFont4.drawString(g, text2, num23 + 5, num24 + 11, mFont.LEFT);
								}
								SmallImage.drawSmallImage(g, (int)item4.template.iconID, num26 + num28 / 2, num27 + num29 / 2, 0, 3);
								bool flag58 = item4.itemOption != null;
								if (flag58)
								{
									for (int num33 = 0; num33 < item4.itemOption.Length; num33++)
									{
										this.paintOptItem(g, item4.itemOption[num33].optionTemplate.id, item4.itemOption[num33].param, num26, num27, num28, num29);
									}
									for (int num34 = 0; num34 < item4.itemOption.Length; num34++)
									{
										this.paintOptSlotItem(g, item4.itemOption[num34].optionTemplate.id, item4.itemOption[num34].param, num26, num27, num28, num29);
									}
								}
								bool flag59 = item4.quantity > 1;
								if (flag59)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item4.quantity.ToString(), num26 + num28, num27 + num29 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0007DFE0 File Offset: 0x0007C1E0
	private void paintTab(mGraphics g)
	{
		bool flag = this.type == 23 || this.type == 24;
		if (flag)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.gameInfo, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else
		{
			bool flag2 = this.type == 20;
			if (flag2)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.account, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			}
			else
			{
				bool flag3 = this.type == 22;
				if (flag3)
				{
					g.setColor(13524492);
					g.fillRect(this.X + 1, 78, this.W - 2, 1);
					mFont.tahoma_7b_dark.drawString(g, mResources.autoFunction, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				}
				else
				{
					bool flag4 = this.type == 19;
					if (flag4)
					{
						g.setColor(13524492);
						g.fillRect(this.X + 1, 78, this.W - 2, 1);
						mFont.tahoma_7b_dark.drawString(g, mResources.option, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
					}
					else
					{
						bool flag5 = this.type == 18;
						if (flag5)
						{
							g.setColor(13524492);
							g.fillRect(this.X + 1, 78, this.W - 2, 1);
							mFont.tahoma_7b_dark.drawString(g, mResources.change_flag, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
						}
						else
						{
							bool flag6 = this.type == 13 && this.Equals(GameCanvas.panel2);
							if (flag6)
							{
								g.setColor(13524492);
								g.fillRect(this.X + 1, 78, this.W - 2, 1);
								mFont.tahoma_7b_dark.drawString(g, mResources.item_receive2, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
							}
							else
							{
								bool flag7 = this.type == 12 && GameCanvas.panel2 != null;
								if (flag7)
								{
									g.setColor(13524492);
									g.fillRect(this.X + 1, 78, this.W - 2, 1);
									mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
								}
								else
								{
									bool flag8 = this.type == 11;
									if (flag8)
									{
										g.setColor(13524492);
										g.fillRect(this.X + 1, 78, this.W - 2, 1);
										mFont.tahoma_7b_dark.drawString(g, mResources.friend, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
									}
									else
									{
										bool flag9 = this.type == 16;
										if (flag9)
										{
											g.setColor(13524492);
											g.fillRect(this.X + 1, 78, this.W - 2, 1);
											mFont.tahoma_7b_dark.drawString(g, mResources.enemy, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
										}
										else
										{
											bool flag10 = this.type == 15;
											if (flag10)
											{
												g.setColor(13524492);
												g.fillRect(this.X + 1, 78, this.W - 2, 1);
												mFont.tahoma_7b_dark.drawString(g, this.topName, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
											}
											else
											{
												bool flag11 = this.type == 2 && GameCanvas.panel2 != null;
												if (flag11)
												{
													g.setColor(13524492);
													g.fillRect(this.X + 1, 78, this.W - 2, 1);
													mFont.tahoma_7b_dark.drawString(g, mResources.chest, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
												}
												else
												{
													bool flag12 = this.type == 9;
													if (flag12)
													{
														g.setColor(13524492);
														g.fillRect(this.X + 1, 78, this.W - 2, 1);
														mFont.tahoma_7b_dark.drawString(g, mResources.achievement_mission, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
													}
													else
													{
														bool flag13 = this.type == 3;
														if (flag13)
														{
															g.setColor(13524492);
															g.fillRect(this.X + 1, 78, this.W - 2, 1);
															mFont.tahoma_7b_dark.drawString(g, mResources.select_zone, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
														}
														else
														{
															bool flag14 = this.type == 14;
															if (flag14)
															{
																g.setColor(13524492);
																g.fillRect(this.X + 1, 78, this.W - 2, 1);
																mFont.tahoma_7b_dark.drawString(g, mResources.select_map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
															}
															else
															{
																bool flag15 = this.type == 4;
																if (flag15)
																{
																	mFont.tahoma_7b_dark.drawString(g, mResources.map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																	g.setColor(13524492);
																	g.fillRect(this.X + 1, 78, this.W - 2, 1);
																}
																else
																{
																	bool flag16 = this.type == 7;
																	if (flag16)
																	{
																		mFont.tahoma_7b_dark.drawString(g, mResources.trangbi, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																		g.setColor(13524492);
																		g.fillRect(this.X + 1, 78, this.W - 2, 1);
																	}
																	else
																	{
																		bool flag17 = this.type == 17;
																		if (flag17)
																		{
																			mFont.tahoma_7b_dark.drawString(g, mResources.kigui, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																			g.setColor(13524492);
																			g.fillRect(this.X + 1, 78, this.W - 2, 1);
																		}
																		else
																		{
																			bool flag18 = this.type == 8;
																			if (flag18)
																			{
																				mFont.tahoma_7b_dark.drawString(g, mResources.msg, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																				g.setColor(13524492);
																				g.fillRect(this.X + 1, 78, this.W - 2, 1);
																			}
																			else
																			{
																				bool flag19 = this.type == 10;
																				if (flag19)
																				{
																					mFont.tahoma_7b_dark.drawString(g, mResources.wat_do_u_want, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																					g.setColor(13524492);
																					g.fillRect(this.X + 1, 78, this.W - 2, 1);
																				}
																				else
																				{
																					bool flag20 = this.currentTabIndex == 3 && this.mainTabName.Length != 4;
																					if (flag20)
																					{
																						g.translate(-this.cmx, 0);
																					}
																					for (int i = 0; i < this.currentTabName.Length; i++)
																					{
																						g.setColor((i != this.currentTabIndex) ? 16773296 : 6805896);
																						PopUp.paintPopUp(g, this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25, (i != this.currentTabIndex) ? 0 : 1, true);
																						bool flag21 = i == this.keyTouchTab;
																						if (flag21)
																						{
																							g.drawImage(ItemMap.imageFlare, this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 62, 3);
																						}
																						mFont mFont = (i != this.currentTabIndex) ? mFont.tahoma_7_grey : mFont.tahoma_7_green2;
																						bool flag22 = !this.currentTabName[i][1].Equals(string.Empty);
																						if (flag22)
																						{
																							mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 53, mFont.CENTER);
																							mFont.drawString(g, this.currentTabName[i][1], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 64, mFont.CENTER);
																						}
																						else
																						{
																							mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 59, mFont.CENTER);
																						}
																						bool flag23 = this.type == 0 && this.currentTabName.Length == 5 && GameScr.isNewClanMessage && GameCanvas.gameTick % 4 == 0;
																						if (flag23)
																						{
																							g.drawImage(ItemMap.imageFlare, this.startTabPos + 3 * this.TAB_W + this.TAB_W / 2, 77, mGraphics.BOTTOM | mGraphics.HCENTER);
																						}
																					}
																					g.setColor(13524492);
																					g.fillRect(1, 78, this.W - 2, 1);
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0007E9B8 File Offset: 0x0007CBB8
	private void paintBottomMoneyInfo(mGraphics g)
	{
		bool flag = this.type == 13 && (this.currentTabIndex == 2 || this.Equals(GameCanvas.panel2));
		if (!flag)
		{
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.setColor(11837316);
			g.fillRect(this.X + 1, this.H - 15, this.W - 2, 14);
			g.setColor(13524492);
			g.fillRect(this.X + 1, this.H - 15, this.W - 2, 1);
			g.drawImage(Panel.imgXu, this.X + 11, this.H - 7, 3);
			g.drawImage(Panel.imgLuong, this.X + 75, this.H - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().xuStr + string.Empty, this.X + 24, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongStr + string.Empty, this.X + 85, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgLuongKhoa, this.X + 130, this.H - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongKhoaStr + string.Empty, this.X + 140, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0007EB78 File Offset: 0x0007CD78
	private void paintClanInfo(mGraphics g)
	{
		bool flag = global::Char.myCharz().clan == null;
		if (flag)
		{
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
			mFont.tahoma_7b_white.drawString(g, mResources.not_join_clan, (this.wScroll - 50) / 2 + 50, 20, mFont.CENTER);
		}
		else
		{
			bool flag2 = !this.isViewMember;
			if (flag2)
			{
				Clan clan = global::Char.myCharz().clan;
				bool flag3 = clan != null;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
					mFont.tahoma_7b_white.drawString(g, clan.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
					mFont.tahoma_7_yellow.drawString(g, mResources.achievement_point + ": " + clan.powerPoint, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawString(g, mResources.clan_point + ": " + clan.clanPoint.ToString(), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawString(g, mResources.level + ": " + clan.level.ToString(), 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
					TextInfo.paint(g, clan.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
				}
			}
			else
			{
				Clan clan2 = (this.currClan == null) ? global::Char.myCharz().clan : this.currClan;
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
				mFont.tahoma_7b_white.drawString(g, clan2.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
				mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
				{
					mResources.member,
					": ",
					clan2.currMember,
					"/",
					clan2.maxMember
				}), 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.clan_leader + ": " + clan2.leaderName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
				TextInfo.paint(g, clan2.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
			}
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0007EE08 File Offset: 0x0007D008
	private void paintToolInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, mResources.character + ": " + global::Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		string text = (!GameCanvas.loginScr.tfUser.getText().Equals(string.Empty)) ? GameCanvas.loginScr.tfUser.getText() : mResources.not_register_yet;
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.account_server,
			" ",
			ServerListScreen.nameServer[ServerListScreen.ipSelect],
			": ",
			text
		}), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0007EEF8 File Offset: 0x0007D0F8
	private void paintGiaoDichInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.select_item, 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.lock_trade, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.wait_opp_lock_trade, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.press_done, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0007EF81 File Offset: 0x0007D181
	private void paintMyInfo(mGraphics g)
	{
		this.paintCharInfo(g, global::Char.myCharz());
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0007EF94 File Offset: 0x0007D194
	private void paintPetInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		bool flag = global::Char.myPetz().cPower > 0L;
		if (flag)
		{
			mFont.tahoma_7_yellow.drawString(g, (!global::Char.myPetz().me) ? global::Char.myPetz().currStrLevel : global::Char.myPetz().getStrLevel(), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		}
		bool flag2 = global::Char.myPetz().cDamFull > 0;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myPetz().cDamFull.ToString(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		bool flag3 = global::Char.myPetz().cMaxStamina > 0;
		if (flag3)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 100, 41, 0);
			int num = global::Char.myPetz().cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)global::Char.myPetz().cMaxStamina;
			g.setClip(100, this.X + 41, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 100, 41, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0007F140 File Offset: 0x0007D340
	private void paintCharInfo(mGraphics g, global::Char c)
	{
		mFont.tahoma_7b_white.drawString(g, ((GameScr.isNewMember == 1) ? "       " : string.Empty) + c.cName, this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		bool flag = GameScr.isNewMember == 1;
		if (flag)
		{
			SmallImage.drawSmallImage(g, 5427, this.X + 55, 4, 0, 0);
		}
		bool flag2 = c.cMaxStamina > 0;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 95, 19, 0);
			int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
			g.setClip(95, this.X + 19, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 95, 19, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag3 = c.cPower > 0L;
		if (flag3)
		{
			mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0007F2E0 File Offset: 0x0007D4E0
	private void paintCharInfo(mGraphics g, global::Char c, int x, int y)
	{
		mFont.tahoma_7b_white.drawString(g, ((GameScr.isNewMember == 1) ? "       " : string.Empty) + c.cName, x + 60, y + 4, mFont.LEFT, mFont.tahoma_7b_dark);
		bool flag = GameScr.isNewMember == 1;
		if (flag)
		{
			SmallImage.drawSmallImage(g, 5427, x + 55, y + 4, 0, 0);
		}
		bool flag2 = c.cMaxStamina > 0;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, x + 60, y + 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, x + 95, y + 19, 0);
			int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
			g.drawImage(GameScr.imgMP, x + 95, y + 19, 0);
		}
		bool flag3 = c.cPower > 0L;
		if (flag3)
		{
			mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), x + 60, y + 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), x + 60, y + 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0007F448 File Offset: 0x0007D648
	private void paintZoneInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.zone + " " + TileMap.zoneID.ToString(), 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7b_white.drawString(g, TileMap.zoneID.ToString() + string.Empty, 25, 27, mFont.CENTER);
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0007F4D0 File Offset: 0x0007D6D0
	public int getCompare(Item item)
	{
		bool flag = item == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = !item.isTypeBody();
			if (flag2)
			{
				result = 0;
			}
			else
			{
				bool flag3 = item.itemOption == null;
				if (flag3)
				{
					result = -1;
				}
				else
				{
					ItemOption itemOption = item.itemOption[0];
					bool flag4 = itemOption.optionTemplate.id == 22;
					if (flag4)
					{
						itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
						itemOption.param *= 1000;
					}
					bool flag5 = itemOption.optionTemplate.id == 23;
					if (flag5)
					{
						itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
						itemOption.param *= 1000;
					}
					Item item2 = null;
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						Item item3 = global::Char.myCharz().arrItemBody[i];
						bool flag6 = itemOption.optionTemplate.id == 22;
						if (flag6)
						{
							itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
							itemOption.param *= 1000;
						}
						bool flag7 = itemOption.optionTemplate.id == 23;
						if (flag7)
						{
							itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
							itemOption.param *= 1000;
						}
						bool flag8 = item3 != null && item3.itemOption != null && item3.template.type == item.template.type;
						if (flag8)
						{
							item2 = item3;
							break;
						}
					}
					bool flag9 = item2 == null;
					if (flag9)
					{
						this.isUp = true;
						result = itemOption.param;
					}
					else
					{
						bool flag10 = item2 != null && item2.itemOption != null;
						int num;
						if (flag10)
						{
							num = itemOption.param - item2.itemOption[0].param;
						}
						else
						{
							num = itemOption.param;
						}
						bool flag11 = num < 0;
						if (flag11)
						{
							this.isUp = false;
						}
						else
						{
							this.isUp = true;
						}
						result = num;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0007F6FC File Offset: 0x0007D8FC
	private void paintMapInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.MENUGENDER[(int)TileMap.planetID], 60, 4, mFont.LEFT);
		string str = string.Empty;
		bool flag = TileMap.mapID >= 135 && TileMap.mapID <= 138;
		if (flag)
		{
			str = " " + mResources.tang + TileMap.zoneID.ToString();
		}
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName + str, 60, 16, mFont.LEFT);
		mFont.tahoma_7b_white.drawString(g, mResources.quest_place + ": ", 60, 27, mFont.LEFT);
		bool flag2 = GameScr.getTaskMapId() >= 0 && GameScr.getTaskMapId() <= TileMap.mapNames.Length - 1;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, TileMap.mapNames[GameScr.getTaskMapId()], 60, 38, mFont.LEFT);
		}
		else
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.random, 60, 38, mFont.LEFT);
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0007F814 File Offset: 0x0007DA14
	private void paintShopInfo(mGraphics g)
	{
		bool flag = this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null;
		if (flag)
		{
			this.paintMyInfo(g);
		}
		else
		{
			bool flag2 = this.selected < 0;
			if (flag2)
			{
				bool flag3 = this.typeShop != 2;
				if (flag3)
				{
					mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 14, 0);
					mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 26, 0);
				}
				else
				{
					mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 5, 0);
					mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 17, 0);
					mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
					{
						mResources.page,
						" ",
						this.currPageShop[this.currentTabIndex] + 1,
						"/",
						this.maxPageShop[this.currentTabIndex]
					}), this.X + 60, 29, 0);
				}
			}
			else
			{
				bool flag4 = this.currentTabIndex >= 0 && this.currentTabIndex <= global::Char.myCharz().arrItemShop.Length - 1 && this.selected >= 0 && this.selected <= global::Char.myCharz().arrItemShop[this.currentTabIndex].Length - 1;
				if (flag4)
				{
					Item item = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
					bool flag5 = item != null;
					if (flag5)
					{
						bool flag6 = this.Equals(GameCanvas.panel) && this.currentTabIndex <= 3 && this.typeShop == 2;
						if (flag6)
						{
							mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
							{
								mResources.page,
								" ",
								this.currPageShop[this.currentTabIndex] + 1,
								"/",
								this.maxPageShop[this.currentTabIndex]
							}), this.X + 55, 4, 0);
						}
						mFont.tahoma_7b_white.drawString(g, item.template.name, this.X + 55, 24, 0);
						string st = mResources.pow_request + " " + Res.formatNumber((long)item.template.strRequire);
						bool flag7 = (long)item.template.strRequire > global::Char.myCharz().cPower;
						if (flag7)
						{
							mFont.tahoma_7_yellow.drawString(g, st, this.X + 55, 35, 0);
						}
						else
						{
							mFont.tahoma_7_green.drawString(g, st, this.X + 55, 35, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0007FB08 File Offset: 0x0007DD08
	private void paintItemBoxInfo(mGraphics g)
	{
		string st = string.Concat(new object[]
		{
			mResources.used,
			": ",
			this.hasUse,
			"/",
			global::Char.myCharz().arrItemBox.Length,
			" ",
			mResources.place
		});
		mFont.tahoma_7b_white.drawString(g, mResources.chest, 60, 4, 0);
		mFont.tahoma_7_yellow.drawString(g, st, 60, 16, 0);
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0007FB94 File Offset: 0x0007DD94
	private void paintSkillInfo(mGraphics g)
	{
		mFont.tahoma_7_white.drawString(g, "Top " + global::Char.myCharz().rank.ToString(), this.X + 45 + (this.W - 50) / 2, 2, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.potential_point, this.X + 45 + (this.W - 50) / 2, 14, mFont.CENTER);
		mFont.tahoma_7_white.drawString(g, string.Empty + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), this.X + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)) + 45 + (this.W - 50) / 2, 26, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.active_point + ": " + NinjaUtil.getMoneys(global::Char.myCharz().cNangdong), this.X + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)) + 45 + (this.W - 50) / 2, 38, mFont.CENTER);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0007FCC8 File Offset: 0x0007DEC8
	private void paintItemBodyBagInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.HP,
			": ",
			global::Char.myCharz().cHP,
			" / ",
			global::Char.myCharz().cHPFull
		}), this.X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.KI,
			": ",
			global::Char.myCharz().cMP,
			" / ",
			global::Char.myCharz().cMPFull
		}), this.X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myCharz().cDamFull.ToString(), this.X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.armor,
			": ",
			global::Char.myCharz().cDefull,
			", ",
			mResources.critical,
			": ",
			global::Char.myCharz().cCriticalFull,
			"%"
		}), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0007FE64 File Offset: 0x0007E064
	private void paintItemBodyBagInfo(mGraphics g, int x, int y)
	{
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.HP,
			": ",
			global::Char.myCharz().cHP,
			" / ",
			global::Char.myCharz().cHPFull
		}), x, y + 2, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.KI,
			": ",
			global::Char.myCharz().cMP,
			" / ",
			global::Char.myCharz().cMPFull
		}), x, y + 14, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myCharz().cDamFull.ToString(), x, y + 26, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.armor,
			": ",
			global::Char.myCharz().cDefull,
			", ",
			mResources.critical,
			": ",
			global::Char.myCharz().cCriticalFull,
			"%"
		}), x, y + 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0007FFE8 File Offset: 0x0007E1E8
	private void paintTopInfo(mGraphics g)
	{
		g.setClip(this.X + 1, this.Y, this.W - 2, this.yScroll - 2);
		g.setColor(9993045);
		g.fillRect(this.X, this.Y, this.W - 2, 50);
		switch (this.type)
		{
		case 0:
		{
			bool flag = this.currentTabIndex == 0;
			if (flag)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
			}
			bool flag2 = this.currentTabIndex == 1;
			if (flag2)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				bool flag3 = this.isnewInventory;
				if (flag3)
				{
					this.paintCharInfo(g, global::Char.myCharz());
				}
				else
				{
					this.paintItemBodyBagInfo(g);
				}
			}
			bool flag4 = this.currentTabIndex == 2;
			if (flag4)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintSkillInfo(g);
			}
			bool flag5 = this.currentTabIndex == 3;
			if (flag5)
			{
				bool flag6 = this.mainTabName.Length == 5;
				if (flag6)
				{
					this.paintClanInfo(g);
				}
				else
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
				}
			}
			bool flag7 = this.currentTabIndex == 4;
			if (flag7)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintToolInfo(g);
			}
			break;
		}
		case 1:
		{
			bool flag8 = this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null;
			if (flag8)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			}
			else
			{
				bool flag9 = global::Char.myCharz().npcFocus != null;
				if (flag9)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().npcFocus.avatar, this.X + 25, 50, 0, 33);
				}
			}
			this.paintShopInfo(g);
			break;
		}
		case 2:
		{
			bool flag10 = this.currentTabIndex == 0;
			if (flag10)
			{
				SmallImage.drawSmallImage(g, 526, this.X + 25, 50, 0, 33);
				this.paintItemBoxInfo(g);
			}
			bool flag11 = this.currentTabIndex == 1;
			if (flag11)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
			}
			break;
		}
		case 3:
			SmallImage.drawSmallImage(g, 561, this.X + 25, 50, 0, 33);
			this.paintZoneInfo(g);
			break;
		case 4:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMapInfo(g);
			break;
		case 7:
		case 17:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 8:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 9:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 10:
		{
			bool flag12 = this.charMenu != null;
			if (flag12)
			{
				SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
				this.paintCharInfo(g, this.charMenu);
			}
			break;
		}
		case 11:
		case 16:
		case 23:
		case 24:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 12:
		{
			bool flag13 = this.currentTabIndex == 0;
			if (flag13)
			{
				int id = 1410;
				for (int i = 0; i < GameScr.vNpc.size(); i++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(i);
					bool flag14 = npc.template.npcTemplateId == this.idNPC;
					if (flag14)
					{
						id = npc.avatar;
					}
				}
				SmallImage.drawSmallImage(g, id, this.X + 25, 50, 0, 33);
				this.paintCombineInfo(g);
			}
			bool flag15 = this.currentTabIndex == 1;
			if (flag15)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
			}
			break;
		}
		case 13:
		{
			bool flag16 = this.currentTabIndex == 0 || this.currentTabIndex == 1;
			if (flag16)
			{
				bool flag17 = this.Equals(GameCanvas.panel);
				if (flag17)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintGiaoDichInfo(g);
				}
				bool flag18 = this.Equals(GameCanvas.panel2) && this.charMenu != null;
				if (flag18)
				{
					SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
					this.paintCharInfo(g, this.charMenu);
				}
			}
			bool flag19 = this.currentTabIndex == 2 && this.charMenu != null;
			if (flag19)
			{
				SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
				this.paintCharInfo(g, this.charMenu);
			}
			break;
		}
		case 14:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMapInfo(g);
			break;
		case 15:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 18:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 19:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 20:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 21:
		{
			bool flag20 = this.currentTabIndex == 0;
			if (flag20)
			{
				SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintPetInfo(g);
			}
			bool flag21 = this.currentTabIndex == 1;
			if (flag21)
			{
				SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintPetStatusInfo(g);
			}
			bool flag22 = this.currentTabIndex == 2;
			if (flag22)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
			}
			break;
		}
		case 22:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 25:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		}
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00003136 File Offset: 0x00001336
	private void paintChatManager(mGraphics g)
	{
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00003136 File Offset: 0x00001336
	private void paintChatPlayer(mGraphics g)
	{
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x000807FC File Offset: 0x0007E9FC
	private string getStatus(int status)
	{
		string result;
		switch (status)
		{
		case 0:
			result = mResources.follow;
			break;
		case 1:
			result = mResources.defend;
			break;
		case 2:
			result = mResources.attack;
			break;
		case 3:
			result = mResources.gohome;
			break;
		default:
			result = "aaa";
			break;
		}
		return result;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00080850 File Offset: 0x0007EA50
	private void paintPetStatusInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
		{
			"HP: ",
			global::Char.myPetz().cHP,
			"/",
			global::Char.myPetz().cHPFull
		}), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
		{
			"MP: ",
			global::Char.myPetz().cMP,
			"/",
			global::Char.myPetz().cMPFull
		}), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.critical,
			": ",
			global::Char.myPetz().cCriticalFull,
			"   ",
			mResources.armor,
			": ",
			global::Char.myPetz().cDefull
		}), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.status + ": " + this.strStatus[(int)global::Char.myPetz().petStatus], this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x000809D8 File Offset: 0x0007EBD8
	private void paintCombineInfo(mGraphics g)
	{
		bool flag = this.combineTopInfo != null;
		if (flag)
		{
			for (int i = 0; i < this.combineTopInfo.Length; i++)
			{
				mFont.tahoma_7_white.drawString(g, this.combineTopInfo[i], this.X + 45 + (this.W - 50) / 2, 5 + i * 14, mFont.CENTER);
			}
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00003136 File Offset: 0x00001336
	private void paintInfomation(mGraphics g)
	{
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00080A44 File Offset: 0x0007EC44
	public void paintMap(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmxMap, -this.cmyMap);
		g.drawImage(Panel.imgMap, this.xScroll, this.yScroll, 0);
		int head = global::Char.myCharz().head;
		Part part = GameScr.parts[head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, this.xMap, this.yMap + 5, 0, 3);
		int align = mFont.CENTER;
		bool flag = this.xMap <= 40;
		if (flag)
		{
			align = mFont.LEFT;
		}
		bool flag2 = this.xMap >= 220;
		if (flag2)
		{
			align = mFont.RIGHT;
		}
		mFont.tahoma_7b_yellow.drawString(g, TileMap.mapName, this.xMap, this.yMap - 12, align, mFont.tahoma_7_grey);
		int num = -1;
		bool flag3 = GameScr.getTaskMapId() != -1;
		if (flag3)
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag4 = Panel.mapId[(int)TileMap.planetID][i] == GameScr.getTaskMapId();
				if (flag4)
				{
					num = i;
					break;
				}
				num = 4;
			}
			bool flag5 = GameCanvas.gameTick % 4 > 0;
			if (flag5)
			{
				g.drawImage(ItemMap.imageFlare, this.xScroll + Panel.mapX[(int)TileMap.planetID][num], this.yScroll + Panel.mapY[(int)TileMap.planetID][num], 3);
			}
		}
		bool flag6 = !GameCanvas.isTouch;
		if (flag6)
		{
			g.drawImage(Panel.imgBantay, this.xMove, this.yMove, StaticObj.TOP_RIGHT);
			for (int j = 0; j < Panel.mapX[(int)TileMap.planetID].Length; j++)
			{
				int num2 = Panel.mapX[(int)TileMap.planetID][j] + this.xScroll;
				int num3 = Panel.mapY[(int)TileMap.planetID][j] + this.yScroll;
				bool flag7 = Res.inRect(num2 - 15, num3 - 15, 30, 30, this.xMove, this.yMove);
				if (flag7)
				{
					align = mFont.CENTER;
					bool flag8 = num2 <= 20;
					if (flag8)
					{
						align = mFont.LEFT;
					}
					bool flag9 = num2 >= 220;
					if (flag9)
					{
						align = mFont.RIGHT;
					}
					mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][j]], num2, num3 - 12, align, mFont.tahoma_7_grey);
					break;
				}
			}
		}
		else
		{
			bool flag10 = !this.trans;
			if (flag10)
			{
				for (int k = 0; k < Panel.mapX[(int)TileMap.planetID].Length; k++)
				{
					int num4 = Panel.mapX[(int)TileMap.planetID][k] + this.xScroll;
					int num5 = Panel.mapY[(int)TileMap.planetID][k] + this.yScroll;
					bool flag11 = Res.inRect(num4 - 15, num5 - 15, 30, 30, this.pX, this.pY);
					if (flag11)
					{
						align = mFont.CENTER;
						bool flag12 = num4 <= 30;
						if (flag12)
						{
							align = mFont.LEFT;
						}
						bool flag13 = num4 >= 220;
						if (flag13)
						{
							align = mFont.RIGHT;
						}
						g.drawImage(Panel.imgBantay, num4, num5, StaticObj.TOP_RIGHT);
						mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][k]], num4, num5 - 12, align, mFont.tahoma_7_grey);
						break;
					}
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool flag14 = num != -1;
		if (flag14)
		{
			bool flag15 = Panel.mapX[(int)TileMap.planetID][num] + this.xScroll < this.cmxMap;
			if (flag15)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, this.xScroll + 5, this.yScroll + this.hScroll / 2 - 4, 0);
			}
			bool flag16 = this.cmxMap + this.wScroll < Panel.mapX[(int)TileMap.planetID][num] + this.xScroll;
			if (flag16)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 6, this.xScroll + this.wScroll - 5, this.yScroll + this.hScroll / 2 - 4, StaticObj.TOP_RIGHT);
			}
			bool flag17 = Panel.mapY[(int)TileMap.planetID][num] < this.cmyMap;
			if (flag17)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll / 2, this.yScroll + 5, StaticObj.TOP_CENTER);
			}
			bool flag18 = Panel.mapY[(int)TileMap.planetID][num] > this.cmyMap + this.hScroll;
			if (flag18)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 5, StaticObj.BOTTOM_HCENTER);
			}
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00080F90 File Offset: 0x0007F190
	public void paintTask(mGraphics g)
	{
		int num = (GameCanvas.h <= 300) ? 15 : 20;
		bool flag2 = Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && !GameScr.gI().isMapFize();
		if (flag2)
		{
			g.drawImage((this.keyTouchMapButton != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - num, 3);
			mFont.tahoma_7b_dark.drawString(g, mResources.map, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - (num + 5), mFont.CENTER);
		}
		this.xstart = this.xScroll + 5;
		this.ystart = this.yScroll + 14;
		this.yPaint = this.ystart;
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll - 35);
		bool flag3 = this.scroll != null;
		if (flag3)
		{
			bool flag4 = this.scroll.cmy > 0;
			if (flag4)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
			}
			bool flag5 = this.scroll.cmy < this.scroll.cmyLim;
			if (flag5)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 45, 0);
			}
			g.translate(0, -this.scroll.cmy);
		}
		this.indexRowMax = 0;
		bool flag6 = this.indexMenu == 0;
		if (flag6)
		{
			bool flag = false;
			bool flag7 = global::Char.myCharz().taskMaint != null;
			if (flag7)
			{
				for (int i = 0; i < global::Char.myCharz().taskMaint.names.Length; i++)
				{
					mFont.tahoma_7_grey.drawString(g, global::Char.myCharz().taskMaint.names[i], this.xScroll + this.wScroll / 2, this.yPaint - 5 + i * 12, mFont.CENTER);
					this.indexRowMax++;
				}
				this.yPaint += (global::Char.myCharz().taskMaint.names.Length - 1) * 12;
				int num2 = 0;
				string text = string.Empty;
				for (int j = 0; j < global::Char.myCharz().taskMaint.subNames.Length; j++)
				{
					bool flag8 = global::Char.myCharz().taskMaint.subNames[j] != null;
					if (flag8)
					{
						num2 = j;
						text = "- " + global::Char.myCharz().taskMaint.subNames[j];
						bool flag9 = global::Char.myCharz().taskMaint.counts[j] != -1;
						if (flag9)
						{
							bool flag10 = global::Char.myCharz().taskMaint.index == j;
							if (flag10)
							{
								bool flag11 = global::Char.myCharz().taskMaint.counts[j] != 1;
								if (flag11)
								{
									string text2 = text;
									text = string.Concat(new object[]
									{
										text2,
										" (",
										global::Char.myCharz().taskMaint.count,
										"/",
										global::Char.myCharz().taskMaint.counts[j],
										")"
									});
								}
								bool flag12 = global::Char.myCharz().taskMaint.count == global::Char.myCharz().taskMaint.counts[j];
								if (flag12)
								{
									mFont.tahoma_7.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									mFont mFont = mFont.tahoma_7_grey;
									bool flag13 = !flag;
									if (flag13)
									{
										flag = true;
										mFont = mFont.tahoma_7_blue;
										mFont.drawString(g, text, this.xstart + 5 + ((mFont != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
									}
									else
									{
										mFont.drawString(g, "- ...", this.xstart + 5 + ((mFont != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
									}
								}
							}
							else
							{
								bool flag14 = global::Char.myCharz().taskMaint.index > j;
								if (flag14)
								{
									bool flag15 = global::Char.myCharz().taskMaint.counts[j] != 1;
									if (flag15)
									{
										string text3 = text;
										text = string.Concat(new object[]
										{
											text3,
											" (",
											global::Char.myCharz().taskMaint.counts[j],
											"/",
											global::Char.myCharz().taskMaint.counts[j],
											")"
										});
									}
									mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									bool flag16 = global::Char.myCharz().taskMaint.counts[j] != 1;
									if (flag16)
									{
										text = text + " 0/" + global::Char.myCharz().taskMaint.counts[j].ToString();
									}
									mFont mFont2 = mFont.tahoma_7_grey;
									bool flag17 = !flag;
									if (flag17)
									{
										flag = true;
										mFont2 = mFont.tahoma_7_blue;
										mFont2.drawString(g, text, this.xstart + 5 + ((mFont2 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
									}
									else
									{
										mFont2.drawString(g, "- ...", this.xstart + 5 + ((mFont2 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
									}
								}
							}
						}
						else
						{
							bool flag18 = global::Char.myCharz().taskMaint.index > j;
							if (flag18)
							{
								mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
							}
							else
							{
								mFont mFont3 = mFont.tahoma_7_grey;
								bool flag19 = !flag;
								if (flag19)
								{
									flag = true;
									mFont3 = mFont.tahoma_7_blue;
									mFont3.drawString(g, text, this.xstart + 5 + ((mFont3 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
								}
								else
								{
									mFont3.drawString(g, "- ...", this.xstart + 5 + ((mFont3 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
								}
							}
						}
						this.indexRowMax++;
					}
					else
					{
						bool flag20 = global::Char.myCharz().taskMaint.index <= j;
						if (flag20)
						{
							text = "- " + global::Char.myCharz().taskMaint.subNames[num2];
							mFont mFont4 = mFont.tahoma_7_grey;
							bool flag21 = !flag;
							if (flag21)
							{
								flag = true;
								mFont4 = mFont.tahoma_7_blue;
							}
							mFont4.drawString(g, text, this.xstart + 5 + ((mFont4 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
						}
					}
				}
				this.yPaint += 5;
				for (int k = 0; k < global::Char.myCharz().taskMaint.details.Length; k++)
				{
					mFont.tahoma_7_green2.drawString(g, global::Char.myCharz().taskMaint.details[k], this.xstart + 5, this.yPaint += 12, 0);
					this.indexRowMax++;
				}
			}
			else
			{
				int taskMapId = GameScr.getTaskMapId();
				sbyte taskNpcId = GameScr.getTaskNpcId();
				string src = string.Empty;
				bool flag22 = taskMapId == -3 || taskNpcId == -3;
				if (flag22)
				{
					src = mResources.DES_TASK[3];
				}
				else
				{
					bool flag23 = global::Char.myCharz().taskMaint == null && global::Char.myCharz().ctaskId == 9 && global::Char.myCharz().nClass.classId == 0;
					if (flag23)
					{
						src = mResources.TASK_INPUT_CLASS;
					}
					else
					{
						bool flag24 = taskNpcId < 0 || taskMapId < 0;
						if (flag24)
						{
							return;
						}
						src = string.Concat(new string[]
						{
							mResources.DES_TASK[0],
							Npc.arrNpcTemplate[(int)taskNpcId].name,
							mResources.DES_TASK[1],
							TileMap.mapNames[taskMapId],
							mResources.DES_TASK[2]
						});
					}
				}
				string[] array = mFont.tahoma_7_white.splitFontArray(src, 150);
				for (int l = 0; l < array.Length; l++)
				{
					bool flag25 = l == 0;
					if (flag25)
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint = this.ystart, 0);
					}
					else
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint += 12, 0);
					}
				}
			}
		}
		else
		{
			bool flag26 = this.indexMenu == 1;
			if (flag26)
			{
				this.yPaint = this.ystart - 12;
				for (int m = 0; m < global::Char.myCharz().taskOrders.size(); m++)
				{
					TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(m);
					mFont.tahoma_7_white.drawString(g, taskOrder.name, this.xstart + 5, this.yPaint += 12, 0);
					bool flag27 = taskOrder.count == (int)taskOrder.maxCount;
					if (flag27)
					{
						mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
						{
							(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
							" ",
							Mob.arrMobTemplate[taskOrder.killId].name,
							" (",
							taskOrder.count,
							"/",
							taskOrder.maxCount,
							")"
						}), this.xstart + 5, this.yPaint += 12, 0);
					}
					else
					{
						mFont.tahoma_7_blue.drawString(g, string.Concat(new object[]
						{
							(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
							" ",
							Mob.arrMobTemplate[taskOrder.killId].name,
							" (",
							taskOrder.count,
							"/",
							taskOrder.maxCount,
							")"
						}), this.xstart + 5, this.yPaint += 12, 0);
					}
					this.indexRowMax += 3;
					this.inforW = this.popupW - 25;
					this.paintMultiLine(g, mFont.tahoma_7_grey, taskOrder.description, this.xstart + 5, this.yPaint += 12, 0);
					this.yPaint += 12;
				}
			}
		}
		bool flag28 = this.scroll == null;
		if (flag28)
		{
			this.scroll = new Scroll();
			this.scroll.setStyle(this.indexRowMax, 12, this.xScroll, this.yScroll, this.wScroll, this.hScroll - num - 40, true, 1);
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00081CAC File Offset: 0x0007FEAC
	public void paintMultiLine(mGraphics g, mFont f, string[] arr, string str, int x, int y, int align)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			string text = arr[i];
			bool flag = text.StartsWith("c");
			if (flag)
			{
				bool flag2 = text.StartsWith("c0");
				if (flag2)
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_dark;
				}
				else
				{
					bool flag3 = text.StartsWith("c1");
					if (flag3)
					{
						text = text.Substring(2);
						f = mFont.tahoma_7b_yellow;
					}
					else
					{
						bool flag4 = text.StartsWith("c2");
						if (flag4)
						{
							text = text.Substring(2);
							f = mFont.tahoma_7b_green;
						}
					}
				}
			}
			bool flag5 = i == 0;
			if (flag5)
			{
				f.drawString(g, text, x, y, align);
			}
			else
			{
				bool flag6 = i < this.indexRow + 30 && i > this.indexRow - 30;
				if (flag6)
				{
					f.drawString(g, text, x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00081DD4 File Offset: 0x0007FFD4
	public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
	{
		int num = (!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20;
		string[] array = f.splitFontArray(str, this.inforW - num);
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = i == 0;
			if (flag)
			{
				f.drawString(g, array[i], x, y, align);
			}
			else
			{
				bool flag2 = i < this.indexRow + 15 && i > this.indexRow - 15;
				if (flag2)
				{
					f.drawString(g, array[i], x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00081EAC File Offset: 0x000800AC
	public void cleanCombine()
	{
		for (int i = 0; i < this.vItemCombine.size(); i++)
		{
			((Item)this.vItemCombine.elementAt(i)).isSelect = false;
		}
		this.vItemCombine.removeAllElements();
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00081EFC File Offset: 0x000800FC
	public void hideNow()
	{
		bool flag = this.timeShow > 0;
		if (flag)
		{
			this.isClose = false;
		}
		else
		{
			bool flag2 = this.isTypeShop();
			if (flag2)
			{
				global::Char.myCharz().resetPartTemp();
			}
			bool flag3 = this.chatTField != null && this.type == 13 && this.chatTField.isShow;
			if (flag3)
			{
				this.chatTField = null;
			}
			bool flag4 = this.type == 13 && !this.isAccept;
			if (flag4)
			{
				Service.gI().giaodich(3, -1, -1, -1);
			}
			Res.outz("HIDE PANELLLLLLLLLLLLLLLLLLLLLL");
			SoundMn.gI().buttonClose();
			GameScr.isPaint = true;
			TileMap.lastPlanetId = -1;
			Panel.imgMap = null;
			mSystem.gcc();
			this.isClanOption = false;
			this.isClose = true;
			this.cleanCombine();
			Hint.clickNpc();
			GameCanvas.panel2 = null;
			GameCanvas.clearAllPointerEvent();
			GameCanvas.clearKeyPressed();
			this.pointerDownTime = (this.pointerDownFirstX = 0);
			this.pointerIsDowning = false;
			this.isShow = false;
			bool flag5 = (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead;
			if (flag5)
			{
				Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
				GameScr.gI().center = center;
				global::Char.myCharz().cHP = 0;
			}
		}
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0008207C File Offset: 0x0008027C
	public void hide()
	{
		bool flag = this.timeShow > 0;
		if (flag)
		{
			this.isClose = false;
		}
		else
		{
			bool flag2 = this.isTypeShop();
			if (flag2)
			{
				global::Char.myCharz().resetPartTemp();
			}
			bool flag3 = this.chatTField != null && this.type == 13 && this.chatTField.isShow;
			if (flag3)
			{
				this.chatTField = null;
			}
			bool flag4 = this.type == 13 && !this.isAccept;
			if (flag4)
			{
				Service.gI().giaodich(3, -1, -1, -1);
			}
			bool flag5 = this.type == 15;
			if (flag5)
			{
				Service.gI().sendThachDau(-1);
			}
			SoundMn.gI().buttonClose();
			GameScr.isPaint = true;
			TileMap.lastPlanetId = -1;
			bool flag6 = Panel.imgMap != null;
			if (flag6)
			{
				Panel.imgMap.texture = null;
				Panel.imgMap = null;
			}
			mSystem.gcc();
			this.isClanOption = false;
			bool flag7 = this.type != 4;
			if (flag7)
			{
				bool flag8 = this.type == 24;
				if (flag8)
				{
					this.setTypeGameInfo();
				}
				else
				{
					bool flag9 = this.type == 23;
					if (flag9)
					{
						this.setTypeMain();
					}
					else
					{
						bool flag10 = this.type == 3 || this.type == 14;
						if (flag10)
						{
							bool flag11 = this.isChangeZone;
							if (flag11)
							{
								this.isClose = true;
							}
							else
							{
								this.setTypeMain();
								this.cmx = (this.cmtoX = 0);
							}
						}
						else
						{
							bool flag12 = this.type == 18 || this.type == 19 || this.type == 20 || this.type == 21;
							if (flag12)
							{
								this.setTypeMain();
								this.cmx = (this.cmtoX = 0);
							}
							else
							{
								bool flag13 = this.type == 8 || this.type == 11 || this.type == 16;
								if (flag13)
								{
									this.setTypeAccount();
									this.cmx = (this.cmtoX = 0);
								}
								else
								{
									this.isClose = true;
								}
							}
						}
					}
				}
			}
			else
			{
				this.setTypeMain();
				this.cmx = (this.cmtoX = 0);
			}
			Hint.clickNpc();
			GameCanvas.panel2 = null;
			GameCanvas.clearAllPointerEvent();
			GameCanvas.clearKeyPressed();
			GameCanvas.isFocusPanel2 = false;
			this.pointerDownTime = (this.pointerDownFirstX = 0);
			this.pointerIsDowning = false;
			bool flag14 = (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead;
			if (flag14)
			{
				Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
				GameScr.gI().center = center;
				global::Char.myCharz().cHP = 0;
			}
		}
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00082378 File Offset: 0x00080578
	public void update()
	{
		bool flag = this.chatTField != null && this.chatTField.isShow;
		if (flag)
		{
			this.chatTField.update();
		}
		else
		{
			bool flag2 = this.isKiguiXu;
			if (flag2)
			{
				this.delayKigui++;
				bool flag3 = this.delayKigui == 10;
				if (flag3)
				{
					this.delayKigui = 0;
					this.isKiguiXu = false;
					this.chatTField.tfChat.setText(string.Empty);
					this.chatTField.strChat = mResources.kiguiXuchat + " ";
					this.chatTField.tfChat.name = mResources.input_money;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					this.chatTField.tfChat.setMaxTextLenght(10);
					bool isTouch = GameCanvas.isTouch;
					if (isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					bool isWindowsPhone = Main.isWindowsPhone;
					if (isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
					}
					bool flag4 = !Main.isPC;
					if (flag4)
					{
						this.chatTField.startChat2(this, string.Empty);
					}
				}
			}
			else
			{
				bool flag5 = this.isKiguiLuong;
				if (flag5)
				{
					this.delayKigui++;
					bool flag6 = this.delayKigui == 10;
					if (flag6)
					{
						this.delayKigui = 0;
						this.isKiguiLuong = false;
						this.chatTField.tfChat.setText(string.Empty);
						this.chatTField.strChat = mResources.kiguiLuongchat + "  ";
						this.chatTField.tfChat.name = mResources.input_money;
						this.chatTField.to = string.Empty;
						this.chatTField.isShow = true;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
						this.chatTField.tfChat.setMaxTextLenght(10);
						bool isTouch2 = GameCanvas.isTouch;
						if (isTouch2)
						{
							this.chatTField.tfChat.doChangeToTextBox();
						}
						bool isWindowsPhone2 = Main.isWindowsPhone;
						if (isWindowsPhone2)
						{
							this.chatTField.tfChat.strInfo = this.chatTField.strChat;
						}
						bool flag7 = !Main.isPC;
						if (flag7)
						{
							this.chatTField.startChat2(this, string.Empty);
						}
					}
				}
				else
				{
					bool flag8 = this.scroll != null;
					if (flag8)
					{
						this.scroll.updatecm();
					}
					bool flag9 = this.tabIcon != null && this.tabIcon.isShow;
					if (flag9)
					{
						this.tabIcon.update();
					}
					else
					{
						this.moveCamera();
						bool flag10 = this.isTabInven() && this.isnewInventory;
						if (flag10)
						{
							bool flag11 = this.eBanner == null;
							if (flag11)
							{
								this.eBanner = new Effect(205, 0, 0, 3, 10, -1);
								this.eBanner.typeEff = 2;
							}
							bool flag12 = this.eBanner != null;
							if (flag12)
							{
								this.eBanner.update();
							}
						}
						bool flag13 = this.waitToPerform > 0;
						if (flag13)
						{
							this.waitToPerform--;
							bool flag14 = this.waitToPerform == 0;
							if (flag14)
							{
								this.lastSelect[this.currentTabIndex] = this.selected;
								switch (this.type)
								{
								case 0:
									this.doFireMain();
									break;
								case 1:
								case 17:
									this.doFireShop();
									break;
								case 2:
									this.doFireBox();
									break;
								case 3:
									this.doFireZone();
									break;
								case 4:
									this.doFireMap();
									break;
								case 7:
								{
									bool flag15 = this.Equals(GameCanvas.panel2) && GameCanvas.panel.type == 2;
									if (flag15)
									{
										this.doFireBox();
										return;
									}
									this.doFireInventory();
									break;
								}
								case 8:
									this.doFireLogMessage();
									break;
								case 9:
									this.doFireArchivement();
									break;
								case 10:
									this.doFirePlayerMenu();
									break;
								case 11:
									this.doFireFriend();
									break;
								case 12:
									this.doFireCombine();
									break;
								case 13:
									this.doFireGiaoDich();
									break;
								case 14:
									this.doFireMapTrans();
									break;
								case 15:
									this.doFireTop();
									break;
								case 16:
									this.doFireEnemy();
									break;
								case 18:
									this.doFireChangeFlag();
									break;
								case 19:
									this.doFireOption();
									break;
								case 20:
									this.doFireAccount();
									break;
								case 21:
									this.doFirePetMain();
									break;
								case 22:
									this.doFireAuto();
									break;
								case 23:
									this.doFireGameInfo();
									break;
								case 25:
									this.doSpeacialSkill();
									break;
								}
							}
						}
						for (int i = 0; i < ClanMessage.vMessage.size(); i++)
						{
							((ClanMessage)ClanMessage.vMessage.elementAt(i)).update();
						}
						this.updateCombineEff();
					}
				}
			}
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00003136 File Offset: 0x00001336
	private void doSpeacialSkill()
	{
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000828EC File Offset: 0x00080AEC
	private void doFireGameInfo()
	{
		bool flag = this.selected == -1;
		if (!flag)
		{
			this.infoSelect = this.selected;
			((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).hasRead = true;
			Rms.saveRMSInt(((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).id.ToString() + string.Empty, 1);
			this.setTypeGameSubInfo();
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00003136 File Offset: 0x00001336
	private void doFireAuto()
	{
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00082968 File Offset: 0x00080B68
	private void doFirePetMain()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			bool flag2 = this.selected == -1;
			if (flag2)
			{
				return;
			}
			bool flag3 = this.selected > global::Char.myPetz().arrItemBody.Length - 1;
			if (flag3)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			Item item = global::Char.myPetz().arrItemBody[this.selected];
			this.currItem = item;
			bool flag4 = this.currItem != null;
			if (flag4)
			{
				myVector.addElement(new Command(mResources.MOVEOUT, this, 2006, this.currItem));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		bool flag5 = this.currentTabIndex == 1;
		if (flag5)
		{
			this.doFirePetStatus();
		}
		bool flag6 = this.currentTabIndex == 2;
		if (flag6)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00082A88 File Offset: 0x00080C88
	private void doFirePetStatus()
	{
		bool flag = this.selected == -1;
		if (!flag)
		{
			bool flag2 = this.selected == 5;
			if (flag2)
			{
				GameCanvas.startYesNoDlg(mResources.sure_fusion, new Command(mResources.YES, 888351), new Command(mResources.NO, 2001));
			}
			else
			{
				Service.gI().petStatus((sbyte)this.selected);
				bool flag3 = this.selected < 4;
				if (flag3)
				{
					global::Char.myPetz().petStatus = (sbyte)this.selected;
				}
			}
		}
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00082B14 File Offset: 0x00080D14
	private void doFireTop()
	{
		bool flag = this.selected < -1;
		if (!flag)
		{
			bool flag2 = this.isThachDau;
			if (flag2)
			{
				Service.gI().sendTop(this.topName, (sbyte)this.selected);
			}
			else
			{
				MyVector myVector = new MyVector(string.Empty);
				myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)this.vTop.elementAt(this.selected)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addThachDauDetail((TopInfo)this.vTop.elementAt(this.selected));
			}
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00082BE6 File Offset: 0x00080DE6
	private void doFireMapTrans()
	{
		this.doFireZone();
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00082BF0 File Offset: 0x00080DF0
	private void doFireGiaoDich()
	{
		bool flag = this.currentTabIndex == 0 && this.Equals(GameCanvas.panel);
		if (flag)
		{
			this.doFireInventory();
		}
		else
		{
			bool flag2 = (this.currentTabIndex == 0 && this.Equals(GameCanvas.panel2)) || this.currentTabIndex == 2;
			if (flag2)
			{
				bool flag3 = this.Equals(GameCanvas.panel2);
				if (flag3)
				{
					this.currItem = (Item)GameCanvas.panel2.vFriendGD.elementAt(this.selected);
				}
				else
				{
					this.currItem = (Item)GameCanvas.panel.vFriendGD.elementAt(this.selected);
				}
				Res.outz2("toi day select= " + this.selected.ToString());
				MyVector myVector = new MyVector();
				myVector.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
				bool flag4 = this.currItem != null;
				if (flag4)
				{
					GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addItemDetail(this.currItem);
				}
				else
				{
					this.cp = null;
				}
			}
			bool flag5 = this.currentTabIndex == 1;
			if (flag5)
			{
				bool flag6 = this.selected == this.currentListLength - 3;
				if (flag6)
				{
					bool flag7 = this.isLock;
					if (flag7)
					{
						return;
					}
					this.putMoney();
				}
				else
				{
					bool flag8 = this.selected == this.currentListLength - 2;
					if (flag8)
					{
						bool flag9 = !this.isAccept;
						if (flag9)
						{
							this.isLock = !this.isLock;
							bool flag10 = this.isLock;
							if (flag10)
							{
								Service.gI().giaodich(5, -1, -1, -1);
							}
							else
							{
								this.hide();
								InfoDlg.showWait();
								Service.gI().giaodich(3, -1, -1, -1);
							}
						}
						else
						{
							this.isAccept = false;
						}
					}
					else
					{
						bool flag11 = this.selected == this.currentListLength - 1;
						if (flag11)
						{
							bool flag12 = this.isLock && !this.isAccept && this.isFriendLock;
							if (flag12)
							{
								GameCanvas.startYesNoDlg(mResources.do_u_sure_to_trade, new Command(mResources.YES, this, 7002, null), new Command(mResources.NO, this, 4005, null));
							}
						}
						else
						{
							bool flag13 = this.isLock;
							if (flag13)
							{
								return;
							}
							this.currItem = (Item)GameCanvas.panel.vMyGD.elementAt(this.selected);
							MyVector myVector2 = new MyVector();
							myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
							bool flag14 = this.currItem != null;
							if (flag14)
							{
								GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addItemDetail(this.currItem);
							}
							else
							{
								this.cp = null;
							}
						}
					}
				}
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.selected = -1;
			}
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00082F3C File Offset: 0x0008113C
	private void doFireCombine()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			bool flag2 = this.selected == -1;
			if (flag2)
			{
				return;
			}
			bool flag3 = this.vItemCombine.size() == 0;
			if (flag3)
			{
				return;
			}
			bool flag4 = this.selected == this.vItemCombine.size();
			if (flag4)
			{
				this.keyTouchCombine = -1;
				this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
				InfoDlg.showWait();
				Service.gI().combine(1, this.vItemCombine);
				return;
			}
			bool flag5 = this.selected > this.vItemCombine.size() - 1;
			if (flag5)
			{
				return;
			}
			this.currItem = (Item)GameCanvas.panel.vItemCombine.elementAt(this.selected);
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.GETOUT, this, 6001, this.currItem));
			bool flag6 = this.currItem != null;
			if (flag6)
			{
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		bool flag7 = this.currentTabIndex == 1;
		if (flag7)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x000830AC File Offset: 0x000812AC
	private void doFirePlayerMenu()
	{
		bool flag = this.selected == -1;
		if (!flag)
		{
			this.isSelectPlayerMenu = true;
			this.hide();
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x000830D8 File Offset: 0x000812D8
	private void doFireShop()
	{
		this.currItem = null;
		bool flag = this.selected < 0;
		if (!flag)
		{
			MyVector myVector = new MyVector();
			bool flag2 = this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17;
			if (flag2)
			{
				this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
				bool flag3 = this.currItem != null;
				if (flag3)
				{
					bool isBuySpec = this.currItem.isBuySpec;
					if (isBuySpec)
					{
						bool flag4 = this.currItem.buySpec > 0;
						if (flag4)
						{
							myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2((long)this.currItem.buySpec), this, 3005, this.currItem));
						}
					}
					else
					{
						bool flag5 = this.typeShop == 4;
						if (flag5)
						{
							myVector.addElement(new Command(mResources.receive_upper, this, 30001, this.currItem));
							myVector.addElement(new Command(mResources.DELETE, this, 30002, this.currItem));
							myVector.addElement(new Command(mResources.receive_all, this, 30003, this.currItem));
						}
						else
						{
							bool flag6 = this.currItem.buyCoin == 0 && this.currItem.buyGold == 0;
							if (flag6)
							{
								bool flag7 = this.currItem.powerRequire != 0L;
								if (flag7)
								{
									myVector.addElement(new Command(string.Concat(new string[]
									{
										mResources.learn_with,
										"\n",
										Res.formatNumber(this.currItem.powerRequire),
										" \n",
										mResources.potential
									}), this, 3004, this.currItem));
								}
								else
								{
									myVector.addElement(new Command(mResources.receive_upper + "\n" + mResources.free, this, 3000, this.currItem));
								}
							}
							else
							{
								bool flag8 = this.typeShop == 8;
								if (flag8)
								{
									bool flag9 = this.currItem.buyCoin > 0;
									if (flag9)
									{
										myVector.addElement(new Command(string.Concat(new string[]
										{
											mResources.buy_with,
											"\n",
											Res.formatNumber2((long)this.currItem.buyCoin),
											"\n",
											mResources.XU
										}), this, 30001, this.currItem));
									}
									bool flag10 = this.currItem.buyGold > 0;
									if (flag10)
									{
										myVector.addElement(new Command(string.Concat(new string[]
										{
											mResources.buy_with,
											"\n",
											Res.formatNumber2((long)this.currItem.buyGold),
											"\n",
											mResources.LUONG
										}), this, 30002, this.currItem));
									}
								}
								else
								{
									bool flag11 = this.typeShop != 2;
									if (flag11)
									{
										bool flag12 = this.currItem.buyCoin > 0;
										if (flag12)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyCoin),
												"\n",
												mResources.XU
											}), this, 3000, this.currItem));
										}
										bool flag13 = this.currItem.buyGold > 0;
										if (flag13)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyGold),
												"\n",
												mResources.LUONG
											}), this, 3001, this.currItem));
										}
									}
									else
									{
										bool flag14 = this.currItem.buyCoin != -1;
										if (flag14)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyCoin),
												"\n",
												mResources.XU
											}), this, 10016, this.currItem));
										}
										bool flag15 = this.currItem.buyGold != -1;
										if (flag15)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyGold),
												"\n",
												mResources.LUONG
											}), this, 10017, this.currItem));
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag16 = this.typeShop == 0;
				if (flag16)
				{
					bool flag17 = this.selected == 0;
					if (flag17)
					{
						this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
					}
					else
					{
						this.currItem = null;
						bool flag18 = !this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
						if (flag18)
						{
							Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
							bool flag19 = item != null;
							if (flag19)
							{
								this.currItem = item;
							}
						}
						else
						{
							Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
							bool flag20 = item2 != null;
							if (flag20)
							{
								this.currItem = item2;
							}
						}
						bool flag21 = this.currItem != null;
						if (flag21)
						{
							myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
						}
					}
				}
				else
				{
					bool flag22 = this.type == 17;
					if (flag22)
					{
						this.currItem = global::Char.myCharz().arrItemShop[4][this.selected];
					}
					else
					{
						this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
					}
					bool flag23 = this.currItem.buyType == 0;
					if (flag23)
					{
						bool flag24 = this.currItem.isHaveOption(87);
						if (flag24)
						{
							myVector.addElement(new Command(mResources.kiguiLuong, this, 10013, this.currItem));
						}
						else
						{
							myVector.addElement(new Command(mResources.kiguiXu, this, 10012, this.currItem));
						}
					}
					else
					{
						bool flag25 = this.currItem.buyType == 1;
						if (flag25)
						{
							myVector.addElement(new Command(mResources.huykigui, this, 10014, this.currItem));
							myVector.addElement(new Command(mResources.upTop, this, 10018, this.currItem));
						}
						else
						{
							bool flag26 = this.currItem.buyType == 2;
							if (flag26)
							{
								myVector.addElement(new Command(mResources.nhantien, this, 10015, this.currItem));
							}
						}
					}
				}
			}
			bool flag27 = this.currItem != null;
			if (flag27)
			{
				global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x000838D0 File Offset: 0x00081AD0
	private void doFireArchivement()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().arrArchive[this.selected].isFinish && !global::Char.myCharz().arrArchive[this.selected].isRecieve;
			if (flag2)
			{
				bool flag3 = !GameCanvas.isTouch;
				if (flag3)
				{
					Service.gI().getArchivemnt(this.selected);
				}
				else
				{
					bool flag4 = GameCanvas.px > this.xScroll + this.wScroll - 40;
					if (flag4)
					{
						Service.gI().getArchivemnt(this.selected);
					}
				}
			}
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0008397C File Offset: 0x00081B7C
	private void doFireInventory()
	{
		Res.outz("fire inventory");
		bool flag = global::Char.myCharz().statusMe == 14;
		if (flag)
		{
			GameCanvas.startOKDlg(mResources.can_not_do_when_die);
		}
		else
		{
			bool flag2 = this.selected == -1;
			if (!flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
				}
				else
				{
					this.currItem = null;
					MyVector myVector = new MyVector();
					bool flag4 = this.isnewInventory && this.isnewInventory;
					if (flag4)
					{
						this.currItem = this.itemInvenNew;
						bool flag5 = this.newSelected == 0;
						if (flag5)
						{
							myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
						}
						else
						{
							bool flag6 = GameCanvas.panel.type == 12;
							if (flag6)
							{
								myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
							}
							else
							{
								bool flag7 = GameCanvas.panel.type == 13;
								if (flag7)
								{
									myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
								}
								else
								{
									bool flag8 = this.currItem.isTypeBody();
									if (flag8)
									{
										myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
										bool havePet = global::Char.myCharz().havePet;
										if (havePet)
										{
											myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
										}
									}
									else
									{
										myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
									}
								}
							}
						}
					}
					else
					{
						bool flag9 = !this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
						if (flag9)
						{
							Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
							bool flag10 = item != null;
							if (flag10)
							{
								this.currItem = item;
								bool flag11 = GameCanvas.panel.type == 12;
								if (flag11)
								{
									myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
								}
								else
								{
									bool flag12 = GameCanvas.panel.type == 13;
									if (flag12)
									{
										myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
									}
									else
									{
										bool flag13 = item.isTypeBody();
										if (flag13)
										{
											myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
											bool havePet2 = global::Char.myCharz().havePet;
											if (havePet2)
											{
												myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
											}
										}
										else
										{
											myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
										}
									}
								}
							}
						}
						else
						{
							Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
							bool flag14 = item2 != null;
							if (flag14)
							{
								this.currItem = item2;
								myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
							}
						}
					}
					bool flag15 = this.currItem != null;
					if (flag15)
					{
						global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
						bool flag16 = GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13;
						if (flag16)
						{
							bool flag17 = this.position == 0;
							if (flag17)
							{
								myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, this.currItem));
							}
							bool flag18 = this.position == 1;
							if (flag18)
							{
								myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
							}
						}
						GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addItemDetail(this.currItem);
					}
					else
					{
						this.cp = null;
					}
				}
			}
		}
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00083E20 File Offset: 0x00082020
	private void doRada()
	{
		this.hide();
		bool flag = RadarScr.list == null || RadarScr.list.size() == 0;
		if (flag)
		{
			Service.gI().SendRada(0, -1);
			RadarScr.gI().switchToMe();
		}
		else
		{
			RadarScr.gI().switchToMe();
		}
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00083E7C File Offset: 0x0008207C
	private void doFireTool()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = SoundMn.IsDelAcc && this.selected == Panel.strTool.Length - 1;
			if (flag2)
			{
				Service.gI().sendDelAcc();
			}
			else
			{
				bool flag3 = !global::Char.myCharz().havePet;
				if (flag3)
				{
					switch (this.selected)
					{
					case 0:
						this.doRada();
						break;
					case 1:
						Service.gI().openMenu(54);
						break;
					case 2:
						this.setTypeGameInfo();
						break;
					case 3:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						break;
					case 4:
					{
						bool flag4 = global::Char.myCharz().statusMe == 14;
						if (flag4)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
						}
						else
						{
							Service.gI().openUIZone();
						}
						break;
					}
					case 5:
					{
						GameCanvas.endDlg();
						bool flag5 = global::Char.myCharz().checkLuong() < 5;
						if (flag5)
						{
							GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
						}
						else
						{
							bool flag6 = this.chatTField == null;
							if (flag6)
							{
								this.chatTField = new ChatTextField();
								this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
								this.chatTField.initChatTextField();
								this.chatTField.parentScreen = GameCanvas.panel;
							}
							this.chatTField.strChat = mResources.world_channel_5_luong;
							this.chatTField.tfChat.name = mResources.CHAT;
							this.chatTField.to = string.Empty;
							this.chatTField.isShow = true;
							this.chatTField.tfChat.isFocus = true;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							bool isWindowsPhone = Main.isWindowsPhone;
							if (isWindowsPhone)
							{
								this.chatTField.tfChat.strInfo = this.chatTField.strChat;
							}
							bool flag7 = !Main.isPC;
							if (flag7)
							{
								this.chatTField.startChat2(this, string.Empty);
							}
							else
							{
								bool isTouch = GameCanvas.isTouch;
								if (isTouch)
								{
									this.chatTField.tfChat.doChangeToTextBox();
								}
							}
						}
						break;
					}
					case 6:
						this.setTypeAccount();
						break;
					case 7:
						this.setTypeOption();
						break;
					case 8:
						GameCanvas.loginScr.backToRegister();
						break;
					case 9:
					{
						bool isLogin = GameCanvas.loginScr.isLogin2;
						if (isLogin)
						{
							SoundMn.gI().backToRegister();
						}
						break;
					}
					}
				}
				else
				{
					switch (this.selected)
					{
					case 0:
						this.doRada();
						break;
					case 1:
						Service.gI().openMenu(54);
						break;
					case 2:
						this.setTypeGameInfo();
						break;
					case 3:
						this.doFirePet();
						break;
					case 4:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						break;
					case 5:
					{
						bool flag8 = global::Char.myCharz().statusMe == 14;
						if (flag8)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
						}
						else
						{
							Service.gI().openUIZone();
						}
						break;
					}
					case 6:
					{
						GameCanvas.endDlg();
						bool flag9 = global::Char.myCharz().checkLuong() < 5;
						if (flag9)
						{
							GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
						}
						else
						{
							bool flag10 = this.chatTField == null;
							if (flag10)
							{
								this.chatTField = new ChatTextField();
								this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
								this.chatTField.initChatTextField();
								this.chatTField.parentScreen = GameCanvas.panel;
							}
							this.chatTField.strChat = mResources.world_channel_5_luong;
							this.chatTField.tfChat.name = mResources.CHAT;
							this.chatTField.to = string.Empty;
							this.chatTField.isShow = true;
							this.chatTField.tfChat.isFocus = true;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							bool isWindowsPhone2 = Main.isWindowsPhone;
							if (isWindowsPhone2)
							{
								this.chatTField.tfChat.strInfo = this.chatTField.strChat;
							}
							bool flag11 = !Main.isPC;
							if (flag11)
							{
								this.chatTField.startChat2(this, string.Empty);
							}
							else
							{
								bool isTouch2 = GameCanvas.isTouch;
								if (isTouch2)
								{
									this.chatTField.tfChat.doChangeToTextBox();
								}
							}
						}
						break;
					}
					case 7:
						this.setTypeAccount();
						break;
					case 8:
						this.setTypeOption();
						break;
					case 9:
						GameCanvas.loginScr.backToRegister();
						break;
					case 10:
					{
						bool isLogin2 = GameCanvas.loginScr.isLogin2;
						if (isLogin2)
						{
							SoundMn.gI().backToRegister();
						}
						break;
					}
					}
				}
			}
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x000843A0 File Offset: 0x000825A0
	private void setTypeGameSubInfo()
	{
		string content = ((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).content;
		Panel.contenInfo = mFont.tahoma_7_grey.splitFontArray(content, this.wScroll - 40);
		this.currentListLength = Panel.contenInfo.Length;
		this.ITEM_HEIGHT = 16;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 24;
		this.setType(0);
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00084494 File Offset: 0x00082694
	private void setTypeGameInfo()
	{
		this.currentListLength = Panel.vGameInfo.size();
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 23;
		this.setType(0);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00084553 File Offset: 0x00082753
	private void doFirePet()
	{
		InfoDlg.showWait();
		Service.gI().petInfo();
		this.timeShow = 20;
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00084570 File Offset: 0x00082770
	private void searchClan()
	{
		this.chatTField.strChat = mResources.input_clan_name;
		this.chatTField.tfChat.name = mResources.clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0008462C File Offset: 0x0008282C
	private void chatClan()
	{
		this.chatTField.strChat = mResources.chat_clan;
		this.chatTField.tfChat.name = mResources.CHAT;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x000846E8 File Offset: 0x000828E8
	public void creatClan()
	{
		this.chatTField.strChat = mResources.input_clan_name_to_create;
		this.chatTField.tfChat.name = mResources.input_clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00084794 File Offset: 0x00082994
	public void putMoney()
	{
		bool flag = this.chatTField == null;
		if (flag)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_money_to_trade;
		this.chatTField.tfChat.name = mResources.input_money;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		this.chatTField.tfChat.setMaxTextLenght(10);
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag2 = !Main.isPC;
		if (flag2)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x000848CC File Offset: 0x00082ACC
	public void putQuantily()
	{
		bool flag = this.chatTField == null;
		if (flag)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_quantity_to_trade;
		this.chatTField.tfChat.name = mResources.input_quantity;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag2 = !Main.isPC;
		if (flag2)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x000849F4 File Offset: 0x00082BF4
	public void chagenSlogan()
	{
		this.chatTField.strChat = mResources.input_clan_slogan;
		this.chatTField.tfChat.name = mResources.input_clan_slogan;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00084AB0 File Offset: 0x00082CB0
	public void changeIcon()
	{
		bool flag = this.tabIcon == null;
		if (flag)
		{
			this.tabIcon = new TabClanIcon();
		}
		this.tabIcon.text = this.chatTField.tfChat.getText();
		this.tabIcon.show(false);
		this.chatTField.isShow = false;
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00084B0C File Offset: 0x00082D0C
	private void addFriend(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName;
		text += "\n";
		bool isOnline = info.isOnline;
		if (isOnline)
		{
			text = text + "|4|1|" + mResources.is_online;
		}
		else
		{
			text = text + "|3|1|" + mResources.is_offline;
		}
		text += "\n--";
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|5|",
			mResources.power,
			": ",
			info.s
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00084BDC File Offset: 0x00082DDC
	private void doFireEnemy()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = this.vEnemy.size() == 0;
			if (!flag2)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected;
				myVector.addElement(new Command(mResources.REVENGE, this, 10000, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
				myVector.addElement(new Command(mResources.DELETE, this, 10001, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addFriend((InfoItem)this.vEnemy.elementAt(this.selected));
			}
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00084CD0 File Offset: 0x00082ED0
	private void doFireFriend()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = this.vFriend.size() == 0;
			if (!flag2)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected;
				myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
				myVector.addElement(new Command(mResources.DELETE, this, 8002, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
				myVector.addElement(new Command(mResources.den, this, 8004, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addFriend((InfoItem)this.vFriend.elementAt(this.selected));
			}
		}
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00084DF0 File Offset: 0x00082FF0
	private void doFireChangeFlag()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
			myVector.addElement(new Command(mResources.BACK, this, 10031, null));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00084E80 File Offset: 0x00083080
	private void doFireLogMessage()
	{
		bool flag = this.selected == 0;
		if (flag)
		{
			this.isViewChatServer = !this.isViewChatServer;
			Rms.saveRMSInt("viewchat", (!this.isViewChatServer) ? 0 : 1);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.selected = -1;
			}
		}
		else
		{
			bool flag2 = this.selected < 0;
			if (!flag2)
			{
				bool flag3 = this.logChat.size() == 0;
				if (!flag3)
				{
					MyVector myVector = new MyVector();
					this.currInfoItem = this.selected - 1;
					myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
					myVector.addElement(new Command(mResources.make_friend, this, 8003, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
					GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addLogMessage((InfoItem)this.logChat.elementAt(this.selected - 1));
				}
			}
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00084FC4 File Offset: 0x000831C4
	private void doFireClanOption()
	{
		try
		{
			this.partID = null;
			this.charInfo = null;
			Res.outz("cSelect= " + this.cSelected.ToString());
			bool flag = this.selected < 0;
			if (flag)
			{
				this.cSelected = -1;
			}
			else
			{
				bool flag2 = global::Char.myCharz().clan == null;
				if (flag2)
				{
					bool flag3 = this.selected == 0;
					if (flag3)
					{
						bool flag4 = this.cSelected == 0;
						if (flag4)
						{
							this.searchClan();
						}
						else
						{
							bool flag5 = this.cSelected == 1;
							if (flag5)
							{
								InfoDlg.showWait();
								this.creatClan();
								Service.gI().getClan(1, -1, null);
							}
						}
					}
					else
					{
						bool flag6 = this.selected != -1;
						if (flag6)
						{
							bool flag7 = this.selected == 1;
							if (flag7)
							{
								bool flag8 = this.isSearchClan;
								if (flag8)
								{
									Service.gI().searchClan(string.Empty);
								}
								else
								{
									bool flag9 = this.isViewMember && this.currClan != null;
									if (flag9)
									{
										GameCanvas.startYesNoDlg(mResources.do_u_want_join_clan + this.currClan.name, new Command(mResources.YES, this, 4000, this.currClan), new Command(mResources.NO, this, 4005, this.currClan));
									}
								}
							}
							else
							{
								bool flag10 = this.isSearchClan;
								if (flag10)
								{
									this.currClan = this.getCurrClan();
									bool flag11 = this.currClan != null;
									if (flag11)
									{
										MyVector myVector = new MyVector();
										myVector.addElement(new Command(mResources.request_join_clan, this, 4000, this.currClan));
										myVector.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
										GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
										this.addClanDetail(this.getCurrClan());
									}
								}
								else
								{
									bool flag12 = this.isViewMember;
									if (flag12)
									{
										this.currMem = this.getCurrMember();
										bool flag13 = this.currMem != null;
										if (flag13)
										{
											MyVector myVector2 = new MyVector();
											myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currClan));
											GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
											GameCanvas.menu.startAt(myVector2, 0, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
											this.addClanMemberDetail(this.currMem);
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag14 = this.selected == 0;
					if (flag14)
					{
						bool flag15 = this.isMessage;
						if (flag15)
						{
							bool flag16 = this.cSelected == 0;
							if (flag16)
							{
								bool flag17 = this.myMember.size() > 1;
								if (flag17)
								{
									this.chatClan();
								}
								else
								{
									this.member = null;
									this.isSearchClan = false;
									this.isViewMember = true;
									this.isMessage = false;
									this.currentListLength = this.myMember.size() + 2;
									this.initTabClans();
								}
							}
							bool flag18 = this.cSelected == 1;
							if (flag18)
							{
								Service.gI().clanMessage(1, null, -1);
							}
							bool flag19 = this.cSelected == 2;
							if (flag19)
							{
								this.member = null;
								this.isSearchClan = false;
								this.isViewMember = true;
								this.isMessage = false;
								this.currentListLength = this.myMember.size() + 2;
								this.initTabClans();
								this.getCurrClanOtion();
							}
						}
						else
						{
							bool flag20 = this.isViewMember;
							if (flag20)
							{
								bool flag21 = this.cSelected == 0;
								if (flag21)
								{
									this.isSearchClan = false;
									this.isViewMember = false;
									this.isMessage = true;
									this.currentListLength = ClanMessage.vMessage.size() + 2;
									this.initTabClans();
								}
								bool flag22 = this.cSelected == 1;
								if (flag22)
								{
									bool flag23 = this.myMember.size() > 1;
									if (flag23)
									{
										Service.gI().leaveClan();
									}
									else
									{
										this.chagenSlogan();
									}
								}
								bool flag24 = this.cSelected == 2;
								if (flag24)
								{
									bool flag25 = this.myMember.size() > 1;
									if (flag25)
									{
										this.chagenSlogan();
									}
									else
									{
										Service.gI().getClan(3, -1, null);
									}
								}
								bool flag26 = this.cSelected == 3;
								if (flag26)
								{
									Service.gI().getClan(3, -1, null);
								}
							}
						}
					}
					else
					{
						bool flag27 = this.selected == 1;
						if (flag27)
						{
							bool flag28 = this.isSearchClan;
							if (flag28)
							{
								Service.gI().searchClan(string.Empty);
							}
						}
						else
						{
							bool flag29 = this.isSearchClan;
							if (flag29)
							{
								this.currClan = this.getCurrClan();
								bool flag30 = this.currClan != null;
								if (flag30)
								{
									MyVector myVector3 = new MyVector();
									myVector3.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
									GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
									this.addClanDetail(this.getCurrClan());
								}
							}
							else
							{
								bool flag31 = this.isViewMember;
								if (flag31)
								{
									Res.outz("TOI DAY 1");
									this.currMem = this.getCurrMember();
									bool flag32 = this.currMem != null;
									if (flag32)
									{
										MyVector myVector4 = new MyVector();
										Res.outz("TOI DAY 2");
										bool flag33 = this.member != null;
										if (flag33)
										{
											myVector4.addElement(new Command(mResources.CLOSE, this, 8000, null));
											Res.outz("TOI DAY 3");
										}
										else
										{
											bool flag34 = this.myMember != null;
											if (flag34)
											{
												Res.outz("TOI DAY 4");
												Res.outz("my role= " + global::Char.myCharz().role.ToString());
												bool flag35 = global::Char.myCharz().charID == this.currMem.ID || global::Char.myCharz().role == 2;
												if (flag35)
												{
													myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
												}
												bool flag36 = global::Char.myCharz().role < 2 && global::Char.myCharz().charID != this.currMem.ID;
												if (flag36)
												{
													Res.outz("TOI DAY");
													bool flag37 = this.currMem.role == 0 || this.currMem.role == 1;
													if (flag37)
													{
														myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
													}
													bool flag38 = this.currMem.role == 2;
													if (flag38)
													{
														myVector4.addElement(new Command(mResources.create_clan_co_leader, this, 5002, this.currMem));
													}
													bool flag39 = global::Char.myCharz().role == 0;
													if (flag39)
													{
														myVector4.addElement(new Command(mResources.create_clan_leader, this, 5001, this.currMem));
														bool flag40 = this.currMem.role == 1;
														if (flag40)
														{
															myVector4.addElement(new Command(mResources.disable_clan_mastership, this, 5003, this.currMem));
														}
													}
												}
												bool flag41 = global::Char.myCharz().role < this.currMem.role;
												if (flag41)
												{
													myVector4.addElement(new Command(mResources.kick_clan_mem, this, 5004, this.currMem));
												}
											}
										}
										GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
										this.addClanMemberDetail(this.currMem);
									}
								}
								else
								{
									bool flag42 = this.isMessage;
									if (flag42)
									{
										this.currMess = this.getCurrMessage();
										bool flag43 = this.currMess != null;
										if (flag43)
										{
											bool flag44 = this.currMess.type == 0;
											if (flag44)
											{
												MyVector myVector5 = new MyVector();
												myVector5.addElement(new Command(mResources.CLOSE, this, 8000, this.currMess));
												GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
												this.addMessageDetail(this.currMess);
											}
											else
											{
												bool flag45 = this.currMess.type == 1;
												if (flag45)
												{
													bool flag46 = this.currMess.playerId != global::Char.myCharz().charID && this.cSelected != -1;
													if (flag46)
													{
														Service.gI().clanDonate(this.currMess.id);
													}
												}
												else
												{
													bool flag47 = this.currMess.type == 2 && this.currMess.option != null;
													if (flag47)
													{
														bool flag48 = this.cSelected == 0;
														if (flag48)
														{
															Service.gI().joinClan(this.currMess.id, 1);
														}
														else
														{
															bool flag49 = this.cSelected == 1;
															if (flag49)
															{
																Service.gI().joinClan(this.currMess.id, 0);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					this.cSelected = -1;
					this.selected = -1;
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x000859E0 File Offset: 0x00083BE0
	private void doFireMain()
	{
		try
		{
			bool flag = this.currentTabIndex == 0;
			if (flag)
			{
				this.setTypeMap();
			}
			bool flag2 = this.currentTabIndex == 1;
			if (flag2)
			{
				this.doFireInventory();
			}
			bool flag3 = this.currentTabIndex == 2;
			if (flag3)
			{
				this.doFireSkill();
			}
			bool flag4 = this.currentTabIndex == 3;
			if (flag4)
			{
				bool flag5 = this.mainTabName.Length == 4;
				if (flag5)
				{
					this.doFireTool();
				}
				else
				{
					this.doFireClanOption();
				}
			}
			bool flag6 = this.currentTabIndex == 4;
			if (flag6)
			{
				this.doFireTool();
			}
		}
		catch (Exception ex)
		{
			Res.outz("Throw ex " + ex.StackTrace);
		}
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00085AB0 File Offset: 0x00083CB0
	private void doFireSkill()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().statusMe == 14;
			if (flag2)
			{
				GameCanvas.startOKDlg(mResources.can_not_do_when_die);
			}
			else
			{
				bool flag3 = this.selected != 0 && this.selected != 1 && this.selected != 2 && this.selected != 3 && this.selected != 4 && this.selected != 5;
				if (flag3)
				{
					int num = this.selected - 6;
					SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num];
					Skill skill = global::Char.myCharz().getSkill(skillTemplate);
					Skill skill2 = null;
					MyVector myVector = new MyVector(string.Empty);
					bool flag4 = skill != null;
					if (flag4)
					{
						bool flag5 = skill.point == skillTemplate.maxPoint;
						if (flag5)
						{
							myVector.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
							myVector.addElement(new Command(mResources.CLOSE, 2));
						}
						else
						{
							skill2 = skillTemplate.skills[skill.point];
							myVector.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
							myVector.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
						}
					}
					else
					{
						skill2 = skillTemplate.skills[0];
						myVector.addElement(new Command(mResources.learn, this, 9004, skill2));
					}
					GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addSkillDetail(skillTemplate, skill, skill2);
				}
				else
				{
					long cTiemNang = global::Char.myCharz().cTiemNang;
					int cHPGoc = global::Char.myCharz().cHPGoc;
					int cMPGoc = global::Char.myCharz().cMPGoc;
					int cDamGoc = global::Char.myCharz().cDamGoc;
					int cDefGoc = global::Char.myCharz().cDefGoc;
					int cCriticalGoc = global::Char.myCharz().cCriticalGoc;
					int num2 = 1000;
					bool flag6 = this.selected == 0;
					if (flag6)
					{
						bool flag7 = cTiemNang < (long)(global::Char.myCharz().cHPGoc + num2);
						if (flag7)
						{
							GameCanvas.startOKDlg(string.Concat(new object[]
							{
								mResources.not_enough_potential_point1,
								global::Char.myCharz().cTiemNang,
								mResources.not_enough_potential_point2,
								global::Char.myCharz().cHPGoc + num2
							}), false);
							return;
						}
						bool flag8 = cTiemNang > (long)cHPGoc && cTiemNang < (long)(10 * (2 * (cHPGoc + num2) + 180) / 2);
						if (flag8)
						{
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								cHPGoc + num2,
								mResources.use_potential_point_for2,
								global::Char.myCharz().hpFrom1000TiemNang,
								mResources.for_HP
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							return;
						}
						bool flag9 = cTiemNang >= (long)(10 * (2 * (cHPGoc + num2) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cHPGoc + num2) + 1980) / 2);
						if (flag9)
						{
							MyVector myVector2 = new MyVector(string.Empty);
							myVector2.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().hpFrom1000TiemNang,
								mResources.HP,
								"\n-",
								Res.formatNumber2((long)(cHPGoc + num2))
							}), this, 9000, null));
							myVector2.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().hpFrom1000TiemNang),
								mResources.HP,
								"\n-",
								Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
							}), this, 9006, null));
							GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
						bool flag10 = cTiemNang >= (long)(100 * (2 * (cHPGoc + num2) + 1980) / 2);
						if (flag10)
						{
							MyVector myVector3 = new MyVector(string.Empty);
							myVector3.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().hpFrom1000TiemNang,
								mResources.HP,
								"\n-",
								Res.formatNumber2((long)(cHPGoc + num2))
							}), this, 9000, null));
							myVector3.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().hpFrom1000TiemNang),
								mResources.HP,
								"\n-",
								Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
							}), this, 9006, null));
							myVector3.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(100 * global::Char.myCharz().hpFrom1000TiemNang),
								mResources.HP,
								"\n-",
								Res.formatNumber2((long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					bool flag11 = this.selected == 1;
					if (flag11)
					{
						bool flag12 = global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cMPGoc + num2);
						if (flag12)
						{
							GameCanvas.startOKDlg(string.Concat(new object[]
							{
								mResources.not_enough_potential_point1,
								global::Char.myCharz().cTiemNang,
								mResources.not_enough_potential_point2,
								global::Char.myCharz().cMPGoc + num2
							}));
							return;
						}
						bool flag13 = cTiemNang > (long)cMPGoc && cTiemNang < (long)(10 * (2 * (cMPGoc + num2) + 180) / 2);
						if (flag13)
						{
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								cMPGoc + num2,
								mResources.use_potential_point_for2,
								global::Char.myCharz().mpFrom1000TiemNang,
								mResources.for_KI
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							return;
						}
						bool flag14 = cTiemNang >= (long)(10 * (2 * (cMPGoc + num2) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cMPGoc + num2) + 1980) / 2);
						if (flag14)
						{
							MyVector myVector4 = new MyVector(string.Empty);
							myVector4.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().mpFrom1000TiemNang,
								mResources.KI,
								"\n-",
								Res.formatNumber2((long)(cHPGoc + num2))
							}), this, 9000, null));
							myVector4.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().mpFrom1000TiemNang),
								mResources.KI,
								"\n-",
								Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
							}), this, 9006, null));
							GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
						bool flag15 = cTiemNang >= (long)(100 * (2 * (cMPGoc + num2) + 1980) / 2);
						if (flag15)
						{
							MyVector myVector5 = new MyVector(string.Empty);
							myVector5.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().mpFrom1000TiemNang,
								mResources.KI,
								"\n-",
								Res.formatNumber2((long)(cMPGoc + num2))
							}), this, 9000, null));
							myVector5.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().mpFrom1000TiemNang),
								mResources.KI,
								"\n-",
								Res.formatNumber2((long)(10 * (2 * (cMPGoc + num2) + 180) / 2))
							}), this, 9006, null));
							myVector5.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(100 * global::Char.myCharz().mpFrom1000TiemNang),
								mResources.KI,
								"\n-",
								Res.formatNumber2((long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					bool flag16 = this.selected == 2;
					if (flag16)
					{
						bool flag17 = global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd);
						if (flag17)
						{
							GameCanvas.startOKDlg(string.Concat(new object[]
							{
								mResources.not_enough_potential_point1,
								global::Char.myCharz().cTiemNang,
								mResources.not_enough_potential_point2,
								cDamGoc * 100
							}));
							return;
						}
						bool flag18 = cTiemNang > (long)cDamGoc && cTiemNang < (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd);
						if (flag18)
						{
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								cDamGoc * 100,
								mResources.use_potential_point_for2,
								global::Char.myCharz().damFrom1000TiemNang,
								mResources.for_hit_point
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							return;
						}
						bool flag19 = cTiemNang >= (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd) && cTiemNang < (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd);
						if (flag19)
						{
							MyVector myVector6 = new MyVector(string.Empty);
							myVector6.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().damFrom1000TiemNang,
								"\n",
								mResources.hit_point,
								"\n-",
								Res.formatNumber2((long)(cDamGoc * 100))
							}), this, 9000, null));
							myVector6.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().damFrom1000TiemNang),
								"\n",
								mResources.hit_point,
								"\n-",
								Res.formatNumber2((long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
							}), this, 9006, null));
							GameCanvas.menu.startAt(myVector6, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
						bool flag20 = cTiemNang >= (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd);
						if (flag20)
						{
							MyVector myVector7 = new MyVector(string.Empty);
							myVector7.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().damFrom1000TiemNang,
								"\n",
								mResources.hit_point,
								"\n-",
								Res.formatNumber2((long)(cDamGoc * 100))
							}), this, 9000, null));
							myVector7.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().damFrom1000TiemNang),
								"\n",
								mResources.hit_point,
								"\n-",
								Res.formatNumber2((long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
							}), this, 9006, null));
							myVector7.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(100 * global::Char.myCharz().damFrom1000TiemNang),
								"\n",
								mResources.hit_point,
								"\n-",
								Res.formatNumber2((long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector7, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					bool flag21 = this.selected == 3;
					if (flag21)
					{
						bool flag22 = global::Char.myCharz().cTiemNang < (long)(50000 + global::Char.myCharz().cDefGoc * 1000);
						if (flag22)
						{
							GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys((long)(50000 + global::Char.myCharz().cDefGoc * 1000)));
						}
						else
						{
							long number = (long)(2 * (cDefGoc + 5)) / 2L * 100000L;
							long number2 = 10L * (long)(2 * (cDefGoc + 5) + 9) / 2L * 100000L;
							long number3 = 100L * (long)(2 * (cDefGoc + 5) + 99) / 2L * 100000L;
							mResources.use_potential_point_for1 = mResources.increase_upper;
							MyVector myVector8 = new MyVector(string.Empty);
							myVector8.addElement(new Command(string.Concat(new string[]
							{
								mResources.use_potential_point_for1,
								"\n1 ",
								mResources.armor,
								"\n",
								Res.formatNumber2(number)
							}), this, 9000, null));
							myVector8.addElement(new Command(string.Concat(new string[]
							{
								mResources.use_potential_point_for1,
								"\n10 ",
								mResources.armor,
								"\n",
								Res.formatNumber2(number2)
							}), this, 9006, null));
							myVector8.addElement(new Command(string.Concat(new string[]
							{
								mResources.use_potential_point_for1,
								"\n100 ",
								mResources.armor,
								"\n",
								Res.formatNumber2(number3)
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector8, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					else
					{
						bool flag23 = this.selected != 4;
						if (flag23)
						{
							bool flag24 = this.selected == 5;
							if (flag24)
							{
								Service.gI().speacialSkill(0);
							}
						}
						else
						{
							int num3 = global::Char.myCharz().cCriticalGoc;
							bool flag25 = num3 > Panel.t_tiemnang.Length - 1;
							if (flag25)
							{
								num3 = Panel.t_tiemnang.Length - 1;
							}
							long num4 = Panel.t_tiemnang[num3];
							bool flag26 = global::Char.myCharz().cTiemNang < num4;
							if (flag26)
							{
								GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num4));
							}
							else
							{
								GameCanvas.startYesNoDlg(string.Concat(new object[]
								{
									mResources.use_potential_point_for1,
									Res.formatNumber(num4),
									mResources.use_potential_point_for2,
									global::Char.myCharz().criticalFrom1000Tiemnang,
									mResources.for_crit
								}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00086C40 File Offset: 0x00084E40
	private void addLogMessage(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName;
		text += "\n";
		text += "\n--";
		text = text + "\n|5|" + Res.split(info.s, "|", 0)[2];
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00086CC8 File Offset: 0x00084EC8
	private void addSkillDetail2(int type)
	{
		string text = string.Empty;
		int num = 0;
		bool flag = this.selected == 0;
		if (flag)
		{
			num = global::Char.myCharz().cHPGoc + 1000;
		}
		bool flag2 = this.selected == 1;
		if (flag2)
		{
			num = global::Char.myCharz().cMPGoc + 1000;
		}
		bool flag3 = this.selected == 2;
		if (flag3)
		{
			num = global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd;
		}
		bool flag4 = this.selected == 3;
		if (flag4)
		{
			num = 500000 + global::Char.myCharz().cDefGoc * 100000;
		}
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"|5|2|",
			mResources.USE,
			" ",
			num,
			" ",
			mResources.potential
		});
		bool flag5 = type == 0;
		if (flag5)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20hp;
		}
		bool flag6 = type == 1;
		if (flag6)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20mp;
		}
		bool flag7 = type == 2;
		if (flag7)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		bool flag8 = type == 3;
		if (flag8)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.idIcon = -1;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00003136 File Offset: 0x00001336
	private void doFireClanIcon()
	{
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00086E5C File Offset: 0x0008505C
	private void doFireMap()
	{
		bool flag = Panel.imgMap != null;
		if (flag)
		{
			Panel.imgMap.texture = null;
			Panel.imgMap = null;
		}
		TileMap.lastPlanetId = -1;
		mSystem.gcc();
		SmallImage.loadBigRMS();
		this.setTypeMain();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00086EB4 File Offset: 0x000850B4
	private void doFireZone()
	{
		bool flag = this.selected == -1;
		if (!flag)
		{
			Res.outz("FIRE ZONE");
			this.isChangeZone = true;
			GameCanvas.panel.hide();
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00086EF0 File Offset: 0x000850F0
	public void updateRequest(int recieve, int maxCap)
	{
		this.cp.says[this.cp.says.Length - 1] = string.Concat(new object[]
		{
			mResources.received,
			" ",
			recieve,
			"/",
			maxCap
		});
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00086F50 File Offset: 0x00085150
	private void doFireBox()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			this.currItem = null;
			MyVector myVector = new MyVector();
			bool flag2 = this.currentTabIndex == 0 && !this.Equals(GameCanvas.panel2);
			if (flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					this.setNewSelected(global::Char.myCharz().arrItemBox.Length, false);
				}
				else
				{
					sbyte b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
					Item item = global::Char.myCharz().arrItemBox[(int)b];
					bool flag4 = item != null;
					if (flag4)
					{
						bool flag5 = this.isBoxClan;
						if (flag5)
						{
							myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
							myVector.addElement(new Command(mResources.USE, this, 2010, item));
						}
						else
						{
							bool flag6 = item.isTypeBody();
							if (flag6)
							{
								myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
							}
							else
							{
								myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
							}
						}
						this.currItem = item;
					}
				}
			}
			bool flag7 = this.currentTabIndex == 1 || this.Equals(GameCanvas.panel2);
			if (flag7)
			{
				bool flag8 = this.selected == 0;
				if (flag8)
				{
					this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, true);
				}
				else
				{
					Item[] arrItemBody = global::Char.myCharz().arrItemBody;
					bool flag9 = !this.GetInventorySelect_isbody(this.selected, this.newSelected, arrItemBody);
					if (flag9)
					{
						sbyte b2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
						Item item2 = global::Char.myCharz().arrItemBag[(int)b2];
						bool flag10 = item2 != null;
						if (flag10)
						{
							myVector.addElement(new Command(mResources.move_to_chest, this, 1001, item2));
							bool flag11 = item2.isTypeBody();
							if (flag11)
							{
								myVector.addElement(new Command(mResources.USE, this, 2000, item2));
							}
							else
							{
								myVector.addElement(new Command(mResources.USE, this, 2001, item2));
							}
							this.currItem = item2;
						}
					}
					else
					{
						Item item3 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
						bool flag12 = item3 != null;
						if (flag12)
						{
							myVector.addElement(new Command(mResources.move_to_chest2, this, 1002, item3));
							this.currItem = item3;
						}
					}
				}
			}
			bool flag13 = this.currItem != null;
			if (flag13)
			{
				global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				bool flag14 = this.isBoxClan;
				if (flag14)
				{
					myVector.addElement(new Command(mResources.MOVEOUT, this, 2011, this.currItem));
				}
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x000872D8 File Offset: 0x000854D8
	public void itemRequest(sbyte itemAction, string info, sbyte where, sbyte index)
	{
		GameCanvas.endDlg();
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)itemAction;
		itemObject.id = (int)index;
		itemObject.where = (int)where;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 2004, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00087334 File Offset: 0x00085534
	public void saleRequest(sbyte type, string info, short id)
	{
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)type;
		itemObject.id = (int)id;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 3003, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00087380 File Offset: 0x00085580
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 9999;
		if (flag)
		{
			TopInfo topInfo = (TopInfo)p;
			Service.gI().sendThachDau(topInfo.pId);
		}
		bool flag2 = idAction == 170391;
		if (flag2)
		{
			Rms.clearAll();
			bool flag3 = mGraphics.zoomLevel > 1;
			if (flag3)
			{
				Rms.saveRMSInt("levelScreenKN", 1);
			}
			else
			{
				Rms.saveRMSInt("levelScreenKN", 0);
			}
			GameMidlet.instance.exit();
		}
		bool flag4 = idAction == 6001;
		if (flag4)
		{
			Item item = (Item)p;
			item.isSelect = false;
			GameCanvas.panel.vItemCombine.removeElement(item);
			bool flag5 = GameCanvas.panel.currentTabIndex == 0;
			if (flag5)
			{
				GameCanvas.panel.setTabCombine();
			}
		}
		bool flag6 = idAction == 6000;
		if (flag6)
		{
			Item item2 = (Item)p;
			for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
			{
				Item item3 = (Item)GameCanvas.panel.vItemCombine.elementAt(i);
				bool flag7 = item3.template.id == item2.template.id;
				if (flag7)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			item2.isSelect = true;
			GameCanvas.panel.vItemCombine.addElement(item2);
			bool flag8 = GameCanvas.panel.currentTabIndex == 0;
			if (flag8)
			{
				GameCanvas.panel.setTabCombine();
			}
		}
		bool flag9 = idAction == 7000;
		if (flag9)
		{
			bool flag10 = this.isLock;
			if (flag10)
			{
				GameCanvas.startOKDlg(mResources.unlock_item_to_trade);
				return;
			}
			Item item4 = (Item)p;
			for (int j = 0; j < GameCanvas.panel.vMyGD.size(); j++)
			{
				Item item5 = (Item)GameCanvas.panel.vMyGD.elementAt(j);
				bool flag11 = item5.indexUI == item4.indexUI;
				if (flag11)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			bool flag12 = item4.quantity > 1;
			if (flag12)
			{
				this.putQuantily();
				return;
			}
			item4.isSelect = true;
			Item item6 = new Item();
			item6.template = item4.template;
			item6.itemOption = item4.itemOption;
			item6.indexUI = item4.indexUI;
			GameCanvas.panel.vMyGD.addElement(item6);
			Service.gI().giaodich(2, -1, (sbyte)item6.indexUI, item6.quantity);
		}
		bool flag13 = idAction == 7001;
		if (flag13)
		{
			Item item7 = (Item)p;
			item7.isSelect = false;
			GameCanvas.panel.vMyGD.removeElement(item7);
			bool flag14 = GameCanvas.panel.currentTabIndex == 1;
			if (flag14)
			{
				GameCanvas.panel.setTabGiaoDich(true);
			}
			Service.gI().giaodich(4, -1, (sbyte)item7.indexUI, -1);
		}
		bool flag15 = idAction == 7002;
		if (flag15)
		{
			this.isAccept = true;
			GameCanvas.endDlg();
			Service.gI().giaodich(7, -1, -1, -1);
			this.hide();
		}
		bool flag16 = idAction == 8003;
		if (flag16)
		{
			InfoItem infoItem = (InfoItem)p;
			Service.gI().friend(1, infoItem.charInfo.charID);
			bool flag17 = this.type == 8;
			if (flag17)
			{
			}
		}
		bool flag18 = idAction == 8002;
		if (flag18)
		{
			InfoItem infoItem2 = (InfoItem)p;
			Service.gI().friend(2, infoItem2.charInfo.charID);
		}
		bool flag19 = idAction == 8004;
		if (flag19)
		{
			InfoItem infoItem3 = (InfoItem)p;
			Service.gI().gotoPlayer(infoItem3.charInfo.charID);
		}
		bool flag20 = idAction == 8001;
		if (flag20)
		{
			Res.outz("chat player");
			InfoItem infoItem4 = (InfoItem)p;
			bool flag21 = this.chatTField == null;
			if (flag21)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.chat_player;
			this.chatTField.tfChat.name = mResources.chat_with + " " + infoItem4.charInfo.cName;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			bool isWindowsPhone = Main.isWindowsPhone;
			if (isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			bool flag22 = !Main.isPC;
			if (flag22)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		bool flag23 = idAction == 1000;
		if (flag23)
		{
			Service.gI().getItem(Panel.BOX_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		bool flag24 = idAction == 1001;
		if (flag24)
		{
			sbyte id = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_BOX, id);
		}
		bool flag25 = idAction == 1003;
		if (flag25)
		{
			this.hide();
		}
		bool flag26 = idAction == 1002;
		if (flag26)
		{
			Service.gI().getItem(Panel.BODY_BOX, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		bool flag27 = idAction == 2011;
		if (flag27)
		{
			Service.gI().useItem(1, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
		}
		bool flag28 = idAction == 2010;
		if (flag28)
		{
			Service.gI().useItem(0, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
			Item item8 = (Item)p;
			bool flag29 = item8 != null && (item8.template.id == 193 || item8.template.id == 194);
			if (flag29)
			{
				GameCanvas.panel.hide();
			}
		}
		bool flag30 = idAction == 2000;
		if (flag30)
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			sbyte id2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
			bool flag31 = this.isnewInventory;
			if (flag31)
			{
				id2 = (sbyte)this.currItem.indexUI;
			}
			Service.gI().getItem(Panel.BAG_BODY, id2);
		}
		bool flag32 = idAction == 2001;
		if (flag32)
		{
			Res.outz("use item");
			Item item9 = (Item)p;
			bool inventorySelect_isbody = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			bool flag33 = !inventorySelect_isbody;
			sbyte index;
			if (flag33)
			{
				index = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			}
			else
			{
				index = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
			}
			bool flag34 = this.isnewInventory;
			if (flag34)
			{
				index = (sbyte)this.currItem.indexUI;
				sbyte where = 0;
				bool flag35 = this.newSelected != 0;
				if (flag35)
				{
					where = 1;
				}
				Service.gI().useItem(0, where, index, -1);
			}
			else
			{
				Service.gI().useItem(0, (sbyte)((!inventorySelect_isbody) ? 1 : 0), index, -1);
			}
			bool flag36 = item9.template.id == 193 || item9.template.id == 194;
			if (flag36)
			{
				GameCanvas.panel.hide();
			}
		}
		bool flag37 = idAction == 2002;
		if (flag37)
		{
			bool flag38 = this.isnewInventory;
			if (flag38)
			{
				Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.sellectInventory);
			}
			else
			{
				Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
			}
		}
		bool flag39 = idAction == 2003;
		if (flag39)
		{
			Res.outz("remove item");
			bool inventorySelect_isbody2 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			bool flag40 = !inventorySelect_isbody2;
			sbyte index2;
			if (flag40)
			{
				index2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			}
			else
			{
				index2 = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
			}
			Service.gI().useItem(1, (sbyte)((!inventorySelect_isbody2) ? 1 : 0), index2, -1);
		}
		bool flag41 = idAction == 2004;
		if (flag41)
		{
			GameCanvas.endDlg();
			ItemObject itemObject = (ItemObject)p;
			sbyte where2 = (sbyte)itemObject.where;
			sbyte index3 = (sbyte)itemObject.id;
			Service.gI().useItem((sbyte)((itemObject.type != 0) ? 2 : 3), where2, index3, -1);
		}
		bool flag42 = idAction == 2005;
		if (flag42)
		{
			sbyte id3 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_PET, id3);
		}
		bool flag43 = idAction == 2006;
		if (flag43)
		{
			Item[] arrItemBody2 = global::Char.myPetz().arrItemBody;
			sbyte id4 = (sbyte)this.selected;
			Service.gI().getItem(Panel.PET_BAG, id4);
		}
		bool flag44 = idAction == 30001;
		if (flag44)
		{
			Res.outz("nhan do");
			Service.gI().buyItem(0, this.selected, 0);
		}
		bool flag45 = idAction == 30002;
		if (flag45)
		{
			Res.outz("xoa do");
			Service.gI().buyItem(1, this.selected, 0);
		}
		bool flag46 = idAction == 30003;
		if (flag46)
		{
			Res.outz("nhan tat");
			Service.gI().buyItem(2, this.selected, 0);
		}
		bool flag47 = idAction == 3000;
		if (flag47)
		{
			Res.outz("mua do");
			Item item10 = (Item)p;
			Service.gI().buyItem(0, (int)item10.template.id, 0);
		}
		bool flag48 = idAction == 3001;
		if (flag48)
		{
			Item item11 = (Item)p;
			GameCanvas.msgdlg.pleasewait();
			Service.gI().buyItem(1, (int)item11.template.id, 0);
		}
		bool flag49 = idAction == 3002;
		if (flag49)
		{
			GameCanvas.endDlg();
			bool inventorySelect_isbody3 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			bool flag50 = !inventorySelect_isbody3;
			sbyte b;
			if (flag50)
			{
				b = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			}
			else
			{
				b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
			}
			Service.gI().saleItem(0, (sbyte)((!inventorySelect_isbody3) ? 1 : 0), (short)b);
		}
		bool flag51 = idAction == 3003;
		if (flag51)
		{
			GameCanvas.endDlg();
			ItemObject itemObject2 = (ItemObject)p;
			Service.gI().saleItem(1, (sbyte)itemObject2.type, (short)itemObject2.id);
		}
		bool flag52 = idAction == 3004;
		if (flag52)
		{
			Item item12 = (Item)p;
			Service.gI().buyItem(3, (int)item12.template.id, 0);
		}
		bool flag53 = idAction == 3005;
		if (flag53)
		{
			Res.outz("mua do");
			Item item13 = (Item)p;
			Service.gI().buyItem(3, (int)item13.template.id, 0);
		}
		bool flag54 = idAction == 4000;
		if (flag54)
		{
			Clan clan = (Clan)p;
			bool flag55 = clan != null;
			if (flag55)
			{
				GameCanvas.endDlg();
				Service.gI().clanMessage(2, null, clan.ID);
			}
		}
		bool flag56 = idAction == 4001;
		if (flag56)
		{
			Clan clan2 = (Clan)p;
			bool flag57 = clan2 != null;
			if (flag57)
			{
				InfoDlg.showWait();
				this.clanReport = mResources.PLEASEWAIT;
				Service.gI().clanMember(clan2.ID);
			}
		}
		bool flag58 = idAction == 4005;
		if (flag58)
		{
			GameCanvas.endDlg();
		}
		bool flag59 = idAction == 4007;
		if (flag59)
		{
			GameCanvas.endDlg();
		}
		bool flag60 = idAction == 4006;
		if (flag60)
		{
			ClanMessage clanMessage = (ClanMessage)p;
			Service.gI().clanDonate(clanMessage.id);
		}
		bool flag61 = idAction == 5001;
		if (flag61)
		{
			Member member = (Member)p;
			Service.gI().clanRemote(member.ID, 0);
		}
		bool flag62 = idAction == 5002;
		if (flag62)
		{
			Member member2 = (Member)p;
			Service.gI().clanRemote(member2.ID, 1);
		}
		bool flag63 = idAction == 5003;
		if (flag63)
		{
			Member member3 = (Member)p;
			Service.gI().clanRemote(member3.ID, 2);
		}
		bool flag64 = idAction == 5004;
		if (flag64)
		{
			Member member4 = (Member)p;
			Service.gI().clanRemote(member4.ID, -1);
		}
		bool flag65 = idAction == 9000;
		if (flag65)
		{
			Service.gI().upPotential(this.selected, 1);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		bool flag66 = idAction == 9006;
		if (flag66)
		{
			Service.gI().upPotential(this.selected, 10);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		bool flag67 = idAction == 9007;
		if (flag67)
		{
			Service.gI().upPotential(this.selected, 100);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		bool flag68 = idAction == 9002;
		if (flag68)
		{
			Skill skill = (Skill)p;
			bool flag69 = skill.template.isSkillSpec();
			if (flag69)
			{
				GameCanvas.startOKDlg(mResources.updSkill);
			}
			else
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.can_buy_from_Uron1,
					skill.powRequire,
					mResources.can_buy_from_Uron2,
					skill.moreInfo,
					mResources.can_buy_from_Uron3
				}));
			}
		}
		bool flag70 = idAction == 9003;
		if (flag70)
		{
			bool flag71 = GameCanvas.isTouch && !Main.isPC;
			if (flag71)
			{
				GameScr.gI().doSetOnScreenSkill((SkillTemplate)p);
			}
			else
			{
				GameScr.gI().doSetKeySkill((SkillTemplate)p);
			}
		}
		bool flag72 = idAction == 9004;
		if (flag72)
		{
			Skill skill2 = (Skill)p;
			bool flag73 = skill2.template.isSkillSpec();
			if (flag73)
			{
				GameCanvas.startOKDlg(mResources.learnSkill);
			}
			else
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.can_buy_from_Uron1,
					skill2.powRequire,
					mResources.can_buy_from_Uron2,
					skill2.moreInfo,
					mResources.can_buy_from_Uron3
				}));
			}
		}
		bool flag74 = idAction == 10000;
		if (flag74)
		{
			InfoItem infoItem5 = (InfoItem)p;
			Service.gI().enemy(1, infoItem5.charInfo.charID);
			GameCanvas.panel.hideNow();
		}
		bool flag75 = idAction == 10001;
		if (flag75)
		{
			InfoItem infoItem6 = (InfoItem)p;
			Service.gI().enemy(2, infoItem6.charInfo.charID);
			InfoDlg.showWait();
		}
		bool flag76 = idAction == 10021;
		if (flag76)
		{
		}
		bool flag77 = idAction == 10012;
		if (flag77)
		{
			bool flag78 = this.chatTField == null;
			if (flag78)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.chatTField.tfChat.setText(string.Empty);
			bool flag79 = this.currItem.quantity == 1;
			if (flag79)
			{
				this.chatTField.strChat = mResources.kiguiXuchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + " ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.tfChat.setMaxTextLenght(10);
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			bool isWindowsPhone2 = Main.isWindowsPhone;
			if (isWindowsPhone2)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			bool flag80 = !Main.isPC;
			if (flag80)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		bool flag81 = idAction == 10013;
		if (flag81)
		{
			bool flag82 = this.chatTField == null;
			if (flag82)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.chatTField.tfChat.setText(string.Empty);
			bool flag83 = this.currItem.quantity == 1;
			if (flag83)
			{
				this.chatTField.strChat = mResources.kiguiLuongchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + "  ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			bool isTouch2 = GameCanvas.isTouch;
			if (isTouch2)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			bool isWindowsPhone3 = Main.isWindowsPhone;
			if (isWindowsPhone3)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			bool flag84 = !Main.isPC;
			if (flag84)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		bool flag85 = idAction == 10014;
		if (flag85)
		{
			Item item14 = (Item)p;
			Service.gI().kigui(1, item14.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		bool flag86 = idAction == 10015;
		if (flag86)
		{
			Item item15 = (Item)p;
			Service.gI().kigui(2, item15.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		bool flag87 = idAction == 10016;
		if (flag87)
		{
			Item item16 = (Item)p;
			Service.gI().kigui(3, item16.itemId, 0, item16.buyCoin, -1);
			InfoDlg.showWait();
		}
		bool flag88 = idAction == 10017;
		if (flag88)
		{
			Item item17 = (Item)p;
			Service.gI().kigui(3, item17.itemId, 1, item17.buyGold, -1);
			InfoDlg.showWait();
		}
		bool flag89 = idAction == 10018;
		if (flag89)
		{
			Item item18 = (Item)p;
			Service.gI().kigui(5, item18.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		bool flag90 = idAction == 10019;
		if (flag90)
		{
			Session_ME.gI().close();
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
			GameCanvas.loginScr.tfPass.setText(string.Empty);
			GameCanvas.loginScr.tfUser.setText(string.Empty);
			GameCanvas.loginScr.isLogin2 = false;
			GameCanvas.loginScr.switchToMe();
			GameCanvas.endDlg();
			this.hide();
		}
		bool flag91 = idAction == 10020;
		if (flag91)
		{
			GameCanvas.endDlg();
		}
		bool flag92 = idAction == 10030;
		if (flag92)
		{
			Service.gI().getFlag(1, (sbyte)this.selected);
			GameCanvas.panel.hideNow();
		}
		bool flag93 = idAction == 10031;
		if (flag93)
		{
			Session_ME.gI().close();
		}
		bool flag94 = idAction == 11000;
		if (flag94)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, 1);
			GameCanvas.endDlg();
		}
		bool flag95 = idAction == 11001;
		if (flag95)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, this.currItem.quantilyToBuy);
			GameCanvas.endDlg();
		}
		bool flag96 = idAction == 11002;
		if (flag96)
		{
			this.chatTField.isShow = false;
			GameCanvas.endDlg();
		}
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0008899C File Offset: 0x00086B9C
	public void onChatFromMe(string text, string to)
	{
		bool flag = this.chatTField.tfChat.getText() == null || this.chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null;
		if (flag)
		{
			this.chatTField.isShow = false;
		}
		else
		{
			bool flag2 = this.chatTField.strChat.Equals(mResources.input_clan_name);
			if (flag2)
			{
				InfoDlg.showWait();
				this.chatTField.isShow = false;
				Service.gI().searchClan(text);
			}
			else
			{
				bool flag3 = this.chatTField.strChat.Equals(mResources.chat_clan);
				if (flag3)
				{
					InfoDlg.showWait();
					this.chatTField.isShow = false;
					Service.gI().clanMessage(0, text, -1);
				}
				else
				{
					bool flag4 = this.chatTField.strChat.Equals(mResources.input_clan_name_to_create);
					if (flag4)
					{
						bool flag5 = this.chatTField.tfChat.getText() == string.Empty;
						if (flag5)
						{
							GameScr.info1.addInfo(mResources.clan_name_blank, 0);
						}
						else
						{
							bool flag6 = this.tabIcon == null;
							if (flag6)
							{
								this.tabIcon = new TabClanIcon();
							}
							this.tabIcon.text = this.chatTField.tfChat.getText();
							this.tabIcon.show(false);
							this.chatTField.isShow = false;
						}
					}
					else
					{
						bool flag7 = this.chatTField.strChat.Equals(mResources.input_clan_slogan);
						if (flag7)
						{
							bool flag8 = this.chatTField.tfChat.getText() == string.Empty;
							if (flag8)
							{
								GameScr.info1.addInfo(mResources.clan_slogan_blank, 0);
							}
							else
							{
								Service.gI().getClan(4, (sbyte)global::Char.myCharz().clan.imgID, this.chatTField.tfChat.getText());
								this.chatTField.isShow = false;
							}
						}
						else
						{
							bool flag9 = this.chatTField.strChat.Equals(mResources.input_Inventory_Pass);
							if (flag9)
							{
								try
								{
									int lockInventory = int.Parse(this.chatTField.tfChat.getText());
									this.chatTField.isShow = false;
									this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
									this.hide();
									bool flag10 = this.chatTField.tfChat.getText().Length != 6 || this.chatTField.tfChat.getText().Equals(string.Empty);
									if (flag10)
									{
										GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
									}
									else
									{
										Service.gI().setLockInventory(lockInventory);
										this.chatTField.isShow = false;
										this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
										this.hide();
									}
								}
								catch (Exception ex)
								{
									GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
								}
							}
							else
							{
								bool flag11 = this.chatTField.strChat.Equals(mResources.world_channel_5_luong);
								if (flag11)
								{
									bool flag12 = this.chatTField.tfChat.getText().Equals(string.Empty);
									if (!flag12)
									{
										Service.gI().chatGlobal(this.chatTField.tfChat.getText());
										this.chatTField.isShow = false;
										this.hide();
									}
								}
								else
								{
									bool flag13 = this.chatTField.strChat.Equals(mResources.chat_player);
									if (flag13)
									{
										this.chatTField.isShow = false;
										InfoItem infoItem = null;
										bool flag14 = this.type == 8;
										if (flag14)
										{
											infoItem = (InfoItem)this.logChat.elementAt(this.currInfoItem);
										}
										else
										{
											bool flag15 = this.type == 11;
											if (flag15)
											{
												infoItem = (InfoItem)this.vFriend.elementAt(this.currInfoItem);
											}
										}
										bool flag16 = infoItem.charInfo.charID == global::Char.myCharz().charID;
										if (!flag16)
										{
											Service.gI().chatPlayer(text, infoItem.charInfo.charID);
										}
									}
									else
									{
										bool flag17 = this.chatTField.strChat.Equals(mResources.input_quantity_to_trade);
										if (flag17)
										{
											int num = 0;
											try
											{
												num = int.Parse(this.chatTField.tfChat.getText());
											}
											catch (Exception ex2)
											{
												GameCanvas.startOKDlg(mResources.input_quantity_wrong);
												this.chatTField.isShow = false;
												this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												return;
											}
											bool flag18 = num <= 0 || num > this.currItem.quantity;
											if (flag18)
											{
												GameCanvas.startOKDlg(mResources.input_quantity_wrong);
												this.chatTField.isShow = false;
												this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
											}
											else
											{
												this.currItem.isSelect = true;
												Item item = new Item();
												item.template = this.currItem.template;
												item.quantity = num;
												item.indexUI = this.currItem.indexUI;
												item.itemOption = this.currItem.itemOption;
												GameCanvas.panel.vMyGD.addElement(item);
												Service.gI().giaodich(2, -1, (sbyte)item.indexUI, item.quantity);
												this.chatTField.isShow = false;
												this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
											}
										}
										else
										{
											bool flag19 = this.chatTField.strChat == mResources.input_money_to_trade;
											if (flag19)
											{
												int num2 = 0;
												try
												{
													num2 = int.Parse(this.chatTField.tfChat.getText());
												}
												catch (Exception ex3)
												{
													GameCanvas.startOKDlg(mResources.input_money_wrong);
													this.chatTField.isShow = false;
													this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
													return;
												}
												bool flag20 = (long)num2 > global::Char.myCharz().xu;
												if (flag20)
												{
													GameCanvas.startOKDlg(mResources.not_enough_money);
													this.chatTField.isShow = false;
													this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												}
												else
												{
													this.moneyGD = num2;
													Service.gI().giaodich(2, -1, -1, num2);
													this.chatTField.isShow = false;
													this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												}
											}
											else
											{
												bool flag21 = this.chatTField.strChat.Equals(mResources.kiguiXuchat);
												if (flag21)
												{
													Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), 1);
													this.chatTField.isShow = false;
												}
												else
												{
													bool flag22 = this.chatTField.strChat.Equals(mResources.kiguiXuchat + " ");
													if (flag22)
													{
														Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), this.currItem.quantilyToBuy);
														this.chatTField.isShow = false;
													}
													else
													{
														bool flag23 = this.chatTField.strChat.Equals(mResources.kiguiLuongchat);
														if (flag23)
														{
															this.doNotiRuby(0);
															this.chatTField.isShow = false;
														}
														else
														{
															bool flag24 = this.chatTField.strChat.Equals(mResources.kiguiLuongchat + "  ");
															if (flag24)
															{
																this.doNotiRuby(1);
																this.chatTField.isShow = false;
															}
															else
															{
																bool flag25 = this.chatTField.strChat.Equals(mResources.input_quantity + " ");
																if (flag25)
																{
																	this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
																	bool flag26 = this.currItem.quantilyToBuy > this.currItem.quantity;
																	if (flag26)
																	{
																		GameCanvas.startOKDlg(mResources.input_quantity_wrong);
																	}
																	else
																	{
																		this.isKiguiXu = true;
																		this.chatTField.isShow = false;
																	}
																}
																else
																{
																	bool flag27 = this.chatTField.strChat.Equals(mResources.input_quantity + "  ");
																	if (flag27)
																	{
																		this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
																		bool flag28 = this.currItem.quantilyToBuy > this.currItem.quantity;
																		if (flag28)
																		{
																			GameCanvas.startOKDlg(mResources.input_quantity_wrong);
																		}
																		else
																		{
																			this.isKiguiLuong = true;
																			this.chatTField.isShow = false;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x000892D4 File Offset: 0x000874D4
	public void onCancelChat()
	{
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x000892F0 File Offset: 0x000874F0
	public void setCombineEff(int type)
	{
		this.typeCombine = type;
		this.rS = 90;
		bool flag = this.typeCombine == 0;
		if (flag)
		{
			this.iDotS = 5;
			this.angleS = (this.angleO = 90);
			this.time = 2;
			for (int i = 0; i < this.vItemCombine.size(); i++)
			{
				Item item = (Item)this.vItemCombine.elementAt(i);
				bool flag2 = item != null;
				if (flag2)
				{
					bool flag3 = item.template.type == 14;
					if (flag3)
					{
						this.iconID2 = item.template.iconID;
					}
					else
					{
						this.iconID1 = item.template.iconID;
					}
				}
			}
		}
		else
		{
			bool flag4 = this.typeCombine == 1;
			if (flag4)
			{
				this.iDotS = 2;
				this.angleS = (this.angleO = 0);
				this.time = 1;
				for (int j = 0; j < this.vItemCombine.size(); j++)
				{
					Item item2 = (Item)this.vItemCombine.elementAt(j);
					bool flag5 = item2 != null;
					if (flag5)
					{
						bool flag6 = j == 0;
						if (flag6)
						{
							this.iconID1 = item2.template.iconID;
						}
						else
						{
							this.iconID2 = item2.template.iconID;
						}
					}
				}
			}
			else
			{
				bool flag7 = this.typeCombine == 2;
				if (flag7)
				{
					this.iDotS = 7;
					this.angleS = (this.angleO = 25);
					this.time = 1;
					for (int k = 0; k < this.vItemCombine.size(); k++)
					{
						Item item3 = (Item)this.vItemCombine.elementAt(k);
						bool flag8 = item3 != null;
						if (flag8)
						{
							this.iconID1 = item3.template.iconID;
						}
					}
				}
				else
				{
					bool flag9 = this.typeCombine == 3;
					if (flag9)
					{
						this.xS = GameCanvas.hw;
						this.yS = GameCanvas.hh;
						this.iDotS = 1;
						this.angleS = (this.angleO = 1);
						this.time = 4;
						for (int l = 0; l < this.vItemCombine.size(); l++)
						{
							Item item4 = (Item)this.vItemCombine.elementAt(l);
							bool flag10 = item4 != null;
							if (flag10)
							{
								this.iconID1 = item4.template.iconID;
							}
						}
					}
					else
					{
						bool flag11 = this.typeCombine == 4;
						if (flag11)
						{
							this.iDotS = this.vItemCombine.size();
							this.iconID = new short[this.iDotS];
							this.angleS = (this.angleO = 25);
							this.time = 1;
							for (int m = 0; m < this.vItemCombine.size(); m++)
							{
								Item item5 = (Item)this.vItemCombine.elementAt(m);
								bool flag12 = item5 != null;
								if (flag12)
								{
									this.iconID[m] = item5.template.iconID;
								}
							}
						}
					}
				}
			}
		}
		this.speed = 1;
		this.isSpeedCombine = true;
		this.isDoneCombine = false;
		this.isCompleteEffCombine = false;
		this.iAngleS = 360 / this.iDotS;
		this.xArgS = new int[this.iDotS];
		this.yArgS = new int[this.iDotS];
		this.xDotS = new int[this.iDotS];
		this.yDotS = new int[this.iDotS];
		this.setDotStar();
		this.isPaintCombine = true;
		this.countUpdate = 10;
		this.countR = 30;
		this.countWait = 10;
		this.addTextCombineNPC(this.idNPC, mResources.combineSpell);
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x000896E8 File Offset: 0x000878E8
	private void updateCombineEff()
	{
		this.countUpdate--;
		bool flag = this.countUpdate < 0;
		if (flag)
		{
			this.countUpdate = 0;
		}
		this.countR--;
		bool flag2 = this.countR < 0;
		if (flag2)
		{
			this.countR = 0;
		}
		bool flag3 = this.countUpdate == 0;
		if (flag3)
		{
			bool flag4 = !this.isCompleteEffCombine;
			if (flag4)
			{
				bool flag5 = this.time > 0;
				if (flag5)
				{
					bool flag6 = this.combineSuccess != -1;
					if (flag6)
					{
						bool flag7 = this.typeCombine == 3;
						if (flag7)
						{
							bool flag8 = GameCanvas.gameTick % 10 == 0;
							if (flag8)
							{
								Effect me = new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1);
								EffecMn.addEff(me);
								this.time--;
							}
						}
						else
						{
							bool flag9 = GameCanvas.gameTick % 2 == 0;
							if (flag9)
							{
								bool flag10 = this.isSpeedCombine;
								if (flag10)
								{
									bool flag11 = this.speed < 40;
									if (flag11)
									{
										this.speed += 2;
									}
								}
								else
								{
									bool flag12 = this.speed > 10;
									if (flag12)
									{
										this.speed -= 2;
									}
								}
							}
							bool flag13 = this.countR == 0;
							if (flag13)
							{
								bool flag14 = this.isSpeedCombine;
								if (flag14)
								{
									bool flag15 = this.rS > 0;
									if (flag15)
									{
										this.rS -= 5;
									}
									else
									{
										bool flag16 = GameCanvas.gameTick % 10 == 0;
										if (flag16)
										{
											this.isSpeedCombine = false;
											this.time--;
											this.countR = 5;
											this.countWait = 10;
										}
									}
								}
								else
								{
									bool flag17 = this.rS < 90;
									if (flag17)
									{
										this.rS += 5;
									}
									else
									{
										bool flag18 = GameCanvas.gameTick % 10 == 0;
										if (flag18)
										{
											this.isSpeedCombine = true;
											this.countR = 10;
										}
									}
								}
							}
							this.angleS = this.angleO;
							this.angleS -= this.speed;
							bool flag19 = this.angleS >= 360;
							if (flag19)
							{
								this.angleS -= 360;
							}
							bool flag20 = this.angleS < 0;
							if (flag20)
							{
								this.angleS = 360 + this.angleS;
							}
							this.angleO = this.angleS;
							this.setDotStar();
						}
					}
				}
				else
				{
					bool flag21 = GameCanvas.gameTick % 20 == 0;
					if (flag21)
					{
						this.isCompleteEffCombine = true;
					}
				}
				bool flag22 = GameCanvas.gameTick % 20 == 0;
				if (flag22)
				{
					bool flag23 = this.typeCombine != 3;
					if (flag23)
					{
						EffectPanel.addServerEffect(132, this.xS, this.yS, 2);
					}
					EffectPanel.addServerEffect(114, this.xS, this.yS + 20, 2);
				}
			}
			else
			{
				bool flag24 = this.isCompleteEffCombine;
				if (flag24)
				{
					bool flag25 = this.combineSuccess == 1;
					if (flag25)
					{
						bool flag26 = this.countWait == 10;
						if (flag26)
						{
							Effect me2 = new Effect(22, this.xS - 3, this.yS + 25, 4, 1, 1);
							EffecMn.addEff(me2);
						}
						this.countWait--;
						bool flag27 = this.countWait < 0;
						if (flag27)
						{
							this.countWait = 0;
						}
						bool flag28 = this.rS < 300;
						if (flag28)
						{
							this.rS = Res.abs(this.rS + 10);
							bool flag29 = this.rS == 20;
							if (flag29)
							{
								this.addTextCombineNPC(this.idNPC, mResources.combineFail);
							}
						}
						else
						{
							bool flag30 = GameCanvas.gameTick % 20 == 0;
							if (flag30)
							{
								bool flag31 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
								if (flag31)
								{
									GameCanvas.panel2 = new Panel();
									GameCanvas.panel2.tabName[7] = new string[][]
									{
										new string[]
										{
											string.Empty
										}
									};
									GameCanvas.panel2.setTypeBodyOnly();
									GameCanvas.panel2.show();
								}
								this.combineSuccess = -1;
								this.isDoneCombine = true;
								bool flag32 = this.typeCombine == 4;
								if (flag32)
								{
									GameCanvas.panel.hideNow();
								}
							}
						}
						this.setDotStar();
					}
					else
					{
						bool flag33 = this.combineSuccess == 0;
						if (flag33)
						{
							bool flag34 = this.countWait == 10;
							if (flag34)
							{
								bool flag35 = this.typeCombine == 2;
								if (flag35)
								{
									Effect me3 = new Effect(20, this.xS - 3, this.yS + 15, 4, 2, 1);
									EffecMn.addEff(me3);
								}
								else
								{
									Effect me4 = new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1);
									EffecMn.addEff(me4);
								}
								this.addTextCombineNPC(this.idNPC, mResources.combineSuccess);
								this.isPaintCombine = false;
							}
							bool flag36 = !this.isPaintCombine;
							if (flag36)
							{
								this.countWait--;
								bool flag37 = this.countWait < -50;
								if (flag37)
								{
									this.countWait = -50;
									bool flag38 = this.typeCombine < 3 && GameCanvas.w > 2 * Panel.WIDTH_PANEL;
									if (flag38)
									{
										GameCanvas.panel2 = new Panel();
										GameCanvas.panel2.tabName[7] = new string[][]
										{
											new string[]
											{
												string.Empty
											}
										};
										GameCanvas.panel2.setTypeBodyOnly();
										GameCanvas.panel2.show();
									}
									this.combineSuccess = -1;
									this.isDoneCombine = true;
									bool flag39 = this.typeCombine == 4;
									if (flag39)
									{
										GameCanvas.panel.hideNow();
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00089CEC File Offset: 0x00087EEC
	public void paintCombineEff(mGraphics g)
	{
		GameScr.gI().paintBlackSky(g);
		this.paintCombineNPC(g);
		bool flag = GameCanvas.gameTick % 4 == 0;
		if (flag)
		{
			g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		bool flag2 = this.typeCombine == 0;
		if (flag2)
		{
			for (int i = 0; i < this.yArgS.Length; i++)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				bool flag3 = this.isPaintCombine;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[i], this.yDotS[i], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}
		else
		{
			bool flag4 = this.typeCombine == 1;
			if (flag4)
			{
				bool flag5 = !this.isPaintCombine;
				if (flag5)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
				else
				{
					for (int j = 0; j < this.yArgS.Length; j++)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[0], this.yDotS[0], 0, mGraphics.VCENTER | mGraphics.HCENTER);
						SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[1], this.yDotS[1], 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
			}
			else
			{
				bool flag6 = this.typeCombine == 2;
				if (flag6)
				{
					bool flag7 = !this.isPaintCombine;
					if (flag7)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
					else
					{
						for (int k = 0; k < this.yArgS.Length; k++)
						{
							SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[k], this.yDotS[k], 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
				}
				else
				{
					bool flag8 = this.typeCombine == 3;
					if (flag8)
					{
						bool flag9 = !this.isPaintCombine;
						if (flag9)
						{
							SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
						else
						{
							SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
					else
					{
						bool flag10 = this.typeCombine == 4;
						if (flag10)
						{
							bool flag11 = !this.isPaintCombine;
							if (flag11)
							{
								bool flag12 = this.iconID3 != -1;
								if (flag12)
								{
									SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
								}
							}
							else
							{
								for (int l = 0; l < this.iconID.Length; l++)
								{
									SmallImage.drawSmallImage(g, (int)this.iconID[l], this.xDotS[l], this.yDotS[l], 0, mGraphics.VCENTER | mGraphics.HCENTER);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0008A054 File Offset: 0x00088254
	private void setDotStar()
	{
		for (int i = 0; i < this.yArgS.Length; i++)
		{
			bool flag = this.angleS >= 360;
			if (flag)
			{
				this.angleS -= 360;
			}
			bool flag2 = this.angleS < 0;
			if (flag2)
			{
				this.angleS = 360 + this.angleS;
			}
			this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
			this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
			bool flag3 = this.angleS < 90;
			if (flag3)
			{
				this.xDotS[i] = this.xS + this.xArgS[i];
				this.yDotS[i] = this.yS - this.yArgS[i];
			}
			else
			{
				bool flag4 = this.angleS >= 90 && this.angleS < 180;
				if (flag4)
				{
					this.xDotS[i] = this.xS - this.xArgS[i];
					this.yDotS[i] = this.yS - this.yArgS[i];
				}
				else
				{
					bool flag5 = this.angleS >= 180 && this.angleS < 270;
					if (flag5)
					{
						this.xDotS[i] = this.xS - this.xArgS[i];
						this.yDotS[i] = this.yS + this.yArgS[i];
					}
					else
					{
						this.xDotS[i] = this.xS + this.xArgS[i];
						this.yDotS[i] = this.yS + this.yArgS[i];
					}
				}
			}
			this.angleS -= this.iAngleS;
		}
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0008A248 File Offset: 0x00088448
	public void paintCombineNPC(mGraphics g)
	{
		g.translate(-GameScr.cmx, -GameScr.cmy);
		bool flag = this.typeCombine < 3;
		if (flag)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				bool flag2 = npc.template.npcTemplateId == this.idNPC;
				if (flag2)
				{
					npc.paint(g);
					bool flag3 = npc.chatInfo != null;
					if (flag3)
					{
						npc.chatInfo.paint(g, npc.cx, npc.cy - npc.ch - GameCanvas.transY, npc.cdir);
					}
				}
			}
		}
		GameCanvas.resetTrans(g);
		bool flag4 = GameCanvas.gameTick % 4 == 0;
		if (flag4)
		{
			g.drawImage(ItemMap.imageFlare, this.xS - 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS + 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		for (int j = 0; j < Effect2.vEffect3.size(); j++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect3.elementAt(j);
			effect.paint(g);
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0008A3D8 File Offset: 0x000885D8
	public void addTextCombineNPC(int idNPC, string text)
	{
		bool flag = this.typeCombine < 3;
		if (flag)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				bool flag2 = npc.template.npcTemplateId == idNPC;
				if (flag2)
				{
					npc.addInfo(text);
				}
			}
		}
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0008A440 File Offset: 0x00088640
	public void setTypeOption()
	{
		this.type = 19;
		this.setType(0);
		this.setTabOption();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0008A478 File Offset: 0x00088678
	private void setTabOption()
	{
		SoundMn.gI().getStrOption();
		this.currentListLength = Panel.strCauhinh.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0008A54C File Offset: 0x0008874C
	private void paintOption(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strCauhinh.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strCauhinh[i], this.xScroll + 25, num + 6, mFont.LEFT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0008A66C File Offset: 0x0008886C
	private void doFireOption()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			switch (this.selected)
			{
			case 0:
				SoundMn.gI().AuraToolOption();
				break;
			case 1:
				SoundMn.gI().AuraToolOption2();
				break;
			case 2:
				SoundMn.gI().soundToolOption();
				break;
			case 3:
			{
				bool isPC = Main.isPC;
				if (isPC)
				{
					GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				}
				else
				{
					SoundMn.gI().CaseSizeScr();
				}
				break;
			}
			case 4:
			{
				bool isPC2 = Main.isPC;
				if (isPC2)
				{
					GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				}
				else
				{
					SoundMn.gI().CaseAnalog();
				}
				break;
			}
			case 5:
				SoundMn.gI().CaseAnalog();
				break;
			}
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0008A78C File Offset: 0x0008898C
	public void setTypeAccount()
	{
		this.type = 20;
		this.setType(0);
		this.setTabAccount();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0008A7C4 File Offset: 0x000889C4
	private void setTabAccount()
	{
		bool iphoneVersionApp = Main.IphoneVersionApp;
		if (iphoneVersionApp)
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg
			};
			bool canAutoPlay = GameScr.canAutoPlay;
			if (canAutoPlay)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.autoFunction
				};
			}
		}
		else
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg,
				mResources.charger
			};
			bool canAutoPlay2 = GameScr.canAutoPlay;
			if (canAutoPlay2)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger,
					mResources.autoFunction
				};
			}
			bool flag = (mSystem.clientType == 2 || mSystem.clientType == 7) && mResources.language != 2;
			if (flag)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger
				};
				bool canAutoPlay3 = GameScr.canAutoPlay;
				if (canAutoPlay3)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.charger,
						mResources.autoFunction
					};
				}
			}
		}
		this.currentListLength = Panel.strAccount.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag2 = this.cmyLim < 0;
		if (flag2)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag3 = this.cmy < 0;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag4 = this.cmy > this.cmyLim;
		if (flag4)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0008AA2C File Offset: 0x00088C2C
	private void paintAccount(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strAccount.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll;
			if (flag)
			{
				bool flag2 = num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strAccount[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0008AB50 File Offset: 0x00088D50
	private void doFireAccount()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			switch (this.selected)
			{
			case 0:
			{
				GameCanvas.endDlg();
				bool flag2 = this.chatTField == null;
				if (flag2)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.input_Inventory_Pass;
				this.chatTField.tfChat.name = mResources.input_Inventory_Pass;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.isFocus = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				bool flag3 = !Main.isPC;
				if (flag3)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
				bool isWindowsPhone = Main.isWindowsPhone;
				if (isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				break;
			}
			case 1:
				Service.gI().friend(0, -1);
				InfoDlg.showWait();
				break;
			case 2:
				Service.gI().enemy(0, -1);
				InfoDlg.showWait();
				break;
			case 3:
			{
				this.setTypeMessage();
				bool flag4 = this.chatTField == null;
				if (flag4)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				break;
			}
			case 4:
			{
				bool flag5 = mResources.language == 2;
				if (flag5)
				{
					string url = "http://dragonball.indonaga.com/coda/?username=" + GameCanvas.loginScr.tfUser.getText();
					this.hideNow();
					try
					{
						GameMidlet.instance.platformRequest(url);
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
				}
				else
				{
					this.hideNow();
					bool flag6 = global::Char.myCharz().taskMaint.taskId <= 10;
					if (flag6)
					{
						GameCanvas.startOKDlg(mResources.finishBomong);
					}
					else
					{
						MoneyCharge.gI().switchToMe();
					}
				}
				break;
			}
			case 5:
				this.setTypeAuto();
				break;
			}
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0007298D File Offset: 0x00070B8D
	private void updateKeyOption()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0008AE3C File Offset: 0x0008903C
	public void setTypeSpeacialSkill()
	{
		this.type = 25;
		this.setType(0);
		this.setTabSpeacialSkill();
		this.currentTabIndex = 0;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0008AE60 File Offset: 0x00089060
	private void setTabSpeacialSkill()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0008AF34 File Offset: 0x00089134
	public bool isTypeShop()
	{
		return this.type == 1;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0008AF50 File Offset: 0x00089150
	private void doNotiRuby(int type)
	{
		try
		{
			this.currItem.buyRuby = int.Parse(this.chatTField.tfChat.getText());
		}
		catch (Exception ex)
		{
			GameCanvas.startOKDlg(mResources.input_money_wrong);
			this.chatTField.isShow = false;
			return;
		}
		Command cmdYes = new Command(mResources.YES, this, (type != 0) ? 11001 : 11000, null);
		Command cmdNo = new Command(mResources.NO, this, 11002, null);
		GameCanvas.startYesNoDlg(mResources.notiRuby, cmdYes, cmdNo);
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0008AFE8 File Offset: 0x000891E8
	public static void paintUpgradeEffect(int x, int y, int wItem, int hItem, int nline, int cl, mGraphics g)
	{
		try
		{
			int num = (wItem << 1) + (hItem << 1);
			int num2 = num / nline;
			Panel.nsize = Panel.sizeUpgradeEff.Length;
			bool flag = nline > 4;
			if (flag)
			{
				Panel.nsize = 2;
			}
			for (int i = 0; i < nline; i++)
			{
				for (int j = 0; j < Panel.nsize; j++)
				{
					int wSize = (Panel.sizeUpgradeEff[j] <= 1) ? 1 : ((Panel.sizeUpgradeEff[j] >> 1) + 1);
					int x2 = x + Panel.upgradeEffectX(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
					int y2 = y + Panel.upgradeEffectY(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
					g.setColor(Panel.colorUpgradeEffect[cl][j]);
					g.fillRect(x2, y2, Panel.sizeUpgradeEff[j], Panel.sizeUpgradeEff[j]);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0008B0F8 File Offset: 0x000892F8
	private static int upgradeEffectX(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		bool flag = 0 <= num && num < wItem;
		int result;
		if (flag)
		{
			result = num % wItem;
		}
		else
		{
			bool flag2 = wItem <= num && num < wItem + hitem;
			if (flag2)
			{
				result = wItem - wSize;
			}
			else
			{
				bool flag3 = wItem + hitem <= num && num < (wItem << 1) + hitem;
				if (flag3)
				{
					result = wItem - (num - hitem) % wItem - wSize;
				}
				else
				{
					result = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0008B16C File Offset: 0x0008936C
	private static int upgradeEffectY(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		bool flag = 0 <= num && num < wItem;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = wItem <= num && num < wItem + hitem;
			if (flag2)
			{
				result = num % wItem;
			}
			else
			{
				bool flag3 = wItem + hitem <= num && num < (wItem << 1) + hitem;
				if (flag3)
				{
					result = hitem - wSize;
				}
				else
				{
					result = hitem - (num - (wItem << 1)) % hitem - wSize;
				}
			}
		}
		return result;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0008B1E4 File Offset: 0x000893E4
	public static int GetColor_ItemBg(int id)
	{
		int result;
		switch (id)
		{
		case 1:
			result = 2786816;
			break;
		case 2:
			result = 7078041;
			break;
		case 3:
			result = 12537346;
			break;
		case 4:
			result = 1269146;
			break;
		case 5:
			result = 13279744;
			break;
		case 6:
			result = 11599872;
			break;
		default:
			result = -1;
			break;
		}
		return result;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0008B250 File Offset: 0x00089450
	public static sbyte GetColor_Item_Upgrade(int lv)
	{
		bool flag = lv < 0;
		sbyte result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			switch (lv)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				result = 0;
				break;
			case 9:
				result = 4;
				break;
			case 10:
				result = 1;
				break;
			case 11:
				result = 5;
				break;
			case 12:
				result = 3;
				break;
			case 13:
				result = 2;
				break;
			default:
				result = 6;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0008B2CC File Offset: 0x000894CC
	public static mFont GetFont(int color)
	{
		mFont result = mFont.tahoma_7;
		switch (color + 1)
		{
		case 0:
			result = mFont.tahoma_7;
			break;
		case 1:
			result = mFont.tahoma_7b_dark;
			break;
		case 2:
			result = mFont.tahoma_7b_green;
			break;
		case 3:
			result = mFont.tahoma_7b_blue;
			break;
		case 4:
			result = mFont.tahoma_7_red;
			break;
		case 5:
			result = mFont.tahoma_7_green;
			break;
		case 6:
			result = mFont.tahoma_7_blue;
			break;
		case 8:
			result = mFont.tahoma_7b_red;
			break;
		case 9:
			result = mFont.tahoma_7b_yellow;
			break;
		}
		return result;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0008B364 File Offset: 0x00089564
	public void paintOptItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		bool flag = idOpt == 34;
		if (flag)
		{
			bool flag2 = this.imgo_0 != null;
			if (flag2)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			bool flag3 = this.imgo_1 != null;
			if (flag3)
			{
				g.drawImage(this.imgo_1, x, y + h - this.imgo_1.getHeight());
			}
			else
			{
				this.imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
			}
		}
		else
		{
			bool flag4 = idOpt == 35;
			if (flag4)
			{
				bool flag5 = this.imgo_0 != null;
				if (flag5)
				{
					g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
				}
				else
				{
					this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
				}
				bool flag6 = this.imgo_2 != null;
				if (flag6)
				{
					g.drawImage(this.imgo_2, x, y + h - this.imgo_2.getHeight());
				}
				else
				{
					this.imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
				}
			}
			else
			{
				bool flag7 = idOpt == 36;
				if (flag7)
				{
					bool flag8 = this.imgo_0 != null;
					if (flag8)
					{
						g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
					}
					else
					{
						this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
					}
					bool flag9 = this.imgo_3 != null;
					if (flag9)
					{
						g.drawImage(this.imgo_3, x, y + h - this.imgo_3.getHeight());
					}
					else
					{
						this.imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
					}
				}
			}
		}
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0008B544 File Offset: 0x00089744
	public void paintOptSlotItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		bool flag = idOpt == 102 && param > ChatPopup.numSlot;
		if (flag)
		{
			sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(param);
			int nline = param - ChatPopup.numSlot;
			Panel.paintUpgradeEffect(x, y, w, h, nline, (int)color_Item_Upgrade, g);
		}
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0008B588 File Offset: 0x00089788
	public static mFont setTextColor(int id, int type)
	{
		bool flag = type == 0;
		mFont result;
		if (flag)
		{
			switch (id)
			{
			case 0:
				return mFont.bigNumber_While;
			case 1:
				return mFont.bigNumber_green;
			case 3:
				return mFont.bigNumber_orange;
			case 4:
				return mFont.bigNumber_blue;
			case 5:
				return mFont.bigNumber_yellow;
			case 6:
				return mFont.bigNumber_red;
			}
			result = mFont.bigNumber_While;
		}
		else
		{
			switch (id)
			{
			case 0:
				return mFont.tahoma_7b_white;
			case 1:
				return mFont.tahoma_7b_green;
			case 3:
				return mFont.tahoma_7b_yellowSmall2;
			case 4:
				return mFont.tahoma_7b_blue;
			case 5:
				return mFont.tahoma_7b_yellow;
			case 6:
				return mFont.tahoma_7b_red;
			case 7:
				return mFont.tahoma_7b_dark;
			}
			result = mFont.tahoma_7b_white;
		}
		return result;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0008B680 File Offset: 0x00089880
	private bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return subSelect == 0 && num < arrItem.Length;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0008B6A8 File Offset: 0x000898A8
	private int GetInventorySelect_body(int select, int subSelect)
	{
		return select - 1 + subSelect * 20;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0008B6C4 File Offset: 0x000898C4
	private int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return num - arrItem.Length;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0008B6E4 File Offset: 0x000898E4
	private bool isTabInven()
	{
		return (this.type == 0 && this.currentTabIndex == 1) || (this.type == 7 && this.currentTabIndex == 0);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0008B720 File Offset: 0x00089920
	private void updateKeyInvenTab()
	{
		bool flag = this.selected >= 0;
		if (flag)
		{
			bool flag2 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag2)
			{
				this.newSelected--;
				bool flag3 = this.isnewInventory;
				if (flag3)
				{
					this.currentListLength = 5;
				}
				bool flag4 = this.newSelected < 0;
				if (flag4)
				{
					this.newSelected = 0;
					bool isFocusPanel = GameCanvas.isFocusPanel2;
					if (isFocusPanel)
					{
						GameCanvas.isFocusPanel2 = false;
						GameCanvas.panel.selected = 0;
					}
				}
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
				if (flag5)
				{
					this.newSelected++;
					bool flag6 = this.isnewInventory;
					if (flag6)
					{
						this.currentListLength = 5;
					}
					bool flag7 = this.newSelected > (int)(this.size_tab - 1);
					if (flag7)
					{
						this.newSelected = (int)(this.size_tab - 1);
						bool flag8 = GameCanvas.panel2 != null;
						if (flag8)
						{
							GameCanvas.isFocusPanel2 = true;
							GameCanvas.panel2.selected = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0008B83C File Offset: 0x00089A3C
	private void updateKeyInventory()
	{
		this.updateKeyScrollView();
		bool flag = this.selected == 0;
		if (flag)
		{
			this.updateKeyInvenTab();
		}
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0008B868 File Offset: 0x00089A68
	private bool IsTabOption()
	{
		bool flag = this.size_tab > 0;
		if (flag)
		{
			bool flag2 = this.currentTabName.Length > 1;
			if (flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					return true;
				}
			}
			else
			{
				bool flag4 = this.selected >= 0;
				if (flag4)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0008B8C8 File Offset: 0x00089AC8
	private int checkCurrentListLength(int arrLength)
	{
		int num = 20;
		int num2 = arrLength / 20 + ((arrLength % 20 <= 0) ? 0 : 1);
		this.size_tab = (sbyte)num2;
		bool flag = this.newSelected > num2 - 1;
		if (flag)
		{
			this.newSelected = num2 - 1;
		}
		bool flag2 = arrLength % 20 > 0 && this.newSelected == num2 - 1;
		if (flag2)
		{
			num = arrLength % 20;
		}
		return num + 1;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0008B938 File Offset: 0x00089B38
	private void setNewSelected(int arrLength, bool resetSelect)
	{
		int num = arrLength / 20 + ((arrLength % 20 <= 0) ? 0 : 1);
		int num2 = this.xScroll;
		this.newSelected = (GameCanvas.px - num2) / this.TAB_W_NEW;
		bool flag = this.newSelected > num - 1;
		if (flag)
		{
			this.newSelected = num - 1;
		}
		bool flag2 = GameCanvas.px < num2;
		if (flag2)
		{
			this.newSelected = 0;
		}
		this.setTabInventory(resetSelect);
	}

	// Token: 0x04000E7B RID: 3707
	public bool isShow;

	// Token: 0x04000E7C RID: 3708
	public int X;

	// Token: 0x04000E7D RID: 3709
	public int Y;

	// Token: 0x04000E7E RID: 3710
	public int W;

	// Token: 0x04000E7F RID: 3711
	public int H;

	// Token: 0x04000E80 RID: 3712
	public int ITEM_HEIGHT;

	// Token: 0x04000E81 RID: 3713
	public int TAB_W;

	// Token: 0x04000E82 RID: 3714
	public int TAB_W_NEW;

	// Token: 0x04000E83 RID: 3715
	public int cmtoY;

	// Token: 0x04000E84 RID: 3716
	public int cmy;

	// Token: 0x04000E85 RID: 3717
	public int cmdy;

	// Token: 0x04000E86 RID: 3718
	public int cmvy;

	// Token: 0x04000E87 RID: 3719
	public int cmyLim;

	// Token: 0x04000E88 RID: 3720
	public int xc;

	// Token: 0x04000E89 RID: 3721
	public int[] cmyLast;

	// Token: 0x04000E8A RID: 3722
	public int cmtoX;

	// Token: 0x04000E8B RID: 3723
	public int cmx;

	// Token: 0x04000E8C RID: 3724
	public int cmxLim;

	// Token: 0x04000E8D RID: 3725
	public int cmxMap;

	// Token: 0x04000E8E RID: 3726
	public int cmyMap;

	// Token: 0x04000E8F RID: 3727
	public int cmxMapLim;

	// Token: 0x04000E90 RID: 3728
	public int cmyMapLim;

	// Token: 0x04000E91 RID: 3729
	public int cmyQuest;

	// Token: 0x04000E92 RID: 3730
	public static Image imgBantay;

	// Token: 0x04000E93 RID: 3731
	public static Image imgX;

	// Token: 0x04000E94 RID: 3732
	public static Image imgMap;

	// Token: 0x04000E95 RID: 3733
	public TabClanIcon tabIcon;

	// Token: 0x04000E96 RID: 3734
	public MyVector vItemCombine = new MyVector();

	// Token: 0x04000E97 RID: 3735
	public int moneyGD;

	// Token: 0x04000E98 RID: 3736
	public int friendMoneyGD;

	// Token: 0x04000E99 RID: 3737
	public bool isLock;

	// Token: 0x04000E9A RID: 3738
	public bool isFriendLock;

	// Token: 0x04000E9B RID: 3739
	public bool isAccept;

	// Token: 0x04000E9C RID: 3740
	public bool isFriendAccep;

	// Token: 0x04000E9D RID: 3741
	public string topName;

	// Token: 0x04000E9E RID: 3742
	public ChatTextField chatTField;

	// Token: 0x04000E9F RID: 3743
	public static string specialInfo;

	// Token: 0x04000EA0 RID: 3744
	public static short spearcialImage;

	// Token: 0x04000EA1 RID: 3745
	public static Image imgStar;

	// Token: 0x04000EA2 RID: 3746
	public static Image imgMaxStar;

	// Token: 0x04000EA3 RID: 3747
	public static Image imgStar8;

	// Token: 0x04000EA4 RID: 3748
	public static Image imgNew;

	// Token: 0x04000EA5 RID: 3749
	public static Image imgXu;

	// Token: 0x04000EA6 RID: 3750
	public static Image imgTicket;

	// Token: 0x04000EA7 RID: 3751
	public static Image imgLuong;

	// Token: 0x04000EA8 RID: 3752
	public static Image imgLuongKhoa;

	// Token: 0x04000EA9 RID: 3753
	private static Image imgUp;

	// Token: 0x04000EAA RID: 3754
	private static Image imgDown;

	// Token: 0x04000EAB RID: 3755
	private int pa1;

	// Token: 0x04000EAC RID: 3756
	private int pa2;

	// Token: 0x04000EAD RID: 3757
	private bool trans;

	// Token: 0x04000EAE RID: 3758
	private int pX;

	// Token: 0x04000EAF RID: 3759
	private int pY;

	// Token: 0x04000EB0 RID: 3760
	private Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000EB1 RID: 3761
	public int type;

	// Token: 0x04000EB2 RID: 3762
	public int currentTabIndex;

	// Token: 0x04000EB3 RID: 3763
	public int startTabPos;

	// Token: 0x04000EB4 RID: 3764
	public int[] lastTabIndex;

	// Token: 0x04000EB5 RID: 3765
	public string[][] currentTabName;

	// Token: 0x04000EB6 RID: 3766
	private int[] currClanOption;

	// Token: 0x04000EB7 RID: 3767
	public int mainTabPos = 4;

	// Token: 0x04000EB8 RID: 3768
	public int shopTabPos = 50;

	// Token: 0x04000EB9 RID: 3769
	public int boxTabPos = 50;

	// Token: 0x04000EBA RID: 3770
	public string[][] mainTabName;

	// Token: 0x04000EBB RID: 3771
	public string[] mapNames;

	// Token: 0x04000EBC RID: 3772
	public string[] planetNames;

	// Token: 0x04000EBD RID: 3773
	public static string[] strTool = new string[]
	{
		mResources.gameInfo,
		mResources.change_flag,
		mResources.change_zone,
		mResources.chat_world,
		mResources.account,
		mResources.option,
		mResources.change_account
	};

	// Token: 0x04000EBE RID: 3774
	public static string[] strCauhinh = new string[]
	{
		(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
		mResources.increase_vga,
		mResources.analog,
		(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
	};

	// Token: 0x04000EBF RID: 3775
	public static string[] strAccount = new string[]
	{
		mResources.inventory_Pass,
		mResources.friend,
		mResources.enemy,
		mResources.msg,
		mResources.charger
	};

	// Token: 0x04000EC0 RID: 3776
	public static string[] strAuto = new string[]
	{
		mResources.useGem
	};

	// Token: 0x04000EC1 RID: 3777
	public static int graphics = 0;

	// Token: 0x04000EC2 RID: 3778
	public string[][] shopTabName;

	// Token: 0x04000EC3 RID: 3779
	public int[] maxPageShop;

	// Token: 0x04000EC4 RID: 3780
	public int[] currPageShop;

	// Token: 0x04000EC5 RID: 3781
	private static string[][] boxTabName = new string[][]
	{
		mResources.chestt,
		mResources.inventory
	};

	// Token: 0x04000EC6 RID: 3782
	private static string[][] boxCombine = new string[][]
	{
		mResources.combine,
		mResources.inventory
	};

	// Token: 0x04000EC7 RID: 3783
	private static string[][] boxZone = new string[][]
	{
		mResources.zonee
	};

	// Token: 0x04000EC8 RID: 3784
	private static string[][] boxMap = new string[][]
	{
		mResources.mapp
	};

	// Token: 0x04000EC9 RID: 3785
	private static string[][] boxGD = new string[][]
	{
		mResources.inventory,
		mResources.item_give,
		mResources.item_receive
	};

	// Token: 0x04000ECA RID: 3786
	private static string[][] boxPet = mResources.petMainTab;

	// Token: 0x04000ECB RID: 3787
	public string[][][] tabName = new string[][][]
	{
		null,
		null,
		Panel.boxTabName,
		Panel.boxZone,
		Panel.boxMap,
		null,
		null,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		Panel.boxCombine,
		Panel.boxGD,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		Panel.boxPet,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		}
	};

	// Token: 0x04000ECC RID: 3788
	private static sbyte BOX_BAG = 0;

	// Token: 0x04000ECD RID: 3789
	private static sbyte BAG_BOX = 1;

	// Token: 0x04000ECE RID: 3790
	private static sbyte BOX_BODY = 2;

	// Token: 0x04000ECF RID: 3791
	private static sbyte BODY_BOX = 3;

	// Token: 0x04000ED0 RID: 3792
	private static sbyte BAG_BODY = 4;

	// Token: 0x04000ED1 RID: 3793
	private static sbyte BODY_BAG = 5;

	// Token: 0x04000ED2 RID: 3794
	private static sbyte BAG_PET = 6;

	// Token: 0x04000ED3 RID: 3795
	private static sbyte PET_BAG = 7;

	// Token: 0x04000ED4 RID: 3796
	public int hasUse;

	// Token: 0x04000ED5 RID: 3797
	public int hasUseBag;

	// Token: 0x04000ED6 RID: 3798
	public int currentListLength;

	// Token: 0x04000ED7 RID: 3799
	private int[] lastSelect;

	// Token: 0x04000ED8 RID: 3800
	public static int[] mapIdTraidat = new int[]
	{
		21,
		0,
		1,
		2,
		24,
		3,
		4,
		5,
		6,
		27,
		28,
		29,
		30,
		42,
		47,
		46
	};

	// Token: 0x04000ED9 RID: 3801
	public static int[] mapXTraidat = new int[]
	{
		39,
		42,
		105,
		93,
		61,
		93,
		142,
		165,
		210,
		100,
		165,
		220,
		233,
		10,
		125,
		125
	};

	// Token: 0x04000EDA RID: 3802
	public static int[] mapYTraidat = new int[]
	{
		28,
		60,
		48,
		96,
		88,
		131,
		136,
		95,
		32,
		200,
		189,
		167,
		120,
		110,
		20,
		20
	};

	// Token: 0x04000EDB RID: 3803
	public static int[] mapIdNamek = new int[]
	{
		22,
		7,
		8,
		9,
		25,
		11,
		12,
		13,
		10,
		31,
		32,
		33,
		34,
		43
	};

	// Token: 0x04000EDC RID: 3804
	public static int[] mapXNamek = new int[]
	{
		55,
		30,
		93,
		80,
		24,
		149,
		219,
		220,
		233,
		170,
		148,
		195,
		148,
		10
	};

	// Token: 0x04000EDD RID: 3805
	public static int[] mapYNamek = new int[]
	{
		136,
		84,
		69,
		34,
		25,
		42,
		32,
		110,
		192,
		70,
		106,
		156,
		210,
		57
	};

	// Token: 0x04000EDE RID: 3806
	public static int[] mapIdSaya = new int[]
	{
		23,
		14,
		15,
		16,
		26,
		17,
		18,
		20,
		19,
		35,
		36,
		37,
		38,
		44
	};

	// Token: 0x04000EDF RID: 3807
	public static int[] mapXSaya = new int[]
	{
		90,
		95,
		144,
		234,
		231,
		122,
		176,
		158,
		205,
		54,
		105,
		159,
		231,
		27
	};

	// Token: 0x04000EE0 RID: 3808
	public static int[] mapYSaya = new int[]
	{
		10,
		43,
		20,
		36,
		69,
		87,
		112,
		167,
		160,
		151,
		173,
		207,
		194,
		29
	};

	// Token: 0x04000EE1 RID: 3809
	public static int[][] mapId = new int[][]
	{
		Panel.mapIdTraidat,
		Panel.mapIdNamek,
		Panel.mapIdSaya
	};

	// Token: 0x04000EE2 RID: 3810
	public static int[][] mapX = new int[][]
	{
		Panel.mapXTraidat,
		Panel.mapXNamek,
		Panel.mapXSaya
	};

	// Token: 0x04000EE3 RID: 3811
	public static int[][] mapY = new int[][]
	{
		Panel.mapYTraidat,
		Panel.mapYNamek,
		Panel.mapYSaya
	};

	// Token: 0x04000EE4 RID: 3812
	public Item currItem;

	// Token: 0x04000EE5 RID: 3813
	public Clan currClan;

	// Token: 0x04000EE6 RID: 3814
	public ClanMessage currMess;

	// Token: 0x04000EE7 RID: 3815
	public Member currMem;

	// Token: 0x04000EE8 RID: 3816
	public Clan[] clans;

	// Token: 0x04000EE9 RID: 3817
	public MyVector member;

	// Token: 0x04000EEA RID: 3818
	public MyVector myMember;

	// Token: 0x04000EEB RID: 3819
	public MyVector logChat = new MyVector();

	// Token: 0x04000EEC RID: 3820
	public MyVector vPlayerMenu = new MyVector();

	// Token: 0x04000EED RID: 3821
	public MyVector vFriend = new MyVector();

	// Token: 0x04000EEE RID: 3822
	public MyVector vMyGD = new MyVector();

	// Token: 0x04000EEF RID: 3823
	public MyVector vFriendGD = new MyVector();

	// Token: 0x04000EF0 RID: 3824
	public MyVector vTop = new MyVector();

	// Token: 0x04000EF1 RID: 3825
	public MyVector vEnemy = new MyVector();

	// Token: 0x04000EF2 RID: 3826
	public MyVector vFlag = new MyVector();

	// Token: 0x04000EF3 RID: 3827
	public MyVector vPlayerMenu_id = new MyVector();

	// Token: 0x04000EF4 RID: 3828
	public Command cmdClose;

	// Token: 0x04000EF5 RID: 3829
	public static bool CanNapTien = false;

	// Token: 0x04000EF6 RID: 3830
	public static int WIDTH_PANEL = 240;

	// Token: 0x04000EF7 RID: 3831
	private int position;

	// Token: 0x04000EF8 RID: 3832
	public string playerChat;

	// Token: 0x04000EF9 RID: 3833
	public Dictionary<string, Panel.PlayerChat> chats = new Dictionary<string, Panel.PlayerChat>();

	// Token: 0x04000EFA RID: 3834
	public global::Char charMenu;

	// Token: 0x04000EFB RID: 3835
	private bool isThachDau;

	// Token: 0x04000EFC RID: 3836
	public int typeShop = -1;

	// Token: 0x04000EFD RID: 3837
	public int xScroll;

	// Token: 0x04000EFE RID: 3838
	public int yScroll;

	// Token: 0x04000EFF RID: 3839
	public int wScroll;

	// Token: 0x04000F00 RID: 3840
	public int hScroll;

	// Token: 0x04000F01 RID: 3841
	public ChatPopup cp;

	// Token: 0x04000F02 RID: 3842
	public int idIcon;

	// Token: 0x04000F03 RID: 3843
	public int[] partID;

	// Token: 0x04000F04 RID: 3844
	private int timeShow;

	// Token: 0x04000F05 RID: 3845
	public bool isBoxClan;

	// Token: 0x04000F06 RID: 3846
	public int w;

	// Token: 0x04000F07 RID: 3847
	private int pa;

	// Token: 0x04000F08 RID: 3848
	public int selected;

	// Token: 0x04000F09 RID: 3849
	private int cSelected;

	// Token: 0x04000F0A RID: 3850
	private int newSelected;

	// Token: 0x04000F0B RID: 3851
	private bool isClanOption;

	// Token: 0x04000F0C RID: 3852
	public bool isSearchClan;

	// Token: 0x04000F0D RID: 3853
	public bool isMessage;

	// Token: 0x04000F0E RID: 3854
	public bool isViewMember;

	// Token: 0x04000F0F RID: 3855
	public const int TYPE_MAIN = 0;

	// Token: 0x04000F10 RID: 3856
	public const int TYPE_SHOP = 1;

	// Token: 0x04000F11 RID: 3857
	public const int TYPE_BOX = 2;

	// Token: 0x04000F12 RID: 3858
	public const int TYPE_ZONE = 3;

	// Token: 0x04000F13 RID: 3859
	public const int TYPE_MAP = 4;

	// Token: 0x04000F14 RID: 3860
	public const int TYPE_CLANS = 5;

	// Token: 0x04000F15 RID: 3861
	public const int TYPE_INFOMATION = 6;

	// Token: 0x04000F16 RID: 3862
	public const int TYPE_BODY = 7;

	// Token: 0x04000F17 RID: 3863
	public const int TYPE_MESS = 8;

	// Token: 0x04000F18 RID: 3864
	public const int TYPE_ARCHIVEMENT = 9;

	// Token: 0x04000F19 RID: 3865
	public const int PLAYER_MENU = 10;

	// Token: 0x04000F1A RID: 3866
	public const int TYPE_FRIEND = 11;

	// Token: 0x04000F1B RID: 3867
	public const int TYPE_COMBINE = 12;

	// Token: 0x04000F1C RID: 3868
	public const int TYPE_GIAODICH = 13;

	// Token: 0x04000F1D RID: 3869
	public const int TYPE_MAPTRANS = 14;

	// Token: 0x04000F1E RID: 3870
	public const int TYPE_TOP = 15;

	// Token: 0x04000F1F RID: 3871
	public const int TYPE_ENEMY = 16;

	// Token: 0x04000F20 RID: 3872
	public const int TYPE_KIGUI = 17;

	// Token: 0x04000F21 RID: 3873
	public const int TYPE_FLAG = 18;

	// Token: 0x04000F22 RID: 3874
	public const int TYPE_OPTION = 19;

	// Token: 0x04000F23 RID: 3875
	public const int TYPE_ACCOUNT = 20;

	// Token: 0x04000F24 RID: 3876
	public const int TYPE_PET_MAIN = 21;

	// Token: 0x04000F25 RID: 3877
	public const int TYPE_AUTO = 22;

	// Token: 0x04000F26 RID: 3878
	public const int TYPE_GAMEINFO = 23;

	// Token: 0x04000F27 RID: 3879
	public const int TYPE_GAMEINFOSUB = 24;

	// Token: 0x04000F28 RID: 3880
	public const int TYPE_SPEACIALSKILL = 25;

	// Token: 0x04000F29 RID: 3881
	private int pointerDownTime;

	// Token: 0x04000F2A RID: 3882
	private int pointerDownFirstX;

	// Token: 0x04000F2B RID: 3883
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000F2C RID: 3884
	private bool pointerIsDowning;

	// Token: 0x04000F2D RID: 3885
	private bool isDownWhenRunning;

	// Token: 0x04000F2E RID: 3886
	private bool wantUpdateList;

	// Token: 0x04000F2F RID: 3887
	private int waitToPerform;

	// Token: 0x04000F30 RID: 3888
	private int cmRun;

	// Token: 0x04000F31 RID: 3889
	private int keyTouchLock = -1;

	// Token: 0x04000F32 RID: 3890
	private int keyToundGD = -1;

	// Token: 0x04000F33 RID: 3891
	private int keyTouchCombine = -1;

	// Token: 0x04000F34 RID: 3892
	private int keyTouchMapButton = -1;

	// Token: 0x04000F35 RID: 3893
	public int indexMouse = -1;

	// Token: 0x04000F36 RID: 3894
	private bool justRelease;

	// Token: 0x04000F37 RID: 3895
	private int keyTouchTab = -1;

	// Token: 0x04000F38 RID: 3896
	private int nTableItem;

	// Token: 0x04000F39 RID: 3897
	public string[][] clansOption = new string[][]
	{
		mResources.findClan,
		mResources.createClan
	};

	// Token: 0x04000F3A RID: 3898
	public string clanInfo = string.Empty;

	// Token: 0x04000F3B RID: 3899
	public string clanReport = string.Empty;

	// Token: 0x04000F3C RID: 3900
	private bool isHaveClan;

	// Token: 0x04000F3D RID: 3901
	private Scroll scroll;

	// Token: 0x04000F3E RID: 3902
	private int cmvx;

	// Token: 0x04000F3F RID: 3903
	private int cmdx;

	// Token: 0x04000F40 RID: 3904
	private bool isSelectPlayerMenu;

	// Token: 0x04000F41 RID: 3905
	private string[] strStatus = new string[]
	{
		mResources.follow,
		mResources.defend,
		mResources.attack,
		mResources.gohome,
		mResources.fusion,
		mResources.fusionForever
	};

	// Token: 0x04000F42 RID: 3906
	private static string log;

	// Token: 0x04000F43 RID: 3907
	private int tt;

	// Token: 0x04000F44 RID: 3908
	private int currentButtonPress;

	// Token: 0x04000F45 RID: 3909
	public static long[] t_tiemnang = new long[]
	{
		50000000L,
		250000000L,
		1250000000L,
		5000000000L,
		15000000000L,
		30000000000L,
		45000000000L,
		60000000000L,
		75000000000L,
		90000000000L,
		110000000000L,
		130000000000L,
		150000000000L,
		170000000000L
	};

	// Token: 0x04000F46 RID: 3910
	private int[] zoneColor = new int[]
	{
		43520,
		14743570,
		14155776
	};

	// Token: 0x04000F47 RID: 3911
	public string[] combineInfo;

	// Token: 0x04000F48 RID: 3912
	public string[] combineTopInfo;

	// Token: 0x04000F49 RID: 3913
	public static int[] color1 = new int[]
	{
		2327248,
		8982199,
		16713222
	};

	// Token: 0x04000F4A RID: 3914
	public static int[] color2 = new int[]
	{
		4583423,
		16719103,
		16714764
	};

	// Token: 0x04000F4B RID: 3915
	private int sellectInventory;

	// Token: 0x04000F4C RID: 3916
	private Item itemInvenNew;

	// Token: 0x04000F4D RID: 3917
	private Effect eBanner;

	// Token: 0x04000F4E RID: 3918
	private static FrameImage screenTab6;

	// Token: 0x04000F4F RID: 3919
	private bool isUp;

	// Token: 0x04000F50 RID: 3920
	private int compare;

	// Token: 0x04000F51 RID: 3921
	public static string strWantToBuy = string.Empty;

	// Token: 0x04000F52 RID: 3922
	public int xstart;

	// Token: 0x04000F53 RID: 3923
	public int ystart;

	// Token: 0x04000F54 RID: 3924
	public int popupW = 140;

	// Token: 0x04000F55 RID: 3925
	public int popupH = 160;

	// Token: 0x04000F56 RID: 3926
	public int cmySK;

	// Token: 0x04000F57 RID: 3927
	public int cmtoYSK;

	// Token: 0x04000F58 RID: 3928
	public int cmdySK;

	// Token: 0x04000F59 RID: 3929
	public int cmvySK;

	// Token: 0x04000F5A RID: 3930
	public int cmyLimSK;

	// Token: 0x04000F5B RID: 3931
	public int popupY;

	// Token: 0x04000F5C RID: 3932
	public int popupX;

	// Token: 0x04000F5D RID: 3933
	public int isborderIndex;

	// Token: 0x04000F5E RID: 3934
	public int isselectedRow;

	// Token: 0x04000F5F RID: 3935
	public int indexSize = 28;

	// Token: 0x04000F60 RID: 3936
	public int indexTitle;

	// Token: 0x04000F61 RID: 3937
	public int indexSelect;

	// Token: 0x04000F62 RID: 3938
	public int indexRow = -1;

	// Token: 0x04000F63 RID: 3939
	public int indexRowMax;

	// Token: 0x04000F64 RID: 3940
	public int indexMenu;

	// Token: 0x04000F65 RID: 3941
	public int columns = 6;

	// Token: 0x04000F66 RID: 3942
	public int rows;

	// Token: 0x04000F67 RID: 3943
	public int inforX;

	// Token: 0x04000F68 RID: 3944
	public int inforY;

	// Token: 0x04000F69 RID: 3945
	public int inforW;

	// Token: 0x04000F6A RID: 3946
	public int inforH;

	// Token: 0x04000F6B RID: 3947
	private int yPaint;

	// Token: 0x04000F6C RID: 3948
	private int xMap;

	// Token: 0x04000F6D RID: 3949
	private int yMap;

	// Token: 0x04000F6E RID: 3950
	private int xMapTask;

	// Token: 0x04000F6F RID: 3951
	private int yMapTask;

	// Token: 0x04000F70 RID: 3952
	private int xMove;

	// Token: 0x04000F71 RID: 3953
	private int yMove;

	// Token: 0x04000F72 RID: 3954
	public static bool isPaintMap = true;

	// Token: 0x04000F73 RID: 3955
	public bool isClose;

	// Token: 0x04000F74 RID: 3956
	private int infoSelect;

	// Token: 0x04000F75 RID: 3957
	public static MyVector vGameInfo = new MyVector(string.Empty);

	// Token: 0x04000F76 RID: 3958
	public static string[] contenInfo;

	// Token: 0x04000F77 RID: 3959
	public bool isViewChatServer;

	// Token: 0x04000F78 RID: 3960
	private int currInfoItem;

	// Token: 0x04000F79 RID: 3961
	public global::Char charInfo;

	// Token: 0x04000F7A RID: 3962
	private bool isChangeZone;

	// Token: 0x04000F7B RID: 3963
	private bool isKiguiXu;

	// Token: 0x04000F7C RID: 3964
	private bool isKiguiLuong;

	// Token: 0x04000F7D RID: 3965
	private int delayKigui;

	// Token: 0x04000F7E RID: 3966
	public sbyte combineSuccess = -1;

	// Token: 0x04000F7F RID: 3967
	public int idNPC;

	// Token: 0x04000F80 RID: 3968
	public int xS;

	// Token: 0x04000F81 RID: 3969
	public int yS;

	// Token: 0x04000F82 RID: 3970
	private int rS;

	// Token: 0x04000F83 RID: 3971
	private int angleS;

	// Token: 0x04000F84 RID: 3972
	private int angleO;

	// Token: 0x04000F85 RID: 3973
	private int iAngleS;

	// Token: 0x04000F86 RID: 3974
	private int iDotS;

	// Token: 0x04000F87 RID: 3975
	private int speed;

	// Token: 0x04000F88 RID: 3976
	private int[] xArgS;

	// Token: 0x04000F89 RID: 3977
	private int[] yArgS;

	// Token: 0x04000F8A RID: 3978
	private int[] xDotS;

	// Token: 0x04000F8B RID: 3979
	private int[] yDotS;

	// Token: 0x04000F8C RID: 3980
	private int time;

	// Token: 0x04000F8D RID: 3981
	private int typeCombine;

	// Token: 0x04000F8E RID: 3982
	private int countUpdate;

	// Token: 0x04000F8F RID: 3983
	private int countR;

	// Token: 0x04000F90 RID: 3984
	private int countWait;

	// Token: 0x04000F91 RID: 3985
	private bool isSpeedCombine;

	// Token: 0x04000F92 RID: 3986
	private bool isCompleteEffCombine = true;

	// Token: 0x04000F93 RID: 3987
	private bool isPaintCombine;

	// Token: 0x04000F94 RID: 3988
	public bool isDoneCombine = true;

	// Token: 0x04000F95 RID: 3989
	public short iconID1;

	// Token: 0x04000F96 RID: 3990
	public short iconID2;

	// Token: 0x04000F97 RID: 3991
	public short iconID3;

	// Token: 0x04000F98 RID: 3992
	public short[] iconID;

	// Token: 0x04000F99 RID: 3993
	public string[][] speacialTabName;

	// Token: 0x04000F9A RID: 3994
	public static int[] sizeUpgradeEff = new int[]
	{
		2,
		1,
		1
	};

	// Token: 0x04000F9B RID: 3995
	public static int nsize = 1;

	// Token: 0x04000F9C RID: 3996
	public const sbyte COLOR_WHITE = 0;

	// Token: 0x04000F9D RID: 3997
	public const sbyte COLOR_GREEN = 1;

	// Token: 0x04000F9E RID: 3998
	public const sbyte COLOR_PURPLE = 2;

	// Token: 0x04000F9F RID: 3999
	public const sbyte COLOR_ORANGE = 3;

	// Token: 0x04000FA0 RID: 4000
	public const sbyte COLOR_BLUE = 4;

	// Token: 0x04000FA1 RID: 4001
	public const sbyte COLOR_YELLOW = 5;

	// Token: 0x04000FA2 RID: 4002
	public const sbyte COLOR_RED = 6;

	// Token: 0x04000FA3 RID: 4003
	public const sbyte COLOR_BLACK = 7;

	// Token: 0x04000FA4 RID: 4004
	public static int[][] colorUpgradeEffect = new int[][]
	{
		new int[]
		{
			16777215,
			15000805,
			13487823,
			11711155,
			9671828,
			7895160
		},
		new int[]
		{
			61952,
			58624,
			52224,
			45824,
			39168,
			32768
		},
		new int[]
		{
			13500671,
			12058853,
			10682572,
			9371827,
			7995545,
			6684800
		},
		new int[]
		{
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			37119,
			33509,
			28108,
			24499,
			21145,
			17536
		},
		new int[]
		{
			16776192,
			15063040,
			12635136,
			11776256,
			10063872,
			8290304
		},
		new int[]
		{
			16711680,
			15007744,
			13369344,
			11730944,
			10027008,
			8388608
		}
	};

	// Token: 0x04000FA5 RID: 4005
	public const int color_item_white = 15987701;

	// Token: 0x04000FA6 RID: 4006
	public const int color_item_green = 2786816;

	// Token: 0x04000FA7 RID: 4007
	public const int color_item_purple = 7078041;

	// Token: 0x04000FA8 RID: 4008
	public const int color_item_orange = 12537346;

	// Token: 0x04000FA9 RID: 4009
	public const int color_item_blue = 1269146;

	// Token: 0x04000FAA RID: 4010
	public const int color_item_yellow = 13279744;

	// Token: 0x04000FAB RID: 4011
	public const int color_item_red = 11599872;

	// Token: 0x04000FAC RID: 4012
	public const int color_item_black = 2039326;

	// Token: 0x04000FAD RID: 4013
	private Image imgo_0;

	// Token: 0x04000FAE RID: 4014
	private Image imgo_1;

	// Token: 0x04000FAF RID: 4015
	private Image imgo_2;

	// Token: 0x04000FB0 RID: 4016
	private Image imgo_3;

	// Token: 0x04000FB1 RID: 4017
	public const int numItem = 20;

	// Token: 0x04000FB2 RID: 4018
	public const sbyte INVENTORY_TAB = 1;

	// Token: 0x04000FB3 RID: 4019
	public sbyte size_tab;

	// Token: 0x04000FB4 RID: 4020
	private bool isnewInventory;

	// Token: 0x020000CA RID: 202
	public class PlayerChat
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x000AFB30 File Offset: 0x000ADD30
		public PlayerChat(string name, int charId)
		{
			this.name = name;
			this.charID = charId;
			this.isNewMessage = true;
		}

		// Token: 0x040014E7 RID: 5351
		public string name;

		// Token: 0x040014E8 RID: 5352
		public int charID;

		// Token: 0x040014E9 RID: 5353
		public bool isNewMessage;

		// Token: 0x040014EA RID: 5354
		public List<InfoItem> chats = new List<InfoItem>();
	}
}
