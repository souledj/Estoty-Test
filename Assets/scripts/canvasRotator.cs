using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasRotator : MonoBehaviour
{
    private Transform camera;   

    // Start is called before the first frame update
    void Awake()
    {       
        camera = Camera.main.transform;        
    }   

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camera.position - transform.position, Vector3.up), Time.deltaTime * 3);       
    }
}
