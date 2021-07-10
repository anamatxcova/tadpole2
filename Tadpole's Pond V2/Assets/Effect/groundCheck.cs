using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    //created using this tutorial: https://www.youtube.com/watch?v=Fp-vWTkaiw8&t=196s
    [SerializeField]
    GameObject dustCloud;

    bool coroutineAllowed, grounded;
    private CharacterMovement thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<CharacterMovement>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals ("Ground"))
        {
            grounded = true;
            coroutineAllowed = true;
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
            coroutineAllowed = false;
        }
     }

    private void Update()
    {
        if (grounded && thePlayer.body.velocity.x != 0 && coroutineAllowed)
        {
            StartCoroutine("SpawnCloud");
            coroutineAllowed = false;
        }

        if (thePlayer.body.velocity.x ==0 || grounded)
        {
            StopCoroutine("SpawnCloud");
            coroutineAllowed = true;
        }
    }


    IEnumerator SpawnCloud()
    {
        while(grounded)
        {
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
    }


}
