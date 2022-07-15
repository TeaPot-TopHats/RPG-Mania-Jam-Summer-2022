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
    [SerializeField] private bool alreadyTriggered = false;

    
        // Update is called once per frame
        void Update()
        {
            if (stopLeft == true) // Platform stops on left side
            {
                // Horizontal transformations

                if (transform.position.x > rightBound) // Once platform hits right bound
                {
                    moveLeft = true;
                }
                if (transform.position.x < leftBound) // Once platform hits left bound
                {
                    moveLeft = false;
                }

                if (triggered == true && moveLeft == false) 
                {
                    stopLeft = false;
                    transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
                    triggered = false;
                }
                if (moveLeft == true)
                {
                    transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
                }
            }
            if (stopLeft == false) // Platform stops on right side
            {
                // Horizontal transformations

                if (transform.position.x > rightBound)
                {
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
                    stopLeft = true;
                    triggered = false;
                }
                
            }
           
        }
        // Detects if player is platform. Only moves to the right if player is on.
        private void OnTriggerEnter2D(Collider2D col)
        {

            if (alreadyTriggered == false)
            {
                if (col.gameObject.tag == "Player")
                {
                    triggered = true;
                    alreadyTriggered = true;

                }
            }

        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                alreadyTriggered = false;
            }
    }

        
     
}
