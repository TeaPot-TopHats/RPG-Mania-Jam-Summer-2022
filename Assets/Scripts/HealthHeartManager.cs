using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartManager : MonoBehaviour
{
    public GameObject heartPrefaps;
    public float health, maxHealth;
    List<HeartsManager> hearts = new List<HeartsManager>();

    private void OnEnable()
    {
        // when player take damage draw a new heart
        /*
         * for example if you have a method called OnPlayerDamaged you would call that method here and draw a heart
         * 
         * Classname.OnPlayerDamager += drawHearts;
         */
    }

    private void OnDisable()
    {
        // basiclly disable onEnable method
        /*
         * ClassName.OnplayerDamaged -= drawHearts;
         */
    }
    public void Start()
    {
        DrawHearts();
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject); // destory them ALL BOI 
        }
        hearts = new List<HeartsManager>();
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefaps); // instantiating a prefap
        newHeart.transform.SetParent(transform); // setting the transform parent 
        newHeart.transform.localScale = new Vector3(1, 1, 0);   
        HeartsManager heartsComponent = newHeart.GetComponent<HeartsManager>();
        heartsComponent.setHeartImage(HeartStatus.Empty); // telling our component prefap to be set to empty
        hearts.Add(heartsComponent); // adding hearts :)
    }// end of create empty heart

    public void DrawHearts()
    {
        ClearHearts(); // clear all hearts 
        // we need to see how many hearts to draw in total
        // this would be base one player max health
        float maxHealthRemainder = maxHealth % 2; // check if the max health is even or odd
        int heatsToMake = (int)((maxHealth / 2) + maxHealthRemainder);

        for(int i = 0; i < heatsToMake; i++)
        {
            CreateEmptyHeart(); 
        } 

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusReminder = (int)Mathf.Clamp(health - (i * 2), 0, 2);
            hearts[i].setHeartImage((HeartStatus)heartStatusReminder);
        }

    }
}// end of class
