using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject[] keyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    private GameObject[] myKeys;
    private Player playerScript;

    private void Start()
    {
        myKeys = new GameObject[keyPrefabs.Length];
        spawnKey();
    }

    public void spawnKey()
    {
        if (myKeys != null)
        {
            foreach (GameObject m_key in myKeys) { Destroy(m_key); }
        }

        if (keyPrefabs == null) 
        {
            Debug.LogError("Missing key prefab");
            return;
        }

        for (int i = 0; i < keyPrefabs.Length; i++)
        {
            if (keyPrefabs[i] == null)
                continue;
            else 
            myKeys[i] = Instantiate(keyPrefabs[i], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerScript = other.GetComponent<Player>();
            if (playerScript.hasKey)
            {
                Destroy(gameObject);

                if (keyPrefabs == null)
                {
                    Debug.LogError("Missing key prefab");
                    return;
                }

                Destroy(other.transform.GetComponent<Player>().m_key);

                playerScript.keyInRange = false;
                playerScript.hasKey = false;
            }
        }
    }
}
