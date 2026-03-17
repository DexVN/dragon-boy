using System;

// Token: 0x02000043 RID: 67
public interface IMessageHandler
{
	// Token: 0x060003D8 RID: 984
	void onMessage(Message message);

	// Token: 0x060003D9 RID: 985
	void onConnectionFail(bool isMain);

	// Token: 0x060003DA RID: 986
	void onDisconnected(bool isMain);

	// Token: 0x060003DB RID: 987
	void onConnectOK(bool isMain);
}
