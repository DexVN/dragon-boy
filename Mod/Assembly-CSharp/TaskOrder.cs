using System;

// Token: 0x02000091 RID: 145
public class TaskOrder
{
	// Token: 0x060004AF RID: 1199 RVA: 0x0003BF3D File Offset: 0x0003A33D
	public TaskOrder(sbyte taskId, short count, short maxCount, string name, string description, sbyte killId, sbyte mapId)
	{
		this.count = (int)count;
		this.maxCount = maxCount;
		this.taskId = (int)taskId;
		this.name = name;
		this.description = description;
		this.killId = (int)killId;
		this.mapId = (int)mapId;
	}

	// Token: 0x04000801 RID: 2049
	public const sbyte TASK_DAY = 0;

	// Token: 0x04000802 RID: 2050
	public const sbyte TASK_BOSS = 1;

	// Token: 0x04000803 RID: 2051
	public int taskId;

	// Token: 0x04000804 RID: 2052
	public int count;

	// Token: 0x04000805 RID: 2053
	public short maxCount;

	// Token: 0x04000806 RID: 2054
	public string name;

	// Token: 0x04000807 RID: 2055
	public string description;

	// Token: 0x04000808 RID: 2056
	public int killId;

	// Token: 0x04000809 RID: 2057
	public int mapId;
}
