using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public float speed;
    [SerializeField] int damage;
    public Vector2 Trajectory;

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }
    void Update()
    {
        transform.Translate(Trajectory * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IDamagable hit = collision.GetComponent<IDamagable>();
            if (hit != null)
            {
                hit.Damage(damage);
                Destroy(gameObject);
            }
        }
        else if(collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
