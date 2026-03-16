using System;

// Token: 0x020000A5 RID: 165
public class CrackBallScr : mScreen
{
	// Token: 0x060006E0 RID: 1760 RVA: 0x0005DAB8 File Offset: 0x0005BEB8
	public CrackBallScr()
	{
		CrackBallScr.xSkill = new int[2];
		CrackBallScr.xSkill[0] = 16;
		CrackBallScr.ySkill = GameCanvas.h - 41;
		CrackBallScr.xSkill[1] = GameCanvas.w - 40;
		Image img = GameCanvas.loadImage("/e/e_1.png");
		CrackBallScr.fraImgKame = new FrameImage(img, 30, 30);
		Image img2 = GameCanvas.loadImage("/e/e_0.png");
		CrackBallScr.fraImgKame_1 = new FrameImage(img2, 68, 65);
		Image img3 = GameCanvas.loadImage("/e/e_2.png");
		CrackBallScr.fraImgKame_2 = new FrameImage(img3, 66, 70);
		CrackBallScr.imgReplay = GameCanvas.loadImage("/e/nut2.png");
		CrackBallScr.imgX = GameCanvas.loadImage("/e/nut3.png");
		this.wP = 230;
		this.xP = GameCanvas.hw - this.wP / 2;
		this.hP = 40;
		this.yP = -this.hP;
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0005DBCB File Offset: 0x0005BFCB
	public static CrackBallScr gI()
	{
		if (CrackBallScr.instance == null)
		{
			CrackBallScr.instance = new CrackBallScr();
		}
		return CrackBallScr.instance;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0005DBE8 File Offset: 0x0005BFE8
	public void SetCrackBallScr(short[] idImage, byte typePrice, int price, short idTicket)
	{
		if (idImage == null || idImage.Length <= 0)
		{
			return;
		}
		this.yTo = global::Char.myCharz().cy - 10;
		this.setAuraItem();
		this.listBall = new BallInfo[idImage.Length];
		for (int i = 0; i < this.listBall.Length; i++)
		{
			this.listBall[i] = new BallInfo();
			this.listBall[i].idImg = (int)idImage[i];
			this.listBall[i].count = i * 25;
			this.listBall[i].yTo = -999;
			this.listBall[i].vx = Res.random(2, 5);
			this.listBall[i].dir = Res.random(-1, 2);
			this.listBall[i].SetChar();
		}
		this.isCanSkill = false;
		this.isKame = false;
		this.isSendSv = false;
		this.timeStart = GameCanvas.timeNow + (long)Res.random(1000, 2000);
		this.step = 0;
		this.indexSelect = -1;
		this.indexSkillSelect = -1;
		this.typePrice = typePrice;
		this.price = price;
		this.cost = 0;
		global::Char.myCharz().moveTo(470, 408, 1);
		global::Char.myCharz().cdir = 2;
		global::Char.myCharz().statusMe = 1;
		this.countFr = 0;
		this.countKame = 0;
		this.frame = 0;
		this.vp = 0;
		this.yP = -this.hP;
		this.idTicket = idTicket;
		this.numTicket = 0;
		this.checkNumTicket();
		this.switchToMe();
		SoundMn.gI().hoisinh();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0005DD90 File Offset: 0x0005C190
	private void setAuraItem()
	{
		this.rO = GameCanvas.hh / 3 + 10;
		if (this.rO > 50)
		{
			this.rO = 50;
		}
		this.xO = 360;
		GameScr.cmx = GameScr.cmxLim / 2;
		this.yO = GameScr.cmy + GameCanvas.hh / 3 + 30;
		this.iDot = 175;
		this.angle = 0;
		this.iAngle = 360 / this.iDot;
		this.xArg = new int[this.iDot];
		this.yArg = new int[this.iDot];
		this.xDot = new int[this.iDot];
		this.yDot = new int[this.iDot];
		this.setDotPosition();
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0005DE60 File Offset: 0x0005C260
	private void setDotPosition()
	{
		if (!GameCanvas.lowGraphic)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				if (this.angle < 90)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 90 && this.angle < 180)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 180 && this.angle < 270)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				else
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				this.angle += this.iAngle;
			}
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0005DFFE File Offset: 0x0005C3FE
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0005E000 File Offset: 0x0005C400
	public override void update()
	{
		try
		{
			this.cost = this.price * (int)this.checkNum();
			this.checkNumTicket();
			GameScr.gI().update();
			if (this.timeStart - GameCanvas.timeNow > 0L)
			{
				for (int i = 0; i < this.listBall.Length; i++)
				{
					this.listBall[i].count += 2;
					if (this.listBall[i].count >= this.iDot)
					{
						this.listBall[i].count = 0;
					}
					this.listBall[i].x = this.xDot[this.listBall[i].count];
					this.listBall[i].y = this.yDot[this.listBall[i].count];
				}
			}
			else
			{
				if (this.step == 0)
				{
					this.step = 1;
				}
				if (this.step == 1)
				{
					for (int j = 0; j < this.listBall.Length; j++)
					{
						if (this.listBall[j].yTo != -999 && !this.listBall[j].isDone)
						{
							if (this.listBall[j].y < this.listBall[j].yTo)
							{
								if (this.listBall[j].vy < 0)
								{
									this.listBall[j].vy = 0;
								}
								if (this.listBall[j].y + this.listBall[j].vy > this.listBall[j].yTo)
								{
									this.listBall[j].y = this.listBall[j].yTo;
								}
								else
								{
									this.listBall[j].y += this.listBall[j].vy;
								}
								this.listBall[j].vy++;
							}
							else
							{
								if (this.listBall[j].vy > 0)
								{
									this.listBall[j].vy = 0;
								}
								this.listBall[j].y += this.listBall[j].vy;
								this.listBall[j].vy--;
							}
							if (this.listBall[j].y == this.listBall[j].yTo)
							{
								Effect me = new Effect(19, this.listBall[j].x - 5, this.listBall[j].y + 25, 2, 1, -1);
								EffecMn.addEff(me);
								SoundMn.gI().charFall();
								this.listBall[j].isDone = true;
								if (!this.isCanSkill)
								{
									this.isCanSkill = true;
								}
							}
						}
					}
				}
				if (this.step == 2)
				{
					for (int k = 0; k < this.listBall.Length; k++)
					{
						if (!this.listBall[k].isDone)
						{
							if (this.listBall[k].y > -10)
							{
								if (this.listBall[k].vy > 0)
								{
									this.listBall[k].vy = 0;
								}
								this.listBall[k].y += this.listBall[k].vy;
								this.listBall[k].vy--;
								this.listBall[k].x += this.listBall[k].vx * this.listBall[k].dir;
								this.listBall[k].vx -= 3;
							}
							if (this.listBall[k].y == -10)
							{
								this.listBall[k].isPaint = false;
							}
						}
					}
					this.countFr++;
					if (this.countFr > this.fr.Length - 1)
					{
						this.countFr = this.fr.Length - 1;
						this.isKame = true;
						SoundMn.gI().newKame();
						if (!this.isSendSv && this.timeKame - GameCanvas.timeNow < 0L)
						{
							Service.gI().SendCrackBall(2, (byte)(this.checkTicket() + this.checkNum()));
							this.isSendSv = true;
						}
					}
					global::Char.myCharz().cf = (int)this.fr[this.countFr];
					this.countKame++;
					if (this.countKame > 5)
					{
						this.countKame = 0;
					}
					this.frame = (int)this.nFrame[this.countKame];
				}
				if (this.step == 3)
				{
					if (this.countKame <= 5)
					{
						this.countKame = 5;
					}
					this.countKame++;
					if (this.countKame > this.nFrame.Length - 1)
					{
						this.countKame = this.nFrame.Length - 1;
						this.step = 4;
						this.isKame = false;
						int num = 0;
						for (int l = 0; l < this.listBall.Length; l++)
						{
							if (this.listBall[l].isDone && !this.listBall[l].isSetImg)
							{
								this.listBall[l].idImg = (int)this.idItem[num];
								this.listBall[l].isSetImg = true;
								num++;
							}
						}
					}
					this.frame = (int)this.nFrame[this.countKame];
				}
				if (this.step == 4)
				{
					for (int m = 0; m < this.listBall.Length; m++)
					{
						if (this.listBall[m].isPaint)
						{
							this.listBall[m].xTo = global::Char.myCharz().cx;
						}
					}
					this.step = 5;
				}
				if (this.step == 5)
				{
					this.vp++;
					if (this.yP < GameCanvas.hh / 3)
					{
						if (this.yP + this.vp > GameCanvas.hh / 3)
						{
							this.yP = GameCanvas.hh / 3;
						}
						else
						{
							this.yP += this.vp;
						}
					}
					for (int n = 0; n < this.listBall.Length; n++)
					{
						if (this.listBall[n].isPaint)
						{
							if (this.listBall[n].x < this.listBall[n].xTo)
							{
								if (this.listBall[n].vx < 0)
								{
									this.listBall[n].vx = 0;
								}
								if (this.listBall[n].x + this.listBall[n].vx > this.listBall[n].xTo)
								{
									this.listBall[n].x = this.listBall[n].xTo;
								}
								else
								{
									this.listBall[n].x += this.listBall[n].vx;
								}
								this.listBall[n].vx++;
							}
							else
							{
								if (this.listBall[n].vx > 0)
								{
									this.listBall[n].vx = 0;
								}
								this.listBall[n].x += this.listBall[n].vx;
								this.listBall[n].vx--;
							}
							if (this.listBall[n].x == this.listBall[n].xTo)
							{
								this.listBall[n].isPaint = false;
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

	// Token: 0x060006E7 RID: 1767 RVA: 0x0005E80C File Offset: 0x0005CC0C
	public override void updateKey()
	{
		if (InfoDlg.isLock)
		{
			return;
		}
		if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
		{
			this.updateKeyTouchControl();
		}
		for (int i = 1; i < 8; i++)
		{
			if (GameCanvas.keyPressed[i])
			{
				GameCanvas.keyPressed[i] = false;
				this.doClickBall(i - 1);
			}
		}
		if (GameCanvas.keyPressed[12])
		{
			GameCanvas.keyPressed[12] = false;
			this.doClickSkill(0);
		}
		if (GameCanvas.keyPressed[13])
		{
			GameCanvas.keyPressed[13] = false;
			this.doClickSkill(1);
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0005E8C0 File Offset: 0x0005CCC0
	private void updateKeyTouchControl()
	{
		if (this.step == 1 && GameCanvas.isPointerClick)
		{
			for (int i = 0; i < this.listBall.Length; i++)
			{
				if (GameCanvas.isPointerHoldIn(this.listBall[i].x - 20 - GameScr.cmx, this.listBall[i].y - 10 - GameScr.cmy, 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.doClickBall(i);
				}
			}
		}
		if (GameCanvas.isPointerClick)
		{
			for (int j = 0; j < CrackBallScr.xSkill.Length; j++)
			{
				if (GameCanvas.isPointerHoldIn(CrackBallScr.xSkill[j], CrackBallScr.ySkill, 36, 36) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.doClickSkill(j);
				}
			}
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0005E9A8 File Offset: 0x0005CDA8
	private void doClickBall(int index)
	{
		if (this.listBall[index].isDone)
		{
			return;
		}
		SoundMn.gI().getItem();
		long num = (this.typePrice != 0) ? ((long)global::Char.myCharz().checkLuong()) : global::Char.myCharz().xu;
		if ((int)this.checkTicket() >= this.numTicket && num < (long)(this.cost + this.price))
		{
			string s = mResources.not_enough_money_1 + " " + ((this.typePrice != 0) ? mResources.LUONG : mResources.XU);
			GameScr.info1.addInfo(s, 0);
			return;
		}
		this.indexSelect = index;
		this.listBall[this.indexSelect].yTo = this.yTo + Res.random(-3, 3);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0005EA7C File Offset: 0x0005CE7C
	private void doClickSkill(int index)
	{
		this.indexSkillSelect = index;
		if (index == 0)
		{
			if (this.step < 2)
			{
				if (this.checkTicket() + this.checkNum() > 0)
				{
					this.step = 2;
					SoundMn.gI().gong();
					global::Char.myCharz().setSkillPaint(GameScr.sks[13], 0);
					this.timeKame = GameCanvas.timeNow + (long)Res.random(2000, 3000);
				}
			}
			else if (this.yP == GameCanvas.hh / 3)
			{
				Service.gI().SendCrackBall(this.typePrice, 0);
			}
		}
		else
		{
			GameScr.gI().isRongThanXuatHien = false;
			GameScr.gI().switchToMe();
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0005EB38 File Offset: 0x0005CF38
	public override void paint(mGraphics g)
	{
		try
		{
			GameScr.gI().paint(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			g.translate(0, GameCanvas.transY);
			for (int i = 0; i < this.listBall.Length; i++)
			{
				if (this.listBall[i].isPaint && this.listBall[i].y > this.listBall[i].yTo - 20)
				{
					g.drawImage(TileMap.bong, this.listBall[i].x, this.listBall[i].yTo + 7, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			for (int j = 0; j < this.listBall.Length; j++)
			{
				if (this.listBall[j].isPaint)
				{
					SmallImage.drawSmallImage(g, this.listBall[j].idImg, this.listBall[j].x, this.listBall[j].y, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			if (this.isKame)
			{
				if (CrackBallScr.fraImgKame != null)
				{
					int num = global::Char.myCharz().cx - CrackBallScr.fraImgKame.frameWidth - 28;
					for (int k = 0; k < GameCanvas.w / CrackBallScr.fraImgKame.frameWidth + 1; k++)
					{
						CrackBallScr.fraImgKame.drawFrame(this.frame, num - k * (CrackBallScr.fraImgKame.frameWidth - 1), global::Char.myCharz().cy - CrackBallScr.fraImgKame.frameHeight / 2 - 12 + 2, 0, 0, g);
					}
				}
				if (CrackBallScr.fraImgKame_1 != null)
				{
					int num2 = global::Char.myCharz().cx - CrackBallScr.fraImgKame_1.frameWidth - 10;
					CrackBallScr.fraImgKame_1.drawFrame(this.frame, num2 - 5, global::Char.myCharz().cy - CrackBallScr.fraImgKame_1.frameHeight / 2 - 12, 0, 0, g);
				}
			}
			GameScr.resetTranslate(g);
			int num3 = 240;
			int num4 = GameCanvas.w - num3;
			int num5 = 15;
			g.setColor(13524492);
			g.fillRect(num4, num5 - 15, num3, 15);
			g.drawImage(Panel.imgXu, num4 + 11, num5 - 7, 3);
			g.drawImage(Panel.imgLuong, num4 + 90, num5 - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().xuStr + string.Empty, num4 + 24, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongStr + string.Empty, num4 + 100, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgLuongKhoa, num4 + 150, num5 - 7, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongKhoaStr + string.Empty, num4 + 160, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgTicket, num4 + 200, num5 - 7, 3);
			mFont.tahoma_7_yellow.drawString(g, this.numTicket + string.Empty, num4 + 210, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			if (this.step < 4)
			{
				int num6 = num3 / 2 + 20;
				int num7 = GameCanvas.w - num6;
				g.setColor(11837316);
				g.fillRect(num7, num5, num6, 15);
				if (this.typePrice == 0)
				{
					g.drawImage(Panel.imgXu, num7 + 21, num5 + 8, 3);
				}
				else
				{
					g.drawImage(Panel.imgLuongKhoa, num7 + 21, num5 + 7, 3);
					g.drawImage(Panel.imgLuong, num7 + 18, num5 + 7, 3);
				}
				mFont.tahoma_7_red.drawString(g, " -" + this.cost, num7 + 30, num5 + 2, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(Panel.imgTicket, num7 + 80, num5 + 7, 3);
				mFont.tahoma_7_red.drawString(g, " -" + this.checkTicket(), num7 + 90, num5 + 2, mFont.LEFT, mFont.tahoma_7_grey);
			}
			g.drawImage(GameScr.imgSkill, CrackBallScr.xSkill[0], CrackBallScr.ySkill, 0);
			if (this.indexSkillSelect == 0)
			{
				g.drawImage(GameScr.imgSkill2, CrackBallScr.xSkill[0], CrackBallScr.ySkill, 0);
			}
			if (this.step < 3)
			{
				SmallImage.drawSmallImage(g, 540, CrackBallScr.xSkill[0] + 14, CrackBallScr.ySkill + 14, 0, StaticObj.VCENTER_HCENTER);
			}
			else
			{
				g.drawImage(CrackBallScr.imgReplay, CrackBallScr.xSkill[0] + 14 - 10, CrackBallScr.ySkill + 14 - 10, 0);
			}
			g.drawImage(GameScr.imgSkill, CrackBallScr.xSkill[1], CrackBallScr.ySkill, 0);
			if (this.indexSkillSelect == 1)
			{
				g.drawImage(GameScr.imgSkill2, CrackBallScr.xSkill[1], CrackBallScr.ySkill, 0);
			}
			g.drawImage(CrackBallScr.imgX, CrackBallScr.xSkill[1] + 14 - 10, CrackBallScr.ySkill + 14 - 10, 0);
			if (this.step > 3)
			{
				GameCanvas.paintz.paintFrameSimple(this.xP, this.yP, this.wP, this.hP, g);
				int num8 = GameCanvas.hw - this.idItem.Length * 30 / 2;
				for (int l = 0; l < this.idItem.Length; l++)
				{
					SmallImage.drawSmallImage(g, (int)this.idItem[l], num8 + 5 + l * 30, this.yP + 10, 0, 0);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0005F13C File Offset: 0x0005D53C
	public void DoneCrackBallScr(short[] idImage)
	{
		this.step = 3;
		this.idItem = idImage;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0005F14C File Offset: 0x0005D54C
	public override void switchToMe()
	{
		GameScr.isPaintOther = true;
		GameScr.gI().isRongThanXuatHien = true;
		base.switchToMe();
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0005F168 File Offset: 0x0005D568
	private byte checkTicket()
	{
		byte b = 0;
		for (int i = 0; i < this.listBall.Length; i++)
		{
			if (this.listBall[i].isDone)
			{
				b += 1;
			}
		}
		if ((int)b > this.numTicket)
		{
			b = (byte)this.numTicket;
		}
		return b;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0005F1BC File Offset: 0x0005D5BC
	private byte checkNum()
	{
		byte b = 0;
		for (int i = 0; i < this.listBall.Length; i++)
		{
			if (this.listBall[i].isDone)
			{
				b += 1;
			}
		}
		b -= this.checkTicket();
		if (b <= 0)
		{
			b = 0;
		}
		return b;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0005F210 File Offset: 0x0005D610
	private void checkNumTicket()
	{
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == this.idTicket)
			{
				this.numTicket = global::Char.myCharz().arrItemBag[i].quantity;
				break;
			}
		}
	}

	// Token: 0x04000C96 RID: 3222
	public static CrackBallScr instance;

	// Token: 0x04000C97 RID: 3223
	private BallInfo[] listBall;

	// Token: 0x04000C98 RID: 3224
	private byte step;

	// Token: 0x04000C99 RID: 3225
	private byte typePrice;

	// Token: 0x04000C9A RID: 3226
	private int rO;

	// Token: 0x04000C9B RID: 3227
	private int xO;

	// Token: 0x04000C9C RID: 3228
	private int yO;

	// Token: 0x04000C9D RID: 3229
	private int angle;

	// Token: 0x04000C9E RID: 3230
	private int iAngle;

	// Token: 0x04000C9F RID: 3231
	private int iDot;

	// Token: 0x04000CA0 RID: 3232
	private int yTo;

	// Token: 0x04000CA1 RID: 3233
	private int indexSelect;

	// Token: 0x04000CA2 RID: 3234
	private int indexSkillSelect;

	// Token: 0x04000CA3 RID: 3235
	private int numTicket;

	// Token: 0x04000CA4 RID: 3236
	private int xP;

	// Token: 0x04000CA5 RID: 3237
	private int yP;

	// Token: 0x04000CA6 RID: 3238
	private int wP;

	// Token: 0x04000CA7 RID: 3239
	private int hP;

	// Token: 0x04000CA8 RID: 3240
	private int price;

	// Token: 0x04000CA9 RID: 3241
	private int cost;

	// Token: 0x04000CAA RID: 3242
	private int countFr;

	// Token: 0x04000CAB RID: 3243
	private int countKame;

	// Token: 0x04000CAC RID: 3244
	private int frame;

	// Token: 0x04000CAD RID: 3245
	private int vp;

	// Token: 0x04000CAE RID: 3246
	private int[] xArg;

	// Token: 0x04000CAF RID: 3247
	private int[] yArg;

	// Token: 0x04000CB0 RID: 3248
	private int[] xDot;

	// Token: 0x04000CB1 RID: 3249
	private int[] yDot;

	// Token: 0x04000CB2 RID: 3250
	private short[] idItem;

	// Token: 0x04000CB3 RID: 3251
	private long timeStart;

	// Token: 0x04000CB4 RID: 3252
	private long timeKame;

	// Token: 0x04000CB5 RID: 3253
	private bool isKame;

	// Token: 0x04000CB6 RID: 3254
	private bool isCanSkill;

	// Token: 0x04000CB7 RID: 3255
	private bool isSendSv;

	// Token: 0x04000CB8 RID: 3256
	private short idTicket;

	// Token: 0x04000CB9 RID: 3257
	private static int ySkill;

	// Token: 0x04000CBA RID: 3258
	private static int[] xSkill;

	// Token: 0x04000CBB RID: 3259
	private static FrameImage fraImgKame;

	// Token: 0x04000CBC RID: 3260
	private static FrameImage fraImgKame_1;

	// Token: 0x04000CBD RID: 3261
	private static FrameImage fraImgKame_2;

	// Token: 0x04000CBE RID: 3262
	private static Image imgX;

	// Token: 0x04000CBF RID: 3263
	private static Image imgReplay;

	// Token: 0x04000CC0 RID: 3264
	private byte[] fr = new byte[]
	{
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		20
	};

	// Token: 0x04000CC1 RID: 3265
	private byte[] nFrame = new byte[]
	{
		0,
		0,
		0,
		1,
		1,
		1,
		2,
		2,
		2,
		3,
		3,
		3
	};
}
