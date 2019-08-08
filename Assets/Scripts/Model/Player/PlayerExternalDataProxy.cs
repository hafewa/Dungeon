using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家数据扩展的代理
public class PlayerExternalDataProxy :PlayerExternalData {
    private static PlayerExternalDataProxy _instance = null;

    public PlayerExternalDataProxy(int exp,int killNum,int level,int gold,int dia) : base(exp,killNum,level,gold,dia)
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError(GetType() + "构造函数不允许重复调用");
        }
    }

    //得到本类示例
    public static PlayerExternalDataProxy GetInstance()
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


    #region 经验值
    //增加经验值
    public void AddExp(int ExpValues)
    {
        //++base.Experience;
        base.Experience += ExpValues;

        //经验值增加到阀值会提升等级等等
        UpgradeRule.GetInstance().GetUpgradeCondition(base.Experience);
    }

    //得到当前经验值
    public int GetCurrentExp()
    {
        return base.Experience;
    }


    #endregion

    #region 杀敌数量

    //增加杀敌数量
    public void AddKillNumber()
    {
        ++base.KillNumber;

    }

    //得到当前杀敌数量
    public int GetCurrentKillNumber()
    {
        return base.KillNumber;
    }

    #endregion

    #region 等级

    //增加等级
    public void AddLevel()
    {
        ++base.Level;

        //等级提升，相应主角核心数值的最大值将改变
        UpgradeRule.GetInstance().UpgradeOperation((LevelName)base.Level);  //强转
    }

    //得到当前等级
    public int GetCurrentLevel()
    {
        return base.Level;
    }

    #endregion

    #region 金币

    //增加金币
    public void AddGold(int goldNum)
    {
        base.Gold += Mathf.Abs(goldNum);
    }
    
    /// <summary>
    /// 减少金币
    /// </summary>
    /// <param name="goldNum">数量</param>
    /// <returns></returns>
    public bool DecreaseGolds(int goldNum)
    {
        bool bResult = false;       //处理结果
        if (GetCurrentGold() - Mathf.Abs(goldNum) >= 0)
        {
            base.Gold -= Mathf.Abs(goldNum);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

    //得到当前金币
    public int GetCurrentGold()
    {
        return base.Gold;
    }

    #endregion

    #region 钻石

    //增加钻石
    public void AddDiamonds(int diamondsNum)
    {
        base.Diamonds += Mathf.Abs(diamondsNum);
    }

    /// <summary>
    /// 减少钻石
    /// </summary>
    /// <param name="diamonNum">数量</param>
    /// <returns>true：处理成功</returns>
    public bool DecreaseDiamonds(int diamonNum)
    {
        bool bResult = false;       //处理结果
        //购买后的钻石余额不能为负数
        if (GetCurrentDiamonds() - Mathf.Abs(diamonNum)>=0)
        {
            base.Diamonds -= Mathf.Abs(diamonNum);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

    //得到当前钻石
    public int GetCurrentDiamonds()
    {
        return base.Diamonds;
    }

    #endregion

    public void DisplayAllOriginalData()
    {
        base.Experience = base.Experience;
        base.KillNumber = base.KillNumber;
        base.Level = base.Level;
        base.Gold = base.Gold;
        base.Diamonds = base.Diamonds;
    }
}
