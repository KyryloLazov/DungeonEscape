using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack : MonoBehaviour
{
    private bool canAtack = true;
    [SerializeField] float damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable hit = collision.gameObject.GetComponent<IDamagable>();

        if(hit != null)
        {
            if (canAtack)
            {
                hit.Damage(damage);
                canAtack = false;
                StartCoroutine(ResetAtack());
            }
            
        }
    }

    IEnumerator ResetAtack()
    {
        yield return new WaitForSeconds(0.5f);
        canAtack = true;
    }

    public void IncreaseDamage(float dmg)
    {
        damage = dmg;
    }
}
