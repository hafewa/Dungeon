using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层  用于控制主角输入的攻击，通过键盘按键来实现
public class Ctrl_HeroAttackInputByKey : BaseControl
{
    //使用预编译指令来优化代码    不同平台使用不同的代码
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    //事件：主角控制事件
    public static event del_PlayerControlWithStr EvePlayerControl;

	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton(GlobalParameter.INPUT_MANAGER_ATTACKNAME_NORMAL))
	    {
	        if (EvePlayerControl != null)
	        {
	            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_NORMAL);
	        }
	    }
        else if (Input.GetButtonDown(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKA))
	    {
	        if (EvePlayerControl != null)
	        {
	            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKA);
	        }
        }
        else if (Input.GetButtonDown(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKB))
	    {
	        if (EvePlayerControl != null)
	        {
	            EvePlayerControl(GlobalParameter.INPUT_MANAGER_ATTACKNAME_MAGICTRICKB);
	        }
        }
	}   //update End

#endif
} //class End
