using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlock : MonoBehaviour
{
    private PhaseController phaseController;
    private enum BlockPhase { A, B };
    [SerializeField] private BlockPhase thisBlockPhase = BlockPhase.A;

    private GameObject EnabledObject;
    private GameObject DisabledObject;

    // Start is called before the first frame update
    void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
        EnabledObject = transform.Find("EnabledObject").gameObject;
        DisabledObject = transform.Find("DisabledObject").gameObject;
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
                    EnabledObject.SetActive(true);
                    DisabledObject.SetActive(false);
                    break;
                case BlockPhase.B:
                    EnabledObject.SetActive(false);
                    DisabledObject.SetActive(true);
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
                    EnabledObject.SetActive(false);
                    DisabledObject.SetActive(true);
                    break;
                case BlockPhase.B:
                    EnabledObject.SetActive(true);
                    DisabledObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
