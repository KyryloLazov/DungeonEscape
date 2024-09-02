using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpiderAnimEvent : MonoBehaviour
{
    private Spider spider;

    private void Start()
    {
        spider = GetComponentInParent<Spider>();
    }
    public void Fire()
    {
        spider.Atack();
    }
}
