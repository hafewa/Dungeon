using System.Collections;
using System.Collections.Generic;
using kernal;
using UnityEngine;
using UnityEngine.SceneManagement;

//用于创建一些控制层中共同实现的功能
public class BaseControl : MonoBehaviour {

    //进入下一个场景
    protected void EnterNextScenes(ScenesEnum scenesEnumName)
    {
        GlobalParameterManager.NextScensName = scenesEnumName;
        SceneManager.LoadScene(ConvertEnumToString.GetInstance().GetStrByEnumScenes(ScenesEnum.LoadingScenes));
    }

    /// <summary>
    /// 加载粒子特效
    /// </summary>
    /// <param name="interTime">加载特效间隔时间</param>
    /// <param name="particalPath">加载特效资源的路径</param>
    /// <param name="isCatch">是否需要缓存</param>
    /// <param name="traParent">加载出来的特效设置的父对象</param>
    /// <param name="particalPosition">特效的位置</param>
    /// <param name="particalAudio">特效声音（默认为null）</param>
    /// <param name="destroyTime">特效销毁时间（默认为0）</param>
    /// <returns></returns>
    protected IEnumerator CreateParticalEffect(float interTime,string particalPath,bool isCatch,Transform traParent,Vector3 particalPosition,string particalAudio=null,float destroyTime=0)
    {
        //生成特效的间隔时间
        yield return new WaitForSeconds(interTime);
        //加载的特效
        GameObject goParticalEffect = ResourcesManager.GetInstance().LoadAsset(particalPath,isCatch);
        //特效的父对象
        goParticalEffect.transform.parent = traParent;
        //特效出现的位置
        goParticalEffect.transform.position = particalPosition;
        //特效的声音
        if (!string.IsNullOrEmpty(particalAudio))
        {
            AudioManager.PlayAudioEffectA(particalAudio);
        }
        //特效的销毁时间
        if (destroyTime > 0)
        {
            Destroy(goParticalEffect,destroyTime);
        }
    }
}
