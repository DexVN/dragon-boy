using System;

// Token: 0x020000B9 RID: 185
public class MsgDlg : Dialog
{
	// Token: 0x06000858 RID: 2136 RVA: 0x00075E54 File Offset: 0x00074254
	public MsgDlg()
	{
		this.padLeft = 35;
		if (GameCanvas.w <= 176)
		{
			this.padLeft = 10;
		}
		if (GameCanvas.w > 320)
		{
			this.padLeft = 80;
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00075EA5 File Offset: 0x000742A5
	public void pleasewait()
	{
		this.setInfo(mResources.PLEASEWAIT, null, null, null);
		GameCanvas.currentDialog = this;
		this.time = mSystem.currentTimeMillis() + 5000L;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00075ECD File Offset: 0x000742CD
	public override void show()
	{
		GameCanvas.currentDialog = this;
		this.time = -1L;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00075EE0 File Offset: 0x000742E0
	public void setInfo(string info)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00075F44 File Offset: 0x00074344
	public void setInfo(string info, Command left, Command center, Command right)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.left = left;
		this.center = center;
		this.right = right;
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
		if (GameCanvas.isTouch)
		{
			if (left != null)
			{
				this.left.x = GameCanvas.w / 2 - 68 - 5;
				this.left.y = GameCanvas.h - 50;
			}
			if (right != null)
			{
				this.right.x = GameCanvas.w / 2 + 5;
				this.right.y = GameCanvas.h - 50;
			}
			if (center != null)
			{
				this.center.x = GameCanvas.w / 2 - 35;
				this.center.y = GameCanvas.h - 50;
			}
		}
		this.isWait = false;
		this.time = -1L;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00076060 File Offset: 0x00074460
	public override void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		int num = GameCanvas.h - this.h - 38;
		int w = GameCanvas.w - this.padLeft * 2;
		GameCanvas.paintz.paintPopUp(this.padLeft, num, w, this.h, g);
		int num2 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
		if (this.isWait)
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

	// Token: 0x0600085E RID: 2142 RVA: 0x0007614E File Offset: 0x0007454E
	public override void update()
	{
		base.update();
		if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04000FFE RID: 4094
	public string[] info;

	// Token: 0x04000FFF RID: 4095
	public bool isWait;

	// Token: 0x04001000 RID: 4096
	private int h;

	// Token: 0x04001001 RID: 4097
	private int padLeft;

	// Token: 0x04001002 RID: 4098
	private long time = -1L;
}
