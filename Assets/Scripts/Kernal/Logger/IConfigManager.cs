using System.Collections;
using System.Collections.Generic;

/*接口：配置管理器
	作用：读取系统核心XML配置信息
 */
public interface IConfigManager
{
	//属性：应用设置
    Dictionary<string, string> AppSetting { get; }

	//得到APPSetting的最大数量
	int GetAppSettingMaxNumber();
}
