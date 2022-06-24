using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScriptableObjectGO : MonoBehaviour
{

    public AO_GameObject soGameObject;
    public GameObject assignWithThisGo;

    private void Awake()
    {
        soGameObject.gameObject = assignWithThisGo;

    }

}
