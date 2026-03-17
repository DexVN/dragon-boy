using System;

// Token: 0x0200002F RID: 47
public class Effect_End
{
	// Token: 0x0600023B RID: 571 RVA: 0x0003763C File Offset: 0x0003583C
	public Effect_End(int type, int typeSub, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		this.f = 0;
		this.stt = 0;
		this.typeEffect = type;
		this.typeSub = typeSub;
		this.x = x;
		this.y = y;
		this.levelPaint = levelPaint;
		this.dir = dir;
		this.dir_nguoc = ((dir != -1) ? 0 : 2);
		this.time = mSystem.currentTimeMillis();
		this.timeRemove = timeRemove;
		this.isRemove = (this.isAddSub = false);
		this.n_frame = 4;
		bool flag = listObj != null;
		if (flag)
		{
			this.listObj = new Point[listObj.Length];
			for (int i = 0; i < this.listObj.Length; i++)
			{
				this.listObj[i] = listObj[i];
			}
		}
		this.get_Img_Skill();
		this.create_Effect();
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00037784 File Offset: 0x00035984
	public Effect_End(int type, int typeSub, int typePaint, global::Char charUse, Point target, int levelPaint, short timeRemove, short range)
	{
		this.f = 0;
		this.stt = 0;
		this.typeEffect = type;
		this.typeSub = typeSub;
		this.typePaint = typePaint;
		this.charUse = charUse;
		bool flag = charUse.containsCaiTrang(1265);
		if (flag)
		{
			bool flag2 = this.typeEffect == 21 || this.typeEffect == 22 || this.typeEffect == 23;
			if (flag2)
			{
				this.charUse.cx += 10 * this.charUse.cdir;
			}
			else
			{
				bool flag3 = this.typeEffect == 18 || this.typeEffect == 19 || this.typeEffect == 20;
				if (flag3)
				{
					this.charUse.cx += -15 * this.charUse.cdir;
				}
				else
				{
					this.charUse.cx += 15 * this.charUse.cdir;
				}
			}
		}
		this.x = this.charUse.cx;
		this.y = this.charUse.cy;
		this.dir = this.charUse.cdir;
		this.dir_nguoc = ((this.dir != -1) ? 0 : 2);
		this.target = target;
		this.levelPaint = levelPaint;
		this.time = mSystem.currentTimeMillis();
		this.timeRemove = timeRemove;
		this.range = (int)range;
		this.isRemove = (this.isAddSub = false);
		this.n_frame = 4;
		this.get_Img_Skill();
		this.create_Effect();
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00037998 File Offset: 0x00035B98
	public Effect_End(int type, int typeSub, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		this.f = 0;
		this.stt = 0;
		this.typeEffect = type;
		this.typeSub = typeSub;
		this.typePaint = typePaint;
		this.x = x;
		this.y = y;
		this.levelPaint = levelPaint;
		this.dir = dir;
		this.dir_nguoc = ((dir != -1) ? 0 : 2);
		this.time = mSystem.currentTimeMillis();
		this.timeRemove = timeRemove;
		this.isRemove = (this.isAddSub = false);
		this.n_frame = 4;
		bool flag = listObj != null;
		if (flag)
		{
			this.listObj = new Point[listObj.Length];
			for (int i = 0; i < this.listObj.Length; i++)
			{
				this.listObj[i] = listObj[i];
			}
		}
		this.get_Img_Skill();
		this.create_Effect();
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00037AE8 File Offset: 0x00035CE8
	public static Image getImage(int id)
	{
		bool flag = id < 0;
		Image result2;
		if (flag)
		{
			result2 = null;
		}
		else
		{
			string path = "/e/e_" + id.ToString() + ".png";
			Image result = null;
			try
			{
				result = mSystem.loadImage(path);
			}
			catch (Exception ex)
			{
			}
			result2 = result;
		}
		return result2;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00037B44 File Offset: 0x00035D44
	public static void setSoundSkill_END(int x, int y, int typeEffect)
	{
		try
		{
			int num = -1;
			int num2 = Res.random(3);
			bool flag = num >= 0;
			if (flag)
			{
				SoundMn.playSound(x, y, num, SoundMn.volume);
			}
		}
		catch (Exception ex)
		{
			Res.err("ERR setSoundSkill_END: " + ex.ToString());
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00037BA8 File Offset: 0x00035DA8
	public void create_Effect()
	{
		try
		{
			Effect_End.setSoundSkill_END(this.x, this.y, this.typeEffect);
			int num = this.typeEffect;
			switch (num)
			{
			case 16:
			case 17:
				this.set_Sub();
				break;
			case 18:
			case 19:
			case 20:
				this.set_Pow();
				break;
			case 21:
			case 22:
			case 23:
				this.set_Gong();
				break;
			case 24:
				this.set_Skill_Kamex10();
				break;
			case 25:
				this.set_Skill_Destroy();
				break;
			case 26:
				this.set_Skill_MaFuba();
				break;
			default:
				switch (num)
				{
				case 0:
				case 1:
				case 2:
					this.set_End_String(this.typeEffect);
					break;
				case 3:
					this.set_FireWork();
					break;
				case 9:
					this.set_LINE_IN();
					break;
				case 10:
				case 11:
					this.set_End_Rock();
					break;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Res.err("ERR create_Effect: " + ex.ToString());
			this.removeEff();
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00037D00 File Offset: 0x00035F00
	public void update()
	{
		try
		{
			this.f++;
			int num = this.typeEffect;
			switch (num)
			{
			case 16:
			case 17:
				this.upd_Sub();
				break;
			case 18:
			case 19:
			case 20:
				this.upd_Pow();
				break;
			case 21:
			case 22:
			case 23:
				this.upd_Gong();
				break;
			case 24:
				this.upd_Skill_Kamex10();
				break;
			case 25:
				this.upd_Skill_Destroy();
				break;
			case 26:
				this.upd_Skill_MaFuba();
				break;
			default:
				switch (num)
				{
				case 0:
				case 1:
				case 2:
					this.upd_End_String();
					break;
				case 3:
					this.upd_FireWork();
					break;
				case 9:
					this.upd_LINE_IN();
					break;
				case 10:
				case 11:
					this.upd_End_Rock();
					break;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Res.err("ERR update: " + ex.ToString());
			this.removeEff();
		}
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00037E38 File Offset: 0x00036038
	public void paint(mGraphics g)
	{
		try
		{
			bool flag = !this.isRemove && this.f >= 0;
			if (flag)
			{
				int num = this.typeEffect;
				switch (num)
				{
				case 16:
				{
					bool flag2 = this.typeSub == 0;
					if (flag2)
					{
						this.pnt_Sub(g, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						this.pnt_Sub(g, mGraphics.VCENTER | mGraphics.HCENTER);
					}
					break;
				}
				case 17:
					this.pnt_Sub(g, mGraphics.VCENTER);
					break;
				case 18:
				case 19:
				case 20:
					this.pnt_Pow(g, mGraphics.BOTTOM | mGraphics.HCENTER);
					break;
				case 21:
				case 22:
				case 23:
					this.pnt_Gong(g, mGraphics.VCENTER | mGraphics.HCENTER);
					break;
				case 24:
					this.pnt_Skill_Kamex10(g);
					break;
				case 25:
					this.pnt_Skill_Destroy(g);
					break;
				case 26:
					this.pnt_Skill_MaFuba(g);
					break;
				default:
					switch (num)
					{
					case 0:
					case 1:
					case 2:
						this.pnt_End_String(g);
						break;
					case 3:
						this.pnt_FireWork(g);
						break;
					case 9:
						this.pnt_LINE_IN(g);
						break;
					case 10:
					case 11:
						this.pnt_End_Rock(g);
						break;
					}
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Res.err(ex.ToString());
			this.removeEff();
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00037FF0 File Offset: 0x000361F0
	public void removeEff()
	{
		this.isRemove = true;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00037FFC File Offset: 0x000361FC
	public void createDanFocus(bool isRandom, global::Char obj)
	{
		if (isRandom)
		{
			switch (Res.random(4))
			{
			case 0:
				this.gocT_Arc = 90;
				break;
			case 1:
				this.gocT_Arc = 270;
				break;
			case 2:
				this.gocT_Arc = 180;
				break;
			case 3:
				this.gocT_Arc = 0;
				break;
			}
		}
		else
		{
			bool flag = obj.cdir == 1;
			if (flag)
			{
				this.gocT_Arc = 0;
			}
			else
			{
				this.gocT_Arc = 180;
			}
		}
		this.va = (int)((short)(256 * this.vMax));
		this.vx = 0;
		this.vy = 0;
		this.life = 0;
		this.vx1000 = this.va * Res.cos(this.gocT_Arc) >> 10;
		this.vy1000 = this.va * Res.sin(this.gocT_Arc) >> 10;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x000380E4 File Offset: 0x000362E4
	public void updateAngleXP(int fmove)
	{
		bool flag = this.f < fmove;
		if (!flag)
		{
			bool flag2 = this.charUse == null || this.target == null || this.f >= this.fRemove;
			if (flag2)
			{
				this.f = this.fRemove;
			}
			else
			{
				int num = this.target.x - this.charUse.cx;
				int num2 = this.target.y - this.charUse.cy;
				this.life++;
				bool flag3 = (Res.abs(num) < 10 && Res.abs(num2) < 10) || this.life > this.fRemove;
				if (flag3)
				{
					this.f = this.fRemove;
				}
				else
				{
					int num3 = Res.angle(num, num2);
					bool flag4 = Res.abs(num3 - this.gocT_Arc) < 90 || num * num + num2 * num2 > 4096;
					if (flag4)
					{
						bool flag5 = Res.abs(num3 - this.gocT_Arc) < 15;
						if (flag5)
						{
							this.gocT_Arc = num3;
						}
						else
						{
							bool flag6 = (num3 - this.gocT_Arc >= 0 && num3 - this.gocT_Arc < 180) || num3 - this.gocT_Arc < -180;
							if (flag6)
							{
								this.gocT_Arc = Res.fixangle(this.gocT_Arc + 15);
							}
							else
							{
								this.gocT_Arc = Res.fixangle(this.gocT_Arc - 15);
							}
						}
					}
					bool flag7 = this.f > this.fRemove * 2 / 3 && this.va < 8192;
					if (flag7)
					{
						this.va += 3096;
					}
					this.vx1000 = this.va * Res.cos(this.gocT_Arc) >> 10;
					this.vy1000 = this.va * Res.sin(this.gocT_Arc) >> 10;
					num += this.vx1000;
					int num4 = num >> 10;
					this.x += num4;
					num &= 1023;
					num2 += this.vy1000;
					int num5 = num2 >> 10;
					this.y += num5;
					num2 &= 1023;
				}
			}
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00038334 File Offset: 0x00036534
	public int setFrameAngle(int goc)
	{
		bool flag = goc <= 15 || goc > 345;
		int result;
		if (flag)
		{
			result = 12;
		}
		else
		{
			int num = (goc - 15) / 15 + 1;
			bool flag2 = num > 24;
			if (flag2)
			{
				num = 24;
			}
			result = (int)this.mpaintone_Arrow[num];
		}
		return result;
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00038388 File Offset: 0x00036588
	public void create_Arrow(int vMax, Point targetPoint)
	{
		this.vMax = vMax;
		bool flag = targetPoint != null;
		int num;
		int num2;
		if (flag)
		{
			num = targetPoint.x - this.x;
			num2 = targetPoint.y - this.y;
			this.toX = targetPoint.x;
			this.toY = targetPoint.y;
		}
		else
		{
			num = this.toX - this.x;
			num2 = this.toY - this.y;
		}
		bool flag2 = this.x > this.toX;
		if (flag2)
		{
			this.dir = 2;
			this.dir_nguoc = 0;
		}
		else
		{
			this.dir = 0;
			this.dir_nguoc = 2;
		}
		int frameAngle = Res.angle(num, num2);
		this.frame = this.setFrameAngle(frameAngle);
		this.fSpeed = this.frame;
		this.create_Speed(num, num2);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0003845C File Offset: 0x0003665C
	public void create_Speed(int dx, int dy)
	{
		int num = Res.getDistance(dx, dy) / this.vMax;
		bool flag = num == 0;
		if (flag)
		{
			num = 1;
		}
		int num2 = dx / num;
		int num3 = dy / num;
		bool flag2 = num2 == 0 && dx < num;
		if (flag2)
		{
			num2 = ((dx >= 0) ? 1 : -1);
		}
		bool flag3 = num3 == 0 && dy < num;
		if (flag3)
		{
			num3 = ((dy >= 0) ? 1 : -1);
		}
		bool flag4 = Res.abs(num2) > Res.abs(dx);
		if (flag4)
		{
			num2 = dx;
		}
		bool flag5 = Res.abs(num3) > Res.abs(dy);
		if (flag5)
		{
			num3 = dy;
		}
		this.vx = num2;
		this.vy = num3;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00038504 File Offset: 0x00036704
	public void moveTo_xy(int toX, int toY, int fMove, int typeEff_End, int rangeEnd)
	{
		bool flag = this.f < fMove;
		if (flag)
		{
			this.frame = this.setFrameAngle((this.dir != -1) ? 0 : 180);
		}
		else
		{
			this.frame = this.fSpeed;
			bool flag2 = Res.abs(this.x - toX) < Res.abs(this.vx);
			if (flag2)
			{
				this.x = toX;
				this.vx = 0;
			}
			else
			{
				this.x += this.vx;
			}
			bool flag3 = Res.abs(this.y - toY) < Res.abs(this.vy);
			if (flag3)
			{
				this.y = toY;
				this.vy = 0;
			}
			else
			{
				this.y += this.vy;
			}
			bool flag4 = Res.abs(this.x - toX) < Res.abs(this.vMax) && Res.abs(this.y - toY) < Res.abs(this.vMax) && typeEff_End >= 0;
			if (flag4)
			{
				bool flag5 = this.target != null;
				if (flag5)
				{
					int num = this.target.x;
					int num2 = this.target.y;
					bool flag6 = rangeEnd > 0;
					if (flag6)
					{
						num += Res.random_Am(0, rangeEnd);
						num2 += Res.random_Am(0, rangeEnd);
					}
					GameScr.addEffectEnd(typeEff_End, 0, 0, num, num2, 1, 0, -1, null);
					this.removeEff();
				}
				else
				{
					bool flag7 = this.isAddSub;
					if (flag7)
					{
						this.isAddSub = false;
						int num3 = this.x;
						int num4 = this.y;
						bool flag8 = rangeEnd > 1;
						if (flag8)
						{
							num3 += Res.random_Am_0(rangeEnd);
							num4 += Res.random_Am_0(rangeEnd);
						}
						GameScr.addEffectEnd(typeEff_End, 0, 0, num3, num4, 1, 0, -1, null);
					}
				}
			}
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x000386EC File Offset: 0x000368EC
	public void paint_Arrow(mGraphics g, FrameImage frm, int index, int x, int y, int anchor, bool isCountFr)
	{
		bool flag = frm == null;
		if (!flag)
		{
			int num = frm.nFrame / 3;
			bool flag2 = num < 1;
			if (flag2)
			{
				num = 1;
			}
			int num2 = 3;
			bool flag3 = frm.nFrame > 6;
			int num3;
			if (flag3)
			{
				num = 1;
				bool flag4 = this.f / num2 - this.fMove > 8;
				if (flag4)
				{
					num3 = 6;
				}
				else
				{
					bool flag5 = this.f / num2 - this.fMove > 4;
					if (flag5)
					{
						num3 = 3;
					}
					else
					{
						num3 = 0;
					}
				}
			}
			else
			{
				bool flag6 = frm.nFrame > 3;
				if (flag6)
				{
					num3 = ((this.f / num2 % 2 != 0) ? 3 : 0);
				}
				else
				{
					num3 = this.f % num;
				}
			}
			int idx = num * (int)this.mImageArrow[index] + num3;
			bool flag7 = frm.nFrame < 3;
			if (flag7)
			{
				idx = this.f / num2 % frm.nFrame;
			}
			if (isCountFr)
			{
				idx = this.f / num2 % frm.nFrame;
			}
			frm.drawFrame(idx, x, y, (int)this.mXoayArrow[index], anchor, g);
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00038808 File Offset: 0x00036A08
	private void set_End_String(int typeEffect)
	{
		bool flag = typeEffect != 0;
		if (flag)
		{
			bool flag2 = typeEffect != 1;
			if (flag2)
			{
				bool flag3 = typeEffect == 2;
				if (flag3)
				{
					this.fraImgEff = new FrameImage(6);
				}
			}
			else
			{
				this.fraImgEff = new FrameImage(5);
			}
		}
		else
		{
			this.fraImgEff = new FrameImage(4);
		}
		this.fRemove = 100;
		this.dy_throw = GameCanvas.h / 3 + 10;
		this.vy = 10;
		this.y1000 = 0;
		this.isAddSub = false;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00038894 File Offset: 0x00036A94
	private void upd_End_String()
	{
		this.x = GameCanvas.hw;
		this.y = this.y1000;
		bool flag = this.f > this.fRemove;
		if (flag)
		{
			this.removeEff();
		}
		this.vy++;
		bool flag2 = this.vy > 15;
		if (flag2)
		{
			this.vy = 15;
		}
		bool flag3 = this.y1000 + this.vy < this.dy_throw;
		if (flag3)
		{
			this.y1000 += this.vy;
		}
		else
		{
			this.y1000 = this.dy_throw;
			bool flag4 = !this.isAddSub;
			if (flag4)
			{
				this.isAddSub = true;
				bool flag5 = this.typeSub != -1;
				if (flag5)
				{
					GameScr.addEffectEnd(this.typeSub, 0, 0, this.x, this.y, this.levelPaint, 0, -1, null);
				}
			}
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00038988 File Offset: 0x00036B88
	private void pnt_End_String(mGraphics g)
	{
		bool flag = this.fraImgEff != null;
		if (flag)
		{
			this.fraImgEff.drawFrame(this.f / 5 % this.fraImgEff.nFrame, this.x, this.y, 0, 33, g);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000389D8 File Offset: 0x00036BD8
	private void set_FireWork()
	{
		int num = Res.random(3, 5);
		this.fRemove = 90;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = this.x + Res.random_Am_0(4);
			point.y = this.y + Res.random_Am_0(5);
			bool flag = this.typeSub == 0;
			if (flag)
			{
				point.fRe = Res.random(10);
				int num2 = 1;
				bool flag2 = i % 2 == 0;
				if (flag2)
				{
					num2 = -1;
				}
				point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num2;
				point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
				point.fraImgEff = new FrameImage(7);
			}
			this.VecEffEnd.addElement(point);
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00038AC4 File Offset: 0x00036CC4
	private void upd_FireWork()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			point.update();
			bool flag = point.f == point.fRe;
			if (flag)
			{
				SoundMn.playSound(point.x, point.y, SoundMn.FIREWORK, SoundMn.volume);
			}
			bool flag2 = point.f - point.fRe > point.fraImgEff.nFrame * 3 - 1;
			if (flag2)
			{
				point.f = 0;
				bool flag3 = this.typeSub == 0;
				if (flag3)
				{
					point.fRe = Res.random(10);
					int num = 1;
					bool flag4 = i % 2 == 0;
					if (flag4)
					{
						num = -1;
					}
					point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num;
					point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
				}
			}
		}
		bool flag5 = this.f >= this.fRemove;
		if (flag5)
		{
			this.removeEff();
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00038BFC File Offset: 0x00036DFC
	private void pnt_FireWork(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			bool flag = point.f - point.fRe > -1 && point.fraImgEff != null;
			if (flag)
			{
				point.fraImgEff.drawFrame((point.f - point.fRe) / 3 % point.fraImgEff.nFrame, point.x, point.y, 0, 3, g);
			}
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00038C94 File Offset: 0x00036E94
	private void set_Skill_Kamex10()
	{
		this.w = this.fra_skill[0].frameWidth;
		this.h = this.fra_skill[0].frameHeight;
		this.vMax = Res.abs(this.x - this.target.x);
		this.nFrame = new byte[]
		{
			0,
			0,
			0,
			1,
			1,
			1
		};
		this.isAddSub = false;
		SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_1, SoundMn.volume);
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00038D20 File Offset: 0x00036F20
	private void upd_Skill_Kamex10()
	{
		this.fSpeed++;
		this.w += 20;
		bool flag = this.w > this.vMax;
		if (flag)
		{
			this.w = this.vMax;
		}
		this.x = this.charUse.cx + 10;
		this.y = this.charUse.cy - 3;
		bool flag2 = this.dir == -1;
		if (flag2)
		{
			this.x = this.charUse.cx - this.w - 10;
		}
		bool flag3 = !this.isAddSub && GameCanvas.timeNow - this.time >= (long)this.timeRemove;
		if (flag3)
		{
			this.f = 0;
			this.nFrame = new byte[]
			{
				2,
				2,
				2,
				3,
				3,
				3
			};
			this.isAddSub = true;
		}
		bool flag4 = this.f > this.nFrame.Length - 1;
		if (flag4)
		{
			bool flag5 = this.isAddSub;
			if (flag5)
			{
				this.removeEff();
			}
			else
			{
				this.f = 0;
			}
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00038E44 File Offset: 0x00037044
	private void pnt_Skill_Kamex10(mGraphics g)
	{
		bool flag = this.fra_skill == null;
		if (!flag)
		{
			g.setClip(this.x, this.y - this.h / 2, this.w, this.h);
			this.Fill_Rect_Img(g, this.fra_skill[0], this.fra_skill[1], this.fra_skill[2], (int)this.nFrame[this.f], this.x, this.y, this.vMax);
			GameCanvas.resetTransGameScr(g);
			bool flag2 = this.dir == -1 && this.fra_skill[0] != null;
			if (flag2)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.f], this.x + this.w - this.fra_skill[0].frameWidth, this.y - this.fra_skill[0].frameHeight / 2 - 1, 2, 0, g);
			}
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00038F44 File Offset: 0x00037144
	private void set_Skill_Destroy()
	{
		this.x = this.charUse.cx + 20 * this.charUse.cdir;
		int num = 15;
		this.fMove = (int)this.timeRemove / num;
		bool flag = this.target != null;
		if (flag)
		{
			for (int i = 0; i < num; i++)
			{
				Point point = new Point();
				point.fraImgEff = this.fra_skill[0];
				point.fraImgEff_2 = this.fra_skill[2];
				point.x = this.x;
				point.y = this.y;
				bool flag2 = this.target != null;
				if (flag2)
				{
					point.toX = this.target.x;
					point.toY = this.target.y;
					bool flag3 = this.range > 0;
					if (flag3)
					{
						point.toX += Res.random_Am(0, this.range);
						point.toY += Res.random_Am(0, this.range);
					}
				}
				this.vMax = Res.random(9, 12);
				bool flag4 = i == num - 1;
				if (flag4)
				{
					point.fraImgEff = this.fra_skill[1];
					point.fraImgEff_2 = this.fra_skill[3];
					point.toX = this.target.x;
					point.toY = this.target.y;
					this.vMax = 9;
				}
				point.isPaint = false;
				point.isChange = false;
				point.isRemove = false;
				point.create_Arrow(this.vMax);
				this.VecEffEnd.addElement(point);
			}
		}
		else
		{
			this.removeEff();
		}
	}

	// Token: 0x06000255 RID: 597 RVA: 0x000390FC File Offset: 0x000372FC
	private void upd_Skill_Destroy()
	{
		int num = 0;
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			bool flag = !point.isPaint && GameCanvas.timeNow - this.time >= (long)(i * this.fMove);
			if (flag)
			{
				point.isPaint = true;
				GameScr.addEffectEnd(17, 0, this.typePaint, this.charUse.cx, this.charUse.cy - 3, 2, this.dir_nguoc, -1, null);
				bool flag2 = i == this.VecEffEnd.size() - 1;
				if (flag2)
				{
					SoundMn.playSound(point.x, point.y, SoundMn.DESTROY_1, SoundMn.volume);
				}
				else
				{
					SoundMn.playSound(point.x, point.y, SoundMn.DESTROY_0, SoundMn.volume);
				}
			}
			bool flag3 = point.isPaint && !point.isRemove;
			if (flag3)
			{
				point.f++;
				bool flag4 = !point.isChange;
				if (flag4)
				{
					bool flag5 = point.f < 10 && i == this.VecEffEnd.size() - 1 && this.charUse != null && !TileMap.tileTypeAt(this.charUse.cx - (this.charUse.chw + 1) * this.charUse.cdir, this.charUse.cy, (this.charUse.cdir != 1) ? 4 : 8);
					if (flag5)
					{
						this.charUse.cx -= this.charUse.cdir;
					}
					point.moveTo_xy(point.toX, point.toY);
					bool flag6 = point.x == point.toX;
					if (flag6)
					{
						point.isChange = true;
						point.f = 0;
					}
				}
				bool flag7 = point.isChange && point.f >= this.n_frame * point.fraImgEff_2.nFrame;
				if (flag7)
				{
					point.isRemove = true;
				}
			}
			bool flag8 = point.isRemove;
			if (flag8)
			{
				num++;
			}
		}
		bool flag9 = num == this.VecEffEnd.size();
		if (flag9)
		{
			this.removeEff();
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00039370 File Offset: 0x00037570
	private void pnt_Skill_Destroy(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			bool flag = point.isPaint && !point.isRemove;
			if (flag)
			{
				bool flag2 = !point.isChange;
				if (flag2)
				{
					point.paint_Arrow(g, point.fraImgEff, mGraphics.VCENTER | mGraphics.HCENTER, false);
				}
				bool isChange = point.isChange;
				if (isChange)
				{
					point.fraImgEff_2.drawFrame(point.f / this.n_frame % point.fraImgEff_2.nFrame, point.x, point.y, this.dir_nguoc, mGraphics.VCENTER | mGraphics.HCENTER, g);
				}
			}
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00039448 File Offset: 0x00037648
	private void set_Skill_MaFuba()
	{
		this.nFrame = new byte[]
		{
			0,
			0,
			0,
			1,
			1,
			1,
			2,
			2,
			2
		};
		this.isAddSub = false;
		this.fMove = 10;
		this.x1000 = this.x;
		this.y1000 = this.y + 12;
		this.dy = 25;
		this.dy_throw = 19;
		bool flag = this.typeSub == 1;
		if (flag)
		{
			this.dy_throw = 21;
		}
		else
		{
			bool flag2 = this.typeSub == 2;
			if (flag2)
			{
				this.dy_throw = 31;
			}
		}
		this.h = this.fra_skill[1].frameHeight + 50 - this.dy_throw;
		this.vy = 1;
		this.vy1000 = 1;
		this.y = this.y1000 - this.h;
		this.rS = 90;
		this.vMax = 1;
		this.angleS = (this.angleO = 25);
		this.iDotS = 1;
		bool flag3 = this.listObj != null && this.listObj.Length != 0;
		if (flag3)
		{
			this.iDotS = this.listObj.Length;
		}
		this.iAngleS = 360 / this.iDotS;
		this.xArgS = new int[this.iDotS];
		this.yArgS = new int[this.iDotS];
		this.xDotS = new int[this.iDotS];
		this.yDotS = new int[this.iDotS];
		GameScr.addEffectEnd(16, 0, this.typePaint, this.x1000, this.y1000, 1, 0, -1, null);
		SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_0, SoundMn.volume);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x000395F8 File Offset: 0x000377F8
	private void changeAngleStar()
	{
		bool flag = this.vMax < 40;
		if (flag)
		{
			this.vMax += 2;
		}
		this.angleS = this.angleO;
		this.angleS -= this.vMax;
		bool flag2 = this.angleS >= 360;
		if (flag2)
		{
			this.angleS -= 360;
		}
		bool flag3 = this.angleS < 0;
		if (flag3)
		{
			this.angleS = 360 + this.angleS;
		}
		this.angleO = this.angleS;
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00039698 File Offset: 0x00037898
	private void setDotStar()
	{
		for (int i = 0; i < this.yArgS.Length; i++)
		{
			bool flag = this.angleS >= 360;
			if (flag)
			{
				this.angleS -= 360;
			}
			bool flag2 = this.angleS < 0;
			if (flag2)
			{
				this.angleS = 360 + this.angleS;
			}
			this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
			this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
			bool flag3 = this.angleS < 90;
			if (flag3)
			{
				this.xDotS[i] = this.x + this.xArgS[i];
				this.yDotS[i] = this.y - this.yArgS[i];
			}
			else
			{
				bool flag4 = this.angleS >= 90 && this.angleS < 180;
				if (flag4)
				{
					this.xDotS[i] = this.x - this.xArgS[i];
					this.yDotS[i] = this.y - this.yArgS[i];
				}
				else
				{
					bool flag5 = this.angleS >= 180 && this.angleS < 270;
					if (flag5)
					{
						this.xDotS[i] = this.x - this.xArgS[i];
						this.yDotS[i] = this.y + this.yArgS[i];
					}
					else
					{
						this.xDotS[i] = this.x + this.xArgS[i];
						this.yDotS[i] = this.y + this.yArgS[i];
					}
				}
			}
			this.angleS -= this.iAngleS;
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0003988C File Offset: 0x00037A8C
	private void upd_Skill_MaFuba()
	{
		bool flag = this.stt == 0;
		if (flag)
		{
			bool flag2 = this.f == 3;
			if (flag2)
			{
				SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_1, SoundMn.volume);
			}
			this.frame++;
			bool flag3 = this.frame > this.nFrame.Length - 1;
			if (flag3)
			{
				this.frame = this.nFrame.Length - 1;
			}
			bool flag4 = this.f == this.fMove + 4;
			if (flag4)
			{
				GameScr.addEffectEnd(16, 1, this.typePaint, this.x, this.y, 3, 0, 2945, null);
			}
			bool flag5 = this.f > this.fMove + 4;
			if (flag5)
			{
				this.rS--;
				bool flag6 = this.rS < 0;
				if (flag6)
				{
					this.rS = 0;
					this.f = 0;
					this.fSpeed = 0;
					this.nFrame_2 = new byte[]
					{
						1,
						1,
						0,
						0,
						0,
						0,
						1,
						1,
						1,
						1,
						0,
						0,
						0,
						1,
						1,
						1,
						0,
						0,
						1,
						1,
						1,
						2
					};
					this.hideListObj_Mafuba(true);
					this.stt = 1;
				}
				else
				{
					this.changeAngleStar();
					this.setDotStar();
					this.updListObj_Mafuba(true);
				}
			}
		}
		else
		{
			bool flag7 = this.stt == 1;
			if (flag7)
			{
				this.fSpeed++;
				bool flag8 = this.fSpeed > this.nFrame_2.Length - 1;
				if (flag8)
				{
					this.fSpeed = this.nFrame_2.Length - 1;
					bool flag9 = GameCanvas.gameTick % 2 == 0;
					if (flag9)
					{
						this.vy1000++;
					}
					this.vy += this.vy1000;
					bool flag10 = this.vy >= this.h - this.fra_skill[0].frameHeight - this.dy + this.dy_throw;
					if (flag10)
					{
						this.vy = this.h - this.fra_skill[0].frameHeight - this.dy + this.dy_throw;
						this.f = 0;
						this.fSpeed = 0;
						this.stt = 2;
						this.nFrame_2 = new byte[]
						{
							3,
							3,
							3,
							3,
							3,
							4,
							4,
							4,
							5,
							5,
							5
						};
					}
				}
			}
			else
			{
				bool flag11 = this.stt == 2;
				if (flag11)
				{
					this.fSpeed++;
					bool flag12 = this.fSpeed > this.nFrame_2.Length - 1;
					if (flag12)
					{
						this.stt = 3;
						this.frame = 0;
						this.nFrame = new byte[]
						{
							2,
							2,
							1,
							1,
							0,
							0,
							3,
							3,
							3,
							0,
							0,
							0,
							4,
							4,
							4,
							0,
							0
						};
					}
				}
				else
				{
					bool flag13 = this.stt == 3;
					if (flag13)
					{
						this.frame++;
						bool flag14 = this.frame == 3;
						if (flag14)
						{
							SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_1, SoundMn.volume);
						}
						bool flag15 = this.frame > this.nFrame.Length - 1;
						if (flag15)
						{
							this.frame = 0;
							this.stt = 4;
							this.nFrame = new byte[]
							{
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								3,
								3,
								3,
								0,
								0,
								0,
								4,
								4,
								4,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								3,
								3,
								0,
								0,
								4,
								4
							};
						}
					}
					else
					{
						this.frame++;
						bool flag16 = this.frame > this.nFrame.Length - 1;
						if (flag16)
						{
							this.frame = 0;
						}
						bool flag17 = GameCanvas.timeNow - this.time >= (long)this.timeRemove;
						if (flag17)
						{
							GameScr.addEffectEnd(16, 0, this.typePaint, this.x1000, this.y1000, 1, 0, -1, null);
							this.updListObj_Mafuba(false);
							this.removeEff();
						}
					}
				}
			}
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00039C5C File Offset: 0x00037E5C
	private void pnt_Skill_MaFuba(mGraphics g)
	{
		bool flag = this.fra_skill == null;
		if (!flag)
		{
			bool flag2 = this.nFrame != null;
			if (flag2)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.frame], this.x1000, this.y1000, 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			bool flag3 = this.stt == 1 || this.stt == 2;
			if (flag3)
			{
				int anchor = mGraphics.BOTTOM | mGraphics.HCENTER;
				int num = this.dy;
				bool flag4 = this.nFrame_2[this.fSpeed] == 0 || this.nFrame_2[this.fSpeed] == 1;
				if (flag4)
				{
					anchor = (mGraphics.VCENTER | mGraphics.HCENTER);
					num = 0;
				}
				this.fra_skill[1].drawFrame((int)this.nFrame_2[this.fSpeed], this.x, this.y + num + this.vy, 0, anchor, g);
			}
		}
	}

	// Token: 0x0600025C RID: 604 RVA: 0x00039D60 File Offset: 0x00037F60
	private void Fill_Rect_Img(mGraphics g, FrameImage head, FrameImage body, FrameImage foot, int frame, int x, int y, int w)
	{
		int num = w;
		bool flag = false;
		bool flag2 = head != null && foot != null;
		if (flag2)
		{
			flag = true;
			num = w - (head.frameWidth + foot.frameWidth);
		}
		bool flag3 = num > 0;
		if (flag3)
		{
			int num2 = num / body.frameWidth;
			bool flag4 = num % body.frameWidth > 0;
			if (flag4)
			{
				num2++;
			}
			bool flag5 = this.dir == -1;
			if (flag5)
			{
				for (int i = 0; i < num2; i++)
				{
					bool flag6 = i == num2 - 1;
					int num3;
					if (flag6)
					{
						bool flag7 = flag;
						if (flag7)
						{
							num3 = x + foot.frameWidth;
						}
						else
						{
							num3 = x + w - body.frameWidth;
						}
					}
					else
					{
						bool flag8 = flag;
						if (flag8)
						{
							num3 = x + foot.frameWidth + body.frameWidth + i * body.frameWidth;
						}
						else
						{
							num3 = x + i * body.frameWidth;
						}
					}
					body.drawFrame(frame, num3, y - body.frameHeight / 2, 2, 0, g);
				}
			}
			else
			{
				for (int j = 0; j < num2; j++)
				{
					bool flag9 = j == num2 - 1;
					int num4;
					if (flag9)
					{
						bool flag10 = flag;
						if (flag10)
						{
							num4 = x + w - (body.frameWidth + foot.frameWidth);
						}
						else
						{
							num4 = x + w - body.frameWidth;
						}
					}
					else
					{
						bool flag11 = flag;
						if (flag11)
						{
							num4 = x + j * body.frameWidth + head.frameWidth;
						}
						else
						{
							num4 = x + j * body.frameWidth;
						}
					}
					body.drawFrame(frame, num4, y - body.frameHeight / 2, 0, 0, g);
				}
			}
		}
		bool flag12 = this.dir == -1;
		if (flag12)
		{
			bool flag13 = head != null;
			if (flag13)
			{
				head.drawFrame(frame, x + w - head.frameWidth, y - head.frameHeight / 2, 2, 0, g);
			}
			bool flag14 = foot != null;
			if (flag14)
			{
				foot.drawFrame(frame, x, y - foot.frameHeight / 2, 2, 0, g);
			}
		}
		else
		{
			bool flag15 = head != null;
			if (flag15)
			{
				head.drawFrame(frame, x, y - head.frameHeight / 2, 0, 0, g);
			}
			bool flag16 = foot != null;
			if (flag16)
			{
				foot.drawFrame(frame, x + w - foot.frameWidth - 1, y - foot.frameHeight / 2, 0, 0, g);
			}
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0003A000 File Offset: 0x00038200
	private void set_LINE_IN()
	{
		this.indexColorStar = this.typeSub;
		this.x1000 = this.x * 1000;
		this.y1000 = this.y * 1000;
		this.fRemove = Res.random(4, 6);
		this.vMax = 5;
		this.xline = 10;
		this.yline = 20;
		this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0003A07C File Offset: 0x0003827C
	private void upd_LINE_IN()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Line line = (Line)this.VecEffEnd.elementAt(i);
			line.update();
			bool flag = this.f >= this.fRemove;
			if (flag)
			{
				this.VecEffEnd.removeElement(line);
				i--;
			}
		}
		bool flag2 = this.f >= this.fRemove;
		if (flag2)
		{
			bool flag3 = GameCanvas.timeNow - this.time >= (long)this.timeRemove;
			if (flag3)
			{
				this.VecEffEnd.removeAllElements();
				this.removeEff();
			}
			else
			{
				this.fRemove = Res.random(4, 6);
				this.f = 0;
				this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
			}
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0003A168 File Offset: 0x00038368
	private void create_Star_Line_In(int vline, int minline, int maxline, int numpoint)
	{
		bool flag = this.f == -1;
		if (flag)
		{
			this.VecEffEnd.removeAllElements();
		}
		int num = 4;
		this.colorpaint = new int[num];
		bool flag2 = maxline <= minline;
		if (flag2)
		{
			maxline = minline + 1;
		}
		for (int i = 0; i < num; i++)
		{
			bool flag3 = Res.random(2) == 0;
			if (flag3)
			{
				this.colorpaint[i] = Effect_End.colorStar[this.indexColorStar][Res.random(3)];
			}
			else
			{
				this.colorpaint[i] = Effect_End.colorStar[this.indexColorStar][2];
			}
		}
		for (int j = 0; j < num; j++)
		{
			Line line = new Line();
			int num2 = 5 + 180 / num * j;
			int num3 = 180 / num + 180 / num * j - 5;
			bool flag4 = num3 <= num2;
			if (flag4)
			{
				num3 = num2 + 1;
			}
			int num4 = Res.random(minline, maxline);
			int num5 = Res.random(vline, vline + 3);
			int num6 = Res.random(num2, num3);
			int num7 = Res.random(13, 23);
			bool is2Line = Res.random(4) == 0;
			num6 = Res.fixangle(num6 % 360);
			line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, is2Line);
			bool flag5 = numpoint > 0;
			if (flag5)
			{
				line.type = Res.random(numpoint);
			}
			this.VecEffEnd.addElement(line);
			line = new Line();
			num6 += 180 + Res.random_Am(2, 5);
			num6 = Res.fixangle(num6 % 360);
			line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, is2Line);
			bool flag6 = numpoint > 0;
			if (flag6)
			{
				line.type = Res.random(numpoint);
			}
			this.VecEffEnd.addElement(line);
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0003A3FC File Offset: 0x000385FC
	private void pnt_LINE_IN(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Line line = (Line)this.VecEffEnd.elementAt(i);
			bool flag = line != null;
			if (flag)
			{
				int color = 0;
				bool flag2 = i / 2 < this.colorpaint.Length;
				if (flag2)
				{
					color = this.colorpaint[i / 2];
				}
				g.setColor(color);
				g.drawLine(line.x0 / 1000, line.y0 / 1000, line.x1 / 1000, line.y1 / 1000);
				bool is2Line = line.is2Line;
				if (is2Line)
				{
					g.drawLine(line.x0 / 1000 + 1, line.y0 / 1000, line.x1 / 1000 + 1, line.y1 / 1000);
				}
			}
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0003A4F8 File Offset: 0x000386F8
	private void set_End_Rock()
	{
		this.fraImgEff = new FrameImage(8);
		this.fRemove = Res.random(23, 27);
		int num = Res.random(1, 3);
		this.toY = this.y - 40;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = this.x + Res.random_Am(0, 20);
			point.y = this.y + Res.random_Am_0(7);
			bool flag = this.typeEffect == 10;
			if (flag)
			{
				point.frame = Res.random(0, this.fraImgEff.nFrame - 2);
			}
			else
			{
				bool flag2 = this.typeEffect == 11;
				if (flag2)
				{
					point.frame = Res.random(2, this.fraImgEff.nFrame);
				}
				else
				{
					point.frame = Res.random(0, this.fraImgEff.nFrame);
				}
			}
			point.dis = Res.random(2);
			point.vy = -Res.random(1, 4);
			this.VecEffEnd.addElement(point);
		}
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0003A618 File Offset: 0x00038818
	private void upd_End_Rock()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			point.update();
			bool flag = point.y < this.toY;
			if (flag)
			{
				this.VecEffEnd.removeElementAt(i);
				i--;
			}
		}
		bool flag2 = this.f >= this.fRemove;
		if (flag2)
		{
			this.removeEff();
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0003A6A0 File Offset: 0x000388A0
	private void pnt_End_Rock(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			bool flag = this.fraImgEff != null;
			if (flag)
			{
				this.fraImgEff.drawFrame(point.frame, point.x, point.y, 0, mGraphics.VCENTER | mGraphics.HCENTER, g);
			}
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0003A718 File Offset: 0x00038918
	private void updListObj_Mafuba(bool ismafuba)
	{
		bool flag = this.listObj == null;
		if (!flag)
		{
			for (int i = 0; i < this.listObj.Length; i++)
			{
				bool flag2 = this.listObj[i] != null;
				if (flag2)
				{
					bool flag3 = this.listObj[i].type == 0;
					if (flag3)
					{
						Mob mob = GameScr.findMobInMap(this.listObj[i].id);
						bool flag4 = mob != null;
						if (flag4)
						{
							mob.isMafuba = ismafuba;
							mob.isHide = false;
							mob.xMFB = this.xDotS[i];
							mob.yMFB = this.yDotS[i];
						}
					}
					else
					{
						bool flag5 = global::Char.myCharz().charID == this.listObj[i].id;
						global::Char @char;
						if (flag5)
						{
							@char = global::Char.myCharz();
						}
						else
						{
							@char = GameScr.findCharInMap(this.listObj[i].id);
						}
						bool flag6 = @char != null;
						if (flag6)
						{
							@char.isMafuba = ismafuba;
							@char.isHide = false;
							@char.xMFB = this.xDotS[i];
							@char.yMFB = this.yDotS[i];
						}
					}
				}
			}
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0003A854 File Offset: 0x00038A54
	private void hideListObj_Mafuba(bool ishide)
	{
		bool flag = this.listObj == null;
		if (!flag)
		{
			for (int i = 0; i < this.listObj.Length; i++)
			{
				bool flag2 = this.listObj[i] != null;
				if (flag2)
				{
					bool flag3 = this.listObj[i].type == 0;
					if (flag3)
					{
						Mob mob = GameScr.findMobInMap(this.listObj[i].id);
						bool flag4 = mob != null;
						if (flag4)
						{
							mob.isHide = ishide;
						}
					}
					else
					{
						bool flag5 = global::Char.myCharz().charID == this.listObj[i].id;
						global::Char @char;
						if (flag5)
						{
							@char = global::Char.myCharz();
						}
						else
						{
							@char = GameScr.findCharInMap(this.listObj[i].id);
						}
						bool flag6 = @char != null;
						if (flag6)
						{
							@char.isHide = ishide;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0003A944 File Offset: 0x00038B44
	private void get_Img_Skill()
	{
		int num = 0;
		int[] array = null;
		int[] array2 = null;
		switch (this.typeEffect)
		{
		case 16:
		{
			num = 26;
			bool flag = this.typeSub == 0;
			if (flag)
			{
				array = new int[]
				{
					7
				};
				array2 = new int[]
				{
					28
				};
			}
			bool flag2 = this.typeSub == 1;
			if (flag2)
			{
				array = new int[]
				{
					2
				};
				array2 = new int[]
				{
					23
				};
			}
			break;
		}
		case 17:
			num = 25;
			array = new int[]
			{
				2
			};
			array2 = new int[]
			{
				16
			};
			break;
		case 18:
			num = 24;
			array = new int[1];
			array2 = new int[]
			{
				9
			};
			break;
		case 19:
			num = 25;
			array = new int[1];
			array2 = new int[]
			{
				14
			};
			break;
		case 20:
			num = 26;
			array = new int[1];
			array2 = new int[]
			{
				21
			};
			break;
		case 21:
			num = 24;
			array = new int[]
			{
				1
			};
			array2 = new int[]
			{
				10
			};
			break;
		case 22:
			num = 25;
			array = new int[]
			{
				1
			};
			array2 = new int[]
			{
				15
			};
			break;
		case 23:
			num = 26;
			array = new int[]
			{
				1
			};
			array2 = new int[]
			{
				22
			};
			break;
		case 24:
			num = 24;
			array = new int[]
			{
				2,
				3,
				4
			};
			array2 = new int[]
			{
				11,
				12,
				13
			};
			break;
		case 25:
			num = 25;
			array = new int[]
			{
				3,
				4,
				5,
				6
			};
			array2 = new int[]
			{
				17,
				18,
				19,
				20
			};
			break;
		case 26:
		{
			num = 26;
			int num2 = 0;
			int num3 = 0;
			bool flag3 = this.typeSub == 0;
			if (flag3)
			{
				num2 = 4;
				num3 = 25;
			}
			else
			{
				bool flag4 = this.typeSub == 1;
				if (flag4)
				{
					num2 = 5;
					num3 = 26;
				}
				else
				{
					bool flag5 = this.typeSub == 2;
					if (flag5)
					{
						num2 = 6;
						num3 = 27;
					}
				}
			}
			array = new int[]
			{
				num2,
				3
			};
			array2 = new int[]
			{
				num3,
				24
			};
			break;
		}
		}
		bool flag6 = array != null && array2 != null;
		if (flag6)
		{
			this.fra_skill = new FrameImage[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string nameImg = string.Concat(new object[]
				{
					"Skills_",
					num,
					"_",
					this.typePaint,
					"_",
					array[i]
				});
				FrameImage frameImage = mSystem.getFraImage(nameImg);
				bool flag7 = frameImage == null;
				if (flag7)
				{
					frameImage = new FrameImage(array2[i]);
				}
				bool flag8 = frameImage != null;
				if (flag8)
				{
					this.fra_skill[i] = frameImage;
				}
			}
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0003AC54 File Offset: 0x00038E54
	private void set_Gong()
	{
		bool flag = this.charUse != null;
		if (flag)
		{
			bool flag2 = this.typeEffect == 21;
			if (flag2)
			{
				this.x = this.charUse.cx - 3 * this.charUse.cdir;
				this.y = this.charUse.cy;
				SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_0, SoundMn.volume);
			}
			else
			{
				bool flag3 = this.typeEffect == 22;
				if (flag3)
				{
					this.x = this.charUse.cx + 20 * this.charUse.cdir;
					this.y = this.charUse.cy - 4;
					SoundMn.playSound(this.x, this.y, SoundMn.DESTROY_2, SoundMn.volume);
				}
				else
				{
					bool flag4 = this.typeEffect == 23;
					if (flag4)
					{
						this.x = this.charUse.cx;
						this.y = this.charUse.cy - 50;
						SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_2, SoundMn.volume);
					}
					else
					{
						this.x = this.charUse.cx;
						this.y = this.charUse.cy;
					}
				}
			}
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0003ADB0 File Offset: 0x00038FB0
	private void upd_Gong()
	{
		bool flag = this.charUse != null;
		if (flag)
		{
			bool flag2 = this.typeEffect == 21;
			if (flag2)
			{
				this.x = this.charUse.cx - 3 * this.charUse.cdir;
				this.y = this.charUse.cy;
			}
			else
			{
				bool flag3 = this.typeEffect == 22;
				if (flag3)
				{
					this.x = this.charUse.cx + 20 * this.charUse.cdir;
					this.y = this.charUse.cy - 4;
				}
				else
				{
					bool flag4 = this.typeEffect == 23;
					if (flag4)
					{
						this.x = this.charUse.cx;
						this.y = this.charUse.cy - 50;
					}
					else
					{
						this.x = this.charUse.cx;
						this.y = this.charUse.cy;
					}
				}
			}
		}
		bool flag5 = this.timeRemove > 0;
		if (flag5)
		{
			bool flag6 = GameCanvas.timeNow - this.time >= (long)this.timeRemove;
			if (flag6)
			{
				this.removeEff();
			}
		}
		else
		{
			bool flag7 = this.f >= this.fra_skill[0].nFrame * this.n_frame;
			if (flag7)
			{
				this.removeEff();
			}
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0003AF20 File Offset: 0x00039120
	private void pnt_Gong(mGraphics g, int anchor)
	{
		bool flag = this.fra_skill[0] != null;
		if (flag)
		{
			this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0003AF7C File Offset: 0x0003917C
	private void set_Pow()
	{
		this.nFrame = null;
		this.n_frame = 3;
		bool flag = this.typeEffect == 18;
		if (flag)
		{
			bool flag2 = this.typeSub == 0;
			if (flag2)
			{
				this.nFrame = new byte[]
				{
					0,
					0,
					0,
					1,
					1,
					1,
					2,
					2,
					2
				};
			}
			else
			{
				this.nFrame = new byte[]
				{
					3,
					3,
					3,
					4,
					4,
					4,
					5,
					5,
					5,
					6,
					6,
					6
				};
			}
		}
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0003AFEC File Offset: 0x000391EC
	private void upd_Pow()
	{
		bool flag = this.charUse != null;
		if (flag)
		{
			this.x = this.charUse.cx;
			this.y = this.charUse.cy + 13;
		}
		bool flag2 = this.timeRemove > 0;
		if (flag2)
		{
			bool flag3 = GameCanvas.timeNow - this.time >= (long)this.timeRemove;
			if (flag3)
			{
				this.removeEff();
			}
		}
		else
		{
			bool flag4 = this.nFrame != null;
			if (flag4)
			{
				bool flag5 = this.f > this.nFrame.Length;
				if (flag5)
				{
					this.removeEff();
				}
			}
			else
			{
				bool flag6 = this.f >= this.fra_skill[0].nFrame * this.n_frame;
				if (flag6)
				{
					this.removeEff();
				}
			}
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0003B0C4 File Offset: 0x000392C4
	private void pnt_Pow(mGraphics g, int anchor)
	{
		bool flag = this.fra_skill[0] != null;
		if (flag)
		{
			bool flag2 = this.nFrame != null;
			if (flag2)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.f % this.nFrame.Length], this.x, this.y, this.dir_nguoc, anchor, g);
			}
			else
			{
				this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
			}
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0003B170 File Offset: 0x00039370
	private void set_Sub()
	{
		bool flag = this.typeEffect == 17;
		if (flag)
		{
			this.x += ((this.dir != 0) ? (-this.fra_skill[0].frameWidth) : 0);
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0003B1B4 File Offset: 0x000393B4
	private void upd_Sub()
	{
		bool flag = this.timeRemove > 0;
		if (flag)
		{
			bool flag2 = GameCanvas.timeNow - this.time >= (long)this.timeRemove;
			if (flag2)
			{
				this.removeEff();
			}
		}
		else
		{
			bool flag3 = this.f >= this.fra_skill[0].nFrame * this.n_frame;
			if (flag3)
			{
				this.removeEff();
			}
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0003B224 File Offset: 0x00039424
	private void pnt_Sub(mGraphics g, int anchor)
	{
		this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir, anchor, g);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00003136 File Offset: 0x00001336
	private void set_()
	{
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00003136 File Offset: 0x00001336
	private void upd_()
	{
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00003136 File Offset: 0x00001336
	private void pnt_(mGraphics g)
	{
	}

	// Token: 0x04000532 RID: 1330
	public const sbyte Lvlpaint_All = -1;

	// Token: 0x04000533 RID: 1331
	public const sbyte Lvlpaint_Front = 0;

	// Token: 0x04000534 RID: 1332
	public const sbyte Lvlpaint_Mid = 1;

	// Token: 0x04000535 RID: 1333
	public const sbyte Lvlpaint_Mid_2 = 2;

	// Token: 0x04000536 RID: 1334
	public const sbyte Lvlpaint_Behind = 3;

	// Token: 0x04000537 RID: 1335
	public const short End_String_Lose = 0;

	// Token: 0x04000538 RID: 1336
	public const short End_String_Win = 1;

	// Token: 0x04000539 RID: 1337
	public const short End_String_Draw = 2;

	// Token: 0x0400053A RID: 1338
	public const short End_FireWork = 3;

	// Token: 0x0400053B RID: 1339
	public const short End_line_in = 9;

	// Token: 0x0400053C RID: 1340
	public const short End_e8_rock = 10;

	// Token: 0x0400053D RID: 1341
	public const short End_e8_ice = 11;

	// Token: 0x0400053E RID: 1342
	public const short End_SUB_MaFuBa = 16;

	// Token: 0x0400053F RID: 1343
	public const short End_SUB_Destroy = 17;

	// Token: 0x04000540 RID: 1344
	public const short End_POW_Kamex10 = 18;

	// Token: 0x04000541 RID: 1345
	public const short End_POW_Destroy = 19;

	// Token: 0x04000542 RID: 1346
	public const short End_POW_MaFuBa = 20;

	// Token: 0x04000543 RID: 1347
	public const short End_GONG_Kamex10 = 21;

	// Token: 0x04000544 RID: 1348
	public const short End_GONG_Destroy = 22;

	// Token: 0x04000545 RID: 1349
	public const short End_GONG_MaFuBa = 23;

	// Token: 0x04000546 RID: 1350
	public const short End_Skill_Kamex10 = 24;

	// Token: 0x04000547 RID: 1351
	public const short End_Skill_Destroy = 25;

	// Token: 0x04000548 RID: 1352
	public const short End_Skill_MaFuBa = 26;

	// Token: 0x04000549 RID: 1353
	private MyVector VecEffEnd = new MyVector("EffectEnd VecEffEnd");

	// Token: 0x0400054A RID: 1354
	public FrameImage fraImgEff;

	// Token: 0x0400054B RID: 1355
	public byte[] nFrame = new byte[10];

	// Token: 0x0400054C RID: 1356
	public byte[] nFrame_2 = new byte[10];

	// Token: 0x0400054D RID: 1357
	public int typePaint;

	// Token: 0x0400054E RID: 1358
	public int typeEffect;

	// Token: 0x0400054F RID: 1359
	public int typeSub;

	// Token: 0x04000550 RID: 1360
	public int range;

	// Token: 0x04000551 RID: 1361
	public short idEndeff;

	// Token: 0x04000552 RID: 1362
	public int fRemove;

	// Token: 0x04000553 RID: 1363
	public int fMove;

	// Token: 0x04000554 RID: 1364
	public int n_frame;

	// Token: 0x04000555 RID: 1365
	public int x;

	// Token: 0x04000556 RID: 1366
	public int y;

	// Token: 0x04000557 RID: 1367
	public int w;

	// Token: 0x04000558 RID: 1368
	public int h;

	// Token: 0x04000559 RID: 1369
	public int dir;

	// Token: 0x0400055A RID: 1370
	public int dir_nguoc;

	// Token: 0x0400055B RID: 1371
	public int levelPaint;

	// Token: 0x0400055C RID: 1372
	public int f;

	// Token: 0x0400055D RID: 1373
	public int frame;

	// Token: 0x0400055E RID: 1374
	public int fSpeed;

	// Token: 0x0400055F RID: 1375
	public int vx;

	// Token: 0x04000560 RID: 1376
	public int vy;

	// Token: 0x04000561 RID: 1377
	public int x1000;

	// Token: 0x04000562 RID: 1378
	public int y1000;

	// Token: 0x04000563 RID: 1379
	public int vx1000;

	// Token: 0x04000564 RID: 1380
	public int vy1000;

	// Token: 0x04000565 RID: 1381
	public int dy_throw;

	// Token: 0x04000566 RID: 1382
	public int vMax;

	// Token: 0x04000567 RID: 1383
	public int toX;

	// Token: 0x04000568 RID: 1384
	public int toY;

	// Token: 0x04000569 RID: 1385
	public int stt;

	// Token: 0x0400056A RID: 1386
	public int dx;

	// Token: 0x0400056B RID: 1387
	public int dy;

	// Token: 0x0400056C RID: 1388
	public short timeRemove;

	// Token: 0x0400056D RID: 1389
	public long time;

	// Token: 0x0400056E RID: 1390
	public bool isRemove;

	// Token: 0x0400056F RID: 1391
	public bool isAddSub;

	// Token: 0x04000570 RID: 1392
	public global::Char charUse;

	// Token: 0x04000571 RID: 1393
	public Point[] listObj;

	// Token: 0x04000572 RID: 1394
	public Point target;

	// Token: 0x04000573 RID: 1395
	public static short[][] arrInfoEff = new short[][]
	{
		new short[]
		{
			68,
			264,
			4
		},
		new short[]
		{
			30,
			120,
			4
		},
		new short[]
		{
			66,
			280,
			4
		},
		new short[]
		{
			0,
			0,
			1
		},
		new short[]
		{
			111,
			68,
			2
		},
		new short[]
		{
			90,
			68,
			2
		},
		new short[]
		{
			125,
			68,
			2
		},
		new short[]
		{
			47,
			282,
			6
		},
		new short[]
		{
			10,
			40,
			4
		},
		new short[]
		{
			92,
			525,
			7
		},
		new short[]
		{
			62,
			372,
			6
		},
		new short[]
		{
			80,
			352,
			4
		},
		new short[]
		{
			80,
			352,
			4
		},
		new short[]
		{
			80,
			352,
			4
		},
		new short[]
		{
			72,
			240,
			3
		},
		new short[]
		{
			20,
			42,
			3
		},
		new short[]
		{
			65,
			160,
			4
		},
		new short[]
		{
			50,
			300,
			6
		},
		new short[]
		{
			84,
			168,
			2
		},
		new short[]
		{
			90,
			540,
			6
		},
		new short[]
		{
			180,
			900,
			6
		},
		new short[]
		{
			62,
			186,
			3
		},
		new short[]
		{
			34,
			80,
			4
		},
		new short[]
		{
			140,
			560,
			4
		},
		new short[]
		{
			64,
			600,
			6
		},
		new short[]
		{
			36,
			200,
			5
		},
		new short[]
		{
			35,
			200,
			5
		},
		new short[]
		{
			50,
			250,
			5
		},
		new short[]
		{
			50,
			240,
			6
		}
	};

	// Token: 0x04000574 RID: 1396
	public int life;

	// Token: 0x04000575 RID: 1397
	public int goc_Arc;

	// Token: 0x04000576 RID: 1398
	public int va;

	// Token: 0x04000577 RID: 1399
	public int gocT_Arc;

	// Token: 0x04000578 RID: 1400
	public byte[] mpaintone_Arrow = new byte[]
	{
		12,
		11,
		10,
		9,
		8,
		7,
		6,
		5,
		4,
		3,
		2,
		1,
		0,
		23,
		22,
		21,
		20,
		19,
		18,
		17,
		16,
		15,
		14,
		13
	};

	// Token: 0x04000579 RID: 1401
	public byte[] mImageArrow = new byte[]
	{
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2
	};

	// Token: 0x0400057A RID: 1402
	public byte[] mXoayArrow = new byte[]
	{
		2,
		2,
		3,
		3,
		3,
		4,
		5,
		5,
		5,
		5,
		5,
		1,
		0,
		0,
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		6,
		6,
		2
	};

	// Token: 0x0400057B RID: 1403
	private int rS;

	// Token: 0x0400057C RID: 1404
	private int angleS;

	// Token: 0x0400057D RID: 1405
	private int angleO;

	// Token: 0x0400057E RID: 1406
	private int iAngleS;

	// Token: 0x0400057F RID: 1407
	private int iDotS;

	// Token: 0x04000580 RID: 1408
	private int[] xArgS;

	// Token: 0x04000581 RID: 1409
	private int[] yArgS;

	// Token: 0x04000582 RID: 1410
	private int[] xDotS;

	// Token: 0x04000583 RID: 1411
	private int[] yDotS;

	// Token: 0x04000584 RID: 1412
	public static int[][] colorStar = new int[][]
	{
		new int[]
		{
			16310304,
			16298056,
			16777215
		},
		new int[]
		{
			7045120,
			12643960,
			16777215
		},
		new int[]
		{
			2407423,
			11987199,
			16777215
		}
	};

	// Token: 0x04000585 RID: 1413
	private int[] colorpaint;

	// Token: 0x04000586 RID: 1414
	private int indexColorStar;

	// Token: 0x04000587 RID: 1415
	private int xline;

	// Token: 0x04000588 RID: 1416
	private int yline;

	// Token: 0x04000589 RID: 1417
	private FrameImage[] fra_skill;
}
