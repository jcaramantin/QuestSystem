using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public float speed;
    public int life;
    public int maxLife, minLife;


    protected Vector2 movement;
    protected Vector2 lookDirection;

    protected Rigidbody2D rb2d;
    protected BoxCollider2D bc2d;
    protected SpriteRenderer sprite;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

    }
    protected void Update()
    {
        
    }
    protected abstract Vector2 InputMove();
    

    protected virtual void Movement()
    {

        movement = InputMove();

        rb2d.MovePosition(rb2d.position + movement * speed*Time.deltaTime);

    }
    protected void calculateLookDirection()
    {
        if(!Mathf.Approximately(movement.x,0)|| !Mathf.Approximately(movement.y, 0))
        {
            lookDirection = movement.normalized;
        }
    }
    protected virtual void AddorSustrLife(int change)
    {
        if(life + change > maxLife)
        {
            return;
        }
        else
        {
            if(life - change < minLife)
            {
                life = minLife;
            }
        }
    }
}
