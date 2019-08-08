using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 测试背包系统中的拖拽功能
/// </summary>
public class TestPackageDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    private CanvasGroup _canvasGroup;     //用于贴图的穿透处理

    private Vector3 _originalPos;         //原始的位置
    private RectTransform _myRetrans;   //二维方位

    void Start()
    {
        //贴图穿透组件
        _canvasGroup = this.GetComponent<CanvasGroup>();
        //二维方位
        _myRetrans = this.transform as RectTransform;
        //获得原始位置
        _originalPos = _myRetrans.position;

    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //忽略自身（可以穿透）
        _canvasGroup.blocksRaycasts = false;
        //保证图片始终在所有的上面
        this.gameObject.transform.SetAsLastSibling();
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
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
    public void OnEndDrag(PointerEventData eventData)
    {
        //当前鼠标经过的“格子”名称
        GameObject cur = eventData.pointerEnter;
        if (cur != null)
        {
            //是否经过拖拽的目标点
            if (cur.name.Equals("Image-Goal"))
            {
                _myRetrans.position = cur.transform.position;
                _originalPos = _myRetrans.position;
            }
            //没有遇到目标位置就返回原来的位置
            else
            {
                _myRetrans.position = _originalPos;
                //阻止穿透，可以进行再次移动
                _canvasGroup.blocksRaycasts = true;
            }
        }
        //拖拽到了一个没有物体的位置
        else
        {
            _myRetrans.position = _originalPos;
        }
    }


}
