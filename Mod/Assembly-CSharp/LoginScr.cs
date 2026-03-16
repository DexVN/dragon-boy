using System;

// Token: 0x020000B4 RID: 180
public class LoginScr : mScreen, IActionListener
{
	// Token: 0x060007EC RID: 2028 RVA: 0x00071CA8 File Offset: 0x000700A8
	public LoginScr()
	{
		this.yLog = GameCanvas.hh - 30;
		TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		Main.closeKeyBoard();
		if (GameCanvas.h > 200)
		{
			this.defYL = GameCanvas.hh - 80;
		}
		else
		{
			this.defYL = GameCanvas.hh - 65;
		}
		this.resetLogo();
		int num = (GameCanvas.w < 200) ? 140 : 160;
		this.wC = num;
		this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
		if (GameCanvas.h <= 160)
		{
			this.yt = 20;
		}
		this.tfUser = new TField();
		this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
		this.tfUser.width = this.wC;
		this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
		this.tfUser.isFocus = true;
		this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
		this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
		this.tfPass = new TField();
		this.tfPass.y = GameCanvas.hh - 4;
		this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		this.tfPass.width = this.wC;
		this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
		this.yt += 35;
		this.isCheck = true;
		int num2 = Rms.loadRMSInt("check");
		if (num2 == 1)
		{
			this.isCheck = true;
		}
		else if (num2 == 2)
		{
			this.isCheck = false;
		}
		this.tfUser.setText(Rms.loadRMSString("acc"));
		this.tfPass.setText(Rms.loadRMSString("pass"));
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
		this.focus = 0;
		this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
		this.cmdCheck = new Command(mResources.remember, this, 2001, null);
		this.cmdRes = new Command(mResources.register, this, 2002, null);
		this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
		this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
		this.freeAreaHeight = this.tfUser.y - 2 * this.tfUser.height;
		if (GameCanvas.isTouch)
		{
			this.cmdLogin.x = GameCanvas.w / 2 + 8;
			this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
			if (GameCanvas.h >= 200)
			{
				this.cmdLogin.y = this.yLog + 110;
				this.cmdMenu.y = this.yLog + 110;
			}
			this.cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
			this.cmdBackFromRegister.y = this.yLog + 110;
			this.cmdRes.x = GameCanvas.w / 2 - 84;
			this.cmdRes.y = this.cmdMenu.y;
		}
		this.wP = 170;
		this.hP = ((!this.isRes) ? 100 : 110);
		this.xP = GameCanvas.hw - this.wP / 2;
		this.yP = this.tfUser.y - 15;
		int num4 = 4;
		int num5 = num4 * 32 + 23 + 33;
		if (num5 >= GameCanvas.w)
		{
			num4--;
			num5 = num4 * 32 + 23 + 33;
		}
		this.xLog = GameCanvas.w / 2 - num5 / 2;
		this.yLog = GameCanvas.hh - 30;
		this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
		this.tfUser.x = this.xLog + 10;
		this.tfUser.y = this.yLog + 20;
		this.cmdOK = new Command(mResources.OK, this, 2008, null);
		this.cmdOK.x = GameCanvas.w / 2 - 84;
		this.cmdOK.y = this.cmdLogin.y;
		this.cmdFogetPass = new Command(mResources.forgetPass, this, 1003, null);
		this.cmdFogetPass.x = GameCanvas.w / 2 + 3;
		this.cmdFogetPass.y = this.cmdLogin.y;
		this.center = this.cmdOK;
		this.left = this.cmdFogetPass;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000722CC File Offset: 0x000706CC
	public static void getServerLink()
	{
		try
		{
			if (!LoginScr.isTryGetIPFromWap)
			{
				Command command = new Command();
				ActionChat actionChat = delegate(string str)
				{
					try
					{
						if (str == null)
						{
							return;
						}
						if (str == string.Empty)
						{
							return;
						}
						Rms.saveIP(str);
						if (!str.Contains(":"))
						{
							return;
						}
						int num = str.IndexOf(":");
						string text = str.Substring(0, num);
						string s = str.Substring(num + 1);
						GameMidlet.IP = text;
						GameMidlet.PORT = int.Parse(s);
						Session_ME.gI().connect(text, int.Parse(s));
						LoginScr.isTryGetIPFromWap = true;
					}
					catch (Exception ex)
					{
					}
				};
				command.actionChat = actionChat;
				Net.connectHTTP(ServerListScreen.linkGetHost, command);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0007233C File Offset: 0x0007073C
	public override void switchToMe()
	{
		this.isRegistering = false;
		SoundMn.gI().stopAll();
		this.tfUser.isFocus = true;
		this.tfPass.isFocus = false;
		if (GameCanvas.isTouch)
		{
			this.tfUser.isFocus = false;
		}
		GameCanvas.loadBG(0);
		base.switchToMe();
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00072394 File Offset: 0x00070794
	public void setUserPass()
	{
		string text = Rms.loadRMSString("acc");
		if (text != null && !text.Equals(string.Empty))
		{
			this.tfUser.setText(text);
		}
		string text2 = Rms.loadRMSString("pass");
		if (text2 != null && !text2.Equals(string.Empty))
		{
			this.tfPass.setText(text2);
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x000723FB File Offset: 0x000707FB
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00072400 File Offset: 0x00070800
	protected void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
		if (!this.isLogin2)
		{
			myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
		}
		myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
		myVector.addElement(new Command(mResources.website, this, 1005, null));
		if (Main.isPC)
		{
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x000724AC File Offset: 0x000708AC
	protected void doRegister()
	{
		if (this.tfUser.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.userBlank);
			return;
		}
		char[] array = this.tfUser.getText().ToCharArray();
		if (this.tfPass.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.passwordBlank);
			return;
		}
		if (this.tfUser.getText().Length < 5)
		{
			GameCanvas.startOKDlg(mResources.accTooShort);
			return;
		}
		int num = 0;
		string text = null;
		if ((int)mResources.language == 2)
		{
			if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
			{
				text = mResources.emailInvalid;
			}
			num = 0;
		}
		else
		{
			try
			{
				long num2 = long.Parse(this.tfUser.getText());
				if (this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84")))
				{
					text = mResources.phoneInvalid;
				}
				num = 1;
			}
			catch (Exception ex)
			{
				if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
				{
					text = mResources.emailInvalid;
				}
				num = 0;
			}
		}
		if (text != null)
		{
			GameCanvas.startOKDlg(text);
		}
		else
		{
			GameCanvas.msgdlg.setInfo(string.Concat(new string[]
			{
				mResources.plsCheckAcc,
				(num != 1) ? (mResources.email + ": ") : (mResources.phone + ": "),
				this.tfUser.getText(),
				"\n",
				mResources.password,
				": ",
				this.tfPass.getText()
			}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0007271C File Offset: 0x00070B1C
	protected void doRegister(string user)
	{
		this.isFAQ = false;
		GameCanvas.startWaitDlg(mResources.CONNECTING);
		GameCanvas.connect();
		GameCanvas.startWaitDlg(mResources.REGISTERING);
		this.passRe = this.tfPass.getText();
		Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect), GameMidlet.VERSION);
		Rms.saveRMSString("acc", user);
		Rms.saveRMSString("pass", this.tfPass.getText());
		this.t = 20;
		this.isRegistering = true;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x000727D8 File Offset: 0x00070BD8
	public void doViewFAQ()
	{
		if (!this.listFAQ.Equals(string.Empty) || !this.listFAQ.Equals(string.Empty))
		{
		}
		if (!Session_ME.connected)
		{
			this.isFAQ = true;
			GameCanvas.connect();
		}
		GameCanvas.startWaitDlg();
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0007282C File Offset: 0x00070C2C
	protected void doSelectServer()
	{
		MyVector myVector = new MyVector();
		if (LoginScr.isLocal)
		{
			myVector.addElement(new Command("Server LOCAL", this, 20004, null));
		}
		myVector.addElement(new Command("Server Bokken", this, 20001, null));
		myVector.addElement(new Command("Server Shuriken", this, 20002, null));
		myVector.addElement(new Command("Server Tessen (mới)", this, 20003, null));
		GameCanvas.menu.startAt(myVector, 0);
		if (this.loadIndexServer() != -1 && !GameCanvas.isTouch)
		{
			GameCanvas.menu.menuSelectedItem = this.loadIndexServer();
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x000728D7 File Offset: 0x00070CD7
	protected void saveIndexServer(int index)
	{
		Rms.saveRMSInt("indServer", index);
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000728E4 File Offset: 0x00070CE4
	protected int loadIndexServer()
	{
		return Rms.loadRMSInt("indServer");
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x000728F0 File Offset: 0x00070CF0
	public void doLogin()
	{
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("pass");
		if (text != null && !text.Equals(string.Empty))
		{
			this.isLogin2 = false;
		}
		else if (Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty))
		{
			this.isLogin2 = true;
		}
		else
		{
			this.isLogin2 = false;
		}
		if ((text == null || text.Equals(string.Empty)) && this.isLogin2)
		{
			text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			text2 = "a";
		}
		if (text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty))
		{
			return;
		}
		if (text2.Equals(string.Empty))
		{
			this.focus = 1;
			this.tfUser.isFocus = false;
			this.tfPass.isFocus = true;
			if (!GameCanvas.isTouch)
			{
				this.right = this.tfPass.cmdClear;
			}
			return;
		}
		if (!Session_ME.gI().isConnected())
		{
			GameCanvas.connect();
		}
		Res.outz(string.Concat(new object[]
		{
			"ccccccc ",
			text,
			" ",
			text2,
			" ",
			GameMidlet.VERSION,
			" ",
			(!this.isLogin2) ? 0 : 1
		}));
		Service.gI().login(text, text2, GameMidlet.VERSION, (sbyte)((!this.isLogin2) ? 0 : 1));
		if (Session_ME.connected)
		{
			GameCanvas.startWaitDlg();
		}
		else
		{
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
		}
		this.focus = 0;
		if (!this.isLogin2)
		{
			this.actRegisterLeft();
		}
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00072B1C File Offset: 0x00070F1C
	public void savePass()
	{
		if (this.isCheck)
		{
			Rms.saveRMSInt("check", 1);
			Rms.saveRMSString("acc", this.tfUser.getText().ToLower().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().ToLower().Trim());
		}
		else
		{
			Rms.saveRMSInt("check", 2);
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00072BAC File Offset: 0x00070FAC
	public override void update()
	{
		if (Main.isWindowsPhone && this.isRegistering)
		{
			if (this.t < 0)
			{
				GameCanvas.endDlg();
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
				this.isRegistering = false;
			}
			else
			{
				this.t--;
			}
		}
		if (LoginScr.timeLogin > 0)
		{
			GameCanvas.startWaitDlg();
			LoginScr.currTimeLogin = mSystem.currentTimeMillis();
			if (LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L)
			{
				LoginScr.timeLogin -= 1;
				if (LoginScr.timeLogin == 0)
				{
					GameCanvas.loginScr.doLogin();
				}
				LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
			}
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (TouchScreenKeyboard.visible)
		{
			mGraphics.addYWhenOpenKeyBoard = 50;
		}
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			effect.update();
		}
		if (LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill)
		{
			LoginScr.isUpdateAll = false;
			mSystem.gcc();
			Service.gI().finishUpdate();
		}
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		GameCanvas.debug("LGU1", 0);
		GameCanvas.debug("LGU2", 0);
		GameCanvas.debug("LGU3", 0);
		this.updateLogo();
		GameCanvas.debug("LGU4", 0);
		GameCanvas.debug("LGU5", 0);
		if (this.g >= 0)
		{
			this.ylogo += this.dir * this.g;
			this.g += this.dir * this.v;
			if (this.g <= 0)
			{
				this.dir *= -1;
			}
			if (this.ylogo > 0)
			{
				this.dir *= -1;
				this.g -= 2 * this.v;
			}
		}
		GameCanvas.debug("LGU6", 0);
		if (this.tipid >= 0 && GameCanvas.gameTick % 100 == 0)
		{
			this.doChangeTip();
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (!Main.isPC && !TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone)
		{
			string text = this.tfUser.getText().ToLower().Trim();
			string text2 = this.tfPass.getText().ToLower().Trim();
			if (!text.Equals(string.Empty) && !text2.Equals(string.Empty))
			{
				this.doLogin();
			}
			Main.isMiniApp = true;
		}
		this.updateTfWhenOpenKb();
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x000730C8 File Offset: 0x000714C8
	private void doChangeTip()
	{
		this.tipid++;
		if (this.tipid >= mResources.tips.Length)
		{
			this.tipid = 0;
		}
		if (GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait)
		{
			GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00073130 File Offset: 0x00071530
	public void updateLogo()
	{
		if (this.defYL != this.yL)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00073160 File Offset: 0x00071560
	public override void keyPress(int keyCode)
	{
		if (this.tfUser.isFocus)
		{
			this.tfUser.keyPressed(keyCode);
		}
		else if (this.tfPass.isFocus)
		{
			this.tfPass.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000731B3 File Offset: 0x000715B3
	public override void unLoad()
	{
		base.unLoad();
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000731BC File Offset: 0x000715BC
	public override void paint(mGraphics g)
	{
		GameCanvas.debug("PLG1", 1);
		GameCanvas.paintBGGameScr(g);
		GameCanvas.debug("PLG2", 2);
		int num = this.tfUser.y - 50;
		if (GameCanvas.h <= 220)
		{
			num += 5;
		}
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		if (mSystem.clientType == 1 && !GameCanvas.isTouch)
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		if (ChatPopup.serverChatPopUp != null)
		{
			return;
		}
		if (GameCanvas.currentDialog == null)
		{
			int h = 105;
			int w = (GameCanvas.w < 200) ? 160 : 180;
			PopUp.paintPopUp(g, this.xLog, this.yLog - 10, w, h, -1, true);
			if (GameCanvas.h > 160 && LoginScr.imgTitle != null)
			{
				g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
			}
			GameCanvas.debug("PLG4", 1);
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.tfPass.x = this.xLog + 10;
			this.tfPass.y = this.yLog + 55;
			this.tfUser.paint(g);
			this.tfPass.paint(g);
			if (GameCanvas.w < 176)
			{
				mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
			}
		}
		base.paint(g);
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00073488 File Offset: 0x00071888
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
		}
		else if (mSystem.clientType == 1 && GameCanvas.keyPressed[13])
		{
			GameCanvas.keyPressed[13] = false;
			this.cmdCallHotline.performAction();
		}
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		if (!GameCanvas.isTouch)
		{
			if (this.tfUser.isFocus)
			{
				this.right = this.tfUser.cmdClear;
			}
			else
			{
				this.right = this.tfPass.cmdClear;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			this.focus++;
			if (this.focus > 1)
			{
				this.focus = 0;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			GameCanvas.clearKeyPressed();
			if (!this.isLogin2 || this.isRes)
			{
				if (this.focus == 1)
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = true;
				}
				else if (this.focus == 0)
				{
					this.tfUser.isFocus = true;
					this.tfPass.isFocus = false;
				}
				else
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = false;
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (!this.isLogin2 || this.isRes)
			{
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
				}
				else if (GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height))
				{
					this.focus = 1;
				}
			}
		}
		if (Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null)
		{
			this.right.performAction();
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0007381B File Offset: 0x00071C1B
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00073828 File Offset: 0x00071C28
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 1000:
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception ex)
			{
			}
			GameCanvas.endDlg();
			break;
		case 1001:
			GameCanvas.endDlg();
			this.isRes = false;
			break;
		case 1002:
		{
			GameCanvas.startWaitDlg();
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
			break;
		}
		case 1003:
			GameCanvas.startOKDlg(mResources.goToWebForPassword);
			break;
		case 1004:
			ServerListScreen.doUpdateServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		case 1005:
			try
			{
				GameMidlet.instance.platformRequest("http://ngocrongonline.com");
			}
			catch (Exception ex2)
			{
			}
			break;
		default:
			switch (idAction)
			{
			case 2000:
				break;
			case 2001:
				if (this.isCheck)
				{
					this.isCheck = false;
				}
				else
				{
					this.isCheck = true;
				}
				break;
			case 2002:
				this.doRegister();
				break;
			case 2003:
				this.doMenu();
				break;
			case 2004:
				this.actRegister();
				break;
			default:
				if (idAction != 10041)
				{
					if (idAction != 10042)
					{
						if (idAction != 13)
						{
							if (idAction != 4000)
							{
								if (idAction == 10021)
								{
									this.actRegisterLeft();
								}
							}
							else
							{
								this.doRegister(this.tfUser.getText());
							}
						}
						else
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
					}
					else
					{
						Rms.saveRMSInt("lowGraphic", 1);
						GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					}
				}
				else
				{
					Rms.saveRMSInt("lowGraphic", 0);
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				}
				break;
			case 2008:
				Rms.saveRMSString("acc", this.tfUser.getText().Trim());
				Rms.saveRMSString("pass", this.tfPass.getText().Trim());
				if (ServerListScreen.loadScreen)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				else
				{
					GameCanvas.serverScreen.show2();
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00073B34 File Offset: 0x00071F34
	public void actRegisterLeft()
	{
		if (this.isLogin2)
		{
			this.doLogin();
			return;
		}
		this.isRes = false;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
		this.left = this.cmdMenu;
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00073B73 File Offset: 0x00071F73
	public void actRegister()
	{
		GameCanvas.endDlg();
		this.isRes = true;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00073B9C File Offset: 0x00071F9C
	public void backToRegister()
	{
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
		ServerListScreen.countDieConnect = 0;
		if (GameCanvas.loginScr.isLogin2)
		{
			GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
			return;
		}
		if (Main.isWindowsPhone)
		{
			GameMidlet.isBackWindowsPhone = true;
		}
		GameCanvas.instance.resetToLoginScr = false;
		GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
	}

	// Token: 0x04000F05 RID: 3845
	public TField tfUser;

	// Token: 0x04000F06 RID: 3846
	public TField tfPass;

	// Token: 0x04000F07 RID: 3847
	public static bool isContinueToLogin = false;

	// Token: 0x04000F08 RID: 3848
	private int focus;

	// Token: 0x04000F09 RID: 3849
	private int wC;

	// Token: 0x04000F0A RID: 3850
	private int yL;

	// Token: 0x04000F0B RID: 3851
	private int defYL;

	// Token: 0x04000F0C RID: 3852
	public bool isCheck;

	// Token: 0x04000F0D RID: 3853
	public bool isRes;

	// Token: 0x04000F0E RID: 3854
	public Command cmdLogin;

	// Token: 0x04000F0F RID: 3855
	public Command cmdCheck;

	// Token: 0x04000F10 RID: 3856
	public Command cmdFogetPass;

	// Token: 0x04000F11 RID: 3857
	public Command cmdRes;

	// Token: 0x04000F12 RID: 3858
	public Command cmdMenu;

	// Token: 0x04000F13 RID: 3859
	public Command cmdBackFromRegister;

	// Token: 0x04000F14 RID: 3860
	public string listFAQ = string.Empty;

	// Token: 0x04000F15 RID: 3861
	public string titleFAQ;

	// Token: 0x04000F16 RID: 3862
	public string subtitleFAQ;

	// Token: 0x04000F17 RID: 3863
	private string numSupport = string.Empty;

	// Token: 0x04000F18 RID: 3864
	public static bool isLocal = false;

	// Token: 0x04000F19 RID: 3865
	public static bool isUpdateAll;

	// Token: 0x04000F1A RID: 3866
	public static bool isUpdateData;

	// Token: 0x04000F1B RID: 3867
	public static bool isUpdateMap;

	// Token: 0x04000F1C RID: 3868
	public static bool isUpdateSkill;

	// Token: 0x04000F1D RID: 3869
	public static bool isUpdateItem;

	// Token: 0x04000F1E RID: 3870
	public static string serverName;

	// Token: 0x04000F1F RID: 3871
	public static Image imgTitle;

	// Token: 0x04000F20 RID: 3872
	public int plX;

	// Token: 0x04000F21 RID: 3873
	public int plY;

	// Token: 0x04000F22 RID: 3874
	public int lY;

	// Token: 0x04000F23 RID: 3875
	public int lX;

	// Token: 0x04000F24 RID: 3876
	public int logoDes;

	// Token: 0x04000F25 RID: 3877
	public int lineX;

	// Token: 0x04000F26 RID: 3878
	public int lineY;

	// Token: 0x04000F27 RID: 3879
	public static int[] bgId = new int[]
	{
		0,
		8,
		2,
		6,
		9
	};

	// Token: 0x04000F28 RID: 3880
	public static bool isTryGetIPFromWap;

	// Token: 0x04000F29 RID: 3881
	public static short timeLogin;

	// Token: 0x04000F2A RID: 3882
	public static long lastTimeLogin;

	// Token: 0x04000F2B RID: 3883
	public static long currTimeLogin;

	// Token: 0x04000F2C RID: 3884
	private int yt;

	// Token: 0x04000F2D RID: 3885
	private Command cmdSelect;

	// Token: 0x04000F2E RID: 3886
	private Command cmdOK;

	// Token: 0x04000F2F RID: 3887
	private int xLog;

	// Token: 0x04000F30 RID: 3888
	private int yLog;

	// Token: 0x04000F31 RID: 3889
	public static GameMidlet m;

	// Token: 0x04000F32 RID: 3890
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000F33 RID: 3891
	private int freeAreaHeight;

	// Token: 0x04000F34 RID: 3892
	private int xP;

	// Token: 0x04000F35 RID: 3893
	private int yP;

	// Token: 0x04000F36 RID: 3894
	private int wP;

	// Token: 0x04000F37 RID: 3895
	private int hP;

	// Token: 0x04000F38 RID: 3896
	private int t = 20;

	// Token: 0x04000F39 RID: 3897
	private bool isRegistering;

	// Token: 0x04000F3A RID: 3898
	private string passRe = string.Empty;

	// Token: 0x04000F3B RID: 3899
	public bool isFAQ;

	// Token: 0x04000F3C RID: 3900
	private int tipid = -1;

	// Token: 0x04000F3D RID: 3901
	public bool isLogin2;

	// Token: 0x04000F3E RID: 3902
	private int v = 2;

	// Token: 0x04000F3F RID: 3903
	private int g;

	// Token: 0x04000F40 RID: 3904
	private int ylogo = -40;

	// Token: 0x04000F41 RID: 3905
	private int dir = 1;

	// Token: 0x04000F42 RID: 3906
	private Command cmdCallHotline;

	// Token: 0x04000F43 RID: 3907
	public static bool isLoggingIn;
}
