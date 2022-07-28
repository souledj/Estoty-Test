using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seedsButton : MonoBehaviour
{
    
    public Image image;
    private Image self;
    private Color transparent = new Color(1,1,1,0);
    private Color white = new Color(1, 1, 1, 1);
    private bool TakeOff;
    public float FadeSpeed;
    private Player player;
    public bool Seeds = true;
    public bool Watering;
    public Sprite wateringCan;
    public bool Scythe;

    private void Awake()
    {
        self = GetComponent<Image>();
        player = FindObjectOfType<Player>();    
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        TakeOff = false;
        self.color = transparent;
        image.color = transparent;

        if(player.seeding ^ player.watering)
        {
            QuickOff();
        }
        else if(player.scythe)
        {
            QuickOff();
        }
      
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
            if(image.color.a <= 0.1f)
            {
                QuickOff();
            }
        }
       
    }
    void QuickOff()
    {
        TakeOff = false;
        Watering = false;
        Seeds = false;
        Scythe = false;
        gameObject.SetActive(false);
    }

    

    public void Click()
    {
        TakeOff = true;
        if(Seeds)
        {
            player.Seeds();
            Seeds = false;
        }
        if (Watering)
        {
            player.Watering(true);
            Watering = false;
        }
        if(Scythe)
        {            
            player.Scythe(true);
        }
       
    }

    public void Off()
    {
        TakeOff = true;
    }
}
