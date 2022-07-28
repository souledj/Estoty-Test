using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wetGround : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    private Material sourceMaterial;
    private Color color;
    private Color colorTransparent;
    public bool onetime = true;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        sourceMaterial = renderer.material;
        material = new Material(sourceMaterial);
        color = material.color;
        colorTransparent = new Color(color.r, color.g, color.b, 0);
        
    }

    public void Transparent()
    {
        if (onetime)
        {
            
            material.color = colorTransparent;
            renderer.material = material;
            onetime = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        material.color = Color.Lerp(material.color, color, Time.deltaTime * 0.5f);
       renderer.material = material;
    }
}
