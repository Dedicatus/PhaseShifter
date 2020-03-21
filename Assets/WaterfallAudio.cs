using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallAudio : MonoBehaviour
{
    [FMODUnity.EventRef] public string WaterfallEvent = "event:/WaterFall";
    FMOD.Studio.EventInstance Waterfall;
    // Start is called before the first frame update
    void Start()
    {
        Waterfall = FMODUnity.RuntimeManager.CreateInstance(WaterfallEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Waterfall, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }
    private void OnEnable()
    {
        Waterfall.start();
    }

    private void OnDisable()
    {
        Waterfall.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
