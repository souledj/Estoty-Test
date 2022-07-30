using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    public Sprite culture;
    public RectTransform SeedsButton;
    private Camera mainCamera;
    public Player player;
    public Transform LandsRoot;
    public List<Transform> Cultures = new List<Transform>();
    public GameObject Plant;
    public List<Transform> PlantsReady = new List<Transform>();
    //private int PlantsIsReady;
    public int PlantsTotal;
    public bool allPlantsIsReady;
    public Sprite waterCan;
    public bool GardenBedIsReady;
    private int cultureIsReady;
    public bool check;
    public Sprite ScytheIco;
    private seedsButton seedsButtonComp;

    // Start is called before the first frame update
    void Awake()
    {
        string path = "Cultures/" + culture.name + " seedling";
        GameObject CultureToLoad = Resources.Load(path) as GameObject;
        PlantsTotal = LandsRoot.childCount;

        SeedsButton = FindObjectOfType<canvasManager>().SeedsButton;
        seedsButtonComp = SeedsButton.GetComponent<seedsButton>();
        mainCamera = Camera.main;
        player = FindObjectOfType<Player>();

        for (int i = 0; i < LandsRoot.childCount; i++)
        {
            Transform plant = Instantiate(Plant, LandsRoot.GetChild(i).transform.position, Quaternion.identity, LandsRoot.GetChild(i).transform).transform;
            plant.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            plant.gameObject.name = i.ToString();
            plant.gameObject.SetActive(false);
           // plant.localScale = Vector3.zero;
        }

        for (int i = 0; i < LandsRoot.childCount; i++)
        {
            Transform culture = Instantiate(CultureToLoad, LandsRoot.GetChild(i).transform.position, Quaternion.identity, LandsRoot.GetChild(i).transform).transform;
            Cultures.Add(culture);
            culture.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            culture.gameObject.SetActive(false);
            culture.gameObject.name = i.ToString();
            //culture.localScale = Vector3.zero;
        }

    }
    

    public void Gross(int Land, bool plant)
    {
        Transform TargetLand = LandsRoot.GetChild(Land);
        int i;

        if (plant)
        {
            i = 1;
            TargetLand.GetChild(i).gameObject.SetActive(true);                   
        }
        else
        {
            i = 2;
            TargetLand.GetChild(0).gameObject.SetActive(true);
            TargetLand.GetChild(0).GetComponent<wetGround>().Transparent();

            TargetLand.GetChild(1).localScale = Vector3.zero;
            TargetLand.GetChild(1).gameObject.SetActive(false);
                     
            TargetLand.GetChild(i).gameObject.SetActive(true);

        }
       // TargetLand.GetChild(i).GetComponent<plant>().Add();
    }

    // Update is called once per frame
    void Update()
    {
              
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 & !player.seeding)
        {
            SeedButtonActivating();                                
        }
    }
    void SeedButtonActivating()
    {
        if (!GardenBedIsReady)
        {
           
            if (allPlantsIsReady)
            {
               PlantsReady.Clear();
               seedsButtonComp.Watering = true;
               seedsButtonComp.image.sprite = waterCan;               
            }
            else
            {
                seedsButtonComp.Seeds = true;
                seedsButtonComp.image.sprite = culture;
            }          
        }
        else
        {
            seedsButtonComp.Scythe = true;
            seedsButtonComp.image.sprite = ScytheIco;
        }
        SeedsButton.gameObject.SetActive(true);
        SeedsButton.position = mainCamera.WorldToScreenPoint(transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if(check)
        {
            SeedButtonActivating();
            check = false;
        }

        if (other.gameObject.layer == 6)
        {           
            SeedsButton.position = Vector3.Lerp(SeedsButton.position, mainCamera.WorldToScreenPoint(transform.position), Time.deltaTime * 5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            player.Normalize();
            seedsButtonComp.Off();
        }
    }

    public void Reload()
    {        
        allPlantsIsReady = false;
        GardenBedIsReady = false;
        player.Normalize();
        check = true;
    }


}
