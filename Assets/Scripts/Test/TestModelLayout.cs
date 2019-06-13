using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//用于测试模型数据
public class TestModelLayout : MonoBehaviour
{
    //测试核心数据的文本
    public Text textHP;
    public Text textMaxHP;
    public Text textMP;
    public Text textMaxMP;
    public Text textATK;
    public Text textMaxATK;
    public Text textDEF;
    public Text textMaxDEF;
    public Text textDEX;
    public Text textMaxDEX;

    //测试扩展数值的文本
    public Text textExp;
    public Text textKillNum;
    public Text textGold;
    public Text textDiamonds;
    public Text textLevel;

    void Awake()
    {
        //注册核心数值的事件
        PlayerKernalData.EvePlayerKernalData += DisplayHP;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxHP;
        PlayerKernalData.EvePlayerKernalData += DisplayMP;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxMP;
        PlayerKernalData.EvePlayerKernalData += DisplayATK;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxATK;
        PlayerKernalData.EvePlayerKernalData += DisplayDEF;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxDEF;
        PlayerKernalData.EvePlayerKernalData += DisplayDEX;
        PlayerKernalData.EvePlayerKernalData += DisplayMaxDEX;

        //注册扩展数值的事件
        PlayerExternalData.EvePlayerExternalData += DisplayExp;
        PlayerExternalData.EvePlayerExternalData += DisplayKillNum;
        PlayerExternalData.EvePlayerExternalData += DisplayLevel;
        PlayerExternalData.EvePlayerExternalData += DisplayGold;
        PlayerExternalData.EvePlayerExternalData += DisplayDiamonds;

    }


    // Use this for initialization
    void Start () {
        //先给这些核心数据赋值
		PlayerKernalDataProxy playerKernalData=new PlayerKernalDataProxy(100,100,10,5,45,100,100,10,50,5,0,0,0);	    
        //显示核心数值初始数值
        PlayerKernalDataProxy.GetInstance().DisplayAllOriValues();

        //显示扩展数值的初始值
        PlayerExternalDataProxy playerExternalData =new PlayerExternalDataProxy(3,0,0,0,0);
        PlayerExternalDataProxy.GetInstance().DisplayAllOriginalData();
	}

    #region 事件用户点击

    public void IncreaseHP()
    {
        PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(30);
    }
    public void DecreaseHP()
    {
        PlayerKernalDataProxy.GetInstance().DecreaseHealthValues(10);
    }
    public void IncreaseMP()
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(40);
    }
    public void DecreaseMP()
    {
        PlayerKernalDataProxy.GetInstance().DecreaseMagicValues(15);
    }

    public void IncreaseExp()
    {
        PlayerExternalDataProxy.GetInstance().AddExp(20);
    }

    #endregion

    #region 事件注册方法

    //显示血量值  当前注册事件的这个键必须是要和PlayerKernalData里面的调用事件的键保持一致
    private void DisplayHP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Health"))
        {
            textHP.text = kv.Value.ToString();      //修改文本显示
        }
    }
    private void DisplayMaxHP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxHealth"))
        {
            textMaxHP.text = kv.Value.ToString();
        }
    }
    private void DisplayMP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Magic"))
        {
            textMP.text = kv.Value.ToString();
        }
    }
    private void DisplayMaxMP(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxMagic"))
        {
            textMaxMP.text = kv.Value.ToString();
        }
    }
    private void DisplayATK(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Attack"))
        {
            textATK.text = kv.Value.ToString();
        }
    }
    private void DisplayMaxATK(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxAttack"))
        {
            textMaxATK.text = kv.Value.ToString();
        }
    }
    private void DisplayDEF(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Defence"))
        {
            textDEF.text = kv.Value.ToString();
        }
    }
    private void DisplayMaxDEF(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxDefence"))
        {
            textMaxDEF.text = kv.Value.ToString();
        }
    }
    private void DisplayDEX(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Dexterity"))
        {
            textDEX.text = kv.Value.ToString();
        }
    }
    private void DisplayMaxDEX(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("MaxDexterity"))
        {
            textMaxDEX.text = kv.Value.ToString();
        }
    }


    private void DisplayExp(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Experience"))
        {
            textExp.text = kv.Value.ToString();
        }
    }
    private void DisplayKillNum(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("KillNumber"))
        {
            textKillNum.text = kv.Value.ToString();
        }
    }
    private void DisplayLevel(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Level"))
        {
            textLevel.text = kv.Value.ToString();
        }
    }
    private void DisplayGold(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Gold"))
        {
            textGold.text = kv.Value.ToString();
        }
    }
    private void DisplayDiamonds(KeyValueUpdate kv)
    {
        if (kv.Key.Equals("Diamonds"))
        {
            textDiamonds.text = kv.Value.ToString();
        }
    }
    #endregion
}
