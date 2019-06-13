using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 测试对话系统UI界面
/// </summary>
public class TestDialogUI : MonoBehaviour {
	// Use this for initialization
	void Start ()
	{
	    //DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, 1);//默认显示第一条
	    DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, 6);//默认显示第一条
    }

    /// <summary>
    /// 显示下一条信息，注册在继续按钮上
    /// </summary>
    public void DisplayNextDialogInfo()
    {
        //bool bResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, 1);
        bool bResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, 6);
        if (bResult)
        {
            Log.Write(GetType()+"对话结束");
        }
        Log.SynLogArrayToFile();
    }
}
