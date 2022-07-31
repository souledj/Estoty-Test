using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using SaveIsEasy;



public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public Joystick joystick;
    public float MoveSpeed;
    private float MaxMoveSpeed;
    public float RotationSpeed;
    public Animator animator;
    public GameObject SeedsBag;
    public bool seeding;
    private RigBuilder rigBuilder;
    public GameObject waterCan;
    public bool watering;
    private float maxMagnitude = 1;
    public GameObject water;
    public bool scythe;
    public GameObject ScytheObj;
    public bool stop;
    public Vector3 pos;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();  
        animator = GetComponentInChildren<Animator>();
        MaxMoveSpeed = MoveSpeed;
        rigBuilder = GetComponentInChildren<RigBuilder>();
    }
    // Start is called before the first frame update
    void Start()
    {    
        if(pos!=Vector3.zero)
        {
            transform.position = pos;
        }
       
       
        FindObjectOfType<camera_controller>().transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        animator.transform.localPosition = Vector3.zero;
        if(!stop)
        {
            Vector3 vector = Vector3.ClampMagnitude(joystick.MoveVector, maxMagnitude);
            animator.SetBool("seeds", seeding);
            animator.SetBool("scythe", scythe);
            SeedsBag.SetActive(seeding);
            if (seeding ^ watering)
            {
                MoveSpeed = MaxMoveSpeed * 0.5f;
                if (watering)
                {
                    water.SetActive(true);
                    maxMagnitude = 0.5f;
                }
            }
            else if (scythe)
            {
                MoveSpeed = MaxMoveSpeed * 0.4f;
            }
            else
            {

                water.SetActive(false);
                rigBuilder.enabled = false;
                waterCan.SetActive(false);
                MoveSpeed = MaxMoveSpeed;
                maxMagnitude = 1;
            }

            characterController.SimpleMove(vector * MoveSpeed);
            animator.SetFloat("Blend", vector.magnitude, 0.05f, Time.deltaTime);
            if (joystick.MoveVector != Vector3.zero)
            {
                transform.forward = Vector3.Lerp(transform.forward, joystick.MoveVector, Time.deltaTime * RotationSpeed);
            }
        }
      

    }
    public void Scythe(bool On)
    {
        if(On)
        {
            scythe = true;
            ScytheObj.SetActive(true);
        }
        else
        {
            scythe = false;
            ScytheObj.SetActive(false);
        }
    }

    public void Watering(bool On)
    {
        if(On)
        {
            watering = true;
            rigBuilder.enabled = true;
            waterCan.SetActive(true);
        }
        else
        {           
            rigBuilder.enabled = false;
            waterCan.SetActive(false);
            watering = false;
            water.SetActive(false);
        }               
    }

    public void Normalize()
    {
        Watering(false);
        Scythe(false);
        seeding = false;
    }

    public void Seeds()
    {       
        seeding = true;
    }
}
