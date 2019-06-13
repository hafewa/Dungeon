using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrrigerDisplayOrHide : MonoBehaviour
{
    public string TagHeroName = "Player";
    public string TagNameDisplay = "TagNameDisplay";
    public string TagNameHide = "TagNameHide";

    private GameObject[] goDisplayArray;					//需要显示的游戏对象数组
    private GameObject[] goHideArray;						//需要隐藏的游戏对象数组


    // Use this for initialization
    void Start()
    {
        goDisplayArray = GameObject.FindGameObjectsWithTag(TagNameDisplay);
        goHideArray = GameObject.FindGameObjectsWithTag(TagNameHide);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagHeroName)
        {
            foreach (GameObject goItem in goDisplayArray)
            {
                //进入触发区域后，显示需要显示的游戏对象
                goItem.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagHeroName)
        {
            foreach (GameObject goItem in goHideArray)
            {
                //离开触发区域后，隐藏相关的游戏对象
                goItem.SetActive(false);
            }
        }
    }


}
