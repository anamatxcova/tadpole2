using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoiFish : MonoBehaviour
{
    private CharacterMovement thePlayer;
    private SpriteRenderer rend;
    public Animator animatorKoi;
	private Rigidbody2D rb;
	private Vector3 startPos;
    private Vector2 movement;
	private bool chase;
    private bool onSpot;
    private float scale;
    private float direction;
	private float timeTrigger;
    private float turnTimer;
    private float posX;
	public float speed;
	public float argoRange;
	public bool facingLeft = true;
    float horizontal;
    float vertical;
    int stunFactor = 1;
	private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        thePlayer = FindObjectOfType<CharacterMovement> ();
        rend = GetComponent<SpriteRenderer>();	
		rb = GetComponent<Rigidbody2D> ();
		startPos = GetComponent<Transform>().position;
		chase = false;
        onSpot = true;
		turnTimer = 0;
		timeTrigger = 5f;
        scale = transform.localScale.x;
        posX = transform.position.x;
        direction = 1;
    }

    void Update()
    {
		// Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        // if (!onSpot)
        // {
        //     if (transform.position.x > posX)
        //     {
        //         Debug.Log("Moving right - " + transform.position.x);
        //         if (direction == -1)
        //         {
        //             transform.localScale = new Vector2(-scale, transform.localScale.y);
        //             direction = 1;
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("Moving left - " + transform.position.x);
        //         if (direction == 1)
        //         {
        //             transform.localScale = new Vector2(scale, transform.localScale.y);
        //             direction = -1;
        //         }
        //     }
        // }
        

        posX = transform.position.x;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

		// Player is in the enemy reaction range
		if (distToPlayer < argoRange)
		{
            chase = true;
            onSpot = false;
            if (thePlayer.hiding)
					StartCoroutine(wait());
		}
		
		// Player is out of the enemy reaction range
		else 
		{
			if (distToPlayer <= 0)
			{

			}
			else
			{
				chase = false;
			}
		}
    }

    private void FixedUpdate ()
    {

		// Decide on action to be taken
		if (!chase && !onSpot)
			goBack();

		else if (chase)
		{
			chasePlayer();
		}

        if (onSpot)
            patrol();
	}

	void chasePlayer()
	{
		if ((horizontal > 0 && facingLeft) || (horizontal < 0 && !facingLeft))
        {
            facingLeft = !facingLeft;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime * stunFactor);
        animatorKoi.SetBool("IsKoiEating", true);
        StartCoroutine(SetBoolKoiEating());
    }

	void goBack()
	{
		transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime * stunFactor);
		if (startPos == transform.position)
        {
            onSpot = true;
        }
	}

	void patrol()
	{
		rb.velocity = new Vector3 (rb.transform.localScale.x * (-speed) * stunFactor, rb.velocity.y, 0f);

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

    // If  player is in the hiding spot, wait around for 10 sec and then abort chase
    IEnumerator wait()
	{
		yield return new WaitForSeconds(7);
		chase = false;
	}

    // Move Enemy character to specified direction
	void moveCharacter(Vector2 direction)
	{
		rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime * stunFactor));
	}

    // Handle triggers
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
		{
		    thePlayer.death();
            
		}
	}

    IEnumerator SetBoolKoiEating ()
    {
        yield return new WaitForSeconds(5f);
        animatorKoi.SetBool("IsKoiEating", false);
    }

    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(2);
        stunFactor = 1;
    }

    public void stun()
    {
        rb.velocity = Vector3.zero;
        stunFactor = 0;
        StartCoroutine(Stunned());
    }
}

