using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool useInt = true;
    public int nextSceneNumber;
    private string nextSceneName;

    void OnTriggerEnter2D (Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.tag == "Player")
        {
            if (useInt)
                SceneManager.LoadScene(nextSceneNumber);
            else 
                SceneManager.LoadScene(nextSceneName);
        }
    }
}
