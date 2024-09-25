using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    [field: Header("Main Theme")]
    [field: SerializeField] public EventReference playMainTheme {get; private set;}

    [field: Header("Game Theme")]
    [field: SerializeField] public EventReference playGameTheme { get; private set; }

    [field: Header("Footsteps")]
    [field: SerializeField] public EventReference playFootstep { get; private set; }

    [field: Header("Climbing")]
    [field: SerializeField] public EventReference playClimbing { get; private set; }

    [field: Header("Corona")]
    [field: SerializeField] public EventReference playCorona { get; private set; }
   
    [field: Header("Princess voice")]
    [field: SerializeField] public EventReference playPrincessVoice { get; private set; }

    [field: Header("Sciantist voice")]
    [field: SerializeField] public EventReference playSciantistVoice { get; private set; }

    [field: Header("TwiceSoldier voice")]
    [field: SerializeField] public EventReference playTwiceSoldierVoice { get; private set; }

    [field: Header("Hystericant voice")]
    [field: SerializeField] public EventReference playHystericantVoice { get; private set; }

    [field: Header("WideExoAnt voice")]
    [field: SerializeField] public EventReference playWideExoAntVoice { get; private set; }

    [field: Header("UI Accept")]
    [field: SerializeField] public EventReference playUIAccept { get; private set; }
    
    [field: Header("UI Cancel")]
    [field: SerializeField] public EventReference playUICancel { get; private set; }

    public static FmodEvents instance { get; private set; }

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
    }
}
