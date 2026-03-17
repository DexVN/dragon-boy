using System;

// Token: 0x02000099 RID: 153
public class ServerScr : mScreen, IActionListener
{
	// Token: 0x06000854 RID: 2132 RVA: 0x00093E60 File Offset: 0x00092060
	public ServerScr()
	{
		TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
		bool flag = TileMap.bgID == 5 || TileMap.bgID == 6;
		if (flag)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00093EC8 File Offset: 0x000920C8
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

	// Token: 0x06000856 RID: 2134 RVA: 0x00093F78 File Offset: 0x00092178
	private void sort()
	{
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 2;
		bool flag = GameCanvas.w > 3 * (this.wc + this.w2c);
		if (flag)
		{
			this.numw = 3;
		}
		bool flag2 = this.vecServer.size() < 3;
		if (flag2)
		{
			this.numw = 2;
		}
		this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			bool flag3 = command != null;
			if (flag3)
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

	// Token: 0x06000857 RID: 2135 RVA: 0x000940E8 File Offset: 0x000922E8
	public override void update()
	{
		GameScr.cmx++;
		bool flag = GameScr.cmx > GameCanvas.w * 3 + 100;
		if (flag)
		{
			GameScr.cmx = 100;
		}
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			bool flag2 = !GameCanvas.isTouch;
			if (flag2)
			{
				bool flag3 = i == this.mainSelect;
				if (flag3)
				{
					bool flag4 = GameCanvas.gameTick % 10 < 4;
					if (flag4)
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
			else
			{
				bool flag5 = command != null && command.isPointerPressInside();
				if (flag5)
				{
					command.performAction();
				}
			}
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x000941E8 File Offset: 0x000923E8
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			bool flag = this.vecServer.elementAt(i) != null;
			if (flag)
			{
				((Command)this.vecServer.elementAt(i)).paint(g);
			}
		}
		base.paint(g);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00094250 File Offset: 0x00092450
	public override void updateKey()
	{
		base.updateKey();
		int num = this.mainSelect % this.numw;
		int num2 = this.mainSelect / this.numw;
		bool flag = GameCanvas.keyPressed[4];
		if (flag)
		{
			bool flag2 = num > 0;
			if (flag2)
			{
				this.mainSelect--;
			}
			GameCanvas.keyPressed[4] = false;
		}
		else
		{
			bool flag3 = GameCanvas.keyPressed[6];
			if (flag3)
			{
				bool flag4 = num < this.numw - 1;
				if (flag4)
				{
					this.mainSelect++;
				}
				GameCanvas.keyPressed[6] = false;
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[2];
				if (flag5)
				{
					bool flag6 = num2 > 0;
					if (flag6)
					{
						this.mainSelect -= this.numw;
					}
					GameCanvas.keyPressed[2] = false;
				}
				else
				{
					bool flag7 = GameCanvas.keyPressed[8];
					if (flag7)
					{
						bool flag8 = num2 < this.numh - 1;
						if (flag8)
						{
							this.mainSelect += this.numw;
						}
						GameCanvas.keyPressed[8] = false;
					}
				}
			}
		}
		bool flag9 = this.mainSelect < 0;
		if (flag9)
		{
			this.mainSelect = 0;
		}
		bool flag10 = this.mainSelect >= this.vecServer.size();
		if (flag10)
		{
			this.mainSelect = this.vecServer.size() - 1;
		}
		bool flag11 = GameCanvas.keyPressed[5];
		if (flag11)
		{
			((Command)this.vecServer.elementAt(num)).performAction();
			GameCanvas.keyPressed[5] = false;
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000943E0 File Offset: 0x000925E0
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 97:
			this.vecServer.removeAllElements();
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				bool flag = ServerListScreen.language[i] != 0;
				if (flag)
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
				bool flag2 = ServerListScreen.language[j] == 0;
				if (flag2)
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
			Res.outz("Default:    ServerListScreen.ipSelect " + ServerListScreen.ipSelect.ToString());
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		}
	}

	// Token: 0x040010EB RID: 4331
	private int mainSelect;

	// Token: 0x040010EC RID: 4332
	private MyVector vecServer = new MyVector();

	// Token: 0x040010ED RID: 4333
	private Command cmdCheck;

	// Token: 0x040010EE RID: 4334
	public const int icmd = 100;

	// Token: 0x040010EF RID: 4335
	private int wc;

	// Token: 0x040010F0 RID: 4336
	private int hc;

	// Token: 0x040010F1 RID: 4337
	private int w2c;

	// Token: 0x040010F2 RID: 4338
	private int numw;

	// Token: 0x040010F3 RID: 4339
	private int numh;

	// Token: 0x040010F4 RID: 4340
	private Command cmdGlobal;

	// Token: 0x040010F5 RID: 4341
	private Command cmdVietNam;
}
