using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEvents : MonoBehaviour
{
    public ParticleSystem seeds;   

    public void Seeds()
    {
        seeds.Emit(Random.Range(5, 10));
    }
}
