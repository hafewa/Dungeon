using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//根据设计模式里面的“开放-封闭原则”和“单一职责”，这个类和PlayerExternalDataProxy分离出来并且只负责计算主角的升级操作
public class UpgradeRule
{
    private static UpgradeRule _instance;

    private UpgradeRule() { }
    //得到本类实例
    public static UpgradeRule GetInstance()
    {
        if (_instance == null)
        {
            _instance=new UpgradeRule();
        }

        return _instance;
    }

    //得到升级条件（等级提升）
    public void GetUpgradeCondition(int experence)
    {
        int currentLevel = 0; //记录当前的等级
        currentLevel = PlayerExternalDataProxy.GetInstance().GetCurrentLevel();

        //当经验值达到条件的时候，调用方法进行升级
        if (experence >= 100&&experence<=300&&currentLevel==0)
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        else if (experence >= 300 && experence <= 500 && currentLevel == 1)
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        else if (experence >= 500 && experence <= 1000 && currentLevel == 2)
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        else if (experence >= 1000 && experence <= 3000 && currentLevel == 3)
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        else if (experence >= 3000 && experence <= 5000 && currentLevel == 4)
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        else if (experence >= 5000 && experence <= 10000 && currentLevel == 5)
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }

    }

    /*升级操作
     *1.所有的核心最大数值发生改变：生命值、魔法值等等
     * 2.最大的核心数值加满为最大数值
     */
    public void UpgradeOperation(LevelName levelName)
    {

        switch (levelName)
        {
            case LevelName.Level_0:
                //定义一个方法  里面的每一个数都是当前等级下的增量
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_1:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_2:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_3:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_4:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_5:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_6:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_7:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_8:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_9:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_10:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
        }
    }  //UpgradeOperation  end

    //具体的升级规则的操作
    private void UpgradeRuleOperation(float hp,float mp,float ATK,float DED,float DEX)
    {
        //所有的核心数值增加
        PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(hp);
        PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(mp);
        PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(ATK);
        PlayerKernalDataProxy.GetInstance().IncreaseMaxDefence(DED);
        PlayerKernalDataProxy.GetInstance().IncreaseMaxDexterity(DEX);

        //对应的“生命数值”和“魔法数值”核心数值，加满最大数值  加大的值为本身当前最大的值

        PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(PlayerKernalDataProxy.GetInstance().GetMaxHealth());

        PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(PlayerKernalDataProxy.GetInstance().GetMaxMagic());
    }
}