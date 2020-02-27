using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public enum GamePhase { A, B};
    public GamePhase curGamePhase;
    private bool phaseSwitched;

    private void Start()
    {
        curGamePhase = GamePhase.A;
        phaseSwitched = false;
    }

    private void Update()
    {
        inputHandler();
    }

    private void inputHandler()
    {
        if (Input.GetAxis("LRT") < -0.19f && !phaseSwitched)
        {
            switchPhase();
        }

        if (Input.GetAxis("LRT") >= -0.19f && Input.GetAxis("LRT") <= 0f)
        {
            phaseSwitched = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !phaseSwitched)
        {
            switchPhase();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            phaseSwitched = false;
        }

    }

    private void switchPhase()
    {
        switch (curGamePhase)
        {
            case GamePhase.A:
                curGamePhase = GamePhase.B;
                phaseSwitched = true;
                break;
            case GamePhase.B:
                curGamePhase = GamePhase.A;
                phaseSwitched = true;
                break;
            default:
                break;
        }
    }
}
