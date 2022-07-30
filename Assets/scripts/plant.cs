using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public GardenBed gardenBed;
    public bool firstTime;
    public bool oneTime;


    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
        gardenBed = GetComponentInParent<GardenBed>();        
    }

    private void OnEnable()
    {
        oneTime = true;
    }

    private void Add()
    {
        if(!gardenBed.PlantsReady.Contains(transform))
        {
            gardenBed.PlantsReady.Add(transform);
            if (gardenBed.PlantsReady.Count == gardenBed.PlantsTotal)
            {
                if (gardenBed.allPlantsIsReady)
                {
                    gardenBed.allPlantsIsReady = false;
                    gardenBed.player.Normalize();
                    gardenBed.GardenBedIsReady = true;
                    gardenBed.check = true;
                }
                else
                {

                    gardenBed.allPlantsIsReady = true;
                    gardenBed.player.Normalize();
                    gardenBed.SeedsButton.GetComponent<seedsButton>().Seeds = false;
                    gardenBed.check = true;
                    gardenBed.PlantsReady.Clear();
                }
               
            }
        }
        else
        {
            Debug.Log("no");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 2);
        if(transform.localScale == Vector3.one & oneTime)
        {            
            Add();
            oneTime = false;
        }
    }
}
