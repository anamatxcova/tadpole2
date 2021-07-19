using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        // Player is on the bed
        if (CharacterMovement.status == "12" && other.gameObject.tag.Equals("Player"))
        {
            // Change Player status and reset foodbar
            CharacterMovement.status = "2";
            
            // If the food bar is full (equls maxFood) start sleep
            SceneManager.LoadScene(4); // Play closing cutscene

            // Change tadpole to tadpole with legs here
        }
	}
}
