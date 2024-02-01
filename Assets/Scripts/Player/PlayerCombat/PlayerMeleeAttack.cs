using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDamage = 3f;
    [SerializeField] private float attackRange = 1f;

    private float timeUntilMelee;

    private void Update()
    {
        if (timeUntilMelee > 0f)
        {
            timeUntilMelee -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && timeUntilMelee <= 0f)
        {
            MeleeAttack();
            timeUntilMelee = attackSpeed;
        }
    }

    private void MeleeAttack()
    {
        Debug.Log("Melee attack initiated");

        // Get the enemy's position
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyObject != null)
        {
            Vector2 enemyPosition = enemyObject.transform.position;

            // Vector from player to enemy
            Vector2 directionToEnemy = enemyPosition - (Vector2)transform.position;

            // Check if the player is facing the enemy (dot product > 0)
            float dotProduct = Vector2.Dot(transform.up, directionToEnemy.normalized);

            if (dotProduct > 0)
            {
                // Perform raycast towards the enemy's position
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToEnemy, attackRange);

                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 2f);

                    // Check if the hit object has an enemy tag
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        Debug.Log("Hit an object with the 'Enemy' tag.");

                        // Deal damage to the enemy
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(attackDamage);
                            Debug.Log("Dealt damage to the enemy!");
                        }
                    }
                    else
                    {
                        Debug.Log("Hit an object without the 'Enemy' tag: " + hit.collider.gameObject.name);
                    }
                }
            }
            else
            {
                Debug.Log("Player is not facing the enemy.");
            }
        }
    }





}
