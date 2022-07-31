using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scythe : MonoBehaviour
{
    private ParticleSystem FX;
    public GameObject colliders;
    public GameObject trail;


    // Start is called before the first frame update
    void Awake()
    {
        FX = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.SendMessage("Hit");
            Color color = other.GetComponent<culture>().colorForScythe;
            ParticleSystem.MainModule main = FX.main;
            main.startColor = color;
            FX.Emit(Random.Range(45, 55));
        }
    }
}
