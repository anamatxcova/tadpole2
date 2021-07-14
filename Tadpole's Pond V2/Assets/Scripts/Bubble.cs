using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float bubbleSpeed = 20f;
    public Rigidbody2D body;
    public float stunDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        body.velocity = transform.right * bubbleSpeed * -1;
    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {
        Enemy enemy = hitObject.GetComponent<Enemy>();
        KoiFish koifish = hitObject.GetComponent<KoiFish>();
        if (enemy != null)
        {
            enemy.stun();
            Destroy(gameObject);
        }
        else if (koifish != null)
        {
            koifish.stun();
            Destroy(gameObject);
        }
    }
}
