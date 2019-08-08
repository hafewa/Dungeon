using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型层，玩家的背包数据
/// </summary>
public class PlayerPackageData {
    //定义事件
    public static del_PlayerKernalModel evePlayerPackageData;       //玩家背包数据事件

    //字段
    private int _iBloodBottleNum;       //血瓶的数量
    private int _iMagicBottleNum;       //魔法瓶数量
    private int _iATKPropNum;           //攻击型道具数量
    private int _iDEFPropNum;           //防御型道具数量
    private int _iDEXPropNum;           //敏捷度道具

    //私有的构造函数
    private PlayerPackageData()
    {
    }

    public PlayerPackageData(int bloodBottleNum,int magicBottleNum,int atkNum,int defNum,int dexNum)
    {
        this._iBloodBottleNum = bloodBottleNum;
        this._iMagicBottleNum = magicBottleNum;
        this._iATKPropNum = atkNum;
        this._iDEFPropNum = defNum;
        this._iDEXPropNum = dexNum;
    }

    #region 属性

    public int IBloodBottleNum
    {
        get { return _iBloodBottleNum; }

        set
        {
            _iBloodBottleNum = value;

            //事件调用
            if (evePlayerPackageData != null)
            {
                KeyValueUpdate kv=new KeyValueUpdate("IBloodBottleNum", IBloodBottleNum);
                evePlayerPackageData(kv);
            }
        }
    }

    public int IMagicBottleNum
    {
        get
        {
            return _iMagicBottleNum;
        }

        set
        {
            _iMagicBottleNum = value;
            //事件调用
            if (evePlayerPackageData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("IMagicBottleNum", IMagicBottleNum);
                evePlayerPackageData(kv);
            }
        }
    }

    public int IATKPropNum
    {
        get
        {
            return _iATKPropNum;
        }

        set
        {
            _iATKPropNum = value;
            //事件调用
            if (evePlayerPackageData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("IATKPropNum", IATKPropNum);
                evePlayerPackageData(kv);
            }
        }
    }

    public int IDEFPropNum
    {
        get
        {
            return _iDEFPropNum;
        }

        set
        {
            _iDEFPropNum = value;

            //事件调用
            if (evePlayerPackageData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("IDEFPropNum", IDEFPropNum);
                evePlayerPackageData(kv);
            }
        }
    }

    public int IDEXPropNum
    {
        get
        {
            return _iDEXPropNum;
        }

        set
        {
            _iDEXPropNum = value;

            //事件调用
            if (evePlayerPackageData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("IDEXPropNum", IDEXPropNum);
                evePlayerPackageData(kv);
            }
        }
    }


    #endregion

}
