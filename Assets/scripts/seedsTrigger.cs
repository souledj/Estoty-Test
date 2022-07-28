using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedsTrigger : MonoBehaviour
{
    public bool plant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 & other.gameObject.tag == "Land")
        {
            GardenBed gardenBed = other.transform.parent.parent.GetComponent<GardenBed>();
            gardenBed.Gross(other.transform.GetSiblingIndex(), plant);
            
        }
    }
}
