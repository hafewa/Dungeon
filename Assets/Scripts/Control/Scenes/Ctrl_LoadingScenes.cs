using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景异步加载，后台逻辑处理
/// </summary>
public class Ctrl_LoadingScenes : BaseControl {

    IEnumerator Start()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);
        //场景预处理
        StartCoroutine("ScenesPreProgressing");
        //垃圾收集
        StartCoroutine("HandleGC");
        
    }

    IEnumerator ScenesPreProgressing()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);

        switch (GlobalParameterManager.NextScensName)
        {
            case ScenesEnum.TestDialogScene:
                break;
            case ScenesEnum.StartScenes:
                break;
            case ScenesEnum.LoadingScenes:
                break;
            case ScenesEnum.LoginScenes:
                break;
            case ScenesEnum.LevelOne:
                //第一关卡
                StartCoroutine("ScenePreProgressing_LevelOne");
                break;
            case ScenesEnum.LevelTow:
                break;
            case ScenesEnum.BaseScenes:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 垃圾资源收集
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleGC()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);
           //卸载无用的资源
        Resources.UnloadUnusedAssets();
        //强制垃圾收集
        System.GC.Collect();
    }

    /// <summary>
    /// 预处理第一关卡
    /// </summary>
    /// <returns></returns>
    IEnumerator ScenePreProgressing_LevelOne()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);
        ConfigManager config = new ConfigManager(KernalParameter.GetLogPath(), KernalParameter.GetLogRootNodeName());

        Log.ClearLogFileAndBufferData();
        XMLDialogDataAnalysisManager.GetInstance().SetXMLPathAndRootNodeName(KernalParameter.GetDialogPath(), KernalParameter.GetDialogRootNodeName());

        yield return new WaitForSeconds(0.3f);  //需要等待得到了路径之后才能根据路径得到XML里面的数据
        //得到XML中所有的数值
        List<DialogDataFormat> liDialogsDataArray = XMLDialogDataAnalysisManager.GetInstance().GetAllXMLDataArray();
        //得到对话的数据是否加载成功
        bool bResult = DialogDataMgr.GetInstance().LoadAllDialogData(liDialogsDataArray);
        if (!bResult)
        {
            //如果加载失败，显示失败信息在Log日志中
            Log.Write(GetType() + "对话数据管理器加载数据失败", Log.Level.High);
        }
    }

}
