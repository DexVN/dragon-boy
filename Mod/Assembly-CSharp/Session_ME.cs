using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class Session_ME : ISession
{
	// Token: 0x060008F4 RID: 2292 RVA: 0x00099CB0 File Offset: 0x00097EB0
	public Session_ME()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00099CC5 File Offset: 0x00097EC5
	public void clearSendingMessage()
	{
		Session_ME.sender.sendingMessage.Clear();
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00099CD8 File Offset: 0x00097ED8
	public static Session_ME gI()
	{
		bool flag = Session_ME.instance == null;
		if (flag)
		{
			Session_ME.instance = new Session_ME();
		}
		return Session_ME.instance;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00099D08 File Offset: 0x00097F08
	public bool isConnected()
	{
		return Session_ME.connected && Session_ME.sc != null && Session_ME.dis != null;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00099D33 File Offset: 0x00097F33
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME.messageHandler = msgHandler;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00099D3C File Offset: 0x00097F3C
	public void connect(string host, int port)
	{
		bool flag = Session_ME.connected || Session_ME.connecting;
		if (!flag)
		{
			bool flag2 = mSystem.currentTimeMillis() < this.timeWaitConnect;
			if (!flag2)
			{
				this.timeWaitConnect = mSystem.currentTimeMillis() + 50L;
				bool flag3 = Session_ME.isMainSession;
				if (flag3)
				{
					ServerListScreen.testConnect = -1;
				}
				this.host = host;
				this.port = port;
				Session_ME.getKeyComplete = false;
				this.close();
				Debug.Log("connecting...!");
				Debug.Log("host: " + host);
				Debug.Log("port: " + port.ToString());
				Session_ME.initThread = new Thread(new ThreadStart(this.NetworkInit));
				Session_ME.initThread.Start();
			}
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00099E0C File Offset: 0x0009800C
	private void NetworkInit()
	{
		Session_ME.isCancel = false;
		Session_ME.connecting = true;
		Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
		Session_ME.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME.messageHandler.onConnectOK(Session_ME.isMainSession);
		}
		catch (Exception)
		{
			bool flag = Session_ME.messageHandler != null;
			if (flag)
			{
				this.close();
				Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
			}
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00099E9C File Offset: 0x0009809C
	public void doConnect(string host, int port)
	{
		Session_ME.sc = new TcpClient();
		Session_ME.sc.Connect(host, port);
		Session_ME.dataStream = Session_ME.sc.GetStream();
		Session_ME.dis = new BinaryReader(Session_ME.dataStream, new UTF8Encoding());
		Session_ME.dos = new BinaryWriter(Session_ME.dataStream, new UTF8Encoding());
		Session_ME.sendThread = new Thread(new ThreadStart(Session_ME.sender.run));
		Session_ME.sendThread.Start();
		Session_ME.MessageCollector @object = new Session_ME.MessageCollector();
		Cout.LogError("new -----");
		Session_ME.collectorThread = new Thread(new ThreadStart(@object.run));
		Session_ME.collectorThread.Start();
		Session_ME.timeConnected = Session_ME.currentTimeMillis();
		Session_ME.connecting = false;
		Session_ME.doSendMessage(new Message(-27));
		Session_ME.key = null;
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00099F72 File Offset: 0x00098172
	public void sendMessage(Message message)
	{
		Session_ME.count++;
		Res.outz("SEND MSG: " + message.command.ToString());
		Session_ME.sender.AddMessage(message);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00099FA8 File Offset: 0x000981A8
	private static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			bool flag = Session_ME.getKeyComplete;
			if (flag)
			{
				sbyte value = Session_ME.writeKey(m.command);
				Session_ME.dos.Write(value);
			}
			else
			{
				Session_ME.dos.Write(m.command);
			}
			bool flag2 = data != null;
			if (flag2)
			{
				int num = data.Length;
				bool flag3 = Session_ME.getKeyComplete;
				if (flag3)
				{
					int num2 = (int)Session_ME.writeKey((sbyte)(num >> 8));
					Session_ME.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME.writeKey((sbyte)(num & 255));
					Session_ME.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME.dos.Write((ushort)num);
				}
				bool flag4 = Session_ME.getKeyComplete;
				if (flag4)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME.writeKey(data[i]);
						Session_ME.dos.Write(value2);
					}
				}
				Session_ME.sendByteCount += 5 + data.Length;
			}
			else
			{
				bool flag5 = Session_ME.getKeyComplete;
				if (flag5)
				{
					int num4 = 0;
					int num5 = (int)Session_ME.writeKey((sbyte)(num4 >> 8));
					Session_ME.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME.writeKey((sbyte)(num4 & 255));
					Session_ME.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME.dos.Write((ushort)0);
				}
				Session_ME.sendByteCount += 5;
			}
			Session_ME.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
			Session_ME.dos.Flush();
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0009A160 File Offset: 0x00098360
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curR;
		Session_ME.curR = (sbyte)(b2 + 1);
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME.curR >= Session_ME.key.Length;
		if (flag)
		{
			Session_ME.curR %= (sbyte)Session_ME.key.Length;
		}
		return result;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0009A1C8 File Offset: 0x000983C8
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curW;
		Session_ME.curW = (sbyte)(b2 + 1);
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME.curW >= Session_ME.key.Length;
		if (flag)
		{
			Session_ME.curW %= (sbyte)Session_ME.key.Length;
		}
		return result;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0009A230 File Offset: 0x00098430
	public static void onRecieveMsg(Message msg)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Session_ME.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0009A274 File Offset: 0x00098474
	public static void update()
	{
		while (Session_ME.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME.recieveMsg.elementAt(0);
			bool isStopReadMessage = Controller.isStopReadMessage;
			if (isStopReadMessage)
			{
				break;
			}
			bool flag = message == null;
			if (flag)
			{
				Session_ME.recieveMsg.removeElementAt(0);
				break;
			}
			Session_ME.messageHandler.onMessage(message);
			Session_ME.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0009A2E3 File Offset: 0x000984E3
	public void close()
	{
		Session_ME.cleanNetwork();
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0009A2EC File Offset: 0x000984EC
	private static void cleanNetwork()
	{
		Session_ME.key = null;
		Session_ME.curR = 0;
		Session_ME.curW = 0;
		try
		{
			Session_ME.connected = false;
			Session_ME.connecting = false;
			bool flag = Session_ME.sc != null;
			if (flag)
			{
				Session_ME.sc.Close();
				Session_ME.sc = null;
			}
			bool flag2 = Session_ME.dataStream != null;
			if (flag2)
			{
				Session_ME.dataStream.Close();
				Session_ME.dataStream = null;
			}
			bool flag3 = Session_ME.dos != null;
			if (flag3)
			{
				Session_ME.dos.Close();
				Session_ME.dos = null;
			}
			bool flag4 = Session_ME.dis != null;
			if (flag4)
			{
				Session_ME.dis.Close();
				Session_ME.dis = null;
			}
			bool flag5 = Thread.CurrentThread.Name == Main.mainThreadName;
			if (flag5)
			{
				bool flag6 = Session_ME.sendThread != null;
				if (flag6)
				{
					Session_ME.sendThread.Abort();
				}
				Session_ME.sendThread = null;
				bool flag7 = Session_ME.initThread != null;
				if (flag7)
				{
					Session_ME.initThread.Abort();
				}
				Session_ME.initThread = null;
				bool flag8 = Session_ME.collectorThread != null;
				if (flag8)
				{
					Session_ME.collectorThread.Abort();
				}
				Session_ME.collectorThread = null;
			}
			else
			{
				Session_ME.sendThread = null;
				Session_ME.initThread = null;
				Session_ME.collectorThread = null;
			}
			bool flag9 = Session_ME.isMainSession;
			if (flag9)
			{
				ServerListScreen.testConnect = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0009A464 File Offset: 0x00098664
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0009A47C File Offset: 0x0009867C
	public static byte convertSbyteToByte(sbyte var)
	{
		bool flag = var > 0;
		byte result;
		if (flag)
		{
			result = (byte)var;
		}
		else
		{
			result = (byte)((int)var + 256);
		}
		return result;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0009A4A4 File Offset: 0x000986A4
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0009A4FC File Offset: 0x000986FC
	public bool isCompareIPConnect()
	{
		return true;
	}

	// Token: 0x040010FE RID: 4350
	protected static Session_ME instance = new Session_ME();

	// Token: 0x040010FF RID: 4351
	private static NetworkStream dataStream;

	// Token: 0x04001100 RID: 4352
	private static BinaryReader dis;

	// Token: 0x04001101 RID: 4353
	private static BinaryWriter dos;

	// Token: 0x04001102 RID: 4354
	public static IMessageHandler messageHandler;

	// Token: 0x04001103 RID: 4355
	public static bool isMainSession = true;

	// Token: 0x04001104 RID: 4356
	private static TcpClient sc;

	// Token: 0x04001105 RID: 4357
	public static bool connected;

	// Token: 0x04001106 RID: 4358
	public static bool connecting;

	// Token: 0x04001107 RID: 4359
	private static Session_ME.Sender sender = new Session_ME.Sender();

	// Token: 0x04001108 RID: 4360
	public static Thread initThread;

	// Token: 0x04001109 RID: 4361
	public static Thread collectorThread;

	// Token: 0x0400110A RID: 4362
	public static Thread sendThread;

	// Token: 0x0400110B RID: 4363
	public static int sendByteCount;

	// Token: 0x0400110C RID: 4364
	public static int recvByteCount;

	// Token: 0x0400110D RID: 4365
	private static bool getKeyComplete;

	// Token: 0x0400110E RID: 4366
	public static sbyte[] key = null;

	// Token: 0x0400110F RID: 4367
	private static sbyte curR;

	// Token: 0x04001110 RID: 4368
	private static sbyte curW;

	// Token: 0x04001111 RID: 4369
	private static int timeConnected;

	// Token: 0x04001112 RID: 4370
	private long lastTimeConn;

	// Token: 0x04001113 RID: 4371
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04001114 RID: 4372
	public static bool isCancel;

	// Token: 0x04001115 RID: 4373
	private string host;

	// Token: 0x04001116 RID: 4374
	private int port;

	// Token: 0x04001117 RID: 4375
	private long timeWaitConnect;

	// Token: 0x04001118 RID: 4376
	public static int count;

	// Token: 0x04001119 RID: 4377
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x020000CC RID: 204
	public class Sender
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x000AFC48 File Offset: 0x000ADE48
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000AFC5D File Offset: 0x000ADE5D
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000AFC70 File Offset: 0x000ADE70
		public void run()
		{
			while (Session_ME.connected)
			{
				try
				{
					bool getKeyComplete = Session_ME.getKeyComplete;
					if (getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message i = this.sendingMessage[0];
							Session_ME.doSendMessage(i);
							this.sendingMessage.RemoveAt(0);
						}
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception ex)
					{
						Cout.LogError(ex.ToString());
					}
				}
				catch (Exception)
				{
					Res.outz("error send message! ");
				}
			}
		}

		// Token: 0x040014ED RID: 5357
		public List<Message> sendingMessage;
	}

	// Token: 0x020000CD RID: 205
	private class MessageCollector
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x000AFD1C File Offset: 0x000ADF1C
		public void run()
		{
			try
			{
				while (Session_ME.connected)
				{
					Message message = this.readMessage();
					bool flag = message == null;
					if (flag)
					{
						break;
					}
					try
					{
						bool flag2 = message.command == -27;
						if (flag2)
						{
							this.getKey(message);
						}
						else
						{
							Session_ME.onRecieveMsg(message);
						}
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 1");
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 2");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log("error read message!");
				Debug.Log(ex.Message.ToString());
			}
			bool connected = Session_ME.connected;
			if (connected)
			{
				bool flag3 = Session_ME.messageHandler != null;
				if (flag3)
				{
					bool flag4 = Session_ME.currentTimeMillis() - Session_ME.timeConnected > 500;
					if (flag4)
					{
						Session_ME.messageHandler.onDisconnected(Session_ME.isMainSession);
					}
					else
					{
						Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
					}
				}
				bool flag5 = Session_ME.sc != null;
				if (flag5)
				{
					Session_ME.cleanNetwork();
				}
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000AFE60 File Offset: 0x000AE060
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME.key;
					int num = j + 1;
					key[num] ^= Session_ME.key[j];
				}
				Session_ME.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = (message.reader().readByte() != 0);
				bool flag = Session_ME.isMainSession && GameMidlet.isConnect2;
				if (flag)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000AFF54 File Offset: 0x000AE154
		private Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num3 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num4 = (num3 * 256 + num2) * 256 + num;
			sbyte[] array = new sbyte[num4];
			byte[] src = Session_ME.dis.ReadBytes(num4);
			Buffer.BlockCopy(src, 0, array, 0, num4);
			Session_ME.recvByteCount += 5 + num4;
			int num5 = Session_ME.recvByteCount + Session_ME.sendByteCount;
			Session_ME.strRecvByteCount = string.Concat(new object[]
			{
				num5 / 1024,
				".",
				num5 % 1024 / 102,
				"Kb"
			});
			bool getKeyComplete = Session_ME.getKeyComplete;
			if (getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000B007C File Offset: 0x000AE27C
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME.dis.ReadSByte();
				bool getKeyComplete = Session_ME.getKeyComplete;
				if (getKeyComplete)
				{
					b = Session_ME.readKey(b);
				}
				bool flag = b == -32 || b == -66 || b == 11 || b == -67 || b == -74 || b == -87 || b == 66;
				if (flag)
				{
					return this.readMessage2(b);
				}
				bool getKeyComplete2 = Session_ME.getKeyComplete;
				int num;
				if (getKeyComplete2)
				{
					sbyte b2 = Session_ME.dis.ReadSByte();
					sbyte b3 = Session_ME.dis.ReadSByte();
					num = (((int)Session_ME.readKey(b2) & 255) << 8 | ((int)Session_ME.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME.dis.ReadSByte();
					sbyte b5 = Session_ME.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME.recvByteCount += 5 + num;
				int num2 = Session_ME.recvByteCount + Session_ME.sendByteCount;
				Session_ME.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				bool getKeyComplete3 = Session_ME.getKeyComplete;
				if (getKeyComplete3)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME.readKey(array[i]);
					}
				}
				return new Message(b, array);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.StackTrace.ToString());
			}
			return null;
		}
	}
}
