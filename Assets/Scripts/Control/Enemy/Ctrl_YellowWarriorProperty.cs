using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_YellowWarriorProperty : Ctrl_BaseEnemyProperty
{

    public int Experence = 20;
    public int Attack = 15;
    public int Defence = 5;
    public int MaxHealth = 100;

    void Start()
    {
        base.HeroExperence = Experence;
        base.EnemyAttack = Attack;
        base.EnemyDefence = Defence;
        base.IntMaxHealth = MaxHealth;

        base.RunMethodInChild();
    }
}
