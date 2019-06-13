using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源动态加载管理器(脚本插件)
/// 具备“对象缓冲”功能的资源加载脚本
/// </summary>
public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager _instance;
    private Hashtable ht = null;        //容器键值对集合

    private ResourcesManager()
    {
        ht=new Hashtable();
    }

    public static ResourcesManager GetInstance()
    {
        if (_instance == null)
        {
            _instance=new GameObject    ("_ResourceManager").AddComponent<ResourcesManager>();      //实例化出来的是一个GameObject，并且向里面添加一个ResourcesManager脚本
        }

        return _instance;
    }

    /// <summary>
    /// 自定义调用资源，带对象缓冲技术
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="path">路径</param>
    /// <param name="isCatch">是否需要加入缓冲技术</param>
    /// <returns></returns>
    public T LoadResources<T>(string path,bool isCatch) where T:UnityEngine.Object //UnityEngine.Object可以提取所有的资源
    {
        //判断集合里面是否已经有了资源，如果有就直接返回
        if (ht.Contains(path))
        {
            return ht[path] as T;
        }

        T TResource = Resources.Load<T>(path);
        if (TResource == null)
        {
            Debug.LogError(GetType()+"提取的资源找不到，请检查,path="+path);
        }
        else if (isCatch)       //如果资源存在，就添加到集合中
        {
            ht.Add(path,TResource);
        }

        return TResource;

    }

    /// <summary>
    /// 调用资源，带对象缓冲技术(这个特指加载的是GameObject)
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="isCatch">是否需要缓存</param>
    /// <returns></returns>
    public GameObject LoadAsset(string path,bool isCatch)
    {
        GameObject go = LoadResources<GameObject>(path, isCatch);
        //GameObject goClone=GameObject.Instantiate(go) as GameObject;
        GameObject goClone = GameObject.Instantiate<GameObject>(go);

        if (goClone == null)
        {
            Debug.LogError(GetType()+"克隆资源不成功，请检查,path="+path);
        }

        return goClone;
    }

}
