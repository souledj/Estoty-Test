using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newPlace : MonoBehaviour
{
    public float waitingTime;
    public int price;
    public TextMeshProUGUI text;
    public float maxWaitInCicle;
    private float waitInCicle;
    private bool open;
    public Transform land;
    private waitingIco waitingIco;
    public List<GameObject> nextPlaces;
    private bool opened;
    public GameObject walls;
    public GameObject canvas;
    public bool firstPlace;
    bool onetime;

    // Start is called before the first frame update
    void Awake()
    {
        waitInCicle = maxWaitInCicle;
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
       //PlayerPrefs.SetFloat("money", 1000);
        waitingIco = FindObjectOfType<canvasManager>().waitingIco;
        text.text = price.ToString();
        land = transform.GetChild(1);
        land.gameObject.SetActive(false);
    }

   

    void Start()
    {
        if (price != 0)
        {
            text.text = price.ToString();
            if (!firstPlace & !onetime)
            {
                land.gameObject.SetActive(false);
                canvas.gameObject.SetActive(false);
                if(name == "new place (3)")
                {
                    //Debug.Log(name);
                }
                onetime = true;
            }
        }
        else
        {
            opened = true;
            land.gameObject.SetActive(true);
            land.localPosition = new Vector3(land.localPosition.x, -1, land.localPosition.z);
            land.localScale = new Vector3(2, 2, 2);
            walls.SetActive(false);
            foreach (GameObject go in nextPlaces)
            {
                go.GetComponent<newPlace>().Activate();
            }
        }
    }

    public void Activate()
    {
        canvas.gameObject.SetActive(true);
        //open = false;
    }
    // Update is called once per frame
    void Update()
    {
      if(open)
        {
            land.localPosition = Vector3.MoveTowards(land.localPosition, new Vector3(land.localPosition.x, -1, land.localPosition.z), Time.deltaTime * 3);
            land.localScale = Vector3.MoveTowards(land.localScale, new Vector3(2,2,2), Time.deltaTime * 3);
            if (land.localScale == new Vector3(2, 2, 2))
            {
                walls.SetActive(false);
                if(nextPlaces.Count>0)
                {
                    foreach (GameObject go in nextPlaces)
                    {
                        go.GetComponent<newPlace>().Activate();
                    }
                }
                  
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 & !open)
        {
            if(PlayerPrefs.GetFloat("money") > 0 & !opened & canvas.activeSelf)
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
        float money = PlayerPrefs.GetFloat("money");
        for (int i = 0; i < money; i++)
        {
           
           
            if (price!=0)
            {
                
                if (money != 0)
                {
                    PlayerPrefs.SetFloat("money", Mathf.Clamp(PlayerPrefs.GetFloat("money") - 1, 0, 999999));
                    price -= 1;
                    text.text = price.ToString();
                    waitingIco.CoinJump(waitInCicle);
                    waitInCicle = Mathf.Clamp(waitInCicle - 0.03f, 0.01f, maxWaitInCicle);                    
                }                               
            }            
            yield return new WaitForSeconds(waitInCicle);           
            if (PlayerPrefs.GetFloat("money") == 0)
            {
                Off();
            }

            if(price == 0)
            {

                open = true;
                land.localScale = Vector3.zero;
                Off();
                land.gameObject.SetActive(true);
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
