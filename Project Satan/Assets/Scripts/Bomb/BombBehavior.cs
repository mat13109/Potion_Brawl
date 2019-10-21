using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    [SerializeField] float explosionRadius = 10;
    [SerializeField] float radius = 10;
    [SerializeField] float power = 50;
    [SerializeField] float speed = .5f;

    public Rigidbody2D bombRB;

    private void Start()
    {
        Invoke("Explode", 3);

        bombRB.AddForce(transform.right * speed);
        
    }

    void Explode()
    {
        Vector3 explosionPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null && hit.gameObject != gameObject)
            {
                rb.velocity = Vector2.zero;
                try
                {
                    hit.GetComponent<PlayerBehavior>().GetStunnned(1);
                } catch
                {
                    //it's not a player
                }
                
                rb.AddExplosionForce(power, explosionPosition, radius);
            }
                
        }
        Destroy(gameObject);
    }

    
}
