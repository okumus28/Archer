using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class ReklamInterstitial : MonoBehaviour
{
    private int altin = 0;
    private RewardedAd reklamObjesi;

    public Button rewardButton;
    public Text startMoneyText;
    public Text rewardAdsText;

    void Start()
    {
        MobileAds.Initialize(reklamDurumu => { });
        YeniReklamAl(null, null);

        startMoneyText.text = "START MONEY : " + GameObject.FindObjectOfType<Attribiutes>().money.ToString();


        if (reklamObjesi.IsLoaded())
        {
            // Reklam gösterime hazýr. Reklama AdMob konsolundan atanan ödülün (Reward) ne kadar olduðunu öðren
            rewardAdsText.text = reklamObjesi.GetRewardItem().Amount * 100 * PlayerPrefs.GetInt("MAXLEVEL") + " altýn kazan!";
            //rewardButton.gameObject.SetActive(true);
        }
        else
        {
            //rewardButton.gameObject.SetActive(false);
            rewardAdsText.transform.parent.gameObject.SetActive(false);
            rewardAdsText.text = "Reklam yükleniyor...";
        }

    }

    public void YeniReklamAl(object sender, EventArgs args)
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();

        reklamObjesi = new RewardedAd("ca-app-pub-3166374920854257/6333375764");
        reklamObjesi.OnAdClosed += YeniReklamAl; // Kullanýcý reklamý kapattýktan sonra çaðrýlýr
        reklamObjesi.OnUserEarnedReward += OyuncuyuOdullendir; // Kullanýcý reklamý tamamen izledikten sonra çaðrýlýr

        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi);
    }

    private void OyuncuyuOdullendir(object sender, Reward odul)
    {
        Debug.Log("Ödül türü: " + odul.Type);
        GameObject.FindObjectOfType<Attribiutes>().money += (int)odul.Amount * 100 * PlayerPrefs.GetInt("MAXLEVEL");
        GameObject.FindObjectOfType<Attribiutes>().GetMoney();
        startMoneyText.text = "START MONEY : " + GameObject.FindObjectOfType<Attribiutes>().money.ToString();
        rewardAdsText.transform.parent.gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();
    }

    public void AddReward()
    {        
        reklamObjesi.Show();
    }
}