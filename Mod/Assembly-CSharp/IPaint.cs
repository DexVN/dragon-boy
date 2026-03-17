using System;

// Token: 0x0200004E RID: 78
public abstract class IPaint
{
	// Token: 0x0600042A RID: 1066
	public abstract void paintDefaultBg(mGraphics g);

	// Token: 0x0600042B RID: 1067
	public abstract void paintfillDefaultBg(mGraphics g);

	// Token: 0x0600042C RID: 1068
	public abstract void repaintCircleBg();

	// Token: 0x0600042D RID: 1069
	public abstract void paintSolidBg(mGraphics g);

	// Token: 0x0600042E RID: 1070
	public abstract void paintDefaultPopup(mGraphics g, int x, int y, int w, int h);

	// Token: 0x0600042F RID: 1071
	public abstract void paintWhitePopup(mGraphics g, int y, int x, int width, int height);

	// Token: 0x06000430 RID: 1072
	public abstract void paintDefaultPopupH(mGraphics g, int h);

	// Token: 0x06000431 RID: 1073
	public abstract void paintCmdBar(mGraphics g, Command left, Command center, Command right);

	// Token: 0x06000432 RID: 1074
	public abstract void paintSelect(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000433 RID: 1075
	public abstract void paintLogo(mGraphics g, int x, int y);

	// Token: 0x06000434 RID: 1076
	public abstract void paintHotline(mGraphics g, string num);

	// Token: 0x06000435 RID: 1077
	public abstract void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text);

	// Token: 0x06000436 RID: 1078
	public abstract void paintTabSoft(mGraphics g);

	// Token: 0x06000437 RID: 1079
	public abstract void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x06000438 RID: 1080
	public abstract void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check);

	// Token: 0x06000439 RID: 1081
	public abstract void paintDefaultScrLisst(mGraphics g, string title, string subTitle, string check);

	// Token: 0x0600043A RID: 1082
	public abstract void paintCheck(mGraphics g, int x, int y, int index);

	// Token: 0x0600043B RID: 1083
	public abstract void paintImgMsg(mGraphics g, int x, int y, int index);

	// Token: 0x0600043C RID: 1084
	public abstract void paintTitleBoard(mGraphics g, int roomID);

	// Token: 0x0600043D RID: 1085
	public abstract void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus);

	// Token: 0x0600043E RID: 1086
	public abstract void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str);

	// Token: 0x0600043F RID: 1087
	public abstract void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool issSe, int i, int wStr);

	// Token: 0x06000440 RID: 1088
	public abstract void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo);

	// Token: 0x06000441 RID: 1089
	public abstract void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x06000442 RID: 1090
	public abstract void paintScroll(mGraphics g, int x, int y, int h);

	// Token: 0x06000443 RID: 1091
	public abstract int[] getColorMsg();

	// Token: 0x06000444 RID: 1092
	public abstract void paintLogo(mGraphics g);

	// Token: 0x06000445 RID: 1093
	public abstract void paintTextLogin(mGraphics g, bool issRes);

	// Token: 0x06000446 RID: 1094
	public abstract void paintSellectBoard(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000447 RID: 1095
	public abstract int issRegissterUsingWAP();

	// Token: 0x06000448 RID: 1096
	public abstract string getCard();

	// Token: 0x06000449 RID: 1097
	public abstract void paintSellectedShop(mGraphics g, int x, int y, int w, int h);

	// Token: 0x0600044A RID: 1098
	public abstract string getUrlUpdateGame();

	// Token: 0x0600044B RID: 1099 RVA: 0x00058598 File Offset: 0x00056798
	public string getFAQLink()
	{
		return "http://wap.teamobi.com/faqs.php?provider=";
	}

	// Token: 0x0600044C RID: 1100
	public abstract void doSelect(int focus);
}
