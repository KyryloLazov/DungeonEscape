using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public interface IDamagable
{
    float health { get; set; }
    void Damage(float damage)
    {

    }
}
