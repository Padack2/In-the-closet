using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFactory : MonoBehaviour {

    public GameObject[] blood;
    int i;
    float nowTime;
    public float makeTime;

	// Use this for initialization
	void Start () {
        i = 0;
        nowTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - nowTime >= makeTime)
        {
            blood[i].transform.position = gameObject.transform.position;
            blood[i].SetActive(true);
            nowTime = Time.time;

            i++;
            if (i == blood.Length) i = 0;
        }
	}
}
