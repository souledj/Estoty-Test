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
    public List<Transform> ripesApples;
    private List<Transform> notRipesApples = new List<Transform>();
    bool treeBend;
    camera_controller cameraController;
    public GameObject circle;

    // Start is called before the first frame update
    void Awake()
    {
        cameraController = FindObjectOfType<camera_controller>();
        mainCamera =cameraController.transform.GetChild(0);
        player = FindObjectOfType<Player>();
        canvasManager = FindObjectOfType<canvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ripesApples.Count>0)
        {
            circle.SetActive(true);
        }
        else
        {
            circle.SetActive(false);
        }

       if(playerIn)
        {
            bool PlayerReady;
           // Debug.Log(Vector3.Distance(player.transform.position, playerPivot.position));
            if (Vector3.Distance(player.transform.position, playerPivot.position) < 0.1f)
            {
                PlayerReady = true;
                //player.transform.forward = Vector3.MoveTowards(player.transform.forward, playerPivot.forward, Time.deltaTime * 5);
                player.animator.SetFloat("Blend", 0);
                player.animator.SetLayerWeight(0, Mathf.MoveTowards(player.animator.GetLayerWeight(0), 0, Time.deltaTime * 3));
                player.animator.SetLayerWeight(1, Mathf.MoveTowards(player.animator.GetLayerWeight(1), 1, Time.deltaTime * 3));
                
            }
            else
            {
                Vector3 vector = playerPivot.position - player.transform.position;
               // player.characterController.SimpleMove(vector.normalized * player.MoveSpeed * 0.5f);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerPivot.position, Time.deltaTime * 3);
                // player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(playerPivot.position - player.transform.position, Vector3.up), Time.deltaTime * 1);
                
                player.animator.SetFloat("Blend", vector.magnitude * 0.5f, 0.05f, Time.deltaTime);
                PlayerReady = false;
            }
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, playerPivot.rotation, Time.deltaTime * 3);
            if (PlayerReady)
            {
              
                float X;
                if(Input.GetMouseButtonDown(0))
                {
                    startClick = Input.mousePosition;
                    treeBend = true;
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
                if(treeBend)
                {
                    treePivot.rotation = Quaternion.LookRotation(player.treeShaker.forward, treePivot.up);
                }

                
            }
        }
       else
        {
            treePivot.forward = Vector3.up;
        }
    }
    private void AppleFall()
    {
        if (ripesApples.Count == 0)
        {
            StartCoroutine(Out());
        }
        else
        {
            StartCoroutine(ShakeCam(0.1f));
            var apple = ripesApples[Random.RandomRange(0, ripesApples.Count - 1)];
            apple.GetChild(0).localPosition = Vector3.zero;
            apple.GetComponent<apple>().fetus.enabled = true;
            apple.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            notRipesApples.Add(apple);
            ripesApples.Remove(apple);
        }       
        
    }

    public IEnumerator ShakeCam(float y)
    {
        camera.position = new Vector3(camera.position.x, camera.position.y + y, camera.position.z);
        yield return new WaitForSeconds(0.1f);
        camera.position = cameraPivot.position;
    }

    public IEnumerator Out()
    {
        playerIn = false;
        treeBend = false;
        mainCamera.SetPositionAndRotation(camera.position, camera.rotation);
        camera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        StartCoroutine(CameraMove(mainCamera, FindObjectOfType<camera_controller>().cameraPivot));
        player.joystick.gameObject.SetActive(true);
        player.animator.SetLayerWeight(0, 1);
        player.animator.SetLayerWeight(1, 0);
        player.animator.SetFloat("Blend", 0);
        player.stop = false;
        treePivot.GetComponent<Collider>().enabled = true;
        player.joystick.Disable();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < notRipesApples.Count; i++)
        {
            notRipesApples[i].GetComponent<apple>().StartCoroutine("Grossing", 0);
        }
        notRipesApples.Clear();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 & ripesApples.Count > 0)
        {
            treePivot.GetComponent<Collider>().enabled = false;
            camera.SetPositionAndRotation(mainCamera.position, mainCamera.rotation);
            mainCamera.gameObject.SetActive(false);
            camera.gameObject.SetActive(true);
            StartCoroutine(CameraMove(camera, cameraPivot));
            player.joystick.gameObject.SetActive(false);
            player.animator.SetFloat("Blend", 0);
            player.stop = true;
            playerIn = true;
        }
    }

    IEnumerator CameraMove(Transform start, Transform target)
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
            yield return null;
        }
    }
}
