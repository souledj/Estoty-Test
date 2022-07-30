using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEvents : MonoBehaviour
{
    public ParticleSystem seeds;
    public ParticleSystem Feeds;

    public void Seeds()
    {
        seeds.Emit(Random.Range(5, 10));
    }

    public void Feed()
    {
        Feeds.Emit(Random.Range(10, 15));
    }
}
