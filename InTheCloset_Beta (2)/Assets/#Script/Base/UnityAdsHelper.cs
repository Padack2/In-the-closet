using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAdsHelper : MonoBehaviour {

    private string gameId = "2707197";
    public string placementId = "rewardedVideo";
    string kind;

    // Use this for initialization
    void Start () {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true);
        }
	}
	
	// Update is called once per frame
	public void ShowAd(string kind)
    {
        this.kind = kind;

        if (Advertisement.IsReady(placementId))
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(placementId, options);
        }
        
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            if(kind == "Wish")
            {
                gameObject.GetComponent<ItemButton>().GetAdsReward();
            }
            
        }
        else if (result == ShowResult.Skipped)
        {

        }
        else if (result == ShowResult.Failed)
        {

        }
    }
}
