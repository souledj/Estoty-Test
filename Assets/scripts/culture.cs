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
      
    }

    public void Hit()
    {
        if(transform.localScale == Vector3.one)
        {
            Ground ground = transform.GetComponentInParent<Ground>();
            ground.onetime = true;
            ground.Transparent();
            ground.transform.GetChild(0).gameObject.SetActive(false);
            ground.stage = 0;
            Fetus.transform.localScale = Vector3.one*10;
            Fetus.transform.parent = null;
            Fetus.SetActive(true);
            Fetus.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            transform.localScale = Vector3.zero;
            gardenBed.CulturesReady.Remove(transform);
            if(gardenBed.CulturesReady.Count == 0)
            {
                gardenBed.Reload();
            }          
            gameObject.SetActive(false);
        }
       
    }

}
