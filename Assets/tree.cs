using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    public Transform camera;
    private Transform mainCamera;
    public Transform cameraPivot;
    public float cameraSpeed;
    private Player player;
    private bool playerIn;
    public Transform playerPivot;
    Vector3 startClick;
    private canvasManager canvasManager;
    private bool shake;
    private bool shakeback;
    public Transform treePivot;
    public ParticleSystem leaves;
    public Transform apples;
    private List<Transform> ripesApples = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = FindObjectOfType<camera_controller>().transform.GetChild(0);
        player = FindObjectOfType<Player>();
        canvasManager = FindObjectOfType<canvasManager>();
        for (int i = 0; i < apples.childCount; i++)
        {
            Transform apple = apples.GetChild(i);
            ripesApples.Add(apple);
            fetus fetus =apple.GetComponentInChildren<fetus>();
            fetus.parent = apple;
            fetus.enabled = false;
            apple.GetComponentInChildren<Rigidbody>().isKinematic = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
       if(playerIn)
        {
            bool PlayerReady;

            if (Vector3.Distance(player.transform.position, playerPivot.position) < 0.3f)
            {
                PlayerReady = true;
                player.transform.forward = Vector3.MoveTowards(player.transform.forward, playerPivot.forward, Time.deltaTime * 5);
                if(player.transform.forward == playerPivot.forward)
                {
                    player.animator.SetFloat("Blend", 0);
                    player.animator.SetLayerWeight(0, Mathf.MoveTowards(player.animator.GetLayerWeight(0),0, Time.deltaTime * 3));
                    player.animator.SetLayerWeight(1, Mathf.MoveTowards(player.animator.GetLayerWeight(1), 1, Time.deltaTime * 3));
                }
                
            }
            else
            {
                Vector3 vector = playerPivot.position - player.transform.position;
                player.characterController.SimpleMove(vector.normalized * player.MoveSpeed * 0.5f);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(playerPivot.position - player.transform.position, Vector3.up), Time.deltaTime * 1);
                player.animator.SetFloat("Blend", vector.magnitude * 0.5f, 0.05f, Time.deltaTime);
                PlayerReady = false;
            }

            if(PlayerReady)
            {
              
                float X;
                if(Input.GetMouseButtonDown(0))
                {
                    startClick = Input.mousePosition;
                }
                if(Input.GetMouseButton(0))
                {
                    X = (Input.mousePosition - startClick).x / Screen.width;
                    if(X>0 & !shake)
                    {
                        player.animator.SetTrigger("shake");
                        leaves.Emit(Random.Range(10, 15));

                        shake = true;
                        shakeback = false;
                        AppleFall();

                    }
                    if(X<0 & !shakeback)
                    {
                        player.animator.SetTrigger("shake back");
                        leaves.Emit(Random.Range(10, 15));
                        shake = false;
                        shakeback = true;
                        AppleFall();       
                    }
                }
                else
                {
                    shake = false;
                    shakeback = false;
                }

                treePivot.rotation = Quaternion.LookRotation(player.treeShaker.forward, treePivot.up);
            }
        }
       else
        {
            treePivot.forward = Vector3.Lerp(treePivot.forward, Vector3.up, Time.deltaTime * 3);
        }
    }
    private void AppleFall()
    {
        if (ripesApples.Count == 0)
        {
            playerIn = false;
            mainCamera.SetPositionAndRotation(camera.position, camera.rotation);
            camera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            StartCoroutine(CameraMove(mainCamera, FindObjectOfType<camera_controller>().cameraPivot, true));
        }
        else
        {
            var apple = ripesApples[Random.RandomRange(0, ripesApples.Count - 1)];
            apple.GetChild(0).localPosition = Vector3.zero;
            apple.GetComponentInChildren<fetus>().enabled = true;
            apple.GetComponentInChildren<Rigidbody>().isKinematic = false;
            ripesApples.Remove(apple);
        }
       
        
    }
    public void Out()
    {
       
        player.joystick.gameObject.SetActive(true);
        player.animator.SetLayerWeight(0, 1);
        player.animator.SetLayerWeight(1, 0);
        player.animator.SetFloat("Blend", 0);
        player.stop = false;
        
        player.joystick.Disable();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            camera.SetPositionAndRotation(mainCamera.position, mainCamera.rotation);
            mainCamera.gameObject.SetActive(false);
            camera.gameObject.SetActive(true);
            StartCoroutine(CameraMove(camera, cameraPivot, false));
            player.joystick.gameObject.SetActive(false);
            player.animator.SetFloat("Blend", 0);
            player.stop = true;
            playerIn = true;
        }
    }

    IEnumerator CameraMove(Transform start, Transform target, bool ToOut)
    {
        Transform obj = start;
        Vector3 targetPosition = target.position;
        Vector3 startPosition = start.position;
        Quaternion startRotation = start.rotation;
        Quaternion targetRotation = target.rotation;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) /cameraSpeed);
            obj.position = Vector3.Lerp(startPosition, targetPosition, fraction);
            obj.rotation = Quaternion.Slerp(startRotation, targetRotation, fraction);
            if(ToOut & fraction ==1)
            {
                Out();
            }
            yield return null;
        }
    }
}
