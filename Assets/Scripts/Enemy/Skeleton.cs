using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public float Health;

    public override void Init()
    {
        base.Init();
        base.health = Health;
    }

    
}
