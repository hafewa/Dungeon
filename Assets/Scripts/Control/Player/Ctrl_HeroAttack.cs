using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层  用于控制主角的攻击
/*
 * 开发思路：
 * 1.把附近的所有敌人放入数组（敌人数组）
 *   1.1.得到所有的敌人，放入敌人集合
 *   1.2.判断敌人集合，然后找出最近的敌人
 *
 * 2.主角在一定范围内，开始自动“注视”最近的敌人
 *
 * 3.响应输入攻击信号，对于主角正面的敌人予以一定的伤害处理
 */
public class Ctrl_HeroAttack : BaseControl
{

    private List<GameObject> _ListEnemys;   //敌人集合
    private Transform _TraNearestEnemy;    //最近的敌人方位
    private float _MaxDistance=10f; //敌我最大距离

    public float FloMinAttackDistance = 5.0f; //最小攻击距离（关注）
    public float FloRealAttackArea = 2.0f;  //主角实际有效普通攻击距离
    public float FloHeroRotationSpeed = 1.0f;//主角的旋转速率
    public float FloAttackAreaByMagicA = 5.0f;  //主角大招A的攻击范围
    public float FloAttackAreaByMagicB = 8.0f; //大招B的攻击范围
    public int MagicAPower = 5;     //大招A 的攻击力
    public int MagicBPower = 20;    //大招B的攻击力

	// Use this for initialization
	void Awake () {
	    //使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        //事件注册（多播委托:就是一个事件可以注册多个方法） 主角攻击输入(键盘按键) 
        Ctrl_HeroAttackInputByKey.EvePlayerControl += ResponseNormalAttack;

	    Ctrl_HeroAttackInputByKey.EvePlayerControl += ResponseMagicTrickA;

	    Ctrl_HeroAttackInputByKey.EvePlayerControl += ResponseMagicTrickB;
#endif
        //使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_ANDROID || UNITY_IPHONE
        //事件注册（多播委托:就是一个事件可以注册多个方法） 主角攻击输入(虚拟按键) 
        Ctrl_HeroAttackInputByET.EvePlayerControl += ResponseNormalAttack;

	    Ctrl_HeroAttackInputByET.EvePlayerControl += ResponseMagicTrickA;

	    Ctrl_HeroAttackInputByET.EvePlayerControl += ResponseMagicTrickB;
#endif
    }

#region 响应攻击处理


    //响应普通攻击
    public void ResponseNormalAttack(string controlType)
    {
        if (controlType == GlobalParameter.INPUT_MANAGER_ATTACKNAME_NORMAL)
        {
            //如果这个事件被响应 播放普通攻击动画
            Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.NormalAttack);

            //给特定敌人已伤害处理
            AttackEnemyByNormal();
        }
    }

    //响应大招A
    public void ResponseMagicTrickA(string controlType)
    {
        if (controlType == GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKA)
        {
            //如果这个事件被响应 播放大招1攻击动画
            Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickA);
            //给特定敌人已伤害处理
            StartCoroutine("AttackEnemyByMagicA");
        }
    }

    public void ResponseMagicTrickB(string controlType)
    {
        if (controlType ==GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKB)
        {
            //如果这个事件被响应 播放大招2攻击动画
            Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickB);
            //给特定敌人已伤害处理
            StartCoroutine("AttackEnemyByMagicB");
        }
    }

    /// <summary>
    /// 普通攻击敌人
    /// </summary>
    private void AttackEnemyByNormal()
    {
       AttackEnemy(FloRealAttackArea,1);
    }   //Method_end

    /// <summary>
    /// 释放大招A攻击敌人
    ///     大招A是范围攻击，对周围的敌人产生伤害
    /// </summary>
    private IEnumerator AttackEnemyByMagicA()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);
        AttackEnemy(FloAttackAreaByMagicA,MagicAPower,false);
    }   //Method_end

    /// <summary>
    /// 大招B攻击敌人
    ///     大招B是对前方的敌人产生大量的伤害
    /// </summary>
    private IEnumerator AttackEnemyByMagicB()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);
        AttackEnemy(FloAttackAreaByMagicB,MagicBPower);
    }   //Method_end

    /// <summary>
    /// 提取公共的攻击方法
    /// </summary>
    /// <param name="attackArea">攻击范围</param>
    /// <param name="attackPower">攻击力度</param>
    /// <param name="isDirection">是否有方向</param>
    private void AttackEnemy(float attackArea, int attackPower, bool isDirection=true)
    {
        //参数检查  如果敌人数组里没有敌人就不再执行往下的代码
        if (_ListEnemys == null || _ListEnemys.Count <= 0)
        {
            _TraNearestEnemy = null;
            return;
        }

        foreach (GameObject goEnemy in _ListEnemys)
        {
            //需要先判断该敌人是否还存在，这样避免当主角打死敌人之后可能会出现的问题
            if (goEnemy && goEnemy.GetComponent<Ctrl_BaseEnemyProperty>())
            {
                if (goEnemy.GetComponent<Ctrl_BaseEnemyProperty>().CurrentState != EnemyState.Dead)
                {
                    //得到敌我距离
                    float floDistance = Vector3.Distance(this.gameObject.transform.position, goEnemy.transform.position);

                    if (isDirection)
                    {
                        //定义敌我方向
                        Vector3 dir = (goEnemy.transform.position - this.gameObject.transform.position)
                            .normalized; //向量减法  因为向量既表示距离也表示方向，加上normalized目的就是只需要取它的方向

                        //定义主角与敌人的夹角  用向量的“点乘”进行计算
                        float floDirection = Vector3.Dot(dir, this.gameObject.transform.forward); //主角的正方向与敌人方向的夹角

                        //如果主角与敌人在同一个方向，且在有效攻击范围内，则对敌人做伤害处理
                        if (floDirection > 0 && floDistance <= attackArea) //角度大于0代表主角与敌人在同一个方向
                        {
                            goEnemy.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurrentATK() * attackPower, SendMessageOptions.DontRequireReceiver);

                        }
                    }
                    else
                    {
                        //如果主角与敌人在同一个方向，且在有效攻击范围内，则对敌人做伤害处理
                        if (floDistance <= attackArea) //角度大于0代表主角与敌人在同一个方向
                        {
                            goEnemy.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurrentATK() * attackPower, SendMessageOptions.DontRequireReceiver);

                        }
                    }

                    
                }
            }   //if end

        }  //foreach end
    }
    #endregion


    void Start()
    {        
        //集合类型初始化
        _ListEnemys=new List<GameObject>();

        StartCoroutine("RecordNearByEnemyToArray");

        StartCoroutine("HeroRotationEnemy");
    }

    //把附近的所有敌人放入数组
    IEnumerator RecordNearByEnemyToArray()
    {
        while (true)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_2F);  //每隔两秒后再启动这个协程

            GetEnemyToArray();

            GetNearestEnemy();
        }

    }
    //得到所有活着的敌人，放入敌人集合
    private void GetEnemyToArray()
    {
        //清空敌人
        _ListEnemys.Clear();

        GameObject[] goEnemy = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys);

        foreach (GameObject golItem in goEnemy)
        {
            //判断敌人是否还存活  存活就放进列表里面
            //Ctrl_Enemy enemy = golItem.GetComponent<Ctrl_Enemy>();
            Ctrl_BaseEnemyProperty enemy = golItem.GetComponent<Ctrl_BaseEnemyProperty>();
            if (enemy != null && enemy.CurrentState!=EnemyState.Dead)
            {
                _ListEnemys.Add(golItem);

            }

        }
    }
    //判断敌人集合，然后找出最近的敌人
    private void GetNearestEnemy()
    {
        if (_ListEnemys != null&&_ListEnemys.Count>=1)
        {
            foreach (GameObject goEnemy in _ListEnemys)
            {
                //计算得出主角的位置和敌人的位置距离
                float distance = Vector3.Distance(this.gameObject.transform.position, goEnemy.transform.position);

                if (distance < _MaxDistance)
                {
                    _MaxDistance = distance;
                    _TraNearestEnemy = goEnemy.transform;   //得到的最近敌人
                }
            } //foreach End
        }  //if End
    }  //method End

    IEnumerator HeroRotationEnemy()
    {
        while (true)
        {
            //每隔0.5s不断的执行这个协程
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5F);
            //当最近的敌人部位空和当前的动作状态是Idle状态的情况下才让主角朝向敌人
            if (_TraNearestEnemy != null&&Ctrl_HeroAnimationCtrl.Instance.CurrentActionState==HeroActionState.Idle)
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, _TraNearestEnemy.position);
                if (distance < FloMinAttackDistance)
                {

                    //this.transform.LookAt(_TraNearestEnemy);  //如果用这个来关注敌人，会引起主角的x和z轴都会发生旋转，但是主角要面向敌人，只需要y轴旋转就可以了

                    //通过四元数的计算来让主角朝向敌人
                    //this.transform.rotation=Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(new Vector3(_TraNearestEnemy.position.x,0,_TraNearestEnemy.position.z)-new Vector3(this.gameObject.transform.position.x,0,this.gameObject.transform.position.z)), FloHeroRotationSpeed);
                    UnityHelper.GetInstance().FaceToGo(this.gameObject.transform,_TraNearestEnemy,FloHeroRotationSpeed);
                }

            }
        }  //while end
    }
}   //class end
