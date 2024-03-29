﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    [SerializeField] float explosionRadius = 10;
    [SerializeField] float radius = 10;
    [SerializeField] float power = 50;
    float speed;
    private float minSpeed = 20f;
    private float maxSpeed = 60f;

    [SerializeField] GameObject rumble;

    [SerializeField] GameObject particles;
    [SerializeField] GameObject puddle = null;
    public GameObject boum;
    public GameObject bombinette;

 //   [SerializeField] float stuckDuration = 1.0f;
    private GameObject Players;
       
       

    public Rigidbody2D bombRB;
    public string type;
    float starttime;
    float alpha = 0;



    private void Update()
    {
        if (Time.time - starttime > 2.5f) { 
            alpha += 0.02f;
            if (!GameObject.Find("PauseUI").GetComponent<Animator>().GetBool("paused"))
                gameObject.transform.localScale += new Vector3(.01f, .01f, .01f);
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color =  new Color(color.r, color.g, color.b, alpha);
        }
    }


    private void Start()
    {
        starttime = Time.time;
        SetRandomSpeed();
        Cursor.visible = false;

        switch (type)
        {
            case "explosion":
                Invoke("Explode", 3); 
                break;
            case "siphon":
                Invoke("Implode", 3);
                break;
            case "humiliator":
                Invoke("Humiliation", 3);
                break;
            case "glue":
                Invoke("Glued", 3);
                break;
        }
        
        bombRB.AddForce(transform.right * speed);
        
    }

    private void Explode()
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
                if(hit.CompareTag("Bomb")) 
                    rb.AddExplosionForce(power/4, explosionPosition, radius);
                else 
                    rb.AddExplosionForce(power, explosionPosition, radius);
                }
        }
        GameObject temp = Instantiate(particles, transform.position, Quaternion.identity);
        GameObject explo = Instantiate(boum, transform.localPosition, Quaternion.identity);
        Destroy(explo, 1);
        Destroy(temp, 1);
        Instantiate(rumble);
        Destroy(gameObject);
    }

    private void Glued()
    {
        Vector3 explosionPosition = transform.position;
        GameObject gl = Instantiate(puddle, explosionPosition, Quaternion.identity);
        Destroy(gl, 4);
        Instantiate(rumble);
        Destroy(gameObject);

    }

    private void Humiliation()
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
                }
                catch
                {
                    //it's not a player
                }
                rb.AddExplosionForce(power, explosionPosition, radius);

            }

        }
        GameObject temp = Instantiate(particles, transform.position, Quaternion.identity);
        GameObject bombi = Instantiate(bombinette, transform.position, Quaternion.identity);
        Destroy(temp, 1);
        Destroy(bombi, 2);
        Instantiate(rumble);
        Destroy(gameObject);
    }

    private void SetRandomSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        
    }
}
