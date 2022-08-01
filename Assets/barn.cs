using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barn : MonoBehaviour
{
    private Animator animator;
    private canvasManager canvasManager;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        canvasManager = FindObjectOfType<canvasManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void doorsOpen()
    {
        if(animator.GetBool("open"))
        {
            canvasManager.UpgradeScreen.SetBool("open", true);
        }
    }
    public void doorsClose()
    {
        if (!animator.GetBool("open"))
        {
            canvasManager.UpgradeScreen.SetBool("open", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            animator.SetBool("open", true);
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            animator.SetBool("open", false);
        }
    }
}
