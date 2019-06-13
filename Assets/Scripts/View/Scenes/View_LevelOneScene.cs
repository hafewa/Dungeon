using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_LevelOneScene : MonoBehaviour
{

    public GameObject NormalATK;
    public GameObject MagicATK_A;
    public GameObject MagicATK_B;
    public GameObject MagicATK_C;
    public GameObject MagicATK_D;


    // Use this for initialization
    IEnumerator Start () {
	    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT3F);

        MagicATK_A.GetComponent<View_ATKButtonCDEffect>().EnableSelf();     //启用控件
        MagicATK_B.GetComponent<View_ATKButtonCDEffect>().EnableSelf();     //启用控件
        MagicATK_C.GetComponent<View_ATKButtonCDEffect>().DisableSelf();    //禁用控件
        MagicATK_D.GetComponent<View_ATKButtonCDEffect>().DisableSelf();    //禁用控件
    }
	
}
