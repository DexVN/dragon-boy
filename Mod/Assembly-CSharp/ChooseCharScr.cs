using System;

// Token: 0x02000013 RID: 19
public class ChooseCharScr : mScreen, IActionListener
{
	// Token: 0x06000168 RID: 360 RVA: 0x0001C444 File Offset: 0x0001A644
	public override void switchToMe()
	{
		ServerListScreen.isWait = false;
		global::Char.isLoadingMap = false;
		LoginScr.isContinueToLogin = false;
		ServerListScreen.waitToLogin = false;
		GameScr.gI().initSelectChar();
		base.switchToMe();
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0001C474 File Offset: 0x0001A674
	public override void update()
	{
		bool flag = GameCanvas.gameTick % 10 > 2;
		if (flag)
		{
			this.cf = 1;
		}
		else
		{
			this.cf = 0;
		}
		for (int i = 0; i < this.vc_players.Length; i++)
		{
			bool flag2 = this.vc_players[i].isPointerPressInside();
			if (flag2)
			{
				this.vc_players[i].performAction();
			}
		}
		for (int j = 0; j < this.cx.Length; j++)
		{
			bool flag3 = GameCanvas.isPointerHoldIn(this.cx[j] + this.offsetX, this.cy[j] + this.offsetY, this.rectPanel[2], 60);
			if (flag3)
			{
				bool isPointerDown = GameCanvas.isPointerDown;
				if (isPointerDown)
				{
					this.focus = j;
					break;
				}
				bool flag4 = !GameCanvas.isPointerJustRelease || GameCanvas.isPointerClick;
				if (flag4)
				{
				}
			}
		}
		base.update();
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0001C56C File Offset: 0x0001A76C
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		try
		{
			PopUp.paintPopUp(g, this.rectPanel[0] - 10, this.rectPanel[1], this.rectPanel[2] + 20, this.rectPanel[3], 16777215, true);
			bool flag = this.vc_players != null;
			if (flag)
			{
				for (int i = 0; i < this.vc_players.Length; i++)
				{
					this.vc_players[i].paint(g);
				}
			}
			bool flag2 = ChooseCharScr.playerData != null;
			if (flag2)
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
					bool flag3 = this.focus == j;
					if (flag3)
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

	// Token: 0x0600016B RID: 363 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
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

	// Token: 0x0600016C RID: 364 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
	public void perform(int idAction, object p)
	{
		bool flag = idAction != 1;
		if (flag)
		{
			bool flag2 = idAction == 2;
			if (flag2)
			{
				GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
			}
		}
		else
		{
			bool flag3 = this.focus != -1;
			if (flag3)
			{
				GameCanvas.startWaitDlg();
				Service.gI().finishUpdate(ChooseCharScr.playerData[this.focus].playerID);
			}
		}
	}

	// Token: 0x040002E5 RID: 741
	public Command[] vc_players;

	// Token: 0x040002E6 RID: 742
	public static PlayerData[] playerData;

	// Token: 0x040002E7 RID: 743
	private int cf;

	// Token: 0x040002E8 RID: 744
	private int[] cx = new int[]
	{
		GameCanvas.w / 2 - 100,
		GameCanvas.w / 2 - 100
	};

	// Token: 0x040002E9 RID: 745
	private int focus;

	// Token: 0x040002EA RID: 746
	private int[] cy = new int[2];

	// Token: 0x040002EB RID: 747
	private int[] rectPanel = new int[]
	{
		GameCanvas.w / 2 - 150,
		GameCanvas.h / 2 - 100,
		300,
		200
	};

	// Token: 0x040002EC RID: 748
	private int offsetY = -35;

	// Token: 0x040002ED RID: 749
	private int offsetX = -35;
}
