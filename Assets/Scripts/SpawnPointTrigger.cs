using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointTrigger : MonoBehaviour
{
    private LevelController levelController;

    [SerializeField] private bool triggered;

    private void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        triggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!triggered && !other.GetComponent<Player>().isRespawning)
            {
                levelController.nextSpawnPoint();
                triggered = true;
            }
        }
    }
}
