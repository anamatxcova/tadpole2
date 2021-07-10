using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterMovement thePlayer;
    private SpriteRenderer rend;
	private Rigidbody2D rb;
	private Vector3 startPos;
    private Vector2 movement;
	private bool chase;
	private bool run;
	public bool isPrey = true;
	public float speed;
	public float argoRange;
	public bool facingLeft = true;
	public float stunDuration = 2f;
    float horizontal;
    float vertical;
	int stunFactor = 1;

    [SerializeField]
	Transform player;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<CharacterMovement> ();
        rend = GetComponent<SpriteRenderer>();	
		rb = GetComponent<Rigidbody2D> ();
		startPos = GetComponent<Transform>().position;
		chase = false;
		run = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        float distToPlayer = Vector2.Distance(transform.position, player.position);

		if (distToPlayer < argoRange)
		{
			if (isPrey)
			{
				chase = false;
				run = true;
			}
			else
			{
				chase = true;
				run = false;
				if (thePlayer.hiding)
					StartCoroutine(Wait());
			}
		}
		
		else 
		{
			if (distToPlayer <= 0)
			{

			}
			else
			{
				chase = false;
				run = false;
			}
		}
    }

    private void FixedUpdate ()
    {
        
        if ((chase || run) && (horizontal > 0 && facingLeft) || (horizontal < 0 && !facingLeft))
        {
            facingLeft = !facingLeft;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }

		if (chase)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime * stunFactor);
		}

		else
		{
			if (run)
			{
				Vector3 dir = transform.position - player.position;
     			transform.Translate(dir.normalized * speed * Time.deltaTime * stunFactor);
			}
		}
	}

    // If  player is in the hiding spot, wait around for 10 sec and then abort chase
    IEnumerator Wait()
	{
		yield return new WaitForSeconds(10);
		chase = false;
	}

    // Handle triggers
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
		{
			if (isPrey)
			{
				//thePlayer.animator.SetBool("isEating", true);
				thePlayer.eatFood();
				Destroy(gameObject);
			}
			else
			{
				
				thePlayer.death();
			}
		}
	}

	IEnumerator Stunned()
    {
		yield return new WaitForSeconds(stunDuration);
		stunFactor = 1;
    }

	public void stun()
    {
		stunFactor = 0;
		StartCoroutine(Stunned());
    }
}
