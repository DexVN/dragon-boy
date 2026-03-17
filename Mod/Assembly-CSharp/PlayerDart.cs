using System;

// Token: 0x0200008A RID: 138
public class PlayerDart
{
	// Token: 0x060007A2 RID: 1954 RVA: 0x0008BE20 File Offset: 0x0008A020
	public PlayerDart(global::Char charBelong, int dartType, SkillPaint sp, int x, int y)
	{
		this.skillPaint = sp;
		this.charBelong = charBelong;
		this.info = GameScr.darts[dartType];
		this.va = this.info.va;
		this.x = x;
		this.y = y;
		bool flag = charBelong.mobFocus == null;
		IMapObject mapObject;
		if (flag)
		{
			IMapObject charFocus = charBelong.charFocus;
			mapObject = charFocus;
		}
		else
		{
			mapObject = charBelong.mobFocus;
		}
		IMapObject mapObject2 = mapObject;
		this.setAngle(Res.angle(mapObject2.getX() - x, mapObject2.getY() - y));
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0008BEC6 File Offset: 0x0008A0C6
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0008BEFC File Offset: 0x0008A0FC
	public void update()
	{
		bool flag = !this.isActive;
		if (!flag)
		{
			bool flag2 = this.charBelong.mobFocus == null && this.charBelong.charFocus == null;
			if (flag2)
			{
				this.endMe();
			}
			else
			{
				bool flag3 = this.charBelong.mobFocus == null;
				IMapObject mapObject;
				if (flag3)
				{
					IMapObject charFocus = this.charBelong.charFocus;
					mapObject = charFocus;
				}
				else
				{
					mapObject = this.charBelong.mobFocus;
				}
				IMapObject mapObject2 = mapObject;
				for (int i = 0; i < (int)this.info.nUpdate; i++)
				{
					bool flag4 = this.info.tail.Length != 0;
					if (flag4)
					{
						this.darts.addElement(new SmallDart(this.x, this.y));
					}
					int num = (this.charBelong.getX() <= mapObject2.getX()) ? -10 : 10;
					this.dx = mapObject2.getX() + num - this.x;
					this.dy = mapObject2.getY() - mapObject2.getH() / 2 - this.y;
					this.life++;
					bool flag5 = Res.abs(this.dx) < 20 && Res.abs(this.dy) < 20;
					if (flag5)
					{
						bool flag6 = this.charBelong.charFocus != null && this.charBelong.charFocus.me;
						if (flag6)
						{
							this.charBelong.charFocus.doInjure(this.charBelong.charFocus.damHP, 0, this.charBelong.charFocus.isCrit, this.charBelong.charFocus.isMob);
						}
						this.endMe();
						return;
					}
					int num2 = Res.angle(this.dx, this.dy);
					bool flag7 = global::Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096;
					if (flag7)
					{
						bool flag8 = global::Math.abs(num2 - this.angle) < 15;
						if (flag8)
						{
							this.angle = num2;
						}
						else
						{
							bool flag9 = (num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180;
							if (flag9)
							{
								this.angle = Res.fixangle(this.angle + 15);
							}
							else
							{
								this.angle = Res.fixangle(this.angle - 15);
							}
						}
					}
					bool flag10 = !this.isSpeedUp && this.va < 8192;
					if (flag10)
					{
						this.va += 1024;
					}
					this.vx = this.va * Res.cos(this.angle) >> 10;
					this.vy = this.va * Res.sin(this.angle) >> 10;
					this.dx += this.vx;
					int num3 = this.dx >> 10;
					this.x += num3;
					this.dx &= 1023;
					this.dy += this.vy;
					int num4 = this.dy >> 10;
					this.y += num4;
					this.dy &= 1023;
				}
				for (int j = 0; j < this.darts.size(); j++)
				{
					SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
					smallDart.index++;
					bool flag11 = smallDart.index >= this.info.tail.Length;
					if (flag11)
					{
						this.darts.removeElementAt(j);
					}
				}
			}
		}
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0008C314 File Offset: 0x0008A514
	private void endMe()
	{
		bool flag = !this.charBelong.isUseSkillAfterCharge && this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			SoundMn.gI().explode_1();
		}
		this.charBelong.setAttack();
		bool me = this.charBelong.me;
		if (me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
		bool isUseSkillAfterCharge = this.charBelong.isUseSkillAfterCharge;
		if (isUseSkillAfterCharge)
		{
			this.charBelong.isUseSkillAfterCharge = false;
			bool flag2 = this.charBelong.isLockMove && this.charBelong.me && this.charBelong.statusMe != 14 && this.charBelong.statusMe != 5;
			if (flag2)
			{
				this.charBelong.isLockMove = false;
			}
			GameScr.gI().activeSuperPower(this.x, this.y);
		}
		this.charBelong.dart = null;
		this.charBelong.isCreateDark = false;
		this.charBelong.skillPaint = null;
		this.charBelong.skillPaintRandomPaint = null;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0008C444 File Offset: 0x0008A644
	public void paint(mGraphics g)
	{
		bool flag = !this.isActive;
		if (!flag)
		{
			int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
			int num2 = (int)MonsterDart.FRAME[num];
			int transform = MonsterDart.TRANSFORM[num];
			for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
			{
				SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
				SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
			}
			int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
			SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
			for (int j = 0; j < this.darts.size(); j++)
			{
				SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
				SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
			}
			SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
			for (int k = 0; k < this.darts.size(); k++)
			{
				SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
				bool flag2 = Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
				}
			}
			g.setColor(16711680);
		}
	}

	// Token: 0x04000FBC RID: 4028
	public global::Char charBelong;

	// Token: 0x04000FBD RID: 4029
	public DartInfo info;

	// Token: 0x04000FBE RID: 4030
	public MyVector darts = new MyVector();

	// Token: 0x04000FBF RID: 4031
	public int angle;

	// Token: 0x04000FC0 RID: 4032
	public int vx;

	// Token: 0x04000FC1 RID: 4033
	public int vy;

	// Token: 0x04000FC2 RID: 4034
	public int va;

	// Token: 0x04000FC3 RID: 4035
	public int x;

	// Token: 0x04000FC4 RID: 4036
	public int y;

	// Token: 0x04000FC5 RID: 4037
	public int z;

	// Token: 0x04000FC6 RID: 4038
	private int life;

	// Token: 0x04000FC7 RID: 4039
	private int dx;

	// Token: 0x04000FC8 RID: 4040
	private int dy;

	// Token: 0x04000FC9 RID: 4041
	public bool isActive = true;

	// Token: 0x04000FCA RID: 4042
	public bool isSpeedUp;

	// Token: 0x04000FCB RID: 4043
	public SkillPaint skillPaint;
}
