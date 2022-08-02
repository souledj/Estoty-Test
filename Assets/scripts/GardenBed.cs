using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GardenBed : MonoBehaviour
{
    public Sprite culture;
    public RectTransform SeedsButton;
    private Camera mainCamera;
    public Player player;
    public Transform LandsRoot;
    public List<Transform> CulturesReady = new List<Transform>();
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
    bool start;
    public float reloadTime;
    public float reloading;
    private bool reloadNow;
    private canvasManager canvasManager;

    // Start is called before the first frame update
    void Awake()
    {
        string path = "Cultures/" + culture.name + " seedling";
        GameObject CultureToLoad = Resources.Load(path) as GameObject;
        PlantsTotal = LandsRoot.childCount;
        canvasManager = FindObjectOfType<canvasManager>();
        SeedsButton = canvasManager.SeedsButton;
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
            culture.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            culture.gameObject.SetActive(false);
            culture.gameObject.name = i.ToString();
            //culture.localScale = Vector3.zero;
        }

    }

    IEnumerator Start()
    {
        if(GardenBedIsReady)
        {
          
            for (int i = 0; i < LandsRoot.childCount; i++)
            {
                Ground ground = LandsRoot.GetChild(i).GetComponent<Ground>();
                int stage = ground.stage;
                if (stage == 2)
                {
                    Gross(i, false);
                }
            }
            yield return null;
        }
        else
        {
           

            start = true;
            for (int i = 0; i < LandsRoot.childCount; i++)
            {
                Ground ground = LandsRoot.GetChild(i).GetComponent<Ground>();
                int stage = ground.stage;
                if (stage == 1 ^ stage == 2)
                {
                    Gross(i, true);
                }
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < LandsRoot.childCount; i++)
            {
                Ground ground = LandsRoot.GetChild(i).GetComponent<Ground>();
                int stage = ground.stage;
                if (stage == 2)
                {
                    Gross(i, false);
                }
            }
            start = false;
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
            TargetLand.GetComponent<Ground>().Transparent();

            TargetLand.GetChild(1).localScale = Vector3.zero;
            TargetLand.GetChild(1).gameObject.SetActive(false);
                     
            TargetLand.GetChild(i).gameObject.SetActive(true);

        }  
        if(!start)
        {
            TargetLand.GetComponent<Ground>().stage = i;
        }
        
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
        if(!reloadNow)
        {
            if (!GardenBedIsReady)
            {

                if (allPlantsIsReady)
                {
                    seedsButtonComp.Watering = true;
                    seedsButtonComp.Seeds = false;
                    seedsButtonComp.Scythe = false;
                    seedsButtonComp.image.sprite = waterCan;
                }
                else
                {
                    seedsButtonComp.Seeds = true;
                    seedsButtonComp.Scythe = false;
                    seedsButtonComp.Watering = false;
                    seedsButtonComp.image.sprite = culture;
                }
            }
            else
            {
                seedsButtonComp.Scythe = true;
                seedsButtonComp.Seeds = false;
                seedsButtonComp.Watering = false;
                seedsButtonComp.image.sprite = ScytheIco;
            }
            SeedsButton.gameObject.SetActive(true);
            SeedsButton.position = mainCamera.WorldToScreenPoint(transform.position);
        }
        else
        {
            canvasManager.WaitingGardenBed.gameObject.SetActive(true); 
        }
      
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
            RectTransform targetObject;
            if(reloadNow)
            {
                targetObject = canvasManager.WaitingGardenBed;
                canvasManager.WaitingGardenBed.GetChild(0).GetComponent<Image>().fillAmount = reloading;
            }
            else
            {
                targetObject = SeedsButton;
            }
           targetObject.position = Vector3.Lerp(targetObject.position, mainCamera.WorldToScreenPoint(transform.position), Time.deltaTime * 5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            player.Normalize();
            seedsButtonComp.Off();
            canvasManager.WaitingGardenBed.GetComponent<imageFader>().Off();
        }
    }

    public void Reload()
    {
        PlantsReady.Clear();
        CulturesReady.Clear();
        allPlantsIsReady = false;
        GardenBedIsReady = false;
        player.Normalize();
        reloadNow = true;
        check = true;
        StartCoroutine(Reloading(0)); 
    }

    IEnumerator Reloading(float start)
    {
        float targetPosition = 1;
        float startPosition = start;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / reloadTime);
            reloading = Mathf.Lerp(startPosition, targetPosition, fraction);
            if(reloading == targetPosition)
            {
                canvasManager.WaitingGardenBed.gameObject.SetActive(false);
                reloadNow = false;
                check = true;
            }
            yield return null;
        }
    }


}
