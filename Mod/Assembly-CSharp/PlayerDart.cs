using System;

// Token: 0x02000077 RID: 119
public class PlayerDart
{
	// Token: 0x06000407 RID: 1031 RVA: 0x00031FF4 File Offset: 0x000303F4
	public PlayerDart(global::Char charBelong, int dartType, SkillPaint sp, int x, int y)
	{
		this.skillPaint = sp;
		this.charBelong = charBelong;
		this.info = GameScr.darts[dartType];
		this.va = this.info.va;
		this.x = x;
		this.y = y;
		IMapObject mapObject;
		if (charBelong.mobFocus == null)
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

	// Token: 0x06000408 RID: 1032 RVA: 0x00032091 File Offset: 0x00030491
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000320C8 File Offset: 0x000304C8
	public void update()
	{
		if (!this.isActive)
		{
			return;
		}
		if (this.charBelong.mobFocus == null && this.charBelong.charFocus == null)
		{
			this.endMe();
		}
		else
		{
			IMapObject mapObject;
			if (this.charBelong.mobFocus == null)
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
				if (this.info.tail.Length > 0)
				{
					this.darts.addElement(new SmallDart(this.x, this.y));
				}
				int num = (this.charBelong.getX() <= mapObject2.getX()) ? -10 : 10;
				this.dx = mapObject2.getX() + num - this.x;
				this.dy = mapObject2.getY() - mapObject2.getH() / 2 - this.y;
				this.life++;
				if (Res.abs(this.dx) < 20 && Res.abs(this.dy) < 20)
				{
					if (this.charBelong.charFocus != null && this.charBelong.charFocus.me)
					{
						this.charBelong.charFocus.doInjure(this.charBelong.charFocus.damHP, 0, this.charBelong.charFocus.isCrit, this.charBelong.charFocus.isMob);
					}
					this.endMe();
					return;
				}
				int num2 = Res.angle(this.dx, this.dy);
				if (global::Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096)
				{
					if (global::Math.abs(num2 - this.angle) < 15)
					{
						this.angle = num2;
					}
					else if ((num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180)
					{
						this.angle = Res.fixangle(this.angle + 15);
					}
					else
					{
						this.angle = Res.fixangle(this.angle - 15);
					}
				}
				if (!this.isSpeedUp && this.va < 8192)
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
				if (smallDart.index >= this.info.tail.Length)
				{
					this.darts.removeElementAt(j);
				}
			}
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0003248C File Offset: 0x0003088C
	private void endMe()
	{
		if (!this.charBelong.isUseSkillAfterCharge && this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w)
		{
			SoundMn.gI().explode_1();
		}
		this.charBelong.setAttack();
		if (this.charBelong.me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
		if (this.charBelong.isUseSkillAfterCharge)
		{
			this.charBelong.isUseSkillAfterCharge = false;
			if (this.charBelong.isLockMove && this.charBelong.me && this.charBelong.statusMe != 14 && this.charBelong.statusMe != 5)
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

	// Token: 0x0600040B RID: 1035 RVA: 0x000325B0 File Offset: 0x000309B0
	public void paint(mGraphics g)
	{
		if (!this.isActive)
		{
			return;
		}
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
			if (Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent)
			{
				SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
			}
		}
		g.setColor(16711680);
	}

	// Token: 0x040006D9 RID: 1753
	public global::Char charBelong;

	// Token: 0x040006DA RID: 1754
	public DartInfo info;

	// Token: 0x040006DB RID: 1755
	public MyVector darts = new MyVector();

	// Token: 0x040006DC RID: 1756
	public int angle;

	// Token: 0x040006DD RID: 1757
	public int vx;

	// Token: 0x040006DE RID: 1758
	public int vy;

	// Token: 0x040006DF RID: 1759
	public int va;

	// Token: 0x040006E0 RID: 1760
	public int x;

	// Token: 0x040006E1 RID: 1761
	public int y;

	// Token: 0x040006E2 RID: 1762
	public int z;

	// Token: 0x040006E3 RID: 1763
	private int life;

	// Token: 0x040006E4 RID: 1764
	private int dx;

	// Token: 0x040006E5 RID: 1765
	private int dy;

	// Token: 0x040006E6 RID: 1766
	public bool isActive = true;

	// Token: 0x040006E7 RID: 1767
	public bool isSpeedUp;

	// Token: 0x040006E8 RID: 1768
	public SkillPaint skillPaint;
}
