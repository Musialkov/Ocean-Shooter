using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector3 startingPosition;
   
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        float rawSinWave = Mathf.Sin(cycles * Mathf.PI * 2);

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
