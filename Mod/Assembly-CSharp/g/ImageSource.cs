using System;

namespace Assets.src.g
{
	// Token: 0x020000AC RID: 172
	internal class ImageSource
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x0007028B File Offset: 0x0006E68B
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000702A4 File Offset: 0x0006E6A4
		public static void checkRMS()
		{
			MyVector myVector = new MyVector();
			sbyte[] array = Rms.loadRMS("ImageSource");
			if (array == null)
			{
				Service.gI().imageSource(myVector);
				return;
			}
			ImageSource.vRms = new MyVector();
			DataInputStream dataInputStream = new DataInputStream(array);
			if (dataInputStream == null)
			{
				return;
			}
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
			if (flag)
			{
				for (int j = 0; j < ImageSource.vSource.size(); j++)
				{
					ImageSource imageSource = (ImageSource)ImageSource.vSource.elementAt(j);
					if (!ImageSource.isExistID(imageSource.id))
					{
						myVector.addElement(imageSource);
					}
				}
				for (int k = 0; k < ImageSource.vRms.size(); k++)
				{
					ImageSource imageSource2 = (ImageSource)ImageSource.vRms.elementAt(k);
					if ((int)ImageSource.getVersionRMSByID(imageSource2.id) != (int)ImageSource.getCurrVersionByID(imageSource2.id))
					{
						myVector.addElement(imageSource2);
					}
				}
			}
			Service.gI().imageSource(myVector);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00070474 File Offset: 0x0006E874
		public static sbyte getVersionRMSByID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id))
				{
					return ((ImageSource)ImageSource.vRms.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000704D4 File Offset: 0x0006E8D4
		public static sbyte getCurrVersionByID(string id)
		{
			for (int i = 0; i < ImageSource.vSource.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vSource.elementAt(i)).id))
				{
					return ((ImageSource)ImageSource.vSource.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00070534 File Offset: 0x0006E934
		public static bool isExistID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00070580 File Offset: 0x0006E980
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

		// Token: 0x04000EAE RID: 3758
		public sbyte version;

		// Token: 0x04000EAF RID: 3759
		public string id;

		// Token: 0x04000EB0 RID: 3760
		public static MyVector vSource = new MyVector();

		// Token: 0x04000EB1 RID: 3761
		public static MyVector vRms = new MyVector();
	}
}
