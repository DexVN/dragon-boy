using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class mFont
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x0005EF44 File Offset: 0x0005D144
	public mFont(string strFont, string pathImage, string pathData, int space)
	{
		try
		{
			this.strFont = strFont;
			this.space = space;
			this.pathImage = pathImage;
			DataInputStream dataInputStream = null;
			this.reloadImage();
			try
			{
				dataInputStream = MyStream.readFile(pathData);
				this.fImages = new int[(int)dataInputStream.readShort()][];
				for (int i = 0; i < this.fImages.Length; i++)
				{
					this.fImages[i] = new int[4];
					this.fImages[i][0] = (int)dataInputStream.readShort();
					this.fImages[i][1] = (int)dataInputStream.readShort();
					this.fImages[i][2] = (int)dataInputStream.readShort();
					this.fImages[i][3] = (int)dataInputStream.readShort();
					this.setHeight(this.fImages[i][3]);
				}
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					ex2.StackTrace.ToString();
				}
			}
		}
		catch (Exception ex3)
		{
			ex3.StackTrace.ToString();
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0005F0A4 File Offset: 0x0005D2A4
	public mFont(sbyte id)
	{
		string text = "chelthm";
		bool flag = (id > 0 && id < 10) || id == 19;
		if (flag)
		{
			this.yAdd = 1;
			text = "barmeneb";
		}
		else
		{
			bool flag2 = id >= 10 && id <= 18;
			if (flag2)
			{
				text = "chelthm";
				this.yAdd = 2;
			}
			else
			{
				bool flag3 = id > 24;
				if (flag3)
				{
					text = "staccato";
				}
			}
		}
		this.id = id;
		text = string.Concat(new object[]
		{
			"FontSys/x",
			mGraphics.zoomLevel,
			"/",
			text
		});
		this.myFont = (Font)Resources.Load(text);
		bool flag4 = id < 25;
		if (flag4)
		{
			this.color1 = this.setColorFont(id);
			this.color2 = this.setColorFont(id);
		}
		else
		{
			this.color1 = this.bigColor((int)id);
			this.color2 = this.bigColor((int)id);
		}
		this.wO = this.getWidthExactOf("o");
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0005F1E4 File Offset: 0x0005D3E4
	public static void init()
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			mFont.tahoma_7b_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_red.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_blue.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_white.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellowSmall = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_dark = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_brown.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green2.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_focus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_focus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_unfocus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_unfocus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue1 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue1.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green2.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_yellow.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_grey = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_grey.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_red.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_white.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_8b = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_8b.png", "/myfont/tahoma_8b", -1);
			mFont.number_yellow = new mFont(" 0123456789+-", "/myfont/number_yellow.png", "/myfont/number", 0);
			mFont.number_red = new mFont(" 0123456789+-", "/myfont/number_red.png", "/myfont/number", 0);
			mFont.number_green = new mFont(" 0123456789+-", "/myfont/number_green.png", "/myfont/number", 0);
			mFont.number_gray = new mFont(" 0123456789+-", "/myfont/number_gray.png", "/myfont/number", 0);
			mFont.number_orange = new mFont(" 0123456789+-", "/myfont/number_orange.png", "/myfont/number", 0);
			mFont.bigNumber_red = mFont.number_red;
			mFont.bigNumber_While = mFont.tahoma_7b_white;
			mFont.bigNumber_yellow = mFont.number_yellow;
			mFont.bigNumber_green = mFont.number_green;
			mFont.bigNumber_orange = mFont.number_orange;
			mFont.bigNumber_blue = mFont.tahoma_7_blue1;
			mFont.nameFontRed = mFont.tahoma_7_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
		}
		else
		{
			mFont.gI = new mFont(0);
			mFont.tahoma_7b_red = new mFont(1);
			mFont.tahoma_7b_blue = new mFont(2);
			mFont.tahoma_7b_white = new mFont(3);
			mFont.tahoma_7b_yellow = new mFont(4);
			mFont.tahoma_7b_yellowSmall = new mFont(4);
			mFont.tahoma_7b_dark = new mFont(5);
			mFont.tahoma_7b_green2 = new mFont(6);
			mFont.tahoma_7b_green = new mFont(7);
			mFont.tahoma_7b_focus = new mFont(8);
			mFont.tahoma_7b_unfocus = new mFont(9);
			mFont.tahoma_7 = new mFont(10);
			mFont.tahoma_7_blue1 = new mFont(11);
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
			mFont.tahoma_7_green2 = new mFont(12);
			mFont.tahoma_7_yellow = new mFont(13);
			mFont.tahoma_7_grey = new mFont(14);
			mFont.tahoma_7_red = new mFont(15);
			mFont.tahoma_7_blue = new mFont(16);
			mFont.tahoma_7_green = new mFont(17);
			mFont.tahoma_7_white = new mFont(18);
			mFont.tahoma_8b = new mFont(19);
			mFont.number_yellow = new mFont(20);
			mFont.number_red = new mFont(21);
			mFont.number_green = new mFont(22);
			mFont.number_gray = new mFont(23);
			mFont.number_orange = new mFont(24);
			mFont.bigNumber_red = new mFont(25);
			mFont.bigNumber_yellow = new mFont(26);
			mFont.bigNumber_green = new mFont(27);
			mFont.bigNumber_While = new mFont(28);
			mFont.bigNumber_blue = new mFont(29);
			mFont.bigNumber_orange = new mFont(30);
			mFont.bigNumber_black = new mFont(31);
			mFont.nameFontRed = mFont.tahoma_7b_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.yAddFont = 1;
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				mFont.yAddFont = -3;
			}
		}
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0005F721 File Offset: 0x0005D921
	public void setHeight(int height)
	{
		this.height = height;
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0005F72C File Offset: 0x0005D92C
	public Color setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float b = (float)num / 256f;
		float g = (float)num2 / 256f;
		float r = (float)num3 / 256f;
		Color result = new Color(r, g, b);
		return result;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0005F788 File Offset: 0x0005D988
	public Color bigColor(int id)
	{
		Color[] array = new Color[]
		{
			Color.red,
			Color.yellow,
			Color.green,
			Color.white,
			this.setColor(40404),
			Color.red,
			Color.black
		};
		return array[id - 25];
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0005F805 File Offset: 0x0005DA05
	public void setColorByID(int ID)
	{
		this.color1 = this.setColor(mFont.colorJava[ID]);
		this.color2 = this.setColor(mFont.colorJava[ID]);
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0005F830 File Offset: 0x0005DA30
	public void setTypePaint(mGraphics g, string st, int x, int y, int align, sbyte idFont)
	{
		sbyte b = this.id;
		bool flag = idFont > 0;
		if (flag)
		{
			b = idFont;
		}
		x--;
		bool flag2 = this.id > 24;
		if (flag2)
		{
			Color[] array = new Color[]
			{
				this.setColor(6029312),
				this.setColor(7169025),
				this.setColor(7680),
				this.setColor(0),
				this.setColor(9264),
				this.setColor(6029312)
			};
			this.color1 = array[(int)(this.id - 25)];
			this.color2 = array[(int)(this.id - 25)];
			this._drawString(g, st, x + 1, y, align);
			this._drawString(g, st, x - 1, y, align);
			this._drawString(g, st, x, y - 1, align);
			this._drawString(g, st, x, y + 1, align);
			this._drawString(g, st, x + 1, y + 1, align);
			this._drawString(g, st, x + 1, y - 1, align);
			this._drawString(g, st, x - 1, y - 1, align);
			this._drawString(g, st, x - 1, y + 1, align);
			this.color1 = this.bigColor((int)this.id);
			this.color2 = this.bigColor((int)this.id);
		}
		else
		{
			this.setColorByID((int)b);
		}
		this._drawString(g, st, x, y - this.yAdd, align);
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0005F9D4 File Offset: 0x0005DBD4
	public Color setColorFont(sbyte id)
	{
		return this.setColor(mFont.colorJava[(int)id]);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0005F9F4 File Offset: 0x0005DBF4
	public void drawString(mGraphics g, string st, int x, int y, int align)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			int length = st.Length;
			bool flag2 = align == 0;
			int num;
			if (flag2)
			{
				num = x;
			}
			else
			{
				bool flag3 = align == 1;
				if (flag3)
				{
					num = x - this.getWidth(st);
				}
				else
				{
					num = x - (this.getWidth(st) >> 1);
				}
			}
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i].ToString() + string.Empty);
				bool flag4 = num2 == -1;
				if (flag4)
				{
					num2 = 0;
				}
				bool flag5 = num2 > -1;
				if (flag5)
				{
					int x2 = this.fImages[num2][0];
					int num3 = this.fImages[num2][1];
					int w = this.fImages[num2][2];
					int num4 = this.fImages[num2][3];
					bool flag6 = num3 + num4 > this.imgFont.texture.height;
					if (flag6)
					{
						num3 -= this.imgFont.texture.height;
						x2 = this.imgFont.texture.width / 2;
					}
					g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
		}
		else
		{
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0005FB78 File Offset: 0x0005DD78
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			this.drawString(g, st, x, y, align);
		}
		else
		{
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0005FBB8 File Offset: 0x0005DDB8
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			this.drawString(g, st, x, y, align, font2);
		}
		else
		{
			this.drawStringBd(g, st, x, y, align, font2);
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0005FBF8 File Offset: 0x0005DDF8
	public void drawStringBd(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		this.setTypePaint(g, st, x - 1, y - 1, align, 0);
		this.setTypePaint(g, st, x - 1, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y - 1, align, 0);
		this.setTypePaint(g, st, x + 1, y + 1, align, 0);
		this.setTypePaint(g, st, x, y - 1, align, 0);
		this.setTypePaint(g, st, x, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y, align, 0);
		this.setTypePaint(g, st, x - 1, y, align, 0);
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x0005FCA8 File Offset: 0x0005DEA8
	public void drawString(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			int length = st.Length;
			bool flag2 = align == 0;
			int num;
			if (flag2)
			{
				num = x;
			}
			else
			{
				bool flag3 = align == 1;
				if (flag3)
				{
					num = x - this.getWidth(st);
				}
				else
				{
					num = x - (this.getWidth(st) >> 1);
				}
			}
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i]);
				bool flag4 = num2 == -1;
				if (flag4)
				{
					num2 = 0;
				}
				bool flag5 = num2 > -1;
				if (flag5)
				{
					int x2 = this.fImages[num2][0];
					int num3 = this.fImages[num2][1];
					int w = this.fImages[num2][2];
					int num4 = this.fImages[num2][3];
					bool flag6 = num3 + num4 > this.imgFont.texture.height;
					if (flag6)
					{
						num3 -= this.imgFont.texture.height;
						x2 = this.imgFont.texture.width / 2;
					}
					bool flag7 = !GameCanvas.lowGraphic && font != null;
					if (flag7)
					{
						g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num + 1, y, 20);
						g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num, y + 1, 20);
					}
					g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
		}
		else
		{
			this.setTypePaint(g, st, x, y + 1, align, font.id);
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0005FE80 File Offset: 0x0005E080
	public MyVector splitFontVector(string src, int lineWidth)
	{
		MyVector myVector = new MyVector();
		string text = string.Empty;
		for (int i = 0; i < src.Length; i++)
		{
			bool flag = src[i] == '\n' || src[i] == '\b';
			if (flag)
			{
				myVector.addElement(text);
				text = string.Empty;
			}
			else
			{
				text += src[i].ToString();
				bool flag2 = this.getWidth(text) > lineWidth;
				if (flag2)
				{
					int j;
					for (j = text.Length - 1; j >= 0; j--)
					{
						bool flag3 = text[j] == ' ';
						if (flag3)
						{
							break;
						}
					}
					bool flag4 = j < 0;
					if (flag4)
					{
						j = text.Length - 1;
					}
					myVector.addElement(text.Substring(0, j));
					i = i - (text.Length - j) + 1;
					text = string.Empty;
				}
				bool flag5 = i == src.Length - 1 && !text.Trim().Equals(string.Empty);
				if (flag5)
				{
					myVector.addElement(text);
				}
			}
		}
		return myVector;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0005FFC0 File Offset: 0x0005E1C0
	public string splitFirst(string str)
	{
		string text = string.Empty;
		bool flag = false;
		for (int i = 0; i < str.Length; i++)
		{
			bool flag2 = !flag;
			if (flag2)
			{
				string text2 = str.Substring(i);
				bool flag3 = this.compare(text2, " ");
				if (flag3)
				{
					text = text + str[i].ToString() + "-";
				}
				else
				{
					text += text2;
				}
				flag = true;
			}
			else
			{
				bool flag4 = str[i] == ' ';
				if (flag4)
				{
					flag = false;
				}
			}
		}
		return text;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00060060 File Offset: 0x0005E260
	public string[] splitStrInLine(string src, int lineWidth)
	{
		ArrayList arrayList = this.splitStrInLineA(src, lineWidth);
		string[] array = new string[arrayList.Count];
		for (int i = 0; i < arrayList.Count; i++)
		{
			array[i] = (string)arrayList[i];
		}
		return array;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x000600B0 File Offset: 0x0005E2B0
	public ArrayList splitStrInLineA(string src, int lineWidth)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int num2 = 0;
		int length = src.Length;
		bool flag = length < 5;
		ArrayList result;
		if (flag)
		{
			arrayList.Add(src);
			result = arrayList;
		}
		else
		{
			string text = string.Empty;
			try
			{
				for (;;)
				{
					while (this.getWidthNotExactOf(text) < lineWidth)
					{
						text += src[num2].ToString();
						num2++;
						bool flag2 = src[num2] == '\n';
						if (flag2)
						{
							break;
						}
						bool flag3 = num2 >= length - 1;
						if (flag3)
						{
							num2 = length - 1;
							break;
						}
					}
					bool flag4 = num2 != length - 1 && src[num2 + 1] != ' ';
					if (flag4)
					{
						int num3 = num2;
						while (src[num2 + 1] != '\n')
						{
							bool flag5 = src[num2 + 1] != ' ' || src[num2] == ' ';
							if (flag5)
							{
								bool flag6 = num2 != num;
								if (flag6)
								{
									num2--;
									continue;
								}
							}
							bool flag7 = num2 == num;
							if (flag7)
							{
								num2 = num3;
								goto IL_122;
							}
							goto IL_122;
						}
						continue;
					}
					IL_122:
					string text2 = src.Substring(num, num2 + 1 - num);
					bool flag8 = text2[0] == '\n';
					if (flag8)
					{
						text2 = text2.Substring(1, text2.Length - 1);
					}
					bool flag9 = text2[text2.Length - 1] == '\n';
					if (flag9)
					{
						text2 = text2.Substring(0, text2.Length - 1);
					}
					arrayList.Add(text2);
					bool flag10 = num2 == length - 1;
					if (flag10)
					{
						break;
					}
					num = num2 + 1;
					while (num != length - 1 && src[num] == ' ')
					{
						num++;
					}
					bool flag11 = num == length - 1;
					if (flag11)
					{
						break;
					}
					num2 = num;
					text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				Cout.LogWarning(string.Concat(new object[]
				{
					"EXCEPTION WHEN REAL SPLIT ",
					src,
					"\nend=",
					num2,
					"\n",
					ex.Message,
					"\n",
					ex.StackTrace
				}));
				arrayList.Add(src);
			}
			result = arrayList;
		}
		return result;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00060328 File Offset: 0x0005E528
	public string[] splitFontArray(string src, int lineWidth)
	{
		MyVector myVector = this.splitFontVector(src, lineWidth);
		string[] array = new string[myVector.size()];
		for (int i = 0; i < myVector.size(); i++)
		{
			array[i] = (string)myVector.elementAt(i);
		}
		return array;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00060378 File Offset: 0x0005E578
	public bool compare(string strSource, string str)
	{
		for (int i = 0; i < strSource.Length; i++)
		{
			bool flag = (string.Empty + strSource[i].ToString()).Equals(str);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x000603CC File Offset: 0x0005E5CC
	public int getWidth(string s)
	{
		bool flag = mGraphics.zoomLevel == 1;
		int result;
		if (flag)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				int num2 = this.strFont.IndexOf(s[i]);
				bool flag2 = num2 == -1;
				if (flag2)
				{
					num2 = 0;
				}
				num += this.fImages[num2][2] + this.space;
			}
			result = num;
		}
		else
		{
			result = this.getWidthExactOf(s);
		}
		return result;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0006044C File Offset: 0x0005E64C
	public int getWidthExactOf(string s)
	{
		int result;
		try
		{
			result = (int)new GUIStyle
			{
				font = this.myFont
			}.CalcSize(new GUIContent(s)).x / mGraphics.zoomLevel;
		}
		catch (Exception ex)
		{
			Cout.LogError(string.Concat(new string[]
			{
				"GET WIDTH OF ",
				s,
				" FAIL.\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			result = this.getWidthNotExactOf(s);
		}
		return result;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x000604EC File Offset: 0x0005E6EC
	public int getWidthNotExactOf(string s)
	{
		return s.Length * this.wO / mGraphics.zoomLevel;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00060514 File Offset: 0x0005E714
	public int getHeight()
	{
		bool flag = mGraphics.zoomLevel == 1;
		int result;
		if (flag)
		{
			result = this.height;
		}
		else
		{
			bool flag2 = this.height > 0;
			if (flag2)
			{
				result = this.height / mGraphics.zoomLevel;
			}
			else
			{
				GUIStyle guistyle = new GUIStyle();
				guistyle.font = this.myFont;
				try
				{
					this.height = (int)guistyle.CalcSize(new GUIContent("Adg")).y + 2;
				}
				catch (Exception ex)
				{
					Cout.LogError("FAIL GET HEIGHT " + ex.StackTrace);
					this.height = 20;
				}
				result = this.height / mGraphics.zoomLevel;
			}
		}
		return result;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x000605D4 File Offset: 0x0005E7D4
	public void _drawString(mGraphics g, string st, int x0, int y0, int align)
	{
		y0 += mFont.yAddFont;
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.font = this.myFont;
		float num = 0f;
		float num2 = 0f;
		switch (align)
		{
		case 0:
			num = (float)x0;
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperLeft;
			break;
		case 1:
			num = (float)(x0 - GameCanvas.w);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperRight;
			break;
		case 2:
		case 3:
			num = (float)(x0 - GameCanvas.w / 2);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperCenter;
			break;
		}
		guistyle.normal.textColor = this.color1;
		g.drawString(st, (int)num, (int)num2, guistyle);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00060694 File Offset: 0x0005E894
	public static string[] splitStringSv(string _text, string _searchStr)
	{
		int num = 0;
		int startIndex = 0;
		int length = _searchStr.Length;
		int num2 = _text.IndexOf(_searchStr, startIndex);
		while (num2 != -1)
		{
			startIndex = num2 + length;
			num2 = _text.IndexOf(_searchStr, startIndex);
			num++;
		}
		string[] array = new string[num + 1];
		int num3 = _text.IndexOf(_searchStr);
		int num4 = 0;
		int num5 = 0;
		while (num3 != -1)
		{
			array[num5] = _text.Substring(num4, num3 - num4);
			num4 = num3 + length;
			num3 = _text.IndexOf(_searchStr, num4);
			num5++;
		}
		array[num5] = _text.Substring(num4, _text.Length - num4);
		return array;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0006074C File Offset: 0x0005E94C
	public void reloadImage()
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			this.imgFont = GameCanvas.loadImage(this.pathImage);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00003136 File Offset: 0x00001336
	public void freeImage()
	{
	}

	// Token: 0x04000AD4 RID: 2772
	public static int LEFT = 0;

	// Token: 0x04000AD5 RID: 2773
	public static int RIGHT = 1;

	// Token: 0x04000AD6 RID: 2774
	public static int CENTER = 2;

	// Token: 0x04000AD7 RID: 2775
	public static int RED = 0;

	// Token: 0x04000AD8 RID: 2776
	public static int YELLOW = 1;

	// Token: 0x04000AD9 RID: 2777
	public static int GREEN = 2;

	// Token: 0x04000ADA RID: 2778
	public static int FATAL = 3;

	// Token: 0x04000ADB RID: 2779
	public static int MISS = 4;

	// Token: 0x04000ADC RID: 2780
	public static int ORANGE = 5;

	// Token: 0x04000ADD RID: 2781
	public static int ADDMONEY = 6;

	// Token: 0x04000ADE RID: 2782
	public static int MISS_ME = 7;

	// Token: 0x04000ADF RID: 2783
	public static int FATAL_ME = 8;

	// Token: 0x04000AE0 RID: 2784
	public static int HP = 9;

	// Token: 0x04000AE1 RID: 2785
	public static int MP = 10;

	// Token: 0x04000AE2 RID: 2786
	private int space;

	// Token: 0x04000AE3 RID: 2787
	private Image imgFont;

	// Token: 0x04000AE4 RID: 2788
	private string strFont;

	// Token: 0x04000AE5 RID: 2789
	private int[][] fImages;

	// Token: 0x04000AE6 RID: 2790
	public static int yAddFont;

	// Token: 0x04000AE7 RID: 2791
	public static int[] colorJava = new int[]
	{
		0,
		16711680,
		6520319,
		16777215,
		16755200,
		5449989,
		21285,
		52224,
		7386228,
		16771788,
		0,
		65535,
		21285,
		16776960,
		5592405,
		16742263,
		33023,
		8701737,
		15723503,
		7999781,
		16768815,
		14961237,
		4124899,
		4671303,
		16096312,
		16711680,
		16755200,
		52224,
		16777215,
		6520319,
		16096312
	};

	// Token: 0x04000AE8 RID: 2792
	public static mFont gI;

	// Token: 0x04000AE9 RID: 2793
	public static mFont tahoma_7b_red;

	// Token: 0x04000AEA RID: 2794
	public static mFont tahoma_7b_blue;

	// Token: 0x04000AEB RID: 2795
	public static mFont tahoma_7b_white;

	// Token: 0x04000AEC RID: 2796
	public static mFont tahoma_7b_yellow;

	// Token: 0x04000AED RID: 2797
	public static mFont tahoma_7b_yellowSmall;

	// Token: 0x04000AEE RID: 2798
	public static mFont tahoma_7b_dark;

	// Token: 0x04000AEF RID: 2799
	public static mFont tahoma_7b_green2;

	// Token: 0x04000AF0 RID: 2800
	public static mFont tahoma_7b_green;

	// Token: 0x04000AF1 RID: 2801
	public static mFont tahoma_7b_focus;

	// Token: 0x04000AF2 RID: 2802
	public static mFont tahoma_7b_unfocus;

	// Token: 0x04000AF3 RID: 2803
	public static mFont tahoma_7;

	// Token: 0x04000AF4 RID: 2804
	public static mFont tahoma_7_blue1;

	// Token: 0x04000AF5 RID: 2805
	public static mFont tahoma_7_blue1Small;

	// Token: 0x04000AF6 RID: 2806
	public static mFont tahoma_7_green2;

	// Token: 0x04000AF7 RID: 2807
	public static mFont tahoma_7_yellow;

	// Token: 0x04000AF8 RID: 2808
	public static mFont tahoma_7_grey;

	// Token: 0x04000AF9 RID: 2809
	public static mFont tahoma_7_red;

	// Token: 0x04000AFA RID: 2810
	public static mFont tahoma_7_blue;

	// Token: 0x04000AFB RID: 2811
	public static mFont tahoma_7_green;

	// Token: 0x04000AFC RID: 2812
	public static mFont tahoma_7_white;

	// Token: 0x04000AFD RID: 2813
	public static mFont tahoma_8b;

	// Token: 0x04000AFE RID: 2814
	public static mFont number_yellow;

	// Token: 0x04000AFF RID: 2815
	public static mFont number_red;

	// Token: 0x04000B00 RID: 2816
	public static mFont number_green;

	// Token: 0x04000B01 RID: 2817
	public static mFont number_gray;

	// Token: 0x04000B02 RID: 2818
	public static mFont number_orange;

	// Token: 0x04000B03 RID: 2819
	public static mFont bigNumber_red;

	// Token: 0x04000B04 RID: 2820
	public static mFont bigNumber_While;

	// Token: 0x04000B05 RID: 2821
	public static mFont bigNumber_yellow;

	// Token: 0x04000B06 RID: 2822
	public static mFont bigNumber_green;

	// Token: 0x04000B07 RID: 2823
	public static mFont bigNumber_orange;

	// Token: 0x04000B08 RID: 2824
	public static mFont bigNumber_blue;

	// Token: 0x04000B09 RID: 2825
	public static mFont bigNumber_black;

	// Token: 0x04000B0A RID: 2826
	public static mFont nameFontRed;

	// Token: 0x04000B0B RID: 2827
	public static mFont nameFontYellow;

	// Token: 0x04000B0C RID: 2828
	public static mFont nameFontGreen;

	// Token: 0x04000B0D RID: 2829
	public static mFont tahoma_7_greySmall;

	// Token: 0x04000B0E RID: 2830
	public static mFont tahoma_7b_yellowSmall2;

	// Token: 0x04000B0F RID: 2831
	public static mFont tahoma_7b_green2Small;

	// Token: 0x04000B10 RID: 2832
	public static mFont tahoma_7_whiteSmall;

	// Token: 0x04000B11 RID: 2833
	public static mFont tahoma_7b_greenSmall;

	// Token: 0x04000B12 RID: 2834
	public Font myFont;

	// Token: 0x04000B13 RID: 2835
	private int height;

	// Token: 0x04000B14 RID: 2836
	private int wO;

	// Token: 0x04000B15 RID: 2837
	public Color color1 = Color.white;

	// Token: 0x04000B16 RID: 2838
	public Color color2 = Color.gray;

	// Token: 0x04000B17 RID: 2839
	public sbyte id;

	// Token: 0x04000B18 RID: 2840
	public int fstyle;

	// Token: 0x04000B19 RID: 2841
	public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

	// Token: 0x04000B1A RID: 2842
	public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

	// Token: 0x04000B1B RID: 2843
	public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

	// Token: 0x04000B1C RID: 2844
	private int yAdd;

	// Token: 0x04000B1D RID: 2845
	private string pathImage;
}
