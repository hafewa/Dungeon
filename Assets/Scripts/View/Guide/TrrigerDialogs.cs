using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*新手引导模块
	触发对话引导

 */
public class TrrigerDialogs : MonoBehaviour,IGuideTrriger
{
    /// <summary>
    /// 新手引导的步骤状态
    /// </summary>
    public enum DialogWithGuideStep
    {
        None,                           //无
        Step_DoublePersonDialog,        //双人对话
        Step_IntroduceET,               //新手指导EaseTouch的使用
        Step_IntroduceVirtualKey,       //新手指导使用虚拟按键
        Step_IntroduceEnd               //新手指导结束
    }


    public static TrrigerDialogs Instance;
    public GameObject GoGuideUIBackground;      //新手指引UI背景对象

    private bool _IsNextDialogRecoder = false;  //是否存在下一条对话记录
    private Image _ImgDialogBg;                 //对话界面背景图
    private DialogWithGuideStep _DialogStep = DialogWithGuideStep.None;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Log.Write(GetType()+"Start方法执行");

        GoGuideUIBackground.SetActive(true);
        //当前状态
        _DialogStep = DialogWithGuideStep.Step_DoublePersonDialog;
        //背景贴图
        _ImgDialogBg=transform.parent.Find("Background").GetComponent<Image>();
        //注册背景贴图
        RigisterDialogs();

        //讲解第一句话
        DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, 1);
    }

    public bool CheckCondition()
    {
        //如果存在下一条对话记录，进行相关的逻辑操作
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
        bool bResult = false;       //本方法是否已经运行结束
        bool bCurrentDialogResult = false;  //当前对话是否结束

        _IsNextDialogRecoder = false;

        //根据当前的状态，进行业务逻辑判断
        switch (_DialogStep)
        {
            case DialogWithGuideStep.None:
                break;
            case DialogWithGuideStep.Step_DoublePersonDialog:
                bCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.DoubleDialog, 1);
                break;
            case DialogWithGuideStep.Step_IntroduceET:
                Log.Write(GetType()+"NPC开始介绍摇杆操作");
                bCurrentDialogResult =DialogUIMgr.Instance.DisplayNextDialog(DialogType.SingleDialog, 2);
                break;
            case DialogWithGuideStep.Step_IntroduceVirtualKey:
                Log.Write(GetType()+"NPC开始介绍虚拟按键操作");
                bCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.SingleDialog, 3);  //介绍虚拟攻击按键
                break;
            case DialogWithGuideStep.Step_IntroduceEnd:
                //显示引导结束会话
                Log.Write(GetType()+"新手引导会话结束");
                bCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.SingleDialog, 4);
                break;
            default:
                break;
        }

        //当前的对话已经结束
        if (bCurrentDialogResult)
        {
            switch (_DialogStep)
            {
                case DialogWithGuideStep.None:
                    break;
                case DialogWithGuideStep.Step_DoublePersonDialog:
                    break;
                case DialogWithGuideStep.Step_IntroduceET:      //NPC介绍完毕ET完毕
                    //显示引导虚拟摇杆贴图，控制权转移到TrrigerOperET脚本上
                    TrrigerOperET.Instance.DisplayGuideET();
                    //暂停会话
                    UnRigisterDialogs();
                    break;
                case DialogWithGuideStep.Step_IntroduceVirtualKey:
                    //显示引导虚拟按键贴图，把控制权交到TrrigerOperVirtualKey上
                    TrrigerOperVirtualKey.Instance.DisplayGuideVirtualKey();
                    //暂停会话
                    UnRigisterDialogs();
                    break;
                case DialogWithGuideStep.Step_IntroduceEnd:
                    //激活ET和所有的虚拟攻击按键
                    View_PlayerInfoReseponse.Instance.DisplayAllVirtualKey();
                    //显示英雄头像面板信息
                    View_PlayerInfoReseponse.Instance.DisplayHeroUIInfo();
                    //允许生成敌人（激活脚本）
                    GameObject.Find("_SecenControl/GameManager").GetComponent<View_LevelOneScene>().enabled=true;
                    GameObject.Find("_SecenControl/GameManager").GetComponent<Ctrl_LevelOneScenes>().enabled = true;

                    //隐藏对话界面
                    GoGuideUIBackground.SetActive(false);
                    bResult = true;
                    break;
                default:
                    break;
            }
            //进入下一个状态
            EnterNextDialog();

        }

        return bResult;
    }

    /// <summary>
    /// 进入下一条对话
    /// </summary>
    private void EnterNextDialog()
    {
        switch (_DialogStep)
        {
            case DialogWithGuideStep.None:
                break;
            case DialogWithGuideStep.Step_DoublePersonDialog:
                _DialogStep = DialogWithGuideStep.Step_IntroduceET;
                break;
            case DialogWithGuideStep.Step_IntroduceET:
                _DialogStep = DialogWithGuideStep.Step_IntroduceVirtualKey;
                break;
            case DialogWithGuideStep.Step_IntroduceVirtualKey:
                _DialogStep = DialogWithGuideStep.Step_IntroduceEnd;
                break;
            case DialogWithGuideStep.Step_IntroduceEnd:
                _DialogStep = DialogWithGuideStep.None;
                break;
            default:
                break;
        }
    }

    public void RigisterDialogs()
    {
        if (_ImgDialogBg != null)
        {
            //事件注册  如果点击了该背景贴图，则显示下一条对话记录
            EventTrrigerListener.Get(_ImgDialogBg.gameObject).onClick+= DisplayNextDialog;
        }
    }

    /// <summary>
    /// 取消注册“背景贴图”
    /// </summary>
    public void UnRigisterDialogs()
    {
        EventTrrigerListener.Get(_ImgDialogBg.gameObject).onClick -= DisplayNextDialog;
    }

    /// <summary>
    ///显示下一条对话记录 
    /// </summary>
    /// <param name="go">注册的游戏对象</param>
    private void DisplayNextDialog(GameObject go)
    {
        //如果当前的游戏对象为对话的背景贴图，表明还存在下一条对话记录
        if (go == _ImgDialogBg.gameObject)
        {
            _IsNextDialogRecoder = true;
            Log.Write(GetType() + "点击了该背景图!!");
        }
    }
}
