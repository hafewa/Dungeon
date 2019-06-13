using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//提供玩家的扩展数据    例如经验值、等级、杀敌数量等
public class PlayerExternalData
{
    //定义事件 玩家的扩展数值事件
    public static event del_PlayerKernalModel EvePlayerExternalData;  //玩家扩展数值

    private int _Experience;    //经验值
    private int _KillNumber;    //杀敌数量
    private int _Level;         //当前的等级
    private int _Gold;          //金币
    private int _Diamonds;      //钻石

    #region 属性

    public int Experience
    {
        get
        {
            return _Experience;
        }

        set
        {
            _Experience = value;
            //事件调用
            if (EvePlayerExternalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Experience", Experience);

                EvePlayerExternalData(kv);
            }
        }
    }

    public int KillNumber
    {
        get
        {
            return _KillNumber;
        }

        set
        {
            _KillNumber = value;
            //事件调用
            if (EvePlayerExternalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("KillNumber", KillNumber);

                EvePlayerExternalData(kv);
            }
        }
    }

    public int Level
    {
        get
        {
            return _Level;
        }

        set
        {
            _Level = value;
            //事件调用
            if (EvePlayerExternalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Level", Level);

                EvePlayerExternalData(kv);
            }
        }
    }

    public int Gold
    {
        get
        {
            return _Gold;
        }

        set
        {
            _Gold = value;
            //事件调用
            if (EvePlayerExternalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Gold", Gold);

                EvePlayerExternalData(kv);
            }
        }
    }

    public int Diamonds
    {
        get
        {
            return _Diamonds;
        }

        set
        {
            _Diamonds = value;
            //事件调用
            if (EvePlayerExternalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Diamonds", Diamonds);

                EvePlayerExternalData(kv);
            }
        }
    }

    #endregion

    private PlayerExternalData() { }

    public PlayerExternalData(int experience, int killNumber, int level, int gold, int diamonds)
    {
        this._Experience = experience;
        this._KillNumber = killNumber;
        this._Level = level;
        this._Gold = gold;
        this._Diamonds = diamonds;
    }

}
