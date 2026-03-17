using System;

// Token: 0x020000AF RID: 175
public class TabClanIcon : IActionListener
{
	// Token: 0x060009B2 RID: 2482 RVA: 0x000A1F34 File Offset: 0x000A0134
	public TabClanIcon()
	{
		this.left = new Command(mResources.SELECT, this, 1, null);
		this.right = new Command(mResources.CLOSE, this, 2, null);
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x000A1F8C File Offset: 0x000A018C
	public void init()
	{
		bool flag = this.isGetName;
		if (flag)
		{
			this.w = 170;
			this.h = 118;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
		}
		else
		{
			this.w = 170;
			this.h = 170;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
			bool flag2 = GameCanvas.h < 240;
			if (flag2)
			{
				this.y -= 10;
			}
		}
		this.cmx = this.x;
		this.cmtoX = 0;
		bool flag3 = !this.isRequest;
		if (flag3)
		{
			this.nItem = ClanImage.vClanImage.size();
		}
		else
		{
			this.nItem = this.vItems.size();
		}
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.left.x = this.x;
			this.left.y = this.y + this.h + 5;
			this.right.x = this.x + this.w - 68;
			this.right.y = this.y + this.h + 5;
		}
		TabClanIcon.scrMain = new Scroll();
		TabClanIcon.scrMain.setStyle(this.nItem, this.WIDTH, this.x, this.y + this.disStart, this.w, this.h - this.disStart, true, 1);
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x000A2148 File Offset: 0x000A0348
	public void show(bool isGetName)
	{
		bool flag = global::Char.myCharz().clan != null;
		if (flag)
		{
			this.isUpdate = true;
		}
		this.isShow = true;
		this.isGetName = isGetName;
		this.init();
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x000A2185 File Offset: 0x000A0385
	public void showRequest(int msgID)
	{
		this.isShow = true;
		this.isRequest = true;
		this.msgID = msgID;
		this.init();
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x000A21A4 File Offset: 0x000A03A4
	public void hide()
	{
		this.cmtoX = this.x + this.w;
		SmallImage.clearHastable();
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00003136 File Offset: 0x00001336
	public void paintPeans(mGraphics g)
	{
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x000A21C0 File Offset: 0x000A03C0
	public void paintIcon(mGraphics g)
	{
		g.translate(-this.cmx, 0);
		PopUp.paintPopUp(g, this.x, this.y - 17, this.w, this.h + 17, -1, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.select_clan_icon, this.x + this.w / 2, this.y - 7, 2);
		bool flag = this.lastSelect >= 0 && this.lastSelect <= ClanImage.vClanImage.size() - 1;
		if (flag)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect);
			bool flag2 = clanImage.idImage != null;
			if (flag2)
			{
				global::Char.myCharz().paintBag(g, clanImage.idImage, GameCanvas.w / 2, this.y + 45, 1, false);
			}
		}
		global::Char.myCharz().paintCharBody(g, GameCanvas.w / 2, this.y + 45, 1, global::Char.myCharz().cf, false);
		g.setClip(this.x, this.y + this.disStart, this.w, this.h - this.disStart - 10);
		bool flag3 = TabClanIcon.scrMain != null;
		if (flag3)
		{
			g.translate(0, -TabClanIcon.scrMain.cmy);
		}
		for (int i = 0; i < this.nItem; i++)
		{
			int num = this.x + 10;
			int num2 = this.y + i * this.WIDTH + this.disStart;
			bool flag4 = num2 + this.WIDTH - ((TabClanIcon.scrMain == null) ? 0 : TabClanIcon.scrMain.cmy) >= this.y + this.disStart && num2 - ((TabClanIcon.scrMain == null) ? 0 : TabClanIcon.scrMain.cmy) <= this.y + this.disStart + this.h;
			if (flag4)
			{
				ClanImage clanImage2 = (ClanImage)ClanImage.vClanImage.elementAt(i);
				mFont mFont = mFont.tahoma_7_grey;
				bool flag5 = i == this.lastSelect;
				if (flag5)
				{
					mFont = mFont.tahoma_7_blue;
				}
				bool flag6 = clanImage2.name != null;
				if (flag6)
				{
					mFont.drawString(g, clanImage2.name, num + 20, num2, 0);
				}
				bool flag7 = clanImage2.xu > 0;
				if (flag7)
				{
					mFont.drawString(g, clanImage2.xu.ToString() + " " + mResources.XU, num + this.w - 20, num2, mFont.RIGHT);
				}
				else
				{
					bool flag8 = clanImage2.luong > 0;
					if (flag8)
					{
						mFont.drawString(g, clanImage2.luong.ToString() + " " + mResources.LUONG, num + this.w - 20, num2, mFont.RIGHT);
					}
				}
				bool flag9 = clanImage2.idImage != null;
				if (flag9)
				{
					SmallImage.drawSmallImage(g, (int)clanImage2.idImage[0], num, num2, 0, 0);
				}
			}
		}
		g.translate(0, -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x000A2524 File Offset: 0x000A0724
	public void paint(mGraphics g)
	{
		bool flag = !this.isRequest;
		if (flag)
		{
			this.paintIcon(g);
		}
		else
		{
			this.paintPeans(g);
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x000A2558 File Offset: 0x000A0758
	public void update()
	{
		bool flag = TabClanIcon.scrMain != null;
		if (flag)
		{
			TabClanIcon.scrMain.updatecm();
		}
		bool flag2 = this.cmx != this.cmtoX;
		if (flag2)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		bool flag3 = global::Math.abs(this.cmtoX - this.cmx) < 10;
		if (flag3)
		{
			this.cmx = this.cmtoX;
		}
		bool flag4 = this.cmx >= this.x + this.w - 10 && this.cmtoX >= this.x + this.w - 10;
		if (flag4)
		{
			this.isShow = false;
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x000A2650 File Offset: 0x000A0850
	public void updateKey()
	{
		bool flag = this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left));
		if (flag)
		{
			this.left.performAction();
		}
		bool flag2 = this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right));
		if (flag2)
		{
			this.right.performAction();
		}
		bool flag3 = this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center));
		if (flag3)
		{
			this.center.performAction();
		}
		bool flag4 = !this.isGetName;
		if (flag4)
		{
			bool flag5 = TabClanIcon.scrMain == null;
			if (flag5)
			{
				return;
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				TabClanIcon.scrMain.updateKey();
				this.select = TabClanIcon.scrMain.selectedItem;
			}
			bool flag6 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag6)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.select--;
				bool flag7 = this.select < 0;
				if (flag7)
				{
					this.select = this.nItem - 1;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			bool flag8 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			if (flag8)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.select++;
				bool flag9 = this.select > this.nItem - 1;
				if (flag9)
				{
					this.select = 0;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			bool flag10 = this.select != -1;
			if (flag10)
			{
				this.lastSelect = this.select;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x000A2870 File Offset: 0x000A0A70
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 2;
		if (flag)
		{
			this.hide();
		}
		bool flag2 = idAction == 1;
		if (flag2)
		{
			bool flag3 = !this.isGetName;
			if (flag3)
			{
				bool flag4 = !this.isRequest;
				if (flag4)
				{
					bool flag5 = this.lastSelect >= 0;
					if (flag5)
					{
						this.hide();
						bool flag6 = global::Char.myCharz().clan == null;
						if (flag6)
						{
							Service.gI().getClan(2, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, this.text);
						}
						else
						{
							Service.gI().getClan(4, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, string.Empty);
						}
					}
				}
				else
				{
					bool flag7 = this.lastSelect >= 0;
					if (flag7)
					{
						Item item = (Item)this.vItems.elementAt(this.select);
					}
				}
			}
		}
	}

	// Token: 0x04001221 RID: 4641
	private int x;

	// Token: 0x04001222 RID: 4642
	private int y;

	// Token: 0x04001223 RID: 4643
	private int w;

	// Token: 0x04001224 RID: 4644
	private int h;

	// Token: 0x04001225 RID: 4645
	private Command left;

	// Token: 0x04001226 RID: 4646
	private Command right;

	// Token: 0x04001227 RID: 4647
	private Command center;

	// Token: 0x04001228 RID: 4648
	private int WIDTH = 24;

	// Token: 0x04001229 RID: 4649
	public int nItem;

	// Token: 0x0400122A RID: 4650
	private int disStart = 50;

	// Token: 0x0400122B RID: 4651
	public static Scroll scrMain;

	// Token: 0x0400122C RID: 4652
	public int cmtoX;

	// Token: 0x0400122D RID: 4653
	public int cmx;

	// Token: 0x0400122E RID: 4654
	public int cmvx;

	// Token: 0x0400122F RID: 4655
	public int cmdx;

	// Token: 0x04001230 RID: 4656
	public bool isShow;

	// Token: 0x04001231 RID: 4657
	public bool isGetName;

	// Token: 0x04001232 RID: 4658
	public string text;

	// Token: 0x04001233 RID: 4659
	private bool isRequest;

	// Token: 0x04001234 RID: 4660
	private bool isUpdate;

	// Token: 0x04001235 RID: 4661
	public MyVector vItems = new MyVector();

	// Token: 0x04001236 RID: 4662
	private int msgID;

	// Token: 0x04001237 RID: 4663
	private int select;

	// Token: 0x04001238 RID: 4664
	private int lastSelect;

	// Token: 0x04001239 RID: 4665
	private ScrollResult sr;
}
