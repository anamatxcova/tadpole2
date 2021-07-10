using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float speed;
    public float timeTrigger;
    private Rigidbody2D rb;
    private float turnTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        turnTimer = 0;
        turnAround();
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
    }

    void patrol() {
		rb.velocity = new Vector3 (rb.transform.localScale.x * (-speed), rb.velocity.y, 0f);

			turnTimer += Time.deltaTime;
			if(turnTimer >= timeTrigger){
				turnAround ();
				turnTimer = 0;
			}
	}

	void turnAround(){
		Vector3 snailScale = transform.localScale;
            snailScale.x *= -1;
            transform.localScale = snailScale;
	}
}
