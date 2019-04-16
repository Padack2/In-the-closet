using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JemController : MonoBehaviour {

    [Header("Function")]
    public float speed;
    public float nowTime;
    public float time;
    public bool isLight;
    public bool isOk = false;
    bool one = true;
    private bool oneSoundPlay = true;

    [Space]
    [Header("Animation")]
    public Light GemLight;
    public GameObject interactionObject;
    public Vector2 objectPosition;
    [Space]
    [Header("Lock")]
    public GameObject Puzzle;
    public Sprite[] puzzleSprite;
    public Image puzzleImg;
    public InputField answer;
    public Button AnswerCheck;


    // Update is called once per frame
    void Update()
    {
        if (one)    //첫번째 젬 발동이면
        {
            if (isLight && !isOk)       //빛이 컨트롤러에 비춰졌고, 현재 젬컨트롤러에 불이 켜진 상태가 아님
            {
                if (Time.time - nowTime >= time)    //LightCollider 스크립트에서 젬컨트롤러와 마주치면 현재 시간을 설정, 그 후로 불이 켜져있으면서
                {                                   //흐른 시간을 측정, 설정한 time을 지났는지 체크한다. 지났다면 불 켜짐(isOk)를 true로 바꿈)
                    isOk = true;
                    interactionObject.SetActive(true);
                    if (interactionObject.gameObject.tag == "gem")
                        interactionObject.transform.position = new Vector3(objectPosition.x, objectPosition.y - 2.5f);
                    //오브젝트가 올라오는 효과를 주기 위해 상호작용 오브젝트(interactionObject)가 gem이라는 태그에 속한다면 상호작용 오브젝트가
                    //나타날 지점에서 -2.5 떨어진 곳에 오브젝트의 위치를 지정함.
                }
                else
                {
                    GemLight.intensity = ((Time.time - nowTime) / time) * 5;    //내가 정한 시간에 따라 gem의 라이트를 바꿈
                    //젬 라이트의 빛 강도 = (흐른 시간 / 흘러야 할 시간) * 내가 최종적으로 지정하고 싶은 빛의 강도
                }
            }

            if (isOk)   //빛이 모두 켜졌다.
            {
                if (oneSoundPlay)
                {
                    oneSoundPlay = false;
                    gameObject.GetComponent<AudioSource>().Play();

                }
                if (interactionObject.gameObject.tag == "ladder" || interactionObject.gameObject.tag == "floor") //상호작용 오브젝트가 사다리 또는 바닥이면
                {
                    if (interactionObject.GetComponent<SpriteRenderer>().color.a <= 255)//투명한 오브젝트가 선명해지는 효과를 주기위한 코드
                        interactionObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.05f);

                    if (interactionObject.transform.position.y <= objectPosition.y)     //밑에 있던 오브젝트를 원래 있어야 할 위치로 올려주는 코드
                        interactionObject.transform.position += new Vector3(0, speed * Time.deltaTime);

                    if (interactionObject.GetComponent<SpriteRenderer>().color.a > 255 && interactionObject.transform.position.y >= objectPosition.y)
                    {
                        one = false;
                    }
                }
                else if (interactionObject.gameObject.tag == "lock")    //자물쇠면
                {
                    if (interactionObject.GetComponent<SpriteRenderer>().color.a > 0)       //점점 투명하게 없어지는 애니메이션 코드
                        interactionObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.05f);
                    else
                    {
                        Puzzle.SetActive(true);
                        int a = Random.Range(0, puzzleSprite.Length);
                        puzzleImg.sprite = puzzleSprite[a];

                         AnswerCheck.onClick.RemoveAllListeners();
                        switch (a) {
                            case 0:
                            AnswerCheck.onClick.AddListener(() =>AnswerCheckFunction("11"));
                            break;
                            case 1:
                                AnswerCheck.onClick.AddListener(() => AnswerCheckFunction("15"));
                                break;
                            case 2:
                                AnswerCheck.onClick.AddListener(() => AnswerCheckFunction("3"));
                                break;
                            case 3:
                                AnswerCheck.onClick.AddListener(() => AnswerCheckFunction("4"));
                                break;
                        }


                        one = false;
                    }
                }
            }
        }

        
       
    }
    private void AnswerCheckFunction(string ans)
    {
        if (ans == answer.text)
        {
            interactionObject.SetActive(false);
            Puzzle.SetActive(false);
        }
        else
        {
            GameObject.Find("Hurt").GetComponent<AudioSource>().Play();
            answer.text = "";
        }
            
        return;
    }


}
