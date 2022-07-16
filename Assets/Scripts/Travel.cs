using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Travel : MonoBehaviour
{
    
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneToUnload;
    [SerializeField] private float xPos;
    [SerializeField] private float yPos;



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            yield return load;
            SceneManager.UnloadSceneAsync(sceneToUnload);
            SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByName(sceneToLoad));
            other.gameObject.transform.position = new Vector2(xPos, yPos);

        }
    }
}