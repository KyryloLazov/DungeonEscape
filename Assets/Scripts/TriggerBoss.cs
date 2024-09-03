using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bossMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Boss.SetActive(true);
        }
    }
}
