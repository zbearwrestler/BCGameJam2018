using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour {
    [Range(0f,1f)] public float volumeMultiplier;
    [System.Serializable]
    public struct Effect
    {
        public string Name;
        [Range(0f, 1f)]
        public float Volume;
        public AudioClip[] audioClips;
    }


    //Unity public variables
    [Header("Effects")]
    public Effect[] Effects;

    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void mPlay(string effectName)
    {
        for (int i = 0; i < Effects.Length; ++i)
        {
           if(Effects[i].Name == effectName)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = Effects[i].audioClips[Random.Range(0, Effects[i].audioClips.Length - 1)];
                audioSource.volume = Effects[i].Volume;
                audioSource.Play();
                Destroy(audioSource, audioSource.clip.length);
                return;
            }
        }
        Debug.LogWarning("No effect called:" + effectName);
    }

    //singolton stuff
    private static AudioManager instance;
    private static object syncRoot = new Object();

    private AudioManager()
    {
    }

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioManager>();
                if (instance == null)
                    Debug.LogError("no AudioManager in scene");   
            }
            return instance;
        }
    }

    public static void Play(string effectName)
    {
        Instance.mPlay(effectName);
    }

}
