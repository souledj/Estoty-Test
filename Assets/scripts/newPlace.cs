using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newPlace : MonoBehaviour
{
    public float waitingTime;
    public int price;
    private TextMeshProUGUI text;
    public float maxWaitInCicle;
    private float waitInCicle;
    private bool open;
    public Transform land;
    private waitingIco waitingIco;
    public List<GameObject> nextPlaces;

    // Start is called before the first frame update
    void Awake()
    {
        waitInCicle = maxWaitInCicle;
        text = GetComponentInChildren<TextMeshProUGUI>();
        PlayerPrefs.SetInt("money", 150);
        waitingIco = FindObjectOfType<canvasManager>().waitingIco;
        text.text = price.ToString();
        land = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
      if(open)
        {
            land.localPosition = Vector3.MoveTowards(land.localPosition, new Vector3(land.localPosition.x, -1, land.localPosition.z), Time.deltaTime * 3);
            if(land.localPosition.y == -1 & nextPlaces.Count>0)
            {
                foreach(GameObject go in nextPlaces)
                {
                    go.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 & !open)
        {
            if(PlayerPrefs.GetInt("money") > 0)
            {
                StartCoroutine(Opening());
                waitingIco.gameObject.SetActive(true);
                waitingIco.Move(waitingTime);
            }
           
        }
    }

    IEnumerator Opening()
    {
        yield return new WaitForSeconds(waitingTime);
        int money = PlayerPrefs.GetInt("money");
        for (int i = 0; i < money; i++)
        {
           
           
            if (price!=0)
            {
                
                if (money != 0)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 1);
                    price -= 1;
                    text.text = price.ToString();
                    waitingIco.CoinJump(waitInCicle);
                    waitInCicle = Mathf.Clamp(waitInCicle - 0.03f, 0.03f, maxWaitInCicle);                    
                } 
               
               
            }
            else
            {                
                open = true;
                Off();
                land.gameObject.SetActive(true);
                
            }

            yield return new WaitForSeconds(waitInCicle);           
            if (PlayerPrefs.GetInt("money") == 0)
            {
                Off();
            }
        }
        
    }

    private void Off()
    {
        waitingIco.Off();       
        StopAllCoroutines();
        waitInCicle = maxWaitInCicle;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {           
            Off();
        }
    }
}
