using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家核心数值代理类   为了简化数值的开发，把数值的直接存取与复杂操作计算相分离（本质是“代理”设计模式的应用）   这个类必须设计为单例模式

public class PlayerKernalDataProxy :PlayerKernalData
{
    private static PlayerKernalDataProxy _instance = null;

    public const int Enemy_Min_Attack = 1;  //敌人最低攻击力

    public PlayerKernalDataProxy(float hp,float mp,float atk,float def,float dex,float maxHp,float maxMp,float maxAtk,float maxDex,float maxDef,float atkByPro,float defByPro,float dexByPro):base(hp,mp,atk,def,dex,maxHp,maxMp,maxAtk,maxDef,maxDex,atkByPro,defByPro,dexByPro)
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError(GetType()+"构造函数不允许重复调用");
        }
    }

    //得到本类示例
    public static PlayerKernalDataProxy GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }
        else
        {
            Debug.LogWarning("GetInstance()/需要先进行调用构造函数");
            return null;
        }
    }

    #region 生命数值操作（五个方法）

    /// <summary>
    /// 减少生命数值 (典型应用场景：被敌人攻击)
    /// 公式：_Health=_Health - (敌人攻击力 - 主角防御力 - 主角武器防御力）
    /// </summary>
    /// <param name="enemyAttackValue"> 参数为敌人的攻击力</param>
    public void DecreaseHealthValues(float enemyAttackValue)
    {
        float enemyRealAttack = 0.0f;

        enemyRealAttack = enemyAttackValue - base.Defence - base.DefenceByProp;

        //敌人的攻击 必须是大于0攻击才会生效，还有一种情况是如果主角的总防御力大于了敌人的攻击力，需要让主角减少一个最低的流血量这里是设置为1
        if (enemyRealAttack > 0)
        {
            base.Health -= enemyRealAttack;
        }
        else
        {
            base.Health -= Enemy_Min_Attack;
        }


        //生命值发生改变  需要改变攻击力、防御力、敏捷度
        this.UpdateATKValue();
        this.UpdateDefenceValue();
        this.UpdateDexterityValue();
    }

    //增加生命值  典型的应用场景是吃血包  
    public void IncreaseHealthValues(float healthValue)
    {
        float floRealIncreaseValues = 0.0f; //真实的增加量
        floRealIncreaseValues= base.Health + healthValue;

        //只有当主角当前的血量是小于最大血量才增加，另外，如果增加的血量值大于了最大血量这个临界值，那么需要让血量等于最大的血量值
        if (floRealIncreaseValues < base.MaxHealth)
        {
            base.Health += healthValue;
        }
        else
        {
            base.Health = base.MaxHealth;
        }

        //生命值发生改变  需要改变攻击力、防御力、敏捷度
        this.UpdateATKValue();
        this.UpdateDefenceValue();
        this.UpdateDexterityValue();

    }

    //得到当前的健康值
    public float GetCurrentHealth()
    {
        return base.Health;
    }

    //增加最大生命数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxHealth(float increaseHealth)
    {
        base.MaxHealth += Mathf.Abs(increaseHealth);
    }

    //得到增加的最大生命数值
    public float GetMaxHealth()
    {
        return base.MaxHealth;
    }

    #endregion

    #region 魔法数值操作（五个方法）

    /*减少魔法数值  应用场景：释放大招
     公式：_Magic = _Magic - (释放一次“特定的损耗”) 
     */
    public void DecreaseMagicValues(float magicValue)
    {
        float realMagicValue = 0.0f;
        realMagicValue = base.Magic - magicValue;   //实际的剩余魔法数值

        //只有当还剩下魔法数值才能释放
        if (realMagicValue > 0)
        {
            base.Magic -= Mathf.Abs(magicValue);
        }
        else
        {
            base.Magic = 0;
        }

    }

    //增加魔法值  典型的应用场景是吃魔法包  
    public void IncreaseMagicValues(float magicValue)
    {
        float floRealIncreaseMagicValues = 0.0f; //真实的魔法增加量
        floRealIncreaseMagicValues = base.Magic + magicValue;

        if (floRealIncreaseMagicValues < base.MaxMagic)
        {
            base.Magic += magicValue;
        }
        else
        {
            base.Magic = base.MaxMagic;
        }

    }

    //得到当前的魔法值
    public float GetCurrentMagic()
    {
        return base.Magic;
    }

    //增加最大魔法数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxMagic(float increaseMagic)
    {
        base.MaxMagic += Mathf.Abs(increaseMagic);
    }

    //得到增加的最大魔法数值
    public float GetMaxMagic()
    {
        return base.MaxMagic;
    }

    #endregion

    #region 攻击力数值操作（四个方法）
    /*
     *公式：_AttackForce=MaxAttack/2*(_Health/MaxHealth)+["武器攻击力"]
     * MaxAttack除2的原因是为了防止不能让人物的攻击力超过最大的数值，例如当一个主角的等级很低但是拿了一把特别厉害的武器，这样就有可能造成等级低的人都可以杀死等级很高的
     */

    //更新攻击力 典型场景：当主角的健康数值改变或者取得了新的武器道具  参数为新武器数值 如果没有武器增量为0
    public void UpdateATKValue(float newWeaponValues=0)
    {
        float realAttackValues = 0.0f;  //实际攻击数值

        //没有获取新的武器道具
        if (realAttackValues == 0)
        {
            //如果当前没有更新任何有攻击力的道具 公式为：实际的攻击力 = 最大攻击力/2 * （当前健康值 / 最大健康值）+道具增加的攻击力
            realAttackValues = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + base.AttackByProp;
        }
        //获取了新的武器道具
        else if(realAttackValues>0)
        {
            //获得了新的道具后，攻击力需要更改为增加的新的武器数值  然后需要更新一下当前通过道具增加的攻击值
            realAttackValues = base.MaxAttack / 2 * (base.Health / base.MaxHealth) +newWeaponValues;

            base.AttackByProp = newWeaponValues;
        }

        //数值有效性验证   控制当前的攻击数值不能大于最大攻击力数值
        if (realAttackValues > base.MaxAttack)
        {
            base.Attack = base.MaxAttack;
        }
        else
        {
            base.Attack = realAttackValues;
        }

        
    }

    //得到当前的攻击数值
    public float GetCurrentATK()
    {
        return base.Attack;
    }

    //增加最大攻击数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxATK(float increaseAttack)
    {
        base.MaxAttack += Mathf.Abs(increaseAttack);
    }

    //得到增加的最大攻击力数值
    public float GetMaxATK()
    {
        return base.MaxAttack;
    }

    

    #endregion

    #region 防御力数值操作 (四个方法)

    /*
     * 公式：_Defence=MaxDefence/2 * (_Health / MaxHealth) + [“武器防御力”]
     */

    //更新防御力  应用场景：当主角健康数值改变，或者取得了新的物理道具
    public void UpdateDefenceValue(float newWeaponValues = 0)
    {
        float realDefenceValues = 0.0f;  //实际攻击数值

        //没有获取新的防御道具
        if (realDefenceValues == 0)
        {
            //如果当前没有更新任何有防御性的道具 公式为：实际的防御力 = 最大防御力/2 * （当前健康值 / 最大健康值）+道具增加的防御力
            realDefenceValues = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
        }
        //获取了新的防御性道具
        else if (realDefenceValues > 0)
        {
            //获得了新的道具后，防御力需要更改为增加的新的防御数值  然后需要更新一下当前通过道具增加的防御值
            realDefenceValues = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + newWeaponValues;

            base.DefenceByProp = newWeaponValues;
        }

        //数值有效性验证   控制当前的防御数值不能大于最大防御力数值
        if (realDefenceValues > base.MaxDefence)
        {
            base.Defence = base.MaxDefence;
        }
        else
        {
            base.Defence = realDefenceValues;
        }

    }

    //得到当前的防御数值
    public float GetCurrentDefence()
    {
        return base.Defence;
    }

    //增加最大防御数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxDefence(float increaseDefence)
    {
        base.MaxDefence += Mathf.Abs(increaseDefence);
    }

    //得到增加的最大防御力数值
    public float GetMaxDefence()
    {
        return base.MaxDefence;
    }

    #endregion

    #region 敏捷度数值操作(四个方法)

    /*
     * 公式：_MoveSpeed=MaxMoveSpeed/2 * (_Health / MaxHealth) - _Defence + [“道具敏捷度”]
     * 后面需要减去防御力数值是因为如果护甲特别高，那么敏捷度应该下降
     */

    //更新敏捷度  应用场景：当主角健康数值、防御力改变，或者取得了新的物理道具
    public void UpdateDexterityValue(float newWeaponValues = 0)
    {
        //实际敏捷度
        float realDexterity = 0.0f;

        //计算的数值与增加的攻击力和防御力差不多
        if (newWeaponValues == 0)
        {
            realDexterity = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence + base.DexterityByProp;
        }
        else if(newWeaponValues>0)
        {
            realDexterity = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence + newWeaponValues;
        }

        //数值有效性验证   控制当前的敏捷度数值不能大于最大敏捷度数值
        if (realDexterity > base.MaxDexterity)
        {
            base.Dexterity = base.MaxDexterity;
        }
        else
        {
            base.Dexterity = realDexterity;
        }
    }

    //得到当前的敏捷数值
    public float GetCurrentDexterity()
    {
        return base.Dexterity;
    }

    //增加最大敏捷度数值(应用场景：等级提升)  参数是增量数值
    public void IncreaseMaxDexterity(float increaseDexterity)
    {
        base.MaxDexterity += Mathf.Abs(increaseDexterity);
    }

    //得到增加的最大敏捷度数值
    public float GetMaxDexterity()
    {
        return base.MaxDexterity;
    }

    #endregion

    //显示所有的初始数值
    public void DisplayAllOriValues()
    {
        base.Health = base.Health;
        base.Magic = base.Magic;
        base.Attack = base.Attack;
        base.Defence = base.Defence;
        base.Dexterity = base.Dexterity;

        base.MaxHealth = base.MaxHealth;
        base.MaxMagic = base.MaxMagic;
        base.MaxDefence = base.MaxDefence;
        base.MaxDexterity = base.MaxDexterity;
        base.MaxAttack = base.MaxAttack;

        base.AttackByProp = base.AttackByProp;
        base.DefenceByProp = base.DefenceByProp;
        base.DexterityByProp = base.DexterityByProp;
    }
}
