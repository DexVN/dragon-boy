using System;

// Token: 0x02000060 RID: 96
public class MagicTree : Npc, IActionListener
{
	// Token: 0x060004BF RID: 1215 RVA: 0x0005C874 File Offset: 0x0005AA74
	public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
	{
		this.p = new PopUp(string.Empty, 0, 0);
		this.p.command = new Command(null, this, 1, null);
		PopUp.addPopUp(this.p);
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0005C8C4 File Offset: 0x0005AAC4
	public override void paint(mGraphics g)
	{
		bool flag = this.id == 0;
		if (!flag)
		{
			SmallImage.drawSmallImage(g, this.id, this.cx, this.cy, 0, StaticObj.BOTTOM_HCENTER);
			bool flag2 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
			if (flag2)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
				bool flag3 = this.name != null;
				if (flag3)
				{
					mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			else
			{
				bool flag4 = this.name != null;
				if (flag4)
				{
					mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			try
			{
				for (int i = 0; i < this.currPeas; i++)
				{
					g.drawImage(MagicTree.pea, this.cx + this.peaPostionX[i] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[i] - SmallImage.smallImg[this.id][4], 0);
				}
			}
			catch (Exception ex)
			{
			}
			bool flag5 = this.indexEffTask >= 0 && this.effTask != null && this.cTypePk == 0;
			if (flag5)
			{
				SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx + SmallImage.smallImg[this.id][3] / 2 + 5, this.cy - 15 + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				bool flag6 = GameCanvas.gameTick % 2 == 0;
				if (flag6)
				{
					this.indexEffTask++;
					bool flag7 = this.indexEffTask >= this.effTask.arrEfInfo.Length;
					if (flag7)
					{
						this.indexEffTask = 0;
					}
				}
			}
		}
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0005CB6C File Offset: 0x0005AD6C
	public override void update()
	{
		this.p.isPaint = MagicTree.isPaint;
		this.cur = mSystem.currentTimeMillis();
		bool flag = this.cur - this.last >= 1000L;
		if (flag)
		{
			this.seconds--;
			this.last = this.cur;
			bool flag2 = this.seconds < 0;
			if (flag2)
			{
				this.seconds = 0;
			}
		}
		bool flag3 = !this.isUpdate;
		if (flag3)
		{
			bool flag4 = this.currPeas < this.maxPeas && this.seconds == 0;
			if (flag4)
			{
				this.waitToUpdate = true;
			}
		}
		else
		{
			bool flag5 = this.seconds == 0;
			if (flag5)
			{
				this.isUpdate = false;
				this.waitToUpdate = true;
			}
		}
		bool flag6 = this.waitToUpdate;
		if (flag6)
		{
			this.delay++;
			bool flag7 = this.delay == 20;
			if (flag7)
			{
				this.delay = 0;
				this.waitToUpdate = false;
				Service.gI().getMagicTree(2);
			}
		}
		this.num = ((this.peaPostionX == null) ? 0 : (this.peaPostionX.Length * this.currPeas / this.maxPeas));
		bool flag8 = this.isUpdateTree;
		if (flag8)
		{
			this.isUpdateTree = false;
			bool flag9 = (this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect;
			if (flag9)
			{
				this.p.updateXYWH(new string[]
				{
					this.isUpdate ? mResources.UPGRADING : (this.currPeas.ToString() + "/" + this.maxPeas.ToString()),
					NinjaUtil.getTime(this.seconds)
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
			else
			{
				bool flag10 = this.currPeas == this.maxPeas && !this.isUpdate;
				if (flag10)
				{
					this.p.updateXYWH(new string[]
					{
						mResources.can_harvest,
						this.currPeas.ToString() + "/" + this.maxPeas.ToString()
					}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
				}
			}
		}
		bool flag11 = (this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate);
		if (flag11)
		{
			this.p.says[this.p.says.Length - 1] = NinjaUtil.getTime(this.seconds);
		}
		bool flag12 = this.isPeasEffect;
		if (flag12)
		{
			this.p.isPaint = false;
			ServerEffect.addServerEffect(98, this.cx + this.peaPostionX[this.currPeas - 1] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[this.currPeas - 1] - SmallImage.smallImg[this.id][4], 1);
			this.currPeas--;
			bool flag13 = GameCanvas.gameTick % 2 == 0;
			if (flag13)
			{
				SoundMn.gI().HP_MPup();
			}
			bool flag14 = this.currPeas == this.remainPeas;
			if (flag14)
			{
				this.p.isPaint = true;
				this.isUpdateTree = true;
				this.isPeasEffect = false;
			}
		}
		base.update();
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0005CF18 File Offset: 0x0005B118
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1;
		if (flag)
		{
			Service.gI().magicTree(1);
		}
	}

	// Token: 0x04000A4C RID: 2636
	public static Image imgMagicTree;

	// Token: 0x04000A4D RID: 2637
	public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

	// Token: 0x04000A4E RID: 2638
	public int id;

	// Token: 0x04000A4F RID: 2639
	public int level;

	// Token: 0x04000A50 RID: 2640
	public int x;

	// Token: 0x04000A51 RID: 2641
	public int y;

	// Token: 0x04000A52 RID: 2642
	public int currPeas;

	// Token: 0x04000A53 RID: 2643
	public int remainPeas;

	// Token: 0x04000A54 RID: 2644
	public int maxPeas;

	// Token: 0x04000A55 RID: 2645
	public new string strInfo;

	// Token: 0x04000A56 RID: 2646
	public string name;

	// Token: 0x04000A57 RID: 2647
	public int timeToRecieve;

	// Token: 0x04000A58 RID: 2648
	public bool isUpdate;

	// Token: 0x04000A59 RID: 2649
	public int[] peaPostionX;

	// Token: 0x04000A5A RID: 2650
	public int[] peaPostionY;

	// Token: 0x04000A5B RID: 2651
	private int num;

	// Token: 0x04000A5C RID: 2652
	public PopUp p;

	// Token: 0x04000A5D RID: 2653
	public bool isUpdateTree;

	// Token: 0x04000A5E RID: 2654
	public new static bool isPaint = true;

	// Token: 0x04000A5F RID: 2655
	public bool isPeasEffect;

	// Token: 0x04000A60 RID: 2656
	public new int seconds;

	// Token: 0x04000A61 RID: 2657
	public new long last;

	// Token: 0x04000A62 RID: 2658
	public new long cur;

	// Token: 0x04000A63 RID: 2659
	private int wPopUp;

	// Token: 0x04000A64 RID: 2660
	private bool waitToUpdate;

	// Token: 0x04000A65 RID: 2661
	private int delay;
}
