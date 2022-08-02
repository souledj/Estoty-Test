using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class upgrade : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;
    private string Pref;
    public Image button;
    int level;
    public float price;
    private Player player;
    public int MaxLevel;
    public float startPrice;
    public float PriceGrossingPercent;
    public GameObject maxLevelText;



    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
        Pref = name;
        PlayerPrefs.SetFloat("money", 1000);

        if (!PlayerPrefs.HasKey(Pref + " price"))
        {
            PlayerPrefs.SetFloat(Pref + " price", startPrice);
        }
       
        level = PlayerPrefs.GetInt(Pref + " level");
        price = PlayerPrefs.GetFloat(Pref + " price");
        UpdateText();
    }

    private void OnEnable()
    {
        CheckButton();
    }

    private void CheckButton()
    {
        if (PlayerPrefs.GetFloat("money") >= price & level < MaxLevel-1)
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
        if (level < MaxLevel - 1)
        {
            levelText.text = "Level " + (level + 2).ToString();
            priceText.text = price.ToString();
        }
        else
        {
            priceText.gameObject.SetActive(false);
            levelText.gameObject.SetActive(false);
            maxLevelText.SetActive(true);
        }
       

    }

    public void Upgrade()
    {
        float money = PlayerPrefs.GetFloat("money");
        if(money>=price & level < MaxLevel-1)
        {
            PlayerPrefs.SetFloat("money", money - price);
            level++;
            PlayerPrefs.SetInt(Pref + " level", level);
            price += Mathf.Round((price / 100) * PriceGrossingPercent);
            PlayerPrefs.SetFloat(Pref + " price", price);
            UpdateText();
            player.Upgrade(Pref);
        }

        upgrade[] upgrades = FindObjectsOfType<upgrade>();
        foreach (upgrade item in upgrades)
        {
            item.CheckButton();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
