using System.Collections;
using System.Collections.Generic;
using kernal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_StartScenes : BaseControl
{
    private static Ctrl_StartScenes _instance;

    public static Ctrl_StartScenes Instance
    {
        get { return _instance; }
    }

    public AudioClip audioClip;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        AudioManager.SetAudioBackgroundVolumns(0.3f);   //设置背景音乐
        AudioManager.SetAudioEffectVolumns(1.0f);
        //AudioManager.PlayBackground("StartScenes");     //播放背景音乐  方式1：将音乐加入到音乐的数组中，这样会比较消耗内存，优点是反应比较快
        AudioManager.PlayBackground(audioClip); //播放音乐的方式2 这样消耗的内存较小，适用于播放背景音乐这样内存比较大的音乐
    }


    //当点击了新的传奇按钮时  1.让屏幕淡出  2.加载下一个场景，这里是加载到LoadingScenes
    public void OnClickNewGame()
    {
        Debug.Log(GetType()+ "点击Ctrl里的OnClickNewGame方法");

        //启动EnterNextScenes的线程
        StartCoroutine("EnterNextScenes");
    }

    public void OnClickGameContinue()
    {
        Debug.Log(GetType()+ "点击控制层的OnClickGameContinue方法");
    }

    //进入下一个场景
    IEnumerator EnterNextScenes()
    {

        FadeInAndOut.Instance.SetScenesToBlack();       //设置场景为淡出效果，让屏幕逐渐变暗

        //系统等待3秒
        yield return new WaitForSeconds(3.0f);

        //转到下一个场景
       // GlobalParameterManager.NextScensName = ScenesEnum.LoginScenes;  //转到登陆场景
       // //SceneManager.LoadScene(2);
       //SceneManager.LoadScene(ConvertEnumToString.GetInstance().GetStrByEnumScenes(ScenesEnum.LoadingScenes));

        base.EnterNextScenes(ScenesEnum.LoginScenes);
    }
}
