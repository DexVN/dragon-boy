using System;

// Token: 0x0200003E RID: 62
public interface IChatable
{
	// Token: 0x060003AC RID: 940
	void onChatFromMe(string text, string to);

	// Token: 0x060003AD RID: 941
	void onCancelChat();
}
