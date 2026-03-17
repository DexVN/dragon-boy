using System;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class Rms
{
	// Token: 0x0600080A RID: 2058 RVA: 0x00090140 File Offset: 0x0008E340
	public static void saveRMS(string filename, sbyte[] data)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Rms.__saveRMS(filename, data);
		}
		else
		{
			Rms._saveRMS(filename, data);
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0009017C File Offset: 0x0008E37C
	public static sbyte[] loadRMS(string filename)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		sbyte[] result;
		if (flag)
		{
			result = Rms.__loadRMS(filename);
		}
		else
		{
			result = Rms._loadRMS(filename);
		}
		return result;
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x000901B8 File Offset: 0x0008E3B8
	public static string loadRMSString(string fileName)
	{
		sbyte[] array = Rms.loadRMS(fileName);
		bool flag = array == null;
		string result2;
		if (flag)
		{
			result2 = null;
		}
		else
		{
			DataInputStream dataInputStream = new DataInputStream(array);
			try
			{
				string result = dataInputStream.readUTF();
				dataInputStream.close();
				return result;
			}
			catch (Exception ex)
			{
				Cout.println(ex.StackTrace);
			}
			result2 = null;
		}
		return result2;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00090220 File Offset: 0x0008E420
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00090278 File Offset: 0x0008E478
	public static void saveRMSString(string filename, string data)
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF(data);
			Rms.saveRMS(filename, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x000902D0 File Offset: 0x0008E4D0
	private static void _saveRMS(string filename, sbyte[] data)
	{
		bool flag = Rms.status != 0;
		if (flag)
		{
			Debug.LogError("Cannot save RMS " + filename + " because current is saving " + Rms.filename);
		}
		else
		{
			Rms.filename = filename;
			Rms.data = data;
			Rms.status = 2;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Rms.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Debug.LogError("TOO LONG TO SAVE RMS " + filename);
			}
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00090368 File Offset: 0x0008E568
	private static sbyte[] _loadRMS(string filename)
	{
		bool flag = Rms.status != 0;
		sbyte[] result;
		if (flag)
		{
			Debug.LogError("Cannot load RMS " + filename + " because current is loading " + Rms.filename);
			result = null;
		}
		else
		{
			Rms.filename = filename;
			Rms.data = null;
			Rms.status = 3;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Rms.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Debug.LogError("TOO LONG TO LOAD RMS " + filename);
			}
			result = Rms.data;
		}
		return result;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0009040C File Offset: 0x0008E60C
	public static void update()
	{
		bool flag = Rms.status == 2;
		if (flag)
		{
			Rms.status = 1;
			Rms.__saveRMS(Rms.filename, Rms.data);
			Rms.status = 0;
		}
		else
		{
			bool flag2 = Rms.status == 3;
			if (flag2)
			{
				Rms.status = 1;
				Rms.data = Rms.__loadRMS(Rms.filename);
				Rms.status = 0;
			}
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00090470 File Offset: 0x0008E670
	public static int loadRMSInt(string file)
	{
		sbyte[] array = Rms.loadRMS(file);
		return (int)((array != null) ? array[0] : -1);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00090494 File Offset: 0x0008E694
	public static void saveRMSInt(string file, int x)
	{
		try
		{
			Rms.saveRMS(file, new sbyte[]
			{
				(sbyte)x
			});
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x000904D0 File Offset: 0x0008E6D0
	public static string GetiPhoneDocumentsPath()
	{
		return Application.persistentDataPath;
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000904E8 File Offset: 0x0008E6E8
	private static void __saveRMS(string filename, sbyte[] data)
	{
		string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
		FileStream fileStream = new FileStream(text, FileMode.Create);
		fileStream.Write(ArrayCast.cast(data), 0, data.Length);
		fileStream.Flush();
		fileStream.Close();
		Main.setBackupIcloud(text);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00090538 File Offset: 0x0008E738
	private static sbyte[] __loadRMS(string filename)
	{
		sbyte[] result;
		try
		{
			FileStream fileStream = new FileStream(Rms.GetiPhoneDocumentsPath() + "/" + filename, FileMode.Open);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			sbyte[] array2 = ArrayCast.cast(array);
			result = ArrayCast.cast(array);
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x000905B0 File Offset: 0x0008E7B0
	public static void clearAll()
	{
		Cout.LogError3("clean rms");
		foreach (FileInfo fileInfo in new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles())
		{
			fileInfo.Delete();
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00090600 File Offset: 0x0008E800
	public static void DeleteStorage(string path)
	{
		try
		{
			File.Delete(Rms.GetiPhoneDocumentsPath() + "/" + path);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00090640 File Offset: 0x0008E840
	public static string ByteArrayToString(byte[] ba)
	{
		string text = BitConverter.ToString(ba);
		return text.Replace("-", string.Empty);
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0009066C File Offset: 0x0008E86C
	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x000906B8 File Offset: 0x0008E8B8
	public static void deleteRecord(string name)
	{
		try
		{
			PlayerPrefs.DeleteKey(name);
		}
		catch (Exception ex)
		{
			Cout.println("loi xoa RMS --------------------------" + ex.ToString());
		}
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x000906FC File Offset: 0x0008E8FC
	public static void clearRMS()
	{
		Rms.deleteRecord("data");
		Rms.deleteRecord("dataVersion");
		Rms.deleteRecord("map");
		Rms.deleteRecord("mapVersion");
		Rms.deleteRecord("skill");
		Rms.deleteRecord("killVersion");
		Rms.deleteRecord("item");
		Rms.deleteRecord("itemVersion");
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00090762 File Offset: 0x0008E962
	public static void saveIP(string strID)
	{
		Rms.saveRMSString("NRIPlink", strID);
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00090774 File Offset: 0x0008E974
	public static string loadIP()
	{
		string text = Rms.loadRMSString("NRIPlink");
		bool flag = text == null;
		string result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = text;
		}
		return result;
	}

	// Token: 0x0400108A RID: 4234
	public static int status;

	// Token: 0x0400108B RID: 4235
	public static sbyte[] data;

	// Token: 0x0400108C RID: 4236
	public static string filename;

	// Token: 0x0400108D RID: 4237
	private const int INTERVAL = 5;

	// Token: 0x0400108E RID: 4238
	private const int MAXTIME = 500;
}
