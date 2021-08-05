using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFollow : MonoBehaviour
{
    private CinemachineVirtualCamera myCinemachine;

    // Start is called before the first frame update
    void Start()
    {
        myCinemachine = GetComponent<CinemachineVirtualCamera>();
        myCinemachine.m_Follow = GameObject.FindWithTag("Player").transform;
    }

}
