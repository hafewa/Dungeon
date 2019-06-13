using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对话信息UI层的管理器
/// </summary>
public class DialogUIMgr : MonoBehaviour
{
    public static DialogUIMgr Instance;
    private DialogType dialogType=DialogType.None;

    //UI对象
    public GameObject Hero;             //英雄
    public GameObject NPC_Left;         //NPC在左边
    public GameObject NPC_Right;        //NPC在右边
    public GameObject SingleDialogArea;//单人对话
    public GameObject DoubleDialogArea; //双人对话

    //对话显示控件
    public Text TextPersonName;         //人名
    public Text TextDoubleDialogContent;    //单人对话内容
    public Text TextSingleDialogContent;    //双人对话内容

    //sprint资源数组    (规定：0下标表示彩色精灵  1表示黑白精灵)
    public Sprite[] SpritHero;          //英雄精灵数组
    public Sprite[] SpritNPC_Left;      //左边的NPC精灵数组
    public Sprite[] SpritNPC_Right;     //右边的NPC精灵数组

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 显示下一句对话
    /// </summary>
    /// <param name="diaType">对话的类型（单人、双人）</param>
    /// <param name="diaSectionNum">对话段落</param>
    /// <returns>
    /// true：对话结束
    /// false:对话继续
    /// </returns>
    public bool DisplayNextDialog(DialogType diaType,int diaSectionNum)
    {
        bool isDialogEnd=false;     //会话是否已经结束
        DialogSide dialogSide = DialogSide.None;        //正在说话的一方
        string strPersonName;       //对话姓名
        string strPersonContent;    //对话内容

        //切换对话类型
        ChangeDialogType(diaType);

        //得到会话数据
        bool bFlag = DialogDataMgr.GetInstance()
            .GetNextDialogInfoRecoder(diaSectionNum, out dialogSide, out strPersonName, out strPersonContent);
        if (bFlag)  //如果为真，表示当前的段落还有对话信息
        {
            //显示对话信息
            DisplayDialogInfo(diaType, dialogSide, strPersonName, strPersonContent);
        }
        else
        {
            isDialogEnd = true; //对话结束,没有对话信息
        }

        return isDialogEnd;
    }

    //切换对话类型
    private void ChangeDialogType(DialogType dialogType)
    {
        switch (dialogType)
        {
            case DialogType.None:                   //为空，什么都不显示
                Hero.SetActive(false);
                NPC_Left.SetActive(false);
                NPC_Right.SetActive(false);
                SingleDialogArea.SetActive(false);
                DoubleDialogArea.SetActive(false);
                break;
            case DialogType.SingleDialog:       //如果是单人对话，显示左边NPC和单人对话区域
                Hero.SetActive(false);
                NPC_Left.SetActive(true);
                NPC_Right.SetActive(false);
                SingleDialogArea.SetActive(true);
                DoubleDialogArea.SetActive(false);
                break;
            case DialogType.DoubleDialog:       //如果是双人对话，显示右边的NPC、英雄和双人对话区域
                Hero.SetActive(true);
                NPC_Left.SetActive(false);
                NPC_Right.SetActive(true);
                SingleDialogArea.SetActive(false);
                DoubleDialogArea.SetActive(true); 
                break;
            default:
                Hero.SetActive(false);
                NPC_Left.SetActive(false);
                NPC_Right.SetActive(false);
                SingleDialogArea.SetActive(false);
                DoubleDialogArea.SetActive(false);
                break;
        }
    }

    //显示对话信息
    private void DisplayDialogInfo(DialogType dialogType, DialogSide dialogSide, string strPersonName,
        string strPersonContent)
    {
        switch (dialogType)
        {
            case DialogType.None:
                break;
            case DialogType.SingleDialog:
                TextSingleDialogContent.text = strPersonContent;    //单人对话只需要显示内容
                break;
            case DialogType.DoubleDialog:
                //显示人名对话信息
                if (!string.IsNullOrEmpty(strPersonName) && !string.IsNullOrEmpty(strPersonContent))
                {
                    //当前的对话处于主角方，则对话人的名字为前一个场景输入的参数名字
                    if (!string.IsNullOrEmpty(GlobalParameterManager.PlayerName) && dialogSide == DialogSide.HeroSide)
                    {
                        TextPersonName.text = GlobalParameterManager.PlayerName;
                    }
                    else
                    {
                        TextPersonName.text = strPersonName;
                    }
                    TextDoubleDialogContent.text = strPersonContent;
                }
                //确定显示精灵
                switch (dialogSide)
                {
                    case DialogSide.None:
                        break;
                    case DialogSide.HeroSide:
                        Hero.GetComponent<Image>().overrideSprite=SpritHero[0];     //英雄显示彩色
                        NPC_Right.GetComponent<Image>().overrideSprite = SpritNPC_Right[1]; //NPC显示黑白
                        break;
                    case DialogSide.NPCSide:
                        Hero.GetComponent<Image>().overrideSprite = SpritHero[1];     //英雄显示黑白
                        NPC_Right.GetComponent<Image>().overrideSprite = SpritNPC_Right[0]; //NPC显示彩色
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

}

/// <summary>
/// 对话类型
/// </summary>
public enum DialogType
{
    None,               //无
    SingleDialog,       //单人对话
    DoubleDialog        //双人对话
}