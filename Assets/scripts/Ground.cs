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
    public bool colorFade;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = transform.GetChild(0).GetComponent<Renderer>();
        sourceMaterial = renderer.material;
        material = new Material(sourceMaterial);
        color = material.color;
        colorTransparent = new Color(color.r, color.g, color.b, 0);        
    }


    public void Transparent()
    {
        if (stage == 1)
        {
            material.color = colorTransparent;
            renderer.material = material;
            colorGo = true;
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
        if(colorFade)
        {
            material.color = Color.Lerp(material.color, colorTransparent, Time.deltaTime * 5);
            renderer.material = material;
          
            if(material.color.a < 0.05f)
            {
                colorFade = false;
                renderer.gameObject.SetActive(false);
            }
        }
      
    }
}
