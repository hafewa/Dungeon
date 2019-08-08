using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店系统的逻辑层代码
/// </summary>
public class Ctrl_MarketPanel : BaseControl
{
    public static Ctrl_MarketPanel Instance;

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 购买钻石
    /// </summary>
    public bool PruchaseDiamond()
    {
        //需要对接APP Store的收费SDK，进行玩家的实际扣费
        PlayerExternalDataProxy.GetInstance().AddDiamonds(10);
        return true;
    }

    /// <summary>
    /// 购买金币
    /// </summary>
    public bool PruchaseGolds()
    {
        bool bResult=false;           //购买是否成功的返回值

        //购买十个金币消耗一个钻石
        bool bFlat = PlayerExternalDataProxy.GetInstance().DecreaseDiamonds(1);
        if (bFlat)
        {
            PlayerExternalDataProxy.GetInstance().AddGold(10);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }


    /// <summary>
    /// 购买血瓶
    /// </summary>
    public bool PruchaseBloodBottle()
    {
        bool bResult=false;
        bool bFlat = PlayerExternalDataProxy.GetInstance().DecreaseGolds(10);
        if (bFlat)
        {
            //增加血瓶数量
            PlayerPackageProxy.GetInstance().IncreaseBloodBottleNum(5);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

    /// <summary>
    /// 购买魔法瓶
    /// </summary>
    public bool PruchaseMagicBottle()
    {
        bool bResult=false;
        bool bFlat = PlayerExternalDataProxy.GetInstance().DecreaseGolds(20);
        if (bFlat)
        {
            //增加血瓶数量
            PlayerPackageProxy.GetInstance().IncreaseMagicBottleNum(9);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

    /// <summary>
    /// 购买攻击力道具
    /// </summary>
    public bool PruchaseAttackProp()
    {
        bool bResult = false;
        bool bFlat = PlayerExternalDataProxy.GetInstance().DecreaseGolds(100);
        if (bFlat)
        {
            PlayerPackageProxy.GetInstance().IncreaseATKPropNum(1);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

    /// <summary>
    /// 购买防御力道具
    /// </summary>
    public bool PruchaseDefenceProp()
    {
        bool bResult=false;
        bool bFlat = PlayerExternalDataProxy.GetInstance().DecreaseGolds(120);
        if (bFlat)
        {
            PlayerPackageProxy.GetInstance().IncreaseDEFPropNum(1);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

    /// <summary>
    /// 购买敏捷度道具
    /// </summary>
    public bool PruchaseDexterityProp()
    {
        bool bResult=false;
        bool bFlat = PlayerExternalDataProxy.GetInstance().DecreaseGolds(140);
        if (bFlat)
        {
            PlayerPackageProxy.GetInstance().IncreaseDEXPropNum(1);
            bResult = true;
        }
        else
        {
            bResult = false;
        }

        return bResult;
    }

}
