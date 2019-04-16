using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }
    
	// Use this for initialization
	void Awake () {
        instance = this;
        MaxHP += MaxHP * 0.01f * PlayerPrefs.GetFloat("HP");
        MaxFear += MaxFear * 0.01f * PlayerPrefs.GetFloat("Fear");      //강화값을 불러와서 적용
        HP = MaxHP;
        fear = MaxFear;
	}
    public float MaxBattery;
    public float battery;
    public float MaxHP;
    private float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            
            if(GameObject.Find("Hurt") != null && value < hp)
            {
                GameObject.Find("Hurt").GetComponent<AudioSource>().Play();     //들어온 값이 현재 hp보다 작으면 Hurt사운드 실행
            }

            hp = value;
        }
    }
    public int fairy = 0;
    public float MaxFear;
    public float fear;
    public int lamp;
    public float DelayTime;

    public bool gameOver;


}
