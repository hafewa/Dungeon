using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*新手引导模块
	引导触发接口

 */
public interface IGuideTrriger
{
    /// <summary>
    /// 检查是否触发条件
    /// </summary>
    /// <returns></returns>
    bool CheckCondition();
    /// <summary>
    /// 运行业务逻辑
    /// </summary>
    /// <returns>
    /// true：表示业务逻辑执行完毕
    /// </returns>
    bool RunOperation();
}
