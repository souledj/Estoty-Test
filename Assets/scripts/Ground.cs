using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    private Material sourceMaterial;
    private Color color;
    private Color colorTransparent;
    public bool onetime = true;
    private bool colorGo;
    public int stage = 0;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        sourceMaterial = renderer.material;
        material = new Material(sourceMaterial);
        color = material.color;
        colorTransparent = new Color(color.r, color.g, color.b, 0);        
    }

    private void Start()
    {
        /*/GardenBed gardenBed = GetComponentInParent<GardenBed>();       
        switch (stage)
        {           
            case 1:
                gardenBed.Gross(transform.GetSiblingIndex(), true);
                break;
            case 2:
                gardenBed.Gross(transform.GetSiblingIndex(), false);
                
                break;
        }     /*/  
    }

    public void Transparent()
    {
        if (onetime)
        {
            
            material.color = colorTransparent;
            renderer.material = material;
            colorGo = true;
            onetime = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(colorGo)
        {
            material.color = Color.Lerp(material.color, color, Time.deltaTime * 0.5f);
            renderer.material = material;
        }
      
    }
}
