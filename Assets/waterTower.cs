using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTower : MonoBehaviour
{
    private Player player;
    private wateringCan wateringCan;
    public float waterSpeed;
    canvasManager canvasManager;
    Camera mainCamera;
    public GameObject canvas;
    

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
        canvasManager = FindObjectOfType<canvasManager>();
        mainCamera = Camera.main;
        player.WaterIsLoss += IcoOn;
    }

    public void IcoOn()
    {
        canvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
          
            wateringCan = player.waterCan.GetComponent<wateringCan>();
            
            if(wateringCan.waterVolume < wateringCan.maxWaterVolume)
            {
                
                var ico =canvasManager.waterIco;
                ico.gameObject.SetActive(true);
                StartCoroutine(Wateringup(wateringCan.waterVolume));
                float startIco = ((wateringCan.waterVolume * 100) / wateringCan.maxWaterVolume) / 100;
                StartCoroutine(WateringupIco(startIco));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            RectTransform waterIco = canvasManager.waterIco;
            Vector3 target = mainCamera.WorldToScreenPoint(new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z));
            waterIco.position = Vector3.Lerp(waterIco.position, target, Time.deltaTime * 3);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        canvasManager.waterIco.GetComponent<imageFader>().Off();
        StopAllCoroutines();
    }



    IEnumerator Wateringup(float start)
    {
        float targetPosition = wateringCan.maxWaterVolume;
        float startPosition = start;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / waterSpeed);
            wateringCan.waterVolume = Mathf.Lerp(startPosition, targetPosition, fraction);
            if (wateringCan.waterVolume == wateringCan.maxWaterVolume)
            {
                canvas.SetActive(false);
                StopAllCoroutines();
            }
            yield return null;
        }
    }
    IEnumerator WateringupIco(float start)
    {
        float targetPosition = 1;
        float startPosition = start;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / waterSpeed);
            wateringCan.waterIco.fillAmount = Mathf.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }
}
