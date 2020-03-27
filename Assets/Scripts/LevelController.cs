using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject mainCamera;

    [SerializeField] private float respawnCameraY = 25.0f;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int curSpawnPointIndex;

    [SerializeField] private int initSpawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        spawnPlayer();
        curSpawnPointIndex = initSpawnIndex - 1;
    }

    public void nextSpawnPoint()
    {
        ++curSpawnPointIndex;
    }

    public void spawnPlayer()
    {
        Instantiate(player, spawnPoints[initSpawnIndex].position, spawnPoints[initSpawnIndex].rotation);
        mainCamera.GetComponent<ThirdPersonCameraFollow>().setRotation(spawnPoints[initSpawnIndex].rotation.eulerAngles.y, respawnCameraY);
        
    }

    public void respawnPlayer()
    {
        Debug.Log("Ohyes!");
        if (spawnPoints[curSpawnPointIndex] == null)
        {
            throw new Exception("SpawnPoint Overflow");
        }
        Instantiate(player, spawnPoints[curSpawnPointIndex].position, spawnPoints[curSpawnPointIndex].rotation);
        mainCamera.GetComponent<ThirdPersonCameraFollow>().setRotation(spawnPoints[curSpawnPointIndex].rotation.eulerAngles.y, respawnCameraY);
    }
}
