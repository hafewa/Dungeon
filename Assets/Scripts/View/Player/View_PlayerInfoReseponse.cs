using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//专门用来处理玩家信息的点击事件
public class View_PlayerInfoReseponse : MonoBehaviour
{
    public static View_PlayerInfoReseponse Instance;
    public GameObject PlayerDetailInfo;
    public GameObject GoET;                     //ET控件
    public GameObject GoHeroInfo;               //英雄头像面板
    public GameObject GoNormalATKKey;           //虚拟攻击按键对象
    public GameObject GoMagicAKey;              //大招A虚拟按键
    public GameObject GoMagicBKey;              //大招B虚拟按键
    public GameObject GoMagicCKey;              //大招C虚拟按键
    public GameObject GoMagicDKey;              //大招D虚拟按键
    public GameObject GoIncreaseHPKey;          //增加血量的虚拟按键

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }


    /// <summary>
    /// 显示ET
    /// </summary>
    public void DisplayET()
    {
        GoET.SetActive(true);
    }
    /// <summary>
    /// 隐藏 ET
    /// </summary>
    public void HidenET()
    {
        GoET.SetActive(false);
    }

    /// <summary>
    /// 隐藏所有虚拟按键
    /// </summary>
    public void HideAllVirtualKey()
    {
        GoNormalATKKey.SetActive(false);
        GoMagicAKey.SetActive(false);
        GoMagicBKey.SetActive(false);
        GoMagicCKey.SetActive(false);
        GoMagicDKey.SetActive(false);
        GoIncreaseHPKey.SetActive(false);
        HidenET();
    }

    /// <summary>
    /// 显示所有虚拟按键
    /// </summary>
    public void DisplayAllVirtualKey()
    {
        GoNormalATKKey.SetActive(true);
        GoMagicAKey.SetActive(true);
        GoMagicBKey.SetActive(true);
        GoMagicCKey.SetActive(true);
        GoMagicDKey.SetActive(true);
        GoIncreaseHPKey.SetActive(true);
        DisplayET();
    }

    /// <summary>
    /// 只显示普通攻击按键
    /// </summary>
    public void DisplayNoramalATKKey()
    {
        GoNormalATKKey.SetActive(true);
        GoMagicAKey.SetActive(false);
        GoMagicBKey.SetActive(false);
        GoMagicCKey.SetActive(false);
        GoMagicDKey.SetActive(false);
        GoIncreaseHPKey.SetActive(false);
        DisplayET();
    }

    /// <summary>
    /// 显示英雄头像信息
    /// </summary>
    public void DisplayHeroUIInfo()
    {
        GoHeroInfo.SetActive(true);
    }

    /// <summary>
    /// 隐藏英雄详细信息面板
    /// </summary>
    public void HideHeroUIInfo()
    {
        GoHeroInfo.SetActive(false);
    }

    /// <summary>
    /// 显示英雄详细信息面板
    /// </summary>
    public void DisplayHeroDetailInfo()
    {
        if (PlayerDetailInfo != null)
        {
            BeforOpenPanel(PlayerDetailInfo);
            PlayerDetailInfo.SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏英雄详细信息面板
    /// </summary>
    public void HideHeroDetailInfo()
    {
        if (PlayerDetailInfo != null)
        {
            BeforClosePanel();
            PlayerDetailInfo.SetActive(false);
        }
    }

    //在打开UI窗体之前的预处理
    private void BeforOpenPanel(GameObject goDisplayPanel)
    {
        //禁用ET
        HidenET();
        //窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().SetMaskWindow(goDisplayPanel);
    }

    //关闭窗体之前的预处理
    private void BeforClosePanel()
    {
        //开启ET
        DisplayET();
        //取消窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().CancelMaskWindow();
    }

    //使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_ANDROID || UNITY_IPHONE
    #region 响应玩家虚拟攻击按键点击
    //响应普通攻击（OnResponseNormalAttack()）这个方法已经在View_ATKNormalPressed类中调用了一次，所以就没有必要再在这里调用一次， 如果这两处地方都调用会出现点击一次普通攻击虚拟按键会出现两次的攻击效果
    public void OnNormalAttackClick()
    {
        Ctrl_HeroAttackInputByET.Instance.OnResponseNormalAttack();
    }

    public void OnMagicAttackAClick()
    {
        Ctrl_HeroAttackInputByET.Instance.OnResponseMagicAttackA();
        Ctrl_HeroProperty.Instance.DecreaseMagicValues(20);
    }
    public void OnMagicAttackBClick()
    {
        Ctrl_HeroAttackInputByET.Instance.OnResponseMagicAttackB();
        Ctrl_HeroProperty.Instance.DecreaseMagicValues(30);
    }
    public void OnMagicAttackCClick()
    {
        Ctrl_HeroAttackInputByET.Instance.OnResponseMagicAttackC();
    }
    public void OnMagicAttackDClick()
    {
        Ctrl_HeroAttackInputByET.Instance.OnResponseMagicAttackD();
    }

    /// <summary>
    /// 退出游戏系统
    /// </summary>
    public void GameExit()
    {
        Application.Quit();
    }

    #endregion

#endif
}
