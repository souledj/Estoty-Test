using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class animalFidder : MonoBehaviour
{
    private paddock paddock;
    public Image cultureIco;
    public Sprite culture;
    private string cultureName;
    public GameObject Offer;
    public Transform feed;
    private bool feedUp;
    private Animator animator;
    public float FeedingTime;
    public Image OfferFromIco;
    public Image OfferToIco;
    private TextMeshProUGUI OfferFromText;
    private TextMeshProUGUI OfferToText;
    public int OfferFrom;
    public int OfferTo;
    public GameObject PrefabTo;
    public Transform produscts;
    public float maxf;
    public float minf;
    private Transform player;
    public float offset;
    private bool FeedNow;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        cultureName = OfferFromIco.GetComponent<Image>().sprite.name;
        animator = GetComponentInChildren<Animator>();
        OfferFromText = OfferFromIco.transform.GetComponentInChildren<TextMeshProUGUI>();
        OfferToText = OfferToIco.transform.GetComponentInChildren<TextMeshProUGUI>();
        OfferFromText.text = OfferFrom.ToString();
        OfferToText.text = OfferTo.ToString();
        PlayerPrefs.SetInt(cultureName, 120);
        for (int i = 0; i < OfferTo; i++)
        {
            var item = Instantiate(PrefabTo, produscts.position, Quaternion.identity, produscts);
            item.GetComponent<fetus>().parent = produscts;  
            int textLenght = item.name.Length;
            string newname = item.name.Substring(0, textLenght - 7);
            item.name = newname;    
            item.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(feedUp)
        {
            feed.localScale = Vector3.MoveTowards(feed.localScale, Vector3.one, Time.deltaTime * 1);
            if(feed.localScale == Vector3.one)
            {
                animator.SetBool("feed", true);
                StartCoroutine(Feeding());
                feedUp = false;
            }
        }
    }

    IEnumerator Combination()
    {
        for (int i = 0; i < produscts.childCount; i++)
        {
            var product = produscts.GetChild(i);           
            product.localPosition = Vector3.zero;            
            product.rotation = Quaternion.LookRotation(product.forward, Vector3.up);

            Rigidbody rb = product.GetComponent<Rigidbody>();
                     
            yield return new WaitForSeconds(0.01f);
            product.gameObject.SetActive(true);
            rb.AddForce((new Vector3(player.position.x + (Random.Range(-4,4)), player.position.y + offset, player.position.z + (Random.Range(-4, 4))) - produscts.position).normalized * Random.Range(minf,maxf), ForceMode.VelocityChange);
        }
        FeedNow = false;
        StopAllCoroutines();
    }


    IEnumerator Feeding()
    {
        Vector3 targetPosition = Vector3.zero;
        Vector3 startPosition = Vector3.one;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / FeedingTime);       
            feed.localScale = Vector3.Lerp(startPosition, targetPosition, fraction);
            
            if (feed.localScale.x < 0.45f)
            {
                feed.localScale = Vector3.zero;
                animator.SetBool("feed", false);
                StartCoroutine(Combination());
                StopCoroutine(Feeding());
            }
            yield return null;
        }        
       
    }

    private void OnTriggerEnter(Collider other)
    {       
        if(other.gameObject.layer == 6)
        {          
            if(!FeedNow)
            {
                int PlayerHave = PlayerPrefs.GetInt(cultureName);
                if (PlayerHave > OfferFrom)
                {
                    feedUp = true;
                    FeedNow = true;
                    PlayerPrefs.SetInt(cultureName, PlayerHave - OfferFrom);
                }
                else
                {
                    Offer.SetActive(true);
                }
            }
                      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Offer.SetActive(false);

        }
    }
}
