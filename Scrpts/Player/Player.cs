using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private Rigidbody2D rb;
    private PlayerAnimation anim;
    private AudioSource audioSource;
    public AudioClip[] damageSounds;

    public AudioClip swingSound;
    public AudioClip flameSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip healSound;
    public AudioClip deathSound;

    private bool resetSwing = true;

    private bool ResetJump = false;
    private bool ResetDoubleJump = false;

    public bool hasDoubleJump = false;
    private bool hasFlame = false;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float JumpForce;

    public int diamonds = 0;
    public float health { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<PlayerAnimation>();
        audioSource = GetComponent<AudioSource>();

        health = 4;
    }

    void Update()
    {
        if (GameManager.Instance.isPaused) return;

        if(health <= 0) 
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Move();

        Jump();

        CheckGround();

        Atack();  
    }

    private void Move()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        anim.Run(dir);
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                ResetJump = true;
                anim.Jump(true);
                PlaySound(jumpSound);
                StartCoroutine(ResetJumpCourotuine());
            }
            else if (!CheckGround() && hasDoubleJump)
            {
                if (!ResetDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                    ResetJump = true;
                    ResetDoubleJump = true;
                    anim.Jump(true);
                    PlaySound(jumpSound);
                    StartCoroutine(ResetJumpCourotuine());
                }
            }
        }
    }

    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 3);
        if (hit.collider != null)
        {
            anim.Fall(true);
            

            ResetDoubleJump = false;
            if (!ResetJump)
            {
                anim.Jump(false);                
                return true;             
            }
        }
        anim.Fall(false);
        return false;
    }

    IEnumerator ResetJumpCourotuine()
    {
        yield return new WaitForSeconds(0.1f);
        PlaySound(landSound);
        ResetJump = false;
    }

    private void Atack()
    {
        if (Input.GetMouseButtonDown(0) && CheckGround() == true)
        {
            if (hasFlame && resetSwing)
            {
                PlaySound(flameSound);
                resetSwing = false;
                StartCoroutine(resetSwingSound());
            }
            else if(!hasFlame && resetSwing)
            {
                PlaySound(swingSound);
                resetSwing = false;
                StartCoroutine(resetSwingSound());
            }
            anim.Swing();
        }
    }

    public void Damage(float damage)
    {
        if(health < 1) return;
        
        health -= damage;

        PlayRandomDamageSound();

        UIManager.Instance.TakeDamage(health);
        if(health < 1)
        {
            anim.Death();
            PlaySound(deathSound);
            UIManager.Instance.StartDeathScreen();
        }
    }

    public void Heal()
    {
        health += 1;
        PlaySound(healSound);
        UIManager.Instance.Heal(health);
    }

    private void PlayRandomDamageSound()
    {
        if (damageSounds.Length == 0) return;

        int randomIndex = Random.Range(0, damageSounds.Length);
        AudioClip clip = damageSounds[randomIndex];
        PlaySound(clip);
       
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private IEnumerator resetSwingSound()
    {
        yield return new WaitForSeconds(0.65f);
        resetSwing = true;
    }

    public void AddGems(int amount)
    {
        diamonds += amount;       
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    public void SwipeAnim(float dmg)
    {
        anim.SwipeAnim(dmg);
        hasFlame = true;
    }
}
