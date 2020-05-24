using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    protected override Vector2 InputMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        return new Vector2(h, v);
    }
    protected override void Movement()
    {
        base.Movement();
        
    }
}
