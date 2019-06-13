using System.Collections;
using System.Collections.Generic;
using kernal;
using UnityEngine;

public class Ctrl_MainCityScenes : BaseControl
{

    public AudioClip AcBackground;           //主城背景音乐


	// Use this for initialization
	void Start () {
		AudioManager.SetAudioBackgroundVolumns(0.4f);
        AudioManager.PlayBackground(AcBackground);
	}
}
