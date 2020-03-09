using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Transform spawnPoint;
    private GameObject myKey;
    private Player playerScript;

    private void Start()
    {
        spawnKey();
    }

    private void Update()
    {
        if (myKey == null)
        {
            spawnKey();
        }
    }

    private void spawnKey()
    {
        myKey = Instantiate(keyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerScript = other.GetComponent<Player>();
            if (playerScript.hasKey)
            {
                Destroy(gameObject);
                Destroy(myKey);
                playerScript.keyInRange = false;
                playerScript.hasKey = false;
            }
        }
    }
}
