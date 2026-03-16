using System;

namespace Assets.src.g
{
	// Token: 0x020000C1 RID: 193
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x000930F4 File Offset: 0x000914F4
		public RegisterScreen(sbyte haveName)
		{
			this.yLog = 130;
			TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
			if (TileMap.bgID == 5 || TileMap.bgID == 6)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
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
			this.tfSodt = new TField();
			this.tfSodt.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfSodt.width = 220;
			this.tfSodt.height = mScreen.ITEM_HEIGHT + 2;
			this.tfSodt.name = "Số điện thoại/ địa chỉ email";
			if ((int)haveName == 1)
			{
				this.tfSodt.setText("01234567890");
			}
			this.tfUser = new TField();
			this.tfUser.width = 220;
			this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
			this.tfUser.isFocus = true;
			this.tfUser.name = "Họ và tên";
			if ((int)haveName == 1)
			{
				this.tfUser.setText("Nguyễn Văn A");
			}
			this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNgay = new TField();
			this.tfNgay.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgay.width = 70;
			this.tfNgay.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgay.name = "Ngày sinh";
			if ((int)haveName == 1)
			{
				this.tfNgay.setText("01");
			}
			this.tfThang = new TField();
			this.tfThang.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfThang.width = 70;
			this.tfThang.height = mScreen.ITEM_HEIGHT + 2;
			this.tfThang.name = "Tháng sinh";
			if ((int)haveName == 1)
			{
				this.tfThang.setText("01");
			}
			this.tfNam = new TField();
			this.tfNam.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNam.width = 70;
			this.tfNam.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNam.name = "Năm sinh";
			if ((int)haveName == 1)
			{
				this.tfNam.setText("1990");
			}
			this.tfDiachi = new TField();
			this.tfDiachi.setIputType(TField.INPUT_TYPE_ANY);
			this.tfDiachi.width = 220;
			this.tfDiachi.height = mScreen.ITEM_HEIGHT + 2;
			this.tfDiachi.name = "Địa chỉ đăng ký thường trú";
			if ((int)haveName == 1)
			{
				this.tfDiachi.setText("123 đường số 1, Quận 1, TP.HCM");
			}
			this.tfCMND = new TField();
			this.tfCMND.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfCMND.width = 220;
			this.tfCMND.height = mScreen.ITEM_HEIGHT + 2;
			this.tfCMND.name = "Số Chứng minh nhân dân hoặc số hộ chiếu";
			if ((int)haveName == 1)
			{
				this.tfCMND.setText("123456789");
			}
			this.tfNgayCap = new TField();
			this.tfNgayCap.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgayCap.width = 220;
			this.tfNgayCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgayCap.name = "Ngày cấp";
			if ((int)haveName == 1)
			{
				this.tfNgayCap.setText("01/01/2005");
			}
			this.tfNoiCap = new TField();
			this.tfNoiCap.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNoiCap.width = 220;
			this.tfNoiCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNoiCap.name = "Nơi cấp";
			if ((int)haveName == 1)
			{
				this.tfNoiCap.setText("TP.HCM");
			}
			this.yt += 35;
			this.isCheck = true;
			this.focus = 0;
			this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
			this.cmdCheck = new Command(mResources.remember, this, 2001, null);
			this.cmdRes = new Command(mResources.register, this, 2002, null);
			this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
			this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
			if (GameCanvas.isTouch)
			{
				this.cmdLogin.x = GameCanvas.w / 2 - 100;
				this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
				if (GameCanvas.h >= 200)
				{
					this.cmdLogin.y = GameCanvas.h / 2 - 40;
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
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.yLog = 5;
			this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.cmdOK = new Command(mResources.OK, this, 2008, null);
			this.cmdOK.x = 260;
			this.cmdOK.y = GameCanvas.h - 60;
			this.cmdFogetPass = new Command("Thoát", this, 1003, null);
			this.cmdFogetPass.x = 260;
			this.cmdFogetPass.y = GameCanvas.h - 30;
			if (GameCanvas.w < 250)
			{
				this.cmdOK.x = GameCanvas.w / 2 - 80;
				this.cmdFogetPass.x = GameCanvas.w / 2 + 10;
				this.cmdFogetPass.y = (this.cmdOK.y = GameCanvas.h - 25);
			}
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000938F4 File Offset: 0x00091CF4
		public new void switchToMe()
		{
			Res.outz("Res switch");
			SoundMn.gI().stopAll();
			this.focus = 0;
			this.tfUser.isFocus = true;
			this.tfNgay.isFocus = false;
			if (GameCanvas.isTouch)
			{
				this.tfUser.isFocus = false;
				this.focus = -1;
			}
			base.switchToMe();
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00093958 File Offset: 0x00091D58
		protected void doMenu()
		{
			MyVector myVector = new MyVector("vMenu Login");
			myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
			if (!this.isLogin2)
			{
				myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
			}
			myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
			myVector.addElement(new Command(mResources.website, this, 1005, null));
			int num = Rms.loadRMSInt("lowGraphic");
			if (num == 1)
			{
				myVector.addElement(new Command(mResources.increase_vga, this, 10041, null));
			}
			else
			{
				myVector.addElement(new Command(mResources.decrease_vga, this, 10042, null));
			}
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00093A44 File Offset: 0x00091E44
		protected void doRegister()
		{
			if (this.tfUser.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.userBlank);
				return;
			}
			char[] array = this.tfUser.getText().ToCharArray();
			if (this.tfNgay.getText().Equals(string.Empty))
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
					this.tfNgay.getText()
				}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
			}
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00093CB4 File Offset: 0x000920B4
		protected void doRegister(string user)
		{
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00093CB8 File Offset: 0x000920B8
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

		// Token: 0x06000999 RID: 2457 RVA: 0x00093D0C File Offset: 0x0009210C
		protected void doSelectServer()
		{
			MyVector myVector = new MyVector("vServer");
			if (RegisterScreen.isLocal)
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

		// Token: 0x0600099A RID: 2458 RVA: 0x00093DBC File Offset: 0x000921BC
		protected void saveIndexServer(int index)
		{
			Rms.saveRMSInt("indServer", index);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00093DC9 File Offset: 0x000921C9
		protected int loadIndexServer()
		{
			return Rms.loadRMSInt("indServer");
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00093DD5 File Offset: 0x000921D5
		public void doLogin()
		{
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00093DD7 File Offset: 0x000921D7
		public void savePass()
		{
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00093DDC File Offset: 0x000921DC
		public override void update()
		{
			this.tfUser.update();
			this.tfNgay.update();
			this.tfThang.update();
			this.tfNam.update();
			this.tfDiachi.update();
			this.tfCMND.update();
			this.tfNoiCap.update();
			this.tfSodt.update();
			this.tfNgayCap.update();
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				effect.update();
			}
			if (RegisterScreen.isUpdateAll && !RegisterScreen.isUpdateData && !RegisterScreen.isUpdateItem && !RegisterScreen.isUpdateMap && !RegisterScreen.isUpdateSkill)
			{
				RegisterScreen.isUpdateAll = false;
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
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00094078 File Offset: 0x00092478
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

		// Token: 0x060009A0 RID: 2464 RVA: 0x000940E0 File Offset: 0x000924E0
		public void updateLogo()
		{
			if (this.defYL != this.yL)
			{
				this.yL += this.defYL - this.yL >> 1;
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00094110 File Offset: 0x00092510
		public override void keyPress(int keyCode)
		{
			if (this.tfUser.isFocus)
			{
				this.tfUser.keyPressed(keyCode);
			}
			else if (this.tfNgay.isFocus)
			{
				this.tfNgay.keyPressed(keyCode);
			}
			else if (this.tfThang.isFocus)
			{
				this.tfThang.keyPressed(keyCode);
			}
			else if (this.tfNam.isFocus)
			{
				this.tfNam.keyPressed(keyCode);
			}
			else if (this.tfDiachi.isFocus)
			{
				this.tfDiachi.keyPressed(keyCode);
			}
			else if (this.tfCMND.isFocus)
			{
				this.tfCMND.keyPressed(keyCode);
			}
			else if (this.tfNoiCap.isFocus)
			{
				this.tfNoiCap.keyPressed(keyCode);
			}
			else if (this.tfSodt.isFocus)
			{
				this.tfSodt.keyPressed(keyCode);
			}
			else if (this.tfNgayCap.isFocus)
			{
				this.tfNgayCap.keyPressed(keyCode);
			}
			base.keyPress(keyCode);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00094251 File Offset: 0x00092651
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0009425C File Offset: 0x0009265C
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
				this.xLog = 5;
				int num2 = 233;
				if (GameCanvas.w < 260)
				{
					this.xLog = (GameCanvas.w - 240) / 2;
				}
				this.yLog = (GameCanvas.h - num2) / 2;
				int num3 = (GameCanvas.w < 200) ? 160 : 180;
				PopUp.paintPopUp(g, this.xLog, this.yLog, 240, num2, -1, true);
				if (GameCanvas.h > 160 && RegisterScreen.imgTitle != null)
				{
					g.drawImage(RegisterScreen.imgTitle, GameCanvas.hw, num, 3);
				}
				GameCanvas.debug("PLG4", 1);
				int num4 = 4;
				int num5 = num4 * 32 + 23 + 33;
				if (num5 >= GameCanvas.w)
				{
					num4--;
					num5 = num4 * 32 + 23 + 33;
				}
				this.tfSodt.x = this.xLog + 10;
				this.tfSodt.y = this.yLog + 15;
				this.tfUser.x = this.tfSodt.x;
				this.tfUser.y = this.tfSodt.y + 30;
				this.tfNgay.x = this.xLog + 10;
				this.tfNgay.y = this.tfUser.y + 30;
				this.tfThang.x = this.tfNgay.x + 75;
				this.tfThang.y = this.tfNgay.y;
				this.tfNam.x = this.tfThang.x + 75;
				this.tfNam.y = this.tfThang.y;
				this.tfDiachi.x = this.tfUser.x;
				this.tfDiachi.y = this.tfNgay.y + 30;
				this.tfCMND.x = this.tfUser.x;
				this.tfCMND.y = this.tfDiachi.y + 30;
				this.tfNgayCap.x = this.tfUser.x;
				this.tfNgayCap.y = this.tfCMND.y + 30;
				this.tfNoiCap.x = this.tfUser.x;
				this.tfNoiCap.y = this.tfNgayCap.y + 30;
				this.tfUser.paint(g);
				this.tfNgay.paint(g);
				this.tfThang.paint(g);
				this.tfNam.paint(g);
				this.tfDiachi.paint(g);
				this.tfCMND.paint(g);
				this.tfNgayCap.paint(g);
				this.tfNoiCap.paint(g);
				this.tfSodt.paint(g);
				if (GameCanvas.w < 176)
				{
					mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfNgay.x - 35, this.tfNgay.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.server + ": " + RegisterScreen.serverName, GameCanvas.w / 2, this.tfNgay.y + 32, 2);
					if (this.isRes)
					{
					}
				}
			}
			string version = GameMidlet.VERSION;
			g.setColor(GameCanvas.skyColor);
			g.fillRect(GameCanvas.w - 40, 4, 36, 11);
			mFont.tahoma_7_grey.drawString(g, version, GameCanvas.w - 22, 4, mFont.CENTER);
			GameCanvas.resetTrans(g);
			if (GameCanvas.currentDialog == null)
			{
				if (GameCanvas.w > 250)
				{
					mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi", 260, 10, 0, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "chỉ có thể chơi", 260, 25, 0, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", 260, 40, 0, mFont.tahoma_7b_dark);
				}
				else
				{
					mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi chỉ có thể chơi", GameCanvas.w / 2, 5, 2, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", GameCanvas.w / 2, 15, 2, mFont.tahoma_7b_dark);
				}
			}
			base.paint(g);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00094774 File Offset: 0x00092B74
		private void turnOffFocus()
		{
			this.tfUser.isFocus = false;
			this.tfNgay.isFocus = false;
			this.tfThang.isFocus = false;
			this.tfNam.isFocus = false;
			this.tfDiachi.isFocus = false;
			this.tfCMND.isFocus = false;
			this.tfNgayCap.isFocus = false;
			this.tfNoiCap.isFocus = false;
			this.tfSodt.isFocus = false;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000947F0 File Offset: 0x00092BF0
		private void processFocus()
		{
			this.turnOffFocus();
			switch (this.focus)
			{
			case 0:
				this.tfUser.isFocus = true;
				break;
			case 1:
				this.tfNgay.isFocus = true;
				break;
			case 2:
				this.tfThang.isFocus = true;
				break;
			case 3:
				this.tfNam.isFocus = true;
				break;
			case 4:
				this.tfDiachi.isFocus = true;
				break;
			case 5:
				this.tfCMND.isFocus = true;
				break;
			case 6:
				this.tfNgayCap.isFocus = true;
				break;
			case 7:
				this.tfNoiCap.isFocus = true;
				break;
			case 8:
				this.tfSodt.isFocus = true;
				break;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000948D4 File Offset: 0x00092CD4
		public override void updateKey()
		{
			if (RegisterScreen.isContinueToLogin)
			{
				return;
			}
			if (!GameCanvas.isTouch)
			{
				if (this.tfUser.isFocus)
				{
					this.right = this.tfUser.cmdClear;
				}
				else if (this.tfNgay.isFocus)
				{
					this.right = this.tfNgay.cmdClear;
				}
				else if (this.tfThang.isFocus)
				{
					this.right = this.tfThang.cmdClear;
				}
				else if (this.tfNam.isFocus)
				{
					this.right = this.tfNam.cmdClear;
				}
				else if (this.tfDiachi.isFocus)
				{
					this.right = this.tfDiachi.cmdClear;
				}
				else if (this.tfCMND.isFocus)
				{
					this.right = this.tfCMND.cmdClear;
				}
				else if (this.tfNgayCap.isFocus)
				{
					this.right = this.tfNgayCap.cmdClear;
				}
				else if (this.tfNoiCap.isFocus)
				{
					this.right = this.tfNoiCap.cmdClear;
				}
				else if (this.tfSodt.isFocus)
				{
					this.right = this.tfSodt.cmdClear;
				}
			}
			if (GameCanvas.keyPressed[21])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = 8;
				}
				this.processFocus();
			}
			else if (GameCanvas.keyPressed[22])
			{
				this.focus++;
				if (this.focus > 8)
				{
					this.focus = 0;
				}
				this.processFocus();
			}
			if (GameCanvas.keyPressed[21] || GameCanvas.keyPressed[22])
			{
				GameCanvas.clearKeyPressed();
				if (!this.isLogin2 || this.isRes)
				{
					if (this.focus == 1)
					{
						this.tfUser.isFocus = false;
						this.tfNgay.isFocus = true;
					}
					else if (this.focus == 0)
					{
						this.tfUser.isFocus = true;
						this.tfNgay.isFocus = false;
					}
					else
					{
						this.tfUser.isFocus = false;
						this.tfNgay.isFocus = false;
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
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNgay.x, this.tfNgay.y, this.tfNgay.width, this.tfNgay.height))
				{
					this.focus = 1;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfThang.x, this.tfThang.y, this.tfThang.width, this.tfThang.height))
				{
					this.focus = 2;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNam.x, this.tfNam.y, this.tfNam.width, this.tfNam.height))
				{
					this.focus = 3;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfDiachi.x, this.tfDiachi.y, this.tfDiachi.width, this.tfDiachi.height))
				{
					this.focus = 4;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfCMND.x, this.tfCMND.y, this.tfCMND.width, this.tfCMND.height))
				{
					this.focus = 5;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNgayCap.x, this.tfNgayCap.y, this.tfNgayCap.width, this.tfNgayCap.height))
				{
					this.focus = 6;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNoiCap.x, this.tfNoiCap.y, this.tfNoiCap.width, this.tfNoiCap.height))
				{
					this.focus = 7;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfSodt.x, this.tfSodt.y, this.tfSodt.width, this.tfSodt.height))
				{
					this.focus = 8;
					this.processFocus();
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00094E7E File Offset: 0x0009327E
		public void resetLogo()
		{
			this.yL = -50;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00094E88 File Offset: 0x00093288
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
					ex.StackTrace.ToString();
				}
				GameCanvas.endDlg();
				break;
			case 1001:
				GameCanvas.endDlg();
				this.isRes = false;
				break;
			case 1002:
				break;
			case 1003:
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
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
					ex2.StackTrace.ToString();
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
					}
					break;
				case 2008:
					if (this.tfNgay.getText().Equals(string.Empty) || this.tfThang.getText().Equals(string.Empty) || this.tfNam.getText().Equals(string.Empty) || this.tfDiachi.getText().Equals(string.Empty) || this.tfCMND.getText().Equals(string.Empty) || this.tfNgayCap.getText().Equals(string.Empty) || this.tfNoiCap.getText().Equals(string.Empty) || this.tfSodt.getText().Equals(string.Empty) || this.tfUser.getText().Equals(string.Empty))
					{
						GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
					}
					else
					{
						GameCanvas.startOKDlg(mResources.PLEASEWAIT);
						Service.gI().charInfo(this.tfNgay.getText(), this.tfThang.getText(), this.tfNam.getText(), this.tfDiachi.getText(), this.tfCMND.getText(), this.tfNgayCap.getText(), this.tfNoiCap.getText(), this.tfSodt.getText(), this.tfUser.getText());
					}
					break;
				}
				break;
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000951C0 File Offset: 0x000935C0
		public void actRegisterLeft()
		{
			if (this.isLogin2)
			{
				this.doLogin();
				return;
			}
			this.isRes = false;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
			this.left = this.cmdMenu;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000951FF File Offset: 0x000935FF
		public void actRegister()
		{
			GameCanvas.endDlg();
			GameCanvas.startOKDlg(mResources.regNote);
			this.isRes = true;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00095230 File Offset: 0x00093630
		public void backToRegister()
		{
			if (GameCanvas.loginScr.isLogin2)
			{
				GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
				return;
			}
			GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
			Session_ME.gI().close();
		}

		// Token: 0x040011CF RID: 4559
		public TField tfUser;

		// Token: 0x040011D0 RID: 4560
		public TField tfNgay;

		// Token: 0x040011D1 RID: 4561
		public TField tfThang;

		// Token: 0x040011D2 RID: 4562
		public TField tfNam;

		// Token: 0x040011D3 RID: 4563
		public TField tfDiachi;

		// Token: 0x040011D4 RID: 4564
		public TField tfCMND;

		// Token: 0x040011D5 RID: 4565
		public TField tfNgayCap;

		// Token: 0x040011D6 RID: 4566
		public TField tfNoiCap;

		// Token: 0x040011D7 RID: 4567
		public TField tfSodt;

		// Token: 0x040011D8 RID: 4568
		public static bool isContinueToLogin = false;

		// Token: 0x040011D9 RID: 4569
		private int focus;

		// Token: 0x040011DA RID: 4570
		private int wC;

		// Token: 0x040011DB RID: 4571
		private int yL;

		// Token: 0x040011DC RID: 4572
		private int defYL;

		// Token: 0x040011DD RID: 4573
		public bool isCheck;

		// Token: 0x040011DE RID: 4574
		public bool isRes;

		// Token: 0x040011DF RID: 4575
		private Command cmdLogin;

		// Token: 0x040011E0 RID: 4576
		private Command cmdCheck;

		// Token: 0x040011E1 RID: 4577
		private Command cmdFogetPass;

		// Token: 0x040011E2 RID: 4578
		private Command cmdRes;

		// Token: 0x040011E3 RID: 4579
		private Command cmdMenu;

		// Token: 0x040011E4 RID: 4580
		private Command cmdBackFromRegister;

		// Token: 0x040011E5 RID: 4581
		public string listFAQ = string.Empty;

		// Token: 0x040011E6 RID: 4582
		public string titleFAQ;

		// Token: 0x040011E7 RID: 4583
		public string subtitleFAQ;

		// Token: 0x040011E8 RID: 4584
		private string numSupport = string.Empty;

		// Token: 0x040011E9 RID: 4585
		private string strUser;

		// Token: 0x040011EA RID: 4586
		private string strPass;

		// Token: 0x040011EB RID: 4587
		public static bool isLocal = false;

		// Token: 0x040011EC RID: 4588
		public static bool isUpdateAll;

		// Token: 0x040011ED RID: 4589
		public static bool isUpdateData;

		// Token: 0x040011EE RID: 4590
		public static bool isUpdateMap;

		// Token: 0x040011EF RID: 4591
		public static bool isUpdateSkill;

		// Token: 0x040011F0 RID: 4592
		public static bool isUpdateItem;

		// Token: 0x040011F1 RID: 4593
		public static string serverName;

		// Token: 0x040011F2 RID: 4594
		public static Image imgTitle;

		// Token: 0x040011F3 RID: 4595
		public int plX;

		// Token: 0x040011F4 RID: 4596
		public int plY;

		// Token: 0x040011F5 RID: 4597
		public int lY;

		// Token: 0x040011F6 RID: 4598
		public int lX;

		// Token: 0x040011F7 RID: 4599
		public int logoDes;

		// Token: 0x040011F8 RID: 4600
		public int lineX;

		// Token: 0x040011F9 RID: 4601
		public int lineY;

		// Token: 0x040011FA RID: 4602
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x040011FB RID: 4603
		public static bool isTryGetIPFromWap;

		// Token: 0x040011FC RID: 4604
		public static short timeLogin;

		// Token: 0x040011FD RID: 4605
		public static long lastTimeLogin;

		// Token: 0x040011FE RID: 4606
		public static long currTimeLogin;

		// Token: 0x040011FF RID: 4607
		private int yt;

		// Token: 0x04001200 RID: 4608
		private Command cmdSelect;

		// Token: 0x04001201 RID: 4609
		private Command cmdOK;

		// Token: 0x04001202 RID: 4610
		private int xLog;

		// Token: 0x04001203 RID: 4611
		private int yLog;

		// Token: 0x04001204 RID: 4612
		private int xP;

		// Token: 0x04001205 RID: 4613
		private int yP;

		// Token: 0x04001206 RID: 4614
		private int wP;

		// Token: 0x04001207 RID: 4615
		private int hP;

		// Token: 0x04001208 RID: 4616
		private string passRe = string.Empty;

		// Token: 0x04001209 RID: 4617
		public bool isFAQ;

		// Token: 0x0400120A RID: 4618
		private int tipid = -1;

		// Token: 0x0400120B RID: 4619
		public bool isLogin2;

		// Token: 0x0400120C RID: 4620
		private int v = 2;

		// Token: 0x0400120D RID: 4621
		private int g;

		// Token: 0x0400120E RID: 4622
		private int ylogo = -40;

		// Token: 0x0400120F RID: 4623
		private int dir = 1;

		// Token: 0x04001210 RID: 4624
		public static bool isLoggingIn;
	}
}
