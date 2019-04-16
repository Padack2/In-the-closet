using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    public GameObject TutorialCan;
    public GameObject noSelectImg;
    public GameObject SelectImg;
    public GameObject touch;
    public Text talk;
    bool IsTalk;
    bool Complete;

    [Header("Lobby")]
    public AnimCtrl LobbyFunc;

    public float TalkSpeed;

    int Num = 0;
    bool MentionOK = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            DontDestroyOnLoad(gameObject);
            Num = 1;
        }else if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            DontDestroyOnLoad(gameObject);
            Num = 18;
        }else if(PlayerPrefs.GetInt("Tutorial") == 2)
        {
            DontDestroyOnLoad(gameObject);
            Num = 25;
        }
        else
        {
            if (GameObject.Find("TutorialObject") != null)
            {
                Destroy(GameObject.Find("TutorialObject"));
            }
        }
    }

    // Use this for initialization
    void Update () {
        #region Tutorial1
        if (Num == 1)
        {
            if (!TutorialCan.activeSelf)
            {
                TutorialCan.SetActive(true);
                Talk("우릴 도와준다고 말해줘서 고마워!");
                touch.SetActive(true);
                touch.transform.localPosition = new Vector2(-52, -277);
            }
        }else if(Num == 2)
        {
            if (!MentionOK)
            {
                Talk("역시 넌 내 가장 소중한 친구야!");
            }
        }
        else if (Num == 3)
        {
            if (!MentionOK)
            {
                Talk("하지만 아직 어린 네겐 이 여정이 조금 위험하고 어려울 수도 있어.");
            }
        }
        else if (Num == 4)
        {
            if (!MentionOK)
            {
                Talk("그러니까 되도록 네가 다치지 않게, 우리가 옆에서 도와줄게.");
            }
        }
        else if (Num == 5)
        {
            if (!MentionOK)
            {
                Talk("먼저, 우리의 세계로 가는 법부터 설명할게.");
            }
        }
        else if (Num == 6)
        {
            if (!MentionOK)
            {
                Talk("내 힘으로 우리가 평소에 만나던 옷장을 우리의 세계와 연결시켜 놨어. 옷장을 눌러봐!");
                noSelectImg.SetActive(false);
                SelectImg.SetActive(true);
                SelectImg.transform.localPosition = new Vector2(147.6f, -3);
                touch.transform.localPosition = new Vector2(140.5f, 16.8f);

            }
        }else if(Num == 7)
        {
            if (!MentionOK)
            {
                Talk("잘했어! 이번엔 들어가기 버튼을 눌러볼까?");
                noSelectImg.SetActive(false);
                SelectImg.SetActive(true);
                SelectImg.transform.localPosition = new Vector2(147.6f, -3);
                touch.transform.localPosition = new Vector2(140.5f, 16.8f);
            }
        }else if(Num == 8)
        {
            if (!MentionOK && SceneManager.GetActiveScene().name != "Lobby")
            {
                touch.SetActive(false);
                Talk("짜잔..! 이라고 하기엔 조금 황량한가?");
                noSelectImg.SetActive(true);
                SelectImg.SetActive(false);
            }
        }
        else if (Num == 9)
        {
            if (!MentionOK)
            {
                Talk("여긴 제 1 출구가 있는 숲이야. 현재는 이 곳까지 밖에 지키지 못했어.");
            }
        }
        else if (Num == 10)
        {
            if (!MentionOK)
            {
                Talk("우리의 왕국을 되찾기 위해선, 우리의 힘을 증폭시키고 그들을 무찌를 빛에너지가 필요해.");
            }
        }
        else if (Num == 11)
        {
            if (!MentionOK)
            {
                Talk("저 보석들이 보여? 저게 우리가 복원해야 할 에너지 코어들이야.");
            }
        }
        else if (Num == 12)
        {
            if (!MentionOK)
            {
                Talk("지금은 대부분의 코어가 에너지를 잃어서 들어갈 수 없어.");
            }
        }
        else if (Num == 13)
        {
            if (!MentionOK)
            {
                Talk("하지만 다른 코어를 복원하다 보면 접근할 수 있는 지역이 늘어날거야.");
            }
        }
        else if (Num == 14)
        {
            if (!MentionOK)
            {
                Talk("아참, 그리고 붉은 요정은 우리와 반대로 빛에 약하다는 걸 기억해 줘.");
            }
        }
        else if (Num == 15)
        {
            if (!MentionOK)
            {
                Talk("1-1이라고 써진 코어를 터치해봐!");
                noSelectImg.SetActive(false);
                SelectImg.SetActive(true);
                SelectImg.transform.localPosition = new Vector2(106, -107);
                touch.transform.localPosition = new Vector2(105, -86);
            }
        }
        else if (Num == 16)
        {
            if (!MentionOK)
            {
                noSelectImg.SetActive(true);
                SelectImg.SetActive(false);
                Talk("코어에 빛이 들어왔지? 저건 이 지역에 접근할 수 있다는 뜻이야!");
            }
        }else if (Num == 17)
        {
            if (!MentionOK)
            {
                noSelectImg.SetActive(false);
                Talk("우측 하단의 버튼을 누르면 이 지역으로 들어갈 수 있어. 1-1의 에너지를 되찾고 돌아와 줘.");
                PlayerPrefs.SetInt("Tutorial", 1);
            }
        }else if (Num == 17 && SceneManager.GetActiveScene().name != "Chapter")
        {
            if (GameObject.Find("TutorialObject") != null)
            {
                Destroy(GameObject.Find("TutorialObject"));
            }
        }
        #endregion
        else if(Num == 18)
        {
            if (!TutorialCan.activeSelf && PlayerPrefs.GetInt("1-1") > 0 && SceneManager.GetActiveScene().name == "Lobby")
            {
                TutorialCan.SetActive(true);
                Talk("..대단한걸? 이 정도의 성과를 낼 줄은 몰랐어.");
                touch.SetActive(true);
                touch.transform.localPosition = new Vector2(-52, -277);
            }
        }else if(Num == 19)
        {
            if (!MentionOK)
            {
                Talk("음, 사실 조금 걱정스러워서 네가 없는 사이에 여기저기 손을 좀 댔거든.");
            }
            
        }else if(Num == 20)
        {
            if (!MentionOK)
            {
                Talk("만약 언제든, 언젠가 코어를 복원시키기 어려운 상황이 올땐, 창문과 문을 눌러봐.");
            }
            
        }else if(Num == 21)
        {
            if (!MentionOK)
            {
                Talk("마법의 힘으로 도움을 받을 수 있을거야.");
            }
            
        }
        else if (Num == 22)
        {
            if (!MentionOK)
            {
                Talk("난 다음에 도움이 필요해질 때 다시 오도록 할게. 다른 출구들을 복구시켜야 하거든.");
            }
        }
        else if (Num == 23)
        {
            if (!MentionOK)
            {
                Talk("잘있어!");
                PlayerPrefs.SetInt("Tutorial", 2);

            }
        }else if (Num == 24)
        {
            TutorialCan.SetActive(false);
            
        }

        if (Num == 25 && SceneManager.GetActiveScene().name == "WishRoom")
        {
            if (!MentionOK)
            {

                TutorialCan.SetActive(true);
                Talk("이곳은 소원의 방이야.");
            }
        }else if(Num == 26)
        {
            if (!MentionOK)
            {
                Talk("은별이 네가 우리를 도와주면서 어떻게 행동할지, 어떻게 성장할지를 결정할 수 있어.");
            }
        }
        else if (Num == 27)
        {
            if (!MentionOK)
            {
                Talk("대신 대가가 조금 필요해");
            }
        }
        else if (Num == 28)
        {
            if (!MentionOK)
            {
                Talk("코어를 복구하면서 깃털모양 화폐를 얻었지? 그건 소원이라는 우리의 에너지야.");
            }
        }
        else if (Num == 29)
        {
            if (!MentionOK)
            {
                Talk("하늘에 있는 커다란 별을 보고 소원을 사용해봐!");
            }
        }
        else if (Num == 30)
        {
            if (!MentionOK)
            {
                Talk("아참, 그리고 너의 선택에 따라 앞으로의 운명이 달라질거야. 신중하길 바랄게.");
                PlayerPrefs.SetInt("Tutorial", 3);
            }
        }
        else if(Num == 31)
        {
            if (GameObject.Find("TutorialObject") != null)
            {
                Destroy(GameObject.Find("TutorialObject"));
            }
        }
    }

    void Talk(string mention)
    {
        MentionOK = true;
        StartCoroutine(textAnim(mention));
    }

    IEnumerator textAnim(string mention)
    {
        IsTalk = true;
        Complete = false;
        talk.text = "";
        int n = Num;
        for(int i = 0; i < mention.Length; i++)
        {
            if(n == Num)
            {
                talk.text += mention[i];
                yield return new WaitForSeconds(TalkSpeed);
            }
            if (Complete)
            {
                talk.text = mention; break;
            }
        }
        IsTalk = false;
    }

    public void Touch()
    {
        if (!IsTalk)
        {
            Num++;
            MentionOK = false;
        }
        else
        {
            Complete = true;
        }
    }

    public void ExplainTouch()
    {
        #region Tutorial1
        if (!IsTalk)
        {
            if (Num == 6)
            {
                LobbyFunc.ClosetLightOn();
                Num++;
                MentionOK = false;
            }
            else if (Num == 7)
            {
                LobbyFunc.ClosetScene();
                Num++;
                MentionOK = false;
            }
            else if (Num == 15)
            {
                //Debug.Log(GameObject.Find("SceneCtrl"));
                GameObject.Find("SceneCtrl").GetComponent<SceneCtrl>().select(1, "Stage1-1", GameObject.Find("Tutorial").transform.position);
                Num++;
                MentionOK = false;
            }
        }
        else { Complete = true;  }
        
        #endregion
    }
}
