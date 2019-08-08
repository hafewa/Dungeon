using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *公共层：全局参数      目的是为了防止一些代码不小心打错字母等,减少错误率
 *1.定义整个项目的枚举类型
 * 2.定义整个项目的委托
 * 3.定义整个项目的系统常量
 * 4.定义系统中的所有标签tag
 */

#region 项目的枚举类型

//场景名称的枚举
public enum ScenesEnum
{
    TestDialogScene,        //测试场景
    StartScenes,
    LoadingScenes,
    LoginScenes,
    LevelOne,
    LevelTow,
    BaseScenes,
    MainCityScene
}

//角色的类型
public enum PlayerEnumType
{
    SwordHero,
    MagicHero,
    Other
}

//主角的动作状态
public enum HeroActionState
{
    None,           //无
    Idle,           //休闲
    Running,        //跑动
    NormalAttack,   //普通攻击  
    MagicTrickA,    //大招A
    MagicTrickB,    //大招B

}

//定义主角的连招状态
public enum NormalAttackComboState
{
    NorlmalAttack1,
    NorlmalAttack2,
    NorlmalAttack3,
}

//等级名称
public enum LevelName
{
    Level_0=0,
    Level_1 = 1,
    Level_2 = 2,
    Level_3 = 3,
    Level_4 = 4,
    Level_5 = 5,
    Level_6 = 6,
    Level_7 = 7,
    Level_8 = 8,
    Level_9 = 9,
    Level_10 = 10,
}

public enum EnemyState
{
    Idle,           //休闲
    Walking,        //移动
    Attack,         //攻击
    Hurt,           //受伤
    Dead            //死亡
}

#endregion

#region 项目的委托类型

/*委托：主角控制  参数为控制类型
 */
public delegate void del_PlayerControlWithStr(string controlType);

//委托： 关于玩家核心数值
public delegate void del_PlayerKernalModel(KeyValueUpdate kv);
//键值更新类
public class KeyValueUpdate
{
    private string _Key;    //键
    private object _Value;  //值

    //设置只读属性
    public string Key
    {
        get
        {
            return _Key;
        }
    }
    public object Value
    {
        get
        {
            return _Value;
        }
    }

    public KeyValueUpdate(string key, object value)
    {
        _Key = key;
        _Value = value;
    }
}

#endregion

public class GlobalParameter  {
    //定义项目系统常量

    //Easy Touch插件定义的摇杆名称
    public const string JOYSTICK_NAME = "HeroJoystick";

    //输入管理器里面定义的攻击名称
    public const string INPUT_MANAGER_ATTACKNAME_NORMAL = "NormalAttack";           //普通攻击
    public const string INPUT_MANAGER_ATTACKNAME_MAGICTRICKA = "MagicTrickA";       //大招A攻击
    public const string INPUT_MANAGER_ATTACKNAME_MAGICTRICKB = "MagicTrickB";       //大招B攻击
    public const string INPUT_MANAGER_ATTACKNAME_MAGICTRICKC = "MagicTrickC";       //大招C攻击
    public const string INPUT_MANAGER_ATTACKNAME_MAGICTRICKD = "MagicTrickD";       //大招D攻击


    //间隔时间
    public const float INTERVAL_TIME_0DOT1F = 0.1f;
    public const float INTERVAL_TIME_0DOT2F = 0.2f;
    public const float INTERVAL_TIME_0DOT3F = 0.3f;
    public const float INTERVAL_TIME_0DOT5F = 0.5f;

    public const float INTERVAL_TIME_1F = 1.0f;
    public const float INTERVAL_TIME_1DOT5F = 1.5f;
    public const float INTERVAL_TIME_2F = 2.0f;
    public const float INTERVAL_TIME_2DOT5F = 2.5f;
    public const float INTERVAL_TIME_3F = 3.0f;
    public const float INTERVAL_TIME_3DOT5F = 3.5f;

}

//定义所有的项目标签
public class Tag
{
    public static string Tag_Enemys = "Tag_Enemys";
    public static string Player = "Player";
}
