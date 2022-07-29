using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    private Camera mainCamera;
    private RectTransform coinsCounter;
    public bool go;
    public float speed;
    public Rigidbody rb;
    public float min;
    public float max;
    public float time;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        coinsCounter = FindObjectOfType<countes>().money;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        go = false;
        float forces = Random.Range(min, max);
        float forceY = Random.Range(max*3, max*5);
        Vector3 force = new Vector3(forces, forceY, forces);
        rb.AddForce(force, ForceMode.VelocityChange);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            StartCoroutine(Go());
        }
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(time);
        rb.isKinematic = true;
        go = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(go)
        {
            Vector3 target = mainCamera.ScreenToWorldPoint(new Vector3(coinsCounter.position.x, coinsCounter.position.y, -mainCamera.transform.position.z));
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed*Random.Range(0.5f,5));
            if(Vector3.Distance(transform.position, target) < 1)
            {
                transform.position = transform.parent.position;
                go = false;
                gameObject.SetActive(false);
            }
        }
    }
}