using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private PhaseController phaseController;
    private LevelController levelController;

    private enum ZonePhase { A, B, Always };
    [SerializeField] private ZonePhase thisZonePhase = ZonePhase.A;

    BoxCollider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
        levelController = GameObject.Find("LevelController").transform.GetComponent<LevelController>();
        myCollider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisZonePhase != ZonePhase.Always)
        {
            phaseHandler();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            levelController.respawnPlayer();
        }
    }

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisZonePhase)
            {
                case ZonePhase.A:
                    //gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //cubeMaterial.SetColor("_Color", new Color(253f / 255f, 85f / 255f, 85f / 255f, 255f / 255f));
                    //cubeMaterial.SetColor("_Color", blockAEnabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    //gameObject.layer = 0;
                    break;
                case ZonePhase.B:
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
            switch (thisZonePhase)
            {
                case ZonePhase.A:
                    //gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //cubeMaterial.SetColor("_Color", new Color(253f / 255f, 85f / 255f, 85f / 255f, 30f / 255f));
                    //cubeMaterial.SetColor("_Color", blockADisabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    //gameObject.layer = 8;
                    break;
                case ZonePhase.B:
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
