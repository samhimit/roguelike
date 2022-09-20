using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioManager instance = null;
    public AudioSource efxSource;
    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
    public void play(AudioClip clip)
    {
        Debug.Log("sound");
        efxSource.clip = clip;
        efxSource.Play();
    }
}
