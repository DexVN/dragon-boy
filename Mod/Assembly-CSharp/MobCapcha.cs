using System;

// Token: 0x0200006D RID: 109
public class MobCapcha
{
	// Token: 0x060003C8 RID: 968 RVA: 0x00031011 File Offset: 0x0002F411
	public static void init()
	{
		MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00031024 File Offset: 0x0002F424
	public static void paint(mGraphics g, int x, int y)
	{
		if (!MobCapcha.isAttack)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					MobCapcha.cmtoX = x - 20 - GameScr.cmx;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					MobCapcha.cmtoX = x + 20 - GameScr.cmx;
				}
			}
			MobCapcha.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
		}
		else
		{
			MobCapcha.delay++;
			if (MobCapcha.delay == 5)
			{
				MobCapcha.isAttack = false;
				MobCapcha.delay = 0;
			}
			MobCapcha.cmtoX = x - GameScr.cmx;
			MobCapcha.cmtoY = y - GameScr.cmy;
		}
		if (MobCapcha.cmx > x - GameScr.cmx)
		{
			MobCapcha.dir = -1;
		}
		else
		{
			MobCapcha.dir = 1;
		}
		g.drawImage(GameScr.imgCapcha, MobCapcha.cmx, MobCapcha.cmy - 40, 3);
		PopUp.paintPopUp(g, MobCapcha.cmx - 25, MobCapcha.cmy - 70, 50, 20, 16777215, false);
		mFont.tahoma_7b_dark.drawString(g, GameScr.gI().keyInput, MobCapcha.cmx, MobCapcha.cmy - 65, 2);
		if (MobCapcha.isCreateMob)
		{
			MobCapcha.isCreateMob = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
		}
		if (MobCapcha.explode)
		{
			MobCapcha.explode = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
			GameScr.gI().mobCapcha = null;
			MobCapcha.cmtoX = -GameScr.cmx;
			MobCapcha.cmtoY = -GameScr.cmy;
		}
		g.drawRegion(MobCapcha.imgMob, 0, MobCapcha.f * 40, 40, 40, (MobCapcha.dir != 1) ? 2 : 0, MobCapcha.cmx, MobCapcha.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 3);
		MobCapcha.moveCamera();
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0003123C File Offset: 0x0002F63C
	public static void moveCamera()
	{
		if (MobCapcha.cmy != MobCapcha.cmtoY)
		{
			MobCapcha.cmvy = MobCapcha.cmtoY - MobCapcha.cmy << 2;
			MobCapcha.cmdy += MobCapcha.cmvy;
			MobCapcha.cmy += MobCapcha.cmdy >> 4;
			MobCapcha.cmdy &= 15;
		}
		if (MobCapcha.cmx != MobCapcha.cmtoX)
		{
			MobCapcha.cmvx = MobCapcha.cmtoX - MobCapcha.cmx << 2;
			MobCapcha.cmdx += MobCapcha.cmvx;
			MobCapcha.cmx += MobCapcha.cmdx >> 4;
			MobCapcha.cmdx &= 15;
		}
		MobCapcha.tF++;
		if (MobCapcha.tF == 5)
		{
			MobCapcha.tF = 0;
			MobCapcha.f++;
			if (MobCapcha.f > 2)
			{
				MobCapcha.f = 0;
			}
		}
	}

	// Token: 0x04000679 RID: 1657
	public static Image imgMob;

	// Token: 0x0400067A RID: 1658
	public static int cmtoY;

	// Token: 0x0400067B RID: 1659
	public static int cmy;

	// Token: 0x0400067C RID: 1660
	public static int cmdy;

	// Token: 0x0400067D RID: 1661
	public static int cmvy;

	// Token: 0x0400067E RID: 1662
	public static int cmyLim;

	// Token: 0x0400067F RID: 1663
	public static int cmtoX;

	// Token: 0x04000680 RID: 1664
	public static int cmx;

	// Token: 0x04000681 RID: 1665
	public static int cmdx;

	// Token: 0x04000682 RID: 1666
	public static int cmvx;

	// Token: 0x04000683 RID: 1667
	public static int cmxLim;

	// Token: 0x04000684 RID: 1668
	public static bool explode;

	// Token: 0x04000685 RID: 1669
	public static int delay;

	// Token: 0x04000686 RID: 1670
	public static bool isCreateMob;

	// Token: 0x04000687 RID: 1671
	public static int tF;

	// Token: 0x04000688 RID: 1672
	public static int f;

	// Token: 0x04000689 RID: 1673
	public static int dir;

	// Token: 0x0400068A RID: 1674
	public static bool isAttack;
}
