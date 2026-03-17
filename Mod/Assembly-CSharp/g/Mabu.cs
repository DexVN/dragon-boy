using System;

namespace Assets.src.g
{
	// Token: 0x020000C1 RID: 193
	internal class Mabu : global::Char
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x000A98F2 File Offset: 0x000A7AF2
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000A9914 File Offset: 0x000A7B14
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			bool flag = id == global::Char.myCharz().charID;
			if (flag)
			{
				this.focus = global::Char.myCharz();
			}
			else
			{
				this.focus = GameScr.findCharInMap(id);
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000A997C File Offset: 0x000A7B7C
		public new void checkFrameTick(int[] array)
		{
			bool flag = this.skillID == 0;
			if (flag)
			{
				bool flag2 = this.tick == 11;
				if (flag2)
				{
					this.addFoot = true;
					Effect me = new Effect(19, this.cx, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(me);
				}
				bool flag3 = this.tick >= array.Length - 1;
				if (flag3)
				{
					this.skillID = 2;
					return;
				}
			}
			bool flag4 = this.skillID == 1 && this.tick == array.Length - 1;
			if (flag4)
			{
				this.skillID = 3;
				this.cy -= 15;
			}
			else
			{
				this.tick++;
				bool flag5 = this.tick > array.Length - 1;
				if (flag5)
				{
					this.tick = 0;
				}
				this.frame = array[this.tick];
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000A9A60 File Offset: 0x000A7C60
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
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102.ToString() + "/img.png");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000A9B08 File Offset: 0x000A7D08
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

		// Token: 0x06000A63 RID: 2659 RVA: 0x000A9B60 File Offset: 0x000A7D60
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
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103.ToString() + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000A9C14 File Offset: 0x000A7E14
		public override void update()
		{
			bool flag = this.focus != null;
			if (flag)
			{
				bool flag2 = this.effEat.t >= 30;
				if (flag2)
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
			bool flag3 = this.skillID != -1;
			if (flag3)
			{
				bool flag4 = this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0;
				if (flag4)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					bool flag5 = (this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo);
					if (flag5)
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
				bool flag6 = this.skillID == 3;
				if (flag6)
				{
					this.xTo = this.charAttack[this.pIndex].cx;
					this.yTo = this.charAttack[this.pIndex].cy;
					this.cx += (this.xTo - this.cx) / 3;
					this.cy += (this.yTo - this.cy) / 3;
					bool flag7 = GameCanvas.gameTick % 5 == 0;
					if (flag7)
					{
						Effect me = new Effect(19, this.cx, this.cy, 2, 1, -1);
						EffecMn.addEff(me);
					}
					bool flag8 = Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20;
					if (flag8)
					{
						this.cx = this.xTo;
						this.cy = this.yTo;
						this.charAttack[this.pIndex].doInjure(this.damageAttack[this.pIndex], 0, false, false);
						this.pIndex++;
						bool flag9 = this.pIndex == this.charAttack.Length;
						if (flag9)
						{
							this.skillID = -1;
							this.pIndex = 0;
						}
					}
				}
			}
			else
			{
				base.update();
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000A9FFC File Offset: 0x000A81FC
		public override void paint(mGraphics g)
		{
			bool flag = this.skillID != -1;
			if (flag)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				bool flag2 = this.skillID == 0 || this.skillID == 1;
				if (flag2)
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

		// Token: 0x0400138C RID: 5004
		public static EffectData data1;

		// Token: 0x0400138D RID: 5005
		public static EffectData data2;

		// Token: 0x0400138E RID: 5006
		private new int tick;

		// Token: 0x0400138F RID: 5007
		private int lastDir;

		// Token: 0x04001390 RID: 5008
		private bool addFoot;

		// Token: 0x04001391 RID: 5009
		private Effect effEat;

		// Token: 0x04001392 RID: 5010
		private new global::Char focus;

		// Token: 0x04001393 RID: 5011
		public int xTo;

		// Token: 0x04001394 RID: 5012
		public int yTo;

		// Token: 0x04001395 RID: 5013
		public bool haftBody;

		// Token: 0x04001396 RID: 5014
		public bool change;

		// Token: 0x04001397 RID: 5015
		private global::Char[] charAttack;

		// Token: 0x04001398 RID: 5016
		private int[] damageAttack;

		// Token: 0x04001399 RID: 5017
		private int dx;

		// Token: 0x0400139A RID: 5018
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

		// Token: 0x0400139B RID: 5019
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

		// Token: 0x0400139C RID: 5020
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

		// Token: 0x0400139D RID: 5021
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

		// Token: 0x0400139E RID: 5022
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x0400139F RID: 5023
		public sbyte skillID = -1;

		// Token: 0x040013A0 RID: 5024
		private int frame;

		// Token: 0x040013A1 RID: 5025
		private int pIndex;
	}
}
