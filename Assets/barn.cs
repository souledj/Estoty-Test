using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barn : MonoBehaviour
{
    private Animator animator;
    private canvasManager canvasManager;
    private Animator upgradeAnimator;
    private upgradeScreen upgradeScreen;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        canvasManager = FindObjectOfType<canvasManager>();
        upgradeAnimator = canvasManager.UpgradeScreen.GetComponent<Animator>();
        upgradeScreen = canvasManager.UpgradeScreen;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void doorsOpend()
    {
        if(animator.GetBool("open"))
        {
            upgradeAnimator.SetBool("open", true);
        }
    }
    public void doorsClosed()
    {
        if (!animator.GetBool("open"))
        {
            upgradeAnimator.SetBool("open", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            upgradeScreen.CloseBarn += CloseDoors;
            animator.SetBool("open", true);
           
        }
    }
    public void CloseDoors()
    {
        animator.SetBool("open", false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            CloseDoors();
        }
    }
}
