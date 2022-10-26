using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAds : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject rewardButton;

    private RewardedAd rewardedAd;
    [SerializeField] string rewardedAd_ID;

    public Text startMoneyText;

    public static int rewardCount = 0;

    private void Start()
    {
        startMoneyText.text = "MONEY : "+ GameObject.FindObjectOfType<Attribiutes>().money.ToString();
        MobileAds.Initialize(initStatus => { });
        RequestRewardedVideo();
        rewardButton.SetActive(false);
        Debug.Log(rewardCount);
    }

    private void Update()
    {
        if (rewardedAd.IsLoaded() && RewardedAds.rewardCount < 2)
        {
            rewardButton.SetActive(true);
            rewardButton.transform.GetChild(0).GetComponent<Text>().text = "Watch Ad \n +" + 1000 * PlayerPrefs.GetInt("MAXLEVEL") + "\nGOLD";
        }
    }
    void RequestRewardedVideo()
    {
        rewardedAd = new RewardedAd(rewardedAd_ID);
        rewardedAd.OnUserEarnedReward += HandlerUserEarnedReward;
        rewardedAd.OnAdClosed += HandlerRewardedAdClosed;
        rewardedAd.OnAdFailedToShow += HandlerRewardedFailedToShow;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void ShowRewardedVideo()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
    private void HandlerRewardedAdClosed(object sender, EventArgs e)
    {
        RequestRewardedVideo();
    }
    private void HandlerUserEarnedReward(object sender, Reward e)
    {
        GameObject.FindObjectOfType<Attribiutes>().money += 1000 * PlayerPrefs.GetInt("MAXLEVEL");
        GameObject.FindObjectOfType<Attribiutes>().GetMoney();
        startMoneyText.text = "START MONEY : " + GameObject.FindObjectOfType<Attribiutes>().money.ToString();
        RequestRewardedVideo();
        RewardedAds.rewardCount++;
        Debug.Log(rewardCount);

        rewardButton.SetActive(false);
    }

    private void HandlerRewardedFailedToShow(object sender, AdErrorEventArgs e)
    {
        RequestRewardedVideo();
    }
}
