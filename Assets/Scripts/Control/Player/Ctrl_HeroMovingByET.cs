using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层 主角移动控制脚本  通过Easy Touch来控制
public class Ctrl_HeroMovingByET : BaseControl
{
    //使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_ANDROID || UNITY_IPHONE
    //主角移动速度
    public float HeroMovingSpeed=5.0f;
    public float HeroAttackMocingSpeed = 10.0f;

    private CharacterController characterController;
    private float _Gravity = 1.0f;  //角色控制器模拟重力

    void Start()
    {
        //得到角色控制器
        characterController = this.GetComponent<CharacterController>();

        StartCoroutine("AttackByMove");
    }

    /// <summary>
    /// 主角攻击时会向前移动
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackByMove()
    {
        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5F);
        while (true)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT3F);
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.NormalAttack)
            {
                Vector3 movement = transform.forward * Time.deltaTime * HeroMovingSpeed;
                characterController.Move(movement);
            }
        }
    }

#region 事件注册
    //游戏对象启用
    private void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    //游戏对象禁用
    private void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }

    //游戏对象销毁
    public void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }
#endregion

    //摇杆移动中
    private void OnJoystickMove(MovingJoystick move)
    {
        //摇杆的名称
        if (move.joystickName != GlobalParameter.JOYSTICK_NAME)
        {
            return;
            
        }

        //获取摇杆中心偏移的坐标
        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicTrickB)
            {
                //设置角色的朝向（朝向当前坐标+摇杆偏移量）     变成减是为了转换坐标
                transform.LookAt(new Vector3(transform.position.x-joyPositionX,transform.position.y,transform.position.z-joyPositionY));
            }


            //移动玩家的位置
            //transform.Translate(Vector3.forward*Time.deltaTime*5);
           

            Vector3 movement = transform.forward * Time.deltaTime * HeroMovingSpeed;

            //给控制器增加模拟重力
            movement.y -= _Gravity;
            //只有当主角处于休闲状态或者跑动状态才允许移动
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Running)
            {
                //用角色控制器来控制移动
                characterController.Move(movement);
                //播放奔跑动画
                if (UnityHelper.GetInstance().GetSmallTime(0.1f))
                {
                    Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Running); //将播放动画的代码分离出来

                }
            }

        }
    }

    //摇杆结束
    private void OnJoystickMoveEnd(MovingJoystick move)
    {
        //停止时，角色恢复到Idle状态
        if (move.joystickName == GlobalParameter.JOYSTICK_NAME)
        {
           //GetComponent<Animation>().CrossFade(Ani_Idle.name);
            Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
        }
    }

#endif
}
