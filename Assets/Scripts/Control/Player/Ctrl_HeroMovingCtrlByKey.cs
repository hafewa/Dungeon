using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层 用于控制主角移动，通过键盘的方式实现 
public class Ctrl_HeroMovingCtrlByKey : BaseControl {
//使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    public float HeroMovingSpeed = 5.0f;

    private CharacterController characterController;
    private float _Gravity = 1.0f;  //角色控制器模拟重力

    void Start()
    {
        //得到角色控制器
        characterController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        ControlMoving();
    }

    //控制主角移动
    private void ControlMoving()
    {
        float floMovingXPos = Input.GetAxis("Horizontal");
        float floMovingYPos = Input.GetAxis("Vertical");

        if (floMovingXPos != 0 || floMovingYPos != 0)
        {
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicTrickB)
            {
                transform.LookAt(new Vector3(transform.position.x - floMovingXPos, transform.position.y, transform.position.z - floMovingYPos));
            }

            Vector3 movement = transform.forward * Time.deltaTime * HeroMovingSpeed;

           //给控制器增加模拟重力
           movement.y -= _Gravity;

            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Running)
            {
                //用角色控制器来控制移动
                characterController.Move(movement);

                if (UnityHelper.GetInstance().GetSmallTime(0.2f))
                {
                    Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Running); //将播放动画的代码分离出来

                }
            }
        }
        else
        {
            //等0.2s再执行  解决了因为下面的这个代码会每一秒执行很多次，这就会导致执行方法的拥挤，有可能代码就一直执行播放着Idle动画，导致有时按其它键会没有反应
            if (UnityHelper.GetInstance().GetSmallTime(0.3f))
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
            }
        }

      
    }
#endif
}
