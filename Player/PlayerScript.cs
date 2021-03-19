using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] Vector3 faithJump;
    [SerializeField] float speed = 3f;
    [SerializeField] float stopShootingTime = 1f;
    [SerializeField] GameObject particle;

    public float distanceToKilledEnemy = 1f;
    public int score = 0;
    
    Enemy[] enemyTab;
    Enemy[] enemyTab2;

    Animator animator;
    Rigidbody rigidbody;
    bool isRunning = true;
    bool isShooting = false;
    public enum State { alive, dead, shooting, finish}
    public State state = State.alive;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();                            
    }  

    void Update()
    {
        if(state == State.alive)
        {
            MovingForward();
            Jumping();           
            if(isRunning == true && isShooting == false)
            {
                Shooting();
            }            
        }        
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                if (state == State.alive)
                {
                    HittingGround();
                }
                break;
            case "Obstacle":
                HittingObstacle();
                break;
            case "Skocznia":
                FaithJump();
                break;
            case "Finish":
                Finish();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                score++;
                Destroy(other.gameObject);
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("isOnGround", false);
        isRunning = false;
    }
    public Enemy NearestEnemy()
    {
        enemyTab = FindObjectsOfType <ShootingEnemy> (); 
        enemyTab2 = FindObjectsOfType <WalkingEnemy> ();

        Enemy enemy1 = NearestEnemyFromTable(enemyTab);
        Enemy enemy2 = NearestEnemyFromTable(enemyTab2);

        if (Math.Abs(enemy1.transform.position.z - transform.position.z) > Math.Abs(enemy2.transform.position.z - transform.position.z))
            return enemy2;
        else return enemy1;
       
    }   
    private Enemy NearestEnemyFromTable(Enemy[] tab)
    {
        Enemy nearestEnemy = null;
        float distanceToNearestEnemy = Mathf.Infinity;
        foreach (Enemy enemy in tab)
        {
            float distance = Math.Abs(enemy.transform.position.z - transform.position.z);
            if (distance < distanceToNearestEnemy)
            {
                distanceToNearestEnemy = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }  
    private void Shooting()
    {
        if(CrossPlatformInputManager.GetButton("Fire1") && isRunning == true)
        {           
            isShooting = true;
            particle.SetActive(true);
            Invoke("StopShooting", stopShootingTime);           
            Enemy nearestEnemy = NearestEnemy();           
            if (Math.Abs(transform.position.z - nearestEnemy.transform.position.z) < distanceToKilledEnemy && nearestEnemy != null)
            {
                nearestEnemy.EnemyDead();
            }           
        }
    }
    private void StopShooting()
    {
        particle.SetActive(false);
        isShooting = false;
    }
    private void Finish()
    {      
        animator.SetBool("Finish", true);
        state = State.finish;
    }
    private void FaithJump()
    {
        rigidbody.AddForce(faithJump);
    }
    
    public void HittingObstacle()
    {
        state = State.dead;
        animator.SetBool("isKilledByObstacle", true);
        animator.SetBool("isOnGround", false);
    }
    private void HittingGround()
    {     
        animator.SetBool("isKilledByObstacle", false);
        animator.SetBool("isOnGround", true);
        animator.SetBool("isRunning", true);           
        isRunning = true;            
    }
    private void Jumping()
    {
       if (CrossPlatformInputManager.GetButton("Jump") && isRunning == true)
       {
           animator.SetBool("isRunning", false);
           animator.SetBool("isOnGround", false);
           rigidbody.AddForce(force);
           isRunning = false;
       }
    }
    private void MovingForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


}
