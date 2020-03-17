using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotationTrigger : MonoBehaviour
{
    [SerializeField] GameObject box;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(box.transform.position.x, box.transform.position.y + 3.5f, box.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {   
        if (other.tag == "Player")
        {
            box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
