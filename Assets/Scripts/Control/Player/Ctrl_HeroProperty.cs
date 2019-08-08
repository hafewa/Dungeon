using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/*
 * 控制层：英雄属性脚本
 * 1.实例化对应的模型层类且初始化数据
 * 2.整个模型层关于“玩家”（Player）模块的核心方法，用于给本控制层其它脚本调用
 */
public class Ctrl_HeroProperty : BaseControl
{
    public static Ctrl_HeroProperty Instance;
    //玩家核心数值
    public float PlayerCurrentHP=100;
    public float PlayerMaxHP=100;
    public float PlayerCurrentMP=100;
    public float PlayerMaxMP=100;
    public float PlayerCurrentAttack=10;
    public float PlayerMaxAttack=10;
    public float PlayerCurrentDefence=5;
    public float PlayerMaxDefence=5;
    public float PlayerCurrentDexterity=20;
    public float PlayerMaxDexterity=50;

    public float AttackByProp=0;
    public float DefenceByProp=0;
    public float DexterityByProp=0;

    //玩家扩展数值
    public int Exp=0;
    public int Level=0;
    public int KillNum=0;
    public int Gold=0;
    public int Diamonds=0;

    //背包系统道具数值
    public int BloodBottleNum;
    public int MagicBottleNum;
    public int ATKNum;
    public int DEFNum;
    public int DEXNum;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        //初始化模型层的数据
        PlayerKernalDataProxy playerKernalData = new PlayerKernalDataProxy(PlayerCurrentHP, PlayerCurrentMP, PlayerCurrentAttack, PlayerCurrentDefence, PlayerCurrentDexterity, PlayerMaxHP, PlayerMaxMP, PlayerMaxAttack, PlayerMaxDexterity,PlayerMaxDefence , AttackByProp, DefenceByProp, DexterityByProp);


        PlayerExternalDataProxy playerExternalData = new PlayerExternalDataProxy(Exp, KillNum, Level,Gold, Diamonds);

        PlayerPackageProxy playerPackageProxy=new PlayerPackageProxy(BloodBottleNum,MagicBottleNum,ATKNum,DEFNum,DEXNum);
        
    }

    #region 生命数值操作（五个方法）
    /// <summary>
    /// 减少生命数值
    /// </summary>
    /// <param name="enemyAttackValue">敌人攻击数值</param>
    public void DecreaseHealthValues(float enemyAttackValue)
    {
        if(enemyAttackValue>0)
            PlayerKernalDataProxy.GetInstance().DecreaseHealthValues(enemyAttackValue);
    }

    public void IncreaseHealthValues(float healthValue)
    {
        if(healthValue>0)
            PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(healthValue);

    }

    //得到当前的健康值
    public float GetCurrentHealth()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentHealth();
    }

    //增加最大生命数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxHealth(float increaseHealth)
    {
        if(increaseHealth>0)
            PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(increaseHealth);
    }

    //得到增加的最大生命数值
    public float GetMaxHealth()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxHealth();
    }

    #endregion

    #region 魔法数值操作（五个方法）

    /*减少魔法数值  应用场景：释放大招
     公式：_Magic = _Magic - (释放一次“特定的损耗”) 
     */
    public void DecreaseMagicValues(float magicValue)
    {
        if(magicValue>0)
            PlayerKernalDataProxy.GetInstance().DecreaseMagicValues(magicValue);

    }

    //增加魔法值  典型的应用场景是吃魔法包  
    public void IncreaseMagicValues(float magicValue)
    {
        if(magicValue>0)
            PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(magicValue);

    }

    //得到当前的魔法值
    public float GetCurrentMagic()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentMagic();
    }

    //增加最大魔法数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxMagic(float increaseMagic)
    {
        if (increaseMagic > 0)
            PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(increaseMagic);
    }

    //得到增加的最大魔法数值
    public float GetMaxMagic()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxMagic();
    }

    #endregion

    #region 攻击力数值操作（四个方法）

    //更新攻击力 典型场景：当主角的健康数值改变或者取得了新的武器道具  参数为新武器数值 如果没有武器增量为0
    public void UpdateATKValue(float newWeaponValues = 0)
    {
        PlayerKernalDataProxy.GetInstance().UpdateATKValue(newWeaponValues);
    }

    //得到当前的攻击数值
    public float GetCurrentATK()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentATK();
    }

    //增加最大攻击数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxATK(float increaseAttack)
    {
        if (increaseAttack > 0)
            PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(increaseAttack);

    }

    //得到增加的最大攻击力数值
    public float GetMaxATK()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxATK();
    }



    #endregion

    #region 防御力数值操作 (四个方法)

    //更新防御力  应用场景：当主角健康数值改变，或者取得了新的物理道具
    public void UpdateDefenceValue(float newWeaponValues = 0)
    {
        PlayerKernalDataProxy.GetInstance().UpdateDefenceValue(newWeaponValues);
    }

    //得到当前的防御数值
    public float GetCurrentDefence()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentDefence();
    }

    //增加最大防御数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxDefence(float increaseDefence)
    {
        if (increaseDefence > 0)
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDefence(increaseDefence);
    }

    //得到增加的最大防御力数值
    public float GetMaxDefence()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxDefence();
    }

    #endregion

    #region 敏捷度数值操作(四个方法)

    //更新敏捷度  应用场景：当主角健康数值、防御力改变，或者取得了新的物理道具
    public void UpdateDexterityValue(float newWeaponValues = 0)
    {
        PlayerKernalDataProxy.GetInstance().UpdateDexterityValue(newWeaponValues);
    }

    //得到当前的敏捷数值
    public float GetCurrentDexterity()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentDexterity();
    }

    //增加最大敏捷度数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxDexterity(float increaseDexterity)
    {
        if (increaseDexterity > 0)
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDexterity(increaseDexterity);
    }

    //得到增加的最大敏捷度数值
    public float GetMaxDexterity()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxDexterity();
    }

    #endregion

    #region 经验值
    //增加经验值
    public void AddExp(int ExpValues)
    {
        if (ExpValues > 0)
            PlayerExternalDataProxy.GetInstance().AddExp(ExpValues);
    }

    //得到当前经验值
    public int GetCurrentExp()
    {
        return PlayerExternalDataProxy.GetInstance().GetCurrentExp();
    }


    #endregion

    #region 杀敌数量

    //增加杀敌数量
    public void AddKillNumber()
    {
        PlayerExternalDataProxy.GetInstance().AddKillNumber();

    }

    //得到当前杀敌数量
    public int GetCurrentKillNumber()
    {
        return PlayerExternalDataProxy.GetInstance().GetCurrentKillNumber();
    }

    #endregion

    #region 等级

    //增加等级
    public void AddLevel()
    {
        
        PlayerExternalDataProxy.GetInstance().AddLevel();
    }

    //得到当前等级
    public int GetCurrentLevel()
    {
        return PlayerExternalDataProxy.GetInstance().GetCurrentLevel();
    }

    #endregion

    #region 金币

    //增加金币
    public void AddGold(int goldNum)
    {
        PlayerExternalDataProxy.GetInstance().AddGold(goldNum);
    }

    //得到当前金币
    public int GetCurrentGold()
    {
        return PlayerExternalDataProxy.GetInstance().GetCurrentGold();
    }

    #endregion

    #region 钻石

    //增加钻石
    public void AddDiamonds(int diamondsNum)
    {
        PlayerExternalDataProxy.GetInstance().AddDiamonds(diamondsNum);
    }

    //得到当前钻石
    public int GetCurrentDiamonds()
    {
        return PlayerExternalDataProxy.GetInstance().GetCurrentDiamonds();
    }

    #endregion

}
