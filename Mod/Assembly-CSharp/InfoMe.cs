using System;

// Token: 0x02000048 RID: 72
public class InfoMe
{
	// Token: 0x060003F7 RID: 1015 RVA: 0x00056BB0 File Offset: 0x00054DB0
	public InfoMe()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00056C0C File Offset: 0x00054E0C
	public static InfoMe gI()
	{
		bool flag = InfoMe.me == null;
		if (flag)
		{
			InfoMe.me = new InfoMe();
		}
		return InfoMe.me;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00056C3C File Offset: 0x00054E3C
	public void loadCharId()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00056C74 File Offset: 0x00054E74
	public void paint(mGraphics g)
	{
		bool flag = this.Equals(GameScr.info2) && GameScr.gI().isVS();
		if (!flag)
		{
			bool flag2 = this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null;
			if (!flag2)
			{
				bool flag3 = !GameScr.isPaint;
				if (!flag3)
				{
					bool flag4 = GameCanvas.currentScreen != GameScr.gI() && GameCanvas.currentScreen != CrackBallScr.gI();
					if (!flag4)
					{
						bool flag5 = ChatPopup.serverChatPopUp != null;
						if (!flag5)
						{
							bool flag6 = !this.isUpdate;
							if (!flag6)
							{
								bool ischangingMap = global::Char.ischangingMap;
								if (!ischangingMap)
								{
									bool flag7 = GameCanvas.panel.isShow && this.Equals(GameScr.info2);
									if (!flag7)
									{
										g.translate(-g.getTranslateX(), -g.getTranslateY());
										g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
										bool flag8 = this.info != null;
										if (flag8)
										{
											this.info.paint(g, this.cmx, this.cmy, this.dir);
											bool flag9 = this.info.info == null || this.info.info.charInfo == null || this.cmdChat != null || !GameCanvas.isTouch;
											if (flag9)
											{
											}
											bool flag10 = this.info.info == null || this.info.info.charInfo == null || this.cmdChat != null;
											if (flag10)
											{
											}
										}
										bool flag11 = this.info.info != null && this.info.info.charInfo == null && this.charId != null;
										if (flag11)
										{
											SmallImage.drawSmallImage(g, this.charId[global::Char.myCharz().cgender][this.f], this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
										}
										g.translate(-g.getTranslateX(), -g.getTranslateY());
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00056EC6 File Offset: 0x000550C6
	public void hide()
	{
		this.info.hide();
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00056ED8 File Offset: 0x000550D8
	public void moveCamera()
	{
		bool flag = this.cmy != this.cmtoY;
		if (flag)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		bool flag2 = this.cmx != this.cmtoX;
		if (flag2)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		this.tF++;
		bool flag3 = this.tF == 5;
		if (flag3)
		{
			this.tF = 0;
			bool flag4 = this.f == 0;
			if (flag4)
			{
				this.f = 1;
			}
			else
			{
				this.f = 0;
			}
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00056FF1 File Offset: 0x000551F1
	public void doClick(int t)
	{
		this.timeDelay = t;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00056FFC File Offset: 0x000551FC
	public void update()
	{
		bool flag = this.info != null && this.info.infoWaitToShow != null && this.info.infoWaitToShow.size() == 0 && this.cmy != -40;
		if (flag)
		{
			this.info.timeW--;
			bool flag2 = this.info.timeW <= 0;
			if (flag2)
			{
				this.cmy = -40;
				this.info.time = 0;
				this.info.infoWaitToShow.removeAllElements();
				this.info.says = null;
				this.info.timeW = 200;
			}
		}
		bool flag3 = this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null;
		if (!flag3)
		{
			bool flag4 = !this.isUpdate;
			if (!flag4)
			{
				this.moveCamera();
				bool flag5 = this.info == null;
				if (!flag5)
				{
					bool flag6 = this.info != null && this.info.info == null;
					if (!flag6)
					{
						bool flag7 = !this.isDone;
						if (flag7)
						{
							bool flag8 = this.timeDelay > 0;
							if (flag8)
							{
								this.timeDelay--;
								bool flag9 = this.timeDelay == 0;
								if (flag9)
								{
									GameCanvas.panel.setTypeMessage();
									GameCanvas.panel.show();
								}
							}
							bool flag10 = GameCanvas.gameTick % 3 == 0;
							if (flag10)
							{
								bool flag11 = global::Char.myCharz().cdir == 1;
								if (flag11)
								{
									this.cmtoX = global::Char.myCharz().cx - 20 - GameScr.cmx;
								}
								bool flag12 = global::Char.myCharz().cdir == -1;
								if (flag12)
								{
									this.cmtoX = global::Char.myCharz().cx + 20 - GameScr.cmx;
								}
								bool flag13 = this.cmtoX <= 24;
								if (flag13)
								{
									this.cmtoX += this.info.sayWidth / 2;
								}
								bool flag14 = this.cmtoX >= GameCanvas.w - 24;
								if (flag14)
								{
									this.cmtoX -= this.info.sayWidth / 2;
								}
								this.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
								bool flag15 = this.info.says != null && this.cmtoY < (this.info.says.Length + 1) * 12 + 10;
								if (flag15)
								{
									this.cmtoY = (this.info.says.Length + 1) * 12 + 10;
								}
								bool flag16 = this.info.info.charInfo != null;
								if (flag16)
								{
									bool flag17 = GameCanvas.w - 50 > 155 + this.info.W;
									if (flag17)
									{
										this.cmtoX = GameCanvas.w - 60 - this.info.W / 2;
										this.cmtoY = this.info.H + 10;
									}
									else
									{
										this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
										this.cmtoY = 45 + this.info.H;
										bool flag18 = GameCanvas.w > GameCanvas.h || GameCanvas.w < 220;
										if (flag18)
										{
											this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
											this.cmtoY = this.info.H + 10;
										}
									}
								}
							}
							bool flag19 = this.cmx > global::Char.myCharz().cx - GameScr.cmx;
							if (flag19)
							{
								this.dir = -1;
							}
							else
							{
								this.dir = 1;
							}
						}
						bool flag20 = this.info.info != null;
						if (flag20)
						{
							bool flag21 = this.info.infoWaitToShow.size() > 1;
							if (flag21)
							{
								bool flag22 = this.info.info.timeCount == 0;
								if (flag22)
								{
									this.info.time++;
									bool flag23 = this.info.time >= this.info.info.speed;
									if (flag23)
									{
										this.info.time = 0;
										this.info.infoWaitToShow.removeElementAt(0);
										InfoItem infoItem = (InfoItem)this.info.infoWaitToShow.firstElement();
										this.info.info = infoItem;
										this.info.getInfo();
									}
								}
								else
								{
									this.info.info.curr = mSystem.currentTimeMillis();
									bool flag24 = this.info.info.curr - this.info.info.last >= 100L;
									if (flag24)
									{
										this.info.info.last = mSystem.currentTimeMillis();
										this.info.info.timeCount--;
									}
									bool flag25 = this.info.info.timeCount == 0;
									if (flag25)
									{
										this.info.infoWaitToShow.removeElementAt(0);
										bool flag26 = this.info.infoWaitToShow.size() == 0;
										if (!flag26)
										{
											InfoItem infoItem2 = (InfoItem)this.info.infoWaitToShow.firstElement();
											this.info.info = infoItem2;
											this.info.getInfo();
										}
									}
								}
							}
							else
							{
								bool flag27 = this.info.infoWaitToShow.size() == 1;
								if (flag27)
								{
									bool flag28 = this.info.info.timeCount == 0;
									if (flag28)
									{
										this.info.time++;
										bool flag29 = this.info.time >= this.info.info.speed;
										if (flag29)
										{
											this.isDone = true;
										}
										bool flag30 = this.info.time == this.info.info.speed;
										if (flag30)
										{
											this.cmtoY = -40;
											this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
										}
										bool flag31 = this.info.time >= this.info.info.speed + 20;
										if (flag31)
										{
											this.info.time = 0;
											this.info.infoWaitToShow.removeAllElements();
											this.info.says = null;
											this.info.timeW = 200;
										}
									}
									else
									{
										this.info.info.curr = mSystem.currentTimeMillis();
										bool flag32 = this.info.info.curr - this.info.info.last >= 100L;
										if (flag32)
										{
											this.info.info.last = mSystem.currentTimeMillis();
											this.info.info.timeCount--;
										}
										bool flag33 = this.info.info.timeCount == 0;
										if (flag33)
										{
											this.isDone = true;
											this.cmtoY = -40;
											this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
											this.info.time = 0;
											this.info.infoWaitToShow.removeAllElements();
											this.info.says = null;
											this.cmdChat = null;
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

	// Token: 0x060003FF RID: 1023 RVA: 0x00057802 File Offset: 0x00055A02
	public void addInfoWithChar(string s, global::Char c, bool isChatServer)
	{
		this.playerID = c.charID;
		this.info.addInfo(s, 3, c, isChatServer);
		this.isDone = false;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00057828 File Offset: 0x00055A28
	public void addInfo(string s, int Type)
	{
		s = Res.changeString(s);
		bool flag = this.info.infoWaitToShow.size() > 0 && s.Equals(((InfoItem)this.info.infoWaitToShow.lastElement()).s);
		if (!flag)
		{
			bool flag2 = this.info.infoWaitToShow.size() > 10;
			if (flag2)
			{
				for (int i = 0; i < 5; i++)
				{
					this.info.infoWaitToShow.removeElementAt(0);
				}
			}
			global::Char cInfo = null;
			this.info.addInfo(s, Type, cInfo, false);
			bool flag3 = this.info.infoWaitToShow.size() == 1;
			if (flag3)
			{
				this.cmy = 0;
				this.cmx = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
			}
			this.isDone = false;
		}
	}

	// Token: 0x040008CA RID: 2250
	public static InfoMe me;

	// Token: 0x040008CB RID: 2251
	public int[][] charId = new int[3][];

	// Token: 0x040008CC RID: 2252
	public Info info = new Info();

	// Token: 0x040008CD RID: 2253
	public int dir;

	// Token: 0x040008CE RID: 2254
	public int f;

	// Token: 0x040008CF RID: 2255
	public int tF;

	// Token: 0x040008D0 RID: 2256
	public int cmtoY;

	// Token: 0x040008D1 RID: 2257
	public int cmy;

	// Token: 0x040008D2 RID: 2258
	public int cmdy;

	// Token: 0x040008D3 RID: 2259
	public int cmvy;

	// Token: 0x040008D4 RID: 2260
	public int cmyLim;

	// Token: 0x040008D5 RID: 2261
	public int cmtoX;

	// Token: 0x040008D6 RID: 2262
	public int cmx;

	// Token: 0x040008D7 RID: 2263
	public int cmdx;

	// Token: 0x040008D8 RID: 2264
	public int cmvx;

	// Token: 0x040008D9 RID: 2265
	public int cmxLim;

	// Token: 0x040008DA RID: 2266
	public bool isDone;

	// Token: 0x040008DB RID: 2267
	public bool isUpdate = true;

	// Token: 0x040008DC RID: 2268
	public int timeDelay;

	// Token: 0x040008DD RID: 2269
	public int playerID;

	// Token: 0x040008DE RID: 2270
	public int timeCount;

	// Token: 0x040008DF RID: 2271
	public Command cmdChat;

	// Token: 0x040008E0 RID: 2272
	public bool isShow;
}
