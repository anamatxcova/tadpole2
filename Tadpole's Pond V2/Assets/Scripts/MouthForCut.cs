using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthForCut : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bubblePrefab;
    public float reloadTime = 3;

    private float nextBubbleTime = 0f;

    private CutSceneMovement thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<CutSceneMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextBubbleTime)
        {
            nextBubbleTime = Time.time + reloadTime;
            Shoot();
            thePlayer.animator.SetBool("isShooting", true);
            StartCoroutine(SetBoolIsShootingTAD());
        }
    }

    IEnumerator SetBoolIsShootingTAD ()
    {
        yield return new WaitForSeconds(0.05f);
        thePlayer.animator.SetBool("isShooting", false);
    }

    void Shoot()
    {
        GameObject tempBubble = Instantiate(bubblePrefab, firePoint.position, firePoint.rotation);
        Destroy(tempBubble, 2f);
        
    }
}
