using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    private PhaseController phaseController;

    private enum BlockPhase { A, B };

    [SerializeField] private BlockPhase thisBlockPhase = BlockPhase.A;

    private enum MovingMode { Always, PhaseOnly, PlayerOn };
    [SerializeField] private MovingMode thisMovingMode = MovingMode.Always;

    private BoxCollider myTrigger;

    [SerializeField] private GameObject m_moveTrigger;

    private GameObject EnabledObject;
    private GameObject DisabledObject;

    Animator movingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
        movingAnimator = gameObject.GetComponent<Animator>();
        myTrigger = gameObject.GetComponent<BoxCollider>();
        EnabledObject = transform.Find("EnabledObject").gameObject;
        DisabledObject = transform.Find("DisabledObject").gameObject;
        if (thisMovingMode == MovingMode.PlayerOn) { movingAnimator.speed = 0; }
    }

    // Update is called once per frame
    void Update()
    {
        if (thisMovingMode == MovingMode.PhaseOnly || thisMovingMode == MovingMode.PlayerOn)
        {
            phaseHandler();
        }

        if (thisMovingMode == MovingMode.PlayerOn)
        {
            if (m_moveTrigger != null)
            {
                if (m_moveTrigger.GetComponent<MoveTrigger>().playerOn)
                {
                    movingAnimator.speed = 1;
                }
                else
                {
                    movingAnimator.speed = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisBlockPhase)
            {
                case BlockPhase.A:
                    myTrigger.enabled = true;
                    EnabledObject.SetActive(true);
                    DisabledObject.SetActive(false);
                    if (thisMovingMode == MovingMode.PhaseOnly) movingAnimator.speed = 1;
                    break;
                case BlockPhase.B:
                    myTrigger.enabled = false;
                    EnabledObject.SetActive(false);
                    DisabledObject.SetActive(true);
                    if (thisMovingMode == MovingMode.PhaseOnly) movingAnimator.speed = 0;
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
                    myTrigger.enabled = false;
                    EnabledObject.SetActive(false);
                    DisabledObject.SetActive(true);
                    if (thisMovingMode == MovingMode.PhaseOnly) movingAnimator.speed = 0;
                    break;
                case BlockPhase.B:
                    myTrigger.enabled = true;
                    EnabledObject.SetActive(true);
                    DisabledObject.SetActive(false);
                    if (thisMovingMode == MovingMode.PhaseOnly) movingAnimator.speed = 1;
                    break;
                default:
                    break;
            }
        }
    }
}
