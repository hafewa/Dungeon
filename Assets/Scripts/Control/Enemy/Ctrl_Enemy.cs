using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Enemy : BaseControl
{
    public int HeroExperence=5;
    private bool _IsAlive = true;    //是否生存
    public int IntMaxHealth = 20;   //最大的生命值
    private float FloCurrentHealth = 0; //当前的生命数值

    //属性：是否存活
    public bool IsAlive
    {
        get
        {
            return _IsAlive;
        }

    }


    // Use this for initialization
    void Start ()
	{
	    FloCurrentHealth = IntMaxHealth;

        //启动一个协程 判断生命是否存活
	    StartCoroutine("CheckLifeContinue");
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
                _IsAlive = false;

                //打死一个小怪，增加经验值
                Ctrl_HeroProperty.Instance.AddExp(HeroExperence);
                //增肌杀敌数量
                Ctrl_HeroProperty.Instance.AddKillNumber();

                Destroy(this.gameObject);
            }
        }

    }

    //伤害处理
    public void  OnHurt(int hurtValue)
    {
        Debug.Log(GetType()+ "伤害处理");

        int hurtValus = 0;
        hurtValus = Mathf.Abs(hurtValue);   //取绝对值  伤害值不会是负数
        if (hurtValus > 0)
        {
            FloCurrentHealth -= hurtValus;
        }

    }

	
}
