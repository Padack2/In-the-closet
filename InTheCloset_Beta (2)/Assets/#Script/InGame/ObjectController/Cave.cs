using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour {

    public GameObject Gem;
    private CaveGem CG;
    private AudioSource _caveAudio;
    public bool TimeOk = true;
    
    public AudioClip[] RockSounds = new AudioClip[3];



    private void Start()
    {
         CG = Gem.GetComponent<CaveGem>();
         _caveAudio = gameObject.GetComponent<AudioSource>();

    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("cave1") && CG.isOk)
            {
                CG.oriCheck = true;
                CG.movePossible = true;
            }else if (gameObject.CompareTag("cave2") && CG.isOk)
            {
                CG.oriCheck = false;
                CG.movePossible = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CG.movePossible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CG.movePossible = false;
        }
    }
    
    /*

    public void OnMouseDown()
    {
        if (CG.TimeOk == true)
        {
            if (CG.movePossible && CG.isOk)
            {
                StartCoroutine(TimeCheck());
                CG.TimeOk = false;
                _caveAudio.clip = RockSounds[Random.Range(0, 3)];
                _caveAudio.Play();
                StartCoroutine(darkPanelOn());
            }
        }
    }
    
    */
    
    public void _Touch()
    {
        if (CG.TimeOk == true)
        {
            if (CG.movePossible && CG.isOk)
            {
                StartCoroutine(TimeCheck());
                CG.TimeOk = false;
                _caveAudio.clip = RockSounds[Random.Range(0, 3)];
                _caveAudio.Play();
                StartCoroutine(darkPanelOn());

            }
        }
    }

    public void StartCR()
    {
        StartCoroutine(darkPanelOn());
    }

    IEnumerator darkPanelOn()
    {
        CG.PanelCanvas.SetActive(true);
        while (CG.darkPanel.color.a < 1)
        {
            CG.darkPanel.color += new Color(0, 0, 0, 0.05f);    
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        PlayerMove();

        while (CG.darkPanel.color.a > 0)
        {
            CG.darkPanel.color += new Color(0, 0, 0, -0.05f);
            yield return null;
        }
        CG.darkPanel.color=new Color(0,0,0,0);
        CG.PanelCanvas.SetActive(false);
        StopCoroutine(darkPanelOn());
    }

   
    private void PlayerMove()
    {
        if (CG.oriCheck == true && CG.isOk)
        {
            CG.player.transform.position = (Vector2)CG.reverseObj.transform.position;
            CG.player.transform.position += new Vector3(0, -2,- .1f);
            CG.oriCheck = false;

        }
        else if (CG.oriCheck == false && CG.isOk)
        {
            CG.player.transform.position = (Vector2)CG.oriObj.transform.position;
            CG.player.transform.position += new Vector3(0, -2, -.1f);
            CG.oriCheck = true;

        }

        CG.movePossible = true;
    }
    
    
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(2f);
        
        CG.TimeOk = true;        
        CG.movePossible = true;
        CG.isOk=true;
        StopCoroutine(TimeCheck());
    }

}
