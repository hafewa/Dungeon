using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 背包系统父类
/// 作用：定义装备的一般性操作，例如拖拽
/// </summary>
public class BasePackage : MonoBehaviour
{
    protected string strMoveToTagetName;    //移动到的目标名称
    private CanvasGroup _canvasGroup;     //用于贴图的穿透处理

    private Vector3 _originalPos;         //原始的位置
    private Transform _originalParent;    //原本处于的父对象
    private Transform _myTransform;     //本对象方位
    private RectTransform _myRetrans;   //二维方位

    /// <summary>
    /// 通过子类运行本类实例
    /// </summary>
    protected void RunInstanceByChild()
    {
        BaseStart();
    }

    /// <summary>
    /// 进行父类的实例化
    /// </summary>
    void BaseStart()
    {
        //贴图穿透组件
        _canvasGroup = this.GetComponent<CanvasGroup>();
        //二维方位
        _myRetrans = this.transform as RectTransform;
        //贴图的方位
        _myTransform = this.transform;
        //初始父对象
        _originalParent = _myTransform.parent;

        
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void BaseOnBeginDrag(PointerEventData eventData)
    {
        //忽略自身（可以穿透）
        _canvasGroup.blocksRaycasts = false;
        //保证图片始终在所有的上面
        this.gameObject.transform.SetAsLastSibling();
        //每次拖拽开始前都需要获得原始位置
        _originalPos = _myTransform.position;
        
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    /// <param name="eventData"></param>
    public void BaseOnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;     //当前鼠标的位置
        //屏幕坐标，转二维矩阵坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_myRetrans, eventData.position,
            eventData.pressEventCamera, out globalMousePos))
        {
            _myRetrans.position = globalMousePos;   //得到当前的鼠标位置,实现贴图跟着鼠标走
        }
    }

    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void BaseOnEndDrag(PointerEventData eventData)
    {
        //当前鼠标经过的“格子”名称
        //eventData.pointerEnter返回的是一个当前鼠标经过的对象
        GameObject cur = eventData.pointerEnter;
        if (cur != null)
        {
            //是否经过拖拽的目标点
            if (cur.name.Equals(strMoveToTagetName))
            {
                if (cur.transform.childCount == 0)
                {
                    //遇到了目标点，把当前的拖拽物品的位置设置为目标点的位置
                    _myTransform.position = cur.transform.position;
                    _originalPos = _myTransform.position;
                    _myTransform.SetParent(cur.transform);
                }
                else
                {
                    foreach (Transform child in cur.transform)
                    {
                        //换装，将未装备的道具和已经装备的道具互换位置
                        Vector3 replacePos = child.position;
                        child.position = _originalPos;
                        _myTransform.position = replacePos;
                        _originalPos = _myTransform.position;
                        //将父对象重置
                        child.SetParent(_originalParent);
                        _myTransform.SetParent(cur.transform);
                       
                    }
                }

                //执行特定的装备方法
                InvokeMethodByEndDrag();
            }
            //没有遇到目标位置就返回原来的位置
            else
            {
                //移动到背包系统的其他有效位置上
                //如果都是背包道具，则交换位置
                if (cur.tag==eventData.pointerDrag.tag&&cur.name!=eventData.pointerDrag.name)
                {
                    //装备位置互换
                    Vector3 targetPos = cur.transform.position;
                    cur.transform.position = _originalPos;
                    _myTransform.position = targetPos;
                    //再将新的位置作为原始位置
                    _originalPos = _myTransform.position;
                }
                //拖拽到背包系统界面的其他对象上
                else
                {
                    //返回原始的位置
                    _myTransform.position = _originalPos;
                }
                //阻止穿透，可以进行再次移动
                _canvasGroup.blocksRaycasts = true;
            }
        }
        //拖拽到了一个没有物体的位置
        else
        {
            _myTransform.position = _originalPos;
        }
    }

    /// <summary>
    /// 执行特定的装备方法，让子类去实现
    /// </summary>
    protected virtual void InvokeMethodByEndDrag()
    {
    }
}
