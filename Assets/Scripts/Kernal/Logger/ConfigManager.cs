using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;		//会用到XDocument的命名空间
using System.IO;            //文件输入输出流命名空间

//配置管理器，读取系统核心XML配置信息
public class ConfigManager : IConfigManager
{
    static Dictionary<string, string> _AppSetting;
    public ConfigManager(string logPath, string xmlRootNodeName)
    {
        _AppSetting = new Dictionary<string, string>();
        InitAndAnalysisXML(logPath, xmlRootNodeName);
    }

    /// <summary>
    /// 初始化解析XML数据到集合中
    /// </summary>
    /// <param name="logPath">日志的路径</param>
    /// <param name="xmlRootNodeName">XML根节点的名称</param>
    void InitAndAnalysisXML(string logPath, string xmlRootNodeName)
    {
        if (string.IsNullOrEmpty(logPath) || string.IsNullOrEmpty(xmlRootNodeName))
        {
            return;
        }
        XDocument xmlDoc;
        XmlReader xmlReader;
        try
        {
            xmlDoc = XDocument.Load(logPath);
            xmlReader = XmlReader.Create(new StringReader(xmlDoc.ToString()));
        }
        catch
        {
            throw new XMLAnalysisException(GetType() + "解析异常");
        }
        //循环解析XML
        while (xmlReader.Read())
        {
            if (xmlReader.IsStartElement() && xmlReader.LocalName == xmlRootNodeName)
            {
                using (XmlReader xmlReaderItem = xmlReader.ReadSubtree())
                {
                    while (xmlReaderItem.Read())
                    {
                        //如果是节点元素
                        if (xmlReaderItem.NodeType == XmlNodeType.Element)
                        {
                            //节点元素
                            string strNode = xmlReaderItem.Name;
                            //都XML当前行的下一个内容
                            xmlReaderItem.Read();
                            //如果是节点内容
                            if (xmlReaderItem.NodeType == XmlNodeType.Text)
                            {
                                //XML当前行，键值对赋值
                                _AppSetting[strNode] = xmlReaderItem.Value;
                            }
                        }
                    }
                }
            }
        }
    }

    public Dictionary<string, string> AppSetting
    {
        get { return _AppSetting; }
    }

    public int GetAppSettingMaxNumber()
    {
        if (_AppSetting != null && _AppSetting.Count > 0)
        {
            return _AppSetting.Count;
        }
        else
        {
            return 0;
        }
    }
}
