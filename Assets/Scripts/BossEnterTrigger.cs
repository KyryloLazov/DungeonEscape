using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.Instance.HasKey)
        {
            //GameManager.Instance.Save();
            GameManager.Instance.LoadScene(2);
        }
    }
}
