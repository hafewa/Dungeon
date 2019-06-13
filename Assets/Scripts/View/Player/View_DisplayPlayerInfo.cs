using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//视图层  用于显示玩家的各项文本信息
public class View_DisplayPlayerInfo : MonoBehaviour {


    #region 控件信息
    //屏幕上的信息
    public Text textHeroName;
    public Text textHeroNameByDetailPanel;
    public Slider slidHP;
    public Slider slidMp;
    public Text textCurrentHP;
    public Text textMaxHP;
    public Text textCurrentMP;
    public Text textLevel;
    public Text textMaxMP;
    public Text textExp;
    public Text textGold;
    public Text textDiamonds;

    //玩家详细信息
    public Text textDetailLevel;
    public Text textDetailCurrentHP;
    public Text textDetailMaxHP;
    public Text textDetailCurrentMP;
    public Text textDetailMaxMP;
    public Text textDetailCurrentExp;
    public Text textMaxExp;
    public Text textDetailGold;
    public Text textDetailDiamonds;
    public Text textAttack;
    public Text textDefence;
    public Text textDexterity;
    public Text textKillNum;

    #endregion

    void Awake()
    {
        //核心数值事件注册
        PlayerKernalData.EvePlayerKernalData += DisplayHP;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxHP;
        PlayerKernalData.EvePlayerKernalData += DisplayMP;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxMP;
        PlayerKernalData.EvePlayerKernalData += DisplayAttack;
        PlayerKernalData.EvePlayerKernalData += DisplayDefence;
        PlayerKernalData.EvePlayerKernalData += DisplayDexterity;
        //扩展数值事件注册
        PlayerExternalData.EvePlayerExternalData += DisplayExp;
        PlayerExternalData.EvePlayerExternalData += DisplayKillNum;
        PlayerExternalData.EvePlayerExternalData += DisplayCurrentLevel;
        PlayerExternalData.EvePlayerExternalData += DisplayGold;
        PlayerExternalData.EvePlayerExternalData += DisplayDiamonds;

    }

    
    IEnumerator Start () {

        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT3F);

        //因为这个类的执行顺序必须是要在Ctrl_HeroProperty之后才执行(需要先初始化数据)，所以需要定义一个协程来延迟处理
        PlayerKernalDataProxy.GetInstance().DisplayAllOriValues();
        PlayerExternalDataProxy.GetInstance().DisplayAllOriginalData();

        //给玩家的名字赋值  玩家的姓名不能为空和不允许空格
        //if ((GlobalParameterManager.PlayerName != null) && (!GlobalParameterManager.PlayerName.Trim().Equals("")))
        if (!string.IsNullOrEmpty(GlobalParameterManager.PlayerName))
        {
            textHeroName.text = GlobalParameterManager.PlayerName;
            textHeroNameByDetailPanel.text = GlobalParameterManager.PlayerName;
        }
        else
        {
            Debug.LogError(GetType()+"玩家的姓名不能为空或者空格");
        }
	}

    #region 事件注册
    //以下的方法参考TestModelLayout.cs
    private void DisplayHP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Health"))
        {
            textCurrentHP.text = kv.Value.ToString();      //修改文本显示
            textDetailCurrentHP.text = kv.Value.ToString();
            //处理滑动条
            slidHP.value = (float) kv.Value;
        }
    }
    private void DisplayMaxHP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxHealth"))
        {
            textMaxHP.text = kv.Value.ToString();      //修改文本显示
            textDetailMaxHP.text = kv.Value.ToString();
            //处理滑动条
            slidHP.maxValue = (float)kv.Value;
            slidHP.minValue = 0;
        }
    }
    private void DisplayMP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Magic"))
        {
            textCurrentMP.text = kv.Value.ToString();      //修改文本显示
            textDetailCurrentMP.text = kv.Value.ToString();
            //处理滑动条
            slidMp.value = (float)kv.Value;
        }
    }
    private void DisplayMaxMP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxMagic"))
        {
            textMaxMP.text = kv.Value.ToString();      //修改文本显示
            textDetailMaxMP.text = kv.Value.ToString();
            slidMp.maxValue = (float)kv.Value;
            slidMp.minValue = 0;
        }
    }
    private void DisplayCurrentLevel(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Level"))
        {
            textLevel.text = kv.Value.ToString();      //修改文本显示
            textDetailLevel.text = kv.Value.ToString();
        }
    }
    private void DisplayExp(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Experience"))
        {
            textExp.text = kv.Value.ToString();      //修改文本显示
            textDetailCurrentExp.text = kv.Value.ToString();
        }
    }
    private void DisplayGold(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Gold"))
        {
            textGold.text = kv.Value.ToString();      //修改文本显示
            textDetailGold.text = kv.Value.ToString();
        }
    }
    private void DisplayDiamonds(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Diamonds"))
        {
            textDiamonds.text = kv.Value.ToString();      //修改文本显示
            textDetailDiamonds.text = kv.Value.ToString();      //修改文本显示
        }
    }
    private void DisplayAttack(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Attack"))
        {
            textAttack.text = kv.Value.ToString();      //修改文本显示
        }
    }
    private void DisplayDefence(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Defence"))
        {
            textDefence.text = kv.Value.ToString();      //修改文本显示
        }
    }
    private void DisplayDexterity(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Dexterity"))
        {
            textDexterity.text = kv.Value.ToString();      //修改文本显示
        }
    }
    private void DisplayKillNum(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("KillNumber"))
        {
            textKillNum.text = kv.Value.ToString();      //修改文本显示
        }
    }

    #endregion

}
