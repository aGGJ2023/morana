using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 10;

    public void TakeDamage()
    {
        Debug.Log("Seed is taking damage");
        hitPoints--;
        if (hitPoints <= 0)
            Die();
    }

    private void Die()
    {
        // TODO DEATH LOGIC
        Debug.Log("Death");
    }
}
