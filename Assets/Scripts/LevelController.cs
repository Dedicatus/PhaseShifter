using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int spawnPointIndex;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlayer();
        spawnPointIndex = 0;
    }

    public void nextSpawnPoint()
    {
        ++spawnPointIndex;
    }

    public void spawnPlayer()
    {
        Instantiate(player, spawnPoints[0].position, spawnPoints[0].rotation);
    }

    public void respawnPlayer()
    {
        if (spawnPoints[spawnPointIndex] == null)
        {
            throw new Exception("SpawnPoint Overflow");
        }
        Instantiate(player, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
