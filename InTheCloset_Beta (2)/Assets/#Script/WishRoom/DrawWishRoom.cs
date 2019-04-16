using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Level
{
    public enum Effect{
        SPEED = 0, HP = 1, FEAR = 2
    }

    public Button levelBtn;     //레벨버튼
    public GameObject Obj;      //보여야 할 오브젝트
    public Sprite illust;       //일러스트
    public string Title;        //제목
    public string Story;        //설명
    public string number;       //몇번째?
    public int level;
    public string mission;
    public int price;
    public float effectNumber;
    public Effect effect;
}

[System.Serializable]
public class LevelArray
{
    public Level[] level;
}

public class DrawWishRoom : MonoBehaviour {

    [Header("Data")]
    public ParticleSystem star;
    public ParticleSystem Firefly;
    public LevelArray[] level;
    public Sprite[] EffectSprite;

    [Space]
    [Header("UI")]
    public GameObject Explain;
    public Text Story;
    public Text Title;
    public Image illust;
    public Text price;
    public Text effect;
    public Image effectImg;
    public GameObject waring;
    public Text Money;
    public Text Wish;

    MoneyHelper moneyHelper;
    Level nowLevel;
    float speed = 0;
    float heart = 0;
    float fear = 0;

    void Start () {
        moneyHelper = GetComponent<MoneyHelper>();
        Money.text = "" + moneyHelper.Money;
        Wish.text = "" + moneyHelper.Wish;

        if(PlayerPrefs.GetInt("Story") == 0)
            PlayerPrefs.SetInt("Story", 1);

        star.Simulate(100, true, true);
        star.Play();
        Firefly.Simulate(100, true, true);
        Firefly.Play();
        for(int i = 0; i < PlayerPrefs.GetInt("Story"); i++)        //스토리진행이 어디까지 되었는지 호출
        {
            
            for(int j = 0; j <level[i].level.Length; j++)       //스토리 진행이 된 곳까지 오브젝트를 on하기 위한 코드
            {
                Level temp;
                if (level[i].level.Length == 1)                //스토리 -1의 선택지가 없다면 모든 오브젝트 on
                {
                    level[i].level[j].Obj.SetActive(true);
                    temp = level[i].level[j];
                }
                else if (level[i - 1].level.Length == 1)
                {
                    if(PlayerPrefs.GetInt("Story") == i+1) {                  //두갈래 길인데 선택의 기로에 놓였을 땐 다 on
                        level[i].level[j].Obj.SetActive(true);
                        temp = level[i].level[j];
                    }
                    else//하지만 이미 지났다면 선택했던 하나만 on
                    {
                        temp = level[i].level[PlayerPrefs.GetInt("Story" + (i + 1)) - 1];
                        temp.Obj.SetActive(true);
                        
                        if(temp.effect == Level.Effect.HP)
                        {
                            heart += temp.effectNumber;
                        }else if (temp.effect == Level.Effect.FEAR)
                        {
                            fear += temp.effectNumber;
                        }else if (temp.effect == Level.Effect.SPEED)
                        {
                            speed += temp.effectNumber;
                        }


                        temp.levelBtn.onClick.AddListener(() => StoryView(temp));
                        break;
                    }
                    
                }
                else
                {
                    temp = level[i].level[PlayerPrefs.GetInt("Story" + i)-1];
                    temp.Obj.SetActive(true);      //그렇지 않다면 저장해놓은 값에 맞는 오브젝트 On
                }
                temp.levelBtn.onClick.AddListener(() => StoryView(temp));
            }
        }

        PlayerPrefs.SetFloat("HP", heart);
        PlayerPrefs.SetFloat("Fear", fear);
        PlayerPrefs.SetFloat("Speed", speed);

        
	}

    public void StoryView(Level temp)
    {

        if(PlayerPrefs.GetInt(temp.mission) == 0)
        {
            if (waring.activeSelf)
                waring.SetActive(false);
            waring.SetActive(true);

            waring.GetComponent<Text>().text = temp.mission + " 스테이지를 클리어하면 볼 수 있습니다.";
            return;
        }
        Explain.SetActive(true);
        Story.text = temp.Story;
        Title.text = temp.Title;
        price.text = ""+temp.price;
        illust.sprite = temp.illust;

        if (temp.level == PlayerPrefs.GetInt("Story"))
        {
            effect.text = "???";
            effectImg.sprite = EffectSprite[3];
        }
        else {
            effectImg.sprite = EffectSprite[(int)temp.effect];
            effect.text = temp.effectNumber + "%";
        }
        switch (temp.effect)
        {
            case Level.Effect.FEAR:
                break;
            case Level.Effect.HP:
                break;
            case Level.Effect.SPEED:
                break;
        }
        nowLevel = temp;
        //이미 본 스토리면 업그레이드 on 아니면 소원 on하는 코드 넣어야됨.
    }

    public void Ok()
    {
        if(PlayerPrefs.GetInt("Story") == nowLevel.level)
        {
            if(PlayerPrefs.GetInt("Wish") >= nowLevel.price)
            {
                PlayerPrefs.SetInt("Wish", PlayerPrefs.GetInt("Wish") - nowLevel.price);   
                PlayerPrefs.SetInt("Story", PlayerPrefs.GetInt("Story") + 1);
            }
        }
        else
        {
            SceneMove.Instance.Move("Story" + nowLevel.number);
        }
    }

    public void Exit()
    {
        Explain.SetActive(false);       //스토리 설명창 off
    }

    public void goMain()
    {
        SceneMove.Instance.Move("Lobby");
    }
}
