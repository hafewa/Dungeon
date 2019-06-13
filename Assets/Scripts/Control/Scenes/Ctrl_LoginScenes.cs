using System.Collections;
using System.Collections.Generic;
using kernal;
using UnityEngine;
using UnityEngine.SceneManagement;

//控制层  登陆场景
public class Ctrl_LoginScenes : BaseControl
{
    private static Ctrl_LoginScenes _instance;

    public static Ctrl_LoginScenes Instance
    {
        get { return _instance; }
    }

    public AudioClip audioBackgroundSound;


	// Use this for initialization
	void Awake ()
	{
	    _instance = this;
	}

    void Start()
    {
        //确定音频的音量
        AudioManager.SetAudioBackgroundVolumns(0.4f);
        AudioManager.SetAudioEffectVolumns(1.0f);

        AudioManager.PlayBackground(audioBackgroundSound);
        
    }

    //播放剑士音效
    public void PlayAudioEffectBySword()
    {
       AudioManager.PlayAudioEffectA("Nan_1");
    }

    //播放魔法师音效
    public void PlayAudioEffectByMagic()
    {
        AudioManager.PlayAudioEffectA("2_FireBallEffect_MagicHero");
    }


    //进入下一个场景
    public void EnterNextScenes()
    {
        //让全局的参数里面的下一个场景变成下一个关卡
        //GlobalParameterManager.NextScensName = ScenesEnum.LevelOne;
        //SceneManager.LoadScene(ConvertEnumToString.GetInstance().GetStrByEnumScenes(ScenesEnum.LoadingScenes));
        base.EnterNextScenes(ScenesEnum.LevelOne);
    }
}
