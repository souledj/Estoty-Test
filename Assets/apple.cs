using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public fetus fetus;
    private tree tree;
    public float scaleFactor;

    // Start is called before the first frame update
    void Awake()
    {
        tree = GetComponentInParent<tree>();
        fetus = GetComponentInChildren<fetus>();
        fetus.GetComponent<Rigidbody>().isKinematic = true;
        fetus.parent = transform;
        fetus.enabled = false;
        maxSpeed *= Random.Range(1f, 3f);
        speed = maxSpeed;
       
    }
     void Start()
    {
        
        if (scaleFactor != 1)
        {
            transform.localScale = Vector3.one * scaleFactor;
            float percent = scaleFactor * 100;
            speed = maxSpeed- (maxSpeed * percent / 100);
            StartCoroutine(Grossing(scaleFactor));
        }
        else
        {
            tree.ripesApples.Add(transform);
        }
    }

    IEnumerator Grossing(float start)
    {
        fetus.GetComponent<Rigidbody>().isKinematic = true;
        fetus.enabled = false;
        fetus.gameObject.SetActive(true);
        fetus.transform.localPosition = Vector3.zero;
        float targetPosition = 1;
        float startPosition = start;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) /speed);
            transform.localScale = Vector3.Lerp(Vector3.one * startPosition, Vector3.one * targetPosition, fraction);
            scaleFactor = Mathf.Lerp(startPosition, targetPosition, fraction);
            if(transform.localScale == Vector3.one)
            {
                tree.ripesApples.Add(transform);
            }
            yield return null;
        }
    }
}
