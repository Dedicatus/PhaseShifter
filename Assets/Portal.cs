using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && exit != null)
        {
            other.transform.position = exit.transform.position + distanceDifference(other.transform);
        }
    }

    private Vector3 distanceDifference(Transform other)
    {
        return (other.transform.position - transform.position);
    }
}
