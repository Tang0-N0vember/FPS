using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AggroDetection aggroDetection;
    private Transform target;
    public AudioSource enemySound;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {   
        if (target != null && !gameObject.GetComponent<Health>().isDead)
        {
            navMeshAgent.SetDestination(target.position);
            float currentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
            enemySound.Play();
        }
    }

}