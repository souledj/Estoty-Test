using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeScreen : MonoBehaviour
{
    private Animator animator;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
        animator.SetBool("open",false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
