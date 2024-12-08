using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;

    // Dla ataku
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Dla podnoszenia przedmiotów
    public float pickupRange = 1f;
    public LayerMask itemLayers;
    [SerializeField]
    private Transform grabPoint;

    private Transform heldItem; // Obiekt aktualnie trzymany

    void Update()
    {
        // Atak
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        // Podnoszenie/opuszczanie przedmiotów
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (heldItem == null)
            {
                PickupItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void PickupItem()
    {
        // Wyszukaj przedmioty w zasięgu
        Collider2D[] itemsInRange = Physics2D.OverlapCircleAll(transform.position, pickupRange, itemLayers);

        foreach (Collider2D item in itemsInRange)
        {
            heldItem = item.transform; // Przypisz podniesiony przedmiot
            heldItem.position = grabPoint.position;
            heldItem.SetParent(transform);

            Debug.Log($"Podniesiono: {item.name}");

            // Opcjonalnie wyłącz collider przedmiotu
            item.GetComponent<Collider2D>().enabled = false;

            break; // Zatrzymaj po znalezieniu jednego przedmiotu
        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.SetParent(null); // Odłącz przedmiot od gracza
            heldItem.position = transform.position + transform.forward; // Ustaw pozycję przed graczem
            heldItem.GetComponent<Collider2D>().enabled = true; // Włącz collider przedmiotu
            Debug.Log($"Upuszczono: {heldItem.name}");

            heldItem = null; // Wyzeruj referencję do trzymanego przedmiotu
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
