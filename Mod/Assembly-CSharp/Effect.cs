using System;

// Token: 0x02000024 RID: 36
public class Effect
{
	// Token: 0x06000202 RID: 514 RVA: 0x00034FD8 File Offset: 0x000331D8
	public Effect()
	{
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0003506C File Offset: 0x0003326C
	public Effect(int id, global::Char c, int layer, int loop, int loopCount, sbyte isStand)
	{
		this.c = c;
		this.effId = id;
		this.layer = layer;
		this.loop = loop;
		this.tLoop = loopCount;
		this.isStand = (int)isStand;
		bool flag = Effect.getEffDataById(id) == null;
		if (flag)
		{
			EffectData effectData = new EffectData();
			effectData.ID = id;
			bool flag2 = id >= 42 && id <= 46;
			if (flag2)
			{
				id = 106;
			}
			string text = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				id,
				"/data"
			});
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag3 = dataInputStream != null;
			if (flag3)
			{
				bool flag4 = id > 100 && id < 200;
				if (flag4)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id.ToString() + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			Effect.addEffData(effectData);
		}
		this.indexFrom = -1;
		this.indexTo = -1;
		this.trans = -1;
		this.typeEff = 4;
		bool flag5 = id == 78;
		if (flag5)
		{
			this.typeEff = 5;
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0003524C File Offset: 0x0003344C
	public Effect(int id, int x, int y, int layer, int loop, int loopCount)
	{
		this.x = x;
		this.y = y;
		this.effId = id;
		this.layer = layer;
		this.loop = loop;
		this.tLoop = loopCount;
		bool flag = Effect.getEffDataById(id) == null;
		if (flag)
		{
			EffectData effectData = new EffectData();
			effectData.ID = id;
			bool flag2 = id >= 42 && id <= 46;
			if (flag2)
			{
				id = 106;
			}
			string text = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				id,
				"/data"
			});
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag3 = dataInputStream != null;
			if (flag3)
			{
				bool flag4 = id > 100 && id < 200;
				if (flag4)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id.ToString() + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			Effect.addEffData(effectData);
			bool flag5 = Effect.lastEff.size() > 20;
			if (flag5)
			{
				Effect.removeEffData(int.Parse((string)Effect.lastEff.elementAt(0)));
				Effect.lastEff.removeElementAt(0);
			}
			Effect.lastEff.addElement(this.effId.ToString() + string.Empty);
		}
		this.indexFrom = -1;
		this.indexTo = -1;
		bool flag6 = id == 78;
		if (flag6)
		{
			this.typeEff = 5;
		}
		else
		{
			this.typeEff = 1;
		}
		bool flag7 = !Effect.isExistNewEff(this.effId.ToString() + string.Empty);
		if (flag7)
		{
			Effect.newEff.addElement(this.effId.ToString() + string.Empty);
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x000354CC File Offset: 0x000336CC
	public static void removeEffData(int id)
	{
		for (int i = 0; i < Effect.vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
			bool flag = effectData.ID == id;
			if (flag)
			{
				Effect.vEffData.removeElement(effectData);
				break;
			}
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00035524 File Offset: 0x00033724
	public static void addEffData(EffectData eff)
	{
		Effect.vEffData.addElement(eff);
		bool flag = TileMap.mapID == 130;
		if (!flag)
		{
			bool flag2 = Effect.vEffData.size() > 10;
			if (flag2)
			{
				for (int i = 0; i < 5; i++)
				{
					Effect.vEffData.removeElementAt(0);
				}
			}
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00035584 File Offset: 0x00033784
	public static EffectData getEffDataById(int id)
	{
		for (int i = 0; i < Effect.vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
			bool flag = effectData.ID == id;
			if (flag)
			{
				return effectData;
			}
		}
		return null;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x000355D8 File Offset: 0x000337D8
	public static bool isExistNewEff(string id)
	{
		for (int i = 0; i < Effect.newEff.size(); i++)
		{
			string text = (string)Effect.newEff.elementAt(i);
			bool flag = text.Equals(id);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0003562C File Offset: 0x0003382C
	public bool isPaintz()
	{
		return this.isPaint;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00035644 File Offset: 0x00033844
	public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
	{
		bool flag = !this.isPaintz();
		if (!flag)
		{
			bool flag2 = Effect.getEffDataById(this.effId).img != null;
			if (flag2)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x000356B0 File Offset: 0x000338B0
	public void getFrameKhangia()
	{
		bool flag = this.effId == 42;
		if (flag)
		{
			this.currFrame = this.khangia1[this.t];
		}
		bool flag2 = this.effId == 43;
		if (flag2)
		{
			this.currFrame = this.khangia2[this.t];
		}
		bool flag3 = this.effId == 44;
		if (flag3)
		{
			this.currFrame = this.khangia3[this.t];
		}
		bool flag4 = this.effId == 45;
		if (flag4)
		{
			this.currFrame = this.khangia4[this.t];
		}
		bool flag5 = this.effId == 46;
		if (flag5)
		{
			this.currFrame = this.khangia5[this.t];
		}
		this.t++;
		bool flag6 = this.t > this.khangia1.Length - 1;
		if (flag6)
		{
			this.t = 0;
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x000357A0 File Offset: 0x000339A0
	public void paint(mGraphics g)
	{
		bool flag = !this.isPaint;
		if (!flag)
		{
			bool flag2 = Effect.getEffDataById(this.effId) == null;
			if (!flag2)
			{
				bool flag3 = Effect.getEffDataById(this.effId).img != null;
				if (flag3)
				{
					try
					{
						Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x, this.y, this.trans, this.layer);
					}
					catch (Exception ex)
					{
					}
				}
			}
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00035838 File Offset: 0x00033A38
	public void update()
	{
		try
		{
			bool flag = this.effId >= 42 && this.effId <= 46;
			if (flag)
			{
				this.getFrameKhangia();
			}
			else
			{
				bool flag2 = Effect.getEffDataById(this.effId) != null;
				if (flag2)
				{
					bool flag3 = Effect.getEffDataById(this.effId).img != null;
					if (flag3)
					{
						bool flag4 = this.typeEff == 5;
						if (flag4)
						{
							this.data = Effect.getEffDataById(this.effId).get(this.c.statusMe);
						}
						else
						{
							this.data = Effect.getEffDataById(this.effId).get();
						}
						bool flag5 = this.data != null;
						if (flag5)
						{
							bool flag6 = !this.isGetTime;
							if (flag6)
							{
								this.isGetTime = true;
								int num = this.data.Length - 1;
								bool flag7 = num > 0 && this.typeEff != 1;
								if (flag7)
								{
									this.t = Res.random(0, num);
								}
								bool flag8 = this.typeEff == 0;
								if (flag8)
								{
									this.t = Res.random(this.indexFrom, this.indexTo);
								}
							}
							switch (this.typeEff)
							{
							case 0:
							{
								bool flag9 = Res.inRect(this.x - 50, this.y - 50, 100, 100, global::Char.myCharz().cx, global::Char.myCharz().cy) && this.t > this.indexFrom && this.t < this.indexTo;
								if (flag9)
								{
									bool flag10 = this.t < this.indexTo;
									if (flag10)
									{
										this.t = this.indexTo;
									}
									this.isNearPlayer = true;
								}
								bool flag11 = !this.isNearPlayer;
								if (flag11)
								{
									this.t++;
									bool flag12 = this.t == this.indexTo;
									if (flag12)
									{
										this.t = this.indexFrom;
									}
								}
								else
								{
									bool flag13 = this.t < this.data.Length;
									if (flag13)
									{
										this.t++;
									}
								}
								break;
							}
							case 1:
							case 3:
							{
								bool flag14 = this.t < this.data.Length;
								if (flag14)
								{
									this.t++;
								}
								break;
							}
							case 2:
							{
								bool flag15 = this.t < this.data.Length;
								if (flag15)
								{
									this.t++;
								}
								this.tLoopCount++;
								bool flag16 = this.tLoopCount == this.tLoop;
								if (flag16)
								{
									this.tLoopCount = 0;
									this.trans = Res.random(0, 2);
								}
								break;
							}
							case 4:
							{
								this.x = this.c.cx;
								this.y = this.c.cy;
								bool flag17 = this.t < this.data.Length;
								if (flag17)
								{
									this.t++;
								}
								break;
							}
							case 5:
							{
								this.trans = ((this.c.cdir != 1) ? 1 : 0);
								bool flag18 = this.c.cdir == 1;
								if (flag18)
								{
									this.x = this.c.cx - 15;
								}
								else
								{
									this.x = this.c.cx + 15;
								}
								bool flag19 = this.c.isMonkey == 0;
								if (flag19)
								{
									this.y = this.c.cy - 25;
								}
								else
								{
									this.y = this.c.cy - 35;
								}
								bool flag20 = this.t < this.data.Length;
								if (flag20)
								{
									this.t++;
								}
								break;
							}
							}
							bool flag21 = this.t == this.data.Length / 2 && (this.effId == 62 || this.effId == 63 || this.effId == 64 || this.effId == 65);
							if (flag21)
							{
								SoundMn.playSound(this.x, this.y, SoundMn.FIREWORK, SoundMn.volume);
							}
							bool flag22 = this.t <= this.data.Length - 1;
							if (flag22)
							{
								this.currFrame = (int)this.data[this.t];
							}
						}
						bool flag23 = this.t >= this.data.Length - 1;
						if (flag23)
						{
							bool flag24 = this.typeEff == 0 || this.typeEff == 3;
							if (flag24)
							{
								this.isPaint = false;
							}
							bool flag25 = this.tLoop == -1;
							if (flag25)
							{
								EffecMn.vEff.removeElement(this);
							}
							bool flag26 = this.typeEff == 2;
							if (flag26)
							{
								this.t = 0;
							}
							else
							{
								bool flag27 = this.typeEff == 1 && this.loop == 1;
								if (flag27)
								{
									this.isPaint = false;
								}
								bool flag28 = this.typeEff == 4 || this.typeEff == 5;
								if (flag28)
								{
									bool flag29 = this.loop == -1;
									if (flag29)
									{
										this.t = 0;
									}
									else
									{
										this.tLoopCount++;
										bool flag30 = this.tLoopCount == this.tLoop;
										if (flag30)
										{
											this.tLoopCount = 0;
											this.loop--;
											this.t = 0;
											bool flag31 = this.loop == 0;
											if (flag31)
											{
												this.c.removeEffChar(0, this.effId);
											}
										}
									}
								}
								else
								{
									this.isNearPlayer = false;
									bool flag32 = this.loop == -1;
									if (flag32)
									{
										this.tLoopCount++;
										this.t = 0;
										bool flag33 = this.tLoopCount == this.tLoop;
										if (flag33)
										{
											this.tLoopCount = 0;
											bool flag34 = this.tLoop > 1;
											if (flag34)
											{
												this.trans = Res.random(0, 2);
											}
										}
									}
									else
									{
										this.tLoopCount++;
										this.t = 0;
										bool flag35 = this.tLoopCount == this.tLoop;
										if (flag35)
										{
											this.tLoopCount = 0;
											this.loop--;
											bool flag36 = this.loop == 0;
											if (flag36)
											{
												EffecMn.vEff.removeElement(this);
											}
										}
									}
								}
							}
						}
						else
						{
							this.isPaint = true;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			EffecMn.vEff.removeElement(this);
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00035F1C File Offset: 0x0003411C
	public int getnFrame()
	{
		return this.data.Length;
	}

	// Token: 0x040004B7 RID: 1207
	public int effId;

	// Token: 0x040004B8 RID: 1208
	public int typeEff;

	// Token: 0x040004B9 RID: 1209
	public int indexFrom;

	// Token: 0x040004BA RID: 1210
	public int indexTo;

	// Token: 0x040004BB RID: 1211
	public bool isNearPlayer;

	// Token: 0x040004BC RID: 1212
	public const int NEAR_PLAYER = 0;

	// Token: 0x040004BD RID: 1213
	public const int LOOP_NORMAL = 1;

	// Token: 0x040004BE RID: 1214
	public const int LOOP_TRANS = 2;

	// Token: 0x040004BF RID: 1215
	public const int BACKGROUND = 3;

	// Token: 0x040004C0 RID: 1216
	public const int CHAR = 4;

	// Token: 0x040004C1 RID: 1217
	public const int CHAR_PET_EFF = 5;

	// Token: 0x040004C2 RID: 1218
	public const int FIRE_TD = 0;

	// Token: 0x040004C3 RID: 1219
	public const int BIRD = 1;

	// Token: 0x040004C4 RID: 1220
	public const int FIRE_NAMEK = 2;

	// Token: 0x040004C5 RID: 1221
	public const int FIRE_SAYAI = 3;

	// Token: 0x040004C6 RID: 1222
	public const int FROG = 5;

	// Token: 0x040004C7 RID: 1223
	public const int CA = 4;

	// Token: 0x040004C8 RID: 1224
	public const int ECH = 6;

	// Token: 0x040004C9 RID: 1225
	public const int TACKE = 7;

	// Token: 0x040004CA RID: 1226
	public const int RAN = 8;

	// Token: 0x040004CB RID: 1227
	public const int KHI = 9;

	// Token: 0x040004CC RID: 1228
	public const int GACON = 10;

	// Token: 0x040004CD RID: 1229
	public const int DANONG = 11;

	// Token: 0x040004CE RID: 1230
	public const int DANBUOM = 12;

	// Token: 0x040004CF RID: 1231
	public const int QUA = 13;

	// Token: 0x040004D0 RID: 1232
	public const int THIENTHACH = 14;

	// Token: 0x040004D1 RID: 1233
	public const int CAVOI = 15;

	// Token: 0x040004D2 RID: 1234
	public const int NAM = 16;

	// Token: 0x040004D3 RID: 1235
	public const int RONGTHAN = 17;

	// Token: 0x040004D4 RID: 1236
	public const int BUOMBAY = 26;

	// Token: 0x040004D5 RID: 1237
	public const int KHUCGO = 27;

	// Token: 0x040004D6 RID: 1238
	public const int DOIBAY = 28;

	// Token: 0x040004D7 RID: 1239
	public const int CONMEO = 29;

	// Token: 0x040004D8 RID: 1240
	public const int LUATAT = 30;

	// Token: 0x040004D9 RID: 1241
	public const int ONGCONG = 31;

	// Token: 0x040004DA RID: 1242
	public const int KHANGIA1 = 42;

	// Token: 0x040004DB RID: 1243
	public const int KHANGIA2 = 43;

	// Token: 0x040004DC RID: 1244
	public const int KHANGIA3 = 44;

	// Token: 0x040004DD RID: 1245
	public const int KHANGIA4 = 45;

	// Token: 0x040004DE RID: 1246
	public const int KHANGIA5 = 46;

	// Token: 0x040004DF RID: 1247
	public global::Char c;

	// Token: 0x040004E0 RID: 1248
	public int t;

	// Token: 0x040004E1 RID: 1249
	public int currFrame;

	// Token: 0x040004E2 RID: 1250
	public int x;

	// Token: 0x040004E3 RID: 1251
	public int y;

	// Token: 0x040004E4 RID: 1252
	public int loop;

	// Token: 0x040004E5 RID: 1253
	public int tLoop;

	// Token: 0x040004E6 RID: 1254
	public int tLoopCount;

	// Token: 0x040004E7 RID: 1255
	private bool isPaint = true;

	// Token: 0x040004E8 RID: 1256
	public int layer;

	// Token: 0x040004E9 RID: 1257
	public int isStand;

	// Token: 0x040004EA RID: 1258
	public static MyVector vEffData = new MyVector();

	// Token: 0x040004EB RID: 1259
	public int trans;

	// Token: 0x040004EC RID: 1260
	public long timeExist;

	// Token: 0x040004ED RID: 1261
	public static MyVector lastEff = new MyVector();

	// Token: 0x040004EE RID: 1262
	public static MyVector newEff = new MyVector();

	// Token: 0x040004EF RID: 1263
	private int[] khangia1 = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x040004F0 RID: 1264
	private int[] khangia2 = new int[]
	{
		2,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		3
	};

	// Token: 0x040004F1 RID: 1265
	private int[] khangia3 = new int[]
	{
		4,
		4,
		4,
		4,
		4,
		5,
		5,
		5,
		5,
		5
	};

	// Token: 0x040004F2 RID: 1266
	private int[] khangia4 = new int[]
	{
		6,
		6,
		6,
		6,
		6,
		7,
		7,
		7,
		7,
		7
	};

	// Token: 0x040004F3 RID: 1267
	private int[] khangia5 = new int[]
	{
		8,
		8,
		8,
		8,
		8,
		9,
		9,
		9,
		9,
		9
	};

	// Token: 0x040004F4 RID: 1268
	private bool isGetTime;

	// Token: 0x040004F5 RID: 1269
	private short[] data;

	// Token: 0x040004F6 RID: 1270
	public int cLastStatusMe;

	// Token: 0x040004F7 RID: 1271
	public long cur_time_cLastStatusMe;
}
