using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//控制层  控层敌人的动画播放
public class Ctrl_Warrior_Animation : BaseControl
{
    private Ctrl_BaseEnemyProperty _MyProperty;   //得到本身的属性
    private Animator _animator;     //动画状态机
    private Ctrl_HeroProperty _heroProperty;      //得到英雄的属性
    private bool _IsWarriorDead = true;             //控制播放敌人死亡动画的时候只播放一次

    //当这个敌人对象被启用的时候，就重新的启用敌人的播放动画代码，这样可以防止敌人在对象池中重新启用的时候，这两个代码只会启用一次
    private void OnEnable()
    {

        //启动两个协程动画事件
        StartCoroutine("PlayWarriorAnimation_A");
        StartCoroutine("PlayWarriorAnimation_B");

        //开启单次模式
        _IsWarriorDead=true;
    }
    private void OnDisable()
    {

        //启动两个协程动画事件
        StopCoroutine("PlayWarriorAnimation_A");
        StopCoroutine("PlayWarriorAnimation_B");
        //当敌人被回收到资源池的时候，应该把敌人播放的最后那个动画（死亡动画），重新设置为Idle状态
        if(_animator!=null){
            _animator.SetTrigger("RecoverLife");
        }
    }
    // Use this for initialization
    void Start()
    {
        _MyProperty = this.gameObject.GetComponent<Ctrl_BaseEnemyProperty>();

        //得到动画状态机
        _animator = this.gameObject.GetComponent<Animator>();

        //得到英雄属性
        GameObject goHero = GameObject.FindGameObjectWithTag(Tag.Player);
        if (goHero)
            _heroProperty = goHero.GetComponent<Ctrl_HeroProperty>();

    }

    /// <summary>
    /// 这个动画事件A负责播放敌人的Idle、Walking、Attack
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayWarriorAnimation_A()
    {
        //等待显示完所有的场景之后再执行
        yield return new WaitForEndOfFrame();

        while (true)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);

            switch (_MyProperty.CurrentState)
            {
                case EnemyState.Idle:
                    _animator.SetFloat("MoveSpeed", 0);
                    _animator.SetBool("Attack", false);
                    break;
                case EnemyState.Walking:
                    _animator.SetFloat("MoveSpeed", 1);
                    _animator.SetBool("Attack", false);
                    break;
                case EnemyState.Attack:
                    _animator.SetBool("Attack", true);
                    _animator.SetFloat("MoveSpeed", 0);
                    break;
            }
        }   //switch_end

    }  //Method_End

    /// <summary>
    /// 播放动画事件B  受伤、死亡      两个动画事件的不同在于执行等待时间，上一个是每隔0.1秒就执行一次，一秒钟执行太多次，就可能会导致播放受伤动画时会反复的播放多次
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayWarriorAnimation_B()
    {
        //等待显示完所有的场景之后再执行
        yield return new WaitForEndOfFrame();

        while (true)
        {
            yield return new WaitForSeconds(0.8f);

            switch (_MyProperty.CurrentState)
            {
                case EnemyState.Hurt:
                    _animator.SetTrigger("Hurt");
                    break;
                case EnemyState.Dead:
                    if (_IsWarriorDead)
                    {
                        _animator.SetTrigger("Dead");
                        _IsWarriorDead = false;
                    }
                    break;
            }
        }   //switch_end

    }  //Method_End

    /// <summary>
    /// 攻击主角事件   这个方法将会添加在敌人在攻击动画中某一个时机上
    /// </summary>
    public void AttackHeroByAnimationEvent()
    {
        _heroProperty.DecreaseHealthValues(_MyProperty.EnemyAttack);     //主角受到多少攻击由敌人的攻击力决定
    }

    /// <summary>
    /// 敌人受到攻击时的例子特效
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimationEvent_EnemyHurt()
    {
        base.CreateParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1F, "Prefabs/Effect/spark 04FF", true, transform, transform.position, null, GlobalParameter.INTERVAL_TIME_1F);
        yield break;
        //yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);
        //GameObject goEnemyHurt = ResourcesManager.GetInstance().LoadAsset("Prefabs/Effect/spark 04FF", true);         //加载受伤特效出来
        ////设置特效出现的位置
        //goEnemyHurt.transform.position = this.gameObject.transform.position;
        //goEnemyHurt.transform.SetParent(this.gameObject.transform);

        //Destroy(goEnemyHurt, 1f);
    }
}
