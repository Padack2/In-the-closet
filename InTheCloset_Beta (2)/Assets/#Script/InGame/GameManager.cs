using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Base")]
    public Text playTime;
    public Image battery;
    public Image hp;
    public GameObject light;
    public DualJoystickPlayerController player;
    public PlayerInventoryManager inventory;
    [Space]

    [Header("Play")]
    public GameObject[] ObjectClear;
    public GameObject clearLight;
    public GameObject gameOver;
    public GameObject HPWaring;
    public GameObject FearWaring;
    public GameObject Waring;
    public GameObject Clear;
    public Light handLight;
    public Light playerLight;
    public int stageLamp;
    public GameObject itemUse;
    public Animator character;
    public GameObject PauseUI;

    [Space]
    [Header("Game Result")]
    public string stageName;
    public int[] fairy;
    public float[] time;
    public int[] coin;
    public int[] wish;
    int resultCoin = 0;
    int resultWish = 0;

    float nowTime;
    int star;

    bool subBattery = false;
    bool music = false;
    Text clearLampNum;
    
	// Use this for initialization
	void Start () {
        clearLampNum = GameObject.Find("ClearNumber").GetComponent<Text>(); //현재 클리어된 램프의 수를 표시하는 텍스트를 불러옴.
        if (inventory.SubBattery > 0) {
            inventory.SubBattery -= 1;      //인벤토리에 아이템이 있으면 자동사용
            subBattery = true;
        }
        if(inventory.BreakMusicBox > 0)
        {
            inventory.BreakMusicBox -= 1;
            music = true;
        }
        if(inventory.Bangle > 0)
        {
            inventory.Bangle -= 1;
            handLight.spotAngle = 40;
        }
        if(inventory.Lens > 0)
        {
            playerLight.range = 7;
            inventory.Lens -= 1;
        }
        nowTime = Time.time;    // 시간 측정을 위해 현재 시간 저장
        
	}
	
	// Update is called once per frame
	void Update () {
        int second = (int)(Time.time - nowTime);    //플레이 시간을 저장

        if (!DataManager.Instance.gameOver)     //아직 게임오버가 아니라면
        {
            playTime.text = "" + (int)second / 60 + ":" + string.Format("{0:D2}", second % 60);     //시간 표시
            clearLampNum.text = DataManager.Instance.lamp + "/" + stageLamp;        // 클리어 램프 수 표시

            if (light.activeSelf)//라이트가 켜져있다면
            {
                if (DataManager.Instance.battery > 0f)      //배터리가 0 이상일 때 배터리 감소
                    DataManager.Instance.battery -= 0.1f;
                else
                {
                    if (DataManager.Instance.battery < 0) DataManager.Instance.battery = 0; //배터리가 0이하로 떨어지면 0으로 다시 변경
                    light.SetActive(false); //라이트를 다시 꺼트림
                }
            }
            battery.fillAmount = DataManager.Instance.battery / DataManager.Instance.MaxBattery;        // Image의 fillAmount를 이용해 점점 줄어드는 게이지 구현
            hp.fillAmount = DataManager.Instance.HP / DataManager.Instance.MaxHP;

            
            if (stageLamp == DataManager.Instance.lamp)     //모든 램프가 켜졌으면
            {
                DataManager.Instance.gameOver = true;       //게임오버 on (이는 게임의 장치를 멈추기 위함임.)
                Clear.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y); //현재 카메라의 위치를 계산하여 카메라 중앙에 클리어 문구 출력
                Clear.SetActive(true);
                for (int i = 0; i < ObjectClear.Length; i++)       //없애야 하는(보이지 않아야 하는) 오브젝트 off
                {
                    ObjectClear[i].SetActive(false);
                }
                PlayerPrefs.SetInt("Fairy", DataManager.Instance.fairy);
                PlayerPrefs.SetFloat("Time", second);
                
                PlayerPrefs.SetString("Now", stageName);                //게임 결과 결산에 필요한 정보 저장

                if (DataManager.Instance.fairy >= fairy[1] && second <= time[1])    //지정된 조건에 맞게 별 갯수 지정
                {
                    //Debug.Log(DataManager.Instance.fairy + ", " + fairy[1]);
                    star = 3;
                }
                else if (DataManager.Instance.fairy >= fairy[0] && second <= time[0])
                {
                    star = 2;
                }
                else star = 1;

                PlayerPrefs.SetInt("Result", star);

                if (PlayerPrefs.GetInt(stageName) <= star)  //전에 플레이했던 기록이 전보다 적거나 없다면
                {
                    
                    if(PlayerPrefs.GetInt(stageName) == 0)
                    {
                        for(int i = 0; i<star; i++)
                        {
                            resultCoin += coin[i];
                            resultWish += wish[i];
                        }
                    }
                    else
                    {
                        for (int i = PlayerPrefs.GetInt(stageName); i < star; i++)
                        {
                            resultWish += wish[i];
                        }
                    }

                    PlayerPrefs.SetInt(stageName, star);
                }
                else
                {                       //전에 플레이했던 기록보다 낮은 기록일 경우 보상 X
                    resultCoin = 0;
                    resultWish = 0;
                }

                PlayerPrefs.SetInt("ResultWish", resultWish);       //보상 저장
                PlayerPrefs.SetInt("ResultCoin", resultCoin);
                
                

                StartCoroutine(goResultScene());
            }

            if (DataManager.Instance.HP <= 0)
            {
                if (subBattery)
                {
                    subBattery = false;
                    DataManager.Instance.HP = DataManager.Instance.MaxHP * 0.2f;

                    if (itemUse.activeSelf)
                    {
                        itemUse.SetActive(false);
                    }

                    itemUse.SetActive(true);
                    
                }
                else
                {
                    DataManager.Instance.gameOver = true;
                    gameOver.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
                    gameOver.SetActive(true);
                    for (int i = 0; i < ObjectClear.Length; i++)
                    {
                        ObjectClear[i].SetActive(false);
                    }
                    PlayerPrefs.SetInt("Result", 0);
                    PlayerPrefs.SetString("Now", stageName);
                    StartCoroutine(goResultScene());
                    character.SetBool("GameOver", true);
                }
                
            } else if (DataManager.Instance.fear <= 0)
            {
                if (music)
                {
                    music = false;
                    DataManager.Instance.fear = DataManager.Instance.MaxFear * 0.2f;

                    if (itemUse.activeSelf)
                    {
                        itemUse.SetActive(false);
                    }

                    itemUse.SetActive(true);
                }
                else
                {
                    character.SetBool("GameOver", true);
                    DataManager.Instance.gameOver = true;
                    gameOver.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
                    gameOver.SetActive(true);
                    for (int i = 0; i < ObjectClear.Length; i++)
                    {
                        ObjectClear[i].SetActive(false);
                    }
                    PlayerPrefs.SetInt("Result", 0);
                    StartCoroutine(goResultScene());
                }
                
            }

            if (DataManager.Instance.HP <= DataManager.Instance.MaxHP * 0.25f && DataManager.Instance.fear <= DataManager.Instance.MaxFear * 0.25f)
            {
                if(!Waring.activeSelf)
                    Waring.SetActive(true);
                if (HPWaring.activeSelf)
                    HPWaring.SetActive(false);
                if (FearWaring.activeSelf)
                    FearWaring.SetActive(false);
            } else if(DataManager.Instance.HP <= DataManager.Instance.MaxHP * 0.25f)
            {
                if (Waring.activeSelf)
                    Waring.SetActive(false);
                if (!HPWaring.activeSelf)
                    HPWaring.SetActive(true);
                if (FearWaring.activeSelf)
                    FearWaring.SetActive(false);
            } else if(DataManager.Instance.fear <= DataManager.Instance.MaxFear * 0.25f)
            {
                if (Waring.activeSelf)
                    Waring.SetActive(false);
                if (HPWaring.activeSelf)
                    HPWaring.SetActive(false);
                if (!FearWaring.activeSelf)
                    FearWaring.SetActive(true);
            }
            else
            {
                if (Waring.activeSelf)
                    Waring.SetActive(false);
                if (HPWaring.activeSelf)
                    HPWaring.SetActive(false);
                if (FearWaring.activeSelf)
                    FearWaring.SetActive(false);
            }
        }
    }

    IEnumerator goResultScene()
    {
        yield return new WaitForSeconds(0.5f);
        if(resultCoin > 0)
            clearLight.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("GameResult");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void Replay()
    {
        SceneMove.Instance.Move("Stage" + stageName);
        Time.timeScale = 1;
    }

    public void MainFunc()
    {
        SceneMove.Instance.Move("Lobby");
        Time.timeScale = 1;
    }

}
