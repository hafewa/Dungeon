using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//控制场景的淡入淡出
public class FadeInAndOut : MonoBehaviour
{
    //提供单例给控制层调用
    private static FadeInAndOut _instance;

    public static FadeInAndOut Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject goRawImage; //RawImage的对象
    private RawImage rawImage;   //RawImage的组件
    public float ColorChangeSpeed=1.0f; //颜色的变化速度
    private bool isScenesToClear = true;    //控制屏幕的淡入淡出
    private bool isScenesToBlack = false;

	void Awake()
	{
	    _instance = this;
        if(goRawImage)
	        rawImage =goRawImage.GetComponent<RawImage>();
	}

    //设置场景的淡入 供给外界调用来控制场景是淡入还是淡出
    public void SetScenesToClear()
    {
        isScenesToClear = true;
        isScenesToBlack = false; 
    }

    //设置场景的淡出
    public void SetScenesToBlack()
    {
        isScenesToBlack = true;
        isScenesToClear = false;
    }

	
    //屏幕的淡入(即屏幕逐渐清晰)
    private void FadeToClear()
    {
        //运用Color的插值计算来达到效果 rawImage的颜色从当前的颜色逐渐clear掉
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, Time.deltaTime * ColorChangeSpeed);
    }

    //屏幕逐渐变暗
    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, Time.deltaTime * ColorChangeSpeed);
    }

    private void ScenesToClear()
    {
        FadeToClear();

        //如果rawImage的Alpha值小于0.05  就把颜色完全清理掉，再禁用该组件
        if (rawImage.color.a < 0.1)
        {
            rawImage.color=Color.clear;
            isScenesToClear = false;
            rawImage.enabled = false;
        }
    }

    //屏幕淡出 逐渐变暗
    private void ScenesToBlack()
    {
        rawImage.enabled = true;
        FadeToBlack();
        if (rawImage.color.a > 0.95)
        {
            rawImage.color=Color.black;
            isScenesToBlack = false;
        }
    }

	// Update is called once per frame
	void Update () {

        //时刻监听屏幕的淡入淡出
	    if (isScenesToClear)
	    {
            ScenesToClear();
	    }
        else if (isScenesToBlack)
	    {
            ScenesToBlack();
	    }

	}
}
