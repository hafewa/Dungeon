using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 代理模式，玩家背包数据代理，用于封装核心背包数据的方法
/// </summary>
public class PlayerPackageProxy : PlayerPackageData
{

    private static PlayerPackageProxy _instance = null;
    

    public PlayerPackageProxy(int bloodBottleNum, int magicBottleNum, int atkNum, int defNum, int dexNum) : base(
        bloodBottleNum, magicBottleNum, atkNum, defNum, dexNum)
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError(GetType()+ "构造函数不允许多次调用");
        }
    }

    /// <summary>
    /// 本类实例
    /// </summary>
    /// <returns></returns>
    public static PlayerPackageProxy GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }
        else
        {
            Debug.LogError("先调用构造函数");
            return null;
        }
    }

    #region 血瓶的方法

    /// <summary>
    /// 增加血瓶数量
    /// </summary>
    /// <param name="bottleNum">数量</param>
    public void IncreaseBloodBottleNum(int bottleNum)
    {
        base.IBloodBottleNum += Mathf.Abs(bottleNum);
    }

    /// <summary>
    /// 减少血瓶数量
    /// </summary>
    /// <param name="bottleNum">数量</param>
    public void DecreaseBloodBottleNum(int bottleNum)
    {
        //背包中的血瓶是否足够
        if (base.IBloodBottleNum - Mathf.Abs(bottleNum) >= 0)
        {
            base.IBloodBottleNum -= Mathf.Abs(bottleNum);
        }
    }

    /// <summary>
    /// 得到当前的血瓶数量
    /// </summary>
    public int GetCurrentBloodBottleNum()
    {
        return base.IBloodBottleNum;
    }

    #endregion

    #region 魔法瓶的方法
    /// <summary>
    /// 增加魔法瓶数量
    /// </summary>
    /// <param name="bottleNum">数量</param>
    public void IncreaseMagicBottleNum(int bottleNum)
    {
        base.IMagicBottleNum += Mathf.Abs(bottleNum);
    }

    /// <summary>
    /// 减少魔法瓶数量
    /// </summary>
    /// <param name="bottleNum">数量</param>
    public void DecreaseMagicBottleNum(int bottleNum)
    {
        //背包中的血瓶是否足够
        if (base.IMagicBottleNum - Mathf.Abs(bottleNum) >= 0)
        {
            base.IMagicBottleNum -= Mathf.Abs(bottleNum);
        }
    }

    /// <summary>
    /// 得到当前的魔法瓶数量
    /// </summary>
    public int GetCurrentMagicBottleNum()
    {
        return base.IMagicBottleNum;
    }
    #endregion

    #region 攻击道具方法

    /// <summary>
    /// 增加攻击道具
    /// </summary>
    /// <param name="atkNum">数量</param>
    public void IncreaseATKPropNum(int atkNum)
    {
        base.IATKPropNum += Mathf.Abs(atkNum);
    }

    /// <summary>
    /// 减少攻击道具
    /// </summary>
    /// <param name="atkNum">数量</param>
    public void DecreaseATKPropNum(int atkNum)
    {
        if (base.IATKPropNum - Mathf.Abs(atkNum) >= 0)
        {
            base.IATKPropNum -= Mathf.Abs(atkNum);
        }
    }

    /// <summary>
    /// 得到当前的血瓶数量
    /// </summary>
    public int GetCurrentATKPropNum()
    {
        return base.IATKPropNum;
    }

    #endregion

    #region 防御道具方法

    /// <summary>
    /// 增加防御道具
    /// </summary>
    /// <param name="defNum">数量</param>
    public void IncreaseDEFPropNum(int defNum)
    {
        base.IDEFPropNum += Mathf.Abs(defNum);
    }

    /// <summary>
    /// 减少防御道具
    /// </summary>
    /// <param name="defNum">数量</param>
    public void DecreaseDEFPropNum(int defNum)
    {
        //背包中的血瓶是否足够
        if (base.IDEFPropNum - Mathf.Abs(defNum) >= 0)
        {
            base.IDEFPropNum -= Mathf.Abs(defNum);
        }
    }

    /// <summary>
    /// 得到当前防御道具数量
    /// </summary>
    public int GetCurrentDEFPropNum()
    {
        return base.IDEFPropNum;
    }

    #endregion

    #region 敏捷度道具方法

    /// <summary>
    /// 增加敏捷度道具
    /// </summary>
    /// <param name="dexNum">数量</param>
    public void IncreaseDEXPropNum(int dexNum)
    {
        base.IDEXPropNum += Mathf.Abs(dexNum);
    }

    /// <summary>
    /// 减少敏捷度道具
    /// </summary>
    /// <param name="dexNum">数量</param>
    public void DecreaseDEXPropNum(int dexNum)
    {
        //背包中的血瓶是否足够
        if (base.IDEXPropNum - Mathf.Abs(dexNum) >= 0)
        {
            base.IDEXPropNum -= Mathf.Abs(dexNum);
        }
    }

    /// <summary>
    /// 得到当前的血瓶数量
    /// </summary>
    public int GetCurrentDEXPropNum()
    {
        return base.IDEXPropNum;
    }

    #endregion
}
