using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private float cooldownEnd;
    private float cooldownTime = 2f;

    // Update is called once per frame
    void Update()
    {
    }

    public void Interact()
    {
        if (Time.time >= cooldownEnd)
        {
            cooldownEnd = Time.time + cooldownTime;
            Vector2 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
        }
    }
}