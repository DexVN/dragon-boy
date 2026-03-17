using System;

// Token: 0x0200005F RID: 95
public class LoginScr : mScreen, IActionListener
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x0005A6D0 File Offset: 0x000588D0
	public LoginScr()
	{
		this.yLog = GameCanvas.hh - 30;
		TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
		bool flag = TileMap.bgID == 5 || TileMap.bgID == 6;
		if (flag)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		Main.closeKeyBoard();
		bool flag2 = GameCanvas.h > 200;
		if (flag2)
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
		bool flag3 = GameCanvas.h <= 160;
		if (flag3)
		{
			this.yt = 20;
		}
		this.tfUser = new TField();
		this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
		this.tfUser.width = this.wC;
		this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
		this.tfUser.isFocus = true;
		this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
		this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
		this.tfPass = new TField();
		this.tfPass.y = GameCanvas.hh - 4;
		this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		this.tfPass.width = this.wC;
		this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
		this.yt += 35;
		this.isCheck = true;
		int num2 = Rms.loadRMSInt("check");
		bool flag4 = num2 == 1;
		if (flag4)
		{
			this.isCheck = true;
		}
		else
		{
			bool flag5 = num2 == 2;
			if (flag5)
			{
				this.isCheck = false;
			}
		}
		this.tfUser.setText(Rms.loadRMSString("acc"));
		this.tfPass.setText(Rms.loadRMSString("pass"));
		bool flag6 = this.cmdCallHotline == null;
		if (flag6)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			bool flag7 = mSystem.clientType == 1 && !GameCanvas.isTouch;
			if (flag7)
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
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.cmdLogin.x = GameCanvas.w / 2 + 8;
			this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
			bool flag8 = GameCanvas.h >= 200;
			if (flag8)
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
		bool flag9 = num5 >= GameCanvas.w;
		if (flag9)
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

	// Token: 0x060004A5 RID: 1189 RVA: 0x0005AD10 File Offset: 0x00058F10
	public static void getServerLink()
	{
		try
		{
			bool flag = !LoginScr.isTryGetIPFromWap;
			if (flag)
			{
				Command command = new Command();
				ActionChat actionChat = delegate(string str)
				{
					try
					{
						bool flag2 = str == null;
						if (!flag2)
						{
							bool flag3 = str == string.Empty;
							if (!flag3)
							{
								Rms.saveIP(str);
								bool flag4 = !str.Contains(":");
								if (!flag4)
								{
									int num = str.IndexOf(":");
									string text = str.Substring(0, num);
									string s = str.Substring(num + 1);
									GameMidlet.IP = text;
									GameMidlet.PORT = int.Parse(s);
									Session_ME.gI().connect(text, int.Parse(s));
									LoginScr.isTryGetIPFromWap = true;
								}
							}
						}
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

	// Token: 0x060004A6 RID: 1190 RVA: 0x0005AD80 File Offset: 0x00058F80
	public override void switchToMe()
	{
		this.isRegistering = false;
		SoundMn.gI().stopAll();
		this.tfUser.isFocus = true;
		this.tfPass.isFocus = false;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.tfUser.isFocus = false;
		}
		GameCanvas.loadBG(0);
		base.switchToMe();
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0005ADE0 File Offset: 0x00058FE0
	public void setUserPass()
	{
		string text = Rms.loadRMSString("acc");
		bool flag = text != null && !text.Equals(string.Empty);
		if (flag)
		{
			this.tfUser.setText(text);
		}
		string text2 = Rms.loadRMSString("pass");
		bool flag2 = text2 != null && !text2.Equals(string.Empty);
		if (flag2)
		{
			this.tfPass.setText(text2);
		}
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00003136 File Offset: 0x00001336
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0005AE54 File Offset: 0x00059054
	protected void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
		bool flag = !this.isLogin2;
		if (flag)
		{
			myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
		}
		myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
		myVector.addElement(new Command(mResources.website, this, 1005, null));
		bool isPC = Main.isPC;
		if (isPC)
		{
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0005AF0C File Offset: 0x0005910C
	protected void doRegister()
	{
		bool flag = this.tfUser.getText().Equals(string.Empty);
		if (flag)
		{
			GameCanvas.startOKDlg(mResources.userBlank);
		}
		else
		{
			char[] array = this.tfUser.getText().ToCharArray();
			bool flag2 = this.tfPass.getText().Equals(string.Empty);
			if (flag2)
			{
				GameCanvas.startOKDlg(mResources.passwordBlank);
			}
			else
			{
				bool flag3 = this.tfUser.getText().Length < 5;
				if (flag3)
				{
					GameCanvas.startOKDlg(mResources.accTooShort);
				}
				else
				{
					int num = 0;
					string text = null;
					bool flag4 = mResources.language == 2;
					if (flag4)
					{
						bool flag5 = this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1;
						if (flag5)
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
							bool flag6 = this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84"));
							if (flag6)
							{
								text = mResources.phoneInvalid;
							}
							num = 1;
						}
						catch (Exception ex)
						{
							bool flag7 = this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1;
							if (flag7)
							{
								text = mResources.emailInvalid;
							}
							num = 0;
						}
					}
					bool flag8 = text != null;
					if (flag8)
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
			}
		}
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0005B1A8 File Offset: 0x000593A8
	protected void doRegister(string user)
	{
		this.isFAQ = false;
		GameCanvas.startWaitDlg(mResources.CONNECTING);
		GameCanvas.connect();
		GameCanvas.startWaitDlg(mResources.REGISTERING);
		this.passRe = this.tfPass.getText();
		Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect.ToString()), GameMidlet.VERSION);
		Rms.saveRMSString("acc", user);
		Rms.saveRMSString("pass", this.tfPass.getText());
		this.t = 20;
		this.isRegistering = true;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0005B26C File Offset: 0x0005946C
	public void doViewFAQ()
	{
		bool flag = !this.listFAQ.Equals(string.Empty) || !this.listFAQ.Equals(string.Empty);
		if (flag)
		{
		}
		bool flag2 = !Session_ME.connected;
		if (flag2)
		{
			this.isFAQ = true;
			GameCanvas.connect();
		}
		GameCanvas.startWaitDlg();
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0005B2CC File Offset: 0x000594CC
	protected void doSelectServer()
	{
		MyVector myVector = new MyVector();
		bool flag = LoginScr.isLocal;
		if (flag)
		{
			myVector.addElement(new Command("Server LOCAL", this, 20004, null));
		}
		myVector.addElement(new Command("Server Bokken", this, 20001, null));
		myVector.addElement(new Command("Server Shuriken", this, 20002, null));
		myVector.addElement(new Command("Server Tessen (mới)", this, 20003, null));
		GameCanvas.menu.startAt(myVector, 0);
		bool flag2 = this.loadIndexServer() != -1 && !GameCanvas.isTouch;
		if (flag2)
		{
			GameCanvas.menu.menuSelectedItem = this.loadIndexServer();
		}
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0005B382 File Offset: 0x00059582
	protected void saveIndexServer(int index)
	{
		Rms.saveRMSInt("indServer", index);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0005B394 File Offset: 0x00059594
	protected int loadIndexServer()
	{
		return Rms.loadRMSInt("indServer");
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0005B3B0 File Offset: 0x000595B0
	public void doLogin()
	{
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("pass");
		bool flag = text != null && !text.Equals(string.Empty);
		if (flag)
		{
			this.isLogin2 = false;
		}
		else
		{
			bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()).Equals(string.Empty);
			if (flag2)
			{
				this.isLogin2 = true;
			}
			else
			{
				this.isLogin2 = false;
			}
		}
		bool flag3 = (text == null || text.Equals(string.Empty)) && this.isLogin2;
		if (flag3)
		{
			text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			text2 = "a";
		}
		bool flag4 = text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty);
		if (!flag4)
		{
			bool flag5 = text2.Equals(string.Empty);
			if (flag5)
			{
				this.focus = 1;
				this.tfUser.isFocus = false;
				this.tfPass.isFocus = true;
				bool flag6 = !GameCanvas.isTouch;
				if (flag6)
				{
					this.right = this.tfPass.cmdClear;
				}
			}
			else
			{
				bool flag7 = !Session_ME.gI().isConnected();
				if (flag7)
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
				bool connected = Session_ME.connected;
				if (connected)
				{
					GameCanvas.startWaitDlg();
				}
				else
				{
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				}
				this.focus = 0;
				bool flag8 = !this.isLogin2;
				if (flag8)
				{
					this.actRegisterLeft();
				}
				GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
			}
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0005B5F4 File Offset: 0x000597F4
	public void savePass()
	{
		bool flag = this.isCheck;
		if (flag)
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

	// Token: 0x060004B2 RID: 1202 RVA: 0x0005B68C File Offset: 0x0005988C
	public override void update()
	{
		bool flag = Main.isWindowsPhone && this.isRegistering;
		if (flag)
		{
			bool flag2 = this.t < 0;
			if (flag2)
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
		bool flag3 = LoginScr.timeLogin > 0;
		if (flag3)
		{
			GameCanvas.startWaitDlg();
			LoginScr.currTimeLogin = mSystem.currentTimeMillis();
			bool flag4 = LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L;
			if (flag4)
			{
				LoginScr.timeLogin -= 1;
				bool flag5 = LoginScr.timeLogin == 0;
				if (flag5)
				{
					GameCanvas.loginScr.doLogin();
				}
				LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
			}
		}
		bool flag6 = this.isLogin2 && !this.isRes;
		if (flag6)
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		bool visible = TouchScreenKeyboard.visible;
		if (visible)
		{
			mGraphics.addYWhenOpenKeyBoard = 50;
		}
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			effect.update();
		}
		bool flag7 = LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill;
		if (flag7)
		{
			LoginScr.isUpdateAll = false;
			mSystem.gcc();
			Service.gI().finishUpdate();
		}
		GameScr.cmx++;
		bool flag8 = GameScr.cmx > GameCanvas.w * 3 + 100;
		if (flag8)
		{
			GameScr.cmx = 100;
		}
		bool flag9 = ChatPopup.currChatPopup != null;
		if (!flag9)
		{
			GameCanvas.debug("LGU1", 0);
			GameCanvas.debug("LGU2", 0);
			GameCanvas.debug("LGU3", 0);
			this.updateLogo();
			GameCanvas.debug("LGU4", 0);
			GameCanvas.debug("LGU5", 0);
			bool flag10 = this.g >= 0;
			if (flag10)
			{
				this.ylogo += this.dir * this.g;
				this.g += this.dir * this.v;
				bool flag11 = this.g <= 0;
				if (flag11)
				{
					this.dir *= -1;
				}
				bool flag12 = this.ylogo > 0;
				if (flag12)
				{
					this.dir *= -1;
					this.g -= 2 * this.v;
				}
			}
			GameCanvas.debug("LGU6", 0);
			bool flag13 = this.tipid >= 0 && GameCanvas.gameTick % 100 == 0;
			if (flag13)
			{
				this.doChangeTip();
			}
			bool flag14 = this.isLogin2 && !this.isRes;
			if (flag14)
			{
				this.tfUser.isPaintCarret = false;
				this.tfPass.isPaintCarret = false;
				this.tfUser.update();
				this.tfPass.update();
			}
			else
			{
				this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
				this.tfPass.name = mResources.password;
				this.tfUser.update();
				this.tfPass.update();
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				bool flag15 = this.isRes;
				if (flag15)
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
			else
			{
				bool flag16 = this.isRes;
				if (flag16)
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
			bool flag17 = !Main.isPC && !TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone;
			if (flag17)
			{
				string text = this.tfUser.getText().ToLower().Trim();
				string text2 = this.tfPass.getText().ToLower().Trim();
				bool flag18 = !text.Equals(string.Empty) && !text2.Equals(string.Empty);
				if (flag18)
				{
					this.doLogin();
				}
				Main.isMiniApp = true;
			}
			this.updateTfWhenOpenKb();
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0005BC0C File Offset: 0x00059E0C
	private void doChangeTip()
	{
		this.tipid++;
		bool flag = this.tipid >= mResources.tips.Length;
		if (flag)
		{
			this.tipid = 0;
		}
		bool flag2 = GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait;
		if (flag2)
		{
			GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0005BC80 File Offset: 0x00059E80
	public void updateLogo()
	{
		bool flag = this.defYL != this.yL;
		if (flag)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0005BCC4 File Offset: 0x00059EC4
	public override void keyPress(int keyCode)
	{
		bool isFocus = this.tfUser.isFocus;
		if (isFocus)
		{
			this.tfUser.keyPressed(keyCode);
		}
		else
		{
			bool isFocus2 = this.tfPass.isFocus;
			if (isFocus2)
			{
				this.tfPass.keyPressed(keyCode);
			}
		}
		base.keyPress(keyCode);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0005BD18 File Offset: 0x00059F18
	public override void unLoad()
	{
		base.unLoad();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0005BD24 File Offset: 0x00059F24
	public override void paint(mGraphics g)
	{
		GameCanvas.debug("PLG1", 1);
		GameCanvas.paintBGGameScr(g);
		GameCanvas.debug("PLG2", 2);
		int num = this.tfUser.y - 50;
		bool flag = GameCanvas.h <= 220;
		if (flag)
		{
			num += 5;
		}
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		bool flag2 = mSystem.clientType == 1 && !GameCanvas.isTouch;
		if (flag2)
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
		}
		bool flag3 = ChatPopup.currChatPopup != null;
		if (!flag3)
		{
			bool flag4 = ChatPopup.serverChatPopUp != null;
			if (!flag4)
			{
				bool flag5 = GameCanvas.currentDialog == null;
				if (flag5)
				{
					int h = 105;
					int w = (GameCanvas.w < 200) ? 160 : 180;
					PopUp.paintPopUp(g, this.xLog, this.yLog - 10, w, h, -1, true);
					bool flag6 = GameCanvas.h > 160 && LoginScr.imgTitle != null;
					if (flag6)
					{
						g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
					}
					GameCanvas.debug("PLG4", 1);
					int num2 = 4;
					int num3 = num2 * 32 + 23 + 33;
					bool flag7 = num3 >= GameCanvas.w;
					if (flag7)
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
					bool flag8 = GameCanvas.w < 176;
					if (flag8)
					{
						mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
						mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
						mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
					}
				}
				base.paint(g);
			}
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0005C02C File Offset: 0x0005A22C
	public override void updateKey()
	{
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			bool flag = this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside();
			if (flag)
			{
				this.cmdCallHotline.performAction();
			}
		}
		else
		{
			bool flag2 = mSystem.clientType == 1 && GameCanvas.keyPressed[13];
			if (flag2)
			{
				GameCanvas.keyPressed[13] = false;
				this.cmdCallHotline.performAction();
			}
		}
		bool flag3 = LoginScr.isContinueToLogin;
		if (!flag3)
		{
			bool flag4 = !GameCanvas.isTouch;
			if (flag4)
			{
				bool isFocus = this.tfUser.isFocus;
				if (isFocus)
				{
					this.right = this.tfUser.cmdClear;
				}
				else
				{
					this.right = this.tfPass.cmdClear;
				}
			}
			bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag5)
			{
				this.focus--;
				bool flag6 = this.focus < 0;
				if (flag6)
				{
					this.focus = 1;
				}
			}
			else
			{
				bool flag7 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16];
				if (flag7)
				{
					this.focus++;
					bool flag8 = this.focus > 1;
					if (flag8)
					{
						this.focus = 0;
					}
				}
			}
			bool flag9 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16];
			if (flag9)
			{
				GameCanvas.clearKeyPressed();
				bool flag10 = !this.isLogin2 || this.isRes;
				if (flag10)
				{
					bool flag11 = this.focus == 1;
					if (flag11)
					{
						this.tfUser.isFocus = false;
						this.tfPass.isFocus = true;
					}
					else
					{
						bool flag12 = this.focus == 0;
						if (flag12)
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
			}
			bool isTouch2 = GameCanvas.isTouch;
			if (isTouch2)
			{
				bool flag13 = this.isRes;
				if (flag13)
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
			else
			{
				bool flag14 = this.isRes;
				if (flag14)
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
			bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
			if (isPointerJustRelease)
			{
				bool flag15 = !this.isLogin2 || this.isRes;
				if (flag15)
				{
					bool flag16 = GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height);
					if (flag16)
					{
						this.focus = 0;
					}
					else
					{
						bool flag17 = GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height);
						if (flag17)
						{
							this.focus = 1;
						}
					}
				}
			}
			bool flag18 = Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null;
			if (flag18)
			{
				this.right.performAction();
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0005C3DA File Offset: 0x0005A5DA
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0005C3E8 File Offset: 0x0005A5E8
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
			string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			bool flag = text == null || text.Equals(string.Empty);
			if (flag)
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
		{
			switch (idAction)
			{
			case 2000:
				goto IL_32B;
			case 2001:
			{
				bool flag2 = this.isCheck;
				if (flag2)
				{
					this.isCheck = false;
				}
				else
				{
					this.isCheck = true;
				}
				goto IL_32B;
			}
			case 2002:
				this.doRegister();
				goto IL_32B;
			case 2003:
				this.doMenu();
				goto IL_32B;
			case 2004:
				this.actRegister();
				goto IL_32B;
			case 2008:
			{
				Rms.saveRMSString("acc", this.tfUser.getText().Trim());
				Rms.saveRMSString("pass", this.tfPass.getText().Trim());
				bool loadScreen = ServerListScreen.loadScreen;
				if (loadScreen)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				else
				{
					GameCanvas.serverScreen.show2();
				}
				goto IL_32B;
			}
			}
			bool flag3 = idAction != 10041;
			if (flag3)
			{
				bool flag4 = idAction != 10042;
				if (flag4)
				{
					bool flag5 = idAction != 13;
					if (flag5)
					{
						bool flag6 = idAction != 4000;
						if (flag6)
						{
							bool flag7 = idAction == 10021;
							if (flag7)
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
			IL_32B:
			break;
		}
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0005C740 File Offset: 0x0005A940
	public void actRegisterLeft()
	{
		bool flag = this.isLogin2;
		if (flag)
		{
			this.doLogin();
		}
		else
		{
			this.isRes = false;
			this.tfPass.isFocus = false;
			this.tfUser.isFocus = true;
			this.left = this.cmdMenu;
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0005C78D File Offset: 0x0005A98D
	public void actRegister()
	{
		GameCanvas.endDlg();
		this.isRes = true;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0005C7B8 File Offset: 0x0005A9B8
	public void backToRegister()
	{
		GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
		ServerListScreen.countDieConnect = 0;
		bool flag = GameCanvas.loginScr.isLogin2;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
		}
		else
		{
			bool isWindowsPhone = Main.isWindowsPhone;
			if (isWindowsPhone)
			{
				GameMidlet.isBackWindowsPhone = true;
			}
			GameCanvas.instance.resetToLoginScr = false;
			GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
		}
	}

	// Token: 0x04000A0D RID: 2573
	public TField tfUser;

	// Token: 0x04000A0E RID: 2574
	public TField tfPass;

	// Token: 0x04000A0F RID: 2575
	public static bool isContinueToLogin = false;

	// Token: 0x04000A10 RID: 2576
	private int focus;

	// Token: 0x04000A11 RID: 2577
	private int wC;

	// Token: 0x04000A12 RID: 2578
	private int yL;

	// Token: 0x04000A13 RID: 2579
	private int defYL;

	// Token: 0x04000A14 RID: 2580
	public bool isCheck;

	// Token: 0x04000A15 RID: 2581
	public bool isRes;

	// Token: 0x04000A16 RID: 2582
	public Command cmdLogin;

	// Token: 0x04000A17 RID: 2583
	public Command cmdCheck;

	// Token: 0x04000A18 RID: 2584
	public Command cmdFogetPass;

	// Token: 0x04000A19 RID: 2585
	public Command cmdRes;

	// Token: 0x04000A1A RID: 2586
	public Command cmdMenu;

	// Token: 0x04000A1B RID: 2587
	public Command cmdBackFromRegister;

	// Token: 0x04000A1C RID: 2588
	public string listFAQ = string.Empty;

	// Token: 0x04000A1D RID: 2589
	public string titleFAQ;

	// Token: 0x04000A1E RID: 2590
	public string subtitleFAQ;

	// Token: 0x04000A1F RID: 2591
	private string numSupport = string.Empty;

	// Token: 0x04000A20 RID: 2592
	public static bool isLocal = false;

	// Token: 0x04000A21 RID: 2593
	public static bool isUpdateAll;

	// Token: 0x04000A22 RID: 2594
	public static bool isUpdateData;

	// Token: 0x04000A23 RID: 2595
	public static bool isUpdateMap;

	// Token: 0x04000A24 RID: 2596
	public static bool isUpdateSkill;

	// Token: 0x04000A25 RID: 2597
	public static bool isUpdateItem;

	// Token: 0x04000A26 RID: 2598
	public static string serverName;

	// Token: 0x04000A27 RID: 2599
	public static Image imgTitle;

	// Token: 0x04000A28 RID: 2600
	public int plX;

	// Token: 0x04000A29 RID: 2601
	public int plY;

	// Token: 0x04000A2A RID: 2602
	public int lY;

	// Token: 0x04000A2B RID: 2603
	public int lX;

	// Token: 0x04000A2C RID: 2604
	public int logoDes;

	// Token: 0x04000A2D RID: 2605
	public int lineX;

	// Token: 0x04000A2E RID: 2606
	public int lineY;

	// Token: 0x04000A2F RID: 2607
	public static int[] bgId = new int[]
	{
		0,
		8,
		2,
		6,
		9
	};

	// Token: 0x04000A30 RID: 2608
	public static bool isTryGetIPFromWap;

	// Token: 0x04000A31 RID: 2609
	public static short timeLogin;

	// Token: 0x04000A32 RID: 2610
	public static long lastTimeLogin;

	// Token: 0x04000A33 RID: 2611
	public static long currTimeLogin;

	// Token: 0x04000A34 RID: 2612
	private int yt;

	// Token: 0x04000A35 RID: 2613
	private Command cmdSelect;

	// Token: 0x04000A36 RID: 2614
	private Command cmdOK;

	// Token: 0x04000A37 RID: 2615
	private int xLog;

	// Token: 0x04000A38 RID: 2616
	private int yLog;

	// Token: 0x04000A39 RID: 2617
	public static GameMidlet m;

	// Token: 0x04000A3A RID: 2618
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000A3B RID: 2619
	private int freeAreaHeight;

	// Token: 0x04000A3C RID: 2620
	private int xP;

	// Token: 0x04000A3D RID: 2621
	private int yP;

	// Token: 0x04000A3E RID: 2622
	private int wP;

	// Token: 0x04000A3F RID: 2623
	private int hP;

	// Token: 0x04000A40 RID: 2624
	private int t = 20;

	// Token: 0x04000A41 RID: 2625
	private bool isRegistering;

	// Token: 0x04000A42 RID: 2626
	private string passRe = string.Empty;

	// Token: 0x04000A43 RID: 2627
	public bool isFAQ;

	// Token: 0x04000A44 RID: 2628
	private int tipid = -1;

	// Token: 0x04000A45 RID: 2629
	public bool isLogin2;

	// Token: 0x04000A46 RID: 2630
	private int v = 2;

	// Token: 0x04000A47 RID: 2631
	private int g;

	// Token: 0x04000A48 RID: 2632
	private int ylogo = -40;

	// Token: 0x04000A49 RID: 2633
	private int dir = 1;

	// Token: 0x04000A4A RID: 2634
	private Command cmdCallHotline;

	// Token: 0x04000A4B RID: 2635
	public static bool isLoggingIn;
}
