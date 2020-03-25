using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioController ac;
    private UIController uc;

    void Start()
    {
        ac = GameObject.FindWithTag("AudioController").GetComponent<AudioController>();
        uc = GameObject.FindWithTag("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uc.addGem();
            uc.setGemActive();
            other.GetComponent<Player>().addBouns();
            ac.playPickupKey();
            Destroy(gameObject);
        }
    }
}
