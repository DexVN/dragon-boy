using System;
using Assets.src.g;

// Token: 0x0200009A RID: 154
public class Service
{
	// Token: 0x0600085B RID: 2139 RVA: 0x00094548 File Offset: 0x00092748
	public static Service gI()
	{
		bool flag = Service.instance == null;
		if (flag)
		{
			Service.instance = new Service();
		}
		return Service.instance;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00094578 File Offset: 0x00092778
	public void gotoPlayer(int id)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000945E8 File Offset: 0x000927E8
	public void androidPack()
	{
		bool flag = mSystem.android_pack == null;
		if (!flag)
		{
			Message message = null;
			try
			{
				message = new Message(126);
				message.writer().writeUTF(mSystem.android_pack);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0009466C File Offset: 0x0009286C
	public void charInfo(string day, string month, string year, string address, string cmnd, string dayCmnd, string noiCapCmnd, string sdt, string name)
	{
		Message message = null;
		try
		{
			message = new Message(42);
			message.writer().writeUTF(day);
			message.writer().writeUTF(month);
			message.writer().writeUTF(year);
			message.writer().writeUTF(address);
			message.writer().writeUTF(cmnd);
			message.writer().writeUTF(dayCmnd);
			message.writer().writeUTF(noiCapCmnd);
			message.writer().writeUTF(sdt);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0009474C File Offset: 0x0009294C
	public void androidPack2()
	{
		bool flag = mSystem.android_pack == null;
		if (!flag)
		{
			Message message = null;
			try
			{
				message = new Message(126);
				message.writer().writeUTF(mSystem.android_pack);
				bool flag2 = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
				if (flag2)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00094814 File Offset: 0x00092A14
	public void checkAd(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-44);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00094884 File Offset: 0x00092A84
	public void combine(sbyte action, MyVector id)
	{
		Res.outz("combine");
		Message message = null;
		try
		{
			message = new Message(-81);
			message.writer().writeByte(action);
			bool flag = action == 1;
			if (flag)
			{
				message.writer().writeByte(id.size());
				for (int i = 0; i < id.size(); i++)
				{
					message.writer().writeByte(((Item)id.elementAt(i)).indexUI);
					Res.outz("gui id " + ((Item)id.elementAt(i)).indexUI.ToString());
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0009496C File Offset: 0x00092B6C
	public void giaodich(sbyte action, int playerID, sbyte index, int num)
	{
		Res.outz2("giao dich action = " + action.ToString());
		Message message = null;
		try
		{
			message = new Message(-86);
			message.writer().writeByte(action);
			bool flag = action == 0 || action == 1;
			if (flag)
			{
				Res.outz2(">>>> len playerID =" + playerID.ToString());
				message.writer().writeInt(playerID);
			}
			bool flag2 = action == 2;
			if (flag2)
			{
				Res.outz2(string.Concat(new object[]
				{
					"gui len index =",
					index,
					" num= ",
					num
				}));
				message.writer().writeByte(index);
				message.writer().writeInt(num);
			}
			bool flag3 = action == 4;
			if (flag3)
			{
				Res.outz2(">>>> len index =" + index.ToString());
				message.writer().writeByte(index);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00094AA4 File Offset: 0x00092CA4
	public void sendClientInput(TField[] t)
	{
		Message message = null;
		try
		{
			Res.outz(" gui input ");
			message = new Message(-125);
			Res.outz("byte lent = " + t.Length.ToString());
			message.writer().writeByte(t.Length);
			for (int i = 0; i < t.Length; i++)
			{
				message.writer().writeUTF(t[i].getText());
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00094B58 File Offset: 0x00092D58
	public void speacialSkill(sbyte index)
	{
		Message message = null;
		try
		{
			message = new Message(112);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00094BC8 File Offset: 0x00092DC8
	public void test(short x, short y)
	{
		Res.outz(string.Concat(new object[]
		{
			"gui x= ",
			x,
			" y= ",
			y
		}));
		Message message = null;
		try
		{
			message = new Message(0);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00094C78 File Offset: 0x00092E78
	public void test2()
	{
		Res.outz("gui test1");
		Message message = null;
		try
		{
			message = new Message(1);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00003136 File Offset: 0x00001336
	public void testJoint()
	{
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00094CE4 File Offset: 0x00092EE4
	public void mobCapcha(char ch)
	{
		Res.outz("cap char c= " + ch.ToString());
		Message message = null;
		try
		{
			message = new Message(-85);
			message.writer().writeChar(ch);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00094D60 File Offset: 0x00092F60
	public void friend(sbyte action, int playerId)
	{
		Res.outz("add friend");
		Message message = null;
		try
		{
			message = new Message(-80);
			message.writer().writeByte(action);
			bool flag = playerId != -1;
			if (flag)
			{
				message.writer().writeInt(playerId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00094E00 File Offset: 0x00093000
	public void getArchivemnt(int index)
	{
		Res.outz("get ngoc");
		Message message = null;
		try
		{
			message = new Message(-76);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00094E88 File Offset: 0x00093088
	public void getPlayerMenu(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-79);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00094EEC File Offset: 0x000930EC
	public void clanImage(sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-62);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00094F68 File Offset: 0x00093168
	public void skill_not_focus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00094FE4 File Offset: 0x000931E4
	public void clanDonate(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-54);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00095060 File Offset: 0x00093260
	public void clanMessage(int type, string text, int clanID)
	{
		Message message = null;
		try
		{
			message = new Message(-51);
			message.writer().writeByte(type);
			bool flag = type == 0;
			if (flag)
			{
				message.writer().writeUTF(text);
			}
			bool flag2 = type == 2;
			if (flag2)
			{
				message.writer().writeInt(clanID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00095108 File Offset: 0x00093308
	public void useItem(sbyte type, sbyte where, sbyte index, short template)
	{
		Cout.println("USE ITEM! " + type.ToString());
		bool flag = global::Char.myCharz().statusMe == 14;
		if (!flag)
		{
			Message message = null;
			try
			{
				message = new Message(-43);
				message.writer().writeByte(type);
				message.writer().writeByte(where);
				message.writer().writeByte(index);
				bool flag2 = index == -1;
				if (flag2)
				{
					message.writer().writeShort(template);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x000951CC File Offset: 0x000933CC
	public void joinClan(int id, sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-49);
			message.writer().writeInt(id);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00095254 File Offset: 0x00093454
	public void clanMember(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-50);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x000952D0 File Offset: 0x000934D0
	public void searchClan(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-47);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0009534C File Offset: 0x0009354C
	public void requestClan(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-53);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000953C8 File Offset: 0x000935C8
	public void clanRemote(int id, sbyte role)
	{
		Message message = null;
		try
		{
			message = new Message(-56);
			message.writer().writeInt(id);
			message.writer().writeByte(role);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00095450 File Offset: 0x00093650
	public void leaveClan()
	{
		Message message = null;
		try
		{
			message = new Message(-55);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x000954C0 File Offset: 0x000936C0
	public void clanInvite(sbyte action, int playerID, int clanID, int code)
	{
		Message message = null;
		try
		{
			message = new Message(-57);
			message.writer().writeByte(action);
			bool flag = action == 0;
			if (flag)
			{
				message.writer().writeInt(playerID);
			}
			bool flag2 = action == 1 || action == 2;
			if (flag2)
			{
				message.writer().writeInt(clanID);
				message.writer().writeInt(code);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00095580 File Offset: 0x00093780
	public void getClan(sbyte action, sbyte id, string text)
	{
		Message message = null;
		try
		{
			message = new Message(-46);
			message.writer().writeByte(action);
			bool flag = action == 2 || action == 4;
			if (flag)
			{
				message.writer().writeByte(id);
				message.writer().writeUTF(text);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00095628 File Offset: 0x00093828
	public void updateCaption(sbyte gender)
	{
		Message message = null;
		try
		{
			message = new Message(-41);
			message.writer().writeByte(gender);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000956A4 File Offset: 0x000938A4
	public void getItem(sbyte type, sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-40);
			message.writer().writeByte(type);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0009572C File Offset: 0x0009392C
	public void getTask(int npcTemplateId, int menuId, int optionId)
	{
		Message message = null;
		try
		{
			message = new Message(40);
			message.writer().writeByte(npcTemplateId);
			message.writer().writeByte(menuId);
			bool flag = optionId >= 0;
			if (flag)
			{
				message.writer().writeByte(optionId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x000957D0 File Offset: 0x000939D0
	public Message messageNotLogin(sbyte command)
	{
		Message message = new Message(-29);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x000957F8 File Offset: 0x000939F8
	public Message messageNotMap(sbyte command)
	{
		Message message = new Message(-28);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00095820 File Offset: 0x00093A20
	public static Message messageSubCommand(sbyte command)
	{
		Message message = new Message(-30);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00095848 File Offset: 0x00093A48
	public void setClientType()
	{
		bool flag = Rms.loadRMSInt("clienttype") != -1;
		if (flag)
		{
			Main.typeClient = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(Main.typeClient);
			message.writer().writeByte(mGraphics.zoomLevel);
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			DataInputStream dataInputStream = MyStream.readFile("/info");
			bool flag2 = dataInputStream != null;
			if (flag2)
			{
				sbyte[] array = new sbyte[dataInputStream.r.buffer.Length];
				dataInputStream.read(ref array);
				bool flag3 = array != null;
				if (flag3)
				{
					message.writer().writeShort(array.Length);
					message.writer().write(array);
					Res.err(string.Concat(new object[]
					{
						"write ",
						array.Length,
						"|",
						GameMidlet.VERSION
					}));
				}
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x000959FC File Offset: 0x00093BFC
	public void setClientType2()
	{
		Res.outz("SET CLIENT TYPE");
		bool flag = Rms.loadRMSInt("clienttype") != -1;
		if (flag)
		{
			mSystem.clientType = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Res.outz("setType");
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(mSystem.clientType);
			message.writer().writeByte(mGraphics.zoomLevel);
			Res.outz("gui zoomlevel = " + mGraphics.zoomLevel.ToString());
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			DataInputStream dataInputStream = MyStream.readFile("/info");
			bool flag2 = dataInputStream != null;
			if (flag2)
			{
				sbyte[] array = new sbyte[dataInputStream.r.buffer.Length];
				dataInputStream.read(ref array);
				bool flag3 = array != null;
				if (flag3)
				{
					message.writer().writeShort(array.Length);
					message.writer().write(array);
					Res.err(string.Concat(new object[]
					{
						"write ",
						array.Length,
						"|",
						GameMidlet.VERSION
					}));
				}
			}
			this.session = Session_ME2.gI();
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
			message.cleanup();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00095BEC File Offset: 0x00093DEC
	public void sendCheckController()
	{
		Message message = null;
		try
		{
			message = new Message(-120);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			Service.curCheckController = mSystem.currentTimeMillis();
			message.cleanup();
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00095C4C File Offset: 0x00093E4C
	public void sendCheckMap()
	{
		Message message = null;
		try
		{
			message = new Message(-121);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			Service.curCheckMap = mSystem.currentTimeMillis();
			message.cleanup();
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00095CAC File Offset: 0x00093EAC
	public void login(string username, string pass, string version, sbyte type)
	{
		try
		{
			Message message = this.messageNotLogin(0);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(version);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00095D3C File Offset: 0x00093F3C
	public void requestRegister(string username, string pass, string usernameAo, string passAo, string version)
	{
		try
		{
			Message message = this.messageNotLogin(1);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			bool flag = usernameAo != null && !usernameAo.Equals(string.Empty);
			if (flag)
			{
				message.writer().writeUTF(usernameAo);
				message.writer().writeUTF("a");
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00095DE8 File Offset: 0x00093FE8
	public void requestChangeMap()
	{
		Message message = new Message(-23);
		this.session.sendMessage(message);
		message.cleanup();
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00095E14 File Offset: 0x00094014
	public void magicTree(sbyte type)
	{
		Message message = new Message(-34);
		try
		{
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00095E64 File Offset: 0x00094064
	public void requestChangeZone(int zoneId, int indexUI)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(zoneId);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00095EB4 File Offset: 0x000940B4
	public void checkMMove(int second)
	{
		Message message = new Message(-78);
		try
		{
			message.writer().writeInt(second);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00095F04 File Offset: 0x00094104
	public void charMove()
	{
		int num = global::Char.myCharz().cx - global::Char.myCharz().cxSend;
		int num2 = global::Char.myCharz().cy - global::Char.myCharz().cySend;
		bool flag = global::Char.ischangingMap || (num == 0 && num2 == 0) || Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().cy <= 0 || global::Char.myCharz().telePortSkill;
		if (!flag)
		{
			try
			{
				Message message = new Message(-7);
				global::Char.myCharz().cxSend = global::Char.myCharz().cx;
				global::Char.myCharz().cySend = global::Char.myCharz().cy;
				global::Char.myCharz().cdirSend = global::Char.myCharz().cdir;
				global::Char.myCharz().cactFirst = global::Char.myCharz().statusMe;
				bool flag2 = TileMap.tileTypeAt(global::Char.myCharz().cx / (int)TileMap.size, global::Char.myCharz().cy / (int)TileMap.size) == 0;
				if (flag2)
				{
					message.writer().writeByte(1);
					bool canFly = global::Char.myCharz().canFly;
					if (canFly)
					{
						bool flag3 = !global::Char.myCharz().isHaveMount;
						if (flag3)
						{
							global::Char.myCharz().cMP -= global::Char.myCharz().cMPGoc / 100 * ((global::Char.myCharz().isMonkey != 1) ? 1 : 2);
						}
						bool flag4 = global::Char.myCharz().cMP < 0;
						if (flag4)
						{
							global::Char.myCharz().cMP = 0;
						}
						GameScr.gI().isInjureMp = true;
						GameScr.gI().twMp = 0;
					}
				}
				else
				{
					message.writer().writeByte(0);
				}
				message.writer().writeShort(global::Char.myCharz().cx);
				bool flag5 = num2 != 0;
				if (flag5)
				{
					message.writer().writeShort(global::Char.myCharz().cy);
				}
				this.session.sendMessage(message);
				GameScr.tickMove++;
				message.cleanup();
			}
			catch (Exception ex)
			{
				Cout.LogError("LOI CHAR MOVE " + ex.ToString());
			}
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00096150 File Offset: 0x00094350
	public void selectCharToPlay(string charname)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(1);
			message.writer().writeUTF(charname);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00003136 File Offset: 0x00001336
	public void selectZone(sbyte sub, int value)
	{
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x000961C0 File Offset: 0x000943C0
	public void createChar(string name, int gender, int hair)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(2);
			message.writer().writeUTF(name);
			message.writer().writeByte(gender);
			message.writer().writeByte(hair);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00096248 File Offset: 0x00094448
	public void requestModTemplate(int modTemplateId)
	{
		Message message = null;
		try
		{
			message = new Message(11);
			message.writer().writeByte(modTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x000962C4 File Offset: 0x000944C4
	public void requestNpcTemplate(int npcTemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(12);
			message.writer().writeByte(npcTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00096340 File Offset: 0x00094540
	public void requestSkill(int skillId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(9);
			message.writer().writeShort(skillId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x000963BC File Offset: 0x000945BC
	public void requestItemInfo(int typeUI, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(35);
			message.writer().writeByte(typeUI);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00096444 File Offset: 0x00094644
	public void requestItemPlayer(int charId, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(90);
			message.writer().writeInt(charId);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x000964CC File Offset: 0x000946CC
	public void upSkill(int skillTemplateId, int point)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(17);
			message.writer().writeShort(skillTemplateId);
			message.writer().writeByte(point);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00096554 File Offset: 0x00094754
	public void saleItem(sbyte action, sbyte type, short id)
	{
		Message message = null;
		try
		{
			message = new Message(7);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x000965E8 File Offset: 0x000947E8
	public void buyItem(sbyte type, int id, int quantity)
	{
		Message message = null;
		try
		{
			message = new Message(6);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			bool flag = quantity > 1;
			if (flag)
			{
				message.writer().writeShort(quantity);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00096688 File Offset: 0x00094888
	public void selectSkill(int skillTemplateId)
	{
		Cout.println(global::Char.myCharz().cName + " SELECT SKILL " + skillTemplateId.ToString());
		Message message = null;
		try
		{
			message = new Message(34);
			message.writer().writeShort(skillTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00096724 File Offset: 0x00094924
	public void getEffData(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-66);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x000967A0 File Offset: 0x000949A0
	public void openUIZone()
	{
		Message message = null;
		try
		{
			message = new Message(29);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00096810 File Offset: 0x00094A10
	public void confirmMenu(short npcID, sbyte select)
	{
		Res.outz("confirme menu" + select.ToString());
		Message message = null;
		try
		{
			message = new Message(32);
			message.writer().writeShort(npcID);
			message.writer().writeByte(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x000968B0 File Offset: 0x00094AB0
	public void openMenu(int npcId)
	{
		Message message = null;
		try
		{
			message = new Message(33);
			message.writer().writeShort(npcId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0009692C File Offset: 0x00094B2C
	public void menu(int npcId, int menuId, int optionId)
	{
		Cout.println("menuid: " + menuId.ToString());
		Message message = null;
		try
		{
			message = new Message(22);
			message.writer().writeByte(npcId);
			message.writer().writeByte(menuId);
			message.writer().writeByte(optionId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x000969D8 File Offset: 0x00094BD8
	public void menuId(short menuId)
	{
		Message message = null;
		try
		{
			message = new Message(27);
			message.writer().writeShort(menuId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00096A54 File Offset: 0x00094C54
	public void textBoxId(short menuId, string str)
	{
		Message message = null;
		try
		{
			message = new Message(88);
			message.writer().writeShort(menuId);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00096ADC File Offset: 0x00094CDC
	public void requestItem(int typeUI)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(22);
			message.writer().writeByte(typeUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00096B58 File Offset: 0x00094D58
	public void boxSort()
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(19);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00096BC8 File Offset: 0x00094DC8
	public void boxCoinOut(int coinOut)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(21);
			message.writer().writeInt(coinOut);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00096C44 File Offset: 0x00094E44
	public void upgradeItem(Item item, Item[] items, bool isGold)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(14);
			message.writer().writeBoolean(isGold);
			message.writer().writeByte(item.indexUI);
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i] != null;
				if (flag)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00096D14 File Offset: 0x00094F14
	public void crystalCollectLock(Item[] items)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(13);
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i] != null;
				if (flag)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00096DC4 File Offset: 0x00094FC4
	public void acceptInviteTrade(int playerMapId)
	{
		Message message = null;
		try
		{
			message = new Message(37);
			message.writer().writeInt(playerMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00096E40 File Offset: 0x00095040
	public void cancelInviteTrade()
	{
		Message message = null;
		try
		{
			message = new Message(50);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00096EB0 File Offset: 0x000950B0
	public void tradeAccept()
	{
		Message message = null;
		try
		{
			message = new Message(39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00096F20 File Offset: 0x00095120
	public void tradeItemLock(int coin, Item[] items)
	{
		Message message = null;
		try
		{
			message = new Message(38);
			message.writer().writeInt(coin);
			int num = 0;
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i] != null;
				if (flag)
				{
					num++;
				}
			}
			message.writer().writeByte(num);
			for (int j = 0; j < items.Length; j++)
			{
				bool flag2 = items[j] != null;
				if (flag2)
				{
					message.writer().writeByte(items[j].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00097010 File Offset: 0x00095210
	public void sendPlayerAttack(MyVector vMob, MyVector vChar, int type)
	{
		try
		{
			Res.outz(string.Concat(new object[]
			{
				">>SEND ATTACT  vMob=",
				vMob.size(),
				"  vChar=",
				vChar.size()
			}));
			Message message = null;
			bool flag = type != 0;
			if (flag)
			{
				bool flag2 = vMob.size() > 0 && vChar.size() > 0;
				if (flag2)
				{
					bool flag3 = type == 1;
					if (flag3)
					{
						message = new Message(-4);
					}
					else
					{
						bool flag4 = type == 2;
						if (flag4)
						{
							message = new Message(67);
						}
					}
					message.writer().writeByte(vMob.size());
					for (int i = 0; i < vMob.size(); i++)
					{
						Mob mob = (Mob)vMob.elementAt(i);
						message.writer().writeByte(mob.mobId);
					}
					for (int j = 0; j < vChar.size(); j++)
					{
						global::Char @char = (global::Char)vChar.elementAt(j);
						bool flag5 = @char != null;
						if (flag5)
						{
							message.writer().writeInt(@char.charID);
						}
						else
						{
							message.writer().writeInt(-1);
						}
					}
				}
				else
				{
					bool flag6 = vMob.size() > 0;
					if (flag6)
					{
						message = new Message(54);
						for (int k = 0; k < vMob.size(); k++)
						{
							Mob mob2 = (Mob)vMob.elementAt(k);
							bool flag7 = !mob2.isMobMe;
							if (flag7)
							{
								message.writer().writeByte(mob2.mobId);
							}
							else
							{
								message.writer().writeByte(-1);
								message.writer().writeInt(mob2.mobId);
							}
						}
					}
					else
					{
						bool flag8 = vChar.size() > 0;
						if (flag8)
						{
							message = new Message(-60);
							for (int l = 0; l < vChar.size(); l++)
							{
								global::Char char2 = (global::Char)vChar.elementAt(l);
								message.writer().writeInt(char2.charID);
							}
						}
					}
				}
				message.writer().writeSByte((sbyte)global::Char.myCharz().cdir);
				bool flag9 = message != null;
				if (flag9)
				{
					this.session.sendMessage(message);
				}
			}
		}
		catch (Exception ex)
		{
			Res.err(string.Concat(new object[]
			{
				">>err ATTACT  vMob=",
				vMob.size(),
				"  vChar=",
				vChar.size()
			}));
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x000972E4 File Offset: 0x000954E4
	public void pickItem(int itemMapId)
	{
		Message message = null;
		try
		{
			message = new Message(-20);
			message.writer().writeShort(itemMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00097360 File Offset: 0x00095560
	public void throwItem(int index)
	{
		Message message = null;
		try
		{
			message = new Message(-18);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x000973DC File Offset: 0x000955DC
	public void returnTownFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-15);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0009744C File Offset: 0x0009564C
	public void wakeUpFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-16);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x000974BC File Offset: 0x000956BC
	public void chat(string text)
	{
		Message message = null;
		try
		{
			message = new Message(44);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00097538 File Offset: 0x00095738
	public void updateData()
	{
		Message message = null;
		try
		{
			message = new Message(-87);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x000975E8 File Offset: 0x000957E8
	public void updateMap()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(6);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00097698 File Offset: 0x00095898
	public void updateSkill()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(7);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0009773C File Offset: 0x0009593C
	public void updateItem()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(8);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x000977E0 File Offset: 0x000959E0
	public void clientOk()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(13);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00097850 File Offset: 0x00095A50
	public void tradeInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(36);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x000978CC File Offset: 0x00095ACC
	public void addFriend(string name)
	{
		Message message = null;
		try
		{
			message = new Message(53);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x00097948 File Offset: 0x00095B48
	public void addPartyAccept(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(76);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x000979C4 File Offset: 0x00095BC4
	public void addPartyCancel(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(77);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00097A40 File Offset: 0x00095C40
	public void testInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(59);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00097ABC File Offset: 0x00095CBC
	public void addCuuSat(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(62);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x00097B38 File Offset: 0x00095D38
	public void addParty(string name)
	{
		Message message = null;
		try
		{
			message = new Message(75);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00097BB4 File Offset: 0x00095DB4
	public void player_vs_player(sbyte action, sbyte type, int playerId)
	{
		Message message = null;
		try
		{
			message = new Message(-59);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeInt(playerId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00097C48 File Offset: 0x00095E48
	public void requestMaptemplate(int maptemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(10);
			message.writer().writeByte(maptemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00097CC4 File Offset: 0x00095EC4
	public void outParty()
	{
		Message message = null;
		try
		{
			message = new Message(79);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00097D34 File Offset: 0x00095F34
	public void requestPlayerInfo(MyVector chars)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeByte(chars.size());
			for (int i = 0; i < chars.size(); i++)
			{
				global::Char @char = (global::Char)chars.elementAt(i);
				message.writer().writeInt(@char.charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00097DEC File Offset: 0x00095FEC
	public void pleaseInputParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(16);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00097E68 File Offset: 0x00096068
	public void acceptPleaseParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(17);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00097EE4 File Offset: 0x000960E4
	public void chatPlayer(string text, int id)
	{
		Res.outz("chat player text = " + text);
		Message message = null;
		try
		{
			message = new Message(-72);
			message.writer().writeInt(id);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00097F7C File Offset: 0x0009617C
	public void chatGlobal(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-71);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00097FF8 File Offset: 0x000961F8
	public void chatPrivate(string to, string text)
	{
		Message message = null;
		try
		{
			message = new Message(91);
			message.writer().writeUTF(to);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00098080 File Offset: 0x00096280
	public void sendCardInfo(string NAP, string PIN)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(16);
			message.writer().writeUTF(NAP);
			message.writer().writeUTF(PIN);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00098108 File Offset: 0x00096308
	public void saveRms(string key, sbyte[] data)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(60);
			message.writer().writeUTF(key);
			message.writer().writeInt(data.Length);
			message.writer().write(data);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x000981A0 File Offset: 0x000963A0
	public void loadRMS(string key)
	{
		Cout.println("REQUEST RMS");
		Message message = null;
		try
		{
			message = Service.messageSubCommand(61);
			message.writer().writeUTF(key);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00098228 File Offset: 0x00096428
	public void clearTask()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(17);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00098298 File Offset: 0x00096498
	public void changeName(string name, int id)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(18);
			message.writer().writeInt(id);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00098320 File Offset: 0x00096520
	public void requestIcon(int id)
	{
		GameCanvas.connect();
		Message message = null;
		try
		{
			Res.outz("REQUEST ICON " + id.ToString());
			message = new Message(-67);
			message.writer().writeInt(id);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x000983FC File Offset: 0x000965FC
	public void doConvertUpgrade(int index1, int index2, int index3)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(33);
			message.writer().writeByte(index1);
			message.writer().writeByte(index2);
			message.writer().writeByte(index3);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00098494 File Offset: 0x00096694
	public void inviteClanDun(string name)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(34);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00098510 File Offset: 0x00096710
	public void inputNumSplit(int indexItem, int numSplit)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(40);
			message.writer().writeByte(indexItem);
			message.writer().writeInt(numSplit);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00098598 File Offset: 0x00096798
	public void activeAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(37);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00098614 File Offset: 0x00096814
	public void clearAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(41);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00098690 File Offset: 0x00096890
	public void updateActive(int passOld, int passNew)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(38);
			message.writer().writeInt(passOld);
			message.writer().writeInt(passNew);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00098718 File Offset: 0x00096918
	public void openLockAccProtect(int pass2)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(39);
			message.writer().writeInt(pass2);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00098794 File Offset: 0x00096994
	public void getBgTemplate(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-32);
			message.writer().writeShort(id);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00098850 File Offset: 0x00096A50
	public void getMapOffline()
	{
		Message message = null;
		try
		{
			message = new Message(-33);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000988C0 File Offset: 0x00096AC0
	public void finishUpdate()
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00098930 File Offset: 0x00096B30
	public void finishUpdate(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00098994 File Offset: 0x00096B94
	public void finishLoadMap()
	{
		Message message = null;
		try
		{
			message = new Message(-39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00098A04 File Offset: 0x00096C04
	public void getChest(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-35);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00098A80 File Offset: 0x00096C80
	public void requestBagImage(sbyte ID)
	{
		Message message = null;
		try
		{
			message = new Message(-63);
			message.writer().writeByte(ID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00098AFC File Offset: 0x00096CFC
	public void getBag(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-36);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00098B78 File Offset: 0x00096D78
	public void getBody(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-37);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00098BF4 File Offset: 0x00096DF4
	public void login2(string user)
	{
		Res.outz("Login 2");
		Message message = null;
		try
		{
			message = new Message(-101);
			message.writer().writeUTF(user);
			message.writer().writeByte(1);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00098C70 File Offset: 0x00096E70
	public void getMagicTree(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-34);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00098CEC File Offset: 0x00096EEC
	public void upPotential(int typePotential, int num)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(16);
			message.writer().writeByte(typePotential);
			message.writer().writeShort(num);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00098D74 File Offset: 0x00096F74
	public void getResource(sbyte action, MyVector vResourceIndex)
	{
		Res.outz("request resource action= " + action.ToString());
		Message message = null;
		try
		{
			message = new Message(-74);
			message.writer().writeByte(action);
			bool flag = action == 2 && vResourceIndex != null;
			if (flag)
			{
				message.writer().writeShort(vResourceIndex.size());
				for (int i = 0; i < vResourceIndex.size(); i++)
				{
					message.writer().writeShort(short.Parse((string)vResourceIndex.elementAt(i)));
				}
			}
			bool flag2 = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag2)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				Service.reciveFromMainSession = true;
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00098EAC File Offset: 0x000970AC
	public void requestMapSelect(int selected)
	{
		Res.outz("request magic tree");
		Message message = null;
		try
		{
			message = new Message(-91);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00098F1C File Offset: 0x0009711C
	public void petInfo()
	{
		Message message = null;
		try
		{
			message = new Message(-107);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00098F74 File Offset: 0x00097174
	public void sendTop(string topName, sbyte selected)
	{
		Message message = null;
		try
		{
			message = new Message(-96);
			message.writer().writeUTF(topName);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00098FE4 File Offset: 0x000971E4
	public void enemy(sbyte b, int charID)
	{
		Message message = null;
		Res.outz("add enemy");
		try
		{
			message = new Message(-99);
			message.writer().writeByte(b);
			bool flag = b == 1 || b == 2;
			if (flag)
			{
				message.writer().writeInt(charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00099070 File Offset: 0x00097270
	public void kigui(sbyte action, int itemId, sbyte moneyType, int money, int quaintly)
	{
		Message message = null;
		try
		{
			Res.outz("ki gui action= " + action.ToString());
			message = new Message(-100);
			message.writer().writeByte(action);
			bool flag = action == 0;
			if (flag)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
				message.writer().writeInt(quaintly);
			}
			bool flag2 = action == 1 || action == 2;
			if (flag2)
			{
				message.writer().writeShort(itemId);
			}
			bool flag3 = action == 3;
			if (flag3)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
			}
			bool flag4 = action == 4;
			if (flag4)
			{
				message.writer().writeByte(moneyType);
				message.writer().writeByte(money);
				Res.outz(string.Concat(new object[]
				{
					"currTab= ",
					moneyType,
					" page= ",
					money
				}));
			}
			bool flag5 = action == 5;
			if (flag5)
			{
				message.writer().writeShort(itemId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00099208 File Offset: 0x00097408
	public void getFlag(sbyte action, sbyte flagType)
	{
		Message message = null;
		try
		{
			message = new Message(-103);
			message.writer().writeByte(action);
			Res.outz(string.Concat(new object[]
			{
				"------------service--  ",
				action,
				"   ",
				flagType
			}));
			bool flag = action != 0;
			if (flag)
			{
				message.writer().writeByte(flagType);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x000992B8 File Offset: 0x000974B8
	public void setLockInventory(int pass)
	{
		Message message = null;
		try
		{
			Res.outz("------------setLockInventory:     " + pass.ToString());
			message = new Message(-104);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00099334 File Offset: 0x00097534
	public void petStatus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-108);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00099398 File Offset: 0x00097598
	public void transportNow()
	{
		Message message = null;
		try
		{
			Res.outz("------------transportNow  ");
			message = new Message(-105);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x000993FC File Offset: 0x000975FC
	public void funsion(sbyte type)
	{
		Message message = null;
		try
		{
			Res.outz("FUNSION");
			message = new Message(125);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00099478 File Offset: 0x00097678
	public void imageSource(MyVector vID)
	{
		Message message = null;
		try
		{
			Res.outz("IMAGE SOURCE size= " + vID.size().ToString());
			message = new Message(-111);
			message.writer().writeShort(vID.size());
			bool flag = vID.size() > 0;
			if (flag)
			{
				for (int i = 0; i < vID.size(); i++)
				{
					Res.outz("gui len str " + ((ImageSource)vID.elementAt(i)).id);
					message.writer().writeUTF(((ImageSource)vID.elementAt(i)).id);
				}
			}
			bool flag2 = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag2)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
				Service.reciveFromMainSession = true;
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x000995D4 File Offset: 0x000977D4
	public void getQuayso()
	{
		Message message = null;
		try
		{
			message = new Message(-126);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00099638 File Offset: 0x00097838
	public void sendServerData(sbyte action, int id, sbyte[] data)
	{
		Message message = null;
		try
		{
			Res.outz("SERVER DATA");
			message = new Message(-110);
			message.writer().writeByte(action);
			bool flag = action == 1;
			if (flag)
			{
				message.writer().writeInt(id);
				bool flag2 = data != null;
				if (flag2)
				{
					int num = data.Length;
					message.writer().writeShort(num);
					message.writer().write(ref data, 0, num);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000996EC File Offset: 0x000978EC
	public void changeOnKeyScr(sbyte[] skill)
	{
		Message message = null;
		try
		{
			message = new Message(-113);
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				message.writer().writeByte(skill[i]);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00099778 File Offset: 0x00097978
	public void requestPean()
	{
		Message message = null;
		try
		{
			message = new Message(-114);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x000997DC File Offset: 0x000979DC
	public void sendThachDau(int id)
	{
		Res.outz("GUI THACH DAU");
		Message message = null;
		try
		{
			message = new Message(-118);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00099858 File Offset: 0x00097A58
	public void messagePlayerMenu(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(63);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x000998D8 File Offset: 0x00097AD8
	public void playerMenuAction(int charId, short select)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(64);
			message.writer().writeInt(charId);
			message.writer().writeShort(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00099964 File Offset: 0x00097B64
	public void getImgByName(string nameImg)
	{
		Message message = null;
		try
		{
			message = new Message(66);
			message.writer().writeUTF(nameImg);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000999C8 File Offset: 0x00097BC8
	public void SendCrackBall(byte type, byte soluong)
	{
		Message message = new Message(-127);
		try
		{
			message.writer().writeByte((int)type);
			bool flag = soluong > 0;
			if (flag)
			{
				message.writer().writeByte((int)soluong);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00099A40 File Offset: 0x00097C40
	public void SendRada(int i, int id)
	{
		Message message = new Message(sbyte.MaxValue);
		try
		{
			message.writer().writeByte(i);
			bool flag = id != -1;
			if (flag)
			{
				message.writer().writeShort(id);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00099ABC File Offset: 0x00097CBC
	public void sendOptHat(sbyte action)
	{
		Message message = new Message(24);
		try
		{
			bool flag = action == 1;
			if (flag)
			{
				sbyte[] array = Res.TakeSnapShot();
				message.writer().writeByte(1);
				message.writer().writeShort(array.Length);
				message.writer().write(array);
			}
			else
			{
				message.writer().writeByte((global::Char.myCharz().idHat != -1) ? -1 : 0);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00099B6C File Offset: 0x00097D6C
	public void sendDelAcc()
	{
		Message message = new Message(69);
		try
		{
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00099BC0 File Offset: 0x00097DC0
	public void new_skill_not_focus(sbyte idTemplateSkill, sbyte dir, short x, short y)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeSByte(20);
			message.writer().writeSByte(idTemplateSkill);
			message.writer().writeShort(global::Char.myCharz().cx);
			message.writer().writeShort(global::Char.myCharz().cy);
			message.writer().writeSByte(dir);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x040010F6 RID: 4342
	private ISession session = Session_ME.gI();

	// Token: 0x040010F7 RID: 4343
	protected static Service instance;

	// Token: 0x040010F8 RID: 4344
	public static long curCheckController;

	// Token: 0x040010F9 RID: 4345
	public static long curCheckMap;

	// Token: 0x040010FA RID: 4346
	public static long logController;

	// Token: 0x040010FB RID: 4347
	public static long logMap;

	// Token: 0x040010FC RID: 4348
	public int demGui;

	// Token: 0x040010FD RID: 4349
	public static bool reciveFromMainSession;
}
