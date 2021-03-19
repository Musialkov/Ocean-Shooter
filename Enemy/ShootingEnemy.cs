using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingEnemy : Enemy
{
    [SerializeField] int distanceToShoot = 30;
    [SerializeField] float timeToKill = 2f;   
    [SerializeField] Vector3 force;
    float time;
    [SerializeField] GameObject particle;
    [SerializeField] UnityEngine.UI.Image image;

    ShootingEnemy enemyScript;
    PlayerScript player;
    Rigidbody rigidbody;
    Animator animator;

    enum State { alive, dead}
    State state = State.alive;
    void Start()
    {
        time = timeToKill;
        player = FindObjectOfType<PlayerScript>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemyScript = GetComponent<ShootingEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.alive)
        {
            RotateToThePlayer();
            ShootToPlayer();
        }              
    }

    private void ShootToPlayer()
    {
        if (Math.Abs(player.transform.position.z - transform.position.z) < distanceToShoot)
        {
            RedBar();
            Invoke("KillPlayer", timeToKill);
        }
    }
    public void RedBar()
    {    
         time -= Time.deltaTime;
         image.fillAmount = Mathf.Lerp(0, 1, time);                             
    }

    private void KillPlayer()
    {
        if(state == State.alive && player.state == PlayerScript.State.alive)
        {
            particle.SetActive(true);
            animator.SetBool("hitThePlayer", true);
            player.HittingObstacle();
        }
    }
    public override void EnemyDead()
    {       
        state = State.dead;
        animator.SetBool("isDead ", true);
        rigidbody.AddForce(force);
        image.fillAmount = 0;
        Destroy(enemyScript);
        base.EnemyDead();
    }

    private void RotateToThePlayer()
    {
        Vector3 lookVector = player.transform.position - transform.position;    
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }
}