using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageFader : MonoBehaviour
{
    public Image image;
    private Image self;
    private Color transparent = new Color(1, 1, 1, 0);
    private Color white = new Color(1, 1, 1, 1);
    private bool TakeOff;
    public float FadeSpeed;
    
    private void Awake()
    {
        self = GetComponent<Image>();        
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        TakeOff = false;
        self.color = transparent;
        image.color = transparent;     

    }

    // Update is called once per frame
    void Update()
    {
        if (!TakeOff)
        {
            self.color = Color.Lerp(self.color, white, Time.deltaTime * FadeSpeed);
            image.color = Color.Lerp(image.color, white, Time.deltaTime * FadeSpeed);
        }
        else
        {
            self.color = Color.Lerp(self.color, transparent, Time.deltaTime * FadeSpeed);
            image.color = Color.Lerp(image.color, transparent, Time.deltaTime * FadeSpeed);
            if (image.color.a <= 0.1f)
            {
                QuickOff();
            }
        }
    }


    public void Off()
    {
        TakeOff = true;
    }


    void QuickOff()
    {
        TakeOff = false;       
        gameObject.SetActive(false);
    }
}
