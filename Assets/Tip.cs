using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    // Start is called before the first frame update
    private UIController uc;
    [SerializeField] private string tip;

    void Start()
    {
        uc = GameObject.FindWithTag("UIController").GetComponent<UIController>();
    }

// Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uc.setTip(tip);
            uc.showTip();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uc.closeTip();
        }
    }
}
