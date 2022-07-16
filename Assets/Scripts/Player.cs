using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health, maxHealth;
    public ReloadSceneScript reload;
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death Trap"))
        {
            Debug.Log("HOLY SHIT!!!");
            collision.gameObject.GetComponent<Player>().Attacked(999);
        }
    }

    public void Attacked(float receivedDamage)
    {
        Debug.Log("Player took a slap in the face" + receivedDamage);
        health -= receivedDamage;
        OnPlayerDamaged?.Invoke();

        if(health <= 0)
        { 
            health = 0;
            Debug.Log("player is dead" + receivedDamage);
            OnPlayerDeath?.Invoke();
            reload.ReloadScene();
        }
    }
}
