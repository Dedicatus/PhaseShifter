using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlock : MonoBehaviour
{
    private PhaseController phaseController;
    private enum BlockPhase { A, B };
    [SerializeField] private BlockPhase thisBlockPhase = BlockPhase.A;

    // Start is called before the first frame update
    void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisBlockPhase)
            {
                case BlockPhase.A:
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    break;
                case BlockPhase.B:
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
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
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    break;
                case BlockPhase.B:
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
