using System;

// Token: 0x02000083 RID: 131
public class Npc : global::Char
{
	// Token: 0x0600067D RID: 1661 RVA: 0x0006D454 File Offset: 0x0006B654
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
		bool flag = npcId != -1;
		if (flag)
		{
			this.template = Npc.arrNpcTemplate[templateId];
		}
		bool flag2 = templateId == 23 || templateId == 42;
		if (flag2)
		{
			this.ch = 45;
		}
		bool flag3 = templateId == 51;
		if (flag3)
		{
			this.isShadown = false;
			this.duaHauIndex = status;
		}
		bool flag4 = this.template != null;
		if (flag4)
		{
			bool flag5 = this.template.name == null;
			if (flag5)
			{
				this.template.name = string.Empty;
			}
			this.template.name = Res.changeString(this.template.name);
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0006D550 File Offset: 0x0006B750
	public void setStatus(sbyte s, int sc)
	{
		this.duaHauIndex = (int)s;
		this.last = (this.cur = mSystem.currentTimeMillis());
		this.seconds = sc;
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0006D580 File Offset: 0x0006B780
	public static void clearEffTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			npc.effTask = null;
			npc.indexEffTask = -1;
		}
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0006D5C8 File Offset: 0x0006B7C8
	public override void update()
	{
		bool flag = this.template.npcTemplateId == 51;
		if (flag)
		{
			this.cur = mSystem.currentTimeMillis();
			bool flag2 = this.cur - this.last >= 1000L;
			if (flag2)
			{
				this.seconds--;
				this.last = this.cur;
				bool flag3 = this.seconds < 0;
				if (flag3)
				{
					this.seconds = 0;
				}
			}
		}
		bool isShadown = this.isShadown;
		if (isShadown)
		{
			base.updateShadown();
		}
		bool flag4 = this.effTask == null;
		if (flag4)
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
			bool flag5 = global::Char.myCharz().ctaskId >= 9 && global::Char.myCharz().ctaskId <= 10 && global::Char.myCharz().nClass.classId > 0 && (int)array[global::Char.myCharz().nClass.classId] == this.template.npcTemplateId;
			if (flag5)
			{
				bool flag6 = global::Char.myCharz().taskMaint == null;
				if (flag6)
				{
					this.effTask = GameScr.efs[57];
					this.indexEffTask = 0;
				}
				else
				{
					bool flag7 = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length;
					if (flag7)
					{
						this.effTask = GameScr.efs[62];
						this.indexEffTask = 0;
					}
				}
			}
			else
			{
				sbyte taskNpcId = GameScr.getTaskNpcId();
				bool flag8 = global::Char.myCharz().taskMaint == null && (int)taskNpcId == this.template.npcTemplateId;
				if (flag8)
				{
					this.indexEffTask = 0;
				}
				else
				{
					bool flag9 = global::Char.myCharz().taskMaint != null && (int)taskNpcId == this.template.npcTemplateId;
					if (flag9)
					{
						bool flag10 = global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length;
						if (flag10)
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
		}
		base.update();
		bool flag11 = TileMap.mapID == 51;
		if (flag11)
		{
			bool flag12 = this.cx > global::Char.myCharz().cx;
			if (flag12)
			{
				this.cdir = -1;
			}
			else
			{
				this.cdir = 1;
			}
			bool flag13 = this.template.npcTemplateId % 2 == 0;
			if (flag13)
			{
				bool flag14 = this.cf == 1;
				if (flag14)
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

	// Token: 0x06000681 RID: 1665 RVA: 0x0006D88C File Offset: 0x0006BA8C
	public void paintHead(mGraphics g, int xStart, int yStart)
	{
		Part part = GameScr.parts[this.template.headId];
		bool flag = this.cdir == 1;
		if (flag)
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 0, 0);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 2, 24);
		}
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0006D938 File Offset: 0x0006BB38
	public override void paint(mGraphics g)
	{
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			bool flag = this.isHide;
			if (!flag)
			{
				bool flag2 = !base.isPaint();
				if (!flag2)
				{
					bool flag3 = this.statusMe == 15;
					if (!flag3)
					{
						bool flag4 = this.cTypePk != 0;
						if (flag4)
						{
							base.paint(g);
						}
						else
						{
							bool flag5 = this.template == null;
							if (!flag5)
							{
								bool flag6 = this.template.npcTemplateId != 4 && this.template.npcTemplateId != 51 && this.template.npcTemplateId != 50;
								if (flag6)
								{
									g.drawImage(TileMap.bong, this.cx, this.cy, 3);
								}
								bool flag7 = this.template.npcTemplateId == 3;
								if (flag7)
								{
									SmallImage.drawSmallImage(g, 265, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
									bool flag8 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
									if (flag8)
									{
										bool flag9 = ChatPopup.currChatPopup == null;
										if (flag9)
										{
											g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch + 4, mGraphics.BOTTOM | mGraphics.HCENTER);
										}
									}
									this.dyEff = 60;
								}
								else
								{
									bool flag10 = this.template.npcTemplateId != 4;
									if (flag10)
									{
										bool flag11 = this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51;
										if (flag11)
										{
											bool flag12 = this.duahau != null;
											if (flag12)
											{
												bool flag13 = this.template.npcTemplateId == 50 && Npc.mabuEff;
												if (flag13)
												{
													Npc.tMabuEff++;
													bool flag14 = GameCanvas.gameTick % 3 == 0;
													if (flag14)
													{
														Effect me = new Effect(19, this.cx + Res.random(-50, 50), this.cy, 2, 1, -1);
														EffecMn.addEff(me);
													}
													bool flag15 = GameCanvas.gameTick % 15 == 0;
													if (flag15)
													{
														Effect me2 = new Effect(18, this.cx + Res.random(-5, 5), this.cy + Res.random(-90, 0), 2, 1, -1);
														EffecMn.addEff(me2);
													}
													bool flag16 = Npc.tMabuEff == 100;
													if (flag16)
													{
														GameScr.gI().activeSuperPower(this.cx, this.cy);
													}
													bool flag17 = Npc.tMabuEff == 110;
													if (flag17)
													{
														Npc.mabuEff = false;
														this.template.npcTemplateId = 4;
													}
												}
												int num = 0;
												bool flag18 = SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null;
												if (flag18)
												{
													num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
												}
												SmallImage.drawSmallImage(g, this.duahau[this.duaHauIndex], this.cx + Res.random(-1, 1), this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
												bool flag19 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
												if (flag19)
												{
													bool flag20 = ChatPopup.currChatPopup == null;
													if (flag20)
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
										else
										{
											bool flag21 = this.template.npcTemplateId == 6;
											if (flag21)
											{
												SmallImage.drawSmallImage(g, 545, this.cx, this.cy + 5, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
												bool flag22 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
												if (flag22)
												{
													bool flag23 = ChatPopup.currChatPopup == null;
													if (flag23)
													{
														g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9, mGraphics.BOTTOM | mGraphics.HCENTER);
													}
												}
												mFont.tahoma_7b_white.drawString(g, TileMap.zoneID.ToString() + string.Empty, this.cx, this.cy - this.ch + 19 - mFont.tahoma_7.getHeight(), mFont.CENTER);
											}
											else
											{
												int headId = this.template.headId;
												int legId = this.template.legId;
												int bodyId = this.template.bodyId;
												Part part = GameScr.parts[headId];
												Part part2 = GameScr.parts[legId];
												Part part3 = GameScr.parts[bodyId];
												bool flag24 = this.cdir == 1;
												if (flag24)
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
												bool flag25 = TileMap.mapID != 51;
												if (flag25)
												{
													int num2 = 15;
													bool flag26 = this.template.npcTemplateId == 47;
													if (flag26)
													{
														num2 = 47;
													}
													bool flag27 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
													if (flag27)
													{
														bool flag28 = ChatPopup.currChatPopup == null;
														if (flag28)
														{
															int num3 = 0;
															int num4 = 0;
															bool flag29 = global::Char.myCharz().npcFocus.template.npcTemplateId == 28 || global::Char.myCharz().npcFocus.template.npcTemplateId == 41;
															if (flag29)
															{
																num3 = 3;
																num4 = -12;
															}
															g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx + num3, this.cy - this.ch - (num2 - 8) + num4, mGraphics.BOTTOM | mGraphics.HCENTER);
														}
													}
													else
													{
														bool flag30 = this.template.npcTemplateId == 47;
														if (flag30)
														{
														}
													}
												}
												this.dyEff = 65;
											}
										}
									}
								}
								bool flag31 = this.indexEffTask >= 0 && this.effTask != null && this.cTypePk == 0;
								if (flag31)
								{
									SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy - this.dyEff, 0, mGraphics.VCENTER | mGraphics.HCENTER);
									bool flag32 = GameCanvas.gameTick % 2 == 0;
									if (flag32)
									{
										this.indexEffTask++;
										bool flag33 = this.indexEffTask >= this.effTask.arrEfInfo.Length;
										if (flag33)
										{
											this.indexEffTask = 0;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0006E468 File Offset: 0x0006C668
	public new void paintName(mGraphics g)
	{
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			bool flag = this.isHide;
			if (!flag)
			{
				bool flag2 = !base.isPaint();
				if (!flag2)
				{
					bool flag3 = this.statusMe == 15;
					if (!flag3)
					{
						bool flag4 = this.template == null;
						if (!flag4)
						{
							bool flag5 = this.template.npcTemplateId == 3;
							if (flag5)
							{
								bool flag6 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
								if (flag6)
								{
									mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
								}
								else
								{
									mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 3 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
								}
								this.dyEff = 60;
							}
							else
							{
								bool flag7 = this.template.npcTemplateId != 4;
								if (flag7)
								{
									bool flag8 = this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51;
									if (flag8)
									{
										bool flag9 = this.duahau != null;
										if (flag9)
										{
											int num = 0;
											bool flag10 = SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null;
											if (flag10)
											{
												num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
											}
											bool flag11 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
											if (flag11)
											{
												mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num, mFont.CENTER, mFont.tahoma_7_grey);
											}
											else
											{
												mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - num + 16, mFont.CENTER, mFont.tahoma_7_grey);
											}
										}
									}
									else
									{
										bool flag12 = this.template.npcTemplateId == 6;
										if (flag12)
										{
											bool flag13 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
											if (flag13)
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
											bool flag14 = TileMap.mapID != 51;
											if (flag14)
											{
												int num2 = 15;
												bool flag15 = this.template.npcTemplateId == 47;
												if (flag15)
												{
													num2 = 47;
												}
												bool flag16 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
												if (flag16)
												{
													bool flag17 = TileMap.mapID != 113;
													if (flag17)
													{
														int num3 = 0;
														int num4 = 0;
														bool flag18 = global::Char.myCharz().npcFocus.template.npcTemplateId == 28 || global::Char.myCharz().npcFocus.template.npcTemplateId == 41;
														if (flag18)
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
													bool flag19 = this.template.npcTemplateId == 47;
													if (flag19)
													{
														num2 = 40;
													}
													bool flag20 = TileMap.mapID != 113;
													if (flag20)
													{
														int num5 = 0;
														int num6 = 0;
														bool flag21 = this.template.npcTemplateId == 28 || this.template.npcTemplateId == 41;
														if (flag21)
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
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0006E9AD File Offset: 0x0006CBAD
	public new void hide()
	{
		this.statusMe = 15;
		global::Char.chatPopup = null;
	}

	// Token: 0x04000E45 RID: 3653
	public const sbyte BINH_KHI = 0;

	// Token: 0x04000E46 RID: 3654
	public const sbyte PHONG_CU = 1;

	// Token: 0x04000E47 RID: 3655
	public const sbyte TRANG_SUC = 2;

	// Token: 0x04000E48 RID: 3656
	public const sbyte DUOC_PHAM = 3;

	// Token: 0x04000E49 RID: 3657
	public const sbyte TAP_HOA = 4;

	// Token: 0x04000E4A RID: 3658
	public const sbyte THU_KHO = 5;

	// Token: 0x04000E4B RID: 3659
	public const sbyte DA_LUYEN = 6;

	// Token: 0x04000E4C RID: 3660
	public NpcTemplate template;

	// Token: 0x04000E4D RID: 3661
	public int npcId;

	// Token: 0x04000E4E RID: 3662
	public bool isFocus = true;

	// Token: 0x04000E4F RID: 3663
	public static NpcTemplate[] arrNpcTemplate;

	// Token: 0x04000E50 RID: 3664
	public int sys;

	// Token: 0x04000E51 RID: 3665
	public new bool isHide;

	// Token: 0x04000E52 RID: 3666
	private int duaHauIndex;

	// Token: 0x04000E53 RID: 3667
	private int dyEff;

	// Token: 0x04000E54 RID: 3668
	public static bool mabuEff;

	// Token: 0x04000E55 RID: 3669
	public static int tMabuEff;

	// Token: 0x04000E56 RID: 3670
	private static int[] shock_x = new int[]
	{
		1,
		-1,
		1,
		-1
	};

	// Token: 0x04000E57 RID: 3671
	private static int[] shock_y = new int[]
	{
		1,
		-1,
		-1,
		1
	};

	// Token: 0x04000E58 RID: 3672
	public static int shock_scr;

	// Token: 0x04000E59 RID: 3673
	public int[] duahau;

	// Token: 0x04000E5A RID: 3674
	public new int seconds;

	// Token: 0x04000E5B RID: 3675
	public new long last;

	// Token: 0x04000E5C RID: 3676
	public new long cur;

	// Token: 0x04000E5D RID: 3677
	public int idItem;
}
