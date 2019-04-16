using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{

    public float Hp_up = 10;
    public bool one = true;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (one)
            {
                DataManager.Instance.HP += Hp_up;
                one = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(eatTime());
                

            }
        }
    }

    IEnumerator eatTime()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);

    }
}
