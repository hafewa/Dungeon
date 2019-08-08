using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 根据背包系统中模型层的数据进行相关的操作,显示背包系统的道具
/// </summary>
public class View_Package : MonoBehaviour {
    //定义道具对象
    public GameObject GoBloodBottle;        //血瓶

    public GameObject GoMagicBottle;        //魔法瓶

    public GameObject GoATKProp;        //攻击道具

    public GameObject GoDEFProp;        //防御道具

    public GameObject GoDEXProp;        //敏捷度道具
    //定义道具的数量
    public Text TextBloodBottleNum;     //血瓶数量
    public Text TextMagicBottleNum;     //魔法瓶数量


    void Awake()
    {
        //事件注册
        PlayerPackageData.evePlayerPackageData += DisplayBloodBottle;
        PlayerPackageData.evePlayerPackageData += DisplayMagicBottle;
        PlayerPackageData.evePlayerPackageData += DisplayATKProp;
        PlayerPackageData.evePlayerPackageData += DisplayDEFProp;
        PlayerPackageData.evePlayerPackageData += DisplayDEXProp;
    }

    /// <summary>
    /// 显示血瓶以及数量
    /// </summary>
    /// <param name="kv"></param>
    public void DisplayBloodBottle(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("IBloodBottleNum"))
        {
            if (GoBloodBottle && TextBloodBottleNum)
            {
                //如果道具数量大于等于1，则显示道具
                if (Convert.ToInt32(kv.Value) >= 1)
                {
                    GoBloodBottle.SetActive(true);
                    //显示血瓶的数量
                    TextBloodBottleNum.text = kv.Value.ToString();
                }
            }
        }
    }

    /// <summary>
    /// 显示魔法瓶以及数量
    /// </summary>
    /// <param name="kv"></param>
    public void DisplayMagicBottle(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("IMagicBottleNum"))
        {
            if (GoMagicBottle && TextMagicBottleNum)
            {
                if (Convert.ToInt32(kv.Value) >= 1)
                {
                    GoMagicBottle.SetActive(true);
                    TextMagicBottleNum.text = kv.Value.ToString();
                }
            }
        }
    }

    /// <summary>
    /// 显示攻击力道具
    /// </summary>
    /// <param name="kv"></param>
    public void DisplayATKProp(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("IATKPropNum"))
        {
            if (GoATKProp)
            {
                if(Convert.ToInt32(kv.Value)>=1)
                    GoATKProp.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 显示防御力道具
    /// </summary>
    /// <param name="kv"></param>
    public void DisplayDEFProp(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("IDEFPropNum"))
        {
            if (GoDEFProp)
            {
                if (Convert.ToInt32(kv.Value) >= 1)
                    GoDEFProp.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 显示敏捷度道具
    /// </summary>
    /// <param name="kv"></param>
    public void DisplayDEXProp(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("IDEXPropNum"))
        {
            if (GoDEXProp)
            {
                if (Convert.ToInt32(kv.Value) >= 1)
                    GoDEXProp.SetActive(true);
            }
        }
    }

}
