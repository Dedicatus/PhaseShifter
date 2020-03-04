using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Player playerScript;
    private PhaseController phaseController;

    private enum Phase { A, B, Both };
    [SerializeField] private Phase thisPhase = Phase.A;

    private bool inRange;

    GameObject myPlayer;

    private void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
        inRange = false;
    }

    private void Update()
    {
        if (thisPhase != Phase.Both)
        {
            phaseHandler();
        }
        pickedUp();
    }

    private void pickedUp()
    {
        if (Input.GetAxis("LRT") > 0.19f)
        {
            if ( myPlayer != null )
            {
                transform.parent = myPlayer.transform;
                transform.position = myPlayer.transform.position + new Vector3(0.02f, 2.6f, 0.1f);
                transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myPlayer = other.gameObject;
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisPhase)
            {
                case Phase.A:
                    //gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //cubeMaterial.SetColor("_Color", new Color(253f / 255f, 85f / 255f, 85f / 255f, 255f / 255f));
                    //cubeMaterial.SetColor("_Color", blockAEnabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    //gameObject.layer = 0;
                    break;
                case Phase.B:
                    //gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //cubeMaterial.SetColor("_Color", new Color(103f / 255f, 231f / 255f, 250f / 255f, 30f / 255f));
                    //cubeMaterial.SetColor("_Color", blockBDisabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    //gameObject.layer = 8;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (thisPhase)
            {
                case Phase.A:
                    //gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //cubeMaterial.SetColor("_Color", new Color(253f / 255f, 85f / 255f, 85f / 255f, 30f / 255f));
                    //cubeMaterial.SetColor("_Color", blockADisabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    //gameObject.layer = 8;
                    break;
                case Phase.B:
                    //gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //cubeMaterial.SetColor("_Color", new Color(103f / 255f, 231f / 255f, 250f / 255f, 255f / 255f));
                    //cubeMaterial.SetColor("_Color", blockBEnabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    //gameObject.layer = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
