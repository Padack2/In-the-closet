using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

    bool isBreak = false;
    public float delayTime;
    public float strong;
    float nowTime;


    private Vector2 _originPos;
    Vector2 currentPos;
    Vector3 v;

    public void Start()
    {
        _originPos = gameObject.transform.position;
        currentPos = transform.localPosition;
        strong = 0.2f;
    }
    
    private void Update()
    {
        if (isBreak)
        {
            if(Time.time - nowTime < delayTime)
            {
                //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-strong, strong), Random.Range(-strong, strong));
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition,
                new Vector3(currentPos.x + Random.Range(-strong , strong), currentPos.y + Random.Range(-strong, strong)),
                
                ref v, smoothTime: 0.1f);
            }
            else
            {
                Invoke("Recycliing",5);
                gameObject.SetActive(false);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isBreak)
        {
            gameObject.GetComponent<AudioSource>().Play();
            isBreak = true;
            nowTime = Time.time;
        }
    }

    public void Recycliing()
    {
        gameObject.transform.position = _originPos;
        gameObject.SetActive(true);
        isBreak = false;
    }


}
