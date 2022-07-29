using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public GardenBed gardenBed;
    public bool firstTime;


    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
        gardenBed = GetComponentInParent<GardenBed>();
    }
  


    private void OnEnable()
    {       
        if(firstTime)
        {           
            gardenBed.PlantsReady.Add(transform);
            if (gardenBed.PlantsReady.Count == gardenBed.PlantsTotal)
            {
                if (gardenBed.allPlantsIsReady)
                {                    
                    gardenBed.player.Watering(false);
                    gardenBed.GardenBedIsReady = true;
                    gardenBed.check = true;
                }
                else
                {
                    
                    gardenBed.allPlantsIsReady = true;
                    gardenBed.player.seeding = false;
                    gardenBed.SeedsButton.GetComponent<seedsButton>().Seeds = false;
                    gardenBed.check = true;
                }               
            }
        }
        else
        {
            firstTime = true;           
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 2);
    }
}
