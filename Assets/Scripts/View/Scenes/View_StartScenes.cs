using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_StartScenes : MonoBehaviour {

    //点击了开始场景中的“新的传奇”
    public void OnClickNewGame()
    {
        //输出信息方便以后的调试
        Debug.Log(GetType() + "点击视图层里的OnClickNewGame方法");
        //在视图层中调用控制层里面的OnClickNewGame方法
        Ctrl_StartScenes.Instance.OnClickNewGame();
    }

    //点击了开始场景中的“继续游戏”
    public void OnClickGameContinue()
    {
        Debug.Log(GetType() + "点击视图层里的OnClickGameContinue方法");
        //在视图中调用控制层里面的OnClickGameContinue方法
        Ctrl_StartScenes.Instance.OnClickGameContinue();
    }

}
