using System;

// Token: 0x0200007B RID: 123
public class MyStream
{
	// Token: 0x06000613 RID: 1555 RVA: 0x0006B1AC File Offset: 0x000693AC
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
