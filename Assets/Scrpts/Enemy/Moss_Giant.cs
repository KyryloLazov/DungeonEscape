using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy
{
    public float Health;

    public override void Init()
    {
        base.Init();
        base.health = Health;
    }
}
