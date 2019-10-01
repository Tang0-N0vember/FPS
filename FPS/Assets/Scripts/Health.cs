using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 2;
    public int currentHealth;

    public bool isDead = false;

    public bool playerDamaged = false;

    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;
            if(gameObject.tag == "Player")
            {
                playerDamaged = true;
                StartCoroutine(Example());
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        Destroy(gameObject, 5);
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(2f);
        playerDamaged = false;
    }
}
