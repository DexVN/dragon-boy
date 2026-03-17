using System;

// Token: 0x020000BC RID: 188
public class Waypoint : IActionListener
{
	// Token: 0x06000A22 RID: 2594 RVA: 0x000A75A0 File Offset: 0x000A57A0
	public Waypoint(short minX, short minY, short maxX, short maxY, bool isEnter, bool isOffline, string name)
	{
		this.minX = minX;
		this.minY = minY;
		this.maxX = maxX;
		this.maxY = maxY;
		name = Res.changeString(name);
		this.isEnter = isEnter;
		this.isOffline = isOffline;
		bool flag = (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && this.minX >= 0 && this.minX <= 24;
		if (!flag)
		{
			bool flag2 = ((TileMap.mapID == 0 && global::Char.myCharz().cgender != 0) || (TileMap.mapID == 7 && global::Char.myCharz().cgender != 1) || (TileMap.mapID == 14 && global::Char.myCharz().cgender != 2)) && isOffline;
			if (!flag2)
			{
				bool flag3 = !TileMap.isInAirMap() && TileMap.mapID != 47;
				if (flag3)
				{
					bool flag4 = !isEnter && !isOffline;
					if (flag4)
					{
						this.popup = new PopUp(name, (int)minX, (int)(minY - 24));
						this.popup.command = new Command(null, this, 1, this);
						this.popup.isWayPoint = true;
						this.popup.isPaint = false;
						PopUp.addPopUp(this.popup);
					}
					else
					{
						bool flag5 = TileMap.isTrainingMap();
						if (flag5)
						{
							this.popup = new PopUp(name, (int)minX, (int)(minY - 16));
						}
						else
						{
							int x = (int)(minX + (maxX - minX) / 2);
							this.popup = new PopUp(name, x, (int)(minY - ((minY == 0) ? -32 : 16)));
						}
						this.popup.command = new Command(null, this, 2, this);
						this.popup.isWayPoint = true;
						this.popup.isPaint = false;
						PopUp.addPopUp(this.popup);
					}
					TileMap.vGo.addElement(this);
				}
				else
				{
					bool flag6 = minY > 150 && TileMap.isInAirMap();
					if (!flag6)
					{
						this.popup = new PopUp(name, (int)(minX + (maxX - minX) / 2), (int)(maxY - ((minX <= 100) ? 48 : 24)));
						this.popup.command = new Command(null, this, 1, this);
						this.popup.isWayPoint = true;
						this.popup.isPaint = false;
						PopUp.addPopUp(this.popup);
						TileMap.vGo.addElement(this);
					}
				}
			}
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x000A780C File Offset: 0x000A5A0C
	public void perform(int idAction, object p)
	{
		bool flag = idAction != 1;
		if (flag)
		{
			bool flag2 = idAction == 2;
			if (flag2)
			{
				GameScr.gI().auto = 0;
				bool flag3 = global::Char.myCharz().isInEnterOfflinePoint() != null;
				if (flag3)
				{
					Service.gI().charMove();
					InfoDlg.showWait();
					Service.gI().getMapOffline();
					global::Char.ischangingMap = true;
				}
				else
				{
					bool flag4 = global::Char.myCharz().isInEnterOnlinePoint() != null;
					if (flag4)
					{
						Service.gI().charMove();
						Service.gI().requestChangeMap();
						global::Char.isLockKey = true;
						global::Char.ischangingMap = true;
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						InfoDlg.showWait();
					}
					else
					{
						int xEnd = (int)((this.minX + this.maxX) / 2);
						int yEnd = (int)this.maxY;
						global::Char.myCharz().currentMovePoint = new MovePoint(xEnd, yEnd);
						global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
						global::Char.myCharz().endMovePointCommand = new Command(null, this, 2, null);
					}
				}
			}
		}
		else
		{
			int xEnd2 = (int)((this.minX + this.maxX) / 2);
			int yEnd2 = (int)this.maxY;
			bool flag5 = this.maxY > this.minY + 24;
			if (flag5)
			{
				yEnd2 = (int)((this.minY + this.maxY) / 2);
			}
			GameScr.gI().auto = 0;
			global::Char.myCharz().currentMovePoint = new MovePoint(xEnd2, yEnd2);
			global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
			Service.gI().charMove();
		}
	}

	// Token: 0x04001344 RID: 4932
	public short minX;

	// Token: 0x04001345 RID: 4933
	public short minY;

	// Token: 0x04001346 RID: 4934
	public short maxX;

	// Token: 0x04001347 RID: 4935
	public short maxY;

	// Token: 0x04001348 RID: 4936
	public bool isEnter;

	// Token: 0x04001349 RID: 4937
	public bool isOffline;

	// Token: 0x0400134A RID: 4938
	public PopUp popup;
}
