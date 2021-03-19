using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    Animator animator;
    PlayerScript player;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.state == PlayerScript.State.finish)
        {
            animator.SetBool("Finish", true);
        }
    }
}
