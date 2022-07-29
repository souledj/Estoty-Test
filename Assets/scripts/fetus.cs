using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fetus : MonoBehaviour
{
    private bool ready;
    private Camera mainCamera;
    private RectTransform TargetCounter;
    private bool oneTime = true;
    private Rigidbody rb;
    public Transform parent;


    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ready)
        {   Vector3 target = mainCamera.ScreenToWorldPoint(new Vector3(TargetCounter.position.x, TargetCounter.position.y,- mainCamera.transform.position.z));            
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 30f);                     
            if (Vector3.Distance(transform.position, target) < 1)
            {
                PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + 1);
                transform.parent = parent;
                transform.localPosition = new Vector3(0, 5, 0);
                transform.localRotation = Quaternion.identity;
                oneTime = true;
                ready = false;
                rb.isKinematic = false;
                gameObject.SetActive(false);
            }
        }
      
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8 & oneTime)
        {            
            oneTime = false;
            yield return new WaitForSeconds(0.2f);
            rb.isKinematic = true;
            TargetCounter = FindObjectOfType<countes>().Count(name);           
            ready = true;
        }
    }
}
