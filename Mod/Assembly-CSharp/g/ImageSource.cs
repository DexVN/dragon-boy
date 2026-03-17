using System;

namespace Assets.src.g
{
	// Token: 0x020000C0 RID: 192
	internal class ImageSource
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x000A94E3 File Offset: 0x000A76E3
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000A94FC File Offset: 0x000A76FC
		public static void checkRMS()
		{
			MyVector myVector = new MyVector();
			sbyte[] array = Rms.loadRMS("ImageSource");
			bool flag2 = array == null;
			if (flag2)
			{
				Service.gI().imageSource(myVector);
			}
			else
			{
				ImageSource.vRms = new MyVector();
				DataInputStream dataInputStream = new DataInputStream(array);
				bool flag3 = dataInputStream == null;
				if (!flag3)
				{
					try
					{
						short num = dataInputStream.readShort();
						string[] array2 = new string[(int)num];
						sbyte[] array3 = new sbyte[(int)num];
						for (int i = 0; i < (int)num; i++)
						{
							array2[i] = dataInputStream.readUTF();
							array3[i] = dataInputStream.readByte();
							ImageSource.vRms.addElement(new ImageSource(array2[i], array3[i]));
						}
						dataInputStream.close();
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
					Res.outz(string.Concat(new object[]
					{
						"vS size= ",
						ImageSource.vSource.size(),
						" vRMS size= ",
						ImageSource.vRms.size()
					}));
					bool flag = false;
					bool flag4 = flag;
					if (flag4)
					{
						for (int j = 0; j < ImageSource.vSource.size(); j++)
						{
							ImageSource imageSource = (ImageSource)ImageSource.vSource.elementAt(j);
							bool flag5 = !ImageSource.isExistID(imageSource.id);
							if (flag5)
							{
								myVector.addElement(imageSource);
							}
						}
						for (int k = 0; k < ImageSource.vRms.size(); k++)
						{
							ImageSource imageSource2 = (ImageSource)ImageSource.vRms.elementAt(k);
							bool flag6 = ImageSource.getVersionRMSByID(imageSource2.id) != ImageSource.getCurrVersionByID(imageSource2.id);
							if (flag6)
							{
								myVector.addElement(imageSource2);
							}
						}
					}
					Service.gI().imageSource(myVector);
				}
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000A9700 File Offset: 0x000A7900
		public static sbyte getVersionRMSByID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				bool flag = id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id);
				if (flag)
				{
					return ((ImageSource)ImageSource.vRms.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000A9768 File Offset: 0x000A7968
		public static sbyte getCurrVersionByID(string id)
		{
			for (int i = 0; i < ImageSource.vSource.size(); i++)
			{
				bool flag = id.Equals(((ImageSource)ImageSource.vSource.elementAt(i)).id);
				if (flag)
				{
					return ((ImageSource)ImageSource.vSource.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000A97D0 File Offset: 0x000A79D0
		public static bool isExistID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				bool flag = id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000A9824 File Offset: 0x000A7A24
		public static void saveRMS()
		{
			DataOutputStream dataOutputStream = new DataOutputStream();
			try
			{
				dataOutputStream.writeShort((short)ImageSource.vSource.size());
				for (int i = 0; i < ImageSource.vSource.size(); i++)
				{
					dataOutputStream.writeUTF(((ImageSource)ImageSource.vSource.elementAt(i)).id);
					dataOutputStream.writeByte(((ImageSource)ImageSource.vSource.elementAt(i)).version);
				}
				Rms.saveRMS("ImageSource", dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
		}

		// Token: 0x04001388 RID: 5000
		public sbyte version;

		// Token: 0x04001389 RID: 5001
		public string id;

		// Token: 0x0400138A RID: 5002
		public static MyVector vSource = new MyVector();

		// Token: 0x0400138B RID: 5003
		public static MyVector vRms = new MyVector();
	}
}
