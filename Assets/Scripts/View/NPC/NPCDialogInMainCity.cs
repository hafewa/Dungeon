using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于管理主城场景NPC的对话
/// </summary>
public class NPCDialogInMainCity : MonoBehaviour
{
    public GameObject goDialogPanel;        //对话面板
    private Image _ImgBgDialog;             //背景对话贴图
                                            //当前“触发对话目标”
    private CommonTrrigerType _CommonTrrigerType = CommonTrrigerType.None;

    void Start()
    {
        //背景贴图
        _ImgBgDialog = this.transform.parent.Find("Background").GetComponent<Image>();
        //注册触发器对话系统
        RigisterTrrigerDialog();
        //注册背景贴图
        RigisterBgImage();
        //初始时设置背景贴图不显示
        _ImgBgDialog.gameObject.SetActive(false);
    }

    #region 对话准备阶段
    //注册触发器，对话系统
    private void RigisterTrrigerDialog()
    {
        TrrigerCommonEvent.eveCommonTrriger += StarDialogPrepare;

    }

    //开始对话准备
    private void StarDialogPrepare(CommonTrrigerType common)
    {
        switch (common)
        {
            case CommonTrrigerType.None:
                break;
            case CommonTrrigerType.NPC1_Dialog:
                ActiveNPC1_Dialog();
                break;
            case CommonTrrigerType.NPC2_Dialog:
                ActiveNPC2_Dialog();
                break;
            default:
                break;
        }
    }

    //激活NPC1对话
    private void ActiveNPC1_Dialog()
    {
        //给NPC1，动态加载贴图
        LoadNPC1_Texture();
        //赋值当前状态
        _CommonTrrigerType = CommonTrrigerType.NPC1_Dialog;
        //禁用ET
        View_PlayerInfoReseponse.Instance.HidenET();
        //显示对话UI面板
        goDialogPanel.SetActive(true);
        //显示首句对话
        DisplayNextDialog(5);
    }

    //激活NPC2对话
    private void ActiveNPC2_Dialog()
    {
        //给NPC2，动态加载贴图
        LoadNPC2_Texture();
        //赋值当前状态
        _CommonTrrigerType = CommonTrrigerType.NPC1_Dialog;
        //禁用ET
        View_PlayerInfoReseponse.Instance.HidenET();
        //显示对话UI面板
        goDialogPanel.SetActive(true);
        //显示首句对话
        DisplayNextDialog(6);

    }

    //加载NPC1的贴图
    private void LoadNPC1_Texture()
    {
        DialogUIMgr.Instance.SpritNPC_Right[0] = ResourcesManager.GetInstance().LoadResources<Sprite>("Textrure/NPCBW_1", true);
        DialogUIMgr.Instance.SpritNPC_Right[1] = ResourcesManager.GetInstance().LoadResources<Sprite>("Textrure/NPCTrue_1", true);

    }

    //加载NPC2的贴图
    private void LoadNPC2_Texture()
    {
        DialogUIMgr.Instance.SpritNPC_Right[0] = ResourcesManager.GetInstance().LoadResources<Sprite>("Textrure/NPCBW_2", true);
        DialogUIMgr.Instance.SpritNPC_Right[1] = ResourcesManager.GetInstance().LoadResources<Sprite>("Textrure/NPCTrue_2", true);

    }

    #endregion

    #region 正式对话阶段
    //注册背景贴图
    private void RigisterBgImage()
    {
        if (_ImgBgDialog != null)
        {
            EventTrrigerListener.Get(_ImgBgDialog.gameObject).onClick += DisplayDialogByNPC;
        }
    }

    //显示NPC对话
    private void DisplayDialogByNPC(GameObject go)
    {
        switch (_CommonTrrigerType)
        {
            case CommonTrrigerType.None:

                break;
            case CommonTrrigerType.NPC1_Dialog:
                DisplayNextDialog(5);
                break;
            case CommonTrrigerType.NPC2_Dialog:
                DisplayNextDialog(6);
                break;
            default:
                break;
        }
    }

    //显示下一条对话信息，参数是对话编号
    private void DisplayNextDialog(int sectionNum)
    {
        bool bResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, sectionNum);
        if (bResult)        //返回的结果为true则代表对话结束
        {
            //对话结束，关闭对话面板
            goDialogPanel.SetActive(false);
            //启用ET
            View_PlayerInfoReseponse.Instance.DisplayET();
        }

    }

    #endregion


}
