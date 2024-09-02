using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamagable
{
    public float MaxHealth = 20;

    public int MaxPoice = 5;
    public int poice = 0;
    public float PushForce = 0.5f;
    public bool isStagger = false;

    bool isDead = false;

    private Animator animator;
    private Rigidbody2D rb;
    private Boss boss;
    private AudioSource audioSource;
    public AudioClip[] damageSounds;

    public float health { get; set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        audioSource = GetComponent<AudioSource>();

        health = MaxHealth;
        poice = MaxPoice;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Damage(float damage)
    {
        if (isDead) return;
        health -= damage;

        PlayRandomDamageSound();

        rb.velocity = Vector2.zero;

        Vector2 direction = new Vector3(transform.position.x, 0, 0) - new Vector3(boss.player.position.x, 0, 0);
        direction = direction.normalized;

        boss.isAtacked = true;
        rb.AddForce(direction * PushForce, ForceMode2D.Impulse);

        StartCoroutine(StopForce());

        if (health < 1)
        {
            animator.SetTrigger("Death");
            isDead = true;
            UIManager.Instance.Win();
        }

        if (!isStagger)
        {
            poice -= 1;

            if (poice < 1)
            {
                animator.SetTrigger("Stagger");
            }
        }
    }

    private void PlayRandomDamageSound()
    {
        if (damageSounds.Length == 0) return;

        int randomIndex = Random.Range(0, damageSounds.Length);
        AudioClip clip = damageSounds[randomIndex];

        audioSource.clip = clip;
        audioSource.Play();
    }

    private IEnumerator StopForce()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        boss.isAtacked = false;
    }

    public float GetPercentage()
    {
        return (float)health / MaxHealth;
    }
}
