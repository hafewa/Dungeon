using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideCursorMoving : MonoBehaviour
{
    public GameObject GoMovingGoal;             //移动的目标位置
	// Use this for initialization
	void Start () {
        //1秒的时间移动到目标位置，并且是循环状态
		iTween.MoveTo(this.gameObject,iTween.Hash("position",GoMovingGoal.transform.position,
            "time",2,
            "looptype",iTween.LoopType.loop
		));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
