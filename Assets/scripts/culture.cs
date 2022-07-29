using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class culture : MonoBehaviour
{
    public GameObject Fetus;
    private GardenBed gardenBed;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
        gardenBed = GetComponentInParent<GardenBed>();       
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 2);
    }

    public void Hit()
    {
        if(transform.localScale == Vector3.one)
        {
            wetGround ground = transform.parent.GetComponentInChildren<wetGround>();
            ground.onetime = true;
            ground.Transparent();
            ground.gameObject.SetActive(false);
            
            Fetus.transform.localScale = Vector3.one*10;
            Fetus.transform.parent = null;
            Fetus.SetActive(true);
            Fetus.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            transform.localScale = Vector3.zero;
            gardenBed.PlantsReady.Remove(transform);
            if(gardenBed.PlantsReady.Count == 0)
            {
                gardenBed.Reload();
            }          
            gameObject.SetActive(false);
        }
       
    }

}
