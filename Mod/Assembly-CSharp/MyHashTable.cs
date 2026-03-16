using System;
using System.Collections;

// Token: 0x0200000D RID: 13
public class MyHashTable
{
	// Token: 0x0600005F RID: 95 RVA: 0x00003482 File Offset: 0x00001882
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003490 File Offset: 0x00001890
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000349D File Offset: 0x0000189D
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000034AA File Offset: 0x000018AA
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000034B7 File Offset: 0x000018B7
	public void put(object k, object v)
	{
		if (this.h.ContainsKey(k))
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000034E3 File Offset: 0x000018E3
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000034F1 File Offset: 0x000018F1
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000034FF File Offset: 0x000018FF
	public bool containsKey(object key)
	{
		return this.h.ContainsKey(key);
	}

	// Token: 0x04000024 RID: 36
	public Hashtable h = new Hashtable();
}
