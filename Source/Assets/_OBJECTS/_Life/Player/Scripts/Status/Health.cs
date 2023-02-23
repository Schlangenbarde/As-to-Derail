using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Transform spawnPoint;
    public float maxHealth = 100;
    float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void GetDamage(float damage)
    {
        Debug.LogWarning("Player Get Damage: " + damage);
        currentHealth -= damage;
        DeathCheck();
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        OverFlowCheck();
    }


    void OverFlowCheck()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void DeathCheck()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoint.position;
        GetComponent<CharacterController>().enabled = true;
    }
}
