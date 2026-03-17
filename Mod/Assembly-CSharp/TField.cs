using System;
using System.Threading;

// Token: 0x020000B5 RID: 181
public class TField : IActionListener
{
	// Token: 0x060009CA RID: 2506 RVA: 0x000A398C File Offset: 0x000A1B8C
	public TField(mScreen parentScr)
	{
		this.text = string.Empty;
		this.parentScr = parentScr;
		this.init();
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x000A3A30 File Offset: 0x000A1C30
	public TField()
	{
		this.text = string.Empty;
		this.init();
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x000A3ACC File Offset: 0x000A1CCC
	public TField(int x, int y, int w, int h)
	{
		this.text = string.Empty;
		this.init();
		this.x = x;
		this.y = y;
		this.width = w;
		this.height = h;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x000A3B84 File Offset: 0x000A1D84
	public TField(string text, int maxLen, int inputType)
	{
		this.text = text;
		this.maxTextLenght = maxLen;
		this.inputType = inputType;
		this.init();
		this.isTfield = true;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000A3C30 File Offset: 0x000A1E30
	public static bool setNormal(char ch)
	{
		return (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00003136 File Offset: 0x00001336
	public void doChangeToTextBox()
	{
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000A3C6C File Offset: 0x000A1E6C
	public static void setVendorTypeMode(int mode)
	{
		bool flag = mode == TField.MOTO;
		if (flag)
		{
			TField.print[0] = "0";
			TField.print[10] = " *";
			TField.print[11] = "#";
			TField.changeModeKey = 35;
		}
		else
		{
			bool flag2 = mode == TField.NOKIA;
			if (flag2)
			{
				TField.print[0] = " 0";
				TField.print[10] = "*";
				TField.print[11] = "#";
				TField.changeModeKey = 35;
			}
			else
			{
				bool flag3 = mode == TField.ORTHER;
				if (flag3)
				{
					TField.print[0] = "0";
					TField.print[10] = "*";
					TField.print[11] = " #";
					TField.changeModeKey = 42;
				}
			}
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x000A3D30 File Offset: 0x000A1F30
	public void init()
	{
		TField.CARET_HEIGHT = mScreen.ITEM_HEIGHT + 1;
		this.cmdClear = new Command(mResources.DELETE, this, 1000, null);
		bool isPC = Main.isPC;
		if (isPC)
		{
			TField.typeXpeed = 0;
		}
		bool flag = TField.imgTf == null;
		if (flag)
		{
			TField.imgTf = GameCanvas.loadImage("/mainImage/myTexture2dtf.png");
		}
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x000A3D90 File Offset: 0x000A1F90
	public void clearKeyWhenPutText(int keyCode)
	{
		bool flag = keyCode != -8;
		if (!flag)
		{
			bool flag2 = this.timeDelayKyCode > 0;
			if (!flag2)
			{
				bool flag3 = this.timeDelayKyCode <= 0;
				if (flag3)
				{
					this.timeDelayKyCode = 1;
				}
				this.clear();
			}
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x000A3DE0 File Offset: 0x000A1FE0
	public void clearAllText()
	{
		this.text = string.Empty;
		bool flag = TField.kb != null;
		if (flag)
		{
			TField.kb.text = string.Empty;
		}
		this.caretPos = 0;
		this.setOffset(0);
		this.setPasswordTest();
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x000A3E2C File Offset: 0x000A202C
	public void clear()
	{
		bool flag = this.caretPos > 0 && this.text.Length > 0;
		if (flag)
		{
			this.text = this.text.Substring(0, this.caretPos - 1);
			this.caretPos--;
			this.setOffset(0);
			this.setPasswordTest();
			bool flag2 = TField.kb != null;
			if (flag2)
			{
				TField.kb.text = this.text;
			}
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x000A3EB0 File Offset: 0x000A20B0
	public void clearAll()
	{
		bool flag = this.caretPos > 0 && this.text.Length > 0;
		if (flag)
		{
			this.text = this.text.Substring(0, this.text.Length - 1);
			this.caretPos--;
			this.setOffset();
			this.setPasswordTest();
			this.setFocusWithKb(true);
			bool flag2 = TField.kb != null;
			if (flag2)
			{
				TField.kb.text = string.Empty;
			}
		}
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x000A3F40 File Offset: 0x000A2140
	public void setOffset()
	{
		bool flag = this.paintedText == null || mFont.tahoma_8b == null;
		if (!flag)
		{
			bool flag2 = this.inputType == TField.INPUT_TYPE_PASSWORD;
			if (flag2)
			{
				this.paintedText = this.passwordText;
			}
			else
			{
				this.paintedText = this.text;
			}
			bool flag3 = this.offsetX < 0 && mFont.tahoma_8b.getWidth(this.paintedText) + this.offsetX < this.width - TField.TEXT_GAP_X - 13 - TField.typingModeAreaWidth;
			if (flag3)
			{
				this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText);
			}
			bool flag4 = this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) <= 0;
			if (flag4)
			{
				this.offsetX = -mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
				this.offsetX += 40;
			}
			else
			{
				bool flag5 = this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) >= this.width - 12 - TField.typingModeAreaWidth;
				if (flag5)
				{
					this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) - 2 * TField.TEXT_GAP_X;
				}
			}
			bool flag6 = this.offsetX > 0;
			if (flag6)
			{
				this.offsetX = 0;
			}
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x000A40F4 File Offset: 0x000A22F4
	private void keyPressedAny(int keyCode)
	{
		bool flag = this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY;
		string[] array;
		if (flag)
		{
			array = TField.printA;
		}
		else
		{
			array = TField.print;
		}
		bool flag2 = keyCode == TField.lastKey;
		if (flag2)
		{
			this.indexOfActiveChar = (this.indexOfActiveChar + 1) % array[keyCode - 48].Length;
			char c = array[keyCode - 48][this.indexOfActiveChar];
			bool flag3 = TField.mode == 0;
			if (flag3)
			{
				c = char.ToLower(c);
			}
			else
			{
				bool flag4 = TField.mode == 1;
				if (flag4)
				{
					c = char.ToUpper(c);
				}
				else
				{
					bool flag5 = TField.mode == 2;
					if (flag5)
					{
						c = char.ToUpper(c);
					}
					else
					{
						c = array[keyCode - 48][array[keyCode - 48].Length - 1];
					}
				}
			}
			string str = this.text.Substring(0, this.caretPos - 1) + c.ToString();
			bool flag6 = this.caretPos < this.text.Length;
			if (flag6)
			{
				str += this.text.Substring(this.caretPos, this.text.Length);
			}
			this.text = str;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.setPasswordTest();
		}
		else
		{
			bool flag7 = this.text.Length < this.maxTextLenght;
			if (flag7)
			{
				bool flag8 = TField.mode == 1 && TField.lastKey != -1984;
				if (flag8)
				{
					TField.mode = 0;
				}
				this.indexOfActiveChar = 0;
				char c2 = array[keyCode - 48][this.indexOfActiveChar];
				bool flag9 = TField.mode == 0;
				if (flag9)
				{
					c2 = char.ToLower(c2);
				}
				else
				{
					bool flag10 = TField.mode == 1;
					if (flag10)
					{
						c2 = char.ToUpper(c2);
					}
					else
					{
						bool flag11 = TField.mode == 2;
						if (flag11)
						{
							c2 = char.ToUpper(c2);
						}
						else
						{
							c2 = array[keyCode - 48][array[keyCode - 48].Length - 1];
						}
					}
				}
				string str2 = this.text.Substring(0, this.caretPos) + c2.ToString();
				bool flag12 = this.caretPos < this.text.Length;
				if (flag12)
				{
					str2 += this.text.Substring(this.caretPos, this.text.Length);
				}
				this.text = str2;
				this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
				this.caretPos++;
				this.setPasswordTest();
				this.setOffset();
			}
		}
		TField.lastKey = keyCode;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x000A43C8 File Offset: 0x000A25C8
	private void keyPressedAscii(int keyCode)
	{
		bool flag = (this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY) && (keyCode < 48 || keyCode > 57) && (keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122);
		if (!flag)
		{
			bool flag2 = this.text.Length < this.maxTextLenght;
			if (flag2)
			{
				string str = this.text.Substring(0, this.caretPos) + ((char)keyCode).ToString();
				bool flag3 = this.caretPos < this.text.Length;
				if (flag3)
				{
					str += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
				}
				this.text = str;
				this.caretPos++;
				this.setPasswordTest();
				this.setOffset(0);
			}
			bool flag4 = TField.kb != null;
			if (flag4)
			{
				TField.kb.text = this.text;
			}
		}
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x000A44E8 File Offset: 0x000A26E8
	public static void setMode()
	{
		TField.mode++;
		bool flag = TField.mode > 3;
		if (flag)
		{
			TField.mode = 0;
		}
		TField.lastKey = TField.changeModeKey;
		TField.timeChangeMode = (long)(Environment.TickCount / 1000);
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x000A4534 File Offset: 0x000A2734
	private void setDau()
	{
		this.timeDau = (long)(Environment.TickCount / 100);
		bool flag = this.indexDau == -1;
		if (flag)
		{
			for (int i = this.caretPos; i > 0; i--)
			{
				char c = this.text[i - 1];
				for (int j = 0; j < TField.printDau.Length; j++)
				{
					char c2 = TField.printDau[j];
					bool flag2 = c == c2;
					if (flag2)
					{
						this.indexTemplate = j;
						this.indexCong = 0;
						this.indexDau = i - 1;
						return;
					}
				}
			}
			this.indexDau = -1;
		}
		else
		{
			this.indexCong++;
			bool flag3 = this.indexCong >= 6;
			if (flag3)
			{
				this.indexCong = 0;
			}
			string str = this.text.Substring(0, this.indexDau);
			string str2 = this.text.Substring(this.indexDau + 1);
			string str3 = TField.printDau.Substring(this.indexTemplate + this.indexCong, 1);
			this.text = str + str3 + str2;
		}
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x000A4668 File Offset: 0x000A2868
	public bool keyPressed(int keyCode)
	{
		bool flag = Main.isPC && keyCode == -8;
		bool result;
		if (flag)
		{
			this.clearKeyWhenPutText(-8);
			result = true;
		}
		else
		{
			bool flag2 = keyCode == 8 || keyCode == -8 || keyCode == 204;
			if (flag2)
			{
				this.clear();
				result = true;
			}
			else
			{
				bool flag3 = TField.isQwerty && keyCode >= 32;
				if (flag3)
				{
					this.keyPressedAscii(keyCode);
					result = false;
				}
				else
				{
					bool flag4 = keyCode == TField.changeDau && this.inputType == TField.INPUT_TYPE_ANY;
					if (flag4)
					{
						this.setDau();
						result = false;
					}
					else
					{
						bool flag5 = keyCode == 42;
						if (flag5)
						{
							keyCode = 58;
						}
						bool flag6 = keyCode == 35;
						if (flag6)
						{
							keyCode = 59;
						}
						bool flag7 = keyCode >= 48 && keyCode <= 59;
						if (flag7)
						{
							bool flag8 = this.inputType == TField.INPUT_TYPE_ANY || this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY;
							if (flag8)
							{
								this.keyPressedAny(keyCode);
							}
							else
							{
								bool flag9 = this.inputType == TField.INPUT_TYPE_NUMERIC;
								if (flag9)
								{
									this.keyPressedAscii(keyCode);
									this.keyInActiveState = 1;
								}
							}
						}
						else
						{
							this.indexOfActiveChar = 0;
							TField.lastKey = -1984;
							bool flag10 = keyCode == 14 && !this.lockArrow;
							if (flag10)
							{
								bool flag11 = this.caretPos > 0;
								if (flag11)
								{
									this.caretPos--;
									this.setOffset(0);
									this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
									return false;
								}
							}
							else
							{
								bool flag12 = keyCode == 15 && !this.lockArrow;
								if (flag12)
								{
									bool flag13 = this.caretPos < this.text.Length;
									if (flag13)
									{
										this.caretPos++;
										this.setOffset(0);
										this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
										return false;
									}
								}
								else
								{
									bool flag14 = keyCode == 19;
									if (flag14)
									{
										this.clear();
										return false;
									}
									TField.lastKey = keyCode;
								}
							}
						}
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x000A4898 File Offset: 0x000A2A98
	public void setOffset(int index)
	{
		bool flag = this.inputType == TField.INPUT_TYPE_PASSWORD;
		if (flag)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		int num = mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
		bool flag2 = index == -1;
		if (flag2)
		{
			bool flag3 = num + this.offsetX < 15 && this.caretPos > 0 && this.caretPos < this.paintedText.Length;
			if (flag3)
			{
				this.offsetX += mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos, 1));
			}
		}
		else
		{
			bool flag4 = index == 1;
			if (flag4)
			{
				bool flag5 = num + this.offsetX > this.width - 25 && this.caretPos < this.paintedText.Length && this.caretPos > 0;
				if (flag5)
				{
					this.offsetX -= mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos - 1, 1));
				}
			}
			else
			{
				this.offsetX = -(num - (this.width - 12));
			}
		}
		bool flag6 = this.offsetX > 0;
		if (flag6)
		{
			this.offsetX = 0;
		}
		else
		{
			bool flag7 = this.offsetX < 0;
			if (flag7)
			{
				int num2 = mFont.tahoma_8b.getWidth(this.paintedText) - (this.width - 12);
				bool flag8 = this.offsetX < -num2;
				if (flag8)
				{
					this.offsetX = -num2;
				}
			}
		}
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000A4A48 File Offset: 0x000A2C48
	public void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text, string info)
	{
		g.setColor(0);
		if (iss)
		{
			g.drawRegion(TField.imgTf, 0, 81, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 135, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + w - 58, y, 0);
			for (int i = 0; i < (w - 58) / 29; i++)
			{
				g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + 29 + i * 29, y, 0);
			}
		}
		else
		{
			g.drawRegion(TField.imgTf, 0, 0, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 54, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + w - 58, y, 0);
			for (int j = 0; j < (w - 58) / 29; j++)
			{
				g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + 29 + j * 29, y, 0);
			}
		}
		g.setClip(x + 3, y + 1, w - 4, h);
		bool flag = text != null && !text.Equals(string.Empty);
		if (flag)
		{
			mFont.tahoma_8b.drawString(g, text, xText, yText, 0);
		}
		else
		{
			bool flag2 = info != null;
			if (flag2)
			{
				if (iss)
				{
					mFont.tahoma_7b_focus.drawString(g, info, xText, yText, 0);
				}
				else
				{
					mFont.tahoma_7b_unfocus.drawString(g, info, xText, yText, 0);
				}
			}
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x000A4C14 File Offset: 0x000A2E14
	public void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag = this.isFocused();
		bool flag2 = this.inputType == TField.INPUT_TYPE_PASSWORD;
		if (flag2)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		this.paintInputTf(g, flag, this.x, this.y - 1, this.width, this.height + 5, TField.TEXT_GAP_X + this.offsetX + this.x + 1, this.y + (this.height - mFont.tahoma_8b.getHeight()) / 2 + 2, this.paintedText, this.name);
		g.setClip(this.x + 3, this.y + 1, this.width - 4, this.height - 2);
		g.setColor(0);
		bool flag3 = flag && this.isPaintMouse && this.isPaintCarret;
		if (flag3)
		{
			bool flag4 = this.keyInActiveState == 0 && (this.showCaretCounter > 0 || this.counter / TField.CARET_SHOWING_TIME % 4 == 0);
			if (flag4)
			{
				g.setColor(7999781);
				g.fillRect(TField.TEXT_GAP_X + 1 + this.offsetX + this.x + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos) + "a") - TField.CARET_WIDTH - mFont.tahoma_8b.getWidth("a"), this.y + (this.height - TField.CARET_HEIGHT) / 2 + 5, TField.CARET_WIDTH, TField.CARET_HEIGHT);
			}
			GameCanvas.resetTrans(g);
			bool flag5 = this.text != null && this.text.Length > 0 && GameCanvas.isTouch;
			if (flag5)
			{
				g.drawImage(GameCanvas.imgClear, this.x + this.width - 13, this.y + this.height / 2 + 3, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x000A4E38 File Offset: 0x000A3038
	private bool isFocused()
	{
		return this.isFocus;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x000A4E50 File Offset: 0x000A3050
	public string subString(string str, int index, int indexTo)
	{
		bool flag = index >= 0 && indexTo > str.Length - 1;
		string result;
		if (flag)
		{
			result = str.Substring(index);
		}
		else
		{
			bool flag2 = index < 0 || index > str.Length - 1 || indexTo < 0 || indexTo > str.Length - 1;
			if (flag2)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Empty;
				for (int i = index; i < indexTo; i++)
				{
					text += str[i].ToString();
				}
				result = text;
			}
		}
		return result;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x000A4EE8 File Offset: 0x000A30E8
	private void setPasswordTest()
	{
		bool flag = this.inputType == TField.INPUT_TYPE_PASSWORD;
		if (flag)
		{
			this.passwordText = string.Empty;
			for (int i = 0; i < this.text.Length; i++)
			{
				this.passwordText += "*";
			}
			bool flag2 = this.keyInActiveState > 0 && this.caretPos > 0;
			if (flag2)
			{
				this.passwordText = this.passwordText.Substring(0, this.caretPos - 1) + this.text[this.caretPos - 1].ToString() + this.passwordText.Substring(this.caretPos, this.passwordText.Length);
			}
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x000A4FBC File Offset: 0x000A31BC
	public void update()
	{
		this.isPaintCarret = true;
		bool isPC = Main.isPC;
		if (isPC)
		{
			bool flag = this.timeDelayKyCode > 0;
			if (flag)
			{
				this.timeDelayKyCode--;
			}
			bool flag2 = this.timeDelayKyCode <= 0;
			if (flag2)
			{
				this.timeDelayKyCode = 0;
			}
		}
		bool flag3 = TField.kb != null && TField.currentTField == this;
		if (flag3)
		{
			bool flag4 = TField.kb.text.Length < 40 && this.isFocus;
			if (flag4)
			{
				this.setText(TField.kb.text);
			}
			bool flag5 = TField.kb.done && this.cmdDoneAction != null;
			if (flag5)
			{
				this.cmdDoneAction.performAction();
			}
		}
		this.counter++;
		bool flag6 = this.keyInActiveState > 0;
		if (flag6)
		{
			this.keyInActiveState--;
			bool flag7 = this.keyInActiveState == 0;
			if (flag7)
			{
				this.indexOfActiveChar = 0;
				bool flag8 = TField.mode == 1 && TField.lastKey != TField.changeModeKey && this.isFocus;
				if (flag8)
				{
					TField.mode = 0;
				}
				TField.lastKey = -1984;
				this.setPasswordTest();
			}
		}
		bool flag9 = this.showCaretCounter > 0;
		if (flag9)
		{
			this.showCaretCounter--;
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			this.setTextBox();
		}
		bool flag10 = this.indexDau != -1 && (long)(Environment.TickCount / 100) - this.timeDau > 5L;
		if (flag10)
		{
			this.indexDau = -1;
		}
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x000A5170 File Offset: 0x000A3370
	public void setTextBox()
	{
		bool flag = GameCanvas.isPointerHoldIn(this.x + this.width - 20, this.y, 40, this.height);
		if (flag)
		{
			this.clearAllText();
			this.isFocus = true;
		}
		else
		{
			bool flag2 = GameCanvas.isPointerHoldIn(this.x, this.y, this.width - 20, this.height);
			if (flag2)
			{
				this.setFocusWithKb(true);
			}
			else
			{
				this.setFocus(false);
			}
		}
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000A51F4 File Offset: 0x000A33F4
	public void setFocus(bool isFocus)
	{
		bool flag = this.isFocus != isFocus;
		if (flag)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		if (isFocus)
		{
			TField.currentTField = this;
			bool flag2 = TField.kb != null;
			if (flag2)
			{
				TField.kb.text = TField.currentTField.text;
			}
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x000A5278 File Offset: 0x000A3478
	public void setFocusWithKb(bool isFocus)
	{
		bool flag = this.isFocus != isFocus;
		if (flag)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		bool flag2 = isFocus;
		if (flag2)
		{
			TField.currentTField = this;
		}
		else
		{
			bool flag3 = TField.currentTField == this;
			if (flag3)
			{
				TField.currentTField = null;
			}
		}
		bool flag4 = Thread.CurrentThread.Name == Main.mainThreadName && TField.currentTField != null;
		if (flag4)
		{
			isFocus = true;
			TouchScreenKeyboard.hideInput = !TField.currentTField.showSubTextField;
			TouchScreenKeyboardType t = TouchScreenKeyboardType.ASCIICapable;
			bool flag5 = this.inputType == TField.INPUT_TYPE_NUMERIC;
			if (flag5)
			{
				t = TouchScreenKeyboardType.NumberPad;
			}
			bool type = false;
			bool flag6 = this.inputType == TField.INPUT_TYPE_PASSWORD;
			if (flag6)
			{
				type = true;
			}
			TField.kb = TouchScreenKeyboard.Open(TField.currentTField.text, t, false, false, type, false, TField.currentTField.name);
			bool flag7 = TField.kb != null;
			if (flag7)
			{
				TField.kb.text = TField.currentTField.text;
			}
			Cout.LogWarning("SHOW KEYBOARD FOR " + TField.currentTField.text);
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x000A53C8 File Offset: 0x000A35C8
	public string getText()
	{
		return this.text;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x000A53E0 File Offset: 0x000A35E0
	public void clearKb()
	{
		bool flag = TField.kb != null;
		if (flag)
		{
			TField.kb.text = string.Empty;
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x000A540C File Offset: 0x000A360C
	public void setText(string text)
	{
		bool flag = text == null;
		if (!flag)
		{
			TField.lastKey = -1984;
			this.keyInActiveState = 0;
			this.indexOfActiveChar = 0;
			this.text = text;
			this.paintedText = text;
			bool flag2 = text == string.Empty;
			if (flag2)
			{
				TouchScreenKeyboard.Clear();
			}
			this.setPasswordTest();
			this.caretPos = text.Length;
			this.setOffset();
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x000A547C File Offset: 0x000A367C
	public void insertText(string text)
	{
		this.text = this.text.Substring(0, this.caretPos) + text + this.text.Substring(this.caretPos);
		this.setPasswordTest();
		this.caretPos += text.Length;
		this.setOffset();
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000A54DC File Offset: 0x000A36DC
	public int getMaxTextLenght()
	{
		return this.maxTextLenght;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x000A54F4 File Offset: 0x000A36F4
	public void setMaxTextLenght(int maxTextLenght)
	{
		this.maxTextLenght = maxTextLenght;
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x000A5500 File Offset: 0x000A3700
	public int getIputType()
	{
		return this.inputType;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x000A5518 File Offset: 0x000A3718
	public void setIputType(int iputType)
	{
		this.inputType = iputType;
		this.setMaxTextLenght(500);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x000A5530 File Offset: 0x000A3730
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1000;
		if (flag)
		{
			this.clear();
		}
	}

	// Token: 0x0400126B RID: 4715
	public bool isFocus;

	// Token: 0x0400126C RID: 4716
	public int x;

	// Token: 0x0400126D RID: 4717
	public int y;

	// Token: 0x0400126E RID: 4718
	public int width;

	// Token: 0x0400126F RID: 4719
	public int height;

	// Token: 0x04001270 RID: 4720
	public bool lockArrow;

	// Token: 0x04001271 RID: 4721
	public bool justReturnFromTextBox;

	// Token: 0x04001272 RID: 4722
	public bool paintFocus = true;

	// Token: 0x04001273 RID: 4723
	public const sbyte KEY_LEFT = 14;

	// Token: 0x04001274 RID: 4724
	public const sbyte KEY_RIGHT = 15;

	// Token: 0x04001275 RID: 4725
	public const sbyte KEY_CLEAR = 19;

	// Token: 0x04001276 RID: 4726
	public static int typeXpeed = 2;

	// Token: 0x04001277 RID: 4727
	private static readonly int[] MAX_TIME_TO_CONFIRM_KEY = new int[]
	{
		30,
		14,
		11,
		9,
		6,
		4,
		2
	};

	// Token: 0x04001278 RID: 4728
	private static int CARET_HEIGHT = 0;

	// Token: 0x04001279 RID: 4729
	private static readonly int CARET_WIDTH = 1;

	// Token: 0x0400127A RID: 4730
	private static readonly int CARET_SHOWING_TIME = 5;

	// Token: 0x0400127B RID: 4731
	private static readonly int TEXT_GAP_X = 4;

	// Token: 0x0400127C RID: 4732
	private static readonly int MAX_SHOW_CARET_COUNER = 10;

	// Token: 0x0400127D RID: 4733
	public static readonly int INPUT_TYPE_ANY = 0;

	// Token: 0x0400127E RID: 4734
	public static readonly int INPUT_TYPE_NUMERIC = 1;

	// Token: 0x0400127F RID: 4735
	public static readonly int INPUT_TYPE_PASSWORD = 2;

	// Token: 0x04001280 RID: 4736
	public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

	// Token: 0x04001281 RID: 4737
	private static string[] print = new string[]
	{
		" 0",
		".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1",
		"abc2áàảãạâấầẩẫậăắằẳẵặ2",
		"def3đéèẻẽẹêếềểễệ3",
		"ghi4íìỉĩị4",
		"jkl5",
		"mno6óòỏõọôốồổỗộơớờởỡợ6",
		"pqrs7",
		"tuv8úùủũụưứừửữự8",
		"wxyz9ýỳỷỹỵ9",
		"*",
		"#"
	};

	// Token: 0x04001282 RID: 4738
	private static string[] printA = new string[]
	{
		"0",
		"1",
		"abc2",
		"def3",
		"ghi4",
		"jkl5",
		"mno6",
		"pqrs7",
		"tuv8",
		"wxyz9",
		"0",
		"0"
	};

	// Token: 0x04001283 RID: 4739
	private static string[] printBB = new string[]
	{
		" 0",
		"er1",
		"ty2",
		"ui3",
		"df4",
		"gh5",
		"jk6",
		"cv7",
		"bn8",
		"m9",
		"0",
		"0",
		"qw!",
		"as?",
		"zx",
		"op.",
		"l,"
	};

	// Token: 0x04001284 RID: 4740
	private string text = string.Empty;

	// Token: 0x04001285 RID: 4741
	private string passwordText = string.Empty;

	// Token: 0x04001286 RID: 4742
	private string paintedText = string.Empty;

	// Token: 0x04001287 RID: 4743
	private int caretPos;

	// Token: 0x04001288 RID: 4744
	private int counter;

	// Token: 0x04001289 RID: 4745
	private int maxTextLenght = 500;

	// Token: 0x0400128A RID: 4746
	private int offsetX;

	// Token: 0x0400128B RID: 4747
	private static int lastKey = -1984;

	// Token: 0x0400128C RID: 4748
	private int keyInActiveState;

	// Token: 0x0400128D RID: 4749
	private int indexOfActiveChar;

	// Token: 0x0400128E RID: 4750
	private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

	// Token: 0x0400128F RID: 4751
	private int inputType = TField.INPUT_TYPE_ANY;

	// Token: 0x04001290 RID: 4752
	public static bool isQwerty = true;

	// Token: 0x04001291 RID: 4753
	public static int typingModeAreaWidth;

	// Token: 0x04001292 RID: 4754
	public static int mode = 0;

	// Token: 0x04001293 RID: 4755
	public static long timeChangeMode;

	// Token: 0x04001294 RID: 4756
	public static readonly string[] modeNotify = new string[]
	{
		"abc",
		"Abc",
		"ABC",
		"123"
	};

	// Token: 0x04001295 RID: 4757
	public static readonly int NOKIA = 0;

	// Token: 0x04001296 RID: 4758
	public static readonly int MOTO = 1;

	// Token: 0x04001297 RID: 4759
	public static readonly int ORTHER = 2;

	// Token: 0x04001298 RID: 4760
	public static readonly int BB = 3;

	// Token: 0x04001299 RID: 4761
	public static int changeModeKey = 11;

	// Token: 0x0400129A RID: 4762
	public static readonly sbyte abc = 0;

	// Token: 0x0400129B RID: 4763
	public static readonly sbyte Abc = 1;

	// Token: 0x0400129C RID: 4764
	public static readonly sbyte ABC = 2;

	// Token: 0x0400129D RID: 4765
	public static readonly sbyte number123 = 3;

	// Token: 0x0400129E RID: 4766
	public static TField currentTField;

	// Token: 0x0400129F RID: 4767
	public bool isTfield;

	// Token: 0x040012A0 RID: 4768
	public bool isPaintMouse = true;

	// Token: 0x040012A1 RID: 4769
	public string name = string.Empty;

	// Token: 0x040012A2 RID: 4770
	public string title = string.Empty;

	// Token: 0x040012A3 RID: 4771
	public string strInfo;

	// Token: 0x040012A4 RID: 4772
	public Command cmdClear;

	// Token: 0x040012A5 RID: 4773
	public Command cmdDoneAction;

	// Token: 0x040012A6 RID: 4774
	private mScreen parentScr;

	// Token: 0x040012A7 RID: 4775
	private int timeDelayKyCode;

	// Token: 0x040012A8 RID: 4776
	private int holdCount;

	// Token: 0x040012A9 RID: 4777
	public static int changeDau;

	// Token: 0x040012AA RID: 4778
	private int indexDau = -1;

	// Token: 0x040012AB RID: 4779
	private int indexTemplate;

	// Token: 0x040012AC RID: 4780
	private int indexCong;

	// Token: 0x040012AD RID: 4781
	private long timeDau;

	// Token: 0x040012AE RID: 4782
	private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

	// Token: 0x040012AF RID: 4783
	public static Image imgTf;

	// Token: 0x040012B0 RID: 4784
	public int timePutKeyClearAll;

	// Token: 0x040012B1 RID: 4785
	public int timeClearFirt;

	// Token: 0x040012B2 RID: 4786
	public bool isPaintCarret;

	// Token: 0x040012B3 RID: 4787
	public bool showSubTextField = true;

	// Token: 0x040012B4 RID: 4788
	public static TouchScreenKeyboard kb;

	// Token: 0x040012B5 RID: 4789
	public static int[][] BBKEY = new int[][]
	{
		new int[]
		{
			32,
			48
		},
		new int[]
		{
			49,
			69
		},
		new int[]
		{
			50,
			84
		},
		new int[]
		{
			51,
			85
		},
		new int[]
		{
			52,
			68
		},
		new int[]
		{
			53,
			71
		},
		new int[]
		{
			54,
			74
		},
		new int[]
		{
			55,
			67
		},
		new int[]
		{
			56,
			66
		},
		new int[]
		{
			57,
			77
		},
		new int[]
		{
			42,
			128
		},
		new int[]
		{
			35,
			137
		},
		new int[]
		{
			33,
			113
		},
		new int[]
		{
			63,
			97
		},
		new int[]
		{
			64,
			121,
			122
		},
		new int[]
		{
			46,
			111
		},
		new int[]
		{
			44,
			108
		}
	};
}
