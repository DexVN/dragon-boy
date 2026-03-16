using System;

// Token: 0x0200004A RID: 74
public interface ISession
{
	// Token: 0x060002BD RID: 701
	bool isConnected();

	// Token: 0x060002BE RID: 702
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x060002BF RID: 703
	void connect(string host, int port);

	// Token: 0x060002C0 RID: 704
	void sendMessage(Message message);

	// Token: 0x060002C1 RID: 705
	void close();
}
