using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 层消隐技术
///     对于小件物体，只有当主角看得见的时候才显示出来，远距离的时候消失
/// </summary>
public class SmallObjManager : MonoBehaviour
{
    [Header("隐藏的距离")]
    public int DisappearDistance = 10;      //隐藏的距离

    private float[] distanceArray = new float[32];

	// Use this for initialization
	void Start ()
	{
	    distanceArray[9] = DisappearDistance;              //9为自定义的层级的索引值
	    Camera.main.layerCullDistances = distanceArray;


	}
	
}
