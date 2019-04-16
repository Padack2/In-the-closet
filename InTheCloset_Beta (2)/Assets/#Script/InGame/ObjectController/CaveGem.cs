using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaveGem : MonoBehaviour {


    public bool isLight = false;
    public bool one = true;


    public bool isOk = false;
    public bool movePossible = false;
    public bool oriCheck;

    public Light gemLight;

    public GameObject reverseObj;
    public GameObject oriObj;

    public GameObject player;
    public GameObject PanelCanvas;
    public Image darkPanel;

    public bool TimeOk=true;
    
    private Cave CV;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        PanelCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
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
                    one = false;
                    isOk = true;
                    StartCoroutine(OpenDoor());
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cave1") || col.gameObject.CompareTag("cave2"))
        {
            CV = col.gameObject.GetComponent<Cave>();
        }
    }
    IEnumerator OpenDoor()
    {
        while (gameObject.GetComponent<SpriteRenderer>().color.a < 255)
        {
            oriObj.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.05f);
            reverseObj.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.05f);
            yield return null;
        }

        StopCoroutine(OpenDoor());
    }

    
}
