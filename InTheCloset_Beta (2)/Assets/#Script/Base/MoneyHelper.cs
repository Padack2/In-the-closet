using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHelper : MonoBehaviour {

    public int Money
    {
        get
        {
            return PlayerPrefs.GetInt("Money");
        }
        set
        {
            PlayerPrefs.SetInt("Money", value);
        }
    }

    public int Wish
    {
        get
        {
            return PlayerPrefs.GetInt("Wish");
        }
        set
        {
            PlayerPrefs.SetInt("Wish", value);
        }
    }

    private void Awake()
    {
        Money = PlayerPrefs.GetInt("Money");
        Wish = PlayerPrefs.GetInt("Wish");
    }
}
