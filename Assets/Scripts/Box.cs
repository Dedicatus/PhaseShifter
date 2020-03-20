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
        transform.parent.parent.parent = null;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
