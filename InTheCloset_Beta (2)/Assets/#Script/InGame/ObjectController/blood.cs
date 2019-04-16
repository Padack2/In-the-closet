using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour {

    public float Damage;

    public AudioClip[] BloodSound = new AudioClip[2];
    private AudioSource _bloodAudio;

    private void Start()
    {
        _bloodAudio = gameObject.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("floor")){
            _bloodAudio.clip = BloodSound[Random.Range(0, 2)];
            _bloodAudio.Play();
        }
        if (collision.gameObject.tag == "Player")
        {
            DataManager.Instance.HP -= Damage;
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            gameObject.SetActive(false);
        }
    }
}
