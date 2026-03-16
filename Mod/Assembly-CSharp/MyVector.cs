using System;
using System.Collections;

// Token: 0x02000011 RID: 17
public class MyVector
{
	// Token: 0x06000070 RID: 112 RVA: 0x00003AC0 File Offset: 0x00001EC0
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003AD3 File Offset: 0x00001ED3
	public MyVector(string s)
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003AE6 File Offset: 0x00001EE6
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003AF5 File Offset: 0x00001EF5
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003B04 File Offset: 0x00001F04
	public bool contains(object o)
	{
		return this.a.Contains(o);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003B1A File Offset: 0x00001F1A
	public int size()
	{
		if (this.a == null)
		{
			return 0;
		}
		return this.a.Count;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003B34 File Offset: 0x00001F34
	public object elementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			return this.a[index];
		}
		return null;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003B5C File Offset: 0x00001F5C
	public void set(int index, object obj)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003B83 File Offset: 0x00001F83
	public void setElementAt(object obj, int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003BAA File Offset: 0x00001FAA
	public int indexOf(object o)
	{
		return this.a.IndexOf(o);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003BB8 File Offset: 0x00001FB8
	public void removeElementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a.RemoveAt(index);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003BDE File Offset: 0x00001FDE
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003BEC File Offset: 0x00001FEC
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003BF9 File Offset: 0x00001FF9
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003C08 File Offset: 0x00002008
	public object firstElement()
	{
		return this.a[0];
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003C16 File Offset: 0x00002016
	public object lastElement()
	{
		return this.a[this.a.Count - 1];
	}

	// Token: 0x04000027 RID: 39
	private ArrayList a;
}
