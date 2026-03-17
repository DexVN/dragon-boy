using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class Controller : IMessageHandler
{
	// Token: 0x0600018A RID: 394 RVA: 0x0001D7DC File Offset: 0x0001B9DC
	public static Controller gI()
	{
		bool flag = Controller.me == null;
		if (flag)
		{
			Controller.me = new Controller();
		}
		return Controller.me;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0001D80C File Offset: 0x0001BA0C
	public static Controller gI2()
	{
		bool flag = Controller.me2 == null;
		if (flag)
		{
			Controller.me2 = new Controller();
		}
		return Controller.me2;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0001D83B File Offset: 0x0001BA3B
	public void onConnectOK(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectOK();
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0001D84A File Offset: 0x0001BA4A
	public void onConnectionFail(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectionFail();
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0001D859 File Offset: 0x0001BA59
	public void onDisconnected(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onDisconnected();
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0001D868 File Offset: 0x0001BA68
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

	// Token: 0x06000190 RID: 400 RVA: 0x0001D940 File Offset: 0x0001BB40
	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			bool flag18 = msg.command != -74;
			if (flag18)
			{
				Res.outz("=========> [READ] cmd= " + msg.command.ToString());
			}
			global::Char @char = null;
			MyVector myVector = new MyVector();
			int i = 0;
			GameCanvas.timeLoading = 15;
			Controller2.readMessage(msg);
			sbyte command = msg.command;
			int num355 = (int)(command + 99);
			int num356 = num355;
			if (num356 <= 105)
			{
				if (num356 <= 85)
				{
					switch (num356)
					{
					case -128:
						GameCanvas.debug("SA58", 2);
						GameScr.gI().openUIZone(msg);
						goto IL_B603;
					case -127:
					case -126:
					case -123:
					case -122:
					case -121:
					case -120:
					case -115:
					case -113:
					case -112:
					case -109:
					case -108:
					case -106:
					case -105:
					case -104:
					case -102:
					case -98:
					case -97:
					case -96:
					case -90:
					case -87:
					case -86:
					case -85:
					case -84:
					case -83:
					case -82:
					case -81:
					case -80:
					case -77:
					case -68:
					case -66:
					case -64:
						break;
					case -125:
					{
						GameCanvas.debug("SA68", 2);
						int num149 = (int)msg.reader().readShort();
						int num357;
						for (int num150 = 0; num150 < GameScr.vNpc.size(); num150 = num357 + 1)
						{
							Npc npc2 = (Npc)GameScr.vNpc.elementAt(num150);
							bool flag19 = npc2.template.npcTemplateId == num149 && npc2.Equals(global::Char.myCharz().npcFocus);
							if (flag19)
							{
								string chat2 = msg.reader().readUTF();
								string[] array11 = new string[(int)msg.reader().readByte()];
								for (int num151 = 0; num151 < array11.Length; num151 = num357 + 1)
								{
									array11[num151] = msg.reader().readUTF();
									num357 = num151;
								}
								GameScr.gI().createMenu(array11, npc2);
								ChatPopup.addChatPopup(chat2, 100000, npc2);
								return;
							}
							num357 = num150;
						}
						Npc npc3 = new Npc(num149, 0, -100, 100, num149, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
						Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
						string chat3 = msg.reader().readUTF();
						string[] array12 = new string[(int)msg.reader().readByte()];
						for (int num152 = 0; num152 < array12.Length; num152 = num357 + 1)
						{
							array12[num152] = msg.reader().readUTF();
							num357 = num152;
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
						goto IL_B603;
					}
					case -124:
					{
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
						bool flag20 = global::Char.myCharz().npcFocus == null;
						if (flag20)
						{
							return;
						}
						int num357;
						for (int num153 = 0; num153 < global::Char.myCharz().npcFocus.template.menu.Length; num153 = num357 + 1)
						{
							string[] array13 = global::Char.myCharz().npcFocus.template.menu[num153];
							myVector.addElement(new Command(array13[0], GameCanvas.instance, 88820, array13));
							num357 = num153;
						}
						GameCanvas.menu.startAt(myVector, 3);
						goto IL_B603;
					}
					case -119:
					{
						GameCanvas.debug("SA67", 2);
						InfoDlg.hide();
						int num154 = (int)msg.reader().readShort();
						Res.outz("OPEN_UI_SAY ID= " + num154.ToString());
						string text6 = msg.reader().readUTF();
						text6 = Res.changeString(text6);
						int num357;
						for (int num155 = 0; num155 < GameScr.vNpc.size(); num155 = num357 + 1)
						{
							Npc npc4 = (Npc)GameScr.vNpc.elementAt(num155);
							Res.outz("npc id= " + npc4.template.npcTemplateId.ToString());
							bool flag21 = npc4.template.npcTemplateId == num154;
							if (flag21)
							{
								ChatPopup.addChatPopupMultiLine(text6, 100000, npc4);
								GameCanvas.panel.hideNow();
								return;
							}
							num357 = num155;
						}
						Npc npc5 = new Npc(num154, 0, 0, 0, num154, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
						bool flag22 = npc5.template.npcTemplateId == 5;
						if (flag22)
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
						goto IL_B603;
					}
					case -118:
					{
						GameCanvas.debug("SA49", 2);
						GameScr.gI().typeTradeOrder = 2;
						bool flag23 = GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2;
						if (flag23)
						{
							InfoDlg.showWait();
						}
						goto IL_B603;
					}
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
						int num357;
						for (int num156 = 0; num156 < array14.Length; num156 = num357 + 1)
						{
							string text9 = msg.reader().readUTF();
							text9 = Res.changeString(text9);
							GameScr.tasks[num156] = (int)msg.reader().readByte();
							GameScr.mapTasks[num156] = (int)msg.reader().readShort();
							string text10 = msg.reader().readUTF();
							text10 = Res.changeString(text10);
							array16[num156] = -1;
							bool flag24 = !text9.Equals(string.Empty);
							if (flag24)
							{
								array14[num156] = text9;
								array15[num156] = text10;
							}
							num357 = num156;
						}
						try
						{
							count = msg.reader().readShort();
							for (int num157 = 0; num157 < array14.Length; num157 = num357 + 1)
							{
								array16[num157] = msg.reader().readShort();
								num357 = num157;
							}
						}
						catch (Exception ex19)
						{
							Cout.println("Loi TASK_GET " + ex19.ToString());
						}
						global::Char.myCharz().taskMaint = new Task(taskId, index3, text7, text8, array14, array16, count, array15);
						bool flag25 = global::Char.myCharz().npcFocus != null;
						if (flag25)
						{
							Npc.clearEffTask();
						}
						global::Char.taskAction(false);
						goto IL_B603;
					}
					case -116:
					{
						GameCanvas.debug("SA53", 2);
						GameCanvas.taskTick = 100;
						Res.outz("TASK NEXT");
						Task taskMaint = global::Char.myCharz().taskMaint;
						Task task = taskMaint;
						int num357 = taskMaint.index;
						task.index = num357 + 1;
						global::Char.myCharz().taskMaint.count = 0;
						Npc.clearEffTask();
						global::Char.taskAction(true);
						goto IL_B603;
					}
					case -114:
					{
						GameCanvas.taskTick = 50;
						GameCanvas.debug("SA55", 2);
						global::Char.myCharz().taskMaint.count = msg.reader().readShort();
						bool flag26 = global::Char.myCharz().npcFocus != null;
						if (flag26)
						{
							Npc.clearEffTask();
						}
						try
						{
							short num158 = msg.reader().readShort();
							short num159 = msg.reader().readShort();
							global::Char.myCharz().x_hint = num158;
							global::Char.myCharz().y_hint = num159;
							Res.outz(string.Concat(new object[]
							{
								"CMD   TASK_UPDATE:43_mapID =    x|y ",
								num158,
								"|",
								num159
							}));
							int num357;
							for (int num160 = 0; num160 < TileMap.vGo.size(); num160 = num357 + 1)
							{
								string str = "===> ";
								object obj = TileMap.vGo.elementAt(num160);
								Res.outz(str + ((obj != null) ? obj.ToString() : null));
								num357 = num160;
							}
						}
						catch (Exception ex20)
						{
						}
						goto IL_B603;
					}
					case -111:
						GameCanvas.debug("SA5", 2);
						Cout.LogWarning("Controler RESET_POINT  " + global::Char.ischangingMap.ToString());
						global::Char.isLockKey = false;
						global::Char.myCharz().setResetPoint((int)msg.reader().readShort(), (int)msg.reader().readShort());
						goto IL_B603;
					case -110:
						GameCanvas.debug("SA4", 2);
						GameScr.gI().resetButton();
						goto IL_B603;
					case -107:
					{
						sbyte b63 = msg.reader().readByte();
						Panel.vGameInfo.removeAllElements();
						int num357;
						for (int num161 = 0; num161 < (int)b63; num161 = num357 + 1)
						{
							GameInfo gameInfo = new GameInfo();
							gameInfo.id = msg.reader().readShort();
							gameInfo.main = msg.reader().readUTF();
							gameInfo.content = msg.reader().readUTF();
							Panel.vGameInfo.addElement(gameInfo);
							bool hasRead = Rms.loadRMSInt(gameInfo.id.ToString() + string.Empty) != -1;
							gameInfo.hasRead = hasRead;
							num357 = num161;
						}
						goto IL_B603;
					}
					case -103:
					{
						@char = GameScr.findCharInMap(msg.reader().readInt());
						bool flag27 = @char == null;
						if (flag27)
						{
							return;
						}
						int num162 = (int)msg.reader().readUnsignedByte();
						bool flag28 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
						if (flag28)
						{
							@char.setSkillPaint(GameScr.sks[num162], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[num162], 1);
						}
						Mob[] array17 = new Mob[10];
						i = 0;
						try
						{
							int num357;
							for (i = 0; i < array17.Length; i = num357 + 1)
							{
								Mob mob6 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
								array17[i] = mob6;
								bool flag29 = i == 0;
								if (flag29)
								{
									bool flag30 = @char.cx <= mob6.x;
									if (flag30)
									{
										@char.cdir = 1;
									}
									else
									{
										@char.cdir = -1;
									}
								}
								num357 = i;
							}
						}
						catch (Exception ex21)
						{
						}
						bool flag31 = i > 0;
						if (flag31)
						{
							@char.attMobs = new Mob[i];
							int num357;
							for (i = 0; i < @char.attMobs.Length; i = num357 + 1)
							{
								@char.attMobs[i] = array17[i];
								num357 = i;
							}
							@char.charFocus = null;
							@char.mobFocus = @char.attMobs[0];
						}
						goto IL_B603;
					}
					case -101:
					{
						GameCanvas.debug("SXX6", 2);
						@char = null;
						int num163 = msg.reader().readInt();
						bool flag32 = num163 == global::Char.myCharz().charID;
						if (flag32)
						{
							bool flag7 = false;
							@char = global::Char.myCharz();
							@char.cHP = msg.readInt3Byte();
							int num164 = msg.readInt3Byte();
							Res.outz("dame hit = " + num164.ToString());
							bool flag33 = num164 != 0;
							if (flag33)
							{
								@char.doInjure();
							}
							int num165 = 0;
							try
							{
								flag7 = msg.reader().readBoolean();
								sbyte b64 = msg.reader().readByte();
								bool flag34 = b64 != -1;
								if (flag34)
								{
									Res.outz("hit eff= " + b64.ToString());
									EffecMn.addEff(new Effect((int)b64, @char.cx, @char.cy, 3, 1, -1));
								}
							}
							catch (Exception ex22)
							{
							}
							num164 += num165;
							bool flag35 = global::Char.myCharz().cTypePk != 4;
							if (flag35)
							{
								bool flag36 = num164 == 0;
								if (flag36)
								{
									GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
								}
								else
								{
									GameScr.startFlyText("-" + num164.ToString(), @char.cx, @char.cy - @char.ch, 0, -3, flag7 ? mFont.FATAL : mFont.RED);
								}
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num163);
							bool flag37 = @char == null;
							if (flag37)
							{
								return;
							}
							@char.cHP = msg.readInt3Byte();
							bool flag8 = false;
							int num166 = msg.readInt3Byte();
							bool flag38 = num166 != 0;
							if (flag38)
							{
								@char.doInjure();
							}
							int num167 = 0;
							try
							{
								flag8 = msg.reader().readBoolean();
								sbyte b65 = msg.reader().readByte();
								bool flag39 = b65 != -1;
								if (flag39)
								{
									Res.outz("hit eff= " + b65.ToString());
									EffecMn.addEff(new Effect((int)b65, @char.cx, @char.cy, 3, 1, -1));
								}
							}
							catch (Exception ex23)
							{
							}
							num166 += num167;
							bool flag40 = @char.cTypePk != 4;
							if (flag40)
							{
								bool flag41 = num166 == 0;
								if (flag41)
								{
									GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
								}
								else
								{
									GameScr.startFlyText("-" + num166.ToString(), @char.cx, @char.cy - @char.ch, 0, -3, flag8 ? mFont.FATAL : mFont.ORANGE);
								}
							}
						}
						goto IL_B603;
					}
					case -100:
					{
						GameCanvas.debug("SZ6", 2);
						MyVector myVector2 = new MyVector();
						myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
						GameCanvas.menu.startAt(myVector2, 3);
						goto IL_B603;
					}
					case -99:
					{
						GameCanvas.debug("SZ7", 2);
						int num168 = msg.reader().readInt();
						bool flag42 = num168 == global::Char.myCharz().charID;
						global::Char char2;
						if (flag42)
						{
							char2 = global::Char.myCharz();
						}
						else
						{
							char2 = GameScr.findCharInMap(num168);
						}
						char2.moveFast = new short[3];
						char2.moveFast[0] = 0;
						short num169 = msg.reader().readShort();
						short num170 = msg.reader().readShort();
						char2.moveFast[1] = num169;
						char2.moveFast[2] = num170;
						try
						{
							num168 = msg.reader().readInt();
							bool flag43 = num168 == global::Char.myCharz().charID;
							global::Char char3;
							if (flag43)
							{
								char3 = global::Char.myCharz();
							}
							else
							{
								char3 = GameScr.findCharInMap(num168);
							}
							char3.cx = (int)num169;
							char3.cy = (int)num170;
						}
						catch (Exception ex24)
						{
							Cout.println("Loi MOVE_FAST " + ex24.ToString());
						}
						goto IL_B603;
					}
					case -95:
					{
						GameCanvas.debug("SZ3", 2);
						@char = GameScr.findCharInMap(msg.reader().readInt());
						bool flag44 = @char != null;
						if (flag44)
						{
							@char.killCharId = global::Char.myCharz().charID;
							global::Char.myCharz().npcFocus = null;
							global::Char.myCharz().mobFocus = null;
							global::Char.myCharz().itemFocus = null;
							global::Char.myCharz().charFocus = @char;
							global::Char.isManualFocus = true;
							GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
						}
						goto IL_B603;
					}
					case -94:
						GameCanvas.debug("SZ4", 2);
						global::Char.myCharz().killCharId = msg.reader().readInt();
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().mobFocus = null;
						global::Char.myCharz().itemFocus = null;
						global::Char.myCharz().charFocus = GameScr.findCharInMap(global::Char.myCharz().killCharId);
						global::Char.isManualFocus = true;
						goto IL_B603;
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
						goto IL_B603;
					case -92:
					{
						sbyte b66 = msg.reader().readSByte();
						string text11 = msg.reader().readUTF();
						short num171 = msg.reader().readShort();
						bool flag45 = ItemTime.isExistMessage((int)b66);
						if (flag45)
						{
							bool flag46 = num171 != 0;
							if (flag46)
							{
								ItemTime.getMessageById((int)b66).initTimeText(b66, text11, (int)num171);
							}
							else
							{
								GameScr.textTime.removeElement(ItemTime.getMessageById((int)b66));
							}
						}
						else
						{
							ItemTime itemTime = new ItemTime();
							itemTime.initTimeText(b66, text11, (int)num171);
							GameScr.textTime.addElement(itemTime);
						}
						goto IL_B603;
					}
					case -91:
						this.readGetImgByName(msg);
						goto IL_B603;
					case -89:
					{
						Res.outz("ADD ITEM TO MAP --------------------------------------");
						GameCanvas.debug("SA6333", 2);
						short num172 = msg.reader().readShort();
						short itemTemplateID = msg.reader().readShort();
						int x = (int)msg.reader().readShort();
						int y = (int)msg.reader().readShort();
						int num173 = msg.reader().readInt();
						short r = 0;
						bool flag47 = num173 == -2;
						if (flag47)
						{
							r = msg.reader().readShort();
						}
						ItemMap itemMap3 = new ItemMap(num173, num172, itemTemplateID, x, y, r);
						bool flag9 = false;
						int num357;
						for (int num174 = 0; num174 < GameScr.vItemMap.size(); num174 = num357 + 1)
						{
							ItemMap itemMap4 = (ItemMap)GameScr.vItemMap.elementAt(num174);
							bool flag48 = itemMap4.itemMapID == itemMap3.itemMapID;
							if (flag48)
							{
								flag9 = true;
								break;
							}
							num357 = num174;
						}
						bool flag49 = !flag9;
						if (flag49)
						{
							GameScr.vItemMap.addElement(itemMap3);
						}
						goto IL_B603;
					}
					case -88:
						SoundMn.IsDelAcc = (msg.reader().readByte() != 0);
						goto IL_B603;
					case -79:
						goto IL_B603;
					case -78:
						goto IL_B603;
					case -76:
					{
						GameCanvas.debug("SXX4", 2);
						Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						mob7.isDisable = msg.reader().readBool();
						goto IL_B603;
					}
					case -75:
					{
						GameCanvas.debug("SXX5", 2);
						Mob mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						mob8.isDontMove = msg.reader().readBool();
						goto IL_B603;
					}
					case -74:
					{
						GameCanvas.debug("SXX8", 2);
						int num175 = msg.reader().readInt();
						bool flag50 = num175 == global::Char.myCharz().charID;
						if (flag50)
						{
							@char = global::Char.myCharz();
						}
						else
						{
							@char = GameScr.findCharInMap(num175);
						}
						bool flag51 = @char == null;
						if (flag51)
						{
							return;
						}
						Mob mobToAttack = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						bool flag52 = @char.mobMe != null;
						if (flag52)
						{
							@char.mobMe.attackOtherMob(mobToAttack);
						}
						goto IL_B603;
					}
					case -73:
					{
						int num176 = msg.reader().readInt();
						bool flag53 = num176 == global::Char.myCharz().charID;
						if (flag53)
						{
							@char = global::Char.myCharz();
						}
						else
						{
							@char = GameScr.findCharInMap(num176);
							bool flag54 = @char == null;
							if (flag54)
							{
								return;
							}
						}
						@char.cHP = @char.cHPFull;
						@char.cMP = @char.cMPFull;
						@char.cx = (int)msg.reader().readShort();
						@char.cy = (int)msg.reader().readShort();
						@char.liveFromDead();
						goto IL_B603;
					}
					case -72:
					{
						GameCanvas.debug("SXX5", 2);
						Mob mob9 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						mob9.isFire = msg.reader().readBool();
						goto IL_B603;
					}
					case -71:
					{
						GameCanvas.debug("SXX5", 2);
						Mob mob10 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						mob10.isIce = msg.reader().readBool();
						bool flag55 = !mob10.isIce;
						if (flag55)
						{
							ServerEffect.addServerEffect(77, mob10.x, mob10.y - 9, 1);
						}
						goto IL_B603;
					}
					case -70:
					{
						GameCanvas.debug("SXX5", 2);
						Mob mob11 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						mob11.isWind = msg.reader().readBool();
						goto IL_B603;
					}
					case -69:
					{
						string info4 = msg.reader().readUTF();
						short num177 = msg.reader().readShort();
						GameCanvas.inputDlg.show(info4, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num177), TField.INPUT_TYPE_ANY);
						goto IL_B603;
					}
					case -67:
						GameCanvas.debug("SA577", 2);
						this.requestItemPlayer(msg);
						goto IL_B603;
					case -65:
					{
						bool flag56 = GameCanvas.currentScreen == GameScr.instance;
						if (flag56)
						{
							GameCanvas.endDlg();
						}
						string text12 = msg.reader().readUTF();
						string text13 = msg.reader().readUTF();
						text13 = Res.changeString(text13);
						string text14 = string.Empty;
						global::Char char4 = null;
						sbyte b67 = 0;
						bool flag57 = !text12.Equals(string.Empty);
						if (flag57)
						{
							char4 = new global::Char();
							char4.charID = msg.reader().readInt();
							char4.head = (int)msg.reader().readShort();
							char4.headICON = (int)msg.reader().readShort();
							char4.body = (int)msg.reader().readShort();
							char4.bag = (int)msg.reader().readShort();
							char4.leg = (int)msg.reader().readShort();
							b67 = msg.reader().readByte();
							char4.cName = text12;
						}
						text14 += text13;
						InfoDlg.hide();
						bool flag58 = text12.Equals(string.Empty);
						if (flag58)
						{
							GameScr.info1.addInfo(text14, 0);
						}
						else
						{
							GameScr.info2.addInfoWithChar(text14, char4, b67 == 0);
							bool flag59 = GameCanvas.panel.isShow && GameCanvas.panel.type == 8;
							if (flag59)
							{
								GameCanvas.panel.initLogMessage();
							}
						}
						goto IL_B603;
					}
					case -63:
						GameCanvas.debug("SA3", 2);
						GameScr.info1.addInfo(msg.reader().readUTF(), 0);
						goto IL_B603;
					default:
						switch (num356)
						{
						case -45:
						{
							sbyte b68 = msg.reader().readByte();
							Res.outz("spec type= " + b68.ToString());
							bool flag60 = b68 == 0;
							if (flag60)
							{
								Panel.spearcialImage = msg.reader().readShort();
								Panel.specialInfo = msg.reader().readUTF();
							}
							else
							{
								bool flag61 = b68 == 1;
								if (flag61)
								{
									sbyte b69 = msg.reader().readByte();
									global::Char.myCharz().infoSpeacialSkill = new string[(int)b69][];
									global::Char.myCharz().imgSpeacialSkill = new short[(int)b69][];
									GameCanvas.panel.speacialTabName = new string[(int)b69][];
									int num357;
									for (int num178 = 0; num178 < (int)b69; num178 = num357 + 1)
									{
										GameCanvas.panel.speacialTabName[num178] = new string[2];
										string[] array18 = Res.split(msg.reader().readUTF(), "\n", 0);
										bool flag62 = array18.Length == 2;
										if (flag62)
										{
											GameCanvas.panel.speacialTabName[num178] = array18;
										}
										bool flag63 = array18.Length == 1;
										if (flag63)
										{
											GameCanvas.panel.speacialTabName[num178][0] = array18[0];
											GameCanvas.panel.speacialTabName[num178][1] = string.Empty;
										}
										int num179 = (int)msg.reader().readByte();
										global::Char.myCharz().infoSpeacialSkill[num178] = new string[num179];
										global::Char.myCharz().imgSpeacialSkill[num178] = new short[num179];
										for (int num180 = 0; num180 < num179; num180 = num357 + 1)
										{
											global::Char.myCharz().imgSpeacialSkill[num178][num180] = msg.reader().readShort();
											global::Char.myCharz().infoSpeacialSkill[num178][num180] = msg.reader().readUTF();
											num357 = num180;
										}
										num357 = num178;
									}
									GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
									GameCanvas.panel.setTypeSpeacialSkill();
									GameCanvas.panel.show();
								}
							}
							goto IL_B603;
						}
						case 0:
						{
							InfoDlg.hide();
							sbyte b70 = msg.reader().readByte();
							bool flag64 = b70 == 0;
							if (flag64)
							{
								GameCanvas.panel.vEnemy.removeAllElements();
								int num181 = (int)msg.reader().readUnsignedByte();
								int num357;
								for (int j = 0; j < num181; j = num357 + 1)
								{
									global::Char char5 = new global::Char();
									char5.charID = msg.reader().readInt();
									char5.head = (int)msg.reader().readShort();
									char5.headICON = (int)msg.reader().readShort();
									char5.body = (int)msg.reader().readShort();
									char5.leg = (int)msg.reader().readShort();
									char5.bag = (int)msg.reader().readShort();
									char5.cName = msg.reader().readUTF();
									InfoItem infoItem = new InfoItem(msg.reader().readUTF());
									bool flag10 = msg.reader().readBoolean();
									infoItem.charInfo = char5;
									infoItem.isOnline = flag10;
									Res.outz("isonline = " + flag10.ToString());
									GameCanvas.panel.vEnemy.addElement(infoItem);
									num357 = j;
								}
								GameCanvas.panel.setTypeEnemy();
								GameCanvas.panel.show();
							}
							goto IL_B603;
						}
						case 1:
						{
							sbyte b71 = msg.reader().readByte();
							GameCanvas.menu.showMenu = false;
							bool flag65 = b71 == 0;
							if (flag65)
							{
								GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
							}
							goto IL_B603;
						}
						case 2:
							global::Char.myCharz().cNangdong = (long)msg.reader().readInt();
							goto IL_B603;
						case 3:
						{
							sbyte typeTop = msg.reader().readByte();
							GameCanvas.panel.vTop.removeAllElements();
							string topName = msg.reader().readUTF();
							sbyte b72 = msg.reader().readByte();
							int num357;
							for (int k = 0; k < (int)b72; k = num357 + 1)
							{
								int rank = msg.reader().readInt();
								int pId = msg.reader().readInt();
								short headID = msg.reader().readShort();
								short headICON = msg.reader().readShort();
								short body = msg.reader().readShort();
								short leg = msg.reader().readShort();
								string name = msg.reader().readUTF();
								string info5 = msg.reader().readUTF();
								TopInfo topInfo = new TopInfo();
								topInfo.rank = rank;
								topInfo.headID = (int)headID;
								topInfo.headICON = (int)headICON;
								topInfo.body = body;
								topInfo.leg = leg;
								topInfo.name = name;
								topInfo.info = info5;
								topInfo.info2 = msg.reader().readUTF();
								topInfo.pId = pId;
								GameCanvas.panel.vTop.addElement(topInfo);
								num357 = k;
							}
							GameCanvas.panel.topName = topName;
							GameCanvas.panel.setTypeTop(typeTop);
							GameCanvas.panel.show();
							goto IL_B603;
						}
						case 4:
						{
							sbyte b73 = msg.reader().readByte();
							Res.outz("type= " + b73.ToString());
							bool flag66 = b73 == 0;
							if (flag66)
							{
								int num182 = msg.reader().readInt();
								short templateId = msg.reader().readShort();
								int num183 = msg.readInt3Byte();
								SoundMn.gI().explode_1();
								bool flag67 = num182 == global::Char.myCharz().charID;
								if (flag67)
								{
									global::Char.myCharz().mobMe = new Mob(num182, false, false, false, false, false, (int)templateId, 1, num183, 0, num183, (short)(global::Char.myCharz().cx + ((global::Char.myCharz().cdir != 1) ? -40 : 40)), (short)global::Char.myCharz().cy, 4, 0);
									global::Char.myCharz().mobMe.isMobMe = true;
									EffecMn.addEff(new Effect(18, global::Char.myCharz().mobMe.x, global::Char.myCharz().mobMe.y, 2, 10, -1));
									global::Char.myCharz().tMobMeBorn = 30;
									GameScr.vMob.addElement(global::Char.myCharz().mobMe);
								}
								else
								{
									@char = GameScr.findCharInMap(num182);
									bool flag68 = @char != null;
									if (flag68)
									{
										@char.mobMe = new Mob(num182, false, false, false, false, false, (int)templateId, 1, num183, 0, num183, (short)@char.cx, (short)@char.cy, 4, 0)
										{
											isMobMe = true
										};
										GameScr.vMob.addElement(@char.mobMe);
									}
									else
									{
										bool flag69 = GameScr.findMobInMap(num182) == null;
										if (flag69)
										{
											Mob mob12 = new Mob(num182, false, false, false, false, false, (int)templateId, 1, num183, 0, num183, -100, -100, 4, 0);
											mob12.isMobMe = true;
											GameScr.vMob.addElement(mob12);
										}
									}
								}
							}
							bool flag70 = b73 == 1;
							if (flag70)
							{
								int num184 = msg.reader().readInt();
								int mobId = (int)msg.reader().readByte();
								Res.outz("mod attack id= " + num184.ToString());
								bool flag71 = num184 == global::Char.myCharz().charID;
								if (flag71)
								{
									bool flag72 = GameScr.findMobInMap(mobId) != null;
									if (flag72)
									{
										global::Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
									}
								}
								else
								{
									@char = GameScr.findCharInMap(num184);
									bool flag73 = @char != null && GameScr.findMobInMap(mobId) != null;
									if (flag73)
									{
										@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
									}
								}
							}
							bool flag74 = b73 == 2;
							if (flag74)
							{
								int num185 = msg.reader().readInt();
								int num186 = msg.reader().readInt();
								int num187 = msg.readInt3Byte();
								int cHPNew = msg.readInt3Byte();
								bool flag75 = num185 == global::Char.myCharz().charID;
								if (flag75)
								{
									Res.outz("mob dame= " + num187.ToString());
									@char = GameScr.findCharInMap(num186);
									bool flag76 = @char != null;
									if (flag76)
									{
										@char.cHPNew = cHPNew;
										bool isBusyAttackSomeOne = global::Char.myCharz().mobMe.isBusyAttackSomeOne;
										if (isBusyAttackSomeOne)
										{
											@char.doInjure(num187, 0, false, true);
										}
										else
										{
											global::Char.myCharz().mobMe.dame = num187;
											global::Char.myCharz().mobMe.setAttack(@char);
										}
									}
								}
								else
								{
									Mob mob13 = GameScr.findMobInMap(num185);
									bool flag77 = mob13 != null;
									if (flag77)
									{
										bool flag78 = num186 == global::Char.myCharz().charID;
										if (flag78)
										{
											global::Char.myCharz().cHPNew = cHPNew;
											bool isBusyAttackSomeOne2 = mob13.isBusyAttackSomeOne;
											if (isBusyAttackSomeOne2)
											{
												global::Char.myCharz().doInjure(num187, 0, false, true);
											}
											else
											{
												mob13.dame = num187;
												mob13.setAttack(global::Char.myCharz());
											}
										}
										else
										{
											@char = GameScr.findCharInMap(num186);
											bool flag79 = @char != null;
											if (flag79)
											{
												@char.cHPNew = cHPNew;
												bool isBusyAttackSomeOne3 = mob13.isBusyAttackSomeOne;
												if (isBusyAttackSomeOne3)
												{
													@char.doInjure(num187, 0, false, true);
												}
												else
												{
													mob13.dame = num187;
													mob13.setAttack(@char);
												}
											}
										}
									}
								}
							}
							bool flag80 = b73 == 3;
							if (flag80)
							{
								int num188 = msg.reader().readInt();
								int mobId2 = msg.reader().readInt();
								int hp = msg.readInt3Byte();
								int num189 = msg.readInt3Byte();
								@char = null;
								bool flag81 = global::Char.myCharz().charID == num188;
								if (flag81)
								{
									@char = global::Char.myCharz();
								}
								else
								{
									@char = GameScr.findCharInMap(num188);
								}
								bool flag82 = @char != null;
								if (flag82)
								{
									Mob mob14 = GameScr.findMobInMap(mobId2);
									bool flag83 = @char.mobMe != null;
									if (flag83)
									{
										@char.mobMe.attackOtherMob(mob14);
									}
									bool flag84 = mob14 != null;
									if (flag84)
									{
										mob14.hp = hp;
										mob14.updateHp_bar();
										bool flag85 = num189 == 0;
										if (flag85)
										{
											mob14.x = mob14.xFirst;
											mob14.y = mob14.yFirst;
											GameScr.startFlyText(mResources.miss, mob14.x, mob14.y - mob14.h, 0, -2, mFont.MISS);
										}
										else
										{
											GameScr.startFlyText("-" + num189.ToString(), mob14.x, mob14.y - mob14.h, 0, -2, mFont.ORANGE);
										}
									}
								}
							}
							bool flag86 = b73 == 4;
							if (flag86)
							{
							}
							bool flag87 = b73 == 5;
							if (flag87)
							{
								int num190 = msg.reader().readInt();
								sbyte b74 = msg.reader().readByte();
								int mobId3 = msg.reader().readInt();
								int num191 = msg.readInt3Byte();
								int hp2 = msg.readInt3Byte();
								@char = null;
								bool flag88 = num190 == global::Char.myCharz().charID;
								if (flag88)
								{
									@char = global::Char.myCharz();
								}
								else
								{
									@char = GameScr.findCharInMap(num190);
								}
								bool flag89 = @char == null;
								if (flag89)
								{
									return;
								}
								bool flag90 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
								if (flag90)
								{
									@char.setSkillPaint(GameScr.sks[(int)b74], 0);
								}
								else
								{
									@char.setSkillPaint(GameScr.sks[(int)b74], 1);
								}
								Mob mob15 = GameScr.findMobInMap(mobId3);
								bool flag91 = @char.cx <= mob15.x;
								if (flag91)
								{
									@char.cdir = 1;
								}
								else
								{
									@char.cdir = -1;
								}
								@char.mobFocus = mob15;
								mob15.hp = hp2;
								mob15.updateHp_bar();
								GameCanvas.debug("SA83v2", 2);
								bool flag92 = num191 == 0;
								if (flag92)
								{
									mob15.x = mob15.xFirst;
									mob15.y = mob15.yFirst;
									GameScr.startFlyText(mResources.miss, mob15.x, mob15.y - mob15.h, 0, -2, mFont.MISS);
								}
								else
								{
									GameScr.startFlyText("-" + num191.ToString(), mob15.x, mob15.y - mob15.h, 0, -2, mFont.ORANGE);
								}
							}
							bool flag93 = b73 == 6;
							if (flag93)
							{
								int num192 = msg.reader().readInt();
								bool flag94 = num192 == global::Char.myCharz().charID;
								if (flag94)
								{
									global::Char.myCharz().mobMe.startDie();
								}
								else
								{
									@char = GameScr.findCharInMap(num192);
									bool flag95 = @char != null;
									if (flag95)
									{
										@char.mobMe.startDie();
									}
								}
							}
							bool flag96 = b73 == 7;
							if (flag96)
							{
								int num193 = msg.reader().readInt();
								bool flag97 = num193 == global::Char.myCharz().charID;
								if (flag97)
								{
									global::Char.myCharz().mobMe = null;
									int num357;
									for (int l = 0; l < GameScr.vMob.size(); l = num357 + 1)
									{
										bool flag98 = ((Mob)GameScr.vMob.elementAt(l)).mobId == num193;
										if (flag98)
										{
											GameScr.vMob.removeElementAt(l);
										}
										num357 = l;
									}
								}
								else
								{
									@char = GameScr.findCharInMap(num193);
									int num357;
									for (int m = 0; m < GameScr.vMob.size(); m = num357 + 1)
									{
										bool flag99 = ((Mob)GameScr.vMob.elementAt(m)).mobId == num193;
										if (flag99)
										{
											GameScr.vMob.removeElementAt(m);
										}
										num357 = m;
									}
									bool flag100 = @char != null;
									if (flag100)
									{
										@char.mobMe = null;
									}
								}
							}
							goto IL_B603;
						}
						case 5:
							while (msg.reader().available() > 0)
							{
								short num194 = msg.reader().readShort();
								int num195 = msg.reader().readInt();
								int num357;
								for (int n = 0; n < global::Char.myCharz().vSkill.size(); n = num357 + 1)
								{
									Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(n);
									bool flag101 = skill != null && skill.skillId == num194;
									if (flag101)
									{
										bool flag102 = num195 < skill.coolDown;
										if (flag102)
										{
											skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num195);
										}
										Res.outz(string.Concat(new object[]
										{
											"1 chieu id= ",
											skill.template.id,
											" cooldown= ",
											num195,
											"curr cool down= ",
											skill.coolDown
										}));
									}
									num357 = n;
								}
							}
							goto IL_B603;
						case 6:
						{
							short num196 = msg.reader().readShort();
							BgItem.newSmallVersion = new sbyte[(int)num196];
							int num357;
							for (int num197 = 0; num197 < (int)num196; num197 = num357 + 1)
							{
								BgItem.newSmallVersion[num197] = msg.reader().readByte();
								num357 = num197;
							}
							goto IL_B603;
						}
						case 7:
						{
							Main.typeClient = (int)msg.reader().readByte();
							bool flag103 = Rms.loadRMSString("ResVersion") == null;
							if (flag103)
							{
								Rms.clearAll();
							}
							Rms.saveRMSInt("clienttype", Main.typeClient);
							Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
							bool flag104 = Rms.loadRMSString("ResVersion") == null;
							if (flag104)
							{
								GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
							}
							goto IL_B603;
						}
						case 8:
						{
							sbyte b75 = msg.reader().readByte();
							GameCanvas.panel.mapNames = new string[(int)b75];
							GameCanvas.panel.planetNames = new string[(int)b75];
							int num357;
							for (int num198 = 0; num198 < (int)b75; num198 = num357 + 1)
							{
								GameCanvas.panel.mapNames[num198] = msg.reader().readUTF();
								GameCanvas.panel.planetNames[num198] = msg.reader().readUTF();
								num357 = num198;
							}
							GameCanvas.panel.setTypeMapTrans();
							GameCanvas.panel.show();
							goto IL_B603;
						}
						case 9:
						{
							sbyte b76 = msg.reader().readByte();
							int num199 = msg.reader().readInt();
							Res.outz("===> UPDATE_BODY:    type = " + b76.ToString());
							@char = ((global::Char.myCharz().charID != num199) ? GameScr.findCharInMap(num199) : global::Char.myCharz());
							bool flag105 = b76 != -1;
							if (flag105)
							{
								short num200 = msg.reader().readShort();
								short num201 = msg.reader().readShort();
								short num202 = msg.reader().readShort();
								sbyte isMonkey = msg.reader().readByte();
								bool flag106 = @char != null;
								if (flag106)
								{
									bool flag107 = @char.charID == num199;
									if (flag107)
									{
										@char.isMask = true;
										@char.isMonkey = isMonkey;
										bool flag108 = @char.isMonkey != 0;
										if (flag108)
										{
											@char.isWaitMonkey = false;
											@char.isLockMove = false;
										}
									}
									else
									{
										bool flag109 = @char != null;
										if (flag109)
										{
											@char.isMask = true;
											@char.isMonkey = isMonkey;
										}
									}
									bool flag110 = num200 != -1;
									if (flag110)
									{
										@char.head = (int)num200;
									}
									bool flag111 = num201 != -1;
									if (flag111)
									{
										@char.body = (int)num201;
									}
									bool flag112 = num202 != -1;
									if (flag112)
									{
										@char.leg = (int)num202;
									}
								}
							}
							bool flag113 = b76 == -1 && @char != null;
							if (flag113)
							{
								@char.isMask = false;
								@char.isMonkey = 0;
							}
							bool flag114 = @char != null;
							if (flag114)
							{
							}
							goto IL_B603;
						}
						case 11:
							GameCanvas.endDlg();
							GameCanvas.serverScreen.switchToMe();
							goto IL_B603;
						case 12:
						{
							Res.outz("GET UPDATE_DATA " + msg.reader().available().ToString() + " bytes");
							msg.reader().mark(100000);
							this.createData(msg.reader(), true);
							msg.reader().reset();
							sbyte[] array19 = new sbyte[msg.reader().available()];
							msg.reader().readFully(ref array19);
							sbyte[] data = new sbyte[]
							{
								GameScr.vcData
							};
							Rms.saveRMS("NRdataVersion", data);
							LoginScr.isUpdateData = false;
							bool flag115 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
							if (flag115)
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
							goto IL_B603;
						}
						case 13:
						{
							sbyte b77 = msg.reader().readByte();
							Res.outz("server gui ve giao dich action = " + b77.ToString());
							bool flag116 = b77 == 0;
							if (flag116)
							{
								int playerID = msg.reader().readInt();
								GameScr.gI().giaodich(playerID);
							}
							bool flag117 = b77 == 1;
							if (flag117)
							{
								int num203 = msg.reader().readInt();
								global::Char char6 = GameScr.findCharInMap(num203);
								bool flag118 = char6 == null;
								if (flag118)
								{
									return;
								}
								GameCanvas.panel.setTypeGiaoDich(char6);
								GameCanvas.panel.show();
								Service.gI().getPlayerMenu(num203);
							}
							bool flag119 = b77 == 2;
							if (flag119)
							{
								sbyte b78 = msg.reader().readByte();
								int num357;
								for (int num204 = 0; num204 < GameCanvas.panel.vMyGD.size(); num204 = num357 + 1)
								{
									Item item = (Item)GameCanvas.panel.vMyGD.elementAt(num204);
									bool flag120 = item.indexUI == (int)b78;
									if (flag120)
									{
										GameCanvas.panel.vMyGD.removeElement(item);
										break;
									}
									num357 = num204;
								}
							}
							bool flag121 = b77 == 5;
							if (flag121)
							{
							}
							bool flag122 = b77 == 6;
							if (flag122)
							{
								GameCanvas.panel.isFriendLock = true;
								bool flag123 = GameCanvas.panel2 != null;
								if (flag123)
								{
									GameCanvas.panel2.isFriendLock = true;
								}
								GameCanvas.panel.vFriendGD.removeAllElements();
								bool flag124 = GameCanvas.panel2 != null;
								if (flag124)
								{
									GameCanvas.panel2.vFriendGD.removeAllElements();
								}
								int friendMoneyGD = msg.reader().readInt();
								sbyte b79 = msg.reader().readByte();
								Res.outz("item size = " + b79.ToString());
								int num357;
								for (int num205 = 0; num205 < (int)b79; num205 = num357 + 1)
								{
									Item item2 = new Item();
									item2.template = ItemTemplates.get(msg.reader().readShort());
									item2.quantity = msg.reader().readInt();
									int num206 = (int)msg.reader().readUnsignedByte();
									bool flag125 = num206 != 0;
									if (flag125)
									{
										item2.itemOption = new ItemOption[num206];
										for (int num207 = 0; num207 < item2.itemOption.Length; num207 = num357 + 1)
										{
											int num208 = (int)msg.reader().readUnsignedByte();
											int param2 = (int)msg.reader().readUnsignedShort();
											bool flag126 = num208 != -1;
											if (flag126)
											{
												item2.itemOption[num207] = new ItemOption(num208, param2);
												item2.compare = GameCanvas.panel.getCompare(item2);
											}
											num357 = num207;
										}
									}
									bool flag127 = GameCanvas.panel2 != null;
									if (flag127)
									{
										GameCanvas.panel2.vFriendGD.addElement(item2);
									}
									else
									{
										GameCanvas.panel.vFriendGD.addElement(item2);
									}
									num357 = num205;
								}
								bool flag128 = GameCanvas.panel2 != null;
								if (flag128)
								{
									GameCanvas.panel2.setTabGiaoDich(false);
									GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
								}
								else
								{
									GameCanvas.panel.friendMoneyGD = friendMoneyGD;
									bool flag129 = GameCanvas.panel.currentTabIndex == 2;
									if (flag129)
									{
										GameCanvas.panel.setTabGiaoDich(false);
									}
								}
							}
							bool flag130 = b77 == 7;
							if (flag130)
							{
								InfoDlg.hide();
								bool isShow = GameCanvas.panel.isShow;
								if (isShow)
								{
									GameCanvas.panel.hide();
								}
							}
							goto IL_B603;
						}
						case 14:
						{
							Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
							sbyte b80 = msg.reader().readByte();
							bool flag131 = b80 == 0;
							if (flag131)
							{
								int num209 = (int)msg.reader().readUnsignedShort();
								Res.outz("lent =" + num209.ToString());
								sbyte[] imageData = new sbyte[num209];
								msg.reader().read(ref imageData, 0, num209);
								GameScr.imgCapcha = Image.createImage(imageData, 0, num209);
								GameScr.gI().keyInput = "-----";
								GameScr.gI().strCapcha = msg.reader().readUTF();
								GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
								GameScr.gI().mobCapcha = new Mob();
								GameScr.gI().right = null;
							}
							bool flag132 = b80 == 1;
							if (flag132)
							{
								MobCapcha.isAttack = true;
							}
							bool flag133 = b80 == 2;
							if (flag133)
							{
								MobCapcha.explode = true;
								GameScr.gI().right = GameScr.gI().cmdFocus;
							}
							goto IL_B603;
						}
						case 15:
						{
							int index4 = (int)msg.reader().readUnsignedByte();
							Mob mob16 = null;
							try
							{
								mob16 = (Mob)GameScr.vMob.elementAt(index4);
							}
							catch (Exception ex26)
							{
							}
							bool flag134 = mob16 != null;
							if (flag134)
							{
								mob16.maxHp = msg.reader().readInt();
							}
							goto IL_B603;
						}
						case 16:
						{
							sbyte b81 = msg.reader().readByte();
							bool flag135 = b81 == 0;
							if (flag135)
							{
								int num210 = (int)msg.reader().readShort();
								int bgRID = (int)msg.reader().readShort();
								int num211 = (int)msg.reader().readUnsignedByte();
								int num212 = msg.reader().readInt();
								string text15 = msg.reader().readUTF();
								int num213 = (int)msg.reader().readShort();
								int num214 = (int)msg.reader().readShort();
								sbyte b82 = msg.reader().readByte();
								bool flag136 = b82 == 1;
								if (flag136)
								{
									GameScr.gI().isRongNamek = true;
								}
								else
								{
									GameScr.gI().isRongNamek = false;
								}
								GameScr.gI().xR = num213;
								GameScr.gI().yR = num214;
								Res.outz(string.Concat(new object[]
								{
									"xR= ",
									num213,
									" yR= ",
									num214,
									" +++++++++++++++++++++++++++++++++++++++"
								}));
								bool flag137 = global::Char.myCharz().charID == num212;
								if (flag137)
								{
									GameCanvas.panel.hideNow();
									GameScr.gI().activeRongThanEff(true);
								}
								else
								{
									bool flag138 = TileMap.mapID == num210 && TileMap.zoneID == num211;
									if (flag138)
									{
										GameScr.gI().activeRongThanEff(false);
									}
									else
									{
										bool flag139 = mGraphics.zoomLevel > 1;
										if (flag139)
										{
											GameScr.gI().doiMauTroi();
										}
									}
								}
								GameScr.gI().mapRID = num210;
								GameScr.gI().bgRID = bgRID;
								GameScr.gI().zoneRID = num211;
							}
							bool flag140 = b81 == 1;
							if (flag140)
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
								bool flag141 = TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID;
								if (flag141)
								{
									GameScr.gI().hideRongThanEff();
								}
								else
								{
									GameScr.gI().isRongThanXuatHien = false;
									bool isRongNamek = GameScr.gI().isRongNamek;
									if (isRongNamek)
									{
										GameScr.gI().isRongNamek = false;
									}
								}
							}
							bool flag142 = b81 == 2;
							if (flag142)
							{
							}
							goto IL_B603;
						}
						case 17:
						{
							sbyte b83 = msg.reader().readByte();
							TileMap.tileIndex = new int[(int)b83][][];
							TileMap.tileType = new int[(int)b83][];
							int num357;
							for (int num215 = 0; num215 < (int)b83; num215 = num357 + 1)
							{
								sbyte b84 = msg.reader().readByte();
								TileMap.tileType[num215] = new int[(int)b84];
								TileMap.tileIndex[num215] = new int[(int)b84][];
								for (int num216 = 0; num216 < (int)b84; num216 = num357 + 1)
								{
									TileMap.tileType[num215][num216] = msg.reader().readInt();
									sbyte b85 = msg.reader().readByte();
									TileMap.tileIndex[num215][num216] = new int[(int)b85];
									for (int num217 = 0; num217 < (int)b85; num217 = num357 + 1)
									{
										TileMap.tileIndex[num215][num216][num217] = (int)msg.reader().readByte();
										num357 = num217;
									}
									num357 = num216;
								}
								num357 = num215;
							}
							goto IL_B603;
						}
						case 18:
						{
							sbyte b86 = msg.reader().readByte();
							bool flag143 = b86 == 0;
							if (flag143)
							{
								string src = msg.reader().readUTF();
								string src2 = msg.reader().readUTF();
								GameCanvas.panel.setTypeCombine();
								GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
								GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
								GameCanvas.panel.show();
							}
							bool flag144 = b86 == 1;
							int num357;
							if (flag144)
							{
								GameCanvas.panel.vItemCombine.removeAllElements();
								sbyte b87 = msg.reader().readByte();
								for (int num218 = 0; num218 < (int)b87; num218 = num357 + 1)
								{
									sbyte b88 = msg.reader().readByte();
									for (int num219 = 0; num219 < global::Char.myCharz().arrItemBag.Length; num219 = num357 + 1)
									{
										Item item3 = global::Char.myCharz().arrItemBag[num219];
										bool flag145 = item3 != null && item3.indexUI == (int)b88;
										if (flag145)
										{
											item3.isSelect = true;
											GameCanvas.panel.vItemCombine.addElement(item3);
										}
										num357 = num219;
									}
									num357 = num218;
								}
								bool isShow2 = GameCanvas.panel.isShow;
								if (isShow2)
								{
									GameCanvas.panel.setTabCombine();
								}
							}
							bool flag146 = b86 == 2;
							if (flag146)
							{
								GameCanvas.panel.combineSuccess = 0;
								GameCanvas.panel.setCombineEff(0);
							}
							bool flag147 = b86 == 3;
							if (flag147)
							{
								GameCanvas.panel.combineSuccess = 1;
								GameCanvas.panel.setCombineEff(0);
							}
							bool flag148 = b86 == 4;
							if (flag148)
							{
								short iconID = msg.reader().readShort();
								GameCanvas.panel.iconID3 = iconID;
								GameCanvas.panel.combineSuccess = 0;
								GameCanvas.panel.setCombineEff(1);
							}
							bool flag149 = b86 == 5;
							if (flag149)
							{
								short iconID2 = msg.reader().readShort();
								GameCanvas.panel.iconID3 = iconID2;
								GameCanvas.panel.combineSuccess = 0;
								GameCanvas.panel.setCombineEff(2);
							}
							bool flag150 = b86 == 6;
							if (flag150)
							{
								short iconID3 = msg.reader().readShort();
								short iconID4 = msg.reader().readShort();
								GameCanvas.panel.combineSuccess = 0;
								GameCanvas.panel.setCombineEff(3);
								GameCanvas.panel.iconID1 = iconID3;
								GameCanvas.panel.iconID3 = iconID4;
							}
							bool flag151 = b86 == 7;
							if (flag151)
							{
								short iconID5 = msg.reader().readShort();
								GameCanvas.panel.iconID3 = iconID5;
								GameCanvas.panel.combineSuccess = 0;
								GameCanvas.panel.setCombineEff(4);
							}
							bool flag152 = b86 == 8;
							if (flag152)
							{
								GameCanvas.panel.iconID3 = -1;
								GameCanvas.panel.combineSuccess = 1;
								GameCanvas.panel.setCombineEff(4);
							}
							short num220 = 21;
							try
							{
								num220 = msg.reader().readShort();
								int num221 = (int)msg.reader().readShort();
								int num222 = (int)msg.reader().readShort();
								GameCanvas.panel.xS = num221 - GameScr.cmx;
								GameCanvas.panel.yS = num222 - GameScr.cmy;
							}
							catch (Exception ex27)
							{
							}
							for (int num223 = 0; num223 < GameScr.vNpc.size(); num223 = num357 + 1)
							{
								Npc npc6 = (Npc)GameScr.vNpc.elementAt(num223);
								bool flag153 = npc6.template.npcTemplateId == (int)num220;
								if (flag153)
								{
									GameCanvas.panel.xS = npc6.cx - GameScr.cmx;
									GameCanvas.panel.yS = npc6.cy - GameScr.cmy;
									GameCanvas.panel.idNPC = (int)num220;
									break;
								}
								num357 = num223;
							}
							goto IL_B603;
						}
						case 19:
						{
							sbyte b89 = msg.reader().readByte();
							InfoDlg.hide();
							bool flag154 = b89 == 0;
							if (flag154)
							{
								GameCanvas.panel.vFriend.removeAllElements();
								int num224 = (int)msg.reader().readUnsignedByte();
								int num357;
								for (int num225 = 0; num225 < num224; num225 = num357 + 1)
								{
									global::Char char7 = new global::Char();
									char7.charID = msg.reader().readInt();
									char7.head = (int)msg.reader().readShort();
									char7.headICON = (int)msg.reader().readShort();
									char7.body = (int)msg.reader().readShort();
									char7.leg = (int)msg.reader().readShort();
									char7.bag = (int)msg.reader().readUnsignedByte();
									char7.cName = msg.reader().readUTF();
									bool isOnline = msg.reader().readBoolean();
									InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
									infoItem2.charInfo = char7;
									infoItem2.isOnline = isOnline;
									GameCanvas.panel.vFriend.addElement(infoItem2);
									num357 = num225;
								}
								GameCanvas.panel.setTypeFriend();
								GameCanvas.panel.show();
							}
							bool flag155 = b89 == 3;
							if (flag155)
							{
								MyVector vFriend = GameCanvas.panel.vFriend;
								int num226 = msg.reader().readInt();
								Res.outz("online offline id=" + num226.ToString());
								int num357;
								for (int num227 = 0; num227 < vFriend.size(); num227 = num357 + 1)
								{
									InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num227);
									bool flag156 = infoItem3.charInfo != null && infoItem3.charInfo.charID == num226;
									if (flag156)
									{
										Res.outz("online= " + infoItem3.isOnline.ToString());
										infoItem3.isOnline = msg.reader().readBoolean();
										break;
									}
									num357 = num227;
								}
							}
							bool flag157 = b89 == 2;
							if (flag157)
							{
								MyVector vFriend2 = GameCanvas.panel.vFriend;
								int num228 = msg.reader().readInt();
								int num357;
								for (int num229 = 0; num229 < vFriend2.size(); num229 = num357 + 1)
								{
									InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num229);
									bool flag158 = infoItem4.charInfo != null && infoItem4.charInfo.charID == num228;
									if (flag158)
									{
										vFriend2.removeElement(infoItem4);
										break;
									}
									num357 = num229;
								}
								bool isShow3 = GameCanvas.panel.isShow;
								if (isShow3)
								{
									GameCanvas.panel.setTabFriend();
								}
							}
							goto IL_B603;
						}
						case 20:
						{
							InfoDlg.hide();
							int num230 = msg.reader().readInt();
							global::Char charMenu = GameCanvas.panel.charMenu;
							bool flag159 = charMenu == null;
							if (flag159)
							{
								return;
							}
							charMenu.cPower = msg.reader().readLong();
							charMenu.currStrLevel = msg.reader().readUTF();
							goto IL_B603;
						}
						case 22:
						{
							short num231 = msg.reader().readShort();
							SmallImage.newSmallVersion = new sbyte[(int)num231];
							SmallImage.maxSmall = num231;
							SmallImage.imgNew = new Small[(int)num231];
							int num357;
							for (int num232 = 0; num232 < (int)num231; num232 = num357 + 1)
							{
								SmallImage.newSmallVersion[num232] = msg.reader().readByte();
								num357 = num232;
							}
							goto IL_B603;
						}
						case 23:
						{
							sbyte b90 = msg.reader().readByte();
							bool flag160 = b90 == 0;
							if (flag160)
							{
								sbyte b91 = msg.reader().readByte();
								bool flag161 = b91 <= 0;
								if (flag161)
								{
									return;
								}
								global::Char.myCharz().arrArchive = new Archivement[(int)b91];
								int num357;
								for (int num233 = 0; num233 < (int)b91; num233 = num357 + 1)
								{
									global::Char.myCharz().arrArchive[num233] = new Archivement();
									global::Char.myCharz().arrArchive[num233].info1 = (num233 + 1).ToString() + ". " + msg.reader().readUTF();
									global::Char.myCharz().arrArchive[num233].info2 = msg.reader().readUTF();
									global::Char.myCharz().arrArchive[num233].money = (int)msg.reader().readShort();
									global::Char.myCharz().arrArchive[num233].isFinish = msg.reader().readBoolean();
									global::Char.myCharz().arrArchive[num233].isRecieve = msg.reader().readBoolean();
									num357 = num233;
								}
								GameCanvas.panel.setTypeArchivement();
								GameCanvas.panel.show();
							}
							else
							{
								bool flag162 = b90 == 1;
								if (flag162)
								{
									int num234 = (int)msg.reader().readUnsignedByte();
									bool flag163 = global::Char.myCharz().arrArchive[num234] != null;
									if (flag163)
									{
										global::Char.myCharz().arrArchive[num234].isRecieve = true;
									}
								}
							}
							goto IL_B603;
						}
						case 25:
						{
							bool stopDownload = ServerListScreen.stopDownload;
							if (stopDownload)
							{
								return;
							}
							bool flag164 = !GameCanvas.isGetResourceFromServer();
							if (flag164)
							{
								Service.gI().getResource(3, null);
								SmallImage.loadBigRMS();
								SplashScr.imgLogo = null;
								bool flag165 = Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null;
								if (flag165)
								{
									LoginScr.isContinueToLogin = true;
								}
								GameCanvas.loginScr = new LoginScr();
								GameCanvas.loginScr.switchToMe();
								return;
							}
							bool flag11 = true;
							sbyte b92 = msg.reader().readByte();
							bool flag166 = b92 == 0;
							if (flag166)
							{
								int num235 = msg.reader().readInt();
								string text16 = Rms.loadRMSString("ResVersion");
								int num236 = (text16 == null || !(text16 != string.Empty)) ? -1 : int.Parse(text16);
								bool flag12 = Session_ME.gI().isCompareIPConnect();
								bool flag167 = flag12;
								if (flag167)
								{
									bool flag168 = num236 == -1 || num236 != num235;
									if (flag168)
									{
										GameCanvas.serverScreen.show2();
									}
									else
									{
										Res.outz("login ngay");
										SmallImage.loadBigRMS();
										SplashScr.imgLogo = null;
										ServerListScreen.loadScreen = true;
										bool flag169 = GameCanvas.currentScreen != GameCanvas.loginScr;
										if (flag169)
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
							bool flag170 = b92 == 1;
							if (flag170)
							{
								ServerListScreen.strWait = mResources.downloading_data;
								short nBig = msg.reader().readShort();
								ServerListScreen.nBig = (int)nBig;
								Service.gI().getResource(2, null);
							}
							bool flag171 = b92 == 2;
							if (flag171)
							{
								try
								{
									Controller.isLoadingData = true;
									GameCanvas.endDlg();
									int num357 = ServerListScreen.demPercent;
									ServerListScreen.demPercent = num357 + 1;
									ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
									string original = msg.reader().readUTF();
									string[] array20 = Res.split(original, "/", 0);
									string filename = "x" + mGraphics.zoomLevel.ToString() + array20[array20.Length - 1];
									int num237 = msg.reader().readInt();
									sbyte[] data2 = new sbyte[num237];
									msg.reader().read(ref data2, 0, num237);
									Rms.saveRMS(filename, data2);
								}
								catch (Exception ex28)
								{
									GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
								}
							}
							bool flag172 = b92 == 3 && flag11;
							if (flag172)
							{
								Controller.isLoadingData = false;
								int num238 = msg.reader().readInt();
								Res.outz("last version= " + num238.ToString());
								Rms.saveRMSString("ResVersion", num238.ToString() + string.Empty);
								Service.gI().getResource(3, null);
								GameCanvas.endDlg();
								SplashScr.imgLogo = null;
								SmallImage.loadBigRMS();
								mSystem.gcc();
								ServerListScreen.bigOk = true;
								ServerListScreen.loadScreen = true;
								GameScr.gI().loadGameScr();
								bool flag173 = GameCanvas.currentScreen != GameCanvas.loginScr;
								if (flag173)
								{
									GameCanvas.serverScreen.switchToMe();
								}
							}
							goto IL_B603;
						}
						case 29:
						{
							Res.outz("BIG MESSAGE .......................................");
							GameCanvas.endDlg();
							int avatar3 = (int)msg.reader().readShort();
							string chat4 = msg.reader().readUTF();
							ChatPopup.addBigMessage(chat4, 100000, new Npc(-1, 0, 0, 0, 0, 0)
							{
								avatar = avatar3
							});
							sbyte b93 = msg.reader().readByte();
							bool flag174 = b93 == 0;
							if (flag174)
							{
								ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
								ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
								ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
							}
							bool flag175 = b93 == 1;
							if (flag175)
							{
								string p = msg.reader().readUTF();
								string caption5 = msg.reader().readUTF();
								ChatPopup.serverChatPopUp.cmdMsg1 = new Command(caption5, ChatPopup.serverChatPopUp, 1000, p);
								ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
								ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
								ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
								ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
								ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
							}
							goto IL_B603;
						}
						case 30:
							global::Char.myCharz().cMaxStamina = msg.reader().readShort();
							goto IL_B603;
						case 31:
							global::Char.myCharz().cStamina = (int)msg.reader().readShort();
							goto IL_B603;
						case 32:
						{
							this.demCount += 1f;
							int num239 = msg.reader().readInt();
							sbyte[] array21 = null;
							try
							{
								array21 = NinjaUtil.readByteArray(msg);
								bool flag176 = num239 == 3896;
								if (flag176)
								{
								}
								SmallImage.imgNew[num239].img = this.createImage(array21);
							}
							catch (Exception ex29)
							{
								array21 = null;
								SmallImage.imgNew[num239].img = Image.createRGBImage(new int[1], 1, 1, true);
							}
							bool flag177 = array21 != null && mGraphics.zoomLevel > 1;
							if (flag177)
							{
								Rms.saveRMS(mGraphics.zoomLevel.ToString() + "Small" + num239.ToString(), array21);
							}
							goto IL_B603;
						}
						case 33:
						{
							short id = msg.reader().readShort();
							sbyte[] data3 = NinjaUtil.readByteArray(msg);
							EffectData effDataById = Effect.getEffDataById((int)id);
							sbyte b94 = msg.reader().readSByte();
							bool flag178 = b94 == 0;
							if (flag178)
							{
								effDataById.readData(data3);
							}
							else
							{
								effDataById.readDataNewBoss(data3, b94);
							}
							sbyte[] array22 = NinjaUtil.readByteArray(msg);
							effDataById.img = Image.createImage(array22, 0, array22.Length);
							goto IL_B603;
						}
						case 34:
						{
							InfoDlg.hide();
							int num240 = msg.reader().readInt();
							sbyte b95 = msg.reader().readByte();
							bool flag179 = b95 != 0;
							if (flag179)
							{
								bool flag180 = global::Char.myCharz().charID == num240;
								if (flag180)
								{
									Controller.isStopReadMessage = true;
									GameScr.lockTick = 500;
									GameScr.gI().center = null;
									bool flag181 = b95 == 0 || b95 == 1 || b95 == 3;
									if (flag181)
									{
										Teleport p2 = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 0, true, (b95 != 1) ? ((int)b95) : global::Char.myCharz().cgender);
										Teleport.addTeleport(p2);
									}
									bool flag182 = b95 == 2;
									if (flag182)
									{
										GameScr.lockTick = 50;
										global::Char.myCharz().hide();
									}
								}
								else
								{
									global::Char char8 = GameScr.findCharInMap(num240);
									bool flag183 = (b95 == 0 || b95 == 1 || b95 == 3) && char8 != null;
									if (flag183)
									{
										char8.isUsePlane = true;
										Teleport.addTeleport(new Teleport(char8.cx, char8.cy, char8.head, char8.cdir, 0, false, (b95 != 1) ? ((int)b95) : char8.cgender)
										{
											id = num240
										});
									}
									bool flag184 = b95 == 2;
									if (flag184)
									{
										char8.hide();
									}
								}
							}
							goto IL_B603;
						}
						case 35:
						{
							int num241 = msg.reader().readInt();
							int num242 = (int)msg.reader().readUnsignedByte();
							@char = null;
							bool flag185 = num241 == global::Char.myCharz().charID;
							if (flag185)
							{
								@char = global::Char.myCharz();
							}
							else
							{
								@char = GameScr.findCharInMap(num241);
							}
							bool flag186 = @char == null;
							if (flag186)
							{
								return;
							}
							@char.bag = num242;
							int num357;
							for (int num243 = 0; num243 < 54; num243 = num357 + 1)
							{
								@char.removeEffChar(0, 201 + num243);
								num357 = num243;
							}
							bool flag187 = @char.bag >= 201 && @char.bag < 255;
							if (flag187)
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
								num241,
								" BAG ID= ",
								num242
							}));
							goto IL_B603;
						}
						case 36:
						{
							Res.outz("GET BAG");
							int num244 = (int)msg.reader().readUnsignedByte();
							sbyte b96 = msg.reader().readByte();
							ClanImage clanImage = new ClanImage();
							clanImage.ID = num244;
							bool flag188 = b96 > 0;
							if (flag188)
							{
								clanImage.idImage = new short[(int)b96];
								int num357;
								for (int num245 = 0; num245 < (int)b96; num245 = num357 + 1)
								{
									clanImage.idImage[num245] = msg.reader().readShort();
									Res.outz(string.Concat(new object[]
									{
										"ID=  ",
										num244,
										" frame= ",
										clanImage.idImage[num245]
									}));
									num357 = num245;
								}
								ClanImage.idImages.put(num244.ToString() + string.Empty, clanImage);
							}
							goto IL_B603;
						}
						case 37:
						{
							int num246 = (int)msg.reader().readUnsignedByte();
							sbyte b97 = msg.reader().readByte();
							bool flag189 = b97 > 0;
							if (flag189)
							{
								ClanImage clanImage2 = ClanImage.getClanImage((short)num246);
								bool flag190 = clanImage2 != null;
								if (flag190)
								{
									clanImage2.idImage = new short[(int)b97];
									int num357;
									for (int num247 = 0; num247 < (int)b97; num247 = num357 + 1)
									{
										clanImage2.idImage[num247] = msg.reader().readShort();
										bool flag191 = clanImage2.idImage[num247] > 0;
										if (flag191)
										{
											SmallImage.vKeys.addElement(clanImage2.idImage[num247].ToString() + string.Empty);
										}
										num357 = num247;
									}
								}
							}
							goto IL_B603;
						}
						case 38:
						{
							int num248 = msg.reader().readInt();
							bool flag192 = num248 != global::Char.myCharz().charID;
							if (flag192)
							{
								bool flag193 = GameScr.findCharInMap(num248) != null;
								if (flag193)
								{
									GameScr.findCharInMap(num248).clanID = msg.reader().readInt();
									bool flag194 = GameScr.findCharInMap(num248).clanID == -2;
									if (flag194)
									{
										GameScr.findCharInMap(num248).isCopy = true;
									}
								}
							}
							else
							{
								bool flag195 = global::Char.myCharz().clan != null;
								if (flag195)
								{
									global::Char.myCharz().clan.ID = msg.reader().readInt();
								}
							}
							goto IL_B603;
						}
						case 39:
						{
							GameCanvas.debug("SA7666", 2);
							int num249 = msg.reader().readInt();
							int num250 = -1;
							bool flag196 = num249 != global::Char.myCharz().charID;
							if (flag196)
							{
								global::Char char9 = GameScr.findCharInMap(num249);
								bool flag197 = char9 == null;
								if (flag197)
								{
									return;
								}
								bool flag198 = char9.currentMovePoint != null;
								if (flag198)
								{
									char9.createShadow(char9.cx, char9.cy, 10);
									char9.cx = char9.currentMovePoint.xEnd;
									char9.cy = char9.currentMovePoint.yEnd;
								}
								int num251 = (int)msg.reader().readUnsignedByte();
								bool flag199 = (TileMap.tileTypeAtPixel(char9.cx, char9.cy) & 2) == 2;
								if (flag199)
								{
									char9.setSkillPaint(GameScr.sks[num251], 0);
								}
								else
								{
									char9.setSkillPaint(GameScr.sks[num251], 1);
								}
								sbyte b98 = msg.reader().readByte();
								global::Char[] array23 = new global::Char[(int)b98];
								int num357;
								for (i = 0; i < array23.Length; i = num357 + 1)
								{
									num250 = msg.reader().readInt();
									bool flag200 = num250 == global::Char.myCharz().charID;
									global::Char char10;
									if (flag200)
									{
										char10 = global::Char.myCharz();
										bool flag201 = !GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay;
										if (flag201)
										{
											Service.gI().requestChangeZone(-1, -1);
											GameScr.isChangeZone = true;
										}
									}
									else
									{
										char10 = GameScr.findCharInMap(num250);
									}
									array23[i] = char10;
									bool flag202 = i == 0;
									if (flag202)
									{
										bool flag203 = char9.cx <= char10.cx;
										if (flag203)
										{
											char9.cdir = 1;
										}
										else
										{
											char9.cdir = -1;
										}
									}
									num357 = i;
								}
								bool flag204 = i > 0;
								if (flag204)
								{
									char9.attChars = new global::Char[i];
									for (i = 0; i < char9.attChars.Length; i = num357 + 1)
									{
										char9.attChars[i] = array23[i];
										num357 = i;
									}
									char9.mobFocus = null;
									char9.charFocus = char9.attChars[0];
								}
							}
							else
							{
								sbyte b99 = msg.reader().readByte();
								sbyte b100 = msg.reader().readByte();
								num250 = msg.reader().readInt();
							}
							try
							{
								sbyte b101 = msg.reader().readByte();
								Res.outz("isRead continue = " + b101.ToString());
								bool flag205 = b101 == 1;
								if (flag205)
								{
									sbyte b102 = msg.reader().readByte();
									Res.outz("type skill = " + b102.ToString());
									bool flag206 = num250 == global::Char.myCharz().charID;
									if (flag206)
									{
										@char = global::Char.myCharz();
										int num252 = msg.readInt3Byte();
										Res.outz("dame hit = " + num252.ToString());
										@char.isDie = msg.reader().readBoolean();
										bool isDie = @char.isDie;
										if (isDie)
										{
											global::Char.isLockKey = true;
										}
										Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
										int num253 = 0;
										bool isCrit = msg.reader().readBoolean();
										@char.isCrit = isCrit;
										@char.isMob = false;
										num252 += num253;
										@char.damHP = num252;
										bool flag207 = b102 == 0;
										if (flag207)
										{
											@char.doInjure(num252, 0, isCrit, false);
										}
									}
									else
									{
										@char = GameScr.findCharInMap(num250);
										bool flag208 = @char == null;
										if (flag208)
										{
											return;
										}
										int num254 = msg.readInt3Byte();
										Res.outz("dame hit= " + num254.ToString());
										@char.isDie = msg.reader().readBoolean();
										Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
										int num255 = 0;
										bool isCrit2 = msg.reader().readBoolean();
										@char.isCrit = isCrit2;
										@char.isMob = false;
										num254 += num255;
										@char.damHP = num254;
										bool flag209 = b102 == 0;
										if (flag209)
										{
											@char.doInjure(num254, 0, isCrit2, false);
										}
									}
								}
							}
							catch (Exception ex30)
							{
							}
							goto IL_B603;
						}
						case 40:
						{
							sbyte typePK = msg.reader().readByte();
							GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
							goto IL_B603;
						}
						case 41:
							goto IL_B603;
						case 42:
						{
							string strInvite = msg.reader().readUTF();
							int clanID = msg.reader().readInt();
							int code = msg.reader().readInt();
							GameScr.gI().clanInvite(strInvite, clanID, code);
							goto IL_B603;
						}
						case 46:
						{
							InfoDlg.hide();
							bool flag13 = false;
							int num256 = msg.reader().readInt();
							Res.outz("clanId= " + num256.ToString());
							bool flag210 = num256 == -1;
							if (flag210)
							{
								global::Char.myCharz().clan = null;
								ClanMessage.vMessage.removeAllElements();
								bool flag211 = GameCanvas.panel.member != null;
								if (flag211)
								{
									GameCanvas.panel.member.removeAllElements();
								}
								bool flag212 = GameCanvas.panel.myMember != null;
								if (flag212)
								{
									GameCanvas.panel.myMember.removeAllElements();
								}
								bool flag213 = GameCanvas.currentScreen == GameScr.gI();
								if (flag213)
								{
									GameCanvas.panel.setTabClans();
								}
								return;
							}
							GameCanvas.panel.tabIcon = null;
							bool flag214 = global::Char.myCharz().clan == null;
							if (flag214)
							{
								global::Char.myCharz().clan = new Clan();
							}
							global::Char.myCharz().clan.ID = num256;
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
							int num357;
							for (int num257 = 0; num257 < global::Char.myCharz().clan.currMember; num257 = num357 + 1)
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
								num357 = num257;
							}
							int num258 = (int)msg.reader().readUnsignedByte();
							for (int num259 = 0; num259 < num258; num259 = num357 + 1)
							{
								this.readClanMsg(msg, -1);
								num357 = num259;
							}
							bool flag215 = GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage;
							if (flag215)
							{
								GameCanvas.panel.setTabClans();
							}
							bool flag216 = flag13;
							if (flag216)
							{
								GameCanvas.panel.setTabClans();
							}
							Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -537 MY CLAN INFO");
							goto IL_B603;
						}
						case 47:
						{
							sbyte b103 = msg.reader().readByte();
							bool flag217 = b103 == 0;
							if (flag217)
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
								bool flag218 = GameCanvas.panel.myMember == null;
								if (flag218)
								{
									GameCanvas.panel.myMember = new MyVector();
								}
								GameCanvas.panel.myMember.addElement(member2);
								GameCanvas.panel.initTabClans();
							}
							bool flag219 = b103 == 1;
							if (flag219)
							{
								GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
								Panel panel = GameCanvas.panel;
								Panel panel2 = panel;
								int num357 = panel.currentListLength;
								panel2.currentListLength = num357 - 1;
								GameCanvas.panel.initTabClans();
							}
							bool flag220 = b103 == 2;
							if (flag220)
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
								int num357;
								for (int num260 = 0; num260 < GameCanvas.panel.myMember.size(); num260 = num357 + 1)
								{
									Member member4 = (Member)GameCanvas.panel.myMember.elementAt(num260);
									bool flag221 = member4.ID == member3.ID;
									if (flag221)
									{
										bool flag222 = global::Char.myCharz().charID == member3.ID;
										if (flag222)
										{
											global::Char.myCharz().role = member3.role;
										}
										Member o = member3;
										GameCanvas.panel.myMember.removeElement(member4);
										GameCanvas.panel.myMember.insertElementAt(o, num260);
										return;
									}
									num357 = num260;
								}
							}
							Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -52  MY CLAN UPDSTE");
							goto IL_B603;
						}
						case 48:
						{
							InfoDlg.hide();
							this.readClanMsg(msg, 0);
							bool flag223 = GameCanvas.panel.isMessage && GameCanvas.panel.type == 5;
							if (flag223)
							{
								GameCanvas.panel.initTabClans();
							}
							goto IL_B603;
						}
						case 49:
						{
							InfoDlg.hide();
							GameCanvas.panel.member = new MyVector();
							sbyte b104 = msg.reader().readByte();
							int num357;
							for (int num261 = 0; num261 < (int)b104; num261 = num357 + 1)
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
								num357 = num261;
							}
							GameCanvas.panel.isViewMember = true;
							GameCanvas.panel.isSearchClan = false;
							GameCanvas.panel.isMessage = false;
							GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
							GameCanvas.panel.initTabClans();
							goto IL_B603;
						}
						case 52:
						{
							InfoDlg.hide();
							sbyte b105 = msg.reader().readByte();
							Res.outz("clan = " + b105.ToString());
							bool flag224 = b105 == 0;
							if (flag224)
							{
								GameCanvas.panel.clanReport = mResources.cannot_find_clan;
								GameCanvas.panel.clans = null;
							}
							else
							{
								GameCanvas.panel.clans = new Clan[(int)b105];
								Res.outz("clan search lent= " + GameCanvas.panel.clans.Length.ToString());
								int num357;
								for (int num262 = 0; num262 < GameCanvas.panel.clans.Length; num262 = num357 + 1)
								{
									GameCanvas.panel.clans[num262] = new Clan();
									GameCanvas.panel.clans[num262].ID = msg.reader().readInt();
									GameCanvas.panel.clans[num262].name = msg.reader().readUTF();
									GameCanvas.panel.clans[num262].slogan = msg.reader().readUTF();
									GameCanvas.panel.clans[num262].imgID = (int)msg.reader().readUnsignedByte();
									GameCanvas.panel.clans[num262].powerPoint = msg.reader().readUTF();
									GameCanvas.panel.clans[num262].leaderName = msg.reader().readUTF();
									GameCanvas.panel.clans[num262].currMember = (int)msg.reader().readUnsignedByte();
									GameCanvas.panel.clans[num262].maxMember = (int)msg.reader().readUnsignedByte();
									GameCanvas.panel.clans[num262].date = msg.reader().readInt();
									num357 = num262;
								}
							}
							GameCanvas.panel.isSearchClan = true;
							GameCanvas.panel.isViewMember = false;
							GameCanvas.panel.isMessage = false;
							bool isSearchClan = GameCanvas.panel.isSearchClan;
							if (isSearchClan)
							{
								GameCanvas.panel.initTabClans();
							}
							goto IL_B603;
						}
						case 53:
						{
							InfoDlg.hide();
							sbyte b106 = msg.reader().readByte();
							bool flag225 = b106 == 1 || b106 == 3;
							if (flag225)
							{
								GameCanvas.endDlg();
								ClanImage.vClanImage.removeAllElements();
								int num263 = (int)msg.reader().readUnsignedByte();
								int num357;
								for (int num264 = 0; num264 < num263; num264 = num357 + 1)
								{
									ClanImage clanImage3 = new ClanImage();
									clanImage3.ID = (int)msg.reader().readUnsignedByte();
									clanImage3.name = msg.reader().readUTF();
									clanImage3.xu = msg.reader().readInt();
									clanImage3.luong = msg.reader().readInt();
									bool flag226 = !ClanImage.isExistClanImage(clanImage3.ID);
									if (flag226)
									{
										ClanImage.addClanImage(clanImage3);
									}
									else
									{
										ClanImage.getClanImage((short)clanImage3.ID).name = clanImage3.name;
										ClanImage.getClanImage((short)clanImage3.ID).xu = clanImage3.xu;
										ClanImage.getClanImage((short)clanImage3.ID).luong = clanImage3.luong;
									}
									num357 = num264;
								}
								bool flag227 = global::Char.myCharz().clan != null;
								if (flag227)
								{
									GameCanvas.panel.changeIcon();
								}
							}
							bool flag228 = b106 == 4;
							if (flag228)
							{
								global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
								global::Char.myCharz().clan.slogan = msg.reader().readUTF();
							}
							goto IL_B603;
						}
						case 54:
						{
							sbyte b107 = msg.reader().readByte();
							int num265 = msg.reader().readInt();
							short num266 = msg.reader().readShort();
							Res.outz(string.Concat(new object[]
							{
								">.SKILL_NOT_FOCUS      skillNotFocusID: ",
								num266,
								" skill type= ",
								b107,
								"   player use= ",
								num265
							}));
							bool flag229 = b107 == 20;
							if (flag229)
							{
								sbyte b108 = msg.reader().readByte();
								sbyte dir = msg.reader().readByte();
								short timeGong = msg.reader().readShort();
								bool isFly = msg.reader().readByte() != 0;
								sbyte typePaint = msg.reader().readByte();
								sbyte typeItem = -1;
								try
								{
									typeItem = msg.reader().readByte();
								}
								catch (Exception ex31)
								{
								}
								Res.outz(">.SKILL_NOT_FOCUS  skill typeFrame= " + b108.ToString());
								bool flag230 = global::Char.myCharz().charID == num265;
								if (flag230)
								{
									@char = global::Char.myCharz();
								}
								else
								{
									@char = GameScr.findCharInMap(num265);
								}
								@char.SetSkillPaint_NEW(num266, isFly, b108, typePaint, dir, timeGong, typeItem);
							}
							bool flag231 = b107 == 21;
							if (flag231)
							{
								Point point = new Point();
								point.x = (int)msg.reader().readShort();
								point.y = (int)msg.reader().readShort();
								short timeDame = msg.reader().readShort();
								short rangeDame = msg.reader().readShort();
								sbyte typePaint2 = 0;
								sbyte typeItem2 = -1;
								Point[] array24 = null;
								bool flag232 = global::Char.myCharz().charID == num265;
								if (flag232)
								{
									@char = global::Char.myCharz();
								}
								else
								{
									@char = GameScr.findCharInMap(num265);
								}
								try
								{
									typePaint2 = msg.reader().readByte();
									sbyte b109 = msg.reader().readByte();
									array24 = new Point[(int)b109];
									int num357;
									for (int num267 = 0; num267 < array24.Length; num267 = num357 + 1)
									{
										array24[num267] = new Point();
										array24[num267].type = msg.reader().readByte();
										bool flag233 = array24[num267].type == 0;
										if (flag233)
										{
											array24[num267].id = (int)msg.reader().readByte();
										}
										else
										{
											array24[num267].id = msg.reader().readInt();
										}
										num357 = num267;
									}
								}
								catch (Exception ex32)
								{
								}
								try
								{
									typeItem2 = msg.reader().readByte();
								}
								catch (Exception ex33)
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
								@char.SetSkillPaint_STT(1, num266, point, timeDame, rangeDame, typePaint2, array24, typeItem2);
							}
							bool flag234 = b107 == 0;
							if (flag234)
							{
								Res.outz("id use= " + num265.ToString());
								bool flag235 = global::Char.myCharz().charID != num265;
								if (flag235)
								{
									@char = GameScr.findCharInMap(num265);
									bool flag236 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
									if (flag236)
									{
										@char.setSkillPaint(GameScr.sks[(int)num266], 0);
									}
									else
									{
										@char.setSkillPaint(GameScr.sks[(int)num266], 1);
										@char.delayFall = 20;
									}
								}
								else
								{
									global::Char.myCharz().saveLoadPreviousSkill();
									Res.outz("LOAD LAST SKILL");
								}
								sbyte b110 = msg.reader().readByte();
								Res.outz("npc size= " + b110.ToString());
								int num357;
								for (int num268 = 0; num268 < (int)b110; num268 = num357 + 1)
								{
									sbyte b111 = msg.reader().readByte();
									sbyte b112 = msg.reader().readByte();
									Res.outz("index= " + b111.ToString());
									bool flag237 = num266 >= 42 && num266 <= 48;
									if (flag237)
									{
										((Mob)GameScr.vMob.elementAt((int)b111)).isFreez = true;
										((Mob)GameScr.vMob.elementAt((int)b111)).seconds = (int)b112;
										((Mob)GameScr.vMob.elementAt((int)b111)).last = (((Mob)GameScr.vMob.elementAt((int)b111)).cur = mSystem.currentTimeMillis());
									}
									num357 = num268;
								}
								sbyte b113 = msg.reader().readByte();
								for (int num269 = 0; num269 < (int)b113; num269 = num357 + 1)
								{
									int num270 = msg.reader().readInt();
									sbyte b114 = msg.reader().readByte();
									Res.outz(string.Concat(new object[]
									{
										"player ID= ",
										num270,
										" my ID= ",
										global::Char.myCharz().charID
									}));
									bool flag238 = num266 >= 42 && num266 <= 48;
									if (flag238)
									{
										bool flag239 = num270 == global::Char.myCharz().charID;
										if (flag239)
										{
											bool flag240 = !global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isStandAndCharge;
											if (flag240)
											{
												GameScr.gI().isFreez = true;
												global::Char.myCharz().isFreez = true;
												global::Char.myCharz().freezSeconds = (int)b114;
												global::Char.myCharz().lastFreez = (global::Char.myCharz().currFreez = mSystem.currentTimeMillis());
												global::Char.myCharz().isLockMove = true;
											}
										}
										else
										{
											@char = GameScr.findCharInMap(num270);
											bool flag241 = @char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge;
											if (flag241)
											{
												@char.isFreez = true;
												@char.seconds = (int)b114;
												@char.freezSeconds = (int)b114;
												@char.lastFreez = (GameScr.findCharInMap(num270).currFreez = mSystem.currentTimeMillis());
											}
										}
									}
									num357 = num269;
								}
							}
							bool flag242 = b107 == 1;
							if (flag242)
							{
								bool flag243 = num265 != global::Char.myCharz().charID;
								if (flag243)
								{
									GameScr.findCharInMap(num265).isCharge = true;
								}
							}
							bool flag244 = b107 == 3;
							if (flag244)
							{
								bool flag245 = num265 == global::Char.myCharz().charID;
								if (flag245)
								{
									global::Char.myCharz().isCharge = false;
									SoundMn.gI().taitaoPause();
									global::Char.myCharz().saveLoadPreviousSkill();
								}
								else
								{
									GameScr.findCharInMap(num265).isCharge = false;
								}
							}
							bool flag246 = b107 == 4;
							if (flag246)
							{
								bool flag247 = num265 == global::Char.myCharz().charID;
								if (flag247)
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
								else
								{
									bool flag248 = GameScr.findCharInMap(num265) != null;
									if (flag248)
									{
										int cgender = GameScr.findCharInMap(num265).cgender;
										bool flag249 = cgender == 0;
										if (flag249)
										{
											GameScr.findCharInMap(num265).useChargeSkill(false);
										}
										else
										{
											bool flag250 = cgender == 1;
											if (flag250)
											{
												GameScr.findCharInMap(num265).useChargeSkill(true);
											}
										}
										GameScr.findCharInMap(num265).skillTemplateId = (int)num266;
										GameScr.findCharInMap(num265).isUseSkillAfterCharge = true;
										GameScr.findCharInMap(num265).seconds = (int)msg.reader().readShort();
										GameScr.findCharInMap(num265).last = mSystem.currentTimeMillis();
									}
								}
							}
							bool flag251 = b107 == 5;
							if (flag251)
							{
								bool flag252 = num265 == global::Char.myCharz().charID;
								if (flag252)
								{
									global::Char.myCharz().stopUseChargeSkill();
								}
								else
								{
									bool flag253 = GameScr.findCharInMap(num265) != null;
									if (flag253)
									{
										GameScr.findCharInMap(num265).stopUseChargeSkill();
									}
								}
							}
							bool flag254 = b107 == 6;
							if (flag254)
							{
								bool flag255 = num265 == global::Char.myCharz().charID;
								if (flag255)
								{
									global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)num266], 0);
								}
								else
								{
									bool flag256 = GameScr.findCharInMap(num265) != null;
									if (flag256)
									{
										GameScr.findCharInMap(num265).setAutoSkillPaint(GameScr.sks[(int)num266], 0);
										SoundMn.gI().gong();
									}
								}
							}
							bool flag257 = b107 == 7;
							if (flag257)
							{
								bool flag258 = num265 == global::Char.myCharz().charID;
								if (flag258)
								{
									global::Char.myCharz().seconds = (int)msg.reader().readShort();
									Res.outz("second = " + global::Char.myCharz().seconds.ToString());
									global::Char.myCharz().last = mSystem.currentTimeMillis();
								}
								else
								{
									bool flag259 = GameScr.findCharInMap(num265) != null;
									if (flag259)
									{
										GameScr.findCharInMap(num265).useChargeSkill(true);
										GameScr.findCharInMap(num265).seconds = (int)msg.reader().readShort();
										GameScr.findCharInMap(num265).last = mSystem.currentTimeMillis();
										SoundMn.gI().gong();
									}
								}
							}
							bool flag260 = b107 == 8;
							if (flag260)
							{
								bool flag261 = num265 != global::Char.myCharz().charID;
								if (flag261)
								{
									bool flag262 = GameScr.findCharInMap(num265) != null;
									if (flag262)
									{
										GameScr.findCharInMap(num265).setAutoSkillPaint(GameScr.sks[(int)num266], 0);
									}
								}
							}
							goto IL_B603;
						}
						case 55:
						{
							bool flag14 = false;
							bool flag263 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
							if (flag263)
							{
								flag14 = true;
							}
							sbyte b115 = msg.reader().readByte();
							int num271 = (int)msg.reader().readUnsignedByte();
							global::Char.myCharz().arrItemShop = new Item[num271][];
							GameCanvas.panel.shopTabName = new string[num271 + (flag14 ? 0 : 1)][];
							int num357;
							for (int num272 = 0; num272 < GameCanvas.panel.shopTabName.Length; num272 = num357 + 1)
							{
								GameCanvas.panel.shopTabName[num272] = new string[2];
								num357 = num272;
							}
							bool flag264 = b115 == 2;
							if (flag264)
							{
								GameCanvas.panel.maxPageShop = new int[num271];
								GameCanvas.panel.currPageShop = new int[num271];
							}
							bool flag265 = !flag14;
							if (flag265)
							{
								GameCanvas.panel.shopTabName[num271] = mResources.inventory;
							}
							for (int num273 = 0; num273 < num271; num273 = num357 + 1)
							{
								string[] array25 = Res.split(msg.reader().readUTF(), "\n", 0);
								bool flag266 = b115 == 2;
								if (flag266)
								{
									GameCanvas.panel.maxPageShop[num273] = (int)msg.reader().readUnsignedByte();
								}
								bool flag267 = array25.Length == 2;
								if (flag267)
								{
									GameCanvas.panel.shopTabName[num273] = array25;
								}
								bool flag268 = array25.Length == 1;
								if (flag268)
								{
									GameCanvas.panel.shopTabName[num273][0] = array25[0];
									GameCanvas.panel.shopTabName[num273][1] = string.Empty;
								}
								int num274 = (int)msg.reader().readUnsignedByte();
								global::Char.myCharz().arrItemShop[num273] = new Item[num274];
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
								bool flag269 = b115 == 1;
								if (flag269)
								{
									Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
								}
								for (int num275 = 0; num275 < num274; num275 = num357 + 1)
								{
									short num276 = msg.reader().readShort();
									bool flag270 = num276 != -1;
									if (flag270)
									{
										global::Char.myCharz().arrItemShop[num273][num275] = new Item();
										global::Char.myCharz().arrItemShop[num273][num275].template = ItemTemplates.get(num276);
										Res.outz(string.Concat(new object[]
										{
											"name ",
											num273,
											" = ",
											global::Char.myCharz().arrItemShop[num273][num275].template.name,
											" id templat= ",
											global::Char.myCharz().arrItemShop[num273][num275].template.id
										}));
										bool flag271 = b115 == 8;
										if (flag271)
										{
											global::Char.myCharz().arrItemShop[num273][num275].buyCoin = msg.reader().readInt();
											global::Char.myCharz().arrItemShop[num273][num275].buyGold = msg.reader().readInt();
											global::Char.myCharz().arrItemShop[num273][num275].quantity = msg.reader().readInt();
										}
										else
										{
											bool flag272 = b115 == 4;
											if (flag272)
											{
												global::Char.myCharz().arrItemShop[num273][num275].reason = msg.reader().readUTF();
											}
											else
											{
												bool flag273 = b115 == 0;
												if (flag273)
												{
													global::Char.myCharz().arrItemShop[num273][num275].buyCoin = msg.reader().readInt();
													global::Char.myCharz().arrItemShop[num273][num275].buyGold = msg.reader().readInt();
												}
												else
												{
													bool flag274 = b115 == 1;
													if (flag274)
													{
														global::Char.myCharz().arrItemShop[num273][num275].powerRequire = msg.reader().readLong();
													}
													else
													{
														bool flag275 = b115 == 2;
														if (flag275)
														{
															global::Char.myCharz().arrItemShop[num273][num275].itemId = (int)msg.reader().readShort();
															global::Char.myCharz().arrItemShop[num273][num275].buyCoin = msg.reader().readInt();
															global::Char.myCharz().arrItemShop[num273][num275].buyGold = msg.reader().readInt();
															global::Char.myCharz().arrItemShop[num273][num275].buyType = msg.reader().readByte();
															global::Char.myCharz().arrItemShop[num273][num275].quantity = msg.reader().readInt();
															global::Char.myCharz().arrItemShop[num273][num275].isMe = msg.reader().readByte();
														}
														else
														{
															bool flag276 = b115 == 3;
															if (flag276)
															{
																global::Char.myCharz().arrItemShop[num273][num275].isBuySpec = true;
																global::Char.myCharz().arrItemShop[num273][num275].iconSpec = msg.reader().readShort();
																global::Char.myCharz().arrItemShop[num273][num275].buySpec = msg.reader().readInt();
															}
														}
													}
												}
											}
										}
										int num277 = (int)msg.reader().readUnsignedByte();
										bool flag277 = num277 != 0;
										if (flag277)
										{
											global::Char.myCharz().arrItemShop[num273][num275].itemOption = new ItemOption[num277];
											for (int num278 = 0; num278 < global::Char.myCharz().arrItemShop[num273][num275].itemOption.Length; num278 = num357 + 1)
											{
												int num279 = (int)msg.reader().readUnsignedByte();
												int param3 = (int)msg.reader().readUnsignedShort();
												bool flag278 = num279 != -1;
												if (flag278)
												{
													global::Char.myCharz().arrItemShop[num273][num275].itemOption[num278] = new ItemOption(num279, param3);
													global::Char.myCharz().arrItemShop[num273][num275].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[num273][num275]);
												}
												num357 = num278;
											}
										}
										sbyte b116 = msg.reader().readByte();
										global::Char.myCharz().arrItemShop[num273][num275].newItem = (b116 != 0);
										sbyte b117 = msg.reader().readByte();
										bool flag279 = b117 == 1;
										if (flag279)
										{
											int headTemp = (int)msg.reader().readShort();
											int bodyTemp = (int)msg.reader().readShort();
											int legTemp = (int)msg.reader().readShort();
											int bagTemp = (int)msg.reader().readShort();
											global::Char.myCharz().arrItemShop[num273][num275].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
										}
										bool flag280 = b115 == 2 && GameMidlet.intVERSION >= 237;
										if (flag280)
										{
											global::Char.myCharz().arrItemShop[num273][num275].nameNguoiKyGui = msg.reader().readUTF();
											Res.err("nguoi ki gui  " + global::Char.myCharz().arrItemShop[num273][num275].nameNguoiKyGui);
										}
									}
									num357 = num275;
								}
								num357 = num273;
							}
							bool flag281 = flag14;
							if (flag281)
							{
								bool flag282 = b115 != 2;
								if (flag282)
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
							bool flag283 = b115 == 2;
							if (flag283)
							{
								string[][] array26 = GameCanvas.panel.tabName[1];
								bool flag284 = flag14;
								if (flag284)
								{
									GameCanvas.panel.tabName[1] = new string[][]
									{
										array26[0],
										array26[1],
										array26[2],
										array26[3]
									};
								}
								else
								{
									GameCanvas.panel.tabName[1] = new string[][]
									{
										array26[0],
										array26[1],
										array26[2],
										array26[3],
										array26[4]
									};
								}
							}
							GameCanvas.panel.setTypeShop((int)b115);
							GameCanvas.panel.show();
							goto IL_B603;
						}
						case 56:
						{
							sbyte itemAction = msg.reader().readByte();
							sbyte where = msg.reader().readByte();
							sbyte index5 = msg.reader().readByte();
							string info6 = msg.reader().readUTF();
							GameCanvas.panel.itemRequest(itemAction, info6, where, index5);
							goto IL_B603;
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
							goto IL_B603;
						case 58:
						{
							sbyte b118 = msg.reader().readByte();
							global::Char.myCharz().strLevel = new string[(int)b118];
							int num357;
							for (int num280 = 0; num280 < (int)b118; num280 = num357 + 1)
							{
								string text17 = msg.reader().readUTF();
								global::Char.myCharz().strLevel[num280] = text17;
								num357 = num280;
							}
							Res.outz("---   xong  level caption cmd : " + msg.command.ToString());
							goto IL_B603;
						}
						case 62:
						{
							sbyte b119 = msg.reader().readByte();
							Res.outz("cAction= " + b119.ToString());
							bool flag285 = b119 == 0;
							if (flag285)
							{
								global::Char.myCharz().head = (int)msg.reader().readShort();
								global::Char.myCharz().setDefaultPart();
								int num281 = (int)msg.reader().readUnsignedByte();
								Res.outz("num body = " + num281.ToString());
								global::Char.myCharz().arrItemBody = new Item[num281];
								int num357;
								for (int num282 = 0; num282 < num281; num282 = num357 + 1)
								{
									short num283 = msg.reader().readShort();
									bool flag286 = num283 != -1;
									if (flag286)
									{
										global::Char.myCharz().arrItemBody[num282] = new Item();
										global::Char.myCharz().arrItemBody[num282].template = ItemTemplates.get(num283);
										int num284 = (int)global::Char.myCharz().arrItemBody[num282].template.type;
										global::Char.myCharz().arrItemBody[num282].quantity = msg.reader().readInt();
										global::Char.myCharz().arrItemBody[num282].info = msg.reader().readUTF();
										global::Char.myCharz().arrItemBody[num282].content = msg.reader().readUTF();
										int num285 = (int)msg.reader().readUnsignedByte();
										bool flag287 = num285 != 0;
										if (flag287)
										{
											global::Char.myCharz().arrItemBody[num282].itemOption = new ItemOption[num285];
											for (int num286 = 0; num286 < global::Char.myCharz().arrItemBody[num282].itemOption.Length; num286 = num357 + 1)
											{
												int num287 = (int)msg.reader().readUnsignedByte();
												int param4 = (int)msg.reader().readUnsignedShort();
												bool flag288 = num287 != -1;
												if (flag288)
												{
													global::Char.myCharz().arrItemBody[num282].itemOption[num286] = new ItemOption(num287, param4);
												}
												num357 = num286;
											}
										}
										bool flag289 = num284 == 0;
										if (flag289)
										{
											global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[num282].template.part;
										}
										else
										{
											bool flag290 = num284 == 1;
											if (flag290)
											{
												global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[num282].template.part;
											}
										}
									}
									num357 = num282;
								}
							}
							goto IL_B603;
						}
						case 63:
						{
							sbyte b120 = msg.reader().readByte();
							Res.outz("cAction= " + b120.ToString());
							bool flag291 = b120 == 0;
							if (flag291)
							{
								int num288 = (int)msg.reader().readUnsignedByte();
								global::Char.myCharz().arrItemBag = new Item[num288];
								GameScr.hpPotion = 0;
								Res.outz("numC=" + num288.ToString());
								int num357;
								for (int num289 = 0; num289 < num288; num289 = num357 + 1)
								{
									short num290 = msg.reader().readShort();
									bool flag292 = num290 != -1;
									if (flag292)
									{
										global::Char.myCharz().arrItemBag[num289] = new Item();
										global::Char.myCharz().arrItemBag[num289].template = ItemTemplates.get(num290);
										global::Char.myCharz().arrItemBag[num289].quantity = msg.reader().readInt();
										global::Char.myCharz().arrItemBag[num289].info = msg.reader().readUTF();
										global::Char.myCharz().arrItemBag[num289].content = msg.reader().readUTF();
										global::Char.myCharz().arrItemBag[num289].indexUI = num289;
										int num291 = (int)msg.reader().readUnsignedByte();
										bool flag293 = num291 != 0;
										if (flag293)
										{
											global::Char.myCharz().arrItemBag[num289].itemOption = new ItemOption[num291];
											for (int num292 = 0; num292 < global::Char.myCharz().arrItemBag[num289].itemOption.Length; num292 = num357 + 1)
											{
												int num293 = (int)msg.reader().readUnsignedByte();
												int param5 = (int)msg.reader().readUnsignedShort();
												bool flag294 = num293 != -1;
												if (flag294)
												{
													global::Char.myCharz().arrItemBag[num289].itemOption[num292] = new ItemOption(num293, param5);
												}
												num357 = num292;
											}
											global::Char.myCharz().arrItemBag[num289].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemBag[num289]);
										}
										bool flag295 = global::Char.myCharz().arrItemBag[num289].template.type == 11;
										if (flag295)
										{
										}
										bool flag296 = global::Char.myCharz().arrItemBag[num289].template.type == 6;
										if (flag296)
										{
											GameScr.hpPotion += global::Char.myCharz().arrItemBag[num289].quantity;
										}
									}
									num357 = num289;
								}
							}
							bool flag297 = b120 == 2;
							if (flag297)
							{
								sbyte b121 = msg.reader().readByte();
								int quantity = msg.reader().readInt();
								int quantity2 = global::Char.myCharz().arrItemBag[(int)b121].quantity;
								global::Char.myCharz().arrItemBag[(int)b121].quantity = quantity;
								bool flag298 = global::Char.myCharz().arrItemBag[(int)b121].quantity < quantity2 && global::Char.myCharz().arrItemBag[(int)b121].template.type == 6;
								if (flag298)
								{
									GameScr.hpPotion -= quantity2 - global::Char.myCharz().arrItemBag[(int)b121].quantity;
								}
								bool flag299 = global::Char.myCharz().arrItemBag[(int)b121].quantity == 0;
								if (flag299)
								{
									global::Char.myCharz().arrItemBag[(int)b121] = null;
								}
							}
							goto IL_B603;
						}
						case 64:
						{
							sbyte b122 = msg.reader().readByte();
							Res.outz("cAction= " + b122.ToString());
							bool flag300 = b122 == 0;
							if (flag300)
							{
								int num294 = (int)msg.reader().readUnsignedByte();
								global::Char.myCharz().arrItemBox = new Item[num294];
								GameCanvas.panel.hasUse = 0;
								int num357;
								for (int num295 = 0; num295 < num294; num295 = num357 + 1)
								{
									short num296 = msg.reader().readShort();
									bool flag301 = num296 != -1;
									if (flag301)
									{
										global::Char.myCharz().arrItemBox[num295] = new Item();
										global::Char.myCharz().arrItemBox[num295].template = ItemTemplates.get(num296);
										global::Char.myCharz().arrItemBox[num295].quantity = msg.reader().readInt();
										global::Char.myCharz().arrItemBox[num295].info = msg.reader().readUTF();
										global::Char.myCharz().arrItemBox[num295].content = msg.reader().readUTF();
										int num297 = (int)msg.reader().readUnsignedByte();
										bool flag302 = num297 != 0;
										if (flag302)
										{
											global::Char.myCharz().arrItemBox[num295].itemOption = new ItemOption[num297];
											for (int num298 = 0; num298 < global::Char.myCharz().arrItemBox[num295].itemOption.Length; num298 = num357 + 1)
											{
												int num299 = (int)msg.reader().readUnsignedByte();
												int param6 = (int)msg.reader().readUnsignedShort();
												bool flag303 = num299 != -1;
												if (flag303)
												{
													global::Char.myCharz().arrItemBox[num295].itemOption[num298] = new ItemOption(num299, param6);
												}
												num357 = num298;
											}
										}
										Panel panel = GameCanvas.panel;
										Panel panel3 = panel;
										num357 = panel.hasUse;
										panel3.hasUse = num357 + 1;
									}
									num357 = num295;
								}
							}
							bool flag304 = b122 == 1;
							if (flag304)
							{
								bool isBoxClan = false;
								try
								{
									sbyte b123 = msg.reader().readByte();
									bool flag305 = b123 == 1;
									if (flag305)
									{
										isBoxClan = true;
									}
								}
								catch (Exception ex34)
								{
								}
								GameCanvas.panel.setTypeBox();
								GameCanvas.panel.isBoxClan = isBoxClan;
								GameCanvas.panel.show();
							}
							bool flag306 = b122 == 2;
							if (flag306)
							{
								sbyte b124 = msg.reader().readByte();
								int quantity3 = msg.reader().readInt();
								global::Char.myCharz().arrItemBox[(int)b124].quantity = quantity3;
								bool flag307 = global::Char.myCharz().arrItemBox[(int)b124].quantity == 0;
								if (flag307)
								{
									global::Char.myCharz().arrItemBox[(int)b124] = null;
								}
							}
							goto IL_B603;
						}
						case 65:
						{
							sbyte b125 = msg.reader().readByte();
							Res.outz("act= " + b125.ToString());
							bool flag308 = b125 == 0 && GameScr.gI().magicTree != null;
							if (flag308)
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
								Res.outz("curr Peas= " + magicTree.currPeas.ToString());
								magicTree.strInfo = msg.reader().readUTF();
								magicTree.seconds = msg.reader().readInt();
								magicTree.timeToRecieve = magicTree.seconds;
								sbyte b126 = msg.reader().readByte();
								magicTree.peaPostionX = new int[(int)b126];
								magicTree.peaPostionY = new int[(int)b126];
								int num357;
								for (int num300 = 0; num300 < (int)b126; num300 = num357 + 1)
								{
									magicTree.peaPostionX[num300] = (int)msg.reader().readByte();
									magicTree.peaPostionY[num300] = (int)msg.reader().readByte();
									num357 = num300;
								}
								magicTree.isUpdate = msg.reader().readBool();
								magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
								GameScr.gI().magicTree.isUpdateTree = true;
							}
							bool flag309 = b125 == 1;
							if (flag309)
							{
								myVector = new MyVector();
								try
								{
									while (msg.reader().available() > 0)
									{
										string caption6 = msg.reader().readUTF();
										myVector.addElement(new Command(caption6, GameCanvas.instance, 888392, null));
									}
								}
								catch (Exception ex35)
								{
									Cout.println("Loi MAGIC_TREE " + ex35.ToString());
								}
								GameCanvas.menu.startAt(myVector, 3);
							}
							bool flag310 = b125 == 2;
							if (flag310)
							{
								GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
								GameScr.gI().magicTree.seconds = msg.reader().readInt();
								GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
								GameScr.gI().magicTree.isUpdateTree = true;
								GameScr.gI().magicTree.isPeasEffect = true;
							}
							goto IL_B603;
						}
						case 67:
						{
							short num301 = msg.reader().readShort();
							int num302 = msg.reader().readInt();
							sbyte[] array27 = null;
							Image image = null;
							try
							{
								array27 = new sbyte[num302];
								int num357;
								for (int num303 = 0; num303 < num302; num303 = num357 + 1)
								{
									array27[num303] = msg.reader().readByte();
									num357 = num303;
								}
								image = Image.createImage(array27, 0, num302);
								BgItem.imgNew.put(num301.ToString() + string.Empty, image);
							}
							catch (Exception ex36)
							{
								array27 = null;
								BgItem.imgNew.put(num301.ToString() + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
							}
							bool flag311 = array27 != null;
							if (flag311)
							{
								bool flag312 = mGraphics.zoomLevel > 1;
								if (flag312)
								{
									Rms.saveRMS(mGraphics.zoomLevel.ToString() + "bgItem" + num301.ToString(), array27);
								}
								BgItemMn.blendcurrBg(num301, image);
							}
							goto IL_B603;
						}
						case 68:
						{
							TileMap.vItemBg.removeAllElements();
							short num304 = msg.reader().readShort();
							Res.err("[ITEM_BACKGROUND] nItem= " + num304.ToString());
							int num357;
							for (int num305 = 0; num305 < (int)num304; num305 = num357 + 1)
							{
								BgItem bgItem = new BgItem();
								bgItem.id = num305;
								bgItem.idImage = msg.reader().readShort();
								bgItem.layer = msg.reader().readByte();
								bgItem.dx = (int)msg.reader().readShort();
								bgItem.dy = (int)msg.reader().readShort();
								sbyte b127 = msg.reader().readByte();
								bgItem.tileX = new int[(int)b127];
								bgItem.tileY = new int[(int)b127];
								for (int num306 = 0; num306 < (int)b127; num306 = num357 + 1)
								{
									bgItem.tileX[num305] = (int)msg.reader().readByte();
									bgItem.tileY[num305] = (int)msg.reader().readByte();
									num357 = num306;
								}
								TileMap.vItemBg.addElement(bgItem);
								num357 = num305;
							}
							goto IL_B603;
						}
						case 69:
							this.messageSubCommand(msg);
							goto IL_B603;
						case 70:
							this.messageNotLogin(msg);
							goto IL_B603;
						case 71:
							this.messageNotMap(msg);
							goto IL_B603;
						case 73:
						{
							ServerListScreen.testConnect = 2;
							GameCanvas.debug("SA2", 2);
							GameCanvas.startOKDlg(msg.reader().readUTF());
							InfoDlg.hide();
							LoginScr.isContinueToLogin = false;
							global::Char.isLoadingMap = false;
							bool flag313 = GameCanvas.currentScreen == GameCanvas.loginScr;
							if (flag313)
							{
								GameCanvas.serverScreen.switchToMe();
							}
							goto IL_B603;
						}
						case 74:
							GameCanvas.debug("SA3", 2);
							GameScr.info1.addInfo(msg.reader().readUTF(), 0);
							goto IL_B603;
						case 75:
						{
							bool flag314 = GameCanvas.currentScreen is GameScr;
							if (flag314)
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
							catch (Exception ex37)
							{
								Service.gI().requestMaptemplate(TileMap.mapID);
								this.messWait = msg;
								return;
							}
							this.loadInfoMap(msg);
							try
							{
								sbyte b128 = msg.reader().readByte();
								TileMap.isMapDouble = (b128 != 0);
							}
							catch (Exception ex38)
							{
							}
							GameScr.cmx = GameScr.cmtoX;
							GameScr.cmy = GameScr.cmtoY;
							goto IL_B603;
						}
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
							goto IL_B603;
						case 78:
						{
							GameCanvas.debug("SA60", 2);
							short num307 = msg.reader().readShort();
							int num357;
							for (int num308 = 0; num308 < GameScr.vItemMap.size(); num308 = num357 + 1)
							{
								bool flag315 = ((ItemMap)GameScr.vItemMap.elementAt(num308)).itemMapID == (int)num307;
								if (flag315)
								{
									GameScr.vItemMap.removeElementAt(num308);
									break;
								}
								num357 = num308;
							}
							goto IL_B603;
						}
						case 79:
						{
							GameCanvas.debug("SA61", 2);
							global::Char.myCharz().itemFocus = null;
							short num309 = msg.reader().readShort();
							int num357;
							for (int num310 = 0; num310 < GameScr.vItemMap.size(); num310 = num357 + 1)
							{
								ItemMap itemMap5 = (ItemMap)GameScr.vItemMap.elementAt(num310);
								bool flag316 = itemMap5.itemMapID == (int)num309;
								if (flag316)
								{
									itemMap5.setPoint(global::Char.myCharz().cx, global::Char.myCharz().cy - 10);
									string text18 = msg.reader().readUTF();
									i = 0;
									try
									{
										i = (int)msg.reader().readShort();
										bool flag317 = itemMap5.template.type == 9;
										if (flag317)
										{
											i = (int)msg.reader().readShort();
											global::Char char16 = global::Char.myCharz();
											char16.xu += (long)i;
											global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
										}
										else
										{
											bool flag318 = itemMap5.template.type == 10;
											if (flag318)
											{
												i = (int)msg.reader().readShort();
												global::Char char16 = global::Char.myCharz();
												char16.luong += i;
												global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
											}
											else
											{
												bool flag319 = itemMap5.template.type == 34;
												if (flag319)
												{
													i = (int)msg.reader().readShort();
													global::Char char16 = global::Char.myCharz();
													char16.luongKhoa += i;
													global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
												}
											}
										}
									}
									catch (Exception ex39)
									{
									}
									bool flag320 = text18.Equals(string.Empty);
									if (flag320)
									{
										bool flag321 = itemMap5.template.type == 9;
										if (flag321)
										{
											GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.YELLOW);
											SoundMn.gI().getItem();
										}
										else
										{
											bool flag322 = itemMap5.template.type == 10;
											if (flag322)
											{
												GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.GREEN);
												SoundMn.gI().getItem();
											}
											else
											{
												bool flag323 = itemMap5.template.type == 34;
												if (flag323)
												{
													GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.RED);
													SoundMn.gI().getItem();
												}
												else
												{
													GameScr.info1.addInfo(mResources.you_receive + " " + ((i <= 0) ? string.Empty : (i.ToString() + " ")) + itemMap5.template.name, 0);
													SoundMn.gI().getItem();
												}
											}
										}
										bool flag324 = i > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 4683;
										if (flag324)
										{
											ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
											ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
										}
									}
									else
									{
										bool flag325 = text18.Length == 1;
										if (flag325)
										{
											Cout.LogError3("strInf.Length =1:  " + text18);
										}
										else
										{
											GameScr.info1.addInfo(text18, 0);
										}
									}
									break;
								}
								num357 = num310;
							}
							goto IL_B603;
						}
						case 80:
						{
							GameCanvas.debug("SA62", 2);
							short num311 = msg.reader().readShort();
							@char = GameScr.findCharInMap(msg.reader().readInt());
							int num312 = 0;
							while (num312 < GameScr.vItemMap.size())
							{
								ItemMap itemMap6 = (ItemMap)GameScr.vItemMap.elementAt(num312);
								bool flag326 = itemMap6.itemMapID == (int)num311;
								if (flag326)
								{
									bool flag327 = @char == null;
									if (flag327)
									{
										return;
									}
									itemMap6.setPoint(@char.cx, @char.cy - 10);
									bool flag328 = itemMap6.x < @char.cx;
									if (flag328)
									{
										@char.cdir = -1;
									}
									else
									{
										bool flag329 = itemMap6.x > @char.cx;
										if (flag329)
										{
											@char.cdir = 1;
										}
									}
									break;
								}
								else
								{
									int num357 = num312;
									num312 = num357 + 1;
								}
							}
							goto IL_B603;
						}
						case 81:
						{
							GameCanvas.debug("SA63", 2);
							int num313 = (int)msg.reader().readByte();
							GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), global::Char.myCharz().arrItemBag[num313].template.id, global::Char.myCharz().cx, global::Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
							global::Char.myCharz().arrItemBag[num313] = null;
							goto IL_B603;
						}
						case 85:
						{
							GameCanvas.debug("SA64", 2);
							@char = GameScr.findCharInMap(msg.reader().readInt());
							bool flag330 = @char == null;
							if (flag330)
							{
								return;
							}
							GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
							goto IL_B603;
						}
						}
						break;
					}
				}
				else
				{
					switch (num356)
					{
					case 95:
					{
						GameCanvas.debug("SA76", 2);
						@char = GameScr.findCharInMap(msg.reader().readInt());
						bool flag331 = @char == null;
						if (flag331)
						{
							return;
						}
						GameCanvas.debug("SA76v1", 2);
						bool flag332 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
						if (flag332)
						{
							@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
						}
						GameCanvas.debug("SA76v2", 2);
						@char.attMobs = new Mob[(int)msg.reader().readByte()];
						int num357;
						for (int num314 = 0; num314 < @char.attMobs.Length; num314 = num357 + 1)
						{
							Mob mob17 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
							@char.attMobs[num314] = mob17;
							bool flag333 = num314 == 0;
							if (flag333)
							{
								bool flag334 = @char.cx <= mob17.x;
								if (flag334)
								{
									@char.cdir = 1;
								}
								else
								{
									@char.cdir = -1;
								}
							}
							num357 = num314;
						}
						GameCanvas.debug("SA76v3", 2);
						@char.charFocus = null;
						@char.mobFocus = @char.attMobs[0];
						global::Char[] array28 = new global::Char[10];
						i = 0;
						try
						{
							for (i = 0; i < array28.Length; i = num357 + 1)
							{
								int num315 = msg.reader().readInt();
								bool flag335 = num315 == global::Char.myCharz().charID;
								global::Char char11;
								if (flag335)
								{
									char11 = global::Char.myCharz();
								}
								else
								{
									char11 = GameScr.findCharInMap(num315);
								}
								array28[i] = char11;
								bool flag336 = i == 0;
								if (flag336)
								{
									bool flag337 = @char.cx <= char11.cx;
									if (flag337)
									{
										@char.cdir = 1;
									}
									else
									{
										@char.cdir = -1;
									}
								}
								num357 = i;
							}
						}
						catch (Exception ex40)
						{
							Cout.println("Loi PLAYER_ATTACK_N_P " + ex40.ToString());
						}
						GameCanvas.debug("SA76v4", 2);
						bool flag338 = i > 0;
						if (flag338)
						{
							@char.attChars = new global::Char[i];
							for (i = 0; i < @char.attChars.Length; i = num357 + 1)
							{
								@char.attChars[i] = array28[i];
								num357 = i;
							}
							@char.charFocus = @char.attChars[0];
							@char.mobFocus = null;
						}
						GameCanvas.debug("SA76v5", 2);
						goto IL_B603;
					}
					case 96:
					case 97:
					case 98:
						break;
					case 99:
						this.readLogin(msg);
						goto IL_B603;
					case 100:
					{
						bool flag15 = msg.reader().readBool();
						Res.outz("isRes= " + flag15.ToString());
						bool flag339 = !flag15;
						if (flag339)
						{
							GameCanvas.startOKDlg(msg.reader().readUTF());
						}
						else
						{
							GameCanvas.loginScr.isLogin2 = false;
							Rms.saveRMSString("userAo" + ServerListScreen.ipSelect.ToString(), string.Empty);
							GameCanvas.endDlg();
							GameCanvas.loginScr.doLogin();
						}
						goto IL_B603;
					}
					case 101:
					{
						global::Char.isLoadingMap = false;
						LoginScr.isLoggingIn = false;
						bool flag340 = !GameScr.isLoadAllData;
						if (flag340)
						{
							GameScr.gI().initSelectChar();
						}
						BgItem.clearHashTable();
						GameCanvas.endDlg();
						CreateCharScr.isCreateChar = true;
						CreateCharScr.gI().switchToMe();
						goto IL_B603;
					}
					default:
						if (num356 == 105)
						{
							GameCanvas.debug("SA70", 2);
							global::Char.myCharz().xu = msg.reader().readLong();
							global::Char.myCharz().luong = msg.reader().readInt();
							global::Char.myCharz().luongKhoa = msg.reader().readInt();
							global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
							global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
							global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
							GameCanvas.endDlg();
							goto IL_B603;
						}
						break;
					}
				}
			}
			else if (num356 <= 110)
			{
				if (num356 == 106)
				{
					sbyte type = msg.reader().readByte();
					short id2 = msg.reader().readShort();
					string info7 = msg.reader().readUTF();
					GameCanvas.panel.saleRequest(type, info7, id2);
					goto IL_B603;
				}
				if (num356 == 110)
				{
					GameCanvas.debug("SA9", 2);
					int num316 = (int)msg.reader().readByte();
					sbyte b129 = msg.reader().readByte();
					bool flag341 = b129 != 0;
					if (flag341)
					{
						Mob.arrMobTemplate[num316].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b129);
					}
					else
					{
						Mob.arrMobTemplate[num316].data.readData(NinjaUtil.readByteArray(msg));
					}
					int num357;
					for (int num317 = 0; num317 < GameScr.vMob.size(); num317 = num357 + 1)
					{
						Mob mob18 = (Mob)GameScr.vMob.elementAt(num317);
						bool flag342 = mob18.templateId == num316;
						if (flag342)
						{
							mob18.w = Mob.arrMobTemplate[num316].data.width;
							mob18.h = Mob.arrMobTemplate[num316].data.height;
						}
						num357 = num317;
					}
					sbyte[] array29 = NinjaUtil.readByteArray(msg);
					Image img = Image.createImage(array29, 0, array29.Length);
					Mob.arrMobTemplate[num316].data.img = img;
					int num318 = (int)msg.reader().readByte();
					Mob.arrMobTemplate[num316].data.typeData = num318;
					bool flag343 = num318 == 1 || num318 == 2;
					if (flag343)
					{
						this.readFrameBoss(msg, num316);
					}
					goto IL_B603;
				}
			}
			else
			{
				if (num356 == 119)
				{
					this.phuban_Info(msg);
					goto IL_B603;
				}
				if (num356 == 123)
				{
					this.read_opt(msg);
					goto IL_B603;
				}
				if (num356 == 126)
				{
					myVector = new MyVector();
					string text19 = msg.reader().readUTF();
					int num319 = (int)msg.reader().readByte();
					int num357;
					for (int num320 = 0; num320 < num319; num320 = num357 + 1)
					{
						string caption7 = msg.reader().readUTF();
						short num321 = msg.reader().readShort();
						myVector.addElement(new Command(caption7, GameCanvas.instance, 88819, num321));
						num357 = num320;
					}
					GameCanvas.menu.startWithoutCloseButton(myVector, 3);
					goto IL_B603;
				}
			}
			bool flag344 = command != -112;
			if (flag344)
			{
				bool flag345 = command == -107;
				if (flag345)
				{
					sbyte b130 = msg.reader().readByte();
					bool flag346 = b130 == 0;
					if (flag346)
					{
						global::Char.myCharz().havePet = false;
					}
					bool flag347 = b130 == 1;
					if (flag347)
					{
						global::Char.myCharz().havePet = true;
					}
					bool flag348 = b130 == 2;
					if (flag348)
					{
						InfoDlg.hide();
						global::Char.myPetz().head = (int)msg.reader().readShort();
						global::Char.myPetz().setDefaultPart();
						int num322 = (int)msg.reader().readUnsignedByte();
						Res.outz("num body = " + num322.ToString());
						global::Char.myPetz().arrItemBody = new Item[num322];
						int num357;
						for (int num323 = 0; num323 < num322; num323 = num357 + 1)
						{
							short num324 = msg.reader().readShort();
							Res.outz("template id= " + num324.ToString());
							bool flag349 = num324 != -1;
							if (flag349)
							{
								Res.outz("1");
								global::Char.myPetz().arrItemBody[num323] = new Item();
								global::Char.myPetz().arrItemBody[num323].template = ItemTemplates.get(num324);
								int num325 = (int)global::Char.myPetz().arrItemBody[num323].template.type;
								global::Char.myPetz().arrItemBody[num323].quantity = msg.reader().readInt();
								Res.outz("3");
								global::Char.myPetz().arrItemBody[num323].info = msg.reader().readUTF();
								global::Char.myPetz().arrItemBody[num323].content = msg.reader().readUTF();
								int num326 = (int)msg.reader().readUnsignedByte();
								Res.outz("option size= " + num326.ToString());
								bool flag350 = num326 != 0;
								if (flag350)
								{
									global::Char.myPetz().arrItemBody[num323].itemOption = new ItemOption[num326];
									for (int num327 = 0; num327 < global::Char.myPetz().arrItemBody[num323].itemOption.Length; num327 = num357 + 1)
									{
										int num328 = (int)msg.reader().readUnsignedByte();
										int param7 = (int)msg.reader().readUnsignedShort();
										bool flag351 = num328 != -1;
										if (flag351)
										{
											global::Char.myPetz().arrItemBody[num323].itemOption[num327] = new ItemOption(num328, param7);
										}
										num357 = num327;
									}
								}
								bool flag352 = num325 == 0;
								if (flag352)
								{
									global::Char.myPetz().body = (int)global::Char.myPetz().arrItemBody[num323].template.part;
								}
								else
								{
									bool flag353 = num325 == 1;
									if (flag353)
									{
										global::Char.myPetz().leg = (int)global::Char.myPetz().arrItemBody[num323].template.part;
									}
								}
							}
							num357 = num323;
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
						string str2 = "SKILLENT = ";
						Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
						Res.outz(str2 + ((arrPetSkill != null) ? arrPetSkill.ToString() : null));
						for (int num329 = 0; num329 < global::Char.myPetz().arrPetSkill.Length; num329 = num357 + 1)
						{
							short num330 = msg.reader().readShort();
							bool flag354 = num330 != -1;
							if (flag354)
							{
								global::Char.myPetz().arrPetSkill[num329] = Skills.get(num330);
							}
							else
							{
								global::Char.myPetz().arrPetSkill[num329] = new Skill();
								global::Char.myPetz().arrPetSkill[num329].template = null;
								global::Char.myPetz().arrPetSkill[num329].moreInfo = msg.reader().readUTF();
							}
							num357 = num329;
						}
						bool flag355 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
						if (flag355)
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
				sbyte b131 = msg.reader().readByte();
				bool flag356 = b131 == 0;
				if (flag356)
				{
					sbyte mobIndex = msg.reader().readByte();
					GameScr.findMobInMap(mobIndex).clearBody();
				}
				bool flag357 = b131 == 1;
				if (flag357)
				{
					sbyte mobIndex2 = msg.reader().readByte();
					GameScr.findMobInMap(mobIndex2).setBody(msg.reader().readShort());
				}
			}
			IL_B603:
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
				catch (Exception ex41)
				{
					Cout.println("Loi tai ME_DIE " + msg.command.ToString());
				}
				global::Char.myCharz().countKill = 0;
				goto IL_D77E;
			case 1:
			{
				GameCanvas.debug("SA90", 2);
				bool flag358 = global::Char.myCharz().wdx != 0 || global::Char.myCharz().wdy != 0;
				if (flag358)
				{
					global::Char.myCharz().cx = (int)global::Char.myCharz().wdx;
					global::Char.myCharz().cy = (int)global::Char.myCharz().wdy;
					global::Char.myCharz().wdx = (global::Char.myCharz().wdy = 0);
				}
				global::Char.myCharz().liveFromDead();
				global::Char.myCharz().isLockMove = false;
				global::Char.myCharz().meDead = false;
				goto IL_D77E;
			}
			case 4:
			{
				GameCanvas.debug("SA82", 2);
				int num331 = (int)msg.reader().readUnsignedByte();
				bool flag359 = num331 > GameScr.vMob.size() - 1 || num331 < 0;
				if (flag359)
				{
					return;
				}
				Mob mob19 = (Mob)GameScr.vMob.elementAt(num331);
				mob19.sys = (int)msg.reader().readByte();
				mob19.levelBoss = msg.reader().readByte();
				bool flag360 = mob19.levelBoss != 0;
				if (flag360)
				{
					mob19.typeSuperEff = Res.random(0, 3);
				}
				mob19.x = mob19.xFirst;
				mob19.y = mob19.yFirst;
				mob19.status = 5;
				mob19.injureThenDie = false;
				mob19.hp = msg.reader().readInt();
				mob19.maxHp = mob19.hp;
				mob19.updateHp_bar();
				ServerEffect.addServerEffect(60, mob19.x, mob19.y, 1);
				goto IL_D77E;
			}
			case 5:
			{
				Res.outz("SERVER SEND MOB DIE");
				GameCanvas.debug("SA85", 2);
				Mob mob20 = null;
				try
				{
					mob20 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex42)
				{
					Cout.println("LOi tai NPC_DIE cmd " + msg.command.ToString());
				}
				bool flag361 = mob20 != null && mob20.status != 0 && mob20.status != 0;
				if (flag361)
				{
					mob20.startDie();
					try
					{
						int num332 = msg.readInt3Byte();
						bool flag16 = msg.reader().readBool();
						bool flag362 = flag16;
						if (flag362)
						{
							GameScr.startFlyText("-" + num332.ToString(), mob20.x, mob20.y - mob20.h, 0, -2, mFont.FATAL);
						}
						else
						{
							GameScr.startFlyText("-" + num332.ToString(), mob20.x, mob20.y - mob20.h, 0, -2, mFont.ORANGE);
						}
						sbyte b132 = msg.reader().readByte();
						int num357;
						for (int num333 = 0; num333 < (int)b132; num333 = num357 + 1)
						{
							ItemMap itemMap7 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob20.x, mob20.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
							int num334 = msg.reader().readInt();
							itemMap7.playerId = num334;
							Res.outz(string.Concat(new object[]
							{
								"playerid= ",
								num334,
								" my id= ",
								global::Char.myCharz().charID
							}));
							GameScr.vItemMap.addElement(itemMap7);
							bool flag363 = Res.abs(itemMap7.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap7.x - global::Char.myCharz().cx) < 24;
							if (flag363)
							{
								global::Char.myCharz().charFocus = null;
							}
							num357 = num333;
						}
					}
					catch (Exception ex43)
					{
					}
				}
				goto IL_D77E;
			}
			case 6:
			{
				GameCanvas.debug("SA86", 2);
				Mob mob21 = null;
				try
				{
					int index6 = (int)msg.reader().readUnsignedByte();
					mob21 = (Mob)GameScr.vMob.elementAt(index6);
				}
				catch (Exception ex44)
				{
					Res.outz(string.Concat(new object[]
					{
						"Loi tai NPC_ATTACK_ME ",
						msg.command,
						" err= ",
						ex44.StackTrace
					}));
				}
				bool flag364 = mob21 != null;
				if (flag364)
				{
					global::Char.myCharz().isDie = false;
					global::Char.isLockKey = false;
					int num335 = msg.readInt3Byte();
					int num336;
					try
					{
						num336 = msg.readInt3Byte();
					}
					catch (Exception ex45)
					{
						num336 = 0;
					}
					bool isBusyAttackSomeOne4 = mob21.isBusyAttackSomeOne;
					if (isBusyAttackSomeOne4)
					{
						global::Char.myCharz().doInjure(num335, num336, false, true);
					}
					else
					{
						mob21.dame = num335;
						mob21.dameMp = num336;
						mob21.setAttack(global::Char.myCharz());
					}
				}
				goto IL_D77E;
			}
			case 7:
			{
				GameCanvas.debug("SA87", 2);
				Mob mob22 = null;
				try
				{
					mob22 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex46)
				{
				}
				GameCanvas.debug("SA87x1", 2);
				bool flag365 = mob22 != null;
				if (flag365)
				{
					GameCanvas.debug("SA87x2", 2);
					@char = GameScr.findCharInMap(msg.reader().readInt());
					bool flag366 = @char == null;
					if (flag366)
					{
						return;
					}
					GameCanvas.debug("SA87x3", 2);
					int num337 = msg.readInt3Byte();
					mob22.dame = @char.cHP - num337;
					@char.cHPNew = num337;
					GameCanvas.debug("SA87x4", 2);
					try
					{
						@char.cMP = msg.readInt3Byte();
					}
					catch (Exception ex47)
					{
					}
					GameCanvas.debug("SA87x5", 2);
					bool isBusyAttackSomeOne5 = mob22.isBusyAttackSomeOne;
					if (isBusyAttackSomeOne5)
					{
						@char.doInjure(mob22.dame, 0, false, true);
					}
					else
					{
						mob22.setAttack(@char);
					}
					GameCanvas.debug("SA87x6", 2);
				}
				goto IL_D77E;
			}
			case 8:
			{
				GameCanvas.debug("SA83", 2);
				Mob mob23 = null;
				try
				{
					mob23 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex48)
				{
				}
				GameCanvas.debug("SA83v1", 2);
				bool flag367 = mob23 != null;
				if (flag367)
				{
					mob23.hp = msg.readInt3Byte();
					mob23.updateHp_bar();
					int num338 = msg.readInt3Byte();
					bool flag368 = num338 == 1;
					if (flag368)
					{
						return;
					}
					bool flag369 = num338 > 1;
					if (flag369)
					{
						mob23.setInjure();
					}
					bool flag17 = false;
					try
					{
						flag17 = msg.reader().readBoolean();
					}
					catch (Exception ex49)
					{
					}
					sbyte b133 = msg.reader().readByte();
					bool flag370 = b133 != -1;
					if (flag370)
					{
						EffecMn.addEff(new Effect((int)b133, mob23.x, mob23.getY(), 3, 1, -1));
					}
					GameCanvas.debug("SA83v2", 2);
					bool flag371 = flag17;
					if (flag371)
					{
						GameScr.startFlyText("-" + num338.ToString(), mob23.x, mob23.getY() - mob23.getH(), 0, -2, mFont.FATAL);
					}
					else
					{
						bool flag372 = num338 == 0;
						if (flag372)
						{
							mob23.x = mob23.xFirst;
							mob23.y = mob23.yFirst;
							GameScr.startFlyText(mResources.miss, mob23.x, mob23.getY() - mob23.getH(), 0, -2, mFont.MISS);
						}
						else
						{
							bool flag373 = num338 > 1;
							if (flag373)
							{
								GameScr.startFlyText("-" + num338.ToString(), mob23.x, mob23.getY() - mob23.getH(), 0, -2, mFont.ORANGE);
							}
						}
					}
				}
				GameCanvas.debug("SA83v3", 2);
				goto IL_D77E;
			}
			case 9:
			{
				GameCanvas.debug("SA89", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag374 = @char == null;
				if (flag374)
				{
					return;
				}
				@char.cPk = msg.reader().readByte();
				@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
				goto IL_D77E;
			}
			case 10:
			{
				GameCanvas.debug("SA80", 2);
				int num339 = msg.reader().readInt();
				int num357;
				for (int num340 = 0; num340 < GameScr.vCharInMap.size(); num340 = num357 + 1)
				{
					global::Char char12 = null;
					try
					{
						char12 = (global::Char)GameScr.vCharInMap.elementAt(num340);
					}
					catch (Exception ex50)
					{
					}
					bool flag375 = char12 == null;
					if (flag375)
					{
						break;
					}
					bool flag376 = char12.charID == num339;
					if (flag376)
					{
						GameCanvas.debug("SA8x2y" + num340.ToString(), 2);
						char12.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort(), 0);
						char12.lastUpdateTime = mSystem.currentTimeMillis();
						break;
					}
					num357 = num340;
				}
				GameCanvas.debug("SA80x3", 2);
				goto IL_D77E;
			}
			case 11:
			{
				GameCanvas.debug("SA81", 2);
				int num341 = msg.reader().readInt();
				int num357;
				for (int num342 = 0; num342 < GameScr.vCharInMap.size(); num342 = num357 + 1)
				{
					global::Char char13 = (global::Char)GameScr.vCharInMap.elementAt(num342);
					bool flag377 = char13 != null && char13.charID == num341;
					if (flag377)
					{
						bool flag378 = !char13.isInvisiblez && !char13.isUsePlane;
						if (flag378)
						{
							ServerEffect.addServerEffect(60, char13.cx, char13.cy, 1);
						}
						bool flag379 = !char13.isUsePlane;
						if (flag379)
						{
							GameScr.vCharInMap.removeElementAt(num342);
						}
						return;
					}
					num357 = num342;
				}
				goto IL_D77E;
			}
			case 12:
			{
				GameCanvas.debug("SA79", 2);
				int charID = msg.reader().readInt();
				int num343 = msg.reader().readInt();
				bool flag380 = num343 != -100;
				global::Char char14;
				if (flag380)
				{
					char14 = new global::Char();
					char14.charID = charID;
					char14.clanID = num343;
				}
				else
				{
					char14 = new Mabu();
					char14.charID = charID;
					char14.clanID = num343;
				}
				bool flag381 = char14.clanID == -2;
				if (flag381)
				{
					char14.isCopy = true;
				}
				bool flag382 = this.readCharInfo(char14, msg);
				if (flag382)
				{
					sbyte b134 = msg.reader().readByte();
					bool flag383 = char14.cy <= 10 && b134 != 0 && b134 != 2;
					if (flag383)
					{
						Res.outz(string.Concat(new object[]
						{
							"nhân vật bay trên trời xuống x= ",
							char14.cx,
							" y= ",
							char14.cy
						}));
						Teleport teleport = new Teleport(char14.cx, char14.cy, char14.head, char14.cdir, 1, false, (b134 != 1) ? ((int)b134) : char14.cgender);
						teleport.id = char14.charID;
						char14.isTeleport = true;
						Teleport.addTeleport(teleport);
					}
					bool flag384 = b134 == 2;
					if (flag384)
					{
						char14.show();
					}
					int num357;
					for (int num344 = 0; num344 < GameScr.vMob.size(); num344 = num357 + 1)
					{
						Mob mob24 = (Mob)GameScr.vMob.elementAt(num344);
						bool flag385 = mob24 != null && mob24.isMobMe && mob24.mobId == char14.charID;
						if (flag385)
						{
							Res.outz("co 1 con quai");
							char14.mobMe = mob24;
							char14.mobMe.x = char14.cx;
							char14.mobMe.y = char14.cy - 40;
							break;
						}
						num357 = num344;
					}
					bool flag386 = GameScr.findCharInMap(char14.charID) == null;
					if (flag386)
					{
						GameScr.vCharInMap.addElement(char14);
					}
					char14.isMonkey = msg.reader().readByte();
					short num345 = msg.reader().readShort();
					Res.outz("mount id= " + num345.ToString() + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					bool flag387 = num345 != -1;
					if (flag387)
					{
						char14.isHaveMount = true;
						bool flag388 = num345 == 346 || num345 == 347 || num345 == 348;
						if (flag388)
						{
							char14.isMountVip = false;
						}
						else
						{
							bool flag389 = num345 == 349 || num345 == 350 || num345 == 351;
							if (flag389)
							{
								char14.isMountVip = true;
							}
							else
							{
								bool flag390 = num345 == 396;
								if (flag390)
								{
									char14.isEventMount = true;
								}
								else
								{
									bool flag391 = num345 == 532;
									if (flag391)
									{
										char14.isSpeacialMount = true;
									}
									else
									{
										bool flag392 = num345 >= global::Char.ID_NEW_MOUNT;
										if (flag392)
										{
											char14.idMount = num345;
										}
									}
								}
							}
						}
					}
					else
					{
						char14.isHaveMount = false;
					}
				}
				sbyte b135 = msg.reader().readByte();
				Res.outz("addplayer:   " + b135.ToString());
				char14.cFlag = b135;
				char14.isNhapThe = (msg.reader().readByte() == 1);
				try
				{
					char14.idAuraEff = msg.reader().readShort();
					char14.idEff_Set_Item = (short)msg.reader().readSByte();
					char14.idHat = msg.reader().readShort();
					bool flag393 = char14.bag >= 201 && char14.bag < 255;
					if (flag393)
					{
						char14.addEffChar(new Effect(char14.bag, char14, 2, -1, 10, 1)
						{
							typeEff = 5
						});
					}
					else
					{
						int num357;
						for (int num346 = 0; num346 < 54; num346 = num357 + 1)
						{
							char14.removeEffChar(0, 201 + num346);
							num357 = num346;
						}
					}
				}
				catch (Exception ex51)
				{
					Res.outz("cmd: -5 err: " + ex51.StackTrace);
				}
				GameScr.gI().getFlagImage(char14.charID, char14.cFlag);
				goto IL_D77E;
			}
			case 14:
			{
				GameCanvas.debug("SA78", 2);
				sbyte b136 = msg.reader().readByte();
				int num347 = msg.reader().readInt();
				bool flag394 = b136 == 0;
				if (flag394)
				{
					global::Char char16 = global::Char.myCharz();
					char16.cPower += (long)num347;
				}
				bool flag395 = b136 == 1;
				if (flag395)
				{
					global::Char char16 = global::Char.myCharz();
					char16.cTiemNang += (long)num347;
				}
				bool flag396 = b136 == 2;
				if (flag396)
				{
					global::Char char16 = global::Char.myCharz();
					char16.cPower += (long)num347;
					char16 = global::Char.myCharz();
					char16.cTiemNang += (long)num347;
				}
				global::Char.myCharz().applyCharLevelPercent();
				bool flag397 = global::Char.myCharz().cTypePk != 3;
				if (flag397)
				{
					GameScr.startFlyText(((num347 <= 0) ? string.Empty : "+") + num347.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -4, mFont.GREEN);
					bool flag398 = num347 > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5002;
					if (flag398)
					{
						ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
						ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
					}
				}
				goto IL_D77E;
			}
			case 15:
			{
				GameCanvas.debug("SA77", 22);
				int num348 = msg.reader().readInt();
				global::Char char16 = global::Char.myCharz();
				char16.yen += num348;
				GameScr.startFlyText((num348 <= 0) ? (string.Empty + num348.ToString()) : ("+" + num348.ToString()), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				goto IL_D77E;
			}
			case 16:
			{
				GameCanvas.debug("SA77", 222);
				int num349 = msg.reader().readInt();
				global::Char char16 = global::Char.myCharz();
				char16.xu += (long)num349;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				char16 = global::Char.myCharz();
				char16.yen -= num349;
				GameScr.startFlyText("+" + num349.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				goto IL_D77E;
			}
			}
			switch (command2)
			{
			case 95:
			{
				GameCanvas.debug("SA77", 22);
				int num350 = msg.reader().readInt();
				global::Char char16 = global::Char.myCharz();
				char16.xu += (long)num350;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				GameScr.startFlyText((num350 <= 0) ? (string.Empty + num350.ToString()) : ("+" + num350.ToString()), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			case 96:
				GameCanvas.debug("SA77a", 22);
				global::Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
				break;
			case 97:
			{
				sbyte b137 = msg.reader().readByte();
				int num357;
				for (int num351 = 0; num351 < global::Char.myCharz().taskOrders.size(); num351 = num357 + 1)
				{
					TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(num351);
					bool flag399 = taskOrder.taskId == (int)b137;
					if (flag399)
					{
						taskOrder.count = (int)msg.reader().readShort();
						break;
					}
					num357 = num351;
				}
				break;
			}
			default:
			{
				int num358 = (int)(command2 + 75);
				int num359 = num358;
				if (num359 != 0)
				{
					if (num359 != 2)
					{
						bool flag400 = command2 != 18;
						if (flag400)
						{
							bool flag401 = command2 != 19;
							if (flag401)
							{
								bool flag402 = command2 != 44;
								if (flag402)
								{
									bool flag403 = command2 != 45;
									if (flag403)
									{
										bool flag404 = command2 != 66;
										if (flag404)
										{
											bool flag405 = command2 == 74;
											if (flag405)
											{
												GameCanvas.debug("SA85", 2);
												Mob mob25 = null;
												try
												{
													mob25 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
												}
												catch (Exception ex52)
												{
													Cout.println("Loi tai NPC CHANGE " + msg.command.ToString());
												}
												bool flag406 = mob25 != null && mob25.status != 0 && mob25.status != 0;
												if (flag406)
												{
													mob25.status = 0;
													ServerEffect.addServerEffect(60, mob25.x, mob25.y, 1);
													ItemMap itemMap8 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob25.x, mob25.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
													GameScr.vItemMap.addElement(itemMap8);
													bool flag407 = Res.abs(itemMap8.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap8.x - global::Char.myCharz().cx) < 24;
													if (flag407)
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
										Mob mob26 = null;
										try
										{
											mob26 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
										}
										catch (Exception ex53)
										{
											Cout.println("Loi tai NPC_MISS  " + ex53.ToString());
										}
										bool flag408 = mob26 != null;
										if (flag408)
										{
											mob26.hp = msg.reader().readInt();
											mob26.updateHp_bar();
											GameScr.startFlyText(mResources.miss, mob26.x, mob26.y - mob26.h, 0, -2, mFont.MISS);
										}
									}
								}
								else
								{
									GameCanvas.debug("SA91", 2);
									int num352 = msg.reader().readInt();
									string text20 = msg.reader().readUTF();
									Res.outz(string.Concat(new object[]
									{
										"user id= ",
										num352,
										" text= ",
										text20
									}));
									bool flag409 = global::Char.myCharz().charID == num352;
									if (flag409)
									{
										@char = global::Char.myCharz();
									}
									else
									{
										@char = GameScr.findCharInMap(num352);
									}
									bool flag410 = @char == null;
									if (flag410)
									{
										return;
									}
									@char.addInfo(text20);
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
							sbyte b138 = msg.reader().readByte();
							int num357;
							for (int num353 = 0; num353 < (int)b138; num353 = num357 + 1)
							{
								int charId = msg.reader().readInt();
								int cx = (int)msg.reader().readShort();
								int cy = (int)msg.reader().readShort();
								int cHPShow = msg.readInt3Byte();
								global::Char char15 = GameScr.findCharInMap(charId);
								bool flag411 = char15 != null;
								if (flag411)
								{
									char15.cx = cx;
									char15.cy = cy;
									char15.cHP = (char15.cHPShow = cHPShow);
									char15.lastUpdateTime = mSystem.currentTimeMillis();
								}
								num357 = num353;
							}
						}
					}
					else
					{
						sbyte b139 = msg.reader().readByte();
						int num357;
						for (int num354 = 0; num354 < GameScr.vNpc.size(); num354 = num357 + 1)
						{
							Npc npc7 = (Npc)GameScr.vNpc.elementAt(num354);
							bool flag412 = npc7.template.npcTemplateId == (int)b139;
							if (flag412)
							{
								sbyte b140 = msg.reader().readByte();
								bool flag413 = b140 == 0;
								if (flag413)
								{
									npc7.isHide = true;
								}
								else
								{
									npc7.isHide = false;
								}
								break;
							}
							num357 = num354;
						}
					}
				}
				else
				{
					Mob mob27 = null;
					try
					{
						mob27 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
					}
					catch (Exception ex54)
					{
					}
					bool flag414 = mob27 != null;
					if (flag414)
					{
						mob27.levelBoss = msg.reader().readByte();
						bool flag415 = mob27.levelBoss > 0;
						if (flag415)
						{
							mob27.typeSuperEff = Res.random(0, 3);
						}
					}
				}
				break;
			}
			}
			IL_D77E:
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception ex55)
		{
			Res.err(string.Concat(new object[]
			{
				"[Controller] [error] ",
				ex55.StackTrace,
				" msg: ",
				ex55.Message,
				" cause ",
				ex55.Data
			}));
		}
		finally
		{
			bool flag416 = msg != null;
			if (flag416)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0002B534 File Offset: 0x00029734
	private void readLogin(Message msg)
	{
		sbyte b = msg.reader().readByte();
		ChooseCharScr.playerData = new PlayerData[(int)b];
		Res.outz("[LEN] sl nguoi choi " + b.ToString());
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

	// Token: 0x06000192 RID: 402 RVA: 0x0002B600 File Offset: 0x00029800
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

	// Token: 0x06000193 RID: 403 RVA: 0x0002B70C File Offset: 0x0002990C
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
				bool flag = GameCanvas.w == 128 || GameCanvas.h <= 208;
				if (flag)
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

	// Token: 0x06000194 RID: 404 RVA: 0x0002BB60 File Offset: 0x00029D60
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
			b += 1;
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
			b2 += 1;
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0002BD8C File Offset: 0x00029F8C
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

	// Token: 0x06000196 RID: 406 RVA: 0x0002BE20 File Offset: 0x0002A020
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

	// Token: 0x06000197 RID: 407 RVA: 0x0002BE58 File Offset: 0x0002A058
	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = (int)b[i];
			bool flag = num < 0;
			if (flag)
			{
				num += 256;
			}
			array[i] = num;
		}
		return array;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
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
			bool flag2 = b == 0;
			if (flag2)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				bool flag3 = mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60;
				if (flag3)
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
			else
			{
				bool flag4 = b == 1;
				if (flag4)
				{
					clanMessage.recieve = (int)msg.reader().readByte();
					clanMessage.maxCap = (int)msg.reader().readByte();
					flag = (msg.reader().readByte() == 1);
					bool flag5 = flag;
					if (flag5)
					{
						GameScr.isNewClanMessage = true;
					}
					bool flag6 = clanMessage.playerId != global::Char.myCharz().charID;
					if (flag6)
					{
						bool flag7 = clanMessage.recieve < clanMessage.maxCap;
						if (flag7)
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
					bool flag8 = GameCanvas.panel.cp != null;
					if (flag8)
					{
						GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
					}
				}
				else
				{
					bool flag9 = b == 2 && global::Char.myCharz().role == 0;
					if (flag9)
					{
						GameScr.isNewClanMessage = true;
						clanMessage.option = new string[]
						{
							mResources.CANCEL,
							mResources.receive
						};
					}
				}
			}
			bool flag10 = GameCanvas.currentScreen != GameScr.instance;
			if (flag10)
			{
				GameScr.isNewClanMessage = false;
			}
			else
			{
				bool flag11 = GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3;
				if (flag11)
				{
					GameScr.isNewClanMessage = false;
				}
			}
			ClanMessage.addMessage(clanMessage, index, flag);
		}
		catch (Exception ex)
		{
			Cout.println("LOI TAI CMD -= " + msg.command.ToString());
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0002C164 File Offset: 0x0002A364
	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("[CONTROLER] start load map " + teleport3.ToString());
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		bool flag = !GameScr.isLoadAllData && !CreateCharScr.isCreateChar;
		if (flag)
		{
			GameScr.gI().initSelectChar();
		}
		GameScr.loadCamera(false, (teleport3 != 1) ? -1 : global::Char.myCharz().cx, (teleport3 != 0) ? 0 : -1);
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
		bool flag2 = global::Char.myCharz().cy >= TileMap.pxh - 100;
		if (flag2)
		{
			global::Char.myCharz().isFlyUp = true;
			global::Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		global::Char.isLockKey = false;
		Res.outz("cy= " + global::Char.myCharz().cy.ToString() + "---------------------------------------------");
		for (int i = 0; i < global::Char.myCharz().vEff.size(); i++)
		{
			EffectChar effectChar = (EffectChar)global::Char.myCharz().vEff.elementAt(i);
			bool flag3 = effectChar.template.type == 10;
			if (flag3)
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
		bool flag4 = global::Char.myCharz().cy <= 10 && teleport3 != 0 && teleport3 != 2;
		if (flag4)
		{
			Teleport p = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 1, true, (teleport3 != 1) ? ((int)teleport3) : global::Char.myCharz().cgender);
			Teleport.addTeleport(p);
			global::Char.myCharz().isTeleport = true;
		}
		bool flag5 = teleport3 == 2;
		if (flag5)
		{
			global::Char.myCharz().show();
		}
		bool isRongThanXuatHien = GameScr.gI().isRongThanXuatHien;
		if (isRongThanXuatHien)
		{
			bool flag6 = TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID;
			if (flag6)
			{
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			}
			bool flag7 = mGraphics.zoomLevel > 1;
			if (flag7)
			{
				GameScr.gI().doiMauTroi();
			}
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID.ToString(), 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
		Res.outz("[CONTROLLER] loadMap DONE!!!!!!!!!");
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0002C534 File Offset: 0x0002A734
	public void loadInfoMap(Message msg)
	{
		try
		{
			bool flag3 = mGraphics.zoomLevel == 1;
			if (flag3)
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
			bool flag4 = global::Char.myCharz().cx >= 0 && global::Char.myCharz().cx <= 100;
			if (flag4)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				bool flag5 = global::Char.myCharz().cx >= TileMap.tmw - 100 && global::Char.myCharz().cx <= TileMap.tmw;
				if (flag5)
				{
					global::Char.myCharz().cdir = -1;
				}
			}
			GameCanvas.debug("SA75x4", 2);
			int num = (int)msg.reader().readByte();
			Res.outz("vGo size= " + num.ToString());
			bool flag6 = !GameScr.info1.isDone;
			if (flag6)
			{
				GameScr.info1.cmx = global::Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = global::Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				bool flag7 = (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && waypoint.minX >= 0 && waypoint.minX <= 24;
				if (flag7)
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
				bool flag8 = Mob.arrMobTemplate[mob.templateId].type != 0;
				if (flag8)
				{
					bool flag9 = b % 3 == 0;
					if (flag9)
					{
						mob.dir = -1;
					}
					else
					{
						mob.dir = 1;
					}
					mob.x += (int)(10 - b % 20);
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				NewBoss newBoss = null;
				bool flag10 = mob.templateId == 70;
				if (flag10)
				{
					bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				}
				bool flag11 = mob.templateId == 71;
				if (flag11)
				{
					bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				}
				bool flag12 = mob.templateId == 72;
				if (flag12)
				{
					bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				}
				bool isBoss = mob.isBoss;
				if (isBoss)
				{
					newBoss = new NewBoss((int)b, (short)mob.x, (short)mob.y, mob.templateId, mob.hp, mob.maxHp, mob.sys);
				}
				bool flag13 = newBoss != null;
				if (flag13)
				{
					GameScr.vMob.addElement(newBoss);
				}
				else
				{
					bool flag14 = bigBoss != null;
					if (flag14)
					{
						GameScr.vMob.addElement(bigBoss);
					}
					else
					{
						bool flag15 = bachTuoc != null;
						if (flag15)
						{
							GameScr.vMob.addElement(bachTuoc);
						}
						else
						{
							bool flag16 = bigBoss2 != null;
							if (flag16)
							{
								GameScr.vMob.addElement(bigBoss2);
							}
							else
							{
								GameScr.vMob.addElement(mob);
							}
						}
					}
				}
				b += 1;
			}
			bool flag17 = global::Char.myCharz().mobMe != null && GameScr.findMobInMap(global::Char.myCharz().mobMe.mobId) == null;
			if (flag17)
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
			Res.outz("NPC size= " + num.ToString());
			for (int j = 0; j < num; j++)
			{
				sbyte b3 = msg.reader().readByte();
				short cx = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				bool flag18 = b4 != 6;
				if (flag18)
				{
					bool flag19 = (global::Char.myCharz().taskMaint.taskId >= 7 && (global::Char.myCharz().taskMaint.taskId != 7 || global::Char.myCharz().taskMaint.index > 1)) || (b4 != 7 && b4 != 8 && b4 != 9);
					if (flag19)
					{
						bool flag20 = global::Char.myCharz().taskMaint.taskId >= 6 || b4 != 16;
						if (flag20)
						{
							bool flag21 = b4 == 4;
							if (flag21)
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
			Res.outz("item size = " + num.ToString());
			text = text + "item: " + num.ToString();
			for (int k = 0; k < num; k++)
			{
				short itemMapID = msg.reader().readShort();
				short num4 = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num5 = msg.reader().readInt();
				short r = 0;
				bool flag22 = num5 == -2;
				if (flag22)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap = new ItemMap(num5, itemMapID, num4, x, y, r);
				bool flag = false;
				for (int l = 0; l < GameScr.vItemMap.size(); l++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
					bool flag23 = itemMap2.itemMapID == itemMap.itemMapID;
					if (flag23)
					{
						flag = true;
						break;
					}
				}
				bool flag24 = !flag;
				if (flag24)
				{
					GameScr.vItemMap.addElement(itemMap);
				}
				text = text + num4.ToString() + ",";
			}
			Res.err("sl item on map " + text + "\n");
			TileMap.vCurrItem.removeAllElements();
			bool flag25 = mGraphics.zoomLevel == 1;
			if (flag25)
			{
				BgItem.clearHashTable();
			}
			BgItem.vKeysNew.removeAllElements();
			bool flag26 = !GameCanvas.lowGraphic || (GameCanvas.lowGraphic && TileMap.isVoDaiMap()) || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 47 || TileMap.mapID == 48;
			if (flag26)
			{
				short num6 = msg.reader().readShort();
				text = "item high graphic: ";
				for (int m = 0; m < (int)num6; m++)
				{
					short num7 = msg.reader().readShort();
					short num8 = msg.reader().readShort();
					short num9 = msg.reader().readShort();
					bool flag27 = TileMap.getBIById((int)num7) != null;
					if (flag27)
					{
						BgItem bibyId = TileMap.getBIById((int)num7);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)num7;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)(num8 * (short)TileMap.size);
						bgItem.y = (int)(num9 * (short)TileMap.size);
						bgItem.layer = bibyId.layer;
						bool flag28 = TileMap.isExistMoreOne(bgItem.id);
						if (flag28)
						{
							bgItem.trans = ((m % 2 != 0) ? 2 : 0);
							bool flag29 = TileMap.mapID == 45;
							if (flag29)
							{
								bgItem.trans = 0;
							}
						}
						bool flag30 = !BgItem.imgNew.containsKey(bgItem.idImage.ToString() + string.Empty);
						if (flag30)
						{
							bool flag31 = mGraphics.zoomLevel == 1;
							if (flag31)
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
								bool flag32 = image == null;
								if (flag32)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
							}
							else
							{
								bool flag2 = false;
								sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel.ToString() + "bgItem" + bgItem.idImage.ToString());
								bool flag33 = array != null;
								if (flag33)
								{
									bool flag34 = BgItem.newSmallVersion != null;
									if (flag34)
									{
										Res.outz(string.Concat(new object[]
										{
											"Small  last= ",
											array.Length % 127,
											"new Version= ",
											BgItem.newSmallVersion[(int)bgItem.idImage]
										}));
										bool flag35 = array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage];
										if (flag35)
										{
											flag2 = true;
										}
									}
									bool flag36 = !flag2;
									if (flag36)
									{
										Image image2 = Image.createImage(array, 0, array.Length);
										bool flag37 = image2 != null;
										if (flag37)
										{
											BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image2);
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
								bool flag38 = flag2;
								if (flag38)
								{
									Image image3 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									bool flag39 = image3 == null;
									if (flag39)
									{
										image3 = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image3);
								}
							}
							BgItem.vKeysLast.addElement(bgItem.idImage.ToString() + string.Empty);
						}
						bool flag40 = !BgItem.isExistKeyNews(bgItem.idImage.ToString() + string.Empty);
						if (flag40)
						{
							BgItem.vKeysNew.addElement(bgItem.idImage.ToString() + string.Empty);
						}
						bgItem.changeColor();
						TileMap.vCurrItem.addElement(bgItem);
					}
					text = text + num7.ToString() + ",";
				}
				Res.err("item High Graphics: " + text);
				for (int n = 0; n < BgItem.vKeysLast.size(); n++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(n);
					bool flag41 = !BgItem.isExistKeyNews(text2);
					if (flag41)
					{
						BgItem.imgNew.remove(text2);
						bool flag42 = BgItem.imgNew.containsKey(text2 + "blend" + 1.ToString());
						if (flag42)
						{
							BgItem.imgNew.remove(text2 + "blend" + 1.ToString());
						}
						bool flag43 = BgItem.imgNew.containsKey(text2 + "blend" + 3.ToString());
						if (flag43)
						{
							BgItem.imgNew.remove(text2 + "blend" + 3.ToString());
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

	// Token: 0x0600019B RID: 411 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
	public void keyValueAction(string key, string value)
	{
		bool flag = key.Equals("eff");
		if (flag)
		{
			bool flag2 = Panel.graphics > 0;
			if (!flag2)
			{
				string[] array = Res.split(value, ".", 0);
				int id = int.Parse(array[0]);
				int layer = int.Parse(array[1]);
				int x = int.Parse(array[2]);
				int y = int.Parse(array[3]);
				bool flag3 = array.Length <= 4;
				int loop;
				int loopCount;
				if (flag3)
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
				bool flag4 = array.Length > 6;
				if (flag4)
				{
					effect.typeEff = int.Parse(array[6]);
					bool flag5 = array.Length > 7;
					if (flag5)
					{
						effect.indexFrom = int.Parse(array[7]);
						effect.indexTo = int.Parse(array[8]);
					}
				}
				EffecMn.addEff(effect);
			}
		}
		else
		{
			bool flag6 = key.Equals("beff");
			if (flag6)
			{
				bool flag7 = Panel.graphics > 1;
				if (!flag7)
				{
					BackgroudEffect.addEffect(int.Parse(value));
				}
			}
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0002D71C File Offset: 0x0002B91C
	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotMap : " + b.ToString());
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
				bool isLogin = GameCanvas.loginScr.isLogin2;
				if (isLogin)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
				{
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect.ToString(), string.Empty);
				}
				bool flag = GameScr.vsData != GameScr.vcData;
				if (flag)
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
				bool flag2 = GameScr.vsMap != GameScr.vcMap;
				if (flag2)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
				{
					try
					{
						bool flag3 = !GameScr.isLoadAllData;
						if (flag3)
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
				bool flag4 = GameScr.vsSkill != GameScr.vcSkill;
				if (flag4)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
				{
					try
					{
						bool flag5 = !GameScr.isLoadAllData;
						if (flag5)
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
				bool flag6 = GameScr.vsItem != GameScr.vcItem;
				if (flag6)
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
				bool flag7 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag7)
				{
					bool flag8 = !GameScr.isLoadAllData;
					if (flag8)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
					}
					Service.gI().clientOk();
				}
				sbyte b3 = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b3.ToString());
				GameScr.exps = new long[(int)b3];
				for (int i = 0; i < GameScr.exps.Length; i++)
				{
					GameScr.exps[i] = msg.reader().readLong();
				}
				goto IL_9B0;
			}
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available().ToString() + " bytes");
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
				bool flag9 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag9)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				goto IL_9B0;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available().ToString() + " bytes");
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
				bool flag10 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag10)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				goto IL_9B0;
			}
			case 8:
				Res.outz("GET UPDATE_ITEM " + msg.reader().available().ToString() + " bytes");
				this.createItemNew(msg.reader());
				goto IL_9B0;
			case 9:
				GameCanvas.debug("SA11", 2);
				goto IL_9B0;
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
					Res.err("   M apsize= " + (TileMap.tmw * TileMap.tmh).ToString());
					for (int j = 0; j < TileMap.maps.Length; j++)
					{
						int num = (int)msg.reader().readByte();
						bool flag11 = num < 0;
						if (flag11)
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
						TileMap.isMapDouble = (b4 != 0);
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
				goto IL_9B0;
			case 12:
				GameCanvas.debug("SA10", 2);
				goto IL_9B0;
			case 16:
				MoneyCharge.gI().switchToMe();
				goto IL_9B0;
			case 17:
				GameCanvas.debug("SYB123", 2);
				global::Char.myCharz().clearTask();
				goto IL_9B0;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num2 = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
				goto IL_9B0;
			}
			case 20:
				global::Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + global::Char.myCharz().cPk.ToString(), 0);
				goto IL_9B0;
			}
			sbyte b5 = b;
			sbyte b6 = b5;
			if (b6 != 35)
			{
				if (b6 == 36)
				{
					GameScr.typeActive = msg.reader().readByte();
					Res.outz("load Me Active: " + GameScr.typeActive.ToString());
				}
			}
			else
			{
				GameCanvas.endDlg();
				GameScr.gI().resetButton();
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
			}
			IL_9B0:;
		}
		catch (Exception ex7)
		{
			Cout.LogError("LOI TAI messageNotMap + " + msg.command.ToString());
		}
		finally
		{
			bool flag12 = msg != null;
			if (flag12)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0002E1DC File Offset: 0x0002C3DC
	public void messageNotLogin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotLogin : " + b.ToString());
			bool flag = b == 2;
			if (flag)
			{
				string linkDefault = msg.reader().readUTF();
				bool flag2 = Rms.loadRMSInt("AdminLink") != 1;
				if (flag2)
				{
					bool flag3 = mSystem.clientType == 1;
					if (flag3)
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
						Panel.CanNapTien = (b2 == 1);
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
			bool flag4 = msg != null;
			if (flag4)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			sbyte b = msg.reader().readByte();
			Res.outz("---messageSubCommand : " + b.ToString());
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
				for (sbyte b3 = 0; b3 < b2; b3 += 1)
				{
					Skill skill = Skills.get(msg.reader().readShort());
					this.useSkill(skill);
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
						bool flag = num != -1;
						if (flag)
						{
							ItemTemplate itemTemplate = ItemTemplates.get(num);
							int num2 = (int)itemTemplate.type;
							global::Char.myCharz().arrItemBody[i] = new Item();
							global::Char.myCharz().arrItemBody[i].template = itemTemplate;
							global::Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
							int num3 = (int)msg.reader().readUnsignedByte();
							bool flag2 = num3 != 0;
							if (flag2)
							{
								global::Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num3];
								for (int j = 0; j < global::Char.myCharz().arrItemBody[i].itemOption.Length; j++)
								{
									int num4 = (int)msg.reader().readUnsignedByte();
									int param = (int)msg.reader().readUnsignedShort();
									bool flag3 = num4 != -1;
									if (flag3)
									{
										global::Char.myCharz().arrItemBody[i].itemOption[j] = new ItemOption(num4, param);
									}
								}
							}
							bool flag4 = num2 == 0;
							if (flag4)
							{
								Res.outz("toi day =======================================" + global::Char.myCharz().body.ToString());
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[i].template.part;
							}
							else
							{
								bool flag5 = num2 == 1;
								if (flag5)
								{
									global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[i].template.part;
									Res.outz("toi day =======================================" + global::Char.myCharz().leg.ToString());
								}
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
					bool flag6 = num5 != -1;
					if (flag6)
					{
						global::Char.myCharz().arrItemBag[k] = new Item();
						global::Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num5);
						global::Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].indexUI = k;
						sbyte b4 = msg.reader().readByte();
						bool flag7 = b4 != 0;
						if (flag7)
						{
							global::Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b4];
							for (int l = 0; l < global::Char.myCharz().arrItemBag[k].itemOption.Length; l++)
							{
								int num6 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								bool flag8 = num6 != -1;
								if (flag8)
								{
									global::Char.myCharz().arrItemBag[k].itemOption[l] = new ItemOption(num6, param2);
									global::Char.myCharz().arrItemBag[k].getCompare();
								}
							}
						}
						bool flag9 = global::Char.myCharz().arrItemBag[k].template.type == 6;
						if (flag9)
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
					bool flag10 = num7 != -1;
					if (flag10)
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
							bool flag11 = num8 != -1;
							if (flag11)
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
				bool flag12 = num9 < 1;
				if (flag12)
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
				global::Char.myCharz().isNhapThe = (msg.reader().readByte() == 1);
				Res.outz("NHAP THE= " + global::Char.myCharz().isNhapThe.ToString());
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
				goto IL_1B76;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				global::Char.myCharz().myskill = null;
				goto IL_1B76;
			case 2:
			{
				GameCanvas.debug("SA14", 2);
				bool flag13 = global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5;
				if (flag13)
				{
					global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
					global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
					Cout.LogError2(" ME_LOAD_SKILL");
				}
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				sbyte b5 = msg.reader().readByte();
				for (sbyte b6 = 0; b6 < b5; b6 += 1)
				{
					short skillId = msg.reader().readShort();
					Skill skill2 = Skills.get(skillId);
					this.useSkill(skill2);
				}
				GameScr.gI().sortSkill();
				bool isPaintInfoMe = GameScr.isPaintInfoMe;
				if (isPaintInfoMe)
				{
					GameScr.indexRow = -1;
					GameScr.gI().left = (GameScr.gI().center = null);
				}
				goto IL_1B76;
			}
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
				goto IL_1B76;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				int cHP = global::Char.myCharz().cHP;
				global::Char.myCharz().cHP = msg.readInt3Byte();
				bool flag14 = global::Char.myCharz().cHP > cHP && global::Char.myCharz().cTypePk != 4;
				if (flag14)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"+",
						global::Char.myCharz().cHP - cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					bool flag15 = global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5003;
					if (flag15)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
					}
				}
				bool flag16 = global::Char.myCharz().cHP < cHP;
				if (flag16)
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
				bool flag17 = !GameScr.isPaintInfoMe;
				if (flag17)
				{
				}
				goto IL_1B76;
			}
			case 6:
			{
				GameCanvas.debug("SA25", 2);
				bool flag18 = global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5;
				if (flag18)
				{
					int cMP = global::Char.myCharz().cMP;
					global::Char.myCharz().cMP = msg.readInt3Byte();
					bool flag19 = global::Char.myCharz().cMP > cMP;
					if (flag19)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"+",
							global::Char.myCharz().cMP - cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
						SoundMn.gI().HP_MPup();
						bool flag20 = global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5001;
						if (flag20)
						{
							MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
						}
					}
					bool flag21 = global::Char.myCharz().cMP < cMP;
					if (flag21)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"-",
							cMP - global::Char.myCharz().cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
					}
					Res.outz("curr MP= " + global::Char.myCharz().cMP.ToString());
					GameScr.gI().dMP = global::Char.myCharz().cMP;
					bool flag22 = !GameScr.isPaintInfoMe;
					if (flag22)
					{
					}
				}
				goto IL_1B76;
			}
			case 7:
			{
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag23 = @char != null;
				if (flag23)
				{
					@char.clanID = msg.reader().readInt();
					bool flag24 = @char.clanID == -2;
					if (flag24)
					{
						@char.isCopy = true;
					}
					this.readCharInfo(@char, msg);
					try
					{
						@char.idAuraEff = msg.reader().readShort();
						@char.idEff_Set_Item = (short)msg.reader().readSByte();
						@char.idHat = msg.reader().readShort();
						bool flag25 = @char.bag >= 201;
						if (flag25)
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
				goto IL_1B76;
			}
			case 8:
			{
				GameCanvas.debug("SA26", 2);
				global::Char char2 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag26 = char2 != null;
				if (flag26)
				{
					char2.cspeed = (int)msg.reader().readByte();
				}
				goto IL_1B76;
			}
			case 9:
			{
				GameCanvas.debug("SA27", 2);
				global::Char char3 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag27 = char3 != null;
				if (flag27)
				{
					char3.cHP = msg.readInt3Byte();
					char3.cHPFull = msg.readInt3Byte();
				}
				goto IL_1B76;
			}
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				global::Char char4 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag28 = char4 != null;
				if (flag28)
				{
					char4.cHP = msg.readInt3Byte();
					char4.cHPFull = msg.readInt3Byte();
					char4.eff5BuffHp = (int)msg.reader().readShort();
					char4.eff5BuffMp = (int)msg.reader().readShort();
					char4.wp = (int)msg.reader().readShort();
					bool flag29 = char4.wp == -1;
					if (flag29)
					{
						char4.setDefaultWeapon();
					}
				}
				goto IL_1B76;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				global::Char char5 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag30 = char5 != null;
				if (flag30)
				{
					char5.cHP = msg.readInt3Byte();
					char5.cHPFull = msg.readInt3Byte();
					char5.eff5BuffHp = (int)msg.reader().readShort();
					char5.eff5BuffMp = (int)msg.reader().readShort();
					char5.body = (int)msg.reader().readShort();
					bool flag31 = char5.body == -1;
					if (flag31)
					{
						char5.setDefaultBody();
					}
				}
				goto IL_1B76;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				global::Char char6 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag32 = char6 != null;
				if (flag32)
				{
					char6.cHP = msg.readInt3Byte();
					char6.cHPFull = msg.readInt3Byte();
					char6.eff5BuffHp = (int)msg.reader().readShort();
					char6.eff5BuffMp = (int)msg.reader().readShort();
					char6.leg = (int)msg.reader().readShort();
					bool flag33 = char6.leg == -1;
					if (flag33)
					{
						char6.setDefaultLeg();
					}
				}
				goto IL_1B76;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				int num13 = msg.reader().readInt();
				bool flag34 = num13 == global::Char.myCharz().charID;
				global::Char char7;
				if (flag34)
				{
					char7 = global::Char.myCharz();
				}
				else
				{
					char7 = GameScr.findCharInMap(num13);
				}
				bool flag35 = char7 != null;
				if (flag35)
				{
					char7.cHP = msg.readInt3Byte();
					char7.cHPFull = msg.readInt3Byte();
					char7.eff5BuffHp = (int)msg.reader().readShort();
					char7.eff5BuffMp = (int)msg.reader().readShort();
				}
				goto IL_1B76;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				global::Char char8 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag36 = char8 != null;
				if (flag36)
				{
					char8.cHP = msg.readInt3Byte();
					sbyte b7 = msg.reader().readByte();
					Res.outz("player load hp type= " + b7.ToString());
					bool flag37 = b7 == 1;
					if (flag37)
					{
						ServerEffect.addServerEffect(11, char8, 5);
						ServerEffect.addServerEffect(104, char8, 4);
					}
					bool flag38 = b7 == 2;
					if (flag38)
					{
						char8.doInjure();
					}
					try
					{
						char8.cHPFull = msg.readInt3Byte();
					}
					catch (Exception ex4)
					{
					}
				}
				goto IL_1B76;
			}
			case 15:
			{
				GameCanvas.debug("SA33", 2);
				global::Char char9 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag39 = char9 != null;
				if (flag39)
				{
					char9.cHP = msg.readInt3Byte();
					char9.cHPFull = msg.readInt3Byte();
					char9.cx = (int)msg.reader().readShort();
					char9.cy = (int)msg.reader().readShort();
					char9.statusMe = 1;
					char9.cp3 = 3;
					ServerEffect.addServerEffect(109, char9, 2);
				}
				goto IL_1B76;
			}
			case 19:
				GameCanvas.debug("SA17", 2);
				global::Char.myCharz().boxSort();
				goto IL_1B76;
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num14 = msg.reader().readInt();
				global::Char.myCharz().xuInBox -= num14;
				global::Char.myCharz().xu += (long)num14;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				goto IL_1B76;
			}
			case 23:
			{
				short num15 = msg.reader().readShort();
				Skill skill3 = Skills.get(num15);
				this.useSkill(skill3);
				bool flag40 = num15 != 0 && num15 != 14 && num15 != 28;
				if (flag40)
				{
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill3.template.name, 0);
				}
				goto IL_1B76;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num16 = msg.reader().readInt();
				Res.outz("CID = " + num16.ToString());
				bool flag41 = TileMap.mapID == 130;
				if (flag41)
				{
					GameScr.gI().starVS();
				}
				bool flag42 = num16 == global::Char.myCharz().charID;
				if (flag42)
				{
					global::Char.myCharz().cTypePk = msg.reader().readByte();
					bool flag43 = GameScr.gI().isVS() && global::Char.myCharz().cTypePk != 0;
					if (flag43)
					{
						GameScr.gI().starVS();
					}
					Res.outz("type pk= " + global::Char.myCharz().cTypePk.ToString());
					global::Char.myCharz().npcFocus = null;
					bool flag44 = !GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus);
					if (flag44)
					{
						global::Char.myCharz().mobFocus = null;
					}
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					global::Char char10 = GameScr.findCharInMap(num16);
					bool flag45 = char10 != null;
					if (flag45)
					{
						Res.outz("type pk= " + char10.cTypePk.ToString());
						char10.cTypePk = msg.reader().readByte();
						bool flag46 = char10.isAttacPlayerStatus();
						if (flag46)
						{
							global::Char.myCharz().charFocus = char10;
						}
					}
				}
				for (int num17 = 0; num17 < GameScr.vCharInMap.size(); num17++)
				{
					global::Char char11 = GameScr.findCharInMap(num17);
					bool flag47 = char11 != null && char11.cTypePk != 0 && char11.cTypePk == global::Char.myCharz().cTypePk;
					if (flag47)
					{
						bool flag48 = !global::Char.myCharz().mobFocus.isMobMe;
						if (flag48)
						{
							global::Char.myCharz().mobFocus = null;
						}
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				goto IL_1B76;
			}
			}
			switch (b)
			{
			case 61:
			{
				string text = msg.reader().readUTF();
				sbyte[] array = new sbyte[msg.reader().readInt()];
				msg.reader().read(ref array);
				bool flag49 = array.Length == 0;
				if (flag49)
				{
					array = null;
				}
				bool flag50 = text.Equals("KSkill");
				if (flag50)
				{
					GameScr.gI().onKSkill(array);
				}
				else
				{
					bool flag51 = text.Equals("OSkill");
					if (flag51)
					{
						GameScr.gI().onOSkill(array);
					}
					else
					{
						bool flag52 = text.Equals("CSkill");
						if (flag52)
						{
							GameScr.gI().onCSkill(array);
						}
					}
				}
				break;
			}
			case 62:
				Res.outz("ME UPDATE SKILL");
				this.read_UpdateSkill(msg);
				break;
			case 63:
			{
				sbyte b8 = msg.reader().readByte();
				bool flag53 = b8 > 0;
				if (flag53)
				{
					GameCanvas.panel.vPlayerMenu_id.removeAllElements();
					InfoDlg.showWait();
					MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
					for (int num18 = 0; num18 < (int)b8; num18++)
					{
						string caption = msg.reader().readUTF();
						string caption2 = msg.reader().readUTF();
						short num19 = msg.reader().readShort();
						GameCanvas.panel.vPlayerMenu_id.addElement(num19.ToString() + string.Empty);
						global::Char.myCharz().charFocus.menuSelect = (int)num19;
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
			IL_1B76:;
		}
		catch (Exception ex5)
		{
			Cout.println("Loi tai Sub : " + ex5.ToString());
		}
		finally
		{
			bool flag54 = msg != null;
			if (flag54)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0002FF44 File Offset: 0x0002E144
	private void useSkill(Skill skill)
	{
		bool flag = global::Char.myCharz().myskill == null;
		if (flag)
		{
			global::Char.myCharz().myskill = skill;
		}
		else
		{
			bool flag2 = skill.template.Equals(global::Char.myCharz().myskill.template);
			if (flag2)
			{
				global::Char.myCharz().myskill = skill;
			}
		}
		global::Char.myCharz().vSkill.addElement(skill);
		bool flag3 = (skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0));
		if (flag3)
		{
			bool flag4 = (int)skill.template.id == global::Char.myCharz().skillTemplateId;
			if (flag4)
			{
				Service.gI().selectSkill(global::Char.myCharz().skillTemplateId);
			}
			global::Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0003005C File Offset: 0x0002E25C
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
			bool flag = c.cHP == 0;
			if (flag)
			{
				c.statusMe = 14;
			}
			c.cHPFull = msg.readInt3Byte();
			bool flag2 = c.cy >= TileMap.pxh - 100;
			if (flag2)
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
			bool flag3 = c.wp == -1;
			if (flag3)
			{
				c.setDefaultWeapon();
			}
			bool flag4 = c.body == -1;
			if (flag4)
			{
				c.setDefaultBody();
			}
			bool flag5 = c.leg == -1;
			if (flag5)
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
				bool flag6 = effectChar.template.type == 12 || effectChar.template.type == 11;
				if (flag6)
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

	// Token: 0x060001A1 RID: 417 RVA: 0x000303C4 File Offset: 0x0002E5C4
	private void readGetImgByName(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			sbyte nFrame = msg.reader().readByte();
			sbyte[] array = NinjaUtil.readByteArray(msg);
			Image img = this.createImage(array);
			ImgByName.SetImage(text, img, nFrame);
			bool flag = array != null;
			if (flag)
			{
				ImgByName.saveRMS(text, nFrame, array);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00030434 File Offset: 0x0002E634
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

	// Token: 0x060001A3 RID: 419 RVA: 0x00030468 File Offset: 0x0002E668
	private void loadItemNew(myReader d, sbyte type, bool isSave)
	{
		try
		{
			d.mark(100000);
			GameScr.vcItem = d.readByte();
			type = d.readByte();
			bool flag = type == 0;
			if (flag)
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
			else
			{
				bool flag2 = type == 1;
				if (flag2)
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
				else
				{
					bool flag3 = type == 2;
					if (flag3)
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
							bool flag4 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
							if (flag4)
							{
								GameScr.gI().readDart();
								GameScr.gI().readEfect();
								GameScr.gI().readArrow();
								GameScr.gI().readSkill();
								Service.gI().clientOk();
							}
						}
					}
					else
					{
						bool flag5 = type == 100;
						if (flag5)
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
				}
			}
		}
		catch (Exception ex)
		{
			ex.ToString();
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000307E0 File Offset: 0x0002E9E0
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
			Controller.frameHT_NEWBOSS.put(mobTemplateId.ToString() + string.Empty, array);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0003088C File Offset: 0x0002EA8C
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

	// Token: 0x060001A6 RID: 422 RVA: 0x00030934 File Offset: 0x0002EB34
	public void phuban_Info(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = b == 0;
			if (flag)
			{
				this.readPhuBan_CHIENTRUONGNAMEK(msg, (int)b);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0003097C File Offset: 0x0002EB7C
	private void readPhuBan_CHIENTRUONGNAMEK(Message msg, int type_PB)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = b == 0;
			if (flag)
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
			else
			{
				bool flag2 = b == 1;
				if (flag2)
				{
					int pointTeam = msg.reader().readInt();
					int pointTeam2 = msg.reader().readInt();
					bool flag3 = GameScr.phuban_Info != null;
					if (flag3)
					{
						GameScr.phuban_Info.updatePoint(type_PB, pointTeam, pointTeam2);
					}
				}
				else
				{
					bool flag4 = b == 2;
					if (flag4)
					{
						sbyte b2 = msg.reader().readByte();
						short type = 0;
						bool flag5 = b2 == 1;
						if (flag5)
						{
							type = 1;
						}
						else
						{
							bool flag6 = b2 == 2;
							if (flag6)
							{
								type = 2;
							}
						}
						short subtype = -1;
						GameScr.phuban_Info = null;
						GameScr.addEffectEnd((int)type, (int)subtype, 0, GameCanvas.hw, GameCanvas.hh, 0, 0, -1, null);
					}
					else
					{
						bool flag7 = b == 5;
						if (flag7)
						{
							short timeSecond2 = msg.reader().readShort();
							bool flag8 = GameScr.phuban_Info != null;
							if (flag8)
							{
								GameScr.phuban_Info.updateTime(type_PB, timeSecond2);
							}
						}
						else
						{
							bool flag9 = b == 4;
							if (flag9)
							{
								int lifeTeam = (int)msg.reader().readByte();
								int lifeTeam2 = (int)msg.reader().readByte();
								bool flag10 = GameScr.phuban_Info != null;
								if (flag10)
								{
									GameScr.phuban_Info.updateLife(type_PB, lifeTeam, lifeTeam2);
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

	// Token: 0x060001A8 RID: 424 RVA: 0x00030B74 File Offset: 0x0002ED74
	public void read_opt(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = b == 0;
			if (flag)
			{
				short idHat = msg.reader().readShort();
				global::Char.myCharz().idHat = idHat;
				SoundMn.gI().getStrOption();
			}
			else
			{
				bool flag2 = b == 2;
				if (flag2)
				{
					int num = msg.reader().readInt();
					sbyte b2 = msg.reader().readByte();
					short num2 = msg.reader().readShort();
					string v = num2.ToString() + "," + b2.ToString();
					MainImage imagePath = ImgByName.getImagePath("banner_" + num2.ToString(), ImgByName.hashImagePath);
					GameCanvas.danhHieu.put(num.ToString() + string.Empty, v);
				}
				else
				{
					bool flag3 = b == 3;
					if (flag3)
					{
						short num3 = msg.reader().readShort();
						SmallImage.createImage((int)num3);
						BackgroudEffect.id_water1 = num3;
					}
					else
					{
						bool flag4 = b == 4;
						if (flag4)
						{
							string o = msg.reader().readUTF();
							GameCanvas.messageServer.addElement(o);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00030CC0 File Offset: 0x0002EEC0
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
			bool flag = b == 0;
			if (flag)
			{
				short curExp = msg.reader().readShort();
				for (int i = 0; i < global::Char.myCharz().vSkill.size(); i++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(i);
					bool flag2 = skill.skillId == num;
					if (flag2)
					{
						skill.curExp = curExp;
						break;
					}
				}
			}
			else
			{
				bool flag3 = b == 1;
				if (flag3)
				{
					sbyte b2 = msg.reader().readByte();
					for (int j = 0; j < global::Char.myCharz().vSkill.size(); j++)
					{
						Skill skill2 = (Skill)global::Char.myCharz().vSkill.elementAt(j);
						bool flag4 = skill2.skillId == num;
						if (flag4)
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
				else
				{
					bool flag5 = b == -1;
					if (flag5)
					{
						Skill skill3 = Skills.get(num);
						for (int l = 0; l < global::Char.myCharz().vSkill.size(); l++)
						{
							Skill skill4 = (Skill)global::Char.myCharz().vSkill.elementAt(l);
							bool flag6 = skill4.template.id == skill3.template.id;
							if (flag6)
							{
								global::Char.myCharz().vSkill.setElementAt(skill3, l);
								break;
							}
						}
						for (int m = 0; m < global::Char.myCharz().vSkillFight.size(); m++)
						{
							Skill skill5 = (Skill)global::Char.myCharz().vSkillFight.elementAt(m);
							bool flag7 = skill5.template.id == skill3.template.id;
							if (flag7)
							{
								global::Char.myCharz().vSkillFight.setElementAt(skill3, m);
								break;
							}
						}
						for (int n = 0; n < GameScr.onScreenSkill.Length; n++)
						{
							bool flag8 = GameScr.onScreenSkill[n] != null && GameScr.onScreenSkill[n].template.id == skill3.template.id;
							if (flag8)
							{
								GameScr.onScreenSkill[n] = skill3;
								break;
							}
						}
						for (int num2 = 0; num2 < GameScr.keySkill.Length; num2++)
						{
							bool flag9 = GameScr.keySkill[num2] != null && GameScr.keySkill[num2].template.id == skill3.template.id;
							if (flag9)
							{
								GameScr.keySkill[num2] = skill3;
								break;
							}
						}
						bool flag10 = global::Char.myCharz().myskill.template.id == skill3.template.id;
						if (flag10)
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
			}
		}
		catch (Exception ex2)
		{
		}
	}

	// Token: 0x0400044C RID: 1100
	protected static Controller me;

	// Token: 0x0400044D RID: 1101
	protected static Controller me2;

	// Token: 0x0400044E RID: 1102
	public Message messWait;

	// Token: 0x0400044F RID: 1103
	public static bool isLoadingData = false;

	// Token: 0x04000450 RID: 1104
	public static bool isConnectOK;

	// Token: 0x04000451 RID: 1105
	public static bool isConnectionFail;

	// Token: 0x04000452 RID: 1106
	public static bool isDisconnected;

	// Token: 0x04000453 RID: 1107
	public static bool isMain;

	// Token: 0x04000454 RID: 1108
	private float demCount;

	// Token: 0x04000455 RID: 1109
	private int move;

	// Token: 0x04000456 RID: 1110
	private int total;

	// Token: 0x04000457 RID: 1111
	public static bool isStopReadMessage;

	// Token: 0x04000458 RID: 1112
	public static MyHashTable frameHT_NEWBOSS = new MyHashTable();

	// Token: 0x04000459 RID: 1113
	public const sbyte PHUBAN_TYPE_CHIENTRUONGNAMEK = 0;

	// Token: 0x0400045A RID: 1114
	public const sbyte PHUBAN_START = 0;

	// Token: 0x0400045B RID: 1115
	public const sbyte PHUBAN_UPDATE_POINT = 1;

	// Token: 0x0400045C RID: 1116
	public const sbyte PHUBAN_END = 2;

	// Token: 0x0400045D RID: 1117
	public const sbyte PHUBAN_LIFE = 4;

	// Token: 0x0400045E RID: 1118
	public const sbyte PHUBAN_INFO = 5;
}
