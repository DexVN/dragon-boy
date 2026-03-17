using System;
using System.Threading;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class Sound
{
	// Token: 0x06000943 RID: 2371 RVA: 0x00003136 File Offset: 0x00001336
	public static void setActivity(SoundMn.AssetManager ac)
	{
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0009BB00 File Offset: 0x00099D00
	public static void stop()
	{
		for (int i = 0; i < Sound.player.Length; i++)
		{
			bool flag = Sound.player[i] != null;
			if (flag)
			{
				Sound.player[i].GetComponent<AudioSource>().Pause();
			}
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0009BB4C File Offset: 0x00099D4C
	public static bool isPlaying()
	{
		return false;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0009BB60 File Offset: 0x00099D60
	public static void init()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Audio Player";
		gameObject.transform.position = Vector3.zero;
		gameObject.AddComponent<AudioListener>();
		Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0009BBA4 File Offset: 0x00099DA4
	public static void init(int[] musicID, int[] sID)
	{
		bool flag = Sound.player != null || Sound.music != null;
		if (!flag)
		{
			Sound.init();
			Sound.l1 = musicID.Length;
			Sound.player = new GameObject[musicID.Length + sID.Length];
			Sound.music = new AudioClip[musicID.Length + sID.Length];
			for (int i = 0; i < Sound.player.Length; i++)
			{
				string fileName = (i >= Sound.l1) ? ("/sound/" + (i - Sound.l1).ToString()) : ("/music/" + i.ToString());
				Sound.getAssetSoundFile(fileName, i);
			}
		}
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0009BC55 File Offset: 0x00099E55
	public static void playSound(int id, float volume)
	{
		Sound.play(id + Sound.l1, volume);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0009BC66 File Offset: 0x00099E66
	public static void playSound1(int id, float volume)
	{
		Sound.play(id, volume);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0009BC74 File Offset: 0x00099E74
	public static void getAssetSoundFile(string fileName, int pos)
	{
		Sound.stop(pos);
		string filename = string.Empty;
		filename = Main.res + fileName;
		Sound.load(filename, pos);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0009BCA4 File Offset: 0x00099EA4
	public static void stopAllz()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		for (int j = 0; j < Sound.l1; j++)
		{
			Sound.sTopSoundBG(j);
		}
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0009BCF0 File Offset: 0x00099EF0
	public static void stopAllBg()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		Sound.sTopSoundBG(0);
		Sound.sTopSoundRun();
		Sound.stopSoundNatural(0);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00003136 File Offset: 0x00001336
	public static void update()
	{
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0009BD34 File Offset: 0x00099F34
	public static void stopMusic(int x)
	{
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			Sound.stop(x);
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0009BD54 File Offset: 0x00099F54
	public static void play(int id, float volume)
	{
		bool flag = Sound.isNotPlay;
		if (!flag)
		{
			bool isPlaySound = GameCanvas.isPlaySound;
			if (isPlaySound)
			{
				Sound.start(volume, id);
			}
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0009BD84 File Offset: 0x00099F84
	public static void playSoundRun(int id, float volume)
	{
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			bool flag = Sound.SoundRun == null;
			if (!flag)
			{
				Sound.SoundRun.GetComponent<AudioSource>().loop = true;
				Sound.SoundRun.GetComponent<AudioSource>().clip = Sound.music[id];
				Sound.SoundRun.GetComponent<AudioSource>().volume = volume;
				Sound.SoundRun.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0009BDF8 File Offset: 0x00099FF8
	public static void sTopSoundRun()
	{
		Sound.SoundRun.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0009BE0C File Offset: 0x0009A00C
	public static bool isPlayingSound()
	{
		return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0009BE40 File Offset: 0x0009A040
	public static void playSoundNatural(int id, float volume, bool isLoop)
	{
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			bool flag = Sound.SoundBGLoop == null;
			if (!flag)
			{
				Sound.SoundWater.GetComponent<AudioSource>().loop = isLoop;
				Sound.SoundWater.GetComponent<AudioSource>().clip = Sound.music[id];
				Sound.SoundWater.GetComponent<AudioSource>().volume = volume;
				Sound.SoundWater.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0009BEB4 File Offset: 0x0009A0B4
	public static void stopSoundNatural(int id)
	{
		Sound.SoundWater.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0009BEC8 File Offset: 0x0009A0C8
	public static bool isPlayingSoundatural(int id)
	{
		return !(Sound.SoundWater == null) && Sound.SoundWater.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0009BEFC File Offset: 0x0009A0FC
	public static void playMus(int type, float vl, bool loop)
	{
		bool flag = Sound.isNotPlay;
		if (!flag)
		{
			vl -= 0.3f;
			bool flag2 = vl <= 0f;
			if (flag2)
			{
				vl = 0.01f;
			}
			Sound.playSoundBGLoop(type, vl);
		}
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0009BF40 File Offset: 0x0009A140
	public static void playSoundBGLoop(int id, float volume)
	{
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			bool flag = id == SoundMn.AIR_SHIP;
			if (flag)
			{
				Sound.playSound1(id, volume + 0.2f);
			}
			else
			{
				bool flag2 = Sound.SoundBGLoop == null;
				if (!flag2)
				{
					bool flag3 = Sound.isPlayingSoundBG(id);
					if (!flag3)
					{
						Sound.SoundBGLoop.GetComponent<AudioSource>().loop = true;
						Sound.SoundBGLoop.GetComponent<AudioSource>().clip = Sound.music[id];
						Sound.SoundBGLoop.GetComponent<AudioSource>().volume = volume;
						Sound.SoundBGLoop.GetComponent<AudioSource>().Play();
					}
				}
			}
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0009BFE1 File Offset: 0x0009A1E1
	public static void sTopSoundBG(int id)
	{
		Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0009BFF4 File Offset: 0x0009A1F4
	public static bool isPlayingSoundBG(int id)
	{
		return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0009C028 File Offset: 0x0009A228
	public static void load(string filename, int pos)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Sound.__load(filename, pos);
		}
		else
		{
			Sound._load(filename, pos);
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0009C064 File Offset: 0x0009A264
	private static void _load(string filename, int pos)
	{
		bool flag = Sound.status != 0;
		if (flag)
		{
			Cout.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + Sound.filenametemp);
		}
		else
		{
			Sound.filenametemp = filename;
			Sound.postem = pos;
			Sound.status = 2;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Sound.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 100;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR LOAD AUDIO " + filename);
			}
			else
			{
				Cout.Log(string.Concat(new object[]
				{
					"Load Audio ",
					filename,
					" done in ",
					i * 5,
					"ms"
				}));
			}
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0009C134 File Offset: 0x0009A334
	private static void __load(string filename, int pos)
	{
		Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
		GameObject.Find("Main Camera").AddComponent<AudioSource>();
		Sound.player[pos] = GameObject.Find("Main Camera");
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0009C174 File Offset: 0x0009A374
	public static void start(float volume, int pos)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Sound.__start(volume, pos);
		}
		else
		{
			Sound._start(volume, pos);
		}
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x0009C1B0 File Offset: 0x0009A3B0
	public static void _start(float volume, int pos)
	{
		bool flag = Sound.status != 0;
		if (flag)
		{
			Debug.LogError("CANNOT START AUDIO WHEN STARTING");
		}
		else
		{
			Sound.volumetem = volume;
			Sound.postem = pos;
			Sound.status = 3;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Sound.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 100;
			if (flag3)
			{
				Debug.LogError("TOO LONG FOR START AUDIO");
			}
			else
			{
				Debug.Log("Start Audio done in " + (i * 5).ToString() + "ms");
			}
		}
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0009C250 File Offset: 0x0009A450
	public static void __start(float volume, int pos)
	{
		bool flag = Sound.player[pos] == null;
		if (!flag)
		{
			Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
		}
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x0009C28C File Offset: 0x0009A48C
	public static void stop(int pos)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Sound.__stop(pos);
		}
		else
		{
			Sound._stop(pos);
		}
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0009C2C8 File Offset: 0x0009A4C8
	public static void _stop(int pos)
	{
		bool flag = Sound.status != 0;
		if (flag)
		{
			Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
		}
		else
		{
			Sound.postem = pos;
			Sound.status = 4;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Sound.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 100;
			if (flag3)
			{
				Debug.LogError("TOO LONG FOR STOP AUDIO");
			}
			else
			{
				Debug.Log("Stop Audio done in " + (i * 5).ToString() + "ms");
			}
		}
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0009C364 File Offset: 0x0009A564
	public static void __stop(int pos)
	{
		bool flag = Sound.player[pos] != null;
		if (flag)
		{
			Sound.player[pos].GetComponent<AudioSource>().Stop();
		}
	}

	// Token: 0x04001184 RID: 4484
	private const int INTERVAL = 5;

	// Token: 0x04001185 RID: 4485
	private const int MAXTIME = 100;

	// Token: 0x04001186 RID: 4486
	public static int status;

	// Token: 0x04001187 RID: 4487
	public static int postem;

	// Token: 0x04001188 RID: 4488
	public static int timestart;

	// Token: 0x04001189 RID: 4489
	private static string filenametemp;

	// Token: 0x0400118A RID: 4490
	private static float volumetem;

	// Token: 0x0400118B RID: 4491
	public static bool isSound = true;

	// Token: 0x0400118C RID: 4492
	public static bool isNotPlay;

	// Token: 0x0400118D RID: 4493
	public static bool stopAll;

	// Token: 0x0400118E RID: 4494
	public static AudioSource SoundWater;

	// Token: 0x0400118F RID: 4495
	public static AudioSource SoundRun;

	// Token: 0x04001190 RID: 4496
	public static AudioSource SoundBGLoop;

	// Token: 0x04001191 RID: 4497
	public static AudioClip[] music;

	// Token: 0x04001192 RID: 4498
	public static GameObject[] player;

	// Token: 0x04001193 RID: 4499
	public static sbyte MLogin;

	// Token: 0x04001194 RID: 4500
	public static sbyte MBClick = 1;

	// Token: 0x04001195 RID: 4501
	public static sbyte MTone = 2;

	// Token: 0x04001196 RID: 4502
	public static sbyte MSanzu = 3;

	// Token: 0x04001197 RID: 4503
	public static sbyte MChakumi = 4;

	// Token: 0x04001198 RID: 4504
	public static sbyte MChai = 5;

	// Token: 0x04001199 RID: 4505
	public static sbyte MOshin = 6;

	// Token: 0x0400119A RID: 4506
	public static sbyte MEchigo = 7;

	// Token: 0x0400119B RID: 4507
	public static sbyte MKojin = 8;

	// Token: 0x0400119C RID: 4508
	public static sbyte MHaruna = 9;

	// Token: 0x0400119D RID: 4509
	public static sbyte MHirosaki = 10;

	// Token: 0x0400119E RID: 4510
	public static sbyte MOokaza = 11;

	// Token: 0x0400119F RID: 4511
	public static sbyte MGiotuyet = 12;

	// Token: 0x040011A0 RID: 4512
	public static sbyte MHangdong = 13;

	// Token: 0x040011A1 RID: 4513
	public static sbyte MDeKeu = 14;

	// Token: 0x040011A2 RID: 4514
	public static sbyte MChimKeu = 15;

	// Token: 0x040011A3 RID: 4515
	public static sbyte MBuocChan = 16;

	// Token: 0x040011A4 RID: 4516
	public static sbyte MNuocChay = 17;

	// Token: 0x040011A5 RID: 4517
	public static sbyte MBomMau = 18;

	// Token: 0x040011A6 RID: 4518
	public static sbyte MKiemGo = 19;

	// Token: 0x040011A7 RID: 4519
	public static sbyte MKiem = 20;

	// Token: 0x040011A8 RID: 4520
	public static sbyte MTieu = 21;

	// Token: 0x040011A9 RID: 4521
	public static sbyte MKunai = 22;

	// Token: 0x040011AA RID: 4522
	public static sbyte MCung = 23;

	// Token: 0x040011AB RID: 4523
	public static sbyte MDao = 24;

	// Token: 0x040011AC RID: 4524
	public static sbyte MQuat = 25;

	// Token: 0x040011AD RID: 4525
	public static sbyte MCung2 = 26;

	// Token: 0x040011AE RID: 4526
	public static sbyte MTieu2 = 27;

	// Token: 0x040011AF RID: 4527
	public static sbyte MTieu3 = 28;

	// Token: 0x040011B0 RID: 4528
	public static sbyte MKiem2 = 29;

	// Token: 0x040011B1 RID: 4529
	public static sbyte MKiem3 = 30;

	// Token: 0x040011B2 RID: 4530
	public static sbyte MDao2 = 31;

	// Token: 0x040011B3 RID: 4531
	public static sbyte MDao3 = 32;

	// Token: 0x040011B4 RID: 4532
	public static sbyte MCung3 = 33;

	// Token: 0x040011B5 RID: 4533
	public static int l1;
}
