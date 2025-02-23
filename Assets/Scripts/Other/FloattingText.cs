using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloattingText : MonoBehaviour
{
    private float destroyTime = 0.333f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }


    void Update()
    {
        
    }
}
