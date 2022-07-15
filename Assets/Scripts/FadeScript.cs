using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUICanvas;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public void ShowUI()
    {
        fadeIn = true;
    }// end of show ui
    public void hideUI()
    {
        fadeOut = true;
    }// end of hide ui

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if(myUICanvas.alpha < 1)
            {
                myUICanvas.alpha += Time.deltaTime;
                if(myUICanvas.alpha >= 1)
                {
                    fadeIn = false;
                }// end of if myUIcanvas.alpha >= 1
            }// end of if myUIcanvas.alpha >= 1
        }// end of if fade IN

        if (fadeOut)
        {
            if (myUICanvas.alpha >= 0)
            {
                myUICanvas.alpha -= Time.deltaTime;
                if (myUICanvas.alpha == 0)
                {
                    fadeOut = false;
                }// end of if myUIcanvas.alpha >= 1
            }// end of if myUIcanvas
        }// end of fadeOUT
        }// end of update
}
