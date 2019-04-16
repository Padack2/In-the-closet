using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlay : MonoBehaviour {
    public string SceneKey;
    public int SceneInt;
    public int level;
    public bool IsExit;
	public bool isPrologue = false;
	
	// Use this for initialization
	void Start () {
		if (!isPrologue)
		{
			if (PlayerPrefs.GetInt("Story") < level)
				PlayerPrefs.SetInt("Story", level); //현재 씬 + 1 (스토리 1이면 2 기입)
			if (SceneKey != "")
				PlayerPrefs.SetInt(SceneKey, SceneInt); //선택지가 있는 경우였으면(스토리 3, 스토리 6) 첫번째 선택지일 경우 1, 두번째일 경우 2
		}
	}
	
	// Update is called once per frame
	void Update () {
		
        if (IsExit)
        {
	        if (isPrologue)
	        {
		        SceneMove.Instance.Move("Lobby");
	        }
	        else
	        {
		        SceneMove.Instance.Move("WishRoom"); //애니메이션 끝에서 IsExit만 체크
	        }

	        IsExit = false;
        }
	}
}
