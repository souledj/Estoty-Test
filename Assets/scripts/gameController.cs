using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveIsEasy;


public class gameController : MonoBehaviour
{
  

    // Start is called before the first frame update
    void Awake()
    {

        //DontDestroyOnLoad(FindObjectOfType<SaveIsEasyManager>().gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
       
    }
}
