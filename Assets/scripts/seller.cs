using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seller : MonoBehaviour
{
    public List<string> products;   
    public List<int> prices;
    public Transform canvas;
    public List<string> productsToBuy;
    public Transform coins;
    public int CoinsAmount;
    public GameObject CoinPrefab;
    int coinsToSpawn;

    void Awake()
    {
       for (int i = 0; i < CoinsAmount; i++)
        {
           var coin = Instantiate(CoinPrefab, coins.position, Quaternion.identity,coins);
           coin.SetActive(false);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    int price = prices[index];
                    int amount = PlayerPrefs.GetInt(productName);
                    int money = PlayerPrefs.GetInt("money");
                    PlayerPrefs.SetInt("money",money + (amount * price));                    
                    PlayerPrefs.SetInt(productName, 0);
                    coinsToSpawn = Mathf.Clamp(amount * price, 0, coins.childCount);

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
