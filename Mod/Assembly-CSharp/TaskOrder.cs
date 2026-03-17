using System;

// Token: 0x020000B1 RID: 177
public class TaskOrder
{
	// Token: 0x060009BE RID: 2494 RVA: 0x000A29F2 File Offset: 0x000A0BF2
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

	// Token: 0x04001243 RID: 4675
	public const sbyte TASK_DAY = 0;

	// Token: 0x04001244 RID: 4676
	public const sbyte TASK_BOSS = 1;

	// Token: 0x04001245 RID: 4677
	public int taskId;

	// Token: 0x04001246 RID: 4678
	public int count;

	// Token: 0x04001247 RID: 4679
	public short maxCount;

	// Token: 0x04001248 RID: 4680
	public string name;

	// Token: 0x04001249 RID: 4681
	public string description;

	// Token: 0x0400124A RID: 4682
	public int killId;

	// Token: 0x0400124B RID: 4683
	public int mapId;
}
