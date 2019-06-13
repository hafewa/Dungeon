using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KernalParameter
{
//#if UNITY_STANDALONE_WIN
//    //系统配置信息——日志路径
//    internal static readonly string SystemConfiginfo_LogPath ="file://" + Application.dataPath + "/StreamingAssets/SystemConfigInfo.xml";

//    internal static readonly string SystemConfiginfo_LogRootNodeName = "SystemConfigInfo";
//    //系统配置信息——对话系统路径
//    internal static readonly string DialogsXMLConfiginfo_DialogsPath ="file://" + Application.dataPath + "/StreamingAssets/SystemDialogInfo.xml";

//    internal static readonly string DialogsXMLConfiginfo_DialogsRootNodeName = "Dialogs_CN";
//#elif UNITY_ANDROID
////系统配置信息——日志路径
//            internal static readonly string SystemConfiginfo_LogPath =
// Application.dataPath+ "!/Assets/SystemConfigInfo.xml";
//            internal static readonly string SystemConfiginfo_LogRootNodeName = "SystemConfigInfo";
//            //系统配置信息——对话系统路径
//            internal static readonly string DialogsXMLConfiginfo_DialogsPath =
// Application.dataPath+ "!/Assets/SystemDialogInfo.xml";
//            internal static readonly string DialogsXMLConfiginfo_DialogsRootNodeName = "Dialogs_CN";
//#elif UNITY_IPHONE
////系统配置信息——日志路径
//            internal static readonly string SystemConfiginfo_LogPath =
// Application.dataPath+ "/Raw/SystemConfigInfo.xml";
//            internal static readonly string SystemConfiginfo_LogRootNodeName = "SystemConfigInfo";
//            //系统配置信息——对话系统路径
//            internal static readonly string DialogsXMLConfiginfo_DialogsPath =
// Application.dataPath+ "/Raw/SystemDialogInfo.xml";
//            internal static readonly string DialogsXMLConfiginfo_DialogsRootNodeName = "Dialogs_CN";
//#endif

    /// <summary>
    /// 得到日志路径
    /// </summary>
    /// <returns></returns>
    public static string GetLogPath()
    {
        string logPath = null;

        //安卓或者IPhone环境
        if (Application.platform == RuntimePlatform.Android||Application.platform==RuntimePlatform.IPhonePlayer)
        {
            logPath = Application.streamingAssetsPath + "/SystemConfigInfo.xml";
        }
        else
        {
            //Win环境
            logPath = "file://" + Application.streamingAssetsPath + "/SystemConfigInfo.xml";
        }
        return logPath;
    }
    /// <summary>
    /// 得到日志根节点名称
    /// </summary>
    /// <returns></returns>
    public static string GetLogRootNodeName()
    {
        string logRootNodeName = null;
        logRootNodeName = "SystemConfigInfo";
        return logRootNodeName;
    }
    /// <summary>
    /// 得到对话XML路径
    /// </summary>
    /// <returns></returns>
    public static string GetDialogPath()
    {
        string dialogPath = null;

        //安卓或者IPhone环境
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            dialogPath = Application.streamingAssetsPath + "/SystemDialogInfo.xml";
        }
        else
        {
            //Win环境
            dialogPath = "file://" + Application.streamingAssetsPath + "/SystemDialogInfo.xml";
        }
        return dialogPath;
    }
    /// <summary>
    /// 得到对话XML根节点名称
    /// </summary>
    /// <returns></returns>
    public static string GetDialogRootNodeName()
    {
        string dialogRootNodeName = null;
        dialogRootNodeName = "Dialogs_CN";
        return dialogRootNodeName;
    }

}