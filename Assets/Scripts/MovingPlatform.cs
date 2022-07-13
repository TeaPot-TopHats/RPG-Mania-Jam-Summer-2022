using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private float upperBound;
    [SerializeField] private float lowerBound;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool moveRight = true;
    [SerializeField] private bool moveUp = true;

    // Update is called once per frame
    void Update()
    {
        //Horizontal transformations
        if (transform.position.x > rightBound)
        {
            moveRight = false;
        }
        if (transform.position.x < leftBound)
        {
            moveRight = true;
        }

        if (moveRight == true)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }

        //Vertical transformations
        if (transform.position.y > upperBound)
        {
            moveUp = false;
        }
        if (transform.position.y < lowerBound)
        {
            moveUp = true;
        }

        if (moveUp == true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }
    }
}
