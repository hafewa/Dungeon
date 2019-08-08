using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//公共层 将枚举类型转换成字符串
public class ConvertEnumToString
{
    private static ConvertEnumToString _instance;

    //用字典存放场景的枚举类型
    private Dictionary<ScenesEnum, string> _DicScenesEnumLib; 

    //构造函数
    private ConvertEnumToString()
    {
        _DicScenesEnumLib=new Dictionary<ScenesEnum, string>();
        _DicScenesEnumLib.Add(ScenesEnum.StartScenes, "01_StartScene");
        _DicScenesEnumLib.Add(ScenesEnum.LoginScenes, "02_LoginScene");
        _DicScenesEnumLib.Add(ScenesEnum.LoadingScenes, "LoadingScene");
        _DicScenesEnumLib.Add(ScenesEnum.LevelOne,"03_LevelOneScene");
        _DicScenesEnumLib.Add(ScenesEnum.LevelTow,"05_LevelTowScene");
        _DicScenesEnumLib.Add(ScenesEnum.MainCityScene,"04_MainCity");
        _DicScenesEnumLib.Add(ScenesEnum.TestDialogScene, "TestDialogScene");
    }

    public static ConvertEnumToString GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ConvertEnumToString();
        }

        return _instance;
    }

    //得到字符串形式的场景名称
    public string GetStrByEnumScenes(ScenesEnum scenesEnum)
    {
        if (_DicScenesEnumLib != null && _DicScenesEnumLib.Count >= 1)
        {
            return _DicScenesEnumLib[scenesEnum];
        }
        else
        {
            Debug.LogWarning(GetType() + "没有得到场景的枚举类型");
            return null;
        }
    }
}

