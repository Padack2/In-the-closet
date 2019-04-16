using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameResult : MonoBehaviour {

	// Use this for initialization
    public Text Time;
    public Text ResultWish;
    public Text ResultCoin;
    public Text StageName;
    public Text HelpFairy;
    public Image result;
    public Image[] Star;
    public Sprite[] resultSprite;
    public Sprite[] starSprite;
    public MoneyHelper Money;

	void Start () {
        if (PlayerPrefs.GetInt("Result") == 0) // Fail
        {
            StageName.text = PlayerPrefs.GetString("Now");
            result.sprite = resultSprite[0];
        }
        else         //Success
        {
            result.sprite = resultSprite[1];
            float second = PlayerPrefs.GetFloat("Time");
            Time.text = ""+(int)(second / 60) + ":" + string.Format("{0:D2}", (int)second % 60);  //걸린 시간
            ResultWish.text = PlayerPrefs.GetInt("ResultWish").ToString(); //줘야하는 소원 수
            ResultCoin.text = PlayerPrefs.GetInt("ResultCoin").ToString(); //줘야하는 코인 수
            HelpFairy.text = PlayerPrefs.GetInt("Fairy").ToString();  //구한 요정 수
            StageName.text = PlayerPrefs.GetString("Now"); //현재 스테이지 명

            for(int i = 0; i < PlayerPrefs.GetInt("Result"); i++)
            {
                Star[i].sprite = starSprite[1];
            }

            Money.Money += PlayerPrefs.GetInt("ResultCoin");
            Money.Wish += PlayerPrefs.GetInt("ResultWish");
        }
	}

    public void goLobby()
    {
        SceneMove.Instance.Move("Chapter");
    }

    public void Replay()
    {
        SceneMove.Instance.Move("Stage" + PlayerPrefs.GetString("Now"));
    }
}
