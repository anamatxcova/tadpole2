using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleEnvironment : MonoBehaviour
{
    //created following this tutorial, and modified to fit our game: https://www.youtube.com/watch?v=E7gmylDS1C4&t=335s
    public float speed = 10.0f;
    public Animator eBubbleanimator;

    private Rigidbody2D rb;
    private Vector2 screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < screenBounds.y *2)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
		{
            eBubbleanimator.SetBool("isBurst", true);
            FindObjectOfType<audioManager>().Play("ebubblePop");
            Destroy(gameObject);
        }
	}
}
