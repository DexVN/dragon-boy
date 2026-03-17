using System;
using System.Collections;
using Assets.src.e;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class mGraphics
{
	// Token: 0x06000515 RID: 1301 RVA: 0x000607F8 File Offset: 0x0005E9F8
	private void cache(string key, Texture value)
	{
		bool flag = mGraphics.cachedTextures.Count > 400;
		if (flag)
		{
			mGraphics.cachedTextures.Clear();
		}
		bool flag2 = value.width * value.height < GameCanvas.w * GameCanvas.h;
		if (flag2)
		{
			mGraphics.cachedTextures.Add(key, value);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00060858 File Offset: 0x0005EA58
	public void translate(int tx, int ty)
	{
		tx *= mGraphics.zoomLevel;
		ty *= mGraphics.zoomLevel;
		this.translateX += tx;
		this.translateY += ty;
		this.isTranslate = true;
		bool flag = this.translateX == 0 && this.translateY == 0;
		if (flag)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x000608BC File Offset: 0x0005EABC
	public void translate(float x, float y)
	{
		this.translateXf += x;
		this.translateYf += y;
		this.isTranslate = true;
		bool flag = this.translateXf == 0f && this.translateYf == 0f;
		if (flag)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00060918 File Offset: 0x0005EB18
	public int getTranslateX()
	{
		return this.translateX / mGraphics.zoomLevel;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00060938 File Offset: 0x0005EB38
	public int getTranslateY()
	{
		return this.translateY / mGraphics.zoomLevel + mGraphics.addYWhenOpenKeyBoard;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0006095C File Offset: 0x0005EB5C
	public void setClip(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		this.clipTX = this.translateX;
		this.clipTY = this.translateY;
		this.clipX = x;
		this.clipY = y;
		this.clipW = w;
		this.clipH = h;
		this.isClip = true;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000609CC File Offset: 0x0005EBCC
	public int getClipX()
	{
		return GameScr.cmx;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000609E4 File Offset: 0x0005EBE4
	public int getClipY()
	{
		return GameScr.cmy;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x000609FC File Offset: 0x0005EBFC
	public int getClipWidth()
	{
		return GameScr.gW;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00060A14 File Offset: 0x0005EC14
	public int getClipHeight()
	{
		return GameScr.gH;
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00060A2C File Offset: 0x0005EC2C
	public void fillRect(int x, int y, int w, int h, int color, int alpha)
	{
		float alpha2 = 0.5f;
		this.setColor(color, alpha2);
		this.fillRect(x, y, w, h);
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00060A58 File Offset: 0x0005EC58
	public void drawLine(int x1, int y1, int x2, int y2)
	{
		x1 *= mGraphics.zoomLevel;
		y1 *= mGraphics.zoomLevel;
		x2 *= mGraphics.zoomLevel;
		y2 *= mGraphics.zoomLevel;
		bool flag = y1 == y2;
		if (flag)
		{
			bool flag2 = x1 > x2;
			if (flag2)
			{
				int num = x2;
				x2 = x1;
				x1 = num;
			}
			this.fillRect(x1, y1, x2 - x1, 1);
		}
		else
		{
			bool flag3 = x1 == x2;
			if (flag3)
			{
				bool flag4 = y1 > y2;
				if (flag4)
				{
					int num2 = y2;
					y2 = y1;
					y1 = num2;
				}
				this.fillRect(x1, y1, 1, y2 - y1);
			}
			else
			{
				bool flag5 = this.isTranslate;
				if (flag5)
				{
					x1 += this.translateX;
					y1 += this.translateY;
					x2 += this.translateX;
					y2 += this.translateY;
				}
				string key = string.Concat(new object[]
				{
					"dl",
					this.r,
					this.g,
					this.b
				});
				Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
				bool flag6 = texture2D == null;
				if (flag6)
				{
					texture2D = new Texture2D(1, 1);
					Color color = new Color(this.r, this.g, this.b);
					texture2D.SetPixel(0, 0, color);
					texture2D.Apply();
					this.cache(key, texture2D);
				}
				Vector2 pivotPoint = new Vector2((float)x1, (float)y1);
				Vector2 vector = new Vector2((float)x2, (float)y2);
				Vector2 vector2 = vector - pivotPoint;
				float num3 = 57.29578f * Mathf.Atan(vector2.y / vector2.x);
				bool flag7 = vector2.x < 0f;
				if (flag7)
				{
					num3 += 180f;
				}
				int num4 = (int)Mathf.Ceil(0f);
				GUIUtility.RotateAroundPivot(num3, pivotPoint);
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				bool flag8 = this.isClip;
				if (flag8)
				{
					num5 = this.clipX;
					num6 = this.clipY;
					num7 = this.clipW;
					num8 = this.clipH;
					bool flag9 = this.isTranslate;
					if (flag9)
					{
						num5 += this.clipTX;
						num6 += this.clipTY;
					}
				}
				bool flag10 = this.isClip;
				if (flag10)
				{
					GUI.BeginGroup(new Rect((float)num5, (float)num6, (float)num7, (float)num8));
				}
				Graphics.DrawTexture(new Rect(pivotPoint.x - (float)num5, pivotPoint.y - (float)num4 - (float)num6, vector2.magnitude, 1f), texture2D);
				bool flag11 = this.isClip;
				if (flag11)
				{
					GUI.EndGroup();
				}
				GUIUtility.RotateAroundPivot(-num3, pivotPoint);
			}
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00060D0C File Offset: 0x0005EF0C
	public Color setColorMiniMap(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color result = new Color(num6, num5, num4);
		return result;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00060D68 File Offset: 0x0005EF68
	public float[] getRGB(Color cl)
	{
		float num = 256f * cl.r;
		float num2 = 256f * cl.g;
		float num3 = 256f * cl.b;
		return new float[]
		{
			num,
			num2,
			num3
		};
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00060DB4 File Offset: 0x0005EFB4
	public void drawRect(int x, int y, int w, int h)
	{
		int num = 1;
		this.fillRect(x, y, w, num);
		this.fillRect(x, y, num, h);
		this.fillRect(x + w, y, num, h + 1);
		this.fillRect(x, y + h, w + 1, num);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00060DFC File Offset: 0x0005EFFC
	public void fillRect(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		bool flag = w < 0 || h < 0;
		if (!flag)
		{
			bool flag2 = this.isTranslate;
			if (flag2)
			{
				x += this.translateX;
				y += this.translateY;
			}
			int num = 1;
			int num2 = 1;
			string key = string.Concat(new object[]
			{
				"fr",
				num,
				num2,
				this.r,
				this.g,
				this.b,
				this.a
			});
			Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
			bool flag3 = texture2D == null;
			if (flag3)
			{
				texture2D = new Texture2D(num, num2);
				Color color = new Color(this.r, this.g, this.b, this.a);
				texture2D.SetPixel(0, 0, color);
				texture2D.Apply();
				this.cache(key, texture2D);
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			bool flag4 = this.isClip;
			if (flag4)
			{
				num3 = this.clipX;
				num4 = this.clipY;
				num5 = this.clipW;
				num6 = this.clipH;
				bool flag5 = this.isTranslate;
				if (flag5)
				{
					num3 += this.clipTX;
					num4 += this.clipTY;
				}
			}
			bool flag6 = this.isClip;
			if (flag6)
			{
				GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
			}
			GUI.DrawTexture(new Rect((float)(x - num3), (float)(y - num4), (float)w, (float)h), texture2D);
			bool flag7 = this.isClip;
			if (flag7)
			{
				GUI.EndGroup();
			}
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00060FE0 File Offset: 0x0005F1E0
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00061040 File Offset: 0x0005F240
	public void setColor(Color color)
	{
		this.b = color.b;
		this.g = color.g;
		this.r = color.r;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00061068 File Offset: 0x0005F268
	public void setBgColor(int rgb)
	{
		bool flag = rgb != this.currentBGColor;
		if (flag)
		{
			this.currentBGColor = rgb;
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			int num3 = rgb >> 16 & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			Main.main.GetComponent<Camera>().backgroundColor = new Color(this.r, this.g, this.b);
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00061100 File Offset: 0x0005F300
	public void drawString(string s, int x, int y, GUIStyle style)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		bool flag = this.isTranslate;
		if (flag)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		bool flag2 = this.isClip;
		if (flag2)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			bool flag3 = this.isTranslate;
			if (flag3)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		bool flag4 = this.isClip;
		if (flag4)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
		bool flag5 = this.isClip;
		if (flag5)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000611EC File Offset: 0x0005F3EC
	public void setColor(int rgb, float alpha)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = alpha;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00061248 File Offset: 0x0005F448
	public void drawString(string s, int x, int y, GUIStyle style, int w)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		bool flag = this.isTranslate;
		if (flag)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		bool flag2 = this.isClip;
		if (flag2)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			bool flag3 = this.isTranslate;
			if (flag3)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		bool flag4 = this.isClip;
		if (flag4)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2 - 4), (float)w, 100f), s, style);
		bool flag5 = this.isClip;
		if (flag5)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00061334 File Offset: 0x0005F534
	private void UpdatePos(int anchor)
	{
		Vector2 vector = new Vector2(0f, 0f);
		if (anchor != 3)
		{
			if (anchor != 6)
			{
				if (anchor != 17)
				{
					if (anchor != 20)
					{
						if (anchor != 33)
						{
							if (anchor != 36)
							{
								bool flag = anchor != 10;
								if (flag)
								{
									bool flag2 = anchor != 24;
									if (flag2)
									{
										bool flag3 = anchor == 40;
										if (flag3)
										{
											vector = new Vector2((float)Screen.width, (float)Screen.height);
										}
									}
									else
									{
										vector = new Vector2((float)Screen.width, 0f);
									}
								}
								else
								{
									vector = new Vector2((float)Screen.width, (float)(Screen.height / 2));
								}
							}
							else
							{
								vector = new Vector2(0f, (float)Screen.height);
							}
						}
						else
						{
							vector = new Vector2((float)(Screen.width / 2), (float)Screen.height);
						}
					}
					else
					{
						vector = new Vector2(0f, 0f);
					}
				}
				else
				{
					vector = new Vector2((float)(Screen.width / 2), 0f);
				}
			}
			else
			{
				vector = new Vector2(0f, (float)(Screen.height / 2));
			}
		}
		else
		{
			vector = new Vector2(this.size.x / 2f, this.size.y / 2f);
		}
		this.pos = vector + this.relativePosition;
		this.rect = new Rect(this.pos.x - this.size.x * 0.5f, this.pos.y - this.size.y * 0.5f, this.size.x, this.size.y);
		this.pivot = new Vector2(this.rect.xMin + this.rect.width * 0.5f, this.rect.yMin + this.rect.height * 0.5f);
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0006155C File Offset: 0x0005F75C
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8)
	{
		bool flag = arg0 == null;
		if (!flag)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			x0 *= mGraphics.zoomLevel;
			y0 *= mGraphics.zoomLevel;
			w0 *= mGraphics.zoomLevel;
			h0 *= mGraphics.zoomLevel;
			this._drawRegion(arg0, (float)x0, (float)y0, w0, h0, arg5, x, y, arg8);
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000615C8 File Offset: 0x0005F7C8
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, float x, float y, int arg8)
	{
		bool flag = arg0 == null;
		if (!flag)
		{
			x *= (float)mGraphics.zoomLevel;
			y *= (float)mGraphics.zoomLevel;
			x0 *= mGraphics.zoomLevel;
			y0 *= mGraphics.zoomLevel;
			w0 *= mGraphics.zoomLevel;
			h0 *= mGraphics.zoomLevel;
			this.__drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00061634 File Offset: 0x0005F834
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isClip)
	{
		this.drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00061658 File Offset: 0x0005F858
	public void __drawRegion(Image image, int x0, int y0, int w, int h, int transform, float x, float y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			bool flag2 = this.isTranslate;
			if (flag2)
			{
				x += (float)this.translateX;
				y += (float)this.translateY;
			}
			float num = (float)w;
			float num2 = (float)h;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 0f;
			int num9 = 1;
			bool flag3 = (anchor & mGraphics.HCENTER) == mGraphics.HCENTER;
			if (flag3)
			{
				num5 -= num / 2f;
			}
			bool flag4 = (anchor & mGraphics.VCENTER) == mGraphics.VCENTER;
			if (flag4)
			{
				num6 -= num2 / 2f;
			}
			bool flag5 = (anchor & mGraphics.RIGHT) == mGraphics.RIGHT;
			if (flag5)
			{
				num5 -= num;
			}
			bool flag6 = (anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM;
			if (flag6)
			{
				num6 -= num2;
			}
			x += num5;
			y += num6;
			int num10 = 0;
			int num11 = 0;
			bool flag7 = this.isClip;
			if (flag7)
			{
				num10 = this.clipX;
				int num12 = this.clipY;
				num11 = this.clipW;
				int num13 = this.clipH;
				bool flag8 = this.isTranslate;
				if (flag8)
				{
					num10 += this.clipTX;
					num12 += this.clipTY;
				}
				Rect r = new Rect(x, y, (float)w, (float)h);
				Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
				Rect rect = this.intersectRect(r, r2);
				bool flag9 = rect.width <= 0f || rect.height <= 0f;
				if (flag9)
				{
					return;
				}
				num = rect.width;
				num2 = rect.height;
				num3 = rect.x - r.x;
				num4 = rect.y - r.y;
			}
			float num14 = 0f;
			float num15 = 0f;
			bool flag10 = transform == 2;
			if (flag10)
			{
				num14 += num;
				num7 = -1f;
				bool flag11 = this.isClip;
				if (flag11)
				{
					bool flag12 = (float)num10 > x;
					if (flag12)
					{
						num8 = -num3;
					}
					else
					{
						bool flag13 = (float)(num10 + num11) < x + (float)w;
						if (flag13)
						{
							num8 = -((float)(num10 + num11) - x - (float)w);
						}
					}
				}
			}
			else
			{
				bool flag14 = transform == 1;
				if (flag14)
				{
					num9 = -1;
					num15 += num2;
				}
				else
				{
					bool flag15 = transform == 3;
					if (flag15)
					{
						num9 = -1;
						num15 += num2;
						num7 = -1f;
						num14 += num;
					}
				}
			}
			int num16 = 0;
			int num17 = 0;
			bool flag16 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag16)
			{
				this.matrixBackup = GUI.matrix;
				this.size = new Vector2((float)w, (float)h);
				this.relativePosition = new Vector2(x, y);
				this.UpdatePos(3);
				bool flag17 = transform == 6;
				if (flag17)
				{
					this.UpdatePos(3);
				}
				else
				{
					bool flag18 = transform == 5;
					if (flag18)
					{
						this.size = new Vector2((float)w, (float)h);
						this.UpdatePos(3);
					}
				}
				bool flag19 = transform == 5;
				if (flag19)
				{
					GUIUtility.RotateAroundPivot(90f, this.pivot);
				}
				else
				{
					bool flag20 = transform == 6;
					if (flag20)
					{
						GUIUtility.RotateAroundPivot(270f, this.pivot);
					}
					else
					{
						bool flag21 = transform == 4;
						if (flag21)
						{
							GUIUtility.RotateAroundPivot(270f, this.pivot);
							num14 += num;
							num7 = -1f;
							bool flag22 = this.isClip;
							if (flag22)
							{
								bool flag23 = (float)num10 > x;
								if (flag23)
								{
									num8 = -num3;
								}
								else
								{
									bool flag24 = (float)(num10 + num11) < x + (float)w;
									if (flag24)
									{
										num8 = -((float)(num10 + num11) - x - (float)w);
									}
								}
							}
						}
						else
						{
							bool flag25 = transform == 7;
							if (flag25)
							{
								GUIUtility.RotateAroundPivot(270f, this.pivot);
								num9 = -1;
								num15 += num2;
							}
						}
					}
				}
			}
			Graphics.DrawTexture(new Rect(x + num3 + num14 + (float)num16, y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect(((float)x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - ((float)y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
			bool flag26 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag26)
			{
				GUI.matrix = this.matrixBackup;
			}
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00061B24 File Offset: 0x0005FD24
	public void _drawRegion(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			bool flag2 = this.isTranslate;
			if (flag2)
			{
				x += this.translateX;
				y += this.translateY;
			}
			float num = (float)w;
			float num2 = (float)h;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 0f;
			int num9 = 1;
			bool flag3 = (anchor & mGraphics.HCENTER) == mGraphics.HCENTER;
			if (flag3)
			{
				num5 -= num / 2f;
			}
			bool flag4 = (anchor & mGraphics.VCENTER) == mGraphics.VCENTER;
			if (flag4)
			{
				num6 -= num2 / 2f;
			}
			bool flag5 = (anchor & mGraphics.RIGHT) == mGraphics.RIGHT;
			if (flag5)
			{
				num5 -= num;
			}
			bool flag6 = (anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM;
			if (flag6)
			{
				num6 -= num2;
			}
			x += (int)num5;
			y += (int)num6;
			int num10 = 0;
			int num11 = 0;
			bool flag7 = this.isClip;
			if (flag7)
			{
				num10 = this.clipX;
				int num12 = this.clipY;
				num11 = this.clipW;
				int num13 = this.clipH;
				bool flag8 = this.isTranslate;
				if (flag8)
				{
					num10 += this.clipTX;
					num12 += this.clipTY;
				}
				Rect r = new Rect((float)x, (float)y, (float)w, (float)h);
				Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
				Rect rect = this.intersectRect(r, r2);
				bool flag9 = rect.width <= 0f || rect.height <= 0f;
				if (flag9)
				{
					return;
				}
				num = rect.width;
				num2 = rect.height;
				num3 = rect.x - r.x;
				num4 = rect.y - r.y;
			}
			float num14 = 0f;
			float num15 = 0f;
			bool flag10 = transform == 2;
			if (flag10)
			{
				num14 += num;
				num7 = -1f;
				bool flag11 = this.isClip;
				if (flag11)
				{
					bool flag12 = num10 > x;
					if (flag12)
					{
						num8 = -num3;
					}
					else
					{
						bool flag13 = num10 + num11 < x + w;
						if (flag13)
						{
							num8 = -(float)(num10 + num11 - x - w);
						}
					}
				}
			}
			else
			{
				bool flag14 = transform == 1;
				if (flag14)
				{
					num9 = -1;
					num15 += num2;
				}
				else
				{
					bool flag15 = transform == 3;
					if (flag15)
					{
						num9 = -1;
						num15 += num2;
						num7 = -1f;
						num14 += num;
					}
				}
			}
			int num16 = 0;
			int num17 = 0;
			bool flag16 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag16)
			{
				this.matrixBackup = GUI.matrix;
				this.size = new Vector2((float)w, (float)h);
				this.relativePosition = new Vector2((float)x, (float)y);
				this.UpdatePos(3);
				bool flag17 = transform == 6;
				if (flag17)
				{
					this.UpdatePos(3);
				}
				else
				{
					bool flag18 = transform == 5;
					if (flag18)
					{
						this.size = new Vector2((float)w, (float)h);
						this.UpdatePos(3);
					}
				}
				bool flag19 = transform == 5;
				if (flag19)
				{
					GUIUtility.RotateAroundPivot(90f, this.pivot);
				}
				else
				{
					bool flag20 = transform == 6;
					if (flag20)
					{
						GUIUtility.RotateAroundPivot(270f, this.pivot);
					}
					else
					{
						bool flag21 = transform == 4;
						if (flag21)
						{
							GUIUtility.RotateAroundPivot(270f, this.pivot);
							num14 += num;
							num7 = -1f;
							bool flag22 = this.isClip;
							if (flag22)
							{
								bool flag23 = num10 > x;
								if (flag23)
								{
									num8 = -num3;
								}
								else
								{
									bool flag24 = num10 + num11 < x + w;
									if (flag24)
									{
										num8 = -(float)(num10 + num11 - x - w);
									}
								}
							}
						}
						else
						{
							bool flag25 = transform == 7;
							if (flag25)
							{
								GUIUtility.RotateAroundPivot(270f, this.pivot);
								num9 = -1;
								num15 += num2;
							}
						}
					}
				}
			}
			Graphics.DrawTexture(new Rect((float)x + num3 + num14 + (float)num16, (float)y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect((x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
			bool flag26 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag26)
			{
				GUI.matrix = this.matrixBackup;
			}
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00061FF0 File Offset: 0x000601F0
	public void drawRegionGui(Image image, float x0, float y0, int w, int h, int transform, float x, float y, int anchor)
	{
		GUI.color = this.setColorMiniMap(807956);
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		x0 *= (float)mGraphics.zoomLevel;
		y0 *= (float)mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00062050 File Offset: 0x00060250
	public void drawRegion2(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		GUI.color = image.colorBlend;
		bool flag = this.isTranslate;
		if (flag)
		{
			x += this.translateX;
			y += this.translateY;
		}
		string key = string.Concat(new object[]
		{
			"dg",
			x0,
			y0,
			w,
			h,
			transform,
			image.GetHashCode()
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
		bool flag2 = texture2D == null;
		if (flag2)
		{
			Image image2 = Image.createImage(image, (int)x0, (int)y0, w, h, transform);
			texture2D = image2.texture;
			this.cache(key, texture2D);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		float num5 = (float)w;
		float num6 = (float)h;
		float num7 = 0f;
		float num8 = 0f;
		bool flag3 = (anchor & mGraphics.HCENTER) == mGraphics.HCENTER;
		if (flag3)
		{
			num7 -= num5 / 2f;
		}
		bool flag4 = (anchor & mGraphics.VCENTER) == mGraphics.VCENTER;
		if (flag4)
		{
			num8 -= num6 / 2f;
		}
		bool flag5 = (anchor & mGraphics.RIGHT) == mGraphics.RIGHT;
		if (flag5)
		{
			num7 -= num5;
		}
		bool flag6 = (anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM;
		if (flag6)
		{
			num8 -= num6;
		}
		x += (int)num7;
		y += (int)num8;
		bool flag7 = this.isClip;
		if (flag7)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			bool flag8 = this.isTranslate;
			if (flag8)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		bool flag9 = this.isClip;
		if (flag9)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.DrawTexture(new Rect((float)(x - num), (float)(y - num2), (float)w, (float)h), texture2D);
		bool flag10 = this.isClip;
		if (flag10)
		{
			GUI.EndGroup();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00062298 File Offset: 0x00060498
	public void drawImagaByDrawTexture(Image image, float x, float y)
	{
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		GUI.DrawTexture(new Rect(x + (float)this.translateX, y + (float)this.translateY, (float)image.getRealImageWidth(), (float)image.getRealImageHeight()), image.texture);
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x000622EC File Offset: 0x000604EC
	public void drawImage(Image image, int x, int y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
		}
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00062320 File Offset: 0x00060520
	public void drawImageFog(Image image, int x, int y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.drawRegion(image, 0, 0, image.texture.width, image.texture.height, 0, x, y, anchor);
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00062360 File Offset: 0x00060560
	public void drawImage(Image image, int x, int y)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, mGraphics.TOP | mGraphics.LEFT);
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000623A0 File Offset: 0x000605A0
	public void drawImage(Image image, float x, float y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000623D4 File Offset: 0x000605D4
	public void drawRoundRect(int x, int y, int w, int h, int arcWidth, int arcHeight)
	{
		this.drawRect(x, y, w, h);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x000623E3 File Offset: 0x000605E3
	public void fillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
	{
		this.fillRect(x, y, width, height);
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x000623F2 File Offset: 0x000605F2
	public void reset()
	{
		this.isClip = false;
		this.isTranslate = false;
		this.translateX = 0;
		this.translateY = 0;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00062414 File Offset: 0x00060614
	public Rect intersectRect(Rect r1, Rect r2)
	{
		float num = r1.x;
		float num2 = r1.y;
		float x = r2.x;
		float y = r2.y;
		float num3 = num;
		num3 += r1.width;
		float num4 = num2;
		num4 += r1.height;
		float num5 = x;
		num5 += r2.width;
		float num6 = y;
		num6 += r2.height;
		bool flag = num < x;
		if (flag)
		{
			num = x;
		}
		bool flag2 = num2 < y;
		if (flag2)
		{
			num2 = y;
		}
		bool flag3 = num3 > num5;
		if (flag3)
		{
			num3 = num5;
		}
		bool flag4 = num4 > num6;
		if (flag4)
		{
			num4 = num6;
		}
		num3 -= num;
		num4 -= num2;
		bool flag5 = num3 < -30000f;
		if (flag5)
		{
			num3 = -30000f;
		}
		bool flag6 = num4 < -30000f;
		if (flag6)
		{
			num4 = -30000f;
		}
		return new Rect(num, num2, (float)((int)num3), (float)((int)num4));
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00062510 File Offset: 0x00060710
	public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
	{
		GUI.color = Color.red;
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		bool flag = image != null;
		if (flag)
		{
			Graphics.DrawTexture(new Rect((float)(x + this.translateX), (float)(y + this.translateY), (tranform != 0) ? (-(float)w) : ((float)w), (float)h), image.texture);
		}
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00062590 File Offset: 0x00060790
	public void drawImageSimple(Image image, int x, int y)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		bool flag = image != null;
		if (flag)
		{
			Graphics.DrawTexture(new Rect((float)x, (float)y, (float)image.w, (float)image.h), image.texture);
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x000625E0 File Offset: 0x000607E0
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x000625F8 File Offset: 0x000607F8
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00062610 File Offset: 0x00060810
	public static bool isNotTranColor(Color color)
	{
		return !(color == Color.clear) && !(color == mGraphics.transParentColor);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00062640 File Offset: 0x00060840
	public static Image blend(Image img0, float level, int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color color = new Color(num6, num5, num4);
		Color[] pixels = img0.texture.GetPixels();
		float num7 = color.r;
		float num8 = color.g;
		float num9 = color.b;
		for (int i = 0; i < pixels.Length; i++)
		{
			Color color2 = pixels[i];
			bool flag = mGraphics.isNotTranColor(color2);
			if (flag)
			{
				float num10 = (num7 - color2.r) * level + color2.r;
				float num11 = (num8 - color2.g) * level + color2.g;
				float num12 = (num9 - color2.b) * level + color2.b;
				bool flag2 = num10 > 255f;
				if (flag2)
				{
					num10 = 255f;
				}
				bool flag3 = num10 < 0f;
				if (flag3)
				{
					num10 = 0f;
				}
				bool flag4 = num11 > 255f;
				if (flag4)
				{
					num11 = 255f;
				}
				bool flag5 = num11 < 0f;
				if (flag5)
				{
					num11 = 0f;
				}
				bool flag6 = num12 < 0f;
				if (flag6)
				{
					num12 = 0f;
				}
				bool flag7 = num12 > 255f;
				if (flag7)
				{
					num12 = 255f;
				}
				pixels[i].r = num10;
				pixels[i].g = num11;
				pixels[i].b = num12;
			}
		}
		Image image = Image.createImage(img0.getRealImageWidth(), img0.getRealImageHeight());
		image.texture.SetPixels(pixels);
		Image.setTextureQuality(image.texture);
		image.texture.Apply();
		Cout.LogError2("BLEND ----------------------------------------------------");
		return image;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0006284C File Offset: 0x00060A4C
	public static Color setColorObj(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color result = new Color(num6, num5, num4);
		return result;
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x000628A8 File Offset: 0x00060AA8
	public void fillTrans(Image imgTrans, int x, int y, int w, int h)
	{
		this.setColor(0, 0.5f);
		this.fillRect(x * mGraphics.zoomLevel, y * mGraphics.zoomLevel, w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x000628E0 File Offset: 0x00060AE0
	public static int blendColor(float level, int color, int colorBlend)
	{
		Color color2 = mGraphics.setColorObj(colorBlend);
		float num = color2.r * 255f;
		float num2 = color2.g * 255f;
		float num3 = color2.b * 255f;
		Color color3 = mGraphics.setColorObj(color);
		float num4 = (num + color3.r) * level + color3.r;
		float num5 = (num2 + color3.g) * level + color3.g;
		float num6 = (num3 + color3.b) * level + color3.b;
		bool flag = num4 > 255f;
		if (flag)
		{
			num4 = 255f;
		}
		bool flag2 = num4 < 0f;
		if (flag2)
		{
			num4 = 0f;
		}
		bool flag3 = num5 > 255f;
		if (flag3)
		{
			num5 = 255f;
		}
		bool flag4 = num5 < 0f;
		if (flag4)
		{
			num5 = 0f;
		}
		bool flag5 = num6 < 0f;
		if (flag5)
		{
			num6 = 0f;
		}
		bool flag6 = num6 > 255f;
		if (flag6)
		{
			num6 = 255f;
		}
		return (int)num6 & 255 + ((int)num5 << 8) & 255 + ((int)num4 << 16) & 255;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00062A1C File Offset: 0x00060C1C
	public static int getIntByColor(Color cl)
	{
		float num = cl.r * 255f;
		float num2 = cl.b * 255f;
		float num3 = cl.g * 255f;
		return ((int)num & 255) << 16 | ((int)num3 & 255) << 8 | ((int)num2 & 255);
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00062A74 File Offset: 0x00060C74
	public static int getRealImageWidth(Image img)
	{
		return img.w;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00062A8C File Offset: 0x00060C8C
	public static int getRealImageHeight(Image img)
	{
		return img.h;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00062AA4 File Offset: 0x00060CA4
	public void fillArg(int i, int j, int k, int l, int m, int n)
	{
		this.fillRect(i * mGraphics.zoomLevel, j * mGraphics.zoomLevel, k * mGraphics.zoomLevel, l * mGraphics.zoomLevel);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00062ACC File Offset: 0x00060CCC
	public void CreateLineMaterial()
	{
		bool flag = !this.lineMaterial;
		if (flag)
		{
			this.lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {  Blend SrcAlpha OneMinusSrcAlpha  ZWrite Off Cull Off Fog { Mode Off }  BindChannels { Bind \"vertex\", vertex Bind \"color\", color }} } }");
			this.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			this.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00062B20 File Offset: 0x00060D20
	public void drawlineGL(MyVector totalLine)
	{
		this.lineMaterial.SetPass(0);
		GL.PushMatrix();
		GL.Begin(1);
		for (int i = 0; i < totalLine.size(); i++)
		{
			mLine mLine = (mLine)totalLine.elementAt(i);
			GL.Color(new Color(mLine.r, mLine.g, mLine.b, mLine.a));
			int num = mLine.x1 * mGraphics.zoomLevel;
			int num2 = mLine.y1 * mGraphics.zoomLevel;
			int num3 = mLine.x2 * mGraphics.zoomLevel;
			int num4 = mLine.y2 * mGraphics.zoomLevel;
			bool flag = this.isTranslate;
			if (flag)
			{
				num += this.translateX;
				num2 += this.translateY;
				num3 += this.translateX;
				num4 += this.translateY;
			}
			for (int j = 0; j < mGraphics.zoomLevel; j++)
			{
				GL.Vertex(new Vector2((float)(num + j), (float)(num2 + j)));
				GL.Vertex(new Vector2((float)(num3 + j), (float)(num4 + j)));
				bool flag2 = j > 0;
				if (flag2)
				{
					GL.Vertex(new Vector2((float)(num + j), (float)num2));
					GL.Vertex(new Vector2((float)(num3 + j), (float)num4));
					GL.Vertex(new Vector2((float)num, (float)(num2 + j)));
					GL.Vertex(new Vector2((float)num3, (float)(num4 + j)));
				}
			}
		}
		GL.End();
		GL.PopMatrix();
		totalLine.removeAllElements();
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00062CDC File Offset: 0x00060EDC
	public void drawLine(mGraphics g, int x, int y, int xTo, int yTo, int nLine, int color)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < nLine; i++)
		{
			myVector.addElement(new mLine(x, y, xTo + i, yTo + i, color));
		}
		g.drawlineGL(myVector);
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00034931 File Offset: 0x00032B31
	internal void drawRegion(Small img, int p1, int p2, int p3, int p4, int transform, int x, int y, int anchor)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000B1E RID: 2846
	public static int HCENTER = 1;

	// Token: 0x04000B1F RID: 2847
	public static int VCENTER = 2;

	// Token: 0x04000B20 RID: 2848
	public static int LEFT = 4;

	// Token: 0x04000B21 RID: 2849
	public static int RIGHT = 8;

	// Token: 0x04000B22 RID: 2850
	public static int TOP = 16;

	// Token: 0x04000B23 RID: 2851
	public static int BOTTOM = 32;

	// Token: 0x04000B24 RID: 2852
	private float r;

	// Token: 0x04000B25 RID: 2853
	private float g;

	// Token: 0x04000B26 RID: 2854
	private float b;

	// Token: 0x04000B27 RID: 2855
	private float a;

	// Token: 0x04000B28 RID: 2856
	public int clipX;

	// Token: 0x04000B29 RID: 2857
	public int clipY;

	// Token: 0x04000B2A RID: 2858
	public int clipW;

	// Token: 0x04000B2B RID: 2859
	public int clipH;

	// Token: 0x04000B2C RID: 2860
	private bool isClip;

	// Token: 0x04000B2D RID: 2861
	private bool isTranslate = true;

	// Token: 0x04000B2E RID: 2862
	private int translateX;

	// Token: 0x04000B2F RID: 2863
	private int translateY;

	// Token: 0x04000B30 RID: 2864
	private float translateXf;

	// Token: 0x04000B31 RID: 2865
	private float translateYf;

	// Token: 0x04000B32 RID: 2866
	public static int zoomLevel = 1;

	// Token: 0x04000B33 RID: 2867
	public const int BASELINE = 64;

	// Token: 0x04000B34 RID: 2868
	public const int SOLID = 0;

	// Token: 0x04000B35 RID: 2869
	public const int DOTTED = 1;

	// Token: 0x04000B36 RID: 2870
	public const int TRANS_MIRROR = 2;

	// Token: 0x04000B37 RID: 2871
	public const int TRANS_MIRROR_ROT180 = 1;

	// Token: 0x04000B38 RID: 2872
	public const int TRANS_MIRROR_ROT270 = 4;

	// Token: 0x04000B39 RID: 2873
	public const int TRANS_MIRROR_ROT90 = 7;

	// Token: 0x04000B3A RID: 2874
	public const int TRANS_NONE = 0;

	// Token: 0x04000B3B RID: 2875
	public const int TRANS_ROT180 = 3;

	// Token: 0x04000B3C RID: 2876
	public const int TRANS_ROT270 = 6;

	// Token: 0x04000B3D RID: 2877
	public const int TRANS_ROT90 = 5;

	// Token: 0x04000B3E RID: 2878
	public static Hashtable cachedTextures = new Hashtable();

	// Token: 0x04000B3F RID: 2879
	public static int addYWhenOpenKeyBoard;

	// Token: 0x04000B40 RID: 2880
	private int clipTX;

	// Token: 0x04000B41 RID: 2881
	private int clipTY;

	// Token: 0x04000B42 RID: 2882
	private int currentBGColor;

	// Token: 0x04000B43 RID: 2883
	private Vector2 pos = new Vector2(0f, 0f);

	// Token: 0x04000B44 RID: 2884
	private Rect rect;

	// Token: 0x04000B45 RID: 2885
	private Matrix4x4 matrixBackup;

	// Token: 0x04000B46 RID: 2886
	private Vector2 pivot;

	// Token: 0x04000B47 RID: 2887
	public Vector2 size = new Vector2(128f, 128f);

	// Token: 0x04000B48 RID: 2888
	public Vector2 relativePosition = new Vector2(0f, 0f);

	// Token: 0x04000B49 RID: 2889
	public Color clTrans;

	// Token: 0x04000B4A RID: 2890
	public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04000B4B RID: 2891
	private Material lineMaterial;
}
