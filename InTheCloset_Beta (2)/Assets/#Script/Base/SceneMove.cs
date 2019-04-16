using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class SceneMove : MonoBehaviour {
    private static SceneMove instance;
    public static SceneMove Instance
    {
        get
        {
            if (instance == null)
                instance = new SceneMove();
            return instance;
        }
    }

    public Image FadeImage;
    private float FadeSpeed= 7.0f;

    private void Awake()
    {
        instance = this;
        StartCoroutine(FadeIn());
        
    }

    public void Move(string SceneName)
    {
        StartCoroutine(FadeOut(SceneName));
    }

    public IEnumerator FadeIn()
    {
        FadeImage.gameObject.SetActive(true);
        FadeImage.enabled = true;
        FadeImage.color = new Color(0, 0, 0, 1);
        while (FadeImage.color.a >= 0)
        {
            FadeImage.color -= new Color(0, 0, 0, FadeSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
		FadeImage.gameObject.SetActive(false);
    } 

    public IEnumerator FadeOut(string SceneName)
    {
		FadeImage.gameObject.SetActive(true);
        FadeImage.enabled = true;
        FadeImage.color = new Color(0,0, 0, 0);
        while (FadeImage.color.a <= 1)
        {
            FadeImage.color += new Color(0, 0, 0, FadeSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(SceneName);

        yield return null;
    }

    public IEnumerator FadeOut()
    {
        FadeImage.enabled = true;
        while (FadeImage.color.a <= 1)
        {
            FadeImage.color += new Color(0, 0, 0, FadeSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
