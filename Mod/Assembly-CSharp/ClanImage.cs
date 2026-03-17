using System;

// Token: 0x02000015 RID: 21
public class ClanImage
{
	// Token: 0x0600016F RID: 367 RVA: 0x0001CBF6 File Offset: 0x0001ADF6
	public static void addClanImage(ClanImage cm)
	{
		Service.gI().clanImage((sbyte)cm.ID);
		ClanImage.vClanImage.addElement(cm);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0001CC18 File Offset: 0x0001AE18
	public static ClanImage getClanImage(short ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			bool flag = clanImage.ID == (int)ID;
			if (flag)
			{
				return clanImage;
			}
		}
		return null;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0001CC6C File Offset: 0x0001AE6C
	public static bool isExistClanImage(int ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			bool flag = clanImage.ID == ID;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040002FA RID: 762
	public int ID;

	// Token: 0x040002FB RID: 763
	public string name;

	// Token: 0x040002FC RID: 764
	public short[] idImage;

	// Token: 0x040002FD RID: 765
	public int xu;

	// Token: 0x040002FE RID: 766
	public int luong;

	// Token: 0x040002FF RID: 767
	public static MyVector vClanImage = new MyVector();

	// Token: 0x04000300 RID: 768
	public static MyHashTable idImages = new MyHashTable();
}
