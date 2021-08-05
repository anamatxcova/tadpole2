using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRock : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if (CharacterMovement.status == "2" && other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject); 
        }
	}

    // For later when we inplement kick
    // private void OnTriggerEnter2D(Collider2D other) {
    //     // Player kicked the rock
    //     if (CharacterMovement.status == "2" && other.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.))
    //     {
    //         Destroy(gameObject); 
    //     }
	// }
}
