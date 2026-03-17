using System;

// Token: 0x0200008C RID: 140
public class PlayerInfo
{
	// Token: 0x060007A9 RID: 1961 RVA: 0x0008C6EC File Offset: 0x0008A8EC
	public string getName()
	{
		return this.name;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0008C704 File Offset: 0x0008A904
	public void setMoney(int m)
	{
		this.xu = m;
		this.strMoney = GameCanvas.getMoneys(this.xu);
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0008C720 File Offset: 0x0008A920
	public void setName(string name)
	{
		this.name = name;
		bool flag = name.Length > 9;
		if (flag)
		{
			this.showName = name.Substring(0, 8);
		}
		else
		{
			this.showName = name;
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00003136 File Offset: 0x00001336
	public void paint(mGraphics g, int x, int y)
	{
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0008C760 File Offset: 0x0008A960
	public int getExp()
	{
		return this.exp;
	}

	// Token: 0x04000FD2 RID: 4050
	public string name;

	// Token: 0x04000FD3 RID: 4051
	public string showName;

	// Token: 0x04000FD4 RID: 4052
	public string status;

	// Token: 0x04000FD5 RID: 4053
	public int IDDB;

	// Token: 0x04000FD6 RID: 4054
	private int exp;

	// Token: 0x04000FD7 RID: 4055
	public bool isReady;

	// Token: 0x04000FD8 RID: 4056
	public int xu;

	// Token: 0x04000FD9 RID: 4057
	public int gold;

	// Token: 0x04000FDA RID: 4058
	public string strMoney = string.Empty;

	// Token: 0x04000FDB RID: 4059
	public sbyte finishPosition;

	// Token: 0x04000FDC RID: 4060
	public bool isMaster;

	// Token: 0x04000FDD RID: 4061
	public static Image[] imgStart;

	// Token: 0x04000FDE RID: 4062
	public sbyte[] indexLv;

	// Token: 0x04000FDF RID: 4063
	public int onlineTime;
}
