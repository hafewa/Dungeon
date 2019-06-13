using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectRotation : BaseControl
{

    public float RotateSpeed = 2.0f;

    private void Update()
    {
		this.gameObject.transform.Rotate(Vector3.up,RotateSpeed);
    }


}
