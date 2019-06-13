using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectByTime : BaseControl
{
    public float DestroyTime = 3.0f;


	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,DestroyTime);
	}

}
