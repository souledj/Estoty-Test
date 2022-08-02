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
        float amount;
        if (name == "money")
        {
            amount = PlayerPrefs.GetFloat(TargetPref);
        }
        else
        {
            amount = PlayerPrefs.GetInt(TargetPref);
        }
      
        
        text.text = amount.ToString();

        if (amount > 0)
        {
           target = new Vector3(open, rectTransform.localPosition.y, rectTransform.localPosition.z);
        }
        else
        {            
            target = new Vector3(close, rectTransform.localPosition.y, rectTransform.localPosition.z);
        }
       rectTransform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * 200);
    }
}
