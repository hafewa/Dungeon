using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*新手引导模块
	触发虚拟摇杆

 */
public class TrrigerOperET : MonoBehaviour,IGuideTrriger
{

    public static TrrigerOperET Instance;
    
    public GameObject GoGuideUIBackground;      //新手指引UI背景对象
    public Image ImgGuideET;                    //新手指导ET贴图

    private bool _IsNextDialogRecoder = false;  //是否存在下一条对话记录


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //注册引导ET贴图
        RegisterGuideET();
    }

    public bool CheckCondition()
    {
        if (_IsNextDialogRecoder)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RunOperation()
    {
        _IsNextDialogRecoder = false;
        //隐藏对话界面
        GoGuideUIBackground.SetActive(false);
        //隐藏引导ET贴图
        ImgGuideET.gameObject.SetActive(false);
        //激活ET
        View_PlayerInfoReseponse.Instance.DisplayET();
        //恢复对话系统，继续对话
        StartCoroutine("ResumeDialog");

        return true;
    }

    /// <summary>
    /// 注册引导ET贴图事件，当点击了ET贴图后，再进行相关的逻辑操作
    /// </summary>
    private void RegisterGuideET()
    {
        if (ImgGuideET!=null)
        {
            EventTrrigerListener.Get(ImgGuideET.gameObject).onClick += GuideETOperation;
        }
    }

    private void GuideETOperation(GameObject go)
    {
        if (go == ImgGuideET.gameObject)
        {
            _IsNextDialogRecoder = true;
        }
    }

    /// <summary>
    /// 显示ET贴图
    /// </summary>
    public void DisplayGuideET()
    {
        ImgGuideET.gameObject.SetActive(true);
    }

    //恢复对话系统，继续会话
    IEnumerator ResumeDialog()
    {
        //等待3s之后恢复对话
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_3F);
        //隐藏ET
        View_PlayerInfoReseponse.Instance.HidenET();
        //注册会话系统，允许继续会话
        TrrigerDialogs.Instance.RigisterDialogs();
        //运行对话系统，直接显示下一条会话，不用玩家点击就可以直接显示出来
        TrrigerDialogs.Instance.RunOperation();
        //显示对话界面
        GoGuideUIBackground.SetActive(true);
        
    }
}
