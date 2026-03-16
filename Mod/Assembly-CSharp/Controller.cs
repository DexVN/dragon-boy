using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class Controller : IMessageHandler
{
	// Token: 0x060004C4 RID: 1220 RVA: 0x0003D1CA File Offset: 0x0003B5CA
	public static Controller gI()
	{
		if (Controller.me == null)
		{
			Controller.me = new Controller();
		}
		return Controller.me;
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0003D1E5 File Offset: 0x0003B5E5
	public static Controller gI2()
	{
		if (Controller.me2 == null)
		{
			Controller.me2 = new Controller();
		}
		return Controller.me2;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0003D200 File Offset: 0x0003B600
	public void onConnectOK(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectOK();
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0003D20D File Offset: 0x0003B60D
	public void onConnectionFail(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectionFail();
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0003D21A File Offset: 0x0003B61A
	public void onDisconnected(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onDisconnected();
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0003D228 File Offset: 0x0003B628
	public void requestItemPlayer(Message msg)
	{
		try
		{
			int num = (int)msg.reader().readUnsignedByte();
			Item item = GameScr.currentCharViewInfo.arrItemBody[num];
			item.saleCoinLock = msg.reader().readInt();
			item.sys = (int)msg.reader().readByte();
			item.options = new MyVector();
			try
			{
				for (;;)
				{
					item.options.addElement(new ItemOption((int)msg.reader().readUnsignedByte(), (int)msg.reader().readUnsignedShort()));
				}
			}
			catch (Exception ex)
			{
				Cout.println("Loi tairequestItemPlayer 1" + ex.ToString());
			}
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tairequestItemPlayer 2" + ex2.ToString());
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0003D300 File Offset: 0x0003B700
	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			if ((int)msg.command != -74)
			{
				Res.outz("=========> [READ] cmd= " + msg.command);
			}
			global::Char @char = null;
			MyVector myVector = new MyVector();
			int i = 0;
			GameCanvas.timeLoading = 15;
			Controller2.readMessage(msg);
			sbyte command = msg.command;
			switch (command + 99)
			{
			case 0:
			{
				InfoDlg.hide();
				sbyte b = msg.reader().readByte();
				if ((int)b == 0)
				{
					GameCanvas.panel.vEnemy.removeAllElements();
					int num = (int)msg.reader().readUnsignedByte();
					for (int j = 0; j < num; j++)
					{
						global::Char char2 = new global::Char();
						char2.charID = msg.reader().readInt();
						char2.head = (int)msg.reader().readShort();
						char2.headICON = (int)msg.reader().readShort();
						char2.body = (int)msg.reader().readShort();
						char2.leg = (int)msg.reader().readShort();
						char2.bag = (int)msg.reader().readShort();
						char2.cName = msg.reader().readUTF();
						InfoItem infoItem = new InfoItem(msg.reader().readUTF());
						bool flag = msg.reader().readBoolean();
						infoItem.charInfo = char2;
						infoItem.isOnline = flag;
						Res.outz("isonline = " + flag);
						GameCanvas.panel.vEnemy.addElement(infoItem);
					}
					GameCanvas.panel.setTypeEnemy();
					GameCanvas.panel.show();
				}
				break;
			}
			case 1:
			{
				sbyte b2 = msg.reader().readByte();
				GameCanvas.menu.showMenu = false;
				if ((int)b2 == 0)
				{
					GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
				}
				break;
			}
			case 2:
				global::Char.myCharz().cNangdong = (long)msg.reader().readInt();
				break;
			case 3:
			{
				sbyte typeTop = msg.reader().readByte();
				GameCanvas.panel.vTop.removeAllElements();
				string topName = msg.reader().readUTF();
				sbyte b3 = msg.reader().readByte();
				for (int k = 0; k < (int)b3; k++)
				{
					int rank = msg.reader().readInt();
					int pId = msg.reader().readInt();
					short headID = msg.reader().readShort();
					short headICON = msg.reader().readShort();
					short body = msg.reader().readShort();
					short leg = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					TopInfo topInfo = new TopInfo();
					topInfo.rank = rank;
					topInfo.headID = (int)headID;
					topInfo.headICON = (int)headICON;
					topInfo.body = body;
					topInfo.leg = leg;
					topInfo.name = name;
					topInfo.info = info;
					topInfo.info2 = msg.reader().readUTF();
					topInfo.pId = pId;
					GameCanvas.panel.vTop.addElement(topInfo);
				}
				GameCanvas.panel.topName = topName;
				GameCanvas.panel.setTypeTop(typeTop);
				GameCanvas.panel.show();
				break;
			}
			case 4:
			{
				sbyte b4 = msg.reader().readByte();
				Res.outz("type= " + b4);
				if ((int)b4 == 0)
				{
					int num2 = msg.reader().readInt();
					short templateId = msg.reader().readShort();
					int num3 = msg.readInt3Byte();
					SoundMn.gI().explode_1();
					if (num2 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe = new Mob(num2, false, false, false, false, false, (int)templateId, 1, num3, 0, num3, (short)(global::Char.myCharz().cx + ((global::Char.myCharz().cdir != 1) ? -40 : 40)), (short)global::Char.myCharz().cy, 4, 0);
						global::Char.myCharz().mobMe.isMobMe = true;
						EffecMn.addEff(new Effect(18, global::Char.myCharz().mobMe.x, global::Char.myCharz().mobMe.y, 2, 10, -1));
						global::Char.myCharz().tMobMeBorn = 30;
						GameScr.vMob.addElement(global::Char.myCharz().mobMe);
					}
					else
					{
						@char = GameScr.findCharInMap(num2);
						if (@char != null)
						{
							@char.mobMe = new Mob(num2, false, false, false, false, false, (int)templateId, 1, num3, 0, num3, (short)@char.cx, (short)@char.cy, 4, 0)
							{
								isMobMe = true
							};
							GameScr.vMob.addElement(@char.mobMe);
						}
						else if (GameScr.findMobInMap(num2) == null)
						{
							Mob mob = new Mob(num2, false, false, false, false, false, (int)templateId, 1, num3, 0, num3, -100, -100, 4, 0);
							mob.isMobMe = true;
							GameScr.vMob.addElement(mob);
						}
					}
				}
				if ((int)b4 == 1)
				{
					int num4 = msg.reader().readInt();
					int mobId = (int)msg.reader().readByte();
					Res.outz("mod attack id= " + num4);
					if (num4 == global::Char.myCharz().charID)
					{
						if (GameScr.findMobInMap(mobId) != null)
						{
							global::Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num4);
						if (@char != null && GameScr.findMobInMap(mobId) != null)
						{
							@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
				}
				if ((int)b4 == 2)
				{
					int num5 = msg.reader().readInt();
					int num6 = msg.reader().readInt();
					int num7 = msg.readInt3Byte();
					int cHPNew = msg.readInt3Byte();
					if (num5 == global::Char.myCharz().charID)
					{
						Res.outz("mob dame= " + num7);
						@char = GameScr.findCharInMap(num6);
						if (@char != null)
						{
							@char.cHPNew = cHPNew;
							if (global::Char.myCharz().mobMe.isBusyAttackSomeOne)
							{
								@char.doInjure(num7, 0, false, true);
							}
							else
							{
								global::Char.myCharz().mobMe.dame = num7;
								global::Char.myCharz().mobMe.setAttack(@char);
							}
						}
					}
					else
					{
						Mob mob2 = GameScr.findMobInMap(num5);
						if (mob2 != null)
						{
							if (num6 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().cHPNew = cHPNew;
								if (mob2.isBusyAttackSomeOne)
								{
									global::Char.myCharz().doInjure(num7, 0, false, true);
								}
								else
								{
									mob2.dame = num7;
									mob2.setAttack(global::Char.myCharz());
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num6);
								if (@char != null)
								{
									@char.cHPNew = cHPNew;
									if (mob2.isBusyAttackSomeOne)
									{
										@char.doInjure(num7, 0, false, true);
									}
									else
									{
										mob2.dame = num7;
										mob2.setAttack(@char);
									}
								}
							}
						}
					}
				}
				if ((int)b4 == 3)
				{
					int num8 = msg.reader().readInt();
					int mobId2 = msg.reader().readInt();
					int hp = msg.readInt3Byte();
					int num9 = msg.readInt3Byte();
					@char = null;
					if (global::Char.myCharz().charID == num8)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num8);
					}
					if (@char != null)
					{
						Mob mob2 = GameScr.findMobInMap(mobId2);
						if (@char.mobMe != null)
						{
							@char.mobMe.attackOtherMob(mob2);
						}
						if (mob2 != null)
						{
							mob2.hp = hp;
							mob2.updateHp_bar();
							if (num9 == 0)
							{
								mob2.x = mob2.xFirst;
								mob2.y = mob2.yFirst;
								GameScr.startFlyText(mResources.miss, mob2.x, mob2.y - mob2.h, 0, -2, mFont.MISS);
							}
							else
							{
								GameScr.startFlyText("-" + num9, mob2.x, mob2.y - mob2.h, 0, -2, mFont.ORANGE);
							}
						}
					}
				}
				if ((int)b4 == 4)
				{
				}
				if ((int)b4 == 5)
				{
					int num10 = msg.reader().readInt();
					sbyte b5 = msg.reader().readByte();
					int mobId3 = msg.reader().readInt();
					int num11 = msg.readInt3Byte();
					int hp2 = msg.readInt3Byte();
					@char = null;
					if (num10 == global::Char.myCharz().charID)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num10);
					}
					if (@char == null)
					{
						return;
					}
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					{
						@char.setSkillPaint(GameScr.sks[(int)b5], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[(int)b5], 1);
					}
					Mob mob3 = GameScr.findMobInMap(mobId3);
					if (@char.cx <= mob3.x)
					{
						@char.cdir = 1;
					}
					else
					{
						@char.cdir = -1;
					}
					@char.mobFocus = mob3;
					mob3.hp = hp2;
					mob3.updateHp_bar();
					GameCanvas.debug("SA83v2", 2);
					if (num11 == 0)
					{
						mob3.x = mob3.xFirst;
						mob3.y = mob3.yFirst;
						GameScr.startFlyText(mResources.miss, mob3.x, mob3.y - mob3.h, 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num11, mob3.x, mob3.y - mob3.h, 0, -2, mFont.ORANGE);
					}
				}
				if ((int)b4 == 6)
				{
					int num12 = msg.reader().readInt();
					if (num12 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe.startDie();
					}
					else
					{
						@char = GameScr.findCharInMap(num12);
						if (@char != null)
						{
							@char.mobMe.startDie();
						}
					}
				}
				if ((int)b4 == 7)
				{
					int num13 = msg.reader().readInt();
					if (num13 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe = null;
						for (int l = 0; l < GameScr.vMob.size(); l++)
						{
							if (((Mob)GameScr.vMob.elementAt(l)).mobId == num13)
							{
								GameScr.vMob.removeElementAt(l);
							}
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num13);
						for (int m = 0; m < GameScr.vMob.size(); m++)
						{
							if (((Mob)GameScr.vMob.elementAt(m)).mobId == num13)
							{
								GameScr.vMob.removeElementAt(m);
							}
						}
						if (@char != null)
						{
							@char.mobMe = null;
						}
					}
				}
				break;
			}
			case 5:
				while (msg.reader().available() > 0)
				{
					short num14 = msg.reader().readShort();
					int num15 = msg.reader().readInt();
					for (int n = 0; n < global::Char.myCharz().vSkill.size(); n++)
					{
						Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(n);
						if (skill != null && skill.skillId == num14)
						{
							if (num15 < skill.coolDown)
							{
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num15);
							}
							Res.outz(string.Concat(new object[]
							{
								"1 chieu id= ",
								skill.template.id,
								" cooldown= ",
								num15,
								"curr cool down= ",
								skill.coolDown
							}));
						}
					}
				}
				break;
			case 6:
			{
				short num16 = msg.reader().readShort();
				BgItem.newSmallVersion = new sbyte[(int)num16];
				for (int num17 = 0; num17 < (int)num16; num17++)
				{
					BgItem.newSmallVersion[num17] = msg.reader().readByte();
				}
				break;
			}
			case 7:
				Main.typeClient = (int)msg.reader().readByte();
				if (Rms.loadRMSString("ResVersion") == null)
				{
					Rms.clearAll();
				}
				Rms.saveRMSInt("clienttype", Main.typeClient);
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				if (Rms.loadRMSString("ResVersion") == null)
				{
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				}
				break;
			case 8:
			{
				sbyte b6 = msg.reader().readByte();
				GameCanvas.panel.mapNames = new string[(int)b6];
				GameCanvas.panel.planetNames = new string[(int)b6];
				for (int num18 = 0; num18 < (int)b6; num18++)
				{
					GameCanvas.panel.mapNames[num18] = msg.reader().readUTF();
					GameCanvas.panel.planetNames[num18] = msg.reader().readUTF();
				}
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				break;
			}
			case 9:
			{
				sbyte b7 = msg.reader().readByte();
				int num19 = msg.reader().readInt();
				Res.outz("===> UPDATE_BODY:    type = " + b7);
				@char = ((global::Char.myCharz().charID != num19) ? GameScr.findCharInMap(num19) : global::Char.myCharz());
				if ((int)b7 != -1)
				{
					short num20 = msg.reader().readShort();
					short num21 = msg.reader().readShort();
					short num22 = msg.reader().readShort();
					sbyte isMonkey = msg.reader().readByte();
					if (@char != null)
					{
						if (@char.charID == num19)
						{
							@char.isMask = true;
							@char.isMonkey = isMonkey;
							if ((int)@char.isMonkey != 0)
							{
								@char.isWaitMonkey = false;
								@char.isLockMove = false;
							}
						}
						else if (@char != null)
						{
							@char.isMask = true;
							@char.isMonkey = isMonkey;
						}
						if (num20 != -1)
						{
							@char.head = (int)num20;
						}
						if (num21 != -1)
						{
							@char.body = (int)num21;
						}
						if (num22 != -1)
						{
							@char.leg = (int)num22;
						}
					}
				}
				if ((int)b7 == -1 && @char != null)
				{
					@char.isMask = false;
					@char.isMonkey = 0;
				}
				if (@char != null)
				{
				}
				break;
			}
			default:
				if (command != -112)
				{
					if (command == -107)
					{
						sbyte b8 = msg.reader().readByte();
						if ((int)b8 == 0)
						{
							global::Char.myCharz().havePet = false;
						}
						if ((int)b8 == 1)
						{
							global::Char.myCharz().havePet = true;
						}
						if ((int)b8 == 2)
						{
							InfoDlg.hide();
							global::Char.myPetz().head = (int)msg.reader().readShort();
							global::Char.myPetz().setDefaultPart();
							int num23 = (int)msg.reader().readUnsignedByte();
							Res.outz("num body = " + num23);
							global::Char.myPetz().arrItemBody = new Item[num23];
							for (int num24 = 0; num24 < num23; num24++)
							{
								short num25 = msg.reader().readShort();
								Res.outz("template id= " + num25);
								if (num25 != -1)
								{
									Res.outz("1");
									global::Char.myPetz().arrItemBody[num24] = new Item();
									global::Char.myPetz().arrItemBody[num24].template = ItemTemplates.get(num25);
									int num26 = (int)global::Char.myPetz().arrItemBody[num24].template.type;
									global::Char.myPetz().arrItemBody[num24].quantity = msg.reader().readInt();
									Res.outz("3");
									global::Char.myPetz().arrItemBody[num24].info = msg.reader().readUTF();
									global::Char.myPetz().arrItemBody[num24].content = msg.reader().readUTF();
									int num27 = (int)msg.reader().readUnsignedByte();
									Res.outz("option size= " + num27);
									if (num27 != 0)
									{
										global::Char.myPetz().arrItemBody[num24].itemOption = new ItemOption[num27];
										for (int num28 = 0; num28 < global::Char.myPetz().arrItemBody[num24].itemOption.Length; num28++)
										{
											int num29 = (int)msg.reader().readUnsignedByte();
											int param = (int)msg.reader().readUnsignedShort();
											if (num29 != -1)
											{
												global::Char.myPetz().arrItemBody[num24].itemOption[num28] = new ItemOption(num29, param);
											}
										}
									}
									if (num26 == 0)
									{
										global::Char.myPetz().body = (int)global::Char.myPetz().arrItemBody[num24].template.part;
									}
									else if (num26 == 1)
									{
										global::Char.myPetz().leg = (int)global::Char.myPetz().arrItemBody[num24].template.part;
									}
								}
							}
							global::Char.myPetz().cHP = msg.readInt3Byte();
							global::Char.myPetz().cHPFull = msg.readInt3Byte();
							global::Char.myPetz().cMP = msg.readInt3Byte();
							global::Char.myPetz().cMPFull = msg.readInt3Byte();
							global::Char.myPetz().cDamFull = msg.readInt3Byte();
							global::Char.myPetz().cName = msg.reader().readUTF();
							global::Char.myPetz().currStrLevel = msg.reader().readUTF();
							global::Char.myPetz().cPower = msg.reader().readLong();
							global::Char.myPetz().cTiemNang = msg.reader().readLong();
							global::Char.myPetz().petStatus = msg.reader().readByte();
							global::Char.myPetz().cStamina = (int)msg.reader().readShort();
							global::Char.myPetz().cMaxStamina = msg.reader().readShort();
							global::Char.myPetz().cCriticalFull = (int)msg.reader().readByte();
							global::Char.myPetz().cDefull = (int)msg.reader().readShort();
							global::Char.myPetz().arrPetSkill = new Skill[(int)msg.reader().readByte()];
							Res.outz("SKILLENT = " + global::Char.myPetz().arrPetSkill);
							for (int num30 = 0; num30 < global::Char.myPetz().arrPetSkill.Length; num30++)
							{
								short num31 = msg.reader().readShort();
								if (num31 != -1)
								{
									global::Char.myPetz().arrPetSkill[num30] = Skills.get(num31);
								}
								else
								{
									global::Char.myPetz().arrPetSkill[num30] = new Skill();
									global::Char.myPetz().arrPetSkill[num30].template = null;
									global::Char.myPetz().arrPetSkill[num30].moreInfo = msg.reader().readUTF();
								}
							}
							if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
							{
								GameCanvas.panel2 = new Panel();
								GameCanvas.panel2.tabName[7] = new string[][]
								{
									new string[]
									{
										string.Empty
									}
								};
								GameCanvas.panel2.setTypeBodyOnly();
								GameCanvas.panel2.show();
								GameCanvas.panel.setTypePetMain();
								GameCanvas.panel.show();
							}
							else
							{
								GameCanvas.panel.tabName[21] = mResources.petMainTab;
								GameCanvas.panel.setTypePetMain();
								GameCanvas.panel.show();
							}
						}
					}
				}
				else
				{
					sbyte b9 = msg.reader().readByte();
					if ((int)b9 == 0)
					{
						sbyte mobIndex = msg.reader().readByte();
						GameScr.findMobInMap(mobIndex).clearBody();
					}
					if ((int)b9 == 1)
					{
						sbyte mobIndex2 = msg.reader().readByte();
						GameScr.findMobInMap(mobIndex2).setBody(msg.reader().readShort());
					}
				}
				break;
			case 11:
				GameCanvas.endDlg();
				GameCanvas.serverScreen.switchToMe();
				break;
			case 12:
			{
				Res.outz("GET UPDATE_DATA " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createData(msg.reader(), true);
				msg.reader().reset();
				sbyte[] array = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array);
				sbyte[] data = new sbyte[]
				{
					GameScr.vcData
				};
				Rms.saveRMS("NRdataVersion", data);
				LoginScr.isUpdateData = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					Res.outz(string.Concat(new object[]
					{
						GameScr.vsData,
						",",
						GameScr.vsMap,
						",",
						GameScr.vsSkill,
						",",
						GameScr.vsItem
					}));
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
					return;
				}
				break;
			}
			case 13:
			{
				sbyte b10 = msg.reader().readByte();
				Res.outz("server gui ve giao dich action = " + b10);
				if ((int)b10 == 0)
				{
					int playerID = msg.reader().readInt();
					GameScr.gI().giaodich(playerID);
				}
				if ((int)b10 == 1)
				{
					int num32 = msg.reader().readInt();
					global::Char char3 = GameScr.findCharInMap(num32);
					if (char3 == null)
					{
						return;
					}
					GameCanvas.panel.setTypeGiaoDich(char3);
					GameCanvas.panel.show();
					Service.gI().getPlayerMenu(num32);
				}
				if ((int)b10 == 2)
				{
					sbyte b11 = msg.reader().readByte();
					for (int num33 = 0; num33 < GameCanvas.panel.vMyGD.size(); num33++)
					{
						Item item = (Item)GameCanvas.panel.vMyGD.elementAt(num33);
						if (item.indexUI == (int)b11)
						{
							GameCanvas.panel.vMyGD.removeElement(item);
							break;
						}
					}
				}
				if ((int)b10 == 5)
				{
				}
				if ((int)b10 == 6)
				{
					GameCanvas.panel.isFriendLock = true;
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.isFriendLock = true;
					}
					GameCanvas.panel.vFriendGD.removeAllElements();
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.vFriendGD.removeAllElements();
					}
					int friendMoneyGD = msg.reader().readInt();
					sbyte b12 = msg.reader().readByte();
					Res.outz("item size = " + b12);
					for (int num34 = 0; num34 < (int)b12; num34++)
					{
						Item item2 = new Item();
						item2.template = ItemTemplates.get(msg.reader().readShort());
						item2.quantity = msg.reader().readInt();
						int num35 = (int)msg.reader().readUnsignedByte();
						if (num35 != 0)
						{
							item2.itemOption = new ItemOption[num35];
							for (int num36 = 0; num36 < item2.itemOption.Length; num36++)
							{
								int num37 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								if (num37 != -1)
								{
									item2.itemOption[num36] = new ItemOption(num37, param2);
									item2.compare = GameCanvas.panel.getCompare(item2);
								}
							}
						}
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.vFriendGD.addElement(item2);
						}
						else
						{
							GameCanvas.panel.vFriendGD.addElement(item2);
						}
					}
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.setTabGiaoDich(false);
						GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
					}
					else
					{
						GameCanvas.panel.friendMoneyGD = friendMoneyGD;
						if (GameCanvas.panel.currentTabIndex == 2)
						{
							GameCanvas.panel.setTabGiaoDich(false);
						}
					}
				}
				if ((int)b10 == 7)
				{
					InfoDlg.hide();
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.hide();
					}
				}
				break;
			}
			case 14:
			{
				Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				sbyte b13 = msg.reader().readByte();
				if ((int)b13 == 0)
				{
					int num38 = (int)msg.reader().readUnsignedShort();
					Res.outz("lent =" + num38);
					sbyte[] imageData = new sbyte[num38];
					msg.reader().read(ref imageData, 0, num38);
					GameScr.imgCapcha = Image.createImage(imageData, 0, num38);
					GameScr.gI().keyInput = "-----";
					GameScr.gI().strCapcha = msg.reader().readUTF();
					GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
					GameScr.gI().mobCapcha = new Mob();
					GameScr.gI().right = null;
				}
				if ((int)b13 == 1)
				{
					MobCapcha.isAttack = true;
				}
				if ((int)b13 == 2)
				{
					MobCapcha.explode = true;
					GameScr.gI().right = GameScr.gI().cmdFocus;
				}
				break;
			}
			case 15:
			{
				int index = (int)msg.reader().readUnsignedByte();
				Mob mob4 = null;
				try
				{
					mob4 = (Mob)GameScr.vMob.elementAt(index);
				}
				catch (Exception ex)
				{
				}
				if (mob4 != null)
				{
					mob4.maxHp = msg.reader().readInt();
				}
				break;
			}
			case 16:
			{
				sbyte b14 = msg.reader().readByte();
				if ((int)b14 == 0)
				{
					int num39 = (int)msg.reader().readShort();
					int bgRID = (int)msg.reader().readShort();
					int num40 = (int)msg.reader().readUnsignedByte();
					int num41 = msg.reader().readInt();
					string text = msg.reader().readUTF();
					int num42 = (int)msg.reader().readShort();
					int num43 = (int)msg.reader().readShort();
					sbyte b15 = msg.reader().readByte();
					if ((int)b15 == 1)
					{
						GameScr.gI().isRongNamek = true;
					}
					else
					{
						GameScr.gI().isRongNamek = false;
					}
					GameScr.gI().xR = num42;
					GameScr.gI().yR = num43;
					Res.outz(string.Concat(new object[]
					{
						"xR= ",
						num42,
						" yR= ",
						num43,
						" +++++++++++++++++++++++++++++++++++++++"
					}));
					if (global::Char.myCharz().charID == num41)
					{
						GameCanvas.panel.hideNow();
						GameScr.gI().activeRongThanEff(true);
					}
					else if (TileMap.mapID == num39 && TileMap.zoneID == num40)
					{
						GameScr.gI().activeRongThanEff(false);
					}
					else if (mGraphics.zoomLevel > 1)
					{
						GameScr.gI().doiMauTroi();
					}
					GameScr.gI().mapRID = num39;
					GameScr.gI().bgRID = bgRID;
					GameScr.gI().zoneRID = num40;
				}
				if ((int)b14 == 1)
				{
					Res.outz(string.Concat(new object[]
					{
						"map RID = ",
						GameScr.gI().mapRID,
						" zone RID= ",
						GameScr.gI().zoneRID
					}));
					Res.outz(string.Concat(new object[]
					{
						"map ID = ",
						TileMap.mapID,
						" zone ID= ",
						TileMap.zoneID
					}));
					if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
					{
						GameScr.gI().hideRongThanEff();
					}
					else
					{
						GameScr.gI().isRongThanXuatHien = false;
						if (GameScr.gI().isRongNamek)
						{
							GameScr.gI().isRongNamek = false;
						}
					}
				}
				if ((int)b14 == 2)
				{
				}
				break;
			}
			case 17:
			{
				sbyte b16 = msg.reader().readByte();
				TileMap.tileIndex = new int[(int)b16][][];
				TileMap.tileType = new int[(int)b16][];
				for (int num44 = 0; num44 < (int)b16; num44++)
				{
					sbyte b17 = msg.reader().readByte();
					TileMap.tileType[num44] = new int[(int)b17];
					TileMap.tileIndex[num44] = new int[(int)b17][];
					for (int num45 = 0; num45 < (int)b17; num45++)
					{
						TileMap.tileType[num44][num45] = msg.reader().readInt();
						sbyte b18 = msg.reader().readByte();
						TileMap.tileIndex[num44][num45] = new int[(int)b18];
						for (int num46 = 0; num46 < (int)b18; num46++)
						{
							TileMap.tileIndex[num44][num45][num46] = (int)msg.reader().readByte();
						}
					}
				}
				break;
			}
			case 18:
			{
				sbyte b19 = msg.reader().readByte();
				if ((int)b19 == 0)
				{
					string src = msg.reader().readUTF();
					string src2 = msg.reader().readUTF();
					GameCanvas.panel.setTypeCombine();
					GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
					GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
					GameCanvas.panel.show();
				}
				if ((int)b19 == 1)
				{
					GameCanvas.panel.vItemCombine.removeAllElements();
					sbyte b20 = msg.reader().readByte();
					for (int num47 = 0; num47 < (int)b20; num47++)
					{
						sbyte b21 = msg.reader().readByte();
						for (int num48 = 0; num48 < global::Char.myCharz().arrItemBag.Length; num48++)
						{
							Item item3 = global::Char.myCharz().arrItemBag[num48];
							if (item3 != null && item3.indexUI == (int)b21)
							{
								item3.isSelect = true;
								GameCanvas.panel.vItemCombine.addElement(item3);
							}
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabCombine();
					}
				}
				if ((int)b19 == 2)
				{
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(0);
				}
				if ((int)b19 == 3)
				{
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(0);
				}
				if ((int)b19 == 4)
				{
					short iconID = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(1);
				}
				if ((int)b19 == 5)
				{
					short iconID2 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID2;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(2);
				}
				if ((int)b19 == 6)
				{
					short iconID3 = msg.reader().readShort();
					short iconID4 = msg.reader().readShort();
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(3);
					GameCanvas.panel.iconID1 = iconID3;
					GameCanvas.panel.iconID3 = iconID4;
				}
				if ((int)b19 == 7)
				{
					short iconID5 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID5;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(4);
				}
				if ((int)b19 == 8)
				{
					GameCanvas.panel.iconID3 = -1;
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(4);
				}
				short num49 = 21;
				try
				{
					num49 = msg.reader().readShort();
					int num50 = (int)msg.reader().readShort();
					int num51 = (int)msg.reader().readShort();
					GameCanvas.panel.xS = num50 - GameScr.cmx;
					GameCanvas.panel.yS = num51 - GameScr.cmy;
				}
				catch (Exception ex2)
				{
				}
				for (int num52 = 0; num52 < GameScr.vNpc.size(); num52++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(num52);
					if (npc.template.npcTemplateId == (int)num49)
					{
						GameCanvas.panel.xS = npc.cx - GameScr.cmx;
						GameCanvas.panel.yS = npc.cy - GameScr.cmy;
						GameCanvas.panel.idNPC = (int)num49;
						break;
					}
				}
				break;
			}
			case 19:
			{
				sbyte b22 = msg.reader().readByte();
				InfoDlg.hide();
				if ((int)b22 == 0)
				{
					GameCanvas.panel.vFriend.removeAllElements();
					int num53 = (int)msg.reader().readUnsignedByte();
					for (int num54 = 0; num54 < num53; num54++)
					{
						global::Char char4 = new global::Char();
						char4.charID = msg.reader().readInt();
						char4.head = (int)msg.reader().readShort();
						char4.headICON = (int)msg.reader().readShort();
						char4.body = (int)msg.reader().readShort();
						char4.leg = (int)msg.reader().readShort();
						char4.bag = (int)msg.reader().readUnsignedByte();
						char4.cName = msg.reader().readUTF();
						bool isOnline = msg.reader().readBoolean();
						InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
						infoItem2.charInfo = char4;
						infoItem2.isOnline = isOnline;
						GameCanvas.panel.vFriend.addElement(infoItem2);
					}
					GameCanvas.panel.setTypeFriend();
					GameCanvas.panel.show();
				}
				if ((int)b22 == 3)
				{
					MyVector vFriend = GameCanvas.panel.vFriend;
					int num55 = msg.reader().readInt();
					Res.outz("online offline id=" + num55);
					for (int num56 = 0; num56 < vFriend.size(); num56++)
					{
						InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num56);
						if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num55)
						{
							Res.outz("online= " + infoItem3.isOnline);
							infoItem3.isOnline = msg.reader().readBoolean();
							break;
						}
					}
				}
				if ((int)b22 == 2)
				{
					MyVector vFriend2 = GameCanvas.panel.vFriend;
					int num57 = msg.reader().readInt();
					for (int num58 = 0; num58 < vFriend2.size(); num58++)
					{
						InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num58);
						if (infoItem4.charInfo != null && infoItem4.charInfo.charID == num57)
						{
							vFriend2.removeElement(infoItem4);
							break;
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabFriend();
					}
				}
				break;
			}
			case 20:
			{
				InfoDlg.hide();
				int num59 = msg.reader().readInt();
				global::Char charMenu = GameCanvas.panel.charMenu;
				if (charMenu == null)
				{
					return;
				}
				charMenu.cPower = msg.reader().readLong();
				charMenu.currStrLevel = msg.reader().readUTF();
				break;
			}
			case 22:
			{
				short num60 = msg.reader().readShort();
				SmallImage.newSmallVersion = new sbyte[(int)num60];
				SmallImage.maxSmall = num60;
				SmallImage.imgNew = new Small[(int)num60];
				for (int num61 = 0; num61 < (int)num60; num61++)
				{
					SmallImage.newSmallVersion[num61] = msg.reader().readByte();
				}
				break;
			}
			case 23:
			{
				sbyte b23 = msg.reader().readByte();
				if ((int)b23 == 0)
				{
					sbyte b24 = msg.reader().readByte();
					if ((int)b24 <= 0)
					{
						return;
					}
					global::Char.myCharz().arrArchive = new Archivement[(int)b24];
					for (int num62 = 0; num62 < (int)b24; num62++)
					{
						global::Char.myCharz().arrArchive[num62] = new Archivement();
						global::Char.myCharz().arrArchive[num62].info1 = num62 + 1 + ". " + msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num62].info2 = msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num62].money = (int)msg.reader().readShort();
						global::Char.myCharz().arrArchive[num62].isFinish = msg.reader().readBoolean();
						global::Char.myCharz().arrArchive[num62].isRecieve = msg.reader().readBoolean();
					}
					GameCanvas.panel.setTypeArchivement();
					GameCanvas.panel.show();
				}
				else if ((int)b23 == 1)
				{
					int num63 = (int)msg.reader().readUnsignedByte();
					if (global::Char.myCharz().arrArchive[num63] != null)
					{
						global::Char.myCharz().arrArchive[num63].isRecieve = true;
					}
				}
				break;
			}
			case 25:
			{
				if (ServerListScreen.stopDownload)
				{
					return;
				}
				if (!GameCanvas.isGetResourceFromServer())
				{
					Service.gI().getResource(3, null);
					SmallImage.loadBigRMS();
					SplashScr.imgLogo = null;
					if (Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null)
					{
						LoginScr.isContinueToLogin = true;
					}
					GameCanvas.loginScr = new LoginScr();
					GameCanvas.loginScr.switchToMe();
					return;
				}
				bool flag2 = true;
				sbyte b25 = msg.reader().readByte();
				if ((int)b25 == 0)
				{
					int num64 = msg.reader().readInt();
					string text2 = Rms.loadRMSString("ResVersion");
					int num65 = (text2 == null || !(text2 != string.Empty)) ? -1 : int.Parse(text2);
					bool flag3 = Session_ME.gI().isCompareIPConnect();
					if (flag3)
					{
						if (num65 == -1 || num65 != num64)
						{
							GameCanvas.serverScreen.show2();
						}
						else
						{
							Res.outz("login ngay");
							SmallImage.loadBigRMS();
							SplashScr.imgLogo = null;
							ServerListScreen.loadScreen = true;
							if (GameCanvas.currentScreen != GameCanvas.loginScr)
							{
								GameCanvas.serverScreen.switchToMe();
							}
						}
					}
					else
					{
						Session_ME.gI().close();
						ServerListScreen.loadScreen = true;
						ServerListScreen.isAutoConect = false;
						ServerListScreen.countDieConnect = 1000;
						GameCanvas.serverScreen.switchToMe();
					}
				}
				if ((int)b25 == 1)
				{
					ServerListScreen.strWait = mResources.downloading_data;
					short nBig = msg.reader().readShort();
					ServerListScreen.nBig = (int)nBig;
					Service.gI().getResource(2, null);
				}
				if ((int)b25 == 2)
				{
					try
					{
						Controller.isLoadingData = true;
						GameCanvas.endDlg();
						ServerListScreen.demPercent++;
						ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
						string original = msg.reader().readUTF();
						string[] array2 = Res.split(original, "/", 0);
						string filename = "x" + mGraphics.zoomLevel + array2[array2.Length - 1];
						int num66 = msg.reader().readInt();
						sbyte[] data2 = new sbyte[num66];
						msg.reader().read(ref data2, 0, num66);
						Rms.saveRMS(filename, data2);
					}
					catch (Exception ex3)
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
				if ((int)b25 == 3 && flag2)
				{
					Controller.isLoadingData = false;
					int num67 = msg.reader().readInt();
					Res.outz("last version= " + num67);
					Rms.saveRMSString("ResVersion", num67 + string.Empty);
					Service.gI().getResource(3, null);
					GameCanvas.endDlg();
					SplashScr.imgLogo = null;
					SmallImage.loadBigRMS();
					mSystem.gcc();
					ServerListScreen.bigOk = true;
					ServerListScreen.loadScreen = true;
					GameScr.gI().loadGameScr();
					if (GameCanvas.currentScreen != GameCanvas.loginScr)
					{
						GameCanvas.serverScreen.switchToMe();
					}
				}
				break;
			}
			case 29:
			{
				Res.outz("BIG MESSAGE .......................................");
				GameCanvas.endDlg();
				int avatar = (int)msg.reader().readShort();
				string chat = msg.reader().readUTF();
				ChatPopup.addBigMessage(chat, 100000, new Npc(-1, 0, 0, 0, 0, 0)
				{
					avatar = avatar
				});
				sbyte b26 = msg.reader().readByte();
				if ((int)b26 == 0)
				{
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
				}
				if ((int)b26 == 1)
				{
					string p = msg.reader().readUTF();
					string caption = msg.reader().readUTF();
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(caption, ChatPopup.serverChatPopUp, 1000, p);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
				}
				break;
			}
			case 30:
				global::Char.myCharz().cMaxStamina = msg.reader().readShort();
				break;
			case 31:
				global::Char.myCharz().cStamina = (int)msg.reader().readShort();
				break;
			case 32:
			{
				this.demCount += 1f;
				int num68 = msg.reader().readInt();
				sbyte[] array3 = null;
				try
				{
					array3 = NinjaUtil.readByteArray(msg);
					if (num68 == 3896)
					{
					}
					SmallImage.imgNew[num68].img = this.createImage(array3);
				}
				catch (Exception ex4)
				{
					array3 = null;
					SmallImage.imgNew[num68].img = Image.createRGBImage(new int[1], 1, 1, true);
				}
				if (array3 != null && mGraphics.zoomLevel > 1)
				{
					Rms.saveRMS(mGraphics.zoomLevel + "Small" + num68, array3);
				}
				break;
			}
			case 33:
			{
				short id = msg.reader().readShort();
				sbyte[] data3 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById((int)id);
				sbyte b27 = msg.reader().readSByte();
				if ((int)b27 == 0)
				{
					effDataById.readData(data3);
				}
				else
				{
					effDataById.readDataNewBoss(data3, b27);
				}
				sbyte[] array4 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array4, 0, array4.Length);
				break;
			}
			case 34:
			{
				InfoDlg.hide();
				int num69 = msg.reader().readInt();
				sbyte b28 = msg.reader().readByte();
				if ((int)b28 != 0)
				{
					if (global::Char.myCharz().charID == num69)
					{
						Controller.isStopReadMessage = true;
						GameScr.lockTick = 500;
						GameScr.gI().center = null;
						if ((int)b28 == 0 || (int)b28 == 1 || (int)b28 == 3)
						{
							Teleport p2 = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 0, true, ((int)b28 != 1) ? ((int)b28) : global::Char.myCharz().cgender);
							Teleport.addTeleport(p2);
						}
						if ((int)b28 == 2)
						{
							GameScr.lockTick = 50;
							global::Char.myCharz().hide();
						}
					}
					else
					{
						global::Char char5 = GameScr.findCharInMap(num69);
						if (((int)b28 == 0 || (int)b28 == 1 || (int)b28 == 3) && char5 != null)
						{
							char5.isUsePlane = true;
							Teleport.addTeleport(new Teleport(char5.cx, char5.cy, char5.head, char5.cdir, 0, false, ((int)b28 != 1) ? ((int)b28) : char5.cgender)
							{
								id = num69
							});
						}
						if ((int)b28 == 2)
						{
							char5.hide();
						}
					}
				}
				break;
			}
			case 35:
			{
				int num70 = msg.reader().readInt();
				int num71 = (int)msg.reader().readUnsignedByte();
				@char = null;
				if (num70 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num70);
				}
				if (@char == null)
				{
					return;
				}
				@char.bag = num71;
				for (int num72 = 0; num72 < 54; num72++)
				{
					@char.removeEffChar(0, 201 + num72);
				}
				if (@char.bag >= 201 && @char.bag < 255)
				{
					@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
					{
						typeEff = 5
					});
				}
				Res.outz(string.Concat(new object[]
				{
					"cmd:-64 UPDATE BAG PLAER = ",
					(@char != null) ? @char.cName : string.Empty,
					num70,
					" BAG ID= ",
					num71
				}));
				break;
			}
			case 36:
			{
				Res.outz("GET BAG");
				int num73 = (int)msg.reader().readUnsignedByte();
				sbyte b29 = msg.reader().readByte();
				ClanImage clanImage = new ClanImage();
				clanImage.ID = num73;
				if ((int)b29 > 0)
				{
					clanImage.idImage = new short[(int)b29];
					for (int num74 = 0; num74 < (int)b29; num74++)
					{
						clanImage.idImage[num74] = msg.reader().readShort();
						Res.outz(string.Concat(new object[]
						{
							"ID=  ",
							num73,
							" frame= ",
							clanImage.idImage[num74]
						}));
					}
					ClanImage.idImages.put(num73 + string.Empty, clanImage);
				}
				break;
			}
			case 37:
			{
				int num75 = (int)msg.reader().readUnsignedByte();
				sbyte b30 = msg.reader().readByte();
				if ((int)b30 > 0)
				{
					ClanImage clanImage2 = ClanImage.getClanImage((short)num75);
					if (clanImage2 != null)
					{
						clanImage2.idImage = new short[(int)b30];
						for (int num76 = 0; num76 < (int)b30; num76++)
						{
							clanImage2.idImage[num76] = msg.reader().readShort();
							if (clanImage2.idImage[num76] > 0)
							{
								SmallImage.vKeys.addElement(clanImage2.idImage[num76] + string.Empty);
							}
						}
					}
				}
				break;
			}
			case 38:
			{
				int num77 = msg.reader().readInt();
				if (num77 != global::Char.myCharz().charID)
				{
					if (GameScr.findCharInMap(num77) != null)
					{
						GameScr.findCharInMap(num77).clanID = msg.reader().readInt();
						if (GameScr.findCharInMap(num77).clanID == -2)
						{
							GameScr.findCharInMap(num77).isCopy = true;
						}
					}
				}
				else if (global::Char.myCharz().clan != null)
				{
					global::Char.myCharz().clan.ID = msg.reader().readInt();
				}
				break;
			}
			case 39:
			{
				GameCanvas.debug("SA7666", 2);
				int num78 = msg.reader().readInt();
				int num79 = -1;
				if (num78 != global::Char.myCharz().charID)
				{
					global::Char char6 = GameScr.findCharInMap(num78);
					if (char6 == null)
					{
						return;
					}
					if (char6.currentMovePoint != null)
					{
						char6.createShadow(char6.cx, char6.cy, 10);
						char6.cx = char6.currentMovePoint.xEnd;
						char6.cy = char6.currentMovePoint.yEnd;
					}
					int num80 = (int)msg.reader().readUnsignedByte();
					if ((TileMap.tileTypeAtPixel(char6.cx, char6.cy) & 2) == 2)
					{
						char6.setSkillPaint(GameScr.sks[num80], 0);
					}
					else
					{
						char6.setSkillPaint(GameScr.sks[num80], 1);
					}
					sbyte b31 = msg.reader().readByte();
					global::Char[] array5 = new global::Char[(int)b31];
					for (i = 0; i < array5.Length; i++)
					{
						num79 = msg.reader().readInt();
						global::Char char7;
						if (num79 == global::Char.myCharz().charID)
						{
							char7 = global::Char.myCharz();
							if (!GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay)
							{
								Service.gI().requestChangeZone(-1, -1);
								GameScr.isChangeZone = true;
							}
						}
						else
						{
							char7 = GameScr.findCharInMap(num79);
						}
						array5[i] = char7;
						if (i == 0)
						{
							if (char6.cx <= char7.cx)
							{
								char6.cdir = 1;
							}
							else
							{
								char6.cdir = -1;
							}
						}
					}
					if (i > 0)
					{
						char6.attChars = new global::Char[i];
						for (i = 0; i < char6.attChars.Length; i++)
						{
							char6.attChars[i] = array5[i];
						}
						char6.mobFocus = null;
						char6.charFocus = char6.attChars[0];
					}
				}
				else
				{
					sbyte b32 = msg.reader().readByte();
					sbyte b33 = msg.reader().readByte();
					num79 = msg.reader().readInt();
				}
				try
				{
					sbyte b34 = msg.reader().readByte();
					Res.outz("isRead continue = " + b34);
					if ((int)b34 == 1)
					{
						sbyte b35 = msg.reader().readByte();
						Res.outz("type skill = " + b35);
						if (num79 == global::Char.myCharz().charID)
						{
							@char = global::Char.myCharz();
							int num81 = msg.readInt3Byte();
							Res.outz("dame hit = " + num81);
							@char.isDie = msg.reader().readBoolean();
							if (@char.isDie)
							{
								global::Char.isLockKey = true;
							}
							Res.outz("isDie=" + @char.isDie + "---------------------------------------");
							int num82 = 0;
							bool isCrit = msg.reader().readBoolean();
							@char.isCrit = isCrit;
							@char.isMob = false;
							num81 += num82;
							@char.damHP = num81;
							if ((int)b35 == 0)
							{
								@char.doInjure(num81, 0, isCrit, false);
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num79);
							if (@char == null)
							{
								return;
							}
							int num83 = msg.readInt3Byte();
							Res.outz("dame hit= " + num83);
							@char.isDie = msg.reader().readBoolean();
							Res.outz("isDie=" + @char.isDie + "---------------------------------------");
							int num84 = 0;
							bool isCrit2 = msg.reader().readBoolean();
							@char.isCrit = isCrit2;
							@char.isMob = false;
							num83 += num84;
							@char.damHP = num83;
							if ((int)b35 == 0)
							{
								@char.doInjure(num83, 0, isCrit2, false);
							}
						}
					}
				}
				catch (Exception ex5)
				{
				}
				break;
			}
			case 40:
			{
				sbyte typePK = msg.reader().readByte();
				GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
				break;
			}
			case 41:
				break;
			case 42:
			{
				string strInvite = msg.reader().readUTF();
				int clanID = msg.reader().readInt();
				int code = msg.reader().readInt();
				GameScr.gI().clanInvite(strInvite, clanID, code);
				break;
			}
			case 46:
			{
				InfoDlg.hide();
				bool flag4 = false;
				int num85 = msg.reader().readInt();
				Res.outz("clanId= " + num85);
				if (num85 == -1)
				{
					global::Char.myCharz().clan = null;
					ClanMessage.vMessage.removeAllElements();
					if (GameCanvas.panel.member != null)
					{
						GameCanvas.panel.member.removeAllElements();
					}
					if (GameCanvas.panel.myMember != null)
					{
						GameCanvas.panel.myMember.removeAllElements();
					}
					if (GameCanvas.currentScreen == GameScr.gI())
					{
						GameCanvas.panel.setTabClans();
					}
					return;
				}
				GameCanvas.panel.tabIcon = null;
				if (global::Char.myCharz().clan == null)
				{
					global::Char.myCharz().clan = new Clan();
				}
				global::Char.myCharz().clan.ID = num85;
				global::Char.myCharz().clan.name = msg.reader().readUTF();
				global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.powerPoint = msg.reader().readUTF();
				global::Char.myCharz().clan.leaderName = msg.reader().readUTF();
				global::Char.myCharz().clan.currMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.maxMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().role = msg.reader().readByte();
				global::Char.myCharz().clan.clanPoint = msg.reader().readInt();
				global::Char.myCharz().clan.level = (int)msg.reader().readByte();
				GameCanvas.panel.myMember = new MyVector();
				for (int num86 = 0; num86 < global::Char.myCharz().clan.currMember; num86++)
				{
					Member member = new Member();
					member.ID = msg.reader().readInt();
					member.head = msg.reader().readShort();
					member.headICON = msg.reader().readShort();
					member.leg = msg.reader().readShort();
					member.body = msg.reader().readShort();
					member.name = msg.reader().readUTF();
					member.role = msg.reader().readByte();
					member.powerPoint = msg.reader().readUTF();
					member.donate = msg.reader().readInt();
					member.receive_donate = msg.reader().readInt();
					member.clanPoint = msg.reader().readInt();
					member.curClanPoint = msg.reader().readInt();
					member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.myMember.addElement(member);
				}
				int num87 = (int)msg.reader().readUnsignedByte();
				for (int num88 = 0; num88 < num87; num88++)
				{
					this.readClanMsg(msg, -1);
				}
				if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
				{
					GameCanvas.panel.setTabClans();
				}
				if (flag4)
				{
					GameCanvas.panel.setTabClans();
				}
				Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -537 MY CLAN INFO");
				break;
			}
			case 47:
			{
				sbyte b36 = msg.reader().readByte();
				if ((int)b36 == 0)
				{
					Member member2 = new Member();
					member2.ID = msg.reader().readInt();
					member2.head = msg.reader().readShort();
					member2.headICON = msg.reader().readShort();
					member2.leg = msg.reader().readShort();
					member2.body = msg.reader().readShort();
					member2.name = msg.reader().readUTF();
					member2.role = msg.reader().readByte();
					member2.powerPoint = msg.reader().readUTF();
					member2.donate = msg.reader().readInt();
					member2.receive_donate = msg.reader().readInt();
					member2.clanPoint = msg.reader().readInt();
					member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					if (GameCanvas.panel.myMember == null)
					{
						GameCanvas.panel.myMember = new MyVector();
					}
					GameCanvas.panel.myMember.addElement(member2);
					GameCanvas.panel.initTabClans();
				}
				if ((int)b36 == 1)
				{
					GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
					GameCanvas.panel.currentListLength--;
					GameCanvas.panel.initTabClans();
				}
				if ((int)b36 == 2)
				{
					Member member3 = new Member();
					member3.ID = msg.reader().readInt();
					member3.head = msg.reader().readShort();
					member3.headICON = msg.reader().readShort();
					member3.leg = msg.reader().readShort();
					member3.body = msg.reader().readShort();
					member3.name = msg.reader().readUTF();
					member3.role = msg.reader().readByte();
					member3.powerPoint = msg.reader().readUTF();
					member3.donate = msg.reader().readInt();
					member3.receive_donate = msg.reader().readInt();
					member3.clanPoint = msg.reader().readInt();
					member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					for (int num89 = 0; num89 < GameCanvas.panel.myMember.size(); num89++)
					{
						Member member4 = (Member)GameCanvas.panel.myMember.elementAt(num89);
						if (member4.ID == member3.ID)
						{
							if (global::Char.myCharz().charID == member3.ID)
							{
								global::Char.myCharz().role = member3.role;
							}
							Member o = member3;
							GameCanvas.panel.myMember.removeElement(member4);
							GameCanvas.panel.myMember.insertElementAt(o, num89);
							return;
						}
					}
				}
				Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -52  MY CLAN UPDSTE");
				break;
			}
			case 48:
				InfoDlg.hide();
				this.readClanMsg(msg, 0);
				if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
				{
					GameCanvas.panel.initTabClans();
				}
				break;
			case 49:
			{
				InfoDlg.hide();
				GameCanvas.panel.member = new MyVector();
				sbyte b37 = msg.reader().readByte();
				for (int num90 = 0; num90 < (int)b37; num90++)
				{
					Member member5 = new Member();
					member5.ID = msg.reader().readInt();
					member5.head = msg.reader().readShort();
					member5.headICON = msg.reader().readShort();
					member5.leg = msg.reader().readShort();
					member5.body = msg.reader().readShort();
					member5.name = msg.reader().readUTF();
					member5.role = msg.reader().readByte();
					member5.powerPoint = msg.reader().readUTF();
					member5.donate = msg.reader().readInt();
					member5.receive_donate = msg.reader().readInt();
					member5.clanPoint = msg.reader().readInt();
					member5.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.member.addElement(member5);
				}
				GameCanvas.panel.isViewMember = true;
				GameCanvas.panel.isSearchClan = false;
				GameCanvas.panel.isMessage = false;
				GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
				GameCanvas.panel.initTabClans();
				break;
			}
			case 52:
			{
				InfoDlg.hide();
				sbyte b38 = msg.reader().readByte();
				Res.outz("clan = " + b38);
				if ((int)b38 == 0)
				{
					GameCanvas.panel.clanReport = mResources.cannot_find_clan;
					GameCanvas.panel.clans = null;
				}
				else
				{
					GameCanvas.panel.clans = new Clan[(int)b38];
					Res.outz("clan search lent= " + GameCanvas.panel.clans.Length);
					for (int num91 = 0; num91 < GameCanvas.panel.clans.Length; num91++)
					{
						GameCanvas.panel.clans[num91] = new Clan();
						GameCanvas.panel.clans[num91].ID = msg.reader().readInt();
						GameCanvas.panel.clans[num91].name = msg.reader().readUTF();
						GameCanvas.panel.clans[num91].slogan = msg.reader().readUTF();
						GameCanvas.panel.clans[num91].imgID = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num91].powerPoint = msg.reader().readUTF();
						GameCanvas.panel.clans[num91].leaderName = msg.reader().readUTF();
						GameCanvas.panel.clans[num91].currMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num91].maxMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num91].date = msg.reader().readInt();
					}
				}
				GameCanvas.panel.isSearchClan = true;
				GameCanvas.panel.isViewMember = false;
				GameCanvas.panel.isMessage = false;
				if (GameCanvas.panel.isSearchClan)
				{
					GameCanvas.panel.initTabClans();
				}
				break;
			}
			case 53:
			{
				InfoDlg.hide();
				sbyte b39 = msg.reader().readByte();
				if ((int)b39 == 1 || (int)b39 == 3)
				{
					GameCanvas.endDlg();
					ClanImage.vClanImage.removeAllElements();
					int num92 = (int)msg.reader().readUnsignedByte();
					for (int num93 = 0; num93 < num92; num93++)
					{
						ClanImage clanImage3 = new ClanImage();
						clanImage3.ID = (int)msg.reader().readUnsignedByte();
						clanImage3.name = msg.reader().readUTF();
						clanImage3.xu = msg.reader().readInt();
						clanImage3.luong = msg.reader().readInt();
						if (!ClanImage.isExistClanImage(clanImage3.ID))
						{
							ClanImage.addClanImage(clanImage3);
						}
						else
						{
							ClanImage.getClanImage((short)clanImage3.ID).name = clanImage3.name;
							ClanImage.getClanImage((short)clanImage3.ID).xu = clanImage3.xu;
							ClanImage.getClanImage((short)clanImage3.ID).luong = clanImage3.luong;
						}
					}
					if (global::Char.myCharz().clan != null)
					{
						GameCanvas.panel.changeIcon();
					}
				}
				if ((int)b39 == 4)
				{
					global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				}
				break;
			}
			case 54:
			{
				sbyte b40 = msg.reader().readByte();
				int num94 = msg.reader().readInt();
				short num95 = msg.reader().readShort();
				Res.outz(string.Concat(new object[]
				{
					">.SKILL_NOT_FOCUS      skillNotFocusID: ",
					num95,
					" skill type= ",
					b40,
					"   player use= ",
					num94
				}));
				if ((int)b40 == 20)
				{
					sbyte b41 = msg.reader().readByte();
					sbyte dir = msg.reader().readByte();
					short timeGong = msg.reader().readShort();
					bool isFly = (int)msg.reader().readByte() != 0;
					sbyte typePaint = msg.reader().readByte();
					sbyte typeItem = -1;
					try
					{
						typeItem = msg.reader().readByte();
					}
					catch (Exception ex6)
					{
					}
					Res.outz(">.SKILL_NOT_FOCUS  skill typeFrame= " + b41);
					if (global::Char.myCharz().charID == num94)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num94);
					}
					@char.SetSkillPaint_NEW(num95, isFly, b41, typePaint, dir, timeGong, typeItem);
				}
				if ((int)b40 == 21)
				{
					Point point = new Point();
					point.x = (int)msg.reader().readShort();
					point.y = (int)msg.reader().readShort();
					short timeDame = msg.reader().readShort();
					short rangeDame = msg.reader().readShort();
					sbyte typePaint2 = 0;
					sbyte typeItem2 = -1;
					Point[] array6 = null;
					if (global::Char.myCharz().charID == num94)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num94);
					}
					try
					{
						typePaint2 = msg.reader().readByte();
						sbyte b42 = msg.reader().readByte();
						array6 = new Point[(int)b42];
						for (int num96 = 0; num96 < array6.Length; num96++)
						{
							array6[num96] = new Point();
							array6[num96].type = msg.reader().readByte();
							if ((int)array6[num96].type == 0)
							{
								array6[num96].id = (int)msg.reader().readByte();
							}
							else
							{
								array6[num96].id = msg.reader().readInt();
							}
						}
					}
					catch (Exception ex7)
					{
					}
					try
					{
						typeItem2 = msg.reader().readByte();
					}
					catch (Exception ex8)
					{
					}
					Res.outz(string.Concat(new object[]
					{
						">.SKILL_NOT_FOCUS  skill targetDame= ",
						point.x,
						":",
						point.y,
						"    c:",
						@char.cx,
						":",
						@char.cy,
						"   cdir:",
						@char.cdir
					}));
					@char.SetSkillPaint_STT(1, num95, point, timeDame, rangeDame, typePaint2, array6, typeItem2);
				}
				if ((int)b40 == 0)
				{
					Res.outz("id use= " + num94);
					if (global::Char.myCharz().charID != num94)
					{
						@char = GameScr.findCharInMap(num94);
						if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
						{
							@char.setSkillPaint(GameScr.sks[(int)num95], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)num95], 1);
							@char.delayFall = 20;
						}
					}
					else
					{
						global::Char.myCharz().saveLoadPreviousSkill();
						Res.outz("LOAD LAST SKILL");
					}
					sbyte b43 = msg.reader().readByte();
					Res.outz("npc size= " + b43);
					for (int num97 = 0; num97 < (int)b43; num97++)
					{
						sbyte b44 = msg.reader().readByte();
						sbyte b45 = msg.reader().readByte();
						Res.outz("index= " + b44);
						if (num95 >= 42 && num95 <= 48)
						{
							((Mob)GameScr.vMob.elementAt((int)b44)).isFreez = true;
							((Mob)GameScr.vMob.elementAt((int)b44)).seconds = (int)b45;
							((Mob)GameScr.vMob.elementAt((int)b44)).last = (((Mob)GameScr.vMob.elementAt((int)b44)).cur = mSystem.currentTimeMillis());
						}
					}
					sbyte b46 = msg.reader().readByte();
					for (int num98 = 0; num98 < (int)b46; num98++)
					{
						int num99 = msg.reader().readInt();
						sbyte b47 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"player ID= ",
							num99,
							" my ID= ",
							global::Char.myCharz().charID
						}));
						if (num95 >= 42 && num95 <= 48)
						{
							if (num99 == global::Char.myCharz().charID)
							{
								if (!global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isStandAndCharge)
								{
									GameScr.gI().isFreez = true;
									global::Char.myCharz().isFreez = true;
									global::Char.myCharz().freezSeconds = (int)b47;
									global::Char.myCharz().lastFreez = (global::Char.myCharz().currFreez = mSystem.currentTimeMillis());
									global::Char.myCharz().isLockMove = true;
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num99);
								if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
								{
									@char.isFreez = true;
									@char.seconds = (int)b47;
									@char.freezSeconds = (int)b47;
									@char.lastFreez = (GameScr.findCharInMap(num99).currFreez = mSystem.currentTimeMillis());
								}
							}
						}
					}
				}
				if ((int)b40 == 1)
				{
					if (num94 != global::Char.myCharz().charID)
					{
						GameScr.findCharInMap(num94).isCharge = true;
					}
				}
				if ((int)b40 == 3)
				{
					if (num94 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().isCharge = false;
						SoundMn.gI().taitaoPause();
						global::Char.myCharz().saveLoadPreviousSkill();
					}
					else
					{
						GameScr.findCharInMap(num94).isCharge = false;
					}
				}
				if ((int)b40 == 4)
				{
					if (num94 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)(msg.reader().readShort() - 1000);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
						Res.outz(string.Concat(new object[]
						{
							"second= ",
							global::Char.myCharz().seconds,
							" last= ",
							global::Char.myCharz().last
						}));
					}
					else if (GameScr.findCharInMap(num94) != null)
					{
						int cgender = GameScr.findCharInMap(num94).cgender;
						if (cgender == 0)
						{
							GameScr.findCharInMap(num94).useChargeSkill(false);
						}
						else if (cgender == 1)
						{
							GameScr.findCharInMap(num94).useChargeSkill(true);
						}
						GameScr.findCharInMap(num94).skillTemplateId = (int)num95;
						GameScr.findCharInMap(num94).isUseSkillAfterCharge = true;
						GameScr.findCharInMap(num94).seconds = (int)msg.reader().readShort();
						GameScr.findCharInMap(num94).last = mSystem.currentTimeMillis();
					}
				}
				if ((int)b40 == 5)
				{
					if (num94 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().stopUseChargeSkill();
					}
					else if (GameScr.findCharInMap(num94) != null)
					{
						GameScr.findCharInMap(num94).stopUseChargeSkill();
					}
				}
				if ((int)b40 == 6)
				{
					if (num94 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)num95], 0);
					}
					else if (GameScr.findCharInMap(num94) != null)
					{
						GameScr.findCharInMap(num94).setAutoSkillPaint(GameScr.sks[(int)num95], 0);
						SoundMn.gI().gong();
					}
				}
				if ((int)b40 == 7)
				{
					if (num94 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)msg.reader().readShort();
						Res.outz("second = " + global::Char.myCharz().seconds);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
					}
					else if (GameScr.findCharInMap(num94) != null)
					{
						GameScr.findCharInMap(num94).useChargeSkill(true);
						GameScr.findCharInMap(num94).seconds = (int)msg.reader().readShort();
						GameScr.findCharInMap(num94).last = mSystem.currentTimeMillis();
						SoundMn.gI().gong();
					}
				}
				if ((int)b40 == 8)
				{
					if (num94 != global::Char.myCharz().charID)
					{
						if (GameScr.findCharInMap(num94) != null)
						{
							GameScr.findCharInMap(num94).setAutoSkillPaint(GameScr.sks[(int)num95], 0);
						}
					}
				}
				break;
			}
			case 55:
			{
				bool flag5 = false;
				if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
				{
					flag5 = true;
				}
				sbyte b48 = msg.reader().readByte();
				int num100 = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().arrItemShop = new Item[num100][];
				GameCanvas.panel.shopTabName = new string[num100 + (flag5 ? 0 : 1)][];
				for (int num101 = 0; num101 < GameCanvas.panel.shopTabName.Length; num101++)
				{
					GameCanvas.panel.shopTabName[num101] = new string[2];
				}
				if ((int)b48 == 2)
				{
					GameCanvas.panel.maxPageShop = new int[num100];
					GameCanvas.panel.currPageShop = new int[num100];
				}
				if (!flag5)
				{
					GameCanvas.panel.shopTabName[num100] = mResources.inventory;
				}
				for (int num102 = 0; num102 < num100; num102++)
				{
					string[] array7 = Res.split(msg.reader().readUTF(), "\n", 0);
					if ((int)b48 == 2)
					{
						GameCanvas.panel.maxPageShop[num102] = (int)msg.reader().readUnsignedByte();
					}
					if (array7.Length == 2)
					{
						GameCanvas.panel.shopTabName[num102] = array7;
					}
					if (array7.Length == 1)
					{
						GameCanvas.panel.shopTabName[num102][0] = array7[0];
						GameCanvas.panel.shopTabName[num102][1] = string.Empty;
					}
					int num103 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[num102] = new Item[num103];
					Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
					if ((int)b48 == 1)
					{
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
					}
					for (int num104 = 0; num104 < num103; num104++)
					{
						short num105 = msg.reader().readShort();
						if (num105 != -1)
						{
							global::Char.myCharz().arrItemShop[num102][num104] = new Item();
							global::Char.myCharz().arrItemShop[num102][num104].template = ItemTemplates.get(num105);
							Res.outz(string.Concat(new object[]
							{
								"name ",
								num102,
								" = ",
								global::Char.myCharz().arrItemShop[num102][num104].template.name,
								" id templat= ",
								global::Char.myCharz().arrItemShop[num102][num104].template.id
							}));
							if ((int)b48 == 8)
							{
								global::Char.myCharz().arrItemShop[num102][num104].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num102][num104].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num102][num104].quantity = msg.reader().readInt();
							}
							else if ((int)b48 == 4)
							{
								global::Char.myCharz().arrItemShop[num102][num104].reason = msg.reader().readUTF();
							}
							else if ((int)b48 == 0)
							{
								global::Char.myCharz().arrItemShop[num102][num104].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num102][num104].buyGold = msg.reader().readInt();
							}
							else if ((int)b48 == 1)
							{
								global::Char.myCharz().arrItemShop[num102][num104].powerRequire = msg.reader().readLong();
							}
							else if ((int)b48 == 2)
							{
								global::Char.myCharz().arrItemShop[num102][num104].itemId = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num102][num104].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num102][num104].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num102][num104].buyType = msg.reader().readByte();
								global::Char.myCharz().arrItemShop[num102][num104].quantity = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num102][num104].isMe = msg.reader().readByte();
							}
							else if ((int)b48 == 3)
							{
								global::Char.myCharz().arrItemShop[num102][num104].isBuySpec = true;
								global::Char.myCharz().arrItemShop[num102][num104].iconSpec = msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num102][num104].buySpec = msg.reader().readInt();
							}
							int num106 = (int)msg.reader().readUnsignedByte();
							if (num106 != 0)
							{
								global::Char.myCharz().arrItemShop[num102][num104].itemOption = new ItemOption[num106];
								for (int num107 = 0; num107 < global::Char.myCharz().arrItemShop[num102][num104].itemOption.Length; num107++)
								{
									int num108 = (int)msg.reader().readUnsignedByte();
									int param3 = (int)msg.reader().readUnsignedShort();
									if (num108 != -1)
									{
										global::Char.myCharz().arrItemShop[num102][num104].itemOption[num107] = new ItemOption(num108, param3);
										global::Char.myCharz().arrItemShop[num102][num104].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[num102][num104]);
									}
								}
							}
							sbyte b49 = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[num102][num104].newItem = ((int)b49 != 0);
							sbyte b50 = msg.reader().readByte();
							if ((int)b50 == 1)
							{
								int headTemp = (int)msg.reader().readShort();
								int bodyTemp = (int)msg.reader().readShort();
								int legTemp = (int)msg.reader().readShort();
								int bagTemp = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num102][num104].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
							}
							if ((int)b48 == 2 && GameMidlet.intVERSION >= 237)
							{
								global::Char.myCharz().arrItemShop[num102][num104].nameNguoiKyGui = msg.reader().readUTF();
								Res.err("nguoi ki gui  " + global::Char.myCharz().arrItemShop[num102][num104].nameNguoiKyGui);
							}
						}
					}
				}
				if (flag5)
				{
					if ((int)b48 != 2)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					else
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.setTypeKiGuiOnly();
						GameCanvas.panel2.show();
					}
				}
				GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
				if ((int)b48 == 2)
				{
					string[][] array8 = GameCanvas.panel.tabName[1];
					if (flag5)
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array8[0],
							array8[1],
							array8[2],
							array8[3]
						};
					}
					else
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array8[0],
							array8[1],
							array8[2],
							array8[3],
							array8[4]
						};
					}
				}
				GameCanvas.panel.setTypeShop((int)b48);
				GameCanvas.panel.show();
				break;
			}
			case 56:
			{
				sbyte itemAction = msg.reader().readByte();
				sbyte where = msg.reader().readByte();
				sbyte index2 = msg.reader().readByte();
				string info2 = msg.reader().readUTF();
				GameCanvas.panel.itemRequest(itemAction, info2, where, index2);
				break;
			}
			case 57:
				global::Char.myCharz().cHPGoc = msg.readInt3Byte();
				global::Char.myCharz().cMPGoc = msg.readInt3Byte();
				global::Char.myCharz().cDamGoc = msg.reader().readInt();
				global::Char.myCharz().cHPFull = msg.readInt3Byte();
				global::Char.myCharz().cMPFull = msg.readInt3Byte();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				global::Char.myCharz().cspeed = (int)msg.reader().readByte();
				global::Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().cDamFull = msg.reader().readInt();
				global::Char.myCharz().cDefull = msg.reader().readInt();
				global::Char.myCharz().cCriticalFull = (int)msg.reader().readByte();
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().expForOneAdd = msg.reader().readShort();
				global::Char.myCharz().cDefGoc = (int)msg.reader().readShort();
				global::Char.myCharz().cCriticalGoc = (int)msg.reader().readByte();
				InfoDlg.hide();
				break;
			case 58:
			{
				sbyte b51 = msg.reader().readByte();
				global::Char.myCharz().strLevel = new string[(int)b51];
				for (int num109 = 0; num109 < (int)b51; num109++)
				{
					string text3 = msg.reader().readUTF();
					global::Char.myCharz().strLevel[num109] = text3;
				}
				Res.outz("---   xong  level caption cmd : " + msg.command);
				break;
			}
			case 62:
			{
				sbyte b52 = msg.reader().readByte();
				Res.outz("cAction= " + b52);
				if ((int)b52 == 0)
				{
					global::Char.myCharz().head = (int)msg.reader().readShort();
					global::Char.myCharz().setDefaultPart();
					int num110 = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num110);
					global::Char.myCharz().arrItemBody = new Item[num110];
					for (int num111 = 0; num111 < num110; num111++)
					{
						short num112 = msg.reader().readShort();
						if (num112 != -1)
						{
							global::Char.myCharz().arrItemBody[num111] = new Item();
							global::Char.myCharz().arrItemBody[num111].template = ItemTemplates.get(num112);
							int num113 = (int)global::Char.myCharz().arrItemBody[num111].template.type;
							global::Char.myCharz().arrItemBody[num111].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[num111].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[num111].content = msg.reader().readUTF();
							int num114 = (int)msg.reader().readUnsignedByte();
							if (num114 != 0)
							{
								global::Char.myCharz().arrItemBody[num111].itemOption = new ItemOption[num114];
								for (int num115 = 0; num115 < global::Char.myCharz().arrItemBody[num111].itemOption.Length; num115++)
								{
									int num116 = (int)msg.reader().readUnsignedByte();
									int param4 = (int)msg.reader().readUnsignedShort();
									if (num116 != -1)
									{
										global::Char.myCharz().arrItemBody[num111].itemOption[num115] = new ItemOption(num116, param4);
									}
								}
							}
							if (num113 == 0)
							{
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[num111].template.part;
							}
							else if (num113 == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[num111].template.part;
							}
						}
					}
				}
				break;
			}
			case 63:
			{
				sbyte b53 = msg.reader().readByte();
				Res.outz("cAction= " + b53);
				if ((int)b53 == 0)
				{
					int num117 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBag = new Item[num117];
					GameScr.hpPotion = 0;
					Res.outz("numC=" + num117);
					for (int num118 = 0; num118 < num117; num118++)
					{
						short num119 = msg.reader().readShort();
						if (num119 != -1)
						{
							global::Char.myCharz().arrItemBag[num118] = new Item();
							global::Char.myCharz().arrItemBag[num118].template = ItemTemplates.get(num119);
							global::Char.myCharz().arrItemBag[num118].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBag[num118].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num118].content = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num118].indexUI = num118;
							int num120 = (int)msg.reader().readUnsignedByte();
							if (num120 != 0)
							{
								global::Char.myCharz().arrItemBag[num118].itemOption = new ItemOption[num120];
								for (int num121 = 0; num121 < global::Char.myCharz().arrItemBag[num118].itemOption.Length; num121++)
								{
									int num122 = (int)msg.reader().readUnsignedByte();
									int param5 = (int)msg.reader().readUnsignedShort();
									if (num122 != -1)
									{
										global::Char.myCharz().arrItemBag[num118].itemOption[num121] = new ItemOption(num122, param5);
									}
								}
								global::Char.myCharz().arrItemBag[num118].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemBag[num118]);
							}
							if ((int)global::Char.myCharz().arrItemBag[num118].template.type == 11)
							{
							}
							if ((int)global::Char.myCharz().arrItemBag[num118].template.type == 6)
							{
								GameScr.hpPotion += global::Char.myCharz().arrItemBag[num118].quantity;
							}
						}
					}
				}
				if ((int)b53 == 2)
				{
					sbyte b54 = msg.reader().readByte();
					int quantity = msg.reader().readInt();
					int quantity2 = global::Char.myCharz().arrItemBag[(int)b54].quantity;
					global::Char.myCharz().arrItemBag[(int)b54].quantity = quantity;
					if (global::Char.myCharz().arrItemBag[(int)b54].quantity < quantity2 && (int)global::Char.myCharz().arrItemBag[(int)b54].template.type == 6)
					{
						GameScr.hpPotion -= quantity2 - global::Char.myCharz().arrItemBag[(int)b54].quantity;
					}
					if (global::Char.myCharz().arrItemBag[(int)b54].quantity == 0)
					{
						global::Char.myCharz().arrItemBag[(int)b54] = null;
					}
				}
				break;
			}
			case 64:
			{
				sbyte b55 = msg.reader().readByte();
				Res.outz("cAction= " + b55);
				if ((int)b55 == 0)
				{
					int num123 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBox = new Item[num123];
					GameCanvas.panel.hasUse = 0;
					for (int num124 = 0; num124 < num123; num124++)
					{
						short num125 = msg.reader().readShort();
						if (num125 != -1)
						{
							global::Char.myCharz().arrItemBox[num124] = new Item();
							global::Char.myCharz().arrItemBox[num124].template = ItemTemplates.get(num125);
							global::Char.myCharz().arrItemBox[num124].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBox[num124].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBox[num124].content = msg.reader().readUTF();
							int num126 = (int)msg.reader().readUnsignedByte();
							if (num126 != 0)
							{
								global::Char.myCharz().arrItemBox[num124].itemOption = new ItemOption[num126];
								for (int num127 = 0; num127 < global::Char.myCharz().arrItemBox[num124].itemOption.Length; num127++)
								{
									int num128 = (int)msg.reader().readUnsignedByte();
									int param6 = (int)msg.reader().readUnsignedShort();
									if (num128 != -1)
									{
										global::Char.myCharz().arrItemBox[num124].itemOption[num127] = new ItemOption(num128, param6);
									}
								}
							}
							GameCanvas.panel.hasUse++;
						}
					}
				}
				if ((int)b55 == 1)
				{
					bool isBoxClan = false;
					try
					{
						sbyte b56 = msg.reader().readByte();
						if ((int)b56 == 1)
						{
							isBoxClan = true;
						}
					}
					catch (Exception ex9)
					{
					}
					GameCanvas.panel.setTypeBox();
					GameCanvas.panel.isBoxClan = isBoxClan;
					GameCanvas.panel.show();
				}
				if ((int)b55 == 2)
				{
					sbyte b57 = msg.reader().readByte();
					int quantity3 = msg.reader().readInt();
					global::Char.myCharz().arrItemBox[(int)b57].quantity = quantity3;
					if (global::Char.myCharz().arrItemBox[(int)b57].quantity == 0)
					{
						global::Char.myCharz().arrItemBox[(int)b57] = null;
					}
				}
				break;
			}
			case 65:
			{
				sbyte b58 = msg.reader().readByte();
				Res.outz("act= " + b58);
				if ((int)b58 == 0 && GameScr.gI().magicTree != null)
				{
					Res.outz("toi duoc day");
					MagicTree magicTree = GameScr.gI().magicTree;
					magicTree.id = (int)msg.reader().readShort();
					magicTree.name = msg.reader().readUTF();
					magicTree.name = Res.changeString(magicTree.name);
					magicTree.x = (int)msg.reader().readShort();
					magicTree.y = (int)msg.reader().readShort();
					magicTree.level = (int)msg.reader().readByte();
					magicTree.currPeas = (int)msg.reader().readShort();
					magicTree.maxPeas = (int)msg.reader().readShort();
					Res.outz("curr Peas= " + magicTree.currPeas);
					magicTree.strInfo = msg.reader().readUTF();
					magicTree.seconds = msg.reader().readInt();
					magicTree.timeToRecieve = magicTree.seconds;
					sbyte b59 = msg.reader().readByte();
					magicTree.peaPostionX = new int[(int)b59];
					magicTree.peaPostionY = new int[(int)b59];
					for (int num129 = 0; num129 < (int)b59; num129++)
					{
						magicTree.peaPostionX[num129] = (int)msg.reader().readByte();
						magicTree.peaPostionY[num129] = (int)msg.reader().readByte();
					}
					magicTree.isUpdate = msg.reader().readBool();
					magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
				}
				if ((int)b58 == 1)
				{
					myVector = new MyVector();
					try
					{
						while (msg.reader().available() > 0)
						{
							string caption2 = msg.reader().readUTF();
							myVector.addElement(new Command(caption2, GameCanvas.instance, 888392, null));
						}
					}
					catch (Exception ex10)
					{
						Cout.println("Loi MAGIC_TREE " + ex10.ToString());
					}
					GameCanvas.menu.startAt(myVector, 3);
				}
				if ((int)b58 == 2)
				{
					GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
					GameScr.gI().magicTree.seconds = msg.reader().readInt();
					GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
					GameScr.gI().magicTree.isPeasEffect = true;
				}
				break;
			}
			case 67:
			{
				short num130 = msg.reader().readShort();
				int num131 = msg.reader().readInt();
				sbyte[] array9 = null;
				Image image = null;
				try
				{
					array9 = new sbyte[num131];
					for (int num132 = 0; num132 < num131; num132++)
					{
						array9[num132] = msg.reader().readByte();
					}
					image = Image.createImage(array9, 0, num131);
					BgItem.imgNew.put(num130 + string.Empty, image);
				}
				catch (Exception ex11)
				{
					array9 = null;
					BgItem.imgNew.put(num130 + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
				}
				if (array9 != null)
				{
					if (mGraphics.zoomLevel > 1)
					{
						Rms.saveRMS(mGraphics.zoomLevel + "bgItem" + num130, array9);
					}
					BgItemMn.blendcurrBg(num130, image);
				}
				break;
			}
			case 68:
			{
				TileMap.vItemBg.removeAllElements();
				short num133 = msg.reader().readShort();
				Res.err("[ITEM_BACKGROUND] nItem= " + num133);
				for (int num134 = 0; num134 < (int)num133; num134++)
				{
					BgItem bgItem = new BgItem();
					bgItem.id = num134;
					bgItem.idImage = msg.reader().readShort();
					bgItem.layer = msg.reader().readByte();
					bgItem.dx = (int)msg.reader().readShort();
					bgItem.dy = (int)msg.reader().readShort();
					sbyte b60 = msg.reader().readByte();
					bgItem.tileX = new int[(int)b60];
					bgItem.tileY = new int[(int)b60];
					for (int num135 = 0; num135 < (int)b60; num135++)
					{
						bgItem.tileX[num134] = (int)msg.reader().readByte();
						bgItem.tileY[num134] = (int)msg.reader().readByte();
					}
					TileMap.vItemBg.addElement(bgItem);
				}
				break;
			}
			case 69:
				this.messageSubCommand(msg);
				break;
			case 70:
				this.messageNotLogin(msg);
				break;
			case 71:
				this.messageNotMap(msg);
				break;
			case 73:
				ServerListScreen.testConnect = 2;
				GameCanvas.debug("SA2", 2);
				GameCanvas.startOKDlg(msg.reader().readUTF());
				InfoDlg.hide();
				LoginScr.isContinueToLogin = false;
				global::Char.isLoadingMap = false;
				if (GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				break;
			case 74:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case 75:
				if (GameCanvas.currentScreen is GameScr)
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 3000L;
				}
				else
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
				}
				global::Char.isLoadingMap = true;
				Cout.println("GET MAP INFO");
				GameScr.gI().magicTree = null;
				GameCanvas.isLoading = true;
				GameCanvas.debug("SA75", 2);
				GameScr.resetAllvector();
				GameCanvas.endDlg();
				TileMap.vGo.removeAllElements();
				PopUp.vPopups.removeAllElements();
				mSystem.gcc();
				TileMap.mapID = (int)msg.reader().readUnsignedByte();
				TileMap.planetID = msg.reader().readByte();
				TileMap.tileID = (int)msg.reader().readByte();
				TileMap.bgID = (int)msg.reader().readByte();
				Cout.println(string.Concat(new object[]
				{
					"load planet from server: ",
					TileMap.planetID,
					"bgType= ",
					TileMap.bgType,
					"............................."
				}));
				TileMap.typeMap = (int)msg.reader().readByte();
				TileMap.mapName = msg.reader().readUTF();
				TileMap.zoneID = (int)msg.reader().readByte();
				GameCanvas.debug("SA75x1", 2);
				try
				{
					TileMap.loadMapFromResource(TileMap.mapID);
				}
				catch (Exception ex12)
				{
					Service.gI().requestMaptemplate(TileMap.mapID);
					this.messWait = msg;
					return;
				}
				this.loadInfoMap(msg);
				try
				{
					sbyte b61 = msg.reader().readByte();
					TileMap.isMapDouble = ((int)b61 != 0);
				}
				catch (Exception ex13)
				{
				}
				GameScr.cmx = GameScr.cmtoX;
				GameScr.cmy = GameScr.cmtoY;
				break;
			case 77:
				GameCanvas.debug("SA65", 2);
				global::Char.isLockKey = true;
				global::Char.ischangingMap = true;
				GameScr.gI().timeStartMap = 0;
				GameScr.gI().timeLengthMap = 0;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().focus.removeAllElements();
				global::Char.myCharz().testCharId = -9999;
				global::Char.myCharz().killCharId = -9999;
				GameCanvas.resetBg();
				GameScr.gI().resetButton();
				GameScr.gI().center = null;
				break;
			case 78:
			{
				GameCanvas.debug("SA60", 2);
				short num136 = msg.reader().readShort();
				for (int num137 = 0; num137 < GameScr.vItemMap.size(); num137++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num137)).itemMapID == (int)num136)
					{
						GameScr.vItemMap.removeElementAt(num137);
						break;
					}
				}
				break;
			}
			case 79:
			{
				GameCanvas.debug("SA61", 2);
				global::Char.myCharz().itemFocus = null;
				short num136 = msg.reader().readShort();
				for (int num138 = 0; num138 < GameScr.vItemMap.size(); num138++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num138);
					if (itemMap.itemMapID == (int)num136)
					{
						itemMap.setPoint(global::Char.myCharz().cx, global::Char.myCharz().cy - 10);
						string text4 = msg.reader().readUTF();
						i = 0;
						try
						{
							i = (int)msg.reader().readShort();
							if ((int)itemMap.template.type == 9)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().xu += (long)i;
								global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
							}
							else if ((int)itemMap.template.type == 10)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luong += i;
								global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
							}
							else if ((int)itemMap.template.type == 34)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luongKhoa += i;
								global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
							}
						}
						catch (Exception ex14)
						{
						}
						if (text4.Equals(string.Empty))
						{
							if ((int)itemMap.template.type == 9)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.YELLOW);
								SoundMn.gI().getItem();
							}
							else if ((int)itemMap.template.type == 10)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.GREEN);
								SoundMn.gI().getItem();
							}
							else if ((int)itemMap.template.type == 34)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.RED);
								SoundMn.gI().getItem();
							}
							else
							{
								GameScr.info1.addInfo(mResources.you_receive + " " + ((i <= 0) ? string.Empty : (i + " ")) + itemMap.template.name, 0);
								SoundMn.gI().getItem();
							}
							if (i > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 4683)
							{
								ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
							}
						}
						else if (text4.Length == 1)
						{
							Cout.LogError3("strInf.Length =1:  " + text4);
						}
						else
						{
							GameScr.info1.addInfo(text4, 0);
						}
						break;
					}
				}
				break;
			}
			case 80:
			{
				GameCanvas.debug("SA62", 2);
				short num136 = msg.reader().readShort();
				@char = GameScr.findCharInMap(msg.reader().readInt());
				int num139 = 0;
				while (num139 < GameScr.vItemMap.size())
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num139);
					if (itemMap2.itemMapID == (int)num136)
					{
						if (@char == null)
						{
							return;
						}
						itemMap2.setPoint(@char.cx, @char.cy - 10);
						if (itemMap2.x < @char.cx)
						{
							@char.cdir = -1;
						}
						else if (itemMap2.x > @char.cx)
						{
							@char.cdir = 1;
						}
						break;
					}
					else
					{
						num139++;
					}
				}
				break;
			}
			case 81:
			{
				GameCanvas.debug("SA63", 2);
				int num140 = (int)msg.reader().readByte();
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), global::Char.myCharz().arrItemBag[num140].template.id, global::Char.myCharz().cx, global::Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				global::Char.myCharz().arrItemBag[num140] = null;
				break;
			}
			case 85:
				GameCanvas.debug("SA64", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				break;
			case 95:
			{
				GameCanvas.debug("SA76", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameCanvas.debug("SA76v1", 2);
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
				}
				GameCanvas.debug("SA76v2", 2);
				@char.attMobs = new Mob[(int)msg.reader().readByte()];
				for (int num141 = 0; num141 < @char.attMobs.Length; num141++)
				{
					Mob mob5 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
					@char.attMobs[num141] = mob5;
					if (num141 == 0)
					{
						if (@char.cx <= mob5.x)
						{
							@char.cdir = 1;
						}
						else
						{
							@char.cdir = -1;
						}
					}
				}
				GameCanvas.debug("SA76v3", 2);
				@char.charFocus = null;
				@char.mobFocus = @char.attMobs[0];
				global::Char[] array5 = new global::Char[10];
				i = 0;
				try
				{
					for (i = 0; i < array5.Length; i++)
					{
						int num142 = msg.reader().readInt();
						global::Char char8;
						if (num142 == global::Char.myCharz().charID)
						{
							char8 = global::Char.myCharz();
						}
						else
						{
							char8 = GameScr.findCharInMap(num142);
						}
						array5[i] = char8;
						if (i == 0)
						{
							if (@char.cx <= char8.cx)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception ex15)
				{
					Cout.println("Loi PLAYER_ATTACK_N_P " + ex15.ToString());
				}
				GameCanvas.debug("SA76v4", 2);
				if (i > 0)
				{
					@char.attChars = new global::Char[i];
					for (i = 0; i < @char.attChars.Length; i++)
					{
						@char.attChars[i] = array5[i];
					}
					@char.charFocus = @char.attChars[0];
					@char.mobFocus = null;
				}
				GameCanvas.debug("SA76v5", 2);
				break;
			}
			case 99:
				this.readLogin(msg);
				break;
			case 100:
			{
				bool flag6 = msg.reader().readBool();
				Res.outz("isRes= " + flag6);
				if (!flag6)
				{
					GameCanvas.startOKDlg(msg.reader().readUTF());
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = false;
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
					GameCanvas.endDlg();
					GameCanvas.loginScr.doLogin();
				}
				break;
			}
			case 101:
				global::Char.isLoadingMap = false;
				LoginScr.isLoggingIn = false;
				if (!GameScr.isLoadAllData)
				{
					GameScr.gI().initSelectChar();
				}
				BgItem.clearHashTable();
				GameCanvas.endDlg();
				CreateCharScr.isCreateChar = true;
				CreateCharScr.gI().switchToMe();
				break;
			case 105:
				GameCanvas.debug("SA70", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				GameCanvas.endDlg();
				break;
			case 106:
			{
				sbyte type = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				string info3 = msg.reader().readUTF();
				GameCanvas.panel.saleRequest(type, info3, id2);
				break;
			}
			case 110:
			{
				GameCanvas.debug("SA9", 2);
				int num143 = (int)msg.reader().readByte();
				sbyte b62 = msg.reader().readByte();
				if ((int)b62 != 0)
				{
					Mob.arrMobTemplate[num143].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b62);
				}
				else
				{
					Mob.arrMobTemplate[num143].data.readData(NinjaUtil.readByteArray(msg));
				}
				for (int num144 = 0; num144 < GameScr.vMob.size(); num144++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(num144);
					if (mob2.templateId == num143)
					{
						mob2.w = Mob.arrMobTemplate[num143].data.width;
						mob2.h = Mob.arrMobTemplate[num143].data.height;
					}
				}
				sbyte[] array10 = NinjaUtil.readByteArray(msg);
				Image img = Image.createImage(array10, 0, array10.Length);
				Mob.arrMobTemplate[num143].data.img = img;
				int num145 = (int)msg.reader().readByte();
				Mob.arrMobTemplate[num143].data.typeData = num145;
				if (num145 == 1 || num145 == 2)
				{
					this.readFrameBoss(msg, num143);
				}
				break;
			}
			case 119:
				this.phuban_Info(msg);
				break;
			case 123:
				this.read_opt(msg);
				break;
			case 126:
			{
				myVector = new MyVector();
				string text5 = msg.reader().readUTF();
				int num146 = (int)msg.reader().readByte();
				for (int num147 = 0; num147 < num146; num147++)
				{
					string caption3 = msg.reader().readUTF();
					short num148 = msg.reader().readShort();
					myVector.addElement(new Command(caption3, GameCanvas.instance, 88819, num148));
				}
				GameCanvas.menu.startWithoutCloseButton(myVector, 3);
				break;
			}
			case -128:
				GameCanvas.debug("SA58", 2);
				GameScr.gI().openUIZone(msg);
				break;
			case -125:
			{
				GameCanvas.debug("SA68", 2);
				int num149 = (int)msg.reader().readShort();
				for (int num150 = 0; num150 < GameScr.vNpc.size(); num150++)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(num150);
					if (npc2.template.npcTemplateId == num149 && npc2.Equals(global::Char.myCharz().npcFocus))
					{
						string chat2 = msg.reader().readUTF();
						string[] array11 = new string[(int)msg.reader().readByte()];
						for (int num151 = 0; num151 < array11.Length; num151++)
						{
							array11[num151] = msg.reader().readUTF();
						}
						GameScr.gI().createMenu(array11, npc2);
						ChatPopup.addChatPopup(chat2, 100000, npc2);
						return;
					}
				}
				Npc npc3 = new Npc(num149, 0, -100, 100, num149, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				string chat3 = msg.reader().readUTF();
				string[] array12 = new string[(int)msg.reader().readByte()];
				for (int num152 = 0; num152 < array12.Length; num152++)
				{
					array12[num152] = msg.reader().readUTF();
				}
				try
				{
					short avatar2 = msg.reader().readShort();
					npc3.avatar = (int)avatar2;
				}
				catch (Exception ex16)
				{
				}
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				GameScr.gI().createMenu(array12, npc3);
				ChatPopup.addChatPopup(chat3, 100000, npc3);
				break;
			}
			case -124:
				GameCanvas.debug("SA51", 2);
				InfoDlg.hide();
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				myVector = new MyVector();
				try
				{
					for (;;)
					{
						string caption4 = msg.reader().readUTF();
						myVector.addElement(new Command(caption4, GameCanvas.instance, 88822, null));
					}
				}
				catch (Exception ex17)
				{
					Cout.println("Loi OPEN_UI_MENU " + ex17.ToString());
				}
				if (global::Char.myCharz().npcFocus == null)
				{
					return;
				}
				for (int num153 = 0; num153 < global::Char.myCharz().npcFocus.template.menu.Length; num153++)
				{
					string[] array13 = global::Char.myCharz().npcFocus.template.menu[num153];
					myVector.addElement(new Command(array13[0], GameCanvas.instance, 88820, array13));
				}
				GameCanvas.menu.startAt(myVector, 3);
				break;
			case -119:
			{
				GameCanvas.debug("SA67", 2);
				InfoDlg.hide();
				int num149 = (int)msg.reader().readShort();
				Res.outz("OPEN_UI_SAY ID= " + num149);
				string text6 = msg.reader().readUTF();
				text6 = Res.changeString(text6);
				for (int num154 = 0; num154 < GameScr.vNpc.size(); num154++)
				{
					Npc npc4 = (Npc)GameScr.vNpc.elementAt(num154);
					Res.outz("npc id= " + npc4.template.npcTemplateId);
					if (npc4.template.npcTemplateId == num149)
					{
						ChatPopup.addChatPopupMultiLine(text6, 100000, npc4);
						GameCanvas.panel.hideNow();
						return;
					}
				}
				Npc npc5 = new Npc(num149, 0, 0, 0, num149, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				if (npc5.template.npcTemplateId == 5)
				{
					npc5.charID = 5;
				}
				try
				{
					npc5.avatar = (int)msg.reader().readShort();
				}
				catch (Exception ex18)
				{
				}
				ChatPopup.addChatPopupMultiLine(text6, 100000, npc5);
				GameCanvas.panel.hideNow();
				break;
			}
			case -118:
				GameCanvas.debug("SA49", 2);
				GameScr.gI().typeTradeOrder = 2;
				if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
				{
					InfoDlg.showWait();
				}
				break;
			case -117:
			{
				GameCanvas.debug("SA52", 2);
				GameCanvas.taskTick = 150;
				short taskId = msg.reader().readShort();
				sbyte index3 = msg.reader().readByte();
				string text7 = msg.reader().readUTF();
				text7 = Res.changeString(text7);
				string text8 = msg.reader().readUTF();
				text8 = Res.changeString(text8);
				string[] array14 = new string[(int)msg.reader().readByte()];
				string[] array15 = new string[array14.Length];
				GameScr.tasks = new int[array14.Length];
				GameScr.mapTasks = new int[array14.Length];
				short[] array16 = new short[array14.Length];
				short count = -1;
				for (int num155 = 0; num155 < array14.Length; num155++)
				{
					string text9 = msg.reader().readUTF();
					text9 = Res.changeString(text9);
					GameScr.tasks[num155] = (int)msg.reader().readByte();
					GameScr.mapTasks[num155] = (int)msg.reader().readShort();
					string text10 = msg.reader().readUTF();
					text10 = Res.changeString(text10);
					array16[num155] = -1;
					if (!text9.Equals(string.Empty))
					{
						array14[num155] = text9;
						array15[num155] = text10;
					}
				}
				try
				{
					count = msg.reader().readShort();
					for (int num156 = 0; num156 < array14.Length; num156++)
					{
						array16[num156] = msg.reader().readShort();
					}
				}
				catch (Exception ex19)
				{
					Cout.println("Loi TASK_GET " + ex19.ToString());
				}
				global::Char.myCharz().taskMaint = new Task(taskId, index3, text7, text8, array14, array16, count, array15);
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				global::Char.taskAction(false);
				break;
			}
			case -116:
				GameCanvas.debug("SA53", 2);
				GameCanvas.taskTick = 100;
				Res.outz("TASK NEXT");
				global::Char.myCharz().taskMaint.index++;
				global::Char.myCharz().taskMaint.count = 0;
				Npc.clearEffTask();
				global::Char.taskAction(true);
				break;
			case -114:
				GameCanvas.taskTick = 50;
				GameCanvas.debug("SA55", 2);
				global::Char.myCharz().taskMaint.count = msg.reader().readShort();
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				try
				{
					short num157 = msg.reader().readShort();
					short num158 = msg.reader().readShort();
					global::Char.myCharz().x_hint = num157;
					global::Char.myCharz().y_hint = num158;
					Res.outz(string.Concat(new object[]
					{
						"CMD   TASK_UPDATE:43_mapID =    x|y ",
						num157,
						"|",
						num158
					}));
					for (int num159 = 0; num159 < TileMap.vGo.size(); num159++)
					{
						Res.outz("===> " + TileMap.vGo.elementAt(num159));
					}
				}
				catch (Exception ex20)
				{
				}
				break;
			case -111:
				GameCanvas.debug("SA5", 2);
				Cout.LogWarning("Controler RESET_POINT  " + global::Char.ischangingMap);
				global::Char.isLockKey = false;
				global::Char.myCharz().setResetPoint((int)msg.reader().readShort(), (int)msg.reader().readShort());
				break;
			case -110:
				GameCanvas.debug("SA4", 2);
				GameScr.gI().resetButton();
				break;
			case -107:
			{
				sbyte b63 = msg.reader().readByte();
				Panel.vGameInfo.removeAllElements();
				for (int num160 = 0; num160 < (int)b63; num160++)
				{
					GameInfo gameInfo = new GameInfo();
					gameInfo.id = msg.reader().readShort();
					gameInfo.main = msg.reader().readUTF();
					gameInfo.content = msg.reader().readUTF();
					Panel.vGameInfo.addElement(gameInfo);
					bool hasRead = Rms.loadRMSInt(gameInfo.id + string.Empty) != -1;
					gameInfo.hasRead = hasRead;
				}
				break;
			}
			case -103:
			{
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				int num161 = (int)msg.reader().readUnsignedByte();
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[num161], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[num161], 1);
				}
				Mob[] array17 = new Mob[10];
				i = 0;
				try
				{
					for (i = 0; i < array17.Length; i++)
					{
						Mob mob6 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
						array17[i] = mob6;
						if (i == 0)
						{
							if (@char.cx <= mob6.x)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception ex21)
				{
				}
				if (i > 0)
				{
					@char.attMobs = new Mob[i];
					for (i = 0; i < @char.attMobs.Length; i++)
					{
						@char.attMobs[i] = array17[i];
					}
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
				}
				break;
			}
			case -101:
			{
				GameCanvas.debug("SXX6", 2);
				@char = null;
				int num142 = msg.reader().readInt();
				if (num142 == global::Char.myCharz().charID)
				{
					bool flag7 = false;
					@char = global::Char.myCharz();
					@char.cHP = msg.readInt3Byte();
					int num162 = msg.readInt3Byte();
					Res.outz("dame hit = " + num162);
					if (num162 != 0)
					{
						@char.doInjure();
					}
					int num163 = 0;
					try
					{
						flag7 = msg.reader().readBoolean();
						sbyte b64 = msg.reader().readByte();
						if ((int)b64 != -1)
						{
							Res.outz("hit eff= " + b64);
							EffecMn.addEff(new Effect((int)b64, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception ex22)
					{
					}
					num162 += num163;
					if ((int)global::Char.myCharz().cTypePk != 4)
					{
						if (num162 == 0)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
						}
						else
						{
							GameScr.startFlyText("-" + num162, @char.cx, @char.cy - @char.ch, 0, -3, flag7 ? mFont.FATAL : mFont.RED);
						}
					}
				}
				else
				{
					@char = GameScr.findCharInMap(num142);
					if (@char == null)
					{
						return;
					}
					@char.cHP = msg.readInt3Byte();
					bool flag8 = false;
					int num164 = msg.readInt3Byte();
					if (num164 != 0)
					{
						@char.doInjure();
					}
					int num165 = 0;
					try
					{
						flag8 = msg.reader().readBoolean();
						sbyte b65 = msg.reader().readByte();
						if ((int)b65 != -1)
						{
							Res.outz("hit eff= " + b65);
							EffecMn.addEff(new Effect((int)b65, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception ex23)
					{
					}
					num164 += num165;
					if ((int)@char.cTypePk != 4)
					{
						if (num164 == 0)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
						}
						else
						{
							GameScr.startFlyText("-" + num164, @char.cx, @char.cy - @char.ch, 0, -3, flag8 ? mFont.FATAL : mFont.ORANGE);
						}
					}
				}
				break;
			}
			case -100:
			{
				GameCanvas.debug("SZ6", 2);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
				GameCanvas.menu.startAt(myVector2, 3);
				break;
			}
			case -99:
			{
				GameCanvas.debug("SZ7", 2);
				int num142 = msg.reader().readInt();
				global::Char char9;
				if (num142 == global::Char.myCharz().charID)
				{
					char9 = global::Char.myCharz();
				}
				else
				{
					char9 = GameScr.findCharInMap(num142);
				}
				char9.moveFast = new short[3];
				char9.moveFast[0] = 0;
				short num166 = msg.reader().readShort();
				short num167 = msg.reader().readShort();
				char9.moveFast[1] = num166;
				char9.moveFast[2] = num167;
				try
				{
					num142 = msg.reader().readInt();
					global::Char char10;
					if (num142 == global::Char.myCharz().charID)
					{
						char10 = global::Char.myCharz();
					}
					else
					{
						char10 = GameScr.findCharInMap(num142);
					}
					char10.cx = (int)num166;
					char10.cy = (int)num167;
				}
				catch (Exception ex24)
				{
					Cout.println("Loi MOVE_FAST " + ex24.ToString());
				}
				break;
			}
			case -95:
				GameCanvas.debug("SZ3", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.killCharId = global::Char.myCharz().charID;
					global::Char.myCharz().npcFocus = null;
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().itemFocus = null;
					global::Char.myCharz().charFocus = @char;
					global::Char.isManualFocus = true;
					GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
				}
				break;
			case -94:
				GameCanvas.debug("SZ4", 2);
				global::Char.myCharz().killCharId = msg.reader().readInt();
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().charFocus = GameScr.findCharInMap(global::Char.myCharz().killCharId);
				global::Char.isManualFocus = true;
				break;
			case -93:
				GameCanvas.debug("SZ5", 2);
				@char = global::Char.myCharz();
				try
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
				}
				catch (Exception ex25)
				{
					Cout.println("Loi CLEAR_CUU_SAT " + ex25.ToString());
				}
				@char.killCharId = -9999;
				break;
			case -92:
			{
				sbyte b66 = msg.reader().readSByte();
				string text11 = msg.reader().readUTF();
				short num168 = msg.reader().readShort();
				if (ItemTime.isExistMessage((int)b66))
				{
					if (num168 != 0)
					{
						ItemTime.getMessageById((int)b66).initTimeText(b66, text11, (int)num168);
					}
					else
					{
						GameScr.textTime.removeElement(ItemTime.getMessageById((int)b66));
					}
				}
				else
				{
					ItemTime itemTime = new ItemTime();
					itemTime.initTimeText(b66, text11, (int)num168);
					GameScr.textTime.addElement(itemTime);
				}
				break;
			}
			case -91:
				this.readGetImgByName(msg);
				break;
			case -89:
			{
				Res.outz("ADD ITEM TO MAP --------------------------------------");
				GameCanvas.debug("SA6333", 2);
				short num136 = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num169 = msg.reader().readInt();
				short r = 0;
				if (num169 == -2)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap3 = new ItemMap(num169, num136, itemTemplateID, x, y, r);
				bool flag9 = false;
				for (int num170 = 0; num170 < GameScr.vItemMap.size(); num170++)
				{
					ItemMap itemMap4 = (ItemMap)GameScr.vItemMap.elementAt(num170);
					if (itemMap4.itemMapID == itemMap3.itemMapID)
					{
						flag9 = true;
						break;
					}
				}
				if (!flag9)
				{
					GameScr.vItemMap.addElement(itemMap3);
				}
				break;
			}
			case -88:
				SoundMn.IsDelAcc = ((int)msg.reader().readByte() != 0);
				break;
			case -79:
				break;
			case -78:
				break;
			case -76:
			{
				GameCanvas.debug("SXX4", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isDisable = msg.reader().readBool();
				break;
			}
			case -75:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isDontMove = msg.reader().readBool();
				break;
			}
			case -74:
			{
				GameCanvas.debug("SXX8", 2);
				int num142 = msg.reader().readInt();
				if (num142 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num142);
				}
				if (@char == null)
				{
					return;
				}
				Mob mobToAttack = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				if (@char.mobMe != null)
				{
					@char.mobMe.attackOtherMob(mobToAttack);
				}
				break;
			}
			case -73:
			{
				int num142 = msg.reader().readInt();
				if (num142 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num142);
					if (@char == null)
					{
						return;
					}
				}
				@char.cHP = @char.cHPFull;
				@char.cMP = @char.cMPFull;
				@char.cx = (int)msg.reader().readShort();
				@char.cy = (int)msg.reader().readShort();
				@char.liveFromDead();
				break;
			}
			case -72:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isFire = msg.reader().readBool();
				break;
			}
			case -71:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isIce = msg.reader().readBool();
				if (!mob7.isIce)
				{
					ServerEffect.addServerEffect(77, mob7.x, mob7.y - 9, 1);
				}
				break;
			}
			case -70:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isWind = msg.reader().readBool();
				break;
			}
			case -69:
			{
				string info4 = msg.reader().readUTF();
				short num171 = msg.reader().readShort();
				GameCanvas.inputDlg.show(info4, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num171), TField.INPUT_TYPE_ANY);
				break;
			}
			case -67:
				GameCanvas.debug("SA577", 2);
				this.requestItemPlayer(msg);
				break;
			case -65:
			{
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					GameCanvas.endDlg();
				}
				string text12 = msg.reader().readUTF();
				string text13 = msg.reader().readUTF();
				text13 = Res.changeString(text13);
				string text14 = string.Empty;
				global::Char char11 = null;
				sbyte b67 = 0;
				if (!text12.Equals(string.Empty))
				{
					char11 = new global::Char();
					char11.charID = msg.reader().readInt();
					char11.head = (int)msg.reader().readShort();
					char11.headICON = (int)msg.reader().readShort();
					char11.body = (int)msg.reader().readShort();
					char11.bag = (int)msg.reader().readShort();
					char11.leg = (int)msg.reader().readShort();
					b67 = msg.reader().readByte();
					char11.cName = text12;
				}
				text14 += text13;
				InfoDlg.hide();
				if (text12.Equals(string.Empty))
				{
					GameScr.info1.addInfo(text14, 0);
				}
				else
				{
					GameScr.info2.addInfoWithChar(text14, char11, (int)b67 == 0);
					if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
					{
						GameCanvas.panel.initLogMessage();
					}
				}
				break;
			}
			case -63:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case -45:
			{
				sbyte b68 = msg.reader().readByte();
				Res.outz("spec type= " + b68);
				if ((int)b68 == 0)
				{
					Panel.spearcialImage = msg.reader().readShort();
					Panel.specialInfo = msg.reader().readUTF();
				}
				else if ((int)b68 == 1)
				{
					sbyte b69 = msg.reader().readByte();
					global::Char.myCharz().infoSpeacialSkill = new string[(int)b69][];
					global::Char.myCharz().imgSpeacialSkill = new short[(int)b69][];
					GameCanvas.panel.speacialTabName = new string[(int)b69][];
					for (int num172 = 0; num172 < (int)b69; num172++)
					{
						GameCanvas.panel.speacialTabName[num172] = new string[2];
						string[] array18 = Res.split(msg.reader().readUTF(), "\n", 0);
						if (array18.Length == 2)
						{
							GameCanvas.panel.speacialTabName[num172] = array18;
						}
						if (array18.Length == 1)
						{
							GameCanvas.panel.speacialTabName[num172][0] = array18[0];
							GameCanvas.panel.speacialTabName[num172][1] = string.Empty;
						}
						int num173 = (int)msg.reader().readByte();
						global::Char.myCharz().infoSpeacialSkill[num172] = new string[num173];
						global::Char.myCharz().imgSpeacialSkill[num172] = new short[num173];
						for (int num174 = 0; num174 < num173; num174++)
						{
							global::Char.myCharz().imgSpeacialSkill[num172][num174] = msg.reader().readShort();
							global::Char.myCharz().infoSpeacialSkill[num172][num174] = msg.reader().readUTF();
						}
					}
					GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
					GameCanvas.panel.setTypeSpeacialSkill();
					GameCanvas.panel.show();
				}
				break;
			}
			}
			sbyte command2 = msg.command;
			switch (command2 + 17)
			{
			case 0:
				GameCanvas.debug("SA88", 2);
				global::Char.myCharz().meDead = true;
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
				try
				{
					global::Char.myCharz().cPower = msg.reader().readLong();
					global::Char.myCharz().applyCharLevelPercent();
				}
				catch (Exception ex26)
				{
					Cout.println("Loi tai ME_DIE " + msg.command);
				}
				global::Char.myCharz().countKill = 0;
				break;
			case 1:
				GameCanvas.debug("SA90", 2);
				if (global::Char.myCharz().wdx != 0 || global::Char.myCharz().wdy != 0)
				{
					global::Char.myCharz().cx = (int)global::Char.myCharz().wdx;
					global::Char.myCharz().cy = (int)global::Char.myCharz().wdy;
					global::Char.myCharz().wdx = (global::Char.myCharz().wdy = 0);
				}
				global::Char.myCharz().liveFromDead();
				global::Char.myCharz().isLockMove = false;
				global::Char.myCharz().meDead = false;
				break;
			default:
				switch (command2)
				{
				case 95:
				{
					GameCanvas.debug("SA77", 22);
					int num175 = msg.reader().readInt();
					global::Char.myCharz().xu += (long)num175;
					global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
					GameScr.startFlyText((num175 <= 0) ? (string.Empty + num175) : ("+" + num175), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
					break;
				}
				case 96:
					GameCanvas.debug("SA77a", 22);
					global::Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
					break;
				case 97:
				{
					sbyte b70 = msg.reader().readByte();
					for (int num176 = 0; num176 < global::Char.myCharz().taskOrders.size(); num176++)
					{
						TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(num176);
						if (taskOrder.taskId == (int)b70)
						{
							taskOrder.count = (int)msg.reader().readShort();
							break;
						}
					}
					break;
				}
				default:
					switch (command2 + 75)
					{
					case 0:
					{
						Mob mob8 = null;
						try
						{
							mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception ex27)
						{
						}
						if (mob8 != null)
						{
							mob8.levelBoss = msg.reader().readByte();
							if ((int)mob8.levelBoss > 0)
							{
								mob8.typeSuperEff = Res.random(0, 3);
							}
						}
						break;
					}
					default:
						if (command2 != 18)
						{
							if (command2 != 19)
							{
								if (command2 != 44)
								{
									if (command2 != 45)
									{
										if (command2 != 66)
										{
											if (command2 == 74)
											{
												GameCanvas.debug("SA85", 2);
												Mob mob8 = null;
												try
												{
													mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
												}
												catch (Exception ex28)
												{
													Cout.println("Loi tai NPC CHANGE " + msg.command);
												}
												if (mob8 != null && mob8.status != 0 && mob8.status != 0)
												{
													mob8.status = 0;
													ServerEffect.addServerEffect(60, mob8.x, mob8.y, 1);
													ItemMap itemMap5 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob8.x, mob8.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
													GameScr.vItemMap.addElement(itemMap5);
													if (Res.abs(itemMap5.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap5.x - global::Char.myCharz().cx) < 24)
													{
														global::Char.myCharz().charFocus = null;
													}
												}
											}
										}
										else
										{
											Res.outz("ME DIE XP DOWN NOT IMPLEMENT YET!!!!!!!!!!!!!!!!!!!!!!!!!!");
										}
									}
									else
									{
										GameCanvas.debug("SA84", 2);
										Mob mob8 = null;
										try
										{
											mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
										}
										catch (Exception ex29)
										{
											Cout.println("Loi tai NPC_MISS  " + ex29.ToString());
										}
										if (mob8 != null)
										{
											mob8.hp = msg.reader().readInt();
											mob8.updateHp_bar();
											GameScr.startFlyText(mResources.miss, mob8.x, mob8.y - mob8.h, 0, -2, mFont.MISS);
										}
									}
								}
								else
								{
									GameCanvas.debug("SA91", 2);
									int num177 = msg.reader().readInt();
									string text15 = msg.reader().readUTF();
									Res.outz(string.Concat(new object[]
									{
										"user id= ",
										num177,
										" text= ",
										text15
									}));
									if (global::Char.myCharz().charID == num177)
									{
										@char = global::Char.myCharz();
									}
									else
									{
										@char = GameScr.findCharInMap(num177);
									}
									if (@char == null)
									{
										return;
									}
									@char.addInfo(text15);
								}
							}
							else
							{
								global::Char.myCharz().countKill = (int)msg.reader().readUnsignedShort();
								global::Char.myCharz().countKillMax = (int)msg.reader().readUnsignedShort();
							}
						}
						else
						{
							sbyte b71 = msg.reader().readByte();
							for (int num178 = 0; num178 < (int)b71; num178++)
							{
								int charId = msg.reader().readInt();
								int cx = (int)msg.reader().readShort();
								int cy = (int)msg.reader().readShort();
								int cHPShow = msg.readInt3Byte();
								global::Char char12 = GameScr.findCharInMap(charId);
								if (char12 != null)
								{
									char12.cx = cx;
									char12.cy = cy;
									char12.cHP = (char12.cHPShow = cHPShow);
									char12.lastUpdateTime = mSystem.currentTimeMillis();
								}
							}
						}
						break;
					case 2:
					{
						sbyte b72 = msg.reader().readByte();
						for (int num179 = 0; num179 < GameScr.vNpc.size(); num179++)
						{
							Npc npc6 = (Npc)GameScr.vNpc.elementAt(num179);
							if (npc6.template.npcTemplateId == (int)b72)
							{
								sbyte b73 = msg.reader().readByte();
								if ((int)b73 == 0)
								{
									npc6.isHide = true;
								}
								else
								{
									npc6.isHide = false;
								}
								break;
							}
						}
						break;
					}
					}
					break;
				}
				break;
			case 4:
			{
				GameCanvas.debug("SA82", 2);
				int num180 = (int)msg.reader().readUnsignedByte();
				if (num180 > GameScr.vMob.size() - 1 || num180 < 0)
				{
					return;
				}
				Mob mob8 = (Mob)GameScr.vMob.elementAt(num180);
				mob8.sys = (int)msg.reader().readByte();
				mob8.levelBoss = msg.reader().readByte();
				if ((int)mob8.levelBoss != 0)
				{
					mob8.typeSuperEff = Res.random(0, 3);
				}
				mob8.x = mob8.xFirst;
				mob8.y = mob8.yFirst;
				mob8.status = 5;
				mob8.injureThenDie = false;
				mob8.hp = msg.reader().readInt();
				mob8.maxHp = mob8.hp;
				mob8.updateHp_bar();
				ServerEffect.addServerEffect(60, mob8.x, mob8.y, 1);
				break;
			}
			case 5:
			{
				Res.outz("SERVER SEND MOB DIE");
				GameCanvas.debug("SA85", 2);
				Mob mob8 = null;
				try
				{
					mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex30)
				{
					Cout.println("LOi tai NPC_DIE cmd " + msg.command);
				}
				if (mob8 != null && mob8.status != 0 && mob8.status != 0)
				{
					mob8.startDie();
					try
					{
						int num181 = msg.readInt3Byte();
						bool flag10 = msg.reader().readBool();
						if (flag10)
						{
							GameScr.startFlyText("-" + num181, mob8.x, mob8.y - mob8.h, 0, -2, mFont.FATAL);
						}
						else
						{
							GameScr.startFlyText("-" + num181, mob8.x, mob8.y - mob8.h, 0, -2, mFont.ORANGE);
						}
						sbyte b74 = msg.reader().readByte();
						for (int num182 = 0; num182 < (int)b74; num182++)
						{
							ItemMap itemMap6 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob8.x, mob8.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
							int num183 = msg.reader().readInt();
							itemMap6.playerId = num183;
							Res.outz(string.Concat(new object[]
							{
								"playerid= ",
								num183,
								" my id= ",
								global::Char.myCharz().charID
							}));
							GameScr.vItemMap.addElement(itemMap6);
							if (Res.abs(itemMap6.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap6.x - global::Char.myCharz().cx) < 24)
							{
								global::Char.myCharz().charFocus = null;
							}
						}
					}
					catch (Exception ex31)
					{
					}
				}
				break;
			}
			case 6:
			{
				GameCanvas.debug("SA86", 2);
				Mob mob8 = null;
				try
				{
					int index4 = (int)msg.reader().readUnsignedByte();
					mob8 = (Mob)GameScr.vMob.elementAt(index4);
				}
				catch (Exception ex32)
				{
					Res.outz(string.Concat(new object[]
					{
						"Loi tai NPC_ATTACK_ME ",
						msg.command,
						" err= ",
						ex32.StackTrace
					}));
				}
				if (mob8 != null)
				{
					global::Char.myCharz().isDie = false;
					global::Char.isLockKey = false;
					int num184 = msg.readInt3Byte();
					int num185;
					try
					{
						num185 = msg.readInt3Byte();
					}
					catch (Exception ex33)
					{
						num185 = 0;
					}
					if (mob8.isBusyAttackSomeOne)
					{
						global::Char.myCharz().doInjure(num184, num185, false, true);
					}
					else
					{
						mob8.dame = num184;
						mob8.dameMp = num185;
						mob8.setAttack(global::Char.myCharz());
					}
				}
				break;
			}
			case 7:
			{
				GameCanvas.debug("SA87", 2);
				Mob mob8 = null;
				try
				{
					mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex34)
				{
				}
				GameCanvas.debug("SA87x1", 2);
				if (mob8 != null)
				{
					GameCanvas.debug("SA87x2", 2);
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char == null)
					{
						return;
					}
					GameCanvas.debug("SA87x3", 2);
					int num186 = msg.readInt3Byte();
					mob8.dame = @char.cHP - num186;
					@char.cHPNew = num186;
					GameCanvas.debug("SA87x4", 2);
					try
					{
						@char.cMP = msg.readInt3Byte();
					}
					catch (Exception ex35)
					{
					}
					GameCanvas.debug("SA87x5", 2);
					if (mob8.isBusyAttackSomeOne)
					{
						@char.doInjure(mob8.dame, 0, false, true);
					}
					else
					{
						mob8.setAttack(@char);
					}
					GameCanvas.debug("SA87x6", 2);
				}
				break;
			}
			case 8:
			{
				GameCanvas.debug("SA83", 2);
				Mob mob8 = null;
				try
				{
					mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex36)
				{
				}
				GameCanvas.debug("SA83v1", 2);
				if (mob8 != null)
				{
					mob8.hp = msg.readInt3Byte();
					mob8.updateHp_bar();
					int num187 = msg.readInt3Byte();
					if (num187 == 1)
					{
						return;
					}
					if (num187 > 1)
					{
						mob8.setInjure();
					}
					bool flag11 = false;
					try
					{
						flag11 = msg.reader().readBoolean();
					}
					catch (Exception ex37)
					{
					}
					sbyte b75 = msg.reader().readByte();
					if ((int)b75 != -1)
					{
						EffecMn.addEff(new Effect((int)b75, mob8.x, mob8.getY(), 3, 1, -1));
					}
					GameCanvas.debug("SA83v2", 2);
					if (flag11)
					{
						GameScr.startFlyText("-" + num187, mob8.x, mob8.getY() - mob8.getH(), 0, -2, mFont.FATAL);
					}
					else if (num187 == 0)
					{
						mob8.x = mob8.xFirst;
						mob8.y = mob8.yFirst;
						GameScr.startFlyText(mResources.miss, mob8.x, mob8.getY() - mob8.getH(), 0, -2, mFont.MISS);
					}
					else if (num187 > 1)
					{
						GameScr.startFlyText("-" + num187, mob8.x, mob8.getY() - mob8.getH(), 0, -2, mFont.ORANGE);
					}
				}
				GameCanvas.debug("SA83v3", 2);
				break;
			}
			case 9:
				GameCanvas.debug("SA89", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				@char.cPk = msg.reader().readByte();
				@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
				break;
			case 10:
			{
				GameCanvas.debug("SA80", 2);
				int num188 = msg.reader().readInt();
				for (int num189 = 0; num189 < GameScr.vCharInMap.size(); num189++)
				{
					global::Char char13 = null;
					try
					{
						char13 = (global::Char)GameScr.vCharInMap.elementAt(num189);
					}
					catch (Exception ex38)
					{
					}
					if (char13 == null)
					{
						break;
					}
					if (char13.charID == num188)
					{
						GameCanvas.debug("SA8x2y" + num189, 2);
						char13.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort(), 0);
						char13.lastUpdateTime = mSystem.currentTimeMillis();
						break;
					}
				}
				GameCanvas.debug("SA80x3", 2);
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA81", 2);
				int num188 = msg.reader().readInt();
				for (int num190 = 0; num190 < GameScr.vCharInMap.size(); num190++)
				{
					global::Char char14 = (global::Char)GameScr.vCharInMap.elementAt(num190);
					if (char14 != null && char14.charID == num188)
					{
						if (!char14.isInvisiblez && !char14.isUsePlane)
						{
							ServerEffect.addServerEffect(60, char14.cx, char14.cy, 1);
						}
						if (!char14.isUsePlane)
						{
							GameScr.vCharInMap.removeElementAt(num190);
						}
						return;
					}
				}
				break;
			}
			case 12:
			{
				GameCanvas.debug("SA79", 2);
				int charID = msg.reader().readInt();
				int num191 = msg.reader().readInt();
				global::Char char15;
				if (num191 != -100)
				{
					char15 = new global::Char();
					char15.charID = charID;
					char15.clanID = num191;
				}
				else
				{
					char15 = new Mabu();
					char15.charID = charID;
					char15.clanID = num191;
				}
				if (char15.clanID == -2)
				{
					char15.isCopy = true;
				}
				if (this.readCharInfo(char15, msg))
				{
					sbyte b76 = msg.reader().readByte();
					if (char15.cy <= 10 && (int)b76 != 0 && (int)b76 != 2)
					{
						Res.outz(string.Concat(new object[]
						{
							"nhân vật bay trên trời xuống x= ",
							char15.cx,
							" y= ",
							char15.cy
						}));
						Teleport teleport = new Teleport(char15.cx, char15.cy, char15.head, char15.cdir, 1, false, ((int)b76 != 1) ? ((int)b76) : char15.cgender);
						teleport.id = char15.charID;
						char15.isTeleport = true;
						Teleport.addTeleport(teleport);
					}
					if ((int)b76 == 2)
					{
						char15.show();
					}
					for (int num192 = 0; num192 < GameScr.vMob.size(); num192++)
					{
						Mob mob9 = (Mob)GameScr.vMob.elementAt(num192);
						if (mob9 != null && mob9.isMobMe && mob9.mobId == char15.charID)
						{
							Res.outz("co 1 con quai");
							char15.mobMe = mob9;
							char15.mobMe.x = char15.cx;
							char15.mobMe.y = char15.cy - 40;
							break;
						}
					}
					if (GameScr.findCharInMap(char15.charID) == null)
					{
						GameScr.vCharInMap.addElement(char15);
					}
					char15.isMonkey = msg.reader().readByte();
					short num193 = msg.reader().readShort();
					Res.outz("mount id= " + num193 + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					if (num193 != -1)
					{
						char15.isHaveMount = true;
						if (num193 == 346 || num193 == 347 || num193 == 348)
						{
							char15.isMountVip = false;
						}
						else if (num193 == 349 || num193 == 350 || num193 == 351)
						{
							char15.isMountVip = true;
						}
						else if (num193 == 396)
						{
							char15.isEventMount = true;
						}
						else if (num193 == 532)
						{
							char15.isSpeacialMount = true;
						}
						else if (num193 >= global::Char.ID_NEW_MOUNT)
						{
							char15.idMount = num193;
						}
					}
					else
					{
						char15.isHaveMount = false;
					}
				}
				sbyte b77 = msg.reader().readByte();
				Res.outz("addplayer:   " + b77);
				char15.cFlag = b77;
				char15.isNhapThe = ((int)msg.reader().readByte() == 1);
				try
				{
					char15.idAuraEff = msg.reader().readShort();
					char15.idEff_Set_Item = (short)msg.reader().readSByte();
					char15.idHat = msg.reader().readShort();
					if (char15.bag >= 201 && char15.bag < 255)
					{
						char15.addEffChar(new Effect(char15.bag, char15, 2, -1, 10, 1)
						{
							typeEff = 5
						});
					}
					else
					{
						for (int num194 = 0; num194 < 54; num194++)
						{
							char15.removeEffChar(0, 201 + num194);
						}
					}
				}
				catch (Exception ex39)
				{
					Res.outz("cmd: -5 err: " + ex39.StackTrace);
				}
				GameScr.gI().getFlagImage(char15.charID, char15.cFlag);
				break;
			}
			case 14:
			{
				GameCanvas.debug("SA78", 2);
				sbyte b78 = msg.reader().readByte();
				int num195 = msg.reader().readInt();
				if ((int)b78 == 0)
				{
					global::Char.myCharz().cPower += (long)num195;
				}
				if ((int)b78 == 1)
				{
					global::Char.myCharz().cTiemNang += (long)num195;
				}
				if ((int)b78 == 2)
				{
					global::Char.myCharz().cPower += (long)num195;
					global::Char.myCharz().cTiemNang += (long)num195;
				}
				global::Char.myCharz().applyCharLevelPercent();
				if ((int)global::Char.myCharz().cTypePk != 3)
				{
					GameScr.startFlyText(((num195 <= 0) ? string.Empty : "+") + num195, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -4, mFont.GREEN);
					if (num195 > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5002)
					{
						ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
						ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
					}
				}
				break;
			}
			case 15:
			{
				GameCanvas.debug("SA77", 22);
				int num196 = msg.reader().readInt();
				global::Char.myCharz().yen += num196;
				GameScr.startFlyText((num196 <= 0) ? (string.Empty + num196) : ("+" + num196), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			case 16:
			{
				GameCanvas.debug("SA77", 222);
				int num197 = msg.reader().readInt();
				global::Char.myCharz().xu += (long)num197;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().yen -= num197;
				GameScr.startFlyText("+" + num197, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			}
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception ex40)
		{
			Res.err(string.Concat(new object[]
			{
				"[Controller] [error] ",
				ex40.StackTrace,
				" msg: ",
				ex40.Message,
				" cause ",
				ex40.Data
			}));
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00048130 File Offset: 0x00046530
	private void readLogin(Message msg)
	{
		sbyte b = msg.reader().readByte();
		ChooseCharScr.playerData = new PlayerData[(int)b];
		Res.outz("[LEN] sl nguoi choi " + b);
		for (int i = 0; i < (int)b; i++)
		{
			int playerID = msg.reader().readInt();
			string name = msg.reader().readUTF();
			short head = msg.reader().readShort();
			short body = msg.reader().readShort();
			short leg = msg.reader().readShort();
			long ppoint = msg.reader().readLong();
			ChooseCharScr.playerData[i] = new PlayerData(playerID, name, head, body, leg, ppoint);
		}
		GameCanvas.chooseCharScr.switchToMe();
		GameCanvas.chooseCharScr.updateChooseCharacter((byte)b);
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x000481F8 File Offset: 0x000465F8
	private void createItem(myReader d)
	{
		GameScr.vcItem = d.readByte();
		ItemTemplates.itemTemplates.clear();
		GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
		for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
		{
			GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
			GameScr.gI().iOptionTemplates[i].id = i;
			GameScr.gI().iOptionTemplates[i].name = d.readUTF();
			GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
		}
		int num = (int)d.readShort();
		for (int j = 0; j < num; j++)
		{
			ItemTemplate it = new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBool());
			ItemTemplates.add(it);
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x000482FC File Offset: 0x000466FC
	private void createSkill(myReader d)
	{
		GameScr.vcSkill = d.readByte();
		GameScr.gI().sOptionTemplates = new SkillOptionTemplate[(int)d.readByte()];
		for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
		{
			GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
			GameScr.gI().sOptionTemplates[i].id = i;
			GameScr.gI().sOptionTemplates[i].name = d.readUTF();
		}
		GameScr.nClasss = new NClass[(int)d.readByte()];
		for (int j = 0; j < GameScr.nClasss.Length; j++)
		{
			GameScr.nClasss[j] = new NClass();
			GameScr.nClasss[j].classId = j;
			GameScr.nClasss[j].name = d.readUTF();
			GameScr.nClasss[j].skillTemplates = new SkillTemplate[(int)d.readByte()];
			for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
			{
				GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
				GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates[k].maxPoint = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].manaUseType = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].type = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].iconId = (int)d.readShort();
				GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
				int lineWidth = 130;
				if (GameCanvas.w == 128 || GameCanvas.h <= 208)
				{
					lineWidth = 100;
				}
				GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), lineWidth);
				GameScr.nClasss[j].skillTemplates[k].skills = new Skill[(int)d.readByte()];
				for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
				{
					GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
					GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
					GameScr.nClasss[j].skillTemplates[k].skills[l].point = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
					GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dx = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dy = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
					Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
				}
			}
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00048734 File Offset: 0x00046B34
	private void createMap(myReader d)
	{
		GameScr.vcMap = d.readByte();
		TileMap.mapNames = new string[(int)d.readUnsignedByte()];
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			TileMap.mapNames[i] = d.readUTF();
		}
		Npc.arrNpcTemplate = new NpcTemplate[(int)d.readByte()];
		sbyte b = 0;
		while ((int)b < Npc.arrNpcTemplate.Length)
		{
			Npc.arrNpcTemplate[(int)b] = new NpcTemplate();
			Npc.arrNpcTemplate[(int)b].npcTemplateId = (int)b;
			Npc.arrNpcTemplate[(int)b].name = d.readUTF();
			Npc.arrNpcTemplate[(int)b].headId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].bodyId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].legId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].menu = new string[(int)d.readByte()][];
			for (int j = 0; j < Npc.arrNpcTemplate[(int)b].menu.Length; j++)
			{
				Npc.arrNpcTemplate[(int)b].menu[j] = new string[(int)d.readByte()];
				for (int k = 0; k < Npc.arrNpcTemplate[(int)b].menu[j].Length; k++)
				{
					Npc.arrNpcTemplate[(int)b].menu[j][k] = d.readUTF();
				}
			}
			b = (sbyte)((int)b + 1);
		}
		Mob.arrMobTemplate = new MobTemplate[(int)d.readByte()];
		sbyte b2 = 0;
		while ((int)b2 < Mob.arrMobTemplate.Length)
		{
			Mob.arrMobTemplate[(int)b2] = new MobTemplate();
			Mob.arrMobTemplate[(int)b2].mobTemplateId = b2;
			Mob.arrMobTemplate[(int)b2].type = d.readByte();
			Mob.arrMobTemplate[(int)b2].name = d.readUTF();
			Mob.arrMobTemplate[(int)b2].hp = d.readInt();
			Mob.arrMobTemplate[(int)b2].rangeMove = d.readByte();
			Mob.arrMobTemplate[(int)b2].speed = d.readByte();
			Mob.arrMobTemplate[(int)b2].dartType = d.readByte();
			b2 = (sbyte)((int)b2 + 1);
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00048968 File Offset: 0x00046D68
	private void createData(myReader d, bool isSaveRMS)
	{
		GameScr.vcData = d.readByte();
		if (isSaveRMS)
		{
			Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
			Rms.DeleteStorage("NRdata");
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x000489F0 File Offset: 0x00046DF0
	private Image createImage(sbyte[] arr)
	{
		try
		{
			return Image.createImage(arr, 0, arr.Length);
		}
		catch (Exception ex)
		{
		}
		return null;
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00048A28 File Offset: 0x00046E28
	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = (int)b[i];
			if (num < 0)
			{
				num += 256;
			}
			array[i] = num;
		}
		return array;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00048A6C File Offset: 0x00046E6C
	public void readClanMsg(Message msg, int index)
	{
		try
		{
			ClanMessage clanMessage = new ClanMessage();
			sbyte b = msg.reader().readByte();
			clanMessage.type = (int)b;
			clanMessage.id = msg.reader().readInt();
			clanMessage.playerId = msg.reader().readInt();
			clanMessage.playerName = msg.reader().readUTF();
			clanMessage.role = msg.reader().readByte();
			clanMessage.time = (long)(msg.reader().readInt() + 1000000000);
			bool flag = false;
			GameScr.isNewClanMessage = false;
			if ((int)b == 0)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				if (mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60)
				{
					clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
				}
				else
				{
					clanMessage.chat = new string[1];
					clanMessage.chat[0] = text;
				}
				clanMessage.color = msg.reader().readByte();
			}
			else if ((int)b == 1)
			{
				clanMessage.recieve = (int)msg.reader().readByte();
				clanMessage.maxCap = (int)msg.reader().readByte();
				flag = ((int)msg.reader().readByte() == 1);
				if (flag)
				{
					GameScr.isNewClanMessage = true;
				}
				if (clanMessage.playerId != global::Char.myCharz().charID)
				{
					if (clanMessage.recieve < clanMessage.maxCap)
					{
						clanMessage.option = new string[]
						{
							mResources.donate
						};
					}
					else
					{
						clanMessage.option = null;
					}
				}
				if (GameCanvas.panel.cp != null)
				{
					GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
				}
			}
			else if ((int)b == 2 && (int)global::Char.myCharz().role == 0)
			{
				GameScr.isNewClanMessage = true;
				clanMessage.option = new string[]
				{
					mResources.CANCEL,
					mResources.receive
				};
			}
			if (GameCanvas.currentScreen != GameScr.instance)
			{
				GameScr.isNewClanMessage = false;
			}
			else if (GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3)
			{
				GameScr.isNewClanMessage = false;
			}
			ClanMessage.addMessage(clanMessage, index, flag);
		}
		catch (Exception ex)
		{
			Cout.println("LOI TAI CMD -= " + msg.command);
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00048D08 File Offset: 0x00047108
	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("[CONTROLER] start load map " + teleport3);
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		if (!GameScr.isLoadAllData && !CreateCharScr.isCreateChar)
		{
			GameScr.gI().initSelectChar();
		}
		GameScr.loadCamera(false, ((int)teleport3 != 1) ? -1 : global::Char.myCharz().cx, ((int)teleport3 != 0) ? 0 : -1);
		TileMap.loadMainTile();
		TileMap.loadMap(TileMap.tileID);
		Res.outz("LOAD GAMESCR 2");
		global::Char.myCharz().cvx = 0;
		global::Char.myCharz().statusMe = 4;
		global::Char.myCharz().currentMovePoint = null;
		global::Char.myCharz().mobFocus = null;
		global::Char.myCharz().charFocus = null;
		global::Char.myCharz().npcFocus = null;
		global::Char.myCharz().itemFocus = null;
		global::Char.myCharz().skillPaint = null;
		global::Char.myCharz().setMabuHold(false);
		global::Char.myCharz().skillPaintRandomPaint = null;
		GameCanvas.clearAllPointerEvent();
		if (global::Char.myCharz().cy >= TileMap.pxh - 100)
		{
			global::Char.myCharz().isFlyUp = true;
			global::Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		global::Char.isLockKey = false;
		Res.outz("cy= " + global::Char.myCharz().cy + "---------------------------------------------");
		for (int i = 0; i < global::Char.myCharz().vEff.size(); i++)
		{
			EffectChar effectChar = (EffectChar)global::Char.myCharz().vEff.elementAt(i);
			if ((int)effectChar.template.type == 10)
			{
				global::Char.isLockKey = true;
				break;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		GameScr.gI().dHP = global::Char.myCharz().cHP;
		GameScr.gI().dMP = global::Char.myCharz().cMP;
		global::Char.ischangingMap = false;
		GameScr.gI().switchToMe();
		if (global::Char.myCharz().cy <= 10 && (int)teleport3 != 0 && (int)teleport3 != 2)
		{
			Teleport p = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 1, true, ((int)teleport3 != 1) ? ((int)teleport3) : global::Char.myCharz().cgender);
			Teleport.addTeleport(p);
			global::Char.myCharz().isTeleport = true;
		}
		if ((int)teleport3 == 2)
		{
			global::Char.myCharz().show();
		}
		if (GameScr.gI().isRongThanXuatHien)
		{
			if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
			{
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			}
			if (mGraphics.zoomLevel > 1)
			{
				GameScr.gI().doiMauTroi();
			}
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID, 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
		Res.outz("[CONTROLLER] loadMap DONE!!!!!!!!!");
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x000490B0 File Offset: 0x000474B0
	public void loadInfoMap(Message msg)
	{
		try
		{
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			global::Char.myCharz().cx = (global::Char.myCharz().cxSend = (global::Char.myCharz().cxFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().cy = (global::Char.myCharz().cySend = (global::Char.myCharz().cyFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().xSd = global::Char.myCharz().cx;
			global::Char.myCharz().ySd = global::Char.myCharz().cy;
			Res.outz(string.Concat(new object[]
			{
				"head= ",
				global::Char.myCharz().head,
				" body= ",
				global::Char.myCharz().body,
				" left= ",
				global::Char.myCharz().leg,
				" x= ",
				global::Char.myCharz().cx,
				" y= ",
				global::Char.myCharz().cy,
				" chung toc= ",
				global::Char.myCharz().cgender
			}));
			if (global::Char.myCharz().cx >= 0 && global::Char.myCharz().cx <= 100)
			{
				global::Char.myCharz().cdir = 1;
			}
			else if (global::Char.myCharz().cx >= TileMap.tmw - 100 && global::Char.myCharz().cx <= TileMap.tmw)
			{
				global::Char.myCharz().cdir = -1;
			}
			GameCanvas.debug("SA75x4", 2);
			int num = (int)msg.reader().readByte();
			Res.outz("vGo size= " + num);
			if (!GameScr.info1.isDone)
			{
				GameScr.info1.cmx = global::Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = global::Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && waypoint.minX >= 0 && waypoint.minX <= 24)
				{
				}
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			GameCanvas.debug("SA75x5", 2);
			num = (int)msg.reader().readByte();
			Mob.newMob.removeAllElements();
			sbyte b = 0;
			while ((int)b < num)
			{
				Mob mob = new Mob((int)b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), (int)msg.reader().readByte(), (int)msg.reader().readByte(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				mob.xSd = mob.x;
				mob.ySd = mob.y;
				mob.isBoss = msg.reader().readBoolean();
				if ((int)Mob.arrMobTemplate[mob.templateId].type != 0)
				{
					if ((int)b % 3 == 0)
					{
						mob.dir = -1;
					}
					else
					{
						mob.dir = 1;
					}
					mob.x += 10 - (int)b % 20;
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				NewBoss newBoss = null;
				if (mob.templateId == 70)
				{
					bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 71)
				{
					bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 72)
				{
					bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				}
				if (mob.isBoss)
				{
					newBoss = new NewBoss((int)b, (short)mob.x, (short)mob.y, mob.templateId, mob.hp, mob.maxHp, mob.sys);
				}
				if (newBoss != null)
				{
					GameScr.vMob.addElement(newBoss);
				}
				else if (bigBoss != null)
				{
					GameScr.vMob.addElement(bigBoss);
				}
				else if (bachTuoc != null)
				{
					GameScr.vMob.addElement(bachTuoc);
				}
				else if (bigBoss2 != null)
				{
					GameScr.vMob.addElement(bigBoss2);
				}
				else
				{
					GameScr.vMob.addElement(mob);
				}
				b = (sbyte)((int)b + 1);
			}
			if (global::Char.myCharz().mobMe != null && GameScr.findMobInMap(global::Char.myCharz().mobMe.mobId) == null)
			{
				global::Char.myCharz().mobMe.getData();
				global::Char.myCharz().mobMe.x = global::Char.myCharz().cx;
				global::Char.myCharz().mobMe.y = global::Char.myCharz().cy - 40;
				GameScr.vMob.addElement(global::Char.myCharz().mobMe);
			}
			num = (int)msg.reader().readByte();
			byte b2 = 0;
			while ((int)b2 < num)
			{
				b2 += 1;
			}
			GameCanvas.debug("SA75x6", 2);
			num = (int)msg.reader().readByte();
			Res.outz("NPC size= " + num);
			for (int j = 0; j < num; j++)
			{
				sbyte b3 = msg.reader().readByte();
				short cx = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				if ((int)b4 != 6)
				{
					if ((global::Char.myCharz().taskMaint.taskId >= 7 && (global::Char.myCharz().taskMaint.taskId != 7 || global::Char.myCharz().taskMaint.index > 1)) || ((int)b4 != 7 && (int)b4 != 8 && (int)b4 != 9))
					{
						if (global::Char.myCharz().taskMaint.taskId >= 6 || (int)b4 != 16)
						{
							if ((int)b4 == 4)
							{
								GameScr.gI().magicTree = new MagicTree(j, (int)b3, (int)cx, (int)num2, (int)b4, (int)num3);
								Service.gI().magicTree(2);
								GameScr.vNpc.addElement(GameScr.gI().magicTree);
							}
							else
							{
								Npc o = new Npc(j, (int)b3, (int)cx, (int)(num2 + 3), (int)b4, (int)num3);
								GameScr.vNpc.addElement(o);
							}
						}
					}
				}
			}
			GameCanvas.debug("SA75x7", 2);
			num = (int)msg.reader().readByte();
			string text = string.Empty;
			Res.outz("item size = " + num);
			text = text + "item: " + num;
			for (int k = 0; k < num; k++)
			{
				short itemMapID = msg.reader().readShort();
				short num4 = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num5 = msg.reader().readInt();
				short r = 0;
				if (num5 == -2)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap = new ItemMap(num5, itemMapID, num4, x, y, r);
				bool flag = false;
				for (int l = 0; l < GameScr.vItemMap.size(); l++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
					if (itemMap2.itemMapID == itemMap.itemMapID)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GameScr.vItemMap.addElement(itemMap);
				}
				text = text + num4 + ",";
			}
			Res.err("sl item on map " + text + "\n");
			TileMap.vCurrItem.removeAllElements();
			if (mGraphics.zoomLevel == 1)
			{
				BgItem.clearHashTable();
			}
			BgItem.vKeysNew.removeAllElements();
			if (!GameCanvas.lowGraphic || (GameCanvas.lowGraphic && TileMap.isVoDaiMap()) || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 47 || TileMap.mapID == 48)
			{
				short num6 = msg.reader().readShort();
				text = "item high graphic: ";
				for (int m = 0; m < (int)num6; m++)
				{
					short num7 = msg.reader().readShort();
					short num8 = msg.reader().readShort();
					short num9 = msg.reader().readShort();
					if (TileMap.getBIById((int)num7) != null)
					{
						BgItem bibyId = TileMap.getBIById((int)num7);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)num7;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)num8 * (int)TileMap.size;
						bgItem.y = (int)num9 * (int)TileMap.size;
						bgItem.layer = bibyId.layer;
						if (TileMap.isExistMoreOne(bgItem.id))
						{
							bgItem.trans = ((m % 2 != 0) ? 2 : 0);
							if (TileMap.mapID == 45)
							{
								bgItem.trans = 0;
							}
						}
						if (!BgItem.imgNew.containsKey(bgItem.idImage + string.Empty))
						{
							if (mGraphics.zoomLevel == 1)
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image == null)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
							}
							else
							{
								bool flag2 = false;
								sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "bgItem" + bgItem.idImage);
								if (array != null)
								{
									if (BgItem.newSmallVersion != null)
									{
										Res.outz(string.Concat(new object[]
										{
											"Small  last= ",
											array.Length % 127,
											"new Version= ",
											BgItem.newSmallVersion[(int)bgItem.idImage]
										}));
										if (array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage])
										{
											flag2 = true;
										}
									}
									if (!flag2)
									{
										Image image = Image.createImage(array, 0, array.Length);
										if (image != null)
										{
											BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
										}
										else
										{
											flag2 = true;
										}
									}
								}
								else
								{
									flag2 = true;
								}
								if (flag2)
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
									if (image == null)
									{
										image = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
								}
							}
							BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
						}
						if (!BgItem.isExistKeyNews(bgItem.idImage + string.Empty))
						{
							BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
						}
						bgItem.changeColor();
						TileMap.vCurrItem.addElement(bgItem);
					}
					text = text + num7 + ",";
				}
				Res.err("item High Graphics: " + text);
				for (int n = 0; n < BgItem.vKeysLast.size(); n++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(n);
					if (!BgItem.isExistKeyNews(text2))
					{
						BgItem.imgNew.remove(text2);
						if (BgItem.imgNew.containsKey(text2 + "blend" + 1))
						{
							BgItem.imgNew.remove(text2 + "blend" + 1);
						}
						if (BgItem.imgNew.containsKey(text2 + "blend" + 3))
						{
							BgItem.imgNew.remove(text2 + "blend" + 3);
						}
						BgItem.vKeysLast.removeElementAt(n);
						n--;
					}
				}
				BackgroudEffect.isFog = false;
				BackgroudEffect.nCloud = 0;
				EffecMn.vEff.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				Effect.newEff.removeAllElements();
				short num10 = msg.reader().readShort();
				for (int num11 = 0; num11 < (int)num10; num11++)
				{
					string key = msg.reader().readUTF();
					string value = msg.reader().readUTF();
					this.keyValueAction(key, value);
				}
			}
			else
			{
				short num12 = msg.reader().readShort();
				for (int num13 = 0; num13 < (int)num12; num13++)
				{
					short num14 = msg.reader().readShort();
					short num15 = msg.reader().readShort();
					short num16 = msg.reader().readShort();
				}
				short num17 = msg.reader().readShort();
				for (int num18 = 0; num18 < (int)num17; num18++)
				{
					string text3 = msg.reader().readUTF();
					string text4 = msg.reader().readUTF();
				}
			}
			TileMap.bgType = (int)msg.reader().readByte();
			sbyte teleport = msg.reader().readByte();
			this.loadCurrMap(teleport);
			global::Char.isLoadingMap = false;
			GameCanvas.debug("SA75x8", 2);
			Resources.UnloadUnusedAssets();
			GC.Collect();
			Res.outz("[ LOAD INFO MAP ]    [DONE]   in game");
		}
		catch (Exception ex)
		{
			Res.err("[error] [TAI LOADMAP INFO]" + ex.StackTrace + ex.Message);
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0004A06C File Offset: 0x0004846C
	public void keyValueAction(string key, string value)
	{
		if (key.Equals("eff"))
		{
			if (Panel.graphics > 0)
			{
				return;
			}
			string[] array = Res.split(value, ".", 0);
			int id = int.Parse(array[0]);
			int layer = int.Parse(array[1]);
			int x = int.Parse(array[2]);
			int y = int.Parse(array[3]);
			int loop;
			int loopCount;
			if (array.Length <= 4)
			{
				loop = -1;
				loopCount = 1;
			}
			else
			{
				loop = int.Parse(array[4]);
				loopCount = int.Parse(array[5]);
			}
			Effect effect = new Effect(id, x, y, layer, loop, loopCount);
			if (array.Length > 6)
			{
				effect.typeEff = int.Parse(array[6]);
				if (array.Length > 7)
				{
					effect.indexFrom = int.Parse(array[7]);
					effect.indexTo = int.Parse(array[8]);
				}
			}
			EffecMn.addEff(effect);
		}
		else if (key.Equals("beff"))
		{
			if (Panel.graphics > 1)
			{
				return;
			}
			BackgroudEffect.addEffect(int.Parse(value));
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0004A174 File Offset: 0x00048574
	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotMap : " + b);
			switch (b)
			{
			case 4:
			{
				GameCanvas.debug("SA8", 2);
				GameCanvas.loginScr.savePass();
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				LoginScr.isUpdateAll = true;
				LoginScr.isUpdateData = true;
				LoginScr.isUpdateMap = true;
				LoginScr.isUpdateSkill = true;
				LoginScr.isUpdateItem = true;
				GameScr.vsData = msg.reader().readByte();
				GameScr.vsMap = msg.reader().readByte();
				GameScr.vsSkill = msg.reader().readByte();
				GameScr.vsItem = msg.reader().readByte();
				sbyte b2 = msg.reader().readByte();
				if (GameCanvas.loginScr.isLogin2)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
				{
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				}
				if ((int)GameScr.vsData != (int)GameScr.vcData)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateData();
				}
				else
				{
					try
					{
						LoginScr.isUpdateData = false;
					}
					catch (Exception ex)
					{
						GameScr.vcData = -1;
						Service.gI().updateData();
					}
				}
				if ((int)GameScr.vsMap != (int)GameScr.vcMap)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NRmap"));
							this.createMap(dataInputStream.r);
						}
						LoginScr.isUpdateMap = false;
					}
					catch (Exception ex2)
					{
						GameScr.vcMap = -1;
						Service.gI().updateMap();
					}
				}
				if ((int)GameScr.vsSkill != (int)GameScr.vcSkill)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							DataInputStream dataInputStream2 = new DataInputStream(Rms.loadRMS("NRskill"));
							this.createSkill(dataInputStream2.r);
						}
						LoginScr.isUpdateSkill = false;
					}
					catch (Exception ex3)
					{
						GameScr.vcSkill = -1;
						Service.gI().updateSkill();
					}
				}
				if ((int)GameScr.vsItem != (int)GameScr.vcItem)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateItem();
				}
				else
				{
					try
					{
						DataInputStream dataInputStream3 = new DataInputStream(Rms.loadRMS("NRitem0"));
						this.loadItemNew(dataInputStream3.r, 0, false);
						DataInputStream dataInputStream4 = new DataInputStream(Rms.loadRMS("NRitem1"));
						this.loadItemNew(dataInputStream4.r, 1, false);
						DataInputStream dataInputStream5 = new DataInputStream(Rms.loadRMS("NRitem2"));
						this.loadItemNew(dataInputStream5.r, 2, false);
						DataInputStream dataInputStream6 = new DataInputStream(Rms.loadRMS("NRitem100"));
						this.loadItemNew(dataInputStream6.r, 100, false);
						LoginScr.isUpdateItem = false;
					}
					catch (Exception ex4)
					{
						GameScr.vcItem = -1;
						Service.gI().updateItem();
					}
				}
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					if (!GameScr.isLoadAllData)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
					}
					Service.gI().clientOk();
				}
				sbyte b3 = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b3);
				GameScr.exps = new long[(int)b3];
				for (int i = 0; i < GameScr.exps.Length; i++)
				{
					GameScr.exps[i] = msg.reader().readLong();
				}
				break;
			}
			default:
				switch (b)
				{
				case 35:
					GameCanvas.endDlg();
					GameScr.gI().resetButton();
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					break;
				case 36:
					GameScr.typeActive = msg.reader().readByte();
					Res.outz("load Me Active: " + GameScr.typeActive);
					break;
				}
				break;
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createMap(msg.reader());
				msg.reader().reset();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data);
				Rms.saveRMS("NRmap", data);
				sbyte[] data2 = new sbyte[]
				{
					GameScr.vcMap
				};
				Rms.saveRMS("NRmapVersion", data2);
				LoginScr.isUpdateMap = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createSkill(msg.reader());
				msg.reader().reset();
				sbyte[] data3 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data3);
				Rms.saveRMS("NRskill", data3);
				sbyte[] data4 = new sbyte[]
				{
					GameScr.vcSkill
				};
				Rms.saveRMS("NRskillVersion", data4);
				LoginScr.isUpdateSkill = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 8:
				Res.outz("GET UPDATE_ITEM " + msg.reader().available() + " bytes");
				this.createItemNew(msg.reader());
				break;
			case 9:
				GameCanvas.debug("SA11", 2);
				break;
			case 10:
				try
				{
					global::Char.isLoadingMap = true;
					Res.outz("REQUEST MAP TEMPLATE");
					GameCanvas.isLoading = true;
					TileMap.maps = null;
					TileMap.types = null;
					mSystem.gcc();
					GameCanvas.debug("SA99", 2);
					TileMap.tmw = (int)msg.reader().readByte();
					TileMap.tmh = (int)msg.reader().readByte();
					TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
					Res.err("   M apsize= " + TileMap.tmw * TileMap.tmh);
					for (int j = 0; j < TileMap.maps.Length; j++)
					{
						int num = (int)msg.reader().readByte();
						if (num < 0)
						{
							num += 256;
						}
						TileMap.maps[j] = (int)((ushort)num);
					}
					TileMap.types = new int[TileMap.maps.Length];
					msg = this.messWait;
					this.loadInfoMap(msg);
					try
					{
						sbyte b4 = msg.reader().readByte();
						TileMap.isMapDouble = ((int)b4 != 0);
					}
					catch (Exception ex5)
					{
						Res.err(" 1 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex5.ToString());
					}
				}
				catch (Exception ex6)
				{
					Res.err("2 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex6.ToString());
				}
				msg.cleanup();
				this.messWait.cleanup();
				msg = (this.messWait = null);
				GameScr.gI().switchToMe();
				break;
			case 12:
				GameCanvas.debug("SA10", 2);
				break;
			case 16:
				MoneyCharge.gI().switchToMe();
				break;
			case 17:
				GameCanvas.debug("SYB123", 2);
				global::Char.myCharz().clearTask();
				break;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num2 = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
				break;
			}
			case 20:
				global::Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + global::Char.myCharz().cPk, 0);
				break;
			}
		}
		catch (Exception ex7)
		{
			Cout.LogError("LOI TAI messageNotMap + " + msg.command);
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0004ABD8 File Offset: 0x00048FD8
	public void messageNotLogin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotLogin : " + b);
			if (b == 2)
			{
				string linkDefault = msg.reader().readUTF();
				if (Rms.loadRMSInt("AdminLink") != 1)
				{
					if (mSystem.clientType == 1)
					{
						ServerListScreen.linkDefault = linkDefault;
					}
					else
					{
						ServerListScreen.linkDefault = linkDefault;
					}
					mSystem.AddIpTest();
					ServerListScreen.getServerList(ServerListScreen.linkDefault);
					try
					{
						sbyte b2 = msg.reader().readByte();
						Panel.CanNapTien = ((int)b2 == 1);
						sbyte b3 = msg.reader().readByte();
						Rms.saveRMSInt("AdminLink", (int)b3);
					}
					catch (Exception ex)
					{
					}
				}
			}
		}
		catch (Exception ex2)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0004ACE8 File Offset: 0x000490E8
	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			sbyte b = msg.reader().readByte();
			Res.outz("---messageSubCommand : " + b);
			switch (b)
			{
			case 0:
			{
				GameCanvas.debug("SA21", 2);
				RadarScr.list = new MyVector();
				Teleport.vTeleport.removeAllElements();
				GameScr.vCharInMap.removeAllElements();
				GameScr.vItemMap.removeAllElements();
				global::Char.vItemTime.removeAllElements();
				GameScr.loadImg();
				GameScr.currentCharViewInfo = global::Char.myCharz();
				global::Char.myCharz().charID = msg.reader().readInt();
				global::Char.myCharz().ctaskId = (int)msg.reader().readByte();
				global::Char.myCharz().cgender = (int)msg.reader().readByte();
				global::Char.myCharz().head = (int)msg.reader().readShort();
				global::Char.myCharz().cName = msg.reader().readUTF();
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().cTypePk = msg.reader().readByte();
				global::Char.myCharz().cPower = msg.reader().readLong();
				global::Char.myCharz().applyCharLevelPercent();
				global::Char.myCharz().eff5BuffHp = (int)msg.reader().readShort();
				global::Char.myCharz().eff5BuffMp = (int)msg.reader().readShort();
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				sbyte b2 = msg.reader().readByte();
				sbyte b3 = 0;
				while ((int)b3 < (int)b2)
				{
					Skill skill = Skills.get(msg.reader().readShort());
					this.useSkill(skill);
					b3 = (sbyte)((int)b3 + 1);
				}
				GameScr.gI().sortSkill();
				GameScr.gI().loadSkillShortcut();
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				global::Char.myCharz().arrItemBody = new Item[(int)msg.reader().readByte()];
				try
				{
					global::Char.myCharz().setDefaultPart();
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						short num = msg.reader().readShort();
						if (num != -1)
						{
							ItemTemplate itemTemplate = ItemTemplates.get(num);
							int num2 = (int)itemTemplate.type;
							global::Char.myCharz().arrItemBody[i] = new Item();
							global::Char.myCharz().arrItemBody[i].template = itemTemplate;
							global::Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
							int num3 = (int)msg.reader().readUnsignedByte();
							if (num3 != 0)
							{
								global::Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num3];
								for (int j = 0; j < global::Char.myCharz().arrItemBody[i].itemOption.Length; j++)
								{
									int num4 = (int)msg.reader().readUnsignedByte();
									int param = (int)msg.reader().readUnsignedShort();
									if (num4 != -1)
									{
										global::Char.myCharz().arrItemBody[i].itemOption[j] = new ItemOption(num4, param);
									}
								}
							}
							if (num2 == 0)
							{
								Res.outz("toi day =======================================" + global::Char.myCharz().body);
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[i].template.part;
							}
							else if (num2 == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[i].template.part;
								Res.outz("toi day =======================================" + global::Char.myCharz().leg);
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
				global::Char.myCharz().arrItemBag = new Item[(int)msg.reader().readByte()];
				GameScr.hpPotion = 0;
				for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
				{
					short num5 = msg.reader().readShort();
					if (num5 != -1)
					{
						global::Char.myCharz().arrItemBag[k] = new Item();
						global::Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num5);
						global::Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].indexUI = k;
						sbyte b4 = msg.reader().readByte();
						if ((int)b4 != 0)
						{
							global::Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b4];
							for (int l = 0; l < global::Char.myCharz().arrItemBag[k].itemOption.Length; l++)
							{
								int num6 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								if (num6 != -1)
								{
									global::Char.myCharz().arrItemBag[k].itemOption[l] = new ItemOption(num6, param2);
									global::Char.myCharz().arrItemBag[k].getCompare();
								}
							}
						}
						if ((int)global::Char.myCharz().arrItemBag[k].template.type == 6)
						{
							GameScr.hpPotion += global::Char.myCharz().arrItemBag[k].quantity;
						}
					}
				}
				global::Char.myCharz().arrItemBox = new Item[(int)msg.reader().readByte()];
				GameCanvas.panel.hasUse = 0;
				for (int m = 0; m < global::Char.myCharz().arrItemBox.Length; m++)
				{
					short num7 = msg.reader().readShort();
					if (num7 != -1)
					{
						global::Char.myCharz().arrItemBox[m] = new Item();
						global::Char.myCharz().arrItemBox[m].template = ItemTemplates.get(num7);
						global::Char.myCharz().arrItemBox[m].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBox[m].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].itemOption = new ItemOption[(int)msg.reader().readByte()];
						for (int n = 0; n < global::Char.myCharz().arrItemBox[m].itemOption.Length; n++)
						{
							int num8 = (int)msg.reader().readUnsignedByte();
							int param3 = (int)msg.reader().readUnsignedShort();
							if (num8 != -1)
							{
								global::Char.myCharz().arrItemBox[m].itemOption[n] = new ItemOption(num8, param3);
								global::Char.myCharz().arrItemBox[m].getCompare();
							}
						}
						GameCanvas.panel.hasUse++;
					}
				}
				global::Char.myCharz().statusMe = 4;
				int num9 = Rms.loadRMSInt(global::Char.myCharz().cName + "vci");
				if (num9 < 1)
				{
					GameScr.isViewClanInvite = false;
				}
				else
				{
					GameScr.isViewClanInvite = true;
				}
				short num10 = msg.reader().readShort();
				global::Char.idHead = new short[(int)num10];
				global::Char.idAvatar = new short[(int)num10];
				for (int num11 = 0; num11 < (int)num10; num11++)
				{
					global::Char.idHead[num11] = msg.reader().readShort();
					global::Char.idAvatar[num11] = msg.reader().readShort();
				}
				for (int num12 = 0; num12 < GameScr.info1.charId.Length; num12++)
				{
					GameScr.info1.charId[num12] = new int[3];
				}
				GameScr.info1.charId[global::Char.myCharz().cgender][0] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][1] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][2] = (int)msg.reader().readShort();
				global::Char.myCharz().isNhapThe = ((int)msg.reader().readByte() == 1);
				Res.outz("NHAP THE= " + global::Char.myCharz().isNhapThe);
				GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
				GameScr.isNewMember = msg.reader().readByte();
				Service.gI().updateCaption((sbyte)global::Char.myCharz().cgender);
				Service.gI().androidPack();
				try
				{
					global::Char.myCharz().idAuraEff = msg.reader().readShort();
					global::Char.myCharz().idEff_Set_Item = (short)msg.reader().readSByte();
					global::Char.myCharz().idHat = msg.reader().readShort();
				}
				catch (Exception ex2)
				{
				}
				break;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				global::Char.myCharz().myskill = null;
				break;
			case 2:
			{
				GameCanvas.debug("SA14", 2);
				if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
				{
					global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
					global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
					Cout.LogError2(" ME_LOAD_SKILL");
				}
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				sbyte b2 = msg.reader().readByte();
				sbyte b5 = 0;
				while ((int)b5 < (int)b2)
				{
					short skillId = msg.reader().readShort();
					Skill skill2 = Skills.get(skillId);
					this.useSkill(skill2);
					b5 = (sbyte)((int)b5 + 1);
				}
				GameScr.gI().sortSkill();
				if (GameScr.isPaintInfoMe)
				{
					GameScr.indexRow = -1;
					GameScr.gI().left = (GameScr.gI().center = null);
				}
				break;
			}
			default:
				switch (b)
				{
				case 61:
				{
					string text = msg.reader().readUTF();
					sbyte[] array = new sbyte[msg.reader().readInt()];
					msg.reader().read(ref array);
					if (array.Length == 0)
					{
						array = null;
					}
					if (text.Equals("KSkill"))
					{
						GameScr.gI().onKSkill(array);
					}
					else if (text.Equals("OSkill"))
					{
						GameScr.gI().onOSkill(array);
					}
					else if (text.Equals("CSkill"))
					{
						GameScr.gI().onCSkill(array);
					}
					break;
				}
				case 62:
					Res.outz("ME UPDATE SKILL");
					this.read_UpdateSkill(msg);
					break;
				case 63:
				{
					sbyte b6 = msg.reader().readByte();
					if ((int)b6 > 0)
					{
						GameCanvas.panel.vPlayerMenu_id.removeAllElements();
						InfoDlg.showWait();
						MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
						for (int num13 = 0; num13 < (int)b6; num13++)
						{
							string caption = msg.reader().readUTF();
							string caption2 = msg.reader().readUTF();
							short num14 = msg.reader().readShort();
							GameCanvas.panel.vPlayerMenu_id.addElement(num14 + string.Empty);
							global::Char.myCharz().charFocus.menuSelect = (int)num14;
							vPlayerMenu.addElement(new Command(caption, 11115, global::Char.myCharz().charFocus)
							{
								caption2 = caption2
							});
						}
						InfoDlg.hide();
						GameCanvas.panel.setTabPlayerMenu();
					}
					break;
				}
				}
				break;
			case 4:
				GameCanvas.debug("SA23", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				break;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				int cHP = global::Char.myCharz().cHP;
				global::Char.myCharz().cHP = msg.readInt3Byte();
				if (global::Char.myCharz().cHP > cHP && (int)global::Char.myCharz().cTypePk != 4)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"+",
						global::Char.myCharz().cHP - cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5003)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
					}
				}
				if (global::Char.myCharz().cHP < cHP)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"-",
						cHP - global::Char.myCharz().cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
				}
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				if (!GameScr.isPaintInfoMe)
				{
				}
				break;
			}
			case 6:
				GameCanvas.debug("SA25", 2);
				if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
				{
					int cMP = global::Char.myCharz().cMP;
					global::Char.myCharz().cMP = msg.readInt3Byte();
					if (global::Char.myCharz().cMP > cMP)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"+",
							global::Char.myCharz().cMP - cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
						SoundMn.gI().HP_MPup();
						if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5001)
						{
							MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
						}
					}
					if (global::Char.myCharz().cMP < cMP)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"-",
							cMP - global::Char.myCharz().cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
					}
					Res.outz("curr MP= " + global::Char.myCharz().cMP);
					GameScr.gI().dMP = global::Char.myCharz().cMP;
					if (!GameScr.isPaintInfoMe)
					{
					}
				}
				break;
			case 7:
			{
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.clanID = msg.reader().readInt();
					if (@char.clanID == -2)
					{
						@char.isCopy = true;
					}
					this.readCharInfo(@char, msg);
					try
					{
						@char.idAuraEff = msg.reader().readShort();
						@char.idEff_Set_Item = (short)msg.reader().readSByte();
						@char.idHat = msg.reader().readShort();
						if (@char.bag >= 201)
						{
							@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
							{
								typeEff = 5
							});
						}
						else
						{
							@char.removeEffChar(0, 201);
						}
					}
					catch (Exception ex3)
					{
					}
				}
				break;
			}
			case 8:
			{
				GameCanvas.debug("SA26", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cspeed = (int)msg.reader().readByte();
				}
				break;
			}
			case 9:
			{
				GameCanvas.debug("SA27", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
				}
				break;
			}
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
					@char.wp = (int)msg.reader().readShort();
					if (@char.wp == -1)
					{
						@char.setDefaultWeapon();
					}
				}
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
					@char.body = (int)msg.reader().readShort();
					if (@char.body == -1)
					{
						@char.setDefaultBody();
					}
				}
				break;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
					@char.leg = (int)msg.reader().readShort();
					if (@char.leg == -1)
					{
						@char.setDefaultLeg();
					}
				}
				break;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				int num15 = msg.reader().readInt();
				global::Char @char;
				if (num15 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num15);
				}
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
				}
				break;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					sbyte b7 = msg.reader().readByte();
					Res.outz("player load hp type= " + b7);
					if ((int)b7 == 1)
					{
						ServerEffect.addServerEffect(11, @char, 5);
						ServerEffect.addServerEffect(104, @char, 4);
					}
					if ((int)b7 == 2)
					{
						@char.doInjure();
					}
					try
					{
						@char.cHPFull = msg.readInt3Byte();
					}
					catch (Exception ex4)
					{
					}
				}
				break;
			}
			case 15:
			{
				GameCanvas.debug("SA33", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.cx = (int)msg.reader().readShort();
					@char.cy = (int)msg.reader().readShort();
					@char.statusMe = 1;
					@char.cp3 = 3;
					ServerEffect.addServerEffect(109, @char, 2);
				}
				break;
			}
			case 19:
				GameCanvas.debug("SA17", 2);
				global::Char.myCharz().boxSort();
				break;
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num16 = msg.reader().readInt();
				global::Char.myCharz().xuInBox -= num16;
				global::Char.myCharz().xu += (long)num16;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				break;
			}
			case 23:
			{
				short num17 = msg.reader().readShort();
				Skill skill3 = Skills.get(num17);
				this.useSkill(skill3);
				if (num17 != 0 && num17 != 14 && num17 != 28)
				{
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill3.template.name, 0);
				}
				break;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num18 = msg.reader().readInt();
				Res.outz("CID = " + num18);
				if (TileMap.mapID == 130)
				{
					GameScr.gI().starVS();
				}
				if (num18 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().cTypePk = msg.reader().readByte();
					if (GameScr.gI().isVS() && (int)global::Char.myCharz().cTypePk != 0)
					{
						GameScr.gI().starVS();
					}
					Res.outz("type pk= " + global::Char.myCharz().cTypePk);
					global::Char.myCharz().npcFocus = null;
					if (!GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
					{
						global::Char.myCharz().mobFocus = null;
					}
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					global::Char @char = GameScr.findCharInMap(num18);
					if (@char != null)
					{
						Res.outz("type pk= " + @char.cTypePk);
						@char.cTypePk = msg.reader().readByte();
						if (@char.isAttacPlayerStatus())
						{
							global::Char.myCharz().charFocus = @char;
						}
					}
				}
				for (int num19 = 0; num19 < GameScr.vCharInMap.size(); num19++)
				{
					global::Char char2 = GameScr.findCharInMap(num19);
					if (char2 != null && (int)char2.cTypePk != 0 && (int)char2.cTypePk == (int)global::Char.myCharz().cTypePk)
					{
						if (!global::Char.myCharz().mobFocus.isMobMe)
						{
							global::Char.myCharz().mobFocus = null;
						}
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				break;
			}
			}
		}
		catch (Exception ex5)
		{
			Cout.println("Loi tai Sub : " + ex5.ToString());
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0004C7A8 File Offset: 0x0004ABA8
	private void useSkill(Skill skill)
	{
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		else if (skill.template.Equals(global::Char.myCharz().myskill.template))
		{
			global::Char.myCharz().myskill = skill;
		}
		global::Char.myCharz().vSkill.addElement(skill);
		if ((skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0)))
		{
			if ((int)skill.template.id == global::Char.myCharz().skillTemplateId)
			{
				Service.gI().selectSkill(global::Char.myCharz().skillTemplateId);
			}
			global::Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0004C8C0 File Offset: 0x0004ACC0
	public bool readCharInfo(global::Char c, Message msg)
	{
		try
		{
			c.clevel = (int)msg.reader().readByte();
			c.isInvisiblez = msg.reader().readBoolean();
			c.cTypePk = msg.reader().readByte();
			Res.outz(string.Concat(new object[]
			{
				"ADD TYPE PK= ",
				c.cTypePk,
				" to player ",
				c.charID,
				" @@ ",
				c.cName
			}));
			c.nClass = GameScr.nClasss[(int)msg.reader().readByte()];
			c.cgender = (int)msg.reader().readByte();
			c.head = (int)msg.reader().readShort();
			c.cName = msg.reader().readUTF();
			c.cHP = msg.readInt3Byte();
			c.dHP = c.cHP;
			if (c.cHP == 0)
			{
				c.statusMe = 14;
			}
			c.cHPFull = msg.readInt3Byte();
			if (c.cy >= TileMap.pxh - 100)
			{
				c.isFlyUp = true;
			}
			c.body = (int)msg.reader().readShort();
			c.leg = (int)msg.reader().readShort();
			c.bag = (int)msg.reader().readUnsignedByte();
			Res.outz(string.Concat(new object[]
			{
				" body= ",
				c.body,
				" leg= ",
				c.leg,
				" bag=",
				c.bag,
				"BAG ==",
				c.bag,
				"*********************************"
			}));
			c.isShadown = true;
			sbyte b = msg.reader().readByte();
			if (c.wp == -1)
			{
				c.setDefaultWeapon();
			}
			if (c.body == -1)
			{
				c.setDefaultBody();
			}
			if (c.leg == -1)
			{
				c.setDefaultLeg();
			}
			c.cx = (int)msg.reader().readShort();
			c.cy = (int)msg.reader().readShort();
			c.xSd = c.cx;
			c.ySd = c.cy;
			c.eff5BuffHp = (int)msg.reader().readShort();
			c.eff5BuffMp = (int)msg.reader().readShort();
			int num = (int)msg.reader().readByte();
			for (int i = 0; i < num; i++)
			{
				EffectChar effectChar = new EffectChar(msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
				c.vEff.addElement(effectChar);
				if ((int)effectChar.template.type == 12 || (int)effectChar.template.type == 11)
				{
					c.isInvisiblez = true;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		return false;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0004CBFC File Offset: 0x0004AFFC
	private void readGetImgByName(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			sbyte nFrame = msg.reader().readByte();
			sbyte[] array = NinjaUtil.readByteArray(msg);
			Image img = this.createImage(array);
			ImgByName.SetImage(text, img, nFrame);
			if (array != null)
			{
				ImgByName.saveRMS(text, nFrame, array);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0004CC64 File Offset: 0x0004B064
	private void createItemNew(myReader d)
	{
		try
		{
			this.loadItemNew(d, -1, true);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0004CC98 File Offset: 0x0004B098
	private void loadItemNew(myReader d, sbyte type, bool isSave)
	{
		try
		{
			d.mark(100000);
			GameScr.vcItem = d.readByte();
			type = d.readByte();
			if ((int)type == 0)
			{
				GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
				for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
				{
					GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
					GameScr.gI().iOptionTemplates[i].id = i;
					GameScr.gI().iOptionTemplates[i].name = d.readUTF();
					GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
				}
				if (isSave)
				{
					d.reset();
					sbyte[] data = new sbyte[d.available()];
					d.readFully(ref data);
					Rms.saveRMS("NRitem0", data);
				}
			}
			else if ((int)type == 1)
			{
				ItemTemplates.itemTemplates.clear();
				int num = (int)d.readShort();
				for (int j = 0; j < num; j++)
				{
					ItemTemplate it = new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean());
					ItemTemplates.add(it);
				}
				if (isSave)
				{
					d.reset();
					sbyte[] data2 = new sbyte[d.available()];
					d.readFully(ref data2);
					Rms.saveRMS("NRitem1", data2);
				}
			}
			else if ((int)type == 2)
			{
				int num2 = (int)d.readShort();
				int num3 = (int)d.readShort();
				for (int k = num2; k < num3; k++)
				{
					ItemTemplate it2 = new ItemTemplate((short)k, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean());
					ItemTemplates.add(it2);
				}
				if (isSave)
				{
					d.reset();
					sbyte[] data3 = new sbyte[d.available()];
					d.readFully(ref data3);
					Rms.saveRMS("NRitem2", data3);
					sbyte[] data4 = new sbyte[]
					{
						GameScr.vcItem
					};
					Rms.saveRMS("NRitemVersion", data4);
					LoginScr.isUpdateItem = false;
					if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
						Service.gI().clientOk();
					}
				}
			}
			else if ((int)type == 100)
			{
				global::Char.Arr_Head_2Fr = this.readArrHead(d);
				if (isSave)
				{
					d.reset();
					sbyte[] data5 = new sbyte[d.available()];
					d.readFully(ref data5);
					Rms.saveRMS("NRitem100", data5);
				}
			}
		}
		catch (Exception ex)
		{
			ex.ToString();
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0004CFD4 File Offset: 0x0004B3D4
	private void readFrameBoss(Message msg, int mobTemplateId)
	{
		try
		{
			int num = (int)msg.reader().readByte();
			int[][] array = new int[num][];
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)msg.reader().readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)msg.reader().readByte();
				}
			}
			Controller.frameHT_NEWBOSS.put(mobTemplateId + string.Empty, array);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0004D07C File Offset: 0x0004B47C
	private int[][] readArrHead(myReader d)
	{
		int[][] array = new int[][]
		{
			new int[]
			{
				542,
				543
			}
		};
		try
		{
			int num = (int)d.readShort();
			array = new int[num][];
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = (int)d.readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)d.readShort();
				}
			}
		}
		catch (Exception ex)
		{
		}
		return array;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0004D11C File Offset: 0x0004B51C
	public void phuban_Info(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if ((int)b == 0)
			{
				this.readPhuBan_CHIENTRUONGNAMEK(msg, (int)b);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0004D160 File Offset: 0x0004B560
	private void readPhuBan_CHIENTRUONGNAMEK(Message msg, int type_PB)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if ((int)b == 0)
			{
				short idmapPaint = msg.reader().readShort();
				string nameTeam = msg.reader().readUTF();
				string nameTeam2 = msg.reader().readUTF();
				int maxPoint = msg.reader().readInt();
				short timeSecond = msg.reader().readShort();
				int maxLife = (int)msg.reader().readByte();
				GameScr.phuban_Info = new InfoPhuBan(type_PB, idmapPaint, nameTeam, nameTeam2, maxPoint, timeSecond);
				GameScr.phuban_Info.maxLife = maxLife;
				GameScr.phuban_Info.updateLife(type_PB, 0, 0);
			}
			else if ((int)b == 1)
			{
				int pointTeam = msg.reader().readInt();
				int pointTeam2 = msg.reader().readInt();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updatePoint(type_PB, pointTeam, pointTeam2);
				}
			}
			else if ((int)b == 2)
			{
				sbyte b2 = msg.reader().readByte();
				short type = 0;
				if ((int)b2 == 1)
				{
					type = 1;
				}
				else if ((int)b2 == 2)
				{
					type = 2;
				}
				short subtype = -1;
				GameScr.phuban_Info = null;
				GameScr.addEffectEnd((int)type, (int)subtype, 0, GameCanvas.hw, GameCanvas.hh, 0, 0, -1, null);
			}
			else if ((int)b == 5)
			{
				short timeSecond2 = msg.reader().readShort();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updateTime(type_PB, timeSecond2);
				}
			}
			else if ((int)b == 4)
			{
				int lifeTeam = (int)msg.reader().readByte();
				int lifeTeam2 = (int)msg.reader().readByte();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updateLife(type_PB, lifeTeam, lifeTeam2);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0004D334 File Offset: 0x0004B734
	public void read_opt(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if ((int)b == 0)
			{
				short idHat = msg.reader().readShort();
				global::Char.myCharz().idHat = idHat;
				SoundMn.gI().getStrOption();
			}
			else if ((int)b == 2)
			{
				int num = msg.reader().readInt();
				sbyte b2 = msg.reader().readByte();
				short num2 = msg.reader().readShort();
				string v = num2 + "," + b2;
				MainImage imagePath = ImgByName.getImagePath("banner_" + num2, ImgByName.hashImagePath);
				GameCanvas.danhHieu.put(num + string.Empty, v);
			}
			else if ((int)b == 3)
			{
				short num3 = msg.reader().readShort();
				SmallImage.createImage((int)num3);
				BackgroudEffect.id_water1 = num3;
			}
			else if ((int)b == 4)
			{
				string o = msg.reader().readUTF();
				GameCanvas.messageServer.addElement(o);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0004D470 File Offset: 0x0004B870
	public void read_UpdateSkill(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = -1;
			try
			{
				b = msg.reader().readSByte();
			}
			catch (Exception ex)
			{
			}
			if ((int)b == 0)
			{
				short curExp = msg.reader().readShort();
				for (int i = 0; i < global::Char.myCharz().vSkill.size(); i++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(i);
					if (skill.skillId == num)
					{
						skill.curExp = curExp;
						break;
					}
				}
			}
			else if ((int)b == 1)
			{
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < global::Char.myCharz().vSkill.size(); j++)
				{
					Skill skill2 = (Skill)global::Char.myCharz().vSkill.elementAt(j);
					if (skill2.skillId == num)
					{
						for (int k = 0; k < 20; k++)
						{
							string nameImg = string.Concat(new object[]
							{
								"Skills_",
								skill2.template.id,
								"_",
								b2,
								"_",
								k
							});
							MainImage imagePath = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
						}
						break;
					}
				}
			}
			else if ((int)b == -1)
			{
				Skill skill3 = Skills.get(num);
				for (int l = 0; l < global::Char.myCharz().vSkill.size(); l++)
				{
					Skill skill4 = (Skill)global::Char.myCharz().vSkill.elementAt(l);
					if ((int)skill4.template.id == (int)skill3.template.id)
					{
						global::Char.myCharz().vSkill.setElementAt(skill3, l);
						break;
					}
				}
				for (int m = 0; m < global::Char.myCharz().vSkillFight.size(); m++)
				{
					Skill skill5 = (Skill)global::Char.myCharz().vSkillFight.elementAt(m);
					if ((int)skill5.template.id == (int)skill3.template.id)
					{
						global::Char.myCharz().vSkillFight.setElementAt(skill3, m);
						break;
					}
				}
				for (int n = 0; n < GameScr.onScreenSkill.Length; n++)
				{
					if (GameScr.onScreenSkill[n] != null && (int)GameScr.onScreenSkill[n].template.id == (int)skill3.template.id)
					{
						GameScr.onScreenSkill[n] = skill3;
						break;
					}
				}
				for (int num2 = 0; num2 < GameScr.keySkill.Length; num2++)
				{
					if (GameScr.keySkill[num2] != null && (int)GameScr.keySkill[num2].template.id == (int)skill3.template.id)
					{
						GameScr.keySkill[num2] = skill3;
						break;
					}
				}
				if ((int)global::Char.myCharz().myskill.template.id == (int)skill3.template.id)
				{
					global::Char.myCharz().myskill = skill3;
				}
				GameScr.info1.addInfo(string.Concat(new object[]
				{
					mResources.hasJustUpgrade1,
					skill3.template.name,
					mResources.hasJustUpgrade2,
					skill3.point
				}), 0);
			}
		}
		catch (Exception ex2)
		{
		}
	}

	// Token: 0x040009FE RID: 2558
	protected static Controller me;

	// Token: 0x040009FF RID: 2559
	protected static Controller me2;

	// Token: 0x04000A00 RID: 2560
	public Message messWait;

	// Token: 0x04000A01 RID: 2561
	public static bool isLoadingData = false;

	// Token: 0x04000A02 RID: 2562
	public static bool isConnectOK;

	// Token: 0x04000A03 RID: 2563
	public static bool isConnectionFail;

	// Token: 0x04000A04 RID: 2564
	public static bool isDisconnected;

	// Token: 0x04000A05 RID: 2565
	public static bool isMain;

	// Token: 0x04000A06 RID: 2566
	private float demCount;

	// Token: 0x04000A07 RID: 2567
	private int move;

	// Token: 0x04000A08 RID: 2568
	private int total;

	// Token: 0x04000A09 RID: 2569
	public static bool isStopReadMessage;

	// Token: 0x04000A0A RID: 2570
	public static MyHashTable frameHT_NEWBOSS = new MyHashTable();

	// Token: 0x04000A0B RID: 2571
	public const sbyte PHUBAN_TYPE_CHIENTRUONGNAMEK = 0;

	// Token: 0x04000A0C RID: 2572
	public const sbyte PHUBAN_START = 0;

	// Token: 0x04000A0D RID: 2573
	public const sbyte PHUBAN_UPDATE_POINT = 1;

	// Token: 0x04000A0E RID: 2574
	public const sbyte PHUBAN_END = 2;

	// Token: 0x04000A0F RID: 2575
	public const sbyte PHUBAN_LIFE = 4;

	// Token: 0x04000A10 RID: 2576
	public const sbyte PHUBAN_INFO = 5;
}
