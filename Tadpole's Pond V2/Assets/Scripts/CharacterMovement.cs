using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;
    private float tadLoc;
    private int maxFood = 5;
    static int foodPoint = 0;
    public static string status;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    float flipRatio = 0f;

    public float targetAngle = 45f;
    public float rotateSpeed = 7.5f;

    public float runSpeed = 15.0f;
    public bool facingLeft = true;

    private SpriteRenderer rend;
    public GameObject gameOverPanel;
    public GameObject levelChanger;
	private bool canHide = false;
	public bool hiding = false;
    public FoodBar foodBar;
    public static CharacterMovement Instance;

    void Start()
    {
        status = "1";
        // Deal with duplicates in DontDestroyOnLoad
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        foodBar.setBar(foodPoint);
    }

    void Update()
    {

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        tadLoc = vertical + horizontal;
        animator.SetFloat("Speed", Mathf.Abs(tadLoc));


        // Hiding the player from https://www.youtube.com/watch?v=7kIOeELWwbI&t=51s
        if (canHide)
		{
			Physics2D.IgnoreLayerCollision(8, 9 , true);
			rend.sortingOrder = 0;
			hiding = true;
			Debug.Log("Player is hiding");
		}

		else
		{
			Physics2D.IgnoreLayerCollision(8, 9 , false);
			rend.sortingOrder = 3;
			hiding = false;
		}

    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        if ((horizontal > 0 && facingLeft) || (horizontal < 0 && !facingLeft)) // only flip when the tadpole is facing the other way
        {
            facingLeft = !facingLeft;
            flipRatio = 180 - flipRatio;
            transform.Rotate(0f, 180f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, flipRatio, transform.rotation.z), 1f);
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, flipRatio, -1 * targetAngle * vertical), rotateSpeed * Time.deltaTime);
    }

	private void OnTriggerStay2D(Collider2D other) {
        // Player entered the hiding spot
		if (other.gameObject.tag.Equals("Hiding spot")) {
			canHide = true;
			other.gameObject.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 0.5f);
			Debug.Log("Player entered the hiding spot");
		}

        // Player is on the bed
        if (CharacterMovement.foodPoint >= maxFood && other.gameObject.tag.Equals("Bed"))
        {
            // If the food bar is full (equls maxFood) start sleep
            status = "12";
            SceneManager.LoadScene(4, LoadSceneMode.Single); // Play closing cutscene
            levelChanger.SetActive(true);

            // Change tadpole to tadpole with legs here
        }
	}

    // Player exited hiding spot
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Hiding spot")) {
			canHide = false;
			other.gameObject.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 1f);
			Debug.Log("Player exited the hiding spot");
		}
	}

    public void death() {
        // gameObject.GetComponent<Animator>().Play ("PlayerDeath");
        status = "dead";
        FindObjectOfType<audioManager>().Play("playerDeath");
        Destroy(gameObject); 
        gameOverPanel.SetActive(true);       
	}

    public void eatFood()
    {
        foodPoint++;
        Debug.Log(foodPoint);
        foodBar.setBar(foodPoint);
        animator.SetBool("isEating", true);
        FindObjectOfType<audioManager>().Play("playerEat");
        StartCoroutine(SetBoolIsEating());
    }

    IEnumerator SetBoolIsEating()
    {
        yield return new WaitForSeconds(0.75f);
        animator.SetBool("isEating", false);
    }

    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();
    }

    void FindStartPos()
    {
        if (GameObject.FindWithTag("StartPos") != null)
            transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
