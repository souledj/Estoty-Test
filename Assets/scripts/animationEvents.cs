using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEvents : MonoBehaviour
{
    public ParticleSystem seeds;
    public ParticleSystem Feeds;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void Seeds()
    {
        seeds.Emit(Random.Range(5, 10));
    }

    public void Feed()
    {
        Feeds.Emit(Random.Range(10, 15));
    }

    public void Scythe(int on)
    {
        scythe scythe = player.ScytheObj;

        if (on ==1)
        {
           scythe.colliders.SetActive(true);
           scythe.trail.SetActive(true);

        }
        else
        {
           scythe.trail.SetActive(false);
           scythe.colliders.SetActive(false);
        }
      
    }
}
