using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using SaveIsEasy;
using System;



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
    public scythe ScytheObj;
    public bool stop;
    public Vector3 pos;
    public Transform scytheGraber;
    public Transform wateringGraber;
    private canvasManager canvasManager;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        canvasManager = FindObjectOfType<canvasManager>();
        characterController = GetComponent<CharacterController>();  
        animator = GetComponentInChildren<Animator>();
        MaxMoveSpeed = MoveSpeed;
        rigBuilder = GetComponentInChildren<RigBuilder>();
        ScytheObj = ToolSpawn("scythes/scythe " + PlayerPrefs.GetInt("scythe level"), scytheGraber).GetComponent<scythe>();
        waterCan = ToolSpawn("waterings/watering can " + PlayerPrefs.GetInt("watering can level"), wateringGraber);
        water.transform.localScale = (Vector3.one * (PlayerPrefs.GetInt("watering can level") + 1)) * 0.3f;


    }
    public void Upgrade(string target)
    {
        switch (target)
        {
            case "scythe":
                Destroy(ScytheObj.gameObject);
                ScytheObj = ToolSpawn("scythes/scythe " + PlayerPrefs.GetInt("scythe level"), scytheGraber).GetComponent<scythe>();
                break;

            case "watering can":
                Destroy(waterCan);
                waterCan = ToolSpawn("waterings/watering can " + PlayerPrefs.GetInt("watering can level"), wateringGraber);
                water.transform.localScale = (Vector3.one * (PlayerPrefs.GetInt("watering can level") + 1)) * 0.3f;
                break;
        }
    }

    public event Action WaterIsLoss;

    public void WaterLoss()
    {
        WaterIsLoss?.Invoke();
    }

    public GameObject ToolSpawn(string path, Transform graber)
    {
        var ToLoad = Resources.Load(path) as GameObject;
        var Obj = Instantiate(ToLoad, graber.position, graber.rotation, graber);
        Obj.transform.localScale *= (1 / 0.35f);
        Obj.gameObject.SetActive(false);
        return Obj;

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
                    RectTransform waterIco = canvasManager.waterIco;
                    Vector3 target = mainCamera.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z));
                    waterIco.position = Vector3.Lerp(waterIco.position, target, Time.deltaTime * 3);
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
            ScytheObj.gameObject.SetActive(true);
        }
        else
        {
            scythe = false;
            ScytheObj.gameObject.SetActive(false);
        }
    }

    public void Watering(bool On)
    {
        if(On & waterCan.GetComponent<wateringCan>().waterVolume > 0)
        {
            watering = true;
            canvasManager.waterIco.gameObject.SetActive(true);
            rigBuilder.enabled = true;
            waterCan.SetActive(true);
            waterCan.GetComponent<wateringCan>().Go();
            //waterCan.GetComponent<wateringCan>().StartCoroutine("WateringLoss");
        }
        else
        {           
            rigBuilder.enabled = false;
            canvasManager.waterIco.GetComponent<imageFader>().Off();
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
