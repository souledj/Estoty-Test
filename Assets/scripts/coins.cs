using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private RectTransform coinsCounter;
    private Camera mainCamera;
    public float Speed;
    public float time;
    private bool go;
    private ParticleSystem.VelocityOverLifetimeModule vs;   

    // Start is called before the first frame update
    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        coinsCounter = FindObjectOfType<countes>().money;
        mainCamera = Camera.main;
        vs = particleSystem.velocityOverLifetime;      

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Emit(100));          
        }
        if(go)
        {
            Vector3 target = mainCamera.ScreenToWorldPoint(new Vector3(coinsCounter.position.x, coinsCounter.position.y, -mainCamera.transform.position.z * 3));
            Vector3 vector = (target - transform.position).normalized * Speed;
            Debug.Log(vector);
            vs.space = ParticleSystemSimulationSpace.World;

            vs.enabled = true;
            vs.x = vector.x;
            vs.y = vector.y;
            vs.z = vector.z;
        }

    }

    public IEnumerator Emit(int amount)
    {
        go = false;
        var gravity = particleSystem.main.gravityModifier;
        vs.x = 0;
        vs.y = 0;
        vs.z = 0;
        vs.enabled = false;
        gravity.constant = 2;
        particleSystem.Emit(amount);
        yield return new WaitForSeconds(time);
        gravity.constant = 0;
        go = true;
        
       
    }
}
