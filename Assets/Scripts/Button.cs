using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool leverOn = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            leverOn = true;
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }

}