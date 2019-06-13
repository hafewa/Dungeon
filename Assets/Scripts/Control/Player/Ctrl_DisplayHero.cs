using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//控制层，用于英雄的展示
public class Ctrl_DisplayHero : MonoBehaviour
{

    public AnimationClip AniIdle;      //动画剪辑，休闲状态
    public AnimationClip AniRunnig;
    public AnimationClip AniAttack;

    private Animation _AniCurrentAnimation;
    private float _IntervalTimes = 3f;  //间隔的时间
    private int _RandomPlayNumber;  //随机动作编号

	// Use this for initialization
	void Start ()
	{
	    _AniCurrentAnimation = this.GetComponent<Animation>();
	}
	
    //算法：间隔三秒钟，随机播放一个人物动作
	// Update is called once per frame
	void Update ()
	{
        //每隔3秒得到一个随机数，让人物随机播放一个动作
	    _IntervalTimes -= Time.deltaTime;
	    if (_IntervalTimes <= 0)
	    {
	        _IntervalTimes = 3f;

            //得到一个随机数
	        _RandomPlayNumber = Random.Range(1, 4);
            DisplayHeroPlay(_RandomPlayNumber);
	    }


	}

    //显示英雄的动作  playingId为动作的编号
    internal void DisplayHeroPlay(int playingId)
    {
        switch (playingId)
        {
            case 1:
                DisplayIdle();
                break;
            case 2:
                DisplayRunning();
                break;
            case 3:
                DisplayAttack();
                break;
            case 4:
                break;
        }
    }

    //展示休闲动作
    internal void DisplayIdle()
    {
        if (_AniCurrentAnimation != null)
        {
            //_AniCurrentAnimation.Play(AniIdle.name);
            _AniCurrentAnimation.CrossFade(AniIdle.name);
            
        }
    }

    //展示跑动动画
    internal void DisplayRunning()
    {
        _AniCurrentAnimation.CrossFade(AniAttack.name);
    }

    //展示攻击动画
    internal void DisplayAttack()
    {
        _AniCurrentAnimation.CrossFade(AniRunnig.name);
    }
}
