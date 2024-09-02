using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    public float health { get; set; }

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float AggroSpeed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    [SerializeField]
    protected GameObject diamond;
    [SerializeField]
    protected float AggroDist = 5f;
    [SerializeField]
    protected float AtackDist = 1f;
    [SerializeField]
    protected float PushForce = 0.5f;
    [SerializeField] protected Transform checkground;

    public int Maxpoise;
    public int poise;

    protected Transform dest;
    protected Animator animator;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb;

    protected bool isDead = false;
    protected bool inCombat = false;
    protected bool inReach = true;

    protected bool isAtacked = false;
    public bool isStagger = false;

    protected Player Player;
    protected float distToPlayer;

    protected bool isFlipped = false;

    private AudioSource audioSource;
    public AudioClip[] damageSounds;

    public AudioClip stepSound;
    public AudioClip atackSound;
    public AudioClip deathSound;
    public AudioClip staggerSound;


    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        poise = Maxpoise;
    }

    private void Start()
    {
        Init();
    }



    public virtual void Update()
    {
        if (isDead) return;

        distToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if(distToPlayer <= AggroDist)
        {
            inCombat = true;
            animator.SetBool("InCombat", true);
        }
        else
        {
            inCombat = false;
            animator.SetBool("InCombat", false);
        }

        CheckReach();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !inCombat) return;

        SetPoint();

        if (inCombat)
        {
            if (distToPlayer > AtackDist) Movement(Player.transform, AggroSpeed); 

            Flip(Player.transform);
            Atack();            
        }

        else
        {
            Flip(dest);
            Movement(dest, speed);
        }
    }

    public virtual void Atack()
    {        
        if (distToPlayer <= AtackDist && !isStagger)
        {
            animator.SetTrigger("Atack");
            //audioSource.loop = false;
            PlaySound(atackSound);
        }

        else
        {
            animator.ResetTrigger("Atack");
        }

    }

    public virtual void Damage(float damage)
    {
        if (isDead) return;
        

        PlayRandomDamageSound();

        rb.velocity = Vector2.zero;

        Vector2 direction = new Vector3(transform.position.x, 0, 0) - new Vector3(Player.transform.position.x, 0, 0);
        direction = direction.normalized;

        isAtacked = true;
        rb.AddForce(direction * PushForce, ForceMode2D.Impulse);

        StartCoroutine(StopForce());

        if (health < 1)
        {
            animator.SetTrigger("Death");
            PlaySound(deathSound);
            isDead = true;
            Diamond _diamond = Instantiate(diamond, transform.position, Quaternion.identity).GetComponent<Diamond>();
            _diamond.amount = gems;
        }

        if (!isStagger)
        {
            poise -= 1;
           // audioSource.loop = false;
            health -= damage;
            if (poise < 1)
            {
                PlaySound(staggerSound);
                animator.SetTrigger("Hit");
            }
        }
    }

    public void PlayRandomDamageSound()
    {
        if (damageSounds.Length == 0) return;

        int randomIndex = Random.Range(0, damageSounds.Length);
        AudioClip clip = damageSounds[randomIndex];

        PlaySound(clip);
    }

    private IEnumerator StopForce()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        isAtacked = false;
    }

    public virtual void Movement(Transform destination, float CurrSpeed)
    {
        if (isStagger || isAtacked) return;

        if (!inReach)
        {
            animator.SetTrigger("Idle");
            return;
        }
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Atack")) return;
        
        Vector3 MoveTo = new Vector3(destination.position.x, transform.position.y, transform.position.z);

        

        rb.MovePosition(Vector2.MoveTowards(rb.position, MoveTo, CurrSpeed * Time.fixedDeltaTime));
    }
    
    public void Flip(Transform target)
    {
        if (isStagger) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Atack")) return;
        
        Vector3 flipped = transform.localScale;

        flipped.z *= -1f;

        if (transform.position.x < target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public virtual void SetPoint()
    {
        if (transform.position == pointA.position)
        {
            dest = pointB;
            animator.SetTrigger("Idle");
        }

        else if (transform.position == pointB.position)
        {
            dest = pointA;
            animator.SetTrigger("Idle");
        }
    }

    public virtual void CheckReach()
    {
        if (checkground != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(checkground.position, Vector2.down, 0.8f, 1 << 3);
            if (hit.collider != null)
            {
                inReach = true;
                animator.SetBool("InReach", true);
            }
            else
            {
                inReach = false;
                animator.SetBool("InReach", false);
            }
        }
        
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
