using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*对话数据管理器
 *  作用：根据对话数据格式（DialogFormat）定义
 *          输入“段落编号”，输出相应的内容
 */
public class DialogDataMgr
{
    private static DialogDataMgr _instance;
    private static List<DialogDataFormat> _AllDialogDataArray;      //所有的对话数据集合
    private static List<DialogDataFormat> _CurrentDialogBufferArray;        //当前对话缓存集合  用于提高系统的效率，当XML里面的数据有特别多的数据，应该把一些同一类型的都缓存起来，不用每次都在庞大的XML里查询
    private static int _IntIndexByDialogSection;    //对话序号（某个段落)
    private static int _OriginalSectionNum = 1;

    //常量
    private const string XML_HERO = "Hero";     //该常量为XML里面的DialogSide属性名，当其他项目中的该属性名不同时，这里要响应改动
    private const string XML_NPC = "NPC";

    private DialogDataMgr()
    {
        //实例化字段信息
        _AllDialogDataArray=new List<DialogDataFormat>();
        _CurrentDialogBufferArray=new List<DialogDataFormat>();
        _IntIndexByDialogSection = 0;
    }

    /// <summary>
    /// 得到本类实例
    /// </summary>
    /// <returns></returns>
    public static DialogDataMgr GetInstance()
    {
        if (_instance == null)
        {
            _instance=new DialogDataMgr();
        }

        return _instance;
    }
    
    /// <summary>
    /// 加载外部数据集合
    /// </summary>
    /// <param name="dialogData">外部数据集合</param>
    /// <returns>true：数据加载成功 false:数据加载失败</returns>
    public bool LoadAllDialogData(List<DialogDataFormat> dialogData)
    {
        //参数检查
        if (dialogData == null || dialogData.Count == 0)
        {
            return false;
        }

        //如果之前已经加载了数据，就不再执行
        if (_AllDialogDataArray!=null&&_AllDialogDataArray.Count==0)
        {
            for (int i = 0; i < dialogData.Count; i++)
            {
                _AllDialogDataArray.Add(dialogData[i]);
            }

            return true;
        }
        else
        {
            return false;
        }
        
    }


    /// <summary>
    /// 得到下一条对话记录
    /// </summary>
    /// <param name="diaSectionNum">输入段落编号</param>
    /// <param name="side">输出对话方</param>
    /// <param name="strPersonName">输出人名</param>
    /// <param name="strDialogContent">输出对话内容</param>
    /// <returns>
    /// true:输出的是合法数据
    ///false:没有数据对话数据
    /// </returns>
    public bool GetNextDialogInfoRecoder(int diaSectionNum,out DialogSide side,out string strPersonName,out string strDialogContent)
    {
        side = DialogSide.None;
        strPersonName = "[GetNextDialogInfoRecoder/strPersonName]";
        strDialogContent = "[GetNextDialogInfoRecoder/strDialogContent]";
        //输入参数检查
        if (diaSectionNum < 0)
        {
            return false;
        }

        //如果对话段落编号大于开始的段落编号，表明当前的对话段落已经结束对话，跳转到了下一段对话，此时应该重置对话索引
        if (diaSectionNum > _OriginalSectionNum)
        {
            //重新内部编号
            _IntIndexByDialogSection = 0;
            //清空当前的对话缓存     因为已经换了一段对话，所以需要清空已经结束的对话缓存
            _CurrentDialogBufferArray.Clear();
            //把当前的段落记录为原始的段落编号
            _OriginalSectionNum = diaSectionNum;
        }

        //先检查缓存集合里面是否有数据
        if (_CurrentDialogBufferArray != null && _CurrentDialogBufferArray.Count >= 1)
        {
            if (_IntIndexByDialogSection < _CurrentDialogBufferArray.Count)
            {
                ++_IntIndexByDialogSection;
            }
            else
            {
                return false;
            }
        }
        else
        {
            //当前的缓存集合为空
            ++_IntIndexByDialogSection;
        }
        //得到对话信息
        GetDialogInfoRecoder(diaSectionNum, out side, out strPersonName, out strDialogContent);
        return true;
    }

    /// <summary>
    /// 得到对话信息
    ///     思路：
    ///         对于输入的“段落编号”，首先在当前对话数据集合中进行查询，如果找到，直接返回结果，否则在全部对话数据集合中查找
    /// </summary>
    /// <param name="diaSectionNum">输入段落编号</param>
    /// <param name="side">输出对话方</param>
    /// <param name="strPersonName">输出人名</param>
    /// <param name="strDialogContent">输出对话内容</param>
    /// <returns>
    /// true:输出的是合法数据
    ///false:没有数据对话数据
    /// </returns>
    private bool GetDialogInfoRecoder(int diaSectionNum, out DialogSide side, out string strPersonName,
        out string strDialogContent)
    {
        side = DialogSide.None;
        string strDialogSide = "[GetNextDialogInfoRecoder/strDialogSide]";
        strPersonName = "[GetNextDialogInfoRecoder/strPersonName]";
        strDialogContent = "[GetNextDialogInfoRecoder/strDialogContent]";

        //参数检查
        if (diaSectionNum <= 0)
        {
            return false;
        }

        //1.在当前对话数据集合中进行查询
        Log.Write(GetType()+"1.在当前缓存集合中查询对话语句");
        if (_CurrentDialogBufferArray != null && _CurrentDialogBufferArray.Count >= 1)
        {
            for (int i = 0; i < _CurrentDialogBufferArray.Count; i++)
            {
                //段落编号相同
                if (_CurrentDialogBufferArray[i].DialogSecNum == diaSectionNum)
                {
                    //段落内序号相同
                    if (_CurrentDialogBufferArray[i].DialogSectionIndex == _IntIndexByDialogSection)
                    {
                        //找到数据，提取当前数据返回
                        strDialogSide = _CurrentDialogBufferArray[i].DialogSide;
                        if (strDialogSide.Trim().Equals(XML_HERO))
                        {
                            side = DialogSide.HeroSide;
                        }
                        else if (strDialogSide.Trim().Equals(XML_NPC))
                        {
                            side = DialogSide.NPCSide;
                        }

                        strPersonName = _CurrentDialogBufferArray[i].DialogPerson;
                        strDialogContent = _CurrentDialogBufferArray[i].DialogContent;

                        return true;
                    }
                }
            }
        }
        //2.在全部对话数据集合中查询
        Log.Write(GetType()+"2.在全部数据集合中查询对话信息");
        if (_AllDialogDataArray != null && _AllDialogDataArray.Count >= 1)
        {
            for (int i = 0; i < _AllDialogDataArray.Count; i++)
            {
                //段落编号相同
                if (_AllDialogDataArray[i].DialogSecNum == diaSectionNum)
                {
                    //段落内序号相同
                    if (_AllDialogDataArray[i].DialogSectionIndex == _IntIndexByDialogSection)
                    {
                        //找到数据，提取当前数据返回
                        strDialogSide = _AllDialogDataArray[i].DialogSide;
                        if (strDialogSide.Trim().Equals("Hero"))
                        {
                            side = DialogSide.HeroSide;
                        }
                        else if (strDialogSide.Trim().Equals("NPC"))
                        {
                            side = DialogSide.NPCSide;
                        }
                        //给输出的参数赋值
                        strPersonName = _AllDialogDataArray[i].DialogPerson;
                        strDialogContent = _AllDialogDataArray[i].DialogContent;
                        //把当前段落编号的数据，写入到“当前段落缓存集合”
                        LoadToBufferArrayBySectionNum(diaSectionNum);       //可能有问题

                        return true;
                    }
                }
            }
        }
        //根据当前段落编号，无法查询数据结果，则返回false，数据是无效的
        return false;
    }

    /// <summary>
    /// 把当前段落编号的数据，写入到“当前段落缓存集合”
    /// </summary>
    /// <param name="diaSectionNum">段落编号</param>
    /// <returns>
    /// true:表示操作成功
    ///false:操作无效
    /// </returns>
    private bool LoadToBufferArrayBySectionNum(int diaSectionNum)
    {
        if (diaSectionNum <= 0)
        {
            return false;
        }

        if (_AllDialogDataArray != null && _AllDialogDataArray.Count >= 1)
        {
            //当前缓存集合，清空以前的数据
            _CurrentDialogBufferArray.Clear();
            for (int i = 0; i < _AllDialogDataArray.Count; i++)
            {
                //把符合条件的段落加入到当前的缓存集合中
                if (_AllDialogDataArray[i].DialogSecNum == diaSectionNum)
                {
                    //当前缓存集合，加入新的数据
                    _CurrentDialogBufferArray.Add(_AllDialogDataArray[i]);
                }
            }

            return true;
        }

        return false;
    }

}

/// <summary>
/// 对话的双方
/// </summary>
public enum DialogSide
{
    None,           //无
    HeroSide,       //英雄方
    NPCSide         //NPC方
}
