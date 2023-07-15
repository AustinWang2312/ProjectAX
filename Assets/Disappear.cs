using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    private float duration;


    public void Initialize(float duration)
    {
        this.duration = duration;
        Destroy(gameObject, duration);
    }
}
