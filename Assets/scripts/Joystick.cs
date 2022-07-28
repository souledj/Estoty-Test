using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Transform MoveJoystick;
    public Transform ImageJoystick;
    public float MaxLenght;
    public Vector3 MoveVector;

    // Start is called before the first frame update
    void Start()
    {
       MoveJoystick.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
        {            
            MoveJoystick.gameObject.SetActive(true);
            Vector3 click = transform.InverseTransformPoint(Input.mousePosition);
            MoveJoystick.localPosition = click;            
        } 
      
      if(Input.GetMouseButtonUp(0))
        {
            Disable();
        }
    }  

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 main = Vector3.ClampMagnitude(MoveJoystick.InverseTransformPoint(eventData.position), MaxLenght);
        ImageJoystick.localPosition = main;
        MoveVector = new Vector3(main.x, 0, main.y) / MaxLenght;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Disable();
    }

    void Disable()
    {
        MoveVector = Vector3.zero;
        ImageJoystick.localPosition = Vector3.zero;
        MoveJoystick.gameObject.SetActive(false);
    }
}
