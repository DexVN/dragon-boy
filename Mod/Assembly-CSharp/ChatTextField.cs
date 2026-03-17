using System;

// Token: 0x02000012 RID: 18
public class ChatTextField : IActionListener
{
	// Token: 0x06000159 RID: 345 RVA: 0x0001B9D8 File Offset: 0x00019BD8
	public ChatTextField()
	{
		this.tfChat = new TField();
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.name = "chat";
		bool isWindowsPhone2 = Main.isWindowsPhone;
		if (isWindowsPhone2)
		{
			this.tfChat.strInfo = this.tfChat.name;
		}
		this.tfChat.width = GameCanvas.w - 6;
		bool flag = Main.isPC && this.tfChat.width > 250;
		if (flag)
		{
			this.tfChat.width = 250;
		}
		this.tfChat.height = mScreen.ITEM_HEIGHT + 2;
		this.tfChat.x = GameCanvas.w / 2 - this.tfChat.width / 2;
		this.tfChat.isFocus = true;
		this.tfChat.setMaxTextLenght(80);
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0001BAF4 File Offset: 0x00019CF4
	public void initChatTextField()
	{
		this.left = new Command(mResources.OK, this, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
		this.center = null;
		this.w = this.tfChat.width + 20;
		this.h = this.tfChat.height + 26;
		this.x = GameCanvas.w / 2 - this.w / 2;
		this.y = this.tfChat.y - 18;
		bool flag = Main.isPC && this.w > 320;
		if (flag)
		{
			this.w = 320;
		}
		this.left.x = this.x;
		this.right.x = this.x + this.w - 68;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.tfChat.y -= 5;
			this.y -= 20;
			this.h += 30;
			this.left.x = GameCanvas.w / 2 - 68 - 5;
			this.right.x = GameCanvas.w / 2 + 5;
			this.left.y = GameCanvas.h - 30;
			this.right.y = GameCanvas.h - 30;
		}
		this.cmdChat = new Command();
		ActionChat actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.tfChat.setText(str);
			this.parentScreen.onChatFromMe(str, this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		};
		this.cmdChat.actionChat = actionChat;
		this.cmdChat2 = new Command();
		this.cmdChat2.actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			bool flag2 = this.parentScreen != null;
			if (flag2)
			{
				this.tfChat.setText(str);
				this.parentScreen.onChatFromMe(str, this.to);
				this.tfChat.setText(string.Empty);
				this.tfChat.clearKb();
				bool flag3 = this.right != null;
				if (flag3)
				{
					this.right.performAction();
				}
			}
			this.isShow = false;
		};
		this.yBegin = this.tfChat.y;
		this.yUp = GameCanvas.h / 2 - 2 * this.tfChat.height;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00003136 File Offset: 0x00001336
	public void updateWhenKeyBoardVisible()
	{
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0001BD34 File Offset: 0x00019F34
	public void keyPressed(int keyCode)
	{
		bool flag = this.isShow;
		if (flag)
		{
			this.tfChat.keyPressed(keyCode);
		}
		bool flag2 = this.tfChat.getText().Equals(string.Empty);
		if (flag2)
		{
			this.right.caption = mResources.CLOSE;
		}
		else
		{
			this.right.caption = mResources.DELETE;
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0001BD9C File Offset: 0x00019F9C
	public static ChatTextField gI()
	{
		return (ChatTextField.instance != null) ? ChatTextField.instance : (ChatTextField.instance = new ChatTextField());
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0001BDC8 File Offset: 0x00019FC8
	public void startChat(int firstCharacter, IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.keyPressed(firstCharacter);
		bool flag = !this.tfChat.getText().Equals(string.Empty) && GameCanvas.currentDialog == null;
		if (flag)
		{
			this.parentScreen = parentScreen;
			this.isShow = true;
		}
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0001BE60 File Offset: 0x0001A060
	public void startChat(IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		bool flag = GameCanvas.currentDialog == null;
		if (flag)
		{
			this.isShow = true;
			this.tfChat.isFocus = true;
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0001BF30 File Offset: 0x0001A130
	public void startChat2(IChatable parentScreen, string to)
	{
		this.tfChat.setFocusWithKb(true);
		this.to = to;
		this.parentScreen = parentScreen;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		bool flag = GameCanvas.currentDialog == null;
		if (flag)
		{
			this.isShow = true;
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat2);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00003136 File Offset: 0x00001336
	public void updateKey()
	{
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
	public void update()
	{
		bool flag = !this.isShow;
		if (!flag)
		{
			this.tfChat.update();
			bool isWindowsPhone = Main.isWindowsPhone;
			if (isWindowsPhone)
			{
				this.updateWhenKeyBoardVisible();
			}
			bool justReturnFromTextBox = this.tfChat.justReturnFromTextBox;
			if (justReturnFromTextBox)
			{
				this.tfChat.justReturnFromTextBox = false;
				this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
				this.tfChat.setText(string.Empty);
				this.right.caption = mResources.CLOSE;
			}
			bool isPC = Main.isPC;
			if (isPC)
			{
				bool flag2 = GameCanvas.keyPressed[15];
				if (flag2)
				{
					bool flag3 = this.left != null && this.tfChat.getText() != string.Empty;
					if (flag3)
					{
						this.left.performAction();
					}
					GameCanvas.keyPressed[15] = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
				bool flag4 = GameCanvas.keyPressed[14];
				if (flag4)
				{
					bool flag5 = this.right != null;
					if (flag5)
					{
						this.right.performAction();
					}
					GameCanvas.keyPressed[14] = false;
				}
			}
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0001C136 File Offset: 0x0001A336
	public void close()
	{
		this.tfChat.setText(string.Empty);
		this.isShow = false;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0001C154 File Offset: 0x0001A354
	public void paint(mGraphics g)
	{
		bool flag = !this.isShow;
		if (!flag)
		{
			bool isIPhone = Main.isIPhone;
			if (!isIPhone)
			{
				int num = (!Main.isWindowsPhone) ? (this.y - this.KC) : (this.tfChat.y - 5);
				int num2 = (!Main.isWindowsPhone) ? this.x : 0;
				int num3 = (!Main.isWindowsPhone) ? this.w : GameCanvas.w;
				PopUp.paintPopUp(g, num2, num, num3, this.h, -1, true);
				bool isPC = Main.isPC;
				if (isPC)
				{
					mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.tfChat.x, this.tfChat.y - ((!GameCanvas.isTouch) ? 12 : 17), 0);
					GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
				}
				this.tfChat.paint(g);
			}
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0001C25C File Offset: 0x0001A45C
	public void perform(int idAction, object p)
	{
		if (idAction != 8000)
		{
			if (idAction == 8001)
			{
				Cout.LogError("perform chat 8001");
				bool flag = this.tfChat.getText().Equals(string.Empty);
				if (flag)
				{
					this.isShow = false;
					this.parentScreen.onCancelChat();
				}
				this.tfChat.clear();
			}
		}
		else
		{
			Cout.LogError("perform chat 8000");
			bool flag2 = this.parentScreen != null;
			if (flag2)
			{
				long num = mSystem.currentTimeMillis();
				bool flag3 = num - this.lastChatTime < 1000L;
				if (!flag3)
				{
					this.lastChatTime = num;
					this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
					this.tfChat.setText(string.Empty);
					this.right.caption = mResources.CLOSE;
					this.tfChat.clearKb();
				}
			}
		}
	}

	// Token: 0x040002D1 RID: 721
	private static ChatTextField instance;

	// Token: 0x040002D2 RID: 722
	public TField tfChat;

	// Token: 0x040002D3 RID: 723
	public bool isShow;

	// Token: 0x040002D4 RID: 724
	public IChatable parentScreen;

	// Token: 0x040002D5 RID: 725
	private long lastChatTime;

	// Token: 0x040002D6 RID: 726
	public Command left;

	// Token: 0x040002D7 RID: 727
	public Command cmdChat;

	// Token: 0x040002D8 RID: 728
	public Command right;

	// Token: 0x040002D9 RID: 729
	public Command center;

	// Token: 0x040002DA RID: 730
	private int x;

	// Token: 0x040002DB RID: 731
	private int y;

	// Token: 0x040002DC RID: 732
	private int w;

	// Token: 0x040002DD RID: 733
	private int h;

	// Token: 0x040002DE RID: 734
	private bool isPublic;

	// Token: 0x040002DF RID: 735
	public Command cmdChat2;

	// Token: 0x040002E0 RID: 736
	public int yBegin;

	// Token: 0x040002E1 RID: 737
	public int yUp;

	// Token: 0x040002E2 RID: 738
	public int KC;

	// Token: 0x040002E3 RID: 739
	public string to;

	// Token: 0x040002E4 RID: 740
	public string strChat = "Chat ";
}
