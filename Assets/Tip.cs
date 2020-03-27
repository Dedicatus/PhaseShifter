using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    // Start is called before the first frame update
    private UIController uc;
    [SerializeField] private string controllerTip;
    [SerializeField] private string keyboardTip;

    void Start()
    {
        uc = GameObject.FindWithTag("UIController").GetComponent<UIController>();
    }

    
// Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(other.GetComponent<Player>().isKeyboard)
                uc.setTip(keyboardTip);
            else
                uc.setTip(controllerTip);
            uc.showTip();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Player>().isKeyboard)
                uc.setTip(keyboardTip);
            else
                uc.setTip(controllerTip);
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
