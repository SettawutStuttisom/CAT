using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private Animator animator;
    private bool canTakeDamage = true;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        health = maxHealth; 
    }
    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) { return; }

        health -= damage;
        animator.SetBool("Damage", true);
        Debug.Log("Player health " + health);
        if (health <= 0)
        { 
            GetComponentInParent<GatherInput>().DisableControl();
            Debug.Log("Player is dead");
        }
        StartCoroutine(DamagePrevention());
    }
    private IEnumerator DamagePrevention() { 
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);

        if (health > 0)
        {
            canTakeDamage = true;
            animator.SetBool("Damage", false); // to idle animation
        }
        else { 
            animator.SetBool("Death", true ); // to dead animation
        }
    }
}