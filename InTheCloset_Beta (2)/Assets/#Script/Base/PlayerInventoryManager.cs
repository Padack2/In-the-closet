using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour {

	public int FairyMagic
    {
        get
        {
            return PlayerPrefs.GetInt("FairyMagic");
        }
        set
        {
            PlayerPrefs.SetInt("FairyMagic", value);
        }
    }

    public int SubBattery
    {
        get
        {
            return PlayerPrefs.GetInt("SubBattery");
        }
        set
        {
            PlayerPrefs.SetInt("SubBattery", value);
        }
    }

    public int BreakMusicBox
    {
        get
        {
            return PlayerPrefs.GetInt("BreakMusicBox");
        }
        set
        {
            PlayerPrefs.SetInt("BreakMusicBox", value);
        }
    }

    public int Bangle
    {
        get
        {
            return PlayerPrefs.GetInt("Bangle");
        }
        set
        {
            PlayerPrefs.SetInt("Bangle", value);
        }
    }

    public int Lens
    {
        get
        {
            return PlayerPrefs.GetInt("Lens");
        }
        set
        {
            PlayerPrefs.SetInt("Lens", value);
        }
    }

    private void Awake()
    {
        FairyMagic = PlayerPrefs.GetInt("FairyMagic");
        SubBattery = PlayerPrefs.GetInt("SubBattery");
        BreakMusicBox = PlayerPrefs.GetInt("BreakMusicBox");
        Bangle = PlayerPrefs.GetInt("Bangle");
        Lens = PlayerPrefs.GetInt("Lens");
    }
}
