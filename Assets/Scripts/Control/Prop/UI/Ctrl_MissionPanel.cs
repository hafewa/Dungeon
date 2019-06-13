using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_MissionPanel : BaseControl
{
    public static Ctrl_MissionPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    //转到第二场景
    public void EnterLevelTowScene()
    {
        base.EnterNextScenes(ScenesEnum.LevelTow);
    }
}
