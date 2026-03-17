using System;

// Token: 0x02000066 RID: 102
public class Menu
{
	// Token: 0x060004E2 RID: 1250 RVA: 0x0005DABE File Offset: 0x0005BCBE
	public static void loadBg()
	{
		Menu.imgMenu1 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu1.png");
		Menu.imgMenu2 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu2.png");
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0005DADF File Offset: 0x0005BCDF
	public void startWithoutCloseButton(MyVector menuItems, int pos)
	{
		this.startAt(menuItems, pos);
		this.disableClose = true;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
	public void startAt(MyVector menuItems, int x, int y)
	{
		this.startAt(menuItems, 0);
		this.menuX = x;
		this.menuY = y;
		while (this.menuY + this.menuH > GameCanvas.h)
		{
			this.menuY -= 2;
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0005DB44 File Offset: 0x0005BD44
	public void startAt(MyVector menuItems, int pos)
	{
		bool flag = this.showMenu;
		if (!flag)
		{
			this.isClose = false;
			this.touch = false;
			this.close = false;
			this.tDelay = 0;
			bool flag2 = menuItems.size() == 1;
			if (flag2)
			{
				this.menuSelectedItem = 0;
				Command command = (Command)menuItems.elementAt(0);
				bool flag3 = command != null && command.caption.Equals(mResources.saying);
				if (flag3)
				{
					command.performAction();
					this.showMenu = false;
					InfoDlg.showWait();
					return;
				}
			}
			SoundMn.gI().openMenu();
			this.isNotClose = new bool[menuItems.size()];
			for (int i = 0; i < this.isNotClose.Length; i++)
			{
				this.isNotClose[i] = false;
			}
			this.disableClose = false;
			ChatPopup.currChatPopup = null;
			Effect2.vEffect2.removeAllElements();
			Effect2.vEffect2Outside.removeAllElements();
			InfoDlg.hide();
			bool flag4 = menuItems.size() == 0;
			if (!flag4)
			{
				this.menuItems = menuItems;
				this.menuW = 60;
				this.menuH = 60;
				for (int j = 0; j < menuItems.size(); j++)
				{
					Command command2 = (Command)menuItems.elementAt(j);
					command2.isPlaySoundButton = false;
					int width = mFont.tahoma_7_yellow.getWidth(command2.caption);
					command2.subCaption = mFont.tahoma_7_yellow.splitFontArray(command2.caption, this.menuW - 10);
				}
				Menu.menuTemY = new int[menuItems.size()];
				this.menuX = (GameCanvas.w - menuItems.size() * this.menuW) / 2;
				bool flag5 = this.menuX < 1;
				if (flag5)
				{
					this.menuX = 1;
				}
				this.menuY = GameCanvas.h - this.menuH - (Paint.hTab + 1) - 1;
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					this.menuY -= 3;
				}
				this.menuY += 27;
				for (int k = 0; k < Menu.menuTemY.Length; k++)
				{
					Menu.menuTemY[k] = GameCanvas.h;
				}
				this.showMenu = true;
				this.menuSelectedItem = 0;
				Menu.cmxLim = this.menuItems.size() * this.menuW - GameCanvas.w;
				bool flag6 = Menu.cmxLim < 0;
				if (flag6)
				{
					Menu.cmxLim = 0;
				}
				Menu.cmtoX = 0;
				Menu.cmx = 0;
				Menu.xc = 50;
				this.w = menuItems.size() * this.menuW - 1;
				bool flag7 = this.w > GameCanvas.w - 2;
				if (flag7)
				{
					this.w = GameCanvas.w - 2;
				}
				bool flag8 = GameCanvas.isTouch && !Main.isPC;
				if (flag8)
				{
					this.menuSelectedItem = -1;
				}
			}
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0005DE3C File Offset: 0x0005C03C
	public bool isScrolling()
	{
		return (!this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] > this.menuY) || (this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] < GameCanvas.h);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0005DE94 File Offset: 0x0005C094
	public void updateMenuKey()
	{
		bool flag3 = GameScr.gI().activeRongThan && GameScr.gI().isUseFreez;
		if (!flag3)
		{
			bool flag4 = !this.showMenu;
			if (!flag4)
			{
				bool flag5 = this.isScrolling();
				if (!flag5)
				{
					bool flag = false;
					bool flag6 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
					if (flag6)
					{
						flag = true;
						this.menuSelectedItem--;
						bool flag7 = this.menuSelectedItem < 0;
						if (flag7)
						{
							this.menuSelectedItem = this.menuItems.size() - 1;
						}
					}
					else
					{
						bool flag8 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
						if (flag8)
						{
							flag = true;
							this.menuSelectedItem++;
							bool flag9 = this.menuSelectedItem > this.menuItems.size() - 1;
							if (flag9)
							{
								this.menuSelectedItem = 0;
							}
						}
						else
						{
							bool flag10 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
							if (flag10)
							{
								bool flag11 = this.center != null;
								if (flag11)
								{
									bool flag12 = this.center.idAction > 0;
									if (flag12)
									{
										bool flag13 = this.center.actionListener == GameScr.gI();
										if (flag13)
										{
											GameScr.gI().actionPerform(this.center.idAction, this.center.p);
										}
										else
										{
											this.perform(this.center.idAction, this.center.p);
										}
									}
								}
								else
								{
									this.waitToPerform = 2;
								}
							}
							else
							{
								bool flag14 = GameCanvas.keyPressed[12] && !GameScr.gI().isRongThanMenu();
								if (flag14)
								{
									bool flag15 = this.isScrolling();
									if (flag15)
									{
										return;
									}
									bool flag16 = this.left.idAction > 0;
									if (flag16)
									{
										this.perform(this.left.idAction, this.left.p);
									}
									else
									{
										this.waitToPerform = 2;
									}
									SoundMn.gI().buttonClose();
								}
								else
								{
									bool flag17 = !GameScr.gI().isRongThanMenu() && !this.disableClose && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right));
									if (flag17)
									{
										bool flag18 = this.isScrolling();
										if (flag18)
										{
											return;
										}
										bool flag19 = !this.close;
										if (flag19)
										{
											this.close = true;
										}
										this.isClose = true;
										SoundMn.gI().buttonClose();
									}
								}
							}
						}
					}
					bool flag20 = flag;
					if (flag20)
					{
						Menu.cmtoX = this.menuSelectedItem * this.menuW + this.menuW - GameCanvas.w / 2;
						bool flag21 = Menu.cmtoX > Menu.cmxLim;
						if (flag21)
						{
							Menu.cmtoX = Menu.cmxLim;
						}
						bool flag22 = Menu.cmtoX < 0;
						if (flag22)
						{
							Menu.cmtoX = 0;
						}
						bool flag23 = this.menuSelectedItem == this.menuItems.size() - 1 || this.menuSelectedItem == 0;
						if (flag23)
						{
							Menu.cmx = Menu.cmtoX;
						}
					}
					bool flag2 = true;
					bool flag24 = GameCanvas.panel.cp != null && GameCanvas.panel.cp.isClip;
					if (flag24)
					{
						bool flag25 = !GameCanvas.isPointerHoldIn(GameCanvas.panel.cp.cx, 0, GameCanvas.panel.cp.sayWidth + 2, GameCanvas.panel.cp.ch);
						if (flag25)
						{
							flag2 = true;
						}
						else
						{
							flag2 = false;
							GameCanvas.panel.cp.updateKey();
						}
					}
					bool flag26 = this.disableClose || !GameCanvas.isPointerJustRelease || GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH) || this.pointerIsDowning || GameScr.gI().isRongThanMenu() || !flag2;
					if (flag26)
					{
						bool isPointerDown = GameCanvas.isPointerDown;
						if (isPointerDown)
						{
							bool flag27 = !this.pointerIsDowning && GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH);
							if (flag27)
							{
								for (int i = 0; i < this.pointerDownLastX.Length; i++)
								{
									this.pointerDownLastX[0] = GameCanvas.px;
								}
								this.pointerDownFirstX = GameCanvas.px;
								this.pointerIsDowning = true;
								this.isDownWhenRunning = (this.cmRun != 0);
								this.cmRun = 0;
							}
							else
							{
								bool flag28 = this.pointerIsDowning;
								if (flag28)
								{
									this.pointerDownTime++;
									bool flag29 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning;
									if (flag29)
									{
										this.pointerDownFirstX = -1000;
										this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
									}
									int num = GameCanvas.px - this.pointerDownLastX[0];
									bool flag30 = num != 0 && this.menuSelectedItem != -1;
									if (flag30)
									{
										this.menuSelectedItem = -1;
									}
									for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
									{
										this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
									}
									this.pointerDownLastX[0] = GameCanvas.px;
									Menu.cmtoX -= num;
									bool flag31 = Menu.cmtoX < 0;
									if (flag31)
									{
										Menu.cmtoX = 0;
									}
									bool flag32 = Menu.cmtoX > Menu.cmxLim;
									if (flag32)
									{
										Menu.cmtoX = Menu.cmxLim;
									}
									bool flag33 = Menu.cmx < 0 || Menu.cmx > Menu.cmxLim;
									if (flag33)
									{
										num /= 2;
									}
									Menu.cmx -= num;
									bool flag34 = Menu.cmx < -(GameCanvas.h / 3);
									if (flag34)
									{
										this.wantUpdateList = true;
									}
									else
									{
										this.wantUpdateList = false;
									}
								}
							}
						}
						bool flag35 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
						if (flag35)
						{
							int i2 = GameCanvas.px - this.pointerDownLastX[0];
							GameCanvas.isPointerJustRelease = false;
							bool flag36 = Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
							if (flag36)
							{
								this.cmRun = 0;
								Menu.cmtoX = Menu.cmx;
								this.pointerDownFirstX = -1000;
								this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
								this.pointerDownTime = 0;
								this.waitToPerform = 10;
							}
							else
							{
								bool flag37 = this.menuSelectedItem != -1 && this.pointerDownTime > 5;
								if (flag37)
								{
									this.pointerDownTime = 0;
									this.waitToPerform = 1;
								}
								else
								{
									bool flag38 = this.menuSelectedItem == -1 && !this.isDownWhenRunning;
									if (flag38)
									{
										bool flag39 = Menu.cmx < 0;
										if (flag39)
										{
											Menu.cmtoX = 0;
										}
										else
										{
											bool flag40 = Menu.cmx > Menu.cmxLim;
											if (flag40)
											{
												Menu.cmtoX = Menu.cmxLim;
											}
											else
											{
												int num2 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
												bool flag41 = num2 > 10;
												if (flag41)
												{
													num2 = 10;
												}
												else
												{
													bool flag42 = num2 < -10;
													if (flag42)
													{
														num2 = -10;
													}
													else
													{
														num2 = 0;
													}
												}
												this.cmRun = -num2 * 100;
											}
										}
									}
								}
							}
							this.pointerIsDowning = false;
							this.pointerDownTime = 0;
							GameCanvas.isPointerJustRelease = false;
						}
						GameCanvas.clearKeyPressed();
						GameCanvas.clearKeyHold();
					}
					else
					{
						bool flag43 = this.isScrolling();
						if (!flag43)
						{
							this.pointerDownTime = (this.pointerDownFirstX = 0);
							this.pointerIsDowning = false;
							GameCanvas.clearAllPointerEvent();
							Res.outz("menu select= " + this.menuSelectedItem.ToString());
							this.isClose = true;
							this.close = true;
							SoundMn.gI().buttonClose();
						}
					}
				}
			}
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0005E718 File Offset: 0x0005C918
	public void moveCamera()
	{
		bool flag = this.cmRun != 0 && !this.pointerIsDowning;
		if (flag)
		{
			Menu.cmtoX += this.cmRun / 100;
			bool flag2 = Menu.cmtoX < 0;
			if (flag2)
			{
				Menu.cmtoX = 0;
			}
			else
			{
				bool flag3 = Menu.cmtoX > Menu.cmxLim;
				if (flag3)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				else
				{
					Menu.cmx = Menu.cmtoX;
				}
			}
			this.cmRun = this.cmRun * 9 / 10;
			bool flag4 = this.cmRun < 100 && this.cmRun > -100;
			if (flag4)
			{
				this.cmRun = 0;
			}
		}
		bool flag5 = Menu.cmx != Menu.cmtoX && !this.pointerIsDowning;
		if (flag5)
		{
			this.cmvx = Menu.cmtoX - Menu.cmx << 2;
			this.cmdx += this.cmvx;
			Menu.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0005E838 File Offset: 0x0005CA38
	public void paintMenu(mGraphics g)
	{
		bool flag = GameScr.gI().activeRongThan && GameScr.gI().isUseFreez;
		if (!flag)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.translate(-Menu.cmx, 0);
			for (int i = 0; i < this.menuItems.size(); i++)
			{
				bool flag2 = i == this.menuSelectedItem;
				if (flag2)
				{
					g.drawImage(Menu.imgMenu2, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
				}
				else
				{
					g.drawImage(Menu.imgMenu1, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
				}
				Command command = (Command)this.menuItems.elementAt(i);
				string[] array = command.subCaption;
				bool flag3 = array == null;
				if (flag3)
				{
					array = new string[]
					{
						((Command)this.menuItems.elementAt(i)).caption
					};
				}
				int num = Menu.menuTemY[i] + (this.menuH - array.Length * 14) / 2 + 1;
				for (int j = 0; j < array.Length; j++)
				{
					bool flag4 = i == this.menuSelectedItem;
					if (flag4)
					{
						mFont.tahoma_7b_green2.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
					}
					else
					{
						bool isDisplay = command.isDisplay;
						if (isDisplay)
						{
							mFont.tahoma_7b_red.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
						}
						else
						{
							mFont.tahoma_7b_dark.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
						}
					}
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0005EA70 File Offset: 0x0005CC70
	public void doCloseMenu()
	{
		Res.outz("CLOSE MENU");
		this.isClose = false;
		this.showMenu = false;
		InfoDlg.hide();
		bool flag = this.close;
		if (flag)
		{
			GameCanvas.panel.cp = null;
			global::Char.chatPopup = null;
			bool flag2 = GameCanvas.panel2 != null && GameCanvas.panel2.cp != null;
			if (flag2)
			{
				GameCanvas.panel2.cp = null;
			}
		}
		else
		{
			bool flag3 = this.touch;
			if (flag3)
			{
				GameCanvas.panel.cp = null;
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.cp != null;
				if (flag4)
				{
					GameCanvas.panel2.cp = null;
				}
				bool flag5 = this.menuSelectedItem >= 0;
				if (flag5)
				{
					Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
					bool flag6 = command != null;
					if (flag6)
					{
						SoundMn.gI().buttonClose();
						command.performAction();
					}
				}
			}
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0005EB78 File Offset: 0x0005CD78
	public void performSelect()
	{
		InfoDlg.hide();
		bool flag = this.menuSelectedItem >= 0;
		if (flag)
		{
			Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
			bool flag2 = command != null;
			if (flag2)
			{
				command.performAction();
			}
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0005EBC8 File Offset: 0x0005CDC8
	public void updateMenu()
	{
		this.moveCamera();
		bool flag = !this.isClose;
		if (flag)
		{
			this.tDelay++;
			for (int i = 0; i < Menu.menuTemY.Length; i++)
			{
				bool flag2 = Menu.menuTemY[i] > this.menuY;
				if (flag2)
				{
					int num = Menu.menuTemY[i] - this.menuY >> 1;
					bool flag3 = num < 1;
					if (flag3)
					{
						num = 1;
					}
					bool flag4 = this.tDelay > i;
					if (flag4)
					{
						Menu.menuTemY[i] -= num;
					}
				}
			}
			bool flag5 = Menu.menuTemY[Menu.menuTemY.Length - 1] <= this.menuY;
			if (flag5)
			{
				this.tDelay = 0;
			}
		}
		else
		{
			this.tDelay++;
			for (int j = 0; j < Menu.menuTemY.Length; j++)
			{
				bool flag6 = Menu.menuTemY[j] < GameCanvas.h;
				if (flag6)
				{
					int num2 = (GameCanvas.h - Menu.menuTemY[j] >> 1) + 2;
					bool flag7 = num2 < 1;
					if (flag7)
					{
						num2 = 1;
					}
					bool flag8 = this.tDelay > j;
					if (flag8)
					{
						Menu.menuTemY[j] += num2;
					}
				}
			}
			bool flag9 = Menu.menuTemY[Menu.menuTemY.Length - 1] >= GameCanvas.h;
			if (flag9)
			{
				this.tDelay = 0;
				this.doCloseMenu();
			}
		}
		bool flag10 = Menu.xc != 0;
		if (flag10)
		{
			Menu.xc >>= 1;
			bool flag11 = Menu.xc < 0;
			if (flag11)
			{
				Menu.xc = 0;
			}
		}
		bool flag12 = this.isScrolling();
		if (!flag12)
		{
			bool flag13 = this.waitToPerform > 0;
			if (flag13)
			{
				this.waitToPerform--;
				bool flag14 = this.waitToPerform == 0;
				if (flag14)
				{
					bool flag15 = this.menuSelectedItem >= 0 && !this.isNotClose[this.menuSelectedItem];
					if (flag15)
					{
						this.isClose = true;
						this.touch = true;
						GameCanvas.panel.cp = null;
					}
					else
					{
						this.performSelect();
					}
				}
			}
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00003136 File Offset: 0x00001336
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000AAB RID: 2731
	public bool showMenu;

	// Token: 0x04000AAC RID: 2732
	public MyVector menuItems;

	// Token: 0x04000AAD RID: 2733
	public int menuSelectedItem;

	// Token: 0x04000AAE RID: 2734
	public int menuX;

	// Token: 0x04000AAF RID: 2735
	public int menuY;

	// Token: 0x04000AB0 RID: 2736
	public int menuW;

	// Token: 0x04000AB1 RID: 2737
	public int menuH;

	// Token: 0x04000AB2 RID: 2738
	public static int[] menuTemY;

	// Token: 0x04000AB3 RID: 2739
	public static int cmtoX;

	// Token: 0x04000AB4 RID: 2740
	public static int cmx;

	// Token: 0x04000AB5 RID: 2741
	public static int cmdy;

	// Token: 0x04000AB6 RID: 2742
	public static int cmvy;

	// Token: 0x04000AB7 RID: 2743
	public static int cmxLim;

	// Token: 0x04000AB8 RID: 2744
	public static int xc;

	// Token: 0x04000AB9 RID: 2745
	private Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000ABA RID: 2746
	private Command right = new Command(mResources.CLOSE, 0, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH + 1);

	// Token: 0x04000ABB RID: 2747
	private Command center;

	// Token: 0x04000ABC RID: 2748
	public static Image imgMenu1;

	// Token: 0x04000ABD RID: 2749
	public static Image imgMenu2;

	// Token: 0x04000ABE RID: 2750
	private bool disableClose;

	// Token: 0x04000ABF RID: 2751
	public int tDelay;

	// Token: 0x04000AC0 RID: 2752
	public int w;

	// Token: 0x04000AC1 RID: 2753
	private int pa;

	// Token: 0x04000AC2 RID: 2754
	private bool trans;

	// Token: 0x04000AC3 RID: 2755
	private int pointerDownTime;

	// Token: 0x04000AC4 RID: 2756
	private int pointerDownFirstX;

	// Token: 0x04000AC5 RID: 2757
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000AC6 RID: 2758
	private bool pointerIsDowning;

	// Token: 0x04000AC7 RID: 2759
	private bool isDownWhenRunning;

	// Token: 0x04000AC8 RID: 2760
	private bool wantUpdateList;

	// Token: 0x04000AC9 RID: 2761
	private int waitToPerform;

	// Token: 0x04000ACA RID: 2762
	private int cmRun;

	// Token: 0x04000ACB RID: 2763
	private bool touch;

	// Token: 0x04000ACC RID: 2764
	private bool close;

	// Token: 0x04000ACD RID: 2765
	private int cmvx;

	// Token: 0x04000ACE RID: 2766
	private int cmdx;

	// Token: 0x04000ACF RID: 2767
	private bool isClose;

	// Token: 0x04000AD0 RID: 2768
	public bool[] isNotClose;
}
