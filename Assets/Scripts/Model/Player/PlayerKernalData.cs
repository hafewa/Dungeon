using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*模型层玩家的核心数据
 *这个类用于提供玩家核心数据的存取数值
 * */

public class PlayerKernalData
{
    //定义事件 玩家的核心数值事件
    public static event del_PlayerKernalModel EvePlayerKernalData;  //玩家核心数值

    private float _Health;  //血条
    private float _Magic;   //魔法值
    private float _Attack;  //攻击力
    private float _Defence; //防御力
    private float _Dexterity;   //敏捷度

    //玩家各数据的最大数值
    private float _MaxHealth;
    private float _MaxMagic;
    private float _MaxAttack;
    private float _MaxDefence;
    private float _MaxDexterity;

    private float _AttackByProp;    //道具增加的攻击力
    private float _DefenceByProp;   //道具增加的防御力
    private float _DexterityByProp;  //道具增加的敏捷度

    #region 属性信息

    public float Health
    {
        get
        {
            return _Health;
        }

        set
        {
            _Health = value;

            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv=new KeyValueUpdate("Health",Health);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float Magic
    {
        get
        {
            return _Magic;
        }

        set
        {
            _Magic = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Magic", Magic);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float Attack
    {
        get
        {
            return _Attack;
        }

        set
        {
            _Attack = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Attack", Attack);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float Defence
    {
        get
        {
            return _Defence;
        }

        set
        {
            _Defence = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Defence", Defence);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float Dexterity
    {
        get
        {
            return _Dexterity;
        }

        set
        {
            _Dexterity = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("Dexterity", Dexterity);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float MaxHealth
    {
        get
        {
            return _MaxHealth;
        }

        set
        {
            _MaxHealth = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("MaxHealth", MaxHealth);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float MaxMagic
    {
        get
        {
            return _MaxMagic;
        }

        set
        {
            _MaxMagic = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("MaxMagic", MaxMagic);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float MaxAttack
    {
        get
        {
            return _MaxAttack;
        }

        set
        {
            _MaxAttack = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("MaxAttack", MaxAttack);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float MaxDefence
    {
        get
        {
            return _MaxDefence;
        }

        set
        {
            _MaxDefence = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("MaxDefence", MaxDefence);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float MaxDexterity
    {
        get
        {
            return _MaxDexterity;
        }

        set
        {
            _MaxDexterity = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("MaxDexterity", MaxDexterity);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float AttackByProp
    {
        get
        {
            return _AttackByProp;
        }

        set
        {
            _AttackByProp = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("AttackByProp", AttackByProp);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float DefenceByProp
    {
        get
        {
            return _DefenceByProp;
        }

        set
        {
            _DefenceByProp = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("DefenceByProp", DefenceByProp);

                EvePlayerKernalData(kv);
            }
        }
    }
    public float DexterityByProp
    {
        get
        {
            return _DexterityByProp;
        }

        set
        {
            _DexterityByProp = value;
            //事件调用
            if (EvePlayerKernalData != null)
            {
                KeyValueUpdate kv = new KeyValueUpdate("DexterityByProp", DexterityByProp);

                EvePlayerKernalData(kv);
            }
        }
    }

    #endregion

    private PlayerKernalData(){ }

    //公共构造函数
    public PlayerKernalData(float health, float magic, float attack, float defence, float dexterity, float maxHealth,
        float maxMagic, float maxAttack, float maxDefence, float maxDexterity, float attackByProp, float defenceByProp,
        float dexterityByProp)
    {
        this._Health = health;
        this._Attack = attack;
        this._Magic = magic;
        this._Defence = defence;
        this._Dexterity = dexterity;

        this._MaxAttack = maxAttack;
        this._MaxDefence = maxDefence;
        this._MaxDexterity = maxDexterity;
        this._MaxHealth = maxHealth;
        this._MaxMagic = maxMagic;

        this._AttackByProp = attackByProp;
        this._DefenceByProp = defenceByProp;
        this._DexterityByProp = dexterityByProp;
    }
}
