using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    private Animator animator;

    public bool isFlipped = false;
    public bool isAtacked = false;

    public float DistanceToHit = 2f;

    public float distanceToFire = 5f;
    public float timeBetweenFire = 10f;
    float timeSinceFire = 0f;

    public void Look()
    {
        Vector3 flipped = transform.localScale;

        flipped.z *= -1f;

        if(transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateTimers();
        
        if(Vector2.Distance(transform.position, player.position) <= DistanceToHit)
        {
            animator.SetBool("InRange", true);
        }
        else
        {
            animator.SetBool("InRange", false);
        }

        Fire();
    }

    private void UpdateTimers()
    {
        timeSinceFire += Time.deltaTime;
    }

    private void Fire()
    {
        if(Vector2.Distance(transform.position, player.position) <= distanceToFire && timeSinceFire > timeBetweenFire)
        {
            timeSinceFire = 0;
            animator.SetTrigger("Fire");
        }
        
    }
}
