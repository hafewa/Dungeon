using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems; //事件系统

/// <summary>
///事件监听器
/// </summary>
public class EventTrrigerListener:UnityEngine.EventSystems.EventTrigger
{
    /// <summary>
    /// 事件监听委托
    /// </summary>
    /// <param name="go">监听的对象</param>
    public delegate void VoidDelegate(GameObject go);

    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onUp;
    public VoidDelegate onExit;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;

    /// <summary>
    /// 得到监听器
    /// </summary>
    /// <param name="go">监听的游戏对象</param>
    /// <returns>监听器</returns>
    public static EventTrrigerListener Get(GameObject go)
    {
        EventTrrigerListener listener = go.GetComponent<EventTrrigerListener>();

        if (listener == null)
        {
            listener = go.AddComponent<EventTrrigerListener>();
        }

        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
        {
            onDown(gameObject);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
        {
            onEnter(gameObject);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
        {
            onUp(gameObject);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
        {
            onExit(gameObject);
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
        {
            onSelect(gameObject);
        }
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null)
        {
            onUpdateSelect(gameObject);
        }
    }
}
