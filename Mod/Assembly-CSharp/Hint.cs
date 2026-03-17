using System;

// Token: 0x0200003B RID: 59
public class Hint
{
	// Token: 0x0600039F RID: 927 RVA: 0x00053D10 File Offset: 0x00051F10
	public static bool isOnTask(int tastId, int index)
	{
		return global::Char.myCharz().taskMaint != null && (int)global::Char.myCharz().taskMaint.taskId == tastId && global::Char.myCharz().taskMaint.index == index;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00053D58 File Offset: 0x00051F58
	public static bool isPaintz()
	{
		return (!Hint.isOnTask(0, 3) || GameCanvas.panel.currentTabIndex != 0 || (GameCanvas.panel.cmy >= 0 && GameCanvas.panel.cmy <= 30)) && (!Hint.isOnTask(2, 0) || !GameCanvas.panel.isShow || GameCanvas.panel.currentTabIndex == 0);
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00053DC4 File Offset: 0x00051FC4
	public static void clickNpc()
	{
		bool flag = GameCanvas.panel.isShow;
		if (flag)
		{
			Hint.isPaint = false;
		}
		bool flag2 = GameScr.getNpcTask() != null;
		if (flag2)
		{
			Hint.x = GameScr.getNpcTask().cx;
			Hint.y = GameScr.getNpcTask().cy;
			Hint.trans = 0;
			Hint.isCamera = true;
			Hint.type = ((!GameCanvas.isTouch) ? 0 : 1);
		}
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00053E30 File Offset: 0x00052030
	public static void nextMap(int index)
	{
		bool flag = GameCanvas.panel.isShow;
		if (!flag)
		{
			bool flag2 = PopUp.vPopups.size() - 1 < index;
			if (!flag2)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt(index);
				Hint.x = popUp.cx + popUp.sayWidth / 2;
				Hint.y = popUp.cy + 30;
				bool flag3 = popUp.isHide || !popUp.isPaint;
				if (flag3)
				{
					Hint.isPaint = false;
				}
				else
				{
					Hint.isPaint = true;
				}
				Hint.type = 0;
				Hint.isCamera = true;
				Hint.trans = 0;
				bool flag4 = !GameCanvas.isTouch;
				if (flag4)
				{
					Hint.isPaint = false;
				}
			}
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00053EF0 File Offset: 0x000520F0
	public static void clickMob()
	{
		Hint.type = 1;
		bool flag2 = GameCanvas.panel.isShow;
		if (flag2)
		{
			Hint.isPaint = false;
		}
		bool flag = false;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool isHintFocus = mob.isHintFocus;
			if (isHintFocus)
			{
				flag = true;
				break;
			}
		}
		for (int j = 0; j < GameScr.vMob.size(); j++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
			bool isHintFocus2 = mob2.isHintFocus;
			if (isHintFocus2)
			{
				Hint.x = mob2.x;
				Hint.y = mob2.y + 5;
				Hint.isCamera = true;
				bool flag3 = mob2.status == 0;
				if (flag3)
				{
					mob2.isHintFocus = false;
				}
				break;
			}
			bool flag4 = !flag;
			if (flag4)
			{
				bool flag5 = mob2.status != 0;
				if (flag5)
				{
					mob2.isHintFocus = true;
					break;
				}
				mob2.isHintFocus = false;
			}
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00054010 File Offset: 0x00052210
	public static bool isHaveItem()
	{
		bool flag = GameCanvas.panel.isShow;
		if (flag)
		{
			Hint.isPaint = false;
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			bool flag2 = itemMap.playerId == global::Char.myCharz().charID && itemMap.template.id == 73;
			if (flag2)
			{
				Hint.type = 1;
				Hint.x = itemMap.x;
				Hint.y = itemMap.y + 5;
				Hint.isCamera = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x000540BC File Offset: 0x000522BC
	public static void paintArrowPointToHint(mGraphics g)
	{
		try
		{
			bool flag = Hint.isPaintArrow;
			if (flag)
			{
				bool flag2 = Hint.x <= GameScr.cmx || Hint.x >= GameScr.cmx + GameScr.gW || Hint.y <= GameScr.cmy || Hint.y >= GameScr.cmy + GameScr.gH;
				if (flag2)
				{
					bool flag3 = GameCanvas.gameTick % 10 >= 5;
					if (flag3)
					{
						bool flag4 = ChatPopup.currChatPopup == null;
						if (flag4)
						{
							bool flag5 = ChatPopup.serverChatPopUp == null;
							if (flag5)
							{
								bool flag6 = !GameCanvas.panel.isShow;
								if (flag6)
								{
									bool flag7 = Hint.isCamera;
									if (flag7)
									{
										int num = Hint.x - global::Char.myCharz().cx;
										int num2 = Hint.y - global::Char.myCharz().cy;
										int num3 = 0;
										int num4 = 0;
										int arg = 0;
										bool flag8 = num > 0 && num2 >= 0;
										if (flag8)
										{
											bool flag9 = Res.abs(num) >= Res.abs(num2);
											if (flag9)
											{
												num3 = GameScr.gW - 10;
												num4 = GameScr.gH / 2 + 30;
												bool isTouch = GameCanvas.isTouch;
												if (isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 0;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = GameScr.gH - 10;
												arg = 5;
											}
										}
										else
										{
											bool flag10 = num >= 0 && num2 < 0;
											if (flag10)
											{
												bool flag11 = Res.abs(num) >= Res.abs(num2);
												if (flag11)
												{
													num3 = GameScr.gW - 10;
													num4 = GameScr.gH / 2 + 30;
													bool isTouch2 = GameCanvas.isTouch;
													if (isTouch2)
													{
														num4 = GameScr.gH / 2 + 10;
													}
													arg = 0;
												}
												else
												{
													num3 = GameScr.gW / 2;
													num4 = 10;
													arg = 6;
												}
											}
										}
										bool flag12 = num < 0 && num2 >= 0;
										if (flag12)
										{
											bool flag13 = Res.abs(num) >= Res.abs(num2);
											if (flag13)
											{
												num3 = 10;
												num4 = GameScr.gH / 2 + 30;
												bool isTouch3 = GameCanvas.isTouch;
												if (isTouch3)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 3;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = GameScr.gH - 10;
												arg = 5;
											}
										}
										else
										{
											bool flag14 = num <= 0 && num2 < 0;
											if (flag14)
											{
												bool flag15 = Res.abs(num) >= Res.abs(num2);
												if (flag15)
												{
													num3 = 10;
													num4 = GameScr.gH / 2 + 30;
													bool isTouch4 = GameCanvas.isTouch;
													if (isTouch4)
													{
														num4 = GameScr.gH / 2 + 10;
													}
													arg = 3;
												}
												else
												{
													num3 = GameScr.gW / 2;
													num4 = 10;
													arg = 6;
												}
											}
										}
										GameScr.resetTranslate(g);
										g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, num3, num4, StaticObj.VCENTER_HCENTER);
									}
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
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x000543E0 File Offset: 0x000525E0
	public static void paint(mGraphics g)
	{
		bool flag = ChatPopup.serverChatPopUp != null;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().isUsePlane || global::Char.myCharz().isTeleport;
			if (!flag2)
			{
				Hint.paintArrowPointToHint(g);
				bool flag3 = GameCanvas.menu.tDelay != 0;
				if (!flag3)
				{
					bool flag4 = !Hint.isPaint;
					if (!flag4)
					{
						bool flag5 = ChatPopup.scr != null;
						if (!flag5)
						{
							bool ischangingMap = global::Char.ischangingMap;
							if (!ischangingMap)
							{
								bool flag6 = GameCanvas.currentScreen != GameScr.gI();
								if (!flag6)
								{
									bool flag7 = GameCanvas.panel.isShow && GameCanvas.panel.cmx != 0;
									if (!flag7)
									{
										bool flag8 = Hint.isCamera;
										if (flag8)
										{
											g.translate(-GameScr.cmx, -GameScr.cmy);
										}
										bool flag9 = Hint.trans == 0;
										if (flag9)
										{
											g.drawImage(Panel.imgBantay, Hint.x - 15, Hint.y, 0);
										}
										bool flag10 = Hint.trans == 1;
										if (flag10)
										{
											g.drawRegion(Panel.imgBantay, 0, 0, 14, 16, 2, Hint.x + 15, Hint.y, StaticObj.TOP_RIGHT);
										}
										bool flag11 = Hint.paintFlare;
										if (flag11)
										{
											g.drawImage(ItemMap.imageFlare, Hint.x, Hint.y, 3);
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

	// Token: 0x060003A7 RID: 935 RVA: 0x00054560 File Offset: 0x00052760
	public static void hint()
	{
		bool flag = global::Char.myCharz().taskMaint != null && GameCanvas.currentScreen == GameScr.instance;
		if (flag)
		{
			int taskId = (int)global::Char.myCharz().taskMaint.taskId;
			int index = global::Char.myCharz().taskMaint.index;
			Hint.isCamera = false;
			Hint.trans = 0;
			Hint.type = 0;
			Hint.isPaint = true;
			Hint.isPaintArrow = true;
			bool flag2 = GameCanvas.menu.showMenu && taskId > 0;
			if (flag2)
			{
				Hint.isPaint = false;
			}
			switch (taskId)
			{
			case 0:
			{
				bool flag3 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag3)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag4 = index == 0 && TileMap.vGo.size() != 0;
					if (flag4)
					{
						Hint.x = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minX - 100);
						Hint.y = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minY + 40);
						Hint.isCamera = true;
					}
					bool flag5 = index == 1;
					if (flag5)
					{
						Hint.nextMap(0);
					}
					bool flag6 = index == 2;
					if (flag6)
					{
						Hint.clickNpc();
					}
					bool flag7 = index == 3;
					if (flag7)
					{
						bool flag8 = !GameCanvas.panel.isShow;
						if (flag8)
						{
							Hint.clickNpc();
						}
						else
						{
							bool flag9 = GameCanvas.panel.currentTabIndex == 0;
							if (flag9)
							{
								bool flag10 = GameCanvas.panel.cp == null;
								if (flag10)
								{
									Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
									Hint.y = GameCanvas.panel.yScroll + 20;
								}
								else
								{
									bool flag11 = GameCanvas.menu.tDelay != 0;
									if (flag11)
									{
										Hint.x = GameCanvas.panel.xScroll + 25;
										Hint.y = GameCanvas.panel.yScroll + 60;
									}
								}
							}
							else
							{
								bool flag12 = GameCanvas.panel.currentTabIndex == 1;
								if (flag12)
								{
									Hint.x = GameCanvas.panel.startTabPos + 10;
									Hint.y = 65;
								}
							}
						}
					}
					bool flag13 = index == 4;
					if (flag13)
					{
						bool flag14 = GameCanvas.panel.isShow;
						if (flag14)
						{
							Hint.x = GameCanvas.panel.cmdClose.x + 5;
							Hint.y = GameCanvas.panel.cmdClose.y + 5;
						}
						else
						{
							bool showMenu = GameCanvas.menu.showMenu;
							if (showMenu)
							{
								Hint.x = GameCanvas.w / 2;
								Hint.y = GameCanvas.h - 20;
							}
							else
							{
								Hint.clickNpc();
							}
						}
					}
					bool flag15 = index == 5;
					if (flag15)
					{
						Hint.clickNpc();
					}
				}
				break;
			}
			case 1:
			{
				bool flag16 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag16)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag17 = index == 0;
					if (flag17)
					{
						bool flag18 = TileMap.isOfflineMap();
						if (flag18)
						{
							Hint.nextMap(0);
						}
						else
						{
							Hint.clickMob();
						}
					}
					bool flag19 = index == 1;
					if (flag19)
					{
						bool flag20 = !TileMap.isOfflineMap();
						if (flag20)
						{
							Hint.nextMap(1);
						}
						else
						{
							Hint.clickNpc();
						}
					}
				}
				break;
			}
			case 2:
			{
				bool flag21 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag21)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag22 = index == 0;
					if (flag22)
					{
						bool flag23 = !TileMap.isOfflineMap();
						if (flag23)
						{
							Hint.isViewMap = true;
						}
						bool flag24 = !GameCanvas.panel.isShow;
						if (flag24)
						{
							bool flag25 = !Hint.isViewMap;
							if (flag25)
							{
								Hint.x = GameScr.gI().cmdMenu.x;
								Hint.y = GameScr.gI().cmdMenu.y + 13;
								Hint.trans = 1;
							}
							else
							{
								bool flag26 = GameScr.getTaskMapId() == TileMap.mapID;
								if (flag26)
								{
									bool flag27 = !Hint.isHaveItem();
									if (flag27)
									{
										Hint.clickMob();
									}
								}
								else
								{
									Hint.nextMap(0);
								}
								bool flag28 = Hint.isViewMap;
								if (flag28)
								{
									Hint.isCloseMap = true;
								}
							}
						}
						else
						{
							bool flag29 = !Hint.isViewMap;
							if (flag29)
							{
								bool flag30 = GameCanvas.panel.currentTabIndex == 0;
								if (flag30)
								{
									int num = (GameCanvas.h <= 300) ? 10 : 15;
									Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
									Hint.y = GameCanvas.panel.yScroll + GameCanvas.panel.hScroll - num;
								}
								else
								{
									Hint.x = GameCanvas.panel.startTabPos + 10;
									Hint.y = 65;
								}
							}
							else
							{
								bool flag31 = !Hint.isCloseMap;
								if (flag31)
								{
									Hint.x = GameCanvas.panel.cmdClose.x + 5;
									Hint.y = GameCanvas.panel.cmdClose.y + 5;
								}
								else
								{
									Hint.isPaint = false;
								}
							}
						}
						bool flag32 = global::Char.myCharz().cMP <= 0;
						if (flag32)
						{
							Hint.x = GameScr.xHP + 5;
							Hint.y = GameScr.yHP + 13;
							Hint.isCamera = false;
						}
					}
					bool flag33 = index == 1;
					if (flag33)
					{
						Hint.isPaint = false;
						Hint.isPaintArrow = false;
					}
				}
				break;
			}
			case 3:
			{
				bool flag34 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag34)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag35 = index == 0;
					if (flag35)
					{
						bool flag36 = !GameCanvas.panel.isShow;
						if (flag36)
						{
							bool flag37 = !Hint.isViewPotential;
							if (flag37)
							{
								Hint.x = GameScr.gI().cmdMenu.x;
								Hint.y = GameScr.gI().cmdMenu.y + 13;
								Hint.trans = 1;
							}
							else
							{
								bool flag38 = GameScr.getTaskMapId() == TileMap.mapID;
								if (flag38)
								{
									bool flag39 = !Hint.isHaveItem();
									if (flag39)
									{
										Hint.clickMob();
									}
								}
								else
								{
									Hint.nextMap(0);
								}
								bool flag40 = Hint.isViewMap;
								if (flag40)
								{
									Hint.isCloseMap = true;
								}
							}
						}
						else
						{
							bool flag41 = !Hint.isViewPotential;
							if (flag41)
							{
								int num2 = (GameCanvas.h <= 300) ? 10 : 15;
								Hint.x = GameCanvas.panel.xScroll + 10 + 108 - 18;
								Hint.y = 65;
							}
							else
							{
								bool flag42 = !Hint.isCloseMap;
								if (flag42)
								{
									Hint.x = GameCanvas.panel.cmdClose.x + 5;
									Hint.y = GameCanvas.panel.cmdClose.y + 5;
								}
								else
								{
									Hint.isPaint = false;
								}
							}
						}
						bool flag43 = global::Char.myCharz().cMP <= 0;
						if (flag43)
						{
							Hint.x = GameScr.xHP + 5;
							Hint.y = GameScr.yHP + 13;
							Hint.isCamera = false;
						}
					}
					else
					{
						Hint.isPaint = false;
						Hint.isPaintArrow = false;
					}
				}
				break;
			}
			default:
			{
				bool flag44 = global::Char.myCharz().taskMaint.taskId == 9 && global::Char.myCharz().taskMaint.index == 2;
				if (flag44)
				{
					for (int i = 0; i < PopUp.vPopups.size(); i++)
					{
						PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
						bool flag45 = popUp.cy <= 24;
						if (flag45)
						{
							Hint.x = popUp.cx + popUp.sayWidth / 2;
							Hint.y = popUp.cy + 30;
							Hint.isCamera = true;
							Hint.isPaint = false;
							Hint.isPaintArrow = true;
							return;
						}
					}
				}
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
				break;
			}
			}
		}
		else
		{
			Hint.isPaint = false;
			Hint.isPaintArrow = false;
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00054DD8 File Offset: 0x00052FD8
	public static void update()
	{
		Hint.hint();
		int num = (Hint.trans != 0) ? -2 : 2;
		bool flag = !Hint.activeClick;
		if (flag)
		{
			Hint.paintFlare = false;
			Hint.t++;
			bool flag2 = Hint.t == 50;
			if (flag2)
			{
				Hint.t = 0;
				Hint.activeClick = true;
			}
		}
		else
		{
			Hint.t++;
			bool flag3 = Hint.type == 0;
			if (flag3)
			{
				bool flag4 = Hint.t == 2;
				if (flag4)
				{
					Hint.x += 2 * num;
					Hint.y -= 4;
					Hint.paintFlare = true;
				}
				bool flag5 = Hint.t == 4;
				if (flag5)
				{
					Hint.x -= 2 * num;
					Hint.y += 4;
					Hint.activeClick = false;
					Hint.paintFlare = false;
					Hint.t = 0;
				}
				bool flag6 = Hint.t > 4;
				if (flag6)
				{
					Hint.activeClick = false;
				}
			}
			bool flag7 = Hint.type == 1;
			if (flag7)
			{
				bool flag8 = Hint.t == 2;
				if (flag8)
				{
					bool isTouch = GameCanvas.isTouch;
					if (isTouch)
					{
						GameScr.startFlyText(mResources.press_twice, Hint.x, Hint.y + 10, 0, 20, mFont.MISS_ME);
					}
					Hint.paintFlare = true;
					Hint.x += 2 * num;
					Hint.y -= 4;
				}
				bool flag9 = Hint.t == 4;
				if (flag9)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				bool flag10 = Hint.t == 6;
				if (flag10)
				{
					Hint.paintFlare = true;
					Hint.x += num;
					Hint.y -= 2;
				}
				bool flag11 = Hint.t == 8;
				if (flag11)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				bool flag12 = Hint.t == 10;
				if (flag12)
				{
					Hint.x -= num;
					Hint.y += 2;
					Hint.activeClick = false;
					Hint.t = 0;
				}
			}
		}
	}

	// Token: 0x04000875 RID: 2165
	public static int x;

	// Token: 0x04000876 RID: 2166
	public static int y;

	// Token: 0x04000877 RID: 2167
	public static int type;

	// Token: 0x04000878 RID: 2168
	public static int t;

	// Token: 0x04000879 RID: 2169
	public static int xF;

	// Token: 0x0400087A RID: 2170
	public static int yF;

	// Token: 0x0400087B RID: 2171
	public static bool isShow;

	// Token: 0x0400087C RID: 2172
	public static bool activeClick;

	// Token: 0x0400087D RID: 2173
	public static bool isViewMap;

	// Token: 0x0400087E RID: 2174
	public static bool isCloseMap;

	// Token: 0x0400087F RID: 2175
	public static bool isViewPotential;

	// Token: 0x04000880 RID: 2176
	public static bool isPaint;

	// Token: 0x04000881 RID: 2177
	public static bool isCamera;

	// Token: 0x04000882 RID: 2178
	public static int trans;

	// Token: 0x04000883 RID: 2179
	public static bool paintFlare;

	// Token: 0x04000884 RID: 2180
	public static bool isPaintArrow;

	// Token: 0x04000885 RID: 2181
	private int s = 2;
}
