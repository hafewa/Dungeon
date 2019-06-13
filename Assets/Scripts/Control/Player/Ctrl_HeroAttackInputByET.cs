using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层  用于控制主角输入的攻击，通过虚拟按键来实现
public class Ctrl_HeroAttackInputByET : BaseControl
{
//使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_ANDROID || UNITY_IPHONE
    public static Ctrl_HeroAttackInputByET Instance;

    //事件：主角控制事件
    public static event del_PlayerControlWithStr EvePlayerControl;

    public bool IsMagicAttackA=false;
    public bool IsMagicAttackB=false;
    public bool IsMagicAttackC=false;
    public bool IsMagicAttackD=false;

    void Awake()
    {
        Instance = this;
    }

    //响应普通攻击输入
    public void OnResponseNormalAttack()
    {
        if (EvePlayerControl != null)
        {
            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_NORMAL);

        }
    }
    //响应大招A输入
    public void OnResponseMagicAttackA()
    {
        if (EvePlayerControl != null)
        {
            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKA);
            IsMagicAttackA = true;
        }
    }
    //响应大招B输入
    public void OnResponseMagicAttackB()
    {
        if (EvePlayerControl != null)
        {
            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKB);
            IsMagicAttackB = true;
        }
    }
    //响应大招C输入
    public void OnResponseMagicAttackC()
    {
        if (EvePlayerControl != null)
        {
            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKC);
            IsMagicAttackC = true;
        }
    }
    //响应大招D输入
    public void OnResponseMagicAttackD()
    {
        if (EvePlayerControl != null)
        {
            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKD);
            IsMagicAttackD = true;
        }
    }
#endif
}
