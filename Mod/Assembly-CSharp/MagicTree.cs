using System;

// Token: 0x0200006B RID: 107
public class MagicTree : Npc, IActionListener
{
	// Token: 0x060003C0 RID: 960 RVA: 0x0003092C File Offset: 0x0002ED2C
	public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
	{
		this.p = new PopUp(string.Empty, 0, 0);
		this.p.command = new Command(null, this, 1, null);
		PopUp.addPopUp(this.p);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0003097C File Offset: 0x0002ED7C
	public override void paint(mGraphics g)
	{
		if (this.id == 0)
		{
			return;
		}
		SmallImage.drawSmallImage(g, this.id, this.cx, this.cy, 0, StaticObj.BOTTOM_HCENTER);
		if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
			if (this.name != null)
			{
				mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
		else if (this.name != null)
		{
			mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
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
		if (this.indexEffTask >= 0 && this.effTask != null && (int)this.cTypePk == 0)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx + SmallImage.smallImg[this.id][3] / 2 + 5, this.cy - 15 + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00030BF0 File Offset: 0x0002EFF0
	public override void update()
	{
		this.p.isPaint = MagicTree.isPaint;
		this.cur = mSystem.currentTimeMillis();
		if (this.cur - this.last >= 1000L)
		{
			this.seconds--;
			this.last = this.cur;
			if (this.seconds < 0)
			{
				this.seconds = 0;
			}
		}
		if (!this.isUpdate)
		{
			if (this.currPeas < this.maxPeas && this.seconds == 0)
			{
				this.waitToUpdate = true;
			}
		}
		else if (this.seconds == 0)
		{
			this.isUpdate = false;
			this.waitToUpdate = true;
		}
		if (this.waitToUpdate)
		{
			this.delay++;
			if (this.delay == 20)
			{
				this.delay = 0;
				this.waitToUpdate = false;
				Service.gI().getMagicTree(2);
			}
		}
		this.num = ((this.peaPostionX == null) ? 0 : (this.peaPostionX.Length * this.currPeas / this.maxPeas));
		if (this.isUpdateTree)
		{
			this.isUpdateTree = false;
			if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect)
			{
				this.p.updateXYWH(new string[]
				{
					this.isUpdate ? mResources.UPGRADING : (this.currPeas + "/" + this.maxPeas),
					NinjaUtil.getTime(this.seconds)
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
			else if (this.currPeas == this.maxPeas && !this.isUpdate)
			{
				this.p.updateXYWH(new string[]
				{
					mResources.can_harvest,
					this.currPeas + "/" + this.maxPeas
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
		}
		if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate))
		{
			this.p.says[this.p.says.Length - 1] = NinjaUtil.getTime(this.seconds);
		}
		if (this.isPeasEffect)
		{
			this.p.isPaint = false;
			ServerEffect.addServerEffect(98, this.cx + this.peaPostionX[this.currPeas - 1] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[this.currPeas - 1] - SmallImage.smallImg[this.id][4], 1);
			this.currPeas--;
			if (GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().HP_MPup();
			}
			if (this.currPeas == this.remainPeas)
			{
				this.p.isPaint = true;
				this.isUpdateTree = true;
				this.isPeasEffect = false;
			}
		}
		base.update();
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00030F70 File Offset: 0x0002F370
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			Service.gI().magicTree(1);
		}
	}

	// Token: 0x04000657 RID: 1623
	public static Image imgMagicTree;

	// Token: 0x04000658 RID: 1624
	public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

	// Token: 0x04000659 RID: 1625
	public int id;

	// Token: 0x0400065A RID: 1626
	public int level;

	// Token: 0x0400065B RID: 1627
	public int x;

	// Token: 0x0400065C RID: 1628
	public int y;

	// Token: 0x0400065D RID: 1629
	public int currPeas;

	// Token: 0x0400065E RID: 1630
	public int remainPeas;

	// Token: 0x0400065F RID: 1631
	public int maxPeas;

	// Token: 0x04000660 RID: 1632
	public new string strInfo;

	// Token: 0x04000661 RID: 1633
	public string name;

	// Token: 0x04000662 RID: 1634
	public int timeToRecieve;

	// Token: 0x04000663 RID: 1635
	public bool isUpdate;

	// Token: 0x04000664 RID: 1636
	public int[] peaPostionX;

	// Token: 0x04000665 RID: 1637
	public int[] peaPostionY;

	// Token: 0x04000666 RID: 1638
	private int num;

	// Token: 0x04000667 RID: 1639
	public PopUp p;

	// Token: 0x04000668 RID: 1640
	public bool isUpdateTree;

	// Token: 0x04000669 RID: 1641
	public new static bool isPaint = true;

	// Token: 0x0400066A RID: 1642
	public bool isPeasEffect;

	// Token: 0x0400066B RID: 1643
	public new int seconds;

	// Token: 0x0400066C RID: 1644
	public new long last;

	// Token: 0x0400066D RID: 1645
	public new long cur;

	// Token: 0x0400066E RID: 1646
	private int wPopUp;

	// Token: 0x0400066F RID: 1647
	private bool waitToUpdate;

	// Token: 0x04000670 RID: 1648
	private int delay;
}
