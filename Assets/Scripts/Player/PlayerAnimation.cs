using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer Sprite;

    [SerializeField]
    private AnimatorOverrideController overrideController;

    private Animator SwordAnim;
    private SpriteRenderer SwordSprite;

    void Start()
    {
        animator= GetComponent<Animator>();        
        Sprite = GetComponent<SpriteRenderer>();

        SwordAnim = transform.GetChild(1).GetComponentInChildren<Animator>();
        SwordSprite = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
    }

    public void Run(float move)
    {
        animator.SetFloat("Move", Mathf.Abs(move));
        
        if(move < 0)
        {
            Sprite.flipX = true;
            SwordSprite.flipY = true;

            Vector3 SwordPos = SwordSprite.transform.localPosition;
            SwordPos.x = -1.01f;
            SwordSprite.transform.localPosition = SwordPos;
        }
        else if(move > 0)
        {
            Sprite.flipX = false;
            SwordSprite.flipY = false;

            Vector3 SwordPos = SwordSprite.transform.localPosition;
            SwordPos.x = 1.01f;
            SwordSprite.transform.localPosition = SwordPos;
        }
    }

    public void Jump(bool isGround)
    {
        animator.SetBool("Jump", isGround);
    }

    public void Swing()
    {
        animator.SetTrigger("Atack");       
    }

    public void SpawnArc()
    {
        SwordAnim.SetTrigger("Atack");       
    }

    public void Fall(bool isGrounded)
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void SwipeAnim(float dmg)
    {
        animator.runtimeAnimatorController = overrideController;
        transform.GetChild(1).GetComponentInChildren<Atack>().IncreaseDamage(dmg);
    }
}
