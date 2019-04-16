using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFloor : MonoBehaviour {

    public bool one = true;
    public bool isLight = false;
    public bool isOk = false;
    public Light gemLight ;
    public GameObject floor;
    private Vector2 objectPosition;
    private Vector2 tempPosition;
    public float moveSpeed;


    public void Start()
    {
        objectPosition = floor.transform.position;
        floor.transform.position -= new Vector3(0, 2, 0);

    }

    // Use this for initialization
    private void Update()
    {
        if (one)
        {
            if (isLight)
            {
                if (gemLight.intensity < 2)
                {

                    gemLight.intensity += 0.05f;
                }
                else
                {
                    isOk = true;
                    floor.SetActive(true);
                }
            }
            if (isOk)
            {
                if (floor.transform.position.y<objectPosition.y)
                {
                    floor.transform.position += new Vector3(0, moveSpeed*Time.deltaTime, 0);
                }
                else
                {
                    Invoke("FadeStart",3);
                    one = false;
                }
            }
        }
    }

    public void FadeStart()
    {
        floor.transform.position -= new Vector3(0, 2);

        one = true;
        isOk = false;
        gemLight.intensity = 0;
        floor.SetActive(false);
    }
}
