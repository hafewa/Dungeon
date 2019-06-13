using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制层   敌人的属性
public class Ctrl_WarriorProperty : Ctrl_BaseEnemyProperty
{
    public int Experence = 5;
    public int Attack = 5;
    public int Defence = 3;
    public int MaxHealth = 30;

    void Start()
    {
        base.HeroExperence = Experence;
        base.EnemyAttack = Attack;
        base.EnemyDefence = Defence;
        base.IntMaxHealth = MaxHealth;

        base.RunMethodInChild();
    }
}
