using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    private PhaseController phaseController;

    private enum BlockPhase { A, B };
    [FMODUnity.EventRef] public string WaterfallEvent = "event:/Wind";
    FMOD.Studio.EventInstance Wind;
    [SerializeField] private BlockPhase thisBlockPhase = BlockPhase.A;

    private enum MovingMode { Always, PhaseOnly, PhaseBoth, PlayerOn };
    [SerializeField] private MovingMode thisMovingMode = MovingMode.Always;

    private BoxCollider myTrigger;

    [SerializeField] private GameObject m_moveTrigger;
    [SerializeField] private Transform spwanPoint;

    private GameObject EnabledObject;
    private GameObject DisabledObject;
    private bool isPlayerAboard;
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
        Wind = FMODUnity.RuntimeManager.CreateInstance(WaterfallEvent);
        isPlayerAboard = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisMovingMode == MovingMode.PhaseOnly || thisMovingMode == MovingMode.PlayerOn|| thisMovingMode == MovingMode.PhaseBoth)
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
        if (other.tag == "Player"|| other.tag == "Box")
        {

            if (other.tag == "Box")
            {
                other.transform.parent.parent.parent = transform;
            }
            else
            {
                other.transform.parent = transform;
                isPlayerAboard = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player" || other.tag == "Box")
        {

            if (other.tag == "Box")
                other.transform.parent.parent.parent = null;
            else
            {
                isPlayerAboard = false;
                other.transform.parent = null;
                Wind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }

    public void resetBlock()
    {
        if (thisMovingMode == MovingMode.PlayerOn && spwanPoint != null)
        {
            transform.position = spwanPoint.position;
            transform.rotation = spwanPoint.rotation;
            movingAnimator.Play(movingAnimator.runtimeAnimatorController.animationClips[0].name, -1, 0f);
            m_moveTrigger.GetComponent<MoveTrigger>().playerOn = false;
        }
    }

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            switch (thisBlockPhase)
            {
                case BlockPhase.A:
                    if (thisMovingMode == MovingMode.PhaseOnly)
                    {
                        myTrigger.enabled = true;
                        movingAnimator.speed = 1;
                    }
                    EnabledObject.SetActive(true);
                    DisabledObject.SetActive(false);
                    break;
                case BlockPhase.B:
                    if (thisMovingMode == MovingMode.PhaseOnly)
                    {
                        myTrigger.enabled = false;
                        movingAnimator.speed = 0;
                    }
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
                    if (thisMovingMode == MovingMode.PhaseOnly)
                    {
                        myTrigger.enabled = false;
                        movingAnimator.speed = 0;
                    }
                    EnabledObject.SetActive(false);
                    DisabledObject.SetActive(true);
                    break;
                case BlockPhase.B:
                    if (thisMovingMode == MovingMode.PhaseOnly)
                    {
                        myTrigger.enabled = true;
                        movingAnimator.speed = 1;
                    }
                    EnabledObject.SetActive(true);
                    DisabledObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    void playwind()
    {
        if (isPlayerAboard)
        { 
            Wind.start();
        }
           
    }

    void stopwind()
    {
        if (isPlayerAboard)
        {
            Wind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
