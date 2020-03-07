using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public enum GamePhase { A, B};
    public GamePhase curGamePhase;
    private bool phaseSwitched;
    GameObject player;

    private void Start()
    {
        curGamePhase = GamePhase.A;
        phaseSwitched = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        inputHandler();
    }

    private void inputHandler()
    {
        if (!player.GetComponent<Player>().isKeyboard)
        {
            if (Input.GetAxis("LRT") < -0.19f && !phaseSwitched)
            {
                switchPhase();
            }

            if (Input.GetAxis("LRT") >= -0.19f && Input.GetAxis("LRT") <= 0f)
            {
                phaseSwitched = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q) && !phaseSwitched)
            {
                switchPhase();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                phaseSwitched = false;
            }
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
