using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseEnvironment : MonoBehaviour
{
    private PhaseController phaseController;
    private enum ObjectType { Grass, Leaf };
    [SerializeField] private ObjectType thisObjectType;
    [SerializeField] private int leafIndex;

    Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
        phaseController = GameObject.FindWithTag("PhaseController").GetComponent<PhaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        phaseHandler();
    }

    /*
    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            if (thisObjectType == ObjectType.Grass)
            {
                if (transform.GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    transform.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", phaseController.GrassMaterialA.GetColor("_Color"));
                }
                else
                {
                    transform.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", phaseController.GrassMaterialA.GetColor("_Color"));
                }
            }
            else
            {
                transform.GetComponent<MeshRenderer>().materials[leafIndex].SetColor("_Color", phaseController.LeafMaterialA.GetColor("_Color"));
            }
        }
        else
        {
            if (thisObjectType == ObjectType.Grass)
            {
                if (transform.GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    transform.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", phaseController.GrassMaterialB.GetColor("_Color"));
                }
                else
                {
                    transform.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", phaseController.GrassMaterialB.GetColor("_Color"));
                }
            }
            else
            {
                transform.GetComponent<MeshRenderer>().materials[leafIndex].SetColor("_Color", phaseController.LeafMaterialB.GetColor("_Color"));
            }
        }
    }
    */

    private void phaseHandler()
    {
        if (phaseController.curGamePhase == PhaseController.GamePhase.A)
        {
            if (thisObjectType == ObjectType.Grass)
            {

            }
            else
            {
               
            }
        }
        else
        {
            if (thisObjectType == ObjectType.Grass)
            {

            }
            else
            {

            }
        }
    }
}
