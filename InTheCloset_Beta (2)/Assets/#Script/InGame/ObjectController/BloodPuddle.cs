using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPuddle : MonoBehaviour {

    private bool isDamaging = false;
    public float damage = 10;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DataManager.Instance.HP -= damage;

            //Debug.Log(DataManager.Instance.HP);
            isDamaging = true;
            StartCoroutine(DamageDelay());
            //애니메이션 재생
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isDamaging = false;
            StopCoroutine(DamageDelay());
        }
    }

    IEnumerator DamageDelay()
    {
        while (isDamaging)
        {
            DataManager.Instance.HP -= damage;   //지속적인 피해
                                                 //애니메이션 재생

            //Debug.Log(DataManager.Instance.HP);
            yield return new WaitForSeconds(1f);
        }
    }
}
