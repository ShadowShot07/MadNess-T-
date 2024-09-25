using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventSystem : MonoBehaviour
{
    public UnityEvent dialogueStarted;
    public UnityEvent dialogueEnded;


    public static GlobalEventSystem instance { get; private set; }

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
