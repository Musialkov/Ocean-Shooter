using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WalkingEnemy  : Enemy
{   
    [SerializeField] float speed = 3f;  
    [SerializeField] float minimumDistanceToStartAnimate = 20;
    [SerializeField] Vector3 force;
    [SerializeField] Vector3 force2;
    
    Rigidbody rigidbody;
    PlayerScript player;
    Animator anim;
    WalkingEnemy enemy;
    Enemy en;
    bool iKilEnemy = false;

    public enum State {alive, dead}
    public State state = State.alive;

    void Start()
    {       
        player = FindObjectOfType<PlayerScript>();
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        enemy = GetComponent<WalkingEnemy>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Math.Abs(player.gameObject.transform.position.z - transform.position.z) < minimumDistanceToStartAnimate && state == State.alive && player.state != PlayerScript.State.dead)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if(iKilEnemy == false)
        {
            Dancing();
        }
        
    }

    public override void EnemyDead()
    {       
        state = State.dead;
        anim.SetBool("isDead", true);
        int deathForce = UnityEngine.Random.Range(0, 2);      
        if(deathForce == 1)
        {
            rigidbody.AddForce(force);
        }
        else
        {
            rigidbody.AddForce(force2);
        }
        Destroy(enemy);
        base.EnemyDead();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Player":
                if(state == State.alive)
                {
                    iKilEnemy = true;                   
                    anim.SetBool("iKilThePlayer", true);
                    player.HittingObstacle();
                }               
                break;
        }
    }
    private void Dancing()
    {
        if(player.state == PlayerScript.State.dead)
        {
            anim.SetBool("playerIsDead", true);           
        }
    }
}
