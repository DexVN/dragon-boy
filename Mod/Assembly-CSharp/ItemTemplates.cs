using System;

// Token: 0x02000067 RID: 103
public class ItemTemplates
{
	// Token: 0x060003A8 RID: 936 RVA: 0x0001E6DA File Offset: 0x0001CADA
	public static void add(ItemTemplate it)
	{
		ItemTemplates.itemTemplates.put(it.id, it);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001E6F2 File Offset: 0x0001CAF2
	public static ItemTemplate get(short id)
	{
		return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001E709 File Offset: 0x0001CB09
	public static short getPart(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).part;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001E716 File Offset: 0x0001CB16
	public static short getIcon(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).iconID;
	}

	// Token: 0x04000628 RID: 1576
	public static MyHashTable itemTemplates = new MyHashTable();
}
