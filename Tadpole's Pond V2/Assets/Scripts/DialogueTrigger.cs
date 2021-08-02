using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject smallCanvas;

    // Player is in dialog trigger area
    private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player") && smallCanvas.activeInHierarchy == false) {
			smallCanvas.SetActive(true);
		}
	}

    // Player exited dialogue trigger area
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) {
			smallCanvas.SetActive(false);
		}
	}
}
