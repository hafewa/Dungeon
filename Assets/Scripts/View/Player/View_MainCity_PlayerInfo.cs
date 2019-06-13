using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 玩家主城信息响应  挂载在 MainCityCanvas->PanelGroup->ScriptHolder
	作用：主城场景中，关于玩家各种面板的显示与隐藏（任务系统、背包系统、装备系统）
 */
public class View_MainCity_PlayerInfo : MonoBehaviour
{
    public GameObject GoPlayerSkillPanel;       //玩家的技能系统面板
    public GameObject GoPlayerMissionPanel;     //玩家的任务系统面板
    public GameObject GoPlayerMarketPanel;      //商城系统面板
    public GameObject GoPlayerPackagePanel;     //背包系统面板


    ///<summary>
    ///显示英雄的角色信息
    ///</summary>
    public void DisplayPlayerInfo()
    {
        View_PlayerInfoReseponse.Instance.DisplayHeroDetailInfo();
    }

    ///<summary>
    ///隐藏英雄的角色信息
    ///</summary>
    public void HidePlayerInfo()
    {
        View_PlayerInfoReseponse.Instance.HideHeroDetailInfo();
    }

    ///<summary>
    ///显示技能系统面板
    ///</summary>
    public void DisplaySkillPanel()
    {
        BeforOpenPanel(GoPlayerSkillPanel);
        GoPlayerSkillPanel.SetActive(true);
    }
    ///<summary>
    ///隐藏技能系统面板
    ///</summary>
    public void HideSkillPanel()
    {
        BeforClosePanel();
        GoPlayerSkillPanel.SetActive(false);
    }

    ///<summary>
    ///显示任务系统面板
    ///</summary>
    public void DisplayMissionPanel()
    {
        BeforOpenPanel(GoPlayerMissionPanel);
        GoPlayerMissionPanel.SetActive(true);

    }

    ///<summary>
    ///隐藏任务系统面板
    ///</summary>
    public void HideMissioPanel()
    {

        BeforClosePanel();
        GoPlayerMissionPanel.SetActive(false);
    }

    ///<summary>
    ///显示商城系统面板
    ///</summary>
    public void DisplayMarketPanel()
    {

        BeforOpenPanel(GoPlayerMarketPanel);
        GoPlayerMarketPanel.SetActive(true);
    }
    ///<summary>
    ///隐藏商城系统面板
    ///</summary>
    public void HideMarketPanel()
    {
        BeforClosePanel();
        GoPlayerMarketPanel.SetActive(false);
    }
    ///<summary>
    ///显示背包系统面板
    ///</summary>
    public void DisplayPackagePanel()
    {
        BeforOpenPanel(GoPlayerPackagePanel);
        GoPlayerPackagePanel.SetActive(true);
    }
    ///<summary>
    ///隐藏背包系统面板
    ///</summary>
    public void HidePackagePanel()
    {
        BeforClosePanel();
        GoPlayerPackagePanel.SetActive(false);
    }

    //在打开UI窗体之前的预处理
    private void BeforOpenPanel(GameObject goDisplayPanel)
    {
        //禁用ET
        View_PlayerInfoReseponse.Instance.HidenET();
        //窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().SetMaskWindow(goDisplayPanel);
    }

    //关闭窗体之前的预处理
    private void BeforClosePanel()
    {
        //开启ET
        View_PlayerInfoReseponse.Instance.DisplayET();
        //取消窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().CancelMaskWindow();
    }
}
