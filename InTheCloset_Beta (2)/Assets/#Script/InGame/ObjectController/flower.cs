using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flower : MonoBehaviour {

    public GameObject player;
    public Light flowerLight;


    public bool one=true;
    public bool ischeck = true;
    public bool isLight = false;
    public bool isOk = false;

    public float moveSpeed=0.2f;
    public float lightScale=0.004f;

    Vector3 dir;
    

    // Update is called once per frame
    void Update()
    {
        if (one)
        {
            if (isLight)
            {
                if (flowerLight.intensity < 3)
                {
                    flowerLight.intensity += 0.05f;
                }
                else isOk = true;
            }
            if (isOk)
            {
                if (ischeck)
                {
                    dir = (gameObject.transform.position - player.transform.position).normalized;
                       ischeck = false;
                }
                dir.z -= lightScale;
                flowerLight.intensity += lightScale;
                flowerLight.transform.Translate(dir * moveSpeed);
                StartCoroutine(durationTime());
            }
        }

    }

    //몬스터 정지 추가 계획

    IEnumerator durationTime()
    {
        yield return new WaitForSeconds(7f);
        one = false;
    }
}
