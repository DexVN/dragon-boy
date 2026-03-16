using System;
using Assets.src.g;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class GameCanvas : IActionListener
{
	// Token: 0x06000A3C RID: 2620 RVA: 0x0009ABB8 File Offset: 0x00098FB8
	public GameCanvas()
	{
		int num = Rms.loadRMSInt("languageVersion");
		if (num == -1)
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		else if (num != 2)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt("languageVersion", 2);
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		if (GameCanvas.clearOldData != 1)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0009AC74 File Offset: 0x00099074
	public static string getPlatformName()
	{
		return "Pc platform xxx";
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0009AC7C File Offset: 0x0009907C
	public void initGame()
	{
		MotherCanvas.instance.setChildCanvas(this);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.isTouch = true;
		if (GameCanvas.w >= 240)
		{
			GameCanvas.isTouchControl = true;
		}
		if (GameCanvas.w < 320)
		{
			GameCanvas.isTouchControlSmallScreen = true;
		}
		if (GameCanvas.w >= 320)
		{
			GameCanvas.isTouchControlLargeScreen = true;
		}
		GameCanvas.msgdlg = new MsgDlg();
		if (GameCanvas.h <= 160)
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
		if (num != -1)
		{
			if (num > 7)
			{
				Rms.saveRMSInt("clienttype", mSystem.clientType);
			}
			else
			{
				mSystem.clientType = num;
			}
		}
		if (mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty))
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
			GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i + ".png");
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
		if (Panel.WIDTH_PANEL > GameCanvas.w)
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
			GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j + ".png");
			GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j + ".png");
		}
		ServerListScreen.createDeleteRMS();
		GameCanvas.serverScr = new ServerScr();
		GameCanvas.chooseCharScr = new ChooseCharScr();
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0009B061 File Offset: 0x00099461
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0009B068 File Offset: 0x00099468
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0009B074 File Offset: 0x00099474
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0009B088 File Offset: 0x00099488
	public void update()
	{
		if (mSystem.currentTimeMillis() > this.timefps)
		{
			this.timefps += 1000L;
			GameCanvas.max = GameCanvas.fps;
			GameCanvas.fps = 0;
		}
		GameCanvas.fps++;
		if (GameCanvas.messageServer.size() > 0 && GameCanvas.thongBaoTest == null)
		{
			GameCanvas.startserverThongBao((string)GameCanvas.messageServer.elementAt(0));
			GameCanvas.messageServer.removeElementAt(0);
		}
		if (GameCanvas.gameTick % 5 == 0)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			if (global::TouchScreenKeyboard.visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				if (GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5))
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
			if (num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1)
			{
				GameCanvas.timeTickEff1 = num;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			if (num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2)
			{
				GameCanvas.timeTickEff2 = num;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			if (GameCanvas.taskTick > 0)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			if (GameCanvas.gameTick > 10000)
			{
				if (mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameMidlet.instance.exit();
				}
				GameCanvas.gameTick = 0;
			}
			if (GameCanvas.currentScreen != null)
			{
				if (ChatPopup.serverChatPopUp != null)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else if (ChatPopup.currChatPopup != null)
				{
					ChatPopup.currChatPopup.update();
					ChatPopup.currChatPopup.updateKey();
				}
				else if (GameCanvas.currentDialog != null)
				{
					GameCanvas.debug("B", 0);
					GameCanvas.currentDialog.update();
				}
				else if (GameCanvas.menu.showMenu)
				{
					GameCanvas.debug("C", 0);
					GameCanvas.menu.updateMenu();
					GameCanvas.debug("D", 0);
					GameCanvas.menu.updateMenuKey();
				}
				else if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.update();
					if (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H))
					{
						GameCanvas.isFocusPanel2 = false;
					}
					if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
					{
						GameCanvas.panel2.update();
						if (GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
						{
							GameCanvas.isFocusPanel2 = true;
						}
					}
					if (GameCanvas.panel2 != null)
					{
						if (GameCanvas.isFocusPanel2)
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
					if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
					{
						GameCanvas.panel.chatTFUpdateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
					{
						GameCanvas.panel2.chatTFUpdateKey();
					}
					else if ((GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null)
					{
						GameCanvas.panel.updateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
					{
						GameCanvas.panel2.updateKey();
					}
					if (GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine)
					{
						GameCanvas.panel.hide();
					}
				}
				GameCanvas.debug("E", 0);
				if (!GameCanvas.isLoading)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				if (!GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null)
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
			if (this.resetToLoginScr)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.serverScreen);
			}
			GameCanvas.debug("Zzz", 0);
			if (Controller.isConnectOK)
			{
				if (Controller.isMain)
				{
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
					ServerListScreen.testConnect = 2;
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					Rms.saveIP(GameMidlet.IP + ":" + GameMidlet.PORT);
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
			if (Controller.isDisconnected)
			{
				Debug.Log("disconnect");
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession)
					{
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
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
			if (Controller.isConnectionFail)
			{
				Debug.Log("connect fail");
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession)
					{
						ServerListScreen.testConnect = 0;
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onConnectionFail();
					}
				}
				else if (Session_ME.gI().isCompareIPConnect())
				{
					this.onConnectionFail();
				}
				Controller.isConnectionFail = false;
			}
			if (Main.isResume)
			{
				Main.isResume = false;
				if (GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr)
			{
				GameCanvas.xThongBaoTranslate += GameCanvas.dir_ * 2;
				if (GameCanvas.xThongBaoTranslate - Panel.imgNew.getWidth() <= 60)
				{
					GameCanvas.dir_ = 0;
					this.tickWaitThongBao++;
					if (this.tickWaitThongBao > 150)
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

	// Token: 0x06000A43 RID: 2627 RVA: 0x0009B89C File Offset: 0x00099C9C
	public void onDisconnected()
	{
		if (Controller.isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Controller.isLoadingData)
		{
			GameCanvas.instance.resetToLoginScrz();
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
			return;
		}
		if (GameCanvas.currentScreen != GameCanvas.serverScreen)
		{
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
		}
		else
		{
			GameCanvas.endDlg();
		}
		global::Char.isLoadingMap = false;
		if (Controller.isMain)
		{
			ServerListScreen.testConnect = 0;
		}
		GameCanvas.instance.resetToLoginScrz();
		mSystem.endKey();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0009B95C File Offset: 0x00099D5C
	public void onConnectionFail()
	{
		if (GameCanvas.currentScreen.Equals(SplashScr.instance))
		{
			if (ServerListScreen.hasConnected != null)
			{
				ServerListScreen.getServerList(ServerListScreen.linkDefault);
				if (!ServerListScreen.hasConnected[0])
				{
					ServerListScreen.hasConnected[0] = true;
					ServerListScreen.ipSelect = 0;
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					GameCanvas.connect();
				}
				else if (!ServerListScreen.hasConnected[2])
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
			else
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			}
			return;
		}
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		ServerListScreen.isWait = false;
		if (Controller.isLoadingData)
		{
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isConnectionFail = false;
			return;
		}
		GameCanvas.isResume = true;
		LoginScr.isContinueToLogin = false;
		if (GameCanvas.loginScr != null)
		{
			GameCanvas.instance.resetToLoginScrz();
		}
		else
		{
			GameCanvas.loginScr = new LoginScr();
		}
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		if (GameCanvas.currentScreen != GameCanvas.serverScreen)
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
		if (Controller.isMain)
		{
			ServerListScreen.testConnect = 0;
		}
		mSystem.endKey();
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0009BB04 File Offset: 0x00099F04
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0009BB55 File Offset: 0x00099F55
	public static void connect()
	{
		if (!Session_ME.gI().isConnected())
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0009BB7C File Offset: 0x00099F7C
	public static void connect2()
	{
		if (!Session_ME2.gI().isConnected())
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

	// Token: 0x06000A48 RID: 2632 RVA: 0x0009BBE1 File Offset: 0x00099FE1
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0009BC0C File Offset: 0x0009A00C
	public static void resetTransGameScr(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate(0, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-GameScr.cmx, -GameScr.cmy);
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0009BC5C File Offset: 0x0009A05C
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

	// Token: 0x06000A4B RID: 2635 RVA: 0x0009BD6D File Offset: 0x0009A16D
	public void start()
	{
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0009BD6F File Offset: 0x0009A16F
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0009BD77 File Offset: 0x0009A177
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0009BD7F File Offset: 0x0009A17F
	public static void debug(string s, int type)
	{
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0009BD84 File Offset: 0x0009A184
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
			if (GameCanvas.panel.tabIcon != null)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			if (mGraphics.zoomLevel == 1)
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

	// Token: 0x06000A50 RID: 2640 RVA: 0x0009BF5C File Offset: 0x0009A35C
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0009BF5E File Offset: 0x0009A35E
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0009BF60 File Offset: 0x0009A360
	public static void updateBG()
	{
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0009BF64 File Offset: 0x0009A364
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		if (cmy > GameCanvas.h)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY == 0) ? 0 : (cmy >> detalY)), w, h + ((detalY == 0) ? 0 : (cmy >> detalY)));
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0009BFC8 File Offset: 0x0009A3C8
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			if (num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks))
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				if (GameScr.gI().isFireWorks && !GameCanvas.lowGraphic)
				{
					FireWorkEff.paint(g);
				}
			}
			else if (GameCanvas.imgBG != null && GameCanvas.imgBG[num] != null)
			{
				if (GameCanvas.moveX[num] != 0)
				{
					GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
				}
				int cmy = GameScr.cmy;
				if (cmy > GameCanvas.h)
				{
					cmy = GameCanvas.h;
				}
				if (GameCanvas.layerSpeed[num] != 0)
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
				if (color1 != -1)
				{
					if (num == GameCanvas.nBg - 1)
					{
						GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
					}
				}
				if (color2 != -1)
				{
					if (num == 0)
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
					}
				}
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					if (layer == 1 && GameCanvas.typeBg == 11)
					{
						g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
					}
					if (layer == 1 && GameCanvas.typeBg == 13)
					{
						g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
					}
					if (layer == 3 && TileMap.mapID == 1)
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
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0009C444 File Offset: 0x0009A844
	public static void drawSun1(mGraphics g)
	{
		if (GameCanvas.imgSun != null)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		if (GameCanvas.isBoltEff)
		{
			if (GameCanvas.gameTick % 200 == 0)
			{
				GameCanvas.boltActive = true;
			}
			if (GameCanvas.boltActive)
			{
				GameCanvas.tBolt++;
				if (GameCanvas.tBolt == 10)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				if (GameCanvas.tBolt % 2 == 0)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0009C4E8 File Offset: 0x0009A8E8
	public static void drawSun2(mGraphics g)
	{
		if (GameCanvas.imgSun2 != null)
		{
			g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
		}
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0009C50A File Offset: 0x0009A90A
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0009C51C File Offset: 0x0009A91C
	public static void paint_ios_bg(mGraphics g)
	{
		if (mSystem.clientType != 5)
		{
			return;
		}
		if (GameCanvas.imgBgIOS != null)
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
			int num = (TileMap.bgID % 2 != 0) ? 1 : 2;
			GameCanvas.imgBgIOS = mSystem.loadImage("/bg/bg_ios_" + num + ".png");
		}
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0009C5CC File Offset: 0x0009A9CC
	public static void paintBGGameScr(mGraphics g)
	{
		if (!GameCanvas.isLoadBGok)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		if (global::Char.isLoadingMap)
		{
			return;
		}
		int gW = GameScr.gW;
		int gH = GameScr.gH;
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		try
		{
			if (GameCanvas.paintBG)
			{
				if (GameCanvas.currentScreen == GameScr.gI())
				{
					if (TileMap.mapID == 137 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 120 || TileMap.isMapDouble)
					{
						g.setColor(0);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						return;
					}
					if (TileMap.mapID == 138)
					{
						g.setColor(6776679);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						return;
					}
				}
				if (GameCanvas.typeBg == 0)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 1)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, -1);
					GameCanvas.fillRect(g, GameCanvas.colorTop[2], 0, -(GameScr.cmy >> 5), gW, GameCanvas.yb[2], 5);
					GameCanvas.fillRect(g, GameCanvas.colorBotton[2], 0, GameCanvas.yb[2] + GameCanvas.bgH[2] - (GameScr.cmy >> 3), gW, 70, 3);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 2)
				{
					GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
					GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					GameCanvas.paintCloud(g);
				}
				else if (GameCanvas.typeBg == 3)
				{
					int num = GameScr.cmy - (325 - GameScr.gH23);
					g.translate(0, -num);
					GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien && !GameScr.gI().isFireWorks) ? GameCanvas.colorTop[2] : GameScr.gI().mautroi, 0, num - (GameScr.cmy >> 3), gW, GameCanvas.yb[2] - num + (GameScr.cmy >> 3) + 100, 2);
					GameCanvas.paintBackgroundtLayer(g, 3, 2, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 0, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, GameCanvas.colorBotton[0]);
					g.translate(0, -g.getTranslateY());
				}
				else if (GameCanvas.typeBg == 4)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 7, GameCanvas.colorTop[3], -1);
					GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, (!GameCanvas.isHDVersion()) ? GameCanvas.colorTop[1] : GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, GameCanvas.colorTop[1], GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 5)
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
				else if (GameCanvas.typeBg == 6)
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
				else if (GameCanvas.typeBg == 7)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 2, 4, -1, -1);
					GameCanvas.paintBackgroundtLayer(g, 1, 3, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 8)
				{
					GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
					GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
					if (((TileMap.mapID < 92 || TileMap.mapID > 96) && TileMap.mapID != 51 && TileMap.mapID != 52) || GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					}
				}
				else if (GameCanvas.typeBg == 9)
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
				else if (GameCanvas.typeBg == 10)
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
				else if (GameCanvas.typeBg == 11)
				{
					GameCanvas.paintBackgroundtLayer(g, 3, 6, GameCanvas.colorTop[2], GameCanvas.colorBotton[2]);
					GameCanvas.drawSun1(g);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 12)
				{
					g.setColor(9161471);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, 14417919);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, 14417919);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, 14417919);
					GameCanvas.paintCloud(g);
				}
				else if (GameCanvas.typeBg == 13)
				{
					g.setColor(15268088);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 1, 5, -1, 15268088);
				}
				else if (GameCanvas.typeBg == 15)
				{
					g.setColor(2631752);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
				}
				else if (GameCanvas.typeBg == 16)
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
				else if (GameCanvas.typeBg == 19)
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
			else
			{
				g.setColor(2315859);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.tam != null)
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

	// Token: 0x06000A5A RID: 2650 RVA: 0x0009D034 File Offset: 0x0009B434
	public static void resetBg()
	{
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0009D038 File Offset: 0x0009B438
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
				goto IL_685;
			case 1:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
				GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
				goto IL_685;
			case 2:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_685;
			case 3:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
				GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_685;
			case 4:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
				goto IL_685;
			case 5:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_685;
			case 6:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
				goto IL_685;
			case 7:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_685;
			case 8:
				GameCanvas.yb[0] = gH - 103 + 150;
				if (TileMap.mapID == 103)
				{
					GameCanvas.yb[0] -= 100;
				}
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
				goto IL_685;
			case 9:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
				goto IL_685;
			case 10:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				goto IL_685;
			case 11:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
				goto IL_685;
			case 12:
				GameCanvas.yb[0] = gH + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
				goto IL_685;
			case 13:
				GameCanvas.yb[0] = gH - 80;
				GameCanvas.yb[1] = GameCanvas.yb[0];
				goto IL_685;
			case 15:
				GameCanvas.yb[0] = gH - 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
				goto IL_685;
			case 16:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				goto IL_685;
			case 19:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_685;
			}
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
			IL_685:;
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

	// Token: 0x06000A5C RID: 2652 RVA: 0x0009D718 File Offset: 0x0009BB18
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
			if (GameCanvas.typeBg == 12)
			{
				BackgroudEffect.yfog = TileMap.pxh - 100;
			}
			else
			{
				BackgroudEffect.yfog = TileMap.pxh - 160;
			}
			BackgroudEffect.clearImage();
			GameCanvas.randomRaintEff(typeBG);
			if ((TileMap.lastBgID != typeBG || TileMap.lastType != TileMap.bgType) && typeBG != -1)
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
					GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					if (TileMap.bgType == 2)
					{
						GameCanvas.transY = 8;
					}
					goto IL_340;
				case 1:
					GameCanvas.transY = 7;
					GameCanvas.nBg = 4;
					goto IL_340;
				case 2:
				{
					int[] array = new int[5];
					array[2] = 1;
					GameCanvas.moveX = array;
					int[] array2 = new int[5];
					array2[2] = 2;
					GameCanvas.moveXSpeed = array2;
					GameCanvas.nBg = 5;
					goto IL_340;
				}
				case 3:
					GameCanvas.nBg = 3;
					goto IL_340;
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
					goto IL_340;
				}
				case 5:
					GameCanvas.nBg = 4;
					goto IL_340;
				case 6:
				{
					int[] array5 = new int[5];
					array5[0] = 1;
					GameCanvas.moveX = array5;
					int[] array6 = new int[5];
					array6[0] = 2;
					GameCanvas.moveXSpeed = array6;
					GameCanvas.nBg = 5;
					goto IL_340;
				}
				case 7:
					GameCanvas.nBg = 4;
					goto IL_340;
				case 8:
					GameCanvas.transY = 8;
					GameCanvas.nBg = 4;
					goto IL_340;
				case 9:
					BackgroudEffect.addEffect(9);
					GameCanvas.nBg = 4;
					goto IL_340;
				case 10:
					GameCanvas.nBg = 2;
					goto IL_340;
				case 11:
					GameCanvas.transY = 7;
					GameCanvas.layerSpeed[2] = 0;
					GameCanvas.nBg = 3;
					goto IL_340;
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
					goto IL_340;
				}
				case 13:
					GameCanvas.nBg = 2;
					goto IL_340;
				case 15:
					Res.outz("HELL");
					GameCanvas.nBg = 2;
					goto IL_340;
				case 16:
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					goto IL_340;
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
					goto IL_340;
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
				IL_340:
				if (typeBG <= 16)
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
						if (TileMap.bgType != 0)
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
				if (GameCanvas.lowGraphic)
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
					if (TileMap.bgType == 100)
					{
						GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
						GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
						for (int j = 0; j < GameCanvas.nBg; j++)
						{
							if (GameCanvas.imgBG[j] != null)
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
							else if (GameCanvas.nBg > 1)
							{
								GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg + "0.png");
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
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
							if (TileMap.bgType != 0)
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
							if (GameCanvas.imgBG[k] != null)
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
							else if (GameCanvas.nBg > 1)
							{
								GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg + "0.png");
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
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
					if (GameCanvas.typeBg != 0)
					{
						if (GameCanvas.typeBg == 2)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
							GameCanvas.sunX = GameScr.gW / 2 + 50;
							GameCanvas.sunY = GameCanvas.yb[4] - 40;
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts");
						}
						else if (GameCanvas.typeBg == 19)
						{
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_32");
						}
						else if (GameCanvas.typeBg == 4)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
							GameCanvas.sunX = GameScr.gW / 2 + 30;
							GameCanvas.sunY = GameCanvas.yb[3];
						}
						else if (GameCanvas.typeBg == 7)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[3] - 80;
							GameCanvas.sunX2 = GameCanvas.sunX - 100;
							GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
						}
						else if (GameCanvas.typeBg == 6)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[4];
							GameCanvas.sunX2 = GameCanvas.sunX - 100;
							GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
						}
						else if (typeBG == 5)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 50;
							GameCanvas.sunY = GameCanvas.yb[3] + 20;
							GameCanvas.sunX2 = GameScr.gW / 2 + 20;
							GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
						}
						else if (GameCanvas.typeBg == 8 && TileMap.mapID < 90)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 30;
							GameCanvas.sunY = GameCanvas.yb[3] + 60;
							GameCanvas.sunX2 = GameScr.gW / 2 + 20;
							GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
						}
						else if (typeBG == 9)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[4] + 20;
							GameCanvas.sunX2 = GameCanvas.sunX - 80;
							GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
						}
						else if (typeBG == 10)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[1] - 30;
							GameCanvas.sunX2 = GameCanvas.sunX - 80;
							GameCanvas.sunY2 = GameCanvas.yb[1];
						}
						else if (typeBG == 11)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
							GameCanvas.sunX = GameScr.gW / 2 - 30;
							GameCanvas.sunY = GameCanvas.yb[2] - 30;
						}
						else if (typeBG == 12)
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
						else if (typeBG == 16)
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
								if (l == 0 || l == 2 || l == 3 || l == 2 || l == 6)
								{
									num = 160;
								}
								GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num + ".png");
							}
						}
						else if (typeBG == 19)
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
								(TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty,
								".png"
							}));
							GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
							GameCanvas.sunY = GameCanvas.yb[2] - 30;
						}
					}
					GameCanvas.paintBG = false;
					if (!GameCanvas.paintBG)
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

	// Token: 0x06000A5D RID: 2653 RVA: 0x0009E84C File Offset: 0x0009CC4C
	private static void randomRaintEff(int typeBG)
	{
		for (int i = 0; i < GameCanvas.bgRain.Length; i++)
		{
			if (typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0)
			{
				BackgroudEffect.addEffect(0);
				break;
			}
		}
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0009E898 File Offset: 0x0009CC98
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31)
		{
			GameCanvas.keyAsciiPress = keyCode;
		}
		this.mapKeyPress(keyCode);
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0009E904 File Offset: 0x0009CD04
	public void mapKeyPress(int keyCode)
	{
		if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
			return;
		}
		GameCanvas.currentScreen.keyPress(keyCode);
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = true;
			GameCanvas.keyPressed[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[1] = true;
				GameCanvas.keyPressed[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[2] = true;
				GameCanvas.keyPressed[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[3] = true;
				GameCanvas.keyPressed[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[4] = true;
				GameCanvas.keyPressed[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[5] = true;
				GameCanvas.keyPressed[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[6] = true;
				GameCanvas.keyPressed[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = true;
			GameCanvas.keyPressed[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[8] = true;
				GameCanvas.keyPressed[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = true;
			GameCanvas.keyPressed[9] = true;
			return;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = true;
				GameCanvas.keyPressed[14] = true;
				return;
			case 1:
				goto IL_354;
			case 2:
				goto IL_341;
			case 3:
				goto IL_1F9;
			case 4:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[24] = true;
				GameCanvas.keyPressed[24] = true;
				return;
			case 5:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[23] = true;
				GameCanvas.keyPressed[23] = true;
				return;
			case 6:
				goto IL_118;
			case 7:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_118;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_354;
					}
					if (keyCode == -21)
					{
						goto IL_341;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_1F9;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					}
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = true;
					GameCanvas.keyPressed[17] = true;
					return;
				}
				break;
			}
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[21] = true;
			GameCanvas.keyPressed[21] = true;
			return;
			IL_118:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[22] = true;
			GameCanvas.keyPressed[22] = true;
			return;
			IL_1F9:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[25] = true;
			GameCanvas.keyPressed[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_341:
			GameCanvas.keyHold[12] = true;
			GameCanvas.keyPressed[12] = true;
			return;
			IL_354:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
			return;
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0009EE25 File Offset: 0x0009D225
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0009EE34 File Offset: 0x0009D234
	public void mapKeyRelease(int keyCode)
	{
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = false;
			GameCanvas.keyReleased[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyReleased[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[2] = false;
				GameCanvas.keyReleased[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyReleased[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[4] = false;
				GameCanvas.keyReleased[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[5] = false;
				GameCanvas.keyReleased[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[6] = false;
				GameCanvas.keyReleased[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = false;
			GameCanvas.keyReleased[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
			{
				GameCanvas.keyHold[8] = false;
				GameCanvas.keyReleased[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = false;
			GameCanvas.keyReleased[9] = true;
			return;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = false;
				return;
			case 1:
				goto IL_1F1;
			case 2:
				goto IL_1DE;
			case 3:
				goto IL_CE;
			case 4:
				GameCanvas.keyHold[24] = false;
				return;
			case 5:
				GameCanvas.keyHold[23] = false;
				return;
			case 6:
				goto IL_B0;
			case 7:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_B0;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_1F1;
					}
					if (keyCode == -21)
					{
						goto IL_1DE;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = false;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_CE;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = false;
						GameCanvas.keyReleased[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = false;
						GameCanvas.keyReleased[10] = true;
						return;
					}
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = false;
					GameCanvas.keyReleased[17] = true;
					return;
				}
				break;
			}
			GameCanvas.keyHold[21] = false;
			return;
			IL_B0:
			GameCanvas.keyHold[22] = false;
			return;
			IL_CE:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_1DE:
			GameCanvas.keyHold[12] = false;
			GameCanvas.keyReleased[12] = true;
			return;
			IL_1F1:
			GameCanvas.keyHold[13] = false;
			GameCanvas.keyReleased[13] = true;
			return;
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0009F1E0 File Offset: 0x0009D5E0
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0009F1EE File Offset: 0x0009D5EE
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		if (GameCanvas.panel != null && GameCanvas.panel.isShow)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0009F21C File Offset: 0x0009D61C
	public void pointerDragged(int x, int y)
	{
		if (Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10)
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerMove = true;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		if (GameCanvas.curPos > 3)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0009F29C File Offset: 0x0009D69C
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0009F2B8 File Offset: 0x0009D6B8
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

	// Token: 0x06000A67 RID: 2663 RVA: 0x0009F311 File Offset: 0x0009D711
	public void pointerReleased(int x, int y)
	{
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustRelease = true;
		GameCanvas.isPointerMove = false;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0009F338 File Offset: 0x0009D738
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0009F38E File Offset: 0x0009D78E
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0009F3C4 File Offset: 0x0009D7C4
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0009F3F8 File Offset: 0x0009D7F8
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0009F428 File Offset: 0x0009D828
	public static void checkBackButton()
	{
		if (ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0009F480 File Offset: 0x0009D880
	public void paintChangeMap(mGraphics g)
	{
		string empty = string.Empty;
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? empty : (" " + LoginScr.timeLogin + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0009F538 File Offset: 0x0009D938
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			if (GameCanvas.currentScreen != null)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (GameCanvas.panel.isShow)
			{
				GameCanvas.panel.paint(this.g);
				if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
				{
					GameCanvas.panel2.paint(this.g);
				}
				if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else if (GameCanvas.menu.showMenu)
			{
				GameCanvas.debug("PD", 1);
				GameCanvas.menu.paintMenu(this.g);
			}
			GameScr.info1.paint(this.g);
			GameScr.info2.paint(this.g);
			if (GameScr.gI().popUpYesNo != null)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			if (ChatPopup.currChatPopup != null)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			if (ChatPopup.serverChatPopUp != null)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				if (effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp))
				{
					effect.paint(this.g);
				}
			}
			if (global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait)
			{
				this.paintChangeMap(this.g);
				if (GameCanvas.timeLoading > 0 && LoginScr.timeLogin <= 0)
				{
					GameCanvas.startWaitDlg();
					if (mSystem.currentTimeMillis() - GameCanvas.TIMEOUT >= 1000L)
					{
						GameCanvas.timeLoading--;
						Res.outz("[COUNT] == " + GameCanvas.timeLoading);
						if (GameCanvas.timeLoading == 0)
						{
							GameCanvas.timeLoading = 15;
						}
						GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
					}
				}
				if (mSystem.currentTimeMillis() > GameCanvas.timeBreakLoading)
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
					if (GameCanvas.currentScreen != null)
					{
						if (GameCanvas.currentScreen is GameScr)
						{
							GameScr.gI().switchToMe();
						}
						else if (!(GameCanvas.currentScreen is SplashScr))
						{
							if (GameCanvas.currentScreen is LoginScr)
							{
								GameCanvas.gI().resetToLoginScrz();
							}
						}
					}
				}
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			if (GameCanvas.open3Hour && !GameCanvas.isLoading)
			{
				if (GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen || GameCanvas.currentScreen == GameCanvas.serverScr)
				{
					this.g.drawImage(GameCanvas.img12, 5, 5, 0);
				}
				if (GameCanvas.currentScreen == CreateCharScr.instance)
				{
					this.g.drawImage(GameCanvas.img12, 5, 20, 0);
				}
			}
			GameCanvas.resetTrans(this.g);
			int num = GameCanvas.h / 4;
			if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr && GameCanvas.thongBaoTest != null)
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

	// Token: 0x06000A6F RID: 2671 RVA: 0x0009FA50 File Offset: 0x0009DE50
	public static void endDlg()
	{
		if (GameCanvas.inputDlg != null)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0009FA7B File Offset: 0x0009DE7B
	public static void startOKDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0009FAAE File Offset: 0x0009DEAE
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0009FAEC File Offset: 0x0009DEEC
	public static void startOKDlg(string info, bool isError)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0009FB2A File Offset: 0x0009DF2A
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0009FB37 File Offset: 0x0009DF37
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0009FB6A File Offset: 0x0009DF6A
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0009FB9C File Offset: 0x0009DF9C
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0009FBF7 File Offset: 0x0009DFF7
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0009FC16 File Offset: 0x0009E016
	public static void startserverThongBao(string msgSv)
	{
		GameCanvas.thongBaoTest = msgSv;
		GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
		GameCanvas.dir_ = -1;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0009FC34 File Offset: 0x0009E034
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			if (m < 1000)
			{
				text = m + text;
				break;
			}
			int num2 = m % 1000;
			if (num2 == 0)
			{
				text = ".000" + text;
			}
			else if (num2 < 10)
			{
				text = ".00" + num2 + text;
			}
			else if (num2 < 100)
			{
				text = ".0" + num2 + text;
			}
			else
			{
				text = "." + num2 + text;
			}
			m /= 1000;
		}
		return text;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0009FCFE File Offset: 0x0009E0FE
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0009FD09 File Offset: 0x0009E109
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0009FD14 File Offset: 0x0009E114
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0009FD16 File Offset: 0x0009E116
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0009FD1C File Offset: 0x0009E11C
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
				string filename = "x" + mGraphics.zoomLevel + array[array.Length - 1];
				sbyte[] array2 = Rms.loadRMS(filename);
				if (array2 != null)
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

	// Token: 0x06000A7F RID: 2687 RVA: 0x0009FE00 File Offset: 0x0009E200
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

	// Token: 0x06000A80 RID: 2688 RVA: 0x0009FE68 File Offset: 0x0009E268
	public static string cutPng(string str)
	{
		string result = str;
		if (str.Contains(".png"))
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0009FE99 File Offset: 0x0009E299
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0009FEAC File Offset: 0x0009E2AC
	public bool startDust(int dir, int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = (dir != 1) ? 1 : 0;
		if (this.dustState[num] != -1)
		{
			return false;
		}
		this.dustState[num] = 0;
		this.dustX[num] = x;
		this.dustY[num] = y;
		return true;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0009FF00 File Offset: 0x0009E300
	public void loadWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		GameCanvas.imgWS = new Image[3];
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i + ".png");
		}
		GameCanvas.wsX = new int[2];
		GameCanvas.wsY = new int[2];
		GameCanvas.wsState = new int[2];
		GameCanvas.wsF = new int[2];
		GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0009FF94 File Offset: 0x0009E394
	public bool startWaterSplash(int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = (GameCanvas.wsState[0] != -1) ? 1 : 0;
		if (GameCanvas.wsState[num] != -1)
		{
			return false;
		}
		GameCanvas.wsState[num] = 0;
		GameCanvas.wsX[num] = x;
		GameCanvas.wsY[num] = y;
		return true;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0009FFEC File Offset: 0x0009E3EC
	public void updateWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (GameCanvas.wsState[i] != -1)
			{
				GameCanvas.wsY[i]--;
				if (GameCanvas.gameTick % 2 == 0)
				{
					GameCanvas.wsState[i]++;
					if (GameCanvas.wsState[i] > 2)
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

	// Token: 0x06000A86 RID: 2694 RVA: 0x000A0078 File Offset: 0x0009E478
	public void updateDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1)
			{
				this.dustState[i]++;
				if (this.dustState[i] >= 5)
				{
					this.dustState[i] = -1;
				}
				if (i == 0)
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

	// Token: 0x06000A87 RID: 2695 RVA: 0x000A0118 File Offset: 0x0009E518
	public static bool isPaint(int x, int y)
	{
		return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x000A016C File Offset: 0x0009E56C
	public void paintDust(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]))
			{
				g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
			}
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x000A01E8 File Offset: 0x0009E5E8
	public void loadDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (GameCanvas.imgDust == null)
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

	// Token: 0x06000A8A RID: 2698 RVA: 0x000A02D4 File Offset: 0x0009E6D4
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x000A0307 File Offset: 0x0009E707
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x000A0310 File Offset: 0x0009E710
	public static bool isPointer(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x000A0368 File Offset: 0x0009E768
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 88810:
		{
			int playerMapId = (int)p;
			GameCanvas.endDlg();
			Service.gI().acceptInviteTrade(playerMapId);
			break;
		}
		case 88811:
			GameCanvas.endDlg();
			Service.gI().cancelInviteTrade();
			break;
		default:
			switch (idAction)
			{
			case 8881:
			{
				string url = (string)p;
				try
				{
					GameMidlet.instance.platformRequest(url);
				}
				catch (Exception ex)
				{
				}
				GameCanvas.currentDialog = null;
				break;
			}
			case 8882:
				InfoDlg.hide();
				GameCanvas.currentDialog = null;
				ServerListScreen.isAutoConect = false;
				ServerListScreen.countDieConnect = 0;
				break;
			default:
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
					if (GameCanvas.loginScr == null)
					{
						GameCanvas.loginScr = new LoginScr();
					}
					GameCanvas.loginScr.doLogin();
					Main.closeKeyBoard();
					break;
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
					string text = (string)p;
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
						GameCanvas.endDlg();
						if (ServerListScreen.loadScreen)
						{
							GameCanvas.serverScreen.switchToMe();
						}
						else
						{
							GameCanvas.serverScreen.show2();
						}
						break;
					default:
						if (idAction != 999)
						{
							if (idAction != 9000)
							{
								if (idAction != 9999)
								{
									if (idAction == 888361)
									{
										string text2 = GameCanvas.inputDlg.tfInput.getText();
										GameCanvas.endDlg();
										if (text2.Length < 6 || text2.Equals(string.Empty))
										{
											GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
										}
										else
										{
											try
											{
												Service.gI().activeAccProtect(int.Parse(text2));
											}
											catch (Exception ex2)
											{
												GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
												Cout.println("Loi tai 888361 Gamescavas " + ex2.ToString());
											}
										}
									}
								}
								else
								{
									GameCanvas.endDlg();
									GameCanvas.connect();
									Service.gI().setClientType();
									if (GameCanvas.loginScr == null)
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
								if (GameCanvas.currentScreen != GameCanvas.loginScr)
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
					break;
				}
				break;
			case 8884:
				GameCanvas.endDlg();
				GameCanvas.loginScr.switchToMe();
				break;
			case 8885:
				GameMidlet.instance.exit();
				break;
			case 8886:
			{
				GameCanvas.endDlg();
				string name = (string)p;
				Service.gI().addFriend(name);
				break;
			}
			case 8887:
			{
				GameCanvas.endDlg();
				int charId = (int)p;
				Service.gI().addPartyAccept(charId);
				break;
			}
			case 8888:
			{
				int charId2 = (int)p;
				Service.gI().addPartyCancel(charId2);
				GameCanvas.endDlg();
				break;
			}
			case 8889:
			{
				string str = (string)p;
				GameCanvas.endDlg();
				Service.gI().acceptPleaseParty(str);
				break;
			}
			}
			break;
		case 88814:
		{
			Item[] items = (Item[])p;
			GameCanvas.endDlg();
			Service.gI().crystalCollectLock(items);
			break;
		}
		case 88815:
			break;
		case 88817:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			break;
		case 88818:
		{
			short menuId = (short)p;
			Service.gI().textBoxId(menuId, GameCanvas.inputDlg.tfInput.getText());
			GameCanvas.endDlg();
			break;
		}
		case 88819:
		{
			short menuId2 = (short)p;
			Service.gI().menuId(menuId2);
			break;
		}
		case 88820:
		{
			string[] array = (string[])p;
			if (global::Char.myCharz().npcFocus == null)
			{
				return;
			}
			int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
			if (array.Length > 1)
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
			break;
		}
		case 88821:
		{
			int menuId3 = (int)p;
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuId3, GameCanvas.menu.menuSelectedItem);
			break;
		}
		case 88822:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			break;
		case 88823:
			GameCanvas.startOKDlg(mResources.SENTMSG);
			break;
		case 88824:
			GameCanvas.startOKDlg(mResources.NOSENDMSG);
			break;
		case 88825:
			GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
			break;
		case 88826:
			GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
			break;
		case 88827:
			GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
			break;
		case 88828:
			GameCanvas.startOKDlg(mResources.sendMsgFail);
			break;
		case 88829:
		{
			string text3 = GameCanvas.inputDlg.tfInput.getText();
			if (text3.Equals(string.Empty))
			{
				return;
			}
			Service.gI().changeName(text3, (int)p);
			InfoDlg.showWait();
			break;
		}
		case 88836:
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
			GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
			break;
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
			break;
		}
		case 88839:
		{
			string text5 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			if (text5.Length < 6 || text5.Equals(string.Empty))
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
			break;
		}
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x000A0B74 File Offset: 0x0009EF74
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x000A0BA5 File Offset: 0x0009EFA5
	public static void backToRegister()
	{
	}

	// Token: 0x04001323 RID: 4899
	public static long timeNow = 0L;

	// Token: 0x04001324 RID: 4900
	public static bool open3Hour;

	// Token: 0x04001325 RID: 4901
	public static bool lowGraphic = false;

	// Token: 0x04001326 RID: 4902
	public static bool serverchat = false;

	// Token: 0x04001327 RID: 4903
	public static bool isMoveNumberPad = true;

	// Token: 0x04001328 RID: 4904
	public static bool isLoading;

	// Token: 0x04001329 RID: 4905
	public static bool isTouch = false;

	// Token: 0x0400132A RID: 4906
	public static bool isTouchControl;

	// Token: 0x0400132B RID: 4907
	public static bool isTouchControlSmallScreen;

	// Token: 0x0400132C RID: 4908
	public static bool isTouchControlLargeScreen;

	// Token: 0x0400132D RID: 4909
	public static bool isConnectFail;

	// Token: 0x0400132E RID: 4910
	public static GameCanvas instance;

	// Token: 0x0400132F RID: 4911
	public static bool bRun;

	// Token: 0x04001330 RID: 4912
	public static bool[] keyPressed = new bool[30];

	// Token: 0x04001331 RID: 4913
	public static bool[] keyReleased = new bool[30];

	// Token: 0x04001332 RID: 4914
	public static bool[] keyHold = new bool[30];

	// Token: 0x04001333 RID: 4915
	public static bool isPointerDown;

	// Token: 0x04001334 RID: 4916
	public static bool isPointerClick;

	// Token: 0x04001335 RID: 4917
	public static bool isPointerJustRelease;

	// Token: 0x04001336 RID: 4918
	public static bool isPointerMove;

	// Token: 0x04001337 RID: 4919
	public static int px;

	// Token: 0x04001338 RID: 4920
	public static int py;

	// Token: 0x04001339 RID: 4921
	public static int pxFirst;

	// Token: 0x0400133A RID: 4922
	public static int pyFirst;

	// Token: 0x0400133B RID: 4923
	public static int pxLast;

	// Token: 0x0400133C RID: 4924
	public static int pyLast;

	// Token: 0x0400133D RID: 4925
	public static int pxMouse;

	// Token: 0x0400133E RID: 4926
	public static int pyMouse;

	// Token: 0x0400133F RID: 4927
	public static Position[] arrPos = new Position[4];

	// Token: 0x04001340 RID: 4928
	public static int gameTick;

	// Token: 0x04001341 RID: 4929
	public static int taskTick;

	// Token: 0x04001342 RID: 4930
	public static bool isEff1;

	// Token: 0x04001343 RID: 4931
	public static bool isEff2;

	// Token: 0x04001344 RID: 4932
	public static long timeTickEff1;

	// Token: 0x04001345 RID: 4933
	public static long timeTickEff2;

	// Token: 0x04001346 RID: 4934
	public static int w;

	// Token: 0x04001347 RID: 4935
	public static int h;

	// Token: 0x04001348 RID: 4936
	public static int hw;

	// Token: 0x04001349 RID: 4937
	public static int hh;

	// Token: 0x0400134A RID: 4938
	public static int wd3;

	// Token: 0x0400134B RID: 4939
	public static int hd3;

	// Token: 0x0400134C RID: 4940
	public static int w2d3;

	// Token: 0x0400134D RID: 4941
	public static int h2d3;

	// Token: 0x0400134E RID: 4942
	public static int w3d4;

	// Token: 0x0400134F RID: 4943
	public static int h3d4;

	// Token: 0x04001350 RID: 4944
	public static int wd6;

	// Token: 0x04001351 RID: 4945
	public static int hd6;

	// Token: 0x04001352 RID: 4946
	public static mScreen currentScreen;

	// Token: 0x04001353 RID: 4947
	public static Menu menu = new Menu();

	// Token: 0x04001354 RID: 4948
	public static Panel panel;

	// Token: 0x04001355 RID: 4949
	public static Panel panel2;

	// Token: 0x04001356 RID: 4950
	public static ChooseCharScr chooseCharScr;

	// Token: 0x04001357 RID: 4951
	public static LoginScr loginScr;

	// Token: 0x04001358 RID: 4952
	public static RegisterScreen registerScr;

	// Token: 0x04001359 RID: 4953
	public static Dialog currentDialog;

	// Token: 0x0400135A RID: 4954
	public static MsgDlg msgdlg;

	// Token: 0x0400135B RID: 4955
	public static InputDlg inputDlg;

	// Token: 0x0400135C RID: 4956
	public static MyVector currentPopup = new MyVector();

	// Token: 0x0400135D RID: 4957
	public static int requestLoseCount;

	// Token: 0x0400135E RID: 4958
	public static MyVector listPoint;

	// Token: 0x0400135F RID: 4959
	public static Paint paintz;

	// Token: 0x04001360 RID: 4960
	public static bool isGetResFromServer;

	// Token: 0x04001361 RID: 4961
	public static Image[] imgBG;

	// Token: 0x04001362 RID: 4962
	public static int skyColor;

	// Token: 0x04001363 RID: 4963
	public static int curPos = 0;

	// Token: 0x04001364 RID: 4964
	public static int[] bgW;

	// Token: 0x04001365 RID: 4965
	public static int[] bgH;

	// Token: 0x04001366 RID: 4966
	public static int planet = 0;

	// Token: 0x04001367 RID: 4967
	private mGraphics g = new mGraphics();

	// Token: 0x04001368 RID: 4968
	public static Image img12;

	// Token: 0x04001369 RID: 4969
	public static Image[] imgBlue = new Image[7];

	// Token: 0x0400136A RID: 4970
	public static Image[] imgViolet = new Image[7];

	// Token: 0x0400136B RID: 4971
	public static MyHashTable danhHieu = new MyHashTable();

	// Token: 0x0400136C RID: 4972
	public static MyVector messageServer = new MyVector(string.Empty);

	// Token: 0x0400136D RID: 4973
	public static bool isPlaySound = true;

	// Token: 0x0400136E RID: 4974
	private static int clearOldData;

	// Token: 0x0400136F RID: 4975
	public static int timeOpenKeyBoard;

	// Token: 0x04001370 RID: 4976
	public static bool isFocusPanel2;

	// Token: 0x04001371 RID: 4977
	public static int fps = 0;

	// Token: 0x04001372 RID: 4978
	public static int max;

	// Token: 0x04001373 RID: 4979
	public static int up;

	// Token: 0x04001374 RID: 4980
	public static int upmax;

	// Token: 0x04001375 RID: 4981
	private long timefps = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x04001376 RID: 4982
	private long timeup = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x04001377 RID: 4983
	private static int dir_ = -1;

	// Token: 0x04001378 RID: 4984
	private int tickWaitThongBao;

	// Token: 0x04001379 RID: 4985
	public bool isPaintCarret;

	// Token: 0x0400137A RID: 4986
	public static MyVector debugUpdate;

	// Token: 0x0400137B RID: 4987
	public static MyVector debugPaint;

	// Token: 0x0400137C RID: 4988
	public static MyVector debugSession;

	// Token: 0x0400137D RID: 4989
	private static bool isShowErrorForm = false;

	// Token: 0x0400137E RID: 4990
	public static bool paintBG;

	// Token: 0x0400137F RID: 4991
	public static int gsskyHeight;

	// Token: 0x04001380 RID: 4992
	public static int gsgreenField1Y;

	// Token: 0x04001381 RID: 4993
	public static int gsgreenField2Y;

	// Token: 0x04001382 RID: 4994
	public static int gshouseY;

	// Token: 0x04001383 RID: 4995
	public static int gsmountainY;

	// Token: 0x04001384 RID: 4996
	public static int bgLayer0y;

	// Token: 0x04001385 RID: 4997
	public static int bgLayer1y;

	// Token: 0x04001386 RID: 4998
	public static Image imgCloud;

	// Token: 0x04001387 RID: 4999
	public static Image imgSun;

	// Token: 0x04001388 RID: 5000
	public static Image imgSun2;

	// Token: 0x04001389 RID: 5001
	public static Image imgClear;

	// Token: 0x0400138A RID: 5002
	public static Image[] imgBorder = new Image[3];

	// Token: 0x0400138B RID: 5003
	public static Image[] imgSunSpec = new Image[3];

	// Token: 0x0400138C RID: 5004
	public static int borderConnerW;

	// Token: 0x0400138D RID: 5005
	public static int borderConnerH;

	// Token: 0x0400138E RID: 5006
	public static int borderCenterW;

	// Token: 0x0400138F RID: 5007
	public static int borderCenterH;

	// Token: 0x04001390 RID: 5008
	public static int[] cloudX;

	// Token: 0x04001391 RID: 5009
	public static int[] cloudY;

	// Token: 0x04001392 RID: 5010
	public static int sunX;

	// Token: 0x04001393 RID: 5011
	public static int sunY;

	// Token: 0x04001394 RID: 5012
	public static int sunX2;

	// Token: 0x04001395 RID: 5013
	public static int sunY2;

	// Token: 0x04001396 RID: 5014
	public static int[] layerSpeed;

	// Token: 0x04001397 RID: 5015
	public static int[] moveX;

	// Token: 0x04001398 RID: 5016
	public static int[] moveXSpeed;

	// Token: 0x04001399 RID: 5017
	public static bool isBoltEff;

	// Token: 0x0400139A RID: 5018
	public static bool boltActive;

	// Token: 0x0400139B RID: 5019
	public static int tBolt;

	// Token: 0x0400139C RID: 5020
	public static Image imgBgIOS;

	// Token: 0x0400139D RID: 5021
	public static int typeBg = -1;

	// Token: 0x0400139E RID: 5022
	public static int transY;

	// Token: 0x0400139F RID: 5023
	public static int[] yb = new int[5];

	// Token: 0x040013A0 RID: 5024
	public static int[] colorTop;

	// Token: 0x040013A1 RID: 5025
	public static int[] colorBotton;

	// Token: 0x040013A2 RID: 5026
	public static int yb1;

	// Token: 0x040013A3 RID: 5027
	public static int yb2;

	// Token: 0x040013A4 RID: 5028
	public static int yb3;

	// Token: 0x040013A5 RID: 5029
	public static int nBg = 0;

	// Token: 0x040013A6 RID: 5030
	public static int lastBg = -1;

	// Token: 0x040013A7 RID: 5031
	public static int[] bgRain = new int[]
	{
		1,
		4,
		11
	};

	// Token: 0x040013A8 RID: 5032
	public static int[] bgRainFont = new int[]
	{
		-1
	};

	// Token: 0x040013A9 RID: 5033
	public static Image imgCaycot;

	// Token: 0x040013AA RID: 5034
	public static Image tam;

	// Token: 0x040013AB RID: 5035
	public static int typeBackGround = -1;

	// Token: 0x040013AC RID: 5036
	public static int saveIDBg = -10;

	// Token: 0x040013AD RID: 5037
	public static bool isLoadBGok;

	// Token: 0x040013AE RID: 5038
	private static long lastTimePress = 0L;

	// Token: 0x040013AF RID: 5039
	public static int keyAsciiPress;

	// Token: 0x040013B0 RID: 5040
	public static int pXYScrollMouse;

	// Token: 0x040013B1 RID: 5041
	private static Image imgSignal;

	// Token: 0x040013B2 RID: 5042
	public static MyVector flyTexts = new MyVector();

	// Token: 0x040013B3 RID: 5043
	public int longTime;

	// Token: 0x040013B4 RID: 5044
	public static long timeBreakLoading;

	// Token: 0x040013B5 RID: 5045
	private static string thongBaoTest;

	// Token: 0x040013B6 RID: 5046
	public static int xThongBaoTranslate = GameCanvas.w - 60;

	// Token: 0x040013B7 RID: 5047
	public static bool isPointerJustDown = false;

	// Token: 0x040013B8 RID: 5048
	private int count = 1;

	// Token: 0x040013B9 RID: 5049
	public static bool csWait;

	// Token: 0x040013BA RID: 5050
	public static MyRandom r = new MyRandom();

	// Token: 0x040013BB RID: 5051
	public static bool isBlackScreen;

	// Token: 0x040013BC RID: 5052
	public static int[] bgSpeed;

	// Token: 0x040013BD RID: 5053
	public static int cmdBarX;

	// Token: 0x040013BE RID: 5054
	public static int cmdBarY;

	// Token: 0x040013BF RID: 5055
	public static int cmdBarW;

	// Token: 0x040013C0 RID: 5056
	public static int cmdBarH;

	// Token: 0x040013C1 RID: 5057
	public static int cmdBarLeftW;

	// Token: 0x040013C2 RID: 5058
	public static int cmdBarRightW;

	// Token: 0x040013C3 RID: 5059
	public static int cmdBarCenterW;

	// Token: 0x040013C4 RID: 5060
	public static int hpBarX;

	// Token: 0x040013C5 RID: 5061
	public static int hpBarY;

	// Token: 0x040013C6 RID: 5062
	public static int hpBarW;

	// Token: 0x040013C7 RID: 5063
	public static int expBarW;

	// Token: 0x040013C8 RID: 5064
	public static int lvPosX;

	// Token: 0x040013C9 RID: 5065
	public static int moneyPosX;

	// Token: 0x040013CA RID: 5066
	public static int hpBarH;

	// Token: 0x040013CB RID: 5067
	public static int girlHPBarY;

	// Token: 0x040013CC RID: 5068
	public int timeOut;

	// Token: 0x040013CD RID: 5069
	public int[] dustX;

	// Token: 0x040013CE RID: 5070
	public int[] dustY;

	// Token: 0x040013CF RID: 5071
	public int[] dustState;

	// Token: 0x040013D0 RID: 5072
	public static int[] wsX;

	// Token: 0x040013D1 RID: 5073
	public static int[] wsY;

	// Token: 0x040013D2 RID: 5074
	public static int[] wsState;

	// Token: 0x040013D3 RID: 5075
	public static int[] wsF;

	// Token: 0x040013D4 RID: 5076
	public static Image[] imgWS;

	// Token: 0x040013D5 RID: 5077
	public static Image imgShuriken;

	// Token: 0x040013D6 RID: 5078
	public static Image[][] imgDust;

	// Token: 0x040013D7 RID: 5079
	public static bool isResume;

	// Token: 0x040013D8 RID: 5080
	public static ServerListScreen serverScreen;

	// Token: 0x040013D9 RID: 5081
	public static ServerScr serverScr;

	// Token: 0x040013DA RID: 5082
	public bool resetToLoginScr;

	// Token: 0x040013DB RID: 5083
	public static long TIMEOUT;

	// Token: 0x040013DC RID: 5084
	public static int timeLoading = 15;
}
