using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//显示层  用于切换人物
public class View_LoginScenes : MonoBehaviour
{
    public GameObject goSwordHero;      //剑士对象
    public GameObject goMagicHero;      //魔法师对象 
    public GameObject goSwordHeroInfo;  //剑士的消息面板
    public GameObject goMagicHeroInfo;  //魔法师的消息面板

    public InputField inputUserName;     //获取用户的名称

    void Start()
    {
        //让系统默认选择的是剑士
        GlobalParameterManager.PlayerEnumTypes = PlayerEnumType.SwordHero;
        //用户名称默认数值
        inputUserName.text = "枫枫枫子";
    }

    //显示剑士
    public void ChangeToSwordHero()
    {
        //显示剑士人物和剑士的信息
        goSwordHero.SetActive(true);
        goMagicHero.SetActive(false);

        goSwordHeroInfo.SetActive(true);
        goMagicHeroInfo.SetActive(false);

        //播放声音
        Ctrl_LoginScenes.Instance.PlayAudioEffectBySword();
    }

    //显示魔法师
    public void ChangeToMagicHero()
    {
        //显示魔法师人物和信息
        goSwordHero.SetActive(false);
        goMagicHero.SetActive(true);

        goSwordHeroInfo.SetActive(false);
        goMagicHeroInfo.SetActive(true);

        GlobalParameterManager.PlayerEnumTypes = PlayerEnumType.MagicHero;

        //播放音效
        Ctrl_LoginScenes.Instance.PlayAudioEffectByMagic();
    }

    //提交信息
    public void SubmitInfo()
    {
        
            //获取玩家的姓名  获得这个名字用于跨场景使用
            GlobalParameterManager.PlayerName = inputUserName.text;
            //跳转到下一个场景
            Ctrl_LoginScenes.Instance.EnterNextScenes();
       

    }
}
