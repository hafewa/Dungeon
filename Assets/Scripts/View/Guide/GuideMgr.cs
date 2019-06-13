using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*新手引导管理器
	作用：
		控制与协调所有具体新手引导业务脚本的检查与执行

*/

public class GuideMgr : MonoBehaviour {

    //所有新手引导业务逻辑脚本集合
    private List<IGuideTrriger> _LiGuideTrriger=new List<IGuideTrriger>();


	IEnumerator Start () {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT2F);
	    IGuideTrriger iTri_1 = TrrigerDialogs.Instance;
	    IGuideTrriger iTri_2 = TrrigerOperET.Instance;
	    IGuideTrriger iTri_3 = TrrigerOperVirtualKey.Instance;

        _LiGuideTrriger.Add(iTri_1);
        _LiGuideTrriger.Add(iTri_2);
        _LiGuideTrriger.Add(iTri_3);

        //启动业务逻辑脚本检查
	    StartCoroutine("CheckGuideState");
	}

    /// <summary>
    /// 检查引导状态
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckGuideState()
    {
        Log.Write(GetType()+"检查引导状态函数");
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT2F);

        while (true)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5F);

            for (int i = 0; i < _LiGuideTrriger.Count; i++)
            {
                IGuideTrriger iTrriger = _LiGuideTrriger[i];

                //先判断是否满足触发条件,满足后才能运行里面的业务逻辑脚本
                if (iTrriger.CheckCondition())
                {
                    //每个业务脚本执行业务逻辑
                    if (iTrriger.RunOperation())
                    {
                        Log.Write(GetType()+"将即将执行完毕的业务逻辑移除:"+i);
                        _LiGuideTrriger.Remove(iTrriger);   //当业务逻辑执行完毕，就从集合中移除
                    }
                }
            }
        }

    }
	
}
