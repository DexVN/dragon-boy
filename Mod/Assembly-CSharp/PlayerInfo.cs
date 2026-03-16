using System;

// Token: 0x02000078 RID: 120
public class PlayerInfo
{
	// Token: 0x0600040D RID: 1037 RVA: 0x000327CA File Offset: 0x00030BCA
	public string getName()
	{
		return this.name;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x000327D2 File Offset: 0x00030BD2
	public void setMoney(int m)
	{
		this.xu = m;
		this.strMoney = GameCanvas.getMoneys(this.xu);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x000327EC File Offset: 0x00030BEC
	public void setName(string name)
	{
		this.name = name;
		if (name.Length > 9)
		{
			this.showName = name.Substring(0, 8);
		}
		else
		{
			this.showName = name;
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0003281C File Offset: 0x00030C1C
	public void paint(mGraphics g, int x, int y)
	{
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0003281E File Offset: 0x00030C1E
	public int getExp()
	{
		return this.exp;
	}

	// Token: 0x040006E9 RID: 1769
	public string name;

	// Token: 0x040006EA RID: 1770
	public string showName;

	// Token: 0x040006EB RID: 1771
	public string status;

	// Token: 0x040006EC RID: 1772
	public int IDDB;

	// Token: 0x040006ED RID: 1773
	private int exp;

	// Token: 0x040006EE RID: 1774
	public bool isReady;

	// Token: 0x040006EF RID: 1775
	public int xu;

	// Token: 0x040006F0 RID: 1776
	public int gold;

	// Token: 0x040006F1 RID: 1777
	public string strMoney = string.Empty;

	// Token: 0x040006F2 RID: 1778
	public sbyte finishPosition;

	// Token: 0x040006F3 RID: 1779
	public bool isMaster;

	// Token: 0x040006F4 RID: 1780
	public static Image[] imgStart;

	// Token: 0x040006F5 RID: 1781
	public sbyte[] indexLv;

	// Token: 0x040006F6 RID: 1782
	public int onlineTime;
}
