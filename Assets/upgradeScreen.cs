using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class upgradeScreen : MonoBehaviour
{
    private Animator animator;
    public barn barn;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public event Action CloseBarn;
    public void Close()
    {
        CloseBarn?.Invoke();
        animator.SetBool("open",false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
