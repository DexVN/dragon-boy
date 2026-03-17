using System;

// Token: 0x020000B6 RID: 182
public class TileMap
{
	// Token: 0x060009F0 RID: 2544 RVA: 0x000A5910 File Offset: 0x000A3B10
	public static void loadBg()
	{
		TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
		bool flag = mGraphics.zoomLevel == 1 || Main.isIpod || Main.isIphone4;
		if (!flag)
		{
			TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
		}
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x000A595C File Offset: 0x000A3B5C
	public static bool isVoDaiMap()
	{
		return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x000A59B0 File Offset: 0x000A3BB0
	public static bool isTrainingMap()
	{
		return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x000A59E0 File Offset: 0x000A3BE0
	public static bool mapPhuBang()
	{
		return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000A5A10 File Offset: 0x000A3C10
	public static BgItem getBIById(int id)
	{
		for (int i = 0; i < TileMap.vItemBg.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
			bool flag = bgItem.id == id;
			if (flag)
			{
				return bgItem;
			}
		}
		return null;
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000A5A64 File Offset: 0x000A3C64
	public static bool isOfflineMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			bool flag = TileMap.mapID == TileMap.offlineId[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000A5AA8 File Offset: 0x000A3CA8
	public static bool isHighterMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			bool flag = TileMap.mapID == TileMap.highterId[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x000A5AEC File Offset: 0x000A3CEC
	public static bool isToOfflineMap()
	{
		for (int i = 0; i < TileMap.toOfflineId.Length; i++)
		{
			bool flag = TileMap.mapID == TileMap.toOfflineId[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000A5B2E File Offset: 0x000A3D2E
	public static void freeTilemap()
	{
		TileMap.imgTile = null;
		mSystem.gcc();
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00003136 File Offset: 0x00001336
	public static void loadTileCreatChar()
	{
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x000A5B40 File Offset: 0x000A3D40
	public static bool isExistMoreOne(int id)
	{
		bool flag = id == 156 || id == 330 || id == 345 || id == 334;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num = 0;
				for (int i = 0; i < TileMap.vCurrItem.size(); i++)
				{
					BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
					bool flag3 = bgItem.id == id;
					if (flag3)
					{
						num++;
					}
				}
				result = (num > 2);
			}
		}
		return result;
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x000A5C1C File Offset: 0x000A3E1C
	public static void loadTileImage()
	{
		bool flag = TileMap.imgWaterfall == null;
		if (flag)
		{
			TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
		}
		bool flag2 = TileMap.imgTopWaterfall == null;
		if (flag2)
		{
			TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
		}
		bool flag3 = TileMap.imgWaterflow == null;
		if (flag3)
		{
			TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
		}
		bool flag4 = TileMap.imgWaterlowN == null;
		if (flag4)
		{
			TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
		}
		bool flag5 = TileMap.imgWaterlowN2 == null;
		if (flag5)
		{
			TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
		}
		mSystem.gcc();
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000A5CC4 File Offset: 0x000A3EC4
	public static void setTile(int index, int[] mapsArr, int type)
	{
		for (int i = 0; i < mapsArr.Length; i++)
		{
			bool flag = TileMap.maps[index] == mapsArr[i];
			if (flag)
			{
				TileMap.types[index] |= type;
				break;
			}
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x000A5D0C File Offset: 0x000A3F0C
	public static void loadMap(int tileId)
	{
		TileMap.pxh = TileMap.tmh * (int)TileMap.size;
		TileMap.pxw = TileMap.tmw * (int)TileMap.size;
		Res.outz("load tile ID= " + TileMap.tileID.ToString());
		int num = tileId - 1;
		try
		{
			for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
			{
				for (int j = 0; j < TileMap.tileType[num].Length; j++)
				{
					TileMap.setTile(i, TileMap.tileIndex[num][j], TileMap.tileType[num][j]);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Error Load Map");
			GameMidlet.instance.exit();
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x000A5DDC File Offset: 0x000A3FDC
	public static bool isInAirMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x000A5E0C File Offset: 0x000A400C
	public static bool isDoubleMap()
	{
		return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x000A5ED4 File Offset: 0x000A40D4
	public static void getTile()
	{
		bool flag = Main.typeClient == 3 || Main.typeClient == 5;
		if (flag)
		{
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID.ToString() + ".png");
			}
			else
			{
				TileMap.imgTile = new Image[100];
				for (int i = 0; i < TileMap.imgTile.Length; i++)
				{
					TileMap.imgTile[i] = GameCanvas.loadImage(string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"/",
						i + 1,
						".png"
					}));
				}
			}
		}
		else
		{
			bool flag3 = mGraphics.zoomLevel == 1;
			if (flag3)
			{
				bool flag4 = TileMap.imgTile != null;
				if (flag4)
				{
					for (int j = 0; j < TileMap.imgTile.Length; j++)
					{
						bool flag5 = TileMap.imgTile[j] != null;
						if (flag5)
						{
							TileMap.imgTile[j].texture = null;
							TileMap.imgTile[j] = null;
						}
					}
					mSystem.gcc();
				}
				TileMap.imgTile = new Image[100];
				string path = string.Empty;
				for (int k = 0; k < TileMap.imgTile.Length; k++)
				{
					bool flag6 = k < 9;
					if (flag6)
					{
						path = string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"/t_0",
							k + 1
						});
					}
					else
					{
						path = string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"/t_",
							k + 1
						});
					}
					TileMap.imgTile[k] = GameCanvas.loadImage(path);
				}
			}
			else
			{
				Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID.ToString() + "$1.png");
				bool flag7 = image != null;
				if (flag7)
				{
					Rms.DeleteStorage(string.Concat(new object[]
					{
						"x",
						mGraphics.zoomLevel,
						"t",
						TileMap.tileID
					}));
					TileMap.imgTile = new Image[100];
					for (int l = 0; l < TileMap.imgTile.Length; l++)
					{
						TileMap.imgTile[l] = GameCanvas.loadImageRMS(string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"$",
							l + 1,
							".png"
						}));
					}
				}
				else
				{
					image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID.ToString() + ".png");
					bool flag8 = image != null;
					if (flag8)
					{
						Rms.DeleteStorage("$");
						TileMap.imgTile = new Image[1];
						TileMap.imgTile[0] = image;
					}
				}
			}
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x000A621C File Offset: 0x000A441C
	public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
	{
		bool flag = TileMap.imgTile == null;
		if (!flag)
		{
			bool flag2 = TileMap.imgTile.Length == 1;
			if (flag2)
			{
				g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
			}
			else
			{
				g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
			}
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x000A629C File Offset: 0x000A449C
	public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
	{
		bool flag = TileMap.imgTile == null;
		if (!flag)
		{
			bool flag2 = TileMap.imgTile.Length == 1;
			if (flag2)
			{
				g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
			}
			else
			{
				g.drawImage(TileMap.imgTile[frame], x, y, 0);
			}
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x000A62FC File Offset: 0x000A44FC
	public static void paintTilemapLOW(mGraphics g)
	{
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				int num = TileMap.maps[j * TileMap.tmw + i] - 1;
				bool flag = num != -1;
				if (flag)
				{
					TileMap.paintTile(g, num, i, j);
				}
				bool flag2 = (TileMap.tileTypeAt(i, j) & 32) == 32;
				if (flag2)
				{
					g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				else
				{
					bool flag3 = (TileMap.tileTypeAt(i, j) & 64) == 64;
					if (flag3)
					{
						bool flag4 = (TileMap.tileTypeAt(i, j - 1) & 32) == 32;
						if (flag4)
						{
							g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
						}
						else
						{
							bool flag5 = (TileMap.tileTypeAt(i, j - 1) & 4096) == 4096;
							if (flag5)
							{
								TileMap.paintTile(g, 21, i, j);
							}
						}
						bool flag6 = TileMap.tileID == 5;
						Image arg;
						if (flag6)
						{
							arg = TileMap.imgWaterlowN;
						}
						else
						{
							bool flag7 = TileMap.tileID == 8;
							if (flag7)
							{
								arg = TileMap.imgWaterlowN2;
							}
							else
							{
								arg = TileMap.imgWaterflow;
							}
						}
						g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
				}
				bool flag8 = (TileMap.tileTypeAt(i, j) & 2048) == 2048;
				if (flag8)
				{
					bool flag9 = (TileMap.tileTypeAt(i, j - 1) & 32) == 32;
					if (flag9)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else
					{
						bool flag10 = (TileMap.tileTypeAt(i, j - 1) & 4096) == 4096;
						if (flag10)
						{
							TileMap.paintTile(g, 21, i, j);
						}
					}
					TileMap.paintTile(g, TileMap.maps[j * TileMap.tmw + i] - 1, i, j);
				}
			}
		}
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000A6550 File Offset: 0x000A4750
	public static void paintTilemap(mGraphics g)
	{
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			GameScr.gI().paintBgItem(g, 1);
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
			}
			for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
			{
				for (int k = GameScr.gssy; k < GameScr.gssye; k++)
				{
					bool flag = j != 0;
					if (flag)
					{
						bool flag2 = j != TileMap.tmw - 1;
						if (flag2)
						{
							int num = TileMap.maps[k * TileMap.tmw + j] - 1;
							bool flag3 = (TileMap.tileTypeAt(j, k) & 256) != 256;
							if (flag3)
							{
								bool flag4 = (TileMap.tileTypeAt(j, k) & 32) == 32;
								if (flag4)
								{
									g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
								}
								else
								{
									bool flag5 = (TileMap.tileTypeAt(j, k) & 128) == 128;
									if (flag5)
									{
										g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
									}
									else
									{
										bool flag6 = TileMap.tileID != 13 || num == -1;
										if (flag6)
										{
											bool flag7 = TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1;
											if (flag7)
											{
												TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
												TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
											}
											bool flag8 = TileMap.tileID == 3;
											if (flag8)
											{
											}
											bool flag9 = (TileMap.tileTypeAt(j, k) & 16) == 16;
											if (flag9)
											{
												TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
												TileMap.dbx = TileMap.bx - GameScr.gW2;
												TileMap.dfx = (int)(TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
												TileMap.fx = TileMap.dfx + GameScr.gW2;
												TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
											}
											else
											{
												bool flag10 = (TileMap.tileTypeAt(j, k) & 512) == 512;
												if (flag10)
												{
													bool flag11 = num != -1;
													if (flag11)
													{
														TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
														TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
													}
												}
												else
												{
													bool flag12 = num != -1;
													if (flag12)
													{
														TileMap.paintTile(g, num, j, k);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag13 = GameScr.cmx < 24;
			if (flag13)
			{
				for (int l = GameScr.gssy; l < GameScr.gssye; l++)
				{
					int num2 = TileMap.maps[l * TileMap.tmw + 1] - 1;
					bool flag14 = num2 != -1;
					if (flag14)
					{
						TileMap.paintTile(g, num2, 0, l);
					}
				}
			}
			bool flag15 = GameScr.cmx > GameScr.cmxLim;
			if (flag15)
			{
				int num3 = TileMap.tmw - 2;
				for (int m = GameScr.gssy; m < GameScr.gssye; m++)
				{
					int num4 = TileMap.maps[m * TileMap.tmw + num3] - 1;
					bool flag16 = num4 != -1;
					if (flag16)
					{
						TileMap.paintTile(g, num4, num3 + 1, m);
					}
				}
			}
		}
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x000A6960 File Offset: 0x000A4B60
	public static bool isWaterEff()
	{
		return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x000A69B4 File Offset: 0x000A4BB4
	public static void paintOutTilemap(mGraphics g)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			int num = 0;
			for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
			{
				for (int j = GameScr.gssy; j < GameScr.gssye; j++)
				{
					num++;
					bool flag = (TileMap.tileTypeAt(i, j) & 64) == 64;
					if (flag)
					{
						bool flag2 = TileMap.tileID == 5;
						Image arg;
						if (flag2)
						{
							arg = TileMap.imgWaterlowN;
						}
						else
						{
							bool flag3 = TileMap.tileID == 8;
							if (flag3)
							{
								arg = TileMap.imgWaterlowN2;
							}
							else
							{
								arg = TileMap.imgWaterflow;
							}
						}
						bool flag4 = !TileMap.isWaterEff();
						if (flag4)
						{
							g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
							g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
						}
						g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
						bool flag5 = TileMap.yWater == 0 && TileMap.isWaterEff();
						if (flag5)
						{
							TileMap.yWater = j * (int)TileMap.size - 12;
							int color = 16777215;
							bool flag6 = GameCanvas.typeBg == 2;
							if (flag6)
							{
								color = 10871287;
							}
							else
							{
								bool flag7 = GameCanvas.typeBg == 4;
								if (flag7)
								{
									color = 8111470;
								}
								else
								{
									bool flag8 = GameCanvas.typeBg == 7;
									if (flag8)
									{
										color = 5693125;
									}
									else
									{
										bool flag9 = GameCanvas.typeBg == 19;
										if (flag9)
										{
											color = 16711680;
										}
									}
								}
							}
							BackgroudEffect.addWater(color, TileMap.yWater + 15);
						}
					}
				}
			}
			BackgroudEffect.paintWaterAll(g);
		}
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x000A6B98 File Offset: 0x000A4D98
	public static void loadMapFromResource(int mapID)
	{
		DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID.ToString());
		TileMap.tmw = (int)((ushort)dataInputStream.read());
		TileMap.tmh = (int)((ushort)dataInputStream.read());
		TileMap.maps = new int[dataInputStream.available()];
		for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
		{
			TileMap.maps[i] = (int)((ushort)dataInputStream.read());
		}
		TileMap.types = new int[TileMap.maps.Length];
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x000A6C20 File Offset: 0x000A4E20
	public static int tileAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.maps[y * TileMap.tmw + x];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000A6C64 File Offset: 0x000A4E64
	public static int tileTypeAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.types[y * TileMap.tmw + x];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x000A6CA8 File Offset: 0x000A4EA8
	public static int tileTypeAtPixel(int px, int py)
	{
		int result;
		try
		{
			result = TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x000A6CF8 File Offset: 0x000A4EF8
	public static bool tileTypeAt(int px, int py, int t)
	{
		bool result;
		try
		{
			result = ((TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] & t) == t);
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x000A6D48 File Offset: 0x000A4F48
	public static void setTileTypeAtPixel(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000A6D6F File Offset: 0x000A4F6F
	public static void setTileTypeAt(int x, int y, int t)
	{
		TileMap.types[y * TileMap.tmw + x] = t;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000A6D82 File Offset: 0x000A4F82
	public static void killTileTypeAt(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x000A6DAC File Offset: 0x000A4FAC
	public static int tileYofPixel(int py)
	{
		return py / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000A6DCC File Offset: 0x000A4FCC
	public static int tileXofPixel(int px)
	{
		return px / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000A6DEC File Offset: 0x000A4FEC
	public static void loadMainTile()
	{
		bool flag = TileMap.lastTileID != TileMap.tileID;
		if (flag)
		{
			TileMap.getTile();
			TileMap.lastTileID = TileMap.tileID;
		}
	}

	// Token: 0x040012B6 RID: 4790
	public const int T_EMPTY = 0;

	// Token: 0x040012B7 RID: 4791
	public const int T_TOP = 2;

	// Token: 0x040012B8 RID: 4792
	public const int T_LEFT = 4;

	// Token: 0x040012B9 RID: 4793
	public const int T_RIGHT = 8;

	// Token: 0x040012BA RID: 4794
	public const int T_TREE = 16;

	// Token: 0x040012BB RID: 4795
	public const int T_WATERFALL = 32;

	// Token: 0x040012BC RID: 4796
	public const int T_WATERFLOW = 64;

	// Token: 0x040012BD RID: 4797
	public const int T_TOPFALL = 128;

	// Token: 0x040012BE RID: 4798
	public const int T_OUTSIDE = 256;

	// Token: 0x040012BF RID: 4799
	public const int T_DOWN1PIXEL = 512;

	// Token: 0x040012C0 RID: 4800
	public const int T_BRIDGE = 1024;

	// Token: 0x040012C1 RID: 4801
	public const int T_UNDERWATER = 2048;

	// Token: 0x040012C2 RID: 4802
	public const int T_SOLIDGROUND = 4096;

	// Token: 0x040012C3 RID: 4803
	public const int T_BOTTOM = 8192;

	// Token: 0x040012C4 RID: 4804
	public const int T_DIE = 16384;

	// Token: 0x040012C5 RID: 4805
	public const int T_HEBI = 32768;

	// Token: 0x040012C6 RID: 4806
	public const int T_BANG = 65536;

	// Token: 0x040012C7 RID: 4807
	public const int T_JUM8 = 131072;

	// Token: 0x040012C8 RID: 4808
	public const int T_NT0 = 262144;

	// Token: 0x040012C9 RID: 4809
	public const int T_NT1 = 524288;

	// Token: 0x040012CA RID: 4810
	public const int T_CENTER = 1;

	// Token: 0x040012CB RID: 4811
	public static int tmw;

	// Token: 0x040012CC RID: 4812
	public static int tmh;

	// Token: 0x040012CD RID: 4813
	public static int pxw;

	// Token: 0x040012CE RID: 4814
	public static int pxh;

	// Token: 0x040012CF RID: 4815
	public static int tileID;

	// Token: 0x040012D0 RID: 4816
	public static int lastTileID = -1;

	// Token: 0x040012D1 RID: 4817
	public static int[] maps;

	// Token: 0x040012D2 RID: 4818
	public static int[] types;

	// Token: 0x040012D3 RID: 4819
	public static Image[] imgTile;

	// Token: 0x040012D4 RID: 4820
	public static Image imgTileSmall;

	// Token: 0x040012D5 RID: 4821
	public static Image imgMiniMap;

	// Token: 0x040012D6 RID: 4822
	public static Image imgWaterfall;

	// Token: 0x040012D7 RID: 4823
	public static Image imgTopWaterfall;

	// Token: 0x040012D8 RID: 4824
	public static Image imgWaterflow;

	// Token: 0x040012D9 RID: 4825
	public static Image imgWaterlowN;

	// Token: 0x040012DA RID: 4826
	public static Image imgWaterlowN2;

	// Token: 0x040012DB RID: 4827
	public static Image imgWaterF;

	// Token: 0x040012DC RID: 4828
	public static Image imgLeaf;

	// Token: 0x040012DD RID: 4829
	public static sbyte size = 24;

	// Token: 0x040012DE RID: 4830
	private static int bx;

	// Token: 0x040012DF RID: 4831
	private static int dbx;

	// Token: 0x040012E0 RID: 4832
	private static int fx;

	// Token: 0x040012E1 RID: 4833
	private static int dfx;

	// Token: 0x040012E2 RID: 4834
	public static string[] instruction;

	// Token: 0x040012E3 RID: 4835
	public static int[] iX;

	// Token: 0x040012E4 RID: 4836
	public static int[] iY;

	// Token: 0x040012E5 RID: 4837
	public static int[] iW;

	// Token: 0x040012E6 RID: 4838
	public static int iCount;

	// Token: 0x040012E7 RID: 4839
	public static bool isMapDouble = false;

	// Token: 0x040012E8 RID: 4840
	public static string mapName = string.Empty;

	// Token: 0x040012E9 RID: 4841
	public static sbyte versionMap = 1;

	// Token: 0x040012EA RID: 4842
	public static int mapID;

	// Token: 0x040012EB RID: 4843
	public static int lastBgID = -1;

	// Token: 0x040012EC RID: 4844
	public static int zoneID;

	// Token: 0x040012ED RID: 4845
	public static int bgID;

	// Token: 0x040012EE RID: 4846
	public static int bgType;

	// Token: 0x040012EF RID: 4847
	public static int lastType = -1;

	// Token: 0x040012F0 RID: 4848
	public static int typeMap;

	// Token: 0x040012F1 RID: 4849
	public static sbyte planetID;

	// Token: 0x040012F2 RID: 4850
	public static sbyte lastPlanetId = -1;

	// Token: 0x040012F3 RID: 4851
	public static long timeTranMini;

	// Token: 0x040012F4 RID: 4852
	public static MyVector vGo = new MyVector();

	// Token: 0x040012F5 RID: 4853
	public static MyVector vItemBg = new MyVector();

	// Token: 0x040012F6 RID: 4854
	public static MyVector vCurrItem = new MyVector();

	// Token: 0x040012F7 RID: 4855
	public static string[] mapNames;

	// Token: 0x040012F8 RID: 4856
	public static sbyte MAP_NORMAL = 0;

	// Token: 0x040012F9 RID: 4857
	public static Image bong;

	// Token: 0x040012FA RID: 4858
	public const int TRAIDAT_DOINUI = 0;

	// Token: 0x040012FB RID: 4859
	public const int TRAIDAT_RUNG = 1;

	// Token: 0x040012FC RID: 4860
	public const int TRAIDAT_DAORUA = 2;

	// Token: 0x040012FD RID: 4861
	public const int TRAIDAT_DADO = 3;

	// Token: 0x040012FE RID: 4862
	public const int NAMEK_THUNGLUNG = 5;

	// Token: 0x040012FF RID: 4863
	public const int NAMEK_DOINUI = 4;

	// Token: 0x04001300 RID: 4864
	public const int NAMEK_RUNG = 6;

	// Token: 0x04001301 RID: 4865
	public const int NAMEK_DAO = 7;

	// Token: 0x04001302 RID: 4866
	public const int SAYAI_DOINUI = 8;

	// Token: 0x04001303 RID: 4867
	public const int SAYAI_RUNG = 9;

	// Token: 0x04001304 RID: 4868
	public const int SAYAI_CITY = 10;

	// Token: 0x04001305 RID: 4869
	public const int SAYAI_NIGHT = 11;

	// Token: 0x04001306 RID: 4870
	public const int KAMISAMA = 12;

	// Token: 0x04001307 RID: 4871
	public const int TIME_ROOM = 13;

	// Token: 0x04001308 RID: 4872
	public const int HELL = 15;

	// Token: 0x04001309 RID: 4873
	public const int BEERUS = 16;

	// Token: 0x0400130A RID: 4874
	public const int THE_HELL = 19;

	// Token: 0x0400130B RID: 4875
	public static Image[] bgItem = new Image[8];

	// Token: 0x0400130C RID: 4876
	public static MyVector vObject = new MyVector();

	// Token: 0x0400130D RID: 4877
	public static int[] offlineId = new int[]
	{
		21,
		22,
		23,
		39,
		40,
		41
	};

	// Token: 0x0400130E RID: 4878
	public static int[] highterId = new int[]
	{
		21,
		22,
		23,
		24,
		25,
		26
	};

	// Token: 0x0400130F RID: 4879
	public static int[] toOfflineId = new int[]
	{
		0,
		7,
		14
	};

	// Token: 0x04001310 RID: 4880
	public static int[][] tileType;

	// Token: 0x04001311 RID: 4881
	public static int[][][] tileIndex;

	// Token: 0x04001312 RID: 4882
	public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

	// Token: 0x04001313 RID: 4883
	public static int sizeMiniMap = 2;

	// Token: 0x04001314 RID: 4884
	public static int gssx;

	// Token: 0x04001315 RID: 4885
	public static int gssxe;

	// Token: 0x04001316 RID: 4886
	public static int gssy;

	// Token: 0x04001317 RID: 4887
	public static int gssye;

	// Token: 0x04001318 RID: 4888
	public static int countx;

	// Token: 0x04001319 RID: 4889
	public static int county;

	// Token: 0x0400131A RID: 4890
	private static int[] colorMini = new int[]
	{
		5257738,
		8807192
	};

	// Token: 0x0400131B RID: 4891
	public static int yWater = 0;
}
