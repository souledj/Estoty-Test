using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public float speed = 5;
    public GameObject target;
    public GameObject camera;
    private float cameraY;
    public Transform cameraPivot;

    void Start()
    {
        cameraY = camera.transform.position.y;       
    }
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,target.transform.position, speed * Time.unscaledDeltaTime);
    }

    public void shake(float y)
    {
        camera.transform.position = new Vector3(camera.transform.position.x, cameraY + y, camera.transform.position.z);
        Invoke("shakeback", 0.1f);
    }

    private void shakeback()
    {
        camera.transform.position = new Vector3(camera.transform.position.x, cameraY, camera.transform.position.z);
    }
}
