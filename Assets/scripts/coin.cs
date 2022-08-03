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
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        coinsCounter = FindObjectOfType<countes>().money;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
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
            //mainCamera.ScreenToWorldPoint(new Vector3(coinsCounter.position.x, coinsCounter.position.y, -mainCamera.transform.position.z));
            Vector3 target = new Vector3( player.transform.position.x, player.transform.position.y +2, player.transform.position.z); 
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed*Random.Range(1f,1.5f));
            if(Vector3.Distance(transform.position, target) < 1)
            {
                transform.position = transform.parent.position;
                go = false;
                gameObject.SetActive(false);
            }
        }
    }
}
