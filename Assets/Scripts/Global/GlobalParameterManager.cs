using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 公共层：主要作用是跨场景全局数值传递
 */

public static class GlobalParameterManager
{
    //下一个场景的名称
    public static ScenesEnum NextScensName = ScenesEnum.LoginScenes;

    //玩家的姓名
    public static string PlayerName = "枫枫枫子";

    //玩家类型  当选择了某个角色，需要把选择的角色跨场景传输
    public static PlayerEnumType PlayerEnumTypes = PlayerEnumType.SwordHero;    //默认是剑士
}
