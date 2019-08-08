using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *通用触发器脚本
 *功能：
 *      1.NPC /敌人触发对话
 *      2.存盘/继续
 *      3.加载启用特定的脚本（例如产生敌人）
 *      4.触发UI“对话/确认框”等
 */
public class TrrigerCommonEvent : MonoBehaviour {
    //定义事件
    public static event del_CommonTrriger eveCommonTrriger;

    //对话类型
    public CommonTrrigerType trrigerType = CommonTrrigerType.None;

    /// <summary>
    /// 进入触发检测
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
        {
            //事件调用
            if (eveCommonTrriger != null)
            {
                eveCommonTrriger(trrigerType);
            }
        }
    }
    
}

//定义委托
public delegate void del_CommonTrriger(CommonTrrigerType commonType);

/// <summary>
/// 触发事件的类型枚举
/// </summary>
public enum CommonTrrigerType
{
    None,
    NPC1_Dialog,                 //NPC对话
    NPC2_Dialog,
    NPC3_Dialog,
    NPC4_Dialog,
    NPC5_Dialog,
    Enemy1_Dialog,              //敌人对话
    Enemy2_Dialog,
    Enemy3_Dialog,
    SaveDataOrScenes,           //存盘
    LoadDataOrScenes,           //调用
    EnableScript1,              //加载或启用特定脚本
    EnableScript2,
    ActiveConfimWindows,        //触发确认窗体
    ActiveDialogWindows         //触发对话窗体
}
