using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private float upperBound;
    [SerializeField] private float moveSpeed;
    private bool moveUp = true;
    public GameObject button0;
    public GameObject button1;


    // Update is called once per frame
    void Update()
    {
        if ((button0.GetComponent<Button>().leverOn == true || button1.GetComponent<Button>().leverOn == true) && moveUp == true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            if (transform.position.y > upperBound)
            {
                moveUp = false;
            }
        }
    }
}