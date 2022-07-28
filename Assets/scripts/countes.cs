using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countes : MonoBehaviour
{
    public RectTransform wheat;
    public RectTransform money;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public RectTransform Count(string name)
    {
        RectTransform toReturn = null;    
        switch (name)
        {
            case "wheat":
                toReturn = wheat; 
                break;
        }

        return toReturn;
    }


}
