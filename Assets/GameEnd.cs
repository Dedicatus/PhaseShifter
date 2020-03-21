using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject[] texts;

    private bool endingStarted;
    private float timer;
    [SerializeField] private float fadeInTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (endingStarted)
        {
            timer += Time.deltaTime;
            mask.GetComponent<Image>().color = Color.Lerp(new Color32(0, 0, 0, 0), new Color32(0, 0, 0, 220), timer / fadeInTime);
            foreach (GameObject text in texts)
            {
                text.GetComponent<Text>().color = Color.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), timer / fadeInTime);
            }
        }

        
    }

    public void startEnding()
    {
        endingStarted = true;
        mask.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        mask.SetActive(true);

        foreach (GameObject text in texts) 
        {
            text.GetComponent<Text>().color = new Color32(255, 255, 255, 0);
            text.SetActive(true); 
        }
    }
}
