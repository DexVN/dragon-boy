using System;

// Token: 0x02000059 RID: 89
public class ItemTemplates
{
	// Token: 0x06000486 RID: 1158 RVA: 0x00059EF0 File Offset: 0x000580F0
	public static void add(ItemTemplate it)
	{
		ItemTemplates.itemTemplates.put(it.id, it);
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00059F0C File Offset: 0x0005810C
	public static ItemTemplate get(short id)
	{
		return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00059F34 File Offset: 0x00058134
	public static short getPart(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).part;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00059F54 File Offset: 0x00058154
	public static short getIcon(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).iconID;
	}

	// Token: 0x040009C9 RID: 2505
	public static MyHashTable itemTemplates = new MyHashTable();
}
