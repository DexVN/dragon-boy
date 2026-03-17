using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class Main : MonoBehaviour
{
	// Token: 0x060004C4 RID: 1220 RVA: 0x0005CF54 File Offset: 0x0005B154
	private void Start()
	{
		bool flag = !Main.started;
		if (flag)
		{
			bool flag2 = Thread.CurrentThread.Name != "Main";
			if (flag2)
			{
				Thread.CurrentThread.Name = "Main";
			}
			Main.mainThreadName = Thread.CurrentThread.Name;
			Main.isPC = true;
			Main.started = true;
			bool flag3 = Main.isPC;
			if (flag3)
			{
				this.level = Rms.loadRMSInt("levelScreenKN");
				bool flag4 = this.level == 1;
				if (flag4)
				{
					Screen.SetResolution(720, 320, false);
				}
				else
				{
					Screen.SetResolution(1024, 600, false);
				}
			}
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0005D008 File Offset: 0x0005B208
	private void SetInit()
	{
		base.enabled = true;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0005D014 File Offset: 0x0005B214
	private void OnHideUnity(bool isGameShown)
	{
		bool flag = !isGameShown;
		if (flag)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0005D048 File Offset: 0x0005B248
	private void OnGUI()
	{
		bool flag = this.count < 10;
		if (!flag)
		{
			bool flag2 = this.fps == 0;
			if (flag2)
			{
				this.timefps = mSystem.currentTimeMillis();
			}
			else
			{
				bool flag3 = mSystem.currentTimeMillis() - this.timefps > 1000L;
				if (flag3)
				{
					this.max = this.fps;
					this.fps = 0;
					this.timefps = mSystem.currentTimeMillis();
				}
			}
			this.fps++;
			this.checkInput();
			Session_ME.update();
			Session_ME2.update();
			bool flag4 = Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount;
			if (flag4)
			{
				GameMidlet.gameCanvas.paint(Main.g);
				this.paintCount++;
				Main.g.reset();
			}
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0005D144 File Offset: 0x0005B344
	public void setsizeChange()
	{
		bool flag = !this.isRun;
		if (flag)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Application.runInBackground = true;
			Application.targetFrameRate = 35;
			base.useGUILayout = false;
			Main.isCompactDevice = Main.detectCompactDevice();
			bool flag2 = Main.main == null;
			if (flag2)
			{
				Main.main = this;
			}
			this.isRun = true;
			ScaleGUI.initScaleGUI();
			bool flag3 = Main.isPC;
			if (flag3)
			{
				Main.IMEI = SystemInfo.deviceUniqueIdentifier;
			}
			else
			{
				Main.IMEI = this.GetMacAddress();
			}
			Main.isPC = true;
			bool flag4 = Main.isPC;
			if (flag4)
			{
				Screen.fullScreen = false;
			}
			bool flag5 = Main.isWindowsPhone;
			if (flag5)
			{
				Main.typeClient = 6;
			}
			bool flag6 = Main.isPC;
			if (flag6)
			{
				Main.typeClient = 4;
			}
			bool iphoneVersionApp = Main.IphoneVersionApp;
			if (iphoneVersionApp)
			{
				Main.typeClient = 5;
			}
			bool flag7 = iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen;
			if (flag7)
			{
				Main.isIpod = true;
			}
			bool flag8 = iPhoneSettings.generation == iPhoneGeneration.iPhone4;
			if (flag8)
			{
				Main.isIphone4 = true;
			}
			Main.g = new mGraphics();
			Main.midlet = new GameMidlet();
			TileMap.loadBg();
			Paint.loadbg();
			PopUp.loadBg();
			GameScr.loadBg();
			InfoMe.gI().loadCharId();
			Panel.loadBg();
			Menu.loadBg();
			Key.mapKeyPC();
			SoundMn.gI().loadSound(TileMap.mapID);
			Main.g.CreateLineMaterial();
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00003136 File Offset: 0x00001336
	public static void setBackupIcloud(string path)
	{
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
	public string GetMacAddress()
	{
		string empty = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		for (int i = 0; i < allNetworkInterfaces.Length; i++)
		{
			PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
			bool flag = physicalAddress.ToString() != string.Empty;
			if (flag)
			{
				return physicalAddress.ToString();
			}
		}
		return string.Empty;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0005D320 File Offset: 0x0005B520
	public void doClearRMS()
	{
		bool flag = !Main.isPC;
		if (!flag)
		{
			int num = Rms.loadRMSInt("lastZoomlevel");
			bool flag2 = num != mGraphics.zoomLevel;
			if (flag2)
			{
				Rms.clearAll();
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				Rms.saveRMSInt("levelScreenKN", this.level);
			}
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0005D380 File Offset: 0x0005B580
	public static void closeKeyBoard()
	{
		bool visible = global::TouchScreenKeyboard.visible;
		if (visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0005D3AC File Offset: 0x0005B5AC
	private void FixedUpdate()
	{
		Rms.update();
		this.count++;
		bool flag = this.count < 10;
		if (!flag)
		{
			bool flag2 = this.up == 0;
			if (flag2)
			{
				this.timeup = mSystem.currentTimeMillis();
			}
			else
			{
				bool flag3 = mSystem.currentTimeMillis() - this.timeup > 1000L;
				if (flag3)
				{
					this.upmax = this.up;
					this.up = 0;
					this.timeup = mSystem.currentTimeMillis();
				}
			}
			this.up++;
			this.setsizeChange();
			this.updateCount++;
			ipKeyboard.update();
			GameMidlet.gameCanvas.update();
			Image.update();
			DataInputStream.update();
			SMS.update();
			Net.update();
			Main.f++;
			bool flag4 = Main.f > 8;
			if (flag4)
			{
				Main.f = 0;
			}
			bool flag5 = !Main.isPC;
			if (flag5)
			{
				int num = 1 / Main.a;
			}
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00003136 File Offset: 0x00001336
	private void Update()
	{
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0005D4BC File Offset: 0x0005B6BC
	private void checkInput()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		if (mouseButtonDown)
		{
			Vector3 mousePosition = Input.mousePosition;
			GameMidlet.gameCanvas.pointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButton)
		{
			Vector3 mousePosition2 = Input.mousePosition;
			GameMidlet.gameCanvas.pointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		if (mouseButtonUp)
		{
			Vector3 mousePosition3 = Input.mousePosition;
			this.lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			GameMidlet.gameCanvas.pointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
		}
		bool flag = Input.anyKeyDown && Event.current.type == EventType.KeyDown;
		if (flag)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			bool flag2 = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			if (flag2)
			{
				KeyCode keyCode = Event.current.keyCode;
				bool flag3 = keyCode != KeyCode.Alpha2;
				if (flag3)
				{
					bool flag4 = keyCode == KeyCode.Minus;
					if (flag4)
					{
						num = 95;
					}
				}
				else
				{
					num = 64;
				}
			}
			bool flag5 = num != 0;
			if (flag5)
			{
				GameMidlet.gameCanvas.keyPressedz(num);
			}
		}
		bool flag6 = Event.current.type == EventType.KeyUp;
		if (flag6)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			bool flag7 = num2 != 0;
			if (flag7)
			{
				GameMidlet.gameCanvas.keyReleasedz(num2);
			}
		}
		bool flag8 = Main.isPC;
		if (flag8)
		{
			GameMidlet.gameCanvas.scrollMouse((int)(Input.GetAxis("Mouse ScrollWheel") * 10f));
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			int x2 = (int)x / mGraphics.zoomLevel;
			int y2 = (Screen.height - (int)y) / mGraphics.zoomLevel;
			GameMidlet.gameCanvas.pointerMouse(x2, y2);
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0005D79C File Offset: 0x0005B99C
	private void OnApplicationQuit()
	{
		Debug.LogWarning("APP QUIT");
		GameCanvas.bRun = false;
		Session_ME.gI().close();
		Session_ME2.gI().close();
		bool flag = Main.isPC;
		if (flag)
		{
			Application.Quit();
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0005D7E4 File Offset: 0x0005B9E4
	private void OnApplicationPause(bool paused)
	{
		Main.isResume = false;
		if (paused)
		{
			bool flag = GameCanvas.isWaiting();
			if (flag)
			{
				Main.isQuitApp = true;
			}
		}
		else
		{
			Main.isResume = true;
		}
		bool visible = global::TouchScreenKeyboard.visible;
		if (visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
		bool flag2 = Main.isQuitApp;
		if (flag2)
		{
			Application.Quit();
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0005D848 File Offset: 0x0005BA48
	public static void exit()
	{
		bool flag = Main.isPC;
		if (flag)
		{
			Main.main.OnApplicationQuit();
		}
		else
		{
			Main.a = 0;
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0005D878 File Offset: 0x0005BA78
	public static bool detectCompactDevice()
	{
		return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0005D8B0 File Offset: 0x0005BAB0
	public static bool checkCanSendSMS()
	{
		return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
	}

	// Token: 0x04000A66 RID: 2662
	public static Main main;

	// Token: 0x04000A67 RID: 2663
	public static mGraphics g;

	// Token: 0x04000A68 RID: 2664
	public static GameMidlet midlet;

	// Token: 0x04000A69 RID: 2665
	public static string res = "res";

	// Token: 0x04000A6A RID: 2666
	public static string mainThreadName;

	// Token: 0x04000A6B RID: 2667
	public static bool started;

	// Token: 0x04000A6C RID: 2668
	public static bool isIpod;

	// Token: 0x04000A6D RID: 2669
	public static bool isIphone4;

	// Token: 0x04000A6E RID: 2670
	public static bool isPC;

	// Token: 0x04000A6F RID: 2671
	public static bool isWindowsPhone;

	// Token: 0x04000A70 RID: 2672
	public static bool isIPhone;

	// Token: 0x04000A71 RID: 2673
	public static bool IphoneVersionApp;

	// Token: 0x04000A72 RID: 2674
	public static string IMEI;

	// Token: 0x04000A73 RID: 2675
	public static int versionIp;

	// Token: 0x04000A74 RID: 2676
	public static int numberQuit = 1;

	// Token: 0x04000A75 RID: 2677
	public static int typeClient = 4;

	// Token: 0x04000A76 RID: 2678
	public const sbyte PC_VERSION = 4;

	// Token: 0x04000A77 RID: 2679
	public const sbyte IP_APPSTORE = 5;

	// Token: 0x04000A78 RID: 2680
	public const sbyte WINDOWSPHONE = 6;

	// Token: 0x04000A79 RID: 2681
	private int level;

	// Token: 0x04000A7A RID: 2682
	public const sbyte IP_JB = 3;

	// Token: 0x04000A7B RID: 2683
	private int updateCount;

	// Token: 0x04000A7C RID: 2684
	private int paintCount;

	// Token: 0x04000A7D RID: 2685
	private int count;

	// Token: 0x04000A7E RID: 2686
	private int fps;

	// Token: 0x04000A7F RID: 2687
	private int max;

	// Token: 0x04000A80 RID: 2688
	private int up;

	// Token: 0x04000A81 RID: 2689
	private int upmax;

	// Token: 0x04000A82 RID: 2690
	private long timefps;

	// Token: 0x04000A83 RID: 2691
	private long timeup;

	// Token: 0x04000A84 RID: 2692
	private bool isRun;

	// Token: 0x04000A85 RID: 2693
	public static int waitTick;

	// Token: 0x04000A86 RID: 2694
	public static int f;

	// Token: 0x04000A87 RID: 2695
	public static bool isResume;

	// Token: 0x04000A88 RID: 2696
	public static bool isMiniApp = true;

	// Token: 0x04000A89 RID: 2697
	public static bool isQuitApp;

	// Token: 0x04000A8A RID: 2698
	private Vector2 lastMousePos = default(Vector2);

	// Token: 0x04000A8B RID: 2699
	public static int a = 1;

	// Token: 0x04000A8C RID: 2700
	public static bool isCompactDevice = true;
}
