using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class counterPsitioner : MonoBehaviour
{
    private string TargetPref;
    private Vector3 target;
    private TextMeshProUGUI text;
    private RectTransform rectTransform;
    public float open;
    public float close;
    public bool right;
    private float offset;

    // Start is called before the first frame update
    void Awake()
    {
        TargetPref = name;               
        text = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = text.GetComponent<RectTransform>().rect.width*3;
        float amount;
        if (name == "money")
        {
            amount = Mathf.Round(PlayerPrefs.GetFloat(TargetPref));
        }
        else
        {
            amount = PlayerPrefs.GetInt(TargetPref);
        }
      
        
        text.text = amount.ToString();

        if (amount > 0)
        {
           target = new Vector3(0, rectTransform.position.y, rectTransform.position.z);
        }
        else
        {     
            if(right)
            {
                target = new Vector3(Screen.width + offset, rectTransform.position.y, rectTransform.position.z);
            }
            else
            {
                target = new Vector3(-offset, rectTransform.position.y, rectTransform.position.z);
            }
            
        }
       rectTransform.position = Vector3.MoveTowards(rectTransform.position, target, Time.deltaTime * 200);
    }
}
