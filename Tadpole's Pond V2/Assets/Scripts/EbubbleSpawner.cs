using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbubbleSpawner : MonoBehaviour
{
    //created following this tutorial, and modified to fit our game: https://www.youtube.com/watch?v=E7gmylDS1C4&t=335s
    public GameObject EbubblePrefab;
    public float respawnTime = 1.0f;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(EBubbleWave());
    }

    private void spawnEBubble()
    {
        GameObject a = Instantiate(EbubblePrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y *2);
    }

    IEnumerator EBubbleWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEBubble();
        }
    }    
}
