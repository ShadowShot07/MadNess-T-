using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private EventInstance mainTheme;
    private EventInstance gameTheme;
    private EventInstance footsteps;
    private EventInstance climb;
    private EventInstance crown;
    private EventInstance voice_princess;
    private EventInstance voice_twiceSlodierAnt;
    private EventInstance voice_Scientist;
    private EventInstance voice_hystericant;
    private EventInstance voice_wideExoAnt;
    private EventInstance uiAccept;
    private EventInstance uiCancel;


    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1f;
    [Range(0, 1)]
    public float musicVolume = 1f;
    [Range(0, 1)]
    public float sfxVolume = 1f;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    public enum BusType
    {
        MASTER,
        MUSIC,
        SFX
    }

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one AudioManager in the scene!!!");
            Debug.LogError("Destroying new instance!!!");
            Destroy(this);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");

        CreateInstances();
    }

    private void CreateInstances()
    {
        mainTheme = CreateEventInstance(FmodEvents.instance.playMainTheme);
        footsteps = CreateEventInstance(FmodEvents.instance.playFootstep);
        climb = CreateEventInstance(FmodEvents.instance.playClimbing);
        crown = CreateEventInstance(FmodEvents.instance.playCorona);
        gameTheme = CreateEventInstance(FmodEvents.instance.playGameTheme);
        voice_princess = CreateEventInstance(FmodEvents.instance.playPrincessVoice);
        voice_twiceSlodierAnt = CreateEventInstance(FmodEvents.instance.playTwiceSoldierVoice);
        voice_Scientist = CreateEventInstance(FmodEvents.instance.playSciantistVoice);
        voice_hystericant = CreateEventInstance(FmodEvents.instance.playHystericantVoice);
        voice_wideExoAnt = CreateEventInstance(FmodEvents.instance.playWideExoAntVoice);
        uiAccept = CreateEventInstance(FmodEvents.instance.playUIAccept);
        uiCancel = CreateEventInstance(FmodEvents.instance.playUICancel);
    }

    public void StartMusic()
    {
        mainTheme.start();
    }

    public void StopMusic()
    {
        mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void StartGameMusic()
    {
        gameTheme.start();
    }

    public void StopGameMusic()
    {
        gameTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StartVoice(DialogueManager.Actors actor)
    {
        switch (actor) 
        {
            case DialogueManager.Actors.PRINCESS:

                voice_princess.start();
                break;

            case DialogueManager.Actors.TWICE_SOLDIER_ANT:
                
                voice_twiceSlodierAnt.start();
                break;

            case DialogueManager.Actors.SCIANTIST:

                voice_Scientist.start();
                break;

            case DialogueManager.Actors.HYSTERICANT:

                voice_hystericant.start();
                break;

            case DialogueManager.Actors.WIDE_EXOSKELETON:

                voice_wideExoAnt.start();
                break;
        }
        
    }

    public void PauseVoice(DialogueManager.Actors actor)
    {
        switch (actor)
        {
            case DialogueManager.Actors.PRINCESS:

                voice_princess.setPaused(true);
                break;

            case DialogueManager.Actors.TWICE_SOLDIER_ANT:

                voice_twiceSlodierAnt.setPaused(true);
                break;

            case DialogueManager.Actors.SCIANTIST:

                voice_Scientist.setPaused(true);
                break;

            case DialogueManager.Actors.HYSTERICANT:

                voice_hystericant.setPaused(true);
                break;

            case DialogueManager.Actors.WIDE_EXOSKELETON:

                voice_wideExoAnt.setPaused(true);
                break;
        }
    }

    public void UnPauseVoice(DialogueManager.Actors actor)
    {
        switch (actor)
        {
            case DialogueManager.Actors.PRINCESS:

                voice_princess.setPaused(false);
                break;

            case DialogueManager.Actors.TWICE_SOLDIER_ANT:

                voice_twiceSlodierAnt.setPaused(false);
                break;

            case DialogueManager.Actors.SCIANTIST:

                voice_Scientist.setPaused(false);
                break;

            case DialogueManager.Actors.HYSTERICANT:

                voice_hystericant.setPaused(false);
                break;

            case DialogueManager.Actors.WIDE_EXOSKELETON:

                voice_wideExoAnt.setPaused(false);
                break;
        }
    }

    public void StopVoice(DialogueManager.Actors actor)
    {
        switch (actor)
        {
            case DialogueManager.Actors.PRINCESS:

                voice_princess.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;

            case DialogueManager.Actors.TWICE_SOLDIER_ANT:

                voice_twiceSlodierAnt.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;

            case DialogueManager.Actors.SCIANTIST:

                voice_Scientist.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;

            case DialogueManager.Actors.HYSTERICANT:

                voice_hystericant.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;

            case DialogueManager.Actors.WIDE_EXOSKELETON:

                voice_wideExoAnt.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;
        }
    }

    public void PlayFootstep()
    {
        footsteps.start();
    }

    public void PlayClimbing()
    {
        climb.start();
    }

    public void PlayCorona()
    {
        crown.start();
    }

    public void PlayUIAccept()
    {
        uiAccept.start();
    }

    public void PlayUICancel()
    {
        uiCancel.start();
    }

    public EventInstance CreateEventInstance(EventReference eventReference) 
    {
        EventInstance newEventInstance = RuntimeManager.CreateInstance(eventReference);
        return newEventInstance;
    }

    public void ChangeBusVolume(BusType bus, float newVolume)
    {
        switch (bus)
        {
            case BusType.MASTER:
                masterVolume = newVolume;
                masterBus.setVolume(masterVolume);
                break;
            case BusType.MUSIC:
                musicVolume = newVolume;
                musicBus.setVolume(musicVolume);
                break;
            case BusType.SFX:
                sfxVolume = newVolume;
                sfxBus.setVolume(sfxVolume);
                break;
            default:
                Debug.Log("Bus Type not supported: " + bus);
                break;
        }
    }

}
