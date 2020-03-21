using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameEnd ge;
    // Start is called before the first frame update
    void Start()
    {
        ge = GameObject.Find("GameEnd").GetComponent<GameEnd>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endGame()
    {
        ge.startEnding();
    }
}
