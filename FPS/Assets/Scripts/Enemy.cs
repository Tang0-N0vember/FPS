using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float attackRefreshRate=1.5f;
    private float attackTimer;

    public bool isDead=false;
    private bool earnedMoney = false;

    private Animator animator;

    private AggroDetection aggroDetection;

    private Health healthTarget;

    public Transform enemyDirection;

    public Collider coll;
    private void Awake()
    {

        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
        animator = GetComponentInChildren<Animator>();
        enemyDirection = GetComponent<Transform>();
        coll = GetComponent<Collider>();

    }
    private void AggroDetection_OnAggro(Transform target)
    {
        Health health = target.GetComponent<Health>();
        if(health !=null)
        {
            healthTarget = health;
        }
    }

    private void Update()
    {
        isDead = gameObject.GetComponent<Health>().isDead;
        if (isDead&&!earnedMoney)
        {
            GameObject.Find("GameManager").GetComponent<Gamemanger>().credits += 100;
            GameObject.Find("GameManager").GetComponent<Gamemanger>().enemeyCounter -= 1;
            earnedMoney = true;
            
        }
        if (healthTarget!=null)
        {
            attackTimer += Time.deltaTime;
            if (CanAttack())
            {
                Attack();
            }
        }
    }

    private bool CanAttack()
    {
        return attackTimer >= attackRefreshRate;
    }

    private void Attack()
    {
        if (!gameObject.GetComponent<Health>().isDead)
        {
            Ray ray = new Ray(enemyDirection.position, transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * 3f, Color.blue, 2f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                animator.SetTrigger("Attack");
                healthTarget.TakeDamage(1);
            }
            attackTimer = 0;
        }
    }
}