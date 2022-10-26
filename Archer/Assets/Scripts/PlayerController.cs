using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[System.NonSerialized]
    public int healt;
    [SerializeField]
    int currentHealt;

    public int healtRecovery;
    float recoveryTime = 2f;

    Attribiutes atr;

    public GameObject gameOverPanel;

    //RewardedAds rewardedAds;

    private void Awake()
    {
        atr = transform.GetComponent<Attribiutes>();
        //rewardedAds = GameObject.FindObjectOfType<RewardedAds>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healt = this.atr._healt + this.atr.healtLevel * 10;
        currentHealt = healt;
        TakeDamage(0);
        gameOverPanel.SetActive(false);
    }
    private void Update()
    {
        if (currentHealt < healt)
        {
            recoveryTime -= Time.deltaTime;
            if (recoveryTime <= 0)
            {
                currentHealt += healtRecovery;
                if (currentHealt > healt)
                {
                    currentHealt = healt;
                }
                recoveryTime = 2;
                TakeDamage(0);
            }
        }
    }

    public void TakeDamage(int value)
    {
        currentHealt -= value;

        float percent = (float)(currentHealt / (float)healt);

        atr.healtBar.transform.localScale = new Vector3(percent,1,1);
        atr.healtBar.transform.parent.transform.GetChild(1).GetComponent<Text>().text = currentHealt.ToString() + " / " + healt.ToString();
        
        if (currentHealt <= 0)
        {
            Time.timeScale = 0;
            RewardedAds.rewardCount = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        gameOverPanel.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
    }
}
