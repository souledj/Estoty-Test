using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;
    private string Pref;
    public Image button;
    int level;
    int price;
    private Player player;
    public int MaxLevel;



    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
        Pref = name;
        PlayerPrefs.SetInt("money", 1000);

        if (!PlayerPrefs.HasKey(Pref + " price"))
        {
            PlayerPrefs.SetInt(Pref + " price", 1000);
        }
       
        level = PlayerPrefs.GetInt(Pref + " level");
        price = PlayerPrefs.GetInt(Pref + " price");
        UpdateText();
    }

    private void OnEnable()
    {
        CheckButton();
    }

    private void CheckButton()
    {
        if (PlayerPrefs.GetInt("money") >= price)
        {
            button.color = Color.green;
        }
        else
        {
            button.color = Color.white;
        }
    }

    public void UpdateText()
    {
        levelText.text = "Level " + (level+2).ToString();
        priceText.text = price.ToString();

    }

    public void Upgrade()
    {
        int money = PlayerPrefs.GetInt("money");
        if(money>=price)
        {
            PlayerPrefs.SetInt("money", money - price);
            level++;
            PlayerPrefs.SetInt(Pref + " level", Mathf.Clamp(level, 0, MaxLevel));
            price += (price / 100) * 30;
            PlayerPrefs.SetInt(Pref + " price", price);
            UpdateText();
            player.Upgrade(Pref);
        }
        CheckButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
