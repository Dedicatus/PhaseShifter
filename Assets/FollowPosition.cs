using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] GameObject myObject;
    [SerializeField] float yOffset;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(myObject.transform.position.x, myObject.transform.position.y + yOffset, myObject.transform.position.z);
    }
}
