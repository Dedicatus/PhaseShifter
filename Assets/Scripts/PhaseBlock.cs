using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlock : MonoBehaviour
{
    private PhaseController phaseController;
    private enum BlockPhase { A, B };
    [SerializeField] private BlockPhase thisBlockPhase = BlockPhase.A;

    private Material cubeMaterial;

    [SerializeField] private Material blockAEnabledMaterial;
    [SerializeField] private Material blockADisabledMaterial;
    [SerializeField] private Material blockBEnabledMaterial;
    [SerializeField] private Material blockBDisabledMaterial;

    // Start is called before the first frame update
    void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
        cubeMaterial = transform.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        phaseHandler();
    }

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisBlockPhase)
            {
                case BlockPhase.A:
                    //gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //cubeMaterial.SetColor("_Color", new Color(253f / 255f, 85f / 255f, 85f / 255f, 255f / 255f));
                    cubeMaterial.SetColor("_Color", blockAEnabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    //gameObject.layer = 0;
                    break;
                case BlockPhase.B:
                    //gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //cubeMaterial.SetColor("_Color", new Color(103f / 255f, 231f / 255f, 250f / 255f, 30f / 255f));
                    cubeMaterial.SetColor("_Color", blockBDisabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    //gameObject.layer = 8;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (thisBlockPhase)
            {
                case BlockPhase.A:
                    //gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //cubeMaterial.SetColor("_Color", new Color(253f / 255f, 85f / 255f, 85f / 255f, 30f / 255f));
                    cubeMaterial.SetColor("_Color", blockADisabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    //gameObject.layer = 8;
                    break;
                case BlockPhase.B:
                    //gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //cubeMaterial.SetColor("_Color", new Color(103f / 255f, 231f / 255f, 250f / 255f, 255f / 255f));
                    cubeMaterial.SetColor("_Color", blockBEnabledMaterial.GetColor("_Color"));
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    //gameObject.layer = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
