using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{

    public Image FadeImage;
    bool click = false;
    public GameObject PrologueBoard;
    


    public void BtnClick()
    {
        if(!click)
            if (PlayerPrefs.GetInt("OneTime")!=1)
            {
                PlayerPrefs.SetInt("OneTime",1);
                StartCoroutine(FadeOut());
                click = true;
            }
            else
            {
                SceneManager.LoadScene("Lobby");
                click = true;
            }
    }

    public void MoveLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void MovePrologue()
    {
        SceneManager.LoadScene("Prologue");
    }
    
    public IEnumerator FadeOut()
    {
        FadeImage.gameObject.SetActive(true);

        while (FadeImage.color.a <= 1)
        {
            FadeImage.color += new Color(0, 0, 0, 2 * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
        PrologueBoard.SetActive(true);

    }
}
