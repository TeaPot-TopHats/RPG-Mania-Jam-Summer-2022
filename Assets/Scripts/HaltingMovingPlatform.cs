using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaltingMovingPlatform : MonoBehaviour
{
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool moveLeft = true;
    [SerializeField] private bool triggered = false;
    [SerializeField] private bool stopLeft = true;

    
        // Update is called once per frame
        void Update()
        {
            if (stopLeft == true)
            {
                //Horizontal transformations
                if (transform.position.x > rightBound)
                {
                    triggered = false;
                    moveLeft = true;
                }
                if (transform.position.x < leftBound)
                {
                    moveLeft = false;
                }

                if (triggered == true && moveLeft == false)
                {
                    transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);

                }
                if (moveLeft == true)
                {
                    transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
                }
            }
            else
            {
                //Horizontal transformations
                if (transform.position.x > rightBound)
                {
                    triggered = false;
                    moveLeft = true;
                }
                if (transform.position.x < leftBound)
                {
                    moveLeft = false;
                }

                if (moveLeft == false)
                {
                    transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);

                }
                if (moveLeft == true && triggered == true)
                {
                    transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
                }
            }
        }
        //Detects if player is platform. Only moves to the right if player is on.
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (moveLeft == false)
            {
                if (col.gameObject.tag == "Player" && transform.position.x < rightBound)
                {
                    triggered = true;
                }

            }
        }
     
}
