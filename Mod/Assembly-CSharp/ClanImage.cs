using System;

// Token: 0x02000031 RID: 49
public class ClanImage
{
	// Token: 0x0600021F RID: 543 RVA: 0x0000E054 File Offset: 0x0000C454
	public static void addClanImage(ClanImage cm)
	{
		Service.gI().clanImage((sbyte)cm.ID);
		ClanImage.vClanImage.addElement(cm);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000E074 File Offset: 0x0000C474
	public static ClanImage getClanImage(short ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			if (clanImage.ID == (int)ID)
			{
				return clanImage;
			}
		}
		return null;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000E0BC File Offset: 0x0000C4BC
	public static bool isExistClanImage(int ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			if (clanImage.ID == ID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040001F5 RID: 501
	public int ID;

	// Token: 0x040001F6 RID: 502
	public string name;

	// Token: 0x040001F7 RID: 503
	public short[] idImage;

	// Token: 0x040001F8 RID: 504
	public int xu;

	// Token: 0x040001F9 RID: 505
	public int luong;

	// Token: 0x040001FA RID: 506
	public static MyVector vClanImage = new MyVector();

	// Token: 0x040001FB RID: 507
	public static MyHashTable idImages = new MyHashTable();
}
