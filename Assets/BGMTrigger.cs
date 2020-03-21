using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
    [SerializeField] private float part = 0f;
    [SerializeField] private float melodyIntensity = 0f;

    private AudioController ac;

    // Start is called before the first frame update
    void Start()
    {
        ac = GameObject.FindWithTag("AudioController").GetComponent<AudioController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ac.setBGMPart(part);
            ac.setBGMMelodyIntensity(melodyIntensity);
        }
    }
}
