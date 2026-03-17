using System;
using Assets.src.g;

namespace Assets.src.f
{
	// Token: 0x020000C4 RID: 196
	internal class Controller2
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x000AC778 File Offset: 0x000AA978
		public static void readMessage(Message msg)
		{
			try
			{
				sbyte command = msg.command;
				switch ((int)command + 128)
				{
				case 0:
					Controller2.readInfoEffChar(msg);
					goto IL_282E;
				case 1:
					Controller2.readLuckyRound(msg);
					goto IL_282E;
				case 2:
				{
					sbyte b = msg.reader().readByte();
					Res.outz("type quay= " + b.ToString());
					bool flag2 = b == 1;
					if (flag2)
					{
						sbyte b2 = msg.reader().readByte();
						string num = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num, finish);
					}
					bool flag3 = b == 0;
					if (flag3)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					goto IL_282E;
				}
				case 3:
				{
					ChatTextField.gI().isShow = false;
					string text = msg.reader().readUTF();
					Res.outz("titile= " + text);
					sbyte b3 = msg.reader().readByte();
					ClientInput.gI().setInput((int)b3, text);
					for (int i = 0; i < (int)b3; i++)
					{
						ClientInput.gI().tf[i].name = msg.reader().readUTF();
						sbyte b4 = msg.reader().readByte();
						bool flag4 = b4 == 0;
						if (flag4)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						bool flag5 = b4 == 1;
						if (flag5)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_ANY);
						}
						bool flag6 = b4 == 2;
						if (flag6)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					goto IL_282E;
				}
				case 4:
				{
					sbyte b5 = msg.reader().readByte();
					sbyte b6 = msg.reader().readByte();
					bool flag7 = b6 == 0;
					if (flag7)
					{
						bool flag8 = b5 == 2;
						if (flag8)
						{
							int num2 = msg.reader().readInt();
							bool flag9 = num2 == global::Char.myCharz().charID;
							if (flag9)
							{
								global::Char.myCharz().removeEffect();
							}
							else
							{
								bool flag10 = GameScr.findCharInMap(num2) != null;
								if (flag10)
								{
									GameScr.findCharInMap(num2).removeEffect();
								}
							}
						}
						int num3 = (int)msg.reader().readUnsignedByte();
						int num4 = msg.reader().readInt();
						bool flag11 = num3 == 32;
						if (flag11)
						{
							bool flag12 = b5 == 1;
							if (flag12)
							{
								int num5 = msg.reader().readInt();
								bool flag13 = num4 == global::Char.myCharz().charID;
								if (flag13)
								{
									global::Char.myCharz().holdEffID = num3;
									GameScr.findCharInMap(num5).setHoldChar(global::Char.myCharz());
								}
								else
								{
									bool flag14 = GameScr.findCharInMap(num4) != null && num5 != global::Char.myCharz().charID;
									if (flag14)
									{
										GameScr.findCharInMap(num4).holdEffID = num3;
										GameScr.findCharInMap(num5).setHoldChar(GameScr.findCharInMap(num4));
									}
									else
									{
										bool flag15 = GameScr.findCharInMap(num4) != null && num5 == global::Char.myCharz().charID;
										if (flag15)
										{
											GameScr.findCharInMap(num4).holdEffID = num3;
											global::Char.myCharz().setHoldChar(GameScr.findCharInMap(num4));
										}
									}
								}
							}
							else
							{
								bool flag16 = num4 == global::Char.myCharz().charID;
								if (flag16)
								{
									global::Char.myCharz().removeHoleEff();
								}
								else
								{
									bool flag17 = GameScr.findCharInMap(num4) != null;
									if (flag17)
									{
										GameScr.findCharInMap(num4).removeHoleEff();
									}
								}
							}
						}
						bool flag18 = num3 == 33;
						if (flag18)
						{
							bool flag19 = b5 == 1;
							if (flag19)
							{
								bool flag20 = num4 == global::Char.myCharz().charID;
								if (flag20)
								{
									global::Char.myCharz().protectEff = true;
								}
								else
								{
									bool flag21 = GameScr.findCharInMap(num4) != null;
									if (flag21)
									{
										GameScr.findCharInMap(num4).protectEff = true;
									}
								}
							}
							else
							{
								bool flag22 = num4 == global::Char.myCharz().charID;
								if (flag22)
								{
									global::Char.myCharz().removeProtectEff();
								}
								else
								{
									bool flag23 = GameScr.findCharInMap(num4) != null;
									if (flag23)
									{
										GameScr.findCharInMap(num4).removeProtectEff();
									}
								}
							}
						}
						bool flag24 = num3 == 39;
						if (flag24)
						{
							bool flag25 = b5 == 1;
							if (flag25)
							{
								bool flag26 = num4 == global::Char.myCharz().charID;
								if (flag26)
								{
									global::Char.myCharz().huytSao = true;
								}
								else
								{
									bool flag27 = GameScr.findCharInMap(num4) != null;
									if (flag27)
									{
										GameScr.findCharInMap(num4).huytSao = true;
									}
								}
							}
							else
							{
								bool flag28 = num4 == global::Char.myCharz().charID;
								if (flag28)
								{
									global::Char.myCharz().removeHuytSao();
								}
								else
								{
									bool flag29 = GameScr.findCharInMap(num4) != null;
									if (flag29)
									{
										GameScr.findCharInMap(num4).removeHuytSao();
									}
								}
							}
						}
						bool flag30 = num3 == 40;
						if (flag30)
						{
							bool flag31 = b5 == 1;
							if (flag31)
							{
								bool flag32 = num4 == global::Char.myCharz().charID;
								if (flag32)
								{
									global::Char.myCharz().blindEff = true;
								}
								else
								{
									bool flag33 = GameScr.findCharInMap(num4) != null;
									if (flag33)
									{
										GameScr.findCharInMap(num4).blindEff = true;
									}
								}
							}
							else
							{
								bool flag34 = num4 == global::Char.myCharz().charID;
								if (flag34)
								{
									global::Char.myCharz().removeBlindEff();
								}
								else
								{
									bool flag35 = GameScr.findCharInMap(num4) != null;
									if (flag35)
									{
										GameScr.findCharInMap(num4).removeBlindEff();
									}
								}
							}
						}
						bool flag36 = num3 == 41;
						if (flag36)
						{
							bool flag37 = b5 == 1;
							if (flag37)
							{
								bool flag38 = num4 == global::Char.myCharz().charID;
								if (flag38)
								{
									global::Char.myCharz().sleepEff = true;
								}
								else
								{
									bool flag39 = GameScr.findCharInMap(num4) != null;
									if (flag39)
									{
										GameScr.findCharInMap(num4).sleepEff = true;
									}
								}
							}
							else
							{
								bool flag40 = num4 == global::Char.myCharz().charID;
								if (flag40)
								{
									global::Char.myCharz().removeSleepEff();
								}
								else
								{
									bool flag41 = GameScr.findCharInMap(num4) != null;
									if (flag41)
									{
										GameScr.findCharInMap(num4).removeSleepEff();
									}
								}
							}
						}
						bool flag42 = num3 == 42;
						if (flag42)
						{
							bool flag43 = b5 == 1;
							if (flag43)
							{
								bool flag44 = num4 == global::Char.myCharz().charID;
								if (flag44)
								{
									global::Char.myCharz().stone = true;
								}
							}
							else
							{
								bool flag45 = num4 == global::Char.myCharz().charID;
								if (flag45)
								{
									global::Char.myCharz().stone = false;
								}
							}
						}
					}
					bool flag46 = b6 == 1;
					if (flag46)
					{
						int num6 = (int)msg.reader().readUnsignedByte();
						sbyte b7 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"modbHoldID= ",
							b7,
							" skillID= ",
							num6,
							"eff ID= ",
							b5
						}));
						bool flag47 = num6 == 32;
						if (flag47)
						{
							bool flag48 = b5 == 1;
							if (flag48)
							{
								int num7 = msg.reader().readInt();
								bool flag49 = num7 == global::Char.myCharz().charID;
								if (flag49)
								{
									GameScr.findMobInMap(b7).holdEffID = num6;
									global::Char.myCharz().setHoldMob(GameScr.findMobInMap(b7));
								}
								else
								{
									bool flag50 = GameScr.findCharInMap(num7) != null;
									if (flag50)
									{
										GameScr.findMobInMap(b7).holdEffID = num6;
										GameScr.findCharInMap(num7).setHoldMob(GameScr.findMobInMap(b7));
									}
								}
							}
							else
							{
								GameScr.findMobInMap(b7).removeHoldEff();
							}
						}
						bool flag51 = num6 == 40;
						if (flag51)
						{
							bool flag52 = b5 == 1;
							if (flag52)
							{
								GameScr.findMobInMap(b7).blindEff = true;
							}
							else
							{
								GameScr.findMobInMap(b7).removeBlindEff();
							}
						}
						bool flag53 = num6 == 41;
						if (flag53)
						{
							bool flag54 = b5 == 1;
							if (flag54)
							{
								GameScr.findMobInMap(b7).sleepEff = true;
							}
							else
							{
								GameScr.findMobInMap(b7).removeSleepEff();
							}
						}
					}
					goto IL_282E;
				}
				case 5:
				{
					int charId = msg.reader().readInt();
					bool flag55 = GameScr.findCharInMap(charId) != null;
					if (flag55)
					{
						GameScr.findCharInMap(charId).perCentMp = (int)msg.reader().readByte();
					}
					goto IL_282E;
				}
				case 6:
				{
					short id = msg.reader().readShort();
					Npc npc = GameScr.findNPCInMap(id);
					sbyte b8 = msg.reader().readByte();
					npc.duahau = new int[(int)b8];
					Res.outz("N DUA HAU= " + b8.ToString());
					for (int j = 0; j < (int)b8; j++)
					{
						npc.duahau[j] = (int)msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					goto IL_282E;
				}
				case 7:
				{
					long num8 = mSystem.currentTimeMillis();
					Service.logMap = num8 - Service.curCheckMap;
					Service.gI().sendCheckMap();
					goto IL_282E;
				}
				case 8:
				{
					long num9 = mSystem.currentTimeMillis();
					Service.logController = num9 - Service.curCheckController;
					Service.gI().sendCheckController();
					goto IL_282E;
				}
				case 9:
					global::Char.myCharz().rank = msg.reader().readInt();
					goto IL_282E;
				case 11:
				{
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					bool flag56 = GameScr.gI().percentMabu == 100;
					if (flag56)
					{
						GameScr.gI().mabuEff = true;
					}
					bool flag57 = GameScr.gI().percentMabu == 101;
					if (flag57)
					{
						Npc.mabuEff = true;
					}
					goto IL_282E;
				}
				case 12:
					GameScr.canAutoPlay = (msg.reader().readByte() == 1);
					goto IL_282E;
				case 13:
					global::Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					goto IL_282E;
				case 15:
				{
					sbyte[] array10 = new sbyte[10];
					for (int num10 = 0; num10 < 10; num10++)
					{
						array10[num10] = msg.reader().readByte();
						Res.outz("vlue i= " + array10[num10].ToString());
					}
					GameScr.gI().onKSkill(array10);
					GameScr.gI().onOSkill(array10);
					GameScr.gI().onCSkill(array10);
					goto IL_282E;
				}
				case 17:
				{
					short num11 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int num12 = 0; num12 < (int)num11; num12++)
					{
						string id2 = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(id2, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					goto IL_282E;
				}
				case 18:
				{
					sbyte b9 = msg.reader().readByte();
					bool flag58 = b9 == 1;
					if (flag58)
					{
						int num13 = msg.reader().readInt();
						sbyte[] array11 = Rms.loadRMS(num13.ToString() + string.Empty);
						bool flag59 = array11 == null;
						if (flag59)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, num13, array11);
						}
					}
					bool flag60 = b9 == 0;
					if (flag60)
					{
						int num14 = msg.reader().readInt();
						short num15 = msg.reader().readShort();
						sbyte[] data = new sbyte[(int)num15];
						msg.reader().read(ref data, 0, (int)num15);
						Rms.saveRMS(num14.ToString() + string.Empty, data);
					}
					goto IL_282E;
				}
				case 22:
				{
					short num16 = msg.reader().readShort();
					int num17 = (int)msg.reader().readShort();
					bool flag61 = ItemTime.isExistItem((int)num16);
					if (flag61)
					{
						ItemTime.getItemById((int)num16).initTime(num17);
					}
					else
					{
						ItemTime o = new ItemTime(num16, num17);
						global::Char.vItemTime.addElement(o);
					}
					goto IL_282E;
				}
				case 23:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					goto IL_282E;
				case 25:
				{
					sbyte b10 = msg.reader().readByte();
					bool flag62 = b10 == 0;
					if (flag62)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b11 = msg.reader().readByte();
						for (int num18 = 0; num18 < (int)b11; num18++)
						{
							Item item2 = new Item();
							short num19 = msg.reader().readShort();
							bool flag63 = num19 != -1;
							if (flag63)
							{
								item2.template = ItemTemplates.get(num19);
								sbyte b12 = msg.reader().readByte();
								bool flag64 = b12 != -1;
								if (flag64)
								{
									item2.itemOption = new ItemOption[(int)b12];
									for (int num20 = 0; num20 < item2.itemOption.Length; num20++)
									{
										int num21 = (int)msg.reader().readUnsignedByte();
										int param2 = (int)msg.reader().readUnsignedShort();
										bool flag65 = num21 != -1;
										if (flag65)
										{
											item2.itemOption[num20] = new ItemOption(num21, param2);
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item2);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else
					{
						bool flag66 = b10 == 1;
						if (flag66)
						{
							int num22 = msg.reader().readInt();
							sbyte b13 = msg.reader().readByte();
							Res.outz(string.Concat(new object[]
							{
								"---------------actionFlag1:  ",
								num22,
								" : ",
								b13
							}));
							bool flag67 = num22 == global::Char.myCharz().charID;
							if (flag67)
							{
								global::Char.myCharz().cFlag = b13;
							}
							else
							{
								bool flag68 = GameScr.findCharInMap(num22) != null;
								if (flag68)
								{
									GameScr.findCharInMap(num22).cFlag = b13;
								}
							}
							GameScr.gI().getFlagImage(num22, b13);
						}
						else
						{
							bool flag69 = b10 == 2;
							if (flag69)
							{
								sbyte b14 = msg.reader().readByte();
								int num23 = (int)msg.reader().readShort();
								PKFlag pkflag = new PKFlag();
								pkflag.cflag = b14;
								pkflag.IDimageFlag = num23;
								GameScr.vFlag.addElement(pkflag);
								for (int num24 = 0; num24 < GameScr.vFlag.size(); num24++)
								{
									PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(num24);
									Res.outz(string.Concat(new object[]
									{
										"i: ",
										num24,
										"  cflag: ",
										pkflag2.cflag,
										"   IDimageFlag: ",
										pkflag2.IDimageFlag
									}));
								}
								for (int num25 = 0; num25 < GameScr.vCharInMap.size(); num25++)
								{
									global::Char char6 = (global::Char)GameScr.vCharInMap.elementAt(num25);
									bool flag70 = char6 != null && char6.cFlag == b14;
									if (flag70)
									{
										char6.flagImage = num23;
									}
								}
								bool flag71 = global::Char.myCharz().cFlag == b14;
								if (flag71)
								{
									global::Char.myCharz().flagImage = num23;
								}
							}
						}
					}
					goto IL_282E;
				}
				case 26:
				{
					sbyte b15 = msg.reader().readByte();
					bool flag72 = b15 != 0;
					if (flag72)
					{
						bool flag73 = b15 == 1;
						if (flag73)
						{
							GameCanvas.loginScr.isLogin2 = false;
							Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
							LoginScr.isLoggingIn = true;
						}
					}
					goto IL_282E;
				}
				case 27:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text2 = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect.ToString(), text2);
					Service.gI().setClientType();
					Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
					goto IL_282E;
				}
				case 28:
				{
					InfoDlg.hide();
					bool flag = false;
					bool flag74 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
					if (flag74)
					{
						flag = true;
					}
					sbyte b16 = msg.reader().readByte();
					Res.outz("t Indxe= " + b16.ToString());
					GameCanvas.panel.maxPageShop[(int)b16] = (int)msg.reader().readByte();
					GameCanvas.panel.currPageShop[(int)b16] = (int)msg.reader().readByte();
					Res.outz(string.Concat(new object[]
					{
						"max page= ",
						GameCanvas.panel.maxPageShop[(int)b16],
						" curr page= ",
						GameCanvas.panel.currPageShop[(int)b16]
					}));
					int num26 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[(int)b16] = new Item[num26];
					for (int num27 = 0; num27 < num26; num27++)
					{
						short num28 = msg.reader().readShort();
						bool flag75 = num28 != -1;
						if (flag75)
						{
							Res.outz("template id= " + num28.ToString());
							global::Char.myCharz().arrItemShop[(int)b16][num27] = new Item();
							global::Char.myCharz().arrItemShop[(int)b16][num27].template = ItemTemplates.get(num28);
							global::Char.myCharz().arrItemShop[(int)b16][num27].itemId = (int)msg.reader().readShort();
							global::Char.myCharz().arrItemShop[(int)b16][num27].buyCoin = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b16][num27].buyGold = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b16][num27].buyType = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[(int)b16][num27].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b16][num27].isMe = msg.reader().readByte();
							Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
							sbyte b17 = msg.reader().readByte();
							bool flag76 = b17 != -1;
							if (flag76)
							{
								global::Char.myCharz().arrItemShop[(int)b16][num27].itemOption = new ItemOption[(int)b17];
								for (int num29 = 0; num29 < global::Char.myCharz().arrItemShop[(int)b16][num27].itemOption.Length; num29++)
								{
									int num30 = (int)msg.reader().readUnsignedByte();
									int param3 = (int)msg.reader().readUnsignedShort();
									bool flag77 = num30 != -1;
									if (flag77)
									{
										global::Char.myCharz().arrItemShop[(int)b16][num27].itemOption[num29] = new ItemOption(num30, param3);
										global::Char.myCharz().arrItemShop[(int)b16][num27].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[(int)b16][num27]);
									}
								}
							}
							sbyte b18 = msg.reader().readByte();
							bool flag78 = b18 == 1;
							if (flag78)
							{
								int headTemp = (int)msg.reader().readShort();
								int bodyTemp = (int)msg.reader().readShort();
								int legTemp = (int)msg.reader().readShort();
								int bagTemp = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[(int)b16][num27].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
							}
							bool flag79 = GameMidlet.intVERSION >= 237;
							if (flag79)
							{
								global::Char.myCharz().arrItemShop[(int)b16][num27].nameNguoiKyGui = msg.reader().readUTF();
								Res.err("nguoi ki gui  " + global::Char.myCharz().arrItemShop[(int)b16][num27].nameNguoiKyGui);
							}
						}
					}
					bool flag80 = flag;
					if (flag80)
					{
						GameCanvas.panel2.setTabKiGui();
					}
					GameCanvas.panel.setTabShop();
					GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
					goto IL_282E;
				}
				case 39:
					GameCanvas.open3Hour = (msg.reader().readByte() == 1);
					goto IL_282E;
				}
				switch (command)
				{
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					goto IL_196D;
				case 122:
				{
					short num31 = msg.reader().readShort();
					Res.outz("second login = " + num31.ToString());
					LoginScr.timeLogin = num31;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					goto IL_196D;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num32 = msg.reader().readInt();
					short xPos = msg.reader().readShort();
					short yPos = msg.reader().readShort();
					sbyte b19 = msg.reader().readByte();
					global::Char char7 = null;
					bool flag81 = num32 == global::Char.myCharz().charID;
					if (flag81)
					{
						char7 = global::Char.myCharz();
					}
					else
					{
						bool flag82 = GameScr.findCharInMap(num32) != null;
						if (flag82)
						{
							char7 = GameScr.findCharInMap(num32);
						}
					}
					bool flag83 = char7 != null;
					if (flag83)
					{
						ServerEffect.addServerEffect((b19 != 0) ? 173 : 60, char7, 1);
						char7.setPos(xPos, yPos, b19);
					}
					goto IL_196D;
				}
				case 124:
				{
					short num33 = msg.reader().readShort();
					string text3 = msg.reader().readUTF();
					Res.outz(string.Concat(new object[]
					{
						"noi chuyen = ",
						text3,
						"npc ID= ",
						num33
					}));
					Npc npc2 = GameScr.findNPCInMap(num33);
					bool flag84 = npc2 != null;
					if (flag84)
					{
						npc2.addInfo(text3);
					}
					goto IL_196D;
				}
				case 125:
				{
					sbyte fusion = msg.reader().readByte();
					int num34 = msg.reader().readInt();
					bool flag85 = num34 == global::Char.myCharz().charID;
					if (flag85)
					{
						global::Char.myCharz().setFusion(fusion);
					}
					else
					{
						bool flag86 = GameScr.findCharInMap(num34) != null;
						if (flag86)
						{
							GameScr.findCharInMap(num34).setFusion(fusion);
						}
					}
					goto IL_196D;
				}
				case 127:
					Controller2.readInfoRada(msg);
					goto IL_196D;
				}
				switch (command)
				{
				case 48:
				{
					sbyte b20 = msg.reader().readByte();
					ServerListScreen.ipSelect = (int)b20;
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					goto IL_1962;
				}
				case 51:
				{
					int charId2 = msg.reader().readInt();
					Mabu mabu = (Mabu)GameScr.findCharInMap(charId2);
					sbyte id3 = msg.reader().readByte();
					short x2 = msg.reader().readShort();
					short y2 = msg.reader().readShort();
					sbyte b21 = msg.reader().readByte();
					global::Char[] array12 = new global::Char[(int)b21];
					int[] array13 = new int[(int)b21];
					for (int num35 = 0; num35 < (int)b21; num35++)
					{
						int num36 = msg.reader().readInt();
						Res.outz("char ID=" + num36.ToString());
						array12[num35] = null;
						bool flag87 = num36 != global::Char.myCharz().charID;
						if (flag87)
						{
							array12[num35] = GameScr.findCharInMap(num36);
						}
						else
						{
							array12[num35] = global::Char.myCharz();
						}
						array13[num35] = msg.reader().readInt();
					}
					mabu.setSkill(id3, x2, y2, array12, array13);
					goto IL_1962;
				}
				case 52:
				{
					sbyte b22 = msg.reader().readByte();
					bool flag88 = b22 == 1;
					if (flag88)
					{
						int num37 = msg.reader().readInt();
						bool flag89 = num37 == global::Char.myCharz().charID;
						if (flag89)
						{
							global::Char.myCharz().setMabuHold(true);
							global::Char.myCharz().cx = (int)msg.reader().readShort();
							global::Char.myCharz().cy = (int)msg.reader().readShort();
						}
						else
						{
							global::Char char8 = GameScr.findCharInMap(num37);
							bool flag90 = char8 != null;
							if (flag90)
							{
								char8.setMabuHold(true);
								char8.cx = (int)msg.reader().readShort();
								char8.cy = (int)msg.reader().readShort();
							}
						}
					}
					bool flag91 = b22 == 0;
					if (flag91)
					{
						int num38 = msg.reader().readInt();
						bool flag92 = num38 == global::Char.myCharz().charID;
						if (flag92)
						{
							global::Char.myCharz().setMabuHold(false);
						}
						else
						{
							global::Char char9 = GameScr.findCharInMap(num38);
							bool flag93 = char9 != null;
							if (flag93)
							{
								char9.setMabuHold(false);
							}
						}
					}
					bool flag94 = b22 == 2;
					if (flag94)
					{
						int charId3 = msg.reader().readInt();
						int id4 = msg.reader().readInt();
						Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId3);
						mabu2.eat(id4);
					}
					bool flag95 = b22 == 3;
					if (flag95)
					{
						GameScr.mabuPercent = msg.reader().readByte();
					}
					goto IL_1962;
				}
				}
				switch (command)
				{
				case 100:
				{
					sbyte b23 = msg.reader().readByte();
					sbyte b24 = msg.reader().readByte();
					Item item3 = null;
					bool flag96 = b23 == 0;
					if (flag96)
					{
						item3 = global::Char.myCharz().arrItemBody[(int)b24];
					}
					bool flag97 = b23 == 1;
					if (flag97)
					{
						item3 = global::Char.myCharz().arrItemBag[(int)b24];
					}
					short num39 = msg.reader().readShort();
					bool flag98 = num39 != -1;
					if (flag98)
					{
						item3.template = ItemTemplates.get(num39);
						item3.quantity = msg.reader().readInt();
						item3.info = msg.reader().readUTF();
						item3.content = msg.reader().readUTF();
						sbyte b25 = msg.reader().readByte();
						bool flag99 = b25 != 0;
						if (flag99)
						{
							item3.itemOption = new ItemOption[(int)b25];
							for (int k = 0; k < item3.itemOption.Length; k++)
							{
								int num40 = (int)msg.reader().readUnsignedByte();
								Res.outz("id o= " + num40.ToString());
								int param4 = (int)msg.reader().readUnsignedShort();
								bool flag100 = num40 != -1;
								if (flag100)
								{
									item3.itemOption[k] = new ItemOption(num40, param4);
								}
							}
						}
						bool flag101 = item3.quantity <= 0;
						if (flag101)
						{
						}
					}
					break;
				}
				case 101:
				{
					Res.outz("big boss--------------------------------------------------");
					BigBoss bigBoss = Mob.getBigBoss();
					bool flag102 = bigBoss != null;
					if (flag102)
					{
						sbyte b26 = msg.reader().readByte();
						bool flag103 = b26 == 0 || b26 == 1 || b26 == 2 || b26 == 4 || b26 == 3;
						if (flag103)
						{
							bool flag104 = b26 == 3;
							if (flag104)
							{
								bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
								bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
								bigBoss.setFly();
							}
							else
							{
								sbyte b27 = msg.reader().readByte();
								Res.outz("CHUONG nChar= " + b27.ToString());
								global::Char[] array14 = new global::Char[(int)b27];
								int[] array15 = new int[(int)b27];
								for (int l = 0; l < (int)b27; l++)
								{
									int num41 = msg.reader().readInt();
									Res.outz("char ID=" + num41.ToString());
									array14[l] = null;
									bool flag105 = num41 != global::Char.myCharz().charID;
									if (flag105)
									{
										array14[l] = GameScr.findCharInMap(num41);
									}
									else
									{
										array14[l] = global::Char.myCharz();
									}
									array15[l] = msg.reader().readInt();
								}
								bigBoss.setAttack(array14, array15, b26);
							}
						}
						bool flag106 = b26 == 5;
						if (flag106)
						{
							bigBoss.haftBody = true;
							bigBoss.status = 2;
						}
						bool flag107 = b26 == 6;
						if (flag107)
						{
							bigBoss.getDataB2();
							bigBoss.x = (int)msg.reader().readShort();
							bigBoss.y = (int)msg.reader().readShort();
						}
						bool flag108 = b26 == 7;
						if (flag108)
						{
							bigBoss.setAttack(null, null, b26);
						}
						bool flag109 = b26 == 8;
						if (flag109)
						{
							bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
							bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
							bigBoss.status = 2;
						}
						bool flag110 = b26 == 9;
						if (flag110)
						{
							bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
						}
					}
					break;
				}
				case 102:
				{
					sbyte b28 = msg.reader().readByte();
					bool flag111 = b28 == 0 || b28 == 1 || b28 == 2 || b28 == 6;
					if (flag111)
					{
						BigBoss2 bigBoss2 = Mob.getBigBoss2();
						bool flag112 = bigBoss2 == null;
						if (flag112)
						{
							break;
						}
						bool flag113 = b28 == 6;
						if (flag113)
						{
							bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
							break;
						}
						sbyte b29 = msg.reader().readByte();
						global::Char[] array16 = new global::Char[(int)b29];
						int[] array17 = new int[(int)b29];
						for (int m = 0; m < (int)b29; m++)
						{
							int num42 = msg.reader().readInt();
							array16[m] = null;
							bool flag114 = num42 != global::Char.myCharz().charID;
							if (flag114)
							{
								array16[m] = GameScr.findCharInMap(num42);
							}
							else
							{
								array16[m] = global::Char.myCharz();
							}
							array17[m] = msg.reader().readInt();
						}
						bigBoss2.setAttack(array16, array17, b28);
					}
					bool flag115 = b28 == 3 || b28 == 4 || b28 == 5 || b28 == 7;
					if (flag115)
					{
						BachTuoc bachTuoc = Mob.getBachTuoc();
						bool flag116 = bachTuoc == null;
						if (flag116)
						{
							break;
						}
						bool flag117 = b28 == 7;
						if (flag117)
						{
							bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
							break;
						}
						bool flag118 = b28 == 3 || b28 == 4;
						if (flag118)
						{
							sbyte b30 = msg.reader().readByte();
							global::Char[] array18 = new global::Char[(int)b30];
							int[] array19 = new int[(int)b30];
							for (int n = 0; n < (int)b30; n++)
							{
								int num43 = msg.reader().readInt();
								array18[n] = null;
								bool flag119 = num43 != global::Char.myCharz().charID;
								if (flag119)
								{
									array18[n] = GameScr.findCharInMap(num43);
								}
								else
								{
									array18[n] = global::Char.myCharz();
								}
								array19[n] = msg.reader().readInt();
							}
							bachTuoc.setAttack(array18, array19, b28);
						}
						bool flag120 = b28 == 5;
						if (flag120)
						{
							short xMoveTo = msg.reader().readShort();
							bachTuoc.move(xMoveTo);
						}
					}
					bool flag121 = b28 > 9 && b28 < 30;
					if (flag121)
					{
						Controller2.readActionBoss(msg, (int)b28);
					}
					break;
				}
				default:
				{
					bool flag122 = command != 113;
					if (flag122)
					{
						bool flag123 = command != 114;
						if (flag123)
						{
							bool flag124 = command != 31;
							if (flag124)
							{
								bool flag125 = command != 42;
								if (flag125)
								{
									bool flag126 = command == 93;
									if (flag126)
									{
										string text4 = msg.reader().readUTF();
										text4 = Res.changeString(text4);
										GameScr.gI().chatVip(text4);
									}
								}
								else
								{
									GameCanvas.endDlg();
									LoginScr.isContinueToLogin = false;
									global::Char.isLoadingMap = false;
									sbyte haveName = msg.reader().readByte();
									bool flag127 = GameCanvas.registerScr == null;
									if (flag127)
									{
										GameCanvas.registerScr = new RegisterScreen(haveName);
									}
									GameCanvas.registerScr.switchToMe();
								}
							}
							else
							{
								int num44 = msg.reader().readInt();
								sbyte b31 = msg.reader().readByte();
								bool flag128 = b31 == 1;
								if (flag128)
								{
									short smallID = msg.reader().readShort();
									sbyte b32 = -1;
									int[] array20 = null;
									short wimg = 0;
									short himg = 0;
									try
									{
										b32 = msg.reader().readByte();
										bool flag129 = b32 > 0;
										if (flag129)
										{
											sbyte b33 = msg.reader().readByte();
											array20 = new int[(int)b33];
											for (int num45 = 0; num45 < (int)b33; num45++)
											{
												array20[num45] = (int)msg.reader().readByte();
											}
											wimg = msg.reader().readShort();
											himg = msg.reader().readShort();
										}
									}
									catch (Exception ex)
									{
									}
									bool flag130 = num44 == global::Char.myCharz().charID;
									if (flag130)
									{
										global::Char.myCharz().petFollow = new PetFollow();
										global::Char.myCharz().petFollow.smallID = smallID;
										bool flag131 = b32 > 0;
										if (flag131)
										{
											global::Char.myCharz().petFollow.SetImg((int)b32, array20, (int)wimg, (int)himg);
										}
									}
									else
									{
										global::Char char10 = GameScr.findCharInMap(num44);
										char10.petFollow = new PetFollow();
										char10.petFollow.smallID = smallID;
										bool flag132 = b32 > 0;
										if (flag132)
										{
											char10.petFollow.SetImg((int)b32, array20, (int)wimg, (int)himg);
										}
									}
								}
								else
								{
									bool flag133 = num44 == global::Char.myCharz().charID;
									if (flag133)
									{
										global::Char.myCharz().petFollow.remove();
										global::Char.myCharz().petFollow = null;
									}
									else
									{
										global::Char char11 = GameScr.findCharInMap(num44);
										char11.petFollow.remove();
										char11.petFollow = null;
									}
								}
							}
						}
						else
						{
							try
							{
								string text5 = msg.reader().readUTF();
								mSystem.curINAPP = msg.reader().readByte();
								mSystem.maxINAPP = msg.reader().readByte();
							}
							catch (Exception ex2)
							{
							}
						}
					}
					else
					{
						int loop = 0;
						int layer = 0;
						int id5 = 0;
						short x3 = 0;
						short y3 = 0;
						short loopCount = -1;
						try
						{
							loop = (int)msg.reader().readByte();
							layer = (int)msg.reader().readByte();
							id5 = (int)msg.reader().readUnsignedByte();
							x3 = msg.reader().readShort();
							y3 = msg.reader().readShort();
							loopCount = msg.reader().readShort();
						}
						catch (Exception ex3)
						{
						}
						EffecMn.addEff(new Effect(id5, (int)x3, (int)y3, layer, loop, (int)loopCount));
					}
					break;
				}
				}
				IL_1962:
				IL_196D:
				IL_282E:;
			}
			catch (Exception ex4)
			{
				Res.outz("=====> Controller2 " + ex4.StackTrace);
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000AF040 File Offset: 0x000AD240
		private static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				bool flag = b == 0;
				if (flag)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[(int)b2];
					for (int i = 0; i < (int)b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, price, idTicket);
				}
				else
				{
					bool flag2 = b == 1;
					if (flag2)
					{
						sbyte b4 = msg.reader().readByte();
						short[] array2 = new short[(int)b4];
						for (int j = 0; j < (int)b4; j++)
						{
							array2[j] = msg.reader().readShort();
						}
						CrackBallScr.gI().DoneCrackBallScr(array2);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000AF14C File Offset: 0x000AD34C
		private static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				bool flag = b == 0;
				if (flag)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < (int)num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = (int)msg.reader().readShort();
						int no = i + 1;
						int idIcon = (int)msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						global::Char charInfo = null;
						sbyte b2 = msg.reader().readByte();
						bool flag2 = b2 == 0;
						if (flag2)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							int head = (int)msg.reader().readShort();
							int body = (int)msg.reader().readShort();
							int leg = (int)msg.reader().readShort();
							int bag = (int)msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						ItemOption[] array = null;
						bool flag3 = b4 != 0;
						if (flag3)
						{
							array = new ItemOption[(int)b4];
							for (int j = 0; j < array.Length; j++)
							{
								int num3 = (int)msg.reader().readUnsignedByte();
								int param = (int)msg.reader().readUnsignedShort();
								sbyte activeCard = msg.reader().readByte();
								bool flag4 = num3 != -1;
								if (flag4)
								{
									array[j] = new ItemOption(num3, param);
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b2, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b3);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						bool flag5 = b3 > 0;
						if (flag5)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, (int)num);
					RadarScr.gI().switchToMe();
				}
				else
				{
					bool flag6 = b == 1;
					if (flag6)
					{
						int id2 = (int)msg.reader().readShort();
						sbyte use2 = msg.reader().readByte();
						bool flag7 = Info_RadaScr.GetInfo(RadarScr.list, id2) != null;
						if (flag7)
						{
							Info_RadaScr.GetInfo(RadarScr.list, id2).SetUse(use2);
						}
						RadarScr.SetListUse();
					}
					else
					{
						bool flag8 = b == 2;
						if (flag8)
						{
							int num4 = (int)msg.reader().readShort();
							sbyte level = msg.reader().readByte();
							int num5 = 0;
							for (int k = 0; k < RadarScr.list.size(); k++)
							{
								Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
								bool flag9 = info_RadaScr2 != null;
								if (flag9)
								{
									bool flag10 = info_RadaScr2.id == num4;
									if (flag10)
									{
										info_RadaScr2.SetLevel(level);
									}
									bool flag11 = info_RadaScr2.level > 0;
									if (flag11)
									{
										num5++;
									}
								}
							}
							RadarScr.SetNum(num5, RadarScr.list.size());
							bool flag12 = Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null;
							if (flag12)
							{
								Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
							}
						}
						else
						{
							bool flag13 = b == 3;
							if (flag13)
							{
								int id3 = (int)msg.reader().readShort();
								sbyte amount2 = msg.reader().readByte();
								sbyte max_amount2 = msg.reader().readByte();
								bool flag14 = Info_RadaScr.GetInfo(RadarScr.list, id3) != null;
								if (flag14)
								{
									Info_RadaScr.GetInfo(RadarScr.list, id3).SetAmount(amount2, max_amount2);
								}
								bool flag15 = Info_RadaScr.GetInfo(RadarScr.listUse, id3) != null;
								if (flag15)
								{
									Info_RadaScr.GetInfo(RadarScr.listUse, id3).SetAmount(amount2, max_amount2);
								}
							}
							else
							{
								bool flag16 = b == 4;
								if (flag16)
								{
									int num6 = msg.reader().readInt();
									short idAuraEff = msg.reader().readShort();
									bool flag17 = num6 == global::Char.myCharz().charID;
									global::Char @char;
									if (flag17)
									{
										@char = global::Char.myCharz();
									}
									else
									{
										@char = GameScr.findCharInMap(num6);
									}
									bool flag18 = @char != null;
									if (flag18)
									{
										@char.idAuraEff = idAuraEff;
										@char.idEff_Set_Item = (short)msg.reader().readByte();
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000AF620 File Offset: 0x000AD820
		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				int num = msg.reader().readInt();
				bool flag = num == global::Char.myCharz().charID;
				global::Char @char;
				if (flag)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num);
				}
				bool flag2 = b == 0;
				if (flag2)
				{
					int id = (int)msg.reader().readShort();
					int layer = (int)msg.reader().readByte();
					int loop = (int)msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					bool flag3 = @char != null;
					if (flag3)
					{
						@char.addEffChar(new Effect(id, @char, layer, loop, (int)loopCount, isStand));
					}
				}
				else
				{
					bool flag4 = b == 1;
					if (flag4)
					{
						int id2 = (int)msg.reader().readShort();
						bool flag5 = @char != null;
						if (flag5)
						{
							@char.removeEffChar(0, id2);
						}
					}
					else
					{
						bool flag6 = b == 2;
						if (flag6)
						{
							bool flag7 = @char != null;
							if (flag7)
							{
								@char.removeEffChar(-1, 0);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000AF754 File Offset: 0x000AD954
		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				sbyte idBoss = msg.reader().readByte();
				NewBoss newBoss = Mob.getNewBoss(idBoss);
				bool flag = newBoss != null;
				if (flag)
				{
					bool flag2 = actionBoss == 10;
					if (flag2)
					{
						short xMoveTo = msg.reader().readShort();
						short yMoveTo = msg.reader().readShort();
						newBoss.move(xMoveTo, yMoveTo);
					}
					bool flag3 = actionBoss >= 11 && actionBoss <= 20;
					if (flag3)
					{
						sbyte b = msg.reader().readByte();
						global::Char[] array = new global::Char[(int)b];
						int[] array2 = new int[(int)b];
						for (int i = 0; i < (int)b; i++)
						{
							int num = msg.reader().readInt();
							array[i] = null;
							bool flag4 = num != global::Char.myCharz().charID;
							if (flag4)
							{
								array[i] = GameScr.findCharInMap(num);
							}
							else
							{
								array[i] = global::Char.myCharz();
							}
							array2[i] = msg.reader().readInt();
						}
						sbyte dir = msg.reader().readByte();
						newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
					}
					bool flag5 = actionBoss == 21;
					if (flag5)
					{
						newBoss.xTo = (int)msg.reader().readShort();
						newBoss.yTo = (int)msg.reader().readShort();
						newBoss.setFly();
					}
					bool flag6 = actionBoss == 22;
					if (flag6)
					{
					}
					bool flag7 = actionBoss == 23;
					if (flag7)
					{
						newBoss.setDie();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
	}
}
