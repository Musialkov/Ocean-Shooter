using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotateToNearestEnemy : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float speed = 1f;
    [SerializeField] float time = 1f;
    [SerializeField] Transform startTransform;
    Enemy nearestEnemy;
    PlayerScript player;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        nearestEnemy = player.NearestEnemy();
        if(player.state == PlayerScript.State.alive)
           RotateToTheEnemy();               
    }
    private void RotateToTheEnemy()
    {
        Vector3 lookVector = nearestEnemy.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        Quaternion rot2 = Quaternion.LookRotation(-startTransform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.time * speed);      
    }
    IEnumerator SlerpRot(Quaternion startRot, Quaternion endRot, float slerpTime) 
 {
     for(var t = 0f; t< 1; t += Time.deltaTime)
        {
            print(t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
     
 }
 
}
