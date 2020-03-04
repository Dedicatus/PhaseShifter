using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointTrigger : MonoBehaviour
{
    private LevelController levelController;

    private bool triggered;

    private void Start()
    {
        levelController = GameObject.Find("LevelController").transform.GetComponent<LevelController>();
        triggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!triggered)
            {
                levelController.nextSpawnPoint();
                triggered = true;
            }
        }
    }
}
