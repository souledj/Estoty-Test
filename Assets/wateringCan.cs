using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class wateringCan : MonoBehaviour
{
    private Player player;
    private canvasManager canvasManager;
    public Image waterIco;
    public float waterVolume;
    public float maxWaterVolume;
    private float maxWaterTime;
    private float time;
     

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
        canvasManager = FindObjectOfType<canvasManager>();
        waterIco = canvasManager.waterIco.transform.GetChild(0).GetComponent<Image>();
        maxWaterVolume = waterVolume;
        maxWaterTime = maxWaterVolume;
    }


    public void Go()
    {
        float percent = ((waterVolume * 100) / maxWaterVolume);
        time = (percent * maxWaterTime)/ 100;
        float startIco = percent / 100;
      
        StartCoroutine(WateringLoss(waterVolume));
        StartCoroutine(WateringupIco(startIco));
       
    }

    IEnumerator WateringLoss(float start)
    {
        float targetPosition = 0;
        float startPosition = start;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            waterIco.fillAmount = Mathf.Lerp(startPosition, targetPosition, fraction);
            waterVolume = Mathf.Lerp(startPosition, targetPosition, fraction);
            if (waterVolume == 0)
            {
                
                player.Watering(false);
                player.WaterLoss();
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    IEnumerator WateringupIco(float start)
    {
        float targetPosition = 0;
        float startPosition = start;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            waterIco.fillAmount = Mathf.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
      
    }
}
