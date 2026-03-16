using System;

// Token: 0x020000C4 RID: 196
public class ServerListScreen : mScreen, IActionListener
{
	// Token: 0x060009DA RID: 2522 RVA: 0x00095BF8 File Offset: 0x00093FF8
	public ServerListScreen()
	{
		int num = 4;
		int num2 = num * 32 + 23 + 33;
		if (num2 >= GameCanvas.w)
		{
			num--;
			num2 = num * 32 + 23 + 33;
		}
		this.initCommand();
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		if (this.cmdCallHotline == null)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				this.cmdCallHotline.y = GameCanvas.h - 20;
			}
			else
			{
				int num3 = 2;
				this.cmdCallHotline.y = num3 + 6;
			}
		}
		ServerListScreen.cmdUpdateServer = new Command();
		ServerListScreen.cmdUpdateServer.actionChat = delegate(string str)
		{
			string text = str;
			string text2 = str;
			if (text == null)
			{
				text = ServerListScreen.linkDefault;
				return;
			}
			if (text == null && text2 != null)
			{
				if (text2.Equals(string.Empty) || text2.Length < 20)
				{
					text2 = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text2);
			}
			if (text != null && text2 == null)
			{
				if (text.Equals(string.Empty) || text.Length < 20)
				{
					text = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text);
			}
			if (text != null && text2 != null)
			{
				if (text.Length > text2.Length)
				{
					ServerListScreen.getServerList(text);
				}
				else
				{
					ServerListScreen.getServerList(text2);
				}
			}
		};
		this.setLinkDefault(mSystem.LANGUAGE);
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00095D1C File Offset: 0x0009411C
	public static void createDeleteRMS()
	{
		if (ServerListScreen.cmdDeleteRMS == null)
		{
			if (GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			ServerListScreen.cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			ServerListScreen.cmdDeleteRMS.x = GameCanvas.w - 78;
			ServerListScreen.cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x00095D84 File Offset: 0x00094184
	private void initCommand()
	{
		this.nCmdPlay = 0;
		string text = Rms.loadRMSString("acc");
		if (text == null)
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else if (text.Equals(string.Empty))
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else
		{
			this.nCmdPlay = 1;
		}
		this.cmd = new Command[(mGraphics.zoomLevel <= 1) ? (4 + this.nCmdPlay) : (3 + this.nCmdPlay)];
		int num = GameCanvas.hh - 15 * this.cmd.Length + 28;
		for (int i = 0; i < this.cmd.Length; i++)
		{
			switch (i)
			{
			case 0:
				this.cmd[0] = new Command(string.Empty, this, 3, null);
				if (text == null)
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else if (text.Equals(string.Empty))
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else
				{
					this.cmd[0].caption = mResources.playAcc + ": " + text;
					if (this.cmd[0].caption.Length > 23)
					{
						this.cmd[0].caption = this.cmd[0].caption.Substring(0, 23);
						Command command = this.cmd[0];
						command.caption += "...";
					}
				}
				break;
			case 1:
				if (this.nCmdPlay == 1)
				{
					this.cmd[1] = new Command(string.Empty, this, 10100, null);
					this.cmd[1].caption = mResources.playNew;
				}
				else
				{
					this.cmd[1] = new Command(mResources.change_account, this, 7, null);
				}
				break;
			case 2:
				if (this.nCmdPlay == 1)
				{
					this.cmd[2] = new Command(mResources.change_account, this, 7, null);
				}
				else
				{
					this.cmd[2] = new Command(string.Empty, this, 17, null);
				}
				break;
			case 3:
				if (this.nCmdPlay == 1)
				{
					this.cmd[3] = new Command(string.Empty, this, 17, null);
				}
				else
				{
					this.cmd[3] = new Command(mResources.option, this, 8, null);
				}
				break;
			case 4:
				this.cmd[4] = new Command(mResources.option, this, 8, null);
				break;
			}
			this.cmd[i].y = num;
			this.cmd[i].setType();
			this.cmd[i].x = (GameCanvas.w - this.cmd[i].w) / 2;
			num += 30;
		}
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000960F3 File Offset: 0x000944F3
	public static void doUpdateServer()
	{
		if (ServerListScreen.cmdUpdateServer == null && GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		Net.connectHTTP2(ServerListScreen.linkDefault, ServerListScreen.cmdUpdateServer);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x00096124 File Offset: 0x00094524
	public static void getServerList(string str)
	{
		ServerListScreen.lengthServer = new int[3];
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz("tem leng= " + array.Length);
		mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
		ServerListScreen.nameServer = new string[array.Length - 2];
		ServerListScreen.address = new string[array.Length - 2];
		ServerListScreen.port = new short[array.Length - 2];
		ServerListScreen.language = new sbyte[array.Length - 2];
		ServerListScreen.hasConnected = new bool[2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string[] array2 = Res.split(array[i].Trim(), ":", 0);
			ServerListScreen.nameServer[i] = array2[0];
			ServerListScreen.address[i] = array2[1];
			ServerListScreen.port[i] = short.Parse(array2[2]);
			ServerListScreen.language[i] = sbyte.Parse(array2[3].Trim());
			ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
		}
		ServerListScreen.serverPriority = sbyte.Parse(array[array.Length - 1]);
		ServerListScreen.saveIP();
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0009624C File Offset: 0x0009464C
	public override void paint(mGraphics g)
	{
		if (!ServerListScreen.loadScreen)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			if (!ServerListScreen.bigOk)
			{
			}
		}
		else
		{
			GameCanvas.paintBGGameScr(g);
		}
		int num = 2;
		mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
		{
			"v",
			GameMidlet.VERSION,
			"(",
			mGraphics.zoomLevel,
			")"
		}), GameCanvas.w - 2, num + 15, 1, mFont.tahoma_7_grey);
		string text = string.Empty;
		if (ServerListScreen.testConnect == 0)
		{
			text = text + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " disconnect";
		}
		else
		{
			text = text + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " connected";
		}
		if (mSystem.isTest)
		{
			mFont.tahoma_7_white.drawString(g, text, GameCanvas.w - 2, num + 15 + 15, 1, mFont.tahoma_7_grey);
		}
		if (!ServerListScreen.isGetData || ServerListScreen.loadScreen)
		{
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
			}
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
		}
		int num2 = (GameCanvas.w < 200) ? 160 : 180;
		if (ServerListScreen.cmdDeleteRMS != null)
		{
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		if (GameCanvas.currentDialog == null)
		{
			if (!ServerListScreen.loadScreen)
			{
				if (!ServerListScreen.bigOk)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
					if (!ServerListScreen.isGetData)
					{
						mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
						if (ServerListScreen.cmdDownload != null)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
					}
					else
					{
						if (ServerListScreen.cmdDownload != null)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
						mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + ServerListScreen.percent + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
						GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, 100f, g);
						GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, (float)ServerListScreen.percent, g);
					}
				}
			}
			else
			{
				int num3 = GameCanvas.hh - 15 * this.cmd.Length - 15;
				if (num3 < 25)
				{
					num3 = 25;
				}
				if (LoginScr.imgTitle != null)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num3, 3);
				}
				for (int i = 0; i < this.cmd.Length; i++)
				{
					this.cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (ServerListScreen.testConnect == -1)
				{
					if (GameCanvas.gameTick % 20 > 10)
					{
						g.drawRegion(GameScr.imgRoomStat, 0, 14, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 10, 0);
					}
				}
				else
				{
					g.drawRegion(GameScr.imgRoomStat, 0, ServerListScreen.testConnect * 7, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 9, 0);
				}
			}
		}
		base.paint(g);
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x000966B0 File Offset: 0x00094AB0
	public void selectServer()
	{
		ServerListScreen.flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		Session_ME.gI().close();
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		GameMidlet.LANGUAGE = (int)ServerListScreen.language[ServerListScreen.ipSelect];
		Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
		if ((int)ServerListScreen.language[ServerListScreen.ipSelect] != (int)mResources.language)
		{
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
		}
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.initCommand();
		ServerListScreen.loadScreen = true;
		ServerListScreen.countDieConnect = 0;
		ServerListScreen.testConnect = -1;
		ServerListScreen.isAutoConect = true;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00096770 File Offset: 0x00094B70
	public override void update()
	{
		if (ServerListScreen.waitToLogin)
		{
			ServerListScreen.tWaitToLogin++;
			if (ServerListScreen.tWaitToLogin == 50)
			{
				GameCanvas.serverScreen.selectServer();
			}
			if (ServerListScreen.tWaitToLogin == 100)
			{
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				ServerListScreen.waitToLogin = false;
			}
		}
		if (ServerListScreen.flagServer > 0)
		{
			ServerListScreen.flagServer--;
			if (ServerListScreen.flagServer == 0)
			{
				GameCanvas.endDlg();
			}
			if (ServerListScreen.testConnect == 2)
			{
				ServerListScreen.flagServer = 0;
				GameCanvas.endDlg();
			}
		}
		if (ServerListScreen.flagServer <= 0 && ServerListScreen.isAutoConect)
		{
			ServerListScreen.countDieConnect++;
			if (ServerListScreen.countDieConnect > 100000)
			{
				ServerListScreen.countDieConnect = 0;
			}
		}
		for (int i = 0; i < this.cmd.Length; i++)
		{
			if (i == ServerListScreen.selected)
			{
				this.cmd[i].isFocus = true;
			}
			else
			{
				this.cmd[i].isFocus = false;
			}
		}
		GameScr.cmx++;
		if (!ServerListScreen.loadScreen && (ServerListScreen.bigOk || ServerListScreen.percent == 100))
		{
			ServerListScreen.cmdDownload = null;
		}
		base.update();
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (!ServerListScreen.loadScreen)
		{
			return;
		}
		if (!ServerListScreen.isAutoConect)
		{
			return;
		}
		if (GameCanvas.currentScreen != this)
		{
			return;
		}
		if (ServerListScreen.testConnect != 2)
		{
			if (ServerListScreen.countDieConnect < ((mSystem.clientType != 1) ? 5 : 2))
			{
				if (ServerListScreen.flagServer <= 0)
				{
					ServerListScreen.flagServer = 30;
					GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
					GameCanvas.connect();
				}
			}
			else if (!Session_ME.gI().isConnected())
			{
				if (ServerListScreen.flagServer <= 0)
				{
					Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 18, null);
					Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 19, null);
					GameCanvas.startYesNoDlg(mResources.maychutathoacmatsong + "." + mResources.confirmChangeServer, cmdYes, cmdNo);
					ServerListScreen.flagServer = 30;
				}
			}
			else if (ServerListScreen.flagServer <= 0)
			{
				ServerListScreen.countDieConnect = 0;
			}
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x000969C4 File Offset: 0x00094DC4
	private void processInput()
	{
		if (ServerListScreen.loadScreen)
		{
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		}
		else
		{
			this.center = ServerListScreen.cmdDownload;
		}
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00096A03 File Offset: 0x00094E03
	public static void updateDeleteData()
	{
		if (ServerListScreen.cmdDeleteRMS != null && ServerListScreen.cmdDeleteRMS.isPointerPressInside())
		{
			ServerListScreen.cmdDeleteRMS.performAction();
		}
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00096A28 File Offset: 0x00094E28
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			ServerListScreen.updateDeleteData();
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
			if (!ServerListScreen.loadScreen)
			{
				if (ServerListScreen.cmdDownload != null && ServerListScreen.cmdDownload.isPointerPressInside())
				{
					ServerListScreen.cmdDownload.performAction();
				}
				base.updateKey();
				return;
			}
			for (int i = 0; i < this.cmd.Length; i++)
			{
				if (this.cmd[i] != null && this.cmd[i].isPointerPressInside())
				{
					if (ServerListScreen.testConnect == -1 || ServerListScreen.testConnect == 0)
					{
						if (this.cmd[i].caption.IndexOf(mResources.server) != -1)
						{
							this.cmd[i].performAction();
						}
					}
					else
					{
						this.cmd[i].performAction();
					}
				}
			}
		}
		else if (ServerListScreen.loadScreen)
		{
			if (GameCanvas.keyPressed[8])
			{
				int num = (mGraphics.zoomLevel <= 1) ? 4 : 2;
				GameCanvas.keyPressed[8] = false;
				ServerListScreen.selected++;
				if (ServerListScreen.selected > num)
				{
					ServerListScreen.selected = 0;
				}
				this.processInput();
			}
			if (GameCanvas.keyPressed[2])
			{
				int num2 = (mGraphics.zoomLevel <= 1) ? 4 : 2;
				GameCanvas.keyPressed[2] = false;
				ServerListScreen.selected--;
				if (ServerListScreen.selected < 0)
				{
					ServerListScreen.selected = num2;
				}
				this.processInput();
			}
		}
		if (ServerListScreen.isWait)
		{
			return;
		}
		base.updateKey();
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00096BDC File Offset: 0x00094FDC
	public static void saveIP()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(mResources.language);
			dataOutputStream.writeByte((sbyte)ServerListScreen.nameServer.Length);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				dataOutputStream.writeUTF(ServerListScreen.nameServer[i]);
				dataOutputStream.writeUTF(ServerListScreen.address[i]);
				dataOutputStream.writeShort(ServerListScreen.port[i]);
				dataOutputStream.writeByte(ServerListScreen.language[i]);
			}
			ServerListScreen.serverPriority = (sbyte)((!mSystem.isTest) ? ((int)ServerListScreen.serverPriority) : ((int)ServerListScreen.serverPriority + 5));
			dataOutputStream.writeByte(ServerListScreen.serverPriority);
			Rms.saveRMS("NRlink2", dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00096CB8 File Offset: 0x000950B8
	public static bool allServerConnected()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!ServerListScreen.hasConnected[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00096CE8 File Offset: 0x000950E8
	public static void loadIP()
	{
		sbyte[] array = Rms.loadRMS("NRlink2");
		if (array == null)
		{
			ServerListScreen.getServerList(ServerListScreen.linkDefault);
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		if (dataInputStream == null)
		{
			return;
		}
		try
		{
			ServerListScreen.lengthServer = new int[3];
			mResources.loadLanguague(dataInputStream.readByte());
			sbyte b = dataInputStream.readByte();
			ServerListScreen.nameServer = new string[(int)b];
			ServerListScreen.address = new string[(int)b];
			ServerListScreen.port = new short[(int)b];
			ServerListScreen.language = new sbyte[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				ServerListScreen.nameServer[i] = dataInputStream.readUTF();
				ServerListScreen.address[i] = dataInputStream.readUTF();
				ServerListScreen.port[i] = dataInputStream.readShort();
				ServerListScreen.language[i] = dataInputStream.readByte();
				ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
			}
			ServerListScreen.serverPriority = dataInputStream.readByte();
			dataInputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x00096DFC File Offset: 0x000951FC
	public static string[] loadIP_2()
	{
		string[] array = null;
		sbyte[] array2 = Rms.loadRMS("NRlink2");
		if (array2 == null)
		{
			return null;
		}
		DataInputStream dataInputStream = new DataInputStream(array2);
		if (dataInputStream == null)
		{
			return null;
		}
		try
		{
			ServerListScreen.lengthServer = new int[3];
			dataInputStream.readByte();
			sbyte b = dataInputStream.readByte();
			ServerListScreen.nameServer = new string[(int)b];
			ServerListScreen.address = new string[(int)b];
			ServerListScreen.port = new short[(int)b];
			ServerListScreen.language = new sbyte[(int)b];
			array = new string[(int)b];
			Res.outz("len sv == " + b);
			for (int i = 0; i < (int)b; i++)
			{
				ServerListScreen.nameServer[i] = dataInputStream.readUTF();
				ServerListScreen.address[i] = dataInputStream.readUTF();
				ServerListScreen.port[i] = dataInputStream.readShort();
				ServerListScreen.language[i] = dataInputStream.readByte();
				ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
				array[i] = string.Concat(new object[]
				{
					ServerListScreen.nameServer[i],
					":",
					ServerListScreen.address[i],
					":",
					ServerListScreen.port[i],
					":",
					ServerListScreen.language[i]
				});
			}
			ServerListScreen.serverPriority = dataInputStream.readByte();
			dataInputStream.close();
		}
		catch (Exception ex)
		{
		}
		return array;
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00096F94 File Offset: 0x00095394
	public override void switchToMe()
	{
		EffectManager.remove();
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		int num = (text == null || !(text != string.Empty)) ? -1 : int.Parse(text);
		if (num > 0)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		global::Char.isLoadingMap = false;
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000970B4 File Offset: 0x000954B4
	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		int num = (text == null || !(text != string.Empty)) ? -1 : int.Parse(text);
		if (num > 0)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x000971C9 File Offset: 0x000955C9
	public void connectOk()
	{
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x000971CC File Offset: 0x000955CC
	public void cancel()
	{
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		ServerListScreen.demPercent = 0;
		ServerListScreen.percent = 0;
		ServerListScreen.stopDownload = true;
		GameCanvas.serverScreen.show2();
		ServerListScreen.isGetData = false;
		ServerListScreen.cmdDownload.isFocus = true;
		this.center = new Command(string.Empty, this, 2, null);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00097230 File Offset: 0x00095630
	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction);
		if (idAction == 1000)
		{
			GameCanvas.connect();
		}
		if (idAction == 1 || idAction == 4)
		{
			Session_ME.gI().close();
			ServerListScreen.isAutoConect = false;
			ServerListScreen.countDieConnect = 0;
			ServerListScreen.loadScreen = true;
			ServerListScreen.testConnect = 0;
			ServerListScreen.isGetData = false;
			Rms.clearAll();
			this.switchToMe();
		}
		if (idAction == 2)
		{
			ServerListScreen.stopDownload = false;
			ServerListScreen.cmdDownload = new Command(mResources.huy, this, 4, null);
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 65;
			this.right = null;
			if (!GameCanvas.isTouch)
			{
				ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				ServerListScreen.cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			this.center = new Command(string.Empty, this, 4, null);
			if (!ServerListScreen.isGetData)
			{
				Service.gI().getResource(1, null);
				if (!GameCanvas.isTouch)
				{
					ServerListScreen.cmdDownload.isFocus = true;
					this.center = new Command(string.Empty, this, 4, null);
				}
				ServerListScreen.isGetData = true;
			}
		}
		if (idAction == 3)
		{
			Res.outz("toi day");
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			bool flag = Rms.loadRMSString("acc") != null && !Rms.loadRMSString("acc").Equals(string.Empty);
			bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty);
			if (!flag && !flag2)
			{
				GameCanvas.connect();
				string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
				if (text == null || text.Equals(string.Empty))
				{
					Service.gI().login2(string.Empty);
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
				}
				if (Session_ME.connected)
				{
					GameCanvas.startWaitDlg();
				}
				else
				{
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				}
			}
			else
			{
				GameCanvas.loginScr.doLogin();
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 10100)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			GameCanvas.connect();
			Service.gI().login2(string.Empty);
			Res.outz("tao user ao");
			GameCanvas.startWaitDlg();
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 5)
		{
			ServerListScreen.doUpdateServer();
			if (ServerListScreen.nameServer.Length == 1)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				myVector.addElement(new Command(ServerListScreen.nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			if (!GameCanvas.isTouch)
			{
				GameCanvas.menu.menuSelectedItem = ServerListScreen.ipSelect;
			}
		}
		if (idAction == 6)
		{
			ServerListScreen.ipSelect = GameCanvas.menu.menuSelectedItem;
			this.selectServer();
		}
		if (idAction == 7)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
		}
		if (idAction == 8)
		{
			bool flag3 = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			if (flag3)
			{
				GameCanvas.menu.menuSelectedItem = 0;
			}
			else
			{
				GameCanvas.menu.menuSelectedItem = 1;
			}
		}
		if (idAction == 9)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 10)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 11)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			string text2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			if (text2 == null || text2.Equals(string.Empty))
			{
				Service.gI().login2(string.Empty);
			}
			else
			{
				GameCanvas.loginScr.isLogin2 = true;
				GameCanvas.connect();
				Service.gI().setClientType();
				Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
			}
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Res.outz("tao user ao");
		}
		if (idAction == 12)
		{
			GameMidlet.instance.exit();
		}
		if (idAction == 13 && (!ServerListScreen.isGetData || ServerListScreen.loadScreen))
		{
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				break;
			case 4:
				mSystem.callHotlinePC();
				break;
			case 6:
				mSystem.callHotlineWindowsPhone();
				break;
			}
		}
		if (idAction == 14)
		{
			Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
			Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
			GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
		}
		if (idAction == 15)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 16)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
		if (idAction == 17)
		{
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 18)
		{
			GameCanvas.endDlg();
			InfoDlg.hide();
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		if (idAction == 19)
		{
			if (mSystem.clientType == 1)
			{
				InfoDlg.hide();
				GameCanvas.currentDialog = null;
			}
			else
			{
				ServerListScreen.countDieConnect = 0;
				ServerListScreen.testConnect = 0;
				ServerListScreen.isAutoConect = true;
			}
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00097918 File Offset: 0x00095D18
	public void init()
	{
		if (!ServerListScreen.loadScreen)
		{
			ServerListScreen.cmdDownload = new Command(mResources.taidulieu, this, 2, null);
			ServerListScreen.cmdDownload.isFocus = true;
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 45;
			if (ServerListScreen.cmdDownload.y > GameCanvas.h - 26)
			{
				ServerListScreen.cmdDownload.y = GameCanvas.h - 26;
			}
		}
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x000979B8 File Offset: 0x00095DB8
	public void show2()
	{
		GameScr.cmx = 0;
		GameScr.cmy = 0;
		this.initCommand();
		ServerListScreen.loadScreen = false;
		ServerListScreen.percent = 0;
		ServerListScreen.bigOk = false;
		ServerListScreen.isGetData = false;
		ServerListScreen.p = 0;
		ServerListScreen.demPercent = 0;
		ServerListScreen.strWait = mResources.PLEASEWAIT;
		global::Char.isLoadingMap = false;
		this.init();
		base.switchToMe();
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00097A18 File Offset: 0x00095E18
	public void setLinkDefault(sbyte language)
	{
		if ((int)language == 2)
		{
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaIn;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneIn;
			}
		}
		else if ((int)language == 1)
		{
			ServerListScreen.linkDefault = ServerListScreen.javaE;
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaE;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneE;
			}
		}
		else
		{
			ServerListScreen.linkDefault = ServerListScreen.javaVN;
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaVN;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneVN;
			}
		}
		mSystem.AddIpTest();
	}

	// Token: 0x04001224 RID: 4644
	public static string[] nameServer;

	// Token: 0x04001225 RID: 4645
	public static string[] address;

	// Token: 0x04001226 RID: 4646
	public static sbyte serverPriority;

	// Token: 0x04001227 RID: 4647
	public static bool[] hasConnected;

	// Token: 0x04001228 RID: 4648
	public static short[] port;

	// Token: 0x04001229 RID: 4649
	public static int selected;

	// Token: 0x0400122A RID: 4650
	public static bool isWait;

	// Token: 0x0400122B RID: 4651
	public static Command cmdUpdateServer;

	// Token: 0x0400122C RID: 4652
	public static sbyte[] language;

	// Token: 0x0400122D RID: 4653
	private Command[] cmd;

	// Token: 0x0400122E RID: 4654
	private Command cmdCallHotline;

	// Token: 0x0400122F RID: 4655
	private int nCmdPlay;

	// Token: 0x04001230 RID: 4656
	public static Command cmdDeleteRMS;

	// Token: 0x04001231 RID: 4657
	private int lY;

	// Token: 0x04001232 RID: 4658
	//public static string smartPhoneVN = "Vũ trụ 1:dragon1.teamobi.com:14445:0,Vũ trụ 2:dragon2.teamobi.com:14445:0,Vũ trụ 3:dragon3.teamobi.com:14445:0,Vũ trụ 4:dragon4.teamobi.com:14445:0,Vũ trụ 5:dragon5.teamobi.com:14445:0,Vũ trụ 6:dragon6.teamobi.com:14445:0,Vũ trụ 7:dragon7.teamobi.com:14445:0,Vũ trụ 8:dragon10.teamobi.com:14446:0,Vũ trụ 9:dragon10.teamobi.com:14447:0,Vũ trụ 10:dragon10.teamobi.com:14445:0,Vũ trụ 11:dragon11.teamobi.com:14445:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0,Universe 1:dragon.indonaga.com:14445:1,Naga:dragon.indonaga.com:14446:2,0,0";
    public static string smartPhoneVN = "NRO KAI:130.27.237.238:14445:0,0,0,0";

    // Token: 0x04001233 RID: 4659
    public static string javaVN = "Vũ trụ 1:112.213.94.23:14445:0,Vũ trụ 2:210.211.109.199:14445:0,Vũ trụ 3:112.213.85.88:14445:0,Vũ trụ 4:27.0.12.164:14445:0,Vũ trụ 5:27.0.12.16:14445:0,Vũ trụ 6:27.0.12.173:14445:0,Vũ trụ 7:112.213.94.223:14445:0,Vũ trụ 8:27.0.14.66:14446:0,Vũ trụ 9:27.0.14.66:14447:0,Vũ trụ 10:27.0.14.66:14445:0,Vũ trụ 11:112.213.85.35:14445:0,Võ đài liên vũ trụ:27.0.12.173:20000:0,Universe 1:52.74.230.22:14445:1,Naga:52.74.230.22:14446:2,0,0";

	// Token: 0x04001234 RID: 4660
	public static string smartPhoneIn = "Naga:dragon.indonaga.com:14446:2,2,0";

	// Token: 0x04001235 RID: 4661
	public static string javaIn = "Naga:52.74.230.22:14446:2,2,0";

	// Token: 0x04001236 RID: 4662
	public static string smartPhoneE = "Universe 1:dragon.indonaga.com:14445:1,1,0";

	// Token: 0x04001237 RID: 4663
	public static string javaE = "Universe 1:52.74.230.22:14445:1,1,0";

	// Token: 0x04001238 RID: 4664
	public static string linkGetHost = "http://sv1.ngocrongonline.com/game/ngocrong031_t.php";

	// Token: 0x04001239 RID: 4665
	public static string linkDefault = ServerListScreen.javaVN;

	// Token: 0x0400123A RID: 4666
	public const sbyte languageVersion = 2;

	// Token: 0x0400123B RID: 4667
	public new int keyTouch = -1;

	// Token: 0x0400123C RID: 4668
	private int tam;

	// Token: 0x0400123D RID: 4669
	public static bool stopDownload;

	// Token: 0x0400123E RID: 4670
	public static string linkweb = "http://ngocrongonline.com";

	// Token: 0x0400123F RID: 4671
	public static int countDieConnect;

	// Token: 0x04001240 RID: 4672
	public static bool waitToLogin;

	// Token: 0x04001241 RID: 4673
	public static int tWaitToLogin;

	// Token: 0x04001242 RID: 4674
	public static int[] lengthServer = new int[3];

	// Token: 0x04001243 RID: 4675
	public static int ipSelect;

	// Token: 0x04001244 RID: 4676
	public static int flagServer;

	// Token: 0x04001245 RID: 4677
	public static bool bigOk;

	// Token: 0x04001246 RID: 4678
	public static int percent;

	// Token: 0x04001247 RID: 4679
	public static string strWait;

	// Token: 0x04001248 RID: 4680
	public static int nBig;

	// Token: 0x04001249 RID: 4681
	public static int nBg;

	// Token: 0x0400124A RID: 4682
	public static int demPercent;

	// Token: 0x0400124B RID: 4683
	public static int maxBg;

	// Token: 0x0400124C RID: 4684
	public static bool isGetData = false;

	// Token: 0x0400124D RID: 4685
	public static Command cmdDownload;

	// Token: 0x0400124E RID: 4686
	private Command cmdStart;

	// Token: 0x0400124F RID: 4687
	public string dataSize;

	// Token: 0x04001250 RID: 4688
	public static int p;

	// Token: 0x04001251 RID: 4689
	public static int testConnect = -1;

	// Token: 0x04001252 RID: 4690
	public static bool loadScreen;

	// Token: 0x04001253 RID: 4691
	public static bool isAutoConect = true;
}
