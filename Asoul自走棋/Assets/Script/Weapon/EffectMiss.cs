using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMiss : MonoBehaviour
{
    public void Miss() => ObjectPool.instance.Recycle(gameObject);
    public void Play()=>GetComponent<AudioSource>().Play();
}
