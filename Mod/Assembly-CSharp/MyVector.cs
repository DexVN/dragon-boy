using System;
using System.Collections;

// Token: 0x0200007C RID: 124
public class MyVector
{
	// Token: 0x06000615 RID: 1557 RVA: 0x0006B1F0 File Offset: 0x000693F0
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0006B1F0 File Offset: 0x000693F0
	public MyVector(string s)
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0006B205 File Offset: 0x00069405
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0006B216 File Offset: 0x00069416
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0006B228 File Offset: 0x00069428
	public bool contains(object o)
	{
		return this.a.Contains(o);
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0006B248 File Offset: 0x00069448
	public int size()
	{
		bool flag = this.a == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.a.Count;
		}
		return result;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0006B278 File Offset: 0x00069478
	public object elementAt(int index)
	{
		bool flag = index > -1 && index < this.a.Count;
		object result;
		if (flag)
		{
			result = this.a[index];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0006B2B4 File Offset: 0x000694B4
	public void set(int index, object obj)
	{
		bool flag = index > -1 && index < this.a.Count;
		if (flag)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0006B2EC File Offset: 0x000694EC
	public void setElementAt(object obj, int index)
	{
		bool flag = index > -1 && index < this.a.Count;
		if (flag)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0006B324 File Offset: 0x00069524
	public int indexOf(object o)
	{
		return this.a.IndexOf(o);
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0006B344 File Offset: 0x00069544
	public void removeElementAt(int index)
	{
		bool flag = index > -1 && index < this.a.Count;
		if (flag)
		{
			this.a.RemoveAt(index);
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0006B37A File Offset: 0x0006957A
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0006B38A File Offset: 0x0006958A
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0006B399 File Offset: 0x00069599
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0006B3AC File Offset: 0x000695AC
	public object firstElement()
	{
		return this.a[0];
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0006B3CC File Offset: 0x000695CC
	public object lastElement()
	{
		return this.a[this.a.Count - 1];
	}

	// Token: 0x04000E02 RID: 3586
	private ArrayList a;
}
