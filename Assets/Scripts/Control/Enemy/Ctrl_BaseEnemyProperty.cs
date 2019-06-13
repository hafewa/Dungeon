using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_BaseEnemyProperty : BaseControl
{
    public int HeroExperence = 0;
    public int EnemyAttack = 0;
    public int EnemyDefence = 0;

    public int IntMaxHealth = 20;   //最大的生命值
    private float FloCurrentHealth = 0; //当前的生命数值

    private EnemyState _CurrentState = EnemyState.Idle; //当前状态
    //属性：当前的状态
    public EnemyState CurrentState
    {
        get
        {
            return _CurrentState;
        }

        set
        {
            _CurrentState = value;
        }
    }

    private void OnEnable() {
        
        //启动一个协程 判断生命是否存活
        StartCoroutine("CheckLifeContinue");
    }
    private void OnDisable() {
        
        //禁用一个协程
        StopCoroutine("CheckLifeContinue");
    }


    public void RunMethodInChild()
    {
        FloCurrentHealth = IntMaxHealth;

    }

    //判断敌人是否存活
    IEnumerator CheckLifeContinue()
    {

        while (true)
        {
            //每隔2秒检查一次敌人是否死亡
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);

            //敌人当前的血量值已经耗尽 设置为死亡状态
            if (FloCurrentHealth <= IntMaxHealth * 0.01)
            {
                //打死一个小怪，增加经验值
                Ctrl_HeroProperty.Instance.AddExp(HeroExperence);
                //增杀敌数量
                Ctrl_HeroProperty.Instance.AddKillNumber();
                _CurrentState = EnemyState.Dead;
                //利用缓冲池的技术对敌人进行回收，而不是直接销毁掉
                StartCoroutine("RecoverEnemy");
                //Destroy(this.gameObject, 5f);        //延迟5秒中在销毁这个对象
            }
        }

    }

    //伤害处理
    public void OnHurt(int hurtValue)
    {
        //当前的状态设置为受伤
        _CurrentState = EnemyState.Hurt;

        int hurtValus = 0;
        hurtValus = Mathf.Abs(hurtValue);   //取绝对值  伤害值不会是负数
        if (hurtValus > 0)
        {
            FloCurrentHealth -= hurtValus;
        }

    }

    //回收对象
    IEnumerator RecoverEnemy()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_3F);
        //敌人回收前，重置敌人的属性
        FloCurrentHealth = IntMaxHealth;
        _CurrentState = EnemyState.Idle;
        //回收敌人对象
        PoolManager.PoolsArray["EnemyPool"].RecoverGameObjectToPool(this.gameObject);
    }
}
