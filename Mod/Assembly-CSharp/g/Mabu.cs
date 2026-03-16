using System;

namespace Assets.src.g
{
	// Token: 0x020000B5 RID: 181
	internal class Mabu : global::Char
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x00073D00 File Offset: 0x00072100
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00073D1C File Offset: 0x0007211C
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			if (id == global::Char.myCharz().charID)
			{
				this.focus = global::Char.myCharz();
			}
			else
			{
				this.focus = GameScr.findCharInMap(id);
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00073D80 File Offset: 0x00072180
		public new void checkFrameTick(int[] array)
		{
			if ((int)this.skillID == 0)
			{
				if (this.tick == 11)
				{
					this.addFoot = true;
					Effect me = new Effect(19, this.cx, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(me);
				}
				if (this.tick >= array.Length - 1)
				{
					this.skillID = 2;
					return;
				}
			}
			if ((int)this.skillID == 1 && this.tick == array.Length - 1)
			{
				this.skillID = 3;
				this.cy -= 15;
				return;
			}
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00073E4C File Offset: 0x0007224C
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				102,
				"/data"
			});
			try
			{
				Mabu.data1.readData2(patch);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102 + "/img.png");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00073EF0 File Offset: 0x000722F0
		public void setSkill(sbyte id, short x, short y, global::Char[] charHit, int[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo <= this.cx) ? -1 : 1);
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00073F4C File Offset: 0x0007234C
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				103,
				"/data"
			});
			try
			{
				Mabu.data2.readData2(patch);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00073FFC File Offset: 0x000723FC
		public override void update()
		{
			if (this.focus != null)
			{
				if (this.effEat.t >= 30)
				{
					this.effEat.x += (this.cx - this.effEat.x) / 4;
					this.effEat.y += (this.cy - this.effEat.y) / 4;
					this.focus.cx = this.effEat.x;
					this.focus.cy = this.effEat.y;
					this.focus.isMabuHold = true;
				}
				else
				{
					this.effEat.trans = ((this.effEat.x <= this.focus.cx) ? 0 : 1);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			if ((int)this.skillID != -1)
			{
				if ((int)this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					if ((this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo))
					{
						this.addFoot = false;
						this.skillID = -1;
						this.dx = 0;
						this.tick = 0;
						this.cdir = this.lastDir;
						for (int i = 0; i < this.charAttack.Length; i++)
						{
							this.charAttack[i].doInjure(this.damageAttack[i], 0, false, false);
						}
					}
				}
				if ((int)this.skillID == 3)
				{
					this.xTo = this.charAttack[this.pIndex].cx;
					this.yTo = this.charAttack[this.pIndex].cy;
					this.cx += (this.xTo - this.cx) / 3;
					this.cy += (this.yTo - this.cy) / 3;
					if (GameCanvas.gameTick % 5 == 0)
					{
						Effect me = new Effect(19, this.cx, this.cy, 2, 1, -1);
						EffecMn.addEff(me);
					}
					if (Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20)
					{
						this.cx = this.xTo;
						this.cy = this.yTo;
						this.charAttack[this.pIndex].doInjure(this.damageAttack[this.pIndex], 0, false, false);
						this.pIndex++;
						if (this.pIndex == this.charAttack.Length)
						{
							this.skillID = -1;
							this.pIndex = 0;
						}
					}
				}
				return;
			}
			base.update();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000743B0 File Offset: 0x000727B0
		public override void paint(mGraphics g)
		{
			if ((int)this.skillID != -1)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				if ((int)this.skillID == 0 || (int)this.skillID == 1)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
			}
			else
			{
				base.paint(g);
			}
		}

		// Token: 0x04000F45 RID: 3909
		public static EffectData data1;

		// Token: 0x04000F46 RID: 3910
		public static EffectData data2;

		// Token: 0x04000F47 RID: 3911
		private new int tick;

		// Token: 0x04000F48 RID: 3912
		private int lastDir;

		// Token: 0x04000F49 RID: 3913
		private bool addFoot;

		// Token: 0x04000F4A RID: 3914
		private Effect effEat;

		// Token: 0x04000F4B RID: 3915
		private new global::Char focus;

		// Token: 0x04000F4C RID: 3916
		public int xTo;

		// Token: 0x04000F4D RID: 3917
		public int yTo;

		// Token: 0x04000F4E RID: 3918
		public bool haftBody;

		// Token: 0x04000F4F RID: 3919
		public bool change;

		// Token: 0x04000F50 RID: 3920
		private global::Char[] charAttack;

		// Token: 0x04000F51 RID: 3921
		private int[] damageAttack;

		// Token: 0x04000F52 RID: 3922
		private int dx;

		// Token: 0x04000F53 RID: 3923
		public static int[] skill1 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x04000F54 RID: 3924
		public static int[] skill2 = new int[]
		{
			0,
			0,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10
		};

		// Token: 0x04000F55 RID: 3925
		public static int[] skill3 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12
		};

		// Token: 0x04000F56 RID: 3926
		public static int[] skill4 = new int[]
		{
			13,
			13,
			14,
			14,
			15,
			15,
			16,
			16
		};

		// Token: 0x04000F57 RID: 3927
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x04000F58 RID: 3928
		public sbyte skillID = -1;

		// Token: 0x04000F59 RID: 3929
		private int frame;

		// Token: 0x04000F5A RID: 3930
		private int pIndex;
	}
}
