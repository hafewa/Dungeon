using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*新手引导模块
	触发虚拟按键
 */
public class TrrigerOperVirtualKey : MonoBehaviour,IGuideTrriger
{

    public static TrrigerOperVirtualKey Instance;
    public GameObject GoGuideUIBackground;      //新手指引UI背景对象
    public Image ImgGuideVirtualKey;            //新手指导虚拟按键贴图

    private bool _IsNextDialogRecoder = false;  //是否存在下一条对话记录

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //注册引导ET贴图
        RegisterGuideVirtualKey();
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
        ImgGuideVirtualKey.gameObject.SetActive(false);
        //激活ET和虚拟攻击按键
        View_PlayerInfoReseponse.Instance.DisplayNoramalATKKey();
        //恢复对话系统，继续对话
        StartCoroutine("ResumeDialog");

        return false;
    }

    /// <summary>
    /// 注册引导ET贴图事件，当点击了ET贴图后，再进行相关的逻辑操作
    /// </summary>
    private void RegisterGuideVirtualKey()
    {
        if (ImgGuideVirtualKey != null)
        {
            EventTrrigerListener.Get(ImgGuideVirtualKey.gameObject).onClick += GuideVirtualKeyOperation;
        }
    }

    private void GuideVirtualKeyOperation(GameObject go)
    {
        if (go == ImgGuideVirtualKey.gameObject)
        {
            _IsNextDialogRecoder = true;
        }
    }

    /// <summary>
    /// 显示ET贴图
    /// </summary>
    public void DisplayGuideVirtualKey()
    {
        ImgGuideVirtualKey.gameObject.SetActive(true);
    }

    //恢复对话系统，继续会话
    IEnumerator ResumeDialog()
    {
        //等待3s之后恢复对话
        yield return new WaitForSeconds(8);
        //隐藏所有的虚拟按键按钮
        View_PlayerInfoReseponse.Instance.HideAllVirtualKey();
        //注册会话系统，允许继续会话
        TrrigerDialogs.Instance.RigisterDialogs();
        //运行对话系统，直接显示下一条会话，不用玩家点击就可以直接显示出来
        TrrigerDialogs.Instance.RunOperation();
        //显示对话界面
        GoGuideUIBackground.SetActive(true);
    }

}
