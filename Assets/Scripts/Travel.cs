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


    [SerializeField] private string[] list1 = new string[] { "StartingScene" };
    [SerializeField] private string[] list2 = new string[] { "MoleHole", "Hallway0A", "Hallway1A", "Hallway2A" };
    [SerializeField] private string[] list3 = new string[] { "HallToDogs", "DogPark0", "DogPark1", "DogBoss" };
    [SerializeField] private string[][] data = new string[3][];

    [SerializeField] private int pScenePos;
    [SerializeField] private int cScenePos;

    void Awake()
    {
        data[0] = new string[] { "StartingScene" };
        data[1] = new string[] { "MoleHole", "Hallway0A", "Hallway1A", "Hallway2A" };
        data[2] = new string[] { "HallToDogs", "DogPark0", "DogPark1", "DogBoss" };
    }
IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("moving?");
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            yield return load;
            SceneManager.UnloadSceneAsync(sceneToUnload);
            SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByName(sceneToLoad));
            other.gameObject.transform.position = new Vector2(xPos, yPos);

            pScenePos = 0;
            cScenePos = 0;
            for(int x = 0; x < data.Length; x++)
            {
                Debug.Log("getting x?" + x);
                Debug.Log("getting data?" + (data.Length- 1));
                for (int y = 0; y < data[x].Length; y++)
                {
                    Debug.Log("getting x?" + x + y);
                    if (sceneToLoad == data[x][y])
                        cScenePos = x;
                    if (sceneToUnload == data[x][y])
                        pScenePos = x;
                }
            }
            if(pScenePos != cScenePos)
            {
                if (pScenePos == 0)
                {
                    Debug.Log("works?");
                    other.gameObject.GetComponentInChildren<AudioManager>().Stop("theme");
                }
                else if (pScenePos == 1)
                    other.gameObject.GetComponentInChildren<AudioManager>().Stop("sewers");
                else
                    other.gameObject.GetComponentInChildren<AudioManager>().Stop("fight");

                if (cScenePos == 0)
                    other.gameObject.GetComponentInChildren<AudioManager>().Play("theme");
                else if (cScenePos == 1)
                {
                    Debug.Log("works2?");
                    other.gameObject.GetComponentInChildren<AudioManager>().Play("sewers");
                }
                else
                    other.gameObject.GetComponentInChildren<AudioManager>().Play("fight");
            }

        }
    }
}