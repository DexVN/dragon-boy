using System;

// Token: 0x0200006C RID: 108
public class MobCapcha
{
	// Token: 0x06000581 RID: 1409 RVA: 0x000660A6 File Offset: 0x000642A6
	public static void init()
	{
		MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x000660B8 File Offset: 0x000642B8
	public static void paint(mGraphics g, int x, int y)
	{
		bool flag = !MobCapcha.isAttack;
		if (flag)
		{
			bool flag2 = GameCanvas.gameTick % 3 == 0;
			if (flag2)
			{
				bool flag3 = global::Char.myCharz().cdir == 1;
				if (flag3)
				{
					MobCapcha.cmtoX = x - 20 - GameScr.cmx;
				}
				bool flag4 = global::Char.myCharz().cdir == -1;
				if (flag4)
				{
					MobCapcha.cmtoX = x + 20 - GameScr.cmx;
				}
			}
			MobCapcha.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
		}
		else
		{
			MobCapcha.delay++;
			bool flag5 = MobCapcha.delay == 5;
			if (flag5)
			{
				MobCapcha.isAttack = false;
				MobCapcha.delay = 0;
			}
			MobCapcha.cmtoX = x - GameScr.cmx;
			MobCapcha.cmtoY = y - GameScr.cmy;
		}
		bool flag6 = MobCapcha.cmx > x - GameScr.cmx;
		if (flag6)
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
		bool flag7 = MobCapcha.isCreateMob;
		if (flag7)
		{
			MobCapcha.isCreateMob = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
		}
		bool flag8 = MobCapcha.explode;
		if (flag8)
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

	// Token: 0x06000583 RID: 1411 RVA: 0x000662E8 File Offset: 0x000644E8
	public static void moveCamera()
	{
		bool flag = MobCapcha.cmy != MobCapcha.cmtoY;
		if (flag)
		{
			MobCapcha.cmvy = MobCapcha.cmtoY - MobCapcha.cmy << 2;
			MobCapcha.cmdy += MobCapcha.cmvy;
			MobCapcha.cmy += MobCapcha.cmdy >> 4;
			MobCapcha.cmdy &= 15;
		}
		bool flag2 = MobCapcha.cmx != MobCapcha.cmtoX;
		if (flag2)
		{
			MobCapcha.cmvx = MobCapcha.cmtoX - MobCapcha.cmx << 2;
			MobCapcha.cmdx += MobCapcha.cmvx;
			MobCapcha.cmx += MobCapcha.cmdx >> 4;
			MobCapcha.cmdx &= 15;
		}
		MobCapcha.tF++;
		bool flag3 = MobCapcha.tF == 5;
		if (flag3)
		{
			MobCapcha.tF = 0;
			MobCapcha.f++;
			bool flag4 = MobCapcha.f > 2;
			if (flag4)
			{
				MobCapcha.f = 0;
			}
		}
	}

	// Token: 0x04000BC4 RID: 3012
	public static Image imgMob;

	// Token: 0x04000BC5 RID: 3013
	public static int cmtoY;

	// Token: 0x04000BC6 RID: 3014
	public static int cmy;

	// Token: 0x04000BC7 RID: 3015
	public static int cmdy;

	// Token: 0x04000BC8 RID: 3016
	public static int cmvy;

	// Token: 0x04000BC9 RID: 3017
	public static int cmyLim;

	// Token: 0x04000BCA RID: 3018
	public static int cmtoX;

	// Token: 0x04000BCB RID: 3019
	public static int cmx;

	// Token: 0x04000BCC RID: 3020
	public static int cmdx;

	// Token: 0x04000BCD RID: 3021
	public static int cmvx;

	// Token: 0x04000BCE RID: 3022
	public static int cmxLim;

	// Token: 0x04000BCF RID: 3023
	public static bool explode;

	// Token: 0x04000BD0 RID: 3024
	public static int delay;

	// Token: 0x04000BD1 RID: 3025
	public static bool isCreateMob;

	// Token: 0x04000BD2 RID: 3026
	public static int tF;

	// Token: 0x04000BD3 RID: 3027
	public static int f;

	// Token: 0x04000BD4 RID: 3028
	public static int dir;

	// Token: 0x04000BD5 RID: 3029
	public static bool isAttack;
}
