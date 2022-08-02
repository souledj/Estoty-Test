using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countes : MonoBehaviour
{
    public RectTransform wheat;
    public RectTransform money;
    public RectTransform tomato;
    public RectTransform milk;
    public RectTransform onion;



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

            case "tomato":
                toReturn = tomato;
                break;

            case "onion":
                toReturn = onion;
                break;

        }

        return toReturn;
    }


}
