using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public GardenBed gardenBed;
    public bool firstTime;
    public bool oneTime;
    private Ground ground;
    private bool isPlant;


    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
        gardenBed = GetComponentInParent<GardenBed>();
        ground = GetComponentInParent<Ground>();
        if(gameObject.tag == "plant")
        {
            isPlant = true;
        }
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        oneTime = true;
    }

    private void Add()
    {
        if(!gardenBed.PlantsReady.Contains(transform))
        {
            if (isPlant)
            {
                gardenBed.PlantsReady.Add(transform);               
                if (gardenBed.PlantsReady.Count == gardenBed.PlantsTotal)
                {
                    gardenBed.player.Normalize();
                    gardenBed.SeedsButton.GetComponent<seedsButton>().Seeds = false;
                    gardenBed.check = true;
                    gardenBed.allPlantsIsReady = true;
                }

            }
            else
            {
                gardenBed.CulturesReady.Add(transform);                
                if (gardenBed.CulturesReady.Count == gardenBed.PlantsTotal)
                {
                    gardenBed.GardenBedIsReady = true;
                    gardenBed.player.Normalize();
                    gardenBed.check = true;
                }
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(oneTime)
        {
            Add();
            oneTime = false;
        }

        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 2);
        
    }
}
