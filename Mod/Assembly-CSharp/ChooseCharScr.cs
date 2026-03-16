using System;

// Token: 0x020000A3 RID: 163
public class ChooseCharScr : mScreen, IActionListener
{
	// Token: 0x060006D0 RID: 1744 RVA: 0x0005CE62 File Offset: 0x0005B262
	public override void switchToMe()
	{
		ServerListScreen.isWait = false;
		global::Char.isLoadingMap = false;
		LoginScr.isContinueToLogin = false;
		ServerListScreen.waitToLogin = false;
		GameScr.gI().initSelectChar();
		base.switchToMe();
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0005CE8C File Offset: 0x0005B28C
	public override void update()
	{
		if (GameCanvas.gameTick % 10 > 2)
		{
			this.cf = 1;
		}
		else
		{
			this.cf = 0;
		}
		for (int i = 0; i < this.vc_players.Length; i++)
		{
			if (this.vc_players[i].isPointerPressInside())
			{
				this.vc_players[i].performAction();
			}
		}
		for (int j = 0; j < this.cx.Length; j++)
		{
			if (GameCanvas.isPointerHoldIn(this.cx[j] + this.offsetX, this.cy[j] + this.offsetY, this.rectPanel[2], 60))
			{
				if (GameCanvas.isPointerDown)
				{
					this.focus = j;
					break;
				}
				if (!GameCanvas.isPointerJustRelease || GameCanvas.isPointerClick)
				{
				}
			}
		}
		base.update();
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0005CF70 File Offset: 0x0005B370
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		try
		{
			PopUp.paintPopUp(g, this.rectPanel[0] - 10, this.rectPanel[1], this.rectPanel[2] + 20, this.rectPanel[3], 16777215, true);
			if (this.vc_players != null)
			{
				for (int i = 0; i < this.vc_players.Length; i++)
				{
					this.vc_players[i].paint(g);
				}
			}
			if (ChooseCharScr.playerData != null)
			{
				for (int j = 0; j < ChooseCharScr.playerData.Length; j++)
				{
					PopUp.paintPopUp(g, this.cx[j] - 20, this.cy[j] + this.offsetY, this.rectPanel[2], 60, 16777215, false);
					Part part = GameScr.parts[(int)ChooseCharScr.playerData[j].head];
					Part part2 = GameScr.parts[(int)ChooseCharScr.playerData[j].leg];
					Part part3 = GameScr.parts[(int)ChooseCharScr.playerData[j].body];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx[j] + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy[j] - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx[j] + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy[j] - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx[j] + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy[j] - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 0, 0);
					if (this.focus == j)
					{
						mFont.tahoma_7b_yellow.drawString(g, ChooseCharScr.playerData[j].name, this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY, 1);
						mFont.tahoma_7b_yellow.drawString(g, mResources.power_point + " " + Res.formatNumber2(ChooseCharScr.playerData[j].powpoint), this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY + mFont.tahoma_7b_yellow.getHeight(), 1);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, ChooseCharScr.playerData[j].name, this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY, 1);
						mFont.tahoma_7b_dark.drawString(g, mResources.power_point + " " + Res.formatNumber2(ChooseCharScr.playerData[j].powpoint), this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY + mFont.tahoma_7b_dark.getHeight(), 1);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Res.outz(ex.StackTrace);
		}
		base.paint(g);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0005D3A4 File Offset: 0x0005B7A4
	internal void updateChooseCharacter(byte len)
	{
		this.cx = new int[(int)len];
		this.cy = new int[(int)len];
		for (int i = 0; i < (int)len; i++)
		{
			this.cx[i] = this.rectPanel[0] + 20;
			this.cy[i] = i * 70 + this.rectPanel[1] + 50;
		}
		this.vc_players = new Command[2];
		this.vc_players[1] = new Command("Vào game", this, 1, null, this.rectPanel[0] + this.rectPanel[2] - 80 - 80, this.rectPanel[1] + this.rectPanel[3] - 30);
		this.vc_players[0] = new Command("Trờ ra", this, 2, null, this.rectPanel[0] + this.rectPanel[2] - 80, this.rectPanel[1] + this.rectPanel[3] - 30);
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0005D490 File Offset: 0x0005B890
	public void perform(int idAction, object p)
	{
		if (idAction != 1)
		{
			if (idAction == 2)
			{
				GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
			}
		}
		else if (this.focus != -1)
		{
			GameCanvas.startWaitDlg();
			Service.gI().finishUpdate(ChooseCharScr.playerData[this.focus].playerID);
		}
	}

	// Token: 0x04000C84 RID: 3204
	public Command[] vc_players;

	// Token: 0x04000C85 RID: 3205
	public static PlayerData[] playerData;

	// Token: 0x04000C86 RID: 3206
	private int cf;

	// Token: 0x04000C87 RID: 3207
	private int[] cx = new int[]
	{
		GameCanvas.w / 2 - 100,
		GameCanvas.w / 2 - 100
	};

	// Token: 0x04000C88 RID: 3208
	private int focus;

	// Token: 0x04000C89 RID: 3209
	private int[] cy = new int[2];

	// Token: 0x04000C8A RID: 3210
	private int[] rectPanel = new int[]
	{
		GameCanvas.w / 2 - 150,
		GameCanvas.h / 2 - 100,
		300,
		200
	};

	// Token: 0x04000C8B RID: 3211
	private int offsetY = -35;

	// Token: 0x04000C8C RID: 3212
	private int offsetX = -35;
}
