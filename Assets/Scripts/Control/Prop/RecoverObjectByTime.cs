using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回收使用对象缓冲池技术做按时回收对象
public class RecoverObjectByTime : BaseControl
{
    public float RecoverTime = 1;       //回收时间

    private void OnEnable()
    {
        //当这个游戏对象激活的时候，就启用这个协程
        StartCoroutine("RecoverGameObjectByTime");
    }

    private void OnDisable()
    {
        //当这个游戏对象被禁用的时候，停止这个协程
        StopCoroutine("RecoverGameObjectByTime");
    }

    void Start()
    {

    }

    IEnumerator RecoverGameObjectByTime()
    {
        yield return new WaitForSeconds(RecoverTime);
        PoolManager.PoolsArray["ParticalPool"].RecoverGameObjectToPool(this.gameObject);
    }
}
