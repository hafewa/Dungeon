using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 背包系统子类，关于敏捷力道具的物品
/// </summary>
public class Dexterity_Items : BasePackage, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    //定义目标格子名称
    public string strTargetName = "Image-DEX";
    //主角增加防御力
    public float AddHeroMaxDEX = 10;

    void Start()
    {
        //赋值目标格子名称
        base.strMoveToTagetName = this.strTargetName;
        //运行父类的初始化方华
        base.RunInstanceByChild();
    }

    /// <summary>
    /// 重载特定的装备方法
    /// </summary>
    protected override void InvokeMethodByEndDrag()
    {
        //主角增加攻击力
        PlayerKernalDataProxy.GetInstance().IncreaseMaxDexterity(AddHeroMaxDEX);
        PlayerKernalDataProxy.GetInstance().UpdateDexterityValue();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        base.BaseOnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        base.BaseOnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        base.BaseOnEndDrag(eventData);
    }
}
