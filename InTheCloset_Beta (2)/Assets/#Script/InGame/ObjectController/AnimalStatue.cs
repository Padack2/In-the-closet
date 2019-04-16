using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStatue : MonoBehaviour {

    private bool isCloser = false;
    public float scale = 5;
    public float time = 3;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log(DataManager.Instance.fear);
            isCloser = true;
            StartCoroutine(DamageDelay());
            //애니메이션 재생
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isCloser = false;
            StopCoroutine(DamageDelay());
        }
    }

    IEnumerator DamageDelay()
    {
        while (isCloser)
        {
            DataManager.Instance.fear -= scale;   //지속적인 감소
                                                 //애니메이션 재생

            //Debug.Log(DataManager.Instance.fear);
            yield return new WaitForSeconds(time);
        }
    }
}
