using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCageFairy : MonoBehaviour {

    public float moveDistance;
    public float speed;

    public float colorSpeed;

    Vector3 pos;
    bool isCheck = false;

    bool distanceCheck;
    bool colorCheck;

    private void Start()
    {
        pos = transform.localPosition;
    }

    private void Update()
    {
        if (isCheck)
        {
            if(Vector3.Distance(pos, gameObject.transform.localPosition) < moveDistance)
            {
                gameObject.transform.localPosition += new Vector3(speed * Time.deltaTime, speed * Time.deltaTime);
            }
            if (gameObject.GetComponent<SpriteRenderer>().color.a >= 0)
                gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, colorSpeed * Time.deltaTime);
            else gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isCheck)
        {
            gameObject.GetComponent<AudioSource>().Play();
            DataManager.Instance.fairy++;
            isCheck = true;
        }
    }
}
