using System;

// Token: 0x020000C9 RID: 201
public class TileMap
{
	// Token: 0x06000A0F RID: 2575 RVA: 0x0009916D File Offset: 0x0009756D
	public static void loadBg()
	{
		TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
		if (mGraphics.zoomLevel == 1 || Main.isIpod || Main.isIphone4)
		{
			return;
		}
		TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000991B0 File Offset: 0x000975B0
	public static bool isVoDaiMap()
	{
		return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0009920E File Offset: 0x0009760E
	public static bool isTrainingMap()
	{
		return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00099237 File Offset: 0x00097637
	public static bool mapPhuBang()
	{
		return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0009925C File Offset: 0x0009765C
	public static BgItem getBIById(int id)
	{
		for (int i = 0; i < TileMap.vItemBg.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
			if (bgItem.id == id)
			{
				return bgItem;
			}
		}
		return null;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x000992A4 File Offset: 0x000976A4
	public static bool isOfflineMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.offlineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x000992E0 File Offset: 0x000976E0
	public static bool isHighterMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.highterId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0009931C File Offset: 0x0009771C
	public static bool isToOfflineMap()
	{
		for (int i = 0; i < TileMap.toOfflineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.toOfflineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00099355 File Offset: 0x00097755
	public static void freeTilemap()
	{
		TileMap.imgTile = null;
		mSystem.gcc();
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00099362 File Offset: 0x00097762
	public static void loadTileCreatChar()
	{
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00099364 File Offset: 0x00097764
	public static bool isExistMoreOne(int id)
	{
		if (id == 156 || id == 330 || id == 345 || id == 334)
		{
			return false;
		}
		if (TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103)
		{
			return false;
		}
		int num = 0;
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.id == id)
			{
				num++;
			}
		}
		return num > 2;
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00099440 File Offset: 0x00097840
	public static void loadTileImage()
	{
		if (TileMap.imgWaterfall == null)
		{
			TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
		}
		if (TileMap.imgTopWaterfall == null)
		{
			TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
		}
		if (TileMap.imgWaterflow == null)
		{
			TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
		}
		if (TileMap.imgWaterlowN == null)
		{
			TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
		}
		if (TileMap.imgWaterlowN2 == null)
		{
			TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
		}
		mSystem.gcc();
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x000994D0 File Offset: 0x000978D0
	public static void setTile(int index, int[] mapsArr, int type)
	{
		for (int i = 0; i < mapsArr.Length; i++)
		{
			if (TileMap.maps[index] == mapsArr[i])
			{
				TileMap.types[index] |= type;
				return;
			}
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00099514 File Offset: 0x00097914
	public static void loadMap(int tileId)
	{
		TileMap.pxh = TileMap.tmh * (int)TileMap.size;
		TileMap.pxw = TileMap.tmw * (int)TileMap.size;
		Res.outz("load tile ID= " + TileMap.tileID);
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

	// Token: 0x06000A1D RID: 2589 RVA: 0x000995E0 File Offset: 0x000979E0
	public static bool isInAirMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0009960C File Offset: 0x00097A0C
	public static bool isDoubleMap()
	{
		return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x000996F8 File Offset: 0x00097AF8
	public static void getTile()
	{
		if (Main.typeClient == 3 || Main.typeClient == 5)
		{
			if (mGraphics.zoomLevel == 1)
			{
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID + ".png");
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
			if (mGraphics.zoomLevel == 1)
			{
				if (TileMap.imgTile != null)
				{
					for (int j = 0; j < TileMap.imgTile.Length; j++)
					{
						if (TileMap.imgTile[j] != null)
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
					if (k < 9)
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
				return;
			}
			Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + "$1.png");
			if (image != null)
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
				image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + ".png");
				if (image != null)
				{
					Rms.DeleteStorage("$");
					TileMap.imgTile = new Image[1];
					TileMap.imgTile[0] = image;
				}
			}
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x000999E8 File Offset: 0x00097DE8
	public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
	{
		if (TileMap.imgTile == null)
		{
			return;
		}
		if (TileMap.imgTile.Length == 1)
		{
			g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
		else
		{
			g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00099A64 File Offset: 0x00097E64
	public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
	{
		if (TileMap.imgTile == null)
		{
			return;
		}
		if (TileMap.imgTile.Length == 1)
		{
			g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
		}
		else
		{
			g.drawImage(TileMap.imgTile[frame], x, y, 0);
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00099AB8 File Offset: 0x00097EB8
	public static void paintTilemapLOW(mGraphics g)
	{
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				int num = TileMap.maps[j * TileMap.tmw + i] - 1;
				if (num != -1)
				{
					TileMap.paintTile(g, num, i, j);
				}
				if ((TileMap.tileTypeAt(i, j) & 32) == 32)
				{
					g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				else if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					Image arg;
					if (TileMap.tileID == 5)
					{
						arg = TileMap.imgWaterlowN;
					}
					else if (TileMap.tileID == 8)
					{
						arg = TileMap.imgWaterlowN2;
					}
					else
					{
						arg = TileMap.imgWaterflow;
					}
					g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				if ((TileMap.tileTypeAt(i, j) & 2048) == 2048)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					TileMap.paintTile(g, TileMap.maps[j * TileMap.tmw + i] - 1, i, j);
				}
			}
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00099CCC File Offset: 0x000980CC
	public static void paintTilemap(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameScr.gI().paintBgItem(g, 1);
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
		}
		for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
		{
			for (int k = GameScr.gssy; k < GameScr.gssye; k++)
			{
				if (j != 0)
				{
					if (j != TileMap.tmw - 1)
					{
						int num = TileMap.maps[k * TileMap.tmw + j] - 1;
						if ((TileMap.tileTypeAt(j, k) & 256) != 256)
						{
							if ((TileMap.tileTypeAt(j, k) & 32) == 32)
							{
								g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if ((TileMap.tileTypeAt(j, k) & 128) == 128)
							{
								g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if (TileMap.tileID != 13 || num == -1)
							{
								if (TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1)
								{
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
								}
								if (TileMap.tileID == 3)
								{
								}
								if ((TileMap.tileTypeAt(j, k) & 16) == 16)
								{
									TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
									TileMap.dbx = TileMap.bx - GameScr.gW2;
									TileMap.dfx = ((int)TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
									TileMap.fx = TileMap.dfx + GameScr.gW2;
									TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
								}
								else if ((TileMap.tileTypeAt(j, k) & 512) == 512)
								{
									if (num != -1)
									{
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
									}
								}
								else if (num != -1)
								{
									TileMap.paintTile(g, num, j, k);
								}
							}
						}
					}
				}
			}
		}
		if (GameScr.cmx < 24)
		{
			for (int l = GameScr.gssy; l < GameScr.gssye; l++)
			{
				int num2 = TileMap.maps[l * TileMap.tmw + 1] - 1;
				if (num2 != -1)
				{
					TileMap.paintTile(g, num2, 0, l);
				}
			}
		}
		if (GameScr.cmx > GameScr.cmxLim)
		{
			int num3 = TileMap.tmw - 2;
			for (int m = GameScr.gssy; m < GameScr.gssye; m++)
			{
				int num4 = TileMap.maps[m * TileMap.tmw + num3] - 1;
				if (num4 != -1)
				{
					TileMap.paintTile(g, num4, num3 + 1, m);
				}
			}
		}
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0009A05C File Offset: 0x0009845C
	public static bool isWaterEff()
	{
		return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0009A0BC File Offset: 0x000984BC
	public static void paintOutTilemap(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		int num = 0;
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				num++;
				if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					Image arg;
					if (TileMap.tileID == 5)
					{
						arg = TileMap.imgWaterlowN;
					}
					else if (TileMap.tileID == 8)
					{
						arg = TileMap.imgWaterlowN2;
					}
					else
					{
						arg = TileMap.imgWaterflow;
					}
					if (!TileMap.isWaterEff())
					{
						g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
						g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
					}
					g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
					if (TileMap.yWater == 0 && TileMap.isWaterEff())
					{
						TileMap.yWater = j * (int)TileMap.size - 12;
						int color = 16777215;
						if (GameCanvas.typeBg == 2)
						{
							color = 10871287;
						}
						else if (GameCanvas.typeBg == 4)
						{
							color = 8111470;
						}
						else if (GameCanvas.typeBg == 7)
						{
							color = 5693125;
						}
						else if (GameCanvas.typeBg == 19)
						{
							color = 16711680;
						}
						BackgroudEffect.addWater(color, TileMap.yWater + 15);
					}
				}
			}
		}
		BackgroudEffect.paintWaterAll(g);
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0009A264 File Offset: 0x00098664
	public static void loadMapFromResource(int mapID)
	{
		DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID);
		TileMap.tmw = (int)((ushort)dataInputStream.read());
		TileMap.tmh = (int)((ushort)dataInputStream.read());
		TileMap.maps = new int[dataInputStream.available()];
		for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
		{
			TileMap.maps[i] = (int)((ushort)dataInputStream.read());
		}
		TileMap.types = new int[TileMap.maps.Length];
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0009A2EC File Offset: 0x000986EC
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

	// Token: 0x06000A28 RID: 2600 RVA: 0x0009A32C File Offset: 0x0009872C
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

	// Token: 0x06000A29 RID: 2601 RVA: 0x0009A36C File Offset: 0x0009876C
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

	// Token: 0x06000A2A RID: 2602 RVA: 0x0009A3BC File Offset: 0x000987BC
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

	// Token: 0x06000A2B RID: 2603 RVA: 0x0009A40C File Offset: 0x0009880C
	public static void setTileTypeAtPixel(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0009A434 File Offset: 0x00098834
	public static void setTileTypeAt(int x, int y, int t)
	{
		TileMap.types[y * TileMap.tmw + x] = t;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0009A446 File Offset: 0x00098846
	public static void killTileTypeAt(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0009A46F File Offset: 0x0009886F
	public static int tileYofPixel(int py)
	{
		return py / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0009A480 File Offset: 0x00098880
	public static int tileXofPixel(int px)
	{
		return px / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0009A491 File Offset: 0x00098891
	public static void loadMainTile()
	{
		if (TileMap.lastTileID != TileMap.tileID)
		{
			TileMap.getTile();
			TileMap.lastTileID = TileMap.tileID;
		}
	}

	// Token: 0x040012A7 RID: 4775
	public const int T_EMPTY = 0;

	// Token: 0x040012A8 RID: 4776
	public const int T_TOP = 2;

	// Token: 0x040012A9 RID: 4777
	public const int T_LEFT = 4;

	// Token: 0x040012AA RID: 4778
	public const int T_RIGHT = 8;

	// Token: 0x040012AB RID: 4779
	public const int T_TREE = 16;

	// Token: 0x040012AC RID: 4780
	public const int T_WATERFALL = 32;

	// Token: 0x040012AD RID: 4781
	public const int T_WATERFLOW = 64;

	// Token: 0x040012AE RID: 4782
	public const int T_TOPFALL = 128;

	// Token: 0x040012AF RID: 4783
	public const int T_OUTSIDE = 256;

	// Token: 0x040012B0 RID: 4784
	public const int T_DOWN1PIXEL = 512;

	// Token: 0x040012B1 RID: 4785
	public const int T_BRIDGE = 1024;

	// Token: 0x040012B2 RID: 4786
	public const int T_UNDERWATER = 2048;

	// Token: 0x040012B3 RID: 4787
	public const int T_SOLIDGROUND = 4096;

	// Token: 0x040012B4 RID: 4788
	public const int T_BOTTOM = 8192;

	// Token: 0x040012B5 RID: 4789
	public const int T_DIE = 16384;

	// Token: 0x040012B6 RID: 4790
	public const int T_HEBI = 32768;

	// Token: 0x040012B7 RID: 4791
	public const int T_BANG = 65536;

	// Token: 0x040012B8 RID: 4792
	public const int T_JUM8 = 131072;

	// Token: 0x040012B9 RID: 4793
	public const int T_NT0 = 262144;

	// Token: 0x040012BA RID: 4794
	public const int T_NT1 = 524288;

	// Token: 0x040012BB RID: 4795
	public const int T_CENTER = 1;

	// Token: 0x040012BC RID: 4796
	public static int tmw;

	// Token: 0x040012BD RID: 4797
	public static int tmh;

	// Token: 0x040012BE RID: 4798
	public static int pxw;

	// Token: 0x040012BF RID: 4799
	public static int pxh;

	// Token: 0x040012C0 RID: 4800
	public static int tileID;

	// Token: 0x040012C1 RID: 4801
	public static int lastTileID = -1;

	// Token: 0x040012C2 RID: 4802
	public static int[] maps;

	// Token: 0x040012C3 RID: 4803
	public static int[] types;

	// Token: 0x040012C4 RID: 4804
	public static Image[] imgTile;

	// Token: 0x040012C5 RID: 4805
	public static Image imgTileSmall;

	// Token: 0x040012C6 RID: 4806
	public static Image imgMiniMap;

	// Token: 0x040012C7 RID: 4807
	public static Image imgWaterfall;

	// Token: 0x040012C8 RID: 4808
	public static Image imgTopWaterfall;

	// Token: 0x040012C9 RID: 4809
	public static Image imgWaterflow;

	// Token: 0x040012CA RID: 4810
	public static Image imgWaterlowN;

	// Token: 0x040012CB RID: 4811
	public static Image imgWaterlowN2;

	// Token: 0x040012CC RID: 4812
	public static Image imgWaterF;

	// Token: 0x040012CD RID: 4813
	public static Image imgLeaf;

	// Token: 0x040012CE RID: 4814
	public static sbyte size = 24;

	// Token: 0x040012CF RID: 4815
	private static int bx;

	// Token: 0x040012D0 RID: 4816
	private static int dbx;

	// Token: 0x040012D1 RID: 4817
	private static int fx;

	// Token: 0x040012D2 RID: 4818
	private static int dfx;

	// Token: 0x040012D3 RID: 4819
	public static string[] instruction;

	// Token: 0x040012D4 RID: 4820
	public static int[] iX;

	// Token: 0x040012D5 RID: 4821
	public static int[] iY;

	// Token: 0x040012D6 RID: 4822
	public static int[] iW;

	// Token: 0x040012D7 RID: 4823
	public static int iCount;

	// Token: 0x040012D8 RID: 4824
	public static bool isMapDouble = false;

	// Token: 0x040012D9 RID: 4825
	public static string mapName = string.Empty;

	// Token: 0x040012DA RID: 4826
	public static sbyte versionMap = 1;

	// Token: 0x040012DB RID: 4827
	public static int mapID;

	// Token: 0x040012DC RID: 4828
	public static int lastBgID = -1;

	// Token: 0x040012DD RID: 4829
	public static int zoneID;

	// Token: 0x040012DE RID: 4830
	public static int bgID;

	// Token: 0x040012DF RID: 4831
	public static int bgType;

	// Token: 0x040012E0 RID: 4832
	public static int lastType = -1;

	// Token: 0x040012E1 RID: 4833
	public static int typeMap;

	// Token: 0x040012E2 RID: 4834
	public static sbyte planetID;

	// Token: 0x040012E3 RID: 4835
	public static sbyte lastPlanetId = -1;

	// Token: 0x040012E4 RID: 4836
	public static long timeTranMini;

	// Token: 0x040012E5 RID: 4837
	public static MyVector vGo = new MyVector();

	// Token: 0x040012E6 RID: 4838
	public static MyVector vItemBg = new MyVector();

	// Token: 0x040012E7 RID: 4839
	public static MyVector vCurrItem = new MyVector();

	// Token: 0x040012E8 RID: 4840
	public static string[] mapNames;

	// Token: 0x040012E9 RID: 4841
	public static sbyte MAP_NORMAL = 0;

	// Token: 0x040012EA RID: 4842
	public static Image bong;

	// Token: 0x040012EB RID: 4843
	public const int TRAIDAT_DOINUI = 0;

	// Token: 0x040012EC RID: 4844
	public const int TRAIDAT_RUNG = 1;

	// Token: 0x040012ED RID: 4845
	public const int TRAIDAT_DAORUA = 2;

	// Token: 0x040012EE RID: 4846
	public const int TRAIDAT_DADO = 3;

	// Token: 0x040012EF RID: 4847
	public const int NAMEK_THUNGLUNG = 5;

	// Token: 0x040012F0 RID: 4848
	public const int NAMEK_DOINUI = 4;

	// Token: 0x040012F1 RID: 4849
	public const int NAMEK_RUNG = 6;

	// Token: 0x040012F2 RID: 4850
	public const int NAMEK_DAO = 7;

	// Token: 0x040012F3 RID: 4851
	public const int SAYAI_DOINUI = 8;

	// Token: 0x040012F4 RID: 4852
	public const int SAYAI_RUNG = 9;

	// Token: 0x040012F5 RID: 4853
	public const int SAYAI_CITY = 10;

	// Token: 0x040012F6 RID: 4854
	public const int SAYAI_NIGHT = 11;

	// Token: 0x040012F7 RID: 4855
	public const int KAMISAMA = 12;

	// Token: 0x040012F8 RID: 4856
	public const int TIME_ROOM = 13;

	// Token: 0x040012F9 RID: 4857
	public const int HELL = 15;

	// Token: 0x040012FA RID: 4858
	public const int BEERUS = 16;

	// Token: 0x040012FB RID: 4859
	public const int THE_HELL = 19;

	// Token: 0x040012FC RID: 4860
	public static Image[] bgItem = new Image[8];

	// Token: 0x040012FD RID: 4861
	public static MyVector vObject = new MyVector();

	// Token: 0x040012FE RID: 4862
	public static int[] offlineId = new int[]
	{
		21,
		22,
		23,
		39,
		40,
		41
	};

	// Token: 0x040012FF RID: 4863
	public static int[] highterId = new int[]
	{
		21,
		22,
		23,
		24,
		25,
		26
	};

	// Token: 0x04001300 RID: 4864
	public static int[] toOfflineId = new int[]
	{
		0,
		7,
		14
	};

	// Token: 0x04001301 RID: 4865
	public static int[][] tileType;

	// Token: 0x04001302 RID: 4866
	public static int[][][] tileIndex;

	// Token: 0x04001303 RID: 4867
	public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

	// Token: 0x04001304 RID: 4868
	public static int sizeMiniMap = 2;

	// Token: 0x04001305 RID: 4869
	public static int gssx;

	// Token: 0x04001306 RID: 4870
	public static int gssxe;

	// Token: 0x04001307 RID: 4871
	public static int gssy;

	// Token: 0x04001308 RID: 4872
	public static int gssye;

	// Token: 0x04001309 RID: 4873
	public static int countx;

	// Token: 0x0400130A RID: 4874
	public static int county;

	// Token: 0x0400130B RID: 4875
	private static int[] colorMini = new int[]
	{
		5257738,
		8807192
	};

	// Token: 0x0400130C RID: 4876
	public static int yWater = 0;
}
