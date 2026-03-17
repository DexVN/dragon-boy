using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class Session_ME2 : ISession
{
	// Token: 0x06000909 RID: 2313 RVA: 0x00099CB0 File Offset: 0x00097EB0
	public Session_ME2()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0009A545 File Offset: 0x00098745
	public void clearSendingMessage()
	{
		Session_ME2.sender.sendingMessage.Clear();
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0009A558 File Offset: 0x00098758
	public static Session_ME2 gI()
	{
		bool flag = Session_ME2.instance == null;
		if (flag)
		{
			Session_ME2.instance = new Session_ME2();
		}
		return Session_ME2.instance;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0009A588 File Offset: 0x00098788
	public bool isConnected()
	{
		return Session_ME2.connected && Session_ME2.sc != null && Session_ME2.dis != null;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0009A5B3 File Offset: 0x000987B3
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME2.messageHandler = msgHandler;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0009A5BC File Offset: 0x000987BC
	public void connect(string host, int port)
	{
		bool flag = Session_ME2.connected || Session_ME2.connecting;
		if (!flag)
		{
			bool flag2 = mSystem.currentTimeMillis() < this.timeWaitConnect;
			if (!flag2)
			{
				this.timeWaitConnect = mSystem.currentTimeMillis() + 50L;
				this.host = host;
				this.port = port;
				Session_ME2.getKeyComplete = false;
				this.close();
				Debug.Log("connecting...!");
				Debug.Log("host: " + host);
				Debug.Log("port: " + port.ToString());
				Session_ME2.initThread = new Thread(new ThreadStart(this.NetworkInit));
				Session_ME2.initThread.Start();
			}
		}
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0009A678 File Offset: 0x00098878
	private void NetworkInit()
	{
		Session_ME2.isCancel = false;
		Session_ME2.connecting = true;
		Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
		Session_ME2.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME2.messageHandler.onConnectOK(Session_ME2.isMainSession);
		}
		catch (Exception ex)
		{
			bool flag = Session_ME2.messageHandler != null;
			if (flag)
			{
				this.close();
				Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
			}
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0009A708 File Offset: 0x00098908
	public void doConnect(string host, int port)
	{
		Session_ME2.sc = new TcpClient();
		Session_ME2.sc.Connect(host, port);
		Session_ME2.dataStream = Session_ME2.sc.GetStream();
		Session_ME2.dis = new BinaryReader(Session_ME2.dataStream, new UTF8Encoding());
		Session_ME2.dos = new BinaryWriter(Session_ME2.dataStream, new UTF8Encoding());
		Session_ME2.sendThread = new Thread(new ThreadStart(Session_ME2.sender.run));
		Session_ME2.sendThread.Start();
		Session_ME2.MessageCollector @object = new Session_ME2.MessageCollector();
		Cout.LogError("new -----");
		Session_ME2.collectorThread = new Thread(new ThreadStart(@object.run));
		Session_ME2.collectorThread.Start();
		Session_ME2.timeConnected = Session_ME2.currentTimeMillis();
		Session_ME2.connecting = false;
		Session_ME2.doSendMessage(new Message(-27));
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0009A7D8 File Offset: 0x000989D8
	public void sendMessage(Message message)
	{
		Res.outz("SEND MSG: " + message.command.ToString());
		Session_ME2.sender.AddMessage(message);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0009A804 File Offset: 0x00098A04
	private static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			bool flag = Session_ME2.getKeyComplete;
			if (flag)
			{
				sbyte value = Session_ME2.writeKey(m.command);
				Session_ME2.dos.Write(value);
			}
			else
			{
				Session_ME2.dos.Write(m.command);
			}
			bool flag2 = data != null;
			if (flag2)
			{
				int num = data.Length;
				bool flag3 = Session_ME2.getKeyComplete;
				if (flag3)
				{
					int num2 = (int)Session_ME2.writeKey((sbyte)(num >> 8));
					Session_ME2.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME2.writeKey((sbyte)(num & 255));
					Session_ME2.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME2.dos.Write((ushort)num);
				}
				bool flag4 = Session_ME2.getKeyComplete;
				if (flag4)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME2.writeKey(data[i]);
						Session_ME2.dos.Write(value2);
					}
				}
				Session_ME2.sendByteCount += 5 + data.Length;
			}
			else
			{
				bool flag5 = Session_ME2.getKeyComplete;
				if (flag5)
				{
					int num4 = 0;
					int num5 = (int)Session_ME2.writeKey((sbyte)(num4 >> 8));
					Session_ME2.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME2.writeKey((sbyte)(num4 & 255));
					Session_ME2.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME2.dos.Write(0);
				}
				Session_ME2.sendByteCount += 5;
			}
			Session_ME2.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0009A9B0 File Offset: 0x00098BB0
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curR;
		Session_ME2.curR = (sbyte)(b2 + 1);
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME2.curR >= Session_ME2.key.Length;
		if (flag)
		{
			Session_ME2.curR %= (sbyte)Session_ME2.key.Length;
		}
		return result;
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0009AA18 File Offset: 0x00098C18
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curW;
		Session_ME2.curW = (sbyte)(b2 + 1);
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME2.curW >= Session_ME2.key.Length;
		if (flag)
		{
			Session_ME2.curW %= (sbyte)Session_ME2.key.Length;
		}
		return result;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0009AA80 File Offset: 0x00098C80
	public static void onRecieveMsg(Message msg)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Session_ME2.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME2.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0009AAC4 File Offset: 0x00098CC4
	public static void update()
	{
		while (Session_ME2.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME2.recieveMsg.elementAt(0);
			bool isStopReadMessage = Controller.isStopReadMessage;
			if (isStopReadMessage)
			{
				break;
			}
			bool flag = message == null;
			if (flag)
			{
				Session_ME2.recieveMsg.removeElementAt(0);
				break;
			}
			Session_ME2.messageHandler.onMessage(message);
			Session_ME2.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0009AB33 File Offset: 0x00098D33
	public void close()
	{
		Session_ME2.cleanNetwork();
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0009AB3C File Offset: 0x00098D3C
	private static void cleanNetwork()
	{
		Session_ME2.key = null;
		Session_ME2.curR = 0;
		Session_ME2.curW = 0;
		try
		{
			Session_ME2.connected = false;
			Session_ME2.connecting = false;
			bool flag = Session_ME2.sc != null;
			if (flag)
			{
				Session_ME2.sc.Close();
				Session_ME2.sc = null;
			}
			bool flag2 = Session_ME2.dataStream != null;
			if (flag2)
			{
				Session_ME2.dataStream.Close();
				Session_ME2.dataStream = null;
			}
			bool flag3 = Session_ME2.dos != null;
			if (flag3)
			{
				Session_ME2.dos.Close();
				Session_ME2.dos = null;
			}
			bool flag4 = Session_ME2.dis != null;
			if (flag4)
			{
				Session_ME2.dis.Close();
				Session_ME2.dis = null;
			}
			Session_ME2.sendThread = null;
			Session_ME2.collectorThread = null;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0009AC0C File Offset: 0x00098E0C
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0009AC24 File Offset: 0x00098E24
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

	// Token: 0x0600091B RID: 2331 RVA: 0x0009AC4C File Offset: 0x00098E4C
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

	// Token: 0x0400111A RID: 4378
	protected static Session_ME2 instance = new Session_ME2();

	// Token: 0x0400111B RID: 4379
	private static NetworkStream dataStream;

	// Token: 0x0400111C RID: 4380
	private static BinaryReader dis;

	// Token: 0x0400111D RID: 4381
	private static BinaryWriter dos;

	// Token: 0x0400111E RID: 4382
	public static IMessageHandler messageHandler;

	// Token: 0x0400111F RID: 4383
	public static bool isMainSession = true;

	// Token: 0x04001120 RID: 4384
	private static TcpClient sc;

	// Token: 0x04001121 RID: 4385
	public static bool connected;

	// Token: 0x04001122 RID: 4386
	public static bool connecting;

	// Token: 0x04001123 RID: 4387
	private static Session_ME2.Sender sender = new Session_ME2.Sender();

	// Token: 0x04001124 RID: 4388
	public static Thread initThread;

	// Token: 0x04001125 RID: 4389
	public static Thread collectorThread;

	// Token: 0x04001126 RID: 4390
	public static Thread sendThread;

	// Token: 0x04001127 RID: 4391
	public static int sendByteCount;

	// Token: 0x04001128 RID: 4392
	public static int recvByteCount;

	// Token: 0x04001129 RID: 4393
	private static bool getKeyComplete;

	// Token: 0x0400112A RID: 4394
	public static sbyte[] key = null;

	// Token: 0x0400112B RID: 4395
	private static sbyte curR;

	// Token: 0x0400112C RID: 4396
	private static sbyte curW;

	// Token: 0x0400112D RID: 4397
	private static int timeConnected;

	// Token: 0x0400112E RID: 4398
	private long lastTimeConn;

	// Token: 0x0400112F RID: 4399
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04001130 RID: 4400
	public static bool isCancel;

	// Token: 0x04001131 RID: 4401
	private string host;

	// Token: 0x04001132 RID: 4402
	private int port;

	// Token: 0x04001133 RID: 4403
	private long timeWaitConnect;

	// Token: 0x04001134 RID: 4404
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x020000CE RID: 206
	public class Sender
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000B024C File Offset: 0x000AE44C
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000B0261 File Offset: 0x000AE461
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000B0274 File Offset: 0x000AE474
		public void run()
		{
			while (Session_ME2.connected)
			{
				try
				{
					bool getKeyComplete = Session_ME2.getKeyComplete;
					if (getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message i = this.sendingMessage[0];
							Session_ME2.doSendMessage(i);
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

		// Token: 0x040014EE RID: 5358
		public List<Message> sendingMessage;
	}

	// Token: 0x020000CF RID: 207
	private class MessageCollector
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x000B0320 File Offset: 0x000AE520
		public void run()
		{
			try
			{
				while (Session_ME2.connected)
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
							Session_ME2.onRecieveMsg(message);
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
			bool connected = Session_ME2.connected;
			if (connected)
			{
				bool flag3 = Session_ME2.messageHandler != null;
				if (flag3)
				{
					bool flag4 = Session_ME2.currentTimeMillis() - Session_ME2.timeConnected > 500;
					if (flag4)
					{
						Session_ME2.messageHandler.onDisconnected(Session_ME2.isMainSession);
					}
					else
					{
						Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
					}
				}
				bool flag5 = Session_ME2.sc != null;
				if (flag5)
				{
					Session_ME2.cleanNetwork();
				}
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000B0464 File Offset: 0x000AE664
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME2.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME2.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME2.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME2.key;
					int num = j + 1;
					key[num] ^= Session_ME2.key[j];
				}
				Session_ME2.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = (message.reader().readByte() != 0);
				bool flag = Session_ME2.isMainSession && GameMidlet.isConnect2;
				if (flag)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000B0558 File Offset: 0x000AE758
		private Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num3 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num4 = (num3 * 256 + num2) * 256 + num;
			Cout.LogError("SIZE = " + num4.ToString());
			sbyte[] array = new sbyte[num4];
			byte[] src = Session_ME2.dis.ReadBytes(num4);
			Buffer.BlockCopy(src, 0, array, 0, num4);
			Session_ME2.recvByteCount += 5 + num4;
			int num5 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
			Session_ME2.strRecvByteCount = string.Concat(new object[]
			{
				num5 / 1024,
				".",
				num5 % 1024 / 102,
				"Kb"
			});
			bool getKeyComplete = Session_ME2.getKeyComplete;
			if (getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME2.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000B0698 File Offset: 0x000AE898
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME2.dis.ReadSByte();
				bool getKeyComplete = Session_ME2.getKeyComplete;
				if (getKeyComplete)
				{
					b = Session_ME2.readKey(b);
				}
				bool flag = b == -32 || b == -66 || b == 11 || b == -67 || b == -74 || b == -87;
				if (flag)
				{
					return this.readMessage2(b);
				}
				bool getKeyComplete2 = Session_ME2.getKeyComplete;
				int num;
				if (getKeyComplete2)
				{
					sbyte b2 = Session_ME2.dis.ReadSByte();
					sbyte b3 = Session_ME2.dis.ReadSByte();
					num = (((int)Session_ME2.readKey(b2) & 255) << 8 | ((int)Session_ME2.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME2.dis.ReadSByte();
					sbyte b5 = Session_ME2.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME2.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME2.recvByteCount += 5 + num;
				int num2 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
				Session_ME2.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				bool getKeyComplete3 = Session_ME2.getKeyComplete;
				if (getKeyComplete3)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME2.readKey(array[i]);
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
