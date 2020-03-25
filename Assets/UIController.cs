using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameEnd ge;
    [SerializeField] private float GemTimer = 5f;
    [SerializeField] private int GemNum = 0;
    private float GemTimeCount = 0f;
    public bool isTip;
    public GameObject GemCount, Tip, GemUI, TipUI;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        ge = GameObject.Find("GameEnd").GetComponent<GameEnd>();
        GemNum = 0;
        GemTimeCount = 0f;
        isTip = false;
        GemUI.SetActive(false);
        TipUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        showGem();
    }

    public void endGame()
    {
        ge.startEnding();
    }

    public void addGem()
    {
        GemNum++;
    }
    public void setGemActive()
    {
        GemTimeCount = GemTimer;
        GemUI.SetActive(true);
    }

    void showGem()
    {
        if (GemTimeCount > 0f)
        {
            GemTimeCount -= Time.deltaTime;
            GemCount.GetComponent<Text>().text = GemNum.ToString();
        }
        else GemUI.SetActive(false);
    }

    public void setTip(string s)
    {
        Tip.GetComponent<Text>().text = s;
    }

    public void showTip()
    {
        TipUI.SetActive(true);
    }

    public void closeTip()
    {
        TipUI.SetActive(false);
    }
}
