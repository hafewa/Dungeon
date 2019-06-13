using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_SkillDetailInfo : MonoBehaviour
{
    //技能对象
    public Image ATK1;
    public Image ATK2;
    public Image ATK3;
    public Image ATK4;
    public Image ATK5;
    //介绍信息
    public Text SkillName;
    public Text SkillDetail;

    private void Awake()
    {
        RegisterClickEvent();
    }

    private void Start()
    {

        SkillName.text = "普通攻击";
        SkillDetail.text = "普通攻击的详细介绍xxxxxxxx";

    }

    //点击事件注册
    private void RegisterClickEvent()
    {
        if (ATK1 != null)
        {
            EventTrrigerListener.Get(ATK1.gameObject).onClick += OnClickATK1;
        }
        if (ATK2 != null)
        {
            EventTrrigerListener.Get(ATK2.gameObject).onClick += OnClickATK2;
        }
        if (ATK3 != null)
        {
            EventTrrigerListener.Get(ATK3.gameObject).onClick += OnClickATK3;
        }
        if (ATK4 != null)
        {
            EventTrrigerListener.Get(ATK4.gameObject).onClick += OnClickATK4;
        }
        if (ATK5 != null)
        {
            EventTrrigerListener.Get(ATK5.gameObject).onClick += OnClickATK5;
        }

    }

    private void OnClickATK1(GameObject go)
    {
        if (go != null)
        {
            SkillName.text = "普通攻击";
            SkillDetail.text = "普通攻击的详细介绍xxxxxxxx";
        }
    }
    private void OnClickATK2(GameObject go)
    {
        if (go != null)
        {
            SkillName.text = "技能1";
            SkillDetail.text = "技能1的介绍。。。。。。。。。。";
        }
    }
    private void OnClickATK3(GameObject go)
    {
        if (go != null)
        {
            SkillName.text = "技能2";
            SkillDetail.text = "技能2的介绍。。。。。。。。。。";
        }
    }
    private void OnClickATK4(GameObject go)
    {
        if (go != null)
        {
            SkillName.text = "技能3";
            SkillDetail.text = "技能3的介绍。。。。。。。。。。";
        }
    }
    private void OnClickATK5(GameObject go)
    {
        if (go != null)
        {
            SkillName.text = "技能4";
            SkillDetail.text = "技能4的介绍。。。。。。。。。。";
        }
    }
}
