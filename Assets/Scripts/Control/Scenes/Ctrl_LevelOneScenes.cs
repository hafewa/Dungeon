using System.Collections;
using System.Collections.Generic;
using kernal;
using UnityEngine;
/// <summary>
/// 第一关卡的控制处理
///     1.负责第一关卡的敌人动态加载
///     2.负责多个敌人的出生位置设置
/// </summary>
public class Ctrl_LevelOneScenes : BaseControl
{

    public AudioClip AcBackground;

    public Transform traSpawnEnemyPos_1;        //敌人出现的位置
    public Transform traSpawnEnemyPos_2;
    public Transform traSpawnEnemyPos_3;
    public Transform traSpawnEnemyPos_4;
    public Transform traSpawnEnemyPos_5;
    public Transform traSpawnEnemyPos_6;
    public Transform traSpawnEnemyPos_7;
    public Transform traSpawnEnemyPos_8;
    public Transform traSpawnEnemyPos_9;
    public Transform traSpawnEnemyPos_10;

    //单次开关
    private bool isSimgleTime = true;

    //对象缓冲池，敌人预设
    public GameObject goEnemyPrefabs;
    private void Awake()
    {
        //主角升级的事件注册
        PlayerExternalData.EvePlayerExternalData += LevelUp;
    }


    // Use this for initialization
    void Start()
    {
        AudioManager.PlayBackground(AcBackground);
        AudioManager.SetAudioEffectVolumns(1.0f);
        AudioManager.SetAudioBackgroundVolumns(0.3f);

        StartCoroutine(SpawnEnemy(10));
    }

    // /// <summary>
    // /// 敌人的出生
    // /// </summary>
    // /// <param name="createEnemyNum">敌人出生的数量</param>
    // /// <returns></returns>
    // IEnumerator SpawnEnemy(int createEnemyNum)
    // {
    //     yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5F);

    //     for (int i = 0; i < createEnemyNum; i++)
    //     {
    //         yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);      //每个1s钟产生一个敌人
    //         //GameObject goEnemy=Resources.Load("Prefabs/Enemys/skeleton_king_green",typeof(GameObject)) as GameObject;
    //         //GameObject goClone = GameObject.Instantiate(goEnemy);
    //         //优化算法，Resources.Load这个方法会在每一次加载的时候都查询一次资源，会比较消耗性能，用自己写的方法来优化可以将加载的对象缓存起来
    //         //GameObject goClone = ResourcesManager.GetInstance().LoadAsset("Prefabs/Enemys/skeleton_king_green", true);
    //         //GameObject goClone = ResourcesManager.GetInstance().LoadAsset("Prefabs/Enemys/skeleton_king_yellow", true);
    //         GameObject goClone = ResourcesManager.GetInstance().LoadAsset(RandomEnemyPath(), true);

    //         Transform TraSpawnEnemtPos = GetEnemyRandomSpawnPos();

    //         goClone.transform.position = new Vector3(TraSpawnEnemtPos.position.x, TraSpawnEnemtPos.position.y, TraSpawnEnemtPos.position.z);

    //         goClone.transform.parent = TraSpawnEnemtPos.parent;

    //         //敌人产生时的特效
    //         EnemyDisplayEffect(goClone);
    //     }
    // }

    /// <summary>
    /// 敌人的出生   使用对象缓冲池技术
    /// </summary>
    /// <param name="createEnemyNum">敌人出生的数量</param>
    /// <returns></returns>
    IEnumerator SpawnEnemy(int createEnemyNum)
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5F);

        for (int i = 0; i < createEnemyNum; i++)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);      //每个1s钟产生一个敌人

            //定义克隆体随机出现的位置
            Transform TraSpawnEnemtPos = GetEnemyRandomSpawnPos();
            //克隆位置
            goEnemyPrefabs.transform.position = new Vector3(TraSpawnEnemtPos.position.x, TraSpawnEnemtPos.position.y, TraSpawnEnemtPos.position.z);
            //在对象缓冲池中激活指定的对象
            PoolManager.PoolsArray["EnemyPool"].GetGameObjectByPool(goEnemyPrefabs, goEnemyPrefabs.transform.position, Quaternion.identity);

            //敌人产生时的特效
            //EnemyDisplayEffect(goEnemyPrefabs);
        }
    }

    /// <summary>
    /// 得到敌人随机出生的位置
    /// </summary>
    /// <returns></returns>
    public Transform GetEnemyRandomSpawnPos()
    {
        Transform TraResult = null;

        int TraNum = UnityHelper.GetInstance().RandomNum(1, 10);

        if (TraNum == 1)
        {
            TraResult = traSpawnEnemyPos_1;

        }
        else if (TraNum == 2)
        {
            TraResult = traSpawnEnemyPos_2;
        }
        else if (TraNum == 3)
        {
            TraResult = traSpawnEnemyPos_3;
        }
        else if (TraNum == 4)
        {
            TraResult = traSpawnEnemyPos_4;
        }
        else if (TraNum == 5)
        {
            TraResult = traSpawnEnemyPos_5;
        }
        else if (TraNum == 6)
        {
            TraResult = traSpawnEnemyPos_6;
        }
        else if (TraNum == 7)
        {
            TraResult = traSpawnEnemyPos_7;
        }
        else if (TraNum == 8)
        {
            TraResult = traSpawnEnemyPos_8;
        }
        else if (TraNum == 9)
        {
            TraResult = traSpawnEnemyPos_9;
        }
        else if (TraNum == 10)
        {
            TraResult = traSpawnEnemyPos_10;
        }

        return TraResult;
    }

    /// <summary>
    /// 随机产生的敌人的种类
    /// </summary>
    /// <returns></returns>
    public string RandomEnemyPath()
    {
        string strEnemyPath = "Prefabs/Enemys/skeleton_king_yellow";

        int TraNum = UnityHelper.GetInstance().RandomNum(1, 2);

        if (TraNum == 1)
        {
            strEnemyPath = "Prefabs/Enemys/skeleton_king_yellow";

        }
        else if (TraNum == 2)
        {
            strEnemyPath = "Prefabs/Enemys/skeleton_king_green";
        }


        return strEnemyPath;
    }

    /// <summary>
    /// 敌人出现的特效
    /// </summary>
    /// <param name="goEnemy">敌人对象</param>
    private void EnemyDisplayEffect(GameObject goEnemy)
    {
        StartCoroutine(base.CreateParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1F, "Prefabs/Effect/csPoint", true, goEnemy.transform, goEnemy.transform.position + goEnemy.transform.TransformDirection(0, 3.2f, 0), "EnemyDisplayEffect"));

    }

    //通过事件注册的方式得到主角的升级
    private void LevelUp(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Level"))
        {
            //主角一开始是0级的时候是不用产生升级特效的，所以做一个单次开关，禁止特效在一开始就出现
            if (isSimgleTime)
            {
                isSimgleTime = false;
            }
            else
            {
                //主角的等级发生变化时的操作
                HeroLevelUp();
            }

        }
    }

    //主角升级时产生的技能特效
    private void HeroLevelUp()
    {
        GameObject goLevelUpPartical = ResourcesManager.GetInstance().LoadAsset("Prefabs/Effect/Level_Up", true);
        AudioManager.PlayAudioEffectA("LevelUp");
    }
}
