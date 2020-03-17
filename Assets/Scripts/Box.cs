using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject stoneArea;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == stoneArea)
        {
            resetPosition();
        }
    }

    public void resetPosition()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
}
