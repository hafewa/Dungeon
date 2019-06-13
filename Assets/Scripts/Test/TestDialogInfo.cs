using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 测试对话系统信息
/// </summary>
public class TestDialogInfo : MonoBehaviour
{
    public Text TextSide;
    public Text TextPersonName;
    public Text TextPersonContent;

    /// <summary>
    /// 显示对话信息，方法注册在点击继续按钮上
    /// </summary>
    public void DisplayNextDialogInfo()
    {
        DialogSide dialogSide = DialogSide.None;
        string strDialogPersonName;
        string strDialogPersonContent;

        bool bResult= DialogDataMgr.GetInstance()
            .GetNextDialogInfoRecoder(1, out dialogSide, out strDialogPersonName, out strDialogPersonContent);
        if (bResult)
        {
            switch (dialogSide)
            {
                case DialogSide.None:
                    break;
                case DialogSide.HeroSide:
                    TextSide.text = "Hero";
                    break;
                case DialogSide.NPCSide:
                    TextSide.text = "NPC";
                    break;
                default:
                    break;
            }

            TextPersonName.text = strDialogPersonName;
            TextPersonContent.text = strDialogPersonContent;
        }
        else
        {
            TextPersonName.text = "没有输出数据了";
            TextPersonContent.text = "没有输出数据";
        }
        Log.Write(GetType()+"");
        Log.SynLogArrayToFile();//每点击一次，同步到日志文件中
    }
}
