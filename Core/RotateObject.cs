using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 10f;
   
    void Update()
    {
        transform.Rotate(-Vector3.forward * (RotationSpeed * Time.deltaTime));
    }
}
