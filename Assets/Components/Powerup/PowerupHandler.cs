using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    private float BobbingHeight = .05f;
    private float BobbingSpeed = .75f;

    private void Bob()
    {
        float yComputed = transform.position.y + BobbingHeight * (Mathf.Sin(Time.time * BobbingSpeed) * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, yComputed, transform.position.z);
    }
    
    private void Update()
    {
        Bob();
    }
}
