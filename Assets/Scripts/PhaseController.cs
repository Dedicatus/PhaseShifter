using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public enum GamePhase { A, B};
    public GamePhase curGamePhase;
    private bool phaseSwitched;
    GameObject player;

    [SerializeField] Material grassMaterial;
    [SerializeField] Material grassMaterialA;
    [SerializeField] Material grassMaterialB;
    [SerializeField] Material leafMaterial;
    [SerializeField] Material leafMaterialA;
    [SerializeField] Material leafMaterialB;

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
        if (player != null)
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
                grassMaterial.SetColor("_Color", grassMaterialB.GetColor("_Color"));
                grassMaterial.SetColor("_EmissionColor", grassMaterialB.GetColor("_EmissionColor"));
                leafMaterial.SetColor("_Color", leafMaterialB.GetColor("_Color"));
                leafMaterial.SetColor("_EmissionColor", leafMaterialB.GetColor("_EmissionColor"));
                break;
            case GamePhase.B:
                curGamePhase = GamePhase.A;
                grassMaterial.SetColor("_Color", grassMaterialA.GetColor("_Color"));
                grassMaterial.SetColor("_EmissionColor", grassMaterialA.GetColor("_EmissionColor"));
                leafMaterial.SetColor("_Color", leafMaterialA.GetColor("_Color"));
                leafMaterial.SetColor("_EmissionColor", leafMaterialA.GetColor("_EmissionColor"));
                phaseSwitched = true;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        grassMaterial.SetColor("_Color", grassMaterialA.GetColor("_Color"));
        grassMaterial.SetColor("_EmissionColor", grassMaterialA.GetColor("_EmissionColor"));
        leafMaterial.SetColor("_Color", leafMaterialA.GetColor("_Color"));
        leafMaterial.SetColor("_EmissionColor", leafMaterialA.GetColor("_EmissionColor"));
    }
}
