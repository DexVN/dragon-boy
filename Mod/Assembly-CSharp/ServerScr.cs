using System;

// Token: 0x020000C5 RID: 197
public class ServerScr : mScreen, IActionListener
{
	// Token: 0x060009F3 RID: 2547 RVA: 0x00097C00 File Offset: 0x00096000
	public ServerScr()
	{
		TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00097C64 File Offset: 0x00096064
	public override void switchToMe()
	{
		SoundMn.gI().stopAll();
		base.switchToMe();
		this.cmdGlobal = new Command("VIỆT NAM", this, 98, null);
		this.cmdGlobal.x = 0;
		this.cmdGlobal.y = 0;
		this.cmdVietNam = new Command("GLOBAL", this, 97, null);
		this.cmdVietNam.x = 50;
		this.cmdVietNam.y = 0;
		this.vecServer = new MyVector();
		this.vecServer.addElement(this.cmdGlobal);
		this.vecServer.addElement(this.cmdVietNam);
		this.sort();
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00097D10 File Offset: 0x00096110
	private void sort()
	{
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 2;
		if (GameCanvas.w > 3 * (this.wc + this.w2c))
		{
			this.numw = 3;
		}
		if (this.vecServer.size() < 3)
		{
			this.numw = 2;
		}
		this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			if (command != null)
			{
				int num = GameCanvas.hw - this.numw * (this.wc + this.w2c) / 2;
				int x = num + i % this.numw * (this.wc + this.w2c);
				int num2 = GameCanvas.hh - this.numh * (this.hc + this.w2c) / 2;
				int y = num2 + i / this.numw * (this.hc + this.w2c);
				command.x = x;
				command.y = y;
			}
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00097E6C File Offset: 0x0009626C
	public override void update()
	{
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			if (!GameCanvas.isTouch)
			{
				if (i == this.mainSelect)
				{
					if (GameCanvas.gameTick % 10 < 4)
					{
						command.isFocus = true;
					}
					else
					{
						command.isFocus = false;
					}
					this.cmdCheck = new Command(mResources.SELECT, this, command.idAction, null);
					this.center = this.cmdCheck;
				}
				else
				{
					command.isFocus = false;
				}
			}
			else if (command != null && command.isPointerPressInside())
			{
				command.performAction();
			}
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00097F54 File Offset: 0x00096354
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			if (this.vecServer.elementAt(i) != null)
			{
				((Command)this.vecServer.elementAt(i)).paint(g);
			}
		}
		base.paint(g);
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00097FB4 File Offset: 0x000963B4
	public override void updateKey()
	{
		base.updateKey();
		int num = this.mainSelect % this.numw;
		int num2 = this.mainSelect / this.numw;
		if (GameCanvas.keyPressed[4])
		{
			if (num > 0)
			{
				this.mainSelect--;
			}
			GameCanvas.keyPressed[4] = false;
		}
		else if (GameCanvas.keyPressed[6])
		{
			if (num < this.numw - 1)
			{
				this.mainSelect++;
			}
			GameCanvas.keyPressed[6] = false;
		}
		else if (GameCanvas.keyPressed[2])
		{
			if (num2 > 0)
			{
				this.mainSelect -= this.numw;
			}
			GameCanvas.keyPressed[2] = false;
		}
		else if (GameCanvas.keyPressed[8])
		{
			if (num2 < this.numh - 1)
			{
				this.mainSelect += this.numw;
			}
			GameCanvas.keyPressed[8] = false;
		}
		if (this.mainSelect < 0)
		{
			this.mainSelect = 0;
		}
		if (this.mainSelect >= this.vecServer.size())
		{
			this.mainSelect = this.vecServer.size() - 1;
		}
		if (GameCanvas.keyPressed[5])
		{
			((Command)this.vecServer.elementAt(num)).performAction();
			GameCanvas.keyPressed[5] = false;
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00098120 File Offset: 0x00096520
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 97:
			this.vecServer.removeAllElements();
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				if ((int)ServerListScreen.language[i] != 0)
				{
					this.vecServer.addElement(new Command(ServerListScreen.nameServer[i], this, 100 + i, null));
				}
			}
			this.sort();
			break;
		case 98:
			this.vecServer.removeAllElements();
			for (int j = 0; j < ServerListScreen.nameServer.Length; j++)
			{
				if ((int)ServerListScreen.language[j] == 0)
				{
					this.vecServer.addElement(new Command(ServerListScreen.nameServer[j], this, 100 + j, null));
				}
			}
			this.sort();
			break;
		case 99:
			Session_ME.gI().clearSendingMessage();
			ServerListScreen.ipSelect = this.mainSelect;
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		default:
			Session_ME.gI().clearSendingMessage();
			ServerListScreen.ipSelect = idAction - 100;
			Res.outz("Default:    ServerListScreen.ipSelect " + ServerListScreen.ipSelect);
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		}
	}

	// Token: 0x04001255 RID: 4693
	private int mainSelect;

	// Token: 0x04001256 RID: 4694
	private MyVector vecServer = new MyVector();

	// Token: 0x04001257 RID: 4695
	private Command cmdCheck;

	// Token: 0x04001258 RID: 4696
	public const int icmd = 100;

	// Token: 0x04001259 RID: 4697
	private int wc;

	// Token: 0x0400125A RID: 4698
	private int hc;

	// Token: 0x0400125B RID: 4699
	private int w2c;

	// Token: 0x0400125C RID: 4700
	private int numw;

	// Token: 0x0400125D RID: 4701
	private int numh;

	// Token: 0x0400125E RID: 4702
	private Command cmdGlobal;

	// Token: 0x0400125F RID: 4703
	private Command cmdVietNam;
}
