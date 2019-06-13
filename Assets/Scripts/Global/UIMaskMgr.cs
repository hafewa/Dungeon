using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*UI遮罩管理器
	作用：当点击了一个弹出窗口时，应当启用一个遮罩管理，以免玩家再点击其它的UI
*/
public class UIMaskMgr : MonoBehaviour
{
    public GameObject GoTopPanel;               //顶层面板
    public GameObject GoMaskPanel;              //UI遮罩面板
    private Camera _UICamera;                   //UI摄像机
    private float _OriginalCameraDepth;         //原始UI摄像机的层深


    // Use this for initialization
    void Start()
    {
        //得到UI摄像机的原始层深
        _UICamera = this.gameObject.transform.parent.Find("UICamera").GetComponent<Camera>();
        if (_UICamera != null)
        {
            _OriginalCameraDepth = _UICamera.depth;
        }
        else
        {
            Debug.LogError(GetType() + "UI摄像机为空，请检查是否有添加UI摄像机“UICamera”！！");
        }
    }

    ///<summary>
	///设置遮罩窗体
	///</summary>
	///<param name="goDisplayPanel">需要显示的窗体</param>
	public void SetMaskWindow(GameObject goDisplayPanel)
    {
        //顶层窗体下移
        GoTopPanel.transform.SetAsLastSibling();
        //启用遮罩窗体
        GoMaskPanel.SetActive(true);
        //遮罩窗体下移
        GoMaskPanel.transform.SetAsLastSibling();
        //显示窗体下移
        goDisplayPanel.transform.SetAsLastSibling();
        //增加当前UI摄像机的层深
        if (_UICamera != null)
        {
            _UICamera.depth = _UICamera.depth + 20;
        }
    }

    ///<summary>
	///取消遮罩窗体
	///</summary>
	public void CancelMaskWindow()
    {
        //顶层窗体上移
        GoTopPanel.transform.SetAsFirstSibling();
        //禁用遮罩窗体
        GoMaskPanel.SetActive(false);
        //恢复UI摄像机原始层深
        if (_UICamera != null)
        {
            _UICamera.depth = _OriginalCameraDepth;
        }
    }
}
