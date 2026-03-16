using System;

// Token: 0x020000A2 RID: 162
public class ChatTextField : IActionListener
{
	// Token: 0x060006C0 RID: 1728 RVA: 0x0005BED8 File Offset: 0x0005A2D8
	public ChatTextField()
	{
		this.tfChat = new TField();
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.name = "chat";
		if (Main.isWindowsPhone)
		{
			this.tfChat.strInfo = this.tfChat.name;
		}
		this.tfChat.width = GameCanvas.w - 6;
		if (Main.isPC && this.tfChat.width > 250)
		{
			this.tfChat.width = 250;
		}
		this.tfChat.height = mScreen.ITEM_HEIGHT + 2;
		this.tfChat.x = GameCanvas.w / 2 - this.tfChat.width / 2;
		this.tfChat.isFocus = true;
		this.tfChat.setMaxTextLenght(80);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0005BFEC File Offset: 0x0005A3EC
	public void initChatTextField()
	{
		this.left = new Command(mResources.OK, this, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
		this.center = null;
		this.w = this.tfChat.width + 20;
		this.h = this.tfChat.height + 26;
		this.x = GameCanvas.w / 2 - this.w / 2;
		this.y = this.tfChat.y - 18;
		if (Main.isPC && this.w > 320)
		{
			this.w = 320;
		}
		this.left.x = this.x;
		this.right.x = this.x + this.w - 68;
		if (GameCanvas.isTouch)
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
			if (this.parentScreen != null)
			{
				this.tfChat.setText(str);
				this.parentScreen.onChatFromMe(str, this.to);
				this.tfChat.setText(string.Empty);
				this.tfChat.clearKb();
				if (this.right != null)
				{
					this.right.performAction();
				}
			}
			this.isShow = false;
		};
		this.yBegin = this.tfChat.y;
		this.yUp = GameCanvas.h / 2 - 2 * this.tfChat.height;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0005C21E File Offset: 0x0005A61E
	public void updateWhenKeyBoardVisible()
	{
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0005C220 File Offset: 0x0005A620
	public void keyPressed(int keyCode)
	{
		if (this.isShow)
		{
			this.tfChat.keyPressed(keyCode);
		}
		if (this.tfChat.getText().Equals(string.Empty))
		{
			this.right.caption = mResources.CLOSE;
		}
		else
		{
			this.right.caption = mResources.DELETE;
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0005C284 File Offset: 0x0005A684
	public static ChatTextField gI()
	{
		return (ChatTextField.instance != null) ? ChatTextField.instance : (ChatTextField.instance = new ChatTextField());
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0005C2A8 File Offset: 0x0005A6A8
	public void startChat(int firstCharacter, IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.keyPressed(firstCharacter);
		if (!this.tfChat.getText().Equals(string.Empty) && GameCanvas.currentDialog == null)
		{
			this.parentScreen = parentScreen;
			this.isShow = true;
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0005C338 File Offset: 0x0005A738
	public void startChat(IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			this.tfChat.isFocus = true;
			if (!Main.isPC)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0005C3F8 File Offset: 0x0005A7F8
	public void startChat2(IChatable parentScreen, string to)
	{
		this.tfChat.setFocusWithKb(true);
		this.to = to;
		this.parentScreen = parentScreen;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			if (!Main.isPC)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat2);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0005C4AF File Offset: 0x0005A8AF
	public void updateKey()
	{
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0005C4B4 File Offset: 0x0005A8B4
	public void update()
	{
		if (!this.isShow)
		{
			return;
		}
		this.tfChat.update();
		if (Main.isWindowsPhone)
		{
			this.updateWhenKeyBoardVisible();
		}
		if (this.tfChat.justReturnFromTextBox)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		}
		if (Main.isPC)
		{
			if (GameCanvas.keyPressed[15])
			{
				if (this.left != null && this.tfChat.getText() != string.Empty)
				{
					this.left.performAction();
				}
				GameCanvas.keyPressed[15] = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			}
			if (GameCanvas.keyPressed[14])
			{
				if (this.right != null)
				{
					this.right.performAction();
				}
				GameCanvas.keyPressed[14] = false;
			}
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0005C5D5 File Offset: 0x0005A9D5
	public void close()
	{
		this.tfChat.setText(string.Empty);
		this.isShow = false;
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0005C5F0 File Offset: 0x0005A9F0
	public void paint(mGraphics g)
	{
		if (!this.isShow)
		{
			return;
		}
		if (Main.isIPhone)
		{
			return;
		}
		int num = (!Main.isWindowsPhone) ? (this.y - this.KC) : (this.tfChat.y - 5);
		int num2 = (!Main.isWindowsPhone) ? this.x : 0;
		int num3 = (!Main.isWindowsPhone) ? this.w : GameCanvas.w;
		PopUp.paintPopUp(g, num2, num, num3, this.h, -1, true);
		if (Main.isPC)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.tfChat.x, this.tfChat.y - ((!GameCanvas.isTouch) ? 12 : 17), 0);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}
		this.tfChat.paint(g);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0005C6FC File Offset: 0x0005AAFC
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 8000:
			Cout.LogError("perform chat 8000");
			if (this.parentScreen != null)
			{
				long num = mSystem.currentTimeMillis();
				if (num - this.lastChatTime < 1000L)
				{
					return;
				}
				this.lastChatTime = num;
				this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
				this.tfChat.setText(string.Empty);
				this.right.caption = mResources.CLOSE;
				this.tfChat.clearKb();
			}
			break;
		case 8001:
			Cout.LogError("perform chat 8001");
			if (this.tfChat.getText().Equals(string.Empty))
			{
				this.isShow = false;
				this.parentScreen.onCancelChat();
			}
			this.tfChat.clear();
			break;
		}
	}

	// Token: 0x04000C70 RID: 3184
	private static ChatTextField instance;

	// Token: 0x04000C71 RID: 3185
	public TField tfChat;

	// Token: 0x04000C72 RID: 3186
	public bool isShow;

	// Token: 0x04000C73 RID: 3187
	public IChatable parentScreen;

	// Token: 0x04000C74 RID: 3188
	private long lastChatTime;

	// Token: 0x04000C75 RID: 3189
	public Command left;

	// Token: 0x04000C76 RID: 3190
	public Command cmdChat;

	// Token: 0x04000C77 RID: 3191
	public Command right;

	// Token: 0x04000C78 RID: 3192
	public Command center;

	// Token: 0x04000C79 RID: 3193
	private int x;

	// Token: 0x04000C7A RID: 3194
	private int y;

	// Token: 0x04000C7B RID: 3195
	private int w;

	// Token: 0x04000C7C RID: 3196
	private int h;

	// Token: 0x04000C7D RID: 3197
	private bool isPublic;

	// Token: 0x04000C7E RID: 3198
	public Command cmdChat2;

	// Token: 0x04000C7F RID: 3199
	public int yBegin;

	// Token: 0x04000C80 RID: 3200
	public int yUp;

	// Token: 0x04000C81 RID: 3201
	public int KC;

	// Token: 0x04000C82 RID: 3202
	public string to;

	// Token: 0x04000C83 RID: 3203
	public string strChat = "Chat ";
}
