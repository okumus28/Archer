using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attribiutes : MonoBehaviour
{
    [Header("CURRENT ATR")]
    public int _damage;
    public float _speed;
    public int _multiArrow;
    public int _healt;

    [Header("LEVEL")]
    public Slider expSlider;
    public Text levelText;
    public int level;
    public int currentExp;
    public int levelUpExp;

    [Header("MONEY")]
    public Text moneyText;
    public int money = 0;

    [Header("BARS")]
    public Image healtBar;
    public Image manaBar;

    [Header("ATR LEVELS")]
    public int damageLevel;
    [Range(1,19)]
    public int speedLevel;
    [Range(1,11)]
    public int multiArrowLevel;
    public int healtLevel;    

    [Header("UI OBJECTS")]
    public Transform damage;
    public Transform speed;
    public Transform multiArrow;
    public Transform healt;    

    int damagePrice = 100;
    int speedPrice = 120;
    int multiArrowPrice = 5000;
    int healtPrice = 500;

    WeaponControl weaponControl;
    PlayerController playerController;
    EventSystem eventSystem;

    void Awake()
    {
        SetExpSlider(levelUpExp);
        GetLevel();
        
        
        weaponControl = GetComponent<WeaponControl>();
        playerController = GetComponent<PlayerController>();

        eventSystem = GameObject.FindObjectOfType<EventSystem>();

        _damage = 50;
        _speed = 1;
        _multiArrow = 1;
        _healt = 100;

        DamageUpgrade(false);
        SpeedUpgrade(false);
        MultiUpgrade(false);
        HealtUpgrade(false);

        //PlayerPrefs.SetInt("MAXLEVEL" , 1);

        Debug.Log( "MAX LEVEL : " + PlayerPrefs.GetInt("MAXLEVEL"));

        money += (int)Mathf.Pow(2 , PlayerPrefs.GetInt("MAXLEVEL") - 1) * 500;

        GetMoney();
        GetHealt();
    }

    private void Update()
    {
        Control(damage, damagePrice);
        Control(speed, speedPrice);
        Control(multiArrow, multiArrowPrice);
        Control(healt, healtPrice);
    }

    void Control(Transform transform , int price)
    {
        if (money >= price)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            eventSystem.canUpgrade = true;
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            eventSystem.canUpgrade = false;
        }
    }

    #region ATR LEVEL FUNC
    public void DamageUpgrade(bool ctrl)
    {
        if (ctrl)
        {
            damageLevel++;
            money -= damagePrice;
            damagePrice *= 2;
            GetMoney();
            GetDamage();
        }

        damage.GetChild(3).GetComponent<Text>().text = damageLevel.ToString();
        damage.GetChild(2).GetComponent<Text>().text = "Current : " + _damage.ToString() + "\n" + "Price : " + damagePrice.ToString();
        damage.GetChild(1).GetChild(0).GetComponent<Text>().text = _damage.ToString() + "\n" + (_damage + 4).ToString() + "\n" + damagePrice;
    }
    public void GetDamage()
    {
        _damage = 50 + 5 * damageLevel;
    }   

    public void SpeedUpgrade(bool ctrl)
    {
        if (ctrl)
        {
            speedLevel++;
            money -= speedPrice;
            speedPrice *= 2;
            GetMoney();
            GetSpeed();
        }
        speed.GetChild(3).GetComponent<Text>().text = speedLevel.ToString();
        speed.GetChild(2).GetComponent<Text>().text = "Current : " + _speed.ToString() + "\n" + "Price : " + speedPrice.ToString();
        speed.GetChild(1).GetChild(0).GetComponent<Text>().text = _speed.ToString() + "\n" + (_speed + 4).ToString() + "\n" + speedPrice;

    }
    public void GetSpeed()
    {
        _speed = 1 + 0.05f * speedLevel;
    }

    public void MultiUpgrade(bool ctrl)
    {
        if (ctrl)
        {
            multiArrowLevel++;
            money -= multiArrowPrice;
            multiArrowPrice *= 2;
            GetMoney();
            GetMultiArrow();
        }
        multiArrow.GetChild(3).GetComponent<Text>().text = multiArrowLevel.ToString();
        multiArrow.GetChild(2).GetComponent<Text>().text = "Current : " + _multiArrow.ToString() + "\n" + "Price : " + multiArrowPrice.ToString();
        multiArrow.GetChild(1).GetChild(0).GetComponent<Text>().text = _multiArrow.ToString() + "\n" + (_multiArrow + 4).ToString() + "\n" + multiArrowPrice;
    }
    public void GetMultiArrow()
    {
        _multiArrow = multiArrowLevel;
    }

    public void HealtUpgrade(bool ctrl)
    {
        if (ctrl)
        {
            healtLevel++;
            money -= healtPrice;
            healtPrice *= 2;
            GetMoney();
            GetHealt();
            playerController.healt = _healt;
            playerController.healtRecovery += 2;
            playerController.TakeDamage(0);
        }
        healt.GetChild(3).GetComponent<Text>().text = healtLevel.ToString();
        healt.GetChild(2).GetComponent<Text>().text = "Current : " + _healt.ToString() + "\n" + "Price : " + healtPrice.ToString();
        healt.GetChild(1).GetChild(0).GetComponent<Text>().text = _healt.ToString() + "\n" + (_healt + 4).ToString() + "\n" + healtPrice;
    }
    public void GetHealt()
    {
        _healt = 100 + 10 * healtLevel * healtLevel;
    }
    #endregion
    #region MONEY
    public void GetMoney(int gold)
    {        
        money += gold;
        moneyText.text = money.ToString();
    }
    public void GetMoney()
    {
        moneyText.text = money.ToString();
    }
    #endregion

    #region LEVEL & EXP
    public void GetLevel()
    {
        levelText.text = "Lvl " + level.ToString();
        expSlider.value = currentExp;
    }
    public void SetExpSlider(int max)
    {
        expSlider.maxValue = max;
    }
    public void GetExp(int exp)
    {
        currentExp += exp;

        if (currentExp >= levelUpExp)
        {
            currentExp = currentExp - levelUpExp;
            money += level * 1000;
            level++;
            if (level > PlayerPrefs.GetInt("MAXLEVEL"))
            {
                PlayerPrefs.SetInt("MAXLEVEL", level);
            }
            GameObject.FindObjectOfType<Spawner>().SpawnerEnemies(level);
            levelUpExp = (int)((float)levelUpExp * 2.5f);
            SetExpSlider(levelUpExp);
        }
        GetLevel();

    }
    #endregion
}
