using System;

// Token: 0x02000074 RID: 116
public class MsgDlg : Dialog
{
	// Token: 0x060005B8 RID: 1464 RVA: 0x00069224 File Offset: 0x00067424
	public MsgDlg()
	{
		this.padLeft = 35;
		bool flag = GameCanvas.w <= 176;
		if (flag)
		{
			this.padLeft = 10;
		}
		bool flag2 = GameCanvas.w > 320;
		if (flag2)
		{
			this.padLeft = 80;
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00069280 File Offset: 0x00067480
	public void pleasewait()
	{
		this.setInfo(mResources.PLEASEWAIT, null, null, null);
		GameCanvas.currentDialog = this;
		this.time = mSystem.currentTimeMillis() + 5000L;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000692AA File Offset: 0x000674AA
	public override void show()
	{
		GameCanvas.currentDialog = this;
		this.time = -1L;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x000692BC File Offset: 0x000674BC
	public void setInfo(string info)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.h = 80;
		bool flag = this.info.Length >= 5;
		if (flag)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00069324 File Offset: 0x00067524
	public void setInfo(string info, Command left, Command center, Command right)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.left = left;
		this.center = center;
		this.right = right;
		this.h = 80;
		bool flag = this.info.Length >= 5;
		if (flag)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			bool flag2 = left != null;
			if (flag2)
			{
				this.left.x = GameCanvas.w / 2 - 68 - 5;
				this.left.y = GameCanvas.h - 50;
			}
			bool flag3 = right != null;
			if (flag3)
			{
				this.right.x = GameCanvas.w / 2 + 5;
				this.right.y = GameCanvas.h - 50;
			}
			bool flag4 = center != null;
			if (flag4)
			{
				this.center.x = GameCanvas.w / 2 - 35;
				this.center.y = GameCanvas.h - 50;
			}
		}
		this.isWait = false;
		this.time = -1L;
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0006945C File Offset: 0x0006765C
	public override void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool isContinueToLogin = LoginScr.isContinueToLogin;
		if (!isContinueToLogin)
		{
			int num = GameCanvas.h - this.h - 38;
			int w = GameCanvas.w - this.padLeft * 2;
			GameCanvas.paintz.paintPopUp(this.padLeft, num, w, this.h, g);
			int num2 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
			bool flag = this.isWait;
			if (flag)
			{
				num2 += 8;
				GameCanvas.paintShukiren(GameCanvas.hw, num2 - 12, g);
			}
			int i = 0;
			int num3 = num2;
			while (i < this.info.Length)
			{
				mFont.tahoma_7b_dark.drawString(g, this.info[i], GameCanvas.hw, num3, 2);
				i++;
				num3 += mFont.tahoma_8b.getHeight();
			}
			base.paint(g);
		}
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0006955C File Offset: 0x0006775C
	public override void update()
	{
		base.update();
		bool flag = this.time != -1L && mSystem.currentTimeMillis() > this.time;
		if (flag)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04000DE0 RID: 3552
	public string[] info;

	// Token: 0x04000DE1 RID: 3553
	public bool isWait;

	// Token: 0x04000DE2 RID: 3554
	private int h;

	// Token: 0x04000DE3 RID: 3555
	private int padLeft;

	// Token: 0x04000DE4 RID: 3556
	private long time = -1L;
}
