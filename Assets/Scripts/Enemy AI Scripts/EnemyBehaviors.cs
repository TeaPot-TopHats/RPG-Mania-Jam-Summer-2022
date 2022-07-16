using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviors : MonoBehaviour
{
    public EnemySensors EnemySensors;
    public Rigidbody2D Rigid;
    //public GameObject target;
    public bool facingLeft = true;
    public bool jump;
    [SerializeField] bool isFalling;//might not need
    public float jumpStrength;
    public float movementDirection;
    public float moveSpeed;
    public float chaseSpeed;



    private void Awake()
    {
        if (facingLeft)
            movementDirection = -1;
        //else
            //movementDirection = 1;
        
    }
    public void FlipSprite()
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        movementDirection *= -1;

        facingLeft = !facingLeft;
    }

    public void Walk()
    {
        /*if (nearGround == false && isFalling == false)
            FlipSprite();*/


        //transform.position += new Vector3(movementDirection * Time.deltaTime * moveSpeed, 0);
        Rigid.velocity = new Vector2(movementDirection * moveSpeed, Rigid.velocity.y);
        
    }

    public void Stop()
    {
        Rigid.velocity = new Vector2(0, Rigid.velocity.y);
    }

    public void Knocked(bool left)
    {
        if (left)
            Rigid.AddForce(new Vector2(-2, 1), ForceMode2D.Impulse);
        else
            Rigid.AddForce(new Vector2(2, 1), ForceMode2D.Impulse);

    }

    /*public void Jump()
    {
        if (jump)
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, 0); //makes velocity 0 before jumping
            Rigid.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            jump = false;
            
        }

    }*/

    public void Jump()
    {
        Rigid.velocity = new Vector2(Rigid.velocity.x, 0); //makes velocity 0 before jumping
        Rigid.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
    }

    public bool CheckFall()//might not need
    {
        if (Rigid.velocity.y < -0.1)
            return true;
        return false;
    }

    void CheckFeet()
    {
        if (EnemySensors.feetPlanted)
            jump = true;
    }

    public void Run()
    {
        //transform.position += new Vector3(movementDirection * Time.deltaTime * chaseSpeed, 0);
        Rigid.velocity = new Vector2(movementDirection * chaseSpeed, Rigid.velocity.y);
    }



}

/*if (Vector2.Distance(transform.position, Player.transform.position) > NpcData.detectRange)
       {

           transform.position = Vector2.MoveTowards(transform.position, Player.transform.position,
               NpcData.speed * Time.deltaTime);

       }*/ 