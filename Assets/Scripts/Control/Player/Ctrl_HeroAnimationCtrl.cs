using System.Collections;
using System.Collections.Generic;
using kernal;
using UnityEngine;


//控制层  用于控制主角的动画播放
public class Ctrl_HeroAnimationCtrl : BaseControl
{
    private static Ctrl_HeroAnimationCtrl _instance;

    public static Ctrl_HeroAnimationCtrl Instance
    {
        get { return _instance; }
    }

    //属性：当前的主角的动作状态  设置为只读的形式
    public HeroActionState CurrentActionState
    {
        get
        {
            return _CurrentActionState;
        }

    }

    private HeroActionState _CurrentActionState = HeroActionState.None;

    //定义动画剪辑
    public AnimationClip Ani_Idle;
    public AnimationClip Ani_Running;
    public AnimationClip Ani_NormalAttack1;
    public AnimationClip Ani_NormalAttack2;
    public AnimationClip Ani_NormalAttack3;
    public AnimationClip Ani_MagicAttackA;
    public AnimationClip Ani_MagicAttackB;

    public AudioClip AcHeroRunning;     //主角跑动声音

    //定义动画句柄
    private Animation _AnimationHandle;
    //定义动画连招
    private NormalAttackComboState _CurrentAttackCombo = NormalAttackComboState.NorlmalAttack1;


    //对象缓冲池：主角剑法例子特效
    public GameObject goHeroNormalAttackEffectByLeft;
    public GameObject goHeroNormalAttackEffectByRight;
    public GameObject goHeroNormalAttackEffectMid;
    //定义动画单词开关
    private bool _IsSinglePlay = true;

    // Use this for initialization
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //默认动作状态
        _CurrentActionState = HeroActionState.Idle;

        //得到动画句柄实例
        _AnimationHandle = this.GetComponent<Animation>();

        //启动协程 控制主角动画状态
        StartCoroutine("ControlHeroAnimationState");

        HeroDisplayEffect();
    }

    //设置当前的动画状态
    public void SetCurrentActionState(HeroActionState heroActionState)
    {
        _CurrentActionState = heroActionState;
    }

    /// <summary>
    /// 控制主角的动画播放
    /// </summary>
    /// <returns></returns>
    IEnumerator ControlHeroAnimationState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            switch (CurrentActionState)
            {
                case HeroActionState.NormalAttack:
                    /*
                     * 攻击连招处理
                     * 自动状态转换
                     */
                    switch (_CurrentAttackCombo)
                    {

                        case NormalAttackComboState.NorlmalAttack1:
                            //优化代码   解决点击一次普通攻击会有触发两次的问题
                            _CurrentAttackCombo = NormalAttackComboState.NorlmalAttack2;
                            _AnimationHandle.CrossFade(Ani_NormalAttack1.name);
                            AudioManager.PlayAudioEffectA("BeiJi_DaoJian_1");
                            yield return new WaitForSeconds(Ani_NormalAttack1.length);  //为了让动画播放完
                            _CurrentActionState = HeroActionState.Idle;
                            break;
                        case NormalAttackComboState.NorlmalAttack2:
                            //优化代码   解决点击一次普通攻击会有触发两次的问题
                            _CurrentAttackCombo = NormalAttackComboState.NorlmalAttack3;
                            _AnimationHandle.CrossFade(Ani_NormalAttack2.name);
                            AudioManager.PlayAudioEffectA("BeiJi_DaoJian_2");
                            yield return new WaitForSeconds(Ani_NormalAttack2.length);  //为了让动画播放完
                            _CurrentActionState = HeroActionState.Idle;
                            break;
                        case NormalAttackComboState.NorlmalAttack3:
                            //优化代码   解决点击一次普通攻击会有触发两次的问题
                            _CurrentAttackCombo = NormalAttackComboState.NorlmalAttack1;
                            _AnimationHandle.CrossFade(Ani_NormalAttack3.name);
                            AudioManager.PlayAudioEffectA("BeiJi_DaoJian_3");
                            yield return new WaitForSeconds(Ani_NormalAttack3.length);  //为了让动画播放完
                            _CurrentActionState = HeroActionState.Idle;
                            break;
                    }

                    break;
                case HeroActionState.MagicTrickA:
                    _AnimationHandle.CrossFade(Ani_MagicAttackA.name);
                    AudioManager.PlayAudioEffectA("Hero_MagicA");
                    yield return new WaitForSeconds(Ani_MagicAttackA.length);  //为了让动画播放完

                    _CurrentActionState = HeroActionState.Idle;
                    break;
                case HeroActionState.MagicTrickB:
                    //解决点击大招时的灵敏度问题
                    _AnimationHandle.CrossFade(Ani_MagicAttackB.name);
                    AudioManager.PlayAudioEffectA("Hero_MagicB");
                    yield return new WaitForSeconds(Ani_MagicAttackB.length);  //为了让动画播放完
                    _CurrentActionState = HeroActionState.Idle;
                    break;

                case HeroActionState.None:
                    break;
                case HeroActionState.Idle:
                    //播放动画效果
                    _AnimationHandle.CrossFade(Ani_Idle.name);
                    break;
                case HeroActionState.Running:
                    _AnimationHandle.CrossFade(Ani_Running.name);
                    //处理主角跑动音效
                    AudioManager.PlayAudioEffectB(AcHeroRunning);
                    yield return new WaitForSeconds(AcHeroRunning.length);
                    break;
            }   //switch End
        }       //while End

    }

    /// <summary>
    /// 大招A粒子特效
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimationEvent_MagicA()
    {
        StartCoroutine(base.CreateParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1F, "Prefabs/Effect/Skill01", true, this.gameObject.transform,
            this.gameObject.transform.position));
        yield break;
    }

    /// <summary>
    /// 大招B粒子特效
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimationEvent_MagicB()
    {
        StartCoroutine(base.CreateParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1F,
            "Prefabs/Effect/Skill02", true, this.gameObject.transform,
            this.gameObject.transform.position + this.gameObject.transform.TransformDirection(new Vector3(0, 0, 5))));
        yield break;
    }

    /// <summary>
    /// 主角普通攻击粒子特效1
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimationEvent_NormalAttackA()
    {
        //设置特效出现的位置
        goHeroNormalAttackEffectByLeft.transform.position = this.gameObject.transform.position + this.gameObject.transform.TransformDirection(new Vector3(0, 0, 1));
        //使用对象缓冲池技术     在缓冲池中，得到一个指定的预设激活体
        PoolManager.PoolsArray["ParticalPool"].GetGameObjectByPool(goHeroNormalAttackEffectByLeft, goHeroNormalAttackEffectByLeft.transform.position, Quaternion.identity);

        //不使用对象缓冲池技术的做法
        // StartCoroutine(base.CreateParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1F, "Prefabs/Effect/attack01", true,
        //     this.gameObject.transform,
        //     this.transform.position + this.gameObject.transform.TransformDirection(new Vector3(0, 0, 1)), null, 1.0f));
        yield break;

        //yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);
        //GameObject goNaomalAttack = ResourcesManager.GetInstance().LoadAsset("Prefabs/Effect/attack01", true);         //加载技能A特效出来

        //Destroy(goNaomalAttack,1f);
    }

    /// <summary>
    /// 主角普通攻击粒子特效2
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimationEvent_NormalAttackB()
    {
        // StartCoroutine(base.CreateParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1F, "Prefabs/Effect/attack04", true,
        //     this.gameObject.transform, this.transform.position + this.gameObject.transform.TransformDirection(new Vector3(0, 0, 1)), null, GlobalParameter.INTERVAL_TIME_1F));
        
        //设置特效出现的位置
        goHeroNormalAttackEffectByRight.transform.position = this.gameObject.transform.position + this.gameObject.transform.TransformDirection(new Vector3(0, 0, 1));
        //使用对象缓冲池技术     在缓冲池中，得到一个指定的预设激活体
        PoolManager.PoolsArray["ParticalPool"].GetGameObjectByPool(goHeroNormalAttackEffectByRight, goHeroNormalAttackEffectByRight.transform.position, Quaternion.identity);

        yield break;
        //yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1F);
        //GameObject goNaomalAttack = ResourcesManager.GetInstance().LoadAsset("Prefabs/Effect/attack04", true);         //加载技能A特效出来
        //设置特效出现的位置
        //goNaomalAttack.transform.position = this.gameObject.transform.position + this.gameObject.transform.TransformDirection(new Vector3(0, 0, 1));

        //Destroy(goNaomalAttack, 1f);
    }

    public IEnumerator AnimationEvent_NormalAttackC(){
        goHeroNormalAttackEffectMid.transform.position=this.gameObject.transform.position+this.transform.TransformDirection(new Vector3(0,0,1));
        PoolManager.PoolsArray["ParticalPool"].GetGameObjectByPool(goHeroNormalAttackEffectMid,goHeroNormalAttackEffectMid.transform.position,Quaternion.identity);
        yield break;
    }

    /// <summary>
    /// 主角出现的特效
    /// </summary>
    private void HeroDisplayEffect()
    {
        StartCoroutine(base.CreateParticalEffect(0, "Prefabs/Effect/EnemySpawnEff", true, this.gameObject.transform,
            this.gameObject.transform.position));

        //GameObject goDisplayEffect = ResourcesManager.GetInstance().LoadAsset("Prefabs/Effect/EnemySpawnEff", true);
        //goDisplayEffect.transform.position = this.gameObject.transform.position;
        //goDisplayEffect.transform.parent = this.transform;
    }
}
