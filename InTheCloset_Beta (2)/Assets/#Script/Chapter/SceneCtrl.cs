using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour {

    public GameObject[] ButtonParent;
    public Button GameStartBtn;
    public Sprite[] starSprite;
    public Sprite[] iconSprite;
    public Animator Map;
    public Button previous;
    public Button next;
    public GameObject[] selectImage;
    public AudioSource buttonSound;

    string SelectSceneName;
    int page = 1;
    private void Start()
    {

        previous.onClick.AddListener(() => LeftBtn());          //버튼 기능 배정
        next.onClick.AddListener(() => RightBtn());
        GameStartBtn.onClick.AddListener(() => GameStart());

        for(int j = 0; j < ButtonParent.Length; j++)
        {
            for (int i = 1; i < ButtonParent[j].transform.childCount-1; i++)
            {
                string stageName = (j+1) + "-" + i;
                GameObject stageButton = ButtonParent[j].transform.GetChild(i).gameObject;
                stageButton.transform.GetChild(0).GetComponent<Text>().text = stageName;
                Image[] star = stageButton.transform.GetChild(1).GetComponentsInChildren<Image>();
                if (i % 5 == 0) stageButton.GetComponent<Image>().sprite = iconSprite[1];
                else stageButton.GetComponent<Image>().sprite = iconSprite[0];

                for (int k = 0; k < 3; k++)
                {
                    if (k < PlayerPrefs.GetInt(stageName))
                        star[k].sprite = starSprite[1];
                    else
                        star[k].sprite = starSprite[0];
                }
                int temp = j;
                stageButton.GetComponent<Button>().onClick.AddListener(() => select(temp, "Stage" + stageName, stageButton.transform.position));
                stageButton.GetComponent<Button>().interactable = PlayerPrefs.GetInt((j + 1) + "-" + (i-1)) != 0 || stageName.Equals("1-1") ? true : false;
            }
        }
        
    }

    public void select(int chapter, string SceneName, Vector3 pos)
    {
        selectImage[0].SetActive(true);
        GameStartBtn.interactable = true;
        selectImage[0].transform.position = pos;
        SelectSceneName = SceneName;
        buttonSound.Play();
    }

    public void GameStart()
    {
        SceneMove.Instance.Move(SelectSceneName);
        if (GameObject.Find("TutorialObject") != null)
        {
            Destroy(GameObject.Find("TutorialObject"));
        }
    }

    public void LeftBtn()
    {
        page--;
        Map.SetInteger("Page", page);
        if(page == 1)
        {
            previous.interactable = false;
            next.interactable = true;
        }
    }

    public void RightBtn()
    {
        page++;
        Map.SetInteger("Page", page);
        if (page == ButtonParent.Length)
        {
            next.interactable = false;
        }
        if (page == 2)
        {
            selectImage[0].SetActive(false);
            previous.interactable = true;
        }
    }

    public void GoLobby()
    {
        SceneMove.Instance.Move("Lobby");
    }

}
