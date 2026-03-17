using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class MyAudioClip
{
	// Token: 0x060005E5 RID: 1509 RVA: 0x0006A556 File Offset: 0x00068756
	public MyAudioClip(string filename)
	{
		this.clip = (AudioClip)Resources.Load(filename);
		this.name = filename;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0006A578 File Offset: 0x00068778
	public void Play()
	{
		Main.main.GetComponent<AudioSource>().PlayOneShot(this.clip);
		this.timeStart = mSystem.currentTimeMillis();
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0006A59C File Offset: 0x0006879C
	public bool isPlaying()
	{
		return false;
	}

	// Token: 0x04000DF7 RID: 3575
	public string name;

	// Token: 0x04000DF8 RID: 3576
	public AudioClip clip;

	// Token: 0x04000DF9 RID: 3577
	public long timeStart;
}
