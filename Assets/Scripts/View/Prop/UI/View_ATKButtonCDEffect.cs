using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_ATKButtonCDEffect : MonoBehaviour
{
    public Text TextCDColdTime;            //技能冷却倒计时
    public float coldTime=5;              //技能冷却时间
    public Image CircleImg;               //效果图片（外框转动的圆环）
    public Image ImgColorless;          //黑白图片
    public KeyCode keycode;

    private Button _BtnSkill;             //技能按钮
    private float _TimerDeltime;        //累加时间
    private bool IsStartTime = false;   //是否开始计时
    private bool _Enable = false;

    // Use this for initialization
    void Start ()
    {
        _BtnSkill = this.gameObject.GetComponent<Button>();
        TextCDColdTime.enabled = false;     //默认是禁用冷却时间的控件的
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(keycode))
	    {
	        IsStartTime = true;
	        TextCDColdTime.enabled = true;
	    }

	    if (IsStartTime)
	    {
            //冷却的倒计时 = 冷却时间-累加时间
	        TextCDColdTime.text = Mathf.RoundToInt(coldTime - _TimerDeltime).ToString();    //将float类型转化为整形

	        _BtnSkill.interactable = false;      //如果是技能冷却中就禁用按钮的响应
            ImgColorless.gameObject.SetActive(true);
	        _TimerDeltime += Time.deltaTime;
	        CircleImg.fillAmount = _TimerDeltime / coldTime;
	        if (_TimerDeltime >= coldTime)
	        {
                ImgColorless.gameObject.SetActive(false);
	            _TimerDeltime = 0;
	            IsStartTime = false;
                _BtnSkill.interactable = true;      //技能冷却结束再启动按钮
	            TextCDColdTime.enabled = false;
	        }
	    }

	}

    public void ResponseBtnClick()
    {
        //响应按钮点击消息
        IsStartTime = true;

        TextCDColdTime.enabled = true;
    }

    /// <summary>
    ///启用本控件
    /// </summary>
    public void EnableSelf()
    {
        _Enable = true;
        ImgColorless.gameObject.SetActive(false);
        _BtnSkill.interactable = true;
    }
    
    /// <summary>
    /// 禁用本控件
    /// </summary>
    public void DisableSelf()
    {
        _Enable = false;
        ImgColorless.gameObject.SetActive(true);
        _BtnSkill.interactable = false;
    }
}
