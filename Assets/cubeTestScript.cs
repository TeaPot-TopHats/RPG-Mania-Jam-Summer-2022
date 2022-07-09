using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeTestScript : MonoBehaviour
{
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;   
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.GetKeyDown(KeybindsActions.Jump))
        {
            transform.Translate(0,1,0);
        }
    }
}
