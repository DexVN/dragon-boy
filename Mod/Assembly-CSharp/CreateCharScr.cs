using System;

// Token: 0x0200001E RID: 30
public class CreateCharScr : mScreen, IActionListener
{
	// Token: 0x060001C4 RID: 452 RVA: 0x00032BB0 File Offset: 0x00030DB0
	public CreateCharScr()
	{
		try
		{
			bool flag = !GameCanvas.lowGraphic;
			if (flag)
			{
				CreateCharScr.loadMapFromResource(new sbyte[]
				{
					39,
					40,
					41
				});
			}
			this.loadMapTableFromResource(new sbyte[]
			{
				39,
				40,
				41
			});
		}
		catch (Exception ex)
		{
			Cout.LogError("Tao char loi " + ex.ToString());
		}
		bool flag2 = GameCanvas.w <= 200;
		if (flag2)
		{
			GameScr.setPopupSize(128, 100);
			GameScr.popupX = (GameCanvas.w - 128) / 2;
			GameScr.popupY = 10;
			this.cy += 15;
			this.dy -= 15;
		}
		CreateCharScr.indexGender = 1;
		CreateCharScr.tAddName = new TField();
		CreateCharScr.tAddName.width = GameCanvas.loginScr.tfUser.width;
		bool flag3 = GameCanvas.w < 200;
		if (flag3)
		{
			CreateCharScr.tAddName.width = 60;
		}
		CreateCharScr.tAddName.height = mScreen.ITEM_HEIGHT + 2;
		bool flag4 = GameCanvas.w < 200;
		if (flag4)
		{
			CreateCharScr.tAddName.x = GameScr.popupX + 45;
			CreateCharScr.tAddName.y = GameScr.popupY + 12;
		}
		else
		{
			CreateCharScr.tAddName.x = GameCanvas.w / 2 - CreateCharScr.tAddName.width / 2;
			CreateCharScr.tAddName.y = 35;
		}
		bool flag5 = !GameCanvas.isTouch;
		if (flag5)
		{
			CreateCharScr.tAddName.isFocus = true;
		}
		CreateCharScr.tAddName.setIputType(TField.INPUT_TYPE_ANY);
		CreateCharScr.tAddName.showSubTextField = false;
		CreateCharScr.tAddName.strInfo = mResources.char_name;
		bool flag6 = CreateCharScr.tAddName.getText().Equals("@");
		if (flag6)
		{
			CreateCharScr.tAddName.setText(GameCanvas.loginScr.tfUser.getText().Substring(0, GameCanvas.loginScr.tfUser.getText().IndexOf("@")));
		}
		CreateCharScr.tAddName.name = mResources.char_name;
		CreateCharScr.indexGender = 1;
		CreateCharScr.indexHair = 0;
		this.center = new Command(mResources.NEWCHAR, this, 8000, null);
		this.left = new Command(mResources.BACK, this, 8001, null);
		bool flag7 = !GameCanvas.isTouch;
		if (flag7)
		{
			this.right = CreateCharScr.tAddName.cmdClear;
		}
		this.yBegin = CreateCharScr.tAddName.y;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00032E90 File Offset: 0x00031090
	public static CreateCharScr gI()
	{
		bool flag = CreateCharScr.instance == null;
		if (flag)
		{
			CreateCharScr.instance = new CreateCharScr();
		}
		return CreateCharScr.instance;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00003136 File Offset: 0x00001336
	public static void init()
	{
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00032EC0 File Offset: 0x000310C0
	public static void loadMapFromResource(sbyte[] mapID)
	{
		Res.outz("newwwwwwwwww =============");
		for (int i = 0; i < mapID.Length; i++)
		{
			DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID[i].ToString());
			MapTemplate.tmw[i] = (int)((ushort)dataInputStream.read());
			MapTemplate.tmh[i] = (int)((ushort)dataInputStream.read());
			Cout.LogError(string.Concat(new object[]
			{
				"Thong TIn : ",
				MapTemplate.tmw[i],
				"::",
				MapTemplate.tmh[i]
			}));
			MapTemplate.maps[i] = new int[dataInputStream.available()];
			Cout.LogError("lent= " + MapTemplate.maps[i].Length.ToString());
			for (int j = 0; j < MapTemplate.tmw[i] * MapTemplate.tmh[i]; j++)
			{
				MapTemplate.maps[i][j] = dataInputStream.read();
			}
			MapTemplate.types[i] = new int[MapTemplate.maps[i].Length];
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00032FE8 File Offset: 0x000311E8
	public void loadMapTableFromResource(sbyte[] mapID)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			DataInputStream dataInputStream = null;
			try
			{
				for (int i = 0; i < mapID.Length; i++)
				{
					dataInputStream = MyStream.readFile("/mymap/mapTable" + mapID[i].ToString());
					Cout.LogError("mapTable : " + mapID[i].ToString());
					short num = dataInputStream.readShort();
					MapTemplate.vCurrItem[i] = new MyVector();
					Res.outz("nItem= " + num.ToString());
					for (int j = 0; j < (int)num; j++)
					{
						short id = dataInputStream.readShort();
						short num2 = dataInputStream.readShort();
						short num3 = dataInputStream.readShort();
						bool flag = TileMap.getBIById((int)id) != null;
						if (flag)
						{
							BgItem bibyId = TileMap.getBIById((int)id);
							BgItem bgItem = new BgItem();
							bgItem.id = (int)id;
							bgItem.idImage = bibyId.idImage;
							bgItem.dx = bibyId.dx;
							bgItem.dy = bibyId.dy;
							bgItem.x = (int)(num2 * (short)TileMap.size);
							bgItem.y = (int)(num3 * (short)TileMap.size);
							bgItem.layer = bibyId.layer;
							MapTemplate.vCurrItem[i].addElement(bgItem);
							bool flag2 = !BgItem.imgNew.containsKey(bgItem.idImage.ToString() + string.Empty);
							if (flag2)
							{
								try
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									bool flag3 = image == null;
									if (flag3)
									{
										BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									else
									{
										BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
									}
								}
								catch (Exception ex)
								{
									Image image2 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									bool flag4 = image2 == null;
									if (flag4)
									{
										image2 = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image2);
								}
								BgItem.vKeysLast.addElement(bgItem.idImage.ToString() + string.Empty);
							}
							bool flag5 = !BgItem.isExistKeyNews(bgItem.idImage.ToString() + string.Empty);
							if (flag5)
							{
								BgItem.vKeysNew.addElement(bgItem.idImage.ToString() + string.Empty);
							}
							bgItem.changeColor();
						}
						else
						{
							Res.outz("item null");
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Cout.println("LOI TAI loadMapTableFromResource" + ex2.ToString());
			}
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00033368 File Offset: 0x00031568
	public override void switchToMe()
	{
		LoginScr.isContinueToLogin = false;
		GameCanvas.menu.showMenu = false;
		GameCanvas.endDlg();
		base.switchToMe();
		CreateCharScr.indexGender = Res.random(0, 3);
		CreateCharScr.indexHair = Res.random(0, 3);
		this.doChangeMap();
		global::Char.isLoadingMap = false;
		CreateCharScr.tAddName.setFocusWithKb(true);
		ServerListScreen.countDieConnect = 0;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x000333CC File Offset: 0x000315CC
	public void doChangeMap()
	{
		TileMap.maps = new int[MapTemplate.maps[CreateCharScr.indexGender].Length];
		for (int i = 0; i < MapTemplate.maps[CreateCharScr.indexGender].Length; i++)
		{
			TileMap.maps[i] = MapTemplate.maps[CreateCharScr.indexGender][i];
		}
		TileMap.types = MapTemplate.types[CreateCharScr.indexGender];
		TileMap.pxh = MapTemplate.pxh[CreateCharScr.indexGender];
		TileMap.pxw = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tileID = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tmw = MapTemplate.tmw[CreateCharScr.indexGender];
		TileMap.tmh = MapTemplate.tmh[CreateCharScr.indexGender];
		TileMap.tileID = this.bgID[CreateCharScr.indexGender] + 1;
		TileMap.loadMainTile();
		TileMap.loadTileCreatChar();
		GameCanvas.loadBG(this.bgID[CreateCharScr.indexGender]);
		GameScr.loadCamera(false, this.cx, this.cy);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x000334C7 File Offset: 0x000316C7
	public override void keyPress(int keyCode)
	{
		CreateCharScr.tAddName.keyPressed(keyCode);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x000334D8 File Offset: 0x000316D8
	public override void update()
	{
		this.cp1++;
		bool flag = this.cp1 > 30;
		if (flag)
		{
			this.cp1 = 0;
		}
		bool flag2 = this.cp1 % 15 < 5;
		if (flag2)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		CreateCharScr.tAddName.update();
		bool flag3 = CreateCharScr.selected != 0;
		if (flag3)
		{
			CreateCharScr.tAddName.isFocus = false;
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00033554 File Offset: 0x00031754
	public override void updateKey()
	{
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			CreateCharScr.selected--;
			bool flag2 = CreateCharScr.selected < 0;
			if (flag2)
			{
				CreateCharScr.selected = mResources.MENUNEWCHAR.Length - 1;
			}
		}
		bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
		if (flag3)
		{
			CreateCharScr.selected++;
			bool flag4 = CreateCharScr.selected >= mResources.MENUNEWCHAR.Length;
			if (flag4)
			{
				CreateCharScr.selected = 0;
			}
		}
		bool flag5 = CreateCharScr.selected == 0;
		if (flag5)
		{
			bool flag6 = !GameCanvas.isTouch;
			if (flag6)
			{
				this.right = CreateCharScr.tAddName.cmdClear;
			}
			CreateCharScr.tAddName.update();
		}
		bool flag7 = CreateCharScr.selected == 1;
		if (flag7)
		{
			bool flag8 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag8)
			{
				CreateCharScr.indexGender--;
				bool flag9 = CreateCharScr.indexGender < 0;
				if (flag9)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				this.doChangeMap();
			}
			bool flag10 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
			if (flag10)
			{
				CreateCharScr.indexGender++;
				bool flag11 = CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1;
				if (flag11)
				{
					CreateCharScr.indexGender = 0;
				}
				this.doChangeMap();
			}
			this.right = null;
		}
		bool flag12 = CreateCharScr.selected == 2;
		if (flag12)
		{
			bool flag13 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag13)
			{
				CreateCharScr.indexHair--;
				bool flag14 = CreateCharScr.indexHair < 0;
				if (flag14)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
			}
			bool flag15 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
			if (flag15)
			{
				CreateCharScr.indexHair++;
				bool flag16 = CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1;
				if (flag16)
				{
					CreateCharScr.indexHair = 0;
				}
			}
			this.right = null;
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			int num = 110;
			int num2 = 60;
			int num3 = 78;
			bool flag17 = GameCanvas.w > GameCanvas.h;
			if (flag17)
			{
				num = 100;
				num2 = 40;
			}
			bool flag18 = GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, 15, num3 * 3, 80);
			if (flag18)
			{
				CreateCharScr.selected = 0;
				CreateCharScr.tAddName.isFocus = true;
			}
			bool flag19 = GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30, num3 * 3, num2 + 5);
			if (flag19)
			{
				CreateCharScr.selected = 1;
				int num4 = CreateCharScr.indexGender;
				CreateCharScr.indexGender = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				bool flag20 = CreateCharScr.indexGender < 0;
				if (flag20)
				{
					CreateCharScr.indexGender = 0;
				}
				bool flag21 = CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1;
				if (flag21)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				bool flag22 = num4 != CreateCharScr.indexGender;
				if (flag22)
				{
					this.doChangeMap();
				}
			}
			bool flag23 = GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30 + num2 + 5, num3 * 3, 65);
			if (flag23)
			{
				CreateCharScr.selected = 2;
				int num5 = CreateCharScr.indexHair;
				CreateCharScr.indexHair = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				bool flag24 = CreateCharScr.indexHair < 0;
				if (flag24)
				{
					CreateCharScr.indexHair = 0;
				}
				bool flag25 = CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1;
				if (flag25)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
				bool flag26 = num5 != CreateCharScr.selected;
				if (flag26)
				{
					this.doChangeMap();
				}
			}
		}
		bool flag27 = !TouchScreenKeyboard.visible;
		if (flag27)
		{
			base.updateKey();
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00033960 File Offset: 0x00031B60
	public override void paint(mGraphics g)
	{
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			GameCanvas.paintBGGameScr(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			bool flag = !GameCanvas.lowGraphic;
			if (flag)
			{
				for (int i = 0; i < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); i++)
				{
					BgItem bgItem = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(i);
					bool flag2 = bgItem.idImage != -1 && bgItem.layer == 1;
					if (flag2)
					{
						bgItem.paint(g);
					}
				}
			}
			bool flag3 = mSystem.clientType == 5;
			if (flag3)
			{
				GameCanvas.paint_ios_bg(g);
			}
			else
			{
				TileMap.paintTilemap(g);
			}
			int num = 30;
			bool flag4 = GameCanvas.w == 128;
			if (flag4)
			{
				num = 20;
			}
			int num2 = CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair];
			int num3 = CreateCharScr.defaultLeg[CreateCharScr.indexGender];
			int num4 = CreateCharScr.defaultBody[CreateCharScr.indexGender];
			g.drawImage(TileMap.bong, this.cx, this.cy + this.dy, 3);
			Part part = GameScr.parts[num2];
			Part part2 = GameScr.parts[num3];
			Part part3 = GameScr.parts[num4];
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy + this.dy, 0, 0);
			bool flag5 = !GameCanvas.lowGraphic;
			if (flag5)
			{
				for (int j = 0; j < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); j++)
				{
					BgItem bgItem2 = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(j);
					bool flag6 = bgItem2.idImage != -1 && bgItem2.layer == 3;
					if (flag6)
					{
						bgItem2.paint(g);
					}
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			bool flag7 = GameCanvas.w < 200;
			if (flag7)
			{
				GameCanvas.paintz.paintFrame(GameScr.popupX, GameScr.popupY, GameScr.popupW, GameScr.popupH, g);
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + this.dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy + this.dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy, 0, 0);
				for (int k = 0; k < mResources.MENUNEWCHAR.Length; k++)
				{
					bool flag8 = CreateCharScr.selected == k;
					if (flag8)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, GameScr.popupX + 10 + ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, GameScr.popupX + GameScr.popupW - 10 - ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
					}
					mFont.tahoma_7b_dark.drawString(g, mResources.MENUNEWCHAR[k], GameScr.popupX + 20, GameScr.popupY + 30 + k * num, 0);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[CreateCharScr.indexGender], GameScr.popupX + 70, GameScr.popupY + 30 + num, mFont.LEFT);
				mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][CreateCharScr.indexHair], GameScr.popupX + 55, GameScr.popupY + 30 + 2 * num, mFont.LEFT);
				CreateCharScr.tAddName.paint(g);
			}
			else
			{
				bool flag9 = !Main.isPC;
				if (flag9)
				{
					bool flag10 = mGraphics.addYWhenOpenKeyBoard != 0;
					if (flag10)
					{
						this.yButton = 110;
						this.disY = 60;
						bool flag11 = GameCanvas.w > GameCanvas.h;
						if (flag11)
						{
							this.yButton = GameScr.popupY + 30 + 3 * num + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy - 15;
							this.disY = 35;
						}
					}
					else
					{
						this.yButton = 110;
						this.disY = 60;
						bool flag12 = GameCanvas.w > GameCanvas.h;
						if (flag12)
						{
							this.yButton = 100;
							this.disY = 45;
						}
					}
					CreateCharScr.tAddName.y = this.yButton - CreateCharScr.tAddName.height - this.disY + 5;
				}
				else
				{
					this.yButton = 110;
					this.disY = 60;
					bool flag13 = GameCanvas.w > GameCanvas.h;
					if (flag13)
					{
						this.yButton = 100;
						this.disY = 45;
					}
					CreateCharScr.tAddName.y = this.yBegin;
				}
				for (int l = 0; l < 3; l++)
				{
					int num5 = 78;
					bool flag14 = l != CreateCharScr.indexGender;
					if (flag14)
					{
						g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
					}
					else
					{
						bool flag15 = CreateCharScr.selected == 1;
						if (flag15)
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num5 + l * num5, this.yButton - 20 + ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), StaticObj.VCENTER_HCENTER);
						}
						g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
					}
					mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[l], GameCanvas.w / 2 - num5 + l * num5, this.yButton - 5, mFont.CENTER);
				}
				for (int m = 0; m < 3; m++)
				{
					int num6 = 78;
					bool flag16 = m != CreateCharScr.indexHair;
					if (flag16)
					{
						g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
					}
					else
					{
						bool flag17 = CreateCharScr.selected == 2;
						if (flag17)
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 20 + ((GameCanvas.gameTick % 7 <= 3) ? 0 : 1), StaticObj.VCENTER_HCENTER);
						}
						g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
					}
					mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][m], GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 5, mFont.CENTER);
				}
				CreateCharScr.tAddName.paint(g);
			}
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			mFont.tahoma_7b_white.drawString(g, mResources.server + " " + LoginScr.serverName, 5, 5, 0, mFont.tahoma_7b_dark);
			bool flag18 = !TouchScreenKeyboard.visible;
			if (flag18)
			{
				base.paint(g);
			}
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x000343D0 File Offset: 0x000325D0
	public void perform(int idAction, object p)
	{
		bool flag = idAction != 8000;
		if (flag)
		{
			bool flag2 = idAction != 8001;
			if (flag2)
			{
				bool flag3 = idAction != 10019;
				if (flag3)
				{
					bool flag4 = idAction == 10020;
					if (flag4)
					{
						GameCanvas.endDlg();
					}
				}
				else
				{
					Session_ME.gI().close();
					GameCanvas.endDlg();
					GameCanvas.serverScreen.switchToMe();
				}
			}
			else
			{
				bool isLogin = GameCanvas.loginScr.isLogin2;
				if (isLogin)
				{
					GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, this, 10019, null), new Command(mResources.NO, this, 10020, null));
				}
				else
				{
					bool isWindowsPhone = Main.isWindowsPhone;
					if (isWindowsPhone)
					{
						GameMidlet.isBackWindowsPhone = true;
					}
					Session_ME.gI().close();
					GameCanvas.serverScreen.switchToMe();
				}
			}
		}
		else
		{
			bool flag5 = CreateCharScr.tAddName.getText().Equals(string.Empty);
			if (flag5)
			{
				GameCanvas.startOKDlg(mResources.char_name_blank);
			}
			else
			{
				bool flag6 = CreateCharScr.tAddName.getText().Length < 5;
				if (flag6)
				{
					GameCanvas.startOKDlg(mResources.char_name_short);
				}
				else
				{
					bool flag7 = CreateCharScr.tAddName.getText().Length > 15;
					if (flag7)
					{
						GameCanvas.startOKDlg(mResources.char_name_long);
					}
					else
					{
						InfoDlg.showWait();
						Service.gI().createChar(CreateCharScr.tAddName.getText(), CreateCharScr.indexGender, CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair]);
					}
				}
			}
		}
	}

	// Token: 0x0400048C RID: 1164
	public static CreateCharScr instance;

	// Token: 0x0400048D RID: 1165
	private PopUp p;

	// Token: 0x0400048E RID: 1166
	public static bool isCreateChar = false;

	// Token: 0x0400048F RID: 1167
	public static TField tAddName;

	// Token: 0x04000490 RID: 1168
	public static int indexGender;

	// Token: 0x04000491 RID: 1169
	public static int indexHair;

	// Token: 0x04000492 RID: 1170
	public static int selected;

	// Token: 0x04000493 RID: 1171
	public static int[][] hairID = new int[][]
	{
		new int[]
		{
			64,
			30,
			31
		},
		new int[]
		{
			9,
			29,
			32
		},
		new int[]
		{
			6,
			27,
			28
		}
	};

	// Token: 0x04000494 RID: 1172
	public static int[] defaultLeg = new int[]
	{
		2,
		13,
		8
	};

	// Token: 0x04000495 RID: 1173
	public static int[] defaultBody = new int[]
	{
		1,
		12,
		7
	};

	// Token: 0x04000496 RID: 1174
	private int yButton;

	// Token: 0x04000497 RID: 1175
	private int disY;

	// Token: 0x04000498 RID: 1176
	private int[] bgID = new int[]
	{
		0,
		4,
		8
	};

	// Token: 0x04000499 RID: 1177
	public int yBegin;

	// Token: 0x0400049A RID: 1178
	private int curIndex;

	// Token: 0x0400049B RID: 1179
	private int cx = 168;

	// Token: 0x0400049C RID: 1180
	private int cy = 350;

	// Token: 0x0400049D RID: 1181
	private int dy = 45;

	// Token: 0x0400049E RID: 1182
	private int cp1;

	// Token: 0x0400049F RID: 1183
	private int cf;
}
