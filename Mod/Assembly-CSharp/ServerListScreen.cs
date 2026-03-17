using System;

// Token: 0x02000098 RID: 152
public class ServerListScreen : mScreen, IActionListener
{
	// Token: 0x0600083C RID: 2108 RVA: 0x00091BE4 File Offset: 0x0008FDE4
	public ServerListScreen()
	{
		int num = 4;
		int num2 = num * 32 + 23 + 33;
		bool flag = num2 >= GameCanvas.w;
		if (flag)
		{
			num--;
			num2 = num * 32 + 23 + 33;
		}
		this.initCommand();
		bool flag2 = !GameCanvas.isTouch;
		if (flag2)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		bool flag3 = this.cmdCallHotline == null;
		if (flag3)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			bool flag4 = mSystem.clientType == 1 && !GameCanvas.isTouch;
			if (flag4)
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
			bool flag5 = text == null;
			if (flag5)
			{
				text = ServerListScreen.linkDefault;
			}
			else
			{
				bool flag6 = text == null && text2 != null;
				if (flag6)
				{
					bool flag7 = text2.Equals(string.Empty) || text2.Length < 20;
					if (flag7)
					{
						text2 = ServerListScreen.linkDefault;
					}
					ServerListScreen.getServerList(text2);
				}
				bool flag8 = text != null && text2 == null;
				if (flag8)
				{
					bool flag9 = text.Equals(string.Empty) || text.Length < 20;
					if (flag9)
					{
						text = ServerListScreen.linkDefault;
					}
					ServerListScreen.getServerList(text);
				}
				bool flag10 = text != null && text2 != null;
				if (flag10)
				{
					bool flag11 = text.Length > text2.Length;
					if (flag11)
					{
						ServerListScreen.getServerList(text);
					}
					else
					{
						ServerListScreen.getServerList(text2);
					}
				}
			}
		};
		this.setLinkDefault(mSystem.LANGUAGE);
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00091D24 File Offset: 0x0008FF24
	public static void createDeleteRMS()
	{
		bool flag = ServerListScreen.cmdDeleteRMS == null;
		if (flag)
		{
			bool flag2 = GameCanvas.serverScreen == null;
			if (flag2)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			ServerListScreen.cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			ServerListScreen.cmdDeleteRMS.x = GameCanvas.w - 78;
			ServerListScreen.cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00091D94 File Offset: 0x0008FF94
	private void initCommand()
	{
		this.nCmdPlay = 0;
		string text = Rms.loadRMSString("acc");
		bool flag = text == null;
		if (flag)
		{
			bool flag2 = Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null;
			if (flag2)
			{
				this.nCmdPlay = 1;
			}
		}
		else
		{
			bool flag3 = text.Equals(string.Empty);
			if (flag3)
			{
				bool flag4 = Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null;
				if (flag4)
				{
					this.nCmdPlay = 1;
				}
			}
			else
			{
				this.nCmdPlay = 1;
			}
		}
		this.cmd = new Command[(mGraphics.zoomLevel <= 1) ? (4 + this.nCmdPlay) : (3 + this.nCmdPlay)];
		int num = GameCanvas.hh - 15 * this.cmd.Length + 28;
		for (int i = 0; i < this.cmd.Length; i++)
		{
			switch (i)
			{
			case 0:
			{
				this.cmd[0] = new Command(string.Empty, this, 3, null);
				bool flag5 = text == null;
				if (flag5)
				{
					this.cmd[0].caption = mResources.playNew;
					bool flag6 = Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null;
					if (flag6)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else
				{
					bool flag7 = text.Equals(string.Empty);
					if (flag7)
					{
						this.cmd[0].caption = mResources.playNew;
						bool flag8 = Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null;
						if (flag8)
						{
							this.cmd[0].caption = mResources.choitiep;
						}
					}
					else
					{
						this.cmd[0].caption = mResources.playAcc + ": " + text;
						bool flag9 = this.cmd[0].caption.Length > 23;
						if (flag9)
						{
							this.cmd[0].caption = this.cmd[0].caption.Substring(0, 23);
							Command command = this.cmd[0];
							Command command2 = command;
							command2.caption += "...";
						}
					}
				}
				break;
			}
			case 1:
			{
				bool flag10 = this.nCmdPlay == 1;
				if (flag10)
				{
					this.cmd[1] = new Command(string.Empty, this, 10100, null);
					this.cmd[1].caption = mResources.playNew;
				}
				else
				{
					this.cmd[1] = new Command(mResources.change_account, this, 7, null);
				}
				break;
			}
			case 2:
			{
				bool flag11 = this.nCmdPlay == 1;
				if (flag11)
				{
					this.cmd[2] = new Command(mResources.change_account, this, 7, null);
				}
				else
				{
					this.cmd[2] = new Command(string.Empty, this, 17, null);
				}
				break;
			}
			case 3:
			{
				bool flag12 = this.nCmdPlay == 1;
				if (flag12)
				{
					this.cmd[3] = new Command(string.Empty, this, 17, null);
				}
				else
				{
					this.cmd[3] = new Command(mResources.option, this, 8, null);
				}
				break;
			}
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

	// Token: 0x0600083F RID: 2111 RVA: 0x00092148 File Offset: 0x00090348
	public static void doUpdateServer()
	{
		bool flag = ServerListScreen.cmdUpdateServer == null && GameCanvas.serverScreen == null;
		if (flag)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		Net.connectHTTP2(ServerListScreen.linkDefault, ServerListScreen.cmdUpdateServer);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00092188 File Offset: 0x00090388
	public static void getServerList(string str)
	{
		ServerListScreen.lengthServer = new int[3];
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz("tem leng= " + array.Length.ToString());
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

	// Token: 0x06000841 RID: 2113 RVA: 0x000922B8 File Offset: 0x000904B8
	public override void paint(mGraphics g)
	{
		bool flag = !ServerListScreen.loadScreen;
		if (flag)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			bool flag2 = !ServerListScreen.bigOk;
			if (flag2)
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
		bool flag3 = ServerListScreen.testConnect == 0;
		if (flag3)
		{
			text = text + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " disconnect";
		}
		else
		{
			text = text + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " connected";
		}
		bool isTest = mSystem.isTest;
		if (isTest)
		{
			mFont.tahoma_7_white.drawString(g, text, GameCanvas.w - 2, num + 15 + 15, 1, mFont.tahoma_7_grey);
		}
		bool flag4 = !ServerListScreen.isGetData || ServerListScreen.loadScreen;
		if (flag4)
		{
			bool flag5 = mSystem.clientType == 1 && !GameCanvas.isTouch;
			if (flag5)
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
		int num3 = (GameCanvas.w < 200) ? 160 : 180;
		bool flag6 = ServerListScreen.cmdDeleteRMS != null;
		if (flag6)
		{
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		bool flag7 = GameCanvas.currentDialog == null;
		if (flag7)
		{
			bool flag8 = !ServerListScreen.loadScreen;
			if (flag8)
			{
				bool flag9 = !ServerListScreen.bigOk;
				if (flag9)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
					bool flag10 = !ServerListScreen.isGetData;
					if (flag10)
					{
						mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
						bool flag11 = ServerListScreen.cmdDownload != null;
						if (flag11)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
					}
					else
					{
						bool flag12 = ServerListScreen.cmdDownload != null;
						if (flag12)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
						mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + ServerListScreen.percent.ToString() + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
						GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, 100f, g);
						GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, (float)ServerListScreen.percent, g);
					}
				}
			}
			else
			{
				int num2 = GameCanvas.hh - 15 * this.cmd.Length - 15;
				bool flag13 = num2 < 25;
				if (flag13)
				{
					num2 = 25;
				}
				bool flag14 = LoginScr.imgTitle != null;
				if (flag14)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num2, 3);
				}
				for (int i = 0; i < this.cmd.Length; i++)
				{
					this.cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				bool flag15 = ServerListScreen.testConnect == -1;
				if (flag15)
				{
					bool flag16 = GameCanvas.gameTick % 20 > 10;
					if (flag16)
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

	// Token: 0x06000842 RID: 2114 RVA: 0x00092790 File Offset: 0x00090990
	public void selectServer()
	{
		ServerListScreen.flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		Session_ME.gI().close();
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		GameMidlet.LANGUAGE = (int)ServerListScreen.language[ServerListScreen.ipSelect];
		Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
		bool flag = ServerListScreen.language[ServerListScreen.ipSelect] != mResources.language;
		if (flag)
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

	// Token: 0x06000843 RID: 2115 RVA: 0x00092858 File Offset: 0x00090A58
	public override void update()
	{
		bool flag = ServerListScreen.waitToLogin;
		if (flag)
		{
			ServerListScreen.tWaitToLogin++;
			bool flag2 = ServerListScreen.tWaitToLogin == 50;
			if (flag2)
			{
				GameCanvas.serverScreen.selectServer();
			}
			bool flag3 = ServerListScreen.tWaitToLogin == 100;
			if (flag3)
			{
				bool flag4 = GameCanvas.loginScr == null;
				if (flag4)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				ServerListScreen.waitToLogin = false;
			}
		}
		bool flag5 = ServerListScreen.flagServer > 0;
		if (flag5)
		{
			ServerListScreen.flagServer--;
			bool flag6 = ServerListScreen.flagServer == 0;
			if (flag6)
			{
				GameCanvas.endDlg();
			}
			bool flag7 = ServerListScreen.testConnect == 2;
			if (flag7)
			{
				ServerListScreen.flagServer = 0;
				GameCanvas.endDlg();
			}
		}
		bool flag8 = ServerListScreen.flagServer <= 0 && ServerListScreen.isAutoConect;
		if (flag8)
		{
			ServerListScreen.countDieConnect++;
			bool flag9 = ServerListScreen.countDieConnect > 100000;
			if (flag9)
			{
				ServerListScreen.countDieConnect = 0;
			}
		}
		for (int i = 0; i < this.cmd.Length; i++)
		{
			bool flag10 = i == ServerListScreen.selected;
			if (flag10)
			{
				this.cmd[i].isFocus = true;
			}
			else
			{
				this.cmd[i].isFocus = false;
			}
		}
		GameScr.cmx++;
		bool flag11 = !ServerListScreen.loadScreen && (ServerListScreen.bigOk || ServerListScreen.percent == 100);
		if (flag11)
		{
			ServerListScreen.cmdDownload = null;
		}
		base.update();
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			bool flag12 = !ServerListScreen.loadScreen;
			if (!flag12)
			{
				bool flag13 = !ServerListScreen.isAutoConect;
				if (!flag13)
				{
					bool flag14 = GameCanvas.currentScreen != this;
					if (!flag14)
					{
						bool flag15 = ServerListScreen.testConnect != 2;
						if (flag15)
						{
							bool flag16 = ServerListScreen.countDieConnect < ((mSystem.clientType != 1) ? 5 : 2);
							if (flag16)
							{
								bool flag17 = ServerListScreen.flagServer <= 0;
								if (flag17)
								{
									ServerListScreen.flagServer = 30;
									GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
									GameCanvas.connect();
								}
							}
							else
							{
								bool flag18 = !Session_ME.gI().isConnected();
								if (flag18)
								{
									bool flag19 = ServerListScreen.flagServer <= 0;
									if (flag19)
									{
										Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 18, null);
										Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 19, null);
										GameCanvas.startYesNoDlg(mResources.maychutathoacmatsong + "." + mResources.confirmChangeServer, cmdYes, cmdNo);
										ServerListScreen.flagServer = 30;
									}
								}
								else
								{
									bool flag20 = ServerListScreen.flagServer <= 0;
									if (flag20)
									{
										ServerListScreen.countDieConnect = 0;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00092B38 File Offset: 0x00090D38
	private void processInput()
	{
		bool flag = ServerListScreen.loadScreen;
		if (flag)
		{
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		}
		else
		{
			this.center = ServerListScreen.cmdDownload;
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00092B84 File Offset: 0x00090D84
	public static void updateDeleteData()
	{
		bool flag = ServerListScreen.cmdDeleteRMS != null && ServerListScreen.cmdDeleteRMS.isPointerPressInside();
		if (flag)
		{
			ServerListScreen.cmdDeleteRMS.performAction();
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00092BB8 File Offset: 0x00090DB8
	public override void updateKey()
	{
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			ServerListScreen.updateDeleteData();
			bool flag = this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside();
			if (flag)
			{
				this.cmdCallHotline.performAction();
			}
			bool flag2 = !ServerListScreen.loadScreen;
			if (flag2)
			{
				bool flag3 = ServerListScreen.cmdDownload != null && ServerListScreen.cmdDownload.isPointerPressInside();
				if (flag3)
				{
					ServerListScreen.cmdDownload.performAction();
				}
				base.updateKey();
				return;
			}
			for (int i = 0; i < this.cmd.Length; i++)
			{
				bool flag4 = this.cmd[i] != null && this.cmd[i].isPointerPressInside();
				if (flag4)
				{
					bool flag5 = ServerListScreen.testConnect == -1 || ServerListScreen.testConnect == 0;
					if (flag5)
					{
						bool flag6 = this.cmd[i].caption.IndexOf(mResources.server) != -1;
						if (flag6)
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
		else
		{
			bool flag7 = ServerListScreen.loadScreen;
			if (flag7)
			{
				bool flag8 = GameCanvas.keyPressed[8];
				if (flag8)
				{
					int num = (mGraphics.zoomLevel <= 1) ? 4 : 2;
					GameCanvas.keyPressed[8] = false;
					ServerListScreen.selected++;
					bool flag9 = ServerListScreen.selected > num;
					if (flag9)
					{
						ServerListScreen.selected = 0;
					}
					this.processInput();
				}
				bool flag10 = GameCanvas.keyPressed[2];
				if (flag10)
				{
					int num2 = (mGraphics.zoomLevel <= 1) ? 4 : 2;
					GameCanvas.keyPressed[2] = false;
					ServerListScreen.selected--;
					bool flag11 = ServerListScreen.selected < 0;
					if (flag11)
					{
						ServerListScreen.selected = num2;
					}
					this.processInput();
				}
			}
		}
		bool flag12 = ServerListScreen.isWait;
		if (!flag12)
		{
			base.updateKey();
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00092DB4 File Offset: 0x00090FB4
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
			ServerListScreen.serverPriority = ((sbyte)((!mSystem.isTest) ? ServerListScreen.serverPriority : (ServerListScreen.serverPriority + 5)));
			dataOutputStream.writeByte(ServerListScreen.serverPriority);
			Rms.saveRMS("NRlink2", dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00092E94 File Offset: 0x00091094
	public static bool allServerConnected()
	{
		for (int i = 0; i < 2; i++)
		{
			bool flag = !ServerListScreen.hasConnected[i];
			if (flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00092ECC File Offset: 0x000910CC
	public static void loadIP()
	{
		sbyte[] array = Rms.loadRMS("NRlink2");
		bool flag = array == null;
		if (flag)
		{
			ServerListScreen.getServerList(ServerListScreen.linkDefault);
		}
		else
		{
			DataInputStream dataInputStream = new DataInputStream(array);
			bool flag2 = dataInputStream == null;
			if (!flag2)
			{
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
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00093000 File Offset: 0x00091200
	public static string[] loadIP_2()
	{
		string[] array = null;
		sbyte[] array2 = Rms.loadRMS("NRlink2");
		bool flag = array2 == null;
		string[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			DataInputStream dataInputStream = new DataInputStream(array2);
			bool flag2 = dataInputStream == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
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
					Res.outz("len sv == " + b.ToString());
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
				result = array;
			}
		}
		return result;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000931BC File Offset: 0x000913BC
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
		bool flag = num > 0;
		if (flag)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		bool flag2 = this.cmd.Length == 4 + this.nCmdPlay;
		if (flag2)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		global::Char.isLoadingMap = false;
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x000932E0 File Offset: 0x000914E0
	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		int num = (text == null || !(text != string.Empty)) ? -1 : int.Parse(text);
		bool flag = num > 0;
		if (flag)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		bool flag2 = this.cmd.Length == 4 + this.nCmdPlay;
		if (flag2)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00003136 File Offset: 0x00001336
	public void connectOk()
	{
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x000933F8 File Offset: 0x000915F8
	public void cancel()
	{
		bool flag = GameCanvas.serverScreen == null;
		if (flag)
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

	// Token: 0x0600084F RID: 2127 RVA: 0x00093460 File Offset: 0x00091660
	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction.ToString());
		bool flag4 = idAction == 1000;
		if (flag4)
		{
			GameCanvas.connect();
		}
		bool flag5 = idAction == 1 || idAction == 4;
		if (flag5)
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
		bool flag6 = idAction == 2;
		if (flag6)
		{
			ServerListScreen.stopDownload = false;
			ServerListScreen.cmdDownload = new Command(mResources.huy, this, 4, null);
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 65;
			this.right = null;
			bool flag7 = !GameCanvas.isTouch;
			if (flag7)
			{
				ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				ServerListScreen.cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			this.center = new Command(string.Empty, this, 4, null);
			bool flag8 = !ServerListScreen.isGetData;
			if (flag8)
			{
				Service.gI().getResource(1, null);
				bool flag9 = !GameCanvas.isTouch;
				if (flag9)
				{
					ServerListScreen.cmdDownload.isFocus = true;
					this.center = new Command(string.Empty, this, 4, null);
				}
				ServerListScreen.isGetData = true;
			}
		}
		bool flag10 = idAction == 3;
		if (flag10)
		{
			Res.outz("toi day");
			bool flag11 = GameCanvas.loginScr == null;
			if (flag11)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			bool flag = Rms.loadRMSString("acc") != null && !Rms.loadRMSString("acc").Equals(string.Empty);
			bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()).Equals(string.Empty);
			bool flag12 = !flag && !flag2;
			if (flag12)
			{
				GameCanvas.connect();
				string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
				bool flag13 = text == null || text.Equals(string.Empty);
				if (flag13)
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
				bool connected = Session_ME.connected;
				if (connected)
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
		bool flag14 = idAction == 10100;
		if (flag14)
		{
			bool flag15 = GameCanvas.loginScr == null;
			if (flag15)
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
		bool flag16 = idAction == 5;
		if (flag16)
		{
			ServerListScreen.doUpdateServer();
			bool flag17 = ServerListScreen.nameServer.Length == 1;
			if (flag17)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				myVector.addElement(new Command(ServerListScreen.nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			bool flag18 = !GameCanvas.isTouch;
			if (flag18)
			{
				GameCanvas.menu.menuSelectedItem = ServerListScreen.ipSelect;
			}
		}
		bool flag19 = idAction == 6;
		if (flag19)
		{
			ServerListScreen.ipSelect = GameCanvas.menu.menuSelectedItem;
			this.selectServer();
		}
		bool flag20 = idAction == 7;
		if (flag20)
		{
			bool flag21 = GameCanvas.loginScr == null;
			if (flag21)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
		}
		bool flag22 = idAction == 8;
		if (flag22)
		{
			bool flag3 = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			bool flag23 = flag3;
			if (flag23)
			{
				GameCanvas.menu.menuSelectedItem = 0;
			}
			else
			{
				GameCanvas.menu.menuSelectedItem = 1;
			}
		}
		bool flag24 = idAction == 9;
		if (flag24)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		bool flag25 = idAction == 10;
		if (flag25)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		bool flag26 = idAction == 11;
		if (flag26)
		{
			bool flag27 = GameCanvas.loginScr == null;
			if (flag27)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			string text2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			bool flag28 = text2 == null || text2.Equals(string.Empty);
			if (flag28)
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
		bool flag29 = idAction == 12;
		if (flag29)
		{
			GameMidlet.instance.exit();
		}
		bool flag30 = idAction == 13 && (!ServerListScreen.isGetData || ServerListScreen.loadScreen);
		if (flag30)
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
		bool flag31 = idAction == 14;
		if (flag31)
		{
			Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
			Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
			GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
		}
		bool flag32 = idAction == 15;
		if (flag32)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		bool flag33 = idAction == 16;
		if (flag33)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
		bool flag34 = idAction == 17;
		if (flag34)
		{
			bool flag35 = GameCanvas.serverScr == null;
			if (flag35)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		bool flag36 = idAction == 18;
		if (flag36)
		{
			GameCanvas.endDlg();
			InfoDlg.hide();
			bool flag37 = GameCanvas.serverScr == null;
			if (flag37)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
		bool flag38 = idAction == 19;
		if (flag38)
		{
			bool flag39 = mSystem.clientType == 1;
			if (flag39)
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

	// Token: 0x06000850 RID: 2128 RVA: 0x00093C14 File Offset: 0x00091E14
	public void init()
	{
		bool flag = !ServerListScreen.loadScreen;
		if (flag)
		{
			ServerListScreen.cmdDownload = new Command(mResources.taidulieu, this, 2, null);
			ServerListScreen.cmdDownload.isFocus = true;
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 45;
			bool flag2 = ServerListScreen.cmdDownload.y > GameCanvas.h - 26;
			if (flag2)
			{
				ServerListScreen.cmdDownload.y = GameCanvas.h - 26;
			}
		}
		bool flag3 = !GameCanvas.isTouch;
		if (flag3)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00093CC0 File Offset: 0x00091EC0
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

	// Token: 0x06000852 RID: 2130 RVA: 0x00093D24 File Offset: 0x00091F24
	public void setLinkDefault(sbyte language)
	{
		bool flag = language == 2;
		if (flag)
		{
			bool flag2 = mSystem.clientType == 1;
			if (flag2)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaIn;
			}
			else
			{
				ServerListScreen.linkDefault = ServerListScreen.smartPhoneIn;
			}
		}
		else
		{
			bool flag3 = language == 1;
			if (flag3)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaE;
				bool flag4 = mSystem.clientType == 1;
				if (flag4)
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
				bool flag5 = mSystem.clientType == 1;
				if (flag5)
				{
					ServerListScreen.linkDefault = ServerListScreen.javaVN;
				}
				else
				{
					ServerListScreen.linkDefault = ServerListScreen.smartPhoneVN;
				}
			}
		}
		mSystem.AddIpTest();
	}

	// Token: 0x040010BB RID: 4283
	public static string[] nameServer;

	// Token: 0x040010BC RID: 4284
	public static string[] address;

	// Token: 0x040010BD RID: 4285
	public static sbyte serverPriority;

	// Token: 0x040010BE RID: 4286
	public static bool[] hasConnected;

	// Token: 0x040010BF RID: 4287
	public static short[] port;

	// Token: 0x040010C0 RID: 4288
	public static int selected;

	// Token: 0x040010C1 RID: 4289
	public static bool isWait;

	// Token: 0x040010C2 RID: 4290
	public static Command cmdUpdateServer;

	// Token: 0x040010C3 RID: 4291
	public static sbyte[] language;

	// Token: 0x040010C4 RID: 4292
	private Command[] cmd;

	// Token: 0x040010C5 RID: 4293
	private Command cmdCallHotline;

	// Token: 0x040010C6 RID: 4294
	private int nCmdPlay;

	// Token: 0x040010C7 RID: 4295
	public static Command cmdDeleteRMS;

	// Token: 0x040010C8 RID: 4296
	private int lY;

	// Token: 0x040010C9 RID: 4297
	//public static string smartPhoneVN = "Vũ trụ 1:dragon1.teamobi.com:14445:0,Vũ trụ 2:dragon2.teamobi.com:14445:0,Vũ trụ 3:dragon3.teamobi.com:14445:0,Vũ trụ 4:dragon4.teamobi.com:14445:0,Vũ trụ 5:dragon5.teamobi.com:14445:0,Vũ trụ 6:dragon6.teamobi.com:14445:0,Vũ trụ 7:dragon7.teamobi.com:14445:0,Vũ trụ 8:dragon10.teamobi.com:14446:0,Vũ trụ 9:dragon10.teamobi.com:14447:0,Vũ trụ 10:dragon10.teamobi.com:14445:0,Vũ trụ 11:dragon11.teamobi.com:14445:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0,Universe 1:dragon.indonaga.com:14445:1,Naga:dragon.indonaga.com:14446:2,0,0";
    public static string smartPhoneVN = "kaiosv3:103.27.237.238:14445:0,0,0";

    // Token: 0x040010CA RID: 4298
    public static string javaVN = "Vũ trụ 1:112.213.94.23:14445:0,Vũ trụ 2:210.211.109.199:14445:0,Vũ trụ 3:112.213.85.88:14445:0,Vũ trụ 4:27.0.12.164:14445:0,Vũ trụ 5:27.0.12.16:14445:0,Vũ trụ 6:27.0.12.173:14445:0,Vũ trụ 7:112.213.94.223:14445:0,Vũ trụ 8:27.0.14.66:14446:0,Vũ trụ 9:27.0.14.66:14447:0,Vũ trụ 10:27.0.14.66:14445:0,Vũ trụ 11:112.213.85.35:14445:0,Võ đài liên vũ trụ:27.0.12.173:20000:0,Universe 1:52.74.230.22:14445:1,Naga:52.74.230.22:14446:2,0,0";

	// Token: 0x040010CB RID: 4299
	public static string smartPhoneIn = "Naga:dragon.indonaga.com:14446:2,2,0";

	// Token: 0x040010CC RID: 4300
	public static string javaIn = "Naga:52.74.230.22:14446:2,2,0";

	// Token: 0x040010CD RID: 4301
	public static string smartPhoneE = "Universe 1:dragon.indonaga.com:14445:1,1,0";

	// Token: 0x040010CE RID: 4302
	public static string javaE = "Universe 1:52.74.230.22:14445:1,1,0";

	// Token: 0x040010CF RID: 4303
	public static string linkGetHost = "http://sv1.ngocrongonline.com/game/ngocrong031_t.php";

	// Token: 0x040010D0 RID: 4304
	public static string linkDefault = ServerListScreen.javaVN;

	// Token: 0x040010D1 RID: 4305
	public const sbyte languageVersion = 2;

	// Token: 0x040010D2 RID: 4306
	public new int keyTouch = -1;

	// Token: 0x040010D3 RID: 4307
	private int tam;

	// Token: 0x040010D4 RID: 4308
	public static bool stopDownload;

	// Token: 0x040010D5 RID: 4309
	public static string linkweb = "http://ngocrongonline.com";

	// Token: 0x040010D6 RID: 4310
	public static int countDieConnect;

	// Token: 0x040010D7 RID: 4311
	public static bool waitToLogin;

	// Token: 0x040010D8 RID: 4312
	public static int tWaitToLogin;

	// Token: 0x040010D9 RID: 4313
	public static int[] lengthServer = new int[3];

	// Token: 0x040010DA RID: 4314
	public static int ipSelect;

	// Token: 0x040010DB RID: 4315
	public static int flagServer;

	// Token: 0x040010DC RID: 4316
	public static bool bigOk;

	// Token: 0x040010DD RID: 4317
	public static int percent;

	// Token: 0x040010DE RID: 4318
	public static string strWait;

	// Token: 0x040010DF RID: 4319
	public static int nBig;

	// Token: 0x040010E0 RID: 4320
	public static int nBg;

	// Token: 0x040010E1 RID: 4321
	public static int demPercent;

	// Token: 0x040010E2 RID: 4322
	public static int maxBg;

	// Token: 0x040010E3 RID: 4323
	public static bool isGetData = false;

	// Token: 0x040010E4 RID: 4324
	public static Command cmdDownload;

	// Token: 0x040010E5 RID: 4325
	private Command cmdStart;

	// Token: 0x040010E6 RID: 4326
	public string dataSize;

	// Token: 0x040010E7 RID: 4327
	public static int p;

	// Token: 0x040010E8 RID: 4328
	public static int testConnect = -1;

	// Token: 0x040010E9 RID: 4329
	public static bool loadScreen;

	// Token: 0x040010EA RID: 4330
	public static bool isAutoConect = true;
}
