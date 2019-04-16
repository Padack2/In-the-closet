using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimCtrl : MonoBehaviour {

    public GameObject DoorObj;
    public GameObject DoorBTN;
    public GameObject DoorLight;
    public GameObject ClosetObj;
    public GameObject ClosetBTN;
    public GameObject ClosetLight;
    public GameObject ChaObj;
    public GameObject ChaLight;
    public GameObject SpeakObj;
    public GameObject WindowObj;
    public GameObject WindowBtn;
    public GameObject WindowLight;
    public AudioSource door;
    public AudioSource window;

    //public GameObject WindowLight;
    
    public GameObject Shop;
    public bool IsSpeaking=false;
    public string[] speakList;
    public Text speakText;

	// Use this for initialization

    
    
    public void WindowLightOn()
    {
        WindowObj.GetComponent<Animator>().SetTrigger("WindowOn");
        WindowBtn.SetActive(true);

        ClosetLight.SetActive(false);
        ClosetBTN.SetActive(false);
        ChaLight.SetActive(false);
        DoorBTN.SetActive(false);
        DoorLight.SetActive(false);
        SpeakObj.SetActive(false);
        WindowLight.SetActive(true);

        window.Play();
        
    }
    
    public void DoorLightOn()
    {
        DoorObj.GetComponent<Animator>().SetTrigger("DoorOn");
        ClosetLight.SetActive(false);
        ClosetBTN.SetActive(false);
        ChaLight.SetActive(false);
        DoorBTN.SetActive(true);
        DoorLight.SetActive(true);
        SpeakObj.SetActive(false);
        WindowBtn.SetActive(false);
        WindowLight.SetActive(false);

        door.Play();
    }

    public void ClosetLightOn()
    {
        ClosetObj.GetComponent<Animator>().SetTrigger("ClosetOn");
        DoorBTN.SetActive(false);
        DoorLight.SetActive(false);
        ChaLight.SetActive(false);
        ClosetBTN.SetActive(true);
        ClosetLight.SetActive(true);
        SpeakObj.SetActive(false);
        WindowBtn.SetActive(false);
        WindowLight.SetActive(false);

        door.Play();
        
    }

    public void ChaLightOn()
    {
        if (!IsSpeaking)
        {
            int rotation = Random.Range(-20, 1);
            SpeakObj.transform.rotation = Quaternion.Euler(0, 0, rotation);
            ChaObj.GetComponent<Animator>().SetTrigger("ChaOn");
            ClosetLight.SetActive(false);
            ClosetBTN.SetActive(false);
            DoorBTN.SetActive(false);
            DoorLight.SetActive(false);
            ChaLight.SetActive(true);
            SpeakObj.SetActive(true);
            WindowBtn.SetActive(false);
            WindowLight.SetActive(false);

            //////////////////////////
            int index = Random.Range(0, speakList.Length);
            speakText.text = speakList[index];

            StartCoroutine(Speaking());
            IsSpeaking = true;
        }
    }
    
    IEnumerator Speaking()
    {
        yield return new WaitForSeconds(3f);
        SpeakObj.SetActive(false);
        ChaLight.SetActive(false);
        IsSpeaking = false;

    }

    public void DoorScene()
    {
        Shop.SetActive(true);
    }

    public void ClosetScene()
    {
        SceneMove.Instance.Move("Chapter");
    }

    public void Cancel()
    {
        DoorBTN.SetActive(false);
        DoorLight.SetActive(false);
        ClosetBTN.SetActive(false);
        ClosetLight.SetActive(false);
        SpeakObj.SetActive(false);
        WindowBtn.SetActive(false);
        WindowLight.SetActive(false);

        


}
    public void AllCancel()
    {
        DoorBTN.SetActive(false);
        DoorLight.SetActive(false);
        ClosetBTN.SetActive(false);
        ClosetLight.SetActive(false);
        SpeakObj.SetActive(false);
        ChaLight.SetActive(false);
        WindowBtn.SetActive(false);
        WindowLight.SetActive(false);


    }
}
