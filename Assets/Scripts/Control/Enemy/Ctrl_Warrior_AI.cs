using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层 控制敌人的AI  需要处理：1.敌人的思考过程  2.敌人的移动过程
public class Ctrl_Warrior_AI : BaseControl
{
    public float MoveSpeed = 1.0f;                //敌人移动速度
    public float FloHeroRotationSpeed = 1.0f;   //敌人旋转速度
    public float EnemyAttackDistance = 1.0f;    //敌人攻击距离
    public float EnemyCordonDistance = 5.0f;    //敌人警戒距离
    public float EnemyThinkTime = 1.0f;         //敌人思考时间

    private GameObject _Hero;       //主角
    private Transform _MyTransform; //当前战士方位
    private Ctrl_BaseEnemyProperty _MyProperty;   //属性
    private CharacterController _CharacterController;

    //当这个敌人对象被启用的时候，就重新的启用敌人的AI代码，这样可以防止敌人在对象池中重新启用的时候，这两个代码只会启用一次
    private void OnEnable()
    {

        //开启敌人思考的协程
        StartCoroutine("ThinkProcess");

        //开启敌人移动协程
        StartCoroutine("MovingProcess");
    }

    //禁用敌人的AI代码
    private void OnDisable()
    {

        //开启敌人思考的协程
        StopCoroutine("ThinkProcess");

        //开启敌人移动协程
        StopCoroutine("MovingProcess");
    }

    // Use this for initialization
    void Start()
    {
        _MyTransform = this.gameObject.transform;

        /* 确定敌人的个体差异性的数值 */
        MoveSpeed = UnityHelper.GetInstance().RandomNum(1, 2);
        EnemyAttackDistance = UnityHelper.GetInstance().RandomNum(1, 2);
        EnemyCordonDistance = UnityHelper.GetInstance().RandomNum(3, 15);
        EnemyThinkTime = UnityHelper.GetInstance().RandomNum(1, 3);


        //得到当前主角
        _Hero = GameObject.FindGameObjectWithTag(Tags.player);
        //得到当前属性实例
        _MyProperty = this.gameObject.GetComponent<Ctrl_BaseEnemyProperty>();
        //得到角色控制器
        _CharacterController = this.gameObject.GetComponent<CharacterController>();


    }

    //敌人思考过程的协程
    IEnumerator ThinkProcess()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);

        while (true)
        {
            yield return new WaitForSeconds(EnemyThinkTime);

            //敌人当前的状态不能是死亡的状态才有下列的操作
            if (_MyProperty && _MyProperty.CurrentState != EnemyState.Dead)
            {

                //得到主角的当前方位数值
                Vector3 heroPos = _Hero.transform.position;

                //得到主角与当前敌人的距离
                float distance = Vector3.Distance(heroPos, _MyTransform.position);

                //距离判断，如果小于攻击距离
                if (distance < EnemyAttackDistance)
                {
                    //攻击状态
                    _MyProperty.CurrentState = EnemyState.Attack;
                }
                else if (distance < EnemyCordonDistance)
                {
                    //警戒追击距离
                    _MyProperty.CurrentState = EnemyState.Walking;
                }
                else
                {
                    //敌人休闲状态
                    _MyProperty.CurrentState = EnemyState.Idle;
                }

            }
        }
    }

    //敌人移动协程
    IEnumerator MovingProcess()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1F);

        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (_MyProperty && _MyProperty.CurrentState != EnemyState.Dead)
            {
                //敌人需要面对主角
                LookToHero();
                //移动
                switch (_MyProperty.CurrentState)
                {
                    case EnemyState.Walking:
                        //利用向量减法   英雄方位-当前敌人方位
                        Vector3 v = Vector3.ClampMagnitude(_Hero.transform.position - _MyTransform.position, MoveSpeed * Time.deltaTime);
                        _CharacterController.Move(v);
                        break;
                    //敌人受伤会倒退移动
                    case EnemyState.Hurt:
                        Vector3 vec = -transform.forward * FloHeroRotationSpeed / 2 * Time.deltaTime;
                        _CharacterController.Move(vec);
                        break;
                }
            }
        }  //while end
    }   //MovingProcess end

    public void LookToHero()
    {
        //this.transform.rotation = Quaternion.Lerp(_MyTransform.rotation, Quaternion.LookRotation(new Vector3(_Hero.transform.position.x, 0, _Hero.transform.position.z) - new Vector3(_MyTransform.transform.position.x, 0, _MyTransform.transform.position.z)), FloHeroRotationSpeed);

        UnityHelper.GetInstance().FaceToGo(_MyTransform, _Hero.transform, FloHeroRotationSpeed);
    }
}
