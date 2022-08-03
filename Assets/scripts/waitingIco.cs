using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waitingIco : MonoBehaviour
{
    public float time;
    public Image image;
    private Transform player;
    private RectTransform rectTransform;
    private Camera mainCamera;
    public float offset;
    public GameObject coin;
    private RectTransform JumpCoin;
    private float JumpTime;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();  
        player = FindObjectOfType<Player>().transform;
        mainCamera = Camera.main;
        JumpCoin = coin.GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 target = mainCamera.WorldToScreenPoint(new Vector3(player.position.x, player.position.y+offset, player.position.z));
        rectTransform.position = Vector3.Lerp(rectTransform.position, target , Time.deltaTime * 5);
    }

    public void Move(float timer)
    {
        image.fillAmount = 0;
        JumpCoin.position = transform.position;
        time = Mathf.Clamp(timer, 0.03f, 10f);
        StartCoroutine(DoMove());
    }

    public void Off()
    {
        StopAllCoroutines();
        coin.SetActive(false);  
        gameObject.SetActive(false);
    }

    private IEnumerator DoMove()
    {
        float targetPosition = 1;
        float startPosition = 0;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            image.fillAmount = Mathf.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }        
    }

    public void CoinJump(float WaitTime)
    {
        JumpTime = WaitTime;
        JumpCoin.position = transform.position;
        JumpCoin.gameObject.SetActive(true);
        StartCoroutine(StartJump());
    }

    private IEnumerator StartJump()
    {
        Vector3 targetPosition = new Vector3(JumpCoin.position.x, JumpCoin.position.y + 100, JumpCoin.position.z);
        Vector3 startPosition = rectTransform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / JumpTime);
            JumpCoin.position = Vector3.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }
}
