using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_MarketPanel : MonoBehaviour {

	//道具说明显示
    public Text Text_Diamonds;
    public Text Text_Golds;
    public Text Text_BloodBottle;
    public Text Text_MagicBottle;
    public Text Text_AtackProp;
    public Text Text_DefenceProp;
    public Text Text_DexterityProp;

    //相应的按键
    public Button Btn_Diamonds;
    public Button Btn_Golds;
    public Button Btn_BloodBottle;
    public Button Btn_MagicBottle;
    public Button Btn_AtackProp;
    public Button Btn_DefenceProp;
    public Button Btn_DexterityProp;

    //具体道具的文字说明
    public Text TextGoodsDescription;

    void Awake()
    {
        //注册相关按钮
        RigesterTxtAndBtn();
    }

    private void RigesterTxtAndBtn()
    {
        //贴图的注册
        if (Text_Diamonds != null)
        {
            EventTrrigerListener.Get(Text_Diamonds.gameObject).onClick += DisplayDiamonds;
        }

        if (Text_Golds != null)
        {
            EventTrrigerListener.Get(Text_Golds.gameObject).onClick += DisplayGolds;
        }

        if (Text_BloodBottle != null)
        {
            EventTrrigerListener.Get(Text_BloodBottle.gameObject).onClick += DisplayBloodBottle;
        }

        if (Text_MagicBottle != null)
        {
            EventTrrigerListener.Get(Text_MagicBottle.gameObject).onClick += DisplayMagicBottle;
        }

        if (Text_AtackProp != null)
        {
            EventTrrigerListener.Get(Text_AtackProp.gameObject).onClick += DisplayAttackProp;
        }

        if (Text_DefenceProp != null)
        {
            EventTrrigerListener.Get(Text_DefenceProp.gameObject).onClick += DisplayDefenceProp;
        }

        if (Text_DexterityProp != null)
        {
            EventTrrigerListener.Get(Text_DexterityProp.gameObject).onClick += DisplayDexterityProp;
        }
        //按钮的注册
        if (Btn_Diamonds != null)
        {
            EventTrrigerListener.Get(Btn_Diamonds.gameObject).onClick += OnResponceDiamondClick;
        }

        if (Btn_Golds != null)
        {
            EventTrrigerListener.Get(Btn_Golds.gameObject).onClick += OnResponceGoldsClick;
        }

        if (Btn_BloodBottle != null)
        {
            EventTrrigerListener.Get(Btn_BloodBottle.gameObject).onClick += OnResponceBloodBottleClick;
        }

        if (Btn_MagicBottle != null)
        {
            EventTrrigerListener.Get(Btn_MagicBottle.gameObject).onClick += OnResponceMagicBottleClick;
        }

        if (Btn_AtackProp != null)
        {
            EventTrrigerListener.Get(Btn_AtackProp.gameObject).onClick += OnResponceAttackPropClick;
        }

        if (Btn_DefenceProp != null)
        {
            EventTrrigerListener.Get(Btn_DefenceProp.gameObject).onClick += OnResponceDefencePropClick;
        }

        if (Btn_DexterityProp != null)
        {
            EventTrrigerListener.Get(Btn_DexterityProp.gameObject).onClick += OnResponceDexterityPropClick;
        }
    }

    #region 商品的显示信息
    //点击钻石的显示信息
    private void DisplayDiamonds(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "钻石的详细描述";
        }
    }
    //点击金币显示信息
    private void DisplayGolds(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "金币的详细描述";
        }
    }
    //血瓶信息
    private void DisplayBloodBottle(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "血瓶的详细描述";
        }
    }
    //魔法值信息
    private void DisplayMagicBottle(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "魔法瓶的详细信息";
        }
    }
    //攻击力道具信息
    private void DisplayAttackProp(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "攻击力道具的详细信息";
        }
    }
    //防御力信息
    private void DisplayDefenceProp(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "防御力道具的详细信息";
        }
    }
    //敏捷度道具信息
    private void DisplayDexterityProp(GameObject go)
    {
        if (go != null)
        {
            TextGoodsDescription.text = "敏捷度道具的详细道具";
        }
    }
    #endregion

    #region 商品的点击方法
    //钻石对应的按钮
    private void OnResponceDiamondClick(GameObject go)
    {
        if (go == Btn_Diamonds.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseDiamond();

            if (bResult)
            {
                TextGoodsDescription.text = "购买成钻石功";
            }
            else
            {
                TextGoodsDescription.text = "购买钻石失败";
            }
        }
    }
    //金币对应的按钮
    private void OnResponceGoldsClick(GameObject go)
    {
        if (go == Btn_Golds.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseGolds();
            if (bResult)
            {
                TextGoodsDescription.text = "购买金币成功";
            }
            else
            {
                TextGoodsDescription.text = "购买金币失败";
            }
        }
    }
    //血瓶对应的按钮
    private void OnResponceBloodBottleClick(GameObject go)
    {
        if (go == Btn_BloodBottle.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseBloodBottle();
            if (bResult)
            {
                TextGoodsDescription.text = "购买血瓶成功";
            }
            else
            {
                TextGoodsDescription.text = "购买血瓶失败";
            }
        }
    }
    //魔法值对应的按钮
    private void OnResponceMagicBottleClick(GameObject go)
    {
        if (go == Btn_MagicBottle.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseMagicBottle();
            if (bResult)
            {
                TextGoodsDescription.text = "购买魔法瓶成功";
            }
            else
            {
                TextGoodsDescription.text = "购买魔法瓶失败";
            }
        }
    }
    //攻击力对应的按钮
    private void OnResponceAttackPropClick(GameObject go)
    {
        if (go == Btn_AtackProp.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseAttackProp();
            if (bResult)
            {
                TextGoodsDescription.text = "购买攻击力道具成功";
            }
            else
            {
                TextGoodsDescription.text = "购买攻击力道具失败";
            }
        }
    }
    //防御力对应的按钮
    private void OnResponceDefencePropClick(GameObject go)
    {
        if (go == Btn_DefenceProp.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseDefenceProp();
            if (bResult)
            {
                TextGoodsDescription.text = "购买防御力道具成功";
            }
            else
            {
                TextGoodsDescription.text = "购买防御力道具失败";
            }
        }
    }
    //敏捷度对应的按钮
    private void OnResponceDexterityPropClick(GameObject go)
    {
        if (go == Btn_DexterityProp.gameObject)
        {
            //返回结果
            bool bResult = false;
            //调用商城逻辑层代码
            bResult = Ctrl_MarketPanel.Instance.PruchaseDexterityProp();
            if (bResult)
            {
                TextGoodsDescription.text = "购买敏捷度道具成功";
            }
            else
            {
                TextGoodsDescription.text = "购买敏捷度道具失败";
            }
        }
    }
    #endregion
}
