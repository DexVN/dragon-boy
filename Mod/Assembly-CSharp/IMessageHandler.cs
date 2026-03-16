using System;

// Token: 0x02000049 RID: 73
public interface IMessageHandler
{
	// Token: 0x060002B9 RID: 697
	void onMessage(Message message);

	// Token: 0x060002BA RID: 698
	void onConnectionFail(bool isMain);

	// Token: 0x060002BB RID: 699
	void onDisconnected(bool isMain);

	// Token: 0x060002BC RID: 700
	void onConnectOK(bool isMain);
}
