using System;

// Token: 0x02000071 RID: 113
public class Npc : global::Char
{
	// Token: 0x060003D0 RID: 976 RVA: 0x0002F574 File Offset: 0x0002D974
	public Npc(int npcId, int status, int cx, int cy, int templateId, int avatar)
	{
		this.isShadown = true;
		this.npcId = npcId;
		this.avatar = avatar;
		this.cx = cx;
		this.cy = cy;
		this.xSd = cx;
		this.ySd = cy;
		this.statusMe = status;
		if (npcId != -1)
		{
			this.template = Npc.arrNpcTemplate[templateId];
		}
		if (templateId == 23 || templateId == 42)
		{
			this.ch = 45;
		}
		if (templateId == 51)
		{
			this.isShadown = false;
			this.duaHauIndex = status;
		}
		if (this.template != null)
		{
			if (this.template.name == null)
			{
				this.template.name = string.Empty;
			}
			this.template.name = Res.changeString(this.template.name);
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0002F658 File Offset: 0x0002DA58
	public void setStatus(sbyte s, int sc)
	{
		this.duaHauIndex = (int)s;
		this.last = (this.cur = mSystem.currentTimeMillis());
		this.seconds = sc;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0002F688 File Offset: 0x0002DA88
	public static void clearEffTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			npc.effTask = null;
			npc.indexEffTask = -1;
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0002F6D0 File Offset: 0x0002DAD0
	public override void update()
	{
		if (this.template.npcTemplateId == 51)
		{
			this.cur = mSystem.currentTimeMillis();
			if (this.cur - this.last >= 1000L)
			{
				this.seconds--;
				this.last = this.cur;
				if (this.seconds < 0)
				{
					this.seconds = 0;
				}
			}
		}
		if (this.isShadown)
		{
			base.updateShadown();
		}
		if (this.effTask == null)
		{
			sbyte[] array = new sbyte[]
			{
				-1,
				9,
				9,
				10,
				10,
				11,
				11
			};
			if (global::Char.myCharz().ctaskId >= 9 && global::Char.myCharz().ctaskId <= 10 && global::Char.myCharz().nClass.classId > 0 && (int)array[global::Char.myCharz().nClass.classId] == this.template.npcTemplateId)
			{
				if (global::Char.myCharz().taskMaint == null)
				{
					this.effTask = GameScr.efs[57];
					this.indexEffTask = 0;
				}
				else if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length)
				{
					this.effTask = GameScr.efs[62];
					this.indexEffTask = 0;
				}
			}
			else
			{
				sbyte taskNpcId = GameScr.getTaskNpcId();
				if (global::Char.myCharz().taskMaint == null && (int)taskNpcId == this.template.npcTemplateId)
				{
					this.indexEffTask = 0;
				}
				else if (global::Char.myCharz().taskMaint != null && (int)taskNpcId == this.template.npcTemplateId)
				{
					if (global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length)
					{
						this.effTask = GameScr.efs[98];
					}
					else
					{
						this.effTask = GameScr.efs[98];
					}
					this.indexEffTask = 0;
				}
			}
		}
		base.update();
		if (TileMap.mapID == 51)
		{
			if (this.cx > global::Char.myCharz().cx)
			{
				this.cdir = -1;
			}
			else
			{
				this.cdir = 1;
			}
			if (this.template.npcTemplateId % 2 == 0)
			{
				if (this.cf == 1)
				{
					this.cf = 0;
				}
				else
				{
					this.cf = 1;
				}
			}
		}
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0002F950 File Offset: 0x0002DD50
	public void paintHead(mGraphics g, int xStart, int yStart)
	{
		Part part = GameScr.parts[this.template.headId];
		if (this.cdir == 1)
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 0, 0);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 2, 24);
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0002F9F8 File Offset: 0x0002DDF8
	public override void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (this.isHide)
		{
			return;
		}
		if (!base.isPaint())
		{
			return;
		}
		if (this.statusMe == 15)
		{
			return;
		}
		if ((int)this.cTypePk != 0)
		{
			base.paint(g);
			return;
		}
		if (this.template == null)
		{
			return;
		}
		if (this.template.npcTemplateId != 4 && this.template.npcTemplateId != 51 && this.template.npcTemplateId != 50)
		{
			g.drawImage(TileMap.bong, this.cx, this.cy, 3);
		}
		if (this.template.npcTemplateId == 3)
		{
			SmallImage.drawSmallImage(g, 265, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
			{
				if (ChatPopup.currChatPopup == null)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch + 4, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			this.dyEff = 60;
		}
		else if (this.template.npcTemplateId != 4)
		{
			if (this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51)
			{
				if (this.duahau != null)
				{
					if (this.template.npcTemplateId == 50 && Npc.mabuEff)
					{
						Npc.tMabuEff++;
						if (GameCanvas.gameTick % 3 == 0)
						{
							Effect me = new Effect(19, this.cx + Res.random(-50, 50), this.cy, 2, 1, -1);
							EffecMn.addEff(me);
						}
						if (GameCanvas.gameTick % 15 == 0)
						{
							Effect me2 = new Effect(18, this.cx + Res.random(-5, 5), this.cy + Res.random(-90, 0), 2, 1, -1);
							EffecMn.addEff(me2);
						}
						if (Npc.tMabuEff == 100)
						{
							GameScr.gI().activeSuperPower(this.cx, this.cy);
						}
						if (Npc.tMabuEff == 110)
						{
							Npc.mabuEff = false;
							this.template.npcTemplateId = 4;
						}
					}
					int num = 0;
					if (SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null)
					{
						num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
					}
					SmallImage.drawSmallImage(g, this.duahau[this.duaHauIndex], this.cx + Res.random(-1, 1), this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						if (ChatPopup.currChatPopup == null)
						{
							g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9 + 16 - num, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 16 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
					}
					else
					{
						mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
					}
				}
			}
			else if (this.template.npcTemplateId == 6)
			{
				SmallImage.drawSmallImage(g, 545, this.cx, this.cy + 5, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
				{
					if (ChatPopup.currChatPopup == null)
					{
						g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
				mFont.tahoma_7b_white.drawString(g, TileMap.zoneID + string.Empty, this.cx, this.cy - this.ch + 19 - mFont.tahoma_7.getHeight(), mFont.CENTER);
			}
			else
			{
				int headId = this.template.headId;
				int legId = this.template.legId;
				int bodyId = this.template.bodyId;
				Part part = GameScr.parts[headId];
				Part part2 = GameScr.parts[legId];
				Part part3 = GameScr.parts[bodyId];
				if (this.cdir == 1)
				{
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 0, 0);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx - global::Char.CharInfo[this.cf][0][1] - (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 2, 24);
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx - global::Char.CharInfo[this.cf][1][1] - (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 2, 24);
					SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx - global::Char.CharInfo[this.cf][2][1] - (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 2, 24);
				}
				if (TileMap.mapID != 51)
				{
					int num2 = 15;
					if (this.template.npcTemplateId == 47)
					{
						num2 = 47;
					}
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						if (ChatPopup.currChatPopup == null)
						{
							int num3 = 0;
							int num4 = 0;
							if (global::Char.myCharz().npcFocus.template.npcTemplateId == 28 || global::Char.myCharz().npcFocus.template.npcTemplateId == 41)
							{
								num3 = 3;
								num4 = -12;
							}
							g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx + num3, this.cy - this.ch - (num2 - 8) + num4, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
					}
					else if (this.template.npcTemplateId == 47)
					{
					}
				}
				this.dyEff = 65;
			}
		}
		if (this.indexEffTask >= 0 && this.effTask != null && (int)this.cTypePk == 0)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy - this.dyEff, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00030450 File Offset: 0x0002E850
	public new void paintName(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (this.isHide)
		{
			return;
		}
		if (!base.isPaint())
		{
			return;
		}
		if (this.statusMe == 15)
		{
			return;
		}
		if (this.template == null)
		{
			return;
		}
		if (this.template.npcTemplateId == 3)
		{
			if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 3 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
			}
			this.dyEff = 60;
		}
		else if (this.template.npcTemplateId != 4)
		{
			if (this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51)
			{
				if (this.duahau != null)
				{
					int num = 0;
					if (SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null)
					{
						num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
					}
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num, mFont.CENTER, mFont.tahoma_7_grey);
					}
					else
					{
						mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - num + 16, mFont.CENTER, mFont.tahoma_7_grey);
					}
				}
			}
			else if (this.template.npcTemplateId == 6)
			{
				if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
				{
					mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 16, mFont.CENTER, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			else
			{
				if (TileMap.mapID != 51)
				{
					int num2 = 15;
					if (this.template.npcTemplateId == 47)
					{
						num2 = 47;
					}
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						if (TileMap.mapID != 113)
						{
							int num3 = 0;
							int num4 = 0;
							if (global::Char.myCharz().npcFocus.template.npcTemplateId == 28 || global::Char.myCharz().npcFocus.template.npcTemplateId == 41)
							{
								num3 = 3;
								num4 = -12;
							}
							mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx + num3, this.cy - this.ch - mFont.tahoma_7.getHeight() - num2 + num4, mFont.CENTER, mFont.tahoma_7_grey);
						}
					}
					else
					{
						num2 = 8;
						if (this.template.npcTemplateId == 47)
						{
							num2 = 40;
						}
						if (TileMap.mapID != 113)
						{
							int num5 = 0;
							int num6 = 0;
							if (this.template.npcTemplateId == 28 || this.template.npcTemplateId == 41)
							{
								num5 = 3;
								num6 = -12;
							}
							mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx + num5, this.cy - this.ch - num2 - mFont.tahoma_7.getHeight() + num6, mFont.CENTER, mFont.tahoma_7_grey);
						}
					}
				}
				this.dyEff = 65;
			}
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x000308EC File Offset: 0x0002ECEC
	public new void hide()
	{
		this.statusMe = 15;
		global::Char.chatPopup = null;
	}

	// Token: 0x0400069C RID: 1692
	public const sbyte BINH_KHI = 0;

	// Token: 0x0400069D RID: 1693
	public const sbyte PHONG_CU = 1;

	// Token: 0x0400069E RID: 1694
	public const sbyte TRANG_SUC = 2;

	// Token: 0x0400069F RID: 1695
	public const sbyte DUOC_PHAM = 3;

	// Token: 0x040006A0 RID: 1696
	public const sbyte TAP_HOA = 4;

	// Token: 0x040006A1 RID: 1697
	public const sbyte THU_KHO = 5;

	// Token: 0x040006A2 RID: 1698
	public const sbyte DA_LUYEN = 6;

	// Token: 0x040006A3 RID: 1699
	public NpcTemplate template;

	// Token: 0x040006A4 RID: 1700
	public int npcId;

	// Token: 0x040006A5 RID: 1701
	public bool isFocus = true;

	// Token: 0x040006A6 RID: 1702
	public static NpcTemplate[] arrNpcTemplate;

	// Token: 0x040006A7 RID: 1703
	public int sys;

	// Token: 0x040006A8 RID: 1704
	public new bool isHide;

	// Token: 0x040006A9 RID: 1705
	private int duaHauIndex;

	// Token: 0x040006AA RID: 1706
	private int dyEff;

	// Token: 0x040006AB RID: 1707
	public static bool mabuEff;

	// Token: 0x040006AC RID: 1708
	public static int tMabuEff;

	// Token: 0x040006AD RID: 1709
	private static int[] shock_x = new int[]
	{
		1,
		-1,
		1,
		-1
	};

	// Token: 0x040006AE RID: 1710
	private static int[] shock_y = new int[]
	{
		1,
		-1,
		-1,
		1
	};

	// Token: 0x040006AF RID: 1711
	public static int shock_scr;

	// Token: 0x040006B0 RID: 1712
	public int[] duahau;

	// Token: 0x040006B1 RID: 1713
	public new int seconds;

	// Token: 0x040006B2 RID: 1714
	public new long last;

	// Token: 0x040006B3 RID: 1715
	public new long cur;

	// Token: 0x040006B4 RID: 1716
	public int idItem;
}
