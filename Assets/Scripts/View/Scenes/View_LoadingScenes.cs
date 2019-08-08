using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//控制场景的异步加载控制
public class View_LoadingScenes : MonoBehaviour
{
    public Slider SliderLoadingProgress;    //进度条
    private float _ProgressNumber;  //进度数值
    private AsyncOperation _AsyOper;
    
    // Use this for initialization
    IEnumerator Start()
    {
        #region 测试代码
        ////测试Log日志系统
        ConfigManager config = new ConfigManager(KernalParameter.GetLogPath(), KernalParameter.GetLogRootNodeName());
        ////string strLogPath = config.AppSetting["LogPath"];
        ////string strLogState = config.AppSetting["LogState"];
        ////string strLogMaxCapcity = config.AppSetting["LogMaxCapacity"];
        ////string strLogBufferNumber = config.AppSetting["LogBufferNumber"];
        ////Debug.Log("路径：" + strLogPath);
        ////Debug.Log("状态：" + strLogState);
        ////Debug.Log("最大容量：" + strLogMaxCapcity);
        ////Debug.Log("缓存数量：" + strLogBufferNumber);

        ////测试Log.cs脚本
        ////Log.Write("1 低等级调试语句");
        ////Log.Write("1 中等级调试语句", Log.Level.Special);
        ////Log.Write("1 高等级调试语句", Log.Level.High);
        ////Log.Write("2 低等级调试语句");
        ////Log.Write("2 中等级调试语句", Log.Level.Special);
        ////Log.Write("2 高等级调试语句", Log.Level.High);
        ////Log.ClearLogFileAndBufferData();      //测试清除文本中所有的数据

        ////测试XML解析程序
        ////参数赋值
        //Log.ClearLogFileAndBufferData();
        XMLDialogDataAnalysisManager.GetInstance().SetXMLPathAndRootNodeName(KernalParameter.GetDialogPath(), KernalParameter.GetDialogRootNodeName());
        yield return new WaitForSeconds(0.3f);
        ////得到XML中所有的数值
        List<DialogDataFormat> liDialogsDataArray = XMLDialogDataAnalysisManager.GetInstance().GetAllXMLDataArray();
        ////foreach (DialogDataFormat data in liDialogsDataArray)
        ////{
        ////    Log.Write("");
        ////    Log.Write("段落数量=" + data.DialogSecNum);
        ////    Log.Write("段落名称=" + data.DialogSecName);
        ////    Log.Write("段落索引值=" + data.DialogSectionIndex);
        ////    Log.Write("对话双方=" + data.DialogSide);
        ////    Log.Write("对话人名称=" + data.DialogPerson);
        ////    Log.Write("对话内容=" + data.DialogContent);
        ////}
        ////Log.SynLogArrayToFile();

        //测试给对话管理器加载数据
        bool bResult = DialogDataMgr.GetInstance().LoadAllDialogData(liDialogsDataArray);
        if (!bResult)
        {
           //如果加载失败，显示失败信息在Log日志中
           Log.Write(GetType() + "对话数据管理器加载数据失败", Log.Level.High);
        }

        ////进入指定的关卡
        GlobalParameterManager.NextScensName = ScenesEnum.MainCityScene;     //测试进入对话测试场景
        #endregion

        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);

        //启动LoadingScenesProgress线程
        StartCoroutine("LoadingScenesProgress");
    }

    //异步加载场景
    IEnumerator LoadingScenesProgress()
    {
        //用于接受异步加载场景的参数
        _AsyOper = SceneManager.LoadSceneAsync(ConvertEnumToString.GetInstance().GetStrByEnumScenes(GlobalParameterManager.NextScensName));
        _ProgressNumber = _AsyOper.progress;
        yield return _AsyOper;
    }

    // Update is called once per frame
    void Update()
    {
        //用于改变进度条的显示，让进度条每一帧都自加
        if (SliderLoadingProgress.value <= 1)
        {
            _ProgressNumber += 0.01f;
        }

        SliderLoadingProgress.value = _ProgressNumber;
    }
}
