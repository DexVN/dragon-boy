using System;
using System.Collections;

// Token: 0x02000077 RID: 119
public class MyHashTable
{
	// Token: 0x060005E8 RID: 1512 RVA: 0x0006A5B0 File Offset: 0x000687B0
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0006A5CE File Offset: 0x000687CE
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0006A5E0 File Offset: 0x000687E0
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0006A600 File Offset: 0x00068800
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0006A620 File Offset: 0x00068820
	public void put(object k, object v)
	{
		bool flag = this.h.ContainsKey(k);
		if (flag)
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0006A65B File Offset: 0x0006885B
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0006A65B File Offset: 0x0006885B
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0006A66C File Offset: 0x0006886C
	public bool containsKey(object key)
	{
		return this.h.ContainsKey(key);
	}

	// Token: 0x04000DFA RID: 3578
	public Hashtable h = new Hashtable();
}
