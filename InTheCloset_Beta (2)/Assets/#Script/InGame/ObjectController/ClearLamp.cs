using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLamp : MonoBehaviour {

    [Header("Function")]
    public float speed;
    public float nowTime;
    public float time;
    public bool isLight = false;
    public bool isOk = false;
    bool one = true;

    [Space]
    [Header("Animation")]
    public Light GemLight;

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
                gameObject.GetComponent<AudioSource>().Play();
                DataManager.Instance.lamp++;
                one = false;
            }
        }

    }
}
