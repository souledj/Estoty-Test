using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class seller : MonoBehaviour
{
    public List<string> products;   
    public List<float> prices;
    public Transform canvas;
    public List<string> productsToBuy;
    public Transform coins;
    public int CoinsAmount;
    public GameObject CoinPrefab;
    int coinsToSpawn;
    public int moneyGross;
    public Transform productsRoot;

    void Awake()
    {
       for (int i = 0; i < CoinsAmount; i++)
        {
           var coin = Instantiate(CoinPrefab, coins.position, Quaternion.identity,coins);
           coin.SetActive(false);
        }

        FindObjectOfType<Player>().moneyUpgrade += PricesUp;
        if (PlayerPrefs.HasKey("money level"))
        {
            int level = PlayerPrefs.GetInt("money level");
            for (int i = 0; i < level; i++)
            {
                PricesUp();
            }
        }
    }

    public void PricesUp()
    {
        //int level = PlayerPrefs.GetInt("money level");
      
        for (int i = 0; i < prices.Count; i++)
        {
            float ToPlusPercent = moneyGross;
            float ToPlus = prices[i] * ToPlusPercent / 100;
            float newPrice = (float)Math.Round(prices[i] + ToPlus, 1);
            prices[i] = newPrice;
        }
       

    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < productsRoot.childCount; i++)
        {
            TextMeshProUGUI text = productsRoot.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            text.text = prices[i].ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 6)
        {
            foreach (var productname in products)
            {
                int amount = PlayerPrefs.GetInt(productname);
                if (amount > 0)
                {
                    productsToBuy.Add(productname);                                  
                }
            }
            if(productsToBuy.Count>0)
            {
                foreach(var productName in productsToBuy)
                {
                    int index = products.IndexOf(productName);
                    float price = prices[index];
                    float amount = PlayerPrefs.GetInt(productName);
                    float money = PlayerPrefs.GetFloat("money");
                    PlayerPrefs.SetFloat("money",money + (amount * price));                    
                    PlayerPrefs.SetInt(productName, 0);
                    coinsToSpawn =(int) Mathf.Clamp(amount * price, 0, coins.childCount);

                }
                for (int i = 0; i < coinsToSpawn; i++)
                {
                    coin coin = coins.GetChild(i).GetComponent<coin>();
                    coin.gameObject.SetActive(true);
                }
            }
            else
            {
                canvas.gameObject.SetActive(true);               
            }
            productsToBuy.Clear();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.gameObject.SetActive(false);
    }
}
