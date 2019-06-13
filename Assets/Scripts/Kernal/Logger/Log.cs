using System.Collections;
using System.Collections.Generic;
using System;			//C# 的核心命名空间
using System.IO;		//文件读写命名空间
using System.Threading;	//多线程命名空间

/*日志调试系统，作用：便于软件和游戏开发调试系统程序
			基本实现原理：1.把开发人员在代码中定义的调试语句，写入本日志的缓存
				   	     2.当缓存中数量超过定义的最大写入文件数值，则把缓存内容调试语句一次性写入文本文件
					 
*/
public static class Log
{
    private static List<string> _LiLogArray;                //Log日志缓存数据
    private static string _LogPath;                         //Log日志文本路径
    private static State _LogState;                         //Log日志状态（部署模式）
    private static int _LogMaxCapacity;                     //日志最大容量
    private static int _LogBufferNumber;                    //日志缓存数量
    //读取的XML里面的字段名称
    private const string CONFIGINFO_LOGPATH = "LogPath";    
    private const string CONFIGINFO_LOGSTATE = "LogState";
    private const string CONFIGINFO_LOGMAXCAPACITY = "LogMaxCapacity";

    private const string CONFIGINFO_LOGBUFFERNUMBER = "LogBufferNumber";
    //写到本地文件名称
    private const string FileTxtName = "\\DungeonFighterLog.txt";
    //Log日志的四种部署模式
    private const string CONFIG_1 = "Develop";
    private const string CONFIG_2 = "Special";
    private const string CONFIG_3 = "Deploy";
    private const string CONFIG_4 = "Stop";



    static Log()
    {
        //日志缓存数据
        _LiLogArray = new List<string>();

        //日志文件路径
        IConfigManager configMgr = new ConfigManager(KernalParameter.GetLogPath(), KernalParameter.GetLogRootNodeName());
        _LogPath = configMgr.AppSetting[CONFIGINFO_LOGPATH];
        if (string.IsNullOrEmpty(_LogPath))
        {
            _LogPath = UnityEngine.Application.persistentDataPath + FileTxtName;
        }
        
        //日志部署模式
        string strLogState = configMgr.AppSetting[CONFIGINFO_LOGSTATE];
        if (!string.IsNullOrEmpty(strLogState))
        {
            switch (strLogState)
            {
                case CONFIG_1:
                    _LogState = State.Develop;
                    break;
                case CONFIG_2:
                    _LogState = State.Speacial;
                    break;
                case CONFIG_3:
                    _LogState = State.Deploy;
                    break;
                case CONFIG_4:
                    _LogState = State.Stop;
                    break;
                default:
                    _LogState = State.Stop;
                    break;
            }
        }
        else
        {
            _LogState = State.Stop;
        }

        //日志最大容量
        string strLogMaxCapcity = configMgr.AppSetting[CONFIGINFO_LOGMAXCAPACITY];
        if (!string.IsNullOrEmpty(strLogMaxCapcity))
        {
            _LogMaxCapacity = Convert.ToInt32(strLogMaxCapcity);
        }
        else
        {
            _LogMaxCapacity = 2000;
        }

        //日志缓存最大容量
        string strLogBufferMaxNumber = configMgr.AppSetting[CONFIGINFO_LOGBUFFERNUMBER];
        if (!string.IsNullOrEmpty(strLogBufferMaxNumber))
        {
            _LogBufferNumber = Convert.ToInt32(strLogBufferMaxNumber);		//转换
        }
        else
        {
            _LogBufferNumber = 1;
        }

        //创建文件
        //查询是否存在指定的文件路径
        if (!File.Exists(_LogPath))
        {
            //没有就创建一个文件
            File.Create(_LogPath);
            //关闭当前线程
            Thread.CurrentThread.Abort();			//终止当前线程
        }
        //把日志文件中的数据同步到日志缓存中		即如果原有的文件中已经有相应的输出文件，应该放入到缓存中
        SyncFileDataToLogArray();
    }

    //把日志文件中的数据同步到日志缓存中
    private static void SyncFileDataToLogArray()
    {
        if (!string.IsNullOrEmpty(_LogPath))
        {
            StreamReader sr = new StreamReader(_LogPath);       //读文件
            while (sr.Peek() >= 0)
            {
                _LiLogArray.Add(sr.ReadLine());		//把读到的文件存到数组中
            }
            sr.Close();		//读写完需要关闭流文件  防止内存泄漏
        }
    }
    
    /// <summary>
    /// 写数据到文件中
    /// </summary>
    /// <param name="writeFileDate">写入的数据</param>
    /// <param name="level">数据的级别</param>
    public static void Write(string writeFileDate, Level level)
    {
        //参数检查
        if (_LogState == State.Stop)
        {
            return;     //如果当前的状态为Stop则不要再写入文件
        }

        //如果日志缓存中的数量超过指定容量，则清空
        if (_LiLogArray.Count >= _LogMaxCapacity)
        {
            _LiLogArray.Clear();		//清空缓存中的数据
        }

        if (!string.IsNullOrEmpty(writeFileDate))
        {
            //增加日期与时间
            writeFileDate = "Log State:" + _LogState.ToString() + "/" + DateTime.Now.ToString() + "/" + writeFileDate;

            //对于不同的日志状态，分特定情形写入文件
            if (level == Level.High)
            {
                //如果是很严重的错误信息，则提示错误
                writeFileDate = "***Error Or Important***" + writeFileDate;
            }
            switch (_LogState)
            {
                case State.Develop:     //开发状态
                    AppendDateFile(writeFileDate);                    //追加调试信息，写入文件
                    break;
                case State.Speacial:    //指定状态
                    if (level == Level.High || level == Level.Special)
                    {
                        AppendDateFile(writeFileDate);
                    }
                    break;
                case State.Deploy:
                    if (level == Level.High)
                    {
                        AppendDateFile(writeFileDate);
                    }
                    break;
                case State.Stop:
                    break;
                default:
                    break;
            }
        }
    }


    public static void Write(string writeFileDate)
    {
        Write(writeFileDate, Level.Low);
    }

    //追加数据文件        参数为调试信息
    private static void AppendDateFile(string writeFileDate)
    {
        if (!string.IsNullOrEmpty(writeFileDate))
        {
            //调试信息数据追加到缓存集合中
            _LiLogArray.Add(writeFileDate);
        }
        //缓存集合数量超过一定阀值，才把信息写到实体文件中
        if (_LiLogArray.Count % _LogBufferNumber == 0)
        {
            //同步缓存中的数据信息到实体文件中
            SynLogArrayToFile();
        }
    }


    #region 重要管理方法
    /// <summary>
    /// 查询缓存中所有数据
    /// </summary>
    /// <returns></returns>
    public static List<string> QueryAllDateLogBuffer()
    {
        if (_LiLogArray != null)
        {
            return _LiLogArray;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 清除实体文件与日志缓存中所有数据
    /// </summary>
    public static void ClearLogFileAndBufferData()
    {
        if (_LiLogArray != null)
        {
            _LiLogArray.Clear();        //数据清零
        }
        SynLogArrayToFile();
    }

    /// <summary>
    /// 同步缓存中的数据信息到实体文件中
    /// </summary>
    public static void SynLogArrayToFile()
    {
        if (!string.IsNullOrEmpty(_LogPath))
        {
            StreamWriter sw = new StreamWriter(_LogPath);
            foreach (string item in _LiLogArray)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }
    }
    #endregion



    #region 本类的枚举类型
    /// <summary>
    /// 日志状态
    /// </summary>
    public enum State
    {
        Develop,    //开发模式(输出所有日志内容)
        Speacial,   //输出指定模式
        Deploy,     //部署模式(只输出最核心日志信息，例如严重错误信息，用户登录账号)
        Stop        //停止输出模式(不输出任何日志信息)
    };

    //调试信息的重要程度
    public enum Level
    {
        High,			//高
        Special,		//指定
        Low				//低
    }
    #endregion

}
