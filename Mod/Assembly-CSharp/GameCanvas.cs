using System;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class GameCanvas : IActionListener
{
	// Token: 0x0600028D RID: 653 RVA: 0x0003BF18 File Offset: 0x0003A118
	public GameCanvas()
	{
		int num = Rms.loadRMSInt("languageVersion");
		bool flag = num == -1;
		if (flag)
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		else
		{
			bool flag2 = num != 2;
			if (flag2)
			{
				Main.main.doClearRMS();
				Rms.saveRMSInt("languageVersion", 2);
			}
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		bool flag3 = GameCanvas.clearOldData != 1;
		if (flag3)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0003BFE8 File Offset: 0x0003A1E8
	public static string getPlatformName()
	{
		return "Pc platform xxx";
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0003C000 File Offset: 0x0003A200
	public void initGame()
	{
		MotherCanvas.instance.setChildCanvas(this);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.isTouch = true;
		bool flag = GameCanvas.w >= 240;
		if (flag)
		{
			GameCanvas.isTouchControl = true;
		}
		bool flag2 = GameCanvas.w < 320;
		if (flag2)
		{
			GameCanvas.isTouchControlSmallScreen = true;
		}
		bool flag3 = GameCanvas.w >= 320;
		if (flag3)
		{
			GameCanvas.isTouchControlLargeScreen = true;
		}
		GameCanvas.msgdlg = new MsgDlg();
		bool flag4 = GameCanvas.h <= 160;
		if (flag4)
		{
			Paint.hTab = 15;
			mScreen.cmdH = 17;
		}
		GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
		GameCanvas.instance = this;
		mFont.init();
		mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
		this.initPaint();
		this.loadDust();
		this.loadWaterSplash();
		GameCanvas.panel = new Panel();
		GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
		int num = Rms.loadRMSInt("clienttype");
		bool flag5 = num != -1;
		if (flag5)
		{
			bool flag6 = num > 7;
			if (flag6)
			{
				Rms.saveRMSInt("clienttype", mSystem.clientType);
			}
			else
			{
				mSystem.clientType = num;
			}
		}
		bool flag7 = mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty);
		if (flag7)
		{
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/wait.png");
		}
		GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		GameCanvas.debugUpdate = new MyVector();
		GameCanvas.debugPaint = new MyVector();
		GameCanvas.debugSession = new MyVector();
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i.ToString() + ".png");
		}
		GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
		GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
		GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
		GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
		Panel.graphics = Rms.loadRMSInt("lowGraphic");
		GameCanvas.lowGraphic = (Rms.loadRMSInt("lowGraphic") == 1);
		GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") != 1);
		global::Char.isPaintAura = (Rms.loadRMSInt("isPaintAura") == 1);
		global::Char.isPaintAura2 = (Rms.loadRMSInt("isPaintAura2") == 1);
		Res.init();
		SmallImage.loadBigImage();
		Panel.WIDTH_PANEL = 176;
		bool flag8 = Panel.WIDTH_PANEL > GameCanvas.w;
		if (flag8)
		{
			Panel.WIDTH_PANEL = GameCanvas.w;
		}
		InfoMe.gI().loadCharId();
		Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
		Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
		Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
		Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
		Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
		Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
		GameCanvas.serverScreen = new ServerListScreen();
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		for (int j = 0; j < 7; j++)
		{
			GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j.ToString() + ".png");
			GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j.ToString() + ".png");
		}
		ServerListScreen.createDeleteRMS();
		GameCanvas.serverScr = new ServerScr();
		GameCanvas.chooseCharScr = new ChooseCharScr();
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0003C404 File Offset: 0x0003A604
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0003C41B File Offset: 0x0003A61B
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0003C428 File Offset: 0x0003A628
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0003C440 File Offset: 0x0003A640
	public void update()
	{
		bool flag = mSystem.currentTimeMillis() > this.timefps;
		if (flag)
		{
			this.timefps += 1000L;
			GameCanvas.max = GameCanvas.fps;
			GameCanvas.fps = 0;
		}
		GameCanvas.fps++;
		bool flag2 = GameCanvas.messageServer.size() > 0 && GameCanvas.thongBaoTest == null;
		if (flag2)
		{
			GameCanvas.startserverThongBao((string)GameCanvas.messageServer.elementAt(0));
			GameCanvas.messageServer.removeElementAt(0);
		}
		bool flag3 = GameCanvas.gameTick % 5 == 0;
		if (flag3)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			bool visible = global::TouchScreenKeyboard.visible;
			if (visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				bool flag4 = GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5);
				if (flag4)
				{
					mGraphics.addYWhenOpenKeyBoard = 94;
				}
			}
			else
			{
				mGraphics.addYWhenOpenKeyBoard = 0;
				GameCanvas.timeOpenKeyBoard = 0;
			}
			GameCanvas.debugUpdate.removeAllElements();
			long num = mSystem.currentTimeMillis();
			bool flag5 = num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1;
			if (flag5)
			{
				GameCanvas.timeTickEff1 = num;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			bool flag6 = num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2;
			if (flag6)
			{
				GameCanvas.timeTickEff2 = num;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			bool flag7 = GameCanvas.taskTick > 0;
			if (flag7)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			bool flag8 = GameCanvas.gameTick > 10000;
			if (flag8)
			{
				bool flag9 = mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr;
				if (flag9)
				{
					GameMidlet.instance.exit();
				}
				GameCanvas.gameTick = 0;
			}
			bool flag10 = GameCanvas.currentScreen != null;
			if (flag10)
			{
				bool flag11 = ChatPopup.serverChatPopUp != null;
				if (flag11)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else
				{
					bool flag12 = ChatPopup.currChatPopup != null;
					if (flag12)
					{
						ChatPopup.currChatPopup.update();
						ChatPopup.currChatPopup.updateKey();
					}
					else
					{
						bool flag13 = GameCanvas.currentDialog != null;
						if (flag13)
						{
							GameCanvas.debug("B", 0);
							GameCanvas.currentDialog.update();
						}
						else
						{
							bool showMenu = GameCanvas.menu.showMenu;
							if (showMenu)
							{
								GameCanvas.debug("C", 0);
								GameCanvas.menu.updateMenu();
								GameCanvas.debug("D", 0);
								GameCanvas.menu.updateMenuKey();
							}
							else
							{
								bool isShow = GameCanvas.panel.isShow;
								if (isShow)
								{
									GameCanvas.panel.update();
									bool flag14 = GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H);
									if (flag14)
									{
										GameCanvas.isFocusPanel2 = false;
									}
									bool flag15 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow;
									if (flag15)
									{
										GameCanvas.panel2.update();
										bool flag16 = GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H);
										if (flag16)
										{
											GameCanvas.isFocusPanel2 = true;
										}
									}
									bool flag17 = GameCanvas.panel2 != null;
									if (flag17)
									{
										bool flag18 = GameCanvas.isFocusPanel2;
										if (flag18)
										{
											GameCanvas.panel2.updateKey();
										}
										else
										{
											GameCanvas.panel.updateKey();
										}
									}
									else
									{
										GameCanvas.panel.updateKey();
									}
									bool flag19 = GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow;
									if (flag19)
									{
										GameCanvas.panel.chatTFUpdateKey();
									}
									else
									{
										bool flag20 = GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow;
										if (flag20)
										{
											GameCanvas.panel2.chatTFUpdateKey();
										}
										else
										{
											bool flag21 = (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null;
											if (flag21)
											{
												GameCanvas.panel.updateKey();
											}
											else
											{
												bool flag22 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H);
												if (flag22)
												{
													GameCanvas.panel2.updateKey();
												}
											}
										}
									}
									bool flag23 = GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine;
									if (flag23)
									{
										GameCanvas.panel.hide();
									}
								}
							}
						}
					}
				}
				GameCanvas.debug("E", 0);
				bool flag24 = !GameCanvas.isLoading;
				if (flag24)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				bool flag25 = !GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null;
				if (flag25)
				{
					GameCanvas.currentScreen.updateKey();
				}
				Hint.update();
				SoundMn.gI().update();
			}
			GameCanvas.debug("Ix", 0);
			Timer.update();
			GameCanvas.debug("Hx", 0);
			InfoDlg.update();
			GameCanvas.debug("G", 0);
			bool flag26 = this.resetToLoginScr;
			if (flag26)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.serverScreen);
			}
			GameCanvas.debug("Zzz", 0);
			bool isConnectOK = Controller.isConnectOK;
			if (isConnectOK)
			{
				bool isMain = Controller.isMain;
				if (isMain)
				{
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
					ServerListScreen.testConnect = 2;
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					Rms.saveIP(GameMidlet.IP + ":" + GameMidlet.PORT.ToString());
					Service.gI().setClientType();
					Service.gI().androidPack();
				}
				else
				{
					Service.gI().setClientType2();
					Service.gI().androidPack2();
				}
				Controller.isConnectOK = false;
			}
			bool isDisconnected = Controller.isDisconnected;
			if (isDisconnected)
			{
				Debug.Log("disconnect");
				bool flag27 = !Controller.isMain;
				if (flag27)
				{
					bool flag28 = GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession;
					if (flag28)
					{
						GameCanvas.serverScreen.cancel();
					}
					bool flag29 = GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession;
					if (flag29)
					{
						this.onDisconnected();
					}
				}
				else
				{
					this.onDisconnected();
				}
				Controller.isDisconnected = false;
			}
			bool isConnectionFail = Controller.isConnectionFail;
			if (isConnectionFail)
			{
				Debug.Log("connect fail");
				bool flag30 = !Controller.isMain;
				if (flag30)
				{
					bool flag31 = GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession;
					if (flag31)
					{
						ServerListScreen.testConnect = 0;
						GameCanvas.serverScreen.cancel();
					}
					bool flag32 = GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession;
					if (flag32)
					{
						this.onConnectionFail();
					}
				}
				else
				{
					bool flag33 = Session_ME.gI().isCompareIPConnect();
					if (flag33)
					{
						this.onConnectionFail();
					}
				}
				Controller.isConnectionFail = false;
			}
			bool flag34 = Main.isResume;
			if (flag34)
			{
				Main.isResume = false;
				bool flag35 = GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null;
				if (flag35)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
			bool flag36 = GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr;
			if (flag36)
			{
				GameCanvas.xThongBaoTranslate += GameCanvas.dir_ * 2;
				bool flag37 = GameCanvas.xThongBaoTranslate - Panel.imgNew.getWidth() <= 60;
				if (flag37)
				{
					GameCanvas.dir_ = 0;
					this.tickWaitThongBao++;
					bool flag38 = this.tickWaitThongBao > 150;
					if (flag38)
					{
						this.tickWaitThongBao = 0;
						GameCanvas.thongBaoTest = null;
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0003CD3C File Offset: 0x0003AF3C
	public void onDisconnected()
	{
		bool isConnectionFail = Controller.isConnectionFail;
		if (isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		Session_ME.gI().close();
		Session_ME2.gI().close();
		bool isLoadingData = Controller.isLoadingData;
		if (isLoadingData)
		{
			GameCanvas.instance.resetToLoginScrz();
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
		}
		else
		{
			bool flag = GameCanvas.currentScreen != GameCanvas.serverScreen;
			if (flag)
			{
				GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			}
			else
			{
				GameCanvas.endDlg();
			}
			global::Char.isLoadingMap = false;
			bool isMain = Controller.isMain;
			if (isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			GameCanvas.instance.resetToLoginScrz();
			mSystem.endKey();
		}
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0003CE10 File Offset: 0x0003B010
	public void onConnectionFail()
	{
		bool flag = GameCanvas.currentScreen.Equals(SplashScr.instance);
		if (flag)
		{
			bool flag2 = ServerListScreen.hasConnected != null;
			if (flag2)
			{
				ServerListScreen.getServerList(ServerListScreen.linkDefault);
				bool flag3 = !ServerListScreen.hasConnected[0];
				if (flag3)
				{
					ServerListScreen.hasConnected[0] = true;
					ServerListScreen.ipSelect = 0;
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					GameCanvas.connect();
				}
				else
				{
					bool flag4 = !ServerListScreen.hasConnected[2];
					if (flag4)
					{
						ServerListScreen.hasConnected[2] = true;
						ServerListScreen.ipSelect = 2;
						GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
						Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
						GameCanvas.connect();
					}
					else
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
			}
			else
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			}
		}
		else
		{
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			ServerListScreen.isWait = false;
			bool isLoadingData = Controller.isLoadingData;
			if (isLoadingData)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				Controller.isConnectionFail = false;
			}
			else
			{
				GameCanvas.isResume = true;
				LoginScr.isContinueToLogin = false;
				bool flag5 = GameCanvas.loginScr != null;
				if (flag5)
				{
					GameCanvas.instance.resetToLoginScrz();
				}
				else
				{
					GameCanvas.loginScr = new LoginScr();
				}
				LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
				bool flag6 = GameCanvas.currentScreen != GameCanvas.serverScreen;
				if (flag6)
				{
					ServerListScreen.countDieConnect = 0;
				}
				else
				{
					GameCanvas.endDlg();
					ServerListScreen.loadScreen = true;
					GameCanvas.serverScreen.switchToMe();
				}
				global::Char.isLoadingMap = false;
				bool isMain = Controller.isMain;
				if (isMain)
				{
					ServerListScreen.testConnect = 0;
				}
				mSystem.endKey();
			}
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0003CFEC File Offset: 0x0003B1EC
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0003D034 File Offset: 0x0003B234
	public static void connect()
	{
		bool flag = !Session_ME.gI().isConnected();
		if (flag)
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0003D06C File Offset: 0x0003B26C
	public static void connect2()
	{
		bool flag = !Session_ME2.gI().isConnected();
		if (flag)
		{
			Res.outz(string.Concat(new object[]
			{
				"IP2= ",
				GameMidlet.IP2,
				" PORT 2= ",
				GameMidlet.PORT2
			}));
			Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0003D0D8 File Offset: 0x0003B2D8
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0003D104 File Offset: 0x0003B304
	public static void resetTransGameScr(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate(0, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-GameScr.cmx, -GameScr.cmy);
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0003D158 File Offset: 0x0003B358
	public void initGameCanvas()
	{
		GameCanvas.debug("SP2i1", 0);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.debug("SP2i2", 0);
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
		GameCanvas.debug("SP2i3", 0);
		mScreen.initPos();
		GameCanvas.debug("SP2i4", 0);
		GameCanvas.debug("SP2i5", 0);
		GameCanvas.inputDlg = new InputDlg();
		GameCanvas.debug("SP2i6", 0);
		GameCanvas.listPoint = new MyVector();
		GameCanvas.debug("SP2i7", 0);
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00003136 File Offset: 0x00001336
	public void start()
	{
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0003D274 File Offset: 0x0003B474
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0003D28C File Offset: 0x0003B48C
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00003136 File Offset: 0x00001336
	public static void debug(string s, int type)
	{
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0003D2A4 File Offset: 0x0003B4A4
	public void doResetToLoginScr(mScreen screen)
	{
		try
		{
			SoundMn.gI().stopAll();
			LoginScr.isContinueToLogin = false;
			TileMap.lastType = (TileMap.bgType = 0);
			global::Char.clearMyChar();
			GameScr.clearGameScr();
			GameScr.resetAllvector();
			InfoDlg.hide();
			GameScr.info1.hide();
			GameScr.info2.hide();
			GameScr.info2.cmdChat = null;
			Hint.isShow = false;
			ChatPopup.currChatPopup = null;
			Controller.isStopReadMessage = false;
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameCanvas.panel.currentTabIndex = 0;
			GameCanvas.panel.selected = ((!GameCanvas.isTouch) ? 0 : -1);
			GameCanvas.panel.init();
			GameCanvas.panel2 = null;
			GameScr.isPaint = true;
			ClanMessage.vMessage.removeAllElements();
			GameScr.textTime.removeAllElements();
			GameScr.vClan.removeAllElements();
			GameScr.vFriend.removeAllElements();
			GameScr.vEnemies.removeAllElements();
			TileMap.vCurrItem.removeAllElements();
			BackgroudEffect.vBgEffect.removeAllElements();
			EffecMn.vEff.removeAllElements();
			Effect.newEff.removeAllElements();
			GameCanvas.menu.showMenu = false;
			GameCanvas.panel.vItemCombine.removeAllElements();
			GameCanvas.panel.isShow = false;
			bool flag = GameCanvas.panel.tabIcon != null;
			if (flag)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				SmallImage.clearHastable();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
			screen.switchToMe();
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
		}
		ServerListScreen.isAutoConect = true;
		ServerListScreen.countDieConnect = 0;
		ServerListScreen.testConnect = -1;
		ServerListScreen.loadScreen = true;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00003136 File Offset: 0x00001336
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00003136 File Offset: 0x00001336
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x00003136 File Offset: 0x00001336
	public static void updateBG()
	{
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0003D494 File Offset: 0x0003B694
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		bool flag = cmy > GameCanvas.h;
		if (flag)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY == 0) ? 0 : (cmy >> detalY)), w, h + ((detalY == 0) ? 0 : (cmy >> detalY)));
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0003D4F0 File Offset: 0x0003B6F0
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			bool flag = num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks);
			if (flag)
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				bool flag2 = GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7;
				if (flag2)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				bool flag3 = GameScr.gI().isFireWorks && !GameCanvas.lowGraphic;
				if (flag3)
				{
					FireWorkEff.paint(g);
				}
			}
			else
			{
				bool flag4 = GameCanvas.imgBG != null && GameCanvas.imgBG[num] != null;
				if (flag4)
				{
					bool flag5 = GameCanvas.moveX[num] != 0;
					if (flag5)
					{
						GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
					}
					int cmy = GameScr.cmy;
					bool flag6 = cmy > GameCanvas.h;
					if (flag6)
					{
						cmy = GameCanvas.h;
					}
					bool flag7 = GameCanvas.layerSpeed[num] != 0;
					if (flag7)
					{
						for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY <= 0) ? 0 : (cmy >> deltaY)), 0);
						}
					}
					else
					{
						for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY <= 0) ? 0 : (cmy >> deltaY)), 0);
						}
					}
					bool flag8 = color1 != -1;
					if (flag8)
					{
						bool flag9 = num == GameCanvas.nBg - 1;
						if (flag9)
						{
							GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
						}
					}
					bool flag10 = color2 != -1;
					if (flag10)
					{
						bool flag11 = num == 0;
						if (flag11)
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
						}
					}
					bool flag12 = GameCanvas.currentScreen == GameScr.instance;
					if (flag12)
					{
						bool flag13 = layer == 1 && GameCanvas.typeBg == 11;
						if (flag13)
						{
							g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
						}
						bool flag14 = layer == 1 && GameCanvas.typeBg == 13;
						if (flag14)
						{
							g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
							g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						}
						bool flag15 = layer == 3 && TileMap.mapID == 1;
						if (flag15)
						{
							for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
							{
								g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
							}
						}
					}
					int x = -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]);
					EffecMn.paintBackGroundUnderLayer(g, x, GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
	public static void drawSun1(mGraphics g)
	{
		bool flag = GameCanvas.imgSun != null;
		if (flag)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		bool flag2 = GameCanvas.isBoltEff;
		if (flag2)
		{
			bool flag3 = GameCanvas.gameTick % 200 == 0;
			if (flag3)
			{
				GameCanvas.boltActive = true;
			}
			bool flag4 = GameCanvas.boltActive;
			if (flag4)
			{
				GameCanvas.tBolt++;
				bool flag5 = GameCanvas.tBolt == 10;
				if (flag5)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				bool flag6 = GameCanvas.tBolt % 2 == 0;
				if (flag6)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0003DAA0 File Offset: 0x0003BCA0
	public static void drawSun2(mGraphics g)
	{
		bool flag = GameCanvas.imgSun2 != null;
		if (flag)
		{
			g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0003DAD4 File Offset: 0x0003BCD4
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
	public static void paint_ios_bg(mGraphics g)
	{
		bool flag = mSystem.clientType != 5;
		if (!flag)
		{
			bool flag2 = GameCanvas.imgBgIOS != null;
			if (flag2)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				for (int i = 0; i < 3; i++)
				{
					g.drawImage(GameCanvas.imgBgIOS, GameCanvas.imgBgIOS.getWidth() * i, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			else
			{
				GameCanvas.imgBgIOS = mSystem.loadImage("/bg/bg_ios_" + ((TileMap.bgID % 2 != 0) ? 1 : 2).ToString() + ".png");
			}
		}
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0003DBAC File Offset: 0x0003BDAC
	public static void paintBGGameScr(mGraphics g)
	{
		bool flag = !GameCanvas.isLoadBGok;
		if (flag)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			int gW = GameScr.gW;
			int gH = GameScr.gH;
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			try
			{
				bool flag2 = GameCanvas.paintBG;
				if (flag2)
				{
					bool flag3 = GameCanvas.currentScreen == GameScr.gI();
					if (flag3)
					{
						bool flag4 = TileMap.mapID == 137 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 120 || TileMap.isMapDouble;
						if (flag4)
						{
							g.setColor(0);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							return;
						}
						bool flag5 = TileMap.mapID == 138;
						if (flag5)
						{
							g.setColor(6776679);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							return;
						}
					}
					bool flag6 = GameCanvas.typeBg == 0;
					if (flag6)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else
					{
						bool flag7 = GameCanvas.typeBg == 1;
						if (flag7)
						{
							GameCanvas.paintBackgroundtLayer(g, 4, 6, -1, -1);
							GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, -1);
							GameCanvas.fillRect(g, GameCanvas.colorTop[2], 0, -(GameScr.cmy >> 5), gW, GameCanvas.yb[2], 5);
							GameCanvas.fillRect(g, GameCanvas.colorBotton[2], 0, GameCanvas.yb[2] + GameCanvas.bgH[2] - (GameScr.cmy >> 3), gW, 70, 3);
							GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, -1);
							GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
						}
						else
						{
							bool flag8 = GameCanvas.typeBg == 2;
							if (flag8)
							{
								GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
								GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
								GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
								GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
								GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
								GameCanvas.paintCloud(g);
							}
							else
							{
								bool flag9 = GameCanvas.typeBg == 3;
								if (flag9)
								{
									int num = GameScr.cmy - (325 - GameScr.gH23);
									g.translate(0, -num);
									GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien && !GameScr.gI().isFireWorks) ? GameCanvas.colorTop[2] : GameScr.gI().mautroi, 0, num - (GameScr.cmy >> 3), gW, GameCanvas.yb[2] - num + (GameScr.cmy >> 3) + 100, 2);
									GameCanvas.paintBackgroundtLayer(g, 3, 2, -1, GameCanvas.colorBotton[2]);
									GameCanvas.paintBackgroundtLayer(g, 2, 0, -1, -1);
									GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, GameCanvas.colorBotton[0]);
									g.translate(0, -g.getTranslateY());
								}
								else
								{
									bool flag10 = GameCanvas.typeBg == 4;
									if (flag10)
									{
										GameCanvas.paintBackgroundtLayer(g, 4, 7, GameCanvas.colorTop[3], -1);
										GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, (!GameCanvas.isHDVersion()) ? GameCanvas.colorTop[1] : GameCanvas.colorBotton[2]);
										GameCanvas.paintBackgroundtLayer(g, 2, 2, GameCanvas.colorTop[1], GameCanvas.colorBotton[1]);
										GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
									}
									else
									{
										bool flag11 = GameCanvas.typeBg == 5;
										if (flag11)
										{
											GameCanvas.paintBackgroundtLayer(g, 4, 15, GameCanvas.colorTop[3], -1);
											GameCanvas.drawSun1(g);
											g.translate(100, 10);
											GameCanvas.drawSun1(g);
											g.translate(-100, -10);
											GameCanvas.drawSun2(g);
											GameCanvas.paintBackgroundtLayer(g, 3, 10, -1, -1);
											GameCanvas.paintBackgroundtLayer(g, 2, 6, -1, -1);
											GameCanvas.paintBackgroundtLayer(g, 1, 4, -1, -1);
											g.translate(0, 27);
											GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, -1);
											g.translate(0, 20);
											GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
											g.translate(-g.getTranslateX(), -g.getTranslateY());
										}
										else
										{
											bool flag12 = GameCanvas.typeBg == 6;
											if (flag12)
											{
												GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
												GameCanvas.drawSun1(g);
												GameCanvas.drawSun2(g);
												g.translate(60, 40);
												GameCanvas.drawSun2(g);
												g.translate(-60, -40);
												GameCanvas.paintBackgroundtLayer(g, 4, 7, -1, GameCanvas.colorBotton[3]);
												BackgroudEffect.paintFarAll(g);
												GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, -1);
												GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
												GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
											}
											else
											{
												bool flag13 = GameCanvas.typeBg == 7;
												if (flag13)
												{
													GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
													GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
													GameCanvas.paintBackgroundtLayer(g, 2, 4, -1, -1);
													GameCanvas.paintBackgroundtLayer(g, 1, 3, -1, GameCanvas.colorBotton[0]);
												}
												else
												{
													bool flag14 = GameCanvas.typeBg == 8;
													if (flag14)
													{
														GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
														GameCanvas.drawSun1(g);
														GameCanvas.drawSun2(g);
														GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
														GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
														bool flag15 = ((TileMap.mapID < 92 || TileMap.mapID > 96) && TileMap.mapID != 51 && TileMap.mapID != 52) || GameCanvas.currentScreen == GameCanvas.loginScr;
														if (flag15)
														{
															GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
														}
													}
													else
													{
														bool flag16 = GameCanvas.typeBg == 9;
														if (flag16)
														{
															GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
															GameCanvas.drawSun1(g);
															GameCanvas.drawSun2(g);
															g.translate(-80, 20);
															GameCanvas.drawSun2(g);
															g.translate(80, -20);
															BackgroudEffect.paintFarAll(g);
															GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
															GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, -1);
															GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
														}
														else
														{
															bool flag17 = GameCanvas.typeBg == 10;
															if (flag17)
															{
																int num2 = GameScr.cmy - (380 - GameScr.gH23);
																g.translate(0, -num2);
																GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien) ? GameCanvas.colorTop[1] : GameScr.gI().mautroi, 0, num2 - (GameScr.cmy >> 2), gW, GameCanvas.yb[1] - num2 + (GameScr.cmy >> 2) + 100, 2);
																GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
																GameCanvas.drawSun1(g);
																GameCanvas.drawSun2(g);
																GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, -1);
																g.translate(0, -g.getTranslateY());
															}
															else
															{
																bool flag18 = GameCanvas.typeBg == 11;
																if (flag18)
																{
																	GameCanvas.paintBackgroundtLayer(g, 3, 6, GameCanvas.colorTop[2], GameCanvas.colorBotton[2]);
																	GameCanvas.drawSun1(g);
																	GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																	GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																}
																else
																{
																	bool flag19 = GameCanvas.typeBg == 12;
																	if (flag19)
																	{
																		g.setColor(9161471);
																		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
																		GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, 14417919);
																		GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, 14417919);
																		GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, 14417919);
																		GameCanvas.paintCloud(g);
																	}
																	else
																	{
																		bool flag20 = GameCanvas.typeBg == 13;
																		if (flag20)
																		{
																			g.setColor(15268088);
																			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
																			GameCanvas.paintBackgroundtLayer(g, 1, 5, -1, 15268088);
																		}
																		else
																		{
																			bool flag21 = GameCanvas.typeBg == 15;
																			if (flag21)
																			{
																				g.setColor(2631752);
																				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
																				GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																				GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																			}
																			else
																			{
																				bool flag22 = GameCanvas.typeBg == 16;
																				if (flag22)
																				{
																					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
																					for (int i = 0; i < GameCanvas.imgSunSpec.Length; i++)
																					{
																						g.drawImage(GameCanvas.imgSunSpec[i], GameCanvas.cloudX[i], GameCanvas.cloudY[i], 33);
																					}
																					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
																					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																				}
																				else
																				{
																					bool flag23 = GameCanvas.typeBg == 19;
																					if (flag23)
																					{
																						GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
																						GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
																						GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
																						GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
																						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
																						GameCanvas.paintCloud(g);
																					}
																					else
																					{
																						GameCanvas.fillRect(g, GameCanvas.colorBotton[3], 0, GameCanvas.yb[3] + GameCanvas.bgH[3], GameScr.gW, GameCanvas.yb[2] + GameCanvas.bgH[2], 6);
																						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
																						GameCanvas.drawSun1(g);
																						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
																						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
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
				else
				{
					g.setColor(2315859);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					bool flag24 = GameCanvas.tam != null;
					if (flag24)
					{
						for (int j = -((GameScr.cmx >> 2) % mGraphics.getImageWidth(GameCanvas.tam)); j < GameScr.gW; j += mGraphics.getImageWidth(GameCanvas.tam))
						{
							g.drawImage(GameCanvas.tam, j, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50, 0);
						}
					}
					g.setColor(5084791);
					g.fillRect(0, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50 + mGraphics.getImageHeight(GameCanvas.tam), gW, GameCanvas.h);
				}
			}
			catch (Exception ex)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00003136 File Offset: 0x00001336
	public static void resetBg()
	{
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0003E70C File Offset: 0x0003C90C
	public static void getYBackground(int typeBg)
	{
		try
		{
			int gH = GameScr.gH23;
			switch (typeBg)
			{
			case 0:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_686;
			case 1:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
				GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
				goto IL_686;
			case 2:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_686;
			case 3:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
				GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_686;
			case 4:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
				goto IL_686;
			case 5:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_686;
			case 6:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
				goto IL_686;
			case 7:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_686;
			case 8:
			{
				GameCanvas.yb[0] = gH - 103 + 150;
				bool flag = TileMap.mapID == 103;
				if (flag)
				{
					GameCanvas.yb[0] -= 100;
				}
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
				goto IL_686;
			}
			case 9:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
				goto IL_686;
			case 10:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				goto IL_686;
			case 11:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
				goto IL_686;
			case 12:
				GameCanvas.yb[0] = gH + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
				goto IL_686;
			case 13:
				GameCanvas.yb[0] = gH - 80;
				GameCanvas.yb[1] = GameCanvas.yb[0];
				goto IL_686;
			case 15:
				GameCanvas.yb[0] = gH - 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
				goto IL_686;
			case 16:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				goto IL_686;
			case 19:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_686;
			}
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
			IL_686:;
		}
		catch (Exception ex)
		{
			int gH2 = GameScr.gH23;
			for (int i = 0; i < GameCanvas.yb.Length; i++)
			{
				GameCanvas.yb[i] = 1;
			}
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
			bool flag = GameCanvas.typeBg == 12;
			if (flag)
			{
				BackgroudEffect.yfog = TileMap.pxh - 100;
			}
			else
			{
				BackgroudEffect.yfog = TileMap.pxh - 160;
			}
			BackgroudEffect.clearImage();
			GameCanvas.randomRaintEff(typeBG);
			bool flag2 = (TileMap.lastBgID != typeBG || TileMap.lastType != TileMap.bgType) && typeBG != -1;
			if (flag2)
			{
				GameCanvas.transY = 12;
				TileMap.lastBgID = (int)((sbyte)typeBG);
				TileMap.lastType = (int)((sbyte)TileMap.bgType);
				GameCanvas.layerSpeed = new int[]
				{
					1,
					2,
					3,
					7,
					8
				};
				GameCanvas.moveX = new int[5];
				GameCanvas.moveXSpeed = new int[5];
				GameCanvas.typeBg = typeBG;
				GameCanvas.isBoltEff = false;
				GameScr.firstY = GameScr.cmy;
				GameCanvas.imgBG = null;
				GameCanvas.imgCloud = null;
				GameCanvas.imgSun = null;
				GameCanvas.imgCaycot = null;
				GameScr.firstY = -1;
				switch (GameCanvas.typeBg)
				{
				case 0:
				{
					GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					bool flag3 = TileMap.bgType == 2;
					if (flag3)
					{
						GameCanvas.transY = 8;
					}
					goto IL_37B;
				}
				case 1:
					GameCanvas.transY = 7;
					GameCanvas.nBg = 4;
					goto IL_37B;
				case 2:
				{
					int[] array = new int[5];
					array[2] = 1;
					GameCanvas.moveX = array;
					int[] array2 = new int[5];
					array2[2] = 2;
					GameCanvas.moveXSpeed = array2;
					GameCanvas.nBg = 5;
					goto IL_37B;
				}
				case 3:
					GameCanvas.nBg = 3;
					goto IL_37B;
				case 4:
				{
					BackgroudEffect.addEffect(3);
					int[] array3 = new int[5];
					array3[1] = 1;
					GameCanvas.moveX = array3;
					int[] array4 = new int[5];
					array4[1] = 1;
					GameCanvas.moveXSpeed = array4;
					GameCanvas.nBg = 4;
					goto IL_37B;
				}
				case 5:
					GameCanvas.nBg = 4;
					goto IL_37B;
				case 6:
				{
					int[] array5 = new int[5];
					array5[0] = 1;
					GameCanvas.moveX = array5;
					int[] array6 = new int[5];
					array6[0] = 2;
					GameCanvas.moveXSpeed = array6;
					GameCanvas.nBg = 5;
					goto IL_37B;
				}
				case 7:
					GameCanvas.nBg = 4;
					goto IL_37B;
				case 8:
					GameCanvas.transY = 8;
					GameCanvas.nBg = 4;
					goto IL_37B;
				case 9:
					BackgroudEffect.addEffect(9);
					GameCanvas.nBg = 4;
					goto IL_37B;
				case 10:
					GameCanvas.nBg = 2;
					goto IL_37B;
				case 11:
					GameCanvas.transY = 7;
					GameCanvas.layerSpeed[2] = 0;
					GameCanvas.nBg = 3;
					goto IL_37B;
				case 12:
				{
					int[] array7 = new int[5];
					array7[0] = 1;
					array7[1] = 1;
					GameCanvas.moveX = array7;
					int[] array8 = new int[5];
					array8[0] = 2;
					array8[1] = 1;
					GameCanvas.moveXSpeed = array8;
					GameCanvas.nBg = 3;
					goto IL_37B;
				}
				case 13:
					GameCanvas.nBg = 2;
					goto IL_37B;
				case 15:
					Res.outz("HELL");
					GameCanvas.nBg = 2;
					goto IL_37B;
				case 16:
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					goto IL_37B;
				case 19:
				{
					int[] array9 = new int[5];
					array9[1] = 2;
					array9[2] = 1;
					GameCanvas.moveX = array9;
					int[] array10 = new int[5];
					array10[1] = 2;
					array10[2] = 1;
					GameCanvas.moveXSpeed = array10;
					GameCanvas.nBg = 5;
					goto IL_37B;
				}
				}
				GameCanvas.layerSpeed = new int[]
				{
					1,
					3,
					5,
					7
				};
				GameCanvas.nBg = 4;
				IL_37B:
				bool flag4 = typeBG <= 16;
				if (flag4)
				{
					GameCanvas.skyColor = StaticObj.SKYCOLOR[GameCanvas.typeBg];
				}
				else
				{
					try
					{
						string path = string.Concat(new object[]
						{
							"/bg/b",
							GameCanvas.typeBg,
							3,
							".png"
						});
						bool flag5 = TileMap.bgType != 0;
						if (flag5)
						{
							path = string.Concat(new object[]
							{
								"/bg/b",
								GameCanvas.typeBg,
								3,
								"-",
								TileMap.bgType,
								".png"
							});
						}
						int[] array11 = new int[1];
						Image image = GameCanvas.loadImageRMS(path);
						image.getRGB(ref array11, 0, 1, mGraphics.getRealImageWidth(image) / 2, 0, 1, 1);
						GameCanvas.skyColor = array11[0];
					}
					catch (Exception ex)
					{
						GameCanvas.skyColor = StaticObj.SKYCOLOR[StaticObj.SKYCOLOR.Length - 1];
					}
				}
				GameCanvas.colorTop = new int[StaticObj.SKYCOLOR.Length];
				GameCanvas.colorBotton = new int[StaticObj.SKYCOLOR.Length];
				for (int i = 0; i < StaticObj.SKYCOLOR.Length; i++)
				{
					GameCanvas.colorTop[i] = StaticObj.SKYCOLOR[i];
					GameCanvas.colorBotton[i] = StaticObj.SKYCOLOR[i];
				}
				bool flag6 = GameCanvas.lowGraphic;
				if (flag6)
				{
					GameCanvas.tam = GameCanvas.loadImageRMS("/bg/b63.png");
				}
				else
				{
					GameCanvas.imgBG = new Image[GameCanvas.nBg];
					GameCanvas.bgW = new int[GameCanvas.nBg];
					GameCanvas.bgH = new int[GameCanvas.nBg];
					GameCanvas.colorBotton = new int[GameCanvas.nBg];
					GameCanvas.colorTop = new int[GameCanvas.nBg];
					bool flag7 = TileMap.bgType == 100;
					if (flag7)
					{
						GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
						GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
						for (int j = 0; j < GameCanvas.nBg; j++)
						{
							bool flag8 = GameCanvas.imgBG[j] != null;
							if (flag8)
							{
								int[] array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, 0, 1, 1);
								GameCanvas.colorTop[j] = array12[0];
								array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[j]) - 1, 1, 1);
								GameCanvas.colorBotton[j] = array12[0];
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
							else
							{
								bool flag9 = GameCanvas.nBg > 1;
								if (flag9)
								{
									GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
									GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
									GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < GameCanvas.nBg; k++)
						{
							string path2 = string.Concat(new object[]
							{
								"/bg/b",
								GameCanvas.typeBg,
								k,
								".png"
							});
							bool flag10 = TileMap.bgType != 0;
							if (flag10)
							{
								path2 = string.Concat(new object[]
								{
									"/bg/b",
									GameCanvas.typeBg,
									k,
									"-",
									TileMap.bgType,
									".png"
								});
							}
							GameCanvas.imgBG[k] = GameCanvas.loadImageRMS(path2);
							bool flag11 = GameCanvas.imgBG[k] != null;
							if (flag11)
							{
								int[] array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, 0, 1, 1);
								GameCanvas.colorTop[k] = array13[0];
								array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[k]) - 1, 1, 1);
								GameCanvas.colorBotton[k] = array13[0];
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
							else
							{
								bool flag12 = GameCanvas.nBg > 1;
								if (flag12)
								{
									GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
									GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
									GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
								}
							}
						}
					}
					GameCanvas.getYBackground(GameCanvas.typeBg);
					GameCanvas.cloudX = new int[]
					{
						GameScr.gW / 2 - 40,
						GameScr.gW / 2 + 40,
						GameScr.gW / 2 - 100,
						GameScr.gW / 2 - 80,
						GameScr.gW / 2 - 120
					};
					GameCanvas.cloudY = new int[]
					{
						130,
						100,
						150,
						140,
						80
					};
					GameCanvas.imgSunSpec = null;
					bool flag13 = GameCanvas.typeBg != 0;
					if (flag13)
					{
						bool flag14 = GameCanvas.typeBg == 2;
						if (flag14)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
							GameCanvas.sunX = GameScr.gW / 2 + 50;
							GameCanvas.sunY = GameCanvas.yb[4] - 40;
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts");
						}
						else
						{
							bool flag15 = GameCanvas.typeBg == 19;
							if (flag15)
							{
								TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_32");
							}
							else
							{
								bool flag16 = GameCanvas.typeBg == 4;
								if (flag16)
								{
									GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
									GameCanvas.sunX = GameScr.gW / 2 + 30;
									GameCanvas.sunY = GameCanvas.yb[3];
								}
								else
								{
									bool flag17 = GameCanvas.typeBg == 7;
									if (flag17)
									{
										GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
										GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
										GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
										GameCanvas.sunY = GameCanvas.yb[3] - 80;
										GameCanvas.sunX2 = GameCanvas.sunX - 100;
										GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
									}
									else
									{
										bool flag18 = GameCanvas.typeBg == 6;
										if (flag18)
										{
											GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
											GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
											GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
											GameCanvas.sunY = GameCanvas.yb[4];
											GameCanvas.sunX2 = GameCanvas.sunX - 100;
											GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
										}
										else
										{
											bool flag19 = typeBG == 5;
											if (flag19)
											{
												GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
												GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
												GameCanvas.sunX = GameScr.gW / 2 - 50;
												GameCanvas.sunY = GameCanvas.yb[3] + 20;
												GameCanvas.sunX2 = GameScr.gW / 2 + 20;
												GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
											}
											else
											{
												bool flag20 = GameCanvas.typeBg == 8 && TileMap.mapID < 90;
												if (flag20)
												{
													GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
													GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
													GameCanvas.sunX = GameScr.gW / 2 - 30;
													GameCanvas.sunY = GameCanvas.yb[3] + 60;
													GameCanvas.sunX2 = GameScr.gW / 2 + 20;
													GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
												}
												else
												{
													bool flag21 = typeBG == 9;
													if (flag21)
													{
														GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
														GameCanvas.sunY = GameCanvas.yb[4] + 20;
														GameCanvas.sunX2 = GameCanvas.sunX - 80;
														GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
													}
													else
													{
														bool flag22 = typeBG == 10;
														if (flag22)
														{
															GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
															GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
															GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
															GameCanvas.sunY = GameCanvas.yb[1] - 30;
															GameCanvas.sunX2 = GameCanvas.sunX - 80;
															GameCanvas.sunY2 = GameCanvas.yb[1];
														}
														else
														{
															bool flag23 = typeBG == 11;
															if (flag23)
															{
																GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
																GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
																GameCanvas.sunX = GameScr.gW / 2 - 30;
																GameCanvas.sunY = GameCanvas.yb[2] - 30;
															}
															else
															{
																bool flag24 = typeBG == 12;
																if (flag24)
																{
																	GameCanvas.cloudY = new int[]
																	{
																		200,
																		170,
																		220,
																		150,
																		250
																	};
																}
																else
																{
																	bool flag25 = typeBG == 16;
																	if (flag25)
																	{
																		GameCanvas.cloudX = new int[]
																		{
																			90,
																			170,
																			250,
																			320,
																			400,
																			450,
																			500
																		};
																		GameCanvas.cloudY = new int[]
																		{
																			GameCanvas.yb[2] + 5,
																			GameCanvas.yb[2] - 20,
																			GameCanvas.yb[2] - 50,
																			GameCanvas.yb[2] - 30,
																			GameCanvas.yb[2] - 50,
																			GameCanvas.yb[2],
																			GameCanvas.yb[2] - 40
																		};
																		GameCanvas.imgSunSpec = new Image[7];
																		for (int l = 0; l < GameCanvas.imgSunSpec.Length; l++)
																		{
																			int num = 161;
																			bool flag26 = l == 0 || l == 2 || l == 3 || l == 2 || l == 6;
																			if (flag26)
																			{
																				num = 160;
																			}
																			GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num.ToString() + ".png");
																		}
																	}
																	else
																	{
																		bool flag27 = typeBG == 19;
																		if (flag27)
																		{
																			int[] array14 = new int[5];
																			array14[1] = 2;
																			array14[2] = 1;
																			GameCanvas.moveX = array14;
																			int[] array15 = new int[5];
																			array15[1] = 2;
																			array15[2] = 1;
																			GameCanvas.moveXSpeed = array15;
																			GameCanvas.nBg = 5;
																		}
																		else
																		{
																			GameCanvas.imgCloud = null;
																			GameCanvas.imgSun = null;
																			GameCanvas.imgSun2 = null;
																			GameCanvas.imgSun = GameCanvas.loadImageRMS(string.Concat(new object[]
																			{
																				"/bg/sun",
																				typeBG,
																				(TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty,
																				".png"
																			}));
																			GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
																			GameCanvas.sunY = GameCanvas.yb[2] - 30;
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
					GameCanvas.paintBG = false;
					bool flag28 = !GameCanvas.paintBG;
					if (flag28)
					{
						GameCanvas.paintBG = true;
					}
				}
			}
		}
		catch (Exception ex2)
		{
			GameCanvas.isLoadBGok = false;
		}
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0003FFD0 File Offset: 0x0003E1D0
	private static void randomRaintEff(int typeBG)
	{
		for (int i = 0; i < GameCanvas.bgRain.Length; i++)
		{
			bool flag = typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0;
			if (flag)
			{
				BackgroudEffect.addEffect(0);
				break;
			}
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0004001C File Offset: 0x0003E21C
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		bool flag = (keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31;
		if (flag)
		{
			GameCanvas.keyAsciiPress = keyCode;
		}
		this.mapKeyPress(keyCode);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00040078 File Offset: 0x0003E278
	public void mapKeyPress(int keyCode)
	{
		bool flag = GameCanvas.currentDialog != null;
		if (flag)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
		}
		else
		{
			GameCanvas.currentScreen.keyPress(keyCode);
			switch (keyCode)
			{
			case 48:
				GameCanvas.keyHold[0] = true;
				GameCanvas.keyPressed[0] = true;
				break;
			case 49:
			{
				bool flag2 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag2)
				{
					GameCanvas.keyHold[1] = true;
					GameCanvas.keyPressed[1] = true;
				}
				break;
			}
			case 50:
			{
				bool flag3 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag3)
				{
					GameCanvas.keyHold[2] = true;
					GameCanvas.keyPressed[2] = true;
				}
				break;
			}
			case 51:
			{
				bool flag4 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag4)
				{
					GameCanvas.keyHold[3] = true;
					GameCanvas.keyPressed[3] = true;
				}
				break;
			}
			case 52:
			{
				bool flag5 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag5)
				{
					GameCanvas.keyHold[4] = true;
					GameCanvas.keyPressed[4] = true;
				}
				break;
			}
			case 53:
			{
				bool flag6 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag6)
				{
					GameCanvas.keyHold[5] = true;
					GameCanvas.keyPressed[5] = true;
				}
				break;
			}
			case 54:
			{
				bool flag7 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag7)
				{
					GameCanvas.keyHold[6] = true;
					GameCanvas.keyPressed[6] = true;
				}
				break;
			}
			case 55:
				GameCanvas.keyHold[7] = true;
				GameCanvas.keyPressed[7] = true;
				break;
			case 56:
			{
				bool flag8 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
				if (flag8)
				{
					GameCanvas.keyHold[8] = true;
					GameCanvas.keyPressed[8] = true;
				}
				break;
			}
			case 57:
				GameCanvas.keyHold[9] = true;
				GameCanvas.keyPressed[9] = true;
				break;
			default:
			{
				switch (keyCode + 8)
				{
				case 0:
					GameCanvas.keyHold[14] = true;
					GameCanvas.keyPressed[14] = true;
					return;
				case 1:
					goto IL_5EB;
				case 2:
					goto IL_5D6;
				case 3:
					goto IL_575;
				case 4:
				{
					bool flag9 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
					if (flag9)
					{
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						return;
					}
					GameCanvas.keyHold[24] = true;
					GameCanvas.keyPressed[24] = true;
					return;
				}
				case 5:
				{
					bool flag10 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
					if (flag10)
					{
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						return;
					}
					GameCanvas.keyHold[23] = true;
					GameCanvas.keyPressed[23] = true;
					return;
				}
				case 6:
					goto IL_520;
				case 7:
					break;
				default:
				{
					bool flag11 = keyCode == -39;
					if (flag11)
					{
						goto IL_520;
					}
					bool flag12 = keyCode != -38;
					if (flag12)
					{
						bool flag13 = keyCode == -22;
						if (flag13)
						{
							goto IL_5EB;
						}
						bool flag14 = keyCode == -21;
						if (flag14)
						{
							goto IL_5D6;
						}
						bool flag15 = keyCode == -26;
						if (flag15)
						{
							GameCanvas.keyHold[16] = true;
							GameCanvas.keyPressed[16] = true;
							return;
						}
						bool flag16 = keyCode == 10;
						if (flag16)
						{
							goto IL_575;
						}
						bool flag17 = keyCode == 35;
						if (flag17)
						{
							GameCanvas.keyHold[11] = true;
							GameCanvas.keyPressed[11] = true;
							return;
						}
						bool flag18 = keyCode == 42;
						if (flag18)
						{
							GameCanvas.keyHold[10] = true;
							GameCanvas.keyPressed[10] = true;
							return;
						}
						bool flag19 = keyCode != 113;
						if (flag19)
						{
							return;
						}
						GameCanvas.keyHold[17] = true;
						GameCanvas.keyPressed[17] = true;
						return;
					}
					break;
				}
				}
				bool flag20 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
				if (flag20)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					break;
				}
				GameCanvas.keyHold[21] = true;
				GameCanvas.keyPressed[21] = true;
				break;
				IL_520:
				bool flag21 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
				if (flag21)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					break;
				}
				GameCanvas.keyHold[22] = true;
				GameCanvas.keyPressed[22] = true;
				break;
				IL_575:
				bool flag22 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
				if (flag22)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					break;
				}
				GameCanvas.keyHold[25] = true;
				GameCanvas.keyPressed[25] = true;
				GameCanvas.keyHold[15] = true;
				GameCanvas.keyPressed[15] = true;
				break;
				IL_5D6:
				GameCanvas.keyHold[12] = true;
				GameCanvas.keyPressed[12] = true;
				break;
				IL_5EB:
				GameCanvas.keyHold[13] = true;
				GameCanvas.keyPressed[13] = true;
				break;
			}
			}
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00040685 File Offset: 0x0003E885
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00040698 File Offset: 0x0003E898
	public void mapKeyRelease(int keyCode)
	{
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = false;
			GameCanvas.keyReleased[0] = true;
			break;
		case 49:
		{
			bool flag = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag)
			{
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyReleased[1] = true;
			}
			break;
		}
		case 50:
		{
			bool flag2 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag2)
			{
				GameCanvas.keyHold[2] = false;
				GameCanvas.keyReleased[2] = true;
			}
			break;
		}
		case 51:
		{
			bool flag3 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag3)
			{
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyReleased[3] = true;
			}
			break;
		}
		case 52:
		{
			bool flag4 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag4)
			{
				GameCanvas.keyHold[4] = false;
				GameCanvas.keyReleased[4] = true;
			}
			break;
		}
		case 53:
		{
			bool flag5 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag5)
			{
				GameCanvas.keyHold[5] = false;
				GameCanvas.keyReleased[5] = true;
			}
			break;
		}
		case 54:
		{
			bool flag6 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag6)
			{
				GameCanvas.keyHold[6] = false;
				GameCanvas.keyReleased[6] = true;
			}
			break;
		}
		case 55:
			GameCanvas.keyHold[7] = false;
			GameCanvas.keyReleased[7] = true;
			break;
		case 56:
		{
			bool flag7 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
			if (flag7)
			{
				GameCanvas.keyHold[8] = false;
				GameCanvas.keyReleased[8] = true;
			}
			break;
		}
		case 57:
			GameCanvas.keyHold[9] = false;
			GameCanvas.keyReleased[9] = true;
			break;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = false;
				return;
			case 1:
				goto IL_446;
			case 2:
				goto IL_431;
			case 3:
				goto IL_40A;
			case 4:
				GameCanvas.keyHold[24] = false;
				return;
			case 5:
				GameCanvas.keyHold[23] = false;
				return;
			case 6:
				goto IL_3FE;
			case 7:
				break;
			default:
			{
				bool flag8 = keyCode == -39;
				if (flag8)
				{
					goto IL_3FE;
				}
				bool flag9 = keyCode != -38;
				if (flag9)
				{
					bool flag10 = keyCode == -22;
					if (flag10)
					{
						goto IL_446;
					}
					bool flag11 = keyCode == -21;
					if (flag11)
					{
						goto IL_431;
					}
					bool flag12 = keyCode == -26;
					if (flag12)
					{
						GameCanvas.keyHold[16] = false;
						return;
					}
					bool flag13 = keyCode == 10;
					if (flag13)
					{
						goto IL_40A;
					}
					bool flag14 = keyCode == 35;
					if (flag14)
					{
						GameCanvas.keyHold[11] = false;
						GameCanvas.keyReleased[11] = true;
						return;
					}
					bool flag15 = keyCode == 42;
					if (flag15)
					{
						GameCanvas.keyHold[10] = false;
						GameCanvas.keyReleased[10] = true;
						return;
					}
					bool flag16 = keyCode != 113;
					if (flag16)
					{
						return;
					}
					GameCanvas.keyHold[17] = false;
					GameCanvas.keyReleased[17] = true;
					return;
				}
				break;
			}
			}
			GameCanvas.keyHold[21] = false;
			break;
			IL_3FE:
			GameCanvas.keyHold[22] = false;
			break;
			IL_40A:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			break;
			IL_431:
			GameCanvas.keyHold[12] = false;
			GameCanvas.keyReleased[12] = true;
			break;
			IL_446:
			GameCanvas.keyHold[13] = false;
			GameCanvas.keyReleased[13] = true;
			break;
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x00040B00 File Offset: 0x0003ED00
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00040B10 File Offset: 0x0003ED10
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		bool flag = GameCanvas.panel != null && GameCanvas.panel.isShow;
		if (flag)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00040B4C File Offset: 0x0003ED4C
	public void pointerDragged(int x, int y)
	{
		bool flag = Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10;
		if (flag)
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerMove = true;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		bool flag2 = GameCanvas.curPos > 3;
		if (flag2)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00040BD8 File Offset: 0x0003EDD8
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00040C00 File Offset: 0x0003EE00
	public void pointerPressed(int x, int y)
	{
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerJustDown = true;
		GameCanvas.isPointerDown = true;
		GameCanvas.isPointerClick = true;
		GameCanvas.isPointerMove = false;
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.pxFirst = x;
		GameCanvas.pyFirst = y;
		GameCanvas.pxLast = x;
		GameCanvas.pyLast = y;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00040C5A File Offset: 0x0003EE5A
	public void pointerReleased(int x, int y)
	{
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustRelease = true;
		GameCanvas.isPointerMove = false;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00040C84 File Offset: 0x0003EE84
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y) && GameCanvas.py <= y + h;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00040CD4 File Offset: 0x0003EED4
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00040D10 File Offset: 0x0003EF10
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00040D44 File Offset: 0x0003EF44
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00040D74 File Offset: 0x0003EF74
	public static void checkBackButton()
	{
		bool flag = ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00040DD0 File Offset: 0x0003EFD0
	public void paintChangeMap(mGraphics g)
	{
		string empty = string.Empty;
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? empty : (" " + LoginScr.timeLogin.ToString() + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00040E88 File Offset: 0x0003F088
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			bool flag = GameCanvas.currentScreen != null;
			if (flag)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			bool isShow = GameCanvas.panel.isShow;
			if (isShow)
			{
				GameCanvas.panel.paint(this.g);
				bool flag2 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow;
				if (flag2)
				{
					GameCanvas.panel2.paint(this.g);
				}
				bool flag3 = GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow;
				if (flag3)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow;
				if (flag4)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			bool flag5 = GameCanvas.currentDialog != null;
			if (flag5)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else
			{
				bool showMenu = GameCanvas.menu.showMenu;
				if (showMenu)
				{
					GameCanvas.debug("PD", 1);
					GameCanvas.menu.paintMenu(this.g);
				}
			}
			GameScr.info1.paint(this.g);
			GameScr.info2.paint(this.g);
			bool flag6 = GameScr.gI().popUpYesNo != null;
			if (flag6)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			bool flag7 = ChatPopup.currChatPopup != null;
			if (flag7)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			bool flag8 = ChatPopup.serverChatPopUp != null;
			if (flag8)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				bool flag9 = effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp);
				if (flag9)
				{
					effect.paint(this.g);
				}
			}
			bool flag10 = global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait;
			if (flag10)
			{
				this.paintChangeMap(this.g);
				bool flag11 = GameCanvas.timeLoading > 0 && LoginScr.timeLogin <= 0;
				if (flag11)
				{
					GameCanvas.startWaitDlg();
					bool flag12 = mSystem.currentTimeMillis() - GameCanvas.TIMEOUT >= 1000L;
					if (flag12)
					{
						GameCanvas.timeLoading--;
						Res.outz("[COUNT] == " + GameCanvas.timeLoading.ToString());
						bool flag13 = GameCanvas.timeLoading == 0;
						if (flag13)
						{
							GameCanvas.timeLoading = 15;
						}
						GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
					}
				}
				bool flag14 = mSystem.currentTimeMillis() > GameCanvas.timeBreakLoading;
				if (flag14)
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
					bool flag15 = GameCanvas.currentScreen != null;
					if (flag15)
					{
						bool flag16 = GameCanvas.currentScreen is GameScr;
						if (flag16)
						{
							GameScr.gI().switchToMe();
						}
						else
						{
							bool flag17 = !(GameCanvas.currentScreen is SplashScr);
							if (flag17)
							{
								bool flag18 = GameCanvas.currentScreen is LoginScr;
								if (flag18)
								{
									GameCanvas.gI().resetToLoginScrz();
								}
							}
						}
					}
				}
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			bool flag19 = GameCanvas.open3Hour && !GameCanvas.isLoading;
			if (flag19)
			{
				bool flag20 = GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen || GameCanvas.currentScreen == GameCanvas.serverScr;
				if (flag20)
				{
					this.g.drawImage(GameCanvas.img12, 5, 5, 0);
				}
				bool flag21 = GameCanvas.currentScreen == CreateCharScr.instance;
				if (flag21)
				{
					this.g.drawImage(GameCanvas.img12, 5, 20, 0);
				}
			}
			GameCanvas.resetTrans(this.g);
			int num = GameCanvas.h / 4;
			bool flag22 = GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr && GameCanvas.thongBaoTest != null;
			if (flag22)
			{
				this.g.setClip(60, num, GameCanvas.w - 120, mFont.tahoma_7_white.getHeight() + 2);
				mFont.tahoma_7_grey.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num + 1, 0);
				mFont.tahoma_7_yellow.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num, 0);
				this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00041434 File Offset: 0x0003F634
	public static void endDlg()
	{
		bool flag = GameCanvas.inputDlg != null;
		if (flag)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00041471 File Offset: 0x0003F671
	public static void startOKDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x000414A8 File Offset: 0x0003F6A8
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x000414F4 File Offset: 0x0003F6F4
	public static void startOKDlg(string info, bool isError)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00041540 File Offset: 0x0003F740
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0004154F File Offset: 0x0003F74F
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00041584 File Offset: 0x0003F784
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x000415B8 File Offset: 0x0003F7B8
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00041617 File Offset: 0x0003F817
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0004163A File Offset: 0x0003F83A
	public static void startserverThongBao(string msgSv)
	{
		GameCanvas.thongBaoTest = msgSv;
		GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
		GameCanvas.dir_ = -1;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00041658 File Offset: 0x0003F858
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			bool flag = m < 1000;
			if (flag)
			{
				text = m.ToString() + text;
				break;
			}
			int num2 = m % 1000;
			bool flag2 = num2 == 0;
			if (flag2)
			{
				text = ".000" + text;
			}
			else
			{
				bool flag3 = num2 < 10;
				if (flag3)
				{
					text = ".00" + num2.ToString() + text;
				}
				else
				{
					bool flag4 = num2 < 100;
					if (flag4)
					{
						text = ".0" + num2.ToString() + text;
					}
					else
					{
						text = "." + num2.ToString() + text;
					}
				}
			}
			m /= 1000;
		}
		return text;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00041740 File Offset: 0x0003F940
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0004175C File Offset: 0x0003F95C
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x060002CD RID: 717 RVA: 0x00003136 File Offset: 0x00001336
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00041778 File Offset: 0x0003F978
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0004178C File Offset: 0x0003F98C
	public static Image loadImageRMS(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception ex)
		{
			try
			{
				string[] array = Res.split(path, "/", 0);
				string filename = "x" + mGraphics.zoomLevel.ToString() + array[array.Length - 1];
				sbyte[] array2 = Rms.loadRMS(filename);
				bool flag = array2 != null;
				if (flag)
				{
					result = Image.createImage(array2, 0, array2.Length);
				}
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
			}
		}
		return result;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00041870 File Offset: 0x0003FA70
	public static Image loadImage(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception ex)
		{
		}
		return result;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x000418DC File Offset: 0x0003FADC
	public static string cutPng(string str)
	{
		string result = str;
		bool flag = str.Contains(".png");
		if (flag)
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00041914 File Offset: 0x0003FB14
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00041938 File Offset: 0x0003FB38
	public bool startDust(int dir, int x, int y)
	{
		bool flag = GameCanvas.lowGraphic;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (dir != 1) ? 1 : 0;
			bool flag2 = this.dustState[num] != -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				this.dustState[num] = 0;
				this.dustX[num] = x;
				this.dustY[num] = y;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00041994 File Offset: 0x0003FB94
	public void loadWaterSplash()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			GameCanvas.imgWS = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i.ToString() + ".png");
			}
			GameCanvas.wsX = new int[2];
			GameCanvas.wsY = new int[2];
			GameCanvas.wsState = new int[2];
			GameCanvas.wsF = new int[2];
			GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00041A2C File Offset: 0x0003FC2C
	public bool startWaterSplash(int x, int y)
	{
		bool flag = GameCanvas.lowGraphic;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (GameCanvas.wsState[0] != -1) ? 1 : 0;
			bool flag2 = GameCanvas.wsState[num] != -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				GameCanvas.wsState[num] = 0;
				GameCanvas.wsX[num] = x;
				GameCanvas.wsY[num] = y;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00041A8C File Offset: 0x0003FC8C
	public void updateWaterSplash()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = GameCanvas.wsState[i] != -1;
				if (flag2)
				{
					GameCanvas.wsY[i]--;
					bool flag3 = GameCanvas.gameTick % 2 == 0;
					if (flag3)
					{
						GameCanvas.wsState[i]++;
						bool flag4 = GameCanvas.wsState[i] > 2;
						if (flag4)
						{
							GameCanvas.wsState[i] = -1;
						}
						else
						{
							GameCanvas.wsF[i] = GameCanvas.wsState[i];
						}
					}
				}
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00041B2C File Offset: 0x0003FD2C
	public void updateDust()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = this.dustState[i] != -1;
				if (flag2)
				{
					this.dustState[i]++;
					bool flag3 = this.dustState[i] >= 5;
					if (flag3)
					{
						this.dustState[i] = -1;
					}
					bool flag4 = i == 0;
					if (flag4)
					{
						this.dustX[i]--;
					}
					else
					{
						this.dustX[i]++;
					}
					this.dustY[i]--;
				}
			}
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00041BE8 File Offset: 0x0003FDE8
	public static bool isPaint(int x, int y)
	{
		return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00041C30 File Offset: 0x0003FE30
	public void paintDust(mGraphics g)
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]);
				if (flag2)
				{
					g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
				}
			}
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00041CB0 File Offset: 0x0003FEB0
	public void loadDust()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			bool flag2 = GameCanvas.imgDust == null;
			if (flag2)
			{
				GameCanvas.imgDust = new Image[2][];
				for (int i = 0; i < GameCanvas.imgDust.Length; i++)
				{
					GameCanvas.imgDust[i] = new Image[5];
				}
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						GameCanvas.imgDust[j][k] = GameCanvas.loadImage(string.Concat(new object[]
						{
							"/e/d",
							j,
							k,
							".png"
						}));
					}
				}
			}
			this.dustX = new int[2];
			this.dustY = new int[2];
			this.dustState = new int[2];
			this.dustState[0] = (this.dustState[1] = -1);
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00041DB8 File Offset: 0x0003FFB8
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00041DED File Offset: 0x0003FFED
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00041DF8 File Offset: 0x0003FFF8
	public static bool isPointer(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y) && GameCanvas.py <= y + h;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00041E48 File Offset: 0x00040048
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 88810:
		{
			int playerMapId = (int)p;
			GameCanvas.endDlg();
			Service.gI().acceptInviteTrade(playerMapId);
			return;
		}
		case 88811:
			GameCanvas.endDlg();
			Service.gI().cancelInviteTrade();
			return;
		case 88814:
		{
			Item[] items = (Item[])p;
			GameCanvas.endDlg();
			Service.gI().crystalCollectLock(items);
			return;
		}
		case 88815:
			return;
		case 88817:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88818:
		{
			short menuId = (short)p;
			Service.gI().textBoxId(menuId, GameCanvas.inputDlg.tfInput.getText());
			GameCanvas.endDlg();
			return;
		}
		case 88819:
		{
			short menuId2 = (short)p;
			Service.gI().menuId(menuId2);
			return;
		}
		case 88820:
		{
			string[] array = (string[])p;
			bool flag = global::Char.myCharz().npcFocus == null;
			if (flag)
			{
				return;
			}
			int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
			bool flag2 = array.Length > 1;
			if (flag2)
			{
				MyVector myVector = new MyVector();
				for (int i = 0; i < array.Length - 1; i++)
				{
					myVector.addElement(new Command(array[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
				}
				GameCanvas.menu.startAt(myVector, 3);
			}
			else
			{
				ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
				Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
			}
			return;
		}
		case 88821:
		{
			int menuId3 = (int)p;
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuId3, GameCanvas.menu.menuSelectedItem);
			return;
		}
		case 88822:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88823:
			GameCanvas.startOKDlg(mResources.SENTMSG);
			return;
		case 88824:
			GameCanvas.startOKDlg(mResources.NOSENDMSG);
			return;
		case 88825:
			GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
			return;
		case 88826:
			GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
			return;
		case 88827:
			GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
			return;
		case 88828:
			GameCanvas.startOKDlg(mResources.sendMsgFail);
			return;
		case 88829:
		{
			string text3 = GameCanvas.inputDlg.tfInput.getText();
			bool flag3 = text3.Equals(string.Empty);
			if (flag3)
			{
				return;
			}
			Service.gI().changeName(text3, (int)p);
			InfoDlg.showWait();
			return;
		}
		case 88836:
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
			GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
			return;
		case 88837:
		{
			string text4 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			try
			{
				Service.gI().openLockAccProtect(int.Parse(text4.Trim()));
			}
			catch (Exception ex3)
			{
				Cout.println("Loi tai 88837 " + ex3.ToString());
			}
			return;
		}
		case 88839:
		{
			string text5 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			bool flag4 = text5.Length < 6 || text5.Equals(string.Empty);
			if (flag4)
			{
				GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
			}
			else
			{
				try
				{
					GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text5, 8882, null);
				}
				catch (Exception ex4)
				{
					GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
				}
			}
			return;
		}
		}
		switch (idAction)
		{
		case 8881:
		{
			string url = (string)p;
			try
			{
				GameMidlet.instance.platformRequest(url);
			}
			catch (Exception ex5)
			{
			}
			GameCanvas.currentDialog = null;
			return;
		}
		case 8882:
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
			ServerListScreen.isAutoConect = false;
			ServerListScreen.countDieConnect = 0;
			return;
		case 8884:
			GameCanvas.endDlg();
			GameCanvas.loginScr.switchToMe();
			return;
		case 8885:
			GameMidlet.instance.exit();
			return;
		case 8886:
		{
			GameCanvas.endDlg();
			string name = (string)p;
			Service.gI().addFriend(name);
			return;
		}
		case 8887:
		{
			GameCanvas.endDlg();
			int charId = (int)p;
			Service.gI().addPartyAccept(charId);
			return;
		}
		case 8888:
		{
			int charId2 = (int)p;
			Service.gI().addPartyCancel(charId2);
			GameCanvas.endDlg();
			return;
		}
		case 8889:
		{
			string str = (string)p;
			GameCanvas.endDlg();
			Service.gI().acceptPleaseParty(str);
			return;
		}
		}
		switch (idAction)
		{
		case 888391:
		{
			string s = (string)p;
			GameCanvas.endDlg();
			Service.gI().clearAccProtect(int.Parse(s));
			break;
		}
		case 888392:
			Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
			break;
		case 888393:
		{
			bool flag5 = GameCanvas.loginScr == null;
			if (flag5)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.doLogin();
			Main.closeKeyBoard();
			break;
		}
		case 888394:
			GameCanvas.endDlg();
			break;
		case 888395:
			GameCanvas.endDlg();
			break;
		case 888396:
			GameCanvas.endDlg();
			break;
		case 888397:
		{
			string text6 = (string)p;
			break;
		}
		default:
			switch (idAction)
			{
			case 101023:
				Main.numberQuit = 0;
				break;
			case 101024:
				Res.outz("output 101024");
				GameCanvas.endDlg();
				break;
			case 101025:
			{
				GameCanvas.endDlg();
				bool loadScreen = ServerListScreen.loadScreen;
				if (loadScreen)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				else
				{
					GameCanvas.serverScreen.show2();
				}
				break;
			}
			default:
			{
				bool flag6 = idAction != 999;
				if (flag6)
				{
					bool flag7 = idAction != 9000;
					if (flag7)
					{
						bool flag8 = idAction != 9999;
						if (flag8)
						{
							bool flag9 = idAction == 888361;
							if (flag9)
							{
								string text7 = GameCanvas.inputDlg.tfInput.getText();
								GameCanvas.endDlg();
								bool flag10 = text7.Length < 6 || text7.Equals(string.Empty);
								if (flag10)
								{
									GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
								}
								else
								{
									try
									{
										Service.gI().activeAccProtect(int.Parse(text7));
									}
									catch (Exception ex6)
									{
										GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
										Cout.println("Loi tai 888361 Gamescavas " + ex6.ToString());
									}
								}
							}
						}
						else
						{
							GameCanvas.endDlg();
							GameCanvas.connect();
							Service.gI().setClientType();
							bool flag11 = GameCanvas.loginScr == null;
							if (flag11)
							{
								GameCanvas.loginScr = new LoginScr();
							}
							GameCanvas.loginScr.doLogin();
						}
					}
					else
					{
						GameCanvas.endDlg();
						SplashScr.imgLogo = null;
						SmallImage.loadBigRMS();
						mSystem.gcc();
						ServerListScreen.bigOk = true;
						ServerListScreen.loadScreen = true;
						GameScr.gI().loadGameScr();
						bool flag12 = GameCanvas.currentScreen != GameCanvas.loginScr;
						if (flag12)
						{
							GameCanvas.serverScreen.switchToMe2();
						}
					}
				}
				else
				{
					mSystem.closeBanner();
					GameCanvas.endDlg();
				}
				break;
			}
			}
			break;
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00042710 File Offset: 0x00040910
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00003136 File Offset: 0x00001336
	public static void backToRegister()
	{
	}

	// Token: 0x040005C8 RID: 1480
	public static long timeNow = 0L;

	// Token: 0x040005C9 RID: 1481
	public static bool open3Hour;

	// Token: 0x040005CA RID: 1482
	public static bool lowGraphic = false;

	// Token: 0x040005CB RID: 1483
	public static bool serverchat = false;

	// Token: 0x040005CC RID: 1484
	public static bool isMoveNumberPad = true;

	// Token: 0x040005CD RID: 1485
	public static bool isLoading;

	// Token: 0x040005CE RID: 1486
	public static bool isTouch = false;

	// Token: 0x040005CF RID: 1487
	public static bool isTouchControl;

	// Token: 0x040005D0 RID: 1488
	public static bool isTouchControlSmallScreen;

	// Token: 0x040005D1 RID: 1489
	public static bool isTouchControlLargeScreen;

	// Token: 0x040005D2 RID: 1490
	public static bool isConnectFail;

	// Token: 0x040005D3 RID: 1491
	public static GameCanvas instance;

	// Token: 0x040005D4 RID: 1492
	public static bool bRun;

	// Token: 0x040005D5 RID: 1493
	public static bool[] keyPressed = new bool[30];

	// Token: 0x040005D6 RID: 1494
	public static bool[] keyReleased = new bool[30];

	// Token: 0x040005D7 RID: 1495
	public static bool[] keyHold = new bool[30];

	// Token: 0x040005D8 RID: 1496
	public static bool isPointerDown;

	// Token: 0x040005D9 RID: 1497
	public static bool isPointerClick;

	// Token: 0x040005DA RID: 1498
	public static bool isPointerJustRelease;

	// Token: 0x040005DB RID: 1499
	public static bool isPointerMove;

	// Token: 0x040005DC RID: 1500
	public static int px;

	// Token: 0x040005DD RID: 1501
	public static int py;

	// Token: 0x040005DE RID: 1502
	public static int pxFirst;

	// Token: 0x040005DF RID: 1503
	public static int pyFirst;

	// Token: 0x040005E0 RID: 1504
	public static int pxLast;

	// Token: 0x040005E1 RID: 1505
	public static int pyLast;

	// Token: 0x040005E2 RID: 1506
	public static int pxMouse;

	// Token: 0x040005E3 RID: 1507
	public static int pyMouse;

	// Token: 0x040005E4 RID: 1508
	public static Position[] arrPos = new Position[4];

	// Token: 0x040005E5 RID: 1509
	public static int gameTick;

	// Token: 0x040005E6 RID: 1510
	public static int taskTick;

	// Token: 0x040005E7 RID: 1511
	public static bool isEff1;

	// Token: 0x040005E8 RID: 1512
	public static bool isEff2;

	// Token: 0x040005E9 RID: 1513
	public static long timeTickEff1;

	// Token: 0x040005EA RID: 1514
	public static long timeTickEff2;

	// Token: 0x040005EB RID: 1515
	public static int w;

	// Token: 0x040005EC RID: 1516
	public static int h;

	// Token: 0x040005ED RID: 1517
	public static int hw;

	// Token: 0x040005EE RID: 1518
	public static int hh;

	// Token: 0x040005EF RID: 1519
	public static int wd3;

	// Token: 0x040005F0 RID: 1520
	public static int hd3;

	// Token: 0x040005F1 RID: 1521
	public static int w2d3;

	// Token: 0x040005F2 RID: 1522
	public static int h2d3;

	// Token: 0x040005F3 RID: 1523
	public static int w3d4;

	// Token: 0x040005F4 RID: 1524
	public static int h3d4;

	// Token: 0x040005F5 RID: 1525
	public static int wd6;

	// Token: 0x040005F6 RID: 1526
	public static int hd6;

	// Token: 0x040005F7 RID: 1527
	public static mScreen currentScreen;

	// Token: 0x040005F8 RID: 1528
	public static Menu menu = new Menu();

	// Token: 0x040005F9 RID: 1529
	public static Panel panel;

	// Token: 0x040005FA RID: 1530
	public static Panel panel2;

	// Token: 0x040005FB RID: 1531
	public static ChooseCharScr chooseCharScr;

	// Token: 0x040005FC RID: 1532
	public static LoginScr loginScr;

	// Token: 0x040005FD RID: 1533
	public static RegisterScreen registerScr;

	// Token: 0x040005FE RID: 1534
	public static Dialog currentDialog;

	// Token: 0x040005FF RID: 1535
	public static MsgDlg msgdlg;

	// Token: 0x04000600 RID: 1536
	public static InputDlg inputDlg;

	// Token: 0x04000601 RID: 1537
	public static MyVector currentPopup = new MyVector();

	// Token: 0x04000602 RID: 1538
	public static int requestLoseCount;

	// Token: 0x04000603 RID: 1539
	public static MyVector listPoint;

	// Token: 0x04000604 RID: 1540
	public static Paint paintz;

	// Token: 0x04000605 RID: 1541
	public static bool isGetResFromServer;

	// Token: 0x04000606 RID: 1542
	public static Image[] imgBG;

	// Token: 0x04000607 RID: 1543
	public static int skyColor;

	// Token: 0x04000608 RID: 1544
	public static int curPos = 0;

	// Token: 0x04000609 RID: 1545
	public static int[] bgW;

	// Token: 0x0400060A RID: 1546
	public static int[] bgH;

	// Token: 0x0400060B RID: 1547
	public static int planet = 0;

	// Token: 0x0400060C RID: 1548
	private mGraphics g = new mGraphics();

	// Token: 0x0400060D RID: 1549
	public static Image img12;

	// Token: 0x0400060E RID: 1550
	public static Image[] imgBlue = new Image[7];

	// Token: 0x0400060F RID: 1551
	public static Image[] imgViolet = new Image[7];

	// Token: 0x04000610 RID: 1552
	public static MyHashTable danhHieu = new MyHashTable();

	// Token: 0x04000611 RID: 1553
	public static MyVector messageServer = new MyVector(string.Empty);

	// Token: 0x04000612 RID: 1554
	public static bool isPlaySound = true;

	// Token: 0x04000613 RID: 1555
	private static int clearOldData;

	// Token: 0x04000614 RID: 1556
	public static int timeOpenKeyBoard;

	// Token: 0x04000615 RID: 1557
	public static bool isFocusPanel2;

	// Token: 0x04000616 RID: 1558
	public static int fps = 0;

	// Token: 0x04000617 RID: 1559
	public static int max;

	// Token: 0x04000618 RID: 1560
	public static int up;

	// Token: 0x04000619 RID: 1561
	public static int upmax;

	// Token: 0x0400061A RID: 1562
	private long timefps = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x0400061B RID: 1563
	private long timeup = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x0400061C RID: 1564
	private static int dir_ = -1;

	// Token: 0x0400061D RID: 1565
	private int tickWaitThongBao;

	// Token: 0x0400061E RID: 1566
	public bool isPaintCarret;

	// Token: 0x0400061F RID: 1567
	public static MyVector debugUpdate;

	// Token: 0x04000620 RID: 1568
	public static MyVector debugPaint;

	// Token: 0x04000621 RID: 1569
	public static MyVector debugSession;

	// Token: 0x04000622 RID: 1570
	private static bool isShowErrorForm = false;

	// Token: 0x04000623 RID: 1571
	public static bool paintBG;

	// Token: 0x04000624 RID: 1572
	public static int gsskyHeight;

	// Token: 0x04000625 RID: 1573
	public static int gsgreenField1Y;

	// Token: 0x04000626 RID: 1574
	public static int gsgreenField2Y;

	// Token: 0x04000627 RID: 1575
	public static int gshouseY;

	// Token: 0x04000628 RID: 1576
	public static int gsmountainY;

	// Token: 0x04000629 RID: 1577
	public static int bgLayer0y;

	// Token: 0x0400062A RID: 1578
	public static int bgLayer1y;

	// Token: 0x0400062B RID: 1579
	public static Image imgCloud;

	// Token: 0x0400062C RID: 1580
	public static Image imgSun;

	// Token: 0x0400062D RID: 1581
	public static Image imgSun2;

	// Token: 0x0400062E RID: 1582
	public static Image imgClear;

	// Token: 0x0400062F RID: 1583
	public static Image[] imgBorder = new Image[3];

	// Token: 0x04000630 RID: 1584
	public static Image[] imgSunSpec = new Image[3];

	// Token: 0x04000631 RID: 1585
	public static int borderConnerW;

	// Token: 0x04000632 RID: 1586
	public static int borderConnerH;

	// Token: 0x04000633 RID: 1587
	public static int borderCenterW;

	// Token: 0x04000634 RID: 1588
	public static int borderCenterH;

	// Token: 0x04000635 RID: 1589
	public static int[] cloudX;

	// Token: 0x04000636 RID: 1590
	public static int[] cloudY;

	// Token: 0x04000637 RID: 1591
	public static int sunX;

	// Token: 0x04000638 RID: 1592
	public static int sunY;

	// Token: 0x04000639 RID: 1593
	public static int sunX2;

	// Token: 0x0400063A RID: 1594
	public static int sunY2;

	// Token: 0x0400063B RID: 1595
	public static int[] layerSpeed;

	// Token: 0x0400063C RID: 1596
	public static int[] moveX;

	// Token: 0x0400063D RID: 1597
	public static int[] moveXSpeed;

	// Token: 0x0400063E RID: 1598
	public static bool isBoltEff;

	// Token: 0x0400063F RID: 1599
	public static bool boltActive;

	// Token: 0x04000640 RID: 1600
	public static int tBolt;

	// Token: 0x04000641 RID: 1601
	public static Image imgBgIOS;

	// Token: 0x04000642 RID: 1602
	public static int typeBg = -1;

	// Token: 0x04000643 RID: 1603
	public static int transY;

	// Token: 0x04000644 RID: 1604
	public static int[] yb = new int[5];

	// Token: 0x04000645 RID: 1605
	public static int[] colorTop;

	// Token: 0x04000646 RID: 1606
	public static int[] colorBotton;

	// Token: 0x04000647 RID: 1607
	public static int yb1;

	// Token: 0x04000648 RID: 1608
	public static int yb2;

	// Token: 0x04000649 RID: 1609
	public static int yb3;

	// Token: 0x0400064A RID: 1610
	public static int nBg = 0;

	// Token: 0x0400064B RID: 1611
	public static int lastBg = -1;

	// Token: 0x0400064C RID: 1612
	public static int[] bgRain = new int[]
	{
		1,
		4,
		11
	};

	// Token: 0x0400064D RID: 1613
	public static int[] bgRainFont = new int[]
	{
		-1
	};

	// Token: 0x0400064E RID: 1614
	public static Image imgCaycot;

	// Token: 0x0400064F RID: 1615
	public static Image tam;

	// Token: 0x04000650 RID: 1616
	public static int typeBackGround = -1;

	// Token: 0x04000651 RID: 1617
	public static int saveIDBg = -10;

	// Token: 0x04000652 RID: 1618
	public static bool isLoadBGok;

	// Token: 0x04000653 RID: 1619
	private static long lastTimePress = 0L;

	// Token: 0x04000654 RID: 1620
	public static int keyAsciiPress;

	// Token: 0x04000655 RID: 1621
	public static int pXYScrollMouse;

	// Token: 0x04000656 RID: 1622
	private static Image imgSignal;

	// Token: 0x04000657 RID: 1623
	public static MyVector flyTexts = new MyVector();

	// Token: 0x04000658 RID: 1624
	public int longTime;

	// Token: 0x04000659 RID: 1625
	public static long timeBreakLoading;

	// Token: 0x0400065A RID: 1626
	private static string thongBaoTest;

	// Token: 0x0400065B RID: 1627
	public static int xThongBaoTranslate = GameCanvas.w - 60;

	// Token: 0x0400065C RID: 1628
	public static bool isPointerJustDown = false;

	// Token: 0x0400065D RID: 1629
	private int count = 1;

	// Token: 0x0400065E RID: 1630
	public static bool csWait;

	// Token: 0x0400065F RID: 1631
	public static MyRandom r = new MyRandom();

	// Token: 0x04000660 RID: 1632
	public static bool isBlackScreen;

	// Token: 0x04000661 RID: 1633
	public static int[] bgSpeed;

	// Token: 0x04000662 RID: 1634
	public static int cmdBarX;

	// Token: 0x04000663 RID: 1635
	public static int cmdBarY;

	// Token: 0x04000664 RID: 1636
	public static int cmdBarW;

	// Token: 0x04000665 RID: 1637
	public static int cmdBarH;

	// Token: 0x04000666 RID: 1638
	public static int cmdBarLeftW;

	// Token: 0x04000667 RID: 1639
	public static int cmdBarRightW;

	// Token: 0x04000668 RID: 1640
	public static int cmdBarCenterW;

	// Token: 0x04000669 RID: 1641
	public static int hpBarX;

	// Token: 0x0400066A RID: 1642
	public static int hpBarY;

	// Token: 0x0400066B RID: 1643
	public static int hpBarW;

	// Token: 0x0400066C RID: 1644
	public static int expBarW;

	// Token: 0x0400066D RID: 1645
	public static int lvPosX;

	// Token: 0x0400066E RID: 1646
	public static int moneyPosX;

	// Token: 0x0400066F RID: 1647
	public static int hpBarH;

	// Token: 0x04000670 RID: 1648
	public static int girlHPBarY;

	// Token: 0x04000671 RID: 1649
	public int timeOut;

	// Token: 0x04000672 RID: 1650
	public int[] dustX;

	// Token: 0x04000673 RID: 1651
	public int[] dustY;

	// Token: 0x04000674 RID: 1652
	public int[] dustState;

	// Token: 0x04000675 RID: 1653
	public static int[] wsX;

	// Token: 0x04000676 RID: 1654
	public static int[] wsY;

	// Token: 0x04000677 RID: 1655
	public static int[] wsState;

	// Token: 0x04000678 RID: 1656
	public static int[] wsF;

	// Token: 0x04000679 RID: 1657
	public static Image[] imgWS;

	// Token: 0x0400067A RID: 1658
	public static Image imgShuriken;

	// Token: 0x0400067B RID: 1659
	public static Image[][] imgDust;

	// Token: 0x0400067C RID: 1660
	public static bool isResume;

	// Token: 0x0400067D RID: 1661
	public static ServerListScreen serverScreen;

	// Token: 0x0400067E RID: 1662
	public static ServerScr serverScr;

	// Token: 0x0400067F RID: 1663
	public bool resetToLoginScr;

	// Token: 0x04000680 RID: 1664
	public static long TIMEOUT;

	// Token: 0x04000681 RID: 1665
	public static int timeLoading = 15;
}
