using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int amount;
    private AudioSource AudioSource;
    private Transform sprite;
    private bool pickedUp = false;

    private void Start()
    {
        AudioSource = transform.GetComponent<AudioSource>();
        sprite = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !pickedUp)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddGems(amount);
                AudioSource.Play();
                Destroy(sprite.gameObject);
                pickedUp = true;
                Destroy(gameObject, 1f);
            }
        }
    }
}