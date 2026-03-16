using System;

// Token: 0x020000AA RID: 170
public interface IChatable
{
	// Token: 0x060007B2 RID: 1970
	void onChatFromMe(string text, string to);

	// Token: 0x060007B3 RID: 1971
	void onCancelChat();
}
