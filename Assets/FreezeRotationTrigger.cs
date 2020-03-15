using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotationTrigger : MonoBehaviour
{
    [SerializeField] GameObject stone;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(stone.transform.position.x, stone.transform.position.y + 3.5f, stone.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
