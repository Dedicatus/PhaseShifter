using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update

    [FMODUnity.EventRef] public string JumpEvent = "event:/Jump";
    [FMODUnity.EventRef] public string DropKeyEvent = "event:/DropKey";
    [FMODUnity.EventRef] public string LandEvent = "event:/Land";
    [FMODUnity.EventRef] public string OpenDoorEvent = "event:/OpenDoor";
    [FMODUnity.EventRef] public string PickupKeyEvent = "event:/PickupKey";
    [FMODUnity.EventRef] public string ShiftPhaseEvent = "event:/ShiftPhase";
    [FMODUnity.EventRef] public string WaterSplashEvent = "event:/WaterSplash";

    FMOD.Studio.EventInstance Jump;
    FMOD.Studio.EventInstance DropKey;
    FMOD.Studio.EventInstance Land;
    FMOD.Studio.EventInstance OpenDoor;
    FMOD.Studio.EventInstance PickupKey;
    FMOD.Studio.EventInstance ShiftPhase;
    FMOD.Studio.EventInstance WaterSplash;


    void Start()
    {
        Jump = FMODUnity.RuntimeManager.CreateInstance(JumpEvent);
        DropKey = FMODUnity.RuntimeManager.CreateInstance(DropKeyEvent);
        Land = FMODUnity.RuntimeManager.CreateInstance(LandEvent);
        OpenDoor = FMODUnity.RuntimeManager.CreateInstance(OpenDoorEvent);
        PickupKey = FMODUnity.RuntimeManager.CreateInstance(PickupKeyEvent);
        ShiftPhase = FMODUnity.RuntimeManager.CreateInstance(ShiftPhaseEvent);
        WaterSplash = FMODUnity.RuntimeManager.CreateInstance(WaterSplashEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playJump()
    {
        Jump.start();
    }
    public void playDropKey()
    {
        DropKey.start();
    }
    public void playLand()
    {
        Land.start();
    }
    public void playOpenDoor()
    {
        OpenDoor.start();
    }
    public void playPickupKey()
    {
        PickupKey.start();
    }
    public void playShiftPhase()
    {
        ShiftPhase.start();
    }
    public void playWaterSplash()
    {
        WaterSplash.start();
    }
}
