using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject player;
    private PhaseController phaseController;
    [SerializeField] private GameObject myCamera;

    private enum Phase { A, B, Both };
    [SerializeField] private Phase thisPhase = Phase.A;

    private Material myMaterial;

    [SerializeField] private Material keyAEnabledMaterial;
    [SerializeField] private Material keyBEnabledMaterial;
    [SerializeField] private Material keyDisabledMaterial;


    private bool inRange;
    [SerializeField] private bool onPlayer;

    GameObject myPlayer;

    private void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
        myCamera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        myMaterial = transform.GetChild(0).GetComponent<Renderer>().material;
        inRange = false;
    }

    private void Update()
    {
        if (thisPhase != Phase.Both)
        {
            phaseHandler();
        }

        pickUpKey();

        if (onPlayer)
        {
            rotateKeyWithCamera();
        }
    }

    private void pickUpKey()
    {
        if (((player.GetComponent<Player>().isKeyboard && Input.GetKey(KeyCode.F) ) ||(!player.GetComponent<Player>().isKeyboard&&Input.GetAxis("LRT") > 0.19f)) && inRange && !onPlayer && !myPlayer.GetComponent<Player>().pickUpAnimation())
        {
            if ( myPlayer != null )
            {
                transform.parent = myPlayer.transform;
                transform.position = myPlayer.transform.position + new Vector3(0f, 1.76f, 0.1f);
                rotateKeyWithCamera();
                onPlayer = true;
                myPlayer.GetComponent<Player>().pickingUpKey();
                myPlayer.GetComponent<Player>().hasKey = true;
            }
        }
    }

    private void dropKey()
    {
        transform.parent = null;
        transform.position = myPlayer.transform.position + new Vector3(0f, 0.18f, 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        inRange = false;
        onPlayer = false;
        myPlayer.GetComponent<Player>().keyInRange = false;
        myPlayer.GetComponent<Player>().hasKey = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myPlayer = other.gameObject;
            myPlayer.GetComponent<Player>().keyInRange = true;
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            myPlayer.GetComponent<Player>().keyInRange = false;
            inRange = false;
        }
    }

    private void rotateKeyWithCamera()
    {
        transform.rotation = Quaternion.Euler(0.0f, myCamera.transform.rotation.eulerAngles.y, 0f);
    }

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisPhase)
            {
                case Phase.A:
                    gameObject.GetComponent<SphereCollider>().enabled = true;
                    myMaterial.SetColor("_Color", keyAEnabledMaterial.GetColor("_Color"));
                    myMaterial.SetColor("_EmissionColor", keyAEnabledMaterial.GetColor("_EmissionColor"));
                    break;
                case Phase.B:
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                    myMaterial.SetColor("_Color", keyDisabledMaterial.GetColor("_Color"));
                    myMaterial.SetColor("_EmissionColor", keyDisabledMaterial.GetColor("_EmissionColor"));
                    if (onPlayer) dropKey();
                    break;
                default:
                    break;
            }
        }
        else if (phaseController.curGamePhase == PhaseController.GamePhase.B)
        {
            switch (thisPhase)
            {
                case Phase.A:
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                    myMaterial.SetColor("_Color", keyDisabledMaterial.GetColor("_Color"));
                    myMaterial.SetColor("_EmissionColor", keyDisabledMaterial.GetColor("_EmissionColor"));
                    if (onPlayer) dropKey();
                    break;
                case Phase.B:
                    gameObject.GetComponent<SphereCollider>().enabled = true;
                    myMaterial.SetColor("_Color", keyBEnabledMaterial.GetColor("_Color"));
                    myMaterial.SetColor("_EmissionColor", keyBEnabledMaterial.GetColor("_EmissionColor"));
                    break;
                default:
                    break;
            }
        }
    }
}
