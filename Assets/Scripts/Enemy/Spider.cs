using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public float Health;

    [SerializeField]
    private GameObject Acid;
    private Transform player;

    public override void Init()
    {
        base.Init();
        player = GameObject.FindWithTag("Player").transform;
        base.health = Health;
    }

    public override void Update()
    {
        if (isDead) return;

        Flip(player);
    }

    public override void Movement(Transform destination, float speed)
    {
        //Don't move        
    }

    public override void Damage(float damage)
    {
        if (isDead) return;

        health -= damage;

        PlayRandomDamageSound();

        if (health < 1)
        {
            animator.SetTrigger("Death");
            PlaySound(deathSound);
            isDead = true;
            Diamond _diamond = Instantiate(diamond, transform.position, Quaternion.identity).GetComponent<Diamond>();
            _diamond.amount = gems;
        }
    }

    public override void Atack()
    {
        PlaySound(atackSound);
        Vector2 Trajectory = new Vector3(player.position.x, 0, 0) - new Vector3(transform.position.x, 0, 0);
        Trajectory = Trajectory.normalized;
        GameObject Projectile = Instantiate(Acid, transform.position, Quaternion.identity);
        Projectile.GetComponent<Acid>().Trajectory = Trajectory;
    }
}
