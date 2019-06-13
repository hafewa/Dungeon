using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//负责主城界面中的任务系统面板的视图操作，脚本挂载在MissionPanel下的ScriptHolder
public class View_MissionPanel : MonoBehaviour
{
	//转到下一个场景（副本）
    public void EnterLevelTowScene()
    {
		Ctrl_MissionPanel.Instance.EnterLevelTowScene();
    }
}
