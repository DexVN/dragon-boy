using System;

// Token: 0x02000052 RID: 82
public interface ISession
{
	// Token: 0x06000453 RID: 1107
	bool isConnected();

	// Token: 0x06000454 RID: 1108
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x06000455 RID: 1109
	void connect(string host, int port);

	// Token: 0x06000456 RID: 1110
	void sendMessage(Message message);

	// Token: 0x06000457 RID: 1111
	void close();
}
