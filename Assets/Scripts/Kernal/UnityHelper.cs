using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//核心层帮助类   用于集成大量的通用算法
public class UnityHelper
{
    private static UnityHelper _instance=null;

    public static UnityHelper GetInstance()
    {
        if (_instance == null)
        {
            _instance=new UnityHelper();
        }

        return _instance;
             
    }

    private  UnityHelper()
    {

    }

    private float floDeltaTime; //累加时间

    /// <summary>
    /// 间隔指定时间段，返回布尔值  如果返回真，表示指定的时间段到了
    /// </summary>
    /// <param name="smallIntervalTime">延迟时间</param>
    /// <returns></returns>
    public bool GetSmallTime(float smallIntervalTime)
    {
        floDeltaTime += Time.deltaTime;

        if (floDeltaTime >= smallIntervalTime)
        {
            floDeltaTime = 0;
            return true;
        }
        else 
            return false;
    }

    /// <summary>
    /// 把面向目标对象的方法提取出来   因为在主角中会用到，在敌人中也会用到，这两个都需要面向目标
    /// </summary>
    /// <param name="self">旋转的自身</param>
    /// <param name="goal">需要面向的目标对象</param>
    /// <param name="rotateSpeed">旋转速度</param>
    public void FaceToGo(Transform self,Transform goal,float rotateSpeed)
    {
        //this.transform.LookAt(_TraNearestEnemy);  //如果用这个来关注敌人，会引起主角的x和z轴都会发生旋转，但是主角要面向敌人，只需要y轴旋转就可以了

        //通过四元数的计算来让主角朝向敌人
        self.rotation = Quaternion.Lerp(self.rotation, Quaternion.LookRotation(new Vector3(goal.position.x, 0, goal.position.z) - new Vector3(self.position.x, 0, self.position.z)), rotateSpeed);
    }

    /// <summary>
    /// 产生一个随机数
    /// </summary>
    /// <param name="minNum">最小值</param>
    /// <param name="maxNum">最大值</param>
    /// <returns></returns>
    public int RandomNum(int minNum, int maxNum)
    {
        int result = 0;

        if (minNum == maxNum)
            result = minNum;

        result = Random.Range(minNum, maxNum + 1);      //这个随机数不包括最大值，所有后面加一

        return result;

    }
}
