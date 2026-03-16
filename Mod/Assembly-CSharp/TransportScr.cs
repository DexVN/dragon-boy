using System;

// Token: 0x020000CB RID: 203
public class TransportScr : mScreen, IActionListener
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x0009A634 File Offset: 0x00098A34
	public TransportScr()
	{
		this.posX = new int[this.n];
		this.posY = new int[this.n];
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] = Res.random(0, GameCanvas.w);
			this.posY[i] = i * (GameCanvas.h / this.n);
		}
		this.posX2 = new int[this.n];
		this.posY2 = new int[this.n];
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] = Res.random(0, GameCanvas.w);
			this.posY2[j] = j * (GameCanvas.h / this.n);
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0009A71A File Offset: 0x00098B1A
	public static TransportScr gI()
	{
		if (TransportScr.instance == null)
		{
			TransportScr.instance = new TransportScr();
		}
		return TransportScr.instance;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0009A738 File Offset: 0x00098B38
	public override void switchToMe()
	{
		if (TransportScr.ship == null)
		{
			TransportScr.ship = GameCanvas.loadImage("/mainImage/myTexture2dfutherShip.png");
		}
		if (TransportScr.taungam == null)
		{
			TransportScr.taungam = GameCanvas.loadImage("/mainImage/taungam.png");
		}
		this.isSpeed = false;
		this.transNow = false;
		if (global::Char.myCharz().checkLuong() > 0 && (int)this.type == 0)
		{
			this.center = new Command(mResources.faster, this, 1, null);
		}
		else
		{
			this.center = null;
		}
		this.currSpeed = 0;
		base.switchToMe();
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0009A7D0 File Offset: 0x00098BD0
	public override void paint(mGraphics g)
	{
		g.setColor(((int)this.type != 0) ? 3056895 : 0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		for (int i = 0; i < this.n; i++)
		{
			g.setColor(((int)this.type != 0) ? 11140863 : 14802654);
			g.fillRect(this.posX[i], this.posY[i], 10, 2);
		}
		if ((int)this.type == 0)
		{
			g.drawRegion(TransportScr.ship, 0, 0, 72, 95, 7, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		if ((int)this.type == 1)
		{
			g.drawRegion(TransportScr.taungam, 0, 0, 144, 79, 2, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		for (int j = 0; j < this.n; j++)
		{
			g.setColor(((int)this.type != 0) ? 7536127 : 14935011);
			g.fillRect(this.posX2[j], this.posY2[j], 18, 3);
		}
		base.paint(g);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x0009A920 File Offset: 0x00098D20
	public override void update()
	{
		if ((int)this.type == 0)
		{
			if (!this.isSpeed)
			{
				this.currSpeed = GameCanvas.w / 2 * (int)this.time / (int)this.maxTime;
			}
		}
		else
		{
			this.currSpeed += 2;
		}
		Controller.isStopReadMessage = false;
		this.cmx = (((GameCanvas.w / 2 + this.cmx) / 2 + this.cmx) / 2 + this.cmx) / 2;
		if ((int)this.type == 1)
		{
			this.cmx = 0;
		}
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] -= this.speed / 2;
			if (this.posX[i] < -20)
			{
				this.posX[i] = GameCanvas.w;
			}
		}
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] -= this.speed;
			if (this.posX2[j] < -20)
			{
				this.posX2[j] = GameCanvas.w;
			}
		}
		if (GameCanvas.gameTick % 3 == 0)
		{
			this.speed += ((!this.isSpeed) ? 1 : 2);
		}
		if (this.speed > ((!this.isSpeed) ? 25 : 80))
		{
			this.speed = ((!this.isSpeed) ? 25 : 80);
		}
		this.curr = mSystem.currentTimeMillis();
		if (this.curr - this.last >= 1000L)
		{
			this.time += 1;
			this.last = this.curr;
		}
		if (this.isSpeed)
		{
			this.currSpeed += 3;
		}
		if (this.currSpeed >= GameCanvas.w / 2 + 30 && !this.transNow)
		{
			this.transNow = true;
			Service.gI().transportNow();
		}
		base.update();
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0009AB3A File Offset: 0x00098F3A
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0009AB44 File Offset: 0x00098F44
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			GameCanvas.startYesNoDlg(mResources.fasterQuestion, new Command(mResources.YES, this, 2, null), new Command(mResources.NO, this, 3, null));
		}
		if (idAction == 2 && global::Char.myCharz().checkLuong() > 0)
		{
			this.isSpeed = true;
			GameCanvas.endDlg();
			this.center = null;
		}
		if (idAction == 3)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04001311 RID: 4881
	public static TransportScr instance;

	// Token: 0x04001312 RID: 4882
	public static Image ship;

	// Token: 0x04001313 RID: 4883
	public static Image taungam;

	// Token: 0x04001314 RID: 4884
	public sbyte type;

	// Token: 0x04001315 RID: 4885
	public int speed = 5;

	// Token: 0x04001316 RID: 4886
	public int[] posX;

	// Token: 0x04001317 RID: 4887
	public int[] posY;

	// Token: 0x04001318 RID: 4888
	public int[] posX2;

	// Token: 0x04001319 RID: 4889
	public int[] posY2;

	// Token: 0x0400131A RID: 4890
	private int cmx;

	// Token: 0x0400131B RID: 4891
	private int n = 20;

	// Token: 0x0400131C RID: 4892
	public short time;

	// Token: 0x0400131D RID: 4893
	public short maxTime;

	// Token: 0x0400131E RID: 4894
	public long last;

	// Token: 0x0400131F RID: 4895
	public long curr;

	// Token: 0x04001320 RID: 4896
	private bool isSpeed;

	// Token: 0x04001321 RID: 4897
	private bool transNow;

	// Token: 0x04001322 RID: 4898
	private int currSpeed;
}
