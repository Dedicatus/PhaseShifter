using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    public bool playerOn;
    [SerializeField] private bool freezeCamera = false;
    [SerializeField] private float freezeTime = 3f;
    bool isStartFreeze = false;


    private void Awake()
    {
        playerOn = false;
        isStartFreeze = false;
        //freezeCamera = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOn = true;
            if (freezeCamera)
            {
                isStartFreeze = true;
                GameObject.FindWithTag("UIController").GetComponent<UIController>().endGame();
            }
        }
    }

    private void Update()
    {
        if (isStartFreeze)
        {
            if (freezeTime > 0)
            {
                freezeTime -= Time.deltaTime;
            }
            else
            {
                GameObject.FindWithTag("MainCamera").GetComponent<ThirdPersonCameraFollow>().isFrozen = true;
            }
                
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerOn = false;
    //    }
    //}
}
