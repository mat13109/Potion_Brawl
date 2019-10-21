using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    float explosionRadius = 5;
    float radius = 10;
    float power = 500;

    private void Start()
    {
        Explode();
    }

    void Explode()
    {
        Vector3 explosionPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.AddExplosionForce(power, explosionPosition, radius, 1, ForceMode2D.Impulse);
        }
    }

    
}
