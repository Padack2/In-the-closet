using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour { //발바닥이 바닥과 충돌할 때마다 다른 음량으로 발소리를 내는 스크립트

    AudioSource player;
    public bool ok = true;
    public float footTime;

    private void Start()
    {
        player = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("floor") && ok)
        {
            player.volume = Random.Range(0.1f, 0.6f);
            player.Play();
            ok = false;
            StartCoroutine(DelayCoroutine());

        }
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(footTime);
        
        ok = true;
    }
}
