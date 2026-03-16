using System;

// Token: 0x02000010 RID: 16
public class MyStream
{
	// Token: 0x0600006F RID: 111 RVA: 0x00003A80 File Offset: 0x00001E80
	public static DataInputStream readFile(string path)
	{
		path = Main.res + path;
		DataInputStream result;
		try
		{
			result = DataInputStream.getResourceAsStream(path);
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}
}
