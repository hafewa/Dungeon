using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_ATKNormalPressed : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    //使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_ANDROID || UNITY_IPHONE
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //因为这个函数一秒钟执行太多次，需要限制一下执行的次数
        if (UnityHelper.GetInstance().GetSmallTime(GlobalParameter.INTERVAL_TIME_0DOT2F))
        {

            Ctrl_HeroAttackInputByET.Instance.OnResponseNormalAttack();

        }
    }
#endif
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
